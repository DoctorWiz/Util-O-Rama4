namespace Loracity
{
	partial class frmAcity
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAcity));
			this.grpFile = new System.Windows.Forms.GroupBox();
			this.btnBrowseSequence = new System.Windows.Forms.Button();
			this.txtSequenceFile = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnBrowseTransform = new System.Windows.Forms.Button();
			this.txtTransformFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.optNoteOnset = new System.Windows.Forms.RadioButton();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.btnImport = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.grpFile.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpFile
			// 
			this.grpFile.Controls.Add(this.btnBrowseSequence);
			this.grpFile.Controls.Add(this.txtSequenceFile);
			this.grpFile.Controls.Add(this.label8);
			this.grpFile.Location = new System.Drawing.Point(13, 13);
			this.grpFile.Margin = new System.Windows.Forms.Padding(4);
			this.grpFile.Name = "grpFile";
			this.grpFile.Padding = new System.Windows.Forms.Padding(4);
			this.grpFile.Size = new System.Drawing.Size(619, 90);
			this.grpFile.TabIndex = 29;
			this.grpFile.TabStop = false;
			this.grpFile.Text = " Step 1: Sequence File ";
			// 
			// btnBrowseSequence
			// 
			this.btnBrowseSequence.Location = new System.Drawing.Point(569, 37);
			this.btnBrowseSequence.Margin = new System.Windows.Forms.Padding(4);
			this.btnBrowseSequence.Name = "btnBrowseSequence";
			this.btnBrowseSequence.Size = new System.Drawing.Size(43, 25);
			this.btnBrowseSequence.TabIndex = 2;
			this.btnBrowseSequence.Text = "...";
			this.btnBrowseSequence.UseVisualStyleBackColor = true;
			this.btnBrowseSequence.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtSequenceFile
			// 
			this.txtSequenceFile.Location = new System.Drawing.Point(69, 38);
			this.txtSequenceFile.Margin = new System.Windows.Forms.Padding(4);
			this.txtSequenceFile.Name = "txtSequenceFile";
			this.txtSequenceFile.Size = new System.Drawing.Size(493, 22);
			this.txtSequenceFile.TabIndex = 1;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(25, 39);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(34, 17);
			this.label8.TabIndex = 0;
			this.label8.Text = "File:";
			// 
			// dlgOpenFile
			// 
			this.dlgOpenFile.FileName = "openFileDialog1";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnBrowseTransform);
			this.groupBox1.Controls.Add(this.txtTransformFile);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(13, 240);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(619, 90);
			this.groupBox1.TabIndex = 30;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = " Step 3: Note Onset File ";
			// 
			// btnBrowseTransform
			// 
			this.btnBrowseTransform.Location = new System.Drawing.Point(569, 37);
			this.btnBrowseTransform.Margin = new System.Windows.Forms.Padding(4);
			this.btnBrowseTransform.Name = "btnBrowseTransform";
			this.btnBrowseTransform.Size = new System.Drawing.Size(43, 25);
			this.btnBrowseTransform.TabIndex = 2;
			this.btnBrowseTransform.Text = "...";
			this.btnBrowseTransform.UseVisualStyleBackColor = true;
			this.btnBrowseTransform.Click += new System.EventHandler(this.btnBrowseTransform_Click);
			// 
			// txtTransformFile
			// 
			this.txtTransformFile.Location = new System.Drawing.Point(69, 38);
			this.txtTransformFile.Margin = new System.Windows.Forms.Padding(4);
			this.txtTransformFile.Name = "txtTransformFile";
			this.txtTransformFile.Size = new System.Drawing.Size(493, 22);
			this.txtTransformFile.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 39);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "File:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textBox2);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(13, 338);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox2.Size = new System.Drawing.Size(619, 90);
			this.groupBox2.TabIndex = 31;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = " Step 4: Timing Grid Name ";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(69, 38);
			this.textBox2.Margin = new System.Windows.Forms.Padding(4);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(493, 22);
			this.textBox2.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(25, 39);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "File:";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.radioButton3);
			this.groupBox3.Controls.Add(this.radioButton2);
			this.groupBox3.Controls.Add(this.optNoteOnset);
			this.groupBox3.Location = new System.Drawing.Point(13, 111);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox3.Size = new System.Drawing.Size(619, 121);
			this.groupBox3.TabIndex = 32;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = " Step 2: Import File Type";
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Enabled = false;
			this.radioButton3.Location = new System.Drawing.Point(24, 87);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(90, 21);
			this.radioButton3.TabIndex = 2;
			this.radioButton3.Text = "Reserved";
			this.radioButton3.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Enabled = false;
			this.radioButton2.Location = new System.Drawing.Point(24, 60);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(90, 21);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "Reserved";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// optNoteOnset
			// 
			this.optNoteOnset.AutoSize = true;
			this.optNoteOnset.Checked = true;
			this.optNoteOnset.Location = new System.Drawing.Point(24, 33);
			this.optNoteOnset.Name = "optNoteOnset";
			this.optNoteOnset.Size = new System.Drawing.Size(101, 21);
			this.optNoteOnset.TabIndex = 0;
			this.optNoteOnset.TabStop = true;
			this.optNoteOnset.Text = "Note Onset";
			this.optNoteOnset.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.btnImport);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Location = new System.Drawing.Point(13, 436);
			this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox4.Size = new System.Drawing.Size(619, 90);
			this.groupBox4.TabIndex = 33;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = " Step 5: Import ";
			// 
			// btnImport
			// 
			this.btnImport.Location = new System.Drawing.Point(69, 35);
			this.btnImport.Margin = new System.Windows.Forms.Padding(4);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(188, 25);
			this.btnImport.TabIndex = 2;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(25, 39);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "File:";
			// 
			// frmAcity
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(644, 714);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.grpFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmAcity";
			this.Text = "LOR-acity";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAcity_FormClosed);
			this.Load += new System.EventHandler(this.frmAcity_Load);
			this.grpFile.ResumeLayout(false);
			this.grpFile.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpFile;
		private System.Windows.Forms.Button btnBrowseSequence;
		private System.Windows.Forms.TextBox txtSequenceFile;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnBrowseTransform;
		private System.Windows.Forms.TextBox txtTransformFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton optNoteOnset;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Label label3;
	}
}

