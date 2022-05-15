using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FileHelper;

namespace LOR4Utils
{
	public class LORVisualization4 : LORMemberBase4, iLOR4Member, IComparable<iLOR4Member>
	{
		// Sub-Member Heirarchy:
		//	Visualization has ItemGroups,
		//   ItemGroups have DrawObjects,
		//    DrawObjects have 1 or 3 VizChannels,
		//     (1 for single-color, 3 for RGB).

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
		// But Visualizations use "LOR4Channel", "Name" and "Color" -- Arrrggggh!
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
		//public LOROutput4 output = new LOROutput4();
		public int lineCount = 0;
		public List<LORVizItemGroup4> VizItemGroups = new List<LORVizItemGroup4>();
		public List<LORVizDrawObject4> VizDrawObjects = new List<LORVizDrawObject4>();
		// Contains ALL channels, even though they are sub-members to DrawObjects which are sub-members to ItemGroups
		public List<LORVizChannel4> VizChannels = new List<LORVizChannel4>();

		// Members contains the only objects directly below a visualization
		//   which is ItemGroups
		public LOR4Membership Members = null;
		// All Members contains ItemGroups, DrawObjects, and VizChannels
		public LOR4Membership AllMembers = null;
		//public List<Prop> Props = new List<Prop>();
		private int highestSavedIndex = lutils.UNDEFINED;


		private StringBuilder reportBuilder = new StringBuilder();


		public LORVisualization4(iLOR4Member NullParentIgnoreThis, string fileName)
		{
			// I'm my own grandpa
			base.SetParent(this);
			AllMembers = new LOR4Membership(this);
			Members = new LOR4Membership(this);
			MakeDummies();
			myName = fileName;
			ReadVisualizationFile(fileName);
		}

		private void MakeDummies()
		{
			// SavedIndices and SaveIDs in Sequences start at 0. Cool! Great! No Prob!
			// But VizChannels, ItemGroups, and DrawObjects in Visualizations start at 1 (Grrrrr)
			// So add a dummy object at the [0] start of the lists

			// Create a dummy channel and put it at the start of the channel list
			LORVizChannel4 lvc = new LORVizChannel4(this, "### DUMMY VIZCHANNEL AT INDEX [0] - DO NOT USE!");
			lvc.SetIndex(0);
			lvc.SetID(0);
			lvc.SetParent(this);
			// Create a dummy DrawObject and put it at the start of the list
			LORVizDrawObject4 lvdo = new LORVizDrawObject4(this, "### DUMMY VIZDRAWOBJECT AT INDEX [0] - DO NOT USE!");
			lvdo.SetIndex(0);
			lvdo.SetID(0);
			lvdo.SetParent(this);
			// Assign the dummy channel to the dummy drawobject
			lvdo.redChannel = lvc;
			// Create a dummy itemgroup and put it at the start of the list
			LORVizItemGroup4 lvig = new LORVizItemGroup4(this, "### DUMMY VIZITEMGROUP AT INDEX [0] - DO NOT USE!");
			lvig.SetIndex(0);
			lvig.SetID(0);
			// Add the dummy drawobject to the dummy itemgroup membership
			//lvig.Members.Add(lvdo);
			// Add the dummy itemgroup to the Visualizations members at index [0]
			//Members.Add(lvig);

			LORMemberBase4 lmb = new LORMemberBase4(this, "### DUMMY LORMEMBER AT INDEX [0] - DO NOT USE!");
			lmb.SetIndex(0);
			lmb.SetID(0);
			AllMembers.Add(lmb);



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
																 //LOR4SequenceType st = LOR4SequenceType.Undefined;
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
											lastDrawObject = CreateNewDrawObject(lineIn);
										}
										else // Not a LORVizDrawObject4
										{
											//! VizChannel
											li = lutils.ContainsKey(lineIn, STARTvizChannel);
											if (li > 0)
											{
												lastVizChannel = CreateNewVizChannel(lineIn);
												//lastVizChannel.SetParent(this);
												lastVizChannel.DrawObject = lastDrawObject;
												//AllMembers.Add(lastVizChannel);
												if (lastDrawObject.redChannel == null)
												{
													if (lastVizChannel.ItemID == 1)
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
														if (lastVizChannel.ItemID == 2)
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
															if (lastVizChannel.ItemID == 3)
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
																LORVizItemGroup4 newGrp = CreateNewItemGroup(lineIn);
																newGrp.ParseAssignedObjectNumbers(reader);
																while (VizItemGroups.Count <= newGrp.Index)
																{
																	VizItemGroups.Add(null);
																}
																VizItemGroups[newGrp.Index] = newGrp;
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
							AllMembers.ReIndex();
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
				group.Members.Clear();

				LORVizDrawObject4 lvdo = new LORVizDrawObject4(this, "DUMMY");
				lvdo.SetID(0);
				lvdo.SetIndex(0);
				//group.Members[0] = lvdo;
				VizDrawObjects[0] = lvdo;


				group.Members.Add(VizDrawObjects[0]);
				for (int n = 1; n < group.AssignedObjectsNumbers.Length; n++)
				{
					int doi = group.AssignedObjectsNumbers[n];
					//LORVizDrawObject4 ldo = AllMembers.ByObjectID[doi];
					if (doi < VizDrawObjects.Count)
					{
						LORVizDrawObject4 ldo = VizDrawObjects[doi];
						if (ldo != null)
						{
							while (group.Members.Count <= n)
							{
								group.Members.Add(ldo);
							}
							group.Members[n] = ldo;
							//VizDrawObjects.Add(ldo);
						}
					}
					else
					{
						string msg = "Draw Object with ID " + doi + " not found in VizDrawObjects for ItemGroup ";
						msg += group.Name;
						//Fyle.BUG(msg);
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
				VizDrawObjects = new List<LORVizDrawObject4>();
				VizItemGroups = new List<LORVizItemGroup4>();
				Members = new LOR4Membership(this);
				AllMembers = new LOR4Membership(this);
				MakeDummies();

				//Props = new List<Prop>();
				this.info = new LORSeqInfo4(null);

				MakeDirty(false);

			} // end Are You Sure
		} // end Clear Sequence



		public override iLOR4Member Clone()
		{
			LORVisualization4 newViz = (LORVisualization4)Clone();
			newViz.info = info.Clone();
			newViz.lineCount = lineCount;
			//VizChannels
			//Members
			//VizDrawObjects
			return newViz;
		}

		public override iLOR4Member Clone(string newName)
		{
			LORVisualization4 ret = (LORVisualization4)this.Clone();
			ChangeName(newName);
			return ret;
		}

		public override string LineOut()
		{ return ""; }
		public override void Parse(string lineIn)
		{ }

		public LORVizChannel4 CreateNewVizChannel(string lineIn)
		{
			LORVizChannel4 vch = new LORVizChannel4(this, lineIn);
			VizChannels.Add(vch);
			vch.SetIndex(VizChannels.Count - 1);
			highestSavedIndex++;
			vch.SetSavedIndex(highestSavedIndex);
			//AllMembers.Add(vch);
			return vch;
		}

		public LORVizDrawObject4 CreateNewDrawObject(string lineIn)
		{
			LORVizDrawObject4 drob = new LORVizDrawObject4(this, lineIn);
			while (VizDrawObjects.Count <= drob.DrawObjectID)
			{
				VizDrawObjects.Add(null);
			}
			VizDrawObjects[drob.DrawObjectID] = drob;
			drob.SetIndex(drob.DrawObjectID);
			//AllMembers.Add(drob);
			drob.SetIndex(drob.DrawObjectID);
			//myCentiseconds = Math.Max(myCentiseconds, chan.Centiseconds);
			return drob;
		}

		public LORVizItemGroup4 CreateNewItemGroup(string lineIn)
		{
			LORVizItemGroup4 group = new LORVizItemGroup4(this, lineIn);
			group.SetIndex(group.ItemID);
			while (VizItemGroups.Count <= group.ItemID)
			{
				VizItemGroups.Add(null);
			}
			VizItemGroups[group.ItemID] = group;
			while (Members.Count <= group.ItemID)
			{
				LORMemberBase4 mem = new LORMemberBase4(this, "\tDUMMY");
				mem.SetID(0);
				mem.SetIndex(0);
				Members.Add(mem);
			}
			Members[group.ItemID] = group;
			return group;
		}

		public LORVizChannel4 FindVizChannel(string channelName, bool createIfNotFound = false)
		{
			LORVizChannel4 ret = null;
			iLOR4Member member = AllMembers.FindByName(channelName, LOR4MemberType.VizChannel, createIfNotFound);
			if (member != null)
			{
				ret = (LORVizChannel4)member;
			}
			return ret;
		}

		public LORVizItemGroup4 FindItemGroup(string groupName, bool createIfNotFound = false)
		{
			LORVizItemGroup4 ret = null;
			iLOR4Member member = AllMembers.FindByName(groupName, LOR4MemberType.VizItemGroup, createIfNotFound);
			if (member != null)
			{
				ret = (LORVizItemGroup4)member;
			}
			return ret;
		}

		public LORVizDrawObject4 FindDrawObject(string objectName, bool createIfNotFound = false)
		{
			LORVizDrawObject4 ret = null;
			iLOR4Member member = AllMembers.FindByName(objectName, LOR4MemberType.VizDrawObject, createIfNotFound);
			if (member != null)
			{
				ret = (LORVizDrawObject4)member;
			}
			return ret;
		}


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

		public string Tegrity()
		{
			reportBuilder = new StringBuilder(); // Clear, Reset
			int te = 0; // Total Errors
			int ie = 0; // Item Errors
			string l = "";

			l = "-Viz FileName is '" + myName + "'.";
			r(l);
			//! CHANNELS
			l = "-Viz has " + (VizChannels.Count - 1).ToString() + " Channels.";
			r(l);
			if (VizChannels[0] == null)
			{
				l = "!Channel[0] is null.";
				te++;
			}
			else
			{
				int p = VizChannels[0].Name.IndexOf("DUMMY");
				if (p > 0)
				{
					l = "-Channel[0] is the dummy channel.";
				}
				else
				{
					l = "!Channel[0] is " + VizChannels[0].Name;
					te++;
				}
			}
			r(l);
			for (int c = 1; c < VizChannels.Count; c++)
			{
				ie = 0;
				LORVizChannel4 ch = VizChannels[c];
				if (ch == null)
				{
					l = "!Channel [" + c.ToString() + "] is null.";
					r(l);
					ie++;
				}
				else
				{
					string n = "Channel[" + c.ToString() + "] " + ch.Name + " - ";
					if (ch.Name.Length < 1)
					{
						l = "!" + n + "Has no name.";
						r(l);
						ie++;
					}
					//if (ch.ItemID != c)
					//{
					//	l = "!" + n + "ItemID is " + ch.ItemID.ToString();
					//	r(l);
					//	ie++;
					//}
					if (ch.Parent == null)
					{
						l = "!" + n + "Has no parent.";
						r(l);
						ie++;
					}
					if (ch.Owner == null)
					{
						l = "!" + n + "Has no owner.";
						r(l);
						ie++;
					}
					if ((ch.UniverseNumber < 1) || (ch.DMXAddress < 1))
					{
						l = "!" + n + "Has invalid address of " + ch.UniverseNumber.ToString() + "/" + ch.DMXAddress.ToString();
						r(l);
						ie++;
					}
					if (ch.color < 0)
					{
						l = "!" + n + "Has no color.";
						r(l);
						ie++;
					}
				} // End is null
				te += ie;
			} // End loop thru channels

			//! DRAW OBJECTS
			l = "-Viz has " + (VizDrawObjects.Count - 1).ToString() + " DrawObjects.";
			r(l);
			if (VizDrawObjects[0] == null)
			{
				l = "!DrawObject[0] is null.";
				te++;
			}
			else
			{
				int p = VizDrawObjects[0].Name.IndexOf("DUMMY");
				if (p > 0)
				{
					l = "-DrawObject[0] is the dummy object.";
				}
				else
				{
					l = "!DrawObject[0] is NOT the dummy object, it is '" + VizDrawObjects[0].Name + "'.";
					te++;
				}
			}
			r(l);
			for (int c = 1; c < VizDrawObjects.Count; c++)
			{
				ie = 0;
				LORVizDrawObject4 ob = VizDrawObjects[c];
				if (ob == null)
				{
					l = "!DrawObject[" + c.ToString() + "] is null.";
					r(l);
					ie++;
				}
				else
				{
					string n = "DrawObject[" + c.ToString() + "] " + ob.Name + " - ";
					if (ob.Name.Length < 1)
					{
						l = "!" + n + "Has no name.";
						r(l);
						ie++;
					}
					if (ob.DrawObjectID != c)
					{
						l = "!" + n + "DrawObjectID is " + ob.DrawObjectID.ToString() + " (Should be " + c.ToString() + ")"; ;
						r(l);
						ie++;
					}
					if (ob.Parent == null)
					{
						l = "!" + n + "Has no parent.";
						r(l);
						ie++;
					}
					if ((ob.UniverseNumber < 1) || (ob.DMXAddress < 1))
					{
						l = "!" + n + "Has invalid address of " + ob.UniverseNumber.ToString() + "/" + ob.DMXAddress.ToString();
						r(l);
						ie++;
					}
					if (ob.isRGB)
					{
						if (ob.redChannel == null)
						{
							l = "!" + n + "Is RGB and has no Red Channel";
							r(l);
							ie++;
						}
						else
						{
							if (ob.redChannel.Name.Length < 2)
							{
								l = "!" + n + "Is RGB and Red Channel has no name.";
								r(l);
								ie++;
							}
							if (ob.color < 0)
							{
								l = "!" + n + "is RGB but Red Channel isn't Red.";
								r(l);
								ie++;
							}
							if (ob.redChannel.output == null)
							{
								l = "!" + n + "Is RGB and Red Channel has no Output.";
								r(l);
								ie++;
							}
							else
							{
								if (ob.redChannel.output.DMXAddress < 1)
								{
									l = "!" + n + "Is RGB and Red Channel has DMX Address.";
									r(l);
									ie++;
								}
							}
						}
						if (ob.grnChannel == null)
						{
							l = "!" + n + "... and has no Green Channel";
							r(l);
							ie++;
						}
						if (ob.bluChannel == null)
						{
							l = "!" + n + "... and has no Blue Channel";
							r(l);
							ie++;
						}
					}
					else
					{
						if (ob.subChannel == null)
						{
							l = "!" + n + "Is not RGB and has no Sub-Channel";
							r(l);
							ie++;
						}
						else
						{
							if (ob.redChannel.Name.Length < 2)
							{
								l = "!" + n + "Is not RGB and Sub-Channel has no name.";
								r(l);
								ie++;
							}
							if (ob.color < 0)
							{
								l = "!" + n + "Sub-Channel has no color.";
								r(l);
								ie++;
							}
							if (ob.redChannel.output == null)
							{
								l = "!" + n + "Is not RGB and Sub-Channel has no Output.";
								r(l);
								ie++;
							}
							else
							{
								if (ob.redChannel.output.DMXAddress < 1)
								{
									l = "!" + n + "Is not RGB and Sub-Channel has DMX Address.";
									r(l);
									ie++;
								}
							}
						}
					}

				} // End is null
				te += ie;
			} // End loop thru DrawObjects

			//! ITEM GROUPS
			l = "-It has " + (VizItemGroups.Count - 1).ToString() + " Item Groups.";
			r(l);
			if (VizItemGroups[0] == null)
			{
				l = "!ItemGroup[0] is null.";
				te++;
			}
			else
			{
				int p = VizItemGroups[0].Name.IndexOf("DUMMY");
				if (p > 0)
				{
					l = "-ItemGroup[0] is the dummy group.";
				}
				else
				{
					l = "!ItemGroup[0] is NOT the dummy group, it is '" + VizItemGroups[0].Name + "'.";
					te++;
				}
			}
			r(l);
			for (int c = 1; c < VizItemGroups.Count; c++)
			{
				ie = 0;
				LORVizItemGroup4 ig = VizItemGroups[c];
				if (ig == null)
				{
					l = "!ItemGroup[" + c.ToString() + "] is null.";
					r(l);
					ie++;
				}
				else
				{
					string n = "ItemGroup[" + c.ToString() + "] " + ig.Name + " - ";
					if (ig.Name.Length < 1)
					{
						l = "!" + n + "Has no name.";
						r(l);
						ie++;
					}
					if (ig.ItemID != c)
					{
						l = "!" + n + "ItemID is " + ig.ItemID.ToString() + " (Should be " + c.ToString() + ")";

						r(l);
						ie++;
					}
					if (ig.Parent == null)
					{
						l = "!" + n + "Has no parent.";
						r(l);
						ie++;
					}
					if ((ig.UniverseNumber < 1) || (ig.DMXAddress < 1))
					{
						l = "!" + n + "Has invalid address of " + ig.UniverseNumber.ToString() + "/" + ig.DMXAddress.ToString();
						r(l);
						ie++;
					}
					if (ig.color < 0)
					{
						l = "!" + n + "Has no color.";
						r(l);
						ie++;
					}

					if (ig.AssignedObjectsNumbers.Length < 2)
					{
						l = "!" + n + "Has no AssignedObjectNumbers.";
						r(l);
						ie++;
					}
					if (ig.Members.Count < 2)
					{
						l = "!" + n + "Has no Assigned DrawObject Members.";
						r(l);
						ie++;
					}
					if (ig.AssignedObjectsNumbers.Length != ig.Members.Count)
					{
						l = "!" + n + "AssignedObjectNumbers count (" + ig.AssignedObjectsNumbers.Length.ToString();
						l += ") does not match Members Count {" + ig.Members.Count.ToString() + ").";
						r(l);
						ie++;
						l = " * ";
						for (int b = 0; b < ig.Members.Count; b++)
						{
							l += ig.Members[b].Name + ", ";
						}
						r(l);
					}
					if (ig.AssignedObjectsNumbers.Length > 0)
					{
						if (ig.AssignedObjectsNumbers[0] != 0)
						{
							l = "!" + n + "AssignedObjectNumbers[0] != the Dummy Channel.";
							r(l);
							ie++;
						}
						for (int a = 1; a < ig.AssignedObjectsNumbers.Length; a++)
						{
							if (ig.Members[a] == null)
							{
								l = "!" + n + "Assigned Object Members[" + a.ToString() + "] is null.";
								r(l);
								ie++;
							}
							else
							{
								iLOR4Member mbr = ig.Members[a];
								if (mbr.MemberType != LOR4MemberType.VizDrawObject)
								{
									l = "!" + n + "Assigned Object Members[" + a.ToString() + "] '";
									l += mbr.Name + "' is not a DrawObject.";
									r(l);
									ie++;
								}
								else
								{
									LORVizDrawObject4 ob = (LORVizDrawObject4)mbr;
									if (ig.AssignedObjectsNumbers[a] != ob.DrawObjectID)
									{
										l = "!" + n + "Assigned Object Members[" + a.ToString() + "] '";
										l += mbr.Name + "' DrawObjectID (" + ob.DrawObjectID.ToString();
										l += ") does not match AssignedObjectNumber " + ig.AssignedObjectsNumbers[a];
										r(l);
										ie++;
									} // End ID mismatch
								} // Member not DrawObject
							} // Member is null
						} // Loop thru assignedobjectnumbers
					} // has assignedobjectnumbers
				} // End is null
				te += ie;
			} // End loop thru Groups
			l = "- Total Errors =" + te.ToString();
			r(l);

			return reportBuilder.ToString();
		}

		private void r(string line)
		{
			reportBuilder.Append(line);
			reportBuilder.Append("\r\n");
		}










	} // end Visualization class
} // end namespace LOR4Utils