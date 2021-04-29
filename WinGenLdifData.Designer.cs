namespace WsClient
{
    partial class WinGenLdifData
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
            this.lnkOutFile = new System.Windows.Forms.LinkLabel();
            this.txtOutFile = new System.Windows.Forms.TextBox();
            this.btnDoIt = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.cboDomain = new System.Windows.Forms.ComboBox();
            this.lblNumRec = new System.Windows.Forms.Label();
            this.nudRecord = new System.Windows.Forms.NumericUpDown();
            this.cboDnValue = new System.Windows.Forms.ComboBox();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer( this.components );
            ((System.ComponentModel.ISupportInitialize)(this.nudRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // lnkOutFile
            // 
            this.lnkOutFile.AutoSize = true;
            this.lnkOutFile.Location = new System.Drawing.Point( 3, 6 );
            this.lnkOutFile.Name = "lnkOutFile";
            this.lnkOutFile.Size = new System.Drawing.Size( 55, 13 );
            this.lnkOutFile.TabIndex = 0;
            this.lnkOutFile.TabStop = true;
            this.lnkOutFile.Text = "Output file";
            // 
            // txtOutFile
            // 
            this.txtOutFile.Location = new System.Drawing.Point( 61, 3 );
            this.txtOutFile.Name = "txtOutFile";
            this.txtOutFile.Size = new System.Drawing.Size( 249, 20 );
            this.txtOutFile.TabIndex = 1;
            this.txtOutFile.Text = "c:\\txt.txt";
            // 
            // btnDoIt
            // 
            this.btnDoIt.Location = new System.Drawing.Point( 313, 3 );
            this.btnDoIt.Name = "btnDoIt";
            this.btnDoIt.Size = new System.Drawing.Size( 65, 21 );
            this.btnDoIt.TabIndex = 2;
            this.btnDoIt.Text = "Do It";
            this.btnDoIt.UseVisualStyleBackColor = true;
            this.btnDoIt.Click += new System.EventHandler( this.btnDoIt_Click );
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point( 313, 25 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 66, 21 );
            this.btnAbort.TabIndex = 3;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // cboDomain
            // 
            this.cboDomain.FormattingEnabled = true;
            this.cboDomain.Items.AddRange( new object[] {
            "testdomain1",
            "testdomain2"} );
            this.cboDomain.Location = new System.Drawing.Point( 223, 25 );
            this.cboDomain.Name = "cboDomain";
            this.cboDomain.Size = new System.Drawing.Size( 87, 21 );
            this.cboDomain.TabIndex = 4;
            this.cboDomain.Text = "testdomain1";
            // 
            // lblNumRec
            // 
            this.lblNumRec.AutoSize = true;
            this.lblNumRec.Location = new System.Drawing.Point( 6, 29 );
            this.lblNumRec.Name = "lblNumRec";
            this.lblNumRec.Size = new System.Drawing.Size( 52, 13 );
            this.lblNumRec.TabIndex = 8;
            this.lblNumRec.Text = "Num Rec";
            // 
            // nudRecord
            // 
            this.nudRecord.Increment = new decimal( new int[] {
            10,
            0,
            0,
            0} );
            this.nudRecord.Location = new System.Drawing.Point( 62, 26 );
            this.nudRecord.Maximum = new decimal( new int[] {
            9999999,
            0,
            0,
            0} );
            this.nudRecord.Name = "nudRecord";
            this.nudRecord.Size = new System.Drawing.Size( 80, 20 );
            this.nudRecord.TabIndex = 9;
            this.nudRecord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudRecord.Value = new decimal( new int[] {
            10,
            0,
            0,
            0} );
            // 
            // cboDnValue
            // 
            this.cboDnValue.FormattingEnabled = true;
            this.cboDnValue.Items.AddRange( new object[] {
            "msfwid",
            "dbdirid"} );
            this.cboDnValue.Location = new System.Drawing.Point( 144, 25 );
            this.cboDnValue.Name = "cboDnValue";
            this.cboDnValue.Size = new System.Drawing.Size( 73, 21 );
            this.cboDnValue.TabIndex = 10;
            this.cboDnValue.Text = "msfwid";
            // 
            // txtDisplay
            // 
            this.txtDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDisplay.Location = new System.Drawing.Point( 6, 56 );
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.Size = new System.Drawing.Size( 372, 71 );
            this.txtDisplay.TabIndex = 11;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler( this.timer1_Tick );
            // 
            // WinGenLdifData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 381, 130 );
            this.Controls.Add( this.txtDisplay );
            this.Controls.Add( this.cboDnValue );
            this.Controls.Add( this.nudRecord );
            this.Controls.Add( this.lblNumRec );
            this.Controls.Add( this.cboDomain );
            this.Controls.Add( this.btnAbort );
            this.Controls.Add( this.btnDoIt );
            this.Controls.Add( this.txtOutFile );
            this.Controls.Add( this.lnkOutFile );
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WinGenLdifData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WinGenLdifData";
            ((System.ComponentModel.ISupportInitialize)(this.nudRecord)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkOutFile;
        private System.Windows.Forms.TextBox txtOutFile;
        private System.Windows.Forms.Button btnDoIt;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.ComboBox cboDomain;
        private System.Windows.Forms.Label lblNumRec;
        private System.Windows.Forms.NumericUpDown nudRecord;
        private System.Windows.Forms.ComboBox cboDnValue;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Timer timer1;
    }
}