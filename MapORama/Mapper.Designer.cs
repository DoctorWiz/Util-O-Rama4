namespace MapORama
{
	partial class frmRemapper
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRemapper));
			this.pnlAll = new System.Windows.Forms.Panel();
			this.btnEaves = new System.Windows.Forms.Button();
			this.lblDebug = new System.Windows.Forms.Label();
			this.chkCopyBeats = new System.Windows.Forms.CheckBox();
			this.chkAutoLaunch = new System.Windows.Forms.CheckBox();
			this.pnlMessage = new System.Windows.Forms.Panel();
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnAutoMap = new System.Windows.Forms.Button();
			this.btnSummary = new System.Windows.Forms.Button();
			this.treeMaster = new System.Windows.Forms.TreeView();
			this.imlTreeIcons = new System.Windows.Forms.ImageList();
			this.treeSource = new System.Windows.Forms.TreeView();
			this.btnSaveMap = new System.Windows.Forms.Button();
			this.txtMappingFile = new System.Windows.Forms.TextBox();
			this.btnLoadMap = new System.Windows.Forms.Button();
			this.btnUnmap = new System.Windows.Forms.Button();
			this.btnMap = new System.Windows.Forms.Button();
			this.btnSaveNewSeq = new System.Windows.Forms.Button();
			this.lblMasterTree = new System.Windows.Forms.Label();
			this.lblSourceTree = new System.Windows.Forms.Label();
			this.lblMasterFile = new System.Windows.Forms.Label();
			this.btnBrowseMaster = new System.Windows.Forms.Button();
			this.txtMasterFile = new System.Windows.Forms.TextBox();
			this.lblSourceFile = new System.Windows.Forms.Label();
			this.btnBrowseSource = new System.Windows.Forms.Button();
			this.txtSourceFile = new System.Windows.Forms.TextBox();
			this.prgBarInner = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
			this.prgBarOuter = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
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
			this.ttip = new System.Windows.Forms.ToolTip();
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAll.SuspendLayout();
			this.pnlMessage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.prgBarInner)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.prgBarOuter)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.staStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlAll
			// 
			this.pnlAll.Controls.Add(this.btnEaves);
			this.pnlAll.Controls.Add(this.lblDebug);
			this.pnlAll.Controls.Add(this.chkCopyBeats);
			this.pnlAll.Controls.Add(this.chkAutoLaunch);
			this.pnlAll.Controls.Add(this.pnlMessage);
			this.pnlAll.Controls.Add(this.btnAutoMap);
			this.pnlAll.Controls.Add(this.btnSummary);
			this.pnlAll.Controls.Add(this.treeMaster);
			this.pnlAll.Controls.Add(this.treeSource);
			this.pnlAll.Controls.Add(this.btnSaveMap);
			this.pnlAll.Controls.Add(this.txtMappingFile);
			this.pnlAll.Controls.Add(this.btnLoadMap);
			this.pnlAll.Controls.Add(this.btnUnmap);
			this.pnlAll.Controls.Add(this.btnMap);
			this.pnlAll.Controls.Add(this.btnSaveNewSeq);
			this.pnlAll.Controls.Add(this.lblMasterTree);
			this.pnlAll.Controls.Add(this.lblSourceTree);
			this.pnlAll.Controls.Add(this.lblMasterFile);
			this.pnlAll.Controls.Add(this.btnBrowseMaster);
			this.pnlAll.Controls.Add(this.txtMasterFile);
			this.pnlAll.Controls.Add(this.lblSourceFile);
			this.pnlAll.Controls.Add(this.btnBrowseSource);
			this.pnlAll.Controls.Add(this.txtSourceFile);
			this.pnlAll.Controls.Add(this.prgBarInner);
			this.pnlAll.Controls.Add(this.prgBarOuter);
			this.pnlAll.Location = new System.Drawing.Point(0, 26);
			this.pnlAll.Name = "pnlAll";
			this.pnlAll.Size = new System.Drawing.Size(762, 674);
			this.pnlAll.TabIndex = 19;
			this.pnlAll.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.pnlAll.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.pnlAll.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAll_Paint);
			// 
			// btnEaves
			// 
			this.btnEaves.Location = new System.Drawing.Point(355, 263);
			this.btnEaves.Name = "btnEaves";
			this.btnEaves.Size = new System.Drawing.Size(32, 32);
			this.btnEaves.TabIndex = 50;
			this.btnEaves.Text = "E!";
			this.btnEaves.UseVisualStyleBackColor = true;
			this.btnEaves.Click += new System.EventHandler(this.btnEaves_Click);
			// 
			// lblDebug
			// 
			this.lblDebug.AllowDrop = true;
			this.lblDebug.AutoSize = true;
			this.lblDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDebug.ForeColor = System.Drawing.Color.BlueViolet;
			this.lblDebug.Location = new System.Drawing.Point(324, 497);
			this.lblDebug.Name = "lblDebug";
			this.lblDebug.Size = new System.Drawing.Size(41, 12);
			this.lblDebug.TabIndex = 49;
			this.lblDebug.Text = "Debug...";
			this.lblDebug.Click += new System.EventHandler(this.lblDebug_Click);
			// 
			// chkCopyBeats
			// 
			this.chkCopyBeats.AutoSize = true;
			this.chkCopyBeats.Checked = true;
			this.chkCopyBeats.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCopyBeats.Location = new System.Drawing.Point(33, 628);
			this.chkCopyBeats.Name = "chkCopyBeats";
			this.chkCopyBeats.Size = new System.Drawing.Size(117, 17);
			this.chkCopyBeats.TabIndex = 48;
			this.chkCopyBeats.Text = "Copy Beat Track(s)";
			this.chkCopyBeats.UseVisualStyleBackColor = true;
			// 
			// chkAutoLaunch
			// 
			this.chkAutoLaunch.AutoSize = true;
			this.chkAutoLaunch.Location = new System.Drawing.Point(435, 628);
			this.chkAutoLaunch.Name = "chkAutoLaunch";
			this.chkAutoLaunch.Size = new System.Drawing.Size(84, 17);
			this.chkAutoLaunch.TabIndex = 47;
			this.chkAutoLaunch.Text = "AutoLaunch";
			this.chkAutoLaunch.UseVisualStyleBackColor = true;
			this.chkAutoLaunch.CheckedChanged += new System.EventHandler(this.chkAutoLaunch_CheckedChanged);
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
			// btnAutoMap
			// 
			this.btnAutoMap.AllowDrop = true;
			this.btnAutoMap.Enabled = false;
			this.btnAutoMap.Location = new System.Drawing.Point(327, 99);
			this.btnAutoMap.Name = "btnAutoMap";
			this.btnAutoMap.Size = new System.Drawing.Size(76, 29);
			this.btnAutoMap.TabIndex = 42;
			this.btnAutoMap.Text = "Auto Map";
			this.btnAutoMap.UseVisualStyleBackColor = true;
			this.btnAutoMap.Visible = false;
			this.btnAutoMap.Click += new System.EventHandler(this.btnAutoMap_Click);
			this.btnAutoMap.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnAutoMap.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnSummary
			// 
			this.btnSummary.AllowDrop = true;
			this.btnSummary.Enabled = false;
			this.btnSummary.Location = new System.Drawing.Point(327, 406);
			this.btnSummary.Name = "btnSummary";
			this.btnSummary.Size = new System.Drawing.Size(76, 29);
			this.btnSummary.TabIndex = 39;
			this.btnSummary.Text = "Summary";
			this.btnSummary.UseVisualStyleBackColor = true;
			this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
			this.btnSummary.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSummary.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// treeMaster
			// 
			this.treeMaster.AllowDrop = true;
			this.treeMaster.BackColor = System.Drawing.Color.White;
			this.treeMaster.ImageIndex = 0;
			this.treeMaster.ImageList = this.imlTreeIcons;
			this.treeMaster.Location = new System.Drawing.Point(415, 99);
			this.treeMaster.Name = "treeMaster";
			this.treeMaster.SelectedImageIndex = 0;
			this.treeMaster.Size = new System.Drawing.Size(300, 433);
			this.treeMaster.TabIndex = 38;
			this.treeMaster.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMaster_AfterSelect);
			this.treeMaster.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.treeMaster.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
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
			// btnSaveMap
			// 
			this.btnSaveMap.AllowDrop = true;
			this.btnSaveMap.Enabled = false;
			this.btnSaveMap.Location = new System.Drawing.Point(506, 562);
			this.btnSaveMap.Name = "btnSaveMap";
			this.btnSaveMap.Size = new System.Drawing.Size(100, 25);
			this.btnSaveMap.TabIndex = 34;
			this.btnSaveMap.Text = "Save Mapping...";
			this.btnSaveMap.UseVisualStyleBackColor = true;
			this.btnSaveMap.Click += new System.EventHandler(this.btnSaveMap_Click);
			this.btnSaveMap.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSaveMap.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// txtMappingFile
			// 
			this.txtMappingFile.AllowDrop = true;
			this.txtMappingFile.Enabled = false;
			this.txtMappingFile.Location = new System.Drawing.Point(200, 565);
			this.txtMappingFile.Name = "txtMappingFile";
			this.txtMappingFile.Size = new System.Drawing.Size(300, 20);
			this.txtMappingFile.TabIndex = 36;
			this.txtMappingFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtMappingFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnLoadMap
			// 
			this.btnLoadMap.AllowDrop = true;
			this.btnLoadMap.Location = new System.Drawing.Point(94, 562);
			this.btnLoadMap.Name = "btnLoadMap";
			this.btnLoadMap.Size = new System.Drawing.Size(100, 25);
			this.btnLoadMap.TabIndex = 35;
			this.btnLoadMap.Text = "Load Mapping...";
			this.btnLoadMap.UseVisualStyleBackColor = true;
			this.btnLoadMap.Click += new System.EventHandler(this.btnLoadMap_Click);
			this.btnLoadMap.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnLoadMap.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnUnmap
			// 
			this.btnUnmap.AllowDrop = true;
			this.btnUnmap.Enabled = false;
			this.btnUnmap.Location = new System.Drawing.Point(327, 190);
			this.btnUnmap.Name = "btnUnmap";
			this.btnUnmap.Size = new System.Drawing.Size(76, 29);
			this.btnUnmap.TabIndex = 33;
			this.btnUnmap.Text = "Unmap";
			this.btnUnmap.UseVisualStyleBackColor = true;
			this.btnUnmap.Click += new System.EventHandler(this.btnUnmap_Click);
			this.btnUnmap.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnUnmap.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnMap
			// 
			this.btnMap.AllowDrop = true;
			this.btnMap.Enabled = false;
			this.btnMap.Location = new System.Drawing.Point(327, 142);
			this.btnMap.Name = "btnMap";
			this.btnMap.Size = new System.Drawing.Size(76, 29);
			this.btnMap.TabIndex = 32;
			this.btnMap.Text = "Map";
			this.btnMap.UseVisualStyleBackColor = true;
			this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
			this.btnMap.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnMap.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnSaveNewSeq
			// 
			this.btnSaveNewSeq.AllowDrop = true;
			this.btnSaveNewSeq.Enabled = false;
			this.btnSaveNewSeq.Location = new System.Drawing.Point(296, 591);
			this.btnSaveNewSeq.Name = "btnSaveNewSeq";
			this.btnSaveNewSeq.Size = new System.Drawing.Size(129, 62);
			this.btnSaveNewSeq.TabIndex = 31;
			this.btnSaveNewSeq.Text = "Make New Sequence";
			this.btnSaveNewSeq.UseVisualStyleBackColor = true;
			this.btnSaveNewSeq.Click += new System.EventHandler(this.btnSaveNewSeq_Click);
			this.btnSaveNewSeq.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSaveNewSeq.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblMasterTree
			// 
			this.lblMasterTree.AllowDrop = true;
			this.lblMasterTree.AutoSize = true;
			this.lblMasterTree.Location = new System.Drawing.Point(415, 72);
			this.lblMasterTree.Name = "lblMasterTree";
			this.lblMasterTree.Size = new System.Drawing.Size(97, 13);
			this.lblMasterTree.TabIndex = 30;
			this.lblMasterTree.Text = "Available Channels";
			this.lblMasterTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblMasterTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSourceTree
			// 
			this.lblSourceTree.AllowDrop = true;
			this.lblSourceTree.AutoSize = true;
			this.lblSourceTree.Location = new System.Drawing.Point(15, 72);
			this.lblSourceTree.Name = "lblSourceTree";
			this.lblSourceTree.Size = new System.Drawing.Size(128, 13);
			this.lblSourceTree.TabIndex = 28;
			this.lblSourceTree.Text = "Channel to be Remapped";
			this.lblSourceTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSourceTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblMasterFile
			// 
			this.lblMasterFile.AllowDrop = true;
			this.lblMasterFile.AutoSize = true;
			this.lblMasterFile.Location = new System.Drawing.Point(415, 16);
			this.lblMasterFile.Name = "lblMasterFile";
			this.lblMasterFile.Size = new System.Drawing.Size(161, 13);
			this.lblMasterFile.TabIndex = 25;
			this.lblMasterFile.Text = "Master Channel Config Template";
			this.lblMasterFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblMasterFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnBrowseMaster
			// 
			this.btnBrowseMaster.AllowDrop = true;
			this.btnBrowseMaster.Location = new System.Drawing.Point(721, 40);
			this.btnBrowseMaster.Name = "btnBrowseMaster";
			this.btnBrowseMaster.Size = new System.Drawing.Size(36, 20);
			this.btnBrowseMaster.TabIndex = 24;
			this.btnBrowseMaster.Text = "...";
			this.btnBrowseMaster.UseVisualStyleBackColor = true;
			this.btnBrowseMaster.Click += new System.EventHandler(this.btnBrowseMaster_Click);
			this.btnBrowseMaster.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseMaster.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// txtMasterFile
			// 
			this.txtMasterFile.AllowDrop = true;
			this.txtMasterFile.Enabled = false;
			this.txtMasterFile.Location = new System.Drawing.Point(415, 40);
			this.txtMasterFile.Name = "txtMasterFile";
			this.txtMasterFile.Size = new System.Drawing.Size(300, 20);
			this.txtMasterFile.TabIndex = 23;
			this.txtMasterFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtMasterFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSourceFile
			// 
			this.lblSourceFile.AllowDrop = true;
			this.lblSourceFile.AutoSize = true;
			this.lblSourceFile.Location = new System.Drawing.Point(15, 16);
			this.lblSourceFile.Name = "lblSourceFile";
			this.lblSourceFile.Size = new System.Drawing.Size(142, 13);
			this.lblSourceFile.TabIndex = 21;
			this.lblSourceFile.Text = "Source File to be Remapped";
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
			this.txtSourceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtSourceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// prgBarInner
			// 
			this.prgBarInner.BackGradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
			this.prgBarInner.BackGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
			this.prgBarInner.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
			this.prgBarInner.BackSegments = false;
			this.prgBarInner.BackTubeEndColor = System.Drawing.Color.White;
			this.prgBarInner.BackTubeStartColor = System.Drawing.Color.LightGray;
			this.prgBarInner.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.prgBarInner.BorderColor = System.Drawing.Color.Transparent;
			this.prgBarInner.BorderSingle = System.Windows.Forms.ButtonBorderStyle.None;
			this.prgBarInner.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.prgBarInner.CustomText = null;
			this.prgBarInner.CustomWaitingRender = false;
			this.prgBarInner.FontColor = System.Drawing.Color.White;
			this.prgBarInner.ForeColor = System.Drawing.Color.Lime;
			this.prgBarInner.ForegroundImage = null;
			this.prgBarInner.GradientEndColor = System.Drawing.Color.Lime;
			this.prgBarInner.GradientStartColor = System.Drawing.Color.Red;
			this.prgBarInner.Location = new System.Drawing.Point(2, 566);
			this.prgBarInner.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
			this.prgBarInner.Name = "prgBarInner";
			this.prgBarInner.SegmentWidth = 12;
			this.prgBarInner.Size = new System.Drawing.Size(762, 20);
			this.prgBarInner.TabIndex = 46;
			this.prgBarInner.Text = "progressBarAdv2";
			this.prgBarInner.TextStyle = Syncfusion.Windows.Forms.Tools.ProgressBarTextStyles.Custom;
			this.prgBarInner.ThemesEnabled = false;
			this.prgBarInner.TubeEndColor = System.Drawing.Color.Black;
			this.prgBarInner.TubeStartColor = System.Drawing.Color.Red;
			this.prgBarInner.Visible = false;
			this.prgBarInner.WaitingGradientWidth = 400;
			// 
			// prgBarOuter
			// 
			this.prgBarOuter.BackGradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
			this.prgBarOuter.BackGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
			this.prgBarOuter.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
			this.prgBarOuter.BackSegments = false;
			this.prgBarOuter.BackTubeEndColor = System.Drawing.Color.White;
			this.prgBarOuter.BackTubeStartColor = System.Drawing.Color.LightGray;
			this.prgBarOuter.CustomText = null;
			this.prgBarOuter.CustomWaitingRender = false;
			this.prgBarOuter.FontColor = System.Drawing.Color.White;
			this.prgBarOuter.ForeColor = System.Drawing.Color.DarkGreen;
			this.prgBarOuter.ForegroundImage = null;
			this.prgBarOuter.GradientEndColor = System.Drawing.Color.Red;
			this.prgBarOuter.GradientStartColor = System.Drawing.Color.Lime;
			this.prgBarOuter.Location = new System.Drawing.Point(0, 557);
			this.prgBarOuter.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
			this.prgBarOuter.Name = "prgBarOuter";
			this.prgBarOuter.SegmentWidth = 12;
			this.prgBarOuter.Size = new System.Drawing.Size(762, 40);
			this.prgBarOuter.TabIndex = 44;
			this.prgBarOuter.Text = "progressBarAdv1";
			this.prgBarOuter.ThemesEnabled = false;
			this.prgBarOuter.TubeEndColor = System.Drawing.Color.Black;
			this.prgBarOuter.TubeStartColor = System.Drawing.Color.Red;
			this.prgBarOuter.Visible = false;
			this.prgBarOuter.WaitingGradientWidth = 400;
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
			this.menuStrip1.Size = new System.Drawing.Size(761, 24);
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
			this.staStatus.Location = new System.Drawing.Point(0, 685);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(761, 24);
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
			this.pnlStatus.Size = new System.Drawing.Size(516, 19);
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
			// frmRemapper
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(761, 709);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.pnlAll);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmRemapper";
			this.Text = "Map-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRemapper_FormClosing);
			this.Load += new System.EventHandler(this.frmRemapper_Load);
			this.Shown += new System.EventHandler(this.frmRemapper_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmRemapper_Paint);
			this.pnlAll.ResumeLayout(false);
			this.pnlAll.PerformLayout();
			this.pnlMessage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.prgBarInner)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.prgBarOuter)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pnlAll;
		private System.Windows.Forms.Button btnSaveMap;
		private System.Windows.Forms.TextBox txtMappingFile;
		private System.Windows.Forms.Button btnLoadMap;
		private System.Windows.Forms.Button btnUnmap;
		private System.Windows.Forms.Button btnMap;
		private System.Windows.Forms.Button btnSaveNewSeq;
		private System.Windows.Forms.Label lblMasterTree;
		private System.Windows.Forms.Label lblSourceTree;
		private System.Windows.Forms.Label lblMasterFile;
		private System.Windows.Forms.Button btnBrowseMaster;
		private System.Windows.Forms.TextBox txtMasterFile;
		private System.Windows.Forms.Label lblSourceFile;
		private System.Windows.Forms.Button btnBrowseSource;
		private System.Windows.Forms.TextBox txtSourceFile;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.TreeView treeSource;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.TreeView treeMaster;
		private System.Windows.Forms.Button btnSummary;
		private System.Windows.Forms.Button btnAutoMap;
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
		private Syncfusion.Windows.Forms.Tools.ProgressBarAdv prgBarInner;
		private Syncfusion.Windows.Forms.Tools.ProgressBarAdv prgBarOuter;
		private System.Windows.Forms.CheckBox chkAutoLaunch;
		private System.Windows.Forms.CheckBox chkCopyBeats;
		private System.Windows.Forms.Label lblDebug;
		private System.Windows.Forms.Button btnEaves;
	}
}

