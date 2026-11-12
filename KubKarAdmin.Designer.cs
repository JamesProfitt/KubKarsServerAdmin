namespace KubKarsServerAdmin
{
    partial class KubKarAdmin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            bStartNode = new Button();
            rtbNodeJSLog = new RichTextBox();
            timerDGVRefreshTimer = new System.Windows.Forms.Timer(components);
            bStopNode = new Button();
            lNodeStatus = new Label();
            tSvcStatus = new TextBox();
            lSQLStatus = new Label();
            bStartDB = new Button();
            lNodeLog = new Label();
            bConfigure = new Button();
            bRestartDB = new Button();
            bStopDB = new Button();
            gbDatabase = new GroupBox();
            bRefreshSvc = new Button();
            bEditSvcNm = new Button();
            label1 = new Label();
            tSvcName = new TextBox();
            bRestart = new Button();
            groupBox2 = new GroupBox();
            dgvExistingPids = new DataGridView();
            bClearNodeLog = new Button();
            rtbNodeStatus = new RichTextBox();
            bExportLog = new Button();
            lNodeLoc = new Label();
            tNodeLocation = new TextBox();
            fpAvgQueryTime = new ScottPlot.WinForms.FormsPlot();
            dgvUsers = new DataGridView();
            dgvSessions = new DataGridView();
            lSessions = new Label();
            lUsers = new Label();
            bSaveUsers = new Button();
            bSaveSessions = new Button();
            lServerPerformance = new Label();
            bClose = new Button();
            sfdSaveLog = new SaveFileDialog();
            gbDatabase.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExistingPids).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSessions).BeginInit();
            SuspendLayout();
            // 
            // bStartNode
            // 
            bStartNode.Location = new Point(473, 51);
            bStartNode.Name = "bStartNode";
            bStartNode.Size = new Size(100, 23);
            bStartNode.TabIndex = 0;
            bStartNode.Text = "Start";
            bStartNode.UseVisualStyleBackColor = true;
            bStartNode.Click += bStartNode_Click;
            // 
            // rtbNodeJSLog
            // 
            rtbNodeJSLog.Location = new Point(15, 162);
            rtbNodeJSLog.Name = "rtbNodeJSLog";
            rtbNodeJSLog.ReadOnly = true;
            rtbNodeJSLog.Size = new Size(558, 281);
            rtbNodeJSLog.TabIndex = 1;
            rtbNodeJSLog.Text = "";
            rtbNodeJSLog.LinkClicked += rtbNodeJSLog_LinkClicked;
            // 
            // timerDGVRefreshTimer
            // 
            timerDGVRefreshTimer.Enabled = true;
            timerDGVRefreshTimer.Interval = 30000;
            timerDGVRefreshTimer.Tick += timerDGVRefreshTimer_Tick;
            // 
            // bStopNode
            // 
            bStopNode.Location = new Point(473, 83);
            bStopNode.Name = "bStopNode";
            bStopNode.Size = new Size(100, 23);
            bStopNode.TabIndex = 2;
            bStopNode.Text = "Stop";
            bStopNode.UseVisualStyleBackColor = true;
            bStopNode.Click += bStopNode_Click;
            // 
            // lNodeStatus
            // 
            lNodeStatus.AutoSize = true;
            lNodeStatus.Location = new Point(15, 23);
            lNodeStatus.Name = "lNodeStatus";
            lNodeStatus.Size = new Size(42, 15);
            lNodeStatus.TabIndex = 3;
            lNodeStatus.Text = "Status:";
            // 
            // tSvcStatus
            // 
            tSvcStatus.BackColor = SystemColors.Control;
            tSvcStatus.Location = new Point(177, 46);
            tSvcStatus.Name = "tSvcStatus";
            tSvcStatus.ReadOnly = true;
            tSvcStatus.Size = new Size(241, 23);
            tSvcStatus.TabIndex = 7;
            // 
            // lSQLStatus
            // 
            lSQLStatus.AutoSize = true;
            lSQLStatus.Location = new Point(89, 49);
            lSQLStatus.Name = "lSQLStatus";
            lSQLStatus.Size = new Size(82, 15);
            lSQLStatus.TabIndex = 6;
            lSQLStatus.Text = "Service Status:";
            // 
            // bStartDB
            // 
            bStartDB.Location = new Point(493, 20);
            bStartDB.Name = "bStartDB";
            bStartDB.Size = new Size(100, 23);
            bStartDB.TabIndex = 5;
            bStartDB.Text = "Start";
            bStartDB.UseVisualStyleBackColor = true;
            bStartDB.Click += bStartDB_Click;
            // 
            // lNodeLog
            // 
            lNodeLog.AutoSize = true;
            lNodeLog.Location = new Point(15, 145);
            lNodeLog.Name = "lNodeLog";
            lNodeLog.Size = new Size(106, 15);
            lNodeLog.TabIndex = 8;
            lNodeLog.Text = "NodeJs Server Log:";
            // 
            // bConfigure
            // 
            bConfigure.Location = new Point(473, 20);
            bConfigure.Name = "bConfigure";
            bConfigure.Size = new Size(100, 23);
            bConfigure.TabIndex = 9;
            bConfigure.Text = "Configure";
            bConfigure.UseVisualStyleBackColor = true;
            bConfigure.Click += bConfigure_Click;
            // 
            // bRestartDB
            // 
            bRestartDB.Location = new Point(493, 72);
            bRestartDB.Name = "bRestartDB";
            bRestartDB.Size = new Size(100, 24);
            bRestartDB.TabIndex = 10;
            bRestartDB.Text = "Restart";
            bRestartDB.UseVisualStyleBackColor = true;
            bRestartDB.Click += bRestartDB_Click;
            // 
            // bStopDB
            // 
            bStopDB.Location = new Point(493, 45);
            bStopDB.Name = "bStopDB";
            bStopDB.Size = new Size(100, 24);
            bStopDB.TabIndex = 11;
            bStopDB.Text = "Stop";
            bStopDB.UseVisualStyleBackColor = true;
            bStopDB.Click += bStopDB_Click;
            // 
            // gbDatabase
            // 
            gbDatabase.Controls.Add(bRefreshSvc);
            gbDatabase.Controls.Add(bEditSvcNm);
            gbDatabase.Controls.Add(label1);
            gbDatabase.Controls.Add(tSvcName);
            gbDatabase.Controls.Add(bStopDB);
            gbDatabase.Controls.Add(bStartDB);
            gbDatabase.Controls.Add(bRestartDB);
            gbDatabase.Controls.Add(tSvcStatus);
            gbDatabase.Controls.Add(lSQLStatus);
            gbDatabase.Location = new Point(616, 12);
            gbDatabase.Name = "gbDatabase";
            gbDatabase.Size = new Size(608, 106);
            gbDatabase.TabIndex = 12;
            gbDatabase.TabStop = false;
            gbDatabase.Text = "Database Management";
            // 
            // bRefreshSvc
            // 
            bRefreshSvc.Location = new Point(426, 45);
            bRefreshSvc.Name = "bRefreshSvc";
            bRefreshSvc.Size = new Size(24, 23);
            bRefreshSvc.TabIndex = 15;
            bRefreshSvc.Text = "⟲";
            bRefreshSvc.UseVisualStyleBackColor = true;
            bRefreshSvc.Click += bRefreshSvc_Click;
            // 
            // bEditSvcNm
            // 
            bEditSvcNm.Location = new Point(424, 20);
            bEditSvcNm.Name = "bEditSvcNm";
            bEditSvcNm.Size = new Size(41, 23);
            bEditSvcNm.TabIndex = 14;
            bEditSvcNm.Text = "Edit";
            bEditSvcNm.UseVisualStyleBackColor = true;
            bEditSvcNm.Click += bEditSvcNm_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 23);
            label1.Name = "label1";
            label1.Size = new Size(134, 15);
            label1.TabIndex = 13;
            label1.Text = "Windows Service Name:";
            // 
            // tSvcName
            // 
            tSvcName.BackColor = SystemColors.Control;
            tSvcName.Enabled = false;
            tSvcName.Location = new Point(177, 20);
            tSvcName.Name = "tSvcName";
            tSvcName.Size = new Size(241, 23);
            tSvcName.TabIndex = 12;
            tSvcName.Text = "MSSQLSERVER";
            // 
            // bRestart
            // 
            bRestart.Location = new Point(473, 115);
            bRestart.Name = "bRestart";
            bRestart.Size = new Size(100, 23);
            bRestart.TabIndex = 0;
            bRestart.Text = "Restart";
            bRestart.UseVisualStyleBackColor = true;
            bRestart.Click += bRestart_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvExistingPids);
            groupBox2.Controls.Add(bClearNodeLog);
            groupBox2.Controls.Add(rtbNodeStatus);
            groupBox2.Controls.Add(bExportLog);
            groupBox2.Controls.Add(lNodeLoc);
            groupBox2.Controls.Add(bRestart);
            groupBox2.Controls.Add(rtbNodeJSLog);
            groupBox2.Controls.Add(lNodeLog);
            groupBox2.Controls.Add(tNodeLocation);
            groupBox2.Controls.Add(bConfigure);
            groupBox2.Controls.Add(bStartNode);
            groupBox2.Controls.Add(bStopNode);
            groupBox2.Controls.Add(lNodeStatus);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(589, 483);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Kub Kar NodeJS Monitoring";
            // 
            // dgvExistingPids
            // 
            dgvExistingPids.AllowUserToAddRows = false;
            dgvExistingPids.AllowUserToDeleteRows = false;
            dgvExistingPids.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExistingPids.Location = new Point(15, 49);
            dgvExistingPids.Name = "dgvExistingPids";
            dgvExistingPids.ReadOnly = true;
            dgvExistingPids.Size = new Size(452, 89);
            dgvExistingPids.TabIndex = 15;
            dgvExistingPids.CellClick += dgvExistingPids_CellClick;
            // 
            // bClearNodeLog
            // 
            bClearNodeLog.Location = new Point(15, 449);
            bClearNodeLog.Name = "bClearNodeLog";
            bClearNodeLog.Size = new Size(75, 23);
            bClearNodeLog.TabIndex = 14;
            bClearNodeLog.Text = "Clear Log";
            bClearNodeLog.UseVisualStyleBackColor = true;
            bClearNodeLog.Click += bClearNodeLog_Click;
            // 
            // rtbNodeStatus
            // 
            rtbNodeStatus.BackColor = SystemColors.Control;
            rtbNodeStatus.Location = new Point(63, 20);
            rtbNodeStatus.Multiline = false;
            rtbNodeStatus.Name = "rtbNodeStatus";
            rtbNodeStatus.ReadOnly = true;
            rtbNodeStatus.Size = new Size(140, 22);
            rtbNodeStatus.TabIndex = 13;
            rtbNodeStatus.Text = "";
            // 
            // bExportLog
            // 
            bExportLog.Location = new Point(498, 449);
            bExportLog.Name = "bExportLog";
            bExportLog.Size = new Size(75, 23);
            bExportLog.TabIndex = 12;
            bExportLog.Text = "Export Log";
            bExportLog.UseVisualStyleBackColor = true;
            bExportLog.Click += bExportLog_Click;
            // 
            // lNodeLoc
            // 
            lNodeLoc.AutoSize = true;
            lNodeLoc.Location = new Point(209, 23);
            lNodeLoc.Name = "lNodeLoc";
            lNodeLoc.Size = new Size(56, 15);
            lNodeLoc.TabIndex = 10;
            lNodeLoc.Text = "Location:";
            // 
            // tNodeLocation
            // 
            tNodeLocation.BackColor = SystemColors.Control;
            tNodeLocation.Location = new Point(271, 19);
            tNodeLocation.Name = "tNodeLocation";
            tNodeLocation.ReadOnly = true;
            tNodeLocation.Size = new Size(196, 23);
            tNodeLocation.TabIndex = 11;
            // 
            // fpAvgQueryTime
            // 
            fpAvgQueryTime.BackColor = SystemColors.Control;
            fpAvgQueryTime.Location = new Point(3, 524);
            fpAvgQueryTime.Name = "fpAvgQueryTime";
            fpAvgQueryTime.Size = new Size(1109, 182);
            fpAvgQueryTime.TabIndex = 0;
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvUsers.Location = new Point(616, 145);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.Size = new Size(593, 163);
            dgvUsers.TabIndex = 15;
            dgvUsers.UserDeletedRow += dgvUsers_UserDeletedRow;
            // 
            // dgvSessions
            // 
            dgvSessions.AllowUserToAddRows = false;
            dgvSessions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSessions.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvSessions.Location = new Point(616, 355);
            dgvSessions.Name = "dgvSessions";
            dgvSessions.Size = new Size(593, 163);
            dgvSessions.TabIndex = 16;
            // 
            // lSessions
            // 
            lSessions.AutoSize = true;
            lSessions.Location = new Point(616, 337);
            lSessions.Name = "lSessions";
            lSessions.Size = new Size(54, 15);
            lSessions.TabIndex = 17;
            lSessions.Text = "Sessions:";
            // 
            // lUsers
            // 
            lUsers.AutoSize = true;
            lUsers.Location = new Point(616, 127);
            lUsers.Name = "lUsers";
            lUsers.Size = new Size(38, 15);
            lUsers.TabIndex = 18;
            lUsers.Text = "Users:";
            // 
            // bSaveUsers
            // 
            bSaveUsers.Location = new Point(1163, 314);
            bSaveUsers.Name = "bSaveUsers";
            bSaveUsers.Size = new Size(46, 23);
            bSaveUsers.TabIndex = 19;
            bSaveUsers.Text = "Save";
            bSaveUsers.UseVisualStyleBackColor = true;
            bSaveUsers.Click += bSaveUsers_Click;
            // 
            // bSaveSessions
            // 
            bSaveSessions.Location = new Point(1163, 524);
            bSaveSessions.Name = "bSaveSessions";
            bSaveSessions.Size = new Size(46, 23);
            bSaveSessions.TabIndex = 20;
            bSaveSessions.Text = "Save";
            bSaveSessions.UseVisualStyleBackColor = true;
            bSaveSessions.Click += bSaveSessions_Click;
            // 
            // lServerPerformance
            // 
            lServerPerformance.AutoSize = true;
            lServerPerformance.Location = new Point(42, 506);
            lServerPerformance.Name = "lServerPerformance";
            lServerPerformance.Size = new Size(60, 15);
            lServerPerformance.TabIndex = 21;
            lServerPerformance.Text = "API Times";
            // 
            // bClose
            // 
            bClose.Location = new Point(1118, 671);
            bClose.Name = "bClose";
            bClose.Size = new Size(106, 23);
            bClose.TabIndex = 24;
            bClose.Text = "Close Program";
            bClose.UseVisualStyleBackColor = true;
            bClose.Click += bClose_Click;
            // 
            // sfdSaveLog
            // 
            sfdSaveLog.DefaultExt = "log";
            // 
            // KubKarAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1234, 706);
            Controls.Add(fpAvgQueryTime);
            Controls.Add(bClose);
            Controls.Add(lServerPerformance);
            Controls.Add(bSaveSessions);
            Controls.Add(bSaveUsers);
            Controls.Add(lUsers);
            Controls.Add(lSessions);
            Controls.Add(dgvSessions);
            Controls.Add(dgvUsers);
            Controls.Add(groupBox2);
            Controls.Add(gbDatabase);
            MaximizeBox = false;
            Name = "KubKarAdmin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kub Kars Admin";
            FormClosing += KubKarAdmin_FormClosing;
            Load += KubKarAdmin_Load;
            gbDatabase.ResumeLayout(false);
            gbDatabase.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExistingPids).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSessions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbNodeJSLog;
        private Label lNodeStatus;
        private Button bStopNode;
        private System.Windows.Forms.Timer timerDGVRefreshTimer;
        private Label lNodeLog;
        private TextBox tSvcStatus;
        private Label lSQLStatus;
        private Button bStartDB;
        private GroupBox gbDatabase;
        private Button bRestart;
        private Button bStartNode;
        private Button bRestartDB;
        private Button bStopDB;
        private Button bConfigure;
        private GroupBox groupBox2;
        private Label lNodeLoc;
        private TextBox tNodeLocation;
        private ScottPlot.WinForms.FormsPlot fpAvgQueryTime;
        private DataGridView dgvSessions;
        private DataGridView dgvUsers;
        private Label lSessions;
        private Label lUsers;
        private Label lServerPerformance;
        private Button bSaveSessions;
        private Button bSaveUsers;
        private Button bClose;
        private Button bExportLog;
        private RichTextBox rtbNodeStatus;
        private Label label1;
        private TextBox tSvcName;
        private Button bEditSvcNm;
        private Button bRefreshSvc;
        private Button bClearNodeLog;
        private SaveFileDialog sfdSaveLog;
        private DataGridView dgvExistingPids;
    }
}
