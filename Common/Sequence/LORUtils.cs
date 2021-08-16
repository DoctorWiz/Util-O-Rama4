using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;
//using FuzzyString;

namespace LORUtils
{
	public class utils
	{
		public const int UNDEFINED = -1;
		public const string ICONtrack = "Track";
		public const string ICONchannelGroup = "ChannelGroup";
		//public const string ICONchannel = "channel";
		public const string ICONrgbChannel = "RGBchannel";
		//public const string ICONredChannel = "redChannel";
		//public const string ICONgrnChannel = "grnChannel";
		//public const string ICONbluChannel = "bluChannel";
		public const string ICONredChannel = "FF0000";
		public const string ICONgrnChannel = "00FF00";
		public const string ICONbluChannel = "0000FF";
		// Note: LOR colors not in the same order as .Net or Web colors, Red and Blue are reversed
		public const Int32 LORCOLOR_RED = 255;			// 0x0000FF
		public const Int32 LORCOLOR_GRN = 65280;		// 0x00FF00
		public const Int32 LORCOLOR_BLU = 16711680;	// 0xFF0000
		public const Int32 LORCOLOR_BLK = 0;
		public const Int32 LORCOLOR_WHT = 0xFFFFFF;

		public const int ADDshimmer = 0x200;
		public const int ADDtwinkle = 0x400;



		public const string LOG_Error = "Error";
		public const string LOG_Info = "Info";
		public const string LOG_Debug = "Debug";

		public static int nodeIndex = UNDEFINED;

		public const string TABLEchannel = "channel";
		public const string FIELDname = " name";
		public const string FIELDtype = " type";
		public const string FIELDsavedIndex = " savedIndex";
		public const string FIELDcentisecond = " centisecond";
		public const string FIELDcentiseconds = FIELDcentisecond + PLURAL;
		public const string FIELDtotalCentiseconds = " totalCentiseconds";
		public const string FIELDstartCentisecond = " startCentisecond";
		public const string FIELDendCentisecond = " endCentisecond";


		public const string SPC = " ";
		public const string LEVEL0 = "";
		//public const string LEVEL1 = "  ";
		//public const string LEVEL2 = "    ";
		//public const string LEVEL3 = "      ";
		//public const string LEVEL4 = "        ";
		//public const string LEVEL5 = "          ";
		public const string LEVEL1 = "\t";
		public const string LEVEL2 = "\t\t";
		public const string LEVEL3 = "\t\t\t";
		public const string LEVEL4 = "\t\t\t\t";
		public const string LEVEL5 = "\t\t\t\t\t";
		public const string CRLF = "\r\n";
		// Or, if you prefer tabs instead of spaces...
		//public const string LEVEL1 = "\t";
		//public const string LEVEL2 = "\t\t";
		//public const string LEVEL3 = "\t\t\t";
		//public const string LEVEL4 = "\t\t\t\t";
		public const string PLURAL = "s";
		public const string FIELDEQ = "=\"";
		public const string ENDQT = "\"";
		public const string STFLD = "<";
		public const string ENDFLD = "/>";
		public const string FINFLD = ">";
		public const string STTBL = "<";
		public const string FINTBL = "</";
		public const string ENDTBL = ">";

		public const string COMMA = ",";
		public const string SLASH = "/";
		public const char DELIM1 = '⬖';
		public const char DELIM2 = '⬘';
		public const char DELIM3 = '⬗';
		public const char DELIM4 = '⬙';
		private const char DELIM_Map = (char)164;  // ¤
		private const char DELIM_SID = (char)177;  // ±
		private const char DELIM_Name = (char)167;  // §
		private const char DELIM_X = (char)182;  // ¶


		public static void FillChannels(TreeView tree, Sequence4 seq, List<List<TreeNode>> siNodes, bool selectedOnly, bool includeRGBchildren)
		{
			int t = SeqEnums.MEMBER_Channel | SeqEnums.MEMBER_RGBchannel | SeqEnums.MEMBER_ChannelGroup | SeqEnums.MEMBER_Track;
			FillChannels(tree, seq, siNodes, selectedOnly, includeRGBchildren, t);
		}
		public static void FillChannels(TreeView tree, Sequence4 seq, List<List<TreeNode>> siNodes, bool selectedOnly, bool includeRGBchildren, int memberTypes)
		{
			//TODO: 'Selected' not implemented yet

			tree.Nodes.Clear();
			nodeIndex = 1;
			int listSize = seq.Members.HighestSavedIndex + seq.Tracks.Count + seq.TimingGrids.Count + 1;
			//Array.Resize(ref siNodes, listSize);

			const string ERRproc = " in FillChannels(";
			const string ERRtrk = "), in Track #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";



			for (int n = 0; n < listSize; n++)
			{
				//siNodes[n] = null;
				//siNodes[n] = new List<TreeNode>();
				siNodes.Add(new List<TreeNode>());
			}

			for (int t = 0; t < seq.Tracks.Count; t++)
			{
				TreeNode trackNode = AddTrack(seq, tree.Nodes, t, siNodes, selectedOnly, includeRGBchildren, memberTypes);
			}
		}

		private static TreeNode AddTrack(Sequence4 seq, TreeNodeCollection baseNodes, int trackNumber, List<List<TreeNode>> siNodes, bool selectedOnly, bool includeRGBchildren, int memberTypes)
		{

			string nodeText = "";
			bool inclChan = false;
			if ((memberTypes & SeqEnums.MEMBER_Channel) > 0) inclChan = true;
			bool inclRGB = false;
			if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0) inclRGB = true;

			// TEMPORARY, FOR DEBUGGING
			int tcount = 0;
			int gcount = 0;
			int rcount = 0;
			int ccount = 0;

			//try
			//{
			Track theTrack = seq.Tracks[trackNumber];
			nodeText = theTrack.Name;
			TreeNode trackNode = baseNodes.Add(nodeText);
			List<TreeNode> qlist;

			//int inclCount = theTrack.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
			//if (inclCount > 0)
			//{
			// Tracks don't normally have savedIndexes
			// But we will assign one for tracking and matching purposes
			//theTrack.SavedIndex = seq.Members.HighestSavedIndex + t + 1;

			//if ((memberTypes & SeqEnums.MEMBER_Track) > 0)
			//{
			baseNodes = trackNode.Nodes;
			nodeIndex++;
			trackNode.Tag = theTrack;

			trackNode.ImageKey = ICONtrack;
			trackNode.SelectedImageKey = ICONtrack;
			qlist = siNodes[theTrack.SavedIndex];
			qlist.Add(trackNode);
			//}

			for (int ti = 0; ti < theTrack.Members.Count; ti++)
			{
				//try
				//{
				IMember member = theTrack.Members.Items[ti];
				int si = member.SavedIndex;
				if (member != null)
				{
					if (member.MemberType == MemberType.ChannelGroup)
					{
						ChannelGroup memGrp = (ChannelGroup)member;
						int inclCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
						if (inclCount > 0)
						{
							TreeNode groupNode = AddGroup(seq, baseNodes, member.Index, siNodes, selectedOnly, includeRGBchildren, memberTypes);
							//AddNode(siNodes[si], groupNode);
							qlist = siNodes[si];
							qlist.Add(groupNode);
							gcount++;
							//siNodes[si].Add(groupNode);
						}
					}
					if (member.MemberType == MemberType.RGBchannel)
					{
						TreeNode rgbNode = AddRGBchannel(seq, baseNodes, member.Index, siNodes, selectedOnly, includeRGBchildren);
						//AddNode(siNodes[si], rgbNode);
						//siNodes[si].Add(rgbNode);
						qlist = siNodes[si];
						qlist.Add(rgbNode);
						rcount++;
					}
					if (member.MemberType == MemberType.Channel)
					{
						TreeNode channelNode = AddChannel(seq, baseNodes, member.Index, selectedOnly);
						//AddNode(siNodes[si], channelNode);
						//siNodes[si].Add(channelNode);
						qlist = siNodes[si];
						qlist.Add(channelNode);
						ccount++;
					}
				} // end not null
					//} // end try
				#region catch1
				/*	
				catch (System.NullReferenceException ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					catch (System.InvalidCastException ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					catch (Exception ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					*/
				#endregion

			} // end loop thru track items
			#region catch2 
			/*
				} // end try
				catch (System.NullReferenceException ex)
				{
					StackTrace st = new StackTrace(ex, true);
					StackFrame sf = st.GetFrame(st.FrameCount - 1);
					string emsg = ex.ToString();
					emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
					emsg += ERRline + sf.GetFileLineNumber();
					#if DEBUG
						System.Diagnostics.Debugger.Break();
					#endif
					Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
				}
				catch (System.InvalidCastException ex)
				{
					StackTrace st = new StackTrace(ex, true);
					StackFrame sf = st.GetFrame(st.FrameCount - 1);
					string emsg = ex.ToString();
					emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
					emsg += ERRline + sf.GetFileLineNumber();
					#if DEBUG
						System.Diagnostics.Debugger.Break();
					#endif
					Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
				}
				catch (Exception ex)
				{
					StackTrace st = new StackTrace(ex, true);
					StackFrame sf = st.GetFrame(st.FrameCount - 1);
					string emsg = ex.ToString();
					emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
					emsg += ERRline + sf.GetFileLineNumber();
					#if DEBUG
						System.Diagnostics.Debugger.Break();
					#endif
					Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
				}
				*/
			#endregion

		

				int x = 1; // Check ccount, rcount, gcount

			return trackNode;
		} // end fillOldChannels

		private static TreeNode AddGroup(Sequence4 seq, TreeNodeCollection baseNodes, int groupIndex, List<List<TreeNode>> siNodes, bool selectedOnly, bool includeRGBchildren, int memberTypes)
		{
			//ChanInfo nodeTag = new ChanInfo();
			//nodeTag.MemberType = MemberType.ChannelGroup;
			//nodeTag.objIndex = groupIndex;
			//nodeTag.SavedIndex = theGroup.SavedIndex;
			//nodeTag.nodeIndex = nodeIndex;

			ChannelGroup theGroup = seq.ChannelGroups[groupIndex];

			//IMember groupID = theGroup;
			string nodeText = theGroup.Name;
			TreeNode groupNode = null;

			if ((memberTypes & SeqEnums.MEMBER_ChannelGroup) > 0)
			{
				groupNode = baseNodes.Add(nodeText);

				nodeIndex++;
				groupNode.Tag = theGroup;
				groupNode.ImageKey = ICONchannelGroup;
				groupNode.SelectedImageKey = ICONchannelGroup;
				baseNodes = groupNode.Nodes;
			}
			List<TreeNode> qlist;

			const string ERRproc = " in FillChannels-AddGroup(";
			const string ERRgrp = "), in Group #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";

			for (int gi = 0; gi < theGroup.Members.Count; gi++)
			{
				//try
				//{
				IMember member = theGroup.Members.Items[gi];
				int si = member.SavedIndex;
				if (member.MemberType == MemberType.ChannelGroup)
				{
					ChannelGroup memGrp = (ChannelGroup)member;
					bool inclChan = false;
					if ((memberTypes & SeqEnums.MEMBER_Channel) > 0) inclChan = true;
					bool inclRGB = false;
					if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0) inclRGB = true;
					int inclCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
					if (inclCount > 0)
					{
						TreeNode subGroupNode = AddGroup(seq, baseNodes, member.Index, siNodes, selectedOnly, includeRGBchildren, memberTypes);
						qlist = siNodes[si];
						qlist.Add(subGroupNode);
					}
				}
				if (member.MemberType == MemberType.Channel)
				{
				if ((memberTypes & SeqEnums.MEMBER_Channel) > 0)
					{
						TreeNode channelNode = AddChannel(seq, baseNodes, member.Index, selectedOnly);
						qlist = siNodes[si];
						qlist.Add(channelNode);
					}
				}
				if (member.MemberType == MemberType.RGBchannel)
				{
					if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0)
					{
						TreeNode rgbChannelNode = AddRGBchannel(seq, baseNodes, member.Index, siNodes, selectedOnly, includeRGBchildren);
						qlist = siNodes[si];
						qlist.Add(rgbChannelNode);
					}
				}
				#region catch
				/*
	} // end try
		catch (Exception ex)
		{
			StackTrace st = new StackTrace(ex, true);
			StackFrame sf = st.GetFrame(st.FrameCount - 1);
			string emsg = ex.ToString();
			emsg += ERRproc + seq.filename + ERRgrp + groupIndex.ToString() + ERRitem + gi.ToString();
			emsg += ERRline + sf.GetFileLineNumber();
			#if DEBUG
				Debugger.Break();
			#endif
			Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
		} // end catch
		*/
				#endregion

			} // End loop thru items
			return groupNode;
		} // end AddGroup

		private static void AddNode(List<TreeNode> nodeList, TreeNode nOde)
		{
			nodeList.Add(nOde);
			/*
			if (nodeList == null)
			{
				//Array.Resize(ref nodeList, 1);
				nodeList[0].
 			}
			else
			{
				Array.Resize(ref nodeList, nodeList.Length + 1);
				nodeList[nodeList.Length - 1] = nOde;
			}
			*/
		}

		private static TreeNode AddChannel(Sequence4 seq, TreeNodeCollection baseNodes, int channelIndex, bool selectedOnly)
		{
			string nodeText = seq.Channels[channelIndex].Name;
			TreeNode channelNode = baseNodes.Add(nodeText);
			Channel theChannel = seq.Channels[channelIndex];
			//IMember nodeTag = theChannel;
			nodeIndex++;
			channelNode.Tag = theChannel;
			//channelNode.ImageIndex = imlTreeIcons.Images.IndexOfKey("Channel");
			//channelNode.SelectedImageIndex = imlTreeIcons.Images.IndexOfKey("Channel");
			//channelNode.ImageKey = ICONchannel;
			//channelNode.SelectedImageKey = ICONchannel;


			ImageList icons = baseNodes[0].TreeView.ImageList;
			int iconIndex = ColorIcon(icons, seq.Channels[channelIndex].color);
			channelNode.ImageIndex = iconIndex;
			channelNode.SelectedImageIndex = iconIndex;



			return channelNode;
		}

		private static TreeNode AddRGBchannel(Sequence4 seq, TreeNodeCollection baseNodes, int RGBIndex, List<List<TreeNode>> siNodes, bool selectedOnly, bool includeRGBchildren)
		{
			List<TreeNode> qlist;

			string nodeText = seq.RGBchannels[RGBIndex].Name;
			TreeNode channelNode = baseNodes.Add(nodeText);
			RGBchannel theRGB = seq.RGBchannels[RGBIndex];
			//IMember nodeTag = theRGB;
			nodeIndex++;
			channelNode.Tag = theRGB;
			channelNode.ImageKey = ICONrgbChannel;
			channelNode.SelectedImageKey = ICONrgbChannel;

			if (includeRGBchildren)
			{
				// * * R E D   S U B  C H A N N E L * *
				int ci = seq.RGBchannels[RGBIndex].redChannel.Index;
				nodeText = seq.Channels[ci].Name;
				TreeNode colorNode = channelNode.Nodes.Add(nodeText);
				//nodeTag = seq.Channels[ci];
				nodeIndex++;
				colorNode.Tag = seq.Channels[ci];
				colorNode.ImageKey = ICONredChannel;
				colorNode.SelectedImageKey = ICONredChannel;
				qlist = siNodes[seq.Channels[ci].SavedIndex];
				qlist.Add(colorNode);

				// * * G R E E N   S U B  C H A N N E L * *
				ci = seq.RGBchannels[RGBIndex].grnChannel.Index;
				nodeText = seq.Channels[ci].Name;
				colorNode = channelNode.Nodes.Add(nodeText);
				//nodeTag = seq.Channels[ci];
				nodeIndex++;
				colorNode.Tag = seq.Channels[ci];
				colorNode.ImageKey = ICONgrnChannel;
				colorNode.SelectedImageKey = ICONgrnChannel;
				qlist = siNodes[seq.Channels[ci].SavedIndex];
				qlist.Add(colorNode);

				// * * B L U E   S U B  C H A N N E L * *
				ci = seq.RGBchannels[RGBIndex].bluChannel.Index;
				nodeText = seq.Channels[ci].Name;
				colorNode = channelNode.Nodes.Add(nodeText);
				//nodeTag = seq.Channels[ci];
				nodeIndex++;
				colorNode.Tag = seq.Channels[ci];
				colorNode.ImageKey = ICONbluChannel;
				colorNode.SelectedImageKey = ICONbluChannel;
				qlist = siNodes[seq.Channels[ci].SavedIndex];
				qlist.Add(colorNode);
			} // end includeRGBchildren

			return channelNode;
		}

		public static int ColorIcon(ImageList icons, Int32 colorVal)
		{
			int ret = -1;
			string tempID = colorVal.ToString("X6");
			// LOR's Color Format is in BGR format, so have to reverse the Red and the Blue
			string colorID = tempID.Substring(4, 2) + tempID.Substring(2, 2) + tempID.Substring(0, 2);
			ret = icons.Images.IndexOfKey(colorID);
			if (ret < 2)
			{
				// Convert rearranged hex value a real color
				Color theColor = System.Drawing.ColorTranslator.FromHtml("#" + colorID);
				// Create a temporary working bitmap
				Bitmap bmp = new Bitmap(16,16);
				// get the graphics handle from it
				Graphics gr = Graphics.FromImage(bmp);
				// A colored solid brush to fill the middle
				SolidBrush b = new SolidBrush(theColor);
				// define a rectangle for the middle
				Rectangle r = new Rectangle(2, 2, 12, 12);
				// Fill the middle rectangle with color
				gr.FillRectangle(b, r);
				// Draw a 3D button around it
				Pen p = new Pen(Color.Black);
				gr.DrawLine(p, 1, 15, 15, 15);
				gr.DrawLine(p, 15, 1, 15, 14);
				p = new Pen(Color.DarkGray);
				gr.DrawLine(p, 2, 14, 14, 14);
				gr.DrawLine(p, 14, 2, 14, 13);
				p = new Pen(Color.White);
				gr.DrawLine(p, 0, 0, 15, 0);
				gr.DrawLine(p, 0, 1, 0, 15);
				p = new Pen(Color.LightGray);
				gr.DrawLine(p, 1, 1, 14, 1);
				gr.DrawLine(p, 1, 2, 1, 14);

				// Add it to the image list, using it's hex color code as the key
				icons.Images.Add(colorID, bmp);
				// get it's numeric index
				ret = icons.Images.Count - 1;
			}
			// Return the numeric index of the new image
			return ret;
		}

		public static string ShortenLongPath(string longPath, int maxLen)
		{
			//TODO I'm not too pleased with the current results of this function
			//TODO Try to make something better

			string shortPath = longPath;
			// Can't realistically shorten a path and filename to much less than 18 characters, reliably
			if (maxLen > 18)
			{
				// Do even need to shorten it all?
				if (longPath.Length > maxLen)
				{
					// Split it into pieces, get count
					string[] splits = longPath.Split('\\');
					int parts = splits.Length;
					int h = maxLen / 2;
					// loop thru, look for excessively long single pieces
					for (int i = 0; i < parts; i++)
					{
						if (splits[i].Length > h)
						{
							// Is it the filename itself that's too long?
							if (i == (parts - 1))
							{
								// if filename is too long, shorten it, but not as aggressively as a folder
								if (splits[i].Length > (maxLen * .7))
								{
									// shorten filename to "xxxxx…xxxx" (10 characters)
									// which should include .ext assuming a 3 char extension
									splits[i] = splits[i].Substring(0, 5) + "…" + splits[i].Substring(splits[i].Length - 4, 4);
								}
							}
							else
							{
								// shorten folder to "xxx…xxx" (7 characters)
								splits[i] = splits[i].Substring(0, 3) + "…" + splits[i].Substring(splits[i].Length - 3, 3);
							}
						}
					}

					// at minimum, we want the filename, lets start with that
					shortPath = splits[parts - 1];
					// figure out what drive it is on
					string drive = "";
					//byte b = 0;
					if (splits[0].Length == 2)
					{
						// Regular drive letter like C:
						drive = splits[0] + "\\";
						//b = 1;
					}
					if (splits[0].Length + splits[1].Length == 0)
					{
						// UNC path like //server/share
						drive = "\\\\" + splits[0] + "\\" + splits[1] + "\\";
						//b = 2;
					}
					// if drive + filename is still short enough, change to that
					if ((shortPath.Length + drive.Length + 2) <= maxLen)
					{
						shortPath = drive + "…\\" + shortPath;
					}
					// if drive + last folder + filename is still short enough, change to that
					if ((shortPath.Length + splits[parts - 2].Length + 1) <= maxLen)
					{
						shortPath = drive + "…\\" + splits[parts - 2] + "\\" + splits[parts - 1];
					}
					// if drive + first folder + last folder + filename is still short enough, change to that
					if ((shortPath.Length + splits[1].Length + 1) <= maxLen)
					{
						shortPath = drive + splits[1] + "\\…\\" + splits[parts - 2] + "\\" + splits[parts - 1];
					}
				} // end if (longPath.Length > maxLen)
			} // end if (maxLen > 18)
				// whatever we ended up with, return it
			return shortPath;
		} // end ShortenLongPath(string longPath, int maxLen)

		/*
		public static int FindName(string[] names, string Name)
		{
			return Array.BinarySearch(names, Name);
		} // end FindName

		public static int FuzzyFindName(string[] names, string Name)
		{
			return FuzzyFindName(names, Name, .8, .95);
		}

		public static int FuzzyFindName(string[] names, string Name, double preMatchMin, double finalMatchMin)
		{
			// names[] do not have to be sorted (although they can be)

			int ret = UNDEFINED;  // default value, no match
			int[] preIdx = null;
			double[] finalMatchVals = null;
			int preMatchCount = 0;
			double matchVal = 0;

			// Loop thu ALL names
			for (int n=0; n< names.Length; n++)
			{
				// calculate a quick prematch value
				matchVal = names[n].LevenshteinDistance(Name);
				// if above the minimum
				if (matchVal > preMatchMin)
				{
					// add to array of prematches
					Array.Resize(ref preIdx, preMatchCount + 1);
					preIdx[preMatchCount] = n;
					preMatchCount++;
				}
			}
			// any prematches found?
			if (preMatchCount > 0)
			{
				// size array to hold final match values
				Array.Resize(ref finalMatchVals, preMatchCount);
				// loop thru the prematches
				for (int nn = 0; nn < preMatchCount; nn++)
				{
					// calculate and remember the final match value
					finalMatchVals[nn] = names[preIdx[nn]].RankEquality(Name);
				}
				// sort the prematches by their final match value
				Array.Sort(finalMatchVals, preIdx);
				// Is the last one (which will have the highest final match value once sorted)
				// above the final minimum?
				if (finalMatchVals[preMatchCount - 1] > finalMatchMin)
				{
					// set return value to it's index
					ret = preIdx[preMatchCount - 1];
				}
			}
			// return index of best match if found,
			// default of undefined if not
			return ret;
		} // end FuzzyFindName
		*/

		public static int InvalidCharacterCount(string testName)
		{
			int ret = 0;
			int pos1 = 0;
			int pos2 = UNDEFINED;

			// These are not valid anywhere
			char[] badChars = { '<', '>', '\"', '/', '|', '?', '*' };
			for (int c = 0; c < badChars.Length; c++)
			{
				pos1 = 0;
				pos2 = testName.IndexOf(badChars[c], pos1);
				while (pos2 > UNDEFINED)
				{
					ret++;
					pos1 = pos2 + 1;
					pos2 = testName.IndexOf(badChars[c], pos1);
				}
			}

			// and the colon is not valid past position 2
			pos1 = 2;
			pos2 = testName.IndexOf(':', pos1);
			while (pos2 > UNDEFINED)
			{
				ret++;
				pos1 = pos2 + 1;
				pos2 = testName.IndexOf(':', pos1);
			}

			return ret;
		}

		private static string NotifyGenericSound
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\AppEvents\\Schemes\\Apps\\.Default\\Notification.Default";
				string sound = (string)Registry.GetValue(keyName, null, "");
				return sound;
			} // End get the Notify.Generic Sound filename
		}

		public static void PlayNotifyGenericSound()
		{
			string sound = NotifyGenericSound;
			if (sound.Length > 6)
			{
				if (File.Exists(sound))
				{
					SoundPlayer player = new SoundPlayer(sound);
					player.Play();
				}
			}
		}

		private static string MenuClickSound
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\AppEvents\\Schemes\\Apps\\.Default\\Notification.Default";
				string sound = (string)Registry.GetValue(keyName, null, "");
				return sound;
			} // End get the Notify.Generic Sound filename
		}

		public static void PlayMenuClickSound()
		{
			string sound = MenuClickSound;
			if (sound.Length > 6)
			{
				if (File.Exists(sound))
				{
					SoundPlayer player = new SoundPlayer(sound);
					player.Play();
				}
			}
		}





		public static void WriteLogEntry(string message, string logType, string applicationName)
		{
			string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string mySubDir = "\\UtilORama\\";
			if (applicationName.Length > 2)
			{
				applicationName.Replace("-", "");
				mySubDir += applicationName + "\\";
			}
			string file = appDataDir + mySubDir;
			if (!Directory.Exists(file)) Directory.CreateDirectory(file);
			file += logType + ".log";
			//string dt = DateTime.Now.ToString("yyyy-MM-dd ")
			string dt = DateTime.Now.ToString("s") + "=";
			string msgLine = dt + message;
			try
			{
				StreamWriter writer = new StreamWriter(file, append: true);
				writer.WriteLine(msgLine);
				writer.Flush();
				writer.Close();
			}
			catch (System.IO.IOException ex)
			{
				string errMsg = "An error has occured in this application!\r\n";
				errMsg += "Another error has occured while trying to write the details of the first error to a log file!\r\n\r\n";
				errMsg += "The first error was: " + message + "\r\n";
				errMsg += "The second error was: " + ex.ToString();
				errMsg += "The log file is: " + file;
				DialogResult dr = MessageBox.Show(errMsg, "Errors!", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
			}
			finally
			{
			}

		} // end write log entry


		public static string DefaultUserDataPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "UserDataPath", userDocs);
				return fldr;
			} // End get UserDataPath
		}

		public static string DefaultNonAudioPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "NonAudioPath", userDocs);
				return fldr;
			} // End get NonAudioPath (Sequences)
		}

		public static string DefaultSequencesPath
		{
			get
			{
				return DefaultNonAudioPath;
			}
		}


		public static string DefaultAudioPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "AudioPath", userDocs);
				return fldr;
			} // End get AudioPath
		}

		public static string DefaultClipboardsPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "ClipboardsPath", userDocs);
				return fldr;
			} // End get ClipboardsPath
		}

		public static string DefaultChannelConfigsPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "ChannelConfigsPath", "");
				if (fldr.Length < 5)
				{
					fldr = DefaultNonAudioPath + "ChannelConfigs\\";
					if (fldr.Length < 18)
					{
						fldr = userDocs;
					}
					else
					{
						Registry.SetValue(keyName, "ChannelConfigsPath", fldr, RegistryValueKind.String);
					}
				}
				if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);
				return fldr;
			} // End get ChannelConfigsPath
		}

		public static string fileCreatedAt(string filename)
		{
			string ret = "";
			if (File.Exists(filename))
			{
				DateTime dt = File.GetCreationTime(filename);
				ret = dt.ToString("MM/dd/yyyy hh:mm:ss tt");
			}
			return ret;
		}

		public static DateTime fileCreatedDateTime(string filename)
		{
			DateTime ret = new DateTime();
			if (File.Exists(filename))
			{
				ret = File.GetCreationTime(filename);
			}
			return ret;
		}

		public static string fileModiedAt(string filename)
		{
			string ret = "";
			if (File.Exists(filename))
			{
				DateTime dt = File.GetLastWriteTime(filename);
				ret = dt.ToString("MM/dd/yyyy hh:mm:ss tt");
			}
			return ret;
		}

		public static DateTime fileModiedDateTime(string filename)
		{
			DateTime ret = new DateTime();
			if (File.Exists(filename))
			{
				ret = File.GetLastWriteTime(filename);
			}
			return ret;
		}

		public static string fileSizeFormatted(string filename)
		{
			string ret = "0";
			if (File.Exists(filename))
			{
				FileInfo fi = new FileInfo(filename);
				long l = fi.Length;
				if (l < 1024)
				{
					ret = l.ToString() + " bytes";
				}
				else
				{
					if (l < 1048576)
					{
						ret = (l / 1024).ToString() + " KB";
					}
					else
					{
						if (l < 1073741824)
						{
							ret = (l / 1048576).ToString() + " MB";
						}
						else
						{
							if (l < 1099511627776)
							{
								ret = (l / 1073741824).ToString() + " GB";
							}
						}
					}
				}
			}
			return ret;
		}

		public static long fileSize(string filename)
		{
				long ret = 0;
				if (File.Exists(filename))
				{
					FileInfo fi = new FileInfo(filename);
					ret = fi.Length;
				}
				return ret;
		}

		public static string cleanName(string dirtyName)
		{
			string ret = dirtyName;
			ret = ret.Replace("&quot", "\"");
			return ret;
		}

		public static string dirtyName(string cleanName)
		{
			string ret = cleanName;
			ret = ret.Replace("\"", "&quot");
			return ret;
		}

		public static Int32 getKeyValue(string lineIn, string keyWord)
		{
			Int32 valueOut = UNDEFINED;
			int pos1 = UNDEFINED;
			int pos2 = UNDEFINED;
			string fooo = "";

			pos1 = lineIn.IndexOf(keyWord + "=");
			if (pos1 > 0)
			{
				fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
				pos2 = fooo.IndexOf("\"");
				fooo = fooo.Substring(0, pos2);
				valueOut = Convert.ToInt32(fooo);
			}
			else
			{
				valueOut = UNDEFINED;
			}

			return valueOut;
		}

		public static string getKeyWord(string lineIn, string keyWord)
		{
			string valueOut = "";
			int pos1 = UNDEFINED;
			int pos2 = UNDEFINED;
			string fooo = "";

			pos1 = lineIn.IndexOf(keyWord + "=");
			if (pos1 > 0)
			{
				fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
				pos2 = fooo.IndexOf("\"");
				fooo = fooo.Substring(0, pos2);
				valueOut = fooo;
			}
			else
			{
				valueOut = "";
			}

			return valueOut;
		}


		public static int BuildDisplayOrder(Sequence4 seq, ref int[] savedIndexes, ref int[] levels, bool selectedOnly, bool includeRGBchildren)
		{
			//TODO: 'Selected' not implemented yet

			int count = 0;
			int c = 0;
			int level = 0;
			int tot = seq.Tracks.Count + seq.Channels.Count + seq.ChannelGroups.Count;
			if (includeRGBchildren)
			{
				tot += seq.RGBchannels.Count;
			}
			else
			{
				// Why * 2? 'cus we won't show the 3 Members, but will show the parent.  3-1=2.
				tot -= (seq.RGBchannels.Count * 2);
			}
			//Array.Resize(ref savedIndexes, tot);
			//Array.Resize(ref levels, tot);




			// TEMPORARY, FOR DEBUGGING
			int tcount = 0;
			int gcount = 0;
			int rcount = 0;
			int ccount = 0;

			const string ERRproc = " in FillChannels(";
			const string ERRtrk = "), in Track #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";

			for (int t = 0; t < seq.Tracks.Count; t++)
			{
				level = 0;
				Track theTrack = seq.Tracks[t];
				if (!selectedOnly || theTrack.Selected)
				{
					Array.Resize(ref savedIndexes, count + 1);
					Array.Resize(ref levels, count + 1);
					savedIndexes[count] = theTrack.SavedIndex;
					levels[count] = level;
					count++;
					c++;
				}
				//try
				//{
				for (int ti = 0; ti < theTrack.Members.Count; ti++)
				{
					//try
					//{
					IMember member = theTrack.Members.Items[ti];
					int si = member.SavedIndex;
					if (member != null)
					{
						if (member.MemberType == MemberType.ChannelGroup)
						{
							c += BuildGroup(seq, (ChannelGroup)member, level+1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
						}
						if (member.MemberType == MemberType.RGBchannel)
						{
							c += BuildRGBchannel(seq, (RGBchannel)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
						}
						if (member.MemberType == MemberType.Channel)
						{
							c += BuildChannel(seq, (Channel)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly);
						}
					} // end not null
						//} // end try
					#region catch1
					/*	
					catch (System.NullReferenceException ex)
						{
							StackTrace st = new StackTrace(ex, true);
							StackFrame sf = st.GetFrame(st.FrameCount - 1);
							string emsg = ex.ToString();
							emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
							emsg += ERRline + sf.GetFileLineNumber();
							#if DEBUG
								System.Diagnostics.Debugger.Break();
							#endif
							Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
						}
						catch (System.InvalidCastException ex)
						{
							StackTrace st = new StackTrace(ex, true);
							StackFrame sf = st.GetFrame(st.FrameCount - 1);
							string emsg = ex.ToString();
							emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
							emsg += ERRline + sf.GetFileLineNumber();
							#if DEBUG
								System.Diagnostics.Debugger.Break();
							#endif
							Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
						}
						catch (Exception ex)
						{
							StackTrace st = new StackTrace(ex, true);
							StackFrame sf = st.GetFrame(st.FrameCount - 1);
							string emsg = ex.ToString();
							emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
							emsg += ERRline + sf.GetFileLineNumber();
							#if DEBUG
								System.Diagnostics.Debugger.Break();
							#endif
							Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
						}
						*/
					#endregion

				} // end loop thru track items
				#region catch2 
				/*
					} // end try
					catch (System.NullReferenceException ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					catch (System.InvalidCastException ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					catch (Exception ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					*/
				#endregion

			} // end loop thru Tracks

			// Integrity Checks for debugging
			if (c != count)
			{
				string msg = "Houston, we have a problem!";
			}
			if (!selectedOnly)
			{
				if (c != tot)
				{
					string msg = "Houston, we have another problem!";
				}
			}



			return c;

		} // end fillOldChannels

		public static int BuildGroup(Sequence4 seq, ChannelGroup theGroup, int level, ref int count, ref int[] savedIndexes, ref int[]levels, bool selectedOnly, bool includeRGBchildren)
		{
			int c = 0;
			string nodeText = theGroup.Name;

			const string ERRproc = " in FillChannels-AddGroup(";
			const string ERRgrp = "), in Group #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";

			if (!selectedOnly || theGroup.Selected)
			{
				Array.Resize(ref savedIndexes, count + 1);
				Array.Resize(ref levels, count + 1);
				savedIndexes[count] = theGroup.SavedIndex;
				levels[count] = level;
				count++;
				c++;
			}

			for (int gi = 0; gi < theGroup.Members.Count; gi++)
			{
				//try
				//{
				IMember member = theGroup.Members.Items[gi];
				int si = member.SavedIndex;
				if (member.MemberType == MemberType.ChannelGroup)
				{
					c += BuildGroup(seq, (ChannelGroup)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
				}
				if (member.MemberType == MemberType.Channel)
				{
					c += BuildChannel(seq, (Channel)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly);
				}
				if (member.MemberType == MemberType.RGBchannel)
				{
					c += BuildRGBchannel(seq, (RGBchannel)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
				}
				#region catch
				/*
	} // end try
		catch (Exception ex)
		{
			StackTrace st = new StackTrace(ex, true);
			StackFrame sf = st.GetFrame(st.FrameCount - 1);
			string emsg = ex.ToString();
			emsg += ERRproc + seq.filename + ERRgrp + groupIndex.ToString() + ERRitem + gi.ToString();
			emsg += ERRline + sf.GetFileLineNumber();
			#if DEBUG
				Debugger.Break();
			#endif
			Fyle.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
		} // end catch
		*/
				#endregion

			} // End loop thru items
			return c;
		} // end AddGroup

		public static int BuildChannel(Sequence4 seq, Channel theChannel, int level, ref int count, ref int[] savedIndexes, ref int[] levels, bool selectedOnly)
		{
			int c = 0;
			if (!selectedOnly || theChannel.Selected) 
			{
				Array.Resize(ref savedIndexes, count + 1);
				Array.Resize(ref levels, count + 1);
				savedIndexes[count] = theChannel.SavedIndex;
				levels[count] = level;
				count++;
				c++;
			}
			return c;
		}

		public static int BuildRGBchannel(Sequence4 seq, RGBchannel theRGB, int level, ref int count, ref int[] savedIndexes, ref int[] levels, bool selectedOnly, bool includeRGBchildren)
		{
			int c = 0;

			if (!selectedOnly || theRGB.Selected)
			{
				Array.Resize(ref savedIndexes, count + 1);
				Array.Resize(ref levels, count + 1);
				savedIndexes[count] = theRGB.SavedIndex;
				levels[count] = level;
				count++;
				c++;
				if (includeRGBchildren)
				{
					Array.Resize(ref savedIndexes, count + 3);
					Array.Resize(ref levels, count + 3);
					// * * R E D   S U B  C H A N N E L * *
					savedIndexes[count] = theRGB.redChannel.SavedIndex;
					levels[count] = level + 1;
					count++;

					// * * G R E E N   S U B  C H A N N E L * *
					savedIndexes[count] = theRGB.grnChannel.SavedIndex;
					levels[count] = level + 1;
					count++;

					// * * B L U E   S U B  C H A N N E L * *
					savedIndexes[count] = theRGB.bluChannel.SavedIndex;
					levels[count] = level + 1;
					count++;

					c += 3;
				} // end includeRGBchildren
			}

			return c;
		}

		public static void ClearOutputWindow()
		{
			if (!Debugger.IsAttached)
			{
				return;
			}

			/*
			//Application.DoEvents();  // This is for Windows.Forms.
			// This delay to get it to work. Unsure why. See http://stackoverflow.com/questions/2391473/can-the-visual-studio-debug-output-window-be-programatically-cleared
			Thread.Sleep(1000);
			// In VS2008 use EnvDTE80.DTE2
			EnvDTE.DTE ide = (EnvDTE.DTE)Marshal.GetActiveObject("VisualStudio.DTE.14.0");
			if (ide != null)
			{
				ide.ExecuteCommand("Edit.ClearOutputWindow", "");
				Marshal.ReleaseComObject(ide);
			}
			*/
		}

		public static string GetAppTempFolder()
		{

			string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string mySubDir = "\\UtilORama\\";
			string tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			mySubDir += Application.ProductName + "\\";
			mySubDir = mySubDir.Replace("-", "");
			tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			return tempPath;
		}

		public static Color Color_LORtoNet(Int32 LORcolorVal)
		{
			string colorID = Color_LORtoHTML(LORcolorVal);
			// Convert rearranged hex value a real color
			Color theColor = System.Drawing.ColorTranslator.FromHtml("#" + colorID);
			return theColor;
		}

		public static string Color_LORtoHTML(Int32 LORcolorVal)
		{
			string tempID = LORcolorVal.ToString("X6");
			// LOR's Color Format is in BGR format, so have to reverse the Red and the Blue
			string colorID = tempID.Substring(4, 2) + tempID.Substring(2, 2) + tempID.Substring(0, 2);
			return colorID;
		}

		public static Int32 Color_NettoLOR(Color netColor)
		{
			int argb = netColor.ToArgb();
			string tempID = argb.ToString("X6");
			// LOR's Color Format is in BGR format, so have to reverse the Red and the Blue
			string colorID = tempID.Substring(4, 2) + tempID.Substring(2, 2) + tempID.Substring(0, 2);
			Int32 c = Int32.Parse(colorID, System.Globalization.NumberStyles.HexNumber);
			return c;
		}

		public static Int32 Color_HTMLtoLOR(string HTMLcolor)
		{
			// LOR's Color Format is in BGR format, so have to reverse the Red and the Blue
			string colorID = HTMLcolor.Substring(4, 2) + HTMLcolor.Substring(2, 2) + HTMLcolor.Substring(0, 2);
			Int32 c = Int32.Parse(colorID, System.Globalization.NumberStyles.HexNumber);
			return c;
		}

		public static string FormatTime(int centiseconds)
		{
			string timeOut = "";

			int cs = (int)(centiseconds % 100);
			int r = (int)((centiseconds - cs) / 100);
			int sec = r % 60;
			int min = (r - sec) / 60;

			if (min>0)
			{
				timeOut = min.ToString() + ":";
				timeOut += sec.ToString("00");
			}
			else
			{
				timeOut = sec.ToString();
			}
			timeOut += "." + cs.ToString("00");

			return timeOut;
		}

		public static int DecodeTime(string theTime)
		{
			// format mm:ss.cs
			int csOut = UNDEFINED;
			int csTmp = UNDEFINED;

			// Split time by :
			string[] tmpM = theTime.Split(':');
			// Not more than 1 : ?
			if (tmpM.Length < 3)
			{
				// has a : ?
				if (tmpM.Length == 2)
				{
					// first part is minutes
					int min = 0;
					// try to parse minutes from first part of string
					int.TryParse(tmpM[0], out min);
					// each minute is 6000 centiseconds
					csTmp = min * 6000;
					// place second part of split into first part for next step of decoding
					tmpM[0] = tmpM[1];
				}
				// split seconds by . ?
				string[] tmpS = tmpM[0].Split('.');
				// not more than 1 . ?
				if (tmpS.Length < 3)
				{
					// has a . ?
					if (tmpS.Length == 2)
					{
						// next part is seconds
						int sec = 0;
						// try to parse seconds from first part of remaining string
						int.TryParse(tmpS[0], out sec);
						// each second is 100 centiseconds (duh!)
						csTmp += (sec * 100);
						// no more than 2 decimal places allowed
						if (tmpS[1].Length > 2)
						{
							tmpS[1] = tmpS[1].Substring(0, 2);
						}
						// place second part into first part for next step of decoding
						tmpS[0] = tmpS[1];
					}
					int cs = 0;
					int.TryParse(tmpS[0], out cs);
					csTmp += cs;
					csOut = csTmp;
				}
			}



			return csOut;
		}


		public struct RGB
		{
			private byte _r;
			private byte _g;
			private byte _b;

			public RGB(byte r, byte g, byte b)
			{
				this._r = r;
				this._g = g;
				this._b = b;
			}

			public byte R
			{
				get { return this._r; }
				set { this._r = value; }
			}

			public byte G
			{
				get { return this._g; }
				set { this._g = value; }
			}

			public byte B
			{
				get { return this._b; }
				set { this._b = value; }
			}

			public bool Equals(RGB rgb)
			{
				return (this.R == rgb.R) && (this.G == rgb.G) && (this.B == rgb.B);
			}
		}

		public struct HSV
		{
			private double _h;
			private double _s;
			private double _v;

			public HSV(double h, double s, double v)
			{
				this._h = h;
				this._s = s;
				this._v = v;
			}

			public double H
			{
				get { return this._h; }
				set { this._h = value; }
			}

			public double S
			{
				get { return this._s; }
				set { this._s = value; }
			}

			public double V
			{
				get { return this._v; }
				set { this._v = value; }
			}

			public bool Equals(HSV hsv)
			{
				return (this.H == hsv.H) && (this.S == hsv.S) && (this.V == hsv.V);
			}
		}

		public static Int32 HSVToRGB(HSV hsv)
		{
			double r = 0, g = 0, b = 0;

			if (hsv.S == 0)
			{
				r = hsv.V;
				g = hsv.V;
				b = hsv.V;
			}
			else
			{
				int i;
				double f, p, q, t;

				if (hsv.H == 360)
					hsv.H = 0;
				else
					hsv.H = hsv.H / 60;

				i = (int)Math.Truncate(hsv.H);
				f = hsv.H - i;

				p = hsv.V * (1.0 - hsv.S);
				q = hsv.V * (1.0 - (hsv.S * f));
				t = hsv.V * (1.0 - (hsv.S * (1.0 - f)));

				switch (i)
				{
					case 0:
						r = hsv.V;
						g = t;
						b = p;
						break;

					case 1:
						r = q;
						g = hsv.V;
						b = p;
						break;

					case 2:
						r = p;
						g = hsv.V;
						b = t;
						break;

					case 3:
						r = p;
						g = q;
						b = hsv.V;
						break;

					case 4:
						r = t;
						g = p;
						b = hsv.V;
						break;

					default:
						r = hsv.V;
						g = p;
						b = q;
						break;
				}

			}

			RGB x = new RGB((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
			Int32 ret = x.R * 0x10000 + x.G * 0x100 + x.B;
			return ret;
		}

		public static Bitmap RenderEffects(IMember member, int startCentiseconds, int endCentiseconds, int width, int height, bool useRamps)
		{
			// Create a temporary working bitmap
			Bitmap bmp = new Bitmap(width, height);
			if (member.MemberType == MemberType.Channel)
			{
				Channel channel = (Channel)member;
				bmp = RenderEffects(channel, startCentiseconds, endCentiseconds, width, height, useRamps);
			}
			if (member.MemberType == MemberType.RGBchannel)
			{
				RGBchannel rgb = (RGBchannel)member;
				bmp = RenderEffects(rgb, startCentiseconds, endCentiseconds, width, height, useRamps);
			}

			return bmp;

		}

		public static Bitmap RenderEffects(Channel channel, int startCentiseconds, int endCentiseconds, int width, int height, bool useRamps)
		{
			// Create a temporary working bitmap
			Bitmap bmp = new Bitmap(width, height);
			// get the graphics handle from it
			Graphics gr = Graphics.FromImage(bmp);
			// Paint the entire 'background' black
			gr.FillRectangle(Brushes.Silver, 0, 0, width - 1, height - 1);
			Color c = Color_LORtoNet(channel.color);
			Pen p = new Pen(c, 1);
			Brush br = new SolidBrush(c);
			Debug.WriteLine(""); Debug.WriteLine("");
			if (channel.effects.Count > 0)
			{
				int[] levels = PlotLevels(channel, startCentiseconds, endCentiseconds, width);
				for (int x=0; x < width; x++)
				{
					Debug.Write(levels[x].ToString() + " ");
					bool shimmer = ((levels[x] & utils.ADDshimmer) > 0);
					bool twinkle = ((levels[x] & utils.ADDtwinkle) > 0);
					levels[x] &= 0x0FF;
					if (useRamps)
					{
						//int lineLen = levels[x] * Convert.ToInt32((float)height / 100D);
						int ll = levels[x] * height;
						int lineLen = ll / 100 + 1;
						if (shimmer)
						{
							for (int n=0; n< lineLen; n++)
							{
								int m = (n + x) % 6;
								if (m <2)
								{
									gr.FillRectangle(br, x, height - n, 1, 1);
								}
							}
						}
						else if (twinkle)
						{
							for (int n = 0; n < lineLen; n++)
							{
								int m = (n + x) % 6;
								if (m < 1)
								{
									gr.FillRectangle(br, x, height - n, 1, 1);
								}
								m = (x - n + 25000) % 6;
								if (m < 1)
								{
									gr.FillRectangle(br, x, height - n, 1, 1);
								}

							}
						}
						else
						{
							gr.DrawLine(p, x, height - 1, x, height - lineLen);
						}
					}
					else // use fades instead of ramps
					{
						int R = (Color.Silver.R * (100 - levels[x]) / 100) + (c.R * levels[x] / 100);
						int G = (Color.Silver.G * (100 - levels[x]) / 100) + (c.G * levels[x] / 100);
						int B = (Color.Silver.B * (100 - levels[x]) / 100) + (c.B * levels[x] / 100);
						Color d = Color.FromArgb(R, G, B);
						p = new Pen(d, 1);
						br = new SolidBrush(d);
						if (shimmer)
						{
							for (int n = 0; n < height; n++)
							{
								int m = (n + x) % 6;
								if (m < 2)
								{
									gr.FillRectangle(br, x, height - n, 1, 1);
								}
							}
						}
						else if (twinkle)
						{
							for (int n = 0; n < height; n++)
							{
								int m = (n + x) % 6;
								if (m < 1)
								{
									gr.FillRectangle(br, x, height - n, 1, 1);
								}
								m = (x - n + 25000) % 6;
								if (m < 1)
								{
									gr.FillRectangle(br, x, height - n, 1, 1);
								}

							}
						}
						else
						{
							gr.DrawLine(p, x, 0, x, height - 1);
						}
					}
				}
			} // end channel has effects

			return bmp;
		}

		public static Bitmap RenderEffects(RGBchannel rgb, int startCentiseconds, int endCentiseconds, int width, int height, bool useRamps)
		{
			// Create a temporary working bitmap
			Bitmap bmp = new Bitmap(width, height);
			// get the graphics handle from it
			Graphics gr = Graphics.FromImage(bmp);
			// Paint the entire 'background' black
			//gr.FillRectangle(Brushes.Black, 0, 0, width - 1, height - 1);

			int[] rLevels = null;
			Array.Resize(ref rLevels, width);
			int[] gLevels = null;
			Array.Resize(ref gLevels, width);
			int[] bLevels = null;
			Array.Resize(ref bLevels, width);
			int thirdHt = height / 3;

			if (rgb.redChannel.effects.Count > 0)
			{
				rLevels = PlotLevels(rgb.redChannel, startCentiseconds, endCentiseconds, width);
			}
			if (rgb.grnChannel.effects.Count > 0)
			{
				gLevels = PlotLevels(rgb.grnChannel, startCentiseconds, endCentiseconds, width);
			}
			if (rgb.bluChannel.effects.Count > 0)
			{
				bLevels = PlotLevels(rgb.bluChannel, startCentiseconds, endCentiseconds, width);
			}

			for (int x = 0; x < width; x++)
			{
				//Debug.Write(levels[x].ToString() + " ");
				//bool shimmer = ((levels[x] & utils.ADDshimmer) > 0);
				//bool twinkle = ((levels[x] & utils.ADDtwinkle) > 0);
				//levels[x] &= 0x0FF;
				if (useRamps)
				{
					// * * R E D * *
					Pen p = new Pen(Color.Red, 1);
					Brush br = new SolidBrush(Color.Red);
					bool shimmer = ((rLevels[x] & utils.ADDshimmer) > 0);
					bool twinkle = ((rLevels[x] & utils.ADDtwinkle) > 0);
					rLevels[x] &= 0x0FF;
					int ll = rLevels[x] * thirdHt;
					int lineLen = ll / 100 + 1;
					if (shimmer)
					{
						for (int n = 0; n < lineLen; n++)
						{
							int m = (n + x) % 6;
							if (m < 2)
							{
								gr.FillRectangle(br, x, height - n, 1, 1);
							}
						}
					}
					else if (twinkle)
					{
						for (int n = 0; n < lineLen; n++)
						{
							int m = (n + x) % 6;
							if (m < 1)
							{
								gr.FillRectangle(br, x, height - n, 1, 1);
							}
							m = (x - n + 25000) % 6;
							if (m < 1)
							{
								gr.FillRectangle(br, x, height - n, 1, 1);
							}

						}
					}
					else
					{
						gr.DrawLine(p, x, height - 1, x, height - lineLen);
					}
					// END RED

					// * * G R E E N * *
					p = new Pen(Color.Green, 1);
					br = new SolidBrush(Color.Green);
					shimmer = ((rLevels[x] & utils.ADDshimmer) > 0);
					twinkle = ((rLevels[x] & utils.ADDtwinkle) > 0);
					rLevels[x] &= 0x0FF;
					ll = rLevels[x] * thirdHt;
					lineLen = ll / 100 + 1;
					if (shimmer)
					{
						for (int n = 0; n < lineLen; n++)
						{
							int m = (n + x) % 6;
							if (m < 2)
							{
								gr.FillRectangle(br, x, thirdHt + height - n, 1, 1);
							}
						}
					}
					else if (twinkle)
					{
						for (int n = 0; n < lineLen; n++)
						{
							int m = (n + x) % 6;
							if (m < 1)
							{
								gr.FillRectangle(br, x, thirdHt + height - n, 1, 1);
							}
							m = (x - n + 25000) % 6;
							if (m < 1)
							{
								gr.FillRectangle(br, x, thirdHt + height - n, 1, 1);
							}

						}
					}
					else
					{
						gr.DrawLine(p, x, height - 1, x, thirdHt + height - lineLen);
					}
					// END GREEN

					// * * B L U E * *
					p = new Pen(Color.Red, 1);
					br = new SolidBrush(Color.Red);
					shimmer = ((rLevels[x] & utils.ADDshimmer) > 0);
					twinkle = ((rLevels[x] & utils.ADDtwinkle) > 0);
					rLevels[x] &= 0x0FF;
					ll = rLevels[x] * thirdHt;
					lineLen = ll / 100 + 1;
					if (shimmer)
					{
						for (int n = 0; n < lineLen; n++)
						{
							int m = (n + x) % 6;
							if (m < 2)
							{
								gr.FillRectangle(br, x, thirdHt + thirdHt + height - n, 1, 1);
							}
						}
					}
					else if (twinkle)
					{
						for (int n = 0; n < lineLen; n++)
						{
							int m = (n + x) % 6;
							if (m < 1)
							{
								gr.FillRectangle(br, x, thirdHt + thirdHt + height - n, 1, 1);
							}
							m = (x - n + 25000) % 6;
							if (m < 1)
							{
								gr.FillRectangle(br, x, height - n, 1, 1);
							}

						}
					}
					else
					{
						gr.DrawLine(p, x, height - 1, x, thirdHt + thirdHt + height - lineLen);
					}
					// END BLUE

				}
				else // use fades instead of ramps
				{
					rLevels[x] = (int)((float)rLevels[x] * 2.55F);
					gLevels[x] = (int)((float)gLevels[x] * 2.55F);
					bLevels[x] = (int)((float)bLevels[x] * 2.55F);

					Color c = Color.FromArgb(rLevels[x], gLevels[x], bLevels[x]);
					Pen p = new Pen(c, 1);
					gr.DrawLine(p, x, 0, x, height - 1);
				}
			} // end For
			return bmp;
		}

		public static int[] PlotLevels(Channel channel, int startCentiseconds, int endCentiseconds, int width)
		{
			int[] levels = null;
			Array.Resize(ref levels, width);

			int totalCentiseconds = endCentiseconds - startCentiseconds + 1;
			// centisecondsPerPixel = totalCentiseconds / width;
			float cspp = (float)totalCentiseconds / (float)width;
			int curCs = 0;
			int lastCs = 0;
			int curLevel = 0;
			int effectIdx = 0;
			bool keepGoing = true;
			int thisClik = 0;
			int nextClik = width;
			Effect curEffect = channel.effects[effectIdx];


			if (cspp >= 1.0F)
			{
				for (int x = 0; x < width; x++)
				{
					keepGoing = true;
					while (keepGoing)
					{
						// at how many centiseconds does this column represent
						//     Convert.ToInt provides rounding
						//      Whereas casting with (int) does not  (per StackExchange)
						thisClik = Convert.ToInt32(cspp * (float)x);
						// if not to the end
						if (x < width - 2)
						{
							// how many centiseconds does the next column represent
							nextClik = Convert.ToInt32(cspp * ((float)(x + 1)));
						}
						else // at the end
						{
							nextClik = width;
						}
						// does the current effect start at or before this time slice?
						if (thisClik >= curEffect.startCentisecond)
						{
							// does it end at or after this time slice?
							if (thisClik <= curEffect.endCentisecond)
							{
								// We Got One!
								keepGoing = false;
								// This is the current effect at this time
								if (curEffect.EffectType == EffectType.intensity)
								{
									curLevel = curEffect.Intensity;
								}
								if (curEffect.EffectType == EffectType.shimmer)
								{
									curLevel = curEffect.Intensity | ADDshimmer;
								}
								if (curEffect.EffectType == EffectType.twinkle)
								{
									curLevel = curEffect.Intensity | ADDtwinkle;
								}
								if ((curEffect.EffectType == EffectType.intensity))
										
								{
									// Amount of difference in level, from start to end
									int levelDiff = curEffect.endIntensity - curEffect.startIntensity;
									// lenth of the effect
									int effLen = curEffect.endCentisecond - curEffect.startCentisecond;
									// how far we currently are from/past the start of the effect - in centiseconds
									int csFromStart = thisClik - curEffect.startCentisecond;
									// how far is that, as related to the length of the effect, expressed as 0 to 1
									float amtThru = (float)csFromStart / (float)effLen;
									// New relative level is the level difference times how relatively far we are thru it
									float newLev = 0F;
									// is it a fade-out/down?
									if (levelDiff < 0)
									{
										// Get inverse of amount thru
										//amtThru = 1F - amtThru;
										// make difference positive
										//levelDiff *= -1;
									}
									// New relative level is the level difference times how relatively far we are thru it
									newLev = (float)levelDiff * amtThru;
									// add the base starting level to get the current level at this slice in time
									curLevel = curEffect.startIntensity + Convert.ToInt32(newLev);
								}
							}
							else // we are past the end of this effect
							{
								// until we figure out otherwise - assume we are BETWEEN effects
								curLevel = 0;
								// are there more effects?
								if (effectIdx < (channel.effects.Count - 1))
								{										// move to next effect
									effectIdx++;
									curEffect = channel.effects[effectIdx];
									// does it end before this time slice
									while (curEffect.endCentisecond < cspp)
									{
										if (effectIdx < (channel.effects.Count - 1))
										{
											// move to next effect
											effectIdx++;
											curEffect = channel.effects[effectIdx];
										}
										else
										{
											curEffect = new Effect();
											curEffect.startCentisecond = endCentiseconds + 1;
											curEffect.endCentisecond = endCentiseconds + 2;
										} // if more effects remain
									} // end while(currentEffect End < this time slice)
								} // end there are more effects left
								else
								{
									keepGoing = false;
								}
							} // end curent effect ends at or before this time slice
						} 
						else
						{
							keepGoing = false;
						} // end current effect starts at or before this time slice
					} // end while(keepGoing) loop looking at effects
					levels[x] = curLevel;
				} // end for loop (columns/pixels, horizontal x left to right)
			} // end muliple centiseconds per pixel
			else
			{
				// mulitple pixels per centisecond
				//TODO
			} // end pixels to centiseconds ratio
			return levels;
		}



	} // end class utils
} // end namespace LORUtils
