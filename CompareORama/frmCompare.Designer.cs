
namespace UtilORama4
{
	partial class frmCompare
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompare));
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnOK = new System.Windows.Forms.Button();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.grpSeqFile = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowseLOR = new System.Windows.Forms.Button();
			this.txtLORfile = new System.Windows.Forms.TextBox();
			this.grpxLights = new System.Windows.Forms.GroupBox();
			this.txtXFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowseX = new System.Windows.Forms.Button();
			this.grpSpreadsheet = new System.Windows.Forms.GroupBox();
			this.txtSpreadsheet = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnBrowseSheet = new System.Windows.Forms.Button();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			this.grpSeqFile.SuspendLayout();
			this.grpxLights.SuspendLayout();
			this.grpSpreadsheet.SuspendLayout();
			this.SuspendLayout();
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 301);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(455, 24);
			this.staStatus.TabIndex = 4;
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
			this.pnlStatus.Size = new System.Drawing.Size(343, 19);
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
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(168, 172);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "Compare...";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// picAboutIcon
			// 
			this.picAboutIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.BackgroundImage")));
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(790, 17);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 68;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// grpSeqFile
			// 
			this.grpSeqFile.Controls.Add(this.label2);
			this.grpSeqFile.Controls.Add(this.btnBrowseLOR);
			this.grpSeqFile.Controls.Add(this.txtLORfile);
			this.grpSeqFile.Location = new System.Drawing.Point(13, 12);
			this.grpSeqFile.Name = "grpSeqFile";
			this.grpSeqFile.Size = new System.Drawing.Size(432, 74);
			this.grpSeqFile.TabIndex = 0;
			this.grpSeqFile.TabStop = false;
			this.grpSeqFile.Text = "LOR Channel Configuration";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(103, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "LOR Sequence File:";
			// 
			// btnBrowseLOR
			// 
			this.btnBrowseLOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowseLOR.Location = new System.Drawing.Point(380, 43);
			this.btnBrowseLOR.Name = "btnBrowseLOR";
			this.btnBrowseLOR.Size = new System.Drawing.Size(46, 21);
			this.btnBrowseLOR.TabIndex = 2;
			this.btnBrowseLOR.Text = "...";
			this.btnBrowseLOR.UseVisualStyleBackColor = true;
			this.btnBrowseLOR.Click += new System.EventHandler(this.btnBrowseLOR_Click);
			// 
			// txtLORfile
			// 
			this.txtLORfile.Location = new System.Drawing.Point(6, 43);
			this.txtLORfile.Name = "txtLORfile";
			this.txtLORfile.ReadOnly = true;
			this.txtLORfile.Size = new System.Drawing.Size(368, 20);
			this.txtLORfile.TabIndex = 1;
			this.txtLORfile.TabStop = false;
			this.txtLORfile.Text = "C:\\Users\\Me\\Documents\\Light-O-Rama\\MySequence.lms";
			this.txtLORfile.TextChanged += new System.EventHandler(this.txtLORfile_TextChanged);
			// 
			// grpxLights
			// 
			this.grpxLights.Controls.Add(this.txtXFile);
			this.grpxLights.Controls.Add(this.label1);
			this.grpxLights.Controls.Add(this.btnBrowseX);
			this.grpxLights.Location = new System.Drawing.Point(12, 92);
			this.grpxLights.Name = "grpxLights";
			this.grpxLights.Size = new System.Drawing.Size(433, 74);
			this.grpxLights.TabIndex = 1;
			this.grpxLights.TabStop = false;
			this.grpxLights.Text = "xLights Configuration";
			// 
			// txtXFile
			// 
			this.txtXFile.Location = new System.Drawing.Point(6, 40);
			this.txtXFile.Name = "txtXFile";
			this.txtXFile.ReadOnly = true;
			this.txtXFile.Size = new System.Drawing.Size(368, 20);
			this.txtXFile.TabIndex = 1;
			this.txtXFile.TabStop = false;
			this.txtXFile.Text = "C:\\Users\\Me\\Documents\\xLights\\MyShow\\xlights_rgbeffects.xml";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "xLights RGBeffects File:";
			// 
			// btnBrowseX
			// 
			this.btnBrowseX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowseX.Location = new System.Drawing.Point(381, 40);
			this.btnBrowseX.Name = "btnBrowseX";
			this.btnBrowseX.Size = new System.Drawing.Size(46, 21);
			this.btnBrowseX.TabIndex = 2;
			this.btnBrowseX.Text = "...";
			this.btnBrowseX.UseVisualStyleBackColor = true;
			this.btnBrowseX.Click += new System.EventHandler(this.btnBrowseX_Click);
			// 
			// grpSpreadsheet
			// 
			this.grpSpreadsheet.Controls.Add(this.txtSpreadsheet);
			this.grpSpreadsheet.Controls.Add(this.label3);
			this.grpSpreadsheet.Controls.Add(this.btnBrowseSheet);
			this.grpSpreadsheet.Enabled = false;
			this.grpSpreadsheet.Location = new System.Drawing.Point(12, 214);
			this.grpSpreadsheet.Name = "grpSpreadsheet";
			this.grpSpreadsheet.Size = new System.Drawing.Size(433, 74);
			this.grpSpreadsheet.TabIndex = 3;
			this.grpSpreadsheet.TabStop = false;
			this.grpSpreadsheet.Text = "Save to Spreadsheet";
			// 
			// txtSpreadsheet
			// 
			this.txtSpreadsheet.Location = new System.Drawing.Point(6, 40);
			this.txtSpreadsheet.Name = "txtSpreadsheet";
			this.txtSpreadsheet.ReadOnly = true;
			this.txtSpreadsheet.Size = new System.Drawing.Size(368, 20);
			this.txtSpreadsheet.TabIndex = 1;
			this.txtSpreadsheet.TabStop = false;
			this.txtSpreadsheet.Text = "C:\\Users\\Me\\Documents\\LOR-to-xLights_Channel_Comparison.csv";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Spreadsheet File:";
			// 
			// btnBrowseSheet
			// 
			this.btnBrowseSheet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowseSheet.Location = new System.Drawing.Point(381, 40);
			this.btnBrowseSheet.Name = "btnBrowseSheet";
			this.btnBrowseSheet.Size = new System.Drawing.Size(46, 21);
			this.btnBrowseSheet.TabIndex = 2;
			this.btnBrowseSheet.Text = "...";
			this.btnBrowseSheet.UseVisualStyleBackColor = true;
			this.btnBrowseSheet.Click += new System.EventHandler(this.btnBrowseSheet_Click);
			// 
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
			// 
			// frmCompare
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(455, 325);
			this.Controls.Add(this.grpSpreadsheet);
			this.Controls.Add(this.grpSeqFile);
			this.Controls.Add(this.grpxLights);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.picAboutIcon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmCompare";
			this.Text = "Compare-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBlank_FormClosing);
			this.Load += new System.EventHandler(this.frmBlank_Load);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			this.grpSeqFile.ResumeLayout(false);
			this.grpSeqFile.PerformLayout();
			this.grpxLights.ResumeLayout(false);
			this.grpxLights.PerformLayout();
			this.grpSpreadsheet.ResumeLayout(false);
			this.grpSpreadsheet.PerformLayout();
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
		private System.Windows.Forms.GroupBox grpSeqFile;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowseLOR;
		private System.Windows.Forms.TextBox txtLORfile;
		private System.Windows.Forms.GroupBox grpxLights;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBrowseX;
		private System.Windows.Forms.TextBox txtXFile;
		private System.Windows.Forms.GroupBox grpSpreadsheet;
		private System.Windows.Forms.TextBox txtSpreadsheet;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnBrowseSheet;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
	}
}

