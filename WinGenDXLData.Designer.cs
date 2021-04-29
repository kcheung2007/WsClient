namespace WsClient
{
    partial class WinGenDXLData
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
                if(m_thdGenData != null && m_thdGenData.IsAlive)
                {
                    this.KillGenDataThread();
                    commObj.LogToFile( "Thread.log", "   KillGenDataThread Killed" );
                }
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lnkNSFFile = new System.Windows.Forms.LinkLabel();
            this.ttpDXLData = new System.Windows.Forms.ToolTip( this.components );
            this.cboInNsfFile = new System.Windows.Forms.ComboBox();
            this.lnkOutFolder = new System.Windows.Forms.LinkLabel();
            this.cboOutFolder = new System.Windows.Forms.ComboBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.rtbDisplay = new System.Windows.Forms.RichTextBox();
            this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lnkNSFFile
            // 
            this.lnkNSFFile.AutoSize = true;
            this.lnkNSFFile.Location = new System.Drawing.Point( 27, 7 );
            this.lnkNSFFile.Name = "lnkNSFFile";
            this.lnkNSFFile.Size = new System.Drawing.Size( 47, 13 );
            this.lnkNSFFile.TabIndex = 0;
            this.lnkNSFFile.TabStop = true;
            this.lnkNSFFile.Text = "NSF File";
            this.ttpDXLData.SetToolTip( this.lnkNSFFile, "Point to input nsf file" );
            this.lnkNSFFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkNSFFile_LinkClicked );
            // 
            // cboInNsfFile
            // 
            this.cboInNsfFile.FormattingEnabled = true;
            this.cboInNsfFile.Location = new System.Drawing.Point( 83, 3 );
            this.cboInNsfFile.Name = "cboInNsfFile";
            this.cboInNsfFile.Size = new System.Drawing.Size( 250, 21 );
            this.cboInNsfFile.TabIndex = 1;
            this.ttpDXLData.SetToolTip( this.cboInNsfFile, "Input NSF File" );
            this.cboInNsfFile.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboInNsfFile_KeyPress );
            this.cboInNsfFile.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboInNsfFile_KeyDown );
            // 
            // lnkOutFolder
            // 
            this.lnkOutFolder.AutoSize = true;
            this.lnkOutFolder.Location = new System.Drawing.Point( 3, 32 );
            this.lnkOutFolder.Name = "lnkOutFolder";
            this.lnkOutFolder.Size = new System.Drawing.Size( 71, 13 );
            this.lnkOutFolder.TabIndex = 2;
            this.lnkOutFolder.TabStop = true;
            this.lnkOutFolder.Text = "Output Folder";
            this.ttpDXLData.SetToolTip( this.lnkOutFolder, "Output DXL File location" );
            this.lnkOutFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkOutFolder_LinkClicked );
            // 
            // cboOutFolder
            // 
            this.cboOutFolder.FormattingEnabled = true;
            this.cboOutFolder.Location = new System.Drawing.Point( 83, 26 );
            this.cboOutFolder.Name = "cboOutFolder";
            this.cboOutFolder.Size = new System.Drawing.Size( 250, 21 );
            this.cboOutFolder.TabIndex = 3;
            this.ttpDXLData.SetToolTip( this.cboOutFolder, "DXL output location" );
            this.cboOutFolder.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboOutFolder_KeyPress );
            this.cboOutFolder.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboOutFolder_KeyDown );
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point( 17, 54 );
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size( 57, 16 );
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password";
            this.ttpDXLData.SetToolTip( this.lblPassword, "Domino local user profile password" );
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point( 337, 3 );
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size( 75, 23 );
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Generate";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler( this.btnCreate_Click );
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point( 337, 26 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 75, 23 );
            this.btnAbort.TabIndex = 5;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // rtbDisplay
            // 
            this.rtbDisplay.Location = new System.Drawing.Point( 9, 73 );
            this.rtbDisplay.Name = "rtbDisplay";
            this.rtbDisplay.ReadOnly = true;
            this.rtbDisplay.Size = new System.Drawing.Size( 403, 85 );
            this.rtbDisplay.TabIndex = 6;
            this.rtbDisplay.Text = "";
            // 
            // folderBrowserDlg
            // 
            this.folderBrowserDlg.Description = "Browser the folder that contains eml files";
            this.folderBrowserDlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDlg.SelectedPath = "C:\\";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point( 83, 50 );
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size( 248, 20 );
            this.txtPassword.TabIndex = 8;
            // 
            // WinGenDXLData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 415, 163 );
            this.Controls.Add( this.txtPassword );
            this.Controls.Add( this.lblPassword );
            this.Controls.Add( this.rtbDisplay );
            this.Controls.Add( this.btnAbort );
            this.Controls.Add( this.btnCreate );
            this.Controls.Add( this.cboOutFolder );
            this.Controls.Add( this.lnkOutFolder );
            this.Controls.Add( this.cboInNsfFile );
            this.Controls.Add( this.lnkNSFFile );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "WinGenDXLData";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WinGenDXLData";
            this.TopMost = true;
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkNSFFile;
        private System.Windows.Forms.ToolTip ttpDXLData;
        private System.Windows.Forms.ComboBox cboInNsfFile;
        private System.Windows.Forms.LinkLabel lnkOutFolder;
        private System.Windows.Forms.ComboBox cboOutFolder;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.RichTextBox rtbDisplay;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDlg;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
    }
}