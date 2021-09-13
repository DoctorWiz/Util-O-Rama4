using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LORUtils4; using FileHelper;

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

		private LORChannel4 SelectControlChannel()
		{
			return SelectControlChannel(LORRGBChild4.None);
		}

		private LORChannel4 SelectControlChannel(LORRGBChild4 rgbAllowed)
		{
			LORChannel4 ch = null;


			return ch;
		}


	}
}
