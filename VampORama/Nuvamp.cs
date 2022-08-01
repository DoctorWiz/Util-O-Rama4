using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Configuration;
using System.Threading;
using Microsoft.Win32;
using xLights22;
using LOR4;
using FileHelper;
using Musik;
//using Ini;
using TagLib;
using TagLib.Mpeg;
using TagLib.Ogg;
using TagLib.Flac;
//using TagLib.identity3v1;
//using TagLib.identity3v2;
using TagLib.Aac;
using TagLib.Aiff;
using TagLib.Asf;
using TagLib.MusePack;
using TagLib.NonContainer;
using System.Diagnostics.Eventing.Reader;


namespace UtilORama4
{
  public partial class frmVamp //: Form
  {
    private string annotatorProgram = "";
    private string StepSizeExample = "ERROR: BarBeatTracker::initialize: Unsupported step size for this sample rate: 441 (wanted 512)";
    //TODO: Scan output for StepSize Error, and correct it and re-run vamp if err detected
    private string StepSizeError = "ERROR: [Plugin]::initialize: Unsupported step size for this sample rate: [BadSize] (wanted [GoodSize])";
    private Musik.AudioInfo audioData;
    string fileSongTemp = "song.mp3";
    private object syncGate = new object();
    private Process cmdProc;
    private StringBuilder outLog = new StringBuilder();
    private StreamWriter writer = null;
    private bool outputChanged;
    private string songTitle = "[Unknown]";
    private string songArtist = "[Unknown]";
    private const string BY = " by ";
    private string theSong = "[Unknown] by [Unknown]";
    private string fileCurrent = "";


    #region Vamp Specific
    // These first five functions are the ones to be updated whenever a
    // new type of Vamp is added

    public void FillCombos()
    {
      //////////////////////
      //! Bars and Beats //
      ////////////////////
      cboMethodBarsBeats.Items.Clear();
      foreach (string plugName in VampBarBeats.availablePluginNames)
      {
        cboMethodBarsBeats.Items.Add(plugName);
      }
      cboLabelsBarBeats.Items.Clear();
      foreach (vamps.LabelType lblType in VampBarBeats.allowableLabels)
      {
        cboLabelsBarBeats.Items.Add(vamps.LabelName(lblType));
      }
      cboAlignBarBeats.Items.Clear();
      foreach (vamps.AlignmentType alType in VampBarBeats.allowableAlignments)
      {
        cboAlignBarBeats.Items.Add(vamps.AlignmentName(alType));
      }
      SetCombo(cboMethodBarsBeats, userSettings.methodBarsBeats);
      timeSignature = userSettings.timeSignature;
      if (timeSignature == 3) swTrackBeat.Checked = true; else swTrackBeat.Checked = false;
      startBeat = userSettings.startBeat;
      txtStartBeat.Text = startBeat.ToString();
      vscStartBeat.Value = (5 - startBeat);
      SetCombo(cboDetectBarBeats, userSettings.detectBars);
      chkWhiten.Checked = userSettings.whiteBarsBeats;
      chkBars.Checked = userSettings.doBars;
      chkBeatsFull.Checked = userSettings.DoBeatsFull;
      chkBeatsHalf.Checked = userSettings.doBeatsHalf;
      chkBeatsThird.Checked = userSettings.doBeatsThird;
      chkBeatsQuarter.Checked = userSettings.doBeatsQuarter;
      SetCombo(cboAlignBarBeats, userSettings.alignBarsBeats);
      SetCombo(cboLabelsBarBeats, userSettings.labelBarBeats);

      ///////////////////
      //! Note Onsets //
      /////////////////
      cboMethodOnsets.Items.Clear();
      foreach (string plugName in VampNoteOnsets.availablePluginNames)
      {
        cboMethodOnsets.Items.Add(plugName);
      }
      cboLabelsOnsets.Items.Clear();
      foreach (vamps.LabelType lblType in VampNoteOnsets.allowableLabels)
      {
        cboLabelsOnsets.Items.Add(vamps.LabelName(lblType));
      }
      cboAlignOnsets.Items.Clear();
      foreach (vamps.AlignmentType alType in VampNoteOnsets.allowableAlignments)
      {
        cboAlignOnsets.Items.Add(vamps.AlignmentName(alType));
      }
      SetCombo(cboMethodOnsets, userSettings.methodOnsets);
      SetCombo(cboOnsetsDetect, userSettings.detectOnsets);
      vscSensitivity.Value = userSettings.sensitivityOnsets;
      chkWhiten.Checked = userSettings.whiteOnsets;
      chkNoteOnsets.Checked = userSettings.doOnsets;
      SetCombo(cboLabelsOnsets, userSettings.labelOnsets);
      SetCombo(cboAlignOnsets, userSettings.alignOnsets);
      SetCombo(cboStepSize, userSettings.stepSize);
      swRamps.Checked = userSettings.Ramps;

      ////////////////////////////////
      //! Polyphonic Transcription //
      //////////////////////////////
      cboMethodPolyphonic.Items.Clear();
      foreach (string plugName in VampPolyphonic.availablePluginNames)
      {
        cboMethodPolyphonic.Items.Add(plugName);
      }
      cboLabelsPolyphonic.Items.Clear();
      foreach (vamps.LabelType lblType in VampPolyphonic.allowableLabels)
      {
        cboLabelsPolyphonic.Items.Add(vamps.LabelName(lblType));
      }
      cboAlignPolyphonic.Items.Clear();
      foreach (vamps.AlignmentType alType in VampPolyphonic.allowableAlignments)
      {
        cboAlignPolyphonic.Items.Add(vamps.AlignmentName(alType));
      }
      SetCombo(cboMethodPolyphonic, userSettings.methodTranscribe);
      chkPolyphonic.Checked = userSettings.doTranscribe;
      SetCombo(cboLabelsPolyphonic, userSettings.labelTranscribe);
      SetCombo(cboAlignPolyphonic, userSettings.alignTranscribe);

      /////////////////////
      //! Pitch and Key //
      ///////////////////
      cboMethodPitchKey.Items.Clear();
      foreach (string plugName in VampPitchKey.availablePluginNames)
      {
        cboMethodPitchKey.Items.Add(plugName);
      }
      cboLabelsPitchKey.Items.Clear();
      foreach (vamps.LabelType lblType in VampPitchKey.allowableLabels)
      {
        cboLabelsPitchKey.Items.Add(vamps.LabelName(lblType));
      }
      cboAlignPitchKey.Items.Clear();
      foreach (vamps.AlignmentType alType in VampPitchKey.allowableAlignments)
      {
        cboAlignPitchKey.Items.Add(vamps.AlignmentName(alType));
      }
      SetCombo(cboMethodPitchKey, userSettings.methodPitchKey);
      chkPitchKey.Checked = userSettings.doPitchKey;
      SetCombo(cboLabelsPitchKey, userSettings.labelPitchKey);
      SetCombo(cboAlignPitchKey, userSettings.alignPitchKey);

      ///////////////////
      //!  T E M P O  //
      /////////////////
      cboMethodTempo.Items.Clear();
      foreach (string plugName in VampTempo.availablePluginNames)
      {
        cboMethodTempo.Items.Add(plugName);
      }
      cboLabelsTempo.Items.Clear();
      foreach (vamps.LabelType lblType in VampTempo.allowableLabels)
      {
        cboLabelsTempo.Items.Add(vamps.LabelName(lblType));
      }
      cboAlignTempo.Items.Clear();
      foreach (vamps.AlignmentType alType in VampTempo.allowableAlignments)
      {
        cboAlignTempo.Items.Add(vamps.AlignmentName(alType));
      }
      SetCombo(cboMethodTempo, userSettings.methodTempo);
      chkTempo.Checked = userSettings.doTempo;
      SetCombo(cboLabelsTempo, userSettings.labelTempo);
      SetCombo(cboAlignTempo, userSettings.alignTempo);


      ///////////////////
      //!  SEGMENTS  //
      /////////////////
      cboMethodSegments.Items.Clear();
      foreach (string plugName in VampSegments.availablePluginNames)
      {
        cboMethodSegments.Items.Add(plugName);
      }
      cboLabelsSegments.Items.Clear();
      foreach (vamps.LabelType lblType in VampSegments.allowableLabels)
      {
        cboLabelsSegments.Items.Add(vamps.LabelName(lblType));
      }
      cboAlignSegments.Items.Clear();
      foreach (vamps.AlignmentType alType in VampSegments.allowableAlignments)
      {
        cboAlignSegments.Items.Add(vamps.AlignmentName(alType));
      }
      chkSegments.Checked = userSettings.doSegments;
      SetCombo(cboAlignSegments, userSettings.alignSegments);
      SetCombo(cboMethodSegments, userSettings.methodSegments);
      SetCombo(cboLabelsSegments, userSettings.labelSegments);

      ////////////////////
      //!  Chromagram  //
      //////////////////
      cboMethodChromagram.Items.Clear();
      foreach (string plugName in VampChromagram.availablePluginNames)
      {
        cboMethodChromagram.Items.Add(plugName);
      }
      cboLabelsChromagram.Items.Clear();
      foreach (vamps.LabelType lblType in VampChromagram.allowableLabels)
      {
        cboLabelsChromagram.Items.Add(vamps.LabelName(lblType));
      }
      cboAlignChromagram.Items.Clear();
      foreach (vamps.AlignmentType alType in VampChromagram.allowableAlignments)
      {
        cboAlignChromagram.Items.Add(vamps.AlignmentName(alType));
      }
      chkChromathing.Checked = userSettings.doChromagram;
      SetCombo(cboAlignChromagram, userSettings.alignChromagram);
      SetCombo(cboMethodChromagram, userSettings.methodChromagram);
      SetCombo(cboLabelsChromagram, userSettings.labelChromagram);

      /////////////////////////
      //!  Chords Chords  //
      ///////////////////////
      cboMethodChords.Items.Clear();
      foreach (string plugName in VampChords.availablePluginNames)
      {
        cboMethodChords.Items.Add(plugName);
      }
      cboLabelsChords.Items.Clear();
      foreach (vamps.LabelType lblType in VampChords.allowableLabels)
      {
        cboLabelsChords.Items.Add(vamps.LabelName(lblType));
      }
      cboAlignChords.Items.Clear();
      foreach (vamps.AlignmentType alType in VampChords.allowableAlignments)
      {
        cboAlignChords.Items.Add(vamps.AlignmentName(alType));
      }
      chkChords.Checked = userSettings.doChords;
      SetCombo(cboAlignChords, userSettings.alignChords);
      SetCombo(cboMethodChords, userSettings.methodChords);
      SetCombo(cboLabelsChords, userSettings.labelChords);

    }

    private void SaveCombos()
    {
      //! Bars and Beats
      userSettings.doBars = chkBars.Checked;
      userSettings.DoBeatsFull = chkBeatsFull.Checked;
      userSettings.doBeatsHalf = chkBeatsHalf.Checked;
      userSettings.doBeatsThird = chkBeatsThird.Checked;
      userSettings.doBeatsQuarter = chkBeatsQuarter.Checked;
      userSettings.labelBarBeats = cboLabelsBarBeats.Text;
      userSettings.alignBarsBeats = cboAlignBarBeats.Text;
      userSettings.Ramps = swRamps.Checked;

      //! Note Onsets
      userSettings.methodOnsets = cboMethodOnsets.Text;
      userSettings.detectOnsets = cboOnsetsDetect.Text;
      userSettings.sensitivityOnsets = vscSensitivity.Value;
      userSettings.whiteOnsets = chkWhiten.Checked;
      userSettings.doOnsets = chkNoteOnsets.Checked;
      userSettings.labelOnsets = cboLabelsOnsets.Text;
      userSettings.alignOnsets = cboAlignOnsets.Text;
      userSettings.stepSize = cboStepSize.Text;

      //! Polyphonic Transcription
      userSettings.methodTranscribe = cboMethodPolyphonic.Text;
      userSettings.doTranscribe = chkPolyphonic.Checked;
      userSettings.labelTranscribe = cboLabelsPolyphonic.Text;
      userSettings.alignTranscribe = cboAlignPolyphonic.Text;

      //! Pitch & Key
      userSettings.methodPitchKey = cboMethodPitchKey.Text;
      userSettings.doPitchKey = chkPitchKey.Checked;
      userSettings.labelPitchKey = cboLabelsPitchKey.Text;
      userSettings.alignPitchKey = cboAlignPitchKey.Text;

      //! Tempo
      userSettings.methodTempo = cboMethodTempo.Text;
      userSettings.doTempo = chkTempo.Checked;
      userSettings.labelTempo = cboLabelsTempo.Text;
      userSettings.alignTempo = cboAlignTempo.Text;

      //! Segments
      userSettings.doSegments = chkSegments.Checked;
      userSettings.methodSegments = cboMethodSegments.Text;
      userSettings.alignSegments = cboAlignSegments.Text;
      userSettings.labelSegments = cboLabelsSegments.Text;

      //! Chromagram
      userSettings.doChromagram = chkChromathing.Checked;
      userSettings.methodChromagram = cboMethodChromagram.Text;
      userSettings.alignChromagram = cboAlignChromagram.Text;
      userSettings.labelChromagram = cboLabelsChromagram.Text;

      //! Chords
      userSettings.doChords = chkChords.Checked;
      userSettings.methodChords = cboMethodChords.Text;
      userSettings.alignChords = cboAlignChords.Text;
      userSettings.labelChords = cboLabelsChords.Text;

      //! Spectrum
      //userSettings.methodSpectrum = cboMethodChromagram.Text;
      //userSettings.doSpectrum = chkChromagram.Checked;
      //userSettings.labelSpectrum = cboLabelsChromagram.Text;
      //userSettings.alignSpectrum = cboAlignChromagram.Text;
    }




    private int RunSelectedVamps()
    {
      int completed = 0;
      int err = 0;
      string msg = "";
      //vamps.AlignmentType algn = vamps.AlignmentType.None;
      //vamps.LabelType lbls = vamps.LabelType.None;
      bool reuse = chkReuse.Checked;
      Annotator.ReuseResults = chkReuse.Checked;
      Annotator.Whiten = chkWhiten.Checked;
      Annotator.StepSize = Int32.Parse(cboStepSize.Text);
      if (swTrackBeat.Checked) Annotator.BeatsPerBar = 3; else Annotator.BeatsPerBar = 4;
      //Annotator.UseRamps = swRamps.Checked;
      Annotator.FirstBeat = Int32.Parse(txtStartBeat.Text);
      //Annotator.WorkPath = pathWork;



      //!/////////////////
      //! BARS AND BEATS //
      //!/////////////////
      //if (chkBarsBeats.Checked)
      // ALWAYS run Bars and Beats since these timings are used for alignments
      if (true)
      {
        if ((!reuse) || (!Fyle.Exists(VampBarBeats.FileResults)))
        {
          StatusUpdate("Annotating Bars and Beats");
          if (logWindow != null) logWindow.Text = OutputTitle + " - Bars and Beats";
          msg = "**********************"; OutputText(msg);
          msg = "**  BARS and BEATS  **"; OutputText(msg);
          msg = "**********************"; OutputText(msg);
          int pluginIndex = cboMethodBarsBeats.SelectedIndex;
          int detectionMethod = cboDetectBarBeats.SelectedIndex;
          //whiten = chkWhiteBarBeats.Checked;

          err = VampBarBeats.PrepareVampConfig(pluginIndex, detectionMethod);
          if (err == 0)
          {
            string fileConfig = VampBarBeats.filesAvailableConfigs[pluginIndex];
            string vampParams = VampBarBeats.availablePluginCodes[pluginIndex];
            err = VampThatSong(fileSongTemp, vampParams, fileConfig, VampBarBeats.ResultsName);
            //if (Annotator.NeedStepSize > 0)
            if (Annotator.StepErrFlag)
            {
              //! Oh shit, detected a step size error!
              FixStepSize();
              err = VampBarBeats.PrepareVampConfig(pluginIndex, detectionMethod);
              err = VampThatSong(fileSongTemp, vampParams, fileConfig, VampBarBeats.ResultsName);
              //TODO Verify it worked...
            }
          }
        }
      }
      lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();


      //!////////////////
      //! NOTE ONSETS //
      //!/////////////
      if (chkNoteOnsets.Checked)
      {
        if ((!reuse) || (!Fyle.Exists(VampNoteOnsets.FileResults)))
        {
          StatusUpdate("Annotating Note Onsets");
          if (logWindow != null) logWindow.Text = OutputTitle + " - Note Onsets";
          msg = "\r\n\r\n*******************"; OutputText(msg);
          msg = "**  NOTE ONSETS  **"; OutputText(msg);
          msg = "*******************"; OutputText(msg);
          int pluginIndex = cboMethodOnsets.SelectedIndex;
          int detectionMethod = cboOnsetsDetect.SelectedIndex;
          //whiten = chkWhiten.Checked;

          err = VampNoteOnsets.PrepareToVamp(fileSongTemp, pluginIndex, detectionMethod);
          if (err == 0)
          {
            string fileConfig = VampNoteOnsets.filesAvailableConfigs[pluginIndex];
            string vampParams = VampNoteOnsets.availablePluginCodes[pluginIndex];
            err = VampThatSong(fileSongTemp, vampParams, fileConfig, VampNoteOnsets.ResultsName);
          }
        }
      }

      //!///////////////////////
      //! POLYPHONIC TRANSCRIPTION //
      //!/////////////////////
      if (chkPolyphonic.Checked)
      {
        if ((!reuse) || (!Fyle.Exists(VampPolyphonic.FileResults)))
        {
          StatusUpdate("Annotating Polyphonic Transcription");
          if (logWindow != null) logWindow.Text = OutputTitle + " - Polyphonic Transcription";
          msg = "\r\n\r\n********************************"; OutputText(msg);
          msg = "**  POLYPHONIC TRANSCRIPTION  **"; OutputText(msg);
          msg = "********************************"; OutputText(msg);
          int pluginIndex = cboMethodPolyphonic.SelectedIndex;
          //whiten = chkWhiten.Checked;

          err = VampPolyphonic.PrepareToVamp(fileSongTemp, pluginIndex);
          if (err == 0)
          {
            string fileConfig = VampPolyphonic.filesAvailableConfigs[pluginIndex];
            string vampParams = VampPolyphonic.availablePluginCodes[pluginIndex];
            err = VampThatSong(fileSongTemp, vampParams, fileConfig, VampPolyphonic.ResultsName);
          }
        }
      }

      lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();

      //!///////////////////////
      //! PITCH & KEY //
      //!/////////////////////
      if (chkPitchKey.Checked)
      {
        if ((!reuse) || (!Fyle.Exists(VampPitchKey.FileResults)))
        {
          StatusUpdate("Annotating Pitch and Key");
          if (logWindow != null) logWindow.Text = OutputTitle + " - Pitch and Key";
          msg = "\r\n\r\n*********************"; OutputText(msg);
          msg = "**  PITCH and KEY  **"; OutputText(msg);
          msg = "*********************"; OutputText(msg);
          int pluginIndex = cboMethodPitchKey.SelectedIndex;
          //whiten = chkWhiten.Checked;

          err = VampPitchKey.PrepareToVamp(fileSongTemp, pluginIndex);
          if (err == 0)
          {
            string fileConfig = VampPitchKey.filesAvailableConfigs[pluginIndex];
            string vampParams = VampPitchKey.availablePluginCodes[pluginIndex];
            err = VampThatSong(fileSongTemp, vampParams, fileConfig, VampPitchKey.ResultsName);
          }
        }
      }

      //!///////////////////////
      //!  T E M P O  //
      //!/////////////////////
      if (chkTempo.Checked)
      {
        if ((!reuse) || (!Fyle.Exists(VampTempo.FileResults)))
        {
          StatusUpdate("Annotating Tempo Changes");
          if (logWindow != null) logWindow.Text = OutputTitle + " - Tempo Changes";
          msg = "\r\n\r\n*********************"; OutputText(msg);
          msg = "**  TEMPO CHANGES  **"; OutputText(msg);
          msg = "*********************"; OutputText(msg);
          int pluginIndex = cboMethodTempo.SelectedIndex;
          //whiten = chkWhiten.Checked;

          err = VampTempo.PrepareToVamp(fileSongTemp, pluginIndex);
          if (err == 0)
          {
            string fileConfig = VampTempo.filesAvailableConfigs[pluginIndex];
            string vampParams = VampTempo.availablePluginCodes[pluginIndex];
            err = VampThatSong(fileSongTemp, vampParams, fileConfig, VampTempo.ResultsName);
          }
        }
      }

      //!///////////////////////
      //!  SEGMENTS  //
      //!/////////////////////
      if (chkSegments.Checked)
      {
        if ((!reuse) || (!Fyle.Exists(VampSegments.FileResults)))
        {
          StatusUpdate("Annotating Segments");
          if (logWindow != null) logWindow.Text = OutputTitle + " - Segments";
          msg = "\r\n\r\n********************"; OutputText(msg);
          msg = "***   SEGMENTS   ***"; OutputText(msg);
          msg = "********************"; OutputText(msg);
          int pluginIndex = cboMethodSegments.SelectedIndex;
          //whiten = chkWhiten.Checked;

          err = VampSegments.PrepareToVamp(fileSongTemp, pluginIndex);
          if (err == 0)
          {
            string fileConfig = VampSegments.filesAvailableConfigs[pluginIndex];
            string vampParams = VampSegments.availablePluginCodes[pluginIndex];
            err = VampThatSong(fileSongTemp, vampParams, fileConfig, VampSegments.ResultsName);
          }
        }
      }

      //!///////////////////
      //!   CHROMAGRAM   //
      //!/////////////////
      if (chkChromagram.Checked)
      {
        if ((!reuse) || (!Fyle.Exists(VampChromagram.FileResults)))
        {
          StatusUpdate("Annotating Chromagram");
          if (logWindow != null) logWindow.Text = OutputTitle + " - Chromagram";
          msg = "\r\n\r\n********************"; OutputText(msg);
          msg = "**   CHROMAGRAM   **"; OutputText(msg);
          msg = "********************"; OutputText(msg);
          int pluginIndex = cboMethodChromagram.SelectedIndex;

          err = VampChromagram.PrepareToVamp(fileSongTemp, pluginIndex);
          if (err == 0)
          {
            string fileConfig = VampChromagram.filesAvailableConfigs[pluginIndex];
            string vampParams = VampChromagram.availablePluginCodes[pluginIndex];
            err = VampThatSong(fileSongTemp, vampParams, fileConfig, VampChromagram.ResultsName);
          }
        }
      }

      //!///////////////////////
      //!  CHORDS CHORDINO  //
      //!/////////////////////
      if (chkChords.Checked)
      {
        if ((!reuse) || (!Fyle.Exists(VampChords.FileResults)))
        {
          StatusUpdate("Annotating Chords");
          if (logWindow != null) logWindow.Text = OutputTitle + " - Chords";
          msg = "\r\n\r\n******************"; OutputText(msg);
          msg = "***   CHORDS   ***"; OutputText(msg);
          msg = "******************"; OutputText(msg);
          int pluginIndex = cboMethodChords.SelectedIndex;
          //whiten = chkWhiten.Checked;

          err = VampChords.PrepareToVamp(fileSongTemp, pluginIndex);
          if (err == 0)
          {
            string fileConfig = VampChords.filesAvailableConfigs[pluginIndex];
            string vampParams = VampChords.availablePluginCodes[pluginIndex];
            err = VampThatSong(fileSongTemp, vampParams, fileConfig, VampChords.ResultsName);
          }
        }
      }



      if (logWindow != null) logWindow.Text = OutputTitle + " - Vamping Complete!";


      lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();


      dirtyTimes = true;

      return completed;
    }

    private int ProcessSelectedVamps()
    {
      int completed = 0;
      int err = 0;
      string msg = "";
      vamps.AlignmentType algn = vamps.AlignmentType.None;
      vamps.LabelType lbls = vamps.LabelType.None;
      bool reuse = chkReuse.Checked;
      Annotator.ReuseResults = chkReuse.Checked;
      //Annotator.Whiten = chkWhiteBarBeats.Checked;
      //Annotator.StepSize = Int32.Parse(cboStepSize.Text);
      //if (swTrackBeat.Checked) Annotator.BeatsPerBar = 3; else Annotator.BeatsPerBar = 4;
      //Annotator.UseRamps = swRamps.Checked;
      //Annotator.FirstBeat = Int32.Parse(txtStartBeat.Text);
      //Annotator.TempPath = pathWork;



      //!/////////////////
      //! BARS AND BEATS //
      //!/////////////////
      //if (chkBarsBeats.Checked)
      // ALWAYS annotate Bars and Beats since it is used for alignments
      if (true)
      {
        if (Fyle.Exists(VampBarBeats.FileResults))
        {
          StatusUpdate("Processing Bar and Beat Annotations");
          if (logWindow != null) logWindow.Text = ProcTitle + " Bars and Beats";
          msg = "\r\n\r\n**********************"; OutputText(msg);
          msg = "**    Analyzing     **"; OutputText(msg);
          msg = "**  BARS and BEATS  **"; OutputText(msg);
          msg = "**********************"; OutputText(msg);
          algn = vamps.GetAlignmentTypeFromName(cboAlignBarBeats.Text);
          lbls = vamps.GetLabelTypeFromName(cboLabelsBarBeats.Text);
          // Note: Does all 5: Bars, Whole Beats, Half, Third, and Quarter
          VampBarBeats.ResultsToxTimings(VampBarBeats.FileResults, algn, lbls);
          completed++;
        }
        else
        {
          // No Results File!
          Fyle.BUG("Bar and Beat Tracker Failed, no results file!");
        }
      }
      lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();


      //!////////////////
      //! NOTE ONSETS //
      //!/////////////
      if ((chkNoteOnsets.Checked) || (VampNoteOnsets.Timings.Markers.Count < 1))
      {
        if (Fyle.Exists(VampNoteOnsets.FileResults))
        {
          StatusUpdate("Processing Note Onset Annotations");
          if (logWindow != null) logWindow.Text = ProcTitle + " Note Onsets";
          msg = "\r\n\r\n*******************"; OutputText(msg);
          msg = "**   Analyzing   **"; OutputText(msg);
          msg = "**  NOTE ONSETS  **"; OutputText(msg);
          msg = "*******************"; OutputText(msg);
          algn = vamps.GetAlignmentTypeFromName(cboAlignOnsets.Text);
          lbls = vamps.GetLabelTypeFromName(cboLabelsOnsets.Text);
          VampNoteOnsets.ResultsToxTimings(VampNoteOnsets.FileResults, algn, lbls, VampNoteOnsets.DetectionMethods.ComplexDomain);
          completed++;
        }
        else
        {
          // Fyle.BUG("Note Onsets Failed, no results file!");
        }
      }

      //!///////////////////////
      //! POLYPHONIC TRANSCRIPTION //
      //!/////////////////////
      if ((chkPolyphonic.Checked) || (VampPolyphonic.Timings.Markers.Count < 1))
      {
        if (Fyle.Exists(VampPolyphonic.FileResults))
        {
          StatusUpdate("Processing Polyphonic Transcription Annotations");
          if (logWindow != null) logWindow.Text = ProcTitle + " Polyphonic Transcription";
          msg = "\r\n\r\n******************************"; OutputText(msg);
          msg = "**        Analyzing         **"; OutputText(msg);
          msg = "** POLYPHONIC TRANSCRIPTION **"; OutputText(msg);
          msg = "******************************"; OutputText(msg);
          algn = vamps.GetAlignmentTypeFromName(cboAlignPolyphonic.Text);
          lbls = vamps.GetLabelTypeFromName(cboLabelsPolyphonic.Text);
          VampPolyphonic.ResultsToxTimings(VampPolyphonic.FileResults, algn, lbls);
          completed++;
        }
        else
        {
          // No Results File!
          //Fyle.BUG("Polyphonic Transcription failed, no results file!");
        }
      }

      lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();


      //!///////////////////////
      //! PITCH & KEY //
      //!/////////////////////
      if ((chkPitchKey.Checked) || (VampPitchKey.Timings.Markers.Count < 1))
      {
        if (Fyle.Exists(VampPitchKey.FileResults))
        {
          StatusUpdate("Processing Pitch and Key Annotations");
          if (logWindow != null) logWindow.Text = ProcTitle + " Pitch and Key";
          msg = "\r\n\r\n*********************"; OutputText(msg);
          msg = "**    Analyzing    **"; OutputText(msg);
          msg = "**  PITCH and KEY  **"; OutputText(msg);
          msg = "*********************"; OutputText(msg);
          algn = vamps.GetAlignmentTypeFromName(cboAlignPitchKey.Text);
          lbls = vamps.GetLabelTypeFromName(cboLabelsPitchKey.Text);
          VampPitchKey.ResultsToxTimings(VampPitchKey.FileResults, algn, lbls);
          completed++;
        }
        else
        {
          // No Results File!
          //Fyle.BUG("Pitch and Key Tracker failed, no results file!");
        }
      }


      //!///////////////////////
      //!  T E M P O  //
      //!/////////////////////
      if ((chkTempo.Checked) || (VampTempo.Timings.Markers.Count < 1))
      {
        if (Fyle.Exists(VampTempo.FileResults))
        {
          StatusUpdate("Processing Tempo Change Annotations");
          if (logWindow != null) logWindow.Text = ProcTitle + " Tempo Changes";
          msg = "\r\n\r\n********************"; OutputText(msg);
          msg = "**   Analyzing    **"; OutputText(msg);
          msg = "**  TEMPO CHANGE  **"; OutputText(msg);
          msg = "********************"; OutputText(msg);
          algn = vamps.GetAlignmentTypeFromName(cboAlignTempo.Text);
          lbls = vamps.GetLabelTypeFromName(cboLabelsTempo.Text);
          VampTempo.ResultsToxTimings(VampTempo.FileResults, algn, lbls);
          completed++;
        }
        else
        {
          // No Results File!
          //Fyle.BUG("Pitch and Key Tracker failed, no results file!");
        }
      }

      //!///////////////////////
      //!  SEGMENTS  //
      //!/////////////////////
      if ((chkSegments.Checked) || (VampSegments.Timings.Markers.Count < 1))
      {
        if (Fyle.Exists(VampSegments.FileResults))
        {
          StatusUpdate("Processing Segment Annotations");
          if (logWindow != null) logWindow.Text = ProcTitle + " Segments";
          msg = "\r\n\r\n********************"; OutputText(msg);
          msg = "***   Analyzing  ***"; OutputText(msg);
          msg = "***   SEGMENTS   ***"; OutputText(msg);
          msg = "********************"; OutputText(msg);
          algn = vamps.GetAlignmentTypeFromName(cboAlignSegments.Text);
          lbls = vamps.GetLabelTypeFromName(cboLabelsSegments.Text);
          VampSegments.ResultsToxTimings(VampSegments.FileResults, algn, lbls);
          completed++;
        }
        else
        {
          // No Results File!
          //Fyle.BUG("Pitch and Key Tracker failed, no results file!");
        }
      }

      //!///////////////////
      //!   CHROMAGRAM   //
      //!/////////////////
      if ((chkChromagram.Checked) || (VampChromagram.Timings.Markers.Count < 1))
      {
        if (Fyle.Exists(VampChromagram.FileResults))
        {
          StatusUpdate("Processing Chromagram Annotations");
          if (logWindow != null) logWindow.Text = ProcTitle + " Chromagram";
          msg = "\r\n\r\n*******************"; OutputText(msg);
          msg = "**  Analyzing   **"; OutputText(msg);
          msg = "**  CHROMAGRAM  **"; OutputText(msg);
          msg = "******************"; OutputText(msg);
          algn = vamps.GetAlignmentTypeFromName(cboAlignChromagram.Text);
          lbls = vamps.GetLabelTypeFromName(cboLabelsChromagram.Text);
          // Chromagram does not create useful timings
          //VampChromagram.ResultsToxTimings(VampChromagram.FileResults, algn, lbls);
          completed++;
        }
        else
        {
          // No Results File!
          //Fyle.BUG("Chromagram Transcription failed, no results file!");
        }
      }

      //!///////////////////////
      //!  CHORDS CHORDINO //
      //!/////////////////////
      if ((chkChords.Checked) || (VampChords.Timings.Markers.Count < 1))
      {
        if (Fyle.Exists(VampChords.FileResults))
        {
          StatusUpdate("Processing Chord Annotations");
          if (logWindow != null) logWindow.Text = ProcTitle + " Chords";
          msg = "\r\n\r\n******************"; OutputText(msg);
          msg = "***   Analyzing  ***"; OutputText(msg);
          msg = "***   CHORDS   ***"; OutputText(msg);
          msg = "******************"; OutputText(msg);
          algn = vamps.GetAlignmentTypeFromName(cboAlignChords.Text);
          lbls = vamps.GetLabelTypeFromName(cboLabelsChords.Text);
          VampChords.ResultsToxTimings(VampChords.FileResults, algn, lbls);
          completed++;
        }
        else
        {
          // No Results File!
          //Fyle.BUG("Pitch and Key Tracker failed, no results file!");
        }
      }




      lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();


      dirtyTimes = true;

      return completed;
    }

    private void ExportSelectedVampsToxLights(string fileName)
    {
      // Get Temp Directory
      int writeCount = 0;
      bool allInOne = optMultiPer.Checked;
      string lineOut = "<timings>";
      string fileBaseName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName);
      string exten = ".xtiming";

      // Save Filename for next time (really only need the path, but...)
      fileTimingsLast = fileName;
      txtSaveNamexL.Text = ShortenPath(fileName, 100);
      userSettings.fileTimingsLast = fileTimingsLast;

      if (optMultiPer.Checked) userSettings.saveFormat = 2;
      else userSettings.saveFormat = 1;
      mruTimings.UseFile(fileName);
      // Get path and name for export files

      if (allInOne)
      {
        writer = BeingxTimingsFile(fileName);
        writer.WriteLine(lineOut);
        writeCount++;
      }

      //! BARS AND BEATS
      //if (chkBars.Checked)
      //if (doBarsBeats)
      if (chkBarsBeats.Checked)
      {
        //! BARS
        if (VampBarBeats.xBars != null)
        {
          if (VampBarBeats.xBars.Markers.Count > 0)
          {
            StatusUpdate("Exporting Bar Timings");
            if (!allInOne)
            {
              writer = BeingxTimingsFile(fileBaseName + " - " + VampBarBeats.xBars.Name + exten);
              writeCount++;
            }
            lineOut = xTimingsOut(VampBarBeats.xBars, vamps.LabelType.Numbers, allInOne);
            writer.WriteLine(lineOut);
          }
        }
        //! BEATS FULL WHOLE NOTES
        if (chkBeatsFull.Checked)
        {
          if (VampBarBeats.xBeatsFull != null)
          {
            if (VampBarBeats.xBeatsFull.Markers.Count > 0)
            {
              StatusUpdate("Exporting Full Beat Timings");
              if (!allInOne)
              {
                writer = BeingxTimingsFile(fileBaseName + " - " + VampBarBeats.xBeatsFull.Name + exten);
                writeCount++;
              }
              lineOut = xTimingsOut(VampBarBeats.xBeatsFull, vamps.LabelType.Numbers, allInOne);
              writer.WriteLine(lineOut);
            }
          }
        }
        //! HALF BEATS EIGHTH NOTES
        if (chkBeatsHalf.Checked)
        {
          if (VampBarBeats.xBeatsHalf != null)
          {
            if (VampBarBeats.xBeatsHalf.Markers.Count > 0)
            {
              StatusUpdate("Exporting Half Beat Timings");
              if (!allInOne)
              {
                writer = BeingxTimingsFile(fileBaseName + " - " + VampBarBeats.xBeatsHalf.Name + exten);
                writeCount++;
              }
              lineOut = xTimingsOut(VampBarBeats.xBeatsHalf, vamps.LabelType.Numbers, allInOne);
              writer.WriteLine(lineOut);
            }
          }
        }
        //! THIRD BEATS TWELTH NOTES
        if (chkBeatsThird.Checked)
        {
          if (VampBarBeats.xBeatsThird != null)
          {
            if (VampBarBeats.xBeatsThird.Markers.Count > 0)
            {
              StatusUpdate("Exporting Third Beat Timings");
              if (!allInOne)
              {
                writer = BeingxTimingsFile(fileBaseName + " - " + VampBarBeats.xBeatsThird.Name + exten);
                writeCount++;
              }
              lineOut = xTimingsOut(VampBarBeats.xBeatsThird, vamps.LabelType.Numbers, allInOne);
              writer.WriteLine(lineOut);
            }
          }
        }
        //! QUARTER BEATS SIXTEENTH NOTES
        if (chkBeatsQuarter.Checked)
        {
          if (VampBarBeats.xBeatsQuarter != null)
          {
            if (VampBarBeats.xBeatsQuarter.Markers.Count > 0)
            {
              StatusUpdate("Exporting Quarter Beat Timings");
              if (!allInOne)
              {
                writer = BeingxTimingsFile(fileBaseName + " - " + VampBarBeats.xBeatsQuarter.Name + exten);
                writeCount++;
              }
              lineOut = xTimingsOut(VampBarBeats.xBeatsQuarter, vamps.LabelType.Numbers, allInOne);
              writer.WriteLine(lineOut);
            }
          }
        }
      }

      //! NOTE ONSETS
      if (chkNoteOnsets.Checked)
      {
        if (VampNoteOnsets.Timings != null)
        {
          if (VampNoteOnsets.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Note Onset Timings");
            if (!allInOne)
            {
              writer = BeingxTimingsFile(fileBaseName + " - " + VampNoteOnsets.Timings.Name + exten);
              writeCount++;
            }
            vamps.LabelType lt = vamps.GetLabelTypeFromName(cboLabelsOnsets.Text);
            lineOut = xTimingsOut(VampNoteOnsets.Timings, lt, allInOne);
            writer.WriteLine(lineOut);
            writeCount++;
          }
        }
      }

      //! POLYPHONIC TRANSCRIPTION
      if (chkPolyphonic.Checked)
      {
        if (VampPolyphonic.Timings != null)
        {
          if (VampPolyphonic.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Polyphonic Transcription Timings");
            if (!allInOne)
            {
              writer = BeingxTimingsFile(fileBaseName + " - " + VampPolyphonic.Timings.Name + exten);
              writeCount++;
            }
            vamps.LabelType lt = vamps.GetLabelTypeFromName(cboLabelsPolyphonic.Text);
            lineOut = xTimingsOut(VampPolyphonic.Timings, lt, allInOne);
            writer.WriteLine(lineOut);
            writeCount++;
          }
        }
      }

      //! PITCH AND KEY
      if (chkPitchKey.Checked)
      {
        if (VampPitchKey.Timings != null)
        {
          if (VampPitchKey.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Pitch and Key Timings");
            if (!allInOne)
            {
              writer = BeingxTimingsFile(fileBaseName + " - " + VampPitchKey.Timings.Name + exten);
              writeCount++;
            }

            vamps.LabelType lt = vamps.GetLabelTypeFromName(cboLabelsPitchKey.Text);
            lineOut = xTimingsOut(VampPitchKey.Timings, lt, allInOne);
            writer.WriteLine(lineOut);
            writeCount++;
          }
        }
      }

      //! TEMPO CHANGES
      if (chkTempo.Checked)
      {
        if (VampTempo.Timings != null)
        {
          if (VampTempo.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Tempo Change Timings");
            if (!allInOne)
            {
              writer = BeingxTimingsFile(fileBaseName + " - " + VampTempo.Timings.Name + exten);
              writeCount++;
            }
            vamps.LabelType lt = vamps.GetLabelTypeFromName(cboLabelsTempo.Text);
            lineOut = xTimingsOut(VampTempo.Timings, lt, allInOne);
            writer.WriteLine(lineOut);
            writeCount++;
          }
        }
      }

      //! SEGMENTs
      if (chkSegments.Checked)
      {
        if (VampSegments.Timings != null)
        {
          if (VampSegments.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Segment Timings");
            if (!allInOne)
            {
              writer = BeingxTimingsFile(fileBaseName + " - " + VampSegments.Timings.Name + exten);
              writeCount++;
            }
            vamps.LabelType lt = vamps.GetLabelTypeFromName(cboLabelsSegments.Text);
            lineOut = xTimingsOut(VampSegments.Timings, lt, allInOne);
            writer.WriteLine(lineOut);
            writeCount++;
          }
        }
      }

      //! CHROMAGRAM
      if (chkChromagram.Checked)
      {
        if (VampChromagram.Timings != null)
        {
          // Chromagram does not create useful timings
          /*
					if (VampChromagram.Timings.Markers.Count > 0)
					{
						StatusUpdate("Exporting Chromagram Timings");
						if (!allInOne)
						{
							writer = BeingxTimingsFile(fileBaseName + " - " + VampChromagram.Timings.Name + exten);
							writeCount++;
						}
						vamps.LabelType lt = vamps.GetLabelTypeFromName(cboLabelsChromagram.Text);
						lineOut = xTimingsOut(VampChromagram.Timings, lt, allInOne);
						writer.WriteLine(lineOut);
						writeCount++;
					}
					*/
        }
      }

      //! CHORDS
      if (chkChords.Checked)
      {
        if (VampChords.Timings != null)
        {
          if (VampChords.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Chord Timings");
            if (!allInOne)
            {
              writer = BeingxTimingsFile(fileBaseName + " - " + VampChords.Timings.Name + exten);
              writeCount++;
            }
            vamps.LabelType lt = vamps.GetLabelTypeFromName(cboLabelsChords.Text);
            lineOut = xTimingsOut(VampChords.Timings, lt, allInOne);
            writer.WriteLine(lineOut);
            writeCount++;
          }
        }
      }


      if (allInOne)
      {
        lineOut = "</timings>";
        writer.WriteLine(lineOut);
      }
      writer.Close();


      StatusUpdate("Export of " + writeCount.ToString() + " Timings Complete!");
      //pnlStatus.Text = writeCount.ToString() + " files writen.";


    }

    private int ExportSelectedVampsToLOR()
    {
      int completed = 0;
      bool ramps = swRamps.Checked;
      Annotator.UseRamps = ramps;

      //Annotator.VampTrack = Annotator.Sequence.FindTrack("Vamp-O-Rama", true);

      //! BARS AND BEATS
      if (chkBarsBeats.Checked)
      {
        if (lightORamaInstalled && chkLOR.Checked)
        {
          StatusUpdate("Exporting Bar and Beat Annotations");
          VampBarBeats.xTimingsToLORtimings(); // Does all 5: Bars, Full Beats, Halves, Thirds, Quarters
          VampBarBeats.xTimingsToLORChannels(); // Does all 5
          if (Fyle.Exists(VampBarBeats.FileResults)) completed += 5;
        }
      }

      //! NOTE ONSETS
      if (chkNoteOnsets.Checked)
      {
        if (lightORamaInstalled && chkLOR.Checked)
        {
          if (VampNoteOnsets.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Note Onset Annotations");
            VampNoteOnsets.xTimingsToLORtimings();
            VampNoteOnsets.xTimingsToLORChannel();
            if (Fyle.Exists(VampNoteOnsets.FileResults)) completed++;
          }
        }
      }

      //! POLYPHONIC TRANSCRIPTION
      if (chkPolyphonic.Checked)
      {
        if (lightORamaInstalled && chkLOR.Checked)
        {
          if (VampPolyphonic.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Polyphonic Transcription Annotations");
            VampPolyphonic.xTimingsToLORtimings();
            VampPolyphonic.xTimingsToLORChannels();
            if (Fyle.Exists(VampPolyphonic.FileResults)) completed++;
          }
        }
      }

      //! PITCH AND KEY
      if (chkPitchKey.Checked)
      {
        if (lightORamaInstalled && chkLOR.Checked)
        {
          if (VampPitchKey.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Pitch and Key Annotations");
            VampPitchKey.xTimingsToLORtimings();
            // If set to ramps, temporarily change to On-Off
            bool wasRamps = Annotator.UseRamps;
            Annotator.UseRamps = false;
            VampPitchKey.xTimingsToLORChannels();
            Annotator.UseRamps = wasRamps;
            if (Fyle.Exists(VampPitchKey.FileResults)) completed++;
          }
        }
      }

      //! TEMPO CHANGES
      if (chkTempo.Checked)
      {
        if (lightORamaInstalled && chkLOR.Checked)
        {
          if (VampTempo.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Tempo Change Annotations");
            VampTempo.xTimingsToLORtimings();
            // If set to ramps, temporarily change to On-Off
            VampTempo.xTimingsToLORChannels();
            if (Fyle.Exists(VampTempo.FileResults)) completed++;
          }
        }
      }

      //! SEGMENTS
      if (chkSegments.Checked)
      {
        if (lightORamaInstalled && chkLOR.Checked)
        {
          if (VampSegments.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Segment Annotations");
            VampSegments.xTimingsToLORtimings();
            // If set to ramps, temporarily change to On-Off
            VampSegments.xTimingsToLORChannels();
            if (Fyle.Exists(VampSegments.FileResults)) completed++;
          }
        }
      }

      //! CHROMAGRAM
      if (chkChromagram.Checked)
      {
        if (lightORamaInstalled && chkLOR.Checked)
        {
          //if (VampChromagram.Timings.Markers.Count > 0)
          // {
          StatusUpdate("Exporting Chromagram Annotations");
          // Chromagram does not create useful timings
          //VampChromagram.xTimingsToLORtimings();
          //VampChromagram.xTimingsToLORChannels();
          VampChromagram.ResultsToLORChannels();
          if (Fyle.Exists(VampChromagram.FileResults)) completed++;
          //}
        }
      }

      //! CHORDS
      if (chkChords.Checked)
      {
        if (lightORamaInstalled && chkLOR.Checked)
        {
          if (VampChords.Timings.Markers.Count > 0)
          {
            StatusUpdate("Exporting Chord Annotations");
            VampChords.xTimingsToLORtimings();
            VampChords.ResultsToLORChannels();
            if (Fyle.Exists(VampChords.FileResults)) completed++;
          }
        }
      }


      StatusUpdate("Export Complete!");


      return completed;
    }

    public static string LabelName(vamps.LabelType labelType)
    {
      int i = (int)labelType;
      return vamps.LabelNames[(int)labelType]; // availableLabels[i];
    }

    public static vamps.LabelType LabelType(string labelName)
    {
      return vamps.GetLabelTypeFromName(labelName);
    }

    #endregion

    #region Vamp Agnostic
    private void FixStepSize()
    {
      bool found = false;  // Flag if value already in step size list
                           // Set the current step size to the new needed size
      Annotator.StepSize = Annotator.NeedStepSize;
      Annotator.StepErrFlag = true;
      lblStepSize.ForeColor = System.Drawing.Color.Crimson;
      cboStepSize.ForeColor = System.Drawing.Color.Crimson;
      // Loop thru item in step size combo looking for new value
      for (int i = 0; i < cboStepSize.Items.Count; i++)
      {
        int n = 0;
        int.TryParse(cboStepSize.Items[i].ToString(), out n);
        if (n == Annotator.NeedStepSize)
        {
          // Got it!  Change index of combo box
          cboStepSize.SelectedIndex = i;
          found = true;
          i = cboStepSize.Items.Count; // Force exit of loop
        }
      }
      // Was it not already in the combo?
      if (!found)
      {
        // Add it and selecte it
        string ss = Annotator.NeedStepSize.ToString();
        cboStepSize.Items.Add(ss);
        cboStepSize.SelectedIndex = cboStepSize.Items.Count - 1;
      }
      // Reset this
      Annotator.NeedStepSize = 0;
    }

    private int GetDuration(string songLength)
    {
      int ret = 0;
      string rest = "";
      string hrs = "00";
      string min = "00";
      string sec = "00";
      string subsec = "0000000";
      int cp = songLength.IndexOf(':');
      if (cp > 0)
      {
        hrs = songLength.Substring(0, cp);
        rest = songLength.Substring(cp + 1);
        cp = rest.IndexOf(':');
        if (cp > 0)
        {
          min = rest.Substring(0, cp);
          rest = rest.Substring(cp + 1);
        }
        else
        {
          min = hrs;
          hrs = "00";
        }
        cp = rest.IndexOf('.');
        if (cp > 0)
        {
          sec = rest.Substring(0, cp);
          rest = rest.Substring(cp + 1);
          subsec = rest;
        }
        else
        {
          sec = rest;
        }
      }

      int hr = 0;
      int.TryParse(hrs, out hr);
      int mn = 0;
      int.TryParse(min, out mn);
      int sc = 0;
      int.TryParse(sec, out sc);
      int ss = 0;
      int.TryParse(subsec, out ss);
      int ms = 0;
      if (ss > 0)
      {
        double div = Math.Pow(10, subsec.Length - 3);
        double dss = Math.Round(ss / div);
        ms = (int)dss;
      }

      ret = ms;
      ret += sc * 1000;
      ret += mn * 60000;
      ret += hr * 360000;

      return ret;
    }

    private int GetDuration(System.TimeSpan songLength)
    {
      double rms = Math.Round(songLength.TotalMilliseconds);
      int ims = (int)rms;
      return ims;
    }

    private string PrepAudioFile(string originalAudioFile)
    {
      // Input: A fully qualified path and filename to a audio file
      // Example: D:\xLights\MyShow\Audio\Wizards in Winter by TSO.mp3
      // Output: The Name of the COPY of the audio file that is to be annotated, in annotator's required format
      // Example: c:/users/johndoe/appdata/utilorama/Vamperizer/wizardsinwinter.mp3
      int err = 0;
      bool needCopy = false;


      originalPath = Path.GetDirectoryName(originalAudioFile);
      originalFileName = Path.GetFileNameWithoutExtension(originalAudioFile);
      originalExt = Path.GetExtension(originalAudioFile);

      newFile = tempPath + "song" + originalExt;

      // Do we need to copy or re-copy the original song file to the temp folder?
      // Is reuse turned off?  Does it exist?  Is it the same size and date?
      if (!chkReuse.Checked) needCopy = true;
      if (!Fyle.Exists(newFile))
      {
        needCopy = true;
      }
      else
      {
        FileInfo fio = new FileInfo(originalAudioFile);
        FileInfo fin = new FileInfo(newFile);
        if (fio.Length != fin.Length) needCopy = true;
        if (fio.LastWriteTime != fin.LastWriteTime) needCopy = true; ;
      }

      if (needCopy)
      {
        // Copy original audio file to temp folder if necessary
        err = Fyle.SafeCopy(originalAudioFile, newFile, true);
        if (err > 0)
        {
          string msg = "SafeCopy of the Audio File [" + originalAudioFile;
          msg += "]\r\nTo the Temp Folder [" + tempPath;
          msg += "]\r\nFailed with Error " + err.ToString();
          Fyle.BUG(msg);
          newFile = "";
        }
      }
      if (err == 0)
      {
        // if no errors so far
        audioData = ReadAudioFile(newFile);
        string songInfo = audioData.Title + " by " + audioData.Artist;
        this.Text = myTitle + " - " + songInfo;
        if (seq != null)
        {
          if (seq.Centiseconds < audioData.Centiseconds) seq.Centiseconds = audioData.Centiseconds;
        }
        //lblSeqTime.Text = FormatSongTime(seq.Centiseconds * 10);
        fileAudioLast = originalAudioFile;
        fileAudioWork = newFile;
      }

      return newFile;
    }

    public StreamWriter BeingxTimingsFile(string fileName)
    {
      if (writer != null)
      {
        writer.Close();
      }
      writer = new StreamWriter(fileName);
      writer.WriteLine(xTimings.XMLinfo);
      return writer;
    }

    public string xTimingsOut(xTimings timings, vamps.LabelType labelType, bool indent = false)
    {

      string label = "";
      string level0 = "";
      string level1 = "  ";
      string level2 = "    ";
      if (indent)
      {
        level0 = "  ";
        level1 = "    ";
        level2 = "      ";
      }
      xMarker mark = null;

      StringBuilder ret = new StringBuilder();
      //  <timing
      ret.Append(level0);
      ret.Append(xTimings.RECORD_start);
      ret.Append(xTimings.TABLE_timing);
      ret.Append(xTimings.SPC);
      //  name="the Name"
      ret.Append(xTimings.FIELD_name);
      ret.Append(xTimings.VALUE_start);
      ret.Append(timings.Name);
      ret.Append(xTimings.VALUE_end);
      ret.Append(xTimings.SPC);
      //  SourceVersion="2019.21">
      ret.Append(xTimings.FIELD_source);
      ret.Append(xTimings.VALUE_start);
      ret.Append(timings.sourceVersion);
      ret.Append(xTimings.VALUE_end);
      ret.Append(xTimings.RECORD_end);
      ret.Append(xTimings.CRLF);
      //    <EffectLayer>
      ret.Append(level1);
      ret.Append(xTimings.RECORD_start);
      ret.Append(xTimings.TABLE_layers);
      ret.Append(xTimings.RECORD_end);
      ret.Append(xTimings.CRLF);

      for (int i = 0; i < timings.Markers.Count; i++)
      {
        mark = timings.Markers[i];
        ret.Append(level2);
        ret.Append(xTimings.RECORD_start);
        ret.Append(xTimings.TABLE_Effect);
        ret.Append(xTimings.SPC);
        //  label="foo" 
        ret.Append(xTimings.FIELD_label);
        ret.Append(xTimings.VALUE_start);

        //string newLabel = GetLabel(effect.Number, labelType);
        ret.Append(mark.Label);

        ret.Append(xTimings.VALUE_end);
        ret.Append(xTimings.SPC);
        //  starttime="50" 
        ret.Append(xTimings.FIELD_start);
        ret.Append(xTimings.VALUE_start);
        ret.Append(mark.starttime.ToString());
        ret.Append(xTimings.VALUE_end);
        ret.Append(xTimings.SPC);
        //  endtime="350" />
        ret.Append(xTimings.FIELD_end);
        ret.Append(xTimings.VALUE_start);
        ret.Append(mark.endtime.ToString());
        ret.Append(xTimings.VALUE_end);
        ret.Append(xTimings.SPC);

        ret.Append(xTimings.RECORD_end);
        ret.Append(xTimings.CRLF);
      }

      //     </EffectLayer>
      ret.Append(level1);
      ret.Append(xTimings.RECORDS_done);
      ret.Append(xTimings.TABLE_layers);
      ret.Append(xTimings.RECORD_end);
      ret.Append(xTimings.CRLF);
      //  </timing>
      ret.Append(level0);
      ret.Append(xTimings.RECORDS_done);
      ret.Append(xTimings.TABLE_timing);
      ret.Append(xTimings.RECORD_end);

      return ret.ToString();
    }

    public static string GetLabel(int number, vamps.LabelType labelType)
    {
      string label = "";
      switch (labelType)
      {
        case vamps.LabelType.None:
          // Leave Blank
          break;
        case vamps.LabelType.KeyNamesASCII:
          if (number >= 0 && number <= 25)
          {
            label = MusicalNotation.keyNamesASCII[number];
          }
          break;
        case vamps.LabelType.KeyNamesUnicode:
          if (number >= 0 && number <= 25)
          {
            label = MusicalNotation.keyNamesUnicode[number];
          }
          break;
        case vamps.LabelType.KeyNumbers:
          label = number.ToString();
          break;
        case vamps.LabelType.NoteNamesASCII:
          if (number >= 0 && number <= 127)
          {
            label = MusicalNotation.noteNamesASCII[number];
          }
          break;
        case vamps.LabelType.NoteNamesUnicode:
          if (number >= 0 && number <= 127)
          {
            label = MusicalNotation.noteNamesUnicode[number];
          }
          break;
        case vamps.LabelType.MIDINoteNumbers:
          label = number.ToString();
          break;
        case vamps.LabelType.Frequency:
          if (number >= 0 && number <= 127)
          {
            label = MusicalNotation.noteFreqs[number];
          }
          break;
        case vamps.LabelType.Numbers:
          label = number.ToString();
          break;
        case vamps.LabelType.Letters:
          label = ((char)(number + 64)).ToString();
          break;
        case vamps.LabelType.BPM:
          label = number.ToString();
          break;
        case vamps.LabelType.TempoName:
          if (number >= 10 && number <= 250)
          {
            label = MusicalNotation.FindTempoName(number);
          }
          break;
      }
      return label;
    }

    public int VampThatSong(string fileSong, string vampParams, string fileConfig, string resultsOut)
    {
      int err = 0;
      string msg = "";
      string resultsFileOld = "";
      //string pathWork = Path.GetDirectoryName(fileSong) + "\\";
      string ex = Path.GetExtension(fileSong);
      //! string annotatorArguments = "-t " + vampParams;
      string annotatorArguments = "-t " + fileConfig;
      string WRITEformat = " -f -w csv --csv-force ";

      annotatorArguments += " \"" + fileSong + "\""; // + ex;
      annotatorArguments += WRITEformat;
      //annotatorArguments += " 2>output.txt";
      //string outputFile = tempPath + "output.log";
      string fileOutput = vampParams.Replace(':', '_') + ".n3";
      //string fileOutput = fileConfig;

      try
      {
        string emsg = annotatorProgram + " " + annotatorArguments;
        Console.WriteLine(emsg);
        //DialogResult dr = MessageBox.Show(this, emsg, "About to launch", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
        DialogResult dr = DialogResult.Yes;

        if (dr == DialogResult.Yes)
        {
          if (debugMode) Clipboard.SetText(emsg);
        }
        if (dr != DialogResult.Cancel)
        {
          resultsFileOld = tempPath;
          //resultsFile += "song_";
          resultsFileOld += Path.GetFileNameWithoutExtension(fileSong);
          //resultsFile += "_" + Path.GetFileNameWithoutExtension(fileConfig);
          resultsFileOld += "_" + Path.GetFileNameWithoutExtension(fileOutput);
          resultsFileOld += ".csv";

          //! FOR TESTING DEBUGGING, if set to re-use old files, OR if no file from a previous run exists
          if ((!Annotator.ReuseResults) || (!Fyle.Exists(resultsOut)))
          //if (true) 
          {
            lock (syncGate)
            {
              if (cmdProc != null)
              {
                err = 99;
                return err;
              }
            }

            string runthis = annotatorProgram + " " + annotatorArguments;
            runthis = "/c " + runthis; // + " 2>output.txt";

            string vampCommandLast = runthis;
            if (debugMode) Clipboard.SetText(runthis);

            cmdProc = new Process();
            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.FileName = "cmd.exe";
            procInfo.RedirectStandardOutput = true;
            procInfo.RedirectStandardError = true;
            procInfo.CreateNoWindow = true;
            //procInfo.WindowStyle = ProcessWindowStyle.Hidden;
            procInfo.UseShellExecute = false;
            procInfo.Arguments = runthis;
            procInfo.WorkingDirectory = tempPath;

            cmdProc.StartInfo = procInfo;
            cmdProc.EnableRaisingEvents = true;
            cmdProc.ErrorDataReceived += ProcessVampError;
            cmdProc.OutputDataReceived += ProcessVampError;
            cmdProc.Exited += VampProcessEnded;
            cmdProc.Start();
            cmdProc.BeginErrorReadLine();
            cmdProc.BeginOutputReadLine();

            //cmdProc.WaitForExit();
            while (cmdProc != null)
            {
              //while (!cmdProc.HasExited)
              //{
              if (cmdProc.HasExited)
              {
                cmdProc = null;
              }
              Application.DoEvents(); // This keeps your form responsive by processing events
                                      //}
            }

          }

          if (Fyle.Exists(resultsFileOld))
          {
            err = Fyle.SafeRename(resultsFileOld, resultsOut, true);
            // return resultsFile;
            // errCount = 99999;
          }
          else
          {
            //if (Annotator.NeedStepSize > 0)
            if (Annotator.StepErrFlag)
            {
              // No results due to step size error
              Fyle.BUG("Step Size Error!  Attempting Auto-Fix");
            }
            else
            {
              msg = "Vamp-That-Song Failed, results file " + resultsOut + " not found!\r\n";
              msg += "Error number " + err.ToString();
              Fyle.BUG(msg);
            }
          }
        }
      }
      catch (Exception ex2)
      {
        if (debugMode)
        {
          msg = ex2.Message;
          //Fyle.MakeNoise(Fyle.Noises.Crash);
          //System.Diagnostics.Debugger.Break();
          Fyle.BUG(msg + "\r\nWhile Vamping that Song!");
        }
        err = 55;
      }

      return err;
    }

    private void ProcessVampError(object sender, DataReceivedEventArgs drea)
    {
      int err = 0;
      lock (syncGate)
      {
        if (sender != cmdProc)
        {
          err = 99999;
        }
        else
        {
          string lineOut = drea.Data;
          if (lineOut != null)
          {
            if (lineOut.Length > 4)
            {
              if (lineOut.Substring(0, 5).ToLower() == "error")
              {
                string lo = lineOut.ToLower();
                //TODO Process this and look for step size error, return the DESIRED step size
                int pp = lo.IndexOf("unsupported step size for this sample");
                if (pp > 0)
                {
                  Annotator.StepErrFlag = true;
                  lblStepSize.ForeColor = System.Drawing.Color.Crimson;
                  pp = lo.IndexOf("(wanted ");
                  if (pp > 0)
                  {
                    string wantSize = lo.Substring(pp + 8, 3);
                    int newSize = 777;
                    int.TryParse(wantSize, out newSize);
                    Annotator.NeedStepSize = newSize;
                  }
                }
              }
            }
            outLog.AppendLine(lineOut);
          }
          if (outputChanged)
          { }
          else
          {
            outputChanged = true;
            BeginInvoke(new Action(OnOutputChanged));
          }
        }
        //picSleepy.Refresh(); // Causes cross-thread error
        Application.DoEvents();
      }
      return; // err;
    }

    private void OnOutputChanged()
    {
      lock (syncGate)
      {
        logWindow.LogText = outLog.ToString();
        outputChanged = false;
      }
    }

    public void OutputText(string text)
    {
      outLog.AppendLine(text);
      outputChanged = true;
      BeginInvoke(new Action(OnOutputChanged));
    }

    private void VampProcessEnded(object sender, EventArgs e)
    {
      lock (syncGate)
      {
        if (sender != cmdProc) return;
        cmdProc.Dispose();
        cmdProc = null;
      }
    }

    private int PrepareToAnnotate()
    {
      int errs = 0;

      //beatsPerBar = 4; // reset to default
      //if (swTrackBeat.Checked) beatsPerBar = 3;
      //firstBeat = 1; // reset to default
      //Int32.TryParse(txtStartBeat.Text, out firstBeat);
      //if (firstBeat < 1) firstBeat = 1;
      //if (firstBeat > beatsPerBar) firstBeat = beatsPerBar;

      //fileSongTemp = pathWork + "song.mp3";
      //errs = LOR4Admin.SafeCopy(fileAudioLast, fileSongTemp);
      //audioData.Filename = fileSongTemp;
      //audioData = ReadAudioFile(fileSongTemp);
      fileSongTemp = PrepAudioFile(fileAudioLast);


      //reuse = chkReuse.Checked;
      //Int32.TryParse(cboStepSize.Text, out stepSize);
      //if ((stepSize < 200) || (stepSize > 800)) stepSize = 512;
      //anno = new Annotator(annotatorProgram);
      //anno.songTimeMS

      return errs;
    }

    #endregion

  }
}
