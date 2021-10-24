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
			Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowseFirst = new System.Windows.Forms.Button();
			this.txtFirstFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowseSecond = new System.Windows.Forms.Button();
			this.txtSecondFile = new System.Windows.Forms.TextBox();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.btnSave = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.treeNewChannels = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.chkAutoLaunch = new System.Windows.Forms.CheckBox();
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeNewChannels)).BeginInit();
			this.SuspendLayout();
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
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
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
			this.pnlStatus.Size = new System.Drawing.Size(212, 19);
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
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(229, 286);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 130;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// treeNewChannels
			// 
			this.treeNewChannels.BackColor = System.Drawing.Color.White;
			treeNodeAdvStyleInfo1.CheckBoxTickThickness = 1;
			treeNodeAdvStyleInfo1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
			treeNodeAdvStyleInfo1.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
			this.treeNewChannels.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
			this.treeNewChannels.BeforeTouchSize = new System.Drawing.Size(300, 400);
			// 
			// 
			// 
			this.treeNewChannels.HelpTextControl.BaseThemeName = null;
			this.treeNewChannels.HelpTextControl.Location = new System.Drawing.Point(0, 0);
			this.treeNewChannels.HelpTextControl.Name = "";
			this.treeNewChannels.HelpTextControl.Size = new System.Drawing.Size(392, 112);
			this.treeNewChannels.HelpTextControl.TabIndex = 0;
			this.treeNewChannels.HelpTextControl.Visible = true;
			this.treeNewChannels.InactiveSelectedNodeForeColor = System.Drawing.SystemColors.ControlText;
			this.treeNewChannels.LeftImageList = this.imlTreeIcons;
			this.treeNewChannels.Location = new System.Drawing.Point(12, 100);
			this.treeNewChannels.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
			this.treeNewChannels.Name = "treeNewChannels";
			this.treeNewChannels.NodeStateImageList = this.imlTreeIcons;
			this.treeNewChannels.SelectedNodeForeColor = System.Drawing.SystemColors.HighlightText;
			this.treeNewChannels.Size = new System.Drawing.Size(300, 400);
			this.treeNewChannels.TabIndex = 131;
			this.treeNewChannels.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 0;
			this.treeNewChannels.ThemeStyle.TreeNodeAdvStyle.EnsureDefaultOptionedChild = true;
			// 
			// 
			// 
			this.treeNewChannels.ToolTipControl.BaseThemeName = null;
			this.treeNewChannels.ToolTipControl.Location = new System.Drawing.Point(0, 0);
			this.treeNewChannels.ToolTipControl.Name = "";
			this.treeNewChannels.ToolTipControl.Size = new System.Drawing.Size(392, 112);
			this.treeNewChannels.ToolTipControl.TabIndex = 0;
			this.treeNewChannels.ToolTipControl.Visible = true;
			// 
			// imlTreeIcons
			// 
			this.imlTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeIcons.ImageStream")));
			this.imlTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlTreeIcons.Images.SetKeyName(0, "Universe");
			this.imlTreeIcons.Images.SetKeyName(1, "Controller");
			this.imlTreeIcons.Images.SetKeyName(2, "Track");
			this.imlTreeIcons.Images.SetKeyName(3, "Channel");
			this.imlTreeIcons.Images.SetKeyName(4, "RGBChannel");
			this.imlTreeIcons.Images.SetKeyName(5, "ChannelGroup");
			this.imlTreeIcons.Images.SetKeyName(6, "CosmicDevice");
			this.imlTreeIcons.Images.SetKeyName(7, "#400000");
			this.imlTreeIcons.Images.SetKeyName(8, "#804040");
			this.imlTreeIcons.Images.SetKeyName(9, "#FF0000");
			this.imlTreeIcons.Images.SetKeyName(10, "#00FF00");
			this.imlTreeIcons.Images.SetKeyName(11, "#0000FF");
			this.imlTreeIcons.Images.SetKeyName(12, "#FFFFFF");
			this.imlTreeIcons.Images.SetKeyName(13, "#000000");
			this.imlTreeIcons.Images.SetKeyName(14, "#8000FF");
			this.imlTreeIcons.Images.SetKeyName(15, "#FF8000");
			this.imlTreeIcons.Images.SetKeyName(16, "#FFFF00");
			this.imlTreeIcons.Images.SetKeyName(17, "#FF80FF");
			this.imlTreeIcons.Images.SetKeyName(18, "#00FFFF");
			this.imlTreeIcons.Images.SetKeyName(19, "#000080");
			this.imlTreeIcons.Images.SetKeyName(20, "#008000");
			this.imlTreeIcons.Images.SetKeyName(21, "#008080");
			this.imlTreeIcons.Images.SetKeyName(22, "#8080FF");
			this.imlTreeIcons.Images.SetKeyName(23, "#400080");
			this.imlTreeIcons.Images.SetKeyName(24, "#404040");
			this.imlTreeIcons.Images.SetKeyName(25, "#408080");
			this.imlTreeIcons.Images.SetKeyName(26, "#800000");
			this.imlTreeIcons.Images.SetKeyName(27, "#800080");
			this.imlTreeIcons.Images.SetKeyName(28, "#808000");
			this.imlTreeIcons.Images.SetKeyName(29, "#808080");
			this.imlTreeIcons.Images.SetKeyName(30, "#C0C0C0");
			this.imlTreeIcons.Images.SetKeyName(31, "#FF00FF");
			// 
			// chkAutoLaunch
			// 
			this.chkAutoLaunch.AutoSize = true;
			this.chkAutoLaunch.Checked = true;
			this.chkAutoLaunch.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoLaunch.Location = new System.Drawing.Point(243, 525);
			this.chkAutoLaunch.Name = "chkAutoLaunch";
			this.chkAutoLaunch.Size = new System.Drawing.Size(62, 17);
			this.chkAutoLaunch.TabIndex = 132;
			this.chkAutoLaunch.Text = "Launch";
			this.chkAutoLaunch.UseVisualStyleBackColor = true;
			// 
			// frmMerge
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(324, 578);
			this.Controls.Add(this.chkAutoLaunch);
			this.Controls.Add(this.treeNewChannels);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnBrowseFirst);
			this.Controls.Add(this.txtFirstFile);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnBrowseSecond);
			this.Controls.Add(this.txtSecondFile);
			this.Controls.Add(this.picAboutIcon);
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
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeNewChannels)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowseFirst;
		private System.Windows.Forms.TextBox txtFirstFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBrowseSecond;
		private System.Windows.Forms.TextBox txtSecondFile;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripProgressBar pnlProgress;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeNewChannels;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.CheckBox chkAutoLaunch;
	}
}

