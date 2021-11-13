namespace UtilORama4
{
	partial class frmSplit
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
			Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo2 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplit));
			this.btnSaveSequence = new System.Windows.Forms.Button();
			this.lblSelectionsList = new System.Windows.Forms.Label();
			this.btnBrowseSequence = new System.Windows.Forms.Button();
			this.txtSequenceFile = new System.Windows.Forms.TextBox();
			this.lblSequenceFile = new System.Windows.Forms.Label();
			this.btnBrowseSelections = new System.Windows.Forms.Button();
			this.txtSelectionsFile = new System.Windows.Forms.TextBox();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.btnInvert = new System.Windows.Forms.Button();
			this.btnSaveSelections = new System.Windows.Forms.Button();
			this.lblSelectionCount = new System.Windows.Forms.Label();
			this.cmdNothing = new System.Windows.Forms.Button();
			this.ttip = new System.Windows.Forms.ToolTip();
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnAll = new System.Windows.Forms.Button();
			this.lblSelections = new System.Windows.Forms.Label();
			this.lblNewSequence = new System.Windows.Forms.Label();
			this.btnMatchOptions = new System.Windows.Forms.Button();
			this.btnSaveOptions = new System.Windows.Forms.Button();
			this.lblTreeClicks = new System.Windows.Forms.Label();
			this.lblChannels = new System.Windows.Forms.Label();
			this.lblTimingGrids = new System.Windows.Forms.Label();
			this.lstGrids = new System.Windows.Forms.ListBox();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.treeChannels = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
			this.imlTreeIcons = new System.Windows.Forms.ImageList();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.txtFileNewSeq = new System.Windows.Forms.TextBox();
			this.chkAutoLaunch = new System.Windows.Forms.CheckBox();
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeChannels)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSaveSequence
			// 
			this.btnSaveSequence.AllowDrop = true;
			this.btnSaveSequence.Enabled = false;
			this.btnSaveSequence.Location = new System.Drawing.Point(240, 621);
			this.btnSaveSequence.Name = "btnSaveSequence";
			this.btnSaveSequence.Size = new System.Drawing.Size(75, 23);
			this.btnSaveSequence.TabIndex = 0;
			this.btnSaveSequence.Text = "Save As...";
			this.btnSaveSequence.UseVisualStyleBackColor = true;
			this.btnSaveSequence.Click += new System.EventHandler(this.btnSave_Click);
			this.btnSaveSequence.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSaveSequence.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSelectionsList
			// 
			this.lblSelectionsList.AllowDrop = true;
			this.lblSelectionsList.AutoSize = true;
			this.lblSelectionsList.Location = new System.Drawing.Point(12, 57);
			this.lblSelectionsList.Name = "lblSelectionsList";
			this.lblSelectionsList.Size = new System.Drawing.Size(75, 13);
			this.lblSelectionsList.TabIndex = 3;
			this.lblSelectionsList.Text = "Selections List";
			this.lblSelectionsList.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSelectionsList.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnBrowseSequence
			// 
			this.btnBrowseSequence.AllowDrop = true;
			this.btnBrowseSequence.Location = new System.Drawing.Point(240, 25);
			this.btnBrowseSequence.Name = "btnBrowseSequence";
			this.btnBrowseSequence.Size = new System.Drawing.Size(75, 20);
			this.btnBrowseSequence.TabIndex = 2;
			this.btnBrowseSequence.Text = "Browse...";
			this.btnBrowseSequence.UseVisualStyleBackColor = true;
			this.btnBrowseSequence.Click += new System.EventHandler(this.btnBrowseSeq_Click);
			this.btnBrowseSequence.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseSequence.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// txtSequenceFile
			// 
			this.txtSequenceFile.AllowDrop = true;
			this.txtSequenceFile.Location = new System.Drawing.Point(15, 26);
			this.txtSequenceFile.Name = "txtSequenceFile";
			this.txtSequenceFile.ReadOnly = true;
			this.txtSequenceFile.Size = new System.Drawing.Size(219, 20);
			this.txtSequenceFile.TabIndex = 1;
			this.txtSequenceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtSequenceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSequenceFile
			// 
			this.lblSequenceFile.AllowDrop = true;
			this.lblSequenceFile.AutoSize = true;
			this.lblSequenceFile.Location = new System.Drawing.Point(12, 9);
			this.lblSequenceFile.Name = "lblSequenceFile";
			this.lblSequenceFile.Size = new System.Drawing.Size(75, 13);
			this.lblSequenceFile.TabIndex = 100;
			this.lblSequenceFile.Text = "Sequence File";
			this.lblSequenceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSequenceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnBrowseSelections
			// 
			this.btnBrowseSelections.AllowDrop = true;
			this.btnBrowseSelections.Location = new System.Drawing.Point(240, 64);
			this.btnBrowseSelections.Name = "btnBrowseSelections";
			this.btnBrowseSelections.Size = new System.Drawing.Size(75, 20);
			this.btnBrowseSelections.TabIndex = 5;
			this.btnBrowseSelections.Text = "Load...";
			this.btnBrowseSelections.UseVisualStyleBackColor = true;
			this.btnBrowseSelections.Click += new System.EventHandler(this.btnBrowseSelections_Click);
			this.btnBrowseSelections.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseSelections.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// txtSelectionsFile
			// 
			this.txtSelectionsFile.AllowDrop = true;
			this.txtSelectionsFile.Location = new System.Drawing.Point(15, 73);
			this.txtSelectionsFile.Name = "txtSelectionsFile";
			this.txtSelectionsFile.ReadOnly = true;
			this.txtSelectionsFile.Size = new System.Drawing.Size(219, 20);
			this.txtSelectionsFile.TabIndex = 4;
			this.txtSelectionsFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtSelectionsFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
			// 
			// btnInvert
			// 
			this.btnInvert.AllowDrop = true;
			this.btnInvert.Location = new System.Drawing.Point(96, 567);
			this.btnInvert.Name = "btnInvert";
			this.btnInvert.Size = new System.Drawing.Size(75, 23);
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
			this.btnSaveSelections.Location = new System.Drawing.Point(240, 83);
			this.btnSaveSelections.Name = "btnSaveSelections";
			this.btnSaveSelections.Size = new System.Drawing.Size(75, 20);
			this.btnSaveSelections.TabIndex = 6;
			this.btnSaveSelections.Text = "Save As...";
			this.btnSaveSelections.UseVisualStyleBackColor = true;
			this.btnSaveSelections.Click += new System.EventHandler(this.btnSaveSelections_Click);
			this.btnSaveSelections.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSaveSelections.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSelectionCount
			// 
			this.lblSelectionCount.AllowDrop = true;
			this.lblSelectionCount.AutoSize = true;
			this.lblSelectionCount.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblSelectionCount.Location = new System.Drawing.Point(206, 57);
			this.lblSelectionCount.Name = "lblSelectionCount";
			this.lblSelectionCount.Size = new System.Drawing.Size(13, 13);
			this.lblSelectionCount.TabIndex = 11;
			this.lblSelectionCount.Text = "0";
			this.lblSelectionCount.Click += new System.EventHandler(this.lblSelectionCount_Click);
			this.lblSelectionCount.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSelectionCount.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// cmdNothing
			// 
			this.cmdNothing.Location = new System.Drawing.Point(314, 534);
			this.cmdNothing.Name = "cmdNothing";
			this.cmdNothing.Size = new System.Drawing.Size(101, 27);
			this.cmdNothing.TabIndex = 59;
			this.cmdNothing.UseVisualStyleBackColor = true;
			this.cmdNothing.Visible = false;
			this.cmdNothing.Click += new System.EventHandler(this.cmdNothing_Click);
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlProgress,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 655);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(334, 24);
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
			this.pnlHelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			this.pnlStatus.Size = new System.Drawing.Size(120, 19);
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
			// btnClear
			// 
			this.btnClear.AllowDrop = true;
			this.btnClear.Enabled = false;
			this.btnClear.Location = new System.Drawing.Point(177, 567);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
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
			this.btnAll.Location = new System.Drawing.Point(15, 567);
			this.btnAll.Name = "btnAll";
			this.btnAll.Size = new System.Drawing.Size(75, 23);
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
			this.lblSelections.Location = new System.Drawing.Point(150, 57);
			this.lblSelections.Name = "lblSelections";
			this.lblSelections.Size = new System.Drawing.Size(59, 13);
			this.lblSelections.TabIndex = 10;
			this.lblSelections.Text = "Selections:";
			this.lblSelections.Click += new System.EventHandler(this.lblSelections_Click);
			this.lblSelections.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSelections.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblNewSequence
			// 
			this.lblNewSequence.AllowDrop = true;
			this.lblNewSequence.AutoSize = true;
			this.lblNewSequence.Location = new System.Drawing.Point(12, 607);
			this.lblNewSequence.Name = "lblNewSequence";
			this.lblNewSequence.Size = new System.Drawing.Size(81, 13);
			this.lblNewSequence.TabIndex = 101;
			this.lblNewSequence.Text = "New Sequence";
			this.lblNewSequence.Click += new System.EventHandler(this.lblNewSequence_Click);
			this.lblNewSequence.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblNewSequence.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnMatchOptions
			// 
			this.btnMatchOptions.AllowDrop = true;
			this.btnMatchOptions.Location = new System.Drawing.Point(327, 50);
			this.btnMatchOptions.Name = "btnMatchOptions";
			this.btnMatchOptions.Size = new System.Drawing.Size(75, 20);
			this.btnMatchOptions.TabIndex = 102;
			this.btnMatchOptions.Text = "Options...";
			this.btnMatchOptions.UseVisualStyleBackColor = true;
			this.btnMatchOptions.Visible = false;
			this.btnMatchOptions.Click += new System.EventHandler(this.btnMatchOptions_Click);
			// 
			// btnSaveOptions
			// 
			this.btnSaveOptions.AllowDrop = true;
			this.btnSaveOptions.Location = new System.Drawing.Point(354, 502);
			this.btnSaveOptions.Name = "btnSaveOptions";
			this.btnSaveOptions.Size = new System.Drawing.Size(75, 23);
			this.btnSaveOptions.TabIndex = 103;
			this.btnSaveOptions.Text = "Options...";
			this.btnSaveOptions.UseVisualStyleBackColor = true;
			this.btnSaveOptions.Visible = false;
			this.btnSaveOptions.Click += new System.EventHandler(this.btnSaveOptions_Click);
			// 
			// lblTreeClicks
			// 
			this.lblTreeClicks.AllowDrop = true;
			this.lblTreeClicks.AutoSize = true;
			this.lblTreeClicks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTreeClicks.ForeColor = System.Drawing.Color.DarkRed;
			this.lblTreeClicks.Location = new System.Drawing.Point(185, 106);
			this.lblTreeClicks.Name = "lblTreeClicks";
			this.lblTreeClicks.Size = new System.Drawing.Size(13, 13);
			this.lblTreeClicks.TabIndex = 105;
			this.lblTreeClicks.Text = "0";
			// 
			// lblChannels
			// 
			this.lblChannels.AllowDrop = true;
			this.lblChannels.AutoSize = true;
			this.lblChannels.Location = new System.Drawing.Point(12, 106);
			this.lblChannels.Name = "lblChannels";
			this.lblChannels.Size = new System.Drawing.Size(154, 13);
			this.lblChannels.TabIndex = 108;
			this.lblChannels.Text = "Tracks, Groups, and Channels:";
			// 
			// lblTimingGrids
			// 
			this.lblTimingGrids.AllowDrop = true;
			this.lblTimingGrids.AutoSize = true;
			this.lblTimingGrids.Location = new System.Drawing.Point(12, 463);
			this.lblTimingGrids.Name = "lblTimingGrids";
			this.lblTimingGrids.Size = new System.Drawing.Size(68, 13);
			this.lblTimingGrids.TabIndex = 109;
			this.lblTimingGrids.Text = "Timing Grids:";
			// 
			// lstGrids
			// 
			this.lstGrids.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lstGrids.FormattingEnabled = true;
			this.lstGrids.Location = new System.Drawing.Point(15, 479);
			this.lstGrids.Name = "lstGrids";
			this.lstGrids.Size = new System.Drawing.Size(300, 82);
			this.lstGrids.TabIndex = 107;
			this.lstGrids.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstGrids_DrawItem);
			this.lstGrids.SelectedIndexChanged += new System.EventHandler(this.lstGrids_SelectedIndexChanged);
			// 
			// picPreview
			// 
			this.picPreview.BackColor = System.Drawing.Color.Tan;
			this.picPreview.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picPreview.Location = new System.Drawing.Point(15, 428);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(300, 32);
			this.picPreview.TabIndex = 110;
			this.picPreview.TabStop = false;
			// 
			// treeChannels
			// 
			this.treeChannels.BackColor = System.Drawing.Color.White;
			treeNodeAdvStyleInfo2.CheckBoxTickThickness = 1;
			treeNodeAdvStyleInfo2.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo2.EnsureDefaultOptionedChild = true;
			treeNodeAdvStyleInfo2.InteractiveCheckBox = true;
			treeNodeAdvStyleInfo2.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo2.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo2.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
			treeNodeAdvStyleInfo2.ShowCheckBox = true;
			treeNodeAdvStyleInfo2.TextColor = System.Drawing.Color.Black;
			this.treeChannels.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo2)});
			this.treeChannels.BeforeTouchSize = new System.Drawing.Size(300, 300);
			this.treeChannels.ForeColor = System.Drawing.Color.Black;
			// 
			// 
			// 
			this.treeChannels.HelpTextControl.BaseThemeName = null;
			this.treeChannels.HelpTextControl.Location = new System.Drawing.Point(0, 0);
			this.treeChannels.HelpTextControl.Name = "";
			this.treeChannels.HelpTextControl.Size = new System.Drawing.Size(392, 112);
			this.treeChannels.HelpTextControl.TabIndex = 0;
			this.treeChannels.HelpTextControl.Visible = true;
			this.treeChannels.InactiveSelectedNodeForeColor = System.Drawing.SystemColors.ControlText;
			this.treeChannels.InteractiveCheckBoxes = true;
			this.treeChannels.LeftImageList = this.imlTreeIcons;
			this.treeChannels.Location = new System.Drawing.Point(15, 122);
			this.treeChannels.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
			this.treeChannels.Name = "treeChannels";
			this.treeChannels.SelectedNodeForeColor = System.Drawing.SystemColors.HighlightText;
			this.treeChannels.ShowCheckBoxes = true;
			this.treeChannels.Size = new System.Drawing.Size(300, 300);
			this.treeChannels.TabIndex = 132;
			this.treeChannels.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 0;
			this.treeChannels.ThemeStyle.TreeNodeAdvStyle.EnsureDefaultOptionedChild = true;
			// 
			// 
			// 
			this.treeChannels.ToolTipControl.BaseThemeName = null;
			this.treeChannels.ToolTipControl.Location = new System.Drawing.Point(0, 0);
			this.treeChannels.ToolTipControl.Name = "";
			this.treeChannels.ToolTipControl.Size = new System.Drawing.Size(392, 112);
			this.treeChannels.ToolTipControl.TabIndex = 0;
			this.treeChannels.ToolTipControl.Visible = true;
			this.treeChannels.BeforeSelect += new Syncfusion.Windows.Forms.Tools.TreeNodeAdvBeforeSelectEventHandler(this.treeChannels_BeforeSelect);
			this.treeChannels.AfterSelect += new System.EventHandler(this.treeChannels_AfterSelect);
			this.treeChannels.AfterCheck += new Syncfusion.Windows.Forms.Tools.TreeNodeAdvEventHandler(this.treeChannels_AfterCheck);
			this.treeChannels.Click += new System.EventHandler(this.treeChannels_Click);
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
			// picAboutIcon
			// 
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(314, 247);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 133;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// txtFileNewSeq
			// 
			this.txtFileNewSeq.AllowDrop = true;
			this.txtFileNewSeq.Location = new System.Drawing.Point(15, 623);
			this.txtFileNewSeq.Name = "txtFileNewSeq";
			this.txtFileNewSeq.ReadOnly = true;
			this.txtFileNewSeq.Size = new System.Drawing.Size(219, 20);
			this.txtFileNewSeq.TabIndex = 134;
			// 
			// chkAutoLaunch
			// 
			this.chkAutoLaunch.AutoSize = true;
			this.chkAutoLaunch.Location = new System.Drawing.Point(153, 606);
			this.chkAutoLaunch.Name = "chkAutoLaunch";
			this.chkAutoLaunch.Size = new System.Drawing.Size(87, 17);
			this.chkAutoLaunch.TabIndex = 135;
			this.chkAutoLaunch.Text = "Auto-Launch";
			this.chkAutoLaunch.UseVisualStyleBackColor = true;
			// 
			// frmSplit
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 679);
			this.Controls.Add(this.chkAutoLaunch);
			this.Controls.Add(this.txtFileNewSeq);
			this.Controls.Add(this.picAboutIcon);
			this.Controls.Add(this.treeChannels);
			this.Controls.Add(this.picPreview);
			this.Controls.Add(this.lblTimingGrids);
			this.Controls.Add(this.lblChannels);
			this.Controls.Add(this.lstGrids);
			this.Controls.Add(this.lblTreeClicks);
			this.Controls.Add(this.btnSaveOptions);
			this.Controls.Add(this.btnMatchOptions);
			this.Controls.Add(this.lblNewSequence);
			this.Controls.Add(this.lblSelections);
			this.Controls.Add(this.btnAll);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.lblSelectionCount);
			this.Controls.Add(this.btnSaveSelections);
			this.Controls.Add(this.btnInvert);
			this.Controls.Add(this.btnSaveSequence);
			this.Controls.Add(this.lblSelectionsList);
			this.Controls.Add(this.btnBrowseSequence);
			this.Controls.Add(this.txtSequenceFile);
			this.Controls.Add(this.lblSequenceFile);
			this.Controls.Add(this.btnBrowseSelections);
			this.Controls.Add(this.txtSelectionsFile);
			this.Controls.Add(this.cmdNothing);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(600, 1000);
			this.MinimumSize = new System.Drawing.Size(350, 700);
			this.Name = "frmSplit";
			this.Text = "Split-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSplit_FormClosing);
			this.Load += new System.EventHandler(this.frmSplit_Load);
			this.Shown += new System.EventHandler(this.frmSplit_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmSplit_Paint);
			this.Resize += new System.EventHandler(this.frmSplit_Resize);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeChannels)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSaveSequence;
		private System.Windows.Forms.Label lblSelectionsList;
		private System.Windows.Forms.Button btnBrowseSequence;
		private System.Windows.Forms.TextBox txtSequenceFile;
		private System.Windows.Forms.Label lblSequenceFile;
		private System.Windows.Forms.Button btnBrowseSelections;
		private System.Windows.Forms.TextBox txtSelectionsFile;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
		private System.Windows.Forms.Button btnInvert;
		private System.Windows.Forms.Button btnSaveSelections;
		private System.Windows.Forms.Label lblSelectionCount;
		private System.Windows.Forms.Button cmdNothing;
		private System.Windows.Forms.ToolTip ttip;
		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.ToolStripProgressBar pnlProgress;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnAll;
		private System.Windows.Forms.Label lblSelections;
		private System.Windows.Forms.Label lblNewSequence;
		private System.Windows.Forms.Button btnMatchOptions;
		private System.Windows.Forms.Button btnSaveOptions;
		private System.Windows.Forms.Label lblTreeClicks;
		private System.Windows.Forms.Label lblChannels;
		private System.Windows.Forms.Label lblTimingGrids;
		private System.Windows.Forms.ListBox lstGrids;
		private System.Windows.Forms.PictureBox picPreview;
		private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeChannels;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private System.Windows.Forms.TextBox txtFileNewSeq;
		private System.Windows.Forms.CheckBox chkAutoLaunch;
	}
}

