namespace WsClient
{
    partial class UcMailClient
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
                if (mailThread != null && mailThread.IsAlive)
                {
                    this.KillMailThread();
                    commObj.LogToFile("Thread.log", "++ MailThread Killed ++");
                }

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
            this.rdoFile = new System.Windows.Forms.RadioButton();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.lnkFolder = new System.Windows.Forms.LinkLabel();
            this.rdoUI = new System.Windows.Forms.RadioButton();
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.lblDelay = new System.Windows.Forms.Label();
            this.cboTo = new System.Windows.Forms.ComboBox();
            this.lnkTo = new System.Windows.Forms.LinkLabel();
            this.ttipOLPage = new System.Windows.Forms.ToolTip(this.components);
            this.lnkFile = new System.Windows.Forms.LinkLabel();
            this.chkAttach = new System.Windows.Forms.CheckBox();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.cboProfile = new System.Windows.Forms.ComboBox();
            this.txtAttach = new System.Windows.Forms.TextBox();
            this.chkMultiAttach = new System.Windows.Forms.CheckBox();
            this.lnkAttach = new System.Windows.Forms.LinkLabel();
            this.cboBCC = new System.Windows.Forms.ComboBox();
            this.cboCC = new System.Windows.Forms.ComboBox();
            this.chkGUID = new System.Windows.Forms.CheckBox();
            this.nudLoop = new System.Windows.Forms.NumericUpDown();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lnkProfile = new System.Windows.Forms.LinkLabel();
            this.richBox = new System.Windows.Forms.RichTextBox();
            this.lblLoop = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lnkBCC = new System.Windows.Forms.LinkLabel();
            this.lnkCC = new System.Windows.Forms.LinkLabel();
            this.rdoOutlook = new System.Windows.Forms.RadioButton();
            this.rdoLotus = new System.Windows.Forms.RadioButton();
            this.btnAbort = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoop)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoFile
            // 
            this.rdoFile.AutoCheck = false;
            this.rdoFile.Location = new System.Drawing.Point(5, 14);
            this.rdoFile.Name = "rdoFile";
            this.rdoFile.Size = new System.Drawing.Size(16, 16);
            this.rdoFile.TabIndex = 97;
            this.ttipOLPage.SetToolTip(this.rdoFile, "Automate from address file");
            this.rdoFile.Click += new System.EventHandler(this.rdoFile_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(182, 12);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '+';
            this.txtPassword.Size = new System.Drawing.Size(101, 20);
            this.txtPassword.TabIndex = 71;
            this.txtPassword.Text = "password0";
            this.ttipOLPage.SetToolTip(this.txtPassword, "password");
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Enabled = false;
            this.txtFile.Location = new System.Drawing.Point(65, 12);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(220, 20);
            this.txtFile.TabIndex = 98;
            this.txtFile.Text = "load address from file";
            // 
            // lnkFolder
            // 
            this.lnkFolder.Enabled = false;
            this.lnkFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkFolder.Location = new System.Drawing.Point(82, 41);
            this.lnkFolder.Name = "lnkFolder";
            this.lnkFolder.Size = new System.Drawing.Size(40, 16);
            this.lnkFolder.TabIndex = 61;
            this.lnkFolder.TabStop = true;
            this.lnkFolder.Text = "Folder";
            this.lnkFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttipOLPage.SetToolTip(this.lnkFolder, "Locate the attachement folder");
            // 
            // rdoUI
            // 
            this.rdoUI.Checked = true;
            this.rdoUI.Location = new System.Drawing.Point(5, 16);
            this.rdoUI.Name = "rdoUI";
            this.rdoUI.Size = new System.Drawing.Size(16, 16);
            this.rdoUI.TabIndex = 0;
            this.rdoUI.TabStop = true;
            this.ttipOLPage.SetToolTip(this.rdoUI, "Send individual mail");
            this.rdoUI.Click += new System.EventHandler(this.rdoUI_Click);
            // 
            // nudDelay
            // 
            this.nudDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDelay.Location = new System.Drawing.Point(144, 243);
            this.nudDelay.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size(44, 20);
            this.nudDelay.TabIndex = 94;
            this.nudDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ttipOLPage.SetToolTip(this.nudDelay, "sec (0..5)");
            // 
            // lblDelay
            // 
            this.lblDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDelay.Location = new System.Drawing.Point(106, 247);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(36, 16);
            this.lblDelay.TabIndex = 93;
            this.lblDelay.Text = "Delay";
            this.ttipOLPage.SetToolTip(this.lblDelay, "in Sec (0..5)");
            // 
            // cboTo
            // 
            this.cboTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTo.Items.AddRange(new object[] {
            ""});
            this.cboTo.Location = new System.Drawing.Point(70, 117);
            this.cboTo.Name = "cboTo";
            this.cboTo.Size = new System.Drawing.Size(218, 21);
            this.cboTo.TabIndex = 72;
            this.cboTo.Text = "doi@notesclient.zantaz.com";
            this.ttipOLPage.SetToolTip(this.cboTo, "mail to ");
            this.cboTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboTo_KeyPress);
            this.cboTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboTo_KeyDown);
            // 
            // lnkTo
            // 
            this.lnkTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTo.Location = new System.Drawing.Point(41, 119);
            this.lnkTo.Name = "lnkTo";
            this.lnkTo.Size = new System.Drawing.Size(28, 16);
            this.lnkTo.TabIndex = 89;
            this.lnkTo.TabStop = true;
            this.lnkTo.Text = "To";
            this.lnkTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkFile
            // 
            this.lnkFile.Enabled = false;
            this.lnkFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkFile.Location = new System.Drawing.Point(32, 14);
            this.lnkFile.Name = "lnkFile";
            this.lnkFile.Size = new System.Drawing.Size(32, 16);
            this.lnkFile.TabIndex = 51;
            this.lnkFile.TabStop = true;
            this.lnkFile.Text = "File :";
            this.lnkFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttipOLPage.SetToolTip(this.lnkFile, "Browse the address file");
            this.lnkFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFile_LinkClicked);
            // 
            // chkAttach
            // 
            this.chkAttach.Enabled = false;
            this.chkAttach.Location = new System.Drawing.Point(5, 40);
            this.chkAttach.Name = "chkAttach";
            this.chkAttach.Size = new System.Drawing.Size(80, 16);
            this.chkAttach.TabIndex = 99;
            this.chkAttach.Text = "Attachment";
            this.ttipOLPage.SetToolTip(this.chkAttach, "Include Attachment");
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Enabled = false;
            this.txtFolder.Location = new System.Drawing.Point(123, 37);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(161, 20);
            this.txtFolder.TabIndex = 100;
            this.txtFolder.Text = "C:\\C#Proj\\QATool";
            this.ttipOLPage.SetToolTip(this.txtFolder, "Attachment folder ONLY");
            // 
            // cboProfile
            // 
            this.cboProfile.Location = new System.Drawing.Point(65, 12);
            this.cboProfile.Name = "cboProfile";
            this.cboProfile.Size = new System.Drawing.Size(114, 21);
            this.cboProfile.TabIndex = 70;
            this.cboProfile.Text = "noteclientExch";
            this.ttipOLPage.SetToolTip(this.cboProfile, "outlook profile");
            this.cboProfile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboProfile_KeyPress);
            this.cboProfile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboProfile_KeyDown);
            // 
            // txtAttach
            // 
            this.txtAttach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAttach.Location = new System.Drawing.Point(66, 108);
            this.txtAttach.Name = "txtAttach";
            this.txtAttach.Size = new System.Drawing.Size(200, 20);
            this.txtAttach.TabIndex = 76;
            this.ttipOLPage.SetToolTip(this.txtAttach, "Attachment - file name");
            // 
            // chkMultiAttach
            // 
            this.chkMultiAttach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMultiAttach.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkMultiAttach.Location = new System.Drawing.Point(270, 111);
            this.chkMultiAttach.Name = "chkMultiAttach";
            this.chkMultiAttach.Size = new System.Drawing.Size(16, 16);
            this.chkMultiAttach.TabIndex = 75;
            this.chkMultiAttach.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ttipOLPage.SetToolTip(this.chkMultiAttach, "Include multiple Attachment");
            // 
            // lnkAttach
            // 
            this.lnkAttach.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkAttach.Location = new System.Drawing.Point(2, 110);
            this.lnkAttach.Name = "lnkAttach";
            this.lnkAttach.Size = new System.Drawing.Size(61, 16);
            this.lnkAttach.TabIndex = 75;
            this.lnkAttach.TabStop = true;
            this.lnkAttach.Text = "Attachment";
            this.lnkAttach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttipOLPage.SetToolTip(this.lnkAttach, "Browse attachment file");
            this.lnkAttach.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAttach_LinkClicked);
            // 
            // cboBCC
            // 
            this.cboBCC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboBCC.Location = new System.Drawing.Point(70, 165);
            this.cboBCC.Name = "cboBCC";
            this.cboBCC.Size = new System.Drawing.Size(218, 21);
            this.cboBCC.TabIndex = 74;
            this.ttipOLPage.SetToolTip(this.cboBCC, "bcc to");
            this.cboBCC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboBCC_KeyPress);
            this.cboBCC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboBCC_KeyDown);
            // 
            // cboCC
            // 
            this.cboCC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCC.Location = new System.Drawing.Point(70, 141);
            this.cboCC.Name = "cboCC";
            this.cboCC.Size = new System.Drawing.Size(218, 21);
            this.cboCC.TabIndex = 73;
            this.ttipOLPage.SetToolTip(this.cboCC, "cc to");
            this.cboCC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboCC_KeyPress);
            this.cboCC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboCC_KeyDown);
            // 
            // chkGUID
            // 
            this.chkGUID.Location = new System.Drawing.Point(12, 246);
            this.chkGUID.Name = "chkGUID";
            this.chkGUID.Size = new System.Drawing.Size(92, 16);
            this.chkGUID.TabIndex = 85;
            this.chkGUID.Text = "Include GUID";
            this.ttipOLPage.SetToolTip(this.chkGUID, "include GUID");
            // 
            // nudLoop
            // 
            this.nudLoop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudLoop.Location = new System.Drawing.Point(224, 243);
            this.nudLoop.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudLoop.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLoop.Name = "nudLoop";
            this.nudLoop.Size = new System.Drawing.Size(64, 20);
            this.nudLoop.TabIndex = 83;
            this.nudLoop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ttipOLPage.SetToolTip(this.nudLoop, "0..9999");
            this.nudLoop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(154, 266);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(64, 21);
            this.btnSend.TabIndex = 84;
            this.btnSend.Text = "Send";
            this.ttipOLPage.SetToolTip(this.btnSend, "Send mail via native mail client");
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rdoFile);
            this.groupBox1.Controls.Add(this.lnkFile);
            this.groupBox1.Controls.Add(this.txtFile);
            this.groupBox1.Controls.Add(this.chkAttach);
            this.groupBox1.Controls.Add(this.txtFolder);
            this.groupBox1.Controls.Add(this.lnkFolder);
            this.groupBox1.Location = new System.Drawing.Point(4, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 64);
            this.groupBox1.TabIndex = 91;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rdoUI);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.lnkProfile);
            this.groupBox2.Controls.Add(this.cboProfile);
            this.groupBox2.Controls.Add(this.txtAttach);
            this.groupBox2.Controls.Add(this.chkMultiAttach);
            this.groupBox2.Controls.Add(this.lnkAttach);
            this.groupBox2.Location = new System.Drawing.Point(4, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 134);
            this.groupBox2.TabIndex = 92;
            this.groupBox2.TabStop = false;
            // 
            // lnkProfile
            // 
            this.lnkProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkProfile.Location = new System.Drawing.Point(19, 14);
            this.lnkProfile.Name = "lnkProfile";
            this.lnkProfile.Size = new System.Drawing.Size(44, 16);
            this.lnkProfile.TabIndex = 68;
            this.lnkProfile.TabStop = true;
            this.lnkProfile.Text = "Profile";
            this.lnkProfile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // richBox
            // 
            this.richBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richBox.Location = new System.Drawing.Point(6, 293);
            this.richBox.Name = "richBox";
            this.richBox.Size = new System.Drawing.Size(284, 129);
            this.richBox.TabIndex = 88;
            this.richBox.Text = "richBox";
            // 
            // lblLoop
            // 
            this.lblLoop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoop.Location = new System.Drawing.Point(192, 247);
            this.lblLoop.Name = "lblLoop";
            this.lblLoop.Size = new System.Drawing.Size(32, 16);
            this.lblLoop.TabIndex = 82;
            this.lblLoop.Text = "Loop";
            // 
            // lblSubject
            // 
            this.lblSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubject.Location = new System.Drawing.Point(12, 220);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(56, 16);
            this.lblSubject.TabIndex = 81;
            this.lblSubject.Text = "Subject";
            this.lblSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(70, 219);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(216, 20);
            this.txtSubject.TabIndex = 80;
            this.txtSubject.Text = "txtSubject";
            // 
            // lnkBCC
            // 
            this.lnkBCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkBCC.Location = new System.Drawing.Point(33, 167);
            this.lnkBCC.Name = "lnkBCC";
            this.lnkBCC.Size = new System.Drawing.Size(36, 20);
            this.lnkBCC.TabIndex = 79;
            this.lnkBCC.TabStop = true;
            this.lnkBCC.Text = "BCC";
            this.lnkBCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkCC
            // 
            this.lnkCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCC.Location = new System.Drawing.Point(41, 143);
            this.lnkCC.Name = "lnkCC";
            this.lnkCC.Size = new System.Drawing.Size(28, 16);
            this.lnkCC.TabIndex = 78;
            this.lnkCC.TabStop = true;
            this.lnkCC.Text = "CC";
            this.lnkCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rdoOutlook
            // 
            this.rdoOutlook.AutoSize = true;
            this.rdoOutlook.Checked = true;
            this.rdoOutlook.Location = new System.Drawing.Point(9, 3);
            this.rdoOutlook.Name = "rdoOutlook";
            this.rdoOutlook.Size = new System.Drawing.Size(91, 17);
            this.rdoOutlook.TabIndex = 95;
            this.rdoOutlook.TabStop = true;
            this.rdoOutlook.Text = "Outlook Client";
            this.rdoOutlook.UseVisualStyleBackColor = true;
            this.rdoOutlook.Click += new System.EventHandler(this.rdoOutlook_Click);
            // 
            // rdoLotus
            // 
            this.rdoLotus.AutoSize = true;
            this.rdoLotus.Location = new System.Drawing.Point(148, 3);
            this.rdoLotus.Name = "rdoLotus";
            this.rdoLotus.Size = new System.Drawing.Size(82, 17);
            this.rdoLotus.TabIndex = 96;
            this.rdoLotus.Text = "Lotus Notes";
            this.rdoLotus.UseVisualStyleBackColor = true;
            this.rdoLotus.Click += new System.EventHandler(this.rdoLotus_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Location = new System.Drawing.Point(223, 266);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(64, 21);
            this.btnAbort.TabIndex = 97;
            this.btnAbort.Text = "Abort";
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // UcMailClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.rdoLotus);
            this.Controls.Add(this.rdoOutlook);
            this.Controls.Add(this.nudDelay);
            this.Controls.Add(this.lblDelay);
            this.Controls.Add(this.cboTo);
            this.Controls.Add(this.lnkTo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richBox);
            this.Controls.Add(this.cboBCC);
            this.Controls.Add(this.cboCC);
            this.Controls.Add(this.chkGUID);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.nudLoop);
            this.Controls.Add(this.lblLoop);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.lnkBCC);
            this.Controls.Add(this.lnkCC);
            this.Controls.Add(this.groupBox2);
            this.Name = "UcMailClient";
            this.Size = new System.Drawing.Size(300, 428);
            this.Load += new System.EventHandler(this.UcMailClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoop)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoFile;
        private System.Windows.Forms.ToolTip ttipOLPage;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.LinkLabel lnkFolder;
        private System.Windows.Forms.RadioButton rdoUI;
        private System.Windows.Forms.NumericUpDown nudDelay;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.ComboBox cboTo;
        private System.Windows.Forms.LinkLabel lnkTo;
        private System.Windows.Forms.LinkLabel lnkFile;
        private System.Windows.Forms.CheckBox chkAttach;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.ComboBox cboProfile;
        private System.Windows.Forms.TextBox txtAttach;
        private System.Windows.Forms.CheckBox chkMultiAttach;
        private System.Windows.Forms.LinkLabel lnkAttach;
        private System.Windows.Forms.ComboBox cboBCC;
        private System.Windows.Forms.ComboBox cboCC;
        private System.Windows.Forms.CheckBox chkGUID;
        private System.Windows.Forms.NumericUpDown nudLoop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel lnkProfile;
        private System.Windows.Forms.RichTextBox richBox;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblLoop;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.LinkLabel lnkBCC;
        private System.Windows.Forms.LinkLabel lnkCC;
        private System.Windows.Forms.RadioButton rdoOutlook;
        private System.Windows.Forms.RadioButton rdoLotus;
        private System.Windows.Forms.Button btnAbort;






    }
}
