
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUniverse));
			btnOK = new Button();
			btnCancel = new Button();
			lblName = new Label();
			txtName = new TextBox();
			txtComment = new TextBox();
			lblComment = new Label();
			chkActive = new CheckBox();
			lblNumber = new Label();
			numNumber = new NumericUpDown();
			lblSize = new Label();
			lblConnection = new Label();
			txtLocation = new TextBox();
			lblLocation = new Label();
			txtConnection = new TextBox();
			lblxStart = new Label();
			numxStart = new NumericUpDown();
			numSize = new NumericUpDown();
			label1 = new Label();
			lblControllers = new Label();
			tipTool = new ToolTip(components);
			lblDirty = new Label();
			label2 = new Label();
			((System.ComponentModel.ISupportInitialize)numNumber).BeginInit();
			((System.ComponentModel.ISupportInitialize)numxStart).BeginInit();
			((System.ComponentModel.ISupportInitialize)numSize).BeginInit();
			SuspendLayout();
			// 
			// btnOK
			// 
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Location = new Point(464, 926);
			btnOK.Margin = new Padding(6, 7, 6, 7);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(162, 57);
			btnOK.TabIndex = 0;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			btnOK.Click += btnOK_Click;
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(639, 926);
			btnCancel.Margin = new Padding(6, 7, 6, 7);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(162, 57);
			btnCancel.TabIndex = 1;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// lblName
			// 
			lblName.AutoSize = true;
			lblName.Location = new Point(56, 47);
			lblName.Margin = new Padding(6, 0, 6, 0);
			lblName.Name = "lblName";
			lblName.Size = new Size(83, 32);
			lblName.TabIndex = 2;
			lblName.Text = "Name:";
			// 
			// txtName
			// 
			txtName.Location = new Point(152, 39);
			txtName.Margin = new Padding(6, 7, 6, 7);
			txtName.Name = "txtName";
			txtName.Size = new Size(645, 39);
			txtName.TabIndex = 3;
			txtName.TextChanged += txtName_TextChanged;
			txtName.Enter += txtName_Enter;
			txtName.KeyDown += txtName_KeyDown;
			txtName.Leave += txtName_Leave;
			txtName.Validating += txtName_Validating;
			// 
			// txtComment
			// 
			txtComment.Location = new Point(152, 638);
			txtComment.Margin = new Padding(6, 7, 6, 7);
			txtComment.Multiline = true;
			txtComment.Name = "txtComment";
			txtComment.Size = new Size(645, 233);
			txtComment.TabIndex = 18;
			txtComment.Enter += txtComment_Enter;
			txtComment.KeyDown += txtComment_KeyDown;
			txtComment.Leave += txtComment_Leave;
			txtComment.Validating += txtComment_Validating;
			// 
			// lblComment
			// 
			lblComment.AutoSize = true;
			lblComment.Location = new Point(28, 645);
			lblComment.Margin = new Padding(6, 0, 6, 0);
			lblComment.Name = "lblComment";
			lblComment.Size = new Size(125, 32);
			lblComment.TabIndex = 17;
			lblComment.Text = "Comment:";
			// 
			// chkActive
			// 
			chkActive.AutoSize = true;
			chkActive.Checked = true;
			chkActive.CheckState = CheckState.Checked;
			chkActive.Location = new Point(680, 202);
			chkActive.Margin = new Padding(6, 7, 6, 7);
			chkActive.Name = "chkActive";
			chkActive.Size = new Size(111, 36);
			chkActive.TabIndex = 10;
			chkActive.Text = "Active";
			chkActive.UseVisualStyleBackColor = true;
			chkActive.CheckedChanged += chkActive_CheckedChanged;
			chkActive.Enter += chkActive_Enter;
			chkActive.KeyDown += chkActive_KeyDown;
			chkActive.Leave += chkActive_Leave;
			// 
			// lblNumber
			// 
			lblNumber.AutoSize = true;
			lblNumber.Location = new Point(602, 126);
			lblNumber.Margin = new Padding(6, 0, 6, 0);
			lblNumber.Name = "lblNumber";
			lblNumber.Size = new Size(107, 32);
			lblNumber.TabIndex = 6;
			lblNumber.Text = "Number:";
			// 
			// numNumber
			// 
			numNumber.Location = new Point(706, 121);
			numNumber.Margin = new Padding(6, 7, 6, 7);
			numNumber.Maximum = new decimal(new int[] { 2022, 0, 0, 0 });
			numNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numNumber.Name = "numNumber";
			numNumber.Size = new Size(95, 39);
			numNumber.TabIndex = 7;
			numNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
			numNumber.ValueChanged += numNumber_ValueChanged;
			numNumber.Enter += numNumber_Enter;
			numNumber.KeyDown += numNumber_KeyDown;
			numNumber.Leave += numNumber_Leave;
			numNumber.Validating += numNumber_Validating;
			// 
			// lblSize
			// 
			lblSize.AutoSize = true;
			lblSize.Location = new Point(74, 283);
			lblSize.Margin = new Padding(6, 0, 6, 0);
			lblSize.Name = "lblSize";
			lblSize.Size = new Size(62, 32);
			lblSize.TabIndex = 11;
			lblSize.Text = "Size:";
			// 
			// lblConnection
			// 
			lblConnection.AutoSize = true;
			lblConnection.Location = new Point(0, 204);
			lblConnection.Margin = new Padding(6, 0, 6, 0);
			lblConnection.Name = "lblConnection";
			lblConnection.Size = new Size(142, 32);
			lblConnection.TabIndex = 8;
			lblConnection.Text = "Connection:";
			// 
			// txtLocation
			// 
			txtLocation.Location = new Point(152, 118);
			txtLocation.Margin = new Padding(6, 7, 6, 7);
			txtLocation.MaxLength = 40;
			txtLocation.Name = "txtLocation";
			txtLocation.Size = new Size(429, 39);
			txtLocation.TabIndex = 5;
			txtLocation.Enter += txtLocation_Enter;
			txtLocation.KeyDown += txtLocation_KeyDown;
			txtLocation.Leave += txtLocation_Leave;
			txtLocation.Validating += txtLocation_Validating;
			// 
			// lblLocation
			// 
			lblLocation.AutoSize = true;
			lblLocation.Location = new Point(28, 126);
			lblLocation.Margin = new Padding(6, 0, 6, 0);
			lblLocation.Name = "lblLocation";
			lblLocation.Size = new Size(109, 32);
			lblLocation.TabIndex = 4;
			lblLocation.Text = "Location:";
			// 
			// txtConnection
			// 
			txtConnection.Location = new Point(152, 197);
			txtConnection.Margin = new Padding(6, 7, 6, 7);
			txtConnection.MaxLength = 40;
			txtConnection.Name = "txtConnection";
			txtConnection.Size = new Size(429, 39);
			txtConnection.TabIndex = 9;
			txtConnection.Enter += txtConnection_Enter;
			txtConnection.KeyDown += txtConnection_KeyDown;
			txtConnection.Leave += txtConnection_Leave;
			txtConnection.Validating += txtConnection_Validating;
			// 
			// lblxStart
			// 
			lblxStart.AutoSize = true;
			lblxStart.Location = new Point(316, 283);
			lblxStart.Margin = new Padding(6, 0, 6, 0);
			lblxStart.Name = "lblxStart";
			lblxStart.Size = new Size(148, 32);
			lblxStart.TabIndex = 13;
			lblxStart.Text = "xLights Start:";
			// 
			// numxStart
			// 
			numxStart.Increment = new decimal(new int[] { 16, 0, 0, 0 });
			numxStart.Location = new Point(464, 278);
			numxStart.Margin = new Padding(6, 7, 6, 7);
			numxStart.Maximum = new decimal(new int[] { 32769, 0, 0, 0 });
			numxStart.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numxStart.Name = "numxStart";
			numxStart.Size = new Size(121, 39);
			numxStart.TabIndex = 14;
			numxStart.Value = new decimal(new int[] { 1, 0, 0, 0 });
			numxStart.ValueChanged += numxStart_ValueChanged;
			numxStart.Enter += numxStart_Enter;
			numxStart.KeyDown += numxStart_KeyDown;
			numxStart.Leave += numxStart_Leave;
			// 
			// numSize
			// 
			numSize.Increment = new decimal(new int[] { 16, 0, 0, 0 });
			numSize.Location = new Point(152, 278);
			numSize.Margin = new Padding(6, 7, 6, 7);
			numSize.Maximum = new decimal(new int[] { 512, 0, 0, 0 });
			numSize.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
			numSize.Name = "numSize";
			numSize.Size = new Size(95, 39);
			numSize.TabIndex = 12;
			numSize.Value = new decimal(new int[] { 128, 0, 0, 0 });
			numSize.ValueChanged += numSize_ValueChanged;
			numSize.Enter += numSize_Enter;
			numSize.KeyDown += numSize_KeyDown;
			numSize.Leave += numSize_Leave;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(11, 362);
			label1.Margin = new Padding(6, 0, 6, 0);
			label1.Name = "label1";
			label1.Size = new Size(135, 32);
			label1.TabIndex = 15;
			label1.Text = "Controllers:";
			// 
			// lblControllers
			// 
			lblControllers.Location = new Point(152, 362);
			lblControllers.Margin = new Padding(6, 0, 6, 0);
			lblControllers.Name = "lblControllers";
			lblControllers.Size = new Size(650, 249);
			lblControllers.TabIndex = 16;
			lblControllers.Text = "List\r\nOf\r\nControllers\r\nGoes\r\nHere\r\n";
			// 
			// lblDirty
			// 
			lblDirty.AutoSize = true;
			lblDirty.Font = new Font("Segoe UI", 6.75F, FontStyle.Italic);
			lblDirty.ForeColor = SystemColors.GrayText;
			lblDirty.Location = new Point(11, 978);
			lblDirty.Margin = new Padding(7, 0, 7, 0);
			lblDirty.Name = "lblDirty";
			lblDirty.Size = new Size(51, 25);
			lblDirty.TabIndex = 23;
			lblDirty.Text = "Dirty";
			lblDirty.Visible = false;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 6.75F, FontStyle.Italic);
			label2.ForeColor = SystemColors.GrayText;
			label2.Location = new Point(173, 76);
			label2.Margin = new Padding(7, 0, 7, 0);
			label2.Name = "label2";
			label2.Size = new Size(378, 25);
			label2.TabIndex = 24;
			label2.Text = "(Or what group of controllers use this universe)";
			// 
			// frmUniverse
			// 
			AcceptButton = btnOK;
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(832, 1012);
			Controls.Add(lblDirty);
			Controls.Add(lblControllers);
			Controls.Add(label1);
			Controls.Add(numSize);
			Controls.Add(lblxStart);
			Controls.Add(numxStart);
			Controls.Add(txtConnection);
			Controls.Add(txtComment);
			Controls.Add(lblComment);
			Controls.Add(chkActive);
			Controls.Add(numNumber);
			Controls.Add(lblSize);
			Controls.Add(lblConnection);
			Controls.Add(txtLocation);
			Controls.Add(lblLocation);
			Controls.Add(txtName);
			Controls.Add(lblName);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Controls.Add(label2);
			Controls.Add(lblNumber);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(6, 7, 6, 7);
			MaximizeBox = false;
			Name = "frmUniverse";
			ShowInTaskbar = false;
			Text = "Universe";
			FormClosing += frmUniverse_FormClosing;
			Load += frmUniverse_Load;
			Shown += frmUniverse_Shown;
			ResizeBegin += frmUniverse_ResizeBegin;
			ResizeEnd += frmUniverse_ResizeEnd;
			((System.ComponentModel.ISupportInitialize)numNumber).EndInit();
			((System.ComponentModel.ISupportInitialize)numxStart).EndInit();
			((System.ComponentModel.ISupportInitialize)numSize).EndInit();
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Label lblComment;
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
		private Label lblDirty;
		private Label label2;
	}
}