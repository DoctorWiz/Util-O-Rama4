using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace LORUtils
{
	public partial class Sequence4 : SeqPart, IComparable<SeqPart>
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
		public const string STARTtracks = "<Tracks>";
		public const string STARTgrids = "<TimingGrids>";
		public const string TABLEloopLevels = "loopLevels";
		public const string TABLEloopLevel = "loopLevel";
		public const string STARTloops = "<loopLevels>";
		public const string TABLEanimation = "animation";

		private const string STARTsequence = utils.STFLD + TABLEsequence + Info.FIELDsaveFileVersion;
		private const string STARTconfig = utils.STFLD + TABLEchannelConfig + Info.FIELDchannelConfigFileVersion;
		private const string STARTeffect = utils.STFLD + TABLEeffect + utils.FIELDtype;
		private const string STARTchannel = utils.STFLD + utils.TABLEchannel + utils.FIELDname;
		private const string STARTrgbChannel = utils.STFLD + TABLErgbChannel + utils.SPC;
		private const string STARTchannelGroup = utils.STFLD + TABLEchannelGroupList + utils.SPC;
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

		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int mySavedIndex = utils.UNDEFINED;
		private int myAltSavedIndex = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;

		public PartsCollection Children;
		private StreamWriter writer;
		private string lineOut = ""; // line to be Written out, gets modified if necessary
																 //private int curSavedIndex = 0;
		public SequenceType sequenceType = SequenceType.Undefined;
		public List<Channel> Channels = new List<Channel>();
		public List<RGBchannel> RGBchannels = new List<RGBchannel>();
		public List<ChannelGroup> ChannelGroups = new List<ChannelGroup>();
		private int[] altSaveIDs = null;
		public List<TimingGrid> TimingGrids = new List<TimingGrid>();
		public List<Track> Tracks = new List<Track>();
		public Animation animation = null;
		public Info info = null;
		public int lineCount = 0;
		public bool dirty = false;
		private string tempFileName;
		//private static string tempPath = "C:\\Windows\\Temp\\"; // Gets overwritten with X:\Username\AppData\Roaming\Util-O-Rama\
		private static string tempWorkPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\UtilORama\\";
		private string newFilename;
		public int errorStatus = 0;
		//public string animationInfo = "";
		public int videoUsage = 0;

		public string Name
		{
			get
			{
				return myName;
			}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
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
				}
			}
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

		public TableType TableType
		{
			get
			{
				return TableType.Sequence;
			}
		}

		public int CompareTo(SeqPart other)
		{
			int result = 0;
			if (parentSequence.Children.sortMode == PartsCollection.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Children.sortMode == PartsCollection.SORTbyName)
				{
					if (myName == "")
					{
						myName = Path.GetFileName(info.filename);
					}
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Children.sortMode == PartsCollection.SORTbyAltSavedIndex)
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
			// Implemented primarily for compatibility with 'SeqPart' interface
			//TODO: make this return something, say maybe the first few lines of the file...?
			return ret;
		}

		public override string ToString()
		{
			return myName;
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





		public Sequence4()
		{
			//! DEFAULT CONSTRUCTOR
			info = new Info(this);
			animation = new Animation(this);
			Children = new PartsCollection(this);
			Children.parentSequence = this;
		}

		public Sequence4(string theFilename)
		{
			Children = new PartsCollection(this);
			Children.parentSequence = this;
			ReadSequenceFile(theFilename);
		}



		//////////////////////////////////////////////////
		//                                             //
		//   * *    R E A D   S E Q U E N C E   * *   //
		//                                           //
		//////////////////////////////////////////////
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
			const string ERRproc = " in Sequence:ReadSequence(";
			const string ERRgrp = "), on Line #";
			const string ERRitem = ", at position ";
			const string ERRline = ", Code Line #";
			SequenceType st = SequenceType.Undefined;
			string creation = "";
			string modification = "";

			Channel lastChannel = null;
			RGBchannel lastRGBchannel = null;
			ChannelGroup lastGroup = null;
			Track lastTrack = null;
			TimingGrid lastGrid = null;
			LoopLevel lastll = null;
			AnimationRow lastAniRow = null;

			string ext = Path.GetExtension(existingFileName).ToLower();
			if (ext == ".lms") st = SequenceType.Musical;
			if (ext == ".las") st = SequenceType.Animated;
			if (ext == ".lcc") st = SequenceType.ChannelConfig;
			sequenceType = st;

			Clear(true);

			info.file_accessed = File.GetLastAccessTime(existingFileName);
			info.file_created = File.GetCreationTime(existingFileName);
			info.file_modified = File.GetLastWriteTime(existingFileName);



			//try
			//{
			StreamReader reader = new StreamReader(existingFileName);

			// Check for items in the order from most likely item to least likely
			// Effects, Channels,  RGBchannels, Groups, Tracks...

			// Sanity Check #1A, does it have ANY lines?
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
					xmlInfo = lineIn;
					// Sanity Check #1B, does it have at least 2 lines?
					if (!reader.EndOfStream)
					{
						lineIn = reader.ReadLine();
						// Sanity Check #3, is it a sequence?
						if ((st == SequenceType.Musical) || (st == SequenceType.Animated))
						{
							li = lineIn.IndexOf(STARTsequence);
						}
						if (st == SequenceType.ChannelConfig)
						{
							li = lineIn.IndexOf(STARTconfig);
						}
						if (li != 0)
						{
							errorStatus = 102;
						}
						else
						{
							info = new Info(this, lineIn);
							creation = info.createdAt;

							// Save this for later, as they will get changed as we populate the file
							modification = info.lastModified;
							info.filename = existingFileName;

							myName = Path.GetFileName(existingFileName);
							info.xmlInfo = xmlInfo;
							// Sanity Checks #4A and 4B, does it have a 'SaveFileVersion' and is it '14'
							//   (SaveFileVersion="14" means it cane from LOR Sequence Editor ver 4.x)
							if (info.saveFileVersion != 14)
							{
								errorStatus = 114;
							}
							else
							{
								// All sanity checks passed
								// * PARSE LINES
								while ((lineIn = reader.ReadLine()) != null)
								{
									lineCount++;
									//try
									//{
									//! Effects
									if (noEffects)
									{
										li = utils.UNDEFINED;
									}
									else
									{
										li = lineIn.IndexOf(STARTeffect);
									}
									if (li > 0)
									{
										while (li > 0)
										{
											lastChannel.AddEffect(lineIn);

											lineIn = reader.ReadLine();
											lineCount++;
											li = lineIn.IndexOf(STARTeffect);
										}
									}
									else // Not an Effect
									{
										//! Timings
										li = lineIn.IndexOf(STARTtiming);
										if (li > 0)
										{
											int t = utils.getKeyValue(lineIn, utils.FIELDcentiseconds);
											lastGrid.AddTiming(t);
										}
										else // Not a regular channel
										{
											//! Regular Channels
											li = lineIn.IndexOf(STARTchannel);
											if (li > 0)
											{
												lastChannel = ParseChannel(lineIn);
											}
											else // Not a regular channel
											{
												//! RGB Channels
												li = lineIn.IndexOf(STARTrgbChannel);
												if (li > 0)
												{
													lastRGBchannel = ParseRGBchannel(lineIn);
													lineIn = reader.ReadLine();
													lineCount++;
													lineIn = reader.ReadLine();
													lineCount++;

													// RED
													int csi = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
													Channel ch = (Channel)Children.bySavedIndex[csi];
													lastRGBchannel.redChannel = ch;
													ch.rgbChild = RGBchild.Red;
													ch.rgbParent = lastRGBchannel;
													lineIn = reader.ReadLine();
													lineCount++;

													// GREEN
													csi = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
													ch = (Channel)Children.bySavedIndex[csi];
													lastRGBchannel.grnChannel = ch;
													ch.rgbChild = RGBchild.Green;
													ch.rgbParent = lastRGBchannel;
													lineIn = reader.ReadLine();
													lineCount++;

													// BLUE
													csi = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
													ch = (Channel)Children.bySavedIndex[csi];
													lastRGBchannel.bluChannel = ch;
													ch.rgbChild = RGBchild.Blue;
													ch.rgbParent = lastRGBchannel;

												}
												else  // Not an RGB Channel
												{
													//! Channel Groups
													li = lineIn.IndexOf(STARTchannelGroup);
													if (li > 0)
													{
														lastGroup = ParseChannelGroup(lineIn);
														li = lineIn.IndexOf(utils.ENDFLD);
														if (li < 0)
														{
															lineIn = reader.ReadLine();
															lineCount++;
															lineIn = reader.ReadLine();
															lineCount++;
															li = lineIn.IndexOf(TABLEchannelGroup + utils.FIELDsavedIndex);
															while (li > 0)
															{
																int isl = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
																lastGroup.Children.Add(Children.bySavedIndex[isl]);
																//savedIndexes[isl].parents.Add(lastGroup.SavedIndex);

																lineIn = reader.ReadLine();
																lineCount++;
																li = lineIn.IndexOf(TABLEchannelGroup + utils.FIELDsavedIndex);
															}
														}
													}
													else // Not a ChannelGroup
													{
														//! Track Items
														li = lineIn.IndexOf(STARTtrackItem);
														if (li > 0)
														{
															int si = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
															lastTrack.Children.Add(Children.bySavedIndex[si]);
														}
														else // Not a regular channel
														{
															//! Tracks
															li = lineIn.IndexOf(STARTtrack);
															if (li > 0)
															{
																lastTrack = ParseTrack(lineIn);
																//for (int tg = 0; tg < TimingGrids.Count; tg++)
																//{
																//lastTrack.timingGrid == Children.bySaveID[TimingGrids[tg].SaveID];
																//TODO: Assign Timing Grid!!!!
																//{
																//	lastTrack.timingGridObjIndex = tg;
																//	tg = TimingGrids.Count; // break
																//}
																//}
																li = lineIn.IndexOf(utils.ENDFLD);
																if (li < 0)
																{
																	lineIn = reader.ReadLine();
																	lineCount++;
																	lineIn = reader.ReadLine();
																	lineCount++;
																	li = lineIn.IndexOf(STARTtrackItem);
																	while (li > 0)
																	{
																		int isi = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
																		//lastTrack.itemSavedIndexes.Add(isi);
																		lastTrack.Children.Add(Children.bySavedIndex[isi]);
																		//savedIndexes[isi].parents.Add(-100 - Tracks.Count);

																		lineIn = reader.ReadLine();
																		lineCount++;
																		li = lineIn.IndexOf(STARTtrackItem);
																	}
																}
															} // end if a track
															else // not a track
															{
																//! Timing Grids
																li = lineIn.IndexOf(STARTtimingGrid);
																if (li > 0)
																{
																	lastGrid = ParseTimingGrid(lineIn);
																	if (lastGrid.type == TimingGridType.Freeform)
																	{
																		lineIn = reader.ReadLine();
																		lineCount++;
																		li = lineIn.IndexOf(STARTgridItem);
																		while (li > 0)
																		{
																			int gpos = utils.getKeyValue(lineIn, utils.FIELDcentisecond);
																			lastGrid.AddTiming(gpos);
																			lineIn = reader.ReadLine();
																			lineCount++;
																			li = lineIn.IndexOf(STARTgridItem);
																		}
																	}
																}
																else // Not a timing grid
																{
																	//! Loop Levels
																	li = lineIn.IndexOf(STARTloopLevel);
																	if (li > 0)
																	{
																		lastll = lastTrack.AddLoopLevel(lineIn);
																	}
																	else // not a loop level
																	{
																		//! Loops
																		li = lineIn.IndexOf(STARTloop);
																		if (li > 0)
																		{
																			lastll.AddLoop(lineIn);
																		}
																		else // not a loop
																		{
																			//! Animation Rows
																			li = lineIn.IndexOf(STARTaniRow);
																			if (li > 0)
																			{
																				lastAniRow = animation.AddRow(lineIn);
																			}
																			else
																			{
																				//! Animation Columns
																				li = lineIn.IndexOf(STARTaniCol);
																				if (li > 1)
																				{
																					lastAniRow.AddColumn(lineIn);
																				} // end animationColumn
																				else
																				{
																					//! Animation
																					li = lineIn.IndexOf(utils.STFLD + TABLEanimation + utils.SPC);
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
													} // end ChannelGroup (or not)
												} // end RGBchannel (or not)
											} // end regular Channel (or not)
										} // end timing (or not)
									} // end Effect (or not)
										/*
							} // end 2nd Try
									catch (Exception ex)
									{
										StackTrace st = new StackTrace(ex, true);
										StackFrame sf = st.GetFrame(st.FrameCount - 1);
										string emsg = ex.ToString();
										emsg += ERRproc + existingFileName + ERRgrp + lineCount.ToString() + ERRitem + li.ToString();
										emsg += ERRline + sf.GetFileLineNumber();
#if DEBUG
										System.Diagnostics.Debugger.Break();
#endif
										utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
									} // end catch
									*/
								} // end while lines remain
							} // end SaveFileVersion = 14
						} // end second line is sequence info
					} // end has a second line
				} // end first line was xml info
			} // end has a first line


			reader.Close();
			/*
		} // end try
			catch (Exception ex)
			{
				StackTrace st = new StackTrace(ex, true);
				StackFrame sf = st.GetFrame(st.FrameCount - 1);
				string emsg = ex.ToString();
				emsg += ERRproc + existingFileName + ERRgrp + "none";
				emsg += ERRline + sf.GetFileLineNumber();
#if DEBUG
				System.Diagnostics.Debugger.Break();
#endif
				utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
			} // end catch
			*/

			Children.ReIndex();

			if (errorStatus <= 0)
			{
				info.filename = existingFileName;
				//! for debugging
				//string sMsg = summary();
				//MessageBox.Show(sMsg, "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			// Restore these to the values we captured when first reading the file info header
			info.createdAt = creation;
			info.lastModified = modification;

			return errorStatus;
		} // end ReadSequenceFile



		////////////////////////////////////////////////////
		//                                               //
		//   * *    W R I T E   S E Q U E N C E   * *   //
		//                                             //
		////////////////////////////////////////////////

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
			int altSI = utils.UNDEFINED;
			updatedTrack[] updatedTracks = new updatedTrack[Tracks.Count];
			//bySavedIndex.altHighestSavedIndex = utils.UNDEFINED;
			//bySavedIndex.altSaveID = utils.UNDEFINED;
			//altSavedIndexes = null;
			Children.ResetWritten();
			//Array.Resize(ref altSavedIndexes, highestSavedIndex + 3);
			//Array.Resize(ref altSaveIDs, TimingGrids.Count + 1);
			string ext = Path.GetExtension(newFileName).ToLower();
			bool channelConfig = false;
			if (ext.CompareTo(".lcc") == 0) channelConfig = true;
			if (channelConfig) noEffects = true;
			//TODO: implement channelConfig flag to write just a channel config file

			// Clear any 'Written' flags from a previous save
			ClearWrittenFlags();


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
					int asi = Children.GetNextAltSaveID(theTrack.timingGrid);
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
						int asi = Children.GetNextAltSaveID(theGrid);
						theGrid.AltSaveID = asi;
						//altSaveIDs[tg] = altSaveID;
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
					WriteItems(theTrack.Children, selectedOnly, noEffects, TableType.Items);
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
					WriteTrack(theTrack, selectedOnly);
				}
			}
			lineOut = utils.LEVEL1 + utils.FINTBL + TABLEtrack + utils.PLURAL + utils.FINFLD;
			writer.WriteLine(lineOut);

			WriteSequenceClose();

			errorStatus = RenameTempFile(newFileName);
			if (filename.Length < 3) filename = newFileName;

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
			int altSI = Children.GetNextAltSavedIndex(theChannel.SavedIndex);
			lineOut = theChannel.LineOut(noEffects);
			writer.WriteLine(lineOut);
			//theChannel.Written = true;
			return theChannel.AltSavedIndex;
		}

		private int WriteRGBchannel(RGBchannel theRGBchannel)
		{
			return WriteRGBchannel(theRGBchannel, false, false, TableType.Items);
		}

		private int WriteRGBchannel(RGBchannel theRGBchannel, bool selectedOnly, bool noEffects, TableType itemTypes)
		{
			if (!theRGBchannel.Written)
			{
				if ((!selectedOnly) || (theRGBchannel.Selected))
				{
					if ((itemTypes == TableType.Items) || (itemTypes == TableType.Items) || (itemTypes == TableType.Channel))
					{
						//theRGBchannel.LineOut(selectedOnly, noEffects, TableType.Channel);
						//lineOut = theRGBchannel.redChannel.LineOut(noEffects);
						//lineOut += utils.CRLF + theRGBchannel.grnChannel.LineOut(noEffects);
						//lineOut += utils.CRLF + theRGBchannel.bluChannel.LineOut(noEffects);
						WriteChannel(theRGBchannel.redChannel, noEffects);
						WriteChannel(theRGBchannel.grnChannel, noEffects);
						WriteChannel(theRGBchannel.bluChannel, noEffects);
						//writer.WriteLine(lineOut);
					}

					if ((itemTypes == TableType.Items) || (itemTypes == TableType.Items) || (itemTypes == TableType.RGBchannel))
					{
						//theRGBchannel.AltSavedIndex = bySavedIndex.GetNextAltSavedIndex(theRGBchannel.SavedIndex);
						int altSI = Children.GetNextAltSavedIndex(theRGBchannel.SavedIndex);
						lineOut = theRGBchannel.LineOut(selectedOnly, noEffects, TableType.RGBchannel);
						writer.WriteLine(lineOut);
						//theRGBchannel.Written = true;
					}
				}
			}
			return theRGBchannel.AltSavedIndex;
		} // end writergbChannel

		private int WriteChannelGroup(ChannelGroup theGroup)
		{
			return WriteChannelGroup(theGroup, false, false, TableType.Items);
		}

		private int WriteChannelGroup(ChannelGroup theGroup, bool selectedOnly, bool noEffects, TableType itemTypes)
		{
			List<int> altSIs = new List<int>();
			if (!theGroup.Written)
			{
				if ((!selectedOnly) || (theGroup.Selected))
				{
					if ((itemTypes == TableType.Items) ||
						(itemTypes == TableType.Channel) ||
						(itemTypes == TableType.RGBchannel))
					{
						altSIs = WriteItems(theGroup.Children, selectedOnly, noEffects, itemTypes);
					}

					if ((itemTypes == TableType.Items) ||
						(itemTypes == TableType.ChannelGroup))
					{
						//theGroup.AltSavedIndex = bySavedIndex.GetNextAltSavedIndex(theGroup.SavedIndex);
						//if (altSIs.Count > 0)
						//{
						int altSI = Children.GetNextAltSavedIndex(theGroup.SavedIndex);
						theGroup.AltSavedIndex = altSI;
						lineOut = theGroup.LineOut(selectedOnly);
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

		private int WriteTrack(Track theTrack, bool selectedOnly)
		{
			lineOut = theTrack.LineOut(selectedOnly);
			int siOld = utils.UNDEFINED;
			int siAlt = utils.UNDEFINED;


			writer.WriteLine(lineOut);
			return theTrack.AltSavedIndex;
		}

		private List<int> WriteItems(PartsCollection itemIDs, bool selectedOnly, bool noEffects, TableType itemTypes)
		{
			int altSaveIndex = utils.UNDEFINED;
			List<int> altSIs = new List<int>();
			string itsName = "";  //! for debugging

			//Identity id = null;

			foreach (SeqPart item in itemIDs.Items)
			{
				//id = Children.bySavedIndex[si];
				itsName = item.Name;
				if ((!selectedOnly) || (item.Selected))
				{
					if (!item.Written)
					{
						if (item.TableType == TableType.Channel)
						{
							// Prevents unnecessary processing of Channels which have already been Written, during RGB channel and group processing
							if ((itemTypes == TableType.Items) || (itemTypes == TableType.Items) || (itemTypes == TableType.Channel))
							{
								altSaveIndex = WriteChannel((Channel)item, noEffects);
								altSIs.Add(altSaveIndex);
							}
						}
						else
						{
							if (item.TableType == TableType.RGBchannel)
							{
								RGBchannel theRGB = (RGBchannel)item;
								if ((itemTypes == TableType.Items) || (itemTypes == TableType.Items) || (itemTypes == TableType.Channel) || (itemTypes == TableType.RGBchannel))
								{
									altSaveIndex = WriteRGBchannel(theRGB, selectedOnly, noEffects, itemTypes);
									altSIs.Add(altSaveIndex);
								}
							}
							else
							{
								if (item.TableType == TableType.ChannelGroup)
								{
									//if (itemTypes == TableType.channelGroup)
									//if ((itemTypes == TableType.None) ||
									//    (itemTypes == TableType.rgbChannel) ||
									//    (itemTypes == TableType.channel) ||
									//    (itemTypes == TableType.channelGroup))
									// Type NONE actually means ALL in this case
									//{
									if (item.Name.IndexOf("inese 27") > 0)
									{
										int q = 5;
									}



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

		public int WriteItem(int SavedIndex)
		{
			return WriteItem(SavedIndex, false, false, TableType.Items);
		}

		public int WriteItem(int SavedIndex, bool selectedOnly, bool noEffects, TableType theType)
		{
			int ret = utils.UNDEFINED;

			SeqPart part = Children.bySavedIndex[SavedIndex];
			if (!part.Written)
			{
				if (!selectedOnly || part.Selected)
				{
					TableType itemType = part.TableType;
					if (itemType == TableType.Channel)
					{
						Channel theChannel = (Channel)part;
						if ((theType == TableType.Channel) || (theType == TableType.Items))
						{
							ret = WriteChannel(theChannel, noEffects);
						} // end if type
					} // end if Channel
					else
					{
						if (itemType == TableType.RGBchannel)
						{
							RGBchannel theRGB = (RGBchannel)part;
							ret = WriteRGBchannel(theRGB, selectedOnly, noEffects, theType);
						} // end if RGBchannel
						else
						{
							if (itemType == TableType.ChannelGroup)
							{
								ChannelGroup theGroup = (ChannelGroup)part;
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
			int altSI = Children.GetNextAltSaveID(theGrid);
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
		}

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

		private struct match
		{
			public string Name;
			public int savedIdx;
			public TableType type;
			public int itemIdx;
		}

		// CONSTRUCTOR
		//public void Sequence()
		//{
		//}

		public static string DefaultSequencesPath
		{
			get
			{
				return utils.DefaultNonAudioPath;
			}
		}

		public Channel FOOAddChannel(string lineIn)
		{
			Channel newChannel = new Channel(lineIn);
			newChannel.SetParentSeq(this);
			int si = FOOAddChannel(newChannel);
			return newChannel;
		} // end AddChannel

		public int FOOAddChannel(Channel newChannel)
		{
			//int retSI = utils.UNDEFINED;
			//bool alreadyAdded = false;
			newChannel.SetParentSeq(this);
			if (newChannel.SavedIndex < 0)
			{
				int newSI = Children.GetNextSavedIndex(newChannel);
				newChannel.SetSavedIndex(newSI);
			}
			else
			{
				// Already added?  Why does it already have a SavedIndex?
				// Happens during file read
			}
			//newChannel.myIndex = Channels.Count;
			Channels.Add(newChannel);
			Children.Add(newChannel);
			if (newChannel.Centiseconds > myCentiseconds)
			{
				myCentiseconds = newChannel.Centiseconds;
			}
			return newChannel.SavedIndex;
		}

		public RGBchannel FOOAddRGBchannel(string lineIn)
		{
			RGBchannel newRGB = new RGBchannel(lineIn);
			newRGB.SetParentSeq(this);
			int si = FOOAddRGBchannel(newRGB);
			return newRGB;
		}

		public int FOOAddRGBchannel(RGBchannel newRGB)
		{
			newRGB.SetParentSeq(this);
			if (newRGB.SavedIndex < 0)
			{
				int newSI = Children.GetNextSavedIndex(newRGB);
				newRGB.SetSavedIndex(newSI);
			}
			//newRGB.myIndex = RGBchannels.Count;
			RGBchannels.Add(newRGB);
			Children.Add(newRGB);
			if (newRGB.Centiseconds > myCentiseconds)
			{
				myCentiseconds = newRGB.Centiseconds;
			}
			return newRGB.SavedIndex;
		} // end AddChannel

		public ChannelGroup FOOAddChannelGroup(string lineIn)
		{
			ChannelGroup newGroup = new ChannelGroup(lineIn);
			newGroup.SetParentSeq(this);
			FOOAddChannelGroup(newGroup);
			return newGroup;
		}

		public int FOOAddChannelGroup(ChannelGroup newGroup)
		{
			newGroup.SetParentSeq(this);
			newGroup.Children.parentSequence = this;
			if (newGroup.SavedIndex < 0)
			{
				int newSI = Children.GetNextSavedIndex(newGroup);
				newGroup.SetSavedIndex(newSI);
			}
			//newGroup.myIndex = ChannelGroups.Count;
			ChannelGroups.Add(newGroup);
			Children.Add(newGroup);
			if (newGroup.Centiseconds > myCentiseconds)
			{
				myCentiseconds = newGroup.Centiseconds;
			}
			return newGroup.SavedIndex;
		}

		//TODO: add RemoveChannel, RemoveRGBchannel, RemoveChannelGroup, and RemoveTrack procedures

		public Track FOOAddTrack(string lineIn)
		{
			Track newTrack = new Track(lineIn);
			newTrack.SetParentSeq(this);
			FOOAddTrack(newTrack);
			return newTrack;
		}

		public int FOOAddTrack(Track newTrack)
		{
			newTrack.SetParentSeq(this);
			newTrack.Children.parentSequence = this;
			if (newTrack.SavedIndex < 0)
			{
				int newSI = Tracks.Count;
				newTrack.SetSavedIndex(newSI);
			}
			//newTrack.myIndex = Tracks.Count;
			newTrack.SetSavedIndex(Tracks.Count);
			Tracks.Add(newTrack);
			Children.Add(newTrack);
			if (newTrack.Centiseconds > myCentiseconds)
			{
				myCentiseconds = newTrack.Centiseconds;
			}
			return Tracks.Count - 1;
		}

		public TimingGrid FOOAddTimingGrid(string lineIn)
		{
			TimingGrid newGrid = new TimingGrid(lineIn);
			newGrid.SetParentSeq(this);
			FOOAddTimingGrid(newGrid);
			return newGrid;
		}

		public int FOOAddTimingGrid(TimingGrid newGrid)
		{
			newGrid.SetParentSeq(this);
			if (newGrid.SavedIndex < 0)
			{
				int newSI = TimingGrids.Count;
				newGrid.SetSavedIndex(newSI);
			}
			//newGrid.myIndex = TimingGrids.Count;
			TimingGrids.Add(newGrid);
			Children.Add(newGrid);
			if (newGrid.timings.Count > 0)
			{
				if (newGrid.timings[newGrid.timings.Count - 1] > myCentiseconds)
				{
					myCentiseconds = newGrid.timings[newGrid.timings.Count - 1];
				}
			}
			while (Children.byAltSaveID.Count < (newGrid.SaveID + 1))
			{
				Children.byAltSaveID.Add(newGrid);
			}
			Children.byAltSaveID[newGrid.SaveID] = newGrid; ;

			return newGrid.SavedIndex;
		} // end AddChannel

		public int FOOAddItem(SeqPart newItem)
		{
			int retSI = utils.UNDEFINED;
			bool alreadyAdded = false;
			for (int i = 0; i < Children.Count; i++)
			{
				if (newItem.SavedIndex == Children.Items[i].SavedIndex)
				{
					//TODO: Using saved index, look up Name of item being added
					string sMsg = newItem.Name + " has already been added to this Sequence '" + myName + "'.";
					//DialogResult rs = MessageBox.Show(sMsg, "Channel Groups", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					if (System.Diagnostics.Debugger.IsAttached)
						//System.Diagnostics.Debugger.Break();
						//TODO: Make this just a warning, put "add" code below into an else block
						//TODO: Do the same with Tracks
						alreadyAdded = true;
					retSI = newItem.SavedIndex;
					i = Children.Count; // Break out of loop
				}
			}
			if (!alreadyAdded)
			{
				if (newItem.TableType == TableType.Channel)
				{
					Channel ch = (Channel)newItem;
					retSI = FOOAddChannel(ch);
				}
				else
				{
					if (newItem.TableType == TableType.RGBchannel)
					{
						RGBchannel rch = (RGBchannel)newItem;
						retSI = FOOAddRGBchannel(rch);
					}
					else
					{
						if (newItem.TableType == TableType.ChannelGroup)
						{
							ChannelGroup cg = (ChannelGroup)newItem;
							retSI = FOOAddChannelGroup(cg);
						}
						else
						{
							if (newItem.TableType == TableType.Track)
							{
								Track tr = (Track)newItem;
								retSI = FOOAddTrack(tr);
							}
							else
							{
								if (newItem.TableType == TableType.TimingGrid)
								{
									TimingGrid tg = (TimingGrid)newItem;
									retSI = FOOAddTimingGrid(tg);
								}
								else
								{
									string eMsg = "We should not have gotten here!";
								}
							}
						}
					}
				}
				Children.Add(newItem);
			}
			return newItem.SavedIndex;
		}



		public string GetItemName(int SavedIndex)
		{
			string nameOut = "";
			SeqPart part = Children.bySavedIndex[SavedIndex];
			if (part != null)
			{
				nameOut = part.Name;
			}
			return nameOut;
		}

		public void Clear(bool areYouReallySureYouWantToDoThis)
		{
			if (areYouReallySureYouWantToDoThis)
			{
				// Zero these out from any previous run
				lineCount = 0;
				Channels = new List<Channel>();
				RGBchannels = new List<RGBchannel>();
				ChannelGroups = new List<ChannelGroup>();
				Tracks = new List<Track>();
				TimingGrids = new List<TimingGrid>();
				Children = new PartsCollection(this);
				Children.parentSequence = this;
				myCentiseconds = 0;

				info = new Info(this);
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
			sMsg += "Highest Saved Index: " + Children.HighestSavedIndex.ToString() + utils.CRLF;
			return sMsg;
		}

		public int[] DuplicateNameCheck()
		{
			// returns null if no matches
			// if matches found, returns array with pairs of matches
			// Thus array size will always be factor of 2
			// with even numbers being a match to the odd number just after it
			// (or odd numbers being a match to the even number just before it)
			int[] ret = null;
			match[] matches = null;
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
					matches[ch].type = TableType.Channel;
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
					matches[q].type = TableType.RGBchannel;
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
					matches[q].type = TableType.ChannelGroup;
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
						// If they match, add 2 elements to the output array
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

		private void SortMatches(match[] matches, int low, int high)
		{
			int pivot_loc = (low + high) / 2;

			if (low < high)
				pivot_loc = PartitionMatches(matches, low, high);
			SortMatches(matches, low, pivot_loc - 1);
			SortMatches(matches, pivot_loc + 1, high);
		}

		private int PartitionMatches(match[] matches, int low, int high)
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

		private void SwapMatches(match[] matches, int idx1, int idx2)
		{
			match temp = matches[idx1];
			matches[idx1] = matches[idx2];
			matches[idx2] = temp;
			//MakeDirty();

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
				pos1 = lineIn.IndexOf("xml version=");
				if (pos1 > 0)
				{
					info.xmlInfo = lineIn;
				}
				pos1 = lineIn.IndexOf("saveFileVersion=");
				if (pos1 > 0)
				{
					info.Parse(lineIn);
				}
				pos1 = lineIn.IndexOf(utils.STFLD + utils.TABLEchannel + utils.FIELDname);
				if (pos1 > 0)
				{
					//channelsCount++;
				}
				pos1 = lineIn.IndexOf(utils.STFLD + TABLEeffect + utils.SPC);
				if (pos1 > 0)
				{
					//effectCount++;
				}
				if (Tracks.Count == 0)
				{
				}
				pos1 = lineIn.IndexOf(utils.STFLD + TABLEtimingGrid + utils.SPC);
				if (pos1 > 0)
				{
					//timingGridCount++;
				}
				pos1 = lineIn.IndexOf(utils.STFLD + TimingGrid.TABLEtiming + utils.SPC);
				if (pos1 > 0)
				{
					//gridItemCount++;
				}

				pos1 = lineIn.IndexOf(utils.FIELDsavedIndex);
				if (pos1 > 0)
				{
					curSavedIndex = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
					//if (curSavedIndex > bySavedIndex.highestSavedIndex)
					{
						//Children.highestSavedIndex = curSavedIndex;
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
			int pixNo = 0;
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
				pos1 = lineIn.IndexOf(utils.TABLEchannel + utils.ENDFLD);
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

					//si.PartType = TableType.channel;
					//si.objIndex = curChannel;
					//savedIndexes[curSavedIndex] = si;
					if (chwhich == 2)
					{ }
					chwhich++;
					chwhich %= 3;

				}

				// does this line mark the start of an Effect?
				pos1 = lineIn.IndexOf(TABLEeffect + utils.FIELDtype);
				if (pos1 > 0)
				{
					curEffect++;

					//DEBUG!
					if (curEffect > 638)
					{
						errorStatus = 1;
					}

					Effect ef = new Effect();
					ef.type = SeqEnums.enumEffect(utils.getKeyWord(lineIn, utils.FIELDtype));
					ef.startCentisecond = utils.getKeyValue(lineIn, utils.FIELDstartCentisecond);
					ef.endCentisecond = utils.getKeyValue(lineIn, utils.FIELDendCentisecond);
					ef.intensity = utils.getKeyValue(lineIn, utils.SPC + Effect.FIELDintensity);
					ef.startIntensity = utils.getKeyValue(lineIn, Effect.FIELDstartIntensity);
					ef.endIntensity = utils.getKeyValue(lineIn, Effect.FIELDendIntensity);
					Channels[curChannel].effects.Add(ef);
				}

				// does this line mark the start of a Timing Grid?
				pos1 = lineIn.IndexOf(utils.STFLD + TABLEtimingGrid + utils.SPC);
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
						pos1 = lineIn.IndexOf(TimingGrid.TABLEtiming + utils.FIELDcentisecond);
						while (pos1 > 0)
						{
							curGridItem++;
							int gpos = utils.getKeyValue(lineIn, utils.FIELDcentisecond);
							TimingGrids[curTimingGrid].AddTiming(gpos);

							lineIn = reader.ReadLine();
							pos1 = lineIn.IndexOf(TimingGrid.TABLEtiming + utils.FIELDcentisecond);
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

		private void WriteAnimation()
		{
			if (animation != null)
			{
				if (animation.rows > 0)
				{
					lineOut = animation.LineOut();
					writer.WriteLine(lineOut);
				}
			} // end if animation != null
		} // end writeAnimation

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
				for (int ti = 0; ti < Tracks[trk].Children.Count; ti++)
				{
					int si = Tracks[trk].Children.Items[ti].SavedIndex;
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
			TableType itemType = TableType.None; //savedIndexes[saveID].PartType;
			if (itemType == TableType.Channel)
			{
				lineOut = utils.LEVEL2 + utils.STFLD + utils.TABLEchannel + utils.ENDFLD;
				writer.WriteLine(lineOut);
				//WriteEffects(Channels[oi]);
				lineOut = utils.LEVEL2 + utils.FINTBL + utils.TABLEchannel + utils.ENDFLD;
				writer.WriteLine(lineOut);
			} // end if channel

			if (itemType == TableType.RGBchannel)
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

			if (itemType == TableType.ChannelGroup)
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
		// Wrappers for Children Find Functions

		public Channel FindChannel(int SavedIndex)
		{
			Channel ret = null;
			SeqPart part = Children.bySavedIndex[SavedIndex];
			if (part != null)
			{
				if (part.TableType == TableType.Channel)
				{
					ret = (Channel)part;
				}
			}
			return ret;
		} // end FindChannel

		public Channel FindChannel(string channelName)
		{
			Channel ret = null;
			SeqPart part = Children.FindByName(channelName, TableType.Channel);
			if (part != null)
			{
				ret = (Channel)part;
			}
			return ret;
		}

		public RGBchannel FindRGBchannel(int SavedIndex)
		{
			RGBchannel ret = null;
			SeqPart part = Children.bySavedIndex[SavedIndex];
			if (part != null)
			{
				if (part.TableType == TableType.RGBchannel)
				{
					ret = (RGBchannel)part;
				}
			}
			return ret;
		} // end FindrgbChannel

		public RGBchannel FindRGBchannel(string rgbChannelName)
		{
			RGBchannel ret = null;
			SeqPart part = Children.FindByName(rgbChannelName, TableType.RGBchannel);
			if (part != null)
			{
				ret = (RGBchannel)part;
			}
			return ret;
		}

		public ChannelGroup FindChannelGroup(int SavedIndex)
		{
			ChannelGroup ret = null;
			SeqPart part = Children.bySavedIndex[SavedIndex];
			if (part != null)
			{
				if (part.TableType == TableType.ChannelGroup)
				{
					ret = (ChannelGroup)part;
				}
			}
			return ret;
		} // end FindChannelGroup

		public ChannelGroup FindChannelGroup(string channelGroupName)
		{
			ChannelGroup ret = null;
			SeqPart part = Children.FindByName(channelGroupName, TableType.ChannelGroup);
			if (part != null)
			{
				ret = (ChannelGroup)part;
			}
			return ret;
		}

		public Track FindTrack(string trackName)
		{
			Track ret = null;
			SeqPart part = Children.FindByName(trackName, TableType.Track);
			if (part != null)
			{
				ret = (Track)part;
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
			return ret;
		}


		public TimingGrid FindTimingGrid(string timingGridName)
		{
			TimingGrid ret = null;
			SeqPart part = Children.FindByName(timingGridName, TableType.TimingGrid);
			if (part != null)
			{
				ret = (TimingGrid)part;
			}
			return ret;
		}
		#endregion

		private List<int> WriteChannelGroupItems(ChannelGroup theGroup, bool selectedOnly, bool noEffects, TableType itemTypes)
		{
			List<int> altSIs = new List<int>();
			int itemCount = 0;
			int si = utils.UNDEFINED;
			int altSaveIndex = utils.UNDEFINED;
			string itsName = "";  //! For debugging
														//Identity id = null;

			if ((!selectedOnly) || (theGroup.Selected))
			{
				foreach (SeqPart part in theGroup.Children.Items)
				{
					//id = Children.bySavedIndex[igi];
					if (part.TableType == TableType.Channel)
					{
						itsName = part.Name;
						if ((!selectedOnly) || (part.Selected))
						{
							// Prevents unnecessary processing of Channels which have already been Written, during RGB channel and group processing
							if ((itemTypes == TableType.Items) || (itemTypes == TableType.Channel))
							// Type NONE actually means ALL in this case
							{
								altSaveIndex = WriteChannel((Channel)part, noEffects);
								altSIs.Add(altSaveIndex);
							}
						}
					}
					else
					{
						if (part.TableType == TableType.RGBchannel)
						{
							itsName = part.Name;
							if ((!selectedOnly) || (part.Selected))
							{
								// prevents unnecessary processing of RGB Channels that have already been Written, during groupd processing
								if ((itemTypes == TableType.Items) || (itemTypes == TableType.RGBchannel) || (itemTypes == TableType.Channel))
								// Type NONE actually means ALL in this case
								{
									altSaveIndex = WriteRGBchannel((RGBchannel)part, selectedOnly, noEffects, itemTypes);
									altSIs.Add(altSaveIndex);
								}
							}
						}
						else
						{
							if (part.TableType == TableType.ChannelGroup)
							{
								itsName = part.Name;
								if ((!selectedOnly) || (part.Selected))
								{
									//if (itemTypes == TableType.channelGroup)
									//if ((itemTypes == TableType.None) ||
									//    (itemTypes == TableType.rgbChannel) ||
									//    (itemTypes == TableType.channel) ||
									//    (itemTypes == TableType.channelGroup))
									// Type NONE actually means ALL in this case
									//{
									altSaveIndex = WriteChannelGroup((ChannelGroup)part, selectedOnly, noEffects, itemTypes);
									altSIs.Add(altSaveIndex);
									//}
								}
							} // if channelgroup, or not
						} // if rgb channel, or not
					} // if regular channel, or not
				}
			}
			return altSIs;
		} // end writeChannelGroupItems

		public int SavedIndexIntegrityCheck()
		// Returns 0 if no problems found
		// number > 0 = number of problems
		{
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
					if (siCheck[Channels[ch].SavedIndex].TableType == TableType.None)
					{
						siCheck[Channels[ch].SavedIndex].TableType = TableType.Channel;
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
					if (siCheck[RGBchannels[rch].SavedIndex].TableType == TableType.None)
					{
						siCheck[RGBchannels[rch].SavedIndex].TableType = TableType.RGBchannel;
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
					if (siCheck[ChannelGroups[chg].SavedIndex].TableType == TableType.None)
					{
						siCheck[ChannelGroups[chg].SavedIndex].TableType = TableType.ChannelGroup;
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

			if (tot != Children.HighestSavedIndex + 1)
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
						// if match, store savedIndexes in pair of return values
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
						// if match, store savedIndexes in pair of return values
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
						// if match, store savedIndexes in pair of return values
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
						// if match, store savedIndexes in pair of return values
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
						// if match, store savedIndexes in pair of return values
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
		// Looks for duplicate names amongst regular Channels, RGB Channels, channel groups, and Tracks
		// Does not include timing grid names
		{
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
						// if match, store savedIndexes in pair of return values
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
			// looks for an EXACT match (see also: FuzzyFindName)
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
		// Could be a static method, but easier to work with if not
		{
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
			Children.Add(chan);
			myCentiseconds = Math.Max(myCentiseconds, chan.Centiseconds);
			MakeDirty();
			return chan;
		}

		public Channel CreateChannel(string theName)
		{
			// Does it already exist?
			Channel chan;
			SeqPart part;
			if (Children.byName.TryGetValue(theName, out part))
			{
				chan = (Channel)part;
			}
			else
			{
				chan = new Channel(theName, utils.UNDEFINED);
				int newSI = Children.GetNextSavedIndex(chan);
				chan.SetParentSeq(this);
				chan.Centiseconds = Centiseconds;
				chan.SetIndex(Channels.Count);
				Channels.Add(chan);
				Children.Add(chan);
				myCentiseconds = Math.Max(myCentiseconds, chan.Centiseconds);
				MakeDirty();
			}
			return chan;
		}

		public RGBchannel ParseRGBchannel(string lineIn)
		{
			RGBchannel rch = new RGBchannel("");
			rch.SetParentSeq(this);
			rch.SetIndex(RGBchannels.Count);
			RGBchannels.Add(rch);
			rch.Parse(lineIn);
			Children.Add(rch);
			myCentiseconds = Math.Max(myCentiseconds, rch.Centiseconds);
			MakeDirty();
			return rch;
		}

		public RGBchannel CreateRGBchannel(string theName)
		{
			// Does it already exist?
			RGBchannel rch;
			SeqPart part;
			if (Children.byName.TryGetValue(theName, out part))
			{
				rch = (RGBchannel)part;
			}
			else
			{
				rch = new RGBchannel(theName, utils.UNDEFINED);
				int newSI = Children.GetNextSavedIndex(rch);
				rch.SetParentSeq(this);
				rch.Centiseconds = Centiseconds;
				rch.SetIndex(RGBchannels.Count);
				RGBchannels.Add(rch);
				Children.Add(rch);
				myCentiseconds = Math.Max(myCentiseconds, rch.Centiseconds);
				MakeDirty();
			}
			return rch;
		}

		public ChannelGroup ParseChannelGroup(string lineIn)
		{
			ChannelGroup chg = new ChannelGroup("");
			chg.SetParentSeq(this);
			chg.SetIndex(ChannelGroups.Count);
			ChannelGroups.Add(chg);
			chg.Parse(lineIn);
			Children.Add(chg);
			myCentiseconds = Math.Max(myCentiseconds, chg.Centiseconds);
			MakeDirty();
			return chg;
		}

		public ChannelGroup CreateChannelGroup(string theName)
		{
			// Does it already exist?
			ChannelGroup chg;
			SeqPart part;
			if (Children.byName.TryGetValue(theName, out part))
			{
				chg = (ChannelGroup)part;
			}
			else
			{
				chg = new ChannelGroup(theName, utils.UNDEFINED);
				int newSI = Children.GetNextSavedIndex(chg);
				chg.SetParentSeq(this);
				chg.Centiseconds = Centiseconds;
				chg.SetIndex(ChannelGroups.Count);
				ChannelGroups.Add(chg);
				Children.Add(chg);
				myCentiseconds = Math.Max(myCentiseconds, chg.Centiseconds);
				MakeDirty();
			}
			return chg;
		}

		public Track ParseTrack(string lineIn)
		{
			Track tr = new Track("");
			tr.SetParentSeq(this);
			tr.SetIndex(Tracks.Count);
			Tracks.Add(tr);
			tr.Parse(lineIn);
			if (myAltSavedIndex > utils.UNDEFINED)
			{
				// AltSavedIndex has been temporarily set to the track's TimingGrid's SaveID
				// So get the specified TimingGrid by it's SaveID
				SeqPart tg = Children.bySaveID[tr.AltSavedIndex];
				// And assign it to the track
				tr.timingGrid = (TimingGrid)tg;
				// Clear the AltSavedIndex which was temoorarily holding the SaveID of the TimingGrid
				tr.AltSavedIndex = utils.UNDEFINED;
			}
			myCentiseconds = Math.Max(myCentiseconds, tr.Centiseconds);
			Children.Add(tr);
			MakeDirty();
			return tr;
		}

		public Track CreateTrack(string theName)
		{
			// Does it already exist?
			Track tr;
			SeqPart part;
			if (Children.byName.TryGetValue(theName, out part))
			{
				tr = (Track)part;
			}
			else
			{
				int newSI = Children.trackCount;
				tr = new Track(theName, newSI);
				tr.SetParentSeq(this);
				tr.Centiseconds = Centiseconds;
				tr.SetIndex(Tracks.Count);
				Tracks.Add(tr);
				myCentiseconds = Math.Max(myCentiseconds, tr.Centiseconds);
				Children.Add(tr);
				MakeDirty();
			}
			return tr;
		}

		public TimingGrid ParseTimingGrid(string lineIn)
		{
			TimingGrid tg = new TimingGrid("");
			tg.SetParentSeq(this);
			tg.SetIndex(TimingGrids.Count);
			TimingGrids.Add(tg);
			tg.Parse(lineIn);
			Children.Add(tg);
			MakeDirty();
			return tg;
		}

		public TimingGrid CreateTimingGrid(string theName)
		{
			// Does it already exist?
			TimingGrid tg;
			SeqPart part;
			if (Children.byName.TryGetValue(theName, out part))
			{
				tg = (TimingGrid)part;
			}
			else
			{
				int newSI = Children.GetNextSaveID();
				tg = new TimingGrid(theName, newSI);
				tg.SetParentSeq(this);
				tg.Centiseconds = Centiseconds;
				tg.SetIndex(TimingGrids.Count);
				TimingGrids.Add(tg);
				Children.Add(tg);
				MakeDirty();
			}
			return tg;
		}

		public void MakeDirty()
		{
			dirty = true;
			info.lastModified = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
		}



	} // end sequence class
} // end namespace LORUtils