namespace UtilORama4
{
	partial class frmMapper
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
			Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapper));
			Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo2 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
			this.pnlAll = new System.Windows.Forms.Panel();
			this.chkRamps = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblDestHasEffects = new System.Windows.Forms.Label();
			this.treeDest = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.treeSource = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
			this.lblDestMappedTo = new System.Windows.Forms.Label();
			this.lblSourceMappedTo = new System.Windows.Forms.Label();
			this.pnlMapWarn = new System.Windows.Forms.Panel();
			this.btnMap = new System.Windows.Forms.Button();
			this.pnlOverwrite = new System.Windows.Forms.Panel();
			this.picPreviewDest = new System.Windows.Forms.PictureBox();
			this.picPreviewSource = new System.Windows.Forms.PictureBox();
			this.lblMapped = new System.Windows.Forms.Label();
			this.lblMappedCount = new System.Windows.Forms.Label();
			this.btnEaves = new System.Windows.Forms.Button();
			this.lblDebug = new System.Windows.Forms.Label();
			this.btnAutoMap = new System.Windows.Forms.Button();
			this.btnSummary = new System.Windows.Forms.Button();
			this.btnUnmap = new System.Windows.Forms.Button();
			this.lblDestTree = new System.Windows.Forms.Label();
			this.lblSourceTree = new System.Windows.Forms.Label();
			this.lblDestFile = new System.Windows.Forms.Label();
			this.btnBrowseDest = new System.Windows.Forms.Button();
			this.txtDestFile = new System.Windows.Forms.TextBox();
			this.lblSourceFile = new System.Windows.Forms.Label();
			this.btnBrowseSource = new System.Windows.Forms.Button();
			this.txtSourceFile = new System.Windows.Forms.TextBox();
			this.lblDestAlreadyMapped = new System.Windows.Forms.Label();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.chkCopyBeats = new System.Windows.Forms.CheckBox();
			this.chkAutoLaunch = new System.Windows.Forms.CheckBox();
			this.pnlMessage = new System.Windows.Forms.Panel();
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnSaveMap = new System.Windows.Forms.Button();
			this.txtMappingFile = new System.Windows.Forms.TextBox();
			this.btnLoadMap = new System.Windows.Forms.Button();
			this.btnSaveNewSeq = new System.Windows.Forms.Button();
			this.prgBarInner = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
			this.prgBarOuter = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOpenDest = new System.Windows.Forms.ToolStripMenuItem();
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
			this.chkFuzzy = new System.Windows.Forms.CheckBox();
			this.pnlAll.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeDest)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeSource)).BeginInit();
			this.pnlMapWarn.SuspendLayout();
			this.pnlOverwrite.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreviewDest)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picPreviewSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			this.pnlMessage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.prgBarInner)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.prgBarOuter)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.staStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlAll
			// 
			this.pnlAll.Controls.Add(this.chkRamps);
			this.pnlAll.Controls.Add(this.label1);
			this.pnlAll.Controls.Add(this.lblDestHasEffects);
			this.pnlAll.Controls.Add(this.treeDest);
			this.pnlAll.Controls.Add(this.treeSource);
			this.pnlAll.Controls.Add(this.lblDestMappedTo);
			this.pnlAll.Controls.Add(this.lblSourceMappedTo);
			this.pnlAll.Controls.Add(this.pnlMapWarn);
			this.pnlAll.Controls.Add(this.pnlOverwrite);
			this.pnlAll.Controls.Add(this.picPreviewSource);
			this.pnlAll.Controls.Add(this.lblMapped);
			this.pnlAll.Controls.Add(this.lblMappedCount);
			this.pnlAll.Controls.Add(this.btnEaves);
			this.pnlAll.Controls.Add(this.lblDebug);
			this.pnlAll.Controls.Add(this.btnAutoMap);
			this.pnlAll.Controls.Add(this.btnSummary);
			this.pnlAll.Controls.Add(this.btnUnmap);
			this.pnlAll.Controls.Add(this.lblDestTree);
			this.pnlAll.Controls.Add(this.lblSourceTree);
			this.pnlAll.Controls.Add(this.lblDestFile);
			this.pnlAll.Controls.Add(this.btnBrowseDest);
			this.pnlAll.Controls.Add(this.txtDestFile);
			this.pnlAll.Controls.Add(this.lblSourceFile);
			this.pnlAll.Controls.Add(this.btnBrowseSource);
			this.pnlAll.Controls.Add(this.txtSourceFile);
			this.pnlAll.Controls.Add(this.lblDestAlreadyMapped);
			this.pnlAll.Controls.Add(this.picAboutIcon);
			this.pnlAll.Location = new System.Drawing.Point(0, 30);
			this.pnlAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlAll.Name = "pnlAll";
			this.pnlAll.Size = new System.Drawing.Size(889, 669);
			this.pnlAll.TabIndex = 19;
			this.pnlAll.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.pnlAll.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.pnlAll.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAll_Paint);
			this.pnlAll.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlAll_MouseMove);
			// 
			// chkRamps
			// 
			this.chkRamps.AutoSize = true;
			this.chkRamps.Location = new System.Drawing.Point(383, 623);
			this.chkRamps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkRamps.Name = "chkRamps";
			this.chkRamps.Size = new System.Drawing.Size(84, 19);
			this.chkRamps.TabIndex = 131;
			this.chkRamps.Text = "Use Ramps";
			this.chkRamps.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AllowDrop = true;
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Orange;
			this.label1.Location = new System.Drawing.Point(644, 18);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 15);
			this.label1.TabIndex = 130;
			this.label1.Text = "(Master)";
			// 
			// lblDestHasEffects
			// 
			this.lblDestHasEffects.AllowDrop = true;
			this.lblDestHasEffects.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblDestHasEffects.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.lblDestHasEffects.ForeColor = System.Drawing.Color.DarkRed;
			this.lblDestHasEffects.Location = new System.Drawing.Point(378, 201);
			this.lblDestHasEffects.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDestHasEffects.Name = "lblDestHasEffects";
			this.lblDestHasEffects.Size = new System.Drawing.Size(99, 31);
			this.lblDestHasEffects.TabIndex = 119;
			this.lblDestHasEffects.Text = "Destination already has effects";
			this.lblDestHasEffects.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ttip.SetToolTip(this.lblDestHasEffects, "This destination channel already has effects.  They will be erased and overwritte" +
        "n with the effects from the mapped source channel.");
			this.lblDestHasEffects.Visible = false;
			// 
			// treeDest
			// 
			this.treeDest.BackColor = System.Drawing.Color.White;
			treeNodeAdvStyleInfo1.CheckBoxTickThickness = 1;
			treeNodeAdvStyleInfo1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
			treeNodeAdvStyleInfo1.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
			treeNodeAdvStyleInfo1.TextColor = System.Drawing.Color.Black;
			this.treeDest.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
			this.treeDest.BeforeTouchSize = new System.Drawing.Size(349, 499);
			this.treeDest.ForeColor = System.Drawing.Color.Black;
			// 
			// 
			// 
			this.treeDest.HelpTextControl.BaseThemeName = null;
			this.treeDest.HelpTextControl.Location = new System.Drawing.Point(0, 0);
			this.treeDest.HelpTextControl.Name = "";
			this.treeDest.HelpTextControl.Size = new System.Drawing.Size(392, 112);
			this.treeDest.HelpTextControl.TabIndex = 0;
			this.treeDest.HelpTextControl.Visible = true;
			this.treeDest.InactiveSelectedNodeForeColor = System.Drawing.Color.Black;
			this.treeDest.LeftImageList = this.imlTreeIcons;
			this.treeDest.Location = new System.Drawing.Point(484, 114);
			this.treeDest.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.treeDest.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
			this.treeDest.Name = "treeDest";
			this.treeDest.NodeStateImageList = this.imlTreeIcons;
			this.treeDest.SelectedNodeForeColor = System.Drawing.Color.White;
			this.treeDest.Size = new System.Drawing.Size(349, 499);
			this.treeDest.TabIndex = 118;
			this.treeDest.Text = "Destination Channels";
			this.treeDest.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 0;
			this.treeDest.ThemeStyle.TreeNodeAdvStyle.EnsureDefaultOptionedChild = true;
			// 
			// 
			// 
			this.treeDest.ToolTipControl.BaseThemeName = null;
			this.treeDest.ToolTipControl.Location = new System.Drawing.Point(0, 0);
			this.treeDest.ToolTipControl.Name = "";
			this.treeDest.ToolTipControl.Size = new System.Drawing.Size(392, 112);
			this.treeDest.ToolTipControl.TabIndex = 0;
			this.treeDest.ToolTipControl.Visible = true;
			this.treeDest.AfterSelect += new System.EventHandler(this.treeDest_AfterSelect);
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
			// treeSource
			// 
			this.treeSource.BackColor = System.Drawing.Color.White;
			treeNodeAdvStyleInfo2.CheckBoxTickThickness = 1;
			treeNodeAdvStyleInfo2.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo2.EnsureDefaultOptionedChild = true;
			treeNodeAdvStyleInfo2.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo2.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo2.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
			treeNodeAdvStyleInfo2.TextColor = System.Drawing.Color.Black;
			this.treeSource.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo2)});
			this.treeSource.BeforeTouchSize = new System.Drawing.Size(349, 499);
			this.treeSource.CanApplyTheme = false;
			this.treeSource.ForeColor = System.Drawing.Color.Black;
			// 
			// 
			// 
			this.treeSource.HelpTextControl.BaseThemeName = null;
			this.treeSource.HelpTextControl.Location = new System.Drawing.Point(0, 0);
			this.treeSource.HelpTextControl.Name = "";
			this.treeSource.HelpTextControl.Size = new System.Drawing.Size(392, 112);
			this.treeSource.HelpTextControl.TabIndex = 0;
			this.treeSource.HelpTextControl.Visible = true;
			this.treeSource.InactiveSelectedNodeForeColor = System.Drawing.Color.Black;
			this.treeSource.LeftImageList = this.imlTreeIcons;
			this.treeSource.Location = new System.Drawing.Point(18, 114);
			this.treeSource.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.treeSource.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
			this.treeSource.Name = "treeSource";
			this.treeSource.SelectedNodeForeColor = System.Drawing.Color.White;
			this.treeSource.Size = new System.Drawing.Size(349, 499);
			this.treeSource.TabIndex = 117;
			this.treeSource.Text = "Source Channels";
			this.treeSource.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 0;
			this.treeSource.ThemeStyle.TreeNodeAdvStyle.EnsureDefaultOptionedChild = true;
			// 
			// 
			// 
			this.treeSource.ToolTipControl.BaseThemeName = null;
			this.treeSource.ToolTipControl.Location = new System.Drawing.Point(0, 0);
			this.treeSource.ToolTipControl.Name = "";
			this.treeSource.ToolTipControl.Size = new System.Drawing.Size(392, 112);
			this.treeSource.ToolTipControl.TabIndex = 0;
			this.treeSource.ToolTipControl.Visible = true;
			this.treeSource.AfterSelect += new System.EventHandler(this.treeSource_AfterSelect);
			// 
			// lblDestMappedTo
			// 
			this.lblDestMappedTo.AllowDrop = true;
			this.lblDestMappedTo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblDestMappedTo.ForeColor = System.Drawing.Color.DarkMagenta;
			this.lblDestMappedTo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblDestMappedTo.Location = new System.Drawing.Point(484, 646);
			this.lblDestMappedTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDestMappedTo.Name = "lblDestMappedTo";
			this.lblDestMappedTo.Size = new System.Drawing.Size(350, 52);
			this.lblDestMappedTo.TabIndex = 116;
			this.lblDestMappedTo.Text = "Mapped";
			// 
			// lblSourceMappedTo
			// 
			this.lblSourceMappedTo.AllowDrop = true;
			this.lblSourceMappedTo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblSourceMappedTo.ForeColor = System.Drawing.Color.DarkMagenta;
			this.lblSourceMappedTo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblSourceMappedTo.Location = new System.Drawing.Point(18, 647);
			this.lblSourceMappedTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSourceMappedTo.Name = "lblSourceMappedTo";
			this.lblSourceMappedTo.Size = new System.Drawing.Size(350, 52);
			this.lblSourceMappedTo.TabIndex = 115;
			this.lblSourceMappedTo.Text = "Mapped";
			// 
			// pnlMapWarn
			// 
			this.pnlMapWarn.Controls.Add(this.btnMap);
			this.pnlMapWarn.Location = new System.Drawing.Point(380, 162);
			this.pnlMapWarn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlMapWarn.Name = "pnlMapWarn";
			this.pnlMapWarn.Size = new System.Drawing.Size(91, 36);
			this.pnlMapWarn.TabIndex = 114;
			// 
			// btnMap
			// 
			this.btnMap.AllowDrop = true;
			this.btnMap.Enabled = false;
			this.btnMap.Location = new System.Drawing.Point(1, 1);
			this.btnMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnMap.Name = "btnMap";
			this.btnMap.Size = new System.Drawing.Size(89, 33);
			this.btnMap.TabIndex = 33;
			this.btnMap.Text = "Map";
			this.btnMap.UseVisualStyleBackColor = true;
			this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
			// 
			// pnlOverwrite
			// 
			this.pnlOverwrite.BackColor = System.Drawing.Color.Red;
			this.pnlOverwrite.Controls.Add(this.picPreviewDest);
			this.pnlOverwrite.Location = new System.Drawing.Point(483, 618);
			this.pnlOverwrite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlOverwrite.Name = "pnlOverwrite";
			this.pnlOverwrite.Size = new System.Drawing.Size(352, 25);
			this.pnlOverwrite.TabIndex = 113;
			this.pnlOverwrite.Visible = false;
			// 
			// picPreviewDest
			// 
			this.picPreviewDest.BackColor = System.Drawing.Color.Silver;
			this.picPreviewDest.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picPreviewDest.Location = new System.Drawing.Point(1, 1);
			this.picPreviewDest.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picPreviewDest.Name = "picPreviewDest";
			this.picPreviewDest.Size = new System.Drawing.Size(350, 23);
			this.picPreviewDest.TabIndex = 113;
			this.picPreviewDest.TabStop = false;
			this.picPreviewDest.Visible = false;
			// 
			// picPreviewSource
			// 
			this.picPreviewSource.BackColor = System.Drawing.Color.Silver;
			this.picPreviewSource.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picPreviewSource.Location = new System.Drawing.Point(18, 621);
			this.picPreviewSource.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picPreviewSource.Name = "picPreviewSource";
			this.picPreviewSource.Size = new System.Drawing.Size(350, 23);
			this.picPreviewSource.TabIndex = 111;
			this.picPreviewSource.TabStop = false;
			this.picPreviewSource.Visible = false;
			// 
			// lblMapped
			// 
			this.lblMapped.AllowDrop = true;
			this.lblMapped.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.lblMapped.ForeColor = System.Drawing.Color.DarkMagenta;
			this.lblMapped.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblMapped.Location = new System.Drawing.Point(359, 522);
			this.lblMapped.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblMapped.Name = "lblMapped";
			this.lblMapped.Size = new System.Drawing.Size(130, 57);
			this.lblMapped.TabIndex = 52;
			this.lblMapped.Text = "Mapped";
			this.lblMapped.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblMappedCount
			// 
			this.lblMappedCount.AllowDrop = true;
			this.lblMappedCount.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.lblMappedCount.ForeColor = System.Drawing.Color.DarkMagenta;
			this.lblMappedCount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblMappedCount.Location = new System.Drawing.Point(371, 564);
			this.lblMappedCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblMappedCount.Name = "lblMappedCount";
			this.lblMappedCount.Size = new System.Drawing.Size(94, 14);
			this.lblMappedCount.TabIndex = 51;
			this.lblMappedCount.Text = "0";
			this.lblMappedCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnEaves
			// 
			this.btnEaves.Location = new System.Drawing.Point(382, 565);
			this.btnEaves.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnEaves.Name = "btnEaves";
			this.btnEaves.Size = new System.Drawing.Size(37, 37);
			this.btnEaves.TabIndex = 50;
			this.btnEaves.Text = "E!";
			this.btnEaves.UseVisualStyleBackColor = true;
			this.btnEaves.Click += new System.EventHandler(this.btnEaves_Click);
			// 
			// lblDebug
			// 
			this.lblDebug.AllowDrop = true;
			this.lblDebug.AutoSize = true;
			this.lblDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.lblDebug.ForeColor = System.Drawing.Color.BlueViolet;
			this.lblDebug.Location = new System.Drawing.Point(422, 600);
			this.lblDebug.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDebug.Name = "lblDebug";
			this.lblDebug.Size = new System.Drawing.Size(41, 12);
			this.lblDebug.TabIndex = 49;
			this.lblDebug.Text = "Debug...";
			this.lblDebug.Click += new System.EventHandler(this.lblDebug_Click);
			// 
			// btnAutoMap
			// 
			this.btnAutoMap.AllowDrop = true;
			this.btnAutoMap.Enabled = false;
			this.btnAutoMap.Location = new System.Drawing.Point(382, 114);
			this.btnAutoMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnAutoMap.Name = "btnAutoMap";
			this.btnAutoMap.Size = new System.Drawing.Size(89, 33);
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
			this.btnSummary.Location = new System.Drawing.Point(380, 303);
			this.btnSummary.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnSummary.Name = "btnSummary";
			this.btnSummary.Size = new System.Drawing.Size(89, 33);
			this.btnSummary.TabIndex = 39;
			this.btnSummary.Text = "Summary";
			this.btnSummary.UseVisualStyleBackColor = true;
			this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
			this.btnSummary.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSummary.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnUnmap
			// 
			this.btnUnmap.AllowDrop = true;
			this.btnUnmap.Enabled = false;
			this.btnUnmap.Location = new System.Drawing.Point(382, 263);
			this.btnUnmap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnUnmap.Name = "btnUnmap";
			this.btnUnmap.Size = new System.Drawing.Size(89, 33);
			this.btnUnmap.TabIndex = 33;
			this.btnUnmap.Text = "Unmap";
			this.btnUnmap.UseVisualStyleBackColor = true;
			this.btnUnmap.Click += new System.EventHandler(this.btnUnmap_Click);
			this.btnUnmap.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnUnmap.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblDestTree
			// 
			this.lblDestTree.AllowDrop = true;
			this.lblDestTree.AutoSize = true;
			this.lblDestTree.Location = new System.Drawing.Point(484, 83);
			this.lblDestTree.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDestTree.Name = "lblDestTree";
			this.lblDestTree.Size = new System.Drawing.Size(113, 15);
			this.lblDestTree.TabIndex = 30;
			this.lblDestTree.Text = "Channels to copy to";
			this.lblDestTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblDestTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSourceTree
			// 
			this.lblSourceTree.AllowDrop = true;
			this.lblSourceTree.AutoSize = true;
			this.lblSourceTree.Location = new System.Drawing.Point(18, 83);
			this.lblSourceTree.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSourceTree.Name = "lblSourceTree";
			this.lblSourceTree.Size = new System.Drawing.Size(125, 15);
			this.lblSourceTree.TabIndex = 28;
			this.lblSourceTree.Text = "Channels to be copied";
			this.lblSourceTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSourceTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblDestFile
			// 
			this.lblDestFile.AllowDrop = true;
			this.lblDestFile.AutoSize = true;
			this.lblDestFile.Location = new System.Drawing.Point(484, 18);
			this.lblDestFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDestFile.Name = "lblDestFile";
			this.lblDestFile.Size = new System.Drawing.Size(142, 15);
			this.lblDestFile.TabIndex = 25;
			this.lblDestFile.Text = "Destination Sequence File";
			this.lblDestFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblDestFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnBrowseDest
			// 
			this.btnBrowseDest.AllowDrop = true;
			this.btnBrowseDest.Location = new System.Drawing.Point(841, 46);
			this.btnBrowseDest.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnBrowseDest.Name = "btnBrowseDest";
			this.btnBrowseDest.Size = new System.Drawing.Size(42, 23);
			this.btnBrowseDest.TabIndex = 24;
			this.btnBrowseDest.Text = "...";
			this.btnBrowseDest.UseVisualStyleBackColor = true;
			this.btnBrowseDest.Click += new System.EventHandler(this.btnBrowseDest_Click);
			this.btnBrowseDest.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseDest.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// txtDestFile
			// 
			this.txtDestFile.AllowDrop = true;
			this.txtDestFile.Enabled = false;
			this.txtDestFile.Location = new System.Drawing.Point(484, 46);
			this.txtDestFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtDestFile.Name = "txtDestFile";
			this.txtDestFile.Size = new System.Drawing.Size(349, 23);
			this.txtDestFile.TabIndex = 23;
			this.txtDestFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtDestFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSourceFile
			// 
			this.lblSourceFile.AllowDrop = true;
			this.lblSourceFile.AutoSize = true;
			this.lblSourceFile.Location = new System.Drawing.Point(18, 18);
			this.lblSourceFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSourceFile.Name = "lblSourceFile";
			this.lblSourceFile.Size = new System.Drawing.Size(118, 15);
			this.lblSourceFile.TabIndex = 21;
			this.lblSourceFile.Text = "Source Sequence File";
			this.lblSourceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSourceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnBrowseSource
			// 
			this.btnBrowseSource.AllowDrop = true;
			this.btnBrowseSource.Location = new System.Drawing.Point(374, 46);
			this.btnBrowseSource.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnBrowseSource.Name = "btnBrowseSource";
			this.btnBrowseSource.Size = new System.Drawing.Size(42, 23);
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
			this.txtSourceFile.Location = new System.Drawing.Point(18, 46);
			this.txtSourceFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtSourceFile.Name = "txtSourceFile";
			this.txtSourceFile.Size = new System.Drawing.Size(349, 23);
			this.txtSourceFile.TabIndex = 19;
			this.txtSourceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtSourceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblDestAlreadyMapped
			// 
			this.lblDestAlreadyMapped.AllowDrop = true;
			this.lblDestAlreadyMapped.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblDestAlreadyMapped.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.lblDestAlreadyMapped.ForeColor = System.Drawing.Color.DarkOrange;
			this.lblDestAlreadyMapped.Location = new System.Drawing.Point(360, 232);
			this.lblDestAlreadyMapped.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDestAlreadyMapped.Name = "lblDestAlreadyMapped";
			this.lblDestAlreadyMapped.Size = new System.Drawing.Size(128, 31);
			this.lblDestAlreadyMapped.TabIndex = 120;
			this.lblDestAlreadyMapped.Text = "Destination already mapped to other source";
			this.lblDestAlreadyMapped.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ttip.SetToolTip(this.lblDestAlreadyMapped, resources.GetString("lblDestAlreadyMapped.ToolTip"));
			this.lblDestAlreadyMapped.Visible = false;
			// 
			// picAboutIcon
			// 
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(361, 370);
			this.picAboutIcon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 129;
			this.picAboutIcon.TabStop = false;
			// 
			// chkCopyBeats
			// 
			this.chkCopyBeats.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.chkCopyBeats.Checked = true;
			this.chkCopyBeats.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCopyBeats.Location = new System.Drawing.Point(18, 797);
			this.chkCopyBeats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkCopyBeats.Name = "chkCopyBeats";
			this.chkCopyBeats.Size = new System.Drawing.Size(292, 69);
			this.chkCopyBeats.TabIndex = 48;
			this.chkCopyBeats.Text = "Copy Beat Track(s)";
			this.chkCopyBeats.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.chkCopyBeats.UseVisualStyleBackColor = true;
			// 
			// chkAutoLaunch
			// 
			this.chkAutoLaunch.AutoSize = true;
			this.chkAutoLaunch.Location = new System.Drawing.Point(504, 785);
			this.chkAutoLaunch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkAutoLaunch.Name = "chkAutoLaunch";
			this.chkAutoLaunch.Size = new System.Drawing.Size(91, 19);
			this.chkAutoLaunch.TabIndex = 47;
			this.chkAutoLaunch.Text = "AutoLaunch";
			this.chkAutoLaunch.UseVisualStyleBackColor = true;
			this.chkAutoLaunch.CheckedChanged += new System.EventHandler(this.chkAutoLaunch_CheckedChanged);
			// 
			// pnlMessage
			// 
			this.pnlMessage.Controls.Add(this.lblMessage);
			this.pnlMessage.Location = new System.Drawing.Point(18, 706);
			this.pnlMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlMessage.Name = "pnlMessage";
			this.pnlMessage.Size = new System.Drawing.Size(817, 17);
			this.pnlMessage.TabIndex = 43;
			// 
			// lblMessage
			// 
			this.lblMessage.AllowDrop = true;
			this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.lblMessage.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblMessage.Location = new System.Drawing.Point(1, 1);
			this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(814, 15);
			this.lblMessage.TabIndex = 42;
			this.lblMessage.Text = "Status Message";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblMessage.Visible = false;
			// 
			// btnSaveMap
			// 
			this.btnSaveMap.AllowDrop = true;
			this.btnSaveMap.Enabled = false;
			this.btnSaveMap.Location = new System.Drawing.Point(590, 736);
			this.btnSaveMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnSaveMap.Name = "btnSaveMap";
			this.btnSaveMap.Size = new System.Drawing.Size(117, 29);
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
			this.txtMappingFile.Location = new System.Drawing.Point(233, 740);
			this.txtMappingFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtMappingFile.Name = "txtMappingFile";
			this.txtMappingFile.Size = new System.Drawing.Size(349, 23);
			this.txtMappingFile.TabIndex = 36;
			this.txtMappingFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtMappingFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnLoadMap
			// 
			this.btnLoadMap.AllowDrop = true;
			this.btnLoadMap.Location = new System.Drawing.Point(110, 736);
			this.btnLoadMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnLoadMap.Name = "btnLoadMap";
			this.btnLoadMap.Size = new System.Drawing.Size(117, 29);
			this.btnLoadMap.TabIndex = 35;
			this.btnLoadMap.Text = "Load Mapping...";
			this.btnLoadMap.UseVisualStyleBackColor = true;
			this.btnLoadMap.Click += new System.EventHandler(this.btnLoadMap_Click);
			this.btnLoadMap.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnLoadMap.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnSaveNewSeq
			// 
			this.btnSaveNewSeq.AllowDrop = true;
			this.btnSaveNewSeq.Enabled = false;
			this.btnSaveNewSeq.Location = new System.Drawing.Point(345, 770);
			this.btnSaveNewSeq.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnSaveNewSeq.Name = "btnSaveNewSeq";
			this.btnSaveNewSeq.Size = new System.Drawing.Size(150, 72);
			this.btnSaveNewSeq.TabIndex = 31;
			this.btnSaveNewSeq.Text = "Make New Sequence";
			this.btnSaveNewSeq.UseVisualStyleBackColor = true;
			this.btnSaveNewSeq.Click += new System.EventHandler(this.btnSaveNewSeq_Click);
			this.btnSaveNewSeq.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSaveNewSeq.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// prgBarInner
			// 
			this.prgBarInner.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
			this.prgBarInner.BackSegments = false;
			this.prgBarInner.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.prgBarInner.BorderColor = System.Drawing.Color.Transparent;
			this.prgBarInner.BorderSingle = System.Windows.Forms.ButtonBorderStyle.None;
			this.prgBarInner.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.prgBarInner.CustomText = null;
			this.prgBarInner.CustomWaitingRender = false;
			this.prgBarInner.ForeColor = System.Drawing.Color.Lime;
			this.prgBarInner.ForegroundImage = null;
			this.prgBarInner.Location = new System.Drawing.Point(264, 808);
			this.prgBarInner.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.prgBarInner.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
			this.prgBarInner.Name = "prgBarInner";
			this.prgBarInner.SegmentWidth = 12;
			this.prgBarInner.Size = new System.Drawing.Size(889, 23);
			this.prgBarInner.TabIndex = 46;
			this.prgBarInner.Text = "progressBarAdv2";
			this.prgBarInner.TextStyle = Syncfusion.Windows.Forms.Tools.ProgressBarTextStyles.Custom;
			this.prgBarInner.Visible = false;
			this.prgBarInner.WaitingGradientWidth = 400;
			// 
			// prgBarOuter
			// 
			this.prgBarOuter.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
			this.prgBarOuter.BackSegments = false;
			this.prgBarOuter.CustomText = null;
			this.prgBarOuter.CustomWaitingRender = false;
			this.prgBarOuter.ForeColor = System.Drawing.Color.DarkGreen;
			this.prgBarOuter.ForegroundImage = null;
			this.prgBarOuter.GradientEndColor = System.Drawing.Color.Red;
			this.prgBarOuter.GradientStartColor = System.Drawing.Color.Lime;
			this.prgBarOuter.Location = new System.Drawing.Point(244, 785);
			this.prgBarOuter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.prgBarOuter.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
			this.prgBarOuter.Name = "prgBarOuter";
			this.prgBarOuter.SegmentWidth = 12;
			this.prgBarOuter.Size = new System.Drawing.Size(889, 46);
			this.prgBarOuter.TabIndex = 44;
			this.prgBarOuter.Text = "progressBarAdv1";
			this.prgBarOuter.Visible = false;
			this.prgBarOuter.WaitingGradientWidth = 400;
			// 
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
			// 
			// menuStrip1
			// 
			this.menuStrip1.AllowDrop = true;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuMapMenu});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(888, 24);
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
            this.mnuOpenDest,
            this.mnuOpenSource,
            this.mnuOpenMap});
			this.mnuFileOpen.Name = "mnuFileOpen";
			this.mnuFileOpen.Size = new System.Drawing.Size(135, 22);
			this.mnuFileOpen.Text = "&Open";
			// 
			// mnuOpenDest
			// 
			this.mnuOpenDest.Name = "mnuOpenDest";
			this.mnuOpenDest.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
			this.mnuOpenDest.Size = new System.Drawing.Size(233, 22);
			this.mnuOpenDest.Text = "&Destination Sequence";
			this.mnuOpenDest.Click += new System.EventHandler(this.mnuOpenDest_Click);
			// 
			// mnuOpenSource
			// 
			this.mnuOpenSource.Name = "mnuOpenSource";
			this.mnuOpenSource.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mnuOpenSource.Size = new System.Drawing.Size(233, 22);
			this.mnuOpenSource.Text = "&Source Sequence(s)";
			this.mnuOpenSource.Click += new System.EventHandler(this.mnuOpenSource_Click);
			// 
			// mnuOpenMap
			// 
			this.mnuOpenMap.Name = "mnuOpenMap";
			this.mnuOpenMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
			this.mnuOpenMap.Size = new System.Drawing.Size(233, 22);
			this.mnuOpenMap.Text = "Ma&ppings";
			this.mnuOpenMap.Click += new System.EventHandler(this.mnuOpenMap_Click);
			// 
			// mnuFileSaveAs
			// 
			this.mnuFileSaveAs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveNewSequence,
            this.mnuSaveNewMap});
			this.mnuFileSaveAs.Name = "mnuFileSaveAs";
			this.mnuFileSaveAs.Size = new System.Drawing.Size(135, 22);
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
			this.mnuFileDivider1.Size = new System.Drawing.Size(132, 6);
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
			this.mnuOptions.Size = new System.Drawing.Size(135, 22);
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
			this.mnuFileDivider2.Size = new System.Drawing.Size(132, 6);
			// 
			// mnuExit
			// 
			this.mnuExit.Name = "mnuExit";
			this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.mnuExit.Size = new System.Drawing.Size(135, 22);
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
			// ttip
			// 
			this.ttip.ShowAlways = true;
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlProgress,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 848);
			this.staStatus.Name = "staStatus";
			this.staStatus.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.staStatus.Size = new System.Drawing.Size(888, 24);
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
			this.pnlStatus.Size = new System.Drawing.Size(774, 19);
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
			// chkFuzzy
			// 
			this.chkFuzzy.AutoSize = true;
			this.chkFuzzy.Font = new System.Drawing.Font("Fuzzed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkFuzzy.Location = new System.Drawing.Point(110, 770);
			this.chkFuzzy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkFuzzy.Name = "chkFuzzy";
			this.chkFuzzy.Size = new System.Drawing.Size(113, 18);
			this.chkFuzzy.TabIndex = 109;
			this.chkFuzzy.Text = "Fuzzy Match";
			this.chkFuzzy.UseVisualStyleBackColor = true;
			// 
			// frmMapper
			// 
			this.AllowDrop = true;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(888, 872);
			this.Controls.Add(this.chkFuzzy);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.pnlAll);
			this.Controls.Add(this.btnSaveNewSeq);
			this.Controls.Add(this.chkCopyBeats);
			this.Controls.Add(this.prgBarOuter);
			this.Controls.Add(this.chkAutoLaunch);
			this.Controls.Add(this.prgBarInner);
			this.Controls.Add(this.pnlMessage);
			this.Controls.Add(this.btnLoadMap);
			this.Controls.Add(this.txtMappingFile);
			this.Controls.Add(this.btnSaveMap);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "frmMapper";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Map-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRemapper_FormClosing);
			this.Load += new System.EventHandler(this.frmRemapper_Load);
			this.Shown += new System.EventHandler(this.frmRemapper_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmRemapper_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmRemapper_MouseMove);
			this.pnlAll.ResumeLayout(false);
			this.pnlAll.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeDest)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeSource)).EndInit();
			this.pnlMapWarn.ResumeLayout(false);
			this.pnlOverwrite.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picPreviewDest)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picPreviewSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
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
		private System.Windows.Forms.Button btnSaveNewSeq;
		private System.Windows.Forms.Label lblDestTree;
		private System.Windows.Forms.Label lblSourceTree;
		private System.Windows.Forms.Label lblDestFile;
		private System.Windows.Forms.Button btnBrowseDest;
		private System.Windows.Forms.TextBox txtDestFile;
		private System.Windows.Forms.Label lblSourceFile;
		private System.Windows.Forms.Button btnBrowseSource;
		private System.Windows.Forms.TextBox txtSourceFile;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.Button btnSummary;
		private System.Windows.Forms.Button btnAutoMap;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
		private System.Windows.Forms.ToolStripMenuItem mnuOpenDest;
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
		private System.Windows.Forms.Label lblMapped;
		private System.Windows.Forms.Label lblMappedCount;
		private System.Windows.Forms.PictureBox picPreviewSource;
		private System.Windows.Forms.Panel pnlOverwrite;
		private System.Windows.Forms.PictureBox picPreviewDest;
		private System.Windows.Forms.Panel pnlMapWarn;
		private System.Windows.Forms.Button btnMap;
		private System.Windows.Forms.Label lblDestMappedTo;
		private System.Windows.Forms.Label lblSourceMappedTo;
		private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeSource;
		private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeDest;
		private System.Windows.Forms.Label lblDestHasEffects;
		private System.Windows.Forms.Label lblDestAlreadyMapped;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkFuzzy;
		private System.Windows.Forms.CheckBox chkRamps;
	}
}

