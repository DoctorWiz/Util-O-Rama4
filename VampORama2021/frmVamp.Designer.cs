namespace UtilORama4
{
	partial class frmVamp
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVamp));
			this.ttip = new System.Windows.Forms.ToolTip(this.components);
			this.pnlTrackBeatsX = new System.Windows.Forms.Panel();
			this.vscStartBeat = new System.Windows.Forms.VScrollBar();
			this.txtStartBeat = new System.Windows.Forms.TextBox();
			this.lblTrackBeatsX = new System.Windows.Forms.Label();
			this.btnExploreVamp = new System.Windows.Forms.Button();
			this.btnExplorexLights = new System.Windows.Forms.Button();
			this.btnExploreTemp = new System.Windows.Forms.Button();
			this.btnCmdTemp = new System.Windows.Forms.Button();
			this.btnLaunchxLights = new System.Windows.Forms.Button();
			this.btnSequenceEditor = new System.Windows.Forms.Button();
			this.btnExploreLOR = new System.Windows.Forms.Button();
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
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
			this.grpChords = new System.Windows.Forms.GroupBox();
			this.chkChords = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cboLabelsChords = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cboMethodChords = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cboAlignChords = new System.Windows.Forms.ComboBox();
			this.grbGlobal = new System.Windows.Forms.GroupBox();
			this.lblStepSize = new System.Windows.Forms.Label();
			this.cboStepSize = new System.Windows.Forms.ComboBox();
			this.pnlBeatFade = new System.Windows.Forms.Panel();
			this.swRamps = new JCS.ToggleSwitch();
			this.lblBarsRampFade = new System.Windows.Forms.Label();
			this.lblBarsOnOff = new System.Windows.Forms.Label();
			this.chkWhiten = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.swTrackBeat = new JCS.ToggleSwitch();
			this.lblTrackBeat34 = new System.Windows.Forms.Label();
			this.lblTrackBeat44 = new System.Windows.Forms.Label();
			this.lblNote3Third = new System.Windows.Forms.Label();
			this.lblNote1Bars = new System.Windows.Forms.Label();
			this.lblWorkFolder = new System.Windows.Forms.Label();
			this.grpPlatform = new System.Windows.Forms.GroupBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.chkxLights = new System.Windows.Forms.CheckBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.chkLOR = new System.Windows.Forms.CheckBox();
			this.lbllabel = new System.Windows.Forms.Label();
			this.chk24fps = new System.Windows.Forms.CheckBox();
			this.chk30fps = new System.Windows.Forms.CheckBox();
			this.lblTweaks = new System.Windows.Forms.Label();
			this.chk15fps = new System.Windows.Forms.CheckBox();
			this.grpVocals = new System.Windows.Forms.GroupBox();
			this.chkVocals = new System.Windows.Forms.CheckBox();
			this.lblVocalsAlign = new System.Windows.Forms.Label();
			this.cboAlignVocals = new System.Windows.Forms.ComboBox();
			this.lblTrans2Options = new System.Windows.Forms.Label();
			this.grpSegments = new System.Windows.Forms.GroupBox();
			this.lblSegmentsLabels = new System.Windows.Forms.Label();
			this.cboLabelsSegments = new System.Windows.Forms.ComboBox();
			this.lblSegmentsPlugin = new System.Windows.Forms.Label();
			this.cboMethodSegments = new System.Windows.Forms.ComboBox();
			this.chkSegments = new System.Windows.Forms.CheckBox();
			this.lblSegmentsAlign = new System.Windows.Forms.Label();
			this.cboAlignSegments = new System.Windows.Forms.ComboBox();
			this.button3 = new System.Windows.Forms.Button();
			this.grpTempo = new System.Windows.Forms.GroupBox();
			this.chkTempo = new System.Windows.Forms.CheckBox();
			this.lblTempoLabels = new System.Windows.Forms.Label();
			this.cboLabelsTempo = new System.Windows.Forms.ComboBox();
			this.lblTempoPlugin = new System.Windows.Forms.Label();
			this.cboMethodTempo = new System.Windows.Forms.ComboBox();
			this.lblTempoAlign = new System.Windows.Forms.Label();
			this.cboAlignTempo = new System.Windows.Forms.ComboBox();
			this.grpPoly2 = new System.Windows.Forms.GroupBox();
			this.lblTrans2Plugin = new System.Windows.Forms.Label();
			this.lblTrans2Labels = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.lblTrans2Align = new System.Windows.Forms.Label();
			this.cboAlignFoo = new System.Windows.Forms.ComboBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.btnTrackSettings = new System.Windows.Forms.Button();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.grpPitchKey = new System.Windows.Forms.GroupBox();
			this.chkPitchKey = new System.Windows.Forms.CheckBox();
			this.lblKeyLabels = new System.Windows.Forms.Label();
			this.cboLabelsPitchKey = new System.Windows.Forms.ComboBox();
			this.lblKeyPlugin = new System.Windows.Forms.Label();
			this.cboMethodPitchKey = new System.Windows.Forms.ComboBox();
			this.lblKeyAlign = new System.Windows.Forms.Label();
			this.cboAlignPitchKey = new System.Windows.Forms.ComboBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.grpChromagram = new System.Windows.Forms.GroupBox();
			this.chkChromagram = new System.Windows.Forms.CheckBox();
			this.lblSpectrumLabels = new System.Windows.Forms.Label();
			this.cboLabelsChromagram = new System.Windows.Forms.ComboBox();
			this.lblSpectrumPlugin = new System.Windows.Forms.Label();
			this.cboMethodChromagram = new System.Windows.Forms.ComboBox();
			this.lblSpectrumAlign = new System.Windows.Forms.Label();
			this.cboAlignChromagram = new System.Windows.Forms.ComboBox();
			this.grpPolyphonic = new System.Windows.Forms.GroupBox();
			this.lblTranscribePlugin = new System.Windows.Forms.Label();
			this.lblTranscribeLabels = new System.Windows.Forms.Label();
			this.cboLabelsPolyphonic = new System.Windows.Forms.ComboBox();
			this.chkPolyphonic = new System.Windows.Forms.CheckBox();
			this.cboMethodPolyphonic = new System.Windows.Forms.ComboBox();
			this.lblTranscribeAlign = new System.Windows.Forms.Label();
			this.cboAlignPolyphonic = new System.Windows.Forms.ComboBox();
			this.chkChromathing = new System.Windows.Forms.CheckBox();
			this.grpOnsets = new System.Windows.Forms.GroupBox();
			this.pnlOnsetSensitivity = new System.Windows.Forms.Panel();
			this.vscSensitivity = new System.Windows.Forms.VScrollBar();
			this.txtOnsetSensitivity = new System.Windows.Forms.TextBox();
			this.lblOnsetsSensitivity = new System.Windows.Forms.Label();
			this.lblDetectOnsets = new System.Windows.Forms.Label();
			this.cboOnsetsDetect = new System.Windows.Forms.ComboBox();
			this.lblOnsetsLabels = new System.Windows.Forms.Label();
			this.cboLabelsOnsets = new System.Windows.Forms.ComboBox();
			this.chkNoteOnsets = new System.Windows.Forms.CheckBox();
			this.lblOnsetsPlugin = new System.Windows.Forms.Label();
			this.cboMethodOnsets = new System.Windows.Forms.ComboBox();
			this.lblOnsetsAlign = new System.Windows.Forms.Label();
			this.cboAlignOnsets = new System.Windows.Forms.ComboBox();
			this.grpBarsBeats = new System.Windows.Forms.GroupBox();
			this.lblDetectBarBeats = new System.Windows.Forms.Label();
			this.cboDetectBarBeats = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cboLabelsBarBeats = new System.Windows.Forms.ComboBox();
			this.chkBarsBeats = new System.Windows.Forms.CheckBox();
			this.pnlNotes = new System.Windows.Forms.Panel();
			this.chkBeatsThird = new System.Windows.Forms.CheckBox();
			this.chkBars = new System.Windows.Forms.CheckBox();
			this.chkBeatsFull = new System.Windows.Forms.CheckBox();
			this.chkBeatsHalf = new System.Windows.Forms.CheckBox();
			this.chkBeatsQuarter = new System.Windows.Forms.CheckBox();
			this.lblBeats2Half = new System.Windows.Forms.Label();
			this.lblBeats1Full = new System.Windows.Forms.Label();
			this.lblBeats4Quarter = new System.Windows.Forms.Label();
			this.lblBeats0Bars = new System.Windows.Forms.Label();
			this.lblMethod = new System.Windows.Forms.Label();
			this.cboMethodBarsBeats = new System.Windows.Forms.ComboBox();
			this.lblAlignBarsBeats = new System.Windows.Forms.Label();
			this.cboAlignBarBeats = new System.Windows.Forms.ComboBox();
			this.lblStep2B = new System.Windows.Forms.Label();
			this.chkFlux = new System.Windows.Forms.CheckBox();
			this.pnlVamping = new System.Windows.Forms.Panel();
			this.lblSongName = new System.Windows.Forms.Label();
			this.lblAnalyzing = new System.Windows.Forms.Label();
			this.lblWait = new System.Windows.Forms.Label();
			this.lblVampRed = new System.Windows.Forms.Label();
			this.grpExplore = new System.Windows.Forms.GroupBox();
			this.btnResetDefaults = new System.Windows.Forms.Button();
			this.grpAudio = new System.Windows.Forms.GroupBox();
			this.lblSongTime = new System.Windows.Forms.Label();
			this.btnBrowseAudio = new System.Windows.Forms.Button();
			this.txtFileAudio = new System.Windows.Forms.TextBox();
			this.lblFileAudio = new System.Windows.Forms.Label();
			this.lblStep1 = new System.Windows.Forms.Label();
			this.grpSavex = new System.Windows.Forms.GroupBox();
			this.picxLights = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.optOnePer = new System.Windows.Forms.RadioButton();
			this.optMultiPer = new System.Windows.Forms.RadioButton();
			this.btnSaveOptions = new System.Windows.Forms.Button();
			this.lblStep4A = new System.Windows.Forms.Label();
			this.lblFilexTimings = new System.Windows.Forms.Label();
			this.btnSavexL = new System.Windows.Forms.Button();
			this.txtSaveNamexL = new System.Windows.Forms.TextBox();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.grpAnalyze = new System.Windows.Forms.GroupBox();
			this.btnReanalyze = new System.Windows.Forms.Button();
			this.lblVampTime = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblStep3 = new System.Windows.Forms.Label();
			this.grpOptions = new System.Windows.Forms.GroupBox();
			this.picWorking = new System.Windows.Forms.PictureBox();
			this.lblPickYourPoison = new System.Windows.Forms.Label();
			this.chkReuse = new System.Windows.Forms.CheckBox();
			this.picVampire = new System.Windows.Forms.PictureBox();
			this.lblHelpOnsets = new System.Windows.Forms.Label();
			this.picPoisonArrow = new System.Windows.Forms.PictureBox();
			this.lblStep2A = new System.Windows.Forms.Label();
			this.tmrAni = new System.Windows.Forms.Timer(this.components);
			this.grpSaveLOR = new System.Windows.Forms.GroupBox();
			this.lblSeqTime = new System.Windows.Forms.Label();
			this.chkAutolaunch = new System.Windows.Forms.CheckBox();
			this.picLOR = new System.Windows.Forms.PictureBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.optSeqAppend = new System.Windows.Forms.RadioButton();
			this.optSeqNew = new System.Windows.Forms.RadioButton();
			this.button1 = new System.Windows.Forms.Button();
			this.lblStep4B = new System.Windows.Forms.Label();
			this.lblFileSequence = new System.Windows.Forms.Label();
			this.btnSaveSeq = new System.Windows.Forms.Button();
			this.txtSeqName = new System.Windows.Forms.TextBox();
			this.pnlTrackBeatsX.SuspendLayout();
			this.staStatus.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.grpTimings.SuspendLayout();
			this.grpChords.SuspendLayout();
			this.grbGlobal.SuspendLayout();
			this.pnlBeatFade.SuspendLayout();
			this.panel1.SuspendLayout();
			this.grpPlatform.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.grpVocals.SuspendLayout();
			this.grpSegments.SuspendLayout();
			this.grpTempo.SuspendLayout();
			this.grpPoly2.SuspendLayout();
			this.grpPitchKey.SuspendLayout();
			this.grpChromagram.SuspendLayout();
			this.grpPolyphonic.SuspendLayout();
			this.grpOnsets.SuspendLayout();
			this.pnlOnsetSensitivity.SuspendLayout();
			this.grpBarsBeats.SuspendLayout();
			this.pnlNotes.SuspendLayout();
			this.pnlVamping.SuspendLayout();
			this.grpExplore.SuspendLayout();
			this.grpAudio.SuspendLayout();
			this.grpSavex.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picxLights)).BeginInit();
			this.panel2.SuspendLayout();
			this.grpAnalyze.SuspendLayout();
			this.grpOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picWorking)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picVampire)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picPoisonArrow)).BeginInit();
			this.grpSaveLOR.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picLOR)).BeginInit();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTrackBeatsX
			// 
			this.pnlTrackBeatsX.Controls.Add(this.vscStartBeat);
			this.pnlTrackBeatsX.Controls.Add(this.txtStartBeat);
			this.pnlTrackBeatsX.Controls.Add(this.lblTrackBeatsX);
			this.pnlTrackBeatsX.Location = new System.Drawing.Point(6, 55);
			this.pnlTrackBeatsX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlTrackBeatsX.Name = "pnlTrackBeatsX";
			this.pnlTrackBeatsX.Size = new System.Drawing.Size(118, 27);
			this.pnlTrackBeatsX.TabIndex = 142;
			this.ttip.SetToolTip(this.pnlTrackBeatsX, "Sometimes the very first beat detected is NOT the first beat of the bar.\r\nIf (and" +
        " only if) this happens, you can correct it here.\r\n");
			// 
			// vscStartBeat
			// 
			this.vscStartBeat.LargeChange = 1;
			this.vscStartBeat.Location = new System.Drawing.Point(78, -3);
			this.vscStartBeat.Maximum = 4;
			this.vscStartBeat.Minimum = 1;
			this.vscStartBeat.Name = "vscStartBeat";
			this.vscStartBeat.Size = new System.Drawing.Size(16, 21);
			this.vscStartBeat.TabIndex = 28;
			this.ttip.SetToolTip(this.vscStartBeat, "Sometimes the very first beat detected is NOT the first beat of the bar.\r\nIf (and" +
        " only if) this happens, you can correct it here.\r\n");
			this.vscStartBeat.Value = 1;
			this.vscStartBeat.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vscStartBeat_Scroll);
			// 
			// txtStartBeat
			// 
			this.txtStartBeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtStartBeat.Location = new System.Drawing.Point(63, 0);
			this.txtStartBeat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtStartBeat.Name = "txtStartBeat";
			this.txtStartBeat.ReadOnly = true;
			this.txtStartBeat.Size = new System.Drawing.Size(14, 20);
			this.txtStartBeat.TabIndex = 7;
			this.txtStartBeat.Text = "1";
			// 
			// lblTrackBeatsX
			// 
			this.lblTrackBeatsX.AutoSize = true;
			this.lblTrackBeatsX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTrackBeatsX.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblTrackBeatsX.Location = new System.Drawing.Point(1, 5);
			this.lblTrackBeatsX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTrackBeatsX.Name = "lblTrackBeatsX";
			this.lblTrackBeatsX.Size = new System.Drawing.Size(54, 13);
			this.lblTrackBeatsX.TabIndex = 6;
			this.lblTrackBeatsX.Text = "Start Beat";
			this.ttip.SetToolTip(this.lblTrackBeatsX, "Sometimes the very first beat detected is NOT the first beat of the bar.\r\nIf (and" +
        " only if) this happens, you can correct it here.");
			this.lblTrackBeatsX.UseMnemonic = false;
			// 
			// btnExploreVamp
			// 
			this.btnExploreVamp.AllowDrop = true;
			this.btnExploreVamp.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnExploreVamp.Image = ((System.Drawing.Image)(resources.GetObject("btnExploreVamp.Image")));
			this.btnExploreVamp.Location = new System.Drawing.Point(310, 18);
			this.btnExploreVamp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnExploreVamp.Name = "btnExploreVamp";
			this.btnExploreVamp.Size = new System.Drawing.Size(36, 36);
			this.btnExploreVamp.TabIndex = 183;
			this.ttip.SetToolTip(this.btnExploreVamp, "Explore the Vamperizer Source Code Folder");
			this.btnExploreVamp.UseVisualStyleBackColor = true;
			this.btnExploreVamp.Visible = false;
			this.btnExploreVamp.Click += new System.EventHandler(this.btnExploreVamp_Click);
			// 
			// btnExplorexLights
			// 
			this.btnExplorexLights.AllowDrop = true;
			this.btnExplorexLights.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnExplorexLights.Image = ((System.Drawing.Image)(resources.GetObject("btnExplorexLights.Image")));
			this.btnExplorexLights.Location = new System.Drawing.Point(161, 18);
			this.btnExplorexLights.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnExplorexLights.Name = "btnExplorexLights";
			this.btnExplorexLights.Size = new System.Drawing.Size(36, 36);
			this.btnExplorexLights.TabIndex = 182;
			this.ttip.SetToolTip(this.btnExplorexLights, "Explore xLights Show Folder");
			this.btnExplorexLights.UseVisualStyleBackColor = true;
			this.btnExplorexLights.Visible = false;
			this.btnExplorexLights.Click += new System.EventHandler(this.btnExplorexLights_Click);
			// 
			// btnExploreTemp
			// 
			this.btnExploreTemp.AllowDrop = true;
			this.btnExploreTemp.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnExploreTemp.Image = ((System.Drawing.Image)(resources.GetObject("btnExploreTemp.Image")));
			this.btnExploreTemp.Location = new System.Drawing.Point(259, 18);
			this.btnExploreTemp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnExploreTemp.Name = "btnExploreTemp";
			this.btnExploreTemp.Size = new System.Drawing.Size(36, 36);
			this.btnExploreTemp.TabIndex = 181;
			this.ttip.SetToolTip(this.btnExploreTemp, "Explore the Temp Working Folder");
			this.btnExploreTemp.UseVisualStyleBackColor = true;
			this.btnExploreTemp.Visible = false;
			this.btnExploreTemp.Click += new System.EventHandler(this.btnExploreVamp_Click);
			// 
			// btnCmdTemp
			// 
			this.btnCmdTemp.AllowDrop = true;
			this.btnCmdTemp.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnCmdTemp.Image = ((System.Drawing.Image)(resources.GetObject("btnCmdTemp.Image")));
			this.btnCmdTemp.Location = new System.Drawing.Point(210, 18);
			this.btnCmdTemp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnCmdTemp.Name = "btnCmdTemp";
			this.btnCmdTemp.Size = new System.Drawing.Size(36, 36);
			this.btnCmdTemp.TabIndex = 180;
			this.ttip.SetToolTip(this.btnCmdTemp, "Open Command Prompt in the Temp Working Directory");
			this.btnCmdTemp.UseVisualStyleBackColor = true;
			this.btnCmdTemp.Visible = false;
			this.btnCmdTemp.Click += new System.EventHandler(this.btnCmdTemp_Click);
			// 
			// btnLaunchxLights
			// 
			this.btnLaunchxLights.AllowDrop = true;
			this.btnLaunchxLights.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnLaunchxLights.Image = ((System.Drawing.Image)(resources.GetObject("btnLaunchxLights.Image")));
			this.btnLaunchxLights.Location = new System.Drawing.Point(112, 18);
			this.btnLaunchxLights.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnLaunchxLights.Name = "btnLaunchxLights";
			this.btnLaunchxLights.Size = new System.Drawing.Size(36, 36);
			this.btnLaunchxLights.TabIndex = 179;
			this.ttip.SetToolTip(this.btnLaunchxLights, "Launch xLights");
			this.btnLaunchxLights.UseVisualStyleBackColor = true;
			this.btnLaunchxLights.Visible = false;
			this.btnLaunchxLights.Click += new System.EventHandler(this.btnLaunchxLights_Click);
			// 
			// btnSequenceEditor
			// 
			this.btnSequenceEditor.AllowDrop = true;
			this.btnSequenceEditor.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnSequenceEditor.Image = ((System.Drawing.Image)(resources.GetObject("btnSequenceEditor.Image")));
			this.btnSequenceEditor.Location = new System.Drawing.Point(13, 18);
			this.btnSequenceEditor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnSequenceEditor.Name = "btnSequenceEditor";
			this.btnSequenceEditor.Size = new System.Drawing.Size(36, 36);
			this.btnSequenceEditor.TabIndex = 178;
			this.ttip.SetToolTip(this.btnSequenceEditor, "Launch Light-O-Rama Sequence Editor");
			this.btnSequenceEditor.UseVisualStyleBackColor = true;
			this.btnSequenceEditor.Visible = false;
			this.btnSequenceEditor.Click += new System.EventHandler(this.btnSequenceEditor_Click);
			// 
			// btnExploreLOR
			// 
			this.btnExploreLOR.AllowDrop = true;
			this.btnExploreLOR.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnExploreLOR.Image = ((System.Drawing.Image)(resources.GetObject("btnExploreLOR.Image")));
			this.btnExploreLOR.Location = new System.Drawing.Point(62, 18);
			this.btnExploreLOR.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnExploreLOR.Name = "btnExploreLOR";
			this.btnExploreLOR.Size = new System.Drawing.Size(36, 36);
			this.btnExploreLOR.TabIndex = 177;
			this.ttip.SetToolTip(this.btnExploreLOR, "Explore Light-O-Rama Sequences Folder");
			this.btnExploreLOR.UseVisualStyleBackColor = true;
			this.btnExploreLOR.Visible = false;
			this.btnExploreLOR.Click += new System.EventHandler(this.btnExploreTemp_Click);
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlProgress,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 811);
			this.staStatus.Name = "staStatus";
			this.staStatus.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.staStatus.Size = new System.Drawing.Size(1155, 24);
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
			this.pnlStatus.Size = new System.Drawing.Size(1041, 19);
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
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuGenerate});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(1338, 28);
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
			this.mnuFile.Size = new System.Drawing.Size(37, 24);
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
			this.mnuGenerate.Size = new System.Drawing.Size(66, 24);
			this.mnuGenerate.Text = "&Generate";
			// 
			// mnuTimingMarks
			// 
			this.mnuTimingMarks.Name = "mnuTimingMarks";
			this.mnuTimingMarks.Size = new System.Drawing.Size(205, 22);
			this.mnuTimingMarks.Text = "&Timing Marks";
			// 
			// mnuTranscription
			// 
			this.mnuTranscription.Name = "mnuTranscription";
			this.mnuTranscription.Size = new System.Drawing.Size(205, 22);
			this.mnuTranscription.Text = "&Polyphonic Transcription";
			// 
			// grpTimings
			// 
			this.grpTimings.Controls.Add(this.grpChords);
			this.grpTimings.Controls.Add(this.grbGlobal);
			this.grpTimings.Controls.Add(this.lblNote3Third);
			this.grpTimings.Controls.Add(this.lblNote1Bars);
			this.grpTimings.Controls.Add(this.lblWorkFolder);
			this.grpTimings.Controls.Add(this.grpPlatform);
			this.grpTimings.Controls.Add(this.chk24fps);
			this.grpTimings.Controls.Add(this.chk30fps);
			this.grpTimings.Controls.Add(this.lblTweaks);
			this.grpTimings.Controls.Add(this.chk15fps);
			this.grpTimings.Controls.Add(this.grpVocals);
			this.grpTimings.Controls.Add(this.lblTrans2Options);
			this.grpTimings.Controls.Add(this.grpSegments);
			this.grpTimings.Controls.Add(this.button3);
			this.grpTimings.Controls.Add(this.grpTempo);
			this.grpTimings.Controls.Add(this.grpPoly2);
			this.grpTimings.Controls.Add(this.checkBox2);
			this.grpTimings.Controls.Add(this.btnTrackSettings);
			this.grpTimings.Controls.Add(this.checkBox3);
			this.grpTimings.Controls.Add(this.grpPitchKey);
			this.grpTimings.Controls.Add(this.checkBox4);
			this.grpTimings.Controls.Add(this.grpChromagram);
			this.grpTimings.Controls.Add(this.grpPolyphonic);
			this.grpTimings.Controls.Add(this.chkChromathing);
			this.grpTimings.Controls.Add(this.grpOnsets);
			this.grpTimings.Controls.Add(this.grpBarsBeats);
			this.grpTimings.Controls.Add(this.lblStep2B);
			this.grpTimings.Controls.Add(this.chkFlux);
			this.grpTimings.Enabled = false;
			this.grpTimings.Location = new System.Drawing.Point(411, 14);
			this.grpTimings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpTimings.Name = "grpTimings";
			this.grpTimings.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpTimings.Size = new System.Drawing.Size(733, 780);
			this.grpTimings.TabIndex = 113;
			this.grpTimings.TabStop = false;
			this.grpTimings.Text = "      Select Which Timings to Generate";
			this.grpTimings.Enter += new System.EventHandler(this.grpTimings_Enter);
			// 
			// grpChords
			// 
			this.grpChords.Controls.Add(this.chkChords);
			this.grpChords.Controls.Add(this.label2);
			this.grpChords.Controls.Add(this.cboLabelsChords);
			this.grpChords.Controls.Add(this.label3);
			this.grpChords.Controls.Add(this.cboMethodChords);
			this.grpChords.Controls.Add(this.label4);
			this.grpChords.Controls.Add(this.cboAlignChords);
			this.grpChords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.grpChords.Location = new System.Drawing.Point(547, 273);
			this.grpChords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpChords.Name = "grpChords";
			this.grpChords.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpChords.Size = new System.Drawing.Size(172, 178);
			this.grpChords.TabIndex = 181;
			this.grpChords.TabStop = false;
			this.grpChords.Text = "   Chords";
			// 
			// chkChords
			// 
			this.chkChords.AutoSize = true;
			this.chkChords.Checked = true;
			this.chkChords.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkChords.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkChords.Location = new System.Drawing.Point(0, 0);
			this.chkChords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkChords.Name = "chkChords";
			this.chkChords.Size = new System.Drawing.Size(15, 14);
			this.chkChords.TabIndex = 151;
			this.chkChords.Tag = "11";
			this.chkChords.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(7, 74);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 149;
			this.label2.Text = "Labels:";
			// 
			// cboLabelsChords
			// 
			this.cboLabelsChords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsChords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboLabelsChords.FormattingEnabled = true;
			this.cboLabelsChords.Items.AddRange(new object[] {
            "None",
            "Note Names",
            "MIDI Note Numbers"});
			this.cboLabelsChords.Location = new System.Drawing.Point(7, 92);
			this.cboLabelsChords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboLabelsChords.Name = "cboLabelsChords";
			this.cboLabelsChords.Size = new System.Drawing.Size(157, 21);
			this.cboLabelsChords.TabIndex = 148;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.Location = new System.Drawing.Point(7, 18);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(86, 13);
			this.label3.TabIndex = 144;
			this.label3.Text = "Plugin / Method:";
			// 
			// cboMethodChords
			// 
			this.cboMethodChords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodChords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboMethodChords.FormattingEnabled = true;
			this.cboMethodChords.Items.AddRange(new object[] {
            "Queen Mary Note Onset Detector",
            "Queen Mary Polyphonic Transcription",
            "OnsetDS Onset Detector",
            "Silvet Note Transcription",
            "Alicante Note Onset Detector",
            "Alicante Polyphonic Transcription",
            "Aubio Onset Detector",
            "Aubio Note Tracker"});
			this.cboMethodChords.Location = new System.Drawing.Point(7, 37);
			this.cboMethodChords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboMethodChords.Name = "cboMethodChords";
			this.cboMethodChords.Size = new System.Drawing.Size(157, 21);
			this.cboMethodChords.TabIndex = 143;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label4.Location = new System.Drawing.Point(7, 120);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(49, 13);
			this.label4.TabIndex = 142;
			this.label4.Text = "Align To:";
			// 
			// cboAlignChords
			// 
			this.cboAlignChords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignChords.DropDownWidth = 180;
			this.cboAlignChords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboAlignChords.FormattingEnabled = true;
			this.cboAlignChords.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Bars / Whole Notes",
            "Full Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes",
            "Note Onsets"});
			this.cboAlignChords.Location = new System.Drawing.Point(7, 138);
			this.cboAlignChords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboAlignChords.Name = "cboAlignChords";
			this.cboAlignChords.Size = new System.Drawing.Size(157, 21);
			this.cboAlignChords.TabIndex = 141;
			// 
			// grbGlobal
			// 
			this.grbGlobal.Controls.Add(this.lblStepSize);
			this.grbGlobal.Controls.Add(this.cboStepSize);
			this.grbGlobal.Controls.Add(this.pnlBeatFade);
			this.grbGlobal.Controls.Add(this.chkWhiten);
			this.grbGlobal.Controls.Add(this.pnlTrackBeatsX);
			this.grbGlobal.Controls.Add(this.panel1);
			this.grbGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.grbGlobal.Location = new System.Drawing.Point(7, 89);
			this.grbGlobal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grbGlobal.Name = "grbGlobal";
			this.grbGlobal.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grbGlobal.Size = new System.Drawing.Size(172, 201);
			this.grbGlobal.TabIndex = 180;
			this.grbGlobal.TabStop = false;
			this.grbGlobal.Text = " All Vamps ";
			// 
			// lblStepSize
			// 
			this.lblStepSize.AutoSize = true;
			this.lblStepSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblStepSize.Location = new System.Drawing.Point(7, 145);
			this.lblStepSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStepSize.Name = "lblStepSize";
			this.lblStepSize.Size = new System.Drawing.Size(55, 13);
			this.lblStepSize.TabIndex = 164;
			this.lblStepSize.Text = "Step Size:";
			// 
			// cboStepSize
			// 
			this.cboStepSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStepSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboStepSize.FormattingEnabled = true;
			this.cboStepSize.Items.AddRange(new object[] {
            "441",
            "512",
            "557"});
			this.cboStepSize.Location = new System.Drawing.Point(7, 164);
			this.cboStepSize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboStepSize.Name = "cboStepSize";
			this.cboStepSize.Size = new System.Drawing.Size(157, 21);
			this.cboStepSize.TabIndex = 163;
			// 
			// pnlBeatFade
			// 
			this.pnlBeatFade.Controls.Add(this.swRamps);
			this.pnlBeatFade.Controls.Add(this.lblBarsRampFade);
			this.pnlBeatFade.Controls.Add(this.lblBarsOnOff);
			this.pnlBeatFade.Location = new System.Drawing.Point(6, 119);
			this.pnlBeatFade.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlBeatFade.Name = "pnlBeatFade";
			this.pnlBeatFade.Size = new System.Drawing.Size(162, 20);
			this.pnlBeatFade.TabIndex = 158;
			// 
			// swRamps
			// 
			this.swRamps.Location = new System.Drawing.Point(43, 1);
			this.swRamps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.swRamps.Name = "swRamps";
			this.swRamps.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.swRamps.Size = new System.Drawing.Size(37, 18);
			this.swRamps.Style = JCS.ToggleSwitch.ToggleSwitchStyle.OSX;
			this.swRamps.TabIndex = 132;
			// 
			// lblBarsRampFade
			// 
			this.lblBarsRampFade.AutoSize = true;
			this.lblBarsRampFade.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblBarsRampFade.Location = new System.Drawing.Point(80, 2);
			this.lblBarsRampFade.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBarsRampFade.Name = "lblBarsRampFade";
			this.lblBarsRampFade.Size = new System.Drawing.Size(62, 13);
			this.lblBarsRampFade.TabIndex = 128;
			this.lblBarsRampFade.Text = "Ramp-Fade";
			// 
			// lblBarsOnOff
			// 
			this.lblBarsOnOff.AutoSize = true;
			this.lblBarsOnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblBarsOnOff.Location = new System.Drawing.Point(0, 2);
			this.lblBarsOnOff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBarsOnOff.Name = "lblBarsOnOff";
			this.lblBarsOnOff.Size = new System.Drawing.Size(38, 13);
			this.lblBarsOnOff.TabIndex = 127;
			this.lblBarsOnOff.Text = "On-Off";
			// 
			// chkWhiten
			// 
			this.chkWhiten.AutoSize = true;
			this.chkWhiten.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkWhiten.Location = new System.Drawing.Point(6, 92);
			this.chkWhiten.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkWhiten.Name = "chkWhiten";
			this.chkWhiten.Size = new System.Drawing.Size(89, 17);
			this.chkWhiten.TabIndex = 155;
			this.chkWhiten.Tag = "13";
			this.chkWhiten.Text = "\"Whitening\"";
			this.chkWhiten.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.swTrackBeat);
			this.panel1.Controls.Add(this.lblTrackBeat34);
			this.panel1.Controls.Add(this.lblTrackBeat44);
			this.panel1.Location = new System.Drawing.Point(6, 22);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(148, 27);
			this.panel1.TabIndex = 141;
			// 
			// swTrackBeat
			// 
			this.swTrackBeat.Location = new System.Drawing.Point(54, 1);
			this.swTrackBeat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.swTrackBeat.Name = "swTrackBeat";
			this.swTrackBeat.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.swTrackBeat.Size = new System.Drawing.Size(37, 18);
			this.swTrackBeat.Style = JCS.ToggleSwitch.ToggleSwitchStyle.OSX;
			this.swTrackBeat.TabIndex = 133;
			// 
			// lblTrackBeat34
			// 
			this.lblTrackBeat34.AutoSize = true;
			this.lblTrackBeat34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTrackBeat34.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblTrackBeat34.Location = new System.Drawing.Point(93, 2);
			this.lblTrackBeat34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTrackBeat34.Name = "lblTrackBeat34";
			this.lblTrackBeat34.Size = new System.Drawing.Size(46, 13);
			this.lblTrackBeat34.TabIndex = 128;
			this.lblTrackBeat34.Text = "3/4 time";
			// 
			// lblTrackBeat44
			// 
			this.lblTrackBeat44.AutoSize = true;
			this.lblTrackBeat44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTrackBeat44.Location = new System.Drawing.Point(1, 2);
			this.lblTrackBeat44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTrackBeat44.Name = "lblTrackBeat44";
			this.lblTrackBeat44.Size = new System.Drawing.Size(46, 13);
			this.lblTrackBeat44.TabIndex = 127;
			this.lblTrackBeat44.Text = "4/4 time";
			// 
			// lblNote3Third
			// 
			this.lblNote3Third.AutoSize = true;
			this.lblNote3Third.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblNote3Third.Location = new System.Drawing.Point(612, 722);
			this.lblNote3Third.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblNote3Third.Name = "lblNote3Third";
			this.lblNote3Third.Size = new System.Drawing.Size(23, 26);
			this.lblNote3Third.TabIndex = 148;
			this.lblNote3Third.Text = "𝅝";
			this.lblNote3Third.Visible = false;
			// 
			// lblNote1Bars
			// 
			this.lblNote1Bars.AutoSize = true;
			this.lblNote1Bars.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblNote1Bars.Location = new System.Drawing.Point(713, 745);
			this.lblNote1Bars.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblNote1Bars.Name = "lblNote1Bars";
			this.lblNote1Bars.Size = new System.Drawing.Size(19, 30);
			this.lblNote1Bars.TabIndex = 145;
			this.lblNote1Bars.Text = "𝄁";
			this.lblNote1Bars.Visible = false;
			// 
			// lblWorkFolder
			// 
			this.lblWorkFolder.AutoSize = true;
			this.lblWorkFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblWorkFolder.Location = new System.Drawing.Point(554, 14);
			this.lblWorkFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblWorkFolder.Name = "lblWorkFolder";
			this.lblWorkFolder.Size = new System.Drawing.Size(109, 13);
			this.lblWorkFolder.TabIndex = 173;
			this.lblWorkFolder.Text = "Temp Working Folder";
			this.lblWorkFolder.Visible = false;
			// 
			// grpPlatform
			// 
			this.grpPlatform.Controls.Add(this.pictureBox3);
			this.grpPlatform.Controls.Add(this.chkxLights);
			this.grpPlatform.Controls.Add(this.pictureBox2);
			this.grpPlatform.Controls.Add(this.chkLOR);
			this.grpPlatform.Controls.Add(this.lbllabel);
			this.grpPlatform.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.grpPlatform.Location = new System.Drawing.Point(9, 21);
			this.grpPlatform.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpPlatform.Name = "grpPlatform";
			this.grpPlatform.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpPlatform.Size = new System.Drawing.Size(350, 55);
			this.grpPlatform.TabIndex = 157;
			this.grpPlatform.TabStop = false;
			this.grpPlatform.Text = "       Platform";
			// 
			// pictureBox3
			// 
			this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
			this.pictureBox3.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.InitialImage")));
			this.pictureBox3.Location = new System.Drawing.Point(197, 20);
			this.pictureBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(85, 26);
			this.pictureBox3.TabIndex = 160;
			this.pictureBox3.TabStop = false;
			// 
			// chkxLights
			// 
			this.chkxLights.AutoSize = true;
			this.chkxLights.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkxLights.Location = new System.Drawing.Point(176, 25);
			this.chkxLights.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkxLights.Name = "chkxLights";
			this.chkxLights.Size = new System.Drawing.Size(15, 14);
			this.chkxLights.TabIndex = 159;
			this.chkxLights.Tag = "13";
			this.chkxLights.UseVisualStyleBackColor = true;
			this.chkxLights.CheckedChanged += new System.EventHandler(this.chkxLights_CheckedChanged);
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
			this.pictureBox2.Location = new System.Drawing.Point(47, 23);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(90, 19);
			this.pictureBox2.TabIndex = 158;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
			// 
			// chkLOR
			// 
			this.chkLOR.AutoSize = true;
			this.chkLOR.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkLOR.Location = new System.Drawing.Point(22, 25);
			this.chkLOR.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkLOR.Name = "chkLOR";
			this.chkLOR.Size = new System.Drawing.Size(15, 14);
			this.chkLOR.TabIndex = 157;
			this.chkLOR.Tag = "13";
			this.chkLOR.UseVisualStyleBackColor = true;
			this.chkLOR.CheckedChanged += new System.EventHandler(this.chkLOR_CheckedChanged);
			// 
			// lbllabel
			// 
			this.lbllabel.AutoSize = true;
			this.lbllabel.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbllabel.Location = new System.Drawing.Point(154, 63);
			this.lbllabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbllabel.Name = "lbllabel";
			this.lbllabel.Size = new System.Drawing.Size(38, 13);
			this.lbllabel.TabIndex = 142;
			this.lbllabel.Text = "(lable)";
			this.lbllabel.Visible = false;
			// 
			// chk24fps
			// 
			this.chk24fps.AutoSize = true;
			this.chk24fps.Enabled = false;
			this.chk24fps.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chk24fps.Location = new System.Drawing.Point(757, 513);
			this.chk24fps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chk24fps.Name = "chk24fps";
			this.chk24fps.Size = new System.Drawing.Size(153, 17);
			this.chk24fps.TabIndex = 170;
			this.chk24fps.Tag = "12";
			this.chk24fps.Text = "4.16 centiseconds 24 FPS";
			this.chk24fps.UseVisualStyleBackColor = true;
			// 
			// chk30fps
			// 
			this.chk30fps.AllowDrop = true;
			this.chk30fps.AutoSize = true;
			this.chk30fps.Enabled = false;
			this.chk30fps.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chk30fps.Location = new System.Drawing.Point(757, 495);
			this.chk30fps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chk30fps.Name = "chk30fps";
			this.chk30fps.Size = new System.Drawing.Size(153, 17);
			this.chk30fps.TabIndex = 169;
			this.chk30fps.Tag = "12";
			this.chk30fps.Text = "3.33 centiseconds 30 FPS";
			this.chk30fps.UseVisualStyleBackColor = true;
			// 
			// lblTweaks
			// 
			this.lblTweaks.AutoSize = true;
			this.lblTweaks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
			this.lblTweaks.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblTweaks.Location = new System.Drawing.Point(794, 314);
			this.lblTweaks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTweaks.Name = "lblTweaks";
			this.lblTweaks.Size = new System.Drawing.Size(79, 13);
			this.lblTweaks.TabIndex = 157;
			this.lblTweaks.Text = "More Options...";
			this.lblTweaks.Click += new System.EventHandler(this.labelTweaks_Click);
			// 
			// chk15fps
			// 
			this.chk15fps.AutoSize = true;
			this.chk15fps.Enabled = false;
			this.chk15fps.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chk15fps.Location = new System.Drawing.Point(757, 532);
			this.chk15fps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chk15fps.Name = "chk15fps";
			this.chk15fps.Size = new System.Drawing.Size(153, 17);
			this.chk15fps.TabIndex = 168;
			this.chk15fps.Tag = "12";
			this.chk15fps.Text = "6.67 centiseconds 15 FPS";
			this.chk15fps.UseVisualStyleBackColor = true;
			// 
			// grpVocals
			// 
			this.grpVocals.Controls.Add(this.chkVocals);
			this.grpVocals.Controls.Add(this.lblVocalsAlign);
			this.grpVocals.Controls.Add(this.cboAlignVocals);
			this.grpVocals.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.grpVocals.Location = new System.Drawing.Point(682, 708);
			this.grpVocals.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpVocals.Name = "grpVocals";
			this.grpVocals.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpVocals.Size = new System.Drawing.Size(172, 99);
			this.grpVocals.TabIndex = 143;
			this.grpVocals.TabStop = false;
			this.grpVocals.Text = "   Vocals";
			this.grpVocals.Visible = false;
			// 
			// chkVocals
			// 
			this.chkVocals.AutoSize = true;
			this.chkVocals.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkVocals.Location = new System.Drawing.Point(0, 0);
			this.chkVocals.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkVocals.Name = "chkVocals";
			this.chkVocals.Size = new System.Drawing.Size(15, 14);
			this.chkVocals.TabIndex = 143;
			this.chkVocals.Tag = "14";
			this.chkVocals.UseVisualStyleBackColor = true;
			this.chkVocals.Visible = false;
			// 
			// lblVocalsAlign
			// 
			this.lblVocalsAlign.AutoSize = true;
			this.lblVocalsAlign.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblVocalsAlign.Location = new System.Drawing.Point(7, 47);
			this.lblVocalsAlign.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblVocalsAlign.Name = "lblVocalsAlign";
			this.lblVocalsAlign.Size = new System.Drawing.Size(52, 13);
			this.lblVocalsAlign.TabIndex = 142;
			this.lblVocalsAlign.Text = "Align To:";
			// 
			// cboAlignVocals
			// 
			this.cboAlignVocals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignVocals.DropDownWidth = 180;
			this.cboAlignVocals.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
			this.cboAlignVocals.Location = new System.Drawing.Point(7, 66);
			this.cboAlignVocals.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboAlignVocals.Name = "cboAlignVocals";
			this.cboAlignVocals.Size = new System.Drawing.Size(157, 21);
			this.cboAlignVocals.TabIndex = 141;
			// 
			// lblTrans2Options
			// 
			this.lblTrans2Options.AutoSize = true;
			this.lblTrans2Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
			this.lblTrans2Options.Location = new System.Drawing.Point(834, 655);
			this.lblTrans2Options.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTrans2Options.Name = "lblTrans2Options";
			this.lblTrans2Options.Size = new System.Drawing.Size(79, 13);
			this.lblTrans2Options.TabIndex = 167;
			this.lblTrans2Options.Text = "More Options...";
			// 
			// grpSegments
			// 
			this.grpSegments.Controls.Add(this.lblSegmentsLabels);
			this.grpSegments.Controls.Add(this.cboLabelsSegments);
			this.grpSegments.Controls.Add(this.lblSegmentsPlugin);
			this.grpSegments.Controls.Add(this.cboMethodSegments);
			this.grpSegments.Controls.Add(this.chkSegments);
			this.grpSegments.Controls.Add(this.lblSegmentsAlign);
			this.grpSegments.Controls.Add(this.cboAlignSegments);
			this.grpSegments.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.grpSegments.Location = new System.Drawing.Point(547, 477);
			this.grpSegments.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpSegments.Name = "grpSegments";
			this.grpSegments.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpSegments.Size = new System.Drawing.Size(172, 173);
			this.grpSegments.TabIndex = 142;
			this.grpSegments.TabStop = false;
			this.grpSegments.Text = "   Segments";
			// 
			// lblSegmentsLabels
			// 
			this.lblSegmentsLabels.AutoSize = true;
			this.lblSegmentsLabels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblSegmentsLabels.Location = new System.Drawing.Point(7, 69);
			this.lblSegmentsLabels.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSegmentsLabels.Name = "lblSegmentsLabels";
			this.lblSegmentsLabels.Size = new System.Drawing.Size(41, 13);
			this.lblSegmentsLabels.TabIndex = 154;
			this.lblSegmentsLabels.Text = "Labels:";
			// 
			// cboLabelsSegments
			// 
			this.cboLabelsSegments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsSegments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboLabelsSegments.FormattingEnabled = true;
			this.cboLabelsSegments.Items.AddRange(new object[] {
            "None",
            "Numbers",
            "Letters"});
			this.cboLabelsSegments.Location = new System.Drawing.Point(7, 88);
			this.cboLabelsSegments.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboLabelsSegments.Name = "cboLabelsSegments";
			this.cboLabelsSegments.Size = new System.Drawing.Size(157, 21);
			this.cboLabelsSegments.TabIndex = 153;
			// 
			// lblSegmentsPlugin
			// 
			this.lblSegmentsPlugin.AutoSize = true;
			this.lblSegmentsPlugin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblSegmentsPlugin.Location = new System.Drawing.Point(7, 18);
			this.lblSegmentsPlugin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSegmentsPlugin.Name = "lblSegmentsPlugin";
			this.lblSegmentsPlugin.Size = new System.Drawing.Size(86, 13);
			this.lblSegmentsPlugin.TabIndex = 152;
			this.lblSegmentsPlugin.Text = "Plugin / Method:";
			// 
			// cboMethodSegments
			// 
			this.cboMethodSegments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodSegments.DropDownWidth = 200;
			this.cboMethodSegments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboMethodSegments.FormattingEnabled = true;
			this.cboMethodSegments.Items.AddRange(new object[] {
            "Queen Mary Segmenter",
            "Segmentino"});
			this.cboMethodSegments.Location = new System.Drawing.Point(6, 37);
			this.cboMethodSegments.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboMethodSegments.Name = "cboMethodSegments";
			this.cboMethodSegments.Size = new System.Drawing.Size(157, 21);
			this.cboMethodSegments.TabIndex = 151;
			// 
			// chkSegments
			// 
			this.chkSegments.AutoSize = true;
			this.chkSegments.Checked = true;
			this.chkSegments.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSegments.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkSegments.Location = new System.Drawing.Point(0, 0);
			this.chkSegments.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkSegments.Name = "chkSegments";
			this.chkSegments.Size = new System.Drawing.Size(15, 14);
			this.chkSegments.TabIndex = 150;
			this.chkSegments.Tag = "7";
			this.chkSegments.UseVisualStyleBackColor = true;
			// 
			// lblSegmentsAlign
			// 
			this.lblSegmentsAlign.AutoSize = true;
			this.lblSegmentsAlign.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblSegmentsAlign.Location = new System.Drawing.Point(7, 120);
			this.lblSegmentsAlign.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSegmentsAlign.Name = "lblSegmentsAlign";
			this.lblSegmentsAlign.Size = new System.Drawing.Size(52, 13);
			this.lblSegmentsAlign.TabIndex = 142;
			this.lblSegmentsAlign.Text = "Align To:";
			// 
			// cboAlignSegments
			// 
			this.cboAlignSegments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignSegments.DropDownWidth = 180;
			this.cboAlignSegments.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
			this.cboAlignSegments.Location = new System.Drawing.Point(6, 140);
			this.cboAlignSegments.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboAlignSegments.Name = "cboAlignSegments";
			this.cboAlignSegments.Size = new System.Drawing.Size(157, 21);
			this.cboAlignSegments.TabIndex = 141;
			// 
			// button3
			// 
			this.button3.AllowDrop = true;
			this.button3.Enabled = false;
			this.button3.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
			this.button3.Location = new System.Drawing.Point(742, 255);
			this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(37, 37);
			this.button3.TabIndex = 162;
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Visible = false;
			// 
			// grpTempo
			// 
			this.grpTempo.Controls.Add(this.chkTempo);
			this.grpTempo.Controls.Add(this.lblTempoLabels);
			this.grpTempo.Controls.Add(this.cboLabelsTempo);
			this.grpTempo.Controls.Add(this.lblTempoPlugin);
			this.grpTempo.Controls.Add(this.cboMethodTempo);
			this.grpTempo.Controls.Add(this.lblTempoAlign);
			this.grpTempo.Controls.Add(this.cboAlignTempo);
			this.grpTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.grpTempo.Location = new System.Drawing.Point(369, 477);
			this.grpTempo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpTempo.Name = "grpTempo";
			this.grpTempo.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpTempo.Size = new System.Drawing.Size(172, 173);
			this.grpTempo.TabIndex = 141;
			this.grpTempo.TabStop = false;
			this.grpTempo.Text = "   Tempo";
			// 
			// chkTempo
			// 
			this.chkTempo.AutoSize = true;
			this.chkTempo.Checked = true;
			this.chkTempo.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkTempo.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkTempo.ForeColor = System.Drawing.Color.Red;
			this.chkTempo.Location = new System.Drawing.Point(0, 0);
			this.chkTempo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkTempo.Name = "chkTempo";
			this.chkTempo.Size = new System.Drawing.Size(15, 14);
			this.chkTempo.TabIndex = 150;
			this.chkTempo.Tag = "9";
			this.chkTempo.UseVisualStyleBackColor = true;
			// 
			// lblTempoLabels
			// 
			this.lblTempoLabels.AutoSize = true;
			this.lblTempoLabels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTempoLabels.Location = new System.Drawing.Point(7, 68);
			this.lblTempoLabels.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTempoLabels.Name = "lblTempoLabels";
			this.lblTempoLabels.Size = new System.Drawing.Size(41, 13);
			this.lblTempoLabels.TabIndex = 149;
			this.lblTempoLabels.Text = "Labels:";
			// 
			// cboLabelsTempo
			// 
			this.cboLabelsTempo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboLabelsTempo.FormattingEnabled = true;
			this.cboLabelsTempo.Items.AddRange(new object[] {
            "None",
            "Note Names",
            "MIDI Note Numbers"});
			this.cboLabelsTempo.Location = new System.Drawing.Point(7, 87);
			this.cboLabelsTempo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboLabelsTempo.Name = "cboLabelsTempo";
			this.cboLabelsTempo.Size = new System.Drawing.Size(157, 21);
			this.cboLabelsTempo.TabIndex = 148;
			// 
			// lblTempoPlugin
			// 
			this.lblTempoPlugin.AutoSize = true;
			this.lblTempoPlugin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTempoPlugin.Location = new System.Drawing.Point(7, 18);
			this.lblTempoPlugin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTempoPlugin.Name = "lblTempoPlugin";
			this.lblTempoPlugin.Size = new System.Drawing.Size(86, 13);
			this.lblTempoPlugin.TabIndex = 144;
			this.lblTempoPlugin.Text = "Plugin / Method:";
			// 
			// cboMethodTempo
			// 
			this.cboMethodTempo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
			this.cboMethodTempo.Location = new System.Drawing.Point(7, 38);
			this.cboMethodTempo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboMethodTempo.Name = "cboMethodTempo";
			this.cboMethodTempo.Size = new System.Drawing.Size(157, 21);
			this.cboMethodTempo.TabIndex = 143;
			// 
			// lblTempoAlign
			// 
			this.lblTempoAlign.AutoSize = true;
			this.lblTempoAlign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTempoAlign.Location = new System.Drawing.Point(7, 114);
			this.lblTempoAlign.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTempoAlign.Name = "lblTempoAlign";
			this.lblTempoAlign.Size = new System.Drawing.Size(49, 13);
			this.lblTempoAlign.TabIndex = 142;
			this.lblTempoAlign.Text = "Align To:";
			// 
			// cboAlignTempo
			// 
			this.cboAlignTempo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignTempo.DropDownWidth = 180;
			this.cboAlignTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
			this.cboAlignTempo.Location = new System.Drawing.Point(7, 133);
			this.cboAlignTempo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboAlignTempo.Name = "cboAlignTempo";
			this.cboAlignTempo.Size = new System.Drawing.Size(157, 21);
			this.cboAlignTempo.TabIndex = 141;
			// 
			// grpPoly2
			// 
			this.grpPoly2.Controls.Add(this.lblTrans2Plugin);
			this.grpPoly2.Controls.Add(this.lblTrans2Labels);
			this.grpPoly2.Controls.Add(this.comboBox1);
			this.grpPoly2.Controls.Add(this.checkBox1);
			this.grpPoly2.Controls.Add(this.comboBox2);
			this.grpPoly2.Controls.Add(this.lblTrans2Align);
			this.grpPoly2.Controls.Add(this.cboAlignFoo);
			this.grpPoly2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.grpPoly2.Location = new System.Drawing.Point(864, 28);
			this.grpPoly2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpPoly2.Name = "grpPoly2";
			this.grpPoly2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpPoly2.Size = new System.Drawing.Size(172, 399);
			this.grpPoly2.TabIndex = 166;
			this.grpPoly2.TabStop = false;
			this.grpPoly2.Text = "   Polyphonic Transcription";
			this.grpPoly2.Visible = false;
			// 
			// lblTrans2Plugin
			// 
			this.lblTrans2Plugin.AutoSize = true;
			this.lblTrans2Plugin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTrans2Plugin.Location = new System.Drawing.Point(7, 18);
			this.lblTrans2Plugin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTrans2Plugin.Name = "lblTrans2Plugin";
			this.lblTrans2Plugin.Size = new System.Drawing.Size(86, 13);
			this.lblTrans2Plugin.TabIndex = 148;
			this.lblTrans2Plugin.Text = "Plugin / Method:";
			// 
			// lblTrans2Labels
			// 
			this.lblTrans2Labels.AutoSize = true;
			this.lblTrans2Labels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTrans2Labels.Location = new System.Drawing.Point(7, 301);
			this.lblTrans2Labels.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTrans2Labels.Name = "lblTrans2Labels";
			this.lblTrans2Labels.Size = new System.Drawing.Size(41, 13);
			this.lblTrans2Labels.TabIndex = 147;
			this.lblTrans2Labels.Text = "Labels:";
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "MIDI Note Numbers",
            "Note Names"});
			this.comboBox1.Location = new System.Drawing.Point(7, 320);
			this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(157, 21);
			this.comboBox1.TabIndex = 146;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.checkBox1.Location = new System.Drawing.Point(0, 0);
			this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(15, 14);
			this.checkBox1.TabIndex = 145;
			this.checkBox1.Tag = "10";
			this.checkBox1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// comboBox2
			// 
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.DropDownWidth = 200;
			this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Items.AddRange(new object[] {
            "Queen Mary Polyphonic Transcription",
            "Silvet Note Transcription",
            "Alicante Polyphonic Transcription",
            "Aubio Note Tracker"});
			this.comboBox2.Location = new System.Drawing.Point(7, 37);
			this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(157, 21);
			this.comboBox2.TabIndex = 143;
			// 
			// lblTrans2Align
			// 
			this.lblTrans2Align.AutoSize = true;
			this.lblTrans2Align.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTrans2Align.Location = new System.Drawing.Point(7, 347);
			this.lblTrans2Align.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTrans2Align.Name = "lblTrans2Align";
			this.lblTrans2Align.Size = new System.Drawing.Size(49, 13);
			this.lblTrans2Align.TabIndex = 142;
			this.lblTrans2Align.Text = "Align To:";
			// 
			// cboAlignFoo
			// 
			this.cboAlignFoo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignFoo.DropDownWidth = 180;
			this.cboAlignFoo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboAlignFoo.FormattingEnabled = true;
			this.cboAlignFoo.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes",
            "Note Onsets"});
			this.cboAlignFoo.Location = new System.Drawing.Point(7, 366);
			this.cboAlignFoo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboAlignFoo.Name = "cboAlignFoo";
			this.cboAlignFoo.Size = new System.Drawing.Size(157, 21);
			this.cboAlignFoo.TabIndex = 141;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Enabled = false;
			this.checkBox2.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.checkBox2.Location = new System.Drawing.Point(742, 171);
			this.checkBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(80, 17);
			this.checkBox2.TabIndex = 165;
			this.checkBox2.Tag = "8";
			this.checkBox2.Text = "Whateva 1";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// btnTrackSettings
			// 
			this.btnTrackSettings.AllowDrop = true;
			this.btnTrackSettings.Enabled = false;
			this.btnTrackSettings.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnTrackSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnTrackSettings.Image")));
			this.btnTrackSettings.Location = new System.Drawing.Point(798, 255);
			this.btnTrackSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnTrackSettings.Name = "btnTrackSettings";
			this.btnTrackSettings.Size = new System.Drawing.Size(37, 37);
			this.btnTrackSettings.TabIndex = 122;
			this.btnTrackSettings.UseVisualStyleBackColor = true;
			this.btnTrackSettings.Visible = false;
			this.btnTrackSettings.Click += new System.EventHandler(this.btnTrackSettings_Click);
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Enabled = false;
			this.checkBox3.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.checkBox3.Location = new System.Drawing.Point(742, 197);
			this.checkBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(80, 17);
			this.checkBox3.TabIndex = 163;
			this.checkBox3.Tag = "13";
			this.checkBox3.Text = "Whateva 2";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// grpPitchKey
			// 
			this.grpPitchKey.Controls.Add(this.chkPitchKey);
			this.grpPitchKey.Controls.Add(this.lblKeyLabels);
			this.grpPitchKey.Controls.Add(this.cboLabelsPitchKey);
			this.grpPitchKey.Controls.Add(this.lblKeyPlugin);
			this.grpPitchKey.Controls.Add(this.cboMethodPitchKey);
			this.grpPitchKey.Controls.Add(this.lblKeyAlign);
			this.grpPitchKey.Controls.Add(this.cboAlignPitchKey);
			this.grpPitchKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.grpPitchKey.Location = new System.Drawing.Point(188, 477);
			this.grpPitchKey.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpPitchKey.Name = "grpPitchKey";
			this.grpPitchKey.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpPitchKey.Size = new System.Drawing.Size(172, 173);
			this.grpPitchKey.TabIndex = 140;
			this.grpPitchKey.TabStop = false;
			this.grpPitchKey.Text = "   Pitch && Key";
			// 
			// chkPitchKey
			// 
			this.chkPitchKey.AutoSize = true;
			this.chkPitchKey.Checked = true;
			this.chkPitchKey.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPitchKey.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkPitchKey.Location = new System.Drawing.Point(0, 0);
			this.chkPitchKey.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkPitchKey.Name = "chkPitchKey";
			this.chkPitchKey.Size = new System.Drawing.Size(15, 14);
			this.chkPitchKey.TabIndex = 150;
			this.chkPitchKey.Tag = "6";
			this.chkPitchKey.UseVisualStyleBackColor = true;
			// 
			// lblKeyLabels
			// 
			this.lblKeyLabels.AutoSize = true;
			this.lblKeyLabels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblKeyLabels.Location = new System.Drawing.Point(7, 69);
			this.lblKeyLabels.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblKeyLabels.Name = "lblKeyLabels";
			this.lblKeyLabels.Size = new System.Drawing.Size(41, 13);
			this.lblKeyLabels.TabIndex = 149;
			this.lblKeyLabels.Text = "Labels:";
			// 
			// cboLabelsPitchKey
			// 
			this.cboLabelsPitchKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsPitchKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboLabelsPitchKey.FormattingEnabled = true;
			this.cboLabelsPitchKey.Items.AddRange(new object[] {
            "None",
            "Key Names",
            "Key Numbers",
            "MIDI Note Numbers",
            "Frequency"});
			this.cboLabelsPitchKey.Location = new System.Drawing.Point(7, 88);
			this.cboLabelsPitchKey.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboLabelsPitchKey.Name = "cboLabelsPitchKey";
			this.cboLabelsPitchKey.Size = new System.Drawing.Size(157, 21);
			this.cboLabelsPitchKey.TabIndex = 148;
			// 
			// lblKeyPlugin
			// 
			this.lblKeyPlugin.AutoSize = true;
			this.lblKeyPlugin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblKeyPlugin.Location = new System.Drawing.Point(7, 18);
			this.lblKeyPlugin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblKeyPlugin.Name = "lblKeyPlugin";
			this.lblKeyPlugin.Size = new System.Drawing.Size(86, 13);
			this.lblKeyPlugin.TabIndex = 144;
			this.lblKeyPlugin.Text = "Plugin / Method:";
			// 
			// cboMethodPitchKey
			// 
			this.cboMethodPitchKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodPitchKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboMethodPitchKey.FormattingEnabled = true;
			this.cboMethodPitchKey.Items.AddRange(new object[] {
            "Queen Mary Key Detector"});
			this.cboMethodPitchKey.Location = new System.Drawing.Point(7, 37);
			this.cboMethodPitchKey.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboMethodPitchKey.Name = "cboMethodPitchKey";
			this.cboMethodPitchKey.Size = new System.Drawing.Size(157, 21);
			this.cboMethodPitchKey.TabIndex = 143;
			// 
			// lblKeyAlign
			// 
			this.lblKeyAlign.AutoSize = true;
			this.lblKeyAlign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblKeyAlign.Location = new System.Drawing.Point(7, 120);
			this.lblKeyAlign.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblKeyAlign.Name = "lblKeyAlign";
			this.lblKeyAlign.Size = new System.Drawing.Size(49, 13);
			this.lblKeyAlign.TabIndex = 142;
			this.lblKeyAlign.Text = "Align To:";
			// 
			// cboAlignPitchKey
			// 
			this.cboAlignPitchKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignPitchKey.DropDownWidth = 180;
			this.cboAlignPitchKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboAlignPitchKey.FormattingEnabled = true;
			this.cboAlignPitchKey.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Bars / Whole Notes",
            "Full Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes",
            "Note Onsets"});
			this.cboAlignPitchKey.Location = new System.Drawing.Point(7, 138);
			this.cboAlignPitchKey.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboAlignPitchKey.Name = "cboAlignPitchKey";
			this.cboAlignPitchKey.Size = new System.Drawing.Size(157, 21);
			this.cboAlignPitchKey.TabIndex = 141;
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Enabled = false;
			this.checkBox4.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.checkBox4.Location = new System.Drawing.Point(742, 224);
			this.checkBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(80, 17);
			this.checkBox4.TabIndex = 164;
			this.checkBox4.Tag = "12";
			this.checkBox4.Text = "Whateva 3";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// grpChromagram
			// 
			this.grpChromagram.Controls.Add(this.chkChromagram);
			this.grpChromagram.Controls.Add(this.lblSpectrumLabels);
			this.grpChromagram.Controls.Add(this.cboLabelsChromagram);
			this.grpChromagram.Controls.Add(this.lblSpectrumPlugin);
			this.grpChromagram.Controls.Add(this.cboMethodChromagram);
			this.grpChromagram.Controls.Add(this.lblSpectrumAlign);
			this.grpChromagram.Controls.Add(this.cboAlignChromagram);
			this.grpChromagram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.grpChromagram.Location = new System.Drawing.Point(547, 89);
			this.grpChromagram.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpChromagram.Name = "grpChromagram";
			this.grpChromagram.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpChromagram.Size = new System.Drawing.Size(172, 178);
			this.grpChromagram.TabIndex = 139;
			this.grpChromagram.TabStop = false;
			this.grpChromagram.Text = "   Chromagram";
			// 
			// chkChromagram
			// 
			this.chkChromagram.AutoSize = true;
			this.chkChromagram.Checked = true;
			this.chkChromagram.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkChromagram.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkChromagram.Location = new System.Drawing.Point(0, 0);
			this.chkChromagram.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkChromagram.Name = "chkChromagram";
			this.chkChromagram.Size = new System.Drawing.Size(15, 14);
			this.chkChromagram.TabIndex = 150;
			this.chkChromagram.Tag = "11";
			this.chkChromagram.UseVisualStyleBackColor = true;
			// 
			// lblSpectrumLabels
			// 
			this.lblSpectrumLabels.AutoSize = true;
			this.lblSpectrumLabels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblSpectrumLabels.Location = new System.Drawing.Point(7, 74);
			this.lblSpectrumLabels.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSpectrumLabels.Name = "lblSpectrumLabels";
			this.lblSpectrumLabels.Size = new System.Drawing.Size(41, 13);
			this.lblSpectrumLabels.TabIndex = 149;
			this.lblSpectrumLabels.Text = "Labels:";
			// 
			// cboLabelsChromagram
			// 
			this.cboLabelsChromagram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsChromagram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboLabelsChromagram.FormattingEnabled = true;
			this.cboLabelsChromagram.Items.AddRange(new object[] {
            "None",
            "Note Names",
            "MIDI Note Numbers"});
			this.cboLabelsChromagram.Location = new System.Drawing.Point(7, 92);
			this.cboLabelsChromagram.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboLabelsChromagram.Name = "cboLabelsChromagram";
			this.cboLabelsChromagram.Size = new System.Drawing.Size(157, 21);
			this.cboLabelsChromagram.TabIndex = 148;
			// 
			// lblSpectrumPlugin
			// 
			this.lblSpectrumPlugin.AutoSize = true;
			this.lblSpectrumPlugin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblSpectrumPlugin.Location = new System.Drawing.Point(7, 18);
			this.lblSpectrumPlugin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSpectrumPlugin.Name = "lblSpectrumPlugin";
			this.lblSpectrumPlugin.Size = new System.Drawing.Size(86, 13);
			this.lblSpectrumPlugin.TabIndex = 144;
			this.lblSpectrumPlugin.Text = "Plugin / Method:";
			// 
			// cboMethodChromagram
			// 
			this.cboMethodChromagram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodChromagram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboMethodChromagram.FormattingEnabled = true;
			this.cboMethodChromagram.Items.AddRange(new object[] {
            "Queen Mary Note Onset Detector",
            "Queen Mary Polyphonic Transcription",
            "OnsetDS Onset Detector",
            "Silvet Note Transcription",
            "Alicante Note Onset Detector",
            "Alicante Polyphonic Transcription",
            "Aubio Onset Detector",
            "Aubio Note Tracker"});
			this.cboMethodChromagram.Location = new System.Drawing.Point(7, 37);
			this.cboMethodChromagram.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboMethodChromagram.Name = "cboMethodChromagram";
			this.cboMethodChromagram.Size = new System.Drawing.Size(157, 21);
			this.cboMethodChromagram.TabIndex = 143;
			// 
			// lblSpectrumAlign
			// 
			this.lblSpectrumAlign.AutoSize = true;
			this.lblSpectrumAlign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblSpectrumAlign.Location = new System.Drawing.Point(7, 120);
			this.lblSpectrumAlign.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSpectrumAlign.Name = "lblSpectrumAlign";
			this.lblSpectrumAlign.Size = new System.Drawing.Size(49, 13);
			this.lblSpectrumAlign.TabIndex = 142;
			this.lblSpectrumAlign.Text = "Align To:";
			// 
			// cboAlignChromagram
			// 
			this.cboAlignChromagram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignChromagram.DropDownWidth = 180;
			this.cboAlignChromagram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboAlignChromagram.FormattingEnabled = true;
			this.cboAlignChromagram.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Bars / Whole Notes",
            "Full Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes",
            "Note Onsets"});
			this.cboAlignChromagram.Location = new System.Drawing.Point(7, 138);
			this.cboAlignChromagram.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboAlignChromagram.Name = "cboAlignChromagram";
			this.cboAlignChromagram.Size = new System.Drawing.Size(157, 21);
			this.cboAlignChromagram.TabIndex = 141;
			// 
			// grpPolyphonic
			// 
			this.grpPolyphonic.Controls.Add(this.lblTranscribePlugin);
			this.grpPolyphonic.Controls.Add(this.lblTranscribeLabels);
			this.grpPolyphonic.Controls.Add(this.cboLabelsPolyphonic);
			this.grpPolyphonic.Controls.Add(this.chkPolyphonic);
			this.grpPolyphonic.Controls.Add(this.cboMethodPolyphonic);
			this.grpPolyphonic.Controls.Add(this.lblTranscribeAlign);
			this.grpPolyphonic.Controls.Add(this.cboAlignPolyphonic);
			this.grpPolyphonic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.grpPolyphonic.Location = new System.Drawing.Point(7, 477);
			this.grpPolyphonic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpPolyphonic.Name = "grpPolyphonic";
			this.grpPolyphonic.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpPolyphonic.Size = new System.Drawing.Size(172, 173);
			this.grpPolyphonic.TabIndex = 138;
			this.grpPolyphonic.TabStop = false;
			this.grpPolyphonic.Text = "   Polyphonic Transcription";
			// 
			// lblTranscribePlugin
			// 
			this.lblTranscribePlugin.AutoSize = true;
			this.lblTranscribePlugin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTranscribePlugin.Location = new System.Drawing.Point(7, 18);
			this.lblTranscribePlugin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTranscribePlugin.Name = "lblTranscribePlugin";
			this.lblTranscribePlugin.Size = new System.Drawing.Size(86, 13);
			this.lblTranscribePlugin.TabIndex = 148;
			this.lblTranscribePlugin.Text = "Plugin / Method:";
			// 
			// lblTranscribeLabels
			// 
			this.lblTranscribeLabels.AutoSize = true;
			this.lblTranscribeLabels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTranscribeLabels.Location = new System.Drawing.Point(7, 69);
			this.lblTranscribeLabels.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTranscribeLabels.Name = "lblTranscribeLabels";
			this.lblTranscribeLabels.Size = new System.Drawing.Size(41, 13);
			this.lblTranscribeLabels.TabIndex = 147;
			this.lblTranscribeLabels.Text = "Labels:";
			// 
			// cboLabelsPolyphonic
			// 
			this.cboLabelsPolyphonic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsPolyphonic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboLabelsPolyphonic.FormattingEnabled = true;
			this.cboLabelsPolyphonic.Items.AddRange(new object[] {
            "MIDI Note Numbers",
            "Note Names"});
			this.cboLabelsPolyphonic.Location = new System.Drawing.Point(7, 88);
			this.cboLabelsPolyphonic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboLabelsPolyphonic.Name = "cboLabelsPolyphonic";
			this.cboLabelsPolyphonic.Size = new System.Drawing.Size(157, 21);
			this.cboLabelsPolyphonic.TabIndex = 146;
			// 
			// chkPolyphonic
			// 
			this.chkPolyphonic.AutoSize = true;
			this.chkPolyphonic.Checked = true;
			this.chkPolyphonic.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPolyphonic.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkPolyphonic.Location = new System.Drawing.Point(0, 0);
			this.chkPolyphonic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkPolyphonic.Name = "chkPolyphonic";
			this.chkPolyphonic.Size = new System.Drawing.Size(15, 14);
			this.chkPolyphonic.TabIndex = 145;
			this.chkPolyphonic.Tag = "10";
			this.chkPolyphonic.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.chkPolyphonic.UseVisualStyleBackColor = true;
			// 
			// cboMethodPolyphonic
			// 
			this.cboMethodPolyphonic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodPolyphonic.DropDownWidth = 200;
			this.cboMethodPolyphonic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboMethodPolyphonic.FormattingEnabled = true;
			this.cboMethodPolyphonic.Items.AddRange(new object[] {
            "Queen Mary Polyphonic Transcription",
            "Silvet Note Transcription",
            "Alicante Polyphonic Transcription",
            "Aubio Note Tracker"});
			this.cboMethodPolyphonic.Location = new System.Drawing.Point(7, 37);
			this.cboMethodPolyphonic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboMethodPolyphonic.Name = "cboMethodPolyphonic";
			this.cboMethodPolyphonic.Size = new System.Drawing.Size(157, 21);
			this.cboMethodPolyphonic.TabIndex = 143;
			// 
			// lblTranscribeAlign
			// 
			this.lblTranscribeAlign.AutoSize = true;
			this.lblTranscribeAlign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTranscribeAlign.Location = new System.Drawing.Point(7, 114);
			this.lblTranscribeAlign.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTranscribeAlign.Name = "lblTranscribeAlign";
			this.lblTranscribeAlign.Size = new System.Drawing.Size(49, 13);
			this.lblTranscribeAlign.TabIndex = 142;
			this.lblTranscribeAlign.Text = "Align To:";
			// 
			// cboAlignPolyphonic
			// 
			this.cboAlignPolyphonic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignPolyphonic.DropDownWidth = 180;
			this.cboAlignPolyphonic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboAlignPolyphonic.FormattingEnabled = true;
			this.cboAlignPolyphonic.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes",
            "Note Onsets"});
			this.cboAlignPolyphonic.Location = new System.Drawing.Point(7, 133);
			this.cboAlignPolyphonic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboAlignPolyphonic.Name = "cboAlignPolyphonic";
			this.cboAlignPolyphonic.Size = new System.Drawing.Size(157, 21);
			this.cboAlignPolyphonic.TabIndex = 141;
			// 
			// chkChromathing
			// 
			this.chkChromathing.AutoSize = true;
			this.chkChromathing.Enabled = false;
			this.chkChromathing.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkChromathing.Location = new System.Drawing.Point(742, 91);
			this.chkChromathing.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkChromathing.Name = "chkChromathing";
			this.chkChromathing.Size = new System.Drawing.Size(92, 17);
			this.chkChromathing.TabIndex = 134;
			this.chkChromathing.Tag = "8";
			this.chkChromathing.Text = "Chromagram";
			this.chkChromathing.UseVisualStyleBackColor = true;
			this.chkChromathing.Visible = false;
			this.chkChromathing.CheckedChanged += new System.EventHandler(this.chkTiming_CheckedChanged);
			// 
			// grpOnsets
			// 
			this.grpOnsets.Controls.Add(this.pnlOnsetSensitivity);
			this.grpOnsets.Controls.Add(this.lblDetectOnsets);
			this.grpOnsets.Controls.Add(this.cboOnsetsDetect);
			this.grpOnsets.Controls.Add(this.lblOnsetsLabels);
			this.grpOnsets.Controls.Add(this.cboLabelsOnsets);
			this.grpOnsets.Controls.Add(this.chkNoteOnsets);
			this.grpOnsets.Controls.Add(this.lblOnsetsPlugin);
			this.grpOnsets.Controls.Add(this.cboMethodOnsets);
			this.grpOnsets.Controls.Add(this.lblOnsetsAlign);
			this.grpOnsets.Controls.Add(this.cboAlignOnsets);
			this.grpOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.grpOnsets.Location = new System.Drawing.Point(366, 91);
			this.grpOnsets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpOnsets.Name = "grpOnsets";
			this.grpOnsets.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpOnsets.Size = new System.Drawing.Size(172, 378);
			this.grpOnsets.TabIndex = 137;
			this.grpOnsets.TabStop = false;
			this.grpOnsets.Text = "   Note Onsets ";
			// 
			// pnlOnsetSensitivity
			// 
			this.pnlOnsetSensitivity.Controls.Add(this.vscSensitivity);
			this.pnlOnsetSensitivity.Controls.Add(this.txtOnsetSensitivity);
			this.pnlOnsetSensitivity.Controls.Add(this.lblOnsetsSensitivity);
			this.pnlOnsetSensitivity.Location = new System.Drawing.Point(9, 248);
			this.pnlOnsetSensitivity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlOnsetSensitivity.Name = "pnlOnsetSensitivity";
			this.pnlOnsetSensitivity.Size = new System.Drawing.Size(118, 27);
			this.pnlOnsetSensitivity.TabIndex = 152;
			// 
			// vscSensitivity
			// 
			this.vscSensitivity.Location = new System.Drawing.Point(99, -1);
			this.vscSensitivity.Minimum = 10;
			this.vscSensitivity.Name = "vscSensitivity";
			this.vscSensitivity.Size = new System.Drawing.Size(16, 21);
			this.vscSensitivity.TabIndex = 28;
			this.vscSensitivity.Value = 10;
			this.vscSensitivity.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
			// 
			// txtOnsetSensitivity
			// 
			this.txtOnsetSensitivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtOnsetSensitivity.Location = new System.Drawing.Point(63, 0);
			this.txtOnsetSensitivity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtOnsetSensitivity.Name = "txtOnsetSensitivity";
			this.txtOnsetSensitivity.ReadOnly = true;
			this.txtOnsetSensitivity.Size = new System.Drawing.Size(33, 20);
			this.txtOnsetSensitivity.TabIndex = 7;
			this.txtOnsetSensitivity.Text = "50";
			// 
			// lblOnsetsSensitivity
			// 
			this.lblOnsetsSensitivity.AutoSize = true;
			this.lblOnsetsSensitivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblOnsetsSensitivity.Location = new System.Drawing.Point(1, 5);
			this.lblOnsetsSensitivity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblOnsetsSensitivity.Name = "lblOnsetsSensitivity";
			this.lblOnsetsSensitivity.Size = new System.Drawing.Size(57, 13);
			this.lblOnsetsSensitivity.TabIndex = 6;
			this.lblOnsetsSensitivity.Text = "Sensitivity:";
			this.lblOnsetsSensitivity.UseMnemonic = false;
			// 
			// lblDetectOnsets
			// 
			this.lblDetectOnsets.AutoSize = true;
			this.lblDetectOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblDetectOnsets.Location = new System.Drawing.Point(9, 198);
			this.lblDetectOnsets.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDetectOnsets.Name = "lblDetectOnsets";
			this.lblDetectOnsets.Size = new System.Drawing.Size(95, 13);
			this.lblDetectOnsets.TabIndex = 151;
			this.lblDetectOnsets.Text = "Detection Method:";
			// 
			// cboOnsetsDetect
			// 
			this.cboOnsetsDetect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOnsetsDetect.DropDownWidth = 300;
			this.cboOnsetsDetect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboOnsetsDetect.FormattingEnabled = true;
			this.cboOnsetsDetect.Items.AddRange(new object[] {
            "\'Complex Domain\' (Strings/Mixed: Piano, Guitar)",
            "\'Spectral Difference\' (Percussion: Drums, Chimes)",
            "\'Phase Deviation\' (Wind: Flute, Sax, Trumpet)",
            "\'Broadband Energy Rise\' (Percussion mixed with other)"});
			this.cboOnsetsDetect.Location = new System.Drawing.Point(9, 217);
			this.cboOnsetsDetect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboOnsetsDetect.Name = "cboOnsetsDetect";
			this.cboOnsetsDetect.Size = new System.Drawing.Size(157, 21);
			this.cboOnsetsDetect.TabIndex = 150;
			// 
			// lblOnsetsLabels
			// 
			this.lblOnsetsLabels.AutoSize = true;
			this.lblOnsetsLabels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblOnsetsLabels.Location = new System.Drawing.Point(7, 72);
			this.lblOnsetsLabels.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblOnsetsLabels.Name = "lblOnsetsLabels";
			this.lblOnsetsLabels.Size = new System.Drawing.Size(41, 13);
			this.lblOnsetsLabels.TabIndex = 149;
			this.lblOnsetsLabels.Text = "Labels:";
			// 
			// cboLabelsOnsets
			// 
			this.cboLabelsOnsets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboLabelsOnsets.FormattingEnabled = true;
			this.cboLabelsOnsets.Items.AddRange(new object[] {
            "None",
            "Note Names",
            "MIDI Note Numbers"});
			this.cboLabelsOnsets.Location = new System.Drawing.Point(7, 90);
			this.cboLabelsOnsets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboLabelsOnsets.Name = "cboLabelsOnsets";
			this.cboLabelsOnsets.Size = new System.Drawing.Size(157, 21);
			this.cboLabelsOnsets.TabIndex = 148;
			// 
			// chkNoteOnsets
			// 
			this.chkNoteOnsets.AutoSize = true;
			this.chkNoteOnsets.Checked = true;
			this.chkNoteOnsets.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkNoteOnsets.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkNoteOnsets.Location = new System.Drawing.Point(0, 0);
			this.chkNoteOnsets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkNoteOnsets.Name = "chkNoteOnsets";
			this.chkNoteOnsets.Size = new System.Drawing.Size(15, 14);
			this.chkNoteOnsets.TabIndex = 145;
			this.chkNoteOnsets.Tag = "5";
			this.chkNoteOnsets.UseVisualStyleBackColor = true;
			// 
			// lblOnsetsPlugin
			// 
			this.lblOnsetsPlugin.AutoSize = true;
			this.lblOnsetsPlugin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblOnsetsPlugin.Location = new System.Drawing.Point(7, 18);
			this.lblOnsetsPlugin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblOnsetsPlugin.Name = "lblOnsetsPlugin";
			this.lblOnsetsPlugin.Size = new System.Drawing.Size(86, 13);
			this.lblOnsetsPlugin.TabIndex = 144;
			this.lblOnsetsPlugin.Text = "Plugin / Method:";
			// 
			// cboMethodOnsets
			// 
			this.cboMethodOnsets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodOnsets.DropDownWidth = 200;
			this.cboMethodOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
			this.cboMethodOnsets.Location = new System.Drawing.Point(7, 37);
			this.cboMethodOnsets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboMethodOnsets.Name = "cboMethodOnsets";
			this.cboMethodOnsets.Size = new System.Drawing.Size(157, 21);
			this.cboMethodOnsets.TabIndex = 143;
			this.cboMethodOnsets.SelectedIndexChanged += new System.EventHandler(this.cboOnsetsPlugin_SelectedIndexChanged);
			// 
			// lblOnsetsAlign
			// 
			this.lblOnsetsAlign.AutoSize = true;
			this.lblOnsetsAlign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblOnsetsAlign.Location = new System.Drawing.Point(7, 118);
			this.lblOnsetsAlign.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblOnsetsAlign.Name = "lblOnsetsAlign";
			this.lblOnsetsAlign.Size = new System.Drawing.Size(49, 13);
			this.lblOnsetsAlign.TabIndex = 142;
			this.lblOnsetsAlign.Text = "Align To:";
			// 
			// cboAlignOnsets
			// 
			this.cboAlignOnsets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignOnsets.DropDownWidth = 180;
			this.cboAlignOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboAlignOnsets.FormattingEnabled = true;
			this.cboAlignOnsets.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps",
            "Beats / Quarter Notes",
            "Half Beats / Eighth Notes",
            "Quarter Beats / Sixteenth Notes"});
			this.cboAlignOnsets.Location = new System.Drawing.Point(7, 136);
			this.cboAlignOnsets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboAlignOnsets.Name = "cboAlignOnsets";
			this.cboAlignOnsets.Size = new System.Drawing.Size(164, 21);
			this.cboAlignOnsets.TabIndex = 141;
			// 
			// grpBarsBeats
			// 
			this.grpBarsBeats.Controls.Add(this.lblDetectBarBeats);
			this.grpBarsBeats.Controls.Add(this.cboDetectBarBeats);
			this.grpBarsBeats.Controls.Add(this.label1);
			this.grpBarsBeats.Controls.Add(this.cboLabelsBarBeats);
			this.grpBarsBeats.Controls.Add(this.chkBarsBeats);
			this.grpBarsBeats.Controls.Add(this.pnlNotes);
			this.grpBarsBeats.Controls.Add(this.lblMethod);
			this.grpBarsBeats.Controls.Add(this.cboMethodBarsBeats);
			this.grpBarsBeats.Controls.Add(this.lblAlignBarsBeats);
			this.grpBarsBeats.Controls.Add(this.cboAlignBarBeats);
			this.grpBarsBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.grpBarsBeats.Location = new System.Drawing.Point(188, 89);
			this.grpBarsBeats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpBarsBeats.Name = "grpBarsBeats";
			this.grpBarsBeats.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpBarsBeats.Size = new System.Drawing.Size(172, 381);
			this.grpBarsBeats.TabIndex = 136;
			this.grpBarsBeats.TabStop = false;
			this.grpBarsBeats.Text = "   Bars && Beats ";
			// 
			// lblDetectBarBeats
			// 
			this.lblDetectBarBeats.AutoSize = true;
			this.lblDetectBarBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblDetectBarBeats.Location = new System.Drawing.Point(8, 314);
			this.lblDetectBarBeats.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDetectBarBeats.Name = "lblDetectBarBeats";
			this.lblDetectBarBeats.Size = new System.Drawing.Size(95, 13);
			this.lblDetectBarBeats.TabIndex = 163;
			this.lblDetectBarBeats.Text = "Detection Method:";
			// 
			// cboDetectBarBeats
			// 
			this.cboDetectBarBeats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDetectBarBeats.DropDownWidth = 300;
			this.cboDetectBarBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboDetectBarBeats.FormattingEnabled = true;
			this.cboDetectBarBeats.Items.AddRange(new object[] {
            "\'Complex Domain\' (Strings/Mixed: Piano, Guitar)",
            "\'Spectral Difference\' (Percussion: Drums, Chimes)",
            "\'Phase Deviation\' (Wind: Flute, Sax, Trumpet)",
            "\'Broadband Energy Rise\' (Percussion mixed with other)"});
			this.cboDetectBarBeats.Location = new System.Drawing.Point(7, 332);
			this.cboDetectBarBeats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboDetectBarBeats.Name = "cboDetectBarBeats";
			this.cboDetectBarBeats.Size = new System.Drawing.Size(157, 21);
			this.cboDetectBarBeats.TabIndex = 162;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(7, 67);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 161;
			this.label1.Text = "Labels:";
			// 
			// cboLabelsBarBeats
			// 
			this.cboLabelsBarBeats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelsBarBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboLabelsBarBeats.FormattingEnabled = true;
			this.cboLabelsBarBeats.Items.AddRange(new object[] {
            "None",
            "Note Names",
            "MIDI Note Numbers"});
			this.cboLabelsBarBeats.Location = new System.Drawing.Point(7, 85);
			this.cboLabelsBarBeats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboLabelsBarBeats.Name = "cboLabelsBarBeats";
			this.cboLabelsBarBeats.Size = new System.Drawing.Size(157, 21);
			this.cboLabelsBarBeats.TabIndex = 160;
			// 
			// chkBarsBeats
			// 
			this.chkBarsBeats.AutoSize = true;
			this.chkBarsBeats.Checked = true;
			this.chkBarsBeats.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBarsBeats.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkBarsBeats.Location = new System.Drawing.Point(0, 0);
			this.chkBarsBeats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkBarsBeats.Name = "chkBarsBeats";
			this.chkBarsBeats.Size = new System.Drawing.Size(15, 14);
			this.chkBarsBeats.TabIndex = 156;
			this.chkBarsBeats.Tag = "13";
			this.chkBarsBeats.UseVisualStyleBackColor = true;
			// 
			// pnlNotes
			// 
			this.pnlNotes.Controls.Add(this.chkBeatsThird);
			this.pnlNotes.Controls.Add(this.chkBars);
			this.pnlNotes.Controls.Add(this.chkBeatsFull);
			this.pnlNotes.Controls.Add(this.chkBeatsHalf);
			this.pnlNotes.Controls.Add(this.chkBeatsQuarter);
			this.pnlNotes.Controls.Add(this.lblBeats2Half);
			this.pnlNotes.Controls.Add(this.lblBeats1Full);
			this.pnlNotes.Controls.Add(this.lblBeats4Quarter);
			this.pnlNotes.Controls.Add(this.lblBeats0Bars);
			this.pnlNotes.Location = new System.Drawing.Point(2, 166);
			this.pnlNotes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlNotes.Name = "pnlNotes";
			this.pnlNotes.Size = new System.Drawing.Size(169, 136);
			this.pnlNotes.TabIndex = 155;
			this.pnlNotes.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlNotes_Paint);
			// 
			// chkBeatsThird
			// 
			this.chkBeatsThird.AutoSize = true;
			this.chkBeatsThird.Font = new System.Drawing.Font("Bahnschrift Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkBeatsThird.Location = new System.Drawing.Point(23, 76);
			this.chkBeatsThird.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkBeatsThird.Name = "chkBeatsThird";
			this.chkBeatsThird.Size = new System.Drawing.Size(66, 17);
			this.chkBeatsThird.TabIndex = 141;
			this.chkBeatsThird.Tag = "3";
			this.chkBeatsThird.Text = "Third Beats";
			this.chkBeatsThird.UseVisualStyleBackColor = true;
			// 
			// chkBars
			// 
			this.chkBars.AutoSize = true;
			this.chkBars.Checked = true;
			this.chkBars.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBars.Font = new System.Drawing.Font("Bahnschrift Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkBars.Location = new System.Drawing.Point(23, 3);
			this.chkBars.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkBars.Name = "chkBars";
			this.chkBars.Size = new System.Drawing.Size(88, 17);
			this.chkBars.TabIndex = 139;
			this.chkBars.Tag = "0";
			this.chkBars.Text = "Bars Whole Notes";
			this.chkBars.UseVisualStyleBackColor = true;
			// 
			// chkBeatsFull
			// 
			this.chkBeatsFull.AutoSize = true;
			this.chkBeatsFull.Checked = true;
			this.chkBeatsFull.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBeatsFull.Font = new System.Drawing.Font("Bahnschrift Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkBeatsFull.Location = new System.Drawing.Point(23, 28);
			this.chkBeatsFull.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkBeatsFull.Name = "chkBeatsFull";
			this.chkBeatsFull.Size = new System.Drawing.Size(112, 17);
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
			this.chkBeatsHalf.Font = new System.Drawing.Font("Bahnschrift Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkBeatsHalf.Location = new System.Drawing.Point(23, 52);
			this.chkBeatsHalf.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkBeatsHalf.Name = "chkBeatsHalf";
			this.chkBeatsHalf.Size = new System.Drawing.Size(110, 17);
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
			this.chkBeatsQuarter.Font = new System.Drawing.Font("Bahnschrift Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkBeatsQuarter.Location = new System.Drawing.Point(23, 100);
			this.chkBeatsQuarter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkBeatsQuarter.Name = "chkBeatsQuarter";
			this.chkBeatsQuarter.Size = new System.Drawing.Size(133, 17);
			this.chkBeatsQuarter.TabIndex = 140;
			this.chkBeatsQuarter.Tag = "4";
			this.chkBeatsQuarter.Text = "Quarter Beats Sixteenth Notes";
			this.chkBeatsQuarter.UseVisualStyleBackColor = true;
			// 
			// lblBeats2Half
			// 
			this.lblBeats2Half.AutoSize = true;
			this.lblBeats2Half.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblBeats2Half.Location = new System.Drawing.Point(0, 43);
			this.lblBeats2Half.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBeats2Half.Name = "lblBeats2Half";
			this.lblBeats2Half.Size = new System.Drawing.Size(24, 30);
			this.lblBeats2Half.TabIndex = 149;
			this.lblBeats2Half.Text = "𝅘𝅥𝅮";
			this.lblBeats2Half.Visible = false;
			// 
			// lblBeats1Full
			// 
			this.lblBeats1Full.AutoSize = true;
			this.lblBeats1Full.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblBeats1Full.Location = new System.Drawing.Point(0, 18);
			this.lblBeats1Full.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBeats1Full.Name = "lblBeats1Full";
			this.lblBeats1Full.Size = new System.Drawing.Size(21, 30);
			this.lblBeats1Full.TabIndex = 150;
			this.lblBeats1Full.Text = "𝅘𝅥";
			this.lblBeats1Full.Visible = false;
			// 
			// lblBeats4Quarter
			// 
			this.lblBeats4Quarter.AutoSize = true;
			this.lblBeats4Quarter.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblBeats4Quarter.Location = new System.Drawing.Point(0, 90);
			this.lblBeats4Quarter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBeats4Quarter.Name = "lblBeats4Quarter";
			this.lblBeats4Quarter.Size = new System.Drawing.Size(24, 30);
			this.lblBeats4Quarter.TabIndex = 152;
			this.lblBeats4Quarter.Text = "𝅘𝅥𝅯";
			this.lblBeats4Quarter.Visible = false;
			// 
			// lblBeats0Bars
			// 
			this.lblBeats0Bars.AutoSize = true;
			this.lblBeats0Bars.Font = new System.Drawing.Font("Segoe UI Symbol", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblBeats0Bars.Location = new System.Drawing.Point(0, 0);
			this.lblBeats0Bars.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBeats0Bars.Name = "lblBeats0Bars";
			this.lblBeats0Bars.Size = new System.Drawing.Size(33, 45);
			this.lblBeats0Bars.TabIndex = 153;
			this.lblBeats0Bars.Text = "𝅗";
			this.lblBeats0Bars.Visible = false;
			// 
			// lblMethod
			// 
			this.lblMethod.AutoSize = true;
			this.lblMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblMethod.Location = new System.Drawing.Point(7, 18);
			this.lblMethod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblMethod.Name = "lblMethod";
			this.lblMethod.Size = new System.Drawing.Size(86, 13);
			this.lblMethod.TabIndex = 144;
			this.lblMethod.Text = "Plugin / Method:";
			// 
			// cboMethodBarsBeats
			// 
			this.cboMethodBarsBeats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMethodBarsBeats.DropDownWidth = 200;
			this.cboMethodBarsBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboMethodBarsBeats.FormattingEnabled = true;
			this.cboMethodBarsBeats.Items.AddRange(new object[] {
            "Queen Mary Bar and Beat Tracker",
            "Queen Mary Tempo and Beat Tracker",
            "BeatRoot Beat Tracker",
            "Aubio Beat Tracker"});
			this.cboMethodBarsBeats.Location = new System.Drawing.Point(7, 37);
			this.cboMethodBarsBeats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboMethodBarsBeats.Name = "cboMethodBarsBeats";
			this.cboMethodBarsBeats.Size = new System.Drawing.Size(157, 21);
			this.cboMethodBarsBeats.TabIndex = 143;
			this.cboMethodBarsBeats.SelectedIndexChanged += new System.EventHandler(this.cboBarBeatMethod_SelectedIndexChanged);
			// 
			// lblAlignBarsBeats
			// 
			this.lblAlignBarsBeats.AutoSize = true;
			this.lblAlignBarsBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblAlignBarsBeats.Location = new System.Drawing.Point(7, 113);
			this.lblAlignBarsBeats.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblAlignBarsBeats.Name = "lblAlignBarsBeats";
			this.lblAlignBarsBeats.Size = new System.Drawing.Size(49, 13);
			this.lblAlignBarsBeats.TabIndex = 142;
			this.lblAlignBarsBeats.Text = "Align To:";
			// 
			// cboAlignBarBeats
			// 
			this.cboAlignBarBeats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlignBarBeats.DropDownWidth = 135;
			this.cboAlignBarBeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cboAlignBarBeats.FormattingEnabled = true;
			this.cboAlignBarBeats.Items.AddRange(new object[] {
            "None",
            "25ms 40fps",
            "50ms 20fps"});
			this.cboAlignBarBeats.Location = new System.Drawing.Point(7, 132);
			this.cboAlignBarBeats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboAlignBarBeats.Name = "cboAlignBarBeats";
			this.cboAlignBarBeats.Size = new System.Drawing.Size(157, 21);
			this.cboAlignBarBeats.TabIndex = 141;
			// 
			// lblStep2B
			// 
			this.lblStep2B.AutoSize = true;
			this.lblStep2B.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
			this.lblStep2B.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep2B.Location = new System.Drawing.Point(7, -6);
			this.lblStep2B.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStep2B.Name = "lblStep2B";
			this.lblStep2B.Size = new System.Drawing.Size(21, 24);
			this.lblStep2B.TabIndex = 121;
			this.lblStep2B.Text = "2";
			// 
			// chkFlux
			// 
			this.chkFlux.AutoSize = true;
			this.chkFlux.Enabled = false;
			this.chkFlux.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkFlux.Location = new System.Drawing.Point(742, 144);
			this.chkFlux.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkFlux.Name = "chkFlux";
			this.chkFlux.Size = new System.Drawing.Size(47, 17);
			this.chkFlux.TabIndex = 128;
			this.chkFlux.Tag = "12";
			this.chkFlux.Text = "Flux";
			this.chkFlux.UseVisualStyleBackColor = true;
			this.chkFlux.Visible = false;
			this.chkFlux.CheckedChanged += new System.EventHandler(this.chkTiming_CheckedChanged);
			// 
			// pnlVamping
			// 
			this.pnlVamping.Controls.Add(this.lblSongName);
			this.pnlVamping.Controls.Add(this.lblAnalyzing);
			this.pnlVamping.Controls.Add(this.lblWait);
			this.pnlVamping.Controls.Add(this.lblVampRed);
			this.pnlVamping.Location = new System.Drawing.Point(186, 203);
			this.pnlVamping.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlVamping.Name = "pnlVamping";
			this.pnlVamping.Size = new System.Drawing.Size(390, 83);
			this.pnlVamping.TabIndex = 125;
			this.pnlVamping.Visible = false;
			this.pnlVamping.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlVamping_Paint);
			// 
			// lblSongName
			// 
			this.lblSongName.AutoSize = true;
			this.lblSongName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblSongName.ForeColor = System.Drawing.Color.Blue;
			this.lblSongName.Location = new System.Drawing.Point(6, 38);
			this.lblSongName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSongName.Name = "lblSongName";
			this.lblSongName.Size = new System.Drawing.Size(216, 18);
			this.lblSongName.TabIndex = 3;
			this.lblSongName.Text = "Blue Christmas by Elvis Presley";
			this.lblSongName.Click += new System.EventHandler(this.lblSongName_Click);
			// 
			// lblAnalyzing
			// 
			this.lblAnalyzing.AutoSize = true;
			this.lblAnalyzing.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.lblAnalyzing.ForeColor = System.Drawing.Color.Red;
			this.lblAnalyzing.Location = new System.Drawing.Point(6, 8);
			this.lblAnalyzing.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblAnalyzing.Name = "lblAnalyzing";
			this.lblAnalyzing.Size = new System.Drawing.Size(189, 24);
			this.lblAnalyzing.TabIndex = 2;
			this.lblAnalyzing.Text = "Analyzing your song";
			// 
			// lblWait
			// 
			this.lblWait.AutoSize = true;
			this.lblWait.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblWait.ForeColor = System.Drawing.Color.Red;
			this.lblWait.Location = new System.Drawing.Point(70, 61);
			this.lblWait.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblWait.Name = "lblWait";
			this.lblWait.Size = new System.Drawing.Size(75, 15);
			this.lblWait.TabIndex = 1;
			this.lblWait.Text = "Please wait...";
			// 
			// lblVampRed
			// 
			this.lblVampRed.AutoSize = true;
			this.lblVampRed.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
			this.lblVampRed.ForeColor = System.Drawing.Color.Red;
			this.lblVampRed.Location = new System.Drawing.Point(4, 6);
			this.lblVampRed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblVampRed.Name = "lblVampRed";
			this.lblVampRed.Size = new System.Drawing.Size(62, 24);
			this.lblVampRed.TabIndex = 0;
			this.lblVampRed.Text = "Vamp";
			this.lblVampRed.Visible = false;
			// 
			// grpExplore
			// 
			this.grpExplore.Controls.Add(this.btnExploreVamp);
			this.grpExplore.Controls.Add(this.btnExplorexLights);
			this.grpExplore.Controls.Add(this.btnExploreTemp);
			this.grpExplore.Controls.Add(this.btnCmdTemp);
			this.grpExplore.Controls.Add(this.btnLaunchxLights);
			this.grpExplore.Controls.Add(this.btnSequenceEditor);
			this.grpExplore.Controls.Add(this.btnExploreLOR);
			this.grpExplore.Location = new System.Drawing.Point(777, 8);
			this.grpExplore.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpExplore.Name = "grpExplore";
			this.grpExplore.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpExplore.Size = new System.Drawing.Size(366, 65);
			this.grpExplore.TabIndex = 180;
			this.grpExplore.TabStop = false;
			this.grpExplore.Text = " Explore... ";
			// 
			// btnResetDefaults
			// 
			this.btnResetDefaults.AllowDrop = true;
			this.btnResetDefaults.Location = new System.Drawing.Point(1203, 303);
			this.btnResetDefaults.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnResetDefaults.Name = "btnResetDefaults";
			this.btnResetDefaults.Size = new System.Drawing.Size(144, 27);
			this.btnResetDefaults.TabIndex = 158;
			this.btnResetDefaults.Text = "Reset to Defaults";
			this.btnResetDefaults.UseVisualStyleBackColor = true;
			this.btnResetDefaults.Click += new System.EventHandler(this.btnResetDefaults_Click);
			// 
			// grpAudio
			// 
			this.grpAudio.Controls.Add(this.lblSongTime);
			this.grpAudio.Controls.Add(this.btnBrowseAudio);
			this.grpAudio.Controls.Add(this.txtFileAudio);
			this.grpAudio.Controls.Add(this.lblFileAudio);
			this.grpAudio.Controls.Add(this.lblStep1);
			this.grpAudio.Enabled = false;
			this.grpAudio.Location = new System.Drawing.Point(14, 14);
			this.grpAudio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpAudio.Name = "grpAudio";
			this.grpAudio.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpAudio.Size = new System.Drawing.Size(390, 76);
			this.grpAudio.TabIndex = 122;
			this.grpAudio.TabStop = false;
			this.grpAudio.Text = "      Select Audio File";
			// 
			// lblSongTime
			// 
			this.lblSongTime.AutoSize = true;
			this.lblSongTime.Font = new System.Drawing.Font("DejaVu Sans Mono", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblSongTime.ForeColor = System.Drawing.Color.DarkOliveGreen;
			this.lblSongTime.Location = new System.Drawing.Point(278, 14);
			this.lblSongTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSongTime.Name = "lblSongTime";
			this.lblSongTime.Size = new System.Drawing.Size(40, 10);
			this.lblSongTime.TabIndex = 163;
			this.lblSongTime.Text = "0:00.00";
			this.lblSongTime.Visible = false;
			// 
			// btnBrowseAudio
			// 
			this.btnBrowseAudio.AllowDrop = true;
			this.btnBrowseAudio.Location = new System.Drawing.Point(285, 40);
			this.btnBrowseAudio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnBrowseAudio.Name = "btnBrowseAudio";
			this.btnBrowseAudio.Size = new System.Drawing.Size(88, 23);
			this.btnBrowseAudio.TabIndex = 124;
			this.btnBrowseAudio.Text = "Browse...";
			this.btnBrowseAudio.UseVisualStyleBackColor = true;
			this.btnBrowseAudio.Click += new System.EventHandler(this.btnBrowseAudio_Click);
			// 
			// txtFileAudio
			// 
			this.txtFileAudio.AllowDrop = true;
			this.txtFileAudio.Location = new System.Drawing.Point(22, 40);
			this.txtFileAudio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtFileAudio.Name = "txtFileAudio";
			this.txtFileAudio.ReadOnly = true;
			this.txtFileAudio.Size = new System.Drawing.Size(255, 23);
			this.txtFileAudio.TabIndex = 123;
			this.txtFileAudio.TextChanged += new System.EventHandler(this.txtFileAudio_TextChanged);
			this.txtFileAudio.Leave += new System.EventHandler(this.txtFileAudio_Leave);
			// 
			// lblFileAudio
			// 
			this.lblFileAudio.AllowDrop = true;
			this.lblFileAudio.AutoSize = true;
			this.lblFileAudio.Location = new System.Drawing.Point(19, 22);
			this.lblFileAudio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFileAudio.Name = "lblFileAudio";
			this.lblFileAudio.Size = new System.Drawing.Size(60, 15);
			this.lblFileAudio.TabIndex = 125;
			this.lblFileAudio.Text = "Audio File";
			// 
			// lblStep1
			// 
			this.lblStep1.AutoSize = true;
			this.lblStep1.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
			this.lblStep1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep1.Location = new System.Drawing.Point(7, -6);
			this.lblStep1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStep1.Name = "lblStep1";
			this.lblStep1.Size = new System.Drawing.Size(21, 24);
			this.lblStep1.TabIndex = 122;
			this.lblStep1.Text = "1";
			// 
			// grpSavex
			// 
			this.grpSavex.Controls.Add(this.picxLights);
			this.grpSavex.Controls.Add(this.panel2);
			this.grpSavex.Controls.Add(this.btnSaveOptions);
			this.grpSavex.Controls.Add(this.lblStep4A);
			this.grpSavex.Controls.Add(this.lblFilexTimings);
			this.grpSavex.Controls.Add(this.btnSavexL);
			this.grpSavex.Controls.Add(this.txtSaveNamexL);
			this.grpSavex.Enabled = false;
			this.grpSavex.Location = new System.Drawing.Point(14, 522);
			this.grpSavex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpSavex.Name = "grpSavex";
			this.grpSavex.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpSavex.Size = new System.Drawing.Size(390, 128);
			this.grpSavex.TabIndex = 123;
			this.grpSavex.TabStop = false;
			this.grpSavex.Text = "          Save                              Timings";
			// 
			// picxLights
			// 
			this.picxLights.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picxLights.BackgroundImage")));
			this.picxLights.InitialImage = ((System.Drawing.Image)(resources.GetObject("picxLights.InitialImage")));
			this.picxLights.Location = new System.Drawing.Point(66, -2);
			this.picxLights.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picxLights.Name = "picxLights";
			this.picxLights.Size = new System.Drawing.Size(85, 26);
			this.picxLights.TabIndex = 137;
			this.picxLights.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.optOnePer);
			this.panel2.Controls.Add(this.optMultiPer);
			this.panel2.Location = new System.Drawing.Point(22, 22);
			this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(323, 30);
			this.panel2.TabIndex = 136;
			// 
			// optOnePer
			// 
			this.optOnePer.AutoSize = true;
			this.optOnePer.Checked = true;
			this.optOnePer.Location = new System.Drawing.Point(149, 6);
			this.optOnePer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.optOnePer.Name = "optOnePer";
			this.optOnePer.Size = new System.Drawing.Size(143, 22);
			this.optOnePer.TabIndex = 139;
			this.optOnePer.TabStop = true;
			this.optOnePer.Text = "Individual Timing Files";
			this.optOnePer.UseCompatibleTextRendering = true;
			this.optOnePer.UseVisualStyleBackColor = true;
			// 
			// optMultiPer
			// 
			this.optMultiPer.AutoSize = true;
			this.optMultiPer.Location = new System.Drawing.Point(4, 6);
			this.optMultiPer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.optMultiPer.Name = "optMultiPer";
			this.optMultiPer.Size = new System.Drawing.Size(118, 22);
			this.optMultiPer.TabIndex = 138;
			this.optMultiPer.Text = "Single Timing File";
			this.optMultiPer.UseCompatibleTextRendering = true;
			this.optMultiPer.UseVisualStyleBackColor = true;
			// 
			// btnSaveOptions
			// 
			this.btnSaveOptions.AllowDrop = true;
			this.btnSaveOptions.Location = new System.Drawing.Point(220, 111);
			this.btnSaveOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnSaveOptions.Name = "btnSaveOptions";
			this.btnSaveOptions.Size = new System.Drawing.Size(88, 27);
			this.btnSaveOptions.TabIndex = 129;
			this.btnSaveOptions.Text = "Settings...";
			this.btnSaveOptions.UseVisualStyleBackColor = true;
			this.btnSaveOptions.Visible = false;
			this.btnSaveOptions.Click += new System.EventHandler(this.btnSaveOptions_Click_1);
			// 
			// lblStep4A
			// 
			this.lblStep4A.AutoSize = true;
			this.lblStep4A.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
			this.lblStep4A.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep4A.Location = new System.Drawing.Point(7, -6);
			this.lblStep4A.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStep4A.Name = "lblStep4A";
			this.lblStep4A.Size = new System.Drawing.Size(32, 24);
			this.lblStep4A.TabIndex = 127;
			this.lblStep4A.Text = "4a";
			// 
			// lblFilexTimings
			// 
			this.lblFilexTimings.AllowDrop = true;
			this.lblFilexTimings.AutoSize = true;
			this.lblFilexTimings.Location = new System.Drawing.Point(19, 59);
			this.lblFilexTimings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFilexTimings.Name = "lblFilexTimings";
			this.lblFilexTimings.Size = new System.Drawing.Size(111, 15);
			this.lblFilexTimings.TabIndex = 126;
			this.lblFilexTimings.Text = "Timing Filename(s):";
			// 
			// btnSavexL
			// 
			this.btnSavexL.AllowDrop = true;
			this.btnSavexL.Location = new System.Drawing.Point(285, 74);
			this.btnSavexL.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnSavexL.Name = "btnSavexL";
			this.btnSavexL.Size = new System.Drawing.Size(88, 27);
			this.btnSavexL.TabIndex = 125;
			this.btnSavexL.Text = "Save As...";
			this.btnSavexL.UseVisualStyleBackColor = true;
			this.btnSavexL.Click += new System.EventHandler(this.btnSavexL_Click);
			// 
			// txtSaveNamexL
			// 
			this.txtSaveNamexL.AllowDrop = true;
			this.txtSaveNamexL.Location = new System.Drawing.Point(22, 77);
			this.txtSaveNamexL.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtSaveNamexL.Name = "txtSaveNamexL";
			this.txtSaveNamexL.ReadOnly = true;
			this.txtSaveNamexL.Size = new System.Drawing.Size(255, 23);
			this.txtSaveNamexL.TabIndex = 124;
			// 
			// imlTreeIcons
			// 
			this.imlTreeIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imlTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeIcons.ImageStream")));
			this.imlTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlTreeIcons.Images.SetKeyName(0, "LORTrack4");
			this.imlTreeIcons.Images.SetKeyName(1, "LORChannelGroup4");
			this.imlTreeIcons.Images.SetKeyName(2, "LORRGBChannel4");
			this.imlTreeIcons.Images.SetKeyName(3, "LORChannel4");
			this.imlTreeIcons.Images.SetKeyName(4, "RedCh");
			this.imlTreeIcons.Images.SetKeyName(5, "GrnCh");
			this.imlTreeIcons.Images.SetKeyName(6, "BluCh");
			// 
			// grpAnalyze
			// 
			this.grpAnalyze.Controls.Add(this.btnReanalyze);
			this.grpAnalyze.Controls.Add(this.lblVampTime);
			this.grpAnalyze.Controls.Add(this.btnOK);
			this.grpAnalyze.Controls.Add(this.lblStep3);
			this.grpAnalyze.Enabled = false;
			this.grpAnalyze.Location = new System.Drawing.Point(14, 436);
			this.grpAnalyze.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpAnalyze.Name = "grpAnalyze";
			this.grpAnalyze.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpAnalyze.Size = new System.Drawing.Size(390, 76);
			this.grpAnalyze.TabIndex = 126;
			this.grpAnalyze.TabStop = false;
			this.grpAnalyze.Text = "      Analyze the Audio File";
			// 
			// btnReanalyze
			// 
			this.btnReanalyze.AllowDrop = true;
			this.btnReanalyze.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnReanalyze.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnReanalyze.Location = new System.Drawing.Point(189, 23);
			this.btnReanalyze.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnReanalyze.Name = "btnReanalyze";
			this.btnReanalyze.Size = new System.Drawing.Size(136, 47);
			this.btnReanalyze.TabIndex = 165;
			this.btnReanalyze.Text = "Change Options and\r\nAnalyze Again...";
			this.btnReanalyze.UseVisualStyleBackColor = true;
			this.btnReanalyze.Visible = false;
			this.btnReanalyze.Click += new System.EventHandler(this.btnReanalyze_Click);
			// 
			// lblVampTime
			// 
			this.lblVampTime.AutoSize = true;
			this.lblVampTime.Font = new System.Drawing.Font("DejaVu Sans Mono", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblVampTime.ForeColor = System.Drawing.Color.DarkOliveGreen;
			this.lblVampTime.Location = new System.Drawing.Point(278, 22);
			this.lblVampTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblVampTime.Name = "lblVampTime";
			this.lblVampTime.Size = new System.Drawing.Size(40, 10);
			this.lblVampTime.TabIndex = 164;
			this.lblVampTime.Text = "0:00.00";
			this.lblVampTime.Visible = false;
			// 
			// btnOK
			// 
			this.btnOK.AllowDrop = true;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
			this.btnOK.Location = new System.Drawing.Point(104, 22);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(136, 47);
			this.btnOK.TabIndex = 124;
			this.btnOK.Text = "Analyze...";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblStep3
			// 
			this.lblStep3.AutoSize = true;
			this.lblStep3.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
			this.lblStep3.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep3.Location = new System.Drawing.Point(7, -6);
			this.lblStep3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStep3.Name = "lblStep3";
			this.lblStep3.Size = new System.Drawing.Size(21, 24);
			this.lblStep3.TabIndex = 122;
			this.lblStep3.Text = "3";
			// 
			// grpOptions
			// 
			this.grpOptions.BackColor = System.Drawing.SystemColors.Control;
			this.grpOptions.Controls.Add(this.pnlVamping);
			this.grpOptions.Controls.Add(this.picWorking);
			this.grpOptions.Controls.Add(this.lblPickYourPoison);
			this.grpOptions.Controls.Add(this.chkReuse);
			this.grpOptions.Controls.Add(this.picVampire);
			this.grpOptions.Controls.Add(this.lblHelpOnsets);
			this.grpOptions.Controls.Add(this.picPoisonArrow);
			this.grpOptions.Controls.Add(this.lblStep2A);
			this.grpOptions.Enabled = false;
			this.grpOptions.Location = new System.Drawing.Point(14, 103);
			this.grpOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpOptions.Name = "grpOptions";
			this.grpOptions.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpOptions.Size = new System.Drawing.Size(390, 324);
			this.grpOptions.TabIndex = 130;
			this.grpOptions.TabStop = false;
			this.grpOptions.Text = "      Select Timings and Options";
			// 
			// picWorking
			// 
			this.picWorking.Image = ((System.Drawing.Image)(resources.GetObject("picWorking.Image")));
			this.picWorking.Location = new System.Drawing.Point(191, 82);
			this.picWorking.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picWorking.Name = "picWorking";
			this.picWorking.Size = new System.Drawing.Size(132, 40);
			this.picWorking.TabIndex = 157;
			this.picWorking.TabStop = false;
			this.picWorking.Visible = false;
			// 
			// lblPickYourPoison
			// 
			this.lblPickYourPoison.AutoSize = true;
			this.lblPickYourPoison.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.lblPickYourPoison.ForeColor = System.Drawing.Color.DarkViolet;
			this.lblPickYourPoison.Location = new System.Drawing.Point(153, 77);
			this.lblPickYourPoison.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPickYourPoison.Name = "lblPickYourPoison";
			this.lblPickYourPoison.Size = new System.Drawing.Size(162, 24);
			this.lblPickYourPoison.TabIndex = 130;
			this.lblPickYourPoison.Text = "Pick Your Poison";
			// 
			// chkReuse
			// 
			this.chkReuse.AutoSize = true;
			this.chkReuse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.chkReuse.ForeColor = System.Drawing.Color.DeepPink;
			this.chkReuse.Location = new System.Drawing.Point(124, 307);
			this.chkReuse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkReuse.Name = "chkReuse";
			this.chkReuse.Size = new System.Drawing.Size(81, 17);
			this.chkReuse.TabIndex = 135;
			this.chkReuse.Text = "Re-use files";
			this.chkReuse.UseVisualStyleBackColor = true;
			this.chkReuse.Visible = false;
			this.chkReuse.CheckedChanged += new System.EventHandler(this.chkReuse_CheckedChanged);
			// 
			// picVampire
			// 
			this.picVampire.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picVampire.BackgroundImage")));
			this.picVampire.Location = new System.Drawing.Point(26, 52);
			this.picVampire.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picVampire.Name = "picVampire";
			this.picVampire.Size = new System.Drawing.Size(128, 192);
			this.picVampire.TabIndex = 136;
			this.picVampire.TabStop = false;
			// 
			// lblHelpOnsets
			// 
			this.lblHelpOnsets.AutoSize = true;
			this.lblHelpOnsets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
			this.lblHelpOnsets.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblHelpOnsets.Location = new System.Drawing.Point(276, 307);
			this.lblHelpOnsets.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblHelpOnsets.Name = "lblHelpOnsets";
			this.lblHelpOnsets.Size = new System.Drawing.Size(38, 13);
			this.lblHelpOnsets.TabIndex = 156;
			this.lblHelpOnsets.Text = "Help...";
			this.lblHelpOnsets.Click += new System.EventHandler(this.lblHelpOnsets_Click);
			// 
			// picPoisonArrow
			// 
			this.picPoisonArrow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPoisonArrow.BackgroundImage")));
			this.picPoisonArrow.Location = new System.Drawing.Point(253, 114);
			this.picPoisonArrow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picPoisonArrow.Name = "picPoisonArrow";
			this.picPoisonArrow.Size = new System.Drawing.Size(64, 32);
			this.picPoisonArrow.TabIndex = 131;
			this.picPoisonArrow.TabStop = false;
			// 
			// lblStep2A
			// 
			this.lblStep2A.AutoSize = true;
			this.lblStep2A.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
			this.lblStep2A.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep2A.Location = new System.Drawing.Point(7, -6);
			this.lblStep2A.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStep2A.Name = "lblStep2A";
			this.lblStep2A.Size = new System.Drawing.Size(21, 24);
			this.lblStep2A.TabIndex = 122;
			this.lblStep2A.Text = "2";
			// 
			// tmrAni
			// 
			this.tmrAni.Tick += new System.EventHandler(this.tmrAni_Tick);
			// 
			// grpSaveLOR
			// 
			this.grpSaveLOR.Controls.Add(this.lblSeqTime);
			this.grpSaveLOR.Controls.Add(this.chkAutolaunch);
			this.grpSaveLOR.Controls.Add(this.picLOR);
			this.grpSaveLOR.Controls.Add(this.panel3);
			this.grpSaveLOR.Controls.Add(this.button1);
			this.grpSaveLOR.Controls.Add(this.lblStep4B);
			this.grpSaveLOR.Controls.Add(this.lblFileSequence);
			this.grpSaveLOR.Controls.Add(this.btnSaveSeq);
			this.grpSaveLOR.Controls.Add(this.txtSeqName);
			this.grpSaveLOR.Enabled = false;
			this.grpSaveLOR.Location = new System.Drawing.Point(14, 666);
			this.grpSaveLOR.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpSaveLOR.Name = "grpSaveLOR";
			this.grpSaveLOR.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpSaveLOR.Size = new System.Drawing.Size(390, 128);
			this.grpSaveLOR.TabIndex = 136;
			this.grpSaveLOR.TabStop = false;
			this.grpSaveLOR.Text = "          Save                                Sequence";
			// 
			// lblSeqTime
			// 
			this.lblSeqTime.AutoSize = true;
			this.lblSeqTime.Font = new System.Drawing.Font("DejaVu Sans Mono", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblSeqTime.ForeColor = System.Drawing.Color.DarkOliveGreen;
			this.lblSeqTime.Location = new System.Drawing.Point(278, 62);
			this.lblSeqTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSeqTime.Name = "lblSeqTime";
			this.lblSeqTime.Size = new System.Drawing.Size(40, 10);
			this.lblSeqTime.TabIndex = 165;
			this.lblSeqTime.Text = "0:00.00";
			this.lblSeqTime.Visible = false;
			// 
			// chkAutolaunch
			// 
			this.chkAutolaunch.AutoSize = true;
			this.chkAutolaunch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkAutolaunch.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkAutolaunch.Location = new System.Drawing.Point(26, 102);
			this.chkAutolaunch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkAutolaunch.Name = "chkAutolaunch";
			this.chkAutolaunch.Size = new System.Drawing.Size(165, 17);
			this.chkAutolaunch.TabIndex = 138;
			this.chkAutolaunch.Text = "Auto-launch Sequence Editor";
			this.chkAutolaunch.UseVisualStyleBackColor = true;
			// 
			// picLOR
			// 
			this.picLOR.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picLOR.BackgroundImage")));
			this.picLOR.Location = new System.Drawing.Point(66, 0);
			this.picLOR.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picLOR.Name = "picLOR";
			this.picLOR.Size = new System.Drawing.Size(90, 19);
			this.picLOR.TabIndex = 137;
			this.picLOR.TabStop = false;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.optSeqAppend);
			this.panel3.Controls.Add(this.optSeqNew);
			this.panel3.Location = new System.Drawing.Point(22, 22);
			this.panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(323, 30);
			this.panel3.TabIndex = 136;
			// 
			// optSeqAppend
			// 
			this.optSeqAppend.AutoSize = true;
			this.optSeqAppend.Location = new System.Drawing.Point(149, 6);
			this.optSeqAppend.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.optSeqAppend.Name = "optSeqAppend";
			this.optSeqAppend.Size = new System.Drawing.Size(126, 22);
			this.optSeqAppend.TabIndex = 139;
			this.optSeqAppend.Text = "Append to Existing";
			this.optSeqAppend.UseCompatibleTextRendering = true;
			this.optSeqAppend.UseVisualStyleBackColor = true;
			this.optSeqAppend.CheckedChanged += new System.EventHandler(this.optSeqAppend_CheckedChanged);
			// 
			// optSeqNew
			// 
			this.optSeqNew.AutoSize = true;
			this.optSeqNew.Checked = true;
			this.optSeqNew.Location = new System.Drawing.Point(4, 6);
			this.optSeqNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.optSeqNew.Name = "optSeqNew";
			this.optSeqNew.Size = new System.Drawing.Size(86, 22);
			this.optSeqNew.TabIndex = 138;
			this.optSeqNew.TabStop = true;
			this.optSeqNew.Text = "Create New";
			this.optSeqNew.UseCompatibleTextRendering = true;
			this.optSeqNew.UseVisualStyleBackColor = true;
			this.optSeqNew.CheckedChanged += new System.EventHandler(this.optSeqNew_CheckedChanged);
			// 
			// button1
			// 
			this.button1.AllowDrop = true;
			this.button1.Location = new System.Drawing.Point(220, 111);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 27);
			this.button1.TabIndex = 129;
			this.button1.Text = "Settings...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			// 
			// lblStep4B
			// 
			this.lblStep4B.AutoSize = true;
			this.lblStep4B.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
			this.lblStep4B.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblStep4B.Location = new System.Drawing.Point(7, -6);
			this.lblStep4B.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStep4B.Name = "lblStep4B";
			this.lblStep4B.Size = new System.Drawing.Size(32, 24);
			this.lblStep4B.TabIndex = 127;
			this.lblStep4B.Text = "4b";
			// 
			// lblFileSequence
			// 
			this.lblFileSequence.AllowDrop = true;
			this.lblFileSequence.AutoSize = true;
			this.lblFileSequence.Location = new System.Drawing.Point(19, 59);
			this.lblFileSequence.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFileSequence.Name = "lblFileSequence";
			this.lblFileSequence.Size = new System.Drawing.Size(125, 15);
			this.lblFileSequence.TabIndex = 126;
			this.lblFileSequence.Text = "Sequence Filename(s):";
			// 
			// btnSaveSeq
			// 
			this.btnSaveSeq.AllowDrop = true;
			this.btnSaveSeq.Location = new System.Drawing.Point(285, 74);
			this.btnSaveSeq.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnSaveSeq.Name = "btnSaveSeq";
			this.btnSaveSeq.Size = new System.Drawing.Size(88, 27);
			this.btnSaveSeq.TabIndex = 125;
			this.btnSaveSeq.Text = "Save As...";
			this.btnSaveSeq.UseVisualStyleBackColor = true;
			this.btnSaveSeq.Click += new System.EventHandler(this.btnSaveSeq_Click);
			// 
			// txtSeqName
			// 
			this.txtSeqName.AllowDrop = true;
			this.txtSeqName.Location = new System.Drawing.Point(22, 77);
			this.txtSeqName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtSeqName.Name = "txtSeqName";
			this.txtSeqName.ReadOnly = true;
			this.txtSeqName.Size = new System.Drawing.Size(255, 23);
			this.txtSeqName.TabIndex = 124;
			// 
			// frmVamp
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1155, 835);
			this.Controls.Add(this.grpExplore);
			this.Controls.Add(this.grpSaveLOR);
			this.Controls.Add(this.grpAnalyze);
			this.Controls.Add(this.grpSavex);
			this.Controls.Add(this.grpAudio);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.grpOptions);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.btnResetDefaults);
			this.Controls.Add(this.grpTimings);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "frmVamp";
			this.Text = "Vamperizer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVamp_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmVamp_FormClosed);
			this.Load += new System.EventHandler(this.frmVamp_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmVamp_Paint);
			this.pnlTrackBeatsX.ResumeLayout(false);
			this.pnlTrackBeatsX.PerformLayout();
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.grpTimings.ResumeLayout(false);
			this.grpTimings.PerformLayout();
			this.grpChords.ResumeLayout(false);
			this.grpChords.PerformLayout();
			this.grbGlobal.ResumeLayout(false);
			this.grbGlobal.PerformLayout();
			this.pnlBeatFade.ResumeLayout(false);
			this.pnlBeatFade.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.grpPlatform.ResumeLayout(false);
			this.grpPlatform.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.grpVocals.ResumeLayout(false);
			this.grpVocals.PerformLayout();
			this.grpSegments.ResumeLayout(false);
			this.grpSegments.PerformLayout();
			this.grpTempo.ResumeLayout(false);
			this.grpTempo.PerformLayout();
			this.grpPoly2.ResumeLayout(false);
			this.grpPoly2.PerformLayout();
			this.grpPitchKey.ResumeLayout(false);
			this.grpPitchKey.PerformLayout();
			this.grpChromagram.ResumeLayout(false);
			this.grpChromagram.PerformLayout();
			this.grpPolyphonic.ResumeLayout(false);
			this.grpPolyphonic.PerformLayout();
			this.grpOnsets.ResumeLayout(false);
			this.grpOnsets.PerformLayout();
			this.pnlOnsetSensitivity.ResumeLayout(false);
			this.pnlOnsetSensitivity.PerformLayout();
			this.grpBarsBeats.ResumeLayout(false);
			this.grpBarsBeats.PerformLayout();
			this.pnlNotes.ResumeLayout(false);
			this.pnlNotes.PerformLayout();
			this.pnlVamping.ResumeLayout(false);
			this.pnlVamping.PerformLayout();
			this.grpExplore.ResumeLayout(false);
			this.grpAudio.ResumeLayout(false);
			this.grpAudio.PerformLayout();
			this.grpSavex.ResumeLayout(false);
			this.grpSavex.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picxLights)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.grpAnalyze.ResumeLayout(false);
			this.grpAnalyze.PerformLayout();
			this.grpOptions.ResumeLayout(false);
			this.grpOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picWorking)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picVampire)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picPoisonArrow)).EndInit();
			this.grpSaveLOR.ResumeLayout(false);
			this.grpSaveLOR.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picLOR)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
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
		public System.Windows.Forms.OpenFileDialog dlgFileOpen;
		public System.Windows.Forms.SaveFileDialog dlgFileSave;
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
		private System.Windows.Forms.GroupBox grpSavex;
		private System.Windows.Forms.Button btnSaveOptions;
		private System.Windows.Forms.Label lblStep4A;
		private System.Windows.Forms.Label lblFilexTimings;
		private System.Windows.Forms.Button btnSavexL;
		private System.Windows.Forms.TextBox txtSaveNamexL;
		private System.Windows.Forms.CheckBox chkFlux;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.GroupBox grpAnalyze;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblStep3;
		private System.Windows.Forms.CheckBox chkChromathing;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RadioButton optOnePer;
		private System.Windows.Forms.RadioButton optMultiPer;
		private System.Windows.Forms.GroupBox grpChromagram;
		private System.Windows.Forms.Label lblSpectrumLabels;
		private System.Windows.Forms.ComboBox cboLabelsChromagram;
		private System.Windows.Forms.Label lblSpectrumPlugin;
		private System.Windows.Forms.ComboBox cboMethodChromagram;
		private System.Windows.Forms.Label lblSpectrumAlign;
		private System.Windows.Forms.ComboBox cboAlignChromagram;
		private System.Windows.Forms.GroupBox grpPolyphonic;
		private System.Windows.Forms.Label lblTranscribeLabels;
		private System.Windows.Forms.ComboBox cboLabelsPolyphonic;
		private System.Windows.Forms.CheckBox chkPolyphonic;
		private System.Windows.Forms.ComboBox cboMethodPolyphonic;
		private System.Windows.Forms.Label lblTranscribeAlign;
		private System.Windows.Forms.ComboBox cboAlignPolyphonic;
		private System.Windows.Forms.GroupBox grpOnsets;
		private System.Windows.Forms.Label lblDetectOnsets;
		private System.Windows.Forms.ComboBox cboOnsetsDetect;
		private System.Windows.Forms.Label lblOnsetsLabels;
		private System.Windows.Forms.ComboBox cboLabelsOnsets;
		private System.Windows.Forms.CheckBox chkNoteOnsets;
		private System.Windows.Forms.Label lblOnsetsPlugin;
		private System.Windows.Forms.ComboBox cboMethodOnsets;
		private System.Windows.Forms.Label lblOnsetsAlign;
		private System.Windows.Forms.ComboBox cboAlignOnsets;
		private System.Windows.Forms.GroupBox grpBarsBeats;
		private System.Windows.Forms.Label lblMethod;
		private System.Windows.Forms.ComboBox cboMethodBarsBeats;
		private System.Windows.Forms.Label lblAlignBarsBeats;
		private System.Windows.Forms.ComboBox cboAlignBarBeats;
		private System.Windows.Forms.Panel pnlVamping;
		private System.Windows.Forms.Label lblSongName;
		private System.Windows.Forms.Label lblAnalyzing;
		private System.Windows.Forms.Label lblWait;
		private System.Windows.Forms.Label lblVampRed;
		private System.Windows.Forms.Panel pnlOnsetSensitivity;
		private System.Windows.Forms.VScrollBar vscSensitivity;
		private System.Windows.Forms.TextBox txtOnsetSensitivity;
		private System.Windows.Forms.Label lblOnsetsSensitivity;
		private System.Windows.Forms.Label lblTranscribePlugin;
		private System.Windows.Forms.GroupBox grpVocals;
		private System.Windows.Forms.CheckBox chkVocals;
		private System.Windows.Forms.Label lblVocalsAlign;
		private System.Windows.Forms.ComboBox cboAlignVocals;
		private System.Windows.Forms.GroupBox grpSegments;
		private System.Windows.Forms.CheckBox chkSegments;
		private System.Windows.Forms.Label lblSegmentsAlign;
		private System.Windows.Forms.ComboBox cboAlignSegments;
		private System.Windows.Forms.GroupBox grpTempo;
		private System.Windows.Forms.CheckBox chkTempo;
		private System.Windows.Forms.Label lblTempoLabels;
		private System.Windows.Forms.ComboBox cboLabelsTempo;
		private System.Windows.Forms.Label lblTempoPlugin;
		private System.Windows.Forms.ComboBox cboMethodTempo;
		private System.Windows.Forms.Label lblTempoAlign;
		private System.Windows.Forms.ComboBox cboAlignTempo;
		private System.Windows.Forms.GroupBox grpPitchKey;
		private System.Windows.Forms.CheckBox chkPitchKey;
		private System.Windows.Forms.Label lblKeyLabels;
		private System.Windows.Forms.ComboBox cboLabelsPitchKey;
		private System.Windows.Forms.Label lblKeyPlugin;
		private System.Windows.Forms.ComboBox cboMethodPitchKey;
		private System.Windows.Forms.Label lblKeyAlign;
		private System.Windows.Forms.ComboBox cboAlignPitchKey;
		private System.Windows.Forms.CheckBox chkChromagram;
		private System.Windows.Forms.GroupBox grpOptions;
		private System.Windows.Forms.PictureBox picPoisonArrow;
		private System.Windows.Forms.Label lblPickYourPoison;
		private System.Windows.Forms.Label lblStep2A;
		private System.Windows.Forms.Panel pnlNotes;
		private System.Windows.Forms.Label lblNote3Third;
		private System.Windows.Forms.CheckBox chkBeatsThird;
		private System.Windows.Forms.CheckBox chkBars;
		private System.Windows.Forms.CheckBox chkBeatsFull;
		private System.Windows.Forms.CheckBox chkBeatsHalf;
		private System.Windows.Forms.CheckBox chkBeatsQuarter;
		private System.Windows.Forms.Label lblNote1Bars;
		private System.Windows.Forms.Label lblHelpOnsets;
		private System.Windows.Forms.Label lblTweaks;
		private System.Windows.Forms.CheckBox chkReuse;
		private System.Windows.Forms.Button btnResetDefaults;
		private System.Windows.Forms.PictureBox picVampire;
		private System.Windows.Forms.CheckBox chkBarsBeats;
		private System.Windows.Forms.Timer tmrAni;
		private System.Windows.Forms.Label lblSegmentsPlugin;
		private System.Windows.Forms.ComboBox cboMethodSegments;
		private System.Windows.Forms.Label lblSegmentsLabels;
		private System.Windows.Forms.ComboBox cboLabelsSegments;
		private System.Windows.Forms.GroupBox grpSaveLOR;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.RadioButton optSeqAppend;
		private System.Windows.Forms.RadioButton optSeqNew;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label lblStep4B;
		private System.Windows.Forms.Label lblFileSequence;
		private System.Windows.Forms.Button btnSaveSeq;
		private System.Windows.Forms.TextBox txtSeqName;
		private System.Windows.Forms.PictureBox picxLights;
		private System.Windows.Forms.CheckBox chk24fps;
		private System.Windows.Forms.CheckBox chk30fps;
		private System.Windows.Forms.CheckBox chk15fps;
		private System.Windows.Forms.Label lblTrans2Options;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.GroupBox grpPoly2;
		private System.Windows.Forms.Label lblTrans2Plugin;
		private System.Windows.Forms.Label lblTrans2Labels;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.Label lblTrans2Align;
		private System.Windows.Forms.ComboBox cboAlignFoo;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.PictureBox picLOR;
		private System.Windows.Forms.CheckBox chkAutolaunch;
		private System.Windows.Forms.GroupBox grpPlatform;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.CheckBox chkxLights;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.CheckBox chkLOR;
		private System.Windows.Forms.Label lbllabel;
		private System.Windows.Forms.Label lblWorkFolder;
		private System.Windows.Forms.Label lblBeats2Half;
		private System.Windows.Forms.Label lblBeats1Full;
		private System.Windows.Forms.Label lblBeats4Quarter;
		private System.Windows.Forms.Label lblBeats0Bars;
		private System.Windows.Forms.Label lblSongTime;
		private System.Windows.Forms.Label lblVampTime;
		private System.Windows.Forms.Label lblSeqTime;
		private System.Windows.Forms.GroupBox grbGlobal;
		private System.Windows.Forms.Label lblStepSize;
		private System.Windows.Forms.ComboBox cboStepSize;
		private System.Windows.Forms.Panel pnlBeatFade;
		private JCS.ToggleSwitch swRamps;
		private System.Windows.Forms.Label lblBarsRampFade;
		private System.Windows.Forms.Label lblBarsOnOff;
		private System.Windows.Forms.CheckBox chkWhiten;
		private System.Windows.Forms.Panel pnlTrackBeatsX;
		private System.Windows.Forms.VScrollBar vscStartBeat;
		private System.Windows.Forms.TextBox txtStartBeat;
		private System.Windows.Forms.Label lblTrackBeatsX;
		private System.Windows.Forms.Panel panel1;
		private JCS.ToggleSwitch swTrackBeat;
		private System.Windows.Forms.Label lblTrackBeat34;
		private System.Windows.Forms.Label lblTrackBeat44;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboLabelsBarBeats;
		private System.Windows.Forms.Button btnReanalyze;
		private System.Windows.Forms.GroupBox grpExplore;
		private System.Windows.Forms.Button btnExploreVamp;
		private System.Windows.Forms.Button btnExplorexLights;
		private System.Windows.Forms.Button btnExploreTemp;
		private System.Windows.Forms.Button btnCmdTemp;
		private System.Windows.Forms.Button btnLaunchxLights;
		private System.Windows.Forms.Button btnSequenceEditor;
		private System.Windows.Forms.Button btnExploreLOR;
		private System.Windows.Forms.PictureBox picWorking;
		private System.Windows.Forms.Label lblDetectBarBeats;
		private System.Windows.Forms.ComboBox cboDetectBarBeats;
		private System.Windows.Forms.GroupBox grpChords;
		//? private System.Windows.Forms.CheckBox chkChords; // Duplicate?
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboLabelsChords;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cboMethodChords;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cboAlignChords;
		private System.Windows.Forms.CheckBox chkChords;
	}
}

