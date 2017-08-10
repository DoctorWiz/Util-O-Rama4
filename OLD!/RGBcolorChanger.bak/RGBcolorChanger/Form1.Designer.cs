namespace LORcolorChanger
{
    partial class frmColors
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
            this.grpTime = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTimeTo = new System.Windows.Forms.TextBox();
            this.txtTimeFrom = new System.Windows.Forms.TextBox();
            this.optTime2 = new System.Windows.Forms.RadioButton();
            this.optTime1 = new System.Windows.Forms.RadioButton();
            this.grpColors = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtColorTo6 = new System.Windows.Forms.TextBox();
            this.txtColorTo5 = new System.Windows.Forms.TextBox();
            this.txtColorTo4 = new System.Windows.Forms.TextBox();
            this.txtColorTo3 = new System.Windows.Forms.TextBox();
            this.txtColorTo2 = new System.Windows.Forms.TextBox();
            this.txtColorTo1 = new System.Windows.Forms.TextBox();
            this.txtColorFrom6 = new System.Windows.Forms.TextBox();
            this.txtColorFrom5 = new System.Windows.Forms.TextBox();
            this.txtColorFrom4 = new System.Windows.Forms.TextBox();
            this.txtColorFrom3 = new System.Windows.Forms.TextBox();
            this.txtColorFrom2 = new System.Windows.Forms.TextBox();
            this.txtColorFrom1 = new System.Windows.Forms.TextBox();
            this.optColor3 = new System.Windows.Forms.RadioButton();
            this.optColor2 = new System.Windows.Forms.RadioButton();
            this.optColor1 = new System.Windows.Forms.RadioButton();
            this.grpFile = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dlgFile = new System.Windows.Forms.OpenFileDialog();
            this.grpTime.SuspendLayout();
            this.grpColors.SuspendLayout();
            this.grpFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTime
            // 
            this.grpTime.Controls.Add(this.label1);
            this.grpTime.Controls.Add(this.txtTimeTo);
            this.grpTime.Controls.Add(this.txtTimeFrom);
            this.grpTime.Controls.Add(this.optTime2);
            this.grpTime.Controls.Add(this.optTime1);
            this.grpTime.Location = new System.Drawing.Point(12, 91);
            this.grpTime.Name = "grpTime";
            this.grpTime.Size = new System.Drawing.Size(464, 82);
            this.grpTime.TabIndex = 26;
            this.grpTime.TabStop = false;
            this.grpTime.Text = " Step 2: Time ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "to";
            // 
            // txtTimeTo
            // 
            this.txtTimeTo.Location = new System.Drawing.Point(152, 52);
            this.txtTimeTo.Name = "txtTimeTo";
            this.txtTimeTo.Size = new System.Drawing.Size(49, 20);
            this.txtTimeTo.TabIndex = 8;
            this.txtTimeTo.Text = "99:99.99";
            // 
            // txtTimeFrom
            // 
            this.txtTimeFrom.Location = new System.Drawing.Point(71, 52);
            this.txtTimeFrom.Name = "txtTimeFrom";
            this.txtTimeFrom.Size = new System.Drawing.Size(49, 20);
            this.txtTimeFrom.TabIndex = 7;
            this.txtTimeFrom.Text = "0:00.00";
            // 
            // optTime2
            // 
            this.optTime2.AutoSize = true;
            this.optTime2.Location = new System.Drawing.Point(15, 52);
            this.optTime2.Name = "optTime2";
            this.optTime2.Size = new System.Drawing.Size(48, 17);
            this.optTime2.TabIndex = 6;
            this.optTime2.Text = "Time";
            this.optTime2.UseVisualStyleBackColor = true;
            // 
            // optTime1
            // 
            this.optTime1.AutoSize = true;
            this.optTime1.Checked = true;
            this.optTime1.Location = new System.Drawing.Point(15, 29);
            this.optTime1.Name = "optTime1";
            this.optTime1.Size = new System.Drawing.Size(36, 17);
            this.optTime1.TabIndex = 5;
            this.optTime1.TabStop = true;
            this.optTime1.Text = "All";
            this.optTime1.UseVisualStyleBackColor = true;
            // 
            // grpColors
            // 
            this.grpColors.Controls.Add(this.label7);
            this.grpColors.Controls.Add(this.label6);
            this.grpColors.Controls.Add(this.label5);
            this.grpColors.Controls.Add(this.label4);
            this.grpColors.Controls.Add(this.label3);
            this.grpColors.Controls.Add(this.label2);
            this.grpColors.Controls.Add(this.txtColorTo6);
            this.grpColors.Controls.Add(this.txtColorTo5);
            this.grpColors.Controls.Add(this.txtColorTo4);
            this.grpColors.Controls.Add(this.txtColorTo3);
            this.grpColors.Controls.Add(this.txtColorTo2);
            this.grpColors.Controls.Add(this.txtColorTo1);
            this.grpColors.Controls.Add(this.txtColorFrom6);
            this.grpColors.Controls.Add(this.txtColorFrom5);
            this.grpColors.Controls.Add(this.txtColorFrom4);
            this.grpColors.Controls.Add(this.txtColorFrom3);
            this.grpColors.Controls.Add(this.txtColorFrom2);
            this.grpColors.Controls.Add(this.txtColorFrom1);
            this.grpColors.Controls.Add(this.optColor3);
            this.grpColors.Controls.Add(this.optColor2);
            this.grpColors.Controls.Add(this.optColor1);
            this.grpColors.Location = new System.Drawing.Point(13, 179);
            this.grpColors.Name = "grpColors";
            this.grpColors.Size = new System.Drawing.Size(463, 227);
            this.grpColors.TabIndex = 27;
            this.grpColors.TabStop = false;
            this.grpColors.Text = " Step 3: Colors ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(149, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 46;
            this.label7.Text = "to";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(149, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "to";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(149, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "to";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "to";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "to";
            // 
            // txtColorTo6
            // 
            this.txtColorTo6.Location = new System.Drawing.Point(165, 194);
            this.txtColorTo6.Name = "txtColorTo6";
            this.txtColorTo6.Size = new System.Drawing.Size(59, 20);
            this.txtColorTo6.TabIndex = 40;
            // 
            // txtColorTo5
            // 
            this.txtColorTo5.Location = new System.Drawing.Point(165, 168);
            this.txtColorTo5.Name = "txtColorTo5";
            this.txtColorTo5.Size = new System.Drawing.Size(59, 20);
            this.txtColorTo5.TabIndex = 39;
            // 
            // txtColorTo4
            // 
            this.txtColorTo4.Location = new System.Drawing.Point(165, 142);
            this.txtColorTo4.Name = "txtColorTo4";
            this.txtColorTo4.Size = new System.Drawing.Size(59, 20);
            this.txtColorTo4.TabIndex = 38;
            // 
            // txtColorTo3
            // 
            this.txtColorTo3.Location = new System.Drawing.Point(165, 116);
            this.txtColorTo3.Name = "txtColorTo3";
            this.txtColorTo3.Size = new System.Drawing.Size(59, 20);
            this.txtColorTo3.TabIndex = 37;
            // 
            // txtColorTo2
            // 
            this.txtColorTo2.Location = new System.Drawing.Point(165, 90);
            this.txtColorTo2.Name = "txtColorTo2";
            this.txtColorTo2.Size = new System.Drawing.Size(59, 20);
            this.txtColorTo2.TabIndex = 36;
            // 
            // txtColorTo1
            // 
            this.txtColorTo1.Location = new System.Drawing.Point(165, 64);
            this.txtColorTo1.Name = "txtColorTo1";
            this.txtColorTo1.Size = new System.Drawing.Size(59, 20);
            this.txtColorTo1.TabIndex = 35;
            // 
            // txtColorFrom6
            // 
            this.txtColorFrom6.Location = new System.Drawing.Point(84, 194);
            this.txtColorFrom6.Name = "txtColorFrom6";
            this.txtColorFrom6.Size = new System.Drawing.Size(59, 20);
            this.txtColorFrom6.TabIndex = 34;
            // 
            // txtColorFrom5
            // 
            this.txtColorFrom5.Location = new System.Drawing.Point(84, 168);
            this.txtColorFrom5.Name = "txtColorFrom5";
            this.txtColorFrom5.Size = new System.Drawing.Size(59, 20);
            this.txtColorFrom5.TabIndex = 33;
            // 
            // txtColorFrom4
            // 
            this.txtColorFrom4.Location = new System.Drawing.Point(84, 142);
            this.txtColorFrom4.Name = "txtColorFrom4";
            this.txtColorFrom4.Size = new System.Drawing.Size(59, 20);
            this.txtColorFrom4.TabIndex = 32;
            // 
            // txtColorFrom3
            // 
            this.txtColorFrom3.Location = new System.Drawing.Point(84, 116);
            this.txtColorFrom3.Name = "txtColorFrom3";
            this.txtColorFrom3.Size = new System.Drawing.Size(59, 20);
            this.txtColorFrom3.TabIndex = 31;
            // 
            // txtColorFrom2
            // 
            this.txtColorFrom2.Location = new System.Drawing.Point(84, 90);
            this.txtColorFrom2.Name = "txtColorFrom2";
            this.txtColorFrom2.Size = new System.Drawing.Size(59, 20);
            this.txtColorFrom2.TabIndex = 30;
            // 
            // txtColorFrom1
            // 
            this.txtColorFrom1.Location = new System.Drawing.Point(84, 64);
            this.txtColorFrom1.Name = "txtColorFrom1";
            this.txtColorFrom1.Size = new System.Drawing.Size(59, 20);
            this.txtColorFrom1.TabIndex = 29;
            // 
            // optColor3
            // 
            this.optColor3.AutoSize = true;
            this.optColor3.Location = new System.Drawing.Point(15, 65);
            this.optColor3.Name = "optColor3";
            this.optColor3.Size = new System.Drawing.Size(63, 17);
            this.optColor3.TabIndex = 28;
            this.optColor3.TabStop = true;
            this.optColor3.Text = "Custom:";
            this.optColor3.UseVisualStyleBackColor = true;
            // 
            // optColor2
            // 
            this.optColor2.AutoSize = true;
            this.optColor2.Location = new System.Drawing.Point(15, 42);
            this.optColor2.Name = "optColor2";
            this.optColor2.Size = new System.Drawing.Size(176, 17);
            this.optColor2.TabIndex = 27;
            this.optColor2.TabStop = true;
            this.optColor2.Text = "Preset 2: RYGCBM to ROYGBV";
            this.optColor2.UseVisualStyleBackColor = true;
            // 
            // optColor1
            // 
            this.optColor1.AutoSize = true;
            this.optColor1.Location = new System.Drawing.Point(15, 19);
            this.optColor1.Name = "optColor1";
            this.optColor1.Size = new System.Drawing.Size(176, 17);
            this.optColor1.TabIndex = 26;
            this.optColor1.TabStop = true;
            this.optColor1.Text = "Preset 1: ROYGBV to RYGCBM";
            this.optColor1.UseVisualStyleBackColor = true;
            // 
            // grpFile
            // 
            this.grpFile.Controls.Add(this.btnBrowse);
            this.grpFile.Controls.Add(this.txtFilename);
            this.grpFile.Controls.Add(this.label8);
            this.grpFile.Location = new System.Drawing.Point(12, 12);
            this.grpFile.Name = "grpFile";
            this.grpFile.Size = new System.Drawing.Size(464, 73);
            this.grpFile.TabIndex = 28;
            this.grpFile.TabStop = false;
            this.grpFile.Text = " Step 1: File ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "File:";
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(52, 31);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(371, 20);
            this.txtFilename.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(427, 30);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(32, 20);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(202, 429);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 31);
            this.btnOK.TabIndex = 29;
            this.btnOK.Text = "Change!";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dlgFile
            // 
            this.dlgFile.FileName = "openFileDialog1";
            // 
            // frmColors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 472);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpFile);
            this.Controls.Add(this.grpColors);
            this.Controls.Add(this.grpTime);
            this.Name = "frmColors";
            this.Text = "Light-O-Rama RGB Color Changer";
            this.grpTime.ResumeLayout(false);
            this.grpTime.PerformLayout();
            this.grpColors.ResumeLayout(false);
            this.grpColors.PerformLayout();
            this.grpFile.ResumeLayout(false);
            this.grpFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTimeTo;
        private System.Windows.Forms.TextBox txtTimeFrom;
        private System.Windows.Forms.RadioButton optTime2;
        private System.Windows.Forms.RadioButton optTime1;
        private System.Windows.Forms.GroupBox grpColors;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtColorTo6;
        private System.Windows.Forms.TextBox txtColorTo5;
        private System.Windows.Forms.TextBox txtColorTo4;
        private System.Windows.Forms.TextBox txtColorTo3;
        private System.Windows.Forms.TextBox txtColorTo2;
        private System.Windows.Forms.TextBox txtColorTo1;
        private System.Windows.Forms.TextBox txtColorFrom6;
        private System.Windows.Forms.TextBox txtColorFrom5;
        private System.Windows.Forms.TextBox txtColorFrom4;
        private System.Windows.Forms.TextBox txtColorFrom3;
        private System.Windows.Forms.TextBox txtColorFrom2;
        private System.Windows.Forms.TextBox txtColorFrom1;
        private System.Windows.Forms.RadioButton optColor3;
        private System.Windows.Forms.RadioButton optColor2;
        private System.Windows.Forms.RadioButton optColor1;
        private System.Windows.Forms.GroupBox grpFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog dlgFile;

    }
}

