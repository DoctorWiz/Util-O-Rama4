namespace TuneORama
{
	partial class frmTune
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTune));
			this.ttip = new System.Windows.Forms.ToolTip(this.components);
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOpenAudio = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveSequenceAs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileDivider1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOptionsAnnotator = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOptionsVamp = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOptionsDivider1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuOptionsSaveFormat = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileDivider2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuGenerate = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTimingMarks = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTranscription = new System.Windows.Forms.ToolStripMenuItem();
			this.grpGrids = new System.Windows.Forms.GroupBox();
			this.btnGridSettings = new System.Windows.Forms.Button();
			this.lblStep3 = new System.Windows.Forms.Label();
			this.chkBeatsGrid = new System.Windows.Forms.CheckBox();
			this.chkNoteOnsets = new System.Windows.Forms.CheckBox();
			this.grpTracks = new System.Windows.Forms.GroupBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.chkFlux = new System.Windows.Forms.CheckBox();
			this.chkPitch = new System.Windows.Forms.CheckBox();
			this.chkCords = new System.Windows.Forms.CheckBox();
			this.chkVocals = new System.Windows.Forms.CheckBox();
			this.chkSegments = new System.Windows.Forms.CheckBox();
			this.chkChromagram = new System.Windows.Forms.CheckBox();
			this.btnTrackSettings = new System.Windows.Forms.Button();
			this.lblStep4 = new System.Windows.Forms.Label();
			this.chkConstQ = new System.Windows.Forms.CheckBox();
			this.chkBeatChannels = new System.Windows.Forms.CheckBox();
			this.chkPolyphonic = new System.Windows.Forms.CheckBox();
			this.grpStart = new System.Windows.Forms.GroupBox();
			this.lblStep1 = new System.Windows.Forms.Label();
			this.pnlSwitchStart = new System.Windows.Forms.Panel();
			this.picHoleStart = new System.Windows.Forms.PictureBox();
			this.picSliderStart = new System.Windows.Forms.PictureBox();
			this.lblDontUseConfig = new System.Windows.Forms.Label();
			this.lblUseConfig = new System.Windows.Forms.Label();
			this.btnBrowseSequence = new System.Windows.Forms.Button();
			this.txtFileCurrent = new System.Windows.Forms.TextBox();
			this.grpAudio = new System.Windows.Forms.GroupBox();
			this.btnBrowseAudio = new System.Windows.Forms.Button();
			this.txtFileAudio = new System.Windows.Forms.TextBox();
			this.lblFileAudio = new System.Windows.Forms.Label();
			this.lblStep2 = new System.Windows.Forms.Label();
			this.grpSequence = new System.Windows.Forms.GroupBox();
			this.chkAutoLaunch = new System.Windows.Forms.CheckBox();
			this.btnSaveOptions = new System.Windows.Forms.Button();
			this.chkAutoSave = new System.Windows.Forms.CheckBox();
			this.lblStep6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnSaveSequence = new System.Windows.Forms.Button();
			this.txtSaveName = new System.Windows.Forms.TextBox();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.treeMaster = new System.Windows.Forms.TreeView();
			this.pnlVamping = new System.Windows.Forms.Panel();
			this.lblSongName = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.lblStep5 = new System.Windows.Forms.Label();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.staStatus.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.grpGrids.SuspendLayout();
			this.grpTracks.SuspendLayout();
			this.grpStart.SuspendLayout();
			this.pnlSwitchStart.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picHoleStart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picSliderStart)).BeginInit();
			this.grpAudio.SuspendLayout();
			this.grpSequence.SuspendLayout();
			this.pnlVamping.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
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
			this.staStatus.Location = new System.Drawing.Point(0, 615);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(695, 24);
			this.staStatus.TabIndex = 62;
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
			this.pnlStatus.Size = new System.Drawing.Size(583, 19);
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
			// dlgOpenFile
			// 
			this.dlgOpenFile.FileName = "openFileDialog1";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuGenerate});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(359, 24);
			this.menuStrip1.TabIndex = 110;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenAudio,
            this.mnuSaveSequenceAs,
            this.mnuFileDivider1,
            this.toolStripMenuItem2,
            this.mnuFileDivider2,
            this.mnuExit});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(37, 20);
			this.mnuFile.Text = "&File";
			// 
			// mnuOpenAudio
			// 
			this.mnuOpenAudio.Name = "mnuOpenAudio";
			this.mnuOpenAudio.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mnuOpenAudio.Size = new System.Drawing.Size(217, 22);
			this.mnuOpenAudio.Text = "&Open Audio...";
			// 
			// mnuSaveSequenceAs
			// 
			this.mnuSaveSequenceAs.Name = "mnuSaveSequenceAs";
			this.mnuSaveSequenceAs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mnuSaveSequenceAs.Size = new System.Drawing.Size(217, 22);
			this.mnuSaveSequenceAs.Text = "Save Sequence &As...";
			// 
			// mnuFileDivider1
			// 
			this.mnuFileDivider1.Name = "mnuFileDivider1";
			this.mnuFileDivider1.Size = new System.Drawing.Size(214, 6);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptionsAnnotator,
            this.mnuOptionsVamp,
            this.mnuOptionsDivider1,
            this.mnuOptionsSaveFormat});
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(217, 22);
			this.toolStripMenuItem2.Text = "O&ptions";
			// 
			// mnuOptionsAnnotator
			// 
			this.mnuOptionsAnnotator.Name = "mnuOptionsAnnotator";
			this.mnuOptionsAnnotator.Size = new System.Drawing.Size(169, 22);
			this.mnuOptionsAnnotator.Text = "Sonic &Annotator...";
			// 
			// mnuOptionsVamp
			// 
			this.mnuOptionsVamp.Name = "mnuOptionsVamp";
			this.mnuOptionsVamp.Size = new System.Drawing.Size(169, 22);
			this.mnuOptionsVamp.Text = "&Vamp Plugins...";
			// 
			// mnuOptionsDivider1
			// 
			this.mnuOptionsDivider1.Name = "mnuOptionsDivider1";
			this.mnuOptionsDivider1.Size = new System.Drawing.Size(166, 6);
			// 
			// mnuOptionsSaveFormat
			// 
			this.mnuOptionsSaveFormat.Name = "mnuOptionsSaveFormat";
			this.mnuOptionsSaveFormat.Size = new System.Drawing.Size(169, 22);
			this.mnuOptionsSaveFormat.Text = "Save &Format...";
			// 
			// mnuFileDivider2
			// 
			this.mnuFileDivider2.Name = "mnuFileDivider2";
			this.mnuFileDivider2.Size = new System.Drawing.Size(214, 6);
			// 
			// mnuExit
			// 
			this.mnuExit.Name = "mnuExit";
			this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.mnuExit.Size = new System.Drawing.Size(217, 22);
			this.mnuExit.Text = "E&xit";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// mnuGenerate
			// 
			this.mnuGenerate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTimingMarks,
            this.mnuTranscription});
			this.mnuGenerate.Name = "mnuGenerate";
			this.mnuGenerate.Size = new System.Drawing.Size(66, 20);
			this.mnuGenerate.Text = "&Generate";
			// 
			// mnuTimingMarks
			// 
			this.mnuTimingMarks.Name = "mnuTimingMarks";
			this.mnuTimingMarks.Size = new System.Drawing.Size(206, 22);
			this.mnuTimingMarks.Text = "&Timing Marks";
			// 
			// mnuTranscription
			// 
			this.mnuTranscription.Name = "mnuTranscription";
			this.mnuTranscription.Size = new System.Drawing.Size(206, 22);
			this.mnuTranscription.Text = "&Polyphonic Transcription";
			// 
			// grpGrids
			// 
			this.grpGrids.Controls.Add(this.btnGridSettings);
			this.grpGrids.Controls.Add(this.lblStep3);
			this.grpGrids.Controls.Add(this.chkBeatsGrid);
			this.grpGrids.Controls.Add(this.chkNoteOnsets);
			this.grpGrids.Enabled = false;
			this.grpGrids.Location = new System.Drawing.Point(12, 189);
			this.grpGrids.Name = "grpGrids";
			this.grpGrids.Size = new System.Drawing.Size(334, 62);
			this.grpGrids.TabIndex = 112;
			this.grpGrids.TabStop = false;
			this.grpGrids.Text = "      Select Timing Grids ";
			// 
			// btnGridSettings
			// 
			this.btnGridSettings.AllowDrop = true;
			this.btnGridSettings.Location = new System.Drawing.Point(303, 39);
			this.btnGridSettings.Name = "btnGridSettings";
			this.btnGridSettings.Size = new System.Drawing.Size(75, 23);
			this.btnGridSettings.TabIndex = 121;
			this.btnGridSettings.Text = "Settings...";
			this.btnGridSettings.UseVisualStyleBackColor = true;
			this.btnGridSettings.Visible = false;
			this.btnGridSettings.Click += new System.EventHandler(this.btnGridSettings_Click);
			// 
			// lblStep3
			// 
			this.lblStep3.AutoSize = true;
			this.lblStep3.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStep3.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep3.Location = new System.Drawing.Point(6, -5);
			this.lblStep3.Name = "lblStep3";
			this.lblStep3.Size = new System.Drawing.Size(21, 24);
			this.lblStep3.TabIndex = 120;
			this.lblStep3.Text = "3";
			// 
			// chkBeatsGrid
			// 
			this.chkBeatsGrid.AutoSize = true;
			this.chkBeatsGrid.Checked = true;
			this.chkBeatsGrid.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBeatsGrid.Location = new System.Drawing.Point(19, 19);
			this.chkBeatsGrid.Name = "chkBeatsGrid";
			this.chkBeatsGrid.Size = new System.Drawing.Size(53, 17);
			this.chkBeatsGrid.TabIndex = 1;
			this.chkBeatsGrid.Text = "Beats";
			this.chkBeatsGrid.UseVisualStyleBackColor = true;
			this.chkBeatsGrid.CheckedChanged += new System.EventHandler(this.chkBeatsGrid_CheckedChanged);
			// 
			// chkNoteOnsets
			// 
			this.chkNoteOnsets.AutoSize = true;
			this.chkNoteOnsets.Checked = true;
			this.chkNoteOnsets.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkNoteOnsets.Location = new System.Drawing.Point(19, 39);
			this.chkNoteOnsets.Name = "chkNoteOnsets";
			this.chkNoteOnsets.Size = new System.Drawing.Size(85, 17);
			this.chkNoteOnsets.TabIndex = 0;
			this.chkNoteOnsets.Text = "Note Onsets";
			this.chkNoteOnsets.UseVisualStyleBackColor = true;
			this.chkNoteOnsets.CheckedChanged += new System.EventHandler(this.chkNoteOnsets_CheckedChanged);
			// 
			// grpTracks
			// 
			this.grpTracks.Controls.Add(this.checkBox3);
			this.grpTracks.Controls.Add(this.checkBox2);
			this.grpTracks.Controls.Add(this.checkBox1);
			this.grpTracks.Controls.Add(this.chkFlux);
			this.grpTracks.Controls.Add(this.chkPitch);
			this.grpTracks.Controls.Add(this.chkCords);
			this.grpTracks.Controls.Add(this.chkVocals);
			this.grpTracks.Controls.Add(this.chkSegments);
			this.grpTracks.Controls.Add(this.chkChromagram);
			this.grpTracks.Controls.Add(this.btnTrackSettings);
			this.grpTracks.Controls.Add(this.lblStep4);
			this.grpTracks.Controls.Add(this.chkConstQ);
			this.grpTracks.Controls.Add(this.chkBeatChannels);
			this.grpTracks.Controls.Add(this.chkPolyphonic);
			this.grpTracks.Enabled = false;
			this.grpTracks.Location = new System.Drawing.Point(12, 257);
			this.grpTracks.Name = "grpTracks";
			this.grpTracks.Size = new System.Drawing.Size(334, 162);
			this.grpTracks.TabIndex = 113;
			this.grpTracks.TabStop = false;
			this.grpTracks.Text = "      Select Channels";
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Checked = true;
			this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox3.Location = new System.Drawing.Point(206, 138);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(65, 17);
			this.checkBox3.TabIndex = 131;
			this.checkBox3.Text = "Foooo...";
			this.checkBox3.UseVisualStyleBackColor = true;
			this.checkBox3.Visible = false;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Checked = true;
			this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox2.Location = new System.Drawing.Point(206, 115);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(65, 17);
			this.checkBox2.TabIndex = 130;
			this.checkBox2.Text = "Foooo...";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.Visible = false;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(206, 92);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(65, 17);
			this.checkBox1.TabIndex = 129;
			this.checkBox1.Text = "Foooo...";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.Visible = false;
			// 
			// chkFlux
			// 
			this.chkFlux.AutoSize = true;
			this.chkFlux.Enabled = false;
			this.chkFlux.Location = new System.Drawing.Point(206, 23);
			this.chkFlux.Name = "chkFlux";
			this.chkFlux.Size = new System.Drawing.Size(45, 17);
			this.chkFlux.TabIndex = 128;
			this.chkFlux.Text = "Flux";
			this.chkFlux.UseVisualStyleBackColor = true;
			// 
			// chkPitch
			// 
			this.chkPitch.AutoSize = true;
			this.chkPitch.Checked = true;
			this.chkPitch.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPitch.Location = new System.Drawing.Point(206, 46);
			this.chkPitch.Name = "chkPitch";
			this.chkPitch.Size = new System.Drawing.Size(80, 17);
			this.chkPitch.TabIndex = 127;
			this.chkPitch.Text = "Pitch && Key";
			this.chkPitch.UseVisualStyleBackColor = true;
			this.chkPitch.CheckedChanged += new System.EventHandler(this.chkPitch_CheckedChanged);
			// 
			// chkCords
			// 
			this.chkCords.AutoSize = true;
			this.chkCords.Enabled = false;
			this.chkCords.Location = new System.Drawing.Point(206, 69);
			this.chkCords.Name = "chkCords";
			this.chkCords.Size = new System.Drawing.Size(59, 17);
			this.chkCords.TabIndex = 126;
			this.chkCords.Text = "Chords";
			this.chkCords.UseVisualStyleBackColor = true;
			// 
			// chkVocals
			// 
			this.chkVocals.AutoSize = true;
			this.chkVocals.Enabled = false;
			this.chkVocals.Location = new System.Drawing.Point(19, 138);
			this.chkVocals.Name = "chkVocals";
			this.chkVocals.Size = new System.Drawing.Size(58, 17);
			this.chkVocals.TabIndex = 125;
			this.chkVocals.Text = "Vocals";
			this.chkVocals.UseVisualStyleBackColor = true;
			this.chkVocals.CheckedChanged += new System.EventHandler(this.chkVocals_CheckedChanged);
			// 
			// chkSegments
			// 
			this.chkSegments.AutoSize = true;
			this.chkSegments.Enabled = false;
			this.chkSegments.Location = new System.Drawing.Point(19, 115);
			this.chkSegments.Name = "chkSegments";
			this.chkSegments.Size = new System.Drawing.Size(73, 17);
			this.chkSegments.TabIndex = 124;
			this.chkSegments.Text = "Segments";
			this.chkSegments.UseVisualStyleBackColor = true;
			this.chkSegments.CheckedChanged += new System.EventHandler(this.chkSegments_CheckedChanged);
			// 
			// chkChromagram
			// 
			this.chkChromagram.AutoSize = true;
			this.chkChromagram.Checked = true;
			this.chkChromagram.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkChromagram.Location = new System.Drawing.Point(19, 92);
			this.chkChromagram.Name = "chkChromagram";
			this.chkChromagram.Size = new System.Drawing.Size(85, 17);
			this.chkChromagram.TabIndex = 123;
			this.chkChromagram.Text = "Chromagram";
			this.chkChromagram.UseVisualStyleBackColor = true;
			this.chkChromagram.CheckedChanged += new System.EventHandler(this.chkChromagram_CheckedChanged);
			// 
			// btnTrackSettings
			// 
			this.btnTrackSettings.AllowDrop = true;
			this.btnTrackSettings.Location = new System.Drawing.Point(284, 139);
			this.btnTrackSettings.Name = "btnTrackSettings";
			this.btnTrackSettings.Size = new System.Drawing.Size(75, 23);
			this.btnTrackSettings.TabIndex = 122;
			this.btnTrackSettings.Text = "Settings...";
			this.btnTrackSettings.UseVisualStyleBackColor = true;
			this.btnTrackSettings.Visible = false;
			this.btnTrackSettings.Click += new System.EventHandler(this.btnTrackSettings_Click);
			// 
			// lblStep4
			// 
			this.lblStep4.AutoSize = true;
			this.lblStep4.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStep4.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep4.Location = new System.Drawing.Point(6, -5);
			this.lblStep4.Name = "lblStep4";
			this.lblStep4.Size = new System.Drawing.Size(21, 24);
			this.lblStep4.TabIndex = 121;
			this.lblStep4.Text = "4";
			// 
			// chkConstQ
			// 
			this.chkConstQ.AutoSize = true;
			this.chkConstQ.Checked = true;
			this.chkConstQ.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkConstQ.Location = new System.Drawing.Point(19, 69);
			this.chkConstQ.Name = "chkConstQ";
			this.chkConstQ.Size = new System.Drawing.Size(142, 17);
			this.chkConstQ.TabIndex = 6;
			this.chkConstQ.Text = "Constant Q Spectrogram";
			this.chkConstQ.UseVisualStyleBackColor = true;
			this.chkConstQ.CheckedChanged += new System.EventHandler(this.chkSpectro_CheckedChanged);
			// 
			// chkBeatChannels
			// 
			this.chkBeatChannels.AutoSize = true;
			this.chkBeatChannels.Checked = true;
			this.chkBeatChannels.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBeatChannels.Location = new System.Drawing.Point(19, 23);
			this.chkBeatChannels.Name = "chkBeatChannels";
			this.chkBeatChannels.Size = new System.Drawing.Size(86, 17);
			this.chkBeatChannels.TabIndex = 1;
			this.chkBeatChannels.Text = "Bars && Beats";
			this.chkBeatChannels.UseVisualStyleBackColor = true;
			this.chkBeatChannels.CheckedChanged += new System.EventHandler(this.chkBeatsTrack_CheckedChanged);
			// 
			// chkPolyphonic
			// 
			this.chkPolyphonic.AutoSize = true;
			this.chkPolyphonic.Checked = true;
			this.chkPolyphonic.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPolyphonic.Location = new System.Drawing.Point(19, 46);
			this.chkPolyphonic.Name = "chkPolyphonic";
			this.chkPolyphonic.Size = new System.Drawing.Size(142, 17);
			this.chkPolyphonic.TabIndex = 0;
			this.chkPolyphonic.Text = "Polyphonic Transcription";
			this.chkPolyphonic.UseVisualStyleBackColor = true;
			this.chkPolyphonic.CheckedChanged += new System.EventHandler(this.chkPoly_CheckedChanged);
			// 
			// grpStart
			// 
			this.grpStart.Controls.Add(this.lblStep1);
			this.grpStart.Controls.Add(this.pnlSwitchStart);
			this.grpStart.Controls.Add(this.lblDontUseConfig);
			this.grpStart.Controls.Add(this.lblUseConfig);
			this.grpStart.Controls.Add(this.btnBrowseSequence);
			this.grpStart.Controls.Add(this.txtFileCurrent);
			this.grpStart.Location = new System.Drawing.Point(12, 39);
			this.grpStart.Name = "grpStart";
			this.grpStart.Size = new System.Drawing.Size(334, 72);
			this.grpStart.TabIndex = 117;
			this.grpStart.TabStop = false;
			this.grpStart.Text = "      Start From ";
			// 
			// lblStep1
			// 
			this.lblStep1.AutoSize = true;
			this.lblStep1.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStep1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep1.Location = new System.Drawing.Point(6, -5);
			this.lblStep1.Name = "lblStep1";
			this.lblStep1.Size = new System.Drawing.Size(21, 24);
			this.lblStep1.TabIndex = 121;
			this.lblStep1.Text = "1";
			// 
			// pnlSwitchStart
			// 
			this.pnlSwitchStart.Controls.Add(this.picHoleStart);
			this.pnlSwitchStart.Controls.Add(this.picSliderStart);
			this.pnlSwitchStart.Location = new System.Drawing.Point(190, 18);
			this.pnlSwitchStart.Name = "pnlSwitchStart";
			this.pnlSwitchStart.Size = new System.Drawing.Size(32, 16);
			this.pnlSwitchStart.TabIndex = 120;
			// 
			// picHoleStart
			// 
			this.picHoleStart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picHoleStart.BackgroundImage")));
			this.picHoleStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.picHoleStart.Location = new System.Drawing.Point(16, 0);
			this.picHoleStart.Name = "picHoleStart";
			this.picHoleStart.Size = new System.Drawing.Size(16, 16);
			this.picHoleStart.TabIndex = 1;
			this.picHoleStart.TabStop = false;
			this.picHoleStart.Click += new System.EventHandler(this.picHoleStart_Click);
			// 
			// picSliderStart
			// 
			this.picSliderStart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picSliderStart.BackgroundImage")));
			this.picSliderStart.Location = new System.Drawing.Point(0, 0);
			this.picSliderStart.Name = "picSliderStart";
			this.picSliderStart.Size = new System.Drawing.Size(16, 16);
			this.picSliderStart.TabIndex = 0;
			this.picSliderStart.TabStop = false;
			// 
			// lblDontUseConfig
			// 
			this.lblDontUseConfig.AutoSize = true;
			this.lblDontUseConfig.Location = new System.Drawing.Point(230, 21);
			this.lblDontUseConfig.Name = "lblDontUseConfig";
			this.lblDontUseConfig.Size = new System.Drawing.Size(93, 13);
			this.lblDontUseConfig.TabIndex = 119;
			this.lblDontUseConfig.Text = "Create New Blank";
			this.lblDontUseConfig.Click += new System.EventHandler(this.lblDontUseConfig_Click);
			// 
			// lblUseConfig
			// 
			this.lblUseConfig.AutoSize = true;
			this.lblUseConfig.Location = new System.Drawing.Point(16, 21);
			this.lblUseConfig.Name = "lblUseConfig";
			this.lblUseConfig.Size = new System.Drawing.Size(170, 13);
			this.lblUseConfig.TabIndex = 118;
			this.lblUseConfig.Text = "Load Sequence or Channel Config";
			this.lblUseConfig.Click += new System.EventHandler(this.lblUseConfig_Click);
			// 
			// btnBrowseSequence
			// 
			this.btnBrowseSequence.AllowDrop = true;
			this.btnBrowseSequence.Location = new System.Drawing.Point(244, 40);
			this.btnBrowseSequence.Name = "btnBrowseSequence";
			this.btnBrowseSequence.Size = new System.Drawing.Size(75, 20);
			this.btnBrowseSequence.TabIndex = 117;
			this.btnBrowseSequence.Text = "Browse...";
			this.btnBrowseSequence.UseVisualStyleBackColor = true;
			this.btnBrowseSequence.Click += new System.EventHandler(this.btnBrowseSequence_Click);
			// 
			// txtFileCurrent
			// 
			this.txtFileCurrent.AllowDrop = true;
			this.txtFileCurrent.Location = new System.Drawing.Point(19, 40);
			this.txtFileCurrent.Name = "txtFileCurrent";
			this.txtFileCurrent.ReadOnly = true;
			this.txtFileCurrent.Size = new System.Drawing.Size(219, 20);
			this.txtFileCurrent.TabIndex = 116;
			// 
			// grpAudio
			// 
			this.grpAudio.Controls.Add(this.btnBrowseAudio);
			this.grpAudio.Controls.Add(this.txtFileAudio);
			this.grpAudio.Controls.Add(this.lblFileAudio);
			this.grpAudio.Controls.Add(this.lblStep2);
			this.grpAudio.Enabled = false;
			this.grpAudio.Location = new System.Drawing.Point(12, 117);
			this.grpAudio.Name = "grpAudio";
			this.grpAudio.Size = new System.Drawing.Size(334, 66);
			this.grpAudio.TabIndex = 122;
			this.grpAudio.TabStop = false;
			this.grpAudio.Text = "      Select Audio File";
			// 
			// btnBrowseAudio
			// 
			this.btnBrowseAudio.AllowDrop = true;
			this.btnBrowseAudio.Location = new System.Drawing.Point(244, 35);
			this.btnBrowseAudio.Name = "btnBrowseAudio";
			this.btnBrowseAudio.Size = new System.Drawing.Size(75, 20);
			this.btnBrowseAudio.TabIndex = 124;
			this.btnBrowseAudio.Text = "Browse...";
			this.btnBrowseAudio.UseVisualStyleBackColor = true;
			this.btnBrowseAudio.Click += new System.EventHandler(this.btnBrowseAudio_Click);
			// 
			// txtFileAudio
			// 
			this.txtFileAudio.AllowDrop = true;
			this.txtFileAudio.Location = new System.Drawing.Point(19, 35);
			this.txtFileAudio.Name = "txtFileAudio";
			this.txtFileAudio.ReadOnly = true;
			this.txtFileAudio.Size = new System.Drawing.Size(219, 20);
			this.txtFileAudio.TabIndex = 123;
			// 
			// lblFileAudio
			// 
			this.lblFileAudio.AllowDrop = true;
			this.lblFileAudio.AutoSize = true;
			this.lblFileAudio.Location = new System.Drawing.Point(16, 19);
			this.lblFileAudio.Name = "lblFileAudio";
			this.lblFileAudio.Size = new System.Drawing.Size(53, 13);
			this.lblFileAudio.TabIndex = 125;
			this.lblFileAudio.Text = "Audio File";
			// 
			// lblStep2
			// 
			this.lblStep2.AutoSize = true;
			this.lblStep2.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStep2.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep2.Location = new System.Drawing.Point(6, -5);
			this.lblStep2.Name = "lblStep2";
			this.lblStep2.Size = new System.Drawing.Size(21, 24);
			this.lblStep2.TabIndex = 122;
			this.lblStep2.Text = "2";
			// 
			// grpSequence
			// 
			this.grpSequence.Controls.Add(this.chkAutoLaunch);
			this.grpSequence.Controls.Add(this.btnSaveOptions);
			this.grpSequence.Controls.Add(this.chkAutoSave);
			this.grpSequence.Controls.Add(this.lblStep6);
			this.grpSequence.Controls.Add(this.label4);
			this.grpSequence.Controls.Add(this.btnSaveSequence);
			this.grpSequence.Controls.Add(this.txtSaveName);
			this.grpSequence.Enabled = false;
			this.grpSequence.Location = new System.Drawing.Point(12, 499);
			this.grpSequence.Name = "grpSequence";
			this.grpSequence.Size = new System.Drawing.Size(334, 111);
			this.grpSequence.TabIndex = 123;
			this.grpSequence.TabStop = false;
			this.grpSequence.Text = "      Save Sequence";
			// 
			// chkAutoLaunch
			// 
			this.chkAutoLaunch.AutoSize = true;
			this.chkAutoLaunch.Checked = true;
			this.chkAutoLaunch.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoLaunch.Location = new System.Drawing.Point(19, 88);
			this.chkAutoLaunch.Name = "chkAutoLaunch";
			this.chkAutoLaunch.Size = new System.Drawing.Size(145, 17);
			this.chkAutoLaunch.TabIndex = 130;
			this.chkAutoLaunch.Text = "Open in Sequence Editor";
			this.chkAutoLaunch.UseVisualStyleBackColor = true;
			this.chkAutoLaunch.CheckedChanged += new System.EventHandler(this.chkAutoLaunch_CheckedChanged);
			// 
			// btnSaveOptions
			// 
			this.btnSaveOptions.AllowDrop = true;
			this.btnSaveOptions.Location = new System.Drawing.Point(244, 67);
			this.btnSaveOptions.Name = "btnSaveOptions";
			this.btnSaveOptions.Size = new System.Drawing.Size(75, 23);
			this.btnSaveOptions.TabIndex = 129;
			this.btnSaveOptions.Text = "Settings...";
			this.btnSaveOptions.UseVisualStyleBackColor = true;
			this.btnSaveOptions.Click += new System.EventHandler(this.btnSaveOptions_Click_1);
			// 
			// chkAutoSave
			// 
			this.chkAutoSave.AutoSize = true;
			this.chkAutoSave.Checked = true;
			this.chkAutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoSave.Location = new System.Drawing.Point(19, 66);
			this.chkAutoSave.Name = "chkAutoSave";
			this.chkAutoSave.Size = new System.Drawing.Size(71, 17);
			this.chkAutoSave.TabIndex = 128;
			this.chkAutoSave.Text = "Autosave";
			this.chkAutoSave.UseVisualStyleBackColor = true;
			// 
			// lblStep6
			// 
			this.lblStep6.AutoSize = true;
			this.lblStep6.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStep6.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep6.Location = new System.Drawing.Point(6, -5);
			this.lblStep6.Name = "lblStep6";
			this.lblStep6.Size = new System.Drawing.Size(21, 24);
			this.lblStep6.TabIndex = 127;
			this.lblStep6.Text = "6";
			// 
			// label4
			// 
			this.label4.AllowDrop = true;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(18, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 13);
			this.label4.TabIndex = 126;
			this.label4.Text = "New Sequence";
			// 
			// btnSaveSequence
			// 
			this.btnSaveSequence.AllowDrop = true;
			this.btnSaveSequence.Enabled = false;
			this.btnSaveSequence.Location = new System.Drawing.Point(244, 38);
			this.btnSaveSequence.Name = "btnSaveSequence";
			this.btnSaveSequence.Size = new System.Drawing.Size(75, 23);
			this.btnSaveSequence.TabIndex = 125;
			this.btnSaveSequence.Text = "Save As...";
			this.btnSaveSequence.UseVisualStyleBackColor = true;
			this.btnSaveSequence.Click += new System.EventHandler(this.btnSaveSequence_Click);
			// 
			// txtSaveName
			// 
			this.txtSaveName.AllowDrop = true;
			this.txtSaveName.Location = new System.Drawing.Point(19, 40);
			this.txtSaveName.Name = "txtSaveName";
			this.txtSaveName.ReadOnly = true;
			this.txtSaveName.Size = new System.Drawing.Size(219, 20);
			this.txtSaveName.TabIndex = 124;
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
			// treeMaster
			// 
			this.treeMaster.AllowDrop = true;
			this.treeMaster.BackColor = System.Drawing.Color.White;
			this.treeMaster.ImageIndex = 0;
			this.treeMaster.ImageList = this.imlTreeIcons;
			this.treeMaster.Location = new System.Drawing.Point(377, 123);
			this.treeMaster.Name = "treeMaster";
			this.treeMaster.SelectedImageIndex = 0;
			this.treeMaster.Size = new System.Drawing.Size(300, 433);
			this.treeMaster.TabIndex = 124;
			// 
			// pnlVamping
			// 
			this.pnlVamping.Controls.Add(this.lblSongName);
			this.pnlVamping.Controls.Add(this.label3);
			this.pnlVamping.Controls.Add(this.label2);
			this.pnlVamping.Controls.Add(this.label1);
			this.pnlVamping.Location = new System.Drawing.Point(296, 1);
			this.pnlVamping.Name = "pnlVamping";
			this.pnlVamping.Size = new System.Drawing.Size(334, 72);
			this.pnlVamping.TabIndex = 125;
			this.pnlVamping.Visible = false;
			this.pnlVamping.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlVamping_Paint);
			// 
			// lblSongName
			// 
			this.lblSongName.AutoSize = true;
			this.lblSongName.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSongName.ForeColor = System.Drawing.Color.Blue;
			this.lblSongName.Location = new System.Drawing.Point(5, 30);
			this.lblSongName.Name = "lblSongName";
			this.lblSongName.Size = new System.Drawing.Size(179, 20);
			this.lblSongName.TabIndex = 3;
			this.lblSongName.Text = "Blue Christmas by Elvis Presley";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Red;
			this.label3.Location = new System.Drawing.Point(5, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(189, 24);
			this.label3.TabIndex = 2;
			this.label3.Text = "Analyzing your song";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Red;
			this.label2.Location = new System.Drawing.Point(60, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Please wait...";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(3, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Vamp";
			this.label1.Visible = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.lblStep5);
			this.groupBox1.Enabled = false;
			this.groupBox1.Location = new System.Drawing.Point(12, 425);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(334, 66);
			this.groupBox1.TabIndex = 126;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "      Analyze Audio File";
			// 
			// button1
			// 
			this.button1.AllowDrop = true;
			this.button1.Location = new System.Drawing.Point(89, 19);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(117, 41);
			this.button1.TabIndex = 124;
			this.button1.Text = "Analyze...";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// lblStep5
			// 
			this.lblStep5.AutoSize = true;
			this.lblStep5.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStep5.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep5.Location = new System.Drawing.Point(6, -5);
			this.lblStep5.Name = "lblStep5";
			this.lblStep5.Size = new System.Drawing.Size(21, 24);
			this.lblStep5.TabIndex = 122;
			this.lblStep5.Text = "5";
			// 
			// picPreview
			// 
			this.picPreview.BackColor = System.Drawing.Color.Silver;
			this.picPreview.Location = new System.Drawing.Point(377, 567);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(302, 22);
			this.picPreview.TabIndex = 127;
			this.picPreview.TabStop = false;
			// 
			// frmTune
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(695, 639);
			this.Controls.Add(this.picPreview);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.treeMaster);
			this.Controls.Add(this.grpSequence);
			this.Controls.Add(this.grpAudio);
			this.Controls.Add(this.grpStart);
			this.Controls.Add(this.grpTracks);
			this.Controls.Add(this.grpGrids);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.pnlVamping);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "frmTune";
			this.Text = "Tune-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTune_FormClosing);
			this.Load += new System.EventHandler(this.frmTune_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmTune_Paint);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.grpGrids.ResumeLayout(false);
			this.grpGrids.PerformLayout();
			this.grpTracks.ResumeLayout(false);
			this.grpTracks.PerformLayout();
			this.grpStart.ResumeLayout(false);
			this.grpStart.PerformLayout();
			this.pnlSwitchStart.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picHoleStart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picSliderStart)).EndInit();
			this.grpAudio.ResumeLayout(false);
			this.grpAudio.PerformLayout();
			this.grpSequence.ResumeLayout(false);
			this.grpSequence.PerformLayout();
			this.pnlVamping.ResumeLayout(false);
			this.pnlVamping.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
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
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuOpenAudio;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveSequenceAs;
		private System.Windows.Forms.ToolStripSeparator mnuFileDivider1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripSeparator mnuFileDivider2;
		private System.Windows.Forms.ToolStripMenuItem mnuExit;
		private System.Windows.Forms.ToolStripMenuItem mnuGenerate;
		private System.Windows.Forms.ToolStripMenuItem mnuTimingMarks;
		private System.Windows.Forms.ToolStripMenuItem mnuTranscription;
		private System.Windows.Forms.ToolStripMenuItem mnuOptionsAnnotator;
		private System.Windows.Forms.ToolStripMenuItem mnuOptionsVamp;
		private System.Windows.Forms.ToolStripSeparator mnuOptionsDivider1;
		private System.Windows.Forms.ToolStripMenuItem mnuOptionsSaveFormat;
		private System.Windows.Forms.GroupBox grpGrids;
		private System.Windows.Forms.CheckBox chkBeatsGrid;
		private System.Windows.Forms.CheckBox chkNoteOnsets;
		private System.Windows.Forms.GroupBox grpTracks;
		private System.Windows.Forms.CheckBox chkBeatChannels;
		private System.Windows.Forms.CheckBox chkPolyphonic;
		private System.Windows.Forms.CheckBox chkConstQ;
		private System.Windows.Forms.GroupBox grpStart;
		private System.Windows.Forms.Label lblStep1;
		private System.Windows.Forms.Label lblDontUseConfig;
		private System.Windows.Forms.Label lblUseConfig;
		private System.Windows.Forms.Button btnBrowseSequence;
		private System.Windows.Forms.TextBox txtFileCurrent;
		private System.Windows.Forms.Button btnGridSettings;
		private System.Windows.Forms.Label lblStep3;
		private System.Windows.Forms.Label lblStep4;
		private System.Windows.Forms.Button btnTrackSettings;
		private System.Windows.Forms.GroupBox grpAudio;
		private System.Windows.Forms.Button btnBrowseAudio;
		private System.Windows.Forms.TextBox txtFileAudio;
		private System.Windows.Forms.Label lblFileAudio;
		private System.Windows.Forms.Label lblStep2;
		private System.Windows.Forms.GroupBox grpSequence;
		private System.Windows.Forms.Button btnSaveOptions;
		private System.Windows.Forms.CheckBox chkAutoSave;
		private System.Windows.Forms.Label lblStep6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnSaveSequence;
		private System.Windows.Forms.TextBox txtSaveName;
		private System.Windows.Forms.CheckBox chkAutoLaunch;
		private System.Windows.Forms.Panel pnlSwitchStart;
		private System.Windows.Forms.PictureBox picHoleStart;
		private System.Windows.Forms.PictureBox picSliderStart;
		private System.Windows.Forms.CheckBox chkSegments;
		private System.Windows.Forms.CheckBox chkChromagram;
		private System.Windows.Forms.CheckBox chkFlux;
		private System.Windows.Forms.CheckBox chkPitch;
		private System.Windows.Forms.CheckBox chkCords;
		private System.Windows.Forms.CheckBox chkVocals;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.TreeView treeMaster;
		private System.Windows.Forms.Panel pnlVamping;
		private System.Windows.Forms.Label lblSongName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label lblStep5;
		private System.Windows.Forms.PictureBox picPreview;
	}
}

