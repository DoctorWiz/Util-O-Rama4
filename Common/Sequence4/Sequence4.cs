using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LORUtils
{
	public partial class Sequence4 : IMember, IComparable<IMember>, IDisposable
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

		private const string STARTsequence = utils.STFLD + TABLEsequence + Info.FIELDsaveFileVersion;
		private const string STARTconfig = utils.STFLD + TABLEchannelConfig + Info.FIELDchannelConfigFileVersion;
		private const string STARTeffect = utils.STFLD + TABLEeffect + utils.FIELDtype;
		public const string STARTchannel = utils.STFLD + utils.TABLEchannel + utils.FIELDname;
		private const string STARTcosmic = utils.STFLD + TABLEcosmicDevice + utils.SPC;
		public const string STARTrgbChannel = utils.STFLD + TABLErgbChannel + utils.SPC;
		public const string STARTchannelGroup = utils.STFLD + TABLEchannelGroupList + utils.SPC;
		private const string STARTtrack = utils.STFLD + TABLEtrack + utils.SPC;
		private const string STARTtrackItem = utils.STFLD + utils.TABLEchannel + utils.FIELDsavedIndex;
		private const string STARTtimingGrid = utils.STFLD + TABLEtimingGrid + utils.SPC;
		private const string STARTtiming = utils.STFLD + TimingGrid.TABLEtiming + utils.SPC;
		private const string STARTgridItem = TimingGrid.TABLEtiming + utils.FIELDcentisecond;
		private const string STARTloopLevel = utils.STFLD + TABLEloopLevel + utils.FINFLD;
		private const string STARTloop = utils.STFLD + Loop.FIELDloop + utils.SPC;
		private const string STARTaniRow = utils.STFLD + AnimationRow.FIELDrow + utils.SPC + AnimationRow.FIELDindex;
		private const string STARTaniCol = utils.STFLD + AnimationColumn.FIELDcolumnIndex;


		#endregion

		public const int ERROR_Undefined = utils.UNDEFINED;
		public const int ERROR_NONE = 0;
		public const int ERROR_CantOpen = 101;
		public const int ERROR_NotXML = 102;
		public const int ERROR_NotSequence = 103;
		public const int ERROR_EncryptedDemo = 104;
		public const int ERROR_Compressed = 105;
		public const int ERROR_UnsupportedVersion = 114;
		public const int ERROR_UnexpectedData = 50;

		//public SequenceType sequenceType = SequenceType.Undefined;
		public Membership Members;
		public List<Channel> Channels = new List<Channel>();
		public List<RGBchannel> RGBchannels = new List<RGBchannel>();
		public List<ChannelGroup> ChannelGroups = new List<ChannelGroup>();
		public List<CosmicDevice> CosmicDevices = new List<CosmicDevice>();
		public List<TimingGrid> TimingGrids = new List<TimingGrid>();
		public List<Track> Tracks = new List<Track>();
		public Animation animation = null;
		public Info info = null;
		public int videoUsage = 0;
		public int errorStatus = 0;
		public int lineCount = 0;
		public bool dirty = false;
		private object tag = null;
		private IMember mappedTo = null;

		// For now at least, this will remain false, therefore ALL timing grids will ALWAYS get written
		private bool WriteSelectedGridsOnly = false;

		//private string myName = "$_UNNAMED_$";
		private int myCentiseconds = utils.UNDEFINED;
		private int mySavedIndex = utils.UNDEFINED;
		private int myAltSavedIndex = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;
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
			public LORUtils.MemberType MemberType;
			public int itemIdx;
		}



		#region IMember Interface
		public string Name
		{
			get
			{
				//return myName;
				return info.filename;
			}
		}

		public void ChangeName(string newName)
		{
			//myName = newName;
			info.filename = newName;
			MakeDirty();
		}

		public int Centiseconds
		{
			get
			{
				return myCentiseconds;
			}
			set
			{
				if (value > myCentiseconds)
				{
					myCentiseconds = value;
					MakeDirty();
					// LOR allows different tracks to have different times
					// (which doesn't make much sense to me)
					// So just make sure Sequence.Centiseconds is long enough to hold longest track
					// To change (fix) the number of centiseconds for everthing to a consistant length, use SetTotalTime() function below
				}
			}
		}

		public void SetTotalTime(int newCentiseconds)
		{
			myCentiseconds = newCentiseconds;
			for (int t = 0; t < Tracks.Count; t++)
			{
				Tracks[t].Centiseconds = newCentiseconds;
			}
			MakeDirty();

		}

		public int Index
		{
			get
			{
				return utils.UNDEFINED;
			}
		}

		public void SetIndex(int ignored)
		{ }

		public int SavedIndex
		{
			get
			{
				return utils.UNDEFINED;
			}
		}

		public void SetSavedIndex(int ignored)
		{ }

		public int AltSavedIndex
		{
			get
			{
				return utils.UNDEFINED;
			}
			set
			{ }
		}

		public Sequence4 ParentSequence
		{
			get
			{
				return this;
			}
		}

		public void SetParentSeq(Sequence4 newParentSeq)
		{
			// Ignore
		}

		public bool Selected
		{
			get
			{
				return imSelected;
			}
			set
			{
				imSelected = value;
			}
		}

		public MemberType MemberType
		{
			get
			{
				return MemberType.Sequence;
			}
		}

		public int CompareTo(IMember other)
		{
			int result = 0;
			if (parentSequence.Members.sortMode == Membership.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Members.sortMode == Membership.SORTbyName)
				{
					//if (myName == "")

					//{
					//	myName = Path.GetFileName(info.filename);
					//}
					result = Name.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Members.sortMode == Membership.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
				}
			}
			return result;
		}

		public string LineOut()
		{
			string ret = "";
			// Implemented primarily for compatibility with 'IMember' interface
			//TODO: make this return something, say maybe the first few lines of the file...?
			return ret;
		}

		public override string ToString()
		{
			return Name;
		}

		public void Parse(string lineIn)
		{
			ReadSequenceFile(lineIn);
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > utils.UNDEFINED) r = true;
				return r;
			}
		}

		public object Tag
		{
			get
			{
				return tag;
			}
			set
			{
				tag = value;
			}
		}

		public IMember MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == MemberType.Sequence)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
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
		public Sequence4()
		{
			//! DEFAULT CONSTRUCTOR
			this.Members = new Membership(this);
			this.Members.SetParentSequence(this);
			this.info = new Info(this);
			this.animation = new Animation(this);
			this.info.sequenceType = SequenceType.Animated;
			//Members.SetParentSequence(this);
		}

		public Sequence4(string theFilename)
		{
			this.Members = new Membership(this);
			this.Members.SetParentSequence(this);
			this.info = new Info(this);
			this.animation = new Animation(this);
			//Members.SetParentSequence(this);
			ReadSequenceFile(theFilename);
		}
		#endregion

		public SequenceType SequenceType
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
			int li = utils.UNDEFINED; // positions of certain key text in the line
																//Track trk = new Track();
			SequenceType st = SequenceType.Undefined;
			string creation = "";
			DateTime modification;

			Channel lastChannel = null;
			RGBchannel lastRGBchannel = null;
			ChannelGroup lastGroup = null;
			CosmicDevice lastCosmic = null;
			Track lastTrack = null;
			TimingGrid lastGrid = null;
			LoopLevel lastll = null;
			AnimationRow lastAniRow = null;
			Membership lastMembership = null;

			string ext = Path.GetExtension(existingFileName).ToLower();
			if (ext == ".lms") st = SequenceType.Musical;
			if (ext == ".las") st = SequenceType.Animated;
			if (ext == ".lcc") st = SequenceType.ChannelConfig;
			SequenceType = st;

			Clear(true);

			info.file_accessed = File.GetLastAccessTime(existingFileName);
			info.file_created = File.GetCreationTime(existingFileName);
			info.file_saved = File.GetLastWriteTime(existingFileName);

			const string ERRproc = " in Sequence4:ReadSequenceFile(";
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
							li = utils.ContainsKey(lineIn, " Demo ");
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
							if ((st == SequenceType.Musical) || (st == SequenceType.Animated))
							{
								//li = lineIn.IndexOf(STARTsequence);
								li = utils.ContainsKey(lineIn, STARTsequence);
							}
							if (st == SequenceType.ChannelConfig)
							{
								//li = lineIn.IndexOf(STARTconfig);
								li = utils.ContainsKey(lineIn, STARTconfig);
							}
							if (li != 0)
							{
								errorStatus = ERROR_NotSequence;
								info.infoLine = lineIn;
							}
							else
							{
								info = new Info(this, lineIn);
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
											li = utils.UNDEFINED;
										}
										else
										{
											//li = lineIn.IndexOf(STARTeffect);
											li = utils.ContainsKey(lineIn, STARTeffect);
										}
										if (li > 0)
										{
		///////////////////
		///   EFFECT   ///
		/////////////////
												#region Effect
												while (li > 0)
											{
												lastChannel.AddEffect(lineIn);

												lineIn = reader.ReadLine();
												lineCount++;
												//li = lineIn.IndexOf(STARTeffect);
												li = utils.ContainsKey(lineIn, STARTeffect);
											}
												#endregion // Effect
											}
											else // Not an Effect
										{
											//! Timings
											//li = lineIn.IndexOf(STARTtiming);
											li = utils.ContainsKey(lineIn, STARTtiming);
											if (li > 0)
											{
		//////////////////
		///  TIMING   ///
		////////////////
													#region Timing
													int t = utils.getKeyValue(lineIn, utils.FIELDcentiseconds);
												lastGrid.AddTiming(t);
													#endregion // Timing
												}
												else // Not a regular channel
											{
												//! Regular Channels
												//li = lineIn.IndexOf(STARTchannel);
												li = utils.ContainsKey(lineIn, STARTchannel);
												if (li > 0)
												{
		////////////////////////////
		///   REGULAR CHANNEL   ///
		//////////////////////////
														#region Regular Channel
														lastChannel = ParseChannel(lineIn);
														#endregion // Regular Channel
													}
													else // Not a regular channel
												{
													//! RGB Channels
													//li = lineIn.IndexOf(STARTrgbChannel);
													li = utils.ContainsKey(lineIn, STARTrgbChannel);
													if (li > 0)
													{
		////////////////////////
		///   RGB CHANNEL   ///
		//////////////////////
															#region RGB Channel
															lastRGBchannel = ParseRGBchannel(lineIn);
														lineIn = reader.ReadLine();
														lineCount++;
														lineIn = reader.ReadLine();
														lineCount++;

														// RED
														int csi = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
														Channel ch = (Channel)Members.bySavedIndex[csi];
														lastRGBchannel.redChannel = ch;
														ch.rgbChild = RGBchild.Red;
														ch.rgbParent = lastRGBchannel;
														lineIn = reader.ReadLine();
														lineCount++;

														// GREEN
														csi = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
														ch = (Channel)Members.bySavedIndex[csi];
														lastRGBchannel.grnChannel = ch;
														ch.rgbChild = RGBchild.Green;
														ch.rgbParent = lastRGBchannel;
														lineIn = reader.ReadLine();
														lineCount++;

														// BLUE
														csi = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
														ch = (Channel)Members.bySavedIndex[csi];
														lastRGBchannel.bluChannel = ch;
														ch.rgbChild = RGBchild.Blue;
														ch.rgbParent = lastRGBchannel;
															#endregion // RGB Channel
														}
														else  // Not an RGB Channel
													{
														//! Channel Groups
														//li = lineIn.IndexOf(STARTchannelGroup);
														li = utils.ContainsKey(lineIn, STARTchannelGroup);
														if (li > 0)
														{
		//////////////////////////
		///   CHANNEL GROUP   ///
		////////////////////////
																#region Channel Group
																lastGroup = ParseChannelGroup(lineIn);
															//li = lineIn.IndexOf(utils.ENDFLD);
															li = utils.ContainsKey(lineIn, utils.ENDFLD);
															if (li < 0)
															{
																lineIn = reader.ReadLine();
																lineCount++;
																lineIn = reader.ReadLine();
																lineCount++;
																//li = lineIn.IndexOf(TABLEchannelGroup + utils.FIELDsavedIndex);
																li = utils.ContainsKey(lineIn, TABLEchannelGroup + utils.FIELDsavedIndex);
																while (li > 0)
																{
																	int isl = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
																	lastGroup.Members.Add(Members.bySavedIndex[isl]);
																	//savedIndexes[isl].parents.Add(lastGroup.SavedIndex);

																	lineIn = reader.ReadLine();
																	lineCount++;
																	//li = lineIn.IndexOf(TABLEchannelGroup + utils.FIELDsavedIndex);
																	li = utils.ContainsKey(lineIn, TABLEchannelGroup + utils.FIELDsavedIndex);
																}
															}
																#endregion // Channel Group
															}
															else // Not a ChannelGroup
														{
																//! Cosmic Color Devices
																li = utils.ContainsKey(lineIn, STARTcosmic);
																if (li > 0)
																{
		////////////////////////////////
		///   COSMIC COLOR DEVICE   ///
		//////////////////////////////
																	#region Cosmic Color Device
																	lastCosmic = ParseCosmicDevice(lineIn);
																	li = utils.ContainsKey(lineIn, utils.ENDFLD);
																	if (li < 0)
																	{
																		lineIn = reader.ReadLine();
																		lineCount++;
																		lineIn = reader.ReadLine();
																		lineCount++;
																		// Cosmic Color Devices are just like groups
																		// inlcuding how the list of child nodes is done
																		li = utils.ContainsKey(lineIn, TABLEchannelGroup + utils.FIELDsavedIndex);
																		while (li > 0)
																		{
																			int isl = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
																			IMember mm = Members.bySavedIndex[isl]
;																			lastCosmic.Members.Add(mm);

																			lineIn = reader.ReadLine();
																			lineCount++;
																			li = utils.ContainsKey(lineIn, TABLEchannelGroup + utils.FIELDsavedIndex);
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
																	li = utils.ContainsKey(lineIn, STARTtrackItem);
																	if (li > 0)
																	{
		///////////////////////
		///   TRACK ITEM   ///
		/////////////////////
																		#region Track Item
																		int si = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
																		lastTrack.Members.Add(Members.bySavedIndex[si]);
																		#endregion // Track Item
																	}
																	else // Not a regular channel
																	{
																		//! Tracks

																		//! Tracks are [apparently] getting added twice														


																		//li = lineIn.IndexOf(STARTtrack);
																		li = utils.ContainsKey(lineIn, STARTtrack);
																		if (li > 0)
																		{
		//////////////////
		///   TRACK   ///
		////////////////
																			#region Track
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
																			//li = lineIn.IndexOf(utils.ENDFLD);
																			li = utils.ContainsKey(lineIn, utils.ENDFLD);
																			if (li < 0)
																			{
																				lineIn = reader.ReadLine();
																				lineCount++;
																				lineIn = reader.ReadLine();
																				lineCount++;
																				//li = lineIn.IndexOf(STARTtrackItem);
																				li = utils.ContainsKey(lineIn, STARTtrackItem);
																				while (li > 0)
																				{
																					int isi = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
																					//lastTrack.itemSavedIndexes.Add(isi);
																					if (isi == 2189) System.Diagnostics.Debugger.Break();
																					if (isi <= Members.HighestSavedIndex)
																					{
																						IMember SIMem = Members.bySavedIndex[isi];
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
																					li = utils.ContainsKey(lineIn, STARTtrackItem);
																				}
																			}
																			#endregion // Track
																		} // end if a track
																		else // not a track
																		{
																			//! Timing Grids
																			//li = lineIn.IndexOf(STARTtimingGrid);
																			li = utils.ContainsKey(lineIn, STARTtimingGrid);
																			if (li > 0)
																			{
		////////////////////////
		///   TIMING GRID   ///
		//////////////////////
																				#region Timing Grid
																				lastGrid = ParseTimingGrid(lineIn);
																				if (lastGrid.TimingGridType == TimingGridType.Freeform)
																				{
																					lineIn = reader.ReadLine();
																					lineCount++;
																					//li = lineIn.IndexOf(STARTgridItem);
																					li = utils.ContainsKey(lineIn, STARTgridItem);
																					while (li > 0)
																					{
																						int gpos = utils.getKeyValue(lineIn, utils.FIELDcentisecond);
																						lastGrid.AddTiming(gpos);
																						lineIn = reader.ReadLine();
																						lineCount++;
																						//li = lineIn.IndexOf(STARTgridItem);
																						li = utils.ContainsKey(lineIn, STARTgridItem);
																					}
																				}
																				#endregion
																			}
																			else // Not a timing grid
																			{
																				//! Loop Levels
																				//li = lineIn.IndexOf(STARTloopLevel);
																				li = utils.ContainsKey(lineIn, STARTloopLevel);
																				if (li > 0)
																				{
																					lastll = lastTrack.AddLoopLevel(lineIn);
																				}
																				else // not a loop level
																				{
																					//! Loops
																					//li = lineIn.IndexOf(STARTloop);
																					li = utils.ContainsKey(lineIn, STARTloop);
																					if (li > 0)
																					{
																						lastll.AddLoop(lineIn);
																					}
																					else // not a loop
																					{
																						//! Animation Rows
																						//li = lineIn.IndexOf(STARTaniRow);
																						li = utils.ContainsKey(lineIn, STARTaniRow);
																						if (li > 0)
																						{
																							lastAniRow = animation.AddRow(lineIn);
																						}
																						else
																						{
																							//! Animation Columns
																							//li = lineIn.IndexOf(STARTaniCol);
																							li = utils.ContainsKey(lineIn, STARTaniCol);
																							if (li > 1)
																							{
																								lastAniRow.AddColumn(lineIn);
																							} // end animationColumn
																							else
																							{
																								//! Animation
																								//li = lineIn.IndexOf(utils.STFLD + TABLEanimation + utils.SPC);
																								li = utils.ContainsKey(lineIn, utils.STFLD + TABLEanimation + utils.SPC);
																								if (li > 0)
																								{
																									animation = new Animation(this, lineIn);
																								} // end if Animation, or not
																							} // end if AnimationColumn, or not
																						} // end if Animation, or not
																					} // end if LoopLevel, or not
																				} // end if Loop, or not (as in a loopLevel loop, not a for loop)
																			} // end TimingGrid (or not)
																		} // end Track (or not)
																	} // end Track Items (or not)
																} // end Cosmic Color Device (or not)
														} // end ChannelGroup (or not)
													} // end RGBchannel (or not)
												} // end regular Channel (or not)
											} // end timing (or not)
										} // end Effect (or not)
											
								} // end 2nd Try
										catch (Exception ex)
										{
											StackTrace strx = new StackTrace(ex, true);
											StackFrame sf = strx.GetFrame(strx.FrameCount - 1);
											string emsg = ex.Message + utils.CRLF;
											emsg += "at Sequence4.ReadSequence()" + utils.CRLF;
											emsg += "File:" + existingFileName + utils.CRLF;
											emsg += "on line " + lineCount.ToString() + " at position " + li.ToString() + utils.CRLF;
											emsg += "Line Is:" + lineIn + utils.CRLF;
											emsg += "in code line " + sf.GetFileLineNumber() + utils.CRLF;
											emsg += "Last SavedIndex = " + Members.HighestSavedIndex.ToString();
											info.LastError.fileLine = lineCount;
											info.LastError.linePos = li;
											info.LastError.codeLine = sf.GetFileLineNumber();
											info.LastError.errName = ex.ToString();
											info.LastError.errMsg = emsg;
											info.LastError.lineIn = lineIn;

	#if DEBUG
											System.Diagnostics.Debugger.Break();
	#endif
											utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
											if (utils.IsWizard)
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
								dirty = false;
								Members.ReIndex();
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
				utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
				if (utils.IsWizard)
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
			//int altSI = utils.UNDEFINED;
			updatedTrack[] updatedTracks = new updatedTrack[Tracks.Count];
			//bySavedIndex.altHighestSavedIndex = utils.UNDEFINED;
			//bySavedIndex.altSaveID = utils.UNDEFINED;
			//altSavedIndexes = null;
			Members.ResetWritten();
			//Array.Resize(ref altSavedIndexes, highestSavedIndex + 3);
			//Array.Resize(ref altSaveIDs, TimingGrids.Count + 1);
			string ext = Path.GetExtension(newFileName).ToLower();
			bool channelConfig = false;
			if (ext.CompareTo(".lcc") == 0) channelConfig = true;
			if (channelConfig) noEffects = true;
			//TODO: implement channelConfig flag to write just a channel config file

			// Clear any 'Written' flags from a previous save
			ClearWrittenFlags();

			// Write the Timing Grids
			if (WriteSelectedGridsOnly)
			{
				//! First, A Track->TimingGrid renumbering
				// Timing Grids do not get Written to the file yet
				// But we must renumber the saveIDs
				// Assign new altSaveIDs in the order they appear in the Tracks
				foreach (Track theTrack in Tracks)
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
								TimingGrid ntg = CreateTimingGrid("Fixed Grid .05");
								ntg.spacing = 5;
								theTrack.timingGrid = ntg;
							}
						}
						int asi = AssignNextAltSaveID(theTrack.timingGrid);
					}
				}
				for (int tg = 0; tg < TimingGrids.Count; tg++)
				{
					TimingGrid theGrid = TimingGrids[tg];
					// Any remaining timing grids that are Selected, but not used by any Tracks
					if ((!selectedOnly) || (theGrid.Selected))
					{
						if (theGrid.AltSaveID == utils.UNDEFINED)
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
					TimingGrid theGrid = TimingGrids[tg];
					if (theGrid.AltSaveID == utils.UNDEFINED)
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
			lineOut = utils.LEVEL1 + utils.STFLD + utils.TABLEchannel + utils.PLURAL + utils.FINFLD;
			writer.WriteLine(lineOut);

			// Loop thru Tracks and write the items (details) in the order they appear
			foreach (Track theTrack in Tracks)
			{
				if ((!selectedOnly) || (theTrack.Selected))
				{
					WriteListItems(theTrack.Members, selectedOnly, noEffects, MemberType.Items);
				}
			}
			// All Channels should now be Written, close this section
			lineOut = utils.LEVEL1 + utils.FINTBL + utils.TABLEchannel + utils.PLURAL + utils.FINFLD;
			writer.WriteLine(lineOut);

			// TIMING GRIDS
			lineOut = utils.LEVEL1 + utils.STFLD + Sequence4.TABLEtimingGrid + utils.PLURAL + utils.FINFLD;
			writer.WriteLine(lineOut);
			foreach (TimingGrid theGrid in TimingGrids)
			{
				// TIMING GRIDS
				if ((!selectedOnly) || (theGrid.Selected))
				{
					WriteTimingGrid(theGrid);
				}
			}
			lineOut = utils.LEVEL1 + utils.FINTBL + Sequence4.TABLEtimingGrid + utils.PLURAL + utils.FINFLD;
			writer.WriteLine(lineOut);

			// TRACKS
			lineOut = utils.LEVEL1 + utils.STFLD + TABLEtrack + utils.PLURAL + utils.FINFLD;
			writer.WriteLine(lineOut);
			// Loop thru Tracks
			foreach (Track theTrack in Tracks)
			{
				if ((!selectedOnly) || (theTrack.Selected))
				{
					// Items in the track have already been written
					// This writes the track info itself including its member list
					// and loop levels
					WriteTrack(theTrack, selectedOnly, MemberType.Items);
				}
			}
			lineOut = utils.LEVEL1 + utils.FINTBL + TABLEtrack + utils.PLURAL + utils.FINFLD;
			writer.WriteLine(lineOut);

			WriteSequenceClose();

			errorStatus = RenameTempFile(newFileName);
			if (filename.Length < 3)
			{
				//filename = newFileName;
				info.filename = newFileName;
			}
			dirty = false;
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

		public int WriteChannel(Channel theChannel)
		{
			return WriteChannel(theChannel, false);
		}

		public int WriteChannel(Channel theChannel, bool noEffects)
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

		private int WriteRGBchannel(RGBchannel theRGBchannel)
		{
			return WriteRGBchannel(theRGBchannel, false, false, MemberType.Items);
		}

		private int WriteRGBchannel(RGBchannel theRGBchannel, bool selectedOnly, bool noEffects, MemberType itemTypes)
		{
			if (!theRGBchannel.Written)
			{
				if ((!selectedOnly) || (theRGBchannel.Selected))
				{
					if ((itemTypes == MemberType.Items) || (itemTypes == MemberType.Items) || (itemTypes == MemberType.Channel))
					{
						//theRGBchannel.LineOut(selectedOnly, noEffects, MemberType.Channel);
						//lineOut = theRGBchannel.redChannel.LineOut(noEffects);
						//lineOut += utils.CRLF + theRGBchannel.grnChannel.LineOut(noEffects);
						//lineOut += utils.CRLF + theRGBchannel.bluChannel.LineOut(noEffects);
						WriteChannel(theRGBchannel.redChannel, noEffects);
						WriteChannel(theRGBchannel.grnChannel, noEffects);
						WriteChannel(theRGBchannel.bluChannel, noEffects);
						//writer.WriteLine(lineOut);
					}

					if ((itemTypes == MemberType.Items) || (itemTypes == MemberType.Items) || (itemTypes == MemberType.RGBchannel))
					{
						//theRGBchannel.AltSavedIndex = bySavedIndex.GetNextAltSavedIndex(theRGBchannel.SavedIndex);
						int altSI = AssignNextAltSavedIndex(theRGBchannel);
						lineOut = theRGBchannel.LineOut(selectedOnly, noEffects, MemberType.RGBchannel);
						writer.WriteLine(lineOut);
						//theRGBchannel.Written = true;
					}
				}
			}
			return theRGBchannel.AltSavedIndex;
		} // end writergbChannel

		private int WriteChannelGroup(ChannelGroup theGroup)
		{
			return WriteChannelGroup(theGroup, false, false, MemberType.Items);
		}

		private int WriteChannelGroup(ChannelGroup theGroup, bool selectedOnly, bool noEffects, MemberType itemTypes)
		{
			// May be called recursively (because groups can contain groups)
			List<int> altSIs = new List<int>();
			//if (theGroup.Name.IndexOf("ng Parts") > 0) System.Diagnostics.Debugger.Break();
			//if (theGroup.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();

			if (!theGroup.Written)
			{
				if ((!selectedOnly) || (theGroup.Selected))
				{
					if ((itemTypes == MemberType.Items) ||
						(itemTypes == MemberType.Channel) ||
						(itemTypes == MemberType.RGBchannel))
					{
						// WriteListItems may in turn call this procedure (WriteChannelGroup) for any Child Channel Groups
						// Therefore making it recursive at this point
						altSIs = WriteListItems(theGroup.Members, selectedOnly, noEffects, itemTypes);
					}

					if ((itemTypes == MemberType.Items) ||
						(itemTypes == MemberType.ChannelGroup))
					{
						//theGroup.AltSavedIndex = bySavedIndex.GetNextAltSavedIndex(theGroup.SavedIndex);
						//if (altSIs.Count > 0)
						//{
						int altSI = AssignNextAltSavedIndex(theGroup);
						theGroup.AltSavedIndex = altSI;
						//lineOut = theGroup.LineOut(selectedOnly);

						lineOut = utils.LEVEL2 + utils.STFLD + Sequence4.TABLEchannelGroupList;
						lineOut += utils.FIELDtotalCentiseconds + utils.FIELDEQ + theGroup.Centiseconds.ToString() + utils.ENDQT;
						lineOut += utils.FIELDname + utils.FIELDEQ + utils.XMLifyName(theGroup.Name) + utils.ENDQT;
						lineOut += utils.FIELDsavedIndex + utils.FIELDEQ + altSI.ToString() + utils.ENDQT;
						lineOut += utils.FINFLD;
						writer.WriteLine(lineOut);

						WriteItemsList(theGroup.Members, selectedOnly, itemTypes);

						lineOut = utils.LEVEL2 + utils.FINTBL + Sequence4.TABLEchannelGroupList + utils.FINFLD;
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

		private int WriteTrack(Track theTrack, bool selectedOnly, MemberType itemTypes)
		{
			string lineOut = utils.LEVEL2 + utils.STFLD + Sequence4.TABLEtrack;
			//! LOR writes it with the Name last
			// In theory, it shouldn't matter
			//if (Name.Length > 1)
			//{
			//	ret += utils.SPC + FIELDname + utils.FIELDEQ + Name + utils.ENDQT;
			//}
			lineOut += utils.FIELDtotalCentiseconds + utils.FIELDEQ + theTrack.Centiseconds.ToString() + utils.ENDQT;
			int altID = 0;
			if (theTrack.timingGrid == null)
			{
				theTrack.timingGrid = TimingGrids[0];
				altID = theTrack.timingGrid.AltSaveID;
			}
			// if track is selected, but it's timing grid isn't, default to grid 0
			if (altID < 0) altID = 0;
			lineOut += utils.SPC + Sequence4.TABLEtimingGrid + utils.FIELDEQ + altID.ToString() + utils.ENDQT;
			// LOR writes it with the Name last
			if (theTrack.Name.Length > 1)
			{
				lineOut += utils.FIELDname + utils.FIELDEQ + utils.XMLifyName(theTrack.Name) + utils.ENDQT;
			}
			lineOut += utils.FINFLD;
			//int siOld = utils.UNDEFINED;
			//int siAlt = utils.UNDEFINED;
			writer.WriteLine(lineOut);

			WriteItemsList(theTrack.Members, selectedOnly, itemTypes);

			// Write out any LoopLevels in this track
			lineOut = "";
			if (theTrack.loopLevels.Count > 0)
			{
				lineOut += utils.LEVEL3 + utils.STFLD + Sequence4.TABLEloopLevels + utils.FINFLD + utils.CRLF;
				foreach (LoopLevel ll in theTrack.loopLevels)
				{
					lineOut += ll.LineOut() + utils.CRLF;
				}
				lineOut += utils.LEVEL3 + utils.FINTBL + Sequence4.TABLEloopLevels + utils.FINFLD + utils.CRLF;
			}
			else
			{
				lineOut += utils.LEVEL3 + utils.STFLD + Sequence4.TABLEloopLevels + utils.ENDFLD + utils.CRLF;
			}

			// finish the track
			lineOut += utils.LEVEL2 + utils.FINTBL + Sequence4.TABLEtrack + utils.FINFLD;
			writer.WriteLine(lineOut);


			return theTrack.AltSavedIndex;
		}

		private List<int> WriteListItems(Membership itemIDs, bool selectedOnly, bool noEffects, MemberType itemTypes)
		{
			// NOTE: This writes out all the individual items in a membership list
			// It is recursive, also writing any items in subgroups
			// This does NOT write the list of items, that is handled later by the counterpart to this, 'WriteItemsList'

			int altSaveIndex = utils.UNDEFINED;
			List<int> altSIs = new List<int>();
			string itsName = "";  //! for debugging

			//if (itemIDs.owner.Name.IndexOf("ng Parts") > 0) System.Diagnostics.Debugger.Break();
			//if (itemIDs.owner.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();
			//Identity id = null;

			foreach (IMember item in itemIDs.Items)
			{
				// if (item.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();

				//id = Members.bySavedIndex[si];
				itsName = item.Name;
				if ((!selectedOnly) || (item.Selected))
				{
					if (!item.Written)
					{
						if (item.MemberType == MemberType.Channel)
						{
							// Prevents unnecessary processing of Channels which have already been Written, during RGB channel and group processing
							if ((itemTypes == MemberType.Items) || (itemTypes == MemberType.Items) || (itemTypes == MemberType.Channel))
							{
								altSaveIndex = WriteChannel((Channel)item, noEffects);
								altSIs.Add(altSaveIndex);
							}
						}
						else
						{
							if (item.MemberType == MemberType.RGBchannel)
							{
								RGBchannel theRGB = (RGBchannel)item;
								if ((itemTypes == MemberType.Items) || (itemTypes == MemberType.Items) || (itemTypes == MemberType.Channel) || (itemTypes == MemberType.RGBchannel))
								{
									altSaveIndex = WriteRGBchannel(theRGB, selectedOnly, noEffects, itemTypes);
									altSIs.Add(altSaveIndex);
								}
							}
							else
							{
								if (item.MemberType == MemberType.ChannelGroup)
								{
									//if (itemTypes == MemberType.channelGroup)
									//if ((itemTypes == MemberType.None) ||
									//    (itemTypes == MemberType.rgbChannel) ||
									//    (itemTypes == MemberType.channel) ||
									//    (itemTypes == MemberType.channelGroup))
									// Type NONE actually means ALL in this case
									//{

									//									if (item.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break(); // Break here for debugging if named group is found


									// WriteChannelGroup calls this procedure (WriteListItems) so this is where it gets recursive
									altSaveIndex = WriteChannelGroup((ChannelGroup)item, selectedOnly, noEffects, itemTypes);
									altSIs.Add(altSaveIndex);
									//}
								} // if ChannelGroup, or not
							} // if RGBchannel, or not
						} // if regular Channel, or not
					} // if not Written
				} // if Selected
			} // loop thru items

			return altSIs;
		}

		private int WriteItemsList(Membership itemIDs, bool selectedOnly, MemberType itemTypes)
		{
			// NOTE: This writes out the list of items in a membership list.
			// It is NOT recursive.
			// This does NOT write the the individual items, that was handled previously by the counterpart to this, 'WriteListItems'

			//if (itemIDs.owner.Name.IndexOf("ng Parts") > 0) System.Diagnostics.Debugger.Break();
			//if (itemIDs.owner.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();

			int count = 0;
			string leader = utils.LEVEL4 + utils.STFLD;


			string lineOut = utils.LEVEL3 + utils.STFLD;
			if (itemIDs.owner.MemberType == MemberType.Track)
			{
				lineOut += utils.TABLEchannel;
				leader += utils.TABLEchannel;
			}
			if (itemIDs.owner.MemberType == MemberType.ChannelGroup)
			{
				lineOut += Sequence4.TABLEchannelGroup;
				leader += Sequence4.TABLEchannelGroup;
			}
			lineOut += utils.PLURAL + utils.FINFLD + utils.CRLF;

			// Loop thru all items in membership list
			foreach (IMember subID in itemIDs.Items)
			{
				//if (subID.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();

				if (subID.Written)  // The item itself should have already been written!
				{
					bool sel = subID.Selected;
					if (!selectedOnly || sel)
					{
						if ((itemTypes == subID.MemberType) || (itemTypes == MemberType.Items))
						{
							int siAlt = subID.AltSavedIndex;
							if (siAlt > utils.UNDEFINED)
							{
								lineOut += leader;
								lineOut += utils.FIELDsavedIndex + utils.FIELDEQ + siAlt.ToString() + utils.ENDQT;
								lineOut += utils.ENDFLD + utils.CRLF;
								count++;
							}
						}
					}
				}
			}

			// Close the list of items
			lineOut += utils.LEVEL3 + utils.FINTBL;
			if (itemIDs.owner.MemberType == MemberType.Track)
			{
				lineOut += utils.TABLEchannel;
			}
			if (itemIDs.owner.MemberType == MemberType.ChannelGroup)
			{
				lineOut += Sequence4.TABLEchannelGroup;
			}
			lineOut += utils.PLURAL + utils.FINFLD;

			writer.WriteLine(lineOut);
			return count;
		}

		public int WriteItem(int SavedIndex)
		{
			return WriteItem(SavedIndex, false, false, MemberType.Items);
		}

		public int WriteItem(int SavedIndex, bool selectedOnly, bool noEffects, MemberType theType)
		{
			int ret = utils.UNDEFINED;

			IMember member = Members.bySavedIndex[SavedIndex];
			if (!member.Written)
			{
				if (!selectedOnly || member.Selected)
				{
					MemberType itemType = member.MemberType;
					if (itemType == MemberType.Channel)
					{
						Channel theChannel = (Channel)member;
						if ((theType == MemberType.Channel) || (theType == MemberType.Items))
						{
							ret = WriteChannel(theChannel, noEffects);
						} // end if type
					} // end if Channel
					else
					{
						if (itemType == MemberType.RGBchannel)
						{
							RGBchannel theRGB = (RGBchannel)member;
							ret = WriteRGBchannel(theRGB, selectedOnly, noEffects, theType);
						} // end if RGBchannel
						else
						{
							if (itemType == MemberType.ChannelGroup)
							{
								ChannelGroup theGroup = (ChannelGroup)member;
								ret = WriteChannelGroup(theGroup, selectedOnly, noEffects, theType);
							} // end if RGBchannel
						} // RGBchannel, or not
					} // end a channel, or not
				} // end Selected
			} // end if not Written

			return ret;
		}



		private int WriteTimingGrid(TimingGrid theGrid)
		{
			return WriteTimingGrid(theGrid, false);
		}

		private int WriteTimingGrid(TimingGrid theGrid, bool selectedOnly)
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
			lineOut = utils.FINTBL + TABLEsequence + utils.FINFLD; // "</sequence>";
			writer.WriteLine(lineOut);

			Console.WriteLine(lineCount.ToString() + " Out:" + lineOut);
			Console.WriteLine("");

			// We're done writing the file
			writer.Flush();
			writer.Close();
			info.file_saved = DateTime.Now;
			dirty = false;
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
			foreach (Channel ch in Channels)
			{
				//ch.Written = false;
				ch.AltSavedIndex = utils.UNDEFINED;
			}
			foreach (RGBchannel rch in RGBchannels)
			{
				//rch.Written = false;
				rch.AltSavedIndex = utils.UNDEFINED;
			}
			foreach (ChannelGroup chg in ChannelGroups)
			{
				//chg.Written = false;
				chg.AltSavedIndex = utils.UNDEFINED;
			}
			foreach (CosmicDevice dev in CosmicDevices)
			{
				//chg.Written = false;
				dev.AltSavedIndex = utils.UNDEFINED;
			}
			foreach (Track tr in Tracks)
			{
				tr.AltSavedIndex = utils.UNDEFINED;
			}
			foreach (TimingGrid tg in TimingGrids)
			{
				//tg.Written = false;
				tg.AltSavedIndex = utils.UNDEFINED;
			}
		} // end ClearWrittenFlags

		#endregion

		public static string DefaultSequencesPath
		{
			get
			{
				return utils.DefaultNonAudioPath;
			}
		}

		public int LeftBushesChildCount()
			//! for debugging only!

		{
			int ret = 0;
			for (int g = 0; g < ChannelGroups.Count; g++)
			{
				int p = ChannelGroups[g].Name.IndexOf("eft Bushes");
				if (p > 0)
				{
					ret = ChannelGroups[g].Members.Count;
					g = ChannelGroups.Count; // Force exit of for loop
				}
			}
			return ret;

		}


		//TODO: add RemoveChannel, RemoveRGBchannel, RemoveChannelGroup, and RemoveTrack procedures



		public string GetItemName(int SavedIndex)
		{
			string nameOut = "";
			IMember member = Members.bySavedIndex[SavedIndex];
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
				this.Members = new Membership(this);
				this.Members.SetParentSequence(this);
				this.info = new Info(this);
				this.animation = new Animation(this);
				Channels = new List<Channel>();
				RGBchannels = new List<RGBchannel>();
				ChannelGroups = new List<ChannelGroup>();
				CosmicDevices = new List<CosmicDevice>();
				Tracks = new List<Track>();
				TimingGrids = new List<TimingGrid>();
				//Members.SetParentSequence(this);
				myCentiseconds = 0;
				dirty = false;

			} // end Are You Sure
		} // end Clear Sequence

		public string summary()
		{
			string sMsg = "";
			sMsg += "           Filename: " + info.filename + utils.CRLF + utils.CRLF;
			sMsg += "              Lines: " + lineCount.ToString() + utils.CRLF;
			sMsg += "   Regular Channels: " + Channels.Count.ToString() + utils.CRLF;
			sMsg += "       RGB Channels: " + RGBchannels.Count.ToString() + utils.CRLF;
			sMsg += "     Channel Groups: " + ChannelGroups.Count.ToString() + utils.CRLF;
			sMsg += "       Timing Grids: " + TimingGrids.Count.ToString() + utils.CRLF;
			sMsg += "             Tracks: " + Tracks.Count.ToString() + utils.CRLF;
			sMsg += "       Centiseconds: " + myCentiseconds.ToString() + utils.CRLF;
			sMsg += "Highest Saved Index: " + Members.HighestSavedIndex.ToString() + utils.CRLF;
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
					matches[ch].MemberType = MemberType.Channel;
					matches[ch].itemIdx = ch;
				}
				q = Channels.Count;
			} // channel count > 0

			// Any RGB Channels?
			if (RGBchannels.Count > 0)
			{
				// Loop thru 'em and add their Name and info to the list
				for (int rg = 0; rg < RGBchannels.Count; rg++)
				{
					Array.Resize(ref matches, q + 1);
					matches[q].Name = RGBchannels[rg].Name;
					matches[q].savedIdx = RGBchannels[rg].SavedIndex;
					matches[q].MemberType = MemberType.RGBchannel;
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
					matches[q].MemberType = MemberType.ChannelGroup;
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
				// Loop thru sorted list, comparing each member to the previous one
				for (int ql = 1; ql < q; ql++)
				{
					if (matches[ql].Name.CompareTo(matches[q].Name) == 0)
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

		public void CentiFix(int newCentiseconds)
		{
			// Sets the sequence, and all tracks, channels, groups, to the new time (length)
			// And removes any timing marks or effects after that time.
			myCentiseconds = newCentiseconds;
			for (int i=0; i< Tracks.Count; i++)
			{
				Tracks[i].Centiseconds = newCentiseconds;
			}
			for (int i = 0; i < TimingGrids.Count; i++)
			{
				TimingGrids[i].Centiseconds = newCentiseconds;
				if (TimingGrids[i].TimingGridType == TimingGridType.Freeform)
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
							t--;
						}
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
								e--;
							}
						}
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
			int pos1 = utils.UNDEFINED; // positions of certain key text in the line

			// Zero these out from any previous run
			Clear(true);

			int curChannel = utils.UNDEFINED;
			int curSavedIndex = utils.UNDEFINED;
			int curEffect = utils.UNDEFINED;
			int curTimingGrid = utils.UNDEFINED;
			int curGridItem = utils.UNDEFINED;

			// * PASS 1 - COUNT OBJECTS
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// does this line mark the start of a channel?
				//pos1 = lineIn.IndexOf("xml version=");
				pos1 = utils.ContainsKey(lineIn, "xml version=");
				if (pos1 > 0)
				{
						info.xmlInfo = lineIn;
				}
				//pos1 = lineIn.IndexOf("saveFileVersion=");
				pos1 = utils.ContainsKey(lineIn, "saveFileVersion=");
				if (pos1 > 0)
				{
					info.Parse(lineIn);
				}
				//pos1 = lineIn.IndexOf(utils.STFLD + utils.TABLEchannel + utils.FIELDname);
				pos1 = utils.ContainsKey(lineIn, utils.STFLD + utils.TABLEchannel + utils.FIELDname);
				if (pos1 > 0)
				{
					//channelsCount++;
				}
				//pos1 = lineIn.IndexOf(utils.STFLD + TABLEeffect + utils.SPC);
				pos1 = utils.ContainsKey(lineIn, utils.STFLD + TABLEeffect + utils.SPC);
				if (pos1 > 0)
				{
					//effectCount++;
				}
				if (Tracks.Count == 0)
				{
				}
				//pos1 = lineIn.IndexOf(utils.STFLD + TABLEtimingGrid + utils.SPC);
				pos1 = utils.ContainsKey(lineIn, utils.STFLD + TABLEtimingGrid + utils.SPC);
				if (pos1 > 0)
				{
					//timingGridCount++;
				}
				//pos1 = lineIn.IndexOf(utils.STFLD + TimingGrid.TABLEtiming + utils.SPC);
				pos1 = utils.ContainsKey(lineIn, utils.STFLD + TimingGrid.TABLEtiming + utils.SPC);
				if (pos1 > 0)
				{
					//gridItemCount++;
				}

				//pos1 = lineIn.IndexOf(utils.FIELDsavedIndex);
				pos1 = utils.ContainsKey(lineIn, utils.FIELDsavedIndex);
				if (pos1 > 0)
				{
					curSavedIndex = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
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
				//pos1 = lineIn.IndexOf(utils.TABLEchannel + utils.ENDFLD);
				pos1 = utils.ContainsKey(lineIn, utils.TABLEchannel + utils.ENDFLD);
				if (pos1 > 0)
				{
					curChannel++;
					//Channel chan = new Channel();
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
					//chan.color = utils.getKeyValue(lineIn, FIELDcolor);
					//chan.Centiseconds = utils.getKeyValue(lineIn, FIELDcentiseconds);
					//chan.deviceType = SeqEnums.enumDevice(utils.getKeyWord(lineIn, FIELDdeviceType));
					//chan.unit = utils.getKeyValue(lineIn, FIELDunit);
					//chan.network = utils.getKeyValue(lineIn, FIELDnetwork);
					//chan.circuit = utils.getKeyValue(lineIn, FIELDcircuit);
					//chan.SavedIndex = utils.getKeyValue(lineIn, FIELDsavedIndex);
					//Channels[curChannel] = chan;
					//curSavedIndex = chan.SavedIndex;

					//si.PartType = MemberType.channel;
					//si.objIndex = curChannel;
					//savedIndexes[curSavedIndex] = si;
					if (chwhich == 2)
					{ }
					chwhich++;
					chwhich %= 3;

				}

				// does this line mark the start of an Effect?
				//pos1 = lineIn.IndexOf(TABLEeffect + utils.FIELDtype);
				pos1 = utils.ContainsKey(lineIn, TABLEeffect + utils.FIELDtype);
				if (pos1 > 0)
				{
					curEffect++;

					//DEBUG!
					if (curEffect > 638)
					{
						errorStatus = 1;
					}

					Effect ef = new Effect();
					ef.EffectType = SeqEnums.enumEffectType(utils.getKeyWord(lineIn, utils.FIELDtype));
					ef.startCentisecond = utils.getKeyValue(lineIn, utils.FIELDstartCentisecond);
					ef.endCentisecond = utils.getKeyValue(lineIn, utils.FIELDendCentisecond);
					ef.Intensity = utils.getKeyValue(lineIn, utils.SPC + Effect.FIELDintensity);
					ef.startIntensity = utils.getKeyValue(lineIn, Effect.FIELDstartIntensity);
					ef.endIntensity = utils.getKeyValue(lineIn, Effect.FIELDendIntensity);
					Channels[curChannel].effects.Add(ef);
				}

				// does this line mark the start of a Timing Grid?
				//pos1 = lineIn.IndexOf(utils.STFLD + TABLEtimingGrid + utils.SPC);
				pos1 = utils.ContainsKey(lineIn, utils.STFLD + TABLEtimingGrid + utils.SPC);
				if (pos1 > 0)
				{
					curTimingGrid++;
					//TimingGrid tg = new TimingGrid();
					//tg.Name = utils.getKeyWord(lineIn, utils.FIELDname);
					//tg.type = SeqEnums.enumGridType(utils.getKeyWord(lineIn, utils.FIELDtype));
					//tg.SavedIndex = utils.getKeyValue(lineIn, TimingGrid.FIELDsaveID);
					//tg.spacing = utils.getKeyValue(lineIn, TimingGrid.FIELDspacing);
					//TimingGrids[curTimingGrid] = tg;

					//if (tg.type == TimingGridType.Freeform)
					{
						lineIn = reader.ReadLine();
						//pos1 = lineIn.IndexOf(TimingGrid.TABLEtiming + utils.FIELDcentisecond);
						pos1 = utils.ContainsKey(lineIn, TimingGrid.TABLEtiming + utils.FIELDcentisecond);
						while (pos1 > 0)
						{
							curGridItem++;
							int gpos = utils.getKeyValue(lineIn, utils.FIELDcentisecond);
							TimingGrids[curTimingGrid].AddTiming(gpos);

							lineIn = reader.ReadLine();
							//pos1 = lineIn.IndexOf(TimingGrid.TABLEtiming + utils.FIELDcentisecond);
							pos1 = utils.ContainsKey(lineIn, TimingGrid.TABLEtiming + utils.FIELDcentisecond);
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
										//int pos1 = utils.UNDEFINED; // positions of certain key text in the line

			int curTimingGrid = 0;
			//int curGridItem = 0;
			//int curTrack = 0;
			//int curTrackItem = 0;
			int[] masterSIs = new int[1];
			//int masterSI = utils.UNDEFINED;
			updatedTrack[] updatedTracks = new updatedTrack[Tracks.Count];

			lineOut = info.xmlInfo;
			writer.WriteLine(lineOut);
			lineOut = utils.STFLD + TABLEchannelsClipboard + " version=\"1\" Name=\"" + Path.GetFileNameWithoutExtension(newFilename) + "\"" + utils.ENDFLD;
			writer.WriteLine(lineOut);

			// Write Timing Grid aka cellDemarcation
			lineOut = utils.LEVEL1 + utils.STFLD + TABLEcellDemarcation + utils.PLURAL + utils.FINFLD;
			writer.WriteLine(lineOut);
			for (int tm = 0; tm < TimingGrids[0].timings.Count; tm++)
			{
				lineOut = utils.LEVEL2 + utils.STFLD + TABLEcellDemarcation;
				lineOut += utils.FIELDcentisecond + utils.FIELDEQ + TimingGrids[curTimingGrid].timings[tm].ToString() + utils.ENDQT;
				lineOut += utils.ENDFLD;
				writer.WriteLine(lineOut);
			}
			lineOut = utils.LEVEL1 + utils.FINTBL + TABLEcellDemarcation + utils.PLURAL + utils.FINFLD;
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

			lineOut = utils.LEVEL1 + utils.FINTBL + utils.TABLEchannel + utils.PLURAL + utils.FINFLD;
			writer.WriteLine(lineOut);

			lineOut = utils.FINTBL + TABLEchannelsClipboard + utils.FINFLD;
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
			MemberType itemType = MemberType.None; //savedIndexes[saveID].PartType;
			if (itemType == MemberType.Channel)
			{
				lineOut = utils.LEVEL2 + utils.STFLD + utils.TABLEchannel + utils.ENDFLD;
				writer.WriteLine(lineOut);
				//WriteEffects(Channels[oi]);
				lineOut = utils.LEVEL2 + utils.FINTBL + utils.TABLEchannel + utils.ENDFLD;
				writer.WriteLine(lineOut);
			} // end if channel

			if (itemType == MemberType.RGBchannel)
			{
				RGBchannel rgbch = RGBchannels[oi];
				// Get and write Red Channel
				//int ci = RGBchannels[oi].redChannelObjIndex;
				lineOut = utils.LEVEL2 + utils.STFLD + utils.TABLEchannel + utils.ENDFLD;
				writer.WriteLine(lineOut);

				//Channels[ci].LineOut()
				//lineOut = utils.LEVEL2 + utils.FINTBL + utils.TABLEchannel + utils.ENDFLD;
				writer.WriteLine(lineOut);

				// Get and write Green Channel
				//ci = RGBchannels[oi].grnChannelObjIndex;
				lineOut = utils.LEVEL2 + utils.STFLD + utils.TABLEchannel + utils.ENDFLD;
				writer.WriteLine(lineOut);
				//writeEffects(Channels[ci]);
				lineOut = utils.LEVEL2 + utils.FINTBL + utils.TABLEchannel + utils.ENDFLD;
				writer.WriteLine(lineOut);

				// Get and write Blue Channel
				//ci = RGBchannels[oi].bluChannelObjIndex;
				lineOut = utils.LEVEL2 + utils.STFLD + utils.TABLEchannel + utils.ENDFLD;
				writer.WriteLine(lineOut);
				//writeEffects(Channels[ci]);
				lineOut = utils.LEVEL2 + utils.FINTBL + utils.TABLEchannel + utils.ENDFLD;
				writer.WriteLine(lineOut);
			} // end if rgbChannel

			if (itemType == MemberType.ChannelGroup)
			{
				ChannelGroup grp = ChannelGroups[oi];
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

		public Channel FindChannel(int SavedIndex)
		{
			Channel ret = null;
			IMember member = Members.bySavedIndex[SavedIndex];
			if (member != null)
			{
				if (member.MemberType == MemberType.Channel)
				{
					ret = (Channel)member;
				}
			}
			return ret;
		} // end FindChannel

		public Channel FindChannel(string channelName)
		{
			Channel ret = null;
			IMember member = Members.Find(channelName, MemberType.Channel, false);
			if (member != null)
			{
				ret = (Channel)member;
			}
			return ret;
		}

		public RGBchannel FindRGBchannel(int SavedIndex)
		{
			RGBchannel ret = null;
			IMember member = Members.bySavedIndex[SavedIndex];
			if (member != null)
			{
				if (member.MemberType == MemberType.RGBchannel)
				{
					ret = (RGBchannel)member;
				}
			}
			return ret;
		} // end FindrgbChannel

		public RGBchannel FindRGBchannel(string rgbChannelName)
		{
			RGBchannel ret = null;
			IMember member = Members.Find(rgbChannelName, MemberType.RGBchannel, false);
			if (member != null)
			{
				ret = (RGBchannel)member;
			}
			return ret;
		}

		public ChannelGroup FindChannelGroup(int SavedIndex)
		{
			ChannelGroup ret = null;
			IMember member = Members.bySavedIndex[SavedIndex];
			if (member != null)
			{
				if (member.MemberType == MemberType.ChannelGroup)
				{
					ret = (ChannelGroup)member;
				}
			}
			return ret;
		} // end FindChannelGroup

		public ChannelGroup FindChannelGroup(string channelGroupName)
		{
			ChannelGroup ret = null;
			IMember member = Members.Find(channelGroupName, MemberType.ChannelGroup, false);
			if (member != null)
			{
				ret = (ChannelGroup)member;
			}
			return ret;
		}

		public Track FindTrack(string trackName, bool createIfNotFound = false)
		{
			Track ret = null;
			IMember member = Members.Find(trackName, MemberType.Track, false);
			if (member != null)
			{
				ret = (Track)member;
			}
			else
			{
				for (int tr = 0; tr < Tracks.Count; tr++)
				{
					int c = Tracks[tr].Name.CompareTo(trackName);
					if (c == 0)
					{
						ret = Tracks[tr];
						tr = Tracks.Count; // break
					}
				}
			}
			if (ret == null)
			{
				if (createIfNotFound)
				{
					ret = CreateTrack(trackName);
				}
			}
			return ret;
		}

		// Sequence4.FindTimingGrid(name, create)
		public TimingGrid FindTimingGrid(string timingGridName, bool createIfNotFound = false)
		{
#if DEBUG
			string msg = "Sequence4.FindTimingGrid(" + timingGridName + ", " + createIfNotFound.ToString() + ")";
			Debug.WriteLine(msg);
#endif
			TimingGrid ret = null;
			//IMember member = Members.Find(timingGridName, MemberType.TimingGrid, createIfNotFound);
			//IMember member = null;
			for (int i=0; i< TimingGrids.Count; i++)
			{
				TimingGrid member = TimingGrids[i];
				if (member.Name.CompareTo(timingGridName) == 0)
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
			SavedIndex[] siCheck = null;
			Array.Resize(ref siCheck, tot);
			for (int t = 0; t < tot; t++)
			{
				siCheck[t] = new SavedIndex();
			}

			for (int ch = 0; ch < Channels.Count; ch++)
			{
				if (Channels[ch].SavedIndex < tot)
				{
					if (siCheck[Channels[ch].SavedIndex].objIndex == utils.UNDEFINED)
					{
						siCheck[Channels[ch].SavedIndex].objIndex = ch;
					}
					else
					{
						errs++;
					}
					if (siCheck[Channels[ch].SavedIndex].MemberType == MemberType.None)
					{
						siCheck[Channels[ch].SavedIndex].MemberType = MemberType.Channel;
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
					if (siCheck[RGBchannels[rch].SavedIndex].objIndex == utils.UNDEFINED)
					{
						siCheck[RGBchannels[rch].SavedIndex].objIndex = rch;
					}
					else
					{
						errs++;
					}
					if (siCheck[RGBchannels[rch].SavedIndex].MemberType == MemberType.None)
					{
						siCheck[RGBchannels[rch].SavedIndex].MemberType = MemberType.RGBchannel;
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
					if (siCheck[ChannelGroups[chg].SavedIndex].objIndex == utils.UNDEFINED)
					{
						siCheck[ChannelGroups[chg].SavedIndex].objIndex = chg;
					}
					else
					{
						errs++;
					}
					if (siCheck[ChannelGroups[chg].SavedIndex].MemberType == MemberType.None)
					{
						siCheck[ChannelGroups[chg].SavedIndex].MemberType = MemberType.ChannelGroup;
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

			if (tot != Members.HighestSavedIndex + 1)
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
											 // Loop thru all regular Channels
			for (int ch = 0; ch < Channels.Count; ch++)
			{
				// if deviceType != None
				if (Channels[ch].output.deviceType != DeviceType.None)
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
					if (outputs[q - 1].CompareTo(outputs[q]) == 0)
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
				// Loop thru all regular Channels
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
					if (names[q - 1].CompareTo(names[q]) == 0)
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
				// Loop thru all regular Channels
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
					if (names[q - 1].CompareTo(names[q]) == 0)
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
				// Loop thru all regular Channels
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
					if (names[q - 1].CompareTo(names[q]) == 0)
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
				// Loop thru all regular Channels
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
					if (names[q - 1].CompareTo(names[q]) == 0)
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
				// Loop thru all regular Channels
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
				} // end Channel Group Loop

				int trIdx;
				for (int tr = 0; tr < Tracks.Count; tr++)
				{
					names[tr + Channels.Count + RGBchannels.Count + ChannelGroups.Count] = Tracks[tr].Name;
					// use negative numbers for track indexes
					trIdx = utils.UNDEFINED + (-tr);
					indexes[tr + Channels.Count + RGBchannels.Count + ChannelGroups.Count] = trIdx;
				}

				// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < Channels.Count; q++)
				{
					// compare output info
					if (names[q - 1].CompareTo(names[q]) == 0)
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

			int ret = utils.UNDEFINED;
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
			foreach (Channel ch in Channels)
			{
				ch.effects = new List<Effect>();
			}
			MakeDirty();
		}

		public int CopyEffects(Channel SourceChan, Channel DestChan, bool Merge)
		{
			// Could be a static method, but easier to work with if not
			int copiedCount = 0;
			if (!Merge)
			{
				DestChan.effects = new List<Effect>();
			}
			copiedCount = DestChan.CopyEffects(SourceChan.effects, Merge);
			MakeDirty();

			return copiedCount;
		}

		public Channel ParseChannel(string lineIn)
		{
			Channel chan = new Channel("");
			chan.SetParentSeq(this);
			chan.SetIndex(Channels.Count);
			Channels.Add(chan);
			chan.Parse(lineIn);
			Members.Add(chan);
			myCentiseconds = Math.Max(myCentiseconds, chan.Centiseconds);
			return chan;
		}

		public Channel CreateChannel(string theName)
		{
			// Does NOT check to see if a channel with this name already exists
			// Therefore, allows for duplicate channel names (But they will have different SavedIndexes)
			Channel chan;
			chan = new Channel(theName);
			int newSI = AssignNextSavedIndex(chan);
			chan.SetParentSeq(this);
			chan.Centiseconds = Centiseconds;
			chan.SetIndex(Channels.Count);
			Channels.Add(chan);
			Members.Add(chan);
			myCentiseconds = Math.Max(myCentiseconds, chan.Centiseconds);
			return chan;
		}

		public RGBchannel ParseRGBchannel(string lineIn)
		{
			RGBchannel rch = new RGBchannel("");
			rch.SetParentSeq(this);
			rch.SetIndex(RGBchannels.Count);
			RGBchannels.Add(rch);
			rch.Parse(lineIn);
			Members.Add(rch);
			myCentiseconds = Math.Max(myCentiseconds, rch.Centiseconds);
			return rch;
		}

		public RGBchannel CreateRGBchannel(string theName)
		{
			// Does NOT check to see if a channel with this name already exists
			// Therefore, allows for duplicate channel names (But they will have different SavedIndexes)
			RGBchannel rch;
			rch = new RGBchannel(theName, utils.UNDEFINED);
			int newSI = AssignNextSavedIndex(rch);
			rch.SetParentSeq(this);
			rch.Centiseconds = Centiseconds;
			rch.SetIndex(RGBchannels.Count);
			RGBchannels.Add(rch);
			Members.Add(rch);
			myCentiseconds = Math.Max(myCentiseconds, rch.Centiseconds);
			return rch;
		}

		public ChannelGroup ParseChannelGroup(string lineIn)
		{
			ChannelGroup chg = new ChannelGroup("");
			chg.SetParentSeq(this);
			chg.Members.SetParentSequence(this);
			chg.SetIndex(ChannelGroups.Count);
			chg.Parse(lineIn);
			ChannelGroups.Add(chg);
			Members.Add(chg);
			myCentiseconds = Math.Max(myCentiseconds, chg.Centiseconds);
			return chg;
		}

		public CosmicDevice ParseCosmicDevice(string lineIn)
		{
			CosmicDevice cos = new CosmicDevice("");
			cos.SetParentSeq(this);
			cos.Members.SetParentSequence(this);
			cos.SetIndex(CosmicDevices.Count);
			cos.Parse(lineIn);
			CosmicDevices.Add(cos);
			Members.Add(cos);
			myCentiseconds = Math.Max(myCentiseconds, cos.Centiseconds);
			return cos;
		}

		public ChannelGroup CreateChannelGroup(string theName)
		{
			// Does NOT check to see if a group with this name already exists
			// Therefore, allows for duplicate group names (But they will have different SavedIndexes)
			ChannelGroup chg;
			chg = new ChannelGroup(theName, utils.UNDEFINED);
			int newSI = AssignNextSavedIndex(chg);
			chg.SetParentSeq(this);
			chg.Members.SetParentSequence(this);
			chg.Members.owner = chg;
			chg.Centiseconds = Centiseconds;
			chg.SetIndex(ChannelGroups.Count);
			ChannelGroups.Add(chg);
			Members.Add(chg);
			myCentiseconds = Math.Max(myCentiseconds, chg.Centiseconds);

			return chg;
		}

		public CosmicDevice CreateCosmicDevice(string theName)
		{
			// Does NOT check to see if a device with this name already exists
			// Therefore, allows for duplicate device names (But they will have different SavedIndexes)
			CosmicDevice dev;
			dev = new CosmicDevice(theName, utils.UNDEFINED);
			int newSI = AssignNextSavedIndex(dev);
			dev.SetParentSeq(this);
			dev.Members.SetParentSequence(this);
			dev.Members.owner = dev;
			dev.Centiseconds = Centiseconds;
			dev.SetIndex(CosmicDevices.Count);
			CosmicDevices.Add(dev);
			Members.Add(dev);
			myCentiseconds = Math.Max(myCentiseconds, dev.Centiseconds);

			return dev;
		}

		public Track ParseTrack(string lineIn)
		{
			Track tr = new Track("");
			tr.SetParentSeq(this);
			tr.Members.SetParentSequence(this);
			tr.SetIndex(Tracks.Count);
			Tracks.Add(tr);
			tr.Parse(lineIn);
			if (myAltSavedIndex > utils.UNDEFINED)
			{
				// AltSavedIndex has been temporarily set to the track's TimingGrid's SaveID
				// So get the specified TimingGrid by it's SaveID
				//IMember tg = Members.bySaveID[tr.AltSavedIndex];
				TimingGrid tg = null;
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
				tr.timingGrid = (TimingGrid)tg;
				// Clear the AltSavedIndex which was temporarily holding the SaveID of the TimingGrid
				tr.AltSavedIndex = utils.UNDEFINED;
			}
			myCentiseconds = Math.Max(myCentiseconds, tr.Centiseconds);
			Members.Add(tr);
			return tr;
		}

		public Track CreateTrack(string theName)
		{
			// Does NOT check to see if a track with this name already exists
			// Therefore, allows for duplicate track names (But they will have different track numbers)
			Track tr = new Track(theName);
			tr.SetParentSeq(this);
			tr.Members.SetParentSequence(this);
			tr.Members.owner = tr;
			tr.Centiseconds = Centiseconds;
			tr.SetIndex(Tracks.Count);
			Tracks.Add(tr);
			//tr.TrackNumber = Tracks.Count;
			if (TimingGrids.Count > 0)
			{
				//! Is this gonna cause a problem later?
				tr.timingGrid = TimingGrids[0];
			}
			myCentiseconds = Math.Max(myCentiseconds, tr.Centiseconds);
			Members.Add(tr);
			return tr;
		}

		public TimingGrid ParseTimingGrid(string lineIn)
		{
			TimingGrid tg = new TimingGrid("");
			tg.SetParentSeq(this);
			//int newSI = AssignNextSaveID(tg);
			//tg.SaveID = newSI;
			tg.SetIndex(TimingGrids.Count);
			TimingGrids.Add(tg);
			tg.Parse(lineIn);
			//Members.Add(tg);
			return tg;
		}

		// Sequence4.CreateTimingGrid(name)
		public TimingGrid CreateTimingGrid(string theName)
		{
#if DEBUG
			string msg = "Sequence4.CreateTimingGrid(" + theName + ")";
			Debug.WriteLine(msg);
#endif
			// Does NOT check to see if a grid with this name already exists
			// Therefore, allows for duplicate grid names (But they will have different SaveIDs)
			TimingGrid tg = new TimingGrid(theName);
			tg.SetParentSeq(this);
			int newSI = AssignNextSaveID(tg);
			tg.Centiseconds = Centiseconds;
			tg.SetIndex(TimingGrids.Count);
			TimingGrids.Add(tg); // Handled in Members.Add called from AssignNextSaveID
			//Members.Add(tg);
			return tg;
		}

		public void MakeDirty()
		{
			dirty = true;
			info.lastModified = DateTime.Now; //.ToString("MM/dd/yyyy hh:mm:ss tt");
		}

		private int AssignNextSavedIndex(IMember thePart)
		{
			if (thePart.SavedIndex < 0)
			{
				int newSI = Members.HighestSavedIndex + 1;
				thePart.SetSavedIndex(newSI);
				Members.Add(thePart);
			}
			return thePart.SavedIndex;
		}

		private int AssignNextAltSavedIndex(IMember thePart)
		{
			if (thePart.AltSavedIndex < 0)
			{
				int newASI = Members.altHighestSavedIndex + 1;
				thePart.AltSavedIndex = newASI;
				// May cause out of bounds exception, might need to add instead
				Members.byAltSavedIndex[thePart.AltSavedIndex] = thePart;
				Members.altHighestSavedIndex = newASI;
			}
			return thePart.AltSavedIndex;
		}

		private int AssignNextSaveID(TimingGrid theGrid)
		{
			if (theGrid.SaveID < 0)
			{
				int newSI = Members.HighestSaveID + 1;
				theGrid.SaveID = newSI;
				Members.Add(theGrid);
			}
			return theGrid.SaveID;
		}

		private int AssignNextAltSaveID(TimingGrid theGrid)
		{
			if (theGrid.AltSavedIndex < 0)
			{
				int newASI = Members.altHighestSaveID + 1;
				theGrid.AltSaveID = newASI;
				Members.byAltSaveID.Add(theGrid);
				Members.altHighestSaveID = newASI;
			}
			return theGrid.AltSaveID;
		}

		public IMember Clone()
		{
			return this.Clone(Name);
		}

		public IMember Clone(string newName)
		{
			Sequence4 seqOut = new Sequence4();

			seqOut.animation = animation.Clone();
			seqOut.animation.parentSequence = seqOut;
			seqOut.Centiseconds = myCentiseconds;
			seqOut.filename = filename;
			seqOut.lineCount = lineCount;
			seqOut.info.filename = newName;
			seqOut.SequenceType = SequenceType;
			seqOut.videoUsage = videoUsage;
			for (int idx = 0; idx < Members.bySavedIndex.Count; idx++)
			{
				MemberType mt = Members.bySavedIndex[idx].MemberType;
				if (mt == MemberType.Channel)
				{
					Channel newCh = (Channel)Members.bySavedIndex[idx].Clone();
					seqOut.Members.Add(newCh);
				}
			}


			return seqOut;
		}

		public void MoveTrack(int oldPosition, int newPosition)
		{
			// Sanity Checks
			if ((oldPosition >= 0) ||
				(oldPosition < Tracks.Count) ||
				(newPosition >= 0) ||
				(newPosition <= Tracks.Count) ||
				(newPosition != oldPosition))
			{
				List<Track> tracksNew = new List<Track>();
				int newIndex = 0;
				for (int i=0; i< Tracks.Count; i++)
				{
					if (i != oldPosition)
					{
						Tracks[i].SetIndex(newIndex);
						tracksNew.Add(Tracks[i]);
					}
					if (i == newPosition)
					{
						Tracks[oldPosition].SetIndex(newIndex);
						tracksNew.Add(Tracks[oldPosition]);
					}
					newIndex++;
				}
				Tracks = tracksNew;
			}
		} // End MoveTrack


		// END SEQUENCE CLASS
	} // end sequence class
} // end namespace LORUtils