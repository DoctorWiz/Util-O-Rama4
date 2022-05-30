
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChannel));
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.lblLocation = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cboType = new System.Windows.Forms.ComboBox();
			this.picColor2 = new System.Windows.Forms.PictureBox();
			this.lblColor = new System.Windows.Forms.Label();
			this.cboController = new System.Windows.Forms.ComboBox();
			this.lblController = new System.Windows.Forms.Label();
			this.numOutput = new System.Windows.Forms.NumericUpDown();
			this.lblOutput = new System.Windows.Forms.Label();
			this.lblUniverse = new System.Windows.Forms.Label();
			this.lblDMXAddress = new System.Windows.Forms.Label();
			this.lblxLighsAddress = new System.Windows.Forms.Label();
			this.chkActive = new System.Windows.Forms.CheckBox();
			this.lblModel = new System.Windows.Forms.Label();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.clrColors = new System.Windows.Forms.ColorDialog();
			this.tipTool = new System.Windows.Forms.ToolTip(this.components);
			this.picMulticolor = new System.Windows.Forms.PictureBox();
			this.picRGB = new System.Windows.Forms.PictureBox();
			this.btnColor = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picColor2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numOutput)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picMulticolor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picRGB)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(248, 434);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(88, 27);
			this.btnOK.TabIndex = 19;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(343, 434);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 27);
			this.btnCancel.TabIndex = 20;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(30, 22);
			this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(42, 15);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(82, 18);
			this.txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtName.MaxLength = 100;
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(349, 23);
			this.txtName.TabIndex = 1;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
			this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
			// 
			// txtLocation
			// 
			this.txtLocation.Location = new System.Drawing.Point(82, 55);
			this.txtLocation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtLocation.MaxLength = 40;
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(233, 23);
			this.txtLocation.TabIndex = 3;
			this.tipTool.SetToolTip(this.txtLocation, "Where is the item(s) on this channel located?");
			this.txtLocation.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocation_Validating);
			// 
			// lblLocation
			// 
			this.lblLocation.AutoSize = true;
			this.lblLocation.Location = new System.Drawing.Point(15, 59);
			this.lblLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(56, 15);
			this.lblLocation.TabIndex = 2;
			this.lblLocation.Text = "Location:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(35, 96);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 15);
			this.label1.TabIndex = 6;
			this.label1.Text = "Type:";
			// 
			// cboType
			// 
			this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboType.FormattingEnabled = true;
			this.cboType.Location = new System.Drawing.Point(82, 92);
			this.cboType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboType.Name = "cboType";
			this.cboType.Size = new System.Drawing.Size(233, 23);
			this.cboType.TabIndex = 7;
			this.tipTool.SetToolTip(this.cboType, "What type of device(s) or prop(s) are connected to this channel?");
			this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
			this.cboType.Validating += new System.ComponentModel.CancelEventHandler(this.cboType_Validating);
			// 
			// picColor2
			// 
			this.picColor2.BackColor = System.Drawing.Color.White;
			this.picColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picColor2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picColor2.Location = new System.Drawing.Point(163, 433);
			this.picColor2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picColor2.Name = "picColor2";
			this.picColor2.Size = new System.Drawing.Size(20, 20);
			this.picColor2.TabIndex = 8;
			this.picColor2.TabStop = false;
			this.tipTool.SetToolTip(this.picColor2, "White");
			this.picColor2.Visible = false;
			this.picColor2.Click += new System.EventHandler(this.picColor_Click);
			// 
			// lblColor
			// 
			this.lblColor.AutoSize = true;
			this.lblColor.Location = new System.Drawing.Point(356, 59);
			this.lblColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblColor.Name = "lblColor";
			this.lblColor.Size = new System.Drawing.Size(39, 15);
			this.lblColor.TabIndex = 4;
			this.lblColor.Text = "Color:";
			this.lblColor.Click += new System.EventHandler(this.lblColor_Click);
			// 
			// cboController
			// 
			this.cboController.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboController.FormattingEnabled = true;
			this.cboController.Location = new System.Drawing.Point(82, 129);
			this.cboController.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboController.Name = "cboController";
			this.cboController.Size = new System.Drawing.Size(233, 23);
			this.cboController.TabIndex = 10;
			this.tipTool.SetToolTip(this.cboController, "Select the controller this channel is connected to.");
			this.cboController.SelectedIndexChanged += new System.EventHandler(this.cboController_SelectedIndexChanged);
			this.cboController.Validating += new System.ComponentModel.CancelEventHandler(this.cboController_Validating);
			// 
			// lblController
			// 
			this.lblController.AutoSize = true;
			this.lblController.Location = new System.Drawing.Point(12, 133);
			this.lblController.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblController.Name = "lblController";
			this.lblController.Size = new System.Drawing.Size(63, 15);
			this.lblController.TabIndex = 9;
			this.lblController.Text = "Controller:";
			// 
			// numOutput
			// 
			this.numOutput.Location = new System.Drawing.Point(380, 130);
			this.numOutput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numOutput.Maximum = new decimal(new int[] {
						32,
						0,
						0,
						0});
			this.numOutput.Minimum = new decimal(new int[] {
						1,
						0,
						0,
						0});
			this.numOutput.Name = "numOutput";
			this.numOutput.Size = new System.Drawing.Size(51, 23);
			this.numOutput.TabIndex = 12;
			this.tipTool.SetToolTip(this.numOutput, "Select the output # on this controller that this channel is connected to.");
			this.numOutput.Value = new decimal(new int[] {
						1,
						0,
						0,
						0});
			this.numOutput.ValueChanged += new System.EventHandler(this.numOutput_ValueChanged);
			this.numOutput.Validating += new System.ComponentModel.CancelEventHandler(this.numOutput_Validating);
			// 
			// lblOutput
			// 
			this.lblOutput.AutoSize = true;
			this.lblOutput.Location = new System.Drawing.Point(328, 133);
			this.lblOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblOutput.Name = "lblOutput";
			this.lblOutput.Size = new System.Drawing.Size(48, 15);
			this.lblOutput.TabIndex = 11;
			this.lblOutput.Text = "Output:";
			// 
			// lblUniverse
			// 
			this.lblUniverse.AutoSize = true;
			this.lblUniverse.Location = new System.Drawing.Point(88, 175);
			this.lblUniverse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblUniverse.Name = "lblUniverse";
			this.lblUniverse.Size = new System.Drawing.Size(64, 15);
			this.lblUniverse.TabIndex = 14;
			this.lblUniverse.Text = "Universe: 1";
			this.tipTool.SetToolTip(this.lblUniverse, "The DMX Universe this controller and this channel are connected to.");
			// 
			// lblDMXAddress
			// 
			this.lblDMXAddress.AutoSize = true;
			this.lblDMXAddress.Location = new System.Drawing.Point(93, 194);
			this.lblDMXAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDMXAddress.Name = "lblDMXAddress";
			this.lblDMXAddress.Size = new System.Drawing.Size(90, 15);
			this.lblDMXAddress.TabIndex = 15;
			this.lblDMXAddress.Text = "DMX Address: 1";
			this.tipTool.SetToolTip(this.lblDMXAddress, "The DMX address of this channel in this DMX Universe.");
			// 
			// lblxLighsAddress
			// 
			this.lblxLighsAddress.AutoSize = true;
			this.lblxLighsAddress.Location = new System.Drawing.Point(93, 212);
			this.lblxLighsAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblxLighsAddress.Name = "lblxLighsAddress";
			this.lblxLighsAddress.Size = new System.Drawing.Size(102, 15);
			this.lblxLighsAddress.TabIndex = 16;
			this.lblxLighsAddress.Text = "xLights Address: 1";
			this.tipTool.SetToolTip(this.lblxLighsAddress, "The xLights address of this channel.");
			// 
			// chkActive
			// 
			this.chkActive.AutoSize = true;
			this.chkActive.Checked = true;
			this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkActive.Location = new System.Drawing.Point(366, 95);
			this.chkActive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.chkActive.Name = "chkActive";
			this.chkActive.Size = new System.Drawing.Size(59, 19);
			this.chkActive.TabIndex = 8;
			this.chkActive.Text = "Active";
			this.tipTool.SetToolTip(this.chkActive, "Is this channel in active use?");
			this.chkActive.UseVisualStyleBackColor = true;
			this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
			this.chkActive.Validating += new System.ComponentModel.CancelEventHandler(this.chkActive_Validating);
			// 
			// lblModel
			// 
			this.lblModel.AutoSize = true;
			this.lblModel.Location = new System.Drawing.Point(82, 157);
			this.lblModel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblModel.Name = "lblModel";
			this.lblModel.Size = new System.Drawing.Size(119, 15);
			this.lblModel.TabIndex = 13;
			this.lblModel.Text = "LOR LOR1602W Gen3";
			this.tipTool.SetToolTip(this.lblModel, "The brand and model of this controller.");
			// 
			// txtComment
			// 
			this.txtComment.Location = new System.Drawing.Point(82, 299);
			this.txtComment.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(349, 111);
			this.txtComment.TabIndex = 18;
			this.tipTool.SetToolTip(this.txtComment, "Comments, notes, and other important information about this channel.");
			this.txtComment.Validating += new System.ComponentModel.CancelEventHandler(this.txtComment_Validating);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 302);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 15);
			this.label4.TabIndex = 17;
			this.label4.Text = "Comment:";
			// 
			// picMulticolor
			// 
			this.picMulticolor.BackColor = System.Drawing.Color.White;
			this.picMulticolor.Image = ((System.Drawing.Image)(resources.GetObject("picMulticolor.Image")));
			this.picMulticolor.Location = new System.Drawing.Point(38, 433);
			this.picMulticolor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picMulticolor.Name = "picMulticolor";
			this.picMulticolor.Size = new System.Drawing.Size(20, 20);
			this.picMulticolor.TabIndex = 20;
			this.picMulticolor.TabStop = false;
			this.tipTool.SetToolTip(this.picMulticolor, "White");
			this.picMulticolor.Visible = false;
			// 
			// picRGB
			// 
			this.picRGB.BackColor = System.Drawing.Color.White;
			this.picRGB.Image = ((System.Drawing.Image)(resources.GetObject("picRGB.Image")));
			this.picRGB.Location = new System.Drawing.Point(97, 434);
			this.picRGB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picRGB.Name = "picRGB";
			this.picRGB.Size = new System.Drawing.Size(20, 20);
			this.picRGB.TabIndex = 21;
			this.picRGB.TabStop = false;
			this.tipTool.SetToolTip(this.picRGB, "White");
			this.picRGB.Visible = false;
			// 
			// btnColor
			// 
			this.btnColor.BackColor = System.Drawing.Color.White;
			this.btnColor.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnColor.Location = new System.Drawing.Point(400, 53);
			this.btnColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnColor.Name = "btnColor";
			this.btnColor.Size = new System.Drawing.Size(33, 32);
			this.btnColor.TabIndex = 5;
			this.btnColor.UseVisualStyleBackColor = false;
			this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
			// 
			// frmChannel
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(448, 474);
			this.Controls.Add(this.btnColor);
			this.Controls.Add(this.picRGB);
			this.Controls.Add(this.picMulticolor);
			this.Controls.Add(this.txtComment);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblModel);
			this.Controls.Add(this.chkActive);
			this.Controls.Add(this.lblxLighsAddress);
			this.Controls.Add(this.lblDMXAddress);
			this.Controls.Add(this.lblUniverse);
			this.Controls.Add(this.lblOutput);
			this.Controls.Add(this.numOutput);
			this.Controls.Add(this.cboController);
			this.Controls.Add(this.lblController);
			this.Controls.Add(this.lblColor);
			this.Controls.Add(this.picColor2);
			this.Controls.Add(this.cboType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtLocation);
			this.Controls.Add(this.lblLocation);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MinimizeBox = false;
			this.Name = "frmChannel";
			this.ShowInTaskbar = false;
			this.Text = "Channel";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmChannel_FormClosing);
			this.Load += new System.EventHandler(this.frmChannel_Load);
			this.Shown += new System.EventHandler(this.frmChannel_Shown);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmChannel_Paint);
			((System.ComponentModel.ISupportInitialize)(this.picColor2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numOutput)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picMulticolor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picRGB)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.PictureBox picColor2;
		private System.Windows.Forms.Label lblColor;
		private System.Windows.Forms.ComboBox cboController;
		private System.Windows.Forms.Label lblController;
		private System.Windows.Forms.NumericUpDown numOutput;
		private System.Windows.Forms.Label lblOutput;
		private System.Windows.Forms.Label lblUniverse;
		private System.Windows.Forms.Label lblDMXAddress;
		private System.Windows.Forms.Label lblxLighsAddress;
		private System.Windows.Forms.CheckBox chkActive;
		private System.Windows.Forms.ToolTip tipTool;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ColorDialog clrColors;
		private System.Windows.Forms.PictureBox picMulticolor;
		private System.Windows.Forms.PictureBox picRGB;
		private System.Windows.Forms.Button btnColor;
	}
}