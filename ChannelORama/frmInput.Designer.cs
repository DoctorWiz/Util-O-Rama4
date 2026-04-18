namespace UtilORama4
{
	partial class frmInput
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInput));
			txtComment = new TextBox();
			lblComment = new Label();
			txtName = new TextBox();
			lblName = new Label();
			btnCancel = new Button();
			btnOK = new Button();
			lblInput = new Label();
			picIcon = new PictureBox();
			((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
			SuspendLayout();
			// 
			// txtComment
			// 
			txtComment.Location = new Point(165, 181);
			txtComment.Margin = new Padding(6, 7, 6, 7);
			txtComment.Multiline = true;
			txtComment.Name = "txtComment";
			txtComment.Size = new Size(645, 233);
			txtComment.TabIndex = 29;
			txtComment.KeyPress += txtComment_KeyPress;
			// 
			// lblComment
			// 
			lblComment.AutoSize = true;
			lblComment.Location = new Point(13, 184);
			lblComment.Margin = new Padding(6, 0, 6, 0);
			lblComment.Name = "lblComment";
			lblComment.Size = new Size(140, 32);
			lblComment.TabIndex = 28;
			lblComment.Text = "Comment:";
			// 
			// txtName
			// 
			txtName.Location = new Point(165, 122);
			txtName.Margin = new Padding(6, 7, 6, 7);
			txtName.Name = "txtName";
			txtName.Size = new Size(645, 39);
			txtName.TabIndex = 27;
			txtName.KeyPress += txtName_KeyPress;
			// 
			// lblName
			// 
			lblName.AutoSize = true;
			lblName.Location = new Point(13, 125);
			lblName.Margin = new Padding(6, 0, 6, 0);
			lblName.Name = "lblName";
			lblName.Size = new Size(83, 32);
			lblName.TabIndex = 26;
			lblName.Text = "Name:";
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(474, 438);
			btnCancel.Margin = new Padding(6, 7, 6, 7);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(162, 57);
			btnCancel.TabIndex = 25;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// btnOK
			// 
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Location = new Point(648, 438);
			btnOK.Margin = new Padding(6, 7, 6, 7);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(162, 57);
			btnOK.TabIndex = 24;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			btnOK.Click += btnOK_Click;
			// 
			// lblInput
			// 
			lblInput.AutoSize = true;
			lblInput.Location = new Point(15, 27);
			lblInput.Margin = new Padding(6, 0, 6, 0);
			lblInput.Name = "lblInput";
			lblInput.Size = new Size(401, 32);
			lblInput.TabIndex = 30;
			lblInput.Text = "Enter the new name and description";
			// 
			// picIcon
			// 
			picIcon.Image = (Image)resources.GetObject("picIcon.Image");
			picIcon.Location = new Point(733, 27);
			picIcon.Name = "picIcon";
			picIcon.Size = new Size(60, 60);
			picIcon.TabIndex = 31;
			picIcon.TabStop = false;
			// 
			// frmInput
			// 
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(826, 508);
			Controls.Add(picIcon);
			Controls.Add(lblInput);
			Controls.Add(txtComment);
			Controls.Add(lblComment);
			Controls.Add(txtName);
			Controls.Add(lblName);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "frmInput";
			ShowIcon = false;
			ShowInTaskbar = false;
			Text = "Input";
			FormClosing += frmInput_FormClosing;
			((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		public TextBox txtComment;
		private Label lblComment;
		public TextBox txtName;
		private Label lblName;
		private Button btnCancel;
		private Button btnOK;
		public Label lblInput;
		private PictureBox picIcon;
	}
}