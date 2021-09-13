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
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label4 = new System.Windows.Forms.Label();
			this.grpGrids = new System.Windows.Forms.GroupBox();
			this.chkMergeGrids = new System.Windows.Forms.CheckBox();
			this.grpTracks = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.optMergeTracksByNumber = new System.Windows.Forms.RadioButton();
			this.optMergeTracksByName = new System.Windows.Forms.RadioButton();
			this.optMergeTracks = new System.Windows.Forms.RadioButton();
			this.optAddTracks = new System.Windows.Forms.RadioButton();
			this.grpEffects = new System.Windows.Forms.GroupBox();
			this.optMergeEffects = new System.Windows.Forms.RadioButton();
			this.optInfoOnly = new System.Windows.Forms.RadioButton();
			this.grpGroups = new System.Windows.Forms.GroupBox();
			this.chkGroupsByName = new System.Windows.Forms.CheckBox();
			this.grpChannels = new System.Windows.Forms.GroupBox();
			this.lblRecommend = new System.Windows.Forms.Label();
			this.optAddNumber = new System.Windows.Forms.RadioButton();
			this.optKeepBoth = new System.Windows.Forms.RadioButton();
			this.optUseSecond = new System.Windows.Forms.RadioButton();
			this.optKeepFirst = new System.Windows.Forms.RadioButton();
			this.grpGrids.SuspendLayout();
			this.grpTracks.SuspendLayout();
			this.panel1.SuspendLayout();
			this.grpEffects.SuspendLayout();
			this.grpGroups.SuspendLayout();
			this.grpChannels.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdOK
			// 
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.Location = new System.Drawing.Point(156, 451);
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
			this.cmdCancel.Location = new System.Drawing.Point(237, 451);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 18;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(5, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 16);
			this.label4.TabIndex = 1;
			this.label4.Text = "Smart Merge";
			// 
			// grpGrids
			// 
			this.grpGrids.Controls.Add(this.chkMergeGrids);
			this.grpGrids.Location = new System.Drawing.Point(6, 28);
			this.grpGrids.Name = "grpGrids";
			this.grpGrids.Size = new System.Drawing.Size(306, 41);
			this.grpGrids.TabIndex = 20;
			this.grpGrids.TabStop = false;
			this.grpGrids.Text = " Timing Grids ";
			// 
			// chkMergeGrids
			// 
			this.chkMergeGrids.AutoSize = true;
			this.chkMergeGrids.Checked = true;
			this.chkMergeGrids.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkMergeGrids.Location = new System.Drawing.Point(6, 19);
			this.chkMergeGrids.Name = "chkMergeGrids";
			this.chkMergeGrids.Size = new System.Drawing.Size(123, 17);
			this.chkMergeGrids.TabIndex = 20;
			this.chkMergeGrids.Text = "Merge Timing Grid(s)";
			this.chkMergeGrids.UseVisualStyleBackColor = true;
			this.chkMergeGrids.CheckedChanged += new System.EventHandler(this.option_CheckedChanged);
			// 
			// grpTracks
			// 
			this.grpTracks.Controls.Add(this.panel1);
			this.grpTracks.Controls.Add(this.optMergeTracks);
			this.grpTracks.Controls.Add(this.optAddTracks);
			this.grpTracks.Location = new System.Drawing.Point(8, 75);
			this.grpTracks.Name = "grpTracks";
			this.grpTracks.Size = new System.Drawing.Size(304, 116);
			this.grpTracks.TabIndex = 21;
			this.grpTracks.TabStop = false;
			this.grpTracks.Text = " Tracks ";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.optMergeTracksByNumber);
			this.panel1.Controls.Add(this.optMergeTracksByName);
			this.panel1.Location = new System.Drawing.Point(25, 42);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(91, 44);
			this.panel1.TabIndex = 9;
			// 
			// optMergeTracksByNumber
			// 
			this.optMergeTracksByNumber.AutoSize = true;
			this.optMergeTracksByNumber.Enabled = false;
			this.optMergeTracksByNumber.Location = new System.Drawing.Point(3, 26);
			this.optMergeTracksByNumber.Name = "optMergeTracksByNumber";
			this.optMergeTracksByNumber.Size = new System.Drawing.Size(77, 17);
			this.optMergeTracksByNumber.TabIndex = 6;
			this.optMergeTracksByNumber.Text = "By Number";
			this.optMergeTracksByNumber.UseVisualStyleBackColor = true;
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
			// 
			// optMergeTracks
			// 
			this.optMergeTracks.AutoSize = true;
			this.optMergeTracks.Location = new System.Drawing.Point(6, 19);
			this.optMergeTracks.Name = "optMergeTracks";
			this.optMergeTracks.Size = new System.Drawing.Size(97, 17);
			this.optMergeTracks.TabIndex = 8;
			this.optMergeTracks.Text = "Merge LORTrack4(s)";
			this.optMergeTracks.UseVisualStyleBackColor = true;
			this.optMergeTracks.CheckedChanged += new System.EventHandler(this.optMergeTracks_CheckedChanged);
			// 
			// optAddTracks
			// 
			this.optAddTracks.AutoSize = true;
			this.optAddTracks.Enabled = false;
			this.optAddTracks.Location = new System.Drawing.Point(6, 91);
			this.optAddTracks.Name = "optAddTracks";
			this.optAddTracks.Size = new System.Drawing.Size(162, 17);
			this.optAddTracks.TabIndex = 10;
			this.optAddTracks.Text = "Add New LORTrack4(s) to the end";
			this.optAddTracks.UseVisualStyleBackColor = true;
			// 
			// grpEffects
			// 
			this.grpEffects.Controls.Add(this.optMergeEffects);
			this.grpEffects.Controls.Add(this.optInfoOnly);
			this.grpEffects.Location = new System.Drawing.Point(8, 244);
			this.grpEffects.Name = "grpEffects";
			this.grpEffects.Size = new System.Drawing.Size(304, 70);
			this.grpEffects.TabIndex = 22;
			this.grpEffects.TabStop = false;
			this.grpEffects.Text = " Effects ";
			// 
			// optMergeEffects
			// 
			this.optMergeEffects.AutoSize = true;
			this.optMergeEffects.Location = new System.Drawing.Point(6, 42);
			this.optMergeEffects.Name = "optMergeEffects";
			this.optMergeEffects.Size = new System.Drawing.Size(121, 17);
			this.optMergeEffects.TabIndex = 21;
			this.optMergeEffects.Text = "Merge LORSeqInfo4 + Effects";
			this.optMergeEffects.UseVisualStyleBackColor = true;
			// 
			// optInfoOnly
			// 
			this.optInfoOnly.AutoSize = true;
			this.optInfoOnly.Location = new System.Drawing.Point(6, 19);
			this.optInfoOnly.Name = "optInfoOnly";
			this.optInfoOnly.Size = new System.Drawing.Size(229, 17);
			this.optInfoOnly.TabIndex = 20;
			this.optInfoOnly.Text = "Merge LORTrack4 / Group / Channel LORSeqInfo4 ONLY";
			this.optInfoOnly.UseVisualStyleBackColor = true;
			// 
			// grpGroups
			// 
			this.grpGroups.Controls.Add(this.chkGroupsByName);
			this.grpGroups.Location = new System.Drawing.Point(8, 197);
			this.grpGroups.Name = "grpGroups";
			this.grpGroups.Size = new System.Drawing.Size(304, 41);
			this.grpGroups.TabIndex = 23;
			this.grpGroups.TabStop = false;
			this.grpGroups.Text = " Channel Groups ";
			// 
			// chkGroupsByName
			// 
			this.chkGroupsByName.AutoSize = true;
			this.chkGroupsByName.Enabled = false;
			this.chkGroupsByName.Location = new System.Drawing.Point(6, 19);
			this.chkGroupsByName.Name = "chkGroupsByName";
			this.chkGroupsByName.Size = new System.Drawing.Size(183, 17);
			this.chkGroupsByName.TabIndex = 9;
			this.chkGroupsByName.Text = "Merge Channel Groups, by Name";
			this.chkGroupsByName.UseVisualStyleBackColor = true;
			// 
			// grpChannels
			// 
			this.grpChannels.Controls.Add(this.lblRecommend);
			this.grpChannels.Controls.Add(this.optAddNumber);
			this.grpChannels.Controls.Add(this.optKeepBoth);
			this.grpChannels.Controls.Add(this.optUseSecond);
			this.grpChannels.Controls.Add(this.optKeepFirst);
			this.grpChannels.Location = new System.Drawing.Point(8, 320);
			this.grpChannels.Name = "grpChannels";
			this.grpChannels.Size = new System.Drawing.Size(304, 112);
			this.grpChannels.TabIndex = 24;
			this.grpChannels.TabStop = false;
			this.grpChannels.Text = " Duplicate Channel Names ";
			// 
			// lblRecommend
			// 
			this.lblRecommend.AutoSize = true;
			this.lblRecommend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRecommend.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblRecommend.Location = new System.Drawing.Point(192, 67);
			this.lblRecommend.Name = "lblRecommend";
			this.lblRecommend.Size = new System.Drawing.Size(100, 13);
			this.lblRecommend.TabIndex = 21;
			this.lblRecommend.Text = "NOT recommended";
			// 
			// optAddNumber
			// 
			this.optAddNumber.AutoSize = true;
			this.optAddNumber.CausesValidation = false;
			this.optAddNumber.Enabled = false;
			this.optAddNumber.Location = new System.Drawing.Point(6, 88);
			this.optAddNumber.Name = "optAddNumber";
			this.optAddNumber.Size = new System.Drawing.Size(254, 17);
			this.optAddNumber.TabIndex = 22;
			this.optAddNumber.Text = "Keep Both, Add a number to end of the new one";
			this.optAddNumber.UseVisualStyleBackColor = true;
			this.optAddNumber.CheckedChanged += new System.EventHandler(this.option_CheckedChanged);
			// 
			// optKeepBoth
			// 
			this.optKeepBoth.AutoSize = true;
			this.optKeepBoth.Enabled = false;
			this.optKeepBoth.Location = new System.Drawing.Point(6, 65);
			this.optKeepBoth.Name = "optKeepBoth";
			this.optKeepBoth.Size = new System.Drawing.Size(180, 17);
			this.optKeepBoth.TabIndex = 20;
			this.optKeepBoth.Text = "Keep Both, with duplicate names";
			this.optKeepBoth.UseVisualStyleBackColor = true;
			this.optKeepBoth.CheckedChanged += new System.EventHandler(this.option_CheckedChanged);
			// 
			// optUseSecond
			// 
			this.optUseSecond.AutoSize = true;
			this.optUseSecond.Location = new System.Drawing.Point(6, 42);
			this.optUseSecond.Name = "optUseSecond";
			this.optUseSecond.Size = new System.Drawing.Size(225, 17);
			this.optUseSecond.TabIndex = 19;
			this.optUseSecond.Text = "Replace with the New one, and it\'s effects";
			this.optUseSecond.UseVisualStyleBackColor = true;
			this.optUseSecond.CheckedChanged += new System.EventHandler(this.option_CheckedChanged);
			// 
			// optKeepFirst
			// 
			this.optKeepFirst.AutoSize = true;
			this.optKeepFirst.Enabled = false;
			this.optKeepFirst.Location = new System.Drawing.Point(6, 19);
			this.optKeepFirst.Name = "optKeepFirst";
			this.optKeepFirst.Size = new System.Drawing.Size(201, 17);
			this.optKeepFirst.TabIndex = 18;
			this.optKeepFirst.Text = "Keep the Original one, and it\'s effects";
			this.optKeepFirst.UseVisualStyleBackColor = true;
			this.optKeepFirst.CheckedChanged += new System.EventHandler(this.option_CheckedChanged);
			// 
			// frmOptions
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(322, 485);
			this.Controls.Add(this.grpChannels);
			this.Controls.Add(this.grpGroups);
			this.Controls.Add(this.grpEffects);
			this.Controls.Add(this.grpTracks);
			this.Controls.Add(this.grpGrids);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmOptions";
			this.Text = "Options";
			this.Load += new System.EventHandler(this.Options_Load);
			this.ResizeEnd += new System.EventHandler(this.frmOptions_ResizeEnd);
			this.grpGrids.ResumeLayout(false);
			this.grpGrids.PerformLayout();
			this.grpTracks.ResumeLayout(false);
			this.grpTracks.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.grpEffects.ResumeLayout(false);
			this.grpEffects.PerformLayout();
			this.grpGroups.ResumeLayout(false);
			this.grpGroups.PerformLayout();
			this.grpChannels.ResumeLayout(false);
			this.grpChannels.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox grpGrids;
		private System.Windows.Forms.CheckBox chkMergeGrids;
		private System.Windows.Forms.GroupBox grpTracks;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton optMergeTracksByNumber;
		private System.Windows.Forms.RadioButton optMergeTracksByName;
		private System.Windows.Forms.RadioButton optMergeTracks;
		private System.Windows.Forms.RadioButton optAddTracks;
		private System.Windows.Forms.GroupBox grpEffects;
		private System.Windows.Forms.RadioButton optMergeEffects;
		private System.Windows.Forms.RadioButton optInfoOnly;
		private System.Windows.Forms.GroupBox grpGroups;
		private System.Windows.Forms.CheckBox chkGroupsByName;
		private System.Windows.Forms.GroupBox grpChannels;
		private System.Windows.Forms.Label lblRecommend;
		private System.Windows.Forms.RadioButton optAddNumber;
		private System.Windows.Forms.RadioButton optKeepBoth;
		private System.Windows.Forms.RadioButton optUseSecond;
		private System.Windows.Forms.RadioButton optKeepFirst;
	}
}