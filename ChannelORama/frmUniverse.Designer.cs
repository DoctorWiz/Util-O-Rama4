
namespace UtilORama4
{
	partial class frmUniverse
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
			this.lblNumber = new System.Windows.Forms.Label();
			this.numNumber = new System.Windows.Forms.NumericUpDown();
			this.lblSize = new System.Windows.Forms.Label();
			this.lblConnection = new System.Windows.Forms.Label();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.lblLocation = new System.Windows.Forms.Label();
			this.txtConnection = new System.Windows.Forms.TextBox();
			this.lblxStart = new System.Windows.Forms.Label();
			this.numxStart = new System.Windows.Forms.NumericUpDown();
			this.numSize = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.lblControllers = new System.Windows.Forms.Label();
			this.tipTool = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.numNumber)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numxStart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSize)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(214, 376);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(295, 376);
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
			this.txtComment.TabIndex = 18;
			this.txtComment.Validating += new System.ComponentModel.CancelEventHandler(this.txtComment_Validating);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 262);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 13);
			this.label4.TabIndex = 17;
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
			// 
			// lblNumber
			// 
			this.lblNumber.AutoSize = true;
			this.lblNumber.Location = new System.Drawing.Point(278, 51);
			this.lblNumber.Name = "lblNumber";
			this.lblNumber.Size = new System.Drawing.Size(47, 13);
			this.lblNumber.TabIndex = 6;
			this.lblNumber.Text = "Number:";
			// 
			// numNumber
			// 
			this.numNumber.Location = new System.Drawing.Point(326, 49);
			this.numNumber.Maximum = new decimal(new int[] {
            2022,
            0,
            0,
            0});
			this.numNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numNumber.Name = "numNumber";
			this.numNumber.Size = new System.Drawing.Size(44, 20);
			this.numNumber.TabIndex = 7;
			this.numNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numNumber.ValueChanged += new System.EventHandler(this.numNumber_ValueChanged);
			this.numNumber.Validating += new System.ComponentModel.CancelEventHandler(this.numNumber_Validating);
			// 
			// lblSize
			// 
			this.lblSize.AutoSize = true;
			this.lblSize.Location = new System.Drawing.Point(34, 115);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(30, 13);
			this.lblSize.TabIndex = 11;
			this.lblSize.Text = "Size:";
			// 
			// lblConnection
			// 
			this.lblConnection.AutoSize = true;
			this.lblConnection.Location = new System.Drawing.Point(0, 83);
			this.lblConnection.Name = "lblConnection";
			this.lblConnection.Size = new System.Drawing.Size(64, 13);
			this.lblConnection.TabIndex = 8;
			this.lblConnection.Text = "Connection:";
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
			// txtConnection
			// 
			this.txtConnection.Location = new System.Drawing.Point(70, 80);
			this.txtConnection.MaxLength = 40;
			this.txtConnection.Name = "txtConnection";
			this.txtConnection.Size = new System.Drawing.Size(200, 20);
			this.txtConnection.TabIndex = 9;
			this.txtConnection.Validating += new System.ComponentModel.CancelEventHandler(this.txtConnection_Validating);
			// 
			// lblxStart
			// 
			this.lblxStart.AutoSize = true;
			this.lblxStart.Location = new System.Drawing.Point(146, 115);
			this.lblxStart.Name = "lblxStart";
			this.lblxStart.Size = new System.Drawing.Size(68, 13);
			this.lblxStart.TabIndex = 13;
			this.lblxStart.Text = "xLights Start:";
			// 
			// numxStart
			// 
			this.numxStart.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.numxStart.Location = new System.Drawing.Point(214, 113);
			this.numxStart.Maximum = new decimal(new int[] {
            32769,
            0,
            0,
            0});
			this.numxStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numxStart.Name = "numxStart";
			this.numxStart.Size = new System.Drawing.Size(56, 20);
			this.numxStart.TabIndex = 14;
			this.numxStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numxStart.ValueChanged += new System.EventHandler(this.numxStart_ValueChanged);
			// 
			// numSize
			// 
			this.numSize.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.numSize.Location = new System.Drawing.Point(70, 113);
			this.numSize.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
			this.numSize.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.numSize.Name = "numSize";
			this.numSize.Size = new System.Drawing.Size(44, 20);
			this.numSize.TabIndex = 12;
			this.numSize.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.numSize.ValueChanged += new System.EventHandler(this.numSize_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 147);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 15;
			this.label1.Text = "Controllers:";
			// 
			// lblControllers
			// 
			this.lblControllers.Location = new System.Drawing.Point(70, 147);
			this.lblControllers.Name = "lblControllers";
			this.lblControllers.Size = new System.Drawing.Size(300, 101);
			this.lblControllers.TabIndex = 16;
			this.lblControllers.Text = "List\r\nOf\r\nControllers\r\nGoes\r\nHere\r\n";
			// 
			// frmUniverse
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(384, 411);
			this.Controls.Add(this.lblControllers);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numSize);
			this.Controls.Add(this.lblxStart);
			this.Controls.Add(this.numxStart);
			this.Controls.Add(this.txtConnection);
			this.Controls.Add(this.txtComment);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.chkActive);
			this.Controls.Add(this.lblNumber);
			this.Controls.Add(this.numNumber);
			this.Controls.Add(this.lblSize);
			this.Controls.Add(this.lblConnection);
			this.Controls.Add(this.txtLocation);
			this.Controls.Add(this.lblLocation);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "frmUniverse";
			this.ShowInTaskbar = false;
			this.Text = "Universe";
			this.Load += new System.EventHandler(this.frmUniverse_Load);
			this.Shown += new System.EventHandler(this.frmUniverse_Shown);
			((System.ComponentModel.ISupportInitialize)(this.numNumber)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numxStart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSize)).EndInit();
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
		private System.Windows.Forms.Label lblNumber;
		private System.Windows.Forms.NumericUpDown numNumber;
		private System.Windows.Forms.Label lblSize;
		private System.Windows.Forms.Label lblConnection;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.TextBox txtConnection;
		private System.Windows.Forms.Label lblxStart;
		private System.Windows.Forms.NumericUpDown numxStart;
		private System.Windows.Forms.NumericUpDown numSize;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblControllers;
		private System.Windows.Forms.ToolTip tipTool;
	}
}