using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LORUtils;

namespace SparkleORama
{
	public partial class frmSparkle : Form
	{
		public frmSparkle()
		{
			InitializeComponent();
		}

		private void frmSparkle_Load(object sender, EventArgs e)
		{

		}

		private Channel SelectControlChannel()
		{
			return SelectControlChannel(RGBchild.None);
		}

		private Channel SelectControlChannel(RGBchild rgbAllowed)
		{
			Channel ch = null;


			return ch;
		}


	}
}
