using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Syncfusion.Windows.Forms.Tools;
using LORUtils4;
using FileHelper;


namespace LORUtils4
{

	public static class TreeUtils
	{

		public static int nodeIndex = lutils.UNDEFINED;
		// Newest imlTreeIcons as of 10/16/2021
		public static readonly int[] ICONuniverse			= new int[] { 0 };
		public static readonly int[] ICONcontroller		= new int[] { 1 };
		public static readonly int[] ICONtrack				= new int[] { 2 };
		public static readonly int[] ICONchannel			= new int[] { 3 };
		public static readonly int[] ICONrgbChannel		= new int[] { 4 };
		public static readonly int[] ICONchannelGroup = new int[] { 5 };
		public static readonly int[] ICONcosmicDevice = new int[] { 6 };
		public static readonly int[] ICONrgbcolor			= new int[] { 7 }; // Ugly light brown #400000
		public static readonly int[] ICONmulticolor		= new int[] { 8 }; // Ugly dark brown #804040
		public static readonly int[] ICONredChannel		= new int[] { 9 };
		public static readonly int[] ICONgrnChannel		= new int[] { 10 };
		public static readonly int[] ICONbluChannel		= new int[] { 11 };
		public static readonly int[] ICONwhtChannel		= new int[] { 12 };
		public static readonly int[] ICONblkChannel		= new int[] { 13 };
		public static readonly int[] ICONvioChannel		= new int[] { 14 };
		public static readonly int[] ICONornChannel		= new int[] { 15 };
		public static readonly int[] ICONyelChannel		= new int[] { 16 };
		public static readonly int[] ICONpnkChannel		= new int[] { 17 };
		// Additional common colors 18-31

		// Default colors but can be changed
		public static Color textHighlightColor = Color.Purple;
		public static Color textNormalColor = Color.Black;
		public static Color backgroundHighlightColor = Color.PaleGreen;
		public static Color backgroundNormalColor = Color.White;



		#region TreeStuff
		public static void TreeFillChannels(TreeViewAdv tree, LORSequence4 seq, bool selectedOnly, bool includeRGBchildren)
		{
			int listSize = seq.AllMembers.HighestSavedIndex + seq.Tracks.Count + seq.TimingGrids.Count + 1;
			int t = LORSeqEnums4.MEMBER_Channel | LORSeqEnums4.MEMBER_RGBchannel | LORSeqEnums4.MEMBER_ChannelGroup | LORSeqEnums4.MEMBER_Track;
			TreeFillChannels(tree, seq, selectedOnly, includeRGBchildren, t);
		}

		public static void TreeFillChannels(TreeViewAdv tree, LORSequence4 seq, bool selectedOnly, bool includeRGBchildren, int memberTypes)
		{
			//TODO: 'Selected' not implemented yet

			tree.Nodes.Clear();
			tree.Tag = seq;
			nodeIndex = 1;
			int listSize = seq.AllMembers.HighestSavedIndex + seq.Tracks.Count + seq.TimingGrids.Count + 1;


			//const string ERRproc = " in TreeFillChannels(";
			//const string ERRtrk = "), in LORTrack4 #";
			//const string ERRitem = ", Items #";
			//const string ERRline = ", Line #";


			for (int t = 0; t < seq.Tracks.Count; t++)
			{
				LORTrack4 trk = seq.Tracks[t];
				TreeNodeAdv trackNode = TreeAddTrack(seq, tree.Nodes, trk, selectedOnly, includeRGBchildren, memberTypes);
			}
			int xx = 42;
		}

		private static TreeNodeAdv TreeAddTrack(LORSequence4 seq, TreeNodeAdvCollection baseNodes, LORTrack4 track, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			List<TreeNodeAdv> nodeList;

			string nodeText = "";
			bool inclChan = false;
			if ((memberTypes & LORSeqEnums4.MEMBER_Channel) > 0) inclChan = true;
			bool inclRGB = false;
			if ((memberTypes & LORSeqEnums4.MEMBER_RGBchannel) > 0) inclRGB = true;

			// TEMPORARY, FOR DEBUGGING
			// int tcount = 0;
			int gcount = 0;
			int rcount = 0;
			int ccount = 0;
			int dcount = 0;

			nodeText = track.Name;
			TreeNodeAdv trackNode = new TreeNodeAdv(nodeText);
			trackNode.Tag = track;
			trackNode.LeftImageIndices = ICONtrack;
			trackNode.Checked = track.Selected;
			baseNodes.Add(trackNode);

			baseNodes = trackNode.Nodes;
			nodeIndex++;

			// Tracks should never be in the tree more than once
			// So create a new nodelist with just this node and attach it to the track
			nodeList = new List<TreeNodeAdv>();
			nodeList.Add(trackNode);
			track.Nodes = nodeList;

			//List<TreeNodeAdv> qlist;
			for (int ti = 0; ti < track.Members.Count; ti++)
			{
				iLORMember4 member = track.Members.Items[ti];
				int si = member.SavedIndex;
				if (member != null)
				{
					if (member.MemberType == LORMemberType4.ChannelGroup)
					{
						LORChannelGroup4 memGrp = (LORChannelGroup4)member;
						int inclCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
						if (inclCount > 0)
						{
							TreeNodeAdv groupNode = TreeAddGroup(seq, baseNodes, memGrp, selectedOnly, includeRGBchildren, memberTypes);
							//qlist = nodesBySI[si];
							//if (qlist == null) qlist = new List<TreeNodeAdv>();
							//qlist.Add(groupNode);
							gcount++;
						}
					}
					if (member.MemberType == LORMemberType4.Cosmic)
					{
						LORCosmic4 memDev = (LORCosmic4)member;
						int inclCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
						if (inclCount > 0)
						{
							TreeNodeAdv cosmicNode = TreeAddCosmic(seq, baseNodes, memDev, selectedOnly, includeRGBchildren, memberTypes);
							//qlist = nodesBySI[si];
							//if (qlist == null) qlist = new List<TreeNodeAdv>();
							//qlist.Add(cosmicNode);
							dcount++;
						}
					}
					if (member.MemberType == LORMemberType4.RGBChannel)
					{
						LORRGBChannel4 memRGB = (LORRGBChannel4)member;
						TreeNodeAdv rgbNode = TreeAddRGBchannel(seq, baseNodes, memRGB, selectedOnly, includeRGBchildren);
						//qlist = nodesBySI[si];
						//if (qlist == null) qlist = new List<TreeNodeAdv>();
						//qlist.Add(rgbNode);
						rcount++;
					}
					if (member.MemberType == LORMemberType4.Channel)
					{
						LORChannel4 memCh = (LORChannel4)member;
						TreeNodeAdv channelNode = TreeAddChannel(seq, baseNodes, memCh, selectedOnly);
						//qlist = nodesBySI[si];
						//if (qlist == null) qlist = new List<TreeNodeAdv>();
						//qlist.Add(channelNode);
						ccount++;
					}

				} // end not null
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
						Fyle.WriteLogEntry(emsg, lutils.LOG_Error, Application.ProductName);
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
						Fyle.WriteLogEntry(emsg, lutils.LOG_Error, Application.ProductName);
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
						Fyle.WriteLogEntry(emsg, lutils.LOG_Error, Application.ProductName);
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
					Fyle.WriteLogEntry(emsg, lutils.LOG_Error, Application.ProductName);
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
					Fyle.WriteLogEntry(emsg, lutils.LOG_Error, Application.ProductName);
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
					Fyle.WriteLogEntry(emsg, lutils.LOG_Error, Application.ProductName);
				}
				*/
			#endregion



			//	int x = 1; // Check ccount, rcount, gcount

			return trackNode;
		} // end fillOldChannels


		private static TreeNodeAdv TreeAddGroup(LORSequence4 seq, TreeNodeAdvCollection baseNodes, LORChannelGroup4 group, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			TreeNodeAdv groupNode = null;
			List<TreeNodeAdv> nodeList;
			int groupSI = group.SavedIndex;

			if (group.Nodes != null)
			{
				nodeList = (List<TreeNodeAdv>)group.Tag;
			}
			else
			{
				nodeList = new List<TreeNodeAdv>();
				group.Nodes = nodeList;
			}
			// Include groups in the TreeView?
			if ((memberTypes & LORSeqEnums4.MEMBER_ChannelGroup) > 0)
			{
				string nodeText = group.Name;
				groupNode = new TreeNodeAdv(nodeText);
				baseNodes.Add(groupNode);

				nodeIndex++;
				groupNode.Tag = group;
				groupNode.LeftImageIndices = ICONchannelGroup;
				//groupNode.SelectedImageKey = ICONchannelGroup;
				groupNode.Checked = group.Selected;
				baseNodes = groupNode.Nodes;
				nodeList.Add(groupNode);
			}

			// const string ERRproc = " in TreeFillChannels-TreeAddGroup(";
			// const string ERRgrp = "), in Group #";
			// const string ERRitem = ", Items #";
			// const string ERRline = ", Line #";

			for (int gi = 0; gi < group.Members.Count; gi++)
			{
				//try
				//{
				iLORMember4 member = group.Members.Items[gi];
				int si = member.SavedIndex;
				if (member.MemberType == LORMemberType4.ChannelGroup)
				{
					LORChannelGroup4 memGrp = (LORChannelGroup4)member;
					bool inclChan = false;
					if ((memberTypes & LORSeqEnums4.MEMBER_Channel) > 0) inclChan = true;
					bool inclRGB = false;
					if ((memberTypes & LORSeqEnums4.MEMBER_RGBchannel) > 0) inclRGB = true;
					int inclCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
					if (inclCount > 0)
					{
						TreeNodeAdv subGroupNode = TreeAddGroup(seq, baseNodes, memGrp, selectedOnly, includeRGBchildren, memberTypes);
						//qlist = nodesBySI[si];
						//qlist.Add(subGroupNode);
					}
					int cosCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
				}
				if (member.MemberType == LORMemberType4.Cosmic)
				{
					LORCosmic4 memDev = (LORCosmic4)member;
					bool inclChan = false;
					if ((memberTypes & LORSeqEnums4.MEMBER_Channel) > 0) inclChan = true;
					bool inclRGB = false;
					if ((memberTypes & LORSeqEnums4.MEMBER_RGBchannel) > 0) inclRGB = true;
					int inclCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
					if (inclCount > 0)
					{
						TreeNodeAdv subGroupNode = TreeAddCosmic(seq, baseNodes, memDev, selectedOnly, includeRGBchildren, memberTypes);
						//qlist = nodesBySI[si];
						//qlist.Add(subGroupNode);
					}
					int cosCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
				}
				if (member.MemberType == LORMemberType4.Channel)
				{
					if ((memberTypes & LORSeqEnums4.MEMBER_Channel) > 0)
					{
						LORChannel4 memCh = (LORChannel4)member;
						TreeNodeAdv channelNode = TreeAddChannel(seq, baseNodes, memCh, selectedOnly);
						//nodesBySI[si].Add(channelNode);
					}
				}
				if (member.MemberType == LORMemberType4.RGBChannel)
				{
					if ((memberTypes & LORSeqEnums4.MEMBER_RGBchannel) > 0)
					{
						LORRGBChannel4 memRGB = (LORRGBChannel4)member;
						TreeNodeAdv rgbChannelNode = TreeAddRGBchannel(seq, baseNodes, memRGB, selectedOnly, includeRGBchildren);
						//nodesBySI[si].Add(rgbChannelNode);
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
			Fyle.WriteLogEntry(emsg, lutils.LOG_Error, Application.ProductName);
		} // end catch
		*/
				#endregion

			} // End loop thru items
			return groupNode;
		} // end TreeAddGroup

		private static TreeNodeAdv TreeAddCosmic(LORSequence4 seq, TreeNodeAdvCollection baseNodes, LORCosmic4 device, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			int deviceSI = device.SavedIndex;
			List<TreeNodeAdv> nodeList;

			TreeNodeAdv deviceNode = null;
			if (device.Nodes != null)
			{
				nodeList = (List<TreeNodeAdv>)device.Nodes;
			}
			else
			{
				nodeList = new List<TreeNodeAdv>();
				device.Nodes = nodeList;
			}
				//ChanInfo nodeTag = new ChanInfo();
				//nodeTag.MemberType = LORMemberType4.ChannelGroup;
				//nodeTag.objIndex = groupIndex;
				//nodeTag.SavedIndex = theGroup.SavedIndex;
				//nodeTag.nodeIndex = nodeIndex;

				//LORChannelGroup4 theGroup = seq.ChannelGroups[groupIndex];
				//LORCosmic4 device = (LORCosmic4)seq.Members.BySavedIndex[deviceSI];

				//iLORMember4 groupID = theGroup;

				// Include groups in the TreeView?
			if ((memberTypes & LORSeqEnums4.MEMBER_CosmicDevice) > 0)
			{
				string nodeText = device.Name;
				deviceNode = new TreeNodeAdv(nodeText);
				baseNodes.Add(deviceNode);

				nodeIndex++;
				deviceNode.Tag = device;
				nodeList.Add(deviceNode);
				deviceNode.LeftImageIndices = ICONcosmicDevice;
				//deviceNode.SelectedImageKey = ICONcosmicDevice;
				deviceNode.Checked = device.Selected;
				baseNodes = deviceNode.Nodes;
			}
			//List<TreeNodeAdv> qlist;

			// const string ERRproc = " in TreeFillChannels-TreeAddGroup(";
			// const string ERRgrp = "), in Group #";
			// const string ERRitem = ", Items #";
			// const string ERRline = ", Line #";

			for (int gi = 0; gi < device.Members.Count; gi++)
			{
				//try
				//{
				iLORMember4 member = device.Members.Items[gi];
				int si = member.SavedIndex;
				if (member.MemberType == LORMemberType4.ChannelGroup)
				{
					LORChannelGroup4 memGrp = (LORChannelGroup4)member;
					bool inclChan = false;
					if ((memberTypes & LORSeqEnums4.MEMBER_Channel) > 0) inclChan = true;
					bool inclRGB = false;
					if ((memberTypes & LORSeqEnums4.MEMBER_RGBchannel) > 0) inclRGB = true;
					int inclCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
					if (inclCount > 0)
					{
						TreeNodeAdv subGroupNode = TreeAddGroup(seq, baseNodes, memGrp, selectedOnly, includeRGBchildren, memberTypes);
						//qlist = nodesBySI[si];
						//qlist.Add(subGroupNode);
					}
					int cosCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
				}
				if (member.MemberType == LORMemberType4.Cosmic)
				{
					LORCosmic4 memDev = (LORCosmic4)member;
					bool inclChan = false;
					if ((memberTypes & LORSeqEnums4.MEMBER_Channel) > 0) inclChan = true;
					bool inclRGB = false;
					if ((memberTypes & LORSeqEnums4.MEMBER_RGBchannel) > 0) inclRGB = true;
					int inclCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
					if (inclCount > 0)
					{
						TreeNodeAdv subGroupNode = TreeAddCosmic(seq, baseNodes, memDev, selectedOnly, includeRGBchildren, memberTypes);
						//qlist = nodesBySI[si];
						//qlist.Add(subGroupNode);
					}
					int cosCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
				}
				if (member.MemberType == LORMemberType4.Channel)
				{
					if ((memberTypes & LORSeqEnums4.MEMBER_Channel) > 0)
					{
						LORChannel4 chanMem = (LORChannel4)member;
						TreeNodeAdv channelNode = TreeAddChannel(seq, baseNodes, chanMem, selectedOnly);
					}
				}
				if (member.MemberType == LORMemberType4.RGBChannel)
				{
					if ((memberTypes & LORSeqEnums4.MEMBER_RGBchannel) > 0)
					{
						LORRGBChannel4 memRGB = (LORRGBChannel4)member;
						TreeNodeAdv rgbChannelNode = TreeAddRGBchannel(seq, baseNodes, memRGB, selectedOnly, includeRGBchildren);
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
			Fyle.WriteLogEntry(emsg, lutils.LOG_Error, Application.ProductName);
		} // end catch
		*/
				#endregion

			} // End loop thru items
			return deviceNode;
		} // end AddGroup

		private static void TreeAddNode(List<TreeNodeAdv> nodeList, TreeNodeAdv nOde)
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

		private static TreeNodeAdv TreeAddChannel(LORSequence4 seq, TreeNodeAdvCollection baseNodes, LORChannel4 channel, bool selectedOnly)
		{
			//LORChannel4 channel = (LORChannel4)seq.Members.BySavedIndex[channelSI];
			int channelSI = channel.SavedIndex;
			string nodeText = channel.Name;
			TreeNodeAdv channelNode = new TreeNodeAdv(nodeText);
			baseNodes.Add(channelNode);
			List<TreeNodeAdv> nodeList;
			//iLORMember4 nodeTag = channel;
			nodeIndex++;
			if (channel.Nodes == null)
			{
				nodeList = new List<TreeNodeAdv>();
				channel.Nodes = nodeList;
			}
			else
			{
				nodeList = (List<TreeNodeAdv>)channel.Nodes;
			}
			channelNode.Tag = channel;
			nodeList.Add(channelNode);
			//channelNode.ImageIndex = imlTreeIcons.Images.IndexOfKey("LORChannel4");
			//channelNode.SelectedImageIndex = imlTreeIcons.Images.IndexOfKey("LORChannel4");
			//channelNode.ImageKey = ICONchannel;
			//channelNode.SelectedImageKey = ICONchannel;


			ImageList icons = baseNodes[0].TreeView.LeftImageList;
			int iconIndex = ColorIcon(icons, channel.color);
			int[] colorIcon = new int[] { iconIndex };
			channelNode.LeftImageIndices = colorIcon;
			//channelNode.SelectedImageIndex = iconIndex;
			channelNode.Checked = channel.Selected;

			return channelNode;
		}

		private static TreeNodeAdv TreeAddRGBchannel(LORSequence4 seq, TreeNodeAdvCollection baseNodes, LORRGBChannel4 rgbChannel, bool selectedOnly, bool includeRGBchildren)
		{
			TreeNodeAdv channelNode = null;
			List<TreeNodeAdv> nodeList;
			int RGBsi = rgbChannel.SavedIndex;


			if (rgbChannel.Nodes != null)
			{
				nodeList = (List<TreeNodeAdv>)rgbChannel.Nodes;
			}
			else
			{
				nodeList = new List<TreeNodeAdv>();
				rgbChannel.Nodes = nodeList;
			}
			iLORMember4 mbrR = seq.AllMembers.BySavedIndex[RGBsi];
			if (mbrR.MemberType == LORMemberType4.RGBChannel)
			{
				//LORRGBChannel4 rgbChannel = (LORRGBChannel4)mbrR;
				string nodeText = rgbChannel.Name;
				channelNode = new TreeNodeAdv(nodeText);
				baseNodes.Add(channelNode);
				//iLORMember4 nodeTag = rgbChannel;
				nodeIndex++;
				channelNode.LeftImageIndices = ICONrgbChannel;
				//channelNode.SelectedImageKey = ICONrgbChannel;
				channelNode.Checked = rgbChannel.Selected;
				if (rgbChannel.Nodes == null)
				{
					nodeList = new List<TreeNodeAdv>();
					rgbChannel.Nodes = nodeList;
				}
				else
				{
					nodeList = (List<TreeNodeAdv>)rgbChannel.Nodes;
				}
				channelNode.Tag = rgbChannel;
				nodeList.Add(channelNode);

				if (includeRGBchildren)
				{
					// * * R E D   S U B  C H A N N E L * *
					TreeNodeAdv colorNode = null;
					nodeText = rgbChannel.redChannel.Name;
					colorNode = new TreeNodeAdv(nodeText);
					channelNode.Nodes.Add(colorNode);
					nodeIndex++;
					colorNode.LeftImageIndices = ICONredChannel;
					colorNode.Checked = rgbChannel.redChannel.Selected;
					channelNode.Nodes.Add(colorNode);
					if (rgbChannel.redChannel.Nodes == null)
					{
						nodeList = new List<TreeNodeAdv>();
						rgbChannel.redChannel.Nodes = nodeList;
					}
					else
					{
						nodeList = (List<TreeNodeAdv>)rgbChannel.redChannel.Nodes;
					}
					colorNode.Tag = rgbChannel.redChannel;
					nodeList.Add(channelNode);

					// * * G R E E N   S U B  C H A N N E L * *
					nodeText = rgbChannel.grnChannel.Name;
					colorNode = new TreeNodeAdv(nodeText);
					channelNode.Nodes.Add(colorNode);
					nodeIndex++;
					colorNode.LeftImageIndices = ICONgrnChannel;
					colorNode.Checked = rgbChannel.grnChannel.Selected;
					if (rgbChannel.grnChannel.Nodes == null)
					{
						nodeList = new List<TreeNodeAdv>();
						rgbChannel.grnChannel.Nodes = nodeList;
					}
					else
					{
						nodeList = (List<TreeNodeAdv>)rgbChannel.grnChannel.Nodes;
					}
					colorNode.Tag = rgbChannel.grnChannel;
					nodeList.Add(channelNode);

					// * * B L U E   S U B  C H A N N E L * *
					nodeText = rgbChannel.bluChannel.Name;
					colorNode = new TreeNodeAdv(nodeText);
					channelNode.Nodes.Add(colorNode);
					nodeIndex++;
					colorNode.LeftImageIndices = ICONbluChannel;
					colorNode.Checked = rgbChannel.bluChannel.Selected;
					if (rgbChannel.bluChannel.Nodes == null)
					{
						nodeList = new List<TreeNodeAdv>();
						rgbChannel.bluChannel.Nodes = nodeList;
					}
					else
					{
						nodeList = (List<TreeNodeAdv>)rgbChannel.bluChannel.Nodes;
					}
					colorNode.Tag = rgbChannel.bluChannel;
					nodeList.Add(channelNode);
				} // end includeRGBchildren
			}
			else
			{
				string msg = "Attempt to add non-RGB member to RGB node!";
				System.Diagnostics.Debugger.Break();
			}
			return channelNode;
		}


		public static int ColorIcon(ImageList icons, Int32 colorVal)
		{
			int ret = -1;
			string tempID = colorVal.ToString("X6");
			// LOR's Color Format is in BGR format, so have to reverse the Red and the Blue
			string colorID = tempID.Substring(4, 2) + tempID.Substring(2, 2) + tempID.Substring(0, 2);
			ret = icons.Images.IndexOfKey(colorID);
			if (ret < 0)
			{
				// Convert rearranged hex value a real color
				Color theColor = System.Drawing.ColorTranslator.FromHtml("#" + colorID);
				// Create a temporary working bitmap
				Bitmap bmp = new Bitmap(16, 16);
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

		#endregion // Tree Stuff

		public static int SelectMember(iLORMember4 member, bool select)
		{
			int changeCount = 0;
			// Did it even change?
			if (member.Selected != select)
			{
				// Create stub nodelist
				List<TreeNodeAdv> nodeList = null;
				// Does the member have a node list (it should, this is just a sanity check)
				if (member.Nodes != null)
				{
					// Get it.  Does it have any nodes (it should, just sanity check)
					nodeList = (List<TreeNodeAdv>)member.Nodes;
					if (nodeList.Count > 0)
					{
						// And loop thru it's nodeList to select all its nodes in the tree
						foreach (TreeNodeAdv nOde in nodeList)
						{
							string memName = member.Name;
							string nodName = nOde.Text;
							if (memName != nodName) Fyle.MakeNoise(Fyle.Noises.WrongButton);
							if (nOde.Checked != select) nOde.Checked = select;
						} // foreach nOde
					} // nodeList.Count
				} // member.Nodes != null
				// End selecting all the nodes of this member



				// What type of member is it?
				if (member.MemberType == LORMemberType4.Channel)
				{
					if (select) changeCount++; else changeCount--;
					// Channels need no special handling, we're done!
				}
				else
				{
					if (member.MemberType == LORMemberType4.RGBChannel)
					{
						// RGB Channels need their 3 color subchannels [un]selected
						LORRGBChannel4 rgbch = (LORRGBChannel4)member;
						// Recurse X3
						SelectMember(rgbch.redChannel, select);
						SelectMember(rgbch.grnChannel, select);
						SelectMember(rgbch.bluChannel, select);
						if (select) changeCount += 3; else changeCount -= 3;
					} // End if RGB Channel
					else
					{
						if (member.MemberType == LORMemberType4.ChannelGroup)
						{
							// Channel Groups need all thier members [un]selected
							LORChannelGroup4 group = (LORChannelGroup4)member;
							//? WTF?  foreach is skipping the first member!  But for loop works!
							//? Even worse, it skips ALL members on every run after the first
							//foreach (iLORMember4 submem in group.Members)
							for (int sm=0; sm< group.Members.Count; sm++)
							{
								iLORMember4 submem = group.Members[sm];
								// Recurse each submember
								changeCount += SelectMember(submem, select);
							}
						}
						else
						{
							if (member.MemberType == LORMemberType4.Cosmic)
							{
								// Cosmic Devices are effectively Channel Groups, and thus need all their submembers [un]selected
								LORCosmic4 cosmic = (LORCosmic4)member;
								foreach (iLORMember4 submem in cosmic.Members)
								{
									// Recurse each submember
									changeCount += SelectMember(submem, select);
								}
							}
							else
							{
								if (member.MemberType == LORMemberType4.Track)
								{
									// Likewise Tracks are effectively Channel Groups, and also need all their submembers [un]selected
									LORTrack4 track = (LORTrack4)member;
									foreach (iLORMember4 submem in track.Members)
									{
										// Recurse each submember
										changeCount += SelectMember(submem, select);
									} // End foreach loop submembers
								} // End Track
							} // End Cosmic, or not
						} // End Group, or not
					} // End RGB, or not
				} // End member is a channel, or not
				// Select the member itself
				member.Selected = select;
			} // End Did It Change

			return changeCount;
		}

		private static bool HasSelectedMembers(TreeNodeAdv nOde)
		{
			bool ret = false;
			if (nOde.Nodes.Count > 0)
			{
				foreach (TreeNode childNode in nOde.Nodes)
				{
					iLORMember4 m = (iLORMember4)childNode.Tag;
					ret = m.Selected;
					if (ret)
					{
						break;
					}
				}
			}
			return ret;
		} // end HasSelectedMembers

		#region Node Appearnce

		public static void ItalisizeMember(iLORMember4 member, bool italisize)
		{
			if (member.Nodes != null)
			{
				List<TreeNodeAdv> nodeList = (List<TreeNodeAdv>)member.Nodes;
				foreach(TreeNodeAdv nOde in nodeList)
				{
					ItalisizeNode(nOde, italisize);
				}
			}
		}

		public static void ItalisizeNode(TreeNodeAdv nOde, bool italisize)
		{
			if (italisize)
			{
				nOde.Font = new Font(nOde.Font, FontStyle.Italic);
			}
			else
			{
				nOde.Font = new Font(nOde.Font, FontStyle.Regular);
			}
		}

		public static void EmboldenMember(iLORMember4 member, bool embolden)
		{
			if (member.Nodes != null)
			{
				List<TreeNodeAdv> nodeList = (List<TreeNodeAdv>)member.Nodes;
				foreach (TreeNodeAdv nOde in nodeList)
				{
					EmboldenNode(nOde, embolden);
				}
			}
		}

		public static void EmboldenNode(TreeNodeAdv nOde, bool embolden)
		{
			if (embolden)
			{
				nOde.Font = new Font(nOde.Font, FontStyle.Bold);
			}
			else
			{
				nOde.Font = new Font(nOde.Font, FontStyle.Regular);
			}
		}

		public static void HiglightMemberBackground(iLORMember4 member, bool highlight)
		{
			if (member.Nodes != null)
			{
				List<TreeNodeAdv> nodeList = (List<TreeNodeAdv>)member.Nodes;
				foreach (TreeNodeAdv nOde in nodeList)
				{
					HighlightNodeBackground(nOde, highlight);
				}
			}
		}

		public static void HighlightNodeBackground(TreeNodeAdv nOde, bool highlight)
		{
			if (highlight)
			{
				HighlightNodeBackground(nOde, backgroundHighlightColor);
			}
			else
			{
				HighlightNodeBackground(nOde, backgroundNormalColor);
			}
		}

		public static void HighlightMemberBackground(iLORMember4 member, Color backColor)
		{
			if (member.Nodes != null)
			{
				List<TreeNodeAdv> nodeList = (List<TreeNodeAdv>)member.Nodes;
				foreach (TreeNodeAdv nOde in nodeList)
				{
					HighlightNodeBackground(nOde, backColor);
				}
			}
		}

		public static void HighlightNodeBackground(TreeNodeAdv nOde, Color backColor)
		{
			nOde.Background = new Syncfusion.Drawing.BrushInfo(backColor);
		}

		public static void ColorMemberText(iLORMember4 member, bool colorize)
		{
			if (member.Nodes != null)
			{
				List<TreeNodeAdv> nodeList = (List<TreeNodeAdv>)member.Nodes;
				foreach (TreeNodeAdv nOde in nodeList)
				{
					ColorNodeText(nOde, colorize);
				}
			}
		}

		public static void ColorNodeText(TreeNodeAdv nOde, bool colorize)
		{
			if (colorize)
			{
				nOde.TextColor = textHighlightColor;
			}
			else
			{
				nOde.TextColor = textNormalColor;
			}
		}

		public static void ColorMemberText(iLORMember4 member, Color textColor)
		{
			if (member.Nodes != null)
			{
				List<TreeNodeAdv> nodeList = (List<TreeNodeAdv>)member.Nodes;
				foreach (TreeNodeAdv nOde in nodeList)
				{
					ColorNodeText(nOde, textColor);
				}
			}
		}

		public static void ColorNodeText(TreeNodeAdv nOde, Color textColor)
		{
			nOde.TextColor = textColor;
		}



		#endregion





	}
}
		