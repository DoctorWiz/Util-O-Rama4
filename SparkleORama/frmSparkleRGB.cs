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

		private LOR4Channel SelectControlChannel()
		{
			return SelectControlChannel(LORRGBChild4.None);
		}

		private LOR4Channel SelectControlChannel(LORRGBChild4 rgbAllowed)
		{
			LOR4Channel ch = null;


			return ch;
		}


	}
}
