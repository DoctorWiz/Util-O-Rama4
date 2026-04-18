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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeviceTypes));
			btnCancel = new Button();
			btnOK = new Button();
			lstDevices = new ListBox();
			btnUp = new Button();
			btnDown = new Button();
			btnEdit = new Button();
			btnAdd = new Button();
			btnDelete = new Button();
			tipTool = new ToolTip(components);
			lblUsedBy = new Label();
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
			btnCancel.Click += btnCancel_Click;
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
			lstDevices.SelectedIndexChanged += lstDevices_SelectedIndexChanged;
			lstDevices.MouseMove += lstDevices_MouseMove;
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
			tipTool.SetToolTip(btnUp, "Move this device up");
			btnUp.UseVisualStyleBackColor = true;
			btnUp.Click += btnUp_Click;
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
			tipTool.SetToolTip(btnDown, "Move this device down");
			btnDown.UseVisualStyleBackColor = true;
			btnDown.Click += btnDown_Click;
			// 
			// btnEdit
			// 
			btnEdit.Enabled = false;
			btnEdit.Image = (Image)resources.GetObject("btnEdit.Image");
			btnEdit.Location = new Point(16, 289);
			btnEdit.Margin = new Padding(7, 6, 7, 6);
			btnEdit.Name = "btnEdit";
			btnEdit.Size = new Size(58, 58);
			btnEdit.TabIndex = 26;
			tipTool.SetToolTip(btnEdit, "Edit this device's name");
			btnEdit.UseVisualStyleBackColor = true;
			btnEdit.Click += btnEdit_Click;
			// 
			// btnAdd
			// 
			btnAdd.Enabled = false;
			btnAdd.Image = (Image)resources.GetObject("btnAdd.Image");
			btnAdd.Location = new Point(16, 79);
			btnAdd.Margin = new Padding(7, 6, 7, 6);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(58, 58);
			btnAdd.TabIndex = 27;
			tipTool.SetToolTip(btnAdd, "Add a new device type");
			btnAdd.UseVisualStyleBackColor = true;
			btnAdd.Click += btnAdd_Click;
			// 
			// btnDelete
			// 
			btnDelete.Enabled = false;
			btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
			btnDelete.Location = new Point(16, 359);
			btnDelete.Margin = new Padding(7, 6, 7, 6);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(58, 58);
			btnDelete.TabIndex = 28;
			tipTool.SetToolTip(btnDelete, "Delete this device");
			btnDelete.UseVisualStyleBackColor = true;
			btnDelete.Click += btnDelete_Click;
			// 
			// lblUsedBy
			// 
			lblUsedBy.ForeColor = SystemColors.HotTrack;
			lblUsedBy.Location = new Point(15, 717);
			lblUsedBy.Margin = new Padding(6, 0, 6, 0);
			lblUsedBy.Name = "lblUsedBy";
			lblUsedBy.Size = new Size(150, 64);
			lblUsedBy.TabIndex = 123;
			lblUsedBy.Text = "Used by 3 Channels";
			// 
			// frmDeviceTypes
			// 
			AcceptButton = btnOK;
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(582, 796);
			Controls.Add(lblUsedBy);
			Controls.Add(btnDelete);
			Controls.Add(btnAdd);
			Controls.Add(btnEdit);
			Controls.Add(btnDown);
			Controls.Add(btnUp);
			Controls.Add(lstDevices);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MaximumSize = new Size(1000, 1500);
			MinimizeBox = false;
			MinimumSize = new Size(600, 800);
			Name = "frmDeviceTypes";
			Text = "Device Types";
			FormClosing += frmDeviceTypes_FormClosing;
			Load += frmDeviceTypes_Load;
			ResumeLayout(false);
		}

		#endregion

		private Button btnCancel;
		private Button btnOK;
		private ListBox lstDevices;
		private Button btnUp;
		private Button btnDown;
		private Button btnEdit;
		private Button btnAdd;
		private Button btnDelete;
		private ToolTip tipTool;
		private Label lblUsedBy;
	}
}