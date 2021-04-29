using System;
using System.Diagnostics;

namespace WsClient
{
    partial class UcRetrieve
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
                        KillRetrievalThread(ref m_thdList[i]);
                }//end of for
            }//end of try
            catch (Exception ex)
            {
                commObj.LogToFile("UcRetrieve.cs - btnAbort_Click " + ex.Message);
            }//end of catch

            if (disposing && (components != null))
            {
                Trace.WriteLine("UcRetrieve.Designer.cs - components deposing");
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFailCall = new System.Windows.Forms.TextBox();
            this.lblFailCall = new System.Windows.Forms.Label();
            this.cboDomainName = new System.Windows.Forms.ComboBox();
            this.lblDomainName = new System.Windows.Forms.Label();
            this.txtStoreFolder = new System.Windows.Forms.TextBox();
            this.txtKeyFile = new System.Windows.Forms.TextBox();
            this.lnkStoreLocation = new System.Windows.Forms.LinkLabel();
            this.lnkBrowseKeyFile = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkLogIntegrity = new System.Windows.Forms.CheckBox();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.lnkViewRetriLog = new System.Windows.Forms.LinkLabel();
            this.txtAveSize = new System.Windows.Forms.TextBox();
            this.lblAveSize = new System.Windows.Forms.Label();
            this.chkZAPIPerf = new System.Windows.Forms.CheckBox();
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
            this.ttpRetrieve = new System.Windows.Forms.ToolTip( this.components );
            this.rdoMIME = new System.Windows.Forms.RadioButton();
            this.rdoDIME = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.txtFailCall );
            this.groupBox1.Controls.Add( this.lblFailCall );
            this.groupBox1.Controls.Add( this.cboDomainName );
            this.groupBox1.Controls.Add( this.lblDomainName );
            this.groupBox1.Controls.Add( this.txtStoreFolder );
            this.groupBox1.Controls.Add( this.txtKeyFile );
            this.groupBox1.Controls.Add( this.lnkStoreLocation );
            this.groupBox1.Controls.Add( this.lnkBrowseKeyFile );
            this.groupBox1.Location = new System.Drawing.Point( 5, -1 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 251, 165 );
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtFailCall
            // 
            this.txtFailCall.Location = new System.Drawing.Point( 68, 79 );
            this.txtFailCall.Name = "txtFailCall";
            this.txtFailCall.ReadOnly = true;
            this.txtFailCall.Size = new System.Drawing.Size( 66, 20 );
            this.txtFailCall.TabIndex = 64;
            this.ttpRetrieve.SetToolTip( this.txtFailCall, "Attachment count <= 0" );
            // 
            // lblFailCall
            // 
            this.lblFailCall.Location = new System.Drawing.Point( 19, 81 );
            this.lblFailCall.Name = "lblFailCall";
            this.lblFailCall.Size = new System.Drawing.Size( 48, 16 );
            this.lblFailCall.TabIndex = 35;
            this.lblFailCall.Text = "Fail Call";
            this.lblFailCall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDomainName
            // 
            this.cboDomainName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDomainName.FormattingEnabled = true;
            this.cboDomainName.Location = new System.Drawing.Point( 69, 55 );
            this.cboDomainName.Name = "cboDomainName";
            this.cboDomainName.Size = new System.Drawing.Size( 176, 21 );
            this.cboDomainName.TabIndex = 33;
            this.cboDomainName.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboDomainName_KeyPress );
            this.cboDomainName.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboDomainName_KeyDown );
            // 
            // lblDomainName
            // 
            this.lblDomainName.Location = new System.Drawing.Point( 23, 59 );
            this.lblDomainName.Name = "lblDomainName";
            this.lblDomainName.Size = new System.Drawing.Size( 44, 16 );
            this.lblDomainName.TabIndex = 34;
            this.lblDomainName.Text = "Domain";
            this.lblDomainName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStoreFolder
            // 
            this.txtStoreFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStoreFolder.Location = new System.Drawing.Point( 69, 33 );
            this.txtStoreFolder.Name = "txtStoreFolder";
            this.txtStoreFolder.Size = new System.Drawing.Size( 176, 20 );
            this.txtStoreFolder.TabIndex = 3;
            this.ttpRetrieve.SetToolTip( this.txtStoreFolder, "Will generate folder per thread" );
            // 
            // txtKeyFile
            // 
            this.txtKeyFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyFile.Location = new System.Drawing.Point( 69, 11 );
            this.txtKeyFile.Name = "txtKeyFile";
            this.txtKeyFile.Size = new System.Drawing.Size( 176, 20 );
            this.txtKeyFile.TabIndex = 2;
            this.ttpRetrieve.SetToolTip( this.txtKeyFile, "Point to the file contain key info" );
            // 
            // lnkStoreLocation
            // 
            this.lnkStoreLocation.AutoSize = true;
            this.lnkStoreLocation.Location = new System.Drawing.Point( 3, 36 );
            this.lnkStoreLocation.Name = "lnkStoreLocation";
            this.lnkStoreLocation.Size = new System.Drawing.Size( 64, 13 );
            this.lnkStoreLocation.TabIndex = 1;
            this.lnkStoreLocation.TabStop = true;
            this.lnkStoreLocation.Text = "Store Folder";
            this.lnkStoreLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpRetrieve.SetToolTip( this.lnkStoreLocation, "Store location for retrieved file" );
            this.lnkStoreLocation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkStoreLocation_LinkClicked );
            // 
            // lnkBrowseKeyFile
            // 
            this.lnkBrowseKeyFile.AutoSize = true;
            this.lnkBrowseKeyFile.Location = new System.Drawing.Point( 18, 14 );
            this.lnkBrowseKeyFile.Name = "lnkBrowseKeyFile";
            this.lnkBrowseKeyFile.Size = new System.Drawing.Size( 49, 13 );
            this.lnkBrowseKeyFile.TabIndex = 0;
            this.lnkBrowseKeyFile.TabStop = true;
            this.lnkBrowseKeyFile.Text = "zKey File";
            this.lnkBrowseKeyFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpRetrieve.SetToolTip( this.lnkBrowseKeyFile, "Set the zKey file" );
            this.lnkBrowseKeyFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkBrowseKeyFile_LinkClicked );
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add( this.rdoMIME );
            this.groupBox2.Controls.Add( this.rdoDIME );
            this.groupBox2.Controls.Add( this.chkLogIntegrity );
            this.groupBox2.Controls.Add( this.btnAbort );
            this.groupBox2.Controls.Add( this.btnRetrieve );
            this.groupBox2.Controls.Add( this.lnkViewRetriLog );
            this.groupBox2.Controls.Add( this.txtAveSize );
            this.groupBox2.Controls.Add( this.lblAveSize );
            this.groupBox2.Controls.Add( this.chkZAPIPerf );
            this.groupBox2.Controls.Add( this.txtMailsSize );
            this.groupBox2.Controls.Add( this.lblTotalMailsSize );
            this.groupBox2.Controls.Add( this.txtMailPerSec );
            this.groupBox2.Controls.Add( this.lblMailPerSec );
            this.groupBox2.Controls.Add( this.txtDuration );
            this.groupBox2.Controls.Add( this.lblDuration );
            this.groupBox2.Controls.Add( this.txtNumMail );
            this.groupBox2.Controls.Add( this.lblMailStore );
            this.groupBox2.Controls.Add( this.txtEndTime );
            this.groupBox2.Controls.Add( this.lblEndTime );
            this.groupBox2.Controls.Add( this.txtStartTime );
            this.groupBox2.Controls.Add( this.lblStartTime );
            this.groupBox2.Controls.Add( this.nudThread );
            this.groupBox2.Controls.Add( this.nudDelay );
            this.groupBox2.Controls.Add( this.lblThread );
            this.groupBox2.Controls.Add( this.lblDelay );
            this.groupBox2.Location = new System.Drawing.Point( 257, -2 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size( 277, 166 );
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // chkLogIntegrity
            // 
            this.chkLogIntegrity.AutoSize = true;
            this.chkLogIntegrity.Location = new System.Drawing.Point( 9, 118 );
            this.chkLogIntegrity.Name = "chkLogIntegrity";
            this.chkLogIntegrity.Size = new System.Drawing.Size( 84, 17 );
            this.chkLogIntegrity.TabIndex = 67;
            this.chkLogIntegrity.Text = "Log Integrity";
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point( 207, 138 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 64, 21 );
            this.btnAbort.TabIndex = 56;
            this.btnAbort.Text = "Abort";
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point( 137, 138 );
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size( 64, 21 );
            this.btnRetrieve.TabIndex = 55;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.Click += new System.EventHandler( this.btnRetrieve_Click );
            // 
            // lnkViewRetriLog
            // 
            this.lnkViewRetriLog.AutoSize = true;
            this.lnkViewRetriLog.Location = new System.Drawing.Point( 180, 122 );
            this.lnkViewRetriLog.Name = "lnkViewRetriLog";
            this.lnkViewRetriLog.Size = new System.Drawing.Size( 70, 13 );
            this.lnkViewRetriLog.TabIndex = 66;
            this.lnkViewRetriLog.TabStop = true;
            this.lnkViewRetriLog.Text = "Retrieval Log";
            this.lnkViewRetriLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpRetrieve.SetToolTip( this.lnkViewRetriLog, "View retrieval Log" );
            this.lnkViewRetriLog.Click += new System.EventHandler( this.lnkViewRetriLog_Click );
            // 
            // txtAveSize
            // 
            this.txtAveSize.Location = new System.Drawing.Point( 60, 77 );
            this.txtAveSize.Name = "txtAveSize";
            this.txtAveSize.ReadOnly = true;
            this.txtAveSize.Size = new System.Drawing.Size( 66, 20 );
            this.txtAveSize.TabIndex = 65;
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
            // chkZAPIPerf
            // 
            this.chkZAPIPerf.AutoSize = true;
            this.chkZAPIPerf.Location = new System.Drawing.Point( 9, 101 );
            this.chkZAPIPerf.Name = "chkZAPIPerf";
            this.chkZAPIPerf.Size = new System.Drawing.Size( 108, 17 );
            this.chkZAPIPerf.TabIndex = 61;
            this.chkZAPIPerf.Text = "ZAPI Timing Test";
            this.chkZAPIPerf.CheckedChanged += new System.EventHandler( this.chkZAPIPerf_CheckedChanged );
            // 
            // txtMailsSize
            // 
            this.txtMailsSize.Location = new System.Drawing.Point( 60, 55 );
            this.txtMailsSize.Name = "txtMailsSize";
            this.txtMailsSize.ReadOnly = true;
            this.txtMailsSize.Size = new System.Drawing.Size( 66, 20 );
            this.txtMailsSize.TabIndex = 63;
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
            this.ttpRetrieve.SetToolTip( this.txtDuration, "Process time" );
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
            this.ttpRetrieve.SetToolTip( this.txtNumMail, "Number of retrieved mails" );
            // 
            // lblMailStore
            // 
            this.lblMailStore.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMailStore.Location = new System.Drawing.Point( 131, 79 );
            this.lblMailStore.Name = "lblMailStore";
            this.lblMailStore.Size = new System.Drawing.Size( 53, 17 );
            this.lblMailStore.TabIndex = 53;
            this.lblMailStore.Text = "Got Mails";
            this.lblMailStore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpRetrieve.SetToolTip( this.lblMailStore, "Number of retrieved mails" );
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
            this.txtEndTime.TextChanged += new System.EventHandler( this.txtEndTime_TextChanged );
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
            this.nudThread.Size = new System.Drawing.Size( 66, 20 );
            this.nudThread.TabIndex = 48;
            this.nudThread.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // rdoMIME
            // 
            this.rdoMIME.Location = new System.Drawing.Point( 79, 148 );
            this.rdoMIME.Name = "rdoMIME";
            this.rdoMIME.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rdoMIME.Size = new System.Drawing.Size( 53, 16 );
            this.rdoMIME.TabIndex = 69;
            this.rdoMIME.Text = "MIME";
            this.rdoMIME.UseVisualStyleBackColor = true;
            // 
            // rdoDIME
            // 
            this.rdoDIME.Checked = true;
            this.rdoDIME.Location = new System.Drawing.Point( 80, 133 );
            this.rdoDIME.Name = "rdoDIME";
            this.rdoDIME.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rdoDIME.Size = new System.Drawing.Size( 52, 15 );
            this.rdoDIME.TabIndex = 68;
            this.rdoDIME.TabStop = true;
            this.rdoDIME.Text = "DIME";
            this.rdoDIME.UseVisualStyleBackColor = true;
            // 
            // UcRetrieve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.groupBox2 );
            this.Controls.Add( this.groupBox1 );
            this.Name = "UcRetrieve";
            this.Size = new System.Drawing.Size( 537, 336 );
            this.Load += new System.EventHandler( this.UcRetrieve_Load );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout( false );
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel lnkBrowseKeyFile;
        private System.Windows.Forms.ToolTip ttpRetrieve;
        private System.Windows.Forms.LinkLabel lnkStoreLocation;
        private System.Windows.Forms.TextBox txtStoreFolder;
        private System.Windows.Forms.TextBox txtKeyFile;
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
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.ComboBox cboDomainName;
        private System.Windows.Forms.Label lblDomainName;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.TextBox txtMailPerSec;
        private System.Windows.Forms.Label lblMailPerSec;
        private System.Windows.Forms.TextBox txtAveSize;
        private System.Windows.Forms.Label lblAveSize;
        private System.Windows.Forms.CheckBox chkZAPIPerf;
        private System.Windows.Forms.TextBox txtMailsSize;
        private System.Windows.Forms.Label lblTotalMailsSize;
        private System.Windows.Forms.Label lblFailCall;
        private System.Windows.Forms.TextBox txtFailCall;
        private System.Windows.Forms.LinkLabel lnkViewRetriLog;
        private System.Windows.Forms.CheckBox chkLogIntegrity;
        private System.Windows.Forms.RadioButton rdoMIME;
        private System.Windows.Forms.RadioButton rdoDIME;
    }
}
