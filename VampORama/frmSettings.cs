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
	public partial class frmSettings : Form
	{
		public frmSettings()
		{
			InitializeComponent();
		}

		public static byte SHOWtimesBeats = 1;
		public static byte SHOWonsets = 2;
		public static byte SHOWPoly = 3;
		public static byte SHOWspectro = 4;
		public static byte SHOWtrackBeats = 5;
		public static byte SHOWchroma = 6;
		public static byte SHOWfooo = 7;
		public static byte SHOWsave = 50;
		public static byte SHOWannotatorIzer = 98;
		public static byte SHOWannotatorORama = 99;

		private const int TABIDXtimess = 0;
		private const int TABIDXtracks = 1;
		private const int TABIDXsave = 2;
		private const string TABKEYtimess = "tabGrids";
		private const string TABKEYtracks = "tabTracks";
		private const string TABKEYsave = "tabSave";
		private const string TABKEYpoly = "tabPoly";
		private const string TABKEYspectro = "tabSpectro";
		private const string TABKEYonsets = "tabOnsets";
		private const string TABKEYbeats = "tabBeats";
		private const string TABKEYchroma = "tabChroma";
		private const string TABKEYsegment = "tabSegment";



		public bool useRampsPoly = false;
		public bool useOctaveGrouping = true;
		public int timesBeatsX = 4;
		public int trackBeatsX = 4;
		public int timeSignature = 4; // 3 = 3/4 time, 4 = 4/4 time
		public bool useRampsBeats = false;
		public byte useSaveFormat = frmVamp.SAVEmixedDisplay;
		public DialogResult closeMode = DialogResult.Cancel;
		private int initShowMode;
		private int curShowMode;
		private byte[] x44 = { 1, 4, 8, 16 };
		private byte[] x34 = { 1, 3, 6, 9 };
		private int n44 = 0;
		private int n34 = 0;

		private void frmSettings_Load(object sender, EventArgs e)
		{
			//this.Width = 322;
		}

		public void InitForm(byte showWhich)
		{
			// the Grid-Beats group has been set to the perfect size
			// set the others to match
			grpGridsOnsets.Size = grpGridsBeats.Size;
			grpTrackBeats.Size = grpGridsBeats.Size;
			grpTrackPoly.Size = grpGridsBeats.Size;
			grpTrackSpectro.Size = grpGridsBeats.Size;
			this.Width = 380;
			this.Height = 400;
			cmdCancel.Left = this.ClientSize.Width - cmdCancel.Width - 4;
			cmdOK.Left = cmdCancel.Left - cmdOK.Width - 10;





			//SetUseRamps(useRampsPoly);
			//if (saveFormat == frmVamp.SAVEmixedDisplay) optMixedDisplay.Checked = true;
			//if (saveFormat == frmVamp.SAVEcrgDisplay) optCRGDisplay.Checked = true;
			//if (saveFormat == frmVamp.SAVEcrgAlpha) optCRGAlpha.Checked = true;

			SetShowMode(showWhich);
			SetTimeSignature(timeSignature);
			initShowMode = showWhich;

		}

		private void SetShowMode(byte newMode)
		{
			string appPath = Path.GetDirectoryName(Application.ExecutablePath);
			if ((newMode == SHOWannotatorIzer) || (newMode == SHOWannotatorORama))
			{
				tabCategory.Visible = false;
				tabSubcategory.Visible = false;
				grpGridsBeats.Visible = false;
				grpTrackPoly.Visible = false;
				grpSaveFormat.Visible = false;
				brBrowserMsg.Left = 10;
				brBrowserMsg.Top = 10;
				cmdCancel.Left = brBrowserMsg.Left + brBrowserMsg.Width - cmdCancel.Width;
				cmdOK.Left = cmdCancel.Left - cmdOK.Width - 5;
				cmdOK.Top = brBrowserMsg.Top + brBrowserMsg.Height + 10;
				cmdCancel.Top = cmdOK.Top;
				int x = this.Width - this.ClientSize.Width;
				int w = brBrowserMsg.Width + 20 + x;
				int y = this.Height - this.ClientSize.Height;
				int h = brBrowserMsg.Height + cmdOK.Height + 43 + y;
				this.Size = new System.Drawing.Size(w, h);
			}
			if (newMode == SHOWannotatorIzer)
			{
				brBrowserMsg.Navigate(appPath + "\\NeedSonicAnnotatorIzer.htm");
			}
			if (newMode == SHOWannotatorORama)
			{
				brBrowserMsg.Navigate(appPath + "\\NeedSonicAnnotatorORama.htm");
			}


			if (newMode < SHOWannotatorIzer)
			{
				Point homePoint = new Point(0, 6);

				// Turn ALL of them off
				grpGridsBeats.Visible = false;
				grpGridsOnsets.Visible = false;
				grpSaveFormat.Visible = false;
				grpTrackBeats.Visible = false;
				grpTrackPoly.Visible = false;
				grpTrackSpectro.Visible = false;
				grpGridsBeats.Enabled = false;
				grpGridsOnsets.Enabled = false;
				grpSaveFormat.Enabled = false;
				grpTrackBeats.Visible = false;
				grpTrackPoly.Enabled = false;
				grpTrackSpectro.Enabled = false;

				// Then turn on just the one we need

				if (newMode == SHOWtimesBeats)
				{
					tabSubcategory.TabPages.RemoveByKey(TABKEYpoly);
					tabSubcategory.TabPages.RemoveByKey(TABKEYspectro);
					if (!tabSubcategory.TabPages.ContainsKey(TABKEYonsets))
					{
						tabSubcategory.TabPages.Add(tabOnsets);
					}


					//tabCategory.SelectTab(TABKEYtimess);
					//tabCategory.SelectTab(0);
					tabSubcategory.Parent = tabGrids;
					tabSubcategory.Visible = true;
					//int tp = tabSubcategory.TabPages.IndexOfKey (TABNAMEbeats);
					tabSubcategory.SelectTab(TABKEYbeats);
					grpGridsBeats.Parent = tabBeats;
					grpGridsBeats.Location = homePoint;
					grpGridsBeats.Enabled = true;
					grpGridsBeats.Visible = true;
					tabBeats.Show();
					tabGrids.Show();
					SetUseRamps(useRampsBeats);
				}
				if (newMode == SHOWtrackBeats)
				{
					tabSubcategory.TabPages.RemoveByKey(TABKEYonsets);
					if (!tabSubcategory.TabPages.ContainsKey(TABKEYspectro))
					{
						tabSubcategory.TabPages.Add(tabSpectro);
					}
					if (!tabSubcategory.TabPages.ContainsKey(TABKEYpoly))
					{
						tabSubcategory.TabPages.Add(tabPoly);
					}
					tabSubcategory.Parent = tabTracks;
					tabSubcategory.Visible = true;
					grpTrackBeats.Parent = tabBeats;
					grpTrackBeats.Location = homePoint;
					grpTrackBeats.Visible = true;
					grpTrackBeats.Enabled = true;
					tabBeats.Show();
					tabTracks.Show();
				}

				if (newMode == SHOWonsets)
				{
					tabSubcategory.TabPages.RemoveByKey("tabPoly");
					tabSubcategory.TabPages.RemoveByKey("tabSpectro");
					if (!tabSubcategory.TabPages.ContainsKey("tabNoteOnsets"))
					{
						tabSubcategory.TabPages.Add(tabOnsets);
					}
					tabSubcategory.Parent = tabGrids;
					tabSubcategory.Visible = true;
					grpGridsOnsets.Parent = tabOnsets;
					grpGridsOnsets.Location = homePoint;
					grpGridsOnsets.Visible = true;
					grpGridsOnsets.Enabled = true;
					tabOnsets.Show();
					tabGrids.Show();
				}

				if (newMode == SHOWPoly)
				{
					tabSubcategory.TabPages.RemoveByKey("tabNoteOnsets");
					if (!tabSubcategory.TabPages.ContainsKey("tabSpectro"))
					{
						tabSubcategory.TabPages.Add(tabSpectro);
					}
					if (!tabSubcategory.TabPages.ContainsKey("tabPoly"))
					{
						tabSubcategory.TabPages.Add(tabPoly);
					}
					tabSubcategory.Parent = tabTracks;
					tabSubcategory.Visible = true;
					grpTrackPoly.Parent = tabPoly;
					grpTrackPoly.Location = homePoint;
					grpTrackPoly.Enabled = true;
					grpTrackPoly.Visible = true;
					tabPoly.Show();
					tabTracks.Show();
					SetUseRamps(useRampsPoly);
					//checkPolyOctaveGrouping.Checked = useOctaveGrouping;
				}
				if (newMode == SHOWspectro)
				{
					tabSubcategory.TabPages.RemoveByKey("tabNoteOnsets");
					if (!tabSubcategory.TabPages.ContainsKey("tabSpectro"))
					{
						tabSubcategory.TabPages.Add(tabSpectro);
					}
					if (!tabSubcategory.TabPages.ContainsKey("tabPoly"))
					{
						tabSubcategory.TabPages.Add(tabPoly);
					}
					tabSubcategory.Parent = tabTracks;
					tabSubcategory.Visible = true;
					grpTrackSpectro.Parent = tabSpectro;
					grpTrackSpectro.Location = homePoint;
					grpTrackSpectro.Enabled = true;
					grpTrackSpectro.Visible = true;
					tabSpectro.Show();
					tabTracks.Show();
					//chkPolyOctaveGrouping.Checked = useOctaveGrouping;
				}
				if (newMode == SHOWsave)
				{
					tabSubcategory.Visible = false;
					grpSaveFormat.Parent = tabSave;
					grpSaveFormat.Location = homePoint;
					grpSaveFormat.Enabled = true;
					grpSaveFormat.Visible = true;
					tabSave.Show();
				}
			}
			curShowMode = newMode;

		}


		private void cmdOK_Click(object sender, EventArgs e)
		{
			//DialogResult = DialogResult.OK;
			closeMode = DialogResult.OK;
			//this.Hide();
			this.Visible = false;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			closeMode = DialogResult.Cancel;
			//closeAction = DialogResult.Cancel;
			this.Hide();
		}


		private void lblUseRamps_Click(object sender, EventArgs e)
		{
			if (curShowMode == SHOWPoly)
			{
				SetUseRamps(true);
				useRampsPoly = true;
			}
		}

		private void SetUseRamps(bool state)
		{

		}

		private void picHoleStart_Click(object sender, EventArgs e)
		{
			if (curShowMode == SHOWPoly)
			{
				SetUseRamps(!useRampsPoly);
				useRampsPoly = !useRampsPoly;
			}
		}

		private void lblUseOnOff_Click(object sender, EventArgs e)
		{
			SetUseRamps(false);
			if (curShowMode == SHOWPoly)
			{
				useRampsPoly = false;
			}
		}

		private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			//DialogResult = DialogResult.Cancel;
			//closeMode = DialogResult.Cancel;
			this.Hide();
			e.Cancel = true;
		}

		private void brBrowserMsg_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			string url = e.LinkText;
			System.Diagnostics.Process.Start(url);
		}

		private void brBrowserMsg_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			//string link = e.TargetFrameName;
			//System.Windows.Forms.HtmlDocument document = this.brBrowserMsg.Document;
			//string link = System.Windows.Forms.WebBrowserNavigatingEventArgs.Url;
			string link = e.Url.ToString();
			int pos = link.IndexOf("Need");
			if (pos < 0)
			{
				e.Cancel = true;
				pos = link.IndexOf("file:///");
				if (pos >= 0)
				{
					link = link.Substring(8);
				}
				System.Diagnostics.Process.Start(link);
			}
		}

		private void frmSettings_LocationChanged(object sender, EventArgs e)
		{
			int x = 1;

		}

		private void frmSettings_Shown(object sender, EventArgs e)
		{
			if (this.Owner != null)
			{
				int x = this.Owner.Left;
				int w = (this.Owner.Width - this.Width) / 2;
				int y = this.Owner.Top;
				Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
				int h = screenRectangle.Top - this.Top;
				this.Location = new System.Drawing.Point(x + w, y + h);
			}
		}


		private void lblTime44_Click(object sender, EventArgs e)
		{
			SetTimeSignature(4);
		}

		private void SetTimeSignature(int newSignature)
		{
			frmVamp.PlayClickSound();
			if (newSignature == 3)
			{
				swGridBeat.Checked = true;
				swTrackBeat.Checked = true;
				lblGridBeat44.ForeColor = Color.DarkSlateGray;
				lblGridBeat34.ForeColor = Color.Black;
				lblTrackBeat44.ForeColor = Color.DarkSlateGray;
				lblTrackBeat34.ForeColor = Color.Black;
				timesBeatsX = x34[vscGridBeatX.Value];
				trackBeatsX = x34[vscTrackBeatX.Value];
				timeSignature = 3;
			}
			else
			{
				swGridBeat.Checked = false;
				swTrackBeat.Checked = false;
				lblGridBeat34.ForeColor = Color.DarkSlateGray;
				lblGridBeat44.ForeColor = Color.Black;
				lblTrackBeat34.ForeColor = Color.DarkSlateGray;
				lblTrackBeat44.ForeColor = Color.Black;
				timesBeatsX = x44[vscGridBeatX.Value];
				trackBeatsX = x44[vscTrackBeatX.Value];
				// Default to 4 if any value other than 3
				timeSignature = 4;
			}




			if (timeSignature == 3)
			{
			}
			else
			{
				for (int n = 3; n > 0; n--)
				{
					if (timesBeatsX == x44[n])
					{
						n44 = n;
						n = 1; //break from loop
					}
				}
			}

		}

		private void lblTime34_Click(object sender, EventArgs e)
		{
			SetTimeSignature(3);
		}

		private void picHoleTime_Click(object sender, EventArgs e)
		{
			// If 4, set it to 3.  If 3, set it to 4.
			SetTimeSignature(7 - timeSignature);
		}

		private void chkOctaveGrouping_CheckedChanged(object sender, EventArgs e)
		{
			useOctaveGrouping = chkPolyOctaveGrouping.Checked;
			chkSpectroOctaveGrouping.Checked = useOctaveGrouping;
		}

		private void tabCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabCategory.SelectedTab == tabGrids)
			{
				SetShowMode(frmSettings.SHOWtimesBeats);

			}
			if (tabCategory.SelectedTab == tabTracks)
			{
				SetShowMode(frmSettings.SHOWPoly);
			}
			if (tabCategory.SelectedTab == tabSave)
			{
				SetShowMode(frmSettings.SHOWsave);
			}
		}

		private void tabSubcategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			string selTabName = tabCategory.SelectedTab.Text;
			string selSubTabName = tabSubcategory.SelectedTab.Text;
			if (selSubTabName == TABKEYbeats)
			{
				if (selTabName == TABKEYtimess)
				{
					SetShowMode(SHOWtimesBeats);
				}
				else
				{
					SetShowMode(SHOWtrackBeats);
				}
			}
			else
			{
				if (selSubTabName == TABKEYonsets)
				{
					if (selTabName == TABKEYtimess)
					{
						SetShowMode(SHOWtrackBeats);
					}

				}
				else
				{
					if (selSubTabName == TABKEYpoly)
					{
						SetShowMode(SHOWPoly);
					}
					else
					{
						if (selSubTabName == TABKEYspectro)
						{
							SetShowMode(SHOWspectro);
						}
					}
				}
			}
		}

		private void label13_Click(object sender, EventArgs e)
		{

		}

		public void SetTheControlsForTheuserSettings() // Little nod to the Incredible Pink Floyd
		{
			SetTimeSignature(timeSignature);
			txtGridBeatX.Text = timesBeatsX.ToString();
			txtTrackBeatX.Text = trackBeatsX.ToString();
			for (int n = 0; n < 4; n++)
			{
				if (timeSignature == 3)
				{
					if (timesBeatsX == x34[n])
					{
						vscGridBeatX.Value = n;
					}
					if (trackBeatsX == x34[n])
					{
						vscTrackBeatX.Value = n;
					}
				}
				else // timeSignature = 4
				{
					if (timesBeatsX == x44[n])
					{
						vscGridBeatX.Value = n;
					}
					if (trackBeatsX == x44[n])
					{
						vscTrackBeatX.Value = n;
					}
				}
			}
			swTrackBeatsUseRamps.Checked = useRampsBeats;
			swTrackBeatsUseRamps_CheckedChanged(null, null);
			swPolyUseRamps.Checked = useRampsPoly;
			swPolyUseRamps_CheckedChanged(null, null);
			chkPolyOctaveGrouping.Checked = useOctaveGrouping;
			chkSpectroOctaveGrouping.Checked = useOctaveGrouping;
			if (useSaveFormat == frmVamp.SAVEcrgAlpha)
			{
				optCRGAlpha.Checked = true;
			}
			else
			{
				if (useSaveFormat == frmVamp.SAVEcrgDisplay)
				{
					optCRGDisplay.Checked = true;
				}
				else
				{
					optMixedDisplay.Checked = true;
				}
			}

			//public byte useSaveFormat = frmVamp.SAVEmixedDisplay;


		}

		private void swTrackBeatsUseRamps_CheckedChanged(object sender, EventArgs e)
		{
			frmVamp.PlayClickSound();
			useRampsBeats = swTrackBeatsUseRamps.Checked;
			if (useRampsBeats)
			{
				lblTrackBeatsUseRamps.ForeColor = Color.Black;
				lblTrackBeatsUseOnOff.ForeColor = Color.DarkSlateGray;
			}
			else
			{
				lblTrackBeatsUseRamps.ForeColor = Color.DarkSlateGray;
				lblTrackBeatsUseOnOff.ForeColor = Color.Black;
			}

		}

		private void swPolyUseRamps_CheckedChanged(object sender, EventArgs e)
		{
			frmVamp.PlayClickSound();
			useRampsPoly = swPolyUseRamps.Checked;
			if (useRampsBeats)
			{
				lblTrackBeatsUseRamps.ForeColor = Color.Black;
				lblTrackBeatsUseOnOff.ForeColor = Color.DarkSlateGray;
			}
			else
			{
				lblTrackBeatsUseRamps.ForeColor = Color.DarkSlateGray;
				lblTrackBeatsUseOnOff.ForeColor = Color.Black;
			}

		}

		private void lblPolyUseOnOff_Click(object sender, EventArgs e)
		{
			swPolyUseRamps.Checked = false;
		}

		private void lblPolyUseRamps_Click(object sender, EventArgs e)
		{
			swPolyUseRamps.Checked = true;
		}

		private void lblTrackBeatsUseOnOff_Click(object sender, EventArgs e)
		{
			swTrackBeatsUseRamps.Checked = false;
		}

		private void lblTrackBeatsUseRamps_Click(object sender, EventArgs e)
		{
			swTrackBeatsUseRamps.Checked = true;
		}

		private void swGridBeat_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void swTrackBeat_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void lblTrackBeat44_Click(object sender, EventArgs e)
		{
			SetTimeSignature(4);
		}

		private void lblTrackBeat34_Click(object sender, EventArgs e)
		{
			SetTimeSignature(3);
		}

		private void chkPolyOctaveGrouping_CheckedChanged(object sender, EventArgs e)
		{
			useOctaveGrouping = chkPolyOctaveGrouping.Checked;
			chkSpectroOctaveGrouping.Checked = useOctaveGrouping;
		}

		private void chkSpectroOctaveGrouping_CheckedChanged(object sender, EventArgs e)
		{
			useOctaveGrouping = chkSpectroOctaveGrouping.Checked;
			chkPolyOctaveGrouping.Checked = useOctaveGrouping;
		}

		private void vscGridBeatX_Scroll(object sender, ScrollEventArgs e)
		{
			if (timeSignature == 3)
			{
				timesBeatsX = x34[vscGridBeatX.Value];
			}
			else
			{
				timesBeatsX = x44[vscGridBeatX.Value];
			}
			txtGridBeatX.Text = timesBeatsX.ToString();
		} // end scroll

		private void vscTrackBeatX_Scroll(object sender, ScrollEventArgs e)
		{
			if (timeSignature == 3)
			{
				trackBeatsX = x34[vscTrackBeatX.Value];
			}
			else
			{
				trackBeatsX = x44[vscTrackBeatX.Value];
			}
			txtTrackBeatX.Text = trackBeatsX.ToString();
		} // end scroll

		private void swGridBeat_Click(object sender, EventArgs e)
		{
			SetTimeSignature(7 - timeSignature);
		}

		private void swPolyUseRamps_Click(object sender, EventArgs e)
		{
			swPolyUseRamps.Checked = !swPolyUseRamps.Checked;
		}

		private void swTrackBeat_Click(object sender, EventArgs e)
		{
			SetTimeSignature(7 - timeSignature);
		}

		private void swTrackBeatsUseRamps_Click(object sender, EventArgs e)
		{
			swTrackBeatsUseRamps.Checked = !swTrackBeatsUseRamps.Checked;
		}
	} // end form
} // end namespace
