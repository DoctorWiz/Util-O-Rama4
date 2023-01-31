
namespace UtilORama4
{
	partial class frmSync
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSync));
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnOK = new System.Windows.Forms.Button();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkXtoY = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
			this.chkXtoZ = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
			this.chkYtoZ = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chkXtoY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chkXtoZ)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chkYtoZ)).BeginInit();
			this.SuspendLayout();
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 232);
			this.staStatus.Name = "staStatus";
			this.staStatus.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.staStatus.Size = new System.Drawing.Size(476, 24);
			this.staStatus.TabIndex = 63;
			// 
			// pnlHelp
			// 
			this.pnlHelp.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlHelp.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.pnlHelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.pnlHelp.ForeColor = System.Drawing.SystemColors.Highlight;
			this.pnlHelp.IsLink = true;
			this.pnlHelp.Name = "pnlHelp";
			this.pnlHelp.Size = new System.Drawing.Size(45, 19);
			this.pnlHelp.Text = "Help...";
			this.pnlHelp.Click += new System.EventHandler(this.pnlHelp_Click);
			// 
			// pnlStatus
			// 
			this.pnlStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pnlStatus.Name = "pnlStatus";
			this.pnlStatus.Size = new System.Drawing.Size(362, 19);
			this.pnlStatus.Spring = true;
			// 
			// pnlAbout
			// 
			this.pnlAbout.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlAbout.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.pnlAbout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.pnlAbout.ForeColor = System.Drawing.SystemColors.Highlight;
			this.pnlAbout.Name = "pnlAbout";
			this.pnlAbout.Size = new System.Drawing.Size(52, 19);
			this.pnlAbout.Text = "About...";
			this.pnlAbout.Click += new System.EventHandler(this.pnlAbout_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(206, 179);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(88, 27);
			this.btnOK.TabIndex = 67;
			this.btnOK.Text = "Sync";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// picAboutIcon
			// 
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(318, 28);
			this.picAboutIcon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 68;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(318, 179);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(278, 15);
			this.label1.TabIndex = 69;
			this.label1.Text = "This is a sync stub for future Util-O-Rama programs";
			this.label1.Visible = false;
			// 
			// chkXtoY
			// 
			this.chkXtoY.AutoSize = true;
			this.chkXtoY.BeforeTouchSize = new System.Drawing.Size(265, 35);
			this.chkXtoY.CheckedImage = ((System.Drawing.Image)(resources.GetObject("chkXtoY.CheckedImage")));
			this.chkXtoY.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.chkXtoY.ImageCheckBox = true;
			this.chkXtoY.ImageCheckBoxSize = new System.Drawing.Size(32, 32);
			this.chkXtoY.Location = new System.Drawing.Point(29, 28);
			this.chkXtoY.Name = "chkXtoY";
			this.chkXtoY.Size = new System.Drawing.Size(265, 35);
			this.chkXtoY.TabIndex = 70;
			this.chkXtoY.Text = "Rotharin to Wizlights";
			this.chkXtoY.UncheckedImage = ((System.Drawing.Image)(resources.GetObject("chkXtoY.UncheckedImage")));
			// 
			// chkXtoZ
			// 
			this.chkXtoZ.AutoSize = true;
			this.chkXtoZ.BeforeTouchSize = new System.Drawing.Size(239, 35);
			this.chkXtoZ.CheckedImage = ((System.Drawing.Image)(resources.GetObject("chkXtoZ.CheckedImage")));
			this.chkXtoZ.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.chkXtoZ.ImageCheckBox = true;
			this.chkXtoZ.ImageCheckBoxSize = new System.Drawing.Size(32, 32);
			this.chkXtoZ.Location = new System.Drawing.Point(29, 69);
			this.chkXtoZ.Name = "chkXtoZ";
			this.chkXtoZ.Size = new System.Drawing.Size(239, 35);
			this.chkXtoZ.TabIndex = 71;
			this.chkXtoZ.Text = "Rotharin to Server";
			this.chkXtoZ.UncheckedImage = ((System.Drawing.Image)(resources.GetObject("chkXtoZ.UncheckedImage")));
			// 
			// chkYtoZ
			// 
			this.chkYtoZ.AutoSize = true;
			this.chkYtoZ.BeforeTouchSize = new System.Drawing.Size(248, 35);
			this.chkYtoZ.CheckedImage = ((System.Drawing.Image)(resources.GetObject("chkYtoZ.CheckedImage")));
			this.chkYtoZ.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.chkYtoZ.ImageCheckBox = true;
			this.chkYtoZ.ImageCheckBoxSize = new System.Drawing.Size(32, 32);
			this.chkYtoZ.Location = new System.Drawing.Point(29, 110);
			this.chkYtoZ.Name = "chkYtoZ";
			this.chkYtoZ.Size = new System.Drawing.Size(248, 35);
			this.chkYtoZ.TabIndex = 72;
			this.chkYtoZ.Text = "Wizlights to Server";
			this.chkYtoZ.UncheckedImage = ((System.Drawing.Image)(resources.GetObject("chkYtoZ.UncheckedImage")));
			// 
			// frmSync
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(476, 256);
			this.Controls.Add(this.chkYtoZ);
			this.Controls.Add(this.chkXtoZ);
			this.Controls.Add(this.chkXtoY);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.picAboutIcon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "frmSync";
			this.Text = "Sync-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSync_FormClosing);
			this.Load += new System.EventHandler(this.frmSync_Load);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chkXtoY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chkXtoZ)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chkYtoZ)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private System.Windows.Forms.Label label1;
		private Syncfusion.Windows.Forms.Tools.CheckBoxAdv chkXtoY;
		private Syncfusion.Windows.Forms.Tools.CheckBoxAdv chkXtoZ;
		private Syncfusion.Windows.Forms.Tools.CheckBoxAdv chkYtoZ;
	}
}

