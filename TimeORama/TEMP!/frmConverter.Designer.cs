
namespace Time_O_Rama
{
	partial class frmConverter
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConverter));
			this.grpWhich = new System.Windows.Forms.GroupBox();
			this.optNoSelection = new System.Windows.Forms.RadioButton();
			this.optLORtox = new System.Windows.Forms.RadioButton();
			this.optxToLOR = new System.Windows.Forms.RadioButton();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.grpLOR = new System.Windows.Forms.GroupBox();
			this.grpSeqFile = new System.Windows.Forms.GroupBox();
			this.btnBrowseLOR = new System.Windows.Forms.Button();
			this.txtLORfile = new System.Windows.Forms.TextBox();
			this.optNew = new System.Windows.Forms.RadioButton();
			this.optExisting = new System.Windows.Forms.RadioButton();
			this.grpType = new System.Windows.Forms.GroupBox();
			this.txtLORItemName = new System.Windows.Forms.TextBox();
			this.cboLORItem = new System.Windows.Forms.ComboBox();
			this.lblOverwrite = new System.Windows.Forms.Label();
			this.grpEffectType = new System.Windows.Forms.GroupBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.optRampDown = new System.Windows.Forms.RadioButton();
			this.optOnOff = new System.Windows.Forms.RadioButton();
			this.optRampUp = new System.Windows.Forms.RadioButton();
			this.optTimings = new System.Windows.Forms.RadioButton();
			this.optChannel = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnConvert = new System.Windows.Forms.Button();
			this.grpxLights = new System.Windows.Forms.GroupBox();
			this.picxPreview = new System.Windows.Forms.PictureBox();
			this.lblxTimingName = new System.Windows.Forms.Label();
			this.txtxTimingName = new System.Windows.Forms.TextBox();
			this.cboTiming = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowseX = new System.Windows.Forms.Button();
			this.txtXFile = new System.Windows.Forms.TextBox();
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.picLORPreview = new System.Windows.Forms.PictureBox();
			this.lblLORItemName = new System.Windows.Forms.Label();
			this.grpWhich.SuspendLayout();
			this.grpLOR.SuspendLayout();
			this.grpSeqFile.SuspendLayout();
			this.grpType.SuspendLayout();
			this.grpEffectType.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.grpxLights.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picxPreview)).BeginInit();
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picLORPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// grpWhich
			// 
			this.grpWhich.Controls.Add(this.optNoSelection);
			this.grpWhich.Controls.Add(this.optLORtox);
			this.grpWhich.Controls.Add(this.optxToLOR);
			this.grpWhich.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpWhich.Location = new System.Drawing.Point(12, 12);
			this.grpWhich.Name = "grpWhich";
			this.grpWhich.Size = new System.Drawing.Size(450, 50);
			this.grpWhich.TabIndex = 0;
			this.grpWhich.TabStop = false;
			this.grpWhich.Text = "Which Way?";
			// 
			// optNoSelection
			// 
			this.optNoSelection.AutoSize = true;
			this.optNoSelection.Checked = true;
			this.optNoSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optNoSelection.Location = new System.Drawing.Point(248, 19);
			this.optNoSelection.Name = "optNoSelection";
			this.optNoSelection.Size = new System.Drawing.Size(92, 17);
			this.optNoSelection.TabIndex = 2;
			this.optNoSelection.TabStop = true;
			this.optNoSelection.Text = "(No Selection)";
			this.optNoSelection.UseVisualStyleBackColor = true;
			this.optNoSelection.Visible = false;
			// 
			// optLORtox
			// 
			this.optLORtox.AutoSize = true;
			this.optLORtox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optLORtox.Location = new System.Drawing.Point(122, 19);
			this.optLORtox.Name = "optLORtox";
			this.optLORtox.Size = new System.Drawing.Size(111, 17);
			this.optLORtox.TabIndex = 1;
			this.optLORtox.Text = "LOR S4 to xLights";
			this.optLORtox.UseVisualStyleBackColor = true;
			// 
			// optxToLOR
			// 
			this.optxToLOR.AutoSize = true;
			this.optxToLOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optxToLOR.Location = new System.Drawing.Point(6, 19);
			this.optxToLOR.Name = "optxToLOR";
			this.optxToLOR.Size = new System.Drawing.Size(111, 17);
			this.optxToLOR.TabIndex = 0;
			this.optxToLOR.Text = "xLights to LOR S4";
			this.optxToLOR.UseVisualStyleBackColor = true;
			this.optxToLOR.CheckedChanged += new System.EventHandler(this.optxToLOR_CheckedChanged);
			// 
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
			// 
			// grpLOR
			// 
			this.grpLOR.Controls.Add(this.grpSeqFile);
			this.grpLOR.Controls.Add(this.grpType);
			this.grpLOR.Controls.Add(this.label3);
			this.grpLOR.Controls.Add(this.label2);
			this.grpLOR.Enabled = false;
			this.grpLOR.Location = new System.Drawing.Point(12, 203);
			this.grpLOR.Name = "grpLOR";
			this.grpLOR.Size = new System.Drawing.Size(450, 208);
			this.grpLOR.TabIndex = 7;
			this.grpLOR.TabStop = false;
			this.grpLOR.Text = "LOR S4 Sequence";
			// 
			// grpSeqFile
			// 
			this.grpSeqFile.Controls.Add(this.btnBrowseLOR);
			this.grpSeqFile.Controls.Add(this.txtLORfile);
			this.grpSeqFile.Controls.Add(this.optNew);
			this.grpSeqFile.Controls.Add(this.optExisting);
			this.grpSeqFile.Location = new System.Drawing.Point(6, 19);
			this.grpSeqFile.Name = "grpSeqFile";
			this.grpSeqFile.Size = new System.Drawing.Size(438, 74);
			this.grpSeqFile.TabIndex = 17;
			this.grpSeqFile.TabStop = false;
			this.grpSeqFile.Text = "LOR S4 Sequence File";
			// 
			// btnBrowseLOR
			// 
			this.btnBrowseLOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowseLOR.Location = new System.Drawing.Point(380, 42);
			this.btnBrowseLOR.Name = "btnBrowseLOR";
			this.btnBrowseLOR.Size = new System.Drawing.Size(46, 21);
			this.btnBrowseLOR.TabIndex = 19;
			this.btnBrowseLOR.Text = "...";
			this.btnBrowseLOR.UseVisualStyleBackColor = true;
			this.btnBrowseLOR.Click += new System.EventHandler(this.btnBrowseLOR_Click);
			this.btnBrowseLOR.Leave += new System.EventHandler(this.ClearStatus);
			// 
			// txtLORfile
			// 
			this.txtLORfile.Location = new System.Drawing.Point(6, 43);
			this.txtLORfile.Name = "txtLORfile";
			this.txtLORfile.Size = new System.Drawing.Size(368, 20);
			this.txtLORfile.TabIndex = 18;
			// 
			// optNew
			// 
			this.optNew.AutoSize = true;
			this.optNew.Location = new System.Drawing.Point(116, 19);
			this.optNew.Name = "optNew";
			this.optNew.Size = new System.Drawing.Size(81, 17);
			this.optNew.TabIndex = 17;
			this.optNew.Text = "Create New";
			this.optNew.UseVisualStyleBackColor = true;
			// 
			// optExisting
			// 
			this.optExisting.AutoSize = true;
			this.optExisting.Checked = true;
			this.optExisting.Location = new System.Drawing.Point(6, 19);
			this.optExisting.Name = "optExisting";
			this.optExisting.Size = new System.Drawing.Size(95, 17);
			this.optExisting.TabIndex = 16;
			this.optExisting.TabStop = true;
			this.optExisting.Text = "Add to Existing";
			this.optExisting.UseVisualStyleBackColor = true;
			this.optExisting.CheckedChanged += new System.EventHandler(this.optExisting_CheckedChanged);
			// 
			// grpType
			// 
			this.grpType.Controls.Add(this.lblLORItemName);
			this.grpType.Controls.Add(this.picLORPreview);
			this.grpType.Controls.Add(this.txtLORItemName);
			this.grpType.Controls.Add(this.cboLORItem);
			this.grpType.Controls.Add(this.lblOverwrite);
			this.grpType.Controls.Add(this.grpEffectType);
			this.grpType.Controls.Add(this.optTimings);
			this.grpType.Controls.Add(this.optChannel);
			this.grpType.Location = new System.Drawing.Point(9, 99);
			this.grpType.Name = "grpType";
			this.grpType.Size = new System.Drawing.Size(435, 103);
			this.grpType.TabIndex = 16;
			this.grpType.TabStop = false;
			this.grpType.Text = "Type";
			// 
			// txtLORItemName
			// 
			this.txtLORItemName.Location = new System.Drawing.Point(341, 12);
			this.txtLORItemName.Name = "txtLORItemName";
			this.txtLORItemName.Size = new System.Drawing.Size(77, 20);
			this.txtLORItemName.TabIndex = 118;
			this.txtLORItemName.Visible = false;
			// 
			// cboLORItem
			// 
			this.cboLORItem.FormattingEnabled = true;
			this.cboLORItem.Location = new System.Drawing.Point(6, 35);
			this.cboLORItem.Name = "cboLORItem";
			this.cboLORItem.Size = new System.Drawing.Size(200, 21);
			this.cboLORItem.TabIndex = 117;
			this.cboLORItem.SelectedIndexChanged += new System.EventHandler(this.cboLORItem_SelectedIndexChanged);
			// 
			// lblOverwrite
			// 
			this.lblOverwrite.AutoSize = true;
			this.lblOverwrite.ForeColor = System.Drawing.Color.Red;
			this.lblOverwrite.Location = new System.Drawing.Point(237, 15);
			this.lblOverwrite.Name = "lblOverwrite";
			this.lblOverwrite.Size = new System.Drawing.Size(98, 13);
			this.lblOverwrite.TabIndex = 116;
			this.lblOverwrite.Text = "Overwrite Warning!";
			this.lblOverwrite.Visible = false;
			// 
			// grpEffectType
			// 
			this.grpEffectType.Controls.Add(this.pictureBox3);
			this.grpEffectType.Controls.Add(this.pictureBox2);
			this.grpEffectType.Controls.Add(this.pictureBox1);
			this.grpEffectType.Controls.Add(this.optRampDown);
			this.grpEffectType.Controls.Add(this.optOnOff);
			this.grpEffectType.Controls.Add(this.optRampUp);
			this.grpEffectType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.grpEffectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpEffectType.ForeColor = System.Drawing.SystemColors.Control;
			this.grpEffectType.Location = new System.Drawing.Point(230, 30);
			this.grpEffectType.Name = "grpEffectType";
			this.grpEffectType.Size = new System.Drawing.Size(149, 29);
			this.grpEffectType.TabIndex = 112;
			this.grpEffectType.TabStop = false;
			this.grpEffectType.Visible = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			this.pictureBox3.Location = new System.Drawing.Point(26, 8);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(16, 12);
			this.pictureBox3.TabIndex = 5;
			this.pictureBox3.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(74, 8);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(16, 12);
			this.pictureBox2.TabIndex = 4;
			this.pictureBox2.TabStop = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(122, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 12);
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// optRampDown
			// 
			this.optRampDown.AutoSize = true;
			this.optRampDown.Checked = true;
			this.optRampDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optRampDown.Location = new System.Drawing.Point(10, 8);
			this.optRampDown.Name = "optRampDown";
			this.optRampDown.Size = new System.Drawing.Size(14, 13);
			this.optRampDown.TabIndex = 2;
			this.optRampDown.TabStop = true;
			this.optRampDown.UseVisualStyleBackColor = true;
			// 
			// optOnOff
			// 
			this.optOnOff.AutoSize = true;
			this.optOnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optOnOff.Location = new System.Drawing.Point(58, 8);
			this.optOnOff.Name = "optOnOff";
			this.optOnOff.Size = new System.Drawing.Size(14, 13);
			this.optOnOff.TabIndex = 1;
			this.optOnOff.UseVisualStyleBackColor = true;
			// 
			// optRampUp
			// 
			this.optRampUp.AutoSize = true;
			this.optRampUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optRampUp.Location = new System.Drawing.Point(106, 8);
			this.optRampUp.Name = "optRampUp";
			this.optRampUp.Size = new System.Drawing.Size(14, 13);
			this.optRampUp.TabIndex = 0;
			this.optRampUp.UseVisualStyleBackColor = true;
			// 
			// optTimings
			// 
			this.optTimings.AutoSize = true;
			this.optTimings.Checked = true;
			this.optTimings.Location = new System.Drawing.Point(6, 15);
			this.optTimings.Name = "optTimings";
			this.optTimings.Size = new System.Drawing.Size(61, 17);
			this.optTimings.TabIndex = 17;
			this.optTimings.TabStop = true;
			this.optTimings.Text = "Timings";
			this.optTimings.UseVisualStyleBackColor = true;
			this.optTimings.CheckedChanged += new System.EventHandler(this.optTimings_CheckedChanged);
			// 
			// optChannel
			// 
			this.optChannel.AutoSize = true;
			this.optChannel.Location = new System.Drawing.Point(113, 15);
			this.optChannel.Name = "optChannel";
			this.optChannel.Size = new System.Drawing.Size(64, 17);
			this.optChannel.TabIndex = 16;
			this.optChannel.Text = "Channel";
			this.optChannel.UseVisualStyleBackColor = true;
			this.optChannel.CheckedChanged += new System.EventHandler(this.optChannel_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(597, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Sequence File";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(-86, -42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "label2";
			// 
			// btnConvert
			// 
			this.btnConvert.Enabled = false;
			this.btnConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnConvert.Location = new System.Drawing.Point(179, 417);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.Size = new System.Drawing.Size(96, 30);
			this.btnConvert.TabIndex = 8;
			this.btnConvert.Text = "Convert";
			this.btnConvert.UseVisualStyleBackColor = true;
			this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
			// 
			// grpxLights
			// 
			this.grpxLights.Controls.Add(this.picxPreview);
			this.grpxLights.Controls.Add(this.lblxTimingName);
			this.grpxLights.Controls.Add(this.txtxTimingName);
			this.grpxLights.Controls.Add(this.cboTiming);
			this.grpxLights.Controls.Add(this.label1);
			this.grpxLights.Controls.Add(this.btnBrowseX);
			this.grpxLights.Controls.Add(this.txtXFile);
			this.grpxLights.Enabled = false;
			this.grpxLights.Location = new System.Drawing.Point(12, 68);
			this.grpxLights.Name = "grpxLights";
			this.grpxLights.Size = new System.Drawing.Size(450, 129);
			this.grpxLights.TabIndex = 8;
			this.grpxLights.TabStop = false;
			this.grpxLights.Text = "xLights Timings";
			// 
			// picxPreview
			// 
			this.picxPreview.BackColor = System.Drawing.Color.Tan;
			this.picxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picxPreview.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picxPreview.Location = new System.Drawing.Point(6, 85);
			this.picxPreview.Name = "picxPreview";
			this.picxPreview.Size = new System.Drawing.Size(417, 27);
			this.picxPreview.TabIndex = 114;
			this.picxPreview.TabStop = false;
			this.picxPreview.Visible = false;
			this.picxPreview.Click += new System.EventHandler(this.picxPreview_Click);
			// 
			// lblxTimingName
			// 
			this.lblxTimingName.AutoSize = true;
			this.lblxTimingName.Location = new System.Drawing.Point(231, 61);
			this.lblxTimingName.Name = "lblxTimingName";
			this.lblxTimingName.Size = new System.Drawing.Size(38, 13);
			this.lblxTimingName.TabIndex = 66;
			this.lblxTimingName.Text = "Name:";
			this.lblxTimingName.Visible = false;
			// 
			// txtxTimingName
			// 
			this.txtxTimingName.Location = new System.Drawing.Point(282, 58);
			this.txtxTimingName.Name = "txtxTimingName";
			this.txtxTimingName.Size = new System.Drawing.Size(77, 20);
			this.txtxTimingName.TabIndex = 65;
			this.txtxTimingName.Visible = false;
			// 
			// cboTiming
			// 
			this.cboTiming.FormattingEnabled = true;
			this.cboTiming.Location = new System.Drawing.Point(6, 58);
			this.cboTiming.Name = "cboTiming";
			this.cboTiming.Size = new System.Drawing.Size(200, 21);
			this.cboTiming.TabIndex = 64;
			this.cboTiming.SelectedIndexChanged += new System.EventHandler(this.cboTiming_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "xLights Timing File";
			// 
			// btnBrowseX
			// 
			this.btnBrowseX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowseX.Location = new System.Drawing.Point(380, 32);
			this.btnBrowseX.Name = "btnBrowseX";
			this.btnBrowseX.Size = new System.Drawing.Size(46, 21);
			this.btnBrowseX.TabIndex = 7;
			this.btnBrowseX.Text = "...";
			this.btnBrowseX.UseVisualStyleBackColor = true;
			this.btnBrowseX.Click += new System.EventHandler(this.btnBrowseX_Click);
			this.btnBrowseX.Leave += new System.EventHandler(this.ClearStatus);
			// 
			// txtXFile
			// 
			this.txtXFile.Location = new System.Drawing.Point(6, 32);
			this.txtXFile.Name = "txtXFile";
			this.txtXFile.Size = new System.Drawing.Size(368, 20);
			this.txtXFile.TabIndex = 6;
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 485);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(480, 24);
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
			this.pnlHelp.Click += new System.EventHandler(this.pnlHelp_Click);
			// 
			// pnlStatus
			// 
			this.pnlStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pnlStatus.Name = "pnlStatus";
			this.pnlStatus.Size = new System.Drawing.Size(368, 19);
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
			// picLORPreview
			// 
			this.picLORPreview.BackColor = System.Drawing.Color.Yellow;
			this.picLORPreview.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picLORPreview.Location = new System.Drawing.Point(6, 65);
			this.picLORPreview.Name = "picLORPreview";
			this.picLORPreview.Size = new System.Drawing.Size(417, 27);
			this.picLORPreview.TabIndex = 120;
			this.picLORPreview.TabStop = false;
			this.picLORPreview.Visible = false;
			// 
			// lblLORItemName
			// 
			this.lblLORItemName.AutoSize = true;
			this.lblLORItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLORItemName.Location = new System.Drawing.Point(193, 12);
			this.lblLORItemName.Name = "lblLORItemName";
			this.lblLORItemName.Size = new System.Drawing.Size(38, 13);
			this.lblLORItemName.TabIndex = 121;
			this.lblLORItemName.Text = "Name:";
			this.lblLORItemName.Visible = false;
			// 
			// frmConverter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(480, 509);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.grpxLights);
			this.Controls.Add(this.grpLOR);
			this.Controls.Add(this.grpWhich);
			this.Controls.Add(this.btnConvert);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmConverter";
			this.Text = "Time-O-Rama Timings Converter";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmConverter_FormClosed);
			this.Load += new System.EventHandler(this.frmConverter_Load);
			this.grpWhich.ResumeLayout(false);
			this.grpWhich.PerformLayout();
			this.grpLOR.ResumeLayout(false);
			this.grpLOR.PerformLayout();
			this.grpSeqFile.ResumeLayout(false);
			this.grpSeqFile.PerformLayout();
			this.grpType.ResumeLayout(false);
			this.grpType.PerformLayout();
			this.grpEffectType.ResumeLayout(false);
			this.grpEffectType.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.grpxLights.ResumeLayout(false);
			this.grpxLights.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picxPreview)).EndInit();
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picLORPreview)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox grpWhich;
		private System.Windows.Forms.RadioButton optLORtox;
		private System.Windows.Forms.RadioButton optxToLOR;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.GroupBox grpLOR;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnConvert;
		private System.Windows.Forms.GroupBox grpxLights;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBrowseX;
		private System.Windows.Forms.TextBox txtXFile;
		private System.Windows.Forms.GroupBox grpSeqFile;
		private System.Windows.Forms.RadioButton optNew;
		private System.Windows.Forms.RadioButton optExisting;
		private System.Windows.Forms.GroupBox grpType;
		private System.Windows.Forms.RadioButton optTimings;
		private System.Windows.Forms.RadioButton optChannel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnBrowseLOR;
		private System.Windows.Forms.TextBox txtLORfile;
		private System.Windows.Forms.GroupBox grpEffectType;
		private System.Windows.Forms.RadioButton optRampDown;
		private System.Windows.Forms.RadioButton optOnOff;
		private System.Windows.Forms.RadioButton optRampUp;
		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblOverwrite;
		private System.Windows.Forms.ComboBox cboLORItem;
		private System.Windows.Forms.ComboBox cboTiming;
		private System.Windows.Forms.TextBox txtLORItemName;
		private System.Windows.Forms.Label lblxTimingName;
		private System.Windows.Forms.TextBox txtxTimingName;
		private System.Windows.Forms.RadioButton optNoSelection;
		private System.Windows.Forms.PictureBox picxPreview;
		private System.Windows.Forms.PictureBox picLORPreview;
		private System.Windows.Forms.Label lblLORItemName;
	}
}

