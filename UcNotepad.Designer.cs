using System.Diagnostics;
namespace WsClient
{
    partial class UcNotepad
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
                Trace.WriteLine("UcNotepad.Designer.cs - components deposing");
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
            this.txtNotepad = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtNotepad
            // 
            this.txtNotepad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtNotepad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotepad.Location = new System.Drawing.Point( 0, 0 );
            this.txtNotepad.MaxLength = 65536;
            this.txtNotepad.Multiline = true;
            this.txtNotepad.Name = "txtNotepad";
            this.txtNotepad.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotepad.Size = new System.Drawing.Size( 462, 212 );
            this.txtNotepad.TabIndex = 1;
            // 
            // UcNotepad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.txtNotepad );
            this.Name = "UcNotepad";
            this.Size = new System.Drawing.Size( 462, 212 );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNotepad;
    }
}
