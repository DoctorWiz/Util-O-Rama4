namespace MergeORama
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.optMergeTracksByNumber = new System.Windows.Forms.RadioButton();
			this.optMergeTracksByName = new System.Windows.Forms.RadioButton();
			this.optAddTracks = new System.Windows.Forms.RadioButton();
			this.optMergeTracks = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlDuplicateAction = new System.Windows.Forms.Panel();
			this.txtNumberFormat = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lblRecommend = new System.Windows.Forms.Label();
			this.optAddNumber = new System.Windows.Forms.RadioButton();
			this.optKeepBoth = new System.Windows.Forms.RadioButton();
			this.optUseSecond = new System.Windows.Forms.RadioButton();
			this.optKeepFirst = new System.Windows.Forms.RadioButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.chkMergeChannelsByName = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.pnlDuplicateAction.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdOK
			// 
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.Location = new System.Drawing.Point(156, 368);
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
			this.cmdCancel.Location = new System.Drawing.Point(237, 368);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 18;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel1);
			this.panel2.Controls.Add(this.optAddTracks);
			this.panel2.Controls.Add(this.optMergeTracks);
			this.panel2.Location = new System.Drawing.Point(19, 25);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(200, 93);
			this.panel2.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.optMergeTracksByNumber);
			this.panel1.Controls.Add(this.optMergeTracksByName);
			this.panel1.Location = new System.Drawing.Point(21, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(91, 44);
			this.panel1.TabIndex = 4;
			// 
			// optMergeTracksByNumber
			// 
			this.optMergeTracksByNumber.AutoSize = true;
			this.optMergeTracksByNumber.Location = new System.Drawing.Point(3, 26);
			this.optMergeTracksByNumber.Name = "optMergeTracksByNumber";
			this.optMergeTracksByNumber.Size = new System.Drawing.Size(77, 17);
			this.optMergeTracksByNumber.TabIndex = 6;
			this.optMergeTracksByNumber.Text = "By Number";
			this.optMergeTracksByNumber.UseVisualStyleBackColor = true;
			this.optMergeTracksByNumber.CheckedChanged += new System.EventHandler(this.optMergeTracksByNumber_CheckedChanged);
			// 
			// optMergeTracksByName
			// 
			this.optMergeTracksByName.AutoSize = true;
			this.optMergeTracksByName.Location = new System.Drawing.Point(3, 3);
			this.optMergeTracksByName.Name = "optMergeTracksByName";
			this.optMergeTracksByName.Size = new System.Drawing.Size(68, 17);
			this.optMergeTracksByName.TabIndex = 5;
			this.optMergeTracksByName.Text = "By Name";
			this.optMergeTracksByName.UseVisualStyleBackColor = true;
			this.optMergeTracksByName.CheckedChanged += new System.EventHandler(this.optMergeTracksByName_CheckedChanged);
			// 
			// optAddTracks
			// 
			this.optAddTracks.AutoSize = true;
			this.optAddTracks.Location = new System.Drawing.Point(2, 74);
			this.optAddTracks.Name = "optAddTracks";
			this.optAddTracks.Size = new System.Drawing.Size(162, 17);
			this.optAddTracks.TabIndex = 7;
			this.optAddTracks.Text = "Add New Track(s) to the end";
			this.optAddTracks.UseVisualStyleBackColor = true;
			this.optAddTracks.CheckedChanged += new System.EventHandler(this.optAddTracks_CheckedChanged);
			// 
			// optMergeTracks
			// 
			this.optMergeTracks.AutoSize = true;
			this.optMergeTracks.Location = new System.Drawing.Point(2, 2);
			this.optMergeTracks.Name = "optMergeTracks";
			this.optMergeTracks.Size = new System.Drawing.Size(97, 17);
			this.optMergeTracks.TabIndex = 3;
			this.optMergeTracks.Text = "Merge Track(s)";
			this.optMergeTracks.UseVisualStyleBackColor = true;
			this.optMergeTracks.CheckedChanged += new System.EventHandler(this.optMergeTracks_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 165);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(183, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Duplicate Channel and Group Names";
			// 
			// pnlDuplicateAction
			// 
			this.pnlDuplicateAction.Controls.Add(this.txtNumberFormat);
			this.pnlDuplicateAction.Controls.Add(this.label2);
			this.pnlDuplicateAction.Controls.Add(this.lblRecommend);
			this.pnlDuplicateAction.Controls.Add(this.optAddNumber);
			this.pnlDuplicateAction.Controls.Add(this.optKeepBoth);
			this.pnlDuplicateAction.Controls.Add(this.optUseSecond);
			this.pnlDuplicateAction.Controls.Add(this.optKeepFirst);
			this.pnlDuplicateAction.Location = new System.Drawing.Point(19, 181);
			this.pnlDuplicateAction.Name = "pnlDuplicateAction";
			this.pnlDuplicateAction.Size = new System.Drawing.Size(307, 162);
			this.pnlDuplicateAction.TabIndex = 10;
			// 
			// txtNumberFormat
			// 
			this.txtNumberFormat.Location = new System.Drawing.Point(31, 114);
			this.txtNumberFormat.Name = "txtNumberFormat";
			this.txtNumberFormat.Size = new System.Drawing.Size(78, 20);
			this.txtNumberFormat.TabIndex = 17;
			this.txtNumberFormat.Text = " (#)";
			this.txtNumberFormat.TextChanged += new System.EventHandler(this.txtNumberFormat_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(28, 98);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Format";
			// 
			// lblRecommend
			// 
			this.lblRecommend.AutoSize = true;
			this.lblRecommend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRecommend.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblRecommend.Location = new System.Drawing.Point(190, 57);
			this.lblRecommend.Name = "lblRecommend";
			this.lblRecommend.Size = new System.Drawing.Size(100, 13);
			this.lblRecommend.TabIndex = 14;
			this.lblRecommend.Text = "NOT recommended";
			// 
			// optAddNumber
			// 
			this.optAddNumber.AutoSize = true;
			this.optAddNumber.CausesValidation = false;
			this.optAddNumber.Location = new System.Drawing.Point(4, 78);
			this.optAddNumber.Name = "optAddNumber";
			this.optAddNumber.Size = new System.Drawing.Size(213, 17);
			this.optAddNumber.TabIndex = 15;
			this.optAddNumber.Text = "Add a number to end of the second one";
			this.optAddNumber.UseVisualStyleBackColor = true;
			this.optAddNumber.CheckedChanged += new System.EventHandler(this.optAddNumber_CheckedChanged);
			// 
			// optKeepBoth
			// 
			this.optKeepBoth.AutoSize = true;
			this.optKeepBoth.Location = new System.Drawing.Point(4, 55);
			this.optKeepBoth.Name = "optKeepBoth";
			this.optKeepBoth.Size = new System.Drawing.Size(180, 17);
			this.optKeepBoth.TabIndex = 13;
			this.optKeepBoth.Text = "Keep Both, with duplicate names";
			this.optKeepBoth.UseVisualStyleBackColor = true;
			this.optKeepBoth.CheckedChanged += new System.EventHandler(this.optKeepBoth_CheckedChanged);
			// 
			// optUseSecond
			// 
			this.optUseSecond.AutoSize = true;
			this.optUseSecond.Location = new System.Drawing.Point(4, 32);
			this.optUseSecond.Name = "optUseSecond";
			this.optUseSecond.Size = new System.Drawing.Size(240, 17);
			this.optUseSecond.TabIndex = 12;
			this.optUseSecond.Text = "Replace with the Second one, and it\'s effects";
			this.optUseSecond.UseVisualStyleBackColor = true;
			this.optUseSecond.CheckedChanged += new System.EventHandler(this.optUseSecond_CheckedChanged);
			// 
			// optKeepFirst
			// 
			this.optKeepFirst.AutoSize = true;
			this.optKeepFirst.Location = new System.Drawing.Point(4, 9);
			this.optKeepFirst.Name = "optKeepFirst";
			this.optKeepFirst.Size = new System.Drawing.Size(185, 17);
			this.optKeepFirst.TabIndex = 11;
			this.optKeepFirst.Text = "Keep the First one, and it\'s effects";
			this.optKeepFirst.UseVisualStyleBackColor = true;
			this.optKeepFirst.CheckedChanged += new System.EventHandler(this.optKeepFirst_CheckedChanged);
			// 
			// chkMergeChannelsByName
			// 
			this.chkMergeChannelsByName.AutoSize = true;
			this.chkMergeChannelsByName.Enabled = false;
			this.chkMergeChannelsByName.Location = new System.Drawing.Point(21, 133);
			this.chkMergeChannelsByName.Name = "chkMergeChannelsByName";
			this.chkMergeChannelsByName.Size = new System.Drawing.Size(183, 17);
			this.chkMergeChannelsByName.TabIndex = 8;
			this.chkMergeChannelsByName.Text = "Merge Channel Groups, by Name";
			this.chkMergeChannelsByName.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(67, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Smart Merge";
			// 
			// frmOptions
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(324, 403);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.chkMergeChannelsByName);
			this.Controls.Add(this.pnlDuplicateAction);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmOptions";
			this.Text = "Options";
			this.Load += new System.EventHandler(this.Options_Load);
			this.ResizeBegin += new System.EventHandler(this.frmOptions_ResizeBegin);
			this.ResizeEnd += new System.EventHandler(this.frmOptions_ResizeEnd);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.pnlDuplicateAction.ResumeLayout(false);
			this.pnlDuplicateAction.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton optMergeTracksByNumber;
		private System.Windows.Forms.RadioButton optMergeTracksByName;
		private System.Windows.Forms.RadioButton optAddTracks;
		private System.Windows.Forms.RadioButton optMergeTracks;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel pnlDuplicateAction;
		private System.Windows.Forms.TextBox txtNumberFormat;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblRecommend;
		private System.Windows.Forms.RadioButton optAddNumber;
		private System.Windows.Forms.RadioButton optKeepBoth;
		private System.Windows.Forms.RadioButton optUseSecond;
		private System.Windows.Forms.RadioButton optKeepFirst;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox chkMergeChannelsByName;
		private System.Windows.Forms.Label label4;
	}
}