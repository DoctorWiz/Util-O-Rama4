namespace ListORama
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmList));
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOpenMaster = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOpenSource = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOpenMap = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveNewSequence = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveNewMap = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileDivider1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSourceLeft = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSourceRight = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMatchOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileDivider2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMapMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMap = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuUnmap = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSummary = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuClearMap = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuLoadMap = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveMap = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuReapply = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuAutoMap = new System.Windows.Forms.ToolStripMenuItem();
			this.ttip = new System.Windows.Forms.ToolTip(this.components);
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.txtSourceFile = new System.Windows.Forms.TextBox();
			this.btnBrowseSource = new System.Windows.Forms.Button();
			this.lblSourceFile = new System.Windows.Forms.Label();
			this.lblSourceTree = new System.Windows.Forms.Label();
			this.treeSource = new System.Windows.Forms.TreeView();
			this.pnlAll = new System.Windows.Forms.Panel();
			this.optViz = new System.Windows.Forms.RadioButton();
			this.optSeq = new System.Windows.Forms.RadioButton();
			this.lblVisualFile = new System.Windows.Forms.Label();
			this.grpOptions = new System.Windows.Forms.GroupBox();
			this.picSearch = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtKeyword = new System.Windows.Forms.TextBox();
			this.btnGenCSV = new System.Windows.Forms.Button();
			this.optSortSavedIndex = new System.Windows.Forms.RadioButton();
			this.optSortDisplayed = new System.Windows.Forms.RadioButton();
			this.optSortOutput = new System.Windows.Forms.RadioButton();
			this.optSortName = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pnlDividerLine = new System.Windows.Forms.Panel();
			this.chkDigital = new System.Windows.Forms.CheckBox();
			this.chkLOR = new System.Windows.Forms.CheckBox();
			this.chkNoController = new System.Windows.Forms.CheckBox();
			this.chkDMX = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkHeaders = new System.Windows.Forms.CheckBox();
			this.cboTracks = new System.Windows.Forms.ComboBox();
			this.chkRGBKeywd = new System.Windows.Forms.CheckBox();
			this.chkGroupNonKeywd = new System.Windows.Forms.CheckBox();
			this.chkGroupKeywd = new System.Windows.Forms.CheckBox();
			this.chkRegKeywd = new System.Windows.Forms.CheckBox();
			this.chkRGBNonKeywd = new System.Windows.Forms.CheckBox();
			this.chkRegNonKeywd = new System.Windows.Forms.CheckBox();
			this.chkKeyword = new System.Windows.Forms.CheckBox();
			this.menuStrip1.SuspendLayout();
			this.staStatus.SuspendLayout();
			this.pnlAll.SuspendLayout();
			this.grpOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picSearch)).BeginInit();
			this.SuspendLayout();
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
			// 
			// dlgOpenFile
			// 
			this.dlgOpenFile.FileName = "openFileDialog1";
			// 
			// menuStrip1
			// 
			this.menuStrip1.AllowDrop = true;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuMapMenu});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(394, 24);
			this.menuStrip1.TabIndex = 108;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.menuStrip1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
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
            this.mnuOpenMaster,
            this.mnuOpenSource,
            this.mnuOpenMap});
			this.mnuFileOpen.Name = "mnuFileOpen";
			this.mnuFileOpen.Size = new System.Drawing.Size(134, 22);
			this.mnuFileOpen.Text = "&Open";
			// 
			// mnuOpenMaster
			// 
			this.mnuOpenMaster.Name = "mnuOpenMaster";
			this.mnuOpenMaster.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
			this.mnuOpenMaster.Size = new System.Drawing.Size(241, 22);
			this.mnuOpenMaster.Text = "&Master Channel Config";
			this.mnuOpenMaster.Click += new System.EventHandler(this.mnuOpenMaster_Click);
			// 
			// mnuOpenSource
			// 
			this.mnuOpenSource.Name = "mnuOpenSource";
			this.mnuOpenSource.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mnuOpenSource.Size = new System.Drawing.Size(241, 22);
			this.mnuOpenSource.Text = "&Source Sequence(s)";
			this.mnuOpenSource.Click += new System.EventHandler(this.mnuOpenSource_Click);
			// 
			// mnuOpenMap
			// 
			this.mnuOpenMap.Name = "mnuOpenMap";
			this.mnuOpenMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
			this.mnuOpenMap.Size = new System.Drawing.Size(241, 22);
			this.mnuOpenMap.Text = "Ma&ppings";
			this.mnuOpenMap.Click += new System.EventHandler(this.mnuOpenMap_Click);
			// 
			// mnuFileSaveAs
			// 
			this.mnuFileSaveAs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveNewSequence,
            this.mnuSaveNewMap});
			this.mnuFileSaveAs.Name = "mnuFileSaveAs";
			this.mnuFileSaveAs.Size = new System.Drawing.Size(134, 22);
			this.mnuFileSaveAs.Text = "Save &As";
			// 
			// mnuSaveNewSequence
			// 
			this.mnuSaveNewSequence.Name = "mnuSaveNewSequence";
			this.mnuSaveNewSequence.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mnuSaveNewSequence.Size = new System.Drawing.Size(192, 22);
			this.mnuSaveNewSequence.Text = "&New Sequence";
			// 
			// mnuSaveNewMap
			// 
			this.mnuSaveNewMap.Name = "mnuSaveNewMap";
			this.mnuSaveNewMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
			this.mnuSaveNewMap.Size = new System.Drawing.Size(192, 22);
			this.mnuSaveNewMap.Text = "&Mappings";
			// 
			// mnuFileDivider1
			// 
			this.mnuFileDivider1.Name = "mnuFileDivider1";
			this.mnuFileDivider1.Size = new System.Drawing.Size(131, 6);
			// 
			// mnuOptions
			// 
			this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSourceLeft,
            this.mnuSourceRight,
            this.toolStripSeparator3,
            this.mnuMatchOptions,
            this.mnuSaveOptions});
			this.mnuOptions.Name = "mnuOptions";
			this.mnuOptions.Size = new System.Drawing.Size(134, 22);
			this.mnuOptions.Text = "O&ptions";
			// 
			// mnuSourceLeft
			// 
			this.mnuSourceLeft.Checked = true;
			this.mnuSourceLeft.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuSourceLeft.Name = "mnuSourceLeft";
			this.mnuSourceLeft.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
			this.mnuSourceLeft.Size = new System.Drawing.Size(231, 22);
			this.mnuSourceLeft.Text = "Source on &Left";
			this.mnuSourceLeft.Click += new System.EventHandler(this.mnuSourceLeft_Click);
			// 
			// mnuSourceRight
			// 
			this.mnuSourceRight.Name = "mnuSourceRight";
			this.mnuSourceRight.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
			this.mnuSourceRight.Size = new System.Drawing.Size(231, 22);
			this.mnuSourceRight.Text = "Source on &Right";
			this.mnuSourceRight.Click += new System.EventHandler(this.mnuSourceRight_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(228, 6);
			// 
			// mnuMatchOptions
			// 
			this.mnuMatchOptions.Name = "mnuMatchOptions";
			this.mnuMatchOptions.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
			this.mnuMatchOptions.Size = new System.Drawing.Size(231, 22);
			this.mnuMatchOptions.Text = "&Matching...";
			this.mnuMatchOptions.Click += new System.EventHandler(this.mnuMatchOptions_Click);
			// 
			// mnuSaveOptions
			// 
			this.mnuSaveOptions.Name = "mnuSaveOptions";
			this.mnuSaveOptions.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.mnuSaveOptions.Size = new System.Drawing.Size(231, 22);
			this.mnuSaveOptions.Text = "&Save Format...";
			// 
			// mnuFileDivider2
			// 
			this.mnuFileDivider2.Name = "mnuFileDivider2";
			this.mnuFileDivider2.Size = new System.Drawing.Size(131, 6);
			// 
			// mnuExit
			// 
			this.mnuExit.Name = "mnuExit";
			this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.mnuExit.Size = new System.Drawing.Size(134, 22);
			this.mnuExit.Text = "E&xit";
			// 
			// mnuMapMenu
			// 
			this.mnuMapMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMap,
            this.mnuUnmap,
            this.mnuSummary,
            this.mnuClearMap,
            this.toolStripSeparator1,
            this.mnuLoadMap,
            this.mnuSaveMap,
            this.mnuReapply,
            this.toolStripSeparator2,
            this.mnuAutoMap});
			this.mnuMapMenu.Name = "mnuMapMenu";
			this.mnuMapMenu.Size = new System.Drawing.Size(43, 20);
			this.mnuMapMenu.Text = "&Map";
			this.mnuMapMenu.Click += new System.EventHandler(this.mnuSelect_Click);
			// 
			// mnuMap
			// 
			this.mnuMap.Name = "mnuMap";
			this.mnuMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
			this.mnuMap.Size = new System.Drawing.Size(162, 22);
			this.mnuMap.Text = "&Map";
			// 
			// mnuUnmap
			// 
			this.mnuUnmap.Name = "mnuUnmap";
			this.mnuUnmap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.U)));
			this.mnuUnmap.Size = new System.Drawing.Size(162, 22);
			this.mnuUnmap.Text = "&Unmap";
			// 
			// mnuSummary
			// 
			this.mnuSummary.Name = "mnuSummary";
			this.mnuSummary.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Y)));
			this.mnuSummary.Size = new System.Drawing.Size(162, 22);
			this.mnuSummary.Text = "Summar&y";
			// 
			// mnuClearMap
			// 
			this.mnuClearMap.Name = "mnuClearMap";
			this.mnuClearMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
			this.mnuClearMap.Size = new System.Drawing.Size(162, 22);
			this.mnuClearMap.Text = "&Clear All";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(159, 6);
			// 
			// mnuLoadMap
			// 
			this.mnuLoadMap.Name = "mnuLoadMap";
			this.mnuLoadMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
			this.mnuLoadMap.Size = new System.Drawing.Size(162, 22);
			this.mnuLoadMap.Text = "&Load";
			// 
			// mnuSaveMap
			// 
			this.mnuSaveMap.Name = "mnuSaveMap";
			this.mnuSaveMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
			this.mnuSaveMap.Size = new System.Drawing.Size(162, 22);
			this.mnuSaveMap.Text = "&Save";
			// 
			// mnuReapply
			// 
			this.mnuReapply.Name = "mnuReapply";
			this.mnuReapply.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
			this.mnuReapply.Size = new System.Drawing.Size(162, 22);
			this.mnuReapply.Text = "&Reapply";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(159, 6);
			// 
			// mnuAutoMap
			// 
			this.mnuAutoMap.Name = "mnuAutoMap";
			this.mnuAutoMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
			this.mnuAutoMap.Size = new System.Drawing.Size(162, 22);
			this.mnuAutoMap.Text = "&AutoMap";
			this.mnuAutoMap.Click += new System.EventHandler(this.mnuAutoMap_Click);
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlProgress,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 494);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(394, 24);
			this.staStatus.TabIndex = 107;
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
			this.pnlStatus.Size = new System.Drawing.Size(282, 19);
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
			// txtSourceFile
			// 
			this.txtSourceFile.AllowDrop = true;
			this.txtSourceFile.Enabled = false;
			this.txtSourceFile.Location = new System.Drawing.Point(15, 40);
			this.txtSourceFile.Name = "txtSourceFile";
			this.txtSourceFile.Size = new System.Drawing.Size(300, 20);
			this.txtSourceFile.TabIndex = 19;
			this.txtSourceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtSourceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnBrowseSource
			// 
			this.btnBrowseSource.AllowDrop = true;
			this.btnBrowseSource.Location = new System.Drawing.Point(321, 40);
			this.btnBrowseSource.Name = "btnBrowseSource";
			this.btnBrowseSource.Size = new System.Drawing.Size(36, 20);
			this.btnBrowseSource.TabIndex = 20;
			this.btnBrowseSource.Text = "...";
			this.btnBrowseSource.UseVisualStyleBackColor = true;
			this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
			this.btnBrowseSource.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseSource.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSourceFile
			// 
			this.lblSourceFile.AllowDrop = true;
			this.lblSourceFile.AutoSize = true;
			this.lblSourceFile.Location = new System.Drawing.Point(39, 446);
			this.lblSourceFile.Name = "lblSourceFile";
			this.lblSourceFile.Size = new System.Drawing.Size(75, 13);
			this.lblSourceFile.TabIndex = 21;
			this.lblSourceFile.Text = "Sequence File";
			this.lblSourceFile.Visible = false;
			this.lblSourceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSourceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSourceTree
			// 
			this.lblSourceTree.AllowDrop = true;
			this.lblSourceTree.AutoSize = true;
			this.lblSourceTree.Location = new System.Drawing.Point(12, 455);
			this.lblSourceTree.Name = "lblSourceTree";
			this.lblSourceTree.Size = new System.Drawing.Size(88, 13);
			this.lblSourceTree.TabIndex = 28;
			this.lblSourceTree.Text = "Source Channels";
			this.lblSourceTree.Visible = false;
			this.lblSourceTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSourceTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// treeSource
			// 
			this.treeSource.AllowDrop = true;
			this.treeSource.BackColor = System.Drawing.Color.White;
			this.treeSource.Enabled = false;
			this.treeSource.ImageIndex = 0;
			this.treeSource.ImageList = this.imlTreeIcons;
			this.treeSource.Location = new System.Drawing.Point(-186, 406);
			this.treeSource.Name = "treeSource";
			this.treeSource.SelectedImageIndex = 0;
			this.treeSource.Size = new System.Drawing.Size(300, 360);
			this.treeSource.TabIndex = 37;
			this.treeSource.Visible = false;
			this.treeSource.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.treeSource.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// pnlAll
			// 
			this.pnlAll.Controls.Add(this.optViz);
			this.pnlAll.Controls.Add(this.optSeq);
			this.pnlAll.Controls.Add(this.lblVisualFile);
			this.pnlAll.Controls.Add(this.grpOptions);
			this.pnlAll.Controls.Add(this.treeSource);
			this.pnlAll.Controls.Add(this.lblSourceTree);
			this.pnlAll.Controls.Add(this.lblSourceFile);
			this.pnlAll.Controls.Add(this.btnBrowseSource);
			this.pnlAll.Controls.Add(this.txtSourceFile);
			this.pnlAll.Location = new System.Drawing.Point(0, 26);
			this.pnlAll.Name = "pnlAll";
			this.pnlAll.Size = new System.Drawing.Size(398, 475);
			this.pnlAll.TabIndex = 19;
			this.pnlAll.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.pnlAll.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// optViz
			// 
			this.optViz.AutoSize = true;
			this.optViz.Checked = true;
			this.optViz.Location = new System.Drawing.Point(121, 17);
			this.optViz.Name = "optViz";
			this.optViz.Size = new System.Drawing.Size(88, 17);
			this.optViz.TabIndex = 61;
			this.optViz.TabStop = true;
			this.optViz.Text = "Visualizer File";
			this.optViz.UseVisualStyleBackColor = true;
			this.optViz.CheckedChanged += new System.EventHandler(this.optType_CheckedChanged);
			// 
			// optSeq
			// 
			this.optSeq.AutoSize = true;
			this.optSeq.Location = new System.Drawing.Point(15, 17);
			this.optSeq.Name = "optSeq";
			this.optSeq.Size = new System.Drawing.Size(93, 17);
			this.optSeq.TabIndex = 60;
			this.optSeq.Text = "Sequence File";
			this.optSeq.UseVisualStyleBackColor = true;
			this.optSeq.CheckedChanged += new System.EventHandler(this.optType_CheckedChanged);
			// 
			// lblVisualFile
			// 
			this.lblVisualFile.AllowDrop = true;
			this.lblVisualFile.AutoSize = true;
			this.lblVisualFile.Location = new System.Drawing.Point(15, 446);
			this.lblVisualFile.Name = "lblVisualFile";
			this.lblVisualFile.Size = new System.Drawing.Size(75, 13);
			this.lblVisualFile.TabIndex = 59;
			this.lblVisualFile.Text = "Sequence File";
			this.lblVisualFile.Visible = false;
			// 
			// grpOptions
			// 
			this.grpOptions.Controls.Add(this.picSearch);
			this.grpOptions.Controls.Add(this.label3);
			this.grpOptions.Controls.Add(this.txtKeyword);
			this.grpOptions.Controls.Add(this.btnGenCSV);
			this.grpOptions.Controls.Add(this.optSortSavedIndex);
			this.grpOptions.Controls.Add(this.optSortDisplayed);
			this.grpOptions.Controls.Add(this.optSortOutput);
			this.grpOptions.Controls.Add(this.optSortName);
			this.grpOptions.Controls.Add(this.label2);
			this.grpOptions.Controls.Add(this.panel1);
			this.grpOptions.Controls.Add(this.pnlDividerLine);
			this.grpOptions.Controls.Add(this.chkDigital);
			this.grpOptions.Controls.Add(this.chkLOR);
			this.grpOptions.Controls.Add(this.chkNoController);
			this.grpOptions.Controls.Add(this.chkDMX);
			this.grpOptions.Controls.Add(this.label1);
			this.grpOptions.Controls.Add(this.chkHeaders);
			this.grpOptions.Controls.Add(this.cboTracks);
			this.grpOptions.Controls.Add(this.chkRGBKeywd);
			this.grpOptions.Controls.Add(this.chkGroupNonKeywd);
			this.grpOptions.Controls.Add(this.chkGroupKeywd);
			this.grpOptions.Controls.Add(this.chkRegKeywd);
			this.grpOptions.Controls.Add(this.chkRGBNonKeywd);
			this.grpOptions.Controls.Add(this.chkRegNonKeywd);
			this.grpOptions.Controls.Add(this.chkKeyword);
			this.grpOptions.Enabled = false;
			this.grpOptions.Location = new System.Drawing.Point(18, 88);
			this.grpOptions.Name = "grpOptions";
			this.grpOptions.Size = new System.Drawing.Size(367, 348);
			this.grpOptions.TabIndex = 53;
			this.grpOptions.TabStop = false;
			this.grpOptions.Text = " Collect ";
			// 
			// picSearch
			// 
			this.picSearch.Image = ((System.Drawing.Image)(resources.GetObject("picSearch.Image")));
			this.picSearch.Location = new System.Drawing.Point(138, 32);
			this.picSearch.Name = "picSearch";
			this.picSearch.Size = new System.Drawing.Size(24, 24);
			this.picSearch.TabIndex = 84;
			this.picSearch.TabStop = false;
			this.picSearch.Click += new System.EventHandler(this.picSearch_Click);
			// 
			// label3
			// 
			this.label3.AllowDrop = true;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 83;
			this.label3.Text = "Keyword";
			// 
			// txtKeyword
			// 
			this.txtKeyword.Location = new System.Drawing.Point(30, 36);
			this.txtKeyword.Name = "txtKeyword";
			this.txtKeyword.Size = new System.Drawing.Size(102, 20);
			this.txtKeyword.TabIndex = 82;
			this.txtKeyword.Text = "Pixel";
			this.txtKeyword.TextChanged += new System.EventHandler(this.txtKeyword_TextChanged);
			// 
			// btnGenCSV
			// 
			this.btnGenCSV.AllowDrop = true;
			this.btnGenCSV.Enabled = false;
			this.btnGenCSV.Location = new System.Drawing.Point(210, 301);
			this.btnGenCSV.Name = "btnGenCSV";
			this.btnGenCSV.Size = new System.Drawing.Size(141, 29);
			this.btnGenCSV.TabIndex = 56;
			this.btnGenCSV.Text = "Generate CSVs";
			this.btnGenCSV.UseVisualStyleBackColor = true;
			this.btnGenCSV.Click += new System.EventHandler(this.btnGenCSV_Click);
			// 
			// optSortSavedIndex
			// 
			this.optSortSavedIndex.AutoSize = true;
			this.optSortSavedIndex.Location = new System.Drawing.Point(9, 292);
			this.optSortSavedIndex.Name = "optSortSavedIndex";
			this.optSortSavedIndex.Size = new System.Drawing.Size(82, 17);
			this.optSortSavedIndex.TabIndex = 73;
			this.optSortSavedIndex.Text = "SavedIndex";
			this.optSortSavedIndex.UseVisualStyleBackColor = true;
			this.optSortSavedIndex.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// optSortDisplayed
			// 
			this.optSortDisplayed.AutoSize = true;
			this.optSortDisplayed.Location = new System.Drawing.Point(9, 269);
			this.optSortDisplayed.Name = "optSortDisplayed";
			this.optSortDisplayed.Size = new System.Drawing.Size(100, 17);
			this.optSortDisplayed.TabIndex = 72;
			this.optSortDisplayed.Text = "Displayed Order";
			this.optSortDisplayed.UseVisualStyleBackColor = true;
			this.optSortDisplayed.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// optSortOutput
			// 
			this.optSortOutput.AutoSize = true;
			this.optSortOutput.Location = new System.Drawing.Point(9, 246);
			this.optSortOutput.Name = "optSortOutput";
			this.optSortOutput.Size = new System.Drawing.Size(107, 17);
			this.optSortOutput.TabIndex = 71;
			this.optSortOutput.Text = "Controller - Circuit";
			this.optSortOutput.UseVisualStyleBackColor = true;
			this.optSortOutput.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// optSortName
			// 
			this.optSortName.AutoSize = true;
			this.optSortName.Checked = true;
			this.optSortName.Location = new System.Drawing.Point(9, 223);
			this.optSortName.Name = "optSortName";
			this.optSortName.Size = new System.Drawing.Size(53, 17);
			this.optSortName.TabIndex = 70;
			this.optSortName.TabStop = true;
			this.optSortName.Text = "Name";
			this.optSortName.UseVisualStyleBackColor = true;
			this.optSortName.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// label2
			// 
			this.label2.AllowDrop = true;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 207);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 69;
			this.label2.Text = "Sort By";
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Location = new System.Drawing.Point(9, 200);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(346, 4);
			this.panel1.TabIndex = 67;
			// 
			// pnlDividerLine
			// 
			this.pnlDividerLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlDividerLine.Location = new System.Drawing.Point(197, 23);
			this.pnlDividerLine.Name = "pnlDividerLine";
			this.pnlDividerLine.Size = new System.Drawing.Size(4, 180);
			this.pnlDividerLine.TabIndex = 66;
			// 
			// chkDigital
			// 
			this.chkDigital.AutoSize = true;
			this.chkDigital.Checked = true;
			this.chkDigital.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkDigital.Location = new System.Drawing.Point(215, 109);
			this.chkDigital.Name = "chkDigital";
			this.chkDigital.Size = new System.Drawing.Size(102, 17);
			this.chkDigital.TabIndex = 65;
			this.chkDigital.Text = "Digital Channels";
			this.chkDigital.UseVisualStyleBackColor = true;
			this.chkDigital.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// chkLOR
			// 
			this.chkLOR.AutoSize = true;
			this.chkLOR.Checked = true;
			this.chkLOR.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLOR.Location = new System.Drawing.Point(215, 63);
			this.chkLOR.Name = "chkLOR";
			this.chkLOR.Size = new System.Drawing.Size(95, 17);
			this.chkLOR.TabIndex = 64;
			this.chkLOR.Text = "LOR Channels";
			this.chkLOR.UseVisualStyleBackColor = true;
			this.chkLOR.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// chkNoController
			// 
			this.chkNoController.AutoSize = true;
			this.chkNoController.Checked = true;
			this.chkNoController.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkNoController.Location = new System.Drawing.Point(215, 132);
			this.chkNoController.Name = "chkNoController";
			this.chkNoController.Size = new System.Drawing.Size(87, 17);
			this.chkNoController.TabIndex = 63;
			this.chkNoController.Text = "No Controller";
			this.chkNoController.UseVisualStyleBackColor = true;
			this.chkNoController.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// chkDMX
			// 
			this.chkDMX.AutoSize = true;
			this.chkDMX.Checked = true;
			this.chkDMX.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkDMX.Location = new System.Drawing.Point(215, 86);
			this.chkDMX.Name = "chkDMX";
			this.chkDMX.Size = new System.Drawing.Size(97, 17);
			this.chkDMX.TabIndex = 62;
			this.chkDMX.Text = "DMX Channels";
			this.chkDMX.UseVisualStyleBackColor = true;
			this.chkDMX.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// label1
			// 
			this.label1.AllowDrop = true;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(212, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 61;
			this.label1.Text = "Track(s)";
			// 
			// chkHeaders
			// 
			this.chkHeaders.AutoSize = true;
			this.chkHeaders.Checked = true;
			this.chkHeaders.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkHeaders.Location = new System.Drawing.Point(215, 210);
			this.chkHeaders.Name = "chkHeaders";
			this.chkHeaders.Size = new System.Drawing.Size(66, 17);
			this.chkHeaders.TabIndex = 60;
			this.chkHeaders.Text = "Headers";
			this.chkHeaders.UseVisualStyleBackColor = true;
			this.chkHeaders.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// cboTracks
			// 
			this.cboTracks.FormattingEnabled = true;
			this.cboTracks.Items.AddRange(new object[] {
            "All Tracks"});
			this.cboTracks.Location = new System.Drawing.Point(215, 36);
			this.cboTracks.Name = "cboTracks";
			this.cboTracks.Size = new System.Drawing.Size(136, 21);
			this.cboTracks.TabIndex = 59;
			this.cboTracks.SelectedIndexChanged += new System.EventHandler(this.cboTracks_SelectedIndexChanged);
			// 
			// chkRGBKeywd
			// 
			this.chkRGBKeywd.AutoSize = true;
			this.chkRGBKeywd.Checked = true;
			this.chkRGBKeywd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRGBKeywd.Location = new System.Drawing.Point(9, 108);
			this.chkRGBKeywd.Name = "chkRGBKeywd";
			this.chkRGBKeywd.Size = new System.Drawing.Size(153, 17);
			this.chkRGBKeywd.TabIndex = 58;
			this.chkRGBKeywd.Text = "RGB Channels with \"Pixel\"";
			this.chkRGBKeywd.UseVisualStyleBackColor = true;
			this.chkRGBKeywd.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// chkGroupNonKeywd
			// 
			this.chkGroupNonKeywd.AutoSize = true;
			this.chkGroupNonKeywd.Checked = true;
			this.chkGroupNonKeywd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkGroupNonKeywd.Location = new System.Drawing.Point(9, 177);
			this.chkGroupNonKeywd.Name = "chkGroupNonKeywd";
			this.chkGroupNonKeywd.Size = new System.Drawing.Size(132, 17);
			this.chkGroupNonKeywd.TabIndex = 57;
			this.chkGroupNonKeywd.Text = "Groups without \"Pixel\"";
			this.chkGroupNonKeywd.UseVisualStyleBackColor = true;
			this.chkGroupNonKeywd.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// chkGroupKeywd
			// 
			this.chkGroupKeywd.AutoSize = true;
			this.chkGroupKeywd.Checked = true;
			this.chkGroupKeywd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkGroupKeywd.Location = new System.Drawing.Point(9, 154);
			this.chkGroupKeywd.Name = "chkGroupKeywd";
			this.chkGroupKeywd.Size = new System.Drawing.Size(117, 17);
			this.chkGroupKeywd.TabIndex = 56;
			this.chkGroupKeywd.Text = "Groups with \"Pixel\"";
			this.chkGroupKeywd.UseVisualStyleBackColor = true;
			this.chkGroupKeywd.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// chkRegKeywd
			// 
			this.chkRegKeywd.AutoSize = true;
			this.chkRegKeywd.Checked = true;
			this.chkRegKeywd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRegKeywd.Location = new System.Drawing.Point(9, 62);
			this.chkRegKeywd.Name = "chkRegKeywd";
			this.chkRegKeywd.Size = new System.Drawing.Size(167, 17);
			this.chkRegKeywd.TabIndex = 55;
			this.chkRegKeywd.Text = "Regular Channels with \"Pixel\"";
			this.chkRegKeywd.UseVisualStyleBackColor = true;
			this.chkRegKeywd.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// chkRGBNonKeywd
			// 
			this.chkRGBNonKeywd.AutoSize = true;
			this.chkRGBNonKeywd.Checked = true;
			this.chkRGBNonKeywd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRGBNonKeywd.Location = new System.Drawing.Point(9, 131);
			this.chkRGBNonKeywd.Name = "chkRGBNonKeywd";
			this.chkRGBNonKeywd.Size = new System.Drawing.Size(168, 17);
			this.chkRGBNonKeywd.TabIndex = 54;
			this.chkRGBNonKeywd.Text = "RGB Channels without \"Pixel\"";
			this.chkRGBNonKeywd.UseVisualStyleBackColor = true;
			this.chkRGBNonKeywd.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// chkRegNonKeywd
			// 
			this.chkRegNonKeywd.AutoSize = true;
			this.chkRegNonKeywd.Checked = true;
			this.chkRegNonKeywd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRegNonKeywd.Location = new System.Drawing.Point(9, 85);
			this.chkRegNonKeywd.Name = "chkRegNonKeywd";
			this.chkRegNonKeywd.Size = new System.Drawing.Size(182, 17);
			this.chkRegNonKeywd.TabIndex = 53;
			this.chkRegNonKeywd.Text = "Regular Channels without \"Pixel\"";
			this.chkRegNonKeywd.UseVisualStyleBackColor = true;
			this.chkRegNonKeywd.CheckedChanged += new System.EventHandler(this.OptionCheckChanged);
			// 
			// chkKeyword
			// 
			this.chkKeyword.AutoSize = true;
			this.chkKeyword.Checked = true;
			this.chkKeyword.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkKeyword.Location = new System.Drawing.Point(9, 39);
			this.chkKeyword.Name = "chkKeyword";
			this.chkKeyword.Size = new System.Drawing.Size(15, 14);
			this.chkKeyword.TabIndex = 64;
			this.chkKeyword.UseVisualStyleBackColor = true;
			this.chkKeyword.CheckedChanged += new System.EventHandler(this.chkKeyword_CheckedChanged);
			// 
			// frmList
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(394, 518);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.pnlAll);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1500, 561);
			this.MinimumSize = new System.Drawing.Size(200, 561);
			this.Name = "frmList";
			this.Text = "List-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmList_FormClosing);
			this.Load += new System.EventHandler(this.frmList_Load);
			this.Shown += new System.EventHandler(this.frmList_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmList_Paint);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			this.pnlAll.ResumeLayout(false);
			this.pnlAll.PerformLayout();
			this.grpOptions.ResumeLayout(false);
			this.grpOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picSearch)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
		private System.Windows.Forms.ToolStripMenuItem mnuOpenMaster;
		private System.Windows.Forms.ToolStripMenuItem mnuOpenSource;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveNewSequence;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveNewMap;
		private System.Windows.Forms.ToolStripSeparator mnuFileDivider1;
		private System.Windows.Forms.ToolStripMenuItem mnuOptions;
		private System.Windows.Forms.ToolStripMenuItem mnuMatchOptions;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveOptions;
		private System.Windows.Forms.ToolStripSeparator mnuFileDivider2;
		private System.Windows.Forms.ToolStripMenuItem mnuExit;
		private System.Windows.Forms.ToolStripMenuItem mnuMapMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuMap;
		private System.Windows.Forms.ToolStripMenuItem mnuUnmap;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveMap;
		private System.Windows.Forms.ToolStripMenuItem mnuReapply;
		private System.Windows.Forms.ToolTip ttip;
		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripProgressBar pnlProgress;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.ToolStripMenuItem mnuOpenMap;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem mnuLoadMap;
		private System.Windows.Forms.ToolStripMenuItem mnuClearMap;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem mnuAutoMap;
		private System.Windows.Forms.ToolStripMenuItem mnuSourceLeft;
		private System.Windows.Forms.ToolStripMenuItem mnuSourceRight;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem mnuSummary;
		private System.Windows.Forms.TextBox txtSourceFile;
		private System.Windows.Forms.Button btnBrowseSource;
		private System.Windows.Forms.Label lblSourceFile;
		private System.Windows.Forms.Label lblSourceTree;
		private System.Windows.Forms.TreeView treeSource;
		private System.Windows.Forms.Panel pnlAll;
		private System.Windows.Forms.GroupBox grpOptions;
		private System.Windows.Forms.CheckBox chkRGBKeywd;
		private System.Windows.Forms.CheckBox chkGroupNonKeywd;
		private System.Windows.Forms.CheckBox chkGroupKeywd;
		private System.Windows.Forms.CheckBox chkRegKeywd;
		private System.Windows.Forms.CheckBox chkRGBNonKeywd;
		private System.Windows.Forms.CheckBox chkRegNonKeywd;
		private System.Windows.Forms.CheckBox chkHeaders;
		private System.Windows.Forms.ComboBox cboTracks;
		private System.Windows.Forms.Panel pnlDividerLine;
		private System.Windows.Forms.CheckBox chkDigital;
		private System.Windows.Forms.CheckBox chkLOR;
		private System.Windows.Forms.CheckBox chkNoController;
		private System.Windows.Forms.CheckBox chkDMX;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton optSortOutput;
		private System.Windows.Forms.RadioButton optSortName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton optSortSavedIndex;
		private System.Windows.Forms.RadioButton optSortDisplayed;
		private System.Windows.Forms.TextBox txtKeyword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnGenCSV;
		private System.Windows.Forms.PictureBox picSearch;
		private System.Windows.Forms.Label lblVisualFile;
		private System.Windows.Forms.RadioButton optViz;
		private System.Windows.Forms.RadioButton optSeq;
		private System.Windows.Forms.CheckBox chkKeyword;
	}
}

