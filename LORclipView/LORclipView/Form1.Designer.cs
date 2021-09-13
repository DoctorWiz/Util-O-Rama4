namespace LORclipView
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
			this.btnPaste = new System.Windows.Forms.Button();
			this.txtData = new System.Windows.Forms.TextBox();
			this.txtInfo = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnPaste
			// 
			this.btnPaste.Location = new System.Drawing.Point(68, 51);
			this.btnPaste.Name = "btnPaste";
			this.btnPaste.Size = new System.Drawing.Size(142, 46);
			this.btnPaste.TabIndex = 0;
			this.btnPaste.Text = "Paste";
			this.btnPaste.UseVisualStyleBackColor = true;
			this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
			// 
			// txtData
			// 
			this.txtData.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtData.Location = new System.Drawing.Point(64, 250);
			this.txtData.Multiline = true;
			this.txtData.Name = "txtData";
			this.txtData.Size = new System.Drawing.Size(706, 167);
			this.txtData.TabIndex = 1;
			// 
			// txtInfo
			// 
			this.txtInfo.Font = new System.Drawing.Font("DejaVu Sans Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtInfo.Location = new System.Drawing.Point(64, 142);
			this.txtInfo.Multiline = true;
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.Size = new System.Drawing.Size(706, 102);
			this.txtInfo.TabIndex = 2;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.txtInfo);
			this.Controls.Add(this.txtData);
			this.Controls.Add(this.btnPaste);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnPaste;
		private System.Windows.Forms.TextBox txtData;
		private System.Windows.Forms.TextBox txtInfo;
	}
}

