
namespace UtilORama4
{
	partial class frmOutputLog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOutputLog));
			this.btnClose = new System.Windows.Forms.Button();
			this.btnSaveLog = new System.Windows.Forms.Button();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.lblDebug = new System.Windows.Forms.Label();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Enabled = false;
			this.btnClose.Location = new System.Drawing.Point(208, 377);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnSaveLog
			// 
			this.btnSaveLog.Enabled = false;
			this.btnSaveLog.Location = new System.Drawing.Point(315, 377);
			this.btnSaveLog.Name = "btnSaveLog";
			this.btnSaveLog.Size = new System.Drawing.Size(75, 23);
			this.btnSaveLog.TabIndex = 1;
			this.btnSaveLog.Text = "Save As...";
			this.btnSaveLog.UseVisualStyleBackColor = true;
			this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
			// 
			// txtOutput
			// 
			this.txtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.txtOutput.CausesValidation = false;
			this.txtOutput.Font = new System.Drawing.Font("DejaVu Sans Mono", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOutput.ForeColor = System.Drawing.Color.LawnGreen;
			this.txtOutput.Location = new System.Drawing.Point(3, 3);
			this.txtOutput.Multiline = true;
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtOutput.Size = new System.Drawing.Size(591, 368);
			this.txtOutput.TabIndex = 139;
			// 
			// lblDebug
			// 
			this.lblDebug.AutoSize = true;
			this.lblDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDebug.ForeColor = System.Drawing.Color.DarkViolet;
			this.lblDebug.Location = new System.Drawing.Point(404, 382);
			this.lblDebug.Name = "lblDebug";
			this.lblDebug.Size = new System.Drawing.Size(68, 13);
			this.lblDebug.TabIndex = 140;
			this.lblDebug.Text = "Debug info...";
			// 
			// frmOutputLog
			// 
			this.AcceptButton = this.btnClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(598, 411);
			this.Controls.Add(this.lblDebug);
			this.Controls.Add(this.txtOutput);
			this.Controls.Add(this.btnSaveLog);
			this.Controls.Add(this.btnClose);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(614, 1040);
			this.MinimumSize = new System.Drawing.Size(614, 400);
			this.Name = "frmOutputLog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Sonic Annotator Output Log";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOutputLog_FormClosing);
			this.Load += new System.EventHandler(this.frmOutputLog_Load);
			this.Shown += new System.EventHandler(this.frmOutputLog_Shown);
			this.ResizeEnd += new System.EventHandler(this.frmOutputLog_ResizeEnd);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmOutputLog_Paint);
			this.Move += new System.EventHandler(this.frmOutputLog_Move);
			this.Resize += new System.EventHandler(this.frmOutputLog_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnSaveLog;
		private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.Label lblDebug;
		public System.Windows.Forms.SaveFileDialog dlgFileSave;
	}
}