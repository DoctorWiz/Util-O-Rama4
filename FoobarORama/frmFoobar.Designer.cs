namespace FoobarORama
{
	partial class frmFoobar
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFoobar));
			this.lblFile = new System.Windows.Forms.Label();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.pnlInfo = new System.Windows.Forms.Panel();
			this.lblName = new System.Windows.Forms.Label();
			this.lblNum = new System.Windows.Forms.Label();
			this.lblType = new System.Windows.Forms.Label();
			this.pnlInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblFile
			// 
			this.lblFile.AutoSize = true;
			this.lblFile.Location = new System.Drawing.Point(11, 19);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(98, 13);
			this.lblFile.TabIndex = 0;
			this.lblFile.Text = "Click to select file...";
			this.lblFile.Click += new System.EventHandler(this.lblFile_Click);
			// 
			// pnlInfo
			// 
			this.pnlInfo.Controls.Add(this.lblType);
			this.pnlInfo.Controls.Add(this.lblName);
			this.pnlInfo.Controls.Add(this.lblNum);
			this.pnlInfo.Location = new System.Drawing.Point(3, 49);
			this.pnlInfo.Name = "pnlInfo";
			this.pnlInfo.Size = new System.Drawing.Size(472, 80);
			this.pnlInfo.TabIndex = 3;
			this.pnlInfo.Visible = false;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(8, 58);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(35, 13);
			this.lblName.TabIndex = 4;
			this.lblName.Text = "Name";
			// 
			// lblNum
			// 
			this.lblNum.AutoSize = true;
			this.lblNum.Location = new System.Drawing.Point(8, 9);
			this.lblNum.Name = "lblNum";
			this.lblNum.Size = new System.Drawing.Size(67, 13);
			this.lblNum.TabIndex = 3;
			this.lblNum.Text = "Saved Index";
			// 
			// lblType
			// 
			this.lblType.AutoSize = true;
			this.lblType.Location = new System.Drawing.Point(8, 35);
			this.lblType.Name = "lblType";
			this.lblType.Size = new System.Drawing.Size(31, 13);
			this.lblType.TabIndex = 5;
			this.lblType.Text = "Type";
			// 
			// frmFoobar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(478, 139);
			this.Controls.Add(this.pnlInfo);
			this.Controls.Add(this.lblFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmFoobar";
			this.Text = "Foobar-O-Rama";
			this.Load += new System.EventHandler(this.frmFoobar_Load);
			this.pnlInfo.ResumeLayout(false);
			this.pnlInfo.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblFile;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.Panel pnlInfo;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblNum;
	}
}

