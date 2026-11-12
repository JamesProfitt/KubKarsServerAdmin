using DotNetEnv;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ScottPlot;
using ScottPlot.WinForms;
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Color = System.Drawing.Color;

namespace KubKarsServerAdmin
{
    public class NodeJSManager
    {
        private Process? _nodeProcess;
        private readonly RichTextBox _outputBox;
        private readonly FormsPlot _formsplot;

        // A dataframe to hold the underlying data for the graphs
        // We will use this to generate the multiple series on the graph
        private DataTable api_raw;
        private DataTable api_aggregate;

        private DateTime[] Xs;
        private decimal[] Ys;

        public NodeJSManager(RichTextBox outputBox, FormsPlot formsplot)
        {
            _outputBox = outputBox;
            _formsplot = formsplot;
            api_raw = new DataTable("api_raw");
            api_raw.Columns.Add("DF_api_nm", typeof(string));
            api_raw.Columns.Add("DF_api_ts", typeof(DateTime));
            api_raw.Columns.Add("DF_api_tm", typeof(decimal));

            api_aggregate = new DataTable("api_aggregate");
            api_aggregate.Columns.Add("DF_api_ts", typeof(DateTime));
            api_aggregate.Columns.Add("DF_api_tm", typeof(decimal));

        }

        public void StartNodeApp(string scriptPath, string arguments = "")
        {
            _nodeProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "node",
                    Arguments = $"{scriptPath} {arguments}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            _nodeProcess.OutputDataReceived += (sender, e) => ProcessLine(e.Data, isError: false);
            _nodeProcess.ErrorDataReceived += (sender, e) => ProcessLine(e.Data, isError: true);

            _nodeProcess.Start();
            _nodeProcess.BeginOutputReadLine();
            _nodeProcess.BeginErrorReadLine();
        }

        //public void AttachNodeApp(int ProcessId)
        //{
        //    _nodeProcess = Process.GetProcessById(ProcessId);
        //    _nodeProcess.OutputDataReceived += (sender, e) => ProcessLine(e.Data, isError: false);
        //    _nodeProcess.ErrorDataReceived += (sender, e) => ProcessLine(e.Data, isError: true);
        //    _nodeProcess.BeginOutputReadLine();
        //    _nodeProcess.BeginErrorReadLine();
        //}

        private void ProcessLine(string? line, bool isError)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            if (isError)
            {
                AppendToUi($"[Node Error] {line}{Environment.NewLine}", Color.Red);
                return;
            }

            try
            {
                // Parse JSON structure
                using JsonDocument doc = JsonDocument.Parse(line);
                JsonElement root = doc.RootElement;

                // Try and parse the structure to find the fields we care about
                bool JS_level_b = root.TryGetProperty("level", out JsonElement JS_level);
                bool JS_msg_b = root.TryGetProperty("msg", out JsonElement JS_msg);
                bool JS_tstamp_b = root.TryGetProperty("time", out JsonElement JS_tstamp);
                bool JS_pid_b = root.TryGetProperty("pid", out JsonElement JS_pid);
                bool JS_hostname_b = root.TryGetProperty("hostname", out JsonElement JS_hostname);
                bool JS_url_b = root.TryGetProperty("url", out JsonElement JS_url);
                bool JS_method_b = root.TryGetProperty("method", out JsonElement JS_method);
                bool JS_user_b = root.TryGetProperty("user", out JsonElement JS_user);
                bool JS_path_b = root.TryGetProperty("path", out JsonElement JS_path);

                string JS_username_str = "";
                if (JS_user_b)
                {
                    JS_user.TryGetProperty("username", out JsonElement JS_username);
                    JS_username_str = JS_user_b ? JS_username.GetString() : "";
                }

                string ?JS_level_str = JS_level_b ? JS_level.GetString()?.ToUpper() : "INFO";
                string ?JS_msg_str = JS_msg_b ? JS_msg.GetString() : "";
                DateTimeOffset JS_msg_time = DateTimeOffset.FromUnixTimeMilliseconds(JS_tstamp.GetInt64()).ToLocalTime();
                // Force timer to the nearest 30 seconds
                DateTime JS_msg_time_rd = new DateTime(JS_msg_time.Year, JS_msg_time.Month, JS_msg_time.Day, JS_msg_time.Hour, JS_msg_time.Minute, (int)Math.Round(JS_msg_time.Second / 30.0), 0, 0, DateTimeKind.Local);
                Int32 ?JS_pid_int = JS_pid_b ? JS_pid.GetInt32() : 0;
                string ?JS_hostname_str = JS_hostname_b ? JS_hostname.GetString() : "";
                string ?JS_url_str = JS_url_b ? JS_url.GetString() : "";
                string ?JS_method_str = JS_method_b ? JS_method.GetString() : "";
                string ?JS_user_str = JS_user_b ? JS_user.ToString() : "";
                string ?JS_path_str = JS_path_b ? JS_path.GetString() : "";

                Color logColor = JS_level_str switch
                {
                    "ERROR" => Color.DarkRed,
                    "WARN" => Color.Orange,
                    _ => Color.DarkGreen
                };

                // The msg column may contain the string ROUTE_PEFORMANCE: we don't display this directly
                // instead we parse the time element and keep it for graphing.

                if (JS_msg_b == true && JS_msg_str.StartsWith("ROUTE_PERFORMANCE"))
                {
                    string JS_url_full = JS_msg_str.Split("|")[1];
                    string JS_api = JS_url_full.Split("/")[2] + '_' + JS_url_full.Split("/")[3];
                    decimal.TryParse(JS_msg_str.Split("|")[2], out decimal JS_query_time);

                    // Append to API_RAW datatable (use the rounded timestamp instead of the raw version)
                    api_raw.Rows.Add(JS_api, JS_msg_time_rd, JS_query_time);
                    // TODO: Keep the latest 10K records to keep the memory use down

                    // Aggregate the data by timestamp
                    // Clear the aggregate before recreating it, otherwise we will duplicate data
                    // because this runs each time we get a new line in the log.
                    api_aggregate.Clear();
                    var api_groupby = api_raw.AsEnumerable()
                                               .GroupBy(row => row.Field<DateTime>("DF_api_ts"))
                                               .Select(group => new {
                                                   DF_api_ts = group.Key,
                                                   DF_api_tm = group.Average(row => row.Field<decimal>("DF_api_tm")),
                                               });

                    foreach (var item in api_groupby)
                    {
                        this.api_aggregate.Rows.Add(item.DF_api_ts, item.DF_api_tm);
                    }

                    // Add to ScottPlot
                    AppendToGraph();

                }
                else if (JS_msg_b == true)
                {
                    AppendToUi($"[{JS_msg_time}]: {JS_level_str} - {JS_msg_str}{Environment.NewLine}", logColor);
                }
                else if (JS_url_b == true)
                {
                    AppendToUi($"[{JS_msg_time}]: {JS_level_str} - {JS_username_str} - {JS_url_str}{Environment.NewLine}", logColor);
                }
                else
                {
                    AppendToUi($"[JSON]: {line}{Environment.NewLine}", Color.Blue);
                }
            }
            catch (JsonException)
            {
                // Fallback for non-JSON console logs
                AppendToUi($"[Raw]: {line}{Environment.NewLine}", Color.DimGray);
            }
        }

        // Safely marshals the data from the background thread to the WinForms UI thread
        private void AppendToUi(string text, Color color)
        {
            if (_outputBox.InvokeRequired)
            {
                // Use BeginInvoke for non-blocking asynchronous UI updates
                _outputBox.BeginInvoke(new Action(() => AppendToUi(text, color)));
            }
            else
            {
                // We are safe on the UI thread here
                _outputBox.SelectionStart = _outputBox.TextLength;
                _outputBox.SelectionLength = 0;
                _outputBox.SelectionColor = color;
                _outputBox.AppendText(text);
                _outputBox.SelectionColor = _outputBox.ForeColor; // Reset color

                // Auto-scroll to the bottom of the feed
                _outputBox.ScrollToCaret();
            }
        }

        private void AppendToGraph()
        {
            // Each api_nm could represent a series on the graph.
            // There may be multiple api_tm values for each api_end_ts/api_nm combination due to user concurrency
            // Qs: Do we need to aggregate/average points per timeslice
            //     Should we group points, nearest 1s, 5s, 10s?
            //     Adding multiple series
            //     Do we create a master list of expected series, and then filter those without data?
            //       HashSets will help (can do same as EXCEPT in sql)
            //     Xs / Ys. DateTimes. Might be missing values for multiple time series
            //     Or:
            //     Aggregate all APIs into a single value per timeslice (say, 30s, 1min)
            //     Average : sum of all values within X time (30s) / N
            //     Min/Max per timeslice : Add a label
            // 

            // A: Aggregate the data for now.
            // Nearest 30s

            if (_formsplot.InvokeRequired)
            {
                // Use BeginInvoke for non-blocking asynchronous UI updates
                _formsplot.BeginInvoke(new Action(() => AppendToGraph()));
            }
            else
            {
                lock (_formsplot)
                {
                    // We are safe on the UI thread here
                    // Clear the plot before adding a new one to stop plotting multiples.
                    Xs = api_aggregate.AsEnumerable().Select(row => Convert.ToDateTime(row["DF_api_ts"])).ToArray();
                    Ys = api_aggregate.AsEnumerable().Select(row => Convert.ToDecimal(row["DF_api_tm"])).ToArray();
                    _formsplot.Plot.Clear();
                    _formsplot.Plot.Add.SignalXY(Xs, Ys);
                    _formsplot.Plot.Axes.DateTimeTicksBottom();
                    _formsplot.Refresh();
                }
            }
        }

        public void StopNodeApp()
        {
            if (_nodeProcess != null )
            {
                if (!_nodeProcess.HasExited)
                {
                    _nodeProcess.Kill(entireProcessTree: true);
                    _nodeProcess.Dispose();
                    _nodeProcess = null;
                }
            }
        }

    }
}
