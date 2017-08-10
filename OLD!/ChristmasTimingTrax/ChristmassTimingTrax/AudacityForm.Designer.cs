namespace ChristmassTimingTrax
{
    partial class AudacityForm
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
            this.btnAnalyzeInputFile = new System.Windows.Forms.Button();
            this.InputGroup = new System.Windows.Forms.GroupBox();
            this.chkPolyTranscription = new System.Windows.Forms.CheckBox();
            this.btnInputFile = new System.Windows.Forms.Button();
            this.tbInputFile = new System.Windows.Forms.TextBox();
            this.lbInputFile = new System.Windows.Forms.Label();
            this.OutputGroup = new System.Windows.Forms.GroupBox();
            this.chkUseFrequency = new System.Windows.Forms.CheckBox();
            this.tbOutputFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.cbOutputFormat = new System.Windows.Forms.ComboBox();
            this.lblOutputFormat = new System.Windows.Forms.Label();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.tbOutputFolder = new System.Windows.Forms.TextBox();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.DialogInputFile = new System.Windows.Forms.OpenFileDialog();
            this.bwAnalyzer = new System.ComponentModel.BackgroundWorker();
            this.lblAnalyzerResult = new System.Windows.Forms.Label();
            this.bwGenerator = new System.ComponentModel.BackgroundWorker();
            this.DialogOutputFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnGenerateTimingFiles = new System.Windows.Forms.Button();
            this.InputGroup.SuspendLayout();
            this.OutputGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAnalyzeInputFile
            // 
            this.btnAnalyzeInputFile.Enabled = false;
            this.btnAnalyzeInputFile.Location = new System.Drawing.Point(310, 98);
            this.btnAnalyzeInputFile.Name = "btnAnalyzeInputFile";
            this.btnAnalyzeInputFile.Size = new System.Drawing.Size(109, 23);
            this.btnAnalyzeInputFile.TabIndex = 0;
            this.btnAnalyzeInputFile.Text = "Analyze Input File";
            this.btnAnalyzeInputFile.UseVisualStyleBackColor = true;
            this.btnAnalyzeInputFile.Click += new System.EventHandler(this.btnAnalyzeInputFile_Click);
            // 
            // InputGroup
            // 
            this.InputGroup.Controls.Add(this.chkPolyTranscription);
            this.InputGroup.Controls.Add(this.btnInputFile);
            this.InputGroup.Controls.Add(this.tbInputFile);
            this.InputGroup.Controls.Add(this.lbInputFile);
            this.InputGroup.Location = new System.Drawing.Point(13, 13);
            this.InputGroup.Name = "InputGroup";
            this.InputGroup.Size = new System.Drawing.Size(406, 79);
            this.InputGroup.TabIndex = 1;
            this.InputGroup.TabStop = false;
            this.InputGroup.Text = "Input";
            // 
            // chkPolyTranscription
            // 
            this.chkPolyTranscription.AutoSize = true;
            this.chkPolyTranscription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPolyTranscription.Location = new System.Drawing.Point(29, 50);
            this.chkPolyTranscription.Name = "chkPolyTranscription";
            this.chkPolyTranscription.Size = new System.Drawing.Size(142, 17);
            this.chkPolyTranscription.TabIndex = 7;
            this.chkPolyTranscription.Text = "Polyphonic Transcription";
            this.chkPolyTranscription.UseVisualStyleBackColor = true;
            this.chkPolyTranscription.CheckedChanged += new System.EventHandler(this.chkPolyTranscription_CheckedChanged);
            // 
            // btnInputFile
            // 
            this.btnInputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInputFile.Location = new System.Drawing.Point(303, 21);
            this.btnInputFile.Name = "btnInputFile";
            this.btnInputFile.Size = new System.Drawing.Size(80, 25);
            this.btnInputFile.TabIndex = 6;
            this.btnInputFile.Text = "Browse...";
            this.btnInputFile.UseVisualStyleBackColor = true;
            this.btnInputFile.Click += new System.EventHandler(this.dlgInputFile);
            // 
            // tbInputFile
            // 
            this.tbInputFile.Location = new System.Drawing.Point(29, 24);
            this.tbInputFile.Name = "tbInputFile";
            this.tbInputFile.ReadOnly = true;
            this.tbInputFile.Size = new System.Drawing.Size(268, 20);
            this.tbInputFile.TabIndex = 5;
            this.tbInputFile.Click += new System.EventHandler(this.dlgInputFile);
            this.tbInputFile.TextChanged += new System.EventHandler(this.tbInputFile_TextChanged);
            // 
            // lbInputFile
            // 
            this.lbInputFile.AutoSize = true;
            this.lbInputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInputFile.Location = new System.Drawing.Point(6, 27);
            this.lbInputFile.Name = "lbInputFile";
            this.lbInputFile.Size = new System.Drawing.Size(26, 13);
            this.lbInputFile.TabIndex = 4;
            this.lbInputFile.Text = "File:";
            // 
            // OutputGroup
            // 
            this.OutputGroup.Controls.Add(this.chkUseFrequency);
            this.OutputGroup.Controls.Add(this.tbOutputFileName);
            this.OutputGroup.Controls.Add(this.lblFileName);
            this.OutputGroup.Controls.Add(this.cbOutputFormat);
            this.OutputGroup.Controls.Add(this.lblOutputFormat);
            this.OutputGroup.Controls.Add(this.btnOutputFolder);
            this.OutputGroup.Controls.Add(this.tbOutputFolder);
            this.OutputGroup.Controls.Add(this.lblOutputFolder);
            this.OutputGroup.Location = new System.Drawing.Point(13, 127);
            this.OutputGroup.Name = "OutputGroup";
            this.OutputGroup.Size = new System.Drawing.Size(406, 100);
            this.OutputGroup.TabIndex = 2;
            this.OutputGroup.TabStop = false;
            this.OutputGroup.Text = "Output";
            // 
            // chkUseFrequency
            // 
            this.chkUseFrequency.AutoSize = true;
            this.chkUseFrequency.Enabled = false;
            this.chkUseFrequency.Location = new System.Drawing.Point(234, 71);
            this.chkUseFrequency.Name = "chkUseFrequency";
            this.chkUseFrequency.Size = new System.Drawing.Size(162, 17);
            this.chkUseFrequency.TabIndex = 17;
            this.chkUseFrequency.Text = "Use Frequency as File Name";
            this.chkUseFrequency.UseVisualStyleBackColor = true;
            this.chkUseFrequency.CheckedChanged += new System.EventHandler(this.chkUseFrequency_CheckedChanged);
            // 
            // tbOutputFileName
            // 
            this.tbOutputFileName.Location = new System.Drawing.Point(65, 68);
            this.tbOutputFileName.MaxLength = 20;
            this.tbOutputFileName.Name = "tbOutputFileName";
            this.tbOutputFileName.Size = new System.Drawing.Size(162, 20);
            this.tbOutputFileName.TabIndex = 16;
            this.tbOutputFileName.Leave += new System.EventHandler(this.tbOutputFileName_Leave);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(11, 71);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(57, 13);
            this.lblFileName.TabIndex = 12;
            this.lblFileName.Text = "File Name:";
            // 
            // cbOutputFormat
            // 
            this.cbOutputFormat.FormattingEnabled = true;
            this.cbOutputFormat.Location = new System.Drawing.Point(65, 41);
            this.cbOutputFormat.Name = "cbOutputFormat";
            this.cbOutputFormat.Size = new System.Drawing.Size(121, 21);
            this.cbOutputFormat.TabIndex = 15;
            this.cbOutputFormat.SelectionChangeCommitted += new System.EventHandler(this.cbOutputFormat_SelectionChangeCommitted);
            // 
            // lblOutputFormat
            // 
            this.lblOutputFormat.AutoSize = true;
            this.lblOutputFormat.Location = new System.Drawing.Point(11, 44);
            this.lblOutputFormat.Name = "lblOutputFormat";
            this.lblOutputFormat.Size = new System.Drawing.Size(42, 13);
            this.lblOutputFormat.TabIndex = 11;
            this.lblOutputFormat.Text = "Format:";
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Location = new System.Drawing.Point(303, 12);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(75, 25);
            this.btnOutputFolder.TabIndex = 14;
            this.btnOutputFolder.Text = "Browse...";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.dlgOutputFolder);
            // 
            // tbOutputFolder
            // 
            this.tbOutputFolder.Location = new System.Drawing.Point(65, 15);
            this.tbOutputFolder.Name = "tbOutputFolder";
            this.tbOutputFolder.ReadOnly = true;
            this.tbOutputFolder.Size = new System.Drawing.Size(227, 20);
            this.tbOutputFolder.TabIndex = 13;
            this.tbOutputFolder.Click += new System.EventHandler(this.dlgOutputFolder);
            this.tbOutputFolder.TextChanged += new System.EventHandler(this.tbOutputFolder_TextChanged);
            // 
            // lblOutputFolder
            // 
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(11, 18);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(39, 13);
            this.lblOutputFolder.TabIndex = 10;
            this.lblOutputFolder.Text = "Folder:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(344, 233);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // DialogInputFile
            // 
            this.DialogInputFile.FileName = "InputFileDialog";
            // 
            // bwAnalyzer
            // 
            this.bwAnalyzer.WorkerReportsProgress = true;
            this.bwAnalyzer.WorkerSupportsCancellation = true;
            this.bwAnalyzer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwAnalyzer_DoWork);
            this.bwAnalyzer.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwAnalyzer_ProgressChanged);
            this.bwAnalyzer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwAnalyzer_RunWorkerCompleted);
            // 
            // lblAnalyzerResult
            // 
            this.lblAnalyzerResult.AutoSize = true;
            this.lblAnalyzerResult.Location = new System.Drawing.Point(13, 98);
            this.lblAnalyzerResult.Name = "lblAnalyzerResult";
            this.lblAnalyzerResult.Size = new System.Drawing.Size(0, 13);
            this.lblAnalyzerResult.TabIndex = 4;
            // 
            // bwGenerator
            // 
            this.bwGenerator.WorkerReportsProgress = true;
            this.bwGenerator.WorkerSupportsCancellation = true;
            this.bwGenerator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGenerator_DoWork);
            this.bwGenerator.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwGenerator_ProgressChanged);
            this.bwGenerator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwGenerator_RunWorkerCompleted);
            // 
            // DialogOutputFolder
            // 
            this.DialogOutputFolder.SelectedPath = "DialogOutputFolder";
            // 
            // btnGenerateTimingFiles
            // 
            this.btnGenerateTimingFiles.Enabled = false;
            this.btnGenerateTimingFiles.Location = new System.Drawing.Point(204, 233);
            this.btnGenerateTimingFiles.Name = "btnGenerateTimingFiles";
            this.btnGenerateTimingFiles.Size = new System.Drawing.Size(125, 25);
            this.btnGenerateTimingFiles.TabIndex = 11;
            this.btnGenerateTimingFiles.Text = "Generate Timing Files";
            this.btnGenerateTimingFiles.UseVisualStyleBackColor = true;
            this.btnGenerateTimingFiles.Click += new System.EventHandler(this.btnGenerateTimingFiles_Click);
            // 
            // AudacityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 268);
            this.Controls.Add(this.btnGenerateTimingFiles);
            this.Controls.Add(this.lblAnalyzerResult);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.OutputGroup);
            this.Controls.Add(this.InputGroup);
            this.Controls.Add(this.btnAnalyzeInputFile);
            this.Name = "AudacityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AudacityForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AudacityForm_FormClosing);
            this.Load += new System.EventHandler(this.AudacityForm_Load);
            this.InputGroup.ResumeLayout(false);
            this.InputGroup.PerformLayout();
            this.OutputGroup.ResumeLayout(false);
            this.OutputGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnalyzeInputFile;
        private System.Windows.Forms.GroupBox InputGroup;
        private System.Windows.Forms.GroupBox OutputGroup;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkPolyTranscription;
        private System.Windows.Forms.Button btnInputFile;
        private System.Windows.Forms.TextBox tbInputFile;
        private System.Windows.Forms.Label lbInputFile;
        private System.Windows.Forms.OpenFileDialog DialogInputFile;
        private System.ComponentModel.BackgroundWorker bwAnalyzer;
        private System.Windows.Forms.Label lblAnalyzerResult;
        private System.ComponentModel.BackgroundWorker bwGenerator;
        private System.Windows.Forms.CheckBox chkUseFrequency;
        private System.Windows.Forms.TextBox tbOutputFileName;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.ComboBox cbOutputFormat;
        private System.Windows.Forms.Label lblOutputFormat;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.TextBox tbOutputFolder;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.FolderBrowserDialog DialogOutputFolder;
        private System.Windows.Forms.Button btnGenerateTimingFiles;
    }
}