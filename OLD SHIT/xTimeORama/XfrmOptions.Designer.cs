namespace SplitORama
{
	partial class frmOptions
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
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.grpNames = new System.Windows.Forms.GroupBox();
			this.lblFinalMatchScore = new System.Windows.Forms.Label();
			this.lblPreMatchScore = new System.Windows.Forms.Label();
			this.lblFinalMatchSlider = new System.Windows.Forms.Label();
			this.lblPreMatchSlider = new System.Windows.Forms.Label();
			this.sldFinalMatch = new System.Windows.Forms.TrackBar();
			this.sldPreMatch = new System.Windows.Forms.TrackBar();
			this.optFuzzy = new System.Windows.Forms.RadioButton();
			this.optExact = new System.Windows.Forms.RadioButton();
			this.grpSave = new System.Windows.Forms.GroupBox();
			this.optCRGAlpha = new System.Windows.Forms.RadioButton();
			this.optCRGDisplay = new System.Windows.Forms.RadioButton();
			this.optMixedDisplay = new System.Windows.Forms.RadioButton();
			this.grpNames.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldFinalMatch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sldPreMatch)).BeginInit();
			this.grpSave.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(122, 226);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(203, 226);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 14;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// grpNames
			// 
			this.grpNames.Controls.Add(this.lblFinalMatchScore);
			this.grpNames.Controls.Add(this.lblPreMatchScore);
			this.grpNames.Controls.Add(this.lblFinalMatchSlider);
			this.grpNames.Controls.Add(this.lblPreMatchSlider);
			this.grpNames.Controls.Add(this.sldFinalMatch);
			this.grpNames.Controls.Add(this.sldPreMatch);
			this.grpNames.Controls.Add(this.optFuzzy);
			this.grpNames.Controls.Add(this.optExact);
			this.grpNames.Location = new System.Drawing.Point(12, 12);
			this.grpNames.Name = "grpNames";
			this.grpNames.Size = new System.Drawing.Size(266, 195);
			this.grpNames.TabIndex = 1;
			this.grpNames.TabStop = false;
			this.grpNames.Text = " Name Matching... ";
			this.grpNames.Visible = false;
			// 
			// lblFinalMatchScore
			// 
			this.lblFinalMatchScore.AutoSize = true;
			this.lblFinalMatchScore.Location = new System.Drawing.Point(234, 148);
			this.lblFinalMatchScore.Name = "lblFinalMatchScore";
			this.lblFinalMatchScore.Size = new System.Drawing.Size(28, 13);
			this.lblFinalMatchScore.TabIndex = 9;
			this.lblFinalMatchScore.Text = ".950";
			// 
			// lblPreMatchScore
			// 
			this.lblPreMatchScore.AutoSize = true;
			this.lblPreMatchScore.Location = new System.Drawing.Point(234, 89);
			this.lblPreMatchScore.Name = "lblPreMatchScore";
			this.lblPreMatchScore.Size = new System.Drawing.Size(28, 13);
			this.lblPreMatchScore.TabIndex = 6;
			this.lblPreMatchScore.Text = ".800";
			// 
			// lblFinalMatchSlider
			// 
			this.lblFinalMatchSlider.AutoSize = true;
			this.lblFinalMatchSlider.Location = new System.Drawing.Point(39, 132);
			this.lblFinalMatchSlider.Name = "lblFinalMatchSlider";
			this.lblFinalMatchSlider.Size = new System.Drawing.Size(137, 13);
			this.lblFinalMatchSlider.TabIndex = 7;
			this.lblFinalMatchSlider.Text = "Minimum Final Match Score";
			// 
			// lblPreMatchSlider
			// 
			this.lblPreMatchSlider.AutoSize = true;
			this.lblPreMatchSlider.Location = new System.Drawing.Point(39, 72);
			this.lblPreMatchSlider.Name = "lblPreMatchSlider";
			this.lblPreMatchSlider.Size = new System.Drawing.Size(131, 13);
			this.lblPreMatchSlider.TabIndex = 4;
			this.lblPreMatchSlider.Text = "Minimum Pre-Match Score";
			// 
			// sldFinalMatch
			// 
			this.sldFinalMatch.Location = new System.Drawing.Point(31, 148);
			this.sldFinalMatch.Maximum = 999;
			this.sldFinalMatch.Minimum = 800;
			this.sldFinalMatch.Name = "sldFinalMatch";
			this.sldFinalMatch.Size = new System.Drawing.Size(204, 45);
			this.sldFinalMatch.TabIndex = 8;
			this.sldFinalMatch.Value = 800;
			this.sldFinalMatch.Scroll += new System.EventHandler(this.sldFinalMatch_Scroll);
			// 
			// sldPreMatch
			// 
			this.sldPreMatch.Location = new System.Drawing.Point(31, 88);
			this.sldPreMatch.Maximum = 950;
			this.sldPreMatch.Minimum = 750;
			this.sldPreMatch.Name = "sldPreMatch";
			this.sldPreMatch.Size = new System.Drawing.Size(204, 45);
			this.sldPreMatch.TabIndex = 5;
			this.sldPreMatch.Value = 750;
			this.sldPreMatch.Scroll += new System.EventHandler(this.sldPreMatch_Scroll);
			// 
			// optFuzzy
			// 
			this.optFuzzy.AutoSize = true;
			this.optFuzzy.Checked = true;
			this.optFuzzy.Location = new System.Drawing.Point(15, 42);
			this.optFuzzy.Name = "optFuzzy";
			this.optFuzzy.Size = new System.Drawing.Size(118, 17);
			this.optFuzzy.TabIndex = 3;
			this.optFuzzy.TabStop = true;
			this.optFuzzy.Text = "Fuzzy Matches Too";
			this.optFuzzy.UseVisualStyleBackColor = true;
			this.optFuzzy.CheckedChanged += new System.EventHandler(this.optFuzzy_CheckedChanged);
			// 
			// optExact
			// 
			this.optExact.AutoSize = true;
			this.optExact.Location = new System.Drawing.Point(15, 19);
			this.optExact.Name = "optExact";
			this.optExact.Size = new System.Drawing.Size(120, 17);
			this.optExact.TabIndex = 2;
			this.optExact.Text = "Exact Matches Only";
			this.optExact.UseVisualStyleBackColor = true;
			this.optExact.CheckedChanged += new System.EventHandler(this.optExact_CheckedChanged);
			// 
			// grpSave
			// 
			this.grpSave.Controls.Add(this.optCRGAlpha);
			this.grpSave.Controls.Add(this.optCRGDisplay);
			this.grpSave.Controls.Add(this.optMixedDisplay);
			this.grpSave.Location = new System.Drawing.Point(296, 12);
			this.grpSave.Name = "grpSave";
			this.grpSave.Size = new System.Drawing.Size(266, 193);
			this.grpSave.TabIndex = 10;
			this.grpSave.TabStop = false;
			this.grpSave.Text = " Ordering... ";
			this.grpSave.Visible = false;
			// 
			// optCRGAlpha
			// 
			this.optCRGAlpha.Location = new System.Drawing.Point(15, 102);
			this.optCRGAlpha.Name = "optCRGAlpha";
			this.optCRGAlpha.Size = new System.Drawing.Size(245, 43);
			this.optCRGAlpha.TabIndex = 13;
			this.optCRGAlpha.Text = "Channels first, then RGB Channels, then Channel Groups in Alphabetical Order";
			this.optCRGAlpha.UseVisualStyleBackColor = true;
			// 
			// optCRGDisplay
			// 
			this.optCRGDisplay.Location = new System.Drawing.Point(15, 57);
			this.optCRGDisplay.Name = "optCRGDisplay";
			this.optCRGDisplay.Size = new System.Drawing.Size(245, 43);
			this.optCRGDisplay.TabIndex = 12;
			this.optCRGDisplay.Text = "Channels first, then RGB Channels, then Channel Groups in Display Order";
			this.optCRGDisplay.UseVisualStyleBackColor = true;
			// 
			// optMixedDisplay
			// 
			this.optMixedDisplay.Checked = true;
			this.optMixedDisplay.Location = new System.Drawing.Point(15, 19);
			this.optMixedDisplay.Name = "optMixedDisplay";
			this.optMixedDisplay.Size = new System.Drawing.Size(245, 40);
			this.optMixedDisplay.TabIndex = 11;
			this.optMixedDisplay.TabStop = true;
			this.optMixedDisplay.Text = "Channels, RGB Channels, and Channel Groups Mixed, but in Display Order";
			this.optMixedDisplay.UseVisualStyleBackColor = true;
			// 
			// frmOptions
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(646, 261);
			this.Controls.Add(this.grpSave);
			this.Controls.Add(this.grpNames);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "frmOptions";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Match Options";
			this.Load += new System.EventHandler(this.frmOptions_Load);
			this.grpNames.ResumeLayout(false);
			this.grpNames.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldFinalMatch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sldPreMatch)).EndInit();
			this.grpSave.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.GroupBox grpNames;
		private System.Windows.Forms.Label lblFinalMatchScore;
		private System.Windows.Forms.Label lblPreMatchScore;
		private System.Windows.Forms.Label lblFinalMatchSlider;
		private System.Windows.Forms.Label lblPreMatchSlider;
		private System.Windows.Forms.TrackBar sldFinalMatch;
		private System.Windows.Forms.TrackBar sldPreMatch;
		private System.Windows.Forms.RadioButton optFuzzy;
		private System.Windows.Forms.RadioButton optExact;
		private System.Windows.Forms.GroupBox grpSave;
		private System.Windows.Forms.RadioButton optCRGAlpha;
		private System.Windows.Forms.RadioButton optCRGDisplay;
		private System.Windows.Forms.RadioButton optMixedDisplay;
	}
}