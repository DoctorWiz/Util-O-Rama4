
namespace UtilORama4
{
	partial class frmSparkleAC
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSparkleAC));
			Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
			this.ttip = new System.Windows.Forms.ToolTip(this.components);
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.chkAutoLaunch = new System.Windows.Forms.CheckBox();
			this.txtFileNewSeq = new System.Windows.Forms.TextBox();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.treeChannels = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.lblChannels = new System.Windows.Forms.Label();
			this.btnSaveOptions = new System.Windows.Forms.Button();
			this.btnMatchOptions = new System.Windows.Forms.Button();
			this.lblNewSequence = new System.Windows.Forms.Label();
			this.lblSelections = new System.Windows.Forms.Label();
			this.lblTreeClicks = new System.Windows.Forms.Label();
			this.btnSaveSequence = new System.Windows.Forms.Button();
			this.lblSelectionsList = new System.Windows.Forms.Label();
			this.btnBrowseSequence = new System.Windows.Forms.Button();
			this.txtSequenceFile = new System.Windows.Forms.TextBox();
			this.lblSequenceFile = new System.Windows.Forms.Label();
			this.btnBrowseSelections = new System.Windows.Forms.Button();
			this.txtSelectionsFile = new System.Windows.Forms.TextBox();
			this.lblSelectionCount = new System.Windows.Forms.Label();
			this.btnSaveSelections = new System.Windows.Forms.Button();
			this.cmdNothing = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.grpStart = new System.Windows.Forms.GroupBox();
			this.txtStartTime = new System.Windows.Forms.TextBox();
			this.lblStartTime = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.grpEnd = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeChannels)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.grpStart.SuspendLayout();
			this.grpEnd.SuspendLayout();
			this.SuspendLayout();
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlProgress,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 674);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(537, 24);
			this.staStatus.TabIndex = 62;
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
			this.pnlStatus.Size = new System.Drawing.Size(425, 19);
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
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
			// 
			// chkAutoLaunch
			// 
			this.chkAutoLaunch.AutoSize = true;
			this.chkAutoLaunch.Location = new System.Drawing.Point(153, 602);
			this.chkAutoLaunch.Name = "chkAutoLaunch";
			this.chkAutoLaunch.Size = new System.Drawing.Size(87, 17);
			this.chkAutoLaunch.TabIndex = 162;
			this.chkAutoLaunch.Text = "Auto-Launch";
			this.chkAutoLaunch.UseVisualStyleBackColor = true;
			// 
			// txtFileNewSeq
			// 
			this.txtFileNewSeq.AllowDrop = true;
			this.txtFileNewSeq.Location = new System.Drawing.Point(15, 619);
			this.txtFileNewSeq.Name = "txtFileNewSeq";
			this.txtFileNewSeq.ReadOnly = true;
			this.txtFileNewSeq.Size = new System.Drawing.Size(219, 20);
			this.txtFileNewSeq.TabIndex = 161;
			// 
			// picAboutIcon
			// 
			this.picAboutIcon.ErrorImage = null;
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.InitialImage = null;
			this.picAboutIcon.Location = new System.Drawing.Point(327, 12);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 160;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// treeChannels
			// 
			this.treeChannels.BackColor = System.Drawing.Color.White;
			treeNodeAdvStyleInfo1.CheckBoxTickThickness = 1;
			treeNodeAdvStyleInfo1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
			treeNodeAdvStyleInfo1.InteractiveCheckBox = true;
			treeNodeAdvStyleInfo1.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
			treeNodeAdvStyleInfo1.ShowCheckBox = true;
			treeNodeAdvStyleInfo1.TextColor = System.Drawing.Color.Black;
			this.treeChannels.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
			this.treeChannels.BeforeTouchSize = new System.Drawing.Size(300, 413);
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
			this.treeChannels.Location = new System.Drawing.Point(15, 123);
			this.treeChannels.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
			this.treeChannels.Name = "treeChannels";
			this.treeChannels.SelectedNodeForeColor = System.Drawing.SystemColors.HighlightText;
			this.treeChannels.ShowCheckBoxes = true;
			this.treeChannels.Size = new System.Drawing.Size(300, 413);
			this.treeChannels.TabIndex = 159;
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
			// 
			// picPreview
			// 
			this.picPreview.BackColor = System.Drawing.Color.Tan;
			this.picPreview.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picPreview.Location = new System.Drawing.Point(15, 542);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(300, 32);
			this.picPreview.TabIndex = 158;
			this.picPreview.TabStop = false;
			// 
			// lblChannels
			// 
			this.lblChannels.AllowDrop = true;
			this.lblChannels.AutoSize = true;
			this.lblChannels.Location = new System.Drawing.Point(12, 102);
			this.lblChannels.Name = "lblChannels";
			this.lblChannels.Size = new System.Drawing.Size(154, 13);
			this.lblChannels.TabIndex = 156;
			this.lblChannels.Text = "Tracks, Groups, and Channels:";
			// 
			// btnSaveOptions
			// 
			this.btnSaveOptions.AllowDrop = true;
			this.btnSaveOptions.Location = new System.Drawing.Point(377, 635);
			this.btnSaveOptions.Name = "btnSaveOptions";
			this.btnSaveOptions.Size = new System.Drawing.Size(75, 23);
			this.btnSaveOptions.TabIndex = 153;
			this.btnSaveOptions.Text = "Options...";
			this.btnSaveOptions.UseVisualStyleBackColor = true;
			this.btnSaveOptions.Visible = false;
			// 
			// btnMatchOptions
			// 
			this.btnMatchOptions.AllowDrop = true;
			this.btnMatchOptions.Location = new System.Drawing.Point(420, 638);
			this.btnMatchOptions.Name = "btnMatchOptions";
			this.btnMatchOptions.Size = new System.Drawing.Size(75, 20);
			this.btnMatchOptions.TabIndex = 152;
			this.btnMatchOptions.Text = "Options...";
			this.btnMatchOptions.UseVisualStyleBackColor = true;
			this.btnMatchOptions.Visible = false;
			// 
			// lblNewSequence
			// 
			this.lblNewSequence.AllowDrop = true;
			this.lblNewSequence.AutoSize = true;
			this.lblNewSequence.Location = new System.Drawing.Point(12, 603);
			this.lblNewSequence.Name = "lblNewSequence";
			this.lblNewSequence.Size = new System.Drawing.Size(81, 13);
			this.lblNewSequence.TabIndex = 151;
			this.lblNewSequence.Text = "New Sequence";
			// 
			// lblSelections
			// 
			this.lblSelections.AllowDrop = true;
			this.lblSelections.AutoSize = true;
			this.lblSelections.Location = new System.Drawing.Point(150, 53);
			this.lblSelections.Name = "lblSelections";
			this.lblSelections.Size = new System.Drawing.Size(59, 13);
			this.lblSelections.TabIndex = 143;
			this.lblSelections.Text = "Selections:";
			// 
			// lblTreeClicks
			// 
			this.lblTreeClicks.AllowDrop = true;
			this.lblTreeClicks.AutoSize = true;
			this.lblTreeClicks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTreeClicks.ForeColor = System.Drawing.Color.DarkRed;
			this.lblTreeClicks.Location = new System.Drawing.Point(185, 102);
			this.lblTreeClicks.Name = "lblTreeClicks";
			this.lblTreeClicks.Size = new System.Drawing.Size(13, 13);
			this.lblTreeClicks.TabIndex = 154;
			this.lblTreeClicks.Text = "0";
			// 
			// btnSaveSequence
			// 
			this.btnSaveSequence.AllowDrop = true;
			this.btnSaveSequence.Enabled = false;
			this.btnSaveSequence.Location = new System.Drawing.Point(240, 617);
			this.btnSaveSequence.Name = "btnSaveSequence";
			this.btnSaveSequence.Size = new System.Drawing.Size(75, 23);
			this.btnSaveSequence.TabIndex = 136;
			this.btnSaveSequence.Text = "Save As...";
			this.btnSaveSequence.UseVisualStyleBackColor = true;
			// 
			// lblSelectionsList
			// 
			this.lblSelectionsList.AllowDrop = true;
			this.lblSelectionsList.AutoSize = true;
			this.lblSelectionsList.Location = new System.Drawing.Point(12, 53);
			this.lblSelectionsList.Name = "lblSelectionsList";
			this.lblSelectionsList.Size = new System.Drawing.Size(75, 13);
			this.lblSelectionsList.TabIndex = 139;
			this.lblSelectionsList.Text = "Selections List";
			// 
			// btnBrowseSequence
			// 
			this.btnBrowseSequence.AllowDrop = true;
			this.btnBrowseSequence.Location = new System.Drawing.Point(240, 21);
			this.btnBrowseSequence.Name = "btnBrowseSequence";
			this.btnBrowseSequence.Size = new System.Drawing.Size(75, 20);
			this.btnBrowseSequence.TabIndex = 138;
			this.btnBrowseSequence.Text = "Browse...";
			this.btnBrowseSequence.UseVisualStyleBackColor = true;
			// 
			// txtSequenceFile
			// 
			this.txtSequenceFile.AllowDrop = true;
			this.txtSequenceFile.Location = new System.Drawing.Point(15, 22);
			this.txtSequenceFile.Name = "txtSequenceFile";
			this.txtSequenceFile.ReadOnly = true;
			this.txtSequenceFile.Size = new System.Drawing.Size(219, 20);
			this.txtSequenceFile.TabIndex = 137;
			// 
			// lblSequenceFile
			// 
			this.lblSequenceFile.AllowDrop = true;
			this.lblSequenceFile.AutoSize = true;
			this.lblSequenceFile.Location = new System.Drawing.Point(12, 5);
			this.lblSequenceFile.Name = "lblSequenceFile";
			this.lblSequenceFile.Size = new System.Drawing.Size(75, 13);
			this.lblSequenceFile.TabIndex = 150;
			this.lblSequenceFile.Text = "Sequence File";
			// 
			// btnBrowseSelections
			// 
			this.btnBrowseSelections.AllowDrop = true;
			this.btnBrowseSelections.Location = new System.Drawing.Point(240, 60);
			this.btnBrowseSelections.Name = "btnBrowseSelections";
			this.btnBrowseSelections.Size = new System.Drawing.Size(75, 20);
			this.btnBrowseSelections.TabIndex = 141;
			this.btnBrowseSelections.Text = "Load...";
			this.btnBrowseSelections.UseVisualStyleBackColor = true;
			// 
			// txtSelectionsFile
			// 
			this.txtSelectionsFile.AllowDrop = true;
			this.txtSelectionsFile.Location = new System.Drawing.Point(15, 69);
			this.txtSelectionsFile.Name = "txtSelectionsFile";
			this.txtSelectionsFile.ReadOnly = true;
			this.txtSelectionsFile.Size = new System.Drawing.Size(219, 20);
			this.txtSelectionsFile.TabIndex = 140;
			// 
			// lblSelectionCount
			// 
			this.lblSelectionCount.AllowDrop = true;
			this.lblSelectionCount.AutoSize = true;
			this.lblSelectionCount.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblSelectionCount.Location = new System.Drawing.Point(206, 53);
			this.lblSelectionCount.Name = "lblSelectionCount";
			this.lblSelectionCount.Size = new System.Drawing.Size(13, 13);
			this.lblSelectionCount.TabIndex = 144;
			this.lblSelectionCount.Text = "0";
			// 
			// btnSaveSelections
			// 
			this.btnSaveSelections.AllowDrop = true;
			this.btnSaveSelections.Enabled = false;
			this.btnSaveSelections.Location = new System.Drawing.Point(240, 79);
			this.btnSaveSelections.Name = "btnSaveSelections";
			this.btnSaveSelections.Size = new System.Drawing.Size(75, 20);
			this.btnSaveSelections.TabIndex = 142;
			this.btnSaveSelections.Text = "Save As...";
			this.btnSaveSelections.UseVisualStyleBackColor = true;
			// 
			// cmdNothing
			// 
			this.cmdNothing.Location = new System.Drawing.Point(327, 635);
			this.cmdNothing.Name = "cmdNothing";
			this.cmdNothing.Size = new System.Drawing.Size(101, 27);
			this.cmdNothing.TabIndex = 148;
			this.cmdNothing.UseVisualStyleBackColor = true;
			this.cmdNothing.Visible = false;
			// 
			// grpStart
			// 
			this.grpStart.Controls.Add(this.label8);
			this.grpStart.Controls.Add(this.label7);
			this.grpStart.Controls.Add(this.label6);
			this.grpStart.Controls.Add(this.label5);
			this.grpStart.Controls.Add(this.label4);
			this.grpStart.Controls.Add(this.textBox3);
			this.grpStart.Controls.Add(this.label3);
			this.grpStart.Controls.Add(this.textBox2);
			this.grpStart.Controls.Add(this.label2);
			this.grpStart.Controls.Add(this.textBox1);
			this.grpStart.Controls.Add(this.label1);
			this.grpStart.Controls.Add(this.txtStartTime);
			this.grpStart.Controls.Add(this.lblStartTime);
			this.grpStart.Location = new System.Drawing.Point(331, 158);
			this.grpStart.Name = "grpStart";
			this.grpStart.Size = new System.Drawing.Size(133, 205);
			this.grpStart.TabIndex = 163;
			this.grpStart.TabStop = false;
			this.grpStart.Text = "Start";
			// 
			// txtStartTime
			// 
			this.txtStartTime.AllowDrop = true;
			this.txtStartTime.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtStartTime.Location = new System.Drawing.Point(7, 34);
			this.txtStartTime.Name = "txtStartTime";
			this.txtStartTime.ReadOnly = true;
			this.txtStartTime.Size = new System.Drawing.Size(64, 20);
			this.txtStartTime.TabIndex = 163;
			this.txtStartTime.Text = "0:00.00";
			this.txtStartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblStartTime
			// 
			this.lblStartTime.AllowDrop = true;
			this.lblStartTime.AutoSize = true;
			this.lblStartTime.Location = new System.Drawing.Point(4, 18);
			this.lblStartTime.Name = "lblStartTime";
			this.lblStartTime.Size = new System.Drawing.Size(58, 13);
			this.lblStartTime.TabIndex = 162;
			this.lblStartTime.Text = "Start Time:";
			// 
			// textBox1
			// 
			this.textBox1.AllowDrop = true;
			this.textBox1.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(7, 73);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(64, 20);
			this.textBox1.TabIndex = 165;
			this.textBox1.Text = "05";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.AllowDrop = true;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 57);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 13);
			this.label1.TabIndex = 164;
			this.label1.Text = "Minimum On Time:";
			// 
			// textBox2
			// 
			this.textBox2.AllowDrop = true;
			this.textBox2.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox2.Location = new System.Drawing.Point(7, 123);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(64, 20);
			this.textBox2.TabIndex = 167;
			this.textBox2.Text = "10";
			this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AllowDrop = true;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 107);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 13);
			this.label2.TabIndex = 166;
			this.label2.Text = "Percent Time On:";
			// 
			// textBox3
			// 
			this.textBox3.AllowDrop = true;
			this.textBox3.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox3.Location = new System.Drawing.Point(7, 171);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(64, 20);
			this.textBox3.TabIndex = 169;
			this.textBox3.Text = "10";
			this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.AllowDrop = true;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 155);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(94, 13);
			this.label3.TabIndex = 168;
			this.label3.Text = "Minimum Off Time:";
			// 
			// label4
			// 
			this.label4.AllowDrop = true;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(76, 75);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(18, 13);
			this.label4.TabIndex = 170;
			this.label4.Text = "cs";
			// 
			// label5
			// 
			this.label5.AllowDrop = true;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(77, 125);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(15, 13);
			this.label5.TabIndex = 171;
			this.label5.Text = "%";
			// 
			// label6
			// 
			this.label6.AllowDrop = true;
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(77, 173);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(18, 13);
			this.label6.TabIndex = 172;
			this.label6.Text = "cs";
			// 
			// label7
			// 
			this.label7.AllowDrop = true;
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(73, 129);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(18, 13);
			this.label7.TabIndex = 173;
			this.label7.Text = "cs";
			// 
			// label8
			// 
			this.label8.AllowDrop = true;
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(76, 36);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(51, 13);
			this.label8.TabIndex = 174;
			this.label8.Text = "mm:ss.cc";
			// 
			// grpEnd
			// 
			this.grpEnd.Controls.Add(this.label9);
			this.grpEnd.Controls.Add(this.label10);
			this.grpEnd.Controls.Add(this.label11);
			this.grpEnd.Controls.Add(this.label12);
			this.grpEnd.Controls.Add(this.label13);
			this.grpEnd.Controls.Add(this.textBox4);
			this.grpEnd.Controls.Add(this.label14);
			this.grpEnd.Controls.Add(this.textBox5);
			this.grpEnd.Controls.Add(this.label15);
			this.grpEnd.Controls.Add(this.textBox6);
			this.grpEnd.Controls.Add(this.label16);
			this.grpEnd.Controls.Add(this.textBox7);
			this.grpEnd.Controls.Add(this.label17);
			this.grpEnd.Location = new System.Drawing.Point(331, 369);
			this.grpEnd.Name = "grpEnd";
			this.grpEnd.Size = new System.Drawing.Size(133, 205);
			this.grpEnd.TabIndex = 164;
			this.grpEnd.TabStop = false;
			this.grpEnd.Text = "End";
			// 
			// label9
			// 
			this.label9.AllowDrop = true;
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(76, 36);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(51, 13);
			this.label9.TabIndex = 174;
			this.label9.Text = "mm:ss.cc";
			// 
			// label10
			// 
			this.label10.AllowDrop = true;
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(73, 129);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(18, 13);
			this.label10.TabIndex = 173;
			this.label10.Text = "cs";
			// 
			// label11
			// 
			this.label11.AllowDrop = true;
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(77, 173);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(18, 13);
			this.label11.TabIndex = 172;
			this.label11.Text = "cs";
			// 
			// label12
			// 
			this.label12.AllowDrop = true;
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(77, 125);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(15, 13);
			this.label12.TabIndex = 171;
			this.label12.Text = "%";
			// 
			// label13
			// 
			this.label13.AllowDrop = true;
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(76, 75);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(18, 13);
			this.label13.TabIndex = 170;
			this.label13.Text = "cs";
			// 
			// textBox4
			// 
			this.textBox4.AllowDrop = true;
			this.textBox4.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox4.Location = new System.Drawing.Point(7, 171);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(64, 20);
			this.textBox4.TabIndex = 169;
			this.textBox4.Text = "10";
			this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label14
			// 
			this.label14.AllowDrop = true;
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(4, 155);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(94, 13);
			this.label14.TabIndex = 168;
			this.label14.Text = "Minimum Off Time:";
			// 
			// textBox5
			// 
			this.textBox5.AllowDrop = true;
			this.textBox5.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox5.Location = new System.Drawing.Point(7, 123);
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new System.Drawing.Size(64, 20);
			this.textBox5.TabIndex = 167;
			this.textBox5.Text = "10";
			this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label15
			// 
			this.label15.AllowDrop = true;
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(4, 107);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(90, 13);
			this.label15.TabIndex = 166;
			this.label15.Text = "Percent Time On:";
			// 
			// textBox6
			// 
			this.textBox6.AllowDrop = true;
			this.textBox6.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox6.Location = new System.Drawing.Point(7, 73);
			this.textBox6.Name = "textBox6";
			this.textBox6.ReadOnly = true;
			this.textBox6.Size = new System.Drawing.Size(64, 20);
			this.textBox6.TabIndex = 165;
			this.textBox6.Text = "05";
			this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label16
			// 
			this.label16.AllowDrop = true;
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(4, 57);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(94, 13);
			this.label16.TabIndex = 164;
			this.label16.Text = "Minimum On Time:";
			// 
			// textBox7
			// 
			this.textBox7.AllowDrop = true;
			this.textBox7.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox7.Location = new System.Drawing.Point(7, 34);
			this.textBox7.Name = "textBox7";
			this.textBox7.ReadOnly = true;
			this.textBox7.Size = new System.Drawing.Size(64, 20);
			this.textBox7.TabIndex = 163;
			this.textBox7.Text = "0:00.00";
			this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label17
			// 
			this.label17.AllowDrop = true;
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(4, 18);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(55, 13);
			this.label17.TabIndex = 162;
			this.label17.Text = "End Time:";
			// 
			// frmSparkleAC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(537, 698);
			this.Controls.Add(this.grpEnd);
			this.Controls.Add(this.grpStart);
			this.Controls.Add(this.chkAutoLaunch);
			this.Controls.Add(this.txtFileNewSeq);
			this.Controls.Add(this.picAboutIcon);
			this.Controls.Add(this.treeChannels);
			this.Controls.Add(this.picPreview);
			this.Controls.Add(this.lblChannels);
			this.Controls.Add(this.btnSaveOptions);
			this.Controls.Add(this.btnMatchOptions);
			this.Controls.Add(this.lblNewSequence);
			this.Controls.Add(this.lblSelections);
			this.Controls.Add(this.lblTreeClicks);
			this.Controls.Add(this.btnSaveSequence);
			this.Controls.Add(this.lblSelectionsList);
			this.Controls.Add(this.btnBrowseSequence);
			this.Controls.Add(this.txtSequenceFile);
			this.Controls.Add(this.lblSequenceFile);
			this.Controls.Add(this.btnBrowseSelections);
			this.Controls.Add(this.txtSelectionsFile);
			this.Controls.Add(this.lblSelectionCount);
			this.Controls.Add(this.btnSaveSelections);
			this.Controls.Add(this.cmdNothing);
			this.Controls.Add(this.staStatus);
			this.Name = "frmSparkleAC";
			this.Text = "Sparkle-O-Rama";
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeChannels)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			this.grpStart.ResumeLayout(false);
			this.grpStart.PerformLayout();
			this.grpEnd.ResumeLayout(false);
			this.grpEnd.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolTip ttip;
		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripProgressBar pnlProgress;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
		private System.Windows.Forms.CheckBox chkAutoLaunch;
		private System.Windows.Forms.TextBox txtFileNewSeq;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeChannels;
		private System.Windows.Forms.PictureBox picPreview;
		private System.Windows.Forms.Label lblChannels;
		private System.Windows.Forms.Button btnSaveOptions;
		private System.Windows.Forms.Button btnMatchOptions;
		private System.Windows.Forms.Label lblNewSequence;
		private System.Windows.Forms.Label lblSelections;
		private System.Windows.Forms.Label lblTreeClicks;
		private System.Windows.Forms.Button btnSaveSequence;
		private System.Windows.Forms.Label lblSelectionsList;
		private System.Windows.Forms.Button btnBrowseSequence;
		private System.Windows.Forms.TextBox txtSequenceFile;
		private System.Windows.Forms.Label lblSequenceFile;
		private System.Windows.Forms.Button btnBrowseSelections;
		private System.Windows.Forms.TextBox txtSelectionsFile;
		private System.Windows.Forms.Label lblSelectionCount;
		private System.Windows.Forms.Button btnSaveSelections;
		private System.Windows.Forms.Button cmdNothing;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.GroupBox grpStart;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtStartTime;
		private System.Windows.Forms.Label lblStartTime;
		private System.Windows.Forms.GroupBox grpEnd;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.Label label17;
	}
}