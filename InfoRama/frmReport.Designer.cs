namespace InfoRama
{
	partial class frmReport
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReport));
			this.pnlReport = new System.Windows.Forms.Panel();
			this.webReport = new System.Windows.Forms.WebBrowser();
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.btnBrowseSeq = new System.Windows.Forms.Button();
			this.btnSaveReport = new System.Windows.Forms.Button();
			this.btnRename = new System.Windows.Forms.Button();
			this.pnlReport.SuspendLayout();
			this.staStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlReport
			// 
			this.pnlReport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlReport.Controls.Add(this.webReport);
			this.pnlReport.Location = new System.Drawing.Point(5, 5);
			this.pnlReport.Name = "pnlReport";
			this.pnlReport.Size = new System.Drawing.Size(600, 500);
			this.pnlReport.TabIndex = 1;
			// 
			// webReport
			// 
			this.webReport.AllowWebBrowserDrop = false;
			this.webReport.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webReport.Location = new System.Drawing.Point(0, 0);
			this.webReport.MinimumSize = new System.Drawing.Size(20, 20);
			this.webReport.Name = "webReport";
			this.webReport.Size = new System.Drawing.Size(596, 496);
			this.webReport.TabIndex = 1;
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlProgress,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 611);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(608, 24);
			this.staStatus.TabIndex = 62;
			this.staStatus.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.staStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.staStatus.DragLeave += new System.EventHandler(this.Event_DragLeave);
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
			this.pnlStatus.Size = new System.Drawing.Size(496, 19);
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
			// btnBrowseSeq
			// 
			this.btnBrowseSeq.AllowDrop = true;
			this.btnBrowseSeq.Location = new System.Drawing.Point(67, 553);
			this.btnBrowseSeq.Name = "btnBrowseSeq";
			this.btnBrowseSeq.Size = new System.Drawing.Size(160, 23);
			this.btnBrowseSeq.TabIndex = 63;
			this.btnBrowseSeq.Text = "Analyze a Sequence...";
			this.btnBrowseSeq.UseVisualStyleBackColor = true;
			this.btnBrowseSeq.Click += new System.EventHandler(this.btnBrowseSeq_Click);
			this.btnBrowseSeq.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseSeq.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.btnBrowseSeq.DragLeave += new System.EventHandler(this.Event_DragLeave);
			// 
			// btnSaveReport
			// 
			this.btnSaveReport.AllowDrop = true;
			this.btnSaveReport.Enabled = false;
			this.btnSaveReport.Location = new System.Drawing.Point(319, 553);
			this.btnSaveReport.Name = "btnSaveReport";
			this.btnSaveReport.Size = new System.Drawing.Size(160, 23);
			this.btnSaveReport.TabIndex = 64;
			this.btnSaveReport.Text = "Save Report As...";
			this.btnSaveReport.UseVisualStyleBackColor = true;
			this.btnSaveReport.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSaveReport.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.btnSaveReport.DragLeave += new System.EventHandler(this.Event_DragLeave);
			// 
			// btnRename
			// 
			this.btnRename.Location = new System.Drawing.Point(10, 558);
			this.btnRename.Name = "btnRename";
			this.btnRename.Size = new System.Drawing.Size(43, 23);
			this.btnRename.TabIndex = 65;
			this.btnRename.Text = "Ren";
			this.btnRename.UseVisualStyleBackColor = true;
			this.btnRename.Visible = false;
			this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
			// 
			// frmReport
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(608, 635);
			this.Controls.Add(this.btnRename);
			this.Controls.Add(this.btnSaveReport);
			this.Controls.Add(this.btnBrowseSeq);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.pnlReport);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 400);
			this.Name = "frmReport";
			this.Text = "Info-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReport_FormClosing);
			this.Load += new System.EventHandler(this.frmReport_Load);
			this.ResizeEnd += new System.EventHandler(this.frmReport_ResizeEnd);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.DragLeave += new System.EventHandler(this.Event_DragLeave);
			this.pnlReport.ResumeLayout(false);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pnlReport;
		private System.Windows.Forms.WebBrowser webReport;
		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripProgressBar pnlProgress;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.Button btnBrowseSeq;
		private System.Windows.Forms.Button btnSaveReport;
		private System.Windows.Forms.Button btnRename;
	}
}

