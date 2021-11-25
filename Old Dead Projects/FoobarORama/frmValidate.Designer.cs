namespace UtilORama4
{
	partial class frmValidate
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmValidate));
			this.lblSourceFile = new System.Windows.Forms.Label();
			this.btnBrowseSource = new System.Windows.Forms.Button();
			this.txtSourceFile = new System.Windows.Forms.TextBox();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// lblSourceFile
			// 
			this.lblSourceFile.AllowDrop = true;
			this.lblSourceFile.AutoSize = true;
			this.lblSourceFile.Location = new System.Drawing.Point(68, 47);
			this.lblSourceFile.Name = "lblSourceFile";
			this.lblSourceFile.Size = new System.Drawing.Size(128, 13);
			this.lblSourceFile.TabIndex = 24;
			this.lblSourceFile.Text = "Sequence File to Validate";
			// 
			// btnBrowseSource
			// 
			this.btnBrowseSource.AllowDrop = true;
			this.btnBrowseSource.Location = new System.Drawing.Point(374, 71);
			this.btnBrowseSource.Name = "btnBrowseSource";
			this.btnBrowseSource.Size = new System.Drawing.Size(36, 20);
			this.btnBrowseSource.TabIndex = 23;
			this.btnBrowseSource.Text = "...";
			this.btnBrowseSource.UseVisualStyleBackColor = true;
			// 
			// txtSourceFile
			// 
			this.txtSourceFile.AllowDrop = true;
			this.txtSourceFile.Enabled = false;
			this.txtSourceFile.Location = new System.Drawing.Point(68, 71);
			this.txtSourceFile.Name = "txtSourceFile";
			this.txtSourceFile.Size = new System.Drawing.Size(300, 20);
			this.txtSourceFile.TabIndex = 22;
			// 
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
			// 
			// frmFoobar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(478, 139);
			this.Controls.Add(this.lblSourceFile);
			this.Controls.Add(this.btnBrowseSource);
			this.Controls.Add(this.txtSourceFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmFoobar";
			this.Text = "Valid-O-Rama";
			this.Load += new System.EventHandler(this.frmFoobar_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblSourceFile;
		private System.Windows.Forms.Button btnBrowseSource;
		private System.Windows.Forms.TextBox txtSourceFile;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
	}
}

