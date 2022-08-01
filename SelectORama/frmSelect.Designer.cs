namespace UtilORama4
{
	partial class frmSelect
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
			Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo2 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelect));
			this.lblSelectionsList = new System.Windows.Forms.Label();
			this.btnBrowseSequence = new System.Windows.Forms.Button();
			this.lblSequenceFile = new System.Windows.Forms.Label();
			this.btnBrowseSelections = new System.Windows.Forms.Button();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.btnInvert = new System.Windows.Forms.Button();
			this.btnSaveSelections = new System.Windows.Forms.Button();
			this.lblSelectionCount = new System.Windows.Forms.Label();
			this.ttip = new System.Windows.Forms.ToolTip(this.components);
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnAll = new System.Windows.Forms.Button();
			this.lblSelections = new System.Windows.Forms.Label();
			this.lblTreeClicks = new System.Windows.Forms.Label();
			this.lblChannels = new System.Windows.Forms.Label();
			this.lblTimingGrids = new System.Windows.Forms.Label();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.treeSequence = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.cboSequenceFile = new System.Windows.Forms.ComboBox();
			this.cboSelectionsFile = new System.Windows.Forms.ComboBox();
			this.btnOpenSequence = new System.Windows.Forms.Button();
			this.btnOpenSelections = new System.Windows.Forms.Button();
			this.lblCentiseconds = new System.Windows.Forms.Label();
			this.lstTimingGrids = new System.Windows.Forms.ListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colType = new System.Windows.Forms.ColumnHeader();
			this.colTimes = new System.Windows.Forms.ColumnHeader();
			this.chkRamps = new System.Windows.Forms.CheckBox();
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeSequence)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// lblSelectionsList
			// 
			this.lblSelectionsList.AllowDrop = true;
			this.lblSelectionsList.AutoSize = true;
			this.lblSelectionsList.Location = new System.Drawing.Point(14, 66);
			this.lblSelectionsList.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSelectionsList.Name = "lblSelectionsList";
			this.lblSelectionsList.Size = new System.Drawing.Size(81, 15);
			this.lblSelectionsList.TabIndex = 3;
			this.lblSelectionsList.Text = "Selections List";
			this.lblSelectionsList.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSelectionsList.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnBrowseSequence
			// 
			this.btnBrowseSequence.AllowDrop = true;
			this.btnBrowseSequence.Location = new System.Drawing.Point(280, 29);
			this.btnBrowseSequence.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnBrowseSequence.Name = "btnBrowseSequence";
			this.btnBrowseSequence.Size = new System.Drawing.Size(88, 23);
			this.btnBrowseSequence.TabIndex = 2;
			this.btnBrowseSequence.Text = "Browse...";
			this.btnBrowseSequence.UseVisualStyleBackColor = true;
			this.btnBrowseSequence.Click += new System.EventHandler(this.btnBrowseSeq_Click);
			this.btnBrowseSequence.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseSequence.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSequenceFile
			// 
			this.lblSequenceFile.AllowDrop = true;
			this.lblSequenceFile.AutoSize = true;
			this.lblSequenceFile.Location = new System.Drawing.Point(14, 10);
			this.lblSequenceFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSequenceFile.Name = "lblSequenceFile";
			this.lblSequenceFile.Size = new System.Drawing.Size(79, 15);
			this.lblSequenceFile.TabIndex = 100;
			this.lblSequenceFile.Text = "Sequence File";
			this.lblSequenceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSequenceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnBrowseSelections
			// 
			this.btnBrowseSelections.AllowDrop = true;
			this.btnBrowseSelections.Location = new System.Drawing.Point(280, 83);
			this.btnBrowseSelections.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnBrowseSelections.Name = "btnBrowseSelections";
			this.btnBrowseSelections.Size = new System.Drawing.Size(88, 23);
			this.btnBrowseSelections.TabIndex = 5;
			this.btnBrowseSelections.Text = "Browse...";
			this.btnBrowseSelections.UseVisualStyleBackColor = true;
			this.btnBrowseSelections.Click += new System.EventHandler(this.btnBrowseSelections_Click);
			this.btnBrowseSelections.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseSelections.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
			// 
			// btnInvert
			// 
			this.btnInvert.AllowDrop = true;
			this.btnInvert.Location = new System.Drawing.Point(112, 654);
			this.btnInvert.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnInvert.Name = "btnInvert";
			this.btnInvert.Size = new System.Drawing.Size(88, 27);
			this.btnInvert.TabIndex = 12;
			this.btnInvert.Text = "Invert";
			this.btnInvert.UseVisualStyleBackColor = true;
			this.btnInvert.Click += new System.EventHandler(this.btnInvert_Click);
			this.btnInvert.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnInvert.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnSaveSelections
			// 
			this.btnSaveSelections.AllowDrop = true;
			this.btnSaveSelections.Enabled = false;
			this.btnSaveSelections.Location = new System.Drawing.Point(280, 122);
			this.btnSaveSelections.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnSaveSelections.Name = "btnSaveSelections";
			this.btnSaveSelections.Size = new System.Drawing.Size(88, 23);
			this.btnSaveSelections.TabIndex = 6;
			this.btnSaveSelections.Text = "Save As...";
			this.btnSaveSelections.UseVisualStyleBackColor = true;
			this.btnSaveSelections.Visible = false;
			this.btnSaveSelections.Click += new System.EventHandler(this.btnSaveSelections_Click);
			this.btnSaveSelections.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSaveSelections.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSelectionCount
			// 
			this.lblSelectionCount.AllowDrop = true;
			this.lblSelectionCount.AutoSize = true;
			this.lblSelectionCount.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblSelectionCount.Location = new System.Drawing.Point(240, 66);
			this.lblSelectionCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSelectionCount.Name = "lblSelectionCount";
			this.lblSelectionCount.Size = new System.Drawing.Size(13, 15);
			this.lblSelectionCount.TabIndex = 11;
			this.lblSelectionCount.Text = "0";
			this.lblSelectionCount.Click += new System.EventHandler(this.lblSelectionCount_Click);
			this.lblSelectionCount.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSelectionCount.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlProgress,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 687);
			this.staStatus.Name = "staStatus";
			this.staStatus.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.staStatus.Size = new System.Drawing.Size(390, 24);
			this.staStatus.TabIndex = 61;
			this.staStatus.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.staStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
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
			// pnlProgress
			// 
			this.pnlProgress.Name = "pnlProgress";
			this.pnlProgress.Size = new System.Drawing.Size(117, 21);
			this.pnlProgress.Visible = false;
			// 
			// pnlStatus
			// 
			this.pnlStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pnlStatus.Name = "pnlStatus";
			this.pnlStatus.Size = new System.Drawing.Size(276, 19);
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
			// btnClear
			// 
			this.btnClear.AllowDrop = true;
			this.btnClear.Enabled = false;
			this.btnClear.Location = new System.Drawing.Point(206, 654);
			this.btnClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(88, 27);
			this.btnClear.TabIndex = 13;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			this.btnClear.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnClear.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnAll
			// 
			this.btnAll.AllowDrop = true;
			this.btnAll.Location = new System.Drawing.Point(18, 654);
			this.btnAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnAll.Name = "btnAll";
			this.btnAll.Size = new System.Drawing.Size(88, 27);
			this.btnAll.TabIndex = 14;
			this.btnAll.Text = "All";
			this.btnAll.UseVisualStyleBackColor = true;
			this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
			this.btnAll.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnAll.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSelections
			// 
			this.lblSelections.AllowDrop = true;
			this.lblSelections.AutoSize = true;
			this.lblSelections.Location = new System.Drawing.Point(175, 66);
			this.lblSelections.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSelections.Name = "lblSelections";
			this.lblSelections.Size = new System.Drawing.Size(63, 15);
			this.lblSelections.TabIndex = 10;
			this.lblSelections.Text = "Selections:";
			this.lblSelections.Click += new System.EventHandler(this.lblSelections_Click);
			this.lblSelections.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSelections.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblTreeClicks
			// 
			this.lblTreeClicks.AllowDrop = true;
			this.lblTreeClicks.AutoSize = true;
			this.lblTreeClicks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.lblTreeClicks.ForeColor = System.Drawing.Color.DarkRed;
			this.lblTreeClicks.Location = new System.Drawing.Point(216, 122);
			this.lblTreeClicks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTreeClicks.Name = "lblTreeClicks";
			this.lblTreeClicks.Size = new System.Drawing.Size(13, 13);
			this.lblTreeClicks.TabIndex = 105;
			this.lblTreeClicks.Text = "0";
			// 
			// lblChannels
			// 
			this.lblChannels.AllowDrop = true;
			this.lblChannels.AutoSize = true;
			this.lblChannels.Location = new System.Drawing.Point(14, 122);
			this.lblChannels.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblChannels.Name = "lblChannels";
			this.lblChannels.Size = new System.Drawing.Size(164, 15);
			this.lblChannels.TabIndex = 108;
			this.lblChannels.Text = "Tracks, Groups, and Channels:";
			// 
			// lblTimingGrids
			// 
			this.lblTimingGrids.AllowDrop = true;
			this.lblTimingGrids.AutoSize = true;
			this.lblTimingGrids.Location = new System.Drawing.Point(14, 534);
			this.lblTimingGrids.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTimingGrids.Name = "lblTimingGrids";
			this.lblTimingGrids.Size = new System.Drawing.Size(77, 15);
			this.lblTimingGrids.TabIndex = 109;
			this.lblTimingGrids.Text = "Timing Grids:";
			// 
			// picPreview
			// 
			this.picPreview.BackColor = System.Drawing.Color.Tan;
			this.picPreview.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picPreview.ErrorImage = null;
			this.picPreview.InitialImage = null;
			this.picPreview.Location = new System.Drawing.Point(18, 494);
			this.picPreview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(350, 36);
			this.picPreview.TabIndex = 110;
			this.picPreview.TabStop = false;
			// 
			// treeSequence
			// 
			this.treeSequence.BackColor = System.Drawing.Color.White;
			treeNodeAdvStyleInfo2.CheckBoxTickThickness = 1;
			treeNodeAdvStyleInfo2.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo2.EnsureDefaultOptionedChild = true;
			treeNodeAdvStyleInfo2.InteractiveCheckBox = true;
			treeNodeAdvStyleInfo2.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo2.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo2.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
			treeNodeAdvStyleInfo2.ShowCheckBox = true;
			treeNodeAdvStyleInfo2.TextColor = System.Drawing.Color.Black;
			this.treeSequence.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo2)});
			this.treeSequence.BeforeTouchSize = new System.Drawing.Size(349, 346);
			this.treeSequence.ForeColor = System.Drawing.Color.Black;
			// 
			// 
			// 
			this.treeSequence.HelpTextControl.BaseThemeName = null;
			this.treeSequence.HelpTextControl.Location = new System.Drawing.Point(0, 0);
			this.treeSequence.HelpTextControl.Name = "";
			this.treeSequence.HelpTextControl.Size = new System.Drawing.Size(392, 112);
			this.treeSequence.HelpTextControl.TabIndex = 0;
			this.treeSequence.HelpTextControl.Visible = true;
			this.treeSequence.InactiveSelectedNodeForeColor = System.Drawing.SystemColors.ControlText;
			this.treeSequence.InteractiveCheckBoxes = true;
			this.treeSequence.LeftImageList = this.imlTreeIcons;
			this.treeSequence.Location = new System.Drawing.Point(18, 141);
			this.treeSequence.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.treeSequence.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
			this.treeSequence.Name = "treeSequence";
			this.treeSequence.SelectedNodeForeColor = System.Drawing.SystemColors.HighlightText;
			this.treeSequence.ShowCheckBoxes = true;
			this.treeSequence.Size = new System.Drawing.Size(349, 346);
			this.treeSequence.TabIndex = 132;
			this.treeSequence.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 0;
			this.treeSequence.ThemeStyle.TreeNodeAdvStyle.EnsureDefaultOptionedChild = true;
			// 
			// 
			// 
			this.treeSequence.ToolTipControl.BaseThemeName = null;
			this.treeSequence.ToolTipControl.Location = new System.Drawing.Point(0, 0);
			this.treeSequence.ToolTipControl.Name = "";
			this.treeSequence.ToolTipControl.Size = new System.Drawing.Size(392, 112);
			this.treeSequence.ToolTipControl.TabIndex = 0;
			this.treeSequence.ToolTipControl.Visible = true;
			this.treeSequence.BeforeSelect += new Syncfusion.Windows.Forms.Tools.TreeNodeAdvBeforeSelectEventHandler(this.treeChannels_BeforeSelect);
			this.treeSequence.AfterSelect += new System.EventHandler(this.treeChannels_AfterSelect);
			this.treeSequence.AfterCheck += new Syncfusion.Windows.Forms.Tools.TreeNodeAdvEventHandler(this.treeChannels_AfterCheck);
			this.treeSequence.Click += new System.EventHandler(this.treeChannels_Click);
			// 
			// imlTreeIcons
			// 
			this.imlTreeIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
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
			// picAboutIcon
			// 
			this.picAboutIcon.BackColor = System.Drawing.Color.White;
			this.picAboutIcon.Enabled = false;
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(125, 230);
			this.picAboutIcon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 133;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Click += new System.EventHandler(this.picAboutIcon_Click);
			// 
			// cboSequenceFile
			// 
			this.cboSequenceFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSequenceFile.FormattingEnabled = true;
			this.cboSequenceFile.Location = new System.Drawing.Point(18, 30);
			this.cboSequenceFile.Name = "cboSequenceFile";
			this.cboSequenceFile.Size = new System.Drawing.Size(255, 23);
			this.cboSequenceFile.TabIndex = 134;
			this.cboSequenceFile.SelectedIndexChanged += new System.EventHandler(this.cboSequenceFile_SelectedIndexChanged);
			// 
			// cboSelectionsFile
			// 
			this.cboSelectionsFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSelectionsFile.FormattingEnabled = true;
			this.cboSelectionsFile.Location = new System.Drawing.Point(18, 84);
			this.cboSelectionsFile.Name = "cboSelectionsFile";
			this.cboSelectionsFile.Size = new System.Drawing.Size(255, 23);
			this.cboSelectionsFile.TabIndex = 135;
			this.cboSelectionsFile.SelectedIndexChanged += new System.EventHandler(this.cboSelectionsFile_SelectedIndexChanged);
			// 
			// btnOpenSequence
			// 
			this.btnOpenSequence.AllowDrop = true;
			this.btnOpenSequence.Enabled = false;
			this.btnOpenSequence.Location = new System.Drawing.Point(322, 16);
			this.btnOpenSequence.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnOpenSequence.Name = "btnOpenSequence";
			this.btnOpenSequence.Size = new System.Drawing.Size(88, 23);
			this.btnOpenSequence.TabIndex = 136;
			this.btnOpenSequence.Text = "<-- Open";
			this.btnOpenSequence.UseVisualStyleBackColor = true;
			this.btnOpenSequence.Visible = false;
			// 
			// btnOpenSelections
			// 
			this.btnOpenSelections.AllowDrop = true;
			this.btnOpenSelections.Enabled = false;
			this.btnOpenSelections.Location = new System.Drawing.Point(322, 74);
			this.btnOpenSelections.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnOpenSelections.Name = "btnOpenSelections";
			this.btnOpenSelections.Size = new System.Drawing.Size(88, 23);
			this.btnOpenSelections.TabIndex = 137;
			this.btnOpenSelections.Text = "<-- Open";
			this.btnOpenSelections.UseVisualStyleBackColor = true;
			this.btnOpenSelections.Visible = false;
			// 
			// lblCentiseconds
			// 
			this.lblCentiseconds.AllowDrop = true;
			this.lblCentiseconds.AutoSize = true;
			this.lblCentiseconds.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblCentiseconds.Location = new System.Drawing.Point(281, 535);
			this.lblCentiseconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCentiseconds.Name = "lblCentiseconds";
			this.lblCentiseconds.Size = new System.Drawing.Size(87, 15);
			this.lblCentiseconds.TabIndex = 138;
			this.lblCentiseconds.Text = "0 Centiseconds";
			this.lblCentiseconds.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.lblCentiseconds.Click += new System.EventHandler(this.lblCentiseconds_Click);
			// 
			// lstTimingGrids
			// 
			this.lstTimingGrids.BackColor = System.Drawing.Color.White;
			this.lstTimingGrids.CheckBoxes = true;
			this.lstTimingGrids.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colType,
            this.colTimes});
			this.lstTimingGrids.ForeColor = System.Drawing.Color.Black;
			this.lstTimingGrids.FullRowSelect = true;
			this.lstTimingGrids.GridLines = true;
			this.lstTimingGrids.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstTimingGrids.LabelWrap = false;
			this.lstTimingGrids.Location = new System.Drawing.Point(18, 553);
			this.lstTimingGrids.MultiSelect = false;
			this.lstTimingGrids.Name = "lstTimingGrids";
			this.lstTimingGrids.ShowGroups = false;
			this.lstTimingGrids.Size = new System.Drawing.Size(350, 85);
			this.lstTimingGrids.TabIndex = 139;
			this.lstTimingGrids.UseCompatibleStateImageBehavior = false;
			this.lstTimingGrids.View = System.Windows.Forms.View.Details;
			this.lstTimingGrids.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstTimingGrids_ItemChecked);
			this.lstTimingGrids.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstTimingGrids_ItemSelectionChanged);
			this.lstTimingGrids.Click += new System.EventHandler(this.lstTimingGrids_Click);
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 140;
			// 
			// colType
			// 
			this.colType.Text = "Type";
			// 
			// colTimes
			// 
			this.colTimes.Text = "Times";
			this.colTimes.Width = 146;
			// 
			// chkRamps
			// 
			this.chkRamps.AutoSize = true;
			this.chkRamps.Location = new System.Drawing.Point(146, 531);
			this.chkRamps.Name = "chkRamps";
			this.chkRamps.Size = new System.Drawing.Size(62, 19);
			this.chkRamps.TabIndex = 140;
			this.chkRamps.Text = "Ramps";
			this.chkRamps.UseVisualStyleBackColor = true;
			// 
			// frmSelect
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(390, 711);
			this.Controls.Add(this.chkRamps);
			this.Controls.Add(this.lstTimingGrids);
			this.Controls.Add(this.lblCentiseconds);
			this.Controls.Add(this.picAboutIcon);
			this.Controls.Add(this.cboSelectionsFile);
			this.Controls.Add(this.cboSequenceFile);
			this.Controls.Add(this.treeSequence);
			this.Controls.Add(this.picPreview);
			this.Controls.Add(this.lblTimingGrids);
			this.Controls.Add(this.lblChannels);
			this.Controls.Add(this.lblTreeClicks);
			this.Controls.Add(this.lblSelections);
			this.Controls.Add(this.btnAll);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.lblSelectionCount);
			this.Controls.Add(this.btnSaveSelections);
			this.Controls.Add(this.btnInvert);
			this.Controls.Add(this.lblSelectionsList);
			this.Controls.Add(this.btnBrowseSequence);
			this.Controls.Add(this.lblSequenceFile);
			this.Controls.Add(this.btnBrowseSelections);
			this.Controls.Add(this.btnOpenSequence);
			this.Controls.Add(this.btnOpenSelections);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.Enabled = false;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(697, 1140);
			this.MinimumSize = new System.Drawing.Size(406, 750);
			this.Name = "frmSelect";
			this.Text = "Select-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSelect_FormClosing);
			this.Load += new System.EventHandler(this.frmSelect_Load);
			this.Shown += new System.EventHandler(this.frmSelect_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmSelect_Paint);
			this.Resize += new System.EventHandler(this.frmSelect_Resize);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeSequence)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lblSelectionsList;
		private System.Windows.Forms.Button btnBrowseSequence;
		private System.Windows.Forms.Label lblSequenceFile;
		private System.Windows.Forms.Button btnBrowseSelections;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
		private System.Windows.Forms.Button btnInvert;
		private System.Windows.Forms.Button btnSaveSelections;
		private System.Windows.Forms.Label lblSelectionCount;
		private System.Windows.Forms.ToolTip ttip;
		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.ToolStripProgressBar pnlProgress;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnAll;
		private System.Windows.Forms.Label lblSelections;
		private System.Windows.Forms.Label lblTreeClicks;
		private System.Windows.Forms.Label lblChannels;
		private System.Windows.Forms.Label lblTimingGrids;
		private System.Windows.Forms.PictureBox picPreview;
		private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeSequence;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private ComboBox cboSequenceFile;
		private ComboBox cboSelectionsFile;
		private Button btnOpenSequence;
		private Button btnOpenSelections;
		private Label lblCentiseconds;
		private ListView lstTimingGrids;
		private ColumnHeader colName;
		private ColumnHeader colType;
		private ColumnHeader colTimes;
		private CheckBox chkRamps;
	}
}

