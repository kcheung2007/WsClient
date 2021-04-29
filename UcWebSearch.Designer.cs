namespace WsClient
{
    partial class UcWebSearch
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
            if(disposing && (components != null))
            {
                if(m_thdWebSearch != null && m_thdWebSearch.IsAlive)
                {
                    this.KillWebSearchThread();
                    commObj.LogToFile( "Thread.log", "   KillMdeCheckThread Killed" );
                }
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.cboURL = new System.Windows.Forms.ComboBox();
            this.cboSearchKey = new System.Windows.Forms.ComboBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.lblSearchKey = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.cboPassword = new System.Windows.Forms.ComboBox();
            this.cboLoginID = new System.Windows.Forms.ComboBox();
            this.lblLoginID = new System.Windows.Forms.Label();
            this.lblFail = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtFail = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkInFile = new System.Windows.Forms.LinkLabel();
            this.chkCloseIE = new System.Windows.Forms.CheckBox();
            this.lnkViewLog = new System.Windows.Forms.LinkLabel();
            this.chkLogDetail = new System.Windows.Forms.CheckBox();
            this.cboInFile = new System.Windows.Forms.ComboBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoAttachment = new System.Windows.Forms.RadioButton();
            this.rdoBody = new System.Windows.Forms.RadioButton();
            this.rdoSubject = new System.Windows.Forms.RadioButton();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.ttpWebSearch = new System.Windows.Forms.ToolTip( this.components );
            this.cboRepository = new System.Windows.Forms.ComboBox();
            this.lblRepository = new System.Windows.Forms.Label();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.lblLang = new System.Windows.Forms.Label();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point( 470, 2 );
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size( 75, 21 );
            this.btnTest.TabIndex = 35;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler( this.btnTest_Click );
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point( 463, 37 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 75, 21 );
            this.btnAbort.TabIndex = 26;
            this.btnAbort.Text = "Abort";
            this.ttpWebSearch.SetToolTip( this.btnAbort, "Abort - wait for thread finish" );
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point( 463, 13 );
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size( 75, 21 );
            this.btnRun.TabIndex = 25;
            this.btnRun.Text = "Run";
            this.ttpWebSearch.SetToolTip( this.btnRun, "Search based on file input" );
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler( this.btnRun_Click );
            // 
            // cboURL
            // 
            this.cboURL.FormattingEnabled = true;
            this.cboURL.Location = new System.Drawing.Point( 56, 49 );
            this.cboURL.Name = "cboURL";
            this.cboURL.Size = new System.Drawing.Size( 205, 21 );
            this.cboURL.TabIndex = 34;
            this.cboURL.Text = "http://10.1.41.51/index_js.html";
            this.ttpWebSearch.SetToolTip( this.cboURL, "DS URL" );
            this.cboURL.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboURL_KeyPress );
            this.cboURL.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboURL_KeyDown );
            // 
            // cboSearchKey
            // 
            this.cboSearchKey.FormattingEnabled = true;
            this.cboSearchKey.Location = new System.Drawing.Point( 329, 26 );
            this.cboSearchKey.Name = "cboSearchKey";
            this.cboSearchKey.Size = new System.Drawing.Size( 130, 21 );
            this.cboSearchKey.TabIndex = 33;
            this.ttpWebSearch.SetToolTip( this.cboSearchKey, "Subject Search Key\r\nFor Testing Purpose" );
            this.cboSearchKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboSearchKey_KeyPress );
            this.cboSearchKey.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboSearchKey_KeyDown );
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point( 26, 51 );
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size( 29, 13 );
            this.lblURL.TabIndex = 32;
            this.lblURL.Text = "URL";
            // 
            // lblSearchKey
            // 
            this.lblSearchKey.AutoSize = true;
            this.lblSearchKey.Location = new System.Drawing.Point( 265, 30 );
            this.lblSearchKey.Name = "lblSearchKey";
            this.lblSearchKey.Size = new System.Drawing.Size( 62, 13 );
            this.lblSearchKey.TabIndex = 31;
            this.lblSearchKey.Text = "Search Key";
            this.ttpWebSearch.SetToolTip( this.lblSearchKey, "Subject Only" );
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point( 273, 7 );
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size( 53, 13 );
            this.lblPassword.TabIndex = 30;
            this.lblPassword.Text = "Password";
            // 
            // cboPassword
            // 
            this.cboPassword.FormattingEnabled = true;
            this.cboPassword.Location = new System.Drawing.Point( 329, 2 );
            this.cboPassword.Name = "cboPassword";
            this.cboPassword.Size = new System.Drawing.Size( 130, 21 );
            this.cboPassword.TabIndex = 29;
            this.cboPassword.Text = "skyline";
            this.ttpWebSearch.SetToolTip( this.cboPassword, "Password" );
            this.cboPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboPassword_KeyPress );
            this.cboPassword.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboPassword_KeyDown );
            // 
            // cboLoginID
            // 
            this.cboLoginID.FormattingEnabled = true;
            this.cboLoginID.Location = new System.Drawing.Point( 56, 2 );
            this.cboLoginID.Name = "cboLoginID";
            this.cboLoginID.Size = new System.Drawing.Size( 205, 21 );
            this.cboLoginID.TabIndex = 28;
            this.ttpWebSearch.SetToolTip( this.cboLoginID, "DS Login ID" );
            this.cboLoginID.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboLoginID_KeyPress );
            this.cboLoginID.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboLoginID_KeyDown );
            // 
            // lblLoginID
            // 
            this.lblLoginID.AutoSize = true;
            this.lblLoginID.Location = new System.Drawing.Point( 9, 7 );
            this.lblLoginID.Name = "lblLoginID";
            this.lblLoginID.Size = new System.Drawing.Size( 47, 13 );
            this.lblLoginID.TabIndex = 27;
            this.lblLoginID.Text = "Login ID";
            this.lblLoginID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpWebSearch.SetToolTip( this.lblLoginID, "Login ID" );
            // 
            // lblFail
            // 
            this.lblFail.AutoSize = true;
            this.lblFail.Location = new System.Drawing.Point( 101, 44 );
            this.lblFail.Name = "lblFail";
            this.lblFail.Size = new System.Drawing.Size( 26, 13 );
            this.lblFail.TabIndex = 23;
            this.lblFail.Text = "Fail:";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point( 12, 44 );
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size( 33, 13 );
            this.lblPass.TabIndex = 22;
            this.lblPass.Text = "Pass:";
            // 
            // txtFail
            // 
            this.txtFail.Location = new System.Drawing.Point( 129, 41 );
            this.txtFail.Name = "txtFail";
            this.txtFail.ReadOnly = true;
            this.txtFail.Size = new System.Drawing.Size( 53, 20 );
            this.txtFail.TabIndex = 20;
            this.txtFail.Text = "0";
            this.txtFail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point( 46, 41 );
            this.txtPass.Name = "txtPass";
            this.txtPass.ReadOnly = true;
            this.txtPass.Size = new System.Drawing.Size( 53, 20 );
            this.txtPass.TabIndex = 19;
            this.txtPass.Text = "0";
            this.txtPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.lnkInFile );
            this.groupBox1.Controls.Add( this.chkCloseIE );
            this.groupBox1.Controls.Add( this.lnkViewLog );
            this.groupBox1.Controls.Add( this.chkLogDetail );
            this.groupBox1.Controls.Add( this.cboInFile );
            this.groupBox1.Controls.Add( this.txtPass );
            this.groupBox1.Controls.Add( this.txtFail );
            this.groupBox1.Controls.Add( this.btnAbort );
            this.groupBox1.Controls.Add( this.btnRun );
            this.groupBox1.Controls.Add( this.txtTotal );
            this.groupBox1.Controls.Add( this.lblPass );
            this.groupBox1.Controls.Add( this.lblFail );
            this.groupBox1.Controls.Add( this.lblTotal );
            this.groupBox1.Location = new System.Drawing.Point( 3, 93 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 549, 67 );
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            // 
            // lnkInFile
            // 
            this.lnkInFile.AutoSize = true;
            this.lnkInFile.Location = new System.Drawing.Point( 11, 18 );
            this.lnkInFile.Name = "lnkInFile";
            this.lnkInFile.Size = new System.Drawing.Size( 35, 13 );
            this.lnkInFile.TabIndex = 42;
            this.lnkInFile.TabStop = true;
            this.lnkInFile.Text = "In File";
            this.lnkInFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkInFile_LinkClicked );
            // 
            // chkCloseIE
            // 
            this.chkCloseIE.AutoSize = true;
            this.chkCloseIE.Checked = true;
            this.chkCloseIE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCloseIE.Location = new System.Drawing.Point( 315, 17 );
            this.chkCloseIE.Name = "chkCloseIE";
            this.chkCloseIE.Size = new System.Drawing.Size( 65, 17 );
            this.chkCloseIE.TabIndex = 41;
            this.chkCloseIE.Text = "Close IE";
            this.chkCloseIE.UseVisualStyleBackColor = true;
            // 
            // lnkViewLog
            // 
            this.lnkViewLog.AutoSize = true;
            this.lnkViewLog.Location = new System.Drawing.Point( 368, 41 );
            this.lnkViewLog.Name = "lnkViewLog";
            this.lnkViewLog.Size = new System.Drawing.Size( 91, 13 );
            this.lnkViewLog.TabIndex = 40;
            this.lnkViewLog.TabStop = true;
            this.lnkViewLog.Text = "Search Result log";
            this.lnkViewLog.Click += new System.EventHandler( this.lnkViewLog_Click );
            // 
            // chkLogDetail
            // 
            this.chkLogDetail.AutoSize = true;
            this.chkLogDetail.Checked = true;
            this.chkLogDetail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLogDetail.Location = new System.Drawing.Point( 386, 17 );
            this.chkLogDetail.Name = "chkLogDetail";
            this.chkLogDetail.Size = new System.Drawing.Size( 74, 17 );
            this.chkLogDetail.TabIndex = 39;
            this.chkLogDetail.Text = "Log Detail";
            this.chkLogDetail.UseVisualStyleBackColor = true;
            // 
            // cboInFile
            // 
            this.cboInFile.FormattingEnabled = true;
            this.cboInFile.Location = new System.Drawing.Point( 48, 15 );
            this.cboInFile.Name = "cboInFile";
            this.cboInFile.Size = new System.Drawing.Size( 210, 21 );
            this.cboInFile.TabIndex = 38;
            this.ttpWebSearch.SetToolTip( this.cboInFile, "Inpurt file contains a list of search key" );
            this.cboInFile.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboInFile_KeyPress );
            this.cboInFile.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboInFile_KeyDown );
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point( 218, 41 );
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size( 53, 20 );
            this.txtTotal.TabIndex = 21;
            this.txtTotal.Text = "0";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTotal.Location = new System.Drawing.Point( 183, 44 );
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size( 34, 13 );
            this.lblTotal.TabIndex = 24;
            this.lblTotal.Text = "Total:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add( this.rdoAttachment );
            this.groupBox2.Controls.Add( this.rdoBody );
            this.groupBox2.Controls.Add( this.rdoSubject );
            this.groupBox2.Location = new System.Drawing.Point( 464, 22 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size( 86, 72 );
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            // 
            // rdoAttachment
            // 
            this.rdoAttachment.AutoSize = true;
            this.rdoAttachment.Location = new System.Drawing.Point( 5, 49 );
            this.rdoAttachment.Name = "rdoAttachment";
            this.rdoAttachment.Size = new System.Drawing.Size( 79, 17 );
            this.rdoAttachment.TabIndex = 2;
            this.rdoAttachment.Text = "Attachment";
            this.ttpWebSearch.SetToolTip( this.rdoAttachment, "Attachment Search" );
            this.rdoAttachment.UseVisualStyleBackColor = true;
            // 
            // rdoBody
            // 
            this.rdoBody.AutoSize = true;
            this.rdoBody.Location = new System.Drawing.Point( 5, 31 );
            this.rdoBody.Name = "rdoBody";
            this.rdoBody.Size = new System.Drawing.Size( 49, 17 );
            this.rdoBody.TabIndex = 1;
            this.rdoBody.Text = "Body";
            this.ttpWebSearch.SetToolTip( this.rdoBody, "Body search" );
            this.rdoBody.UseVisualStyleBackColor = true;
            // 
            // rdoSubject
            // 
            this.rdoSubject.AutoSize = true;
            this.rdoSubject.Checked = true;
            this.rdoSubject.Location = new System.Drawing.Point( 5, 13 );
            this.rdoSubject.Name = "rdoSubject";
            this.rdoSubject.Size = new System.Drawing.Size( 61, 17 );
            this.rdoSubject.TabIndex = 0;
            this.rdoSubject.TabStop = true;
            this.rdoSubject.Text = "Subject";
            this.ttpWebSearch.SetToolTip( this.rdoSubject, "Subject search" );
            this.rdoSubject.UseVisualStyleBackColor = true;
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point( 329, 72 );
            this.dtpTo.MinDate = new System.DateTime( 1970, 1, 1, 0, 0, 0, 0 );
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size( 130, 20 );
            this.dtpTo.TabIndex = 44;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point( 329, 49 );
            this.dtpFrom.MinDate = new System.DateTime( 1970, 1, 1, 0, 0, 0, 0 );
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size( 130, 20 );
            this.dtpFrom.TabIndex = 43;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point( 304, 76 );
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size( 20, 13 );
            this.lblTo.TabIndex = 42;
            this.lblTo.Text = "To";
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point( 296, 53 );
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size( 30, 13 );
            this.lblFromDate.TabIndex = 41;
            this.lblFromDate.Text = "From";
            // 
            // cboRepository
            // 
            this.cboRepository.FormattingEnabled = true;
            this.cboRepository.Location = new System.Drawing.Point( 56, 26 );
            this.cboRepository.Name = "cboRepository";
            this.cboRepository.Size = new System.Drawing.Size( 205, 21 );
            this.cboRepository.TabIndex = 38;
            this.ttpWebSearch.SetToolTip( this.cboRepository, "DS Login ID" );
            this.cboRepository.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboRepository_KeyPress );
            this.cboRepository.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboRepository_KeyDown );
            // 
            // lblRepository
            // 
            this.lblRepository.AutoSize = true;
            this.lblRepository.Location = new System.Drawing.Point( 0, 29 );
            this.lblRepository.Name = "lblRepository";
            this.lblRepository.Size = new System.Drawing.Size( 57, 13 );
            this.lblRepository.TabIndex = 37;
            this.lblRepository.Text = "Repository";
            this.lblRepository.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpWebSearch.SetToolTip( this.lblRepository, "Login ID" );
            // 
            // cboLanguage
            // 
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point( 56, 72 );
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size( 205, 21 );
            this.cboLanguage.TabIndex = 46;
            this.ttpWebSearch.SetToolTip( this.cboLanguage, "DS Login ID" );
            this.cboLanguage.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboLanguage_KeyPress );
            this.cboLanguage.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboLanguage_KeyDown );
            // 
            // lblLang
            // 
            this.lblLang.AutoSize = true;
            this.lblLang.Location = new System.Drawing.Point( 0, 75 );
            this.lblLang.Name = "lblLang";
            this.lblLang.Size = new System.Drawing.Size( 55, 13 );
            this.lblLang.TabIndex = 45;
            this.lblLang.Text = "Language";
            this.lblLang.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpWebSearch.SetToolTip( this.lblLang, "Login ID" );
            // 
            // txtDisplay
            // 
            this.txtDisplay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtDisplay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtDisplay.Location = new System.Drawing.Point( 0, 166 );
            this.txtDisplay.MaxLength = 65534;
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDisplay.Size = new System.Drawing.Size( 555, 149 );
            this.txtDisplay.TabIndex = 47;
            // 
            // UcWebSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.txtDisplay );
            this.Controls.Add( this.groupBox2 );
            this.Controls.Add( this.cboLanguage );
            this.Controls.Add( this.lblLang );
            this.Controls.Add( this.dtpTo );
            this.Controls.Add( this.dtpFrom );
            this.Controls.Add( this.cboRepository );
            this.Controls.Add( this.lblTo );
            this.Controls.Add( this.lblRepository );
            this.Controls.Add( this.lblFromDate );
            this.Controls.Add( this.btnTest );
            this.Controls.Add( this.cboURL );
            this.Controls.Add( this.cboSearchKey );
            this.Controls.Add( this.lblURL );
            this.Controls.Add( this.lblSearchKey );
            this.Controls.Add( this.lblPassword );
            this.Controls.Add( this.cboPassword );
            this.Controls.Add( this.cboLoginID );
            this.Controls.Add( this.lblLoginID );
            this.Controls.Add( this.groupBox1 );
            this.Name = "UcWebSearch";
            this.Size = new System.Drawing.Size( 555, 315 );
            this.Load += new System.EventHandler( this.UcWebSearch_Load );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout( false );
            this.groupBox2.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ComboBox cboURL;
        private System.Windows.Forms.ComboBox cboSearchKey;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.Label lblSearchKey;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.ComboBox cboPassword;
        private System.Windows.Forms.ComboBox cboLoginID;
        private System.Windows.Forms.Label lblLoginID;
        private System.Windows.Forms.Label lblFail;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtFail;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkLogDetail;
        private System.Windows.Forms.ComboBox cboInFile;
        private System.Windows.Forms.ToolTip ttpWebSearch;
        private System.Windows.Forms.LinkLabel lnkViewLog;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoAttachment;
        private System.Windows.Forms.RadioButton rdoBody;
        private System.Windows.Forms.RadioButton rdoSubject;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.ComboBox cboRepository;
        private System.Windows.Forms.Label lblRepository;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.Label lblLang;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.CheckBox chkCloseIE;
        private System.Windows.Forms.LinkLabel lnkInFile;
    }
}
