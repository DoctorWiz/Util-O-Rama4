using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace LORUtils
{
	public class Visualization4 //: IMember, IComparable<IMember>
	{
		public static readonly string TABLEvisualization =		"LViz";
		public static readonly string TABLEdrawPoint =				"DrawPoint";
		public static readonly string TABLEdrawPoints =				"DrawPoints";
		public static readonly string TABLEdrawObject =				"DrawObject";
		public static readonly string TABLElevel =						"Level";
		public static readonly string TABLElevels =						"Levels";
		public static readonly string TABLEitem =							"Item";
		public static readonly string TABLEitems =						"Items";
		public static readonly string TABLEassignedObject =		"AssignedObject";
		public static readonly string TABLEsample =						"Sample";
		public static readonly string TABLEassignedChannels = "AssignedChannels";
		public static readonly string TABLEsuperStarData =		"SuperStar_Data";
		public static readonly string TABLEvizChannel = "Channel";

		public static readonly string FIELDvizID =				" ID";
		public static readonly string FIELDbackColor =		" Background_Color";
		public static readonly string FIELDreverseOrder = " SSReverseOrder";
		// Sequence Channels use "channel", "name" and "color" (Lower Case)
		// But Visualizations use "Channel", "Name" and "Color" -- Arrrggggh!
		public static readonly string FIELDvizName = " Name";
		public static readonly string FIELDvizColor = " Color";

		private static readonly string STARTvisualization =					utils.STFLD + TABLEvisualization + Info.FIELDlvizSaveFileVersion;
		private static readonly string STARTdrawPoint =							utils.STFLD + TABLEdrawPoint + FIELDvizID +							utils.FIELDEQ;
		private static readonly string STARTdrawObject =						utils.STFLD + TABLEdrawObject + FIELDvizID +						utils.FIELDEQ;
		private static readonly string STARTitem =									utils.STFLD + TABLEitem + FIELDvizID +									utils.FIELDEQ;
		private static readonly string STARTvizChannel =						utils.STFLD + TABLEvizChannel + FIELDvizID +					utils.FIELDEQ;
		private static readonly string STARTsample =								utils.STFLD + TABLEsample + FIELDbackColor +						utils.FIELDEQ;
		private static readonly string STARTsuperStarData =					utils.STFLD + TABLEsuperStarData + FIELDreverseOrder +	utils.FIELDEQ;
		private static readonly string STARTdrawPointsGroup =				utils.STFLD + TABLEdrawPoints +													utils.ENDTBL;
		private static readonly string ENDdrawPointsGroup =					utils.FINTBL + TABLEdrawPoints +												utils.ENDTBL;
		private static readonly string STARTassignedChannelsGroup = utils.STFLD + TABLEassignedChannels +										utils.ENDTBL;
		private static readonly string ENDassignedChannelsGroup =		utils.FINTBL + TABLEassignedChannels +									utils.ENDTBL;
		private static readonly string STARTItemsGroup =						utils.STFLD + TABLEitems +															utils.ENDTBL;
		private static readonly string ENDItemsGroup =							utils.FINTBL + TABLEitems +															utils.ENDTBL;

		public int errorStatus = 0;
		public Info info = null;
		private string myName = "$_UNNAMED_$";
		public int lineCount = 0;
		public bool dirty = false;
		public List<VizChannel> VizChannels = new List<VizChannel>();
		public List<DrawObject> DrawObjects = new List<DrawObject>();
		//public List<Prop> Props = new List<Prop>();

		public int ReadVisualizationFile(string existingFileName)
		{
			errorStatus = 0;
			string lineIn; // line read in (does not get modified)
			string xmlInfo = "";
			int li = utils.UNDEFINED; // positions of certain key text in the line
																//Track trk = new Track();
			const string ERRproc = " in Visualization4:ReadVisualizationFile(";
			const string ERRgrp = "), on Line #";
			const string ERRitem = ", at position ";
			const string ERRline = ", Code Line #";
			SequenceType st = SequenceType.Undefined;
			string creation = "";
			DateTime modification;

			VizChannel lastVizChannel = null;
			//Prop lastProp = null;
			DrawObject lastDrawObject = null;

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
						li = utils.FastIndexOf(lineIn, STARTvisualization);
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
									//li = lineIn.IndexOf(STARTdrawPoint);
									li = utils.FastIndexOf(lineIn, STARTdrawPoint);
									if (li > 0)
									{
										//TODO Save It!
									}
									else // Not a DrawPoint
									{
										//! DrawObjects
										//li = lineIn.IndexOf(STARTdrawObject);
										li = utils.FastIndexOf(lineIn, STARTdrawObject);
										if (li > 0)
										{
										//TODO: Save it!	
										lastDrawObject = ParseDrawObject(lineIn);
										}
										else // Not a DrawObject
										{
											//! Viz Channel
											//li = lineIn.IndexOf(STARTvizChannel);
											li = utils.FastIndexOf(lineIn, STARTvizChannel);
											if (li > 0)
											{
												lastVizChannel = ParseVizChannel(lineIn);
												lastVizChannel.Parent = lastDrawObject;
												if (lastDrawObject.redChannel == null)
												{
													if (lastVizChannel.VizID == 1)
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
														if (lastVizChannel.VizID == 2)
														{
															lastDrawObject.isRGB = true;
															lastDrawObject.redChannel.rgbChild = RGBchild.Red;
															lastDrawObject.grnChannel = lastVizChannel;
															lastDrawObject.grnChannel.rgbChild = RGBchild.Green;
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
															if (lastVizChannel.VizID == 3)
															{
																lastDrawObject.bluChannel = lastVizChannel;
																lastDrawObject.bluChannel.rgbChild = RGBchild.Blue;
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
												//li = lineIn.IndexOf(STARTsample);
												li = utils.FastIndexOf(lineIn, STARTsample);
												if (li > 0)
												{
													//TODO Save It!
											}
												else // Not a Sample
												{
													//! Assigned Channel Groups
													//li = lineIn.IndexOf(STARTassignedChannelsGroup);
													li = utils.FastIndexOf(lineIn, STARTassignedChannelsGroup);
													if (li > 0)
													{
														//TODO Collect channels in group
													}
													else // Not an Assigned Channels Group
													{
														//! DrawPoint Groups
														//li = lineIn.IndexOf(STARTdrawPointsGroup);
														li = utils.FastIndexOf(lineIn, STARTdrawPointsGroup);
														if (li > 0)
														{
															//TODO: Collect DrawPoints in group
														} // end if a track
														else // not a track
														{
														} // end Track (or not)
													} // end Track Items (or not)
												} // end ChannelGroup (or not)
											} // end RGBchannel (or not)
										} // end regular Channel (or not)
										} // end timing (or not)
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

							// Restore these to the values we captured when first reading the file info header
							info.createdAt = creation;
							info.lastModified = info.file_saved;
							dirty = false;
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
				utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
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
				VizChannels = new List<VizChannel>();
				//Props = new List<Prop>();
				this.info = new Info(null);
				
				dirty = false;

			} // end Are You Sure
		} // end Clear Sequence

		public VizChannel ParseVizChannel(string lineIn)
		{
			VizChannel vch = new VizChannel("");
			//vch.SetParentViz(this);
			vch.SetIndex(VizChannels.Count);
			VizChannels.Add(vch);
			vch.Parse(lineIn);
			return vch;
		}

		public DrawObject ParseDrawObject(string lineIn)
		{
			DrawObject drob = new DrawObject("");
			//vch.SetParentViz(this);
			drob.SetIndex(DrawObjects.Count);
			DrawObjects.Add(drob);
			drob.Parse(lineIn);
			return drob;
		}

		public void MakeDirty()
		{
			dirty = true;
			info.lastModified = DateTime.Now; //.ToString("MM/dd/yyyy hh:mm:ss tt");
		}


	} // end Visualization class
} // end namespace LORUtils