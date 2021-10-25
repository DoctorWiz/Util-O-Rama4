using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FileHelper;

namespace LORUtils4
{
	public partial class LORSequence4 : LORMemberBase4, iLORMember4, IComparable<iLORMember4>, IDisposable
	{
		#region XML Tag Constants
		public const string TABLEsequence = "sequence";
		public const string TABLEchannelConfig = "channelConfig";
		public const string TABLErgbChannel = "rgbChannel";
		public const string TABLEchannelGroup = "channelGroup";
		public const string TABLEchannelGroupList = "channelGroupList";
		public const string FIELDchannelGroup = "channelGroup";
		public const string TABLEcellDemarcation = "cellDemarcation";
		public const string TABLEchannelsClipboard = "channelsClipboard";
		public const string TABLEeffect = "effect";
		public const string TABLEtimingGrid = "timingGrid";
		public const string TABLEtrack = "track";
		public const string TABLEcosmicDevice = "cosmicColorDevice";
		public const string STARTtracks = "<Tracks>";
		public const string STARTgrids = "<TimingGrids>";
		public const string TABLEloopLevels = "loopLevels";
		public const string TABLEloopLevel = "loopLevel";
		public const string STARTloops = "<loopLevels>";
		public const string TABLEanimation = "animation";

		private const string STARTsequence = lutils.STFLD + TABLEsequence + LORSeqInfo4.FIELDsaveFileVersion;
		private const string STARTconfig = lutils.STFLD + TABLEchannelConfig + LORSeqInfo4.FIELDchannelConfigFileVersion;
		private const string STARTeffect = lutils.STFLD + TABLEeffect + lutils.FIELDtype;
		public const string STARTchannel = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname;
		private const string STARTcosmic = lutils.STFLD + TABLEcosmicDevice + lutils.SPC;
		public const string STARTrgbChannel = lutils.STFLD + TABLErgbChannel + lutils.SPC;
		public const string STARTchannelGroup = lutils.STFLD + TABLEchannelGroupList + lutils.SPC;
		private const string STARTtrack = lutils.STFLD + TABLEtrack + lutils.SPC;
		private const string STARTtrackItem = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDsavedIndex;
		private const string STARTtimingGrid = lutils.STFLD + TABLEtimingGrid + lutils.SPC;
		private const string STARTtiming = lutils.STFLD + LORTimings4.TABLEtiming + lutils.SPC;
		private const string STARTgridItem = LORTimings4.TABLEtiming + lutils.FIELDcentisecond;
		private const string STARTloopLevel = lutils.STFLD + TABLEloopLevel + lutils.FINFLD;
		private const string STARTloop = lutils.STFLD + LORLoop4.FIELDloop + lutils.SPC;
		private const string STARTaniRow = lutils.STFLD + LORAnimationRow4.FIELDrow + lutils.SPC + LORAnimationRow4.FIELDindex;
		private const string STARTaniCol = lutils.STFLD + LORAnimationColumn4.FIELDcolumnIndex;


		#endregion

		public const int ERROR_Undefined = lutils.UNDEFINED;
		public const int ERROR_NONE = 0;
		public const int ERROR_CantOpen = 101;
		public const int ERROR_NotXML = 102;
		public const int ERROR_NotSequence = 103;
		public const int ERROR_EncryptedDemo = 104;
		public const int ERROR_Compressed = 105;
		public const int ERROR_UnsupportedVersion = 114;
		public const int ERROR_UnexpectedData = 50;

		
		// members should only contain tracks, which are the only DIRECT descendants, in
		// keeping with ChannelGroups, Tracks, and Cosmic Parent items
		public LORMembership4 members = null;
		// AllMembers contain ALL the regular members including Channels and RGBChannels and
		// ChannelGroups which are not DIRECT descendants of the sequence (only Tracks are)
		public LORMembership4 AllMembers = null;

		public List<LORChannel4> Channels = new List<LORChannel4>();
		public List<LORRGBChannel4> RGBchannels = new List<LORRGBChannel4>();
		public List<LORChannelGroup4> ChannelGroups = new List<LORChannelGroup4>();
		public List<LORCosmic4> CosmicDevices = new List<LORCosmic4>();
		public List<LORTimings4> TimingGrids = new List<LORTimings4>();
		public List<LORTrack4> Tracks = new List<LORTrack4>();
		public LORAnimation4 animation = null;
		public LORSeqInfo4 info = null;
		public int videoUsage = 0;
		public int errorStatus = 0;
		public int lineCount = 0;

		// For now at least, this will remain false, therefore ALL timing grids will ALWAYS get written
		private bool WriteSelectedGridsOnly = false;

		//private string myName = "$_UNNAMED_$";
		private StreamWriter writer;
		private string lineOut = ""; // line to be Written out, gets modified if necessary
																 //private int curSavedIndex = 0;
		private string tempFileName;
		private static string tempWorkPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\UtilORama\\";
		private string newFilename;
		private struct Match
		{
			public string Name;
			public int savedIdx;
			public LORMemberType4 MemberType;
			public int itemIdx;
		}

		public static bool debugMode = Fyle.DebugMode;

		#region iLORMember4 Interface

		public override int Centiseconds
		{
			get
			{
				// Return the longest track
				int cs = myCentiseconds;
				for (int t=0; t< Tracks.Count; t++)
				{
					LORTrack4 trk = Tracks[t];
					if (trk.Centiseconds > cs)
					{
						if (trk.Centiseconds > lutils.MINCentiseconds)
						{
							if (trk.Centiseconds < lutils.MAXCentiseconds)
							{
								cs = trk.Centiseconds;
							}
						}
					}
				}
				myCentiseconds = cs;
				return cs;
			}
			set
			{
				// LOR allows different tracks to have different times
				// (which doesn't make much sense to me)
				// So just make sure Sequence.Centiseconds is long enough to hold longest track
				// To change (fix) the number of centiseconds for everthing to a consistant length, use SetTotalTime() function below
				if (value > lutils.MAXCentiseconds)
				{
					string m = "WARNING!! Setting Centiseconds to more than 60 minutes!  Are you sure?";
					Fyle.WriteLogEntry(m, "Warning");
					if (Fyle.DebugMode)
					{
						System.Diagnostics.Debugger.Break();
					}
				}
				else
				{
					if (value < lutils.MINCentiseconds)
					{
						string m = "WARNING!! Setting Centiseconds to less than 1 second!  Are you sure?";
						Fyle.WriteLogEntry(m, "Warning");
						if (Fyle.DebugMode)
						{
							//System.Diagnostics.Debugger.Break();
						}
					}
					else
					{
						for (int t = 0; t < Tracks.Count; t++)
						{
							LORTrack4 trk = Tracks[t];
							if (trk.Centiseconds > value)
							{
								trk.Centiseconds = value;
							}
						}
						myCentiseconds = value;
						MakeDirty(true);
					}
				}
			}
		}

		public void SetTotalTime(int newCentiseconds)
		{
			if (newCentiseconds > lutils.MAXCentiseconds)
			{
				string m = "WARNING!! Setting Centiseconds to more than 60 minutes!  Are you sure?";
				Fyle.WriteLogEntry(m, "Warning");
				if (Fyle.DebugMode)
				{
					System.Diagnostics.Debugger.Break();
				}
			}
			else
			{
				if (newCentiseconds < lutils.MINCentiseconds)
				{
					string m = "WARNING!! Setting Centiseconds to less than 1 second!  Are you sure?";
					Fyle.WriteLogEntry(m, "Warning");
					if (Fyle.DebugMode)
					{
						System.Diagnostics.Debugger.Break();
					}
				}
				else
				{
					CentiFix(newCentiseconds);
					MakeDirty(true);
				}
			}

		}


		public override  LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.Sequence;
			}
		}

		
		public override string LineOut()
		{
			string ret = "";
			// Implemented primarily for compatibility with 'iLORMember4' interface
			//TODO: make this return something, say maybe the first few lines of the file...?
			return ret;
		}

		public override void Parse(string lineIn)
		{
			ReadSequenceFile(lineIn);
		}



		#endregion

		public string filename
		{
			get
			{
				return info.filename;
			}
			set
			{
				info.filename = value;
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// dispose managed resources
				if (writer != null)
				{
					writer.Close();
				}
			}
			// free native resources
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}



		#region Constructors
		public LORSequence4()
		{
			//! DEFAULT CONSTRUCTOR
			this.members = new LORMembership4(this);
			this.AllMembers = new LORMembership4(this);
			//this.Members.SetParent(this);
			this.info = new LORSeqInfo4(this);
			this.animation = new LORAnimation4(this);
			this.info.sequenceType = LORSequenceType4.Animated;
			//Members.SetParentSequence(this);
		}

		public LORSequence4(string theFilename)
		{
			this.members = new LORMembership4(this);
			this.AllMembers = new LORMembership4(this);
			//this.Members.SetParent(this);
			this.info = new LORSeqInfo4(this);
			this.animation = new LORAnimation4(this);
			//Members.SetParentSequence(this);
			ReadSequenceFile(theFilename);
		}
		#endregion

		public LORSequenceType4 LORSequenceType4
		{
			get
			{
				return info.sequenceType;
			}
			set
			{
				info.sequenceType = value;
			}
		}

		//////////////////////////////////////////////////
		//                                             //
		//   * *    R E A D   S E Q U E N C E   * *   //
		//                                           //
		//////////////////////////////////////////////
		#region Read Sequence

		public int ReadSequenceFile(string existingFileName)
		{
			return ReadSequenceFile(existingFileName, false);
		}

		public int ReadSequenceFile(string existingFileName, bool noEffects)
		{
			errorStatus = 0;
			string lineIn; // line read in (does not get modified)
			string xmlInfo = "";
			int li = lutils.UNDEFINED; // positions of certain key text in the line
																//LORTrack4 trk = new LORTrack4();
			LORSequenceType4 st = LORSequenceType4.Undefined;
			string creation = "";
			DateTime modification;

			LORChannel4 lastChannel = null;
			LORRGBChannel4 lastRGBchannel = null;
			LORChannelGroup4 lastGroup = null;
			LORCosmic4 lastCosmic = null;
			LORTrack4 lastTrack = null;
			LORTimings4 lastGrid = null;
			LORLoopLevel4 lastll = null;
			LORAnimationRow4 lastAniRow = null;
			//LORMembership4 lastMembership = null;

			string ext = Path.GetExtension(existingFileName).ToLower();
			if (ext == ".lms") st = LORSequenceType4.Musical;
			if (ext == ".las") st = LORSequenceType4.Animated;
			if (ext == ".lcc") st = LORSequenceType4.ChannelConfig;
			LORSequenceType4 = st;

			Clear(true);

			info.file_accessed = File.GetLastAccessTime(existingFileName);
			info.file_created = File.GetCreationTime(existingFileName);
			info.file_saved = File.GetLastWriteTime(existingFileName);

			const string ERRproc = " in LORSequence4:ReadSequenceFile(";
			const string ERRgrp = "), on Line #";
			// const string ERRitem = ", at position ";
			const string ERRline = ", Code Line #";


			try
			{
				StreamReader reader = new StreamReader(existingFileName);

				// Check for items in the order from most likely item to least likely
				// Effects, Channels,  RGBchannels, Groups, Tracks...

				// Sanity Check #1A, does it have ANY lines?
				if (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();
					xmlInfo = lineIn;
					// Sanity Check #2, is it an XML file?
					//li = lineIn.Substring(0, 6).CompareTo("<?xml ");
					if (lineIn.Substring(0, 6) != "<?xml ")
					//if (li != 0)
					{
						errorStatus = ERROR_NotXML;
						if (lineIn.Substring(0,6) == "******")
						{
							if (!reader.EndOfStream) lineIn = reader.ReadLine();
							if (!reader.EndOfStream) lineIn = reader.ReadLine();
							li = lutils.ContainsKey(lineIn, " Demo ");
							if (li > 0) errorStatus = ERROR_EncryptedDemo;
						}
					}
					else
					{
						// Sanity Check #1B, does it have at least 2 lines?
						if (!reader.EndOfStream)
						{
							lineIn = reader.ReadLine();
							// Sanity Check #3, is it a sequence?
							if ((st == LORSequenceType4.Musical) || (st == LORSequenceType4.Animated))
							{
								//li = lineIn.IndexOf(STARTsequence);
								li = lutils.ContainsKey(lineIn, STARTsequence);
							}
							if (st == LORSequenceType4.ChannelConfig)
							{
								//li = lineIn.IndexOf(STARTconfig);
								li = lutils.ContainsKey(lineIn, STARTconfig);
							}
							if (li != 0)
							{
								errorStatus = ERROR_NotSequence;
								info.infoLine = lineIn;
							}
							else
							{
								info = new LORSeqInfo4(this, lineIn);
								creation = info.createdAt;

								// Save this for later, as they will get changed as we populate the file
								modification = info.lastModified;
								info.filename = existingFileName;

								//myName = Path.GetFileName(existingFileName);
								info.xmlInfo = xmlInfo;
								// Sanity Checks #4A and 4B, does it have a 'SaveFileVersion' and is it '14'
								//   (SaveFileVersion="14" means it cane from LOR Sequence Editor ver 4.x)
								if ((info.saveFileVersion < 1) || (info.saveFileVersion > 14))
								{
									errorStatus = ERROR_UnsupportedVersion;

								}
								else
								{
									// All sanity checks passed
									// * PARSE LINES
									while ((lineIn = reader.ReadLine()) != null)
									{
										lineCount++;
										try
										{
											//! Effects
											if (noEffects)
											{
												li = lutils.UNDEFINED;
											}
											else
											{
												//li = lineIn.IndexOf(STARTeffect);
												li = lutils.ContainsKey(lineIn, STARTeffect);
											}
											if (li > 0)
											{
		///////////////////
		///   EFFECT   ///
		/////////////////
												#region LOREffect4
												while (li > 0)
											{
												lastChannel.AddEffect(lineIn);

												lineIn = reader.ReadLine();
												lineCount++;
												//li = lineIn.IndexOf(STARTeffect);
												li = lutils.ContainsKey(lineIn, STARTeffect);
											}
												#endregion // LOREffect4
											}
											else // Not an LOREffect4
											{
												//! Timings
												//li = lineIn.IndexOf(STARTtiming);
												li = lutils.ContainsKey(lineIn, STARTtiming);
												if (li > 0)
												{
		//////////////////
		///  TIMING   ///
		////////////////
													#region Timing
													int t = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
												lastGrid.AddTiming(t);
													#endregion // Timing
												}
												else // Not a regular channel
												{
													//! Regular Channels
													//li = lineIn.IndexOf(STARTchannel);
													li = lutils.ContainsKey(lineIn, STARTchannel);
													if (li > 0)
													{
		////////////////////////////
		///   REGULAR CHANNEL   ///
		//////////////////////////
														#region Regular LORChannel4
														lastChannel = ParseChannel(lineIn);
														#endregion // Regular LORChannel4
													}
													else // Not a regular channel
													{
														//! RGB Channels
														//li = lineIn.IndexOf(STARTrgbChannel);
														li = lutils.ContainsKey(lineIn, STARTrgbChannel);
														if (li > 0)
														{
		////////////////////////
		///   RGB CHANNEL   ///
		//////////////////////
															#region RGB LORChannel4
															lastRGBchannel = ParseRGBchannel(lineIn);
														lineIn = reader.ReadLine();
														lineCount++;
														lineIn = reader.ReadLine();
														lineCount++;

														// RED
														int csi = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
														LORChannel4 ch = (LORChannel4)AllMembers.BySavedIndex[csi];
														lastRGBchannel.redChannel = ch;
														ch.rgbChild = LORRGBChild4.Red;
														ch.rgbParent = lastRGBchannel;
														lineIn = reader.ReadLine();
														lineCount++;

														// GREEN
														csi = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
														ch = (LORChannel4)AllMembers.BySavedIndex[csi];
														lastRGBchannel.grnChannel = ch;
														ch.rgbChild = LORRGBChild4.Green;
														ch.rgbParent = lastRGBchannel;
														lineIn = reader.ReadLine();
														lineCount++;

														// BLUE
														csi = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
														ch = (LORChannel4)AllMembers.BySavedIndex[csi];
														lastRGBchannel.bluChannel = ch;
														ch.rgbChild = LORRGBChild4.Blue;
														ch.rgbParent = lastRGBchannel;
															#endregion // RGB LORChannel4
														}
														else  // Not an RGB LORChannel4
														{
															//! Channel Groups
															//li = lineIn.IndexOf(STARTchannelGroup);
															li = lutils.ContainsKey(lineIn, STARTchannelGroup);
															if (li > 0)
															{
			//////////////////////////
		///   CHANNEL GROUP   ///
		////////////////////////
																#region Channel Group
																lastGroup = ParseChannelGroup(lineIn);
															//li = lineIn.IndexOf(lutils.ENDFLD);
															li = lutils.ContainsKey(lineIn, lutils.ENDFLD);
															if (li < 0)
															{
																lineIn = reader.ReadLine();
																lineCount++;
																lineIn = reader.ReadLine();
																lineCount++;
																//li = lineIn.IndexOf(TABLEchannelGroup + lutils.FIELDsavedIndex);
																li = lutils.ContainsKey(lineIn, TABLEchannelGroup + lutils.FIELDsavedIndex);
																while (li > 0)
																{
																	int isl = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
																	lastGroup.Members.Add(AllMembers.BySavedIndex[isl]);
																	//savedIndexes[isl].parents.Add(lastGroup.SavedIndex);

																	lineIn = reader.ReadLine();
																	lineCount++;
																	//li = lineIn.IndexOf(TABLEchannelGroup + lutils.FIELDsavedIndex);
																	li = lutils.ContainsKey(lineIn, TABLEchannelGroup + lutils.FIELDsavedIndex);
																}
															}
																#endregion // Channel Group
															}
															else // Not a LORChannelGroup4
															{
																//! Cosmic Color Devices
																li = lutils.ContainsKey(lineIn, STARTcosmic);
																if (li > 0)
																{
		////////////////////////////////
		///   COSMIC COLOR DEVICE   ///
		//////////////////////////////
																	#region Cosmic Color Device
																	lastCosmic = ParseCosmicDevice(lineIn);
																	li = lutils.ContainsKey(lineIn, lutils.ENDFLD);
																	if (li < 0)
																	{
																		lineIn = reader.ReadLine();
																		lineCount++;
																		lineIn = reader.ReadLine();
																		lineCount++;
																		// Cosmic Color Devices are just like groups
																		// inlcuding how the list of child nodes is done
																		li = lutils.ContainsKey(lineIn, TABLEchannelGroup + lutils.FIELDsavedIndex);
																		while (li > 0)
																		{
																			int isl = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
																			iLORMember4 mm = AllMembers.BySavedIndex[isl]
;																			lastCosmic.Members.Add(mm);

																			lineIn = reader.ReadLine();
																			lineCount++;
																			li = lutils.ContainsKey(lineIn, TABLEchannelGroup + lutils.FIELDsavedIndex);
																		}
																	}
																	#endregion // Cosmic Color Device
																}
																else // Not a Cosmic Device
																{
																	//! Track Items
																	//li = lineIn.IndexOf(STARTtrackItem);
																	if (lineCount == 972) System.Diagnostics.Debugger.Break();
																	if (lineCount == 200) System.Diagnostics.Debugger.Break();
																	li = lutils.ContainsKey(lineIn, STARTtrackItem);
																	if (li > 0)
																	{
		///////////////////////
		///   TRACK ITEM   ///
		/////////////////////
																		#region LORTrack4 Item
																		int si = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
																		lastTrack.Members.Add(AllMembers.BySavedIndex[si]);
																		#endregion // LORTrack4 Item
																	}
																	else // Not a regular channel
																	{
																		//! Tracks

																		//! Tracks are [apparently] getting added twice														


																		//li = lineIn.IndexOf(STARTtrack);
																		li = lutils.ContainsKey(lineIn, STARTtrack);
																		if (li > 0)
																		{
		//////////////////
		///   TRACK   ///
		////////////////
																			#region LORTrack4
																			lastTrack = ParseTrack(lineIn);
																			//for (int tg = 0; tg < TimingGrids.Count; tg++)
																			//{
																			//lastTrack.timingGrid == Members.bySaveID[TimingGrids[tg].SaveID];
																			//TODO: Assign Timing Grid!!!!
																			//{
																			//	lastTrack.timingGridObjIndex = tg;
																			//	tg = TimingGrids.Count; // break
																			//}
																			//}
																			//li = lineIn.IndexOf(lutils.ENDFLD);
																			li = lutils.ContainsKey(lineIn, lutils.ENDFLD);
																			if (li < 0)
																			{
																				lineIn = reader.ReadLine();
																				lineCount++;
																				lineIn = reader.ReadLine();
																				lineCount++;
																				//li = lineIn.IndexOf(STARTtrackItem);
																				li = lutils.ContainsKey(lineIn, STARTtrackItem);
																				while (li > 0)
																				{
																					int isi = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
																					//lastTrack.itemSavedIndexes.Add(isi);
																					if (isi == 2189) System.Diagnostics.Debugger.Break();
																					if (isi <= AllMembers.HighestSavedIndex)
																					{
																						iLORMember4 SIMem = AllMembers.BySavedIndex[isi];
																						if (SIMem != null)
																						{
																							lastTrack.Members.Add(SIMem);
																						}
																						else
																						{
																							///WTF why wasn't if found?!?!
																							System.Diagnostics.Debugger.Break();
																						}
																					}
																					else
																					{
																						///WTF why wasn't if found?!?!
																						System.Diagnostics.Debugger.Break();
																					}
																					//savedIndexes[isi].parents.Add(-100 - Tracks.Count);

																					lineIn = reader.ReadLine();
																					lineCount++;
																					//li = lineIn.IndexOf(STARTtrackItem);
																					li = lutils.ContainsKey(lineIn, STARTtrackItem);
																				}
																			}
																			#endregion // LORTrack4
																		} // end if a track
																		else // not a track
																		{
																			//! Timing Grids
																			//li = lineIn.IndexOf(STARTtimingGrid);
																			li = lutils.ContainsKey(lineIn, STARTtimingGrid);
																			if (li > 0)
																			{
		////////////////////////
		///   TIMING GRID   ///
		//////////////////////
																				#region Timing Grid
																				lastGrid = ParseTimingGrid(lineIn);
																				if (lastGrid.LORTimingGridType4 == LORTimingGridType4.Freeform)
																				{
																					lineIn = reader.ReadLine();
																					lineCount++;
																					//li = lineIn.IndexOf(STARTgridItem);
																					li = lutils.ContainsKey(lineIn, STARTgridItem);
																					while (li > 0)
																					{
																						int gpos = lutils.getKeyValue(lineIn, lutils.FIELDcentisecond);
																						lastGrid.AddTiming(gpos);
																						lineIn = reader.ReadLine();
																						lineCount++;
																						//li = lineIn.IndexOf(STARTgridItem);
																						li = lutils.ContainsKey(lineIn, STARTgridItem);
																					}
																				}
																				#endregion
																			}
																			else // Not a timing grid
																			{
																				//! Loop Levels
																				//li = lineIn.IndexOf(STARTloopLevel);
																				li = lutils.ContainsKey(lineIn, STARTloopLevel);
																				if (li > 0)
																				{
																					lastll = lastTrack.AddLoopLevel(lineIn);
																				}
																				else // not a loop level
																				{
																					//! Loops
																					//li = lineIn.IndexOf(STARTloop);
																					li = lutils.ContainsKey(lineIn, STARTloop);
																					if (li > 0)
																					{
																						lastll.AddLoop(lineIn);
																					}
																					else // not a loop
																					{
																						//! Animation Rows
																						//li = lineIn.IndexOf(STARTaniRow);
																						li = lutils.ContainsKey(lineIn, STARTaniRow);
																						if (li > 0)
																						{
																							lastAniRow = animation.AddRow(lineIn);
																						}
																						else
																						{
																							//! Animation Columns
																							//li = lineIn.IndexOf(STARTaniCol);
																							li = lutils.ContainsKey(lineIn, STARTaniCol);
																							if (li > 1)
																							{
																								lastAniRow.AddColumn(lineIn);
																							} // end animationColumn
																							else
																							{
																								//! Animation
																								//li = lineIn.IndexOf(lutils.STFLD + TABLEanimation + lutils.SPC);
																								li = lutils.ContainsKey(lineIn, lutils.STFLD + TABLEanimation + lutils.SPC);
																								if (li > 0)
																								{
																									animation = new LORAnimation4(this, lineIn);
																								} // end if Animation, or not
																							} // end if AnimationColumn4, or not
																						} // end if Animation, or not
																					} // end if LoopLevel, or not
																				} // end if Loop, or not (as in a loopLevel loop, not a for loop)
																			} // end Timings (or not)
																		} // end Track (or not)
																	} // end Track Items (or not)
																} // end Cosmic Color Device (or not)
														} // end ChannelGroup (or not)
													} // end RGBChannel (or not)
												} // end regular Channel (or not)
											} // end timing (or not)
										} // end Effect (or not)
											
								} // end 2nd Try
										catch (Exception ex)
										{
											StackTrace strx = new StackTrace(ex, true);
											StackFrame sf = strx.GetFrame(strx.FrameCount - 1);
											string emsg = ex.Message + lutils.CRLF;
											emsg += "at LORSequence4.ReadSequence()" + lutils.CRLF;
											emsg += "File:" + existingFileName + lutils.CRLF;
											emsg += "on line " + lineCount.ToString() + " at position " + li.ToString() + lutils.CRLF;
											emsg += "Line Is:" + lineIn + lutils.CRLF;
											emsg += "in code line " + sf.GetFileLineNumber() + lutils.CRLF;
											emsg += "Last SavedIndex = " + AllMembers.HighestSavedIndex.ToString();
											info.LastError.fileLine = lineCount;
											info.LastError.linePos = li;
											info.LastError.codeLine = sf.GetFileLineNumber();
											info.LastError.errName = ex.ToString();
											info.LastError.errMsg = emsg;
											info.LastError.lineIn = lineIn;

	#if DEBUG
											System.Diagnostics.Debugger.Break();
	#endif
											Fyle.WriteLogEntry(emsg, lutils.LOG_Error);
											if (debugMode)
											{
												DialogResult dr1 = MessageBox.Show(emsg, "Error Reading Sequence File", MessageBoxButtons.OK, MessageBoxIcon.Error);
												System.Diagnostics.Debugger.Break();
											}
											errorStatus = ERROR_UnexpectedData;
										} // end catch
										
									} // end while lines remain
								} // end SaveFileVersion = 14

								// Restore these to the values we captured when first reading the file info header
								info.createdAt = creation;
								info.lastModified = info.file_saved;
								MakeDirty(false);
								AllMembers.ReIndex();
							} // end second line is sequence info
						} // end has a second line
					} // end first line was xml info
				} // end has a first line


				reader.Close();

			} // end first try
			catch (Exception ex)
			{
				StackTrace strc = new StackTrace(ex, true);
				StackFrame sf = strc.GetFrame(strc.FrameCount - 1);
				string emsg = ex.ToString();
				emsg += ERRproc + existingFileName + ERRgrp + "none";
				emsg += ERRline + sf.GetFileLineNumber();
#if DEBUG
				System.Diagnostics.Debugger.Break();
#endif
				Fyle.WriteLogEntry(emsg, lutils.LOG_Error);
				if (debugMode)
				{
					DialogResult dr2 = MessageBox.Show(emsg, "Error Opening Sequence File", MessageBoxButtons.OK, MessageBoxIcon.Error);
					System.Diagnostics.Debugger.Break();
				}
				errorStatus = ERROR_CantOpen;
			} // end first catch


			if (errorStatus < 100)
			{
				info.filename = existingFileName;
				//! for debugging
				//string sMsg = summary();
				//MessageBox.Show(sMsg, "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			return errorStatus;
		} // end ReadSequenceFile
		#endregion

		////////////////////////////////////////////////////
		//                                               //
		//   * *    W R I T E   S E Q U E N C E   * *   //
		//                                             //
		////////////////////////////////////////////////
		#region Write Sequence

		public int WriteSequenceFile(string newFileName)
		{
			return WriteSequenceFile_DisplayOrder(newFileName, false, false);
		}

		public int WriteSequenceFile_DisplayOrder(string newFileName)
		{
			return WriteSequenceFile_DisplayOrder(newFileName, false, false);
		}

		public int WriteSequenceFile_DisplayOrder(string newFileName, bool selectedOnly, bool noEffects)
		{
			List<int> masterSIs = new List<int>();
			//int altSI = lutils.UNDEFINED;
			updatedTrack[] updatedTracks = new updatedTrack[Tracks.Count];
			//bySavedIndex.AltHighestSavedIndex = lutils.UNDEFINED;
			//bySavedIndex.altSaveID = lutils.UNDEFINED;
			//altSavedIndexes = null;
			AllMembers.ResetWritten();
			//Array.Resize(ref altSavedIndexes, highestSavedIndex + 3);
			//Array.Resize(ref altSaveIDs, TimingGrids.Count + 1);
			string ext = Path.GetExtension(newFileName).ToLower();
			bool channelConfig = false;
			if (ext == ".lcc") channelConfig = true;
			if (channelConfig) noEffects = true;
			//TODO: implement channelConfig flag to write just a channel config file

			// Clear any 'Written' flags from a previous save
			ClearWrittenFlags();

			// Write the Timing Grids
			if (WriteSelectedGridsOnly)
			{
				//! First, A LORTrack4->LORTimings4 renumbering
				// Timing Grids do not get Written to the file yet
				// But we must renumber the saveIDs
				// Assign new altSaveIDs in the order they appear in the Tracks
				foreach (LORTrack4 theTrack in Tracks)
				{
					if ((!selectedOnly) || (theTrack.Selected))
					{
						if (theTrack.timingGrid == null)
						{
							// File integrity check, should never happen under normal circumstances with a sequence
							// But it happens when starting from a channel config file
							if (TimingGrids.Count > 0)
							{
								// just use the first one
								theTrack.timingGrid = TimingGrids[0];
							}
							else
							{
								// Create one
								LORTimings4 ntg = CreateTimingGrid("Fixed Grid .05");
								ntg.spacing = 5;
								theTrack.timingGrid = ntg;
							}
						}
						int asi = AssignNextAltSaveID(theTrack.timingGrid);
					}
				}
				for (int tg = 0; tg < TimingGrids.Count; tg++)
				{
					LORTimings4 theGrid = TimingGrids[tg];
					// Any remaining timing grids that are Selected, but not used by any Tracks
					if ((!selectedOnly) || (theGrid.Selected))
					{
						if (theGrid.AltSaveID == lutils.UNDEFINED)
						{
							int asi = AssignNextAltSaveID(theGrid);
							theGrid.AltSaveID = asi;
							//altSaveIDs[tg] = altSaveID;
						}
					}
				}
			}
			else // if (WriteSelectedGridsOnly)
			{
				// ALWAYS write ALL timing Grids, and keep same numerical order
				for (int tg = 0; tg < TimingGrids.Count; tg++)
				{
					LORTimings4 theGrid = TimingGrids[tg];
					if (theGrid.AltSaveID == lutils.UNDEFINED)
					{
						int asi = AssignNextAltSaveID(theGrid);
						theGrid.AltSaveID = asi;
					}
				}
			}

			//! NOW it's time to write the file
			// Write the first line of the new sequence, containing the XML info
			WriteSequenceStart(newFileName);

			// Start with Channels (regular, RGB, and Groups)
			lineOut = lutils.LEVEL1 + lutils.STFLD + lutils.TABLEchannel + lutils.PLURAL + lutils.FINFLD;
			writer.WriteLine(lineOut);

			// LORLoop4 thru Tracks and write the items (details) in the order they appear
			foreach (LORTrack4 theTrack in Tracks)
			{
				if ((!selectedOnly) || (theTrack.Selected))
				{
					WriteListItems(theTrack.Members, selectedOnly, noEffects, LORMemberType4.Items);
				}
			}
			// All Channels should now be Written, close this section
			lineOut = lutils.LEVEL1 + lutils.FINTBL + lutils.TABLEchannel + lutils.PLURAL + lutils.FINFLD;
			writer.WriteLine(lineOut);

			// TIMING GRIDS
			lineOut = lutils.LEVEL1 + lutils.STFLD + LORSequence4.TABLEtimingGrid + lutils.PLURAL + lutils.FINFLD;
			writer.WriteLine(lineOut);
			foreach (LORTimings4 theGrid in TimingGrids)
			{
				// TIMING GRIDS
				if ((!selectedOnly) || (theGrid.Selected))
				{
					WriteTimingGrid(theGrid);
				}
			}
			lineOut = lutils.LEVEL1 + lutils.FINTBL + LORSequence4.TABLEtimingGrid + lutils.PLURAL + lutils.FINFLD;
			writer.WriteLine(lineOut);

			// TRACKS
			lineOut = lutils.LEVEL1 + lutils.STFLD + TABLEtrack + lutils.PLURAL + lutils.FINFLD;
			writer.WriteLine(lineOut);
			// LORLoop4 thru Tracks
			foreach (LORTrack4 theTrack in Tracks)
			{
				if ((!selectedOnly) || (theTrack.Selected))
				{
					// Items in the track have already been written
					// This writes the track info itself including its member list
					// and loop levels
					WriteTrack(theTrack, selectedOnly, LORMemberType4.Items);
				}
			}
			lineOut = lutils.LEVEL1 + lutils.FINTBL + TABLEtrack + lutils.PLURAL + lutils.FINFLD;
			writer.WriteLine(lineOut);

			WriteSequenceClose();

			errorStatus = RenameTempFile(newFileName);
			if (filename.Length < 3)
			{
				//filename = newFileName;
				info.filename = newFileName;
			}
			MakeDirty(false);
			return errorStatus;
		} // end WriteSequenceFileInDisplayOrder

		private int WriteSequenceStart(string newFileName)
		{
			//string lineOut = "";

			errorStatus = 0;
			lineCount = 0;
			newFilename = newFileName;
			if (!Directory.Exists(tempWorkPath)) Directory.CreateDirectory(tempWorkPath);
			tempFileName = tempWorkPath + Path.GetFileNameWithoutExtension(newFilename) + ".tmp";
			writer = new StreamWriter(tempFileName);

			// Write the first line of the new sequence, containing the XML info
			lineOut = info.xmlInfo;
			writer.WriteLine(lineOut);
			//string createdAt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
			lineOut = info.LineOut();
			writer.WriteLine(lineOut);

			lineOut = "";

			return errorStatus;
		}

		public int WriteChannel(LORChannel4 theChannel)
		{
			return WriteChannel(theChannel, false);
		}

		public int WriteChannel(LORChannel4 theChannel, bool noEffects)
		{
			//theChannel.AltSavedIndex = bySavedIndex.GetNextAltSavedIndex(theChannel.SavedIndex);
			if (theChannel.AltSavedIndex < 0) // (!Written)
			{
				int altSI = AssignNextAltSavedIndex(theChannel);
				theChannel.AltSavedIndex = altSI;
				lineOut = theChannel.LineOut(noEffects);
				writer.WriteLine(lineOut);
				//theChannel.Written = true;
			}
			else
			{
				// Why is this being called, didn't I already check the Written state?
				//System.Diagnostics.Debugger.Break();
			}
			return theChannel.AltSavedIndex;
		}

		private int WriteRGBchannel(LORRGBChannel4 theRGBchannel)
		{
			return WriteRGBchannel(theRGBchannel, false, false, LORMemberType4.Items);
		}

		private int WriteRGBchannel(LORRGBChannel4 theRGBchannel, bool selectedOnly, bool noEffects, LORMemberType4 itemTypes)
		{
			if (!theRGBchannel.Written)
			{
				if ((!selectedOnly) || (theRGBchannel.Selected))
				{
					if ((itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.Channel))
					{
						//theRGBchannel.LineOut(selectedOnly, noEffects, LORMemberType4.Channel);
						//lineOut = theRGBchannel.redChannel.LineOut(noEffects);
						//lineOut += lutils.CRLF + theRGBchannel.grnChannel.LineOut(noEffects);
						//lineOut += lutils.CRLF + theRGBchannel.bluChannel.LineOut(noEffects);
						WriteChannel(theRGBchannel.redChannel, noEffects);
						WriteChannel(theRGBchannel.grnChannel, noEffects);
						WriteChannel(theRGBchannel.bluChannel, noEffects);
						//writer.WriteLine(lineOut);
					}

					if ((itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.RGBChannel))
					{
						//theRGBchannel.AltSavedIndex = bySavedIndex.GetNextAltSavedIndex(theRGBchannel.SavedIndex);
						int altSI = AssignNextAltSavedIndex(theRGBchannel);
						lineOut = theRGBchannel.LineOut(selectedOnly, noEffects, LORMemberType4.RGBChannel);
						writer.WriteLine(lineOut);
						//theRGBchannel.Written = true;
					}
				}
			}
			return theRGBchannel.AltSavedIndex;
		} // end writergbChannel

		private int WriteChannelGroup(LORChannelGroup4 theGroup)
		{
			return WriteChannelGroup(theGroup, false, false, LORMemberType4.Items);
		}

		private int WriteChannelGroup(LORChannelGroup4 theGroup, bool selectedOnly, bool noEffects, LORMemberType4 itemTypes)
		{
			// May be called recursively (because groups can contain groups)
			List<int> altSIs = new List<int>();
			//if (theGroup.Name.IndexOf("ng Parts") > 0) System.Diagnostics.Debugger.Break();
			//if (theGroup.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();

			if (!theGroup.Written)
			{
				if ((!selectedOnly) || (theGroup.Selected))
				{
					if ((itemTypes == LORMemberType4.Items) ||
						(itemTypes == LORMemberType4.Channel) ||
						(itemTypes == LORMemberType4.RGBChannel))
					{
						// WriteListItems may in turn call this procedure (WriteChannelGroup) for any Child Channel Groups
						// Therefore making it recursive at this point
						altSIs = WriteListItems(theGroup.Members, selectedOnly, noEffects, itemTypes);
					}

					if ((itemTypes == LORMemberType4.Items) ||
						(itemTypes == LORMemberType4.ChannelGroup))
					{
						//theGroup.AltSavedIndex = bySavedIndex.GetNextAltSavedIndex(theGroup.SavedIndex);
						//if (altSIs.Count > 0)
						//{
						int altSI = AssignNextAltSavedIndex(theGroup);
						theGroup.AltSavedIndex = altSI;
						//lineOut = theGroup.LineOut(selectedOnly);

						lineOut = lutils.LEVEL2 + lutils.STFLD + LORSequence4.TABLEchannelGroupList;
						lineOut += lutils.FIELDtotalCentiseconds + lutils.FIELDEQ + theGroup.Centiseconds.ToString() + lutils.ENDQT;
						lineOut += lutils.FIELDname + lutils.FIELDEQ + lutils.XMLifyName(theGroup.Name) + lutils.ENDQT;
						lineOut += lutils.FIELDsavedIndex + lutils.FIELDEQ + altSI.ToString() + lutils.ENDQT;
						lineOut += lutils.FINFLD;
						writer.WriteLine(lineOut);

						WriteItemsList(theGroup.Members, selectedOnly, itemTypes);

						lineOut = lutils.LEVEL2 + lutils.FINTBL + LORSequence4.TABLEchannelGroupList + lutils.FINFLD;
						writer.WriteLine(lineOut);
						//}

						//curSavedIndex++;
						// copy the original SavedIndex to the altSavedIndexes at the altHighestSavedIndex position
						//altSavedIndexes[altHighestSavedIndex] = savedIndexes[theGroup.SavedIndex].altCopy();
						// cross reference the new AltSavedIndex to the original SavedIndex
						//altSavedIndexes[altHighestSavedIndex].AltSavedIndex = theGroup.SavedIndex;
						// and cross reference the original saved index to the new AltSavedIndex
						//savedIndexes[theGroup.SavedIndex].AltSavedIndex = altHighestSavedIndex;
					}
				}
			}
			return theGroup.AltSavedIndex;
		} // end writeChannelGroup

		private int WriteTrack(LORTrack4 theTrack, bool selectedOnly, LORMemberType4 itemTypes)
		{
			string lineOut = lutils.LEVEL2 + lutils.STFLD + LORSequence4.TABLEtrack;
			//! LOR writes it with the Name last
			// In theory, it shouldn't matter
			//if (Name.Length > 1)
			//{
			//	ret += lutils.SPC + FIELDname + lutils.FIELDEQ + Name + lutils.ENDQT;
			//}
			lineOut += lutils.FIELDtotalCentiseconds + lutils.FIELDEQ + theTrack.Centiseconds.ToString() + lutils.ENDQT;
			int altID = 0;
			if (theTrack.timingGrid == null)
			{
				if (TimingGrids.Count == 0)
				{
					Fyle.BUG("Why are there no Timing Grids?");
				}
				else
				{
					theTrack.timingGrid = TimingGrids[0];
					altID = theTrack.timingGrid.AltSaveID;
				}
			}
			// if track is selected, but it's timing grid isn't, default to grid 0
			if (altID < 0) altID = 0;
			lineOut += lutils.SPC + LORSequence4.TABLEtimingGrid + lutils.FIELDEQ + altID.ToString() + lutils.ENDQT;
			// LOR writes it with the Name last
			if (theTrack.Name.Length > 1)
			{
				lineOut += lutils.FIELDname + lutils.FIELDEQ + lutils.XMLifyName(theTrack.Name) + lutils.ENDQT;
			}
			lineOut += lutils.FINFLD;
			//int siOld = lutils.UNDEFINED;
			//int siAlt = lutils.UNDEFINED;
			writer.WriteLine(lineOut);

			WriteItemsList(theTrack.Members, selectedOnly, itemTypes);

			// Write out any LoopLevels in this track
			lineOut = "";
			if (theTrack.loopLevels.Count > 0)
			{
				lineOut += lutils.LEVEL3 + lutils.STFLD + LORSequence4.TABLEloopLevels + lutils.FINFLD + lutils.CRLF;
				foreach (LORLoopLevel4 ll in theTrack.loopLevels)
				{
					lineOut += ll.LineOut() + lutils.CRLF;
				}
				lineOut += lutils.LEVEL3 + lutils.FINTBL + LORSequence4.TABLEloopLevels + lutils.FINFLD + lutils.CRLF;
			}
			else
			{
				lineOut += lutils.LEVEL3 + lutils.STFLD + LORSequence4.TABLEloopLevels + lutils.ENDFLD + lutils.CRLF;
			}

			// finish the track
			lineOut += lutils.LEVEL2 + lutils.FINTBL + LORSequence4.TABLEtrack + lutils.FINFLD;
			writer.WriteLine(lineOut);


			return theTrack.AltSavedIndex;
		}

		private List<int> WriteListItems(LORMembership4 itemIDs, bool selectedOnly, bool noEffects, LORMemberType4 itemTypes)
		{
			// NOTE: This writes out all the individual items in a membership list
			// It is recursive, also writing any items in subgroups
			// This does NOT write the list of items, that is handled later by the counterpart to this, 'WriteItemsList'

			int altSaveIndex = lutils.UNDEFINED;
			List<int> altSIs = new List<int>();
			string itsName = "";  //! for debugging

			//if (itemIDs.Owner.Name.IndexOf("ng Parts") > 0) System.Diagnostics.Debugger.Break();
			//if (itemIDs.Owner.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();
			//Identity id = null;

			foreach (iLORMember4 item in itemIDs.Items)
			{
				// if (item.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();

				//id = Members.BySavedIndex[si];
				itsName = item.Name;
				if ((!selectedOnly) || (item.Selected))
				{
					if (!item.Written)
					{
						if (item.MemberType == LORMemberType4.Channel)
						{
							// Prevents unnecessary processing of Channels which have already been Written, during RGB channel and group processing
							if ((itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.Channel))
							{
								altSaveIndex = WriteChannel((LORChannel4)item, noEffects);
								altSIs.Add(altSaveIndex);
							}
						}
						else
						{
							if (item.MemberType == LORMemberType4.RGBChannel)
							{
								LORRGBChannel4 theRGB = (LORRGBChannel4)item;
								if ((itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.Channel) || (itemTypes == LORMemberType4.RGBChannel))
								{
									altSaveIndex = WriteRGBchannel(theRGB, selectedOnly, noEffects, itemTypes);
									altSIs.Add(altSaveIndex);
								}
							}
							else
							{
								if (item.MemberType == LORMemberType4.ChannelGroup)
								{
									//if (itemTypes == LORMemberType4.channelGroup)
									//if ((itemTypes == LORMemberType4.None) ||
									//    (itemTypes == LORMemberType4.rgbChannel) ||
									//    (itemTypes == LORMemberType4.channel) ||
									//    (itemTypes == LORMemberType4.channelGroup))
									// Type NONE actually means ALL in this case
									//{

									//									if (item.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break(); // Break here for debugging if named group is found


									// WriteChannelGroup calls this procedure (WriteListItems) so this is where it gets recursive
									altSaveIndex = WriteChannelGroup((LORChannelGroup4)item, selectedOnly, noEffects, itemTypes);
									altSIs.Add(altSaveIndex);
									//}
								} // if LORChannelGroup4, or not
							} // if LORRGBChannel4, or not
						} // if regular LORChannel4, or not
					} // if not Written
				} // if Selected
			} // loop thru items

			return altSIs;
		}

		private int WriteItemsList(LORMembership4 itemIDs, bool selectedOnly, LORMemberType4 itemTypes)
		{
			// NOTE: This writes out the list of items in a membership list.
			// It is NOT recursive.
			// This does NOT write the the individual items, that was handled previously by the counterpart to this, 'WriteListItems'

			//if (itemIDs.Owner.Name.IndexOf("ng Parts") > 0) System.Diagnostics.Debugger.Break();
			//if (itemIDs.Owner.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();

			int count = 0;
			string leader = lutils.LEVEL4 + lutils.STFLD;


			string lineOut = lutils.LEVEL3 + lutils.STFLD;
			if (itemIDs.Owner.MemberType == LORMemberType4.Track)
			{
				lineOut += lutils.TABLEchannel;
				leader += lutils.TABLEchannel;
			}
			if (itemIDs.Owner.MemberType == LORMemberType4.ChannelGroup)
			{
				lineOut += LORSequence4.TABLEchannelGroup;
				leader += LORSequence4.TABLEchannelGroup;
			}
			lineOut += lutils.PLURAL + lutils.FINFLD + lutils.CRLF;

			// LORLoop4 thru all items in membership list
			foreach (iLORMember4 subID in itemIDs.Items)
			{
				//if (subID.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();

				if (subID.Written)  // The item itself should have already been written!
				{
					bool sel = subID.Selected;
					if (!selectedOnly || sel)
					{
						if ((itemTypes == subID.MemberType) || (itemTypes == LORMemberType4.Items))
						{
							int siAlt = subID.AltSavedIndex;
							if (siAlt > lutils.UNDEFINED)
							{
								lineOut += leader;
								lineOut += lutils.FIELDsavedIndex + lutils.FIELDEQ + siAlt.ToString() + lutils.ENDQT;
								lineOut += lutils.ENDFLD + lutils.CRLF;
								count++;
							}
						}
					}
				}
			}

			// Close the list of items
			lineOut += lutils.LEVEL3 + lutils.FINTBL;
			if (itemIDs.Owner.MemberType == LORMemberType4.Track)
			{
				lineOut += lutils.TABLEchannel;
			}
			if (itemIDs.Owner.MemberType == LORMemberType4.ChannelGroup)
			{
				lineOut += LORSequence4.TABLEchannelGroup;
			}
			lineOut += lutils.PLURAL + lutils.FINFLD;

			writer.WriteLine(lineOut);
			return count;
		}

		public int WriteItem(int SavedIndex)
		{
			return WriteItem(SavedIndex, false, false, LORMemberType4.Items);
		}

		public int WriteItem(int SavedIndex, bool selectedOnly, bool noEffects, LORMemberType4 theType)
		{
			int ret = lutils.UNDEFINED;

			iLORMember4 member = AllMembers.BySavedIndex[SavedIndex];
			if (!member.Written)
			{
				if (!selectedOnly || member.Selected)
				{
					LORMemberType4 itemType = member.MemberType;
					if (itemType == LORMemberType4.Channel)
					{
						LORChannel4 theChannel = (LORChannel4)member;
						if ((theType == LORMemberType4.Channel) || (theType == LORMemberType4.Items))
						{
							ret = WriteChannel(theChannel, noEffects);
						} // end if type
					} // end if LORChannel4
					else
					{
						if (itemType == LORMemberType4.RGBChannel)
						{
							LORRGBChannel4 theRGB = (LORRGBChannel4)member;
							ret = WriteRGBchannel(theRGB, selectedOnly, noEffects, theType);
						} // end if LORRGBChannel4
						else
						{
							if (itemType == LORMemberType4.ChannelGroup)
							{
								LORChannelGroup4 theGroup = (LORChannelGroup4)member;
								ret = WriteChannelGroup(theGroup, selectedOnly, noEffects, theType);
							} // end if LORRGBChannel4
						} // LORRGBChannel4, or not
					} // end a channel, or not
				} // end Selected
			} // end if not Written

			return ret;
		}



		private int WriteTimingGrid(LORTimings4 theGrid)
		{
			return WriteTimingGrid(theGrid, false);
		}

		private int WriteTimingGrid(LORTimings4 theGrid, bool selectedOnly)
		{
			int altSI = AssignNextAltSaveID(theGrid);
			lineOut = theGrid.LineOut();
			writer.WriteLine(lineOut);
			return theGrid.AltSavedIndex;

		} // end writeTimingGrids

		private void WriteSequenceClose()
		{
			string lineOut = "";

			// Write out Animation info, it it exists
			WriteAnimation();

			// Close the sequence
			lineOut = lutils.FINTBL + TABLEsequence + lutils.FINFLD; // "</sequence>";
			writer.WriteLine(lineOut);

			Console.WriteLine(lineCount.ToString() + " Out:" + lineOut);
			Console.WriteLine("");

			// We're done writing the file
			writer.Flush();
			writer.Close();
			info.file_saved = DateTime.Now;
			MakeDirty(false);
		}

		private void WriteAnimation()
		{
			if (animation != null)
			{
				if (animation.sections > 0)
				{
					lineOut = animation.LineOut();
					writer.WriteLine(lineOut);
				}
			} // end if animation != null
		} // end writeAnimation

		private void ClearWrittenFlags()
		{
			foreach (LORChannel4 ch in Channels)
			{
				//ch.Written = false;
				ch.AltSavedIndex = lutils.UNDEFINED;
			}
			foreach (LORRGBChannel4 rch in RGBchannels)
			{
				//rch.Written = false;
				rch.AltSavedIndex = lutils.UNDEFINED;
			}
			foreach (LORChannelGroup4 chg in ChannelGroups)
			{
				//chg.Written = false;
				chg.AltSavedIndex = lutils.UNDEFINED;
			}
			foreach (LORCosmic4 dev in CosmicDevices)
			{
				//chg.Written = false;
				dev.AltSavedIndex = lutils.UNDEFINED;
			}
			foreach (LORTrack4 tr in Tracks)
			{
				tr.AltSavedIndex = lutils.UNDEFINED;
			}
			foreach (LORTimings4 tg in TimingGrids)
			{
				//tg.Written = false;
				tg.AltSavedIndex = lutils.UNDEFINED;
			}
		} // end ClearWrittenFlags

		#endregion


		public override iLORMember4 Parent
		{
			get
			{
				// I am my own parent!
				return this;
			}
		}

		public static string DefaultSequencesPath
		{
			get
			{
				return lutils.DefaultNonAudioPath;
			}
		}

		

		//TODO: add RemoveChannel, RemoveRGBchannel, RemoveChannelGroup, and RemoveTrack procedures



		public string GetMemberName(int SavedIndex)
		{
			string nameOut = "";
			iLORMember4 member = AllMembers.BySavedIndex[SavedIndex];
			if (member != null)
			{
				nameOut = member.Name;
			}
			return nameOut;
		}

		public void Clear(bool areYouReallySureYouWantToDoThis)
		{
			if (areYouReallySureYouWantToDoThis)
			{
				// Zero these out from any previous run
				lineCount = 0;
				this.members = new LORMembership4(this);
				this.AllMembers = new LORMembership4(this);
				//this.Members.SetParent(this);
				this.info = new LORSeqInfo4(this);
				this.animation = new LORAnimation4(this);
				Channels = new List<LORChannel4>();
				RGBchannels = new List<LORRGBChannel4>();
				ChannelGroups = new List<LORChannelGroup4>();
				CosmicDevices = new List<LORCosmic4>();
				Tracks = new List<LORTrack4>();
				TimingGrids = new List<LORTimings4>();
				//Members.SetParentSequence(this);
				myCentiseconds = 0;
				MakeDirty(false);

			} // end Are You Sure
		} // end Clear Sequence

		public string summary()
		{
			string sMsg = "";
			sMsg += "           Filename: " + info.filename + lutils.CRLF + lutils.CRLF;
			sMsg += "              Lines: " + lineCount.ToString() + lutils.CRLF;
			sMsg += "   Regular Channels: " + Channels.Count.ToString() + lutils.CRLF;
			sMsg += "       RGB Channels: " + RGBchannels.Count.ToString() + lutils.CRLF;
			sMsg += "     Channel Groups: " + ChannelGroups.Count.ToString() + lutils.CRLF;
			sMsg += "       Timing Grids: " + TimingGrids.Count.ToString() + lutils.CRLF;
			sMsg += "             Tracks: " + Tracks.Count.ToString() + lutils.CRLF;
			sMsg += "       Centiseconds: " + Centiseconds.ToString() + lutils.CRLF;
			sMsg += "Highest Saved Index: " + AllMembers.HighestSavedIndex.ToString() + lutils.CRLF;
			return sMsg;
		}

		public int[] DuplicateNameCheck()
		{
			// returns null if no matches
			// if matches found, returns array with pairs of matches
			// Thus array size will always be factor of 2
			// with even numbers being a Match to the odd number just after it
			// (or odd numbers being a Match to the even number just before it)
			int[] ret = null;
			Match[] matches = null;
			int q = 0;
			// Do we even have any Channels?  If so...
			if (Channels.Count > 0)
			{
				// resize list to channel count
				Array.Resize(ref matches, Channels.Count);
				// loop thru Channels, collect Name and info
				for (int ch = 0; ch < Channels.Count; ch++)
				{
					matches[ch].Name = Channels[ch].Name;
					matches[ch].savedIdx = Channels[ch].SavedIndex;
					matches[ch].MemberType = LORMemberType4.Channel;
					matches[ch].itemIdx = ch;
				}
				q = Channels.Count;
			} // channel count > 0

			// Any RGB Channels?
			if (RGBchannels.Count > 0)
			{
				// LORLoop4 thru 'em and add their Name and info to the list
				for (int rg = 0; rg < RGBchannels.Count; rg++)
				{
					Array.Resize(ref matches, q + 1);
					matches[q].Name = RGBchannels[rg].Name;
					matches[q].savedIdx = RGBchannels[rg].SavedIndex;
					matches[q].MemberType = LORMemberType4.RGBChannel;
					matches[q].itemIdx = rg;

					q++;
				}
			} // RGB Channel Count > 0

			// Again for channel groups
			if (ChannelGroups.Count > 0)
			{
				for (int gr = 0; gr < ChannelGroups.Count; gr++)
				{
					Array.Resize(ref matches, q + 1);
					matches[q].Name = ChannelGroups[gr].Name;
					matches[q].savedIdx = ChannelGroups[gr].SavedIndex;
					matches[q].MemberType = LORMemberType4.ChannelGroup;
					matches[q].itemIdx = gr;

					q++;
				}
			} // end groups

			// Do we have at least 2 names
			if (q > 1)
			{
				// Sort by Name!
				SortMatches(matches, 0, matches.Length);
				int y = 0;
				// LORLoop4 thru sorted list, comparing each member to the previous one
				for (int ql = 1; ql < q; ql++)
				{
					if (matches[ql].Name == matches[q].Name)
					{
						// If they Match, add 2 elements to the output array
						Array.Resize(ref ret, y + 2);
						// and add their saved indexes
						ret[y] = matches[ql - 1].savedIdx;
						ret[y + 1] = matches[ql].savedIdx;
						y += 2;
					}
				} // end loop thru sorted list
			} // end at least 2 names

			return ret;
		}

		private void SortMatches(Match[] matches, int low, int high)
		{
			int pivot_loc = (low + high) / 2;

			if (low < high)
				pivot_loc = PartitionMatches(matches, low, high);
			SortMatches(matches, low, pivot_loc - 1);
			SortMatches(matches, pivot_loc + 1, high);
		}

		private int PartitionMatches(Match[] matches, int low, int high)
		{
			string pivot = matches[high].Name;
			int i = low - 1;

			for (int j = low; j < high - 1; j++)
			{
				if (matches[j].Name.CompareTo(pivot) <= 0)
				{
					i++;
					SwapMatches(matches, i, j);
				}
			}
			SwapMatches(matches, i + 1, high);
			return i + 1;
		}

		private void SwapMatches(Match[] matches, int idx1, int idx2)
		{
			Match temp = matches[idx1];
			matches[idx1] = matches[idx2];
			matches[idx2] = temp;
			//MakeDirty();

		}

		public void CentiFix()
		{
			int maxCS = 180000;
			int bigCS = 0;
			// Find the largest REASONABLE (less than 30 minutes) time
			if (myCentiseconds < maxCS)
			{
				if (myCentiseconds > bigCS) bigCS = myCentiseconds;
			}
			foreach(LORChannel4 chan in Channels)
			{
				if (chan.Centiseconds < maxCS)
				{
					if (chan.Centiseconds > bigCS) bigCS = chan.Centiseconds;
				}
				foreach (LOREffect4 eff in chan.effects)
				{
					if (eff.endCentisecond < maxCS)
					{
						if (eff.endCentisecond > bigCS) bigCS = eff.endCentisecond;
					}
				}
			}
			foreach(LORTrack4 trk in Tracks)
			{
				if (trk.Centiseconds < maxCS)
				{
					if (trk.Centiseconds > bigCS) bigCS = trk.Centiseconds;
				}
			}

			if (bigCS < lutils.MINCentiseconds)
			{
				string msg = "Probable Error running 'CentiFix'.  The sequence is less than 1 second long!";
				DialogResult dr = MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				CentiFix(bigCS);
			}
		}

		public void CentiFix(int newCentiseconds)
		{
			// Sets the sequence, and all tracks, channels, groups, to the new time (length)
			// And removes any timing marks or effects after that time.
			if (newCentiseconds > lutils.MAXCentiseconds)
			{
				string m = "WARNING!! Setting Centiseconds to more than 60 minutes!  Are you sure?";
				Fyle.WriteLogEntry(m, "Warning");
				if (Fyle.DebugMode)
				{
					System.Diagnostics.Debugger.Break();
				}
			}
			if (newCentiseconds < lutils.MINCentiseconds)
			{
				string m = "WARNING!! Setting Centiseconds to less than 1 second!  Are you sure?";
				Fyle.WriteLogEntry(m, "Warning");
				if (Fyle.DebugMode)
				{
					System.Diagnostics.Debugger.Break();
				}
			}
			myCentiseconds = newCentiseconds;
			for (int i=0; i< Tracks.Count; i++)
			{
				Tracks[i].Centiseconds = newCentiseconds;
			}
			for (int i = 0; i < TimingGrids.Count; i++)
			{
				TimingGrids[i].Centiseconds = newCentiseconds;
				if (TimingGrids[i].LORTimingGridType4 == LORTimingGridType4.Freeform)
				{
					int t = TimingGrids[i].timings.Count -1;
					while (t >= 0)
					{
						if (TimingGrids[i].timings[t] > newCentiseconds)
						{
							TimingGrids[i].timings.RemoveAt(t);
						}
						else
						{
							//t--;
						}
						t--;
					}
				}
			}
			for (int i = 0; i < Channels.Count; i++)
			{
				Channels[i].Centiseconds = newCentiseconds;
				if (Channels[i].effects.Count > 0)
				{
					int e = Channels[i].effects.Count - 1;
					while (e >= 0)
					{
						if (Channels[i].effects[e].startCentisecond >= newCentiseconds)
						{
							Channels[i].effects.RemoveAt(e);
						}
						else
						{
							if (Channels[i].effects[e].endCentisecond > newCentiseconds)
							{
								Channels[i].effects.RemoveAt(e);
							}
							else
							{
								//e--;
							}
						}
						e--;
					}
				}
			}
			for (int i = 0; i < RGBchannels.Count; i++)
			{
				RGBchannels[i].Centiseconds = newCentiseconds;
			}
			for (int i = 0; i < ChannelGroups.Count; i++)
			{
				ChannelGroups[i].Centiseconds = newCentiseconds;
			}
		}


		public int ReadClipboardFile(string existingFilename)
		{
			errorStatus = 0;
			StreamReader reader = new StreamReader(existingFilename);
			string lineIn; // line read in (does not get modified)
			int pos1 = lutils.UNDEFINED; // positions of certain key text in the line

			// Zero these out from any previous run
			Clear(true);

			int curChannel = lutils.UNDEFINED;
			int curSavedIndex = lutils.UNDEFINED;
			int curEffect = lutils.UNDEFINED;
			int curTimingGrid = lutils.UNDEFINED;
			int curGridItem = lutils.UNDEFINED;

			// * PASS 1 - COUNT OBJECTS
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// does this line mark the start of a channel?
				//pos1 = lineIn.IndexOf("xml version=");
				pos1 = lutils.ContainsKey(lineIn, "xml version=");
				if (pos1 > 0)
				{
						info.xmlInfo = lineIn;
				}
				//pos1 = lineIn.IndexOf("saveFileVersion=");
				pos1 = lutils.ContainsKey(lineIn, "saveFileVersion=");
				if (pos1 > 0)
				{
					info.Parse(lineIn);
				}
				//pos1 = lineIn.IndexOf(lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname);
				pos1 = lutils.ContainsKey(lineIn, lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname);
				if (pos1 > 0)
				{
					//channelsCount++;
				}
				//pos1 = lineIn.IndexOf(lutils.STFLD + TABLEeffect + lutils.SPC);
				pos1 = lutils.ContainsKey(lineIn, lutils.STFLD + TABLEeffect + lutils.SPC);
				if (pos1 > 0)
				{
					//effectCount++;
				}
				if (Tracks.Count == 0)
				{
				}
				//pos1 = lineIn.IndexOf(lutils.STFLD + TABLEtimingGrid + lutils.SPC);
				pos1 = lutils.ContainsKey(lineIn, lutils.STFLD + TABLEtimingGrid + lutils.SPC);
				if (pos1 > 0)
				{
					//timingGridCount++;
				}
				//pos1 = lineIn.IndexOf(lutils.STFLD + LORTimings4.TABLEtiming + lutils.SPC);
				pos1 = lutils.ContainsKey(lineIn, lutils.STFLD + LORTimings4.TABLEtiming + lutils.SPC);
				if (pos1 > 0)
				{
					//gridItemCount++;
				}

				//pos1 = lineIn.IndexOf(lutils.FIELDsavedIndex);
				pos1 = lutils.ContainsKey(lineIn, lutils.FIELDsavedIndex);
				if (pos1 > 0)
				{
					curSavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
					//if (curSavedIndex > bySavedIndex.highestSavedIndex)
					{
						//Members.highestSavedIndex = curSavedIndex;
					}
				}
			}

			reader.Close();
			// CREATE ARRAYS TO HOLD OBJECTS
			//Channels = new channel[channelsCount + 2];
			//savedIndexes = new SavedIndex[highestSavedIndex + 3];
			//RGBchannels = new rgbChannel[rgbChannelCount + 2];
			//TimingGrids = new timingGrid[timingGridCount + 2];
			//Tracks = new track[1];
			//int pixNo = 0;
			int chwhich = 0;

			//////////////////////////////////
			// * PASS 2 - COLLECT OBJECTS * //
			//////////////////////////////////
			reader = new StreamReader(existingFilename);
			lineCount = 0;
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// have we reached the Tracks section?
				// does this line mark the start of a regular channel?
				//pos1 = lineIn.IndexOf(lutils.TABLEchannel + lutils.ENDFLD);
				pos1 = lutils.ContainsKey(lineIn, lutils.TABLEchannel + lutils.ENDFLD);
				if (pos1 > 0)
				{
					curChannel++;
					//LORChannel4 chan = new LORChannel4();
					//chan.Name = "ch" + curChannel.ToString("00000");
					if (chwhich == 0)
					{
						//chan.Name += "(R)";
					}
					if (chwhich == 1)
					{
						//chan.Name += "(G)";
					}
					if (chwhich == 2)
					{
						//chan.Name += "(B)";
					}

					//chan.Name += "p" + pixNo.ToString("00000");
					//chan.color = lutils.getKeyValue(lineIn, FIELDcolor);
					//chan.Centiseconds = lutils.getKeyValue(lineIn, FIELDcentiseconds);
					//chan.deviceType = LORSeqEnums4.EnumDevice(lutils.getKeyWord(lineIn, FIELDdeviceType));
					//chan.unit = lutils.getKeyValue(lineIn, FIELDunit);
					//chan.network = lutils.getKeyValue(lineIn, FIELDnetwork);
					//chan.circuit = lutils.getKeyValue(lineIn, FIELDcircuit);
					//chan.SavedIndex = lutils.getKeyValue(lineIn, FIELDsavedIndex);
					//Channels[curChannel] = chan;
					//curSavedIndex = chan.SavedIndex;

					//si.PartType = LORMemberType4.channel;
					//si.objIndex = curChannel;
					//savedIndexes[curSavedIndex] = si;
					if (chwhich == 2)
					{ }
					chwhich++;
					chwhich %= 3;

				}

				// does this line mark the start of an LOREffect4?
				//pos1 = lineIn.IndexOf(TABLEeffect + lutils.FIELDtype);
				pos1 = lutils.ContainsKey(lineIn, TABLEeffect + lutils.FIELDtype);
				if (pos1 > 0)
				{
					curEffect++;

					//DEBUG!
					if (curEffect > 638)
					{
						errorStatus = 1;
					}

					LOREffect4 ef = new LOREffect4();
					ef.EffectType = LORSeqEnums4.EnumEffectType(lutils.getKeyWord(lineIn, lutils.FIELDtype));
					ef.startCentisecond = lutils.getKeyValue(lineIn, lutils.FIELDstartCentisecond);
					ef.endCentisecond = lutils.getKeyValue(lineIn, lutils.FIELDendCentisecond);
					ef.Intensity = lutils.getKeyValue(lineIn, lutils.SPC + LOREffect4.FIELDintensity);
					ef.startIntensity = lutils.getKeyValue(lineIn, LOREffect4.FIELDstartIntensity);
					ef.endIntensity = lutils.getKeyValue(lineIn, LOREffect4.FIELDendIntensity);
					Channels[curChannel].effects.Add(ef);
				}

				// does this line mark the start of a Timing Grid?
				//pos1 = lineIn.IndexOf(lutils.STFLD + TABLEtimingGrid + lutils.SPC);
				pos1 = lutils.ContainsKey(lineIn, lutils.STFLD + TABLEtimingGrid + lutils.SPC);
				if (pos1 > 0)
				{
					curTimingGrid++;
					//LORTimings4 tg = new LORTimings4();
					//tg.Name = lutils.getKeyWord(lineIn, lutils.FIELDname);
					//tg.type = LORSeqEnums4.EnumGridType(lutils.getKeyWord(lineIn, lutils.FIELDtype));
					//tg.SavedIndex = lutils.getKeyValue(lineIn, LORTimings4.FIELDsaveID);
					//tg.spacing = lutils.getKeyValue(lineIn, LORTimings4.FIELDspacing);
					//TimingGrids[curTimingGrid] = tg;

					//if (tg.type == LORTimingGridType4.Freeform)
					{
						lineIn = reader.ReadLine();
						//pos1 = lineIn.IndexOf(LORTimings4.TABLEtiming + lutils.FIELDcentisecond);
						pos1 = lutils.ContainsKey(lineIn, LORTimings4.TABLEtiming + lutils.FIELDcentisecond);
						while (pos1 > 0)
						{
							curGridItem++;
							int gpos = lutils.getKeyValue(lineIn, lutils.FIELDcentisecond);
							TimingGrids[curTimingGrid].AddTiming(gpos);

							lineIn = reader.ReadLine();
							//pos1 = lineIn.IndexOf(LORTimings4.TABLEtiming + lutils.FIELDcentisecond);
							pos1 = lutils.ContainsKey(lineIn, LORTimings4.TABLEtiming + lutils.FIELDcentisecond);
						}
					} // end grid is freeform
				} // end if timingGrid
			} // end while line is valid

			reader.Close();
			Centiseconds = Tracks[0].Centiseconds;

			if (errorStatus <= 0)
			{
				info.filename = existingFilename;
			}

			return errorStatus;
		}

		private int RenameTempFile(string finalFilename)
		{

			if (errorStatus == 0)
			{
				if (File.Exists(finalFilename))
				{
					//string bakFile = newFilename.Substring(0, newFilename.Length - 3) + "bak";
					string bakFile = finalFilename + ".bak";
					if (File.Exists(bakFile))
					{
						File.Delete(bakFile);
					}
					File.Move(finalFilename, bakFile);
				}
				File.Move(tempFileName, finalFilename);

				if (errorStatus <= 0)
				{
					//info.filename = finalFilename;
				}
			}

			return errorStatus;

		}

		public int WriteClipboardFile(string newFilename)
		{
			//TODO: This procedure is totally untested!!

			errorStatus = 0;
			lineCount = 0;

			//backupFile(fileName);

			string tmpFile = newFilename + ".tmp";

			writer = new StreamWriter(tmpFile);
			lineOut = ""; // line to be Written out, gets modified if necessary
										//int pos1 = lutils.UNDEFINED; // positions of certain key text in the line

			int curTimingGrid = 0;
			//int curGridItem = 0;
			//int curTrack = 0;
			//int curTrackItem = 0;
			int[] masterSIs = new int[1];
			//int masterSI = lutils.UNDEFINED;
			updatedTrack[] updatedTracks = new updatedTrack[Tracks.Count];

			lineOut = info.xmlInfo;
			writer.WriteLine(lineOut);
			lineOut = lutils.STFLD + TABLEchannelsClipboard + " version=\"1\" Name=\"" + Path.GetFileNameWithoutExtension(newFilename) + "\"" + lutils.ENDFLD;
			writer.WriteLine(lineOut);

			// Write Timing Grid aka cellDemarcation
			lineOut = lutils.LEVEL1 + lutils.STFLD + TABLEcellDemarcation + lutils.PLURAL + lutils.FINFLD;
			writer.WriteLine(lineOut);
			for (int tm = 0; tm < TimingGrids[0].timings.Count; tm++)
			{
				lineOut = lutils.LEVEL2 + lutils.STFLD + TABLEcellDemarcation;
				lineOut += lutils.FIELDcentisecond + lutils.FIELDEQ + TimingGrids[curTimingGrid].timings[tm].ToString() + lutils.ENDQT;
				lineOut += lutils.ENDFLD;
				writer.WriteLine(lineOut);
			}
			lineOut = lutils.LEVEL1 + lutils.FINTBL + TABLEcellDemarcation + lutils.PLURAL + lutils.FINFLD;
			writer.WriteLine(lineOut);

			// Write JUST CHANNELS in display order
			// DO NOT write track, RGB group, or channel group info
			for (int trk = 0; trk < Tracks.Count; trk++)
			{
				for (int ti = 0; ti < Tracks[trk].Members.Count; ti++)
				{
					int si = Tracks[trk].Members.Items[ti].SavedIndex;
					ParseItemsToClipboard(si);
				} // end for track items loop
			} // end for Tracks loop

			lineOut = lutils.LEVEL1 + lutils.FINTBL + lutils.TABLEchannel + lutils.PLURAL + lutils.FINFLD;
			writer.WriteLine(lineOut);

			lineOut = lutils.FINTBL + TABLEchannelsClipboard + lutils.FINFLD;
			writer.WriteLine(lineOut);

			Console.WriteLine(lineCount.ToString() + " Out:" + lineOut);
			Console.WriteLine("");

			writer.Flush();
			writer.Close();

			if (File.Exists(newFilename))
			{
				string bakFile = newFilename.Substring(0, newFilename.Length - 3) + "bak";
				if (File.Exists(bakFile))
				{
					File.Delete(bakFile);
				}
				File.Move(newFilename, bakFile);
			}
			File.Move(tmpFile, newFilename);

			if (errorStatus <= 0)
			{
				info.filename = newFilename;
			}

			return errorStatus;
		} // end WriteClipboardFile

		private void ParseItemsToClipboard(int saveID)
		{
			int oi = 0; //savedIndexes[saveID].objIndex;
			LORMemberType4 itemType = LORMemberType4.None; //savedIndexes[saveID].PartType;
			if (itemType == LORMemberType4.Channel)
			{
				lineOut = lutils.LEVEL2 + lutils.STFLD + lutils.TABLEchannel + lutils.ENDFLD;
				writer.WriteLine(lineOut);
				//WriteEffects(Channels[oi]);
				lineOut = lutils.LEVEL2 + lutils.FINTBL + lutils.TABLEchannel + lutils.ENDFLD;
				writer.WriteLine(lineOut);
			} // end if channel

			if (itemType == LORMemberType4.RGBChannel)
			{
				LORRGBChannel4 rgbch = RGBchannels[oi];
				// Get and write Red LORChannel4
				//int ci = RGBchannels[oi].redChannelObjIndex;
				lineOut = lutils.LEVEL2 + lutils.STFLD + lutils.TABLEchannel + lutils.ENDFLD;
				writer.WriteLine(lineOut);

				//Channels[ci].LineOut()
				//lineOut = lutils.LEVEL2 + lutils.FINTBL + lutils.TABLEchannel + lutils.ENDFLD;
				writer.WriteLine(lineOut);

				// Get and write Green LORChannel4
				//ci = RGBchannels[oi].grnChannelObjIndex;
				lineOut = lutils.LEVEL2 + lutils.STFLD + lutils.TABLEchannel + lutils.ENDFLD;
				writer.WriteLine(lineOut);
				//writeEffects(Channels[ci]);
				lineOut = lutils.LEVEL2 + lutils.FINTBL + lutils.TABLEchannel + lutils.ENDFLD;
				writer.WriteLine(lineOut);

				// Get and write Blue LORChannel4
				//ci = RGBchannels[oi].bluChannelObjIndex;
				lineOut = lutils.LEVEL2 + lutils.STFLD + lutils.TABLEchannel + lutils.ENDFLD;
				writer.WriteLine(lineOut);
				//writeEffects(Channels[ci]);
				lineOut = lutils.LEVEL2 + lutils.FINTBL + lutils.TABLEchannel + lutils.ENDFLD;
				writer.WriteLine(lineOut);
			} // end if rgbChannel

			if (itemType == LORMemberType4.ChannelGroup)
			{
				LORChannelGroup4 grp = ChannelGroups[oi];
				//for (int itm = 0; itm < grp.itemSavedIndexes.Count; itm++)
				//{
				//	ParseItemsToClipboard(grp.itemSavedIndexes[itm]);
				//}
			} // end if channelGroup
		} // end ParseChannelGroupToClipboard

		private class updatedTrack
		{
			public List<int> newSavedIndexes = new List<int>();
		}

		#region IdentityFindWrappers
		// Wrappers for Members Find Functions

		public LORChannel4 FindChannel(int SavedIndex)
		{
			LORChannel4 ret = null;
			iLORMember4 member = AllMembers.BySavedIndex[SavedIndex];
			if (member != null)
			{
				if (member.MemberType == LORMemberType4.Channel)
				{
					ret = (LORChannel4)member;
				}
			}
			return ret;
		} // end FindChannel

		public LORChannel4 FindChannel(string channelName, bool createIfNotFound = false, bool clearEffects = false)
		{
			LORChannel4 ret = null;
			iLORMember4 member = AllMembers.FindByName(channelName, LORMemberType4.Channel, createIfNotFound);
			if (member != null)
			{
				ret = (LORChannel4)member;
				if (clearEffects)
				{
					ret.effects.Clear();
				}
			}
			return ret;
		}

		public LORChannel4 DEPRECIATED_FindChannel(string channelName, LORMembership4 memberList, bool clearEffects = false, bool createIfNotFound = false)
		{
			// Gets existing channel specified by Name if it already exists in the group
			// Creates it if it does not
			LORChannel4 ret = null;
			iLORMember4 part = null;
			int gidx = 0;
			while ((ret == null) && (gidx < memberList.Count))
			{
				part = memberList.Items[gidx];
				if (part.MemberType == LORMemberType4.Channel)
				{
					if (part.Name == channelName)
					{
						ret = (LORChannel4)part;
						// Clear any existing effects from a previous run
						if (clearEffects)
						{
							if (ret.effects.Count > 0)
							{
								ret.effects = new List<LOREffect4>();
							}
						}
						gidx = memberList.Count; // Loopus Interruptus
					}
				}
				gidx++;
			}

			if (ret == null)
			{
				if (createIfNotFound)
				{
					//int si = Sequence.Members.HighestSavedIndex + 1;
					ret = CreateChannel(channelName);
					ret.Centiseconds = myCentiseconds;
					//Channels.Add(ret);
					memberList.Add(ret);
				}
			}

			return ret;
		}



		public LORRGBChannel4 FindRGBChannel(int SavedIndex)
		{
			LORRGBChannel4 ret = null;
			iLORMember4 member = AllMembers.BySavedIndex[SavedIndex];
			if (member != null)
			{
				if (member.MemberType == LORMemberType4.RGBChannel)
				{
					ret = (LORRGBChannel4)member;
				}
			}
			return ret;
		} // end FindrgbChannel

		public LORRGBChannel4 FindRGBChannel(string rgbChannelName, bool createIfNotFound = false, bool clearEffects = false)
		{
			LORRGBChannel4 ret = null;
			iLORMember4 member = AllMembers.FindByName(rgbChannelName, LORMemberType4.RGBChannel, createIfNotFound);
			if (member!=null)
			{
				if (member.MemberType==LORMemberType4.RGBChannel)
				{
					ret = (LORRGBChannel4)member;
					if (clearEffects)
					{
						ret.redChannel.effects.Clear();
						ret.grnChannel.effects.Clear();
						ret.redChannel.effects.Clear();
					}
				}
			}

			return ret;
		}

		public LORRGBChannel4 DEPRECIATED_FindRGBChannel(string rgbChannelName, LORMembership4 memberList, bool clearEffects = false, bool createIfNotFound = false)
		{
			// Gets existing channel specified by Name if it already exists in the group
			// Creates it if it does not
			LORRGBChannel4 ret = null;
			iLORMember4 part = null;
			int gidx = 0;
			while ((ret == null) && (gidx < memberList.Count))
			{
				part = memberList.Items[gidx];
				if (part.MemberType == LORMemberType4.RGBChannel)
				{
					if (part.Name == rgbChannelName)
					{
						ret = (LORRGBChannel4)part;
						// Clear any existing effects from a previous run
						if (clearEffects)
						{
							ret.redChannel.effects.Clear();
							ret.grnChannel.effects.Clear();
							ret.redChannel.effects.Clear();
						}
						gidx = memberList.Count; // Force exit of loop
					}
				}
				gidx++;
			}

			if (ret == null)
			{
				if (createIfNotFound)
				{
					LORChannel4 redCh = FindChannel(rgbChannelName + " (R)", true);
					redCh.color = lutils.LORCOLOR_RED;
					LORChannel4 grnCh = FindChannel(rgbChannelName + " (G)", true);
					grnCh.color = lutils.LORCOLOR_GRN;
					LORChannel4 bluCh = FindChannel(rgbChannelName + " (B)", true);
					bluCh.color = lutils.LORCOLOR_BLU;
					if (clearEffects)
					{
						redCh.effects.Clear();
						grnCh.effects.Clear();
						redCh.effects.Clear();
					}
					ret = CreateRGBchannel(rgbChannelName);
					ret.redChannel = redCh;
					ret.grnChannel = grnCh;
					ret.bluChannel = bluCh;
					redCh.rgbChild = LORRGBChild4.Red;
					grnCh.rgbChild = LORRGBChild4.Green;
					bluCh.rgbChild = LORRGBChild4.Blue;
					redCh.rgbParent = ret;
					grnCh.rgbParent = ret;
					bluCh.rgbParent = ret;
					//int si = Sequence.Members.HighestSavedIndex + 1;
					ret.Centiseconds = myCentiseconds;
					//Channels.Add(ret);
					memberList.Add(ret);
				}
			}

			return ret;
		}

		public LORChannelGroup4 FindChannelGroup(int SavedIndex)
		{
			LORChannelGroup4 ret = null;
			iLORMember4 member = AllMembers.BySavedIndex[SavedIndex];
			if (member != null)
			{
				if (member.MemberType == LORMemberType4.ChannelGroup)
				{
					ret = (LORChannelGroup4)member;
				}
			}
			return ret;
		} // end FindChannelGroup

		public LORChannelGroup4 DEPRECIATED_FindChannelGroup(string channelGroupName, LORMembership4 memberList, bool createIfNotFound = false)
		{
			// Gets existing group specified by Name if it already exists in the track or group
			// Creates it if it does not
			// Can't use 'Find' functions because we only want to look in this one particular track or group

			// Create blank/null return object
			LORChannelGroup4 ret = null;

			int gidx = 0; // loop counter
										// loop while we still have no group, and we haven't reached to end of the list
			while ((ret == null) && (gidx < memberList.Count))
			{
				// Get each item's ID
				//int SI = Children.Items[gidx].SavedIndex;
				iLORMember4 part = memberList.Items[gidx];
				if (part.MemberType == LORMemberType4.ChannelGroup)
				{
					LORChannelGroup4 group = (LORChannelGroup4)part;
					if (group.Name == channelGroupName)
					{
						// Found it!!
						ret = group;
						gidx = memberList.Count;  // Force exit of loop
					}
				}
				gidx++;
			}


			if (ret == null)
			{
				// Not found, create it?
				if (createIfNotFound)
				{
					ret = CreateChannelGroup(channelGroupName);
					ret.Centiseconds = myCentiseconds;
					//ChannelGroups.Add(ret);
					memberList.Add(ret);
				}
			}

			return ret;

		}


		public LORChannelGroup4 FindChannelGroup(string channelGroupName, bool createIfNotFound = false)
		{
			LORChannelGroup4 ret = null;
			iLORMember4 member = AllMembers.FindByName(channelGroupName, LORMemberType4.ChannelGroup, createIfNotFound);
			if (member != null)
			{
				ret = (LORChannelGroup4)member;
			}
			return ret;
		}

		public LORTrack4 FindTrack(string trackName, bool createIfNotFound = false)
		{
			LORTrack4 ret = null;
			iLORMember4 member = AllMembers.FindByName(trackName, LORMemberType4.Track, createIfNotFound);
			if (member != null)
			{
				ret = (LORTrack4)member;
			}
			return ret;
		}

		// LORSequence4.FindTimingGrid(name, create)
		public LORTimings4 FindTimingGrid(string timingGridName, bool createIfNotFound = false)
		{
#if DEBUG
			string msg = "LORSequence4.FindTimingGrid(" + timingGridName + ", " + createIfNotFound.ToString() + ")";
			Debug.WriteLine(msg);
#endif
			LORTimings4 ret = null;
			//iLORMember4 member = Members.Find(timingGridName, LORMemberType4.Timings, createIfNotFound);
			//iLORMember4 member = null;
			for (int i=0; i< TimingGrids.Count; i++)
			{
				LORTimings4 member = TimingGrids[i];
				if (member.Name == timingGridName)
				{
					ret = member;
					i = TimingGrids.Count; // exit for loop
				}
			}
			if (ret==null)
			{
				if (createIfNotFound)
				{
					ret = CreateTimingGrid(timingGridName);
					//TimingGrids.Add(ret);
				}
			}
			return ret;
		}
		#endregion

		public int SavedIndexIntegrityCheck()
		{
			// Returns 0 if no problems found
			// number > 0 = number of problems
			int errs = 0;



			int tot = Channels.Count + RGBchannels.Count + ChannelGroups.Count;
			LORSavedIndex4[] siCheck = null;
			Array.Resize(ref siCheck, tot);
			for (int t = 0; t < tot; t++)
			{
				siCheck[t] = new LORSavedIndex4();
			}

			for (int ch = 0; ch < Channels.Count; ch++)
			{
				if (Channels[ch].SavedIndex < tot)
				{
					if (siCheck[Channels[ch].SavedIndex].objIndex == lutils.UNDEFINED)
					{
						siCheck[Channels[ch].SavedIndex].objIndex = ch;
					}
					else
					{
						errs++;
					}
					if (siCheck[Channels[ch].SavedIndex].MemberType == LORMemberType4.None)
					{
						siCheck[Channels[ch].SavedIndex].MemberType = LORMemberType4.Channel;
					}
					else
					{
						errs++;
					}
				}
				else
				{
					errs++;
				}
			} // end Channels loop

			for (int rch = 0; rch < RGBchannels.Count; rch++)
			{
				if (RGBchannels[rch].SavedIndex < tot)
				{
					if (siCheck[RGBchannels[rch].SavedIndex].objIndex == lutils.UNDEFINED)
					{
						siCheck[RGBchannels[rch].SavedIndex].objIndex = rch;
					}
					else
					{
						errs++;
					}
					if (siCheck[RGBchannels[rch].SavedIndex].MemberType == LORMemberType4.None)
					{
						siCheck[RGBchannels[rch].SavedIndex].MemberType = LORMemberType4.RGBChannel;
					}
					else
					{
						errs++;
					}
				}
				else
				{
					errs++;
				}
			} // end RGBchannels loop

			for (int chg = 0; chg < ChannelGroups.Count; chg++)
			{
				if (ChannelGroups[chg].SavedIndex < tot)
				{
					if (siCheck[ChannelGroups[chg].SavedIndex].objIndex == lutils.UNDEFINED)
					{
						siCheck[ChannelGroups[chg].SavedIndex].objIndex = chg;
					}
					else
					{
						errs++;
					}
					if (siCheck[ChannelGroups[chg].SavedIndex].MemberType == LORMemberType4.None)
					{
						siCheck[ChannelGroups[chg].SavedIndex].MemberType = LORMemberType4.ChannelGroup;
					}
					else
					{
						errs++;
					}
				}
				else
				{
					errs++;
				}
			} // end Channels loop

			if (tot != AllMembers.HighestSavedIndex + 1)
			{
				errs++;
			}



			return errs;
		}

		public int[] OutputConflicts()
		{
			int[] ret = null;  // holds pairs of matches with identical outputs
			string[] outputs = null;  // holds outputs of each channel in string format (that are not controllerType None)
			int[] indexes = null; // holds SavedIndex of Channels
			int outputCount = 0; // how many Channels (so far) are not controllerType None
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
											 //                                                                   So ret[even#] matches ret[odd#]
											 // LORLoop4 thru all regular Channels
			for (int ch = 0; ch < Channels.Count; ch++)
			{
				// if deviceType != None
				if (Channels[ch].output.deviceType != LORDeviceType4.None)
				{
					// store output info in string format
					Array.Resize(ref outputs, outputCount + 1);
					outputs[outputCount] = Channels[ch].output.ToString();
					// store the SavedIndex of the channel
					Array.Resize(ref indexes, outputCount + 1);
					indexes[outputCount] = Channels[ch].SavedIndex;
					// incremnt count
					outputCount++;
				}
			} // end channel loop
				// if at least 2 Channels deviceType != None
			if (outputCount > 1)
			{
				// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(outputs, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < outputCount; q++)
				{
					// compare output info
					if (outputs[q - 1] == outputs[q])
					{
						// if Match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if outputCount > 1
				// return any matches found
			return ret;
		} // end output conflicts

		public int[] DuplicateChannelNames()
		{
			int[] ret = null;  // holds pairs of matches with identical names
			string[] names = null;  // holds Name of each channel
			int[] indexes = null; // holds SavedIndex of Channels
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
											 //                                                                   So ret[even#] matches ret[odd#]

			if (Channels.Count > 1)
			{
				Array.Resize(ref names, Channels.Count);
				// LORLoop4 thru all regular Channels
				for (int ch = 0; ch < Channels.Count; ch++)
				{
					names[ch] = Channels[ch].Name;
					indexes[ch] = Channels[ch].SavedIndex;
				} // end channel loop
					// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < Channels.Count; q++)
				{
					// compare output info
					if (names[q - 1] == names[q])
					{
						// if Match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if outputCount > 1
				// return any matches found
			return ret;
		} // end DuplicateChannelNames function

		public int[] DuplicateChannelGroupNames()
		{
			int[] ret = null;  // holds pairs of matches with identical names
			string[] names = null;  // holds Name of each channelGroup
			int[] indexes = null; // holds SavedIndex of ChannelGroups
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
											 //                                                                   So ret[even#] matches ret[odd#]

			if (ChannelGroups.Count > 1)
			{
				Array.Resize(ref names, ChannelGroups.Count);
				// LORLoop4 thru all regular Channels
				for (int chg = 0; chg < ChannelGroups.Count; chg++)
				{
					names[chg] = ChannelGroups[chg].Name;
					indexes[chg] = ChannelGroups[chg].SavedIndex;
				} // end channelGroup loop
					// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < ChannelGroups.Count; q++)
				{
					// compare output info
					if (names[q - 1] == names[q])
					{
						// if Match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if outputCount > 1
				// return any matches found
			return ret;
		} // end Duplicate Channel Group Names

		public int[] DuplicateRGBchannelNames()
		{
			int[] ret = null;  // holds pairs of matches with identical names
			string[] names = null;  // holds Name of each rgbChannel
			int[] indexes = null; // holds SavedIndex of RGBchannels
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
											 //                                                                   So ret[even#] matches ret[odd#]

			if (RGBchannels.Count > 1)
			{
				Array.Resize(ref names, RGBchannels.Count);
				// LORLoop4 thru all regular Channels
				for (int rch = 0; rch < RGBchannels.Count; rch++)
				{
					names[rch] = RGBchannels[rch].Name;
					indexes[rch] = RGBchannels[rch].SavedIndex;
				} // end channel loop
					// if at least 2 Channels deviceType != None
					// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < RGBchannels.Count; q++)
				{
					// compare output info
					if (names[q - 1] == names[q])
					{
						// if Match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if outputCount > 1
				// return any matches found
			return ret;
		} // end Duplicate RGB Channel Names

		public int[] DuplicateTrackNames()
		{
			int[] ret = null;  // holds pairs of matches with identical names
			string[] names = null;  // holds Name of each track
			int[] indexes = null; // holds ObjectIndex of track
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
											 //                                                                   So ret[even#] matches ret[odd#]

			if (Tracks.Count > 1)
			{
				Array.Resize(ref names, Tracks.Count);
				// LORLoop4 thru all regular Channels
				for (int tr = 0; tr < Tracks.Count; tr++)
				{
					names[tr] = Tracks[tr].Name;
					indexes[tr] = tr;
				} // end channel loop
					// if at least 2 Channels deviceType != None
					// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < Tracks.Count; q++)
				{
					// compare output info
					if (names[q - 1] == names[q])
					{
						// if Match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if outputCount > 1
				// return any matches found
			return ret;
		} // end DuplicateTrackNames function

		public int[] DuplicateNames()
		{
			// Looks for duplicate names amongst regular Channels, RGB Channels, channel groups, and Tracks
			// Does not include timing grid names
			int[] ret = null;  // holds pairs of matches with identical names
			string[] names = null;  // holds Name of each channel
			int nameCount = 0;
			int[] indexes = null; // holds SavedIndex of Channels
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
											 //                                                                   So ret[even#] matches ret[odd#]

			nameCount = Channels.Count + RGBchannels.Count + ChannelGroups.Count + Tracks.Count;
			if (nameCount > 1)
			{
				Array.Resize(ref names, nameCount);
				// LORLoop4 thru all regular Channels
				for (int ch = 0; ch < Channels.Count; ch++)
				{
					names[ch] = Channels[ch].Name;
					indexes[ch] = Channels[ch].SavedIndex;
				} // end channel loop

				for (int rch = 0; rch < RGBchannels.Count; rch++)
				{
					names[rch + Channels.Count] = RGBchannels[rch].Name;
					indexes[rch + Channels.Count] = RGBchannels[rch].SavedIndex;
				} // end RGB channel loop

				for (int chg = 0; chg < ChannelGroups.Count; chg++)
				{
					names[chg + Channels.Count + RGBchannels.Count] = ChannelGroups[chg].Name;
					indexes[chg + Channels.Count + RGBchannels.Count] = ChannelGroups[chg].SavedIndex;
				} // end Channel Group LORLoop4

				int trIdx;
				for (int tr = 0; tr < Tracks.Count; tr++)
				{
					names[tr + Channels.Count + RGBchannels.Count + ChannelGroups.Count] = Tracks[tr].Name;
					// use negative numbers for track indexes
					trIdx = lutils.UNDEFINED + (-tr);
					indexes[tr + Channels.Count + RGBchannels.Count + ChannelGroups.Count] = trIdx;
				}

				// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < Channels.Count; q++)
				{
					// compare output info
					if (names[q - 1] == names[q])
					{
						// if Match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if nameCount > 1
				// return any matches found
			return ret;
		} // end DuplicateNames function

		private int BTreeFindName(string[] names, string Name)
		{
			// looks for an EXACT Match (see also: FuzzyFindName)
			// names[] must be sorted!

			//TODO Test this THOROUGHLY!

			int ret = lutils.UNDEFINED;
			int bot = 0;
			int top = Tracks.Count - 1;
			int mid = ((top - bot) / 2) + bot;

			while (top > bot)
			{
				mid = ((top - bot) / 2) + bot;

				if (names[mid].CompareTo(Name) < 0)
				{
					mid = top;
				}
				else
				{
					if (names[mid].CompareTo(Name) > 0)
					{
						mid = bot;
					}
					else
					{
						return mid;
					}
				}
			}
			return ret;
		} // end bTreeFindName

		public void ClearAllEffects()
		{
			foreach (LORChannel4 ch in Channels)
			{
				ch.effects = new List<LOREffect4>();
			}
			MakeDirty(true);
		}

		public int CopyEffects(LORChannel4 SourceChan, LORChannel4 DestChan, bool Merge)
		{
			// Could be a static method, but easier to work with if not
			int copiedCount = 0;
			if (!Merge)
			{
				DestChan.effects = new List<LOREffect4>();
			}
			copiedCount = DestChan.CopyEffects(SourceChan.effects, Merge);
			MakeDirty(true);

			return copiedCount;
		}

		public LORChannel4 ParseChannel(string lineIn)
		{
			LORChannel4 chan = new LORChannel4(lineIn);
			chan.SetParent(this);
			chan.SetIndex(Channels.Count);
			Channels.Add(chan);
			//chan.Parse(lineIn);
			AllMembers.Add(chan);
			myCentiseconds = Math.Max(myCentiseconds, chan.Centiseconds);
			return chan;
		}

		public LORChannel4 CreateChannel(string theName)
		{
			// Does NOT check to see if a channel with this name already exists
			// Therefore, allows for duplicate channel names (But they will have different SavedIndexes)
			LORChannel4 chan;
			chan = new LORChannel4(theName);
			int newSI = AssignNextSavedIndex(chan);
			chan.SetParent(this);
			chan.Centiseconds = myCentiseconds;
			chan.SetIndex(Channels.Count);
			Channels.Add(chan);
			AllMembers.Add(chan);
			//myCentiseconds = Math.Max(myCentiseconds, chan.Centiseconds);
			return chan;
		}

		public LORRGBChannel4 ParseRGBchannel(string lineIn)
		{
			LORRGBChannel4 rch = new LORRGBChannel4("");
			rch.SetParent(this);
			rch.SetIndex(RGBchannels.Count);
			RGBchannels.Add(rch);
			rch.Parse(lineIn);
			AllMembers.Add(rch);
			myCentiseconds = Math.Max(myCentiseconds, rch.Centiseconds);
			return rch;
		}

		public LORRGBChannel4 CreateRGBchannel(string theName)
		{
			// Does NOT check to see if a channel with this name already exists
			// Therefore, allows for duplicate channel names (But they will have different SavedIndexes)
			LORRGBChannel4 rch;
			rch = new LORRGBChannel4(theName, lutils.UNDEFINED);
			int newSI = AssignNextSavedIndex(rch);
			rch.SetParent(this);
			rch.Centiseconds = myCentiseconds;
			rch.SetIndex(RGBchannels.Count);
			RGBchannels.Add(rch);
			AllMembers.Add(rch);
			//myCentiseconds = Math.Max(myCentiseconds, rch.Centiseconds);
			return rch;
		}

		public LORChannelGroup4 ParseChannelGroup(string lineIn)
		{
			LORChannelGroup4 chg = new LORChannelGroup4("");
			chg.SetParent(this);
			//chg.Members.SetParent(this);
			chg.SetIndex(ChannelGroups.Count);
			chg.Parse(lineIn);
			ChannelGroups.Add(chg);
			AllMembers.Add(chg);
			myCentiseconds = Math.Max(myCentiseconds, chg.Centiseconds);
			return chg;
		}

		public LORCosmic4 ParseCosmicDevice(string lineIn)
		{
			LORCosmic4 cos = new LORCosmic4("");
			cos.SetParent(this);
			//cos.Members.SetParent(this);
			cos.SetIndex(CosmicDevices.Count);
			cos.Parse(lineIn);
			CosmicDevices.Add(cos);
			AllMembers.Add(cos);
			myCentiseconds = Math.Max(myCentiseconds, cos.Centiseconds);
			return cos;
		}

		public LORChannelGroup4 CreateChannelGroup(string theName)
		{
			// Does NOT check to see if a group with this name already exists
			// Therefore, allows for duplicate group names (But they will have different SavedIndexes)
			LORChannelGroup4 chg;
			chg = new LORChannelGroup4(theName, lutils.UNDEFINED);
			int newSI = AssignNextSavedIndex(chg);
			chg.SetParent(this);
			//chg.Members.SetParent(this);
			//chg.Members.Owner = chg;
			chg.Centiseconds = myCentiseconds;
			chg.SetIndex(ChannelGroups.Count);
			ChannelGroups.Add(chg);
			AllMembers.Add(chg);
			//myCentiseconds = Math.Max(myCentiseconds, chg.Centiseconds);

			return chg;
		}

		public LORCosmic4 CreateCosmicDevice(string theName)
		{
			// Does NOT check to see if a device with this name already exists
			// Therefore, allows for duplicate device names (But they will have different SavedIndexes)
			LORCosmic4 dev;
			dev = new LORCosmic4(theName, lutils.UNDEFINED);
			int newSI = AssignNextSavedIndex(dev);
			dev.SetParent(this);
			//dev.Members.SetParent(this);
			//dev.Members.Owner = dev;
			dev.Centiseconds = myCentiseconds;
			dev.SetIndex(CosmicDevices.Count);
			CosmicDevices.Add(dev);
			AllMembers.Add(dev);
			//myCentiseconds = Math.Max(myCentiseconds, dev.Centiseconds);

			return dev;
		}

		public LORTrack4 ParseTrack(string lineIn)
		{
			LORTrack4 tr = new LORTrack4("");
			tr.SetParent(this);
			//tr.Members.SetParent(this);
			tr.SetIndex(Tracks.Count);
			Tracks.Add(tr);
			tr.Parse(lineIn);
			if (myAltSavedIndex > lutils.UNDEFINED)
			{
				// AltSavedIndex has been temporarily set to the track's LORTimings4's SaveID
				// So get the specified LORTimings4 by it's SaveID
				//iLORMember4 tg = Members.bySaveID[tr.AltSavedIndex];
				LORTimings4 tg = null;
				for (int i =0; i< TimingGrids.Count; i++)
				{
					if (TimingGrids[i].SaveID == tr.AltSavedIndex)
					{
						tg = TimingGrids[i];
						i = TimingGrids.Count; // force exit of loop
					}
				}
				if (tg == null)
				{
					string msg ="Timing Grid with SaveID " + tr.AltSavedIndex.ToString() + " not found!";
					System.Diagnostics.Debugger.Break();

				}
				// And assign it to the track
				tr.timingGrid = (LORTimings4)tg;
				// Clear the AltSavedIndex which was temporarily holding the SaveID of the LORTimings4
				tr.AltSavedIndex = lutils.UNDEFINED;
			}
			myCentiseconds = Math.Max(myCentiseconds, tr.Centiseconds);
			AllMembers.Add(tr);
			members.Add(tr);
			return tr;
		}

		public LORTrack4 CreateTrack(string theName)
		{
			// Does NOT check to see if a track with this name already exists
			// Therefore, allows for duplicate track names (But they will have different track numbers)
			LORTrack4 tr = new LORTrack4(theName);
			tr.SetParent(this);
			//tr.Members.SetParent(this);
			//tr.Members.Owner = tr;
			tr.Centiseconds = myCentiseconds;
			tr.SetIndex(Tracks.Count);
			Tracks.Add(tr);
			//tr.TrackNumber = Tracks.Count;
			if (TimingGrids.Count > 0)
			{
				//! Is this gonna cause a problem later?
				tr.timingGrid = TimingGrids[0];
			}
			myCentiseconds = Math.Max(myCentiseconds, tr.Centiseconds);
			AllMembers.Add(tr);
			members.Add(tr);
			return tr;
		}

		public LORTimings4 ParseTimingGrid(string lineIn)
		{
			LORTimings4 tg = new LORTimings4("");
			tg.SetParent(this);
			//int newSI = AssignNextSaveID(tg);
			//tg.SaveID = newSI;
			tg.SetIndex(TimingGrids.Count);
			TimingGrids.Add(tg);
			tg.Parse(lineIn);
			//Members.Add(tg);
			return tg;
		}

		// LORSequence4.CreateTimingGrid(name)
		public LORTimings4 CreateTimingGrid(string theName)
		{
#if DEBUG
			string msg = "LORSequence4.CreateTimingGrid(" + theName + ")";
			Debug.WriteLine(msg);
#endif
			// Does NOT check to see if a grid with this name already exists
			// Therefore, allows for duplicate grid names (But they will have different SaveIDs)
			LORTimings4 tg = new LORTimings4(theName);
			tg.SetParent(this);
			int newSI = AssignNextSaveID(tg);
			tg.Centiseconds = myCentiseconds;
			tg.SetIndex(TimingGrids.Count);
			TimingGrids.Add(tg); // Handled in Members.Add called from AssignNextSaveID
			//Members.Add(tg);
			return tg;
		}

		public override void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				info.lastModified = DateTime.Now; //.ToString("MM/dd/yyyy hh:mm:ss tt");
			}
			isDirty = dirtyState;
		}

		private int AssignNextSavedIndex(iLORMember4 thePart)
		{
			if (thePart.SavedIndex < 0)
			{
				int newSI = AllMembers.HighestSavedIndex + 1;
				thePart.SetSavedIndex(newSI);
				AllMembers.Add(thePart);
			}
			return thePart.SavedIndex;
		}

		private int AssignNextAltSavedIndex(iLORMember4 thePart)
		{
			if (thePart.AltSavedIndex < 0)
			{
				int newASI = AllMembers.AltHighestSavedIndex + 1;
				thePart.AltSavedIndex = newASI;
				// May cause out of bounds exception, might need to add instead
				AllMembers.ByAltSavedIndex[thePart.AltSavedIndex] = thePart;
				AllMembers.AltHighestSavedIndex = newASI;
			}
			return thePart.AltSavedIndex;
		}

		private int AssignNextSaveID(LORTimings4 theGrid)
		{
			if (theGrid.SaveID < 0)
			{
				int newSI = AllMembers.HighestSaveID + 1;
				theGrid.SaveID = newSI;
				AllMembers.Add(theGrid);
			}
			return theGrid.SaveID;
		}

		private int AssignNextAltSaveID(LORTimings4 theGrid)
		{
			if (theGrid.AltSavedIndex < 0)
			{
				int newASI = AllMembers.AltHighestSaveID + 1;
				theGrid.AltSaveID = newASI;
				AllMembers.ByAltSaveID.Add(theGrid);
				AllMembers.AltHighestSaveID = newASI;
			}
			return theGrid.AltSaveID;
		}

		public override iLORMember4 Clone()
		{
			LORSequence4 seqOut = (LORSequence4)Clone();
			seqOut.animation = animation.Clone();
			seqOut.animation.parentSequence = seqOut;
			seqOut.filename = filename;
			seqOut.lineCount = lineCount;
			seqOut.info = info.Clone();
			seqOut.LORSequenceType4 = LORSequenceType4;
			seqOut.videoUsage = videoUsage;
			for (int idx = 0; idx < AllMembers.BySavedIndex.Count; idx++)
			{
				LORMemberType4 mt = AllMembers.BySavedIndex[idx].MemberType;
				if (mt == LORMemberType4.Channel)
				{
					LORChannel4 newCh = (LORChannel4)AllMembers.BySavedIndex[idx].Clone();
					seqOut.AllMembers.Add(newCh);
				}
			}
			return seqOut;
		}

		public override iLORMember4 Clone(string newName)
		{
			LORSequence4 seqOut = (LORSequence4)this.Clone();
			ChangeName(newName);
			return seqOut;
		}

		public void MoveTrackByNumber(int oldTrackNumber, int newTrackNumber)
		{
			MoveTrackByIndex(oldTrackNumber - 1, newTrackNumber - 1);
		}
		public void MoveTrackByIndex(int oldIndex, int newIndex)
		{
			// IMPORTANT NOTE:
			// POSITIONS ARE THE INDEX (ZERO TO COUNT-1)
			// THEY ARE **NOT** THE TRACK NUMBER (ONE TO COUNT) !!
			string info = "";
			
			// Sanity Checks
			if ((oldIndex >= 0) &&
				(oldIndex < Tracks.Count) &&
				(newIndex >= 0) &&
				(newIndex <= Tracks.Count) &&
				(newIndex != oldIndex))
			{
				List<LORTrack4> tracksNew = new List<LORTrack4>();
				int newSpot = 0;
				for (int i=0; i< Tracks.Count; i++)
				{
					if (i == newIndex)
					{
						info = Tracks[oldIndex].Name + " shifted from " + oldIndex.ToString() + " to " + newSpot.ToString();
						Debug.WriteLine(info);
						tracksNew.Add(Tracks[oldIndex]);
						Tracks[oldIndex].SetIndex(newSpot);
					}
					else
					{
						if (i != oldIndex)
						{
							info = Tracks[i].Name + " shifted from " + i.ToString() + " to " + newSpot.ToString();
							Debug.WriteLine(info);
							tracksNew.Add(Tracks[i]);
							Tracks[i].SetIndex(newSpot);
						}
					}
					newSpot++;
				}
				Tracks = tracksNew;
			}
		} // End MoveTrack

		public int LongestMember
		{
			get
			{
				int longest = 0;
				for (int i=0; i< AllMembers.Count; i++)
				{
					if (AllMembers[i].Centiseconds > longest)
					{
						longest = AllMembers[i].Centiseconds;
					}
					if (longest > lutils.MAXCentiseconds)
					{
						string m = "WARNING!  Member " + AllMembers[i].Name + " is over 60 minutes!";
						Fyle.WriteLogEntry(m, "Warning");
						if (Debugger.IsAttached)
						{
							System.Diagnostics.Debugger.Break();
						}
					}
					if (longest > myCentiseconds)
					{
						string m = "ERROR!  Member " + AllMembers[i].Name + " is longer than the sequence!";
						Fyle.WriteLogEntry(m, "Error");
						if (Debugger.IsAttached)
						{
							System.Diagnostics.Debugger.Break();
						}
					}
				}
				return longest;
			}
		}

		public int LastEffect
		{
			get
			{
				int last = 0;
				for (int i=0; i< Channels.Count; i++)
				{
					if (Channels[i].effects.Count > 0)
					{
						for (int e=0; e< Channels[i].effects.Count; e++)
						{
							if (Channels[i].effects[e].endCentisecond > last)
							{
								last = Channels[i].effects[e].endCentisecond;
								if (last > lutils.MAXCentiseconds)
								{
									string m = "WARNING!  Last LOREffect4 on Channel " + Channels[i].Name + " is past 60 minutes!";
									Fyle.WriteLogEntry(m, "Warning");
									if (Debugger.IsAttached)
									{
										System.Diagnostics.Debugger.Break();
									}
								}
								if (last > myCentiseconds)
								{
									string m = "ERROR! Last effect on Channel " + Channels[i].Name + " is past the end of the sequence!";
									Fyle.WriteLogEntry(m, "Error");
									if (Debugger.IsAttached)
									{
										System.Diagnostics.Debugger.Break();
									}
								}
							}
						}
					}
				}
				return last;
			}
		} // End Last LOREffect4

		public int LastFreeformTiming
		{
			//! Important Note: Returns the last FREEFORM timing
			get
			{
				int last = 0;
				for (int i = 0; i < TimingGrids.Count; i++)
				{
					if (TimingGrids[i].LORTimingGridType4 == LORTimingGridType4.Freeform)
					{
						if (TimingGrids[i].timings.Count > 0)
						{
							for (int e = 0; e < TimingGrids[i].timings.Count; e++)
							{
								if (TimingGrids[i].timings[e] > last)
								{
									last = TimingGrids[i].timings[e];
									if (last > lutils.MAXCentiseconds)
									{
										string m = "WARNING!  Last Timing in Grid " + TimingGrids[i].Name + " is past 60 minutes!";
										Fyle.WriteLogEntry(m, "Warning");
										if (Debugger.IsAttached)
										{
											System.Diagnostics.Debugger.Break();
										}
									}
									if (last > myCentiseconds)
									{
										string m = "ERROR! Last Timing in Grid " + TimingGrids[i].Name + " is past the end of the sequence!";
										Fyle.WriteLogEntry(m, "Error");
										if (Debugger.IsAttached)
										{
											System.Diagnostics.Debugger.Break();
										}
									}
								}
							}
						}
					}
				}
				return last;
			}
		} // End Last Timing

		public override int color
		{
			get { return lutils.LORCOLOR_MULTI; }
			set { int ignore = value; }
		}

		public override System.Drawing.Color Color
		{
			get { return lutils.Color_LORtoNet(this.color); }
			set { System.Drawing.Color ignore = value; }
		}


		// END SEQUENCE CLASS
	} // end sequence class
} // end namespace LORUtils4