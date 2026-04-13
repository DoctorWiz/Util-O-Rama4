namespace UtilORama4
{
	partial class frmList
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmList));
			Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
			staStatus = new StatusStrip();
			pnlHelp = new ToolStripStatusLabel();
			pnlStatus = new ToolStripStatusLabel();
			pnlAbout = new ToolStripStatusLabel();
			btnOK = new Button();
			picAboutIcon = new PictureBox();
			btnUniverse = new Button();
			btnController = new Button();
			btnChannel = new Button();
			btnReport = new Button();
			btnCompareLOR = new Button();
			btnComparex = new Button();
			btnFind = new Button();
			btnWiz = new Button();
			btnSave = new Button();
			tipTool = new ToolTip(components);
			btnExportSeq = new Button();
			btnRemove = new Button();
			btnMoveUp = new Button();
			btnMoveDown = new Button();
			btnSettings = new Button();
			mnuSettings = new ContextMenuStrip(components);
			changeDatabaseLocationToolStripMenuItem = new ToolStripMenuItem();
			dropModeToolStripMenuItem = new ToolStripMenuItem();
			preferencesToolStripMenuItem = new ToolStripMenuItem();
			imlTreeIcons = new ImageList(components);
			treeChannels = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
			dlgFileOpen = new OpenFileDialog();
			dlgFileSave = new SaveFileDialog();
			lblxChannel = new Label();
			lblDirty = new Label();
			lblVersions = new Label();
			lblLoading = new Label();
			staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picAboutIcon).BeginInit();
			mnuSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)treeChannels).BeginInit();
			SuspendLayout();
			// 
			// staStatus
			// 
			staStatus.AllowDrop = true;
			staStatus.ImageScalingSize = new Size(32, 32);
			staStatus.Items.AddRange(new ToolStripItem[] { pnlHelp, pnlStatus, pnlAbout });
			staStatus.Location = new Point(0, 1123);
			staStatus.Name = "staStatus";
			staStatus.Padding = new Padding(2, 0, 30, 0);
			staStatus.Size = new Size(1059, 46);
			staStatus.TabIndex = 8;
			// 
			// pnlHelp
			// 
			pnlHelp.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
			pnlHelp.BorderStyle = Border3DStyle.SunkenInner;
			pnlHelp.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
			pnlHelp.ForeColor = SystemColors.Highlight;
			pnlHelp.IsLink = true;
			pnlHelp.Name = "pnlHelp";
			pnlHelp.Size = new Size(81, 36);
			pnlHelp.Text = "Help...";
			pnlHelp.ToolTipText = "Get Help using Chan-O-Rama Channel Manager";
			pnlHelp.Click += pnlHelp_Click;
			// 
			// pnlStatus
			// 
			pnlStatus.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
			pnlStatus.BorderStyle = Border3DStyle.SunkenOuter;
			pnlStatus.Name = "pnlStatus";
			pnlStatus.Size = new Size(851, 36);
			pnlStatus.Spring = true;
			// 
			// pnlAbout
			// 
			pnlAbout.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
			pnlAbout.BorderStyle = Border3DStyle.SunkenInner;
			pnlAbout.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
			pnlAbout.ForeColor = SystemColors.Highlight;
			pnlAbout.Name = "pnlAbout";
			pnlAbout.Size = new Size(95, 36);
			pnlAbout.Text = "About...";
			pnlAbout.ToolTipText = "About Chan-O-Rama Channel Manager\r\nVersion, Copyright, License, More...";
			pnlAbout.Click += pnlAbout_Click;
			// 
			// btnOK
			// 
			btnOK.Enabled = false;
			btnOK.Location = new Point(851, 1043);
			btnOK.Margin = new Padding(7, 6, 7, 6);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(163, 58);
			btnOK.TabIndex = 11;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			btnOK.Visible = false;
			// 
			// picAboutIcon
			// 
			picAboutIcon.ErrorImage = null;
			picAboutIcon.Image = (Image)resources.GetObject("picAboutIcon.Image");
			picAboutIcon.Location = new Point(908, 875);
			picAboutIcon.Margin = new Padding(7, 6, 7, 6);
			picAboutIcon.Name = "picAboutIcon";
			picAboutIcon.Size = new Size(238, 273);
			picAboutIcon.TabIndex = 68;
			picAboutIcon.TabStop = false;
			picAboutIcon.Visible = false;
			// 
			// btnUniverse
			// 
			btnUniverse.Image = (Image)resources.GetObject("btnUniverse.Image");
			btnUniverse.ImageAlign = ContentAlignment.MiddleRight;
			btnUniverse.Location = new Point(851, 30);
			btnUniverse.Margin = new Padding(7, 6, 7, 6);
			btnUniverse.Name = "btnUniverse";
			btnUniverse.Size = new Size(163, 98);
			btnUniverse.TabIndex = 1;
			btnUniverse.Text = "Edit\r\nUniverse\r\n";
			btnUniverse.TextAlign = ContentAlignment.MiddleLeft;
			tipTool.SetToolTip(btnUniverse, "Edit the selected Universe");
			btnUniverse.UseVisualStyleBackColor = true;
			btnUniverse.Click += btnUniverse_Click;
			// 
			// btnController
			// 
			btnController.Image = (Image)resources.GetObject("btnController.Image");
			btnController.ImageAlign = ContentAlignment.MiddleRight;
			btnController.Location = new Point(851, 143);
			btnController.Margin = new Padding(7, 6, 7, 6);
			btnController.Name = "btnController";
			btnController.Size = new Size(163, 98);
			btnController.TabIndex = 2;
			btnController.Text = "Edit\r\nController";
			btnController.TextAlign = ContentAlignment.MiddleLeft;
			tipTool.SetToolTip(btnController, "Edit the selected Controller");
			btnController.UseVisualStyleBackColor = true;
			btnController.Click += btnController_Click;
			// 
			// btnChannel
			// 
			btnChannel.Enabled = false;
			btnChannel.Image = (Image)resources.GetObject("btnChannel.Image");
			btnChannel.ImageAlign = ContentAlignment.MiddleRight;
			btnChannel.Location = new Point(851, 256);
			btnChannel.Margin = new Padding(7, 6, 7, 6);
			btnChannel.Name = "btnChannel";
			btnChannel.Size = new Size(163, 98);
			btnChannel.TabIndex = 3;
			btnChannel.Text = "Edit\r\nChannel\r\n";
			btnChannel.TextAlign = ContentAlignment.MiddleLeft;
			tipTool.SetToolTip(btnChannel, "Edit the selected Channel");
			btnChannel.UseVisualStyleBackColor = true;
			btnChannel.Click += btnChannel_Click;
			// 
			// btnReport
			// 
			btnReport.Enabled = false;
			btnReport.Image = (Image)resources.GetObject("btnReport.Image");
			btnReport.ImageAlign = ContentAlignment.MiddleRight;
			btnReport.Location = new Point(851, 482);
			btnReport.Margin = new Padding(7, 6, 7, 6);
			btnReport.Name = "btnReport";
			btnReport.Size = new Size(163, 98);
			btnReport.TabIndex = 5;
			btnReport.Text = "Export\r\nCSV";
			btnReport.TextAlign = ContentAlignment.MiddleLeft;
			tipTool.SetToolTip(btnReport, "Export everything to a .CSV Spreadsheet");
			btnReport.UseVisualStyleBackColor = true;
			btnReport.Click += btnReport_Click;
			// 
			// btnCompareLOR
			// 
			btnCompareLOR.Enabled = false;
			btnCompareLOR.Location = new Point(949, 651);
			btnCompareLOR.Margin = new Padding(7, 6, 7, 6);
			btnCompareLOR.Name = "btnCompareLOR";
			btnCompareLOR.Size = new Size(163, 98);
			btnCompareLOR.TabIndex = 9;
			btnCompareLOR.Text = "Compare\r\nLOR Seq\r\n";
			btnCompareLOR.UseVisualStyleBackColor = true;
			btnCompareLOR.Visible = false;
			// 
			// btnComparex
			// 
			btnComparex.Enabled = false;
			btnComparex.Location = new Point(949, 708);
			btnComparex.Margin = new Padding(7, 6, 7, 6);
			btnComparex.Name = "btnComparex";
			btnComparex.Size = new Size(163, 98);
			btnComparex.TabIndex = 10;
			btnComparex.Text = "Compare\r\nxLights\r\n";
			btnComparex.UseVisualStyleBackColor = true;
			btnComparex.Visible = false;
			// 
			// btnFind
			// 
			btnFind.Enabled = false;
			btnFind.Image = (Image)resources.GetObject("btnFind.Image");
			btnFind.ImageAlign = ContentAlignment.MiddleRight;
			btnFind.Location = new Point(851, 369);
			btnFind.Margin = new Padding(7, 6, 7, 6);
			btnFind.Name = "btnFind";
			btnFind.Size = new Size(163, 98);
			btnFind.TabIndex = 4;
			btnFind.Text = "Find...";
			btnFind.TextAlign = ContentAlignment.MiddleLeft;
			tipTool.SetToolTip(btnFind, "Find a Channel...");
			btnFind.UseVisualStyleBackColor = true;
			// 
			// btnWiz
			// 
			btnWiz.Image = (Image)resources.GetObject("btnWiz.Image");
			btnWiz.Location = new Point(851, 595);
			btnWiz.Margin = new Padding(7, 6, 7, 6);
			btnWiz.Name = "btnWiz";
			btnWiz.Size = new Size(163, 98);
			btnWiz.TabIndex = 6;
			tipTool.SetToolTip(btnWiz, "Channel Comparison Wizard\r\n\r\nCompare Managed Channels to:\r\n    Light-O-Rama Showtime S4 Channels\r\n    Light-O-Rama Visualizer Channels & Groups\r\n    xLights Models and Groups");
			btnWiz.UseVisualStyleBackColor = true;
			btnWiz.Visible = false;
			btnWiz.Click += btnWiz_Click;
			// 
			// btnSave
			// 
			btnSave.Image = (Image)resources.GetObject("btnSave.Image");
			btnSave.ImageAlign = ContentAlignment.MiddleRight;
			btnSave.Location = new Point(851, 930);
			btnSave.Margin = new Padding(7, 6, 7, 6);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(163, 98);
			btnSave.TabIndex = 7;
			btnSave.Text = "Save";
			btnSave.TextAlign = ContentAlignment.MiddleLeft;
			tipTool.SetToolTip(btnSave, "Save Everything!");
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// btnExportSeq
			// 
			btnExportSeq.Enabled = false;
			btnExportSeq.Image = (Image)resources.GetObject("btnExportSeq.Image");
			btnExportSeq.ImageAlign = ContentAlignment.MiddleRight;
			btnExportSeq.Location = new Point(949, 764);
			btnExportSeq.Margin = new Padding(7, 6, 7, 6);
			btnExportSeq.Name = "btnExportSeq";
			btnExportSeq.Size = new Size(163, 98);
			btnExportSeq.TabIndex = 119;
			btnExportSeq.Text = "Export\r\nSequence";
			btnExportSeq.TextAlign = ContentAlignment.MiddleLeft;
			tipTool.SetToolTip(btnExportSeq, "Export everything to a .CSV Spreadsheet");
			btnExportSeq.UseVisualStyleBackColor = true;
			btnExportSeq.Visible = false;
			// 
			// btnRemove
			// 
			btnRemove.Image = (Image)resources.GetObject("btnRemove.Image");
			btnRemove.ImageAlign = ContentAlignment.MiddleRight;
			btnRemove.Location = new Point(851, 706);
			btnRemove.Margin = new Padding(7, 6, 7, 6);
			btnRemove.Name = "btnRemove";
			btnRemove.Size = new Size(163, 98);
			btnRemove.TabIndex = 120;
			btnRemove.Text = "Remove\r\nChannel";
			btnRemove.TextAlign = ContentAlignment.MiddleLeft;
			tipTool.SetToolTip(btnRemove, "Export everything to a .CSV Spreadsheet");
			btnRemove.UseVisualStyleBackColor = true;
			btnRemove.Visible = false;
			btnRemove.Click += btnRemove_Click;
			// 
			// btnMoveUp
			// 
			btnMoveUp.Enabled = false;
			btnMoveUp.ImageAlign = ContentAlignment.MiddleRight;
			btnMoveUp.Location = new Point(372, 1076);
			btnMoveUp.Margin = new Padding(7, 6, 7, 6);
			btnMoveUp.Name = "btnMoveUp";
			btnMoveUp.Size = new Size(89, 45);
			btnMoveUp.TabIndex = 123;
			btnMoveUp.Text = "^";
			tipTool.SetToolTip(btnMoveUp, "Move Up");
			btnMoveUp.UseVisualStyleBackColor = true;
			// 
			// btnMoveDown
			// 
			btnMoveDown.Enabled = false;
			btnMoveDown.ImageAlign = ContentAlignment.MiddleRight;
			btnMoveDown.Location = new Point(475, 1075);
			btnMoveDown.Margin = new Padding(7, 6, 7, 6);
			btnMoveDown.Name = "btnMoveDown";
			btnMoveDown.Size = new Size(89, 45);
			btnMoveDown.TabIndex = 124;
			btnMoveDown.Text = "v";
			tipTool.SetToolTip(btnMoveDown, "Move Down");
			btnMoveDown.UseVisualStyleBackColor = true;
			// 
			// btnSettings
			// 
			btnSettings.ContextMenuStrip = mnuSettings;
			btnSettings.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnSettings.Image = (Image)resources.GetObject("btnSettings.Image");
			btnSettings.ImageAlign = ContentAlignment.MiddleRight;
			btnSettings.Location = new Point(626, 1071);
			btnSettings.Margin = new Padding(7, 6, 7, 6);
			btnSettings.Name = "btnSettings";
			btnSettings.Size = new Size(69, 46);
			btnSettings.TabIndex = 125;
			btnSettings.Text = "*";
			tipTool.SetToolTip(btnSettings, "Settings");
			btnSettings.UseVisualStyleBackColor = true;
			btnSettings.Click += btnSettings_Click;
			// 
			// mnuSettings
			// 
			mnuSettings.ImageScalingSize = new Size(32, 32);
			mnuSettings.Items.AddRange(new ToolStripItem[] { changeDatabaseLocationToolStripMenuItem, dropModeToolStripMenuItem, preferencesToolStripMenuItem });
			mnuSettings.Name = "mnuSettings";
			mnuSettings.Size = new Size(373, 118);
			// 
			// changeDatabaseLocationToolStripMenuItem
			// 
			changeDatabaseLocationToolStripMenuItem.Name = "changeDatabaseLocationToolStripMenuItem";
			changeDatabaseLocationToolStripMenuItem.Size = new Size(372, 38);
			changeDatabaseLocationToolStripMenuItem.Text = "Change Database Location";
			changeDatabaseLocationToolStripMenuItem.Click += changeDatabaseLocationToolStripMenuItem_Click;
			// 
			// dropModeToolStripMenuItem
			// 
			dropModeToolStripMenuItem.Name = "dropModeToolStripMenuItem";
			dropModeToolStripMenuItem.Size = new Size(372, 38);
			dropModeToolStripMenuItem.Text = "Drop Mode";
			dropModeToolStripMenuItem.Click += dropModeToolStripMenuItem_Click;
			// 
			// preferencesToolStripMenuItem
			// 
			preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
			preferencesToolStripMenuItem.Size = new Size(372, 38);
			preferencesToolStripMenuItem.Text = "Preferences";
			preferencesToolStripMenuItem.Click += preferencesToolStripMenuItem_Click;
			// 
			// imlTreeIcons
			// 
			imlTreeIcons.ColorDepth = ColorDepth.Depth32Bit;
			imlTreeIcons.ImageStream = (ImageListStreamer)resources.GetObject("imlTreeIcons.ImageStream");
			imlTreeIcons.TransparentColor = Color.Transparent;
			imlTreeIcons.Images.SetKeyName(0, "Universe");
			imlTreeIcons.Images.SetKeyName(1, "Controller");
			imlTreeIcons.Images.SetKeyName(2, "RGBChannel");
			imlTreeIcons.Images.SetKeyName(3, "RGBWChannel");
			imlTreeIcons.Images.SetKeyName(4, "MulticolorChannel");
			imlTreeIcons.Images.SetKeyName(5, "#000000");
			imlTreeIcons.Images.SetKeyName(6, "#D0FFFF");
			imlTreeIcons.Images.SetKeyName(7, "#FFE0D0");
			imlTreeIcons.Images.SetKeyName(8, "#FF0000");
			imlTreeIcons.Images.SetKeyName(9, "#FF8000");
			imlTreeIcons.Images.SetKeyName(10, "#FFFF00");
			imlTreeIcons.Images.SetKeyName(11, "#00FF00");
			imlTreeIcons.Images.SetKeyName(12, "#0000FF");
			imlTreeIcons.Images.SetKeyName(13, "#8000FF");
			imlTreeIcons.Images.SetKeyName(14, "#FF80FF");
			imlTreeIcons.Images.SetKeyName(15, "#000001");
			imlTreeIcons.Images.SetKeyName(16, "#000100");
			imlTreeIcons.Images.SetKeyName(17, "#010000");
			// 
			// treeChannels
			// 
			treeChannels.BackColor = Color.White;
			treeNodeAdvStyleInfo1.CheckBoxTickThickness = 1;
			treeNodeAdvStyleInfo1.CheckColor = Color.FromArgb(109, 109, 109);
			treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
			treeNodeAdvStyleInfo1.IntermediateCheckColor = Color.FromArgb(109, 109, 109);
			treeNodeAdvStyleInfo1.OptionButtonColor = Color.FromArgb(109, 109, 109);
			treeNodeAdvStyleInfo1.SelectedOptionButtonColor = Color.FromArgb(210, 210, 210);
			treeNodeAdvStyleInfo1.TextColor = Color.Black;
			treeChannels.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] { new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1) });
			treeChannels.BeforeTouchSize = new Size(775, 1034);
			treeChannels.ForeColor = Color.Black;
			// 
			// 
			// 
			treeChannels.HelpTextControl.BaseThemeName = null;
			treeChannels.HelpTextControl.Location = new Point(0, 0);
			treeChannels.HelpTextControl.Name = "";
			treeChannels.HelpTextControl.Size = new Size(392, 112);
			treeChannels.HelpTextControl.TabIndex = 0;
			treeChannels.HelpTextControl.Visible = true;
			treeChannels.InactiveSelectedNodeForeColor = SystemColors.ControlText;
			treeChannels.LeftImageList = imlTreeIcons;
			treeChannels.Location = new Point(26, 30);
			treeChannels.Margin = new Padding(7, 6, 7, 6);
			treeChannels.MetroColor = Color.FromArgb(22, 165, 220);
			treeChannels.Name = "treeChannels";
			treeChannels.SelectedNodeForeColor = SystemColors.HighlightText;
			treeChannels.Size = new Size(775, 1034);
			treeChannels.TabIndex = 118;
			treeChannels.Text = "Source Channels";
			treeChannels.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 0;
			treeChannels.ThemeStyle.TreeNodeAdvStyle.EnsureDefaultOptionedChild = true;
			// 
			// 
			// 
			treeChannels.ToolTipControl.BaseThemeName = null;
			treeChannels.ToolTipControl.Location = new Point(0, 0);
			treeChannels.ToolTipControl.Name = "";
			treeChannels.ToolTipControl.Size = new Size(392, 112);
			treeChannels.ToolTipControl.TabIndex = 0;
			treeChannels.ToolTipControl.Visible = true;
			treeChannels.AfterSelect += treeChannels_AfterSelect;
			treeChannels.DoubleClick += treeChannelList_DoubleClick;
			treeChannels.KeyPress += treeChannels_KeyPress;
			treeChannels.KeyUp += treeChannels_KeyUp;
			// 
			// dlgFileOpen
			// 
			dlgFileOpen.FileName = "openFileDialog1";
			// 
			// lblxChannel
			// 
			lblxChannel.AutoSize = true;
			lblxChannel.ForeColor = SystemColors.HotTrack;
			lblxChannel.Location = new Point(111, 1075);
			lblxChannel.Margin = new Padding(6, 0, 6, 0);
			lblxChannel.Name = "lblxChannel";
			lblxChannel.Size = new Size(29, 32);
			lblxChannel.TabIndex = 121;
			lblxChannel.Text = "...";
			// 
			// lblDirty
			// 
			lblDirty.AutoSize = true;
			lblDirty.ForeColor = SystemColors.HotTrack;
			lblDirty.Location = new Point(219, 1075);
			lblDirty.Margin = new Padding(6, 0, 6, 0);
			lblDirty.Name = "lblDirty";
			lblDirty.Size = new Size(29, 32);
			lblDirty.TabIndex = 122;
			lblDirty.Text = "...";
			// 
			// lblVersions
			// 
			lblVersions.AutoSize = true;
			lblVersions.ForeColor = Color.DarkGreen;
			lblVersions.Location = new Point(729, 1070);
			lblVersions.Margin = new Padding(6, 0, 6, 0);
			lblVersions.Name = "lblVersions";
			lblVersions.Size = new Size(29, 32);
			lblVersions.TabIndex = 126;
			lblVersions.Text = "...";
			// 
			// lblLoading
			// 
			lblLoading.AutoSize = true;
			lblLoading.BackColor = Color.White;
			lblLoading.Font = new Font("Segoe UI", 13.875F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			lblLoading.ForeColor = Color.OrangeRed;
			lblLoading.Location = new Point(321, 228);
			lblLoading.Margin = new Padding(6, 0, 6, 0);
			lblLoading.Name = "lblLoading";
			lblLoading.Size = new Size(192, 50);
			lblLoading.TabIndex = 127;
			lblLoading.Text = "Loading...";
			// 
			// frmList
			// 
			AcceptButton = btnOK;
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1059, 1169);
			Controls.Add(lblLoading);
			Controls.Add(lblVersions);
			Controls.Add(btnSettings);
			Controls.Add(btnMoveDown);
			Controls.Add(btnMoveUp);
			Controls.Add(lblDirty);
			Controls.Add(lblxChannel);
			Controls.Add(btnRemove);
			Controls.Add(btnExportSeq);
			Controls.Add(treeChannels);
			Controls.Add(btnSave);
			Controls.Add(btnWiz);
			Controls.Add(btnFind);
			Controls.Add(btnComparex);
			Controls.Add(btnCompareLOR);
			Controls.Add(btnReport);
			Controls.Add(btnChannel);
			Controls.Add(btnController);
			Controls.Add(btnUniverse);
			Controls.Add(btnOK);
			Controls.Add(staStatus);
			Controls.Add(picAboutIcon);
			Cursor = Cursors.WaitCursor;
			Enabled = false;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(7, 6, 7, 6);
			MaximizeBox = false;
			MaximumSize = new Size(1707, 2245);
			MinimumSize = new Size(732, 892);
			Name = "frmList";
			Text = "Chan-O-Rama  Channel Manager";
			FormClosing += frmList_FormClosing;
			Load += frmList_Load;
			Shown += frmList_Shown;
			Click += frmList_Click;
			Resize += frmList_Resize;
			staStatus.ResumeLayout(false);
			staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)picAboutIcon).EndInit();
			mnuSettings.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)treeChannels).EndInit();
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private System.Windows.Forms.Button btnUniverse;
		private System.Windows.Forms.Button btnController;
		private System.Windows.Forms.Button btnChannel;
		private System.Windows.Forms.Button btnReport;
		private System.Windows.Forms.Button btnCompareLOR;
		private System.Windows.Forms.Button btnComparex;
		private System.Windows.Forms.Button btnFind;
		private System.Windows.Forms.Button btnWiz;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ToolTip tipTool;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeChannels;
		private System.Windows.Forms.Button btnExportSeq;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
		private Button btnRemove;
		private Label lblxChannel;
		private Label lblDirty;
		private Button btnMoveUp;
		private Button btnMoveDown;
		private Button btnSettings;
		private ContextMenuStrip mnuSettings;
		private ToolStripMenuItem changeDatabaseLocationToolStripMenuItem;
		private ToolStripMenuItem dropModeToolStripMenuItem;
		private ToolStripMenuItem preferencesToolStripMenuItem;
		private Label lblVersions;
		private Label lblLoading;
	}
}

