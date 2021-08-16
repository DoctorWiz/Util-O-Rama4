using LORUtils; using FileHelper;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using UtilORama;
using static Globals;

namespace RGBORama
{
	



	public partial class frmRGB : Form
	{
		private const string SPC = " ";
		private const string FIELDEQ = "=\"";
		private const string ENDQT = "\"";
		private const string ENDFLD = "/>";
		private const string TABLEpresetSet = "presetSet";
		private const string TABLEcolorChange = "colorChange";
		private const string FIELDfromName = "fromName";
		private const string FIELDtoName = "toName";
		private const string FIELDfromR = "fromR";
		private const string FIELDfromG = "fromG";
		private const string FIELDfromB = "fromB";
		private const string FIELDtoR = "toR";
		private const string FIELDtoG = "toG";
		private const string FIELDtoB = "toB";
		private const string FIELDname = "name";
		private const string helpPage = "http://wizlights.com/utilorama/rgborama";

		private string lastFile = "";
		private bool colorChanged = false;
		//private bool changesMade = false;
		private int changeCount = 0;
		private Sequence4 seq = new Sequence4();
		public Preset[] Presets;
		private PresetSet[] presetSets;

		private int presetSetCount = 0;
		//private int colorChangeCount = 0;

		//private Effect[] NEWeffects;
		//private int newEffectCount = 0;

		//public int[] colorsSearch;
		//public int[] colorsReplace;

		



		public int colorsCount = 0;



		private int startCentisecond = utils.UNDEFINED;
		private const int WHOLESONG = 999999;
		private int endCentisecond = WHOLESONG;

		public frmRGB()
		{
			InitializeComponent();
		}

		private void optTime1_CheckedChanged(object sender, EventArgs e)
		{
			txtTimeFrom.Enabled = false;
			txtTimeTo.Enabled = false;
		}

		private void optTime2_CheckedChanged(object sender, EventArgs e)
		{
			txtTimeTo.Enabled = true;
			txtTimeFrom.Enabled = true;
			txtTimeFrom.Focus();
		}

		private void optColor1_CheckedChanged(object sender, EventArgs e)
		{
				if (colorChanged)
				{
					saveColors();
				}

				txtColorFrom1.Enabled = false;
				txtColorFrom2.Enabled = false;
				txtColorFrom3.Enabled = false;
				txtColorFrom4.Enabled = false;
				txtColorFrom5.Enabled = false;
				txtColorFrom6.Enabled = false;
				txtColorTo1.Enabled = false;
				txtColorTo2.Enabled = false;
				txtColorTo3.Enabled = false;
				txtColorTo4.Enabled = false;
				txtColorTo5.Enabled = false;
				txtColorTo6.Enabled = false;
		} // end optColor1_CheckedChanged

		private void optColor3_CheckedChanged(object sender, EventArgs e)
		{
				txtColorFrom1.Enabled = true;
				txtColorFrom2.Enabled = true;
				txtColorFrom3.Enabled = true;
				txtColorFrom4.Enabled = true;
				txtColorFrom5.Enabled = true;
				txtColorFrom6.Enabled = true;
				txtColorTo1.Enabled = true;
				txtColorTo2.Enabled = true;
				txtColorTo3.Enabled = true;
				txtColorTo4.Enabled = true;
				txtColorTo5.Enabled = true;
				txtColorTo6.Enabled = true;

				txtColorFrom1.Focus();
		} // end optColor3_CheckedChanged

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";
			string workFile = "";

			dlgFileOpen.Filter = "Sequence Files *.las, *.lms|*.las;*.lms|Musical Sequences *.lms|*.lms|Animated Sequences *.las|*.las";
			dlgFileOpen.DefaultExt = "*.lms";
			dlgFileOpen.InitialDirectory = basePath;
			dlgFileOpen.CheckFileExists = true;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.Multiselect = false;
			DialogResult result = dlgFileOpen.ShowDialog();

			if (result == DialogResult.OK)
			{
				workFile = dlgFileOpen.FileName;
				if (workFile.Substring(1, 2) != ":\\")
				{
					workFile = basePath + "\\" + lastFile;
				}

				int errs = seq.ReadSequenceFile(workFile);
				if (errs ==0)
				{
					lastFile = workFile;
					Properties.Settings.Default.lastFile = lastFile;
					Properties.Settings.Default.Save();

					if (lastFile.Length > basePath.Length)
					{
						if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
						{
							txtInputFilename.Text = lastFile.Substring(basePath.Length);
						}
						else
						{
							txtInputFilename.Text = lastFile;
						} // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
					} // end if (lastFile.Length > basePath.Length)

					string t = "All of " + utils.FormatTime(seq.Centiseconds) + " Selected";
					lblSelectionTime.Text = t;
					lblSelectionTime.Visible = true;
					t = "All of " + seq.RGBchannels.Count.ToString() + " RGB Channels Selected";
					lblSelectionCount.Text = t;
					lblSelectionCount.Visible = true;
				}
			} // end if (result = DialogResult.OK)
		}

		private void frmColors_Load(object sender, EventArgs e)
		{
			string s = NearestColor.NetListToLorListPct();
			System.Diagnostics.Debugger.Break();
			initForm();
		}

		private void initForm()
		{
			RestoreFormPosition();

			LoadPresets();
			fillPresetList();

			lastFile = Properties.Settings.Default.lastFile;
			string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

			if (lastFile.Length > basePath.Length)
			{
				if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
				{
					txtInputFilename.Text = lastFile.Substring(basePath.Length);
				}
				else
				{
					txtInputFilename.Text = lastFile;
				} // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
			} // end if (lastFile.Length > basePath.Length)

			string opt = Properties.Settings.Default.lastTimeOpt;
			if (opt == "2")
			{
				optTime2.Checked = true;
			}
			else
			{
				optTime1.Checked = true;
			}
			string timeTxt = Properties.Settings.Default.lastTimeFrom;
			txtTimeFrom.Text = timeTxt;
			timeTxt = Properties.Settings.Default.lastTimeTo;
			txtTimeTo.Text = timeTxt;

			opt = Properties.Settings.Default.lastColorOpt;
			if (opt == "1")
			{
				//optColor1.Checked = true;
			}
			else
			{
				//optColor3.Checked = true;
			}

			string colorTxt = Properties.Settings.Default.lastColorFrom1;
			txtColorFrom1.Text = colorTxt;
			colorTxt = Properties.Settings.Default.lastColorFrom2;
			txtColorFrom2.Text = colorTxt;
			colorTxt = Properties.Settings.Default.lastColorFrom3;
			txtColorFrom3.Text = colorTxt;
			colorTxt = Properties.Settings.Default.lastColorFrom4;
			txtColorFrom4.Text = colorTxt;
			colorTxt = Properties.Settings.Default.lastColorFrom5;
			txtColorFrom5.Text = colorTxt;
			colorTxt = Properties.Settings.Default.lastColorFrom6;
			txtColorFrom6.Text = colorTxt;
			colorTxt = Properties.Settings.Default.lastColorTo1;
			txtColorTo1.Text = colorTxt;
			colorTxt = Properties.Settings.Default.lastColorTo2;
			txtColorTo2.Text = colorTxt;
			colorTxt = Properties.Settings.Default.lastColorTo3;
			txtColorTo3.Text = colorTxt;
			colorTxt = Properties.Settings.Default.lastColorTo4;
			txtColorTo4.Text = colorTxt;
			colorTxt = Properties.Settings.Default.lastColorTo5;
			txtColorTo5.Text = colorTxt;
			colorTxt = Properties.Settings.Default.lastColorTo6;
			txtColorTo6.Text = colorTxt;
			colorChanged = false;
		} // end initForm

		private void saveColors()
		{
			Properties.Settings.Default.lastColorFrom1 = txtColorFrom1.Text;
			Properties.Settings.Default.lastColorFrom2 = txtColorFrom2.Text;
			Properties.Settings.Default.lastColorFrom3 = txtColorFrom3.Text;
			Properties.Settings.Default.lastColorFrom4 = txtColorFrom4.Text;
			Properties.Settings.Default.lastColorFrom5 = txtColorFrom5.Text;
			Properties.Settings.Default.lastColorFrom6 = txtColorFrom6.Text;
			Properties.Settings.Default.lastColorTo1 = txtColorTo1.Text;
			Properties.Settings.Default.lastColorTo2 = txtColorTo2.Text;
			Properties.Settings.Default.lastColorTo3 = txtColorTo3.Text;
			Properties.Settings.Default.lastColorTo4 = txtColorTo4.Text;
			Properties.Settings.Default.lastColorTo5 = txtColorTo5.Text;
			Properties.Settings.Default.lastColorTo6 = txtColorTo6.Text;
			colorChanged = false;
			Properties.Settings.Default.Save();
		} // end saveColors

		private void saveSettings()
		{
			Properties.Settings.Default.lastFile = lastFile;

			string opt = "1";
			if (optTime2.Checked)
			{
				opt = "2";
			}
			Properties.Settings.Default.lastTimeOpt = opt;
			Properties.Settings.Default.lastTimeTo = txtTimeTo.Text;
			Properties.Settings.Default.lastTimeFrom = txtTimeFrom.Text;

			Properties.Settings.Default.lastColorOpt = opt;
			Properties.Settings.Default.Save();
		} // end saveSettings

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
			Properties.Settings.Default.Location = myLoc;
			Properties.Settings.Default.Size = mySize;
			Properties.Settings.Default.WindowState = (int)myState;
			Properties.Settings.Default.Save();
		} // End SaveFormPostion

		private void RestoreFormPosition()
		{
			// Multi-Monitor aware
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

			Point savedLoc = Properties.Settings.Default.Location;
			Size savedSize = Properties.Settings.Default.Size;
			if (this.FormBorderStyle != FormBorderStyle.Sizable)
			{
				savedSize = new Size(this.Width, this.Height);
				this.MinimumSize = this.Size;
				this.MaximumSize = this.Size;
			}
			FormWindowState savedState = (FormWindowState)Properties.Settings.Default.WindowState;
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
		} // End LoadFormPostion

		private DialogResult MakeSelections()
		{
			ImBusy(true);
			DialogResult ret = DialogResult.None;
			frmChannels chanForm = new frmChannels((Sequence4)seq.Clone());
			ret = chanForm.ShowDialog(this);

			if (ret == DialogResult.OK)
			{
				seq = chanForm.seq;
			}

			ImBusy(false);
			return ret;
		}

		public void ImBusy(bool busyState)
		{
			this.Enabled = !busyState;
			if (busyState)
			{
				this.Cursor = Cursors.WaitCursor;
			}
			else
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void txtColorFrom1_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private void txtColorTo1_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private void txtColorFrom2_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private void txtColorFrom3_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private void txtColorFrom4_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private void txtColorFrom5_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private void txtColorFrom6_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private void txtColorTo2_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private void txtColorTo3_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private void txtColorTo4_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private void txtColorTo5_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private void txtColorTo6_TextChanged(object sender, EventArgs e)
		{
			colorChanged = true;
		}

		private int backupFile(string fileName)
		{
			int backupSuccess = 0;
			string bak2 = fileName + ".LorBak2";
			bool bakExists = File.Exists(bak2);
			if (bakExists)
			{
				File.Delete(bak2);
			}
			string bak1 = fileName + ".LorBak";
			bakExists = File.Exists(bak1);
			if (bakExists)
			{
				File.Copy(bak1, bak2);
				File.Delete(bak1);
			}
			File.Copy(fileName, bak1);

			return backupSuccess;
		} // end backupFile

		private int ReColor()
		{
			int errState = 0;

			ImBusy(true);

			for (int idx = 0; idx < seq.RGBchannels.Count; idx++)
			{
				if (seq.RGBchannels[idx].Selected)
				{
					pnlStatus.Text = seq.RGBchannels[idx].Name;
					int e = ExchangeColors(seq.RGBchannels[idx]);
				}
			}
			pnlStatus.Text = "";

			ImBusy(false);
			return errState;
		}

		private int ExchangeColors(RGBchannel rgbCh)
		{
			int errState = 0;
			Effect redEffect = new Effect();
			Effect grnEffect = new Effect();
			Effect bluEffect = new Effect();
			int redEffIdx = 0;
			int grnEffIdx = 0;
			int bluEffIdx = 0;
			Color curColor = Color.Black;
			int curLorColor = 0;
			//bool steady = false;

			redEffect.EffectType = EffectType.Intensity;
			redEffect.startCentisecond = 0;
			redEffect.Intensity = 0;
			redEffect.parent = rgbCh.redChannel;

			grnEffect.EffectType = EffectType.Intensity;
			grnEffect.startCentisecond = 0;
			grnEffect.Intensity = 0;
			grnEffect.parent = rgbCh.redChannel;

			bluEffect.EffectType = EffectType.Intensity;
			bluEffect.startCentisecond = 0;
			bluEffect.Intensity = 0;
			bluEffect.parent = rgbCh.redChannel;

			// Loop thru entire song, by centiseconds
			for (int cs=0; cs <= seq.Centiseconds; cs++)
			{
				bool colorChanged = false;
				bool steady = true;
				// Do any of the three colors change type or intensity at the current moment (centisecond)
				if (rgbCh.redChannel.effects[redEffIdx].startCentisecond == cs)
				{
					redEffect = rgbCh.redChannel.effects[redEffIdx];
					redEffIdx++;
					colorChanged = true;
				}
				if (rgbCh.grnChannel.effects[grnEffIdx].startCentisecond == cs)
				{
					grnEffect = rgbCh.grnChannel.effects[grnEffIdx];
					grnEffIdx++;
					colorChanged = true;
				}
				if (rgbCh.bluChannel.effects[bluEffIdx].startCentisecond == cs)
				{
					bluEffect = rgbCh.bluChannel.effects[bluEffIdx];
					bluEffIdx++;
					colorChanged = true;
				}
				// If something changed
				if (colorChanged)
				{
					curLorColor = utils.Color_RGBtoLOR(redEffect.Intensity, grnEffect.Intensity, bluEffect.Intensity);
					steady = redEffect.Steady && grnEffect.Steady && bluEffect.Steady;
				}

				// Are we within the time window to be checked?
				if ((cs >= startCentisecond) && (cs <= endCentisecond))
				{
					// Was there a change?  Is it to a steady color?
					if (colorChanged && steady)
					{
						// Loop thru colors in the search-replace list
						for (int clrIdx=0; clrIdx < colorsSearch.Length; clrIdx++)
						{
							// Color Match?
							if (curLorColor == colorsSearch[clrIdx])
							{
								// At least one color just changed, but the other 2 may (or may not) have changed earlier
								// Check for that, and split the effect if necessary
								if (redEffect.startCentisecond < cs)
								{
									Effect ef = redEffect.Clone();
									redEffect.endCentisecond = cs - 1;
									ef.startCentisecond = cs;
									redEffect = ef;
									rgbCh.grnChannel.effects.Add(ef);
								}
								if (grnEffect.startCentisecond != cs)
								{
									Effect ef = grnEffect.Clone();
									grnEffect.endCentisecond = cs - 1;
									ef.startCentisecond = cs;
									grnEffect = ef;
									rgbCh.grnChannel.effects.Add(ef);
								}
								if (bluEffect.startCentisecond != cs)
								{
									Effect ef = bluEffect.Clone();
									bluEffect.endCentisecond = cs - 1;
									ef.startCentisecond = cs;
									bluEffect = ef;
									rgbCh.grnChannel.effects.Add(ef);
								}

								// Likewise, effects may extend beyond the cut-off time
								// Check for that too, and split the effect if needed
								if (endCentisecond < seq.Centiseconds)
								{
									if (redEffect.endCentisecond > endCentisecond)
									{
										Effect ef = redEffect.Clone();
										redEffect.endCentisecond = endCentisecond;
										ef.startCentisecond = endCentisecond + 1;
										rgbCh.redChannel.effects.Add(ef);
									}
									if (grnEffect.endCentisecond > endCentisecond)
									{
										Effect ef = grnEffect.Clone();
										grnEffect.endCentisecond = endCentisecond;
										ef.startCentisecond = endCentisecond + 1;
										rgbCh.grnChannel.effects.Add(ef);
									}
									if (bluEffect.endCentisecond > endCentisecond)
									{
										Effect ef = bluEffect.Clone();
										bluEffect.endCentisecond = endCentisecond;
										ef.startCentisecond = endCentisecond + 1;
										rgbCh.bluChannel.effects.Add(ef);
									}
								}

								// Apply the new color
								int r = colorsReplace[clrIdx] & 0xff;
								redEffect.Intensity = r;
								int g = colorsReplace[clrIdx] & 0xff00 >> 8;
								grnEffect.Intensity = g;
								int b = colorsReplace[clrIdx] & 0xff0000 >> 16;


								// Break out of loop
								clrIdx = colorsSearch.Length;
							} // End color matches one in the list
						} // End for loop thru colors in search-replace list
					} // End color has changed and is steady
				} // End in the time range
			} // End centisecond loop

			return errState;
		} // End ExchangeColors


		private bool steadyEffect(Effect theEffect)
		{
			bool steady = true;

			return steady;
		}

		
		private void fillPresetList()
		{
			cboPresets.Items.Clear();
			for (int i = 0; i < presetSetCount; i++)
			{
				cboPresets.Items.Add(presetSets[i].name);
			}
			//cboPresets.SelectedIndex = 0;  // TODO: Normal one!  Restore this after debugging
			cboPresets.SelectedIndex = 3;    // for debugging purposes
		}

		private void cboPresets_SelectedIndexChanged(object sender, EventArgs e)
		{
			PresetSet ps = presetSets[cboPresets.SelectedIndex];
			ColorChange[] css = ps.colorChanges;
			string txt;

			txtColorFrom1.Text = "";
			txtColorFrom2.Text = "";
			txtColorFrom3.Text = "";
			txtColorFrom4.Text = "";
			txtColorFrom5.Text = "";
			txtColorFrom6.Text = "";
			txtColorTo1.Text = "";
			txtColorTo2.Text = "";
			txtColorTo3.Text = "";
			txtColorTo4.Text = "";
			txtColorTo5.Text = "";
			txtColorTo6.Text = "";
			picFrom1.BackColor = grpColors.BackColor;
			picFrom2.BackColor = grpColors.BackColor;
			picFrom3.BackColor = grpColors.BackColor;
			picFrom4.BackColor = grpColors.BackColor;
			picFrom5.BackColor = grpColors.BackColor;
			picFrom6.BackColor = grpColors.BackColor;
			picTo1.BackColor = grpColors.BackColor;
			picTo2.BackColor = grpColors.BackColor;
			picTo3.BackColor = grpColors.BackColor;
			picTo4.BackColor = grpColors.BackColor;
			picTo5.BackColor = grpColors.BackColor;
			picTo6.BackColor = grpColors.BackColor;

			if (ps.changeCount > 0)
			{
				txt = css[0].fromR.ToString() + "," + css[0].fromG.ToString() + "," + css[0].fromB.ToString();
				txtColorFrom1.Text = txt;
				picFrom1.BackColor = makeColor(css[0].fromR, css[0].fromG, css[0].fromB);
				txt = css[0].toR.ToString() + "," + css[0].toG.ToString() + "," + css[0].toB.ToString();
				txtColorTo1.Text = txt;
				picTo1.BackColor = makeColor(css[0].toR, css[0].toG, css[0].toB);
			}
			if (ps.changeCount > 1)
			{
				txt = css[1].fromR.ToString() + "," + css[1].fromG.ToString() + "," + css[1].fromB.ToString();
				txtColorFrom2.Text = txt;
				picFrom2.BackColor = makeColor(css[1].fromR, css[1].fromG, css[1].fromB);
				txt = css[1].toR.ToString() + "," + css[1].toG.ToString() + "," + css[1].toB.ToString();
				txtColorTo2.Text = txt;
				picTo2.BackColor = makeColor(css[1].toR, css[1].toG, css[1].toB);
			}
			if (ps.changeCount > 2)
			{
				txt = css[2].fromR.ToString() + "," + css[2].fromG.ToString() + "," + css[2].fromB.ToString();
				txtColorFrom3.Text = txt;
				picFrom3.BackColor = makeColor(css[2].fromR, css[2].fromG, css[2].fromB);
				txt = css[2].toR.ToString() + "," + css[2].toG.ToString() + "," + css[2].toB.ToString();
				txtColorTo3.Text = txt;
				picTo3.BackColor = makeColor(css[2].toR, css[2].toG, css[2].toB);
			}
			if (ps.changeCount > 3)
			{
				txt = css[3].fromR.ToString() + "," + css[3].fromG.ToString() + "," + css[3].fromB.ToString();
				txtColorFrom4.Text = txt;
				picFrom4.BackColor = makeColor(css[3].fromR, css[3].fromG, css[3].fromB);
				txt = css[3].toR.ToString() + "," + css[3].toG.ToString() + "," + css[3].toB.ToString();
				txtColorTo4.Text = txt;
				picTo4.BackColor = makeColor(css[3].toR, css[3].toG, css[3].toB);
			}
			if (ps.changeCount > 4)
			{
				txt = css[4].fromR.ToString() + "," + css[4].fromG.ToString() + "," + css[4].fromB.ToString();
				txtColorFrom5.Text = txt;
				picFrom5.BackColor = makeColor(css[4].fromR, css[4].fromG, css[4].fromB);
				txt = css[4].toR.ToString() + "," + css[4].toG.ToString() + "," + css[4].toB.ToString();
				txtColorTo5.Text = txt;
				picTo5.BackColor = makeColor(css[4].toR, css[4].toG, css[4].toB);
			}
			if (ps.changeCount > 5)
			{
				txt = css[5].fromR.ToString() + "," + css[5].fromG.ToString() + "," + css[5].fromB.ToString();
				txtColorFrom6.Text = txt;
				picFrom6.BackColor = makeColor(css[5].fromR, css[5].fromG, css[5].fromB);
				txt = css[5].toR.ToString() + "," + css[5].toG.ToString() + "," + css[5].toB.ToString();
				txtColorTo6.Text = txt;
				picTo6.BackColor = makeColor(css[5].toR, css[5].toG, css[5].toB);
			}
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{
		}

		private void pictureBox7_Click(object sender, EventArgs e)
		{
		}

		private int Intensity(int amount)
		{
			return Convert.ToInt16(amount * 2.5);
		}

		public static Color makeColor(int Rd, int Gn, int Bl)
		{
			Color nc = Color.Gray;
			int r2 = Convert.ToInt16(Rd * 2.5);
			int g2 = Convert.ToInt16(Gn * 2.5);
			int b2 = Convert.ToInt16(Bl * 2.5);
			nc = Color.FromArgb(r2, g2, b2);
			return nc;
		}

		public EffectType GetBestEffectType(EffectType FirstType, EffectType SecondType, EffectType ThirdType)
		{
			EffectType returnType = EffectType.Intensity;

			if (FirstType > EffectType.None)
			{
				returnType = FirstType;
			}
			else if (SecondType > EffectType.None)
			{
				returnType = SecondType;
			}
			else if (ThirdType > EffectType.None)
			{
				returnType = ThirdType;
			}

			return returnType;
		}

		public void updateProgress(int chanNo, int centiSecond)
		{
			int pct = chanNo * 100 / seq.Channels.Count;
			int pi = Convert.ToInt16(pct);
			//prgProgress.Value = pi;

			string sMsg = "Channel " + (chanNo + 1).ToString() + " of " + seq.Channels.Count.ToString();
			//sMsg += ", Pass " + member.ToString() + " of 2";
			//sMsg += ", Centisecond " + centiSecond.ToString() + " of " + seq.Centiseconds.ToString();
			//Debug.Print(sMsg);
			//staInfo1.Text = sMsg;
			staStatus.Refresh();
		} // end Form

		public struct rgbKeywd
		{
			public int r;
			public int g;
			public int b;
		}

		private void frmColors_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();
		}

		private void groupBox5_Enter(object sender, EventArgs e)
		{
		}

		private void btnReColor_Click_1(object sender, EventArgs e)
		{
			ReColor();
		}

		private void btnBrowseOutput_Click(object sender, EventArgs e)
		{

		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			Form aboutBox = new About();
			aboutBox.ShowDialog(this);
			ImBusy(false);

		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(helpPage);

		}

		private int LoadPresets()
		{
			int errStatus = 0;

			//string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama 2015\\Sequences\\";
			string basePath = utils.DefaultChannelConfigsPath;
			string fileName = basePath + "ColorChanger Presets.xml";

			StreamReader reader = new StreamReader(fileName);
			string lineIn; // line read in (does not get modified)
			int pos1 = -1; // positions of certain key text in the line
			int lineCount = 0;

			// Zero these out from any previous run
			presetSetCount = 0;
			//colorChangeCount = 0;

			int curPreset = -1;
			int curChange = -1;

			ColorChange[] colorChanges = new ColorChange[1];

			// * PASS 1 - COUNT OBJECTS
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// does this line mark the start of a preset set?
				pos1 = lineIn.IndexOf("<presetSet name=");
				if (pos1 > 0)
				{
					presetSetCount++;
				}
				pos1 = lineIn.IndexOf("<colorChange fromName=");
				if (pos1 > 0)
				{
					//colorChangeCount++;
				}
			}
			reader.Close();

			// CREATE ARRAYS TO HOLD OBJECTS
			presetSets = new PresetSet[presetSetCount];

			// * PASS 2 - COLLECT OBJECTS
			reader = new StreamReader(fileName);
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// does this line mark the start of a preset set?
				pos1 = lineIn.IndexOf(TABLEpresetSet + " " + FIELDname);
				if (pos1 > 0)
				{
					curPreset++;
					PresetSet ps = new PresetSet();
					ps.name = utils.getKeyWord(lineIn, FIELDname);
					presetSets[curPreset] = ps;

					lineIn = reader.ReadLine();
					lineIn = reader.ReadLine();
					curChange = -1;
					pos1 = lineIn.IndexOf(TABLEcolorChange + " " + FIELDfromName);
					while (pos1 > 0)
					{
						curChange++;
						Array.Resize(ref colorChanges, curChange + 1);
						ColorChange cc = new ColorChange();
						cc.fromName = utils.getKeyWord(lineIn, FIELDfromName);
						cc.fromR = utils.getKeyValue(lineIn, FIELDfromR);
						cc.fromG = utils.getKeyValue(lineIn, FIELDfromG);
						cc.fromB = utils.getKeyValue(lineIn, FIELDfromB);
						cc.toName = utils.getKeyWord(lineIn, FIELDtoName);
						cc.toR = utils.getKeyValue(lineIn, FIELDtoR);
						cc.toG = utils.getKeyValue(lineIn, FIELDtoG);
						cc.toB = utils.getKeyValue(lineIn, FIELDtoB);
						colorChanges[curChange] = cc;

						lineIn = reader.ReadLine();
						pos1 = lineIn.IndexOf(TABLEcolorChange + " " + FIELDfromName);
					}
					if (curChange > -1)
					{
						ps.colorChanges = colorChanges;
						ps.changeCount = curChange + 1;
						presetSets[curPreset] = ps;
					}
				}
			}

			reader.Close();


			string sMsg;
			sMsg = "File Parse Complete!\r\n\r\n";
			sMsg += lineCount.ToString() + " lines\r\n";
			sMsg += seq.Channels.Count.ToString() + " Channels\r\n";
			//sMsg += RGBchannels.Count.ToString() + " RGB Channels\r\n";
			//sMsg += seq.effectCount.ToString() + " Effects\r\n";
			//sMsg += groupCount.ToString() + " groups\r\n";
			//sMsg += groupItemCount.ToString() + " group items";

			DialogResult mReturn;
			mReturn = MessageBox.Show(sMsg, "File Parse Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);


			return errStatus;
		}

	} // end Namespace
}