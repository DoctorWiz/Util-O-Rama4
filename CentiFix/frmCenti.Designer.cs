namespace CentiFix
{
	partial class frmCenti
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCenti));
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.btnBrowseSeq = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblLastEnd = new System.Windows.Forms.Label();
			this.lblNewTime = new System.Windows.Forms.Label();
			this.txtNewLength = new System.Windows.Forms.TextBox();
			this.txtFormat = new System.Windows.Forms.TextBox();
			this.txtNewCS = new System.Windows.Forms.TextBox();
			this.lblNewCS = new System.Windows.Forms.Label();
			this.lblCurCS = new System.Windows.Forms.Label();
			this.staStatus.SuspendLayout();
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
			this.staStatus.Location = new System.Drawing.Point(0, 287);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(384, 24);
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
			this.pnlStatus.Size = new System.Drawing.Size(272, 19);
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
			this.btnBrowseSeq.Location = new System.Drawing.Point(293, 23);
			this.btnBrowseSeq.Name = "btnBrowseSeq";
			this.btnBrowseSeq.Size = new System.Drawing.Size(79, 23);
			this.btnBrowseSeq.TabIndex = 63;
			this.btnBrowseSeq.Text = "Browse...";
			this.btnBrowseSeq.UseVisualStyleBackColor = true;
			this.btnBrowseSeq.Click += new System.EventHandler(this.btnBrowseSeq_Click);
			this.btnBrowseSeq.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnBrowseSeq.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.btnBrowseSeq.DragLeave += new System.EventHandler(this.Event_DragLeave);
			// 
			// btnSave
			// 
			this.btnSave.AllowDrop = true;
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(138, 248);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(94, 23);
			this.btnSave.TabIndex = 64;
			this.btnSave.Text = "Save As...";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnSave.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.btnSave.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.btnSave.DragLeave += new System.EventHandler(this.Event_DragLeave);
			// 
			// txtFile
			// 
			this.txtFile.Location = new System.Drawing.Point(12, 25);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(275, 20);
			this.txtFile.TabIndex = 65;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 13);
			this.label1.TabIndex = 66;
			this.label1.Text = "Sequence File:";
			// 
			// lblLastEnd
			// 
			this.lblLastEnd.AutoSize = true;
			this.lblLastEnd.Enabled = false;
			this.lblLastEnd.Location = new System.Drawing.Point(12, 94);
			this.lblLastEnd.Name = "lblLastEnd";
			this.lblLastEnd.Size = new System.Drawing.Size(134, 13);
			this.lblLastEnd.TabIndex = 67;
			this.lblLastEnd.Text = "Last effect ends at 0:00.00";
			// 
			// lblNewTime
			// 
			this.lblNewTime.AutoSize = true;
			this.lblNewTime.Location = new System.Drawing.Point(12, 128);
			this.lblNewTime.Name = "lblNewTime";
			this.lblNewTime.Size = new System.Drawing.Size(68, 13);
			this.lblNewTime.TabIndex = 68;
			this.lblNewTime.Text = "New Length:";
			// 
			// txtNewLength
			// 
			this.txtNewLength.Enabled = false;
			this.txtNewLength.Location = new System.Drawing.Point(86, 125);
			this.txtNewLength.Name = "txtNewLength";
			this.txtNewLength.Size = new System.Drawing.Size(66, 20);
			this.txtNewLength.TabIndex = 69;
			this.txtNewLength.Text = "0:00.00";
			this.txtNewLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtNewLength.TextChanged += new System.EventHandler(this.txtNewLength_TextChanged);
			this.txtNewLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewLength_KeyPress);
			this.txtNewLength.Validating += new System.ComponentModel.CancelEventHandler(this.txtNewLength_Validating);
			// 
			// txtFormat
			// 
			this.txtFormat.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtFormat.Location = new System.Drawing.Point(24, 151);
			this.txtFormat.Multiline = true;
			this.txtFormat.Name = "txtFormat";
			this.txtFormat.ReadOnly = true;
			this.txtFormat.Size = new System.Drawing.Size(304, 91);
			this.txtFormat.TabIndex = 70;
			this.txtFormat.Text = "Format: m:ss.cs\r\nWhere: m=minutes (must be specified, even if zero)\r\nWhere: ss=se" +
    "conds 0-59\r\nWhere: cs=centiseconds 0-99 (must be specified, even if zero))\r\nTime" +
    " must include one colon and one period.";
			// 
			// txtNewCS
			// 
			this.txtNewCS.Enabled = false;
			this.txtNewCS.Location = new System.Drawing.Point(293, 125);
			this.txtNewCS.Name = "txtNewCS";
			this.txtNewCS.Size = new System.Drawing.Size(66, 20);
			this.txtNewCS.TabIndex = 72;
			this.txtNewCS.Text = "0";
			this.txtNewCS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtNewCS.TextChanged += new System.EventHandler(this.txtNewCS_TextChanged);
			this.txtNewCS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewCS_KeyPress);
			this.txtNewCS.Validating += new System.ComponentModel.CancelEventHandler(this.txtNewCS_Validating);
			// 
			// lblNewCS
			// 
			this.lblNewCS.AutoSize = true;
			this.lblNewCS.Location = new System.Drawing.Point(219, 128);
			this.lblNewCS.Name = "lblNewCS";
			this.lblNewCS.Size = new System.Drawing.Size(74, 13);
			this.lblNewCS.TabIndex = 71;
			this.lblNewCS.Text = "Centiseconds:";
			// 
			// lblCurCS
			// 
			this.lblCurCS.AutoSize = true;
			this.lblCurCS.Enabled = false;
			this.lblCurCS.Location = new System.Drawing.Point(12, 72);
			this.lblCurCS.Name = "lblCurCS";
			this.lblCurCS.Size = new System.Drawing.Size(208, 13);
			this.lblCurCS.TabIndex = 73;
			this.lblCurCS.Text = "Current Sequence Length: 0 Centiseconds";
			// 
			// frmCenti
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 311);
			this.Controls.Add(this.lblCurCS);
			this.Controls.Add(this.txtNewCS);
			this.Controls.Add(this.lblNewCS);
			this.Controls.Add(this.txtFormat);
			this.Controls.Add(this.txtNewLength);
			this.Controls.Add(this.lblNewTime);
			this.Controls.Add(this.lblLastEnd);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtFile);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnBrowseSeq);
			this.Controls.Add(this.staStatus);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(400, 350);
			this.Name = "frmCenti";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " Util-O-Rama - CentiFix";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReport_FormClosing);
			this.Load += new System.EventHandler(this.frmReport_Load);
			this.ResizeEnd += new System.EventHandler(this.frmReport_ResizeEnd);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Event_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Event_DragEnter);
			this.DragLeave += new System.EventHandler(this.Event_DragLeave);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripProgressBar pnlProgress;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.Button btnBrowseSeq;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox txtFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblLastEnd;
		private System.Windows.Forms.Label lblNewTime;
		private System.Windows.Forms.TextBox txtNewLength;
		private System.Windows.Forms.TextBox txtFormat;
		private System.Windows.Forms.TextBox txtNewCS;
		private System.Windows.Forms.Label lblNewCS;
		private System.Windows.Forms.Label lblCurCS;
	}
}

