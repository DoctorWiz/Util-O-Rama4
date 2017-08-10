using System;
using System.Windows.Forms;

namespace MergeORama
{
	public partial class frmOptions : Form
	{
		byte recentAction = 0;
		bool recentByName = false;

		public frmOptions()
		{
			InitializeComponent();
		}

		private void Options_Load(object sender, EventArgs e)
		{
			SetOptions();
			cmdOK.Focus();
		}

		private void SetOptions()
		{
			if (Properties.Settings.Default.MergeTracks)
			{
				optMergeTracks.Checked = true;
				optMergeTracksByName.Enabled = true;
				optMergeTracksByNumber.Enabled = true;
				chkMergeChannelsByName.Checked = true;
				chkMergeChannelsByName.Enabled = false;
				recentByName = Properties.Settings.Default.MergeTracksByName;
				if (recentByName)
				{
					optMergeTracksByName.Checked = true;
				}
				else
				{
					optMergeTracksByNumber.Checked = true;
				}
			}
			else
			{
				optAddTracks.Checked = true;
				optMergeTracksByName.Enabled = false;
				optMergeTracksByNumber.Enabled = false;
				optMergeTracksByName.Checked = false;
				optMergeTracksByNumber.Checked = false;
				chkMergeChannelsByName.Checked = false;
				chkMergeChannelsByName.Enabled = false;
				optKeepBoth.Checked = true;
				pnlDuplicateAction.Enabled = false;
				lblRecommend.Visible = false;
			}
			txtNumberFormat.Text = Properties.Settings.Default.AddNumberFormat;
			txtNumberFormat.Enabled = false;
			recentAction = Properties.Settings.Default.DuplicateNameAction;
			switch(recentAction)
			{
				case 1:
					optKeepFirst.Checked = true;
					break;
				case 2:
					optUseSecond.Checked = true;
					break;
				case 3:
					optKeepBoth.Checked = true;
					break;
				case 4:
					optAddNumber.Checked = true;
					txtNumberFormat.Enabled = true;
					break;
			}
		}

		private void SaveOptions()
		{
			Properties.Settings.Default.MergeTracks = optMergeTracks.Checked;
			if (optMergeTracks.Checked)
			{
				Properties.Settings.Default.MergeTracksByName = optMergeTracksByName.Checked;
				if (optKeepFirst.Checked)
				{
					Properties.Settings.Default.DuplicateNameAction = 1;
				}
				else
				{
					if (optUseSecond.Checked)
					{
						Properties.Settings.Default.DuplicateNameAction = 2;
					}
					else
					{
						if (optKeepBoth.Checked)
						{
							Properties.Settings.Default.DuplicateNameAction = 3;
						}
						else
						{
							if (optAddNumber.Checked)
							{
								Properties.Settings.Default.DuplicateNameAction = 4;
								Properties.Settings.Default.AddNumberFormat = txtNumberFormat.Text;
							} // if addNumber
						} // if keepBoth
					} // if useSecond
				} // if keepFirst
			} // if merge tracks
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			SaveOptions();
			DialogResult = DialogResult.OK;
			//Close();
			Hide();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			//Close();
			Hide();
		}

		private void txtNumberFormat_TextChanged(object sender, EventArgs e)
		{
			bool isOK = false;
			string txt = txtNumberFormat.Text;
			int pos = txt.IndexOf("#");
			if (pos >= 0)
			{
				isOK = true;
				int pos2 = txt.IndexOf("#", pos + 1);
				if (pos2 > pos)
				{
					isOK = false;
				}
				else
				{
					pos = txt.IndexOf("\"");
					if (pos >= 0)
					{
						isOK = false;
					}
					else
					{
						pos = txt.IndexOf("<");
						if (pos>=0)
						{
							isOK = false;
						}
						else
						{
							pos = txt.IndexOf(">");
							if (pos>=0 )
							{
								isOK = false;
							}
						}
					}
				}
			}
			cmdOK.Enabled = isOK;
		}

		private void optMergeTracks_CheckedChanged(object sender, EventArgs e)
		{
			byte temp = recentAction;
			optMergeTracksByName.Enabled = true;
			optMergeTracksByNumber.Enabled = true;
			pnlDuplicateAction.Enabled = true;
			chkMergeChannelsByName.Checked = true;
			lblRecommend.Visible = true;
			
			switch (recentAction)
			{
				case 1:
					optKeepFirst.Checked = true;
					break;
				case 2:
					optUseSecond.Checked = true;
					break;
				case 3:
					optKeepBoth.Checked = true;
					break;
				case 4:
					optAddNumber.Checked = true;
					txtNumberFormat.Enabled = true;
					break;
			}
			if (recentByName)
			{
				optMergeTracksByName.Checked = true;
			}
			else
			{
				optMergeTracksByNumber.Checked = true;
			}
			recentAction = temp;

		}

		private void optAddTracks_CheckedChanged(object sender, EventArgs e)
		{
			byte temp = recentAction;
			optMergeTracksByName.Enabled = false;
			optMergeTracksByNumber.Enabled = false;
			pnlDuplicateAction.Enabled = false;
			chkMergeChannelsByName.Checked = false;
			lblRecommend.Visible = false;
			optKeepBoth.Checked = true;
			recentAction = temp;
		}

		private void optAddNumber_CheckedChanged(object sender, EventArgs e)
		{
			txtNumberFormat.Enabled = true;
			recentAction = 4;
		}

		private void optKeepBoth_CheckedChanged(object sender, EventArgs e)
		{
			txtNumberFormat.Enabled = false;
			recentAction = 3;
		}

		private void optUseSecond_CheckedChanged(object sender, EventArgs e)
		{
			txtNumberFormat.Enabled = false;
			recentAction = 2;
		}

		private void optKeepFirst_CheckedChanged(object sender, EventArgs e)
		{
			txtNumberFormat.Enabled = false;
			recentAction = 1;
		}

		private void optMergeTracksByName_CheckedChanged(object sender, EventArgs e)
		{
			recentByName = true;
		}

		private void optMergeTracksByNumber_CheckedChanged(object sender, EventArgs e)
		{
			recentByName = false;
		}

		private void frmOptions_ResizeBegin(object sender, EventArgs e)
		{

		}

		private void frmOptions_ResizeEnd(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				//this.Parent.WindowState = FormWindowState.Minimized;
			}
			if (WindowState == FormWindowState.Normal)
			{
				//this.Parent.WindowState = FormWindowState.Normal;
			}
		}
	}
}
