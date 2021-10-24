
namespace UtilORama4
{
	partial class frmDim
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDim));
			Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblFilenameDest = new System.Windows.Forms.Label();
			this.btnSaveSeq = new System.Windows.Forms.Button();
			this.txtFilenameDest = new System.Windows.Forms.TextBox();
			this.lblSourceFile = new System.Windows.Forms.Label();
			this.btnBrowseSource = new System.Windows.Forms.Button();
			this.txtFilenameSource = new System.Windows.Forms.TextBox();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.ttip = new System.Windows.Forms.ToolTip(this.components);
			this.tabDim = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
			this.lblTabFunction = new System.Windows.Forms.Label();
			this.picPreviewSource = new System.Windows.Forms.PictureBox();
			this.treeSource = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
			this.tabTrim = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
			this.tabOnOff = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
			this.tabMinTime = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
			this.tabNoChange = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.txtTime = new System.Windows.Forms.TextBox();
			this.txtTrim = new System.Windows.Forms.TextBox();
			this.txtDim = new System.Windows.Forms.TextBox();
			this.tabChannels = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowseMap = new System.Windows.Forms.Button();
			this.txtFilenameMap = new System.Windows.Forms.TextBox();
			this.btnSaveMap = new System.Windows.Forms.Button();
			this.chkLaunch = new System.Windows.Forms.CheckBox();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.staStatus.SuspendLayout();
			this.tabDim.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreviewSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabChannels)).BeginInit();
			this.tabChannels.SuspendLayout();
			this.SuspendLayout();
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 716);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(657, 24);
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
			this.pnlHelp.Click += new System.EventHandler(this.pnlHelp_Click);
			// 
			// pnlStatus
			// 
			this.pnlStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pnlStatus.Name = "pnlStatus";
			this.pnlStatus.Size = new System.Drawing.Size(545, 19);
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
			// btnOK
			// 
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(279, 690);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 67;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Visible = false;
			// 
			// lblFilenameDest
			// 
			this.lblFilenameDest.AllowDrop = true;
			this.lblFilenameDest.AutoSize = true;
			this.lblFilenameDest.Location = new System.Drawing.Point(12, 651);
			this.lblFilenameDest.Name = "lblFilenameDest";
			this.lblFilenameDest.Size = new System.Drawing.Size(131, 13);
			this.lblFilenameDest.TabIndex = 117;
			this.lblFilenameDest.Text = "Destination Sequence File";
			// 
			// btnSaveSeq
			// 
			this.btnSaveSeq.AllowDrop = true;
			this.btnSaveSeq.Location = new System.Drawing.Point(318, 667);
			this.btnSaveSeq.Name = "btnSaveSeq";
			this.btnSaveSeq.Size = new System.Drawing.Size(36, 20);
			this.btnSaveSeq.TabIndex = 116;
			this.btnSaveSeq.Text = "...";
			this.btnSaveSeq.UseVisualStyleBackColor = true;
			// 
			// txtFilenameDest
			// 
			this.txtFilenameDest.AllowDrop = true;
			this.txtFilenameDest.BackColor = System.Drawing.Color.LightGray;
			this.txtFilenameDest.Enabled = false;
			this.txtFilenameDest.Location = new System.Drawing.Point(12, 667);
			this.txtFilenameDest.Name = "txtFilenameDest";
			this.txtFilenameDest.Size = new System.Drawing.Size(300, 20);
			this.txtFilenameDest.TabIndex = 115;
			this.txtFilenameDest.Text = "       (Click to save the modified sequence)  --->";
			// 
			// lblSourceFile
			// 
			this.lblSourceFile.AllowDrop = true;
			this.lblSourceFile.AutoSize = true;
			this.lblSourceFile.Location = new System.Drawing.Point(12, 9);
			this.lblSourceFile.Name = "lblSourceFile";
			this.lblSourceFile.Size = new System.Drawing.Size(112, 13);
			this.lblSourceFile.TabIndex = 114;
			this.lblSourceFile.Text = "Source Sequence File";
			// 
			// btnBrowseSource
			// 
			this.btnBrowseSource.AllowDrop = true;
			this.btnBrowseSource.Location = new System.Drawing.Point(318, 25);
			this.btnBrowseSource.Name = "btnBrowseSource";
			this.btnBrowseSource.Size = new System.Drawing.Size(36, 20);
			this.btnBrowseSource.TabIndex = 113;
			this.btnBrowseSource.Text = "...";
			this.btnBrowseSource.UseVisualStyleBackColor = true;
			this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
			// 
			// txtFilenameSource
			// 
			this.txtFilenameSource.AllowDrop = true;
			this.txtFilenameSource.BackColor = System.Drawing.Color.LightGray;
			this.txtFilenameSource.Enabled = false;
			this.txtFilenameSource.Location = new System.Drawing.Point(12, 25);
			this.txtFilenameSource.Name = "txtFilenameSource";
			this.txtFilenameSource.Size = new System.Drawing.Size(300, 20);
			this.txtFilenameSource.TabIndex = 112;
			this.txtFilenameSource.Text = "       (Click to select a sequence)  --->";
			// 
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
			// 
			// ttip
			// 
			this.ttip.ShowAlways = true;
			// 
			// tabDim
			// 
			this.tabDim.BackColor = System.Drawing.Color.LightGray;
			this.tabDim.Controls.Add(this.lblTabFunction);
			this.tabDim.Controls.Add(this.picPreviewSource);
			this.tabDim.Controls.Add(this.treeSource);
			this.tabDim.Image = null;
			this.tabDim.ImageSize = new System.Drawing.Size(16, 16);
			this.tabDim.Location = new System.Drawing.Point(1, 25);
			this.tabDim.Name = "tabDim";
			this.tabDim.ShowCloseButton = true;
			this.tabDim.Size = new System.Drawing.Size(321, 507);
			this.tabDim.TabForeColor = System.Drawing.Color.Crimson;
			this.tabDim.TabIndex = 1;
			this.tabDim.Text = "Dim       %";
			this.tabDim.ThemesEnabled = false;
			this.ttip.SetToolTip(this.tabDim, resources.GetString("tabDim.ToolTip"));
			// 
			// lblTabFunction
			// 
			this.lblTabFunction.AllowDrop = true;
			this.lblTabFunction.AutoSize = true;
			this.lblTabFunction.Location = new System.Drawing.Point(6, 5);
			this.lblTabFunction.Name = "lblTabFunction";
			this.lblTabFunction.Size = new System.Drawing.Size(252, 13);
			this.lblTabFunction.TabIndex = 143;
			this.lblTabFunction.Text = "Scale these channels to a maximum of 70% intensity";
			// 
			// picPreviewSource
			// 
			this.picPreviewSource.BackColor = System.Drawing.Color.Tan;
			this.picPreviewSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picPreviewSource.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picPreviewSource.Location = new System.Drawing.Point(9, 459);
			this.picPreviewSource.Name = "picPreviewSource";
			this.picPreviewSource.Size = new System.Drawing.Size(300, 33);
			this.picPreviewSource.TabIndex = 142;
			this.picPreviewSource.TabStop = false;
			this.picPreviewSource.Visible = false;
			// 
			// treeSource
			// 
			this.treeSource.BackColor = System.Drawing.Color.White;
			this.treeSource.BackgroundColor = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.LightGray);
			treeNodeAdvStyleInfo1.CheckBoxTickThickness = 1;
			treeNodeAdvStyleInfo1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
			treeNodeAdvStyleInfo1.InteractiveCheckBox = true;
			treeNodeAdvStyleInfo1.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
			treeNodeAdvStyleInfo1.ShowCheckBox = true;
			treeNodeAdvStyleInfo1.TextColor = System.Drawing.Color.Black;
			this.treeSource.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
			this.treeSource.BeforeTouchSize = new System.Drawing.Size(300, 433);
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
			this.treeSource.InactiveSelectedNodeForeColor = System.Drawing.SystemColors.ControlText;
			this.treeSource.InteractiveCheckBoxes = true;
			this.treeSource.Location = new System.Drawing.Point(9, 21);
			this.treeSource.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
			this.treeSource.Name = "treeSource";
			this.treeSource.SelectedNodeForeColor = System.Drawing.SystemColors.HighlightText;
			this.treeSource.ShouldSelectNodeOnEnter = false;
			this.treeSource.ShowCheckBoxes = true;
			this.treeSource.Size = new System.Drawing.Size(300, 433);
			this.treeSource.TabIndex = 141;
			this.treeSource.Text = "treeViewAdv1";
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
			this.treeSource.AfterCheck += new Syncfusion.Windows.Forms.Tools.TreeNodeAdvEventHandler(this.treeSource_AfterCheck);
			// 
			// tabTrim
			// 
			this.tabTrim.Image = null;
			this.tabTrim.ImageSize = new System.Drawing.Size(16, 16);
			this.tabTrim.Location = new System.Drawing.Point(1, 25);
			this.tabTrim.Name = "tabTrim";
			this.tabTrim.ShowCloseButton = true;
			this.tabTrim.Size = new System.Drawing.Size(321, 507);
			this.tabTrim.TabForeColor = System.Drawing.Color.Blue;
			this.tabTrim.TabIndex = 2;
			this.tabTrim.Text = "Trim       %";
			this.tabTrim.ThemesEnabled = false;
			this.ttip.SetToolTip(this.tabTrim, resources.GetString("tabTrim.ToolTip"));
			// 
			// tabOnOff
			// 
			this.tabOnOff.Image = null;
			this.tabOnOff.ImageSize = new System.Drawing.Size(16, 16);
			this.tabOnOff.Location = new System.Drawing.Point(1, 25);
			this.tabOnOff.Name = "tabOnOff";
			this.tabOnOff.ShowCloseButton = true;
			this.tabOnOff.Size = new System.Drawing.Size(321, 507);
			this.tabOnOff.TabForeColor = System.Drawing.Color.Green;
			this.tabOnOff.TabIndex = 3;
			this.tabOnOff.Text = "On-Off";
			this.tabOnOff.ThemesEnabled = false;
			this.ttip.SetToolTip(this.tabOnOff, resources.GetString("tabOnOff.ToolTip"));
			// 
			// tabMinTime
			// 
			this.tabMinTime.Image = null;
			this.tabMinTime.ImageSize = new System.Drawing.Size(16, 16);
			this.tabMinTime.Location = new System.Drawing.Point(1, 25);
			this.tabMinTime.Name = "tabMinTime";
			this.tabMinTime.ShowCloseButton = true;
			this.tabMinTime.Size = new System.Drawing.Size(321, 507);
			this.tabMinTime.TabForeColor = System.Drawing.Color.Purple;
			this.tabMinTime.TabIndex = 4;
			this.tabMinTime.Text = "Min       Sec";
			this.tabMinTime.ThemesEnabled = false;
			this.ttip.SetToolTip(this.tabMinTime, resources.GetString("tabMinTime.ToolTip"));
			// 
			// tabNoChange
			// 
			this.tabNoChange.Image = null;
			this.tabNoChange.ImageSize = new System.Drawing.Size(16, 16);
			this.tabNoChange.Location = new System.Drawing.Point(1, 25);
			this.tabNoChange.Name = "tabNoChange";
			this.tabNoChange.ShowCloseButton = true;
			this.tabNoChange.Size = new System.Drawing.Size(321, 507);
			this.tabNoChange.TabIndex = 5;
			this.tabNoChange.Text = "No Change";
			this.tabNoChange.ThemesEnabled = false;
			this.ttip.SetToolTip(this.tabNoChange, "These \'leftover\' channels will not be\r\ndimmed, trimmed, set to on-off, or\r\nhave a" +
        " minimum time applied.  They\r\nwill not be changed in any way.");
			// 
			// picAboutIcon
			// 
			this.picAboutIcon.ErrorImage = null;
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.InitialImage = null;
			this.picAboutIcon.Location = new System.Drawing.Point(528, 559);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 128;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// txtTime
			// 
			this.txtTime.Location = new System.Drawing.Point(232, 2);
			this.txtTime.MaxLength = 2;
			this.txtTime.Name = "txtTime";
			this.txtTime.Size = new System.Drawing.Size(18, 20);
			this.txtTime.TabIndex = 138;
			this.txtTime.Text = "10";
			this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtTime.Enter += new System.EventHandler(this.txtTime_Enter);
			// 
			// txtTrim
			// 
			this.txtTrim.Location = new System.Drawing.Point(208, 2);
			this.txtTrim.MaxLength = 2;
			this.txtTrim.Name = "txtTrim";
			this.txtTrim.Size = new System.Drawing.Size(18, 20);
			this.txtTrim.TabIndex = 137;
			this.txtTrim.Text = "80";
			this.txtTrim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtTrim.Enter += new System.EventHandler(this.txtDim2_Enter);
			// 
			// txtDim
			// 
			this.txtDim.Location = new System.Drawing.Point(184, 2);
			this.txtDim.MaxLength = 2;
			this.txtDim.Name = "txtDim";
			this.txtDim.Size = new System.Drawing.Size(18, 20);
			this.txtDim.TabIndex = 136;
			this.txtDim.Text = "70";
			this.txtDim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtDim.Enter += new System.EventHandler(this.txtDim1_Enter);
			// 
			// tabChannels
			// 
			this.tabChannels.BackColor = System.Drawing.Color.LightGray;
			this.tabChannels.BeforeTouchSize = new System.Drawing.Size(324, 534);
			this.tabChannels.Controls.Add(this.tabNoChange);
			this.tabChannels.Controls.Add(this.tabDim);
			this.tabChannels.Controls.Add(this.tabTrim);
			this.tabChannels.Controls.Add(this.tabOnOff);
			this.tabChannels.Controls.Add(this.tabMinTime);
			this.tabChannels.InactiveTabColor = System.Drawing.Color.DarkGray;
			this.tabChannels.Location = new System.Drawing.Point(12, 105);
			this.tabChannels.Name = "tabChannels";
			this.tabChannels.PersistTabState = true;
			this.tabChannels.ShowScroll = false;
			this.tabChannels.Size = new System.Drawing.Size(324, 534);
			this.tabChannels.TabIndex = 139;
			this.tabChannels.SelectedIndexChanged += new System.EventHandler(this.tabChannels_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AllowDrop = true;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(602, 154);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 141;
			this.label2.Text = "Channels:";
			this.label2.Visible = false;
			// 
			// label1
			// 
			this.label1.AllowDrop = true;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 54);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 13);
			this.label1.TabIndex = 144;
			this.label1.Text = "Channel List File";
			// 
			// btnBrowseMap
			// 
			this.btnBrowseMap.AllowDrop = true;
			this.btnBrowseMap.Location = new System.Drawing.Point(242, 69);
			this.btnBrowseMap.Name = "btnBrowseMap";
			this.btnBrowseMap.Size = new System.Drawing.Size(53, 20);
			this.btnBrowseMap.TabIndex = 143;
			this.btnBrowseMap.Text = "Load...";
			this.btnBrowseMap.UseVisualStyleBackColor = true;
			this.btnBrowseMap.Click += new System.EventHandler(this.btnBrowseChannels_Click);
			// 
			// txtFilenameMap
			// 
			this.txtFilenameMap.AllowDrop = true;
			this.txtFilenameMap.BackColor = System.Drawing.Color.LightGray;
			this.txtFilenameMap.Enabled = false;
			this.txtFilenameMap.Location = new System.Drawing.Point(12, 70);
			this.txtFilenameMap.Name = "txtFilenameMap";
			this.txtFilenameMap.Size = new System.Drawing.Size(224, 20);
			this.txtFilenameMap.TabIndex = 142;
			this.txtFilenameMap.Text = "       (None)   (Click to load a channel list)  --->";
			// 
			// btnSaveMap
			// 
			this.btnSaveMap.AllowDrop = true;
			this.btnSaveMap.Location = new System.Drawing.Point(301, 69);
			this.btnSaveMap.Name = "btnSaveMap";
			this.btnSaveMap.Size = new System.Drawing.Size(53, 20);
			this.btnSaveMap.TabIndex = 145;
			this.btnSaveMap.Text = "Save...";
			this.btnSaveMap.UseVisualStyleBackColor = true;
			this.btnSaveMap.Click += new System.EventHandler(this.btnSaveChannels_Click);
			// 
			// chkLaunch
			// 
			this.chkLaunch.AutoSize = true;
			this.chkLaunch.Location = new System.Drawing.Point(22, 690);
			this.chkLaunch.Name = "chkLaunch";
			this.chkLaunch.Size = new System.Drawing.Size(155, 17);
			this.chkLaunch.TabIndex = 146;
			this.chkLaunch.Text = "Launch in Sequence Editor";
			this.chkLaunch.UseVisualStyleBackColor = true;
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
			// frmDim
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LightGray;
			this.ClientSize = new System.Drawing.Size(657, 740);
			this.Controls.Add(this.chkLaunch);
			this.Controls.Add(this.btnSaveMap);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnBrowseMap);
			this.Controls.Add(this.txtFilenameMap);
			this.Controls.Add(this.txtDim);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tabChannels);
			this.Controls.Add(this.txtTime);
			this.Controls.Add(this.txtTrim);
			this.Controls.Add(this.picAboutIcon);
			this.Controls.Add(this.lblFilenameDest);
			this.Controls.Add(this.btnSaveSeq);
			this.Controls.Add(this.txtFilenameDest);
			this.Controls.Add(this.lblSourceFile);
			this.Controls.Add(this.btnBrowseSource);
			this.Controls.Add(this.txtFilenameSource);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.staStatus);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(750, 950);
			this.MinimumSize = new System.Drawing.Size(380, 600);
			this.Name = "frmDim";
			this.Text = "Dim-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDim_FormClosing);
			this.Load += new System.EventHandler(this.frmDim_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmDim_Paint);
			this.Resize += new System.EventHandler(this.frmDim_Resize);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			this.tabDim.ResumeLayout(false);
			this.tabDim.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreviewSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabChannels)).EndInit();
			this.tabChannels.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblFilenameDest;
		private System.Windows.Forms.Button btnSaveSeq;
		private System.Windows.Forms.TextBox txtFilenameDest;
		private System.Windows.Forms.Label lblSourceFile;
		private System.Windows.Forms.Button btnBrowseSource;
		private System.Windows.Forms.TextBox txtFilenameSource;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.ToolTip ttip;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private System.Windows.Forms.TextBox txtTime;
		private System.Windows.Forms.TextBox txtTrim;
		private System.Windows.Forms.TextBox txtDim;
		private Syncfusion.Windows.Forms.Tools.TabControlAdv tabChannels;
		private Syncfusion.Windows.Forms.Tools.TabPageAdv tabDim;
		private System.Windows.Forms.PictureBox picPreviewSource;
		private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeSource;
		private Syncfusion.Windows.Forms.Tools.TabPageAdv tabMinTime;
		private Syncfusion.Windows.Forms.Tools.TabPageAdv tabNoChange;
		private Syncfusion.Windows.Forms.Tools.TabPageAdv tabTrim;
		private Syncfusion.Windows.Forms.Tools.TabPageAdv tabOnOff;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblTabFunction;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBrowseMap;
		private System.Windows.Forms.TextBox txtFilenameMap;
		private System.Windows.Forms.Button btnSaveMap;
		private System.Windows.Forms.CheckBox chkLaunch;
		private System.Windows.Forms.ImageList imlTreeIcons;
	}
}

