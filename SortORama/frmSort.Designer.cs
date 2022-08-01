
namespace UtilORama4
{
	partial class frmSort
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSort));
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.grpFileType = new System.Windows.Forms.GroupBox();
			this.optLORSeq = new System.Windows.Forms.RadioButton();
			this.optLORViz = new System.Windows.Forms.RadioButton();
			this.optxLights = new System.Windows.Forms.RadioButton();
			this.grpFile = new System.Windows.Forms.GroupBox();
			this.cboFile = new System.Windows.Forms.ComboBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.grpSortType = new System.Windows.Forms.GroupBox();
			this.optLayout = new System.Windows.Forms.RadioButton();
			this.optChannel = new System.Windows.Forms.RadioButton();
			this.optName = new System.Windows.Forms.RadioButton();
			this.grpSave = new System.Windows.Forms.GroupBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.grpDoSort = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			this.grpFileType.SuspendLayout();
			this.grpFile.SuspendLayout();
			this.grpSortType.SuspendLayout();
			this.grpSave.SuspendLayout();
			this.grpDoSort.SuspendLayout();
			this.SuspendLayout();
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 410);
			this.staStatus.Name = "staStatus";
			this.staStatus.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.staStatus.Size = new System.Drawing.Size(406, 24);
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
			this.pnlStatus.Size = new System.Drawing.Size(292, 19);
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
			// picAboutIcon
			// 
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(250, 250);
			this.picAboutIcon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 68;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// grpFileType
			// 
			this.grpFileType.Controls.Add(this.optxLights);
			this.grpFileType.Controls.Add(this.optLORViz);
			this.grpFileType.Controls.Add(this.optLORSeq);
			this.grpFileType.Location = new System.Drawing.Point(12, 12);
			this.grpFileType.Name = "grpFileType";
			this.grpFileType.Size = new System.Drawing.Size(385, 95);
			this.grpFileType.TabIndex = 70;
			this.grpFileType.TabStop = false;
			this.grpFileType.Text = "1: Select File Type:";
			// 
			// optLORSeq
			// 
			this.optLORSeq.AutoSize = true;
			this.optLORSeq.Checked = true;
			this.optLORSeq.Location = new System.Drawing.Point(6, 22);
			this.optLORSeq.Name = "optLORSeq";
			this.optLORSeq.Size = new System.Drawing.Size(292, 19);
			this.optLORSeq.TabIndex = 72;
			this.optLORSeq.TabStop = true;
			this.optLORSeq.Text = "Light-O-Rama Showtime S4 Sequence (*.lms, *.las)";
			this.optLORSeq.UseVisualStyleBackColor = true;
			// 
			// optLORViz
			// 
			this.optLORViz.AutoSize = true;
			this.optLORViz.Location = new System.Drawing.Point(6, 47);
			this.optLORViz.Name = "optLORViz";
			this.optLORViz.Size = new System.Drawing.Size(257, 19);
			this.optLORViz.TabIndex = 73;
			this.optLORViz.Text = "Light-O-Rama Showtime S4 Visualizer (*.viz)";
			this.optLORViz.UseVisualStyleBackColor = true;
			// 
			// optxLights
			// 
			this.optxLights.AutoSize = true;
			this.optxLights.Location = new System.Drawing.Point(6, 70);
			this.optxLights.Name = "optxLights";
			this.optxLights.Size = new System.Drawing.Size(252, 19);
			this.optxLights.TabIndex = 74;
			this.optxLights.Text = "xLights 2022.xx RGBEffects (rgbeffects.xml)";
			this.optxLights.UseVisualStyleBackColor = true;
			// 
			// grpFile
			// 
			this.grpFile.Controls.Add(this.btnBrowse);
			this.grpFile.Controls.Add(this.cboFile);
			this.grpFile.Enabled = false;
			this.grpFile.Location = new System.Drawing.Point(12, 113);
			this.grpFile.Name = "grpFile";
			this.grpFile.Size = new System.Drawing.Size(385, 58);
			this.grpFile.TabIndex = 71;
			this.grpFile.TabStop = false;
			this.grpFile.Text = "2: Select File:";
			// 
			// cboFile
			// 
			this.cboFile.FormattingEnabled = true;
			this.cboFile.Location = new System.Drawing.Point(6, 22);
			this.cboFile.Name = "cboFile";
			this.cboFile.Size = new System.Drawing.Size(269, 23);
			this.cboFile.TabIndex = 73;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(282, 22);
			this.btnBrowse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(88, 23);
			this.btnBrowse.TabIndex = 74;
			this.btnBrowse.Text = "Browse...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			// 
			// grpSortType
			// 
			this.grpSortType.Controls.Add(this.optLayout);
			this.grpSortType.Controls.Add(this.optChannel);
			this.grpSortType.Controls.Add(this.optName);
			this.grpSortType.Enabled = false;
			this.grpSortType.Location = new System.Drawing.Point(12, 177);
			this.grpSortType.Name = "grpSortType";
			this.grpSortType.Size = new System.Drawing.Size(385, 95);
			this.grpSortType.TabIndex = 72;
			this.grpSortType.TabStop = false;
			this.grpSortType.Text = "3: Select Sort:";
			// 
			// optLayout
			// 
			this.optLayout.AutoSize = true;
			this.optLayout.Location = new System.Drawing.Point(6, 70);
			this.optLayout.Name = "optLayout";
			this.optLayout.Size = new System.Drawing.Size(110, 19);
			this.optLayout.TabIndex = 74;
			this.optLayout.Text = "By Layout Order";
			this.optLayout.UseVisualStyleBackColor = true;
			// 
			// optChannel
			// 
			this.optChannel.AutoSize = true;
			this.optChannel.Location = new System.Drawing.Point(6, 47);
			this.optChannel.Name = "optChannel";
			this.optChannel.Size = new System.Drawing.Size(85, 19);
			this.optChannel.TabIndex = 73;
			this.optChannel.Text = "By Channel";
			this.optChannel.UseVisualStyleBackColor = true;
			// 
			// optName
			// 
			this.optName.AutoSize = true;
			this.optName.Checked = true;
			this.optName.Location = new System.Drawing.Point(6, 22);
			this.optName.Name = "optName";
			this.optName.Size = new System.Drawing.Size(73, 19);
			this.optName.TabIndex = 72;
			this.optName.TabStop = true;
			this.optName.Text = "By Name";
			this.optName.UseVisualStyleBackColor = true;
			// 
			// grpSave
			// 
			this.grpSave.Controls.Add(this.btnSave);
			this.grpSave.Controls.Add(this.comboBox1);
			this.grpSave.Enabled = false;
			this.grpSave.Location = new System.Drawing.Point(12, 342);
			this.grpSave.Name = "grpSave";
			this.grpSave.Size = new System.Drawing.Size(385, 58);
			this.grpSave.TabIndex = 74;
			this.grpSave.TabStop = false;
			this.grpSave.Text = "5: Save File";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(282, 22);
			this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(88, 23);
			this.btnSave.TabIndex = 74;
			this.btnSave.Text = "Save As...";
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(6, 22);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(269, 23);
			this.comboBox1.TabIndex = 73;
			// 
			// grpDoSort
			// 
			this.grpDoSort.Controls.Add(this.button1);
			this.grpDoSort.Enabled = false;
			this.grpDoSort.Location = new System.Drawing.Point(12, 278);
			this.grpDoSort.Name = "grpDoSort";
			this.grpDoSort.Size = new System.Drawing.Size(385, 58);
			this.grpDoSort.TabIndex = 75;
			this.grpDoSort.TabStop = false;
			this.grpDoSort.Text = "4: Lets Do It!";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(7, 22);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 74;
			this.button1.Text = "Sort!";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// frmSort
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(406, 434);
			this.Controls.Add(this.grpSortType);
			this.Controls.Add(this.grpSave);
			this.Controls.Add(this.grpDoSort);
			this.Controls.Add(this.picAboutIcon);
			this.Controls.Add(this.grpFile);
			this.Controls.Add(this.grpFileType);
			this.Controls.Add(this.staStatus);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "frmSort";
			this.Text = "Sort-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSort_FormClosing);
			this.Load += new System.EventHandler(this.frmSort_Load);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			this.grpFileType.ResumeLayout(false);
			this.grpFileType.PerformLayout();
			this.grpFile.ResumeLayout(false);
			this.grpSortType.ResumeLayout(false);
			this.grpSortType.PerformLayout();
			this.grpSave.ResumeLayout(false);
			this.grpDoSort.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private GroupBox grpFileType;
		private RadioButton optxLights;
		private RadioButton optLORViz;
		private RadioButton optLORSeq;
		private GroupBox grpFile;
		private ComboBox cboFile;
		private Button btnBrowse;
		private GroupBox grpSortType;
		private RadioButton optLayout;
		private RadioButton optChannel;
		private RadioButton optName;
		private GroupBox grpSave;
		private Button btnSave;
		private ComboBox comboBox1;
		private GroupBox grpDoSort;
		private Button button1;
	}
}

