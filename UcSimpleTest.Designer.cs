namespace WsClient
{
    partial class UcSimpleTest
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblRcptTo = new System.Windows.Forms.Label();
            this.lblMailFrom = new System.Windows.Forms.Label();
            this.lblDomainName = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lnkLabel = new System.Windows.Forms.LinkLabel();
            this.rdoFileName = new System.Windows.Forms.RadioButton();
            this.ttpTestPanel = new System.Windows.Forms.ToolTip( this.components );
            this.rdoString = new System.Windows.Forms.RadioButton();
            this.cboRcptTo = new System.Windows.Forms.ComboBox();
            this.cboMailFrom = new System.Windows.Forms.ComboBox();
            this.txtDataHandler = new System.Windows.Forms.TextBox();
            this.cboDomainName = new System.Windows.Forms.ComboBox();
            this.btnArchive = new System.Windows.Forms.Button();
            this.chkDllTest = new System.Windows.Forms.CheckBox();
            this.btnMdUpdate = new System.Windows.Forms.Button();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.btnIsAlive = new System.Windows.Forms.Button();
            this.cboWsUrl = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.chkSkipCompleteCall = new System.Windows.Forms.CheckBox();
            this.dtpArchive = new System.Windows.Forms.DateTimePicker();
            this.txtZDK = new System.Windows.Forms.TextBox();
            this.lblWsURL = new System.Windows.Forms.Label();
            this.rdoDIME = new System.Windows.Forms.RadioButton();
            this.rdoMIME = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lnkMdeFile = new System.Windows.Forms.LinkLabel();
            this.txtMdeFile = new System.Windows.Forms.TextBox();
            this.chkMde = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRcptTo
            // 
            this.lblRcptTo.Location = new System.Drawing.Point( 16, 167 );
            this.lblRcptTo.Name = "lblRcptTo";
            this.lblRcptTo.Size = new System.Drawing.Size( 44, 16 );
            this.lblRcptTo.TabIndex = 36;
            this.lblRcptTo.Text = "To";
            this.lblRcptTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMailFrom
            // 
            this.lblMailFrom.Location = new System.Drawing.Point( 16, 143 );
            this.lblMailFrom.Name = "lblMailFrom";
            this.lblMailFrom.Size = new System.Drawing.Size( 44, 16 );
            this.lblMailFrom.TabIndex = 35;
            this.lblMailFrom.Text = "From";
            this.lblMailFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDomainName
            // 
            this.lblDomainName.Location = new System.Drawing.Point( 16, 77 );
            this.lblDomainName.Name = "lblDomainName";
            this.lblDomainName.Size = new System.Drawing.Size( 44, 16 );
            this.lblDomainName.TabIndex = 34;
            this.lblDomainName.Text = "Domain";
            this.lblDomainName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point( 16, 119 );
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size( 44, 16 );
            this.lblDate.TabIndex = 33;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkLabel
            // 
            this.lnkLabel.Location = new System.Drawing.Point( 4, 30 );
            this.lnkLabel.Name = "lnkLabel";
            this.lnkLabel.Size = new System.Drawing.Size( 56, 16 );
            this.lnkLabel.TabIndex = 39;
            this.lnkLabel.TabStop = true;
            this.lnkLabel.Text = "File Name";
            this.lnkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpTestPanel.SetToolTip( this.lnkLabel, "Click for select an input file" );
            this.lnkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkLabel_LinkClicked );
            // 
            // rdoFileName
            // 
            this.rdoFileName.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rdoFileName.Checked = true;
            this.rdoFileName.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rdoFileName.Location = new System.Drawing.Point( 4, 10 );
            this.rdoFileName.Name = "rdoFileName";
            this.rdoFileName.Size = new System.Drawing.Size( 92, 16 );
            this.rdoFileName.TabIndex = 38;
            this.rdoFileName.TabStop = true;
            this.rdoFileName.Text = "File Name";
            this.ttpTestPanel.SetToolTip( this.rdoFileName, "Read in a file - Beyon 65526 char" );
            this.rdoFileName.Click += new System.EventHandler( this.rdoFileName_Click );
            // 
            // ttpTestPanel
            // 
            this.ttpTestPanel.AutoPopDelay = 10000;
            this.ttpTestPanel.InitialDelay = 500;
            this.ttpTestPanel.ReshowDelay = 100;
            // 
            // rdoString
            // 
            this.rdoString.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rdoString.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rdoString.Location = new System.Drawing.Point( 100, 10 );
            this.rdoString.Name = "rdoString";
            this.rdoString.Size = new System.Drawing.Size( 92, 17 );
            this.rdoString.TabIndex = 37;
            this.rdoString.Text = "Simple String";
            this.rdoString.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ttpTestPanel.SetToolTip( this.rdoString, "1st field is the simple string" );
            this.rdoString.Click += new System.EventHandler( this.rdoString_Click );
            // 
            // cboRcptTo
            // 
            this.cboRcptTo.FormattingEnabled = true;
            this.cboRcptTo.Location = new System.Drawing.Point( 64, 163 );
            this.cboRcptTo.Name = "cboRcptTo";
            this.cboRcptTo.Size = new System.Drawing.Size( 200, 21 );
            this.cboRcptTo.TabIndex = 29;
            this.ttpTestPanel.SetToolTip( this.cboRcptTo, "Rcpt To\r\nHit Enter to save current value" );
            this.cboRcptTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboRcptTo_KeyPress );
            this.cboRcptTo.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboRcptTo_KeyDown );
            // 
            // cboMailFrom
            // 
            this.cboMailFrom.FormattingEnabled = true;
            this.cboMailFrom.Location = new System.Drawing.Point( 64, 139 );
            this.cboMailFrom.Name = "cboMailFrom";
            this.cboMailFrom.Size = new System.Drawing.Size( 200, 21 );
            this.cboMailFrom.TabIndex = 28;
            this.ttpTestPanel.SetToolTip( this.cboMailFrom, "Mail From\r\nHit Enter to save current value" );
            this.cboMailFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboMailFrom_KeyPress );
            this.cboMailFrom.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboMailFrom_KeyDown );
            // 
            // txtDataHandler
            // 
            this.txtDataHandler.Location = new System.Drawing.Point( 64, 28 );
            this.txtDataHandler.MaxLength = 65536;
            this.txtDataHandler.Name = "txtDataHandler";
            this.txtDataHandler.Size = new System.Drawing.Size( 200, 20 );
            this.txtDataHandler.TabIndex = 26;
            this.ttpTestPanel.SetToolTip( this.txtDataHandler, "Mail Content - max 65536 chars" );
            // 
            // cboDomainName
            // 
            this.cboDomainName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDomainName.FormattingEnabled = true;
            this.cboDomainName.Location = new System.Drawing.Point( 64, 74 );
            this.cboDomainName.Name = "cboDomainName";
            this.cboDomainName.Size = new System.Drawing.Size( 200, 21 );
            this.cboDomainName.TabIndex = 25;
            this.ttpTestPanel.SetToolTip( this.cboDomainName, "Domain Name. \r\nHit Enter to save current value" );
            this.cboDomainName.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboDomainName_KeyPress );
            this.cboDomainName.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboDomainName_KeyDown );
            // 
            // btnArchive
            // 
            this.btnArchive.Location = new System.Drawing.Point( 270, 29 );
            this.btnArchive.Name = "btnArchive";
            this.btnArchive.Size = new System.Drawing.Size( 56, 21 );
            this.btnArchive.TabIndex = 30;
            this.btnArchive.Text = "Archive";
            this.ttpTestPanel.SetToolTip( this.btnArchive, "Archive it" );
            this.btnArchive.Click += new System.EventHandler( this.btnArchive_Click );
            // 
            // chkDllTest
            // 
            this.chkDllTest.Enabled = false;
            this.chkDllTest.Location = new System.Drawing.Point( 271, 5 );
            this.chkDllTest.Name = "chkDllTest";
            this.chkDllTest.Size = new System.Drawing.Size( 64, 15 );
            this.chkDllTest.TabIndex = 40;
            this.chkDllTest.Text = "Use Dll";
            this.chkDllTest.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ttpTestPanel.SetToolTip( this.chkDllTest, "Obsolete - Calling WS via dll" );
            this.chkDllTest.UseVisualStyleBackColor = true;
            this.chkDllTest.Click += new System.EventHandler( this.chkDllTest_Click );
            // 
            // btnMdUpdate
            // 
            this.btnMdUpdate.Location = new System.Drawing.Point( 270, 115 );
            this.btnMdUpdate.Name = "btnMdUpdate";
            this.btnMdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMdUpdate.Size = new System.Drawing.Size( 56, 22 );
            this.btnMdUpdate.TabIndex = 41;
            this.btnMdUpdate.Text = "Update";
            this.btnMdUpdate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ttpTestPanel.SetToolTip( this.btnMdUpdate, "Meta Data Update" );
            this.btnMdUpdate.Click += new System.EventHandler( this.btnMdUpdate_Click );
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point( 270, 74 );
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size( 56, 22 );
            this.btnRetrieve.TabIndex = 32;
            this.btnRetrieve.Text = "Retrieve";
            this.ttpTestPanel.SetToolTip( this.btnRetrieve, "Save to c:\\tmp\\DSfile.msg" );
            this.btnRetrieve.Click += new System.EventHandler( this.btnRetrieve_Click );
            // 
            // btnIsAlive
            // 
            this.btnIsAlive.Location = new System.Drawing.Point( 270, 164 );
            this.btnIsAlive.Name = "btnIsAlive";
            this.btnIsAlive.Size = new System.Drawing.Size( 56, 20 );
            this.btnIsAlive.TabIndex = 44;
            this.btnIsAlive.Text = "IsAlive";
            this.ttpTestPanel.SetToolTip( this.btnIsAlive, "Dime IsAlive API" );
            this.btnIsAlive.Click += new System.EventHandler( this.btnIsAlive_Click );
            // 
            // cboWsUrl
            // 
            this.cboWsUrl.FormattingEnabled = true;
            this.cboWsUrl.Location = new System.Drawing.Point( 64, 186 );
            this.cboWsUrl.Name = "cboWsUrl";
            this.cboWsUrl.Size = new System.Drawing.Size( 468, 21 );
            this.cboWsUrl.TabIndex = 44;
            this.ttpTestPanel.SetToolTip( this.cboWsUrl, "Under Construction - For Dime testing only" );
            this.cboWsUrl.SelectedIndexChanged += new System.EventHandler( this.cboWsUrl_SelectedIndexChanged );
            this.cboWsUrl.Layout += new System.Windows.Forms.LayoutEventHandler( this.cboWsUrl_Layout );
            this.cboWsUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboWsUrl_KeyPress );
            this.cboWsUrl.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboWsUrl_KeyDown );
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point( 270, 141 );
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size( 56, 21 );
            this.btnDelete.TabIndex = 45;
            this.btnDelete.Text = "Delete";
            this.ttpTestPanel.SetToolTip( this.btnDelete, "Delete it" );
            this.btnDelete.Click += new System.EventHandler( this.btnDelete_Click );
            // 
            // chkSkipCompleteCall
            // 
            this.chkSkipCompleteCall.AutoSize = true;
            this.chkSkipCompleteCall.Location = new System.Drawing.Point( 61, 24 );
            this.chkSkipCompleteCall.Name = "chkSkipCompleteCall";
            this.chkSkipCompleteCall.Size = new System.Drawing.Size( 120, 17 );
            this.chkSkipCompleteCall.TabIndex = 48;
            this.chkSkipCompleteCall.Text = "Skip completion call";
            this.ttpTestPanel.SetToolTip( this.chkSkipCompleteCall, "Checked for skiping completion call" );
            this.chkSkipCompleteCall.UseVisualStyleBackColor = true;
            // 
            // dtpArchive
            // 
            this.dtpArchive.CustomFormat = "ddd, dd MMM yyyy HH\':\'mm\':\'ss \'PST\'";
            this.dtpArchive.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpArchive.Location = new System.Drawing.Point( 64, 116 );
            this.dtpArchive.Name = "dtpArchive";
            this.dtpArchive.Size = new System.Drawing.Size( 200, 20 );
            this.dtpArchive.TabIndex = 28;
            this.dtpArchive.Value = new System.DateTime( 2008, 1, 21, 16, 20, 0, 0 );
            // 
            // txtZDK
            // 
            this.txtZDK.AllowDrop = true;
            this.txtZDK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZDK.Location = new System.Drawing.Point( 4, 220 );
            this.txtZDK.MaxLength = 65535;
            this.txtZDK.Multiline = true;
            this.txtZDK.Name = "txtZDK";
            this.txtZDK.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtZDK.Size = new System.Drawing.Size( 528, 112 );
            this.txtZDK.TabIndex = 31;
            // 
            // lblWsURL
            // 
            this.lblWsURL.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWsURL.Location = new System.Drawing.Point( 12, 188 );
            this.lblWsURL.Name = "lblWsURL";
            this.lblWsURL.Size = new System.Drawing.Size( 50, 16 );
            this.lblWsURL.TabIndex = 43;
            this.lblWsURL.Text = "WS URL";
            this.lblWsURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rdoDIME
            // 
            this.rdoDIME.AutoSize = true;
            this.rdoDIME.Checked = true;
            this.rdoDIME.Location = new System.Drawing.Point( 4, 9 );
            this.rdoDIME.Name = "rdoDIME";
            this.rdoDIME.Size = new System.Drawing.Size( 52, 17 );
            this.rdoDIME.TabIndex = 46;
            this.rdoDIME.TabStop = true;
            this.rdoDIME.Text = "DIME";
            this.rdoDIME.UseVisualStyleBackColor = true;
            this.rdoDIME.Click += new System.EventHandler( this.rdoDIME_Click );
            // 
            // rdoMIME
            // 
            this.rdoMIME.Location = new System.Drawing.Point( 4, 24 );
            this.rdoMIME.Name = "rdoMIME";
            this.rdoMIME.Size = new System.Drawing.Size( 53, 17 );
            this.rdoMIME.TabIndex = 47;
            this.rdoMIME.Text = "MIME";
            this.rdoMIME.UseVisualStyleBackColor = true;
            this.rdoMIME.Click += new System.EventHandler( this.rdoMIME_Click );
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.chkSkipCompleteCall );
            this.groupBox1.Controls.Add( this.rdoDIME );
            this.groupBox1.Controls.Add( this.rdoMIME );
            this.groupBox1.Location = new System.Drawing.Point( 332, 69 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 202, 45 );
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add( this.rdoString );
            this.groupBox2.Controls.Add( this.rdoFileName );
            this.groupBox2.Location = new System.Drawing.Point( 62, -4 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size( 200, 29 );
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            // 
            // lnkMdeFile
            // 
            this.lnkMdeFile.Location = new System.Drawing.Point( 4, 53 );
            this.lnkMdeFile.Name = "lnkMdeFile";
            this.lnkMdeFile.Size = new System.Drawing.Size( 56, 16 );
            this.lnkMdeFile.TabIndex = 52;
            this.lnkMdeFile.TabStop = true;
            this.lnkMdeFile.Text = "MDE File";
            this.lnkMdeFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpTestPanel.SetToolTip( this.lnkMdeFile, "Click for select MDE file" );
            this.lnkMdeFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkMdeFile_LinkClicked );
            // 
            // txtMdeFile
            // 
            this.txtMdeFile.Location = new System.Drawing.Point( 64, 51 );
            this.txtMdeFile.MaxLength = 65536;
            this.txtMdeFile.Name = "txtMdeFile";
            this.txtMdeFile.Size = new System.Drawing.Size( 200, 20 );
            this.txtMdeFile.TabIndex = 51;
            this.ttpTestPanel.SetToolTip( this.txtMdeFile, "Mail Content - max 65536 chars" );
            // 
            // chkMde
            // 
            this.chkMde.Checked = true;
            this.chkMde.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMde.Location = new System.Drawing.Point( 271, 54 );
            this.chkMde.Name = "chkMde";
            this.chkMde.Size = new System.Drawing.Size( 89, 17 );
            this.chkMde.TabIndex = 53;
            this.chkMde.Text = "MDE Update";
            this.chkMde.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ttpTestPanel.SetToolTip( this.chkMde, "Include MDE update call" );
            this.chkMde.UseVisualStyleBackColor = true;
            this.chkMde.Click += new System.EventHandler( this.chkMde_Click );
            // 
            // UcSimpleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.chkMde );
            this.Controls.Add( this.lnkMdeFile );
            this.Controls.Add( this.txtMdeFile );
            this.Controls.Add( this.groupBox2 );
            this.Controls.Add( this.groupBox1 );
            this.Controls.Add( this.btnDelete );
            this.Controls.Add( this.btnIsAlive );
            this.Controls.Add( this.cboWsUrl );
            this.Controls.Add( this.lblWsURL );
            this.Controls.Add( this.btnMdUpdate );
            this.Controls.Add( this.chkDllTest );
            this.Controls.Add( this.btnArchive );
            this.Controls.Add( this.btnRetrieve );
            this.Controls.Add( this.lblMailFrom );
            this.Controls.Add( this.lblDomainName );
            this.Controls.Add( this.lblDate );
            this.Controls.Add( this.lnkLabel );
            this.Controls.Add( this.txtZDK );
            this.Controls.Add( this.cboRcptTo );
            this.Controls.Add( this.cboMailFrom );
            this.Controls.Add( this.dtpArchive );
            this.Controls.Add( this.txtDataHandler );
            this.Controls.Add( this.cboDomainName );
            this.Controls.Add( this.lblRcptTo );
            this.Name = "UcSimpleTest";
            this.Size = new System.Drawing.Size( 537, 336 );
            this.Load += new System.EventHandler( this.UcSimpleTest_Load );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout( false );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRcptTo;
        private System.Windows.Forms.Label lblMailFrom;
        private System.Windows.Forms.Label lblDomainName;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.LinkLabel lnkLabel;
        private System.Windows.Forms.ToolTip ttpTestPanel;
        private System.Windows.Forms.RadioButton rdoFileName;
        private System.Windows.Forms.RadioButton rdoString;
        private System.Windows.Forms.ComboBox cboRcptTo;
        private System.Windows.Forms.ComboBox cboMailFrom;
        private System.Windows.Forms.DateTimePicker dtpArchive;
        private System.Windows.Forms.TextBox txtDataHandler;
        private System.Windows.Forms.ComboBox cboDomainName;
        private System.Windows.Forms.TextBox txtZDK;
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.Button btnArchive;
        private System.Windows.Forms.CheckBox chkDllTest;
        private System.Windows.Forms.Button btnMdUpdate;
        private System.Windows.Forms.Button btnIsAlive;
        private System.Windows.Forms.Label lblWsURL;
        private System.Windows.Forms.ComboBox cboWsUrl;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.RadioButton rdoDIME;
        private System.Windows.Forms.RadioButton rdoMIME;
        private System.Windows.Forms.CheckBox chkSkipCompleteCall;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel lnkMdeFile;
        private System.Windows.Forms.TextBox txtMdeFile;
        private System.Windows.Forms.CheckBox chkMde;

    }
}
