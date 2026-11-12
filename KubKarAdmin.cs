using Microsoft.EntityFrameworkCore;
using ScottPlot;
using SkiaSharp;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Management;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.ServiceProcess;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace KubKarsServerAdmin
{


    public partial class KubKarAdmin : Form
    {

        private UserContext? users;
        private SessionContext? sessions;
        private string UserDBConnectionString;
        private string SessionDBConnectionString;
        private NodeJSManager? nodeman;
        private int[] NodePids;
        private int NodeCount;

        // Leave these here in case we find a use for them.
        //[DllImport("kernel32", SetLastError = true)]
        //private static extern bool AttachConsole(int dwProcessId);
        //
        //[DllImport("kernel32", SetLastError = true)]
        //private static extern void FreeConsole();

        public KubKarAdmin()
        {
            InitializeComponent();
            UserDBConnectionString = "";
            SessionDBConnectionString = "";
            UpdateSvcStatus();
            nodeman = new NodeJSManager(this.rtbNodeJSLog, this.fpAvgQueryTime);
            // If Node is running when the form first loads, it is being run from outside the app.
            // We cannot connect to the StdOut and process the logs.
            // We will create a button in the grid for the user to click if they want to kill it.
            dgvExistingPids.Columns.Add("ProcessId", "ProcessId");
            dgvExistingPids.Columns.Add("CWD", "CWD");

            // Attach
            // We can Attach to a console, but it doesn't help get the StdOut
            // Leave this here in case we want something else in future.

            //DataGridViewButtonColumn btnColAttach = new DataGridViewButtonColumn();
            //btnColAttach.Name = "Attach";
            //btnColAttach.HeaderText = "Attach";
            //btnColAttach.Text = "Attach";
            //btnColAttach.UseColumnTextForButtonValue = true;
            //dgvExistingPids.Columns.Add(btnColAttach);

            // Kill Process Button
            DataGridViewButtonColumn btnColKill = new DataGridViewButtonColumn();
            btnColKill.Name = "Kill";
            btnColKill.HeaderText = "Kill";
            btnColKill.Text = "Kill";
            btnColKill.UseColumnTextForButtonValue = true;
            dgvExistingPids.Columns.Add(btnColKill);

            // Refresh Grid
            IsNodeRunning("");
        }




        private void bConfigure_Click(object sender, EventArgs e)
        {
            // Open the config form
            using (KubKarConfig configfrm = new KubKarConfig())
            {
                if (configfrm.ShowDialog() == DialogResult.OK)
                {
                    tNodeLocation.Text = Path.GetDirectoryName(configfrm.KubKarEnvFile);
                    LoadUsersDB();
                    LoadSessionsDB();
                    IsNodeRunning(tNodeLocation.Text);
                }
            }
        }

        private void KubKarAdmin_Load(object sender, EventArgs e)
        {

        }


        private void LoadUsersDB()
        {
            // The path to the database:
            string UserDBPath = Path.Combine(tNodeLocation.Text, "var", "db", "users.db");

            // Does the database exist?
            if (!File.Exists(UserDBPath))
            {
                // We need to create a blank db and initialize it
                this.users.Database.EnsureDeleted();
                this.users.Database.EnsureCreated();
            }

            // Connect the grid to the db
            // We can connect to the database
            this.UserDBConnectionString = $"Data Source={UserDBPath}";
            this.users = new UserContext() { ConnectionString = this.UserDBConnectionString };
            this.users.Users.Load();
            dgvUsers.DataSource = this.users.Users.Local.ToBindingList();
            dgvUsers.Refresh();

        }

        private void LoadSessionsDB()
        {
            // The path to the database:
            string SessionDBPath = Path.Combine(tNodeLocation.Text, "var", "db", "sessions.db");

            // Does the database exist?
            if (!File.Exists(SessionDBPath))
            {
                // We need to create a blank db and initialize it
                this.sessions.Database.EnsureDeleted();
                this.sessions.Database.EnsureCreated();
            }

            // Connect the grid to the db
            // We can connect to the database
            this.SessionDBConnectionString = $"Data Source={SessionDBPath}";
            this.sessions = new SessionContext() { ConnectionString = this.SessionDBConnectionString };
            this.sessions.Sessions.Load();
            dgvSessions.DataSource = this.sessions.Sessions.Local.ToBindingList();
            dgvSessions.Refresh();
        }

        private void KubKarAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.users?.Dispose();
            this.users = null;
            this.sessions?.Dispose();
            this.sessions = null;
            nodeman?.StopNodeApp();
        }


        private bool IsNodeRunning(string targetDirectory)
        {
            rtbNodeStatus.Clear();
            string normalizedTarget = "";

            if (!String.IsNullOrEmpty(targetDirectory))
            {
                normalizedTarget = Path.GetFullPath(targetDirectory).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }

            NodeCount = 0;
            NodePids = [];

            rtbNodeStatus.AppendText("None Found");
            dgvExistingPids.Rows.Clear();

            string query = "SELECT ProcessId, CommandLine, ExecutablePath FROM Win32_Process WHERE Name = 'node.exe'";
            using (var searcher = new ManagementObjectSearcher(query))
            {
                using (var collection = searcher.Get())
                {
                    if (collection.Count > 0)
                    {
                        rtbNodeStatus.Clear();
                    }

                    foreach (ManagementObject process in collection)
                    {
                        // Parse some of the variables
                        int.TryParse(process["ProcessId"].ToString(), out int procid);
                        NodePids.Append(procid);
                        string commandLine = process["CommandLine"]?.ToString() ?? string.Empty;
                        // Use the external class to get the directory
                        string cwd = ProcessUtilities.GetCurrentDirectory(procid).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                        // Add to the grid
                        dgvExistingPids.Rows.Add(procid, cwd);
                        // Increment counter if paths are equal
                        if (Path.Equals(cwd, normalizedTarget))
                        {
                            NodeCount++;
                        }

                    }
                }
            }

            if (NodeCount > 0)
            {
                rtbNodeStatus.Clear();
                rtbNodeStatus.Text = "Node Processes Found: " + NodeCount;
                return true;
            }
            else
            {
                rtbNodeStatus.Clear();
                rtbNodeStatus.Text = "None Found";
                return false;
            }

        }

        private void timerDGVRefreshTimer_Tick(object sender, EventArgs e)
        {
            IsNodeRunning(tNodeLocation.Text);

            if (this.users != null)
            {
                this.users.Users.Load();
                dgvUsers.Refresh();
            }

            if (this.sessions != null)
            {
                this.sessions.Sessions.Load();
                dgvSessions.Refresh();
            }

        }

        private void bEditSvcNm_Click(object sender, EventArgs e)
        {
            if (bEditSvcNm.Text == "Edit")
            {
                tSvcName.Enabled = true;
                tSvcName.ReadOnly = false;
                tSvcName.BackColor = SystemColors.ControlLightLight;
                bEditSvcNm.Text = "Save";
            }
            else if (bEditSvcNm.Text == "Save")
            {
                if (tSvcName.Text != "")
                {
                    tSvcName.Enabled = false;
                    tSvcName.ReadOnly = true;
                    bEditSvcNm.Text = "Edit";
                    tSvcName.BackColor = SystemColors.Control;
                    UpdateSvcStatus();
                }
            }
        }

        private void UpdateSvcStatus()
        {
            try
            {
                using (ServiceController sc = new ServiceController(tSvcName.Text))
                {
                    tSvcStatus.Text = sc.Status.ToString();
                }
            }
            catch (Exception ex)
            {
                tSvcStatus.Text = ex.Message;
                MessageBox.Show(ex.Message, "Error Updating Service Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bStartDB_Click(object sender, EventArgs e)
        {
            try
            {
                using (ServiceController sc = new ServiceController(tSvcName.Text))
                {
                    if (sc.Status == ServiceControllerStatus.Stopped)
                    {
                        sc.Start();
                        sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                        UpdateSvcStatus();
                    }
                }
            }
            catch (Exception ex)
            {
                tSvcStatus.Text = ex.Message;
                MessageBox.Show(ex.Message, "Error Starting Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bStopDB_Click(object sender, EventArgs e)
        {
            try
            {
                using (ServiceController sc = new ServiceController(tSvcName.Text))
                {
                    if (sc.Status == ServiceControllerStatus.Running)
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                        UpdateSvcStatus();
                    }
                }
            }
            catch (Exception ex)
            {
                tSvcStatus.Text = ex.Message;
                MessageBox.Show(ex.Message, "Error Stopping Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bRestartDB_Click(object sender, EventArgs e)
        {
            try
            {
                using (ServiceController sc = new ServiceController(tSvcName.Text))
                {
                    sc.Refresh();
                    UpdateSvcStatus();
                    if (sc.CanStop)
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped);
                        UpdateSvcStatus();
                        sc.Start();
                        sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                        UpdateSvcStatus();
                    }
                }
            }
            catch (Exception ex)
            {
                tSvcStatus.Text = ex.Message;
                MessageBox.Show(ex.Message, "Error Restarting Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bRefreshSvc_Click(object sender, EventArgs e)
        {
            try
            {
                using (ServiceController sc = new ServiceController(tSvcName.Text))
                {
                    sc.Refresh();
                    UpdateSvcStatus();
                }
            }
            catch (Exception ex)
            {
                tSvcStatus.Text = ex.Message;
                MessageBox.Show(ex.Message, "Error Refreshing Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsers_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (this.users != null)
            {
                this.users.Users.Load();
                dgvUsers.Refresh();
            }

            if (this.sessions != null)
            {
                this.sessions.Sessions.Load();
                dgvSessions.Refresh();
            }
        }

        private void bSaveUsers_Click(object sender, EventArgs e)
        {
            if (this.users != null)
            {
                this.users.SaveChanges();
                this.users.Users.Load();
                dgvUsers.Refresh();
            }
        }

        private void bSaveSessions_Click(object sender, EventArgs e)
        {
            if (this.sessions != null)
            {
                this.users.SaveChanges();
                this.sessions.Sessions.Load();
                dgvSessions.Refresh();
            }
        }

        private void bStartNode_Click(object sender, EventArgs e)
        {
            // If there is a node.exe process running from the same CWD
            // we should not start another process.

            // Multiple Node.exe processes could be running at the same time, but
            // we ignore anything that is not from our CWD

            if (tNodeLocation.Text == "")
            {
                rtbNodeJSLog.AppendText("No Location Provided." + Environment.NewLine);
                return;
            }

            if (IsNodeRunning(tNodeLocation.Text))
            {
                MessageBox.Show("Node is already running.");
            }
            else
            {
                // Begin Node Process
                rtbNodeJSLog.AppendText("Starting NodeJS..." + Environment.NewLine);
                rtbNodeJSLog.AppendText("ARG1:" + Path.Combine(tNodeLocation.Text, "server.js" + Environment.NewLine));
                rtbNodeJSLog.AppendText("ARG2:" + "--env-file " + Path.Combine(tNodeLocation.Text, ".env" + Environment.NewLine));
                nodeman?.StartNodeApp(Path.Combine(tNodeLocation.Text, "server.js"), "--env-file " + Path.Combine(tNodeLocation.Text, ".env"));
            }

        }

        private void bStopNode_Click(object sender, EventArgs e)
        {
            // Stopping Node Process
            rtbNodeJSLog.AppendText("Stopping NodeJS..." + Environment.NewLine);
            nodeman?.StopNodeApp();
            rtbNodeJSLog.AppendText("NodeJS Stopped." + Environment.NewLine);
        }

        private void bRestart_Click(object sender, EventArgs e)
        {
            if (tNodeLocation.Text == "")
            {
                rtbNodeJSLog.AppendText("No Location Provided." + Environment.NewLine);
                return;
            }

            if (IsNodeRunning(tNodeLocation.Text))
            {
                // Stopping Node Process
                rtbNodeJSLog.AppendText("Stopping NodeJS..." + Environment.NewLine);
                nodeman?.StopNodeApp();
                rtbNodeJSLog.AppendText("NodeJS Stopped." + Environment.NewLine);
                // Begin Node Process
                rtbNodeJSLog.AppendText("Starting NodeJS..." + Environment.NewLine);
                rtbNodeJSLog.AppendText("ARG1:" + Path.Combine(tNodeLocation.Text, "server.js" + Environment.NewLine));
                rtbNodeJSLog.AppendText("ARG2:" + "--env-file " + Path.Combine(tNodeLocation.Text, ".env" + Environment.NewLine));
                nodeman?.StartNodeApp(Path.Combine(tNodeLocation.Text, "server.js"), "--env-file " + Path.Combine(tNodeLocation.Text, ".env"));
            }
        }

        private void bClearNodeLog_Click(object sender, EventArgs e)
        {
            if (rtbNodeJSLog.TextLength > 0)
            {
                if (MessageBox.Show("Are you sure?", "Clear Log Lines?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    rtbNodeJSLog.Clear();
                }
            }
        }

        private void bExportLog_Click(object sender, EventArgs e)
        {
            // Save the log to a file
            if (rtbNodeJSLog.TextLength > 0)
            {
                sfdSaveLog.FileName = "KubKarsLog_" + DateTime.Now.ToString("yyyy-MMM-dd_HH-mm-ss") + ".log";
                if (sfdSaveLog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        rtbNodeJSLog.SaveFile(sfdSaveLog.FileName, RichTextBoxStreamType.PlainText);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Saving File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rtbNodeJSLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            // When node generates a link, allow clicking on it
            try
            {
                // Opens the clicked URL in the system's default browser
                Process.Start(new ProcessStartInfo
                {
                    FileName = e.LinkText,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Link", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvExistingPids_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Click is on button column

            //if (e.RowIndex >= 0 && e.ColumnIndex == dgvExistingPids.Columns["Attach"].Index)
            //{
            //    int.TryParse(dgvExistingPids.Rows[e.RowIndex].Cells["ProcessId"].Value.ToString(), out int ProcessId);
            //    string rowName = dgvExistingPids.Rows[e.RowIndex].Cells["CWD"].Value.ToString();
            //
            //    try
            //    {
            //        FreeConsole();
            //        nodeman?.AttachNodeApp(ProcessId);
            //        tNodeLocation.Text = rowName;
            //        LoadUsersDB();
            //        LoadSessionsDB();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Error Attaching to Console Process", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    finally
            //    {
            //        IsNodeRunning(tNodeLocation.Text);
            //    }
            //}
            //else 
                
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvExistingPids.Columns["Kill"].Index)
            {
                // Try killing the listed process, and refresh the grid afterwards.
                try
                {
                    int.TryParse(dgvExistingPids.Rows[e.RowIndex].Cells["ProcessId"].Value.ToString(), out int ProcessId);
                    using Process process = Process.GetProcessById(ProcessId);
                    process.Kill();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Killing Process", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally 
                {
                    IsNodeRunning(tNodeLocation.Text);
                }
            }

        }
    }
}
