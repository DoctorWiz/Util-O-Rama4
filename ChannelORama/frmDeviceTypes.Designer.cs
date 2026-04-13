namespace UtilORama4
{
	partial class frmDeviceTypes
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeviceTypes));
			btnCancel = new Button();
			btnOK = new Button();
			lstDevices = new ListBox();
			btnUp = new Button();
			btnDown = new Button();
			SuspendLayout();
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(218, 723);
			btnCancel.Margin = new Padding(7, 6, 7, 6);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(163, 58);
			btnCancel.TabIndex = 22;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Enabled = false;
			btnOK.Location = new Point(395, 723);
			btnOK.Margin = new Padding(7, 6, 7, 6);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(163, 58);
			btnOK.TabIndex = 21;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			btnOK.Click += btnOK_Click;
			// 
			// lstDevices
			// 
			lstDevices.FormattingEnabled = true;
			lstDevices.Location = new Point(84, 24);
			lstDevices.Name = "lstDevices";
			lstDevices.Size = new Size(474, 676);
			lstDevices.TabIndex = 23;
			// 
			// btnUp
			// 
			btnUp.Enabled = false;
			btnUp.Image = (Image)resources.GetObject("btnUp.Image");
			btnUp.Location = new Point(16, 149);
			btnUp.Margin = new Padding(7, 6, 7, 6);
			btnUp.Name = "btnUp";
			btnUp.Size = new Size(58, 58);
			btnUp.TabIndex = 24;
			btnUp.UseVisualStyleBackColor = true;
			// 
			// btnDown
			// 
			btnDown.Enabled = false;
			btnDown.Image = (Image)resources.GetObject("btnDown.Image");
			btnDown.Location = new Point(16, 219);
			btnDown.Margin = new Padding(7, 6, 7, 6);
			btnDown.Name = "btnDown";
			btnDown.Size = new Size(58, 58);
			btnDown.TabIndex = 25;
			btnDown.UseVisualStyleBackColor = true;
			// 
			// frmDeviceTypes
			// 
			AcceptButton = btnOK;
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(582, 796);
			Controls.Add(btnDown);
			Controls.Add(btnUp);
			Controls.Add(lstDevices);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "frmDeviceTypes";
			Text = "Device Types";
			ResumeLayout(false);
		}

		#endregion

		private Button btnCancel;
		private Button btnOK;
		private ListBox lstDevices;
		private Button btnUp;
		private Button btnDown;
	}
}