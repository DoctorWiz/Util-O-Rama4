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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplit));
			this.btnSaveSequence = new System.Windows.Forms.Button();
			this.treChannels = new System.Windows.Forms.TreeView();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
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
			this.ttip = new System.Windows.Forms.ToolTip(this.components);
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOpenSequence = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOpenSelections = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveAsSequence = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveAsSelections = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileDivider1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMatchOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileDivider2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFind = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSelectDivider1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSelectInvert = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSelectClear = new System.Windows.Forms.ToolStripMenuItem();
			this.lblChannels = new System.Windows.Forms.Label();
			this.lblTimingGrids = new System.Windows.Forms.Label();
			this.lstGrids = new System.Windows.Forms.ListBox();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.staStatus.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSaveSequence
			// 
			this.btnSaveSequence.AllowDrop = true;
			this.btnSaveSequence.Enabled = false;
			this.btnSaveSequence.Location = new System.Drawing.Point(310, 607);
			this.btnSaveSequence.Name = "btnSaveSequence";
			this.btnSaveSequence.Size = new System.Drawing.Size(75, 23);
			this.btnSaveSequence.TabIndex = 0;
			this.btnSaveSequence.Text = "Save As...";
			this.btnSaveSequence.UseVisualStyleBackColor = true;
			this.btnSaveSequence.Click += new System.EventHandler(this.btnSave_Click);
			this.btnSaveSequence.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSaveSequence.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// treChannels
			// 
			this.treChannels.AllowDrop = true;
			this.treChannels.BackColor = System.Drawing.Color.White;
			this.treChannels.ForeColor = System.Drawing.Color.Black;
			this.treChannels.FullRowSelect = true;
			this.treChannels.ImageKey = "LORChannel4.ico";
			this.treChannels.ImageList = this.imlTreeIcons;
			this.treChannels.LineColor = System.Drawing.Color.Gray;
			this.treChannels.Location = new System.Drawing.Point(12, 134);
			this.treChannels.Name = "treChannels";
			this.treChannels.SelectedImageKey = "LORChannel4.ico";
			this.treChannels.Size = new System.Drawing.Size(300, 298);
			this.treChannels.TabIndex = 7;
			this.treChannels.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treChannels_DrawNode);
			this.treChannels.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treChannels_AfterSelect);
			this.treChannels.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treChannels_NodeMouseClick);
			this.treChannels.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.treChannels.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.treChannels.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treChannels_MouseMove);
			// 
			// imlTreeIcons
			// 
			this.imlTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeIcons.ImageStream")));
			this.imlTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlTreeIcons.Images.SetKeyName(0, "track");
			this.imlTreeIcons.Images.SetKeyName(1, "channelGroup");
			this.imlTreeIcons.Images.SetKeyName(2, "rgbChannel");
			this.imlTreeIcons.Images.SetKeyName(3, "FF0000");
			this.imlTreeIcons.Images.SetKeyName(4, "00FF00");
			this.imlTreeIcons.Images.SetKeyName(5, "0000FF");
			// 
			// lblSelectionsList
			// 
			this.lblSelectionsList.AllowDrop = true;
			this.lblSelectionsList.AutoSize = true;
			this.lblSelectionsList.Location = new System.Drawing.Point(12, 74);
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
			this.btnBrowseSequence.Location = new System.Drawing.Point(235, 50);
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
			this.txtSequenceFile.Location = new System.Drawing.Point(12, 50);
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
			this.lblSequenceFile.Location = new System.Drawing.Point(12, 34);
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
			this.btnBrowseSelections.Location = new System.Drawing.Point(235, 81);
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
			this.txtSelectionsFile.Location = new System.Drawing.Point(12, 90);
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
			this.btnInvert.Location = new System.Drawing.Point(14, 581);
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
			this.btnSaveSelections.Location = new System.Drawing.Point(235, 101);
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
			this.lblSelectionCount.Location = new System.Drawing.Point(73, 565);
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
			this.cmdNothing.Location = new System.Drawing.Point(12, 607);
			this.cmdNothing.Name = "cmdNothing";
			this.cmdNothing.Size = new System.Drawing.Size(101, 27);
			this.cmdNothing.TabIndex = 59;
			this.cmdNothing.UseVisualStyleBackColor = true;
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
			this.staStatus.Location = new System.Drawing.Point(0, 635);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(393, 24);
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
			this.pnlStatus.Size = new System.Drawing.Size(148, 19);
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
			this.btnClear.Location = new System.Drawing.Point(95, 581);
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
			this.btnAll.Location = new System.Drawing.Point(176, 581);
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
			this.lblSelections.Location = new System.Drawing.Point(14, 565);
			this.lblSelections.Name = "lblSelections";
			this.lblSelections.Size = new System.Drawing.Size(59, 13);
			this.lblSelections.TabIndex = 10;
			this.lblSelections.Text = "Selections:";
			this.lblSelections.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSelections.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblNewSequence
			// 
			this.lblNewSequence.AllowDrop = true;
			this.lblNewSequence.AutoSize = true;
			this.lblNewSequence.Location = new System.Drawing.Point(309, 565);
			this.lblNewSequence.Name = "lblNewSequence";
			this.lblNewSequence.Size = new System.Drawing.Size(81, 13);
			this.lblNewSequence.TabIndex = 101;
			this.lblNewSequence.Text = "New Sequence";
			this.lblNewSequence.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblNewSequence.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnMatchOptions
			// 
			this.btnMatchOptions.AllowDrop = true;
			this.btnMatchOptions.Location = new System.Drawing.Point(311, 81);
			this.btnMatchOptions.Name = "btnMatchOptions";
			this.btnMatchOptions.Size = new System.Drawing.Size(75, 20);
			this.btnMatchOptions.TabIndex = 102;
			this.btnMatchOptions.Text = "Options...";
			this.btnMatchOptions.UseVisualStyleBackColor = true;
			this.btnMatchOptions.Click += new System.EventHandler(this.btnMatchOptions_Click);
			// 
			// btnSaveOptions
			// 
			this.btnSaveOptions.AllowDrop = true;
			this.btnSaveOptions.Location = new System.Drawing.Point(310, 581);
			this.btnSaveOptions.Name = "btnSaveOptions";
			this.btnSaveOptions.Size = new System.Drawing.Size(75, 23);
			this.btnSaveOptions.TabIndex = 103;
			this.btnSaveOptions.Text = "Options...";
			this.btnSaveOptions.UseVisualStyleBackColor = true;
			this.btnSaveOptions.Click += new System.EventHandler(this.btnSaveOptions_Click);
			// 
			// lblTreeClicks
			// 
			this.lblTreeClicks.AllowDrop = true;
			this.lblTreeClicks.AutoSize = true;
			this.lblTreeClicks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTreeClicks.ForeColor = System.Drawing.Color.DarkRed;
			this.lblTreeClicks.Location = new System.Drawing.Point(182, 612);
			this.lblTreeClicks.Name = "lblTreeClicks";
			this.lblTreeClicks.Size = new System.Drawing.Size(13, 13);
			this.lblTreeClicks.TabIndex = 105;
			this.lblTreeClicks.Text = "0";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
						this.mnuFile,
						this.mnuSelect});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(393, 24);
			this.menuStrip1.TabIndex = 106;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
						this.mnuFileOpen,
						this.mnuFileSaveAs,
						this.mnuFileDivider1,
						this.mnuOptions,
						this.mnuFileDivider2,
						this.mnuExit});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(37, 20);
			this.mnuFile.Text = "&File";
			// 
			// mnuFileOpen
			// 
			this.mnuFileOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
						this.mnuOpenSequence,
						this.mnuOpenSelections});
			this.mnuFileOpen.Name = "mnuFileOpen";
			this.mnuFileOpen.Size = new System.Drawing.Size(135, 22);
			this.mnuFileOpen.Text = "&Open";
			this.mnuFileOpen.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// mnuOpenSequence
			// 
			this.mnuOpenSequence.Name = "mnuOpenSequence";
			this.mnuOpenSequence.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mnuOpenSequence.Size = new System.Drawing.Size(202, 22);
			this.mnuOpenSequence.Text = "&Sequence(s)...";
			this.mnuOpenSequence.Click += new System.EventHandler(this.mnuOpenSequence_Click);
			// 
			// mnuOpenSelections
			// 
			this.mnuOpenSelections.Name = "mnuOpenSelections";
			this.mnuOpenSelections.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
						| System.Windows.Forms.Keys.O)));
			this.mnuOpenSelections.Size = new System.Drawing.Size(202, 22);
			this.mnuOpenSelections.Text = "S&elections...";
			this.mnuOpenSelections.Click += new System.EventHandler(this.mnuOpenSelections_Click);
			// 
			// mnuFileSaveAs
			// 
			this.mnuFileSaveAs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
						this.mnuSaveAsSequence,
						this.mnuSaveAsSelections});
			this.mnuFileSaveAs.Name = "mnuFileSaveAs";
			this.mnuFileSaveAs.Size = new System.Drawing.Size(135, 22);
			this.mnuFileSaveAs.Text = "Save &As";
			// 
			// mnuSaveAsSequence
			// 
			this.mnuSaveAsSequence.Name = "mnuSaveAsSequence";
			this.mnuSaveAsSequence.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mnuSaveAsSequence.Size = new System.Drawing.Size(215, 22);
			this.mnuSaveAsSequence.Text = "&Sequence As...";
			this.mnuSaveAsSequence.Click += new System.EventHandler(this.mnuSaveAsSequence_Click);
			// 
			// mnuSaveAsSelections
			// 
			this.mnuSaveAsSelections.Name = "mnuSaveAsSelections";
			this.mnuSaveAsSelections.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
						| System.Windows.Forms.Keys.S)));
			this.mnuSaveAsSelections.Size = new System.Drawing.Size(215, 22);
			this.mnuSaveAsSelections.Text = "S&elections As...";
			this.mnuSaveAsSelections.Click += new System.EventHandler(this.mnuSaveAsSelections_Click);
			// 
			// mnuFileDivider1
			// 
			this.mnuFileDivider1.Name = "mnuFileDivider1";
			this.mnuFileDivider1.Size = new System.Drawing.Size(132, 6);
			// 
			// mnuOptions
			// 
			this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
						this.mnuMatchOptions,
						this.mnuSaveOptions});
			this.mnuOptions.Name = "mnuOptions";
			this.mnuOptions.Size = new System.Drawing.Size(135, 22);
			this.mnuOptions.Text = "&Options";
			// 
			// mnuMatchOptions
			// 
			this.mnuMatchOptions.Name = "mnuMatchOptions";
			this.mnuMatchOptions.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
						| System.Windows.Forms.Keys.M)));
			this.mnuMatchOptions.Size = new System.Drawing.Size(220, 22);
			this.mnuMatchOptions.Text = "&Matching...";
			this.mnuMatchOptions.Click += new System.EventHandler(this.btnMatchOptions_Click);
			// 
			// mnuSaveOptions
			// 
			this.mnuSaveOptions.Name = "mnuSaveOptions";
			this.mnuSaveOptions.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
						| System.Windows.Forms.Keys.S)));
			this.mnuSaveOptions.Size = new System.Drawing.Size(220, 22);
			this.mnuSaveOptions.Text = "&Save Format...";
			this.mnuSaveOptions.Click += new System.EventHandler(this.mnuSaveOptions_Click);
			// 
			// mnuFileDivider2
			// 
			this.mnuFileDivider2.Name = "mnuFileDivider2";
			this.mnuFileDivider2.Size = new System.Drawing.Size(132, 6);
			// 
			// mnuExit
			// 
			this.mnuExit.Name = "mnuExit";
			this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.mnuExit.Size = new System.Drawing.Size(135, 22);
			this.mnuExit.Text = "E&xit";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// mnuSelect
			// 
			this.mnuSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
						this.mnuFind,
						this.mnuSelectDivider1,
						this.mnuSelectAll,
						this.mnuSelectInvert,
						this.mnuSelectClear});
			this.mnuSelect.Name = "mnuSelect";
			this.mnuSelect.Size = new System.Drawing.Size(50, 20);
			this.mnuSelect.Text = "&Select";
			// 
			// mnuFind
			// 
			this.mnuFind.Name = "mnuFind";
			this.mnuFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.mnuFind.Size = new System.Drawing.Size(146, 22);
			this.mnuFind.Text = "&Find...";
			// 
			// mnuSelectDivider1
			// 
			this.mnuSelectDivider1.Name = "mnuSelectDivider1";
			this.mnuSelectDivider1.Size = new System.Drawing.Size(143, 6);
			// 
			// mnuSelectAll
			// 
			this.mnuSelectAll.Name = "mnuSelectAll";
			this.mnuSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.mnuSelectAll.Size = new System.Drawing.Size(146, 22);
			this.mnuSelectAll.Text = "&All";
			// 
			// mnuSelectInvert
			// 
			this.mnuSelectInvert.Name = "mnuSelectInvert";
			this.mnuSelectInvert.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.mnuSelectInvert.Size = new System.Drawing.Size(146, 22);
			this.mnuSelectInvert.Text = "&Invert";
			// 
			// mnuSelectClear
			// 
			this.mnuSelectClear.Name = "mnuSelectClear";
			this.mnuSelectClear.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.mnuSelectClear.Size = new System.Drawing.Size(146, 22);
			this.mnuSelectClear.Text = "&Clear";
			// 
			// lblChannels
			// 
			this.lblChannels.AllowDrop = true;
			this.lblChannels.AutoSize = true;
			this.lblChannels.Location = new System.Drawing.Point(12, 118);
			this.lblChannels.Name = "lblChannels";
			this.lblChannels.Size = new System.Drawing.Size(154, 13);
			this.lblChannels.TabIndex = 108;
			this.lblChannels.Text = "Tracks, Groups, and Channels:";
			// 
			// lblTimingGrids
			// 
			this.lblTimingGrids.AllowDrop = true;
			this.lblTimingGrids.AutoSize = true;
			this.lblTimingGrids.Location = new System.Drawing.Point(12, 461);
			this.lblTimingGrids.Name = "lblTimingGrids";
			this.lblTimingGrids.Size = new System.Drawing.Size(68, 13);
			this.lblTimingGrids.TabIndex = 109;
			this.lblTimingGrids.Text = "Timing Grids:";
			// 
			// lstGrids
			// 
			this.lstGrids.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lstGrids.FormattingEnabled = true;
			this.lstGrids.Location = new System.Drawing.Point(12, 477);
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
			this.picPreview.Location = new System.Drawing.Point(12, 438);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(300, 20);
			this.picPreview.TabIndex = 110;
			this.picPreview.TabStop = false;
			// 
			// frmSplit
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(393, 659);
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
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.lblSelectionCount);
			this.Controls.Add(this.btnSaveSelections);
			this.Controls.Add(this.btnInvert);
			this.Controls.Add(this.btnSaveSequence);
			this.Controls.Add(this.treChannels);
			this.Controls.Add(this.lblSelectionsList);
			this.Controls.Add(this.btnBrowseSequence);
			this.Controls.Add(this.txtSequenceFile);
			this.Controls.Add(this.lblSequenceFile);
			this.Controls.Add(this.btnBrowseSelections);
			this.Controls.Add(this.txtSelectionsFile);
			this.Controls.Add(this.cmdNothing);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(50, 39);
			this.Name = "frmSplit";
			this.Text = "Split-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSplit_FormClosing);
			this.Load += new System.EventHandler(this.frmSplit_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmSplit_Paint);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSaveSequence;
		private System.Windows.Forms.TreeView treChannels;
		private System.Windows.Forms.Label lblSelectionsList;
		private System.Windows.Forms.Button btnBrowseSequence;
		private System.Windows.Forms.TextBox txtSequenceFile;
		private System.Windows.Forms.Label lblSequenceFile;
		private System.Windows.Forms.Button btnBrowseSelections;
		private System.Windows.Forms.TextBox txtSelectionsFile;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
		private System.Windows.Forms.ImageList imlTreeIcons;
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
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
		private System.Windows.Forms.ToolStripMenuItem mnuOpenSequence;
		private System.Windows.Forms.ToolStripMenuItem mnuOpenSelections;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveAsSequence;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveAsSelections;
		private System.Windows.Forms.ToolStripSeparator mnuFileDivider1;
		private System.Windows.Forms.ToolStripMenuItem mnuExit;
		private System.Windows.Forms.ToolStripMenuItem mnuSelect;
		private System.Windows.Forms.ToolStripMenuItem mnuFind;
		private System.Windows.Forms.ToolStripSeparator mnuSelectDivider1;
		private System.Windows.Forms.ToolStripMenuItem mnuSelectAll;
		private System.Windows.Forms.ToolStripMenuItem mnuSelectInvert;
		private System.Windows.Forms.ToolStripMenuItem mnuSelectClear;
		private System.Windows.Forms.ToolStripMenuItem mnuOptions;
		private System.Windows.Forms.ToolStripMenuItem mnuMatchOptions;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveOptions;
		private System.Windows.Forms.ToolStripSeparator mnuFileDivider2;
		private System.Windows.Forms.Label lblChannels;
		private System.Windows.Forms.Label lblTimingGrids;
		private System.Windows.Forms.ListBox lstGrids;
		private System.Windows.Forms.PictureBox picPreview;
	}
}

