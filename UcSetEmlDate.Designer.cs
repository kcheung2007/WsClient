namespace WsClient
{
    partial class UcSetEmlDate
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
            this.lnkSource = new System.Windows.Forms.LinkLabel();
            this.ttpSetEmlDate = new System.Windows.Forms.ToolTip( this.components );
            this.lnkTarget = new System.Windows.Forms.LinkLabel();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnDoit = new System.Windows.Forms.Button();
            this.txtModified = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblModified = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lnkSource
            // 
            this.lnkSource.AutoSize = true;
            this.lnkSource.Location = new System.Drawing.Point( 4, 7 );
            this.lnkSource.Name = "lnkSource";
            this.lnkSource.Size = new System.Drawing.Size( 41, 13 );
            this.lnkSource.TabIndex = 0;
            this.lnkSource.TabStop = true;
            this.lnkSource.Text = "Source";
            this.ttpSetEmlDate.SetToolTip( this.lnkSource, "Browse original eml folder" );
            this.lnkSource.Click += new System.EventHandler( this.lnkSource_Click );
            // 
            // lnkTarget
            // 
            this.lnkTarget.AutoSize = true;
            this.lnkTarget.Location = new System.Drawing.Point( 6, 31 );
            this.lnkTarget.Name = "lnkTarget";
            this.lnkTarget.Size = new System.Drawing.Size( 38, 13 );
            this.lnkTarget.TabIndex = 1;
            this.lnkTarget.TabStop = true;
            this.lnkTarget.Text = "Target";
            this.ttpSetEmlDate.SetToolTip( this.lnkTarget, "Final destination location for modified eml" );
            this.lnkTarget.Click += new System.EventHandler( this.lnkTarget_Click );
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point( 47, 3 );
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size( 239, 20 );
            this.txtSource.TabIndex = 2;
            this.ttpSetEmlDate.SetToolTip( this.txtSource, "Point to folder that store eml for date change" );
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point( 47, 27 );
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size( 238, 20 );
            this.txtTarget.TabIndex = 3;
            this.ttpSetEmlDate.SetToolTip( this.txtTarget, "Store the modified files" );
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "ddd, dd MMM yyyy HH\':\'mm\':\'ss \'PST\'";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point( 47, 50 );
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size( 238, 20 );
            this.dateTimePicker1.TabIndex = 4;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point( 10, 54 );
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size( 33, 13 );
            this.lblDate.TabIndex = 5;
            this.lblDate.Text = "Date:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add( this.btnAbort );
            this.groupBox1.Controls.Add( this.btnDoit );
            this.groupBox1.Controls.Add( this.txtModified );
            this.groupBox1.Controls.Add( this.txtTotal );
            this.groupBox1.Controls.Add( this.lblModified );
            this.groupBox1.Controls.Add( this.lblTotal );
            this.groupBox1.Location = new System.Drawing.Point( 291, -4 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 240, 76 );
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point( 159, 32 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 75, 23 );
            this.btnAbort.TabIndex = 8;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // btnDoit
            // 
            this.btnDoit.Location = new System.Drawing.Point( 159, 9 );
            this.btnDoit.Name = "btnDoit";
            this.btnDoit.Size = new System.Drawing.Size( 75, 23 );
            this.btnDoit.TabIndex = 7;
            this.btnDoit.Text = "Do It!";
            this.btnDoit.UseVisualStyleBackColor = true;
            this.btnDoit.Click += new System.EventHandler( this.btnDoit_Click );
            // 
            // txtModified
            // 
            this.txtModified.Location = new System.Drawing.Point( 68, 33 );
            this.txtModified.Name = "txtModified";
            this.txtModified.ReadOnly = true;
            this.txtModified.Size = new System.Drawing.Size( 87, 20 );
            this.txtModified.TabIndex = 3;
            this.txtModified.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point( 68, 11 );
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size( 87, 20 );
            this.txtTotal.TabIndex = 2;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblModified
            // 
            this.lblModified.AutoSize = true;
            this.lblModified.Location = new System.Drawing.Point( 15, 36 );
            this.lblModified.Name = "lblModified";
            this.lblModified.Size = new System.Drawing.Size( 50, 13 );
            this.lblModified.TabIndex = 1;
            this.lblModified.Text = "Modified:";
            this.lblModified.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point( 6, 12 );
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size( 59, 13 );
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total EML:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UcSetEmlDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.groupBox1 );
            this.Controls.Add( this.lblDate );
            this.Controls.Add( this.dateTimePicker1 );
            this.Controls.Add( this.txtTarget );
            this.Controls.Add( this.txtSource );
            this.Controls.Add( this.lnkTarget );
            this.Controls.Add( this.lnkSource );
            this.Name = "UcSetEmlDate";
            this.Size = new System.Drawing.Size( 535, 113 );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkSource;
        private System.Windows.Forms.ToolTip ttpSetEmlDate;
        private System.Windows.Forms.LinkLabel lnkTarget;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblModified;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtModified;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnDoit;
        private System.Windows.Forms.Button btnAbort;
    }
}
