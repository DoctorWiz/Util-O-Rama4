using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UtilORama4
{
	public partial class frmOutputLog : Form
	{
		Form ownerForm = null;
		public frmOutputLog()
		{
			InitializeComponent();
		}

		public frmOutputLog(Form parentForm)
		{
			InitializeComponent();
			ownerForm = parentForm;
		}
		public string LogText
		{
			get
			{
				return txtOutput.Text;
			}
			set
			{
				txtOutput.Text = value;
				txtOutput.SelectionStart = txtOutput.TextLength;
				txtOutput.ScrollToCaret();
			}
		}

		public bool Done
		{
			get
			{
				return btnClose.Enabled;
			}
			set
			{
				btnClose.Enabled = value;
				btnSaveLog.Enabled = value;
				if (value)
				{
					this.Cursor = Cursors.Default;
				}
				else
				{
					this.Cursor = Cursors.WaitCursor;
				}
			}
		}
		private void frmOutputLog_ResizeEnd(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				txtOutput.Height = ClientSize.Height - 40;
				btnClose.Top = ClientSize.Height - 30;
				btnClose.Left = ClientSize.Width / 2 - btnClose.Width - 16;
				btnSaveLog.Top = ClientSize.Height - 30;
				btnSaveLog.Left = ClientSize.Width / 2 + 16;
				string t = btnClose.Left.ToString() + ", " + btnSaveLog.Left.ToString();
				lblDebug.Text = t;
				lblDebug.Top = btnClose.Top;
			}
			if (this.WindowState == FormWindowState.Minimized)
			{
				// Handled in [regular] Resize event
				//ownerForm.WindowState = FormWindowState.Minimized;
			}
		}

		private void frmOutputLog_Load(object sender, EventArgs e)
		{

		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			//this.Hide();
			this.Close(); //?
		}

		private void frmOutputLog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!btnClose.Enabled)
			{
				e.Cancel = true;
			}
			// Do Nothing?
		}

		private void btnSaveLog_Click(object sender, EventArgs e)
		{
			dlgSaveFile.Title = "Save Output Log File As...";
			dlgSaveFile.InitialDirectory = "shell:::{679F85CB-0220-4080-B29B-5540CC05AAB6}";
			dlgSaveFile.Filter = "Log Files (*.log)|*.log|Text Files (*.txt)|*.txt";
			dlgSaveFile.FilterIndex = 1;
			dlgSaveFile.DefaultExt = ".log";
			dlgSaveFile.FileName = "Sonic Annotator Output.log";
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.SupportMultiDottedExtensions = true;
			dlgSaveFile.ValidateNames = true;
			//newFileIn = Path.GetFileNameWithoutExtension(fileCurrent) + " Part " + member.ToString(); // + ext;
			//newFileIn = "Part " + member.ToString() + " of " + Path.GetFileNameWithoutExtension(fileCurrent);
			//newFileIn = "Part Mother Fucker!!";
			//dlgSaveFile.FileName = initFile;
			DialogResult result = dlgSaveFile.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				StreamWriter writer = new StreamWriter(dlgSaveFile.FileName);
				writer.Write(txtOutput.Text);
				writer.Close();
			}
		}

		private void frmOutputLog_Resize(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				// Ignore for now, handle at ResizeEnd
			}
			if (this.WindowState == FormWindowState.Minimized)
			{
				ownerForm.WindowState = FormWindowState.Minimized;
			}

		}
	}
}
