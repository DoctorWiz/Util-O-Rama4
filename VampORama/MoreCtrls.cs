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
using LORUtils;
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
			cboAlignBarsBeats.Items.Clear();
			cboAlignOnsets.Items.Clear();
			cboAlignTranscribe.Items.Clear();
			cboAlignSpectrum.Items.Clear();
			cboAlignTempo.Items.Clear();
			cboAlignPitch.Items.Clear();
			cboAlignSegments.Items.Clear();
			cboAlignVocals.Items.Clear();

			cboAlignBarsBeats.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignOnsets.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignTranscribe.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignTempo.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignPitch.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignSegments.Items.Add(vamps.ALIGNNAMEnone);
			cboAlignVocals.Items.Add(vamps.ALIGNNAMEnone);

			// 60 FPS, 1.667cs LOR only
			if (chkLOR.Checked)
			{
				cboAlignBarsBeats.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignTranscribe.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignPitch.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps60l);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps60l);
			}
			// 40 FPS, 25ms xLights only
			if (chkxLights.Checked)
			{
				cboAlignBarsBeats.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignTranscribe.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignPitch.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps40x);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps40x);
			}
			// 30 FPS, 3.33cs LOR only
			if (chkLOR.Checked)
			{
				cboAlignBarsBeats.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignTranscribe.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignPitch.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps30l);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps30l);
			}
			// 20 FPS, 5cs LOR only
			if ((chkLOR.Checked) && (!chkxLights.Checked))
			{
				cboAlignBarsBeats.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignTranscribe.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignPitch.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps20l);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps20l);
			}
			// 20 FPS, 50ms or 5cs xLights (with or without LOR)
			if (chkxLights.Checked)
			{
				cboAlignBarsBeats.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignTranscribe.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignPitch.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps20x);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps20x);
			}
			// 1 FPS, 10cs LOR only
			if (chkLOR.Checked)
			{
				cboAlignBarsBeats.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignTranscribe.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignPitch.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEfps10l);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEfps10l);
			}
			if (chkBarsBeats.Checked)
			{
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEbars);
				cboAlignPitch.Items.Add(vamps.ALIGNNAMEbars);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEbars);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEbars);

				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignTranscribe.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignPitch.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEbeatsFull);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEbeatsFull);

				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignTranscribe.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignPitch.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEbeatsHalf);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEbeatsHalf);

				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignTranscribe.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignPitch.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEbeatsThird);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEbeatsThird);

				cboAlignOnsets.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignTranscribe.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignSpectrum.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignTempo.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignPitch.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignSegments.Items.Add(vamps.ALIGNNAMEbeatsQuarter);
				cboAlignVocals.Items.Add(vamps.ALIGNNAMEbeatsQuarter);

			}



		}



	}
}
