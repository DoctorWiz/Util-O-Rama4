namespace UtilORama4
{
	partial class Form1
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
			this.btnBrowseOpen = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnSaveAs = new System.Windows.Forms.Button();
			this.lblStaus = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// btnBrowseOpen
			// 
			this.btnBrowseOpen.Location = new System.Drawing.Point(394, 61);
			this.btnBrowseOpen.Name = "btnBrowseOpen";
			this.btnBrowseOpen.Size = new System.Drawing.Size(65, 27);
			this.btnBrowseOpen.TabIndex = 0;
			this.btnBrowseOpen.Text = "...";
			this.btnBrowseOpen.UseVisualStyleBackColor = true;
			this.btnBrowseOpen.Click += new System.EventHandler(this.btnBrowseOpen_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(55, 68);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(333, 20);
			this.textBox1.TabIndex = 1;
			// 
			// btnSaveAs
			// 
			this.btnSaveAs.Location = new System.Drawing.Point(241, 281);
			this.btnSaveAs.Name = "btnSaveAs";
			this.btnSaveAs.Size = new System.Drawing.Size(185, 56);
			this.btnSaveAs.TabIndex = 2;
			this.btnSaveAs.Text = "Save Fixed As...";
			this.btnSaveAs.UseVisualStyleBackColor = true;
			this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
			// 
			// lblStaus
			// 
			this.lblStaus.AutoSize = true;
			this.lblStaus.Location = new System.Drawing.Point(16, 157);
			this.lblStaus.Name = "lblStaus";
			this.lblStaus.Size = new System.Drawing.Size(120, 13);
			this.lblStaus.TabIndex = 3;
			this.lblStaus.Text = "Choose a file to be fixed";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(52, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "File to be fixed";
			// 
			// dlgOpen
			// 
			this.dlgOpen.FileName = "openFileDialog1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblStaus);
			this.Controls.Add(this.btnSaveAs);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnBrowseOpen);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnBrowseOpen;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnSaveAs;
		private System.Windows.Forms.Label lblStaus;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.OpenFileDialog dlgOpen;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
	}
}

