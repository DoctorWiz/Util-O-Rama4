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
using System.Diagnostics;
using LORUtils4; using FileHelper;
using xUtils;


namespace UtilORama4
{
	public partial class frmConverter : Form
	{
		private LORSequence4 seq = null;
		private bool isWiz = false;
		private bool gotx = false;
		private bool gotLOR = false;
		private bool gotItem = false;
		private bool xChosen = false;
		private string xName = "";
		private bool prevStateExisting = true;
		private bool prevStateTimings = true;
		private bool prevStatexToLor = false;
		private static Properties.Settings heartOfTheSun = Properties.Settings.Default;
		private StreamWriter writer = null;
		private xTimings[] timeTracks = null;
		List<LORChannel4> chList = new List<LORChannel4>();


		public frmConverter()
		{
			InitializeComponent();
			RestoreFormPosition();
		}

		private void SaveFormPosition()
		{
			// Get current location, size, and state
			Point myLoc = this.Location;
			Size mySize = this.Size;
			FormWindowState myState = this.WindowState;
			// if minimized or maximized
			if (myState != FormWindowState.Normal)
			{
				// override with the restore location and size
				myLoc = new Point(this.RestoreBounds.X, this.RestoreBounds.Y);
				mySize = new Size(this.RestoreBounds.Width, this.RestoreBounds.Height);
			}

			// Save it for later!
			int x = this.Left;
			heartOfTheSun.Location = myLoc;
			heartOfTheSun.Size = mySize;
			heartOfTheSun.WindowState = (int)myState;
			heartOfTheSun.Save();
		} // End SaveFormPostion

		private void RestoreFormPosition()
		{
			// Multi-Monitor aware
			// AND NOW with overlooked support for fixed borders!
			// with bounds checking
			// repositions as necessary
			// should(?) be able to handle an additional screen that is no longer there,
			// a repositioned taskbar or gadgets bar,
			// or a resolution change.

			// Note: If the saved position spans more than one screen
			// the form will be repositioned to fit all within the
			// screen containing the center point of the form.
			// Thus, when restoring the position, it will no longer
			// span monitors.
			// This is by design!
			// Alternative 1: Position it entirely in the screen containing
			// the top left corner

			Point savedLoc = heartOfTheSun.Location;
			Size savedSize = heartOfTheSun.Size;
			if (this.FormBorderStyle != FormBorderStyle.Sizable)
			{
				savedSize = new Size(this.Width, this.Height);
				this.MinimumSize = this.Size;
				this.MaximumSize = this.Size;
			}
			FormWindowState savedState = (FormWindowState)heartOfTheSun.WindowState;
			int x = savedLoc.X; // Default to saved postion and size, will override if necessary
			int y = savedLoc.Y;
			int w = savedSize.Width;
			int h = savedSize.Height;
			Point center = new Point(x + w / w, y + h / 2); // Find center point
			int onScreen = 0; // Default to primary screen if not found on screen 2+
			Screen screen = Screen.AllScreens[0];

			// Find which screen it is on
			for (int si = 0; si < Screen.AllScreens.Length; si++)
			{
				// Alternative 1: Change "Contains(center)" to "Contains(savedLoc)"
				if (Screen.AllScreens[si].WorkingArea.Contains(center))
				{
					screen = Screen.AllScreens[si];
					onScreen = si;
				}
			}
			Rectangle bounds = screen.WorkingArea;
			// Alternate 2:
			//Rectangle bounds = Screen.GetWorkingArea(center);

			// Test Horizontal Positioning, correct if necessary
			if (this.MinimumSize.Width > bounds.Width)
			{
				// Houston, we have a problem, monitor is too narrow
				System.Diagnostics.Debugger.Break();
				w = this.MinimumSize.Width;
				// Center it horizontally over the working area...
				//x = (bounds.Width - w) / 2 + bounds.Left;
				// OR position it on left edge
				x = bounds.Left;
			}
			else
			{
				// Should fit horizontally
				// Is it too far left?
				if (x < bounds.Left) x = bounds.Left; // Move over
																							// Is it too wide?
				if (w > bounds.Width) w = bounds.Width; // Shrink it
																								// Is it too far right?
				if ((x + w) > bounds.Right)
				{
					// Keep width, move it over
					x = (bounds.Width - w) + bounds.Left;
				}
			}

			// Test Vertical Positioning, correct if necessary
			if (this.MinimumSize.Height > bounds.Height)
			{
				// Houston, we have a problem, monitor is too short
				System.Diagnostics.Debugger.Break();
				h = this.MinimumSize.Height;
				// Center it vertically over the working area...
				//y = (bounds.Height - h) / 2 + bounds.Top;
				// OR position at the top edge
				y = bounds.Top;
			}
			else
			{
				// Should fit vertically
				// Is it too high?
				if (y < bounds.Top) y = bounds.Top; // Move it down
																						// Is it too tall;
				if (h > bounds.Height) h = bounds.Height; // Shorten it
																									// Is it too low?
				if ((y + h) > bounds.Bottom)
				{
					// Kepp height, raise it up
					y = (bounds.Height - h) + bounds.Top;
				}
			}

			// Position and Size should be safe!
			// Move and Resize the form
			this.SetDesktopLocation(x, y);
			this.Size = new Size(w, h);

			// Window State
			if (savedState == FormWindowState.Maximized)
			{
				if (this.MaximizeBox)
				{
					// Optional.  Personally, I think it should always be reloaded non-maximized.
					//this.WindowState = savedState;
				}
			}
			if (savedState == FormWindowState.Minimized)
			{
				if (this.MinimizeBox)
				{
					// Don't think it's right to reload to a minimized state (confuses the user),
					// but you can enable this if you want.
					//this.WindowState = savedState;
				}
			}

		}

		private void SetTheControlsForTheHeartOfTheSun()
		{

		}

		private void GetTheControlsFromTheHeartOfTheSun()
		{

		}

		private void optxToLOR_CheckedChanged(object sender, EventArgs e)
		{
			if (optxToLOR.Checked != prevStatexToLor)
			{
				int y = 5;
				if (optxToLOR.Checked)
				{
					grpxLights.Enabled = true;
					grpxLights.Top = grpWhich.Top + grpWhich.Height + y;
					grpLOR.Top = grpxLights.Top + grpxLights.Height + y;
					optNew.Enabled = true;
					grpEffectType.Visible = optChannel.Checked;

					cboTiming.Visible = true;
					lblxTimingName.Visible = false;
					txtxTimingName.Visible = false;

					cboLORItem.Visible = false;
					lblLORItemName.Left = cboLORItem.Left;
					lblLORItemName.Top = cboLORItem.Top + 2;
					lblLORItemName.Visible = true;
					txtLORItemName.Left = txtLORItemName.Left + txtLORItemName.Width + 2;
					txtLORItemName.Top = cboLORItem.Top;
					txtLORItemName.Visible = true;

					if (txtXFile.Text.Length < 2)
					{
						btnBrowseX_Click(btnBrowseX, null);
					}
				}
				else
				{
					grpLOR.Enabled = true;
					grpLOR.Top = grpWhich.Top + grpWhich.Height + y;
					grpxLights.Top = grpLOR.Top + grpLOR.Height + y;
					optExisting.Checked = true;
					optNew.Enabled = false;
					grpEffectType.Visible = false;

					cboLORItem.Visible = true;
					lblLORItemName.Visible = false;
					txtLORItemName.Visible = false;

					cboTiming.Visible = false;
					lblxTimingName.Left = cboTiming.Left;
					lblxTimingName.Top = cboTiming.Top + 2;
					lblxTimingName.Visible = true;
					txtxTimingName.Left = txtxTimingName.Left + txtxTimingName.Width + 2;
					txtxTimingName.Top = cboTiming.Top;
					txtxTimingName.Visible = true;

					if (txtLORfile.Text.Length < 2)
					{
						btnBrowseLOR_Click(btnBrowseLOR, null);
					}
				}
				prevStatexToLor = optxToLOR.Checked;
			}

		}

		private void btnBrowseLOR_Click(object sender, EventArgs e)
		{
			string stat = "";
			string initDir = LORSequence4.DefaultSequencesPath;
			string initFile = "";
			string filt = "All Sequences (*.las, *.lms, *.lcc)|*.las;*.lms;*.lcc|Musical Sequences only (*.lms)|*.lms";
			filt += "|Animated Sequences only (*.las)|*.las|LORChannel4 Configurations only(*.lcc)|*.lcc";

			if (optExisting.Checked)
			{
				dlgFileOpen.Filter = filt;
				dlgFileOpen.FilterIndex = 0;
				dlgFileOpen.DefaultExt = "*.lms";
				dlgFileOpen.InitialDirectory = initDir;
				dlgFileOpen.FileName = initFile;
				dlgFileOpen.CheckFileExists = true;
				dlgFileOpen.CheckPathExists = true;
				dlgFileOpen.Multiselect = false;
				dlgFileOpen.Title = "Select a Sequence...";
				DialogResult result = dlgFileOpen.ShowDialog(this);
				if (result == DialogResult.OK)
				{
					txtLORfile.Text = dlgFileOpen.FileName;
					seq = new LORSequence4(dlgFileOpen.FileName);
					if (seq.Channels.Count > 0)
					{
						grpEffectType.Visible = false;
						cboLORItem.DropDownStyle = ComboBoxStyle.DropDown;
						cboLORItem.Visible = true;
						txtLORItemName.Visible = false;
						lblLORItemName.Visible = false;
						optTimings.AllowDrop = true;
						optTimings_CheckedChanged(null, null);
						gotLOR = true;
						stat = "Sequence contains " + seq.Channels.Count.ToString() + " channels and " + seq.TimingGrids.Count.ToString() + " timing grids.";
						
						cboLORItem.Items.Clear();
						if (optTimings.Checked)
						{
							grpEffectType.Visible = false;
							for (int it = 0; it < seq.TimingGrids.Count; it++)
							{
								cboLORItem.Items.Add(seq.TimingGrids[it].Name);
							}
						}
						else // Must be channels
						{
							grpEffectType.Visible = true;
							for (int ic = 0; ic < seq.Channels.Count; ic++)
							{
								cboLORItem.Items.Add(seq.Channels[ic].Name);
							}
						}
						cboLORItem.SelectedIndex = 0;
					}
					pnlStatus.Text = stat;

				} // end if (result = DialogResult.OK)
			}
			else // Create New
			{
				dlgFileSave.Filter = filt;
				dlgFileSave.FilterIndex = 2;
				dlgFileSave.DefaultExt = "*.las";
				dlgFileSave.InitialDirectory = initDir;
				dlgFileSave.FileName = initFile;
				dlgFileSave.CheckFileExists = true;
				dlgFileSave.CheckPathExists = true;
				dlgFileSave.Title = "Save New Sequence As...";
				DialogResult result = dlgFileSave.ShowDialog(this);
				if (result == DialogResult.OK)
				{
					txtLORfile.Text = dlgFileSave.FileName;
					seq = new LORSequence4();
					seq.filename = dlgFileSave.FileName;
					cboLORItem.DropDownStyle = ComboBoxStyle.DropDown;
					gotItem = false;
					cboLORItem.Text = xName;
					if (xName.Length > 0)
					{
						gotItem = true;
					}
					gotLOR = true;
					optTimings_CheckedChanged(null, null);
				} // end if (result = DialogResult.OK)
			}

		}

		private void btnBrowseX_Click(object sender, EventArgs e)
		{
			bool stopLooking = false;
			// Start with xLights show directory
			string initDir = xutils.ShowDirectory;
			if (Directory.Exists(initDir))
			{
				// If it exists, see if it has a Timings subdirectory
				string d = initDir + "\\Timings";
				if (Directory.Exists(d))
				{
					// If yes, use that
					initDir = d;
				}
			}
			else
			{
				// Can't find xLights show directory, lets try elsewhere
				// Check LOR directory
				initDir = LORUtils4.lutils.DefaultSequencesPath;
				if (Directory.Exists(initDir))
				{
					// Its there, does it have a Timings subdirectory
					string d = initDir + "Timings\\";
					if (Directory.Exists(d))
					{
						// Look for any timing files
						string[] files = Directory.GetFiles(d, "*.xtiming");
						if (files.Length > 0)
						{
							initDir = d;
						}
					}
					else
					{
						// None in the Timings subdirectory, how about the LOR directory?
						string[] files = Directory.GetFiles(d, "*.xtiming");
						if (files.Length == 0)
						{
							initDir = "";
						}
					}
				}
			}
			// Have we got something (anything!) yet?
			if (!Directory.Exists(initDir))
			{
				// Last Gasp, go the for the user's documents folder
				initDir = Fyle.DefaultDocumentsPath;
			}

			string initFile = "";
			string filt = "xLights Timing Files (*.xtiming)|*.xtiming";
			filt += "|XML Data Files (*.xml)|*.xml";
			filt += "|All Files (*.*)|*.*";

			if (optxToLOR.Checked) // Use Existing
			{
				dlgFileOpen.Filter = filt;
				dlgFileOpen.FilterIndex = 0;
				dlgFileOpen.DefaultExt = "*.xtimings";
				dlgFileOpen.InitialDirectory = initDir;
				dlgFileOpen.FileName = initFile;
				dlgFileOpen.CheckFileExists = true;
				dlgFileOpen.CheckPathExists = true;
				dlgFileOpen.Multiselect = false;
				dlgFileOpen.Title = "Select a Timings File...";
				DialogResult result = dlgFileOpen.ShowDialog(this);
				if (result == DialogResult.OK)
				{
					txtXFile.Text = dlgFileOpen.FileName;
					xChosen = true;
					gotx = true;
					xName = GetxName(dlgFileOpen.FileName);
					timeTracks = xTimings.ReadTimingsFile(dlgFileOpen.FileName);
					string stats = "";
					if (timeTracks.Length == 1) stats = "1 timing track with "; else stats = timeTracks.Length.ToString() + " timing tracks with ";
					int n = 0;
					cboTiming.Items.Clear();
					cboTiming.DropDownStyle = ComboBoxStyle.DropDownList;
					for (int i1 = 0; i1 < timeTracks.Length; i1++)
					{
						xTimings tt = timeTracks[i1];
						cboTiming.Items.Add(tt.timingName);
						n += tt.effects.Count;
					}
					cboTiming.SelectedIndex = 0;
					if (timeTracks.Length == 1) stats += n.ToString() + " timings."; else stats = n.ToString() + " timings (total).";
					pnlStatus.Text = stats;
					grpLOR.Enabled = true;
				} // end if (result = DialogResult.OK)
			}
			else // LORtoX Create New
			{
				dlgFileSave.Filter = filt;
				dlgFileSave.FilterIndex = 0;
				dlgFileSave.DefaultExt = "*.xtiming";
				dlgFileSave.InitialDirectory = initDir;
				dlgFileSave.FileName = initFile;
				dlgFileSave.CheckFileExists = true;
				dlgFileSave.CheckPathExists = true;
				dlgFileSave.Title = "Save New Timings As...";
				DialogResult result = dlgFileSave.ShowDialog(this);
				if (result == DialogResult.OK)
				{
					txtXFile.Text = dlgFileOpen.FileName;
					xChosen = true;
					gotx = true;
				} // end if (result = DialogResult.OK)
			}

		}

		private void optTimings_CheckedChanged(object sender, EventArgs e)
		{
			
			if (prevStateTimings != optTimings.Checked)
			{
				if (optExisting.Checked)
				{
					cboLORItem.DropDownStyle = ComboBoxStyle.DropDown;
					cboLORItem.AllowDrop = true;
					gotItem = false;
					if (optTimings.Checked)
					{
						if (seq.TimingGrids.Count > 0)
						{
							cboLORItem.Text = xName;
							for (int i = 0; i < seq.TimingGrids.Count; i++)
							{
								if (seq.TimingGrids[i].LORTimingGridType4 == LORTimingGridType4.Freeform)
								{
									cboLORItem.Items.Add(seq.TimingGrids[i].Name);
								}
							}
						}
						grpEffectType.Visible = false;
					}
					else // LORChannel4
					{
						if (seq.Channels.Count > 0)
						{
							chList = new List<LORChannel4>(); // Clear/Reset
							chList.Add(seq.Channels[0]);
							for (int i = 1; i < seq.Channels.Count; i++)
							{
								bool match = false;
								for (int l = 0; l < chList.Count; l++)
								{
									if (seq.Channels[i].Name.CompareTo(chList[l].Name) == 0)
									{
										match = true;
									}
								}
								if (!match)
								{
									chList.Add(seq.Channels[i]);
								}
							}
							chList.Sort();
							cboLORItem.Items.Clear();
							for (int l=0; l< chList.Count; l++)
							{
								cboLORItem.Items.Add(chList[l].Name);
							}
						}
						grpEffectType.Visible = true;
					} // End timings or channel
					if (cboLORItem.Items.Count > 0)
					{
						cboLORItem.SelectedIndex = 0;
						gotItem = true;
					}
				}
				else // New Sequence
				{
					picLORPreview.Visible = false;
					cboLORItem.DropDownStyle = ComboBoxStyle.Simple;
					cboLORItem.AllowDrop = false;
					cboLORItem.Items.Clear();
					gotItem = false;
					cboLORItem.Text = xName;
					if (xName.Length > 0)
					{
						gotItem = true;
					}

				} // End existing or new
				prevStateTimings = optTimings.Checked;
				CheckReady();
			} // End state actually changed
		}

		private void ConvertxTimingsToLORchannel(string xTimingsFile, LORChannel4 LORchannel)
		{
			string lineIn = "";
			string label = "";
			int st = 0;
			int et = 0;
			int lastStart = -1;
			int lastEnd = -1;
			int effCount = 0;
			int back2Back = 0;
			LOREffect4 ef = null;
			StreamReader reader = new StreamReader(xTimingsFile);
			// Read and ignore first 3 lines
			if (!reader.EndOfStream)
			{
				// <?xml version...
				lineIn = reader.ReadLine();
			}
			if (!reader.EndOfStream)
			{
				// <timing name...
				lineIn = reader.ReadLine();
			}
			if (!reader.EndOfStream)
			{
				// <EffectLayer>
				lineIn = reader.ReadLine();
				LORchannel.effects.Clear();
			}
			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				label = LORUtils4.lutils.getKeyWord(lineIn, "label");
				st = LORUtils4.lutils.getKeyValue(lineIn, "starttime");
				et = LORUtils4.lutils.getKeyValue(lineIn, "endtime");
				// Convert (and round) from milliseconds to centiseconds
				st = (st + 5) / 10;
				et = (et + 5) / 10;
				// Sanity Checks
				if (st >= 0)
				{
					if (st >= lastEnd)
					{
						if (et > st)
						{
							if (optRampUp.Checked)
							{
								ef = new LOREffect4(LOREffectType4.Intensity, st, et, 0, 100);
							}
							if (optRampDown.Checked)
							{
								ef = new LOREffect4(LOREffectType4.Intensity, st, et, 100, 0);
							}
							if (optOnOff.Checked)
							{
								ef = new LOREffect4(LOREffectType4.Intensity, st, et);
							}
							LORchannel.effects.Add(ef);
							lastStart = st;
							lastEnd = et;
							if (st == lastEnd)
							{
								back2Back++;
							}
						}
					}
				}
			}
			reader.Close();
			if (optNew.Checked)
			{
				seq.CentiFix(lastEnd);
			}

			if (optOnOff.Checked)
			{
				if (back2Back > 0)
				{
					string msg = "This xLights timing file contains back-to-back timings.  When converted to On-Off effects in a LOR channel, it may ";
					msg += "be difficult if not impossible to tell where one timing ends and the next begins.  In this case, using Ramp-Up or ";
					msg += "Ramp-Down effects instead of On-Off is strongly suggested!  You should seriously consider changing the effect type and ";
					msg += "running this conversion again.";
					DialogResult dr = MessageBox.Show(this, msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}

		private void ConvertxTimingsToLORtimings(string xTimingsFile, LORTimings4 LORgrid)
		{
			string lineIn = "";
			string label = "";
			int st = 0;
			int et = 0;
			int lastStart = -1;
			int lastEnd = -1;
			int effCount = 0;
			int back2Back = 0;
			LOREffect4 ef = null;
			StreamReader reader = new StreamReader(xTimingsFile);
			// Read and ignore first 3 lines
			if (!reader.EndOfStream)
			{
				// <?xml version...
				lineIn = reader.ReadLine();
			}
			if (!reader.EndOfStream)
			{
				// <timing name...
				lineIn = reader.ReadLine();
			}
			if (!reader.EndOfStream)
			{
				// <EffectLayer>
				lineIn = reader.ReadLine();
				LORgrid.timings.Clear();
			}
			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				label = LORUtils4.lutils.getKeyWord(lineIn, "label");
				st = LORUtils4.lutils.getKeyValue(lineIn, "starttime");
				et = LORUtils4.lutils.getKeyValue(lineIn, "endtime");
				// Convert (and round) from milliseconds to centiseconds
				st = (st + 5) / 10;
				et = (et + 5) / 10;
				// Sanity Checks
				if (st >= 0)
				{
					if (st >= lastEnd)
					{
						LORgrid.timings.Add(st);
						if (et > st)
						{
							LORgrid.timings.Add(et);
						}
						lastStart = st;
						lastEnd = et;
					}
				}
			}
			reader.Close();
			if (optNew.Checked)
			{
				seq.CentiFix(lastEnd);
			}
		}

		private void ConvertLORchannelToxTimings(LORChannel4 LORchannel, string xTimingsFile)
		{
			int n = 1;
			StreamWriter writer = new StreamWriter(xTimingsFile);
			string lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
			writer.WriteLine(lineOut);
			lineOut = "<timing name=\"" + LORchannel.Name + "\" SourceVersion=\"2021.20\">";
			writer.WriteLine(lineOut);
			lineOut = "  <EffectLayer>";
			writer.WriteLine(lineOut);
			for (int i = 0; i < LORchannel.effects.Count; i++)
			{
				lineOut = "    <LOREffect4 label=\"" + n.ToString() + "\" ";
				n++;
				string t = (LORchannel.effects[i].startCentisecond * 10).ToString();
				lineOut += "starttime=\"" + t + "\" ";
				t = (LORchannel.effects[i].endCentisecond * 10).ToString();
				lineOut += "endtime=\"" + t + "\" />";
				writer.WriteLine(lineOut);
			}
			lineOut = "  </EffectLayer>>";
			writer.WriteLine(lineOut);
			lineOut = "</timing>";
			writer.WriteLine(lineOut);
			writer.Close();
		}

		private void ConvertLORtimingsToxTimings(LORTimings4 LORgrid, string xTimingsFile)
		{
			int n = 1;
			int lastStart = 0;
			StreamWriter writer = new StreamWriter(xTimingsFile);
			string lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
			writer.WriteLine(lineOut);
			lineOut = "<timing name=\"" + LORgrid.Name + "\" SourceVersion=\"2021.20\">";
			writer.WriteLine(lineOut);
			lineOut = "  <EffectLayer>";
			writer.WriteLine(lineOut);
			for (int i = 0; i < LORgrid.timings.Count; i++)
			{
				int t = LORgrid.timings[i];
				if (t > lastStart)
				{
					lineOut = "    <LOREffect4 label=\"" + n.ToString() + "\" ";
					n++;
					string s = (lastStart * 10).ToString();
					lineOut += "starttime=\"" + s + "\" ";
					s = (t * 10).ToString();
					lineOut += "endtime=\"" + s + "\" />";
					lastStart = t;
					writer.WriteLine(lineOut);
				}
			}
			if (lastStart < seq.Centiseconds)
			{
				lineOut = "    <LOREffect4 label=\"" + n.ToString() + "\" ";
				string s = (lastStart * 10).ToString();
				lineOut += "starttime=\"" + s + "\" ";
				s = (seq.Centiseconds * 10).ToString();
				lineOut += "endtime=\"" + s + "\" />";
				writer.WriteLine(lineOut);
			}
			lineOut = "  </EffectLayer>>";
			writer.WriteLine(lineOut);
			lineOut = "</timing>";
			writer.WriteLine(lineOut);
			writer.Close();
		}

		private void cboLORItem_SelectedIndexChanged(object sender, EventArgs e)
		{
			string stat = "";
			bool overWrite = false;
			//if (optLORtox.Checked)
			//{
				if (optExisting.Checked)
				{
					if (optTimings.Checked)
					{
						LORTimings4 LORgrid = seq.TimingGrids[cboLORItem.SelectedIndex];
						Bitmap bmp = LORUtils4.lutils.RenderTimings(LORgrid, 0, LORgrid.Centiseconds, picLORPreview.Width, picLORPreview.Height);
						picLORPreview.Image = bmp;
						stat = "Timing Grid " + LORgrid.Name + " contains " + LORgrid.timings.Count.ToString() + " timings.";
						if (seq.TimingGrids[cboLORItem.SelectedIndex].timings.Count > 1)
						{
							overWrite = true;
						}
					}
					else // LORChannel4
					{
						LORChannel4 LORchannel = chList[cboLORItem.SelectedIndex];
						Bitmap bmp = LORUtils4.lutils.RenderEffects(LORchannel, 0, LORchannel.Centiseconds, picLORPreview.Width, picLORPreview.Height, true);
						picLORPreview.Image = bmp;
						stat = "LORChannel4 " + LORchannel.Name + " contains " + LORchannel.effects.Count.ToString() + " effects.";
						if (LORchannel.effects.Count > 1)
						{
							overWrite = true;
						}
					}
					picLORPreview.Visible = true;
					pnlStatus.Text = stat;
					lblOverwrite.Visible = overWrite;
				//}




				// Else Create New Sequence
				if (!xChosen)
				{
					/*
					string xf = xutils.ShowDirectory;
					if (Directory.Exists(xf))
					{
						string xft = xf + "Timings\\";
						if (Directory.Exists(xft))
						{
							string fx = xft + cboLORItem.Text + ".xtiming";
							txtXFile.Text = fx;
							gotx = true;
						}
						else
						{
							string fx = xf + cboLORItem.Text + ".xtiming";
							txtXFile.Text = fx;
							gotx = true;
						}
					}
					*/
				}
			}
		}

		private void optExisting_CheckedChanged(object sender, EventArgs e)
		{
			if (prevStateExisting != optExisting.Checked)
			{
				txtLORfile.Text = "";
				cboLORItem.Items.Clear();
				gotLOR = false;
				gotItem = false;
				seq = null;
				btnBrowseLOR_Click(null, null);

				prevStateExisting = optExisting.Checked;
			}
		}

		private bool CheckReady()
		{
			bool bConvert = false;
			if (gotx && gotLOR && gotItem)
			{
				bConvert = true;
			}
			btnConvert.Enabled = bConvert;
			return bConvert;
		}

		private string GetxName(string xTimingsFile)
		{
			string lineIn = "";
			string label = "";
			StreamReader reader = new StreamReader(xTimingsFile);
			// Read and ignore first line
			if (!reader.EndOfStream)
			{
				// <?xml version...
				lineIn = reader.ReadLine();
			}
			if (!reader.EndOfStream)
			{
				// <timing name...
				lineIn = reader.ReadLine();
			}
			reader.Close();
			label = LORUtils4.lutils.getKeyWord(lineIn, "name");
			return label;
		}

		private void btnConvert_Click(object sender, EventArgs e)
		{
			if (optxToLOR.Checked)
			{
				if (optTimings.Checked)
				{
					LORTimings4 LORgrid = seq.FindTimingGrid(cboLORItem.Text);
					ConvertxTimingsToLORtimings(txtXFile.Text, LORgrid);
				}
				else // LORChannel4
				{
					if (optExisting.Checked)
					{
						LORChannel4 LORchannel = seq.FindChannel(cboLORItem.Text);
						ConvertxTimingsToLORchannel(txtXFile.Text, LORchannel);
					}
					else // New sequence
					{
						LORTrack4 LORtrack = seq.FindTrack("Time-O-Rama");
						LORChannel4 LORchannel = seq.CreateNewChannel(cboLORItem.Text);
						LORtrack.Members.Add(LORchannel);
						ConvertxTimingsToLORchannel(txtXFile.Text, LORchannel);
					}
				} // End timing or channel
			}
			else // LOR to xLights
			{ 
				if (optTimings.Checked)
				{
					LORTimings4 LORgrid = seq.FindTimingGrid(cboLORItem.Text);
					ConvertLORtimingsToxTimings(LORgrid, txtXFile.Text);
				}
				else
				{
					LORChannel4 LORchannel = seq.FindChannel(cboLORItem.Text);
					ConvertLORchannelToxTimings(LORchannel, txtXFile.Text);
				}
			
			} // End x to LOR or LOR to x
		} // End Convert

		public StreamWriter BeginTimingsXFile(string fileName)
		{
			if (writer != null)
			{
				writer.Close();
			}
			writer = new StreamWriter(fileName);
			writer.WriteLine(xTimings.XMLinfo);
			return writer;
		}

		private void frmConverter_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveFormPosition();
		}

		private void frmConverter_Load(object sender, EventArgs e)
		{
			RestoreFormPosition();
		}

		private void ClearStatus(object sender, EventArgs e)
		{
			pnlStatus.Text = "";
		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{

		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{

		}

		private void cboTiming_SelectedIndexChanged(object sender, EventArgs e)
		{
			int i = cboTiming.SelectedIndex;
			if (i< timeTracks.Length)
			{
				xTimings timeTrack = timeTracks[i];
				Bitmap img= xutils.RenderTimings(timeTrack, 0, timeTrack.EndTime, picxPreview.Width, picxPreview.Height);
				picxPreview.Image = img;
				picxPreview.Visible = true;
			}
		}

		private void picxPreview_Click(object sender, EventArgs e)
		{
			
		}

		private void GridFix()
		{
			LORTimings4[] uniqueGrids = null;
			int uniqueCount = 0;
			for (int t=0; t< seq.TimingGrids.Count; t++)
			{
				LORTimings4 grid = seq.TimingGrids[t];
				if (uniqueCount == 0)
				{
					Array.Resize(ref uniqueGrids, 1);
					uniqueGrids[0] = grid;
					uniqueCount = 1;
				}
				else
				{
					bool match = false;
					for (int u=0; u< uniqueCount; u++)
					{
						if (grid.Name.CompareTo(uniqueGrids[u].Name)==0)
						{
							match = true;
						}
						if (grid.LORTimingGridType4 == LORTimingGridType4.FixedGrid)
						{
							if (uniqueGrids[u].LORTimingGridType4 == LORTimingGridType4.FixedGrid)
							{
								if (grid.spacing == uniqueGrids[u].spacing)
								{
									match = true;
								}
							}
						}

						if (!match)
						{

						}
					}
				}


			}



		}

		private void optChannel_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}
