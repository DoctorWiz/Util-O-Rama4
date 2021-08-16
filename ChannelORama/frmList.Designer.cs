
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
			this.staStatus = new System.Windows.Forms.StatusStrip();
			this.pnlHelp = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnOK = new System.Windows.Forms.Button();
			this.picAboutIcon = new System.Windows.Forms.PictureBox();
			this.treeChannelList = new System.Windows.Forms.TreeView();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.btnUniverse = new System.Windows.Forms.Button();
			this.btnController = new System.Windows.Forms.Button();
			this.btnChannel = new System.Windows.Forms.Button();
			this.btnReport = new System.Windows.Forms.Button();
			this.btnCompareLOR = new System.Windows.Forms.Button();
			this.btnComparex = new System.Windows.Forms.Button();
			this.btnFind = new System.Windows.Forms.Button();
			this.btnWiz = new System.Windows.Forms.Button();
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
			this.staStatus.Location = new System.Drawing.Point(0, 451);
			this.staStatus.Name = "staStatus";
			this.staStatus.Size = new System.Drawing.Size(489, 24);
			this.staStatus.TabIndex = 9;
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
			this.pnlAbout.Click += new System.EventHandler(this.pnlAbout_Click);
			// 
			// btnOK
			// 
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(393, 424);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Visible = false;
			// 
			// picAboutIcon
			// 
			this.picAboutIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.BackgroundImage")));
			this.picAboutIcon.Image = ((System.Drawing.Image)(resources.GetObject("picAboutIcon.Image")));
			this.picAboutIcon.Location = new System.Drawing.Point(419, 355);
			this.picAboutIcon.Name = "picAboutIcon";
			this.picAboutIcon.Size = new System.Drawing.Size(128, 128);
			this.picAboutIcon.TabIndex = 68;
			this.picAboutIcon.TabStop = false;
			this.picAboutIcon.Visible = false;
			// 
			// treeChannelList
			// 
			this.treeChannelList.ImageIndex = 0;
			this.treeChannelList.ImageList = this.imlTreeIcons;
			this.treeChannelList.Location = new System.Drawing.Point(12, 12);
			this.treeChannelList.Name = "treeChannelList";
			this.treeChannelList.SelectedImageIndex = 0;
			this.treeChannelList.Size = new System.Drawing.Size(375, 406);
			this.treeChannelList.StateImageList = this.imlTreeIcons;
			this.treeChannelList.TabIndex = 1;
			this.treeChannelList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeChannelList_AfterSelect);
			this.treeChannelList.DoubleClick += new System.EventHandler(this.treeChannelList_DoubleClick);
			this.treeChannelList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeChannelList_KeyPress);
			this.treeChannelList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeChannelList_KeyUp);
			// 
			// imlTreeIcons
			// 
			this.imlTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeIcons.ImageStream")));
			this.imlTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlTreeIcons.Images.SetKeyName(0, "Universe");
			this.imlTreeIcons.Images.SetKeyName(1, "Controller");
			this.imlTreeIcons.Images.SetKeyName(2, "#FF0000");
			this.imlTreeIcons.Images.SetKeyName(3, "#00FF00");
			this.imlTreeIcons.Images.SetKeyName(4, "#0000FF");
			this.imlTreeIcons.Images.SetKeyName(5, "#FFFFFF");
			this.imlTreeIcons.Images.SetKeyName(6, "#000000");
			this.imlTreeIcons.Images.SetKeyName(7, "#FF8000");
			this.imlTreeIcons.Images.SetKeyName(8, "#8000FF");
			this.imlTreeIcons.Images.SetKeyName(9, "#804040");
			this.imlTreeIcons.Images.SetKeyName(10, "#400000");
			// 
			// btnUniverse
			// 
			this.btnUniverse.Location = new System.Drawing.Point(405, 12);
			this.btnUniverse.Name = "btnUniverse";
			this.btnUniverse.Size = new System.Drawing.Size(75, 40);
			this.btnUniverse.TabIndex = 2;
			this.btnUniverse.Text = "Add\r\nUniverse\r\n";
			this.btnUniverse.UseVisualStyleBackColor = true;
			this.btnUniverse.Click += new System.EventHandler(this.btnUniverse_Click);
			// 
			// btnController
			// 
			this.btnController.Enabled = false;
			this.btnController.Location = new System.Drawing.Point(393, 58);
			this.btnController.Name = "btnController";
			this.btnController.Size = new System.Drawing.Size(75, 40);
			this.btnController.TabIndex = 3;
			this.btnController.Text = "Add\r\nController";
			this.btnController.UseVisualStyleBackColor = true;
			this.btnController.Click += new System.EventHandler(this.btnController_Click);
			// 
			// btnChannel
			// 
			this.btnChannel.Enabled = false;
			this.btnChannel.Location = new System.Drawing.Point(393, 104);
			this.btnChannel.Name = "btnChannel";
			this.btnChannel.Size = new System.Drawing.Size(75, 40);
			this.btnChannel.TabIndex = 4;
			this.btnChannel.Text = "Add\r\nChannel\r\n";
			this.btnChannel.UseVisualStyleBackColor = true;
			this.btnChannel.Click += new System.EventHandler(this.btnChannel_Click);
			// 
			// btnReport
			// 
			this.btnReport.Enabled = false;
			this.btnReport.Location = new System.Drawing.Point(393, 150);
			this.btnReport.Name = "btnReport";
			this.btnReport.Size = new System.Drawing.Size(75, 40);
			this.btnReport.TabIndex = 5;
			this.btnReport.Text = "Export\r\nCSV";
			this.btnReport.UseVisualStyleBackColor = true;
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			// 
			// btnCompareLOR
			// 
			this.btnCompareLOR.Enabled = false;
			this.btnCompareLOR.Location = new System.Drawing.Point(393, 196);
			this.btnCompareLOR.Name = "btnCompareLOR";
			this.btnCompareLOR.Size = new System.Drawing.Size(75, 40);
			this.btnCompareLOR.TabIndex = 6;
			this.btnCompareLOR.Text = "Compare\r\nLOR Seq\r\n";
			this.btnCompareLOR.UseVisualStyleBackColor = true;
			// 
			// btnComparex
			// 
			this.btnComparex.Enabled = false;
			this.btnComparex.Location = new System.Drawing.Point(393, 242);
			this.btnComparex.Name = "btnComparex";
			this.btnComparex.Size = new System.Drawing.Size(75, 40);
			this.btnComparex.TabIndex = 7;
			this.btnComparex.Text = "Compare\r\nxLights\r\n";
			this.btnComparex.UseVisualStyleBackColor = true;
			this.btnComparex.Visible = false;
			// 
			// btnFind
			// 
			this.btnFind.Enabled = false;
			this.btnFind.Location = new System.Drawing.Point(393, 288);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(75, 40);
			this.btnFind.TabIndex = 8;
			this.btnFind.Text = "Find...";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Visible = false;
			// 
			// btnWiz
			// 
			this.btnWiz.Image = ((System.Drawing.Image)(resources.GetObject("btnWiz.Image")));
			this.btnWiz.Location = new System.Drawing.Point(393, 334);
			this.btnWiz.Name = "btnWiz";
			this.btnWiz.Size = new System.Drawing.Size(75, 40);
			this.btnWiz.TabIndex = 69;
			this.btnWiz.UseVisualStyleBackColor = true;
			this.btnWiz.Visible = false;
			this.btnWiz.Click += new System.EventHandler(this.btnWiz_Click);
			// 
			// frmList
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(489, 475);
			this.Controls.Add(this.btnWiz);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.btnComparex);
			this.Controls.Add(this.btnCompareLOR);
			this.Controls.Add(this.btnReport);
			this.Controls.Add(this.btnChannel);
			this.Controls.Add(this.btnController);
			this.Controls.Add(this.btnUniverse);
			this.Controls.Add(this.treeChannelList);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.staStatus);
			this.Controls.Add(this.picAboutIcon);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(800, 950);
			this.MinimumSize = new System.Drawing.Size(350, 400);
			this.Name = "frmList";
			this.Text = "Channel-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmList_FormClosing);
			this.Load += new System.EventHandler(this.frmList_Load);
			this.Shown += new System.EventHandler(this.frmList_Shown);
			this.Click += new System.EventHandler(this.frmList_Click);
			this.Resize += new System.EventHandler(this.frmList_Resize);
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
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.PictureBox picAboutIcon;
		private System.Windows.Forms.TreeView treeChannelList;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.Button btnUniverse;
		private System.Windows.Forms.Button btnController;
		private System.Windows.Forms.Button btnChannel;
		private System.Windows.Forms.Button btnReport;
		private System.Windows.Forms.Button btnCompareLOR;
		private System.Windows.Forms.Button btnComparex;
		private System.Windows.Forms.Button btnFind;
		private System.Windows.Forms.Button btnWiz;
	}
}

