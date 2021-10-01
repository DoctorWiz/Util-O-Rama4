using System;
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
using LORUtils4;
using FileHelper;
using xUtils;
using FuzzyString;


namespace UtilORama4
{
	public partial class frmCompare : Form
	{
		private static Properties.Settings heartOfTheSun = Properties.Settings.Default;
		private const string helpPage = "http://wizlights.com/utilorama/blankorama";

		LORSequence4 sequence = null;
		public List<LORChannel4> channelList = new List<LORChannel4>();
		public List<LORRGBChannel4> RGBList = new List<LORRGBChannel4>();
		public List<LORChannelGroup4> groupList = new List<LORChannelGroup4>();

		public List<xModel> xModelList = new List<xModel>();
		public List<xRGBmodel> xRGBList = new List<xRGBmodel>();
		public List<xPixels> xPixelList = new List<xPixels>();
		public List<xModelGroup> xGroupList = new List<xModelGroup>();


		//double minPreMatch = 85; // Properties.Settings.Default.FuzzyMinPrematch;
		//long preAlgorithm = FuzzyFunctions.USE_SUGGESTED_PREMATCH;
		//double minFinalMatch = 95; // Properties.Settings.Default.FuzzyMinFinal;
		//long finalAlgorithms = FuzzyFunctions.USE_SUGGESTED_FINALMATCH;

		public frmCompare()
		{
			InitializeComponent();
		}

		private void frmBlank_Load(object sender, EventArgs e)
		{
			RestoreFormPosition();
			GetTheControlsFromTheHeartOfTheSun();
		}

		private void frmBlank_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();
			SetTheControlsForTheHeartOfTheSun();
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
			SaveFormPosition();
			heartOfTheSun.Save();
		}

		private void GetTheControlsFromTheHeartOfTheSun()
		{
			txtXFile.Text = heartOfTheSun.rgbeffects;
			txtLORfile.Text = heartOfTheSun.Sequence;
			txtSpreadsheet.Text = heartOfTheSun.Spreadsheet;

			if (File.Exists(txtXFile.Text))
			{
				CompilexLightsLists(txtXFile.Text);
			}

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
			frmAbout aboutBox = new frmAbout();
			aboutBox.picIcon.Image = picAboutIcon.Image;
			aboutBox.Icon = this.Icon;
			aboutBox.ShowDialog(this);
			ImBusy(false);

		}

		private void txtLORfile_TextChanged(object sender, EventArgs e)
		{

		}

		private void txtXFile_TextChanged(object sender, EventArgs e)
		{

		}

		private int CompileLORlists(LORSequence4 seq)
		{
			bool match = false;
			int count = 0;

			// [Regular] Channels (Non-RGB)
			if (seq.Channels.Count > 0)
			{
				channelList = new List<LORChannel4>(); // Clear/Reset
				channelList.Add(seq.Channels[0]);
				for (int i = 1; i<seq.Channels.Count; i++)
				{
					match = false;
					for (int l = 0; l<channelList.Count; l++)
					{
						if (seq.Channels[i].Name.CompareTo(channelList[l].Name) == 0)
						{
							match = true;
						}
					}
					if (!match)
					{
						channelList.Add(seq.Channels[i]);
						count++;
					}
				}
				channelList.Sort();
			}

			// RGB Channels
			if (seq.RGBchannels.Count > 0)
			{
				RGBList = new List<LORRGBChannel4>(); // Clear/Reset
				RGBList.Add(seq.RGBchannels[0]);
				for (int i = 1; i < seq.RGBchannels.Count; i++)
				{
					match = false;
					for (int l = 0; l < RGBList.Count; l++)
					{
						if (seq.RGBchannels[i].Name.CompareTo(RGBList[l].Name) == 0)
						{
							match = true;
						}
					}
					if (!match)
					{
						RGBList.Add(seq.RGBchannels[i]);
						count++;
					}
				}
				RGBList.Sort();
			}

			// Channel Groups
			if (seq.ChannelGroups.Count > 0)
			{
				groupList = new List<LORChannelGroup4>(); // Clear/Reset
				groupList.Add(seq.ChannelGroups[0]);
				for (int i = 1; i < seq.ChannelGroups.Count; i++)
				{
					match = false;
					for (int l = 0; l < groupList.Count; l++)
					{
						if (seq.ChannelGroups[i].Name.CompareTo(groupList[l].Name) == 0)
						{
							match = true;
						}
					}
					if (!match)
					{
						groupList.Add(seq.ChannelGroups[i]);
						count++;
					}
				}
				groupList.Sort();
			}
			return count;
		}

		private int CompilexLightsLists(string fileName)
		{
			int count = 0;
			string lineIn = "";
			string theName = "";
			xMemberType mbrType = xMemberType.Model; // Default
			xMember member;



			StreamReader reader = new StreamReader(fileName);
			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				int ip = lineIn.IndexOf(" name=\"");
				if (ip >= 0)
				{
					theName = xutils.getKeyWord(lineIn, "name");

					ip = lineIn.IndexOf("<model ");
					if (ip >= 0)
					{
						// If it's a model...
						// Is it a Single Color LORChannel4?
						ip = lineIn.IndexOf("StringType=\"Single Color");
						if (ip >= 0)
						{
							mbrType = xMemberType.Model; // Default
							string st = xutils.getKeyWord(lineIn, "StringType");
							//TODO Get its color!
							xModel xch = new xModel(theName);
							xModelList.Add(xch);
							count++;
							member = xch;
						}
						else // not single color channel
						{
							// Is it a string of pixels?
							ip = lineIn.IndexOf("StringType=\"RGB Nodes");
							if (ip >= 0)
							{
								mbrType = xMemberType.Pixels;
								xPixels xpx = new xPixels(theName);
								xPixelList.Add(xpx);
								//count++;
								member = xpx;
							}
							else // not pixels
							{
								// Is it an RGB floodlight or single pixel?
								ip = lineIn.IndexOf("StringType=\"3 Channel RGB");
								if (ip >= 0)
								{
									mbrType = xMemberType.RGBmodel;
									xRGBmodel xrgb = new xRGBmodel(theName);
									xRGBList.Add(xrgb);
									count++;
									member = xrgb;
								}
							}
						}
					}
					else // Not a model
					{
						// Is it a group?
						ip = lineIn.IndexOf("<modelGroup ");
						if (ip >= 0)
						{
							xModelGroup xgrp = new xModelGroup(theName);
							xGroupList.Add(xgrp);
							count++;
							member = xgrp;
							//TODO Get Members
						}
					}
				}
			}



			return count;
		}

		private void btnBrowseX_Click(object sender, EventArgs e)
		{
			bool stopLooking = false;
			// Start with xLights show directory
			string initDir = xutils.ShowDirectory;
			if (Directory.Exists(initDir))
			{
				//
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
			string filt = "xLights rgbeffects File (xlights_rgbeffects.xml)|xlights_rgbeffects.xml";
			filt += "|XML Data Files (*.xml)|*.xml";
			filt += "|All Files (*.*)|*.*";

			dlgFileOpen.Filter = filt;
			dlgFileOpen.FilterIndex = 0;
			dlgFileOpen.DefaultExt = "xlights_rgbeffects.xml";  //??? or...
			//?? dlgFileOpen.DefaultExt = "*.xml";
			dlgFileOpen.InitialDirectory = initDir;
			dlgFileOpen.FileName = initFile;
			dlgFileOpen.CheckFileExists = true;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.Multiselect = false;
			dlgFileOpen.Title = "Select the rgbeffects File...";
			DialogResult result = dlgFileOpen.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				txtXFile.Text = dlgFileOpen.FileName;
				heartOfTheSun.rgbeffects = dlgFileOpen.FileName;
				CompilexLightsLists(dlgFileOpen.FileName);
			} // end if (result = DialogResult.OK)

		}

		private void btnBrowseLOR_Click(object sender, EventArgs e)
		{
			bool stopLooking = false;
			// Start with LOR Showtime directory
			string initDir = LORUtils4.lutils.DefaultSequencesPath;
			if (Directory.Exists(initDir))
			{
				// If it exists, see if it has a Sequences subdirectory
				string d = initDir + "\\Sequences";
				if (Directory.Exists(d))
				{
					// If yes, use that
					initDir = d;
				}
			}
			else
			{
				// Can't find LOR Showtime directory, lets try elsewhere
				// Check xLights show folder
				initDir = xutils.ShowDirectory;
				if (Directory.Exists(initDir))
				{
					// Its there, does it have a Timings subdirectory
					string d = initDir + "Sequences\\";
					if (Directory.Exists(d))
					{
						// Look for any timing files
						string[] files = Directory.GetFiles(d, "*.lms");
						if (files.Length > 0)
						{
							initDir = d;
						}
						files = Directory.GetFiles(d, "*.las");
						if (files.Length > 0)
						{
							initDir = d;
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
			string filt = "All Sequences (*.las, *.lms, *.lcc)|*.las;*.lms;*.lcc|Musical Sequences only (*.lms)|*.lms";
			filt += "|Animated Sequences only (*.las)|*.las|LORChannel4 Configurations only(*.lcc)|*.lcc";
			filt += "|All Files (*.*)|*.*";

			dlgFileOpen.Filter = filt;
			dlgFileOpen.FilterIndex = 0;
			dlgFileOpen.DefaultExt = "*.lms";
			dlgFileOpen.InitialDirectory = initDir;
			dlgFileOpen.FileName = initFile;
			dlgFileOpen.CheckFileExists = true;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.Multiselect = false;
			dlgFileOpen.Title = "Select the Sequence File...";
			DialogResult result = dlgFileOpen.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				ImBusy(true);
				txtLORfile.Text = dlgFileOpen.FileName;
				heartOfTheSun.Sequence = dlgFileOpen.FileName;
				sequence = new LORSequence4(dlgFileOpen.FileName);
				CompileLORlists(sequence);
				string stat = channelList.Count.ToString() + " Channels, ";
				stat += RGBList.Count.ToString() + " RGB Channels, and ";
				stat += groupList.Count.ToString() + " Channel Groups.";
				pnlStatus.Text = stat;

				if (xModelList.Count > 0)
				{
					btnOK.Enabled = true;
				}
				ImBusy(false);
				btnOK.Focus();
			} // end if (result = DialogResult.OK)


		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			int n = Compare();
			pnlStatus.Text = "Comparison complete with " + n.ToString() + " matches.";
			grpSpreadsheet.Enabled = true;
			ImBusy(false);
			btnBrowseSheet.Focus();
		}

		public int Compare()
		{


			int exactMatches = 0;
			int fuzzyMatches = 0;
			string status = "";

			// LOR Channels to xLights Modesl or Model Groups, Exact Matches
			//   Channels to Models first
			for (int l=0; l< channelList.Count; l++)
			{
				string LORname = channelList[l].Name;
				status = "Searching for " + LORname;
				pnlStatus.Text = status;
				staStatus.Refresh();
				for (int x=0; x < xModelList.Count; x++)
				{
					string xName = xModelList[x].Name;
					if (LORname.CompareTo(xName) == 0)
					{
						channelList[l].Tag = xModelList[x];
						channelList[l].Selected = true;
						xModelList[x].Tag = channelList[l];
						xModelList[x].Selected = true;
						xModelList[x].ExactMatch = true;
						exactMatches++;
						// Force exit of loop
						x = xModelList.Count;
					}
				}
			}

			// LOR Channels to xLights Modesl or Model Groups, Exact Matches
			//   Channels to Model Groups next
			for (int l = 0; l < channelList.Count; l++)
			{
				string LORname = channelList[l].Name;
				status = "Searching for " + LORname;
				pnlStatus.Text = status;
				staStatus.Refresh();
				for (int x = 0; x < xGroupList.Count; x++)
				{
					string xName = xGroupList[x].Name;
					if (LORname.CompareTo(xName) == 0)
					{
						channelList[l].Tag = xGroupList[x];
						channelList[l].Selected = true;
						xGroupList[x].Tag = channelList[l];
						xGroupList[x].Selected = true;
						xGroupList[x].ExactMatch = true;
						exactMatches++;
						// Force exit of loop
						x = xGroupList.Count;
					}
				}
			}

			// LOR Channels to xLights Models or Model Groups, Fuzzy Matches
			//   Channels to Models first
			for (int l = 0; l < channelList.Count; l++)
			{
				if (!channelList[l].Selected)
				{
					string LORname = channelList[l].Name;
					status = "Fuzzy Searching for " + LORname;
					pnlStatus.Text = status;
					staStatus.Refresh();
					double highScore = 0;
					int highModelMatch = -1;
					int highGroupMatch = -1;
					for (int x = 0; x < xModelList.Count; x++)
					{
						if (!xModelList[x].Selected)
						{
							string xName = xModelList[x].Name;
							double preScore = LORname.RankEquality(xName, FuzzyFunctions.USE_SUGGESTED_PREMATCH);
							// if the score is above the minimum PreMatch
							if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								double finalScore = LORname.RankEquality(xName, FuzzyFunctions.USE_SUGGESTED_FINALMATCH);
								if (finalScore > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								{
									if (finalScore > highScore)
									{
										highScore = finalScore;
										highModelMatch = x;
									}
								}
							}
						}
					}

					// Next Channels to Groups
					for (int x = 0; x < xGroupList.Count; x++)
					{
						if (!xGroupList[x].Selected)
						{
							string xName = xGroupList[x].Name;
							double preScore = LORname.RankEquality(xName, FuzzyFunctions.USE_SUGGESTED_PREMATCH);
							// if the score is above the minimum PreMatch
							if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								double finalScore = LORname.RankEquality(xName, FuzzyFunctions.USE_SUGGESTED_FINALMATCH);
								if (finalScore > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								{
									if (finalScore > highScore)
									{
										highScore = finalScore;
										highGroupMatch = x;
									}
								}
							}
						}
					}

					if (highGroupMatch >= 0)
					{
						channelList[l].Tag = xGroupList[highGroupMatch];
						channelList[l].Selected = true;
						xGroupList[highGroupMatch].Tag = channelList[l];
						xGroupList[highGroupMatch].Selected = true;
						fuzzyMatches++;
					}
					else
					{
						if (highModelMatch >= 0)
						{
							channelList[l].Tag = xModelList[highModelMatch];
							channelList[l].Selected = true;
							xModelList[highModelMatch].Tag = channelList[l];
							xModelList[highModelMatch].Selected = true;
							fuzzyMatches++;
						}
					}
				}
			}

			// LOR RGBchannels to xLights RGBchannels, Exact Matches
			for (int l = 0; l < RGBList.Count; l++)
			{
				string LORname = RGBList[l].Name;
				for (int x = 0; x < xRGBList.Count; x++)
				{
					string xName = xRGBList[x].Name;
					if (LORname.CompareTo(xName) == 0)
					{
						RGBList[l].Tag = xRGBList[x];
						RGBList[l].Selected = true;
						xRGBList[x].Tag = RGBList[l];
						xRGBList[x].Selected = true;
						xRGBList[x].ExactMatch = true;
						exactMatches++;
						// Force exit of loop
						x = xRGBList.Count;
					}
				}
			}

			// LOR RGB Channels to xLights RGB Channels, Fuzzy Matches
			for (int l = 0; l < RGBList.Count; l++)
			{
				if (!RGBList[l].Selected)
				{
					string LORname = RGBList[l].Name;
					status = "Fuzzy Searching for " + LORname;
					pnlStatus.Text = status;
					staStatus.Refresh();
					double highScore = 0;
					int highMatch = -1;
					for (int x = 0; x < xRGBList.Count; x++)
					{
						if (!xRGBList[x].Selected)
						{
							string xName = xRGBList[x].Name;
							double preScore = LORname.RankEquality(xName, FuzzyFunctions.USE_SUGGESTED_PREMATCH);
							// if the score is above the minimum PreMatch
							if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								double finalScore = LORname.RankEquality(xName, FuzzyFunctions.USE_SUGGESTED_FINALMATCH);
								if (finalScore > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								{
									if (finalScore > highScore)
									{
										highScore = finalScore;
										highMatch = x;
									}
								}
							}
						}
					}
					if (highMatch >= 0)
					{
						RGBList[l].Tag = xRGBList[highMatch];
						RGBList[l].Selected = true;
						xRGBList[highMatch].Tag = RGBList[l];
						xRGBList[highMatch].Selected = true;
						fuzzyMatches++;
					}
				}
			}

			// LOR Groups to xLights Groups, Exact Matches
			for (int l = 0; l < groupList.Count; l++)
			{
				string LORname = groupList[l].Name;
				for (int x = 0; x < xGroupList.Count; x++)
				{
					string xName = xGroupList[x].Name;
					if (LORname.CompareTo(xName) == 0)
					{
						groupList[l].Tag = xGroupList[x];
						groupList[l].Selected = true;
						xGroupList[x].Tag = groupList[l];
						xGroupList[x].Selected = true;
						xGroupList[x].ExactMatch = true;
						exactMatches++;
						// Force exit of loop
						x = xGroupList.Count;
					}
				}
			}

			// LOR Groups to xLights Groups, Fuzzy Matches
			for (int l = 0; l < groupList.Count; l++)
			{
				if (!groupList[l].Selected)
				{
					string LORname = groupList[l].Name;
					status = "Fuzzy Searching for " + LORname;
					pnlStatus.Text = status;
					staStatus.Refresh();
					double highScore = 0;
					int highMatch = -1;
					for (int x = 0; x < xGroupList.Count; x++)
					{
						if (!xGroupList[x].Selected)
						{
							string xName = xGroupList[x].Name;
							double preScore = LORname.RankEquality(xName, FuzzyFunctions.USE_SUGGESTED_PREMATCH);
							// if the score is above the minimum PreMatch
							if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								double finalScore = LORname.RankEquality(xName, FuzzyFunctions.USE_SUGGESTED_FINALMATCH);
								if (finalScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
								{
									if (finalScore > highScore)
									{
										highScore = finalScore;
										highMatch = x;
									}
								}
							}
						}
					}
					if (highMatch >= 0)
					{
						groupList[l].Tag = xGroupList[highMatch];
						groupList[l].Selected = true;
						xGroupList[highMatch].Tag = groupList[l];
						xGroupList[highMatch].Selected = true;
						fuzzyMatches++;
					}
				}
			}



			return (exactMatches + fuzzyMatches);
		}

		private void btnBrowseSheet_Click(object sender, EventArgs e)
		{
			bool stopLooking = false;

			string lastSheet = heartOfTheSun.Spreadsheet;
			string initDir = Path.GetDirectoryName(lastSheet);
			// Start with last folder where a spreadsheet was saved
			if (Directory.Exists(initDir))
			{
				//
			}
			else
			{
			}
			// Have we got something (anything!) yet?
			if (!Directory.Exists(initDir))
			{
				// Last Gasp, go the for the user's documents folder
				initDir = Fyle.DefaultDocumentsPath;
			}

			string seqName = Path.GetFileNameWithoutExtension(txtLORfile.Text);
			string initFile = "LOR-to-xLights_Channel_Comparison for ";
			initFile += seqName + ".csv";



			string filt = "Comma-Separated-Values Files (*.csv)|*.csv";
			filt += "|All Files (*.*)|*.*";

			dlgFileSave.Filter = filt;
			dlgFileSave.FilterIndex = 0;
			dlgFileSave.DefaultExt = "*.csv";
			dlgFileSave.InitialDirectory = initDir;
			dlgFileSave.FileName = initFile;
			dlgFileSave.CheckPathExists = true;
			dlgFileSave.OverwritePrompt = false; // Use mine instead
			dlgFileSave.Title = "Save Spreedsheet File As...";
			DialogResult result = dlgFileSave.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				DialogResult ow = Fyle.SafeOverwriteFile(dlgFileSave.FileName);
				if (ow == DialogResult.Yes)
				{

					ImBusy(true);
					string f = dlgFileSave.FileName;
					txtSpreadsheet.Text = f;
					heartOfTheSun.Spreadsheet = f;
					int m = CompileSpreadsheet(f, true);
					pnlStatus.Text = "Spreadsheet saved with " + m.ToString() + " lines.";
					System.Diagnostics.Process.Start(@f);
					ImBusy(false);
				}
			} // end if (result = DialogResult.OK)

		}

		private int CompileSpreadsheet(string fileName, bool LORfirst)
		{
			int lineCount = 0;
			StringBuilder lineOut = new StringBuilder();
			StreamWriter writer = new StreamWriter(fileName);
			if (LORfirst)
			{
				lineOut.Append("LOR LORChannel4, Match, xLights Model, Color");
				writer.WriteLine(lineOut.ToString());
				lineCount++;
				
				for (int l=0; l< channelList.Count; l++)
				{
					lineOut.Clear();
					LORChannel4 lc = channelList[l];
					lineOut.Append(CSVsafeName(lc.Name));
					lineOut.Append(',');
					if (lc.Selected)
					{
						xMember xc = (xMember)lc.Tag;
						if (xc.ExactMatch)
						{
							lineOut.Append("Exact");
						}
						else
						{
							lineOut.Append("Fuzzy");
						}
						lineOut.Append(',');
						lineOut.Append(CSVsafeName( xc.Name));
						lineOut.Append(',');
					}
					else
					{
						lineOut.Append("None,,");
					}
					string clr = "#" + LORUtils4.lutils.Color_LORtoHTML(lc.color); 
					lineOut.Append(clr);
					writer.WriteLine(lineOut.ToString());
					lineCount++;
				}
				for (int x=0; x< xModelList.Count; x++)
				{
					xMember xc = xModelList[x];
					if (!xc.Selected)
					{
						lineOut.Clear();
						lineOut.Append(",None,");
						lineOut.Append(CSVsafeName(xc.Name));
						lineOut.Append(',');
						writer.WriteLine(lineOut.ToString());
						lineCount++;
					}
				}

				writer.WriteLine("");
				lineCount++;
				lineOut.Clear();
				lineOut.Append("LOR RGB LORChannel4, Match, xLights Model");
				writer.WriteLine(lineOut.ToString());
				lineCount++;

				for (int l = 0; l < RGBList.Count; l++)
				{
					lineOut.Clear();
					LORRGBChannel4 lc = RGBList[l];
					lineOut.Append(CSVsafeName(lc.Name));
					lineOut.Append(',');
					if (lc.Selected)
					{
						xMember xc = (xMember)lc.Tag;
						if (xc.ExactMatch)
						{
							lineOut.Append("Exact");
						}
						else
						{
							lineOut.Append("Fuzzy");
						}
						lineOut.Append(',');
						lineOut.Append(CSVsafeName(xc.Name));
					}
					else
					{
						lineOut.Append("None,");
					}
					writer.WriteLine(lineOut.ToString());
					lineCount++;
				}
				for (int x = 0; x < xRGBList.Count; x++)
				{
					xMember xc = xRGBList[x];
					if (!xc.Selected)
					{
						lineOut.Clear();
						lineOut.Append(",None,");
						lineOut.Append(CSVsafeName(xc.Name));
						writer.WriteLine(lineOut.ToString());
						lineCount++;
					}
				}

				writer.WriteLine("");
				lineCount++;
				lineOut.Clear();
				lineOut.Append("LOR Channel Group, Match, xLights Model Group");
				writer.WriteLine(lineOut.ToString());
				lineCount++;

				for (int l = 0; l < groupList.Count; l++)
				{
					lineOut.Clear();
					LORChannelGroup4 lc = groupList[l];
					lineOut.Append(CSVsafeName(lc.Name));
					lineOut.Append(',');
					if (lc.Selected)
					{
						xMember xc = (xMember)lc.Tag;
						if (xc.ExactMatch)
						{
							lineOut.Append("Exact");
						}
						else
						{
							lineOut.Append("Fuzzy");
						}
						lineOut.Append(',');
						lineOut.Append(CSVsafeName(xc.Name));
					}
					else
					{
						lineOut.Append("None,");
					}
					writer.WriteLine(lineOut.ToString());
					lineCount++;
				}
				for (int x = 0; x < xGroupList.Count; x++)
				{
					xMember xc = xGroupList[x];
					if (!xc.Selected)
					{
						lineOut.Clear();
						lineOut.Append(",None,");
						lineOut.Append(CSVsafeName(xc.Name));
						writer.WriteLine(lineOut.ToString());
						lineCount++;
					}
				}



			}



			writer.Close();

			return lineCount;
		}

		private string CSVsafeName(string name)
		{
			int i = name.IndexOf('"');
			if (i>=0 )
			{
				//name = "\"" + name + "\"";
				//name.Replace("\"", "''");
				// Less than ideal, but have no other workarounds right now
				name.Replace('"', '^');
			}
			else
			{
				i = name.IndexOf(',');
				if (i >= 0)
				{
					name = "\"" + name + "\"";
				}
			}
			return name;
		}

	}
}
