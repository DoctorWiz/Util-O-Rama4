using System;
using System.Windows.Forms;

namespace UtilORama4
{
	public partial class frmOptions : Form
	{
		byte recentAction = 0;
		bool recentByName = false;
		private const byte ACTIONkeepFirst = 1;
		private const byte ACTIONuseSecond = 2;
		private const byte ACTIONkeepBoth = 3;
		private const byte ACTIONaddNumber = 4;

		private bool mergeTracks = true;
		private bool appendTracks = false;
		private bool mergeTracksByName = true;
		private bool mergeTracksByNumber = false;
		private bool mergeGroupsByName = true;
		private bool mergeGrids = true;
		private byte duplicateNameAction = ACTIONkeepFirst;
		private bool mergeEffects = false;

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
				chkGroupsByName.Checked = true;
				chkGroupsByName.Enabled = false;
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
				chkGroupsByName.Checked = false;
				chkGroupsByName.Enabled = false;
				optKeepBoth.Checked = true;
				grpChannels.Enabled = false;
				lblRecommend.Visible = false;
			}
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
					break;
			}
			chkMergeGrids.Checked = Properties.Settings.Default.MergeGrids;
			optMergeEffects.Checked = Properties.Settings.Default.MergeEffects;
			optInfoOnly.Checked = !optMergeEffects.Checked;
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
							} // if addNumber
						} // if keepBoth
					} // if useSecond
				} // if keepFirst
			} // if merge tracks
			Properties.Settings.Default.MergeGrids = chkMergeGrids.Checked;
			Properties.Settings.Default.MergeEffects = optMergeEffects.Checked;
			Properties.Settings.Default.Save();
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

		private void optMergeTracks_CheckedChanged(object sender, EventArgs e)
		{
			mergeTracks = optMergeTracks.Checked;
			appendTracks = !mergeTracks;
			chkGroupsByName.Checked = mergeTracks;
			lblRecommend.Visible = !mergeTracks;
			optKeepFirst.Enabled = mergeTracks;
			optUseSecond.Enabled = mergeTracks;
			optKeepBoth.Enabled = mergeTracks;
			optAddNumber.Enabled = mergeTracks;
			if (mergeTracks)
			{
				switch(duplicateNameAction)
				{
					case ACTIONkeepFirst:
						optKeepFirst.Checked = true;
						break;
					case ACTIONuseSecond:
						optUseSecond.Checked = true;
						break;
					case ACTIONkeepBoth:
						optKeepBoth.Checked = true;
						break;
					case ACTIONaddNumber:
						optAddNumber.Checked = true;
						break;
				}
			}
			else
			{
				optKeepBoth.Checked = true;
			}
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

		private void option_CheckedChanged(object sender, EventArgs e)
		{
			if (mergeTracks)
			{
				if (optKeepFirst.Checked)
				{
					duplicateNameAction = ACTIONkeepFirst;
					recentAction = ACTIONkeepFirst;
				}
				if (optUseSecond.Checked)
				{
					duplicateNameAction = ACTIONuseSecond;
					recentAction = ACTIONuseSecond;
				}

			}
		}
	}
}
