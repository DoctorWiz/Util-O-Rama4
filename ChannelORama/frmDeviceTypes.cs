using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UtilORama4
{
	public partial class frmDeviceTypes : Form
	{
		public bool isDirty = false;
		private frmList Owner = null;
		private List<DeviceTypes> NewList = new List<DeviceTypes>();
		public frmDeviceTypes()
		{
			InitializeComponent();
		}

		public frmDeviceTypes(frmList owner)
		{
			Owner = owner;
			InitializeComponent();
			Load(owner.DeviceTypes);
		}

		public void Load(List<DeviceTypes> DeviceTypes)
		{
			foreach (DeviceTypes DeviceType in DeviceTypes)
			{
				NewList.Add(DeviceType);
				lstDevices.Items.Add(DeviceType);
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

		}
	}
}
