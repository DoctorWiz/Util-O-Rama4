using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVamp));
			ttip = new ToolTip(components);
			pnlTrackBeatsX = new Panel();
			vscStartBeat = new VScrollBar();
			txtStartBeat = new TextBox();
			lblTrackBeatsX = new Label();
			btnExploreVamp = new Button();
			btnExplorexLights = new Button();
			btnExploreTemp = new Button();
			btnCmdTemp = new Button();
			btnLaunchxLights = new Button();
			btnSequenceEditor = new Button();
			btnExploreLOR = new Button();
			staStatus = new StatusStrip();
			pnlHelp = new ToolStripStatusLabel();
			pnlProgress = new ToolStripProgressBar();
			pnlStatus = new ToolStripStatusLabel();
			pnlAbout = new ToolStripStatusLabel();
			dlgFileOpen = new OpenFileDialog();
			dlgFileSave = new SaveFileDialog();
			menuStrip1 = new MenuStrip();
			mnuFile = new ToolStripMenuItem();
			mnuOpenAudio = new ToolStripMenuItem();
			mnuSaveSequenceAs = new ToolStripMenuItem();
			mnuFileDivider1 = new ToolStripSeparator();
			toolStripMenuItem2 = new ToolStripMenuItem();
			mnuOptionsAnnotator = new ToolStripMenuItem();
			mnuOptionsVamp = new ToolStripMenuItem();
			mnuOptionsDivider1 = new ToolStripSeparator();
			mnuOptionsSaveFormat = new ToolStripMenuItem();
			mnuFileDivider2 = new ToolStripSeparator();
			mnuExit = new ToolStripMenuItem();
			mnuGenerate = new ToolStripMenuItem();
			mnuTimingMarkers = new ToolStripMenuItem();
			mnuTranscription = new ToolStripMenuItem();
			grpTimings = new GroupBox();
			grpChords = new GroupBox();
			chkChords = new CheckBox();
			label2 = new Label();
			cboLabelsChords = new ComboBox();
			label3 = new Label();
			cboMethodChords = new ComboBox();
			label4 = new Label();
			cboAlignChords = new ComboBox();
			grbGlobal = new GroupBox();
			lblStepSize = new Label();
			cboStepSize = new ComboBox();
			pnlBeatFade = new Panel();
			//swRamps = new JCS.ToggleSwitch();
			lblBarsRampFade = new Label();
			lblBarsOnOff = new Label();
			chkWhiten = new CheckBox();
			panel1 = new Panel();
			//swTrackBeat = new JCS.ToggleSwitch();
			lblTrackBeat34 = new Label();
			lblTrackBeat44 = new Label();
			lblNote3Third = new Label();
			lblNote1Bars = new Label();
			lblWorkFolder = new Label();
			grpPlatform = new GroupBox();
			pictureBox3 = new PictureBox();
			chkxLights = new CheckBox();
			pictureBox2 = new PictureBox();
			chkLOR = new CheckBox();
			lbllabel = new Label();
			chk24fps = new CheckBox();
			chk30fps = new CheckBox();
			lblTweaks = new Label();
			chk15fps = new CheckBox();
			grpVocals = new GroupBox();
			chkVocals = new CheckBox();
			lblVocalsAlign = new Label();
			cboAlignVocals = new ComboBox();
			lblTrans2Options = new Label();
			grpSegments = new GroupBox();
			lblSegmentsLabels = new Label();
			cboLabelsSegments = new ComboBox();
			lblSegmentsPlugin = new Label();
			cboMethodSegments = new ComboBox();
			chkSegments = new CheckBox();
			lblSegmentsAlign = new Label();
			cboAlignSegments = new ComboBox();
			button3 = new Button();
			grpTempo = new GroupBox();
			chkTempo = new CheckBox();
			lblTempoLabels = new Label();
			cboLabelsTempo = new ComboBox();
			lblTempoPlugin = new Label();
			cboMethodTempo = new ComboBox();
			lblTempoAlign = new Label();
			cboAlignTempo = new ComboBox();
			grpPoly2 = new GroupBox();
			lblTrans2Plugin = new Label();
			lblTrans2Labels = new Label();
			comboBox1 = new ComboBox();
			checkBox1 = new CheckBox();
			comboBox2 = new ComboBox();
			lblTrans2Align = new Label();
			cboAlignFoo = new ComboBox();
			checkBox2 = new CheckBox();
			btnTrackSettings = new Button();
			checkBox3 = new CheckBox();
			grpPitchKey = new GroupBox();
			chkPitchKey = new CheckBox();
			lblKeyLabels = new Label();
			cboLabelsPitchKey = new ComboBox();
			lblKeyPlugin = new Label();
			cboMethodPitchKey = new ComboBox();
			lblKeyAlign = new Label();
			cboAlignPitchKey = new ComboBox();
			checkBox4 = new CheckBox();
			grpChromagram = new GroupBox();
			chkChromagram = new CheckBox();
			lblSpectrumLabels = new Label();
			cboLabelsChromagram = new ComboBox();
			lblSpectrumPlugin = new Label();
			cboMethodChromagram = new ComboBox();
			lblSpectrumAlign = new Label();
			cboAlignChromagram = new ComboBox();
			grpPolyphonic = new GroupBox();
			lblTranscribePlugin = new Label();
			lblTranscribeLabels = new Label();
			cboLabelsPolyphonic = new ComboBox();
			chkPolyphonic = new CheckBox();
			cboMethodPolyphonic = new ComboBox();
			lblTranscribeAlign = new Label();
			cboAlignPolyphonic = new ComboBox();
			chkChromathing = new CheckBox();
			grpOnsets = new GroupBox();
			pnlOnsetSensitivity = new Panel();
			vscSensitivity = new VScrollBar();
			txtOnsetSensitivity = new TextBox();
			lblOnsetsSensitivity = new Label();
			lblDetectOnsets = new Label();
			cboOnsetsDetect = new ComboBox();
			lblOnsetsLabels = new Label();
			cboLabelsOnsets = new ComboBox();
			chkNoteOnsets = new CheckBox();
			lblOnsetsPlugin = new Label();
			cboMethodOnsets = new ComboBox();
			lblOnsetsAlign = new Label();
			cboAlignOnsets = new ComboBox();
			grpBarsBeats = new GroupBox();
			vScrollBar1 = new VScrollBar();
			lblDetectBarBeats = new Label();
			cboDetectBarBeats = new ComboBox();
			label1 = new Label();
			cboLabelsBarBeats = new ComboBox();
			chkBarsBeats = new CheckBox();
			pnlNotes = new Panel();
			chkBeatsThird = new CheckBox();
			chkBars = new CheckBox();
			chkBeatsFull = new CheckBox();
			chkBeatsHalf = new CheckBox();
			chkBeatsQuarter = new CheckBox();
			lblBeats2Half = new Label();
			lblBeats1Full = new Label();
			lblBeats4Quarter = new Label();
			lblBeats0Bars = new Label();
			lblMethod = new Label();
			cboMethodBarsBeats = new ComboBox();
			lblAlignBarsBeats = new Label();
			cboAlignBarBeats = new ComboBox();
			lblStep2B = new Label();
			chkFlux = new CheckBox();
			pnlVamping = new Panel();
			lblSongName = new Label();
			lblAnalyzing = new Label();
			lblWait = new Label();
			lblVampRed = new Label();
			grpExplore = new GroupBox();
			btnResetDefaults = new Button();
			grpAudio = new GroupBox();
			cboFile_Audio = new ComboBox();
			lblSongTime = new Label();
			btnBrowseAudio = new Button();
			lblFileAudio = new Label();
			lblStep1 = new Label();
			grpSavex = new GroupBox();
			picxLights = new PictureBox();
			panel2 = new Panel();
			optOnePer = new RadioButton();
			optMultiPer = new RadioButton();
			btnSaveOptions = new Button();
			lblStep4A = new Label();
			lblFilexTimings = new Label();
			btnSavexL = new Button();
			txtSaveNamexL = new TextBox();
			imlTreeIcons = new ImageList(components);
			grpAnalyze = new GroupBox();
			btnReanalyze = new Button();
			lblVampTime = new Label();
			btnOK = new Button();
			lblStep3 = new Label();
			grpOptions = new GroupBox();
			picWorking = new PictureBox();
			lblPickYourPoison = new Label();
			chkReuse = new CheckBox();
			picVampire = new PictureBox();
			lblHelpOnsets = new Label();
			picPoisonArrow = new PictureBox();
			lblStep2A = new Label();
			tmrAni = new System.Windows.Forms.Timer(components);
			grpSaveLOR = new GroupBox();
			lblSeqTime = new Label();
			chkAutolaunch = new CheckBox();
			picLOR = new PictureBox();
			panel3 = new Panel();
			optSeqAppend = new RadioButton();
			optSeqNew = new RadioButton();
			button1 = new Button();
			lblStep4B = new Label();
			lblFileSequence = new Label();
			btnSaveSeq = new Button();
			txtSeqName = new TextBox();
			pnlTrackBeatsX.SuspendLayout();
			staStatus.SuspendLayout();
			menuStrip1.SuspendLayout();
			grpTimings.SuspendLayout();
			grpChords.SuspendLayout();
			grbGlobal.SuspendLayout();
			pnlBeatFade.SuspendLayout();
			panel1.SuspendLayout();
			grpPlatform.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			grpVocals.SuspendLayout();
			grpSegments.SuspendLayout();
			grpTempo.SuspendLayout();
			grpPoly2.SuspendLayout();
			grpPitchKey.SuspendLayout();
			grpChromagram.SuspendLayout();
			grpPolyphonic.SuspendLayout();
			grpOnsets.SuspendLayout();
			pnlOnsetSensitivity.SuspendLayout();
			grpBarsBeats.SuspendLayout();
			pnlNotes.SuspendLayout();
			pnlVamping.SuspendLayout();
			grpExplore.SuspendLayout();
			grpAudio.SuspendLayout();
			grpSavex.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picxLights).BeginInit();
			panel2.SuspendLayout();
			grpAnalyze.SuspendLayout();
			grpOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picWorking).BeginInit();
			((System.ComponentModel.ISupportInitialize)picVampire).BeginInit();
			((System.ComponentModel.ISupportInitialize)picPoisonArrow).BeginInit();
			grpSaveLOR.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picLOR).BeginInit();
			panel3.SuspendLayout();
			SuspendLayout();
			// 
			// pnlTrackBeatsX
			// 
			pnlTrackBeatsX.Controls.Add(vscStartBeat);
			pnlTrackBeatsX.Controls.Add(txtStartBeat);
			pnlTrackBeatsX.Controls.Add(lblTrackBeatsX);
			pnlTrackBeatsX.Location = new Point(11, 117);
			pnlTrackBeatsX.Margin = new Padding(7, 6, 7, 6);
			pnlTrackBeatsX.Name = "pnlTrackBeatsX";
			pnlTrackBeatsX.Size = new Size(219, 58);
			pnlTrackBeatsX.TabIndex = 142;
			ttip.SetToolTip(pnlTrackBeatsX, "Sometimes the very first beat detected is NOT the first beat of the bar.\r\nIf (and only if) this happens, you can correct it here.\r\n");
			// 
			// vscStartBeat
			// 
			vscStartBeat.LargeChange = 1;
			vscStartBeat.Location = new Point(145, -6);
			vscStartBeat.Maximum = 4;
			vscStartBeat.Minimum = 1;
			vscStartBeat.Name = "vscStartBeat";
			vscStartBeat.Size = new Size(16, 45);
			vscStartBeat.TabIndex = 28;
			ttip.SetToolTip(vscStartBeat, "Sometimes the very first beat detected is NOT the first beat of the bar.\r\nIf (and only if) this happens, you can correct it here.\r\n");
			vscStartBeat.Value = 1;
			vscStartBeat.Scroll += vscStartBeat_Scroll;
			// 
			// txtStartBeat
			// 
			txtStartBeat.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			txtStartBeat.Location = new Point(117, 0);
			txtStartBeat.Margin = new Padding(7, 6, 7, 6);
			txtStartBeat.Name = "txtStartBeat";
			txtStartBeat.ReadOnly = true;
			txtStartBeat.Size = new Size(23, 32);
			txtStartBeat.TabIndex = 7;
			txtStartBeat.Text = "1";
			// 
			// lblTrackBeatsX
			// 
			lblTrackBeatsX.AutoSize = true;
			lblTrackBeatsX.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTrackBeatsX.ForeColor = Color.OrangeRed;
			lblTrackBeatsX.Location = new Point(2, 11);
			lblTrackBeatsX.Margin = new Padding(7, 0, 7, 0);
			lblTrackBeatsX.Name = "lblTrackBeatsX";
			lblTrackBeatsX.Size = new Size(109, 26);
			lblTrackBeatsX.TabIndex = 6;
			lblTrackBeatsX.Text = "Start Beat";
			ttip.SetToolTip(lblTrackBeatsX, "Sometimes the very first beat detected is NOT the first beat of the bar.\r\nIf (and only if) this happens, you can correct it here.");
			lblTrackBeatsX.UseMnemonic = false;
			// 
			// btnExploreVamp
			// 
			btnExploreVamp.AllowDrop = true;
			btnExploreVamp.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnExploreVamp.Image = (Image)resources.GetObject("btnExploreVamp.Image");
			btnExploreVamp.Location = new Point(576, 38);
			btnExploreVamp.Margin = new Padding(7, 6, 7, 6);
			btnExploreVamp.Name = "btnExploreVamp";
			btnExploreVamp.Size = new Size(67, 77);
			btnExploreVamp.TabIndex = 183;
			ttip.SetToolTip(btnExploreVamp, "Explore the Vamperizer Source Code Folder");
			btnExploreVamp.UseVisualStyleBackColor = true;
			btnExploreVamp.Visible = false;
			btnExploreVamp.Click += btnExploreVamp_Click;
			// 
			// btnExplorexLights
			// 
			btnExplorexLights.AllowDrop = true;
			btnExplorexLights.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnExplorexLights.Image = (Image)resources.GetObject("btnExplorexLights.Image");
			btnExplorexLights.Location = new Point(299, 38);
			btnExplorexLights.Margin = new Padding(7, 6, 7, 6);
			btnExplorexLights.Name = "btnExplorexLights";
			btnExplorexLights.Size = new Size(67, 77);
			btnExplorexLights.TabIndex = 182;
			ttip.SetToolTip(btnExplorexLights, "Explore xLights Show Folder");
			btnExplorexLights.UseVisualStyleBackColor = true;
			btnExplorexLights.Visible = false;
			btnExplorexLights.Click += btnExplorexLights_Click;
			// 
			// btnExploreTemp
			// 
			btnExploreTemp.AllowDrop = true;
			btnExploreTemp.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnExploreTemp.Image = (Image)resources.GetObject("btnExploreTemp.Image");
			btnExploreTemp.Location = new Point(481, 38);
			btnExploreTemp.Margin = new Padding(7, 6, 7, 6);
			btnExploreTemp.Name = "btnExploreTemp";
			btnExploreTemp.Size = new Size(67, 77);
			btnExploreTemp.TabIndex = 181;
			ttip.SetToolTip(btnExploreTemp, "Explore the Temp Working Folder");
			btnExploreTemp.UseVisualStyleBackColor = true;
			btnExploreTemp.Visible = false;
			btnExploreTemp.Click += btnExploreVamp_Click;
			// 
			// btnCmdTemp
			// 
			btnCmdTemp.AllowDrop = true;
			btnCmdTemp.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnCmdTemp.Image = (Image)resources.GetObject("btnCmdTemp.Image");
			btnCmdTemp.Location = new Point(390, 38);
			btnCmdTemp.Margin = new Padding(7, 6, 7, 6);
			btnCmdTemp.Name = "btnCmdTemp";
			btnCmdTemp.Size = new Size(67, 77);
			btnCmdTemp.TabIndex = 180;
			ttip.SetToolTip(btnCmdTemp, "Open Command Prompt in the Temp Working Directory");
			btnCmdTemp.UseVisualStyleBackColor = true;
			btnCmdTemp.Visible = false;
			btnCmdTemp.Click += btnCmdTemp_Click;
			// 
			// btnLaunchxLights
			// 
			btnLaunchxLights.AllowDrop = true;
			btnLaunchxLights.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnLaunchxLights.Image = (Image)resources.GetObject("btnLaunchxLights.Image");
			btnLaunchxLights.Location = new Point(208, 38);
			btnLaunchxLights.Margin = new Padding(7, 6, 7, 6);
			btnLaunchxLights.Name = "btnLaunchxLights";
			btnLaunchxLights.Size = new Size(67, 77);
			btnLaunchxLights.TabIndex = 179;
			ttip.SetToolTip(btnLaunchxLights, "Launch xLights");
			btnLaunchxLights.UseVisualStyleBackColor = true;
			btnLaunchxLights.Visible = false;
			btnLaunchxLights.Click += btnLaunchxLights_Click;
			// 
			// btnSequenceEditor
			// 
			btnSequenceEditor.AllowDrop = true;
			btnSequenceEditor.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnSequenceEditor.Image = (Image)resources.GetObject("btnSequenceEditor.Image");
			btnSequenceEditor.Location = new Point(24, 38);
			btnSequenceEditor.Margin = new Padding(7, 6, 7, 6);
			btnSequenceEditor.Name = "btnSequenceEditor";
			btnSequenceEditor.Size = new Size(67, 77);
			btnSequenceEditor.TabIndex = 178;
			ttip.SetToolTip(btnSequenceEditor, "Launch Light-O-Rama Sequence Editor");
			btnSequenceEditor.UseVisualStyleBackColor = true;
			btnSequenceEditor.Visible = false;
			btnSequenceEditor.Click += btnSequenceEditor_Click;
			// 
			// btnExploreLOR
			// 
			btnExploreLOR.AllowDrop = true;
			btnExploreLOR.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnExploreLOR.Image = (Image)resources.GetObject("btnExploreLOR.Image");
			btnExploreLOR.Location = new Point(115, 38);
			btnExploreLOR.Margin = new Padding(7, 6, 7, 6);
			btnExploreLOR.Name = "btnExploreLOR";
			btnExploreLOR.Size = new Size(67, 77);
			btnExploreLOR.TabIndex = 177;
			ttip.SetToolTip(btnExploreLOR, "Explore Light-O-Rama Sequences Folder");
			btnExploreLOR.UseVisualStyleBackColor = true;
			btnExploreLOR.Visible = false;
			btnExploreLOR.Click += btnExploreTemp_Click;
			// 
			// staStatus
			// 
			staStatus.AllowDrop = true;
			staStatus.ImageScalingSize = new Size(32, 32);
			staStatus.Items.AddRange(new ToolStripItem[] { pnlHelp, pnlProgress, pnlStatus, pnlAbout });
			staStatus.Location = new Point(0, 1735);
			staStatus.Name = "staStatus";
			staStatus.Padding = new Padding(2, 0, 30, 0);
			staStatus.Size = new Size(2145, 46);
			staStatus.TabIndex = 62;
			staStatus.DragDrop += Event_DragDrop;
			staStatus.DragEnter += Event_DragEnter;
			// 
			// pnlHelp
			// 
			pnlHelp.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
			pnlHelp.BorderStyle = Border3DStyle.SunkenInner;
			pnlHelp.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
			pnlHelp.ForeColor = SystemColors.Highlight;
			pnlHelp.IsLink = true;
			pnlHelp.Name = "pnlHelp";
			pnlHelp.Size = new Size(81, 36);
			pnlHelp.Text = "Help...";
			pnlHelp.Click += pnlHelp_Click;
			// 
			// pnlProgress
			// 
			pnlProgress.Name = "pnlProgress";
			pnlProgress.Size = new Size(217, 34);
			pnlProgress.Visible = false;
			// 
			// pnlStatus
			// 
			pnlStatus.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
			pnlStatus.BorderStyle = Border3DStyle.SunkenOuter;
			pnlStatus.Name = "pnlStatus";
			pnlStatus.Size = new Size(1937, 36);
			pnlStatus.Spring = true;
			// 
			// pnlAbout
			// 
			pnlAbout.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
			pnlAbout.BorderStyle = Border3DStyle.SunkenInner;
			pnlAbout.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
			pnlAbout.ForeColor = SystemColors.Highlight;
			pnlAbout.Name = "pnlAbout";
			pnlAbout.Size = new Size(95, 36);
			pnlAbout.Text = "About...";
			pnlAbout.Click += pnlAbout_Click;
			// 
			// dlgFileOpen
			// 
			dlgFileOpen.FileName = "openFileDialog1";
			// 
			// menuStrip1
			// 
			menuStrip1.ImageScalingSize = new Size(32, 32);
			menuStrip1.Items.AddRange(new ToolStripItem[] { mnuFile, mnuGenerate });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Padding = new Padding(13, 4, 0, 4);
			menuStrip1.Size = new Size(2485, 60);
			menuStrip1.TabIndex = 110;
			menuStrip1.Text = "menuStrip1";
			menuStrip1.Visible = false;
			// 
			// mnuFile
			// 
			mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuOpenAudio, mnuSaveSequenceAs, mnuFileDivider1, toolStripMenuItem2, mnuFileDivider2, mnuExit });
			mnuFile.Name = "mnuFile";
			mnuFile.Size = new Size(71, 52);
			mnuFile.Text = "&File";
			// 
			// mnuOpenAudio
			// 
			mnuOpenAudio.Name = "mnuOpenAudio";
			mnuOpenAudio.ShortcutKeys = Keys.Control | Keys.O;
			mnuOpenAudio.Size = new Size(436, 44);
			mnuOpenAudio.Text = "&Open Audio...";
			// 
			// mnuSaveSequenceAs
			// 
			mnuSaveSequenceAs.Name = "mnuSaveSequenceAs";
			mnuSaveSequenceAs.ShortcutKeys = Keys.Control | Keys.S;
			mnuSaveSequenceAs.Size = new Size(436, 44);
			mnuSaveSequenceAs.Text = "Save Sequence &As...";
			// 
			// mnuFileDivider1
			// 
			mnuFileDivider1.Name = "mnuFileDivider1";
			mnuFileDivider1.Size = new Size(433, 6);
			// 
			// toolStripMenuItem2
			// 
			toolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { mnuOptionsAnnotator, mnuOptionsVamp, mnuOptionsDivider1, mnuOptionsSaveFormat });
			toolStripMenuItem2.Name = "toolStripMenuItem2";
			toolStripMenuItem2.Size = new Size(436, 44);
			toolStripMenuItem2.Text = "O&ptions";
			// 
			// mnuOptionsAnnotator
			// 
			mnuOptionsAnnotator.Name = "mnuOptionsAnnotator";
			mnuOptionsAnnotator.Size = new Size(334, 44);
			mnuOptionsAnnotator.Text = "Sonic &Annotator...";
			// 
			// mnuOptionsVamp
			// 
			mnuOptionsVamp.Name = "mnuOptionsVamp";
			mnuOptionsVamp.Size = new Size(334, 44);
			mnuOptionsVamp.Text = "&Vamp Plugins...";
			// 
			// mnuOptionsDivider1
			// 
			mnuOptionsDivider1.Name = "mnuOptionsDivider1";
			mnuOptionsDivider1.Size = new Size(331, 6);
			// 
			// mnuOptionsSaveFormat
			// 
			mnuOptionsSaveFormat.Name = "mnuOptionsSaveFormat";
			mnuOptionsSaveFormat.Size = new Size(334, 44);
			mnuOptionsSaveFormat.Text = "Save &Format...";
			// 
			// mnuFileDivider2
			// 
			mnuFileDivider2.Name = "mnuFileDivider2";
			mnuFileDivider2.Size = new Size(433, 6);
			// 
			// mnuExit
			// 
			mnuExit.Name = "mnuExit";
			mnuExit.ShortcutKeys = Keys.Alt | Keys.F4;
			mnuExit.Size = new Size(436, 44);
			mnuExit.Text = "E&xit";
			mnuExit.Click += mnuExit_Click;
			// 
			// mnuGenerate
			// 
			mnuGenerate.DropDownItems.AddRange(new ToolStripItem[] { mnuTimingMarkers, mnuTranscription });
			mnuGenerate.Name = "mnuGenerate";
			mnuGenerate.Size = new Size(131, 52);
			mnuGenerate.Text = "&Generate";
			// 
			// mnuTimingMarkers
			// 
			mnuTimingMarkers.Name = "mnuTimingMarkers";
			mnuTimingMarkers.Size = new Size(407, 44);
			mnuTimingMarkers.Text = "&Timing Markers";
			// 
			// mnuTranscription
			// 
			mnuTranscription.Name = "mnuTranscription";
			mnuTranscription.Size = new Size(407, 44);
			mnuTranscription.Text = "&Polyphonic Transcription";
			// 
			// grpTimings
			// 
			grpTimings.Controls.Add(grpChords);
			grpTimings.Controls.Add(grbGlobal);
			grpTimings.Controls.Add(lblNote3Third);
			grpTimings.Controls.Add(lblNote1Bars);
			grpTimings.Controls.Add(lblWorkFolder);
			grpTimings.Controls.Add(grpPlatform);
			grpTimings.Controls.Add(chk24fps);
			grpTimings.Controls.Add(chk30fps);
			grpTimings.Controls.Add(lblTweaks);
			grpTimings.Controls.Add(chk15fps);
			grpTimings.Controls.Add(grpVocals);
			grpTimings.Controls.Add(lblTrans2Options);
			grpTimings.Controls.Add(grpSegments);
			grpTimings.Controls.Add(button3);
			grpTimings.Controls.Add(grpTempo);
			grpTimings.Controls.Add(grpPoly2);
			grpTimings.Controls.Add(checkBox2);
			grpTimings.Controls.Add(btnTrackSettings);
			grpTimings.Controls.Add(checkBox3);
			grpTimings.Controls.Add(grpPitchKey);
			grpTimings.Controls.Add(checkBox4);
			grpTimings.Controls.Add(grpChromagram);
			grpTimings.Controls.Add(grpPolyphonic);
			grpTimings.Controls.Add(chkChromathing);
			grpTimings.Controls.Add(grpOnsets);
			grpTimings.Controls.Add(grpBarsBeats);
			grpTimings.Controls.Add(lblStep2B);
			grpTimings.Controls.Add(chkFlux);
			grpTimings.Enabled = false;
			grpTimings.Location = new Point(763, 30);
			grpTimings.Margin = new Padding(7, 6, 7, 6);
			grpTimings.Name = "grpTimings";
			grpTimings.Padding = new Padding(7, 6, 7, 6);
			grpTimings.Size = new Size(1361, 1664);
			grpTimings.TabIndex = 113;
			grpTimings.TabStop = false;
			grpTimings.Text = "      Select Which Timings to Generate";
			grpTimings.Enter += grpTimings_Enter;
			// 
			// grpChords
			// 
			grpChords.Controls.Add(chkChords);
			grpChords.Controls.Add(label2);
			grpChords.Controls.Add(cboLabelsChords);
			grpChords.Controls.Add(label3);
			grpChords.Controls.Add(cboMethodChords);
			grpChords.Controls.Add(label4);
			grpChords.Controls.Add(cboAlignChords);
			grpChords.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			grpChords.Location = new Point(1016, 582);
			grpChords.Margin = new Padding(7, 6, 7, 6);
			grpChords.Name = "grpChords";
			grpChords.Padding = new Padding(7, 6, 7, 6);
			grpChords.Size = new Size(319, 380);
			grpChords.TabIndex = 181;
			grpChords.TabStop = false;
			grpChords.Text = "   Chords";
			// 
			// chkChords
			// 
			chkChords.AutoSize = true;
			chkChords.Checked = true;
			chkChords.CheckState = CheckState.Checked;
			chkChords.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkChords.Location = new Point(0, 0);
			chkChords.Margin = new Padding(7, 6, 7, 6);
			chkChords.Name = "chkChords";
			chkChords.Size = new Size(28, 27);
			chkChords.TabIndex = 151;
			chkChords.Tag = "11";
			chkChords.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(13, 158);
			label2.Margin = new Padding(7, 0, 7, 0);
			label2.Name = "label2";
			label2.Size = new Size(82, 26);
			label2.TabIndex = 149;
			label2.Text = "Labels:";
			// 
			// cboLabelsChords
			// 
			cboLabelsChords.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLabelsChords.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboLabelsChords.FormattingEnabled = true;
			cboLabelsChords.Items.AddRange(new object[] { "None", "Note Names", "MIDI Note Numbers" });
			cboLabelsChords.Location = new Point(13, 196);
			cboLabelsChords.Margin = new Padding(7, 6, 7, 6);
			cboLabelsChords.Name = "cboLabelsChords";
			cboLabelsChords.Size = new Size(288, 34);
			cboLabelsChords.TabIndex = 148;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			label3.Location = new Point(13, 38);
			label3.Margin = new Padding(7, 0, 7, 0);
			label3.Name = "label3";
			label3.Size = new Size(169, 26);
			label3.TabIndex = 144;
			label3.Text = "Plugin / Method:";
			// 
			// cboMethodChords
			// 
			cboMethodChords.DropDownStyle = ComboBoxStyle.DropDownList;
			cboMethodChords.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboMethodChords.FormattingEnabled = true;
			cboMethodChords.Items.AddRange(new object[] { "Queen Mary Note Onset Detector", "Queen Mary Polyphonic Transcription", "OnsetDS Onset Detector", "Silvet Note Transcription", "Alicante Note Onset Detector", "Alicante Polyphonic Transcription", "Aubio Onset Detector", "Aubio Note Tracker" });
			cboMethodChords.Location = new Point(13, 79);
			cboMethodChords.Margin = new Padding(7, 6, 7, 6);
			cboMethodChords.Name = "cboMethodChords";
			cboMethodChords.Size = new Size(288, 34);
			cboMethodChords.TabIndex = 143;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			label4.Location = new Point(13, 256);
			label4.Margin = new Padding(7, 0, 7, 0);
			label4.Name = "label4";
			label4.Size = new Size(97, 26);
			label4.TabIndex = 142;
			label4.Text = "Align To:";
			// 
			// cboAlignChords
			// 
			cboAlignChords.DropDownStyle = ComboBoxStyle.DropDownList;
			cboAlignChords.DropDownWidth = 180;
			cboAlignChords.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboAlignChords.FormattingEnabled = true;
			cboAlignChords.Items.AddRange(new object[] { "None", "25ms 40fps", "50ms 20fps", "Bars / Whole Notes", "Full Beats / Quarter Notes", "Half Beats / Eighth Notes", "Quarter Beats / Sixteenth Notes", "Note Onsets" });
			cboAlignChords.Location = new Point(13, 294);
			cboAlignChords.Margin = new Padding(7, 6, 7, 6);
			cboAlignChords.Name = "cboAlignChords";
			cboAlignChords.Size = new Size(288, 34);
			cboAlignChords.TabIndex = 141;
			// 
			// grbGlobal
			// 
			grbGlobal.Controls.Add(lblStepSize);
			grbGlobal.Controls.Add(cboStepSize);
			grbGlobal.Controls.Add(pnlBeatFade);
			grbGlobal.Controls.Add(chkWhiten);
			grbGlobal.Controls.Add(pnlTrackBeatsX);
			grbGlobal.Controls.Add(panel1);
			grbGlobal.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			grbGlobal.Location = new Point(13, 190);
			grbGlobal.Margin = new Padding(7, 6, 7, 6);
			grbGlobal.Name = "grbGlobal";
			grbGlobal.Padding = new Padding(7, 6, 7, 6);
			grbGlobal.Size = new Size(319, 429);
			grbGlobal.TabIndex = 180;
			grbGlobal.TabStop = false;
			grbGlobal.Text = " All Vamps ";
			// 
			// lblStepSize
			// 
			lblStepSize.AutoSize = true;
			lblStepSize.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblStepSize.Location = new Point(13, 309);
			lblStepSize.Margin = new Padding(7, 0, 7, 0);
			lblStepSize.Name = "lblStepSize";
			lblStepSize.Size = new Size(112, 26);
			lblStepSize.TabIndex = 164;
			lblStepSize.Text = "Step Size:";
			// 
			// cboStepSize
			// 
			cboStepSize.DropDownStyle = ComboBoxStyle.DropDownList;
			cboStepSize.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboStepSize.FormattingEnabled = true;
			cboStepSize.Items.AddRange(new object[] { "441", "512", "557" });
			cboStepSize.Location = new Point(13, 350);
			cboStepSize.Margin = new Padding(7, 6, 7, 6);
			cboStepSize.Name = "cboStepSize";
			cboStepSize.Size = new Size(288, 34);
			cboStepSize.TabIndex = 163;
			// 
			// pnlBeatFade
			// 
			//pnlBeatFade.Controls.Add(swRamps);
			pnlBeatFade.Controls.Add(lblBarsRampFade);
			pnlBeatFade.Controls.Add(lblBarsOnOff);
			pnlBeatFade.Location = new Point(11, 254);
			pnlBeatFade.Margin = new Padding(7, 6, 7, 6);
			pnlBeatFade.Name = "pnlBeatFade";
			pnlBeatFade.Size = new Size(301, 43);
			pnlBeatFade.TabIndex = 158;
			// 
			/*
			// swRamps
			// 
			swRamps.Location = new Point(80, 2);
			swRamps.Margin = new Padding(7, 6, 7, 6);
			swRamps.Name = "swRamps";
			//swRamps.OffFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
			//swRamps.OnFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			swRamps.Size = new Size(69, 38);
			swRamps.Style = JCS.ToggleSwitch.ToggleSwitchStyle.OSX;
			swRamps.TabIndex = 132;
			// 
			*/
			// lblBarsRampFade
			// 
			lblBarsRampFade.AutoSize = true;
			lblBarsRampFade.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblBarsRampFade.Location = new Point(149, 4);
			lblBarsRampFade.Margin = new Padding(7, 0, 7, 0);
			lblBarsRampFade.Name = "lblBarsRampFade";
			lblBarsRampFade.Size = new Size(127, 26);
			lblBarsRampFade.TabIndex = 128;
			lblBarsRampFade.Text = "Ramp-Fade";
			// 
			// lblBarsOnOff
			// 
			lblBarsOnOff.AutoSize = true;
			lblBarsOnOff.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblBarsOnOff.Location = new Point(0, 4);
			lblBarsOnOff.Margin = new Padding(7, 0, 7, 0);
			lblBarsOnOff.Name = "lblBarsOnOff";
			lblBarsOnOff.Size = new Size(77, 26);
			lblBarsOnOff.TabIndex = 127;
			lblBarsOnOff.Text = "On-Off";
			// 
			// chkWhiten
			// 
			chkWhiten.AutoSize = true;
			chkWhiten.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkWhiten.Location = new Point(11, 196);
			chkWhiten.Margin = new Padding(7, 6, 7, 6);
			chkWhiten.Name = "chkWhiten";
			chkWhiten.Size = new Size(162, 34);
			chkWhiten.TabIndex = 155;
			chkWhiten.Tag = "13";
			chkWhiten.Text = "\"Whitening\"";
			chkWhiten.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			//panel1.Controls.Add(swTrackBeat);
			panel1.Controls.Add(lblTrackBeat34);
			panel1.Controls.Add(lblTrackBeat44);
			panel1.Location = new Point(11, 47);
			panel1.Margin = new Padding(7, 6, 7, 6);
			panel1.Name = "panel1";
			panel1.Size = new Size(275, 58);
			panel1.TabIndex = 141;
			// 
			/*
			// swTrackBeat
			// 
			swTrackBeat.Location = new Point(100, 2);
			swTrackBeat.Margin = new Padding(7, 6, 7, 6);
			swTrackBeat.Name = "swTrackBeat";
			//swTrackBeat.OffFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
			//swTrackBeat.OnFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			swTrackBeat.Size = new Size(69, 38);
			swTrackBeat.Style = JCS.ToggleSwitch.ToggleSwitchStyle.OSX;
			swTrackBeat.TabIndex = 133;
			// 
			*/
			// lblTrackBeat34
			// 
			lblTrackBeat34.AutoSize = true;
			lblTrackBeat34.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTrackBeat34.ForeColor = Color.OrangeRed;
			lblTrackBeat34.Location = new Point(173, 4);
			lblTrackBeat34.Margin = new Padding(7, 0, 7, 0);
			lblTrackBeat34.Name = "lblTrackBeat34";
			lblTrackBeat34.Size = new Size(90, 26);
			lblTrackBeat34.TabIndex = 128;
			lblTrackBeat34.Text = "3/4 time";
			// 
			// lblTrackBeat44
			// 
			lblTrackBeat44.AutoSize = true;
			lblTrackBeat44.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTrackBeat44.Location = new Point(2, 4);
			lblTrackBeat44.Margin = new Padding(7, 0, 7, 0);
			lblTrackBeat44.Name = "lblTrackBeat44";
			lblTrackBeat44.Size = new Size(90, 26);
			lblTrackBeat44.TabIndex = 127;
			lblTrackBeat44.Text = "4/4 time";
			// 
			// lblNote3Third
			// 
			lblNote3Third.AutoSize = true;
			lblNote3Third.Font = new Font("Calibri", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
			lblNote3Third.Location = new Point(1137, 1540);
			lblNote3Third.Margin = new Padding(7, 0, 7, 0);
			lblNote3Third.Name = "lblNote3Third";
			lblNote3Third.Size = new Size(43, 51);
			lblNote3Third.TabIndex = 148;
			lblNote3Third.Text = "𝅝";
			lblNote3Third.Visible = false;
			// 
			// lblNote1Bars
			// 
			lblNote1Bars.AutoSize = true;
			lblNote1Bars.Font = new Font("Segoe UI Symbol", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
			lblNote1Bars.Location = new Point(1324, 1589);
			lblNote1Bars.Margin = new Padding(7, 0, 7, 0);
			lblNote1Bars.Name = "lblNote1Bars";
			lblNote1Bars.Size = new Size(38, 57);
			lblNote1Bars.TabIndex = 145;
			lblNote1Bars.Text = "𝄁";
			lblNote1Bars.Visible = false;
			// 
			// lblWorkFolder
			// 
			lblWorkFolder.AutoSize = true;
			lblWorkFolder.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblWorkFolder.Location = new Point(1029, 30);
			lblWorkFolder.Margin = new Padding(7, 0, 7, 0);
			lblWorkFolder.Name = "lblWorkFolder";
			lblWorkFolder.Size = new Size(220, 26);
			lblWorkFolder.TabIndex = 173;
			lblWorkFolder.Text = "Temp Working Folder";
			lblWorkFolder.Visible = false;
			// 
			// grpPlatform
			// 
			grpPlatform.Controls.Add(pictureBox3);
			grpPlatform.Controls.Add(chkxLights);
			grpPlatform.Controls.Add(pictureBox2);
			grpPlatform.Controls.Add(chkLOR);
			grpPlatform.Controls.Add(lbllabel);
			grpPlatform.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			grpPlatform.Location = new Point(17, 45);
			grpPlatform.Margin = new Padding(7, 6, 7, 6);
			grpPlatform.Name = "grpPlatform";
			grpPlatform.Padding = new Padding(7, 6, 7, 6);
			grpPlatform.Size = new Size(650, 117);
			grpPlatform.TabIndex = 157;
			grpPlatform.TabStop = false;
			grpPlatform.Text = "       Platform";
			// 
			// pictureBox3
			// 
			pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
			pictureBox3.InitialImage = (Image)resources.GetObject("pictureBox3.InitialImage");
			pictureBox3.Location = new Point(366, 43);
			pictureBox3.Margin = new Padding(7, 6, 7, 6);
			pictureBox3.Name = "pictureBox3";
			pictureBox3.Size = new Size(158, 55);
			pictureBox3.TabIndex = 160;
			pictureBox3.TabStop = false;
			// 
			// chkxLights
			// 
			chkxLights.AutoSize = true;
			chkxLights.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkxLights.Location = new Point(327, 53);
			chkxLights.Margin = new Padding(7, 6, 7, 6);
			chkxLights.Name = "chkxLights";
			chkxLights.Size = new Size(28, 27);
			chkxLights.TabIndex = 159;
			chkxLights.Tag = "13";
			chkxLights.UseVisualStyleBackColor = true;
			chkxLights.CheckedChanged += chkxLights_CheckedChanged;
			// 
			// pictureBox2
			// 
			pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
			pictureBox2.Location = new Point(87, 49);
			pictureBox2.Margin = new Padding(7, 6, 7, 6);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new Size(167, 41);
			pictureBox2.TabIndex = 158;
			pictureBox2.TabStop = false;
			pictureBox2.Click += pictureBox2_Click;
			// 
			// chkLOR
			// 
			chkLOR.AutoSize = true;
			chkLOR.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkLOR.Location = new Point(41, 53);
			chkLOR.Margin = new Padding(7, 6, 7, 6);
			chkLOR.Name = "chkLOR";
			chkLOR.Size = new Size(28, 27);
			chkLOR.TabIndex = 157;
			chkLOR.Tag = "13";
			chkLOR.UseVisualStyleBackColor = true;
			chkLOR.CheckedChanged += chkLOR_CheckedChanged;
			// 
			// lbllabel
			// 
			lbllabel.AutoSize = true;
			lbllabel.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lbllabel.Location = new Point(286, 134);
			lbllabel.Margin = new Padding(7, 0, 7, 0);
			lbllabel.Name = "lbllabel";
			lbllabel.Size = new Size(73, 30);
			lbllabel.TabIndex = 142;
			lbllabel.Text = "(lable)";
			lbllabel.Visible = false;
			// 
			// chk24fps
			// 
			chk24fps.AutoSize = true;
			chk24fps.Enabled = false;
			chk24fps.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chk24fps.Location = new Point(1406, 1094);
			chk24fps.Margin = new Padding(7, 6, 7, 6);
			chk24fps.Name = "chk24fps";
			chk24fps.Size = new Size(287, 34);
			chk24fps.TabIndex = 170;
			chk24fps.Tag = "12";
			chk24fps.Text = "4.16 centiseconds 24 FPS";
			chk24fps.UseVisualStyleBackColor = true;
			// 
			// chk30fps
			// 
			chk30fps.AllowDrop = true;
			chk30fps.AutoSize = true;
			chk30fps.Enabled = false;
			chk30fps.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chk30fps.Location = new Point(1406, 1056);
			chk30fps.Margin = new Padding(7, 6, 7, 6);
			chk30fps.Name = "chk30fps";
			chk30fps.Size = new Size(287, 34);
			chk30fps.TabIndex = 169;
			chk30fps.Tag = "12";
			chk30fps.Text = "3.33 centiseconds 30 FPS";
			chk30fps.UseVisualStyleBackColor = true;
			// 
			// lblTweaks
			// 
			lblTweaks.AutoSize = true;
			lblTweaks.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point);
			lblTweaks.ForeColor = SystemColors.HotTrack;
			lblTweaks.Location = new Point(1475, 670);
			lblTweaks.Margin = new Padding(7, 0, 7, 0);
			lblTweaks.Name = "lblTweaks";
			lblTweaks.Size = new Size(160, 26);
			lblTweaks.TabIndex = 157;
			lblTweaks.Text = "More Options...";
			lblTweaks.Click += labelTweaks_Click;
			// 
			// chk15fps
			// 
			chk15fps.AutoSize = true;
			chk15fps.Enabled = false;
			chk15fps.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chk15fps.Location = new Point(1406, 1135);
			chk15fps.Margin = new Padding(7, 6, 7, 6);
			chk15fps.Name = "chk15fps";
			chk15fps.Size = new Size(287, 34);
			chk15fps.TabIndex = 168;
			chk15fps.Tag = "12";
			chk15fps.Text = "6.67 centiseconds 15 FPS";
			chk15fps.UseVisualStyleBackColor = true;
			// 
			// grpVocals
			// 
			grpVocals.Controls.Add(chkVocals);
			grpVocals.Controls.Add(lblVocalsAlign);
			grpVocals.Controls.Add(cboAlignVocals);
			grpVocals.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			grpVocals.Location = new Point(1267, 1510);
			grpVocals.Margin = new Padding(7, 6, 7, 6);
			grpVocals.Name = "grpVocals";
			grpVocals.Padding = new Padding(7, 6, 7, 6);
			grpVocals.Size = new Size(319, 211);
			grpVocals.TabIndex = 143;
			grpVocals.TabStop = false;
			grpVocals.Text = "   Vocals";
			grpVocals.Visible = false;
			// 
			// chkVocals
			// 
			chkVocals.AutoSize = true;
			chkVocals.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkVocals.Location = new Point(0, 0);
			chkVocals.Margin = new Padding(7, 6, 7, 6);
			chkVocals.Name = "chkVocals";
			chkVocals.Size = new Size(28, 27);
			chkVocals.TabIndex = 143;
			chkVocals.Tag = "14";
			chkVocals.UseVisualStyleBackColor = true;
			chkVocals.Visible = false;
			// 
			// lblVocalsAlign
			// 
			lblVocalsAlign.AutoSize = true;
			lblVocalsAlign.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblVocalsAlign.Location = new Point(13, 100);
			lblVocalsAlign.Margin = new Padding(7, 0, 7, 0);
			lblVocalsAlign.Name = "lblVocalsAlign";
			lblVocalsAlign.Size = new Size(98, 30);
			lblVocalsAlign.TabIndex = 142;
			lblVocalsAlign.Text = "Align To:";
			// 
			// cboAlignVocals
			// 
			cboAlignVocals.DropDownStyle = ComboBoxStyle.DropDownList;
			cboAlignVocals.DropDownWidth = 180;
			cboAlignVocals.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboAlignVocals.FormattingEnabled = true;
			cboAlignVocals.Items.AddRange(new object[] { "None", "25ms 40fps", "50ms 20fps", "Bars / Whole Notes", "Full Beats / Quarter Notes", "Half Beats / Eighth Notes", "Quarter Beats / Sixteenth Notes", "Note Onsets" });
			cboAlignVocals.Location = new Point(13, 141);
			cboAlignVocals.Margin = new Padding(7, 6, 7, 6);
			cboAlignVocals.Name = "cboAlignVocals";
			cboAlignVocals.Size = new Size(288, 38);
			cboAlignVocals.TabIndex = 141;
			// 
			// lblTrans2Options
			// 
			lblTrans2Options.AutoSize = true;
			lblTrans2Options.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point);
			lblTrans2Options.Location = new Point(1549, 1397);
			lblTrans2Options.Margin = new Padding(7, 0, 7, 0);
			lblTrans2Options.Name = "lblTrans2Options";
			lblTrans2Options.Size = new Size(160, 26);
			lblTrans2Options.TabIndex = 167;
			lblTrans2Options.Text = "More Options...";
			// 
			// grpSegments
			// 
			grpSegments.Controls.Add(lblSegmentsLabels);
			grpSegments.Controls.Add(cboLabelsSegments);
			grpSegments.Controls.Add(lblSegmentsPlugin);
			grpSegments.Controls.Add(cboMethodSegments);
			grpSegments.Controls.Add(chkSegments);
			grpSegments.Controls.Add(lblSegmentsAlign);
			grpSegments.Controls.Add(cboAlignSegments);
			grpSegments.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			grpSegments.Location = new Point(1016, 1018);
			grpSegments.Margin = new Padding(7, 6, 7, 6);
			grpSegments.Name = "grpSegments";
			grpSegments.Padding = new Padding(7, 6, 7, 6);
			grpSegments.Size = new Size(319, 369);
			grpSegments.TabIndex = 142;
			grpSegments.TabStop = false;
			grpSegments.Text = "   Segments";
			// 
			// lblSegmentsLabels
			// 
			lblSegmentsLabels.AutoSize = true;
			lblSegmentsLabels.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblSegmentsLabels.Location = new Point(13, 147);
			lblSegmentsLabels.Margin = new Padding(7, 0, 7, 0);
			lblSegmentsLabels.Name = "lblSegmentsLabels";
			lblSegmentsLabels.Size = new Size(82, 26);
			lblSegmentsLabels.TabIndex = 154;
			lblSegmentsLabels.Text = "Labels:";
			// 
			// cboLabelsSegments
			// 
			cboLabelsSegments.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLabelsSegments.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboLabelsSegments.FormattingEnabled = true;
			cboLabelsSegments.Items.AddRange(new object[] { "None", "Numbers", "Letters" });
			cboLabelsSegments.Location = new Point(13, 188);
			cboLabelsSegments.Margin = new Padding(7, 6, 7, 6);
			cboLabelsSegments.Name = "cboLabelsSegments";
			cboLabelsSegments.Size = new Size(288, 34);
			cboLabelsSegments.TabIndex = 153;
			// 
			// lblSegmentsPlugin
			// 
			lblSegmentsPlugin.AutoSize = true;
			lblSegmentsPlugin.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblSegmentsPlugin.Location = new Point(13, 38);
			lblSegmentsPlugin.Margin = new Padding(7, 0, 7, 0);
			lblSegmentsPlugin.Name = "lblSegmentsPlugin";
			lblSegmentsPlugin.Size = new Size(169, 26);
			lblSegmentsPlugin.TabIndex = 152;
			lblSegmentsPlugin.Text = "Plugin / Method:";
			// 
			// cboMethodSegments
			// 
			cboMethodSegments.DropDownStyle = ComboBoxStyle.DropDownList;
			cboMethodSegments.DropDownWidth = 200;
			cboMethodSegments.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboMethodSegments.FormattingEnabled = true;
			cboMethodSegments.Items.AddRange(new object[] { "Queen Mary Segmenter", "Segmentino" });
			cboMethodSegments.Location = new Point(11, 79);
			cboMethodSegments.Margin = new Padding(7, 6, 7, 6);
			cboMethodSegments.Name = "cboMethodSegments";
			cboMethodSegments.Size = new Size(288, 34);
			cboMethodSegments.TabIndex = 151;
			// 
			// chkSegments
			// 
			chkSegments.AutoSize = true;
			chkSegments.Checked = true;
			chkSegments.CheckState = CheckState.Checked;
			chkSegments.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkSegments.Location = new Point(0, 0);
			chkSegments.Margin = new Padding(7, 6, 7, 6);
			chkSegments.Name = "chkSegments";
			chkSegments.Size = new Size(28, 27);
			chkSegments.TabIndex = 150;
			chkSegments.Tag = "7";
			chkSegments.UseVisualStyleBackColor = true;
			// 
			// lblSegmentsAlign
			// 
			lblSegmentsAlign.AutoSize = true;
			lblSegmentsAlign.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblSegmentsAlign.Location = new Point(13, 256);
			lblSegmentsAlign.Margin = new Padding(7, 0, 7, 0);
			lblSegmentsAlign.Name = "lblSegmentsAlign";
			lblSegmentsAlign.Size = new Size(98, 30);
			lblSegmentsAlign.TabIndex = 142;
			lblSegmentsAlign.Text = "Align To:";
			// 
			// cboAlignSegments
			// 
			cboAlignSegments.DropDownStyle = ComboBoxStyle.DropDownList;
			cboAlignSegments.DropDownWidth = 180;
			cboAlignSegments.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboAlignSegments.FormattingEnabled = true;
			cboAlignSegments.Items.AddRange(new object[] { "None", "25ms 40fps", "50ms 20fps", "Bars / Whole Notes", "Full Beats / Quarter Notes", "Half Beats / Eighth Notes", "Quarter Beats / Sixteenth Notes", "Note Onsets" });
			cboAlignSegments.Location = new Point(11, 299);
			cboAlignSegments.Margin = new Padding(7, 6, 7, 6);
			cboAlignSegments.Name = "cboAlignSegments";
			cboAlignSegments.Size = new Size(288, 38);
			cboAlignSegments.TabIndex = 141;
			// 
			// button3
			// 
			button3.AllowDrop = true;
			button3.Enabled = false;
			button3.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			button3.Image = (Image)resources.GetObject("button3.Image");
			button3.Location = new Point(1378, 544);
			button3.Margin = new Padding(7, 6, 7, 6);
			button3.Name = "button3";
			button3.Size = new Size(69, 79);
			button3.TabIndex = 162;
			button3.UseVisualStyleBackColor = true;
			button3.Visible = false;
			// 
			// grpTempo
			// 
			grpTempo.Controls.Add(chkTempo);
			grpTempo.Controls.Add(lblTempoLabels);
			grpTempo.Controls.Add(cboLabelsTempo);
			grpTempo.Controls.Add(lblTempoPlugin);
			grpTempo.Controls.Add(cboMethodTempo);
			grpTempo.Controls.Add(lblTempoAlign);
			grpTempo.Controls.Add(cboAlignTempo);
			grpTempo.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			grpTempo.Location = new Point(685, 1018);
			grpTempo.Margin = new Padding(7, 6, 7, 6);
			grpTempo.Name = "grpTempo";
			grpTempo.Padding = new Padding(7, 6, 7, 6);
			grpTempo.Size = new Size(319, 369);
			grpTempo.TabIndex = 141;
			grpTempo.TabStop = false;
			grpTempo.Text = "   Tempo";
			// 
			// chkTempo
			// 
			chkTempo.AutoSize = true;
			chkTempo.Checked = true;
			chkTempo.CheckState = CheckState.Checked;
			chkTempo.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkTempo.ForeColor = Color.Red;
			chkTempo.Location = new Point(0, 0);
			chkTempo.Margin = new Padding(7, 6, 7, 6);
			chkTempo.Name = "chkTempo";
			chkTempo.Size = new Size(28, 27);
			chkTempo.TabIndex = 150;
			chkTempo.Tag = "9";
			chkTempo.UseVisualStyleBackColor = true;
			// 
			// lblTempoLabels
			// 
			lblTempoLabels.AutoSize = true;
			lblTempoLabels.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTempoLabels.Location = new Point(13, 145);
			lblTempoLabels.Margin = new Padding(7, 0, 7, 0);
			lblTempoLabels.Name = "lblTempoLabels";
			lblTempoLabels.Size = new Size(82, 26);
			lblTempoLabels.TabIndex = 149;
			lblTempoLabels.Text = "Labels:";
			// 
			// cboLabelsTempo
			// 
			cboLabelsTempo.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLabelsTempo.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboLabelsTempo.FormattingEnabled = true;
			cboLabelsTempo.Items.AddRange(new object[] { "None", "Note Names", "MIDI Note Numbers" });
			cboLabelsTempo.Location = new Point(13, 186);
			cboLabelsTempo.Margin = new Padding(7, 6, 7, 6);
			cboLabelsTempo.Name = "cboLabelsTempo";
			cboLabelsTempo.Size = new Size(288, 34);
			cboLabelsTempo.TabIndex = 148;
			// 
			// lblTempoPlugin
			// 
			lblTempoPlugin.AutoSize = true;
			lblTempoPlugin.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTempoPlugin.Location = new Point(13, 38);
			lblTempoPlugin.Margin = new Padding(7, 0, 7, 0);
			lblTempoPlugin.Name = "lblTempoPlugin";
			lblTempoPlugin.Size = new Size(169, 26);
			lblTempoPlugin.TabIndex = 144;
			lblTempoPlugin.Text = "Plugin / Method:";
			// 
			// cboMethodTempo
			// 
			cboMethodTempo.DropDownStyle = ComboBoxStyle.DropDownList;
			cboMethodTempo.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboMethodTempo.FormattingEnabled = true;
			cboMethodTempo.Items.AddRange(new object[] { "Queen Mary Note Onset Detector", "Queen Mary Polyphonic Transcription", "OnsetDS Onset Detector", "Silvet Note Transcription", "Alicante Note Onset Detector", "Alicante Polyphonic Transcription", "Aubio Onset Detector", "Aubio Note Tracker" });
			cboMethodTempo.Location = new Point(13, 81);
			cboMethodTempo.Margin = new Padding(7, 6, 7, 6);
			cboMethodTempo.Name = "cboMethodTempo";
			cboMethodTempo.Size = new Size(288, 34);
			cboMethodTempo.TabIndex = 143;
			// 
			// lblTempoAlign
			// 
			lblTempoAlign.AutoSize = true;
			lblTempoAlign.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTempoAlign.Location = new Point(13, 243);
			lblTempoAlign.Margin = new Padding(7, 0, 7, 0);
			lblTempoAlign.Name = "lblTempoAlign";
			lblTempoAlign.Size = new Size(97, 26);
			lblTempoAlign.TabIndex = 142;
			lblTempoAlign.Text = "Align To:";
			// 
			// cboAlignTempo
			// 
			cboAlignTempo.DropDownStyle = ComboBoxStyle.DropDownList;
			cboAlignTempo.DropDownWidth = 180;
			cboAlignTempo.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboAlignTempo.FormattingEnabled = true;
			cboAlignTempo.Items.AddRange(new object[] { "None", "25ms 40fps", "50ms 20fps", "Bars / Whole Notes", "Full Beats / Quarter Notes", "Half Beats / Eighth Notes", "Quarter Beats / Sixteenth Notes", "Note Onsets" });
			cboAlignTempo.Location = new Point(13, 284);
			cboAlignTempo.Margin = new Padding(7, 6, 7, 6);
			cboAlignTempo.Name = "cboAlignTempo";
			cboAlignTempo.Size = new Size(288, 34);
			cboAlignTempo.TabIndex = 141;
			// 
			// grpPoly2
			// 
			grpPoly2.Controls.Add(lblTrans2Plugin);
			grpPoly2.Controls.Add(lblTrans2Labels);
			grpPoly2.Controls.Add(comboBox1);
			grpPoly2.Controls.Add(checkBox1);
			grpPoly2.Controls.Add(comboBox2);
			grpPoly2.Controls.Add(lblTrans2Align);
			grpPoly2.Controls.Add(cboAlignFoo);
			grpPoly2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			grpPoly2.Location = new Point(1605, 60);
			grpPoly2.Margin = new Padding(7, 6, 7, 6);
			grpPoly2.Name = "grpPoly2";
			grpPoly2.Padding = new Padding(7, 6, 7, 6);
			grpPoly2.Size = new Size(319, 851);
			grpPoly2.TabIndex = 166;
			grpPoly2.TabStop = false;
			grpPoly2.Text = "   Polyphonic Transcription";
			grpPoly2.Visible = false;
			// 
			// lblTrans2Plugin
			// 
			lblTrans2Plugin.AutoSize = true;
			lblTrans2Plugin.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTrans2Plugin.Location = new Point(13, 38);
			lblTrans2Plugin.Margin = new Padding(7, 0, 7, 0);
			lblTrans2Plugin.Name = "lblTrans2Plugin";
			lblTrans2Plugin.Size = new Size(169, 26);
			lblTrans2Plugin.TabIndex = 148;
			lblTrans2Plugin.Text = "Plugin / Method:";
			// 
			// lblTrans2Labels
			// 
			lblTrans2Labels.AutoSize = true;
			lblTrans2Labels.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTrans2Labels.Location = new Point(13, 642);
			lblTrans2Labels.Margin = new Padding(7, 0, 7, 0);
			lblTrans2Labels.Name = "lblTrans2Labels";
			lblTrans2Labels.Size = new Size(82, 26);
			lblTrans2Labels.TabIndex = 147;
			lblTrans2Labels.Text = "Labels:";
			// 
			// comboBox1
			// 
			comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			comboBox1.FormattingEnabled = true;
			comboBox1.Items.AddRange(new object[] { "MIDI Note Numbers", "Note Names" });
			comboBox1.Location = new Point(13, 683);
			comboBox1.Margin = new Padding(7, 6, 7, 6);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(288, 34);
			comboBox1.TabIndex = 146;
			// 
			// checkBox1
			// 
			checkBox1.AutoSize = true;
			checkBox1.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			checkBox1.Location = new Point(0, 0);
			checkBox1.Margin = new Padding(7, 6, 7, 6);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new Size(28, 27);
			checkBox1.TabIndex = 145;
			checkBox1.Tag = "10";
			checkBox1.TextAlign = ContentAlignment.TopLeft;
			checkBox1.UseVisualStyleBackColor = true;
			// 
			// comboBox2
			// 
			comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox2.DropDownWidth = 200;
			comboBox2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			comboBox2.FormattingEnabled = true;
			comboBox2.Items.AddRange(new object[] { "Queen Mary Polyphonic Transcription", "Silvet Note Transcription", "Alicante Polyphonic Transcription", "Aubio Note Tracker" });
			comboBox2.Location = new Point(13, 79);
			comboBox2.Margin = new Padding(7, 6, 7, 6);
			comboBox2.Name = "comboBox2";
			comboBox2.Size = new Size(288, 34);
			comboBox2.TabIndex = 143;
			// 
			// lblTrans2Align
			// 
			lblTrans2Align.AutoSize = true;
			lblTrans2Align.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTrans2Align.Location = new Point(13, 740);
			lblTrans2Align.Margin = new Padding(7, 0, 7, 0);
			lblTrans2Align.Name = "lblTrans2Align";
			lblTrans2Align.Size = new Size(97, 26);
			lblTrans2Align.TabIndex = 142;
			lblTrans2Align.Text = "Align To:";
			// 
			// cboAlignFoo
			// 
			cboAlignFoo.DropDownStyle = ComboBoxStyle.DropDownList;
			cboAlignFoo.DropDownWidth = 180;
			cboAlignFoo.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboAlignFoo.FormattingEnabled = true;
			cboAlignFoo.Items.AddRange(new object[] { "None", "25ms 40fps", "50ms 20fps", "Beats / Quarter Notes", "Half Beats / Eighth Notes", "Quarter Beats / Sixteenth Notes", "Note Onsets" });
			cboAlignFoo.Location = new Point(13, 781);
			cboAlignFoo.Margin = new Padding(7, 6, 7, 6);
			cboAlignFoo.Name = "cboAlignFoo";
			cboAlignFoo.Size = new Size(288, 34);
			cboAlignFoo.TabIndex = 141;
			// 
			// checkBox2
			// 
			checkBox2.AutoSize = true;
			checkBox2.Enabled = false;
			checkBox2.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			checkBox2.Location = new Point(1378, 365);
			checkBox2.Margin = new Padding(7, 6, 7, 6);
			checkBox2.Name = "checkBox2";
			checkBox2.Size = new Size(148, 34);
			checkBox2.TabIndex = 165;
			checkBox2.Tag = "8";
			checkBox2.Text = "Whateva 1";
			checkBox2.UseVisualStyleBackColor = true;
			// 
			// btnTrackSettings
			// 
			btnTrackSettings.AllowDrop = true;
			btnTrackSettings.Enabled = false;
			btnTrackSettings.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnTrackSettings.Image = (Image)resources.GetObject("btnTrackSettings.Image");
			btnTrackSettings.Location = new Point(1482, 544);
			btnTrackSettings.Margin = new Padding(7, 6, 7, 6);
			btnTrackSettings.Name = "btnTrackSettings";
			btnTrackSettings.Size = new Size(69, 79);
			btnTrackSettings.TabIndex = 122;
			btnTrackSettings.UseVisualStyleBackColor = true;
			btnTrackSettings.Visible = false;
			btnTrackSettings.Click += btnTrackSettings_Click;
			// 
			// checkBox3
			// 
			checkBox3.AutoSize = true;
			checkBox3.Enabled = false;
			checkBox3.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			checkBox3.Location = new Point(1378, 420);
			checkBox3.Margin = new Padding(7, 6, 7, 6);
			checkBox3.Name = "checkBox3";
			checkBox3.Size = new Size(148, 34);
			checkBox3.TabIndex = 163;
			checkBox3.Tag = "13";
			checkBox3.Text = "Whateva 2";
			checkBox3.UseVisualStyleBackColor = true;
			// 
			// grpPitchKey
			// 
			grpPitchKey.Controls.Add(chkPitchKey);
			grpPitchKey.Controls.Add(lblKeyLabels);
			grpPitchKey.Controls.Add(cboLabelsPitchKey);
			grpPitchKey.Controls.Add(lblKeyPlugin);
			grpPitchKey.Controls.Add(cboMethodPitchKey);
			grpPitchKey.Controls.Add(lblKeyAlign);
			grpPitchKey.Controls.Add(cboAlignPitchKey);
			grpPitchKey.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			grpPitchKey.Location = new Point(349, 1018);
			grpPitchKey.Margin = new Padding(7, 6, 7, 6);
			grpPitchKey.Name = "grpPitchKey";
			grpPitchKey.Padding = new Padding(7, 6, 7, 6);
			grpPitchKey.Size = new Size(319, 369);
			grpPitchKey.TabIndex = 140;
			grpPitchKey.TabStop = false;
			grpPitchKey.Text = "   Pitch && Key";
			// 
			// chkPitchKey
			// 
			chkPitchKey.AutoSize = true;
			chkPitchKey.Checked = true;
			chkPitchKey.CheckState = CheckState.Checked;
			chkPitchKey.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkPitchKey.Location = new Point(0, 0);
			chkPitchKey.Margin = new Padding(7, 6, 7, 6);
			chkPitchKey.Name = "chkPitchKey";
			chkPitchKey.Size = new Size(28, 27);
			chkPitchKey.TabIndex = 150;
			chkPitchKey.Tag = "6";
			chkPitchKey.UseVisualStyleBackColor = true;
			// 
			// lblKeyLabels
			// 
			lblKeyLabels.AutoSize = true;
			lblKeyLabels.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblKeyLabels.Location = new Point(13, 147);
			lblKeyLabels.Margin = new Padding(7, 0, 7, 0);
			lblKeyLabels.Name = "lblKeyLabels";
			lblKeyLabels.Size = new Size(82, 26);
			lblKeyLabels.TabIndex = 149;
			lblKeyLabels.Text = "Labels:";
			// 
			// cboLabelsPitchKey
			// 
			cboLabelsPitchKey.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLabelsPitchKey.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboLabelsPitchKey.FormattingEnabled = true;
			cboLabelsPitchKey.Items.AddRange(new object[] { "None", "Key Names", "Key Numbers", "MIDI Note Numbers", "Frequency" });
			cboLabelsPitchKey.Location = new Point(13, 188);
			cboLabelsPitchKey.Margin = new Padding(7, 6, 7, 6);
			cboLabelsPitchKey.Name = "cboLabelsPitchKey";
			cboLabelsPitchKey.Size = new Size(288, 34);
			cboLabelsPitchKey.TabIndex = 148;
			// 
			// lblKeyPlugin
			// 
			lblKeyPlugin.AutoSize = true;
			lblKeyPlugin.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblKeyPlugin.Location = new Point(13, 38);
			lblKeyPlugin.Margin = new Padding(7, 0, 7, 0);
			lblKeyPlugin.Name = "lblKeyPlugin";
			lblKeyPlugin.Size = new Size(169, 26);
			lblKeyPlugin.TabIndex = 144;
			lblKeyPlugin.Text = "Plugin / Method:";
			// 
			// cboMethodPitchKey
			// 
			cboMethodPitchKey.DropDownStyle = ComboBoxStyle.DropDownList;
			cboMethodPitchKey.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboMethodPitchKey.FormattingEnabled = true;
			cboMethodPitchKey.Items.AddRange(new object[] { "Queen Mary Key Detector" });
			cboMethodPitchKey.Location = new Point(13, 79);
			cboMethodPitchKey.Margin = new Padding(7, 6, 7, 6);
			cboMethodPitchKey.Name = "cboMethodPitchKey";
			cboMethodPitchKey.Size = new Size(288, 34);
			cboMethodPitchKey.TabIndex = 143;
			// 
			// lblKeyAlign
			// 
			lblKeyAlign.AutoSize = true;
			lblKeyAlign.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblKeyAlign.Location = new Point(13, 256);
			lblKeyAlign.Margin = new Padding(7, 0, 7, 0);
			lblKeyAlign.Name = "lblKeyAlign";
			lblKeyAlign.Size = new Size(97, 26);
			lblKeyAlign.TabIndex = 142;
			lblKeyAlign.Text = "Align To:";
			// 
			// cboAlignPitchKey
			// 
			cboAlignPitchKey.DropDownStyle = ComboBoxStyle.DropDownList;
			cboAlignPitchKey.DropDownWidth = 180;
			cboAlignPitchKey.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboAlignPitchKey.FormattingEnabled = true;
			cboAlignPitchKey.Items.AddRange(new object[] { "None", "25ms 40fps", "50ms 20fps", "Bars / Whole Notes", "Full Beats / Quarter Notes", "Half Beats / Eighth Notes", "Quarter Beats / Sixteenth Notes", "Note Onsets" });
			cboAlignPitchKey.Location = new Point(13, 294);
			cboAlignPitchKey.Margin = new Padding(7, 6, 7, 6);
			cboAlignPitchKey.Name = "cboAlignPitchKey";
			cboAlignPitchKey.Size = new Size(288, 34);
			cboAlignPitchKey.TabIndex = 141;
			// 
			// checkBox4
			// 
			checkBox4.AutoSize = true;
			checkBox4.Enabled = false;
			checkBox4.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			checkBox4.Location = new Point(1378, 478);
			checkBox4.Margin = new Padding(7, 6, 7, 6);
			checkBox4.Name = "checkBox4";
			checkBox4.Size = new Size(148, 34);
			checkBox4.TabIndex = 164;
			checkBox4.Tag = "12";
			checkBox4.Text = "Whateva 3";
			checkBox4.UseVisualStyleBackColor = true;
			// 
			// grpChromagram
			// 
			grpChromagram.Controls.Add(chkChromagram);
			grpChromagram.Controls.Add(lblSpectrumLabels);
			grpChromagram.Controls.Add(cboLabelsChromagram);
			grpChromagram.Controls.Add(lblSpectrumPlugin);
			grpChromagram.Controls.Add(cboMethodChromagram);
			grpChromagram.Controls.Add(lblSpectrumAlign);
			grpChromagram.Controls.Add(cboAlignChromagram);
			grpChromagram.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			grpChromagram.Location = new Point(1016, 190);
			grpChromagram.Margin = new Padding(7, 6, 7, 6);
			grpChromagram.Name = "grpChromagram";
			grpChromagram.Padding = new Padding(7, 6, 7, 6);
			grpChromagram.Size = new Size(319, 380);
			grpChromagram.TabIndex = 139;
			grpChromagram.TabStop = false;
			grpChromagram.Text = "   Chromagram";
			// 
			// chkChromagram
			// 
			chkChromagram.AutoSize = true;
			chkChromagram.Checked = true;
			chkChromagram.CheckState = CheckState.Checked;
			chkChromagram.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkChromagram.Location = new Point(0, 0);
			chkChromagram.Margin = new Padding(7, 6, 7, 6);
			chkChromagram.Name = "chkChromagram";
			chkChromagram.Size = new Size(28, 27);
			chkChromagram.TabIndex = 150;
			chkChromagram.Tag = "11";
			chkChromagram.UseVisualStyleBackColor = true;
			// 
			// lblSpectrumLabels
			// 
			lblSpectrumLabels.AutoSize = true;
			lblSpectrumLabels.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblSpectrumLabels.Location = new Point(13, 158);
			lblSpectrumLabels.Margin = new Padding(7, 0, 7, 0);
			lblSpectrumLabels.Name = "lblSpectrumLabels";
			lblSpectrumLabels.Size = new Size(82, 26);
			lblSpectrumLabels.TabIndex = 149;
			lblSpectrumLabels.Text = "Labels:";
			// 
			// cboLabelsChromagram
			// 
			cboLabelsChromagram.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLabelsChromagram.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboLabelsChromagram.FormattingEnabled = true;
			cboLabelsChromagram.Items.AddRange(new object[] { "None", "Note Names", "MIDI Note Numbers" });
			cboLabelsChromagram.Location = new Point(13, 196);
			cboLabelsChromagram.Margin = new Padding(7, 6, 7, 6);
			cboLabelsChromagram.Name = "cboLabelsChromagram";
			cboLabelsChromagram.Size = new Size(288, 34);
			cboLabelsChromagram.TabIndex = 148;
			// 
			// lblSpectrumPlugin
			// 
			lblSpectrumPlugin.AutoSize = true;
			lblSpectrumPlugin.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblSpectrumPlugin.Location = new Point(13, 38);
			lblSpectrumPlugin.Margin = new Padding(7, 0, 7, 0);
			lblSpectrumPlugin.Name = "lblSpectrumPlugin";
			lblSpectrumPlugin.Size = new Size(169, 26);
			lblSpectrumPlugin.TabIndex = 144;
			lblSpectrumPlugin.Text = "Plugin / Method:";
			// 
			// cboMethodChromagram
			// 
			cboMethodChromagram.DropDownStyle = ComboBoxStyle.DropDownList;
			cboMethodChromagram.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboMethodChromagram.FormattingEnabled = true;
			cboMethodChromagram.Items.AddRange(new object[] { "Queen Mary Note Onset Detector", "Queen Mary Polyphonic Transcription", "OnsetDS Onset Detector", "Silvet Note Transcription", "Alicante Note Onset Detector", "Alicante Polyphonic Transcription", "Aubio Onset Detector", "Aubio Note Tracker" });
			cboMethodChromagram.Location = new Point(13, 79);
			cboMethodChromagram.Margin = new Padding(7, 6, 7, 6);
			cboMethodChromagram.Name = "cboMethodChromagram";
			cboMethodChromagram.Size = new Size(288, 34);
			cboMethodChromagram.TabIndex = 143;
			// 
			// lblSpectrumAlign
			// 
			lblSpectrumAlign.AutoSize = true;
			lblSpectrumAlign.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblSpectrumAlign.Location = new Point(13, 256);
			lblSpectrumAlign.Margin = new Padding(7, 0, 7, 0);
			lblSpectrumAlign.Name = "lblSpectrumAlign";
			lblSpectrumAlign.Size = new Size(97, 26);
			lblSpectrumAlign.TabIndex = 142;
			lblSpectrumAlign.Text = "Align To:";
			// 
			// cboAlignChromagram
			// 
			cboAlignChromagram.DropDownStyle = ComboBoxStyle.DropDownList;
			cboAlignChromagram.DropDownWidth = 180;
			cboAlignChromagram.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboAlignChromagram.FormattingEnabled = true;
			cboAlignChromagram.Items.AddRange(new object[] { "None", "25ms 40fps", "50ms 20fps", "Bars / Whole Notes", "Full Beats / Quarter Notes", "Half Beats / Eighth Notes", "Quarter Beats / Sixteenth Notes", "Note Onsets" });
			cboAlignChromagram.Location = new Point(13, 294);
			cboAlignChromagram.Margin = new Padding(7, 6, 7, 6);
			cboAlignChromagram.Name = "cboAlignChromagram";
			cboAlignChromagram.Size = new Size(288, 34);
			cboAlignChromagram.TabIndex = 141;
			// 
			// grpPolyphonic
			// 
			grpPolyphonic.Controls.Add(lblTranscribePlugin);
			grpPolyphonic.Controls.Add(lblTranscribeLabels);
			grpPolyphonic.Controls.Add(cboLabelsPolyphonic);
			grpPolyphonic.Controls.Add(chkPolyphonic);
			grpPolyphonic.Controls.Add(cboMethodPolyphonic);
			grpPolyphonic.Controls.Add(lblTranscribeAlign);
			grpPolyphonic.Controls.Add(cboAlignPolyphonic);
			grpPolyphonic.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			grpPolyphonic.Location = new Point(13, 1018);
			grpPolyphonic.Margin = new Padding(7, 6, 7, 6);
			grpPolyphonic.Name = "grpPolyphonic";
			grpPolyphonic.Padding = new Padding(7, 6, 7, 6);
			grpPolyphonic.Size = new Size(319, 369);
			grpPolyphonic.TabIndex = 138;
			grpPolyphonic.TabStop = false;
			grpPolyphonic.Text = "   Polyphonic Transcription";
			// 
			// lblTranscribePlugin
			// 
			lblTranscribePlugin.AutoSize = true;
			lblTranscribePlugin.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTranscribePlugin.Location = new Point(13, 38);
			lblTranscribePlugin.Margin = new Padding(7, 0, 7, 0);
			lblTranscribePlugin.Name = "lblTranscribePlugin";
			lblTranscribePlugin.Size = new Size(169, 26);
			lblTranscribePlugin.TabIndex = 148;
			lblTranscribePlugin.Text = "Plugin / Method:";
			// 
			// lblTranscribeLabels
			// 
			lblTranscribeLabels.AutoSize = true;
			lblTranscribeLabels.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTranscribeLabels.Location = new Point(13, 147);
			lblTranscribeLabels.Margin = new Padding(7, 0, 7, 0);
			lblTranscribeLabels.Name = "lblTranscribeLabels";
			lblTranscribeLabels.Size = new Size(82, 26);
			lblTranscribeLabels.TabIndex = 147;
			lblTranscribeLabels.Text = "Labels:";
			// 
			// cboLabelsPolyphonic
			// 
			cboLabelsPolyphonic.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLabelsPolyphonic.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboLabelsPolyphonic.FormattingEnabled = true;
			cboLabelsPolyphonic.Items.AddRange(new object[] { "MIDI Note Numbers", "Note Names" });
			cboLabelsPolyphonic.Location = new Point(13, 188);
			cboLabelsPolyphonic.Margin = new Padding(7, 6, 7, 6);
			cboLabelsPolyphonic.Name = "cboLabelsPolyphonic";
			cboLabelsPolyphonic.Size = new Size(288, 34);
			cboLabelsPolyphonic.TabIndex = 146;
			// 
			// chkPolyphonic
			// 
			chkPolyphonic.AutoSize = true;
			chkPolyphonic.Checked = true;
			chkPolyphonic.CheckState = CheckState.Checked;
			chkPolyphonic.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkPolyphonic.Location = new Point(0, 0);
			chkPolyphonic.Margin = new Padding(7, 6, 7, 6);
			chkPolyphonic.Name = "chkPolyphonic";
			chkPolyphonic.Size = new Size(28, 27);
			chkPolyphonic.TabIndex = 145;
			chkPolyphonic.Tag = "10";
			chkPolyphonic.TextAlign = ContentAlignment.TopLeft;
			chkPolyphonic.UseVisualStyleBackColor = true;
			// 
			// cboMethodPolyphonic
			// 
			cboMethodPolyphonic.DropDownStyle = ComboBoxStyle.DropDownList;
			cboMethodPolyphonic.DropDownWidth = 200;
			cboMethodPolyphonic.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboMethodPolyphonic.FormattingEnabled = true;
			cboMethodPolyphonic.Items.AddRange(new object[] { "Queen Mary Polyphonic Transcription", "Silvet Note Transcription", "Alicante Polyphonic Transcription", "Aubio Note Tracker" });
			cboMethodPolyphonic.Location = new Point(13, 79);
			cboMethodPolyphonic.Margin = new Padding(7, 6, 7, 6);
			cboMethodPolyphonic.Name = "cboMethodPolyphonic";
			cboMethodPolyphonic.Size = new Size(288, 34);
			cboMethodPolyphonic.TabIndex = 143;
			// 
			// lblTranscribeAlign
			// 
			lblTranscribeAlign.AutoSize = true;
			lblTranscribeAlign.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTranscribeAlign.Location = new Point(13, 243);
			lblTranscribeAlign.Margin = new Padding(7, 0, 7, 0);
			lblTranscribeAlign.Name = "lblTranscribeAlign";
			lblTranscribeAlign.Size = new Size(97, 26);
			lblTranscribeAlign.TabIndex = 142;
			lblTranscribeAlign.Text = "Align To:";
			// 
			// cboAlignPolyphonic
			// 
			cboAlignPolyphonic.DropDownStyle = ComboBoxStyle.DropDownList;
			cboAlignPolyphonic.DropDownWidth = 180;
			cboAlignPolyphonic.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboAlignPolyphonic.FormattingEnabled = true;
			cboAlignPolyphonic.Items.AddRange(new object[] { "None", "25ms 40fps", "50ms 20fps", "Beats / Quarter Notes", "Half Beats / Eighth Notes", "Quarter Beats / Sixteenth Notes", "Note Onsets" });
			cboAlignPolyphonic.Location = new Point(13, 284);
			cboAlignPolyphonic.Margin = new Padding(7, 6, 7, 6);
			cboAlignPolyphonic.Name = "cboAlignPolyphonic";
			cboAlignPolyphonic.Size = new Size(288, 34);
			cboAlignPolyphonic.TabIndex = 141;
			// 
			// chkChromathing
			// 
			chkChromathing.AutoSize = true;
			chkChromathing.Enabled = false;
			chkChromathing.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkChromathing.Location = new Point(1378, 194);
			chkChromathing.Margin = new Padding(7, 6, 7, 6);
			chkChromathing.Name = "chkChromathing";
			chkChromathing.Size = new Size(173, 34);
			chkChromathing.TabIndex = 134;
			chkChromathing.Tag = "8";
			chkChromathing.Text = "Chromagram";
			chkChromathing.UseVisualStyleBackColor = true;
			chkChromathing.Visible = false;
			chkChromathing.CheckedChanged += chkTiming_CheckedChanged;
			// 
			// grpOnsets
			// 
			grpOnsets.Controls.Add(pnlOnsetSensitivity);
			grpOnsets.Controls.Add(lblDetectOnsets);
			grpOnsets.Controls.Add(cboOnsetsDetect);
			grpOnsets.Controls.Add(lblOnsetsLabels);
			grpOnsets.Controls.Add(cboLabelsOnsets);
			grpOnsets.Controls.Add(chkNoteOnsets);
			grpOnsets.Controls.Add(lblOnsetsPlugin);
			grpOnsets.Controls.Add(cboMethodOnsets);
			grpOnsets.Controls.Add(lblOnsetsAlign);
			grpOnsets.Controls.Add(cboAlignOnsets);
			grpOnsets.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			grpOnsets.Location = new Point(680, 194);
			grpOnsets.Margin = new Padding(7, 6, 7, 6);
			grpOnsets.Name = "grpOnsets";
			grpOnsets.Padding = new Padding(7, 6, 7, 6);
			grpOnsets.Size = new Size(319, 806);
			grpOnsets.TabIndex = 137;
			grpOnsets.TabStop = false;
			grpOnsets.Text = "   Note Onsets ";
			// 
			// pnlOnsetSensitivity
			// 
			pnlOnsetSensitivity.Controls.Add(vscSensitivity);
			pnlOnsetSensitivity.Controls.Add(txtOnsetSensitivity);
			pnlOnsetSensitivity.Controls.Add(lblOnsetsSensitivity);
			pnlOnsetSensitivity.Location = new Point(17, 529);
			pnlOnsetSensitivity.Margin = new Padding(7, 6, 7, 6);
			pnlOnsetSensitivity.Name = "pnlOnsetSensitivity";
			pnlOnsetSensitivity.Size = new Size(219, 58);
			pnlOnsetSensitivity.TabIndex = 152;
			// 
			// vscSensitivity
			// 
			vscSensitivity.Location = new Point(184, -2);
			vscSensitivity.Minimum = 10;
			vscSensitivity.Name = "vscSensitivity";
			vscSensitivity.Size = new Size(16, 45);
			vscSensitivity.TabIndex = 28;
			vscSensitivity.Value = 10;
			vscSensitivity.Scroll += vScrollBar1_Scroll;
			// 
			// txtOnsetSensitivity
			// 
			txtOnsetSensitivity.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			txtOnsetSensitivity.Location = new Point(117, 0);
			txtOnsetSensitivity.Margin = new Padding(7, 6, 7, 6);
			txtOnsetSensitivity.Name = "txtOnsetSensitivity";
			txtOnsetSensitivity.ReadOnly = true;
			txtOnsetSensitivity.Size = new Size(58, 32);
			txtOnsetSensitivity.TabIndex = 7;
			txtOnsetSensitivity.Text = "50";
			// 
			// lblOnsetsSensitivity
			// 
			lblOnsetsSensitivity.AutoSize = true;
			lblOnsetsSensitivity.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblOnsetsSensitivity.Location = new Point(2, 11);
			lblOnsetsSensitivity.Margin = new Padding(7, 0, 7, 0);
			lblOnsetsSensitivity.Name = "lblOnsetsSensitivity";
			lblOnsetsSensitivity.Size = new Size(117, 26);
			lblOnsetsSensitivity.TabIndex = 6;
			lblOnsetsSensitivity.Text = "Sensitivity:";
			lblOnsetsSensitivity.UseMnemonic = false;
			// 
			// lblDetectOnsets
			// 
			lblDetectOnsets.AutoSize = true;
			lblDetectOnsets.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblDetectOnsets.Location = new Point(17, 422);
			lblDetectOnsets.Margin = new Padding(7, 0, 7, 0);
			lblDetectOnsets.Name = "lblDetectOnsets";
			lblDetectOnsets.Size = new Size(188, 26);
			lblDetectOnsets.TabIndex = 151;
			lblDetectOnsets.Text = "Detection Method:";
			// 
			// cboOnsetsDetect
			// 
			cboOnsetsDetect.DropDownStyle = ComboBoxStyle.DropDownList;
			cboOnsetsDetect.DropDownWidth = 300;
			cboOnsetsDetect.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboOnsetsDetect.FormattingEnabled = true;
			cboOnsetsDetect.Items.AddRange(new object[] { "'Complex Domain' (Strings/Mixed: Piano, Guitar)", "'Spectral Difference' (Percussion: Drums, Chimes)", "'Phase Deviation' (Wind: Flute, Sax, Trumpet)", "'Broadband Energy Rise' (Percussion mixed with other)" });
			cboOnsetsDetect.Location = new Point(17, 463);
			cboOnsetsDetect.Margin = new Padding(7, 6, 7, 6);
			cboOnsetsDetect.Name = "cboOnsetsDetect";
			cboOnsetsDetect.Size = new Size(288, 34);
			cboOnsetsDetect.TabIndex = 150;
			// 
			// lblOnsetsLabels
			// 
			lblOnsetsLabels.AutoSize = true;
			lblOnsetsLabels.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblOnsetsLabels.Location = new Point(13, 154);
			lblOnsetsLabels.Margin = new Padding(7, 0, 7, 0);
			lblOnsetsLabels.Name = "lblOnsetsLabels";
			lblOnsetsLabels.Size = new Size(82, 26);
			lblOnsetsLabels.TabIndex = 149;
			lblOnsetsLabels.Text = "Labels:";
			// 
			// cboLabelsOnsets
			// 
			cboLabelsOnsets.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLabelsOnsets.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboLabelsOnsets.FormattingEnabled = true;
			cboLabelsOnsets.Items.AddRange(new object[] { "None", "Note Names", "MIDI Note Numbers" });
			cboLabelsOnsets.Location = new Point(13, 192);
			cboLabelsOnsets.Margin = new Padding(7, 6, 7, 6);
			cboLabelsOnsets.Name = "cboLabelsOnsets";
			cboLabelsOnsets.Size = new Size(288, 34);
			cboLabelsOnsets.TabIndex = 148;
			// 
			// chkNoteOnsets
			// 
			chkNoteOnsets.AutoSize = true;
			chkNoteOnsets.Checked = true;
			chkNoteOnsets.CheckState = CheckState.Checked;
			chkNoteOnsets.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkNoteOnsets.Location = new Point(0, 0);
			chkNoteOnsets.Margin = new Padding(7, 6, 7, 6);
			chkNoteOnsets.Name = "chkNoteOnsets";
			chkNoteOnsets.Size = new Size(28, 27);
			chkNoteOnsets.TabIndex = 145;
			chkNoteOnsets.Tag = "5";
			chkNoteOnsets.UseVisualStyleBackColor = true;
			// 
			// lblOnsetsPlugin
			// 
			lblOnsetsPlugin.AutoSize = true;
			lblOnsetsPlugin.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblOnsetsPlugin.Location = new Point(13, 38);
			lblOnsetsPlugin.Margin = new Padding(7, 0, 7, 0);
			lblOnsetsPlugin.Name = "lblOnsetsPlugin";
			lblOnsetsPlugin.Size = new Size(169, 26);
			lblOnsetsPlugin.TabIndex = 144;
			lblOnsetsPlugin.Text = "Plugin / Method:";
			// 
			// cboMethodOnsets
			// 
			cboMethodOnsets.DropDownStyle = ComboBoxStyle.DropDownList;
			cboMethodOnsets.DropDownWidth = 200;
			cboMethodOnsets.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboMethodOnsets.FormattingEnabled = true;
			cboMethodOnsets.Items.AddRange(new object[] { "Queen Mary Note Onset Detector", "Queen Mary Polyphonic Transcription", "OnsetDS Onset Detector", "Silvet Note Transcription", "Aubio Onset Detector", "Aubio Note Tracker", "#Alicante Note Onset Detector", "#Alicante Polyphonic Transcription" });
			cboMethodOnsets.Location = new Point(13, 79);
			cboMethodOnsets.Margin = new Padding(7, 6, 7, 6);
			cboMethodOnsets.Name = "cboMethodOnsets";
			cboMethodOnsets.Size = new Size(288, 34);
			cboMethodOnsets.TabIndex = 143;
			cboMethodOnsets.SelectedIndexChanged += cboOnsetsPlugin_SelectedIndexChanged;
			// 
			// lblOnsetsAlign
			// 
			lblOnsetsAlign.AutoSize = true;
			lblOnsetsAlign.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblOnsetsAlign.Location = new Point(13, 252);
			lblOnsetsAlign.Margin = new Padding(7, 0, 7, 0);
			lblOnsetsAlign.Name = "lblOnsetsAlign";
			lblOnsetsAlign.Size = new Size(97, 26);
			lblOnsetsAlign.TabIndex = 142;
			lblOnsetsAlign.Text = "Align To:";
			// 
			// cboAlignOnsets
			// 
			cboAlignOnsets.DropDownStyle = ComboBoxStyle.DropDownList;
			cboAlignOnsets.DropDownWidth = 180;
			cboAlignOnsets.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboAlignOnsets.FormattingEnabled = true;
			cboAlignOnsets.Items.AddRange(new object[] { "None", "25ms 40fps", "50ms 20fps", "Beats / Quarter Notes", "Half Beats / Eighth Notes", "Quarter Beats / Sixteenth Notes" });
			cboAlignOnsets.Location = new Point(13, 290);
			cboAlignOnsets.Margin = new Padding(7, 6, 7, 6);
			cboAlignOnsets.Name = "cboAlignOnsets";
			cboAlignOnsets.Size = new Size(301, 34);
			cboAlignOnsets.TabIndex = 141;
			// 
			// grpBarsBeats
			// 
			grpBarsBeats.Controls.Add(vScrollBar1);
			grpBarsBeats.Controls.Add(lblDetectBarBeats);
			grpBarsBeats.Controls.Add(cboDetectBarBeats);
			grpBarsBeats.Controls.Add(label1);
			grpBarsBeats.Controls.Add(cboLabelsBarBeats);
			grpBarsBeats.Controls.Add(chkBarsBeats);
			grpBarsBeats.Controls.Add(pnlNotes);
			grpBarsBeats.Controls.Add(lblMethod);
			grpBarsBeats.Controls.Add(cboMethodBarsBeats);
			grpBarsBeats.Controls.Add(lblAlignBarsBeats);
			grpBarsBeats.Controls.Add(cboAlignBarBeats);
			grpBarsBeats.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			grpBarsBeats.Location = new Point(349, 190);
			grpBarsBeats.Margin = new Padding(7, 6, 7, 6);
			grpBarsBeats.Name = "grpBarsBeats";
			grpBarsBeats.Padding = new Padding(7, 6, 7, 6);
			grpBarsBeats.Size = new Size(319, 813);
			grpBarsBeats.TabIndex = 136;
			grpBarsBeats.TabStop = false;
			grpBarsBeats.Text = "   Bars && Beats ";
			// 
			// vScrollBar1
			// 
			vScrollBar1.Location = new Point(176, 748);
			vScrollBar1.Minimum = 10;
			vScrollBar1.Name = "vScrollBar1";
			vScrollBar1.Size = new Size(16, 45);
			vScrollBar1.TabIndex = 165;
			vScrollBar1.Value = 10;
			// 
			// lblDetectBarBeats
			// 
			lblDetectBarBeats.AutoSize = true;
			lblDetectBarBeats.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblDetectBarBeats.Location = new Point(15, 670);
			lblDetectBarBeats.Margin = new Padding(7, 0, 7, 0);
			lblDetectBarBeats.Name = "lblDetectBarBeats";
			lblDetectBarBeats.Size = new Size(188, 26);
			lblDetectBarBeats.TabIndex = 163;
			lblDetectBarBeats.Text = "Detection Method:";
			// 
			// cboDetectBarBeats
			// 
			cboDetectBarBeats.DropDownStyle = ComboBoxStyle.DropDownList;
			cboDetectBarBeats.DropDownWidth = 300;
			cboDetectBarBeats.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboDetectBarBeats.FormattingEnabled = true;
			cboDetectBarBeats.Items.AddRange(new object[] { "'Complex Domain' (Strings/Mixed: Piano, Guitar)", "'Spectral Difference' (Percussion: Drums, Chimes)", "'Phase Deviation' (Wind: Flute, Sax, Trumpet)", "'Broadband Energy Rise' (Percussion mixed with other)" });
			cboDetectBarBeats.Location = new Point(13, 708);
			cboDetectBarBeats.Margin = new Padding(7, 6, 7, 6);
			cboDetectBarBeats.Name = "cboDetectBarBeats";
			cboDetectBarBeats.Size = new Size(288, 34);
			cboDetectBarBeats.TabIndex = 162;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(13, 143);
			label1.Margin = new Padding(7, 0, 7, 0);
			label1.Name = "label1";
			label1.Size = new Size(82, 26);
			label1.TabIndex = 161;
			label1.Text = "Labels:";
			// 
			// cboLabelsBarBeats
			// 
			cboLabelsBarBeats.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLabelsBarBeats.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboLabelsBarBeats.FormattingEnabled = true;
			cboLabelsBarBeats.Items.AddRange(new object[] { "None", "Note Names", "MIDI Note Numbers" });
			cboLabelsBarBeats.Location = new Point(13, 181);
			cboLabelsBarBeats.Margin = new Padding(7, 6, 7, 6);
			cboLabelsBarBeats.Name = "cboLabelsBarBeats";
			cboLabelsBarBeats.Size = new Size(288, 34);
			cboLabelsBarBeats.TabIndex = 160;
			// 
			// chkBarsBeats
			// 
			chkBarsBeats.AutoSize = true;
			chkBarsBeats.Checked = true;
			chkBarsBeats.CheckState = CheckState.Checked;
			chkBarsBeats.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkBarsBeats.Location = new Point(0, 0);
			chkBarsBeats.Margin = new Padding(7, 6, 7, 6);
			chkBarsBeats.Name = "chkBarsBeats";
			chkBarsBeats.Size = new Size(28, 27);
			chkBarsBeats.TabIndex = 156;
			chkBarsBeats.Tag = "13";
			chkBarsBeats.UseVisualStyleBackColor = true;
			// 
			// pnlNotes
			// 
			pnlNotes.Controls.Add(chkBeatsThird);
			pnlNotes.Controls.Add(chkBars);
			pnlNotes.Controls.Add(chkBeatsFull);
			pnlNotes.Controls.Add(chkBeatsHalf);
			pnlNotes.Controls.Add(chkBeatsQuarter);
			pnlNotes.Controls.Add(lblBeats2Half);
			pnlNotes.Controls.Add(lblBeats1Full);
			pnlNotes.Controls.Add(lblBeats4Quarter);
			pnlNotes.Controls.Add(lblBeats0Bars);
			pnlNotes.Location = new Point(4, 354);
			pnlNotes.Margin = new Padding(7, 6, 7, 6);
			pnlNotes.Name = "pnlNotes";
			pnlNotes.Size = new Size(314, 290);
			pnlNotes.TabIndex = 155;
			pnlNotes.Paint += pnlNotes_Paint;
			// 
			// chkBeatsThird
			// 
			chkBeatsThird.AutoSize = true;
			chkBeatsThird.Font = new Font("Bahnschrift Condensed", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkBeatsThird.Location = new Point(43, 162);
			chkBeatsThird.Margin = new Padding(7, 6, 7, 6);
			chkBeatsThird.Name = "chkBeatsThird";
			chkBeatsThird.Size = new Size(126, 31);
			chkBeatsThird.TabIndex = 141;
			chkBeatsThird.Tag = "3";
			chkBeatsThird.Text = "Third Beats";
			chkBeatsThird.UseVisualStyleBackColor = true;
			// 
			// chkBars
			// 
			chkBars.AutoSize = true;
			chkBars.Checked = true;
			chkBars.CheckState = CheckState.Checked;
			chkBars.Font = new Font("Bahnschrift Condensed", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkBars.Location = new Point(43, 6);
			chkBars.Margin = new Padding(7, 6, 7, 6);
			chkBars.Name = "chkBars";
			chkBars.Size = new Size(169, 31);
			chkBars.TabIndex = 139;
			chkBars.Tag = "0";
			chkBars.Text = "Bars Whole Notes";
			chkBars.UseVisualStyleBackColor = true;
			// 
			// chkBeatsFull
			// 
			chkBeatsFull.AutoSize = true;
			chkBeatsFull.Checked = true;
			chkBeatsFull.CheckState = CheckState.Checked;
			chkBeatsFull.Font = new Font("Bahnschrift Condensed", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkBeatsFull.Location = new Point(43, 60);
			chkBeatsFull.Margin = new Padding(7, 6, 7, 6);
			chkBeatsFull.Name = "chkBeatsFull";
			chkBeatsFull.Size = new Size(222, 31);
			chkBeatsFull.TabIndex = 143;
			chkBeatsFull.Tag = "1";
			chkBeatsFull.Text = "Full Beats Quarter Notes";
			chkBeatsFull.UseVisualStyleBackColor = true;
			// 
			// chkBeatsHalf
			// 
			chkBeatsHalf.AutoSize = true;
			chkBeatsHalf.Checked = true;
			chkBeatsHalf.CheckState = CheckState.Checked;
			chkBeatsHalf.Font = new Font("Bahnschrift Condensed", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkBeatsHalf.Location = new Point(43, 111);
			chkBeatsHalf.Margin = new Padding(7, 6, 7, 6);
			chkBeatsHalf.Name = "chkBeatsHalf";
			chkBeatsHalf.Size = new Size(212, 31);
			chkBeatsHalf.TabIndex = 142;
			chkBeatsHalf.Tag = "2";
			chkBeatsHalf.Text = "Half Beats Eighth Notes";
			chkBeatsHalf.UseVisualStyleBackColor = true;
			// 
			// chkBeatsQuarter
			// 
			chkBeatsQuarter.AutoSize = true;
			chkBeatsQuarter.Checked = true;
			chkBeatsQuarter.CheckState = CheckState.Checked;
			chkBeatsQuarter.Font = new Font("Bahnschrift Condensed", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkBeatsQuarter.Location = new Point(43, 213);
			chkBeatsQuarter.Margin = new Padding(7, 6, 7, 6);
			chkBeatsQuarter.Name = "chkBeatsQuarter";
			chkBeatsQuarter.Size = new Size(265, 31);
			chkBeatsQuarter.TabIndex = 140;
			chkBeatsQuarter.Tag = "4";
			chkBeatsQuarter.Text = "Quarter Beats Sixteenth Notes";
			chkBeatsQuarter.UseVisualStyleBackColor = true;
			// 
			// lblBeats2Half
			// 
			lblBeats2Half.AutoSize = true;
			lblBeats2Half.Font = new Font("Segoe UI Symbol", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
			lblBeats2Half.Location = new Point(0, 92);
			lblBeats2Half.Margin = new Padding(7, 0, 7, 0);
			lblBeats2Half.Name = "lblBeats2Half";
			lblBeats2Half.Size = new Size(47, 57);
			lblBeats2Half.TabIndex = 149;
			lblBeats2Half.Text = "𝅘𝅥𝅮";
			lblBeats2Half.Visible = false;
			// 
			// lblBeats1Full
			// 
			lblBeats1Full.AutoSize = true;
			lblBeats1Full.Font = new Font("Segoe UI Symbol", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
			lblBeats1Full.Location = new Point(0, 38);
			lblBeats1Full.Margin = new Padding(7, 0, 7, 0);
			lblBeats1Full.Name = "lblBeats1Full";
			lblBeats1Full.Size = new Size(41, 57);
			lblBeats1Full.TabIndex = 150;
			lblBeats1Full.Text = "𝅘𝅥";
			lblBeats1Full.Visible = false;
			// 
			// lblBeats4Quarter
			// 
			lblBeats4Quarter.AutoSize = true;
			lblBeats4Quarter.Font = new Font("Segoe UI Symbol", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
			lblBeats4Quarter.Location = new Point(0, 192);
			lblBeats4Quarter.Margin = new Padding(7, 0, 7, 0);
			lblBeats4Quarter.Name = "lblBeats4Quarter";
			lblBeats4Quarter.Size = new Size(47, 57);
			lblBeats4Quarter.TabIndex = 152;
			lblBeats4Quarter.Text = "𝅘𝅥𝅯";
			lblBeats4Quarter.Visible = false;
			// 
			// lblBeats0Bars
			// 
			lblBeats0Bars.AutoSize = true;
			lblBeats0Bars.Font = new Font("Segoe UI Symbol", 24F, FontStyle.Regular, GraphicsUnit.Point);
			lblBeats0Bars.Location = new Point(0, 0);
			lblBeats0Bars.Margin = new Padding(7, 0, 7, 0);
			lblBeats0Bars.Name = "lblBeats0Bars";
			lblBeats0Bars.Size = new Size(62, 86);
			lblBeats0Bars.TabIndex = 153;
			lblBeats0Bars.Text = "𝅗";
			lblBeats0Bars.Visible = false;
			// 
			// lblMethod
			// 
			lblMethod.AutoSize = true;
			lblMethod.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblMethod.Location = new Point(13, 38);
			lblMethod.Margin = new Padding(7, 0, 7, 0);
			lblMethod.Name = "lblMethod";
			lblMethod.Size = new Size(169, 26);
			lblMethod.TabIndex = 144;
			lblMethod.Text = "Plugin / Method:";
			// 
			// cboMethodBarsBeats
			// 
			cboMethodBarsBeats.DropDownStyle = ComboBoxStyle.DropDownList;
			cboMethodBarsBeats.DropDownWidth = 200;
			cboMethodBarsBeats.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboMethodBarsBeats.FormattingEnabled = true;
			cboMethodBarsBeats.Items.AddRange(new object[] { "Queen Mary Bar and Beat Tracker", "Queen Mary Tempo and Beat Tracker", "BeatRoot Beat Tracker", "Aubio Beat Tracker" });
			cboMethodBarsBeats.Location = new Point(13, 79);
			cboMethodBarsBeats.Margin = new Padding(7, 6, 7, 6);
			cboMethodBarsBeats.Name = "cboMethodBarsBeats";
			cboMethodBarsBeats.Size = new Size(288, 34);
			cboMethodBarsBeats.TabIndex = 143;
			cboMethodBarsBeats.SelectedIndexChanged += cboBarBeatMethod_SelectedIndexChanged;
			// 
			// lblAlignBarsBeats
			// 
			lblAlignBarsBeats.AutoSize = true;
			lblAlignBarsBeats.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblAlignBarsBeats.Location = new Point(13, 241);
			lblAlignBarsBeats.Margin = new Padding(7, 0, 7, 0);
			lblAlignBarsBeats.Name = "lblAlignBarsBeats";
			lblAlignBarsBeats.Size = new Size(97, 26);
			lblAlignBarsBeats.TabIndex = 142;
			lblAlignBarsBeats.Text = "Align To:";
			// 
			// cboAlignBarBeats
			// 
			cboAlignBarBeats.DropDownStyle = ComboBoxStyle.DropDownList;
			cboAlignBarBeats.DropDownWidth = 135;
			cboAlignBarBeats.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			cboAlignBarBeats.FormattingEnabled = true;
			cboAlignBarBeats.Items.AddRange(new object[] { "None", "25ms 40fps", "50ms 20fps" });
			cboAlignBarBeats.Location = new Point(13, 282);
			cboAlignBarBeats.Margin = new Padding(7, 6, 7, 6);
			cboAlignBarBeats.Name = "cboAlignBarBeats";
			cboAlignBarBeats.Size = new Size(288, 34);
			cboAlignBarBeats.TabIndex = 141;
			// 
			// lblStep2B
			// 
			lblStep2B.AutoSize = true;
			lblStep2B.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			lblStep2B.ForeColor = SystemColors.Highlight;
			lblStep2B.Location = new Point(13, -13);
			lblStep2B.Margin = new Padding(7, 0, 7, 0);
			lblStep2B.Name = "lblStep2B";
			lblStep2B.Size = new Size(41, 47);
			lblStep2B.TabIndex = 121;
			lblStep2B.Text = "2";
			// 
			// chkFlux
			// 
			chkFlux.AutoSize = true;
			chkFlux.Enabled = false;
			chkFlux.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkFlux.Location = new Point(1378, 307);
			chkFlux.Margin = new Padding(7, 6, 7, 6);
			chkFlux.Name = "chkFlux";
			chkFlux.Size = new Size(83, 34);
			chkFlux.TabIndex = 128;
			chkFlux.Tag = "12";
			chkFlux.Text = "Flux";
			chkFlux.UseVisualStyleBackColor = true;
			chkFlux.Visible = false;
			chkFlux.CheckedChanged += chkTiming_CheckedChanged;
			// 
			// pnlVamping
			// 
			pnlVamping.Controls.Add(lblSongName);
			pnlVamping.Controls.Add(lblAnalyzing);
			pnlVamping.Controls.Add(lblWait);
			pnlVamping.Controls.Add(lblVampRed);
			pnlVamping.Location = new Point(345, 433);
			pnlVamping.Margin = new Padding(7, 6, 7, 6);
			pnlVamping.Name = "pnlVamping";
			pnlVamping.Size = new Size(724, 177);
			pnlVamping.TabIndex = 125;
			pnlVamping.Visible = false;
			pnlVamping.Paint += pnlVamping_Paint;
			// 
			// lblSongName
			// 
			lblSongName.AutoSize = true;
			lblSongName.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblSongName.ForeColor = Color.Blue;
			lblSongName.Location = new Point(11, 81);
			lblSongName.Margin = new Padding(7, 0, 7, 0);
			lblSongName.Name = "lblSongName";
			lblSongName.Size = new Size(434, 36);
			lblSongName.TabIndex = 3;
			lblSongName.Text = "Blue Christmas by Elvis Presley";
			lblSongName.Click += lblSongName_Click;
			// 
			// lblAnalyzing
			// 
			lblAnalyzing.AutoSize = true;
			lblAnalyzing.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
			lblAnalyzing.ForeColor = Color.Red;
			lblAnalyzing.Location = new Point(11, 17);
			lblAnalyzing.Margin = new Padding(7, 0, 7, 0);
			lblAnalyzing.Name = "lblAnalyzing";
			lblAnalyzing.Size = new Size(385, 48);
			lblAnalyzing.TabIndex = 2;
			lblAnalyzing.Text = "Analyzing your song";
			// 
			// lblWait
			// 
			lblWait.AutoSize = true;
			lblWait.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			lblWait.ForeColor = Color.Red;
			lblWait.Location = new Point(130, 130);
			lblWait.Margin = new Padding(7, 0, 7, 0);
			lblWait.Name = "lblWait";
			lblWait.Size = new Size(152, 29);
			lblWait.TabIndex = 1;
			lblWait.Text = "Please wait...";
			// 
			// lblVampRed
			// 
			lblVampRed.AutoSize = true;
			lblVampRed.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
			lblVampRed.ForeColor = Color.Red;
			lblVampRed.Location = new Point(7, 13);
			lblVampRed.Margin = new Padding(7, 0, 7, 0);
			lblVampRed.Name = "lblVampRed";
			lblVampRed.Size = new Size(124, 48);
			lblVampRed.TabIndex = 0;
			lblVampRed.Text = "Vamp";
			lblVampRed.Visible = false;
			// 
			// grpExplore
			// 
			grpExplore.Controls.Add(btnExploreVamp);
			grpExplore.Controls.Add(btnExplorexLights);
			grpExplore.Controls.Add(btnExploreTemp);
			grpExplore.Controls.Add(btnCmdTemp);
			grpExplore.Controls.Add(btnLaunchxLights);
			grpExplore.Controls.Add(btnSequenceEditor);
			grpExplore.Controls.Add(btnExploreLOR);
			grpExplore.Location = new Point(1443, 17);
			grpExplore.Margin = new Padding(7, 6, 7, 6);
			grpExplore.Name = "grpExplore";
			grpExplore.Padding = new Padding(7, 6, 7, 6);
			grpExplore.Size = new Size(680, 139);
			grpExplore.TabIndex = 180;
			grpExplore.TabStop = false;
			grpExplore.Text = " Explore... ";
			// 
			// btnResetDefaults
			// 
			btnResetDefaults.AllowDrop = true;
			btnResetDefaults.Location = new Point(2234, 646);
			btnResetDefaults.Margin = new Padding(7, 6, 7, 6);
			btnResetDefaults.Name = "btnResetDefaults";
			btnResetDefaults.Size = new Size(267, 58);
			btnResetDefaults.TabIndex = 158;
			btnResetDefaults.Text = "Reset to Defaults";
			btnResetDefaults.UseVisualStyleBackColor = true;
			btnResetDefaults.Click += btnResetDefaults_Click;
			// 
			// grpAudio
			// 
			grpAudio.Controls.Add(cboFile_Audio);
			grpAudio.Controls.Add(lblSongTime);
			grpAudio.Controls.Add(btnBrowseAudio);
			grpAudio.Controls.Add(lblFileAudio);
			grpAudio.Controls.Add(lblStep1);
			grpAudio.Enabled = false;
			grpAudio.Location = new Point(26, 30);
			grpAudio.Margin = new Padding(7, 6, 7, 6);
			grpAudio.Name = "grpAudio";
			grpAudio.Padding = new Padding(7, 6, 7, 6);
			grpAudio.Size = new Size(724, 162);
			grpAudio.TabIndex = 122;
			grpAudio.TabStop = false;
			grpAudio.Text = "      Select Audio File";
			// 
			// cboFile_Audio
			// 
			cboFile_Audio.FormattingEnabled = true;
			cboFile_Audio.Location = new Point(35, 87);
			cboFile_Audio.Margin = new Padding(6);
			cboFile_Audio.Name = "cboFile_Audio";
			cboFile_Audio.Size = new Size(478, 40);
			cboFile_Audio.TabIndex = 164;
			cboFile_Audio.SelectedIndexChanged += cboFile_Audio_SelectedIndexChanged;
			// 
			// lblSongTime
			// 
			lblSongTime.AutoSize = true;
			lblSongTime.Font = new Font("DejaVu Sans Mono", 6.75F, FontStyle.Regular, GraphicsUnit.Point);
			lblSongTime.ForeColor = Color.DarkOliveGreen;
			lblSongTime.Location = new Point(516, 30);
			lblSongTime.Margin = new Padding(7, 0, 7, 0);
			lblSongTime.Name = "lblSongTime";
			lblSongTime.Size = new Size(87, 21);
			lblSongTime.TabIndex = 163;
			lblSongTime.Text = "0:00.00";
			lblSongTime.Visible = false;
			// 
			// btnBrowseAudio
			// 
			btnBrowseAudio.AllowDrop = true;
			btnBrowseAudio.Location = new Point(529, 85);
			btnBrowseAudio.Margin = new Padding(7, 6, 7, 6);
			btnBrowseAudio.Name = "btnBrowseAudio";
			btnBrowseAudio.Size = new Size(163, 49);
			btnBrowseAudio.TabIndex = 124;
			btnBrowseAudio.Text = "Browse...";
			btnBrowseAudio.UseVisualStyleBackColor = true;
			btnBrowseAudio.Click += btnBrowseAudio_Click;
			// 
			// lblFileAudio
			// 
			lblFileAudio.AllowDrop = true;
			lblFileAudio.AutoSize = true;
			lblFileAudio.Location = new Point(35, 47);
			lblFileAudio.Margin = new Padding(7, 0, 7, 0);
			lblFileAudio.Name = "lblFileAudio";
			lblFileAudio.Size = new Size(121, 32);
			lblFileAudio.TabIndex = 125;
			lblFileAudio.Text = "Audio File";
			// 
			// lblStep1
			// 
			lblStep1.AutoSize = true;
			lblStep1.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			lblStep1.ForeColor = SystemColors.Highlight;
			lblStep1.Location = new Point(13, -13);
			lblStep1.Margin = new Padding(7, 0, 7, 0);
			lblStep1.Name = "lblStep1";
			lblStep1.Size = new Size(41, 47);
			lblStep1.TabIndex = 122;
			lblStep1.Text = "1";
			// 
			// grpSavex
			// 
			grpSavex.Controls.Add(picxLights);
			grpSavex.Controls.Add(panel2);
			grpSavex.Controls.Add(btnSaveOptions);
			grpSavex.Controls.Add(lblStep4A);
			grpSavex.Controls.Add(lblFilexTimings);
			grpSavex.Controls.Add(btnSavexL);
			grpSavex.Controls.Add(txtSaveNamexL);
			grpSavex.Enabled = false;
			grpSavex.Location = new Point(26, 1114);
			grpSavex.Margin = new Padding(7, 6, 7, 6);
			grpSavex.Name = "grpSavex";
			grpSavex.Padding = new Padding(7, 6, 7, 6);
			grpSavex.Size = new Size(724, 273);
			grpSavex.TabIndex = 123;
			grpSavex.TabStop = false;
			grpSavex.Text = "          Save                              Timings";
			// 
			// picxLights
			// 
			picxLights.BackgroundImage = (Image)resources.GetObject("picxLights.BackgroundImage");
			picxLights.InitialImage = (Image)resources.GetObject("picxLights.InitialImage");
			picxLights.Location = new Point(123, -4);
			picxLights.Margin = new Padding(7, 6, 7, 6);
			picxLights.Name = "picxLights";
			picxLights.Size = new Size(158, 55);
			picxLights.TabIndex = 137;
			picxLights.TabStop = false;
			// 
			// panel2
			// 
			panel2.Controls.Add(optOnePer);
			panel2.Controls.Add(optMultiPer);
			panel2.Location = new Point(41, 47);
			panel2.Margin = new Padding(7, 6, 7, 6);
			panel2.Name = "panel2";
			panel2.Size = new Size(600, 64);
			panel2.TabIndex = 136;
			// 
			// optOnePer
			// 
			optOnePer.AutoSize = true;
			optOnePer.Checked = true;
			optOnePer.Location = new Point(277, 13);
			optOnePer.Margin = new Padding(7, 6, 7, 6);
			optOnePer.Name = "optOnePer";
			optOnePer.Size = new Size(281, 39);
			optOnePer.TabIndex = 139;
			optOnePer.TabStop = true;
			optOnePer.Text = "Individual Timing Files";
			optOnePer.UseCompatibleTextRendering = true;
			optOnePer.UseVisualStyleBackColor = true;
			// 
			// optMultiPer
			// 
			optMultiPer.AutoSize = true;
			optMultiPer.Location = new Point(7, 13);
			optMultiPer.Margin = new Padding(7, 6, 7, 6);
			optMultiPer.Name = "optMultiPer";
			optMultiPer.Size = new Size(231, 39);
			optMultiPer.TabIndex = 138;
			optMultiPer.Text = "Single Timing File";
			optMultiPer.UseCompatibleTextRendering = true;
			optMultiPer.UseVisualStyleBackColor = true;
			// 
			// btnSaveOptions
			// 
			btnSaveOptions.AllowDrop = true;
			btnSaveOptions.Location = new Point(409, 237);
			btnSaveOptions.Margin = new Padding(7, 6, 7, 6);
			btnSaveOptions.Name = "btnSaveOptions";
			btnSaveOptions.Size = new Size(163, 58);
			btnSaveOptions.TabIndex = 129;
			btnSaveOptions.Text = "Settings...";
			btnSaveOptions.UseVisualStyleBackColor = true;
			btnSaveOptions.Visible = false;
			btnSaveOptions.Click += btnSaveOptions_Click_1;
			// 
			// lblStep4A
			// 
			lblStep4A.AutoSize = true;
			lblStep4A.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			lblStep4A.ForeColor = SystemColors.Highlight;
			lblStep4A.Location = new Point(13, -13);
			lblStep4A.Margin = new Padding(7, 0, 7, 0);
			lblStep4A.Name = "lblStep4A";
			lblStep4A.Size = new Size(62, 47);
			lblStep4A.TabIndex = 127;
			lblStep4A.Text = "4a";
			// 
			// lblFilexTimings
			// 
			lblFilexTimings.AllowDrop = true;
			lblFilexTimings.AutoSize = true;
			lblFilexTimings.Location = new Point(35, 126);
			lblFilexTimings.Margin = new Padding(7, 0, 7, 0);
			lblFilexTimings.Name = "lblFilexTimings";
			lblFilexTimings.Size = new Size(221, 32);
			lblFilexTimings.TabIndex = 126;
			lblFilexTimings.Text = "Timing Filename(s):";
			// 
			// btnSavexL
			// 
			btnSavexL.AllowDrop = true;
			btnSavexL.Location = new Point(529, 158);
			btnSavexL.Margin = new Padding(7, 6, 7, 6);
			btnSavexL.Name = "btnSavexL";
			btnSavexL.Size = new Size(163, 58);
			btnSavexL.TabIndex = 125;
			btnSavexL.Text = "Save As...";
			btnSavexL.UseVisualStyleBackColor = true;
			btnSavexL.Click += btnSavexL_Click;
			// 
			// txtSaveNamexL
			// 
			txtSaveNamexL.AllowDrop = true;
			txtSaveNamexL.Location = new Point(41, 164);
			txtSaveNamexL.Margin = new Padding(7, 6, 7, 6);
			txtSaveNamexL.Name = "txtSaveNamexL";
			txtSaveNamexL.ReadOnly = true;
			txtSaveNamexL.Size = new Size(470, 39);
			txtSaveNamexL.TabIndex = 124;
			// 
			// imlTreeIcons
			// 
			imlTreeIcons.ColorDepth = ColorDepth.Depth32Bit;
			imlTreeIcons.ImageStream = (ImageListStreamer)resources.GetObject("imlTreeIcons.ImageStream");
			imlTreeIcons.TransparentColor = Color.Transparent;
			imlTreeIcons.Images.SetKeyName(0, "LOR4Track");
			imlTreeIcons.Images.SetKeyName(1, "LOR4ChannelGroup");
			imlTreeIcons.Images.SetKeyName(2, "LOR4RGBChannel");
			imlTreeIcons.Images.SetKeyName(3, "LOR4Channel");
			imlTreeIcons.Images.SetKeyName(4, "RedCh");
			imlTreeIcons.Images.SetKeyName(5, "GrnCh");
			imlTreeIcons.Images.SetKeyName(6, "BluCh");
			// 
			// grpAnalyze
			// 
			grpAnalyze.Controls.Add(btnReanalyze);
			grpAnalyze.Controls.Add(lblVampTime);
			grpAnalyze.Controls.Add(btnOK);
			grpAnalyze.Controls.Add(lblStep3);
			grpAnalyze.Enabled = false;
			grpAnalyze.Location = new Point(26, 930);
			grpAnalyze.Margin = new Padding(7, 6, 7, 6);
			grpAnalyze.Name = "grpAnalyze";
			grpAnalyze.Padding = new Padding(7, 6, 7, 6);
			grpAnalyze.Size = new Size(724, 162);
			grpAnalyze.TabIndex = 126;
			grpAnalyze.TabStop = false;
			grpAnalyze.Text = "      Analyze the Audio File";
			// 
			// btnReanalyze
			// 
			btnReanalyze.AllowDrop = true;
			btnReanalyze.DialogResult = DialogResult.OK;
			btnReanalyze.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnReanalyze.Location = new Point(351, 49);
			btnReanalyze.Margin = new Padding(7, 6, 7, 6);
			btnReanalyze.Name = "btnReanalyze";
			btnReanalyze.Size = new Size(253, 100);
			btnReanalyze.TabIndex = 165;
			btnReanalyze.Text = "Change Options and\r\nAnalyze Again...";
			btnReanalyze.UseVisualStyleBackColor = true;
			btnReanalyze.Visible = false;
			btnReanalyze.Click += btnReanalyze_Click;
			// 
			// lblVampTime
			// 
			lblVampTime.AutoSize = true;
			lblVampTime.Font = new Font("DejaVu Sans Mono", 6.75F, FontStyle.Regular, GraphicsUnit.Point);
			lblVampTime.ForeColor = Color.DarkOliveGreen;
			lblVampTime.Location = new Point(516, 47);
			lblVampTime.Margin = new Padding(7, 0, 7, 0);
			lblVampTime.Name = "lblVampTime";
			lblVampTime.Size = new Size(87, 21);
			lblVampTime.TabIndex = 164;
			lblVampTime.Text = "0:00.00";
			lblVampTime.Visible = false;
			// 
			// btnOK
			// 
			btnOK.AllowDrop = true;
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			btnOK.Location = new Point(193, 47);
			btnOK.Margin = new Padding(7, 6, 7, 6);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(253, 100);
			btnOK.TabIndex = 124;
			btnOK.Text = "Analyze...";
			btnOK.UseVisualStyleBackColor = true;
			btnOK.Click += btnOK_Click;
			// 
			// lblStep3
			// 
			lblStep3.AutoSize = true;
			lblStep3.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			lblStep3.ForeColor = SystemColors.Highlight;
			lblStep3.Location = new Point(13, -13);
			lblStep3.Margin = new Padding(7, 0, 7, 0);
			lblStep3.Name = "lblStep3";
			lblStep3.Size = new Size(41, 47);
			lblStep3.TabIndex = 122;
			lblStep3.Text = "3";
			// 
			// grpOptions
			// 
			grpOptions.BackColor = SystemColors.Control;
			grpOptions.Controls.Add(pnlVamping);
			grpOptions.Controls.Add(picWorking);
			grpOptions.Controls.Add(lblPickYourPoison);
			grpOptions.Controls.Add(chkReuse);
			grpOptions.Controls.Add(picVampire);
			grpOptions.Controls.Add(lblHelpOnsets);
			grpOptions.Controls.Add(picPoisonArrow);
			grpOptions.Controls.Add(lblStep2A);
			grpOptions.Enabled = false;
			grpOptions.Location = new Point(26, 220);
			grpOptions.Margin = new Padding(7, 6, 7, 6);
			grpOptions.Name = "grpOptions";
			grpOptions.Padding = new Padding(7, 6, 7, 6);
			grpOptions.Size = new Size(724, 691);
			grpOptions.TabIndex = 130;
			grpOptions.TabStop = false;
			grpOptions.Text = "      Select Timings and Options";
			// 
			// picWorking
			// 
			picWorking.Image = (Image)resources.GetObject("picWorking.Image");
			picWorking.Location = new Point(355, 175);
			picWorking.Margin = new Padding(7, 6, 7, 6);
			picWorking.Name = "picWorking";
			picWorking.Size = new Size(245, 85);
			picWorking.TabIndex = 157;
			picWorking.TabStop = false;
			picWorking.Visible = false;
			// 
			// lblPickYourPoison
			// 
			lblPickYourPoison.AutoSize = true;
			lblPickYourPoison.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
			lblPickYourPoison.ForeColor = Color.DarkViolet;
			lblPickYourPoison.Location = new Point(284, 164);
			lblPickYourPoison.Margin = new Padding(7, 0, 7, 0);
			lblPickYourPoison.Name = "lblPickYourPoison";
			lblPickYourPoison.Size = new Size(322, 48);
			lblPickYourPoison.TabIndex = 130;
			lblPickYourPoison.Text = "Pick Your Poison";
			// 
			// chkReuse
			// 
			chkReuse.AutoSize = true;
			chkReuse.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point);
			chkReuse.ForeColor = Color.DeepPink;
			chkReuse.Location = new Point(230, 655);
			chkReuse.Margin = new Padding(7, 6, 7, 6);
			chkReuse.Name = "chkReuse";
			chkReuse.Size = new Size(159, 30);
			chkReuse.TabIndex = 135;
			chkReuse.Text = "Re-use files";
			chkReuse.UseVisualStyleBackColor = true;
			chkReuse.Visible = false;
			chkReuse.CheckedChanged += chkReuse_CheckedChanged;
			// 
			// picVampire
			// 
			picVampire.BackgroundImage = (Image)resources.GetObject("picVampire.BackgroundImage");
			picVampire.Location = new Point(48, 111);
			picVampire.Margin = new Padding(7, 6, 7, 6);
			picVampire.Name = "picVampire";
			picVampire.Size = new Size(238, 410);
			picVampire.TabIndex = 136;
			picVampire.TabStop = false;
			// 
			// lblHelpOnsets
			// 
			lblHelpOnsets.AutoSize = true;
			lblHelpOnsets.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point);
			lblHelpOnsets.ForeColor = SystemColors.HotTrack;
			lblHelpOnsets.Location = new Point(513, 655);
			lblHelpOnsets.Margin = new Padding(7, 0, 7, 0);
			lblHelpOnsets.Name = "lblHelpOnsets";
			lblHelpOnsets.Size = new Size(75, 26);
			lblHelpOnsets.TabIndex = 156;
			lblHelpOnsets.Text = "Help...";
			lblHelpOnsets.Click += lblHelpOnsets_Click;
			// 
			// picPoisonArrow
			// 
			picPoisonArrow.BackgroundImage = (Image)resources.GetObject("picPoisonArrow.BackgroundImage");
			picPoisonArrow.Location = new Point(470, 243);
			picPoisonArrow.Margin = new Padding(7, 6, 7, 6);
			picPoisonArrow.Name = "picPoisonArrow";
			picPoisonArrow.Size = new Size(119, 68);
			picPoisonArrow.TabIndex = 131;
			picPoisonArrow.TabStop = false;
			// 
			// lblStep2A
			// 
			lblStep2A.AutoSize = true;
			lblStep2A.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			lblStep2A.ForeColor = SystemColors.Highlight;
			lblStep2A.Location = new Point(13, -13);
			lblStep2A.Margin = new Padding(7, 0, 7, 0);
			lblStep2A.Name = "lblStep2A";
			lblStep2A.Size = new Size(41, 47);
			lblStep2A.TabIndex = 122;
			lblStep2A.Text = "2";
			// 
			// tmrAni
			// 
			tmrAni.Tick += tmrAni_Tick;
			// 
			// grpSaveLOR
			// 
			grpSaveLOR.Controls.Add(lblSeqTime);
			grpSaveLOR.Controls.Add(chkAutolaunch);
			grpSaveLOR.Controls.Add(picLOR);
			grpSaveLOR.Controls.Add(panel3);
			grpSaveLOR.Controls.Add(button1);
			grpSaveLOR.Controls.Add(lblStep4B);
			grpSaveLOR.Controls.Add(lblFileSequence);
			grpSaveLOR.Controls.Add(btnSaveSeq);
			grpSaveLOR.Controls.Add(txtSeqName);
			grpSaveLOR.Enabled = false;
			grpSaveLOR.Location = new Point(26, 1421);
			grpSaveLOR.Margin = new Padding(7, 6, 7, 6);
			grpSaveLOR.Name = "grpSaveLOR";
			grpSaveLOR.Padding = new Padding(7, 6, 7, 6);
			grpSaveLOR.Size = new Size(724, 273);
			grpSaveLOR.TabIndex = 136;
			grpSaveLOR.TabStop = false;
			grpSaveLOR.Text = "          Save                                Sequence";
			// 
			// lblSeqTime
			// 
			lblSeqTime.AutoSize = true;
			lblSeqTime.Font = new Font("DejaVu Sans Mono", 6.75F, FontStyle.Regular, GraphicsUnit.Point);
			lblSeqTime.ForeColor = Color.DarkOliveGreen;
			lblSeqTime.Location = new Point(516, 132);
			lblSeqTime.Margin = new Padding(7, 0, 7, 0);
			lblSeqTime.Name = "lblSeqTime";
			lblSeqTime.Size = new Size(87, 21);
			lblSeqTime.TabIndex = 165;
			lblSeqTime.Text = "0:00.00";
			lblSeqTime.Visible = false;
			// 
			// chkAutolaunch
			// 
			chkAutolaunch.AutoSize = true;
			chkAutolaunch.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			chkAutolaunch.ForeColor = SystemColors.ControlText;
			chkAutolaunch.Location = new Point(48, 218);
			chkAutolaunch.Margin = new Padding(7, 6, 7, 6);
			chkAutolaunch.Name = "chkAutolaunch";
			chkAutolaunch.Size = new Size(327, 30);
			chkAutolaunch.TabIndex = 138;
			chkAutolaunch.Text = "Auto-launch Sequence Editor";
			chkAutolaunch.UseVisualStyleBackColor = true;
			// 
			// picLOR
			// 
			picLOR.BackgroundImage = (Image)resources.GetObject("picLOR.BackgroundImage");
			picLOR.Location = new Point(123, 0);
			picLOR.Margin = new Padding(7, 6, 7, 6);
			picLOR.Name = "picLOR";
			picLOR.Size = new Size(167, 41);
			picLOR.TabIndex = 137;
			picLOR.TabStop = false;
			// 
			// panel3
			// 
			panel3.Controls.Add(optSeqAppend);
			panel3.Controls.Add(optSeqNew);
			panel3.Location = new Point(41, 47);
			panel3.Margin = new Padding(7, 6, 7, 6);
			panel3.Name = "panel3";
			panel3.Size = new Size(600, 64);
			panel3.TabIndex = 136;
			// 
			// optSeqAppend
			// 
			optSeqAppend.AutoSize = true;
			optSeqAppend.Location = new Point(277, 13);
			optSeqAppend.Margin = new Padding(7, 6, 7, 6);
			optSeqAppend.Name = "optSeqAppend";
			optSeqAppend.Size = new Size(246, 39);
			optSeqAppend.TabIndex = 139;
			optSeqAppend.Text = "Append to Existing";
			optSeqAppend.UseCompatibleTextRendering = true;
			optSeqAppend.UseVisualStyleBackColor = true;
			optSeqAppend.CheckedChanged += optSeqAppend_CheckedChanged;
			// 
			// optSeqNew
			// 
			optSeqNew.AutoSize = true;
			optSeqNew.Checked = true;
			optSeqNew.Location = new Point(7, 13);
			optSeqNew.Margin = new Padding(7, 6, 7, 6);
			optSeqNew.Name = "optSeqNew";
			optSeqNew.Size = new Size(166, 39);
			optSeqNew.TabIndex = 138;
			optSeqNew.TabStop = true;
			optSeqNew.Text = "Create New";
			optSeqNew.UseCompatibleTextRendering = true;
			optSeqNew.UseVisualStyleBackColor = true;
			optSeqNew.CheckedChanged += optSeqNew_CheckedChanged;
			// 
			// button1
			// 
			button1.AllowDrop = true;
			button1.Location = new Point(409, 237);
			button1.Margin = new Padding(7, 6, 7, 6);
			button1.Name = "button1";
			button1.Size = new Size(163, 58);
			button1.TabIndex = 129;
			button1.Text = "Settings...";
			button1.UseVisualStyleBackColor = true;
			button1.Visible = false;
			// 
			// lblStep4B
			// 
			lblStep4B.AutoSize = true;
			lblStep4B.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			lblStep4B.ForeColor = SystemColors.Highlight;
			lblStep4B.Location = new Point(13, -13);
			lblStep4B.Margin = new Padding(7, 0, 7, 0);
			lblStep4B.Name = "lblStep4B";
			lblStep4B.Size = new Size(62, 47);
			lblStep4B.TabIndex = 127;
			lblStep4B.Text = "4b";
			// 
			// lblFileSequence
			// 
			lblFileSequence.AllowDrop = true;
			lblFileSequence.AutoSize = true;
			lblFileSequence.Location = new Point(35, 126);
			lblFileSequence.Margin = new Padding(7, 0, 7, 0);
			lblFileSequence.Name = "lblFileSequence";
			lblFileSequence.Size = new Size(252, 32);
			lblFileSequence.TabIndex = 126;
			lblFileSequence.Text = "Sequence Filename(s):";
			// 
			// btnSaveSeq
			// 
			btnSaveSeq.AllowDrop = true;
			btnSaveSeq.Location = new Point(529, 158);
			btnSaveSeq.Margin = new Padding(7, 6, 7, 6);
			btnSaveSeq.Name = "btnSaveSeq";
			btnSaveSeq.Size = new Size(163, 58);
			btnSaveSeq.TabIndex = 125;
			btnSaveSeq.Text = "Save As...";
			btnSaveSeq.UseVisualStyleBackColor = true;
			btnSaveSeq.Click += btnSaveSeq_Click;
			// 
			// txtSeqName
			// 
			txtSeqName.AllowDrop = true;
			txtSeqName.Location = new Point(41, 164);
			txtSeqName.Margin = new Padding(7, 6, 7, 6);
			txtSeqName.Name = "txtSeqName";
			txtSeqName.ReadOnly = true;
			txtSeqName.Size = new Size(470, 39);
			txtSeqName.TabIndex = 124;
			// 
			// frmVamp
			// 
			AcceptButton = btnOK;
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(2145, 1781);
			Controls.Add(grpExplore);
			Controls.Add(grpSaveLOR);
			Controls.Add(grpAnalyze);
			Controls.Add(grpSavex);
			Controls.Add(grpAudio);
			Controls.Add(staStatus);
			Controls.Add(grpOptions);
			Controls.Add(menuStrip1);
			Controls.Add(btnResetDefaults);
			Controls.Add(grpTimings);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MainMenuStrip = menuStrip1;
			Margin = new Padding(7, 6, 7, 6);
			MaximizeBox = false;
			Name = "frmVamp";
			Text = "Vamperizer";
			FormClosing += frmVamp_FormClosing;
			FormClosed += frmVamp_FormClosed;
			Load += frmVamp_Load;
			DragDrop += Event_DragDrop;
			DragEnter += Event_DragEnter;
			Paint += frmVamp_Paint;
			pnlTrackBeatsX.ResumeLayout(false);
			pnlTrackBeatsX.PerformLayout();
			staStatus.ResumeLayout(false);
			staStatus.PerformLayout();
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			grpTimings.ResumeLayout(false);
			grpTimings.PerformLayout();
			grpChords.ResumeLayout(false);
			grpChords.PerformLayout();
			grbGlobal.ResumeLayout(false);
			grbGlobal.PerformLayout();
			pnlBeatFade.ResumeLayout(false);
			pnlBeatFade.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			grpPlatform.ResumeLayout(false);
			grpPlatform.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			grpVocals.ResumeLayout(false);
			grpVocals.PerformLayout();
			grpSegments.ResumeLayout(false);
			grpSegments.PerformLayout();
			grpTempo.ResumeLayout(false);
			grpTempo.PerformLayout();
			grpPoly2.ResumeLayout(false);
			grpPoly2.PerformLayout();
			grpPitchKey.ResumeLayout(false);
			grpPitchKey.PerformLayout();
			grpChromagram.ResumeLayout(false);
			grpChromagram.PerformLayout();
			grpPolyphonic.ResumeLayout(false);
			grpPolyphonic.PerformLayout();
			grpOnsets.ResumeLayout(false);
			grpOnsets.PerformLayout();
			pnlOnsetSensitivity.ResumeLayout(false);
			pnlOnsetSensitivity.PerformLayout();
			grpBarsBeats.ResumeLayout(false);
			grpBarsBeats.PerformLayout();
			pnlNotes.ResumeLayout(false);
			pnlNotes.PerformLayout();
			pnlVamping.ResumeLayout(false);
			pnlVamping.PerformLayout();
			grpExplore.ResumeLayout(false);
			grpAudio.ResumeLayout(false);
			grpAudio.PerformLayout();
			grpSavex.ResumeLayout(false);
			grpSavex.PerformLayout();
			((System.ComponentModel.ISupportInitialize)picxLights).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			grpAnalyze.ResumeLayout(false);
			grpAnalyze.PerformLayout();
			grpOptions.ResumeLayout(false);
			grpOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)picWorking).EndInit();
			((System.ComponentModel.ISupportInitialize)picVampire).EndInit();
			((System.ComponentModel.ISupportInitialize)picPoisonArrow).EndInit();
			grpSaveLOR.ResumeLayout(false);
			grpSaveLOR.PerformLayout();
			((System.ComponentModel.ISupportInitialize)picLOR).EndInit();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
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
		private System.Windows.Forms.ToolStripMenuItem mnuTimingMarkers;
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
		//private JCS.ToggleSwitch swRamps;
		private System.Windows.Forms.Label lblBarsRampFade;
		private System.Windows.Forms.Label lblBarsOnOff;
		private System.Windows.Forms.CheckBox chkWhiten;
		private System.Windows.Forms.Panel pnlTrackBeatsX;
		private System.Windows.Forms.VScrollBar vscStartBeat;
		private System.Windows.Forms.TextBox txtStartBeat;
		private System.Windows.Forms.Label lblTrackBeatsX;
		private System.Windows.Forms.Panel panel1;
		//private JCS.ToggleSwitch swTrackBeat;
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
		private ComboBox cboFile_Audio;
		private VScrollBar vScrollBar1;
	}
}

