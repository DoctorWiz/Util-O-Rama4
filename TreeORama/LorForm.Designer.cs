namespace StripMaker
{
    partial class lorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(lorForm));
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.fileLabel = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fraUniverse3 = new System.Windows.Forms.GroupBox();
            this.chkUniverse3 = new System.Windows.Forms.CheckBox();
            this.txtPixelCt3 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtStartCh3 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numUniv3 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.optOrderReverse3 = new System.Windows.Forms.RadioButton();
            this.optOrderNormal3 = new System.Windows.Forms.RadioButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.optGRB3 = new System.Windows.Forms.RadioButton();
            this.optRGB3 = new System.Windows.Forms.RadioButton();
            this.fraUniverse2 = new System.Windows.Forms.GroupBox();
            this.chkUniverse2 = new System.Windows.Forms.CheckBox();
            this.txtPixelCt2 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtStartCh2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numUniv2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.optOrderReverse2 = new System.Windows.Forms.RadioButton();
            this.optOrderNormal2 = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.optGRB2 = new System.Windows.Forms.RadioButton();
            this.optRGB2 = new System.Windows.Forms.RadioButton();
            this.fraUniverse1 = new System.Windows.Forms.GroupBox();
            this.chkUniverse1 = new System.Windows.Forms.CheckBox();
            this.txtPixelCt1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtTrack = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStartCh1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numUniv1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.optOrderReverse1 = new System.Windows.Forms.RadioButton();
            this.optOrderNormal1 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optGRB1 = new System.Windows.Forms.RadioButton();
            this.optRGB1 = new System.Windows.Forms.RadioButton();
            this.btnMake = new System.Windows.Forms.Button();
            this.fraUniverse3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUniv3)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.fraUniverse2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUniv2)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.fraUniverse1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUniv1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFile
            // 
            this.txtFile.Enabled = false;
            this.txtFile.Location = new System.Drawing.Point(19, 25);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(223, 20);
            this.txtFile.TabIndex = 0;
            this.txtFile.Visible = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(246, 25);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(26, 20);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Visible = false;
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
            // fraUniverse3
            // 
            this.fraUniverse3.Controls.Add(this.chkUniverse3);
            this.fraUniverse3.Controls.Add(this.txtPixelCt3);
            this.fraUniverse3.Controls.Add(this.label16);
            this.fraUniverse3.Controls.Add(this.txtStartCh3);
            this.fraUniverse3.Controls.Add(this.label12);
            this.fraUniverse3.Controls.Add(this.numUniv3);
            this.fraUniverse3.Controls.Add(this.label5);
            this.fraUniverse3.Controls.Add(this.label6);
            this.fraUniverse3.Controls.Add(this.panel5);
            this.fraUniverse3.Controls.Add(this.panel6);
            this.fraUniverse3.Location = new System.Drawing.Point(12, 385);
            this.fraUniverse3.Name = "fraUniverse3";
            this.fraUniverse3.Size = new System.Drawing.Size(348, 151);
            this.fraUniverse3.TabIndex = 20;
            this.fraUniverse3.TabStop = false;
            this.fraUniverse3.Text = " Strip 3 ";
            // 
            // chkUniverse3
            // 
            this.chkUniverse3.AutoSize = true;
            this.chkUniverse3.Location = new System.Drawing.Point(14, 25);
            this.chkUniverse3.Name = "chkUniverse3";
            this.chkUniverse3.Size = new System.Drawing.Size(68, 17);
            this.chkUniverse3.TabIndex = 32;
            this.chkUniverse3.Text = "Universe";
            this.chkUniverse3.UseVisualStyleBackColor = true;
            // 
            // txtPixelCt3
            // 
            this.txtPixelCt3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPixelCt3.Location = new System.Drawing.Point(293, 112);
            this.txtPixelCt3.Name = "txtPixelCt3";
            this.txtPixelCt3.Size = new System.Drawing.Size(44, 20);
            this.txtPixelCt3.TabIndex = 31;
            this.txtPixelCt3.Text = "150";
            this.txtPixelCt3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(225, 115);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 13);
            this.label16.TabIndex = 30;
            this.label16.Text = "Pixel Count";
            // 
            // txtStartCh3
            // 
            this.txtStartCh3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartCh3.Location = new System.Drawing.Point(293, 67);
            this.txtStartCh3.Name = "txtStartCh3";
            this.txtStartCh3.Size = new System.Drawing.Size(44, 20);
            this.txtStartCh3.TabIndex = 29;
            this.txtStartCh3.Text = "1";
            this.txtStartCh3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(214, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Start Channel";
            // 
            // numUniv3
            // 
            this.numUniv3.Location = new System.Drawing.Point(82, 24);
            this.numUniv3.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numUniv3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUniv3.Name = "numUniv3";
            this.numUniv3.Size = new System.Drawing.Size(32, 20);
            this.numUniv3.TabIndex = 27;
            this.numUniv3.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Pixel Order";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Color Order";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.optOrderReverse3);
            this.panel5.Controls.Add(this.optOrderNormal3);
            this.panel5.Location = new System.Drawing.Point(12, 110);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(193, 26);
            this.panel5.TabIndex = 15;
            // 
            // optOrderReverse3
            // 
            this.optOrderReverse3.AutoSize = true;
            this.optOrderReverse3.Location = new System.Drawing.Point(72, 3);
            this.optOrderReverse3.Name = "optOrderReverse3";
            this.optOrderReverse3.Size = new System.Drawing.Size(65, 17);
            this.optOrderReverse3.TabIndex = 11;
            this.optOrderReverse3.Text = "Reverse";
            this.optOrderReverse3.UseVisualStyleBackColor = true;
            // 
            // optOrderNormal3
            // 
            this.optOrderNormal3.AutoSize = true;
            this.optOrderNormal3.Checked = true;
            this.optOrderNormal3.Location = new System.Drawing.Point(3, 3);
            this.optOrderNormal3.Name = "optOrderNormal3";
            this.optOrderNormal3.Size = new System.Drawing.Size(58, 17);
            this.optOrderNormal3.TabIndex = 10;
            this.optOrderNormal3.TabStop = true;
            this.optOrderNormal3.Text = "Normal";
            this.optOrderNormal3.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.optGRB3);
            this.panel6.Controls.Add(this.optRGB3);
            this.panel6.Location = new System.Drawing.Point(12, 65);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(193, 26);
            this.panel6.TabIndex = 14;
            // 
            // optGRB3
            // 
            this.optGRB3.AutoSize = true;
            this.optGRB3.Location = new System.Drawing.Point(72, 3);
            this.optGRB3.Name = "optGRB3";
            this.optGRB3.Size = new System.Drawing.Size(48, 17);
            this.optGRB3.TabIndex = 11;
            this.optGRB3.Text = "GRB";
            this.optGRB3.UseVisualStyleBackColor = true;
            // 
            // optRGB3
            // 
            this.optRGB3.AutoSize = true;
            this.optRGB3.Checked = true;
            this.optRGB3.Location = new System.Drawing.Point(3, 3);
            this.optRGB3.Name = "optRGB3";
            this.optRGB3.Size = new System.Drawing.Size(48, 17);
            this.optRGB3.TabIndex = 10;
            this.optRGB3.TabStop = true;
            this.optRGB3.Text = "RGB";
            this.optRGB3.UseVisualStyleBackColor = true;
            // 
            // fraUniverse2
            // 
            this.fraUniverse2.Controls.Add(this.chkUniverse2);
            this.fraUniverse2.Controls.Add(this.txtPixelCt2);
            this.fraUniverse2.Controls.Add(this.label15);
            this.fraUniverse2.Controls.Add(this.txtStartCh2);
            this.fraUniverse2.Controls.Add(this.label10);
            this.fraUniverse2.Controls.Add(this.numUniv2);
            this.fraUniverse2.Controls.Add(this.label3);
            this.fraUniverse2.Controls.Add(this.label4);
            this.fraUniverse2.Controls.Add(this.panel3);
            this.fraUniverse2.Controls.Add(this.panel4);
            this.fraUniverse2.Location = new System.Drawing.Point(12, 231);
            this.fraUniverse2.Name = "fraUniverse2";
            this.fraUniverse2.Size = new System.Drawing.Size(348, 148);
            this.fraUniverse2.TabIndex = 19;
            this.fraUniverse2.TabStop = false;
            this.fraUniverse2.Text = " Strip 2 ";
            // 
            // chkUniverse2
            // 
            this.chkUniverse2.AutoSize = true;
            this.chkUniverse2.Location = new System.Drawing.Point(14, 24);
            this.chkUniverse2.Name = "chkUniverse2";
            this.chkUniverse2.Size = new System.Drawing.Size(68, 17);
            this.chkUniverse2.TabIndex = 28;
            this.chkUniverse2.Text = "Universe";
            this.chkUniverse2.UseVisualStyleBackColor = true;
            // 
            // txtPixelCt2
            // 
            this.txtPixelCt2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPixelCt2.Location = new System.Drawing.Point(289, 110);
            this.txtPixelCt2.Name = "txtPixelCt2";
            this.txtPixelCt2.Size = new System.Drawing.Size(44, 20);
            this.txtPixelCt2.TabIndex = 27;
            this.txtPixelCt2.Text = "150";
            this.txtPixelCt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(225, 113);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 13);
            this.label15.TabIndex = 26;
            this.label15.Text = "Pixel Count";
            // 
            // txtStartCh2
            // 
            this.txtStartCh2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartCh2.Location = new System.Drawing.Point(289, 65);
            this.txtStartCh2.Name = "txtStartCh2";
            this.txtStartCh2.Size = new System.Drawing.Size(44, 20);
            this.txtStartCh2.TabIndex = 25;
            this.txtStartCh2.Text = "1";
            this.txtStartCh2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(214, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Start Channel";
            // 
            // numUniv2
            // 
            this.numUniv2.Location = new System.Drawing.Point(82, 23);
            this.numUniv2.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numUniv2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUniv2.Name = "numUniv2";
            this.numUniv2.Size = new System.Drawing.Size(32, 20);
            this.numUniv2.TabIndex = 23;
            this.numUniv2.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Pixel Order";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Color Order";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.optOrderReverse2);
            this.panel3.Controls.Add(this.optOrderNormal2);
            this.panel3.Location = new System.Drawing.Point(12, 108);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(193, 26);
            this.panel3.TabIndex = 15;
            // 
            // optOrderReverse2
            // 
            this.optOrderReverse2.AutoSize = true;
            this.optOrderReverse2.Location = new System.Drawing.Point(72, 3);
            this.optOrderReverse2.Name = "optOrderReverse2";
            this.optOrderReverse2.Size = new System.Drawing.Size(65, 17);
            this.optOrderReverse2.TabIndex = 11;
            this.optOrderReverse2.Text = "Reverse";
            this.optOrderReverse2.UseVisualStyleBackColor = true;
            // 
            // optOrderNormal2
            // 
            this.optOrderNormal2.AutoSize = true;
            this.optOrderNormal2.Checked = true;
            this.optOrderNormal2.Location = new System.Drawing.Point(3, 3);
            this.optOrderNormal2.Name = "optOrderNormal2";
            this.optOrderNormal2.Size = new System.Drawing.Size(58, 17);
            this.optOrderNormal2.TabIndex = 10;
            this.optOrderNormal2.TabStop = true;
            this.optOrderNormal2.Text = "Normal";
            this.optOrderNormal2.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.optGRB2);
            this.panel4.Controls.Add(this.optRGB2);
            this.panel4.Location = new System.Drawing.Point(12, 63);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(193, 26);
            this.panel4.TabIndex = 14;
            // 
            // optGRB2
            // 
            this.optGRB2.AutoSize = true;
            this.optGRB2.Location = new System.Drawing.Point(72, 3);
            this.optGRB2.Name = "optGRB2";
            this.optGRB2.Size = new System.Drawing.Size(48, 17);
            this.optGRB2.TabIndex = 11;
            this.optGRB2.Text = "GRB";
            this.optGRB2.UseVisualStyleBackColor = true;
            // 
            // optRGB2
            // 
            this.optRGB2.AutoSize = true;
            this.optRGB2.Checked = true;
            this.optRGB2.Location = new System.Drawing.Point(3, 3);
            this.optRGB2.Name = "optRGB2";
            this.optRGB2.Size = new System.Drawing.Size(48, 17);
            this.optRGB2.TabIndex = 10;
            this.optRGB2.TabStop = true;
            this.optRGB2.Text = "RGB";
            this.optRGB2.UseVisualStyleBackColor = true;
            // 
            // fraUniverse1
            // 
            this.fraUniverse1.Controls.Add(this.chkUniverse1);
            this.fraUniverse1.Controls.Add(this.txtPixelCt1);
            this.fraUniverse1.Controls.Add(this.label14);
            this.fraUniverse1.Controls.Add(this.txtTrack);
            this.fraUniverse1.Controls.Add(this.label9);
            this.fraUniverse1.Controls.Add(this.txtStartCh1);
            this.fraUniverse1.Controls.Add(this.label8);
            this.fraUniverse1.Controls.Add(this.numUniv1);
            this.fraUniverse1.Controls.Add(this.label2);
            this.fraUniverse1.Controls.Add(this.label1);
            this.fraUniverse1.Controls.Add(this.panel2);
            this.fraUniverse1.Controls.Add(this.panel1);
            this.fraUniverse1.Location = new System.Drawing.Point(12, 58);
            this.fraUniverse1.Name = "fraUniverse1";
            this.fraUniverse1.Size = new System.Drawing.Size(348, 167);
            this.fraUniverse1.TabIndex = 18;
            this.fraUniverse1.TabStop = false;
            this.fraUniverse1.Text = " Strip 1 ";
            // 
            // chkUniverse1
            // 
            this.chkUniverse1.AutoSize = true;
            this.chkUniverse1.Checked = true;
            this.chkUniverse1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUniverse1.Enabled = false;
            this.chkUniverse1.Location = new System.Drawing.Point(14, 52);
            this.chkUniverse1.Name = "chkUniverse1";
            this.chkUniverse1.Size = new System.Drawing.Size(68, 17);
            this.chkUniverse1.TabIndex = 26;
            this.chkUniverse1.Text = "Universe";
            this.chkUniverse1.UseVisualStyleBackColor = true;
            // 
            // txtPixelCt1
            // 
            this.txtPixelCt1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPixelCt1.Location = new System.Drawing.Point(289, 135);
            this.txtPixelCt1.Name = "txtPixelCt1";
            this.txtPixelCt1.Size = new System.Drawing.Size(44, 20);
            this.txtPixelCt1.TabIndex = 25;
            this.txtPixelCt1.Text = "150";
            this.txtPixelCt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(225, 138);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "Pixel Count";
            // 
            // txtTrack
            // 
            this.txtTrack.Enabled = false;
            this.txtTrack.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrack.Location = new System.Drawing.Point(82, 23);
            this.txtTrack.Name = "txtTrack";
            this.txtTrack.Size = new System.Drawing.Size(44, 20);
            this.txtTrack.TabIndex = 23;
            this.txtTrack.Text = "1";
            this.txtTrack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Track";
            // 
            // txtStartCh1
            // 
            this.txtStartCh1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartCh1.Location = new System.Drawing.Point(289, 92);
            this.txtStartCh1.Name = "txtStartCh1";
            this.txtStartCh1.Size = new System.Drawing.Size(44, 20);
            this.txtStartCh1.TabIndex = 21;
            this.txtStartCh1.Text = "1";
            this.txtStartCh1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(214, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Start Channel";
            // 
            // numUniv1
            // 
            this.numUniv1.Location = new System.Drawing.Point(82, 51);
            this.numUniv1.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numUniv1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUniv1.Name = "numUniv1";
            this.numUniv1.Size = new System.Drawing.Size(32, 20);
            this.numUniv1.TabIndex = 19;
            this.numUniv1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Pixel Order";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Color Order";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.optOrderReverse1);
            this.panel2.Controls.Add(this.optOrderNormal1);
            this.panel2.Location = new System.Drawing.Point(14, 133);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(193, 26);
            this.panel2.TabIndex = 15;
            // 
            // optOrderReverse1
            // 
            this.optOrderReverse1.AutoSize = true;
            this.optOrderReverse1.Location = new System.Drawing.Point(72, 3);
            this.optOrderReverse1.Name = "optOrderReverse1";
            this.optOrderReverse1.Size = new System.Drawing.Size(65, 17);
            this.optOrderReverse1.TabIndex = 11;
            this.optOrderReverse1.Text = "Reverse";
            this.optOrderReverse1.UseVisualStyleBackColor = true;
            // 
            // optOrderNormal1
            // 
            this.optOrderNormal1.AutoSize = true;
            this.optOrderNormal1.Checked = true;
            this.optOrderNormal1.Location = new System.Drawing.Point(3, 3);
            this.optOrderNormal1.Name = "optOrderNormal1";
            this.optOrderNormal1.Size = new System.Drawing.Size(58, 17);
            this.optOrderNormal1.TabIndex = 10;
            this.optOrderNormal1.TabStop = true;
            this.optOrderNormal1.Text = "Normal";
            this.optOrderNormal1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optGRB1);
            this.panel1.Controls.Add(this.optRGB1);
            this.panel1.Location = new System.Drawing.Point(14, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(193, 26);
            this.panel1.TabIndex = 14;
            // 
            // optGRB1
            // 
            this.optGRB1.AutoSize = true;
            this.optGRB1.Location = new System.Drawing.Point(72, 3);
            this.optGRB1.Name = "optGRB1";
            this.optGRB1.Size = new System.Drawing.Size(48, 17);
            this.optGRB1.TabIndex = 11;
            this.optGRB1.Text = "GRB";
            this.optGRB1.UseVisualStyleBackColor = true;
            // 
            // optRGB1
            // 
            this.optRGB1.AutoSize = true;
            this.optRGB1.Checked = true;
            this.optRGB1.Location = new System.Drawing.Point(3, 3);
            this.optRGB1.Name = "optRGB1";
            this.optRGB1.Size = new System.Drawing.Size(48, 17);
            this.optRGB1.TabIndex = 10;
            this.optRGB1.TabStop = true;
            this.optRGB1.Text = "RGB";
            this.optRGB1.UseVisualStyleBackColor = true;
            // 
            // btnMake
            // 
            this.btnMake.Location = new System.Drawing.Point(124, 554);
            this.btnMake.Name = "btnMake";
            this.btnMake.Size = new System.Drawing.Size(139, 36);
            this.btnMake.TabIndex = 17;
            this.btnMake.Text = "Add Strips";
            this.btnMake.UseVisualStyleBackColor = true;
            // 
            // lorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 603);
            this.Controls.Add(this.fraUniverse3);
            this.Controls.Add(this.fraUniverse2);
            this.Controls.Add(this.fraUniverse1);
            this.Controls.Add(this.btnMake);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "lorForm";
            this.Text = "Strip Maker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.lorForm_FormClosed);
            this.Load += new System.EventHandler(this.lorForm_Load);
            this.fraUniverse3.ResumeLayout(false);
            this.fraUniverse3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUniv3)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.fraUniverse2.ResumeLayout(false);
            this.fraUniverse2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUniv2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.fraUniverse1.ResumeLayout(false);
            this.fraUniverse1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUniv1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox fraUniverse3;
        private System.Windows.Forms.CheckBox chkUniverse3;
        private System.Windows.Forms.TextBox txtPixelCt3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtStartCh3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numUniv3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton optOrderReverse3;
        private System.Windows.Forms.RadioButton optOrderNormal3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton optGRB3;
        private System.Windows.Forms.RadioButton optRGB3;
        private System.Windows.Forms.GroupBox fraUniverse2;
        private System.Windows.Forms.CheckBox chkUniverse2;
        private System.Windows.Forms.TextBox txtPixelCt2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtStartCh2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numUniv2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton optOrderReverse2;
        private System.Windows.Forms.RadioButton optOrderNormal2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton optGRB2;
        private System.Windows.Forms.RadioButton optRGB2;
        private System.Windows.Forms.GroupBox fraUniverse1;
        private System.Windows.Forms.CheckBox chkUniverse1;
        private System.Windows.Forms.TextBox txtPixelCt1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTrack;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtStartCh1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numUniv1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton optOrderReverse1;
        private System.Windows.Forms.RadioButton optOrderNormal1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton optGRB1;
        private System.Windows.Forms.RadioButton optRGB1;
        private System.Windows.Forms.Button btnMake;
    }
}

