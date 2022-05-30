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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(237, 519);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 18;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(156, 519);
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
			this.imlTreeIcons.Images.SetKeyName(0, "LOR4Track");
			this.imlTreeIcons.Images.SetKeyName(1, "LOR4ChannelGroup");
			this.imlTreeIcons.Images.SetKeyName(2, "LOR4RGBChannel");
			this.imlTreeIcons.Images.SetKeyName(3, "LOR4Channel");
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
			this.btnInvert.Location = new System.Drawing.Point(124, 451);
			this.btnInvert.Name = "btnInvert";
			this.btnInvert.Size = new System.Drawing.Size(75, 23);
			this.btnInvert.TabIndex = 39;
			this.btnInvert.Text = "Invert";
			this.btnInvert.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 451);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 40;
			this.button1.Text = "Clear";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(238, 451);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 41;
			this.button2.Text = "All";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(181, 480);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 42;
			this.button3.Text = "Save...";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(65, 480);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 43;
			this.button4.Text = "Load...";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Location = new System.Drawing.Point(13, 509);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(300, 4);
			this.panel1.TabIndex = 44;
			// 
			// frmChannels
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(325, 552);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
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
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Panel panel1;
	}
}