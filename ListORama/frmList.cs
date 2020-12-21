using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using LORUtils;

namespace ListORama
{


	public partial class frmList : Form
	{

		private const string CRLF = "\r\n";
		private const string helpPage = "http://wizlights.com/utilorama/maporama";

		private string applicationName = "List-O-Rama";
		private string thisEXE = "List-o-rama.exe";
		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\Util-O-Rama\\Split-O-Rama\\
		private string[] commandArgs;
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private int batch_fileNumber = 0;
		private string[] batch_fileList = null;
		private string cmdSelectionsFile = "";
		private bool processDrop = false;


		private const double MINSCORE = 0.85D;
		private const double MINMATCH = 0.92D;
		private Color COLOR_BK_SELECTED = Color.FromName("DarkBlue");
		private Color COLOR_BK_SELCHILD = Color.FromName("LightBlue");
		private Color COLOR_BK_MATCHED = Color.FromName("DarkGreen");
		private Color COLOR_BK_MATCHCHILD = Color.FromName("LightGreen");
		private Color COLOR_BK_BOTH = Color.FromName("DarkCyan");
		private Color COLOR_BK_NONE = Color.FromName("White");
		private Color COLOR_FR_SELECTED = Color.FromName("White");
		private Color COLOR_FR_NONE = Color.FromName("Black");

		private const string REGCHAN = "Regular Channels";
		private const string RGBCHAN = "RGB Channels";
		private const string CHANGRP = "Channel Groups";
		private const string CHWITH = " with \"";
		private const string CHWITHOUT = " without \"";
		
		//public static int nodeIndex = utils.UNDEFINED;
		private bool firstShown = false;
		private bool saveOptionChanges = false; // default false to ignore changes during initial setup, then change to true to save user changes

		// These are used by MapList so need to be public
		public Sequence4 theSequence = new Sequence4();
		public Visualization4 theViz = new Visualization4();
		// Original: public List<List<TreeNode>> sourceNodesSI = new List<List<TreeNode>>();
		public List<TreeNode>[] sourceNodesSI = null; //new List<TreeNode>();
		public string lastSeqFile = "";
		private string lastVizFile = "";
		private string lastSavePath = utils.DefaultChannelConfigsPath;

		private string basePath = "";
		private string SeqFolder = "";
		bool ignoreModeChange = false;
		bool workingMyAssOff = false;

		public int nodeIndex = utils.UNDEFINED;
		// Note Master->Source mappings are a 1:Many relationship.
		// A Master channel can map to only one Source channel
		// But a Source channel may map to more than one Master channel


		private int seqChanCount = 0;
		//private ChannelList[] chList = null;
		private int position = 0;
		private List<SeqChannelItem> chItems = new List<SeqChannelItem>();
		private List<SeqRGBItem> rgbItems = new List<SeqRGBItem>();
		private List<SeqGroupItem> groupItems = new List<SeqGroupItem>();
		private List<VizChannelItem> vizItems = new List<VizChannelItem>();

		private bool[] gotit = null; // Got It - Size = HighestSavedIndex - True=AlreadyFound
		private int regKeywdCount = 0;
		private int regNonKeywdCount = 0;
		private int rgbKeywdCount = 0;
		private int rgbNonKeywdCount = 0;
		private int groupKeywdCount = 0;
		private int groupNonKeywdCount = 0;
		private int chanTotCount = 0;
		private int rgbTotCount = 0;
		private int grpTotCount = 0;
		private int chanKeywdCount = 0;
		//private int rgbKeywdCount = 0;
		private int grpKeywdCount = 0;
		private int chanSelCount = 0;
		private int rgbSelCount = 0;
		private int grpSelCount = 0;
		private int regFoundCount = 0;
		private int rgbFoundCount = 0;
		private int groupFoundCount = 0;
		private int vizChanCount = 0; // Reset
		private int vizregKeywdCount = 0;
		private int vizregNonKeywdCount = 0;
		private int vizregFoundCount = 0;
		private int vizPropKeywdCount = 0;
		private int vizPropNonKeywdCount = 0;
		private int vizPropFoundCount = 0;
		private int vizFoundCount = 0;

		private string csvFilePrefix = "";
		private bool isKeywd = false;
		private bool reportMe = false;

		private const int SORT_BYNAME = 0;
		private const int SORT_BYOUTPUT = 1;
		private const int SORT_BYDISPLAYED = 2;
		private const int SORT_BYSAVEDINDEX = 3;
		private int sortOrder = SORT_BYNAME;
		private const string ZEROPAD = "00000";
		private const string DASH = "-";
		private const string COMMA = ",";

		public frmList()
		{
			InitializeComponent();
		}

		private void btnGenCSV_Click(object sender, EventArgs e)
		{
			// Save Settings for next run
			SetTheControlsForTheHeartOfTheSun();  

			if (optSeq.Checked)
			{
				SaveSequenceCSVs();
			}
			if (optViz.Checked)
			{
				GenerateVisualCSVs();
			}
		}

		private void SaveSequenceCSVs()
		{
			string filePath = Properties.Settings.Default.LastSavePath;
			if (!Directory.Exists(filePath))
			{
				filePath = utils.DefaultChannelConfigsPath;
			}
			if (!Directory.Exists(filePath))
			{
				filePath = Path.GetDirectoryName(lastSeqFile);
			}
			dlgSaveFile.InitialDirectory = filePath;
			dlgSaveFile.FileName = Path.GetFileNameWithoutExtension(lastSeqFile);
			dlgSaveFile.DefaultExt = ".csv";
			dlgSaveFile.Filter = "Comma Separated Values *.csv|*.csv";
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.OverwritePrompt = false;
			dlgSaveFile.Title = "Save Data Files As...";
			DialogResult dr = dlgSaveFile.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				lastSavePath = Path.GetDirectoryName(dlgSaveFile.FileName) + "\\";
				Properties.Settings.Default.LastSavePath = lastSavePath;
				Properties.Settings.Default.Save();
				string fileName = lastSavePath + Path.GetFileNameWithoutExtension(dlgSaveFile.FileName);


				GenerateSequenceCSVs(fileName);
			}
		}

		private void GenerateSequenceCSVs(string fileName)
		{
			string keywd = txtKeyword.Text;
			if (!chkKeyword.Checked) keywd = "";

			Membership ukChn = new Membership(theSequence);
			Membership unChn = new Membership(theSequence);
			Membership ukRGB = new Membership(theSequence);
			Membership unRGB = new Membership(theSequence);
			Membership ukGrp = new Membership(theSequence);
			Membership unGrp = new Membership(theSequence);
			if (chkKeyword.Checked)
			{
				if (chkRegKeywd.Checked) ukChn = GetUniqueMembers(MemberType.Channel, keywd, false);
				if (chkRegKeywd.Checked) unChn = GetUniqueMembers(MemberType.Channel, keywd, true);
				if (chkRegKeywd.Checked) ukRGB = GetUniqueMembers(MemberType.RGBchannel, keywd, false);
				if (chkRegKeywd.Checked) unRGB = GetUniqueMembers(MemberType.RGBchannel, keywd, true);
				if (chkRegKeywd.Checked) ukGrp = GetUniqueMembers(MemberType.ChannelGroup, keywd, false);
				if (chkRegKeywd.Checked) unGrp = GetUniqueMembers(MemberType.ChannelGroup, keywd, true);
			}
			else
			{
				if (chkRegKeywd.Checked) ukChn = GetUniqueMembers(MemberType.Channel);
				if (chkRegKeywd.Checked) ukRGB = GetUniqueMembers(MemberType.RGBchannel);
				if (chkRegKeywd.Checked) ukGrp = GetUniqueMembers(MemberType.ChannelGroup);

			}
			
			if (chkKeyword.Checked)
			{
				MakeReport(fileName + " Regular Channels with ''" + keywd + "''.csv", ukChn);
				MakeReport(fileName + " Regular Channels without ''" + keywd + "''.csv", unChn);
				MakeReport(fileName + " RGB Channels with ''" + keywd + "''.csv", ukRGB);
				MakeReport(fileName + " RGB Channels without ''" + keywd + "''.csv", unRGB);
				MakeReport(fileName + " Channel Groups with ''" + keywd + "''.csv", ukGrp);
				MakeReport(fileName + " Channel Groups without ''" + keywd + "''.csv", unGrp);
			}
			else
			{
				MakeReport(fileName + " Regular Channels.csv", ukChn);
				MakeReport(fileName + " RGB Channels.csv", ukRGB);
				MakeReport(fileName + " Channel Groups.csv", ukGrp);
			}



		}

		private void MakeReport(string fileName, Membership members)
		{
			if (members.Items.Count > 0)
			{
				if (optSortDisplayed.Checked)
				{
					// Leave in order found, do not sort
				}
				else
				{
					if (optSortName.Checked)
					{
						members.sortMode = Membership.SORTbyName;
					}
					if (optSortSavedIndex.Checked)
					{
						members.sortMode = Membership.SORTbyName;
					}
					if (optSortOutput.Checked)
					{
						if (members.Items[0].MemberType == MemberType.Channel)
						{
							members.sortMode = Membership.SORTbyOutput;
						}
						else
						{
							members.sortMode = Membership.SORTbyName;
						}
					}
					members.Items.Sort();
				}
				StreamWriter writer = new StreamWriter(fileName);
				if (chkHeaders.Checked)
				{
					string header = "Type,SavedIndex,Name,Centiseconds,Device,Unit,Network,Circuit,Universe,Channel,LOR Color,Web Color,Color Name";
					writer.WriteLine(header);
				}
				for (int i=0; i< members.Count; i++)
				{
					ReportMember(members[i], writer);
				}

				writer.Close();
			}

		}

		private void ReportMember(IMember member, StreamWriter writer)
		{
			StringBuilder l = new StringBuilder();

			l.Append(member.MemberType + COMMA);
			l.Append(member.SavedIndex.ToString() + COMMA);
			l.Append(member.Name + COMMA);
			l.Append(member.Centiseconds.ToString() + COMMA);

			if (member.MemberType == MemberType.Channel)
			{
				Channel ch = (Channel)member;
				l.Append(ch.output.deviceType + COMMA);
				if (ch.output.deviceType == DeviceType.LOR)
				{
					l.Append(ch.output.unit.ToString() + COMMA);
					l.Append(ch.output.networkName + COMMA);
					l.Append(ch.output.channel.ToString() + COMMA);
					l.Append(COMMA + COMMA);
				}
				if (ch.output.deviceType == DeviceType.DMX)
				{
					l.Append(COMMA + COMMA + COMMA);
					l.Append(ch.output.universe.ToString() + COMMA);
					l.Append(ch.output.channel.ToString() + COMMA);
				}
				if (ch.output.deviceType == DeviceType.None)
				{
					l.Append(COMMA + COMMA + COMMA);
					l.Append(COMMA + COMMA);
				}
				l.Append(ch.color.ToString() + COMMA);
				l.Append('#' + utils.Color_LORtoHTML(ch.color) + COMMA);
				l.Append(NearestColor.FindNearestColorName(ch.color) + COMMA);
			}
			if (member.MemberType == MemberType.RGBchannel)
			{
				RGBchannel rgb = (RGBchannel)member;
				Channel ch = rgb.redChannel;
				l.Append(ch.output.deviceType + COMMA);
				if (ch.output.deviceType == DeviceType.LOR)
				{
					l.Append(ch.output.unit.ToString() + COMMA);
					l.Append(ch.output.network.ToString() + COMMA);
					l.Append(ch.output.circuit.ToString() + COMMA);
				}
				if (ch.output.deviceType == DeviceType.DMX)
				{
					l.Append(ch.output.universe.ToString() + COMMA);
					l.Append(COMMA);
					l.Append(ch.output.channel.ToString() + COMMA);
				}
			}
			if (member.MemberType == MemberType.ChannelGroup)
			{
				ChannelGroup grp = (ChannelGroup)member;
				// Child Count
				l.Append(grp.Members.Items.Count.ToString() + COMMA);
				// Count children, grandchildren, grand-grandchildren... (recursive)
				l.Append(grp.Members.DescendantCount(false, true, true, true).ToString() + COMMA);
			}

			string m = l.ToString();
			// Remove trailing comma
			string lineOut = m.Substring(0, m.Length - 1);
			writer.WriteLine(lineOut);

			}


			private Membership GetUniqueMembers(MemberType memberType, string keyword = "",
			
			
			bool exclude = false)
			
		{
			IMember[] members = null;
			Membership uniqueMembers = new Membership(theSequence);
			int uniqueCount = 0;
			int listSize = theSequence.Members.HighestSavedIndex + theSequence.Tracks.Count + theSequence.TimingGrids.Count + 1;
			Array.Resize(ref members, listSize);
			uniqueCount += GrabNodes(treeSource.Nodes, members);
			int spot = 0;
			for (int i = 0; i < members.Length; i++)
			{
				if (members[i] != null)
				{
					if (members[i].MemberType == memberType)
					uniqueMembers.Add(members[i]);
					spot++;
				}
			}
			return uniqueMembers;
		}


		private int GrabNodes(TreeNodeCollection nodes, IMember[] members)
		{
			int uniqueCount = 0;
			foreach (TreeNode node in nodes)
			{
				IMember member = (IMember)node.Tag;
				if (member != null)
				{
					if (members[member.SavedIndex] == null)
					{
						members[member.SavedIndex] = member;
						uniqueCount++;
					}
				}
				if (node.Nodes.Count > 0)
				{
					uniqueCount += GrabNodes(node.Nodes, members);
				}
			}
			return uniqueCount;
		}


		private void OLD_GenerateSequenceCSVs()
		{ 
			//string filt = "Comma-Separated Values *.csv|*.csv|Sequences *.las, *.lms|*.las;*.lms|Musical Sequences *.lms|*.lms|Animated Sequences *.las|*.las|All Files *.*|*.*";
			//string initDir = Path.GetFullPath(lastSeqFile);
			//string initFile = Path.GetFileNameWithoutExtension(lastSeqFile);
			//string tit = "Filename PREFIX for csv files";
			string msg = "";

			/*
			dlgSaveFile.Filter = filt;
			dlgSaveFile.FilterIndex = 1;
			//dlgSaveFile.FileName = Path.GetFullPath(fileSeqCur) + Path.GetFileNameWithoutExtension(fileSeqCur) + " Part " + member.ToString() + ext;
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.InitialDirectory = initDir;
			dlgSaveFile.DefaultExt = "csv";
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.Title = tit;
			dlgSaveFile.SupportMultiDottedExtensions = true;
			dlgSaveFile.ValidateNames = true;
			//newFileIn = Path.GetFileNameWithoutExtension(fileSeqCur) + " Part " + member.ToString(); // + ext;
			//newFileIn = "Part " + member.ToString() + " of " + Path.GetFileNameWithoutExtension(fileSeqCur);
			//newFileIn = "Part Mother Fucker!!";
			dlgSaveFile.FileName = initFile;
			DialogResult result = dlgSaveFile.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				*/
			ImBusy("Analyzing " + Path.GetFileNameWithoutExtension(lastSeqFile));
			Membership membrs;
			if (cboTracks.SelectedIndex == 0)
			{
				membrs = theSequence.Members;
			}
			else
			{
				membrs = theSequence.Tracks[cboTracks.SelectedIndex - 1].Members;
			}
			seqChanCount = 0; // Reset
			regKeywdCount = 0;
			regNonKeywdCount = 0;
			regFoundCount = 0;
			rgbKeywdCount = 0;
			rgbNonKeywdCount = 0;
			rgbFoundCount = 0;
			groupKeywdCount = 0;
			groupNonKeywdCount = 0;
			groupFoundCount = 0;
			// Clear and recreate so all elements are 'false'
			gotit = null;
			Array.Resize(ref gotit, theSequence.Members.HighestSavedIndex+1);

			chItems.Clear();

			//! Some of this code will need to get moved as I add more functionality

			string fn = Path.GetFullPath(lastSeqFile) + Path.GetFileNameWithoutExtension(lastSeqFile);
			if (chkRegNonKeywd.Checked || chkRegKeywd.Checked)
			{
				CollectSeqRegularChannels(membrs);
				if ((regKeywdCount + regNonKeywdCount) == 0)
				{
					msg = "No channels meet the selected criteria.\r\n";
					msg += "Please review your option settings and try again.";
					//DialogResult d0 = MessageBox.Show(this, msg, "Export Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					Sort_RemoveDuplicateChannels_Resort();
					string fnrc = fn + " Regular Channels.csv";
					ReportSeqRegularChannels(fnrc);
					msg = "";
					if (regKeywdCount > 0)
					{
						msg = regKeywdCount.ToString() + " Regular Channels with ";
						msg += txtKeyword.Text;
						if (regNonKeywdCount > 0)
						{
							msg += " and ";
						}
						msg += "\r\n";
					}
					if (regNonKeywdCount > 0)
					{
						msg += regNonKeywdCount.ToString() + " Regular Channels without ";
						msg += txtKeyword.Text;
						msg += "\r\n";
					}

					msg += "Exported to ";
					msg += Path.GetFileName(fnrc);
					//DialogResult dx = MessageBox.Show(this, msg, "Export Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
				} // End Collected Channel Count > 0
			} // End Regular with or without keywoard checked


			if (chkRGBNonKeywd.Checked || chkRGBKeywd.Checked)
			{
				//CollectSeqRegularChannels(membrs);
				if ((rgbKeywdCount + rgbNonKeywdCount) == 0)
				{
					msg += "No RGB channels meet the selected criteria.\r\n";
					msg += "Please review your option settings and try again.";
					//DialogResult d0 = MessageBox.Show(this, msg, "Export Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				}
				else
				{
					Sort_RemoveDuplicateRGBs_Resort();
					string fnrc = fn + " RGB Channels.csv";
					ReportSeqRGBChannels(fnrc);
					msg = "";
					if (rgbKeywdCount > 0)
					{
						msg += rgbKeywdCount.ToString() + " RGB Channels with ";
						msg += txtKeyword.Text;
						if (rgbNonKeywdCount > 0)
						{
							msg += " and ";
						}
						msg += "\r\n";
					}
					if (rgbNonKeywdCount > 0)
					{
						msg += rgbNonKeywdCount.ToString() + " RGB Channels without ";
						msg += txtKeyword.Text;
						msg += "\r\n";
					}

					msg += "Exported to ";
					msg += Path.GetFileName(fnrc);
					//DialogResult dx = MessageBox.Show(this, msg, "Export Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}


			if (chkGrpNonKeywd.Checked || chkGrpKeywd.Checked)
			{
				//CollectSeqRegularChannels(membrs);
				if ((rgbKeywdCount + rgbNonKeywdCount) == 0)
				{
					msg += "No Channel Groups meet the selected criteria.\r\n";
					msg += "Please review your option settings and try again.";
					//DialogResult d0 = MessageBox.Show(this, msg, "Export Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				}
				else
				{
					Sort_RemoveDuplicateGroups_Resort();
					string fnrc = fn + " Channel Groups.csv";
					ReportSeqChannelGroups(fnrc);
					msg = "";
					if (groupKeywdCount > 0)
					{
						msg += groupKeywdCount.ToString() + " Channel Groups with ";
						msg += txtKeyword.Text;
						if (groupNonKeywdCount > 0)
						{
							msg += " and ";
						}
						msg += "\r\n";
					}
					if (groupNonKeywdCount > 0)
					{
						msg += groupKeywdCount.ToString() + " Channel Groups without ";
						msg += txtKeyword.Text;
						msg += "\r\n";
					}

					msg += "Exported to ";
					msg += Path.GetFileName(fnrc);
				}
			}
			DialogResult dx = MessageBox.Show(this, msg, "Export Details", MessageBoxButtons.OK, MessageBoxIcon.Information);


			ImBusy(false);
			//}
		}

		private void GenerateVisualCSVs()
		{ 
			string msg = "";
			ImBusy("Analyzing " + Path.GetFileNameWithoutExtension(lastVizFile));
			vizChanCount = 0; // Reset
			vizregKeywdCount = 0;
			vizregNonKeywdCount = 0;
			vizregFoundCount = 0;
			vizPropKeywdCount = 0;
			vizPropNonKeywdCount = 0;
			vizPropFoundCount = 0;
			vizFoundCount = 0;

				vizItems.Clear();

				//! Some of this code will need to get moved as I add more functionality

				string fn = Path.GetFullPath(lastVizFile) + Path.GetFileNameWithoutExtension(lastVizFile);
				if (chkRegNonKeywd.Checked || chkRegKeywd.Checked)
				{
					CollectVizRegularChannels(theViz);
					if ((vizregKeywdCount + vizregNonKeywdCount) == 0)
					{
						msg = "No channels meet the selected criteria.\r\n";
						msg += "Please review your option settings and try again.";
						//DialogResult d0 = MessageBox.Show(this, msg, "Export Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					}
					else
					{
						Sort_RemoveDuplicateVizChannels_Resort();
						string fnrc = fn + " Regular Channels.csv";
						ReportVizRegularChannels(fnrc);
					msg = "";
						if (vizregKeywdCount > 0)
					{
						msg = vizregKeywdCount.ToString() + " Regular Channels with ";
						msg += txtKeyword.Text;
						if (regNonKeywdCount > 0)
						{
							msg += " and ";
						}
						msg += "\r\n";
					}
						if (vizregNonKeywdCount > 0)
					{
						msg += vizregNonKeywdCount.ToString() + " Regular Channels without ";
						msg += txtKeyword.Text;
						msg += "\r\n";
					}

					msg += "Exported to ";
						msg += Path.GetFileName(fnrc);
						//DialogResult dx = MessageBox.Show(this, msg, "Export Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}

				DialogResult dx = MessageBox.Show(this, msg, "Export Details", MessageBoxButtons.OK, MessageBoxIcon.Information);


			ImBusy(false);
			//}
		}


		private void CollectSeqRegularChannels(Membership members)
		{
			isKeywd = false;
			reportMe = true;

			//try
			//{

			int c = members.Count;
			//! Enumeration NOt Working!
			//foreach (IMember member in members) //! Enumeration Not Working!
			for (int n=0; n<c; n++)
			{
				IMember member = members.Items[n];
				reportMe = true;
				if (member.Name.IndexOf(txtKeyword.Text) > utils.UNDEFINED) isKeywd = true;

				if (member.MemberType == MemberType.Track)
				{
					Track tr = (Track)member;
					CollectSeqRegularChannels(tr.Members); // Recurse!
				}
				if (member.MemberType == MemberType.ChannelGroup)
				{
					if (!gotit[member.SavedIndex])
					{
						gotit[member.SavedIndex] = true;
						ChannelGroup cg = (ChannelGroup)member;
						CollectSeqRegularChannels(cg.Members); // Recurse!
						if (chkGrpKeywd.Checked || chkGrpNonKeywd.Checked)
						{
							ConsiderGroup(cg);
						}
					}
				}
				if (member.MemberType == MemberType.RGBchannel)
				{
					if (!gotit[member.SavedIndex])
					{
						gotit[member.SavedIndex] = true;
						RGBchannel rgb = (RGBchannel)member;
						ConsiderChannel(rgb.redChannel);
						ConsiderChannel(rgb.grnChannel);
						ConsiderChannel(rgb.bluChannel);
						if (chkRGBKeywd.Checked || chkRGBNonKeywd.Checked)
						{
							ConsiderRGB(rgb);
						}
					}
				}
				if (member.MemberType == MemberType.Channel)
				{
					if (!gotit[member.SavedIndex])
					{
						gotit[member.SavedIndex] = true;
						Channel ch = (Channel)member;
						if (chkRegKeywd.Checked || chkRegNonKeywd.Checked)
						{
							ConsiderChannel(ch);
						}
					}
				}
			}
			//}
			//catch
			//{
			//	System.Diagnostics.Debugger.Break();
			//}
		}

		private bool ConsiderChannel(Channel chan)
		{
			bool reportMe = false;

			if (!chkKeyword.Checked)
			{
				reportMe = true;
			}
			else
			{
				if (chkRegKeywd.Checked && isKeywd)
				{
					reportMe = true;
				}
				if (chkRegNonKeywd.Checked && !isKeywd)
				{
					reportMe = true;
				}
			}
			if ((chan.output.deviceType == DeviceType.LOR) && !chkLOR.Checked)
			{
				reportMe = false;
			}
			if ((chan.output.deviceType == DeviceType.DMX) && !chkDMX.Checked)
			{
				reportMe = false;
			}
			if ((chan.output.deviceType == DeviceType.Digital) && !chkDigital.Checked)
			{
				reportMe = false;
			}
			if ((chan.output.deviceType == DeviceType.None) && !chkNoController.Checked)
			{
				reportMe = false;
			}
			if (reportMe)
			{
				SeqChannelItem thisCh = new SeqChannelItem();
				thisCh.theChannel = chan;
				//thisCh.sortString = chan.SavedIndex.ToString(ZEROPAD);
				thisCh.isKeywd = isKeywd;
				regFoundCount++;
				thisCh.foundOrder = regFoundCount;
				chItems.Add(thisCh);
				if (isKeywd)
				{
					regKeywdCount++;
				}
				else
				{
					regNonKeywdCount++;
				}
			}
			return reportMe;
		}

		private bool ConsiderRGB(RGBchannel RGBc)
		{
			bool reportMe = false;
			if (!chkKeyword.Checked)
			{
				reportMe = true;
			}
			else
			{
				if (chkRGBKeywd.Checked && isKeywd)
				{
					reportMe = true;
				}
				if (chkRGBNonKeywd.Checked && !isKeywd)
				{
					reportMe = true;
				}
			}
			Channel chan = RGBc.redChannel;
			if ((chan.output.deviceType == DeviceType.LOR) && !chkLOR.Checked)
			{
				reportMe = false;
			}
			if ((chan.output.deviceType == DeviceType.DMX) && !chkDMX.Checked)
			{
				reportMe = false;
			}
			if ((chan.output.deviceType == DeviceType.Digital) && !chkDigital.Checked)
			{
				reportMe = false;
			}
			if ((chan.output.deviceType == DeviceType.None) && !chkNoController.Checked)
			{
				reportMe = false;
			}
			if (reportMe)
			{
				SeqRGBItem thisRGB = new SeqRGBItem();
				thisRGB.theRGBchannel = RGBc;
				//thisRGB.sortString = chan.SavedIndex.ToString(ZEROPAD);
				thisRGB.isKeywd = isKeywd;
				rgbFoundCount++;
				thisRGB.foundOrder = rgbFoundCount;
				rgbItems.Add(thisRGB);
				if (isKeywd)
				{
					rgbKeywdCount++;
				}
				else
				{
					rgbNonKeywdCount++;
				}
			}
			return reportMe;
		}

		private bool ConsiderGroup(ChannelGroup group)
		{
			bool reportMe = false;

			if (!chkKeyword.Checked)
			{
				reportMe = true;
			}
			else
			{
				if (chkRGBKeywd.Checked && isKeywd)
				{
					reportMe = true;
				}
				if (chkRGBNonKeywd.Checked && !isKeywd)
				{
					reportMe = true;
				}
			}
			if (reportMe)
			{
				SeqGroupItem thisGroup = new SeqGroupItem();
				thisGroup.theGroup = group;
				//thisGroup.sortString = group.SavedIndex.ToString(ZEROPAD);
				thisGroup.isKeywd = isKeywd;
				groupFoundCount++;
				thisGroup.foundOrder = groupFoundCount;
				groupItems.Add(thisGroup);
				if (isKeywd)
				{
					groupKeywdCount++;
				}
				else
				{
					groupNonKeywdCount++;
				}
			}
			return reportMe;
		}

		private void CollectVizRegularChannels(Visualization4 visual)
		{
			bool hasKey = false;
			reportMe = true;

			//try
			//{

			int c = visual.VizChannels.Count;
			//! Enumeration NOt Working!
			//foreach (IMember member in members) //! Enumeration Not Working!
			for (int n = 0; n < c; n++)
			{
				VizChannel vc = visual.VizChannels[n];
				reportMe = true;
				string nm = vc.Name;
				bool bIncl = FilterNode(vc);
				int pp = nm.IndexOf(txtKeyword.Text);
				hasKey = (pp >= 0);


				//if (vc.rgbChild == RGBchild.None)
					if (bIncl)
					{
						VizChannelItem thisVch = new VizChannelItem();
						thisVch.theChannel = vc;
						thisVch.sortString = vc.SavedIndex.ToString(ZEROPAD);
						thisVch.isKeywd = isKeywd;
						vizFoundCount++;
						thisVch.foundOrder = regFoundCount;
						vizItems.Add(thisVch);
						if (hasKey)
						{
							vizregKeywdCount++;
						}
						else
						{
							vizregNonKeywdCount++;
						}
						vizregFoundCount++;
					}
				

			}
			//}
			//catch
			//{
			//	System.Diagnostics.Debugger.Break();
			//}
		}

		private bool ConsiderVizChannel(VizChannel vchan)
		{
			bool reportMe = false;

			if (chkRegKeywd.Checked && isKeywd)
			{
				reportMe = true;
			}
			if (chkRegNonKeywd.Checked && !isKeywd)
			{
				reportMe = true;
			}
			if ((vchan.output.deviceType == DeviceType.LOR) && !chkLOR.Checked)
			{
				reportMe = false;
			}
			if ((vchan.output.deviceType == DeviceType.DMX) && !chkDMX.Checked)
			{
				reportMe = false;
			}
			if ((vchan.output.deviceType == DeviceType.Digital) && !chkDigital.Checked)
			{
				reportMe = false;
			}
			if ((vchan.output.deviceType == DeviceType.None) && !chkNoController.Checked)
			{
				reportMe = false;
			}
			return reportMe;
		}

		private void Sort_RemoveDuplicateChannels_Resort()
		{
			// 1. Sort the list by SavedIndex (which is the only parameter guaranteed to be unique)
			// 2. Iterate thru the list, compare SavedIndexes and remove duplicates
			// 3. Replace (if necessary) the SortKeys to requested criteria (if not SavedIndex)
			// 4. Re-Sort (if necessary) if requested sort criteria is not SavedIndex

			// Step 1 - Sort the list by SavedIndex (which is guaranteed to be unique)
			int idx = 0;
			/*
			while (idx < chItems.Count)
			{
				chItems[idx].sortString = chItems[idx].theChannel.SavedIndex.ToString(ZEROPAD);
				idx++;
			}
			chItems.Sort();

			// Step 2 - Iterate thru the list, compare SavedIndexes and remove duplicates
			idx = 1;
			while (idx < chItems.Count)
			{
				if (chItems[idx].theChannel.SavedIndex == chItems[idx - 1].theChannel.SavedIndex)
				{
					if (chItems[idx].isKeywd)
					{
						regKeywdCount--;
					}
					else
					{
						regNonKeywdCount--;
					}
					chItems.RemoveAt(idx);
				}
				else
				{
					idx++;
				}
			}
			*/
			// Step 3 - Replace (if necessary) the SortKeys to requested criteria (if not SavedIndex)
			//if (sortOrder != SORT_BYSAVEDINDEX)
			//{
				idx = 0;
				while (idx < chItems.Count)
				{
					SeqChannelItem chItem = chItems[idx];
					Channel chan = chItem.theChannel;
					if (sortOrder == SORT_BYNAME) chItem.sortString = chan.Name;
					if (sortOrder == SORT_BYDISPLAYED) chItem.sortString = seqChanCount.ToString(ZEROPAD);
					if (sortOrder == SORT_BYOUTPUT)
					{
						chItem.sortString = chan.output.deviceType.ToString() + DASH;
						if (chan.output.deviceType == DeviceType.LOR)
						{
							chItem.sortString += chan.output.unit.ToString(ZEROPAD) + DASH;
							chItem.sortString += chan.output.channel.ToString(ZEROPAD);
						}
						if (chan.output.deviceType == DeviceType.DMX)
						{
							chItem.sortString += chan.output.universe.ToString(ZEROPAD) + DASH;
							chItem.sortString += chan.output.channel.ToString(ZEROPAD);
						}
						if (chan.output.deviceType == DeviceType.Digital)
						{
							chItem.sortString += chan.output.unit.ToString(ZEROPAD) + DASH;
							chItem.sortString += chan.output.channel.ToString(ZEROPAD);
						}
						if (chan.output.deviceType == DeviceType.None)
						{
							chItem.sortString += seqChanCount.ToString(ZEROPAD) + DASH;
							chItem.sortString += ZEROPAD;
						}
					}
					idx++;
				}

				// Step 4 - Re-Sort (if necessary) if requested sort criteria is not SavedIndex
				chItems.Sort();

			//}
		}

		private void Sort_RemoveDuplicateRGBs_Resort()
		{
			// 1. Sort the list by SavedIndex (which is the only parameter guaranteed to be unique)
			// 2. Iterate thru the list, compare SavedIndexes and remove duplicates
			// 3. Replace (if necessary) the SortKeys to requested criteria (if not SavedIndex)
			// 4. Re-Sort (if necessary) if requested sort criteria is not SavedIndex

			// Step 1 - Sort the list by SavedIndex (which is guaranteed to be unique)
			int idx = 0;
			/*
			while (idx < rgbItems.Count)
			{
				rgbItems[idx].sortString = rgbItems[idx].theRGBchannel.SavedIndex.ToString(ZEROPAD);
				idx++;
			}
			rgbItems.Sort();

			// Step 2 - Iterate thru the list, compare SavedIndexes and remove duplicates
			idx = 1;
			while (idx < rgbItems.Count)
			{
				if (rgbItems[idx].theRGBchannel.SavedIndex == rgbItems[idx - 1].theRGBchannel.SavedIndex)
				{
					if (rgbItems[idx].isKeywd)
					{
						rgbKeywdCount--;
					}
					else
					{
						rgbNonKeywdCount--;
					}
					rgbItems.RemoveAt(idx);
				}
				else
				{
					idx++;
				}
			}
			*/

			// Step 3 - Replace (if necessary) the SortKeys to requested criteria (if not SavedIndex)
			//if (sortOrder != SORT_BYSAVEDINDEX)
			//{
				idx = 0;
				while (idx < rgbItems.Count)
				{
					SeqRGBItem rgbItem = rgbItems[idx];
					Channel chan = rgbItem.theRGBchannel.redChannel;
					if (sortOrder == SORT_BYNAME) rgbItem.sortString = rgbItem.theRGBchannel.Name;
					if (sortOrder == SORT_BYDISPLAYED) rgbItem.sortString = seqChanCount.ToString(ZEROPAD);
					if (sortOrder == SORT_BYOUTPUT)
					{
						rgbItem.sortString = chan.output.deviceType.ToString() + DASH;
						if (chan.output.deviceType == DeviceType.LOR)
						{
							rgbItem.sortString += chan.output.unit.ToString(ZEROPAD) + DASH;
							rgbItem.sortString += chan.output.channel.ToString(ZEROPAD);
						}
						if (chan.output.deviceType == DeviceType.DMX)
						{
							rgbItem.sortString += chan.output.universe.ToString(ZEROPAD) + DASH;
							rgbItem.sortString += chan.output.channel.ToString(ZEROPAD);
						}
						if (chan.output.deviceType == DeviceType.Digital)
						{
							rgbItem.sortString += chan.output.unit.ToString(ZEROPAD) + DASH;
							rgbItem.sortString += chan.output.channel.ToString(ZEROPAD);
						}
						if (chan.output.deviceType == DeviceType.None)
						{
							rgbItem.sortString += seqChanCount.ToString(ZEROPAD) + DASH;
							rgbItem.sortString += ZEROPAD;
						}
					}
					idx++;
				}

				// Step 4 - Re-Sort (if necessary) if requested sort criteria is not SavedIndex
				rgbItems.Sort();

			//}
		}

		private void Sort_RemoveDuplicateGroups_Resort()
		{
			// 1. Sort the list by SavedIndex (which is the only parameter guaranteed to be unique)
			// 2. Iterate thru the list, compare SavedIndexes and remove duplicates
			// 3. Replace (if necessary) the SortKeys to requested criteria (if not SavedIndex)
			// 4. Re-Sort (if necessary) if requested sort criteria is not SavedIndex

			// Step 1 - Sort the list by SavedIndex (which is guaranteed to be unique)
			int idx = 0;
			/*
			while (idx < groupItems.Count)
			{
				groupItems[idx].sortString = groupItems[idx].theGroup.SavedIndex.ToString(ZEROPAD);
				idx++;
			}
			groupItems.Sort();

			// Step 2 - Iterate thru the list, compare SavedIndexes and remove duplicates
			idx = 1;
			while (idx < groupItems.Count)
			{
				if (groupItems[idx].theGroup.SavedIndex == groupItems[idx - 1].theGroup.SavedIndex)
				{
					if (groupItems[idx].isKeywd)
					{
						rgbKeywdCount--;
					}
					else
					{
						rgbNonKeywdCount--;
					}
					groupItems.RemoveAt(idx);
				}
				else
				{
					idx++;
				}
			}
			*/

			// Step 3 - Replace (if necessary) the SortKeys to requested criteria (if not SavedIndex)
			//if (sortOrder != SORT_BYSAVEDINDEX)
			//{
				idx = 0;
				while (idx < groupItems.Count)
				{
					SeqGroupItem groupItem = groupItems[idx];
					if (sortOrder == SORT_BYNAME) groupItem.sortString = groupItem.theGroup.Name;
					if (sortOrder == SORT_BYDISPLAYED) groupItem.sortString = seqChanCount.ToString(ZEROPAD);
					if (sortOrder == SORT_BYOUTPUT)	groupItem.sortString = ",";
					idx++;
				}

				// Step 4 - Re-Sort (if necessary) if requested sort criteria is not SavedIndex
				groupItems.Sort();

			//}
		}

		private void Sort_RemoveDuplicateVizChannels_Resort()
		{
			// 1. Sort the list by SavedIndex (which is the only parameter guaranteed to be unique)
			// 2. Iterate thru the list, compare SavedIndexes and remove duplicates
			// 3. Replace (if necessary) the SortKeys to requested criteria (if not SavedIndex)
			// 4. Re-Sort (if necessary) if requested sort criteria is not SavedIndex

			// Step 1 - Sort the list by SavedIndex (which is guaranteed to be unique)
			int idx = 0;
			/*
			while (idx < vizItems.Count)
			{
				string ss = vizItems[idx].theChannel.SavedIndex.ToString(ZEROPAD);
				ss += "." + vizItems[idx].theChannel.VizID.ToString("0");
				vizItems[idx].sortString = ss;
				idx++;
			}
			vizItems.Sort();

			// Step 2 - Iterate thru the list, compare SavedIndexes and remove duplicates
			idx = 1;
			while (idx < vizItems.Count)
			{
				if (vizItems[idx].theChannel.SavedIndex == vizItems[idx - 1].theChannel.SavedIndex)
				{
					if (vizItems[idx].isKeywd)
					{
						regKeywdCount--;
					}
					else
					{
						regNonKeywdCount--;
					}
					vizItems.RemoveAt(idx);
				}
				else
				{
					idx++;
				}
			}
			*/

			// Step 3 - Replace (if necessary) the SortKeys to requested criteria (if not SavedIndex)
			//if (sortOrder != SORT_BYSAVEDINDEX)
			//{
				idx = 0;
				while (idx < vizItems.Count)
				{
					VizChannelItem vizItem = vizItems[idx];
					VizChannel vchan = vizItem.theChannel;
					if (sortOrder == SORT_BYNAME) vizItem.sortString = vchan.Name;
					if (sortOrder == SORT_BYDISPLAYED) vizItem.sortString = seqChanCount.ToString(ZEROPAD);
					if (sortOrder == SORT_BYOUTPUT)
					{
						vizItem.sortString = vchan.output.deviceType.ToString() + DASH;
						if (vchan.output.deviceType == DeviceType.LOR)
						{
							vizItem.sortString += vchan.output.unit.ToString(ZEROPAD) + DASH;
							vizItem.sortString += vchan.output.channel.ToString(ZEROPAD);
						}
						if (vchan.output.deviceType == DeviceType.DMX)
						{
							vizItem.sortString += vchan.output.universe.ToString(ZEROPAD) + DASH;
							vizItem.sortString += vchan.output.channel.ToString(ZEROPAD);
						}
						if (vchan.output.deviceType == DeviceType.Digital)
						{
							vizItem.sortString += vchan.output.unit.ToString(ZEROPAD) + DASH;
							vizItem.sortString += vchan.output.channel.ToString(ZEROPAD);
						}
						if (vchan.output.deviceType == DeviceType.None)
						{
							vizItem.sortString += seqChanCount.ToString(ZEROPAD) + DASH;
							vizItem.sortString += ZEROPAD;
						}
					}
					idx++;
				}

				// Step 4 - Re-Sort (if necessary) if requested sort criteria is not SavedIndex
				vizItems.Sort();

			//}
		}

		private void ReportSeqRegularChannels(string fileName)
		{
			StreamWriter writer = new StreamWriter(fileName);
			string lineOut = "";
			DeviceType devType;

			if (chkHeaders.Checked)
			{
				lineOut = SeqChannelItem.Header(txtKeyword.Text);
				writer.WriteLine(lineOut);
			}

			for (int l = 0; l < chItems.Count; l++)
			{
				lineOut = chItems[l].ToString();
				writer.WriteLine(lineOut);
			}
			writer.Close();
		}

		private void ReportVizRegularChannels(string fileName)
		{
			StreamWriter writer = new StreamWriter(fileName);
			string lineOut = "";
			DeviceType devType;

			if (chkHeaders.Checked)
			{
				lineOut = VizChannelItem.Header(txtKeyword.Text);
				writer.WriteLine(lineOut);
			}

			for (int l = 0; l < vizItems.Count; l++)
			{
				lineOut = vizItems[l].ToString();
				//ReportMember(vizItems[l], writer);
				writer.WriteLine(lineOut);
			}
			writer.Close();
		}

		private void ReportSeqRGBChannels(string fileName)
		{
			StreamWriter writer = new StreamWriter(fileName);
			string lineOut = "";
			DeviceType devType;

			if (chkHeaders.Checked)
			{
				lineOut = SeqRGBItem.Header(txtKeyword.Text);
				writer.WriteLine(lineOut);
			}

			for (int l = 0; l < rgbItems.Count; l++)
			{
				lineOut = rgbItems[l].ToString();
				writer.WriteLine(lineOut);
			}
			writer.Close();
		}

		private void ReportSeqChannelGroups(string fileName)
		{
			StreamWriter writer = new StreamWriter(fileName);
			string lineOut = "";
			DeviceType devType;

			if (chkHeaders.Checked)
			{
				lineOut += "Name,";
				lineOut += txtKeyword.Text;
				lineOut += "Order,";
				lineOut += "SavedIndex,";
				lineOut += "Count";
				writer.WriteLine(lineOut);
			}

			for (int l = 0; l < groupItems.Count; l++)
			{
				lineOut = groupItems[l].ToString();
				writer.WriteLine(lineOut);
			}
			writer.Close();
		}

		private void SetTheControlsForTheHeartOfTheSun()
		{
			//! A nod to my favorite group, Pink Floyd
			Properties.Settings.Default.RegNonKeywd = chkRegNonKeywd.Checked;
			Properties.Settings.Default.RGBNonKeywd = chkRGBNonKeywd.Checked;
			Properties.Settings.Default.GroupNonKeywd = chkGrpNonKeywd.Checked;
			Properties.Settings.Default.RegKeywd = chkRegKeywd.Checked;
			Properties.Settings.Default.RGBNonKeywd = chkRGBKeywd.Checked;
			Properties.Settings.Default.RegNonKeywd = chkGrpKeywd.Checked;
			Properties.Settings.Default.GroupNonKeywd = chkHeaders.Checked;
			Properties.Settings.Default.LORcontrolled = chkLOR.Checked;
			Properties.Settings.Default.DMXcontrolled = chkDMX.Checked;
			Properties.Settings.Default.DigitalControlled = chkDigital.Checked;
			Properties.Settings.Default.NoController = chkNoController.Checked;
			Properties.Settings.Default.Keyword = txtKeyword.Text;

			if (optSortName.Checked) Properties.Settings.Default.SortOrder = SORT_BYNAME;
			if (optSortOutput.Checked) Properties.Settings.Default.SortOrder = SORT_BYOUTPUT;
			if (optSortDisplayed.Checked) Properties.Settings.Default.SortOrder = SORT_BYDISPLAYED;
			if (optSortSavedIndex.Checked) Properties.Settings.Default.SortOrder = SORT_BYSAVEDINDEX;
			Properties.Settings.Default.Duplicates = chkDuplicates.Checked;
			if (cboTracks.Items.Count > 1)
			{
				Properties.Settings.Default.TrackName = cboTracks.Text;
			}

			Properties.Settings.Default.LastSeqFile = lastSeqFile;
			Properties.Settings.Default.LastVisVile = lastVizFile;

			Properties.Settings.Default.Save();

		}


		private void GetTheControlsFromTheHeartOfTheSun()
		{
			//! A nod to my favorite group, Pink Floyd
			
			chkRegNonKeywd.Checked = Properties.Settings.Default.RegNonKeywd;
			chkRGBNonKeywd.Checked = Properties.Settings.Default.RGBNonKeywd;
			chkGrpNonKeywd.Checked = Properties.Settings.Default.GroupNonKeywd;
			chkRegKeywd.Checked = Properties.Settings.Default.RegKeywd;
			chkRGBKeywd.Checked = Properties.Settings.Default.RGBKeywd;
			chkGrpKeywd.Checked = Properties.Settings.Default.GroupKeywd;
			chkHeaders.Checked = Properties.Settings.Default.Headers;
			chkLOR.Checked = Properties.Settings.Default.LORcontrolled;
			chkDMX.Checked = Properties.Settings.Default.DMXcontrolled;
			chkDigital.Checked = Properties.Settings.Default.DigitalControlled;
			chkNoController.Checked = Properties.Settings.Default.NoController;
			txtKeyword.Text = Properties.Settings.Default.Keyword;

			sortOrder = Properties.Settings.Default.SortOrder % 4;
			if (sortOrder == SORT_BYNAME) optSortName.Checked = true;
			if (sortOrder == SORT_BYOUTPUT) optSortOutput.Checked = true;
			if (sortOrder == SORT_BYDISPLAYED) optSortDisplayed.Checked = true;
			if (sortOrder == SORT_BYSAVEDINDEX) optSortSavedIndex.Checked = true;
			chkDuplicates.Checked = Properties.Settings.Default.Duplicates;
			if (cboTracks.Items.Count > 1)
			{
				string lastTrackName = Properties.Settings.Default.TrackName;
				for (int i=0; i< cboTracks.Items.Count; i++)
				{
					int n = cboTracks.Items[i].ToString().CompareTo(lastTrackName);
					if (n==0)
					{
						cboTracks.SelectedIndex = i;
						i = 999999; // force exit of loop
					}
				}
			}

			lastSeqFile = Properties.Settings.Default.LastSeqFile;
			lastVizFile = Properties.Settings.Default.LastVisVile;

			saveOptionChanges = true;

		}

		private void EnableStuff(bool newState)
		{
			grpOptions.Enabled = newState;
			btnGenCSV.Enabled = newState;
			treeSource.Enabled = newState;
		}




		private void btnBrowseSource_Click(object sender, EventArgs e)
		{
			if (optSeq.Checked)
			{
				BrowseSourceFile();
			}
			if (optViz.Checked)
			{
				BrowseVisualFile();
			}
		}

		private void BrowseSourceFile()
		{ 
			string initDir = utils.DefaultSequencesPath;
			string initFile = "";
			if (lastSeqFile.Length > 4)
			{
				string ldir = Path.GetDirectoryName(lastSeqFile);
				if (Directory.Exists(ldir))
				{
					initDir = ldir;
					if (File.Exists(lastSeqFile))
					{
						initFile = Path.GetFileName(lastSeqFile);
					}
				}
			}


			dlgOpenFile.Filter = "Sequence Files *.las, *.lms|*.las;*.lms|Musical Sequences *.lms|*.lms|Animated Sequences *.las|*.las";
			dlgOpenFile.FilterIndex = 3;
			dlgOpenFile.DefaultExt = "*.lms";
			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			dlgOpenFile.Title = "Open Sequence...";
			ImBusy(true);
			pnlAll.Enabled = false;
			DialogResult result = dlgOpenFile.ShowDialog(this);

			if (result == DialogResult.OK)
			{
				int err = LoadSequenceFile(dlgOpenFile.FileName);
				if (err < 100)
				{
					// All this is done in LoadSequenceFile()
					//lastSeqFile = dlgOpenFile.FileName;
					//txtSourceFile.Text = lastSeqFile;
					//Properties.Settings.Default.LastSeqFile = lastSeqFile;
					//Properties.Settings.Default.Save();
				}
			} // end if (result = DialogResult.OK)
			ImBusy(false);
			pnlAll.Enabled = true;
			//btnSummary.Enabled = (mappingCount > 0);
			//mnuSummary.Enabled = btnSummary.Enabled;

		}

		public int LoadSequenceFile(string sourceChannelFile)
		{
			ImBusy("Loading " + Path.GetFileNameWithoutExtension(sourceChannelFile));
			int err = theSequence.ReadSequenceFile(sourceChannelFile);
			if (err < 100)
			{
				lastSeqFile = sourceChannelFile;
				txtSourceFile.Text = utils.ShortenLongPath(lastSeqFile, 80);
				this.Text = "List-O-Rama - " + Path.GetFileName(lastSeqFile);
				FileInfo fi = new FileInfo(sourceChannelFile);
				//Properties.Settings.Default.BasePath = fi.DirectoryName;
				Properties.Settings.Default.LastSeqFile = lastSeqFile;
				Properties.Settings.Default.Save();
				utils.TreeFillChannels(treeSource, theSequence, ref sourceNodesSI, false, false);

				cboTracks.Items.Clear();
				cboTracks.Items.Add("All Tracks");
				for (int n = 0; n < theSequence.Tracks.Count; n++)
				{
					cboTracks.Items.Add(theSequence.Tracks[n].Name);
				}
				cboTracks.SelectedIndex = 0;

				treeSource.Visible = true;
				//BuildChannelList();
			}
			EnableStuff(true);
			ImBusy(false);
			return err;
		}

		private void BrowseVisualFile()
		{
			string initDir = utils.DefaultVisualizationsPath;
			string initFile = "";
			if (lastVizFile.Length > 4)
			{
				string ldir = Path.GetDirectoryName(lastVizFile);
				if (Directory.Exists(ldir))
				{
					initDir = ldir;
					if (File.Exists(lastVizFile))
					{
						initFile = Path.GetFileName(lastVizFile);
					}
				}
			}


			dlgOpenFile.Filter = "Visualization Files *.lee|*.lee";
			dlgOpenFile.FilterIndex = 1;
			dlgOpenFile.DefaultExt = "*.lee";
			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			dlgOpenFile.Title = "Open Visualization...";
			pnlAll.Enabled = false;
			ImBusy(true);
			DialogResult result = dlgOpenFile.ShowDialog(this);

			if (result == DialogResult.OK)
			{
				string vFile = dlgOpenFile.FileName;
				int err = LoadVisualFile(vFile);
			} // end if (result = DialogResult.OK)
			pnlAll.Enabled = true;
			ImBusy(false);
			//btnSummary.Enabled = (mappingCount > 0);
			//mnuSummary.Enabled = btnSummary.Enabled;

		}

		public int LoadVisualFile(string vizFile)
		{
			ImBusy("Loading " + Path.GetFileNameWithoutExtension(vizFile));
			int err = theViz.ReadVisualizationFile(vizFile);
			if (err < 100)
			{
				lastVizFile = vizFile;
				txtSourceFile.Text = utils.ShortenLongPath(lastVizFile, 80);
				this.Text = "List-O-Rama - " + Path.GetFileName(lastVizFile);
				//FileInfo fi = new FileInfo(vizFile);
				//Properties.Settings.Default.BasePath = fi.DirectoryName;
				Properties.Settings.Default.LastVizFile = lastVizFile;
				Properties.Settings.Default.Save();
			}
			EnableStuff(true);
			ImBusy(false);
			return err;
		}

		private void ImBusy(string statusMsg)
		{
			pnlStatus.Text = statusMsg;
			staStatus.Refresh();
			ImBusy(true);
		}

		private void ImBusy(bool workingHard)
		{
			if (workingHard)
			{
				this.Cursor = Cursors.WaitCursor;
			}
			else
			{
				pnlStatus.Text = "";
				this.Cursor = Cursors.Default;
			}
			workingMyAssOff = workingHard;
			pnlAll.Enabled = !workingHard;

		}


		private void InitForm()
		{
			SeqFolder = utils.DefaultSequencesPath;


			lastSeqFile = Properties.Settings.Default.LastSeqFile;
			if (!File.Exists(lastSeqFile))
			{
				lastSeqFile = "";
				Properties.Settings.Default.LastSeqFile = lastSeqFile;
			}


			lastSavePath = Properties.Settings.Default.LastSavePath;




			//Properties.Settings.Default.BasePath = basePath;
			//Properties.Settings.Default.Save();


			RestoreFormPosition();

		} // end InitForm


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

		/*
		private TreeNode FindNode(TreeNodeCollection treeNodes, string tag)
		{
			int tl = tag.Length;
			string st = "";
			TreeNode ret = new TreeNode("NOT FOUND");
			bool found = false;
			foreach (TreeNode nOde in treeNodes)
			{
				if (!found)
				{
					if (nOde.Tag.ToString().Length >= tag.Length)
					{
						st = nOde.Tag.ToString().Substring(0, tl);
						if (tag.CompareTo(st) == 0)
						{
							ret = nOde;
							found = true;
							break;
						}
					}
					if (!found)
					{
						if (nOde.Nodes.Count > 0)
						{
							ret = FindNode(nOde.Nodes, tag);
							if (ret.Text.CompareTo("NOT FOUND") != 0)
							{
								found = true;
							}
						}
					}
				}
			}
			return ret;
		}

		private int HighlightSourceNode(string tag)
		{
			int tl = tag.Length;
			int found = 0;
			foreach (TreeNode nOde in treeSource.Nodes)
			{
				// Reset any previous selections
				HighlightNode(nOde, false);

				// If node is mapped, it's tag will include the node it is mapped to, and thus the tag length
				// will be longer than just it's own tag info
				if (nOde.Tag.ToString().Length > tl)
				{
					string myTag = nOde.Tag.ToString().Substring(0, tl);
					if (tag.CompareTo(myTag) == 0)
					{
						// The old channel is mapped to this one
						nOde.EnsureVisible();
						HighlightNode(nOde, true);
						treeSource.SelectedNode = nOde;
						found++;

					}
				} // end long tag
			} // end foreach
			return found;
		} // end HighlightNewNodes

		private void UnHighlightAllNodes(TreeNodeCollection treeNodes)
		{
			foreach (TreeNode nOde in treeNodes)
			{
				// Reset any previous selections
				HighlightNode(nOde, false);
				if (nOde.Nodes.Count > 0)
				{
					UnHighlightAllNodes(nOde.Nodes);
				}
			} // end foreach
		}

		private void treeSource_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode sn = e.Node;
			if (sn != null)
			{
				IMember sid = (IMember)sn.Tag;
				//int sourceSI = sid.SavedIndex;
				// Did the selection change?
					
			}
		} // end treeSource_AfterSelect

		private void treeMaster_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode mn = e.Node;
			if (mn != null)
			{
				IMember mid = (IMember)mn.Tag;
				// Did the selection change?
				
			}
		} // end treNewChannel_Click


		private void HighlightNode(TreeNode nOde, bool highlight)
		{
			if (highlight)
			{
				if (nOde.BackColor == COLOR_BK_SELECTED)
				{
					nOde.BackColor = COLOR_BK_BOTH;
				}
				else
				{
					nOde.BackColor = COLOR_BK_MATCHED;
				}
				nOde.ForeColor = COLOR_FR_SELECTED;
			}
			else
			{
				if (nOde.BackColor == COLOR_BK_BOTH)
				{
					nOde.BackColor = COLOR_BK_SELECTED;
				}
				else
				{
					nOde.BackColor = COLOR_BK_NONE;
					nOde.ForeColor = COLOR_FR_NONE;
				}
			}
		}

		private void SelectNode(TreeNode nOde, bool select)
		// BEWARE: Procedure name is a bit misleading
		// This procedure sets the appearance of a node to the 'Selected' appearance
		// (As opposed to the 'Matched' appearance (see HighlightNode procedure above)
		{
			if (select)
			{
				if (nOde.BackColor == COLOR_BK_MATCHED)
				{
					nOde.BackColor = COLOR_BK_BOTH;
				}
				else
				{
					nOde.BackColor = COLOR_BK_SELECTED;
				}
				nOde.ForeColor = COLOR_FR_SELECTED;
			}
			else
			{
				if (nOde.BackColor == COLOR_BK_BOTH)
				{
					nOde.BackColor = COLOR_BK_MATCHED;
				}
				else
				{
					nOde.BackColor = COLOR_BK_NONE;
					nOde.ForeColor = COLOR_FR_NONE;
				}
			}
		}

		private void SelectNodes(TreeNodeCollection nOdes, int SavedIndex, bool select)
		{
			//string nTag = "";
			IMember nID = null;
			foreach (TreeNode nOde in nOdes)
			{
				nID = (IMember)nOde.Tag;
				if (nID.SavedIndex == SavedIndex)
				{
					SelectNode(nOde, select);
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					SelectNodes(nOde.Nodes, SavedIndex, select);
				}
			}
		}

		private void UnSelectAllNodes(TreeNodeCollection nOdes)
		{
			foreach (TreeNode nOde in nOdes)
			{
				if (nOde.BackColor == COLOR_BK_BOTH)
				{
					nOde.BackColor = COLOR_BK_MATCHED;
				}
				else
				{
					nOde.BackColor = COLOR_BK_NONE;
					nOde.ForeColor = COLOR_FR_NONE;
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					UnSelectAllNodes(nOde.Nodes);
				}
			}
		}

		private void HighlightNodes(TreeNodeCollection nOdes, int SavedIndex, bool highlight)
		{
			//string nTag = "";
			IMember nID = null;
			foreach (TreeNode nOde in nOdes)
			{
				nID = (IMember)nOde.Tag;
				if (nID.SavedIndex == SavedIndex)
				{
					HighlightNode(nOde, highlight);
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					HighlightNodes(nOde.Nodes, SavedIndex, highlight);
				}
			}
		}

			private void UnhighlightAllNodes(TreeNodeCollection nOdes)
		{
			foreach (TreeNode nOde in nOdes)
			{
				if (nOde.BackColor == COLOR_BK_BOTH)
				{
					nOde.BackColor = COLOR_BK_SELECTED;
				}
				else
				{
					nOde.BackColor = COLOR_BK_NONE;
					nOde.ForeColor = COLOR_FR_NONE;
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					UnhighlightAllNodes(nOde.Nodes);
				}
			}
		}

		private void BoldNodes(List<TreeNode> nodeList, bool emBolden)
		{
			//string nTag = "";


			foreach (TreeNode thenOde in nodeList)
			{
					if (emBolden)
					{
						thenOde.NodeFont = new Font(treeSource.Font, FontStyle.Bold);
					}
					else
					{
						thenOde.NodeFont = new Font(treeSource.Font, FontStyle.Regular);
					}
					thenOde.Checked = emBolden;
			}
		}

		private int addElement(ref int[] numbers)
		{
			int newIdx = utils.UNDEFINED;

			if (numbers == null)
			{
				Array.Resize(ref numbers, 1);
				newIdx = 0;
			}
			else
			{
				int l = numbers.Length;
				Array.Resize(ref numbers, l + 1);
				newIdx = l;
			}

			return newIdx;
		}

		private int removeElement(ref int[] numbers, int index)
		{
			int l = numbers.Length;
			if (index < l)
			{
				l--;
				if (index < (l))
				{
					for (int i = index; i < l; i++)
					{
						numbers[i] = numbers[i + 1];
					}
				}
			}
			if (l == 0)
			{
				numbers = null;
			}
			else
			{
				Array.Resize(ref numbers, l);
			}

			return l;
		}

		private int FindElement(ref int[] numbers, int SID)
		{
			int found = utils.UNDEFINED;
			for (int i = 0; i < numbers.Length; i++)
			{
				if (numbers[i] == SID)
				{
					found = i;
					break;
				}
			}
			return found;
		}
		*/

		private void StatusMessage(string message)
		{
			// For now at least... I think I'm gonna use this just to show WHY the 'Map' button is not enabled


			//lblMessage.Text = message;
			//lblMessage.Visible = (message.Length > 0);
			//lblMessage.Left = (this.Width - lblMessage.ClientSize.Width) / 2;

		}

		private void LogMessage(string message)
		{
			//TODO: This
		}

		private void frmList_Shown(object sender, EventArgs e)
		{

		}



		private void FirstShow()
		{
			string msg;
			DialogResult dr;

			ProcessCommandLine();
			if (batch_fileCount < 1)
			{
				if (File.Exists(lastSeqFile))
				{
					ModeChange(Properties.Settings.Default.VizMode);

				} // end last sequence file exists
			}
		} // end form shown event


		private void frmList_Paint(object sender, PaintEventArgs e)
		{
			if (!firstShown)
			{
				firstShown = true;
				FirstShow();
			}
		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(helpPage);
		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy("About List-O-Rama");
			Form aboutBox = new About();
			aboutBox.ShowDialog(this);
			ImBusy(false);
		}


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
			FormWindowState savedState = (FormWindowState)Properties.Settings.Default.WindowState;
			int x = savedLoc.X; // Default to saved postion and size, will override if necessary
			int y = savedLoc.Y;
			int w = savedSize.Width;
			int h = savedSize.Height;

			if ((FormBorderStyle == FormBorderStyle.Sizable) || (FormBorderStyle == FormBorderStyle.SizableToolWindow))
			{
				if (w < MinimumSize.Width) w = MinimumSize.Width;
				if (h < MinimumSize.Height) h = MinimumSize.Height;
			}
			else // Fixed Border Style
			{
				if (w < Size.Width) w = Size.Width;
				if (h < Size.Height) h = Size.Height;
			}
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

		private void Event_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				processDrop = true;
				ProcessFileList(files);
				//this.Cursor = Cursors.Default;
			}

		}

		private void Event_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
			//this.Cursor = Cursors.Cross;
		}


		private void ProcessCommandLine()
		{
			commandArgs = Environment.GetCommandLineArgs();
			string arg;
			for (int f = 0; f < commandArgs.Length; f++)
			{
				arg = commandArgs[f];
				// Does it LOOK like a file?
				byte isFile = 0;
				if (arg.Substring(1, 2).CompareTo(":\\") == 0) isFile = 1;  // Local File
				if (arg.Substring(0, 2).CompareTo("\\\\") == 0) isFile = 1; // UNC file
				if (arg.Substring(4).IndexOf(".") > utils.UNDEFINED) isFile++;  // contains a period
				if (utils.InvalidCharacterCount(arg) == 0) isFile++;
				if (isFile == 3)
				{
					if (File.Exists(arg))
					{
						string ext = Path.GetExtension(arg).ToLower();
						if (ext.CompareTo(".exe") == 0)
						{
							if (f == 0)
							{
								thisEXE = arg;
							}
						}
						if ((ext.CompareTo(".lms") == 0) || (ext.CompareTo(".las") == 0))
						{
							Array.Resize(ref batch_fileList, batch_fileCount + 1);
							batch_fileList[batch_fileCount] = arg;
							batch_fileCount++;
						}
						else
						{
							if (ext.CompareTo("chsel") == 0)
							{
								cmdSelectionsFile = arg;
							}
						}
					}
				}
				else
				{
					// Not a file, is it an argument
					if (arg.Substring(0, 1).CompareTo("/") == 0)
					{
						//TODO: Process any commands
					}
				}
			} // foreach argument
			if (batch_fileCount == 1)
			{


			}
			else
			{
				if (batch_fileCount > 1)
				{
					ProcessSourcesBatch(batch_fileList);
				}
			}


		}

		private void ProcessFileList(string[] batchFilenames)
		{
			batch_fileCount = 0; // reset
			bool inclSelections = false;
			DialogResult dr = DialogResult.None;

			foreach (string file in batchFilenames)
			{
				// None of this should be necessary for a file dragdrop			

				// Does it LOOK like a file?
				//byte isFile = 0;
				//if (arg.Substring(1, 2).CompareTo(":\\") == 0) isFile = 1;  // Local File
				//if (arg.Substring(0, 2).CompareTo("\\\\") == 0) isFile = 1; // UNC file
				//if (arg.Substring(4).IndexOf(".") > utils.UNDEFINED) isFile++;  // contains a period
				//if (InvalidCharacterCount(arg) == 0) isFile++;
				//if (isFile == 2)
				//{
				//	if (File.Exists(arg))
				//	{
				string ext = Path.GetExtension(file).ToLower();
				if (ext.CompareTo(".lcc") == 0)
				{
					if (File.Exists(file))
					{
						//LoadSourceFile(file);
					}
				}
				if (ext.CompareTo(".chmap") == 0)
				{
					if (File.Exists(file))
					{
						// Do something...
					}
				}
				if ((ext.CompareTo(".lms") == 0) ||
						(ext.CompareTo(".las") == 0))
				{
					Array.Resize(ref batch_fileList, batch_fileCount + 1);
					batch_fileList[batch_fileCount] = file;
					batch_fileCount++;

				}
				//	}
				//}
			}
			if (batch_fileCount > 1)
			{
				batchMode = true;
				ProcessSourcesBatch(batch_fileList);
			}
			else
			{
			}
		} // end ProcessDragDropFiles

		private void ProcessSourcesBatch(string[] batchFilenames)
		{
			//if (batch_fileCount > 0)
			if (theSequence.Members.Items.Count > 1)
			{
				//ShowProgressBars(2);
				for (int f = 0; f < batch_fileCount; f++)
				{
					LoadSequenceFile(batch_fileList[f]);
					if (File.Exists(lastSeqFile))
					{
						batch_fileNumber = f;
						// Do Something...
						string oldnm = Path.GetFileNameWithoutExtension(lastSeqFile);
						//string newnm = SuggestedNewName(oldnm); // R we gettin here?
						string newfl = Path.GetDirectoryName(lastSavePath) + "\\";
						//newfl += newnm;
						newfl += Path.GetExtension(lastSeqFile).ToLower();
						//SaveNewMappedSequence(newfl);
					}
					else
					{
						f = batch_fileCount;
						break;
					}
				} // cmdSeqFileCount-- Batch Mode or Not
			}
			batchMode = false;
			//ShowProgressBars(0);
			ImBusy(false);
			//string msg = "Batch mode is not supported... yet.\r\nLook for this feature in a future release (soon)!";
			//MessageBox.Show(this, msg, "Batch Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
		} // end ProcessSourcesBatch


		private void frmList_Load(object sender, EventArgs e)
		{
			InitForm();
			//SetTheControlsForTheHeartOfTheSun();

		}

		private void frmList_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();

		}

		private void OptionCheckChanged(object sender, EventArgs e)
		{
			bool en1 = chkRegNonKeywd.Checked || chkRegKeywd.Checked ||
							chkRGBNonKeywd.Checked || chkRGBKeywd.Checked ||
							chkGrpNonKeywd.Checked || chkGrpKeywd.Checked;
			bool en2 = chkLOR.Checked || chkDMX.Checked ||
							chkDigital.Checked || chkNoController.Checked;
			bool enbl = (en1 && en2 && (theSequence.ChannelGroups.Count > 0) && (cboTracks.Items.Count > 1));
			btnGenCSV.Enabled = enbl;

			if (saveOptionChanges)
			{
				Properties.Settings.Default.RegNonKeywd =					chkRegNonKeywd.Checked;
				Properties.Settings.Default.RegKeywd =						chkRegKeywd.Checked;
				Properties.Settings.Default.RGBNonKeywd =					chkRGBNonKeywd.Checked;
				Properties.Settings.Default.RGBKeywd =						chkRGBKeywd.Checked;
				Properties.Settings.Default.GroupNonKeywd =				chkGrpNonKeywd.Checked;
				Properties.Settings.Default.GroupKeywd =					chkGrpKeywd.Checked;
				Properties.Settings.Default.Headers =						chkHeaders.Checked;
				Properties.Settings.Default.LORcontrolled =			chkLOR.Checked;
				Properties.Settings.Default.DMXcontrolled =			chkDMX.Checked;
				Properties.Settings.Default.DigitalControlled = chkDigital.Checked;
				Properties.Settings.Default.NoController =			chkNoController.Checked;

				if (optSortName.Checked)			 sortOrder = SORT_BYNAME;
				if (optSortOutput.Checked)     sortOrder = SORT_BYOUTPUT;
				if (optSortDisplayed.Checked)	 sortOrder = SORT_BYDISPLAYED;
				if (optSortSavedIndex.Checked) sortOrder = SORT_BYSAVEDINDEX;
				Properties.Settings.Default.SortOrder = sortOrder;

				Properties.Settings.Default.Save();
			}
		}

		private void txtKeyword_TextChanged(object sender, EventArgs e)
		{
			if (txtKeyword.Text.Length > 0)
			{ 
				chkRegKeywd.Text =			REGCHAN + CHWITH +		txtKeyword.Text + utils.ENDQT;
				chkRegNonKeywd.Text =		REGCHAN + CHWITHOUT + txtKeyword.Text + utils.ENDQT;
				chkRGBKeywd.Text =			RGBCHAN + CHWITH +		txtKeyword.Text + utils.ENDQT;
				chkRGBNonKeywd.Text =		RGBCHAN + CHWITHOUT + txtKeyword.Text + utils.ENDQT;
				chkGrpKeywd.Text =		CHANGRP + CHWITH +		txtKeyword.Text + utils.ENDQT;
				chkGrpNonKeywd.Text = CHANGRP + CHWITHOUT + txtKeyword.Text + utils.ENDQT;
				chkRegNonKeywd.Visible = true;
				chkRGBNonKeywd.Visible = true;
				chkGrpNonKeywd.Visible = true;
				chkRGBKeywd.Top = 108;
				chkGrpKeywd.Top = 154;
			}
			else
			{
				chkRegKeywd.Text = REGCHAN;
				chkRGBKeywd.Text = RGBCHAN;
				chkGrpKeywd.Text = CHANGRP;
				chkRegNonKeywd.Visible = false;
				chkRGBNonKeywd.Visible = false;
				chkGrpNonKeywd.Visible = false;
				chkRGBKeywd.Top = 85;
				chkGrpKeywd.Top = 108;
			}

			Properties.Settings.Default.Keyword = txtKeyword.Text;
			Properties.Settings.Default.Save();
		}

		private void picSearch_Click(object sender, EventArgs e)
		{
			int chanTotCount = 0;
			int rgbTotCount = 0;
			int grpTotCount = 0;
			int chanKeywdCount = 0;
			int rgbKeywdCount = 0;
			int grpKeywdCount = 0;
			int chanSelCount = 0;
			int rgbSelCount = 0;
			int grpSelCount = 0;

		}

		private void btnBrowseVisual_Click(object sender, EventArgs e)
		{
			BrowseVisualFile();
		}

		private void optType_CheckedChanged(object sender, EventArgs e)
		{
			if (!ignoreModeChange) ModeChange(optViz.Checked);

		}

		private void ModeChange(bool vizMode)
		{
			ignoreModeChange = true;
			optViz.Checked = vizMode;
			optSeq.Checked = !vizMode;
			bool fileValid = false;
			bool pathValid = false;
			bool enbl = false;
			if (optSeq.Checked)
			{
				try
				{
					string lf = lastSeqFile;
					if (lf.Length > 5)
					{
						string lp = Path.GetDirectoryName(lf);
						if (lp.Length > 5)
						{
							pathValid = true;
						}
						if (pathValid)
						{
							if (File.Exists(lf))
							{
								//int errs = theSequence.ReadSequenceFile(lf);
								int errs = LoadSequenceFile(lf);
								if (errs == 0)
								{
									lastSeqFile = lf;
									txtSourceFile.Text = utils.ShortenLongPath(lf, 100);
									enbl = true;
								}
							}
						}
					}
					GetTheControlsFromTheHeartOfTheSun();
				}
				catch
				{
					// Oh Well, just don't do anything else
				}
			}
			if (optViz.Checked)
			{
				try
				{
					string lf = lastVizFile;
					if (lf.Length > 5)
					{
						string lp = Path.GetDirectoryName(lf);
						if (lp.Length > 5)
						{
							pathValid = true;
						}
						if (pathValid)
						{
							if (File.Exists(lf))
							{
								//int errs = theSequence.ReadSequenceFile(lf);
								int errs = LoadVisualFile(lf);
								if (errs == 0)
								{
									lastSeqFile = lf;
									txtSourceFile.Text = utils.ShortenLongPath(lf, 100);
									enbl = true;
								}
							}
						}
					}
				}
				catch
				{
					// Oh Well, just don't do anything else
				}

			}
			EnableStuff(enbl);
			ignoreModeChange = false;
		}

		private void chkKeyword_CheckedChanged(object sender, EventArgs e)
		{
		}

		#region Filtered Tree Fills
		public void FilteredFillChannels(TreeView tree, Sequence4 seq, List<TreeNode>[] siNodes, bool selectedOnly, bool inclRGBchildren)
		{
			int listSize = seq.Members.HighestSavedIndex + seq.Tracks.Count + seq.TimingGrids.Count + 1;
			if (siNodes == null)
			{
				Array.Resize(ref siNodes, listSize);
			}
			int t = SeqEnums.MEMBER_Channel | SeqEnums.MEMBER_RGBchannel | SeqEnums.MEMBER_ChannelGroup | SeqEnums.MEMBER_Track;
			FilteredFillChannels(tree, seq, siNodes, selectedOnly, inclRGBchildren, t);
		}

		public void FilteredFillChannels(TreeView tree, Sequence4 seq, List<TreeNode>[] siNodes, bool selectedOnly, bool inclRGBchildren, int memberTypes)
		{
			//TODO: 'Selected' not implemented yet

			tree.Nodes.Clear();
			nodeIndex = 1;
			int listSize = seq.Members.HighestSavedIndex + seq.Tracks.Count + seq.TimingGrids.Count + 1;
			//Array.Resize(ref siNodes, listSize);
			if (siNodes == null)
			{
				Array.Resize(ref siNodes, listSize);
			}
			if (listSize >= siNodes.Length)
			{
				Array.Resize(ref siNodes, listSize);
			}


			//const string ERRproc = " in TreeFillChannels(";
			//const string ERRtrk = "), in Track #";
			//const string ERRitem = ", Items #";
			//const string ERRline = ", Line #";



			for (int n = 0; n < siNodes.Length; n++)
			{
				//siNodes[n] = null;
				//siNodes[n] = new List<TreeNode>();
				//siNodes.Add(new List<TreeNode>());
				siNodes[n] = new List<TreeNode>();
			}

			for (int t = 0; t < seq.Tracks.Count; t++)
			{
				if (FilterNode(seq.Tracks[t]))
				{
					TreeNode trackNode = FilteredAddTrack(seq, tree.Nodes, t, siNodes, selectedOnly, inclRGBchildren, memberTypes);
				}
			}
		}

		private TreeNode FilteredAddTrack(Sequence4 seq, TreeNodeCollection baseNodes, int trackNumber, List<TreeNode>[] siNodes, bool selectedOnly,
			bool inclRGBchildren, int memberTypes)
		{
			if (siNodes == null)
			{
				Array.Resize(ref siNodes, seq.Members.HighestSavedIndex);
			}

			string nodeText = "";
			bool inclChan = false;
			if ((memberTypes & SeqEnums.MEMBER_Channel) > 0) inclChan = true;
			bool inclRGB = false;
			if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0) inclRGB = true;

			// TEMPORARY, FOR DEBUGGING
			// int tcount = 0;
			int gcount = 0;
			int rcount = 0;
			int ccount = 0;
			int dcount = 0;

			//try
			//{
			Track theTrack = seq.Tracks[trackNumber];
			nodeText = theTrack.Name;
			TreeNode trackNode = baseNodes.Add(nodeText);
			List<TreeNode> qlist;

			//int inclCount = theTrack.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
			//if (inclCount > 0)
			//{
			// Tracks don't normally have savedIndexes
			// But we will assign one for tracking and matching purposes
			//theTrack.SavedIndex = seq.Members.HighestSavedIndex + t + 1;

			//if ((memberTypes & SeqEnums.MEMBER_Track) > 0)
			//{
			baseNodes = trackNode.Nodes;
			nodeIndex++;
			trackNode.Tag = theTrack;

			trackNode.ImageKey = utils.ICONtrack;
			trackNode.SelectedImageKey = utils.ICONtrack;
			trackNode.Checked = theTrack.Selected;
			//}

			for (int ti = 0; ti < theTrack.Members.Count; ti++)
			{
				//try
				//{
				IMember member = theTrack.Members.Items[ti];
				int si = member.SavedIndex;
				if (member != null)
				{
					if (member.MemberType == MemberType.ChannelGroup)
					{
						ChannelGroup memGrp = (ChannelGroup)member;
						int inclCount = FilteredDescendantCount(memGrp.Members, selectedOnly, inclChan, inclRGB, inclRGBchildren);
						if (inclCount > 0)
						{

							//! left off here!
							//! Add 'FilterNode' check before adding channels, RGB channels, and groups
							//! Also create and switch to new version of Members.DescendantCount which performs the filtering



							//if (FilterNode(member))
							//{
								TreeNode groupNode = FilteredAddGroup(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, inclRGBchildren, memberTypes);
								//AddNode(siNodes[si], groupNode);
								qlist = siNodes[si];
								if (qlist == null) qlist = new List<TreeNode>();
								qlist.Add(groupNode);
								gcount++;
							//}
							//siNodes[si].Add(groupNode);
						}
					}
					if (member.MemberType == MemberType.CosmicDevice)
					{
						CosmicDevice memDev = (CosmicDevice)member;
						int inclCount = FilteredDescendantCount(memDev.Members, selectedOnly, inclChan, inclRGB, inclRGBchildren);
						if (inclCount > 0)
						{
							//if (FilterNode(member))
							//{
								TreeNode cosmicNode = FilteredAddGroup(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, inclRGBchildren, memberTypes);
								//AddNode(siNodes[si], groupNode);
								qlist = siNodes[si];
								if (qlist == null) qlist = new List<TreeNode>();
								qlist.Add(cosmicNode);
								dcount++;
							//}
								//siNodes[si].Add(groupNode);
						}
					}
					if (member.MemberType == MemberType.RGBchannel)
					{
						//if (FilterNode(member))
						//{
							TreeNode rgbNode = FilteredAddRGBchannel(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, inclRGBchildren);
							//AddNode(siNodes[si], rgbNode);
							//siNodes[si].Add(rgbNode);
							qlist = siNodes[si];
							if (qlist == null) qlist = new List<TreeNode>();
							qlist.Add(rgbNode);
							rcount++;
						//}
					}
					if (member.MemberType == MemberType.Channel)
					{
						if (FilterNode(member))
						{
							TreeNode channelNode = FilteredAddChannel(seq, baseNodes, member.SavedIndex, selectedOnly);
							//AddNode(siNodes[si], channelNode);
							//siNodes[si].Add(channelNode);
							qlist = siNodes[si];
							if (qlist == null) qlist = new List<TreeNode>();
							qlist.Add(channelNode);
							ccount++;
						}
					}
				} // end not null
					//} // end try
				#region catch1
				/*	
				catch (System.NullReferenceException ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					catch (System.InvalidCastException ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					catch (Exception ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					*/
				#endregion

			} // end loop thru track items
			#region catch2 
			/*
				} // end try
				catch (System.NullReferenceException ex)
				{
					StackTrace st = new StackTrace(ex, true);
					StackFrame sf = st.GetFrame(st.FrameCount - 1);
					string emsg = ex.ToString();
					emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
					emsg += ERRline + sf.GetFileLineNumber();
					#if DEBUG
						System.Diagnostics.Debugger.Break();
					#endif
					utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
				}
				catch (System.InvalidCastException ex)
				{
					StackTrace st = new StackTrace(ex, true);
					StackFrame sf = st.GetFrame(st.FrameCount - 1);
					string emsg = ex.ToString();
					emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
					emsg += ERRline + sf.GetFileLineNumber();
					#if DEBUG
						System.Diagnostics.Debugger.Break();
					#endif
					utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
				}
				catch (Exception ex)
				{
					StackTrace st = new StackTrace(ex, true);
					StackFrame sf = st.GetFrame(st.FrameCount - 1);
					string emsg = ex.ToString();
					emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
					emsg += ERRline + sf.GetFileLineNumber();
					#if DEBUG
						System.Diagnostics.Debugger.Break();
					#endif
					utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
				}
				*/
			#endregion

		

			//	int x = 1; // Check ccount, rcount, gcount

			return trackNode;
		} // end fillOldChannels

		private TreeNode FilteredAddGroup(Sequence4 seq, TreeNodeCollection baseNodes, int groupSI, List<TreeNode>[] siNodes, bool selectedOnly,
			bool inclRGBchildren, int memberTypes)
		{
			TreeNode groupNode = null;
			TreeNodeCollection grpNodes = baseNodes;
			bool phooo = false;

			if (groupSI >= siNodes.Length)
			{
				Array.Resize(ref siNodes, groupSI + 1);
			}

			if (siNodes[groupSI] != null)
			{
				//ChanInfo nodeTag = new ChanInfo();
				//nodeTag.MemberType = MemberType.ChannelGroup;
				//nodeTag.objIndex = groupIndex;
				//nodeTag.SavedIndex = theGroup.SavedIndex;
				//nodeTag.nodeIndex = nodeIndex;

				//ChannelGroup theGroup = seq.ChannelGroups[groupIndex];
				ChannelGroup theGroup = (ChannelGroup)seq.Members.bySavedIndex[groupSI];

				//IMember groupID = theGroup;

				// Include groups in the TreeView?
				//if ((memberTypes & SeqEnums.MEMBER_ChannelGroup) > 0)
				if (FilterNode(theGroup))
				{
					string nodeText = theGroup.Name;
					phooo = true;
					groupNode = new TreeNode(nodeText);

					nodeIndex++;
					groupNode.Tag = theGroup;
					groupNode.ImageKey = utils.ICONchannelGroup;
					groupNode.SelectedImageKey = utils.ICONchannelGroup;
					groupNode.Checked = theGroup.Selected;
					grpNodes = groupNode.Nodes;
					siNodes[groupSI].Add(groupNode);
				}
				//List<TreeNode> qlist;

				// const string ERRproc = " in TreeFillChannels-AddGroup(";
				// const string ERRgrp = "), in Group #";
				// const string ERRitem = ", Items #";
				// const string ERRline = ", Line #";

				for (int gi = 0; gi < theGroup.Members.Count; gi++)
				{
					//try
					//{
					IMember member = theGroup.Members.Items[gi];
					int si = member.SavedIndex;
					if (member.MemberType == MemberType.ChannelGroup)
					{
						ChannelGroup memGrp = (ChannelGroup)member;
						bool inclChan = false;
						if ((memberTypes & SeqEnums.MEMBER_Channel) > 0) inclChan = true;
						bool inclRGB = false;
						if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0) inclRGB = true;
						int inclCount = FilteredDescendantCount(memGrp.Members, selectedOnly, inclChan, inclRGB, inclRGBchildren);
						if (inclCount > 0)
						{
							//if (FilterNode(member))
							//{
								TreeNode subGroupNode = FilteredAddGroup(seq, grpNodes, member.SavedIndex, siNodes, selectedOnly, inclRGBchildren, memberTypes);
								//qlist = siNodes[si];
								//qlist.Add(subGroupNode);
							//}
							}
						int cosCount = FilteredDescendantCount(memGrp.Members, selectedOnly, inclChan, inclRGB, inclRGBchildren);
					}
					if (member.MemberType == MemberType.CosmicDevice)
					{
						CosmicDevice memDev = (CosmicDevice)member;
						bool inclChan = false;
						if ((memberTypes & SeqEnums.MEMBER_Channel) > 0) inclChan = true;
						bool inclRGB = false;
						if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0) inclRGB = true;
						int inclCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, inclRGBchildren);
						if (inclCount > 0)
						{
							//if (FilterNode(member))
							//{
								TreeNode subGroupNode = FilteredAddGroup(seq, grpNodes, member.SavedIndex, siNodes, selectedOnly, inclRGBchildren, memberTypes);
								//qlist = siNodes[si];
								//qlist.Add(subGroupNode);
							//}
							}
						int cosCount = FilteredDescendantCount(memDev.Members, selectedOnly, inclChan, inclRGB, inclRGBchildren);
					}
					if (member.MemberType == MemberType.Channel)
					{
						if ((memberTypes & SeqEnums.MEMBER_Channel) > 0)
						{
							if (FilterNode(member))
							{
								TreeNode channelNode = FilteredAddChannel(seq, grpNodes, member.SavedIndex, selectedOnly);
								siNodes[si].Add(channelNode);
							}
						}
					}
					if (member.MemberType == MemberType.RGBchannel)
					{
						if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0)
						{
							//if (FilterNode(member))
							//{
								TreeNode rgbChannelNode = FilteredAddRGBchannel(seq, grpNodes, member.SavedIndex, siNodes, selectedOnly, inclRGBchildren);
								siNodes[si].Add(rgbChannelNode);
							//}
						}
					}
					#region catch
					/*
		} // end try
			catch (Exception ex)
			{
				StackTrace st = new StackTrace(ex, true);
				StackFrame sf = st.GetFrame(st.FrameCount - 1);
				string emsg = ex.ToString();
				emsg += ERRproc + seq.filename + ERRgrp + groupIndex.ToString() + ERRitem + gi.ToString();
				emsg += ERRline + sf.GetFileLineNumber();
				#if DEBUG
					Debugger.Break();
				#endif
				utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
			} // end catch
			*/
					#endregion

				} // End loop thru items


				
				if (phooo)
				{
					if (grpNodes.Count > 0)
					{
						baseNodes.Add(groupNode);
					}
					else
					{
						// Group is empty!  Should we add it anyway?
						if (!chkKeyword.Checked && !chkRegKeywd.Checked && !chkRGBKeywd.Checked)
						{
							// User asked for groups but not regulars or RGBs
							baseNodes.Add(groupNode);
						}
						if (chkKeyword.Checked &&
							!chkRegKeywd.Checked &&
							!chkRegNonKeywd.Checked &&
							!chkRGBKeywd.Checked &&
							!chkRGBNonKeywd.Checked)
						{
							// User asked for groups but not regulars or RGBs, key matching or not
							baseNodes.Add(groupNode);
						}
						// otherwise, user aked for regular channels and/or RGBs, but none of the children matched
						//   either because of keyword or channel type
						//     So DON'T add it

					}
				}
			}
			return groupNode;
		} // end AddGroup

		private TreeNode FilteredAddDevice(Sequence4 seq, TreeNodeCollection baseNodes, int deviceSI, List<TreeNode>[] siNodes, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			if (deviceSI >= siNodes.Length)
			{
				Array.Resize(ref siNodes, deviceSI + 1);
			}

			TreeNode deviceNode = null;
			if (siNodes[deviceSI] != null)
			{
				//ChanInfo nodeTag = new ChanInfo();
				//nodeTag.MemberType = MemberType.ChannelGroup;
				//nodeTag.objIndex = groupIndex;
				//nodeTag.SavedIndex = theGroup.SavedIndex;
				//nodeTag.nodeIndex = nodeIndex;

				//ChannelGroup theGroup = seq.ChannelGroups[groupIndex];
				CosmicDevice theDevice = (CosmicDevice)seq.Members.bySavedIndex[deviceSI];

				//IMember groupID = theGroup;

				// Include groups in the TreeView?
				if ((memberTypes & SeqEnums.MEMBER_CosmicDevice) > 0)
				{
					string nodeText = theDevice.Name;
					deviceNode = baseNodes.Add(nodeText);

					nodeIndex++;
					deviceNode.Tag = theDevice;
					deviceNode.ImageKey = utils.ICONcosmicDevice;
					deviceNode.SelectedImageKey = utils.ICONcosmicDevice;
					deviceNode.Checked = theDevice.Selected;
					baseNodes = deviceNode.Nodes;
					siNodes[deviceSI].Add(deviceNode);
				}
				//List<TreeNode> qlist;

				// const string ERRproc = " in TreeFillChannels-AddGroup(";
				// const string ERRgrp = "), in Group #";
				// const string ERRitem = ", Items #";
				// const string ERRline = ", Line #";

				for (int gi = 0; gi < theDevice.Members.Count; gi++)
				{
					//try
					//{
					IMember member = theDevice.Members.Items[gi];
					int si = member.SavedIndex;
					if (member.MemberType == MemberType.ChannelGroup)
					{
						ChannelGroup memGrp = (ChannelGroup)member;
						bool inclChan = false;
						if ((memberTypes & SeqEnums.MEMBER_Channel) > 0) inclChan = true;
						bool inclRGB = false;
						if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0) inclRGB = true;
						int inclCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
						if (inclCount > 0)
						{
							if (FilterNode(member))
							{
								TreeNode subGroupNode = FilteredAddGroup(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren, memberTypes);
								//qlist = siNodes[si];
								//qlist.Add(subGroupNode);
							}
							}
						int cosCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
					}
					if (member.MemberType == MemberType.CosmicDevice)
					{
						CosmicDevice memDev = (CosmicDevice)member;
						bool inclChan = false;
						if ((memberTypes & SeqEnums.MEMBER_Channel) > 0) inclChan = true;
						bool inclRGB = false;
						if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0) inclRGB = true;
						int inclCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
						if (inclCount > 0)
						{
							if (FilterNode(member))
							{
								TreeNode subGroupNode = FilteredAddGroup(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren, memberTypes);
								//qlist = siNodes[si];
								//qlist.Add(subGroupNode);
							}
							}
						int cosCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
					}
					if (member.MemberType == MemberType.Channel)
					{
						if ((memberTypes & SeqEnums.MEMBER_Channel) > 0)
						{
							if (FilterNode(member))
							{
								TreeNode channelNode = FilteredAddChannel(seq, baseNodes, member.SavedIndex, selectedOnly);
								siNodes[si].Add(channelNode);
							}
						}
					}
					if (member.MemberType == MemberType.RGBchannel)
					{
						if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0)
						{
							if (FilterNode(member))
							{
								TreeNode rgbChannelNode = FilteredAddRGBchannel(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren);
								siNodes[si].Add(rgbChannelNode);
							}
						}
					}
					#region catch
					/*
		} // end try
			catch (Exception ex)
			{
				StackTrace st = new StackTrace(ex, true);
				StackFrame sf = st.GetFrame(st.FrameCount - 1);
				string emsg = ex.ToString();
				emsg += ERRproc + seq.filename + ERRgrp + groupIndex.ToString() + ERRitem + gi.ToString();
				emsg += ERRline + sf.GetFileLineNumber();
				#if DEBUG
					Debugger.Break();
				#endif
				utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
			} // end catch
			*/
					#endregion

				} // End loop thru items
			}
			return deviceNode;
		} // end AddGroup

		private TreeNode FilteredAddChannel(Sequence4 seq, TreeNodeCollection baseNodes, int channelSI, bool selectedOnly)
		{
			Channel theChannel = (Channel)seq.Members.bySavedIndex[channelSI];
			string nodeText = theChannel.Name;
			TreeNode channelNode = baseNodes.Add(nodeText);
			//IMember nodeTag = theChannel;
			nodeIndex++;
			channelNode.Tag = theChannel;
			//channelNode.ImageIndex = imlTreeIcons.Images.IndexOfKey("Channel");
			//channelNode.SelectedImageIndex = imlTreeIcons.Images.IndexOfKey("Channel");
			//channelNode.ImageKey = utils.ICONchannel;
			//channelNode.SelectedImageKey = utils.ICONchannel;


			ImageList icons = imlTreeIcons; //baseNodes[0].TreeView.ImageList;
			int iconIndex = utils.ColorIcon(icons, theChannel.color);
			channelNode.ImageIndex = iconIndex;
			channelNode.SelectedImageIndex = iconIndex;
			channelNode.Checked = theChannel.Selected;


			return channelNode;
		}

		private TreeNode FilteredAddRGBchannel(Sequence4 seq, TreeNodeCollection baseNodes, int RGBsi, List<TreeNode>[] siNodes, bool selectedOnly, bool includeRGBchildren)
		{
			TreeNode RGBNode = null;
			// Default rgbNodes is basenodes, but normally gets
			// overridden if the parent RGB node is to be included
			TreeNodeCollection rgbNodes = baseNodes;
			TreeNode channelNode = null;
			Channel chan = null;
			string nodeText = "";
			bool phrooo = false;

			if (RGBsi >= siNodes.Length)
			{
				Array.Resize(ref siNodes, RGBsi + 1);
			}
			if (siNodes[RGBsi] != null)
			{
				RGBchannel theRGB = (RGBchannel)seq.Members.bySavedIndex[RGBsi];
				if (FilterNode(theRGB))
				{
					phrooo = true;
					nodeText = theRGB.Name;
					//RGBNode = baseNodes.Add(nodeText);
					RGBNode = new TreeNode(nodeText);

					nodeIndex++;
					RGBNode.Tag = theRGB;
					RGBNode.ImageKey = utils.ICONrgbChannel;
					RGBNode.SelectedImageKey = utils.ICONrgbChannel;
					RGBNode.Checked = theRGB.Selected;
					rgbNodes = RGBNode.Nodes;
					siNodes[RGBsi].Add(RGBNode);
				}

				if (includeRGBchildren)
				{
					// * * R E D   S U B  C H A N N E L * *
					TreeNode colorNode = null;
					chan = theRGB.redChannel;
					if (FilterNode(chan))
					{
						int ci = theRGB.redChannel.SavedIndex;
						nodeText = theRGB.redChannel.Name;
						colorNode = rgbNodes.Add(nodeText);
						//nodeTag = seq.Channels[ci];
						nodeIndex++;
						colorNode.Tag = theRGB.redChannel;
						colorNode.ImageKey = utils.ICONredChannel;
						colorNode.SelectedImageKey = utils.ICONredChannel;
						colorNode.Checked = theRGB.redChannel.Selected;
						siNodes[ci].Add(colorNode);
						//channelNode.Nodes.Add(colorNode);
					}

					// * * G R E E N   S U B  C H A N N E L * *
					chan = theRGB.grnChannel;
					if (FilterNode(chan))
					{
						int ci = theRGB.grnChannel.SavedIndex;
						nodeText = theRGB.grnChannel.Name;
						colorNode = rgbNodes.Add(nodeText);
						//nodeTag = seq.Channels[ci];
						nodeIndex++;
						colorNode.Tag = theRGB.grnChannel;
						colorNode.ImageKey = utils.ICONgrnChannel;
						colorNode.SelectedImageKey = utils.ICONgrnChannel;
						colorNode.Checked = theRGB.grnChannel.Selected;
						siNodes[ci].Add(colorNode);
						//channelNode.Nodes.Add(colorNode);
					}

					// * * B L U E   S U B  C H A N N E L * *
					chan = theRGB.bluChannel;
					if (FilterNode(chan))
					{
						int ci = theRGB.bluChannel.SavedIndex;
						nodeText = theRGB.bluChannel.Name;
						colorNode = rgbNodes.Add(nodeText);
						//nodeTag = seq.Channels[ci];
						nodeIndex++;
						colorNode.Tag = seq.Channels[ci];
						colorNode.ImageKey = utils.ICONbluChannel;
						colorNode.SelectedImageKey = utils.ICONbluChannel;
						colorNode.Checked = theRGB.bluChannel.Selected;
						siNodes[ci].Add(colorNode);
						//channelNode.Nodes.Add(colorNode);
					}
				} // end includeRGBchildren
				if (phrooo)
				{
					if (rgbNodes.Count > 0)
					{
						baseNodes.Add(RGBNode);
					}
					else
					{
						// Why did we make the parent RGB node, yet fail to add any of it's R, G, or B child nodes
						// Reason determines whether or not we should add the parent RGB
						if (!chkKeyword.Checked && !chkRegKeywd.Checked)
						{
							// User asked for RGBs but not regulars
							baseNodes.Add(RGBNode);
						}
						if (chkKeyword.Checked && !chkRegKeywd.Checked && !chkRegNonKeywd.Checked)
						{
							// User asked for RGBs but not regulars, key matching or not
							baseNodes.Add(RGBNode);
						}
						// otherwise, user aked for regular channels, but none of these 3 matched
						//   either because of keyword or channel type
						//     So DON'T add it
					}
				}


			}
			return channelNode;
		}

		public int FilteredDescendantCount(Membership members, bool selectedOnly, bool inclChan, bool inclRGB, bool inclRGBchildren)
		{
			int c = 0;
			for (int l = 0; l < members.Items.Count; l++)
			{
				MemberType t = members.Items[l].MemberType;
				if (t == MemberType.Channel)
				{
					if (inclChan)
					{
						if (members.Items[l].Selected || !selectedOnly) c++;
					}
				}
				if (t == MemberType.RGBchannel)
				{
					if (inclRGB)
					{
						if (members.Items[l].Selected || !selectedOnly) c++;
					}
					if (inclRGBchildren)
					{
						RGBchannel rgbCh = (RGBchannel)members.Items[l];
						if (rgbCh.redChannel.Selected || !selectedOnly) c++;
						if (rgbCh.grnChannel.Selected || !selectedOnly) c++;
						if (rgbCh.bluChannel.Selected || !selectedOnly) c++;
					}
				}
				if (t == MemberType.ChannelGroup)
				{
					ChannelGroup grp = (ChannelGroup)members.Items[l];
					// Recurse!!
					c += FilteredDescendantCount(grp.Members, selectedOnly, inclChan, inclRGB, inclRGBchildren);
				}
				if (t == MemberType.CosmicDevice)
				{
					CosmicDevice dev = (CosmicDevice)members.Items[l];
					// Recurse!!
					c += FilteredDescendantCount(dev.Members, selectedOnly, inclChan, inclRGB, inclRGBchildren);
				}
			}


			return c;

		}


		public bool FilterNode(IMember member)

		{
			bool trakOK = false;
			bool ctrlOK = false;
			bool nameOK = false;
			if (member.MemberType == MemberType.Track)
			{
				if (cboTracks.SelectedIndex == 0)
				{
					nameOK = true;
				}
				else
				{
					if (member.Name.CompareTo(cboTracks.Text) == 0)
					{
						nameOK = true;
					}
				}
			}
			else
			{
				// If no filtering on the name
				if (!chkKeyword.Checked)
				{
					// still need to filter by type
					if (chkRegKeywd.Checked && (member.MemberType == MemberType.Channel)) nameOK = true;
					if (chkRegKeywd.Checked && (member.MemberType == MemberType.VizChannel)) nameOK = true;
					if (chkRGBKeywd.Checked && (member.MemberType == MemberType.RGBchannel)) nameOK = true;
					if (chkGrpKeywd.Checked && (member.MemberType == MemberType.ChannelGroup)) nameOK = true;
				}
				else
				{
					// if name filtering is on
					int pos = utils.FastIndexOf(member.Name, txtKeyword.Text);
					bool pz = (pos >= 0);
					if (member.MemberType == MemberType.Channel)
					{
						// if channel, check name
						if (chkRegKeywd.Checked && pz)
						{
							nameOK = true;
						}
						if (chkRegNonKeywd.Checked && !pz)
						{
							nameOK = true;
						}
						// Even if name is OK, is the type OK?
						Channel ch = (Channel)member;
						if (!chkLOR.Checked && (ch.output.deviceType == DeviceType.LOR)) nameOK = false;
						if (!chkDMX.Checked && (ch.output.deviceType == DeviceType.DMX)) nameOK = false;
						if (!chkDigital.Checked && (ch.output.deviceType == DeviceType.Digital)) nameOK = false;
						if (!chkNoController.Checked && (ch.output.deviceType == DeviceType.None)) nameOK = false;
					}

					if (member.MemberType == MemberType.VizChannel)
					{
						// if channel, check name
						if (chkRegKeywd.Checked && pz)
						{
							nameOK = true;
						}
						if (chkRegNonKeywd.Checked && !pz)
						{
							nameOK = true;
						}
						// Even if name is OK, is the type OK?
						VizChannel vc = (VizChannel)member;
						if (!chkLOR.Checked && (vc.output.deviceType == DeviceType.LOR)) nameOK = false;
						if (!chkDMX.Checked && (vc.output.deviceType == DeviceType.DMX)) nameOK = false;
						if (!chkDigital.Checked && (vc.output.deviceType == DeviceType.Digital)) nameOK = false;
						if (!chkNoController.Checked && (vc.output.deviceType == DeviceType.None)) nameOK = false;
					}

					if (member.MemberType == MemberType.RGBchannel)
					{
						if (chkRGBKeywd.Checked && pz)
						{
							nameOK = true;
						}
						if (chkRGBNonKeywd.Checked && !pz)
						{
							nameOK = true;
						}
					}

					if (member.MemberType == MemberType.ChannelGroup)
					{
						if (chkGrpKeywd.Checked && pz)
						{
							nameOK = true;
						}
						if (chkGrpNonKeywd.Checked && !pz)
						{
							nameOK = true;
						}
					}
				}
				if (member.MemberType == MemberType.Channel)
				{
				}

				}
				return nameOK;
		}



		#endregion

		private void ParameterChange(object sender, EventArgs e)
		{
			if (!workingMyAssOff)
			{
				FilteredFillChannels(treeSource, theSequence, sourceNodesSI, false, true);
			}
		}

		private void KeywordChanged(object sender, EventArgs e)
		{
			if (!workingMyAssOff)
			{
				txtKeyword.Enabled = chkKeyword.Checked;
				picSearch.Enabled = chkKeyword.Checked;
				//chkRegKeywd.Visible = chkKeyword.Checked;
				//chkRGBKeywd.Visible = chkKeyword.Checked;
				//chkGrpKeywd.Visible = chkKeyword.Checked;
				chkRegNonKeywd.Visible = chkKeyword.Checked;
				chkRGBNonKeywd.Visible = chkKeyword.Checked;
				chkGrpNonKeywd.Visible = chkKeyword.Checked;
				if (chkKeyword.Checked)
				{
					chkRegKeywd.Text = REGCHAN + CHWITH + txtKeyword.Text + utils.ENDQT;
					chkRegNonKeywd.Text = REGCHAN + CHWITHOUT + txtKeyword.Text + utils.ENDQT;
					chkRGBKeywd.Text = RGBCHAN + CHWITH + txtKeyword.Text + utils.ENDQT;
					chkRGBNonKeywd.Text = RGBCHAN + CHWITHOUT + txtKeyword.Text + utils.ENDQT;
					chkGrpKeywd.Text = CHANGRP + CHWITH + txtKeyword.Text + utils.ENDQT;
					chkGrpNonKeywd.Text = CHANGRP + CHWITHOUT + txtKeyword.Text + utils.ENDQT;
					chkRGBKeywd.Top = 108;
					chkGrpKeywd.Top = 154;
				}
				else
				{
					chkRegKeywd.Text = REGCHAN;
					chkRGBKeywd.Text = RGBCHAN;
					chkGrpKeywd.Text = CHANGRP;
					chkRGBKeywd.Top = 85;
					chkGrpKeywd.Top = 108;

				}

				ParameterChange(sender, e);
			}
		}
	} // end class frmList
} // end namespace MapORama
