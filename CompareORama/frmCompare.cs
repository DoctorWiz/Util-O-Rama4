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
using FileHelper;
using ReadWriteCsv;
using FormHelper;
using RecentlyUsed;
using LOR4;
using xLights22;
using FuzzORama;
//using DarkMode;


namespace UtilORama4
{
	public partial class frmCompare : Form
	{
		#region Form Scope Variables
		private static Properties.Settings userSettings = Properties.Settings.Default;
		private const string helpPage = "http://wizlights.com/utilorama/blankorama";

		private string fileSequence = "";
		private string pathDatabase = "";
		private string fileVisualization = "";
		public string filergbeffects = "";
		public string fileReport = "";


		LOR4Sequence sequence = null;
		LOR4Visualization visualization = null;
		public List<LOR4Channel> channelList = new List<LOR4Channel>();
		public List<LOR4RGBChannel> RGBList = new List<LOR4RGBChannel>();
		public List<LOR4ChannelGroup> groupList = new List<LOR4ChannelGroup>();

		public List<xModel> xModelList = new List<xModel>();
		public List<xRGBModel> xRGBList = new List<xRGBModel>();
		public List<xPixelModel> xPixelList = new List<xPixelModel>();
		public List<xModelGroup> xGroupList = new List<xModelGroup>();
		private List<DMXUniverse> universes = new List<DMXUniverse>();
		// Just creating a convenient reference to the static list in the DMXUniverse class
		private List<DMXChannel> allChannels = DMXUniverse.AllChannels;
		private List<DMXChannel> datChannels = new List<DMXChannel>();
		// Just creating a convenient reference to the static list in the DMXChannel class
		public List<DMXDeviceType> deviceTypes = DMXChannel.DeviceTypes;
		public List<Matchup> matchups = new List<Matchup>();
		public bool shown = false;

		private int lastID = -1;

		//double minPreMatch = 85; // Properties.Settings.Default.FuzzyMinPrematch;
		//long preAlgorithm = FuzzyFunctions.USE_SUGGESTED_PREMATCH;
		//double minFinalMatch = 95; // Properties.Settings.Default.FuzzyMinFinal;
		//long finalAlgorithms = FuzzyFunctions.USE_SUGGESTED_FINALMATCH;
		#endregion

		public frmCompare()
		{
			InitializeComponent();
		}

		private void frmBlank_Load(object sender, EventArgs e)
		{
			//RestoreFormPosition();
			this.RestoreView();
			RestoreUserControlSettings();
			//DarkMode.DarkMode.SetDarkMode(this, true);
		}

		private void frmBlank_FormClosing(object sender, FormClosingEventArgs e)
		{
			//SaveFormPosition();
			this.SaveView();
			SaveUserControlSettings();
		}


		private void SaveUserControlSettings()
		{
			userSettings.Save();
		}

		private void RestoreUserControlSettings()
		{
			pathDatabase = userSettings.FileDatabase;
			if (pathDatabase.EndSubstring(1) != "\\") pathDatabase += "\\";
			txtFileDatabase.Text = pathDatabase;
			fileSequence = userSettings.FileSequence;
			txtLORfile.Text = fileSequence;
			fileVisualization = userSettings.FileVisualization;
			txtFileVisual.Text = fileVisualization;
			filergbeffects = userSettings.Filergbeffects;
			txtXFile.Text = filergbeffects;
			fileReport = userSettings.FileReport;
			txtSpreadsheet.Text = fileReport;
		}

		private void FirstShow()
		{
			ImBusy(true);
			if (pathDatabase.EndSubstring(1) != "\\") pathDatabase += "\\";

			if (Directory.Exists(pathDatabase))
			{
				string db = pathDatabase + "Channels.csv";
				if (Fyle.Exists(db))
				{
					LoadData(pathDatabase);
				}
			}

			if (Fyle.Exists(fileSequence))
			{
				LoadSequence(fileSequence);
			}

			if (Fyle.Exists(fileVisualization))
			{
				LoadVisualization(fileVisualization);
			}

			if (Fyle.Exists(txtXFile.Text))
			{
				LoadxLights(filergbeffects);
			}

			ImBusy(false);


		}

		private bool ReadyToCompare()
		{
			bool isC = false;
			if (allChannels.Count > 0)
			{
				if (sequence != null)
				{
					if (sequence.Channels.Count > 0) isC = true;
				}
				if (visualization != null)
				{
					if (visualization.VizChannels.Count > 0) isC = true;
				}
				if (xModelList.Count > 0) isC = true;
			}
			if (!isC)
			{
				if (sequence != null)
				{
					if (sequence.Channels.Count > 0)
					{
						if (visualization != null)
						{
							if (visualization.VizChannels.Count > 0) isC = true;
						}
						if (xModelList.Count > 0) isC = true;
					}
				}
			}
			btnBrowseSheet.Enabled = isC;
			grpSpreadsheet.Enabled = isC;
			if (isC) btnBrowseSheet.Focus();
			return isC;
		}

		public void ImBusy(bool busy = true)
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

		public void ImIdle(bool idle = true)
		{
			ImBusy(!idle);
		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(helpPage);

		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			frmAbout aboutBox = new frmAbout();
			aboutBox.Icon = this.Icon;
			aboutBox.Text = "About Compare-O-Rama";
			aboutBox.AppIcon = picAboutIcon.Image;
			aboutBox.ShowDialog(this);
			ImBusy(false);
		}

		private void txtLORfile_TextChanged(object sender, EventArgs e)
		{

		}

		private void txtXFile_TextChanged(object sender, EventArgs e)
		{

		}

		private int CompileLORlists(LOR4Sequence seq)
		{
			bool match = false;
			int count = 0;

			// [Regular] Channels (Non-RGB)
			if (seq.Channels.Count > 0)
			{
				channelList = new List<LOR4Channel>(); // Clear/Reset
				channelList.Add(seq.Channels[0]);
				for (int i = 1; i < seq.Channels.Count; i++)
				{
					match = false;
					for (int l = 0; l < channelList.Count; l++)
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
				RGBList = new List<LOR4RGBChannel>(); // Clear/Reset
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
				groupList = new List<LOR4ChannelGroup>(); // Clear/Reset
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
			xMemberBase member;



			StreamReader reader = new StreamReader(fileName);
			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				int ip = lineIn.IndexOf(" name=\"");
				if (ip >= 0)
				{
					theName = xAdmin.getKeyWord(lineIn, "name");

					ip = lineIn.IndexOf("<model ");
					if (ip >= 0)
					{
						// If it's a model...
						// Is it a Single Color LOR4Channel?
						ip = lineIn.IndexOf("StringType=\"Single Color");
						if (ip >= 0)
						{
							mbrType = xMemberType.Model; // Default
							string st = xAdmin.getKeyWord(lineIn, "StringType");
							xModel xch = new xModel(theName);
							//TODO Get its color!
							string hColor = xAdmin.getKeyWord(lineIn, "CustomColor");
							if (hColor.Length == 7)
							{
								xch.Color = xAdmin.ColorHTMLtoNet(hColor);
							}
							xch.StartChannel = xAdmin.getKeyWord(lineIn, "StartChannel");
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
								xPixelModel xpx = new xPixelModel(theName);
								xPixelList.Add(xpx);
								//count++;
								xpx.StartChannel = xAdmin.getKeyWord(lineIn, "StartChannel");
								xpx.Color = Color.FromArgb(128, 64, 64);
								member = xpx;
							}
							else // not pixels
							{
								// Is it an RGB floodlight or single pixel?
								ip = lineIn.IndexOf("StringType=\"3 Channel RGB");
								if (ip >= 0)
								{
									mbrType = xMemberType.RGBmodel;
									xRGBModel xrgb = new xRGBModel(theName);
									xRGBList.Add(xrgb);
									count++;
									xrgb.StartChannel = xAdmin.getKeyWord(lineIn, "StartChannel");
									xrgb.Color = Color.FromArgb(64, 0, 0);
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
			string initDir = xAdmin.ShowDirectory;
			if (Directory.Exists(initDir))
			{
				//
			}
			else
			{
				// Can't find xLights show directory, lets try elsewhere
				// Check LOR directory
				initDir = LOR4.LOR4Admin.DefaultSequencesPath;
				if (Directory.Exists(initDir))
				{
					// Cool!  Good for us!
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
				LoadxLights(dlgFileOpen.FileName);
			} // end if (result = DialogResult.OK)

		}

		private int LoadxLights(string filexL)
		{
			int errs = 0;
			ImBusy(true);
			CompilexLightsLists(filexL);
			if (xModelList.Count > 0)
			{
				filergbeffects = filexL;
				txtXFile.Text = filergbeffects;
				userSettings.Filergbeffects = filergbeffects;
				userSettings.Save();
				string txt = xModelList.Count.ToString() + "/" +
										xGroupList.Count.ToString();
				lblInfoxL.Text = txt;
				if (Fyle.isWiz) lblInfoxL.Visible = true;
				ReadyToCompare();
			}
			ImBusy(false);
			return errs;
		}

		private void btnBrowseLOR_Click(object sender, EventArgs e)
		{
			bool stopLooking = false;
			// Start with LOR Showtime directory
			string initDir = LOR4.LOR4Admin.DefaultSequencesPath;
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
				initDir = xAdmin.ShowDirectory;
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
			filt += "|Animated Sequences only (*.las)|*.las|LOR4Channel Configurations only(*.lcc)|*.lcc";
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
				LoadSequence(dlgFileOpen.FileName);
			} // end if (result = DialogResult.OK)


		}

		private void LoadSequence(string fileSeqName)
		{
			ImBusy(true);
			sequence = new LOR4Sequence(fileSeqName);
			if (sequence.Channels.Count > 0)
			{
				fileSequence = fileSeqName;
				txtLORfile.Text = fileSequence;
				userSettings.FileSequence = fileSequence;
				userSettings.Save();
				CompileLORlists(sequence);
				string stat = channelList.Count.ToString() + " Channels, ";
				stat += RGBList.Count.ToString() + " RGB Channels, and ";
				stat += groupList.Count.ToString() + " Channel Groups.";
				pnlStatus.Text = stat;

				stat = sequence.Tracks.Count.ToString() + "/" +
							sequence.ChannelGroups.Count.ToString() + "/" +
							sequence.RGBchannels.Count.ToString() + "/" +
							sequence.Channels.Count.ToString() + "/" +
							sequence.TimingGrids.Count.ToString();
				lblInfoSeq.Text = stat;
				if (Fyle.isWiz) lblInfoSeq.Visible = true;
				ReadyToCompare();
			}
			ImBusy(false);
		}

		public int CompareNoDat()
		{
			int exactMatches = 0;
			int fuzzyMatches = 0;
			string status = "";







			// LOR Channels to xLights Modesl or Model Groups, Exact Matches
			//   Channels to Models first
			for (int l = 0; l < channelList.Count; l++)
			{
				string LORname = channelList[l].Name;
				status = "Searching for " + LORname;
				StatusUpdate(status);

				for (int x = 0; x < xModelList.Count; x++)
				{
					string xName = xModelList[x].Name;
					if (LORname.CompareTo(xName) == 0)
					{
						channelList[l].Tag = xModelList[x];
						channelList[l].SelectedState = CheckState.Checked;
						xModelList[x].Tag = channelList[l];
						xModelList[x].SelectedState = CheckState.Checked;
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
				StatusUpdate(status);

				for (int x = 0; x < xGroupList.Count; x++)
				{
					string xName = xGroupList[x].Name;
					if (LORname.CompareTo(xName) == 0)
					{
						channelList[l].Tag = xGroupList[x];
						channelList[l].SelectedState = CheckState.Checked;
						xGroupList[x].Tag = channelList[l];
						xGroupList[x].SelectedState = CheckState.Checked;
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
				if (channelList[l].SelectedState != CheckState.Checked)
				{
					string LORname = channelList[l].Name;
					status = "Fuzzy Searching for " + LORname;
					StatusUpdate(status);

					double highScore = 0;
					int highModelMatch = -1;
					int highGroupMatch = -1;
					for (int x = 0; x < xModelList.Count; x++)
					{
						if (xModelList[x].SelectedState != CheckState.Checked)
						{
							string xName = xModelList[x].Name;
							double preScore = LORname.FuzzyScoreFast(xName);
							// if the score is above the minimum PreMatch
							if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								double finalScore = LORname.FuzzyScoreAccurate(xName);
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
						if (xGroupList[x].SelectedState != CheckState.Checked)
						{
							string xName = xGroupList[x].Name;
							double preScore = LORname.FuzzyScoreFast(xName);
							// if the score is above the minimum PreMatch
							if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								double finalScore = LORname.FuzzyScoreAccurate(xName);
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
						channelList[l].SelectedState = CheckState.Checked;
						xGroupList[highGroupMatch].Tag = channelList[l];
						xGroupList[highGroupMatch].SelectedState = CheckState.Checked;
						fuzzyMatches++;
					}
					else
					{
						if (highModelMatch >= 0)
						{
							channelList[l].Tag = xModelList[highModelMatch];
							channelList[l].SelectedState = CheckState.Checked;
							xModelList[highModelMatch].Tag = channelList[l];
							xModelList[highModelMatch].SelectedState = CheckState.Checked;
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
						RGBList[l].SelectedState = CheckState.Checked;
						xRGBList[x].Tag = RGBList[l];
						xRGBList[x].SelectedState = CheckState.Checked;
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
				if (RGBList[l].SelectedState != CheckState.Checked)
				{
					string LORname = RGBList[l].Name;
					status = "Fuzzy Searching for " + LORname;
					StatusUpdate(status);

					double highScore = 0;
					int highMatch = -1;
					for (int x = 0; x < xRGBList.Count; x++)
					{
						if (xRGBList[x].SelectedState != CheckState.Checked)
						{
							string xName = xRGBList[x].Name;
							double preScore = LORname.FuzzyScoreFast(xName);
							// if the score is above the minimum PreMatch
							if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								double finalScore = LORname.FuzzyScoreAccurate(xName);
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
						RGBList[l].SelectedState = CheckState.Checked;
						xRGBList[highMatch].Tag = RGBList[l];
						xRGBList[highMatch].SelectedState = CheckState.Checked;
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
						groupList[l].SelectedState = CheckState.Checked;
						xGroupList[x].Tag = groupList[l];
						xGroupList[x].SelectedState = CheckState.Checked;
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
				if (groupList[l].SelectedState != CheckState.Checked)
				{
					string LORname = groupList[l].Name;
					status = "Fuzzy Searching for " + LORname;
					StatusUpdate(status);

					double highScore = 0;
					int highMatch = -1;
					for (int x = 0; x < xGroupList.Count; x++)
					{
						if (xGroupList[x].SelectedState != CheckState.Checked)
						{
							string xName = xGroupList[x].Name;
							double preScore = LORname.FuzzyScoreFast(xName);
							// if the score is above the minimum PreMatch
							if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								double finalScore = LORname.FuzzyScoreAccurate(xName);
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
						groupList[l].SelectedState = CheckState.Checked;
						xGroupList[highMatch].Tag = groupList[l];
						xGroupList[highMatch].SelectedState = CheckState.Checked;
						fuzzyMatches++;
					}
				}
			}



			return (exactMatches + fuzzyMatches);
		}

		private void StatusUpdate(string msg)
		{
			pnlStatus.Text = msg;
			staStatus.Refresh();
		}

		public int CompileReportWithDat(bool includeExact = true)
		{
			int exactMatches = 0;
			int fuzzyMatches = 0;
			int lineCount = 0;
			string status = "";
			string outInfo = "";
			ImBusy(true);
			// Step 1: Database Channels to LOR Channels Exact Matches
			for (int datIdx = 0; datIdx < datChannels.Count; datIdx++)
			{
				DMXChannel datChan = datChannels[datIdx];
				string datName = datChan.Name;
				Matchup mup = new Matchup();
				mup.NameDat = datName;
				mup.IndexDat = datIdx;
				outInfo = datChan.UniverseNumber.ToString() + "/" +
									datChan.DMXAddress.ToString() + "/" +
									datChan.xLightsAddress.ToString();
				mup.OutputDat = outInfo;
				mup.ColorDat = datChan.ColorHTML;

				// Search Sequence Channels
				status = "Searching Sequence for " + datName;
				StatusUpdate(status);

				LOR4Channel chanLor = sequence.FindChannel(datName);
				if (chanLor != null)
				{
					mup.NameLOR = chanLor.Name;
					mup.IndexLOR = chanLor.Index;
					mup.SavedIndex = chanLor.SavedIndex;
					outInfo = chanLor.UniverseNumber.ToString() + "/" +
										chanLor.DMXAddress.ToString();
					mup.OutputLOR = outInfo;
					mup.ColorLOR = LOR4Admin.Color_LORtoHTML(chanLor.color);
					mup.ExactLOR = true;
					chanLor.ExactMatch = true;
					chanLor.SelectedState = CheckState.Checked;
					chanLor.Tag = mup;
					exactMatches++;
				} // End if exact match found
				matchups.Add(mup);
			} // End loop thru database channels

			// Step 2: Database Channels to LOR Channels Fuzzy Matches
			for (int datIdx = 0; datIdx < datChannels.Count; datIdx++)
			{
				string datName = datChannels[datIdx].Name;
				Matchup mup = matchups[datIdx];
				if (mup.IndexLOR < 0)
				{
					status = "Fuzzy Searching Sequence for " + datName;
					StatusUpdate(status);


					double highScore = 0;
					int highMatch = -1;
					for (int li = 0; li < sequence.Channels.Count; li++)
					{
						LOR4Channel chanLor = sequence.Channels[li];
						if (chanLor.SelectedState != CheckState.Checked)
						{
							string LORname = chanLor.Name;
							double preScore = datName.FuzzyScoreFast(LORname);
							// if the score is above the minimum PreMatch
							if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								double finalScore = datName.FuzzyScoreAccurate(LORname);
								if (finalScore > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								{
									if (finalScore > highScore)
									{
										highScore = finalScore;
										highMatch = li;
									} // > high score so far
								} // >= min final match
							} // >= min prematch
						} // channel not selected, thus not matched
					} // end loop thru sequence channels
					if (highMatch >= 0)
					{
						if (highScore > 95)
						{
							LOR4Channel chanLor = sequence.Channels[highMatch];
							mup.IndexLOR = chanLor.Index;
							mup.SavedIndex = chanLor.SavedIndex;
							outInfo = chanLor.UniverseNumber.ToString() + "/" +
												chanLor.DMXAddress.ToString();
							mup.OutputLOR = outInfo;
							mup.ColorLOR = LOR4Admin.Color_LORtoHTML(chanLor.color);
							mup.ExactLOR = false;
							chanLor.ExactMatch = false;
							chanLor.SelectedState = CheckState.Checked;
							chanLor.Tag = mup;
							fuzzyMatches++;

						} // end if high enough final score
					} // end if high match index
				} // end if exact match not already found
			} // end loop thru database channels

			// Step 3: Database Channels to Viz Channels, Groups, Objects Exact Matches
			if (visualization != null)
			{
				for (int datIdx = 0; datIdx < datChannels.Count; datIdx++)
				{
					string datName = datChannels[datIdx].Name;
					Matchup mup = matchups[datIdx];
					// Search Visualization Channels, Groups, Objects
					status = "Searching Visualization for " + datName;
					StatusUpdate(status);

					LOR4VizChannel vch = visualization.FindVizChannel(datName, false);
					if (vch != null)
					{
						mup.IndexViz = vch.Index;
						mup.NameViz = vch.Name;
						outInfo = vch.UniverseNumber.ToString() + "/" +
											vch.DMXAddress.ToString();
						mup.OutputViz = outInfo;
						mup.ColorViz = LOR4Admin.Color_LORtoHTML(vch.color);
						mup.TypeViz = 1;
						mup.ExactViz = true;
						vch.ExactMatch = true;
						vch.SelectedState = CheckState.Checked;
						vch.Tag = mup;
						exactMatches++;
					}
					if (vch == null)
					{
						LOR4VizDrawObject vdo = visualization.FindDrawObject(datName);
						if (vdo != null)
						{
							mup.IndexViz = vdo.Index;
							mup.NameViz = vdo.Name;
							outInfo = vdo.UniverseNumber.ToString() + "/" +
												vdo.DMXAddress.ToString();
							mup.OutputViz = outInfo;
							mup.ColorViz = LOR4Admin.Color_LORtoHTML(vdo.color);
							mup.TypeViz = 3;
							mup.ExactViz = true;
							vdo.ExactMatch = true;
							vdo.SelectedState = CheckState.Checked;
							vdo.Tag = mup;
							exactMatches++;
						} // end if it matched something
						if (vdo == null)
						{
							LOR4VizItemGroup vgr = visualization.FindItemGroup(datName);
							if (vgr != null)
							{
								mup.IndexViz = vgr.Index;
								mup.NameViz = vgr.Name;
								outInfo = vgr.UniverseNumber.ToString() + "/" +
													vgr.DMXAddress.ToString();
								mup.OutputViz = outInfo;
								mup.ColorViz = LOR4Admin.Color_LORtoHTML(vgr.color);
								mup.TypeViz = 2;
								mup.ExactViz = true;
								vgr.ExactMatch = true;
								vgr.SelectedState = CheckState.Checked;
								vgr.Tag = mup;
								exactMatches++;
							}
						}
					}
				} // end loop thru database channels

				// Step 4: Database Channels to Viz Channels, Groups, and Objects-- Fuzzy Matches
				for (int datIdx = 0; datIdx < datChannels.Count; datIdx++)
				{
					string datName = datChannels[datIdx].Name;
					Matchup mup = matchups[datIdx];
					if (mup.IndexViz < 0)
					{
						status = "Fuzzy Searching Visualization for " + datName;
						StatusUpdate(status);


						double highScore = 0;
						int highMatch = -1;
						int matchType = -1;
						for (int v = 1; v < visualization.VizChannels.Count; v++)
						{
							LOR4VizChannel member = visualization.VizChannels[v];
							if (member.SelectedState != CheckState.Checked)
							{
								string vName = member.Name;
								double preScore = datName.FuzzyScoreFast(vName);
								// if the score is above the minimum PreMatch
								if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
								{
									double finalScore = datName.FuzzyScoreAccurate(vName);
									if (finalScore > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
									{
										if (finalScore > highScore)
										{
											highScore = finalScore;
											highMatch = v;
											matchType = 1;
										} // > high score so far
									} // >= min final match
								} // >= min prematch
							} // channel not selected, thus not matched
						} // end loop thru VizChannels

						for (int v = 1; v < visualization.VizItemGroups.Count; v++)
						{
							LOR4VizItemGroup member = visualization.VizItemGroups[v];
							if (member.SelectedState != CheckState.Checked)
							{
								string vName = member.Name;
								double preScore = datName.FuzzyScoreFast(vName);
								// if the score is above the minimum PreMatch
								if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
								{
									double finalScore = datName.FuzzyScoreAccurate(vName);
									if (finalScore > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
									{
										if (finalScore > highScore)
										{
											highScore = finalScore;
											highMatch = v;
											matchType = 2;
										} // > high score so far
									} // >= min final match
								} // >= min prematch
							} // channel not selected, thus not matched
						} // end loop thru VizItemGroups

						for (int v = 1; v < visualization.VizDrawObjects.Count; v++)
						{
							LOR4VizDrawObject member = visualization.VizDrawObjects[v];
							if (member.SelectedState != CheckState.Checked)
							{
								string vName = member.Name;
								double preScore = datName.FuzzyScoreFast(vName);
								// if the score is above the minimum PreMatch
								if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
								{
									double finalScore = datName.FuzzyScoreAccurate(vName);
									if (finalScore > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
									{
										if (finalScore > highScore)
										{
											highScore = finalScore;
											highMatch = v;
											matchType = 3;
										} // > high score so far
									} // >= min final match
								} // >= min prematch
							} // channel not selected, thus not matched
						} // end loop thru VizDrawObjects


						if (highMatch >= 0)
						{
							if (highScore > 95)
							{
								iLOR4Member member = null;
								if (matchType == 1)
								{
									member = visualization.VizChannels[highMatch];
								}
								if (matchType == 2)
								{
									member = visualization.VizItemGroups[highMatch];
								}
								if (matchType == 3)
								{
									member = visualization.VizDrawObjects[highMatch];
								}

								mup.NameViz = member.Name;
								outInfo = member.UniverseNumber.ToString() + "/" +
													member.DMXAddress.ToString();
								mup.OutputViz = outInfo;
								mup.ColorViz = LOR4Admin.Color_LORtoHTML(member.color);
								mup.IndexViz = highMatch;
								mup.TypeViz = matchType;
								mup.ExactViz = false;
								member.ExactMatch = false;
								member.SelectedState = CheckState.Checked;
								member.Tag = mup;
								fuzzyMatches++;
							} // end if high enough final score
						} // end if high match index
					} // end if exact match not already found
				} // end loop thru database channels
			} // end have a visualization

			// Step 5: Database Channels to xLights Models & Groups Exact Matches
			for (int datIdx = 0; datIdx < datChannels.Count; datIdx++)
			{
				bool skip = false;
				string datName = datChannels[datIdx].Name;
				string dName = datName;
				//if (dName.EndSubstring(4) == " (R)") dName.Replace(" (R)", " (RGB)");
				//if (dName.EndSubstring(4) == " (G)") skip = true;
				//if (dName.EndSubstring(4) == " (B)") skip = true;
				if (!skip)
				{
					Matchup mup = matchups[datIdx];
					// xLights Models and ModelGroups
					status = "Searching xLights for " + datName;
					StatusUpdate(status);

					for (int x = 0; x < xModelList.Count; x++)
					{
						string xName = xModelList[x].Name;
						if (dName.CompareTo(xName) == 0)
						{
							mup.IndexxLights = x;
							outInfo = xModelList[x].StartChannel.ToString();
							mup.OutputxLights = outInfo;
							mup.ColorxLights = LOR4Admin.Color_NettoHTML(xModelList[x].Color);
							mup.TypexLights = 1;
							mup.ExactxLights = true;
							xModelList[x].ExactMatch = true;
							xModelList[x].SelectedState = CheckState.Checked;
							xModelList[x].Tag = mup;
							exactMatches++;
							// Force exit of loop
							x = xModelList.Count;
						}
					}
					if (mup.IndexxLights < 0)
					{
						for (int x = 0; x < xGroupList.Count; x++)
						{
							string xName = xGroupList[x].Name;
							if (dName.CompareTo(xName) == 0)
							{
								mup.IndexxLights = x;
								mup.NamexLights = xGroupList[x].Name;
								outInfo = xGroupList[x].StartChannel.ToString();
								mup.OutputxLights = outInfo;
								mup.ColorxLights = LOR4Admin.Color_NettoHTML(xGroupList[x].Color);
								mup.TypexLights = 2;
								mup.ExactxLights = true;
								xModelList[x].ExactMatch = true;
								xModelList[x].SelectedState = CheckState.Checked;
								xModelList[x].Tag = mup;
								exactMatches++;
								// Force exit of loop
								x = xGroupList.Count;
							}
						}
					}
				} // skip blue and green
			} // dat channel loop

			// Step 6: Database Channels to xLights Models & Groups-- Fuzzy Matches
			for (int datIdx = 0; datIdx < datChannels.Count; datIdx++)
			{
				string datName = datChannels[datIdx].Name;
				string dName = datName;
				if (dName.EndSubstring(4) == " (R)") dName.Replace(" (R)", " (RGB)");
				if (dName.EndSubstring(4) == " (G)") dName.Replace(" (R)", " (RGB)");
				if (dName.EndSubstring(4) == " (B)") dName.Replace(" (R)", " (RGB)");
				Matchup mup = matchups[datIdx];
				if (mup.IndexViz < 0)
				{
					status = "Fuzzy Searching xLights for " + datName;
					StatusUpdate(status);


					double highScore = 0;
					int highMatch = -1;
					int matchType = -1;
					for (int x = 0; x < xModelList.Count; x++)
					{
						xModel xm = xModelList[x];
						if (xm.SelectedState != CheckState.Checked)
						{
							string xName = xm.Name;
							double preScore = dName.FuzzyScoreFast(xName);
							// if the score is above the minimum PreMatch
							if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								double finalScore = dName.FuzzyScoreAccurate(xName);
								if (finalScore > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								{
									if (finalScore > highScore)
									{
										highScore = finalScore;
										highMatch = x;
										matchType = 1;
									} // > high score so far
								} // >= min final match
							} // >= min prematch
						} // channel not selected, thus not matched
					} // end loop thru VizChannels

					for (int x = 0; x < xGroupList.Count; x++)
					{
						xModelGroup xg = xGroupList[x];
						if (xg.SelectedState != CheckState.Checked)
						{
							string xName = xGroupList[x].Name;
							double preScore = dName.FuzzyScoreFast(xName);
							// if the score is above the minimum PreMatch
							if (preScore > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								double finalScore = dName.FuzzyScoreAccurate(xName);
								if (finalScore > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								{
									if (finalScore > highScore)
									{
										highScore = finalScore;
										highMatch = x;
										matchType = 2;
									} // > high score so far
								} // >= min final match
							} // >= min prematch
						} // channel not selected, thus not matched
					} // end loop thru VizItemGroups

					if (highMatch >= 0)
					{
						if (highScore > 95)
						{
							xMemberBase member = null;
							if (matchType == 1)
							{
								member = xModelList[highMatch];
							}
							if (matchType == 2)
							{
								member = xGroupList[highMatch];
							}

							mup.IndexxLights = highMatch;
							mup.TypexLights = matchType;
							outInfo = member.StartChannel;
							mup.OutputxLights = outInfo;
							mup.ColorxLights = LOR4Admin.Color_NettoHTML(member.Color);
							mup.ExactxLights = false;
							member.ExactMatch = false;
							member.SelectedState = CheckState.Checked;
							member.Tag = mup;
							fuzzyMatches++;
						} // end if high enough final score
					} // end if high match index
				} // end if exact match not already found
			} // end loop thru database channels

			// Part 7, add unmatched LOR Channels
			for (int cl = 0; cl < sequence.Channels.Count; cl++)
			{
				LOR4Channel chan = sequence.Channels[cl];
				if (chan.SelectedState != CheckState.Checked)
				{
					Matchup mup = new Matchup();
					mup.NameLOR = chan.Name;
					mup.IndexLOR = chan.Index;
					mup.SavedIndex = chan.SavedIndex;
					mup.OutputLOR = chan.UniverseNumber.ToString() + "/" +
						chan.DMXAddress.ToString();
					mup.ColorLOR = LOR4Admin.Color_LORtoHTML(chan.color);
					matchups.Add(mup);
				}
			}

			// Part 8, add unmatched Viz Channels
			int c = 0;
			while (true == false)
			//for (int c = 1; c < visualization.VizChannels.Count; c++)
			{
				LOR4VizChannel chan = visualization.VizChannels[c];
				if (chan.SelectedState != CheckState.Checked)
				{
					Matchup mup = new Matchup();
					mup.NameViz = chan.Name;
					mup.IndexViz = chan.Index;
					mup.TypeViz = 1;
					mup.OutputViz = chan.UniverseNumber.ToString() + "/" +
						chan.DMXAddress.ToString();
					mup.ColorLOR = LOR4Admin.Color_LORtoHTML(chan.color);
					matchups.Add(mup);
				}
			}
			while (true == false)
			//for (int c = 1; c < visualization.VizItemGroups.Count; c++)
			{
				LOR4VizItemGroup grp = visualization.VizItemGroups[c];
				if (grp.SelectedState != CheckState.Checked)
				{
					Matchup mup = new Matchup();
					mup.NameViz = grp.Name;
					mup.IndexViz = grp.Index;
					mup.TypeViz = 2;
					mup.OutputViz = grp.UniverseNumber.ToString() + "/" +
						grp.DMXAddress.ToString();
					matchups.Add(mup);
				}
			}
			while (true == false)
			//for (int c = 1; c < visualization.VizDrawObjects.Count; c++)
			{
				LOR4VizDrawObject vdo = visualization.VizDrawObjects[c];
				if (vdo.SelectedState != CheckState.Checked)
				{
					Matchup mup = new Matchup();
					mup.NameViz = vdo.Name;
					mup.IndexViz = vdo.Index;
					mup.TypeViz = 3;
					mup.OutputViz = vdo.UniverseNumber.ToString() + "/" +
						vdo.DMXAddress.ToString();
					matchups.Add(mup);
				}
			}

			// Part 9, add unmatched xLights Models and groups
			while (true == false)
			//for (int c = 0; c < xModelList.Count; c++)
			{
				xModel chan = xModelList[c];
				if (chan.SelectedState != CheckState.Checked)
				{
					Matchup mup = new Matchup();
					mup.NamexLights = chan.Name;
					mup.IndexxLights = c;
					mup.TypexLights = 1;
					mup.OutputxLights = chan.StartChannel;
					mup.ColorLOR = LOR4Admin.Color_NettoHTML(chan.Color);
					matchups.Add(mup);
				}
			}
			while (true == false)
			//for (int c = 0; c < xGroupList.Count; c++)
			{
				xModelGroup grp = xGroupList[c];
				if (grp.SelectedState != CheckState.Checked)
				{
					Matchup mup = new Matchup();
					mup.NamexLights = grp.Name;
					mup.IndexxLights = c;
					mup.TypexLights = 2;
					mup.OutputxLights = grp.StartChannel;
					matchups.Add(mup);
				}
			}

			ImBusy(false);
			return (exactMatches + fuzzyMatches);

		}

		private int WriteReport(string reportName, bool excludeExact = false)
		{
			int lineCount = 0;
			// Part 10, Build the CSV Report
			//string fileReport = "W:\\Documents\\Christmas\\2021\\Docs\\Compare-O-Rama Report.csv";
			StringBuilder lineOut = new StringBuilder();
			StreamWriter writer = new StreamWriter(reportName);

			// Header Columns A, B, C,
			lineOut.Append("Channel Name - Database,Output,Color,");
			// Header Columns D, E, F, G, H, I,
			lineOut.Append("Channel Name - Sequence,Match Name,Output,Match Output,Color,Match Color,");
			// Header Columns J, K, L, M, N, O, P
			lineOut.Append("Channel Name - Visualizer,Match Name,Type,Output,Match Output,Color,Match Color,");
			// Header Columns Q, R, S, T, U, V, W
			lineOut.Append("Channel Name - xLights,Match Name,Type,Output,Match Output,Color,Match Color");
			writer.WriteLine(lineOut.ToString());

			string clr = "";

			for (int m = 0; m < matchups.Count; m++)
			{
				lineOut.Clear();
				Matchup mup = matchups[m];
				bool writeMe = !excludeExact;
				if (!mup.ExactLOR) writeMe = true;
				if (visualization != null)
				{
					if (!mup.ExactViz) writeMe = true;
				}
				if (xModelList.Count > 0)
				{
					if (!mup.ExactxLights) writeMe = true;
				}

				if (writeMe)
				{
					// If matchup includes database (all the first ones will, later ones will not)
					if (mup.IndexDat >= 0)
					{
						DMXChannel chan = datChannels[mup.IndexDat];
						lineOut.Append(LOR4Admin.XMLifyName(mup.NameDat));  // Field 0, Column A
						lineOut.Append(",");
						lineOut.Append(mup.OutputDat); // Field 1, Column B
						lineOut.Append(",");
						//lineOut.Append(chan.DMXController.LetterID);
						//lineOut.Append("-");
						//lineOut.Append(chan.OutputNum.ToString());
						//lineOut.Append(",");
						lineOut.Append(chan.ColorHTML); // Field 2, Column C
						lineOut.Append(",");
						//lineOut.Append(chan.ColorName);
						//lineOut.Append(",");
					}
					else
					{
						lineOut.Append(" --> NOT in Database!,,,");
					}

					// If matchup includes a LOR channel
					if (mup.IndexLOR >= 0)
					{
						// LOR Channel name, and does it match?
						lineOut.Append(LOR4Admin.XMLifyName(mup.NameLOR)); // Field 3, Column D
						lineOut.Append(",");
						if (mup.IndexDat < 0)
						{
							//lineOut.Append("NO MATCH NAME!"); // Field 4, Column E
							lineOut.Append(" ---");
						}
						else
						{
							if (mup.ExactLOR) lineOut.Append("Exact");
							else lineOut.Append("Fuzzy");
						}
						lineOut.Append(",");

						// LOR Output, and does it match?
						lineOut.Append(mup.OutputLOR); // Field 5, Column F
						lineOut.Append(",");
						if (mup.IndexDat < 0)
						{
						}
						else
						{
							int ip = mup.OutputDat.LastIndexOf("/");
							string dout = mup.OutputDat.Substring(0, ip);
							if (dout == mup.OutputLOR) lineOut.Append("Yes"); // Field 6, Column G
							else lineOut.Append("NO!");
						}
						lineOut.Append(",");

						// LOR Color, and does it match?
						lineOut.Append(mup.ColorLOR); // Field 7, Column H
						lineOut.Append(",");
						if (mup.IndexDat < 0)
						{
						}
						else
						{
							if (mup.ColorDat == mup.ColorLOR) lineOut.Append("Yes");
							else lineOut.Append("NO!");
						}
						lineOut.Append(",");
					}
					else
					{
						lineOut.Append(" --> NOT FOUND!,NOFIND!,,,,,");
					}

					// If matchup includes a VIZ channel
					if (mup.IndexViz >= 0)
					{
						// VizChannel name, and does it match?
						lineOut.Append(LOR4Admin.XMLifyName(mup.NameViz));
						lineOut.Append(",");
						if (mup.IndexDat < 0)
						{
							//lineOut.Append("NO MATCH NAME!");
							lineOut.Append(" ---");
						}
						else
						{
							if (mup.ExactLOR) lineOut.Append("Exact");
							else lineOut.Append("Fuzzy");
						}
						lineOut.Append(",");

						// VizThingy Type
						if (mup.TypeViz == 1) lineOut.Append("Channel,");
						if (mup.TypeViz == 3) lineOut.Append("DrawObject,");
						if (mup.TypeViz == 2) lineOut.Append("ItemGroup,");

						// Viz Output, and does it match?
						lineOut.Append(mup.OutputViz);
						lineOut.Append(",");
						if (mup.IndexDat < 0)
						{
						}
						else
						{
							int ip = mup.OutputDat.LastIndexOf("/");
							string dout = mup.OutputDat.Substring(0, ip);
							if (dout == mup.OutputViz) lineOut.Append("Yes"); // Field 6, Column G
							else lineOut.Append("NO!");
						}
						lineOut.Append(",");

						// Viz Color, and does it match?
						lineOut.Append(mup.ColorViz);
						lineOut.Append(",");
						if (mup.IndexDat < 0)
						{
						}
						else
						{
							if (mup.ColorDat == mup.ColorViz) lineOut.Append("Yes");
							else lineOut.Append("NO!");
						}
						lineOut.Append(",");
					}
					else
					{
						lineOut.Append(" --> NOT FOUND,NOFIND!,,,,,,");
					}

					// If matchup includes a xLights channel
					if (mup.IndexxLights >= 0)
					{
						// xLights name, and does it match?
						lineOut.Append(LOR4Admin.XMLifyName(mup.NamexLights));
						lineOut.Append(",");
						if (mup.IndexDat < 0)
						{
							//lineOut.Append("NO MATCH NAME!");
							lineOut.Append(" ---");
						}
						else
						{
							if (mup.ExactLOR) lineOut.Append("Exact");
							else lineOut.Append("Fuzzy");
						}
						lineOut.Append(",");

						// xLights Type
						if (mup.TypexLights == 1) lineOut.Append("Model,");
						if (mup.TypexLights == 2) lineOut.Append("Group,");

						// xLights Output
						lineOut.Append(mup.OutputxLights);
						lineOut.Append(",");
						if (mup.IndexDat < 0)
						{
						}
						else
						{
							string sc1 = "";
							if (mup.OutputxLights.Length > 0) sc1 = mup.OutputxLights.Substring(0, 1);
							if ((sc1 == "") || (sc1 == "@") || (sc1 == "&"))
							{
								lineOut.Append("?");
							}
							else
							{
								int ip = mup.OutputDat.LastIndexOf("/");
								string dout = mup.OutputDat.Substring(ip + 1);
								if (dout == mup.OutputxLights) lineOut.Append("Yes");
								else lineOut.Append("NO!");
							}
						}
						lineOut.Append(",");

						// xLights Color
						lineOut.Append(mup.ColorxLights);
						lineOut.Append(",");
						if (mup.IndexDat < 0)
						{
						}
						else
						{
							if (mup.ColorxLights.Length == 9)
							{
								string xc = "#" + mup.ColorxLights.Substring(3);
								if (mup.ColorDat == xc) lineOut.Append("Yes");
								else lineOut.Append("NO!");
							}
						}
						lineOut.Append(",");
					}
					else
					{
						lineOut.Append(" --> NOT FOUND,NOFIND!,,,,");
					}

					writer.WriteLine(lineOut.ToString());
					lineCount++;
				} // WriteMe
			}
			writer.Close();

			return lineCount;
		}

		private void btnBrowseSheet_Click(object sender, EventArgs e)
		{
			int matchCount = CompileReportWithDat(true);
			if (matchCount > 0)
			{
				bool stopLooking = false;

				string lastSheet = userSettings.FileReport;
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
						int lineCount = 0;
						fileReport = dlgFileSave.FileName;
						txtSpreadsheet.Text = fileReport;
						userSettings.FileReport = fileReport;
						userSettings.Save();
						//int m = CompileSpreadsheet(f, true);
						if (allChannels.Count > 0)
						{
							lineCount = WriteReport(fileReport, false);
							pnlStatus.Text = "Spreadsheet saved with " + lineCount.ToString() + " lines.";
							System.Diagnostics.Process.Start(@fileReport);
						}
						ImBusy(false);
					}
				} // end if (result = DialogResult.OK)
			}

		}

		private int CompileSpreadsheet(string fileName, bool LORfirst)
		{
			int lineCount = 0;
			StringBuilder lineOut = new StringBuilder();
			StreamWriter writer = new StreamWriter(fileName);
			if (LORfirst)
			{
				lineOut.Append("LOR LOR4Channel, Match, xLights Model, Color");
				writer.WriteLine(lineOut.ToString());
				lineCount++;

				for (int l = 0; l < channelList.Count; l++)
				{
					lineOut.Clear();
					LOR4Channel lc = channelList[l];
					lineOut.Append(CSVsafeName(lc.Name));
					lineOut.Append(',');
					if (lc.SelectedState = CheckState.Checked)
					{
						xMemberBase xc = (xMemberBase)lc.Tag;
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
						lineOut.Append(',');
					}
					else
					{
						lineOut.Append("None,,");
					}
					string clr = "#" + LOR4.LOR4Admin.Color_LORtoHTML(lc.color);
					lineOut.Append(clr);
					writer.WriteLine(lineOut.ToString());
					lineCount++;
				}
				for (int x = 0; x < xModelList.Count; x++)
				{
					xMemberBase xc = xModelList[x];
					if (xc.SelectedState != CheckState.Checked)
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
				lineOut.Append("LOR RGB LOR4Channel, Match, xLights Model");
				writer.WriteLine(lineOut.ToString());
				lineCount++;

				for (int l = 0; l < RGBList.Count; l++)
				{
					lineOut.Clear();
					LOR4RGBChannel lc = RGBList[l];
					lineOut.Append(CSVsafeName(lc.Name));
					lineOut.Append(',');
					if (lc.SelectedState = CheckState.Checked)
					{
						xMemberBase xc = (xMemberBase)lc.Tag;
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
					xMemberBase xc = xRGBList[x];
					if (xc.SelectedState != CheckState.Checked)
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
					LOR4ChannelGroup lc = groupList[l];
					lineOut.Append(CSVsafeName(lc.Name));
					lineOut.Append(',');
					if (lc.SelectedState == CheckState.Checked)
					{
						xMemberBase xc = (xMemberBase)lc.Tag;
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
					xMemberBase xc = xGroupList[x];
					if (xc.SelectedState != CheckState.Checked)
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
			if (i >= 0)
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

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private string PathToDB(int year)
		{
			string ret = Fyle.DefaultDocumentsPath;
			ret += "Christmas\\";
			ret += year.ToString();
			ret += "\\Docs\\ChannelDB\\";
			return ret;
		}

		public int LoadData(string filePath)
		{
			ImBusy(true);
			int errs = LoadUniverses(filePath);
			errs += LoadControllers(filePath);
			errs += LoadDeviceTypes(filePath);
			errs += LoadChannels(filePath);

			for (int c = 0; c < allChannels.Count; c++)
			{
				datChannels.Add(allChannels[c]);
			}
			DMXChannel.SortByName = true;
			datChannels.Sort();

			pathDatabase = filePath;
			txtFileDatabase.Text = pathDatabase;
			userSettings.FileDatabase = pathDatabase;
			userSettings.Save();
			string txt = universes.Count.ToString() + "/" +
									DMXUniverse.AllControllers.Count.ToString() + "/" +
									allChannels.Count.ToString();
			lblInfoDat.Text = txt;
			if (Fyle.isWiz) lblInfoDat.Visible = true;
			ReadyToCompare();
			ImBusy(false);

			return errs;
		}

		public int LoadUniverses(string filePath)
		{
			int errs = 0;
			string uniName = ""; // for debugging exceptions
			string uniFile = filePath + "Universes.csv";
			try
			{
				CsvFileReader reader = new CsvFileReader(uniFile);
				CsvRow row = new CsvRow();
				// Read and throw away first line which is headers;
				reader.ReadRow(row);
				while (reader.ReadRow(row))
				{
					try
					{
						DMXUniverse universe = new DMXUniverse();

						int i = -1;
						int.TryParse(row[0], out i);   // Field 0 Universe Number
						universe.UniverseNumber = i;

						universe.Name = row[1];  // Field 1 Name
						uniName = universe.Name;  // For debugging exceptions
						universe.Location = row[2]; // Field 2 Location
						universe.Comment = row[3]; // Field 3 Comment

						bool b = true;
						bool.TryParse(row[4], out b); // Field 4 Active
						universe.Active = b;

						i = 512;
						int.TryParse(row[5], out i);   // Field 5 Size Limit
						universe.SizeLimit = i;

						i = 1;
						int.TryParse(row[6], out i);   // Field 6 xLights Start
						universe.xLightsAddress = i;

						universe.Connection = row[7];  // Field 7 Connection
						lastID++;
						universe.ID = lastID;
						universes.Add(universe);
					}
					catch (Exception ex)
					{
						string msg = "Error " + ex.ToString() + " while reading Universe " + uniName;
						if (Fyle.isWiz)
						{
							DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						errs++;
					}
				}
				reader.Close();
				universes.Sort();
			}
			catch (Exception ex)
			{
				string msg = "Error " + ex.ToString() + " while reading Universes file " + uniFile;
				if (Fyle.isWiz)
				{
					DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				errs++;
			}
			return errs;
		}

		public int LoadControllers(string filePath)
		{
			int errs = 0;
			string ctlName = ""; // For debugging exceptions
			if (universes.Count > 0)
			{
				string ctlFile = filePath + "Controllers.csv";
				try
				{
					CsvFileReader reader = new CsvFileReader(ctlFile);
					CsvRow row = new CsvRow();
					// Read and throw away first line which is headers;
					reader.ReadRow(row);
					while (reader.ReadRow(row))
					{
						try
						{
							DMXController controller = new DMXController();
							int universe = 1;
							int.TryParse(row[0], out universe);   // Field 0 Universe Number

							controller.LetterID = row[1];  // Field 1 Letter ID
							controller.Name = row[2];  // Field 2 Name
							ctlName = controller.Name; // For debugging exceptions
							controller.Location = row[3]; // Field 3 Location
							controller.Comment = row[4]; // Field 4 Comment

							bool b = true;
							bool.TryParse(row[5], out b); // Field 5 Active
							controller.Active = b;

							controller.ControllerBrand = row[6];
							controller.ControllerModel = row[7];

							int i = 16;
							int.TryParse(row[8], out i);   // Field 8 Channel Count
							controller.OutputCount = i;

							i = 1;
							int.TryParse(row[9], out i);   // Field 9 DMX Start LOR4Channel
							controller.DMXStartAddress = i;

							i = 120;
							int.TryParse(row[10], out i);   // Field 10 voltage
							controller.Voltage = i;

							bool uniFound = false;
							for (int u = 0; u < universes.Count; u++)
							{
								if (universe == universes[u].UniverseNumber)
								{
									universes[u].DMXControllers.Add(controller);
									DMXUniverse.AllControllers.Add(controller);
									controller.DMXUniverse = universes[u];
									uniFound = true;
									u = universes.Count; // Exit loop
								}
							}
							if (!uniFound)
							{
								string msg = "Universe not found for controller:" + controller.Name;
								int qqqqq = 1;
							}
							lastID++;
							controller.ID = lastID;
						}
						catch (Exception ex)
						{
							string msg = "Error " + ex.ToString() + " while reading Controller " + ctlName;
							if (Fyle.isWiz)
							{
								DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							errs++;
						}
					}

					reader.Close();
					for (int u = 0; u < universes.Count; u++)
					{
						universes[u].DMXControllers.Sort();
					}
				}
				catch (Exception ex)
				{
					string msg = "Error " + ex.ToString() + " while reading Controllers file " + ctlFile;
					if (Fyle.isWiz)
					{
						DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					errs++;

				}
			}
			return errs;
		}

		public int LoadChannels(string filePath)
		{
			int errs = 0;
			string chanName = ""; // for debugging exceptions
			if (universes.Count > 0)
			{
				string chnFile = filePath + "Channels.csv";
				try
				{
					CsvFileReader reader = new CsvFileReader(chnFile);
					CsvRow row = new CsvRow();
					// Read and throw away first line which is headers;
					reader.ReadRow(row);
					while (reader.ReadRow(row))
					{
						try
						{
							DMXChannel channel = new DMXChannel();
							int uniNum = 1;
							int.TryParse(row[0], out uniNum); // Field 0 = Universe Number

							string ctlrLetter = row[1];       // Field 1 = Controller Letter

							int i = 1;
							int.TryParse(row[2], out i);
							channel.OutputNum = i;               // Field 2 = LOR4Output Number

							channel.Name = row[3];                // Field 3 = Name
							chanName = channel.Name;              // For debugging exceptions
							channel.Location = row[4];            // Field 4 = Location
							channel.Comment = row[5];             // Field 5 = Comment

							bool b = true;
							bool.TryParse(row[6], out b);     // Field 6 = Active
							channel.Active = b;

							int devID = -1;
							int.TryParse(row[7], out devID);      // Field 7 = Channel Type



							//! ONE TIME FIX
							//if (devID == 0) devID = 22;





							if (devID >= 0 && devID < deviceTypes.Count)
							{
								//! Note: DeviceTypes should not yet be sorted by Display Order
								//! Should still be sorted by ID
								string dn = deviceTypes[devID].Name;
								channel.DeviceType = deviceTypes[devID];
							}

							string colhex = row[8];           // Field 8 = Color
							if (chanName.Substring(0, 5).CompareTo("Eaves") == 0)
							{
								if (Fyle.isWiz)
								{
									string foo = colhex;
								}
							}
							//Color color = System.Drawing.ColorTranslator.FromHtml(colhex);
							Color color = LOR4.LOR4Admin.HexToColor(colhex);
							channel.Color = color;

							allChannels.Add(channel);
							//datChannels.Add(channel);
							bool ctlFound = false;
							for (int u = 0; u < universes.Count; u++)
							{
								DMXUniverse universe = universes[u];
								for (int c = 0; c < universe.DMXControllers.Count; c++)
								{
									if (ctlrLetter == universe.DMXControllers[c].LetterID)
									{
										channel.DMXController = universe.DMXControllers[c];
										universe.DMXControllers[c].DMXChannels.Add(channel);
										ctlFound = true;
										c = universe.DMXControllers.Count;
										u = universes.Count; // Exit loop
									}
								}
							}
							if (!ctlFound)
							{
								string msg = "Controller not found for channel " + channel.Name;
								int qqq5 = 5;
							}
							lastID++;
							channel.ID = lastID;
						}
						catch (Exception ex)
						{
							int ln = LOR4.LOR4Admin.ExceptionLineNumber(ex);
							string msg = "Error on line " + ln.ToString() + "\r\n";
							msg += ex.ToString() + " while reading Channel " + chanName;
							if (Fyle.isWiz)
							{
								DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							errs++;
						}
					}
					reader.Close();
					for (int u = 0; u < universes.Count; u++)
					{
						DMXUniverse universe = universes[u];
						for (int c = 0; c < universe.DMXControllers.Count; c++)
						{
							universe.DMXControllers[c].DMXChannels.Sort();
						}
					}
					// *NOW* we can sort the AllDatChannels by display order
					allChannels.Sort();
				}
				catch (Exception ex)
				{
					int ln = LOR4.LOR4Admin.ExceptionLineNumber(ex);
					string msg = "Error " + ex.ToString() + " on line " + ln.ToString() + " while reading Channels file " + chnFile;
					if (Fyle.isWiz)
					{
						DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					errs++;

				}
			}
			return errs;
		}

		public int LoadDeviceTypes(string filePath)
		{
			int errs = 0;
			string devName = ""; // for debugging exceptions
			if (universes.Count > 0)
			{
				string chnFile = filePath + "DeviceTypes.csv";
				try
				{
					CsvFileReader reader = new CsvFileReader(chnFile);
					CsvRow row = new CsvRow();
					// Read and throw away first line which is headers;
					reader.ReadRow(row);
					while (reader.ReadRow(row))
					{
						try
						{
							if (row.Count > 1)
							{
								int devID = -1;
								int.TryParse(row[0], out devID); // Field 0 = Device ID
								devName = row[1];   // Field 1 = Name
								int ord = 0;
								if (row.Count > 2)
								{
									int.TryParse(row[2], out ord); // Field 2 = OutputNum Number
								}
								DMXDeviceType device = new DMXDeviceType(devName, devID, ord);
								deviceTypes.Add(device);
							}
						}
						catch (Exception ex)
						{
							int ln = LOR4.LOR4Admin.ExceptionLineNumber(ex);
							string msg = "Error on line " + ln.ToString() + "\r\n";
							msg += ex.ToString() + " while reading Channel " + devName;
							if (Fyle.isWiz)
							{
								DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							errs++;
						}
					}
					reader.Close();
				}
				catch (Exception ex)
				{
					int ln = LOR4.LOR4Admin.ExceptionLineNumber(ex);
					string msg = "Error " + ex.ToString() + " on line " + ln.ToString() + " while reading Channels file " + chnFile;
					if (Fyle.isWiz)
					{
						DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					errs++;
				}
			}
			int dc = allChannels.Count;
			//! No! do not sort [by display order] yet!  Leave sorted by order added which is also by ID until AFTER the channels have been
			//! loaded, THEN sort by display order
			//AllChannels.Sort();
			return errs;
		}

		private void btnBrowseDatabase_Click(object sender, EventArgs e)
		{
			bool stopLooking = false;
			// Start with LOR Showtime directory
			string initDir = Path.GetDirectoryName(pathDatabase);
			if (Directory.Exists(initDir))
			{
			}
			else
			{
				// Last path is no longer valid, try the user documents folder
				string d = "";
				initDir = Fyle.DefaultDocumentsPath;
				if (Directory.Exists(initDir))
				{
					// Its there, does it have a Christmas subdirectory
					d = initDir + "Christmas\\";
					if (Directory.Exists(d))
					{
						initDir = d;
					}
					// Is there a year folder
					d = initDir + DateTime.Now.Year.ToString() + "\\";
					if (Directory.Exists(d))
					{
						initDir = d;
					}
					// Is there a Docs folder
					d = initDir + "Docs\\";
					if (Directory.Exists(d))
					{
						initDir = d;
					}
					d = initDir + "ChannelDB\\";
					if (Directory.Exists(d))
					{
						initDir = d;
					}
				}
			}

			string filt = "Channel Database (Channels.csv)|Channels.csv";

			dlgFileOpen.Filter = filt;
			dlgFileOpen.FilterIndex = 0;
			dlgFileOpen.DefaultExt = "*.csv";
			dlgFileOpen.InitialDirectory = initDir;
			dlgFileOpen.FileName = "Channels.csv";
			dlgFileOpen.CheckFileExists = true;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.Multiselect = false;
			dlgFileOpen.Title = "Select the Channel Database";
			DialogResult result = dlgFileOpen.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				string dbPath = Path.GetDirectoryName(dlgFileOpen.FileName) + "\\";
				LoadData(dbPath);
				//btnOK.Focus();
			} // end if (result = DialogResult.OK)
		}

		private void btnBrowseViz_Click(object sender, EventArgs e)
		{
			bool stopLooking = false;
			string initDir = "";

			string p = Path.GetDirectoryName(fileVisualization);
			if (Directory.Exists(p)) initDir = p;
			if (initDir.Length < 2) initDir = LOR4.LOR4Admin.DefaultVisualizationsPath;
			// Have we got something (anything!) yet?
			if (!Directory.Exists(initDir))
			{
				// Last Gasp, go the for the user's documents folder
				initDir = Fyle.DefaultDocumentsPath;
			}

			string initFile = "";
			string filt = LOR4Admin.EXT_LEE;

			dlgFileOpen.Filter = filt;
			dlgFileOpen.FilterIndex = 0;
			dlgFileOpen.DefaultExt = "*.lee";
			dlgFileOpen.InitialDirectory = initDir;
			dlgFileOpen.FileName = initFile;
			dlgFileOpen.CheckFileExists = true;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.Multiselect = false;
			dlgFileOpen.Title = "Select the Visualization File...";
			DialogResult result = dlgFileOpen.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				LoadVisualization(dlgFileOpen.FileName);
				//btnOK.Focus();
			} // end if (result = DialogResult.OK)


		}

		public int LoadVisualization(string fileVizName)
		{
			int errs = 0;
			ImBusy(true);


			visualization = new LOR4Visualization(null, fileVizName);
			if (visualization.VizChannels.Count > 0)
			{
				fileVisualization = fileVizName;
				txtFileVisual.Text = fileVisualization;
				userSettings.FileVisualization = fileVisualization;
				userSettings.Save();
				string txt = visualization.VizItemGroups.Count.ToString() + "/" +
										visualization.VizDrawObjects.Count.ToString() + "/" +
										visualization.VizChannels.Count.ToString();
				lblInfoViz.Text = txt;
				//if (Fyle.isWiz) lblInfoViz.Visible = true;
				//string fo = "Visualization file now opened and read in.\r\n";
				//fo += "About to check it's 'tegrity.";
				//MessageBox.Show(this, fo, "Vizzzzzz", MessageBoxButtons.OK, MessageBoxIcon.Information);
				//Fyle.MakeNoise(Fyle.Noises.Kalimbra);
				//VizTegrity();
				ReadyToCompare();
			}
			ImBusy(false);
			return errs;
		}

		private void frmCompare_Shown(object sender, EventArgs e)
		{
			if (!shown)
			{
				FirstShow();
				shown = true;
			}
		}

		private void VizTegrity()
		{
			string theReport = visualization.Tegrity();
			string theFile = "W:\\Documents\\Christmas\\2021\\Docs\\VizTegrity Report.txt";
			StreamWriter w = new StreamWriter(theFile);
			w.Write(theReport);
			w.Close();
			Fyle.LaunchFile(theFile);
		}

	}
}
