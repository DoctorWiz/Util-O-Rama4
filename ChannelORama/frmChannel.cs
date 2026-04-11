using FileHelper;
using FormHelper;
using LOR4;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Tools;   // SyncFusion TreeView Advanced
using Syncfusion.Windows.Forms.Tools.Win32API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Graphics.Printing.Workflow;
using LOR4;



namespace UtilORama4
{
	public partial class frmChannel : Form
	{
		public DMXChannel chanOriginal = null;
		public DMXChannel channel = null;
		public bool isDirty = false;
		public bool renumber = false;
		public bool loading = true;
		private bool moved = false;
		public frmList owner = null;

		private Color MultiColor = Color.FromArgb(128, 64, 64);
		private Color RGBColor = Color.FromArgb(64, 0, 0);
		private string duplicates = "";

		public const int CHANGE_NAME = 1;
		public const int CHANGE_LOCATION = 2;
		public const int CHANGE_COLOR = 4;
		public const int CHANGE_TYPE = 8;
		public const int CHANGE_ACTIVE = 16;
		public const int CHANGE_CONTROLLER = 32;
		public const int CHANGE_OUTPUT = 64;
		public const int CHANGE_COMMENT = 128;
		public int changes = 0;
		private const int WM_SYSCOMMAND = 0x0112;
		private const int SC_MINIMIZE = 0xf020;

		private int lastDeviceIndex = -1;
		private int lastControllerIndex = -1;
		public static readonly Color Color_RGB = ColorTranslator.FromHtml("#000001");
		public static readonly Color Color_RGBW = ColorTranslator.FromHtml("#000100");
		public static readonly Color Color_Multi = ColorTranslator.FromHtml("#010000");

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32() == SC_MINIMIZE)
			{
				// Minimize the owner (parent) form
				if (this.Owner != null)
				{
					this.Owner.WindowState = FormWindowState.Minimized;
				}

				// Optionally: Prevent the child from minimizing independently or closing
				// m.Result = IntPtr.Zero; 
				// return;
			}
			base.WndProc(ref m);
		}

		public frmChannel()
		{
			InitializeComponent();
			//FillCombos();
		}

		public frmChannel(DMXChannel chan)
		{
			owner = this.Owner as frmList;
			InitializeComponent();
			chanOriginal = chan;
			channel = chan.Copy();
			loading = true;
			LoadChannel(chan);
			MakeDirty(false);
			if (Fyle.IsAWizard)
			{
				lblDirty.Visible = true;
			}
		}

		public frmChannel(DMXChannel chan, frmList listform)
		{
			owner = listform;
			InitializeComponent();
			chanOriginal = chan;
			channel = chan.Copy();
			loading = true;
			channel.Editing = true;
			LoadChannel(channel);
			MakeDirty(false);
			if (Fyle.IsAWizard)
			{
				lblDirty.Visible = true;
			}
		}

		public void FillCombos()
		{
			// Device Types
			cboType.Items.Clear();
			for (int d = 0; d < owner.DeviceTypes.Count; d++)
			{
				string dn = owner.DeviceTypes[d].Name;
				int di = owner.DeviceTypes[d].ID;
				cboType.Items.Add(owner.DeviceTypes[d]);
				if (di == channel.DeviceType.ID)
				{
					cboType.SelectedIndex = d;
				}
			}

			// Controllers
			cboController.Items.Clear();
			for (int c = 0; c < owner.AllControllers.Count; c++)
			{
				DMXController controller = owner.AllControllers[c];
				string ctxt = "";
				if (controller.Identifier.Length > 0)
				{
					ctxt = controller.Identifier + ": ";
				}
				ctxt += controller.Name;
				ctxt += " @ " + controller.DMXStartAddress.ToString();
				ListItem li = new ListItem(ctxt, controller.ID);
				cboController.Items.Add(li);
				if (channel.DMXController.ID == controller.ID)
				{
					cboController.SelectedIndex = c;
				}
			}

			// Output Number
			numOutput.Value = channel.OutputNum;
			RefreshAddresses();
			numOutput.Maximum = channel.DMXController.OutputCount;

			if (Etc.xLightsVersion > 0)
			{
				lblxLightsAddress.Visible = false;
			}
			if (Etc.LORVersion > 0)
			{
				// Never mind, do nothing
			}
			loading = false;
			MakeDirty(false);
		}

		public void LoadChannel(DMXChannel chan)
		{
			channel = chan;
			//FillCombos();
			txtName.Text = chan.Name;
			txtComment.Text = chan.Comment;
			txtLocation.Text = chan.Location;
			chkActive.Checked = chan.Active;
			//picColor.BackColor = chan.Color;
			SetColor(chan.Color);
		}


		private void RefreshAddresses()
		{
			string tmodel = "Controller: " + channel.DMXController.ControllerBrand + ": " + channel.DMXController.ControllerModel;
			if (Etc.LORVersion > 0)
			{
				tmodel += ", Unit: " + channel.DMXController.UnitID.ToString();
			}
			lblModel.Text = tmodel;
			lblUniverse.Text = "Universe " + channel.DMXController.DMXUniverse.ToString();
			lblDMXAddress.Text = "DMX Address: " + channel.DMXAddress.ToString();
			if (Etc.xLightsVersion > 0)
			{
				lblxLightsAddress.Text = "xLights Address: " + channel.xLightsAddress.ToString();
			}
			bool wasBad = channel.BadOutput;
			DMXController newCtlr = channel.DMXController;
			int outCount = 0;
			for (int co = 0; co < channel.DMXController.DMXChannels.Count; co++)
			{
				if (channel.DMXController.DMXChannels[co].OutputNum == channel.OutputNum)
				{
					outCount++;
				}
			}
			//channel.BadOutput = outCount > 1;

			string tipText = "Select the controller this channel is connected to.";
			tipTool.SetToolTip(cboController, tipText);
			tipTool.SetToolTip(lblController, tipText);
			tipText = "Select the output # on this controller that this channel is connected to.";
			tipTool.SetToolTip(numOutput, tipText);
			tipTool.SetToolTip(lblOutput, tipText);
			channel.OutputNum = (int)numOutput.Value;
			if (channel.BadOutput)
			{
				tipText = "Warning: This channel shares the same address as\r\n" + duplicates;
				tipTool.SetToolTip(cboController, tipText);
				tipTool.SetToolTip(lblController, tipText);
				tipTool.SetToolTip(numOutput, tipText);
				tipTool.SetToolTip(lblOutput, tipText);
			}
			if (wasBad != channel.BadOutput) this.Refresh();
			//if (!loading) MakeDirty(true);
		}

		private void txtName_Validating(object sender, CancelEventArgs e)
		{
		}

		/*
	public int ValidateName(string theName)
	{
		 * int valid = 0;
		if (theName.Length < 2)
		{
			valid = -1;
			channel.badName = true;
			//tipTool.SetToolTip(txtName, "The channel MUST have a name!");
		}
		else
		{
			for (int c = 0; c < AllChannels.Count; c++)
			{
				if (!AllChannels[c].Editing)
				{
					if (theName.CompareTo(AllChannels[c].Name.ToLower()) == 0)
					{
						valid = -2;
						channel.badName = true;
						//tipTool.SetToolTip(txtName, "The channel MUST have a UNIQUE name!");
						c = AllChannels.Count; // Force exit loop
					}
				}
			}
		}
		return valid;
	}
	*/

		private void numOutput_Validating(object sender, CancelEventArgs e)
		{

		}

		private void cboController_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				// Did it really even change?
				if (cboController.SelectedIndex != lastControllerIndex)
				{
					MakeDirty(true);
				}
			}
		}

		/*
		public bool ValidateOutput()
		{
			bool valid = true;
			if (!loading)
			{
				duplicates = "";
				for (int c = 0; c < AllChannels.Count; c++)
				{
					if (!AllChannels[c].Editing)
					{
						if (AllChannels[c].DMXAddress == channel.DMXAddress)
						{
							if (AllChannels[c].DMXUniverse.UniverseNumber == channel.DMXUniverse.UniverseNumber)
							{
								valid = false;
								string msg = channel.Name + " ID:" + channel.ID + " DMX:" + channel.DMXAddress + " = ";
								msg += AllChannels[c].Name + " ID: " + AllChannels[c].ID + " DMX:" + AllChannels[c].DMXAddress;
								duplicates += AllChannels[c].ToString() + "\r\n";
								//c = AllChannels.Count;
							}
						}
						else
						{
							if (AllChannels[c].xLightsAddress == channel.xLightsAddress)
							{
								valid = false;
								duplicates += AllChannels[c].ToString() + "\r\n";
								//c = AllChannels.Count;
							}
						}
					}
				}
			}
			channel.badOutput = !valid;
			return valid;
		}
		*/

		private void frmChannel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen p = new Pen(Color.Red, 2);
			if (channel.BadOutput)
			{
				//g.DrawRectangle(p, numOutput.Left - 2, numOutput.Top - 2, numOutput.Width + 4, numOutput.Height + 4);
				int w = numOutput.Width + (numOutput.Left - cboController.Left);
				g.DrawRectangle(p, cboController.Left - 2, numOutput.Top - 2, w + 4, numOutput.Height + 4);
			}
			if (channel.BadName)
			{
				g.DrawRectangle(p, txtName.Left - 2, txtName.Top - 2, txtName.Width + 4, txtName.Height + 4);
			}
		}

		private void txtLocation_Validating(object sender, CancelEventArgs e)
		{
		}

		private void cboType_Validating(object sender, CancelEventArgs e)
		{
			//channel.ChannelType = (ChannelType)cboType.SelectedIndex;
			//if (!loading) MakeDirty(true);
		}

		private void chkActive_CheckedChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				channel.Active = chkActive.Checked;
				changes |= CHANGE_ACTIVE;
				MakeDirty(true);
			}
		}

		private void txtComment_Validating(object sender, CancelEventArgs e)
		{
		}

		private void picColor_Click(object sender, EventArgs e)
		{
			/*
			 * DialogResult dr = clrColors.ShowDialog(this);
					if (dr == DialogResult.OK)
					{
						SetColor(clrColors.Color);
					}
		*/
		}

		public void SetColor(Color color)
		{
			// If ColorName is not already set by a prior version, then look it up.
			if (channel.ColorName.Length < 2)
			{
				channel.ColorName = LOR4Admin.NearestColorName(color);
			}

			if (color == Etc.Color_RGB)
			{
				tipTool.SetToolTip(picColor, "RGB");
				tipTool.SetToolTip(lblColorLabel, "RGB");
				picColor.BackColor = Color.Transparent;
				picColor.Image = picRGB.Image;
			}
			else if (color == Etc.Color_RGBW)
			{
				tipTool.SetToolTip(picColor, "RGBW");
				tipTool.SetToolTip(lblColorLabel, "RGBW");
				picColor.BackColor = Color.Transparent;
				picColor.Image = picRGBW.Image;
			}
			else if (color == Etc.Color_Multi)
			{
				tipTool.SetToolTip(picColor, "Multicolored");
				tipTool.SetToolTip(lblColorLabel, "Multicolored");
				picColor.BackColor = Color.Transparent;
				picColor.Image = picMulti.Image;
			}
			else
			{
				tipTool.SetToolTip(picColor, channel.ColorName);
				tipTool.SetToolTip(lblColorLabel, channel.ColorName);
				picColor.BackColor = color;
				picColor.Image = null;
			}
			picColor.Refresh();

			if (!loading)
			{
				if (channel.Color != color)
				{
					channel.Color = color;
					changes |= CHANGE_COLOR;
					MakeDirty(true);
				}
			}
		}


		private void cboController_Validating(object sender, CancelEventArgs e)
		{
			/*
			channel.badOutput = false; // Optomistic reset
			string tipText = "Set the output number for this channel on controller ";
			tipText += channel.DMXController.ControllerID + ": " + channel.DMXController.Name;
			tipTool.SetToolTip(numOutput, tipText);
			tipText = "Select the controller this channel is connected to.";
			tipTool.SetToolTip(cboController, tipText);
			DMXUniverse uni = channel.DMXUniverse;
			channel.DMXController = uni.DMXControllers[cboController.SelectedIndex];
			for (int c = 0; c < channel.DMXController.DMXChannels.Count; c++)
			{
				if (!channel.DMXController.DMXChannels[c].Editing)
				{
					if (numOutput.Value == channel.DMXController.DMXChannels[c].LOR4Output) ;
					{
						channel.badOutput = true;
						tipText = "LOR4Output " + numOutput.Value.ToString() + " is already being used by channel ";
						tipText += channel.DMXController.DMXChannels[c].Name;
						tipText += " on controller " + channel.DMXController.ControllerID + ": " + channel.DMXController.Name;
						tipTool.SetToolTip(numOutput, tipText);
						tipTool.SetToolTip(cboController, tipText);
						c = channel.DMXController.DMXChannels.Count; // Force exit loop
					}
				}
			}
			if (!loading) MakeDirty(true);
			*/

		}

		private void chkActive_Validating(object sender, CancelEventArgs e)
		{
		}

		public void MakeDirty(bool dirty)
		{
			if (dirty != isDirty)
			{
				if (dirty)
				{
					if (!loading)
					{
						this.Text = "Channel: " + channel.Name + " (Modified)";
						isDirty = true;
						lblDirty.ForeColor = Color.Red;
						lblDirty.Text = "Dirty";
					}
				}
				else
				{
					this.Text = "Channel: " + channel.Name;
					isDirty = false;
					lblDirty.ForeColor = SystemColors.GrayText;
					lblDirty.Text = "Clean";
				}
			}
		}

		private void frmChannel_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Fyle.isWiz)
			{
				//System.Diagnostics.Debugger.Break();
			}
			if (isDirty)
			{
				string dtxt = "Channel settings have changed.  Save them?";
				DialogResult dr = MessageBox.Show(this, dtxt, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.Yes)
				{
					SaveAndExit();
				}
				else if (dr == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
			}
		}

		private void numOutput_ValueChanged(object sender, EventArgs e)
		{
		}


		private void frmChannel_Load(object sender, EventArgs e)
		{
			if (!moved)
			{
				Fourm.SetFormPosition(this);
				moved = true;
			}
			lblDirty.Visible = Fyle.IsAWizard;
		}

		private void frmChannel_Shown(object sender, EventArgs e)
		{
			FillCombos();
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
		}

		private void txtName_Leave(object sender, EventArgs e)
		{
			string tipText = "The name of this channel";
			bool wasBad = channel.BadName;
			string t = txtName.Text.Trim();
			if (t != chanOriginal.Name)
			{
				channel.Name = t;
				if (channel.BadName)
				{
					tipText = "The channel MUST have a UNIQUE name!";
				}
				if (wasBad != channel.BadName) this.Refresh();
				if (!loading) MakeDirty(true);
			}
		}

		private void cboType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				if (cboType.SelectedIndex != lastDeviceIndex)
				{
					MakeDirty(true);
				}
			}
		}

		private void lblColor_Click(object sender, EventArgs e)
		{
			picColor_Click(sender, e);
		}

		private void btnColor_Click(object sender, EventArgs e)
		{
			/*
			using (var picker = new frmColor())
			{
				if (picker.ShowDialog() == DialogResult.OK)
				{
					// Use the selected color, for example, to set a button background
					//this.btnPickColor.BackColor = picker.SelectedColor;
					SetColor(picker.SelectedColor);
				}
			}
			*/
		}

		private void txtName_Enter(object sender, EventArgs e)
		{
			string tipText = "The name of this channel";
			tipTool.SetToolTip(txtName, tipText);
			tipTool.SetToolTip(lblName, tipText);
		}

		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			// If escape is pressed, restore to the original value
			if (e.KeyCode == Keys.Escape)
			{
				txtName.Text = chanOriginal.Name;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = txtName.Text.Trim();
				if (t != chanOriginal.Name)
				{
					channel.Name = t;
					txtName.Text = t;
					if (!loading) MakeDirty(true);
				}
			}
		}

		private void txtLocation_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				txtLocation.Text = chanOriginal.Location;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string trimloc = txtLocation.Text.Trim();
				if (chanOriginal.Location != trimloc)
				{
					channel.Location = trimloc;
					txtLocation.Text = trimloc;
					changes |= CHANGE_LOCATION;
					MakeDirty(true);
				}
			}
		}

		private void txtLocation_Enter(object sender, EventArgs e)
		{
			string tipText = "The physical location of this channel (optional)";
			tipTool.SetToolTip(txtLocation, tipText);
			tipTool.SetToolTip(lblLocation, tipText);
		}

		private void txtLocation_Leave(object sender, EventArgs e)
		{
			if (!loading)
			{
				string trimloc = txtLocation.Text.Trim();
				if (chanOriginal.Location != trimloc)
				{
					channel.Location = trimloc;
					txtLocation.Text = trimloc;
					changes |= CHANGE_LOCATION;
					MakeDirty(true);
				}
			}
		}

		private void cboType_Leave(object sender, EventArgs e)
		{
			if (!loading)
			{
				if (cboType.SelectedIndex >= 0)
				{
					if (cboType.SelectedItem != null)
					{
						DMXDeviceType device = (DMXDeviceType)cboType.SelectedItem;
						if (device.Name != chanOriginal.DeviceType.Name)
						{
							//channel.ChannelType = (ChannelType)cboType.SelectedIndex;
							//string dn = device.Name;
							channel.DeviceType = device;
							changes |= CHANGE_TYPE;
							MakeDirty(true);
						}
					}
				}
			}
		}

		private void cboType_Enter(object sender, EventArgs e)
		{
			string tipText = "The type of device this channel is connected to";
			tipTool.SetToolTip(cboType, tipText);
			tipTool.SetToolTip(lblType, tipText);
		}

		private void cboType_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (chanOriginal.DeviceType != null)
				{
					for (int d = 0; d < cboType.Items.Count; d++)
					{
						DMXDeviceType de = (DMXDeviceType)cboType.Items[d];
						string dn = de.Name;
						if (de.ID == chanOriginal.DeviceType.ID)
						{
							cboType.SelectedIndex = d;
							d = cboType.Items.Count; // Force exit loop
						}
					}
				}
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if (cboType.SelectedIndex >= 0)
				{
					if (cboType.SelectedItem != null)
					{
						DMXDeviceType device = (DMXDeviceType)cboType.SelectedItem;
						if (device.Name != chanOriginal.DeviceType.Name)
						{
							//channel.ChannelType = (ChannelType)cboType.SelectedIndex;
							//string dn = device.Name;
							channel.DeviceType = device;
							changes |= CHANGE_TYPE;
							MakeDirty(true);
						}
					}
				}
			}
		}

		private void chkActive_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				chkActive.Checked = !chkActive.Checked;
			}
			else if (e.KeyCode == Keys.Escape)
			{
				chkActive.Checked = chanOriginal.Active;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if (chkActive.Checked != chanOriginal.Active)
				{
					bool wasBad = channel.BadOutput;
					channel.Active = chkActive.Checked;
					changes |= CHANGE_ACTIVE;
					MakeDirty(true);
					if (wasBad != channel.BadOutput) this.Refresh();
				}
			}
		}

		private void chkActive_Enter(object sender, EventArgs e)
		{
			string tipText = "Check if this channel is active and should be included in the output";
			tipTool.SetToolTip(chkActive, tipText);
		}

		private void chkActive_Leave(object sender, EventArgs e)
		{
			if (!loading)
			{
				if (chkActive.Checked != chanOriginal.Active)
				{
					bool wasBad = channel.BadOutput;
					channel.Active = chkActive.Checked;
					changes |= CHANGE_ACTIVE;
					MakeDirty(true);
					if (wasBad != channel.BadOutput) this.Refresh();
				}
			}
		}

		private void cboController_Enter(object sender, EventArgs e)
		{
			string tipText = "Select the controller this channel is connected to.";
			tipText += "\r\nThe controller must be defined in the universe editor.";
			tipTool.SetToolTip(cboController, tipText);
			tipTool.SetToolTip(lblController, tipText);
		}

		private void cboController_Leave(object sender, EventArgs e)
		{
			if (!loading)
			{
				// Did it really even change?
				ListItem li = (ListItem)cboController.SelectedItem;
				int newID = li.ID;
				if (channel.DMXController.ID != newID)
				{
					changes |= CHANGE_CONTROLLER;
					MakeDirty(true);
					// Now we have to find it...
					for (int u = 0; u < owner.AllUniverses.Count; u++)
					{
						DMXUniverse universe = owner.AllUniverses[u];
						for (int c = 0; c < universe.DMXControllers.Count; c++)
						{
							DMXController controller = universe.DMXControllers[c];
							if (controller.ID == newID)
							{
								channel.DMXController = controller;
								c = universe.DMXControllers.Count;
								u = owner.AllUniverses.Count;
							}
						}
					}
					numOutput.Maximum = channel.DMXController.OutputCount;
					//OutputChange();
					RefreshAddresses();
				}
			}

		}

		private void cboController_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (chanOriginal.DMXController != null)
				{
					for (int u = 0; u < owner.AllUniverses.Count; u++)
					{
						DMXUniverse uni = owner.AllUniverses[u];
						for (int c = 0; c < uni.DMXControllers.Count; c++)
						{
							ListItem li = new ListItem(uni.DMXControllers[c].ToString(), uni.DMXControllers[c].ID);
							if (li.ID == chanOriginal.DMXController.ID)
							{
								cboController.SelectedIndex = cboController.Items.IndexOf(li);
								c = uni.DMXControllers.Count; // Force exit loop
								u = owner.AllUniverses.Count;
							}
						}
					}
				}
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if (cboController.SelectedIndex >= 0)
				{
					ListItem li = (ListItem)cboController.SelectedItem;
					int newID = li.ID;
					if (channel.DMXController.ID != newID)
					{
						changes |= CHANGE_CONTROLLER;
						MakeDirty(true);
						// Now we have to find it...
						for (int u = 0; u < owner.AllUniverses.Count; u++)
						{
							DMXUniverse universe = owner.AllUniverses[u];
							for (int c = 0; c < universe.DMXControllers.Count; c++)
							{
								DMXController controller = universe.DMXControllers[c];
								if (controller.ID == newID)
								{
									channel.DMXController = controller;
									c = universe.DMXControllers.Count;
									u = owner.AllUniverses.Count;
								}
							}
						}
						//OutputChange();
						RefreshAddresses();
					}
				}
			}
		}

		private void txtComment_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				txtComment.Text = chanOriginal.Comment;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string trimcom = txtComment.Text.Trim();
				if (chanOriginal.Comment != trimcom)
				{
					channel.Comment = trimcom;
					txtComment.Text = trimcom;
					changes |= CHANGE_COMMENT;
					MakeDirty(true);
				}
			}
		}

		private void txtComment_Enter(object sender, EventArgs e)
		{
			string tipText = "Any comments about this channel";
			tipTool.SetToolTip(txtComment, tipText);
			tipTool.SetToolTip(lblComment, tipText);
		}

		private void txtComment_Leave(object sender, EventArgs e)
		{
			if (!loading)
			{
				string trimcom = txtComment.Text.Trim();
				if (chanOriginal.Comment != trimcom)
				{
					channel.Comment = trimcom;
					txtComment.Text = trimcom;
					changes |= CHANGE_COMMENT;
					MakeDirty(true);
				}
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			SaveAndExit();
		}

		private bool SaveAndExit()
		{
			if (isDirty)
			{
				chanOriginal.ApplyChanges(channel);
			}
			// Do not close or unload, so that parent form can read the dirty and renumber flags and decide what to do. Just hide this form.
			this.Hide();
			return true;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			// Do not close or unload, so that parent form can read the dirty and renumber flags and decide what to do. Just hide this form.
			this.Hide();
		}

		private void numOutput_Enter(object sender, EventArgs e)
		{
			string tipText = "Select the output # on this controller that this channel is connected to.";
			tipTool.SetToolTip(numOutput, tipText);
			tipTool.SetToolTip(lblOutput, tipText);
		}

		private void numOutput_Leave(object sender, EventArgs e)
		{
			if (!loading)
			{
				// Did it really even change?
				// Cast from decimal type used by NumUpDown control to int
				int newVal = (int)numOutput.Value;
				if (chanOriginal.OutputNum != newVal)
				{
					channel.OutputNum = newVal;
					changes |= CHANGE_OUTPUT;
					MakeDirty(true);
					renumber = true;
				}
			}
			RefreshAddresses();

		}

		private void numOutput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				numOutput.Value = chanOriginal.OutputNum;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if ((int)numOutput.Value != chanOriginal.OutputNum)
				{
					channel.OutputNum = (int)numOutput.Value;
					changes |= CHANGE_OUTPUT;
					MakeDirty(true);
					renumber = true;
				}
			}
		}

		private void cboType_DropDown(object sender, EventArgs e)
		{
			if (!loading)
			{
				lastDeviceIndex = cboType.SelectedIndex;
			}
		}

		private void cboController_DropDown(object sender, EventArgs e)
		{
			if (!loading)
			{
				lastControllerIndex = cboController.SelectedIndex;
			}
		}

		private void Pick_Color_Click(object sender, EventArgs e)
		{
			{
				//DialogResult dr = clrColors.ShowDialog(this);
				frmColors picker = new frmColors();
				picker.color = channel.Color;
				DialogResult dr = picker.ShowDialog(this);
				if (dr == DialogResult.OK)
				{
					channel.ColorName = picker.selectedName;
					SetColor(picker.color);
				}
				picker.Dispose();
			}
		}

		private void picColor_Paint(object sender, PaintEventArgs e)
		{
			string text = channel.ColorName;
			Font font = new Font("Arial Narrow", 8.0f, FontStyle.Bold, GraphicsUnit.Point);
			Brush brush = null;

			if (channel.Color.GetBrightness() < 0.6f)
			{
				brush = Brushes.White;
			}
			else
			{
				brush = Brushes.Black;
			}

			// 1. Create a StringFormat object
			using (StringFormat sf = new StringFormat())
			{
				// 2. Set horizontal and vertical alignment to Center
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				// 3. Draw the string using the control's ClientRectangle as the bounds
				e.Graphics.DrawString(text, font, brush, picColor.ClientRectangle, sf);
			}

		}

		private void frmChannel_ResizeBegin(object sender, EventArgs e)
		{

		}

		private void frmChannel_ResizeEnd(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
			{
				owner.WindowState = FormWindowState.Minimized;
				this.WindowState = FormWindowState.Normal;
			}
		}
	} // End class frmChannel
}  // End namespace
