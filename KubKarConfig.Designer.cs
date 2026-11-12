namespace KubKarsServerAdmin
{
    partial class KubKarConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            fbdialog = new FolderBrowserDialog();
            tLocation = new TextBox();
            lLocation = new Label();
            bSelectLocation = new Button();
            gbNodeEnv = new GroupBox();
            rbProduction = new RadioButton();
            rbDevelopment = new RadioButton();
            bClose = new Button();
            tSecret = new TextBox();
            lSecret = new Label();
            bShowSecret = new Button();
            tUpdateSecret = new TextBox();
            lUpdateSecret = new Label();
            bUpdateSecret = new Button();
            gbNodeEnv.SuspendLayout();
            SuspendLayout();
            // 
            // fbdialog
            // 
            fbdialog.ShowNewFolderButton = false;
            // 
            // tLocation
            // 
            tLocation.Location = new Point(116, 19);
            tLocation.Name = "tLocation";
            tLocation.ReadOnly = true;
            tLocation.Size = new Size(406, 23);
            tLocation.TabIndex = 0;
            // 
            // lLocation
            // 
            lLocation.AutoSize = true;
            lLocation.Location = new Point(26, 22);
            lLocation.Name = "lLocation";
            lLocation.Size = new Size(92, 15);
            lLocation.TabIndex = 1;
            lLocation.Text = "Kub Kars Folder:";
            // 
            // bSelectLocation
            // 
            bSelectLocation.Location = new Point(528, 19);
            bSelectLocation.Name = "bSelectLocation";
            bSelectLocation.Size = new Size(25, 23);
            bSelectLocation.TabIndex = 2;
            bSelectLocation.Text = "...";
            bSelectLocation.UseVisualStyleBackColor = true;
            bSelectLocation.Click += bSelectLocation_Click;
            // 
            // gbNodeEnv
            // 
            gbNodeEnv.Controls.Add(rbProduction);
            gbNodeEnv.Controls.Add(rbDevelopment);
            gbNodeEnv.Location = new Point(26, 59);
            gbNodeEnv.Name = "gbNodeEnv";
            gbNodeEnv.Size = new Size(132, 88);
            gbNodeEnv.TabIndex = 3;
            gbNodeEnv.TabStop = false;
            gbNodeEnv.Text = "Environment";
            // 
            // rbProduction
            // 
            rbProduction.AutoSize = true;
            rbProduction.Location = new Point(16, 47);
            rbProduction.Name = "rbProduction";
            rbProduction.Size = new Size(84, 19);
            rbProduction.TabIndex = 1;
            rbProduction.TabStop = true;
            rbProduction.Text = "Production";
            rbProduction.UseVisualStyleBackColor = true;
            // 
            // rbDevelopment
            // 
            rbDevelopment.AutoSize = true;
            rbDevelopment.Location = new Point(16, 22);
            rbDevelopment.Name = "rbDevelopment";
            rbDevelopment.Size = new Size(96, 19);
            rbDevelopment.TabIndex = 0;
            rbDevelopment.TabStop = true;
            rbDevelopment.Text = "Development";
            rbDevelopment.UseVisualStyleBackColor = true;
            // 
            // bClose
            // 
            bClose.Location = new Point(509, 164);
            bClose.Name = "bClose";
            bClose.Size = new Size(75, 23);
            bClose.TabIndex = 2;
            bClose.Text = "Close";
            bClose.UseVisualStyleBackColor = true;
            bClose.Click += bClose_Click;
            // 
            // tSecret
            // 
            tSecret.Location = new Point(234, 59);
            tSecret.Name = "tSecret";
            tSecret.ReadOnly = true;
            tSecret.Size = new Size(288, 23);
            tSecret.TabIndex = 4;
            // 
            // lSecret
            // 
            lSecret.AutoSize = true;
            lSecret.Location = new Point(186, 62);
            lSecret.Name = "lSecret";
            lSecret.Size = new Size(42, 15);
            lSecret.TabIndex = 5;
            lSecret.Text = "Secret:";
            // 
            // bShowSecret
            // 
            bShowSecret.Location = new Point(528, 58);
            bShowSecret.Name = "bShowSecret";
            bShowSecret.Size = new Size(46, 23);
            bShowSecret.TabIndex = 10;
            bShowSecret.Text = "Show";
            bShowSecret.UseVisualStyleBackColor = true;
            bShowSecret.Click += bShowSecret_Click;
            // 
            // tUpdateSecret
            // 
            tUpdateSecret.Location = new Point(186, 124);
            tUpdateSecret.Name = "tUpdateSecret";
            tUpdateSecret.Size = new Size(308, 23);
            tUpdateSecret.TabIndex = 11;
            // 
            // lUpdateSecret
            // 
            lUpdateSecret.AutoSize = true;
            lUpdateSecret.Location = new Point(186, 104);
            lUpdateSecret.Name = "lUpdateSecret";
            lUpdateSecret.Size = new Size(248, 15);
            lUpdateSecret.TabIndex = 12;
            lUpdateSecret.Text = "Update Secret (forces reset of login database):";
            // 
            // bUpdateSecret
            // 
            bUpdateSecret.Location = new Point(500, 122);
            bUpdateSecret.Name = "bUpdateSecret";
            bUpdateSecret.Size = new Size(75, 23);
            bUpdateSecret.TabIndex = 13;
            bUpdateSecret.Text = "Update";
            bUpdateSecret.UseVisualStyleBackColor = true;
            bUpdateSecret.Click += bUpdateSecret_Click;
            // 
            // KubKarConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(592, 198);
            Controls.Add(bClose);
            Controls.Add(bUpdateSecret);
            Controls.Add(lUpdateSecret);
            Controls.Add(tUpdateSecret);
            Controls.Add(bShowSecret);
            Controls.Add(lSecret);
            Controls.Add(tSecret);
            Controls.Add(gbNodeEnv);
            Controls.Add(bSelectLocation);
            Controls.Add(lLocation);
            Controls.Add(tLocation);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "KubKarConfig";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Kub Kars .env Config";
            FormClosed += KubKarConfig_FormClosed;
            gbNodeEnv.ResumeLayout(false);
            gbNodeEnv.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FolderBrowserDialog fbdialog;
        private TextBox tLocation;
        private Label lLocation;
        private Button bSelectLocation;
        private GroupBox gbNodeEnv;
        private RadioButton rbProduction;
        private RadioButton rbDevelopment;
        private TextBox tSecret;
        private Label lSecret;

        private Label lUsers;
        private Button bShowSecret;
        private TextBox tUpdateSecret;
        private Label lUpdateSecret;
        private Button bUpdateSecret;
        private Button bClose;
    }
}