using System;
using System.Diagnostics;
namespace WsClient
{
    partial class UcArchive
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
            try
            {
                for (int i = 0; i < m_initialThread; i++)
                {
                    if (m_thdList[i] != null && m_thdList[i].IsAlive)
                        KillArchiveThread(ref m_thdList[i]);
                }//end of for
            }//end of try
            catch (Exception ex)
            {
                commObj.LogToFile("UcArchive.cs - btnAbort_Click " + ex.Message);
            }//end of catch

            if (disposing && (components != null))
            {
                Trace.WriteLine("UcArchive.Designer.cs - components deposing");
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ttpArchive = new System.Windows.Forms.ToolTip( this.components );
            this.nudThread = new System.Windows.Forms.NumericUpDown();
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.btnAbort = new System.Windows.Forms.Button();
            this.lnkFolder = new System.Windows.Forms.LinkLabel();
            this.chkZAPIPerf = new System.Windows.Forms.CheckBox();
            this.nudCycle = new System.Windows.Forms.NumericUpDown();
            this.cboMailFrom = new System.Windows.Forms.ComboBox();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.cboDomainName = new System.Windows.Forms.ComboBox();
            this.cboRcptTo = new System.Windows.Forms.ComboBox();
            this.txtNumMail = new System.Windows.Forms.TextBox();
            this.txtMailsSize = new System.Windows.Forms.TextBox();
            this.txtAveSize = new System.Windows.Forms.TextBox();
            this.txtMailPerSec = new System.Windows.Forms.TextBox();
            this.lnkViewArchiveLog = new System.Windows.Forms.LinkLabel();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.chkDoUpdate = new System.Windows.Forms.CheckBox();
            this.cboMdeFile = new System.Windows.Forms.ComboBox();
            this.lnkMde = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblMailPerSec = new System.Windows.Forms.Label();
            this.lblAveSize = new System.Windows.Forms.Label();
            this.lblTotalMailsSize = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblSentMail = new System.Windows.Forms.Label();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblThread = new System.Windows.Forms.Label();
            this.lblDelay = new System.Windows.Forms.Label();
            this.lblLoop = new System.Windows.Forms.Label();
            this.btnArchive = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpArchive = new System.Windows.Forms.DateTimePicker();
            this.lblRcptTo = new System.Windows.Forms.Label();
            this.lblMailFrom = new System.Windows.Forms.Label();
            this.lblDomainName = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCycle)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudThread
            // 
            this.nudThread.Location = new System.Drawing.Point( 63, 34 );
            this.nudThread.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.nudThread.Name = "nudThread";
            this.nudThread.Size = new System.Drawing.Size( 60, 20 );
            this.nudThread.TabIndex = 36;
            this.nudThread.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ttpArchive.SetToolTip( this.nudThread, "Max. 100 Threads" );
            this.nudThread.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            // 
            // nudDelay
            // 
            this.nudDelay.Location = new System.Drawing.Point( 63, 55 );
            this.nudDelay.Maximum = new decimal( new int[] {
            5,
            0,
            0,
            0} );
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size( 60, 20 );
            this.nudDelay.TabIndex = 35;
            this.nudDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ttpArchive.SetToolTip( this.nudDelay, "Delay in Second (Max. 5 sec)" );
            this.nudDelay.ValueChanged += new System.EventHandler( this.nudDelay_ValueChanged );
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point( 469, 149 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 64, 20 );
            this.btnAbort.TabIndex = 43;
            this.btnAbort.Text = "Abort";
            this.ttpArchive.SetToolTip( this.btnAbort, "Abort archive" );
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // lnkFolder
            // 
            this.lnkFolder.Location = new System.Drawing.Point( 6, 11 );
            this.lnkFolder.Name = "lnkFolder";
            this.lnkFolder.Size = new System.Drawing.Size( 48, 16 );
            this.lnkFolder.TabIndex = 35;
            this.lnkFolder.TabStop = true;
            this.lnkFolder.Text = "Folder";
            this.lnkFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpArchive.SetToolTip( this.lnkFolder, "Browse the folder that contain data" );
            this.lnkFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkFolder_LinkClicked );
            // 
            // chkZAPIPerf
            // 
            this.chkZAPIPerf.AutoSize = true;
            this.chkZAPIPerf.Location = new System.Drawing.Point( 13, 122 );
            this.chkZAPIPerf.Name = "chkZAPIPerf";
            this.chkZAPIPerf.Size = new System.Drawing.Size( 108, 17 );
            this.chkZAPIPerf.TabIndex = 45;
            this.chkZAPIPerf.Text = "ZAPI Timing Test";
            this.ttpArchive.SetToolTip( this.chkZAPIPerf, "Time the archive API call only" );
            this.chkZAPIPerf.CheckedChanged += new System.EventHandler( this.chkZAPIPerf_CheckedChanged );
            // 
            // nudCycle
            // 
            this.nudCycle.Location = new System.Drawing.Point( 63, 12 );
            this.nudCycle.Maximum = new decimal( new int[] {
            9999,
            0,
            0,
            0} );
            this.nudCycle.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.nudCycle.Name = "nudCycle";
            this.nudCycle.Size = new System.Drawing.Size( 60, 20 );
            this.nudCycle.TabIndex = 34;
            this.nudCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ttpArchive.SetToolTip( this.nudCycle, "Repeat archive" );
            this.nudCycle.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            // 
            // cboMailFrom
            // 
            this.cboMailFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMailFrom.FormattingEnabled = true;
            this.cboMailFrom.Items.AddRange( new object[] {
            "abc@abc.zantaz.com",
            "xyz@xyz.zantaz.com"} );
            this.cboMailFrom.Location = new System.Drawing.Point( 58, 77 );
            this.cboMailFrom.Name = "cboMailFrom";
            this.cboMailFrom.Size = new System.Drawing.Size( 186, 21 );
            this.cboMailFrom.TabIndex = 28;
            this.ttpArchive.SetToolTip( this.cboMailFrom, "TO: add input validation" );
            this.cboMailFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboMailFrom_KeyPress );
            this.cboMailFrom.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboMailFrom_KeyDown );
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point( 58, 10 );
            this.txtFolder.MaxLength = 65536;
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size( 186, 20 );
            this.txtFolder.TabIndex = 26;
            this.ttpArchive.SetToolTip( this.txtFolder, "Point to the folder that contain data" );
            // 
            // cboDomainName
            // 
            this.cboDomainName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDomainName.FormattingEnabled = true;
            this.cboDomainName.Items.AddRange( new object[] {
            "zdemo",
            "testdomain1"} );
            this.cboDomainName.Location = new System.Drawing.Point( 58, 54 );
            this.cboDomainName.Name = "cboDomainName";
            this.cboDomainName.Size = new System.Drawing.Size( 186, 21 );
            this.cboDomainName.TabIndex = 25;
            this.ttpArchive.SetToolTip( this.cboDomainName, "Domain Name" );
            this.cboDomainName.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboDomainName_KeyPress );
            this.cboDomainName.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboDomainName_KeyDown );
            // 
            // cboRcptTo
            // 
            this.cboRcptTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRcptTo.FormattingEnabled = true;
            this.cboRcptTo.Location = new System.Drawing.Point( 58, 100 );
            this.cboRcptTo.Name = "cboRcptTo";
            this.cboRcptTo.Size = new System.Drawing.Size( 186, 21 );
            this.cboRcptTo.TabIndex = 29;
            this.ttpArchive.SetToolTip( this.cboRcptTo, "TO: add input validation" );
            this.cboRcptTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboRcptTo_KeyPress );
            this.cboRcptTo.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboRcptTo_KeyDown );
            // 
            // txtNumMail
            // 
            this.txtNumMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumMail.Location = new System.Drawing.Point( 184, 77 );
            this.txtNumMail.Name = "txtNumMail";
            this.txtNumMail.ReadOnly = true;
            this.txtNumMail.Size = new System.Drawing.Size( 94, 20 );
            this.txtNumMail.TabIndex = 42;
            this.ttpArchive.SetToolTip( this.txtNumMail, "Total Sent Files" );
            // 
            // txtMailsSize
            // 
            this.txtMailsSize.Location = new System.Drawing.Point( 63, 77 );
            this.txtMailsSize.Name = "txtMailsSize";
            this.txtMailsSize.ReadOnly = true;
            this.txtMailsSize.Size = new System.Drawing.Size( 60, 20 );
            this.txtMailsSize.TabIndex = 47;
            this.ttpArchive.SetToolTip( this.txtMailsSize, "Total Mails Size" );
            // 
            // txtAveSize
            // 
            this.txtAveSize.Location = new System.Drawing.Point( 63, 99 );
            this.txtAveSize.Name = "txtAveSize";
            this.txtAveSize.ReadOnly = true;
            this.txtAveSize.Size = new System.Drawing.Size( 60, 20 );
            this.txtAveSize.TabIndex = 49;
            this.ttpArchive.SetToolTip( this.txtAveSize, "Ave. Size/Mail" );
            // 
            // txtMailPerSec
            // 
            this.txtMailPerSec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMailPerSec.Location = new System.Drawing.Point( 184, 99 );
            this.txtMailPerSec.Name = "txtMailPerSec";
            this.txtMailPerSec.ReadOnly = true;
            this.txtMailPerSec.Size = new System.Drawing.Size( 94, 20 );
            this.txtMailPerSec.TabIndex = 51;
            this.ttpArchive.SetToolTip( this.txtMailPerSec, "Number of Mail sent per sec" );
            // 
            // lnkViewArchiveLog
            // 
            this.lnkViewArchiveLog.Location = new System.Drawing.Point( 180, 121 );
            this.lnkViewArchiveLog.Name = "lnkViewArchiveLog";
            this.lnkViewArchiveLog.Size = new System.Drawing.Size( 68, 16 );
            this.lnkViewArchiveLog.TabIndex = 36;
            this.lnkViewArchiveLog.TabStop = true;
            this.lnkViewArchiveLog.Text = "Archive Log";
            this.lnkViewArchiveLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpArchive.SetToolTip( this.lnkViewArchiveLog, "View the Archive Log" );
            this.lnkViewArchiveLog.Click += new System.EventHandler( this.lnkViewArchiveLog_Click );
            // 
            // txtStartTime
            // 
            this.txtStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartTime.Location = new System.Drawing.Point( 184, 12 );
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.ReadOnly = true;
            this.txtStartTime.Size = new System.Drawing.Size( 94, 20 );
            this.txtStartTime.TabIndex = 38;
            this.ttpArchive.SetToolTip( this.txtStartTime, "Test" );
            this.txtStartTime.MouseEnter += new System.EventHandler( this.txtStartTime_MouseEnter );
            // 
            // chkDoUpdate
            // 
            this.chkDoUpdate.AutoSize = true;
            this.chkDoUpdate.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.chkDoUpdate.Checked = true;
            this.chkDoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoUpdate.Location = new System.Drawing.Point( 40, 124 );
            this.chkDoUpdate.Name = "chkDoUpdate";
            this.chkDoUpdate.Size = new System.Drawing.Size( 15, 14 );
            this.chkDoUpdate.TabIndex = 52;
            this.chkDoUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpArchive.SetToolTip( this.chkDoUpdate, "Check for med update after calling archive api" );
            this.chkDoUpdate.CheckedChanged += new System.EventHandler( this.chkDoUpdate_CheckedChanged );
            // 
            // cboMdeFile
            // 
            this.cboMdeFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMdeFile.FormattingEnabled = true;
            this.cboMdeFile.Location = new System.Drawing.Point( 58, 122 );
            this.cboMdeFile.Name = "cboMdeFile";
            this.cboMdeFile.Size = new System.Drawing.Size( 186, 21 );
            this.cboMdeFile.TabIndex = 53;
            this.ttpArchive.SetToolTip( this.cboMdeFile, "MDE File" );
            this.cboMdeFile.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboMdeFile_KeyPress );
            this.cboMdeFile.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboMdeFile_KeyDown );
            // 
            // lnkMde
            // 
            this.lnkMde.Location = new System.Drawing.Point( 6, 124 );
            this.lnkMde.Name = "lnkMde";
            this.lnkMde.Size = new System.Drawing.Size( 33, 16 );
            this.lnkMde.TabIndex = 54;
            this.lnkMde.TabStop = true;
            this.lnkMde.Text = "MDE";
            this.lnkMde.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpArchive.SetToolTip( this.lnkMde, "Select the MDE file" );
            this.lnkMde.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkMde_LinkClicked );
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add( this.lnkViewArchiveLog );
            this.groupBox2.Controls.Add( this.txtMailPerSec );
            this.groupBox2.Controls.Add( this.lblMailPerSec );
            this.groupBox2.Controls.Add( this.txtAveSize );
            this.groupBox2.Controls.Add( this.lblAveSize );
            this.groupBox2.Controls.Add( this.chkZAPIPerf );
            this.groupBox2.Controls.Add( this.txtMailsSize );
            this.groupBox2.Controls.Add( this.lblTotalMailsSize );
            this.groupBox2.Controls.Add( this.txtDuration );
            this.groupBox2.Controls.Add( this.lblDuration );
            this.groupBox2.Controls.Add( this.txtNumMail );
            this.groupBox2.Controls.Add( this.lblSentMail );
            this.groupBox2.Controls.Add( this.txtEndTime );
            this.groupBox2.Controls.Add( this.lblEndTime );
            this.groupBox2.Controls.Add( this.txtStartTime );
            this.groupBox2.Controls.Add( this.lblStartTime );
            this.groupBox2.Controls.Add( this.nudThread );
            this.groupBox2.Controls.Add( this.nudDelay );
            this.groupBox2.Controls.Add( this.nudCycle );
            this.groupBox2.Controls.Add( this.lblThread );
            this.groupBox2.Controls.Add( this.lblDelay );
            this.groupBox2.Controls.Add( this.lblLoop );
            this.groupBox2.Location = new System.Drawing.Point( 255, -3 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size( 286, 147 );
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            // 
            // lblMailPerSec
            // 
            this.lblMailPerSec.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMailPerSec.Location = new System.Drawing.Point( 124, 99 );
            this.lblMailPerSec.Name = "lblMailPerSec";
            this.lblMailPerSec.Size = new System.Drawing.Size( 57, 17 );
            this.lblMailPerSec.TabIndex = 50;
            this.lblMailPerSec.Text = "Mails/sec";
            this.lblMailPerSec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAveSize
            // 
            this.lblAveSize.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblAveSize.Location = new System.Drawing.Point( 4, 100 );
            this.lblAveSize.Name = "lblAveSize";
            this.lblAveSize.Size = new System.Drawing.Size( 56, 17 );
            this.lblAveSize.TabIndex = 48;
            this.lblAveSize.Text = "Ave. Size";
            this.lblAveSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalMailsSize
            // 
            this.lblTotalMailsSize.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTotalMailsSize.Location = new System.Drawing.Point( 4, 79 );
            this.lblTotalMailsSize.Name = "lblTotalMailsSize";
            this.lblTotalMailsSize.Size = new System.Drawing.Size( 56, 17 );
            this.lblTotalMailsSize.TabIndex = 46;
            this.lblTotalMailsSize.Text = "Mails Size";
            this.lblTotalMailsSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDuration
            // 
            this.txtDuration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDuration.Location = new System.Drawing.Point( 184, 55 );
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.ReadOnly = true;
            this.txtDuration.Size = new System.Drawing.Size( 94, 20 );
            this.txtDuration.TabIndex = 44;
            // 
            // lblDuration
            // 
            this.lblDuration.Location = new System.Drawing.Point( 124, 58 );
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size( 57, 17 );
            this.lblDuration.TabIndex = 43;
            this.lblDuration.Text = "Duration";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSentMail
            // 
            this.lblSentMail.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblSentMail.Location = new System.Drawing.Point( 124, 78 );
            this.lblSentMail.Name = "lblSentMail";
            this.lblSentMail.Size = new System.Drawing.Size( 57, 17 );
            this.lblSentMail.TabIndex = 41;
            this.lblSentMail.Text = "Sent Mails";
            this.lblSentMail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndTime
            // 
            this.txtEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndTime.Location = new System.Drawing.Point( 184, 34 );
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.ReadOnly = true;
            this.txtEndTime.Size = new System.Drawing.Size( 94, 20 );
            this.txtEndTime.TabIndex = 40;
            this.txtEndTime.MouseEnter += new System.EventHandler( this.txtEndTime_MouseEnter );
            // 
            // lblEndTime
            // 
            this.lblEndTime.Location = new System.Drawing.Point( 124, 36 );
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size( 57, 17 );
            this.lblEndTime.TabIndex = 39;
            this.lblEndTime.Text = "End Time";
            this.lblEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStartTime
            // 
            this.lblStartTime.Location = new System.Drawing.Point( 124, 15 );
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size( 57, 17 );
            this.lblStartTime.TabIndex = 37;
            this.lblStartTime.Text = "Start Time";
            this.lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblThread
            // 
            this.lblThread.Location = new System.Drawing.Point( 4, 37 );
            this.lblThread.Name = "lblThread";
            this.lblThread.Size = new System.Drawing.Size( 55, 17 );
            this.lblThread.TabIndex = 33;
            this.lblThread.Text = "Thread";
            this.lblThread.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDelay
            // 
            this.lblDelay.Location = new System.Drawing.Point( 4, 56 );
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size( 55, 17 );
            this.lblDelay.TabIndex = 32;
            this.lblDelay.Text = "Delay";
            this.lblDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLoop
            // 
            this.lblLoop.Location = new System.Drawing.Point( 4, 16 );
            this.lblLoop.Name = "lblLoop";
            this.lblLoop.Size = new System.Drawing.Size( 55, 17 );
            this.lblLoop.TabIndex = 31;
            this.lblLoop.Text = "Cycle";
            this.lblLoop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnArchive
            // 
            this.btnArchive.Location = new System.Drawing.Point( 399, 149 );
            this.btnArchive.Name = "btnArchive";
            this.btnArchive.Size = new System.Drawing.Size( 64, 20 );
            this.btnArchive.TabIndex = 30;
            this.btnArchive.Text = "Archive";
            this.btnArchive.Click += new System.EventHandler( this.btnArchive_Click );
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.lnkMde );
            this.groupBox1.Controls.Add( this.cboMdeFile );
            this.groupBox1.Controls.Add( this.chkDoUpdate );
            this.groupBox1.Controls.Add( this.cboMailFrom );
            this.groupBox1.Controls.Add( this.dtpArchive );
            this.groupBox1.Controls.Add( this.txtFolder );
            this.groupBox1.Controls.Add( this.cboDomainName );
            this.groupBox1.Controls.Add( this.lnkFolder );
            this.groupBox1.Controls.Add( this.lblRcptTo );
            this.groupBox1.Controls.Add( this.lblMailFrom );
            this.groupBox1.Controls.Add( this.lblDomainName );
            this.groupBox1.Controls.Add( this.lblDate );
            this.groupBox1.Controls.Add( this.cboRcptTo );
            this.groupBox1.Location = new System.Drawing.Point( 2, -3 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 251, 147 );
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            // 
            // dtpArchive
            // 
            this.dtpArchive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpArchive.CustomFormat = "ddd, dd MMM yyyy HH\':\'mm\':\'ss \'PST\'";
            this.dtpArchive.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpArchive.Location = new System.Drawing.Point( 58, 32 );
            this.dtpArchive.Name = "dtpArchive";
            this.dtpArchive.Size = new System.Drawing.Size( 186, 20 );
            this.dtpArchive.TabIndex = 27;
            // 
            // lblRcptTo
            // 
            this.lblRcptTo.Location = new System.Drawing.Point( 6, 101 );
            this.lblRcptTo.Name = "lblRcptTo";
            this.lblRcptTo.Size = new System.Drawing.Size( 48, 16 );
            this.lblRcptTo.TabIndex = 34;
            this.lblRcptTo.Text = "To";
            this.lblRcptTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMailFrom
            // 
            this.lblMailFrom.Location = new System.Drawing.Point( 6, 79 );
            this.lblMailFrom.Name = "lblMailFrom";
            this.lblMailFrom.Size = new System.Drawing.Size( 48, 16 );
            this.lblMailFrom.TabIndex = 33;
            this.lblMailFrom.Text = "From";
            this.lblMailFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDomainName
            // 
            this.lblDomainName.Location = new System.Drawing.Point( 6, 56 );
            this.lblDomainName.Name = "lblDomainName";
            this.lblDomainName.Size = new System.Drawing.Size( 48, 16 );
            this.lblDomainName.TabIndex = 32;
            this.lblDomainName.Text = "Domain";
            this.lblDomainName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point( 6, 33 );
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size( 48, 16 );
            this.lblDate.TabIndex = 31;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UcArchive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.groupBox1 );
            this.Controls.Add( this.btnAbort );
            this.Controls.Add( this.groupBox2 );
            this.Controls.Add( this.btnArchive );
            this.Name = "UcArchive";
            this.Size = new System.Drawing.Size( 544, 336 );
            this.Load += new System.EventHandler( this.UcArchive_Load );
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCycle)).EndInit();
            this.groupBox2.ResumeLayout( false );
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ToolTip ttpArchive;
        private System.Windows.Forms.TextBox txtNumMail;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblSentMail;
        private System.Windows.Forms.TextBox txtEndTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.NumericUpDown nudThread;
        private System.Windows.Forms.NumericUpDown nudDelay;
        private System.Windows.Forms.NumericUpDown nudCycle;
        private System.Windows.Forms.Label lblThread;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.Label lblLoop;
        private System.Windows.Forms.Button btnArchive;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboMailFrom;
        private System.Windows.Forms.DateTimePicker dtpArchive;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.ComboBox cboDomainName;
        private System.Windows.Forms.LinkLabel lnkFolder;
        private System.Windows.Forms.Label lblRcptTo;
        private System.Windows.Forms.Label lblMailFrom;
        private System.Windows.Forms.Label lblDomainName;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ComboBox cboRcptTo;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.CheckBox chkZAPIPerf;
        private System.Windows.Forms.Label lblTotalMailsSize;
        private System.Windows.Forms.TextBox txtMailsSize;
        private System.Windows.Forms.Label lblAveSize;
        private System.Windows.Forms.TextBox txtAveSize;
        private System.Windows.Forms.Label lblMailPerSec;
        private System.Windows.Forms.TextBox txtMailPerSec;
        private System.Windows.Forms.LinkLabel lnkViewArchiveLog;
        private System.Windows.Forms.CheckBox chkDoUpdate;
        private System.Windows.Forms.ComboBox cboMdeFile;
        private System.Windows.Forms.LinkLabel lnkMde;
    }
}
