using System;
using System.Diagnostics;

namespace WsClient
{
    partial class UcZAPIs
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
                        KillZApiThread(ref m_thdList[i]);
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
            this.cboDomainName = new System.Windows.Forms.ComboBox();
            this.lblDomain = new System.Windows.Forms.Label();
            this.lnkZdkFile = new System.Windows.Forms.LinkLabel();
            this.cboZdkFile = new System.Windows.Forms.ComboBox();
            this.gbxCommon = new System.Windows.Forms.GroupBox();
            this.ttpUcZAPI = new System.Windows.Forms.ToolTip( this.components );
            this.zApiTabCtrl = new System.Windows.Forms.TabControl();
            this.tabPgZAPIs = new System.Windows.Forms.TabPage();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnMDEUpdate = new System.Windows.Forms.Button();
            this.cboMdeFolder = new System.Windows.Forms.ComboBox();
            this.lnkMdeFolder = new System.Windows.Forms.LinkLabel();
            this.tabPgDelete = new System.Windows.Forms.TabPage();
            this.lblNumCall = new System.Windows.Forms.Label();
            this.btnAbortAll = new System.Windows.Forms.Button();
            this.gbxMsiControl = new System.Windows.Forms.GroupBox();
            this.chkZAPIPerf = new System.Windows.Forms.CheckBox();
            this.txtFailCall = new System.Windows.Forms.TextBox();
            this.lblFailCall = new System.Windows.Forms.Label();
            this.txtCallPerSec = new System.Windows.Forms.TextBox();
            this.lblCallPerSec = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.txtNumCall = new System.Windows.Forms.TextBox();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.nudThread = new System.Windows.Forms.NumericUpDown();
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.lblThread = new System.Windows.Forms.Label();
            this.lblDelay = new System.Windows.Forms.Label();
            this.gbxCommon.SuspendLayout();
            this.zApiTabCtrl.SuspendLayout();
            this.tabPgZAPIs.SuspendLayout();
            this.gbxMsiControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // cboDomainName
            // 
            this.cboDomainName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDomainName.FormattingEnabled = true;
            this.cboDomainName.Items.AddRange( new object[] {
            "testdomain1",
            "testdomain2",
            "zdemo"} );
            this.cboDomainName.Location = new System.Drawing.Point( 58, 33 );
            this.cboDomainName.Name = "cboDomainName";
            this.cboDomainName.Size = new System.Drawing.Size( 210, 21 );
            this.cboDomainName.TabIndex = 43;
            this.cboDomainName.Text = "testdomain2";
            this.ttpUcZAPI.SetToolTip( this.cboDomainName, "WS Domain\r\nHit Enter to save current value" );
            this.cboDomainName.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboDomainName_KeyPress );
            this.cboDomainName.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboDomainName_KeyDown );
            // 
            // lblDomain
            // 
            this.lblDomain.Location = new System.Drawing.Point( 10, 35 );
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size( 44, 16 );
            this.lblDomain.TabIndex = 40;
            this.lblDomain.Text = "Domain";
            this.lblDomain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkZdkFile
            // 
            this.lnkZdkFile.AutoSize = true;
            this.lnkZdkFile.Location = new System.Drawing.Point( 6, 13 );
            this.lnkZdkFile.Name = "lnkZdkFile";
            this.lnkZdkFile.Size = new System.Drawing.Size( 49, 13 );
            this.lnkZdkFile.TabIndex = 37;
            this.lnkZdkFile.TabStop = true;
            this.lnkZdkFile.Text = "zKey File";
            this.lnkZdkFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpUcZAPI.SetToolTip( this.lnkZdkFile, "Browse the zdk file" );
            this.lnkZdkFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkZdkFile_LinkClicked );
            // 
            // cboZdkFile
            // 
            this.cboZdkFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboZdkFile.FormattingEnabled = true;
            this.cboZdkFile.Location = new System.Drawing.Point( 58, 10 );
            this.cboZdkFile.Name = "cboZdkFile";
            this.cboZdkFile.Size = new System.Drawing.Size( 210, 21 );
            this.cboZdkFile.TabIndex = 41;
            this.ttpUcZAPI.SetToolTip( this.cboZdkFile, "zdk file location\r\nHit Enter to save current value" );
            this.cboZdkFile.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboZdkFile_KeyPress );
            this.cboZdkFile.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboZdkFile_KeyDown );
            // 
            // gbxCommon
            // 
            this.gbxCommon.Controls.Add( this.cboZdkFile );
            this.gbxCommon.Controls.Add( this.cboDomainName );
            this.gbxCommon.Controls.Add( this.lblDomain );
            this.gbxCommon.Controls.Add( this.lnkZdkFile );
            this.gbxCommon.Location = new System.Drawing.Point( 4, 4 );
            this.gbxCommon.Name = "gbxCommon";
            this.gbxCommon.Size = new System.Drawing.Size( 274, 59 );
            this.gbxCommon.TabIndex = 42;
            this.gbxCommon.TabStop = false;
            // 
            // zApiTabCtrl
            // 
            this.zApiTabCtrl.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.zApiTabCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zApiTabCtrl.Controls.Add( this.tabPgZAPIs );
            this.zApiTabCtrl.Controls.Add( this.tabPgDelete );
            this.zApiTabCtrl.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.zApiTabCtrl.Location = new System.Drawing.Point( 284, 4 );
            this.zApiTabCtrl.Multiline = true;
            this.zApiTabCtrl.Name = "zApiTabCtrl";
            this.zApiTabCtrl.SelectedIndex = 0;
            this.zApiTabCtrl.ShowToolTips = true;
            this.zApiTabCtrl.Size = new System.Drawing.Size( 257, 267 );
            this.zApiTabCtrl.TabIndex = 43;
            this.ttpUcZAPI.SetToolTip( this.zApiTabCtrl, "Whatever" );
            // 
            // tabPgZAPIs
            // 
            this.tabPgZAPIs.Controls.Add( this.btnDelete );
            this.tabPgZAPIs.Controls.Add( this.btnMDEUpdate );
            this.tabPgZAPIs.Controls.Add( this.cboMdeFolder );
            this.tabPgZAPIs.Controls.Add( this.lnkMdeFolder );
            this.tabPgZAPIs.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.tabPgZAPIs.Location = new System.Drawing.Point( 4, 4 );
            this.tabPgZAPIs.Name = "tabPgZAPIs";
            this.tabPgZAPIs.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPgZAPIs.Size = new System.Drawing.Size( 230, 259 );
            this.tabPgZAPIs.TabIndex = 0;
            this.tabPgZAPIs.Text = "ZAPIs";
            this.ttpUcZAPI.SetToolTip( this.tabPgZAPIs, "ZAPIs" );
            this.tabPgZAPIs.ToolTipText = "Another whatever";
            this.tabPgZAPIs.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point( 174, 26 );
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size( 53, 21 );
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.ttpUcZAPI.SetToolTip( this.btnDelete, "Delete based on ZDK file" );
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler( this.btnDelete_Click );
            // 
            // btnMDEUpdate
            // 
            this.btnMDEUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMDEUpdate.Location = new System.Drawing.Point( 174, 3 );
            this.btnMDEUpdate.Name = "btnMDEUpdate";
            this.btnMDEUpdate.Size = new System.Drawing.Size( 53, 21 );
            this.btnMDEUpdate.TabIndex = 2;
            this.btnMDEUpdate.Text = "Update";
            this.ttpUcZAPI.SetToolTip( this.btnMDEUpdate, "MDE Update" );
            this.btnMDEUpdate.UseVisualStyleBackColor = true;
            this.btnMDEUpdate.Click += new System.EventHandler( this.btnMDEUpdate_Click );
            // 
            // cboMdeFolder
            // 
            this.cboMdeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMdeFolder.FormattingEnabled = true;
            this.cboMdeFolder.Location = new System.Drawing.Point( 66, 3 );
            this.cboMdeFolder.Name = "cboMdeFolder";
            this.cboMdeFolder.Size = new System.Drawing.Size( 102, 21 );
            this.cboMdeFolder.TabIndex = 1;
            this.ttpUcZAPI.SetToolTip( this.cboMdeFolder, "Point to MDE files location\r\nHit Enter to save the current value" );
            this.cboMdeFolder.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboMdeFolder_KeyPress );
            this.cboMdeFolder.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboMdeFolder_KeyDown );
            // 
            // lnkMdeFolder
            // 
            this.lnkMdeFolder.AutoSize = true;
            this.lnkMdeFolder.Location = new System.Drawing.Point( 1, 6 );
            this.lnkMdeFolder.Name = "lnkMdeFolder";
            this.lnkMdeFolder.Size = new System.Drawing.Size( 63, 13 );
            this.lnkMdeFolder.TabIndex = 0;
            this.lnkMdeFolder.TabStop = true;
            this.lnkMdeFolder.Text = "MDE Folder";
            this.ttpUcZAPI.SetToolTip( this.lnkMdeFolder, "Browse MDE Files Location" );
            this.lnkMdeFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkMdeFolder_LinkClicked );
            // 
            // tabPgDelete
            // 
            this.tabPgDelete.Location = new System.Drawing.Point( 4, 4 );
            this.tabPgDelete.Name = "tabPgDelete";
            this.tabPgDelete.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPgDelete.Size = new System.Drawing.Size( 230, 259 );
            this.tabPgDelete.TabIndex = 1;
            this.tabPgDelete.Text = "Whatever";
            this.ttpUcZAPI.SetToolTip( this.tabPgDelete, "Whatever" );
            this.tabPgDelete.UseVisualStyleBackColor = true;
            // 
            // lblNumCall
            // 
            this.lblNumCall.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblNumCall.Location = new System.Drawing.Point( 2, 78 );
            this.lblNumCall.Name = "lblNumCall";
            this.lblNumCall.Size = new System.Drawing.Size( 51, 17 );
            this.lblNumCall.TabIndex = 69;
            this.lblNumCall.Text = "Num Call";
            this.lblNumCall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttpUcZAPI.SetToolTip( this.lblNumCall, "Number of API called" );
            // 
            // btnAbortAll
            // 
            this.btnAbortAll.Location = new System.Drawing.Point( 214, 102 );
            this.btnAbortAll.Name = "btnAbortAll";
            this.btnAbortAll.Size = new System.Drawing.Size( 53, 21 );
            this.btnAbortAll.TabIndex = 78;
            this.btnAbortAll.Text = "Abort";
            this.ttpUcZAPI.SetToolTip( this.btnAbortAll, "Abort Test\r\nOS handles the abortion. Wait until you receive the confirmation box." +
                    "" );
            this.btnAbortAll.UseVisualStyleBackColor = true;
            this.btnAbortAll.Click += new System.EventHandler( this.btnAbortAll_Click );
            // 
            // gbxMsiControl
            // 
            this.gbxMsiControl.Controls.Add( this.btnAbortAll );
            this.gbxMsiControl.Controls.Add( this.chkZAPIPerf );
            this.gbxMsiControl.Controls.Add( this.txtFailCall );
            this.gbxMsiControl.Controls.Add( this.lblFailCall );
            this.gbxMsiControl.Controls.Add( this.txtCallPerSec );
            this.gbxMsiControl.Controls.Add( this.lblCallPerSec );
            this.gbxMsiControl.Controls.Add( this.txtDuration );
            this.gbxMsiControl.Controls.Add( this.lblDuration );
            this.gbxMsiControl.Controls.Add( this.txtNumCall );
            this.gbxMsiControl.Controls.Add( this.lblNumCall );
            this.gbxMsiControl.Controls.Add( this.txtEndTime );
            this.gbxMsiControl.Controls.Add( this.lblEndTime );
            this.gbxMsiControl.Controls.Add( this.txtStartTime );
            this.gbxMsiControl.Controls.Add( this.lblStartTime );
            this.gbxMsiControl.Controls.Add( this.nudThread );
            this.gbxMsiControl.Controls.Add( this.nudDelay );
            this.gbxMsiControl.Controls.Add( this.lblThread );
            this.gbxMsiControl.Controls.Add( this.lblDelay );
            this.gbxMsiControl.Location = new System.Drawing.Point( 4, 64 );
            this.gbxMsiControl.Name = "gbxMsiControl";
            this.gbxMsiControl.Size = new System.Drawing.Size( 274, 207 );
            this.gbxMsiControl.TabIndex = 44;
            this.gbxMsiControl.TabStop = false;
            // 
            // chkZAPIPerf
            // 
            this.chkZAPIPerf.AutoSize = true;
            this.chkZAPIPerf.Location = new System.Drawing.Point( 16, 98 );
            this.chkZAPIPerf.Name = "chkZAPIPerf";
            this.chkZAPIPerf.Size = new System.Drawing.Size( 108, 17 );
            this.chkZAPIPerf.TabIndex = 77;
            this.chkZAPIPerf.Text = "ZAPI Timing Test";
            // 
            // txtFailCall
            // 
            this.txtFailCall.Location = new System.Drawing.Point( 58, 54 );
            this.txtFailCall.Name = "txtFailCall";
            this.txtFailCall.ReadOnly = true;
            this.txtFailCall.Size = new System.Drawing.Size( 66, 20 );
            this.txtFailCall.TabIndex = 76;
            // 
            // lblFailCall
            // 
            this.lblFailCall.Location = new System.Drawing.Point( 3, 56 );
            this.lblFailCall.Name = "lblFailCall";
            this.lblFailCall.Size = new System.Drawing.Size( 52, 16 );
            this.lblFailCall.TabIndex = 75;
            this.lblFailCall.Text = "Fail Call";
            this.lblFailCall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCallPerSec
            // 
            this.txtCallPerSec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCallPerSec.Location = new System.Drawing.Point( 184, 77 );
            this.txtCallPerSec.Name = "txtCallPerSec";
            this.txtCallPerSec.ReadOnly = true;
            this.txtCallPerSec.Size = new System.Drawing.Size( 84, 20 );
            this.txtCallPerSec.TabIndex = 74;
            // 
            // lblCallPerSec
            // 
            this.lblCallPerSec.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblCallPerSec.Location = new System.Drawing.Point( 128, 78 );
            this.lblCallPerSec.Name = "lblCallPerSec";
            this.lblCallPerSec.Size = new System.Drawing.Size( 53, 17 );
            this.lblCallPerSec.TabIndex = 73;
            this.lblCallPerSec.Text = "Call/sec";
            this.lblCallPerSec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDuration
            // 
            this.txtDuration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDuration.Location = new System.Drawing.Point( 184, 54 );
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.ReadOnly = true;
            this.txtDuration.Size = new System.Drawing.Size( 84, 20 );
            this.txtDuration.TabIndex = 72;
            // 
            // lblDuration
            // 
            this.lblDuration.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblDuration.Location = new System.Drawing.Point( 128, 56 );
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size( 53, 17 );
            this.lblDuration.TabIndex = 71;
            this.lblDuration.Text = "Duration";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumCall
            // 
            this.txtNumCall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumCall.Location = new System.Drawing.Point( 58, 76 );
            this.txtNumCall.Name = "txtNumCall";
            this.txtNumCall.ReadOnly = true;
            this.txtNumCall.Size = new System.Drawing.Size( 66, 20 );
            this.txtNumCall.TabIndex = 70;
            // 
            // txtEndTime
            // 
            this.txtEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndTime.Location = new System.Drawing.Point( 184, 32 );
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.ReadOnly = true;
            this.txtEndTime.Size = new System.Drawing.Size( 84, 20 );
            this.txtEndTime.TabIndex = 68;
            // 
            // lblEndTime
            // 
            this.lblEndTime.Location = new System.Drawing.Point( 128, 35 );
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size( 53, 17 );
            this.lblEndTime.TabIndex = 67;
            this.lblEndTime.Text = "End Time";
            this.lblEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartTime
            // 
            this.txtStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartTime.Location = new System.Drawing.Point( 184, 10 );
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.ReadOnly = true;
            this.txtStartTime.Size = new System.Drawing.Size( 84, 20 );
            this.txtStartTime.TabIndex = 66;
            // 
            // lblStartTime
            // 
            this.lblStartTime.Location = new System.Drawing.Point( 124, 14 );
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size( 57, 17 );
            this.lblStartTime.TabIndex = 65;
            this.lblStartTime.Text = "Start Time";
            this.lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudThread
            // 
            this.nudThread.Location = new System.Drawing.Point( 58, 10 );
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
            this.nudThread.TabIndex = 64;
            this.nudThread.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudThread.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            // 
            // nudDelay
            // 
            this.nudDelay.Location = new System.Drawing.Point( 58, 32 );
            this.nudDelay.Maximum = new decimal( new int[] {
            5,
            0,
            0,
            0} );
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size( 66, 20 );
            this.nudDelay.TabIndex = 63;
            this.nudDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblThread
            // 
            this.lblThread.Location = new System.Drawing.Point( 3, 13 );
            this.lblThread.Name = "lblThread";
            this.lblThread.Size = new System.Drawing.Size( 52, 17 );
            this.lblThread.TabIndex = 62;
            this.lblThread.Text = "Thread";
            this.lblThread.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDelay
            // 
            this.lblDelay.Location = new System.Drawing.Point( 3, 34 );
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size( 52, 17 );
            this.lblDelay.TabIndex = 61;
            this.lblDelay.Text = "Delay";
            this.lblDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UcZAPIs
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add( this.gbxMsiControl );
            this.Controls.Add( this.zApiTabCtrl );
            this.Controls.Add( this.gbxCommon );
            this.Name = "UcZAPIs";
            this.Size = new System.Drawing.Size( 544, 274 );
            this.Load += new System.EventHandler( this.UcZAPIs_Load );
            this.gbxCommon.ResumeLayout( false );
            this.gbxCommon.PerformLayout();
            this.zApiTabCtrl.ResumeLayout( false );
            this.tabPgZAPIs.ResumeLayout( false );
            this.tabPgZAPIs.PerformLayout();
            this.gbxMsiControl.ResumeLayout( false );
            this.gbxMsiControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDomainName;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.LinkLabel lnkZdkFile;
        private System.Windows.Forms.ComboBox cboZdkFile;
        private System.Windows.Forms.GroupBox gbxCommon;
        private System.Windows.Forms.ToolTip ttpUcZAPI;
        private System.Windows.Forms.TabControl zApiTabCtrl;
        private System.Windows.Forms.TabPage tabPgDelete;
        private System.Windows.Forms.LinkLabel lnkMdeFolder;
        private System.Windows.Forms.ComboBox cboMdeFolder;
        private System.Windows.Forms.Button btnMDEUpdate;
        private System.Windows.Forms.GroupBox gbxMsiControl;
        private System.Windows.Forms.TextBox txtCallPerSec;
        private System.Windows.Forms.Label lblCallPerSec;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtNumCall;
        private System.Windows.Forms.Label lblNumCall;
        private System.Windows.Forms.TextBox txtEndTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.NumericUpDown nudThread;
        private System.Windows.Forms.NumericUpDown nudDelay;
        private System.Windows.Forms.Label lblThread;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.TextBox txtFailCall;
        private System.Windows.Forms.Label lblFailCall;
        private System.Windows.Forms.CheckBox chkZAPIPerf;
        private System.Windows.Forms.Button btnAbortAll;
        private System.Windows.Forms.TabPage tabPgZAPIs;
        private System.Windows.Forms.Button btnDelete;
    }
}
