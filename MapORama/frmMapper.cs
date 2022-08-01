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
using LOR4;
using FileHelper;
using FuzzORama;
using FormHelper;
//using RecentlyUsed;
using Syncfusion.Windows.Forms.Tools;

namespace UtilORama4
{
  public partial class frmMapper : Form
  {
    private bool firstShown = false;
    private Control currentToolTipControl = null;
    private bool isWiz = Fyle.IsWizard || Fyle.IsAWizard;
    private static Settings userSettings = Settings.Default;

    private MRUoRama mruSources = new MRUoRama(MRUoRama.FileType.Sequences, Program.Name, true, "Source");
    private MRUoRama mruDests = new MRUoRama(MRUoRama.FileType.Sequences, Program.Name, true, "Destination");
    private MRUoRama mruMaps = new MRUoRama(MRUoRama.FileType.Maps, Program.Name);



    public frmMapper()
    {
      InitializeComponent();
    } // Constructor
    private void InitForm()
    {
      //RestoreFormPosition();
      this.RestoreView();
      RecallSettings();
    } // end InitForm
    private void ImBusy(bool workingHard)
    {
      pnlAll.Enabled = !workingHard;
      if (workingHard)
      {
        this.Cursor = Cursors.WaitCursor;
      }
      else
      {
        this.Cursor = Cursors.Default;
        System.Media.SystemSounds.Beep.Play();

      }

    }
    private void SaveSettings()
    {
      bool valid = false;
      destFile = userSettings.LastDestFile;
      if (destFile.Length > 6)
      {
        if (Fyle.Exists(destFile))
        {
          userSettings.LastDestFile = destFile;
        }
      }
      if (sourceFile.Length > 6)
      {
        if (!Fyle.Exists(sourceFile))
        {
          userSettings.LastSourceFile = sourceFile;
        }
      }
      if (mapFile.Length > 6)
      {
        if (!Fyle.Exists(mapFile))
        {
          userSettings.LastMapFile = mapFile;
        }
      }
      if (saveFile.Length > 6)
      {
        if (!Fyle.Exists(saveFile))
        {
          userSettings.LastSaveFile = saveFile;
        }
      }

      userSettings.AutoLaunch = chkAutoLaunch.Checked;
      userSettings.UseRamps = chkRamps.Checked;
      userSettings.SourceOnRight = sourceOnRight;

      userSettings.BasePath = basePath;
      userSettings.Save();
    }

    private void RecallSettings()
    {
      bool valid = false;
      SeqFolder = LOR4Admin.DefaultSequencesPath;
      logHomeDir = Fyle.GetMyTempPath();

      //destFile = userSettings.LastDestFile;
      destFile = mruDests.GetFile(0);
      if (destFile.Length > 6)
      {
        valid = Fyle.IsValidPath(destFile, true);
      }
      if (!valid) destFile = LOR4Admin.DefaultChannelConfigsPath;
      if (!File.Exists(destFile))
      {
        destFile = LOR4Admin.DefaultChannelConfigsPath;
        //userSettings.LastDestFile = destFile;
      }

      valid = false;
      //sourceFile = userSettings.LastSourceFile;
      sourceFile = mruSources.GetFile(0);
      if (sourceFile.Length > 6)
      {
        valid = Fyle.IsValidPath(sourceFile, true);
      }
      if (!valid) sourceFile = LOR4Admin.DefaultSequencesPath;
      if (!File.Exists(sourceFile))
      {
        sourceFile = LOR4Admin.DefaultSequencesPath;
        //userSettings.LastSourceFile = sourceFile;
      }

      valid = false;
      //mapFile = userSettings.LastMapFile;
      mapFile = mruMaps.GetFile(0);
      if (mapFile.Length > 6)
      {
        valid = Fyle.IsValidPath(mapFile, true);
      }
      if (!valid) mapFile = LOR4Admin.DefaultChannelConfigsPath;
      if (!File.Exists(mapFile))
      {
        mapFile = LOR4Admin.DefaultChannelConfigsPath;
      }

      valid = false;
      //TODO Review, and possibly change...
      saveFile = userSettings.LastSaveFile;
      if (saveFile.Length > 6)
      {
        valid = Fyle.IsValidPath(saveFile, true);
      }
      if (!valid) saveFile = LOR4Admin.DefaultSequencesPath;
      if (!File.Exists(saveFile))
      {
        saveFile = LOR4Admin.DefaultSequencesPath;
      }

      sourceOnRight = userSettings.SourceOnRight;
      ArrangePanes(sourceOnRight);

      userSettings.BasePath = basePath;
      userSettings.Save();

      chkAutoLaunch.Checked = userSettings.AutoLaunch;
      btnEaves.Visible = isWiz;
      chkRamps.Checked = userSettings.UseRamps;

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
        if (arg.Substring(4).IndexOf(".") > LOR4Admin.UNDEFINED) isFile++;  // contains a period
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
          //ProcessSourcesBatch(batch_fileList);
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
        //if (arg.Substring(4).IndexOf(".") > LOR4Admin.UNDEFINED) isFile++;  // contains a period
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
            LoadDestFile(file);
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
        if (seqDest.Channels.Count < 3)
        {
          if (File.Exists(destFile))
          {
            LoadDestFile(destFile);
          }
        }
        if (seqDest.Channels.Count < 3)
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
            //ProcessSourcesBatch(batch_fileList);
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
    private void ArrangePanes(bool sourceNowOnRight)
    {
      const int lft = 15;
      const int rgt = 415;

      if (sourceNowOnRight)
      {
        lblSourceFile.Left = rgt;
        lblSourceTree.Left = rgt;
        cboSourceFile.Left = rgt;
        treeSource.Left = rgt;
        picPreviewSource.Left = rgt;
        lblSourceMappedTo.Left = rgt;
        btnBrowseSource.Left = 721;

        lblDestFile.Left = lft;
        lblDestTree.Left = lft;
        cboDestFile.Left = lft;
        treeDest.Left = lft;
        //picPreviewDest.Left = lft;
        pnlOverwrite.Left = lft;
        lblDestMappedTo.Left = lft;
        btnBrowseDest.Left = 321;

        mnuSourceLeft.Checked = false;
        mnuSourceRight.Checked = true;
      }
      else
      {
        lblSourceFile.Left = lft;
        lblSourceTree.Left = lft;
        cboSourceFile.Left = lft;
        treeSource.Left = lft;
        picPreviewSource.Left = lft;
        lblSourceMappedTo.Left = lft;
        btnBrowseSource.Left = 321;

        lblDestFile.Left = rgt;
        lblDestTree.Left = rgt;
        cboDestFile.Left = rgt;
        treeDest.Left = rgt;
        //picPreviewDest.Left = rgt;
        pnlOverwrite.Left = rgt;
        lblDestMappedTo.Left = rgt;
        btnBrowseDest.Left = 721;

        mnuSourceLeft.Checked = true;
        mnuSourceRight.Checked = false;
      }

      sourceOnRight = sourceNowOnRight;
      userSettings.SourceOnRight = sourceOnRight;
      userSettings.Save();
    }
    private void btnBrowseSource_Click(object sender, EventArgs e)
    {
      BrowseSourceFile();
    }
    private void btnBrowseDest_Click(object sender, EventArgs e)
    {
      BrowseDestFile();
    }
    private void btnMap_Click(object sender, EventArgs e)
    {
      if (currentSourceMember != null)
      {
        if (currentDestMember != null)
        {
          if (currentSourceMember.MemberType == currentDestMember.MemberType)
          {
            bool success = MapMembers(currentSourceMember, currentDestMember, true);
            if (success)
            {

              btnMap.Enabled = false;
              btnUnmap.Enabled = true;
              string lbltxt = currentSourceMember.Name + " is mapped to " + currentDestMember.Name;
              lblDestMappedTo.Text = lbltxt;
              UpdateSourceMappedTo(currentSourceMember);
              TreeUtils.HighlightMember(currentSourceMember, ColorDestSelectedFG, ColorDestSelectedBG);
            }
          }
        }
      }
    } // end btnMap_Click

    private string UpdateSourceMappedTo(iLOR4Member sourceMember)
    {
      string txt = sourceMember.Name;
      if (currentSourceMember != null)
      {
        if (currentSourceMember.Tag == null)
        {
          currentSourceMember.Tag = new List<iLOR4Member>();
        }
        if (currentSourceMember.ZCount < 1)
        {
          txt += " is unmapped";
        }
        else
        {
          txt += " is mapped to ";
          List<iLOR4Member> destMapList = (List<iLOR4Member>)sourceMember.Tag;
          for (int d = 0; d < destMapList.Count; d++)
          {
            txt += destMapList[d].Name + ", ";
          }
          txt = txt.Substring(txt.Length - 2);
          int p = txt.LastIndexOf(", ");
          txt = txt.Substring(0, p + 2) + "and " + txt.Substring(p + 2);
        }
        lblSourceMappedTo.Text = txt;
      }
      return txt;
    }


    private void btnUnmap_Click(object sender, EventArgs e)
    {
      if (currentSourceMember != null)
      {
        if (currentDestMember != null)
        {
          if (currentSourceMember.MemberType == currentDestMember.MemberType)
          {
            bool success = UnMapMembers(currentSourceMember, currentDestMember, true);
            if (success)
            {

              btnMap.Enabled = true;
              btnUnmap.Enabled = false;
              string lbltxt = "Nothing is mapped to " + currentDestMember.Name;
              lblDestMappedTo.Text = lbltxt;
              UpdateSourceMappedTo(currentSourceMember);
              TreeUtils.HighlightMember(currentSourceMember, ColorSourceSelectedFG, ColorSourceSelectedBG);
            }
          }
        }
      }
    }
    private void frmRemapper_Shown(object sender, EventArgs e)
    { }
    private void FirstShow()
    {
      string msg;
      DialogResult dr;

      ProcessCommandLine();
      if (batch_fileCount < 1)
      {
        if (File.Exists(destFile))
        {
          msg = "Load last Destination Sequence file: '" + Path.GetFileName(destFile) + "'?";
          dr = MessageBox.Show(this, msg, "Load Last Destination File?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
          //dr = DialogResult.Yes; //! For Testing
          if (dr == DialogResult.Yes)
          {
            LoadDestFile(destFile);
          }
        }
        if (File.Exists(sourceFile))
        {
          msg = "Load last Source Sequence file: '" + Path.GetFileName(sourceFile) + "'?";
          dr = MessageBox.Show(this, msg, "Load Last Source File?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
          //dr = DialogResult.Yes; //! For Testing
          if (dr == DialogResult.Yes)
          {
            //seqSource.ReadSequenceFile(sourceFile);
            LoadSourceFile(sourceFile);
            //LOR4Admin.TreeFillChannels(treeSource, seqSource, sourceNodesBySI, false, false);
            // Is a destination also already loaded?
          }
        } // end last sequence file exists
          //! Remarked for Testing
        AskToMap();
      }
    } // end form shown event
    private void btnSummary_Click(object sender, EventArgs e)
    {
      ImBusy(true);
      frmMapList dlgList = new frmMapList(seqSource, seqDest, imlTreeIcons);
      //dlgList.Left = this.Left + 4;
      //dlgList.Top = this.Top + 25;
      Point l = new Point(4, 25);
      dlgList.Location = l;

      dlgList.ShowDialog(this);
      //DialogResult result = dlgList.ShowDialog();
      ImBusy(false);
    }
    private void frmRemapper_FormClosing(object sender, FormClosingEventArgs e)
    {
      SaveSettings();
      //SaveFormPosition();
      this.SaveView();
    }
    private void btnSaveMap_Click(object sender, EventArgs e)
    {
      SaveMap();
    }
    private void pnlAll_Paint(object sender, PaintEventArgs e)
    {

    }
    private void frmRemapper_Load(object sender, EventArgs e)
    {
      InitForm();
    }
    private void btnAutoMap_Click(object sender, EventArgs e)
    {
      AutoMap();
    }
    private void btnLoadMap_Click(object sender, EventArgs e)
    {
      BrowseForMap();
    }
    private void frmRemapper_Paint(object sender, PaintEventArgs e)
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
      ImBusy(true);
      frmAbout aboutBox = new frmAbout();
      aboutBox.Icon = this.Icon;
      aboutBox.Text = "About Map-O-Rama";
      aboutBox.AppIcon = picAboutIcon.Image;
      aboutBox.ShowDialog(this);
      ImBusy(false);
    }
    private void btnSaveNewSeq_Click(object sender, EventArgs e)
    {
      SaveNewMappedSequence();
    }
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
    private void mnuOpenDest_Click(object sender, EventArgs e)
    {
      BrowseDestFile();
    }
    private void mnuOpenSource_Click(object sender, EventArgs e)
    {
      BrowseSourceFile();
    }
    private void mnuOpenMap_Click(object sender, EventArgs e)
    {
      BrowseForMap();
    }
    private void mnuSelect_Click(object sender, EventArgs e)
    {

    }
    private void mnuSourceLeft_Click(object sender, EventArgs e)
    {
      ArrangePanes(false);
    }
    private void mnuSourceRight_Click(object sender, EventArgs e)
    {
      ArrangePanes(true);
    }
    private void mnuMatchOptions_Click(object sender, EventArgs e)
    {
      frmOptions opt = new frmOptions();
      opt.ShowDialog(this);
    }
    private void mnuAutoMap_Click(object sender, EventArgs e)
    {
      AutoMap();
    }
    private void chkAutoLaunch_CheckedChanged(object sender, EventArgs e)
    {
      userSettings.AutoLaunch = chkAutoLaunch.Checked;
      userSettings.Save();
    }
    private void lblDebug_Click(object sender, EventArgs e)
    {
      //string msg = "GLB";
      //msg += LeftBushesChildCount().ToString();
      //lblDebug.Text = msg;
    }
    private void btnEaves_Click(object sender, EventArgs e)
    {
    }
    private void treeDest_AfterSelect(object sender, EventArgs e)
    {
      if (treeDest.SelectedNode != null)
      {
        TreeNodeAdv newDestNode = treeDest.SelectedNode;
        DestNode_Click(newDestNode);
      }
    }
    private void treeSource_AfterSelect(object sender, EventArgs e)
    {
      if (treeSource.SelectedNode != null)
      {
        TreeNodeAdv newSourceNode = treeSource.SelectedNode;
        SourceNode_Click(newSourceNode);
      }
    } // end class frmRemapper
    private void frmRemapper_MouseMove(object sender, MouseEventArgs e)
    {
      /*
			Control control = GetChildAtPoint(e.Location);
			if (control != null)
			{
				string toolTipString = ttip.GetToolTip(control);
				this.ttip.ShowAlways = true;
				// trigger the tooltip with no delay and some basic positioning just to give you an idea
				ttip.Show(toolTipString, control, control.Width / 2, control.Height / 2);
			}
			*/
    }
    private void pnlAll_MouseMove(object sender, MouseEventArgs e)
    {
      /*
			 * Control control = GetChildAtPoint(e.Location);
			if (control != null)
			{
				if (!control.Enabled && currentToolTipControl == null)
				{
					string toolTipString = ttip.GetToolTip(control);
					// trigger the tooltip with no delay and some basic positioning just to give you an idea
					ttip.Show(toolTipString, control, control.Width / 2, control.Height / 2);
					currentToolTipControl = control;
				}
			}
			else
			{
				if (currentToolTipControl != null) ttip.Hide(currentToolTipControl);
				currentToolTipControl = null;
			}
			*/

      var parent = sender as Control;
      if (parent == null)
      {
        return;
      }
      var ctrl = parent.GetChildAtPoint(e.Location);
      if (ctrl != null && !ctrl.Enabled)
      {
        if (ctrl.Visible && ttip.Tag == null)
        {
          var tipstring = ttip.GetToolTip(ctrl);
          ttip.Show(tipstring, ctrl, ctrl.Width / 2, ctrl.Height / 2);
          ttip.Tag = ctrl;
        }
      }
      else
      {
        ctrl = ttip.Tag as Control;
        if (ctrl != null)
        {
          ttip.Hide(ctrl);
          ttip.Tag = null;
        }
      }
    }
    private void StatusMessage(string message)
    {
      // For now at least... I think I'm gonna use this just to show WHY the 'Map' button is not enabled


      lblMessage.Text = message;
      lblMessage.Visible = (message.Length > 0);
      lblMessage.Left = (this.Width - lblMessage.ClientSize.Width) / 2;

    }
    private void ShowProgressBars(int howMany)
    {
      if (howMany == 0)
      {
        btnLoadMap.Visible = true;
        btnSaveMap.Visible = true;
        cboMappingFile.Visible = true;
        prgBarInner.Visible = false;
        prgBarOuter.Visible = false;
      }
      else
      {
        btnLoadMap.Visible = false;
        btnSaveMap.Visible = false;
        cboMappingFile.Visible = false;
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

  }
}// end namespace UtilORama4
