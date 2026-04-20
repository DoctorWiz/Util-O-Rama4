
namespace UtilORama4
{
	partial class frmChannel
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChannel));
			btnOK = new Button();
			btnCancel = new Button();
			lblName = new Label();
			txtName = new TextBox();
			txtLocation = new TextBox();
			lblLocation = new Label();
			lblType = new Label();
			lblColorLabel = new Label();
			cboController = new ComboBox();
			lblController = new Label();
			numOutput = new NumericUpDown();
			lblOutput = new Label();
			lblUniverse = new Label();
			lblAddress = new Label();
			lblxLightsAddress = new Label();
			chkActive = new CheckBox();
			lblModel = new Label();
			txtComment = new TextBox();
			lblComment = new Label();
			clrColors = new ColorDialog();
			tipTool = new ToolTip(components);
			lblDevice = new Label();
			lblDirty = new Label();
			picRGB = new PictureBox();
			picRGBW = new PictureBox();
			picMulti = new PictureBox();
			picColor = new PictureBox();
			cboDevice = new ComboBox();
			btnPrevious = new Button();
			btnNext = new Button();
			label1 = new Label();
			((System.ComponentModel.ISupportInitialize)numOutput).BeginInit();
			((System.ComponentModel.ISupportInitialize)picRGB).BeginInit();
			((System.ComponentModel.ISupportInitialize)picRGBW).BeginInit();
			((System.ComponentModel.ISupportInitialize)picMulti).BeginInit();
			((System.ComponentModel.ISupportInitialize)picColor).BeginInit();
			SuspendLayout();
			// 
			// btnOK
			// 
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Location = new Point(468, 926);
			btnOK.Margin = new Padding(7, 6, 7, 6);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(163, 58);
			btnOK.TabIndex = 19;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			btnOK.Click += btnOK_Click;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(644, 926);
			btnCancel.Margin = new Padding(7, 6, 7, 6);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(163, 58);
			btnCancel.TabIndex = 20;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// lblName
			// 
			lblName.AutoSize = true;
			lblName.Location = new Point(56, 42);
			lblName.Margin = new Padding(7, 0, 7, 0);
			lblName.Name = "lblName";
			lblName.Size = new Size(83, 32);
			lblName.TabIndex = 0;
			lblName.Text = "Name:";
			lblName.Click += lblName_Click;
			// 
			// txtName
			// 
			txtName.Location = new Point(152, 39);
			txtName.Margin = new Padding(7, 6, 7, 6);
			txtName.MaxLength = 100;
			txtName.Name = "txtName";
			txtName.Size = new Size(645, 39);
			txtName.TabIndex = 4;
			txtName.TextChanged += txtName_TextChanged;
			txtName.Enter += txtName_Enter;
			txtName.KeyDown += txtName_KeyDown;
			txtName.Leave += txtName_Leave;
			txtName.Validating += txtName_Validating;
			// 
			// txtLocation
			// 
			txtLocation.Location = new Point(152, 118);
			txtLocation.Margin = new Padding(7, 6, 7, 6);
			txtLocation.MaxLength = 40;
			txtLocation.Name = "txtLocation";
			txtLocation.Size = new Size(429, 39);
			txtLocation.TabIndex = 1;
			tipTool.SetToolTip(txtLocation, "Where is the item(s) on this channel located?");
			txtLocation.Enter += txtLocation_Enter;
			txtLocation.KeyDown += txtLocation_KeyDown;
			txtLocation.Leave += txtLocation_Leave;
			txtLocation.Validating += txtLocation_Validating;
			// 
			// lblLocation
			// 
			lblLocation.AutoSize = true;
			lblLocation.Location = new Point(28, 121);
			lblLocation.Margin = new Padding(7, 0, 7, 0);
			lblLocation.Name = "lblLocation";
			lblLocation.Size = new Size(109, 32);
			lblLocation.TabIndex = 3;
			lblLocation.Text = "Location:";
			// 
			// lblType
			// 
			lblType.AutoSize = true;
			lblType.Location = new Point(67, 279);
			lblType.Margin = new Padding(7, 0, 7, 0);
			lblType.Name = "lblType";
			lblType.Size = new Size(70, 32);
			lblType.TabIndex = 9;
			lblType.Text = "Type:";
			// 
			// lblColorLabel
			// 
			lblColorLabel.AutoSize = true;
			lblColorLabel.Location = new Point(631, 121);
			lblColorLabel.Margin = new Padding(7, 0, 7, 0);
			lblColorLabel.Name = "lblColorLabel";
			lblColorLabel.Size = new Size(76, 32);
			lblColorLabel.TabIndex = 2;
			lblColorLabel.Text = "Color:";
			lblColorLabel.Click += Pick_Color_Click;
			// 
			// cboController
			// 
			cboController.DropDownStyle = ComboBoxStyle.DropDownList;
			cboController.FormattingEnabled = true;
			cboController.Location = new Point(152, 197);
			cboController.Margin = new Padding(7, 6, 7, 6);
			cboController.Name = "cboController";
			cboController.Size = new Size(429, 40);
			cboController.TabIndex = 6;
			tipTool.SetToolTip(cboController, "Select the controller this channel is connected to.");
			cboController.DropDown += cboController_DropDown;
			cboController.SelectedIndexChanged += cboController_SelectedIndexChanged;
			cboController.Enter += cboController_Enter;
			cboController.KeyDown += cboController_KeyDown;
			cboController.Leave += cboController_Leave;
			cboController.Validating += cboController_Validating;
			// 
			// lblController
			// 
			lblController.AutoSize = true;
			lblController.Location = new Point(14, 200);
			lblController.Margin = new Padding(7, 0, 7, 0);
			lblController.Name = "lblController";
			lblController.Size = new Size(125, 32);
			lblController.TabIndex = 5;
			lblController.Text = "Controller:";
			// 
			// numOutput
			// 
			numOutput.Location = new Point(702, 200);
			numOutput.Margin = new Padding(7, 6, 7, 6);
			numOutput.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
			numOutput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numOutput.Name = "numOutput";
			numOutput.Size = new Size(95, 39);
			numOutput.TabIndex = 8;
			tipTool.SetToolTip(numOutput, "Select the output # on this controller that this channel is connected to.");
			numOutput.Value = new decimal(new int[] { 1, 0, 0, 0 });
			numOutput.ValueChanged += numOutput_ValueChanged;
			numOutput.Enter += numOutput_Enter;
			numOutput.KeyDown += numOutput_KeyDown;
			numOutput.Leave += numOutput_Leave;
			numOutput.Validating += numOutput_Validating;
			// 
			// lblOutput
			// 
			lblOutput.AutoSize = true;
			lblOutput.Location = new Point(595, 202);
			lblOutput.Margin = new Padding(7, 0, 7, 0);
			lblOutput.Name = "lblOutput";
			lblOutput.Size = new Size(95, 32);
			lblOutput.TabIndex = 7;
			lblOutput.Text = "Output:";
			// 
			// lblUniverse
			// 
			lblUniverse.AutoSize = true;
			lblUniverse.Location = new Point(152, 372);
			lblUniverse.Margin = new Padding(7, 0, 7, 0);
			lblUniverse.Name = "lblUniverse";
			lblUniverse.Size = new Size(131, 32);
			lblUniverse.TabIndex = 13;
			lblUniverse.Text = "Universe: 1";
			tipTool.SetToolTip(lblUniverse, "The DMX Universe this controller and this channel are connected to.");
			// 
			// lblAddress
			// 
			lblAddress.AutoSize = true;
			lblAddress.Location = new Point(152, 436);
			lblAddress.Margin = new Padding(7, 0, 7, 0);
			lblAddress.Name = "lblAddress";
			lblAddress.Size = new Size(183, 32);
			lblAddress.TabIndex = 15;
			lblAddress.Text = "DMX Address: 1";
			tipTool.SetToolTip(lblAddress, "The DMX address of this channel in this DMX Universe.");
			// 
			// lblxLightsAddress
			// 
			lblxLightsAddress.AutoSize = true;
			lblxLightsAddress.Location = new Point(152, 468);
			lblxLightsAddress.Margin = new Padding(7, 0, 7, 0);
			lblxLightsAddress.Name = "lblxLightsAddress";
			lblxLightsAddress.Size = new Size(204, 32);
			lblxLightsAddress.TabIndex = 16;
			lblxLightsAddress.Text = "xLights Address: 1";
			tipTool.SetToolTip(lblxLightsAddress, "The xLights address of this channel.");
			// 
			// chkActive
			// 
			chkActive.AutoSize = true;
			chkActive.Checked = true;
			chkActive.CheckState = CheckState.Checked;
			chkActive.Location = new Point(686, 278);
			chkActive.Margin = new Padding(7, 6, 7, 6);
			chkActive.Name = "chkActive";
			chkActive.Size = new Size(111, 36);
			chkActive.TabIndex = 11;
			chkActive.Text = "Active";
			tipTool.SetToolTip(chkActive, "Is this channel in active use?");
			chkActive.UseVisualStyleBackColor = true;
			chkActive.CheckedChanged += chkActive_CheckedChanged;
			chkActive.Enter += chkActive_Enter;
			chkActive.KeyDown += chkActive_KeyDown;
			chkActive.Leave += chkActive_Leave;
			chkActive.Validating += chkActive_Validating;
			// 
			// lblModel
			// 
			lblModel.AutoSize = true;
			lblModel.Location = new Point(152, 404);
			lblModel.Margin = new Padding(7, 0, 7, 0);
			lblModel.Name = "lblModel";
			lblModel.Size = new Size(242, 32);
			lblModel.TabIndex = 14;
			lblModel.Text = "LOR LOR1602W Gen3";
			tipTool.SetToolTip(lblModel, "The brand and model of this controller.");
			// 
			// txtComment
			// 
			txtComment.Location = new Point(152, 638);
			txtComment.Margin = new Padding(7, 6, 7, 6);
			txtComment.Multiline = true;
			txtComment.Name = "txtComment";
			txtComment.Size = new Size(645, 232);
			txtComment.TabIndex = 18;
			tipTool.SetToolTip(txtComment, "Comments, notes, and other important information about this channel.");
			txtComment.Enter += txtComment_Enter;
			txtComment.KeyDown += txtComment_KeyDown;
			txtComment.Leave += txtComment_Leave;
			txtComment.Validating += txtComment_Validating;
			// 
			// lblComment
			// 
			lblComment.AutoSize = true;
			lblComment.Location = new Point(28, 645);
			lblComment.Margin = new Padding(7, 0, 7, 0);
			lblComment.Name = "lblComment";
			lblComment.Size = new Size(125, 32);
			lblComment.TabIndex = 17;
			lblComment.Text = "Comment:";
			// 
			// lblDevice
			// 
			lblDevice.AutoSize = true;
			lblDevice.Font = new Font("Segoe UI", 6.75F, FontStyle.Italic);
			lblDevice.ForeColor = SystemColors.GrayText;
			lblDevice.Location = new Point(152, 319);
			lblDevice.Margin = new Padding(7, 0, 7, 0);
			lblDevice.Name = "lblDevice";
			lblDevice.Size = new Size(102, 25);
			lblDevice.TabIndex = 12;
			lblDevice.Text = "Unassigned";
			tipTool.SetToolTip(lblDevice, "No device assigned.");
			// 
			// lblDirty
			// 
			lblDirty.AutoSize = true;
			lblDirty.Font = new Font("Segoe UI", 6.75F, FontStyle.Italic);
			lblDirty.ForeColor = SystemColors.GrayText;
			lblDirty.Location = new Point(609, 895);
			lblDirty.Margin = new Padding(7, 0, 7, 0);
			lblDirty.Name = "lblDirty";
			lblDirty.Size = new Size(58, 25);
			lblDirty.TabIndex = 22;
			lblDirty.Text = "Clean";
			// 
			// picRGB
			// 
			picRGB.BackColor = Color.FromArgb(0, 0, 1);
			picRGB.BorderStyle = BorderStyle.Fixed3D;
			picRGB.Image = (Image)resources.GetObject("picRGB.Image");
			picRGB.Location = new Point(37, 717);
			picRGB.Name = "picRGB";
			picRGB.Size = new Size(80, 72);
			picRGB.SizeMode = PictureBoxSizeMode.StretchImage;
			picRGB.TabIndex = 87;
			picRGB.TabStop = false;
			picRGB.Visible = false;
			// 
			// picRGBW
			// 
			picRGBW.BackColor = Color.FromArgb(0, 1, 0);
			picRGBW.BorderStyle = BorderStyle.Fixed3D;
			picRGBW.Image = (Image)resources.GetObject("picRGBW.Image");
			picRGBW.Location = new Point(15, 693);
			picRGBW.Name = "picRGBW";
			picRGBW.Size = new Size(80, 72);
			picRGBW.SizeMode = PictureBoxSizeMode.StretchImage;
			picRGBW.TabIndex = 88;
			picRGBW.TabStop = false;
			picRGBW.Visible = false;
			picRGBW.Click += picRGBW_Click;
			// 
			// picMulti
			// 
			picMulti.BackColor = Color.FromArgb(1, 0, 0);
			picMulti.BorderStyle = BorderStyle.Fixed3D;
			picMulti.Image = (Image)resources.GetObject("picMulti.Image");
			picMulti.Location = new Point(57, 741);
			picMulti.Name = "picMulti";
			picMulti.Size = new Size(80, 72);
			picMulti.SizeMode = PictureBoxSizeMode.StretchImage;
			picMulti.TabIndex = 89;
			picMulti.TabStop = false;
			picMulti.Visible = false;
			// 
			// picColor
			// 
			picColor.BackColor = SystemColors.ButtonFace;
			picColor.BorderStyle = BorderStyle.Fixed3D;
			picColor.Image = (Image)resources.GetObject("picColor.Image");
			picColor.Location = new Point(717, 107);
			picColor.Name = "picColor";
			picColor.Size = new Size(80, 64);
			picColor.SizeMode = PictureBoxSizeMode.StretchImage;
			picColor.TabIndex = 90;
			picColor.TabStop = false;
			picColor.Click += picColor_Click;
			picColor.Paint += picColor_Paint;
			// 
			// cboDevice
			// 
			cboDevice.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			cboDevice.AutoCompleteSource = AutoCompleteSource.ListItems;
			cboDevice.FormattingEnabled = true;
			cboDevice.Location = new Point(152, 276);
			cboDevice.Name = "cboDevice";
			cboDevice.Size = new Size(429, 40);
			cboDevice.TabIndex = 10;
			cboDevice.SelectedIndexChanged += cboDevice_SelectedIndexChanged;
			cboDevice.Enter += cboDevice_Enter;
			cboDevice.KeyDown += cboDevice_KeyDown;
			cboDevice.Leave += cboDevice_Leave;
			cboDevice.Validating += cboDevice_Validating;
			// 
			// btnPrevious
			// 
			btnPrevious.DialogResult = DialogResult.OK;
			btnPrevious.Enabled = false;
			btnPrevious.Image = (Image)resources.GetObject("btnPrevious.Image");
			btnPrevious.Location = new Point(16, 926);
			btnPrevious.Margin = new Padding(7, 6, 7, 6);
			btnPrevious.Name = "btnPrevious";
			btnPrevious.Size = new Size(58, 58);
			btnPrevious.TabIndex = 91;
			tipTool.SetToolTip(btnPrevious, "Save and move to the previous channel on this controller.");
			btnPrevious.UseVisualStyleBackColor = true;
			// 
			// btnNext
			// 
			btnNext.DialogResult = DialogResult.OK;
			btnNext.Enabled = false;
			btnNext.Image = (Image)resources.GetObject("btnNext.Image");
			btnNext.Location = new Point(88, 926);
			btnNext.Margin = new Padding(7, 6, 7, 6);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(58, 58);
			btnNext.TabIndex = 92;
			tipTool.SetToolTip(btnNext, "Save and move to the next channel on this controller.");
			btnNext.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(22, 856);
			label1.Margin = new Padding(7, 0, 7, 0);
			label1.Name = "label1";
			label1.Size = new Size(124, 64);
			label1.TabIndex = 93;
			label1.Text = "Prev  Next\r\nChannel";
			label1.TextAlign = ContentAlignment.TopCenter;
			tipTool.SetToolTip(label1, "Save and move to the previous or next channel on this controller.");
			// 
			// frmChannel
			// 
			AcceptButton = btnOK;
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(832, 1011);
			Controls.Add(btnNext);
			Controls.Add(btnPrevious);
			Controls.Add(cboDevice);
			Controls.Add(picMulti);
			Controls.Add(picRGBW);
			Controls.Add(picRGB);
			Controls.Add(lblDirty);
			Controls.Add(txtComment);
			Controls.Add(lblComment);
			Controls.Add(lblModel);
			Controls.Add(chkActive);
			Controls.Add(lblxLightsAddress);
			Controls.Add(lblAddress);
			Controls.Add(lblUniverse);
			Controls.Add(lblOutput);
			Controls.Add(numOutput);
			Controls.Add(cboController);
			Controls.Add(lblController);
			Controls.Add(lblColorLabel);
			Controls.Add(lblType);
			Controls.Add(txtLocation);
			Controls.Add(lblLocation);
			Controls.Add(txtName);
			Controls.Add(lblName);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Controls.Add(picColor);
			Controls.Add(lblDevice);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Margin = new Padding(7, 6, 7, 6);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "frmChannel";
			ShowInTaskbar = false;
			Text = "Channel";
			FormClosing += frmChannel_FormClosing;
			Load += frmChannel_Load;
			Shown += frmChannel_Shown;
			ResizeBegin += frmChannel_ResizeBegin;
			ResizeEnd += frmChannel_ResizeEnd;
			Paint += frmChannel_Paint;
			Resize += frmChannel_Resize;
			((System.ComponentModel.ISupportInitialize)numOutput).EndInit();
			((System.ComponentModel.ISupportInitialize)picRGB).EndInit();
			((System.ComponentModel.ISupportInitialize)picRGBW).EndInit();
			((System.ComponentModel.ISupportInitialize)picMulti).EndInit();
			((System.ComponentModel.ISupportInitialize)picColor).EndInit();
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.Label lblColorLabel;
		private System.Windows.Forms.ComboBox cboController;
		private System.Windows.Forms.Label lblController;
		private System.Windows.Forms.NumericUpDown numOutput;
		private System.Windows.Forms.Label lblOutput;
		private System.Windows.Forms.Label lblUniverse;
		private System.Windows.Forms.Label lblAddress;
		private System.Windows.Forms.Label lblxLightsAddress;
		private System.Windows.Forms.CheckBox chkActive;
		private System.Windows.Forms.ToolTip tipTool;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.Label lblDevice;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Label lblComment;
		private System.Windows.Forms.ColorDialog clrColors;
		private System.Windows.Forms.Label lblDirty;
		private System.Windows.Forms.PictureBox picRGB;
		private System.Windows.Forms.PictureBox picRGBW;
		private System.Windows.Forms.PictureBox picMulti;
		private System.Windows.Forms.PictureBox picColor;
		private System.Windows.Forms.ComboBox cboDevice;
		private Button btnPrevious;
		private Button btnNext;
		private Label label1;
	}
}