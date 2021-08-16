using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LORUtils; using FileHelper;
using xUtilities;

namespace UtilORama4
{
	public partial class frmOptions : Form
	{
		public int maxTime = 0;
		public int beats = 4;
		public int timeSignature = 4;

		private byte[] x44 = { 1, 4, 8, 16 };
		private byte[] x34 = { 1, 3, 6, 9 };
		private int n44 = 0;
		private int n34 = 0;

		public frmOptions()
		{
			InitializeComponent();
		}

		private void txtStartTime_KeyPress(object sender, KeyPressEventArgs e)
		{
			bool OK = ((e.KeyChar >= '0') && (e.KeyChar <= '9'));
			if (e.KeyChar == ':')
			{
				if (txtStartTime.Text.IndexOf(':') < 0) OK = true;
			}
			if (e.KeyChar == '.')
			{
				if (txtStartTime.Text.IndexOf('.') < 0) OK = true;
			}
			e.Handled = !OK;
		}

		private void txtEndTime_KeyPress(object sender, KeyPressEventArgs e)
		{
			bool OK = ((e.KeyChar >= '0') && (e.KeyChar <= '9'));
			if (e.KeyChar == ':')
			{
				if (txtEndTime.Text.IndexOf(':') < 0) OK = true;
			}
			if (e.KeyChar == '.')
			{
				if (txtEndTime.Text.IndexOf('.') < 0) OK = true;
			}
			e.Handled = !OK;
		}

		private void txtStartTime_Validating(object sender, CancelEventArgs e)
		{
			int cst = utils.DecodeTime(txtStartTime.Text);
			if ((cst < 0) || (cst > maxTime)) e.Cancel = true;
			int cet = utils.DecodeTime(txtEndTime.Text);
			if (cst < cet) e.Cancel = true;
			if (e.Cancel)
			{
				txtStartTime.ForeColor = System.Drawing.Color.Red;
			}
			else
			{
				txtStartTime.ForeColor = System.Drawing.SystemColors.WindowText;
			}
		}

		private void txtStartTime_Validated(object sender, EventArgs e)
		{
			int cst = utils.DecodeTime(txtStartTime.Text);
			txtStartTime.Text = utils.FormatTime(cst);
		}

		private void txtEndTime_Validated(object sender, EventArgs e)
		{
			int cet = utils.DecodeTime(txtEndTime.Text);
			txtEndTime.Text = utils.FormatTime(cet);
		}

		private void txtEndTime_Validating(object sender, CancelEventArgs e)
		{
			int cet = utils.DecodeTime(txtEndTime.Text);
			if ((cet < 0) || (cet > maxTime)) e.Cancel = true;
			int cst = utils.DecodeTime(txtStartTime.Text);
			if (cst < cet) e.Cancel = true;
			if (e.Cancel)
			{
				txtEndTime.ForeColor = System.Drawing.Color.Red;
			}
			else
			{
				txtEndTime.ForeColor = System.Drawing.SystemColors.WindowText;
			}

		}

		private void txtTrackBeatX_TextChanged(object sender, EventArgs e)
		{

		}

		private void vscTrackBeatX_Scroll(object sender, ScrollEventArgs e)
		{
			if (timeSignature == 3)
			{
				beats = x34[vscTrackBeatX.Value];
			}
			else
			{
				beats = x44[vscTrackBeatX.Value];
			}
			txtTrackBeatX.Text = beats.ToString();

		}

		private void swTrackBeat_CheckedChanged(object sender, EventArgs e)
		{
			if (swTrackBeat.Checked) timeSignature = 3; else timeSignature = 4;
		}
	}
}
