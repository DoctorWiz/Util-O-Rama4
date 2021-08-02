namespace xTimeORama
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.chkUseFuzzy = new System.Windows.Forms.CheckBox();
			this.grmPrematch = new System.Windows.Forms.GroupBox();
			this.lblPrematchValue = new System.Windows.Forms.Label();
			this.lblMinPrematchSlider = new System.Windows.Forms.Label();
			this.sldPrematch = new System.Windows.Forms.TrackBar();
			this.cboPrematch = new System.Windows.Forms.ComboBox();
			this.lblPrematch = new System.Windows.Forms.Label();
			this.grpFinalMatch = new System.Windows.Forms.GroupBox();
			this.chk020000 = new System.Windows.Forms.CheckBox();
			this.chk100000 = new System.Windows.Forms.CheckBox();
			this.chk002000 = new System.Windows.Forms.CheckBox();
			this.chk200000 = new System.Windows.Forms.CheckBox();
			this.chk080000 = new System.Windows.Forms.CheckBox();
			this.chk800000 = new System.Windows.Forms.CheckBox();
			this.chk400000 = new System.Windows.Forms.CheckBox();
			this.chk001000 = new System.Windows.Forms.CheckBox();
			this.chk040000 = new System.Windows.Forms.CheckBox();
			this.chk010000 = new System.Windows.Forms.CheckBox();
			this.chk008000 = new System.Windows.Forms.CheckBox();
			this.chk004000 = new System.Windows.Forms.CheckBox();
			this.lblFinalMatchValue = new System.Windows.Forms.Label();
			this.chk000020 = new System.Windows.Forms.CheckBox();
			this.chk000100 = new System.Windows.Forms.CheckBox();
			this.chk000002 = new System.Windows.Forms.CheckBox();
			this.chk000200 = new System.Windows.Forms.CheckBox();
			this.chk000080 = new System.Windows.Forms.CheckBox();
			this.chk000800 = new System.Windows.Forms.CheckBox();
			this.chk000400 = new System.Windows.Forms.CheckBox();
			this.chk000001 = new System.Windows.Forms.CheckBox();
			this.chk000040 = new System.Windows.Forms.CheckBox();
			this.chk000010 = new System.Windows.Forms.CheckBox();
			this.chk000008 = new System.Windows.Forms.CheckBox();
			this.chk000004 = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblFinalMatchSlider = new System.Windows.Forms.Label();
			this.sldFinalMatch = new System.Windows.Forms.TrackBar();
			this.btnDefaults = new System.Windows.Forms.Button();
			this.chkCase = new System.Windows.Forms.CheckBox();
			this.chkLog = new System.Windows.Forms.CheckBox();
			this.grmPrematch.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldPrematch)).BeginInit();
			this.grpFinalMatch.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldFinalMatch)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(364, 408);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 16;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(283, 408);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 15;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// chkUseFuzzy
			// 
			this.chkUseFuzzy.AutoSize = true;
			this.chkUseFuzzy.Checked = true;
			this.chkUseFuzzy.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUseFuzzy.Location = new System.Drawing.Point(22, 14);
			this.chkUseFuzzy.Name = "chkUseFuzzy";
			this.chkUseFuzzy.Size = new System.Drawing.Size(153, 17);
			this.chkUseFuzzy.TabIndex = 17;
			this.chkUseFuzzy.Text = "Use Fuzzy Name Matching";
			this.chkUseFuzzy.UseVisualStyleBackColor = true;
			this.chkUseFuzzy.CheckedChanged += new System.EventHandler(this.chkUseFuzzy_CheckedChanged);
			// 
			// grmPrematch
			// 
			this.grmPrematch.Controls.Add(this.lblPrematchValue);
			this.grmPrematch.Controls.Add(this.lblMinPrematchSlider);
			this.grmPrematch.Controls.Add(this.sldPrematch);
			this.grmPrematch.Controls.Add(this.cboPrematch);
			this.grmPrematch.Controls.Add(this.lblPrematch);
			this.grmPrematch.Location = new System.Drawing.Point(12, 57);
			this.grmPrematch.Name = "grmPrematch";
			this.grmPrematch.Size = new System.Drawing.Size(252, 144);
			this.grmPrematch.TabIndex = 22;
			this.grmPrematch.TabStop = false;
			this.grmPrematch.Text = " Prematch ";
			// 
			// lblPrematchValue
			// 
			this.lblPrematchValue.AutoSize = true;
			this.lblPrematchValue.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblPrematchValue.Location = new System.Drawing.Point(204, 91);
			this.lblPrematchValue.Name = "lblPrematchValue";
			this.lblPrematchValue.Size = new System.Drawing.Size(19, 13);
			this.lblPrematchValue.TabIndex = 26;
			this.lblPrematchValue.Text = "85";
			// 
			// lblMinPrematchSlider
			// 
			this.lblMinPrematchSlider.AutoSize = true;
			this.lblMinPrematchSlider.Location = new System.Drawing.Point(16, 74);
			this.lblMinPrematchSlider.Name = "lblMinPrematchSlider";
			this.lblMinPrematchSlider.Size = new System.Drawing.Size(79, 13);
			this.lblMinPrematchSlider.TabIndex = 25;
			this.lblMinPrematchSlider.Text = "Minimum Score";
			// 
			// sldPrematch
			// 
			this.sldPrematch.Location = new System.Drawing.Point(16, 90);
			this.sldPrematch.Maximum = 95;
			this.sldPrematch.Minimum = 59;
			this.sldPrematch.Name = "sldPrematch";
			this.sldPrematch.Size = new System.Drawing.Size(182, 45);
			this.sldPrematch.TabIndex = 24;
			this.sldPrematch.Value = 85;
			this.sldPrematch.Scroll += new System.EventHandler(this.sldPrematch_Scroll);
			// 
			// cboPrematch
			// 
			this.cboPrematch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPrematch.FormattingEnabled = true;
			this.cboPrematch.Items.AddRange(new object[] {
            "Levenshtein Distance",
            "Normalized Levenshtein",
            "Damerau-Levenshtein",
            "Hamming Distance",
            "Jaccard Index",
            "Jaro Distance",
            "Jaro-Winkler",
            "Longest Common Subsequence",
            "Longest Common Substring",
            "Overlap Coefficient",
            "Ratcliff-Overshelp",
            "Sorensen-Dice",
            "Tanimoto Coefficient"});
			this.cboPrematch.Location = new System.Drawing.Point(16, 44);
			this.cboPrematch.Name = "cboPrematch";
			this.cboPrematch.Size = new System.Drawing.Size(221, 21);
			this.cboPrematch.TabIndex = 23;
			this.cboPrematch.SelectedIndexChanged += new System.EventHandler(this.cboPrematch_SelectedIndexChanged);
			// 
			// lblPrematch
			// 
			this.lblPrematch.AutoSize = true;
			this.lblPrematch.Location = new System.Drawing.Point(16, 25);
			this.lblPrematch.Name = "lblPrematch";
			this.lblPrematch.Size = new System.Drawing.Size(50, 13);
			this.lblPrematch.TabIndex = 22;
			this.lblPrematch.Text = "Algorithm";
			// 
			// grpFinalMatch
			// 
			this.grpFinalMatch.Controls.Add(this.chk020000);
			this.grpFinalMatch.Controls.Add(this.chk100000);
			this.grpFinalMatch.Controls.Add(this.chk002000);
			this.grpFinalMatch.Controls.Add(this.chk200000);
			this.grpFinalMatch.Controls.Add(this.chk080000);
			this.grpFinalMatch.Controls.Add(this.chk800000);
			this.grpFinalMatch.Controls.Add(this.chk400000);
			this.grpFinalMatch.Controls.Add(this.chk001000);
			this.grpFinalMatch.Controls.Add(this.chk040000);
			this.grpFinalMatch.Controls.Add(this.chk010000);
			this.grpFinalMatch.Controls.Add(this.chk008000);
			this.grpFinalMatch.Controls.Add(this.chk004000);
			this.grpFinalMatch.Controls.Add(this.lblFinalMatchValue);
			this.grpFinalMatch.Controls.Add(this.chk000020);
			this.grpFinalMatch.Controls.Add(this.chk000100);
			this.grpFinalMatch.Controls.Add(this.chk000002);
			this.grpFinalMatch.Controls.Add(this.chk000200);
			this.grpFinalMatch.Controls.Add(this.chk000080);
			this.grpFinalMatch.Controls.Add(this.chk000800);
			this.grpFinalMatch.Controls.Add(this.chk000400);
			this.grpFinalMatch.Controls.Add(this.chk000001);
			this.grpFinalMatch.Controls.Add(this.chk000040);
			this.grpFinalMatch.Controls.Add(this.chk000010);
			this.grpFinalMatch.Controls.Add(this.chk000008);
			this.grpFinalMatch.Controls.Add(this.chk000004);
			this.grpFinalMatch.Controls.Add(this.label1);
			this.grpFinalMatch.Controls.Add(this.lblFinalMatchSlider);
			this.grpFinalMatch.Controls.Add(this.sldFinalMatch);
			this.grpFinalMatch.Location = new System.Drawing.Point(283, 14);
			this.grpFinalMatch.Name = "grpFinalMatch";
			this.grpFinalMatch.Size = new System.Drawing.Size(410, 366);
			this.grpFinalMatch.TabIndex = 23;
			this.grpFinalMatch.TabStop = false;
			this.grpFinalMatch.Text = " Final Match ";
			// 
			// chk020000
			// 
			this.chk020000.AutoSize = true;
			this.chk020000.Location = new System.Drawing.Point(210, 150);
			this.chk020000.Name = "chk020000";
			this.chk020000.Size = new System.Drawing.Size(29, 17);
			this.chk020000.TabIndex = 55;
			this.chk020000.Tag = "0x020000";
			this.chk020000.Text = " ";
			this.chk020000.UseVisualStyleBackColor = true;
			this.chk020000.Visible = false;
			this.chk020000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk100000
			// 
			this.chk100000.AutoSize = true;
			this.chk100000.Location = new System.Drawing.Point(210, 210);
			this.chk100000.Name = "chk100000";
			this.chk100000.Size = new System.Drawing.Size(29, 17);
			this.chk100000.TabIndex = 54;
			this.chk100000.Tag = "0x100000";
			this.chk100000.Text = " ";
			this.chk100000.UseVisualStyleBackColor = true;
			this.chk100000.Visible = false;
			this.chk100000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk002000
			// 
			this.chk002000.AutoSize = true;
			this.chk002000.Location = new System.Drawing.Point(210, 70);
			this.chk002000.Name = "chk002000";
			this.chk002000.Size = new System.Drawing.Size(29, 17);
			this.chk002000.TabIndex = 53;
			this.chk002000.Tag = "0x002000";
			this.chk002000.Text = " ";
			this.chk002000.UseVisualStyleBackColor = true;
			this.chk002000.Visible = false;
			this.chk002000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk200000
			// 
			this.chk200000.AutoSize = true;
			this.chk200000.Location = new System.Drawing.Point(210, 230);
			this.chk200000.Name = "chk200000";
			this.chk200000.Size = new System.Drawing.Size(29, 17);
			this.chk200000.TabIndex = 52;
			this.chk200000.Tag = "0x200000";
			this.chk200000.Text = " ";
			this.chk200000.UseVisualStyleBackColor = true;
			this.chk200000.Visible = false;
			this.chk200000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk080000
			// 
			this.chk080000.AutoSize = true;
			this.chk080000.Location = new System.Drawing.Point(210, 190);
			this.chk080000.Name = "chk080000";
			this.chk080000.Size = new System.Drawing.Size(29, 17);
			this.chk080000.TabIndex = 51;
			this.chk080000.Tag = "0x080000";
			this.chk080000.Text = " ";
			this.chk080000.UseVisualStyleBackColor = true;
			this.chk080000.Visible = false;
			this.chk080000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk800000
			// 
			this.chk800000.AutoSize = true;
			this.chk800000.Location = new System.Drawing.Point(210, 270);
			this.chk800000.Name = "chk800000";
			this.chk800000.Size = new System.Drawing.Size(15, 14);
			this.chk800000.TabIndex = 50;
			this.chk800000.Tag = "0x800000";
			this.chk800000.UseVisualStyleBackColor = true;
			this.chk800000.Visible = false;
			this.chk800000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk400000
			// 
			this.chk400000.AutoSize = true;
			this.chk400000.Location = new System.Drawing.Point(210, 250);
			this.chk400000.Name = "chk400000";
			this.chk400000.Size = new System.Drawing.Size(15, 14);
			this.chk400000.TabIndex = 49;
			this.chk400000.Tag = "0x400000";
			this.chk400000.UseVisualStyleBackColor = true;
			this.chk400000.Visible = false;
			this.chk400000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk001000
			// 
			this.chk001000.AutoSize = true;
			this.chk001000.Cursor = System.Windows.Forms.Cursors.Default;
			this.chk001000.Location = new System.Drawing.Point(210, 50);
			this.chk001000.Name = "chk001000";
			this.chk001000.Size = new System.Drawing.Size(29, 17);
			this.chk001000.TabIndex = 48;
			this.chk001000.Tag = "0x001000";
			this.chk001000.Text = " ";
			this.chk001000.UseVisualStyleBackColor = true;
			this.chk001000.Visible = false;
			this.chk001000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk040000
			// 
			this.chk040000.AutoSize = true;
			this.chk040000.Location = new System.Drawing.Point(210, 170);
			this.chk040000.Name = "chk040000";
			this.chk040000.Size = new System.Drawing.Size(29, 17);
			this.chk040000.TabIndex = 47;
			this.chk040000.Tag = "0x040000";
			this.chk040000.Text = " ";
			this.chk040000.UseVisualStyleBackColor = true;
			this.chk040000.Visible = false;
			this.chk040000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk010000
			// 
			this.chk010000.AutoSize = true;
			this.chk010000.Location = new System.Drawing.Point(210, 130);
			this.chk010000.Name = "chk010000";
			this.chk010000.Size = new System.Drawing.Size(29, 17);
			this.chk010000.TabIndex = 46;
			this.chk010000.Tag = "0x010000";
			this.chk010000.Text = " ";
			this.chk010000.UseVisualStyleBackColor = true;
			this.chk010000.Visible = false;
			this.chk010000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk008000
			// 
			this.chk008000.AutoSize = true;
			this.chk008000.Location = new System.Drawing.Point(210, 110);
			this.chk008000.Name = "chk008000";
			this.chk008000.Size = new System.Drawing.Size(29, 17);
			this.chk008000.TabIndex = 45;
			this.chk008000.Tag = "0x008000";
			this.chk008000.Text = " ";
			this.chk008000.UseVisualStyleBackColor = true;
			this.chk008000.Visible = false;
			this.chk008000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk004000
			// 
			this.chk004000.AutoSize = true;
			this.chk004000.Location = new System.Drawing.Point(210, 90);
			this.chk004000.Name = "chk004000";
			this.chk004000.Size = new System.Drawing.Size(29, 17);
			this.chk004000.TabIndex = 44;
			this.chk004000.Tag = "0x004000";
			this.chk004000.Text = " ";
			this.chk004000.UseVisualStyleBackColor = true;
			this.chk004000.Visible = false;
			this.chk004000.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// lblFinalMatchValue
			// 
			this.lblFinalMatchValue.AutoSize = true;
			this.lblFinalMatchValue.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblFinalMatchValue.Location = new System.Drawing.Point(207, 314);
			this.lblFinalMatchValue.Name = "lblFinalMatchValue";
			this.lblFinalMatchValue.Size = new System.Drawing.Size(19, 13);
			this.lblFinalMatchValue.TabIndex = 43;
			this.lblFinalMatchValue.Text = "95";
			// 
			// chk000020
			// 
			this.chk000020.AutoSize = true;
			this.chk000020.Location = new System.Drawing.Point(16, 150);
			this.chk000020.Name = "chk000020";
			this.chk000020.Size = new System.Drawing.Size(29, 17);
			this.chk000020.TabIndex = 42;
			this.chk000020.Tag = "0x000020";
			this.chk000020.Text = " ";
			this.chk000020.UseVisualStyleBackColor = true;
			this.chk000020.Visible = false;
			this.chk000020.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk000100
			// 
			this.chk000100.AutoSize = true;
			this.chk000100.Location = new System.Drawing.Point(16, 210);
			this.chk000100.Name = "chk000100";
			this.chk000100.Size = new System.Drawing.Size(29, 17);
			this.chk000100.TabIndex = 41;
			this.chk000100.Tag = "0x000100";
			this.chk000100.Text = " ";
			this.chk000100.UseVisualStyleBackColor = true;
			this.chk000100.Visible = false;
			this.chk000100.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk000002
			// 
			this.chk000002.AutoSize = true;
			this.chk000002.Location = new System.Drawing.Point(16, 70);
			this.chk000002.Name = "chk000002";
			this.chk000002.Size = new System.Drawing.Size(29, 17);
			this.chk000002.TabIndex = 40;
			this.chk000002.Tag = "0x000002";
			this.chk000002.Text = " ";
			this.chk000002.UseVisualStyleBackColor = true;
			this.chk000002.Visible = false;
			this.chk000002.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk000200
			// 
			this.chk000200.AutoSize = true;
			this.chk000200.Location = new System.Drawing.Point(16, 230);
			this.chk000200.Name = "chk000200";
			this.chk000200.Size = new System.Drawing.Size(29, 17);
			this.chk000200.TabIndex = 38;
			this.chk000200.Tag = "0x000200";
			this.chk000200.Text = " ";
			this.chk000200.UseVisualStyleBackColor = true;
			this.chk000200.Visible = false;
			this.chk000200.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk000080
			// 
			this.chk000080.AutoSize = true;
			this.chk000080.Location = new System.Drawing.Point(16, 190);
			this.chk000080.Name = "chk000080";
			this.chk000080.Size = new System.Drawing.Size(29, 17);
			this.chk000080.TabIndex = 37;
			this.chk000080.Tag = "0x000080";
			this.chk000080.Text = " ";
			this.chk000080.UseVisualStyleBackColor = true;
			this.chk000080.Visible = false;
			this.chk000080.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk000800
			// 
			this.chk000800.AutoSize = true;
			this.chk000800.Location = new System.Drawing.Point(16, 270);
			this.chk000800.Name = "chk000800";
			this.chk000800.Size = new System.Drawing.Size(29, 17);
			this.chk000800.TabIndex = 36;
			this.chk000800.Tag = "0x000800";
			this.chk000800.Text = " ";
			this.chk000800.UseVisualStyleBackColor = true;
			this.chk000800.Visible = false;
			this.chk000800.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk000400
			// 
			this.chk000400.AutoSize = true;
			this.chk000400.Location = new System.Drawing.Point(16, 250);
			this.chk000400.Name = "chk000400";
			this.chk000400.Size = new System.Drawing.Size(29, 17);
			this.chk000400.TabIndex = 34;
			this.chk000400.Tag = "0x000400";
			this.chk000400.Text = " ";
			this.chk000400.UseVisualStyleBackColor = true;
			this.chk000400.Visible = false;
			this.chk000400.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk000001
			// 
			this.chk000001.AutoSize = true;
			this.chk000001.Cursor = System.Windows.Forms.Cursors.Default;
			this.chk000001.Location = new System.Drawing.Point(16, 50);
			this.chk000001.Name = "chk000001";
			this.chk000001.Size = new System.Drawing.Size(29, 17);
			this.chk000001.TabIndex = 33;
			this.chk000001.Tag = "0x000001";
			this.chk000001.Text = " ";
			this.chk000001.UseVisualStyleBackColor = true;
			this.chk000001.Visible = false;
			this.chk000001.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk000040
			// 
			this.chk000040.AutoSize = true;
			this.chk000040.Location = new System.Drawing.Point(16, 170);
			this.chk000040.Name = "chk000040";
			this.chk000040.Size = new System.Drawing.Size(29, 17);
			this.chk000040.TabIndex = 32;
			this.chk000040.Tag = "0x000040";
			this.chk000040.Text = " ";
			this.chk000040.UseVisualStyleBackColor = true;
			this.chk000040.Visible = false;
			this.chk000040.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk000010
			// 
			this.chk000010.AutoSize = true;
			this.chk000010.Location = new System.Drawing.Point(16, 130);
			this.chk000010.Name = "chk000010";
			this.chk000010.Size = new System.Drawing.Size(29, 17);
			this.chk000010.TabIndex = 31;
			this.chk000010.Tag = "0x000010";
			this.chk000010.Text = " ";
			this.chk000010.UseVisualStyleBackColor = true;
			this.chk000010.Visible = false;
			this.chk000010.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk000008
			// 
			this.chk000008.AutoSize = true;
			this.chk000008.Location = new System.Drawing.Point(16, 110);
			this.chk000008.Name = "chk000008";
			this.chk000008.Size = new System.Drawing.Size(29, 17);
			this.chk000008.TabIndex = 30;
			this.chk000008.Tag = "0x000008";
			this.chk000008.Text = " ";
			this.chk000008.UseVisualStyleBackColor = true;
			this.chk000008.Visible = false;
			this.chk000008.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// chk000004
			// 
			this.chk000004.AutoSize = true;
			this.chk000004.Location = new System.Drawing.Point(16, 90);
			this.chk000004.Name = "chk000004";
			this.chk000004.Size = new System.Drawing.Size(29, 17);
			this.chk000004.TabIndex = 29;
			this.chk000004.Tag = "0x000004";
			this.chk000004.Text = " ";
			this.chk000004.UseVisualStyleBackColor = true;
			this.chk000004.Visible = false;
			this.chk000004.CheckedChanged += new System.EventHandler(this.chkFinal_Changed);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 28;
			this.label1.Text = "Algorithms";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// lblFinalMatchSlider
			// 
			this.lblFinalMatchSlider.AutoSize = true;
			this.lblFinalMatchSlider.Location = new System.Drawing.Point(16, 298);
			this.lblFinalMatchSlider.Name = "lblFinalMatchSlider";
			this.lblFinalMatchSlider.Size = new System.Drawing.Size(79, 13);
			this.lblFinalMatchSlider.TabIndex = 27;
			this.lblFinalMatchSlider.Text = "Minimum Score";
			this.lblFinalMatchSlider.Click += new System.EventHandler(this.lblFinalMatchScore_Click);
			// 
			// sldFinalMatch
			// 
			this.sldFinalMatch.Location = new System.Drawing.Point(19, 314);
			this.sldFinalMatch.Maximum = 99;
			this.sldFinalMatch.Minimum = 80;
			this.sldFinalMatch.Name = "sldFinalMatch";
			this.sldFinalMatch.Size = new System.Drawing.Size(182, 45);
			this.sldFinalMatch.TabIndex = 26;
			this.sldFinalMatch.Value = 95;
			this.sldFinalMatch.Scroll += new System.EventHandler(this.sldFinalMatch_Scroll);
			// 
			// btnDefaults
			// 
			this.btnDefaults.Location = new System.Drawing.Point(12, 264);
			this.btnDefaults.Name = "btnDefaults";
			this.btnDefaults.Size = new System.Drawing.Size(105, 23);
			this.btnDefaults.TabIndex = 24;
			this.btnDefaults.Text = "Use Defaults";
			this.btnDefaults.UseVisualStyleBackColor = true;
			this.btnDefaults.Click += new System.EventHandler(this.btnDefaults_Click);
			// 
			// chkCase
			// 
			this.chkCase.AutoSize = true;
			this.chkCase.Checked = true;
			this.chkCase.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCase.Location = new System.Drawing.Point(22, 34);
			this.chkCase.Name = "chkCase";
			this.chkCase.Size = new System.Drawing.Size(96, 17);
			this.chkCase.TabIndex = 25;
			this.chkCase.Text = "Case Sensitive";
			this.chkCase.UseVisualStyleBackColor = true;
			this.chkCase.CheckedChanged += new System.EventHandler(this.chkCase_CheckedChanged);
			// 
			// chkLog
			// 
			this.chkLog.AutoSize = true;
			this.chkLog.Checked = true;
			this.chkLog.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLog.Location = new System.Drawing.Point(22, 224);
			this.chkLog.Name = "chkLog";
			this.chkLog.Size = new System.Drawing.Size(91, 17);
			this.chkLog.TabIndex = 26;
			this.chkLog.Text = "Write Log File";
			this.chkLog.UseVisualStyleBackColor = true;
			this.chkLog.CheckedChanged += new System.EventHandler(this.chkLog_CheckedChanged);
			// 
			// frmOptions
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(704, 444);
			this.Controls.Add(this.chkLog);
			this.Controls.Add(this.chkCase);
			this.Controls.Add(this.btnDefaults);
			this.Controls.Add(this.grpFinalMatch);
			this.Controls.Add(this.grmPrematch);
			this.Controls.Add(this.chkUseFuzzy);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmOptions";
			this.ShowInTaskbar = false;
			this.Text = "Match Options";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOptions_FormClosing);
			this.Load += new System.EventHandler(this.frmOptions_Load);
			this.grmPrematch.ResumeLayout(false);
			this.grmPrematch.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldPrematch)).EndInit();
			this.grpFinalMatch.ResumeLayout(false);
			this.grpFinalMatch.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldFinalMatch)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.CheckBox chkUseFuzzy;
		private System.Windows.Forms.GroupBox grmPrematch;
		private System.Windows.Forms.Label lblMinPrematchSlider;
		private System.Windows.Forms.TrackBar sldPrematch;
		private System.Windows.Forms.ComboBox cboPrematch;
		private System.Windows.Forms.Label lblPrematch;
		private System.Windows.Forms.GroupBox grpFinalMatch;
		private System.Windows.Forms.CheckBox chk000200;
		private System.Windows.Forms.CheckBox chk000080;
		private System.Windows.Forms.CheckBox chk000800;
		private System.Windows.Forms.CheckBox chk000400;
		private System.Windows.Forms.CheckBox chk000001;
		private System.Windows.Forms.CheckBox chk000040;
		private System.Windows.Forms.CheckBox chk000010;
		private System.Windows.Forms.CheckBox chk000008;
		private System.Windows.Forms.CheckBox chk000004;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblFinalMatchSlider;
		private System.Windows.Forms.TrackBar sldFinalMatch;
		private System.Windows.Forms.CheckBox chk000002;
		private System.Windows.Forms.Button btnDefaults;
		private System.Windows.Forms.CheckBox chkCase;
		private System.Windows.Forms.CheckBox chkLog;
		private System.Windows.Forms.CheckBox chk000100;
		private System.Windows.Forms.CheckBox chk000020;
		private System.Windows.Forms.Label lblPrematchValue;
		private System.Windows.Forms.Label lblFinalMatchValue;
		private System.Windows.Forms.CheckBox chk020000;
		private System.Windows.Forms.CheckBox chk100000;
		private System.Windows.Forms.CheckBox chk002000;
		private System.Windows.Forms.CheckBox chk200000;
		private System.Windows.Forms.CheckBox chk080000;
		private System.Windows.Forms.CheckBox chk800000;
		private System.Windows.Forms.CheckBox chk400000;
		private System.Windows.Forms.CheckBox chk001000;
		private System.Windows.Forms.CheckBox chk040000;
		private System.Windows.Forms.CheckBox chk010000;
		private System.Windows.Forms.CheckBox chk008000;
		private System.Windows.Forms.CheckBox chk004000;
	}
}