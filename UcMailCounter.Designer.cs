namespace WsClient
{
    partial class UcMailCounter
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
                if(countSpecialThread != null && countSpecialThread.IsAlive)
                {
                    this.KillCountThread();
                    commObj.LogToFile( "Thread.log", "   KillPstInFolderThread Killed" );
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( UcMailCounter ) );
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnCountIt = new System.Windows.Forms.Button();
            this.dtgResult = new System.Windows.Forms.DataGrid();
            this.dgContextMenuStrip = new System.Windows.Forms.ContextMenuStrip( this.components );
            this.btnCleanMe = new System.Windows.Forms.Button();
            this.rdoPstCount = new System.Windows.Forms.RadioButton();
            this.rdoNsfCount = new System.Windows.Forms.RadioButton();
            this.lnkFile = new System.Windows.Forms.LinkLabel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtTotalSize = new System.Windows.Forms.TextBox();
            this.lblTotalSize = new System.Windows.Forms.Label();
            this.txtTotalMail = new System.Windows.Forms.TextBox();
            this.lblTotalMail = new System.Windows.Forms.Label();
            this.cboProfile = new System.Windows.Forms.ComboBox();
            this.gbxInfo = new System.Windows.Forms.GroupBox();
            this.lblProfile = new System.Windows.Forms.Label();
            this.gbxRadioBtn = new System.Windows.Forms.GroupBox();
            this.ttpMailCounter = new System.Windows.Forms.ToolTip( this.components );
            this.gbxButton = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).BeginInit();
            this.gbxInfo.SuspendLayout();
            this.gbxRadioBtn.SuspendLayout();
            this.gbxButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAbort
            // 
            this.btnAbort.Image = ((System.Drawing.Image)(resources.GetObject( "btnAbort.Image" )));
            this.btnAbort.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnAbort.Location = new System.Drawing.Point( 194, 33 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 24, 24 );
            this.btnAbort.TabIndex = 108;
            this.ttpMailCounter.SetToolTip( this.btnAbort, "Abort" );
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // btnCountIt
            // 
            this.btnCountIt.Location = new System.Drawing.Point( 168, 10 );
            this.btnCountIt.Name = "btnCountIt";
            this.btnCountIt.Size = new System.Drawing.Size( 51, 22 );
            this.btnCountIt.TabIndex = 98;
            this.btnCountIt.Text = "Count";
            this.ttpMailCounter.SetToolTip( this.btnCountIt, "Do it" );
            this.btnCountIt.Click += new System.EventHandler( this.btnCountIt_Click );
            // 
            // dtgResult
            // 
            this.dtgResult.AlternatingBackColor = System.Drawing.Color.Gainsboro;
            this.dtgResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgResult.BackColor = System.Drawing.Color.Silver;
            this.dtgResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtgResult.CaptionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.dtgResult.CaptionFont = new System.Drawing.Font( "Tahoma", 8F );
            this.dtgResult.CaptionForeColor = System.Drawing.Color.White;
            this.dtgResult.ContextMenuStrip = this.dgContextMenuStrip;
            this.dtgResult.DataMember = "";
            this.dtgResult.FlatMode = true;
            this.dtgResult.ForeColor = System.Drawing.Color.Black;
            this.dtgResult.GridLineColor = System.Drawing.Color.White;
            this.dtgResult.HeaderBackColor = System.Drawing.Color.DarkGray;
            this.dtgResult.HeaderForeColor = System.Drawing.Color.Black;
            this.dtgResult.LinkColor = System.Drawing.Color.DarkSlateBlue;
            this.dtgResult.Location = new System.Drawing.Point( 2, 59 );
            this.dtgResult.Name = "dtgResult";
            this.dtgResult.ParentRowsBackColor = System.Drawing.Color.Black;
            this.dtgResult.ParentRowsForeColor = System.Drawing.Color.White;
            this.dtgResult.ReadOnly = true;
            this.dtgResult.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.dtgResult.SelectionForeColor = System.Drawing.Color.White;
            this.dtgResult.Size = new System.Drawing.Size( 650, 326 );
            this.dtgResult.TabIndex = 99;
            // 
            // dgContextMenuStrip
            // 
            this.dgContextMenuStrip.Name = "dgContextMenuStrip";
            this.dgContextMenuStrip.ShowImageMargin = false;
            this.dgContextMenuStrip.Size = new System.Drawing.Size( 36, 4 );
            // 
            // btnCleanMe
            // 
            this.btnCleanMe.Image = ((System.Drawing.Image)(resources.GetObject( "btnCleanMe.Image" )));
            this.btnCleanMe.Location = new System.Drawing.Point( 168, 33 );
            this.btnCleanMe.Name = "btnCleanMe";
            this.btnCleanMe.Size = new System.Drawing.Size( 24, 24 );
            this.btnCleanMe.TabIndex = 107;
            this.ttpMailCounter.SetToolTip( this.btnCleanMe, "Clean up PST in Outlook" );
            this.btnCleanMe.Click += new System.EventHandler( this.btnCleanMe_Click );
            // 
            // rdoPstCount
            // 
            this.rdoPstCount.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoPstCount.Checked = true;
            this.rdoPstCount.Location = new System.Drawing.Point( 4, 10 );
            this.rdoPstCount.Name = "rdoPstCount";
            this.rdoPstCount.Size = new System.Drawing.Size( 67, 22 );
            this.rdoPstCount.TabIndex = 109;
            this.rdoPstCount.TabStop = true;
            this.rdoPstCount.Text = "PST Count";
            this.ttpMailCounter.SetToolTip( this.rdoPstCount, "Count PST mail" );
            this.rdoPstCount.UseVisualStyleBackColor = true;
            this.rdoPstCount.Click += new System.EventHandler( this.rdoPstCount_Click );
            // 
            // rdoNsfCount
            // 
            this.rdoNsfCount.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoNsfCount.Location = new System.Drawing.Point( 4, 33 );
            this.rdoNsfCount.Name = "rdoNsfCount";
            this.rdoNsfCount.Size = new System.Drawing.Size( 67, 22 );
            this.rdoNsfCount.TabIndex = 110;
            this.rdoNsfCount.Text = "NSF Count";
            this.ttpMailCounter.SetToolTip( this.rdoNsfCount, "Count NSF mail" );
            this.rdoNsfCount.UseVisualStyleBackColor = true;
            this.rdoNsfCount.Click += new System.EventHandler( this.rdoNsfCount_Click );
            // 
            // lnkFile
            // 
            this.lnkFile.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.lnkFile.Location = new System.Drawing.Point( 6, 38 );
            this.lnkFile.Name = "lnkFile";
            this.lnkFile.Size = new System.Drawing.Size( 56, 16 );
            this.lnkFile.TabIndex = 96;
            this.lnkFile.TabStop = true;
            this.lnkFile.Text = "PST File:";
            this.lnkFile.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lnkFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkFile_LinkClicked );
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point( 205, 11 );
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '+';
            this.txtPassword.Size = new System.Drawing.Size( 128, 20 );
            this.txtPassword.TabIndex = 102;
            this.txtPassword.Text = "password0";
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point( 64, 35 );
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFileName.Size = new System.Drawing.Size( 269, 20 );
            this.txtFileName.TabIndex = 97;
            // 
            // txtTotalSize
            // 
            this.txtTotalSize.Location = new System.Drawing.Point( 69, 35 );
            this.txtTotalSize.Name = "txtTotalSize";
            this.txtTotalSize.Size = new System.Drawing.Size( 92, 20 );
            this.txtTotalSize.TabIndex = 105;
            this.txtTotalSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalSize
            // 
            this.lblTotalSize.Location = new System.Drawing.Point( 5, 38 );
            this.lblTotalSize.Name = "lblTotalSize";
            this.lblTotalSize.Size = new System.Drawing.Size( 60, 12 );
            this.lblTotalSize.TabIndex = 104;
            this.lblTotalSize.Text = "Total Size";
            this.lblTotalSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotalMail
            // 
            this.txtTotalMail.Location = new System.Drawing.Point( 69, 11 );
            this.txtTotalMail.Name = "txtTotalMail";
            this.txtTotalMail.Size = new System.Drawing.Size( 92, 20 );
            this.txtTotalMail.TabIndex = 106;
            this.txtTotalMail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalMail
            // 
            this.lblTotalMail.Location = new System.Drawing.Point( 5, 16 );
            this.lblTotalMail.Name = "lblTotalMail";
            this.lblTotalMail.Size = new System.Drawing.Size( 60, 12 );
            this.lblTotalMail.TabIndex = 103;
            this.lblTotalMail.Text = "Total Mails";
            this.lblTotalMail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboProfile
            // 
            this.cboProfile.Items.AddRange( new object[] {
            "Lithium"} );
            this.cboProfile.Location = new System.Drawing.Point( 64, 11 );
            this.cboProfile.Name = "cboProfile";
            this.cboProfile.Size = new System.Drawing.Size( 135, 21 );
            this.cboProfile.TabIndex = 101;
            this.cboProfile.Text = "pstProfile";
            // 
            // gbxInfo
            // 
            this.gbxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxInfo.Controls.Add( this.lblProfile );
            this.gbxInfo.Controls.Add( this.lnkFile );
            this.gbxInfo.Controls.Add( this.cboProfile );
            this.gbxInfo.Controls.Add( this.txtPassword );
            this.gbxInfo.Controls.Add( this.txtFileName );
            this.gbxInfo.Location = new System.Drawing.Point( 83, -3 );
            this.gbxInfo.Name = "gbxInfo";
            this.gbxInfo.Size = new System.Drawing.Size( 339, 59 );
            this.gbxInfo.TabIndex = 112;
            this.gbxInfo.TabStop = false;
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.Location = new System.Drawing.Point( 23, 15 );
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size( 36, 13 );
            this.lblProfile.TabIndex = 107;
            this.lblProfile.Text = "Profile";
            // 
            // gbxRadioBtn
            // 
            this.gbxRadioBtn.Controls.Add( this.rdoPstCount );
            this.gbxRadioBtn.Controls.Add( this.rdoNsfCount );
            this.gbxRadioBtn.Location = new System.Drawing.Point( 3, -2 );
            this.gbxRadioBtn.Name = "gbxRadioBtn";
            this.gbxRadioBtn.Size = new System.Drawing.Size( 75, 58 );
            this.gbxRadioBtn.TabIndex = 111;
            this.gbxRadioBtn.TabStop = false;
            // 
            // gbxButton
            // 
            this.gbxButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxButton.Controls.Add( this.btnCountIt );
            this.gbxButton.Controls.Add( this.btnCleanMe );
            this.gbxButton.Controls.Add( this.btnAbort );
            this.gbxButton.Controls.Add( this.txtTotalMail );
            this.gbxButton.Controls.Add( this.txtTotalSize );
            this.gbxButton.Controls.Add( this.lblTotalMail );
            this.gbxButton.Controls.Add( this.lblTotalSize );
            this.gbxButton.Location = new System.Drawing.Point( 428, -4 );
            this.gbxButton.Name = "gbxButton";
            this.gbxButton.Size = new System.Drawing.Size( 223, 60 );
            this.gbxButton.TabIndex = 113;
            this.gbxButton.TabStop = false;
            // 
            // UcMailCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.gbxButton );
            this.Controls.Add( this.gbxInfo );
            this.Controls.Add( this.gbxRadioBtn );
            this.Controls.Add( this.dtgResult );
            this.Name = "UcMailCounter";
            this.Size = new System.Drawing.Size( 655, 385 );
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).EndInit();
            this.gbxInfo.ResumeLayout( false );
            this.gbxInfo.PerformLayout();
            this.gbxRadioBtn.ResumeLayout( false );
            this.gbxButton.ResumeLayout( false );
            this.gbxButton.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnCountIt;
        private System.Windows.Forms.DataGrid dtgResult;
        private System.Windows.Forms.Button btnCleanMe;
        private System.Windows.Forms.RadioButton rdoPstCount;
        private System.Windows.Forms.RadioButton rdoNsfCount;
        private System.Windows.Forms.LinkLabel lnkFile;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.TextBox txtTotalSize;
        private System.Windows.Forms.Label lblTotalSize;
        private System.Windows.Forms.TextBox txtTotalMail;
        private System.Windows.Forms.Label lblTotalMail;
        private System.Windows.Forms.ComboBox cboProfile;
        private System.Windows.Forms.GroupBox gbxInfo;
        private System.Windows.Forms.ToolTip ttpMailCounter;
        private System.Windows.Forms.GroupBox gbxRadioBtn;
        private System.Windows.Forms.GroupBox gbxButton;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.ContextMenuStrip dgContextMenuStrip;
    }
}
