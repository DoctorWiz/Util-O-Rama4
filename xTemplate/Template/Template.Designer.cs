namespace MapORama
{
	partial class frmTemplate
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplate));
			this.pnlAll = new System.Windows.Forms.Panel();
			this.pnlMessage = new System.Windows.Forms.Panel();
			this.lblMessage = new System.Windows.Forms.Label();
			this.btn1 = new System.Windows.Forms.Button();
			this.treeDestination = new System.Windows.Forms.TreeView();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.treeSource = new System.Windows.Forms.TreeView();
			this.lblDestinationTree = new System.Windows.Forms.Label();
			this.lblSourceTree = new System.Windows.Forms.Label();
			this.lblDestinationFile = new System.Windows.Forms.Label();
			this.btnBrowseDestination = new System.Windows.Forms.Button();
			this.txtDestinationFile = new System.Windows.Forms.TextBox();
			this.lblSourceFile = new System.Windows.Forms.Label();
			this.btnBrowseSource = new System.Windows.Forms.Button();
			this.txtSourceFile = new System.Windows.Forms.TextBox();
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
			this.pnlAll.SuspendLayout();
			this.pnlMessage.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.staStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlAll
			// 
			this.pnlAll.Controls.Add(this.pnlMessage);
			this.pnlAll.Controls.Add(this.btn1);
			this.pnlAll.Controls.Add(this.treeDestination);
			this.pnlAll.Controls.Add(this.treeSource);
			this.pnlAll.Controls.Add(this.lblDestinationTree);
			this.pnlAll.Controls.Add(this.lblSourceTree);
			this.pnlAll.Controls.Add(this.lblDestinationFile);
			this.pnlAll.Controls.Add(this.btnBrowseDestination);
			this.pnlAll.Controls.Add(this.txtDestinationFile);
			this.pnlAll.Controls.Add(this.lblSourceFile);
			this.pnlAll.Controls.Add(this.btnBrowseSource);
			this.pnlAll.Controls.Add(this.txtSourceFile);
			this.pnlAll.Location = new System.Drawing.Point(0, 26);
			this.pnlAll.Name = "pnlAll";
			this.pnlAll.Size = new System.Drawing.Size(762, 674);
			this.pnlAll.TabIndex = 19;
			this.pnlAll.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.pnlAll.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// pnlMessage
			// 
			this.pnlMessage.Controls.Add(this.lblMessage);
			this.pnlMessage.Location = new System.Drawing.Point(15, 536);
			this.pnlMessage.Name = "pnlMessage";
			this.pnlMessage.Size = new System.Drawing.Size(700, 15);
			this.pnlMessage.TabIndex = 43;
			// 
			// lblMessage
			// 
			this.lblMessage.AllowDrop = true;
			this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMessage.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblMessage.Location = new System.Drawing.Point(1, 1);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(698, 13);
			this.lblMessage.TabIndex = 42;
			this.lblMessage.Text = "Status Message";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblMessage.Visible = false;
			// 
			// btn1
			// 
			this.btn1.AllowDrop = true;
			this.btn1.Enabled = false;
			this.btn1.Location = new System.Drawing.Point(327, 99);
			this.btn1.Name = "btn1";
			this.btn1.Size = new System.Drawing.Size(76, 29);
			this.btn1.TabIndex = 42;
			this.btn1.Text = "Button 1";
			this.btn1.UseVisualStyleBackColor = true;
			this.btn1.Visible = false;
			this.btn1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btn1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// treeDestination
			// 
			this.treeDestination.AllowDrop = true;
			this.treeDestination.BackColor = System.Drawing.Color.White;
			this.treeDestination.ImageIndex = 0;
			this.treeDestination.ImageList = this.imlTreeIcons;
			this.treeDestination.Location = new System.Drawing.Point(415, 99);
			this.treeDestination.Name = "treeDestination";
			this.treeDestination.SelectedImageIndex = 0;
			this.treeDestination.Size = new System.Drawing.Size(300, 433);
			this.treeDestination.TabIndex = 38;
			this.treeDestination.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMaster_AfterSelect);
			this.treeDestination.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.treeDestination.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
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
			// treeSource
			// 
			this.treeSource.AllowDrop = true;
			this.treeSource.BackColor = System.Drawing.Color.White;
			this.treeSource.ImageIndex = 0;
			this.treeSource.ImageList = this.imlTreeIcons;
			this.treeSource.Location = new System.Drawing.Point(15, 99);
			this.treeSource.Name = "treeSource";
			this.treeSource.SelectedImageIndex = 0;
			this.treeSource.Size = new System.Drawing.Size(300, 433);
			this.treeSource.TabIndex = 37;
			this.treeSource.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSource_AfterSelect);
			this.treeSource.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.treeSource.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblDestinationTree
			// 
			this.lblDestinationTree.AllowDrop = true;
			this.lblDestinationTree.AutoSize = true;
			this.lblDestinationTree.Location = new System.Drawing.Point(415, 72);
			this.lblDestinationTree.Name = "lblDestinationTree";
			this.lblDestinationTree.Size = new System.Drawing.Size(107, 13);
			this.lblDestinationTree.TabIndex = 30;
			this.lblDestinationTree.Text = "Destination Channels";
			this.lblDestinationTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblDestinationTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSourceTree
			// 
			this.lblSourceTree.AllowDrop = true;
			this.lblSourceTree.AutoSize = true;
			this.lblSourceTree.Location = new System.Drawing.Point(15, 72);
			this.lblSourceTree.Name = "lblSourceTree";
			this.lblSourceTree.Size = new System.Drawing.Size(88, 13);
			this.lblSourceTree.TabIndex = 28;
			this.lblSourceTree.Text = "Source Channels";
			this.lblSourceTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSourceTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblDestinationFile
			// 
			this.lblDestinationFile.AllowDrop = true;
			this.lblDestinationFile.AutoSize = true;
			this.lblDestinationFile.Location = new System.Drawing.Point(415, 16);
			this.lblDestinationFile.Name = "lblDestinationFile";
			this.lblDestinationFile.Size = new System.Drawing.Size(79, 13);
			this.lblDestinationFile.TabIndex = 25;
			this.lblDestinationFile.Text = "Destination File";
			this.lblDestinationFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblDestinationFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnBrowseDestination
			// 
			this.btnBrowseDestination.AllowDrop = true;
			this.btnBrowseDestination.Location = new System.Drawing.Point(721, 40);
			this.btnBrowseDestination.Name = "btnBrowseDestination";
			this.btnBrowseDestination.Size = new System.Drawing.Size(36, 20);
			this.btnBrowseDestination.TabIndex = 24;
			this.btnBrowseDestination.Text = "...";
			this.btnBrowseDestination.UseVisualStyleBackColor = true;
			this.btnBrowseDestination.Click += new System.EventHandler(this.btnBrowseMaster_Click);
			this.btnBrowseDestination.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseDestination.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// txtDestinationFile
			// 
			this.txtDestinationFile.AllowDrop = true;
			this.txtDestinationFile.Enabled = false;
			this.txtDestinationFile.Location = new System.Drawing.Point(415, 40);
			this.txtDestinationFile.Name = "txtDestinationFile";
			this.txtDestinationFile.Size = new System.Drawing.Size(300, 20);
			this.txtDestinationFile.TabIndex = 23;
			this.txtDestinationFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtDestinationFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSourceFile
			// 
			this.lblSourceFile.AllowDrop = true;
			this.lblSourceFile.AutoSize = true;
			this.lblSourceFile.Location = new System.Drawing.Point(15, 16);
			this.lblSourceFile.Name = "lblSourceFile";
			this.lblSourceFile.Size = new System.Drawing.Size(60, 13);
			this.lblSourceFile.TabIndex = 21;
			this.lblSourceFile.Text = "Source File";
			this.lblSourceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSourceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
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
			// txtSourceFile
			// 
			this.txtSourceFile.AllowDrop = true;
			this.txtSourceFile.Enabled = false;
			this.txtSourceFile.Location = new System.Drawing.Point(15, 40);
			this.txtSourceFile.Name = "txtSourceFile";
			this.txtSourceFile.Size = new System.Drawing.Size(300, 20);
			this.txtSourceFile.TabIndex = 19;
			this.txtSourceFile.TextChanged += new System.EventHandler(this.txtSourceFile_TextChanged);
			this.txtSourceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtSourceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
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
			this.menuStrip1.Size = new System.Drawing.Size(762, 24);
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
			this.staStatus.Location = new System.Drawing.Point(0, 703);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(762, 24);
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
			this.pnlStatus.Size = new System.Drawing.Size(650, 19);
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
			// frmTemplate
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(762, 727);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.pnlAll);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmTemplate";
			this.Text = "Template";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTemplate_FormClosing);
			this.Load += new System.EventHandler(this.frmTemplate_Load);
			this.Shown += new System.EventHandler(this.frmTemplate_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmTemplate_Paint);
			this.pnlAll.ResumeLayout(false);
			this.pnlAll.PerformLayout();
			this.pnlMessage.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pnlAll;
		private System.Windows.Forms.Label lblDestinationTree;
		private System.Windows.Forms.Label lblSourceTree;
		private System.Windows.Forms.Label lblDestinationFile;
		private System.Windows.Forms.Button btnBrowseDestination;
		private System.Windows.Forms.TextBox txtDestinationFile;
		private System.Windows.Forms.Label lblSourceFile;
		private System.Windows.Forms.Button btnBrowseSource;
		private System.Windows.Forms.TextBox txtSourceFile;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.TreeView treeSource;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.TreeView treeDestination;
		private System.Windows.Forms.Button btn1;
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
		private System.Windows.Forms.Panel pnlMessage;
		private System.Windows.Forms.Label lblMessage;
	}
}

