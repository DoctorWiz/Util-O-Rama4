namespace RGBORama
{
	partial class frmChannels
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChannels));
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.treeChannels = new System.Windows.Forms.TreeView();
			this.btnInvert = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(237, 464);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 18;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(156, 464);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 17;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// imlTreeIcons
			// 
			this.imlTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeIcons.ImageStream")));
			this.imlTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlTreeIcons.Images.SetKeyName(0, "Track");
			this.imlTreeIcons.Images.SetKeyName(1, "ChannelGroup");
			this.imlTreeIcons.Images.SetKeyName(2, "RGBchannel");
			this.imlTreeIcons.Images.SetKeyName(3, "Channel");
			this.imlTreeIcons.Images.SetKeyName(4, "RedCh");
			this.imlTreeIcons.Images.SetKeyName(5, "GrnCh");
			this.imlTreeIcons.Images.SetKeyName(6, "BluCh");
			// 
			// treeChannels
			// 
			this.treeChannels.AllowDrop = true;
			this.treeChannels.BackColor = System.Drawing.Color.White;
			this.treeChannels.ImageIndex = 0;
			this.treeChannels.ImageList = this.imlTreeIcons;
			this.treeChannels.Location = new System.Drawing.Point(12, 12);
			this.treeChannels.Name = "treeChannels";
			this.treeChannels.SelectedImageIndex = 0;
			this.treeChannels.Size = new System.Drawing.Size(300, 433);
			this.treeChannels.TabIndex = 38;
			// 
			// btnInvert
			// 
			this.btnInvert.Location = new System.Drawing.Point(12, 464);
			this.btnInvert.Name = "btnInvert";
			this.btnInvert.Size = new System.Drawing.Size(75, 23);
			this.btnInvert.TabIndex = 39;
			this.btnInvert.Text = "Invert";
			this.btnInvert.UseVisualStyleBackColor = true;
			// 
			// frmChannels
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(325, 500);
			this.Controls.Add(this.btnInvert);
			this.Controls.Add(this.treeChannels);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmChannels";
			this.ShowInTaskbar = false;
			this.Text = "Select Channels";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmChannels_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.TreeView treeChannels;
		private System.Windows.Forms.Button btnInvert;
	}
}