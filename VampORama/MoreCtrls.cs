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
using xUtilities;
using LORUtils4; using FileHelper;
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
	public partial class frmVamp : Form
	{
	
		private void SetAlignCombos()
		{
			cboAlignBarBeats.Items.Clear();
			cboAlignOnsets.Items.Clear();
			cboAlignPolyphonic.Items.Clear();
			cboAlignSpectrum.Items.Clear();
			cboAlignTempo.Items.Clear();
			cboAlignPitchKey.Items.Clear();
			cboAlignSegments.Items.Clear();
			cboAlignVocals.Items.Clear();

			cboAlignBarBeats.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignOnsets.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignPolyphonic.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignTempo.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignSegments.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignVocals.Items.Add(vamps.ALIGNNAMEnone);

			// 60 FPS, 1.667cs LOR only
			if (chkLOR.Checked)
			{
				cboAlignBarBeats.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignPolyphonic.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps60l);
			}
			// 40 FPS, 25ms xLights only
			if (chkxLights.Checked)
			{
				cboAlignBarBeats.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignPolyphonic.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps40x);
			}
			// 30 FPS, 3.33cs LOR only
			if (chkLOR.Checked)
			{
				cboAlignBarBeats.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignPolyphonic.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps30l);
			}
			// 20 FPS, 5cs LOR only
			if ((chkLOR.Checked) && (!chkxLights.Checked))
			{
				cboAlignBarBeats.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignPolyphonic.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps20l);
			}
			// 20 FPS, 50ms or 5cs xLights (with or without LOR)
			if (chkxLights.Checked)
			{
				cboAlignBarBeats.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignPolyphonic.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps20x);
			}
			// 1 FPS, 10cs LOR only
			if (chkLOR.Checked)
			{
				cboAlignBarBeats.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignPolyphonic.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps10l);
			}
			if (chkBarsBeats.Checked)
			{
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEbars);
				cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEbars);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEbars);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEbars);

				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignPolyphonic.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEbeatsFull);

				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignPolyphonic.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEbeatsHalf);

				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignPolyphonic.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEbeatsThird);

				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignPolyphonic.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignPitchKey.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEbeatsQuarter);

			}



		}



	}
}
