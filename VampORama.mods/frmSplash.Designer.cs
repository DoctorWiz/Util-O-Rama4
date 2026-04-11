namespace UtilORama4
{
	partial class frmSplash
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplash));
			picSplash = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)picSplash).BeginInit();
			SuspendLayout();
			// 
			// picSplash
			// 
			picSplash.Image = (System.Drawing.Image)resources.GetObject("picSplash.Image");
			picSplash.Location = new System.Drawing.Point(0, 0);
			picSplash.Name = "picSplash";
			picSplash.Size = new System.Drawing.Size(300, 248);
			picSplash.TabIndex = 0;
			picSplash.TabStop = false;
			// 
			// frmSplash
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(300, 248);
			Controls.Add(picSplash);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			Name = "frmSplash";
			Text = "Vamp-O-Rama";
			((System.ComponentModel.ISupportInitialize)picSplash).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.PictureBox picSplash;
	}
}