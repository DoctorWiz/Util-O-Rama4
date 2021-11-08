namespace UtilORama4
{
	partial class frmMapList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapList));
			this.lstMap = new System.Windows.Forms.ListView();
			this.hedSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.hedDest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.button1 = new System.Windows.Forms.Button();
			this.chkUnmapped = new System.Windows.Forms.CheckBox();
			this.imlTreeIcons = new System.Windows.Forms.ImageList(this.components);
			this.lblKey = new System.Windows.Forms.Label();
			this.lblAlpha = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lstMap
			// 
			this.lstMap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hedSource,
            this.hedDest});
			this.lstMap.FullRowSelect = true;
			this.lstMap.GridLines = true;
			this.lstMap.Location = new System.Drawing.Point(27, 29);
			this.lstMap.MultiSelect = false;
			this.lstMap.Name = "lstMap";
			this.lstMap.ShowGroups = false;
			this.lstMap.Size = new System.Drawing.Size(642, 471);
			this.lstMap.TabIndex = 0;
			this.lstMap.UseCompatibleStateImageBehavior = false;
			this.lstMap.View = System.Windows.Forms.View.Details;
			this.lstMap.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstMap_ColumnClick);
			this.lstMap.SelectedIndexChanged += new System.EventHandler(this.lstMap_SelectedIndexChanged);
			// 
			// hedSource
			// 
			this.hedSource.Text = "Source LORChannel4";
			this.hedSource.Width = 319;
			// 
			// hedDest
			// 
			this.hedDest.Text = "Destination LORChannel4";
			this.hedDest.Width = 321;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(290, 561);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(121, 39);
			this.button1.TabIndex = 1;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// chkUnmapped
			// 
			this.chkUnmapped.AutoSize = true;
			this.chkUnmapped.Checked = true;
			this.chkUnmapped.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUnmapped.Location = new System.Drawing.Point(27, 507);
			this.chkUnmapped.Name = "chkUnmapped";
			this.chkUnmapped.Size = new System.Drawing.Size(155, 17);
			this.chkUnmapped.TabIndex = 2;
			this.chkUnmapped.Text = "Show Unmapped Channels";
			this.chkUnmapped.UseVisualStyleBackColor = true;
			this.chkUnmapped.CheckedChanged += new System.EventHandler(this.chkUnmapped_CheckedChanged);
			// 
			// imlTreeIcons
			// 
			this.imlTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeIcons.ImageStream")));
			this.imlTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlTreeIcons.Images.SetKeyName(0, "LORTrack4");
			this.imlTreeIcons.Images.SetKeyName(1, "LORChannelGroup4");
			this.imlTreeIcons.Images.SetKeyName(2, "LORRGBChannel4");
			this.imlTreeIcons.Images.SetKeyName(3, "LORChannel4");
			this.imlTreeIcons.Images.SetKeyName(4, "RedCh");
			this.imlTreeIcons.Images.SetKeyName(5, "GrnCh");
			this.imlTreeIcons.Images.SetKeyName(6, "BluCh");
			// 
			// lblKey
			// 
			this.lblKey.AutoSize = true;
			this.lblKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblKey.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.lblKey.Location = new System.Drawing.Point(37, 547);
			this.lblKey.Name = "lblKey";
			this.lblKey.Size = new System.Drawing.Size(39, 9);
			this.lblKey.TabIndex = 3;
			this.lblKey.Text = "by Source";
			// 
			// lblAlpha
			// 
			this.lblAlpha.AutoSize = true;
			this.lblAlpha.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAlpha.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.lblAlpha.Location = new System.Drawing.Point(37, 556);
			this.lblAlpha.Name = "lblAlpha";
			this.lblAlpha.Size = new System.Drawing.Size(34, 9);
			this.lblAlpha.TabIndex = 4;
			this.lblAlpha.Text = "by Alpha";
			// 
			// frmMapList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(694, 612);
			this.Controls.Add(this.lblAlpha);
			this.Controls.Add(this.lblKey);
			this.Controls.Add(this.chkUnmapped);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lstMap);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMapList";
			this.Text = "LORChannel4 Mapping Summary";
			this.Load += new System.EventHandler(this.frmMapList_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMapList_Paint);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.ListView lstMap;
		private System.Windows.Forms.ColumnHeader hedSource;
		private System.Windows.Forms.ColumnHeader hedDest;
		private System.Windows.Forms.Button button1;
		public System.Windows.Forms.CheckBox chkUnmapped;
		private System.Windows.Forms.ImageList imlTreeIcons;
		private System.Windows.Forms.Label lblKey;
		private System.Windows.Forms.Label lblAlpha;
	}
}