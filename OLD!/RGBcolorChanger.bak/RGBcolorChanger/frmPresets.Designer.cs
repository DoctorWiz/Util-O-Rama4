namespace RGBcolorChanger
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
            this.cboPresets = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.pnlColors = new System.Windows.Forms.Panel();
            this.grpColor = new System.Windows.Forms.GroupBox();
            this.lblFromName = new System.Windows.Forms.Label();
            this.lblToName = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblFromR = new System.Windows.Forms.Label();
            this.numFromR = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numFromG = new System.Windows.Forms.NumericUpDown();
            this.lblFromG = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numFromB = new System.Windows.Forms.NumericUpDown();
            this.lblFromB = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numToR = new System.Windows.Forms.NumericUpDown();
            this.lblToR = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.numToG = new System.Windows.Forms.NumericUpDown();
            this.lblToG = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.numToB = new System.Windows.Forms.NumericUpDown();
            this.lblToB = new System.Windows.Forms.Label();
            this.picFromColor = new System.Windows.Forms.PictureBox();
            this.picToColor = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPreset = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pnlColors.SuspendLayout();
            this.grpColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFromR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFromColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picToColor)).BeginInit();
            this.SuspendLayout();
            // 
            // cboPresets
            // 
            this.cboPresets.FormattingEnabled = true;
            this.cboPresets.Location = new System.Drawing.Point(12, 25);
            this.cboPresets.Name = "cboPresets";
            this.cboPresets.Size = new System.Drawing.Size(241, 21);
            this.cboPresets.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(259, 25);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(54, 21);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(319, 25);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(62, 21);
            this.btnAddNew.TabIndex = 2;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            // 
            // pnlColors
            // 
            this.pnlColors.Controls.Add(this.grpColor);
            this.pnlColors.Location = new System.Drawing.Point(12, 78);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Size = new System.Drawing.Size(761, 340);
            this.pnlColors.TabIndex = 3;
            // 
            // grpColor
            // 
            this.grpColor.Controls.Add(this.lblInfo);
            this.grpColor.Controls.Add(this.picToColor);
            this.grpColor.Controls.Add(this.picFromColor);
            this.grpColor.Controls.Add(this.label13);
            this.grpColor.Controls.Add(this.numToB);
            this.grpColor.Controls.Add(this.lblToB);
            this.grpColor.Controls.Add(this.label11);
            this.grpColor.Controls.Add(this.numToG);
            this.grpColor.Controls.Add(this.lblToG);
            this.grpColor.Controls.Add(this.label9);
            this.grpColor.Controls.Add(this.numToR);
            this.grpColor.Controls.Add(this.lblToR);
            this.grpColor.Controls.Add(this.label7);
            this.grpColor.Controls.Add(this.numFromB);
            this.grpColor.Controls.Add(this.lblFromB);
            this.grpColor.Controls.Add(this.label5);
            this.grpColor.Controls.Add(this.numFromG);
            this.grpColor.Controls.Add(this.lblFromG);
            this.grpColor.Controls.Add(this.label4);
            this.grpColor.Controls.Add(this.numFromR);
            this.grpColor.Controls.Add(this.lblFromR);
            this.grpColor.Controls.Add(this.textBox2);
            this.grpColor.Controls.Add(this.textBox1);
            this.grpColor.Controls.Add(this.lblToName);
            this.grpColor.Controls.Add(this.lblFromName);
            this.grpColor.Location = new System.Drawing.Point(3, 3);
            this.grpColor.Name = "grpColor";
            this.grpColor.Size = new System.Drawing.Size(680, 66);
            this.grpColor.TabIndex = 0;
            this.grpColor.TabStop = false;
            this.grpColor.Text = " Color 1 ";
            // 
            // lblFromName
            // 
            this.lblFromName.AutoSize = true;
            this.lblFromName.Location = new System.Drawing.Point(6, 16);
            this.lblFromName.Name = "lblFromName";
            this.lblFromName.Size = new System.Drawing.Size(30, 13);
            this.lblFromName.TabIndex = 0;
            this.lblFromName.Text = "From";
            // 
            // lblToName
            // 
            this.lblToName.AutoSize = true;
            this.lblToName.Location = new System.Drawing.Point(377, 16);
            this.lblToName.Name = "lblToName";
            this.lblToName.Size = new System.Drawing.Size(20, 13);
            this.lblToName.TabIndex = 1;
            this.lblToName.Text = "To";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(42, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(87, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "White";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(410, 13);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(87, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "Black";
            // 
            // lblFromR
            // 
            this.lblFromR.AutoSize = true;
            this.lblFromR.Location = new System.Drawing.Point(6, 41);
            this.lblFromR.Name = "lblFromR";
            this.lblFromR.Size = new System.Drawing.Size(27, 13);
            this.lblFromR.TabIndex = 4;
            this.lblFromR.Text = "Red";
            // 
            // numFromR
            // 
            this.numFromR.Location = new System.Drawing.Point(42, 39);
            this.numFromR.Name = "numFromR";
            this.numFromR.Size = new System.Drawing.Size(41, 20);
            this.numFromR.TabIndex = 5;
            this.numFromR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFromR.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(209, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "%";
            // 
            // numFromG
            // 
            this.numFromG.Location = new System.Drawing.Point(162, 39);
            this.numFromG.Name = "numFromG";
            this.numFromG.Size = new System.Drawing.Size(41, 20);
            this.numFromG.TabIndex = 8;
            this.numFromG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFromG.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblFromG
            // 
            this.lblFromG.AutoSize = true;
            this.lblFromG.Location = new System.Drawing.Point(120, 41);
            this.lblFromG.Name = "lblFromG";
            this.lblFromG.Size = new System.Drawing.Size(36, 13);
            this.lblFromG.TabIndex = 7;
            this.lblFromG.Text = "Green";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(322, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "%";
            // 
            // numFromB
            // 
            this.numFromB.Location = new System.Drawing.Point(275, 39);
            this.numFromB.Name = "numFromB";
            this.numFromB.Size = new System.Drawing.Size(41, 20);
            this.numFromB.TabIndex = 11;
            this.numFromB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFromB.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblFromB
            // 
            this.lblFromB.AutoSize = true;
            this.lblFromB.Location = new System.Drawing.Point(241, 41);
            this.lblFromB.Name = "lblFromB";
            this.lblFromB.Size = new System.Drawing.Size(28, 13);
            this.lblFromB.TabIndex = 10;
            this.lblFromB.Text = "Blue";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(457, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "%";
            // 
            // numToR
            // 
            this.numToR.Location = new System.Drawing.Point(410, 39);
            this.numToR.Name = "numToR";
            this.numToR.Size = new System.Drawing.Size(41, 20);
            this.numToR.TabIndex = 14;
            this.numToR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblToR
            // 
            this.lblToR.AutoSize = true;
            this.lblToR.Location = new System.Drawing.Point(377, 41);
            this.lblToR.Name = "lblToR";
            this.lblToR.Size = new System.Drawing.Size(27, 13);
            this.lblToR.TabIndex = 13;
            this.lblToR.Text = "Red";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(561, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "%";
            // 
            // numToG
            // 
            this.numToG.Location = new System.Drawing.Point(514, 39);
            this.numToG.Name = "numToG";
            this.numToG.Size = new System.Drawing.Size(41, 20);
            this.numToG.TabIndex = 17;
            this.numToG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblToG
            // 
            this.lblToG.AutoSize = true;
            this.lblToG.Location = new System.Drawing.Point(478, 41);
            this.lblToG.Name = "lblToG";
            this.lblToG.Size = new System.Drawing.Size(36, 13);
            this.lblToG.TabIndex = 16;
            this.lblToG.Text = "Green";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(653, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "%";
            // 
            // numToB
            // 
            this.numToB.Location = new System.Drawing.Point(606, 39);
            this.numToB.Name = "numToB";
            this.numToB.Size = new System.Drawing.Size(41, 20);
            this.numToB.TabIndex = 20;
            this.numToB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblToB
            // 
            this.lblToB.AutoSize = true;
            this.lblToB.Location = new System.Drawing.Point(582, 41);
            this.lblToB.Name = "lblToB";
            this.lblToB.Size = new System.Drawing.Size(28, 13);
            this.lblToB.TabIndex = 19;
            this.lblToB.Text = "Blue";
            // 
            // picFromColor
            // 
            this.picFromColor.Location = new System.Drawing.Point(135, 12);
            this.picFromColor.Name = "picFromColor";
            this.picFromColor.Size = new System.Drawing.Size(33, 21);
            this.picFromColor.TabIndex = 22;
            this.picFromColor.TabStop = false;
            // 
            // picToColor
            // 
            this.picToColor.Location = new System.Drawing.Point(503, 12);
            this.picToColor.Name = "picToColor";
            this.picToColor.Size = new System.Drawing.Size(33, 21);
            this.picToColor.TabIndex = 23;
            this.picToColor.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(249, 432);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(81, 28);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(371, 432);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblPreset
            // 
            this.lblPreset.AutoSize = true;
            this.lblPreset.Location = new System.Drawing.Point(12, 9);
            this.lblPreset.Name = "lblPreset";
            this.lblPreset.Size = new System.Drawing.Size(45, 13);
            this.lblPreset.TabIndex = 6;
            this.lblPreset.Text = "Presets:";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(177, 18);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(180, 12);
            this.lblInfo.TabIndex = 24;
            this.lblInfo.Text = "(Color name is for your own reference only)";
            // 
            // frmPresets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 474);
            this.Controls.Add(this.lblPreset);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pnlColors);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cboPresets);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmPresets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Color Change Presets";
            this.pnlColors.ResumeLayout(false);
            this.grpColor.ResumeLayout(false);
            this.grpColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFromR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFromColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picToColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboPresets;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Panel pnlColors;
        private System.Windows.Forms.GroupBox grpColor;
        private System.Windows.Forms.PictureBox picToColor;
        private System.Windows.Forms.PictureBox picFromColor;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numToB;
        private System.Windows.Forms.Label lblToB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numToG;
        private System.Windows.Forms.Label lblToG;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numToR;
        private System.Windows.Forms.Label lblToR;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numFromB;
        private System.Windows.Forms.Label lblFromB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numFromG;
        private System.Windows.Forms.Label lblFromG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numFromR;
        private System.Windows.Forms.Label lblFromR;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblToName;
        private System.Windows.Forms.Label lblFromName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPreset;
        private System.Windows.Forms.Label lblInfo;
    }
}