using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using LORUtils; using FileHelper;
using xUtilities;

using System.Diagnostics;
using System.Linq.Expressions;

namespace UtilORama4
{
	public partial class frmVamp : Form
	{
		public Sequence4 seq;
		private bool doGroups = true;
		private bool useRampsPoly = false;
		private bool useRampsBeats = false;
		private Track vampTrack = null;
		private int centiseconds = 0;
		private string fileSeqName = "";
		private MRU mruSequences = new MRU(heartOfTheSun, "sequence", 9);

		private void SaveAsNewSequence()
		{
			string filter = "Musical Sequence *.lms|*.lms";
			string idr = utils.DefaultSequencesPath;

			string ifile = Path.GetFileNameWithoutExtension(fileCurrent);
			if (ifile.Length < 2)
			{
				//ifile = seq.info.music.Title + " by " + seq.info.music.Artist;
				ifile = audioData.Title + " by " + audioData.Artist;
			}
			ifile = Fyle.FixInvalidFilenameCharacters(ifile);
			ifile += ".lms";

			dlgSaveFile.Filter = filter;
			dlgSaveFile.InitialDirectory = idr;
			dlgSaveFile.FileName = ifile;
			dlgSaveFile.FilterIndex = 1;
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.Title = "Save Sequence As...";
			dlgSaveFile.ValidateNames = true;
			DialogResult result = dlgSaveFile.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				ImBusy(true);
				fileSeqName = dlgSaveFile.FileName;
				txtSeqName.Text = Path.GetFileNameWithoutExtension(fileSeqName);
				seq = CreateNewSequence(fileSeqName);
				seq.info.author = utils.DefaultAuthor;
				seq.info.music.Album = audioData.Album;
				seq.info.music.Artist = audioData.Artist;
				seq.info.music.Title = audioData.Title;
				seq.info.music.File = fileAudioLast;
				//ImportVampsToSequence();
				ExportSelectedVamps();
				SaveSequence(fileSeqName);
				//ImBusy(false);
				if (chkAutolaunch.Checked)
				{
					System.Diagnostics.Process.Start(fileSeqName);
				}

			}
			//btnBrowseSequence.Focus();


		}

		private void SaveInExistingSequence()
		{
			string filt = "Musical Sequences *.lms|*lms";
			string idir = utils.DefaultSequencesPath;
			string ifl = txtSeqName.Text.Trim();
			string theFile = "";
			if (Fyle.ValidFilename(ifl, true, false))
			{
				idir = Path.GetDirectoryName(ifl);
				if (Fyle.ValidFilename(ifl, true, true))
				{
					if (Path.GetExtension(ifl.ToLower()) == "lms")
					{
						theFile = Path.GetFileName(ifl);
					}
				}
			}
			else
			{
				// Keep default sequence path
			}


			dlgOpenFile.Filter = filt;
			dlgOpenFile.FilterIndex = 2;    //! Temporary?  Set back to 1 and/or change filter string?
			dlgOpenFile.InitialDirectory = idir;
			//dlgOpenFile.FileName = Properties.Settings.Default.fileSeqLast;

			DialogResult dr = dlgOpenFile.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				fileCurrent = dlgOpenFile.FileName;
				txtSeqName.Text = Path.GetFileName(fileCurrent);
				string ex = Path.GetExtension(fileCurrent).ToLower();
				// If they picked an existing musical sequence
				if (ex == ".lms")
				{
					seq.ReadSequenceFile(fileCurrent);
					//fileAudioOriginal = seq.info.music.File;
					//txtFileAudio.Text = Path.GetFileNameWithoutExtension(fileAudioOriginal);
					grpAudio.Text = " Original Audio File ";
					btnBrowseAudio.Text = "Analyze";
					//fileSeqCur = fileCurrent;
					//fileChanCfg = "";
					// Add to Sequences MRU

					centiseconds = SequenceFunctions.ms2cs(audioData.Duration);

					//! ImportVampsToSequence();
					SaveSequence(fileCurrent);


				}
				//grpGrids.Enabled = true;
				//grpTracks.Enabled = true;
				//grpAudio.Enabled = true;
				//btnBrowseAudio.Focus();

			}
		}

		private void SaveSequence(string newFilename)
		{
			ImBusy(true);
			// normal default when not testing
			seq.WriteSequenceFile_DisplayOrder(newFilename, false, false);
			System.Media.SystemSounds.Beep.Play();
			//dirtySeq = false;
			//fileSeqSave = newFilename;
			//Add to MRU
			ImBusy(false);

		}

		private Sequence4 CreateNewSequence(string theFilename)
		{
			seq = new Sequence4();
			seq.info.author = utils.DefaultAuthor;
			SaveSongInfo();
			// Save what we have so far...
			//seq.WriteSequenceFile_DisplayOrder(theFilename);

			return seq;
		}

		private void SaveSongInfo()
		{
			centiseconds = SequenceFunctions.ms2cs(audioData.Duration);
			seq.Centiseconds = centiseconds;
			seq.SequenceType = SequenceType.Musical;
			//seq.Tracks[0].Centiseconds = 0;
			seq.info.music.Album = audioData.Album;
			string artst = audioData.Artist;
			if (artst.Length < 1)
			{
				artst = audioData.AlbumArtist;
			}
			seq.info.music.Artist = artst;
			seq.info.music.File = audioData.Filename;
			seq.info.music.Title = audioData.Title;
			theSong = audioData.Title;
			songTitle = audioData.Title;
			if (audioData.Artist.Length > 1)
			{
				theSong += BY + audioData.Artist;
				songArtist = audioData.Artist;
			}
			else
			{
				if (audioData.AlbumArtist.Length > 1)
				{
					theSong += BY + audioData.AlbumArtist;
					songArtist = audioData.AlbumArtist;
				}
			}

		}


		/*

		private int ImportVampsToSequence()
		{
			int errs = 0;

			SaveSongInfo();
			
			TimingGrid ftg = GetGrid("20 FPS", true);
			ftg.TimingGridType = TimingGridType.FixedGrid;
			ftg.Centiseconds = centiseconds;
			ftg.spacing = 5;
			
			vampTrack = GetTrack("Vamp-O-Rama", true);
			vampTrack.Centiseconds = centiseconds;
			vampTrack.timingGrid = ftg;

			string lorAuth = utils.DefaultAuthor;
			seq.info.modifiedBy = lorAuth + " + Vamp-O-Rama";

			if (doBarsBeats && (errLevel == 0))
			{
				errLevel = ImportBarsBeats();
			}
			if (doNoteOnsets && (errLevel == 0))
			{
				errLevel = ImportNoteOnsets();
			}
			if (doTranscribe && (errLevel == 0))
			{
				errLevel = ImportTranscription();
			}
			if (doPitchKey && (errLevel == 0))
			{
				errLevel = ImportPitchKey();
			}
			if (doSegments && (errLevel == 0))
			{
				errLevel = ImportSegments();
			}



			if (seq.Channels.Count < 1)
			{
				Channel ch = seq.CreateChannel("null");
				seq.Tracks[0].Members.Add(ch);
			}



			return errs;
		}

		private int ImportBarsBeats()
		{
			int errs = 0;
			ChannelGroup beatGroup = GetGroup("Bars and Beats", vampTrack);
			if (xBars != null)
			{
				if (xBars.effects.Count > 0)
				{
					TimingGrid barGrid = GetGrid("Bars",true);
					ImportTimingGrid(barGrid, xBars);
					if (swRamps.Checked)
					{
						Channel barCh = GetChannel("Bars", beatGroup.Members);
						ImportBeatChannel(barCh, xBars, 1);
					}
				}
			}
			if (chkBeatsFull.Checked)
			{
				if (xBeatsFull != null)
				{
					if (xBeatsFull.effects.Count > 0)
					{
						TimingGrid barGrid = GetGrid("Beats-Full",true);
						Channel beatCh = GetChannel("Beats-Full", beatGroup.Members);
						ImportTimingGrid(barGrid, xBeatsFull);
						ImportBeatChannel(beatCh, xBeatsFull,beatsPerBar);
					}
				}
			}
			if (chkBeatsHalf.Checked)
			{
				if (xBeatsHalf != null)
				{
					if (xBeatsHalf.effects.Count > 0)
					{
						TimingGrid barGrid = GetGrid("Beats-Half",true);
						Channel beatCh = GetChannel("Beats-Half", beatGroup.Members);
						ImportTimingGrid(barGrid, xBeatsHalf);
						ImportBeatChannel(beatCh, xBeatsHalf,beatsPerBar * 2);
					}
				}
			}
			if (chkBeatsThird.Checked)
			{
				if (xBeatsThird != null)
				{
					if (xBeatsThird.effects.Count > 0)
					{
						TimingGrid barGrid = GetGrid("Beats-Third",true);
						Channel beatCh = GetChannel("Beats-Third", beatGroup.Members);
						ImportTimingGrid(barGrid, xBeatsThird);
						ImportBeatChannel(beatCh, xBeatsThird,beatsPerBar * 3);
					}
				}
			}
			if (chkBeatsQuarter.Checked)
			{
				if (xBeatsQuarter != null)
				{
					if (xBeatsQuarter.effects.Count > 0)
					{
						TimingGrid barGrid = GetGrid("Beats-Quarter",true);
						Channel beatCh = GetChannel("Beats-Quarter", beatGroup.Members);
						ImportTimingGrid(barGrid, xBeatsQuarter);
						ImportBeatChannel(beatCh, xBeatsQuarter,beatsPerBar * 4);
					}
				}
			}
			if (chkNoteOnsets.Checked)
			{
				if (xOnsets != null)
				{
					if (xOnsets.effects.Count > 0)
					{
						TimingGrid noteGrid = GetGrid("Note Onsets",true);
						//Channel noteCh = GetChannel("Note Onsets", beatGroup.Members);
						ImportTimingGrid(noteGrid, xBeatsQuarter);
						//ImportBeatChannel(noteCh, xBeatsQuarter);
					}
				}
			}
			if (chkTranscribe.Checked)
			{
				if (xTranscription != null)
				{
					if (xTranscription.effects.Count > 0)
					{
						//ChannelGroup transGroup = GetGroup("Polyphonic Transcription");
						//ImportTranscription(transGroup);
					}
				}
			}
			if (chkPitchKey.Checked)
			{
				if (xKey != null)
				{
					if (xKey.effects.Count > 0)
					{
						//ChannelGroup transGroup = GetGroup("Polyphonic Transcription");
						//ImportTranscription(transGroup);
					}
				}
			}
			if (chkSegments.Checked)
			{
				if (xSegments != null)
				{
					if (xSegments.effects.Count > 0)
					{
						//ChannelGroup transGroup = GetGroup("Polyphonic Transcription");
						//ImportTranscription(transGroup);
					}
				}
			}







			return errs;
		}

		private int ImportNoteOnsets()
		{
			int errs = 0;
			if (xOnsets != null)
			{
				if (xOnsets.effects.Count > 0)
				{
					TimingGrid onsGrid = GetGrid("Note Onsets");
					ImportTimingGrid(onsGrid, xBars);
					ChannelGroup onsGrp = GetGroup("Note Onsets", vampTrack);
					ImportNoteOnsetChannels(onsGrp, xBeatsFull);
				}
			}
			return errs;
		}

		private int ImportPitchKey()
		{
			int errs = 0;
			if (xOnsets != null)
			{
				if (xKey.effects.Count > 0)
				{
					ChannelGroup keyGrp = GetGroup("Pitch and Key", vampTrack);
					ImportKeyChannels(keyGrp, xKey);
				}
			}
			return errs;
		}


private int		ImportNoteOnsetChannels(ChannelGroup onsGrp, xTimings xBeatsFull)
		{
			int errs = 0;


			return errs;
		}

		private int ImportKeyChannels(ChannelGroup keyGroup, xTimings xKeys)
		{
			int errs = 0;

			// If pitch/key channels exist, clear them
			if (keyGroup.Members.Count > 0)
			{
				keyGroup.Members = new Membership(keyGroup);
			}
			Channel[] keyChannels = null;
			Array.Resize(ref keyChannels, keyNames.Length);
			for (int kc=0; kc<keyNames.Length; kc++)
			{
				keyChannels[kc].ChangeName(keyNames[kc]);
				keyChannels[kc].color = NoteColor(kc);
				keyChannels[kc].output.deviceType = DeviceType.None;
				keyChannels[kc].output.network = 0;
				keyChannels[kc].output.channel = 0;
			}
			//int lb = barDivs - 1;
			for (int q = 0; q < xKeys.effects.Count; q++)
			{
				xEffect xef = xKeys.effects[q];
				Effect lef = new Effect();
				lef.EffectType = EffectType.Intensity;
				lef.Intensity = 100;
				lef.startCentisecond = ms2cs(xef.starttime);
				// This should work, why doesn't it?
				lef.endCentisecond = ms2cs(xef.endtime);
				//if (q < (xKeys.effects.Count - 1))
				//{
				// Alternative
				//	lef.endCentisecond = ms2cs(xKeys.effects[q].starttime);
				//}
				int keyIdx = xef.Midi;
				keyChannels[keyIdx].effects.Add(lef);
			} // end for loop
			
			// Loop thru all new key channels, only add ones with effects to the group
			for (int kc = 0; kc < keyNames.Length; kc++)
			{
				if (keyChannels[kc].effects.Count > 0)
				{
					keyGroup.Members.Add(keyChannels[kc]);
				}
			}

			return errs;
		}

		private int ImportPolyChannels(ChannelGroup polyGroup, xTimings xEffects)
		{
			int errs = 0;



			return errs;
		}

		private int ImportPoly(string polyFile)
		{
			string PolyFile;
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;
			string[] parts;
			int ontime = 0;
			int note = 0;
			Channel ch;
			Effect ef;

			//Track trk = new Track("Polyphonic Transcription");
			Track trk = GetTrack(MASTERTRACK);
			//trk.Centiseconds = seq.Centiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			trk.timingGrid = tg;
			//trk.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPPOLY, trk);
			CreatePolyChannels(grp, "Poly ", doGroups);
			if (tg == null)
			{
				trk.timingGrid = seq.TimingGrids[0];
				//trk.timingGridSaveID = 0;
			}
			else
			{
				trk.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						trk.timingGrid = seq.TimingGrids[tgs];
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}

			StreamReader reader = new StreamReader(polyFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					pcount++;
					centisecs = ParseCentiseconds(parts[0]);
					ontime = ParseCentiseconds(parts[1]);
					note = Int16.Parse(parts[2]);
					//ch = seq.Channels[firstCobjIdx + note];
					//ch = GetChannel("theName");
					ch = noteChannels[note];
					ef = new Effect();
					ef.EffectType = EffectType.Intensity;
					ef.startCentisecond = centisecs;
					ef.endCentisecond = centisecs + ontime;
					if (useRampsPoly)
					{
						ef.startIntensity = 100;
						ef.endIntensity = 0;
					}
					else
					{
						ef.Intensity = 100;
					}
					//ch.effects.Add(ef);
					ch.AddEffect(ef);
				}

			} // end while loop more lines remaining

			reader.Close();

			//seq.AddTrack(trk);



			return pcount;
		}

		//private int SpectrogramToChannels(string spectroFile)
		private int ImportSpectro(string spectroFile)
		{
			string SpectroFile;
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;
			string[] parts;
			int ontime = 0;
			int note = 0;
			Channel ch;
			Effect ef;

			//Track trk = new Track("Spectrogram");
			Track trk = GetTrack(MASTERTRACK);
			//trk.identity.Centiseconds = seq.totalCentiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			trk.timingGrid = tg;
			//trk.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPSPECTRO, trk);
			CreatePolyChannels(grp, "Spectro ", doGroups);
			if (tg == null)
			{
				trk.timingGrid = seq.TimingGrids[0];
				//trk.timingGridSaveID = 0;
			}
			else
			{
				trk.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						trk.timingGrid = seq.TimingGrids[tgs];
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}

			// Pass 1, Get max values
			double[] dVals = new double[1024];
			StreamReader reader = new StreamReader(spectroFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 1025)
				{
					pcount++;
					//centisecs = ParseCentiseconds(parts[0]);
					//Debug.Write(centisecs);
					//Debug.Write(":");
					for (int n = 0; n < 1024; n++)
					{
						double d = Double.Parse(parts[n]);
						if (d > dVals[n]) dVals[n] = d;
					}
				}
			} // end while loop more lines remaining
			reader.Close();

			// Pass 2, Convert those maxvals to a scale factor
			for (int n = 0; n < 1024; n++)
			{
				dVals[n] = 140 / dVals[n];
			}

			// Pass 3, convert to percents
			int lastcs = utils.UNDEFINED;
			double lastdt = 0;
			int lastix = 0;

			reader = new StreamReader(spectroFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 1025)
				{
					pcount++;
					centisecs = ParseCentiseconds(parts[0]);
					//Debug.Write(centisecs);
					//Debug.Write(":");
					for (int n = 0; n < 128; n++)
					{
						double dt = 0;
						for (int m = 0; m < 8; m++)
						{
							int i = n * 8 + m + 1;
							double d = Double.Parse(parts[i]);
							d *= dVals[i];
							dt += d;
						}
						dt /= 8;
						int ix = (int)dt;
						if (ix < 20)
						{
							ix = 0;
						}
						else
						{
							if (ix > 120)
							{
								ix = 100;
							}
							else
							{
								ix -= 20;
							}
						}
						if (centisecs == lastcs)
						{
							ix += lastix;
							ix /= 2;
						}



						lastix = ix;
						lastdt = dt;
						lastcs = centisecs;
					}
				}
			} // end while loop more lines remaining
			reader.Close();









			//seq.AddTrack(trk);



			return pcount;
		}

		private int ConstQToChannels(string constQFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;
			string[] parts;
			int ontime = 0;
			//int note = 0;
			Channel ch;
			Effect ef;

			//Track trk = new Track("Constant Q Spectrogram");
			Track trk = GetTrack(MASTERTRACK);
			//trk.identity.Centiseconds = seq.totalCentiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			trk.timingGrid = tg;
			//trk.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPCONSTQ, trk);
			CreatePolyChannels(grp, "ConstQ ", doGroups);
			if (tg == null)
			{
				trk.timingGrid = seq.TimingGrids[0];
				//trk.timingGridSaveID = 0;
			}
			else
			{
				trk.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						trk.timingGrid = seq.TimingGrids[tgs];
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}

			// Pass 1, Get max values
			double[] dVals = new double[128];
			StreamReader reader = new StreamReader(constQFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 129)
				{
					pcount++;
					//centisecs = ParseCentiseconds(parts[0]);
					//Debug.Write(centisecs);
					//Debug.Write(":");
					for (int note = 0; note < 128; note++)
					{
						double d = Double.Parse(parts[note + 1]);
						if (d > dVals[note]) dVals[note] = d;
					}
				}
			} // end while loop more lines remaining
			reader.Close();

			// Pass 2, Convert those maxvals to a scale factor
			for (int n = 0; n < 128; n++)
			{
				dVals[n] = 140 / dVals[n];
			}

			// Pass 3, convert to percents
			int[] lastcs = new int[128];
			double lastdVal = 0;
			int[] lastiVal = new int[128];

			reader = new StreamReader(constQFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 129)
				{
					pcount++;
					centisecs = ParseCentiseconds(parts[0]);
					//Debug.Write(centisecs);
					//Debug.Write(":");
					for (int note = 0; note < 128; note++)
					{
						double dt = 0;
						double d = Double.Parse(parts[note + 1]);
						d *= dVals[note];
						dt += d;
						int iVal = (int)dt;
						if (iVal < 21)
						{
							iVal = 0;
						}
						else
						{
							if (iVal > 120)
							{
								iVal = 100;
							}
							else
							{
								iVal -= 20;
							}
						}

						if (iVal != lastiVal[note])
						{
							//ch = seq.Channels[firstCobjIdx + note];
							ch = noteChannels[note];
							//Identity id = seq.Members.bySavedIndex[noteChannels[note]];
							//if (id.PartType == MemberType.Channel)
							//{
							//ch = (Channel)id.owner;
							ef = new Effect();
							ef.EffectType = EffectType.Intensity;
							ef.startCentisecond = lastcs[note];
							ef.endCentisecond = centisecs;
							ef.startIntensity = lastiVal[note];
							ef.endIntensity = iVal;
							ch.effects.Add(ef);
							lastcs[note] = centisecs;
							lastiVal[note] = iVal;
							//}
							//else
							//{
							//	string emsg = "Crash! Burn! Explode!";
							//}
						}


					}
				}
			} // end while loop more lines remaining
			reader.Close();









			//seq.AddTrack(trk);



			return pcount;
		}

		private int ParseCentiseconds(string secondsValue)
		{
			int ppos = secondsValue.IndexOf('.');
			// Get number of seconds before the period
			int sec = Int16.Parse(secondsValue.Substring(0, ppos));
			// Get the fraction of a second after the period, only keep most significant 4 digits
			int dotsec = Int16.Parse(secondsValue.Substring(ppos + 1, 4));
			// turn it from an int into an actual fraction
			decimal ds = (dotsec / 100);
			// Round up or down from 4 digits to 2
			dotsec = (int)Math.Round(ds);  // man is this stupid call picky as hell about syntax
																		 // Combine seconds and fraction of a second into Centiseconds
			int centisecs = sec * 100 + dotsec;

			return centisecs;

		}


		private int ImportNoteOnsets(string noteOnsetFile)
		{
			//string noteOnsetFile;
			int onsetCount = 0;
			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;

			//TimingGrid grid = new TimingGrid("Note Onsets");
			TimingGrid grid = GetGrid(GRIDONSETS);
			grid.TimingGridType = TimingGridType.Freeform;
			//grid.type = timingGridType.freeform;
			grid.AddTiming(0); // Needs a timing of zero at the beginning

			StreamReader reader = new StreamReader(noteOnsetFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				ppos = lineIn.IndexOf('.');
				if (ppos > utils.UNDEFINED)
				{
					centisecs = ParseCentiseconds(lineIn);
					// Add centisecond value to the timing grid
					grid.AddTiming(centisecs);
					onsetCount++;
				} // end line contains a period
			} // end while loop more lines remaining

			reader.Close();

			//seq.TimingGrids.Add(grid);
			//seq.AddTimingGrid(grid);
			//Track trk = seq.FindTrack(applicationName);
			//trk.timingGridObjIndex = seq.TimingGrids.Count - 1;
			//trk.timingGridObjIndex = grid.identity.SavedIndex;
			//trk.totalCentiseconds = seq.totalCentiseconds;

			return onsetCount;
		} // end Note Onsets to Timing Grid



		private int RGBtoLOR(int RGBclr)
		{
			int b = RGBclr & 0xFF;
			int g = RGBclr & 0xFF00;
			g /= 0x100;
			int r = RGBclr & 0xFF0000;
			r /= 0x10000;

			int n = b * 0x10000;
			n += g * 0x100;
			n += r;

			return n;
		}























		public int SaveTimings(string timingsName)
		{
			int ret = 0;


			return ret;
		}

		public int SaveTranscriptionChannels()
		{
			int ret = 0;


			return ret;

		}

		public int SaveSpectrogramChannels()
		{
			int ret = 0;


			return ret;

		}

		public int SaveChromagramChannels()
		{
			int ret = 0;


			return ret;

		}

		public int SaveBeatChannels()
		{
			int ret = 0;


			return ret;

		}

		public int SaveKeyChannels()
		{
			int ret = 0;


			return ret;

		}

		public int SaveTempoChannels()
		{
			int ret = 0;


			return ret;

		}

		public int SaveSongPartsChannels()
		{
			int ret = 0;


			return ret;

		}
		*/

	}


}
