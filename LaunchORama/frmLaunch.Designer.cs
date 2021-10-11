
namespace UtilORama4
{
	partial class frmLaunch
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLaunch));
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.btnMap = new System.Windows.Forms.Button();
			this.lblMap = new System.Windows.Forms.Label();
			this.lblMerge = new System.Windows.Forms.Label();
			this.btnMerge = new System.Windows.Forms.Button();
			this.lblSplit = new System.Windows.Forms.Label();
			this.btnSplit = new System.Windows.Forms.Button();
			this.lblChannel = new System.Windows.Forms.Label();
			this.btnChannel = new System.Windows.Forms.Button();
			this.blbVamp = new System.Windows.Forms.Label();
			this.btnVamp = new System.Windows.Forms.Button();
			this.lblCompare = new System.Windows.Forms.Label();
			this.btnCompare = new System.Windows.Forms.Button();
			this.lblTime = new System.Windows.Forms.Label();
			this.btnTime = new System.Windows.Forms.Button();
			this.lblInfo = new System.Windows.Forms.Label();
			this.btnInfo = new System.Windows.Forms.Button();
			this.lblRGB = new System.Windows.Forms.Label();
			this.btnRGB = new System.Windows.Forms.Button();
			this.lblSparkle = new System.Windows.Forms.Label();
			this.btnSparkle = new System.Windows.Forms.Button();
			this.lblDim = new System.Windows.Forms.Label();
			this.btnDim = new System.Windows.Forms.Button();
			this.lblLight = new System.Windows.Forms.Label();
			this.btnLight = new System.Windows.Forms.Button();
			this.tipTool = new System.Windows.Forms.ToolTip(this.components);
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 463);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(498, 24);
			this.staStatus.TabIndex = 63;
			// 
			// pnlHelp
			// 
			this.pnlHelp.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlHelp.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.pnlHelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
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
			this.pnlStatus.Size = new System.Drawing.Size(386, 19);
			this.pnlStatus.Spring = true;
			// 
			// pnlAbout
			// 
			this.pnlAbout.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlAbout.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.pnlAbout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
			this.pnlAbout.ForeColor = System.Drawing.SystemColors.Highlight;
			this.pnlAbout.Name = "pnlAbout";
			this.pnlAbout.Size = new System.Drawing.Size(52, 19);
			this.pnlAbout.Text = "About...";
			this.pnlAbout.Click += new System.EventHandler(this.pnlAbout_Click);
			// 
			// picAboutIcon
			// 
			this.picAboutIcon.ErrorImage = null;
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(549, 444);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 121);
			this.picAboutIcon.TabIndex = 68;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// btnMap
			// 
			this.btnMap.Image = ((System.Drawing.Image)(resources.GetObject("btnMap.Image")));
			this.btnMap.Location = new System.Drawing.Point(10, 10);
			this.btnMap.Name = "btnMap";
			this.btnMap.Size = new System.Drawing.Size(116, 117);
			this.btnMap.TabIndex = 69;
			this.tipTool.SetToolTip(this.btnMap, "Map channels from one sequence to another and copy the effects.\r\n");
			this.btnMap.UseVisualStyleBackColor = true;
			this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
			// 
			// lblMap
			// 
			this.lblMap.Font = new System.Drawing.Font("American Text", 15.75F);
			this.lblMap.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblMap.Location = new System.Drawing.Point(10, 130);
			this.lblMap.Name = "lblMap";
			this.lblMap.Size = new System.Drawing.Size(116, 24);
			this.lblMap.TabIndex = 70;
			this.lblMap.Text = "Map-O-Rama";
			this.lblMap.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblMap.Click += new System.EventHandler(this.lblMap_Click);
			// 
			// lblMerge
			// 
			this.lblMerge.Font = new System.Drawing.Font("American Text", 15.75F);
			this.lblMerge.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblMerge.Location = new System.Drawing.Point(131, 130);
			this.lblMerge.Name = "lblMerge";
			this.lblMerge.Size = new System.Drawing.Size(116, 24);
			this.lblMerge.TabIndex = 72;
			this.lblMerge.Text = "Merge-O-Rama";
			this.lblMerge.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblMerge.Click += new System.EventHandler(this.lblMerge_Click);
			// 
			// btnMerge
			// 
			this.btnMerge.Image = ((System.Drawing.Image)(resources.GetObject("btnMerge.Image")));
			this.btnMerge.Location = new System.Drawing.Point(131, 10);
			this.btnMerge.Name = "btnMerge";
			this.btnMerge.Size = new System.Drawing.Size(116, 117);
			this.btnMerge.TabIndex = 71;
			this.tipTool.SetToolTip(this.btnMerge, "Merge selected tracks, groups, and channels from two sequences into one.\r\n");
			this.btnMerge.UseVisualStyleBackColor = true;
			this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
			// 
			// lblSplit
			// 
			this.lblSplit.Font = new System.Drawing.Font("American Text", 15.75F);
			this.lblSplit.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblSplit.Location = new System.Drawing.Point(252, 130);
			this.lblSplit.Name = "lblSplit";
			this.lblSplit.Size = new System.Drawing.Size(116, 24);
			this.lblSplit.TabIndex = 74;
			this.lblSplit.Text = "Split-O-Rama";
			this.lblSplit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblSplit.Click += new System.EventHandler(this.lblSplit_Click);
			// 
			// btnSplit
			// 
			this.btnSplit.Image = ((System.Drawing.Image)(resources.GetObject("btnSplit.Image")));
			this.btnSplit.Location = new System.Drawing.Point(252, 10);
			this.btnSplit.Name = "btnSplit";
			this.btnSplit.Size = new System.Drawing.Size(116, 117);
			this.btnSplit.TabIndex = 73;
			this.tipTool.SetToolTip(this.btnSplit, "Split selected tracks, groups, and channels into a new sequence.\r\n");
			this.btnSplit.UseVisualStyleBackColor = true;
			this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
			// 
			// lblChannel
			// 
			this.lblChannel.Font = new System.Drawing.Font("American Text", 15.75F);
			this.lblChannel.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblChannel.Location = new System.Drawing.Point(373, 130);
			this.lblChannel.Name = "lblChannel";
			this.lblChannel.Size = new System.Drawing.Size(116, 24);
			this.lblChannel.TabIndex = 76;
			this.lblChannel.Text = "Channel-O-Rama";
			this.lblChannel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblChannel.Click += new System.EventHandler(this.lblChannel_Click);
			// 
			// btnChannel
			// 
			this.btnChannel.Image = ((System.Drawing.Image)(resources.GetObject("btnChannel.Image")));
			this.btnChannel.Location = new System.Drawing.Point(373, 10);
			this.btnChannel.Name = "btnChannel";
			this.btnChannel.Size = new System.Drawing.Size(116, 117);
			this.btnChannel.TabIndex = 75;
			this.tipTool.SetToolTip(this.btnChannel, "Channel Manager- Keep track of all channels and information about them and create" +
        " a spreadsheet or generate reports.\r\n");
			this.btnChannel.UseVisualStyleBackColor = true;
			this.btnChannel.Click += new System.EventHandler(this.btnChannel_Click);
			// 
			// blbVamp
			// 
			this.blbVamp.Font = new System.Drawing.Font("American Text", 15.75F);
			this.blbVamp.ForeColor = System.Drawing.Color.MediumBlue;
			this.blbVamp.Location = new System.Drawing.Point(10, 283);
			this.blbVamp.Name = "blbVamp";
			this.blbVamp.Size = new System.Drawing.Size(116, 24);
			this.blbVamp.TabIndex = 78;
			this.blbVamp.Text = "Vamp-O-Rama";
			this.blbVamp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.blbVamp.Click += new System.EventHandler(this.blbVamp_Click);
			// 
			// btnVamp
			// 
			this.btnVamp.Image = ((System.Drawing.Image)(resources.GetObject("btnVamp.Image")));
			this.btnVamp.Location = new System.Drawing.Point(10, 163);
			this.btnVamp.Name = "btnVamp";
			this.btnVamp.Size = new System.Drawing.Size(116, 117);
			this.btnVamp.TabIndex = 77;
			this.tipTool.SetToolTip(this.btnVamp, "Use the Queen Mary and other Vamp plugins to generate timing tracks and channels " +
        "for beats, notes, pitch, and other metrics.");
			this.btnVamp.UseVisualStyleBackColor = true;
			this.btnVamp.Click += new System.EventHandler(this.btnVamp_Click);
			// 
			// lblCompare
			// 
			this.lblCompare.Font = new System.Drawing.Font("American Text", 15.75F);
			this.lblCompare.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblCompare.Location = new System.Drawing.Point(131, 283);
			this.lblCompare.Name = "lblCompare";
			this.lblCompare.Size = new System.Drawing.Size(116, 24);
			this.lblCompare.TabIndex = 80;
			this.lblCompare.Text = "Compare-O-Rama";
			this.lblCompare.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblCompare.Click += new System.EventHandler(this.lblCompare_Click);
			// 
			// btnCompare
			// 
			this.btnCompare.Image = ((System.Drawing.Image)(resources.GetObject("btnCompare.Image")));
			this.btnCompare.Location = new System.Drawing.Point(131, 163);
			this.btnCompare.Name = "btnCompare";
			this.btnCompare.Size = new System.Drawing.Size(116, 117);
			this.btnCompare.TabIndex = 79;
			this.tipTool.SetToolTip(this.btnCompare, "Compare channels in your LOR sequence, visualizer, and xLights");
			this.btnCompare.UseVisualStyleBackColor = true;
			this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
			// 
			// lblTime
			// 
			this.lblTime.Font = new System.Drawing.Font("American Text", 15.75F);
			this.lblTime.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblTime.Location = new System.Drawing.Point(252, 283);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(116, 24);
			this.lblTime.TabIndex = 82;
			this.lblTime.Text = "Time-O-Rama";
			this.lblTime.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblTime.Click += new System.EventHandler(this.lblTime_Click);
			// 
			// btnTime
			// 
			this.btnTime.Image = ((System.Drawing.Image)(resources.GetObject("btnTime.Image")));
			this.btnTime.Location = new System.Drawing.Point(252, 163);
			this.btnTime.Name = "btnTime";
			this.btnTime.Size = new System.Drawing.Size(116, 117);
			this.btnTime.TabIndex = 81;
			this.tipTool.SetToolTip(this.btnTime, "Convert timing tracks to channels and vise-versa, including xLights");
			this.btnTime.UseVisualStyleBackColor = true;
			this.btnTime.Click += new System.EventHandler(this.btnTime_Click);
			// 
			// lblInfo
			// 
			this.lblInfo.Font = new System.Drawing.Font("American Text", 15.75F);
			this.lblInfo.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblInfo.Location = new System.Drawing.Point(373, 283);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(116, 24);
			this.lblInfo.TabIndex = 84;
			this.lblInfo.Text = "Info-O-Rama";
			this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblInfo.Click += new System.EventHandler(this.lblInfo_Click);
			// 
			// btnInfo
			// 
			this.btnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInfo.Image")));
			this.btnInfo.Location = new System.Drawing.Point(373, 163);
			this.btnInfo.Name = "btnInfo";
			this.btnInfo.Size = new System.Drawing.Size(116, 117);
			this.btnInfo.TabIndex = 83;
			this.tipTool.SetToolTip(this.btnInfo, "Generates reports about sequences with statistics and common errors.");
			this.btnInfo.UseVisualStyleBackColor = true;
			this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
			// 
			// lblRGB
			// 
			this.lblRGB.Font = new System.Drawing.Font("American Text", 15.75F);
			this.lblRGB.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblRGB.Location = new System.Drawing.Point(10, 436);
			this.lblRGB.Name = "lblRGB";
			this.lblRGB.Size = new System.Drawing.Size(116, 24);
			this.lblRGB.TabIndex = 86;
			this.lblRGB.Text = "RGB-O-Rama";
			this.lblRGB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblRGB.Click += new System.EventHandler(this.lblRGB_Click);
			// 
			// btnRGB
			// 
			this.btnRGB.Image = ((System.Drawing.Image)(resources.GetObject("btnRGB.Image")));
			this.btnRGB.Location = new System.Drawing.Point(10, 316);
			this.btnRGB.Name = "btnRGB";
			this.btnRGB.Size = new System.Drawing.Size(116, 117);
			this.btnRGB.TabIndex = 85;
			this.tipTool.SetToolTip(this.btnRGB, "RGB Color Changer with global search-and-replace.");
			this.btnRGB.UseVisualStyleBackColor = true;
			this.btnRGB.Click += new System.EventHandler(this.btnRGB_Click);
			// 
			// lblSparkle
			// 
			this.lblSparkle.Font = new System.Drawing.Font("American Text", 15.75F);
			this.lblSparkle.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblSparkle.Location = new System.Drawing.Point(131, 436);
			this.lblSparkle.Name = "lblSparkle";
			this.lblSparkle.Size = new System.Drawing.Size(116, 24);
			this.lblSparkle.TabIndex = 88;
			this.lblSparkle.Text = "Sparkle-O-Rama";
			this.lblSparkle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblSparkle.Click += new System.EventHandler(this.lblSparkle_Click);
			// 
			// btnSparkle
			// 
			this.btnSparkle.Image = ((System.Drawing.Image)(resources.GetObject("btnSparkle.Image")));
			this.btnSparkle.Location = new System.Drawing.Point(131, 316);
			this.btnSparkle.Name = "btnSparkle";
			this.btnSparkle.Size = new System.Drawing.Size(116, 117);
			this.btnSparkle.TabIndex = 87;
			this.tipTool.SetToolTip(this.btnSparkle, "Creates random sparkle effects with tunable parameters across multiple channels.");
			this.btnSparkle.UseVisualStyleBackColor = true;
			this.btnSparkle.Click += new System.EventHandler(this.btnSparkle_Click);
			// 
			// lblDim
			// 
			this.lblDim.Font = new System.Drawing.Font("American Text", 15.75F);
			this.lblDim.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblDim.Location = new System.Drawing.Point(252, 436);
			this.lblDim.Name = "lblDim";
			this.lblDim.Size = new System.Drawing.Size(116, 24);
			this.lblDim.TabIndex = 90;
			this.lblDim.Text = "Dim-O-Rama";
			this.lblDim.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblDim.Click += new System.EventHandler(this.lblDim_Click);
			// 
			// btnDim
			// 
			this.btnDim.Image = ((System.Drawing.Image)(resources.GetObject("btnDim.Image")));
			this.btnDim.Location = new System.Drawing.Point(252, 316);
			this.btnDim.Name = "btnDim";
			this.btnDim.Size = new System.Drawing.Size(116, 117);
			this.btnDim.TabIndex = 89;
			this.tipTool.SetToolTip(this.btnDim, "Dim, trim, scale, and truncate channel brightness.");
			this.btnDim.UseVisualStyleBackColor = true;
			this.btnDim.Click += new System.EventHandler(this.btnDim_Click);
			// 
			// lblLight
			// 
			this.lblLight.Font = new System.Drawing.Font("American Text", 15.75F);
			this.lblLight.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblLight.Location = new System.Drawing.Point(373, 436);
			this.lblLight.Name = "lblLight";
			this.lblLight.Size = new System.Drawing.Size(116, 24);
			this.lblLight.TabIndex = 92;
			this.lblLight.Text = "Light-O-Rama";
			this.lblLight.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblLight.Click += new System.EventHandler(this.lblLight_Click);
			// 
			// btnLight
			// 
			this.btnLight.Image = ((System.Drawing.Image)(resources.GetObject("btnLight.Image")));
			this.btnLight.Location = new System.Drawing.Point(373, 316);
			this.btnLight.Name = "btnLight";
			this.btnLight.Size = new System.Drawing.Size(116, 117);
			this.btnLight.TabIndex = 91;
			this.tipTool.SetToolTip(this.btnLight, "Light-O-Rama Showtime Sequence Editor");
			this.btnLight.UseVisualStyleBackColor = true;
			this.btnLight.Click += new System.EventHandler(this.btnLight_Click);
			// 
			// frmLaunch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(498, 487);
			this.Controls.Add(this.lblLight);
			this.Controls.Add(this.btnLight);
			this.Controls.Add(this.lblDim);
			this.Controls.Add(this.btnDim);
			this.Controls.Add(this.lblSparkle);
			this.Controls.Add(this.btnSparkle);
			this.Controls.Add(this.lblRGB);
			this.Controls.Add(this.btnRGB);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.btnInfo);
			this.Controls.Add(this.lblTime);
			this.Controls.Add(this.btnTime);
			this.Controls.Add(this.lblCompare);
			this.Controls.Add(this.btnCompare);
			this.Controls.Add(this.blbVamp);
			this.Controls.Add(this.btnVamp);
			this.Controls.Add(this.lblChannel);
			this.Controls.Add(this.btnChannel);
			this.Controls.Add(this.lblSplit);
			this.Controls.Add(this.btnSplit);
			this.Controls.Add(this.lblMerge);
			this.Controls.Add(this.btnMerge);
			this.Controls.Add(this.lblMap);
			this.Controls.Add(this.btnMap);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.picAboutIcon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmLaunch";
			this.Text = "Util-O-Rama Launcher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLaunch_FormClosing);
			this.Load += new System.EventHandler(this.frmLaunch_Load);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private System.Windows.Forms.Button btnMap;
		private System.Windows.Forms.Label lblMap;
		private System.Windows.Forms.Label lblMerge;
		private System.Windows.Forms.Button btnMerge;
		private System.Windows.Forms.Label lblSplit;
		private System.Windows.Forms.Button btnSplit;
		private System.Windows.Forms.Label lblChannel;
		private System.Windows.Forms.Button btnChannel;
		private System.Windows.Forms.Label blbVamp;
		private System.Windows.Forms.Button btnVamp;
		private System.Windows.Forms.Label lblCompare;
		private System.Windows.Forms.Button btnCompare;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.Button btnTime;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.Button btnInfo;
		private System.Windows.Forms.Label lblRGB;
		private System.Windows.Forms.Button btnRGB;
		private System.Windows.Forms.Label lblSparkle;
		private System.Windows.Forms.Button btnSparkle;
		private System.Windows.Forms.Label lblDim;
		private System.Windows.Forms.Button btnDim;
		private System.Windows.Forms.Label lblLight;
		private System.Windows.Forms.Button btnLight;
		private System.Windows.Forms.ToolTip tipTool;
	}
}

