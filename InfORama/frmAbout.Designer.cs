namespace UtilORama4
{
	partial class frmAbout
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
			lblVersion = new Label();
			lblCopyright = new Label();
			btnOK = new Button();
			picIcon = new PictureBox();
			lblProductName = new Label();
			textBoxDescription = new TextBox();
			linkCompanyName = new LinkLabel();
			linkAuthorName = new LinkLabel();
			lblAnd1 = new Label();
			lblFreeware = new Label();
			lblSourceCode = new Label();
			linkGPL = new LinkLabel();
			lblInfo = new Label();
			linkEmail = new LinkLabel();
			picGPL = new PictureBox();
			picxLights = new PictureBox();
			lblMember = new Label();
			labelUtils = new LinkLabel();
			lblSuite = new Label();
			lblBenefit = new Label();
			lblDisclaimer = new Label();
			picLOR = new PictureBox();
			lblAlpha = new Label();
			lblBeta = new Label();
			picDrWiz = new PictureBox();
			lblThanks = new Label();
			lblGitHub = new Label();
			lblGitIssues = new LinkLabel();
			linkCharity = new LinkLabel();
			lblProgram = new Label();
			lblCompiled = new Label();
			lblEXEfile = new Label();
			lblFor = new Label();
			lblReleased = new Label();
			tipTool = new ToolTip(components);
			lblAnd2 = new Label();
			lblCommunity = new Label();
			((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
			((System.ComponentModel.ISupportInitialize)picGPL).BeginInit();
			((System.ComponentModel.ISupportInitialize)picxLights).BeginInit();
			((System.ComponentModel.ISupportInitialize)picLOR).BeginInit();
			((System.ComponentModel.ISupportInitialize)picDrWiz).BeginInit();
			SuspendLayout();
			// 
			// lblVersion
			// 
			lblVersion.AutoSize = true;
			lblVersion.Font = new Font("Calibri", 12F);
			lblVersion.Location = new Point(325, 115);
			lblVersion.Margin = new Padding(13, 0, 7, 0);
			lblVersion.MaximumSize = new Size(0, 43);
			lblVersion.Name = "lblVersion";
			lblVersion.Size = new Size(113, 39);
			lblVersion.TabIndex = 25;
			lblVersion.Text = "Version";
			lblVersion.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblCopyright
			// 
			lblCopyright.AutoSize = true;
			lblCopyright.Font = new Font("Calibri", 12F);
			lblCopyright.Location = new Point(325, 209);
			lblCopyright.Margin = new Padding(13, 0, 7, 0);
			lblCopyright.Name = "lblCopyright";
			lblCopyright.Size = new Size(302, 39);
			lblCopyright.TabIndex = 28;
			lblCopyright.Text = "Copyright © 2026+ by";
			tipTool.SetToolTip(lblCopyright, "For eternity...");
			// 
			// btnOK
			// 
			btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnOK.DialogResult = DialogResult.Cancel;
			btnOK.Location = new Point(726, 785);
			btnOK.Margin = new Padding(7, 6, 7, 6);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(163, 58);
			btnOK.TabIndex = 31;
			btnOK.Text = "&OK";
			tipTool.SetToolTip(btnOK, "I Understand.\r\nGot it.");
			btnOK.Click += okButton_Click_1;
			// 
			// picIcon
			// 
			picIcon.ErrorImage = null;
			picIcon.Image = (Image)resources.GetObject("picIcon.Image");
			picIcon.InitialImage = null;
			picIcon.Location = new Point(26, 30);
			picIcon.Margin = new Padding(7, 6, 7, 6);
			picIcon.Name = "picIcon";
			picIcon.Size = new Size(256, 256);
			picIcon.SizeMode = PictureBoxSizeMode.StretchImage;
			picIcon.TabIndex = 32;
			picIcon.TabStop = false;
			tipTool.SetToolTip(picIcon, "How about this cool icon I made?");
			picIcon.Click += picIcon_Click;
			picIcon.MouseEnter += picIcon_MouseEnter;
			picIcon.MouseLeave += picIcon_MouseLeave;
			// 
			// lblProductName
			// 
			lblProductName.AutoSize = true;
			lblProductName.Font = new Font("Calibri", 15.75F, FontStyle.Bold | FontStyle.Italic);
			lblProductName.Location = new Point(323, 30);
			lblProductName.Margin = new Padding(13, 0, 7, 0);
			lblProductName.Name = "lblProductName";
			lblProductName.Size = new Size(289, 51);
			lblProductName.TabIndex = 27;
			lblProductName.Text = "Program Name";
			lblProductName.TextAlign = ContentAlignment.MiddleLeft;
			tipTool.SetToolTip(lblProductName, "That's what this is called.");
			// 
			// textBoxDescription
			// 
			textBoxDescription.BorderStyle = BorderStyle.None;
			textBoxDescription.Font = new Font("Calibri", 11.25F);
			textBoxDescription.Location = new Point(329, 218);
			textBoxDescription.Margin = new Padding(7, 6, 7, 6);
			textBoxDescription.Multiline = true;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.ReadOnly = true;
			textBoxDescription.Size = new Size(448, 141);
			textBoxDescription.TabIndex = 33;
			textBoxDescription.Visible = false;
			// 
			// linkCompanyName
			// 
			linkCompanyName.AutoSize = true;
			linkCompanyName.Font = new Font("Calibri", 12F);
			linkCompanyName.Location = new Point(557, 252);
			linkCompanyName.Margin = new Padding(7, 0, 7, 0);
			linkCompanyName.Name = "linkCompanyName";
			linkCompanyName.Size = new Size(288, 39);
			linkCompanyName.TabIndex = 34;
			linkCompanyName.TabStop = true;
			linkCompanyName.Text = "W⚡zlights Software";
			tipTool.SetToolTip(linkCompanyName, "https://wizlights.org/\r\nA division of Wizster Software\r\nhttps://wizster.com\r\nA division of Wizmotronics\r\nhttps://wizmotronics.com\r\nFrom the brain of Doctor Wizard\r\nhttps://drwiz.net");
			linkCompanyName.LinkClicked += labelCompanyName_LinkClicked;
			// 
			// linkAuthorName
			// 
			linkAuthorName.AutoEllipsis = true;
			linkAuthorName.AutoSize = true;
			linkAuthorName.Font = new Font("Calibri", 12F);
			linkAuthorName.Location = new Point(325, 252);
			linkAuthorName.Margin = new Padding(7, 0, 7, 0);
			linkAuthorName.Name = "linkAuthorName";
			linkAuthorName.Size = new Size(204, 39);
			linkAuthorName.TabIndex = 35;
			linkAuthorName.TabStop = true;
			linkAuthorName.Text = "Doctor Wizard";
			tipTool.SetToolTip(linkAuthorName, "https://drwiz.net");
			linkAuthorName.LinkClicked += labelAuthorName_LinkClicked;
			// 
			// lblAnd1
			// 
			lblAnd1.AutoSize = true;
			lblAnd1.Font = new Font("Calibri", 12F);
			lblAnd1.Location = new Point(505, 252);
			lblAnd1.Margin = new Padding(7, 0, 7, 0);
			lblAnd1.Name = "lblAnd1";
			lblAnd1.Size = new Size(66, 39);
			lblAnd1.TabIndex = 36;
			lblAnd1.Text = "and";
			// 
			// lblFreeware
			// 
			lblFreeware.Font = new Font("Calibri", 12F);
			lblFreeware.Location = new Point(251, 403);
			lblFreeware.Margin = new Padding(7, 0, 7, 0);
			lblFreeware.Name = "lblFreeware";
			lblFreeware.Size = new Size(899, 43);
			lblFreeware.TabIndex = 37;
			lblFreeware.Text = "Freeware ";
			// 
			// lblSourceCode
			// 
			lblSourceCode.AutoSize = true;
			lblSourceCode.Font = new Font("Calibri", 9.75F);
			lblSourceCode.Location = new Point(26, 516);
			lblSourceCode.Margin = new Padding(7, 0, 7, 0);
			lblSourceCode.Name = "lblSourceCode";
			lblSourceCode.Size = new Size(599, 32);
			lblSourceCode.TabIndex = 40;
			lblSourceCode.Text = "Source Code is available under a General Public License";
			// 
			// linkGPL
			// 
			linkGPL.AutoSize = true;
			linkGPL.Font = new Font("Calibri", 9.75F);
			linkGPL.Location = new Point(696, 516);
			linkGPL.Margin = new Padding(7, 0, 7, 0);
			linkGPL.Name = "linkGPL";
			linkGPL.Size = new Size(70, 32);
			linkGPL.TabIndex = 41;
			linkGPL.TabStop = true;
			linkGPL.Text = "(GPL)";
			linkGPL.Visible = false;
			linkGPL.LinkClicked += labelGPL_LinkClicked;
			// 
			// lblInfo
			// 
			lblInfo.Font = new Font("Calibri", 9F);
			lblInfo.Location = new Point(26, 574);
			lblInfo.Margin = new Padding(7, 0, 7, 0);
			lblInfo.Name = "lblInfo";
			lblInfo.Size = new Size(862, 96);
			lblInfo.TabIndex = 42;
			lblInfo.Text = "For more information, or to submit bug reports, ideas, suggestions, cool sequences, or good dirty jokes, please contact Doctor Wizard at:";
			tipTool.SetToolTip(lblInfo, "Especially the good dirty jokes!");
			lblInfo.Click += label4_Click;
			// 
			// linkEmail
			// 
			linkEmail.Font = new Font("Courier New", 9F, FontStyle.Underline);
			linkEmail.Location = new Point(50, 636);
			linkEmail.Margin = new Padding(7, 0, 7, 0);
			linkEmail.Name = "linkEmail";
			linkEmail.Size = new Size(399, 38);
			linkEmail.TabIndex = 43;
			linkEmail.TabStop = true;
			linkEmail.Text = "dev.utilorama@wizlights.com";
			linkEmail.LinkClicked += lblEmail_LinkClicked;
			// 
			// picGPL
			// 
			picGPL.BackgroundImage = (Image)resources.GetObject("picGPL.BackgroundImage");
			picGPL.Location = new Point(644, 501);
			picGPL.Margin = new Padding(7, 6, 7, 6);
			picGPL.Name = "picGPL";
			picGPL.Size = new Size(119, 55);
			picGPL.SizeMode = PictureBoxSizeMode.Zoom;
			picGPL.TabIndex = 44;
			picGPL.TabStop = false;
			tipTool.SetToolTip(picGPL, "General Public License version 3\r\nhttps://www.gnu.org/licenses/gpl-3.0.en.html");
			picGPL.Click += picGPL_Click;
			picGPL.MouseEnter += picGPL_MouseEnter;
			picGPL.MouseLeave += picGPL_MouseLeave;
			// 
			// picxLights
			// 
			picxLights.BackgroundImage = (Image)resources.GetObject("picxLights.BackgroundImage");
			picxLights.InitialImage = (Image)resources.GetObject("picxLights.InitialImage");
			picxLights.Location = new Point(442, 446);
			picxLights.Margin = new Padding(7, 6, 7, 6);
			picxLights.Name = "picxLights";
			picxLights.Size = new Size(158, 55);
			picxLights.SizeMode = PictureBoxSizeMode.Zoom;
			picxLights.TabIndex = 45;
			picxLights.TabStop = false;
			tipTool.SetToolTip(picxLights, "xLights");
			picxLights.Click += picxLights_Click;
			picxLights.MouseEnter += picLOR_MouseEnter;
			picxLights.MouseLeave += picLOR_MouseLeave;
			// 
			// lblMember
			// 
			lblMember.Font = new Font("Calibri", 9F, FontStyle.Italic);
			lblMember.Location = new Point(672, 30);
			lblMember.Margin = new Padding(7, 0, 7, 0);
			lblMember.Name = "lblMember";
			lblMember.Size = new Size(245, 85);
			lblMember.TabIndex = 46;
			lblMember.Text = " is a member of the";
			// 
			// labelUtils
			// 
			labelUtils.AutoEllipsis = true;
			labelUtils.AutoSize = true;
			labelUtils.Font = new Font("Calibri", 9F, FontStyle.Italic);
			labelUtils.Location = new Point(672, 92);
			labelUtils.Margin = new Padding(7, 0, 7, 0);
			labelUtils.Name = "labelUtils";
			labelUtils.Size = new Size(133, 29);
			labelUtils.TabIndex = 47;
			labelUtils.TabStop = true;
			labelUtils.Text = "Util-O-Rama";
			labelUtils.TextAlign = ContentAlignment.TopRight;
			tipTool.SetToolTip(labelUtils, "https://wizster.org/utilorama");
			labelUtils.LinkClicked += labelUtils_LinkClicked;
			// 
			// lblSuite
			// 
			lblSuite.Font = new Font("Calibri", 9F, FontStyle.Italic);
			lblSuite.Location = new Point(799, 92);
			lblSuite.Margin = new Padding(7, 0, 7, 0);
			lblSuite.Name = "lblSuite";
			lblSuite.Size = new Size(76, 34);
			lblSuite.TabIndex = 48;
			lblSuite.Text = "Suite";
			lblSuite.Click += label5_Click;
			// 
			// lblBenefit
			// 
			lblBenefit.Font = new Font("Calibri", 12F);
			lblBenefit.Location = new Point(19, 446);
			lblBenefit.Margin = new Padding(7, 0, 7, 0);
			lblBenefit.Name = "lblBenefit";
			lblBenefit.Size = new Size(203, 43);
			lblBenefit.TabIndex = 49;
			lblBenefit.Text = "benefit of the";
			// 
			// lblDisclaimer
			// 
			lblDisclaimer.Font = new Font("Calibri", 8.25F, FontStyle.Italic);
			lblDisclaimer.ForeColor = SystemColors.ControlDarkDark;
			lblDisclaimer.Location = new Point(19, 719);
			lblDisclaimer.Margin = new Padding(7, 0, 7, 0);
			lblDisclaimer.Name = "lblDisclaimer";
			lblDisclaimer.Size = new Size(721, 141);
			lblDisclaimer.TabIndex = 50;
			lblDisclaimer.Text = resources.GetString("lblDisclaimer.Text");
			tipTool.SetToolTip(lblDisclaimer, "Seriously!  Back your stuff up, dude!\r\n  Remember: Jesus Saves -- Often!");
			// 
			// picLOR
			// 
			picLOR.BackgroundImage = (Image)resources.GetObject("picLOR.BackgroundImage");
			picLOR.Location = new Point(204, 448);
			picLOR.Margin = new Padding(7, 6, 7, 6);
			picLOR.Name = "picLOR";
			picLOR.Size = new Size(167, 41);
			picLOR.SizeMode = PictureBoxSizeMode.Zoom;
			picLOR.TabIndex = 51;
			picLOR.TabStop = false;
			tipTool.SetToolTip(picLOR, "Light-O-Rama");
			// 
			// lblAlpha
			// 
			lblAlpha.Font = new Font("Calibri", 8.25F, FontStyle.Italic);
			lblAlpha.ForeColor = Color.OrangeRed;
			lblAlpha.Location = new Point(1216, 301);
			lblAlpha.Margin = new Padding(7, 0, 7, 0);
			lblAlpha.Name = "lblAlpha";
			lblAlpha.Size = new Size(721, 141);
			lblAlpha.TabIndex = 52;
			lblAlpha.Text = resources.GetString("lblAlpha.Text");
			tipTool.SetToolTip(lblAlpha, "You're a glutton for punishment, aren't you?");
			// 
			// lblBeta
			// 
			lblBeta.Font = new Font("Calibri", 8.25F, FontStyle.Italic);
			lblBeta.ForeColor = Color.OrangeRed;
			lblBeta.Location = new Point(1216, 516);
			lblBeta.Margin = new Padding(7, 0, 7, 0);
			lblBeta.Name = "lblBeta";
			lblBeta.Size = new Size(721, 141);
			lblBeta.TabIndex = 53;
			lblBeta.Text = resources.GetString("lblBeta.Text");
			tipTool.SetToolTip(lblBeta, "Seriously!  Back your stuff up.  Remember: Jesus saves, often.");
			// 
			// picDrWiz
			// 
			picDrWiz.Image = (Image)resources.GetObject("picDrWiz.Image");
			picDrWiz.Location = new Point(386, 282);
			picDrWiz.Margin = new Padding(7, 6, 7, 6);
			picDrWiz.Name = "picDrWiz";
			picDrWiz.Size = new Size(89, 102);
			picDrWiz.SizeMode = PictureBoxSizeMode.StretchImage;
			picDrWiz.TabIndex = 54;
			picDrWiz.TabStop = false;
			tipTool.SetToolTip(picDrWiz, "Doctor Wizard was bored.");
			picDrWiz.Click += picDrWiz_Click;
			// 
			// lblThanks
			// 
			lblThanks.AutoSize = true;
			lblThanks.Font = new Font("Calibri", 12F, FontStyle.Italic);
			lblThanks.ForeColor = Color.DarkGreen;
			lblThanks.Location = new Point(726, 642);
			lblThanks.Margin = new Padding(13, 0, 7, 0);
			lblThanks.MaximumSize = new Size(0, 43);
			lblThanks.Name = "lblThanks";
			lblThanks.Size = new Size(132, 39);
			lblThanks.TabIndex = 55;
			lblThanks.Text = "Thanks...";
			lblThanks.TextAlign = ContentAlignment.MiddleLeft;
			tipTool.SetToolTip(lblThanks, "for all the fish.");
			lblThanks.Click += lblThanks_Click;
			// 
			// lblGitHub
			// 
			lblGitHub.AutoSize = true;
			lblGitHub.Font = new Font("Calibri", 9F);
			lblGitHub.Location = new Point(56, 676);
			lblGitHub.Margin = new Padding(7, 0, 7, 0);
			lblGitHub.Name = "lblGitHub";
			lblGitHub.Size = new Size(355, 29);
			lblGitHub.TabIndex = 56;
			lblGitHub.Text = "Or you can report issues on GitHub";
			// 
			// lblGitIssues
			// 
			lblGitIssues.AutoEllipsis = true;
			lblGitIssues.AutoSize = true;
			lblGitIssues.Font = new Font("Calibri", 9F);
			lblGitIssues.Location = new Point(412, 676);
			lblGitIssues.Margin = new Padding(7, 0, 7, 0);
			lblGitIssues.Name = "lblGitIssues";
			lblGitIssues.Size = new Size(60, 29);
			lblGitIssues.TabIndex = 57;
			lblGitIssues.TabStop = true;
			lblGitIssues.Text = "Here";
			lblGitIssues.TextAlign = ContentAlignment.TopRight;
			tipTool.SetToolTip(lblGitIssues, "ttps://github.com/DoctorWiz/Util-O-Rama4/issues");
			lblGitIssues.LinkClicked += lblGitIssues_LinkClicked;
			// 
			// linkCharity
			// 
			linkCharity.AutoEllipsis = true;
			linkCharity.AutoSize = true;
			linkCharity.Font = new Font("Calibri", 12F);
			linkCharity.Location = new Point(570, 403);
			linkCharity.Margin = new Padding(7, 0, 7, 0);
			linkCharity.Name = "linkCharity";
			linkCharity.Size = new Size(174, 39);
			linkCharity.TabIndex = 58;
			linkCharity.TabStop = true;
			linkCharity.Text = "Charityware";
			tipTool.SetToolTip(linkCharity, "https://wizlights.org/charitylist.html");
			linkCharity.LinkClicked += linkCharity_LinkClicked;
			// 
			// lblProgram
			// 
			lblProgram.AutoSize = true;
			lblProgram.Font = new Font("Calibri", 12F);
			lblProgram.Location = new Point(19, 403);
			lblProgram.Margin = new Padding(7, 0, 7, 0);
			lblProgram.Name = "lblProgram";
			lblProgram.Size = new Size(272, 39);
			lblProgram.TabIndex = 59;
			lblProgram.Text = "Something-O-Rama";
			// 
			// lblCompiled
			// 
			lblCompiled.AutoSize = true;
			lblCompiled.Font = new Font("Calibri", 8.25F);
			lblCompiled.ForeColor = Color.Gray;
			lblCompiled.Location = new Point(329, 154);
			lblCompiled.Margin = new Padding(13, 0, 7, 0);
			lblCompiled.MaximumSize = new Size(0, 43);
			lblCompiled.Name = "lblCompiled";
			lblCompiled.Size = new Size(199, 27);
			lblCompiled.TabIndex = 60;
			lblCompiled.Text = "Compiled 6/22/2022";
			lblCompiled.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblEXEfile
			// 
			lblEXEfile.AutoSize = true;
			lblEXEfile.Font = new Font("Calibri", 8.25F);
			lblEXEfile.ForeColor = Color.Goldenrod;
			lblEXEfile.Location = new Point(329, 183);
			lblEXEfile.Margin = new Padding(13, 0, 7, 0);
			lblEXEfile.MaximumSize = new Size(0, 43);
			lblEXEfile.Name = "lblEXEfile";
			lblEXEfile.Size = new Size(432, 27);
			lblEXEfile.TabIndex = 61;
			lblEXEfile.Text = "C:\\PortableApps\\UtilORama\\BlankORama.exe";
			lblEXEfile.TextAlign = ContentAlignment.MiddleLeft;
			lblEXEfile.Visible = false;
			// 
			// lblFor
			// 
			lblFor.AutoSize = true;
			lblFor.Font = new Font("Calibri", 12F);
			lblFor.Location = new Point(737, 403);
			lblFor.Margin = new Padding(7, 0, 7, 0);
			lblFor.Name = "lblFor";
			lblFor.Size = new Size(112, 39);
			lblFor.TabIndex = 62;
			lblFor.Text = " for the";
			// 
			// lblReleased
			// 
			lblReleased.AutoSize = true;
			lblReleased.Font = new Font("Calibri", 12F);
			lblReleased.Location = new Point(285, 401);
			lblReleased.Margin = new Padding(7, 0, 7, 0);
			lblReleased.Name = "lblReleased";
			lblReleased.Size = new Size(190, 39);
			lblReleased.TabIndex = 63;
			lblReleased.Text = "is released as";
			// 
			// lblAnd2
			// 
			lblAnd2.Font = new Font("Calibri", 12F);
			lblAnd2.Location = new Point(367, 448);
			lblAnd2.Margin = new Padding(7, 0, 7, 0);
			lblAnd2.Name = "lblAnd2";
			lblAnd2.Size = new Size(71, 43);
			lblAnd2.TabIndex = 64;
			lblAnd2.Text = "and";
			// 
			// lblCommunity
			// 
			lblCommunity.Font = new Font("Calibri", 12F);
			lblCommunity.Location = new Point(677, 442);
			lblCommunity.Margin = new Padding(7, 0, 7, 0);
			lblCommunity.Name = "lblCommunity";
			lblCommunity.Size = new Size(168, 43);
			lblCommunity.TabIndex = 65;
			lblCommunity.Text = "community.";
			// 
			// frmAbout
			// 
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(914, 870);
			Controls.Add(lblCommunity);
			Controls.Add(lblAnd2);
			Controls.Add(lblReleased);
			Controls.Add(lblFor);
			Controls.Add(lblEXEfile);
			Controls.Add(lblCompiled);
			Controls.Add(lblProgram);
			Controls.Add(btnOK);
			Controls.Add(linkCharity);
			Controls.Add(lblGitIssues);
			Controls.Add(lblGitHub);
			Controls.Add(lblThanks);
			Controls.Add(lblBeta);
			Controls.Add(lblAlpha);
			Controls.Add(picLOR);
			Controls.Add(lblDisclaimer);
			Controls.Add(lblSuite);
			Controls.Add(labelUtils);
			Controls.Add(lblMember);
			Controls.Add(picxLights);
			Controls.Add(picGPL);
			Controls.Add(linkEmail);
			Controls.Add(lblInfo);
			Controls.Add(linkGPL);
			Controls.Add(lblSourceCode);
			Controls.Add(lblAnd1);
			Controls.Add(linkAuthorName);
			Controls.Add(linkCompanyName);
			Controls.Add(picIcon);
			Controls.Add(lblProductName);
			Controls.Add(lblVersion);
			Controls.Add(lblCopyright);
			Controls.Add(lblFreeware);
			Controls.Add(lblBenefit);
			Controls.Add(picDrWiz);
			Controls.Add(textBoxDescription);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(7, 6, 7, 6);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "frmAbout";
			Padding = new Padding(19, 21, 19, 21);
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "About Program";
			Load += frmAbout_Load;
			Shown += frmAbout_Shown;
			((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
			((System.ComponentModel.ISupportInitialize)picGPL).EndInit();
			((System.ComponentModel.ISupportInitialize)picxLights).EndInit();
			((System.ComponentModel.ISupportInitialize)picLOR).EndInit();
			((System.ComponentModel.ISupportInitialize)picDrWiz).EndInit();
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.Button btnOK;
		public System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.Label lblProductName;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.LinkLabel linkCompanyName;
		private System.Windows.Forms.LinkLabel linkAuthorName;
		private System.Windows.Forms.Label lblAnd1;
		private System.Windows.Forms.Label lblFreeware;
		private System.Windows.Forms.Label lblSourceCode;
		private System.Windows.Forms.LinkLabel linkGPL;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.LinkLabel linkEmail;
		private System.Windows.Forms.PictureBox picGPL;
		private System.Windows.Forms.PictureBox picxLights;
		private System.Windows.Forms.Label lblMember;
		private System.Windows.Forms.LinkLabel labelUtils;
		private System.Windows.Forms.Label lblSuite;
		private System.Windows.Forms.Label lblBenefit;
		private System.Windows.Forms.Label lblDisclaimer;
		private System.Windows.Forms.PictureBox picLOR;
		private Label lblAlpha;
		private Label lblBeta;
		private PictureBox picDrWiz;
		private Label lblThanks;
		private Label lblGitHub;
		private LinkLabel lblGitIssues;
		private LinkLabel linkCharity;
		private Label lblProgram;
		private Label lblCompiled;
		private Label lblEXEfile;
		private Label lblFor;
		private Label lblReleased;
		private ToolTip tipTool;
		private Label lblAnd2;
		private Label lblCommunity;
	}
}
