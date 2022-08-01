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
using LOR4;
using FileHelper;
using FuzzORama;
using Syncfusion.Windows.Forms.Tools;

namespace UtilORama4
{
  partial class frmMapper //: Form
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
    //private readonly Color COLOR_FR_UnselUnhigh = Color.FromName("Black");
    //private readonly Color COLOR_BK_UnselUnhigh = Color.FromName("White");
    //private readonly Color COLOR_FR_SelUnhigh = Color.FromName("White");
    //private readonly Color COLOR_BK_SelUnhigh = Color.FromName("DarkBlue");
    //private readonly Color COLOR_FR_UnselHigh = Color.FromName("White");
    //private readonly Color COLOR_BK_UnselHigh = Color.FromName("Purple");
    //private readonly Color COLOR_FR_SelHigh = Color.FromName("Yellow");
    //private readonly Color COLOR_BK_SelHigh = Color.FromName("DarkViolet");  // Note, 'Dark Violet' is actually lighter than 'Purple'
    private const string xmlInfo = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"no\"?>";
    private const string TABLEchMap = "channelMap";
    private const string TABLEfiles = "files";
    private const string FIELDdestFile = "destFile";
    private const string FIELDsourceFile = "sourceFile";
    private const string TABLEchannels = "channels";
    private const string FIELDdestChannel = "destChannel";
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
    private int destMapLevel = 0;
    private int sourceMapLevel = 0;
    //public Color sourceHighlightColor = Color.FromArgb(255, 128, 128);
    //public Color destHighlightColor = Color.FromArgb(128, 255, 128);

    // These are used by MapList so need to be public
    public LOR4Sequence seqSource = new LOR4Sequence();
    public LOR4Sequence seqDest = new LOR4Sequence();
    //public iLOR4Member[] mapDestToSource = null; // Array indexed by Destination.SavedIndex, elements contain Source Members
    //public List<iLOR4Member>[] mapSourceToDest = null; // Array indexed by Source.SavedIindex, elements are Lists of Destination Members
    public int mappedMemberCount = 0;
    public int mappedChannelCount = 0;
    //public int[] mappedSIs; // Note: Redundant!  Done only for debugging purposes.
    //public List<TreeNodeAdv>[] sourceNodesBySI = null; // new List<TreeNodeAdv>();
    //public List<TreeNodeAdv>[] destNodesBySI = null; // new List<TreeNodeAdv>();
    public string destFile = "";
    public string sourceFile = "";
    public string mapFile = "";
    private int mapFileLineCount = 0;
    private string saveFile = "";

    //private List<iLOR4Member> lastHighlightedDestMembers = new List<iLOR4Member>();
    //private iLOR4Member lastHighlightedSourceMember = null;
    private iLOR4Member currentDestMember = null;
    private iLOR4Member currentSourceMember = null;
    private bool lastSelectionWasDest = false;
    private bool lastSelectionWasSource = false;
    private int mappedSourceChannelsCount = 0;
    private int mappedSourceRGBChannelsCount = 0;
    private int mappedSourceGroupsCount = 0;
    private int mappedSourceTracksCount = 0;
    private int mappedDestChannelsCount = 0;
    private int mappedDestRGBChannelsCount = 0;
    private int mappedDestGroupsCount = 0;
    private int mappedDestTracksCount = 0;

    private string basePath = "";
    private string SeqFolder = "";
    private TreeNodeAdv selectedSourceNode = null;
    private TreeNodeAdv selectedDestNode = null;
    private iLOR4Member selectedSourceMember = null;
    private iLOR4Member selectedDestMember = null;
    //private int sourceSI = LOR4Admin.UNDEFINED;
    //private int destSI = LOR4Admin.UNDEFINED;
    // int activeList = 0;
    public string statMsg = "Hello World!";
    //private Assembly assy = this.GetType().Assembly;
    //ResourceManager resources = new ResourceManager("Resources.Strings", assy);

    public Color ColorUnselectedBG = Color.White; // Black on White
    public Color ColorUnselectedFG = Color.Black;
    public Color ColorDestSelectedBG = Color.FromArgb(0, 192, 0); // Dark Green
    public Color ColorDestSelectedFG = Color.White;
    public Color ColorSourceSelectedBG = Color.FromArgb(192, 0, 0); // Dark Red
    public Color ColorSourceSelectedFG = Color.White;
    public Color ColorMappedSourceBG = Color.FromArgb(128, 255, 128); // Light Green
    public Color ColorMappedSourceFG = Color.Black;
    public Color ColorMappedDestBG = Color.FromArgb(255, 128, 128); // Light Red
    public Color ColorMappedDestFG = Color.Black;
    public Color ColorHighlightBG = Color.FromArgb(0, 0, 128); // Dark Blue
    public Color ColorHighlightFG = Color.FromArgb(255, 255, 128); // LIght Yellow		





    // Used only at load time to restore previous size/position
    //private int miLoadHeight = 400;
    //private int miLoadWidth = 620;
    //private int miLoadLeft = 0;
    //private int miLoadTop = 0;

    /*
	private class ChanInfo
	{
		public MemberType chType = LOR4MemberType.None;
		public int chIndex = 0;
		public int SavedIndex = LOR4Admin.UNDEFINED;
		public int mapCount = 0;
		//public int[] mapChIndexes;
		//public int[] mapSavedIndexes;
		public int nodeIndex = LOR4Admin.UNDEFINED;
	}
	*/

    private int nodeIndex = LOR4Admin.UNDEFINED;
    // Note Destination->Source mappings are a 1:Many relationship.
    // A Destination channel can map to only one Source channel
    // But a Source channel may map to more than one Destination channel

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

    private struct match
    {
      public double score;
      public int savedIdx;
      public int itemIdx;
      public LOR4MemberType MemberType;
    }

    #endregion

    #region File Operations
    private void BrowseSourceFile()
    {
      string initDir = LOR4Admin.DefaultSequencesPath;
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


      dlgFileOpen.Filter = LOR4Admin.FILTER_SEQ;
      dlgFileOpen.DefaultExt = LOR4Admin.EXT_LMS;
      dlgFileOpen.InitialDirectory = initDir;
      dlgFileOpen.FileName = initFile;
      dlgFileOpen.CheckFileExists = true;
      dlgFileOpen.CheckPathExists = true;
      dlgFileOpen.Multiselect = false;
      dlgFileOpen.Title = "Open Sequence...";
      DialogResult result = dlgFileOpen.ShowDialog();

      pnlAll.Enabled = false;
      if (result == DialogResult.OK)
      {
        ImBusy(true);
        int err = LoadSourceFile(dlgFileOpen.FileName);
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
        // Search for any sort of Beats, Song Parts, Tune-O-Rama, or Vamperizer LOR4Track
        // Default = not found
        beatTracks = 0;
        // LOR4Loop thru tracks checking names
        for (int t = 1; t < seqSource.Tracks.Count; t++)
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
        ///txtSourceFile.Text = Fyle.ShortenLongPath(sourceFile, 80);
        this.Text = "Map-O-Rama - " + Path.GetFileName(sourceFile);
        FileInfo fi = new FileInfo(sourceChannelFile);
        userSettings.BasePath = fi.DirectoryName;
        userSettings.LastSourceFile = sourceFile;
        userSettings.Save();
        TreeUtils.TreeFillChannels(treeSource, seqSource, false, false);
        mappedMemberCount = 0;
        for (int i = 0; i < seqSource.Channels.Count; i++)
        {
          seqSource.Channels[i].Tag = new List<iLOR4Member>();
        }
        for (int i = 0; i < seqSource.RGBchannels.Count; i++)
        {
          seqSource.RGBchannels[i].Tag = new List<iLOR4Member>();
        }
        for (int i = 0; i < seqSource.ChannelGroups.Count; i++)
        {
          seqSource.ChannelGroups[i].Tag = new List<iLOR4Member>();
        }
        for (int i = 0; i < seqSource.CosmicDevices.Count; i++)
        {
          seqSource.CosmicDevices[i].Tag = new List<iLOR4Member>();
        }
        for (int i = 1; i < seqSource.Tracks.Count; i++)
        {
          seqSource.Tracks[i].Tag = new List<iLOR4Member>();
        }
        for (int i = 0; i < seqSource.TimingGrids.Count; i++)
        {
          seqSource.TimingGrids[i].Tag = new List<iLOR4Member>();
        }
      }
      ImBusy(false);
      return err;
    }
    private void BrowseDestFile()
    {

      string initDir = LOR4Admin.DefaultSequencesPath;
      string initFile = "";
      if (destFile.Length > 4)
      {
        string ldir = Path.GetDirectoryName(destFile);
        if (Directory.Exists(ldir))
        {
          initDir = ldir;
          if (File.Exists(destFile))
          {
            initFile = Path.GetFileName(destFile);
          }
        }
      }


      dlgFileOpen.Filter = LOR4Admin.FILTER_CFG;
      dlgFileOpen.DefaultExt = LOR4Admin.EXT_LCC;
      dlgFileOpen.InitialDirectory = initDir;
      dlgFileOpen.FileName = initFile;
      dlgFileOpen.CheckFileExists = true;
      dlgFileOpen.CheckPathExists = true;
      dlgFileOpen.Multiselect = false;
      dlgFileOpen.Title = "Select Destination Sequence File...";
      DialogResult result = dlgFileOpen.ShowDialog();

      //pnlAll.Enabled = false;
      if (result == DialogResult.OK)
      {
        destFile = dlgFileOpen.FileName;

        int err = LoadDestFile(dlgFileOpen.FileName);
        if (err == 0)
        {
          mruDests.UseFile(dlgFileOpen.FileName);
        }

      } // end if (result = DialogResult.OK)
        //pnlAll.Enabled = true;
      if (treeSource.Nodes.Count > 0)
      {
        //btnSummary.Enabled = true;
      }

    }
    private int LoadDestFile(string destFile)
    {
      ImBusy(true);
      int err = seqDest.ReadSequenceFile(destFile);
      if (err < 100)  // Error numbers below 100 are warnings, above 100 are fatal
      {
        for (int w = 0; w < seqDest.AllMembers.ByName.Count; w++)
        {
          //Console.WriteLine(seqDest.AllMembers.ByName[w].Name);
        }





        destFile = destFile;
        FileInfo fi = new FileInfo(destFile);
        mruDests.UseFile(destFile);
        mruDests.FillFileComboBox(cboDestFile);


        TreeUtils.TreeFillChannels(treeDest, seqDest, false, false);

        // Erase any existing mappings, and create a new blank one of proper size
        // Erase any existing mappings, and create a new blank one of proper size
        //mapDestToSource = null;
        //mappedSIs = null;
        //mapSourceToDest = null;
        mappedMemberCount = 0;
        //if (seqSource.Channels.Count > 0)
        //{
        //Array.Resize(ref mapSourceToDest, seqSource.AllMembers.AllCount);
        //for (int i = 0; i < mapSourceToDest.Length; i++)
        //{
        //mapSourceToDest[i] = new List<iLOR4Member>();
        //}
        //}
        //Array.Resize(ref mapDestToSource, seqDest.AllMembers.AllCount);
        //Array.Resize(ref mappedSIs, seqDest.AllMembers.AllCount);
        //for (int i = 0; i < mapDestToSource.Length; i++)
        //{
        //mapDestToSource[i] = null;
        //mappedSIs[i] = LOR4Admin.UNDEFINED;
        //}
        string txt = "AutoLaunch " + cboDestFile.Text;
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
      LOR4Sequence seqNew = seqDest;

      seqNew.info = seqSource.info;
      seqNew.info.filename = newSeqFileName;
      seqNew.LOR4SequenceType = seqSource.LOR4SequenceType;
      seqNew.Centiseconds = seqSource.Centiseconds;
      seqNew.animation = seqSource.animation;
      seqNew.videoUsage = seqSource.videoUsage;
      int newcs = Math.Max(seqSource.Centiseconds, seqDest.Centiseconds);

      //string msg = "GLB";
      //msg += LeftBushesChildCount().ToString();
      //lblDebug.Text = msg;

      //seqNew.ClearAllEffects();

      //seqNew.TimingGrids = new List<LOR4Timings>();
      foreach (LOR4Timings sourceGrid in seqSource.TimingGrids)
      {
        LOR4Timings newGrid = null;
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
          newGrid = seqNew.CreateNewTimingGrid(sourceGrid.LineOut());
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


      for (int c = 0; c < seqDest.Channels.Count; c++)
      {
        LOR4Channel destChannel = seqDest.Channels[c];
        if (destChannel.MapTo != null)
        {
          LOR4Channel sourceChannel = (LOR4Channel)destChannel.MapTo;
          if (sourceChannel.effects.Count > 0)
          {
            if (destChannel.effects.Count > 0)
            {
              destChannel.effects.Clear();
            }
            destChannel.CopyEffects(sourceChannel.effects, false);
            destChannel.Centiseconds = newcs;
          } // end if effects.Count
        } // end if source obj is channel
      }

      seqNew.CentiFix(newcs);

      int err = seqNew.WriteSequenceFile_DisplayOrder(newSeqFileName);
      //seqNew.ClearAllEffects();

      saveFile = newSeqFileName;
      userSettings.LastSaveFile = saveFile;
      userSettings.Save();

      ImBusy(false);
      return err;
    }
    private void SaveMap()
    {
      dlgFileSave.DefaultExt = LOR4Admin.EXT_CHMAP;
      dlgFileSave.Filter = LOR4Admin.FILTER_CHMAP;
      dlgFileSave.FilterIndex = 0;
      string initDir = LOR4Admin.DefaultChannelConfigsPath;
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
      dlgFileSave.FileName = initFile;
      dlgFileSave.InitialDirectory = initDir;
      dlgFileSave.OverwritePrompt = false;
      dlgFileSave.CheckPathExists = true;
      dlgFileSave.Title = "Save Channel Map As...";

      DialogResult dr = dlgFileSave.ShowDialog();
      if (dr == DialogResult.OK)
      {
        DialogResult ow = Fyle.SafeOverwriteFile(dlgFileSave.FileName);
        if (ow == DialogResult.Yes)
        {

          string mapTemp = System.IO.Path.GetTempPath();
          mapTemp += Path.GetFileName(dlgFileSave.FileName);
          int mapErr = SaveMap(mapTemp);
          if (mapErr == 0)
          {
            mapFile = dlgFileSave.FileName;
            if (File.Exists(mapFile))
            {
              //TODO: Add Exception Catch
              File.Delete(mapFile);
            }
            File.Copy(mapTemp, mapFile);
            File.Delete(mapTemp);
            dirtyMap = false;
            //btnSaveMap.Enabled = dirtyMap;
            mruMaps.UseFile(mapFile);
            mruMaps.FillFileComboBox(cboMappingFile)

          } // end no errors saving map
        }
      } // end dialog result = OK
    } // end btnSaveMap Click event
    private void BrowseForMap()
    {
      dlgFileOpen.DefaultExt = LOR4Admin.EXT_CHMAP;
      dlgFileOpen.Filter = LOR4Admin.FILTER_CHMAP;
      dlgFileOpen.FilterIndex = 0;
      string initDir = LOR4Admin.DefaultChannelConfigsPath;
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
      dlgFileOpen.FileName = initFile;
      dlgFileOpen.InitialDirectory = initDir;
      dlgFileOpen.CheckPathExists = true;
      dlgFileOpen.Title = "Load-Apply Channel Map..";

      DialogResult dr = dlgFileOpen.ShowDialog();
      if (dr == DialogResult.OK)
      {
        mapFile = dlgFileOpen.FileName;
        mruMaps.UseFile(dlgFileOpen.FileName);
        mruMaps.FillFileComboBox(cboMappingFile);
        if (seqDest.AllMembers.Items.Count > 1)
        {
          if (seqSource.AllMembers.Items.Count > 1)
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
      StringBuilder lineOut = new StringBuilder(); ; // line to be written out, gets modified if necessary
                                                     //int pos1 = LOR4Admin.UNDEFINED; // positions of certain key text in the line

      lineOut.Append("Util-O-Rama Channel Map  created ");
      lineOut.Append(nowtime);
      writer.WriteLine(lineOut.ToString());
      lineOut.Clear();
      lineOut.Append("Destination Type, Name, SavedIndex, Source Name, SavedIndex");
      writer.WriteLine(lineOut.ToString());
      lineOut.Clear();

      lineOut.Append("[Mapped Channels]");
      writer.WriteLine(lineOut.ToString());
      lineOut.Clear();
      for (int i = 0; i < seqDest.Channels.Count; i++)
      {
        LOR4Channel destMember = seqDest.Channels[i];
        if (destMember.MapTo != null)
        {
          lineOut.Append(LOR4SeqEnums.MemberName(destMember.MemberType));
          lineOut.Append(",");
          lineOut.Append(destMember.Name);
          lineOut.Append(",");
          lineOut.Append(destMember.SavedIndex.ToString());
          lineOut.Append(",");
          iLOR4Member sourceMember = destMember.MapTo;
          lineOut.Append(sourceMember.Name);
          lineOut.Append(",");
          lineOut.Append(sourceMember.SavedIndex.ToString());
          writer.WriteLine(lineOut.ToString());
          lineOut.Clear();
        }
      }

      lineOut.Append("[Mapped RGBChannels]");
      writer.WriteLine(lineOut.ToString());
      lineOut.Clear();
      for (int i = 0; i < seqDest.RGBchannels.Count; i++)
      {
        LOR4RGBChannel destMember = seqDest.RGBchannels[i];
        if (destMember.MapTo != null)
        {
          lineOut.Append(LOR4SeqEnums.MemberName(destMember.MemberType));
          lineOut.Append(",");
          lineOut.Append(destMember.Name);
          lineOut.Append(",");
          lineOut.Append(destMember.SavedIndex.ToString());
          lineOut.Append(",");
          iLOR4Member sourceMember = destMember.MapTo;
          lineOut.Append(sourceMember.Name);
          lineOut.Append(",");
          lineOut.Append(sourceMember.SavedIndex.ToString());
          writer.WriteLine(lineOut.ToString());
          lineOut.Clear();
        }
      }

      lineOut.Append("[Mapped ChannelGroups]");
      writer.WriteLine(lineOut.ToString());
      lineOut.Clear();
      for (int i = 0; i < seqDest.ChannelGroups.Count; i++)
      {
        LOR4ChannelGroup destMember = seqDest.ChannelGroups[i];
        if (destMember.MapTo != null)
        {
          lineOut.Append(LOR4SeqEnums.MemberName(destMember.MemberType));
          lineOut.Append(",");
          lineOut.Append(destMember.Name);
          lineOut.Append(",");
          lineOut.Append(destMember.SavedIndex.ToString());
          lineOut.Append(",");
          iLOR4Member sourceMember = destMember.MapTo;
          lineOut.Append(sourceMember.Name);
          lineOut.Append(",");
          lineOut.Append(sourceMember.SavedIndex.ToString());
          writer.WriteLine(lineOut.ToString());
          lineOut.Clear();
        }
      }

      lineOut.Append("[Mapped CosmicDevices]");
      writer.WriteLine(lineOut.ToString());
      lineOut.Clear();
      for (int i = 0; i < seqDest.CosmicDevices.Count; i++)
      {
        LOR4Cosmic destMember = seqDest.CosmicDevices[i];
        if (destMember.MapTo != null)
        {
          lineOut.Append(LOR4SeqEnums.MemberName(destMember.MemberType));
          lineOut.Append(",");
          lineOut.Append(destMember.Name);
          lineOut.Append(",");
          lineOut.Append(destMember.SavedIndex.ToString());
          lineOut.Append(",");
          iLOR4Member sourceMember = destMember.MapTo;
          lineOut.Append(sourceMember.Name);
          lineOut.Append(",");
          lineOut.Append(sourceMember.SavedIndex.ToString());
          writer.WriteLine(lineOut.ToString());
          lineOut.Clear();
        }
      }

      lineOut.Append("[Mapped Tracks]");
      writer.WriteLine(lineOut.ToString());
      lineOut.Clear();
      for (int i = 1; i < seqDest.Tracks.Count; i++)
      {
        LOR4Track destMember = seqDest.Tracks[i];
        if (destMember.MapTo != null)
        {
          lineOut.Append(LOR4SeqEnums.MemberName(destMember.MemberType));
          lineOut.Append(",");
          lineOut.Append(destMember.Name);
          lineOut.Append(",");
          lineOut.Append(destMember.SavedIndex.ToString());
          lineOut.Append(",");
          iLOR4Member sourceMember = destMember.MapTo;
          lineOut.Append(sourceMember.Name);
          lineOut.Append(",");
          lineOut.Append(sourceMember.SavedIndex.ToString());
          writer.WriteLine(lineOut.ToString());
          lineOut.Clear();
        }
      }

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
      //int mq = seqDest.Tracks.Count + seqDest.ChannelGroups.Count + seqDest.Channels.Count;
      //mq -= seqDest.RGBchannels.Count * 2;
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
      string destName = "";
      string theType = "";
      string temp = "";
      //int tempNum;
      int[] foundChannels;
      int sourceSI = LOR4Admin.UNDEFINED;
      int destSI = LOR4Admin.UNDEFINED;
      //LOR4MemberType sourceType = LOR4MemberType.None;
      LOR4MemberType destType = LOR4MemberType.None;
      iLOR4Member destMember = null;
      iLOR4Member sourceMember = null;
      iLOR4Member foundID = null;
      string mfile = "";
      string sfile = "";
      long finalAlgorithms = Properties.Settings.Default.FuzzyFinalAlgorithms;
      double minPreMatch = Properties.Settings.Default.FuzzyMinPrematch;
      double minFinalMatch = Properties.Settings.Default.FuzzyMinFinal;
      long preAlgorithm = Properties.Settings.Default.FuzzyPrematchAlgorithm;
      logFile1 = logHomeDir + "Apply" + batch_fileNumber.ToString() + ".log";
      logWriter1 = new StreamWriter(logFile1);
      log1Open = true;
      logMsg = "Destination File: " + seqDest.info.filename;
      logWriter1.WriteLine(logMsg);
      logMsg = "Source File: " + seqSource.info.filename;
      logWriter1.WriteLine(logMsg);
      logMsg = "Map File: " + fileName;
      logWriter1.WriteLine(logMsg);
      mappedMemberCount = 0;
      int li = 0;
      int errorStatus = 0;
      Dictionary<string, iLOR4Member> destChanNames = new Dictionary<string, iLOR4Member>();
      Dictionary<string, iLOR4Member> destRGBNames = new Dictionary<string, iLOR4Member>();
      Dictionary<string, iLOR4Member> destGroupNames = new Dictionary<string, iLOR4Member>();
      Dictionary<string, iLOR4Member> destTrackNames = new Dictionary<string, iLOR4Member>();
      Dictionary<string, iLOR4Member> sourceChanNames = new Dictionary<string, iLOR4Member>();
      Dictionary<string, iLOR4Member> sourceRGBNames = new Dictionary<string, iLOR4Member>();
      Dictionary<string, iLOR4Member> sourceGroupNames = new Dictionary<string, iLOR4Member>();
      Dictionary<string, iLOR4Member> sourceTrackNames = new Dictionary<string, iLOR4Member>();

      //string msg = "GLB";
      //msg += LeftBushesChildCount().ToString();
      //lblDebug.Text = msg;

      if (batchMode)
      {
        ShowProgressBars(2);
      }
      else
      {
        ShowProgressBars(1);
      }
      ClearAllMappings();


      // Create sorted dictionaries, by name, lower case;
      // Destination (Master)
      //seqDest.SortOrder = byName;
      // Channels
      for (int i = 0; i < seqDest.Channels.Count; i++)
      {
        string nam = seqDest.Channels[i].Name.ToLower();
        if (nam.Length < 1)
        { nam = "Channel " + seqDest.Channels[i].Index.ToString("0000"); }
        while (destChanNames.ContainsKey(nam))
        { nam += seqDest.Channels[i].SavedIndex.ToString(); }
        destChanNames.Add(nam, seqDest.Channels[i]);
      }
      // RGB Channels
      for (int i = 0; i < seqDest.RGBchannels.Count; i++)
      {
        string nam = seqDest.RGBchannels[i].Name.ToLower();
        if (nam.Length < 1)
        { nam = "RGBChannel " + seqDest.RGBchannels[i].Index.ToString("0000"); }
        while (destRGBNames.ContainsKey(nam))
        { nam += seqDest.RGBchannels[i].SavedIndex.ToString(); }
        destRGBNames.Add(nam, seqDest.RGBchannels[i]);
      }
      // Channel Groups
      for (int i = 0; i < seqDest.ChannelGroups.Count; i++)
      {
        string nam = seqDest.ChannelGroups[i].Name.ToLower();
        if (nam.Length < 1)
        { nam = "ChannelGroup " + seqDest.ChannelGroups[i].Index.ToString("0000"); }
        while (destGroupNames.ContainsKey(nam))
        { nam += seqDest.ChannelGroups[i].SavedIndex.ToString(); }
        destGroupNames.Add(nam, seqDest.ChannelGroups[i]);
      }
      //TODO: Cosmic Devices
      // Tracks
      for (int i = 0; i < seqDest.Tracks.Count; i++)
      {
        string nam = seqDest.Tracks[i].Name.ToLower();
        if (nam.Length < 1)
        { nam = "Track " + seqDest.Tracks[i].Index.ToString("0000"); }
        while (destTrackNames.ContainsKey(nam))
        { nam += seqDest.Tracks[i].TrackNumber.ToString(); }
        destTrackNames.Add(nam, seqDest.Tracks[i]);
      }
      // Source Sequence
      // Channels
      for (int i = 0; i < seqSource.Channels.Count; i++)
      {
        string nam = seqSource.Channels[i].Name.ToLower();
        if (nam.Length < 1)
        { nam = "Channel " + seqSource.Channels[i].Index.ToString("0000"); }
        while (sourceChanNames.ContainsKey(nam))
        { nam += seqSource.Channels[i].SavedIndex.ToString(); }
        sourceChanNames.Add(nam, seqSource.Channels[i]);
      }
      // RGB Channels
      for (int i = 0; i < seqSource.RGBchannels.Count; i++)
      {
        string nam = seqSource.RGBchannels[i].Name.ToLower();
        if (nam.Length < 1)
        { nam = "RGBChannel " + seqSource.RGBchannels[i].Index.ToString("0000"); }
        while (sourceRGBNames.ContainsKey(nam))
        { nam += seqSource.RGBchannels[i].SavedIndex.ToString(); }
        sourceRGBNames.Add(nam, seqSource.RGBchannels[i]);
      }
      // Channel Groups
      for (int i = 0; i < seqSource.ChannelGroups.Count; i++)
      {
        string nam = seqSource.ChannelGroups[i].Name.ToLower();
        if (nam.Length < 1)
        { nam = "ChannelGroup " + seqSource.ChannelGroups[i].Index.ToString("0000"); }
        while (sourceGroupNames.ContainsKey(nam))
        { nam += seqSource.ChannelGroups[i].SavedIndex.ToString(); }
        sourceGroupNames.Add(nam, seqSource.ChannelGroups[i]);
      }
      //TODO: Cosmic Devices
      // Tracks
      for (int i = 0; i < seqSource.Tracks.Count; i++)
      {
        string nam = seqSource.Tracks[i].Name.ToLower();
        if (nam.Length < 1)
        { nam = "Track " + seqSource.Tracks[i].Index.ToString("0000"); }
        while (sourceTrackNames.ContainsKey(nam))
        { nam += seqSource.Tracks[i].TrackNumber.ToString(); }
        sourceTrackNames.Add(nam, seqSource.Tracks[i]);
      }







      ////////////////////////////////////////////////////////////////////////////////////////////////////////////
      // First Pass, just make sure it's valid and get line count so we can update the progress bar accordingly
      //////////////////////////////////////////////////////////////////////////////////////////////////////////
      #region First Pass
      StreamReader reader = new StreamReader(fileName);
      if (!reader.EndOfStream)
      {
        lineIn = reader.ReadLine(); // Line 1

        // Sanity Check #2, is it an Channel Map file?
        li = lineIn.Substring(0, 23).CompareTo("Util-O-Rama Channel Map");
        if (li != 0)
        {
          errorStatus = 101;
        }
        else
        {
          // Sanity Check #1B, does it have at least 2 lines?
          if (!reader.EndOfStream)
          {
            lineIn = reader.ReadLine(); // Line 2
                                        // This should be the CSV Column Headers, ignore and throw away
            lineCount = 2;
            while ((lineIn = reader.ReadLine()) != null)
            {
              lineCount++;
            } // end while lines remain, counting them
          } // It has at least 2 lines
        } //its in XML format
      } // it has at least 1 line
      reader.Close();
      #endregion // First Pass
      // end of first pass




      if ((errorStatus == 0) && (lineCount > 2))
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
        // Read in and then ignore the first 2 lines
        lineIn = reader.ReadLine();
        lineIn = reader.ReadLine();
        lineNum = 2;

        // * PARSE LINES
        while ((lineIn = reader.ReadLine()) != null)
        {
          lineNum++;
          mapData = lineIn.Split(',');
          if (mapData.Length == 5)
          {
            theType = mapData[0].ToLower();
            destType = LOR4MemberType.None; // Reset default
            destName = LOR4Admin.HumanizeName(mapData[1]).ToLower();
            destSI = LOR4Admin.UNDEFINED; // Reset default
            int.TryParse(mapData[2], out destSI);
            sourceName = LOR4Admin.HumanizeName(mapData[3]).ToLower(); ;
            sourceSI = LOR4Admin.UNDEFINED;
            int.TryParse(mapData[4], out sourceSI);
            switch (theType)
            {
              case "channel":
                destType = LOR4MemberType.Channel;
                if ((destSI >= 0) && (destSI < seqDest.AllMembers.HighestSavedIndex))
                {
                  destMember = seqDest.AllMembers.BySavedIndex[destSI];
                  if (destMember.MemberType != destType)
                  {
                    destMember = null;
                  }
                  else
                  {
                    if (destMember.Name.ToLower() != destName)
                    {
                      destMember = null;
                    }
                  }
                  if (destMember == null)
                  {
                    destChanNames.TryGetValue(destName, out destMember);
                  }
                  if (destMember != null)
                  {
                    if ((sourceSI >= 0) && (sourceSI < seqSource.AllMembers.HighestSavedIndex))
                    {
                      sourceMember = seqSource.AllMembers.BySavedIndex[sourceSI];
                      if (sourceMember.MemberType != destType)
                      {
                        sourceMember = null;
                      }
                      else
                      {
                        if (sourceMember.Name.ToLower() != sourceName)
                        {
                          sourceMember = null;
                        }
                      }
                      if (sourceMember == null)
                      { sourceChanNames.TryGetValue(sourceName, out sourceMember); }
                      if (sourceMember != null)
                      {
                        bool success = MapMembers(sourceMember, destMember);
                      }
                    }
                  }
                }
                break;
              case "rgbchannel":
                destType = LOR4MemberType.RGBChannel;
                if ((destSI >= 0) && (destSI < seqDest.AllMembers.HighestSavedIndex))
                {
                  destMember = seqDest.AllMembers.BySavedIndex[destSI];
                  if (destMember.MemberType != destType)
                  {
                    destMember = null;
                  }
                  else
                  {
                    if (destMember.Name.ToLower() != destName)
                    {
                      destMember = null;
                    }
                  }
                  if (destMember == null)
                  {
                    destRGBNames.TryGetValue(destName, out destMember);
                  }
                  if (destMember != null)
                  {
                    if ((sourceSI >= 0) && (sourceSI < seqSource.AllMembers.HighestSavedIndex))
                    {
                      sourceMember = seqSource.AllMembers.BySavedIndex[sourceSI];
                      if (sourceMember.MemberType != destType)
                      {
                        sourceMember = null;
                      }
                      else
                      {
                        if (sourceMember.Name.ToLower() != sourceName)
                        {
                          sourceMember = null;
                        }
                      }
                      if (sourceMember == null)
                      { sourceRGBNames.TryGetValue(sourceName, out sourceMember); }
                      if (sourceMember != null)
                      {
                        bool success = MapMembers(sourceMember, destMember);
                      }
                    }
                  }
                }
                break;
              case "channelgroup":
                destType = LOR4MemberType.ChannelGroup;
                if ((destSI >= 0) && (destSI < seqDest.AllMembers.HighestSavedIndex))
                {
                  destMember = seqDest.AllMembers.BySavedIndex[destSI];
                  if (destMember.MemberType != destType)
                  {
                    destMember = null;
                  }
                  else
                  {
                    if (destMember.Name.ToLower() != destName)
                    {
                      destMember = null;
                    }
                  }
                  if (destMember == null)
                  {
                    destGroupNames.TryGetValue(destName, out destMember);
                  }
                  if (destMember != null)
                  {
                    if ((sourceSI >= 0) && (sourceSI < seqSource.AllMembers.HighestSavedIndex))
                    {
                      sourceMember = seqSource.AllMembers.BySavedIndex[sourceSI];
                      if (sourceMember.MemberType != destType)
                      {
                        sourceMember = null;
                      }
                      else
                      {
                        if (sourceMember.Name.ToLower() != sourceName)
                        {
                          sourceMember = null;
                        }
                      }
                      if (sourceMember == null)
                      { sourceGroupNames.TryGetValue(sourceName, out sourceMember); }
                      if (sourceMember != null)
                      {
                        bool success = MapMembers(sourceMember, destMember);
                      }
                    }
                  }
                }
                break;
              case "cosmicdevice":
                destType = LOR4MemberType.Cosmic;
                break;
              case "track":
                destType = LOR4MemberType.Track;
                if ((destSI >= 0) && (destSI < seqDest.AllMembers.HighestSavedIndex))
                {
                  destMember = seqDest.AllMembers.BySavedIndex[destSI];
                  if (destMember.MemberType != destType)
                  {
                    destMember = null;
                  }
                  else
                  {
                    if (destMember.Name.ToLower() != destName)
                    {
                      destMember = null;
                    }
                  }
                  if (destMember == null)
                  {
                    destTrackNames.TryGetValue(destName, out destMember);
                  }
                  if (destMember != null)
                  {
                    if ((sourceSI >= 0) && (sourceSI < seqSource.AllMembers.HighestSavedIndex))
                    {
                      sourceMember = seqSource.AllMembers.BySavedIndex[sourceSI];
                      if (sourceMember.MemberType != destType)
                      {
                        sourceMember = null;
                      }
                      else
                      {
                        if (sourceMember.Name.ToLower() != sourceName)
                        {
                          sourceMember = null;
                        }
                      }
                      if (sourceMember == null)
                      { sourceTrackNames.TryGetValue(sourceName, out sourceMember); }
                      if (sourceMember != null)
                      {
                        bool success = MapMembers(sourceMember, destMember);
                      }
                    }
                  }
                }
                break;
            } // End Switch(Member Type)
          } // Line has 5 values
        } // while not eof map file
        #endregion

        //if (chkFuzzy.Checked)
        if (false) // Disabled for now, until I get time to update this to actually work

        {


          ////////////////////////////////////////////////////////////////////
          // Third pass, attempt to fuzzy find anything not already matched
          //////////////////////////////////////////////////////////////////
          #region Thid Pass
          //TODO Update this for new CSV format instead of XML format
          // Don't think should do fuzzy find on reading a map file
          reader = new StreamReader(fileName);
          // Read in and then ignore the first 2 lines
          //   File Info and CSV headers
          lineIn = reader.ReadLine();
          lineIn = reader.ReadLine();
          lineNum = 2;

          // * PARSE LINES
          while ((lineIn = reader.ReadLine()) != null)
          {
            lineNum++;
            // Line just read should be "    <channels>"
            //li = lineIn.IndexOf(LOR4Admin.STTBL + TABLEchannels + LOR4Admin.ENDTBL);
            li = LOR4Admin.FastIndexOf(lineIn, LOR4Admin.STTBL + TABLEchannels + LOR4Admin.ENDTBL);
            if (li > 0)
            {
              if (!reader.EndOfStream)
              {
                // Next line is the Destination LOR4Channel
                lineIn = reader.ReadLine();
                lineNum++;
                //li = lineIn.IndexOf(FIELDdestChannel);
                li = LOR4Admin.FastIndexOf(lineIn, FIELDdestChannel);
                if (li > 0)
                {
                  destName = LOR4Admin.HumanizeName(LOR4Admin.getKeyWord(lineIn, LOR4Admin.FIELDname));
                  //lblMessage.Text = "Map \"" + destName + "\" to...";
                  //pnlMessage.Refresh();
                  destSI = LOR4Admin.getKeyValue(lineIn, LOR4Admin.FIELDsavedIndex);
                  destType = LOR4SeqEnums.EnumMemberType(LOR4Admin.getKeyWord(lineIn, LOR4Admin.FIELDtype));
                  // Next line (let's assume its there) is the Source LOR4Channel
                  if (!reader.EndOfStream)
                  {
                    // Next line is the Source LOR4Channel
                    lineIn = reader.ReadLine();
                    lineNum++;
                    //li = lineIn.IndexOf(FIELDsourceChannel);
                    li = LOR4Admin.FastIndexOf(lineIn, FIELDsourceChannel);
                    if (li > 0)
                    {
                      sourceName = LOR4Admin.HumanizeName(LOR4Admin.getKeyWord(lineIn, LOR4Admin.FIELDname));
                      if (sourceOnRight)
                      {
                        lblMessage.Text = destName + " to " + sourceName;
                      }
                      else
                      {
                        lblMessage.Text = sourceName + " to " + destName;
                      }
                      prgBarInner.CustomText = lblMessage.Text;
                      pnlMessage.Refresh();
                      sourceSI = LOR4Admin.getKeyValue(lineIn, LOR4Admin.FIELDsavedIndex);

                      if (!seqDest.AllMembers.BySavedIndex[destSI].Selected)
                      {
                        if (seqDest.AllMembers.BySavedIndex[destSI].MemberType == destType)
                        {
                          // Search for it again by name, this time use fuzzy find
                          logMsg = "Fuzzy searching Destination for ";
                          logMsg += sourceName;
                          Console.WriteLine(logMsg);
                          logWriter1.WriteLine(logMsg);

                          foundID = FindByName(destName, seqDest.AllMembers, destType, preAlgorithm, minPreMatch, finalAlgorithms, minFinalMatch, true);
                          if (foundID != null)
                          {
                            destSI = foundID.SavedIndex;
                            destName = foundID.Name;
                          }

                          // Got it yet?
                          if (destSI > LOR4Admin.UNDEFINED)
                          {
                            if (sourceSI < seqSource.AllMembers.BySavedIndex.Count)
                            {
                              if (seqSource.AllMembers.BySavedIndex[sourceSI].MemberType == destType)
                              {
                                if (!seqSource.AllMembers.BySavedIndex[sourceSI].Selected)
                                {
                                  //try to find it again by name and this time use fuzzy matching
                                  logMsg = "Fuzzy searching Source for ";
                                  logMsg += sourceName;
                                  Console.WriteLine(logMsg);
                                  logWriter1.WriteLine(logMsg);
                                  foundID = FindByName(sourceName, seqSource.AllMembers, destType, preAlgorithm, minPreMatch, finalAlgorithms, minFinalMatch, true);
                                  if (foundID != null)
                                  {
                                    sourceSI = foundID.SavedIndex;
                                  }
                                } // if not Selected

                                // Got it yet?
                                if (sourceSI > LOR4Admin.UNDEFINED)
                                {
                                  sourceMember = seqSource.AllMembers.BySavedIndex[sourceSI];
                                  //iLOR4Member destMember = seqDest.AllMembers.BySavedIndex[destSI];
                                  if (sourceMember.MemberType == destMember.MemberType)
                                  {
                                    logMsg = "Fuzzy Matched " + destName + " to " + sourceName;
                                    Console.WriteLine(logMsg);
                                    logWriter1.WriteLine(logMsg);
                                    bool newMaps = MapMembers(sourceMember, destMember, false);
                                    //mappedMemberCount += newMaps;
                                    //seqDest.AllMembers.BySavedIndex[destSI].Selected = true;
                                    //seqSource.AllMembers.BySavedIndex[sourceSI].Selected = true;
                                  }
                                }
                              } // if source type matchies
                            } // if source SavedIndex in range
                          } // if source section line
                        } // if lines remain
                      } // if got a sdestination channel
                    } // if destination type matches
                  } // if destination SI not Selected
                } // if line is a destination channel
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
        //UpdateMappedCount(0);
      }
      ImBusy(false);
      return errMsgs;
    } // end LoadApplyMapFile

    private void SaveNewMappedSequence()
    {
      ImBusy(true);
      string xt = Path.GetExtension(sourceFile).ToLower();
      dlgFileSave.DefaultExt = xt;
      if (xt == LOR4Admin.EXT_LAS)
      {
        dlgFileSave.Filter = LOR4Admin.FILE_LAS;
      }
      if (xt == LOR4Admin.EXT_LMS)
      {
        dlgFileSave.Filter = LOR4Admin.FILE_LMS;
      }
      //dlgFileSave.Filter = LOR4Admin.FILT_SAVE_EITHER;
      dlgFileSave.FilterIndex = 0;
      //if (xt.CompareTo(LOR4Admin.EXT_LAS) == 0) dlgFileSave.FilterIndex = 1;
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
      dlgFileSave.FileName = newName;
      dlgFileSave.InitialDirectory = initDir;
      dlgFileSave.OverwritePrompt = false;
      dlgFileSave.CheckPathExists = true;
      dlgFileSave.Title = "Save New Sequence As...";

      DialogResult dr = dlgFileSave.ShowDialog();
      if (dr == DialogResult.OK)
      {
        DialogResult ow = Fyle.SafeOverwriteFile(dlgFileSave.FileName);
        if (ow == DialogResult.Yes)
        {

          string saveTemp = System.IO.Path.GetTempPath();
          saveTemp += Path.GetFileName(dlgFileSave.FileName);

          //string msg = "GLB";
          //msg += LeftBushesChildCount().ToString();
          //lblDebug.Text = msg;

          int mapErr = SaveNewMappedSequence(saveTemp);
          if (mapErr == 0)
          {
            saveFile = dlgFileSave.FileName;
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
        }
      } // end dialog result = OK
      ImBusy(false);
    }

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
      while (File.Exists(ret))
      {
        i++;
        ret = p + n + " Remapped (" + i.ToString() + ")" + e;
      }
      return ret;
    } // end SuggestedNewName

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
      if (seqDest.AllMembers.Items.Count > 1)
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



    #endregion

    #region AutoMap and Map from File
    private void AskToMap()
    {
      string msg = "";
      DialogResult dr;

      if (seqDest.Channels.Count > 0)
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
      } // end destination is loaded
    } // end AskToMap

    private void AutoMap()
    {
      ImBusy(true);
      bulkOp = true;

      string sourceName = "";
      int sourceSI = LOR4Admin.UNDEFINED;
      int destSI = LOR4Admin.UNDEFINED;
      int[] matchSIs = null;
      iLOR4Member srcID = null;
      const string STATUSautoMap = "AutoMap \"";
      const string STATUSto = "\" to...";

      int w = pnlStatus.Width;
      int matchCount = 0;
      pnlProgress.Width = w;
      pnlProgress.Size = new Size(w, pnlProgress.Height);
      pnlStatus.Visible = false;
      pnlProgress.Visible = true;
      staStatus.Refresh();
      //int mq = seqDest.Tracks.Count + seqDest.ChannelGroups.Count + seqDest.Channels.Count;
      int mq = seqDest.Tracks.Count;
      //mq -= seqDest.RGBchannels.Count * 2;
      //int sq = seqSource.Tracks.Count + seqSource.ChannelGroups.Count + seqSource.Channels.Count;
      int sq = seqSource.Tracks.Count;
      //sq -= seqSource.RGBchannels.Count * 2;
      int mm = seqDest.AllMembers.Count;
      int qq = mq + sq + mm;
      //pnlProgress.Maximum = (int)(qq);
      pnlProgress.Maximum = seqDest.AllMembers.Count * 10;
      pp = 0;
      string logFile = Fyle.GetTempPath() + "Fuzzy.log";
      if (File.Exists(logFile))
      {
        File.Delete(logFile);
      }




      // First Pass, try to match by SavedIndex
      // TRACKS
      //for (int tridx = 1; tridx < seqDest.Tracks.Count; tridx++)
      //{
      //	LOR4Track destTrack = seqDest.Tracks[tridx];
      //	lblMessage.Text = STATUSautoMap + destTrack.Name + STATUSto;
      //	pnlMessage.Refresh();
      //	pp++;
      //	pnlProgress.Value = pp;
      //	staStatus.Refresh();
      //int mcs = AutoMapMembersBySavedIndex(destTrack.Members);
      int mcs = AutoMapMembersBySavedIndex(seqDest.AllMembers);
      //matchCount += mcs;
      mappedMemberCount += mcs;
      //}

      //for (int tridx = 1; tridx < seqDest.Tracks.Count; tridx++)
      //{
      //LOR4Track destTrack = seqDest.Tracks[tridx];
      //lblMessage.Text = STATUSautoMap + destTrack.Name + STATUSto;
      //pnlMessage.Refresh();
      //pp++;
      //pnlProgress.Value = pp;
      //staStatus.Refresh();
      //int mcn = AutoMapMembersByName(destTrack.Members);
      int mcn = AutoMapMembersByName(seqDest.AllMembers);
      //matchCount += mcn;
      mappedMemberCount += mcn;
      //}

      //for (int tridx = 1; tridx < seqDest.Tracks.Count; tridx++)
      //{
      //LOR4Track destTrack = seqDest.Tracks[tridx];
      //int mc = AutoMapMembersByFuzzy(destTrack.Members);
      int mcc = AutoMapMembersByFuzzy(seqDest.AllMembers);
      //matchCount += mcc;
      mappedMemberCount += mcc;
      //}

      pnlProgress.Visible = false;
      lblMessage.Text = "Automap Complete";
      //UpdateMappedCount(0);
      bulkOp = false;
      ImBusy(false);
    }  // end LOR4Track loop

    private int AutoMapMembersBySavedIndex(LOR4Membership destMembers)
    {
      int matchCount = 0;
      for (int memidx = 0; memidx < destMembers.Count; memidx++)
      {
        iLOR4Member destMember = destMembers[memidx];
        if (destMember.MemberType != LOR4MemberType.Track)
        {
          if (destMember.MapTo == null) // Mapped already?
          {
            if (destMember.SavedIndex <= seqSource.AllMembers.HighestSavedIndex)
            {
              iLOR4Member newSourceMember = seqSource.AllMembers.BySavedIndex[destMember.SavedIndex];
              if (destMember.MemberType == newSourceMember.MemberType)
              {
                if (destMember.Name.ToLower().CompareTo(newSourceMember.Name.ToLower()) == 0)
                {
                  bool newMaps = MapMembers(newSourceMember, destMember, false);
                  //matchCount += newMaps;
                  if (destMember.MemberType == LOR4MemberType.ChannelGroup)
                  {
                    //	LOR4ChannelGroup destGroup = (LOR4ChannelGroup)destMember;
                    //	int mc = AutoMapMembersBySavedIndex(destGroup.Members); // Recurse
                    //	mappedMemberCount += mc;
                    lblMessage.Text = STATUSautoMap + destMember.Name + STATUSto;
                    pnlMessage.Refresh();
                  }
                }
              }
            }
          }
        }
        pp++;
        pnlProgress.Value = pp;
        staStatus.Refresh();
      }
      return matchCount;
    }
    private int AutoMapMembersByName(LOR4Membership destMembers)
    {
      int matchCount = 0;
      for (int memidx = 0; memidx < destMembers.Count; memidx++)
      {
        iLOR4Member destMember = destMembers[memidx];
        if (destMember.MemberType != LOR4MemberType.Track)
        {
          if (destMember.MapTo == null) // Mapped already?
          {
            iLOR4Member newSourceMember = seqSource.AllMembers.FindByName(destMember.Name, destMember.MemberType, false);
            if (newSourceMember != null)
            {
              if (destMember.Name.ToLower().CompareTo(newSourceMember.Name.ToLower()) == 0)
              {
                bool newMaps = MapMembers(newSourceMember, destMember, false);
                //matchCount += newMaps;
                if (destMember.MemberType == LOR4MemberType.ChannelGroup)
                {
                  //	LOR4ChannelGroup destGroup = (LOR4ChannelGroup)destMember;
                  //	int mc = AutoMapMembersByName(destGroup.Members); // Recurse
                  //	mappedMemberCount += mc;
                  lblMessage.Text = STATUSautoMap + destMember.Name + STATUSto;
                  pnlMessage.Refresh();

                }
              }
            }
          }
        }
        pp++;
        pnlProgress.Value = pp;
        staStatus.Refresh();
      }
      return matchCount;
    }
    private int AutoMapMembersByFuzzy(LOR4Membership destMembers)
    {
      int matchCount = 0;
      for (int memidx = 0; memidx < destMembers.Count; memidx++)
      {
        iLOR4Member destMember = destMembers[memidx];
        if (destMember.MemberType != LOR4MemberType.Track)
        {
          if (destMember.MapTo == null) // Mapped already?
          {
            lblMessage.Text = STATUSautoMap + destMember.Name + STATUSto;
            pnlMessage.Refresh();
            iLOR4Member newSourceMember = FuzzyFindName(destMember.Name, seqSource, destMember.MemberType);
            if (newSourceMember != null)
            {
              bool newMaps = MapMembers(newSourceMember, destMember, false);
              //matchCount += newMaps;
              //if (destMember.MemberType == LOR4MemberType.ChannelGroup)
              //{
              //	LOR4ChannelGroup destGroup = (LOR4ChannelGroup)destMember;
              //	int mc = AutoMapMembersByName(destGroup.Members); // Recurse
              //	matchCount += mc;
              //}
            }
          }
        }
        pp += 8;
        pnlProgress.Value = pp;
        staStatus.Refresh();
      }
      return matchCount;
    }
    public iLOR4Member FuzzyFindName(string theName, LOR4Sequence sequence, LOR4MemberType theType)
    {
      iLOR4Member ret = null;
      bool useFuzzy = Properties.Settings.Default.FuzzyUse;
      bool writeLog = Properties.Settings.Default.FuzzyWriteLog;

      string logFile = "";
      StreamWriter writer = new StreamWriter(Stream.Null);
      string lineOut = "";
      if (writeLog)
      {
        logFile = Fyle.GetTempPath() + "Fuzzy.log";
        writer = new StreamWriter(logFile, true);
        writer.WriteLine("");
        lineOut = "Looking for     \"" + theName + "\" in ";
        lineOut += LOR4SeqEnums.MemberName(theType) + "s in ";
        lineOut += Path.GetFileName(sequence.info.filename);
        writer.WriteLine(lineOut);
      }

      if (sequence.AllMembers.ByName.ContainsKey(theName))
      {
        iLOR4Member r2 = sequence.AllMembers.ByName[theName];
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
          //List<iLOR4Member> matchedMembers = null;
          List<iLOR4Member> matchedMembers = new List<iLOR4Member>();
          double[] scores = null;
          int[] SIs = null;
          int count = 0;
          long[] totTimes = null;
          int[] totCount = null;
          //Array.Resize(ref totTimes, FuzzyFunctions.ALGORITHM_COUNT + 1);
          //Array.Resize(ref totCount, FuzzyFunctions.ALGORITHM_COUNT + 1);
          double score = 0;
          double highestScore = 0;
          double bestMatch = -1;
          //long finalAlgorithms = Properties.Settings.Default.FuzzyFinalAlgorithms;
          //long prematchAlgorithm = Properties.Settings.Default.FuzzyPrematchAlgorithm;
          //bool caseSensitive = Properties.Settings.Default.FuzzyCaseSensitive;
          //double minPrematchScore = Properties.Settings.Default.FuzzyMinPrematch;
          //double minFinalMatchScore = Properties.Settings.Default.FuzzyMinFinal;

          // Now perform all other requested algorithms
          if (writeLog)
          {
            //lineOut = "Fuzzy Prematches with a minimum of " + minPrematchScore.ToString() + ":";
            //writer.WriteLine(lineOut);
          }

          if (theType == LOR4MemberType.Channel)
          {
            //foreach (iLOR4Member member in sequence.Channels)
            for (int m = 0; m < sequence.Channels.Count; m++)
            {
              iLOR4Member member = sequence.Channels[m];
              highestScore = 0;
              bestMatch = -1;
              // get a quick prematch score
              score = theName.FuzzyScoreFast(member.Name);
              // fi the score is above the minimum PreMatch
              if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
              {
                score = theName.FuzzyScoreAccurate(member.Name);
                if (score > highestScore)
                {
                  highestScore = score;
                  bestMatch = m;
                }

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

          if (theType == LOR4MemberType.RGBChannel)
          {
            foreach (iLOR4Member member in sequence.RGBchannels)
            {
              // get a quick prematch score
              score = theName.FuzzyScoreFast(member.Name);
              // fi the score is above the minimum PreMatch
              if (score * 100 > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
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
            // LOR4Loop thru qualifying prematches
            for (int i = 0; i < count; i++)
            {
              // Get the ID, perform a more thorough final fuzzy match, and save the score
              iLOR4Member member = sequence.AllMembers.BySavedIndex[SIs[i]];
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
              if (true) //   ((FuzzyFunctions. .USE_CASEINSENSITIVE) > 0)
              {
                source = source.ToUpper();
                target = target.ToUpper();
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



              for (int al = 1; al <= FuzzyFunctions.ALGORITHM_COUNT; al++)
              {
                long mask = (long)Math.Pow(2D, (double)al) / 2;
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
                iLOR4Member y = sequence.AllMembers.BySavedIndex[SIs[f]];
                lineOut += y.SavedIndex.ToString().PadLeft(5);
                lineOut += "=\"" + y.Name + "\"";
                writer.WriteLine(lineOut);
              }
            } // end writelog

            // Is the best/highest above the required minimum Final Match score?
            if (scores[count - 1] * 100 > minFinalMatchScore)
            {
              // Return the ID with the best qualifying final match
              ret = sequence.AllMembers.BySavedIndex[SIs[count - 1]];
              // Get name just for debugging
              string msg = theName + " ~= " + ret.Name;
              if (writeLog)
              {
                lineOut = "Best Match Is:";
                writer.WriteLine(lineOut);
                lineOut = scores[count - 1].ToString("0.0000") + " SI:";
                iLOR4Member y = sequence.AllMembers.BySavedIndex[SIs[count - 1]];
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
    public iLOR4Member FindByName(string theName, LOR4Membership children, LOR4MemberType PartType, long preAlgorithm, double minPreMatch, long finalAlgorithms, double minFinalMatch, bool ignoreSelected)
    {
      iLOR4Member ret = null;
      if (children.ByName.TryGetValue(theName, out ret))
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
          ret = FuzzyFind(theName, children, ignoreSelected);
        }
      }

      return ret;
    }
    public iLOR4Member FuzzyFind(string theName, LOR4Membership children, bool ignoreSelected)
    {
      iLOR4Member ret = null;
      double[] scores = null;
      int[] SIs = null;
      int count = 0;
      double score = 0D;
      double bestScore = 0D;
      int bestIndex = -1;

      // Go thru all objects
      //foreach (iLOR4Member child in children.Items)
      for (int idx = 0; idx < children.Items.Count; idx++)
      {
        iLOR4Member child = children[idx];
        if ((!child.Selected) || (!ignoreSelected))
        {
          bestScore = 0D;
          bestIndex = -1;
          // get a quick prematch score
          score = theName.FuzzyScoreFast(child.Name);
          // fi the score is above the minimum PreMatch
          if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
          {
            // Increment count and save the SavedIndex
            // NOte: No need to save the PreMatch score
            count++;
            score = theName.FuzzyScoreAccurate(child.Name);
            // fi the score is above the minimum PreMatch
            if (score > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
            {
              if (score > bestScore)
              {
                bestScore = score;
                bestIndex = idx;
              }
            }
          }
        }
      }
      if (bestIndex >= 0)
      { ret = children.Items[bestIndex]; }
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
      string lineOut = theScore.ToString("0.0000 "); // + FuzzyFunctions.AlgorithmNames(algorithm);
      if (!isValid) lineOut += " Not";
      lineOut += " Valid  ";
      lineOut += elapsed.ToString() + "μs";

      return lineOut;
    }
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
    #endregion

    #region User Selections and Mapping
    private void SourceNode_Click(TreeNodeAdv sourceNode)
    {
      if (sourceNode.Tag != null)
      {
        iLOR4Member sourceMember = (iLOR4Member)sourceNode.Tag;
        // Did selection change?
        if (currentSourceMember == null)
        {
          SourceMember_Click(sourceMember);
        }
        else
        {
          if (currentSourceMember.ID != sourceMember.ID)
          {
            SourceMember_Click(sourceMember);
          }
        }
      }
    }
    private void DestNode_Click(TreeNodeAdv destNode)
    {
      if (destNode.Tag != null)
      {
        iLOR4Member destMember = (iLOR4Member)destNode.Tag;
        // Did selection change?
        if (currentDestMember == null)
        {
          DestMember_Click(destMember);
        }
        else
        {
          if (currentDestMember.ID != destMember.ID)
          {
            DestMember_Click(destMember);
          }
        }
      }
    }
    private void SourceMember_Click(iLOR4Member newSourceMember)
    {
      iLOR4Member lastSourceMember = currentSourceMember;
      string sourceText = "";
      // Are we already mapped to the last destination member?
      bool wasMapped = false;
      bool isMapped = false;
      int destID = LOR4Admin.UNDEFINED;
      if (currentDestMember != null) destID = currentDestMember.ID;

      // Was a source previously selected
      if (lastSourceMember != null)
      {
        // Clear the previous selection
        TreeUtils.HighlightMember(lastSourceMember, ColorUnselectedFG, ColorUnselectedBG);

        // Was the last source mapped to any destination(s)?
        if (lastSourceMember.Tag != null)
        {
          List<iLOR4Member> mapsTos = (List<iLOR4Member>)lastSourceMember.Tag;
          // Clear the highlighting on those mapped destination(s)
          for (int m = 0; m < mapsTos.Count; m++)
          {
            // Was it mapped to the currently selected destination?
            if (mapsTos[m].ID == destID)
            {
              // Return the destination to the normal selected color
              //! Unnecessary
              //TreeUtils.HighlightMember(mapsTos[m], ColorDestSelectedFG, ColorDestSelectedBG);
            }
            else
            {
              // No?  Then just clear it
              TreeUtils.HighlightMember(mapsTos[m], ColorUnselectedFG, ColorUnselectedBG);
            }
          } // End loop thru mapped members
        } // End has mapped members
      } // End there was a source previously selected

      // Highlight this new source selection
      TreeUtils.HighlightMember(newSourceMember, ColorSourceSelectedFG, ColorSourceSelectedBG);
      switch (newSourceMember.MemberType)
      {
        case LOR4MemberType.Channel:
          LOR4Channel chan = (LOR4Channel)newSourceMember;
          LOR4Admin.RenderEffects(chan, ref picPreviewSource, chkRamps.Checked);
          picPreviewSource.Visible = true;
          break;
        case LOR4MemberType.RGBChannel:
          LOR4RGBChannel rgbc = (LOR4RGBChannel)newSourceMember;
          LOR4Admin.RenderEffects(rgbc, ref picPreviewSource, chkRamps.Checked);
          picPreviewSource.Visible = true;
          break;
        case LOR4MemberType.ChannelGroup:
          picPreviewSource.Visible = false;
          break;
        case LOR4MemberType.Track:
          picPreviewSource.Visible = false;
          break;
        case LOR4MemberType.Cosmic:
          picPreviewSource.Visible = false;
          break;
      }
      // Is it mapped to any destination(s)?
      if (newSourceMember.Tag != null)
      {
        List<iLOR4Member> mapsTos = (List<iLOR4Member>)newSourceMember.Tag;
        // Clear the highlighting on those mapped destination(s)
        for (int m = 0; m < mapsTos.Count; m++)
        {
          // Was it mapped to the currently selected destination?
          if (mapsTos[m].ID == destID)
          {
            // Do NOT change the destination highlight!
            // And change the highlight on this new source to green
            TreeUtils.HighlightMember(newSourceMember, ColorDestSelectedFG, ColorDestSelectedBG);
          }
          else
          {
            // No?  Then highlight it light red
            TreeUtils.HighlightMember(mapsTos[m], ColorMappedDestFG, ColorMappedDestBG);
          }
        } // End loop thru mapped members
      } // End has mapped members

      // Remember this selection
      currentSourceMember = newSourceMember;
      UpdateUI();
    }

    private void DestMember_Click(iLOR4Member newDestMember)
    {
      iLOR4Member lastDestMember = currentDestMember;
      string destText = "";

      bool wasMapped = false;
      bool isMapped = false;
      int sourceID = LOR4Admin.UNDEFINED;
      if (currentSourceMember != null) sourceID = currentSourceMember.ID;

      // Was a destination previously selected
      if (lastDestMember != null)
      {
        // Clear highlighting from last selected destination
        TreeUtils.HighlightMember(lastDestMember, ColorUnselectedFG, ColorUnselectedBG);

        // Was the last destination member mapped to anything?
        if (lastDestMember.MapTo != null)
        {
          // Was it mapped to the current source member?
          if (lastDestMember.MapTo.ID == sourceID)
          {
            wasMapped = true;
            // Return last destination color to light red to indicate it's mapped to the current source
            TreeUtils.HighlightMember(lastDestMember, ColorMappedDestFG, ColorMappedDestBG);
            // And the source back to normal dark red selected color
            TreeUtils.HighlightMember(currentSourceMember, ColorSourceSelectedFG, ColorSourceSelectedBG);
          }
          else
          {
            // last destination was mapped to something other than the current source
            // Return whatever was mapped to the last destination back to normal unselected state
            TreeUtils.HighlightMember(lastDestMember.MapTo, ColorUnselectedFG, ColorUnselectedBG);
          }
        }
      }

      // Highlight the newly selected destination
      TreeUtils.HighlightMember(newDestMember, ColorDestSelectedFG, ColorDestSelectedBG);
      // is the current destination member mapped to anything?
      if (newDestMember.MapTo != null)
      {
        // Would it happen to be the current source member?
        if (newDestMember.MapTo.ID == sourceID)
        {
          isMapped = true;
          // Turn the current source green to indicate mapped to current destination
          TreeUtils.HighlightMember(currentSourceMember, ColorDestSelectedFG, ColorDestSelectedBG);
        }
        else
        {
          // Use normal mapped color highlight
          TreeUtils.HighlightMember(newDestMember.MapTo, ColorDestSelectedFG, ColorDestSelectedBG);
        }
      }

      // Create and turn on preview, or hide it, according to member type
      switch (newDestMember.MemberType)
      {
        case LOR4MemberType.Channel:
          LOR4Channel chan = (LOR4Channel)newDestMember;
          LOR4Admin.RenderEffects(chan, ref picPreviewDest, chkRamps.Checked);
          pnlOverwrite.Visible = true;
          picPreviewDest.Visible = true;
          break;
        case LOR4MemberType.RGBChannel:
          LOR4RGBChannel rgbc = (LOR4RGBChannel)newDestMember;
          LOR4Admin.RenderEffects(rgbc, ref picPreviewDest, chkRamps.Checked);
          pnlOverwrite.Visible = true;
          picPreviewDest.Visible = true;
          break;
        case LOR4MemberType.ChannelGroup:
          picPreviewDest.Visible = false;
          break;
        case LOR4MemberType.Track:
          picPreviewDest.Visible = false;
          break;
        case LOR4MemberType.Cosmic:
          picPreviewDest.Visible = false;
          break;
      }

      // Remember this selection
      currentDestMember = newDestMember;
      // Update Map and UnMap button states
      UpdateUI();
    }

    private void UpdateUI()
    {
      UpdateMappability();
      UpdateMapLabels();
    }

    private void UpdateMapLabels()
    {
      //int sourceID = LOR4Admin.UNDEFINED;
      //int destID = LOR4Admin.UNDEFINED;
      string lbltxt = "";
      //if (currentDestMember != null) destID = currentDestMember.ID;
      if (currentSourceMember != null)
      {
        //sourceID = currentSourceMember.ID;
        lbltxt = currentSourceMember.Name;
        // Is it mapped to any destination(s)?
        if (currentSourceMember.Tag != null)
        {
          lbltxt = currentSourceMember.Name;
          List<iLOR4Member> mapsTos = (List<iLOR4Member>)currentSourceMember.Tag;
          // Clear the highlighting on those mapped destination(s)
          if (mapsTos.Count > 0)
          {
            lbltxt += " maps to ";
            for (int m = 0; m < mapsTos.Count; m++)
            {
              lbltxt += mapsTos[m].Name;
              if (mapsTos.Count == 2)
              { lbltxt += " and "; }
              if (mapsTos.Count > 2)
              {
                if (m == mapsTos.Count - 2)
                { lbltxt += ", and "; }
                else
                { lbltxt += ", "; }
              }
            } // End loop thru mapped members
          }
          else
          {
            lbltxt += " is unmapped";
          }
        } // End has mapped members
        else
        {
          lbltxt += " is unmapped";
        }
      }
      lblSourceMappedTo.Text = lbltxt;

      lbltxt = "";
      if (currentDestMember != null)
      {
        if (currentDestMember.MapTo != null)
        { lbltxt = currentDestMember.MapTo.Name; }
        else
        { lbltxt = "Nothing"; }
        lbltxt += " is mapped to " + currentDestMember.Name;
      }
      lblDestMappedTo.Text = lbltxt;

      lbltxt = mappedDestChannelsCount.ToString() + " mapped channels";


    }

    private void UpdateMappability()
    {
      // https://stackoverflow.com/questions/1732140/displaying-tooltip-over-a-disabled-control
      string tiptxt = "";
      btnMap.Enabled = false;
      btnUnmap.Enabled = false;
      lblDestAlreadyMapped.Visible = false;
      lblDestHasEffects.Visible = false;
      pnlOverwrite.Visible = false;
      ttip.SetToolTip(btnMap, tiptxt);
      ttip.SetToolTip(btnUnmap, tiptxt);
      if (currentDestMember != null)
      {
        if (currentSourceMember != null)
        {
          if (currentSourceMember.MemberType == currentDestMember.MemberType)
          {
            //if (currentSourceMember.MemberType == LOR4MemberType.Channel)
            if (true)
            {
              if (currentDestMember.MapTo != null)
              {
                if (currentDestMember.MapTo.ID == currentSourceMember.ID)
                {
                  btnUnmap.Enabled = true;
                  tiptxt = "Unmap " + currentSourceMember.Name;
                  tiptxt += " from " + currentDestMember.Name;
                  ttip.SetToolTip(btnUnmap, tiptxt);
                  // Set tip for map button...
                }
                else
                {
                  btnMap.Enabled = true;
                  tiptxt = "Unmap " + currentDestMember.MapTo.Name;
                  tiptxt += " from " + currentDestMember.Name;
                  tiptxt += " and map " + currentSourceMember.Name;
                  tiptxt += " to it instead";
                  ttip.SetToolTip(btnUnmap, tiptxt);
                }
              }
              else
              {
                btnMap.Enabled = true;
                tiptxt = "Map " + currentSourceMember.Name;
                tiptxt += " to " + currentDestMember.Name;
                ttip.SetToolTip(btnMap, tiptxt);

                if (currentDestMember.MapTo != null)
                {
                  lblDestAlreadyMapped.Visible = true;
                }
                if (currentDestMember.MemberType == LOR4MemberType.Channel)
                {
                  LOR4Channel ch = (LOR4Channel)currentDestMember;
                  if (ch.effects.Count > 0)
                  {
                    lblDestHasEffects.Visible = true;
                    pnlOverwrite.Visible = true;
                  }
                }


              }



            }
            else
            {
              if (currentSourceMember.MemberType == LOR4MemberType.RGBChannel)
              {

              }
            }
          }
          else
          {
            tiptxt = "abcd";
          }

        }

      }

      if (mappedDestChannelsCount > 0)
      {
        btnSaveNewSeq.Enabled = true;
        if (dirtyMap)
        { btnSaveMap.Enabled = true; }
      }
      else
      {
        btnSaveNewSeq.Enabled = false;
        btnSaveMap.Enabled = false;
      }


    }
    private bool IsCompatible(iLOR4Member sourceMember, iLOR4Member destMember)
    {
      // not necessarily groups, called recursively
      bool ret = true;
      if (sourceMember.MemberType != destMember.MemberType)
      {
        ret = false;
      }
      else
      {
        string sourceCodes = CompatibleCodes(sourceMember);
        string destCodes = CompatibleCodes(destMember);
        ret = (sourceCodes.ToLower() == destCodes.ToLower());
      } // end SIs aren't undefined
      return ret;
    } // end IsCompatible

    private string CompatibleCodes(iLOR4Member member)
    {
      string codes = "";
      LOR4Membership membership = null;
      // Start the code with a letter to indicate its type
      switch (member.MemberType)
      {
        case LOR4MemberType.Channel:
          codes = "c";
          break;
        case LOR4MemberType.RGBChannel:
          codes = "r";
          break;
        case LOR4MemberType.ChannelGroup:
          codes = "g";
          LOR4ChannelGroup group = (LOR4ChannelGroup)member;
          membership = group.Members;
          break;
        case LOR4MemberType.Track:
          codes = "t";
          LOR4Track track = (LOR4Track)member;
          membership = track.Members;
          break;
        case LOR4MemberType.Cosmic:
          codes = "p";
          LOR4Cosmic cosmic = (LOR4Cosmic)member;
          membership = cosmic.Members;
          break;
      }
      // If it's selected, change letter to upper case
      if (member.Selected)
      {
        codes = codes.ToUpper();
      }
      // If this is a group-type member (ChannelGroup, Track, CosmicDevice) get all it's descendants
      if (membership != null)
      {
        for (int m = 0; m < membership.Count; m++)
        {
          //! Recurse
          codes += CompatibleCodes(membership[m]);
        }
      }
      // Finished.  Return what we compiled.
      return codes;
    }
    #endregion User Selections and Mapping

    #region Map and Un-Map
    ///////////////////////////////
    //!  * * MAP  MEMBERS  * *  //
    /////////////////////////////
    private bool MapMembers(iLOR4Member sourceMember, iLOR4Member destMember, bool andChildren = true)
    {
      bool success = false;
      // Sanity checks, including:
      // Do the types match, are they already mapped (or not mapped if false)
      // Are the children compatible?

      if (sourceMember.MemberType == destMember.MemberType)
      {
        List<iLOR4Member> destMapList = null;
        if (sourceMember.Tag != null)
        {
          destMapList = new List<iLOR4Member>();
        }
        else
        {
          destMapList = (List<iLOR4Member>)sourceMember.Tag;
        }

        // Is the destination already mapped to something else?
        if (destMember.MapTo != null)
        {
          // Is it mapped correctly already?
          if (destMember.MapTo.ID != sourceMember.ID)
          {
            // Remove prior mapping
            //! Recurse
            success = UnMapMembers(destMember.MapTo, destMember, andChildren);
          }
        }
        // Use new MapTo property of the destination to hold a reference to the source
        destMember.MapTo = sourceMember;
        // source may map to more than one destination so can't use MapTo property
        // Instead use Tag property to hold a list of members
        destMapList.Add(destMember);
        sourceMember.ZCount = destMapList.Count;

        //? Do I need to put the destMapList back into the tag again?
        sourceMember.Tag = destMapList;



        // Set selected flags as quick easy way to check if they are mampped
        destMember.Selected = true;
        sourceMember.Selected = true;
        TreeUtils.EmboldenMember(sourceMember, true);
        TreeUtils.EmboldenMember(destMember, true);
        success = true;


        // Placeholders for memberships
        LOR4Membership sourceMembers = null;
        LOR4Membership destMembers = null;
        switch (sourceMember.MemberType)
        {
          case LOR4MemberType.Channel:
            // Nothing extra required
            mappedDestChannelsCount++;
            if (sourceMember.ZCount == 1) mappedSourceChannelsCount++;
            break;
          case LOR4MemberType.RGBChannel:
            // Should we also map the children?
            mappedSourceRGBChannelsCount++;
            if (sourceMember.ZCount == 1) mappedDestRGBChannelsCount++;
            if (andChildren)
            {
              LOR4RGBChannel sourceRGB = (LOR4RGBChannel)sourceMember;
              LOR4RGBChannel destRGB = (LOR4RGBChannel)destMember;
              // Map the Red, Green, Blue subchannels
              // Assume success since there is not much chance of failure
              MapMembers(sourceRGB.redChannel, destRGB.redChannel, false);
              MapMembers(sourceRGB.grnChannel, destRGB.grnChannel, false);
              MapMembers(sourceRGB.bluChannel, destRGB.bluChannel, false);
            }
            break;
          case LOR4MemberType.ChannelGroup:
            // Should we also map the children?
            mappedDestGroupsCount++;
            if (sourceMember.ZCount == 1) mappedSourceGroupsCount++;
            if (andChildren)
            {
              // Sanity Check, are they compatible
              if (IsCompatible(sourceMember, destMember))
              {
                // Need to cast them to get memberships
                LOR4ChannelGroup sourceGroup = (LOR4ChannelGroup)sourceMember;
                LOR4ChannelGroup destGroup = (LOR4ChannelGroup)destMember;
                sourceMembers = sourceGroup.Members;
                destMembers = destGroup.Members;
              }
            }
            break;
          case LOR4MemberType.Cosmic:
            // Should we also map the children?
            //mappedDestCosmicCount++;
            //if (sourceMember.ZCount == 1) mappedSourceGroupsCount++;
            if (andChildren)
            {
              // Sanity Check, are they compatible
              if (IsCompatible(sourceMember, destMember))
              {
                LOR4Cosmic sourceCosmic = (LOR4Cosmic)sourceMember;
                LOR4Cosmic destCosmic = (LOR4Cosmic)destMember;
                sourceMembers = sourceCosmic.Members;
                destMembers = destCosmic.Members;
              }
            }
            break;
          case LOR4MemberType.Track:
            // Should we also map the children?
            mappedDestTracksCount++;
            if (sourceMember.ZCount < 2) mappedSourceTracksCount++;
            if (andChildren)
            {
              // Sanity Check, are they compatible
              if (IsCompatible(sourceMember, destMember))
              {
                LOR4Track sourceTrack = (LOR4Track)sourceMember;
                LOR4Track destTrack = (LOR4Track)destMember;
                sourceMembers = sourceTrack.Members;
                destMembers = destTrack.Members;
              }
            }
            break;
        }

        // Did we succeed at getting the child memberships?
        if (destMembers != null)
        {
          if (sourceMembers.Count == destMembers.Count)
          {
            // Loop thru child members
            for (int m = 0; m < destMembers.Count; m++)
            {
              // fetch the current destination child
              iLOR4Member destChild = destMembers[m];
              // Fetch the source child from that
              iLOR4Member sourceChild = sourceMembers[m];
              // Sanity Check: They are supposedly compatible, and thus should have same number of children
              if (sourceChild != null)
              {
                // Another sanity check, this should have already been vetted
                if (sourceChild.MemberType == destChild.MemberType)
                {
                  //! Recurse!
                  bool childSuccess = MapMembers(sourceChild, destChild, true);
                  // If a child mapping failed, extend that to this parent
                  if (!childSuccess) success = false;
                }
                else
                {
                  // Child types don't Match!
                  System.Diagnostics.Debugger.Break();
                }
              }
              else
              {
                // destination child's MapTo not set to source child
                System.Diagnostics.Debugger.Break();
              } // End got the source child
            }
          }
          else
          {
            // source and destination do not have the same number of children
            System.Diagnostics.Debugger.Break();
          } // End source and destination child counts match
        }
        else
        {
          // Failed to get children of destination
          //System.Diagnostics.Debugger.Break();
        } // End got the destination children list

      }
      else
      {
        string msg = "Trying to map " + LOR4SeqEnums.MemberName(sourceMember.MemberType) + "	'" + sourceMember.Name + "'";
        msg += " to " + LOR4SeqEnums.MemberName(destMember.MemberType) + " '" + destMember.Name + "'!";
        Fyle.BUG(msg);
      }

      if (success) MakeDirty();
      UpdateUI();

      return success;
    }

    //////////////////////////////////
    //!  * * UN-MAP  MEMBERS  * *  //
    ////////////////////////////////
    private bool UnMapMembers(iLOR4Member sourceMember, iLOR4Member destMember, bool andChildren = true)
    {
      bool success = false;
      if ((sourceMember.Selected) && (destMember.Selected))
      {
        if (sourceMember.MemberType == destMember.MemberType)
        {
          if (destMember.MapTo != null)
          {
            if (destMember.MapTo.ID == sourceMember.ID)
            {
              List<iLOR4Member> destMapList = (List<iLOR4Member>)sourceMember.Tag;
              destMember.MapTo = null;
              for (int dm = 0; dm < destMapList.Count; dm++)
              {
                if (destMapList[dm].ID == destMember.ID)
                {
                  destMapList[dm].Selected = false;
                  destMapList.RemoveAt(dm);
                  dm = destMapList.Count; // force exit of loop
                }
              }
              sourceMember.ZCount = destMapList.Count;
              sourceMember.Selected = false;
              destMember.Selected = false;
              TreeUtils.EmboldenMember(sourceMember, true);
              TreeUtils.EmboldenMember(destMember, false);
              success = true;

              // Placeholders for memberships
              LOR4Membership sourceMembers = null;
              LOR4Membership destMembers = null;
              switch (sourceMember.MemberType)
              {
                case LOR4MemberType.Channel:
                  mappedDestChannelsCount--;
                  if (sourceMember.ZCount == 0) mappedSourceChannelsCount--;
                  break;
                case LOR4MemberType.RGBChannel:
                  mappedDestRGBChannelsCount--;
                  if (sourceMember.ZCount == 0) mappedSourceRGBChannelsCount--;
                  if (andChildren)
                  {
                    LOR4RGBChannel sourceRGB = (LOR4RGBChannel)sourceMember;
                    LOR4RGBChannel destRGB = (LOR4RGBChannel)destMember;
                    // Un-Map the Red, Green, Blue subchannels
                    // Assume success since there is not much chance of failure
                    UnMapMembers(sourceRGB.redChannel, destRGB.redChannel, false);
                    UnMapMembers(sourceRGB.grnChannel, destRGB.grnChannel, false);
                    UnMapMembers(sourceRGB.bluChannel, destRGB.bluChannel, false);
                  }
                  break;
                // Based on type (group, track, cosmic) get their child memberships
                case LOR4MemberType.ChannelGroup:
                  mappedDestGroupsCount--;
                  if (sourceMember.ZCount == 0) mappedSourceGroupsCount--;
                  if (andChildren)
                  {
                    // Need to cast them to get memberships
                    LOR4ChannelGroup sourceGroup = (LOR4ChannelGroup)sourceMember;
                    LOR4ChannelGroup destGroup = (LOR4ChannelGroup)destMember;
                    sourceMembers = sourceGroup.Members;
                    destMembers = destGroup.Members;
                  }
                  break;
                case LOR4MemberType.Track:
                  mappedDestTracksCount--;
                  if (sourceMember.ZCount == 0) mappedSourceTracksCount--;
                  if (andChildren)
                  {
                    // Need to cast them to get memberships
                    LOR4Track sourceTrack = (LOR4Track)sourceMember;
                    LOR4Track destTrack = (LOR4Track)destMember;
                    sourceMembers = sourceTrack.Members;
                    destMembers = destTrack.Members;
                  }
                  break;
                case LOR4MemberType.Cosmic:
                  //mappedDestCosmicCount--;
                  //if (sourceMember.ZCount == 0) mappedSourceCosmicCount--;
                  if (andChildren)
                  {
                    // Need to cast them to get memberships
                    LOR4Cosmic sourceCosmic = (LOR4Cosmic)sourceMember;
                    LOR4Cosmic destCosmic = (LOR4Cosmic)destMember;
                    sourceMembers = sourceCosmic.Members;
                    destMembers = destCosmic.Members;
                  }
                  break;
              }

              // Did we succeed at getting the child memberships?
              if (destMembers != null)
              {
                if (sourceMembers.Count == destMembers.Count)
                {
                  // Loop thru destination's children
                  for (int m = 0; m < destMembers.Count; m++)
                  {
                    // Get the destination
                    iLOR4Member destChild = destMembers[m];
                    // Get the source from the destination's MapTo
                    iLOR4Member sourceChild = destChild.MapTo;
                    if (sourceChild != null)
                    {
                      if (sourceChild.MemberType == destChild.MemberType)
                      {
                        //! Recurse!
                        bool childSuccess = UnMapMembers(sourceChild, destChild, true);
                        // If a child mapping failed, extend that to this parent
                        if (!childSuccess) success = false;
                      }
                      else
                      {
                        // Child types don't Match!
                        System.Diagnostics.Debugger.Break();
                      }
                    }
                    else
                    {
                      // Couldn't fetch source child from destination child's MapTo
                      System.Diagnostics.Debugger.Break();
                    }
                  } // loop thru dest members
                }
                else
                {
                  // Child counts don't match!
                  //System.Diagnostics.Debugger.Break();
                }
              }
              else
              {
                // Could not fetch destination members!
                //System.Diagnostics.Debugger.Break();
              } // Member Type
            } // End destination.MapTo = sourceMember
          } // End destination MapTo !null
          else
          {
            // destination's MapTo is null!
            System.Diagnostics.Debugger.Break();
          } // End destination's MapTo isn't null
        } // End if both source and destination are selected, and thus mapped
        else
        {
          string msg = "Trying to map " + LOR4SeqEnums.MemberName(sourceMember.MemberType) + "	'" + sourceMember.Name + "'";
          msg += " to " + LOR4SeqEnums.MemberName(destMember.MemberType) + " '" + destMember.Name + "'!";
          Fyle.BUG(msg);
        } // End source and destinations type match
      }
      else
      {
        // source and/or destination is not selected
        //System.Diagnostics.Debugger.Break();
      } // End source & destination selected
      if (success) MakeDirty();
      UpdateUI();
      return success;
    }

    private void ClearAllMappings()
    {
      if (seqSource != null)
      {
        for (int t = 1; t < seqSource.Tracks.Count; t++)
        {
          ClearAllMappings(seqSource.Tracks[t]);
        }
      }
      if (seqDest != null)
      {
        for (int t = 1; t < seqDest.Tracks.Count; t++)
        {
          ClearAllMappings(seqDest.Tracks[t]);
        }
      }
      mappedMemberCount = 0;
      mappedSourceChannelsCount = 0;
      mappedSourceGroupsCount = 0;
      mappedSourceRGBChannelsCount = 0;
      mappedSourceTracksCount = 0;
      //mappedSourceCosmicCount = 0;
      mappedDestChannelsCount = 0;
      mappedDestGroupsCount = 0;
      mappedDestRGBChannelsCount = 0;
      mappedDestTracksCount = 0;
      //mappedDestCosmicCount = 0;
      TreeUtils.ClearAllNodes(treeSource.Nodes);
      TreeUtils.ClearAllNodes(treeDest.Nodes);
      dirtyDest = false;
      dirtyMap = false;
      btnSaveNewSeq.Enabled = false;
      btnSaveMap.Enabled = false;

    }

    private void ClearAllMappings(iLOR4Member member)
    {
      member.Selected = false;
      member.MapTo = null;
      member.Tag = new List<iLOR4Member>();
      member.ZCount = 0;
      LOR4Membership members = null;
      switch (member.MemberType)
      {
        case LOR4MemberType.Channel:
          // Don't need to do anything additional
          break;
        case LOR4MemberType.RGBChannel:
          LOR4RGBChannel rgb = (LOR4RGBChannel)member;
          ClearAllMappings(rgb.redChannel);
          ClearAllMappings(rgb.grnChannel);
          ClearAllMappings(rgb.bluChannel);
          break;
        case LOR4MemberType.ChannelGroup:
          LOR4ChannelGroup group = (LOR4ChannelGroup)member;
          members = group.Members;
          break;
        case LOR4MemberType.Track:
          LOR4Track track = (LOR4Track)member;
          members = track.Members;
          break;
        case LOR4MemberType.Cosmic:
          LOR4Cosmic cosmic = (LOR4Cosmic)member;
          members = cosmic.Members;
          break;
      }
      if (members != null)
      {
        for (int n = 0; n < members.Count; n++)
        {
          ClearAllMappings(members[n]);
        }
      }
    }


    #endregion
    // End Map and Un-Map Region

    #region Copy Beat LOR4Track
    private void CopyBeats(LOR4Sequence seqNew)
    {
      LOR4Track newDestTrack = null;
      for (int t = 1; t < seqSource.Tracks.Count; t++)
      {
        string tn = seqSource.Tracks[t].Name.ToLower();
        if ((tn.IndexOf("beat") >= 0) ||
          (tn.IndexOf("song parts") >= 0) ||
          (tn.IndexOf("information") >= 0) ||
          (tn.IndexOf("o-rama") >= 0) ||
          (tn.IndexOf("vamperizer") >= 0))
        {
          newDestTrack = CopyTrack(seqSource.Tracks[t], seqNew);
          seqNew.MoveTrackByIndex(newDestTrack.Index, t);
        }
      }

    }
    private LOR4Track CopyTrack(LOR4Track sourceTrack, LOR4Sequence seqNew)
    {
      LOR4Track newTrack = seqNew.FindTrack(sourceTrack.Name);
      if (newTrack == null)
      {
        newTrack = seqNew.CreateNewTrack(sourceTrack.LineOut());
      }
      newTrack.Centiseconds = sourceTrack.Centiseconds;
      seqNew.Centiseconds = sourceTrack.Centiseconds;
      CopyItems(sourceTrack.Members, newTrack.Members);

      return newTrack;
    }
    private void CopyItems(LOR4Membership sourceParts, LOR4Membership destParts)
    {
      //foreach(iLOR4Member member in SourceTrack.Members)
      for (int i = 0; i < sourceParts.Count; i++)
      {
        iLOR4Member member = sourceParts.Items[i];
        LOR4MemberType partType = member.MemberType;
        switch (partType)
        {
          case LOR4MemberType.Channel:
            LOR4Channel sourceCh = (LOR4Channel)member;
            LOR4Channel destCh = (LOR4Channel)destParts.FindChannel(sourceCh.Name, true);
            //if (destCh == null)
            //{
            //	destCh = seqDest.CreateNewChannel(sourceCh.Name);
            //destCh = seqDest.CreateNewChannel(sourceCh.LineOut());
            //}
            CopyChannel(sourceCh, destCh);
            //destParts.Items.Add(destCh);
            //destParts.Add(destCh);
            break;
          case LOR4MemberType.RGBChannel:
            LOR4RGBChannel sourceRGB = (LOR4RGBChannel)member;
            LOR4RGBChannel destRGB = (LOR4RGBChannel)destParts.FindRGBChannel(sourceRGB.Name, true);
            //if (destRGB == null)
            //{
            //	destRGB = seqDest.CreateNewRGBChannel(sourceRGB.Name);
            //}
            CopyRGBchannel(sourceRGB, destRGB);
            //destParts.Items.Add(destRGB);
            //destParts.Add(destRGB);
            break;
          case LOR4MemberType.ChannelGroup:
            LOR4ChannelGroup sourceGroup = (LOR4ChannelGroup)member;
            LOR4ChannelGroup destGroup = (LOR4ChannelGroup)destParts.FindChannelGroup(sourceGroup.Name, true);
            //if (destGroup == null)
            //{
            //	destGroup = seqDest.CreateNewChannelGroup(sourceGroup.Name);
            //}
            //! Recurse!
            CopyItems(sourceGroup.Members, destGroup.Members);
            //destParts.Add(destGroup);
            break;
        }
      }
    }

    private LOR4Channel CopyChannel(LOR4Channel sourceChannel, LOR4Channel destChannel)
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
    private LOR4RGBChannel CopyRGBchannel(LOR4RGBChannel sourceRGB, LOR4RGBChannel destRGB)
    {
      // Copy RED LOR4Channel
      LOR4Channel chR = seqDest.FindChannel(sourceRGB.redChannel.Name);
      if (chR == null)
      {
        chR = seqDest.CreateNewChannel(sourceRGB.redChannel.Name);
      }
      CopyChannel(sourceRGB.redChannel, chR);
      chR.rgbParent = destRGB;
      destRGB.redChannel = chR;

      // Copy GREEN LOR4Channel
      LOR4Channel chG = seqDest.FindChannel(sourceRGB.grnChannel.Name);
      if (chG == null)
      {
        chG = seqDest.CreateNewChannel(sourceRGB.grnChannel.Name);
      }
      CopyChannel(sourceRGB.grnChannel, chG);
      chG.rgbParent = destRGB;
      destRGB.grnChannel = chG;

      // Copy BLUE LOR4Channel
      LOR4Channel chB = seqDest.FindChannel(sourceRGB.bluChannel.Name);
      if (chB == null)
      {
        chB = seqDest.CreateNewChannel(sourceRGB.bluChannel.Name);
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


    public void MakeDirty(bool isDirty = true)
    {
      if (isDirty != dirtyMap)
      {
        if (isDirty)
        {
          dirtyMap = isDirty;
          dirtyDest = isDirty;
          btnSaveMap.Enabled = true;
          btnSaveNewSeq.Enabled = true;
        }
        else
        {

        }
      }
    }






  }
}
