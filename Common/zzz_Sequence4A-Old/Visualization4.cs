using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace LORUtils4
{
	public class LORVisualization4 : LORMember4, IComparable<LORMember4>
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
		protected string myName = "$_UNNAMED_$";
		protected int centiseconds = 0;
		protected int myIndex = lutils.UNDEFINED;
		protected int mySavedIndex = lutils.UNDEFINED;
		protected int myAltSI = lutils.UNDEFINED;
		protected bool isSelected = false;
		public int lineCount = 0;
		protected bool isDirty = false;
		protected bool hasBeenWritten = false;
		protected object myTag = null;
		protected LORMember4 mapTo = null;
		protected bool matchesExactly = false;  // Used to distinguish exact name matches from fuzzy name matches
		public List<LORVizChannel4> VizChannels = new List<LORVizChannel4>();
		public LORMembership4 Members = null;
		public List<LORVizDrawObject4> VizDrawObjects = new List<LORVizDrawObject4>();
		public List<LORVizItemGroup4> VizItemGroups = new List<LORVizItemGroup4>();
		//public List<Prop> Props = new List<Prop>();


		public LORVisualization4()
		{
			Members = new LORMembership4(this);
		}

		public LORVisualization4(string fileName)
		{
			Members = new LORMembership4(this);
			ReadVisualizationFile(fileName);
		}

		public string Name
		{ get { return myName; } }
		public void ChangeName(string newName)
		{ myName = newName; }
		public int Centiseconds
		{ get { return centiseconds; } set { centiseconds = value; } }
		public int Index
		{ get { return myIndex; } }
		public void SetIndex(int theIndex)
		{ myIndex = theIndex; }
		public int SavedIndex
		{ get { return mySavedIndex; } }
		public void SetSavedIndex(int theSavedIndex)
		{ mySavedIndex = theSavedIndex; }
		public int AltSavedIndex
		{ get { return myAltSI; } set { myAltSI = value; } }
		public LORMember4 Parent
		{ 
			get
			{
				if (lutils.IsWizard)
				{
					// Check call stack, who/why is asking for the parent?
					// (this is a top level object, has no parent (other than itself))
					if (lutils.IsWizard)
					{
						System.Diagnostics.Debugger.Break();
					}
				}
				return this; 
			}
		}
		public void SetParent(LORMember4 newParent)
		{
			//! IGNORE!
			// This is a top level object, it has no parent
			// Check call stack, who/why is trying to set this?
			if (lutils.IsWizard)
			{
				System.Diagnostics.Debugger.Break();
			}
		}
		public void SetParentSequence(LORSequence4 newParentSequence)
		{ // Do Nothing, don't bother to save parent
		}
		public bool Selected
		{ get { return isSelected; } set { isSelected = value; } }
		public LORMemberType4 MemberType
		{ get { return LORMemberType4.Visualization; } }
		public bool Written
		{ get { return hasBeenWritten; } }
		public object Tag
		{ get { return myTag; } set { myTag = value; } }
		public bool ExactMatch
		{ get { return matchesExactly; } set { matchesExactly = value; } }
		public LORMember4 MapTo
		{ get { return mapTo; } set { mapTo = value; } }
		public int UniverseNumber
		{ get { return 0; } }
		public int DMXAddress
		{ get { return 0; } }


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
			LORVizItemGroup4 lastGroup = null;

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
										}
										else // Not a LORVizDrawObject4
										{
											//! VizChannel
											li = lutils.ContainsKey(lineIn, STARTvizChannel);
											if (li > 0)
											{
												lastVizChannel = ParseVizChannel(lineIn);
												lastVizChannel.DrawObject = lastDrawObject;
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
															li = lutils.ContainsKey(lineIn, STARTitem);
															if (li > 0)
															{
																LORVizItemGroup4 newGrp = new LORVizItemGroup4(lineIn);
																newGrp.ParseAssignedObjectNumbers(reader);
																VizItemGroups.Add(newGrp);
																Members.Add(newGrp);
															}
															else
															{
																if (lutils.IsWizard)
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
				//! for debugging
				//string sMsg = summary();
				//MessageBox.Show(sMsg, "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			return errorStatus;
		} // end ReadSequenceFile

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
			//vch.SetParentViz(this);
			vch.SetIndex(VizChannels.Count);
			VizChannels.Add(vch);
			Members.Add(vch);
			vch.Parse(lineIn);
			return vch;
		}

		public LORVizDrawObject4 ParseDrawObject(string lineIn)
		{
			LORVizDrawObject4 drob = new LORVizDrawObject4("");
			//vch.SetParentViz(this);
			drob.SetIndex(VizDrawObjects.Count);
			VizDrawObjects.Add(drob);
			drob.Parse(lineIn);
			return drob;
		}

		public bool Dirty
		{ get { return isDirty; } }
		public void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				info.lastModified = DateTime.Now; //.ToString("MM/dd/yyyy hh:mm:ss tt");
			}
			isDirty = dirtyState;
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			if (LORMembership4.sortMode == LORMembership4.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership4.sortMode == LORMembership4.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership4.sortMode == LORMembership4.SORTbyAltSavedIndex)
					{
						result = myAltSI.CompareTo(other.AltSavedIndex);
					}
					else
					{
						if (LORMembership4.sortMode == LORMembership4.SORTbyOutput)
						{
							LORChannel4 ch = (LORChannel4)other;
							output.ToString().CompareTo(ch.output.ToString());
						}
					}
				}
			}

			return result;
		}

		public LORMember4 Clone()
		{
			LORVisualization4 newViz = new LORVisualization4();
			newViz.info = info.Clone();
			newViz.myName = myName;
			newViz.centiseconds = centiseconds;
			newViz.myIndex = myIndex;
			newViz.mySavedIndex = mySavedIndex;
			newViz.myAltSI = myAltSI;
			newViz.isSelected = isSelected;
			newViz.lineCount = lineCount;
			newViz.isDirty = isDirty;
			newViz.hasBeenWritten = hasBeenWritten;
			newViz.myTag = newViz.myTag;
			//VizChannels
			//Members
			//VizDrawObjects
			return newViz;
		}

		public LORMember4 Clone(string newName)
		{
			LORVisualization4 ret = (LORVisualization4)this.Clone();
			ret.myName = newName;
			return ret;
		}

		public string LineOut()
		{ return ""; }
		public void Parse(string lineIn)
		{ }

	} // end Visualization class
} // end namespace LORUtils4