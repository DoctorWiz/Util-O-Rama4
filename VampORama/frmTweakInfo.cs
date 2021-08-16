using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xUtilities;
using LORUtils; using FileHelper;

namespace UtilORama4
{
	public partial class frmTweakInfo : Form
	{
		public frmTweakInfo()
		{
			InitializeComponent();
			string xooo = xUtils.ShowDirectory;
			string looo = utils.DefaultSequencesPath;
			if ((xooo.Length > 3) && (looo.Length<3))
			{
				this.linkProgram.Text = "C:\\Program Files\\xUtils\\Vamperizer";
			}
		}

		private void linkPlugins_Click(object sender, EventArgs e)
		{
			// Go to Vamp Plugins Web page
			System.Diagnostics.Process.Start("https://vamp-plugins.org/plugin-doc/qm-vamp-plugins.html#qm-onsetdetector");
		}

		private void linkProgram_Click(object sender, EventArgs e)
		{
			// Go to Program Folder
			string homedir = AppDomain.CurrentDomain.BaseDirectory;
			System.Diagnostics.Process.Start(homedir);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void frmTweakInfo_Load(object sender, EventArgs e)
		{
			linkProgram.Text = AppDomain.CurrentDomain.BaseDirectory;
			lblFolder.Left = linkProgram.Left + linkProgram.Width;
		}
	}
}
