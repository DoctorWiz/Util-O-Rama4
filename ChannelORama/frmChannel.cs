using FileHelper;
using FormHelper;
using LOR4;
using LOR4;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Tools;   // SyncFusion TreeView Advanced
using Syncfusion.Windows.Forms.Tools.Win32API;
using Syncfusion.XlsIO;
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
using System.Runtime.InteropServices;
using Windows.ApplicationModel.Preview.InkWorkspace;
using Windows.Graphics.Printing.Workflow;
using Windows.Management.Update;




namespace UtilORama4
{
	/*
	public class HoverComboBox : ComboBox
	{
		// WinAPI Constants
		private const int WM_CTLCOLORLISTBOX = 0x0134;
		private const int LB_GETCURSEL = 0x0188;

		public event EventHandler<HoverIndexChangedEventArgs> HoverIndexChanged;
		private int _lastHoveredIndex = -1;

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			// When the dropdown list is about to be drawn or updated
			if (m.Msg == WM_CTLCOLORLISTBOX)
			{
				// m.LParam is the handle (HWND) to the internal DropDown ListBox
				IntPtr indexPtr = SendMessage(m.LParam, LB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
				int currentIndex = indexPtr.ToInt32();

				if (currentIndex != _lastHoveredIndex)
				{
					_lastHoveredIndex = currentIndex;
					OnHoverIndexChanged(new HoverIndexChangedEventArgs(currentIndex));
				}
			}

			// Reset index when the dropdown closes
			if (m.Msg == 0x0201) // WM_LBUTTONDOWN or similar can be used to detect closure
			{
				_lastHoveredIndex = -1;
			}
		}

		protected virtual void OnHoverIndexChanged(HoverIndexChangedEventArgs e)
		{
			HoverIndexChanged?.Invoke(this, e);
		}
	}

	public class HoverIndexChangedEventArgs : EventArgs
	{
		public int Index { get; }
		public HoverIndexChangedEventArgs(int index) => Index = index;
	}
	*/

	public partial class frmChannel : Form
	{
		public Channel chanOriginal = null;
		public Channel channel = null;
		public bool isDirty = false;
		public bool renumber = false;
		public bool loading = true;
		private bool moved = false;
		public bool userClose = false;
		public frmList owner = null;
		private string uniName = "Universe";

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
		private bool editing = false;
		private bool adding = false;

		private int lastDeviceIndex = -1;
		private int lastControllerIndex = -1;
		public static readonly Color Color_RGB = ColorTranslator.FromHtml("#000001");
		public static readonly Color Color_RGBW = ColorTranslator.FromHtml("#000100");
		public static readonly Color Color_Multi = ColorTranslator.FromHtml("#010000");

		private frmColors picker = null;
		private FormWindowState prevWindowState = FormWindowState.Normal;

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		protected override void WndProc(ref Message m)
		{
			const int WM_CTLCOLORLISTBOX = 0x0134;
			const int LB_GETCURSEL = 0x0188;

			if (m.Msg == WM_CTLCOLORLISTBOX)
			{
				// m.LParam is the handle to the dropdown list box
				// LB_GETCURSEL retrieves the index of the hovered/selected item
				IntPtr index = SendMessage(m.LParam, LB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
				//Console.WriteLine("Hovered Index: " + index.ToInt32());
				int i = index.ToInt32();
				DeviceType device = (DeviceType)cboDevice.Items[i];
				string ttxt = device.Comment;
				tipTool.SetToolTip(cboDevice, ttxt);
			}
			base.WndProc(ref m);
		}
		public frmChannel()
		{
			InitializeComponent();
			//FillCombos();
		}

		public frmChannel(Channel chan)
		{
			owner = this.Owner as frmList;
			uniName = frmList.uniName;
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

		public frmChannel(Channel chan, frmList listform)
		{
			owner = listform;
			uniName = frmList.uniName;
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
			cboDevice.Items.Clear();
			for (int d = 0; d < frmList.DeviceTypes.Count; d++)
			{
				string dn = frmList.DeviceTypes[d].Name;
				int di = frmList.DeviceTypes[d].ID;
				cboDevice.Items.Add(frmList.DeviceTypes[d]);
				if (di == channel.DeviceType.ID)
				{
					cboDevice.SelectedIndex = d;
				}
			}

			// Controllers
			cboController.Items.Clear();
			for (int c = 0; c < frmList.AllControllers.Count; c++)
			{
				Controller controller = frmList.AllControllers[c];
				string ctxt = "";
				if (controller.Identifier.Length > 0)
				{
					ctxt = controller.Identifier + ": ";
				}
				ctxt += controller.Name;
				ctxt += " @ " + controller.StartAddress.ToString();
				ListItem li = new ListItem(ctxt, controller.ID);
				cboController.Items.Add(li);
				if (channel.Controller.ID == controller.ID)
				{
					cboController.SelectedIndex = c;
				}
			}

			// Output Number
			numOutput.Value = channel.OutputNum;
			RefreshAddresses();
			numOutput.Maximum = channel.Controller.OutputCount;

			if (frmList.hasxLights)
			{
				lblxLightsAddress.Visible = false;
			}
			if (frmList.hasLOR)
			{
				// Never mind, do nothing
			}
			loading = false;
			MakeDirty(false);
		}

		public void LoadChannel(Channel chan)
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
			string tmodel = "Controller: " + channel.Controller.ControllerBrand + ": " + channel.Controller.ControllerModel;
			if (frmList.hasLOR)
			{
				tmodel += ", Unit: " + channel.Controller.UnitID.ToString();
			}
			lblModel.Text = tmodel;
			lblUniverse.Text = uniName + " " + channel.Controller.Universe.ToString();
			lblAddress.Text = "Address: " + channel.Address.ToString();
			if (frmList.hasxLights)
			{
				lblxLightsAddress.Text = "xLights Address: " + channel.xLightsAddress.ToString();
			}
			bool wasBad = channel.BadOutput;
			Controller newCtlr = channel.Controller;
			int outCount = 0;
			for (int co = 0; co < channel.Controller.Channels.Count; co++)
			{
				if (channel.Controller.Channels[co].OutputNum == channel.OutputNum)
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
						if (AllChannels[c].Address == channel.Address)
						{
							if (AllChannels[c].Universe.UniverseNumber == channel.Universe.UniverseNumber)
							{
								valid = false;
								string msg = channel.Name + " ID:" + channel.ID + " DMX:" + channel.Address + " = ";
								msg += AllChannels[c].Name + " ID: " + AllChannels[c].ID + " DMX:" + AllChannels[c].Address;
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

		private void cboDevice_Validating(object sender, CancelEventArgs e)
		{
			//channel.ChannelType = (ChannelType)hcboDevic3.SelectedIndex;
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

		public void SetColor(Color color)
		{
			// If ColorName is not already set by a prior version, then look it up.
			if (channel.ColorName.Length < 2)
			{
				channel.ColorName = LOR4Admin.NearestColorName(color);
			}

			if (color == frmList.Color_RGB)
			{
				tipTool.SetToolTip(picColor, "RGB");
				tipTool.SetToolTip(lblColorLabel, "RGB");
				picColor.BackColor = Color.Transparent;
				picColor.Image = picRGB.Image;
			}
			else if (color == frmList.Color_RGBW)
			{
				tipTool.SetToolTip(picColor, "RGBW");
				tipTool.SetToolTip(lblColorLabel, "RGBW");
				picColor.BackColor = Color.Transparent;
				picColor.Image = picRGBW.Image;
			}
			else if (color == frmList.Color_Multi)
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
			tipText += channel.Controller.ControllerID + ": " + channel.Controller.Name;
			tipTool.SetToolTip(numOutput, tipText);
			tipText = "Select the controller this channel is connected to.";
			tipTool.SetToolTip(cboController, tipText);
			Universe uni = channel.Universe;
			channel.Controller = uni.Controllers[cboController.SelectedIndex];
			for (int c = 0; c < channel.Controller.Channels.Count; c++)
			{
				if (!channel.Controller.Channels[c].Editing)
				{
					if (numOutput.Value == channel.Controller.Channels[c].LOR4Output) ;
					{
						channel.badOutput = true;
						tipText = "LOR4Output " + numOutput.Value.ToString() + " is already being used by channel ";
						tipText += channel.Controller.Channels[c].Name;
						tipText += " on controller " + channel.Controller.ControllerID + ": " + channel.Controller.Name;
						tipTool.SetToolTip(numOutput, tipText);
						tipTool.SetToolTip(cboController, tipText);
						c = channel.Controller.Channels.Count; // Force exit loop
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
			string txtinfo = e.CloseReason.ToString();
			if (e.CloseReason == CloseReason.None)
			{
				// NONE?!?!
				//e.Cancel = true;
				//this.Hide();
			}


			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (!userClose)
				{
					if (isDirty)
					{
						string dtxt = "Channel settings have changed.  Save them?";
						DialogResult dr = MessageBox.Show(this, dtxt, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
						if (dr == DialogResult.Yes)
						{
							DialogResult = DialogResult.OK;
							userClose = true;
							// Don't actually close/unload it, just hide it
							e.Cancel = true;
							SaveAndExit();
						}
						else if (dr == DialogResult.No)
						{
							DialogResult = DialogResult.Cancel;
							userClose = true;
							MakeDirty(false);
							// Don't actually close/unload it, just hide it
							e.Cancel = true;
							this.Hide();
						}
						else if (dr == DialogResult.Cancel)
						{
							e.Cancel = true;
						}
					} // End if dirty
				} // End if not userClose flag
			} // End if reason for closing is by user
		} // End Form Closing event

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

		private void lblColor_Click(object sender, EventArgs e)
		{
			//picColor_Click(sender, e);
			picColor_Click(sender, e);
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
			tipText += "\r\nThe controller must be defined in the " + uniName + " editor.";
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
				if (channel.Controller.ID != newID)
				{
					changes |= CHANGE_CONTROLLER;
					MakeDirty(true);
					// Now we have to find it...
					for (int u = 0; u < frmList.AllUniverses.Count; u++)
					{
						Universe universe = frmList.AllUniverses[u];
						for (int c = 0; c < universe.Controllers.Count; c++)
						{
							Controller controller = universe.Controllers[c];
							if (controller.ID == newID)
							{
								channel.Controller = controller;
								c = universe.Controllers.Count;
								u = frmList.AllUniverses.Count;
							}
						}
					}
					numOutput.Maximum = channel.Controller.OutputCount;
					//OutputChange();
					RefreshAddresses();
				}
			}

		}

		private void cboController_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (chanOriginal.Controller != null)
				{
					for (int u = 0; u < frmList.AllUniverses.Count; u++)
					{
						Universe uni = frmList.AllUniverses[u];
						for (int c = 0; c < uni.Controllers.Count; c++)
						{
							ListItem li = new ListItem(uni.Controllers[c].ToString(), uni.Controllers[c].ID);
							if (li.ID == chanOriginal.Controller.ID)
							{
								cboController.SelectedIndex = cboController.Items.IndexOf(li);
								c = uni.Controllers.Count; // Force exit loop
								u = frmList.AllUniverses.Count;
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
					if (channel.Controller.ID != newID)
					{
						changes |= CHANGE_CONTROLLER;
						MakeDirty(true);
						// Now we have to find it...
						for (int u = 0; u < frmList.AllUniverses.Count; u++)
						{
							Universe universe = frmList.AllUniverses[u];
							for (int c = 0; c < universe.Controllers.Count; c++)
							{
								Controller controller = universe.Controllers[c];
								if (controller.ID == newID)
								{
									channel.Controller = controller;
									c = universe.Controllers.Count;
									u = frmList.AllUniverses.Count;
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
			DialogResult = DialogResult.OK;
			if (isDirty)
			{
				SaveAndExit();
			}
			else
			{
				this.Hide(); ;
			}
		}

		private bool SaveAndExit()
		{
			userClose = true;
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
			DialogResult = DialogResult.Cancel;
			userClose = true;
			MakeDirty(false);
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
			prevWindowState = this.WindowState;
		}

		private void frmChannel_ResizeEnd(object sender, EventArgs e)
		{
		}

		private void frmChannel_Resize(object sender, EventArgs e)
		{

			/*// Did the window state change?
			if (prevWindowState == FormWindowState.Normal)
			{
				// Is it now minimized?
				if (this.WindowState == FormWindowState.Minimized)
				{
					// Minimize my owner/parent
					frmList.WindowState = FormWindowState.Minimized;
					// It should (?) minimize this form when the parent/owner is minimized
					// So set prevWindowState to minimized
					prevWindowState = FormWindowState.Minimized;
					// And set my own window state back to normal,
					// so that when the owner/parent is restored, I'll be normal and showing.

					this.WindowState = FormWindowState.Normal;
				}
			}
			if (picker != null)
			{
				if (picker.WindowState == FormWindowState.Minimized)
				{
					picker.WindowState = FormWindowState.Normal;
				}
			}
			*/
		}

		private void picColor_Click(object sender, EventArgs e)
		{
			picker = new frmColors();
			picker.color = channel.Color;
			DialogResult dr = picker.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				if (picker.isDirty)
				{
					channel.ColorName = picker.selectedName;
					SetColor(picker.selectedColor);
				}
			}
			picker.Dispose();
		}

		private void lblName_Click(object sender, EventArgs e)
		{

		}

		private void picRGBW_Click(object sender, EventArgs e)
		{

		}

		private void cboDevice_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				if (cboDevice.SelectedIndex != lastDeviceIndex)
				{
					MakeDirty(true);
					DeviceType device = (DeviceType)cboDevice.Items[cboDevice.SelectedIndex];
					lblDevice.Text = device.Comment;
				}
			}
		}

		private void cboDevice_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (chanOriginal.DeviceType != null)
				{
					for (int d = 0; d < cboDevice.Items.Count; d++)
					{
						DeviceType de = (DeviceType)cboDevice.Items[d];
						string dn = de.Name;
						if (de.ID == chanOriginal.DeviceType.ID)
						{
							cboDevice.SelectedIndex = d;
							d = cboDevice.Items.Count; // Force exit loop
						}
					}
				}
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if (cboDevice.SelectedIndex >= 0)
				{
					if (cboDevice.SelectedItem != null)
					{
						DeviceType device = (DeviceType)cboDevice.SelectedItem;
						if (device.Name != chanOriginal.DeviceType.Name)
						{
							//channel.ChannelType = (ChannelType)hcboDevic3.SelectedIndex;
							//string dn = device.Name;
							channel.DeviceType = device;
							changes |= CHANGE_TYPE;
							MakeDirty(true);
						}
					}
				}
			}
		}

		private void cboDevice_Leave(object sender, EventArgs e)
		{
			if (!loading)
			{
				if (cboDevice.SelectedIndex >= 0)
				{
					if (cboDevice.SelectedItem != null)
					{
						DeviceType device = (DeviceType)cboDevice.SelectedItem;
						if (device.Name != chanOriginal.DeviceType.Name)
						{
							//channel.ChannelType = (ChannelType)hcboDevic3.SelectedIndex;
							//string dn = device.Name;
							channel.DeviceType = device;
							changes |= CHANGE_TYPE;
							MakeDirty(true);
						}
					}
				}
			}
		}

		private void cboDevice_Enter(object sender, EventArgs e)
		{
			string tipText = "The type of device this channel is connected to";
			tipTool.SetToolTip(cboDevice, tipText);
			tipTool.SetToolTip(lblType, tipText);
		}

		public bool Editing
		{
			get
			{
				return editing;
			}
			set
			{
				editing = value;
				if (editing)
				{
					{
						adding = false;
					}
				}
			}
		}
		public bool Adding
		{
			get
			{
				return adding;
			}
			set
			{
				adding = value;
				if (adding)
				{
					editing = false;
				}
			}
		}



	} // End class frmChannel
}  // End namespace
