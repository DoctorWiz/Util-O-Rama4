﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileHelper;
using FormHelper;
using LOR4;

namespace UtilORama4
{
	public partial class frmSort : Form
	{
		private static Properties.Settings userSetttings = Properties.Settings.Default;
		private const string helpPage = "http://wizlights.com/utilorama/sortorama";

		public frmSort()
		{
			InitializeComponent();
		}

		private void frmSort_Load(object sender, EventArgs e)
		{
			this.RestoreView();
			GetUserSettings();
		}

		private void frmSort_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.SaveView();
			SaveUserSettings();
		}


		private void SaveUserSettings()
		{

		}

		private void GetUserSettings()
		{

		}

		public void ImBusy(bool busy)
		{
			if (busy)
			{
				this.Cursor = Cursors.WaitCursor;
				this.Enabled = false;
			}
			else
			{
				this.Cursor = Cursors.Default;
				this.Enabled = true;
			}
		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(helpPage);

		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			Form aboutBox = new frmAbout();
			//aboutBox.SetIcon(picAboutIcon.Image);
			//aboutBox = picAboutIcon.Image;
			aboutBox.ShowDialog(this);
			ImBusy(false);

		}

	}
}
