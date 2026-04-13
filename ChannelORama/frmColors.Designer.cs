namespace UtilORama4
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmColors));
			btnOK = new Button();
			btnCancel = new Button();
			grpCommon = new GroupBox();
			lblCoolWhite = new Label();
			lblWarmWhite = new Label();
			lblRed = new Label();
			lblOrange = new Label();
			lblYellow = new Label();
			lblGreen = new Label();
			lblBlue = new Label();
			lblPurple = new Label();
			lblPink = new Label();
			groupBox2 = new GroupBox();
			picMulti = new PictureBox();
			picRGBW = new PictureBox();
			picRGB = new PictureBox();
			grpAdditional = new GroupBox();
			pic11 = new Label();
			pic12 = new Label();
			pic13 = new Label();
			pic14 = new Label();
			pic15 = new Label();
			pic16 = new Label();
			pic17 = new Label();
			pic18 = new Label();
			pic21 = new Label();
			pic22 = new Label();
			pic23 = new Label();
			pic24 = new Label();
			pic25 = new Label();
			pic26 = new Label();
			pic27 = new Label();
			pic28 = new Label();
			pic31 = new Label();
			pic32 = new Label();
			pic33 = new Label();
			pic34 = new Label();
			pic35 = new Label();
			pic36 = new Label();
			pic37 = new Label();
			pic38 = new Label();
			pic41 = new Label();
			pic42 = new Label();
			pic43 = new Label();
			pic44 = new Label();
			pic45 = new Label();
			pic46 = new Label();
			pic47 = new Label();
			pic48 = new Label();
			pic51 = new Label();
			pic52 = new Label();
			pic53 = new Label();
			pic54 = new Label();
			pic55 = new Label();
			pic56 = new Label();
			pic57 = new Label();
			pic58 = new Label();
			grpCustom = new GroupBox();
			pic61 = new Label();
			pic62 = new Label();
			pic63 = new Label();
			pic64 = new Label();
			pic65 = new Label();
			pic66 = new Label();
			pic67 = new Label();
			pic68 = new Label();
			pic71 = new Label();
			pic72 = new Label();
			pic73 = new Label();
			pic74 = new Label();
			pic75 = new Label();
			pic76 = new Label();
			pic77 = new Label();
			pic78 = new Label();
			picSelection = new PictureBox();
			lblDirty = new Label();
			label1 = new Label();
			grpCommon.SuspendLayout();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picMulti).BeginInit();
			((System.ComponentModel.ISupportInitialize)picRGBW).BeginInit();
			((System.ComponentModel.ISupportInitialize)picRGB).BeginInit();
			grpAdditional.SuspendLayout();
			grpCustom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picSelection).BeginInit();
			SuspendLayout();
			// 
			// btnOK
			// 
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Enabled = false;
			btnOK.Location = new Point(658, 928);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(140, 64);
			btnOK.TabIndex = 2;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			btnOK.Click += btnOK_Click;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(493, 928);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(140, 64);
			btnCancel.TabIndex = 3;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// grpCommon
			// 
			grpCommon.Controls.Add(lblCoolWhite);
			grpCommon.Controls.Add(lblWarmWhite);
			grpCommon.Controls.Add(lblRed);
			grpCommon.Controls.Add(lblOrange);
			grpCommon.Controls.Add(lblYellow);
			grpCommon.Controls.Add(lblGreen);
			grpCommon.Controls.Add(lblBlue);
			grpCommon.Controls.Add(lblPurple);
			grpCommon.Controls.Add(lblPink);
			grpCommon.Location = new Point(12, 12);
			grpCommon.Name = "grpCommon";
			grpCommon.Size = new Size(806, 127);
			grpCommon.TabIndex = 85;
			grpCommon.TabStop = false;
			grpCommon.Text = "Common LED Colors";
			// 
			// lblCoolWhite
			// 
			lblCoolWhite.BackColor = Color.LightCyan;
			lblCoolWhite.BorderStyle = BorderStyle.Fixed3D;
			lblCoolWhite.Font = new Font("Arial Narrow", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblCoolWhite.ForeColor = Color.Black;
			lblCoolWhite.Location = new Point(18, 45);
			lblCoolWhite.Name = "lblCoolWhite";
			lblCoolWhite.Size = new Size(80, 64);
			lblCoolWhite.TabIndex = 81;
			lblCoolWhite.Tag = "Cool White";
			lblCoolWhite.Text = "Cool\r\nWhite";
			lblCoolWhite.TextAlign = ContentAlignment.MiddleCenter;
			lblCoolWhite.Click += color_Common_Click;
			// 
			// lblWarmWhite
			// 
			lblWarmWhite.BackColor = Color.FromArgb(255, 224, 208);
			lblWarmWhite.BorderStyle = BorderStyle.Fixed3D;
			lblWarmWhite.Font = new Font("Arial Narrow", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblWarmWhite.ForeColor = Color.Black;
			lblWarmWhite.Location = new Point(104, 45);
			lblWarmWhite.Name = "lblWarmWhite";
			lblWarmWhite.Size = new Size(80, 64);
			lblWarmWhite.TabIndex = 80;
			lblWarmWhite.Tag = "Warm White";
			lblWarmWhite.Text = "Warm\r\nWhite";
			lblWarmWhite.TextAlign = ContentAlignment.MiddleCenter;
			lblWarmWhite.Click += color_Common_Click;
			// 
			// lblRed
			// 
			lblRed.BackColor = Color.Red;
			lblRed.BorderStyle = BorderStyle.Fixed3D;
			lblRed.Font = new Font("Arial Narrow", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblRed.ForeColor = Color.Black;
			lblRed.Location = new Point(190, 45);
			lblRed.Name = "lblRed";
			lblRed.Size = new Size(80, 64);
			lblRed.TabIndex = 82;
			lblRed.Tag = "Red";
			lblRed.Text = "Red";
			lblRed.TextAlign = ContentAlignment.MiddleCenter;
			lblRed.Click += color_Common_Click;
			// 
			// lblOrange
			// 
			lblOrange.BackColor = Color.FromArgb(255, 128, 0);
			lblOrange.BorderStyle = BorderStyle.Fixed3D;
			lblOrange.Font = new Font("Arial Narrow", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblOrange.ForeColor = Color.Black;
			lblOrange.Location = new Point(276, 45);
			lblOrange.Name = "lblOrange";
			lblOrange.Size = new Size(80, 64);
			lblOrange.TabIndex = 83;
			lblOrange.Tag = "Orange";
			lblOrange.Text = "Orange";
			lblOrange.TextAlign = ContentAlignment.MiddleCenter;
			lblOrange.Click += color_Common_Click;
			// 
			// lblYellow
			// 
			lblYellow.BackColor = Color.Yellow;
			lblYellow.BorderStyle = BorderStyle.Fixed3D;
			lblYellow.Font = new Font("Arial Narrow", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblYellow.ForeColor = Color.Black;
			lblYellow.Location = new Point(362, 45);
			lblYellow.Name = "lblYellow";
			lblYellow.Size = new Size(80, 64);
			lblYellow.TabIndex = 84;
			lblYellow.Tag = "Yellow";
			lblYellow.Text = "Yellow";
			lblYellow.TextAlign = ContentAlignment.MiddleCenter;
			lblYellow.Click += color_Common_Click;
			// 
			// lblGreen
			// 
			lblGreen.BackColor = Color.Lime;
			lblGreen.BorderStyle = BorderStyle.Fixed3D;
			lblGreen.Font = new Font("Arial Narrow", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblGreen.ForeColor = Color.Black;
			lblGreen.Location = new Point(448, 45);
			lblGreen.Name = "lblGreen";
			lblGreen.Size = new Size(80, 64);
			lblGreen.TabIndex = 85;
			lblGreen.Tag = "Green";
			lblGreen.Text = "Green";
			lblGreen.TextAlign = ContentAlignment.MiddleCenter;
			lblGreen.Click += color_Common_Click;
			// 
			// lblBlue
			// 
			lblBlue.BackColor = Color.Blue;
			lblBlue.BorderStyle = BorderStyle.Fixed3D;
			lblBlue.Font = new Font("Arial Narrow", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblBlue.ForeColor = Color.White;
			lblBlue.Location = new Point(534, 45);
			lblBlue.Name = "lblBlue";
			lblBlue.Size = new Size(80, 64);
			lblBlue.TabIndex = 86;
			lblBlue.Tag = "Blue";
			lblBlue.Text = "Blue";
			lblBlue.TextAlign = ContentAlignment.MiddleCenter;
			lblBlue.Click += color_Common_Click;
			// 
			// lblPurple
			// 
			lblPurple.BackColor = Color.FromArgb(192, 0, 192);
			lblPurple.BorderStyle = BorderStyle.Fixed3D;
			lblPurple.Font = new Font("Arial Narrow", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblPurple.ForeColor = Color.Black;
			lblPurple.Location = new Point(620, 45);
			lblPurple.Name = "lblPurple";
			lblPurple.Size = new Size(80, 64);
			lblPurple.TabIndex = 87;
			lblPurple.Tag = "Purple";
			lblPurple.Text = "Purple";
			lblPurple.TextAlign = ContentAlignment.MiddleCenter;
			lblPurple.Click += color_Common_Click;
			// 
			// lblPink
			// 
			lblPink.BackColor = Color.FromArgb(255, 128, 255);
			lblPink.BorderStyle = BorderStyle.Fixed3D;
			lblPink.Font = new Font("Arial Narrow", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblPink.ForeColor = Color.Black;
			lblPink.Location = new Point(706, 45);
			lblPink.Name = "lblPink";
			lblPink.Size = new Size(80, 64);
			lblPink.TabIndex = 88;
			lblPink.Tag = "Pink";
			lblPink.Text = "Pink";
			lblPink.TextAlign = ContentAlignment.MiddleCenter;
			lblPink.Click += color_Common_Click;
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(picMulti);
			groupBox2.Controls.Add(picRGBW);
			groupBox2.Controls.Add(picRGB);
			groupBox2.Location = new Point(12, 145);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(806, 124);
			groupBox2.TabIndex = 86;
			groupBox2.TabStop = false;
			groupBox2.Text = "Special Colors";
			// 
			// picMulti
			// 
			picMulti.BackColor = Color.FromArgb(1, 0, 0);
			picMulti.BorderStyle = BorderStyle.Fixed3D;
			picMulti.Image = (Image)resources.GetObject("picMulti.Image");
			picMulti.Location = new Point(534, 37);
			picMulti.Name = "picMulti";
			picMulti.Size = new Size(80, 72);
			picMulti.SizeMode = PictureBoxSizeMode.StretchImage;
			picMulti.TabIndex = 88;
			picMulti.TabStop = false;
			picMulti.Click += color_Special_Click;
			picMulti.Paint += picMulti_Paint;
			// 
			// picRGBW
			// 
			picRGBW.BackColor = Color.FromArgb(0, 1, 0);
			picRGBW.BorderStyle = BorderStyle.Fixed3D;
			picRGBW.Image = (Image)resources.GetObject("picRGBW.Image");
			picRGBW.Location = new Point(362, 38);
			picRGBW.Name = "picRGBW";
			picRGBW.Size = new Size(80, 72);
			picRGBW.SizeMode = PictureBoxSizeMode.StretchImage;
			picRGBW.TabIndex = 87;
			picRGBW.TabStop = false;
			picRGBW.Click += color_Special_Click;
			picRGBW.Paint += picRGBW_Paint;
			// 
			// picRGB
			// 
			picRGB.BackColor = Color.FromArgb(0, 0, 1);
			picRGB.BorderStyle = BorderStyle.Fixed3D;
			picRGB.Image = (Image)resources.GetObject("picRGB.Image");
			picRGB.Location = new Point(190, 38);
			picRGB.Name = "picRGB";
			picRGB.Size = new Size(80, 72);
			picRGB.SizeMode = PictureBoxSizeMode.StretchImage;
			picRGB.TabIndex = 86;
			picRGB.TabStop = false;
			picRGB.Click += color_Special_Click;
			picRGB.Paint += picRGB_Paint;
			// 
			// grpAdditional
			// 
			grpAdditional.Controls.Add(pic11);
			grpAdditional.Controls.Add(pic12);
			grpAdditional.Controls.Add(pic13);
			grpAdditional.Controls.Add(pic14);
			grpAdditional.Controls.Add(pic15);
			grpAdditional.Controls.Add(pic16);
			grpAdditional.Controls.Add(pic17);
			grpAdditional.Controls.Add(pic18);
			grpAdditional.Controls.Add(pic21);
			grpAdditional.Controls.Add(pic22);
			grpAdditional.Controls.Add(pic23);
			grpAdditional.Controls.Add(pic24);
			grpAdditional.Controls.Add(pic25);
			grpAdditional.Controls.Add(pic26);
			grpAdditional.Controls.Add(pic27);
			grpAdditional.Controls.Add(pic28);
			grpAdditional.Controls.Add(pic31);
			grpAdditional.Controls.Add(pic32);
			grpAdditional.Controls.Add(pic33);
			grpAdditional.Controls.Add(pic34);
			grpAdditional.Controls.Add(pic35);
			grpAdditional.Controls.Add(pic36);
			grpAdditional.Controls.Add(pic37);
			grpAdditional.Controls.Add(pic38);
			grpAdditional.Controls.Add(pic41);
			grpAdditional.Controls.Add(pic42);
			grpAdditional.Controls.Add(pic43);
			grpAdditional.Controls.Add(pic44);
			grpAdditional.Controls.Add(pic45);
			grpAdditional.Controls.Add(pic46);
			grpAdditional.Controls.Add(pic47);
			grpAdditional.Controls.Add(pic48);
			grpAdditional.Controls.Add(pic51);
			grpAdditional.Controls.Add(pic52);
			grpAdditional.Controls.Add(pic53);
			grpAdditional.Controls.Add(pic54);
			grpAdditional.Controls.Add(pic55);
			grpAdditional.Controls.Add(pic56);
			grpAdditional.Controls.Add(pic57);
			grpAdditional.Controls.Add(pic58);
			grpAdditional.Location = new Point(12, 275);
			grpAdditional.Name = "grpAdditional";
			grpAdditional.Size = new Size(806, 410);
			grpAdditional.TabIndex = 87;
			grpAdditional.TabStop = false;
			grpAdditional.Text = "Additional Colors";
			// 
			// pic11
			// 
			pic11.BackColor = Color.White;
			pic11.BorderStyle = BorderStyle.Fixed3D;
			pic11.Location = new Point(54, 45);
			pic11.Name = "pic11";
			pic11.Size = new Size(80, 64);
			pic11.TabIndex = 52;
			pic11.Tag = "White";
			pic11.Click += color_Additional_Click;
			// 
			// pic12
			// 
			pic12.BackColor = Color.FromArgb(255, 192, 192);
			pic12.BorderStyle = BorderStyle.Fixed3D;
			pic12.Location = new Point(140, 45);
			pic12.Name = "pic12";
			pic12.Size = new Size(80, 64);
			pic12.TabIndex = 53;
			pic12.Click += color_Additional_Click;
			// 
			// pic13
			// 
			pic13.BackColor = Color.FromArgb(255, 224, 192);
			pic13.BorderStyle = BorderStyle.Fixed3D;
			pic13.Location = new Point(226, 45);
			pic13.Name = "pic13";
			pic13.Size = new Size(80, 64);
			pic13.TabIndex = 54;
			pic13.Click += color_Additional_Click;
			// 
			// pic14
			// 
			pic14.BackColor = Color.FromArgb(255, 255, 192);
			pic14.BorderStyle = BorderStyle.Fixed3D;
			pic14.Location = new Point(312, 45);
			pic14.Name = "pic14";
			pic14.Size = new Size(80, 64);
			pic14.TabIndex = 55;
			pic14.Click += color_Additional_Click;
			// 
			// pic15
			// 
			pic15.BackColor = Color.FromArgb(192, 255, 192);
			pic15.BorderStyle = BorderStyle.Fixed3D;
			pic15.Location = new Point(398, 45);
			pic15.Name = "pic15";
			pic15.Size = new Size(80, 64);
			pic15.TabIndex = 56;
			pic15.Click += color_Additional_Click;
			// 
			// pic16
			// 
			pic16.BackColor = Color.FromArgb(192, 255, 255);
			pic16.BorderStyle = BorderStyle.Fixed3D;
			pic16.Location = new Point(484, 45);
			pic16.Name = "pic16";
			pic16.Size = new Size(80, 64);
			pic16.TabIndex = 57;
			pic16.Click += color_Additional_Click;
			// 
			// pic17
			// 
			pic17.BackColor = Color.FromArgb(192, 192, 255);
			pic17.BorderStyle = BorderStyle.Fixed3D;
			pic17.Location = new Point(570, 45);
			pic17.Name = "pic17";
			pic17.Size = new Size(80, 64);
			pic17.TabIndex = 58;
			pic17.Click += color_Additional_Click;
			// 
			// pic18
			// 
			pic18.BackColor = Color.FromArgb(255, 192, 255);
			pic18.BorderStyle = BorderStyle.Fixed3D;
			pic18.Location = new Point(656, 45);
			pic18.Name = "pic18";
			pic18.Size = new Size(80, 64);
			pic18.TabIndex = 59;
			pic18.Click += color_Additional_Click;
			// 
			// pic21
			// 
			pic21.BackColor = Color.FromArgb(224, 224, 224);
			pic21.BorderStyle = BorderStyle.Fixed3D;
			pic21.Location = new Point(54, 115);
			pic21.Name = "pic21";
			pic21.Size = new Size(80, 64);
			pic21.TabIndex = 60;
			pic21.Click += color_Additional_Click;
			// 
			// pic22
			// 
			pic22.BackColor = Color.FromArgb(255, 128, 128);
			pic22.BorderStyle = BorderStyle.Fixed3D;
			pic22.Location = new Point(140, 115);
			pic22.Name = "pic22";
			pic22.Size = new Size(80, 64);
			pic22.TabIndex = 61;
			pic22.Click += color_Additional_Click;
			// 
			// pic23
			// 
			pic23.BackColor = Color.FromArgb(255, 192, 128);
			pic23.BorderStyle = BorderStyle.Fixed3D;
			pic23.Location = new Point(226, 115);
			pic23.Name = "pic23";
			pic23.Size = new Size(80, 64);
			pic23.TabIndex = 62;
			pic23.Click += color_Additional_Click;
			// 
			// pic24
			// 
			pic24.BackColor = Color.FromArgb(255, 255, 128);
			pic24.BorderStyle = BorderStyle.Fixed3D;
			pic24.Location = new Point(312, 115);
			pic24.Name = "pic24";
			pic24.Size = new Size(80, 64);
			pic24.TabIndex = 63;
			pic24.Click += color_Additional_Click;
			// 
			// pic25
			// 
			pic25.BackColor = Color.FromArgb(128, 255, 128);
			pic25.BorderStyle = BorderStyle.Fixed3D;
			pic25.Location = new Point(398, 115);
			pic25.Name = "pic25";
			pic25.Size = new Size(80, 64);
			pic25.TabIndex = 64;
			pic25.Click += color_Additional_Click;
			// 
			// pic26
			// 
			pic26.BackColor = Color.FromArgb(128, 255, 255);
			pic26.BorderStyle = BorderStyle.Fixed3D;
			pic26.Location = new Point(484, 115);
			pic26.Name = "pic26";
			pic26.Size = new Size(80, 64);
			pic26.TabIndex = 65;
			pic26.Click += color_Additional_Click;
			// 
			// pic27
			// 
			pic27.BackColor = Color.FromArgb(128, 128, 255);
			pic27.BorderStyle = BorderStyle.Fixed3D;
			pic27.Location = new Point(570, 115);
			pic27.Name = "pic27";
			pic27.Size = new Size(80, 64);
			pic27.TabIndex = 66;
			pic27.Click += color_Additional_Click;
			// 
			// pic28
			// 
			pic28.BackColor = Color.FromArgb(255, 128, 255);
			pic28.BorderStyle = BorderStyle.Fixed3D;
			pic28.Location = new Point(656, 115);
			pic28.Name = "pic28";
			pic28.Size = new Size(80, 64);
			pic28.TabIndex = 67;
			pic28.Click += color_Additional_Click;
			// 
			// pic31
			// 
			pic31.BackColor = Color.Gray;
			pic31.BorderStyle = BorderStyle.Fixed3D;
			pic31.Location = new Point(54, 185);
			pic31.Name = "pic31";
			pic31.Size = new Size(80, 64);
			pic31.TabIndex = 69;
			pic31.Tag = "Gray";
			pic31.Click += color_Additional_Click;
			// 
			// pic32
			// 
			pic32.BackColor = Color.FromArgb(192, 0, 0);
			pic32.BorderStyle = BorderStyle.Fixed3D;
			pic32.Location = new Point(140, 185);
			pic32.Name = "pic32";
			pic32.Size = new Size(80, 64);
			pic32.TabIndex = 70;
			pic32.Click += color_Additional_Click;
			// 
			// pic33
			// 
			pic33.BackColor = Color.FromArgb(192, 64, 0);
			pic33.BorderStyle = BorderStyle.Fixed3D;
			pic33.Location = new Point(226, 185);
			pic33.Name = "pic33";
			pic33.Size = new Size(80, 64);
			pic33.TabIndex = 71;
			pic33.Click += color_Additional_Click;
			// 
			// pic34
			// 
			pic34.BackColor = Color.FromArgb(192, 192, 0);
			pic34.BorderStyle = BorderStyle.Fixed3D;
			pic34.Location = new Point(312, 185);
			pic34.Name = "pic34";
			pic34.Size = new Size(80, 64);
			pic34.TabIndex = 72;
			pic34.Click += color_Additional_Click;
			// 
			// pic35
			// 
			pic35.BackColor = Color.FromArgb(0, 192, 0);
			pic35.BorderStyle = BorderStyle.Fixed3D;
			pic35.Location = new Point(398, 185);
			pic35.Name = "pic35";
			pic35.Size = new Size(80, 64);
			pic35.TabIndex = 73;
			pic35.Click += color_Additional_Click;
			// 
			// pic36
			// 
			pic36.BackColor = Color.FromArgb(0, 192, 192);
			pic36.BorderStyle = BorderStyle.Fixed3D;
			pic36.Location = new Point(484, 185);
			pic36.Name = "pic36";
			pic36.Size = new Size(80, 64);
			pic36.TabIndex = 74;
			pic36.Click += color_Additional_Click;
			// 
			// pic37
			// 
			pic37.BackColor = Color.FromArgb(0, 0, 192);
			pic37.BorderStyle = BorderStyle.Fixed3D;
			pic37.Location = new Point(570, 185);
			pic37.Name = "pic37";
			pic37.Size = new Size(80, 64);
			pic37.TabIndex = 75;
			pic37.Click += color_Additional_Click;
			// 
			// pic38
			// 
			pic38.BackColor = Color.Fuchsia;
			pic38.BorderStyle = BorderStyle.Fixed3D;
			pic38.Location = new Point(656, 185);
			pic38.Name = "pic38";
			pic38.Size = new Size(80, 64);
			pic38.TabIndex = 68;
			pic38.Tag = "Fushia";
			pic38.Click += color_Additional_Click;
			// 
			// pic41
			// 
			pic41.BackColor = Color.FromArgb(64, 64, 64);
			pic41.BorderStyle = BorderStyle.Fixed3D;
			pic41.Location = new Point(54, 255);
			pic41.Name = "pic41";
			pic41.Size = new Size(80, 64);
			pic41.TabIndex = 76;
			pic41.Click += color_Additional_Click;
			// 
			// pic42
			// 
			pic42.BackColor = Color.Maroon;
			pic42.BorderStyle = BorderStyle.Fixed3D;
			pic42.Location = new Point(140, 255);
			pic42.Name = "pic42";
			pic42.Size = new Size(80, 64);
			pic42.TabIndex = 77;
			pic42.Tag = "Maroon";
			pic42.Click += color_Additional_Click;
			// 
			// pic43
			// 
			pic43.BackColor = Color.FromArgb(128, 64, 0);
			pic43.BorderStyle = BorderStyle.Fixed3D;
			pic43.Location = new Point(226, 255);
			pic43.Name = "pic43";
			pic43.Size = new Size(80, 64);
			pic43.TabIndex = 78;
			pic43.Click += color_Additional_Click;
			// 
			// pic44
			// 
			pic44.BackColor = Color.Olive;
			pic44.BorderStyle = BorderStyle.Fixed3D;
			pic44.Location = new Point(312, 255);
			pic44.Name = "pic44";
			pic44.Size = new Size(80, 64);
			pic44.TabIndex = 79;
			pic44.Tag = "Olive";
			pic44.Click += color_Additional_Click;
			// 
			// pic45
			// 
			pic45.BackColor = Color.Green;
			pic45.BorderStyle = BorderStyle.Fixed3D;
			pic45.Location = new Point(398, 255);
			pic45.Name = "pic45";
			pic45.Size = new Size(80, 64);
			pic45.TabIndex = 80;
			pic45.Tag = "Green";
			pic45.Click += color_Additional_Click;
			// 
			// pic46
			// 
			pic46.BackColor = Color.Teal;
			pic46.BorderStyle = BorderStyle.Fixed3D;
			pic46.Location = new Point(484, 255);
			pic46.Name = "pic46";
			pic46.Size = new Size(80, 64);
			pic46.TabIndex = 81;
			pic46.Tag = "Teal";
			pic46.Click += color_Additional_Click;
			// 
			// pic47
			// 
			pic47.BackColor = Color.Navy;
			pic47.BorderStyle = BorderStyle.Fixed3D;
			pic47.Location = new Point(570, 255);
			pic47.Name = "pic47";
			pic47.Size = new Size(80, 64);
			pic47.TabIndex = 82;
			pic47.Tag = "Navy";
			pic47.Click += color_Additional_Click;
			// 
			// pic48
			// 
			pic48.BackColor = Color.Purple;
			pic48.BorderStyle = BorderStyle.Fixed3D;
			pic48.Location = new Point(656, 255);
			pic48.Name = "pic48";
			pic48.Size = new Size(80, 64);
			pic48.TabIndex = 83;
			pic48.Tag = "Purple";
			pic48.Click += color_Additional_Click;
			// 
			// pic51
			// 
			pic51.BackColor = Color.Black;
			pic51.BorderStyle = BorderStyle.Fixed3D;
			pic51.Location = new Point(54, 325);
			pic51.Name = "pic51";
			pic51.Size = new Size(80, 64);
			pic51.TabIndex = 84;
			pic51.Tag = "Black";
			pic51.Click += color_Additional_Click;
			// 
			// pic52
			// 
			pic52.BackColor = Color.FromArgb(64, 0, 0);
			pic52.BorderStyle = BorderStyle.Fixed3D;
			pic52.Location = new Point(140, 325);
			pic52.Name = "pic52";
			pic52.Size = new Size(80, 64);
			pic52.TabIndex = 85;
			pic52.Click += color_Additional_Click;
			// 
			// pic53
			// 
			pic53.BackColor = Color.FromArgb(128, 64, 64);
			pic53.BorderStyle = BorderStyle.Fixed3D;
			pic53.Location = new Point(226, 325);
			pic53.Name = "pic53";
			pic53.Size = new Size(80, 64);
			pic53.TabIndex = 86;
			pic53.Click += color_Additional_Click;
			// 
			// pic54
			// 
			pic54.BackColor = Color.FromArgb(64, 64, 0);
			pic54.BorderStyle = BorderStyle.Fixed3D;
			pic54.Location = new Point(312, 325);
			pic54.Name = "pic54";
			pic54.Size = new Size(80, 64);
			pic54.TabIndex = 87;
			pic54.Click += color_Additional_Click;
			// 
			// pic55
			// 
			pic55.BackColor = Color.FromArgb(0, 64, 0);
			pic55.BorderStyle = BorderStyle.Fixed3D;
			pic55.Location = new Point(398, 325);
			pic55.Name = "pic55";
			pic55.Size = new Size(80, 64);
			pic55.TabIndex = 88;
			pic55.Click += color_Additional_Click;
			// 
			// pic56
			// 
			pic56.BackColor = Color.FromArgb(0, 64, 64);
			pic56.BorderStyle = BorderStyle.Fixed3D;
			pic56.Location = new Point(484, 325);
			pic56.Name = "pic56";
			pic56.Size = new Size(80, 64);
			pic56.TabIndex = 89;
			pic56.Click += color_Additional_Click;
			// 
			// pic57
			// 
			pic57.BackColor = Color.FromArgb(0, 0, 64);
			pic57.BorderStyle = BorderStyle.Fixed3D;
			pic57.Location = new Point(570, 325);
			pic57.Name = "pic57";
			pic57.Size = new Size(80, 64);
			pic57.TabIndex = 90;
			pic57.Click += color_Additional_Click;
			// 
			// pic58
			// 
			pic58.BackColor = Color.FromArgb(64, 0, 64);
			pic58.BorderStyle = BorderStyle.Fixed3D;
			pic58.Location = new Point(656, 325);
			pic58.Name = "pic58";
			pic58.Size = new Size(80, 64);
			pic58.TabIndex = 91;
			pic58.Click += color_Additional_Click;
			// 
			// grpCustom
			// 
			grpCustom.Controls.Add(pic61);
			grpCustom.Controls.Add(pic62);
			grpCustom.Controls.Add(pic63);
			grpCustom.Controls.Add(pic64);
			grpCustom.Controls.Add(pic65);
			grpCustom.Controls.Add(pic66);
			grpCustom.Controls.Add(pic67);
			grpCustom.Controls.Add(pic68);
			grpCustom.Controls.Add(pic71);
			grpCustom.Controls.Add(pic72);
			grpCustom.Controls.Add(pic73);
			grpCustom.Controls.Add(pic74);
			grpCustom.Controls.Add(pic75);
			grpCustom.Controls.Add(pic76);
			grpCustom.Controls.Add(pic77);
			grpCustom.Controls.Add(pic78);
			grpCustom.Location = new Point(12, 691);
			grpCustom.Name = "grpCustom";
			grpCustom.Size = new Size(806, 200);
			grpCustom.TabIndex = 88;
			grpCustom.TabStop = false;
			grpCustom.Text = "Custom Colors";
			// 
			// pic61
			// 
			pic61.BackColor = Color.HotPink;
			pic61.BorderStyle = BorderStyle.Fixed3D;
			pic61.Location = new Point(54, 45);
			pic61.Name = "pic61";
			pic61.Size = new Size(80, 64);
			pic61.TabIndex = 68;
			pic61.Tag = "Hot Pink";
			pic61.Click += color_Custom_Click;
			// 
			// pic62
			// 
			pic62.BackColor = Color.Tomato;
			pic62.BorderStyle = BorderStyle.Fixed3D;
			pic62.Location = new Point(140, 45);
			pic62.Name = "pic62";
			pic62.Size = new Size(80, 64);
			pic62.TabIndex = 69;
			pic62.Tag = "Tomato";
			pic62.Click += color_Custom_Click;
			// 
			// pic63
			// 
			pic63.BackColor = Color.Chocolate;
			pic63.BorderStyle = BorderStyle.Fixed3D;
			pic63.Location = new Point(226, 45);
			pic63.Name = "pic63";
			pic63.Size = new Size(80, 64);
			pic63.TabIndex = 70;
			pic63.Tag = "Chocolate";
			pic63.Click += color_Custom_Click;
			// 
			// pic64
			// 
			pic64.BackColor = Color.Gold;
			pic64.BorderStyle = BorderStyle.Fixed3D;
			pic64.Location = new Point(312, 45);
			pic64.Name = "pic64";
			pic64.Size = new Size(80, 64);
			pic64.TabIndex = 71;
			pic64.Tag = "Gold";
			pic64.Click += color_Custom_Click;
			// 
			// pic65
			// 
			pic65.BackColor = Color.GreenYellow;
			pic65.BorderStyle = BorderStyle.Fixed3D;
			pic65.Location = new Point(398, 45);
			pic65.Name = "pic65";
			pic65.Size = new Size(80, 64);
			pic65.TabIndex = 72;
			pic65.Tag = "Green Yellow";
			pic65.Click += color_Custom_Click;
			// 
			// pic66
			// 
			pic66.BackColor = Color.Aqua;
			pic66.BorderStyle = BorderStyle.Fixed3D;
			pic66.Location = new Point(484, 45);
			pic66.Name = "pic66";
			pic66.Size = new Size(80, 64);
			pic66.TabIndex = 73;
			pic66.Tag = "Aqua";
			pic66.Click += color_Custom_Click;
			// 
			// pic67
			// 
			pic67.BackColor = Color.SkyBlue;
			pic67.BorderStyle = BorderStyle.Fixed3D;
			pic67.Location = new Point(570, 45);
			pic67.Name = "pic67";
			pic67.Size = new Size(80, 64);
			pic67.TabIndex = 74;
			pic67.Tag = "Sky Blue";
			pic67.Click += color_Custom_Click;
			// 
			// pic68
			// 
			pic68.BackColor = Color.DarkViolet;
			pic68.BorderStyle = BorderStyle.Fixed3D;
			pic68.Location = new Point(656, 45);
			pic68.Name = "pic68";
			pic68.Size = new Size(80, 64);
			pic68.TabIndex = 75;
			pic68.Tag = "Dark Violet";
			pic68.Click += color_Custom_Click;
			// 
			// pic71
			// 
			pic71.BackColor = Color.LavenderBlush;
			pic71.BorderStyle = BorderStyle.Fixed3D;
			pic71.Location = new Point(54, 115);
			pic71.Name = "pic71";
			pic71.Size = new Size(80, 64);
			pic71.TabIndex = 76;
			pic71.Tag = "Lavender Blush";
			pic71.Click += color_Custom_Click;
			// 
			// pic72
			// 
			pic72.BackColor = Color.LightSalmon;
			pic72.BorderStyle = BorderStyle.Fixed3D;
			pic72.Location = new Point(140, 115);
			pic72.Name = "pic72";
			pic72.Size = new Size(80, 64);
			pic72.TabIndex = 77;
			pic72.Tag = "Light Salmon";
			pic72.Click += color_Custom_Click;
			// 
			// pic73
			// 
			pic73.BackColor = Color.BurlyWood;
			pic73.BorderStyle = BorderStyle.Fixed3D;
			pic73.Location = new Point(226, 115);
			pic73.Name = "pic73";
			pic73.Size = new Size(80, 64);
			pic73.TabIndex = 78;
			pic73.Tag = "Burly Wood";
			pic73.Click += color_Custom_Click;
			// 
			// pic74
			// 
			pic74.BackColor = Color.Khaki;
			pic74.BorderStyle = BorderStyle.Fixed3D;
			pic74.Location = new Point(312, 115);
			pic74.Name = "pic74";
			pic74.Size = new Size(80, 64);
			pic74.TabIndex = 79;
			pic74.Tag = "Khaki";
			pic74.Click += color_Custom_Click;
			// 
			// pic75
			// 
			pic75.BackColor = Color.MediumSeaGreen;
			pic75.BorderStyle = BorderStyle.Fixed3D;
			pic75.Location = new Point(398, 115);
			pic75.Name = "pic75";
			pic75.Size = new Size(80, 64);
			pic75.TabIndex = 80;
			pic75.Tag = "Medium Sea Green";
			pic75.Click += color_Custom_Click;
			// 
			// pic76
			// 
			pic76.BackColor = Color.DarkTurquoise;
			pic76.BorderStyle = BorderStyle.Fixed3D;
			pic76.Location = new Point(484, 115);
			pic76.Name = "pic76";
			pic76.Size = new Size(80, 64);
			pic76.TabIndex = 81;
			pic76.Tag = "Dark Turquoise";
			pic76.Click += color_Custom_Click;
			// 
			// pic77
			// 
			pic77.BackColor = Color.RoyalBlue;
			pic77.BorderStyle = BorderStyle.Fixed3D;
			pic77.Location = new Point(570, 115);
			pic77.Name = "pic77";
			pic77.Size = new Size(80, 64);
			pic77.TabIndex = 82;
			pic77.Tag = "Royal Blue";
			pic77.Click += color_Custom_Click;
			// 
			// pic78
			// 
			pic78.BackColor = Color.MediumOrchid;
			pic78.BorderStyle = BorderStyle.Fixed3D;
			pic78.Location = new Point(656, 115);
			pic78.Name = "pic78";
			pic78.Size = new Size(80, 64);
			pic78.TabIndex = 83;
			pic78.Tag = "Medium Orchid";
			pic78.Click += color_Custom_Click;
			// 
			// picSelection
			// 
			picSelection.BackColor = SystemColors.ButtonFace;
			picSelection.BorderStyle = BorderStyle.Fixed3D;
			picSelection.Location = new Point(389, 928);
			picSelection.Name = "picSelection";
			picSelection.Size = new Size(80, 64);
			picSelection.SizeMode = PictureBoxSizeMode.StretchImage;
			picSelection.TabIndex = 89;
			picSelection.TabStop = false;
			picSelection.Paint += picSelection_Paint;
			// 
			// lblDirty
			// 
			lblDirty.AutoSize = true;
			lblDirty.Font = new Font("Segoe UI", 6.75F, FontStyle.Italic);
			lblDirty.ForeColor = SystemColors.GrayText;
			lblDirty.Location = new Point(45, 939);
			lblDirty.Margin = new Padding(7, 0, 7, 0);
			lblDirty.Name = "lblDirty";
			lblDirty.Size = new Size(58, 25);
			lblDirty.TabIndex = 92;
			lblDirty.Text = "Clean";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label1.ForeColor = SystemColors.ControlText;
			label1.Location = new Point(272, 944);
			label1.Margin = new Padding(7, 0, 7, 0);
			label1.Name = "label1";
			label1.Size = new Size(117, 32);
			label1.TabIndex = 93;
			label1.Text = "Selection:";
			// 
			// frmColors
			// 
			AcceptButton = btnOK;
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(830, 1014);
			Controls.Add(label1);
			Controls.Add(lblDirty);
			Controls.Add(picSelection);
			Controls.Add(grpCommon);
			Controls.Add(groupBox2);
			Controls.Add(grpAdditional);
			Controls.Add(grpCustom);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "frmColors";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Choose a color";
			FormClosing += frmColors_FormClosing;
			Load += frmColors_Load;
			Shown += frmColors_Shown;
			ResizeBegin += frmColors_ResizeBegin;
			ResizeEnd += frmColors_ResizeEnd;
			grpCommon.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)picMulti).EndInit();
			((System.ComponentModel.ISupportInitialize)picRGBW).EndInit();
			((System.ComponentModel.ISupportInitialize)picRGB).EndInit();
			grpAdditional.ResumeLayout(false);
			grpCustom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)picSelection).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Button btnOK;
		private Button btnCancel;
		private GroupBox grpCommon;
		private GroupBox groupBox2;
		private GroupBox grpAdditional;
		private GroupBox grpCustom;
		private Label lblCoolWhite;
		private Label lblWarmWhite;
		private Label lblRed;
		private Label lblOrange;
		private Label lblYellow;
		private Label lblGreen;
		private Label lblBlue;
		private Label lblPurple;
		private Label lblPink;
		private PictureBox picMulti;
		private PictureBox picRGBW;
		private PictureBox picRGB;
		private PictureBox picSelection;
		private Label lblDirty;

		private Label pic11;
		private Label pic12;
		private Label pic13;
		private Label pic14;
		private Label pic15;
		private Label pic16;
		private Label pic17;
		private Label pic18;
		private Label pic21;
		private Label pic22;
		private Label pic23;
		private Label pic24;
		private Label pic25;
		private Label pic26;
		private Label pic27;
		private Label pic28;
		private Label pic31;
		private Label pic32;
		private Label pic33;
		private Label pic34;
		private Label pic35;
		private Label pic36;
		private Label pic37;
		private Label pic38;
		private Label pic41;
		private Label pic42;
		private Label pic43;
		private Label pic44;
		private Label pic45;
		private Label pic46;
		private Label pic47;
		private Label pic48;
		private Label pic51;
		private Label pic52;
		private Label pic53;
		private Label pic54;
		private Label pic55;
		private Label pic56;
		private Label pic57;
		private Label pic58;
		private Label pic61;
		private Label pic62;
		private Label pic63;
		private Label pic64;
		private Label pic65;
		private Label pic66;
		private Label pic67;
		private Label pic68;
		private Label pic71;
		private Label pic72;
		private Label pic73;
		private Label pic74;
		private Label pic75;
		private Label pic76;
		private Label pic77;
		private Label pic78;
		private Label label1;
	}
}