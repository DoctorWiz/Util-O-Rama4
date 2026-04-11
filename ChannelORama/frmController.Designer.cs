
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmController));
			btnOK = new Button();
			btnCancel = new Button();
			lblName = new Label();
			txtName = new TextBox();
			txtComment = new TextBox();
			lblComment = new Label();
			chkActive = new CheckBox();
			lblxAddresses = new Label();
			lblLastDMX = new Label();
			lblStart = new Label();
			numStart = new NumericUpDown();
			cboBrand = new ComboBox();
			lblBrand = new Label();
			cboUniverse = new ComboBox();
			lblUniverse = new Label();
			txtLocation = new TextBox();
			lblLocation = new Label();
			txtModel = new TextBox();
			lblModel = new Label();
			txtIdentifier = new TextBox();
			lblIdentifier = new Label();
			lblCount = new Label();
			numCount = new NumericUpDown();
			btnChannels = new Button();
			tipTool = new ToolTip(components);
			cboLORModels = new ComboBox();
			lblUnit = new Label();
			numUnit = new NumericUpDown();
			lblDirty = new Label();
			label1 = new Label();
			((System.ComponentModel.ISupportInitialize)numStart).BeginInit();
			((System.ComponentModel.ISupportInitialize)numCount).BeginInit();
			((System.ComponentModel.ISupportInitialize)numUnit).BeginInit();
			SuspendLayout();
			// 
			// btnOK
			// 
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Location = new Point(468, 926);
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
			btnCancel.Location = new Point(644, 926);
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
			txtComment.TabIndex = 23;
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
			lblComment.TabIndex = 22;
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
			chkActive.Validating += chkActive_Validating;
			// 
			// lblxAddresses
			// 
			lblxAddresses.AutoSize = true;
			lblxAddresses.Location = new Point(145, 497);
			lblxAddresses.Margin = new Padding(6, 0, 6, 0);
			lblxAddresses.Name = "lblxAddresses";
			lblxAddresses.Size = new Size(254, 32);
			lblxAddresses.TabIndex = 20;
			lblxAddresses.Text = "xLights Channels: 1-16";
			// 
			// lblLastDMX
			// 
			lblLastDMX.AutoSize = true;
			lblLastDMX.Location = new Point(145, 441);
			lblLastDMX.Margin = new Padding(6, 0, 6, 0);
			lblLastDMX.Name = "lblLastDMX";
			lblLastDMX.Size = new Size(303, 32);
			lblLastDMX.TabIndex = 19;
			lblLastDMX.Text = "Last DMX LOR4Channel: 16";
			// 
			// lblStart
			// 
			lblStart.AutoSize = true;
			lblStart.Location = new Point(609, 283);
			lblStart.Margin = new Padding(6, 0, 6, 0);
			lblStart.Name = "lblStart";
			lblStart.Size = new Size(67, 32);
			lblStart.TabIndex = 13;
			lblStart.Text = "Start:";
			// 
			// numStart
			// 
			numStart.Location = new Point(706, 278);
			numStart.Margin = new Padding(6, 7, 6, 7);
			numStart.Maximum = new decimal(new int[] { 494, 0, 0, 0 });
			numStart.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numStart.Name = "numStart";
			numStart.Size = new Size(95, 39);
			numStart.TabIndex = 14;
			numStart.Value = new decimal(new int[] { 1, 0, 0, 0 });
			numStart.ValueChanged += numStart_ValueChanged;
			numStart.Enter += numStart_Enter;
			numStart.KeyDown += numStart_KeyDown;
			numStart.Leave += numStart_Leave;
			numStart.Validating += numStart_Validating;
			// 
			// cboBrand
			// 
			cboBrand.AutoCompleteCustomSource.AddRange(new string[] { "LOR", "Renard", "Chinese" });
			cboBrand.DropDownStyle = ComboBoxStyle.DropDownList;
			cboBrand.FormattingEnabled = true;
			cboBrand.Items.AddRange(new object[] { "LOR", "Renard", "Chinese", "Chauvet", "American DJ", "Genius" });
			cboBrand.Location = new Point(152, 276);
			cboBrand.Margin = new Padding(6, 7, 6, 7);
			cboBrand.Name = "cboBrand";
			cboBrand.Size = new Size(429, 40);
			cboBrand.TabIndex = 12;
			cboBrand.SelectedIndexChanged += cboBrand_SelectedIndexChanged;
			cboBrand.Enter += cboBrand_Enter;
			cboBrand.KeyDown += cboBrand_KeyDown;
			cboBrand.Leave += cboBrand_Leave;
			// 
			// lblBrand
			// 
			lblBrand.AutoSize = true;
			lblBrand.Location = new Point(56, 283);
			lblBrand.Margin = new Padding(6, 0, 6, 0);
			lblBrand.Name = "lblBrand";
			lblBrand.Size = new Size(81, 32);
			lblBrand.TabIndex = 11;
			lblBrand.Text = "Brand:";
			// 
			// cboUniverse
			// 
			cboUniverse.DropDownStyle = ComboBoxStyle.DropDownList;
			cboUniverse.FormattingEnabled = true;
			cboUniverse.Location = new Point(152, 197);
			cboUniverse.Margin = new Padding(6, 7, 6, 7);
			cboUniverse.Name = "cboUniverse";
			cboUniverse.Size = new Size(429, 40);
			cboUniverse.TabIndex = 9;
			cboUniverse.SelectedIndexChanged += cboUniverse_SelectedIndexChanged;
			cboUniverse.Enter += cboUniverse_Enter;
			cboUniverse.KeyDown += cboUniverse_KeyDown;
			cboUniverse.Leave += cboUniverse_Leave;
			cboUniverse.Validating += cboUniverse_Validating;
			// 
			// lblUniverse
			// 
			lblUniverse.AutoSize = true;
			lblUniverse.Location = new Point(26, 204);
			lblUniverse.Margin = new Padding(6, 0, 6, 0);
			lblUniverse.Name = "lblUniverse";
			lblUniverse.Size = new Size(111, 32);
			lblUniverse.TabIndex = 8;
			lblUniverse.Text = "Universe:";
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
			// txtModel
			// 
			txtModel.Location = new Point(152, 354);
			txtModel.Margin = new Padding(6, 7, 6, 7);
			txtModel.MaxLength = 40;
			txtModel.Name = "txtModel";
			txtModel.Size = new Size(292, 39);
			txtModel.TabIndex = 16;
			txtModel.Enter += txtModel_Enter;
			txtModel.KeyDown += txtModel_KeyDown;
			txtModel.Leave += txtModel_Leave;
			txtModel.Validating += txtModel_Validating;
			// 
			// lblModel
			// 
			lblModel.AutoSize = true;
			lblModel.Location = new Point(54, 362);
			lblModel.Margin = new Padding(6, 0, 6, 0);
			lblModel.Name = "lblModel";
			lblModel.Size = new Size(88, 32);
			lblModel.TabIndex = 15;
			lblModel.Text = "Model:";
			// 
			// txtIdentifier
			// 
			txtIdentifier.Location = new Point(663, 118);
			txtIdentifier.Margin = new Padding(6, 7, 6, 7);
			txtIdentifier.MaxLength = 8;
			txtIdentifier.Name = "txtIdentifier";
			txtIdentifier.Size = new Size(138, 39);
			txtIdentifier.TabIndex = 7;
			txtIdentifier.TextAlign = HorizontalAlignment.Right;
			txtIdentifier.Enter += txtIdentifier_Enter;
			txtIdentifier.KeyDown += txtIdentifier_KeyDown;
			txtIdentifier.Leave += txtIdentifier_Leave;
			txtIdentifier.Validating += txtIdentifier_Validating;
			// 
			// lblIdentifier
			// 
			lblIdentifier.AutoSize = true;
			lblIdentifier.Location = new Point(609, 126);
			lblIdentifier.Margin = new Padding(6, 0, 6, 0);
			lblIdentifier.Name = "lblIdentifier";
			lblIdentifier.Size = new Size(42, 32);
			lblIdentifier.TabIndex = 6;
			lblIdentifier.Text = "ID:";
			// 
			// lblCount
			// 
			lblCount.AutoSize = true;
			lblCount.Location = new Point(609, 362);
			lblCount.Margin = new Padding(6, 0, 6, 0);
			lblCount.Name = "lblCount";
			lblCount.Size = new Size(84, 32);
			lblCount.TabIndex = 17;
			lblCount.Text = "Count:";
			// 
			// numCount
			// 
			numCount.Location = new Point(706, 357);
			numCount.Margin = new Padding(6, 7, 6, 7);
			numCount.Maximum = new decimal(new int[] { 48, 0, 0, 0 });
			numCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numCount.Name = "numCount";
			numCount.Size = new Size(95, 39);
			numCount.TabIndex = 18;
			numCount.Value = new decimal(new int[] { 16, 0, 0, 0 });
			numCount.ValueChanged += numCount_ValueChanged;
			numCount.Enter += numCount_Enter;
			numCount.KeyDown += numCount_KeyDown;
			numCount.Leave += numCount_Leave;
			numCount.Validating += numCount_Validating;
			// 
			// btnChannels
			// 
			btnChannels.Location = new Point(644, 441);
			btnChannels.Margin = new Padding(6, 7, 6, 7);
			btnChannels.Name = "btnChannels";
			btnChannels.Size = new Size(162, 57);
			btnChannels.TabIndex = 21;
			btnChannels.Text = "Channels...";
			btnChannels.UseVisualStyleBackColor = true;
			btnChannels.Click += btnChannels_Click;
			// 
			// cboLORModels
			// 
			cboLORModels.AutoCompleteCustomSource.AddRange(new string[] { "CTB16PC Gen 1", "CTB16PC Gen 2", "CTB16PC Gen 3", "LOR1602W Gen 1", "LOR1602W Gen 2", "LOR1602Wg3", "CMB24D Gen 3" });
			cboLORModels.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLORModels.FormattingEnabled = true;
			cboLORModels.Items.AddRange(new object[] { "CTB04PC", "CTB16PC gen 1", "CTB16PC gen 3", "CTB24D", "LOR1600 gen 1", "LOR1600Wg3", "LOR1602 gen 1", "LOR1602Wg3" });
			cboLORModels.Location = new Point(368, 560);
			cboLORModels.Margin = new Padding(6, 7, 6, 7);
			cboLORModels.Name = "cboLORModels";
			cboLORModels.Size = new Size(292, 40);
			cboLORModels.TabIndex = 24;
			cboLORModels.Visible = false;
			cboLORModels.SelectedIndexChanged += cboLORModels_SelectedIndexChanged;
			cboLORModels.Enter += cboLORModels_Enter;
			cboLORModels.KeyDown += cboLORModels_KeyDown;
			cboLORModels.Leave += cboLORModels_Leave;
			// 
			// lblUnit
			// 
			lblUnit.AutoSize = true;
			lblUnit.Location = new Point(454, 359);
			lblUnit.Margin = new Padding(6, 0, 6, 0);
			lblUnit.Name = "lblUnit";
			lblUnit.Size = new Size(63, 32);
			lblUnit.TabIndex = 25;
			lblUnit.Text = "Unit:";
			lblUnit.Visible = false;
			// 
			// numUnit
			// 
			numUnit.Location = new Point(529, 357);
			numUnit.Margin = new Padding(6, 7, 6, 7);
			numUnit.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
			numUnit.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numUnit.Name = "numUnit";
			numUnit.Size = new Size(69, 39);
			numUnit.TabIndex = 26;
			numUnit.Value = new decimal(new int[] { 1, 0, 0, 0 });
			numUnit.Visible = false;
			numUnit.ValueChanged += numUnit_ValueChanged;
			numUnit.Enter += numUnit_Enter;
			numUnit.KeyDown += numUnit_KeyDown;
			numUnit.Leave += numUnit_Leave;
			// 
			// lblDirty
			// 
			lblDirty.AutoSize = true;
			lblDirty.Font = new Font("Segoe UI", 6.75F, FontStyle.Italic);
			lblDirty.ForeColor = SystemColors.GrayText;
			lblDirty.Location = new Point(16, 978);
			lblDirty.Margin = new Padding(7, 0, 7, 0);
			lblDirty.Name = "lblDirty";
			lblDirty.Size = new Size(51, 25);
			lblDirty.TabIndex = 27;
			lblDirty.Text = "Dirty";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 6.75F, FontStyle.Italic);
			label1.ForeColor = SystemColors.GrayText;
			label1.Location = new Point(169, 76);
			label1.Margin = new Padding(7, 0, 7, 0);
			label1.Name = "label1";
			label1.Size = new Size(348, 25);
			label1.TabIndex = 28;
			label1.Text = "(Or what group of channels does it control)";
			// 
			// frmController
			// 
			AcceptButton = btnOK;
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(832, 1012);
			Controls.Add(lblDirty);
			Controls.Add(lblUnit);
			Controls.Add(numUnit);
			Controls.Add(cboLORModels);
			Controls.Add(btnChannels);
			Controls.Add(lblCount);
			Controls.Add(numCount);
			Controls.Add(txtIdentifier);
			Controls.Add(lblIdentifier);
			Controls.Add(txtModel);
			Controls.Add(lblModel);
			Controls.Add(txtComment);
			Controls.Add(lblComment);
			Controls.Add(chkActive);
			Controls.Add(lblxAddresses);
			Controls.Add(lblLastDMX);
			Controls.Add(lblStart);
			Controls.Add(numStart);
			Controls.Add(cboBrand);
			Controls.Add(lblBrand);
			Controls.Add(cboUniverse);
			Controls.Add(lblUniverse);
			Controls.Add(txtLocation);
			Controls.Add(lblLocation);
			Controls.Add(txtName);
			Controls.Add(lblName);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(6, 7, 6, 7);
			MaximizeBox = false;
			Name = "frmController";
			ShowInTaskbar = false;
			Text = "Controller";
			FormClosing += frmController_FormClosing;
			Load += frmController_Load;
			Shown += frmController_Shown;
			ResizeBegin += frmController_ResizeBegin;
			ResizeEnd += frmController_ResizeEnd;
			Paint += frmController_Paint;
			((System.ComponentModel.ISupportInitialize)numStart).EndInit();
			((System.ComponentModel.ISupportInitialize)numCount).EndInit();
			((System.ComponentModel.ISupportInitialize)numUnit).EndInit();
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
		//private System.Windows.Forms.TextBox txtControllerID;
		//private System.Windows.Forms.TextBox txtIdentifier;
		//private System.Windows.Forms.Label lblControllerID;
		//private System.Windows.Forms.Label lblIdentifier;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.NumericUpDown numCount;
		private System.Windows.Forms.Button btnChannels;
		private System.Windows.Forms.ToolTip tipTool;
		private ComboBox cboLORModels;
		private Label lblUnit;
		private NumericUpDown numUnit;
		private Label lblDirty;
		private Label label1;
		private TextBox txtIdentifier;
		private Label lblIdentifier;
	}
}