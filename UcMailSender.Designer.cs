using System;
using System.Diagnostics;
namespace WsClient
{
    partial class UcMailSender
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
                        KillMailSenderThread(ref m_thdList[i]);
                }//end of for
            }//end of try
            catch (Exception ex)
            {
                commObj.LogToFile("UcArchive.cs - btnAbort_Click " + ex.Message);
            }//end of catch

            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( UcMailSender ) );
            this.cboRcptTo = new System.Windows.Forms.ComboBox();
            this.lblMailFrom = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.cboHeader = new System.Windows.Forms.ComboBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblRcptTo = new System.Windows.Forms.Label();
            this.cboHeaderVal = new System.Windows.Forms.ComboBox();
            this.rdoSMTP = new System.Windows.Forms.RadioButton();
            this.cboFileFrom = new System.Windows.Forms.ComboBox();
            this.ttpBatchMail = new System.Windows.Forms.ToolTip( this.components );
            this.rdoNormal = new System.Windows.Forms.RadioButton();
            this.cboTo = new System.Windows.Forms.ComboBox();
            this.cboBCC = new System.Windows.Forms.ComboBox();
            this.cboCC = new System.Windows.Forms.ComboBox();
            this.cboMailFrom = new System.Windows.Forms.ComboBox();
            this.chkGUID = new System.Windows.Forms.CheckBox();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.chkAttach = new System.Windows.Forms.CheckBox();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.cboPort = new System.Windows.Forms.ComboBox();
            this.cboSMTP = new System.Windows.Forms.ComboBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblSMTP = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lnkFolder = new System.Windows.Forms.LinkLabel();
            this.lnkSender = new System.Windows.Forms.LinkLabel();
            this.rdoInputFile = new System.Windows.Forms.RadioButton();
            this.txtMailFolder = new System.Windows.Forms.TextBox();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.rdoMsgFolder = new System.Windows.Forms.RadioButton();
            this.btnBroFolder = new System.Windows.Forms.Button();
            this.btnBroMailFile = new System.Windows.Forms.Button();
            this.nudCycle = new System.Windows.Forms.NumericUpDown();
            this.lnkFile = new System.Windows.Forms.LinkLabel();
            this.rdoFileCase = new System.Windows.Forms.RadioButton();
            this.rdoDefault = new System.Windows.Forms.RadioButton();
            this.cboEncoding = new System.Windows.Forms.ComboBox();
            this.lblEncoding = new System.Windows.Forms.Label();
            this.lnkTo = new System.Windows.Forms.LinkLabel();
            this.lnkBCC = new System.Windows.Forms.LinkLabel();
            this.lnkCC = new System.Windows.Forms.LinkLabel();
            this.gpbSMTP = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.richBox = new System.Windows.Forms.RichTextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.gbxDigiSafe = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMailPerSec = new System.Windows.Forms.TextBox();
            this.lblMailPerSec = new System.Windows.Forms.Label();
            this.txtAveSize = new System.Windows.Forms.TextBox();
            this.lblAveSize = new System.Windows.Forms.Label();
            this.txtMailsSize = new System.Windows.Forms.TextBox();
            this.lblTotalMailsSize = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.txtNumMail = new System.Windows.Forms.TextBox();
            this.lblSentMail = new System.Windows.Forms.Label();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.nudThread = new System.Windows.Forms.NumericUpDown();
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.lblThread = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbxOutLook = new System.Windows.Forms.GroupBox();
            this.txtMailAddrFile = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudCycle)).BeginInit();
            this.gpbSMTP.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            this.gbxOutLook.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboRcptTo
            // 
            this.cboRcptTo.Enabled = false;
            this.cboRcptTo.Location = new System.Drawing.Point( 62, 45 );
            this.cboRcptTo.Name = "cboRcptTo";
            this.cboRcptTo.Size = new System.Drawing.Size( 201, 21 );
            this.cboRcptTo.TabIndex = 2;
            this.ttpBatchMail.SetToolTip( this.cboRcptTo, "Type in or select from the pull down" );
            this.cboRcptTo.MouseEnter += new System.EventHandler( this.cboRcptTo_MouseEnter );
            this.cboRcptTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboRcptTo_KeyPress );
            this.cboRcptTo.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboRcptTo_KeyDown );
            // 
            // lblMailFrom
            // 
            this.lblMailFrom.Enabled = false;
            this.lblMailFrom.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lblMailFrom.Location = new System.Drawing.Point( 2, 23 );
            this.lblMailFrom.Name = "lblMailFrom";
            this.lblMailFrom.Size = new System.Drawing.Size( 62, 16 );
            this.lblMailFrom.TabIndex = 3;
            this.lblMailFrom.Text = "Mail From";
            // 
            // lblValue
            // 
            this.lblValue.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lblValue.Location = new System.Drawing.Point( 8, 143 );
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size( 48, 16 );
            this.lblValue.TabIndex = 105;
            this.lblValue.Text = "Value";
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboHeader
            // 
            this.cboHeader.Location = new System.Drawing.Point( 59, 119 );
            this.cboHeader.Name = "cboHeader";
            this.cboHeader.Size = new System.Drawing.Size( 210, 21 );
            this.cboHeader.TabIndex = 51;
            this.cboHeader.MouseEnter += new System.EventHandler( this.cboHeader_MouseEnter );
            this.cboHeader.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboHeader_KeyPress );
            this.cboHeader.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboHeader_KeyDown );
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lblHeader.Location = new System.Drawing.Point( 8, 121 );
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size( 48, 16 );
            this.lblHeader.TabIndex = 50;
            this.lblHeader.Text = "Header";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRcptTo
            // 
            this.lblRcptTo.Enabled = false;
            this.lblRcptTo.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lblRcptTo.Location = new System.Drawing.Point( 2, 47 );
            this.lblRcptTo.Name = "lblRcptTo";
            this.lblRcptTo.Size = new System.Drawing.Size( 62, 16 );
            this.lblRcptTo.TabIndex = 1;
            this.lblRcptTo.Text = "Rcpt To";
            this.ttpBatchMail.SetToolTip( this.lblRcptTo, "Rcpt To" );
            // 
            // cboHeaderVal
            // 
            this.cboHeaderVal.Location = new System.Drawing.Point( 59, 142 );
            this.cboHeaderVal.Name = "cboHeaderVal";
            this.cboHeaderVal.Size = new System.Drawing.Size( 210, 21 );
            this.cboHeaderVal.TabIndex = 52;
            this.cboHeaderVal.MouseEnter += new System.EventHandler( this.cboHeaderVal_MouseEnter );
            this.cboHeaderVal.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboHeaderVal_KeyPress );
            this.cboHeaderVal.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboHeaderVal_KeyDown );
            // 
            // rdoSMTP
            // 
            this.rdoSMTP.AutoCheck = false;
            this.rdoSMTP.BackColor = System.Drawing.SystemColors.Control;
            this.rdoSMTP.Location = new System.Drawing.Point( 8, 0 );
            this.rdoSMTP.Name = "rdoSMTP";
            this.rdoSMTP.Size = new System.Drawing.Size( 84, 16 );
            this.rdoSMTP.TabIndex = 1;
            this.rdoSMTP.Text = "SMTP Case";
            this.ttpBatchMail.SetToolTip( this.rdoSMTP, "Stream a file to an SMTP socket" );
            this.rdoSMTP.UseVisualStyleBackColor = false;
            this.rdoSMTP.Click += new System.EventHandler( this.rdoSMTP_Click );
            // 
            // cboFileFrom
            // 
            this.cboFileFrom.Location = new System.Drawing.Point( 59, 27 );
            this.cboFileFrom.Name = "cboFileFrom";
            this.cboFileFrom.Size = new System.Drawing.Size( 210, 21 );
            this.cboFileFrom.TabIndex = 104;
            this.ttpBatchMail.SetToolTip( this.cboFileFrom, "File that contain FROM address" );
            this.cboFileFrom.MouseEnter += new System.EventHandler( this.cboFileFrom_MouseEnter );
            this.cboFileFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboFileFrom_KeyPress );
            this.cboFileFrom.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboFileFrom_KeyDown );
            // 
            // rdoNormal
            // 
            this.rdoNormal.AutoCheck = false;
            this.rdoNormal.Checked = true;
            this.rdoNormal.Location = new System.Drawing.Point( 11, 7 );
            this.rdoNormal.Name = "rdoNormal";
            this.rdoNormal.Size = new System.Drawing.Size( 88, 16 );
            this.rdoNormal.TabIndex = 0;
            this.rdoNormal.TabStop = true;
            this.rdoNormal.Text = "Normal Case";
            this.ttpBatchMail.SetToolTip( this.rdoNormal, "Send mail by MS API" );
            this.rdoNormal.Click += new System.EventHandler( this.rdoNormal_Click );
            // 
            // cboTo
            // 
            this.cboTo.Items.AddRange( new object[] {
            ""} );
            this.cboTo.Location = new System.Drawing.Point( 59, 50 );
            this.cboTo.Name = "cboTo";
            this.cboTo.Size = new System.Drawing.Size( 210, 21 );
            this.cboTo.TabIndex = 102;
            this.ttpBatchMail.SetToolTip( this.cboTo, "mail to" );
            this.cboTo.MouseEnter += new System.EventHandler( this.cboTo_MouseEnter );
            this.cboTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboTo_KeyPress );
            this.cboTo.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboTo_KeyDown );
            // 
            // cboBCC
            // 
            this.cboBCC.Location = new System.Drawing.Point( 59, 97 );
            this.cboBCC.Name = "cboBCC";
            this.cboBCC.Size = new System.Drawing.Size( 210, 21 );
            this.cboBCC.TabIndex = 49;
            this.ttpBatchMail.SetToolTip( this.cboBCC, "BCC To" );
            this.cboBCC.MouseEnter += new System.EventHandler( this.cboBCC_MouseEnter );
            this.cboBCC.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboBCC_KeyPress );
            this.cboBCC.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboBCC_KeyDown );
            // 
            // cboCC
            // 
            this.cboCC.Location = new System.Drawing.Point( 59, 73 );
            this.cboCC.Name = "cboCC";
            this.cboCC.Size = new System.Drawing.Size( 210, 21 );
            this.cboCC.TabIndex = 103;
            this.ttpBatchMail.SetToolTip( this.cboCC, "CC To" );
            this.cboCC.MouseEnter += new System.EventHandler( this.cboCC_MouseEnter );
            this.cboCC.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboCC_KeyPress );
            this.cboCC.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboCC_KeyDown );
            // 
            // cboMailFrom
            // 
            this.cboMailFrom.Enabled = false;
            this.cboMailFrom.Location = new System.Drawing.Point( 61, 21 );
            this.cboMailFrom.Name = "cboMailFrom";
            this.cboMailFrom.Size = new System.Drawing.Size( 202, 21 );
            this.cboMailFrom.TabIndex = 4;
            this.ttpBatchMail.SetToolTip( this.cboMailFrom, "Type in or select from pull down box" );
            this.cboMailFrom.MouseEnter += new System.EventHandler( this.cboMailFrom_MouseEnter );
            this.cboMailFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboMailFrom_KeyPress );
            this.cboMailFrom.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboMailFrom_KeyDown );
            // 
            // chkGUID
            // 
            this.chkGUID.Location = new System.Drawing.Point( 3, 4 );
            this.chkGUID.Name = "chkGUID";
            this.chkGUID.Size = new System.Drawing.Size( 53, 16 );
            this.chkGUID.TabIndex = 126;
            this.chkGUID.Text = "GUID";
            this.ttpBatchMail.SetToolTip( this.chkGUID, "Include GUID" );
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Enabled = false;
            this.txtFolder.Location = new System.Drawing.Point( 106, 25 );
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size( 189, 20 );
            this.txtFolder.TabIndex = 125;
            this.ttpBatchMail.SetToolTip( this.txtFolder, "Path/Folder for attachments" );
            this.txtFolder.MouseEnter += new System.EventHandler( this.txtFolder_MouseEnter );
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point( 521, 264 );
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size( 53, 21 );
            this.btnSend.TabIndex = 123;
            this.btnSend.Text = "Send";
            this.ttpBatchMail.SetToolTip( this.btnSend, "Initial Sending thread" );
            this.btnSend.Click += new System.EventHandler( this.btnSend_Click );
            // 
            // chkAttach
            // 
            this.chkAttach.Location = new System.Drawing.Point( 3, 27 );
            this.chkAttach.Name = "chkAttach";
            this.chkAttach.Size = new System.Drawing.Size( 57, 16 );
            this.chkAttach.TabIndex = 122;
            this.chkAttach.Text = "Attach";
            this.ttpBatchMail.SetToolTip( this.chkAttach, "Include attachements" );
            this.chkAttach.CheckedChanged += new System.EventHandler( this.chkAttach_CheckedChanged );
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Location = new System.Drawing.Point( 522, 288 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 53, 21 );
            this.btnAbort.TabIndex = 133;
            this.btnAbort.Text = "Abort";
            this.ttpBatchMail.SetToolTip( this.btnAbort, "Kill the Sending thread... patient" );
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point( 521, 220 );
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size( 53, 21 );
            this.btnTest.TabIndex = 119;
            this.btnTest.Text = "Test";
            this.ttpBatchMail.SetToolTip( this.btnTest, "Test SMTP connection" );
            this.btnTest.Click += new System.EventHandler( this.btnTest_Click );
            // 
            // cboPort
            // 
            this.cboPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPort.ItemHeight = 13;
            this.cboPort.Location = new System.Drawing.Point( 465, 220 );
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size( 55, 21 );
            this.cboPort.TabIndex = 118;
            this.cboPort.Text = "25";
            this.ttpBatchMail.SetToolTip( this.cboPort, "port number" );
            this.cboPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboPort_KeyPress );
            this.cboPort.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboPort_KeyDown );
            // 
            // cboSMTP
            // 
            this.cboSMTP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSMTP.ItemHeight = 13;
            this.cboSMTP.Location = new System.Drawing.Point( 314, 220 );
            this.cboSMTP.Name = "cboSMTP";
            this.cboSMTP.Size = new System.Drawing.Size( 118, 21 );
            this.cboSMTP.TabIndex = 117;
            this.ttpBatchMail.SetToolTip( this.cboSMTP, "Server name or IP" );
            this.cboSMTP.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboSMTP_KeyPress );
            this.cboSMTP.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboSMTP_KeyDown );
            // 
            // lblPort
            // 
            this.lblPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPort.Location = new System.Drawing.Point( 435, 223 );
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size( 26, 16 );
            this.lblPort.TabIndex = 116;
            this.lblPort.Text = "Port";
            this.ttpBatchMail.SetToolTip( this.lblPort, "SMTP port number" );
            // 
            // lblSMTP
            // 
            this.lblSMTP.Location = new System.Drawing.Point( 276, 223 );
            this.lblSMTP.Name = "lblSMTP";
            this.lblSMTP.Size = new System.Drawing.Size( 37, 16 );
            this.lblSMTP.TabIndex = 115;
            this.lblSMTP.Text = "SMTP";
            this.ttpBatchMail.SetToolTip( this.lblSMTP, "SMTP Server name or IP" );
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point( 106, 2 );
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size( 189, 20 );
            this.txtSubject.TabIndex = 112;
            this.txtSubject.Text = "txtSubject";
            this.ttpBatchMail.SetToolTip( this.txtSubject, "Empty it if only want to show GUID" );
            this.txtSubject.MouseEnter += new System.EventHandler( this.txtSubject_MouseEnter );
            // 
            // lnkFolder
            // 
            this.lnkFolder.Enabled = false;
            this.lnkFolder.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkFolder.Location = new System.Drawing.Point( 54, 27 );
            this.lnkFolder.Name = "lnkFolder";
            this.lnkFolder.Size = new System.Drawing.Size( 48, 16 );
            this.lnkFolder.TabIndex = 124;
            this.lnkFolder.TabStop = true;
            this.lnkFolder.Text = "Folder";
            this.lnkFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpBatchMail.SetToolTip( this.lnkFolder, "Browse the attachment folder" );
            this.lnkFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkFolder_LinkClicked );
            // 
            // lnkSender
            // 
            this.lnkSender.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkSender.Location = new System.Drawing.Point( 8, 30 );
            this.lnkSender.Name = "lnkSender";
            this.lnkSender.Size = new System.Drawing.Size( 48, 16 );
            this.lnkSender.TabIndex = 83;
            this.lnkSender.TabStop = true;
            this.lnkSender.Text = "Sender";
            this.lnkSender.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpBatchMail.SetToolTip( this.lnkSender, "Browse the file contains sender address" );
            this.lnkSender.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkSender_LinkClicked );
            // 
            // rdoInputFile
            // 
            this.rdoInputFile.Enabled = false;
            this.rdoInputFile.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.rdoInputFile.ForeColor = System.Drawing.Color.Blue;
            this.rdoInputFile.Location = new System.Drawing.Point( 0, 26 );
            this.rdoInputFile.Name = "rdoInputFile";
            this.rdoInputFile.Size = new System.Drawing.Size( 64, 16 );
            this.rdoInputFile.TabIndex = 44;
            this.rdoInputFile.Text = "Mail File";
            this.ttpBatchMail.SetToolTip( this.rdoInputFile, "Stream this file" );
            this.rdoInputFile.Click += new System.EventHandler( this.rdoInputFile_Click );
            // 
            // txtMailFolder
            // 
            this.txtMailFolder.Enabled = false;
            this.txtMailFolder.Location = new System.Drawing.Point( 82, 47 );
            this.txtMailFolder.Name = "txtMailFolder";
            this.txtMailFolder.Size = new System.Drawing.Size( 179, 20 );
            this.txtMailFolder.TabIndex = 7;
            this.ttpBatchMail.SetToolTip( this.txtMailFolder, "file that stream into socket" );
            this.txtMailFolder.MouseEnter += new System.EventHandler( this.txtMailFolder_MouseEnter );
            // 
            // txtInputFile
            // 
            this.txtInputFile.Enabled = false;
            this.txtInputFile.Location = new System.Drawing.Point( 82, 23 );
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size( 179, 20 );
            this.txtInputFile.TabIndex = 6;
            this.ttpBatchMail.SetToolTip( this.txtInputFile, "EML file that stream into socket" );
            this.txtInputFile.MouseEnter += new System.EventHandler( this.txtInputFile_MouseEnter );
            // 
            // rdoMsgFolder
            // 
            this.rdoMsgFolder.Enabled = false;
            this.rdoMsgFolder.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.rdoMsgFolder.ForeColor = System.Drawing.Color.Blue;
            this.rdoMsgFolder.Location = new System.Drawing.Point( 0, 48 );
            this.rdoMsgFolder.Name = "rdoMsgFolder";
            this.rdoMsgFolder.Size = new System.Drawing.Size( 56, 16 );
            this.rdoMsgFolder.TabIndex = 45;
            this.rdoMsgFolder.Text = "Folder";
            this.ttpBatchMail.SetToolTip( this.rdoMsgFolder, "Point to folder that contains mails" );
            this.rdoMsgFolder.Click += new System.EventHandler( this.rdoMsgFolder_Click );
            // 
            // btnBroFolder
            // 
            this.btnBroFolder.Enabled = false;
            this.btnBroFolder.Image = ((System.Drawing.Image)(resources.GetObject( "btnBroFolder.Image" )));
            this.btnBroFolder.Location = new System.Drawing.Point( 59, 47 );
            this.btnBroFolder.Name = "btnBroFolder";
            this.btnBroFolder.Size = new System.Drawing.Size( 19, 19 );
            this.btnBroFolder.TabIndex = 137;
            this.ttpBatchMail.SetToolTip( this.btnBroFolder, "Browse Folder" );
            this.btnBroFolder.UseVisualStyleBackColor = true;
            this.btnBroFolder.Click += new System.EventHandler( this.btnBroFolder_Click );
            // 
            // btnBroMailFile
            // 
            this.btnBroMailFile.Enabled = false;
            this.btnBroMailFile.Image = ((System.Drawing.Image)(resources.GetObject( "btnBroMailFile.Image" )));
            this.btnBroMailFile.Location = new System.Drawing.Point( 60, 23 );
            this.btnBroMailFile.Name = "btnBroMailFile";
            this.btnBroMailFile.Size = new System.Drawing.Size( 19, 19 );
            this.btnBroMailFile.TabIndex = 136;
            this.ttpBatchMail.SetToolTip( this.btnBroMailFile, "Browse EML File" );
            this.btnBroMailFile.UseVisualStyleBackColor = true;
            this.btnBroMailFile.Click += new System.EventHandler( this.btnBroMailFile_Click );
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
            this.ttpBatchMail.SetToolTip( this.nudCycle, "Number of repeat" );
            this.nudCycle.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            // 
            // lnkFile
            // 
            this.lnkFile.Enabled = false;
            this.lnkFile.Location = new System.Drawing.Point( 32, 20 );
            this.lnkFile.Name = "lnkFile";
            this.lnkFile.Size = new System.Drawing.Size( 32, 16 );
            this.lnkFile.TabIndex = 2;
            this.lnkFile.TabStop = true;
            this.lnkFile.Text = "File :";
            this.ttpBatchMail.SetToolTip( this.lnkFile, "Locate the address file" );
            this.lnkFile.Click += new System.EventHandler( this.lnkFile_Click );
            // 
            // rdoFileCase
            // 
            this.rdoFileCase.AutoCheck = false;
            this.rdoFileCase.Location = new System.Drawing.Point( 8, 0 );
            this.rdoFileCase.Name = "rdoFileCase";
            this.rdoFileCase.Size = new System.Drawing.Size( 41, 16 );
            this.rdoFileCase.TabIndex = 0;
            this.rdoFileCase.Text = "File";
            this.ttpBatchMail.SetToolTip( this.rdoFileCase, "File contains address criteria" );
            this.rdoFileCase.Click += new System.EventHandler( this.rdoFileCase_Click );
            // 
            // rdoDefault
            // 
            this.rdoDefault.Checked = true;
            this.rdoDefault.Enabled = false;
            this.rdoDefault.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.rdoDefault.ForeColor = System.Drawing.Color.Blue;
            this.rdoDefault.Location = new System.Drawing.Point( 1, 1 );
            this.rdoDefault.Name = "rdoDefault";
            this.rdoDefault.Size = new System.Drawing.Size( 59, 17 );
            this.rdoDefault.TabIndex = 138;
            this.rdoDefault.TabStop = true;
            this.rdoDefault.Text = "Default";
            this.ttpBatchMail.SetToolTip( this.rdoDefault, "Stream this file" );
            this.rdoDefault.Click += new System.EventHandler( this.rdoDefault_Click );
            // 
            // cboEncoding
            // 
            this.cboEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEncoding.ItemHeight = 13;
            this.cboEncoding.Items.AddRange( new object[] {
            "ASCII",
            "UTF-8"} );
            this.cboEncoding.Location = new System.Drawing.Point( 314, 243 );
            this.cboEncoding.Name = "cboEncoding";
            this.cboEncoding.Size = new System.Drawing.Size( 118, 21 );
            this.cboEncoding.TabIndex = 137;
            this.cboEncoding.Text = "UTF-8";
            this.ttpBatchMail.SetToolTip( this.cboEncoding, "Server name or IP" );
            // 
            // lblEncoding
            // 
            this.lblEncoding.Location = new System.Drawing.Point( 277, 246 );
            this.lblEncoding.Name = "lblEncoding";
            this.lblEncoding.Size = new System.Drawing.Size( 37, 16 );
            this.lblEncoding.TabIndex = 138;
            this.lblEncoding.Text = "Code";
            this.ttpBatchMail.SetToolTip( this.lblEncoding, "Char Set Encoding" );
            // 
            // lnkTo
            // 
            this.lnkTo.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkTo.Location = new System.Drawing.Point( 8, 54 );
            this.lnkTo.Name = "lnkTo";
            this.lnkTo.Size = new System.Drawing.Size( 48, 16 );
            this.lnkTo.TabIndex = 84;
            this.lnkTo.TabStop = true;
            this.lnkTo.Text = "To";
            this.lnkTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkBCC
            // 
            this.lnkBCC.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkBCC.Location = new System.Drawing.Point( 8, 101 );
            this.lnkBCC.Name = "lnkBCC";
            this.lnkBCC.Size = new System.Drawing.Size( 48, 20 );
            this.lnkBCC.TabIndex = 23;
            this.lnkBCC.TabStop = true;
            this.lnkBCC.Text = "BCC";
            this.lnkBCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkCC
            // 
            this.lnkCC.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkCC.Location = new System.Drawing.Point( 8, 77 );
            this.lnkCC.Name = "lnkCC";
            this.lnkCC.Size = new System.Drawing.Size( 48, 16 );
            this.lnkCC.TabIndex = 85;
            this.lnkCC.TabStop = true;
            this.lnkCC.Text = "CC";
            this.lnkCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gpbSMTP
            // 
            this.gpbSMTP.Controls.Add( this.panel1 );
            this.gpbSMTP.Controls.Add( this.cboMailFrom );
            this.gpbSMTP.Controls.Add( this.lblMailFrom );
            this.gpbSMTP.Controls.Add( this.cboRcptTo );
            this.gpbSMTP.Controls.Add( this.lblRcptTo );
            this.gpbSMTP.Controls.Add( this.rdoSMTP );
            this.gpbSMTP.Location = new System.Drawing.Point( 3, 169 );
            this.gpbSMTP.Name = "gpbSMTP";
            this.gpbSMTP.Size = new System.Drawing.Size( 272, 146 );
            this.gpbSMTP.TabIndex = 130;
            this.gpbSMTP.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add( this.rdoDefault );
            this.panel1.Controls.Add( this.btnBroFolder );
            this.panel1.Controls.Add( this.btnBroMailFile );
            this.panel1.Controls.Add( this.rdoMsgFolder );
            this.panel1.Controls.Add( this.dtPicker );
            this.panel1.Controls.Add( this.txtInputFile );
            this.panel1.Controls.Add( this.txtMailFolder );
            this.panel1.Controls.Add( this.rdoInputFile );
            this.panel1.Location = new System.Drawing.Point( 2, 69 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 264, 71 );
            this.panel1.TabIndex = 5;
            // 
            // dtPicker
            // 
            this.dtPicker.Enabled = false;
            this.dtPicker.Location = new System.Drawing.Point( 60, 0 );
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size( 201, 20 );
            this.dtPicker.TabIndex = 137;
            // 
            // richBox
            // 
            this.richBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richBox.Location = new System.Drawing.Point( 279, 266 );
            this.richBox.Name = "richBox";
            this.richBox.Size = new System.Drawing.Size( 239, 42 );
            this.richBox.TabIndex = 114;
            this.richBox.Text = "Under Construction";
            // 
            // lblSubject
            // 
            this.lblSubject.Font = new System.Drawing.Font( "Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lblSubject.Location = new System.Drawing.Point( 53, 3 );
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size( 51, 16 );
            this.lblSubject.TabIndex = 113;
            this.lblSubject.Text = "Subject";
            this.lblSubject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbxDigiSafe
            // 
            this.gbxDigiSafe.Location = new System.Drawing.Point( 5, 7 );
            this.gbxDigiSafe.Name = "gbxDigiSafe";
            this.gbxDigiSafe.Size = new System.Drawing.Size( 270, 162 );
            this.gbxDigiSafe.TabIndex = 127;
            this.gbxDigiSafe.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add( this.txtSubject );
            this.panel2.Controls.Add( this.lnkFolder );
            this.panel2.Controls.Add( this.lblSubject );
            this.panel2.Controls.Add( this.chkAttach );
            this.panel2.Controls.Add( this.txtFolder );
            this.panel2.Controls.Add( this.chkGUID );
            this.panel2.Location = new System.Drawing.Point( 280, 169 );
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size( 300, 49 );
            this.panel2.TabIndex = 134;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add( this.txtMailPerSec );
            this.groupBox2.Controls.Add( this.lblMailPerSec );
            this.groupBox2.Controls.Add( this.txtAveSize );
            this.groupBox2.Controls.Add( this.lblAveSize );
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
            this.groupBox2.Controls.Add( this.label1 );
            this.groupBox2.Controls.Add( this.label2 );
            this.groupBox2.Location = new System.Drawing.Point( 278, 44 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size( 302, 125 );
            this.groupBox2.TabIndex = 135;
            this.groupBox2.TabStop = false;
            // 
            // txtMailPerSec
            // 
            this.txtMailPerSec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMailPerSec.Location = new System.Drawing.Point( 184, 99 );
            this.txtMailPerSec.Name = "txtMailPerSec";
            this.txtMailPerSec.ReadOnly = true;
            this.txtMailPerSec.Size = new System.Drawing.Size( 110, 20 );
            this.txtMailPerSec.TabIndex = 51;
            this.txtMailPerSec.MouseEnter += new System.EventHandler( this.txtMailPerSec_MouseEnter );
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
            // txtAveSize
            // 
            this.txtAveSize.Location = new System.Drawing.Point( 63, 99 );
            this.txtAveSize.Name = "txtAveSize";
            this.txtAveSize.ReadOnly = true;
            this.txtAveSize.Size = new System.Drawing.Size( 60, 20 );
            this.txtAveSize.TabIndex = 49;
            this.txtAveSize.MouseEnter += new System.EventHandler( this.txtAveSize_MouseEnter );
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
            // txtMailsSize
            // 
            this.txtMailsSize.Location = new System.Drawing.Point( 63, 77 );
            this.txtMailsSize.Name = "txtMailsSize";
            this.txtMailsSize.ReadOnly = true;
            this.txtMailsSize.Size = new System.Drawing.Size( 60, 20 );
            this.txtMailsSize.TabIndex = 47;
            this.txtMailsSize.MouseEnter += new System.EventHandler( this.txtMailsSize_MouseEnter );
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
            this.txtDuration.Size = new System.Drawing.Size( 110, 20 );
            this.txtDuration.TabIndex = 44;
            this.txtDuration.MouseEnter += new System.EventHandler( this.txtDuration_MouseEnter );
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
            // txtNumMail
            // 
            this.txtNumMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumMail.Location = new System.Drawing.Point( 184, 77 );
            this.txtNumMail.Name = "txtNumMail";
            this.txtNumMail.ReadOnly = true;
            this.txtNumMail.Size = new System.Drawing.Size( 110, 20 );
            this.txtNumMail.TabIndex = 42;
            this.txtNumMail.MouseEnter += new System.EventHandler( this.txtNumMail_MouseEnter );
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
            this.txtEndTime.Size = new System.Drawing.Size( 110, 20 );
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
            // txtStartTime
            // 
            this.txtStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartTime.Location = new System.Drawing.Point( 184, 12 );
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.ReadOnly = true;
            this.txtStartTime.Size = new System.Drawing.Size( 110, 20 );
            this.txtStartTime.TabIndex = 38;
            this.txtStartTime.MouseEnter += new System.EventHandler( this.txtStartTime_MouseEnter );
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
            // nudThread
            // 
            this.nudThread.Location = new System.Drawing.Point( 63, 34 );
            this.nudThread.Maximum = new decimal( new int[] {
            10,
            0,
            0,
            0} );
            this.nudThread.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.nudThread.Name = "nudThread";
            this.nudThread.Size = new System.Drawing.Size( 60, 20 );
            this.nudThread.TabIndex = 36;
            this.nudThread.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // label1
            // 
            this.label1.Location = new System.Drawing.Point( 4, 56 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 55, 17 );
            this.label1.TabIndex = 32;
            this.label1.Text = "Delay";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point( 4, 16 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 55, 17 );
            this.label2.TabIndex = 31;
            this.label2.Text = "Cycle";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbxOutLook
            // 
            this.gbxOutLook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxOutLook.Controls.Add( this.lnkFile );
            this.gbxOutLook.Controls.Add( this.rdoFileCase );
            this.gbxOutLook.Controls.Add( this.txtMailAddrFile );
            this.gbxOutLook.Location = new System.Drawing.Point( 278, 6 );
            this.gbxOutLook.Name = "gbxOutLook";
            this.gbxOutLook.Size = new System.Drawing.Size( 302, 40 );
            this.gbxOutLook.TabIndex = 136;
            this.gbxOutLook.TabStop = false;
            // 
            // txtMailAddrFile
            // 
            this.txtMailAddrFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMailAddrFile.Enabled = false;
            this.txtMailAddrFile.Location = new System.Drawing.Point( 64, 16 );
            this.txtMailAddrFile.Name = "txtMailAddrFile";
            this.txtMailAddrFile.Size = new System.Drawing.Size( 230, 20 );
            this.txtMailAddrFile.TabIndex = 52;
            this.txtMailAddrFile.Text = "mail address  file";
            // 
            // UcMailSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.lblEncoding );
            this.Controls.Add( this.cboEncoding );
            this.Controls.Add( this.gbxOutLook );
            this.Controls.Add( this.groupBox2 );
            this.Controls.Add( this.panel2 );
            this.Controls.Add( this.lblValue );
            this.Controls.Add( this.cboHeaderVal );
            this.Controls.Add( this.cboFileFrom );
            this.Controls.Add( this.rdoNormal );
            this.Controls.Add( this.richBox );
            this.Controls.Add( this.btnSend );
            this.Controls.Add( this.cboPort );
            this.Controls.Add( this.cboHeader );
            this.Controls.Add( this.lblSMTP );
            this.Controls.Add( this.lnkSender );
            this.Controls.Add( this.lblPort );
            this.Controls.Add( this.lnkTo );
            this.Controls.Add( this.cboSMTP );
            this.Controls.Add( this.lblHeader );
            this.Controls.Add( this.btnAbort );
            this.Controls.Add( this.cboTo );
            this.Controls.Add( this.btnTest );
            this.Controls.Add( this.cboCC );
            this.Controls.Add( this.lnkBCC );
            this.Controls.Add( this.cboBCC );
            this.Controls.Add( this.gpbSMTP );
            this.Controls.Add( this.lnkCC );
            this.Controls.Add( this.gbxDigiSafe );
            this.Name = "UcMailSender";
            this.Size = new System.Drawing.Size( 583, 323 );
            this.Load += new System.EventHandler( this.UcMailSender_Load );
            ((System.ComponentModel.ISupportInitialize)(this.nudCycle)).EndInit();
            this.gpbSMTP.ResumeLayout( false );
            this.panel1.ResumeLayout( false );
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout( false );
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout( false );
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            this.gbxOutLook.ResumeLayout( false );
            this.gbxOutLook.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ComboBox cboRcptTo;
        private System.Windows.Forms.ToolTip ttpBatchMail;
        private System.Windows.Forms.Label lblMailFrom;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ComboBox cboHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblRcptTo;
        private System.Windows.Forms.ComboBox cboHeaderVal;
        private System.Windows.Forms.RadioButton rdoSMTP;
        private System.Windows.Forms.ComboBox cboFileFrom;
        private System.Windows.Forms.RadioButton rdoNormal;
        private System.Windows.Forms.ComboBox cboTo;
        private System.Windows.Forms.ComboBox cboBCC;
        private System.Windows.Forms.ComboBox cboCC;
        private System.Windows.Forms.ComboBox cboMailFrom;
        private System.Windows.Forms.CheckBox chkGUID;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.CheckBox chkAttach;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ComboBox cboPort;
        private System.Windows.Forms.ComboBox cboSMTP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblSMTP;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.LinkLabel lnkFolder;
        private System.Windows.Forms.LinkLabel lnkSender;
        private System.Windows.Forms.LinkLabel lnkTo;
        private System.Windows.Forms.LinkLabel lnkBCC;
        private System.Windows.Forms.LinkLabel lnkCC;
        private System.Windows.Forms.GroupBox gpbSMTP;
        private System.Windows.Forms.RichTextBox richBox;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.GroupBox gbxDigiSafe;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMailPerSec;
        private System.Windows.Forms.Label lblMailPerSec;
        private System.Windows.Forms.TextBox txtAveSize;
        private System.Windows.Forms.Label lblAveSize;
        private System.Windows.Forms.TextBox txtMailsSize;
        private System.Windows.Forms.Label lblTotalMailsSize;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtNumMail;
        private System.Windows.Forms.Label lblSentMail;
        private System.Windows.Forms.TextBox txtEndTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.NumericUpDown nudThread;
        private System.Windows.Forms.NumericUpDown nudDelay;
        private System.Windows.Forms.NumericUpDown nudCycle;
        private System.Windows.Forms.Label lblThread;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdoMsgFolder;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.TextBox txtMailFolder;
        private System.Windows.Forms.RadioButton rdoInputFile;
        private System.Windows.Forms.Button btnBroMailFile;
        private System.Windows.Forms.Button btnBroFolder;
        private System.Windows.Forms.GroupBox gbxOutLook;
        private System.Windows.Forms.LinkLabel lnkFile;
        private System.Windows.Forms.RadioButton rdoFileCase;
        private System.Windows.Forms.TextBox txtMailAddrFile;
        private System.Windows.Forms.DateTimePicker dtPicker;
        private System.Windows.Forms.RadioButton rdoDefault;
        private System.Windows.Forms.ComboBox cboEncoding;
        private System.Windows.Forms.Label lblEncoding;
    }
}
