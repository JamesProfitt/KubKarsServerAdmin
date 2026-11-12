using DotNetEnv;
using DotNetEnv.Extensions;
using Microsoft.EntityFrameworkCore;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KubKarsServerAdmin
{
    public partial class KubKarConfig : Form
    {

        public string? KubKarEnvFile { get; private set; }

        public KubKarConfig()
        {
            InitializeComponent();
        }

        private void bSelectLocation_Click(object sender, EventArgs e)
        {
            // The user selects the location of the Kub Kars App
            DialogResult result = fbdialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbdialog.SelectedPath))
            {
                // Folder location
                tLocation.Text = fbdialog.SelectedPath;
                KubKarEnvFile = Path.Combine(tLocation.Text, ".env");

                // The .env file in NodeJS should exist. 
                // When it exists, load the controls on the form with the values.
                // If not, this code creates one with defaults
                if (File.Exists(KubKarEnvFile))
                {
                    DotNetEnv.Env.Load(KubKarEnvFile);
                    var dict = DotNetEnv.Env.NoEnvVars().Load(KubKarEnvFile).ToDotEnvDictionary();

                    // NODE_ENV
                    if (DotNetEnv.Env.GetString("NODE_ENV") == "development")
                    {
                        rbDevelopment.Checked = true;
                        rbProduction.Checked = false;
                    }
                    else if (DotNetEnv.Env.GetString("NODE_ENV") == "production")
                    {
                        rbDevelopment.Checked = false;
                        rbProduction.Checked = true;
                    }
                    else
                    {
                        rbDevelopment.Checked = false;
                        rbProduction.Checked = false;
                    }

                    // SECRET

                    // This random string is used in the crypto module
                    // Hide it using the password char
                    tSecret.Text = DotNetEnv.Env.GetString("SESSION_SECRET");
                    tSecret.PasswordChar = '*';
                    tUpdateSecret.Text = "";

                }
                else
                {
                    // Make an alert visible, advising user. 
                    // Set defaults
                    rbDevelopment.Checked = true;
                    rbProduction.Checked = false;
                    tSecret.Text = "";
                    tUpdateSecret.Text = "";
                }

            }

        }

        // Show/Hide the secret key when the button is pressed
        private void bShowSecret_Click(object sender, EventArgs e)
        {
            if (tSecret.PasswordChar == '*')
            {
                tSecret.PasswordChar = '\0';
                bShowSecret.Text = "Hide";
            }
            else
            {
                tSecret.PasswordChar = '*';
                bShowSecret.Text = "Show";
            }

        }

        private void SetEnvironmentVariables()
        {
            // If we are closing the form without doing anything, stop here
            if (KubKarEnvFile == null)
                return;

            if (rbDevelopment.Checked == true)
            {
                System.Environment.SetEnvironmentVariable("NODE_ENV", "development");

            }
            else if (rbDevelopment.Checked == true)
            {
                System.Environment.SetEnvironmentVariable("NODE_ENV", "production");
            }

            if (tUpdateSecret.Text != "" & tSecret.Text != tUpdateSecret.Text)
            {
                // Secret updated, need to delete the users.db file
                // the sessions.db and users.db files will be recreated
                // as soon as the nodejs server is started

                string path1;

                if (string.IsNullOrEmpty(Path.GetDirectoryName(KubKarEnvFile)))
                {
                    path1 = KubKarEnvFile;
                }
                else
                {
                    path1 = Path.GetDirectoryName(KubKarEnvFile);
                }

                if (File.Exists(Path.Combine(path1, "var", "db", "users.db")))
                {
                    File.Delete(Path.Combine(path1, "var", "db", "users.db"));
                }

                if (File.Exists(Path.Combine(path1, "var", "db", "sessions.db")))
                {
                    File.Delete(Path.Combine(path1, "var", "db", "sessions.db"));
                }

                System.Environment.SetEnvironmentVariable("SESSION_SECRET", tUpdateSecret.Text);
            }
            else
            {
                System.Environment.SetEnvironmentVariable("SESSION_SECRET", tSecret.Text);
            }

            var dict = new Dictionary<string, string>
            {
                {"NODE_ENV", System.Environment.GetEnvironmentVariable("NODE_ENV")},
                {"SESSION_SECRET", System.Environment.GetEnvironmentVariable("SESSION_SECRET")}
            };

            string temp = string.Join(Environment.NewLine, dict.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            // Save to .env file
            try
            {
                File.WriteAllText(KubKarEnvFile, temp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving .env file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            if (tLocation.Text != "")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void KubKarConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetEnvironmentVariables();
        }

        private void bUpdateSecret_Click(object sender, EventArgs e)
        {
            SetEnvironmentVariables();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
