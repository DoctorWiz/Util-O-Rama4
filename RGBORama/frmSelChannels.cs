using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LORUtils;


namespace RGBORama
{
	public partial class frmChannels : Form
	{
		public Sequence4 seq = null;
		List<List<TreeNode>> nodeList = new List<List<TreeNode>>();


		public frmChannels()
		{
			InitializeComponent();
		}

		public frmChannels(Sequence4 theSequence)
		{
			InitializeComponent();
			seq = theSequence;
			utils.TreeFillChannels(treeChannels, seq, nodeList, false, false);
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			this.Hide();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Hide();
		}

		private void frmChannels_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult = DialogResult.Abort;
			e.Cancel = true;
			this.Hide();
		}
	}
}
