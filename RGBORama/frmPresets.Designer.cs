namespace RGBORama
{
    partial class frmPresets
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPresets));
			this.cboPresets = new System.Windows.Forms.ComboBox();
			this.pnlColors = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnDel = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.grpColor01 = new System.Windows.Forms.GroupBox();
			this.picReplColor01 = new System.Windows.Forms.PictureBox();
			this.lblToName01 = new System.Windows.Forms.Label();
			this.txtFindColorName01 = new System.Windows.Forms.TextBox();
			this.lblInfo01 = new System.Windows.Forms.Label();
			this.picFindColor01 = new System.Windows.Forms.PictureBox();
			this.lblPctToB01 = new System.Windows.Forms.Label();
			this.numReplBluPct01 = new System.Windows.Forms.NumericUpDown();
			this.lblToB01 = new System.Windows.Forms.Label();
			this.lblPctToG01 = new System.Windows.Forms.Label();
			this.numReplGrnPct01 = new System.Windows.Forms.NumericUpDown();
			this.lblToG01 = new System.Windows.Forms.Label();
			this.lblPctToR01 = new System.Windows.Forms.Label();
			this.numReplRedPct01 = new System.Windows.Forms.NumericUpDown();
			this.lblToR01 = new System.Windows.Forms.Label();
			this.lblPctFrB01 = new System.Windows.Forms.Label();
			this.numFindBluPct01 = new System.Windows.Forms.NumericUpDown();
			this.lblFromB01 = new System.Windows.Forms.Label();
			this.lblPctFrG01 = new System.Windows.Forms.Label();
			this.numFindGrnPct01 = new System.Windows.Forms.NumericUpDown();
			this.lblFromG01 = new System.Windows.Forms.Label();
			this.lblPctFrR01 = new System.Windows.Forms.Label();
			this.numFindRedPct01 = new System.Windows.Forms.NumericUpDown();
			this.lblFromR01 = new System.Windows.Forms.Label();
			this.txtReplColorName01 = new System.Windows.Forms.TextBox();
			this.lblFromName01 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblPreset = new System.Windows.Forms.Label();
			this.toolTips = new System.Windows.Forms.ToolTip(this.components);
			this.cboColorNames = new System.Windows.Forms.ComboBox();
			this.mnuStrip = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFavorite = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuShowFavs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuShowAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.pnlColors.SuspendLayout();
			this.panel1.SuspendLayout();
			this.grpColor01.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picReplColor01)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picFindColor01)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numReplBluPct01)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numReplGrnPct01)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numReplRedPct01)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFindBluPct01)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFindGrnPct01)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFindRedPct01)).BeginInit();
			this.mnuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboPresets
			// 
			this.cboPresets.FormattingEnabled = true;
			resources.ApplyResources(this.cboPresets, "cboPresets");
			this.cboPresets.Name = "cboPresets";
			// 
			// pnlColors
			// 
			resources.ApplyResources(this.pnlColors, "pnlColors");
			this.pnlColors.Controls.Add(this.panel1);
			this.pnlColors.Controls.Add(this.grpColor01);
			this.pnlColors.Name = "pnlColors";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnAdd);
			this.panel1.Controls.Add(this.btnDel);
			this.panel1.Controls.Add(this.btnDown);
			this.panel1.Controls.Add(this.btnUp);
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			// 
			// btnAdd
			// 
			resources.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.UseVisualStyleBackColor = true;
			// 
			// btnDel
			// 
			resources.ApplyResources(this.btnDel, "btnDel");
			this.btnDel.Name = "btnDel";
			this.btnDel.UseVisualStyleBackColor = true;
			// 
			// btnDown
			// 
			resources.ApplyResources(this.btnDown, "btnDown");
			this.btnDown.Name = "btnDown";
			this.btnDown.UseVisualStyleBackColor = true;
			// 
			// btnUp
			// 
			resources.ApplyResources(this.btnUp, "btnUp");
			this.btnUp.Name = "btnUp";
			this.btnUp.UseVisualStyleBackColor = true;
			// 
			// grpColor01
			// 
			this.grpColor01.Controls.Add(this.picReplColor01);
			this.grpColor01.Controls.Add(this.lblToName01);
			this.grpColor01.Controls.Add(this.txtFindColorName01);
			this.grpColor01.Controls.Add(this.lblInfo01);
			this.grpColor01.Controls.Add(this.picFindColor01);
			this.grpColor01.Controls.Add(this.lblPctToB01);
			this.grpColor01.Controls.Add(this.numReplBluPct01);
			this.grpColor01.Controls.Add(this.lblToB01);
			this.grpColor01.Controls.Add(this.lblPctToG01);
			this.grpColor01.Controls.Add(this.numReplGrnPct01);
			this.grpColor01.Controls.Add(this.lblToG01);
			this.grpColor01.Controls.Add(this.lblPctToR01);
			this.grpColor01.Controls.Add(this.numReplRedPct01);
			this.grpColor01.Controls.Add(this.lblToR01);
			this.grpColor01.Controls.Add(this.lblPctFrB01);
			this.grpColor01.Controls.Add(this.numFindBluPct01);
			this.grpColor01.Controls.Add(this.lblFromB01);
			this.grpColor01.Controls.Add(this.lblPctFrG01);
			this.grpColor01.Controls.Add(this.numFindGrnPct01);
			this.grpColor01.Controls.Add(this.lblFromG01);
			this.grpColor01.Controls.Add(this.lblPctFrR01);
			this.grpColor01.Controls.Add(this.numFindRedPct01);
			this.grpColor01.Controls.Add(this.lblFromR01);
			this.grpColor01.Controls.Add(this.txtReplColorName01);
			this.grpColor01.Controls.Add(this.lblFromName01);
			resources.ApplyResources(this.grpColor01, "grpColor01");
			this.grpColor01.Name = "grpColor01";
			this.grpColor01.TabStop = false;
			// 
			// picReplColor01
			// 
			resources.ApplyResources(this.picReplColor01, "picReplColor01");
			this.picReplColor01.Name = "picReplColor01";
			this.picReplColor01.TabStop = false;
			this.picReplColor01.Click += new System.EventHandler(this.picColor_Click);
			// 
			// lblToName01
			// 
			resources.ApplyResources(this.lblToName01, "lblToName01");
			this.lblToName01.Name = "lblToName01";
			// 
			// txtFindColorName01
			// 
			resources.ApplyResources(this.txtFindColorName01, "txtFindColorName01");
			this.txtFindColorName01.Name = "txtFindColorName01";
			// 
			// lblInfo01
			// 
			resources.ApplyResources(this.lblInfo01, "lblInfo01");
			this.lblInfo01.Name = "lblInfo01";
			// 
			// picFindColor01
			// 
			resources.ApplyResources(this.picFindColor01, "picFindColor01");
			this.picFindColor01.Name = "picFindColor01";
			this.picFindColor01.TabStop = false;
			this.picFindColor01.Click += new System.EventHandler(this.picColor_Click);
			// 
			// lblPctToB01
			// 
			resources.ApplyResources(this.lblPctToB01, "lblPctToB01");
			this.lblPctToB01.Name = "lblPctToB01";
			// 
			// numReplBluPct01
			// 
			resources.ApplyResources(this.numReplBluPct01, "numReplBluPct01");
			this.numReplBluPct01.Name = "numReplBluPct01";
			this.numReplBluPct01.ValueChanged += new System.EventHandler(this.num_ValueChanged);
			// 
			// lblToB01
			// 
			resources.ApplyResources(this.lblToB01, "lblToB01");
			this.lblToB01.Name = "lblToB01";
			// 
			// lblPctToG01
			// 
			resources.ApplyResources(this.lblPctToG01, "lblPctToG01");
			this.lblPctToG01.Name = "lblPctToG01";
			// 
			// numReplGrnPct01
			// 
			resources.ApplyResources(this.numReplGrnPct01, "numReplGrnPct01");
			this.numReplGrnPct01.Name = "numReplGrnPct01";
			this.numReplGrnPct01.ValueChanged += new System.EventHandler(this.num_ValueChanged);
			// 
			// lblToG01
			// 
			resources.ApplyResources(this.lblToG01, "lblToG01");
			this.lblToG01.Name = "lblToG01";
			// 
			// lblPctToR01
			// 
			resources.ApplyResources(this.lblPctToR01, "lblPctToR01");
			this.lblPctToR01.Name = "lblPctToR01";
			// 
			// numReplRedPct01
			// 
			resources.ApplyResources(this.numReplRedPct01, "numReplRedPct01");
			this.numReplRedPct01.Name = "numReplRedPct01";
			this.numReplRedPct01.ValueChanged += new System.EventHandler(this.num_ValueChanged);
			// 
			// lblToR01
			// 
			resources.ApplyResources(this.lblToR01, "lblToR01");
			this.lblToR01.Name = "lblToR01";
			// 
			// lblPctFrB01
			// 
			resources.ApplyResources(this.lblPctFrB01, "lblPctFrB01");
			this.lblPctFrB01.Name = "lblPctFrB01";
			// 
			// numFindBluPct01
			// 
			resources.ApplyResources(this.numFindBluPct01, "numFindBluPct01");
			this.numFindBluPct01.Name = "numFindBluPct01";
			this.numFindBluPct01.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numFindBluPct01.ValueChanged += new System.EventHandler(this.num_ValueChanged);
			// 
			// lblFromB01
			// 
			resources.ApplyResources(this.lblFromB01, "lblFromB01");
			this.lblFromB01.Name = "lblFromB01";
			// 
			// lblPctFrG01
			// 
			resources.ApplyResources(this.lblPctFrG01, "lblPctFrG01");
			this.lblPctFrG01.Name = "lblPctFrG01";
			// 
			// numFindGrnPct01
			// 
			resources.ApplyResources(this.numFindGrnPct01, "numFindGrnPct01");
			this.numFindGrnPct01.Name = "numFindGrnPct01";
			this.numFindGrnPct01.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numFindGrnPct01.ValueChanged += new System.EventHandler(this.num_ValueChanged);
			// 
			// lblFromG01
			// 
			resources.ApplyResources(this.lblFromG01, "lblFromG01");
			this.lblFromG01.Name = "lblFromG01";
			// 
			// lblPctFrR01
			// 
			resources.ApplyResources(this.lblPctFrR01, "lblPctFrR01");
			this.lblPctFrR01.Name = "lblPctFrR01";
			// 
			// numFindRedPct01
			// 
			resources.ApplyResources(this.numFindRedPct01, "numFindRedPct01");
			this.numFindRedPct01.Name = "numFindRedPct01";
			this.numFindRedPct01.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numFindRedPct01.ValueChanged += new System.EventHandler(this.num_ValueChanged);
			// 
			// lblFromR01
			// 
			resources.ApplyResources(this.lblFromR01, "lblFromR01");
			this.lblFromR01.Name = "lblFromR01";
			// 
			// txtReplColorName01
			// 
			resources.ApplyResources(this.txtReplColorName01, "txtReplColorName01");
			this.txtReplColorName01.Name = "txtReplColorName01";
			this.txtReplColorName01.Click += new System.EventHandler(this.txtColorName_Click);
			// 
			// lblFromName01
			// 
			resources.ApplyResources(this.lblFromName01, "lblFromName01");
			this.lblFromName01.Name = "lblFromName01";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.Name = "btnOK";
			this.toolTips.SetToolTip(this.btnOK, resources.GetString("btnOK.ToolTip"));
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.Name = "btnCancel";
			this.toolTips.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblPreset
			// 
			resources.ApplyResources(this.lblPreset, "lblPreset");
			this.lblPreset.Name = "lblPreset";
			// 
			// cboColorNames
			// 
			this.cboColorNames.FormattingEnabled = true;
			resources.ApplyResources(this.cboColorNames, "cboColorNames");
			this.cboColorNames.Name = "cboColorNames";
			// 
			// mnuStrip
			// 
			this.mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
			resources.ApplyResources(this.mnuStrip, "mnuStrip");
			this.mnuStrip.Name = "mnuStrip";
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.toolStripSeparator1,
            this.mnuOpen,
            this.toolStripSeparator2,
            this.mnuSave,
            this.mnuSaveAs,
            this.toolStripSeparator3,
            this.mnuRename,
            this.mnuDelete,
            this.toolStripSeparator4,
            this.mnuFavorite,
            this.toolStripSeparator5,
            this.mnuShowFavs,
            this.mnuShowAll});
			this.mnuFile.Name = "mnuFile";
			resources.ApplyResources(this.mnuFile, "mnuFile");
			this.mnuFile.Click += new System.EventHandler(this.mnuFile_Click);
			// 
			// mnuOpen
			// 
			this.mnuOpen.Name = "mnuOpen";
			resources.ApplyResources(this.mnuOpen, "mnuOpen");
			this.mnuOpen.Tag = "O";
			this.mnuOpen.Click += new System.EventHandler(this.mnuFile_Click);
			// 
			// mnuSave
			// 
			resources.ApplyResources(this.mnuSave, "mnuSave");
			this.mnuSave.Name = "mnuSave";
			this.mnuSave.Tag = "S";
			this.mnuSave.Click += new System.EventHandler(this.mnuFile_Click);
			// 
			// mnuSaveAs
			// 
			this.mnuSaveAs.Name = "mnuSaveAs";
			resources.ApplyResources(this.mnuSaveAs, "mnuSaveAs");
			this.mnuSaveAs.Tag = "A";
			this.mnuSaveAs.Click += new System.EventHandler(this.mnuFile_Click);
			// 
			// mnuRename
			// 
			this.mnuRename.Name = "mnuRename";
			resources.ApplyResources(this.mnuRename, "mnuRename");
			this.mnuRename.Tag = "R";
			this.mnuRename.Click += new System.EventHandler(this.mnuFile_Click);
			// 
			// mnuDelete
			// 
			this.mnuDelete.Name = "mnuDelete";
			resources.ApplyResources(this.mnuDelete, "mnuDelete");
			this.mnuDelete.Tag = "D";
			this.mnuDelete.Click += new System.EventHandler(this.mnuFile_Click);
			// 
			// mnuNew
			// 
			this.mnuNew.Name = "mnuNew";
			resources.ApplyResources(this.mnuNew, "mnuNew");
			this.mnuNew.Tag = "N";
			this.mnuNew.Click += new System.EventHandler(this.mnuFile_Click);
			// 
			// mnuFavorite
			// 
			this.mnuFavorite.Name = "mnuFavorite";
			resources.ApplyResources(this.mnuFavorite, "mnuFavorite");
			this.mnuFavorite.Tag = "F";
			this.mnuFavorite.Click += new System.EventHandler(this.mnuFile_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
			// 
			// mnuShowFavs
			// 
			this.mnuShowFavs.Name = "mnuShowFavs";
			resources.ApplyResources(this.mnuShowFavs, "mnuShowFavs");
			this.mnuShowFavs.Tag = "V";
			this.mnuShowFavs.Click += new System.EventHandler(this.mnuFile_Click);
			// 
			// mnuShowAll
			// 
			this.mnuShowAll.Name = "mnuShowAll";
			resources.ApplyResources(this.mnuShowAll, "mnuShowAll");
			this.mnuShowAll.Tag = "L";
			this.mnuShowAll.Click += new System.EventHandler(this.mnuFile_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
			// 
			// frmPresets
			// 
			this.AcceptButton = this.btnOK;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ControlBox = false;
			this.Controls.Add(this.cboColorNames);
			this.Controls.Add(this.lblPreset);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.pnlColors);
			this.Controls.Add(this.cboPresets);
			this.Controls.Add(this.mnuStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MainMenuStrip = this.mnuStrip;
			this.MaximizeBox = false;
			this.Name = "frmPresets";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPresets_FormClosing);
			this.Load += new System.EventHandler(this.frmPresets_Load);
			this.pnlColors.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.grpColor01.ResumeLayout(false);
			this.grpColor01.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picReplColor01)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picFindColor01)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numReplBluPct01)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numReplGrnPct01)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numReplRedPct01)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFindBluPct01)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFindGrnPct01)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFindRedPct01)).EndInit();
			this.mnuStrip.ResumeLayout(false);
			this.mnuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboPresets;
        private System.Windows.Forms.Panel pnlColors;
        private System.Windows.Forms.GroupBox grpColor01;
        private System.Windows.Forms.PictureBox picReplColor01;
        private System.Windows.Forms.PictureBox picFindColor01;
        private System.Windows.Forms.Label lblPctToB01;
        private System.Windows.Forms.NumericUpDown numReplBluPct01;
        private System.Windows.Forms.Label lblToB01;
        private System.Windows.Forms.Label lblPctToG01;
        private System.Windows.Forms.NumericUpDown numReplGrnPct01;
        private System.Windows.Forms.Label lblToG01;
        private System.Windows.Forms.Label lblPctToR01;
        private System.Windows.Forms.NumericUpDown numReplRedPct01;
        private System.Windows.Forms.Label lblToR01;
        private System.Windows.Forms.Label lblPctFrB01;
        private System.Windows.Forms.NumericUpDown numFindBluPct01;
        private System.Windows.Forms.Label lblFromB01;
        private System.Windows.Forms.Label lblPctFrG01;
        private System.Windows.Forms.NumericUpDown numFindGrnPct01;
        private System.Windows.Forms.Label lblFromG01;
        private System.Windows.Forms.Label lblPctFrR01;
        private System.Windows.Forms.NumericUpDown numFindRedPct01;
        private System.Windows.Forms.Label lblFromR01;
        private System.Windows.Forms.TextBox txtReplColorName01;
        private System.Windows.Forms.Label lblToName01;
        private System.Windows.Forms.Label lblFromName01;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPreset;
        private System.Windows.Forms.Label lblInfo01;
		private System.Windows.Forms.ToolTip toolTips;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.TextBox txtFindColorName01;
		private System.Windows.Forms.ComboBox cboColorNames;
		private System.Windows.Forms.MenuStrip mnuStrip;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuNew;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem mnuOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem mnuSave;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem mnuRename;
		private System.Windows.Forms.ToolStripMenuItem mnuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem mnuFavorite;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem mnuShowFavs;
		private System.Windows.Forms.ToolStripMenuItem mnuShowAll;
	}
}