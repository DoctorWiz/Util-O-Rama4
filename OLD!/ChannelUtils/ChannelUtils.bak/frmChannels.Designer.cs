namespace ChannelUtils
{
    partial class frmChannels
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpFile = new System.Windows.Forms.GroupBox();
            this.btnReadMaster = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dlgFile = new System.Windows.Forms.OpenFileDialog();
            this.grpFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFile
            // 
            this.grpFile.Controls.Add(this.btnReadMaster);
            this.grpFile.Controls.Add(this.btnBrowse);
            this.grpFile.Controls.Add(this.txtFilename);
            this.grpFile.Controls.Add(this.label8);
            this.grpFile.Location = new System.Drawing.Point(12, 12);
            this.grpFile.Name = "grpFile";
            this.grpFile.Size = new System.Drawing.Size(652, 73);
            this.grpFile.TabIndex = 29;
            this.grpFile.TabStop = false;
            this.grpFile.Text = " Step 1: Master Channel Configuration File ";
            // 
            // btnReadMaster
            // 
            this.btnReadMaster.Location = new System.Drawing.Point(494, 30);
            this.btnReadMaster.Name = "btnReadMaster";
            this.btnReadMaster.Size = new System.Drawing.Size(61, 20);
            this.btnReadMaster.TabIndex = 3;
            this.btnReadMaster.Text = "Read...";
            this.btnReadMaster.UseVisualStyleBackColor = true;
            this.btnReadMaster.Click += new System.EventHandler(this.btnReadMaster_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(427, 30);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(61, 20);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(52, 31);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(371, 20);
            this.txtFilename.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "File:";
            // 
            // dlgFile
            // 
            this.dlgFile.FileName = "*.lcc";
            // 
            // frmChannels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 594);
            this.Controls.Add(this.grpFile);
            this.Name = "frmChannels";
            this.Text = "Light-O-Rama Channel Utilities";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLORutils_FormClosing);
            this.Load += new System.EventHandler(this.frmLORutils_Load);
            this.grpFile.ResumeLayout(false);
            this.grpFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFile;
        private System.Windows.Forms.Button btnReadMaster;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog dlgFile;
    }
}

