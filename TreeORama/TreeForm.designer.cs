namespace TreeORama
{
    partial class frmTree
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTree));
			this.txtFile = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.fileLabel = new System.Windows.Forms.Label();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.btnNextPixel = new System.Windows.Forms.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.txtPixel = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.txtRow = new System.Windows.Forms.TextBox();
			this.lblRow = new System.Windows.Forms.Label();
			this.txtCol = new System.Windows.Forms.TextBox();
			this.lblCol = new System.Windows.Forms.Label();
			this.txtPx = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtDir = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtFace = new System.Windows.Forms.TextBox();
			this.btnNextCol = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.lblPixelName = new System.Windows.Forms.Label();
			this.txtCh = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtUniv = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.lstPixelNames = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnAuto = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtFile
			// 
			this.txtFile.Enabled = false;
			this.txtFile.Location = new System.Drawing.Point(19, 25);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(223, 20);
			this.txtFile.TabIndex = 0;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(246, 25);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(26, 20);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// fileLabel
			// 
			this.fileLabel.AutoSize = true;
			this.fileLabel.Location = new System.Drawing.Point(20, 9);
			this.fileLabel.Name = "fileLabel";
			this.fileLabel.Size = new System.Drawing.Size(26, 13);
			this.fileLabel.TabIndex = 2;
			this.fileLabel.Text = "File:";
			this.fileLabel.Visible = false;
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "*.lms";
			// 
			// btnNextPixel
			// 
			this.btnNextPixel.Location = new System.Drawing.Point(62, 219);
			this.btnNextPixel.Name = "btnNextPixel";
			this.btnNextPixel.Size = new System.Drawing.Size(139, 36);
			this.btnNextPixel.TabIndex = 17;
			this.btnNextPixel.Text = "Make Pixel >>";
			this.btnNextPixel.UseVisualStyleBackColor = true;
			this.btnNextPixel.Click += new System.EventHandler(this.btnNextPixel_Click);
			// 
			// txtPixel
			// 
			this.txtPixel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPixel.Location = new System.Drawing.Point(48, 159);
			this.txtPixel.Name = "txtPixel";
			this.txtPixel.ReadOnly = true;
			this.txtPixel.Size = new System.Drawing.Size(30, 20);
			this.txtPixel.TabIndex = 41;
			this.txtPixel.Text = "1";
			this.txtPixel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(11, 162);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(32, 13);
			this.label19.TabIndex = 40;
			this.label19.Text = "Pixel:";
			// 
			// txtRow
			// 
			this.txtRow.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRow.Location = new System.Drawing.Point(48, 61);
			this.txtRow.Name = "txtRow";
			this.txtRow.ReadOnly = true;
			this.txtRow.Size = new System.Drawing.Size(30, 20);
			this.txtRow.TabIndex = 43;
			this.txtRow.Text = "1";
			this.txtRow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblRow
			// 
			this.lblRow.AutoSize = true;
			this.lblRow.Location = new System.Drawing.Point(18, 64);
			this.lblRow.Name = "lblRow";
			this.lblRow.Size = new System.Drawing.Size(32, 13);
			this.lblRow.TabIndex = 42;
			this.lblRow.Text = "Row:";
			this.lblRow.Click += new System.EventHandler(this.label1_Click);
			// 
			// txtCol
			// 
			this.txtCol.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCol.Location = new System.Drawing.Point(48, 87);
			this.txtCol.Name = "txtCol";
			this.txtCol.ReadOnly = true;
			this.txtCol.Size = new System.Drawing.Size(30, 20);
			this.txtCol.TabIndex = 45;
			this.txtCol.Text = "1";
			this.txtCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblCol
			// 
			this.lblCol.AutoSize = true;
			this.lblCol.Location = new System.Drawing.Point(18, 90);
			this.lblCol.Name = "lblCol";
			this.lblCol.Size = new System.Drawing.Size(25, 13);
			this.lblCol.TabIndex = 44;
			this.lblCol.Text = "Col:";
			// 
			// txtPx
			// 
			this.txtPx.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPx.Location = new System.Drawing.Point(48, 113);
			this.txtPx.Name = "txtPx";
			this.txtPx.ReadOnly = true;
			this.txtPx.Size = new System.Drawing.Size(30, 20);
			this.txtPx.TabIndex = 47;
			this.txtPx.Text = "1";
			this.txtPx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(17, 116);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 13);
			this.label3.TabIndex = 46;
			this.label3.Text = "Pix:";
			// 
			// txtDir
			// 
			this.txtDir.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDir.Location = new System.Drawing.Point(75, 87);
			this.txtDir.Name = "txtDir";
			this.txtDir.ReadOnly = true;
			this.txtDir.Size = new System.Drawing.Size(30, 20);
			this.txtDir.TabIndex = 49;
			this.txtDir.Text = "s";
			this.txtDir.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(110, 71);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(23, 13);
			this.label4.TabIndex = 48;
			this.label4.Text = "Dir:";
			// 
			// txtFace
			// 
			this.txtFace.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFace.Location = new System.Drawing.Point(103, 87);
			this.txtFace.Name = "txtFace";
			this.txtFace.ReadOnly = true;
			this.txtFace.Size = new System.Drawing.Size(30, 20);
			this.txtFace.TabIndex = 50;
			this.txtFace.Text = "L";
			this.txtFace.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// btnNextCol
			// 
			this.btnNextCol.Location = new System.Drawing.Point(169, 78);
			this.btnNextCol.Name = "btnNextCol";
			this.btnNextCol.Size = new System.Drawing.Size(139, 36);
			this.btnNextCol.TabIndex = 51;
			this.btnNextCol.Text = "Next Col/Row >>";
			this.btnNextCol.UseVisualStyleBackColor = true;
			this.btnNextCol.Click += new System.EventHandler(this.btnNextCol_Click);
			// 
			// btnSave
			// 
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(294, 380);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(106, 36);
			this.btnSave.TabIndex = 52;
			this.btnSave.Text = "Save!";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// lblPixelName
			// 
			this.lblPixelName.AutoSize = true;
			this.lblPixelName.Font = new System.Drawing.Font("DejaVu Sans Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPixelName.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblPixelName.Location = new System.Drawing.Point(18, 192);
			this.lblPixelName.Name = "lblPixelName";
			this.lblPixelName.Size = new System.Drawing.Size(303, 15);
			this.lblPixelName.TabIndex = 53;
			this.lblPixelName.Text = "Tree Pixel 000 {R0C0P0d} [U0.000-000]";
			// 
			// txtCh
			// 
			this.txtCh.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCh.Location = new System.Drawing.Point(220, 159);
			this.txtCh.Name = "txtCh";
			this.txtCh.ReadOnly = true;
			this.txtCh.Size = new System.Drawing.Size(30, 20);
			this.txtCh.TabIndex = 57;
			this.txtCh.Text = "1";
			this.txtCh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(191, 162);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(23, 13);
			this.label5.TabIndex = 56;
			this.label5.Text = "Ch:";
			// 
			// txtUniv
			// 
			this.txtUniv.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUniv.Location = new System.Drawing.Point(138, 159);
			this.txtUniv.Name = "txtUniv";
			this.txtUniv.ReadOnly = true;
			this.txtUniv.Size = new System.Drawing.Size(30, 20);
			this.txtUniv.TabIndex = 55;
			this.txtUniv.Text = "1";
			this.txtUniv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(100, 162);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(32, 13);
			this.label6.TabIndex = 54;
			this.label6.Text = "Univ:";
			// 
			// lstPixelNames
			// 
			this.lstPixelNames.FormattingEnabled = true;
			this.lstPixelNames.Location = new System.Drawing.Point(14, 282);
			this.lstPixelNames.Name = "lstPixelNames";
			this.lstPixelNames.Size = new System.Drawing.Size(258, 134);
			this.lstPixelNames.TabIndex = 58;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Location = new System.Drawing.Point(19, 142);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(350, 4);
			this.panel1.TabIndex = 59;
			// 
			// btnAuto
			// 
			this.btnAuto.BackColor = System.Drawing.Color.Fuchsia;
			this.btnAuto.Location = new System.Drawing.Point(307, 263);
			this.btnAuto.Name = "btnAuto";
			this.btnAuto.Size = new System.Drawing.Size(73, 60);
			this.btnAuto.TabIndex = 60;
			this.btnAuto.Text = "AUTO";
			this.btnAuto.UseVisualStyleBackColor = false;
			this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
			// 
			// frmTree
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(419, 438);
			this.Controls.Add(this.btnAuto);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.lstPixelNames);
			this.Controls.Add(this.txtCh);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtUniv);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.lblPixelName);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnNextCol);
			this.Controls.Add(this.txtFace);
			this.Controls.Add(this.txtDir);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPx);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtCol);
			this.Controls.Add(this.lblCol);
			this.Controls.Add(this.txtRow);
			this.Controls.Add(this.lblRow);
			this.Controls.Add(this.txtPixel);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.btnNextPixel);
			this.Controls.Add(this.fileLabel);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmTree";
			this.Text = "Tree-O-Rama";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmString_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.lorForm_FormClosed);
			this.Load += new System.EventHandler(this.Form_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnNextPixel;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.TextBox txtPixel;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox txtRow;
		private System.Windows.Forms.Label lblRow;
		private System.Windows.Forms.TextBox txtCol;
		private System.Windows.Forms.Label lblCol;
		private System.Windows.Forms.TextBox txtPx;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtDir;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtFace;
		private System.Windows.Forms.Button btnNextCol;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label lblPixelName;
		private System.Windows.Forms.TextBox txtCh;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtUniv;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ListBox lstPixelNames;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnAuto;
	}
}

