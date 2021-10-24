using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FileHelper;

namespace LORUtils4
{
	public class LORVisualization4 : LORMemberBase4, iLORMember4, IComparable<iLORMember4>
	{
		public static readonly string TABLEvisualization = "LViz";
		public static readonly string TABLEdrawPoint = "DrawPoint";
		public static readonly string TABLEdrawPoints = "DrawPoints";
		public static readonly string TABLEdrawObject = "DrawObject";
		public static readonly string TABLElevel = "Level";
		public static readonly string TABLElevels = "Levels";
		public static readonly string TABLEitem = "Item";
		public static readonly string TABLEitems = "Items";
		public static readonly string TABLEassignedObject = "AssignedObject";
		public static readonly string TABLEsample = "Sample";
		public static readonly string TABLEassignedChannels = "AssignedChannels";
		public static readonly string TABLEsuperStarData = "SuperStar_Data";
		public static readonly string TABLEvizChannel = "Channel";

		public static readonly string FIELDvizID = " ID";
		public static readonly string FIELDbackColor = " Background_Color";
		public static readonly string FIELDreverseOrder = " SSReverseOrder";
		public static readonly string FIELDAssignedID = "AssignedObject ID";
		// Sequence Channels use "channel", "name" and "color" (Lower Case)
		// But Visualizations use "LORChannel4", "Name" and "Color" -- Arrrggggh!
		public static readonly string FIELDvizName = " Name";
		public static readonly string FIELDvizColor = " Color";

		private static readonly string STARTvisualization = lutils.STFLD + TABLEvisualization + LORSeqInfo4.FIELDlvizSaveFileVersion;
		private static readonly string STARTdrawPoint = lutils.STFLD + TABLEdrawPoint + FIELDvizID + lutils.FIELDEQ;
		private static readonly string STARTdrawObject = lutils.STFLD + TABLEdrawObject + FIELDvizID + lutils.FIELDEQ;
		private static readonly string STARTitem = lutils.STFLD + TABLEitem + FIELDvizID + lutils.FIELDEQ;
		private static readonly string STARTvizChannel = lutils.STFLD + TABLEvizChannel + FIELDvizID + lutils.FIELDEQ;
		private static readonly string STARTsample = lutils.STFLD + TABLEsample + FIELDbackColor + lutils.FIELDEQ;
		private static readonly string STARTsuperStarData = lutils.STFLD + TABLEsuperStarData + FIELDreverseOrder + lutils.FIELDEQ;
		private static readonly string STARTdrawPointsGroup = lutils.STFLD + TABLEdrawPoints + lutils.ENDTBL;
		private static readonly string ENDdrawPointsGroup = lutils.FINTBL + TABLEdrawPoints + lutils.ENDTBL;
		private static readonly string STARTassignedChannelsGroup = lutils.STFLD + TABLEassignedChannels + lutils.ENDTBL;
		private static readonly string ENDassignedChannelsGroup = lutils.FINTBL + TABLEassignedChannels + lutils.ENDTBL;
		private static readonly string STARTItemsGroup = lutils.STFLD + TABLEitems + lutils.ENDTBL;
		private static readonly string ENDItemsGroup = lutils.FINTBL + TABLEitems + lutils.ENDTBL;

		public int errorStatus = 0;
		public LORSeqInfo4 info = null;
		public LOROutput4 output = new LOROutput4();
		public int lineCount = 0;
		public List<LORVizItemGroup4> VizItemGroups = new List<LORVizItemGroup4>();
		public List<LORVizDrawObject4> VizDrawObjects = new List<LORVizDrawObject4>();
		// Contains ALL channels, even though they are sub-members to DrawObjects which are sub-members to ItemGroups
		public List<LORVizChannel4> VizChannels = new List<LORVizChannel4>();

		// Members contains the only objects directly below a visualization
		//   which is ItemGroups
		public LORMembership4 Members = null;
		// All Members contains ItemGroups, DrawObjects, and VizChannels
		public LORMembership4 AllMembers = null;
		//public List<Prop> Props = new List<Prop>();


		public LORVisualization4()
		{
			// I'm my own grandpa
			base.SetParent(this);
			AllMembers = new LORMembership4(this);
			Members = new LORMembership4(this);
			myName = "$_UNNAMED_$";
		}

	public LORVisualization4(string fileName)
		{
			// I'm my own grandpa
			base.SetParent(this);
			AllMembers = new LORMembership4(this);
			Members = new LORMembership4(this);
			myName = fileName;
			ReadVisualizationFile(fileName);
		}

		private void MakeDummies()
		{
			// SavedIndices and SaveIDs in Sequences start at 0. Cool! Great! No Prob!
			// But Channels, Groups, and DrawObjects in Visualizations start at 1 (Grrrrr)
			// So add a dummy object at the [0] start of the lists
			LORVizChannel4 lvc = new LORVizChannel4("\0\0DUMMY VIZCHANNEL AT INDEX [0] - DO NOT USE!");
			lvc.SetIndex(0);
			lvc.SetSavedIndex(0);
			lvc.SetParent(myParent);
			LORVizDrawObject4 lvdo = new LORVizDrawObject4("\0\0DUMMY VIZDRAWOBJECT AT INDEX [0] - DO NOT USE!");
			lvdo.SetIndex(0);
			lvdo.SetSavedIndex(0);
			lvdo.SetParent(myParent);
			LORVizItemGroup4 lvig = new LORVizItemGroup4("\0\0DUMMY VIZITEMGROUP AT INDEX [0] - DO NOT USE!");
			lvig.SetIndex(0);
			lvig.SetSavedIndex(0);
			lvig.SetParent(myParent);

		}



		public int ReadVisualizationFile(string existingFileName)
		{
			errorStatus = 0;
			string lineIn; // line read in (does not get modified)
			string xmlInfo = "";
			int li = lutils.UNDEFINED; // positions of certain key text in the line
																//LORTrack4 trk = new LORTrack4();
			// const string ERRproc = " in LORVisualization4:ReadVisualizationFile(";
			// const string ERRgrp = "), on Line #";
			// const string ERRitem = ", at position ";
			// const string ERRline = ", Code Line #";
			//LORSequenceType4 st = LORSequenceType4.Undefined;
			string creation = "";
			DateTime modification;

			LORVizChannel4 lastVizChannel = null;
			//Prop lastProp = null;
			LORVizDrawObject4 lastDrawObject = null;

			Clear(true);

			info.file_accessed = File.GetLastAccessTime(existingFileName);
			info.file_created = File.GetCreationTime(existingFileName);
			info.file_saved = File.GetLastWriteTime(existingFileName);



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
				if (lineIn.Substring(0, 6) != "<?xml ")
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
						// Sanity Check #3, is it a visualization?
						//li = lineIn.IndexOf(STARTvisualization);
						li = lutils.ContainsKey(lineIn, STARTvisualization);
						if (li != 0)
						{
							errorStatus = 102;
						}
						else
						{
							info = new LORSeqInfo4(null, lineIn);
							creation = info.createdAt;

							// Save this for later, as they will get changed as we populate the file
							modification = info.lastModified;
							info.filename = existingFileName;

							myName = Path.GetFileName(existingFileName);
							info.xmlInfo = xmlInfo;
							// Sanity Checks #4A and 4B, does it have a 'SaveFileVersion' and is it '14'
							//   (SaveFileVersion="14" means it cane from LOR Sequence Editor ver 4.x)
							if (info.saveFileVersion != 3)
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
									//! DrawPoints
									li = lutils.ContainsKey(lineIn, STARTdrawPoint);
									if (li > 0)
									{
										//TODO Save It!
									}
									else // Not a DrawPoint
									{
										//! DrawObjects
										li = lutils.ContainsKey(lineIn, STARTdrawObject);
										if (li > 0)
										{
										//TODO: Save it!	
										lastDrawObject = ParseDrawObject(lineIn);
											lastDrawObject.SetParent(this);
											AllMembers.Add(lastDrawObject);
										}
										else // Not a LORVizDrawObject4
										{
											//! VizChannel
											li = lutils.ContainsKey(lineIn, STARTvizChannel);
											if (li > 0)
											{
												lastVizChannel = ParseVizChannel(lineIn);
												lastVizChannel.SetParent(this);
												lastVizChannel.DrawObject = lastDrawObject;
												AllMembers.Add(lastVizChannel);
												if (lastDrawObject.redChannel == null)
												{
													if (lastVizChannel.SavedIndex == 1)
													{
														lastDrawObject.subChannel = lastVizChannel;
													}
													else
													{
														// Error Condition, unexpected
														System.Diagnostics.Debugger.Break();
													}
												}
												else
												{
													if (lastDrawObject.grnChannel == null)
													{
														if (lastVizChannel.SavedIndex == 2)
														{
															lastDrawObject.isRGB = true;
															lastDrawObject.redChannel.rgbChild = LORRGBChild4.Red;
															lastDrawObject.grnChannel = lastVizChannel;
															lastDrawObject.grnChannel.rgbChild = LORRGBChild4.Green;
														}
														else
														{
															// Error Condition
															System.Diagnostics.Debugger.Break();
														}
													}
													else
													{
														if (lastDrawObject.bluChannel == null)
														{
															if (lastVizChannel.SavedIndex == 3)
															{
																lastDrawObject.bluChannel = lastVizChannel;
																lastDrawObject.bluChannel.rgbChild = LORRGBChild4.Blue;
															}
															else
															{
																// Error Condition
																System.Diagnostics.Debugger.Break();
															}
														}
														else
														{
															// Error Condition
															System.Diagnostics.Debugger.Break();
														} // bluChannel null
													} // grnChannel null
												} // redChannel null
											} // Is it a Viz Channel? (or not?)
											else  // Not a Viz Channel
											{
												//! Samples
												li = lutils.ContainsKey(lineIn, STARTsample);
												if (li > 0)
												{
													//TODO Save It!
												}
												else // Not a Sample
												{
													//! Assigned Channel Groups
													li = lutils.ContainsKey(lineIn, STARTassignedChannelsGroup);
													if (li > 0)
													{
														//TODO Collect channels in group
													}
													else // Not an Assigned Channels Group
													{
														//! DrawPoint Groups
														li = lutils.ContainsKey(lineIn, STARTdrawPointsGroup);
														if (li > 0)
														{
															//TODO: Collect DrawPoints in group
														} // end if a track
														else // not a track
														{
															//! Item Groups
															li = lutils.ContainsKey(lineIn, STARTitem);
															if (li > 0)
															{
																LORVizItemGroup4 newGrp = new LORVizItemGroup4(this, lineIn);
																newGrp.ParseAssignedObjectNumbers(reader);
																while (VizItemGroups.Count <= newGrp.Index)
																{
																	VizItemGroups.Add(null);
																}
																VizItemGroups[newGrp.Index] = newGrp;
																Members.Add(newGrp);
																AllMembers.Add(newGrp);
															}
															else
															{
																if (Fyle.DebugMode)
																{
																	// What the heck is it?
																	string xx = lineIn;
																	//System.Diagnostics.Debugger.Break();
																} // end isWizard Report Unknown Thing Error Condition

															} // end VizItem (or not)
														} // end DrawPoint Group (or not)
													} // end AssignedChannelsGroup (or not)
												} // end Sample (or not)
											} // end VizChannel (or not)
										} // end DrawObject (or not)
									} // end DrawPoints (or not)
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
										Fyle.WriteLogEntry(emsg, lutils.LOG_Error, Application.ProductName);
									} // end catch
									*/
								} // end while lines remain
							} // end SaveFileVersion = 14

							// Restore these to the values we captured when first reading the file info header
							info.createdAt = creation;
							info.lastModified = info.file_saved;
							MakeDirty(false);
							//Members.ReIndex();
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
				Fyle.WriteLogEntry(emsg, lutils.LOG_Error, Application.ProductName);
			} // end catch
			*/


			if (errorStatus <= 0)
			{
				info.filename = existingFileName;
				PutDrawObjectsIntoItemGroups();
				//! for debugging
				//string sMsg = summary();
				//MessageBox.Show(sMsg, "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			return errorStatus;
		} // end ReadSequenceFile

		private void PutDrawObjectsIntoItemGroups()
		{
			// Note: start at 1, not 0.  Viz members start IDs at 1, not 0
			// ItemGroup member [0] is a dummy
			for (int ig = 1; ig < VizItemGroups.Count; ig++)
			{
				LORVizItemGroup4 group = VizItemGroups[ig];
				for (int n=1; n<group.AssignedObjectsNumbers.Length; n++)
				{
					int doi = group.AssignedObjectsNumbers[n];
					//LORVizDrawObject4 ldo = AllMembers.ByObjectID[doi];
					if (doi < VizDrawObjects.Count)
					{
						LORVizDrawObject4 ldo = VizDrawObjects[doi];
						if (ldo != null)
						{
							Members.Add(ldo);
							VizDrawObjects.Add(ldo);
						}
					}
					else
					{
						string msg = "Draw Object with ID " + doi + " not found in VizDrawObjects for ItemGroup ";
						msg += group.Name;
						Fyle.BUG(msg);
					}
				}
			}


		}



		public void Clear(bool areYouReallySureYouWantToDoThis)
		{
			if (areYouReallySureYouWantToDoThis)
			{
				// Zero these out from any previous run
				lineCount = 0;
				VizChannels = new List<LORVizChannel4>();
				//Props = new List<Prop>();
				this.info = new LORSeqInfo4(null);
				
				MakeDirty(false);

			} // end Are You Sure
		} // end Clear Sequence

		public LORVizChannel4 ParseVizChannel(string lineIn)
		{
			LORVizChannel4 vch = new LORVizChannel4("");
			vch.SetParent(this);
			vch.Parse(lineIn);
			VizChannels.Add(vch);
			vch.SetIndex(VizChannels.Count-1);
			AllMembers.Add(vch);
			return vch;
		}

		public LORVizDrawObject4 ParseDrawObject(string lineIn)
		{
			LORVizDrawObject4 drob = new LORVizDrawObject4("");
			drob.SetParent(this);
			drob.Parse(lineIn);
			while (VizDrawObjects.Count <= drob.Index )
			{
				VizDrawObjects.Add(null);
			}
			VizDrawObjects[drob.Index] = drob;
			return drob;
		}


		public new int CompareTo(iLORMember4 other)
		{
			int result = 0;
			
			if (LORMembership4.sortMode == LORMembership4.SORTbyOutput)
			{
				LORChannel4 ch = (LORChannel4)other;
				output.ToString().CompareTo(ch.output.ToString());
			}
			else
			{
				result = base.CompareTo(other);
			}

			return result;
		}

		public new iLORMember4 Clone()
		{
			LORVisualization4 newViz = (LORVisualization4)Clone();
			newViz.info = info.Clone();
			newViz.lineCount = lineCount;
			//VizChannels
			//Members
			//VizDrawObjects
			return newViz;
		}

		public new iLORMember4 Clone(string newName)
		{
			LORVisualization4 ret = (LORVisualization4)this.Clone();
			ChangeName(newName);
			return ret;
		}

		public new string LineOut()
		{ return ""; }
		public new void Parse(string lineIn)
		{ }

		public LORVizChannel4 CreateVizChannel(string theName)
		{
			// Does NOT check to see if a channel with this name already exists
			// Therefore, allows for duplicate channel names (But they will have different SavedIndexes)
			LORVizChannel4 chan;
			chan = new LORVizChannel4(theName);
			int newIID = AssignNextChannelID(chan);
			chan.SetParent(this);
			chan.Centiseconds = myCentiseconds;
			chan.SetIndex(VizChannels.Count);
			VizChannels.Add(chan);
			AllMembers.Add(chan);
			//myCentiseconds = Math.Max(myCentiseconds, chan.Centiseconds);
			return chan;
		}

		public LORVizDrawObject4 CreateDrawObject(string theName)
		{
			// Does NOT check to see if a channel with this name already exists
			// Therefore, allows for duplicate channel names (But they will have different SavedIndexes)
			LORVizDrawObject4 drob;
			drob = new LORVizDrawObject4(theName);
			int newIID = AssignNextObjectID(drob);
			drob.SetParent(this);
			drob.Centiseconds = myCentiseconds;
			drob.SetIndex(VizDrawObjects.Count);
			VizDrawObjects.Add(drob);
			AllMembers.Add(drob);
			//myCentiseconds = Math.Max(myCentiseconds, chan.Centiseconds);
			return drob;
		}

		public LORVizItemGroup4 CreateItemGroup(string theName)
		{
			// Does NOT check to see if a group with this name already exists
			// Therefore, allows for duplicate group names (But they will have different SavedIndexes)
			LORVizItemGroup4 grp;
			grp = new LORVizItemGroup4(this, theName);
			int newIID = AssignNextItemID(grp);
			grp.SetParent(this);
			//chg.Members.SetParent(this);
			//chg.Members.Owner = chg;
			grp.Centiseconds = myCentiseconds;
			grp.SetIndex(VizItemGroups.Count);
			VizItemGroups.Add(grp);
			AllMembers.Add(grp);
			//myCentiseconds = Math.Max(myCentiseconds, chg.Centiseconds);

			return grp;
		}

		public LORVizChannel4 FindChannel(string theName, bool createIfNotFound=false)
		{
			LORVizChannel4 ret = null;
			for (int v = 0; v < VizChannels.Count; v++)
			{
				if (theName == VizChannels[v].Name)
				{
					ret = VizChannels[v];
					v = VizChannels.Count; // Escape loop
				}
			}
			if ((ret== null) && createIfNotFound)
			{
				ret = CreateVizChannel(theName);
			}
			return ret;
		}

		public LORVizItemGroup4 FindItemGroup(string theName, bool createIfNotFound=false)
		{
			LORVizItemGroup4 ret = null;
			for (int v = 0; v < VizItemGroups.Count; v++)
			{
				if (theName == VizItemGroups[v].Name)
				{
					ret = VizItemGroups[v];
					v = VizItemGroups.Count; // Escape loop
				}
			}
			if ((ret == null) && createIfNotFound)
			{
				ret = CreateItemGroup(theName);
			}
			return ret;
		}

		public LORVizDrawObject4 FindDrawObject(string theName, bool createIfNotFound=false)
		{
			LORVizDrawObject4 ret = null;
			for (int v = 0; v < VizDrawObjects.Count; v++)
			{
				if (theName == VizDrawObjects[v].Name)
				{
					ret = VizDrawObjects[v];
					v = VizDrawObjects.Count; // Escape loop
				}
			}
			if ((ret == null) && createIfNotFound)
			{
				ret = CreateDrawObject(theName);
			}
			return ret;
		}


		private int AssignNextChannelID(LORVizChannel4 thePart)
		{
			if (thePart.SavedIndex < 0)
			{
				int newIID = AllMembers.HighestSavedIndex + 1;
				thePart.SetSavedIndex(newIID);
				AllMembers.Add(thePart);
			}
			return thePart.SavedIndex;
		}

		private int AssignNextItemID(LORVizItemGroup4 thePart)
		{
			if (thePart.SavedIndex < 0)
			{
				int newIID = AllMembers.HighestItemID + 1;
				thePart.SetSavedIndex(newIID);
				AllMembers.Add(thePart);
			}
			return thePart.SavedIndex;
		}

		private int AssignNextObjectID(LORVizDrawObject4 thePart)
		{
			if (thePart.SavedIndex < 0)
			{
				int newIID = AllMembers.HighestObjectID + 1;
				thePart.SetSavedIndex(newIID);
				AllMembers.Add(thePart);
			}
			return thePart.SavedIndex;
		}


		private int AssignNextAltChannelID(iLORMember4 thePart)
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





	} // end Visualization class
} // end namespace LORUtils4