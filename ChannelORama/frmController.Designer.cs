
namespace UtilORama4
{
	partial class frmController
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
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.chkActive = new System.Windows.Forms.CheckBox();
			this.lblxAddresses = new System.Windows.Forms.Label();
			this.lblLastDMX = new System.Windows.Forms.Label();
			this.lblStart = new System.Windows.Forms.Label();
			this.numStart = new System.Windows.Forms.NumericUpDown();
			this.cboBrand = new System.Windows.Forms.ComboBox();
			this.lblBrand = new System.Windows.Forms.Label();
			this.cboUniverse = new System.Windows.Forms.ComboBox();
			this.lblUniverse = new System.Windows.Forms.Label();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.lblLocation = new System.Windows.Forms.Label();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.lblModel = new System.Windows.Forms.Label();
			this.txtLetter = new System.Windows.Forms.TextBox();
			this.lblLetter = new System.Windows.Forms.Label();
			this.lblCount = new System.Windows.Forms.Label();
			this.numCount = new System.Windows.Forms.NumericUpDown();
			this.btnChannels = new System.Windows.Forms.Button();
			this.tipTool = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(216, 376);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(297, 376);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(26, 19);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(38, 13);
			this.lblName.TabIndex = 2;
			this.lblName.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(70, 16);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(300, 20);
			this.txtName.TabIndex = 3;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
			// 
			// txtComment
			// 
			this.txtComment.Location = new System.Drawing.Point(70, 259);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(300, 97);
			this.txtComment.TabIndex = 23;
			this.txtComment.Validating += new System.ComponentModel.CancelEventHandler(this.txtComment_Validating);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 262);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 13);
			this.label4.TabIndex = 22;
			this.label4.Text = "Comment:";
			// 
			// chkActive
			// 
			this.chkActive.AutoSize = true;
			this.chkActive.Checked = true;
			this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkActive.Location = new System.Drawing.Point(314, 82);
			this.chkActive.Name = "chkActive";
			this.chkActive.Size = new System.Drawing.Size(56, 17);
			this.chkActive.TabIndex = 10;
			this.chkActive.Text = "Active";
			this.chkActive.UseVisualStyleBackColor = true;
			this.chkActive.Validating += new System.ComponentModel.CancelEventHandler(this.chkActive_Validating);
			// 
			// lblxAddresses
			// 
			this.lblxAddresses.AutoSize = true;
			this.lblxAddresses.Location = new System.Drawing.Point(67, 202);
			this.lblxAddresses.Name = "lblxAddresses";
			this.lblxAddresses.Size = new System.Drawing.Size(114, 13);
			this.lblxAddresses.TabIndex = 20;
			this.lblxAddresses.Text = "xLights Channels: 1-16";
			// 
			// lblLastDMX
			// 
			this.lblLastDMX.AutoSize = true;
			this.lblLastDMX.Location = new System.Drawing.Point(67, 179);
			this.lblLastDMX.Name = "lblLastDMX";
			this.lblLastDMX.Size = new System.Drawing.Size(114, 13);
			this.lblLastDMX.TabIndex = 19;
			this.lblLastDMX.Text = "Last DMX Channel: 16";
			// 
			// lblStart
			// 
			this.lblStart.AutoSize = true;
			this.lblStart.Location = new System.Drawing.Point(281, 115);
			this.lblStart.Name = "lblStart";
			this.lblStart.Size = new System.Drawing.Size(32, 13);
			this.lblStart.TabIndex = 13;
			this.lblStart.Text = "Start:";
			// 
			// numStart
			// 
			this.numStart.Location = new System.Drawing.Point(326, 113);
			this.numStart.Maximum = new decimal(new int[] {
            510,
            0,
            0,
            0});
			this.numStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numStart.Name = "numStart";
			this.numStart.Size = new System.Drawing.Size(44, 20);
			this.numStart.TabIndex = 14;
			this.numStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numStart.ValueChanged += new System.EventHandler(this.numStart_ValueChanged);
			this.numStart.Validating += new System.ComponentModel.CancelEventHandler(this.numStart_Validating);
			// 
			// cboBrand
			// 
			this.cboBrand.AutoCompleteCustomSource.AddRange(new string[] {
            "LOR",
            "Renard",
            "Chinese"});
			this.cboBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboBrand.FormattingEnabled = true;
			this.cboBrand.Items.AddRange(new object[] {
            "LOR",
            "Renard",
            "Chinese",
            "Chauvet",
            "American DJ"});
			this.cboBrand.Location = new System.Drawing.Point(70, 112);
			this.cboBrand.Name = "cboBrand";
			this.cboBrand.Size = new System.Drawing.Size(200, 21);
			this.cboBrand.TabIndex = 12;
			this.cboBrand.SelectedIndexChanged += new System.EventHandler(this.cboBrand_SelectedIndexChanged);
			// 
			// lblBrand
			// 
			this.lblBrand.AutoSize = true;
			this.lblBrand.Location = new System.Drawing.Point(26, 115);
			this.lblBrand.Name = "lblBrand";
			this.lblBrand.Size = new System.Drawing.Size(38, 13);
			this.lblBrand.TabIndex = 11;
			this.lblBrand.Text = "Brand:";
			// 
			// cboUniverse
			// 
			this.cboUniverse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboUniverse.FormattingEnabled = true;
			this.cboUniverse.Location = new System.Drawing.Point(70, 80);
			this.cboUniverse.Name = "cboUniverse";
			this.cboUniverse.Size = new System.Drawing.Size(200, 21);
			this.cboUniverse.TabIndex = 9;
			this.cboUniverse.SelectedIndexChanged += new System.EventHandler(this.cboUniverse_SelectedIndexChanged);
			this.cboUniverse.Validating += new System.ComponentModel.CancelEventHandler(this.cboUniverse_Validating);
			// 
			// lblUniverse
			// 
			this.lblUniverse.AutoSize = true;
			this.lblUniverse.Location = new System.Drawing.Point(12, 83);
			this.lblUniverse.Name = "lblUniverse";
			this.lblUniverse.Size = new System.Drawing.Size(52, 13);
			this.lblUniverse.TabIndex = 8;
			this.lblUniverse.Text = "Universe:";
			// 
			// txtLocation
			// 
			this.txtLocation.Location = new System.Drawing.Point(70, 48);
			this.txtLocation.MaxLength = 40;
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(200, 20);
			this.txtLocation.TabIndex = 5;
			this.txtLocation.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocation_Validating);
			// 
			// lblLocation
			// 
			this.lblLocation.AutoSize = true;
			this.lblLocation.Location = new System.Drawing.Point(13, 51);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(51, 13);
			this.lblLocation.TabIndex = 4;
			this.lblLocation.Text = "Location:";
			// 
			// txtModel
			// 
			this.txtModel.Location = new System.Drawing.Point(70, 144);
			this.txtModel.MaxLength = 40;
			this.txtModel.Name = "txtModel";
			this.txtModel.Size = new System.Drawing.Size(200, 20);
			this.txtModel.TabIndex = 16;
			this.txtModel.Validating += new System.ComponentModel.CancelEventHandler(this.txtModel_Validating);
			// 
			// lblModel
			// 
			this.lblModel.AutoSize = true;
			this.lblModel.Location = new System.Drawing.Point(25, 147);
			this.lblModel.Name = "lblModel";
			this.lblModel.Size = new System.Drawing.Size(39, 13);
			this.lblModel.TabIndex = 15;
			this.lblModel.Text = "Model:";
			// 
			// txtLetter
			// 
			this.txtLetter.Location = new System.Drawing.Point(355, 48);
			this.txtLetter.MaxLength = 1;
			this.txtLetter.Name = "txtLetter";
			this.txtLetter.Size = new System.Drawing.Size(15, 20);
			this.txtLetter.TabIndex = 7;
			this.txtLetter.Text = "A";
			this.txtLetter.TextChanged += new System.EventHandler(this.txtLetter_TextChanged);
			this.txtLetter.Validating += new System.ComponentModel.CancelEventHandler(this.txtLetter_Validating);
			// 
			// lblLetter
			// 
			this.lblLetter.AutoSize = true;
			this.lblLetter.Location = new System.Drawing.Point(312, 51);
			this.lblLetter.Name = "lblLetter";
			this.lblLetter.Size = new System.Drawing.Size(37, 13);
			this.lblLetter.TabIndex = 6;
			this.lblLetter.Text = "Letter:";
			// 
			// lblCount
			// 
			this.lblCount.AutoSize = true;
			this.lblCount.Location = new System.Drawing.Point(281, 147);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(38, 13);
			this.lblCount.TabIndex = 17;
			this.lblCount.Text = "Count:";
			// 
			// numCount
			// 
			this.numCount.Location = new System.Drawing.Point(326, 145);
			this.numCount.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numCount.Name = "numCount";
			this.numCount.Size = new System.Drawing.Size(44, 20);
			this.numCount.TabIndex = 18;
			this.numCount.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.numCount.ValueChanged += new System.EventHandler(this.numCount_ValueChanged);
			this.numCount.Validating += new System.ComponentModel.CancelEventHandler(this.numCount_Validating);
			// 
			// btnChannels
			// 
			this.btnChannels.Location = new System.Drawing.Point(297, 179);
			this.btnChannels.Name = "btnChannels";
			this.btnChannels.Size = new System.Drawing.Size(75, 23);
			this.btnChannels.TabIndex = 21;
			this.btnChannels.Text = "Channels...";
			this.btnChannels.UseVisualStyleBackColor = true;
			this.btnChannels.Click += new System.EventHandler(this.btnChannels_Click);
			// 
			// frmController
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(384, 411);
			this.Controls.Add(this.btnChannels);
			this.Controls.Add(this.lblCount);
			this.Controls.Add(this.numCount);
			this.Controls.Add(this.txtLetter);
			this.Controls.Add(this.lblLetter);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.lblModel);
			this.Controls.Add(this.txtComment);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.chkActive);
			this.Controls.Add(this.lblxAddresses);
			this.Controls.Add(this.lblLastDMX);
			this.Controls.Add(this.lblStart);
			this.Controls.Add(this.numStart);
			this.Controls.Add(this.cboBrand);
			this.Controls.Add(this.lblBrand);
			this.Controls.Add(this.cboUniverse);
			this.Controls.Add(this.lblUniverse);
			this.Controls.Add(this.txtLocation);
			this.Controls.Add(this.lblLocation);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "frmController";
			this.ShowInTaskbar = false;
			this.Text = "Controller";
			this.Shown += new System.EventHandler(this.frmController_Shown);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmController_Paint);
			((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkActive;
		private System.Windows.Forms.Label lblxAddresses;
		private System.Windows.Forms.Label lblLastDMX;
		private System.Windows.Forms.Label lblStart;
		private System.Windows.Forms.NumericUpDown numStart;
		private System.Windows.Forms.ComboBox cboBrand;
		private System.Windows.Forms.Label lblBrand;
		private System.Windows.Forms.ComboBox cboUniverse;
		private System.Windows.Forms.Label lblUniverse;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.TextBox txtLetter;
		private System.Windows.Forms.Label lblLetter;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.NumericUpDown numCount;
		private System.Windows.Forms.Button btnChannels;
		private System.Windows.Forms.ToolTip tipTool;
	}
}