namespace UtilORama4
{
	partial class frmMerge
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMerge));
			this.treNewChannels = new System.Windows.Forms.TreeView();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowseFirst = new System.Windows.Forms.Button();
			this.txtFirstFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowseSecond = new System.Windows.Forms.Button();
			this.txtSecondFile = new System.Windows.Forms.TextBox();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.btnSave = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.staStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// treNewChannels
			// 
			this.treNewChannels.BackColor = System.Drawing.Color.White;
			this.treNewChannels.ImageIndex = 0;
			this.treNewChannels.ImageList = this.imlTreeIcons;
			this.treNewChannels.Location = new System.Drawing.Point(12, 107);
			this.treNewChannels.Name = "treNewChannels";
			this.treNewChannels.SelectedImageIndex = 0;
			this.treNewChannels.Size = new System.Drawing.Size(300, 404);
			this.treNewChannels.TabIndex = 45;
			this.treNewChannels.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treNewChannels_AfterSelect);
			// 
			// imlTreeIcons
			// 
			this.imlTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeIcons.ImageStream")));
			this.imlTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlTreeIcons.Images.SetKeyName(0, "Track");
			this.imlTreeIcons.Images.SetKeyName(1, "ChannelGroup");
			this.imlTreeIcons.Images.SetKeyName(2, "RGBchannel");
			this.imlTreeIcons.Images.SetKeyName(3, "Channel");
			this.imlTreeIcons.Images.SetKeyName(4, "FF0000");
			this.imlTreeIcons.Images.SetKeyName(5, "00FF00");
			this.imlTreeIcons.Images.SetKeyName(6, "0000FF");
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 13);
			this.label2.TabIndex = 44;
			this.label2.Text = "File to Merge";
			// 
			// btnBrowseFirst
			// 
			this.btnBrowseFirst.Location = new System.Drawing.Point(276, 25);
			this.btnBrowseFirst.Name = "btnBrowseFirst";
			this.btnBrowseFirst.Size = new System.Drawing.Size(36, 20);
			this.btnBrowseFirst.TabIndex = 43;
			this.btnBrowseFirst.Text = "...";
			this.btnBrowseFirst.UseVisualStyleBackColor = true;
			this.btnBrowseFirst.Click += new System.EventHandler(this.btnBrowseFirst_Click);
			// 
			// txtFirstFile
			// 
			this.txtFirstFile.Enabled = false;
			this.txtFirstFile.Location = new System.Drawing.Point(12, 25);
			this.txtFirstFile.Name = "txtFirstFile";
			this.txtFirstFile.Size = new System.Drawing.Size(261, 20);
			this.txtFirstFile.TabIndex = 42;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 13);
			this.label1.TabIndex = 41;
			this.label1.Text = "Original File";
			// 
			// btnBrowseSecond
			// 
			this.btnBrowseSecond.Location = new System.Drawing.Point(276, 65);
			this.btnBrowseSecond.Name = "btnBrowseSecond";
			this.btnBrowseSecond.Size = new System.Drawing.Size(36, 20);
			this.btnBrowseSecond.TabIndex = 40;
			this.btnBrowseSecond.Text = "...";
			this.btnBrowseSecond.UseVisualStyleBackColor = true;
			this.btnBrowseSecond.Click += new System.EventHandler(this.btnBrowseSecond_Click);
			// 
			// txtSecondFile
			// 
			this.txtSecondFile.Enabled = false;
			this.txtSecondFile.Location = new System.Drawing.Point(12, 65);
			this.txtSecondFile.Name = "txtSecondFile";
			this.txtSecondFile.Size = new System.Drawing.Size(261, 20);
			this.txtSecondFile.TabIndex = 39;
			// 
			// dlgOpenFile
			// 
			this.dlgOpenFile.FileName = "openFileDialog1";
			// 
			// btnSave
			// 
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(86, 517);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(151, 30);
			this.btnSave.TabIndex = 47;
			this.btnSave.Text = "Save As...";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(253, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(58, 15);
			this.button1.TabIndex = 48;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlProgress,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 554);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(324, 24);
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
			// 
			// pnlProgress
			// 
			this.pnlProgress.Name = "pnlProgress";
			this.pnlProgress.Size = new System.Drawing.Size(100, 18);
			this.pnlProgress.Visible = false;
			// 
			// pnlStatus
			// 
			this.pnlStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pnlStatus.Name = "pnlStatus";
			this.pnlStatus.Size = new System.Drawing.Size(79, 19);
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
			// 
			// frmMerge
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(324, 578);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.treNewChannels);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnBrowseFirst);
			this.Controls.Add(this.txtFirstFile);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnBrowseSecond);
			this.Controls.Add(this.txtSecondFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(340, 617);
			this.Name = "frmMerge";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Merge-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMerge_FormClosing);
			this.Load += new System.EventHandler(this.frmMerge_Load);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView treNewChannels;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowseFirst;
		private System.Windows.Forms.TextBox txtFirstFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBrowseSecond;
		private System.Windows.Forms.TextBox txtSecondFile;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripProgressBar pnlProgress;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
	}
}

