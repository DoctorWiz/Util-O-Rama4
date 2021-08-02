namespace Lor_o_Tune
{
    partial class frmLorOTune
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLorOTune));
			this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgSave = new System.Windows.Forms.SaveFileDialog();
			this.btnOK = new System.Windows.Forms.Button();
			this.grpFiles = new System.Windows.Forms.GroupBox();
			this.btnSavePresets = new System.Windows.Forms.Button();
			this.txtFreqPresetFile = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPresetFile = new System.Windows.Forms.TextBox();
			this.cmdBrowseFreqPreset = new System.Windows.Forms.Button();
			this.txtChannelFile = new System.Windows.Forms.TextBox();
			this.cmdBrowseChannelConfig = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtOutputFile = new System.Windows.Forms.TextBox();
			this.btnBrowseOutput = new System.Windows.Forms.Button();
			this.txtMusicFile = new System.Windows.Forms.TextBox();
			this.btnBrowseMusic = new System.Windows.Forms.Button();
			this.grpChannels = new System.Windows.Forms.GroupBox();
			this.pnlChannels = new System.Windows.Forms.Panel();
			this.grpChannel = new System.Windows.Forms.GroupBox();
			this.picColor = new System.Windows.Forms.PictureBox();
			this.lblDecibels = new System.Windows.Forms.Label();
			this.lblFreq = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.lblLow = new System.Windows.Forms.Label();
			this.numLow = new System.Windows.Forms.NumericUpDown();
			this.numHigh = new System.Windows.Forms.NumericUpDown();
			this.numChannel = new System.Windows.Forms.NumericUpDown();
			this.numUnit = new System.Windows.Forms.NumericUpDown();
			this.lblChannel = new System.Windows.Forms.Label();
			this.lblStart = new System.Windows.Forms.Label();
			this.cboDevice = new System.Windows.Forms.ComboBox();
			this.lblDevice = new System.Windows.Forms.Label();
			this.lblPreset = new System.Windows.Forms.Label();
			this.cboPreset = new System.Windows.Forms.ComboBox();
			this.lblEnd = new System.Windows.Forms.Label();
			this.lblUnit = new System.Windows.Forms.Label();
			this.numStart = new System.Windows.Forms.NumericUpDown();
			this.numEnd = new System.Windows.Forms.NumericUpDown();
			this.lblHigh = new System.Windows.Forms.Label();
			this.grpFiles.SuspendLayout();
			this.grpChannels.SuspendLayout();
			this.pnlChannels.SuspendLayout();
			this.grpChannel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picColor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numLow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numHigh)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numChannel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUnit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numEnd)).BeginInit();
			this.SuspendLayout();
			// 
			// dlgOpen
			// 
			this.dlgOpen.FileName = "openFileDialog1";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(352, 723);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(76, 30);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "Do It!";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// grpFiles
			// 
			this.grpFiles.Controls.Add(this.btnSavePresets);
			this.grpFiles.Controls.Add(this.txtFreqPresetFile);
			this.grpFiles.Controls.Add(this.label4);
			this.grpFiles.Controls.Add(this.txtPresetFile);
			this.grpFiles.Controls.Add(this.cmdBrowseFreqPreset);
			this.grpFiles.Controls.Add(this.txtChannelFile);
			this.grpFiles.Controls.Add(this.cmdBrowseChannelConfig);
			this.grpFiles.Controls.Add(this.label2);
			this.grpFiles.Controls.Add(this.label1);
			this.grpFiles.Controls.Add(this.txtOutputFile);
			this.grpFiles.Controls.Add(this.btnBrowseOutput);
			this.grpFiles.Controls.Add(this.txtMusicFile);
			this.grpFiles.Controls.Add(this.btnBrowseMusic);
			this.grpFiles.Location = new System.Drawing.Point(12, 12);
			this.grpFiles.Name = "grpFiles";
			this.grpFiles.Size = new System.Drawing.Size(865, 111);
			this.grpFiles.TabIndex = 5;
			this.grpFiles.TabStop = false;
			this.grpFiles.Text = " Files: ";
			// 
			// btnSavePresets
			// 
			this.btnSavePresets.Enabled = false;
			this.btnSavePresets.Location = new System.Drawing.Point(589, 74);
			this.btnSavePresets.Name = "btnSavePresets";
			this.btnSavePresets.Size = new System.Drawing.Size(40, 21);
			this.btnSavePresets.TabIndex = 16;
			this.btnSavePresets.Text = "Save";
			this.btnSavePresets.UseVisualStyleBackColor = true;
			// 
			// txtFreqPresetFile
			// 
			this.txtFreqPresetFile.AutoSize = true;
			this.txtFreqPresetFile.Location = new System.Drawing.Point(303, 57);
			this.txtFreqPresetFile.Name = "txtFreqPresetFile";
			this.txtFreqPresetFile.Size = new System.Drawing.Size(90, 13);
			this.txtFreqPresetFile.TabIndex = 15;
			this.txtFreqPresetFile.Text = "Frequency Preset";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 57);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "Channel Configuration";
			// 
			// txtPresetFile
			// 
			this.txtPresetFile.Location = new System.Drawing.Point(306, 74);
			this.txtPresetFile.Name = "txtPresetFile";
			this.txtPresetFile.Size = new System.Drawing.Size(244, 20);
			this.txtPresetFile.TabIndex = 13;
			// 
			// cmdBrowseFreqPreset
			// 
			this.cmdBrowseFreqPreset.Location = new System.Drawing.Point(556, 74);
			this.cmdBrowseFreqPreset.Name = "cmdBrowseFreqPreset";
			this.cmdBrowseFreqPreset.Size = new System.Drawing.Size(27, 21);
			this.cmdBrowseFreqPreset.TabIndex = 12;
			this.cmdBrowseFreqPreset.Text = "...";
			this.cmdBrowseFreqPreset.UseVisualStyleBackColor = true;
			this.cmdBrowseFreqPreset.Click += new System.EventHandler(this.cmdBrowseFreqPreset_Click);
			// 
			// txtChannelFile
			// 
			this.txtChannelFile.Location = new System.Drawing.Point(6, 73);
			this.txtChannelFile.Name = "txtChannelFile";
			this.txtChannelFile.Size = new System.Drawing.Size(219, 20);
			this.txtChannelFile.TabIndex = 11;
			// 
			// cmdBrowseChannelConfig
			// 
			this.cmdBrowseChannelConfig.Location = new System.Drawing.Point(231, 73);
			this.cmdBrowseChannelConfig.Name = "cmdBrowseChannelConfig";
			this.cmdBrowseChannelConfig.Size = new System.Drawing.Size(27, 21);
			this.cmdBrowseChannelConfig.TabIndex = 10;
			this.cmdBrowseChannelConfig.Text = "...";
			this.cmdBrowseChannelConfig.UseVisualStyleBackColor = true;
			this.cmdBrowseChannelConfig.Click += new System.EventHandler(this.cmdBrowseChannelConfig_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(303, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Output Sequence";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Song File";
			// 
			// txtOutputFile
			// 
			this.txtOutputFile.Location = new System.Drawing.Point(306, 33);
			this.txtOutputFile.Name = "txtOutputFile";
			this.txtOutputFile.Size = new System.Drawing.Size(244, 20);
			this.txtOutputFile.TabIndex = 7;
			// 
			// btnBrowseOutput
			// 
			this.btnBrowseOutput.Location = new System.Drawing.Point(556, 33);
			this.btnBrowseOutput.Name = "btnBrowseOutput";
			this.btnBrowseOutput.Size = new System.Drawing.Size(27, 21);
			this.btnBrowseOutput.TabIndex = 6;
			this.btnBrowseOutput.Text = "...";
			this.btnBrowseOutput.UseVisualStyleBackColor = true;
			this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
			// 
			// txtMusicFile
			// 
			this.txtMusicFile.Location = new System.Drawing.Point(6, 32);
			this.txtMusicFile.Name = "txtMusicFile";
			this.txtMusicFile.Size = new System.Drawing.Size(219, 20);
			this.txtMusicFile.TabIndex = 5;
			// 
			// btnBrowseMusic
			// 
			this.btnBrowseMusic.Location = new System.Drawing.Point(231, 32);
			this.btnBrowseMusic.Name = "btnBrowseMusic";
			this.btnBrowseMusic.Size = new System.Drawing.Size(27, 21);
			this.btnBrowseMusic.TabIndex = 4;
			this.btnBrowseMusic.Text = "...";
			this.btnBrowseMusic.UseVisualStyleBackColor = true;
			this.btnBrowseMusic.Click += new System.EventHandler(this.btnBrowseMusic_Click);
			// 
			// grpChannels
			// 
			this.grpChannels.Controls.Add(this.pnlChannels);
			this.grpChannels.Location = new System.Drawing.Point(12, 132);
			this.grpChannels.Name = "grpChannels";
			this.grpChannels.Size = new System.Drawing.Size(865, 585);
			this.grpChannels.TabIndex = 7;
			this.grpChannels.TabStop = false;
			this.grpChannels.Text = " Channels ";
			// 
			// pnlChannels
			// 
			this.pnlChannels.AutoScroll = true;
			this.pnlChannels.Controls.Add(this.grpChannel);
			this.pnlChannels.Location = new System.Drawing.Point(6, 19);
			this.pnlChannels.Name = "pnlChannels";
			this.pnlChannels.Size = new System.Drawing.Size(852, 560);
			this.pnlChannels.TabIndex = 0;
			this.pnlChannels.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlChannels_Paint);
			// 
			// grpChannel
			// 
			this.grpChannel.Controls.Add(this.picColor);
			this.grpChannel.Controls.Add(this.lblDecibels);
			this.grpChannel.Controls.Add(this.lblFreq);
			this.grpChannel.Controls.Add(this.txtName);
			this.grpChannel.Controls.Add(this.lblName);
			this.grpChannel.Controls.Add(this.lblLow);
			this.grpChannel.Controls.Add(this.numLow);
			this.grpChannel.Controls.Add(this.numHigh);
			this.grpChannel.Controls.Add(this.numChannel);
			this.grpChannel.Controls.Add(this.numUnit);
			this.grpChannel.Controls.Add(this.lblChannel);
			this.grpChannel.Controls.Add(this.lblStart);
			this.grpChannel.Controls.Add(this.cboDevice);
			this.grpChannel.Controls.Add(this.lblDevice);
			this.grpChannel.Controls.Add(this.lblPreset);
			this.grpChannel.Controls.Add(this.cboPreset);
			this.grpChannel.Controls.Add(this.lblEnd);
			this.grpChannel.Controls.Add(this.lblUnit);
			this.grpChannel.Controls.Add(this.numStart);
			this.grpChannel.Controls.Add(this.numEnd);
			this.grpChannel.Controls.Add(this.lblHigh);
			this.grpChannel.Location = new System.Drawing.Point(3, 3);
			this.grpChannel.Name = "grpChannel";
			this.grpChannel.Size = new System.Drawing.Size(808, 104);
			this.grpChannel.TabIndex = 1;
			this.grpChannel.TabStop = false;
			this.grpChannel.Text = " Channel 1 ";
			// 
			// picColor
			// 
			this.picColor.Location = new System.Drawing.Point(621, 44);
			this.picColor.Name = "picColor";
			this.picColor.Size = new System.Drawing.Size(24, 24);
			this.picColor.TabIndex = 22;
			this.picColor.TabStop = false;
			// 
			// lblDecibels
			// 
			this.lblDecibels.AutoSize = true;
			this.lblDecibels.Location = new System.Drawing.Point(460, 76);
			this.lblDecibels.Name = "lblDecibels";
			this.lblDecibels.Size = new System.Drawing.Size(21, 13);
			this.lblDecibels.TabIndex = 20;
			this.lblDecibels.Text = "Db";
			// 
			// lblFreq
			// 
			this.lblFreq.AutoSize = true;
			this.lblFreq.Location = new System.Drawing.Point(460, 50);
			this.lblFreq.Name = "lblFreq";
			this.lblFreq.Size = new System.Drawing.Size(20, 13);
			this.lblFreq.TabIndex = 19;
			this.lblFreq.Text = "Hz";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(504, 22);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(141, 20);
			this.txtName.TabIndex = 18;
			this.txtName.Text = "Channel 1";
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(460, 25);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(38, 13);
			this.lblName.TabIndex = 17;
			this.lblName.Text = "Name:";
			// 
			// lblLow
			// 
			this.lblLow.AutoSize = true;
			this.lblLow.Location = new System.Drawing.Point(224, 76);
			this.lblLow.Name = "lblLow";
			this.lblLow.Size = new System.Drawing.Size(61, 13);
			this.lblLow.TabIndex = 16;
			this.lblLow.Text = "Low Cutoff:";
			// 
			// numLow
			// 
			this.numLow.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numLow.Location = new System.Drawing.Point(286, 74);
			this.numLow.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numLow.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
			this.numLow.Name = "numLow";
			this.numLow.Size = new System.Drawing.Size(50, 20);
			this.numLow.TabIndex = 14;
			this.numLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numLow.Value = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
			// 
			// numHigh
			// 
			this.numHigh.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numHigh.Location = new System.Drawing.Point(404, 74);
			this.numHigh.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numHigh.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
			this.numHigh.Name = "numHigh";
			this.numHigh.Size = new System.Drawing.Size(50, 20);
			this.numHigh.TabIndex = 13;
			this.numHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numHigh.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// numChannel
			// 
			this.numChannel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numChannel.Location = new System.Drawing.Point(403, 23);
			this.numChannel.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
			this.numChannel.Name = "numChannel";
			this.numChannel.Size = new System.Drawing.Size(50, 20);
			this.numChannel.TabIndex = 12;
			this.numChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numChannel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numUnit
			// 
			this.numUnit.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUnit.Location = new System.Drawing.Point(286, 22);
			this.numUnit.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.numUnit.Name = "numUnit";
			this.numUnit.Size = new System.Drawing.Size(50, 20);
			this.numUnit.TabIndex = 11;
			this.numUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUnit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblChannel
			// 
			this.lblChannel.AutoSize = true;
			this.lblChannel.Location = new System.Drawing.Point(344, 25);
			this.lblChannel.Name = "lblChannel";
			this.lblChannel.Size = new System.Drawing.Size(49, 13);
			this.lblChannel.TabIndex = 10;
			this.lblChannel.Text = "Channel:";
			// 
			// lblStart
			// 
			this.lblStart.AutoSize = true;
			this.lblStart.Location = new System.Drawing.Point(224, 50);
			this.lblStart.Name = "lblStart";
			this.lblStart.Size = new System.Drawing.Size(56, 13);
			this.lblStart.TabIndex = 9;
			this.lblStart.Text = "Start Freq:";
			// 
			// cboDevice
			// 
			this.cboDevice.FormattingEnabled = true;
			this.cboDevice.Location = new System.Drawing.Point(55, 22);
			this.cboDevice.Name = "cboDevice";
			this.cboDevice.Size = new System.Drawing.Size(100, 21);
			this.cboDevice.TabIndex = 8;
			// 
			// lblDevice
			// 
			this.lblDevice.AutoSize = true;
			this.lblDevice.Location = new System.Drawing.Point(5, 25);
			this.lblDevice.Name = "lblDevice";
			this.lblDevice.Size = new System.Drawing.Size(44, 13);
			this.lblDevice.TabIndex = 7;
			this.lblDevice.Text = "Device:";
			// 
			// lblPreset
			// 
			this.lblPreset.AutoSize = true;
			this.lblPreset.Location = new System.Drawing.Point(7, 50);
			this.lblPreset.Name = "lblPreset";
			this.lblPreset.Size = new System.Drawing.Size(40, 13);
			this.lblPreset.TabIndex = 6;
			this.lblPreset.Text = "Preset:";
			// 
			// cboPreset
			// 
			this.cboPreset.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboPreset.FormattingEnabled = true;
			this.cboPreset.Location = new System.Drawing.Point(56, 47);
			this.cboPreset.Name = "cboPreset";
			this.cboPreset.Size = new System.Drawing.Size(162, 22);
			this.cboPreset.TabIndex = 5;
			this.cboPreset.Click += new System.EventHandler(this.cboPreset_SelectedIndexChanged);
			// 
			// lblEnd
			// 
			this.lblEnd.AutoSize = true;
			this.lblEnd.Location = new System.Drawing.Point(344, 50);
			this.lblEnd.Name = "lblEnd";
			this.lblEnd.Size = new System.Drawing.Size(53, 13);
			this.lblEnd.TabIndex = 4;
			this.lblEnd.Text = "End Freq:";
			// 
			// lblUnit
			// 
			this.lblUnit.AutoSize = true;
			this.lblUnit.Location = new System.Drawing.Point(223, 25);
			this.lblUnit.Name = "lblUnit";
			this.lblUnit.Size = new System.Drawing.Size(29, 13);
			this.lblUnit.TabIndex = 3;
			this.lblUnit.Text = "Unit:";
			// 
			// numStart
			// 
			this.numStart.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numStart.Location = new System.Drawing.Point(286, 48);
			this.numStart.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numStart.Name = "numStart";
			this.numStart.Size = new System.Drawing.Size(50, 20);
			this.numStart.TabIndex = 2;
			this.numStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numStart.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			// 
			// numEnd
			// 
			this.numEnd.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numEnd.Location = new System.Drawing.Point(404, 48);
			this.numEnd.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			this.numEnd.Name = "numEnd";
			this.numEnd.Size = new System.Drawing.Size(50, 20);
			this.numEnd.TabIndex = 1;
			this.numEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numEnd.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
			// 
			// lblHigh
			// 
			this.lblHigh.AutoSize = true;
			this.lblHigh.Location = new System.Drawing.Point(344, 76);
			this.lblHigh.Name = "lblHigh";
			this.lblHigh.Size = new System.Drawing.Size(63, 13);
			this.lblHigh.TabIndex = 15;
			this.lblHigh.Text = "High Cutoff:";
			// 
			// frmLorOTune
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(886, 788);
			this.Controls.Add(this.grpChannels);
			this.Controls.Add(this.grpFiles);
			this.Controls.Add(this.btnOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmLorOTune";
			this.Text = "Lor-O-Tune";
			this.Load += new System.EventHandler(this.frmLorOTune_Load);
			this.grpFiles.ResumeLayout(false);
			this.grpFiles.PerformLayout();
			this.grpChannels.ResumeLayout(false);
			this.pnlChannels.ResumeLayout(false);
			this.grpChannel.ResumeLayout(false);
			this.grpChannel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picColor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numLow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numHigh)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numChannel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUnit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numEnd)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox grpFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.TextBox txtMusicFile;
        private System.Windows.Forms.Button btnBrowseMusic;
        private System.Windows.Forms.GroupBox grpChannels;
        private System.Windows.Forms.Panel pnlChannels;
        private System.Windows.Forms.GroupBox grpChannel;
        private System.Windows.Forms.Label lblLow;
        private System.Windows.Forms.Label lblHigh;
        private System.Windows.Forms.NumericUpDown numLow;
        private System.Windows.Forms.NumericUpDown numHigh;
        private System.Windows.Forms.NumericUpDown numChannel;
        private System.Windows.Forms.NumericUpDown numUnit;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.ComboBox cboDevice;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Label lblPreset;
        private System.Windows.Forms.ComboBox cboPreset;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.NumericUpDown numStart;
        private System.Windows.Forms.NumericUpDown numEnd;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label txtFreqPresetFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPresetFile;
        private System.Windows.Forms.Button cmdBrowseFreqPreset;
        private System.Windows.Forms.TextBox txtChannelFile;
        private System.Windows.Forms.Button cmdBrowseChannelConfig;
        private System.Windows.Forms.Label lblDecibels;
        private System.Windows.Forms.Label lblFreq;
        private System.Windows.Forms.Button btnSavePresets;
        private System.Windows.Forms.PictureBox picColor;
    }
}

