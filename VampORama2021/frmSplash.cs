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
	public partial class frmSplash : Form
	{
		//Timer tmr;
		//frmVamp vampForm;

		public frmSplash()
		{
			InitializeComponent();
		}

		private void frmSplash_Shown(object sender, EventArgs e)
		{
			//tmr = new Timer();
			//set time interval 3 sec
			//tmr.Interval = 3000;
			//starts the timer
			//tmr.Start();
			//tmr.Tick += tmr_Tick;
		}

		void tmr_Tick(object sender, EventArgs e)
		{
			//after 3 sec stop the timer
			//tmr.Stop();
			//display mainform
			//vampForm vf = new frmVamp();
			//vf.Show();
			//hide this form
			//this.Hide();

		}

		private void frmSplash_FormClosed(object sender, FormClosedEventArgs e)
		{
			//Application.Exit();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}
	}
}
