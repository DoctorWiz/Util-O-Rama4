using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOR4Utils;
using FileHelper;


namespace UtilORama4
{
	public partial class frmChannel : Form
	{
		public List<DMXUniverse> universes = null;
		//public List<DMXChannel> AllChannels = null;
		//public List<DMXDevice> DeviceTypes = null;
		public DMXChannel channel = null;
		public bool dirty = false;
		public bool loading = true;
		private bool moved = false;

		private Color MultiColor = Color.FromArgb(128, 64, 64);
		private Color RGBColor = Color.FromArgb(64, 0, 0);
		private string duplicates = "";



		public frmChannel()
		{
			InitializeComponent();
			//FillCombos();
		}

		public frmChannel(DMXChannel chan, List<DMXUniverse> universeList)
		{
			InitializeComponent();
			universes = universeList;
			//devices = deviceList;
			//AllChannels = allChannelList;
			//FillCombos();
			LoadChannel(chan);
			loading = false;
			MakeDirty(false);
			//ValidateName(txtName.Text);
			//ValidateOutput();
			//picColor.CanFocus = true;

		}

		public void SetFormPosition()
		{
			if (!moved)
			{
				int l = (this.Owner.Width - this.Width) / 2 + this.Owner.Left;
				int t = (this.Owner.Height - this.Height) / 2 + this.Owner.Top;
				this.Left = l;
				this.Top = t;
				moved = true;
			}
		}
		public void FillCombos()
		{
			cboType.Items.Clear();
			//cboType.DataSource = Enum.GetValues(typeof(ChannelType));
			for (int d = 0; d < DMXChannel.DeviceTypes.Count; d++)
			{
				string dn = DMXChannel.DeviceTypes[d].Name;
				cboType.Items.Add(DMXChannel.DeviceTypes[d]);
			}


			cboController.Items.Clear();
			if (universes != null)
			{
				if (universes.Count > 0)
				{
					if (channel != null)
					{
						if (channel.DMXController != null)
						{
							if (channel.DMXController.DMXUniverse != null)
							{
								for (int u = 0; u < universes.Count; u++)
								{
									DMXUniverse uni = universes[u];
									for (int c = 0; c < uni.DMXControllers.Count; c++)
									{
										ListItem li = new ListItem(uni.DMXControllers[c].ToString(), uni.DMXControllers[c].ID);
										cboController.Items.Add(li);
									}
								}
							}
						}
					}
				}
			}
		}

		public void LoadChannel(DMXChannel chan)
		{
			channel = chan;
			FillCombos();
			txtName.Text = chan.Name;
			txtComment.Text = chan.Comment;
			txtLocation.Text = chan.Location;
			chkActive.Checked = chan.Active;
			//picColor.BackColor = chan.Color;
			SetColor(chan.Color);
			int devID = chan.DeviceType.ID;

			if (devID > 0 && devID < cboType.Items.Count)
			{
				for (int d = 0; d < cboType.Items.Count; d++)
				{
					DMXDeviceType de = (DMXDeviceType)cboType.Items[d];
					string dn = de.Name;
					if (de.ID == devID)
					{
						cboType.SelectedIndex = d;
						d = cboType.Items.Count;
					}
				}
			}
			numOutput.Value = chan.OutputNum;
			if (chan.DMXController != null)
			{
				for (int i = 0; i < cboController.Items.Count; i++)
				{
					ListItem li = (ListItem)cboController.Items[i];
					if (li.ID == chan.DMXController.ID)
					{
						cboController.SelectedIndex = i;
						i = cboController.Items.Count; // Exit loop
					}
				}
				lblModel.Text = chan.DMXController.ControllerBrand + ": " + chan.DMXController.ControllerModel;
				lblUniverse.Text = "Universe " + chan.DMXController.DMXUniverse.ToString();
				lblDMXAddress.Text = "DMX Address: " + chan.DMXAddress.ToString();
				lblxLighsAddress.Text = "xLights Address: " + chan.xLightsAddress.ToString();
			}
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
				ListItem li = (ListItem)cboController.SelectedItem;
				int newID = li.ID;
				if (channel.DMXController.ID != newID)
				{
					// Now we have to find it...
					for (int u = 0; u < universes.Count; u++)
					{
						DMXUniverse universe = universes[u];
						for (int c = 0; c < universe.DMXControllers.Count; c++)
						{
							DMXController controller = universe.DMXControllers[c];
							if (controller.ID == newID)
							{
								channel.DMXController = controller;
								c = universe.DMXControllers.Count;
								u = universes.Count;
							}
						}
					}
					OutputChange();
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
			channel.Location = txtLocation.Text.TrimStart();
			txtLocation.Text = channel.Location;
			if (!loading) MakeDirty(true);
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
				MakeDirty(true);
			}
		}

		private void txtComment_Validating(object sender, CancelEventArgs e)
		{
			channel.Comment = txtComment.Text.TrimStart();
			txtComment.Text = channel.Comment;
			if (!loading) MakeDirty(true);
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
			string cname = color.Name;
			if (color == MultiColor)
			{
				btnColor.Image = picMulticolor.Image;
				cname = "MultiColor";
			}
			else
			{
				if (color == RGBColor)
				{
					btnColor.Image = picRGB.Image;
					cname = "RGB";
				}
				else
				{
					btnColor.BackColor = color;
					btnColor.Image = null;
					//cname = lutils.NearestColorName(color);

				}
			}
			channel.Color = color;
			tipTool.SetToolTip(btnColor, cname);
			tipTool.SetToolTip(lblColor, cname);
			btnColor.Refresh();
			if (!loading) MakeDirty(true);

		}


		private void cboController_Validating(object sender, CancelEventArgs e)
		{
			/*
			channel.badOutput = false; // Optomistic reset
			string tipText = "Set the output number for this channel on controller ";
			tipText += channel.DMXController.LetterID + ": " + channel.DMXController.Name;
			tipTool.SetToolTip(numOutput, tipText);
			tipText = "Select the controller this channel is connected to.";
			tipTool.SetToolTip(cboController, tipText);
			DMXUniverse uni = channel.DMXUniverse;
			channel.DMXController = uni.DMXControllers[cboController.SelectedIndex];
			for (int c = 0; c < channel.DMXController.DMXChannels.Count; c++)
			{
				if (!channel.DMXController.DMXChannels[c].Editing)
				{
					if (numOutput.Value == channel.DMXController.DMXChannels[c].LOROutput4) ;
					{
						channel.badOutput = true;
						tipText = "LOROutput4 " + numOutput.Value.ToString() + " is already being used by channel ";
						tipText += channel.DMXController.DMXChannels[c].Name;
						tipText += " on controller " + channel.DMXController.LetterID + ": " + channel.DMXController.Name;
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

		public void MakeDirty(bool isDirty)
		{
			if (dirty != isDirty)
			{
				if (dirty)
				{
					if (!loading)
					{
						this.Text = "LOR4Channel: " + channel.Name + " (Modified)";
						dirty = isDirty;
					}
				}
				else
				{
					this.Text = "LOR4Channel: " + channel.Name;
					dirty = isDirty;
				}
			}
		}

		private void frmChannel_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void numOutput_ValueChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				// Did it really even change?
				// Cast from decimal type used by NumUpDown control to int
				int newVal = (int)numOutput.Value;
				if (channel.OutputNum != newVal)
				{
					channel.OutputNum = newVal;
					OutputChange();
				}
			}
		}

		private void OutputChange()
		{
			bool wasBad = channel.BadOutput;
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
			if (!loading) MakeDirty(true);
		}

		private void frmChannel_Load(object sender, EventArgs e)
		{

		}

		private void frmChannel_Shown(object sender, EventArgs e)
		{
			if (Owner != null)
			{
				SetFormPosition();
			}
			else
			{
				Fyle.MakeNoise(Fyle.Noises.Dammit);
			}
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			bool wasBad = channel.BadName;
			string theName = txtName.Text.TrimStart();
			txtName.Text = theName;
			channel.Name = theName;
			string tipText = "The name of this channel";

			if (channel.BadName)
			{
				tipText = "The channel MUST have a UNIQUE name!";
			}
			tipTool.SetToolTip(txtName, tipText);
			tipTool.SetToolTip(lblName, tipText);
			if (!loading) MakeDirty(true);
			if (wasBad != channel.BadName) this.Refresh();
		}

		private void txtName_Leave(object sender, EventArgs e)
		{

		}

		private void cboType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				if (cboType.SelectedIndex >= 0)
				{
					if (cboType.SelectedItem != null)
					{
						DMXDeviceType device = (DMXDeviceType)cboType.SelectedItem;
						string dn = device.Name;
						channel.DeviceType = device;
						MakeDirty(true);
					}
				}
			}
		}

		private void lblColor_Click(object sender, EventArgs e)
		{
			picColor_Click(sender, e);
		}

		private void btnColor_Click(object sender, EventArgs e)
		{
			DialogResult dr = clrColors.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				SetColor(clrColors.Color);
			}

		}
	}
}
