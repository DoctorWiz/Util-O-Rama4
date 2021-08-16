using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using LORUtils; using FileHelper;
using FuzzyString;
using Syncfusion.Windows.Forms.Tools;

namespace UtilORama4
{
	public partial class frmRemapper : Form
	{
		#region constants
		private const int LIST_OLD = 1;
		private const int LIST_NEW = 2;
		private const string CRLF = "\r\n";
		private const string helpPage = "http://wizlights.com/utilorama/maporama";
		private const int BATCHnone = 0;
		private const int BATCHmaster = 1;
		private const int BATCHmap = 2;
		private const int BATCHsources = 4;
		private const int BATCHall = 7;
		private const string STATUSautoMap = "AutoMap \"";
		private const string STATUSto = "\" to...";
		private const string MSG_MapRegularToRegular = "Regular Channels can only be mapped to other regular Channels.";
		private const string MSG_MapRGBtoRGB = "RGB Channels can only be mapped to other RGB Channels.";
		private const string MSG_GroupToGroup = "Groups can only be mapped to other groups, and only if they have the same number of regular Channels and RGB Channels in the same order.";
		private const string MSG_GroupMatch = "Groups can only be mapped to other groups if they have the same number of regular Channels, RGB Channels, and subgroups in the same order.";
		private const string MSG_Tracks = "Tracks can not be mapped.";
		private const string MSG_OnlyOneOldToNew = "The Selected source channel already has a template channel mapped to it.";
		private const string MSG_OnlyOneGroupToNew = "The Selected source group already has a template group mapped to it.";
		private const double MINSCORE = 0.85D; // For Fuzzy Find
		private const double MINMATCH = 0.92D;
		//! Important: All 4 BackColors need to be different
		private readonly Color COLOR_FR_UnselUnhigh = Color.FromName("Black");
		private readonly Color COLOR_BK_UnselUnhigh = Color.FromName("White");
		private readonly Color COLOR_FR_SelUnhigh = Color.FromName("White");
		private readonly Color COLOR_BK_SelUnhigh = Color.FromName("DarkBlue");
		private readonly Color COLOR_FR_UnselHigh = Color.FromName("White");
		private readonly Color COLOR_BK_UnselHigh = Color.FromName("Purple");
		private readonly Color COLOR_FR_SelHigh = Color.FromName("Yellow");
		private readonly Color COLOR_BK_SelHigh = Color.FromName("DarkViolet");  // Note, 'Dark Violet' is actually lighter than 'Purple'
		private const string xmlInfo = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"no\"?>";
		private const string TABLEchMap = "channelMap";
		private const string TABLEfiles = "files";
		private const string FIELDmasterFile = "destinationFile";
		private const string FIELDsourceFile = "sourceFile";
		private const string TABLEchannels = "channels";
		private const string FIELDmasterChannel = "destinationChannel";
		private const string FIELDsourceChannel = "sourceChannel";
		private const string TABLEmappings = "mappings";
		private const string applicationName = "Map-O-Rama";
		#endregion

		#region Form Level Globals
		private bool dirtyMap = false;
		private bool dirtyDest = false;
		private string thisEXE = "map-o-rama.exe";

		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\Util-O-Rama\\Split-O-Rama\\
		private string[] commandArgs;
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private int batch_fileNumber = 0;
		private string[] batch_fileList = null;
		private string cmdSelectionsFile = "";
		private int batchTypes = BATCHnone;
		private bool processDrop = false;
		private bool sourceOnRight = false;
		public int pp = 0;
		public bool bulkOp = false;
		private int beatTracks = 0;
		private int masterMapLevel = 0;
		private int sourceMapLevel = 0;
		
		// These are used by MapList so need to be public
		public Sequence4 seqSource = new Sequence4();
		public Sequence4 seqMaster = new Sequence4();
		public IMember[] mapMastToSrc = null; // Array indexed by Master.SavedIndex, elements contain Source Members
		public List<IMember>[] mapSrcToMast = null; // Array indexed by Source.SavedIindex, elements are Lists of Master Members
		public int mappedMemberCount = 0;
		public int mappedChannelCount = 0;
		public int[] mappedSIs; // Note: Redundant!  Done only for debugging purposes.
		public List<TreeNodeAdv>[] sourceNodesBySI = null; // new List<TreeNodeAdv>();
		public List<TreeNodeAdv>[] masterNodesBySI = null; // new List<TreeNodeAdv>();
		public string masterFile = "";
		public string sourceFile = "";
		public string mapFile = "";
		private int mapFileLineCount = 0;
		private string saveFile = "";

		private int lastHighlightedMaster = utils.UNDEFINED;
		private int lastHighlightedSource = utils.UNDEFINED;
		private int lastSelectedMaster = utils.UNDEFINED;
		private int lastSelectedSource = utils.UNDEFINED;
		private bool lastSelectionWasMaster = false;
		private bool lastSelectionWasSource = false;

		private string basePath = "";
		private string SeqFolder = "";
		private TreeNodeAdv selectedSourceNode = null;
		private TreeNodeAdv selectedMasterNode = null;
		private IMember selectedSourceMember = null;
		private IMember selectedMasterMember = null;
		//private int sourceSI = utils.UNDEFINED;
		//private int masterSI = utils.UNDEFINED;
		// int activeList = 0;
		public string statMsg = "Hello World!";
		//private Assembly assy = this.GetType().Assembly;
		//ResourceManager resources = new ResourceManager("Resources.Strings", assy);

		// Used only at load time to restore previous size/position
		//private int miLoadHeight = 400;
		//private int miLoadWidth = 620;
		//private int miLoadLeft = 0;
		//private int miLoadTop = 0;

		/*
	private class ChanInfo
	{
		public MemberType chType = MemberType.None;
		public int chIndex = 0;
		public int SavedIndex = utils.UNDEFINED;
		public int mapCount = 0;
		//public int[] mapChIndexes;
		//public int[] mapSavedIndexes;
		public int nodeIndex = utils.UNDEFINED;
	}
	*/

		private int nodeIndex = utils.UNDEFINED;
		// Note Master->Source mappings are a 1:Many relationship.
		// A Master channel can map to only one Source channel
		// But a Source channel may map to more than one Master channel

		private StreamWriter logWriter1 = null;
		private StreamWriter logWriter2 = null;
		private StreamWriter logWriter3 = null;
		private string logFile1 = "";
		private string logFile2 = "";
		private string logFile3 = "";
		private bool log1Open = false;
		private bool log2Open = false;
		private bool log3Open = false;
		private string logHomeDir = ""; // Gets set during InitForm to "C:\\Users\\Wizard\\AppData\\Local\\UtilORama\\MapORama\\" (sustitute your name)
		private string logMsg = "";


		#endregion

		private struct match
		{
			public double score;
			public int savedIdx;
			public int itemIdx;
			public MemberType MemberType;
		}

		#region File Operations
		private void BrowseSourceFile()
		{
			string initDir = utils.DefaultSequencesPath;
			string initFile = "";
			if (sourceFile.Length > 4)
			{
				string ldir = Path.GetDirectoryName(sourceFile);
				if (Directory.Exists(ldir))
				{
					initDir = ldir;
					if (File.Exists(sourceFile))
					{
						initFile = Path.GetFileName(sourceFile);
					}
				}
			}


			dlgOpenFile.Filter = utils.FILT_OPEN_ANY;
			dlgOpenFile.DefaultExt = utils.EXT_LMS;
			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			dlgOpenFile.Title = "Open Sequence...";
			DialogResult result = dlgOpenFile.ShowDialog();

			pnlAll.Enabled = false;
			if (result == DialogResult.OK)
			{
				ImBusy(true);
				int err = LoadSourceFile(dlgOpenFile.FileName);
				ImBusy(false);
				AskToMap();
			} // end if (result = DialogResult.OK)
			pnlAll.Enabled = true;
			btnSummary.Enabled = (mappedMemberCount > 0);
			mnuSummary.Enabled = btnSummary.Enabled;

		}

		public int LoadSourceFile(string sourceChannelFile)
		{
			ImBusy(true);
			string beatsName = "";
			int err = seqSource.ReadSequenceFile(sourceChannelFile);
			if (err < 100)
			{
				// Search for any sort of Beats, Song Parts, Tune-O-Rama, or Vamperizer Track
				// Default = not found
				beatTracks = 0;
				// Loop thru tracks checking names
				for (int t = 0; t < seqSource.Tracks.Count; t++)
				{
					string tn = seqSource.Tracks[t].Name.ToLower();
					if ((tn.IndexOf("beat") >= 0) ||
						(tn.IndexOf("song parts") >= 0) ||
						(tn.IndexOf("information") >= 0) ||
						(tn.IndexOf("o-rama") >= 0) ||
						(tn.IndexOf("vamperizer") >= 0))
					{ 
						beatTracks++;
						beatsName += seqSource.Tracks[t].Name + "\n\r";
					}
				}
				// Enable or Disable the Copy Beats Checkbox according to if found or not
				if (beatTracks == 0)
				{
					chkCopyBeats.Text = "Copy Beat Track(s)";
					chkCopyBeats.Enabled = false;
					chkCopyBeats.Checked = false;
				}
				else
				{ 
					string txt = "Copy Beat Track(s): " + beatsName;
					chkCopyBeats.Text = txt;
					chkCopyBeats.Enabled = true;
					chkCopyBeats.Checked = true;
				}


				sourceFile = sourceChannelFile;
				txtSourceFile.Text = Fyle.ShortenLongPath(sourceFile, 80);
				this.Text = "Map-O-Rama - " + Path.GetFileName(sourceFile);
				FileInfo fi = new FileInfo(sourceChannelFile);
				Properties.Settings.Default.BasePath = fi.DirectoryName;
				Properties.Settings.Default.LastSourceFile = sourceFile;
				Properties.Settings.Default.Save();
				TreeUtils.TreeFillChannels(treeSource, seqSource, ref sourceNodesBySI, false, false);
				// Erase any existing mappings, and create a new blank one of proper size
				mapMastToSrc = null;
				mappedSIs = null;
				mapSrcToMast = null;
				mappedMemberCount = 0;
				//Array.Resize(ref mapSrcToMast, seqSource.Members.allCount);
				Array.Resize(ref mapSrcToMast, seqSource.Members.Items.Count);
				for (int i = 0; i < mapSrcToMast.Length; i++)
				{
					mapSrcToMast[i] = new List<IMember>();
				}
				if (seqMaster.Channels.Count > 0)
				{
					//Array.Resize(ref mapMastToSrc, seqMaster.Members.allCount);
					Array.Resize(ref mapMastToSrc, seqMaster.Members.Items.Count);
					Array.Resize(ref mappedSIs, seqMaster.Members.Items.Count);
					for (int i = 0; i < mapMastToSrc.Length; i++)
					{
						mapMastToSrc[i] = null;
						mappedSIs[i] = utils.UNDEFINED;
					}

				}
			}
			ImBusy(false);
			return err;
		}

		private void BrowseMasterFile()
		{

			string initDir = utils.DefaultSequencesPath;
			string initFile = "";
			if (masterFile.Length > 4)
			{
				string ldir = Path.GetDirectoryName(masterFile);
				if (Directory.Exists(ldir))
				{
					initDir = ldir;
					if (File.Exists(masterFile))
					{
						initFile = Path.GetFileName(masterFile);
					}
				}
			}


			dlgOpenFile.Filter = utils.FILT_OPEN_CFG;
			dlgOpenFile.DefaultExt = utils.EXT_LCC;
			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			dlgOpenFile.Title = "Select Destination Sequence File...";
			DialogResult result = dlgOpenFile.ShowDialog();

			//pnlAll.Enabled = false;
			if (result == DialogResult.OK)
			{
				masterFile = dlgOpenFile.FileName;

				int err = LoadMasterFile(dlgOpenFile.FileName);
				if (err == 0)
				{
					Properties.Settings.Default.LastMasterFile = masterFile;
					Properties.Settings.Default.Save();
					Properties.Settings.Default.Upgrade();
					Properties.Settings.Default.Save();
				}

			} // end if (result = DialogResult.OK)
				//pnlAll.Enabled = true;
			if (treeSource.Nodes.Count > 0)
			{
				//btnSummary.Enabled = true;
			}

		}

		private int LoadMasterFile(string masterChannelFile)
		{
			ImBusy(true);
			int err = seqMaster.ReadSequenceFile(masterChannelFile);
			if (err < 100)  // Error numbers below 100 are warnings, above 100 are fatal
			{
				for (int w = 0; w < seqMaster.Members.byName.Count; w++)
				{
					//Console.WriteLine(seqMaster.Members.byName[w].Name);
				}





				masterFile = masterChannelFile;
				FileInfo fi = new FileInfo(masterFile);
				Properties.Settings.Default.LastMasterFile = masterFile;
				Properties.Settings.Default.Save();

				txtMasterFile.Text = Fyle.ShortenLongPath(masterFile, 80);



				TreeUtils.TreeFillChannels(treeMaster, seqMaster, ref masterNodesBySI, false, false);

				// Erase any existing mappings, and create a new blank one of proper size
				// Erase any existing mappings, and create a new blank one of proper size
				mapMastToSrc = null;
				mappedSIs = null;
				mapSrcToMast = null;
				mappedMemberCount = 0;
				if (seqSource.Channels.Count > 0)
				{
					Array.Resize(ref mapSrcToMast, seqSource.Members.allCount);
					for (int i = 0; i < mapSrcToMast.Length; i++)
					{
						mapSrcToMast[i] = new List<IMember>();
					}
				}
				Array.Resize(ref mapMastToSrc, seqMaster.Members.allCount);
				Array.Resize(ref mappedSIs, seqMaster.Members.allCount);
				for (int i = 0; i < mapMastToSrc.Length; i++)
				{
					mapMastToSrc[i] = null;
					mappedSIs[i] = utils.UNDEFINED;
				}
				string txt = "AutoLaunch " + txtMasterFile.Text;
				chkAutoLaunch.Text = txt;
				chkAutoLaunch.Enabled = true;
				AskToMap();

			}
			else
			{
				chkAutoLaunch.Text = "AutoLaunch";
				chkAutoLaunch.Checked = false;
				chkAutoLaunch.Enabled = false;
			}


			ImBusy(false);
			return err;
		}

		public int SaveNewMappedSequence(string newSeqFileName)
		{
			ImBusy(true);
			Sequence4 seqNew = seqMaster;

			seqNew.info = seqSource.info;
			seqNew.info.filename = newSeqFileName;
			seqNew.SequenceType = seqSource.SequenceType;
			seqNew.Centiseconds = seqSource.Centiseconds;
			seqNew.animation = seqSource.animation;
			seqNew.videoUsage = seqSource.videoUsage;

			string msg = "GLB";
			msg += LeftBushesChildCount().ToString();
			lblDebug.Text = msg;

			seqNew.ClearAllEffects();

			//seqNew.TimingGrids = new List<TimingGrid>();
			foreach (TimingGrid sourceGrid in seqSource.TimingGrids)
			{
				TimingGrid newGrid = null;
				for (int gridIndex = 0; gridIndex < seqNew.TimingGrids.Count; gridIndex++)
				{
					if (sourceGrid.Name.ToLower() == seqNew.TimingGrids[gridIndex].Name.ToLower())
					{
						newGrid = seqNew.TimingGrids[gridIndex];
						gridIndex = seqNew.TimingGrids.Count; // Force exit of loop
					}
				}
				if (newGrid == null)
				{
					newGrid = seqNew.ParseTimingGrid(sourceGrid.LineOut());
				}
				newGrid.CopyTimings(sourceGrid.timings, false);
			}


			// Copy beats enabled and checked?
			if (chkCopyBeats.Enabled)
			{
				if (chkCopyBeats.Checked)
				{
					// Is it the FIRST track?
					if (beatTracks > 0)
					{
						CopyBeats(seqNew);
					}
				}
			}


			for (int mapLoop = 0; mapLoop < mapMastToSrc.Length; mapLoop++)
			{
				if (mapMastToSrc[mapLoop] != null)
				{
					int newSI = mapLoop;
					IMember newChild = seqNew.Members.bySavedIndex[newSI];
					if (newChild.MemberType == MemberType.Channel)
					{
						//int sourceSI = mapMastToSrc[mapLoop];
						//IMember sourceChildMember = seqSource.Members.bySavedIndex[sourceSI];
						IMember sourceChildMember = mapMastToSrc[mapLoop];
						if (sourceChildMember.MemberType == MemberType.Channel)
						{
							Channel sourceChannel = (Channel)sourceChildMember;
							if (sourceChannel.effects.Count > 0)
							{
								Channel newChannel = (Channel)newChild;
								newChannel.CopyEffects(sourceChannel.effects, false);
								newChannel.Centiseconds = sourceChannel.Centiseconds;
							} // end if effects.Count
						} // end if source obj is channel
					}
					else // newer obj is NOT a channel
					{
						if (newChild.MemberType == MemberType.RGBchannel)
						{
							//int sourceSI = mapMastToSrc[mapLoop];
							//IMember sourceChildMember = seqSource.Members.bySavedIndex[sourceSI];
							IMember sourceChildMember = mapMastToSrc[mapLoop];
							if (sourceChildMember.MemberType == MemberType.RGBchannel)
							{
								RGBchannel sourceRGBchannel = (RGBchannel)sourceChildMember;
								RGBchannel masterRGBchannel = (RGBchannel)newChild;
								masterRGBchannel.Centiseconds = sourceRGBchannel.Centiseconds;
								Channel sourceChannel = sourceRGBchannel.redChannel;
								Channel newChannel;
								if (sourceChannel.effects.Count > 0)
								{
									newChannel = masterRGBchannel.redChannel;
									newChannel.CopyEffects(sourceChannel.effects, false);
								} // end if effects.Count
								sourceChannel = sourceRGBchannel.grnChannel;
								if (sourceChannel.effects.Count > 0)
								{
									newChannel = masterRGBchannel.grnChannel;
									newChannel.CopyEffects(sourceChannel.effects, false);
								} // end if effects.Count
								sourceChannel = sourceRGBchannel.bluChannel;
								if (sourceChannel.effects.Count > 0)
								{
									newChannel = masterRGBchannel.bluChannel;
									newChannel.CopyEffects(sourceChannel.effects, false);
								} // end if effects.Count

							} // end if source obj is RGB channel
						}
						else // newer obj is NOT an RGBchannel
						{
							// Channel Group = do nothing (?)
							// Track = do nothing (?)
							// Timing Grid = do nothing (?)
						} // end if newer obj is an RGBchannel (or not)
					} // end if newer obj is channel (or not)
				} // end if mapping is undefined
			} // loop thru newer Channels

			// Copy beats enabled and checked?
			if (chkCopyBeats.Enabled)
			{
				if (chkCopyBeats.Checked)
				{
					// Is it the NOT the FIRST track (at the end maybe?)
					if (beatTracks > 0)
					{
						CopyBeats(seqNew);
					}
				}
			}

			foreach (Channel ch in seqMaster.Channels)
			{
				ch.Centiseconds = seqSource.Centiseconds;
			}
			foreach (RGBchannel rc in seqMaster.RGBchannels)
			{
				rc.Centiseconds = seqSource.Centiseconds;
			}
			foreach (ChannelGroup cg in seqMaster.ChannelGroups)
			{
				cg.Centiseconds = seqSource.Centiseconds;
			}
			foreach (Track tr in seqMaster.Tracks)
			{
				tr.Centiseconds = seqSource.Centiseconds;
			}


			//if (chkCopyBeats.Checked)
			//{
			//	CopyBeats(seqNew);
			//}

			msg = "GLB";
			msg += LeftBushesChildCount().ToString();
			lblDebug.Text = msg;




			int err = seqNew.WriteSequenceFile_DisplayOrder(newSeqFileName);
			seqNew.ClearAllEffects();

			saveFile = newSeqFileName;
			Properties.Settings.Default.LastSaveFile = saveFile;
			Properties.Settings.Default.Save();

			ImBusy(false);
			return err;
		}

		private void SaveMap()
		{
			dlgSaveFile.DefaultExt = utils.EXT_CHMAP;
			dlgSaveFile.Filter = utils.FILE_CHMAP;
			dlgSaveFile.FilterIndex = 0;
			string initDir = utils.DefaultChannelConfigsPath;
			string initFile = "";
			if (mapFile.Length > 4)
			{
				string pth = Path.GetFullPath(mapFile);
				if (Directory.Exists(pth))
				{
					initDir = pth;
				}
				if (File.Exists(mapFile))
				{
					initFile = Path.GetFileName(mapFile);
				}
			}
			dlgSaveFile.FileName = initFile;
			dlgSaveFile.InitialDirectory = initDir;
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.Title = "Save Channel Map As...";

			DialogResult dr = dlgSaveFile.ShowDialog();
			if (dr == DialogResult.OK)
			{

				string mapTemp = System.IO.Path.GetTempPath();
				mapTemp += Path.GetFileName(dlgSaveFile.FileName);
				int mapErr = SaveMap(mapTemp);
				if (mapErr == 0)
				{
					mapFile = dlgSaveFile.FileName;
					if (File.Exists(mapFile))
					{
						//TODO: Add Exception Catch
						File.Delete(mapFile);
					}
					File.Copy(mapTemp, mapFile);
					File.Delete(mapTemp);
					dirtyMap = false;
					//btnSaveMap.Enabled = dirtyMap;
					txtMappingFile.Text = Path.GetFileName(mapFile);
					Properties.Settings.Default.LastMapFile = mapFile;
					Properties.Settings.Default.Save();
				} // end no errors saving map
			} // end dialog result = OK
		} // end btnSaveMap Click event

		private void BrowseForMap()
		{
			dlgOpenFile.DefaultExt = utils.EXT_CHMAP;
			dlgOpenFile.Filter = utils.FILE_CHMAP;
			dlgOpenFile.FilterIndex = 0;
			string initDir = utils.DefaultChannelConfigsPath;
			string initFile = "";
			if (mapFile.Length > 4)
			{
				string pth = Path.GetDirectoryName(mapFile);
				if (Directory.Exists(pth))
				{
					initDir = pth;
				}
				if (File.Exists(mapFile))
				{
					initFile = Path.GetFileName(mapFile);
				}
			}
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Title = "Load-Apply Channel Map..";

			DialogResult dr = dlgOpenFile.ShowDialog();
			if (dr == DialogResult.OK)
			{
				mapFile = dlgOpenFile.FileName;
				txtMappingFile.Text = Path.GetFileName(mapFile);
				Properties.Settings.Default.LastMapFile = mapFile;
				Properties.Settings.Default.Save();
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.Save();
				if (seqMaster.Members.Items.Count > 1)
				{
					if (seqSource.Members.Items.Count > 1)
					{
						LoadApplyMapFile(mapFile);
					}
				}
				dirtyMap = false;
				btnSaveMap.Enabled = dirtyMap;
				mnuSaveNewMap.Enabled = dirtyMap;
			} // end dialog result = OK
		} // end btnLoadMap Click event

		private int SaveMap(string fileName)
		{
			int ret = 0;
			string nowtime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
			int lineCount = 0;
			StreamWriter writer = new StreamWriter(fileName);
			string lineOut = ""; // line to be written out, gets modified if necessary
													 //int pos1 = utils.UNDEFINED; // positions of certain key text in the line

			lineOut = xmlInfo;
			writer.WriteLine(lineOut);
			lineOut = utils.STTBL + TABLEchMap;
			lineOut += Info.FIELDsaveFileVersion + utils.FIELDEQ + seqMaster.info.saveFileVersion + utils.ENDQT;
			lineOut += Info.FIELDcreatedAt + utils.FIELDEQ + nowtime + utils.ENDQT + utils.ENDTBL;
			writer.WriteLine(lineOut);

			lineOut = utils.LEVEL1 + utils.STTBL + TABLEfiles + utils.ENDTBL;
			writer.WriteLine(lineOut);
			lineOut = utils.LEVEL2 + utils.STFLD + FIELDmasterFile;
			lineOut += utils.FIELDname + utils.FIELDEQ + seqMaster.info.filename + utils.ENDQT + utils.ENDFLD;
			writer.WriteLine(lineOut);
			lineOut = utils.LEVEL2 + utils.STFLD + FIELDsourceFile;
			lineOut += utils.FIELDname + utils.FIELDEQ + seqSource.info.filename + utils.ENDQT + utils.ENDFLD;
			writer.WriteLine(lineOut);
			lineOut = utils.LEVEL1 + utils.FINTBL + TABLEfiles + utils.ENDTBL;
			writer.WriteLine(lineOut);

			lineOut = utils.LEVEL1 + utils.STTBL + TABLEmappings + utils.ENDTBL;
			writer.WriteLine(lineOut);

			for (int i = 0; i < mapMastToSrc.Length; i++)
			{
				if (mapMastToSrc[i] != null)
				{
					IMember masterMember = seqMaster.Members.bySavedIndex[i];
					IMember newSourceMember = mapMastToSrc[i];

					lineOut = utils.LEVEL2 + utils.STTBL + TABLEchannels + utils.ENDTBL;
					writer.WriteLine(lineOut);

					lineOut = utils.LEVEL3 + utils.STTBL + FIELDmasterChannel;
					lineOut += utils.FIELDname + utils.FIELDEQ + utils.XMLifyName(masterMember.Name) + utils.ENDQT;
					lineOut += utils.FIELDtype + utils.FIELDEQ + SeqEnums.MemberName(masterMember.MemberType) + utils.ENDQT;
					lineOut += utils.FIELDsavedIndex + utils.FIELDEQ + masterMember.SavedIndex + utils.ENDQT + utils.ENDFLD;
					writer.WriteLine(lineOut);

					lineOut = utils.LEVEL3 + utils.STTBL + FIELDsourceChannel;
					lineOut += utils.FIELDname + utils.FIELDEQ + utils.XMLifyName(newSourceMember.Name) + utils.ENDQT;
					lineOut += utils.FIELDtype + utils.FIELDEQ + SeqEnums.MemberName(newSourceMember.MemberType) + utils.ENDQT;
					lineOut += utils.FIELDsavedIndex + utils.FIELDEQ + newSourceMember.SavedIndex + utils.ENDQT + utils.ENDFLD;
					writer.WriteLine(lineOut);

					lineOut = utils.LEVEL2 + utils.FINTBL + TABLEchannels + utils.ENDTBL;
					writer.WriteLine(lineOut);
				}
			}
			lineOut = utils.LEVEL1 + utils.FINTBL + TABLEmappings + utils.ENDTBL;
			writer.WriteLine(lineOut);

			lineOut = utils.FINTBL + TABLEchMap + utils.ENDTBL;
			writer.WriteLine(lineOut);

			writer.Close();
			return ret;
		} // end SaveMap

		private string LoadApplyMapFile(string fileName)
		{
			ImBusy(true);
			//int w = pnlStatus.Width;
			//pnlProgress.Width = w;
			//pnlProgress.Size = new Size(w, pnlProgress.Height);
			//pnlStatus.Visible = false;
			//pnlProgress.Visible = true;
			//staStatus.Refresh();
			//int mq = seqMaster.Tracks.Count + seqMaster.ChannelGroups.Count + seqMaster.Channels.Count;
			//mq -= seqMaster.RGBchannels.Count * 2;
			//int sq = seqSource.Tracks.Count + seqSource.ChannelGroups.Count + seqSource.Channels.Count;
			//sq -= seqSource.RGBchannels.Count * 2;
			//int qq = mq + sq;
			//pnlProgress.Maximum = qq + 1;
			//int pp = 0;
			int lineCount = 0;
			int lineNum = 0;
			string errMsgs = "";
			string lineIn;
			string[] mapData;
			string[] sides;
			string sourceName = "";
			string masterName = "";
			string temp = "";
			//int tempNum;
			int[] foundChannels;
			int sourceSI = utils.UNDEFINED;
			int masterSI = utils.UNDEFINED;
			MemberType sourceType = MemberType.None;
			MemberType masterType = MemberType.None;
			IMember masterMember = null;
			IMember newSourceMember = null;
			IMember foundID = null;
			string mfile = "";
			string sfile = "";
			long finalAlgorithms = Properties.Settings.Default.FuzzyFinalAlgorithms;
			double minPreMatch = Properties.Settings.Default.FuzzyMinPrematch;
			double minFinalMatch = Properties.Settings.Default.FuzzyMinFinal;
			long preAlgorithm = Properties.Settings.Default.FuzzyPrematchAlgorithm;
			logFile1 = logHomeDir + "Apply" + batch_fileNumber.ToString() + ".log";
			logWriter1 = new StreamWriter(logFile1);
			log1Open = true;
			logMsg = "Destination File: " + seqMaster.info.filename;
			logWriter1.WriteLine(logMsg);
			logMsg = "Source File: " + seqSource.info.filename;
			logWriter1.WriteLine(logMsg);
			logMsg = "Map File: " + fileName;
			logWriter1.WriteLine(logMsg);
			mappedMemberCount = 0;
			int li = 0;
			int errorStatus = 0;

			string msg = "GLB";
			msg += LeftBushesChildCount().ToString();
			lblDebug.Text = msg;

			if (batchMode)
			{
				ShowProgressBars(2);
			}
			else
			{
				ShowProgressBars(1);
			}
			ClearAllMappings();




			////////////////////////////////////////////////////////////////////////////////////////////////////////////
			// First Pass, just make sure it's valid and get line count so we can update the progress bar accordingly
			//////////////////////////////////////////////////////////////////////////////////////////////////////////
			#region First Pass
			StreamReader reader = new StreamReader(fileName);
			if (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();

				// Sanity Check #2, is it an XML file?
				li = lineIn.Substring(0, 6).CompareTo("<?xml ");
				if (li != 0)
				{
					errorStatus = 101;
				}
				else
				{
					//xmlInfo = lineIn;
					// Sanity Check #1B, does it have at least 2 lines?
					if (!reader.EndOfStream)
					{
						lineIn = reader.ReadLine();
						// Sanity Check #3, is it a sequence?
						//li = lineIn.IndexOf(TABLEchMap);
						li = utils.FastIndexOf(lineIn, TABLEchMap);
						if (li != 1)
						{
							errorStatus = 102;
						}
						else
						{
							int sfv = utils.getKeyValue(lineIn, Info.FIELDsaveFileVersion);
							// Sanity Checks #4A and 4B, does it have a 'SaveFileVersion' and is it '14'
							//   (SaveFileVersion="14" means it cane from LOR Sequence Editor ver 4.x)
							if (sfv != 14)
							{
								errorStatus = 114;
							}
							else
							{
								lineCount = 2;
								while ((lineIn = reader.ReadLine()) != null)
								{
									lineCount++;
								} // end while lines remain, counting them
							} // saveVersion = 14
						} // Its a channelMap
					} // It has at least 2 lines
				} //its in XML format
			} // it has at least 1 line
			reader.Close();
			#endregion // First Pass
			// end of first pass




			if ((errorStatus == 0) && (lineCount > 12))
			{
				#region Second Pass
				///////////////////////////////////////////////
				// Second Pass, look for EXACT matches only
				/////////////////////////////////////////////
				// All sanity checks passed

				//pnlProgress.Maximum = lineCount;
				prgBarInner.Maximum = lineCount * 10;
				if (batchMode)
				{
					prgBarOuter.Maximum = lineCount * 10 * batch_fileCount;
				}

				// Updating and repainting the status takes time, so set an update frequence resulting in no more that 200 updates
				int updFreq = lineCount / 200;

				reader = new StreamReader(fileName);
				// Read in and then ignore the first 8 lines
				//   xml, channelMap, files, mappings...
				for (int n = 0; n < 7; n++)
				{
					lineIn = reader.ReadLine();
				}
				lineNum = 7;

				// * PARSE LINES
				while ((lineIn = reader.ReadLine()) != null)
				{
					lineNum++;
					// Line just read should be "    <channels>"
					//li = lineIn.IndexOf(utils.STTBL + TABLEchannels + utils.ENDTBL);
					li = utils.FastIndexOf(lineIn, (utils.STTBL + TABLEchannels + utils.ENDTBL));
					if (li > 0)
					{
						if (!reader.EndOfStream)
						{
							// Next line is the Master Channel
							lineIn = reader.ReadLine();
							lineNum++;
							//li = lineIn.IndexOf(FIELDmasterChannel);
							li = utils.FastIndexOf(lineIn, FIELDmasterChannel);
							if (li > 0)
							{
								masterName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
								masterSI = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
								masterType = SeqEnums.enumMemberType(utils.getKeyWord(lineIn, utils.FIELDtype));
								//lblMessage.Text = "Map \"" + masterName + "\" to...";
								//pnlMessage.Refresh();

								// Next line (let's assume its there) is the Source Channel
								if (!reader.EndOfStream)
								{
									// Next line is the Source Channel
									lineIn = reader.ReadLine();
									lineNum++;
									//li = lineIn.IndexOf(FIELDsourceChannel);
									li = utils.FastIndexOf(lineIn, FIELDsourceChannel);
									if (li > 0)
									{
										sourceName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
										sourceSI = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
										sourceType = SeqEnums.enumMemberType(utils.getKeyWord(lineIn, utils.FIELDtype));

										// is it time to update?
										if ((lineNum % updFreq) == 0)
										{
											if (sourceOnRight)
											{
												lblMessage.Text = masterName + " to " + sourceName;
											}
											else
											{
												lblMessage.Text = sourceName + " to " + masterName;
											}
											prgBarInner.CustomText = lblMessage.Text;
											pnlMessage.Refresh();
										} // end update if needed

										// Try to find the master member by SavedIndex first since that is the easiest.
										// May not be a match, but good chance it is.
										// Is the Saved Index within range?
										if (masterSI <= seqMaster.Members.HighestSavedIndex)
										{
											// In range, so fetch it
											masterMember = seqMaster.Members.bySavedIndex[masterSI];
											// Now compare the name, is it actually the same member?
											// (May not be a match, but good chance it is.)
											if (masterMember.Name.ToLower().CompareTo(masterName.ToLower()) != 0)
											{
												// Nope, name doesn't match, nullify the find (by SavedIndex)
												masterMember = null;
											}
										}
										// Did we get it?
										if (masterMember == null)
										{
											// No, search again by name this time
											masterMember = seqMaster.Members.Find(masterName, masterType, false);
										}
										// Did we get it (by name)?
										if (masterMember != null)
										{
											// Did we find the master?  If not, no sense in searching for the source.
											// Follow same procedure as above to try and find the source
											// (Read comments above)
											if (sourceSI <= seqSource.Members.HighestSavedIndex)
											{
												newSourceMember = seqSource.Members.bySavedIndex[sourceSI];
												if (newSourceMember.Name.ToLower().CompareTo(sourceName.ToLower()) != 0)
												{
													newSourceMember = null;
												}
											}
											if (newSourceMember == null)
											{
												newSourceMember = seqSource.Members.Find(sourceName, sourceType, false);
											}
											if (newSourceMember != null)
											{
												// So did we succeed in finding the source member?
												// Final Sanity Checks
												if (masterMember.MemberType == (newSourceMember.MemberType))
												{
													if (masterMember.MemberType == masterType)
													{
														logMsg = "Exact Matched " + masterName + " to " + sourceName;
														Console.WriteLine(logMsg);
														logWriter1.WriteLine(logMsg);
														//MapMembers(masterSI, sourceSI, true, false);
														//seqMaster.Members.bySavedIndex[masterSI].Selected = true;
														//seqSource.Members.bySavedIndex[sourceSI].Selected = true;
														//MapMembers(masterSI, sourceSI, false);
														int newMaps = MapMembers(masterMember, newSourceMember, true);
														mappedMemberCount += newMaps;
													}
												} // End final sanity checks
											} // End and found source member
										} // End found master member
										else
										{
											logMsg = masterName + " not found in Master.";
											Console.WriteLine(logMsg);
											logWriter1.WriteLine(logMsg);
										}
									} // End got source info from file line
									else
									{
										logMsg = sourceName + " not found in Source.";
										Console.WriteLine(logMsg);
										logWriter1.WriteLine(logMsg);
									}
								} // End not end of stream
							} // End got master info from file line
							prgBarInner.Value = lineNum;
							//staStatus.Refresh();
							prgBarInner.Refresh();
							if (batchMode)
							{
								int v = batch_fileNumber * lineCount * 10;
								v += lineNum;
								prgBarOuter.Value = v;
								prgBarOuter.Refresh();
							}
							// After every 256 lines, perform a DoEvents
							// (Can change to 128 with 0x7F or every 64 with 0x3F, etc.)
							int lcr = lineNum & 0xFF;
							if (lcr == 0xFF)
							{
								Application.DoEvents();
							}
						} // End not end of stream
					} // End line not null
				} // End got channels XML table header
				reader.Close();
				#endregion // Second Pass




				////////////////////////////////////////////////////////////////////
				// Third pass, attempt to fuzzy find anything not already matched
				//////////////////////////////////////////////////////////////////
				#region Third Pass
				// Don't think should do fuzzy find on reading a map file
				if (true == false)
				{
					reader = new StreamReader(fileName);
					// Read in and then ignore the first 8 lines
					//   xml, channelMap, files, mappings...
					for (int n = 0; n < 7; n++)
					{
						lineIn = reader.ReadLine();
					}
					lineNum = 7;

					// * PARSE LINES
					while ((lineIn = reader.ReadLine()) != null)
					{
						lineNum++;
						// Line just read should be "    <channels>"
						//li = lineIn.IndexOf(utils.STTBL + TABLEchannels + utils.ENDTBL);
						li = utils.FastIndexOf(lineIn, utils.STTBL + TABLEchannels + utils.ENDTBL);
						if (li > 0)
						{
							if (!reader.EndOfStream)
							{
								// Next line is the Master Channel
								lineIn = reader.ReadLine();
								lineNum++;
								//li = lineIn.IndexOf(FIELDmasterChannel);
								li = utils.FastIndexOf(lineIn, FIELDmasterChannel);
								if (li > 0)
								{
									masterName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
									//lblMessage.Text = "Map \"" + masterName + "\" to...";
									//pnlMessage.Refresh();
									masterSI = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
									masterType = SeqEnums.enumMemberType(utils.getKeyWord(lineIn, utils.FIELDtype));
									// Next line (let's assume its there) is the Source Channel
									if (!reader.EndOfStream)
									{
										// Next line is the Source Channel
										lineIn = reader.ReadLine();
										lineNum++;
										//li = lineIn.IndexOf(FIELDsourceChannel);
										li = utils.FastIndexOf(lineIn, FIELDsourceChannel);
										if (li > 0)
										{
											sourceName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
											if (sourceOnRight)
											{
												lblMessage.Text = masterName + " to " + sourceName;
											}
											else
											{
												lblMessage.Text = sourceName + " to " + masterName;
											}
											prgBarInner.CustomText = lblMessage.Text;
											pnlMessage.Refresh();
											sourceSI = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
											sourceType = SeqEnums.enumMemberType(utils.getKeyWord(lineIn, utils.FIELDtype));

											if (!seqMaster.Members.bySavedIndex[masterSI].Selected)
											{
												if (seqMaster.Members.bySavedIndex[masterSI].MemberType == masterType)
												{
													// Search for it again by name, this time use fuzzy find
													logMsg = "Fuzzy searching Destination for ";
													logMsg += sourceName;
													Console.WriteLine(logMsg);
													logWriter1.WriteLine(logMsg);

													foundID = FindByName(masterName, seqMaster.Members, masterType, preAlgorithm, minPreMatch, finalAlgorithms, minFinalMatch, true);
													if (foundID != null)
													{
														masterSI = foundID.SavedIndex;
														masterName = foundID.Name;
													}

													// Got it yet?
													if (masterSI > utils.UNDEFINED)
													{
														if (sourceSI < seqSource.Members.bySavedIndex.Count)
														{
															if (seqSource.Members.bySavedIndex[sourceSI].MemberType == sourceType)
															{
																if (!seqSource.Members.bySavedIndex[sourceSI].Selected)
																{
																	//try to find it again by name and this time use fuzzy matching
																	logMsg = "Fuzzy searching Source for ";
																	logMsg += sourceName;
																	Console.WriteLine(logMsg);
																	logWriter1.WriteLine(logMsg);
																	foundID = FindByName(sourceName, seqSource.Members, sourceType, preAlgorithm, minPreMatch, finalAlgorithms, minFinalMatch, true);
																	if (foundID != null)
																	{
																		sourceSI = foundID.SavedIndex;
																	}
																} // if not Selected

																// Got it yet?
																if (sourceSI > utils.UNDEFINED)
																{
																	if (seqSource.Members.bySavedIndex[sourceSI].MemberType ==
																			seqMaster.Members.bySavedIndex[masterSI].MemberType)
																	{
																		logMsg = "Fuzzy Matched " + masterName + " to " + sourceName;
																		Console.WriteLine(logMsg);
																		logWriter1.WriteLine(logMsg);
																		int newMaps = MapMembers(masterSI, sourceSI, true, false);
																		mappedMemberCount += newMaps;
																		seqMaster.Members.bySavedIndex[masterSI].Selected = true;
																		seqSource.Members.bySavedIndex[sourceSI].Selected = true;
																	}
																}
															} // if source type matchies
														} // if source SavedIndex in range
													} // if source section line
												} // if lines remain
											} // if got a smaster channel
										} // if master type matches
									} // if master SI not Selected
								} // if line is a master channel
							} // if lines left
						} // if a channel section
							//pnlProgress.Value = lineNum;
						prgBarInner.Value = lineCount + lineNum * 9;
						//staStatus.Refresh();
						prgBarInner.Refresh();
						if (batchMode)
						{
							int v = batch_fileNumber * lineCount * 10;
							v += (lineCount + lineNum * 9);
							prgBarOuter.Value = v;
							prgBarOuter.Refresh();
						}
						// After every 256 lines, perform a DoEvents
						// (Can change to 128 with 0x7F or every 64 with 0x3F, etc.)
						int lcr = lineNum & 0xFF;
						if (lcr == 0xFF)
						{
							Application.DoEvents();
						}

					} // if lines remain
					reader.Close();
				} // End False=True
				#endregion
				// end third pass
			} // End no errors, and correct # of lines

			//pnlProgress.Visible = false;
			//pnlStatus.Visible = true;

			if (!batchMode)
			{
				ShowProgressBars(0);
				ImBusy(false);
			}

			logWriter1.Close();
			UpdateMappedCount(0);
			

			return errMsgs;
		} // end LoadApplyMapFile

		private void SaveNewMappedSequence()
		{
			ImBusy(true);
			string xt = Path.GetExtension(sourceFile).ToLower();
			dlgSaveFile.DefaultExt = xt;
			dlgSaveFile.Filter = utils.FILT_SAVE_EITHER;
			dlgSaveFile.FilterIndex = 0;
			if (xt.CompareTo(utils.EXT_LAS) == 0) dlgSaveFile.FilterIndex = 1;
			string initDir = SeqFolder;
			string initFile = "";
			if (sourceFile.Length > 4)
			{
				if (saveFile.Length > 3)
				{
					string pth = Path.GetFullPath(saveFile);
					if (Directory.Exists(pth))
					{
						initDir = pth;
					}
				}
				if (File.Exists(sourceFile))
				{
					initFile = Path.GetFileName(sourceFile);
				}
			}
			string newName = SuggestedNewName(initFile);
			dlgSaveFile.FileName = newName;
			dlgSaveFile.InitialDirectory = initDir;
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.Title = "Save New Sequence As...";

			DialogResult dr = dlgSaveFile.ShowDialog();
			if (dr == DialogResult.OK)
			{

				string saveTemp = System.IO.Path.GetTempPath();
				saveTemp += Path.GetFileName(dlgSaveFile.FileName);

				string msg = "GLB";
				msg += LeftBushesChildCount().ToString();
				lblDebug.Text = msg;

				int mapErr = SaveNewMappedSequence(saveTemp);
				if (mapErr == 0)
				{
					saveFile = dlgSaveFile.FileName;
					if (File.Exists(saveFile))
					{
						//TODO: Add Exception Catch
						File.Delete(saveFile);
					}
					File.Copy(saveTemp, saveFile);
					File.Delete(saveTemp);

					if (chkAutoLaunch.Checked)
					{
						System.Diagnostics.Process.Start(saveFile);

					}
					//dirtyMap = false;
					//btnSaveMap.Enabled = dirtyMap;
					//txtMappingFile.Text = Path.GetFileName(mapFile);
				} // end no errors saving map
			} // end dialog result = OK
			ImBusy(false);
		}

		#endregion

		#region Node Operations
		private TreeNodeAdv NodeFind(TreeNodeAdvCollection treeNodes, string tag)
		{
			int tl = tag.Length;
			string st = "";
			TreeNodeAdv ret = new TreeNodeAdv("NOT FOUND");
			bool found = false;
			foreach (TreeNodeAdv nOde in treeNodes)
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
							ret = NodeFind(nOde.Nodes, tag);
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

		private void NodesUnselectAll(TreeNodeAdvCollection treeNodes)
		{
			foreach (TreeNodeAdv nOde in treeNodes)
			{
				// Reset any previous selections
				NodeHighlight(nOde, false);
				if (nOde.Nodes.Count > 0)
				{
					NodesUnselectAll(nOde.Nodes);
				}
			} // end foreach
		}

		private void SourceNode_Click(TreeNodeAdv newSourceNode)
		{
			if (newSourceNode != null)
			{
				if (newSourceNode.Tag != null)
				{
					IMember sourceMember = (IMember)newSourceNode.Tag;
					if (sourceMember.SavedIndex != lastSelectedSource)
					{
						SelectSourceMember(sourceMember);

						if (sourceMember.MemberType == MemberType.Channel)
						{
							Channel ch = (Channel)sourceMember;
							Bitmap bmp = utils.RenderEffects(ch, 0, ch.Centiseconds, 300, 20, true);
							picPreviewSource.Visible = true;
							picPreviewSource.Image = bmp;
							//picPreview.Refresh();
						}
						if (sourceMember.MemberType == MemberType.RGBchannel)
						{
							RGBchannel rgb = (RGBchannel)sourceMember;
							Bitmap bmp = utils.RenderEffects(rgb, 0, seqSource.Centiseconds, 300, 21, false);
							picPreviewSource.Image = bmp;
							picPreviewSource.Visible = true;
						}
						if ((sourceMember.MemberType == MemberType.ChannelGroup) ||
								(sourceMember.MemberType == MemberType.Track) ||
								(sourceMember.MemberType == MemberType.CosmicDevice))
						{
							picPreviewSource.Visible = false;
						}
						lastSelectedSource = sourceMember.SavedIndex;
						lastSelectionWasSource = true;
						lastSelectionWasMaster = false;
					}
				}
				else
				{
					picPreviewSource.Visible = false;
					lastSelectedSource = utils.UNDEFINED;
					lastSelectionWasSource = false;
				}
			}
			else
			{
				picPreviewSource.Visible = false;
				lastSelectedSource = utils.UNDEFINED;
				lastSelectionWasSource = false;
			}

			selectedSourceNode = newSourceNode;
		}

		private void NodeMaster_Click(TreeNodeAdv newMasterNode)
		{
			Color overBackColor = SystemColors.Control;

			if (newMasterNode != null)
			{
				if (newMasterNode.Tag != null)
				{
					IMember masterMember = (IMember)newMasterNode.Tag;
					if (masterMember.SavedIndex != lastSelectedMaster)
					{
						MemberSelectMaster(masterMember);

						if (masterMember.MemberType == MemberType.Channel)
						{
							Channel ch = (Channel)masterMember;
							Bitmap bmp = utils.RenderEffects(ch, 0, ch.Centiseconds, 300, 20, true);
							picPreviewMaster.Visible = true;
							pnlOverwrite.Visible = true;
							picPreviewMaster.Image = bmp;
							//picPreview.Refresh();
							//if (mapMastToSrc[masterMember.SavedIndex] != null)
							//{
							if (ch.effects.Count > 0)
							{
								overBackColor = Color.Red;
							}
							//}

						}
						if (masterMember.MemberType == MemberType.RGBchannel)
						{
							RGBchannel rgb = (RGBchannel)masterMember;
							Bitmap bmp = utils.RenderEffects(rgb, 0, seqMaster.Centiseconds, 300, 21, false);
							picPreviewMaster.Image = bmp;
							picPreviewMaster.Visible = true;
							pnlOverwrite.Visible = true;
							if (mapMastToSrc[masterMember.SavedIndex] != null)
							{
								if ((rgb.redChannel.effects.Count > 0) ||
										(rgb.grnChannel.effects.Count > 0) ||
										(rgb.bluChannel.effects.Count > 0))
								{
									overBackColor = Color.Red;
								}
							}
						}
						if ((masterMember.MemberType == MemberType.ChannelGroup) ||
								(masterMember.MemberType == MemberType.Track) ||
								(masterMember.MemberType == MemberType.CosmicDevice))
						{
							picPreviewMaster.Visible = false;
							pnlOverwrite.Visible = false;
						}
						lastSelectedMaster = masterMember.SavedIndex;
						lastSelectionWasMaster = true;
						lastSelectionWasSource = false;
					}
				}
				else
				{
					picPreviewMaster.Visible = false;
					pnlOverwrite.Visible = false;
					lastSelectedMaster = utils.UNDEFINED;
					lastSelectionWasMaster = false;
				}
			}
			else
			{
				picPreviewMaster.Visible = false;
				pnlOverwrite.Visible = false;
				lastSelectedMaster = utils.UNDEFINED;
				lastSelectionWasMaster = false;
			}
			pnlOverwrite.BackColor = overBackColor;
			pnlMapWarn.BackColor = overBackColor;
			selectedMasterNode = newMasterNode;

		}

		private void NodesMapSelected()
		{
			string tipText = "";
			// The button should not have even been enabled if the 2 Channels are not eligible to be mapped ... but--
			// Verify it anyway!

			// Is a node Selected on each side?
			if (selectedSourceNode != null)
			{
				if (selectedMasterNode != null)
				{
					// Types match?
					masterMapLevel = 0; // Reset
					sourceMapLevel = 0;
					if (selectedMasterMember.MemberType == MemberType.Channel)
					{
						if (selectedSourceMember.MemberType == MemberType.Channel)
						{
							int newMaps = MapMembers(selectedMasterMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = false; // because already mapped
							tipText = selectedSourceMember.Name + " is now mapped to " + selectedMasterMember.Name;
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = true;
							tipText = "Unmap " + selectedSourceMember.Name + " from " + selectedMasterMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_MapRegularToRegular;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are not the same type";
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is not mapped to " + selectedMasterMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == MemberType.RGBchannel)
					{
						if (selectedMasterMember.MemberType == MemberType.RGBchannel)
						{
							int newMaps = MapMembers(selectedMasterMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = false; // because already mapped
							tipText = selectedSourceMember.Name + " is now mapped to " + selectedMasterMember.Name;
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = true;
							tipText = "Unmap " + selectedSourceMember.Name + " from " + selectedMasterMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_MapRGBtoRGB;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is not mapped to " + selectedMasterMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == MemberType.ChannelGroup)
					{
						if (selectedMasterMember.MemberType == MemberType.ChannelGroup)
						{
							int newMaps = MapMembers(selectedMasterMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = false; // because already mapped
							tipText = selectedSourceMember.Name + " is now mapped to " + selectedMasterMember.Name;
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = true;
							tipText = "Unmap " + selectedSourceMember.Name + " from " + selectedMasterMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_GroupToGroup;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is not mapped to " + selectedMasterMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == MemberType.Track)
					{
						statMsg = Resources.MSG_Tracks;
						StatusMessage(statMsg);
						System.Media.SystemSounds.Beep.Play();
						btnMap.Enabled = false; // tracks not mapable at this time
						tipText = "Tracks cannot be mapped";
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = false;
						tipText = selectedSourceMember.Name + " is not mapped to " + selectedMasterMember.Name + "\r\n(And Tracks cannot be mapped)";
						ttip.SetToolTip(btnUnmap, tipText);
					}
					if (selectedMasterMember.MemberType == MemberType.Track)
					{
						statMsg = Resources.MSG_Tracks;
						StatusMessage(statMsg);
						System.Media.SystemSounds.Beep.Play();
						btnMap.Enabled = false; // Tracks not mappable at this time
						tipText = "Tracks cannot be mapped";
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = false;
						tipText = selectedSourceMember.Name + " is not mapped to " + selectedMasterMember.Name + "\r\n(And Tracks cannot be mapped)";
						ttip.SetToolTip(btnUnmap, tipText);
					}
					UpdateMappedCount(0);
				}
				else
				{
					System.Media.SystemSounds.Beep.Play();
					btnMap.Enabled = false;
					tipText = "No destination selected";
					ttip.SetToolTip(btnMap, tipText);
					btnUnmap.Enabled = false;
					ttip.SetToolTip(btnUnmap, tipText);
				}
			}
			else
			{
				System.Media.SystemSounds.Beep.Play();
				btnMap.Enabled = false;
				tipText = "No source is selected";
				ttip.SetToolTip(btnMap, tipText);
				btnUnmap.Enabled = false;
				ttip.SetToolTip(btnUnmap, tipText);
			}
			// Copy enabled state from buttons to menus
			mnuMap.Enabled = btnMap.Enabled;
			mnuUnmap.Enabled = btnUnmap.Enabled;
			mnuSaveNewMap.Enabled = btnSaveMap.Enabled;


		} // end btnMap_Click

		/*
		private void NodeHighlight(TreeNodeAdv nOde, bool highlight)
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
		*/

		private void NodeHighlight(TreeNodeAdv nOde, bool highlight)
		{
			bool wasSelected = false;
			//string bs = nOde.BaseStyle;
			Color backColor = nOde.Background.BackColor;
			if ((backColor == COLOR_BK_SelUnhigh) || (backColor == COLOR_BK_SelHigh))
			//if ((bs.CompareTo("Sh") == 0) || (bs.CompareTo("SH") == 0))
			//f (bs.Contains("S"))
							{
				wasSelected = true;
			}

			if (highlight)
			{
				if (wasSelected)
				{
					nOde.TextColor = COLOR_FR_SelHigh;
					//nOde.CheckColor = COLOR_BK_SelHigh;
					//nOde.BaseStyle = "SH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelHigh);
				}
				else
				{
					nOde.TextColor = COLOR_FR_UnselHigh;
					//nOde.CheckColor = COLOR_BK_UnselHigh;
					//nOde.BaseStyle = "sH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselHigh);
				}
			}
			else
			{
				if (wasSelected)
				{
					nOde.TextColor = COLOR_FR_SelUnhigh;
					//nOde.CheckColor = COLOR_BK_SelUnhigh;
					//nOde.BaseStyle = "Sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelUnhigh);
				}
				else
				{
					nOde.TextColor = COLOR_FR_UnselUnhigh;
					//nOde.CheckColor = COLOR_BK_UnselUnhigh;
					//nOde.BaseStyle = "sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselUnhigh);
				}
			}
		}

		private void NodeSelect(TreeNodeAdv nOde, bool select)
		{
			bool wasHighlighted = false;
			//string bs = nOde.BaseStyle;
			Color backColor = nOde.Background.BackColor;
			if ((backColor == COLOR_BK_SelHigh) || (backColor == COLOR_BK_UnselHigh))
			//if ((bs.CompareTo("SH")==0)||(bs.CompareTo("sH")==0))
			//if (bs.Contains("H"))
			{
				wasHighlighted = true;
			}

			if (select)
			{
				if (wasHighlighted)
				{
					nOde.TextColor = COLOR_FR_SelHigh;
					//nOde.CheckColor = COLOR_BK_SelHigh;
					//nOde.BaseStyle = "SH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelHigh);
				}
				else
				{
					nOde.TextColor = COLOR_FR_SelUnhigh;
					//nOde.CheckColor = COLOR_BK_SelUnhigh;
					//nOde.BaseStyle = "Sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelUnhigh);
				}
			}
			else
			{
				if (wasHighlighted)
				{
					nOde.TextColor = COLOR_FR_UnselHigh;
					//nOde.CheckColor = COLOR_BK_UnselHigh;
					//nOde.BaseStyle = "sH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselHigh);
				}
				else
				{
					nOde.TextColor = COLOR_FR_UnselUnhigh;
					//nOde.CheckColor = COLOR_BK_UnselUnhigh;
					//nOde.BaseStyle = "sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselUnhigh);
				}
			}
		}

		/*
		private void NodesSelect(TreeNodeCollection nOdes, int SavedIndex, bool select)
		{
			//string nTag = "";
			IMember nID = null;
			foreach (TreeNodeAdv nOde in nOdes)
			{
				nID = (IMember)nOde.Tag;
				if (nID.SavedIndex == SavedIndex)
				{
					NodeSelect(nOde, select);
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					NodesSelect(nOde.Nodes, SavedIndex, select);
				}
			}
			//nOdes[0].EnsureVisible();
		}
		*/

		private void NodesBold(List<TreeNodeAdv> nodeList, bool isMapped)
		{
			if (nodeList.Count < 1)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				for (int i = 0; i < nodeList.Count; i++)
				{
					TreeNodeAdv nOde = nodeList[i];
					if (isMapped)
					{
						nOde.Font = new Font(treeSource.Font, FontStyle.Bold);
						nOde.Checked = true;
					}
					else
					{
						nOde.Font = new Font(treeSource.Font, FontStyle.Regular);
						nOde.Checked = false;
					}
				}
			}
		}

		private void NodesHighlight(List<TreeNodeAdv> nodeList, bool highlight)
		{
			if (nodeList.Count < 1)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				for (int i=0; i<nodeList.Count; i++)
				{
					NodeHighlight(nodeList[i], highlight);
				}
			}
		}

		private void NodesClearFormatting(TreeNodeCollection nodez, bool clearHighlight, bool clearSelect)
		{
			bool selected = false;
			bool highlighted = false;

			foreach(TreeNodeAdv nOde in nodez)
			{
				//	Color back = nOde.CheckColor;
				//string bs = nOde.BaseStyle;
				Color backColor = nOde.Background.BackColor;
				if ((backColor == COLOR_BK_SelHigh) || (backColor==COLOR_BK_SelUnhigh))
				//if ((bs.CompareTo("SH") == 0) || (bs.CompareTo("Sh") == 0))
				//if (bs.Contains("S"))
				{
					if (!clearSelect)
					{
						selected = true;
					}
				}
				if ((backColor == COLOR_BK_SelHigh) || (backColor == COLOR_BK_UnselHigh))
				//if ((bs.CompareTo("SH") == 0) || (bs.CompareTo("sH") == 0))
				//if (bs.Contains("H")) 
					{
						if (!clearHighlight)
					{
						highlighted = true;
					}
				}

				if (selected && highlighted)
				{
					nOde.TextColor = COLOR_FR_SelHigh;
					//nOde.CheckColor = COLOR_BK_SelHigh;
					//nOde.BaseStyle = "SH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelHigh);
				}
				if (!selected && highlighted)
				{
					nOde.TextColor = COLOR_FR_UnselHigh;
					//nOde.CheckColor = COLOR_BK_UnselHigh;
					//nOde.BaseStyle = "sH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselHigh);
				}
				if (selected && !highlighted)
				{
					nOde.TextColor = COLOR_FR_SelUnhigh;
					//nOde.CheckColor = COLOR_BK_SelUnhigh;
					//nOde.BaseStyle = "Sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelUnhigh);
				}
				if (!selected && !highlighted)
				{
					nOde.TextColor = COLOR_FR_UnselUnhigh;
					//nOde.CheckColor = COLOR_BK_UnselUnhigh;
					//nOde.BaseStyle = "sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselUnhigh);
				}
			}
		}

		private void NodesUnhighlightAllMaster()
		{
			bool wasSelected = false;
			foreach (TreeNodeAdv nOde in treeMaster.Nodes)
			{
				//string bs = nOde.BaseStyle;
				Color backColor = nOde.Background.BackColor;
				if ((backColor == COLOR_BK_SelUnhigh) || (backColor == COLOR_BK_SelHigh))
				//if ((bs.CompareTo("Sh") == 0) || (bs.CompareTo("SH") == 0))
				//if (bs.Contains("S"))
				{
						wasSelected = true;
				}
				if (wasSelected)
				{
					nOde.TextColor = COLOR_FR_SelUnhigh;
					//nOde.CheckColor = COLOR_BK_SelUnhigh;
					//nOde.BaseStyle = "Sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelUnhigh);
				}
				else
				{
					nOde.TextColor = COLOR_FR_UnselUnhigh;
					//nOde.CheckColor = COLOR_BK_UnselUnhigh;
					//nOde.BaseStyle = "sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselUnhigh);
				}
			}
		}

		private void NodesSelect(List<TreeNodeAdv> nodeList, bool select)
		{
			if (nodeList.Count < 1)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				for (int i = 0; i < nodeList.Count; i++)
				{
					NodeSelect(nodeList[i], select);
				}
			}
		}

		private void NodesHighlightMaster(int masterSI, bool highlight)
		{
			List<TreeNodeAdv> nodeList = null;
			if(masterSI != lastHighlightedMaster)
			{
				//if (lastHighlightedMaster >= 0)
				//{
				//	nodeList = masterNodesBySI[lastHighlightedMaster];
				//	NodesHighlight(nodeList, false);
				//}
				if (masterSI >= 0)
				{
					nodeList = masterNodesBySI[masterSI];
					NodesHighlight(nodeList, highlight);
				}
				lastHighlightedMaster = masterSI;
			}
		}

		private void NodesHighlightSource(int sourceSI)
		{
			List<TreeNodeAdv> nodeList = null;
			if (sourceSI != lastHighlightedSource)
			{
				if (lastHighlightedSource >= 0)
				{
					nodeList = sourceNodesBySI[lastHighlightedSource];
					NodesHighlight(nodeList, false);
				}
				if (sourceSI >= 0)
				{
					nodeList = sourceNodesBySI[sourceSI];
					NodesHighlight(nodeList, true);
				}
				lastHighlightedSource = sourceSI;
			}
		}

		private void NodesSelectMaster(int masterSI)
		{
			List<TreeNodeAdv> nodeList = null;
			if (masterSI != lastSelectedMaster)
			{
				if (lastSelectedMaster >= 0)
				{
					nodeList = masterNodesBySI[lastSelectedMaster];
					NodesSelect(nodeList, false);
				}
				if (masterSI >= 0)
				{
					nodeList = masterNodesBySI[masterSI];
					NodesSelect(nodeList, true);
				}
				lastSelectedMaster = masterSI;
			}
		}

		private void NodesSelectSource(int sourceSI)
		{
			List<TreeNodeAdv> nodeList = null;
			if (sourceSI != lastSelectedSource)
			{
				if (lastSelectedSource >= 0)
				{
					nodeList = sourceNodesBySI[lastSelectedSource];
					NodesSelect(nodeList, false);
				}
				if (sourceSI >= 0)
				{
					nodeList = sourceNodesBySI[sourceSI];
					NodesSelect(nodeList, true);
				}
				lastSelectedSource = sourceSI;
			}
		}

		/*
		 * private void NodesUnselectAll(TreeNodeCollection nOdes)
		{
			foreach (TreeNodeAdv nOde in nOdes)
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
					NodesUnselectAll(nOde.Nodes);
				}
			}
		}
		*/
		/*
		private void NodesHighlight(TreeNodeCollection nOdes, int SavedIndex, bool highlight)
		{
			//string nTag = "";
			IMember nID = null;
			foreach (TreeNodeAdv nOde in nOdes)
			{
				nID = (IMember)nOde.Tag;
				if (nID.SavedIndex == SavedIndex)
				{
					NodeHighlight(nOde, highlight);
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					NodesHighlight(nOde.Nodes, SavedIndex, highlight);
				}
			}
		}
		*/
		/*
		 * private void NodesHighlight(List<TreeNodeAdv> nodeList, bool highlight)
		{
			if (nodeList.Count < 1)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				for (int i = 0; i < nodeList.Count; i++)
				{
					NodeHighlight(nodeList[i], highlight);
				}
			}
		}

		

		private void NodesBold(List<TreeNodeAdv> nodeList, bool emBolden)
		{
			//string nTag = "";


			foreach (TreeNodeAdv thenOde in nodeList)
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
		*/

		private void NodesMap(bool foo, TreeNodeAdv theSourceNode, TreeNodeAdv theMasterNode, bool map)
		{
			// Is a node Selected on each side?
			if (theSourceNode != null)
			{
				IMember sid = (IMember)selectedSourceNode.Tag;
				if (selectedMasterNode != null)
				{
					IMember mid = (IMember)selectedMasterNode.Tag;
					int newMaps = MapMembers(mid.SavedIndex, sid.SavedIndex, map, true);
					UpdateMappedCount(newMaps);
				} // end masterNodeannel Node isn't null
			} // end sourceNodeannel Node isn't null
		} // end btnMap_Click




		#endregion

		#region Member Mapping Operations
		bool IsMappable(MemberType sourceType, MemberType masterType)
		{
			bool enM = false;
			// Are they both regular Channels?
			if (sourceType == MemberType.Channel)
			{
				if (masterType == MemberType.Channel)
				{
					enM = true;
				}
				else
				{
					StatusMessage(MSG_MapRegularToRegular);
				}
			} // end old type is regular ch
				// Are they both RGB Channels?
			if (sourceType == MemberType.RGBchannel)
			{
				if (masterType == MemberType.RGBchannel)
				{
					enM = true;
				}
				else
				{
					StatusMessage(MSG_MapRGBtoRGB);
				}
			} // end old type is RGB
				// Are they both groups?
			if (sourceType == MemberType.ChannelGroup)
			{
				if (masterType == MemberType.ChannelGroup)
				{
					// are they similar enough?
					TreeNodeAdv selectedSourceNode = treeSource.SelectedNode;
					IMember selectedSourceMember = (IMember)selectedSourceNode.Tag;
					int sourceSI = selectedSourceMember.SavedIndex;
					TreeNodeAdv selectedMasterNode = treeMaster.SelectedNode;

					IMember selectedMasterMember = (IMember)selectedMasterNode.Tag;
					int masterSI = selectedMasterMember.SavedIndex;
					enM = IsCompatible(sourceSI, masterSI);
					if (!enM)
					{
						StatusMessage(MSG_GroupMatch);
					}
				}
				else
				{
					StatusMessage(MSG_GroupToGroup);
				}
			} // end old type is group
			if (masterType == MemberType.Track)
			{
				StatusMessage(MSG_Tracks);
			}
			if (sourceType == MemberType.Track)
			{
				StatusMessage(MSG_Tracks);
			}
			return enM;
		} // end IsMappable

		bool IsCompatible(int sourceThingSI, int masterThingSI)
		{
			// not necessarily groups, called recursively
			bool ret = true;
			if ((sourceThingSI < 0) || (masterThingSI < 0))
			{
				ret = false;
			}
			else
			{
				MemberType sourceType = seqSource.Members.bySavedIndex[sourceThingSI].MemberType;

				if (sourceType != seqMaster.Members.bySavedIndex[masterThingSI].MemberType)
				{
					ret = false;
				}
				else
				{
					if (sourceType == MemberType.Channel)
					{
						ret = true;
					}
					else
					{
						if (sourceType == MemberType.RGBchannel)
						{
							ret = true;
						}
						else
						{
							if (sourceType == MemberType.Track)
							{
								ret = false;
								Track sourceTrk = (Track)seqSource.Members.bySavedIndex[sourceThingSI];
								Track masterTrk = (Track)seqMaster.Members.bySavedIndex[masterThingSI];
								//if ((sourceTrk.itemSavedIndexes[0] < 0) || (masterTrk.itemSavedIndexes[0] < 0))
								if ((sourceTrk.Members.Items.Count < 1) || (masterTrk.Members.Items.Count < 1))
								{
									ret = false;
								}
								else
								{
									if (sourceTrk.Members.Items.Count != masterTrk.Members.Items.Count)
									{
										ret = false;
									}
									else
									{
										//foreach (int srcSI in sourceTrk.itemSavedIndexes)
										for (int itm = 0; itm < sourceTrk.Members.Items.Count; itm++)
										{
											if (ret)
											{
												ret = IsCompatible(sourceTrk.Members.Items[itm].SavedIndex, masterTrk.Members.Items[itm].SavedIndex);
											} // if last item still OK
										} // end loop thru item saved indexes
									} // end else counts match
								} // end else first elements < 0
							}
							else
							{
								if (sourceType == MemberType.ChannelGroup)
								{
									ChannelGroup sourceGrp = (ChannelGroup)seqSource.Members.bySavedIndex[sourceThingSI]; // seqSource.ChannelGroups[seqSource.savedIndexes[sourceThingSI].objIndex];
									ChannelGroup masterGrp = (ChannelGroup)seqMaster.Members.bySavedIndex[masterThingSI]; // seqMaster.ChannelGroups[seqMaster.savedIndexes[masterThingSI].objIndex];
																																																				//if ((sourceGrp.itemSavedIndexes[0] < 0) || (masterGrp.itemSavedIndexes[0] < 0))
									if ((sourceGrp.Members.Items.Count < 1) || (masterGrp.Members.Items.Count < 1))
									{
										ret = false;
									}
									else
									{
										if (sourceGrp.Members.Items.Count != masterGrp.Members.Items.Count)
										{
											ret = false;
										}
										else
										{
											//foreach (int srcSI in sourceGrp.itemSavedIndexes)
											for (int itm = 0; itm < sourceGrp.Members.Items.Count; itm++)
											{
												if (ret)
												{
													ret = IsCompatible(sourceGrp.Members.Items[itm].SavedIndex, masterGrp.Members.Items[itm].SavedIndex);
												} // if last item still OK
											} // end loop thru item saved indexes
										} // end else counts match
									} // end else first elements < 0
								} // end if group
							} // end if not track
						} // is an RGB Channel
					} // is a channel
				} // end if types match
			} // end SIs aren't undefined
			return ret;
		} // end IsCompatible

		private void SelectSourceMember(IMember sourceMember)
		{
			IMember newSourceMember = sourceMember;
			IMember lastSelectedSourceMember = selectedSourceMember;
			//string mapText = "";

			int masterSI = utils.UNDEFINED;
			bool doSelect = false;
			List<TreeNodeAdv> nodeList = null;
			// Assume (for now) that neither mapping or unmapping is valid
			btnMap.Enabled = false;
			btnUnmap.Enabled = false;
			

			if (newSourceMember.MemberType == MemberType.Track)
			{
				// Unselect any previous selections
				if (lastSelectedSourceMember != null)
				{
					int oldSSI = lastSelectedSourceMember.SavedIndex;
					// Is it different from before?
					if (oldSSI != newSourceMember.SavedIndex)
					{
						// Clear previous selection
						nodeList = sourceNodesBySI[oldSSI];
						NodesSelect(nodeList, false);

						// Was the old one mapped to one or more masters?
						if (mapSrcToMast[lastSelectedSourceMember.SavedIndex].Count > 0)
						{
							// Unhighlight All Previous Master Nodes
							foreach (IMember masterMember in mapSrcToMast[lastSelectedSourceMember.SavedIndex])
							{
								nodeList = masterNodesBySI[masterMember.SavedIndex];
								NodesHighlight(nodeList, false);
							}
						} // End old source mapped to master(s)
					} // End selection changed
				} // End there was a previous selection
				selectedSourceMember = null;
				//mapText = newSourceMember.Name + " is a track.\n\r";
				//mapText += "Tracks cannot be mapped (at this time)";
			}
			else // NOT a Track
			{
				// Was one already selected?
				if (lastSelectedSourceMember != null)
				{
					int oldSSI = lastSelectedSourceMember.SavedIndex;
					// Is it different from before?
					if (oldSSI != newSourceMember.SavedIndex)
					{
						// Clear previous selection
						nodeList = sourceNodesBySI[oldSSI];
						NodesSelect(nodeList, false);

						// Was the old one mapped to one or more masters?
						if (mapSrcToMast[lastSelectedSourceMember.SavedIndex].Count > 0)
						{
							// Unhighlight previously selected & mapped Sources
							nodeList = sourceNodesBySI[oldSSI];
							NodesHighlight(nodeList, false);
							
							// Unhighlight All Previous Master Nodes
							foreach (IMember masterMember in mapSrcToMast[lastSelectedSourceMember.SavedIndex])
							{
								nodeList = masterNodesBySI[masterMember.SavedIndex];
								NodesHighlight(nodeList, false);
							}
						} // End old source mapped to master(s)
					} // End selection changed
				} // End there was a previous selection
				// if no source node was previously selected, then obviously this selection is different (from nothing)
				// Is the new one mapped to one or more masters?
				if (mapSrcToMast[newSourceMember.SavedIndex].Count > 0)
				{
					// highlight all Master Nodes already mapped to this Source node
					foreach (IMember masterMember in mapSrcToMast[newSourceMember.SavedIndex])
					{
						nodeList = masterNodesBySI[masterMember.SavedIndex];
						NodesHighlight(nodeList, true);
						// Build status text
						//mapText += masterMember.Name + ", ";

						// Is a master member selected, and does it happen to be one of the masters mapped to this source?
						if (selectedMasterMember != null)
						{
							if (masterMember.SavedIndex == selectedMasterMember.SavedIndex)
							{
								// Allow it to be unmapped
								btnUnmap.Enabled = true;
								string tipText = "Unmap " + sourceMember.Name + " from " + masterMember.Name;
								ttip.SetToolTip(btnUnmap, tipText);
							}
						}
					}
							
					/*
					// Update status text and fix grammar
					mapText = mapText.Substring(0, mapText.Length - 2);
					int lastComma = mapText.LastIndexOf(',');
					if (lastComma > 1)
					{
						string mt = mapText.Substring(0, lastComma);
						mt += " and";
						mt += mapText.Substring(lastComma + 1);
						mapText = mt;
					}
					if (sourceOnRight)
					{
						if (mapSrcToMast[newSourceMember.SavedIndex].Count > 1)
						{
							mapText = mapText + " are mapped to " + newSourceMember.Name;
						}
						else
						{
							mapText = mapText + " is mapped to " + newSourceMember.Name;
						}
						
					}
					*/
					//else
					//{
					//	mapText = newSourceMember.Name + " is mapped to " + mapText;
					//}
				} // End new source is NOT mapped to one or more masters
				//else
				//{
				//	mapText = newSourceMember.Name + " is unmapped";
				//} // End new source is, or isn't, already mapped to anything

				// If none of the masters mapped to the source is selected
				// (and thus unmap is disabled)
				// Is there a master selected, and is compatible with this source
				if (!btnUnmap.Enabled)
				{
					if (selectedMasterMember != null)
					{
						if (selectedMasterMember.MemberType == newSourceMember.MemberType)
						{
							btnMap.Enabled = true;
							string tipText = "Map " + newSourceMember.Name + " to " + selectedMasterMember.Name;
							ttip.SetToolTip(btnMap, tipText);
						}
					}
				} // End A mapped master is not selected

				// Finally, lets select all nodes for this new source member
				selectedSourceMember = newSourceMember;
				nodeList = sourceNodesBySI[newSourceMember.SavedIndex];
				NodesSelect(nodeList, true);
			} // End new selected source is not a track

			// Menu should mirror button state
			//lblSourceMappedTo.Text = mapText;
			StatusSourceUpdate(newSourceMember);
			mnuMap.Enabled = btnMap.Enabled;
			mnuUnmap.Enabled = btnUnmap.Enabled;

		} // END SELECT SOURCE MEMBER

		private void MemberSelectMaster(IMember masterMember)
		{
			IMember newMasterMember = masterMember;
			IMember lastSelectedMasterMember = selectedMasterMember;
			//string mapText = "";

			int sourceSI = utils.UNDEFINED;
			bool doSelect = false;
			List<TreeNodeAdv> nodeList = null;
			// Assume (for now) that neither mapping or unmapping is valid
			btnMap.Enabled = false;
			btnUnmap.Enabled = false;

			if (newMasterMember.MemberType == MemberType.Track)
			{
				// Unselect any previous selections
				if (lastSelectedMasterMember != null)
				{
					int oldMSI = lastSelectedMasterMember.SavedIndex;
					// Is it different from before
					if (oldMSI != newMasterMember.SavedIndex)
					{
						// Clear previous selection
						nodeList = masterNodesBySI[lastSelectedMasterMember.SavedIndex];
						NodesSelect(nodeList, false);

						// Was a source member mapped to the old master?
						if (mapMastToSrc[oldMSI] != null)
						{
							nodeList = sourceNodesBySI[mapMastToSrc[oldMSI].SavedIndex];
							NodesHighlight(nodeList, false);
						} // End a source was mapped to the old master
					} // End selection changed
				} // End there was a previous selection
				selectedMasterMember = null;
				//mapText = newMasterMember.Name + " is a track.\n\r";
				//mapText += "Tracks cannot be mapped (at this time)";
			}
			else // NOT a Track
			{
				// was one already selected?
				if (lastSelectedMasterMember != null)
				{
					int oldMSI = lastSelectedMasterMember.SavedIndex;
					// Is it different from before
					if (oldMSI != newMasterMember.SavedIndex)
					{
						// Clear previous selection
						nodeList = masterNodesBySI[lastSelectedMasterMember.SavedIndex];
						NodesSelect(nodeList, false);

						// Was a source member mapped to the old master?
						if (mapMastToSrc[oldMSI] != null)
						{
							nodeList = sourceNodesBySI[mapMastToSrc[oldMSI].SavedIndex];
							NodesHighlight(nodeList, false);
							nodeList = masterNodesBySI[oldMSI];
							NodesHighlight(nodeList, false);
						} // End a source was mapped to the old master
					} // End selection changed
				} // End there was a previous selection
					// If no master node was previously selected, then obviously this selection is different (from nothing)
					// Is a source member mapped to this new master member?
				IMember newSource = mapMastToSrc[newMasterMember.SavedIndex];
				if (newSource != null)
				{
					nodeList = sourceNodesBySI[newSource.SavedIndex];
					NodesHighlight(nodeList, true);

					// Build status text
					//if (sourceOnRight)
					//{
					//	mapText = newSource.Name + " is mapped to " + masterMember.Name;
					//}
					//else
					//{
					//	mapText = masterMember.Name + " is mapped to " + newSource.Name;
					//}

					// Is a source member selected, and does it happen to be the one mapped to this master?
					if (selectedSourceMember != null)
					{
						if (selectedSourceMember.SavedIndex == newSource.SavedIndex)
						{
							// Allow it to be unmapped
							btnUnmap.Enabled = true;
							string tipText = "Unmap " + selectedSourceMember.Name + " from " + selectedMasterMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
				} // End a source is mapped to the new master
					//else
					//{
					//	mapText = newMasterMember.Name + " is unmapped";
					//} // End new master does or does not have something mapped to it

				// If the selected source is not the one mapped to this new master
				// (and thus unmap is still disapbled)
				// Is there a source selected, and is it compatible to this master?
				if (!btnUnmap.Enabled)
				{
					if (selectedSourceMember != null)
					{
						if (selectedSourceMember.MemberType == newMasterMember.MemberType)
						{
							btnMap.Enabled = true;
							string tipText = "Map " + selectedSourceMember.Name + " to " + newMasterMember.Name;
							ttip.SetToolTip(btnMap, tipText);
						}
					}
				}
				// Finally, lets select all nodes for this new master member
				selectedMasterMember = newMasterMember;
				nodeList = masterNodesBySI[newMasterMember.SavedIndex];
				NodesSelect(nodeList, true);
			} // End newly selected master is not a track

			// Finally, lets select all the nodes for this new master
			StatusMasterUpdate(newMasterMember);
			// Menu state follows button state
			mnuMap.Enabled = btnMap.Enabled;
			mnuUnmap.Enabled = btnUnmap.Enabled;

		} // END SELECT MASTER MEMBER

		private int MapMembers(int masterSI, int sourceSI, bool andMembers, bool map)
		{
			int mapCount = 0;
			// Are we mapping or unmapping?
			try
			{
				IMember masterMember = seqMaster.Members[masterSI];
				IMember newSourceMember = seqSource.Members[sourceSI];

				if (map)
				{
					mapCount = MapMembers(masterMember, newSourceMember, andMembers);
				}
				else
				{
					mapCount = UnmapMembers(masterMember, newSourceMember, andMembers);
				}

				bool e = false;
				btnMap.Enabled = !map;
				mnuMap.Enabled = !map;
				btnUnmap.Enabled = map;
				btnUnmap.Enabled = map;
				if (mapCount > 0) e = true;
				btnSummary.Enabled = e;
				mnuSummary.Enabled = e;
				btnSaveMap.Enabled = e;
				mnuSaveNewMap.Enabled = e;
				btnSaveNewSeq.Enabled = e;
				mnuSaveNewSequence.Enabled = e;
			}
			catch
			{
				// Ignore?  Do Nothing?
				System.Diagnostics.Debugger.Break();
			}
			return mapCount;
		}

		private int MapMembers(IMember masterMember, IMember sourceMember, bool andMembers)
		{
			int mapCount = 0;
			bool alreadyMapped = false;
			masterMapLevel++;
			// First, is it a track?  And do we want to map its children?
			// Note: Tracks themselves cannot be mapped, but we can try to map its children
			if (masterMember.MemberType == MemberType.Track)
			{
				if (andMembers)
				{
					// Cast member to tracks so we can get the child membership
					Track sourceTrack = (Track)sourceMember;
					Track masterTrack = (Track)masterMember;
					// Is it even possible to map children, do they have the same number?
					if (sourceTrack.Members.Items.Count == masterTrack.Members.Items.Count)
					{
						for (int i = 0; i < sourceTrack.Members.Items.Count; i++)
						{
							IMember sourceItem = sourceTrack.Members.Items[i];
							IMember masterItem = masterTrack.Members.Items[i];
							// Are they the same type
							if (sourceItem.MemberType ==
									masterItem.MemberType)
							{
								//mapCount += MapMembers(masterItem, sourceItem, andMembers);
							}
						}
					}
				}
			}
			else
			{
				// Fetch the source and master
				//IMember sourceMember = seqSource.Members.bySavedIndex[theSourceSI];
				int masterSI = masterMember.SavedIndex;
				int sourceSI = sourceMember.SavedIndex;

				// Is the master channel already mapped to a different source?
				//   Note: A source can map to multiple masters.
				//     A master cannot map to more than one source.
				//       Therefore, mappings list is indexed by the Master SavedIndex

				// First, is this master mapped to something, anything?
				if (mapMastToSrc[masterSI] != null)
				{
					// Is this redundant?  Is this master already mapped to this source?
					if (mapMastToSrc[masterSI].SavedIndex == sourceSI)
					{
						alreadyMapped = true;
					}
					else // NOT alreadyMapped
					{
						// Is this source channel mapped to more than one master channel?
						if (mapSrcToMast[sourceSI].Count > 0)
						{
							// If so, find this master in its list of mappings and remove it
							for (int i = 0; i < mapSrcToMast[sourceSI].Count; i++)
							{
								if (mapSrcToMast[sourceSI][i].SavedIndex == masterSI)
								{
									//mapSrcToMast[sourceSI].RemoveAt(i);
									//mapCount--;
									int newMaps = UnmapMembers(masterMember, mapSrcToMast[sourceSI][i], true);
									mapCount += newMaps;
								}
							}
							// Was that the only channel the source was mapped to, and thus is now mapped to nothing?
							if (mapSrcToMast[sourceSI].Count == 0)
							{
								// Deselect it
								//List<TreeNodeAdv> nodeList = (List<TreeNodeAdv>)sourceMember.Tag;
								List<TreeNodeAdv> nodeList = sourceNodesBySI[sourceSI];
								sourceMember.Selected = false;
								NodesBold(nodeList, false);
								if (!bulkOp)
								{
									if (masterMapLevel < 2)
									{
										NodesHighlight(nodeList, false);
									}
								}
							}
						}
					}
				}

				if (!alreadyMapped)
				{
					// Sanity Check: Are they the same type?
					if (sourceMember.MemberType == masterMember.MemberType)
					{
						string msgOut = "Mapping " + masterMember.MemberType.ToString() + " MEMBER " + sourceMember.Name + " to " + masterMember.Name;
						Debug.WriteLine(msgOut);
						// Map this source to this master in the mapping list
						mapMastToSrc[masterSI] = sourceMember;
						mappedSIs[masterSI] = sourceSI;
						List<TreeNodeAdv> nodeList = masterNodesBySI[masterSI];
						masterMember.Selected = true;
						NodesBold(nodeList, true);
						if (!bulkOp)
						{
							if (masterMapLevel < 2)
							{
								NodesHighlight(nodeList, true);
							}
						}
						// In addition to the map list, put the source SavedIndex in the master channels AltSavedIndex
						masterMember.AltSavedIndex = sourceSI;
						masterMember.MapTo = sourceMember;

						// Map this master to this source in the other mapping list
						mapSrcToMast[sourceSI].Add(masterMember);
						sourceMember.Selected = true;
						nodeList = sourceNodesBySI[sourceSI];
						NodesBold(nodeList, true);
						if (!bulkOp)
						{
							if (masterMapLevel < 2)
							{
								NodesHighlight(nodeList, true);
							}
						}

						// Increment count(s)
						mapCount++;
						if (masterMember.MemberType == MemberType.Channel)
						{
							mappedChannelCount++;
						}

						// Do we need to map its children also?
						if (andMembers)
						{
							// Is it a group, and do we want its children mapped?
							if (masterMember.MemberType == MemberType.ChannelGroup)
							{
								// Cast Member to Group (so we can get its children membership)
								ChannelGroup sourceGroup = (ChannelGroup)sourceMember;
								ChannelGroup masterGroup = (ChannelGroup)masterMember;
								// Is it even possible to map children, do they have the same number?
								if (sourceGroup.Members.Items.Count == masterGroup.Members.Items.Count)
								{
									msgOut = "Recurse GROUP " + sourceMember.Name + " to " + masterMember.Name;
									Debug.WriteLine(msgOut);
									for (int i = 0; i < sourceGroup.Members.Items.Count; i++)
									{
										IMember sourceItem = sourceGroup.Members.Items[i];
										IMember masterItem = masterGroup.Members.Items[i];
										// Are they the same type
										if (masterItem.MemberType == sourceItem.MemberType)
										{
											int newMaps = MapMembers(masterItem, sourceItem, andMembers);
											mapCount += newMaps;
										}
									}
								}
							}
						}

						// Or, instead of a group or track, is it an RGB channel?
						if (masterMember.MemberType == MemberType.RGBchannel)
						{
							// You know the drill by now, cast to RGBchannel so we can get its 3 subchannels	
							RGBchannel sourceRGBchannel = (RGBchannel)sourceMember;
							RGBchannel masterRGBchannel = (RGBchannel)masterMember;
							// Always map an RGBchannels subchannels (don't bother to check 'andMembers' flag)
							msgOut = "Recurse RGB CHANNEL " + sourceMember.Name + " to " + masterMember.Name;
							Debug.WriteLine(msgOut);
							int newMaps = MapMembers(masterRGBchannel.redChannel, sourceRGBchannel.redChannel, false);
							mapCount += newMaps;
							newMaps = MapMembers(masterRGBchannel.grnChannel, sourceRGBchannel.grnChannel, false);
							mapCount += newMaps;
							newMaps = MapMembers(masterRGBchannel.bluChannel, sourceRGBchannel.bluChannel, false);
							mapCount += newMaps;
						}

						// Set button states
						btnMap.Enabled = false;
						string tipText = sourceMember.Name + " is now mapped to " + masterMember.Name;
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = true;
						tipText = "Unmap " + sourceMember.Name + " from " + masterMember.Name;
						ttip.SetToolTip(btnUnmap, tipText);
						btnUnmap.Enabled = true;
					} // end if map
				} // End if track, or not
			} // end NOT alreadyMapped

			StatusSourceUpdate(sourceMember);
			StatusMasterUpdate(masterMember);
			masterMapLevel--;
			return mapCount;
		} // End Map Members

		private int UnmapMembers(IMember masterMember, IMember sourceMember, bool andMembers)
		{
			int mapCount = 0;
			List<TreeNodeAdv> nodeList = null;

			// First, is it a track?  And do we want to map its children?
			// Note: Tracks themselves cannot be mapped, but we can try to map its children
			if (masterMember.MemberType == MemberType.Track)
			{
				// Cast the members to groups so we can get thier child memberships
				//Track sourceTrack = (Track)sourceMember;
				Track masterTrack = (Track)masterMember;
				for (int i = 0; i < masterTrack.Members.Items.Count; i++)
				{
					//mapCount += UnmapMembers(masterTrack.Members.Items[i].SavedIndex, masterTrack.AltSavedIndex, true);
					int masterItemSI = masterTrack.Members.Items[i].SavedIndex;
					int newMaps = UnmapMembers(masterTrack.Members.Items[i], mapMastToSrc[masterItemSI], true);
					mapCount += newMaps;
				}

			}
			else // Not a track
			{
				// Fetch the source and master
				int sourceSI = sourceMember.SavedIndex;
				int masterSI = masterMember.SavedIndex;

				// First sanity check, are these two channels actually already mapped?
				if (mapMastToSrc[masterSI].SavedIndex == sourceSI)
				{
					// Next sanity check, Is this source channel mapped to any masters (one or more)?
					if (mapSrcToMast[sourceSI].Count > 0)
					{
						// Find and remove this master
						for (int i = 0; i < mapSrcToMast[sourceSI].Count; i++)
						{
							if (mapSrcToMast[sourceSI][i].SavedIndex == masterSI)
							{
								mapSrcToMast[sourceSI].RemoveAt(i);
								i = mapSrcToMast[sourceSI].Count; // Force loop to end
							}
						}
					}
					// Was that the only channel the source was mapped to, and thus is now mapped to nothing?
					if (mapSrcToMast[sourceSI].Count == 0)
					{
						// Deselect it
						//nodeList = (List<TreeNodeAdv>)sourceMember.Tag;
						nodeList = sourceNodesBySI[sourceSI];
						sourceMember.Selected = false;
						NodesBold(nodeList, false);
						NodesHighlight(nodeList, false);
					}
					masterMember.AltSavedIndex = utils.UNDEFINED;
					//masterMember.MapTo = null;
					mapMastToSrc[masterSI] = null;
					mappedSIs[masterSI] = utils.UNDEFINED;
					//nodeList = (List<TreeNodeAdv>)masterMember.Tag;
					nodeList = masterNodesBySI[masterSI];
					masterMember.Selected = false;
					NodesBold(nodeList, false);
					NodesHighlight(nodeList, false);
					mapCount--;
					if (masterMember.MemberType == MemberType.Channel)
					{
						mappedChannelCount--;
					}
					btnMap.Enabled = true;
					string tipText = "Map " + sourceMember.Name + " to " + masterMember.Name;
					ttip.SetToolTip(btnMap, tipText);
					mnuMap.Enabled = true;
					btnUnmap.Enabled = false;
					tipText = sourceMember.Name + " is no longer mapped to " + masterMember.Name;
					ttip.SetToolTip(btnUnmap, tipText);
					mnuUnmap.Enabled = false;

					// ALWAYS UNGROUP ALL CHILD MEMBERS whether specified or not
					//if (andMembers)
					//{ 
					// Are they groups?
					if (masterMember.MemberType == MemberType.ChannelGroup)
					{
						// Cast the members to groups so we can get their child memberships
						//ChannelGroup sourceGroup = (ChannelGroup)sourceMember;
						ChannelGroup masterGroup = (ChannelGroup)masterMember;
						for (int i = 0; i < masterGroup.Members.Items.Count; i++)
						{
							//Unmap all children and descendants	
							//mapCount += UnmapMembers(masterGroup.Members.Items[i].SavedIndex, masterGroup.AltSavedIndex, true);
							mapCount += UnmapMembers(masterGroup.Members.Items[i], mapMastToSrc[masterSI], true);
						}
					}

					// if not a group, is it an RGB channel?
					if (masterMember.MemberType == MemberType.RGBchannel)
					{
						//RGBchannel sourceRGBchannel = (RGBchannel)seqSource.Members.bySavedIndex[theSourceSI];
						RGBchannel masterRGBchannel = (RGBchannel)masterMember;
						RGBchannel sourceRGBchannel = (RGBchannel)sourceMember;
						//mapCount += UnmapMembers(masterRGBchannel.redChannel.SavedIndex, sourceRGBchannel.redChannel.SavedIndex, true);
						//mapCount += UnmapMembers(masterRGBchannel.grnChannel.SavedIndex, sourceRGBchannel.grnChannel.SavedIndex, true);
						//mapCount += UnmapMembers(masterRGBchannel.bluChannel.SavedIndex, sourceRGBchannel.bluChannel.SavedIndex, true);
						int newMaps = UnmapMembers(masterRGBchannel.redChannel, sourceRGBchannel.redChannel, true);
						mapCount += newMaps;
						newMaps = UnmapMembers(masterRGBchannel.grnChannel, sourceRGBchannel.grnChannel, true);
						mapCount += newMaps;
						newMaps = UnmapMembers(masterRGBchannel.bluChannel, sourceRGBchannel.bluChannel, true);
						mapCount += newMaps;
					}
				}
			} // end map or unmap
				//mappedMemberCount += mapCount;
				//lblMapCount.Text = mappedMemberCount.ToString();
			StatusSourceUpdate(sourceMember);
			StatusMasterUpdate(masterMember);
			return mapCount;
		}

		private void UnmapSelectedMembers()
		{
			string tipText = "";
			// The button should not have even been enabled if the 2 Channels are not alreaDY mapped ... but--
			// Verify it anyway!

			// Is a node Selected on each side?
			if (selectedSourceNode != null)
			{
				if (selectedMasterNode != null)
				{
					// Types match?
					masterMapLevel = 0; // Reset
					sourceMapLevel = 0;
					if (selectedSourceMember.MemberType == MemberType.Channel)
					{
						if (selectedMasterMember.MemberType == MemberType.Channel)
						{
							//MapMembers(masterSI, sourceSI, false, true);
							int newMaps = UnmapMembers(selectedMasterMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = true;
							tipText = "Map " + selectedSourceMember.Name + " to " + selectedMasterMember.Name;
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is no longer mapped to " + selectedMasterMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_MapRegularToRegular;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are different types";
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are not mapped";
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == MemberType.RGBchannel)
					{
						if (selectedMasterMember.MemberType == MemberType.RGBchannel)
						{
							//MapMembers(masterSI, sourceSI, false, true);
							int newMaps = UnmapMembers(selectedMasterMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = true;
							tipText = "Map " + selectedSourceMember.Name + " to " + selectedMasterMember.Name;
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is no longer mapped to " + selectedMasterMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_MapRGBtoRGB;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are different types";
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are not mapped";
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == MemberType.ChannelGroup)
					{
						if (selectedMasterMember.MemberType == MemberType.ChannelGroup)
						{
							//MapMembers(masterSI, sourceSI, false, true);
							int newMaps = UnmapMembers(selectedMasterMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = false;
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are not mapped";
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_GroupToGroup;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are different types";
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are not mapped";
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == MemberType.Track)
					{
						statMsg = Resources.MSG_Tracks;
						StatusMessage(statMsg);
						System.Media.SystemSounds.Beep.Play();
						btnMap.Enabled = false;
						tipText = "Tracks cannot be mapped";
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = false;
						tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are not mapped";
						ttip.SetToolTip(btnUnmap, tipText);
					}
					if (selectedMasterMember.MemberType == MemberType.Track)
					{
						statMsg = Resources.MSG_Tracks;
						StatusMessage(statMsg);
						System.Media.SystemSounds.Beep.Play();
						btnMap.Enabled = false;
						tipText = "Tracks cannot be mapped";
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = false;
						tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are not mapped";
						ttip.SetToolTip(btnUnmap, tipText);
					}
					UpdateMappedCount(0);
				}
				else
				{
					System.Media.SystemSounds.Beep.Play();
					btnMap.Enabled = false;
					tipText = "No destination is selected";
					ttip.SetToolTip(btnMap, tipText);
					btnUnmap.Enabled = false;
					tipText = selectedSourceMember.Name + " is not mapped";
					ttip.SetToolTip(btnUnmap, tipText);
				}
			}
			else
			{
				System.Media.SystemSounds.Beep.Play();
				btnMap.Enabled = false;
				tipText = "No source is selected";
				ttip.SetToolTip(btnMap, tipText);
				btnUnmap.Enabled = false;
				tipText = "Selection is not mapped";
				ttip.SetToolTip(btnUnmap, tipText);
			}

			// Copy button enable state to menus
			mnuMap.Enabled = btnMap.Enabled;
			mnuUnmap.Enabled = btnUnmap.Enabled;
			mnuSaveNewMap.Enabled = btnSaveMap.Enabled;
		}

		private void AskToMap()
		{
			string msg = "";
			DialogResult dr;

			if (seqMaster.Channels.Count > 0)
			{
				if (seqSource.Channels.Count > 0)
				{
					if (File.Exists(mapFile))
					{
						mapFileLineCount = GetMapFileLineCount(mapFile);
						if (mapFileLineCount > 2)
						{
							msg = "Load and Apply last Map file: '" + Path.GetFileName(mapFile) + "'?";
							dr = MessageBox.Show(this, msg, "Load & Apply last Map?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
							if (dr == DialogResult.Yes)
							{
								txtMappingFile.Text = Path.GetFileName(mapFile);
								LoadApplyMapFile(mapFile);
							} // end load/apply last map
						} // end map file line count
					} // end map file exists

					if (mappedMemberCount < 1)
					{
						msg = "Perform an Auto Map?";
						dr = MessageBox.Show(this, msg, "Perform Auto Map?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
						if (dr == DialogResult.Yes)
						{
							mapFile = "(Automap)";
							txtMappingFile.Text = Path.GetFileName(mapFile);
							AutoMap();
						} // end automap
					} // end mappings exists
				} // end source is loaded
			} // end master is loaded
		} // end AskToMap

		private string MapLine(IMember member)
		{
			string ret = member.Name + utils.DELIM1;
			ret += member.SavedIndex.ToString() + utils.DELIM1;
			ret += SeqEnums.MemberName(member.MemberType);
			return ret;
		}

		private void ClearAllMappings()
		{
			mappedMemberCount = 0;

			mapMastToSrc = null;  // Array indexed by Master.SavedIndex, elements contain Source.SaveIndex
			mappedSIs = null;
			Array.Resize(ref mapMastToSrc, seqMaster.Members.allCount);
			Array.Resize(ref mappedSIs, seqMaster.Members.allCount);
			for (int i = 0; i < mapMastToSrc.Length; i++)
			{
				mapMastToSrc[i] = null;
				mappedSIs[i] = utils.UNDEFINED;
			}

			mapSrcToMast = null; // Array indexed by Source.SavedIindex, elements are Lists of Master.SavedIndex-es
			Array.Resize(ref mapSrcToMast, seqSource.Members.allCount);
			for (int i = 0; i < mapSrcToMast.Length; i++)
			{
				mapSrcToMast[i] = new List<IMember>();
			}

			NodesUnselectAll(treeMaster.Nodes);
			NodesUnselectAll(treeSource.Nodes);

		}

		private int UpdateMappedCount(int change)
		{
			mappedMemberCount += change;
			lblMappedCount.Text = mappedMemberCount.ToString() + " / " + mappedChannelCount.ToString();
			if (mappedMemberCount > 0)
			{
				btnSummary.Enabled = true;
				btnSaveNewSeq.Enabled = true;
			}
			else
			{
				btnSummary.Enabled = false;
				btnSaveNewSeq.Enabled = false;
			}
			EnableMappingButtons();

			return mappedMemberCount;
		}

		private void EnableMappingButtons()
		{
			string tipText = "";
			// Is a master selected?
			if (selectedMasterMember == null)
			{
				// No master selected, so can't map or unmap to nothing
				btnMap.Enabled = false;
				tipText = "No destination selected";
				ttip.SetToolTip(btnMap, tipText);
				btnUnmap.Enabled = false;
				ttip.SetToolTip(btnUnmap, tipText);
			}
			else
			{
				// a master is selected, is a source selected?
				if (selectedSourceMember == null)
				{
					// No source is selected, so can't map or unmap anything from that master
					btnMap.Enabled = false;
					tipText = "No source is selected";
					ttip.SetToolTip(btnMap, tipText);
					btnUnmap.Enabled = false;
					ttip.SetToolTip(btnUnmap, tipText);
				}
				else
				{
					// We have both a master and a source selected
					// TODO: Allow tracks to be mapped
					if (selectedMasterMember.MemberType == MemberType.Track)
					{
						// If master is a track, disallow mapping (for now, TODO)
						btnMap.Enabled = false;
						tipText = "Tracks cannot be mapped";
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = false;
						tipText = selectedSourceMember.Name + " is not mapped to " + selectedMasterMember.Name + "\r\n(And Tracks cannot be mapped)";
						ttip.SetToolTip(btnUnmap, tipText);
					}
					else
					{
						// Master is not track, what about the source?
						if (selectedMasterMember.MemberType == MemberType.Track)
						{
							// Source is a track, disallow mapping (for now, TODO)
							btnMap.Enabled = false;
							tipText = "Tracks cannot be mapped";
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is not mapped to " + selectedMasterMember.Name + "\r\n(And Tracks cannot be mapped)";
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							// Neither is a track
							// Get the selected master's SavedIndex, and check to see if it's mapped
							int masterSI = selectedMasterMember.SavedIndex;
							IMember mapTo = mapMastToSrc[masterSI];
							if (mapTo == null)
							{
								// Master isn't mapped to ANYTHING, much less this source, so we certainly can't unmap it
								btnUnmap.Enabled = false;
								tipText = selectedMasterMember.Name + " is not mapped to a source";
								ttip.SetToolTip(btnUnmap, tipText);
								// So is the selected source the same type as the selected master?
								if (selectedMasterMember.MemberType == selectedSourceMember.MemberType)
								{
									// Types match, so possible to map them
									btnMap.Enabled = true;
									tipText = "Map " + selectedSourceMember.Name + " to " + selectedMasterMember.Name;
									ttip.SetToolTip(btnMap, tipText);
								}
								else
								{
									// Types do NOT match, so can't map them
									btnMap.Enabled = false;
									tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are different types";
									ttip.SetToolTip(btnMap, tipText);
								} // End Types Match (or not)
							}
							else
							{
								// Master is mapped, but to what?
								int mappedSI = mapTo.SavedIndex;
								// Is it mapped to the selected source?
								if (mappedSI == selectedSourceMember.SavedIndex)
								{
									// So the selected master and source ARE mapped, so allow them to be unmapped
									// but not to (redundantly) mapped to each other again
									btnMap.Enabled = false;
									tipText = selectedSourceMember.Name + " is already mapped to " + selectedMasterMember.Name;
									ttip.SetToolTip(btnMap, tipText);
									btnUnmap.Enabled = true;
									tipText = "Unmap " + selectedSourceMember.Name + " from " + selectedMasterMember.Name;
									ttip.SetToolTip(btnUnmap, tipText);
								}
								else
								{
									// The selected master is mapped, but NOT to the selected source
									btnUnmap.Enabled = false;
									tipText = selectedSourceMember.Name + " is not mapped to " + selectedMasterMember.Name;
									ttip.SetToolTip(btnUnmap, tipText);
									// That's OK though, since we could remap the master to something different
									// So is the selected source the same type as the selected master?
									if (selectedMasterMember.MemberType == selectedSourceMember.MemberType)
									{
										// Types match, so possible to map them
										btnMap.Enabled = true;
										tipText = "Map " + selectedSourceMember.Name + " to " + selectedMasterMember.Name;
										ttip.SetToolTip(btnMap, tipText);
									}
									else
									{
										// Types do NOT match, so can't map them
										btnMap.Enabled = false;
										tipText = selectedSourceMember.Name + " and " + selectedMasterMember.Name + " are different types";
										ttip.SetToolTip(btnMap, tipText);
									} // End Types Match (or not)
								} // End Already Mapped (or not)
							} // End Master is already mappee
						} // End Not Track
					} // End Not Track
				} // End Source Set
			} // End Master Set
		} // End EnableMappingButtons

		private void AutoMap()
		{
			ImBusy(true);
			bulkOp = true;

			string sourceName = "";
			int sourceSI = utils.UNDEFINED;
			int masterSI = utils.UNDEFINED;
			int[] matchSIs = null;
			IMember srcID = null;
			const string STATUSautoMap = "AutoMap \"";
			const string STATUSto = "\" to...";

			int w =  pnlStatus.Width;
			int matchCount = 0;
			pnlProgress.Width = w;
			pnlProgress.Size = new Size(w, pnlProgress.Height);
			pnlStatus.Visible = false;
			pnlProgress.Visible = true;
			staStatus.Refresh();
			//int mq = seqMaster.Tracks.Count + seqMaster.ChannelGroups.Count + seqMaster.Channels.Count;
			int mq = seqMaster.Tracks.Count;
			//mq -= seqMaster.RGBchannels.Count * 2;
			//int sq = seqSource.Tracks.Count + seqSource.ChannelGroups.Count + seqSource.Channels.Count;
			int sq = seqSource.Tracks.Count;
			//sq -= seqSource.RGBchannels.Count * 2;
			int mm = seqMaster.Members.Count;
			int qq = mq + sq + mm;
			//pnlProgress.Maximum = (int)(qq);
			pnlProgress.Maximum = seqMaster.Members.Count * 10;
			pp = 0;
			string logFile = Fyle.GetAppTempFolder() + "Fuzzy.log";
			if (File.Exists(logFile))
			{
				File.Delete(logFile);
			}




			// First Pass, try to match by SavedIndex
			// TRACKS
			//for (int tridx = 0; tridx < seqMaster.Tracks.Count; tridx++)
			//{
			//	Track masterTrack = seqMaster.Tracks[tridx];
			//	lblMessage.Text = STATUSautoMap + masterTrack.Name + STATUSto;
			//	pnlMessage.Refresh();
			//	pp++;
			//	pnlProgress.Value = pp;
			//	staStatus.Refresh();
			//int mcs = AutoMapMembersBySavedIndex(masterTrack.Members);
			int mcs = AutoMapMembersBySavedIndex(seqMaster.Members);
				//matchCount += mcs;
			mappedMemberCount += mcs;
			//}

			//for (int tridx = 0; tridx < seqMaster.Tracks.Count; tridx++)
			//{
			//Track masterTrack = seqMaster.Tracks[tridx];
			//lblMessage.Text = STATUSautoMap + masterTrack.Name + STATUSto;
			//pnlMessage.Refresh();
			//pp++;
			//pnlProgress.Value = pp;
			//staStatus.Refresh();
			//int mcn = AutoMapMembersByName(masterTrack.Members);
			int mcn = AutoMapMembersByName(seqMaster.Members);
			//matchCount += mcn;
			mappedMemberCount += mcn;
			//}

			//for (int tridx = 0; tridx < seqMaster.Tracks.Count; tridx++)
			//{
			//Track masterTrack = seqMaster.Tracks[tridx];
			//int mc = AutoMapMembersByFuzzy(masterTrack.Members);
			int mcc = AutoMapMembersByFuzzy(seqMaster.Members);
			//matchCount += mcc;
			mappedMemberCount += mcc;
			//}

			pnlProgress.Visible = false;
			lblMessage.Text = "Automap Complete";
			UpdateMappedCount(0);
			bulkOp = false;
			ImBusy(false);
		}  // end Track loop

		private int AutoMapMembersBySavedIndex(Membership masterMembers)
		{
			int matchCount = 0;
			for (int memidx = 0; memidx < masterMembers.Count; memidx++)
			{
				IMember masterMember = masterMembers[memidx];
				if (masterMember.MemberType != MemberType.Track)
				{
					if (mapMastToSrc[masterMember.SavedIndex] == null) // Mapped already?
					{
						if (masterMember.SavedIndex <= seqSource.Members.HighestSavedIndex)
						{
							IMember newSourceMember = seqSource.Members.bySavedIndex[masterMember.SavedIndex];
							if (masterMember.MemberType == newSourceMember.MemberType)
							{
								if (masterMember.Name.ToLower().CompareTo(newSourceMember.Name.ToLower()) == 0)
								{
									int newMaps = MapMembers(masterMember, newSourceMember, false);
									matchCount += newMaps;
									if (masterMember.MemberType == MemberType.ChannelGroup)
									{
										//	ChannelGroup masterGroup = (ChannelGroup)masterMember;
										//	int mc = AutoMapMembersBySavedIndex(masterGroup.Members); // Recurse
										//	mappedMemberCount += mc;
										lblMessage.Text = STATUSautoMap + masterMember.Name + STATUSto;
										pnlMessage.Refresh();
									}
								}
							}
						}
					}
				}
				pp ++;
				pnlProgress.Value = pp;
				staStatus.Refresh();
			}
			return matchCount;
		}

		private int AutoMapMembersByName(Membership masterMembers)
		{
			int matchCount = 0;
			for (int memidx = 0; memidx < masterMembers.Count; memidx++)
			{
				IMember masterMember = masterMembers[memidx];
				if (masterMember.MemberType != MemberType.Track)
				{
					if (mapMastToSrc[masterMember.SavedIndex] == null) // Mapped already?
					{
						IMember newSourceMember = seqSource.Members.Find(masterMember.Name, masterMember.MemberType, false);
						if (newSourceMember != null)
						{
							if (masterMember.Name.ToLower().CompareTo(newSourceMember.Name.ToLower()) == 0)
							{
								int newMaps = MapMembers(masterMember, newSourceMember, false);
								matchCount += newMaps;
								if (masterMember.MemberType == MemberType.ChannelGroup)
								{
									//	ChannelGroup masterGroup = (ChannelGroup)masterMember;
									//	int mc = AutoMapMembersByName(masterGroup.Members); // Recurse
									//	mappedMemberCount += mc;
									lblMessage.Text = STATUSautoMap + masterMember.Name + STATUSto;
									pnlMessage.Refresh();

								}
							}
						}
					}
				}
				pp ++;
				pnlProgress.Value = pp;
				staStatus.Refresh();
			}
			return matchCount;
		}

		private int AutoMapMembersByFuzzy(Membership masterMembers)
		{
			int matchCount = 0;
			for (int memidx = 0; memidx < masterMembers.Count; memidx++)
			{
				IMember masterMember = masterMembers[memidx];
				if (masterMember.MemberType != MemberType.Track)
				{
					if (mapMastToSrc[masterMember.SavedIndex] == null) // Mapped already?
					{
						lblMessage.Text = STATUSautoMap + masterMember.Name + STATUSto;
						pnlMessage.Refresh();
						IMember newSourceMember = FuzzyFindName(masterMember.Name, seqSource, masterMember.MemberType);
						if (newSourceMember != null)
						{
							int newMaps = MapMembers(masterMember, newSourceMember, false);
							matchCount += newMaps;
							//if (masterMember.MemberType == MemberType.ChannelGroup)
							//{
							//	ChannelGroup masterGroup = (ChannelGroup)masterMember;
							//	int mc = AutoMapMembersByName(masterGroup.Members); // Recurse
							//	matchCount += mc;
							//}
						}
					}
				}
				pp+=8;
				pnlProgress.Value = pp;
				staStatus.Refresh();
			}
			return matchCount;
		}



		#endregion

		#region Status Operations
		private void StatusSourceUpdate(IMember sourceMember)
		{
			string mapText = "";
			if (sourceMember.MemberType == MemberType.Track)
			{
				mapText = sourceMember.Name + " is a track.\n\r";
				mapText += "Tracks cannot be mapped (at this time)";
			}
			else
			{
				// Is the new one mapped to one or more masters?
				if (mapSrcToMast[sourceMember.SavedIndex].Count > 0)
				{
					// highlight all Master Nodes already mapped to this Source node
					foreach (IMember masterMember in mapSrcToMast[sourceMember.SavedIndex])
					{
						// Build status text
						mapText += masterMember.Name + ", ";
					}

					// Update status text and fix grammar
					mapText = mapText.Substring(0, mapText.Length - 2);
					int lastComma = mapText.LastIndexOf(',');
					if (lastComma > 1)
					{
						string mt = mapText.Substring(0, lastComma);
						mt += " and";
						mt += mapText.Substring(lastComma + 1);
						mapText = mt;
					}
					if (sourceOnRight)
					{
						if (mapSrcToMast[sourceMember.SavedIndex].Count > 1)
						{
							mapText = mapText + " are mapped to " + sourceMember.Name;
						}
						else
						{
							mapText = mapText + " is mapped to " + sourceMember.Name;
						}
					}
					else
					{
						mapText = sourceMember.Name + " is mapped to " + mapText;
					}
				} // End new source is NOT mapped to one or more masters
				else
				{
					mapText = sourceMember.Name + " is unmapped";
				} // End new source is, or isn't, already mapped to anything
			} // End Track, or not
			lblSourceMappedTo.Text = mapText;
		}

		private void StatusMasterUpdate(IMember masterMember)
		{
			string mapText = "";
			if (masterMember.MemberType == MemberType.Track)
			{
				mapText = masterMember.Name + " is a track.\n\r";
				mapText += "Tracks cannot be mapped (at this time)";
			}
			else
			{
				IMember mappedSource = mapMastToSrc[masterMember.SavedIndex];
				if (mappedSource == null)
				{
					mapText = masterMember.Name + " is unmapped";
				}
				else
				{
					// Build status text
					if (sourceOnRight)
					{
						mapText = mappedSource.Name + " is mapped to " + masterMember.Name;
					}
					else
					{
						mapText = masterMember.Name + " is mapped to " + mappedSource.Name;
					}
				}
			}
			lblMasterMappedTo.Text = mapText;
		}

		private void StatusMessage(string message)
		{
			// For now at least... I think I'm gonna use this just to show WHY the 'Map' button is not enabled


			lblMessage.Text = message;
			lblMessage.Visible = (message.Length > 0);
			lblMessage.Left = (this.Width - lblMessage.ClientSize.Width) / 2;

		}

		private void LogMessage(string message)
		{
			//TODO: This
		}

		private void ShowProgressBars(int howMany)
		{
			if (howMany == 0)
			{
				btnLoadMap.Visible = true;
				btnSaveMap.Visible = true;
				txtMappingFile.Visible = true;
				prgBarInner.Visible = false;
				prgBarOuter.Visible = false;
			}
			else
			{
				btnLoadMap.Visible = false;
				btnSaveMap.Visible = false;
				txtMappingFile.Visible = false;
				prgBarInner.Value = 0;
				prgBarInner.Visible = true;
			}
			if (howMany == 2)
			{
				prgBarInner.Value = 0;
				prgBarOuter.Value = 0;
				prgBarInner.Visible = true;
				prgBarOuter.Visible = true;
			}
			else
			{
				prgBarOuter.Visible = false;
			}
		} // end ShowProgressBars



		#endregion

		#region Copy Beat Track
		private void CopyBeats(Sequence4 seqNew)
		{
			Track newMasterTrack = null;
			for (int t = 0; t < seqSource.Tracks.Count; t++)
			{
				string tn = seqSource.Tracks[t].Name.ToLower();
				if ((tn.IndexOf("beat") >= 0) ||
					(tn.IndexOf("song parts") >= 0) ||
					(tn.IndexOf("information") >= 0) ||
					(tn.IndexOf("o-rama") >= 0) ||
					(tn.IndexOf("vamperizer") >= 0))
				{
					newMasterTrack = CopyTrack(seqSource.Tracks[t], seqNew);
					seqNew.MoveTrackByIndex(newMasterTrack.Index, t);
				}
			}

		}

		private Track CopyTrack(Track sourceTrack, Sequence4 seqNew)
		{
			Track newTrack = seqNew.FindTrack(sourceTrack.Name);
			if (newTrack == null)
			{
				newTrack = seqNew.ParseTrack(sourceTrack.LineOut());
			}
			newTrack.Centiseconds = sourceTrack.Centiseconds;
			seqNew.Centiseconds = sourceTrack.Centiseconds;
			CopyItems(sourceTrack.Members, newTrack.Members);

			return newTrack;
		}

		private void CopyItems(Membership sourceParts, Membership destParts)
		{
			//foreach(IMember member in SourceTrack.Members)
			for (int i = 0; i < sourceParts.Count; i++)
			{
				IMember member = sourceParts.Items[i];
				MemberType partType = member.MemberType;
				switch (partType)
				{
					case MemberType.Channel:
						Channel sourceCh = (Channel)member;
						Channel destCh = (Channel)destParts.Find(sourceCh.Name, MemberType.Channel, false);
						if (destCh == null)
						{
							destCh = seqMaster.CreateChannel(sourceCh.Name);
							//destCh = seqMaster.ParseChannel(sourceCh.LineOut());
						}
						CopyChannel(sourceCh, destCh);
						destParts.Items.Add(destCh);
						break;
					case MemberType.RGBchannel:
						RGBchannel sourceRGB = (RGBchannel)member;
						RGBchannel destRGB = (RGBchannel)destParts.Find(sourceRGB.Name, MemberType.RGBchannel, false);
						if (destRGB == null)
						{
							destRGB = seqMaster.CreateRGBchannel(sourceRGB.Name);
						}
						CopyRGBchannel(sourceRGB, destRGB);
						destParts.Items.Add(destRGB);
						break;
					case MemberType.ChannelGroup:
						ChannelGroup sourceGroup = (ChannelGroup)member;
						ChannelGroup destGroup = (ChannelGroup)destParts.Find(sourceGroup.Name, MemberType.ChannelGroup, false);
						if (destGroup == null)
						{
							destGroup = seqMaster.CreateChannelGroup(sourceGroup.Name);
						}
						CopyItems(sourceGroup.Members, destGroup.Members);
						destParts.Items.Add(destGroup);
						break;
				}
			}
		}

		private Channel CopyChannel(Channel sourceChannel, Channel destChannel)
		{
			// Assume the name was set when destChannel was constructed
			//destChannel.output.channel		= sourceChannel.output.channel;
			//destChannel.output.circuit		= sourceChannel.output.circuit;
			//destChannel.output.deviceType = sourceChannel.output.deviceType;
			//destChannel.output.isViz			= sourceChannel.output.isViz;
			//destChannel.output.network		= sourceChannel.output.network;
			//destChannel.output.unit				= sourceChannel.output.unit;
			destChannel.output = sourceChannel.output.Clone();
			
			destChannel.Centiseconds = sourceChannel.Centiseconds;
			destChannel.color = sourceChannel.color;
			destChannel.rgbChild = sourceChannel.rgbChild;
			destChannel.CopyEffects(sourceChannel.effects, false);
			return destChannel;
		}

		private RGBchannel CopyRGBchannel(RGBchannel sourceRGB, RGBchannel destRGB)
		{
			// Copy RED Channel
			Channel chR = seqMaster.FindChannel(sourceRGB.redChannel.Name);
			if (chR == null)
			{
				chR = seqMaster.CreateChannel(sourceRGB.redChannel.Name);
			}
			CopyChannel(sourceRGB.redChannel, chR);
			chR.rgbParent = destRGB;
			destRGB.redChannel = chR;

			// Copy GREEN Channel
			Channel chG = seqMaster.FindChannel(sourceRGB.grnChannel.Name);
			if (chG == null)
			{
				chG = seqMaster.CreateChannel(sourceRGB.grnChannel.Name);
			}
			CopyChannel(sourceRGB.grnChannel, chG);
			chG.rgbParent = destRGB;
			destRGB.grnChannel = chG;

			// Copy BLUE Channel
			Channel chB = seqMaster.FindChannel(sourceRGB.bluChannel.Name);
			if (chB == null)
			{
				chB = seqMaster.CreateChannel(sourceRGB.bluChannel.Name);
			}
			CopyChannel(sourceRGB.bluChannel, chB);
			chB.rgbParent = destRGB;
			destRGB.bluChannel = chB;

			// Copy remaining attributes
			destRGB.Centiseconds = sourceRGB.Centiseconds;

			// Return destination
			return destRGB;
		}
		#endregion

		/*
		private int NodeHighlight(TreeView tree, string tag)
		{
			int tl = tag.Length;
			int found = 0;
			foreach (TreeNodeAdv nOde in tree.Nodes)
			{
				// Reset any previous selections
				//NodeHighlight(nOde, false);

				// If node is mapped, it's tag will include the node it is mapped to, and thus the tag length
				// will be longer than just it's own tag info
				int iPos = nOde.Tag.ToString().IndexOf(DELIM_Map);
				if (iPos > 0)
				{
					string mappedTag = nOde.Tag.ToString().Substring(iPos + 1);
					if (mappedTag.CompareTo(oldTag) == 0)
					{
						// The old channel is mapped to this one
						nOde.EnsureVisible();
						NodeHighlight(nOde, true);
						if (found == 0)
						{
							treeMaster.SelectedNode = nOde;
						}
						found++;

					}
				} // end long tag
			} // end foreach
			return found;
		} // end HighlightNewNodes
		*/
		/*
		 * private int HighlightSourceNode(string tag)
		{
			int tl = tag.Length;
			int found = 0;
			foreach (TreeNodeAdv nOde in treeSource.Nodes)
			{
				// Reset any previous selections
				NodeHighlight(nOde, false);

				// If node is mapped, it's tag will include the node it is mapped to, and thus the tag length
				// will be longer than just it's own tag info
				if (nOde.Tag.ToString().Length > tl)
				{
					string myTag = nOde.Tag.ToString().Substring(0, tl);
					if (tag.CompareTo(myTag) == 0)
					{
						// The old channel is mapped to this one
						nOde.EnsureVisible();
						NodeHighlight(nOde, true);
						treeSource.SelectedNode = nOde;
						found++;

					}
				} // end long tag
			} // end foreach
			return found;
		} // end HighlightNewNodes
		*/

		/*
		private void X_UnhighlightAllNodes(TreeNodeCollection nOdes)
		{
			foreach (TreeNodeAdv nOde in nOdes)
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
					X_UnhighlightAllNodes(nOde.Nodes);
				}
			}
		}
		*/

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

		/*private void NodesHighlight(bool master, int SID)
		{
			if (master)
			{
				foreach (TreeNodeAdv nOde in masterNodesBySI[SID])
				{
					NodeHighlight(nOde, true);
				}
			}
			else
			{
				foreach (TreeNodeAdv nOde in sourceNodesBySI[SID])
				{
					NodeHighlight(nOde, true);
				}
			}
		}
		*/

		private int[] FindChannelNames(Sequence4 seq, string theName, MemberType type)
		{
			// Not just regular Channels, but RGB Channels and Channel Groups too
			// Returns an array of Saved Indexes
			// If one or more exact matches are found, it returns a list of those, and no fuzzy find is performed
			// If no exact matches are found, then perform a fuzzy find and return closest match
			int[] ret = null;
			match[] matches = null;
			int matchCount = 0;

			// Pass 1, look for exact matches
			if (type == MemberType.ChannelGroup)
			{
				foreach (ChannelGroup grp in seq.ChannelGroups)
				{
					if (theName.CompareTo(grp.Name) == 0)
					{
						Array.Resize(ref matches, matchCount + 1);
						matches[matchCount].savedIdx = grp.SavedIndex;
						matches[matchCount].MemberType = MemberType.ChannelGroup;
						matches[matchCount].itemIdx = grp.Index;
						matches[matchCount].score = 1;
						matchCount++;
					} // match
				} // loop thru Channel Groups
			} // end if type is Channel Group

			if (type == MemberType.RGBchannel)
			{
				foreach (RGBchannel rgb in seq.RGBchannels)
				{
					if (theName.CompareTo(rgb.Name) == 0)
					{
						Array.Resize(ref matches, matchCount + 1);
						matches[matchCount].savedIdx = rgb.SavedIndex;
						matches[matchCount].MemberType = MemberType.RGBchannel;
						matches[matchCount].itemIdx = rgb.Index;
						matches[matchCount].score = 1;
						matchCount++;
					} // match
				} // loop thru RGB Channels
			} // end if type is RGB Channel

			if (type == MemberType.Channel)
			{
				foreach (Channel ch in seq.Channels)
				{
					if (theName.CompareTo(ch.Name) == 0)
					{
						Array.Resize(ref matches, matchCount + 1);
						matches[matchCount].savedIdx = ch.SavedIndex;
						matches[matchCount].MemberType = MemberType.Channel;
						matches[matchCount].itemIdx = ch.Index;
						matches[matchCount].score = 1;
						matchCount++;
					} // match
				} // End first pass channel loop
			} // type is regular channel

			// Found any exact matches?
			if (matchCount >0)
			{
				Array.Resize(ref ret, matchCount);
				for (int m = 0; m< matchCount; m++)
				{
					ret[m] = matches[m].savedIdx;
				}
			}
			else
			{
				// No exact match(es) found, perform a fuzzy find
				// Pass 2, for the sake of speed, prequalify matches with a quick simple fuzzy algorithm
				// then run full algorithms later only on those that prequalify
				double score = 0;
				if (type == MemberType.ChannelGroup)
				{
					foreach(ChannelGroup grp in seq.ChannelGroups)
					{
						score = theName.LevenshteinDistance(grp.Name);
						if (score > MINSCORE)
						{
							Array.Resize(ref matches, matchCount + 1);
							matches[matchCount].savedIdx = grp.SavedIndex;
							matches[matchCount].MemberType = MemberType.ChannelGroup;
							matches[matchCount].itemIdx = grp.Index;
							matches[matchCount].score = score;
							matchCount++;
						}
					}
				} // end if type is Channel Group

				if (type == MemberType.RGBchannel)
				{
					foreach (Channel ch in seq.Channels)
					{
						score = theName.LevenshteinDistance(ch.Name);
						if (score > MINSCORE)
						{
							Array.Resize(ref matches, matchCount + 1);
							matches[matchCount].savedIdx = ch.SavedIndex;
							matches[matchCount].MemberType = MemberType.Channel;
							matches[matchCount].itemIdx = ch.Index;
							matches[matchCount].score = score;
							matchCount++;
						}
					}
				} // end if type is RGB Channel

				if (type == MemberType.Channel)
				{
					foreach (RGBchannel rgb in seq.RGBchannels)
					{
						score = theName.LevenshteinDistance(rgb.Name);
						if (score > MINSCORE)
						{
							Array.Resize(ref matches, matchCount + 1);
							matches[matchCount].savedIdx = rgb.SavedIndex;
							matches[matchCount].MemberType = MemberType.RGBchannel;
							matches[matchCount].itemIdx = rgb.Index;
							matches[matchCount].score = score;
							matchCount++;
						}
					}
				} // end if type is regular channel



				// Did we get ANY prequalified fuzzy matches?
				if (matchCount > 0)
				{
					for (int q = 0; q < matchCount; q++)
					{
						string myName = "";
						if (matches[q].MemberType == MemberType.Channel)
						{
							myName = seq.Channels[matches[q].itemIdx].Name;
						}
						if (matches[q].MemberType == MemberType.RGBchannel)
						{
							myName = seq.RGBchannels[matches[q].itemIdx].Name;
						}
						if (matches[q].MemberType == MemberType.ChannelGroup)
						{
							myName = seq.ChannelGroups[matches[q].itemIdx].Name;
						}
						// update the score using a FULL fuzzy match
						matches[q].score = theName.RankEquality(myName);
					}

					SortByScore(matches);
					// Is the first one past the minimum
					// NOTE: Minimum score for prequalification, and minimum score for final matching are NOT the same
					if (matches[0].score >= MINMATCH)
					{ 
						for (int q = 0; q < matchCount; q++)
						{
							if (matches[q].score >= MINMATCH)
							{
								Array.Resize(ref ret, q);
								ret[q] = matches[q].savedIdx;
							}
						} // end loop
					} // end any past minimum score
				} // end any prequalified fuzzies
			} // end single exact match
			return ret;
		} // end FindChannelNames

		private void SortByScore(match[] matches)
		{
			SortMatches(matches, 0, matches.Length);
		}

		private void SortMatches(match[] matches, int left, int right)
		{
			int i = left, j = right;
			double pivot = matches[(left + right) / 2].score;

			while (i <= j)
			{
				while (matches[i].score < pivot)
				{
					i++;
				}

				while (matches[j].score > pivot)
				{
					j--;
				}

				if (i <= j)
				{
					// Swap
					match tmp = matches[i];
					matches[i] = matches[j];
					matches[j] = tmp;

					i++;
					j--;
				}
			}

			// Recursive calls
			if (left < j)
			{
				SortMatches(matches, left, j);
			}

			if (i < right)
			{
				SortMatches(matches, i, right);
			}
		}

		/*
		private void SortMatches(match[] matches, int low, int high)
		{
			int pivot_loc = 0;

			if (low < high)
			{
				pivot_loc = PartitionMatches(matches, low, high);
			}
			SortMatches(matches, low, pivot_loc - 1);
			SortMatches(matches, pivot_loc + 1, high);
		}

		private int PartitionMatches(match[] matches, int low, int high)
		{
			double pivot = matches[high].score;
			int i = low - 1;

			for (int j = low; j < high - 1; j++)
			{
				if (matches[j].score <= pivot)
				{
					i++;
					SwapMatches(matches, i, j);
				}
			}
			SwapMatches(matches, i + 1, high);
			return i + 1;
		}

		private void SwapMatches(match[] matches, int a, int b)
		{
			match temp = matches[a];
			matches[a] = matches[b];
			matches[b] = temp;
		}
		*/

		private string SuggestedNewName(string originalName)
		{
			string p = Path.GetDirectoryName(originalName);
			string n = Path.GetFileNameWithoutExtension(originalName);
			string e = Path.GetExtension(originalName);
			string ty = DateTime.Now.Year.ToString();
			string ly = (DateTime.Now.Year - 1).ToString();
			n = n.Replace(ly, ty);
			ly = (DateTime.Now.Year - 2).ToString();
			n = n.Replace(ly, ty);
			int i = 1;
			string ret = p + n + " Remapped" + e;
			while(File.Exists(ret))
			{
				i++;
				ret = p + n + " Remapped (" + i.ToString() + ")" + e;
			}
			return ret;
		} // end SuggestedNewName

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
				if (Fyle.InvalidCharacterCount(arg) == 0) isFile++;
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
			int abortBatch = 0;

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
				batchTypes = BATCHnone;
				string ext = Path.GetExtension(file).ToLower();
				if (ext.CompareTo(".lcc") == 0)
				{
					if (File.Exists(file))
					{
						LoadMasterFile(file);
						batchTypes |= BATCHmaster;
					}
				}
				if (ext.CompareTo(".chmap") == 0)
				{
					if (File.Exists(file))
					{
						mapFile = file;
						batchTypes |= BATCHmap;
					}
				}
				if ((ext.CompareTo(".lms") == 0) ||
						(ext.CompareTo(".las") == 0))
				{
					Array.Resize(ref batch_fileList, batch_fileCount + 1);
					batch_fileList[batch_fileCount] = file;
					batch_fileCount++;
					batchTypes |= BATCHsources;

				}
				//	}
				//}
			}
			if (batch_fileCount > 1)
			{
				if (seqMaster.Channels.Count < 3)
				{
					if (File.Exists(masterFile))
					{
						LoadMasterFile(masterFile);
					}
				}
				if (seqMaster.Channels.Count < 3)
				{
					abortBatch = 1;
				}
				if (abortBatch == 0)
				{
					if (!File.Exists(mapFile))
					{
						abortBatch = 2;
					}
					if (abortBatch == 0)
					{
						batchMode = true;
						ProcessSourcesBatch(batch_fileList);
					}
				}


			}
			else
			{
				abortBatch = 3;
			}
			if (abortBatch > 0)
			{
				string msg = "The reqested files cannot be batch processed.\r\nReason: ";
				if (abortBatch == 1) msg += "No destination file was specified or has been set.";
				if (abortBatch == 2) msg += "No mapping file was specified or has been set.";
				if (abortBatch == 3) msg += "The requested batch of files contains no valid sequences.";
				DialogResult dd = MessageBox.Show(this, msg, "Batch Process Files", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		} // end ProcessDragDropFiles

		private int GetMapFileLineCount(string mapFile)
		{
			int lineCount = 0;
			string lineIn = "";
			StreamReader reader = new StreamReader(mapFile);
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
			}
			reader.Close();
			return lineCount;
		}

		private void ProcessSourcesBatch(string[] batchFilenames)
		{
			//if (batch_fileCount > 0)
			if (seqMaster.Members.Items.Count > 1)
			{
				ShowProgressBars(2);
				for (int f = 0; f < batch_fileCount; f++)
				{
					LoadSourceFile(batch_fileList[f]);
					if (File.Exists(mapFile))
					{
						txtMappingFile.Text = Path.GetFileNameWithoutExtension(mapFile);
						batch_fileNumber = f;
						LoadApplyMapFile(mapFile);
						string oldnm = Path.GetFileNameWithoutExtension(sourceFile);
						string newnm = SuggestedNewName(oldnm); // R we gettin here?
						string newfl = Path.GetDirectoryName(saveFile) + "\\";
						newfl += newnm;
						newfl += Path.GetExtension(sourceFile).ToLower();
						SaveNewMappedSequence(newfl);
					}
					else
					{
						f = batch_fileCount;
						break;
					}
				} // cmdSeqFileCount-- Batch Mode or Not
			}
			batchMode = false;
			ShowProgressBars(0);
			ImBusy(false);
			//string msg = "Batch mode is not supported... yet.\r\nLook for this feature in a future release (soon)!";
			//MessageBox.Show(this, msg, "Batch Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
		} // end ProcessSourcesBatch

		private void ArrangePanes(bool sourceNowOnRight)
		{
			const int lft = 15;
			const int rgt = 415;

			if (sourceNowOnRight)
			{
				lblSourceFile.Left = rgt;
				lblSourceTree.Left = rgt;
				txtSourceFile.Left = rgt;
				treeSource.Left = rgt;
				picPreviewSource.Left =rgt;
				lblSourceMappedTo.Left = rgt;
				btnBrowseSource.Left = 721;

				lblMasterFile.Left = lft;
				lblMasterTree.Left = lft;
				txtMasterFile.Left = lft;
				treeMaster.Left = lft;
				picPreviewMaster.Left = lft;
				lblMasterMappedTo.Left = lft;
				btnBrowseMaster.Left = 321;

				mnuSourceLeft.Checked = false;
				mnuSourceRight.Checked = true;
			}
			else
			{
				lblSourceFile.Left = lft;
				lblSourceTree.Left = lft;
				txtSourceFile.Left = lft;
				treeSource.Left = lft;
				picPreviewSource.Left = lft;
				lblSourceMappedTo.Left = lft;
				btnBrowseSource.Left = 321;

				lblMasterFile.Left = rgt;
				lblMasterTree.Left = rgt;
				txtMasterFile.Left = rgt;
				treeMaster.Left = rgt;
				picPreviewMaster.Left = rgt;
				lblMasterMappedTo.Left = rgt;
				btnBrowseMaster.Left = 721;

				mnuSourceLeft.Checked = true;
				mnuSourceRight.Checked = false;
			}

			sourceOnRight = sourceNowOnRight;
			Properties.Settings.Default.SourceOnRight = sourceOnRight;
			Properties.Settings.Default.Save();
		}

		public IMember FuzzyFindName(string theName, Sequence4 sequence, MemberType theType)
		{
			IMember ret = null;
			bool useFuzzy = Properties.Settings.Default.FuzzyUse;
			bool writeLog = Properties.Settings.Default.FuzzyWriteLog;

			string logFile = "";
			StreamWriter writer = new StreamWriter(Stream.Null);
			string lineOut = "";
			if (writeLog)
			{
				logFile = Fyle.GetAppTempFolder() + "Fuzzy.log";
				writer = new StreamWriter(logFile, true);
				writer.WriteLine("");
				lineOut = "Looking for     \"" + theName + "\" in ";
				lineOut += SeqEnums.MemberName(theType) + "s in ";
				lineOut += Path.GetFileName(sequence.info.filename);
				writer.WriteLine(lineOut);
			}

			if (sequence.Members.byName.ContainsKey(theName))
			{
				IMember r2 = sequence.Members.byName[theName];
				if (r2.MemberType == theType)
				{
					ret = r2;
					if (writeLog)
					{
						lineOut = "Exact Match Found \"" + ret.Name + "\" ";
						lineOut += "Saved Index=" + ret.SavedIndex.ToString();
						writer.WriteLine(lineOut);
					}
				}
			}
			// Didn't find it?
			if (ret != null)
			{
				if (useFuzzy)
				{
					//List<IMember> matchedMembers = null;
					List<IMember> matchedMembers = new List<IMember>();
					double[] scores = null;
					int[] SIs = null;
					int count = 0;
					long[] totTimes = null;
					int[] totCount = null;
					Array.Resize(ref totTimes, FuzzyFunctions.ALGORITHM_COUNT + 1);
					Array.Resize(ref totCount, FuzzyFunctions.ALGORITHM_COUNT + 1);
					double score;
					long finalAlgorithms = Properties.Settings.Default.FuzzyFinalAlgorithms;
					long prematchAlgorithm = Properties.Settings.Default.FuzzyPrematchAlgorithm;
					bool caseSensitive = Properties.Settings.Default.FuzzyCaseSensitive;
					double minPrematchScore = Properties.Settings.Default.FuzzyMinPrematch;
					double minFinalMatchScore = Properties.Settings.Default.FuzzyMinFinal;

					// Now perform all other requested algorithms
					if (writeLog)
					{
						lineOut = "Fuzzy Prematches with a minimum of " + minPrematchScore.ToString() + ":";
						writer.WriteLine(lineOut);
					}

					if (theType == MemberType.Channel)
					{
						foreach (IMember member in sequence.Channels)
						{
							// get a quick prematch score
							score = theName.RankEquality(member.Name, prematchAlgorithm);
							// fi the score is above the minimum PreMatch
							if (score * 100 > minPrematchScore)
							{
								// Increment count and save the SavedIndex
								// NOte: No need to save the PreMatch score
								count++;
								Array.Resize(ref SIs, count);
								SIs[count - 1] = member.SavedIndex;
								matchedMembers.Add(member);
								if (writeLog)
								{
									lineOut = score.ToString("0.0000") + " SI:";
									lineOut += member.SavedIndex.ToString().PadLeft(5);
									lineOut += "=\"" + member.Name + "\"";
									writer.WriteLine(lineOut);
								}
							} // end score exceeds minimum
						}
					}

					if (theType == MemberType.RGBchannel)
					{
						foreach (IMember member in sequence.RGBchannels)
						{
							// get a quick prematch score
							score = theName.RankEquality(member.Name, prematchAlgorithm);
							// fi the score is above the minimum PreMatch
							if (score * 100 > minPrematchScore)
							{
								// Increment count and save the SavedIndex
								// NOte: No need to save the PreMatch score
								count++;
								Array.Resize(ref SIs, count);
								SIs[count - 1] = member.SavedIndex;
								matchedMembers.Add(member);
								if (writeLog)
								{
									lineOut = score.ToString("0.0000") + " SI:";
									lineOut += member.SavedIndex.ToString().PadLeft(5);
									lineOut += "=\"" + member.Name + "\"";
									writer.WriteLine(lineOut);
								}
							} // end score exceeds minimum
						}
					}

					if (writeLog)
					{
						lineOut = count.ToString() + " found.";
						writer.WriteLine(lineOut);
					}
					if (count > 0)
					{
						// Resize scores array to final size
						Array.Resize(ref scores, count);
						// Loop thru qualifying prematches
						for (int i = 0; i < count; i++)
						{
							// Get the ID, perform a more thorough final fuzzy match, and save the score
							IMember member = sequence.Members.bySavedIndex[SIs[i]];
							//score = theName.RankEquality(member.Name, finalAlgorithms);

							string source = theName;
							string target = member.Name;
							double avgScore = 0;
							List<double> comparisonResults = new List<double>();
							int methodCount = 0;
							double runningTotal = 0D;
							double scoreOut = 0D;
							double minScore = 0.4D;
							double maxScore = 0.99D;
							double thisScore = 0.5D;
							double WLscore = 0.8D;
							bool valid = false;

							lineOut = "<";
							if ((finalAlgorithms & FuzzyFunctions.USE_CASEINSENSITIVE) > 0)
							{
								source = source.Capitalize();
								target = target.Capitalize();
								lineOut += "Not ";
							}
							if (writeLog)
							{
								lineOut += "Case Sensitive>";
								writer.Write(lineOut);
							}

							// Use Weighted Levenshtein to set a baseline
							//  for what is an acceptable score from other algorithms
							WLscore = source.WeightedLevenshteinSimilarity(target);
							maxScore = WLscore + (1.0D - WLscore) * 0.75D;
							minScore = WLscore / 3.0D;
							minScore = Math.Max(minScore, 0.4D);

							if (writeLog)
							{
								lineOut = "Baseline Score (Weighted Levenshtein: ";
								lineOut += WLscore.ToString("0.0000 ");
								//writer.WriteLine(lineOut);
								lineOut += "   Min: " + minScore.ToString("0.0000") + "   Max: " + maxScore.ToString("0.0000");
								writer.WriteLine(lineOut);
							}



							for (int al =1; al <= FuzzyFunctions.ALGORITHM_COUNT; al++)
							{
								long mask = (long)Math.Pow(2D, (double) al) / 2;
								//if ((finalAlgorithms & mask) > 0)
								//{
									var watch = System.Diagnostics.Stopwatch.StartNew();
									thisScore = source.RankEquality(target, mask);
									watch.Stop();
									long elapsedMs = watch.ElapsedMilliseconds;
									totTimes[al] += elapsedMs;
									totCount[al]++;
									valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
									if (writeLog)
									{
										lineOut = WankLine(thisScore, valid, mask, elapsedMs);
										writer.WriteLine(lineOut);
									}
								//}
							}
							if (methodCount > 0)
							{
								avgScore = runningTotal / methodCount;
								if (writeLog)
								{
									lineOut = avgScore.ToString("0.0000 ") + "Average";
									writer.WriteLine(lineOut);
								}
							}
							scores[i] = avgScore;
						}
						// Now sort the final scores (and the SavedIndexes along with them)
						Array.Sort(scores, SIs);
						if (writeLog)
						{
							lineOut = "Final Matches:";
							writer.WriteLine(lineOut);
							for (int f = 0; f < count; f++)
							{
								lineOut = scores[f].ToString("0.0000") + " SI:";
								IMember y = sequence.Members.bySavedIndex[SIs[f]];
								lineOut += y.SavedIndex.ToString().PadLeft(5);
								lineOut += "=\"" + y.Name + "\"";
								writer.WriteLine(lineOut);
							}
						} // end writelog

						// Is the best/highest above the required minimum Final Match score?
						if (scores[count - 1] * 100 > minFinalMatchScore)
						{
							// Return the ID with the best qualifying final match
							ret = sequence.Members.bySavedIndex[SIs[count - 1]];
							// Get name just for debugging
							string msg = theName + " ~= " + ret.Name;
							if (writeLog)
							{
								lineOut = "Best Match Is:";
								writer.WriteLine(lineOut);
								lineOut = scores[count - 1].ToString("0.0000") + " SI:";
								IMember y = sequence.Members.bySavedIndex[SIs[count - 1]];
								lineOut += y.SavedIndex.ToString().PadLeft(5);
								lineOut += "=\"" + y.Name + "\"";
								writer.WriteLine(lineOut);
							}
						}
						else
						{
							if (writeLog)
							{
								lineOut = "No Final Matches meet the minimum score of " + minFinalMatchScore.ToString();
								writer.WriteLine(lineOut);
							}
						} // end score beats min final (or not)
					} // end if count; prematches
				} // end if useFuzzy
			} // end if exact match found, or not
			if (writeLog)
			{
				writer.Close();
				//System.Diagnostics.Process.Start(logFile);
			}
			return ret;
		}

		private static bool WankEquality(double thisScore, double minScore, double maxScore, ref double runningTotal, ref int methodCount)
		{
			bool valid = false;
			if ((thisScore >= minScore) && (thisScore <= maxScore)) valid = true;
			if (valid)
			{
				runningTotal += thisScore;
				methodCount++;
			}
			return valid;
		}

		private static string WankLine(double theScore, bool isValid, long algorithm, long elapsed)
		{
			string lineOut = theScore.ToString("0.0000 ") + FuzzyFunctions.AlgorithmNames(algorithm);
			if (!isValid) lineOut += " Not";
			lineOut += " Valid  ";
			lineOut += elapsed.ToString() +"μs";

			return lineOut;
		}

		public static IMember FindName(string theName, List<IMember> IDs)
		{
			IMember ret = null;
			int idx = BinarySearch(theName, IDs);
			if (idx > utils.UNDEFINED)
			{
				ret = IDs[idx];
			}
			return ret;
		}

		public static int BinarySearch(string theName, List<IMember> IDs)
		{
			return BinarySearch3(theName, IDs, 0, IDs.Count - 1);
		}

		public static int BinarySearch3(string theName, List<IMember> IDs, int start, int end)
		{
			int index = -1;
			int mid = (start + end) / 2;
			if ((theName.CompareTo(IDs[start].Name) > 0) && (theName.CompareTo(IDs[end].Name) < 0))
			{
				int cmp = theName.CompareTo(IDs[mid].Name);
				if (cmp < 0)
					BinarySearch3(theName, IDs, start, mid);
				else if (cmp > 0)
					BinarySearch3(theName, IDs, mid + 1, end);
				else if (cmp == 0)
					index = mid;
				//if (index != -1)
					//Console.WriteLine("got it at " + index);
			}
			return index;
		}

		public IMember FindByName(string theName, Membership children, MemberType PartType, long preAlgorithm, double minPreMatch, long finalAlgorithms, double minFinalMatch, bool ignoreSelected)
		{
			IMember ret = null;
			if (children.byName.TryGetValue(theName, out ret))
			{
				// Found the name, is the type correct?
				if (ret.MemberType != PartType)
				{
					ret = null;
				}
			}
			else
			{
				if (finalAlgorithms > 0)
				{
					ret = FuzzyFind(theName, children, preAlgorithm, minPreMatch, finalAlgorithms, minFinalMatch, ignoreSelected);
				}
			}

			return ret;
		}

		public IMember FindByName(string theName, Membership children, MemberType PartType)
		{
			IMember ret = null;
			ret = FindByName(theName, children, PartType, 0, 0, 0, 0, false);
			return ret;
		}

		public static IMember FindByName(string theName, Membership children)
		{
			IMember ret = null;
			int idx = BinarySearch(theName, children.Items);
			if (idx > utils.UNDEFINED)
			{
				ret = children.Items[idx];
			}
			return ret;
		}

		public IMember FuzzyFind(string theName, Membership children, long preAlgorithm, double minPreMatch, long finalAlgorithms, double minFinalMatch, bool ignoreSelected)
		{
			IMember ret = null;
			double[] scores = null;
			int[] SIs = null;
			int count = 0;
			double score;

			// Go thru all objects
			foreach (IMember child in children.Items)
			{
				if ((!child.Selected) || (!ignoreSelected))
				{
					// get a quick prematch score
					score = theName.RankEquality(child.Name, preAlgorithm);
					// fi the score is above the minimum PreMatch
					if (score > minPreMatch)
					{
						// Increment count and save the SavedIndex
						// NOte: No need to save the PreMatch score
						count++;
						Array.Resize(ref SIs, count);
						SIs[count - 1] = child.SavedIndex;
					}
				}
			}
			// Resize scores array to final size
			if (count > 0)
			{
				Array.Resize(ref scores, count);
				// Loop thru qualifying prematches
				for (int i = 0; i < count; i++)
				{
					// Get the ID, perform a more thorough final fuzzy match, and save the score
					IMember child = children.bySavedIndex[SIs[i]];
					score = theName.RankEquality(child.Name, finalAlgorithms);
					scores[i] = score;
				}
				// Now sort the final scores (and the SavedIndexes along with them)
				Array.Sort(scores, SIs);
				// Is the best/highest above the required minimum Final Match score?
				if (scores[count - 1] > minFinalMatch)
				{
					// Return the ID with the best qualifying final match
					ret = children.bySavedIndex[SIs[count - 1]];
					// Get Name just for debugging
					string msg = theName + " ~= " + ret.Name;
				}
			}
			return ret;
		}

		private int LeftBushesChildCount()
		{
			int ret = 0;
			for (int g = 0; g < seqMaster.ChannelGroups.Count; g++)
			{
				int p = seqMaster.ChannelGroups[g].Name.IndexOf("eft Bushes");
				if (p > 0)
				{
					ret = seqMaster.ChannelGroups[g].Members.Count;
					g = seqMaster.ChannelGroups.Count; // Force exit of for loop
				}
			}
			return ret;

		}


	} // end partial class frmRemapper
} // end namespace UtilORama4
