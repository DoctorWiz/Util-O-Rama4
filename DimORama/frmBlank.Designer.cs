
namespace UtilORama4
{
	partial class frmDim
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDim));
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnOK = new System.Windows.Forms.Button();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.picPreviewSource = new System.Windows.Forms.PictureBox();
			this.lblMasterFile = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.lblSourceFile = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.ttip = new System.Windows.Forms.ToolTip(this.components);
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picPreviewSource)).BeginInit();
			this.SuspendLayout();
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 537);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(696, 24);
			this.staStatus.TabIndex = 63;
			// 
			// pnlHelp
			// 
			this.pnlHelp.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlHelp.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.pnlHelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			this.pnlStatus.Size = new System.Drawing.Size(584, 19);
			this.pnlStatus.Spring = true;
			// 
			// pnlAbout
			// 
			this.pnlAbout.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlAbout.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.pnlAbout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlAbout.ForeColor = System.Drawing.SystemColors.Highlight;
			this.pnlAbout.Name = "pnlAbout";
			this.pnlAbout.Size = new System.Drawing.Size(52, 19);
			this.pnlAbout.Text = "About...";
			this.pnlAbout.Click += new System.EventHandler(this.pnlAbout_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(41, 511);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 67;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// picAboutIcon
			// 
			this.picAboutIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.BackgroundImage")));
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(638, 265);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 68;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// picPreviewSource
			// 
			this.picPreviewSource.BackColor = System.Drawing.Color.Tan;
			this.picPreviewSource.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picPreviewSource.Location = new System.Drawing.Point(120, 332);
			this.picPreviewSource.Name = "picPreviewSource";
			this.picPreviewSource.Size = new System.Drawing.Size(300, 20);
			this.picPreviewSource.TabIndex = 118;
			this.picPreviewSource.TabStop = false;
			this.picPreviewSource.Visible = false;
			// 
			// lblMasterFile
			// 
			this.lblMasterFile.AllowDrop = true;
			this.lblMasterFile.AutoSize = true;
			this.lblMasterFile.Location = new System.Drawing.Point(12, 397);
			this.lblMasterFile.Name = "lblMasterFile";
			this.lblMasterFile.Size = new System.Drawing.Size(131, 13);
			this.lblMasterFile.TabIndex = 117;
			this.lblMasterFile.Text = "Destination Sequence File";
			// 
			// button2
			// 
			this.button2.AllowDrop = true;
			this.button2.Location = new System.Drawing.Point(327, 426);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(36, 20);
			this.button2.TabIndex = 116;
			this.button2.Text = "...";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// textBox2
			// 
			this.textBox2.AllowDrop = true;
			this.textBox2.Enabled = false;
			this.textBox2.Location = new System.Drawing.Point(21, 426);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(300, 20);
			this.textBox2.TabIndex = 115;
			// 
			// lblSourceFile
			// 
			this.lblSourceFile.AllowDrop = true;
			this.lblSourceFile.AutoSize = true;
			this.lblSourceFile.Location = new System.Drawing.Point(12, 17);
			this.lblSourceFile.Name = "lblSourceFile";
			this.lblSourceFile.Size = new System.Drawing.Size(112, 13);
			this.lblSourceFile.TabIndex = 114;
			this.lblSourceFile.Text = "Source Sequence File";
			// 
			// button1
			// 
			this.button1.AllowDrop = true;
			this.button1.Location = new System.Drawing.Point(318, 33);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(36, 20);
			this.button1.TabIndex = 113;
			this.button1.Text = "...";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.AllowDrop = true;
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(12, 33);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(300, 20);
			this.textBox1.TabIndex = 112;
			// 
			// dlgOpenFile
			// 
			this.dlgOpenFile.FileName = "openFileDialog1";
			// 
			// imlTreeIcons
			// 
			this.imlTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeIcons.ImageStream")));
			this.imlTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlTreeIcons.Images.SetKeyName(0, "Track");
			this.imlTreeIcons.Images.SetKeyName(1, "ChannelGroup");
			this.imlTreeIcons.Images.SetKeyName(2, "RGBchannel");
			this.imlTreeIcons.Images.SetKeyName(3, "Channel");
			this.imlTreeIcons.Images.SetKeyName(4, "RedCh");
			this.imlTreeIcons.Images.SetKeyName(5, "GrnCh");
			this.imlTreeIcons.Images.SetKeyName(6, "BluCh");
			this.imlTreeIcons.Images.SetKeyName(7, "");
			// 
			// ttip
			// 
			this.ttip.ShowAlways = true;
			// 
			// frmDim
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(696, 561);
			this.Controls.Add(this.picPreviewSource);
			this.Controls.Add(this.lblMasterFile);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.lblSourceFile);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.picAboutIcon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmDim";
			this.Text = "Dim-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBlank_FormClosing);
			this.Load += new System.EventHandler(this.frmBlank_Load);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picPreviewSource)).EndInit();
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
		private System.Windows.Forms.PictureBox picPreviewSource;
		private System.Windows.Forms.Label lblMasterFile;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label lblSourceFile;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.ToolTip ttip;
	}
}

