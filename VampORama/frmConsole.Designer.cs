namespace Vamperizer
{
	partial class frmConsole
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
			this.logConsole = new System.Windows.Forms.ListBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// logConsole
			// 
			this.logConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.logConsole.Font = new System.Drawing.Font("DejaVu Sans Mono", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.logConsole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.logConsole.FormattingEnabled = true;
			this.logConsole.HorizontalScrollbar = true;
			this.logConsole.ItemHeight = 10;
			this.logConsole.Location = new System.Drawing.Point(0, 0);
			this.logConsole.Name = "logConsole";
			this.logConsole.Size = new System.Drawing.Size(788, 394);
			this.logConsole.TabIndex = 160;
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(422, 404);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(77, 26);
			this.btnClose.TabIndex = 161;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// frmConsole
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.ControlBox = false;
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.logConsole);
			this.MaximizeBox = false;
			this.Name = "frmConsole";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Vamperizing";
			this.SizeChanged += new System.EventHandler(this.frmConsole_SizeChanged);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.ListBox logConsole;
		public System.Windows.Forms.Button btnClose;
	}
}