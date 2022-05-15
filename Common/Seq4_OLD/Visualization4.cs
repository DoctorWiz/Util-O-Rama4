using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FileHelper;

namespace LOR4Utils
{
	public class LOR4Visualization : LOR4MemberBase, iLOR4Member, IComparable<iLOR4Member>
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
		// But Visualizations use "LOR4Channel", "Name" and "Color" -- Arrrggggh!
		public static readonly string FIELDvizName = " Name";
		public static readonly string FIELDvizColor = " Color";

		private static readonly string STARTvisualization = lutils.STFLD + TABLEvisualization + LOR4SequenceInfo.FIELDlvizSaveFileVersion;
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
		public LOR4SequenceInfo info = null;
		public LOR4Output output = new LOR4Output();
		public int lineCount = 0;
		public List<LOR4VizChannel> VizChannels = new List<LOR4VizChannel>();
		public LOR4Membership Members = null;
		public List<LOR4VizDrawObject> VizDrawObjects = new List<LOR4VizDrawObject>();
		public List<LOR4VizItemGroup> VizItemGroups = new List<LOR4VizItemGroup>();
		//public List<Prop> Props = new List<Prop>();


		public LOR4Visualization()
		{
			Members = new LOR4Membership(this);
			myName = "$_UNNAMED_$";
		}

		public LOR4Visualization(string fileName)
		{
			Members = new LOR4Membership(this);
			myName = fileName;
			ReadVisualizationFile(fileName);
		}

		public int ReadVisualizationFile(string existingFileName)
		{
			errorStatus = 0;
			string lineIn; // line read in (does not get modified)
			string xmlInfo = "";
			int li = lutils.UNDEFINED; // positions of certain key text in the line
																 //LORTrack4 trk = new LORTrack4();
																 // const string ERRproc = " in LOR4Visualization:ReadVisualizationFile(";
																 // const string ERRgrp = "), on Line #";
																 // const string ERRitem = ", at position ";
																 // const string ERRline = ", Code Line #";
																 //LOR4SequenceType st = LOR4SequenceType.Undefined;
			string creation = "";
			DateTime modification;

			LOR4VizChannel lastVizChannel = null;
			//Prop lastProp = null;
			LOR4VizDrawObject lastDrawObject = null;
			LOR4VizItemGroup lastGroup = null;

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
							info = new LOR4SequenceInfo(null, lineIn);
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
										else // Not a LOR4VizDrawObject
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
															lastDrawObject.redChannel.rgbChild = LOR4RGBChild.Red;
															lastDrawObject.grnChannel = lastVizChannel;
															lastDrawObject.grnChannel.rgbChild = LOR4RGBChild.Green;
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
																lastDrawObject.bluChannel.rgbChild = LOR4RGBChild.Blue;
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
																LOR4VizItemGroup newGrp = new LOR4VizItemGroup(lineIn);
																newGrp.ParseAssignedObjectNumbers(reader);
																VizItemGroups.Add(newGrp);
																Members.Add(newGrp);
															}
															else
															{
																if (Fyle.isWiz)
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
				VizChannels = new List<LOR4VizChannel>();
				//Props = new List<Prop>();
				this.info = new LOR4SequenceInfo(null);

				MakeDirty(false);

			} // end Are You Sure
		} // end Clear Sequence

		public LOR4VizChannel ParseVizChannel(string lineIn)
		{
			LOR4VizChannel vch = new LOR4VizChannel("");
			//vch.SetParentViz(this);
			vch.SetIndex(VizChannels.Count);
			VizChannels.Add(vch);
			Members.Add(vch);
			vch.Parse(lineIn);
			return vch;
		}

		public LOR4VizDrawObject ParseDrawObject(string lineIn)
		{
			LOR4VizDrawObject drob = new LOR4VizDrawObject("");
			//vch.SetParentViz(this);
			drob.SetIndex(VizDrawObjects.Count);
			VizDrawObjects.Add(drob);
			drob.Parse(lineIn);
			return drob;
		}


		public new int CompareTo(iLOR4Member other)
		{
			int result = 0;

			if (LOR4Membership.sortMode == LOR4Membership.SORTbyOutput)
			{
				LOR4Channel ch = (LOR4Channel)other;
				output.ToString().CompareTo(ch.output.ToString());
			}
			else
			{
				result = base.CompareTo(other);
			}

			return result;
		}

		public new iLOR4Member Clone()
		{
			LOR4Visualization newViz = (LOR4Visualization)Clone();
			newViz.info = info.Clone();
			newViz.lineCount = lineCount;
			//VizChannels
			//Members
			//VizDrawObjects
			return newViz;
		}

		public new iLOR4Member Clone(string newName)
		{
			LOR4Visualization ret = (LOR4Visualization)this.Clone();
			ChangeName(newName);
			return ret;
		}

		public new string LineOut()
		{ return ""; }
		public new void Parse(string lineIn)
		{ }

	} // end Visualization class
} // end namespace LOR4Utils