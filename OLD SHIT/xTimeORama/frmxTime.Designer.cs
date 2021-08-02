using Syncfusion.Windows.Forms.Tools;

namespace xTimeORama
{
	partial class frmxTime
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
				seq.Dispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmxTime));
			Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo2 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.btnBrowseSequence = new System.Windows.Forms.Button();
			this.txtSequenceFile = new System.Windows.Forms.TextBox();
			this.lblSequenceFile = new System.Windows.Forms.Label();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.btnExport = new System.Windows.Forms.Button();
			this.lblSelectionCount = new System.Windows.Forms.Label();
			this.ttip = new System.Windows.Forms.ToolTip(this.components);
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblSelections = new System.Windows.Forms.Label();
			this.lblTreeClicks = new System.Windows.Forms.Label();
			this.lblChannels = new System.Windows.Forms.Label();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.treChannels = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
			this.panel1 = new System.Windows.Forms.Panel();
			this.optMultiPer = new System.Windows.Forms.RadioButton();
			this.optOnePer = new System.Windows.Forms.RadioButton();
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treChannels)).BeginInit();
			this.panel1.SuspendLayout();
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
			// btnBrowseSequence
			// 
			this.btnBrowseSequence.AllowDrop = true;
			this.btnBrowseSequence.Location = new System.Drawing.Point(235, 29);
			this.btnBrowseSequence.Name = "btnBrowseSequence";
			this.btnBrowseSequence.Size = new System.Drawing.Size(75, 20);
			this.btnBrowseSequence.TabIndex = 2;
			this.btnBrowseSequence.Text = "Browse...";
			this.btnBrowseSequence.UseVisualStyleBackColor = true;
			this.btnBrowseSequence.Click += new System.EventHandler(this.btnBrowseSeq_Click);
			this.btnBrowseSequence.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseSequence.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// txtSequenceFile
			// 
			this.txtSequenceFile.AllowDrop = true;
			this.txtSequenceFile.Location = new System.Drawing.Point(12, 29);
			this.txtSequenceFile.Name = "txtSequenceFile";
			this.txtSequenceFile.ReadOnly = true;
			this.txtSequenceFile.Size = new System.Drawing.Size(219, 20);
			this.txtSequenceFile.TabIndex = 1;
			this.txtSequenceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.txtSequenceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSequenceFile
			// 
			this.lblSequenceFile.AllowDrop = true;
			this.lblSequenceFile.AutoSize = true;
			this.lblSequenceFile.Location = new System.Drawing.Point(12, 13);
			this.lblSequenceFile.Name = "lblSequenceFile";
			this.lblSequenceFile.Size = new System.Drawing.Size(75, 13);
			this.lblSequenceFile.TabIndex = 100;
			this.lblSequenceFile.Text = "Sequence File";
			this.lblSequenceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSequenceFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// dlgOpenFile
			// 
			this.dlgOpenFile.FileName = "openFileDialog1";
			// 
			// btnExport
			// 
			this.btnExport.AllowDrop = true;
			this.btnExport.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnExport.Enabled = false;
			this.btnExport.Location = new System.Drawing.Point(169, 409);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(83, 41);
			this.btnExport.TabIndex = 12;
			this.btnExport.Text = "Export to...";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.btnExport.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnExport.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblSelectionCount
			// 
			this.lblSelectionCount.AllowDrop = true;
			this.lblSelectionCount.AutoSize = true;
			this.lblSelectionCount.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblSelectionCount.Location = new System.Drawing.Point(49, 468);
			this.lblSelectionCount.Name = "lblSelectionCount";
			this.lblSelectionCount.Size = new System.Drawing.Size(13, 13);
			this.lblSelectionCount.TabIndex = 11;
			this.lblSelectionCount.Text = "0";
			this.lblSelectionCount.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSelectionCount.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlProgress,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 481);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(448, 24);
			this.staStatus.TabIndex = 61;
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
			this.pnlStatus.Size = new System.Drawing.Size(336, 19);
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
			// lblSelections
			// 
			this.lblSelections.AllowDrop = true;
			this.lblSelections.AutoSize = true;
			this.lblSelections.Location = new System.Drawing.Point(172, 453);
			this.lblSelections.Name = "lblSelections";
			this.lblSelections.Size = new System.Drawing.Size(59, 13);
			this.lblSelections.TabIndex = 10;
			this.lblSelections.Text = "Selections:";
			this.lblSelections.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.lblSelections.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// lblTreeClicks
			// 
			this.lblTreeClicks.AllowDrop = true;
			this.lblTreeClicks.AutoSize = true;
			this.lblTreeClicks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTreeClicks.ForeColor = System.Drawing.Color.DarkRed;
			this.lblTreeClicks.Location = new System.Drawing.Point(272, 468);
			this.lblTreeClicks.Name = "lblTreeClicks";
			this.lblTreeClicks.Size = new System.Drawing.Size(13, 13);
			this.lblTreeClicks.TabIndex = 105;
			this.lblTreeClicks.Text = "0";
			// 
			// lblChannels
			// 
			this.lblChannels.AllowDrop = true;
			this.lblChannels.AutoSize = true;
			this.lblChannels.Location = new System.Drawing.Point(10, 59);
			this.lblChannels.Name = "lblChannels";
			this.lblChannels.Size = new System.Drawing.Size(154, 13);
			this.lblChannels.TabIndex = 108;
			this.lblChannels.Text = "Tracks, Groups, and Channels:";
			// 
			// picPreview
			// 
			this.picPreview.BackColor = System.Drawing.Color.Silver;
			this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picPreview.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picPreview.Location = new System.Drawing.Point(10, 379);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(300, 20);
			this.picPreview.TabIndex = 110;
			this.picPreview.TabStop = false;
			this.picPreview.Visible = false;
			// 
			// treChannels
			// 
			this.treChannels.BackColor = System.Drawing.Color.White;
			treeNodeAdvStyleInfo2.EnsureDefaultOptionedChild = true;
			treeNodeAdvStyleInfo2.TextColor = System.Drawing.Color.Black;
			this.treChannels.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo2)});
			this.treChannels.BeforeTouchSize = new System.Drawing.Size(297, 279);
			this.treChannels.ForeColor = System.Drawing.Color.Black;
			// 
			// 
			// 
			this.treChannels.HelpTextControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.treChannels.HelpTextControl.Location = new System.Drawing.Point(0, 0);
			this.treChannels.HelpTextControl.Name = "helpText";
			this.treChannels.HelpTextControl.Size = new System.Drawing.Size(49, 15);
			this.treChannels.HelpTextControl.TabIndex = 0;
			this.treChannels.HelpTextControl.Text = "help text";
			this.treChannels.InactiveSelectedNodeForeColor = System.Drawing.SystemColors.ControlText;
			this.treChannels.LeftImageList = this.imlTreeIcons;
			this.treChannels.Location = new System.Drawing.Point(12, 86);
			this.treChannels.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
			this.treChannels.Name = "treChannels";
			this.treChannels.SelectedNodeForeColor = System.Drawing.SystemColors.HighlightText;
			this.treChannels.Size = new System.Drawing.Size(297, 279);
			this.treChannels.TabIndex = 111;
			// 
			// 
			// 
			this.treChannels.ToolTipControl.BackColor = System.Drawing.SystemColors.Info;
			this.treChannels.ToolTipControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.treChannels.ToolTipControl.Location = new System.Drawing.Point(0, 0);
			this.treChannels.ToolTipControl.Name = "toolTip";
			this.treChannels.ToolTipControl.Size = new System.Drawing.Size(41, 15);
			this.treChannels.ToolTipControl.TabIndex = 1;
			this.treChannels.ToolTipControl.Text = "toolTip";
			this.treChannels.AfterSelect += new System.EventHandler(this.treChannels_AfterSelect);
			this.treChannels.AfterCheck += new Syncfusion.Windows.Forms.Tools.TreeNodeAdvEventHandler(this.treChannels_AfterCheck);
			this.treChannels.Click += new System.EventHandler(this.treChannels_Click);
			this.treChannels.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.treChannels.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.optMultiPer);
			this.panel1.Controls.Add(this.optOnePer);
			this.panel1.Location = new System.Drawing.Point(13, 408);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(150, 47);
			this.panel1.TabIndex = 112;
			// 
			// optMultiPer
			// 
			this.optMultiPer.AutoSize = true;
			this.optMultiPer.Checked = true;
			this.optMultiPer.Enabled = false;
			this.optMultiPer.Location = new System.Drawing.Point(3, 27);
			this.optMultiPer.Name = "optMultiPer";
			this.optMultiPer.Size = new System.Drawing.Size(143, 17);
			this.optMultiPer.TabIndex = 2;
			this.optMultiPer.TabStop = true;
			this.optMultiPer.Text = "One file with all Channels";
			this.optMultiPer.UseVisualStyleBackColor = true;
			// 
			// optOnePer
			// 
			this.optOnePer.AutoSize = true;
			this.optOnePer.Enabled = false;
			this.optOnePer.Location = new System.Drawing.Point(3, 4);
			this.optOnePer.Name = "optOnePer";
			this.optOnePer.Size = new System.Drawing.Size(121, 17);
			this.optOnePer.TabIndex = 1;
			this.optOnePer.Text = "One file per Channel";
			this.optOnePer.UseVisualStyleBackColor = true;
			// 
			// frmxTime
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(448, 505);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.treChannels);
			this.Controls.Add(this.picPreview);
			this.Controls.Add(this.lblChannels);
			this.Controls.Add(this.lblTreeClicks);
			this.Controls.Add(this.lblSelections);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.lblSelectionCount);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnBrowseSequence);
			this.Controls.Add(this.txtSequenceFile);
			this.Controls.Add(this.lblSequenceFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(50, 39);
			this.Name = "frmxTime";
			this.Text = "xTime-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmxTime_FormClosing);
			this.Load += new System.EventHandler(this.frmxTime_Load);
			this.Shown += new System.EventHandler(this.frmxTime_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmxTime_Paint);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treChannels)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnBrowseSequence;
		private System.Windows.Forms.TextBox txtSequenceFile;
		private System.Windows.Forms.Label lblSequenceFile;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Label lblSelectionCount;
		private System.Windows.Forms.ToolTip ttip;
		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.ToolStripProgressBar pnlProgress;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.Label lblSelections;
		private System.Windows.Forms.Label lblTreeClicks;
		private System.Windows.Forms.Label lblChannels;
		private System.Windows.Forms.PictureBox picPreview;
		private TreeViewAdv treChannels;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton optMultiPer;
		private System.Windows.Forms.RadioButton optOnePer;
	}
}

