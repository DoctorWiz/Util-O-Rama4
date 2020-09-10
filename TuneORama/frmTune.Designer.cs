namespace xTune
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
			this.grpTimings = new System.Windows.Forms.GroupBox();
			this.btnResetDefaults = new System.Windows.Forms.Button();
			this.labelTweaks = new System.Windows.Forms.Label();
			this.grpVocals = new System.Windows.Forms.GroupBox();
			this.chkVocals = new System.Windows.Forms.CheckBox();
			this.label19 = new System.Windows.Forms.Label();
			this.cboAlignVocals = new System.Windows.Forms.ComboBox();
			this.grpSegments = new System.Windows.Forms.GroupBox();
			this.chkSegments = new System.Windows.Forms.CheckBox();
			this.label21 = new System.Windows.Forms.Label();
			this.cboAlignSegments = new System.Windows.Forms.ComboBox();
			this.grpTempo = new System.Windows.Forms.GroupBox();
			this.chkTempo = new System.Windows.Forms.CheckBox();
			this.label16 = new System.Windows.Forms.Label();
			this.cboLabelsTempo = new System.Windows.Forms.ComboBox();
			this.label17 = new System.Windows.Forms.Label();
			this.cboMethodTempo = new System.Windows.Forms.ComboBox();
			this.label18 = new System.Windows.Forms.Label();
			this.cboAlignTempo = new System.Windows.Forms.ComboBox();
			this.btnTrackSettings = new System.Windows.Forms.Button();
			this.grpPitchKey = new System.Windows.Forms.GroupBox();
			this.chkPitchKey = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.cboLabelsPitchKey = new System.Windows.Forms.ComboBox();
			this.label14 = new System.Windows.Forms.Label();
			this.cboMethodPitchKey = new System.Windows.Forms.ComboBox();
			this.label15 = new System.Windows.Forms.Label();
			this.cboAlignPitch = new System.Windows.Forms.ComboBox();
			this.grpSpectrum = new System.Windows.Forms.GroupBox();
			this.chkSpectrum = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.cboLabelsSpectrum = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.cboMethodSpectrum = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.cboAlignSpectrum = new System.Windows.Forms.ComboBox();
			this.grpTranscription = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.cboLabelsTranscription = new System.Windows.Forms.ComboBox();
			this.chkTranscribe = new System.Windows.Forms.CheckBox();
			this.cboMethodTranscription = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cboAlignTranscribe = new System.Windows.Forms.ComboBox();
			this.chkChromagram = new System.Windows.Forms.CheckBox();
			this.grpOnsets = new System.Windows.Forms.GroupBox();
			this.chkWhiteOnsets = new System.Windows.Forms.CheckBox();
			this.pnlOnsetSensitivity = new System.Windows.Forms.Panel();
			this.vscSensitivity = new System.Windows.Forms.VScrollBar();
			this.txtOnsetSensitivity = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.lblDetectOnsets = new System.Windows.Forms.Label();
			this.cboDetectOnsets = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.cboLabelsOnsets = new System.Windows.Forms.ComboBox();
			this.chkNoteOnsets = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cboMethodOnsets = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cboAlignOnsets = new System.Windows.Forms.ComboBox();
			this.grpBarsBeats = new System.Windows.Forms.GroupBox();
			this.chkBarsBeats = new System.Windows.Forms.CheckBox();
			this.pnlNotes = new System.Windows.Forms.Panel();
			this.label26 = new System.Windows.Forms.Label();
			this.chkBeatsThird = new System.Windows.Forms.CheckBox();
			this.label25 = new System.Windows.Forms.Label();
			this.chkBars = new System.Windows.Forms.CheckBox();
			this.label24 = new System.Windows.Forms.Label();
			this.chkBeatsFull = new System.Windows.Forms.CheckBox();
			this.chkBeatsHalf = new System.Windows.Forms.CheckBox();
			this.chkBeatsQuarter = new System.Windows.Forms.CheckBox();
			this.label22 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.chkWhiteBarBeats = new System.Windows.Forms.CheckBox();
			this.lblDetectBarBeats = new System.Windows.Forms.Label();
			this.cboDetectBarBeats = new System.Windows.Forms.ComboBox();
			this.lblMethod = new System.Windows.Forms.Label();
			this.cboMethodBarsBeats = new System.Windows.Forms.ComboBox();
			this.lblAlignBarsBeats = new System.Windows.Forms.Label();
			this.cboAlignBarsBeats = new System.Windows.Forms.ComboBox();
			this.pnlTrackBeatsX = new System.Windows.Forms.Panel();
			this.vscStartBeat = new System.Windows.Forms.VScrollBar();
			this.txtStartBeat = new System.Windows.Forms.TextBox();
			this.lblTrackBeatsX = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.swTrackBeat = new JCS.ToggleSwitch();
			this.lblTrackBeat34 = new System.Windows.Forms.Label();
			this.lblTrackBeat44 = new System.Windows.Forms.Label();
			this.lblStep2B = new System.Windows.Forms.Label();
			this.chkChords = new System.Windows.Forms.CheckBox();
			this.chkFlux = new System.Windows.Forms.CheckBox();
			this.grpAudio = new System.Windows.Forms.GroupBox();
			this.btnBrowseAudio = new System.Windows.Forms.Button();
			this.txtFileAudio = new System.Windows.Forms.TextBox();
			this.lblFileAudio = new System.Windows.Forms.Label();
			this.lblStep1 = new System.Windows.Forms.Label();
			this.grpSave = new System.Windows.Forms.GroupBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.optOnePer = new System.Windows.Forms.RadioButton();
			this.optMultiPer = new System.Windows.Forms.RadioButton();
			this.btnSaveOptions = new System.Windows.Forms.Button();
			this.lblStep4 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.txtSaveName = new System.Windows.Forms.TextBox();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.grpAnalyze = new System.Windows.Forms.GroupBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblStep3 = new System.Windows.Forms.Label();
			this.pnlVamping = new System.Windows.Forms.Panel();
			this.lblSongName = new System.Windows.Forms.Label();
			this.lblAnalyzing = new System.Windows.Forms.Label();
			this.lblWait = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.grpOptions = new System.Windows.Forms.GroupBox();
			this.label20 = new System.Windows.Forms.Label();
			this.picVampire = new System.Windows.Forms.PictureBox();
			this.lblHelpOnsets = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblStep2A = new System.Windows.Forms.Label();
			this.chkReuse = new System.Windows.Forms.CheckBox();
			this.tmrAni = new System.Windows.Forms.Timer(this.components);
			this.chk15fps = new System.Windows.Forms.CheckBox();
			this.chk30fps = new System.Windows.Forms.CheckBox();
			this.chk24fps = new System.Windows.Forms.CheckBox();
			this.staStatus.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.grpTimings.SuspendLayout();
			this.grpVocals.SuspendLayout();
			this.grpSegments.SuspendLayout();
			this.grpTempo.SuspendLayout();
			this.grpPitchKey.SuspendLayout();
			this.grpSpectrum.SuspendLayout();
			this.grpTranscription.SuspendLayout();
			this.grpOnsets.SuspendLayout();
			this.pnlOnsetSensitivity.SuspendLayout();
			this.grpBarsBeats.SuspendLayout();
			this.pnlNotes.SuspendLayout();
			this.pnlTrackBeatsX.SuspendLayout();
			this.panel1.SuspendLayout();
			this.grpAudio.SuspendLayout();
			this.grpSave.SuspendLayout();
			this.panel2.SuspendLayout();
			this.grpAnalyze.SuspendLayout();
			this.pnlVamping.SuspendLayout();
			this.grpOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picVampire)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
			this.staStatus.Location = new System.Drawing.Point(0, 571);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(990, 24);
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
			this.pnlStatus.Size = new System.Drawing.Size(878, 19);
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
			this.menuStrip1.Size = new System.Drawing.Size(873, 24);
			this.menuStrip1.TabIndex = 110;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
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
			// grpTimings
			// 
			this.grpTimings.Controls.Add(this.chk24fps);
			this.grpTimings.Controls.Add(this.chk30fps);
			this.grpTimings.Controls.Add(this.chk15fps);
			this.grpTimings.Controls.Add(this.btnResetDefaults);
			this.grpTimings.Controls.Add(this.labelTweaks);
			this.grpTimings.Controls.Add(this.grpVocals);
			this.grpTimings.Controls.Add(this.grpSegments);
			this.grpTimings.Controls.Add(this.grpTempo);
			this.grpTimings.Controls.Add(this.btnTrackSettings);
			this.grpTimings.Controls.Add(this.grpPitchKey);
			this.grpTimings.Controls.Add(this.grpSpectrum);
			this.grpTimings.Controls.Add(this.grpTranscription);
			this.grpTimings.Controls.Add(this.chkChromagram);
			this.grpTimings.Controls.Add(this.grpOnsets);
			this.grpTimings.Controls.Add(this.grpBarsBeats);
			this.grpTimings.Controls.Add(this.lblStep2B);
			this.grpTimings.Controls.Add(this.chkChords);
			this.grpTimings.Controls.Add(this.chkFlux);
			this.grpTimings.Enabled = false;
			this.grpTimings.Location = new System.Drawing.Point(352, 12);
			this.grpTimings.Name = "grpTimings";
			this.grpTimings.Size = new System.Drawing.Size(626, 551);
			this.grpTimings.TabIndex = 113;
			this.grpTimings.TabStop = false;
			this.grpTimings.Text = "      Select Which Timings to Generate";
			// 
			// btnResetDefaults
			// 
			this.btnResetDefaults.AllowDrop = true;
			this.btnResetDefaults.Location = new System.Drawing.Point(487, 513);
			this.btnResetDefaults.Name = "btnResetDefaults";
			this.btnResetDefaults.Size = new System.Drawing.Size(123, 23);
			this.btnResetDefaults.TabIndex = 158;
			this.btnResetDefaults.Text = "Reset to Defaults";
			this.btnResetDefaults.UseVisualStyleBackColor = true;
			this.btnResetDefaults.Click += new System.EventHandler(this.btnResetDefaults_Click);
			// 
			// labelTweaks
			// 
			this.labelTweaks.AutoSize = true;
			this.labelTweaks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTweaks.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.labelTweaks.Location = new System.Drawing.Point(492, 343);
			this.labelTweaks.Name = "labelTweaks";
			this.labelTweaks.Size = new System.Drawing.Size(79, 13);
			this.labelTweaks.TabIndex = 157;
			this.labelTweaks.Text = "More Options...";
			this.labelTweaks.Click += new System.EventHandler(this.labelTweaks_Click);
			// 
			// grpVocals
			// 
			this.grpVocals.Controls.Add(this.chkVocals);
			this.grpVocals.Controls.Add(this.label19);
			this.grpVocals.Controls.Add(this.cboAlignVocals);
			this.grpVocals.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpVocals.Location = new System.Drawing.Point(469, 116);
			this.grpVocals.Name = "grpVocals";
			this.grpVocals.Size = new System.Drawing.Size(147, 86);
			this.grpVocals.TabIndex = 143;
			this.grpVocals.TabStop = false;
			this.grpVocals.Text = "   Vocals";
			// 
			// chkVocals
			// 
			this.chkVocals.AutoSize = true;
			this.chkVocals.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkVocals.Location = new System.Drawing.Point(0, 0);
			this.chkVocals.Name = "chkVocals";
			this.chkVocals.Size = new System.Drawing.Size(15, 14);
			this.chkVocals.TabIndex = 143;
			this.chkVocals.Tag = "14";
			this.chkVocals.UseVisualStyleBackColor = true;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label19.Location = new System.Drawing.Point(6, 41);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(52, 13);
			this.label19.TabIndex = 142;
			this.label19.Text = "Align To:";
			// 
			// cboAlignVocals
			// 
			this.cboAlignVocals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignVocals.DropDownWidth = 180;
			this.cboAlignVocals.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboAlignVocals.FormattingEnabled = true;
			this.cboAlignVocals.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Bars / Whole Notes",
            "Full Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes",
            "Note Onsets"});
			this.cboAlignVocals.Location = new System.Drawing.Point(6, 57);
			this.cboAlignVocals.Name = "cboAlignVocals";
			this.cboAlignVocals.Size = new System.Drawing.Size(135, 21);
			this.cboAlignVocals.TabIndex = 141;
			// 
			// grpSegments
			// 
			this.grpSegments.Controls.Add(this.chkSegments);
			this.grpSegments.Controls.Add(this.label21);
			this.grpSegments.Controls.Add(this.cboAlignSegments);
			this.grpSegments.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpSegments.Location = new System.Drawing.Point(469, 22);
			this.grpSegments.Name = "grpSegments";
			this.grpSegments.Size = new System.Drawing.Size(147, 86);
			this.grpSegments.TabIndex = 142;
			this.grpSegments.TabStop = false;
			this.grpSegments.Text = "   Segments";
			// 
			// chkSegments
			// 
			this.chkSegments.AutoSize = true;
			this.chkSegments.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkSegments.Location = new System.Drawing.Point(0, 0);
			this.chkSegments.Name = "chkSegments";
			this.chkSegments.Size = new System.Drawing.Size(15, 14);
			this.chkSegments.TabIndex = 150;
			this.chkSegments.Tag = "7";
			this.chkSegments.UseVisualStyleBackColor = true;
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label21.Location = new System.Drawing.Point(6, 41);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(52, 13);
			this.label21.TabIndex = 142;
			this.label21.Text = "Align To:";
			// 
			// cboAlignSegments
			// 
			this.cboAlignSegments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignSegments.DropDownWidth = 180;
			this.cboAlignSegments.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboAlignSegments.FormattingEnabled = true;
			this.cboAlignSegments.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Bars / Whole Notes",
            "Full Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes",
            "Note Onsets"});
			this.cboAlignSegments.Location = new System.Drawing.Point(6, 57);
			this.cboAlignSegments.Name = "cboAlignSegments";
			this.cboAlignSegments.Size = new System.Drawing.Size(135, 21);
			this.cboAlignSegments.TabIndex = 141;
			// 
			// grpTempo
			// 
			this.grpTempo.Controls.Add(this.chkTempo);
			this.grpTempo.Controls.Add(this.label16);
			this.grpTempo.Controls.Add(this.cboLabelsTempo);
			this.grpTempo.Controls.Add(this.label17);
			this.grpTempo.Controls.Add(this.cboMethodTempo);
			this.grpTempo.Controls.Add(this.label18);
			this.grpTempo.Controls.Add(this.cboAlignTempo);
			this.grpTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpTempo.Location = new System.Drawing.Point(322, 376);
			this.grpTempo.Name = "grpTempo";
			this.grpTempo.Size = new System.Drawing.Size(147, 166);
			this.grpTempo.TabIndex = 141;
			this.grpTempo.TabStop = false;
			this.grpTempo.Text = "   Tempo";
			// 
			// chkTempo
			// 
			this.chkTempo.AutoSize = true;
			this.chkTempo.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkTempo.Location = new System.Drawing.Point(0, 0);
			this.chkTempo.Name = "chkTempo";
			this.chkTempo.Size = new System.Drawing.Size(15, 14);
			this.chkTempo.TabIndex = 150;
			this.chkTempo.Tag = "9";
			this.chkTempo.UseVisualStyleBackColor = true;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(6, 81);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(41, 13);
			this.label16.TabIndex = 149;
			this.label16.Text = "Labels:";
			// 
			// cboLabelsTempo
			// 
			this.cboLabelsTempo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboLabelsTempo.FormattingEnabled = true;
			this.cboLabelsTempo.Items.AddRange(new object[] {
            "None",
            "Note Names",
            "MIDI Note Numbers"});
			this.cboLabelsTempo.Location = new System.Drawing.Point(6, 97);
			this.cboLabelsTempo.Name = "cboLabelsTempo";
			this.cboLabelsTempo.Size = new System.Drawing.Size(135, 21);
			this.cboLabelsTempo.TabIndex = 148;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label17.Location = new System.Drawing.Point(6, 16);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(86, 13);
			this.label17.TabIndex = 144;
			this.label17.Text = "Plugin / Method:";
			// 
			// cboMethodTempo
			// 
			this.cboMethodTempo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboMethodTempo.FormattingEnabled = true;
			this.cboMethodTempo.Items.AddRange(new object[] {
            "Queen Mary Note Onset Detector",
            "Queen Mary Polyphonic Transcription",
            "OnsetDS Onset Detector",
            "Silvet Note Transcription",
            "Alicante Note Onset Detector",
            "Alicante Polyphonic Transcription",
            "Aubio Onset Detector",
            "Aubio Note Tracker"});
			this.cboMethodTempo.Location = new System.Drawing.Point(6, 33);
			this.cboMethodTempo.Name = "cboMethodTempo";
			this.cboMethodTempo.Size = new System.Drawing.Size(135, 21);
			this.cboMethodTempo.TabIndex = 143;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label18.Location = new System.Drawing.Point(6, 121);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(49, 13);
			this.label18.TabIndex = 142;
			this.label18.Text = "Align To:";
			// 
			// cboAlignTempo
			// 
			this.cboAlignTempo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignTempo.DropDownWidth = 180;
			this.cboAlignTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboAlignTempo.FormattingEnabled = true;
			this.cboAlignTempo.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Bars / Whole Notes",
            "Full Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes",
            "Note Onsets"});
			this.cboAlignTempo.Location = new System.Drawing.Point(6, 137);
			this.cboAlignTempo.Name = "cboAlignTempo";
			this.cboAlignTempo.Size = new System.Drawing.Size(135, 21);
			this.cboAlignTempo.TabIndex = 141;
			// 
			// btnTrackSettings
			// 
			this.btnTrackSettings.AllowDrop = true;
			this.btnTrackSettings.Enabled = false;
			this.btnTrackSettings.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTrackSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnTrackSettings.Image")));
			this.btnTrackSettings.Location = new System.Drawing.Point(523, 458);
			this.btnTrackSettings.Name = "btnTrackSettings";
			this.btnTrackSettings.Size = new System.Drawing.Size(32, 32);
			this.btnTrackSettings.TabIndex = 122;
			this.btnTrackSettings.UseVisualStyleBackColor = true;
			this.btnTrackSettings.Visible = false;
			this.btnTrackSettings.Click += new System.EventHandler(this.btnTrackSettings_Click);
			// 
			// grpPitchKey
			// 
			this.grpPitchKey.Controls.Add(this.chkPitchKey);
			this.grpPitchKey.Controls.Add(this.label12);
			this.grpPitchKey.Controls.Add(this.cboLabelsPitchKey);
			this.grpPitchKey.Controls.Add(this.label14);
			this.grpPitchKey.Controls.Add(this.cboMethodPitchKey);
			this.grpPitchKey.Controls.Add(this.label15);
			this.grpPitchKey.Controls.Add(this.cboAlignPitch);
			this.grpPitchKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpPitchKey.Location = new System.Drawing.Point(169, 376);
			this.grpPitchKey.Name = "grpPitchKey";
			this.grpPitchKey.Size = new System.Drawing.Size(147, 166);
			this.grpPitchKey.TabIndex = 140;
			this.grpPitchKey.TabStop = false;
			this.grpPitchKey.Text = "   Pitch && Key";
			// 
			// chkPitchKey
			// 
			this.chkPitchKey.AutoSize = true;
			this.chkPitchKey.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkPitchKey.Location = new System.Drawing.Point(0, 0);
			this.chkPitchKey.Name = "chkPitchKey";
			this.chkPitchKey.Size = new System.Drawing.Size(15, 14);
			this.chkPitchKey.TabIndex = 150;
			this.chkPitchKey.Tag = "6";
			this.chkPitchKey.UseVisualStyleBackColor = true;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(6, 81);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(41, 13);
			this.label12.TabIndex = 149;
			this.label12.Text = "Labels:";
			// 
			// cboLabelsPitchKey
			// 
			this.cboLabelsPitchKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsPitchKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboLabelsPitchKey.FormattingEnabled = true;
			this.cboLabelsPitchKey.Items.AddRange(new object[] {
            "None",
            "Note Names",
            "MIDI Note Numbers"});
			this.cboLabelsPitchKey.Location = new System.Drawing.Point(6, 97);
			this.cboLabelsPitchKey.Name = "cboLabelsPitchKey";
			this.cboLabelsPitchKey.Size = new System.Drawing.Size(135, 21);
			this.cboLabelsPitchKey.TabIndex = 148;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(6, 16);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(86, 13);
			this.label14.TabIndex = 144;
			this.label14.Text = "Plugin / Method:";
			// 
			// cboMethodPitchKey
			// 
			this.cboMethodPitchKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodPitchKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboMethodPitchKey.FormattingEnabled = true;
			this.cboMethodPitchKey.Items.AddRange(new object[] {
            "Queen Mary Note Onset Detector",
            "Queen Mary Polyphonic Transcription",
            "OnsetDS Onset Detector",
            "Silvet Note Transcription",
            "Alicante Note Onset Detector",
            "Alicante Polyphonic Transcription",
            "Aubio Onset Detector",
            "Aubio Note Tracker"});
			this.cboMethodPitchKey.Location = new System.Drawing.Point(6, 33);
			this.cboMethodPitchKey.Name = "cboMethodPitchKey";
			this.cboMethodPitchKey.Size = new System.Drawing.Size(135, 21);
			this.cboMethodPitchKey.TabIndex = 143;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(6, 121);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(49, 13);
			this.label15.TabIndex = 142;
			this.label15.Text = "Align To:";
			// 
			// cboAlignPitch
			// 
			this.cboAlignPitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignPitch.DropDownWidth = 180;
			this.cboAlignPitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboAlignPitch.FormattingEnabled = true;
			this.cboAlignPitch.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Bars / Whole Notes",
            "Full Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes",
            "Note Onsets"});
			this.cboAlignPitch.Location = new System.Drawing.Point(6, 137);
			this.cboAlignPitch.Name = "cboAlignPitch";
			this.cboAlignPitch.Size = new System.Drawing.Size(135, 21);
			this.cboAlignPitch.TabIndex = 141;
			// 
			// grpSpectrum
			// 
			this.grpSpectrum.Controls.Add(this.chkSpectrum);
			this.grpSpectrum.Controls.Add(this.label9);
			this.grpSpectrum.Controls.Add(this.cboLabelsSpectrum);
			this.grpSpectrum.Controls.Add(this.label10);
			this.grpSpectrum.Controls.Add(this.cboMethodSpectrum);
			this.grpSpectrum.Controls.Add(this.label11);
			this.grpSpectrum.Controls.Add(this.cboAlignSpectrum);
			this.grpSpectrum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpSpectrum.Location = new System.Drawing.Point(11, 376);
			this.grpSpectrum.Name = "grpSpectrum";
			this.grpSpectrum.Size = new System.Drawing.Size(147, 166);
			this.grpSpectrum.TabIndex = 139;
			this.grpSpectrum.TabStop = false;
			this.grpSpectrum.Text = "   Spectrum";
			// 
			// chkSpectrum
			// 
			this.chkSpectrum.AutoSize = true;
			this.chkSpectrum.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkSpectrum.Location = new System.Drawing.Point(0, 0);
			this.chkSpectrum.Name = "chkSpectrum";
			this.chkSpectrum.Size = new System.Drawing.Size(15, 14);
			this.chkSpectrum.TabIndex = 150;
			this.chkSpectrum.Tag = "11";
			this.chkSpectrum.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(6, 81);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(41, 13);
			this.label9.TabIndex = 149;
			this.label9.Text = "Labels:";
			// 
			// cboLabelsSpectrum
			// 
			this.cboLabelsSpectrum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsSpectrum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboLabelsSpectrum.FormattingEnabled = true;
			this.cboLabelsSpectrum.Items.AddRange(new object[] {
            "None",
            "Note Names",
            "MIDI Note Numbers"});
			this.cboLabelsSpectrum.Location = new System.Drawing.Point(6, 97);
			this.cboLabelsSpectrum.Name = "cboLabelsSpectrum";
			this.cboLabelsSpectrum.Size = new System.Drawing.Size(135, 21);
			this.cboLabelsSpectrum.TabIndex = 148;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(6, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(86, 13);
			this.label10.TabIndex = 144;
			this.label10.Text = "Plugin / Method:";
			// 
			// cboMethodSpectrum
			// 
			this.cboMethodSpectrum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodSpectrum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboMethodSpectrum.FormattingEnabled = true;
			this.cboMethodSpectrum.Items.AddRange(new object[] {
            "Queen Mary Note Onset Detector",
            "Queen Mary Polyphonic Transcription",
            "OnsetDS Onset Detector",
            "Silvet Note Transcription",
            "Alicante Note Onset Detector",
            "Alicante Polyphonic Transcription",
            "Aubio Onset Detector",
            "Aubio Note Tracker"});
			this.cboMethodSpectrum.Location = new System.Drawing.Point(6, 32);
			this.cboMethodSpectrum.Name = "cboMethodSpectrum";
			this.cboMethodSpectrum.Size = new System.Drawing.Size(135, 21);
			this.cboMethodSpectrum.TabIndex = 143;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(6, 121);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(49, 13);
			this.label11.TabIndex = 142;
			this.label11.Text = "Align To:";
			// 
			// cboAlignSpectrum
			// 
			this.cboAlignSpectrum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignSpectrum.DropDownWidth = 180;
			this.cboAlignSpectrum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboAlignSpectrum.FormattingEnabled = true;
			this.cboAlignSpectrum.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Bars / Whole Notes",
            "Full Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes",
            "Note Onsets"});
			this.cboAlignSpectrum.Location = new System.Drawing.Point(6, 137);
			this.cboAlignSpectrum.Name = "cboAlignSpectrum";
			this.cboAlignSpectrum.Size = new System.Drawing.Size(135, 21);
			this.cboAlignSpectrum.TabIndex = 141;
			// 
			// grpTranscription
			// 
			this.grpTranscription.Controls.Add(this.label5);
			this.grpTranscription.Controls.Add(this.label7);
			this.grpTranscription.Controls.Add(this.cboLabelsTranscription);
			this.grpTranscription.Controls.Add(this.chkTranscribe);
			this.grpTranscription.Controls.Add(this.cboMethodTranscription);
			this.grpTranscription.Controls.Add(this.label6);
			this.grpTranscription.Controls.Add(this.cboAlignTranscribe);
			this.grpTranscription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpTranscription.Location = new System.Drawing.Point(316, 22);
			this.grpTranscription.Name = "grpTranscription";
			this.grpTranscription.Size = new System.Drawing.Size(147, 346);
			this.grpTranscription.TabIndex = 138;
			this.grpTranscription.TabStop = false;
			this.grpTranscription.Text = "   Transcription";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(6, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 13);
			this.label5.TabIndex = 148;
			this.label5.Text = "Plugin / Method:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(6, 261);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 13);
			this.label7.TabIndex = 147;
			this.label7.Text = "Labels:";
			// 
			// cboLabelsTranscription
			// 
			this.cboLabelsTranscription.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsTranscription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboLabelsTranscription.FormattingEnabled = true;
			this.cboLabelsTranscription.Items.AddRange(new object[] {
            "MIDI Note Numbers",
            "Note Names"});
			this.cboLabelsTranscription.Location = new System.Drawing.Point(6, 277);
			this.cboLabelsTranscription.Name = "cboLabelsTranscription";
			this.cboLabelsTranscription.Size = new System.Drawing.Size(135, 21);
			this.cboLabelsTranscription.TabIndex = 146;
			// 
			// chkTranscribe
			// 
			this.chkTranscribe.AutoSize = true;
			this.chkTranscribe.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkTranscribe.Location = new System.Drawing.Point(0, 0);
			this.chkTranscribe.Name = "chkTranscribe";
			this.chkTranscribe.Size = new System.Drawing.Size(15, 14);
			this.chkTranscribe.TabIndex = 145;
			this.chkTranscribe.Tag = "10";
			this.chkTranscribe.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.chkTranscribe.UseVisualStyleBackColor = true;
			// 
			// cboMethodTranscription
			// 
			this.cboMethodTranscription.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodTranscription.DropDownWidth = 200;
			this.cboMethodTranscription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboMethodTranscription.FormattingEnabled = true;
			this.cboMethodTranscription.Items.AddRange(new object[] {
            "Queen Mary Polyphonic Transcription",
            "Silvet Note Transcription",
            "Alicante Polyphonic Transcription",
            "Aubio Note Tracker"});
			this.cboMethodTranscription.Location = new System.Drawing.Point(6, 32);
			this.cboMethodTranscription.Name = "cboMethodTranscription";
			this.cboMethodTranscription.Size = new System.Drawing.Size(135, 21);
			this.cboMethodTranscription.TabIndex = 143;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(6, 301);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(49, 13);
			this.label6.TabIndex = 142;
			this.label6.Text = "Align To:";
			// 
			// cboAlignTranscribe
			// 
			this.cboAlignTranscribe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignTranscribe.DropDownWidth = 180;
			this.cboAlignTranscribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboAlignTranscribe.FormattingEnabled = true;
			this.cboAlignTranscribe.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes",
            "Note Onsets"});
			this.cboAlignTranscribe.Location = new System.Drawing.Point(6, 317);
			this.cboAlignTranscribe.Name = "cboAlignTranscribe";
			this.cboAlignTranscribe.Size = new System.Drawing.Size(135, 21);
			this.cboAlignTranscribe.TabIndex = 141;
			// 
			// chkChromagram
			// 
			this.chkChromagram.AutoSize = true;
			this.chkChromagram.Enabled = false;
			this.chkChromagram.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkChromagram.Location = new System.Drawing.Point(507, 424);
			this.chkChromagram.Name = "chkChromagram";
			this.chkChromagram.Size = new System.Drawing.Size(92, 17);
			this.chkChromagram.TabIndex = 134;
			this.chkChromagram.Tag = "8";
			this.chkChromagram.Text = "Chromagram";
			this.chkChromagram.UseVisualStyleBackColor = true;
			this.chkChromagram.CheckedChanged += new System.EventHandler(this.chkTiming_CheckedChanged);
			// 
			// grpOnsets
			// 
			this.grpOnsets.Controls.Add(this.chkWhiteOnsets);
			this.grpOnsets.Controls.Add(this.pnlOnsetSensitivity);
			this.grpOnsets.Controls.Add(this.lblDetectOnsets);
			this.grpOnsets.Controls.Add(this.cboDetectOnsets);
			this.grpOnsets.Controls.Add(this.label8);
			this.grpOnsets.Controls.Add(this.cboLabelsOnsets);
			this.grpOnsets.Controls.Add(this.chkNoteOnsets);
			this.grpOnsets.Controls.Add(this.label2);
			this.grpOnsets.Controls.Add(this.cboMethodOnsets);
			this.grpOnsets.Controls.Add(this.label3);
			this.grpOnsets.Controls.Add(this.cboAlignOnsets);
			this.grpOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpOnsets.Location = new System.Drawing.Point(163, 22);
			this.grpOnsets.Name = "grpOnsets";
			this.grpOnsets.Size = new System.Drawing.Size(147, 346);
			this.grpOnsets.TabIndex = 137;
			this.grpOnsets.TabStop = false;
			this.grpOnsets.Text = "   Note Onsets ";
			// 
			// chkWhiteOnsets
			// 
			this.chkWhiteOnsets.AutoSize = true;
			this.chkWhiteOnsets.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkWhiteOnsets.Location = new System.Drawing.Point(6, 125);
			this.chkWhiteOnsets.Name = "chkWhiteOnsets";
			this.chkWhiteOnsets.Size = new System.Drawing.Size(89, 17);
			this.chkWhiteOnsets.TabIndex = 153;
			this.chkWhiteOnsets.Tag = "13";
			this.chkWhiteOnsets.Text = "\"Whitening\"";
			this.chkWhiteOnsets.UseVisualStyleBackColor = true;
			// 
			// pnlOnsetSensitivity
			// 
			this.pnlOnsetSensitivity.Controls.Add(this.vscSensitivity);
			this.pnlOnsetSensitivity.Controls.Add(this.txtOnsetSensitivity);
			this.pnlOnsetSensitivity.Controls.Add(this.label13);
			this.pnlOnsetSensitivity.Location = new System.Drawing.Point(6, 99);
			this.pnlOnsetSensitivity.Name = "pnlOnsetSensitivity";
			this.pnlOnsetSensitivity.Size = new System.Drawing.Size(101, 23);
			this.pnlOnsetSensitivity.TabIndex = 152;
			// 
			// vscSensitivity
			// 
			this.vscSensitivity.Location = new System.Drawing.Point(85, -1);
			this.vscSensitivity.Minimum = 10;
			this.vscSensitivity.Name = "vscSensitivity";
			this.vscSensitivity.Size = new System.Drawing.Size(16, 18);
			this.vscSensitivity.TabIndex = 28;
			this.vscSensitivity.Value = 10;
			this.vscSensitivity.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
			// 
			// txtOnsetSensitivity
			// 
			this.txtOnsetSensitivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOnsetSensitivity.Location = new System.Drawing.Point(54, 0);
			this.txtOnsetSensitivity.Name = "txtOnsetSensitivity";
			this.txtOnsetSensitivity.ReadOnly = true;
			this.txtOnsetSensitivity.Size = new System.Drawing.Size(29, 20);
			this.txtOnsetSensitivity.TabIndex = 7;
			this.txtOnsetSensitivity.Text = "50";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(1, 4);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(57, 13);
			this.label13.TabIndex = 6;
			this.label13.Text = "Sensitivity:";
			this.label13.UseMnemonic = false;
			// 
			// lblDetectOnsets
			// 
			this.lblDetectOnsets.AutoSize = true;
			this.lblDetectOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDetectOnsets.Location = new System.Drawing.Point(6, 56);
			this.lblDetectOnsets.Name = "lblDetectOnsets";
			this.lblDetectOnsets.Size = new System.Drawing.Size(95, 13);
			this.lblDetectOnsets.TabIndex = 151;
			this.lblDetectOnsets.Text = "Detection Method:";
			// 
			// cboDetectOnsets
			// 
			this.cboDetectOnsets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDetectOnsets.DropDownWidth = 300;
			this.cboDetectOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboDetectOnsets.FormattingEnabled = true;
			this.cboDetectOnsets.Items.AddRange(new object[] {
            "\'Complex Domain\' (Strings/Mixed: Piano, Guitar)",
            "\'Spectral Difference\' (Percussion: Drums, Chimes)",
            "\'Phase Deviation\' (Wind: Flute, Sax, Trumpet)",
            "\'Broadband Energy Rise\' (Percussion mixed with other)"});
			this.cboDetectOnsets.Location = new System.Drawing.Point(6, 72);
			this.cboDetectOnsets.Name = "cboDetectOnsets";
			this.cboDetectOnsets.Size = new System.Drawing.Size(135, 21);
			this.cboDetectOnsets.TabIndex = 150;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(6, 261);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 13);
			this.label8.TabIndex = 149;
			this.label8.Text = "Labels:";
			// 
			// cboLabelsOnsets
			// 
			this.cboLabelsOnsets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboLabelsOnsets.FormattingEnabled = true;
			this.cboLabelsOnsets.Items.AddRange(new object[] {
            "None",
            "Note Names",
            "MIDI Note Numbers"});
			this.cboLabelsOnsets.Location = new System.Drawing.Point(6, 277);
			this.cboLabelsOnsets.Name = "cboLabelsOnsets";
			this.cboLabelsOnsets.Size = new System.Drawing.Size(135, 21);
			this.cboLabelsOnsets.TabIndex = 148;
			// 
			// chkNoteOnsets
			// 
			this.chkNoteOnsets.AutoSize = true;
			this.chkNoteOnsets.Checked = true;
			this.chkNoteOnsets.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkNoteOnsets.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkNoteOnsets.Location = new System.Drawing.Point(0, 0);
			this.chkNoteOnsets.Name = "chkNoteOnsets";
			this.chkNoteOnsets.Size = new System.Drawing.Size(15, 14);
			this.chkNoteOnsets.TabIndex = 145;
			this.chkNoteOnsets.Tag = "5";
			this.chkNoteOnsets.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(6, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 13);
			this.label2.TabIndex = 144;
			this.label2.Text = "Plugin / Method:";
			// 
			// cboMethodOnsets
			// 
			this.cboMethodOnsets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodOnsets.DropDownWidth = 200;
			this.cboMethodOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboMethodOnsets.FormattingEnabled = true;
			this.cboMethodOnsets.Items.AddRange(new object[] {
            "Queen Mary Note Onset Detector",
            "Queen Mary Polyphonic Transcription",
            "OnsetDS Onset Detector",
            "Silvet Note Transcription",
            "Aubio Onset Detector",
            "Aubio Note Tracker",
            "#Alicante Note Onset Detector",
            "#Alicante Polyphonic Transcription"});
			this.cboMethodOnsets.Location = new System.Drawing.Point(6, 32);
			this.cboMethodOnsets.Name = "cboMethodOnsets";
			this.cboMethodOnsets.Size = new System.Drawing.Size(135, 21);
			this.cboMethodOnsets.TabIndex = 143;
			this.cboMethodOnsets.SelectedIndexChanged += new System.EventHandler(this.cboOnsetsMethod_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(6, 301);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(49, 13);
			this.label3.TabIndex = 142;
			this.label3.Text = "Align To:";
			// 
			// cboAlignOnsets
			// 
			this.cboAlignOnsets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignOnsets.DropDownWidth = 180;
			this.cboAlignOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboAlignOnsets.FormattingEnabled = true;
			this.cboAlignOnsets.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes"});
			this.cboAlignOnsets.Location = new System.Drawing.Point(6, 317);
			this.cboAlignOnsets.Name = "cboAlignOnsets";
			this.cboAlignOnsets.Size = new System.Drawing.Size(141, 21);
			this.cboAlignOnsets.TabIndex = 141;
			// 
			// grpBarsBeats
			// 
			this.grpBarsBeats.Controls.Add(this.chkBarsBeats);
			this.grpBarsBeats.Controls.Add(this.pnlNotes);
			this.grpBarsBeats.Controls.Add(this.chkWhiteBarBeats);
			this.grpBarsBeats.Controls.Add(this.lblDetectBarBeats);
			this.grpBarsBeats.Controls.Add(this.cboDetectBarBeats);
			this.grpBarsBeats.Controls.Add(this.lblMethod);
			this.grpBarsBeats.Controls.Add(this.cboMethodBarsBeats);
			this.grpBarsBeats.Controls.Add(this.lblAlignBarsBeats);
			this.grpBarsBeats.Controls.Add(this.cboAlignBarsBeats);
			this.grpBarsBeats.Controls.Add(this.pnlTrackBeatsX);
			this.grpBarsBeats.Controls.Add(this.panel1);
			this.grpBarsBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpBarsBeats.Location = new System.Drawing.Point(10, 22);
			this.grpBarsBeats.Name = "grpBarsBeats";
			this.grpBarsBeats.Size = new System.Drawing.Size(147, 346);
			this.grpBarsBeats.TabIndex = 136;
			this.grpBarsBeats.TabStop = false;
			this.grpBarsBeats.Text = "   Bars && Beats ";
			// 
			// chkBarsBeats
			// 
			this.chkBarsBeats.AutoSize = true;
			this.chkBarsBeats.Checked = true;
			this.chkBarsBeats.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBarsBeats.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkBarsBeats.Location = new System.Drawing.Point(0, 0);
			this.chkBarsBeats.Name = "chkBarsBeats";
			this.chkBarsBeats.Size = new System.Drawing.Size(15, 14);
			this.chkBarsBeats.TabIndex = 156;
			this.chkBarsBeats.Tag = "13";
			this.chkBarsBeats.UseVisualStyleBackColor = true;
			// 
			// pnlNotes
			// 
			this.pnlNotes.Controls.Add(this.label26);
			this.pnlNotes.Controls.Add(this.chkBeatsThird);
			this.pnlNotes.Controls.Add(this.label25);
			this.pnlNotes.Controls.Add(this.chkBars);
			this.pnlNotes.Controls.Add(this.label24);
			this.pnlNotes.Controls.Add(this.chkBeatsFull);
			this.pnlNotes.Controls.Add(this.chkBeatsHalf);
			this.pnlNotes.Controls.Add(this.chkBeatsQuarter);
			this.pnlNotes.Controls.Add(this.label22);
			this.pnlNotes.Controls.Add(this.label23);
			this.pnlNotes.Location = new System.Drawing.Point(1, 175);
			this.pnlNotes.Name = "pnlNotes";
			this.pnlNotes.Size = new System.Drawing.Size(145, 118);
			this.pnlNotes.TabIndex = 155;
			this.pnlNotes.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlNotes_Paint);
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label26.Location = new System.Drawing.Point(233, 148);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(24, 30);
			this.label26.TabIndex = 148;
			this.label26.Text = "𝅘𝅥𝅯";
			this.label26.Visible = false;
			// 
			// chkBeatsThird
			// 
			this.chkBeatsThird.AutoSize = true;
			this.chkBeatsThird.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkBeatsThird.Location = new System.Drawing.Point(24, 66);
			this.chkBeatsThird.Name = "chkBeatsThird";
			this.chkBeatsThird.Size = new System.Drawing.Size(73, 19);
			this.chkBeatsThird.TabIndex = 141;
			this.chkBeatsThird.Tag = "3";
			this.chkBeatsThird.Text = "Third Beats";
			this.chkBeatsThird.UseVisualStyleBackColor = true;
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label25.Location = new System.Drawing.Point(239, 104);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(24, 30);
			this.label25.TabIndex = 147;
			this.label25.Text = "𝅘𝅥𝅮";
			this.label25.Visible = false;
			// 
			// chkBars
			// 
			this.chkBars.AutoSize = true;
			this.chkBars.Checked = true;
			this.chkBars.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBars.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkBars.Location = new System.Drawing.Point(24, 3);
			this.chkBars.Name = "chkBars";
			this.chkBars.Size = new System.Drawing.Size(100, 19);
			this.chkBars.TabIndex = 139;
			this.chkBars.Tag = "0";
			this.chkBars.Text = "Bars Whole Notes";
			this.chkBars.UseVisualStyleBackColor = true;
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label24.Location = new System.Drawing.Point(224, 77);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(21, 30);
			this.label24.TabIndex = 146;
			this.label24.Text = "𝅘𝅥";
			this.label24.Visible = false;
			// 
			// chkBeatsFull
			// 
			this.chkBeatsFull.AutoSize = true;
			this.chkBeatsFull.Checked = true;
			this.chkBeatsFull.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBeatsFull.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkBeatsFull.Location = new System.Drawing.Point(24, 24);
			this.chkBeatsFull.Name = "chkBeatsFull";
			this.chkBeatsFull.Size = new System.Drawing.Size(127, 19);
			this.chkBeatsFull.TabIndex = 143;
			this.chkBeatsFull.Tag = "1";
			this.chkBeatsFull.Text = "Full Beats Quarter Notes";
			this.chkBeatsFull.UseVisualStyleBackColor = true;
			// 
			// chkBeatsHalf
			// 
			this.chkBeatsHalf.AutoSize = true;
			this.chkBeatsHalf.Checked = true;
			this.chkBeatsHalf.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBeatsHalf.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkBeatsHalf.Location = new System.Drawing.Point(24, 45);
			this.chkBeatsHalf.Name = "chkBeatsHalf";
			this.chkBeatsHalf.Size = new System.Drawing.Size(122, 19);
			this.chkBeatsHalf.TabIndex = 142;
			this.chkBeatsHalf.Tag = "2";
			this.chkBeatsHalf.Text = "Half Beats Eighth Notes";
			this.chkBeatsHalf.UseVisualStyleBackColor = true;
			// 
			// chkBeatsQuarter
			// 
			this.chkBeatsQuarter.AutoSize = true;
			this.chkBeatsQuarter.Checked = true;
			this.chkBeatsQuarter.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBeatsQuarter.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkBeatsQuarter.Location = new System.Drawing.Point(24, 87);
			this.chkBeatsQuarter.Name = "chkBeatsQuarter";
			this.chkBeatsQuarter.Size = new System.Drawing.Size(150, 19);
			this.chkBeatsQuarter.TabIndex = 140;
			this.chkBeatsQuarter.Tag = "4";
			this.chkBeatsQuarter.Text = "Quarter Beats Sixteenth Notes";
			this.chkBeatsQuarter.UseVisualStyleBackColor = true;
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label22.Location = new System.Drawing.Point(213, 58);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(19, 30);
			this.label22.TabIndex = 145;
			this.label22.Text = "𝄁";
			this.label22.Visible = false;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Font = new System.Drawing.Font("Segoe UI Symbol", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label23.Location = new System.Drawing.Point(230, 40);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(33, 45);
			this.label23.TabIndex = 146;
			this.label23.Text = "𝅗";
			this.label23.Visible = false;
			// 
			// chkWhiteBarBeats
			// 
			this.chkWhiteBarBeats.AutoSize = true;
			this.chkWhiteBarBeats.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkWhiteBarBeats.Location = new System.Drawing.Point(6, 152);
			this.chkWhiteBarBeats.Name = "chkWhiteBarBeats";
			this.chkWhiteBarBeats.Size = new System.Drawing.Size(89, 17);
			this.chkWhiteBarBeats.TabIndex = 154;
			this.chkWhiteBarBeats.Tag = "13";
			this.chkWhiteBarBeats.Text = "\"Whitening\"";
			this.chkWhiteBarBeats.UseVisualStyleBackColor = true;
			// 
			// lblDetectBarBeats
			// 
			this.lblDetectBarBeats.AutoSize = true;
			this.lblDetectBarBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDetectBarBeats.Location = new System.Drawing.Point(7, 109);
			this.lblDetectBarBeats.Name = "lblDetectBarBeats";
			this.lblDetectBarBeats.Size = new System.Drawing.Size(95, 13);
			this.lblDetectBarBeats.TabIndex = 153;
			this.lblDetectBarBeats.Text = "Detection Method:";
			// 
			// cboDetectBarBeats
			// 
			this.cboDetectBarBeats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDetectBarBeats.DropDownWidth = 300;
			this.cboDetectBarBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboDetectBarBeats.FormattingEnabled = true;
			this.cboDetectBarBeats.Items.AddRange(new object[] {
            "\'Complex Domain\' (Strings/Mixed: Piano, Guitar)",
            "\'Spectral Difference\' (Percussion: Drums, Chimes)",
            "\'Phase Deviation\' (Wind: Flute, Sax, Trumpet)",
            "\'Broadband Energy Rise\' (Percussion mixed with other)"});
			this.cboDetectBarBeats.Location = new System.Drawing.Point(6, 125);
			this.cboDetectBarBeats.Name = "cboDetectBarBeats";
			this.cboDetectBarBeats.Size = new System.Drawing.Size(135, 21);
			this.cboDetectBarBeats.TabIndex = 152;
			// 
			// lblMethod
			// 
			this.lblMethod.AutoSize = true;
			this.lblMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMethod.Location = new System.Drawing.Point(6, 16);
			this.lblMethod.Name = "lblMethod";
			this.lblMethod.Size = new System.Drawing.Size(86, 13);
			this.lblMethod.TabIndex = 144;
			this.lblMethod.Text = "Plugin / Method:";
			// 
			// cboMethodBarsBeats
			// 
			this.cboMethodBarsBeats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodBarsBeats.DropDownWidth = 200;
			this.cboMethodBarsBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboMethodBarsBeats.FormattingEnabled = true;
			this.cboMethodBarsBeats.Items.AddRange(new object[] {
            "Queen Mary Bar and Beat Tracker",
            "Queen Mary Tempo and Beat Tracker",
            "BeatRoot Beat Tracker",
            "Porto Beat Tracker",
            "Aubio Beat Tracker"});
			this.cboMethodBarsBeats.Location = new System.Drawing.Point(6, 32);
			this.cboMethodBarsBeats.Name = "cboMethodBarsBeats";
			this.cboMethodBarsBeats.Size = new System.Drawing.Size(135, 21);
			this.cboMethodBarsBeats.TabIndex = 143;
			this.cboMethodBarsBeats.SelectedIndexChanged += new System.EventHandler(this.cboBarBeatMethod_SelectedIndexChanged);
			// 
			// lblAlignBarsBeats
			// 
			this.lblAlignBarsBeats.AutoSize = true;
			this.lblAlignBarsBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAlignBarsBeats.Location = new System.Drawing.Point(6, 301);
			this.lblAlignBarsBeats.Name = "lblAlignBarsBeats";
			this.lblAlignBarsBeats.Size = new System.Drawing.Size(49, 13);
			this.lblAlignBarsBeats.TabIndex = 142;
			this.lblAlignBarsBeats.Text = "Align To:";
			// 
			// cboAlignBarsBeats
			// 
			this.cboAlignBarsBeats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignBarsBeats.DropDownWidth = 135;
			this.cboAlignBarsBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboAlignBarsBeats.FormattingEnabled = true;
			this.cboAlignBarsBeats.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps"});
			this.cboAlignBarsBeats.Location = new System.Drawing.Point(6, 317);
			this.cboAlignBarsBeats.Name = "cboAlignBarsBeats";
			this.cboAlignBarsBeats.Size = new System.Drawing.Size(135, 21);
			this.cboAlignBarsBeats.TabIndex = 141;
			// 
			// pnlTrackBeatsX
			// 
			this.pnlTrackBeatsX.Controls.Add(this.vscStartBeat);
			this.pnlTrackBeatsX.Controls.Add(this.txtStartBeat);
			this.pnlTrackBeatsX.Controls.Add(this.lblTrackBeatsX);
			this.pnlTrackBeatsX.Location = new System.Drawing.Point(6, 82);
			this.pnlTrackBeatsX.Name = "pnlTrackBeatsX";
			this.pnlTrackBeatsX.Size = new System.Drawing.Size(101, 23);
			this.pnlTrackBeatsX.TabIndex = 140;
			// 
			// vscStartBeat
			// 
			this.vscStartBeat.LargeChange = 1;
			this.vscStartBeat.Location = new System.Drawing.Point(67, -3);
			this.vscStartBeat.Maximum = 4;
			this.vscStartBeat.Minimum = 1;
			this.vscStartBeat.Name = "vscStartBeat";
			this.vscStartBeat.Size = new System.Drawing.Size(16, 18);
			this.vscStartBeat.TabIndex = 28;
			this.vscStartBeat.Value = 1;
			this.vscStartBeat.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vscStartBeat_Scroll_1);
			// 
			// txtStartBeat
			// 
			this.txtStartBeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtStartBeat.Location = new System.Drawing.Point(54, 0);
			this.txtStartBeat.Name = "txtStartBeat";
			this.txtStartBeat.ReadOnly = true;
			this.txtStartBeat.Size = new System.Drawing.Size(13, 20);
			this.txtStartBeat.TabIndex = 7;
			this.txtStartBeat.Text = "1";
			// 
			// lblTrackBeatsX
			// 
			this.lblTrackBeatsX.AutoSize = true;
			this.lblTrackBeatsX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTrackBeatsX.Location = new System.Drawing.Point(1, 4);
			this.lblTrackBeatsX.Name = "lblTrackBeatsX";
			this.lblTrackBeatsX.Size = new System.Drawing.Size(54, 13);
			this.lblTrackBeatsX.TabIndex = 6;
			this.lblTrackBeatsX.Text = "Start Beat";
			this.lblTrackBeatsX.UseMnemonic = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.swTrackBeat);
			this.panel1.Controls.Add(this.lblTrackBeat34);
			this.panel1.Controls.Add(this.lblTrackBeat44);
			this.panel1.Location = new System.Drawing.Point(6, 59);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(127, 17);
			this.panel1.TabIndex = 139;
			// 
			// swTrackBeat
			// 
			this.swTrackBeat.Location = new System.Drawing.Point(46, 1);
			this.swTrackBeat.Name = "swTrackBeat";
			this.swTrackBeat.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.swTrackBeat.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.swTrackBeat.Size = new System.Drawing.Size(32, 16);
			this.swTrackBeat.Style = JCS.ToggleSwitch.ToggleSwitchStyle.OSX;
			this.swTrackBeat.TabIndex = 132;
			// 
			// lblTrackBeat34
			// 
			this.lblTrackBeat34.AutoSize = true;
			this.lblTrackBeat34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTrackBeat34.Location = new System.Drawing.Point(80, 2);
			this.lblTrackBeat34.Name = "lblTrackBeat34";
			this.lblTrackBeat34.Size = new System.Drawing.Size(46, 13);
			this.lblTrackBeat34.TabIndex = 128;
			this.lblTrackBeat34.Text = "3/4 time";
			// 
			// lblTrackBeat44
			// 
			this.lblTrackBeat44.AutoSize = true;
			this.lblTrackBeat44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTrackBeat44.Location = new System.Drawing.Point(1, 2);
			this.lblTrackBeat44.Name = "lblTrackBeat44";
			this.lblTrackBeat44.Size = new System.Drawing.Size(46, 13);
			this.lblTrackBeat44.TabIndex = 127;
			this.lblTrackBeat44.Text = "4/4 time";
			// 
			// lblStep2B
			// 
			this.lblStep2B.AutoSize = true;
			this.lblStep2B.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStep2B.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep2B.Location = new System.Drawing.Point(6, -5);
			this.lblStep2B.Name = "lblStep2B";
			this.lblStep2B.Size = new System.Drawing.Size(21, 24);
			this.lblStep2B.TabIndex = 121;
			this.lblStep2B.Text = "2";
			// 
			// chkChords
			// 
			this.chkChords.AutoSize = true;
			this.chkChords.Enabled = false;
			this.chkChords.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkChords.Location = new System.Drawing.Point(508, 398);
			this.chkChords.Name = "chkChords";
			this.chkChords.Size = new System.Drawing.Size(63, 17);
			this.chkChords.TabIndex = 126;
			this.chkChords.Tag = "13";
			this.chkChords.Text = "Chords";
			this.chkChords.UseVisualStyleBackColor = true;
			this.chkChords.CheckedChanged += new System.EventHandler(this.chkTiming_CheckedChanged);
			// 
			// chkFlux
			// 
			this.chkFlux.AutoSize = true;
			this.chkFlux.Enabled = false;
			this.chkFlux.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkFlux.Location = new System.Drawing.Point(508, 375);
			this.chkFlux.Name = "chkFlux";
			this.chkFlux.Size = new System.Drawing.Size(47, 17);
			this.chkFlux.TabIndex = 128;
			this.chkFlux.Tag = "12";
			this.chkFlux.Text = "Flux";
			this.chkFlux.UseVisualStyleBackColor = true;
			this.chkFlux.CheckedChanged += new System.EventHandler(this.chkTiming_CheckedChanged);
			// 
			// grpAudio
			// 
			this.grpAudio.Controls.Add(this.btnBrowseAudio);
			this.grpAudio.Controls.Add(this.txtFileAudio);
			this.grpAudio.Controls.Add(this.lblFileAudio);
			this.grpAudio.Controls.Add(this.lblStep1);
			this.grpAudio.Enabled = false;
			this.grpAudio.Location = new System.Drawing.Point(12, 12);
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
			// lblStep1
			// 
			this.lblStep1.AutoSize = true;
			this.lblStep1.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStep1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep1.Location = new System.Drawing.Point(6, -5);
			this.lblStep1.Name = "lblStep1";
			this.lblStep1.Size = new System.Drawing.Size(21, 24);
			this.lblStep1.TabIndex = 122;
			this.lblStep1.Text = "1";
			// 
			// grpSave
			// 
			this.grpSave.Controls.Add(this.panel2);
			this.grpSave.Controls.Add(this.btnSaveOptions);
			this.grpSave.Controls.Add(this.lblStep4);
			this.grpSave.Controls.Add(this.label4);
			this.grpSave.Controls.Add(this.btnSave);
			this.grpSave.Controls.Add(this.txtSaveName);
			this.grpSave.Enabled = false;
			this.grpSave.Location = new System.Drawing.Point(12, 452);
			this.grpSave.Name = "grpSave";
			this.grpSave.Size = new System.Drawing.Size(334, 111);
			this.grpSave.TabIndex = 123;
			this.grpSave.TabStop = false;
			this.grpSave.Text = "      Save Timings";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.optOnePer);
			this.panel2.Controls.Add(this.optMultiPer);
			this.panel2.Location = new System.Drawing.Point(19, 19);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(277, 26);
			this.panel2.TabIndex = 136;
			// 
			// optOnePer
			// 
			this.optOnePer.AutoSize = true;
			this.optOnePer.Checked = true;
			this.optOnePer.Location = new System.Drawing.Point(128, 5);
			this.optOnePer.Name = "optOnePer";
			this.optOnePer.Size = new System.Drawing.Size(134, 18);
			this.optOnePer.TabIndex = 139;
			this.optOnePer.TabStop = true;
			this.optOnePer.Text = "Individual Timing Files";
			this.optOnePer.UseCompatibleTextRendering = true;
			this.optOnePer.UseVisualStyleBackColor = true;
			// 
			// optMultiPer
			// 
			this.optMultiPer.AutoSize = true;
			this.optMultiPer.Enabled = false;
			this.optMultiPer.Location = new System.Drawing.Point(3, 5);
			this.optMultiPer.Name = "optMultiPer";
			this.optMultiPer.Size = new System.Drawing.Size(112, 18);
			this.optMultiPer.TabIndex = 138;
			this.optMultiPer.Text = "Single Timing File";
			this.optMultiPer.UseCompatibleTextRendering = true;
			this.optMultiPer.UseVisualStyleBackColor = true;
			// 
			// btnSaveOptions
			// 
			this.btnSaveOptions.AllowDrop = true;
			this.btnSaveOptions.Location = new System.Drawing.Point(189, 96);
			this.btnSaveOptions.Name = "btnSaveOptions";
			this.btnSaveOptions.Size = new System.Drawing.Size(75, 23);
			this.btnSaveOptions.TabIndex = 129;
			this.btnSaveOptions.Text = "Settings...";
			this.btnSaveOptions.UseVisualStyleBackColor = true;
			this.btnSaveOptions.Visible = false;
			this.btnSaveOptions.Click += new System.EventHandler(this.btnSaveOptions_Click_1);
			// 
			// lblStep4
			// 
			this.lblStep4.AutoSize = true;
			this.lblStep4.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStep4.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep4.Location = new System.Drawing.Point(6, -5);
			this.lblStep4.Name = "lblStep4";
			this.lblStep4.Size = new System.Drawing.Size(21, 24);
			this.lblStep4.TabIndex = 127;
			this.lblStep4.Text = "4";
			// 
			// label4
			// 
			this.label4.AllowDrop = true;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(97, 13);
			this.label4.TabIndex = 126;
			this.label4.Text = "Timing Filename(s):";
			// 
			// btnSave
			// 
			this.btnSave.AllowDrop = true;
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(244, 64);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 125;
			this.btnSave.Text = "Save As...";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// txtSaveName
			// 
			this.txtSaveName.AllowDrop = true;
			this.txtSaveName.Location = new System.Drawing.Point(19, 67);
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
			// grpAnalyze
			// 
			this.grpAnalyze.Controls.Add(this.btnOK);
			this.grpAnalyze.Controls.Add(this.lblStep3);
			this.grpAnalyze.Enabled = false;
			this.grpAnalyze.Location = new System.Drawing.Point(12, 378);
			this.grpAnalyze.Name = "grpAnalyze";
			this.grpAnalyze.Size = new System.Drawing.Size(334, 66);
			this.grpAnalyze.TabIndex = 126;
			this.grpAnalyze.TabStop = false;
			this.grpAnalyze.Text = "      Analyze the Audio File";
			// 
			// btnOK
			// 
			this.btnOK.AllowDrop = true;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOK.Location = new System.Drawing.Point(89, 19);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(117, 41);
			this.btnOK.TabIndex = 124;
			this.btnOK.Text = "Analyze...";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblStep3
			// 
			this.lblStep3.AutoSize = true;
			this.lblStep3.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStep3.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep3.Location = new System.Drawing.Point(6, -5);
			this.lblStep3.Name = "lblStep3";
			this.lblStep3.Size = new System.Drawing.Size(21, 24);
			this.lblStep3.TabIndex = 122;
			this.lblStep3.Text = "3";
			// 
			// pnlVamping
			// 
			this.pnlVamping.Controls.Add(this.lblSongName);
			this.pnlVamping.Controls.Add(this.lblAnalyzing);
			this.pnlVamping.Controls.Add(this.lblWait);
			this.pnlVamping.Controls.Add(this.label1);
			this.pnlVamping.Location = new System.Drawing.Point(12, 226);
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
			this.lblSongName.Location = new System.Drawing.Point(5, 33);
			this.lblSongName.Name = "lblSongName";
			this.lblSongName.Size = new System.Drawing.Size(179, 20);
			this.lblSongName.TabIndex = 3;
			this.lblSongName.Text = "Blue Christmas by Elvis Presley";
			this.lblSongName.Click += new System.EventHandler(this.lblSongName_Click);
			// 
			// lblAnalyzing
			// 
			this.lblAnalyzing.AutoSize = true;
			this.lblAnalyzing.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAnalyzing.ForeColor = System.Drawing.Color.Red;
			this.lblAnalyzing.Location = new System.Drawing.Point(5, 7);
			this.lblAnalyzing.Name = "lblAnalyzing";
			this.lblAnalyzing.Size = new System.Drawing.Size(189, 24);
			this.lblAnalyzing.TabIndex = 2;
			this.lblAnalyzing.Text = "Analyzing your song";
			// 
			// lblWait
			// 
			this.lblWait.AutoSize = true;
			this.lblWait.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWait.ForeColor = System.Drawing.Color.Red;
			this.lblWait.Location = new System.Drawing.Point(60, 53);
			this.lblWait.Name = "lblWait";
			this.lblWait.Size = new System.Drawing.Size(75, 15);
			this.lblWait.TabIndex = 1;
			this.lblWait.Text = "Please wait...";
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
			// grpOptions
			// 
			this.grpOptions.Controls.Add(this.label20);
			this.grpOptions.Controls.Add(this.picVampire);
			this.grpOptions.Controls.Add(this.lblHelpOnsets);
			this.grpOptions.Controls.Add(this.pictureBox1);
			this.grpOptions.Controls.Add(this.lblStep2A);
			this.grpOptions.Enabled = false;
			this.grpOptions.Location = new System.Drawing.Point(12, 89);
			this.grpOptions.Name = "grpOptions";
			this.grpOptions.Size = new System.Drawing.Size(397, 281);
			this.grpOptions.TabIndex = 130;
			this.grpOptions.TabStop = false;
			this.grpOptions.Text = "      Select Timings and Options";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label20.ForeColor = System.Drawing.Color.DarkViolet;
			this.label20.Location = new System.Drawing.Point(131, 67);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(162, 24);
			this.label20.TabIndex = 130;
			this.label20.Text = "Pick Your Poison";
			// 
			// picVampire
			// 
			this.picVampire.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picVampire.BackgroundImage")));
			this.picVampire.Location = new System.Drawing.Point(22, 44);
			this.picVampire.Name = "picVampire";
			this.picVampire.Size = new System.Drawing.Size(128, 192);
			this.picVampire.TabIndex = 136;
			this.picVampire.TabStop = false;
			// 
			// lblHelpOnsets
			// 
			this.lblHelpOnsets.AutoSize = true;
			this.lblHelpOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHelpOnsets.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblHelpOnsets.Location = new System.Drawing.Point(226, 214);
			this.lblHelpOnsets.Name = "lblHelpOnsets";
			this.lblHelpOnsets.Size = new System.Drawing.Size(38, 13);
			this.lblHelpOnsets.TabIndex = 156;
			this.lblHelpOnsets.Text = "Help...";
			this.lblHelpOnsets.Click += new System.EventHandler(this.lblHelpOnsets_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
			this.pictureBox1.Location = new System.Drawing.Point(217, 99);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 32);
			this.pictureBox1.TabIndex = 131;
			this.pictureBox1.TabStop = false;
			// 
			// lblStep2A
			// 
			this.lblStep2A.AutoSize = true;
			this.lblStep2A.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStep2A.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep2A.Location = new System.Drawing.Point(6, -5);
			this.lblStep2A.Name = "lblStep2A";
			this.lblStep2A.Size = new System.Drawing.Size(21, 24);
			this.lblStep2A.TabIndex = 122;
			this.lblStep2A.Text = "2";
			// 
			// chkReuse
			// 
			this.chkReuse.AutoSize = true;
			this.chkReuse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkReuse.ForeColor = System.Drawing.Color.DeepPink;
			this.chkReuse.Location = new System.Drawing.Point(119, 338);
			this.chkReuse.Name = "chkReuse";
			this.chkReuse.Size = new System.Drawing.Size(81, 17);
			this.chkReuse.TabIndex = 135;
			this.chkReuse.Text = "Re-use files";
			this.chkReuse.UseVisualStyleBackColor = true;
			this.chkReuse.Visible = false;
			this.chkReuse.CheckedChanged += new System.EventHandler(this.chkReuse_CheckedChanged);
			// 
			// tmrAni
			// 
			this.tmrAni.Tick += new System.EventHandler(this.tmrAni_Tick);
			// 
			// chk15fps
			// 
			this.chk15fps.AutoSize = true;
			this.chk15fps.Enabled = false;
			this.chk15fps.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chk15fps.Location = new System.Drawing.Point(469, 240);
			this.chk15fps.Name = "chk15fps";
			this.chk15fps.Size = new System.Drawing.Size(153, 17);
			this.chk15fps.TabIndex = 159;
			this.chk15fps.Tag = "12";
			this.chk15fps.Text = "6.67 centiseconds 15 FPS";
			this.chk15fps.UseVisualStyleBackColor = true;
			// 
			// chk30fps
			// 
			this.chk30fps.AutoSize = true;
			this.chk30fps.Enabled = false;
			this.chk30fps.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chk30fps.Location = new System.Drawing.Point(469, 208);
			this.chk30fps.Name = "chk30fps";
			this.chk30fps.Size = new System.Drawing.Size(153, 17);
			this.chk30fps.TabIndex = 160;
			this.chk30fps.Tag = "12";
			this.chk30fps.Text = "3.33 centiseconds 30 FPS";
			this.chk30fps.UseVisualStyleBackColor = true;
			// 
			// chk24fps
			// 
			this.chk24fps.AutoSize = true;
			this.chk24fps.Enabled = false;
			this.chk24fps.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chk24fps.Location = new System.Drawing.Point(469, 224);
			this.chk24fps.Name = "chk24fps";
			this.chk24fps.Size = new System.Drawing.Size(153, 17);
			this.chk24fps.TabIndex = 161;
			this.chk24fps.Tag = "12";
			this.chk24fps.Text = "4.16 centiseconds 24 FPS";
			this.chk24fps.UseVisualStyleBackColor = true;
			// 
			// frmTune
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(990, 595);
			this.Controls.Add(this.chkReuse);
			this.Controls.Add(this.grpAnalyze);
			this.Controls.Add(this.grpSave);
			this.Controls.Add(this.grpAudio);
			this.Controls.Add(this.pnlVamping);
			this.Controls.Add(this.grpTimings);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.grpOptions);
			this.Controls.Add(this.menuStrip1);
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
			this.grpTimings.ResumeLayout(false);
			this.grpTimings.PerformLayout();
			this.grpVocals.ResumeLayout(false);
			this.grpVocals.PerformLayout();
			this.grpSegments.ResumeLayout(false);
			this.grpSegments.PerformLayout();
			this.grpTempo.ResumeLayout(false);
			this.grpTempo.PerformLayout();
			this.grpPitchKey.ResumeLayout(false);
			this.grpPitchKey.PerformLayout();
			this.grpSpectrum.ResumeLayout(false);
			this.grpSpectrum.PerformLayout();
			this.grpTranscription.ResumeLayout(false);
			this.grpTranscription.PerformLayout();
			this.grpOnsets.ResumeLayout(false);
			this.grpOnsets.PerformLayout();
			this.pnlOnsetSensitivity.ResumeLayout(false);
			this.pnlOnsetSensitivity.PerformLayout();
			this.grpBarsBeats.ResumeLayout(false);
			this.grpBarsBeats.PerformLayout();
			this.pnlNotes.ResumeLayout(false);
			this.pnlNotes.PerformLayout();
			this.pnlTrackBeatsX.ResumeLayout(false);
			this.pnlTrackBeatsX.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.grpAudio.ResumeLayout(false);
			this.grpAudio.PerformLayout();
			this.grpSave.ResumeLayout(false);
			this.grpSave.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.grpAnalyze.ResumeLayout(false);
			this.grpAnalyze.PerformLayout();
			this.pnlVamping.ResumeLayout(false);
			this.pnlVamping.PerformLayout();
			this.grpOptions.ResumeLayout(false);
			this.grpOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picVampire)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
		private System.Windows.Forms.GroupBox grpTimings;
		private System.Windows.Forms.Label lblStep2B;
		private System.Windows.Forms.Button btnTrackSettings;
		private System.Windows.Forms.GroupBox grpAudio;
		private System.Windows.Forms.Button btnBrowseAudio;
		private System.Windows.Forms.TextBox txtFileAudio;
		private System.Windows.Forms.Label lblFileAudio;
		private System.Windows.Forms.Label lblStep1;
		private System.Windows.Forms.GroupBox grpSave;
		private System.Windows.Forms.Button btnSaveOptions;
		private System.Windows.Forms.Label lblStep4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox txtSaveName;
		private System.Windows.Forms.CheckBox chkFlux;
		private System.Windows.Forms.CheckBox chkChords;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.GroupBox grpAnalyze;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblStep3;
		private System.Windows.Forms.CheckBox chkChromagram;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RadioButton optOnePer;
		private System.Windows.Forms.RadioButton optMultiPer;
		private System.Windows.Forms.GroupBox grpSpectrum;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cboLabelsSpectrum;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox cboMethodSpectrum;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox cboAlignSpectrum;
		private System.Windows.Forms.GroupBox grpTranscription;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cboLabelsTranscription;
		private System.Windows.Forms.CheckBox chkTranscribe;
		private System.Windows.Forms.ComboBox cboMethodTranscription;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cboAlignTranscribe;
		private System.Windows.Forms.GroupBox grpOnsets;
		private System.Windows.Forms.Label lblDetectOnsets;
		private System.Windows.Forms.ComboBox cboDetectOnsets;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cboLabelsOnsets;
		private System.Windows.Forms.CheckBox chkNoteOnsets;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboMethodOnsets;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cboAlignOnsets;
		private System.Windows.Forms.GroupBox grpBarsBeats;
		private System.Windows.Forms.Label lblMethod;
		private System.Windows.Forms.ComboBox cboMethodBarsBeats;
		private System.Windows.Forms.Label lblAlignBarsBeats;
		private System.Windows.Forms.ComboBox cboAlignBarsBeats;
		private System.Windows.Forms.Panel pnlTrackBeatsX;
		private System.Windows.Forms.VScrollBar vscStartBeat;
		private System.Windows.Forms.TextBox txtStartBeat;
		private System.Windows.Forms.Label lblTrackBeatsX;
		private System.Windows.Forms.Panel panel1;
		private JCS.ToggleSwitch swTrackBeat;
		private System.Windows.Forms.Label lblTrackBeat34;
		private System.Windows.Forms.Label lblTrackBeat44;
		private System.Windows.Forms.Panel pnlVamping;
		private System.Windows.Forms.Label lblSongName;
		private System.Windows.Forms.Label lblAnalyzing;
		private System.Windows.Forms.Label lblWait;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkWhiteOnsets;
		private System.Windows.Forms.Panel pnlOnsetSensitivity;
		private System.Windows.Forms.VScrollBar vscSensitivity;
		private System.Windows.Forms.TextBox txtOnsetSensitivity;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox grpVocals;
		private System.Windows.Forms.CheckBox chkVocals;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.ComboBox cboAlignVocals;
		private System.Windows.Forms.GroupBox grpSegments;
		private System.Windows.Forms.CheckBox chkSegments;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.ComboBox cboAlignSegments;
		private System.Windows.Forms.GroupBox grpTempo;
		private System.Windows.Forms.CheckBox chkTempo;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.ComboBox cboLabelsTempo;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.ComboBox cboMethodTempo;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.ComboBox cboAlignTempo;
		private System.Windows.Forms.GroupBox grpPitchKey;
		private System.Windows.Forms.CheckBox chkPitchKey;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox cboLabelsPitchKey;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ComboBox cboMethodPitchKey;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.ComboBox cboAlignPitch;
		private System.Windows.Forms.CheckBox chkSpectrum;
		private System.Windows.Forms.GroupBox grpOptions;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label lblStep2A;
		private System.Windows.Forms.CheckBox chkWhiteBarBeats;
		private System.Windows.Forms.Label lblDetectBarBeats;
		private System.Windows.Forms.ComboBox cboDetectBarBeats;
		private System.Windows.Forms.Panel pnlNotes;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.CheckBox chkBeatsThird;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.CheckBox chkBars;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.CheckBox chkBeatsFull;
		private System.Windows.Forms.CheckBox chkBeatsHalf;
		private System.Windows.Forms.CheckBox chkBeatsQuarter;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label lblHelpOnsets;
		private System.Windows.Forms.Label labelTweaks;
		private System.Windows.Forms.CheckBox chkReuse;
		private System.Windows.Forms.Button btnResetDefaults;
		private System.Windows.Forms.PictureBox picVampire;
		private System.Windows.Forms.CheckBox chkBarsBeats;
		private System.Windows.Forms.Timer tmrAni;
		private System.Windows.Forms.CheckBox chk24fps;
		private System.Windows.Forms.CheckBox chk30fps;
		private System.Windows.Forms.CheckBox chk15fps;
	}
}

