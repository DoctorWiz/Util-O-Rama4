using FileHelper;
using Syncfusion.Windows.Forms.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace UtilORama4
{
	public partial class frmDeviceTypes : Form
	{
		public bool isDirty = false;
		private frmList Owner = null;
		//private List<DeviceType> NewList = new List<DeviceType>();
		private DeviceType selectedDevice = null;
		public frmDeviceTypes()
		{
			InitializeComponent();
		}

		public frmDeviceTypes(frmList owner)
		{
			Owner = owner;
			InitializeComponent();
			LoadDevices(frmList.DeviceTypes);
		}

		public void LoadDevices(List<DeviceType> DeviceTypes)
		{
			// Start with index 1, not 0, because index 0 is "Undefined" and is immutable
			for (int dt = 1; dt < DeviceTypes.Count; dt++)
			{
				DeviceType device = DeviceTypes[dt];
				//NewList.Add(device);
				lstDevices.Items.Add(device);
			}
			lstDevices.SelectedIndex = 0;
			btnDown.Enabled = true;
			lstDevices.Select();
		}

		public bool MakeDirty(bool dirty)
		{
			bool ret = isDirty;
			if (dirty != isDirty)
			{
				if (dirty)
				{
					btnOK.Enabled = true;
				}
				else
				{
				}
				isDirty = dirty;
			}
			return ret;
		}


		private void btnOK_Click(object sender, EventArgs e)
		{
			if (isDirty)
			{
				// Create a NEW list
				List<DeviceType> NewList = new List<DeviceType>();
				// Copy the first item from the old list, which is the "Undefined" item
				NewList.Add(frmList.DeviceTypes[0]);
				// Copy the rest from the list box, whatever is there, in whatever order
				for (int dt = 1; dt < lstDevices.Items.Count; dt++)
				{
					NewList.Add((DeviceType)lstDevices.Items[dt]);
				}

			}
			DialogResult = DialogResult.OK;
			this.Hide();

		}

		private void lstDevices_SelectedIndexChanged(object sender, EventArgs e)
		{
			selectedDevice = (DeviceType)lstDevices.Items[lstDevices.SelectedIndex];
			string ttxt = "";
			if (lstDevices.SelectedIndex < 1)
			{
				btnUp.Enabled = false;
			}
			else
			{
				btnUp.Enabled = true;
				ttxt = "Move " + selectedDevice.Name + " above ";
				DeviceType d2 = (DeviceType)lstDevices.Items[lstDevices.SelectedIndex - 1];
				ttxt += d2.Name + ".";
				tipTool.SetToolTip(btnUp, ttxt);
			}
			if (lstDevices.SelectedIndex < lstDevices.Items.Count - 1)
			{
				btnDown.Enabled = false;
			}
			else
			{
				btnDown.Enabled = true;
				ttxt = "Move " + selectedDevice.Name + " below ";
				DeviceType d2 = (DeviceType)lstDevices.Items[lstDevices.SelectedIndex + 1];
				ttxt += d2.Name + ".";
				tipTool.SetToolTip(btnDown, ttxt);
			}
			if (selectedDevice.UsedByCount > 0)
			{
				btnDelete.Enabled = false;
				ttxt = selectedDevice.Name + " cannot be deleted because it is used by " + selectedDevice.UsedByCount.ToString() + " channels.";
				tipTool.SetToolTip(btnDelete, ttxt);
				ttxt = "The following channels are of type " + selectedDevice.Name + " type:\r\n";
				ttxt += selectedDevice.UsedByChannels;
				tipTool.SetToolTip(lstDevices, ttxt);
			}
			else
			{
				btnDelete.Enabled = true;
				ttxt = "Delete unused " + selectedDevice.Name + " type.";
				tipTool.SetToolTip(btnDelete, ttxt);
				ttxt = "No channels are of type " + selectedDevice.Name + ".";
				tipTool.SetToolTip(lstDevices, ttxt);
			}
		} // End Selection Changed

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (selectedDevice == null)
			{
				if (Fyle.isWiz)
				{
					string WTF = "Trying to delete when nothing is selected";
					Debugger.Break();
				}
				else
				{
					frmInput inForm = new frmInput();
					inForm.txtName.Text = selectedDevice.Name;
					inForm.txtComment.Text = selectedDevice.Comment;
					DialogResult dr = inForm.Show(this);
					if (dr == DialogResult.OK)
					{
						DeviceType device = new DeviceType();
						device.Name = inForm.txtName.Text.Trim();
						device.Comment = inForm.txtComment.Text.Trim();
						// Find the next unused ID
						int id = 1;
						for (int d = 1; d < lstDevices.Items.Count; d++)
						{
							DeviceType dx = (DeviceType)lstDevices.Items[d];
							id = Math.Max(id, dx.ID);
						}
						device.ID = id;
						lstDevices.Items.Insert(lstDevices.SelectedIndex, device);
					}
					inForm.Dispose();
				}
			}
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			if (selectedDevice == null)
			{
				if (Fyle.isWiz)
				{
					string WTF = "Trying to move up when nothing is selected";
					Debugger.Break();
				}
			}
			else
			{
				int si = lstDevices.SelectedIndex;
				// Use Tuple Deconstruction to perform the swap
				(lstDevices.Items[si], lstDevices.Items[si - 1]) = (lstDevices.Items[si - 1], lstDevices.Items[si]);
				MakeDirty(true);
			}
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			if (selectedDevice == null)
			{
				if (Fyle.isWiz)
				{
					string WTF = "Trying to move up when nothing is selected";
					Debugger.Break();
				}
			}
			else
			{
				int si = lstDevices.SelectedIndex;
				// Use Tuple Deconstruction to perform the swap
				(lstDevices.Items[si], lstDevices.Items[si + 1]) = (lstDevices.Items[si + 1], lstDevices.Items[si]);
				MakeDirty(true);
			}

		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (selectedDevice == null)
			{
				if (Fyle.isWiz)
				{
					string WTF = "Trying to delete when nothing is selected";
					Debugger.Break();
				}
				else
				{
					frmInput inForm = new frmInput();
					inForm.txtName.Text = selectedDevice.Name;
					inForm.txtComment.Text = selectedDevice.Comment;
					DialogResult dr = inForm.Show(this);
					if (dr == DialogResult.OK)
					{
						selectedDevice.Name = inForm.txtName.Text.Trim();
						selectedDevice.Comment = inForm.txtComment.Text.Trim();
					}
					inForm.Dispose();
				}
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (selectedDevice == null)
			{
				if (Fyle.isWiz)
				{
					string WTF = "Trying to delete when nothing is selected";
					Debugger.Break();
				}
				else
				{
					if (selectedDevice.UsedByCount > 0)
					{
						string WTF = "Trying to delete when nothing is selected";
						Debugger.Break();
					}
					else
					{
						lstDevices.Items.RemoveAt(lstDevices.SelectedIndex);
						MakeDirty(true);
					}
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			string msg = "The Device Types list has been modified.  Save your changes?";
			DialogResult dr = MessageBox.Show(this, msg, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
			if (dr == DialogResult.No)
			{
				this.Hide();
			}
			else if (dr == DialogResult.Yes)
			{
				btnOK_Click(this, null);
			}
			// Else if dialog result is Cancel, do nothing, leave this dialog open

		}

		private void frmDeviceTypes_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				string msg = "The Device Types list has been modified.  Save your changes?";
				DialogResult dr = MessageBox.Show(this, msg, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
				if (dr == DialogResult.No)
				{
					DialogResult = DialogResult.OK;
					this.Hide();
				}
				else if (dr == DialogResult.Yes)
				{
					btnOK_Click(this, null);
				}
				else if (dr == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
			}
		}

		private void frmDeviceTypes_Load(object sender, EventArgs e)
		{
			// Limit minimum and maximum sizes, taking the display magnification level into account
			float mag = frmList.magnification;
			this.MinimumSize = new System.Drawing.Size((int)(300 * mag), (int)(500 * mag));
			this.MaximumSize = new System.Drawing.Size((int)(700 * mag), (int)(1000 * mag));
		}

		private void lstDevices_MouseMove(object sender, MouseEventArgs e)
		{
			// 1. Get the zero-based index of the item at the mouse coordinates
			int index = lstDevices.IndexFromPoint(e.Location);

			// 2. Check if the mouse is actually over an item (index will be >= 0)
			if (index != ListBox.NoMatches) // NoMatches is -1
			{
				// Example: Get the text of the item under the mouse
				DeviceType device = (DeviceType)lstDevices.Items[index];
				string ttxt = device.Comment;
				tipTool.SetToolTip(lstDevices, ttxt);
			}
			else
			{
				//this.Text = "Not over any item";
				string ttxt = "List of Device Types, in the order they will be displayed.";
				tipTool.SetToolTip(lstDevices,ttxt);	
			}
		
		}
	} // End form
} // End namespace
