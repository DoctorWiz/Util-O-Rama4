using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Syncfusion.Windows.Forms.Tools;

//using FuzzyString;

namespace LORUtils
{
	public class ChannelTree
	{
		public const string ICONtrack = "Track";
		public const string ICONchannelGroup = "ChannelGroup";
		public const string ICONcosmicDevice = "CosmicDevice";
		//public const string ICONchannel = "channel";
		public const string ICONrgbChannel = "RGBchannel";
		//public const string ICONredChannel = "redChannel";
		//public const string ICONgrnChannel = "grnChannel";
		//public const string ICONbluChannel = "bluChannel";
		public const string ICONredChannel = "FF0000";
		public const string ICONgrnChannel = "00FF00";
		public const string ICONbluChannel = "0000FF";
		// Note: LOR colors not in the same order as .Net or Web colors, Red and Blue are reversed

		private const int ICODXtrack = 0;
		private const int ICODXchannelGroup = 1;
		private const int ICODXrgbChannel = 2;
		private const int ICODXredChannel = 3;
		private const int ICODXgrnChannel = 4;
		private const int ICODXbluChannel = 5;
		private static int[] icodxTrack = new int[] { ICODXtrack };
		private static int[] icodxChannelGroup = new int[] { ICODXchannelGroup };
		private static int[] icodxRGBchannel = new int[] { ICODXrgbChannel };
		private static int[] icodxRedChannel = new int[] { ICODXredChannel };
		private static int[] icodxGrnChannel = new int[] { ICODXgrnChannel };
		private static int[] icodxBluChannel = new int[] { ICODXbluChannel };

		public const string LOG_Error = "Error";
		public const string LOG_Info = "Info";
		public const string LOG_Debug = "Debug";

		public static int nodeIndex = utils.UNDEFINED;


		public static void FillChannels(TreeViewAdv tree, Sequence4 seq, List<Syncfusion.Windows.Forms.Tools.TreeNodeAdv>[] siNodes, bool selectedOnly, bool includeRGBchildren)
		{
			int t = SeqEnums.MEMBER_Channel | SeqEnums.MEMBER_RGBchannel | SeqEnums.MEMBER_ChannelGroup | SeqEnums.MEMBER_Track;
			FillChannels(tree, seq, siNodes, selectedOnly, includeRGBchildren, t);
		}

		public static void FillChannels(TreeViewAdv tree, Sequence4 seq, List<TreeNodeAdv>[] siNodes, bool selectedOnly, bool includeRGBchildren, int memberTypes)
		{
			//TODO: 'Selected' not implemented yet

			tree.Nodes.Clear();
			nodeIndex = 1;
			int listSize = seq.Members.HighestSavedIndex + seq.Tracks.Count + seq.TimingGrids.Count + 1;
			//Array.Resize(ref siNodes, listSize);

			// const string ERRproc = " in FillChannels(";
			// const string ERRtrk = "), in Track #";
			// const string ERRitem = ", Items #";
			// const string ERRline = ", Line #";



			for (int n = 0; n < siNodes.Length; n++)
			{
				//siNodes[n] = null;
				//siNodes[n] = new List<TreeNodeAdv>();
				//siNodes.Add(new List<TreeNodeAdv>());
				siNodes[n] = new List<TreeNodeAdv>();
			}

			for (int t = 0; t < seq.Tracks.Count; t++)
			{
				TreeNodeAdv trackNode = AddTrack(seq, tree.Nodes, t, siNodes, selectedOnly, includeRGBchildren, memberTypes);
			}
		}


		private static TreeNodeAdv AddTrack(Sequence4 seq, TreeNodeAdvCollection baseNodes, int trackNumber, List<TreeNodeAdv>[] siNodes, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{

			string nodeText = "";
			bool inclChan = false;
			if ((memberTypes & SeqEnums.MEMBER_Channel) > 0) inclChan = true;
			bool inclRGB = false;
			if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0) inclRGB = true;

			// TEMPORARY, FOR DEBUGGING
			// int tcount = 0;
			int gcount = 0;
			int rcount = 0;
			int ccount = 0;
			int dcount = 0;

			//try
			//{
			Track theTrack = seq.Tracks[trackNumber];
			nodeText = theTrack.Name;
			TreeNodeAdv trackNode = new TreeNodeAdv(nodeText);
			baseNodes.Add(trackNode);
			List<TreeNodeAdv> qlist;

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
			trackNode.LeftImageIndices = icodxTrack;
			trackNode.Checked = theTrack.Selected;
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
							TreeNodeAdv groupNode = AddGroup(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren, memberTypes);
							//AddNode(siNodes[si], groupNode);
							qlist = siNodes[si];
							if (qlist == null) qlist = new List<TreeNodeAdv>();
							qlist.Add(groupNode);
							gcount++;
							//siNodes[si].Add(groupNode);
						}
					}
					if (member.MemberType == MemberType.CosmicDevice)
					{
						CosmicDevice memDev = (CosmicDevice)member;
						int inclCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
						if (inclCount > 0)
						{
							TreeNodeAdv cosmicNode = AddGroup(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren, memberTypes);
							//AddNode(siNodes[si], groupNode);
							qlist = siNodes[si];
							if (qlist == null) qlist = new List<TreeNodeAdv>();
							qlist.Add(cosmicNode);
							dcount++;
								//siNodes[si].Add(groupNode);
						}
					}
					if (member.MemberType == MemberType.RGBchannel)
					{
						TreeNodeAdv rgbNode = AddRGBchannel(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren);
						//AddNode(siNodes[si], rgbNode);
						//siNodes[si].Add(rgbNode);
						qlist = siNodes[si];
						if (qlist == null) qlist = new List<TreeNodeAdv>();
						qlist.Add(rgbNode);
						rcount++;
					}
					if (member.MemberType == MemberType.Channel)
					{
						TreeNodeAdv channelNode = AddChannel(seq, baseNodes, member.SavedIndex, selectedOnly);
						//AddNode(siNodes[si], channelNode);
						//siNodes[si].Add(channelNode);
						qlist = siNodes[si];
						if (qlist == null) qlist = new List<TreeNodeAdv>();
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
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
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
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
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
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
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
					utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
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
					utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
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
					utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
				}
				*/
			#endregion

		

			// 	int x = 1; // Check ccount, rcount, gcount

			return trackNode;
		} // end fillOldChannels


		private static TreeNodeAdv AddGroup(Sequence4 seq, TreeNodeAdvCollection baseNodes, int groupSI, List<TreeNodeAdv>[] siNodes, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			TreeNodeAdv groupNode = null;
			if (siNodes[groupSI] != null)
			{
				//ChanInfo nodeTag = new ChanInfo();
				//nodeTag.MemberType = MemberType.ChannelGroup;
				//nodeTag.objIndex = groupIndex;
				//nodeTag.SavedIndex = theGroup.SavedIndex;
				//nodeTag.nodeIndex = nodeIndex;

				//ChannelGroup theGroup = seq.ChannelGroups[groupIndex];
				ChannelGroup theGroup = (ChannelGroup)seq.Members.bySavedIndex[groupSI];

				//IMember groupID = theGroup;

				// Include groups in the TreeView?
				if ((memberTypes & SeqEnums.MEMBER_ChannelGroup) > 0)
				{
					string nodeText = theGroup.Name;
					groupNode = new TreeNodeAdv(nodeText);
					baseNodes.Add(groupNode);

					nodeIndex++;
					groupNode.Tag = theGroup;
					groupNode.LeftImageIndices = icodxChannelGroup;
					groupNode.Checked = theGroup.Selected;
					baseNodes = groupNode.Nodes;
					siNodes[groupSI].Add(groupNode);
				}
				//List<TreeNodeAdv> qlist;

				// const string ERRproc = " in FillChannels-AddGroup(";
				// const string ERRgrp = "), in Group #";
				// const string ERRitem = ", Items #";
				// const string ERRline = ", Line #";

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
							TreeNodeAdv subGroupNode = AddGroup(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren, memberTypes);
							//qlist = siNodes[si];
							//qlist.Add(subGroupNode);
						}
						int cosCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
					}
					if (member.MemberType == MemberType.CosmicDevice)
					{
						CosmicDevice memDev = (CosmicDevice)member;
						bool inclChan = false;
						if ((memberTypes & SeqEnums.MEMBER_Channel) > 0) inclChan = true;
						bool inclRGB = false;
						if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0) inclRGB = true;
						int inclCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
						if (inclCount > 0)
						{
							TreeNodeAdv subGroupNode = AddGroup(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren, memberTypes);
							//qlist = siNodes[si];
							//qlist.Add(subGroupNode);
						}
						int cosCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
					}
					if (member.MemberType == MemberType.Channel)
					{
						if ((memberTypes & SeqEnums.MEMBER_Channel) > 0)
						{
								TreeNodeAdv channelNode = AddChannel(seq, baseNodes, member.SavedIndex, selectedOnly);
								siNodes[si].Add(channelNode);
						}
					}
					if (member.MemberType == MemberType.RGBchannel)
					{
						if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0)
						{
								TreeNodeAdv rgbChannelNode = AddRGBchannel(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren);
								siNodes[si].Add(rgbChannelNode);
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
				utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
			} // end catch
			*/
					#endregion

				} // End loop thru items
			}
			return groupNode;
		} // end AddGroup

		private static TreeNodeAdv AddDevice(Sequence4 seq, TreeNodeAdvCollection baseNodes, int deviceSI, List<TreeNodeAdv>[] siNodes, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			TreeNodeAdv deviceNode = null;
			if (siNodes[deviceSI] != null)
			{
				//ChanInfo nodeTag = new ChanInfo();
				//nodeTag.MemberType = MemberType.ChannelGroup;
				//nodeTag.objIndex = groupIndex;
				//nodeTag.SavedIndex = theGroup.SavedIndex;
				//nodeTag.nodeIndex = nodeIndex;

				//ChannelGroup theGroup = seq.ChannelGroups[groupIndex];
				CosmicDevice theDevice = (CosmicDevice)seq.Members.bySavedIndex[deviceSI];

				//IMember groupID = theGroup;

				// Include groups in the TreeView?
				if ((memberTypes & SeqEnums.MEMBER_CosmicDevice) > 0)
				{
					string nodeText = theDevice.Name;
					deviceNode = new TreeNodeAdv(nodeText);
					baseNodes.Add(deviceNode);

					nodeIndex++;
					deviceNode.Tag = theDevice;
					deviceNode.LeftImageIndices = icodxChannelGroup; //  .ImageKey = ICONcosmicDevice;
					deviceNode.Checked = theDevice.Selected;
					baseNodes = deviceNode.Nodes;
					siNodes[deviceSI].Add(deviceNode);
				}
				//List<TreeNodeAdv> qlist;

				// const string ERRproc = " in FillChannels-AddGroup(";
				// const string ERRgrp = "), in Group #";
				// const string ERRitem = ", Items #";
				// const string ERRline = ", Line #";

				for (int gi = 0; gi < theDevice.Members.Count; gi++)
				{
					//try
					//{
					IMember member = theDevice.Members.Items[gi];
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
							TreeNodeAdv subGroupNode = AddGroup(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren, memberTypes);
							//qlist = siNodes[si];
							//qlist.Add(subGroupNode);
						}
						int cosCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
					}
					if (member.MemberType == MemberType.CosmicDevice)
					{
						CosmicDevice memDev = (CosmicDevice)member;
						bool inclChan = false;
						if ((memberTypes & SeqEnums.MEMBER_Channel) > 0) inclChan = true;
						bool inclRGB = false;
						if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0) inclRGB = true;
						int inclCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
						if (inclCount > 0)
						{
							TreeNodeAdv subGroupNode = AddGroup(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren, memberTypes);
							//qlist = siNodes[si];
							//qlist.Add(subGroupNode);
						}
						int cosCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
					}
					if (member.MemberType == MemberType.Channel)
					{
						if ((memberTypes & SeqEnums.MEMBER_Channel) > 0)
						{
								TreeNodeAdv channelNode = AddChannel(seq, baseNodes, member.SavedIndex, selectedOnly);
								siNodes[si].Add(channelNode);
						}
					}
					if (member.MemberType == MemberType.RGBchannel)
					{
						if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0)
						{
								TreeNodeAdv rgbChannelNode = AddRGBchannel(seq, baseNodes, member.SavedIndex, siNodes, selectedOnly, includeRGBchildren);
								siNodes[si].Add(rgbChannelNode);
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
				utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
			} // end catch
			*/
					#endregion

				} // End loop thru items
			}
			return deviceNode;
		} // end AddGroup

		private static void AddNode(List<TreeNodeAdv> nodeList, TreeNodeAdv nOde)
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

		private static TreeNodeAdv AddChannel(Sequence4 seq, TreeNodeAdvCollection baseNodes, int channelSI, bool selectedOnly)
		{
			Channel theChannel = (Channel) seq.Members.bySavedIndex[channelSI];
			string nodeText = theChannel.Name;
			TreeNodeAdv channelNode = new TreeNodeAdv(nodeText);
			baseNodes.Add(channelNode);
			//IMember nodeTag = theChannel;
			nodeIndex++;
			channelNode.Tag = theChannel;
			//channelNode.ImageIndex = imlTreeIcons.Images.IndexOfKey("Channel");
			//channelNode.SelectedImageIndex = imlTreeIcons.Images.IndexOfKey("Channel");
			//channelNode.ImageKey = ICONchannel;
			//channelNode.SelectedImageKey = ICONchannel;


			ImageList icons = baseNodes[0].TreeView.LeftImageList;
			int iconIndex = ColorIcon(icons, theChannel.color);
			int[] icodxColor = new int[] { iconIndex };
			channelNode.LeftImageIndices = icodxColor;
			channelNode.Checked = theChannel.Selected;


			return channelNode;
		}

		private static TreeNodeAdv AddRGBchannel(Sequence4 seq, TreeNodeAdvCollection baseNodes, int RGBsi, List<TreeNodeAdv>[] siNodes, bool selectedOnly, bool includeRGBchildren)
		{
			TreeNodeAdv rgbNode = null;

			if (siNodes[RGBsi] != null)
			{ 
				RGBchannel theRGB = (RGBchannel)seq.Members.bySavedIndex[RGBsi];
				string nodeText = theRGB.Name;
				rgbNode = new TreeNodeAdv(nodeText);
				baseNodes.Add(rgbNode);
				//IMember nodeTag = theRGB;
				nodeIndex++;
				rgbNode.Tag = theRGB;
				rgbNode.LeftImageIndices = icodxRGBchannel; 
				rgbNode.Checked = theRGB.Selected;

				if (includeRGBchildren)
				{
					// * * R E D   S U B  C H A N N E L * *
					TreeNodeAdv colorNode = null;
					int ci = theRGB.redChannel.SavedIndex;
					nodeText = theRGB.redChannel.Name;
					colorNode = new TreeNodeAdv(nodeText);
					nodeIndex++;
					colorNode.Tag = theRGB.redChannel;
					colorNode.LeftImageIndices = icodxRedChannel;
					colorNode.Checked = theRGB.redChannel.Selected;
					siNodes[ci].Add(colorNode);
					rgbNode.Nodes.Add(colorNode);

					// * * G R E E N   S U B  C H A N N E L * *
					ci = theRGB.grnChannel.SavedIndex;
					nodeText = theRGB.grnChannel.Name;
					colorNode = new TreeNodeAdv(nodeText);
					nodeIndex++;
					colorNode.Tag = theRGB.grnChannel;
					colorNode.LeftImageIndices = icodxGrnChannel; 
					colorNode.Checked = theRGB.grnChannel.Selected;
					siNodes[ci].Add(colorNode);
					rgbNode.Nodes.Add(colorNode);
					
					// * * B L U E   S U B  C H A N N E L * *
					ci = theRGB.bluChannel.SavedIndex;
					nodeText = theRGB.bluChannel.Name;
					colorNode = new TreeNodeAdv(nodeText);
					nodeIndex++;
					colorNode.Tag = theRGB.bluChannel;
					colorNode.LeftImageIndices = icodxBluChannel;
					colorNode.Checked = theRGB.bluChannel.Selected;
					siNodes[ci].Add(colorNode);
					rgbNode.Nodes.Add(colorNode);
				} // end includeRGBchildren
			}
			return rgbNode;
		}

		public static int ColorIcon(ImageList icons, Int32 colorVal)
		{
			int ret = -1;
			string tempID = colorVal.ToString("X6");
			// LOR's Color Format is in BGR format, so have to reverse the Red and the Blue
			string colorID = tempID.Substring(4, 2) + tempID.Substring(2, 2) + tempID.Substring(0, 2);
			ret = icons.Images.IndexOfKey (colorID);
			if (ret < 0)
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
			// int curCs = 0;
			// int lastCs = 0;
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
								if (curEffect.EffectTypeEX == EffectType.Constant)
								{
									curLevel = curEffect.Intensity;
								}
								if (curEffect.EffectTypeEX == EffectType.Shimmer)
								{
									curLevel = curEffect.Intensity | utils.ADDshimmer;
								}
								if (curEffect.EffectTypeEX == EffectType.Twinkle)
								{
									curLevel = curEffect.Intensity | utils.ADDtwinkle;
								}
								if ((curEffect.EffectTypeEX == EffectType.FadeDown) ||
										(curEffect.EffectTypeEX == EffectType.FadeUp))
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

	} // end class ChannelTree
} // end namespace LORUtils
