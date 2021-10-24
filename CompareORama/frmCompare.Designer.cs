
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
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.grpSeqFile = new System.Windows.Forms.GroupBox();
			this.lblInfoViz = new System.Windows.Forms.Label();
			this.lblInfoSeq = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.btnBrowseViz = new System.Windows.Forms.Button();
			this.txtFileVisual = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowseSequence = new System.Windows.Forms.Button();
			this.txtLORfile = new System.Windows.Forms.TextBox();
			this.grpxLights = new System.Windows.Forms.GroupBox();
			this.lblInfoxL = new System.Windows.Forms.Label();
			this.txtXFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowseX = new System.Windows.Forms.Button();
			this.grpSpreadsheet = new System.Windows.Forms.GroupBox();
			this.lblInfoSheet = new System.Windows.Forms.Label();
			this.txtSpreadsheet = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnBrowseSheet = new System.Windows.Forms.Button();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblInfoDat = new System.Windows.Forms.Label();
			this.lblPathData = new System.Windows.Forms.Label();
			this.btnBrowseDatabase = new System.Windows.Forms.Button();
			this.txtFileDatabase = new System.Windows.Forms.TextBox();
			this.picDivider = new System.Windows.Forms.PictureBox();
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			this.grpSeqFile.SuspendLayout();
			this.grpxLights.SuspendLayout();
			this.grpSpreadsheet.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picDivider)).BeginInit();
			this.SuspendLayout();
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 447);
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
			// picAboutIcon
			// 
			this.picAboutIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.BackgroundImage")));
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(790, 207);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 68;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// grpSeqFile
			// 
			this.grpSeqFile.Controls.Add(this.lblInfoViz);
			this.grpSeqFile.Controls.Add(this.lblInfoSeq);
			this.grpSeqFile.Controls.Add(this.label5);
			this.grpSeqFile.Controls.Add(this.btnBrowseViz);
			this.grpSeqFile.Controls.Add(this.txtFileVisual);
			this.grpSeqFile.Controls.Add(this.label2);
			this.grpSeqFile.Controls.Add(this.btnBrowseSequence);
			this.grpSeqFile.Controls.Add(this.txtLORfile);
			this.grpSeqFile.Location = new System.Drawing.Point(12, 111);
			this.grpSeqFile.Name = "grpSeqFile";
			this.grpSeqFile.Size = new System.Drawing.Size(433, 125);
			this.grpSeqFile.TabIndex = 1;
			this.grpSeqFile.TabStop = false;
			this.grpSeqFile.Text = "LOR Channel Configuration";
			// 
			// lblInfoViz
			// 
			this.lblInfoViz.AutoSize = true;
			this.lblInfoViz.Font = new System.Drawing.Font("Cascadia Code PL", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInfoViz.ForeColor = System.Drawing.Color.MediumPurple;
			this.lblInfoViz.Location = new System.Drawing.Point(238, 74);
			this.lblInfoViz.Name = "lblInfoViz";
			this.lblInfoViz.Size = new System.Drawing.Size(55, 15);
			this.lblInfoViz.TabIndex = 5;
			this.lblInfoViz.Text = "00/00/00";
			this.lblInfoViz.Visible = false;
			// 
			// lblInfoSeq
			// 
			this.lblInfoSeq.AutoSize = true;
			this.lblInfoSeq.Font = new System.Drawing.Font("Cascadia Code PL", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInfoSeq.ForeColor = System.Drawing.Color.MediumPurple;
			this.lblInfoSeq.Location = new System.Drawing.Point(238, 22);
			this.lblInfoSeq.Name = "lblInfoSeq";
			this.lblInfoSeq.Size = new System.Drawing.Size(55, 15);
			this.lblInfoSeq.TabIndex = 1;
			this.lblInfoSeq.Text = "00/00/00";
			this.lblInfoSeq.Visible = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 76);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(98, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "LOR Visualizer File:";
			// 
			// btnBrowseViz
			// 
			this.btnBrowseViz.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowseViz.Location = new System.Drawing.Point(380, 95);
			this.btnBrowseViz.Name = "btnBrowseViz";
			this.btnBrowseViz.Size = new System.Drawing.Size(46, 21);
			this.btnBrowseViz.TabIndex = 7;
			this.btnBrowseViz.Text = "...";
			this.btnBrowseViz.UseVisualStyleBackColor = true;
			this.btnBrowseViz.Click += new System.EventHandler(this.btnBrowseViz_Click);
			// 
			// txtFileVisual
			// 
			this.txtFileVisual.Location = new System.Drawing.Point(6, 95);
			this.txtFileVisual.Name = "txtFileVisual";
			this.txtFileVisual.ReadOnly = true;
			this.txtFileVisual.Size = new System.Drawing.Size(368, 20);
			this.txtFileVisual.TabIndex = 6;
			this.txtFileVisual.TabStop = false;
			this.txtFileVisual.Text = "C:\\Users\\Me\\Documents\\Light-O-Rama\\Visualizations\\MyHouse.lee";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(103, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "LOR Sequence File:";
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// btnBrowseSequence
			// 
			this.btnBrowseSequence.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowseSequence.Location = new System.Drawing.Point(380, 43);
			this.btnBrowseSequence.Name = "btnBrowseSequence";
			this.btnBrowseSequence.Size = new System.Drawing.Size(46, 21);
			this.btnBrowseSequence.TabIndex = 3;
			this.btnBrowseSequence.Text = "...";
			this.btnBrowseSequence.UseVisualStyleBackColor = true;
			this.btnBrowseSequence.Click += new System.EventHandler(this.btnBrowseLOR_Click);
			// 
			// txtLORfile
			// 
			this.txtLORfile.Location = new System.Drawing.Point(6, 43);
			this.txtLORfile.Name = "txtLORfile";
			this.txtLORfile.ReadOnly = true;
			this.txtLORfile.Size = new System.Drawing.Size(368, 20);
			this.txtLORfile.TabIndex = 2;
			this.txtLORfile.TabStop = false;
			this.txtLORfile.Text = "C:\\Users\\Me\\Documents\\Light-O-Rama\\MySequence.lms";
			this.txtLORfile.TextChanged += new System.EventHandler(this.txtLORfile_TextChanged);
			// 
			// grpxLights
			// 
			this.grpxLights.Controls.Add(this.lblInfoxL);
			this.grpxLights.Controls.Add(this.txtXFile);
			this.grpxLights.Controls.Add(this.label1);
			this.grpxLights.Controls.Add(this.btnBrowseX);
			this.grpxLights.Location = new System.Drawing.Point(12, 242);
			this.grpxLights.Name = "grpxLights";
			this.grpxLights.Size = new System.Drawing.Size(433, 74);
			this.grpxLights.TabIndex = 2;
			this.grpxLights.TabStop = false;
			this.grpxLights.Text = "xLights Configuration";
			// 
			// lblInfoxL
			// 
			this.lblInfoxL.AutoSize = true;
			this.lblInfoxL.Font = new System.Drawing.Font("Cascadia Code PL", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInfoxL.ForeColor = System.Drawing.Color.MediumPurple;
			this.lblInfoxL.Location = new System.Drawing.Point(238, 22);
			this.lblInfoxL.Name = "lblInfoxL";
			this.lblInfoxL.Size = new System.Drawing.Size(55, 15);
			this.lblInfoxL.TabIndex = 1;
			this.lblInfoxL.Text = "00/00/00";
			this.lblInfoxL.Visible = false;
			// 
			// txtXFile
			// 
			this.txtXFile.Location = new System.Drawing.Point(6, 40);
			this.txtXFile.Name = "txtXFile";
			this.txtXFile.ReadOnly = true;
			this.txtXFile.Size = new System.Drawing.Size(368, 20);
			this.txtXFile.TabIndex = 2;
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
			this.btnBrowseX.TabIndex = 3;
			this.btnBrowseX.Text = "...";
			this.btnBrowseX.UseVisualStyleBackColor = true;
			this.btnBrowseX.Click += new System.EventHandler(this.btnBrowseX_Click);
			// 
			// grpSpreadsheet
			// 
			this.grpSpreadsheet.Controls.Add(this.lblInfoSheet);
			this.grpSpreadsheet.Controls.Add(this.txtSpreadsheet);
			this.grpSpreadsheet.Controls.Add(this.label3);
			this.grpSpreadsheet.Controls.Add(this.btnBrowseSheet);
			this.grpSpreadsheet.Enabled = false;
			this.grpSpreadsheet.Location = new System.Drawing.Point(12, 363);
			this.grpSpreadsheet.Name = "grpSpreadsheet";
			this.grpSpreadsheet.Size = new System.Drawing.Size(433, 74);
			this.grpSpreadsheet.TabIndex = 3;
			this.grpSpreadsheet.TabStop = false;
			this.grpSpreadsheet.Text = "Save to Spreadsheet";
			// 
			// lblInfoSheet
			// 
			this.lblInfoSheet.AutoSize = true;
			this.lblInfoSheet.Font = new System.Drawing.Font("Cascadia Code PL", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInfoSheet.ForeColor = System.Drawing.Color.MediumPurple;
			this.lblInfoSheet.Location = new System.Drawing.Point(238, 22);
			this.lblInfoSheet.Name = "lblInfoSheet";
			this.lblInfoSheet.Size = new System.Drawing.Size(55, 15);
			this.lblInfoSheet.TabIndex = 2;
			this.lblInfoSheet.Text = "00/00/00";
			this.lblInfoSheet.Visible = false;
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
			this.btnBrowseSheet.TabIndex = 3;
			this.btnBrowseSheet.Text = "...";
			this.btnBrowseSheet.UseVisualStyleBackColor = true;
			this.btnBrowseSheet.Click += new System.EventHandler(this.btnBrowseSheet_Click);
			// 
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblInfoDat);
			this.groupBox1.Controls.Add(this.lblPathData);
			this.groupBox1.Controls.Add(this.btnBrowseDatabase);
			this.groupBox1.Controls.Add(this.txtFileDatabase);
			this.groupBox1.Location = new System.Drawing.Point(12, 21);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(433, 74);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Util-O-Rama Master Channel Database";
			// 
			// lblInfoDat
			// 
			this.lblInfoDat.AutoSize = true;
			this.lblInfoDat.Font = new System.Drawing.Font("Cascadia Code PL", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInfoDat.ForeColor = System.Drawing.Color.MediumPurple;
			this.lblInfoDat.Location = new System.Drawing.Point(238, 24);
			this.lblInfoDat.Name = "lblInfoDat";
			this.lblInfoDat.Size = new System.Drawing.Size(55, 15);
			this.lblInfoDat.TabIndex = 1;
			this.lblInfoDat.Text = "00/00/00";
			this.lblInfoDat.Visible = false;
			// 
			// lblPathData
			// 
			this.lblPathData.AutoSize = true;
			this.lblPathData.Location = new System.Drawing.Point(6, 24);
			this.lblPathData.Name = "lblPathData";
			this.lblPathData.Size = new System.Drawing.Size(81, 13);
			this.lblPathData.TabIndex = 0;
			this.lblPathData.Text = "Database Path:";
			// 
			// btnBrowseDatabase
			// 
			this.btnBrowseDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowseDatabase.Location = new System.Drawing.Point(380, 43);
			this.btnBrowseDatabase.Name = "btnBrowseDatabase";
			this.btnBrowseDatabase.Size = new System.Drawing.Size(46, 21);
			this.btnBrowseDatabase.TabIndex = 3;
			this.btnBrowseDatabase.Text = "...";
			this.btnBrowseDatabase.UseVisualStyleBackColor = true;
			this.btnBrowseDatabase.Click += new System.EventHandler(this.btnBrowseDatabase_Click);
			// 
			// txtFileDatabase
			// 
			this.txtFileDatabase.Location = new System.Drawing.Point(6, 43);
			this.txtFileDatabase.Name = "txtFileDatabase";
			this.txtFileDatabase.ReadOnly = true;
			this.txtFileDatabase.Size = new System.Drawing.Size(368, 20);
			this.txtFileDatabase.TabIndex = 2;
			this.txtFileDatabase.TabStop = false;
			this.txtFileDatabase.Text = "C:\\Users\\Me\\Documents\\Util-O-Rama\\ChannelDB";
			// 
			// picDivider
			// 
			this.picDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picDivider.Location = new System.Drawing.Point(12, 331);
			this.picDivider.Name = "picDivider";
			this.picDivider.Size = new System.Drawing.Size(433, 4);
			this.picDivider.TabIndex = 70;
			this.picDivider.TabStop = false;
			// 
			// frmCompare
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(455, 471);
			this.Controls.Add(this.picDivider);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.grpSpreadsheet);
			this.Controls.Add(this.grpSeqFile);
			this.Controls.Add(this.grpxLights);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.picAboutIcon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmCompare";
			this.Text = "Compare-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBlank_FormClosing);
			this.Load += new System.EventHandler(this.frmBlank_Load);
			this.Shown += new System.EventHandler(this.frmCompare_Shown);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			this.grpSeqFile.ResumeLayout(false);
			this.grpSeqFile.PerformLayout();
			this.grpxLights.ResumeLayout(false);
			this.grpxLights.PerformLayout();
			this.grpSpreadsheet.ResumeLayout(false);
			this.grpSpreadsheet.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picDivider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private System.Windows.Forms.GroupBox grpSeqFile;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowseSequence;
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
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblPathData;
		private System.Windows.Forms.Button btnBrowseDatabase;
		private System.Windows.Forms.TextBox txtFileDatabase;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnBrowseViz;
		private System.Windows.Forms.TextBox txtFileVisual;
		private System.Windows.Forms.Label lblInfoViz;
		private System.Windows.Forms.Label lblInfoSeq;
		private System.Windows.Forms.Label lblInfoxL;
		private System.Windows.Forms.Label lblInfoDat;
		private System.Windows.Forms.Label lblInfoSheet;
		private System.Windows.Forms.PictureBox picDivider;
	}
}

