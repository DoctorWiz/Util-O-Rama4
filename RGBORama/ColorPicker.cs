using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class frmColorPicker : Form
	{
		public int RedPct = 0;
		public int GrnPct = 0;
		public int BluPct = 0;

		private bool ignoreChange = false;
		private int lastCustomIdx = -1;
		private PictureBox lastCustomColor = null;
		private int lastBasicIdx = -1;
		private PictureBox lastBasicColor = null;


		public frmColorPicker (int Red, int Grn, int Blu)
		{
			RedPct = Red;
			GrnPct = Grn;
			BluPct = Blu;
			ignoreChange = true;
			numRed.Value = RedPct;
			numGrn.Value = GrnPct;
			numBlu.Value = BluPct;
			ignoreChange = false;
			UpdateChoiceColor();

		}

		public frmColorPicker()
		{
			InitializeComponent();
		}

		private void UpdateChoiceColor()
		{
			int cr = (int)Math.Round(((decimal)(RedPct * 2.55F)));
			int cg = (int)Math.Round(((decimal)(GrnPct * 2.55F)));
			int cb = (int)Math.Round(((decimal)(BluPct * 2.55F)));
			Color c = Color.FromArgb(cr, cg, cb);
			picChoice.BackColor = c;
		}

		private void picBasicXY_Click(object sender, EventArgs e)
		{
			PictureBox selPic = (PictureBox)sender;
			ChooseColor(selPic.BackColor);
			lastBasicColor = selPic;
			lastCustomColor = null;
			if (lastCustomIdx >= 0)
			{
				btnAddToCustom.Enabled = true;
			}
			pnlBasic.Refresh();
		}

		private void picCustomnXY_Click(object sender, EventArgs e)
		{
			PictureBox selPic = (PictureBox)sender;
			ChooseColor(selPic.BackColor);
			lastCustomColor = selPic;
			lastBasicColor = null;
			string n = selPic.Name.Substring(selPic.Name.Length - 2);
			lastCustomIdx = int.Parse(n);
			btnAddToCustom.Enabled = false;
		}

		private void picColorBox_Click(object sender, EventArgs e)
		{

		}

		private void picColorBox_MouseMove(object sender, MouseEventArgs e)
		{

		}

		private void ChooseColor(Color c)
		{
			picChoice.BackColor = c;
			float rf = c.R / 2.55F;
			float gf = c.G / 2.55F;
			float bf = c.B / 2.55F;
			int ri = (int)Math.Round(rf);
			int gi = (int)Math.Round(gf);
			int bi = (int)Math.Round(bf);
			numRed.Value = ri;
			numGrn.Value = gi;
			numBlu.Value = bi;

			//numRed.Refresh();


		}

		private void num_ValueChanged(object sender, EventArgs e)
		{
			if (!ignoreChange)
			{
				RedPct = (int)numRed.Value;
				GrnPct = (int)numGrn.Value;
				BluPct = (int)numBlu.Value;
				UpdateChoiceColor();
				lastBasicColor = null;
				lastCustomColor = null;
				if (lastCustomIdx >= 0)
				{
					btnAddToCustom.Enabled = true;
				}
			}
		}

		private void btnAddToCustom_Click(object sender, EventArgs e)
		{
			if (lastCustomIdx >= 0)
			{
				string n = "picCustom" + lastCustomIdx.ToString("00");
				PictureBox c = (PictureBox)this.Controls[n];
				c.BackColor = picChoice.BackColor;
				n = "CustomColor" + lastCustomIdx.ToString("00");
				//Properties.Settings.Default[n] = picChoice.BackColor.ToArgb();
				//Properties.Settings.Default.Save();
			}
		}

		private void pnlBasic_Paint(object sender, PaintEventArgs e)
		{
			if (lastBasicColor != null)
			{
				Graphics g = e.Graphics;
				Pen p = new Pen(Color.Black, 1);
				Rectangle r = new Rectangle(lastBasicColor.Left - 1, lastBasicColor.Top - 1, lastBasicColor.Width + 2, lastBasicColor.Height + 2);
				g.DrawRectangle(p, r);

				r = new Rectangle(lastBasicColor.Left - 3, lastBasicColor.Top - 3, lastBasicColor.Width + 6, lastBasicColor.Height + 6);
				float[] d = { 1, 1 };
				p.DashPattern = d;
				g.DrawRectangle(p, r);

			}
		}

		private void pnlCustom_Paint(object sender, PaintEventArgs e)
		{
			if (lastCustomColor != null)
			{
				Graphics g = e.Graphics;
				Pen p = new Pen(Color.Black, 1);
				Rectangle r = new Rectangle(lastCustomColor.Left - 1, lastCustomColor.Top - 1, lastCustomColor.Width + 2, lastCustomColor.Height + 2);
				g.DrawRectangle(p, r);

				r = new Rectangle(lastCustomColor.Left - 3, lastCustomColor.Top - 3, lastCustomColor.Width + 6, lastCustomColor.Height + 6);
				float[] d = { 1, 1 };
				p.DashPattern = d;
				g.DrawRectangle(p, r);

			}

		}
	}
}

