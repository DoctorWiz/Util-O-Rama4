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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTune));
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.ttip = new System.Windows.Forms.ToolTip(this.components);
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.btnSaveOptions = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.btnSaveSequence = new System.Windows.Forms.Button();
			this.btnBrowseSequence = new System.Windows.Forms.Button();
			this.txtSequenceFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.staStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// imlTreeIcons
			// 
			this.imlTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeIcons.ImageStream")));
			this.imlTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlTreeIcons.Images.SetKeyName(0, "track");
			this.imlTreeIcons.Images.SetKeyName(1, "channelGroup");
			this.imlTreeIcons.Images.SetKeyName(2, "rgbChannel");
			this.imlTreeIcons.Images.SetKeyName(3, "FF0000");
			this.imlTreeIcons.Images.SetKeyName(4, "00FF00");
			this.imlTreeIcons.Images.SetKeyName(5, "0000FF");
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlProgress,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 209);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(330, 24);
			this.staStatus.TabIndex = 62;
			this.staStatus.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.staStatus_ItemClicked);
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
			this.pnlStatus.Size = new System.Drawing.Size(85, 19);
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
			// btnSaveOptions
			// 
			this.btnSaveOptions.AllowDrop = true;
			this.btnSaveOptions.Location = new System.Drawing.Point(115, 130);
			this.btnSaveOptions.Name = "btnSaveOptions";
			this.btnSaveOptions.Size = new System.Drawing.Size(75, 23);
			this.btnSaveOptions.TabIndex = 109;
			this.btnSaveOptions.Text = "Options...";
			this.btnSaveOptions.UseVisualStyleBackColor = true;
			this.btnSaveOptions.Click += new System.EventHandler(this.btnSaveOptions_Click);
			this.btnSaveOptions.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSaveOptions.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// label4
			// 
			this.label4.AllowDrop = true;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(109, 114);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 13);
			this.label4.TabIndex = 108;
			this.label4.Text = "New Sequence";
			this.label4.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.label4.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnSaveSequence
			// 
			this.btnSaveSequence.AllowDrop = true;
			this.btnSaveSequence.Enabled = false;
			this.btnSaveSequence.Location = new System.Drawing.Point(115, 159);
			this.btnSaveSequence.Name = "btnSaveSequence";
			this.btnSaveSequence.Size = new System.Drawing.Size(75, 23);
			this.btnSaveSequence.TabIndex = 104;
			this.btnSaveSequence.Text = "Save As...";
			this.btnSaveSequence.UseVisualStyleBackColor = true;
			this.btnSaveSequence.Click += new System.EventHandler(this.btnSave_Click);
			this.btnSaveSequence.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSaveSequence.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// btnBrowseSequence
			// 
			this.btnBrowseSequence.AllowDrop = true;
			this.btnBrowseSequence.Location = new System.Drawing.Point(240, 25);
			this.btnBrowseSequence.Name = "btnBrowseSequence";
			this.btnBrowseSequence.Size = new System.Drawing.Size(75, 20);
			this.btnBrowseSequence.TabIndex = 106;
			this.btnBrowseSequence.Text = "Browse...";
			this.btnBrowseSequence.UseVisualStyleBackColor = true;
			this.btnBrowseSequence.Click += new System.EventHandler(this.btnBrowseSeq_Click);
			this.btnBrowseSequence.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseSequence.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// txtSequenceFile
			// 
			this.txtSequenceFile.AllowDrop = true;
			this.txtSequenceFile.Location = new System.Drawing.Point(15, 25);
			this.txtSequenceFile.Name = "txtSequenceFile";
			this.txtSequenceFile.ReadOnly = true;
			this.txtSequenceFile.Size = new System.Drawing.Size(219, 20);
			this.txtSequenceFile.TabIndex = 105;
			this.txtSequenceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtSequenceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// label1
			// 
			this.label1.AllowDrop = true;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 107;
			this.label1.Text = "Sequence File";
			this.label1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.label1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// frmTune
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(330, 233);
			this.Controls.Add(this.btnSaveOptions);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnSaveSequence);
			this.Controls.Add(this.btnBrowseSequence);
			this.Controls.Add(this.txtSequenceFile);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.staStatus);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmTune";
			this.Text = "Tune-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTune_FormClosing);
			this.Load += new System.EventHandler(this.frmTune_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.ToolTip ttip;
		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripProgressBar pnlProgress;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.Button btnSaveOptions;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnSaveSequence;
		private System.Windows.Forms.Button btnBrowseSequence;
		private System.Windows.Forms.TextBox txtSequenceFile;
		private System.Windows.Forms.Label label1;
	}
}

