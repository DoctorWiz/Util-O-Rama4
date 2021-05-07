namespace TuneORama
{
	partial class frmSettings
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.tabCategory = new System.Windows.Forms.TabControl();
			this.tabGrids = new System.Windows.Forms.TabPage();
			this.tabTracks = new System.Windows.Forms.TabPage();
			this.tabSubcategory = new System.Windows.Forms.TabControl();
			this.tabPoly = new System.Windows.Forms.TabPage();
			this.tabSpectro = new System.Windows.Forms.TabPage();
			this.tabOnsets = new System.Windows.Forms.TabPage();
			this.tabBeats = new System.Windows.Forms.TabPage();
			this.tabChroma = new System.Windows.Forms.TabPage();
			this.tabSegment = new System.Windows.Forms.TabPage();
			this.tabSave = new System.Windows.Forms.TabPage();
			this.brBrowserMsg = new System.Windows.Forms.WebBrowser();
			this.grpTrackPoly = new System.Windows.Forms.GroupBox();
			this.lblEffect = new System.Windows.Forms.Label();
			this.pnlRamps = new System.Windows.Forms.Panel();
			this.swPolyUseRamps = new JCS.ToggleSwitch();
			this.lblPolyUseRamps = new System.Windows.Forms.Label();
			this.lblPolyUseOnOff = new System.Windows.Forms.Label();
			this.chkPolyOctaveGrouping = new System.Windows.Forms.CheckBox();
			this.grpGridsBeats = new System.Windows.Forms.GroupBox();
			this.lblGridBeatsDiv = new System.Windows.Forms.Label();
			this.lblGridSignature = new System.Windows.Forms.Label();
			this.pnlTiming = new System.Windows.Forms.Panel();
			this.swGridBeat = new JCS.ToggleSwitch();
			this.lblGridBeat34 = new System.Windows.Forms.Label();
			this.lblGridBeat44 = new System.Windows.Forms.Label();
			this.pnlGridBeatsX = new System.Windows.Forms.Panel();
			this.vscGridBeatX = new System.Windows.Forms.VScrollBar();
			this.txtGridBeatX = new System.Windows.Forms.TextBox();
			this.lblGridBeatsX = new System.Windows.Forms.Label();
			this.grpSaveFormat = new System.Windows.Forms.GroupBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.optCRGAlpha = new System.Windows.Forms.RadioButton();
			this.optCRGDisplay = new System.Windows.Forms.RadioButton();
			this.optMixedDisplay = new System.Windows.Forms.RadioButton();
			this.grpTrackBeats = new System.Windows.Forms.GroupBox();
			this.pnlTrackBeatsX = new System.Windows.Forms.Panel();
			this.vscTrackBeatX = new System.Windows.Forms.VScrollBar();
			this.txtTrackBeatX = new System.Windows.Forms.TextBox();
			this.lblTrackBeatsX = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.swTrackBeatsUseRamps = new JCS.ToggleSwitch();
			this.lblTrackBeatsUseRamps = new System.Windows.Forms.Label();
			this.lblTrackBeatsUseOnOff = new System.Windows.Forms.Label();
			this.lblTrackBeatsDiv = new System.Windows.Forms.Label();
			this.lblTrackSignature = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.swTrackBeat = new JCS.ToggleSwitch();
			this.lblTrackBeat34 = new System.Windows.Forms.Label();
			this.lblTrackBeat44 = new System.Windows.Forms.Label();
			this.grpTrackSpectro = new System.Windows.Forms.GroupBox();
			this.chkSpectroOctaveGrouping = new System.Windows.Forms.CheckBox();
			this.grpGridsOnsets = new System.Windows.Forms.GroupBox();
			this.lblGridOffsetsNoOptions = new System.Windows.Forms.Label();
			this.tabCategory.SuspendLayout();
			this.tabTracks.SuspendLayout();
			this.tabSubcategory.SuspendLayout();
			this.grpTrackPoly.SuspendLayout();
			this.pnlRamps.SuspendLayout();
			this.grpGridsBeats.SuspendLayout();
			this.pnlTiming.SuspendLayout();
			this.pnlGridBeatsX.SuspendLayout();
			this.grpSaveFormat.SuspendLayout();
			this.grpTrackBeats.SuspendLayout();
			this.pnlTrackBeatsX.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.grpTrackSpectro.SuspendLayout();
			this.grpGridsOnsets.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(90, 329);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(184, 329);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 14;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// tabCategory
			// 
			this.tabCategory.Controls.Add(this.tabGrids);
			this.tabCategory.Controls.Add(this.tabTracks);
			this.tabCategory.Controls.Add(this.tabSave);
			this.tabCategory.Location = new System.Drawing.Point(3, 12);
			this.tabCategory.Name = "tabCategory";
			this.tabCategory.SelectedIndex = 0;
			this.tabCategory.Size = new System.Drawing.Size(360, 300);
			this.tabCategory.TabIndex = 15;
			this.tabCategory.SelectedIndexChanged += new System.EventHandler(this.tabCategory_SelectedIndexChanged);
			// 
			// tabGrids
			// 
			this.tabGrids.Location = new System.Drawing.Point(4, 22);
			this.tabGrids.Name = "tabGrids";
			this.tabGrids.Padding = new System.Windows.Forms.Padding(3);
			this.tabGrids.Size = new System.Drawing.Size(352, 274);
			this.tabGrids.TabIndex = 0;
			this.tabGrids.Text = "Timing Grids";
			this.tabGrids.UseVisualStyleBackColor = true;
			// 
			// tabTracks
			// 
			this.tabTracks.Controls.Add(this.tabSubcategory);
			this.tabTracks.Location = new System.Drawing.Point(4, 22);
			this.tabTracks.Name = "tabTracks";
			this.tabTracks.Padding = new System.Windows.Forms.Padding(3);
			this.tabTracks.Size = new System.Drawing.Size(352, 274);
			this.tabTracks.TabIndex = 1;
			this.tabTracks.Text = "Tracks & Channels";
			this.tabTracks.UseVisualStyleBackColor = true;
			// 
			// tabSubcategory
			// 
			this.tabSubcategory.Controls.Add(this.tabPoly);
			this.tabSubcategory.Controls.Add(this.tabSpectro);
			this.tabSubcategory.Controls.Add(this.tabOnsets);
			this.tabSubcategory.Controls.Add(this.tabBeats);
			this.tabSubcategory.Controls.Add(this.tabChroma);
			this.tabSubcategory.Controls.Add(this.tabSegment);
			this.tabSubcategory.Location = new System.Drawing.Point(0, 3);
			this.tabSubcategory.Name = "tabSubcategory";
			this.tabSubcategory.SelectedIndex = 0;
			this.tabSubcategory.Size = new System.Drawing.Size(352, 271);
			this.tabSubcategory.TabIndex = 17;
			this.tabSubcategory.SelectedIndexChanged += new System.EventHandler(this.tabSubcategory_SelectedIndexChanged);
			// 
			// tabPoly
			// 
			this.tabPoly.Location = new System.Drawing.Point(4, 22);
			this.tabPoly.Name = "tabPoly";
			this.tabPoly.Padding = new System.Windows.Forms.Padding(3);
			this.tabPoly.Size = new System.Drawing.Size(344, 245);
			this.tabPoly.TabIndex = 0;
			this.tabPoly.Text = "Polyphonic Transcription";
			this.tabPoly.UseVisualStyleBackColor = true;
			// 
			// tabSpectro
			// 
			this.tabSpectro.Location = new System.Drawing.Point(4, 22);
			this.tabSpectro.Name = "tabSpectro";
			this.tabSpectro.Padding = new System.Windows.Forms.Padding(3);
			this.tabSpectro.Size = new System.Drawing.Size(344, 245);
			this.tabSpectro.TabIndex = 1;
			this.tabSpectro.Text = "Spectrogram";
			this.tabSpectro.UseVisualStyleBackColor = true;
			// 
			// tabOnsets
			// 
			this.tabOnsets.Location = new System.Drawing.Point(4, 22);
			this.tabOnsets.Name = "tabOnsets";
			this.tabOnsets.Size = new System.Drawing.Size(344, 245);
			this.tabOnsets.TabIndex = 2;
			this.tabOnsets.Text = "Note Onsets";
			this.tabOnsets.UseVisualStyleBackColor = true;
			// 
			// tabBeats
			// 
			this.tabBeats.Location = new System.Drawing.Point(4, 22);
			this.tabBeats.Name = "tabBeats";
			this.tabBeats.Size = new System.Drawing.Size(344, 245);
			this.tabBeats.TabIndex = 3;
			this.tabBeats.Text = "Beats";
			this.tabBeats.UseVisualStyleBackColor = true;
			// 
			// tabChroma
			// 
			this.tabChroma.Location = new System.Drawing.Point(4, 22);
			this.tabChroma.Name = "tabChroma";
			this.tabChroma.Size = new System.Drawing.Size(344, 245);
			this.tabChroma.TabIndex = 4;
			this.tabChroma.Text = "Chromagram";
			this.tabChroma.UseVisualStyleBackColor = true;
			// 
			// tabSegment
			// 
			this.tabSegment.Location = new System.Drawing.Point(4, 22);
			this.tabSegment.Name = "tabSegment";
			this.tabSegment.Size = new System.Drawing.Size(344, 245);
			this.tabSegment.TabIndex = 5;
			this.tabSegment.Text = "Segmenting";
			this.tabSegment.UseVisualStyleBackColor = true;
			// 
			// tabSave
			// 
			this.tabSave.Location = new System.Drawing.Point(4, 22);
			this.tabSave.Name = "tabSave";
			this.tabSave.Size = new System.Drawing.Size(352, 274);
			this.tabSave.TabIndex = 2;
			this.tabSave.Text = "Save Format";
			this.tabSave.UseVisualStyleBackColor = true;
			// 
			// brBrowserMsg
			// 
			this.brBrowserMsg.Location = new System.Drawing.Point(958, 51);
			this.brBrowserMsg.MinimumSize = new System.Drawing.Size(20, 20);
			this.brBrowserMsg.Name = "brBrowserMsg";
			this.brBrowserMsg.ScrollBarsEnabled = false;
			this.brBrowserMsg.Size = new System.Drawing.Size(450, 250);
			this.brBrowserMsg.TabIndex = 16;
			this.brBrowserMsg.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.brBrowserMsg_Navigating);
			// 
			// grpTrackPoly
			// 
			this.grpTrackPoly.AutoSize = true;
			this.grpTrackPoly.Controls.Add(this.lblEffect);
			this.grpTrackPoly.Controls.Add(this.pnlRamps);
			this.grpTrackPoly.Controls.Add(this.chkPolyOctaveGrouping);
			this.grpTrackPoly.Enabled = false;
			this.grpTrackPoly.Location = new System.Drawing.Point(520, 0);
			this.grpTrackPoly.Name = "grpTrackPoly";
			this.grpTrackPoly.Size = new System.Drawing.Size(340, 240);
			this.grpTrackPoly.TabIndex = 19;
			this.grpTrackPoly.TabStop = false;
			this.grpTrackPoly.Text = " Track && Channels - Polyphonic Transcription";
			this.grpTrackPoly.Visible = false;
			// 
			// lblEffect
			// 
			this.lblEffect.AutoSize = true;
			this.lblEffect.Location = new System.Drawing.Point(13, 51);
			this.lblEffect.Name = "lblEffect";
			this.lblEffect.Size = new System.Drawing.Size(35, 13);
			this.lblEffect.TabIndex = 128;
			this.lblEffect.Text = "Effect";
			// 
			// pnlRamps
			// 
			this.pnlRamps.Controls.Add(this.swPolyUseRamps);
			this.pnlRamps.Controls.Add(this.lblPolyUseRamps);
			this.pnlRamps.Controls.Add(this.lblPolyUseOnOff);
			this.pnlRamps.Location = new System.Drawing.Point(12, 67);
			this.pnlRamps.Name = "pnlRamps";
			this.pnlRamps.Size = new System.Drawing.Size(124, 17);
			this.pnlRamps.TabIndex = 127;
			// 
			// swPolyUseRamps
			// 
			this.swPolyUseRamps.Location = new System.Drawing.Point(42, 0);
			this.swPolyUseRamps.Name = "swPolyUseRamps";
			this.swPolyUseRamps.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.swPolyUseRamps.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.swPolyUseRamps.Size = new System.Drawing.Size(32, 16);
			this.swPolyUseRamps.Style = JCS.ToggleSwitch.ToggleSwitchStyle.OSX;
			this.swPolyUseRamps.TabIndex = 129;
			this.swPolyUseRamps.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.swPolyUseRamps_CheckedChanged);
			this.swPolyUseRamps.Click += new System.EventHandler(this.swPolyUseRamps_Click);
			// 
			// lblPolyUseRamps
			// 
			this.lblPolyUseRamps.AutoSize = true;
			this.lblPolyUseRamps.Location = new System.Drawing.Point(80, 2);
			this.lblPolyUseRamps.Name = "lblPolyUseRamps";
			this.lblPolyUseRamps.Size = new System.Drawing.Size(40, 13);
			this.lblPolyUseRamps.TabIndex = 128;
			this.lblPolyUseRamps.Text = "Ramps";
			this.lblPolyUseRamps.Click += new System.EventHandler(this.lblPolyUseRamps_Click);
			// 
			// lblPolyUseOnOff
			// 
			this.lblPolyUseOnOff.AutoSize = true;
			this.lblPolyUseOnOff.Location = new System.Drawing.Point(1, 2);
			this.lblPolyUseOnOff.Name = "lblPolyUseOnOff";
			this.lblPolyUseOnOff.Size = new System.Drawing.Size(38, 13);
			this.lblPolyUseOnOff.TabIndex = 127;
			this.lblPolyUseOnOff.Text = "On-Off";
			this.lblPolyUseOnOff.Click += new System.EventHandler(this.lblPolyUseOnOff_Click);
			// 
			// chkPolyOctaveGrouping
			// 
			this.chkPolyOctaveGrouping.AutoSize = true;
			this.chkPolyOctaveGrouping.Checked = true;
			this.chkPolyOctaveGrouping.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPolyOctaveGrouping.Location = new System.Drawing.Point(12, 31);
			this.chkPolyOctaveGrouping.Name = "chkPolyOctaveGrouping";
			this.chkPolyOctaveGrouping.Size = new System.Drawing.Size(108, 17);
			this.chkPolyOctaveGrouping.TabIndex = 123;
			this.chkPolyOctaveGrouping.Text = "Group By Octave";
			this.chkPolyOctaveGrouping.UseVisualStyleBackColor = true;
			this.chkPolyOctaveGrouping.CheckedChanged += new System.EventHandler(this.chkPolyOctaveGrouping_CheckedChanged);
			// 
			// grpGridsBeats
			// 
			this.grpGridsBeats.Controls.Add(this.lblGridBeatsDiv);
			this.grpGridsBeats.Controls.Add(this.lblGridSignature);
			this.grpGridsBeats.Controls.Add(this.pnlTiming);
			this.grpGridsBeats.Controls.Add(this.pnlGridBeatsX);
			this.grpGridsBeats.Enabled = false;
			this.grpGridsBeats.Location = new System.Drawing.Point(280, 0);
			this.grpGridsBeats.Name = "grpGridsBeats";
			this.grpGridsBeats.Size = new System.Drawing.Size(342, 239);
			this.grpGridsBeats.TabIndex = 20;
			this.grpGridsBeats.TabStop = false;
			this.grpGridsBeats.Text = " Timing Grid - Beats ";
			this.grpGridsBeats.Visible = false;
			// 
			// lblGridBeatsDiv
			// 
			this.lblGridBeatsDiv.AutoSize = true;
			this.lblGridBeatsDiv.Location = new System.Drawing.Point(7, 67);
			this.lblGridBeatsDiv.Name = "lblGridBeatsDiv";
			this.lblGridBeatsDiv.Size = new System.Drawing.Size(111, 13);
			this.lblGridBeatsDiv.TabIndex = 131;
			this.lblGridBeatsDiv.Text = "Timing marks per beat";
			// 
			// lblGridSignature
			// 
			this.lblGridSignature.AutoSize = true;
			this.lblGridSignature.Location = new System.Drawing.Point(7, 22);
			this.lblGridSignature.Name = "lblGridSignature";
			this.lblGridSignature.Size = new System.Drawing.Size(78, 13);
			this.lblGridSignature.TabIndex = 130;
			this.lblGridSignature.Text = "Time Signature";
			// 
			// pnlTiming
			// 
			this.pnlTiming.Controls.Add(this.swGridBeat);
			this.pnlTiming.Controls.Add(this.lblGridBeat34);
			this.pnlTiming.Controls.Add(this.lblGridBeat44);
			this.pnlTiming.Location = new System.Drawing.Point(7, 40);
			this.pnlTiming.Name = "pnlTiming";
			this.pnlTiming.Size = new System.Drawing.Size(127, 17);
			this.pnlTiming.TabIndex = 129;
			// 
			// swGridBeat
			// 
			this.swGridBeat.Location = new System.Drawing.Point(46, 1);
			this.swGridBeat.Name = "swGridBeat";
			this.swGridBeat.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.swGridBeat.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.swGridBeat.Size = new System.Drawing.Size(32, 16);
			this.swGridBeat.Style = JCS.ToggleSwitch.ToggleSwitchStyle.OSX;
			this.swGridBeat.TabIndex = 132;
			this.swGridBeat.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.swGridBeat_CheckedChanged);
			this.swGridBeat.Click += new System.EventHandler(this.swGridBeat_Click);
			// 
			// lblGridBeat34
			// 
			this.lblGridBeat34.AutoSize = true;
			this.lblGridBeat34.Location = new System.Drawing.Point(80, 2);
			this.lblGridBeat34.Name = "lblGridBeat34";
			this.lblGridBeat34.Size = new System.Drawing.Size(46, 13);
			this.lblGridBeat34.TabIndex = 128;
			this.lblGridBeat34.Text = "3/4 time";
			this.lblGridBeat34.Click += new System.EventHandler(this.lblTime34_Click);
			// 
			// lblGridBeat44
			// 
			this.lblGridBeat44.AutoSize = true;
			this.lblGridBeat44.Location = new System.Drawing.Point(1, 2);
			this.lblGridBeat44.Name = "lblGridBeat44";
			this.lblGridBeat44.Size = new System.Drawing.Size(46, 13);
			this.lblGridBeat44.TabIndex = 127;
			this.lblGridBeat44.Text = "4/4 time";
			this.lblGridBeat44.Click += new System.EventHandler(this.lblTime44_Click);
			// 
			// pnlGridBeatsX
			// 
			this.pnlGridBeatsX.Controls.Add(this.vscGridBeatX);
			this.pnlGridBeatsX.Controls.Add(this.txtGridBeatX);
			this.pnlGridBeatsX.Controls.Add(this.lblGridBeatsX);
			this.pnlGridBeatsX.Location = new System.Drawing.Point(7, 83);
			this.pnlGridBeatsX.Name = "pnlGridBeatsX";
			this.pnlGridBeatsX.Size = new System.Drawing.Size(78, 23);
			this.pnlGridBeatsX.TabIndex = 6;
			// 
			// vscGridBeatX
			// 
			this.vscGridBeatX.LargeChange = 1;
			this.vscGridBeatX.Location = new System.Drawing.Point(58, 0);
			this.vscGridBeatX.Maximum = 3;
			this.vscGridBeatX.Name = "vscGridBeatX";
			this.vscGridBeatX.Size = new System.Drawing.Size(16, 18);
			this.vscGridBeatX.TabIndex = 28;
			this.vscGridBeatX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vscGridBeatX_Scroll);
			// 
			// txtGridBeatX
			// 
			this.txtGridBeatX.Location = new System.Drawing.Point(45, 1);
			this.txtGridBeatX.Name = "txtGridBeatX";
			this.txtGridBeatX.ReadOnly = true;
			this.txtGridBeatX.Size = new System.Drawing.Size(13, 20);
			this.txtGridBeatX.TabIndex = 7;
			this.txtGridBeatX.Text = "4";
			// 
			// lblGridBeatsX
			// 
			this.lblGridBeatsX.AutoSize = true;
			this.lblGridBeatsX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGridBeatsX.Location = new System.Drawing.Point(1, 4);
			this.lblGridBeatsX.Name = "lblGridBeatsX";
			this.lblGridBeatsX.Size = new System.Drawing.Size(44, 13);
			this.lblGridBeatsX.TabIndex = 6;
			this.lblGridBeatsX.Text = "Beats X";
			this.lblGridBeatsX.UseMnemonic = false;
			// 
			// grpSaveFormat
			// 
			this.grpSaveFormat.Controls.Add(this.label14);
			this.grpSaveFormat.Controls.Add(this.label13);
			this.grpSaveFormat.Controls.Add(this.optCRGAlpha);
			this.grpSaveFormat.Controls.Add(this.optCRGDisplay);
			this.grpSaveFormat.Controls.Add(this.optMixedDisplay);
			this.grpSaveFormat.Enabled = false;
			this.grpSaveFormat.Location = new System.Drawing.Point(760, 0);
			this.grpSaveFormat.Name = "grpSaveFormat";
			this.grpSaveFormat.Size = new System.Drawing.Size(340, 260);
			this.grpSaveFormat.TabIndex = 21;
			this.grpSaveFormat.TabStop = false;
			this.grpSaveFormat.Text = " Save Format ";
			this.grpSaveFormat.Visible = false;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(12, 20);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(222, 37);
			this.label14.TabIndex = 132;
			this.label14.Text = "Channels, Groups, Tracks, etc. will be Written to the Sequence file in the follow" +
    "ing order:";
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label13.Location = new System.Drawing.Point(27, 192);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(190, 42);
			this.label13.TabIndex = 26;
			this.label13.Text = "(This setting matters only if you view or edit the sequence files in a text edito" +
    "r, Excel, or some other database tool.)";
			this.label13.UseMnemonic = false;
			this.label13.Click += new System.EventHandler(this.label13_Click);
			// 
			// optCRGAlpha
			// 
			this.optCRGAlpha.Location = new System.Drawing.Point(15, 134);
			this.optCRGAlpha.Name = "optCRGAlpha";
			this.optCRGAlpha.Size = new System.Drawing.Size(219, 43);
			this.optCRGAlpha.TabIndex = 13;
			this.optCRGAlpha.Text = "Channels first, then RGB Channels, then Channel Groups in Alphabetical Order";
			this.optCRGAlpha.UseVisualStyleBackColor = true;
			// 
			// optCRGDisplay
			// 
			this.optCRGDisplay.Location = new System.Drawing.Point(15, 89);
			this.optCRGDisplay.Name = "optCRGDisplay";
			this.optCRGDisplay.Size = new System.Drawing.Size(219, 43);
			this.optCRGDisplay.TabIndex = 12;
			this.optCRGDisplay.Text = "Channels first, then RGB Channels, then Channel Groups in Display Order";
			this.optCRGDisplay.UseVisualStyleBackColor = true;
			// 
			// optMixedDisplay
			// 
			this.optMixedDisplay.Checked = true;
			this.optMixedDisplay.Location = new System.Drawing.Point(15, 51);
			this.optMixedDisplay.Name = "optMixedDisplay";
			this.optMixedDisplay.Size = new System.Drawing.Size(219, 40);
			this.optMixedDisplay.TabIndex = 11;
			this.optMixedDisplay.TabStop = true;
			this.optMixedDisplay.Text = "Channels, RGB Channels, and Channel Groups Mixed, but in Display Order";
			this.optMixedDisplay.UseVisualStyleBackColor = true;
			// 
			// grpTrackBeats
			// 
			this.grpTrackBeats.Controls.Add(this.pnlTrackBeatsX);
			this.grpTrackBeats.Controls.Add(this.label11);
			this.grpTrackBeats.Controls.Add(this.panel3);
			this.grpTrackBeats.Controls.Add(this.lblTrackBeatsDiv);
			this.grpTrackBeats.Controls.Add(this.lblTrackSignature);
			this.grpTrackBeats.Controls.Add(this.panel1);
			this.grpTrackBeats.Enabled = false;
			this.grpTrackBeats.Location = new System.Drawing.Point(280, 260);
			this.grpTrackBeats.Name = "grpTrackBeats";
			this.grpTrackBeats.Size = new System.Drawing.Size(340, 260);
			this.grpTrackBeats.TabIndex = 23;
			this.grpTrackBeats.TabStop = false;
			this.grpTrackBeats.Text = " Track && Channels - Beats ";
			this.grpTrackBeats.Visible = false;
			// 
			// pnlTrackBeatsX
			// 
			this.pnlTrackBeatsX.Controls.Add(this.vscTrackBeatX);
			this.pnlTrackBeatsX.Controls.Add(this.txtTrackBeatX);
			this.pnlTrackBeatsX.Controls.Add(this.lblTrackBeatsX);
			this.pnlTrackBeatsX.Location = new System.Drawing.Point(11, 83);
			this.pnlTrackBeatsX.Name = "pnlTrackBeatsX";
			this.pnlTrackBeatsX.Size = new System.Drawing.Size(78, 23);
			this.pnlTrackBeatsX.TabIndex = 134;
			// 
			// vscTrackBeatX
			// 
			this.vscTrackBeatX.LargeChange = 1;
			this.vscTrackBeatX.Location = new System.Drawing.Point(58, 0);
			this.vscTrackBeatX.Maximum = 3;
			this.vscTrackBeatX.Name = "vscTrackBeatX";
			this.vscTrackBeatX.Size = new System.Drawing.Size(16, 18);
			this.vscTrackBeatX.TabIndex = 28;
			this.vscTrackBeatX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vscTrackBeatX_Scroll);
			// 
			// txtTrackBeatX
			// 
			this.txtTrackBeatX.Location = new System.Drawing.Point(45, 1);
			this.txtTrackBeatX.Name = "txtTrackBeatX";
			this.txtTrackBeatX.ReadOnly = true;
			this.txtTrackBeatX.Size = new System.Drawing.Size(13, 20);
			this.txtTrackBeatX.TabIndex = 7;
			this.txtTrackBeatX.Text = "4";
			// 
			// lblTrackBeatsX
			// 
			this.lblTrackBeatsX.AutoSize = true;
			this.lblTrackBeatsX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTrackBeatsX.Location = new System.Drawing.Point(1, 4);
			this.lblTrackBeatsX.Name = "lblTrackBeatsX";
			this.lblTrackBeatsX.Size = new System.Drawing.Size(44, 13);
			this.lblTrackBeatsX.TabIndex = 6;
			this.lblTrackBeatsX.Text = "Beats X";
			this.lblTrackBeatsX.UseMnemonic = false;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(12, 111);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(35, 13);
			this.label11.TabIndex = 133;
			this.label11.Text = "Effect";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.swTrackBeatsUseRamps);
			this.panel3.Controls.Add(this.lblTrackBeatsUseRamps);
			this.panel3.Controls.Add(this.lblTrackBeatsUseOnOff);
			this.panel3.Location = new System.Drawing.Point(11, 127);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(124, 17);
			this.panel3.TabIndex = 132;
			// 
			// swTrackBeatsUseRamps
			// 
			this.swTrackBeatsUseRamps.Location = new System.Drawing.Point(42, 0);
			this.swTrackBeatsUseRamps.Name = "swTrackBeatsUseRamps";
			this.swTrackBeatsUseRamps.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.swTrackBeatsUseRamps.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.swTrackBeatsUseRamps.Size = new System.Drawing.Size(32, 16);
			this.swTrackBeatsUseRamps.Style = JCS.ToggleSwitch.ToggleSwitchStyle.OSX;
			this.swTrackBeatsUseRamps.TabIndex = 129;
			this.swTrackBeatsUseRamps.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.swTrackBeatsUseRamps_CheckedChanged);
			this.swTrackBeatsUseRamps.Click += new System.EventHandler(this.swTrackBeatsUseRamps_Click);
			// 
			// lblTrackBeatsUseRamps
			// 
			this.lblTrackBeatsUseRamps.AutoSize = true;
			this.lblTrackBeatsUseRamps.Location = new System.Drawing.Point(80, 2);
			this.lblTrackBeatsUseRamps.Name = "lblTrackBeatsUseRamps";
			this.lblTrackBeatsUseRamps.Size = new System.Drawing.Size(40, 13);
			this.lblTrackBeatsUseRamps.TabIndex = 128;
			this.lblTrackBeatsUseRamps.Text = "Ramps";
			this.lblTrackBeatsUseRamps.Click += new System.EventHandler(this.lblTrackBeatsUseRamps_Click);
			// 
			// lblTrackBeatsUseOnOff
			// 
			this.lblTrackBeatsUseOnOff.AutoSize = true;
			this.lblTrackBeatsUseOnOff.Location = new System.Drawing.Point(1, 2);
			this.lblTrackBeatsUseOnOff.Name = "lblTrackBeatsUseOnOff";
			this.lblTrackBeatsUseOnOff.Size = new System.Drawing.Size(38, 13);
			this.lblTrackBeatsUseOnOff.TabIndex = 127;
			this.lblTrackBeatsUseOnOff.Text = "On-Off";
			this.lblTrackBeatsUseOnOff.Click += new System.EventHandler(this.lblTrackBeatsUseOnOff_Click);
			// 
			// lblTrackBeatsDiv
			// 
			this.lblTrackBeatsDiv.AutoSize = true;
			this.lblTrackBeatsDiv.Location = new System.Drawing.Point(7, 67);
			this.lblTrackBeatsDiv.Name = "lblTrackBeatsDiv";
			this.lblTrackBeatsDiv.Size = new System.Drawing.Size(111, 13);
			this.lblTrackBeatsDiv.TabIndex = 131;
			this.lblTrackBeatsDiv.Text = "Timing marks per beat";
			// 
			// lblTrackSignature
			// 
			this.lblTrackSignature.AutoSize = true;
			this.lblTrackSignature.Location = new System.Drawing.Point(7, 22);
			this.lblTrackSignature.Name = "lblTrackSignature";
			this.lblTrackSignature.Size = new System.Drawing.Size(78, 13);
			this.lblTrackSignature.TabIndex = 130;
			this.lblTrackSignature.Text = "Time Signature";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.swTrackBeat);
			this.panel1.Controls.Add(this.lblTrackBeat34);
			this.panel1.Controls.Add(this.lblTrackBeat44);
			this.panel1.Location = new System.Drawing.Point(7, 40);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(127, 17);
			this.panel1.TabIndex = 129;
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
			this.swTrackBeat.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.swTrackBeat_CheckedChanged);
			this.swTrackBeat.Click += new System.EventHandler(this.swTrackBeat_Click);
			// 
			// lblTrackBeat34
			// 
			this.lblTrackBeat34.AutoSize = true;
			this.lblTrackBeat34.Location = new System.Drawing.Point(80, 2);
			this.lblTrackBeat34.Name = "lblTrackBeat34";
			this.lblTrackBeat34.Size = new System.Drawing.Size(46, 13);
			this.lblTrackBeat34.TabIndex = 128;
			this.lblTrackBeat34.Text = "3/4 time";
			this.lblTrackBeat34.Click += new System.EventHandler(this.lblTrackBeat34_Click);
			// 
			// lblTrackBeat44
			// 
			this.lblTrackBeat44.AutoSize = true;
			this.lblTrackBeat44.Location = new System.Drawing.Point(1, 2);
			this.lblTrackBeat44.Name = "lblTrackBeat44";
			this.lblTrackBeat44.Size = new System.Drawing.Size(46, 13);
			this.lblTrackBeat44.TabIndex = 127;
			this.lblTrackBeat44.Text = "4/4 time";
			this.lblTrackBeat44.Click += new System.EventHandler(this.lblTrackBeat44_Click);
			// 
			// grpTrackSpectro
			// 
			this.grpTrackSpectro.AutoSize = true;
			this.grpTrackSpectro.Controls.Add(this.chkSpectroOctaveGrouping);
			this.grpTrackSpectro.Enabled = false;
			this.grpTrackSpectro.Location = new System.Drawing.Point(520, 260);
			this.grpTrackSpectro.Name = "grpTrackSpectro";
			this.grpTrackSpectro.Size = new System.Drawing.Size(340, 260);
			this.grpTrackSpectro.TabIndex = 25;
			this.grpTrackSpectro.TabStop = false;
			this.grpTrackSpectro.Text = " Track && Channels- Spectrogram ";
			this.grpTrackSpectro.Visible = false;
			// 
			// chkSpectroOctaveGrouping
			// 
			this.chkSpectroOctaveGrouping.AutoSize = true;
			this.chkSpectroOctaveGrouping.Checked = true;
			this.chkSpectroOctaveGrouping.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSpectroOctaveGrouping.Location = new System.Drawing.Point(16, 31);
			this.chkSpectroOctaveGrouping.Name = "chkSpectroOctaveGrouping";
			this.chkSpectroOctaveGrouping.Size = new System.Drawing.Size(108, 17);
			this.chkSpectroOctaveGrouping.TabIndex = 124;
			this.chkSpectroOctaveGrouping.Text = "Group By Octave";
			this.chkSpectroOctaveGrouping.UseVisualStyleBackColor = true;
			this.chkSpectroOctaveGrouping.CheckedChanged += new System.EventHandler(this.chkSpectroOctaveGrouping_CheckedChanged);
			// 
			// grpGridsOnsets
			// 
			this.grpGridsOnsets.AutoSize = true;
			this.grpGridsOnsets.Controls.Add(this.lblGridOffsetsNoOptions);
			this.grpGridsOnsets.Enabled = false;
			this.grpGridsOnsets.Location = new System.Drawing.Point(760, 260);
			this.grpGridsOnsets.Name = "grpGridsOnsets";
			this.grpGridsOnsets.Size = new System.Drawing.Size(340, 260);
			this.grpGridsOnsets.TabIndex = 26;
			this.grpGridsOnsets.TabStop = false;
			this.grpGridsOnsets.Text = " Timing Grid - Note Onsets ";
			this.grpGridsOnsets.Visible = false;
			// 
			// lblGridOffsetsNoOptions
			// 
			this.lblGridOffsetsNoOptions.AutoSize = true;
			this.lblGridOffsetsNoOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGridOffsetsNoOptions.Location = new System.Drawing.Point(51, 83);
			this.lblGridOffsetsNoOptions.Name = "lblGridOffsetsNoOptions";
			this.lblGridOffsetsNoOptions.Size = new System.Drawing.Size(132, 13);
			this.lblGridOffsetsNoOptions.TabIndex = 25;
			this.lblGridOffsetsNoOptions.Text = "(No options - at this time...)";
			this.lblGridOffsetsNoOptions.UseMnemonic = false;
			// 
			// frmSettings
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(1182, 539);
			this.Controls.Add(this.grpGridsOnsets);
			this.Controls.Add(this.grpTrackSpectro);
			this.Controls.Add(this.grpTrackBeats);
			this.Controls.Add(this.grpSaveFormat);
			this.Controls.Add(this.grpGridsBeats);
			this.Controls.Add(this.grpTrackPoly);
			this.Controls.Add(this.brBrowserMsg);
			this.Controls.Add(this.tabCategory);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(800, 200);
			this.MaximizeBox = false;
			this.Name = "frmSettings";
			this.ShowInTaskbar = false;
			this.Text = "Settings";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
			this.Load += new System.EventHandler(this.frmSettings_Load);
			this.Shown += new System.EventHandler(this.frmSettings_Shown);
			this.LocationChanged += new System.EventHandler(this.frmSettings_LocationChanged);
			this.tabCategory.ResumeLayout(false);
			this.tabTracks.ResumeLayout(false);
			this.tabSubcategory.ResumeLayout(false);
			this.grpTrackPoly.ResumeLayout(false);
			this.grpTrackPoly.PerformLayout();
			this.pnlRamps.ResumeLayout(false);
			this.pnlRamps.PerformLayout();
			this.grpGridsBeats.ResumeLayout(false);
			this.grpGridsBeats.PerformLayout();
			this.pnlTiming.ResumeLayout(false);
			this.pnlTiming.PerformLayout();
			this.pnlGridBeatsX.ResumeLayout(false);
			this.pnlGridBeatsX.PerformLayout();
			this.grpSaveFormat.ResumeLayout(false);
			this.grpTrackBeats.ResumeLayout(false);
			this.grpTrackBeats.PerformLayout();
			this.pnlTrackBeatsX.ResumeLayout(false);
			this.pnlTrackBeatsX.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.grpTrackSpectro.ResumeLayout(false);
			this.grpTrackSpectro.PerformLayout();
			this.grpGridsOnsets.ResumeLayout(false);
			this.grpGridsOnsets.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.TabControl tabCategory;
		private System.Windows.Forms.TabPage tabGrids;
		private System.Windows.Forms.TabPage tabTracks;
		private System.Windows.Forms.TabPage tabSave;
		private System.Windows.Forms.TabControl tabSubcategory;
		private System.Windows.Forms.TabPage tabPoly;
		private System.Windows.Forms.TabPage tabSpectro;
		private System.Windows.Forms.WebBrowser brBrowserMsg;
		private System.Windows.Forms.TabPage tabOnsets;
		private System.Windows.Forms.TabPage tabBeats;
		private System.Windows.Forms.GroupBox grpTrackPoly;
		private System.Windows.Forms.Panel pnlRamps;
		private System.Windows.Forms.Label lblPolyUseRamps;
		private System.Windows.Forms.Label lblPolyUseOnOff;
		private System.Windows.Forms.CheckBox chkPolyOctaveGrouping;
		private System.Windows.Forms.GroupBox grpGridsBeats;
		private System.Windows.Forms.Label lblGridBeatsDiv;
		private System.Windows.Forms.Label lblGridSignature;
		private System.Windows.Forms.Panel pnlTiming;
		private System.Windows.Forms.Label lblGridBeat34;
		private System.Windows.Forms.Label lblGridBeat44;
		private System.Windows.Forms.Panel pnlGridBeatsX;
		private System.Windows.Forms.TextBox txtGridBeatX;
		private System.Windows.Forms.Label lblGridBeatsX;
		private System.Windows.Forms.GroupBox grpSaveFormat;
		private System.Windows.Forms.RadioButton optCRGAlpha;
		private System.Windows.Forms.RadioButton optCRGDisplay;
		private System.Windows.Forms.RadioButton optMixedDisplay;
		private System.Windows.Forms.Label lblEffect;
		private JCS.ToggleSwitch swPolyUseRamps;
		private JCS.ToggleSwitch swGridBeat;
		private System.Windows.Forms.GroupBox grpTrackBeats;
		private System.Windows.Forms.Label lblTrackBeatsDiv;
		private System.Windows.Forms.Label lblTrackSignature;
		private System.Windows.Forms.Panel panel1;
		private JCS.ToggleSwitch swTrackBeat;
		private System.Windows.Forms.Label lblTrackBeat34;
		private System.Windows.Forms.Label lblTrackBeat44;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Panel panel3;
		private JCS.ToggleSwitch swTrackBeatsUseRamps;
		private System.Windows.Forms.Label lblTrackBeatsUseRamps;
		private System.Windows.Forms.Label lblTrackBeatsUseOnOff;
		private System.Windows.Forms.GroupBox grpTrackSpectro;
		private System.Windows.Forms.GroupBox grpGridsOnsets;
		private System.Windows.Forms.Label lblGridOffsetsNoOptions;
		private System.Windows.Forms.VScrollBar vscGridBeatX;
		private System.Windows.Forms.Panel pnlTrackBeatsX;
		private System.Windows.Forms.VScrollBar vscTrackBeatX;
		private System.Windows.Forms.TextBox txtTrackBeatX;
		private System.Windows.Forms.Label lblTrackBeatsX;
		private System.Windows.Forms.CheckBox chkSpectroOctaveGrouping;
		private System.Windows.Forms.TabPage tabChroma;
		private System.Windows.Forms.TabPage tabSegment;
	}
}