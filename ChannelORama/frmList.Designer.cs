
namespace UtilORama4
{
	partial class frmList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmList));
			Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnOK = new System.Windows.Forms.Button();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.btnUniverse = new System.Windows.Forms.Button();
			this.btnController = new System.Windows.Forms.Button();
			this.btnChannel = new System.Windows.Forms.Button();
			this.btnReport = new System.Windows.Forms.Button();
			this.btnCompareLOR = new System.Windows.Forms.Button();
			this.btnComparex = new System.Windows.Forms.Button();
			this.btnFind = new System.Windows.Forms.Button();
			this.btnWiz = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.toolTips = new System.Windows.Forms.ToolTip(this.components);
			this.btnExportSeq = new System.Windows.Forms.Button();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.treeChannels = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.staStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeChannels)).BeginInit();
			this.SuspendLayout();
			// 
			// staStatus
			// 
			this.staStatus.AllowDrop = true;
			this.staStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlHelp,
            this.pnlStatus,
            this.pnlAbout});
			this.staStatus.Location = new System.Drawing.Point(0, 451);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(489, 24);
			this.staStatus.TabIndex = 8;
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
			this.pnlHelp.ToolTipText = "Get Help using Chan-O-Rama Channel Manager";
			this.pnlHelp.Click += new System.EventHandler(this.pnlHelp_Click);
			// 
			// pnlStatus
			// 
			this.pnlStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pnlStatus.Name = "pnlStatus";
			this.pnlStatus.Size = new System.Drawing.Size(377, 19);
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
			this.pnlAbout.ToolTipText = "About Chan-O-Rama Channel Manager\r\nVersion, Copyright, License, More...";
			this.pnlAbout.Click += new System.EventHandler(this.pnlAbout_Click);
			// 
			// btnOK
			// 
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(393, 424);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Visible = false;
			// 
			// picAboutIcon
			// 
			this.picAboutIcon.ErrorImage = null;
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(419, 355);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 68;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// btnUniverse
			// 
			this.btnUniverse.Image = ((System.Drawing.Image)(resources.GetObject("btnUniverse.Image")));
			this.btnUniverse.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnUniverse.Location = new System.Drawing.Point(405, 12);
			this.btnUniverse.Name = "btnUniverse";
			this.btnUniverse.Size = new System.Drawing.Size(75, 40);
			this.btnUniverse.TabIndex = 1;
			this.btnUniverse.Text = "Add\r\nUniverse\r\n";
			this.btnUniverse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTips.SetToolTip(this.btnUniverse, "Add a new Universe");
			this.btnUniverse.UseVisualStyleBackColor = true;
			this.btnUniverse.Click += new System.EventHandler(this.btnUniverse_Click);
			// 
			// btnController
			// 
			this.btnController.Enabled = false;
			this.btnController.Image = ((System.Drawing.Image)(resources.GetObject("btnController.Image")));
			this.btnController.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnController.Location = new System.Drawing.Point(393, 58);
			this.btnController.Name = "btnController";
			this.btnController.Size = new System.Drawing.Size(75, 40);
			this.btnController.TabIndex = 2;
			this.btnController.Text = "Add\r\nController";
			this.btnController.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTips.SetToolTip(this.btnController, "Add a new Controller to the current Universe");
			this.btnController.UseVisualStyleBackColor = true;
			this.btnController.Click += new System.EventHandler(this.btnController_Click);
			// 
			// btnChannel
			// 
			this.btnChannel.Enabled = false;
			this.btnChannel.Image = ((System.Drawing.Image)(resources.GetObject("btnChannel.Image")));
			this.btnChannel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnChannel.Location = new System.Drawing.Point(393, 104);
			this.btnChannel.Name = "btnChannel";
			this.btnChannel.Size = new System.Drawing.Size(75, 40);
			this.btnChannel.TabIndex = 3;
			this.btnChannel.Text = "Add\r\nChannel\r\n";
			this.btnChannel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTips.SetToolTip(this.btnChannel, "Add a new Channel to the current Controller");
			this.btnChannel.UseVisualStyleBackColor = true;
			this.btnChannel.Click += new System.EventHandler(this.btnChannel_Click);
			// 
			// btnReport
			// 
			this.btnReport.Enabled = false;
			this.btnReport.Image = ((System.Drawing.Image)(resources.GetObject("btnReport.Image")));
			this.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnReport.Location = new System.Drawing.Point(393, 196);
			this.btnReport.Name = "btnReport";
			this.btnReport.Size = new System.Drawing.Size(75, 40);
			this.btnReport.TabIndex = 5;
			this.btnReport.Text = "Export\r\nCSV";
			this.btnReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTips.SetToolTip(this.btnReport, "Export everything to a .CSV Spreadsheet");
			this.btnReport.UseVisualStyleBackColor = true;
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			// 
			// btnCompareLOR
			// 
			this.btnCompareLOR.Enabled = false;
			this.btnCompareLOR.Location = new System.Drawing.Point(438, 264);
			this.btnCompareLOR.Name = "btnCompareLOR";
			this.btnCompareLOR.Size = new System.Drawing.Size(75, 40);
			this.btnCompareLOR.TabIndex = 9;
			this.btnCompareLOR.Text = "Compare\r\nLOR Seq\r\n";
			this.btnCompareLOR.UseVisualStyleBackColor = true;
			this.btnCompareLOR.Visible = false;
			// 
			// btnComparex
			// 
			this.btnComparex.Enabled = false;
			this.btnComparex.Location = new System.Drawing.Point(438, 288);
			this.btnComparex.Name = "btnComparex";
			this.btnComparex.Size = new System.Drawing.Size(75, 40);
			this.btnComparex.TabIndex = 10;
			this.btnComparex.Text = "Compare\r\nxLights\r\n";
			this.btnComparex.UseVisualStyleBackColor = true;
			this.btnComparex.Visible = false;
			// 
			// btnFind
			// 
			this.btnFind.Enabled = false;
			this.btnFind.Image = ((System.Drawing.Image)(resources.GetObject("btnFind.Image")));
			this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFind.Location = new System.Drawing.Point(393, 150);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(75, 40);
			this.btnFind.TabIndex = 4;
			this.btnFind.Text = "Find...";
			this.btnFind.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTips.SetToolTip(this.btnFind, "Find a Channel...");
			this.btnFind.UseVisualStyleBackColor = true;
			// 
			// btnWiz
			// 
			this.btnWiz.Image = ((System.Drawing.Image)(resources.GetObject("btnWiz.Image")));
			this.btnWiz.Location = new System.Drawing.Point(393, 242);
			this.btnWiz.Name = "btnWiz";
			this.btnWiz.Size = new System.Drawing.Size(75, 40);
			this.btnWiz.TabIndex = 6;
			this.toolTips.SetToolTip(this.btnWiz, "Channel Comparison Wizard\r\n\r\nCompare Managed Channels to:\r\n    Light-O-Rama Showt" +
        "ime S4 Channels\r\n    Light-O-Rama Visualizer Channels & Groups\r\n    xLights Mode" +
        "ls and Groups");
			this.btnWiz.UseVisualStyleBackColor = true;
			this.btnWiz.Visible = false;
			this.btnWiz.Click += new System.EventHandler(this.btnWiz_Click);
			// 
			// btnSave
			// 
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSave.Location = new System.Drawing.Point(393, 378);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 40);
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "Save";
			this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTips.SetToolTip(this.btnSave, "Save Everything!");
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnExportSeq
			// 
			this.btnExportSeq.Enabled = false;
			this.btnExportSeq.Image = ((System.Drawing.Image)(resources.GetObject("btnExportSeq.Image")));
			this.btnExportSeq.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnExportSeq.Location = new System.Drawing.Point(419, 288);
			this.btnExportSeq.Name = "btnExportSeq";
			this.btnExportSeq.Size = new System.Drawing.Size(75, 40);
			this.btnExportSeq.TabIndex = 119;
			this.btnExportSeq.Text = "Export\r\nSequence";
			this.btnExportSeq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTips.SetToolTip(this.btnExportSeq, "Export everything to a .CSV Spreadsheet");
			this.btnExportSeq.UseVisualStyleBackColor = true;
			this.btnExportSeq.Visible = false;
			// 
			// imlTreeIcons
			// 
			this.imlTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeIcons.ImageStream")));
			this.imlTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlTreeIcons.Images.SetKeyName(0, "Universe");
			this.imlTreeIcons.Images.SetKeyName(1, "Controller");
			this.imlTreeIcons.Images.SetKeyName(2, "Track");
			this.imlTreeIcons.Images.SetKeyName(3, "Channel");
			this.imlTreeIcons.Images.SetKeyName(4, "RGBChannel");
			this.imlTreeIcons.Images.SetKeyName(5, "ChannelGroup");
			this.imlTreeIcons.Images.SetKeyName(6, "CosmicDevice");
			this.imlTreeIcons.Images.SetKeyName(7, "#400000");
			this.imlTreeIcons.Images.SetKeyName(8, "#804040");
			this.imlTreeIcons.Images.SetKeyName(9, "#FF0000");
			this.imlTreeIcons.Images.SetKeyName(10, "#00FF00");
			this.imlTreeIcons.Images.SetKeyName(11, "#0000FF");
			this.imlTreeIcons.Images.SetKeyName(12, "#FFFFFF");
			this.imlTreeIcons.Images.SetKeyName(13, "#000000");
			this.imlTreeIcons.Images.SetKeyName(14, "#8000FF");
			this.imlTreeIcons.Images.SetKeyName(15, "#FF8000");
			this.imlTreeIcons.Images.SetKeyName(16, "#FFFF00");
			this.imlTreeIcons.Images.SetKeyName(17, "#FF80FF");
			this.imlTreeIcons.Images.SetKeyName(18, "#00FFFF");
			this.imlTreeIcons.Images.SetKeyName(19, "#000080");
			this.imlTreeIcons.Images.SetKeyName(20, "#008000");
			this.imlTreeIcons.Images.SetKeyName(21, "#008080");
			this.imlTreeIcons.Images.SetKeyName(22, "#8080FF");
			this.imlTreeIcons.Images.SetKeyName(23, "#400080");
			this.imlTreeIcons.Images.SetKeyName(24, "#404040");
			this.imlTreeIcons.Images.SetKeyName(25, "#408080");
			this.imlTreeIcons.Images.SetKeyName(26, "#800000");
			this.imlTreeIcons.Images.SetKeyName(27, "#800080");
			this.imlTreeIcons.Images.SetKeyName(28, "#808000");
			this.imlTreeIcons.Images.SetKeyName(29, "#808080");
			this.imlTreeIcons.Images.SetKeyName(30, "#C0C0C0");
			this.imlTreeIcons.Images.SetKeyName(31, "#FF00FF");
			// 
			// treeChannels
			// 
			this.treeChannels.BackColor = System.Drawing.Color.White;
			treeNodeAdvStyleInfo1.CheckBoxTickThickness = 1;
			treeNodeAdvStyleInfo1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
			treeNodeAdvStyleInfo1.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			treeNodeAdvStyleInfo1.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
			treeNodeAdvStyleInfo1.TextColor = System.Drawing.Color.Black;
			this.treeChannels.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
			this.treeChannels.BeforeTouchSize = new System.Drawing.Size(360, 423);
			this.treeChannels.ForeColor = System.Drawing.Color.Black;
			// 
			// 
			// 
			this.treeChannels.HelpTextControl.BaseThemeName = null;
			this.treeChannels.HelpTextControl.Location = new System.Drawing.Point(0, 0);
			this.treeChannels.HelpTextControl.Name = "";
			this.treeChannels.HelpTextControl.Size = new System.Drawing.Size(392, 112);
			this.treeChannels.HelpTextControl.TabIndex = 0;
			this.treeChannels.HelpTextControl.Visible = true;
			this.treeChannels.InactiveSelectedNodeForeColor = System.Drawing.SystemColors.ControlText;
			this.treeChannels.LeftImageList = this.imlTreeIcons;
			this.treeChannels.Location = new System.Drawing.Point(12, 12);
			this.treeChannels.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
			this.treeChannels.Name = "treeChannels";
			this.treeChannels.SelectedNodeForeColor = System.Drawing.SystemColors.HighlightText;
			this.treeChannels.Size = new System.Drawing.Size(360, 423);
			this.treeChannels.TabIndex = 118;
			this.treeChannels.Text = "Source Channels";
			this.treeChannels.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 0;
			this.treeChannels.ThemeStyle.TreeNodeAdvStyle.EnsureDefaultOptionedChild = true;
			// 
			// 
			// 
			this.treeChannels.ToolTipControl.BaseThemeName = null;
			this.treeChannels.ToolTipControl.Location = new System.Drawing.Point(0, 0);
			this.treeChannels.ToolTipControl.Name = "";
			this.treeChannels.ToolTipControl.Size = new System.Drawing.Size(392, 112);
			this.treeChannels.ToolTipControl.TabIndex = 0;
			this.treeChannels.ToolTipControl.Visible = true;
			this.treeChannels.AfterSelect += new System.EventHandler(this.treeChannels_AfterSelect);
			this.treeChannels.DoubleClick += new System.EventHandler(this.treeChannelList_DoubleClick);
			this.treeChannels.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeChannels_KeyPress);
			this.treeChannels.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeChannels_KeyUp);
			// 
			// dlgFileOpen
			// 
			this.dlgFileOpen.FileName = "openFileDialog1";
			// 
			// frmList
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(489, 475);
			this.Controls.Add(this.btnExportSeq);
			this.Controls.Add(this.treeChannels);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnWiz);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.btnComparex);
			this.Controls.Add(this.btnCompareLOR);
			this.Controls.Add(this.btnReport);
			this.Controls.Add(this.btnChannel);
			this.Controls.Add(this.btnController);
			this.Controls.Add(this.btnUniverse);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.picAboutIcon);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(800, 950);
			this.MinimumSize = new System.Drawing.Size(350, 400);
			this.Name = "frmList";
			this.Text = "Chan-O-Rama  Channel Manager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmList_FormClosing);
			this.Load += new System.EventHandler(this.frmList_Load);
			this.Shown += new System.EventHandler(this.frmList_Shown);
			this.Click += new System.EventHandler(this.frmList_Click);
			this.Resize += new System.EventHandler(this.frmList_Resize);
			this.staStatus.ResumeLayout(false);
			this.staStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAboutIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeChannels)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip staStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlHelp;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private System.Windows.Forms.Button btnUniverse;
		private System.Windows.Forms.Button btnController;
		private System.Windows.Forms.Button btnChannel;
		private System.Windows.Forms.Button btnReport;
		private System.Windows.Forms.Button btnCompareLOR;
		private System.Windows.Forms.Button btnComparex;
		private System.Windows.Forms.Button btnFind;
		private System.Windows.Forms.Button btnWiz;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ToolTip toolTips;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeChannels;
		private System.Windows.Forms.Button btnExportSeq;
		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
	}
}

