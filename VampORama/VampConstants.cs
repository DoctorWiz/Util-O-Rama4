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
using Microsoft.Win32;
using xUtilities;
using Musik;
using FileHelper;

namespace UtilORama4
{
	public static class vamps
	{

		static readonly int ALGORbarBeats = 1;
		static readonly int ALGORqmBeats = 2;
		static readonly int ALGORbeatRoot = 3;
		static readonly int ALGORportoBeat = 4;
		static readonly int ALGORaubioBeat = 5;

		static readonly int ALGORnoteOnset = 6;
		static readonly int ALGORonsetDS = 7;
		static readonly int ALGORsilvetOnset = 8;
		static readonly int ALGORaubioOnset = 9;
		static readonly int ALGORaubioPoly = 10;

		static readonly int ALGORqmPoly = 11;
		static readonly int ALGORconstq = 12;
		static readonly int ALGORchroma = 13;
		static readonly int ALGORsegments = 14;
		static readonly int ALGORspectro = 15;

		static readonly int ALGORkey = 16;
		static readonly int ALGORmelody = 17;
		static readonly int ALGORvocals = 18;
		static readonly int ALGORtempo = 19;

		/* Moved to the corresponding transformation class
				static readonly string VAMPbarBeats = "vamp:qm-vamp-plugins:qm-barbeattracker:beats";
				static readonly string VAMPqmBeats = "vamp:qm-vamp-plugins:qm-tempotracker:beats";
				static readonly string VAMPbeatRoot = "vamp:beatroot-vamp:beatroot:beats";
				static readonly string VAMPportoBeat = "vamp:mvamp-ibt:marsyas_ibt:beat_times";
				static readonly string VAMPaubioBeat = "vamp:vamp-aubio:aubiotempo:beats";
				static readonly string VAMPnoteOnset = "vamp:qm-vamp-plugins:qm-onsetdetector:onsets";
				static readonly string VAMPonsetDS = "vamp:vamp-onsetsds:onsetsds:onsets";
				static readonly string VAMPsilvetOnset = "vamp:silvet:silvet:onsets";
				static readonly string VAMPaubioOnset = "vamp:vamp-aubio:aubioonset:onsets";
				static readonly string VAMPaubioPoly = "vamp:vamp-aubio:aubionotes:notes";
				static readonly string VAMPqmPoly = "vamp:qm-vamp-plugins:qm-transcription:transcription";
		*/
		static readonly string VAMPconstq = "vamp:qm-vamp-plugins:qm-constantq:constantq";
		static readonly string VAMPchroma = "vamp:qm-vamp-plugins:qm-chromagram:chromagram";
		static readonly string VAMPsegmenter = "vamp:qm-vamp-plugins:qm-segmenter:segmentation";
		static readonly string VAMPsegmentino = "vamp:segmentino:segmentino:segmentation";
		static readonly string VAMPspectro = "vamp:qm-vamp-plugins:qm-adaptivespectrogram";
		static readonly string VAMPkey = "vamp:qm-vamp-plugins:qm-keydetector:key";
		static readonly string VAMPmelody = "vamp:mtg-melodia:melodia:melody";
		static readonly string VAMPvocals = "vamp:bbc-vamp-plugins:bbc-speechmusic-segmenter:segmentation";
		static readonly string VAMPtempo = "vamp:blahblahblah";
		static readonly string VAMPtonal = "vamp:qm-vamp-plugins:qm-tonalchange:tcfunction";
		static readonly string VAMPpitch = "vamp:cepstral-pitchtracker:cepstral-pitchtracker:notes";
		static readonly string VAMPfundFreq = "vamp:vamp-libxtract:f0:f0";
		static readonly string VAMPpYIN = "vamp:blahblahblah";
		static readonly string VAMPaubioPitch = "vamp:vamp-aubio:aubiopitch:frequency";

		static readonly string FILEbarBeats = "qm-barbeattracker.n3";
		static readonly string FILEqmBeats = "qm-tempotracker_output_beats.n3";
		static readonly string FILEbeatRoot = "vamp_beatroot_beats.n3";
		static readonly string FILEportoBeat = "mvamp-ibt_marsyas_ibt_beat_times.n3";
		static readonly string FILEaubioBeat = "vamp-aubio_aubiotempo_beats.n3";
		static readonly string FILEnoteOnset = "qm-onsetdetector.n3";
		static readonly string FILEonsetDS = "onsetsds_onsetsds_onsets.n3";
		static readonly string FILEsilvetOnset = "silvet_silvet_onsets.n3";
		static readonly string FILEaubioOnset = "aubio_aubioonset_onsets.n3";
		static readonly string FILEaubioPoly = "aubio_aubionotes_notes.n3";
		static readonly string FILEqmPoly = "qm-transcription.n3";
		static readonly string FILEconstq = "qm-constantq.n3";
		static readonly string FILEchroma = "qm-chromagram.n3";
		static readonly string FILEsegments = "qm-segmenter.n3";
		static readonly string FILEspectro = "qm-adaptivespectrogram.n3";
		static readonly string FILEkey = "qm-keydetector.n3";
		static readonly string FILEmelody = "mtg-melodia.n3";
		static readonly string FILEvocals = "bbc-speech.n3";
		static readonly string FILEtempo = "qm-tempophoo.n3";


		private const int LISTbars = 0;
		private const int LISTbeatsFull = 1;
		private const int LISTbeatsHalf = 2;
		private const int LISTbeatsThird = 3;
		private const int LISTbeatsQuarter = 4;
		private const int LISTnotes = 5;
		private const int LISTpitch = 6;
		private const int LISTsegments = 7;
		private const int LISTchroma = 8;
		private const int LISTtempo = 9;
		private const int LISTpoly = 10;
		private const int LISTconstQ = 11;
		private const int LISTflux = 12;
		private const int LISTchords = 13;
		private const int LISTvocals = 14;
		private const int LISTcount = 15;

		private const int ALIGNnone = 0;
		private const int ALIGNbar = 2;
		private const int ALIGNbeatFull = 3;
		private const int ALIGNbeatHalf = 4;
		private const int ALIGNbeatThird = 5;
		private const int ALIGNbeatQuarter = 6;
		private const int ALIGNonset = 7;
		private const int ALIGNfps25 = 25;
		private const int ALIGNfps50 = 50;

		public const string ALIGNNAMEnone = "None";
		public const string ALIGNNAMEbars = "Bars: Whole Notes";
		public const string ALIGNNAMEbeatsFull = "Full Beats: Quarter Notes";
		public const string ALIGNNAMEbeatsHalf = "Half Beats: Eighth Notes";
		public const string ALIGNNAMEbeatsThird = "Third Beats: 12X 16ths";
		public const string ALIGNNAMEbeatsQuarter = "Quarter Beats: Sixteenth Notes";
		public const string ALIGNNAMEnoteOnsets = "Note Onsets";

		public const string ALIGNNAMEfps40x = "40 FPS, 25 ms";
		public const string ALIGNNAMEfps20x = "20 FPS, 50 ms";

		public const string ALIGNNAMEfps60l = "60 FPS, 1.67 cs";
		public const string ALIGNNAMEfps30l = "30 FPS, 3.33 cs";
		public const string ALIGNNAMEfps20l = "20 FPS, 5 cs";
		public const string ALIGNNAMEfps10l = "10 FPS, 10 cs";
		//private const string ALIGNNAMEfps50l = "50 FPS, 2 cs";


		private const string ALIGNNAMEcustom = "Custom Frames-Per-Second";
		public enum AlignmentType
		{
			None = 1, Bars = 2, BeatsFull = 3, BeatsHalf = 4, BeatsThird = 5, BeatsQuarter = 6,
			NoteOnsets = 7, FPS10 = 10, FPS20 = 20, FPS30 = 30, FPS40 = 40, FPS60 = 60, FPScustom = 100
		};

		public enum LabelType
		{
			None, KeyNamesASCII, KeyNamesUnicode, KeyNumbers, NoteNamesASCII, NoteNamesUnicode,
			MIDINoteNumbers, Frequency, Numbers, Letters, BPM, TempoName
		};

		/*
		public const int LABELnone							= 0;
		public const int LABELkeyNameASCII			= 1;
		public const int LABELkeyNameUnicode		= 2;
		public const int LABELkeyNumbers				= 3;
		public const int LABELnoteNamesASCII		= 4;
		public const int LABELnoteNamesUnicode	= 5;
		public const int LABELmidiNoteNumbers		= 6;
		public const int LABELfrequency					= 7;
		public const int LABELnumbers						= 8;
		public const int LABELletters						= 9;
		*/

		public const string LABELNAMEnone = "None";
		public const string LABELNAMEkeyNamesASCII = "Key Name in ASCII";
		public const string LABELNAMEkeyNamesUnicode = "Key Name in Unicode";
		public const string LABELNAMEkeyNumbers = "Key Number";
		public const string LABELNAMEnoteNamesASCII = "Note Name in ASCII";
		public const string LABELNAMEnoteNamesUnicode = "Note Name in Unicode";
		public const string LABELNAMEmidiNoteNumbers = "MIDI Note Number";
		public const string LABELNAMEfrequency = "Frequency in Hz";
		public const string LABELNAMEnumbers = "Number";
		public const string LABELNAMEletters = "Letter";
		public const string LABELNAMEbpm = "Beats-Per-Minute (BPM)";
		public const string LABELNAMEtempoName = "Tempo Name";

		public readonly static string[] LabelNames = {LABELNAMEnone, LABELNAMEkeyNamesASCII, LABELNAMEkeyNamesUnicode, LABELNAMEkeyNumbers,
																	LABELNAMEnoteNamesASCII, LABELNAMEnoteNamesUnicode, LABELNAMEmidiNoteNumbers,
																	LABELNAMEfrequency, LABELNAMEnumbers, LABELNAMEletters, LABELNAMEbpm, LABELNAMEtempoName };


		//public readonly static string[] LabelTypeNames = { "None", "Key Names ASCII", "Key Names Unicode", "Key Numbers", "Note Names ASCII",
		//												"Note Names Unicode",	"MIDI Note Numbers", "Frequency", "Numbers", "Letters" };




		private static readonly string GRIDBEATS = "Beats";
		private static readonly string GRIDONSETS = "Note Onsets";
		//private const string MASTERTRACK = "Song Information [Tune-O-Rama]";
		private const string MASTERTRACK = "Beats + Song Information [Vamp-O-Rama]";
		private const string MASTERMATCH = "Beats + Song ";
		private const string GROUPBEATS = "Bars and Beats";
		private const string GROUPPOLY = "Polyphonic Transcription";
		private const string GROUPCONSTQ = "Constant Q Spectrogram";
		private const string GROUPSEGMENTS = "Segments";
		private const string GROUPSPECTRO = "Spectrogram";
		private const string GROUPCHROMA = "Chromagram";
		private const string GROUPKEY = "Key";
		private const string GROUPMELODY = "Melody";
		private const string GROUPSPEECH = "Speech";
		private const string CHANbeats = "Beats";

		private const string ALGOqm = " (Queen Mary)";
		private const string ALGOaubio = " (Aubio)";
		private const string ALGObbc = " (BBC)";


		public static string AlignmentName(AlignmentType alignmentType)
		{
			// init return value with default/failsafe
			string theName = ALIGNNAMEnone;

			switch (alignmentType)
			{
				case AlignmentType.FPS10:
					theName = ALIGNNAMEfps10l;
					break;
				case AlignmentType.FPS20:
					theName = ALIGNNAMEfps20x;
					break;
				case AlignmentType.FPS30:
					theName = ALIGNNAMEfps30l;
					break;
				case AlignmentType.FPS40:
					theName = ALIGNNAMEfps40x;
					break;
				case AlignmentType.FPS60:
					theName = ALIGNNAMEfps60l;
					break;
				case AlignmentType.Bars:
					theName = ALIGNNAMEbars;
					break;
				case AlignmentType.BeatsFull:
					theName = ALIGNNAMEbeatsFull;
					break;
				case AlignmentType.BeatsQuarter:
					theName = ALIGNNAMEbeatsQuarter;
					break;
				case AlignmentType.NoteOnsets:
					theName = ALIGNNAMEnoteOnsets;
					break;
				case AlignmentType.BeatsHalf:
					theName = ALIGNNAMEbeatsHalf;
					break;
				case AlignmentType.BeatsThird:
					theName = ALIGNNAMEbeatsThird;
					break;
			}
			return theName;
		}

		public static AlignmentType GetAlignmentTypeFromName(string typeName)
		{
			// Init return value with default/failsafe
			AlignmentType at = AlignmentType.None;

			if (typeName == ALIGNNAMEnone)
			{
				at = AlignmentType.None;
			}
			else
			{
				if (typeName == ALIGNNAMEfps40x)
				{
					at = AlignmentType.FPS40;
				}
				else
				{
					if (typeName == ALIGNNAMEfps20x)
					{
						at = AlignmentType.FPS20;
					}
					else
					{
						if (typeName == ALIGNNAMEbars)
						{
							at = AlignmentType.Bars;
						}
						else
						{
							if (typeName == ALIGNNAMEbars)
							{
								at = AlignmentType.Bars;
							}
							else
							{
								if (typeName == ALIGNNAMEbeatsFull)
								{
									at = AlignmentType.BeatsFull;
								}
								else
								{
									if (typeName == ALIGNNAMEbeatsQuarter)
									{
										at = AlignmentType.BeatsQuarter;
									}
									else
									{
										if (typeName == ALIGNNAMEnoteOnsets)
										{
											at = AlignmentType.NoteOnsets;
										}
										else
										{
											if (typeName == ALIGNNAMEbeatsHalf)
											{
												at = AlignmentType.BeatsHalf;
											}
											else
											{
												if (typeName == ALIGNNAMEbeatsThird)
												{
													at = AlignmentType.BeatsThird;
												}
												else
												{
													if (typeName == ALIGNNAMEfps10l)
													{
														at = AlignmentType.FPS10;
													}
													else
													{
														if (typeName == ALIGNNAMEfps20l)
														{
															at = AlignmentType.FPS20;
														}
														else
														{
															if (typeName == ALIGNNAMEfps30l)
															{
																at = AlignmentType.FPS30;
															}
															else
															{
																if (typeName == ALIGNNAMEfps60l)
																{
																	at = AlignmentType.FPS60;
																}
																else
																{
																	if (typeName == ALIGNNAMEcustom)
																	{
																		at = AlignmentType.FPScustom;
																	}
																	else
																	{
																		string msg = "Alignment Type '" + typeName + "' not recognized!";
																		Fyle.BUG(msg);
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return at;
		}

		public static string LabelName(LabelType labelType)
		{
			// init return value with default/failsafe
			string theName = LABELNAMEnone;

			switch (labelType)
			{
				case LabelType.None:
					theName = LABELNAMEnone;
					break;
				case LabelType.KeyNamesASCII:
					theName = LABELNAMEkeyNamesASCII;
					break;
				case LabelType.KeyNamesUnicode:
					theName = LABELNAMEkeyNamesUnicode;
					break;
				case LabelType.KeyNumbers:
					theName = LABELNAMEkeyNumbers;
					break;
				case LabelType.NoteNamesASCII:
					theName = LABELNAMEnoteNamesASCII;
					break;
				case LabelType.NoteNamesUnicode:
					theName = LABELNAMEnoteNamesUnicode;
					break;
				case LabelType.MIDINoteNumbers:
					theName = LABELNAMEmidiNoteNumbers;
					break;
				case LabelType.Frequency:
					theName = LABELNAMEfrequency;
					break;
				case LabelType.Numbers:
					theName = LABELNAMEnumbers;
					break;
				case LabelType.Letters:
					theName = LABELNAMEletters;
					break;
				case LabelType.BPM:
					theName = LABELNAMEbpm;
					break;
				case LabelType.TempoName:
					theName = LABELNAMEtempoName;
					break;
			}
			return theName;
		}

		public static LabelType GetLabelTypeFromName(string labelName)
		{
			// public enum LabelType { None, KeyNamesASCII, KeyNamesUnicode, KeyNumbers, NoteNamesASCII, NoteNamesUnicode, MIDINoteNumbers, Frequency, Numbers, Letters };
			// public readonly static string[] LabelTypeNames = { "None", "Key Names ASCII", "Key Names Unicode", "Key Numbers", "Note Names ASCII",
			//											"Note Names Unicode", "MIDI Note Numbers", "Frequency", "Numbers", "Letters" };

			// Init return value with default/failsafe
			LabelType lt = LabelType.None;

			if (labelName == LABELNAMEnone)
			{
				lt = LabelType.None;
			}
			else
			{
				if (labelName == LABELNAMEkeyNamesASCII)
				{
					lt = LabelType.KeyNamesASCII;
				}
				else
				{
					if (labelName == LABELNAMEkeyNamesUnicode)
					{
						lt = LabelType.KeyNamesUnicode;
					}
					else
					{
						if (labelName == LABELNAMEkeyNumbers)
						{
							lt = LabelType.KeyNumbers;
						}
						else
						{
							if (labelName == LABELNAMEnoteNamesASCII)
							{
								lt = LabelType.NoteNamesASCII;
							}
							else
							{
								if (labelName == LABELNAMEnoteNamesUnicode)
								{
									lt = LabelType.NoteNamesUnicode;
								}
								else
								{
									if (labelName == LABELNAMEmidiNoteNumbers)
									{
										lt = LabelType.MIDINoteNumbers;
									}
									else
									{
										if (labelName == LABELNAMEfrequency)
										{
											lt = LabelType.Frequency;
										}
										else
										{
											if (labelName == LABELNAMEnumbers)
											{
												lt = LabelType.Numbers;
											}
											else
											{
												if (labelName == LABELNAMEletters)
												{
													lt = LabelType.Letters;
												}
												else
												{
													if (labelName == LABELNAMEbpm)
													{
														lt = LabelType.BPM;
													}
													else
													{
														if (labelName == LABELNAMEtempoName)
														{
															lt = LabelType.TempoName;
														}
														else
														{
															string lowName = labelName.ToLower();

															string msg = "Label Type '" + labelName + "' not recognized!";
															Fyle.BUG(msg);
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return lt;
		}





	}

}