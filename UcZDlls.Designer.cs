using System;
using System.Diagnostics;

namespace WsClient
{
    partial class UcZDlls
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
                        Kill_Dll_Thread(ref m_thdList[i]);
                }//end of for
            }//end of try
            catch (Exception ex)
            {
                commObj.LogToFile("UcZDlls.cs - btnAbort_Click " + ex.Message);
            }//end of catch

            if (disposing && (components != null))
            {
                Trace.WriteLine("UcZDlls.Designer.cs - components deposing");
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
            this.groupArchive = new System.Windows.Forms.GroupBox();
            this.cboMdeFile = new System.Windows.Forms.ComboBox();
            this.chkDoUpdate = new System.Windows.Forms.CheckBox();
            this.rdoDllArchive = new System.Windows.Forms.RadioButton();
            this.cboMailFrom = new System.Windows.Forms.ComboBox();
            this.dtpArchive = new System.Windows.Forms.DateTimePicker();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.cboArchDomain = new System.Windows.Forms.ComboBox();
            this.lnkFolder = new System.Windows.Forms.LinkLabel();
            this.lblRcptTo = new System.Windows.Forms.Label();
            this.lblMailFrom = new System.Windows.Forms.Label();
            this.lblArchDomain = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.cboRcptTo = new System.Windows.Forms.ComboBox();
            this.groupRetrieve = new System.Windows.Forms.GroupBox();
            this.rdoDllRetrieve = new System.Windows.Forms.RadioButton();
            this.cboRetrDomain = new System.Windows.Forms.ComboBox();
            this.lblRetrDomain = new System.Windows.Forms.Label();
            this.txtStoreFolder = new System.Windows.Forms.TextBox();
            this.txtKeyFile = new System.Windows.Forms.TextBox();
            this.lnkStoreLocation = new System.Windows.Forms.LinkLabel();
            this.lnkBrowseKeyFile = new System.Windows.Forms.LinkLabel();
            this.txtFailCall = new System.Windows.Forms.TextBox();
            this.lblFailCall = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lnkViewDllTestLog = new System.Windows.Forms.LinkLabel();
            this.txtAveSize = new System.Windows.Forms.TextBox();
            this.lblAveSize = new System.Windows.Forms.Label();
            this.chkDllTiming = new System.Windows.Forms.CheckBox();
            this.txtMailsSize = new System.Windows.Forms.TextBox();
            this.lblTotalMailsSize = new System.Windows.Forms.Label();
            this.txtMailPerSec = new System.Windows.Forms.TextBox();
            this.lblMailPerSec = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.txtNumMail = new System.Windows.Forms.TextBox();
            this.lblMailStore = new System.Windows.Forms.Label();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.nudThread = new System.Windows.Forms.NumericUpDown();
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.lblThread = new System.Windows.Forms.Label();
            this.lblDelay = new System.Windows.Forms.Label();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.ttpZDlls = new System.Windows.Forms.ToolTip( this.components );
            this.lnkUpdateKeyFile = new System.Windows.Forms.LinkLabel();
            this.lnkMdeFolder = new System.Windows.Forms.LinkLabel();
            this.cboMdeFolder = new System.Windows.Forms.ComboBox();
            this.btnGenFile = new System.Windows.Forms.Button();
            this.groupUpdate = new System.Windows.Forms.GroupBox();
            this.lblUpdateRcptTo = new System.Windows.Forms.Label();
            this.cboUpdateRcptTo = new System.Windows.Forms.ComboBox();
            this.cboUpdateDomain = new System.Windows.Forms.ComboBox();
            this.lblUpdateDomain = new System.Windows.Forms.Label();
            this.rdoDllUpdate = new System.Windows.Forms.RadioButton();
            this.txtUpdateKeyFile = new System.Windows.Forms.TextBox();
            this.groupArchive.SuspendLayout();
            this.groupRetrieve.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            this.groupUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupArchive
            // 
            this.groupArchive.Controls.Add( this.cboMdeFile );
            this.groupArchive.Controls.Add( this.chkDoUpdate );
            this.groupArchive.Controls.Add( this.rdoDllArchive );
            this.groupArchive.Controls.Add( this.cboMailFrom );
            this.groupArchive.Controls.Add( this.dtpArchive );
            this.groupArchive.Controls.Add( this.txtFolder );
            this.groupArchive.Controls.Add( this.cboArchDomain );
            this.groupArchive.Controls.Add( this.lnkFolder );
            this.groupArchive.Controls.Add( this.lblRcptTo );
            this.groupArchive.Controls.Add( this.lblMailFrom );
            this.groupArchive.Controls.Add( this.lblArchDomain );
            this.groupArchive.Controls.Add( this.lblDate );
            this.groupArchive.Controls.Add( this.cboRcptTo );
            this.groupArchive.Enabled = false;
            this.groupArchive.Location = new System.Drawing.Point( 3, 5 );
            this.groupArchive.Name = "groupArchive";
            this.groupArchive.Size = new System.Drawing.Size( 251, 150 );
            this.groupArchive.TabIndex = 39;
            this.groupArchive.TabStop = false;
            // 
            // cboMdeFile
            // 
            this.cboMdeFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMdeFile.FormattingEnabled = true;
            this.cboMdeFile.Location = new System.Drawing.Point( 56, 125 );
            this.cboMdeFile.Name = "cboMdeFile";
            this.cboMdeFile.Size = new System.Drawing.Size( 187, 21 );
            this.cboMdeFile.TabIndex = 55;
            // 
            // chkDoUpdate
            // 
            this.chkDoUpdate.AutoSize = true;
            this.chkDoUpdate.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.chkDoUpdate.Checked = true;
            this.chkDoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoUpdate.Location = new System.Drawing.Point( 3, 128 );
            this.chkDoUpdate.Name = "chkDoUpdate";
            this.chkDoUpdate.Size = new System.Drawing.Size( 50, 17 );
            this.chkDoUpdate.TabIndex = 54;
            this.chkDoUpdate.Text = "MDE";
            this.chkDoUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDoUpdate.CheckedChanged += new System.EventHandler( this.chkDoUpdate_CheckedChanged );
            // 
            // rdoDllArchive
            // 
            this.rdoDllArchive.AutoSize = true;
            this.rdoDllArchive.Checked = true;
            this.rdoDllArchive.Location = new System.Drawing.Point( 7, -2 );
            this.rdoDllArchive.Name = "rdoDllArchive";
            this.rdoDllArchive.Size = new System.Drawing.Size( 61, 17 );
            this.rdoDllArchive.TabIndex = 36;
            this.rdoDllArchive.TabStop = true;
            this.rdoDllArchive.Text = "Archive";
            this.ttpZDlls.SetToolTip( this.rdoDllArchive, "Perform Archive via DLL" );
            this.rdoDllArchive.UseVisualStyleBackColor = false;
            this.rdoDllArchive.Click += new System.EventHandler( this.rdoDllArchive_Click );
            // 
            // cboMailFrom
            // 
            this.cboMailFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMailFrom.FormattingEnabled = true;
            this.cboMailFrom.Items.AddRange( new object[] {
            "abc@abc.zantaz.com",
            "xyz@xyz.zantaz.com"} );
            this.cboMailFrom.Location = new System.Drawing.Point( 56, 81 );
            this.cboMailFrom.Name = "cboMailFrom";
            this.cboMailFrom.Size = new System.Drawing.Size( 188, 21 );
            this.cboMailFrom.TabIndex = 28;
            this.cboMailFrom.Text = "ceo@testdomain2.digitalsafe.net";
            this.cboMailFrom.MouseEnter += new System.EventHandler( this.cboMailFrom_MouseEnter );
            // 
            // dtpArchive
            // 
            this.dtpArchive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpArchive.Location = new System.Drawing.Point( 56, 37 );
            this.dtpArchive.Name = "dtpArchive";
            this.dtpArchive.Size = new System.Drawing.Size( 188, 20 );
            this.dtpArchive.TabIndex = 27;
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point( 56, 15 );
            this.txtFolder.MaxLength = 65536;
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size( 188, 20 );
            this.txtFolder.TabIndex = 26;
            this.txtFolder.Text = "D:\\wsMails\\emlMail";
            this.ttpZDlls.SetToolTip( this.txtFolder, "Location of data for archiving." );
            this.txtFolder.MouseEnter += new System.EventHandler( this.txtFolder_MouseEnter );
            // 
            // cboArchDomain
            // 
            this.cboArchDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboArchDomain.FormattingEnabled = true;
            this.cboArchDomain.Items.AddRange( new object[] {
            "testdomain1",
            "testdomain2",
            "zdemo"} );
            this.cboArchDomain.Location = new System.Drawing.Point( 56, 59 );
            this.cboArchDomain.Name = "cboArchDomain";
            this.cboArchDomain.Size = new System.Drawing.Size( 188, 21 );
            this.cboArchDomain.TabIndex = 25;
            this.cboArchDomain.Text = "testdomain2";
            this.cboArchDomain.MouseEnter += new System.EventHandler( this.cboArchDomain_MouseEnter );
            // 
            // lnkFolder
            // 
            this.lnkFolder.Location = new System.Drawing.Point( 5, 18 );
            this.lnkFolder.Name = "lnkFolder";
            this.lnkFolder.Size = new System.Drawing.Size( 48, 16 );
            this.lnkFolder.TabIndex = 35;
            this.lnkFolder.TabStop = true;
            this.lnkFolder.Text = "Folder";
            this.lnkFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpZDlls.SetToolTip( this.lnkFolder, "Point to data folder (MSG)" );
            this.lnkFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkFolder_LinkClicked );
            // 
            // lblRcptTo
            // 
            this.lblRcptTo.Location = new System.Drawing.Point( 5, 106 );
            this.lblRcptTo.Name = "lblRcptTo";
            this.lblRcptTo.Size = new System.Drawing.Size( 48, 16 );
            this.lblRcptTo.TabIndex = 34;
            this.lblRcptTo.Text = "To";
            this.lblRcptTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMailFrom
            // 
            this.lblMailFrom.Location = new System.Drawing.Point( 5, 84 );
            this.lblMailFrom.Name = "lblMailFrom";
            this.lblMailFrom.Size = new System.Drawing.Size( 48, 16 );
            this.lblMailFrom.TabIndex = 33;
            this.lblMailFrom.Text = "From";
            this.lblMailFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblArchDomain
            // 
            this.lblArchDomain.Location = new System.Drawing.Point( 5, 62 );
            this.lblArchDomain.Name = "lblArchDomain";
            this.lblArchDomain.Size = new System.Drawing.Size( 48, 16 );
            this.lblArchDomain.TabIndex = 32;
            this.lblArchDomain.Text = "Domain";
            this.lblArchDomain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point( 5, 40 );
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size( 48, 16 );
            this.lblDate.TabIndex = 31;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboRcptTo
            // 
            this.cboRcptTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRcptTo.FormattingEnabled = true;
            this.cboRcptTo.Location = new System.Drawing.Point( 56, 103 );
            this.cboRcptTo.Name = "cboRcptTo";
            this.cboRcptTo.Size = new System.Drawing.Size( 188, 21 );
            this.cboRcptTo.TabIndex = 29;
            this.cboRcptTo.Text = "admin@testdomain2.digitalsafe.net";
            this.cboRcptTo.MouseEnter += new System.EventHandler( this.cboRcptTo_MouseEnter );
            // 
            // groupRetrieve
            // 
            this.groupRetrieve.Controls.Add( this.rdoDllRetrieve );
            this.groupRetrieve.Controls.Add( this.cboRetrDomain );
            this.groupRetrieve.Controls.Add( this.lblRetrDomain );
            this.groupRetrieve.Controls.Add( this.txtStoreFolder );
            this.groupRetrieve.Controls.Add( this.txtKeyFile );
            this.groupRetrieve.Controls.Add( this.lnkStoreLocation );
            this.groupRetrieve.Controls.Add( this.lnkBrowseKeyFile );
            this.groupRetrieve.Enabled = false;
            this.groupRetrieve.Location = new System.Drawing.Point( 260, 157 );
            this.groupRetrieve.Name = "groupRetrieve";
            this.groupRetrieve.Size = new System.Drawing.Size( 277, 88 );
            this.groupRetrieve.TabIndex = 40;
            this.groupRetrieve.TabStop = false;
            // 
            // rdoDllRetrieve
            // 
            this.rdoDllRetrieve.AutoSize = true;
            this.rdoDllRetrieve.Location = new System.Drawing.Point( 7, -2 );
            this.rdoDllRetrieve.Name = "rdoDllRetrieve";
            this.rdoDllRetrieve.Size = new System.Drawing.Size( 65, 17 );
            this.rdoDllRetrieve.TabIndex = 57;
            this.rdoDllRetrieve.Text = "Retrieve";
            this.ttpZDlls.SetToolTip( this.rdoDllRetrieve, "Perform retrieve test via DLL" );
            this.rdoDllRetrieve.UseVisualStyleBackColor = false;
            this.rdoDllRetrieve.Click += new System.EventHandler( this.rdoDllRetrieve_Click );
            // 
            // cboRetrDomain
            // 
            this.cboRetrDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRetrDomain.Enabled = false;
            this.cboRetrDomain.FormattingEnabled = true;
            this.cboRetrDomain.Items.AddRange( new object[] {
            "testdomain1",
            "testdomain2",
            "zdemo"} );
            this.cboRetrDomain.Location = new System.Drawing.Point( 56, 61 );
            this.cboRetrDomain.Name = "cboRetrDomain";
            this.cboRetrDomain.Size = new System.Drawing.Size( 214, 21 );
            this.cboRetrDomain.TabIndex = 33;
            this.cboRetrDomain.Text = "testdomain2";
            this.cboRetrDomain.MouseEnter += new System.EventHandler( this.cboRetrDomain_MouseEnter );
            // 
            // lblRetrDomain
            // 
            this.lblRetrDomain.Enabled = false;
            this.lblRetrDomain.Location = new System.Drawing.Point( 9, 65 );
            this.lblRetrDomain.Name = "lblRetrDomain";
            this.lblRetrDomain.Size = new System.Drawing.Size( 44, 16 );
            this.lblRetrDomain.TabIndex = 34;
            this.lblRetrDomain.Text = "Domain";
            this.lblRetrDomain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStoreFolder
            // 
            this.txtStoreFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStoreFolder.Enabled = false;
            this.txtStoreFolder.Location = new System.Drawing.Point( 56, 39 );
            this.txtStoreFolder.Name = "txtStoreFolder";
            this.txtStoreFolder.Size = new System.Drawing.Size( 214, 20 );
            this.txtStoreFolder.TabIndex = 3;
            this.txtStoreFolder.Text = "d:\\tmpStore";
            this.txtStoreFolder.MouseEnter += new System.EventHandler( this.txtStoreFolder_MouseEnter );
            // 
            // txtKeyFile
            // 
            this.txtKeyFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyFile.Enabled = false;
            this.txtKeyFile.Location = new System.Drawing.Point( 56, 17 );
            this.txtKeyFile.Name = "txtKeyFile";
            this.txtKeyFile.Size = new System.Drawing.Size( 214, 20 );
            this.txtKeyFile.TabIndex = 2;
            this.txtKeyFile.Text = "d:\\tmp\\zKey.txt";
            this.txtKeyFile.MouseEnter += new System.EventHandler( this.txtKeyFile_MouseEnter );
            // 
            // lnkStoreLocation
            // 
            this.lnkStoreLocation.AutoSize = true;
            this.lnkStoreLocation.Enabled = false;
            this.lnkStoreLocation.Location = new System.Drawing.Point( 17, 42 );
            this.lnkStoreLocation.Name = "lnkStoreLocation";
            this.lnkStoreLocation.Size = new System.Drawing.Size( 36, 13 );
            this.lnkStoreLocation.TabIndex = 1;
            this.lnkStoreLocation.TabStop = true;
            this.lnkStoreLocation.Text = "Folder";
            this.lnkStoreLocation.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ttpZDlls.SetToolTip( this.lnkStoreLocation, "Point to store folder" );
            this.lnkStoreLocation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkStoreLocation_LinkClicked );
            // 
            // lnkBrowseKeyFile
            // 
            this.lnkBrowseKeyFile.AutoSize = true;
            this.lnkBrowseKeyFile.Enabled = false;
            this.lnkBrowseKeyFile.Location = new System.Drawing.Point( 4, 20 );
            this.lnkBrowseKeyFile.Name = "lnkBrowseKeyFile";
            this.lnkBrowseKeyFile.Size = new System.Drawing.Size( 49, 13 );
            this.lnkBrowseKeyFile.TabIndex = 0;
            this.lnkBrowseKeyFile.TabStop = true;
            this.lnkBrowseKeyFile.Text = "zKey File";
            this.lnkBrowseKeyFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpZDlls.SetToolTip( this.lnkBrowseKeyFile, "Point to the ZDK file for retrieving messages." );
            this.lnkBrowseKeyFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkBrowseKeyFile_LinkClicked );
            // 
            // txtFailCall
            // 
            this.txtFailCall.Location = new System.Drawing.Point( 60, 99 );
            this.txtFailCall.Name = "txtFailCall";
            this.txtFailCall.ReadOnly = true;
            this.txtFailCall.Size = new System.Drawing.Size( 66, 20 );
            this.txtFailCall.TabIndex = 64;
            // 
            // lblFailCall
            // 
            this.lblFailCall.Location = new System.Drawing.Point( 10, 100 );
            this.lblFailCall.Name = "lblFailCall";
            this.lblFailCall.Size = new System.Drawing.Size( 48, 16 );
            this.lblFailCall.TabIndex = 35;
            this.lblFailCall.Text = "Fail Call";
            this.lblFailCall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add( this.lnkViewDllTestLog );
            this.groupBox3.Controls.Add( this.txtFailCall );
            this.groupBox3.Controls.Add( this.lblFailCall );
            this.groupBox3.Controls.Add( this.txtAveSize );
            this.groupBox3.Controls.Add( this.lblAveSize );
            this.groupBox3.Controls.Add( this.chkDllTiming );
            this.groupBox3.Controls.Add( this.txtMailsSize );
            this.groupBox3.Controls.Add( this.lblTotalMailsSize );
            this.groupBox3.Controls.Add( this.txtMailPerSec );
            this.groupBox3.Controls.Add( this.lblMailPerSec );
            this.groupBox3.Controls.Add( this.txtDuration );
            this.groupBox3.Controls.Add( this.lblDuration );
            this.groupBox3.Controls.Add( this.txtNumMail );
            this.groupBox3.Controls.Add( this.lblMailStore );
            this.groupBox3.Controls.Add( this.txtEndTime );
            this.groupBox3.Controls.Add( this.lblEndTime );
            this.groupBox3.Controls.Add( this.txtStartTime );
            this.groupBox3.Controls.Add( this.lblStartTime );
            this.groupBox3.Controls.Add( this.nudThread );
            this.groupBox3.Controls.Add( this.nudDelay );
            this.groupBox3.Controls.Add( this.lblThread );
            this.groupBox3.Controls.Add( this.lblDelay );
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point( 260, 5 );
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size( 277, 150 );
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            // 
            // lnkViewDllTestLog
            // 
            this.lnkViewDllTestLog.AutoSize = true;
            this.lnkViewDllTestLog.Location = new System.Drawing.Point( 184, 122 );
            this.lnkViewDllTestLog.Name = "lnkViewDllTestLog";
            this.lnkViewDllTestLog.Size = new System.Drawing.Size( 72, 13 );
            this.lnkViewDllTestLog.TabIndex = 66;
            this.lnkViewDllTestLog.TabStop = true;
            this.lnkViewDllTestLog.Text = "DLL Test Log";
            this.lnkViewDllTestLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkViewDllTestLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkViewDllTestLog_LinkClicked );
            // 
            // txtAveSize
            // 
            this.txtAveSize.Location = new System.Drawing.Point( 60, 77 );
            this.txtAveSize.Name = "txtAveSize";
            this.txtAveSize.ReadOnly = true;
            this.txtAveSize.Size = new System.Drawing.Size( 66, 20 );
            this.txtAveSize.TabIndex = 65;
            this.txtAveSize.MouseEnter += new System.EventHandler( this.txtAveSize_MouseEnter );
            // 
            // lblAveSize
            // 
            this.lblAveSize.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblAveSize.Location = new System.Drawing.Point( 2, 79 );
            this.lblAveSize.Name = "lblAveSize";
            this.lblAveSize.Size = new System.Drawing.Size( 56, 17 );
            this.lblAveSize.TabIndex = 64;
            this.lblAveSize.Text = "Ave. Size";
            this.lblAveSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDllTiming
            // 
            this.chkDllTiming.AutoSize = true;
            this.chkDllTiming.Location = new System.Drawing.Point( 20, 121 );
            this.chkDllTiming.Name = "chkDllTiming";
            this.chkDllTiming.Size = new System.Drawing.Size( 104, 17 );
            this.chkDllTiming.TabIndex = 61;
            this.chkDllTiming.Text = "DLL Timing Test";
            // 
            // txtMailsSize
            // 
            this.txtMailsSize.Location = new System.Drawing.Point( 60, 55 );
            this.txtMailsSize.Name = "txtMailsSize";
            this.txtMailsSize.ReadOnly = true;
            this.txtMailsSize.Size = new System.Drawing.Size( 66, 20 );
            this.txtMailsSize.TabIndex = 63;
            this.txtMailsSize.MouseEnter += new System.EventHandler( this.txtMailsSize_MouseEnter );
            // 
            // lblTotalMailsSize
            // 
            this.lblTotalMailsSize.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTotalMailsSize.Location = new System.Drawing.Point( 2, 58 );
            this.lblTotalMailsSize.Name = "lblTotalMailsSize";
            this.lblTotalMailsSize.Size = new System.Drawing.Size( 56, 17 );
            this.lblTotalMailsSize.TabIndex = 62;
            this.lblTotalMailsSize.Text = "Mails Size";
            this.lblTotalMailsSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMailPerSec
            // 
            this.txtMailPerSec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMailPerSec.Location = new System.Drawing.Point( 187, 99 );
            this.txtMailPerSec.Name = "txtMailPerSec";
            this.txtMailPerSec.ReadOnly = true;
            this.txtMailPerSec.Size = new System.Drawing.Size( 84, 20 );
            this.txtMailPerSec.TabIndex = 60;
            this.txtMailPerSec.MouseEnter += new System.EventHandler( this.txtMailPerSec_MouseEnter );
            // 
            // lblMailPerSec
            // 
            this.lblMailPerSec.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMailPerSec.Location = new System.Drawing.Point( 131, 100 );
            this.lblMailPerSec.Name = "lblMailPerSec";
            this.lblMailPerSec.Size = new System.Drawing.Size( 53, 17 );
            this.lblMailPerSec.TabIndex = 59;
            this.lblMailPerSec.Text = "Mails/sec";
            this.lblMailPerSec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDuration
            // 
            this.txtDuration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDuration.Location = new System.Drawing.Point( 187, 55 );
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.ReadOnly = true;
            this.txtDuration.Size = new System.Drawing.Size( 84, 20 );
            this.txtDuration.TabIndex = 58;
            this.txtDuration.MouseEnter += new System.EventHandler( this.txtDuration_MouseEnter );
            // 
            // lblDuration
            // 
            this.lblDuration.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblDuration.Location = new System.Drawing.Point( 131, 57 );
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size( 53, 17 );
            this.lblDuration.TabIndex = 57;
            this.lblDuration.Text = "Duration";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumMail
            // 
            this.txtNumMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumMail.Location = new System.Drawing.Point( 187, 77 );
            this.txtNumMail.Name = "txtNumMail";
            this.txtNumMail.ReadOnly = true;
            this.txtNumMail.Size = new System.Drawing.Size( 84, 20 );
            this.txtNumMail.TabIndex = 54;
            this.txtNumMail.MouseEnter += new System.EventHandler( this.txtNumMail_MouseEnter );
            // 
            // lblMailStore
            // 
            this.lblMailStore.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMailStore.Location = new System.Drawing.Point( 131, 79 );
            this.lblMailStore.Name = "lblMailStore";
            this.lblMailStore.Size = new System.Drawing.Size( 53, 17 );
            this.lblMailStore.TabIndex = 53;
            this.lblMailStore.Text = "Mails";
            this.lblMailStore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndTime
            // 
            this.txtEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndTime.Location = new System.Drawing.Point( 187, 33 );
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.ReadOnly = true;
            this.txtEndTime.Size = new System.Drawing.Size( 84, 20 );
            this.txtEndTime.TabIndex = 52;
            this.txtEndTime.MouseEnter += new System.EventHandler( this.txtEndTime_MouseEnter );
            // 
            // lblEndTime
            // 
            this.lblEndTime.Location = new System.Drawing.Point( 131, 36 );
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size( 53, 17 );
            this.lblEndTime.TabIndex = 51;
            this.lblEndTime.Text = "End Time";
            this.lblEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartTime
            // 
            this.txtStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartTime.Location = new System.Drawing.Point( 187, 11 );
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.ReadOnly = true;
            this.txtStartTime.Size = new System.Drawing.Size( 84, 20 );
            this.txtStartTime.TabIndex = 50;
            this.txtStartTime.MouseEnter += new System.EventHandler( this.txtStartTime_MouseEnter );
            // 
            // lblStartTime
            // 
            this.lblStartTime.Location = new System.Drawing.Point( 127, 15 );
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size( 57, 17 );
            this.lblStartTime.TabIndex = 49;
            this.lblStartTime.Text = "Start Time";
            this.lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudThread
            // 
            this.nudThread.Location = new System.Drawing.Point( 60, 11 );
            this.nudThread.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.nudThread.Name = "nudThread";
            this.nudThread.Size = new System.Drawing.Size( 66, 20 );
            this.nudThread.TabIndex = 48;
            this.nudThread.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ttpZDlls.SetToolTip( this.nudThread, "Max. thread 100" );
            this.nudThread.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            // 
            // nudDelay
            // 
            this.nudDelay.Location = new System.Drawing.Point( 60, 33 );
            this.nudDelay.Maximum = new decimal( new int[] {
            5,
            0,
            0,
            0} );
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size( 66, 20 );
            this.nudDelay.TabIndex = 47;
            this.nudDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblThread
            // 
            this.lblThread.Location = new System.Drawing.Point( 2, 14 );
            this.lblThread.Name = "lblThread";
            this.lblThread.Size = new System.Drawing.Size( 56, 17 );
            this.lblThread.TabIndex = 45;
            this.lblThread.Text = "Thread";
            this.lblThread.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDelay
            // 
            this.lblDelay.Location = new System.Drawing.Point( 2, 35 );
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size( 56, 17 );
            this.lblDelay.TabIndex = 44;
            this.lblDelay.Text = "Delay";
            this.lblDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAbort
            // 
            this.btnAbort.Enabled = false;
            this.btnAbort.Location = new System.Drawing.Point( 400, 248 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 64, 21 );
            this.btnAbort.TabIndex = 56;
            this.btnAbort.Text = "Abort";
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // btnRun
            // 
            this.btnRun.Enabled = false;
            this.btnRun.Location = new System.Drawing.Point( 330, 248 );
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size( 64, 21 );
            this.btnRun.TabIndex = 55;
            this.btnRun.Text = "Run";
            this.btnRun.Click += new System.EventHandler( this.btnRun_Click );
            // 
            // lnkUpdateKeyFile
            // 
            this.lnkUpdateKeyFile.AutoSize = true;
            this.lnkUpdateKeyFile.Enabled = false;
            this.lnkUpdateKeyFile.Location = new System.Drawing.Point( 5, 19 );
            this.lnkUpdateKeyFile.Name = "lnkUpdateKeyFile";
            this.lnkUpdateKeyFile.Size = new System.Drawing.Size( 49, 13 );
            this.lnkUpdateKeyFile.TabIndex = 3;
            this.lnkUpdateKeyFile.TabStop = true;
            this.lnkUpdateKeyFile.Text = "zKey File";
            this.lnkUpdateKeyFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpZDlls.SetToolTip( this.lnkUpdateKeyFile, "Point to the ZDK file for retrieving messages." );
            this.lnkUpdateKeyFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkUpdateKeyFile_LinkClicked );
            // 
            // lnkMdeFolder
            // 
            this.lnkMdeFolder.AutoSize = true;
            this.lnkMdeFolder.Enabled = false;
            this.lnkMdeFolder.Location = new System.Drawing.Point( 18, 88 );
            this.lnkMdeFolder.Name = "lnkMdeFolder";
            this.lnkMdeFolder.Size = new System.Drawing.Size( 36, 13 );
            this.lnkMdeFolder.TabIndex = 39;
            this.lnkMdeFolder.TabStop = true;
            this.lnkMdeFolder.Text = "Folder";
            this.lnkMdeFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpZDlls.SetToolTip( this.lnkMdeFolder, "Point to the MDE files location" );
            this.lnkMdeFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkMdeFolder_LinkClicked );
            // 
            // cboMdeFolder
            // 
            this.cboMdeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMdeFolder.Enabled = false;
            this.cboMdeFolder.FormattingEnabled = true;
            this.cboMdeFolder.Items.AddRange( new object[] {
            "testdomain1",
            "testdomain2",
            "zdemo"} );
            this.cboMdeFolder.Location = new System.Drawing.Point( 56, 84 );
            this.cboMdeFolder.Name = "cboMdeFolder";
            this.cboMdeFolder.Size = new System.Drawing.Size( 188, 21 );
            this.cboMdeFolder.TabIndex = 40;
            this.ttpZDlls.SetToolTip( this.cboMdeFolder, "Point to MDE files location.\r\nHit Enter to save the current value." );
            this.cboMdeFolder.MouseEnter += new System.EventHandler( this.cboMdeFolder_MouseEnter );
            // 
            // btnGenFile
            // 
            this.btnGenFile.Enabled = false;
            this.btnGenFile.Location = new System.Drawing.Point( 260, 248 );
            this.btnGenFile.Name = "btnGenFile";
            this.btnGenFile.Size = new System.Drawing.Size( 64, 21 );
            this.btnGenFile.TabIndex = 57;
            this.btnGenFile.Text = "Gen File";
            this.btnGenFile.UseVisualStyleBackColor = true;
            this.btnGenFile.Click += new System.EventHandler( this.btnGenFile_Click );
            // 
            // groupUpdate
            // 
            this.groupUpdate.Controls.Add( this.cboMdeFolder );
            this.groupUpdate.Controls.Add( this.lnkMdeFolder );
            this.groupUpdate.Controls.Add( this.lblUpdateRcptTo );
            this.groupUpdate.Controls.Add( this.cboUpdateRcptTo );
            this.groupUpdate.Controls.Add( this.cboUpdateDomain );
            this.groupUpdate.Controls.Add( this.lblUpdateDomain );
            this.groupUpdate.Controls.Add( this.rdoDllUpdate );
            this.groupUpdate.Controls.Add( this.txtUpdateKeyFile );
            this.groupUpdate.Controls.Add( this.lnkUpdateKeyFile );
            this.groupUpdate.Enabled = false;
            this.groupUpdate.Location = new System.Drawing.Point( 3, 158 );
            this.groupUpdate.Name = "groupUpdate";
            this.groupUpdate.Size = new System.Drawing.Size( 251, 110 );
            this.groupUpdate.TabIndex = 58;
            this.groupUpdate.TabStop = false;
            // 
            // lblUpdateRcptTo
            // 
            this.lblUpdateRcptTo.Enabled = false;
            this.lblUpdateRcptTo.Location = new System.Drawing.Point( 4, 40 );
            this.lblUpdateRcptTo.Name = "lblUpdateRcptTo";
            this.lblUpdateRcptTo.Size = new System.Drawing.Size( 48, 16 );
            this.lblUpdateRcptTo.TabIndex = 38;
            this.lblUpdateRcptTo.Text = "Rcpt To";
            this.lblUpdateRcptTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboUpdateRcptTo
            // 
            this.cboUpdateRcptTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUpdateRcptTo.Enabled = false;
            this.cboUpdateRcptTo.FormattingEnabled = true;
            this.cboUpdateRcptTo.Location = new System.Drawing.Point( 56, 38 );
            this.cboUpdateRcptTo.Name = "cboUpdateRcptTo";
            this.cboUpdateRcptTo.Size = new System.Drawing.Size( 188, 21 );
            this.cboUpdateRcptTo.TabIndex = 37;
            this.cboUpdateRcptTo.Text = "admin@testdomain2.digitalsafe.net";
            this.cboUpdateRcptTo.MouseEnter += new System.EventHandler( this.cboUpdateRcptTo_MouseEnter );
            // 
            // cboUpdateDomain
            // 
            this.cboUpdateDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUpdateDomain.Enabled = false;
            this.cboUpdateDomain.FormattingEnabled = true;
            this.cboUpdateDomain.Items.AddRange( new object[] {
            "testdomain1",
            "testdomain2",
            "zdemo"} );
            this.cboUpdateDomain.Location = new System.Drawing.Point( 57, 61 );
            this.cboUpdateDomain.Name = "cboUpdateDomain";
            this.cboUpdateDomain.Size = new System.Drawing.Size( 188, 21 );
            this.cboUpdateDomain.TabIndex = 35;
            this.cboUpdateDomain.Text = "testdomain2";
            this.cboUpdateDomain.MouseEnter += new System.EventHandler( this.cboUpdateDomain_MouseEnter );
            // 
            // lblUpdateDomain
            // 
            this.lblUpdateDomain.Enabled = false;
            this.lblUpdateDomain.Location = new System.Drawing.Point( 6, 63 );
            this.lblUpdateDomain.Name = "lblUpdateDomain";
            this.lblUpdateDomain.Size = new System.Drawing.Size( 47, 16 );
            this.lblUpdateDomain.TabIndex = 36;
            this.lblUpdateDomain.Text = "Domain";
            this.lblUpdateDomain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rdoDllUpdate
            // 
            this.rdoDllUpdate.AutoSize = true;
            this.rdoDllUpdate.Location = new System.Drawing.Point( 7, -2 );
            this.rdoDllUpdate.Name = "rdoDllUpdate";
            this.rdoDllUpdate.Size = new System.Drawing.Size( 60, 17 );
            this.rdoDllUpdate.TabIndex = 5;
            this.rdoDllUpdate.Text = "Update";
            this.rdoDllUpdate.UseVisualStyleBackColor = false;
            this.rdoDllUpdate.Click += new System.EventHandler( this.rdoDllUpdate_Click );
            // 
            // txtUpdateKeyFile
            // 
            this.txtUpdateKeyFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUpdateKeyFile.Enabled = false;
            this.txtUpdateKeyFile.Location = new System.Drawing.Point( 56, 16 );
            this.txtUpdateKeyFile.Name = "txtUpdateKeyFile";
            this.txtUpdateKeyFile.Size = new System.Drawing.Size( 188, 20 );
            this.txtUpdateKeyFile.TabIndex = 4;
            this.txtUpdateKeyFile.Text = "d:\\tmp\\zKey.txt";
            this.txtUpdateKeyFile.MouseEnter += new System.EventHandler( this.txtUpdateKeyFile_MouseEnter );
            // 
            // UcZDlls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.btnGenFile );
            this.Controls.Add( this.groupBox3 );
            this.Controls.Add( this.btnAbort );
            this.Controls.Add( this.btnRun );
            this.Controls.Add( this.groupArchive );
            this.Controls.Add( this.groupUpdate );
            this.Controls.Add( this.groupRetrieve );
            this.Name = "UcZDlls";
            this.Size = new System.Drawing.Size( 544, 274 );
            this.Load += new System.EventHandler( this.UcZDlls_Load );
            this.groupArchive.ResumeLayout( false );
            this.groupArchive.PerformLayout();
            this.groupRetrieve.ResumeLayout( false );
            this.groupRetrieve.PerformLayout();
            this.groupBox3.ResumeLayout( false );
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            this.groupUpdate.ResumeLayout( false );
            this.groupUpdate.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.GroupBox groupArchive;
        private System.Windows.Forms.ComboBox cboMailFrom;
        private System.Windows.Forms.DateTimePicker dtpArchive;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.ComboBox cboArchDomain;
        private System.Windows.Forms.LinkLabel lnkFolder;
        private System.Windows.Forms.Label lblRcptTo;
        private System.Windows.Forms.Label lblMailFrom;
        private System.Windows.Forms.Label lblArchDomain;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ComboBox cboRcptTo;
        private System.Windows.Forms.GroupBox groupRetrieve;
        private System.Windows.Forms.TextBox txtFailCall;
        private System.Windows.Forms.Label lblFailCall;
        private System.Windows.Forms.ComboBox cboRetrDomain;
        private System.Windows.Forms.Label lblRetrDomain;
        private System.Windows.Forms.TextBox txtStoreFolder;
        private System.Windows.Forms.TextBox txtKeyFile;
        private System.Windows.Forms.LinkLabel lnkStoreLocation;
        private System.Windows.Forms.LinkLabel lnkBrowseKeyFile;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.LinkLabel lnkViewDllTestLog;
        private System.Windows.Forms.TextBox txtAveSize;
        private System.Windows.Forms.Label lblAveSize;
        private System.Windows.Forms.CheckBox chkDllTiming;
        private System.Windows.Forms.TextBox txtMailsSize;
        private System.Windows.Forms.Label lblTotalMailsSize;
        private System.Windows.Forms.TextBox txtMailPerSec;
        private System.Windows.Forms.Label lblMailPerSec;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtNumMail;
        private System.Windows.Forms.Label lblMailStore;
        private System.Windows.Forms.TextBox txtEndTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.NumericUpDown nudThread;
        private System.Windows.Forms.NumericUpDown nudDelay;
        private System.Windows.Forms.Label lblThread;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ToolTip ttpZDlls;
        private System.Windows.Forms.RadioButton rdoDllArchive;
        private System.Windows.Forms.RadioButton rdoDllRetrieve;
        private System.Windows.Forms.Button btnGenFile;
        private System.Windows.Forms.GroupBox groupUpdate;
        private System.Windows.Forms.RadioButton rdoDllUpdate;
        private System.Windows.Forms.TextBox txtUpdateKeyFile;
        private System.Windows.Forms.LinkLabel lnkUpdateKeyFile;
        private System.Windows.Forms.Label lblUpdateRcptTo;
        private System.Windows.Forms.ComboBox cboUpdateRcptTo;
        private System.Windows.Forms.ComboBox cboUpdateDomain;
        private System.Windows.Forms.Label lblUpdateDomain;
        private System.Windows.Forms.LinkLabel lnkMdeFolder;
        private System.Windows.Forms.ComboBox cboMdeFolder;
        private System.Windows.Forms.ComboBox cboMdeFile;
        private System.Windows.Forms.CheckBox chkDoUpdate;
    }
}
