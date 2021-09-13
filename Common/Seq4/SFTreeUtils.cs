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
using LORUtils4; using FileHelper;


namespace LORUtils4
{

	public class TreeUtils
	{

		public static int nodeIndex = lutils.UNDEFINED;
		public static readonly int[] ICONtrack = new int[] { 0 };
		public static readonly int[] ICONchannelGroup = new int[] { 1 };
		public static readonly int[] ICONrgbChannel = new int[] { 2 };
		public static readonly int[] ICONredChannel = new int[] { 4 };
		public static readonly int[] ICONgrnChannel = new int[] { 5 };
		public static readonly int[] ICONbluChannel = new int[] { 6 };
		public static readonly int[] ICONcosmicDevice = new int[] { 7 };

		#region TreeStuff
		public static void TreeFillChannels(TreeViewAdv tree, LORSequence4 seq, ref List<TreeNodeAdv>[] nodesBySI, bool selectedOnly, bool includeRGBchildren)
		{
			int listSize = seq.Members.HighestSavedIndex + seq.Tracks.Count + seq.TimingGrids.Count + 1;
			//Array.Resize(ref nodesBySI, listSize);
			if (nodesBySI == null)
			{
				Array.Resize(ref nodesBySI, listSize);
			}
			int t = LORSeqEnums4.MEMBER_Channel | LORSeqEnums4.MEMBER_RGBchannel | LORSeqEnums4.MEMBER_ChannelGroup | LORSeqEnums4.MEMBER_Track;
			TreeFillChannels(tree, seq, ref nodesBySI, selectedOnly, includeRGBchildren, t);
		}

		public static void TreeFillChannels(TreeViewAdv tree, LORSequence4 seq, ref List<TreeNodeAdv>[] nodesBySI, bool selectedOnly, bool includeRGBchildren, int memberTypes)
		{
			//TODO: 'Selected' not implemented yet

			tree.Nodes.Clear();
			nodeIndex = 1;
			int listSize = seq.Members.HighestSavedIndex + seq.Tracks.Count + seq.TimingGrids.Count + 1;
			//Array.Resize(ref nodesBySI, listSize);
			if (nodesBySI == null)
			{
				Array.Resize(ref nodesBySI, listSize);
			}
			if (listSize >= nodesBySI.Length)
			{
				Array.Resize(ref nodesBySI, listSize);
			}


			//const string ERRproc = " in TreeFillChannels(";
			//const string ERRtrk = "), in LORTrack4 #";
			//const string ERRitem = ", Items #";
			//const string ERRline = ", Line #";



			for (int n = 0; n < nodesBySI.Length; n++)
			{
				//nodesBySI[n] = null;
				//nodesBySI[n] = new List<TreeNodeAdv>();
				//nodesBySI.Add(new List<TreeNodeAdv>());
				nodesBySI[n] = new List<TreeNodeAdv>();
			}

			for (int t = 0; t < seq.Tracks.Count; t++)
			{
				LORTrack4 trk = seq.Tracks[t];
				TreeNodeAdv trackNode = TreeAddTrack(seq, tree.Nodes, trk, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
			}
			int xx = 42;
		}

		private static TreeNodeAdv TreeAddTrack(LORSequence4 seq, TreeNodeAdvCollection baseNodes, LORTrack4 track, ref List<TreeNodeAdv>[] nodesBySI, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			List<TreeNodeAdv> nodeList;

			if (nodesBySI == null)
			{
				Array.Resize(ref nodesBySI, seq.Members.HighestSavedIndex);
			}

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

			//try
			//{
			//LORTrack4 track = seq.Tracks[trackNumber];
			nodeText = track.Name;
			//TreeNodeAdv trackNode = baseNodes.Add(nodeText);
			TreeNodeAdv trackNode = new TreeNodeAdv(nodeText);
			baseNodes.Add(trackNode);
			List<TreeNodeAdv> qlist;

			//int inclCount = track.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
			//if (inclCount > 0)
			//{
			// Tracks don't normally have savedIndexes
			// But we will assign one for tracking and matching purposes
			//track.SavedIndex = seq.Members.HighestSavedIndex + t + 1;

			//if ((memberTypes & LORSeqEnums4.MEMBER_Track) > 0)
			//{
			baseNodes = trackNode.Nodes;
			nodeIndex++;
			trackNode.Tag = track;

			trackNode.LeftImageIndices = ICONtrack;


			trackNode.Checked = track.Selected;
			if (track.Tag == null)
			{
				//nodeList = new List<TreeNodeAdv>();
				//track.Tag = nodeList;
			}
			else
			{
				//nodeList = (List<TreeNodeAdv>)track.Tag;
			}
			//nodeList.Add(trackNode);
			//}

			for (int ti = 0; ti < track.Members.Count; ti++)
			{
				//try
				//{
				LORMember4 member = track.Members.Items[ti];
				int si = member.SavedIndex;
				if (member != null)
				{
					if (member.MemberType == LORMemberType4.ChannelGroup)
					{
						LORChannelGroup4 memGrp = (LORChannelGroup4)member;
						int inclCount = memGrp.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
						if (inclCount > 0)
						{
							TreeNodeAdv groupNode = TreeAddGroup(seq, baseNodes, memGrp, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
							//AddNode(nodesBySI[si], groupNode);
							qlist = nodesBySI[si];
							if (qlist == null) qlist = new List<TreeNodeAdv>();
							qlist.Add(groupNode);
							gcount++;
							//nodesBySI[si].Add(groupNode);
						}
					}
					if (member.MemberType == LORMemberType4.Cosmic)
					{
						LORCosmic4 memDev = (LORCosmic4)member;
						int inclCount = memDev.Members.DescendantCount(selectedOnly, inclChan, inclRGB, includeRGBchildren);
						if (inclCount > 0)
						{
							TreeNodeAdv cosmicNode = TreeAddCosmic(seq, baseNodes, memDev, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
							//AddNode(nodesBySI[si], groupNode);
							qlist = nodesBySI[si];
							if (qlist == null) qlist = new List<TreeNodeAdv>();
							qlist.Add(cosmicNode);
							dcount++;
							//nodesBySI[si].Add(groupNode);
						}
					}
					if (member.MemberType == LORMemberType4.RGBChannel)
					{
						LORRGBChannel4 memRGB = (LORRGBChannel4)member;
						TreeNodeAdv rgbNode = TreeAddRGBchannel(seq, baseNodes, memRGB, ref nodesBySI, selectedOnly, includeRGBchildren);
						//AddNode(nodesBySI[si], rgbNode);
						//nodesBySI[si].Add(rgbNode);
						qlist = nodesBySI[si];
						if (qlist == null) qlist = new List<TreeNodeAdv>();
						qlist.Add(rgbNode);
						rcount++;
					}
					if (member.MemberType == LORMemberType4.Channel)
					{
						LORChannel4 memCh = (LORChannel4)member;
						TreeNodeAdv channelNode = TreeAddChannel(seq, baseNodes, memCh, selectedOnly);
						//AddNode(nodesBySI[si], channelNode);
						//nodesBySI[si].Add(channelNode);
						qlist = nodesBySI[si];
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


		private static TreeNodeAdv TreeAddGroup(LORSequence4 seq, TreeNodeAdvCollection baseNodes, LORChannelGroup4 group, ref List<TreeNodeAdv>[] nodesBySI, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			TreeNodeAdv groupNode = null;
			List<TreeNodeAdv> nodeList;
			int groupSI = group.SavedIndex;

			if (groupSI >= nodesBySI.Length)
			{
				Array.Resize(ref nodesBySI, groupSI + 1);
			}

			if (nodesBySI[groupSI] != null)
			{
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
					nodesBySI[groupSI].Add(groupNode);
					if (group.Tag == null)
					{
						nodeList = new List<TreeNodeAdv>();
						group.Tag = nodeList;
					}
					else
					{
						nodeList = (List<TreeNodeAdv>)group.Tag;
					}
					nodeList.Add(groupNode);
				}
				//List<TreeNodeAdv> qlist;

				// const string ERRproc = " in TreeFillChannels-TreeAddGroup(";
				// const string ERRgrp = "), in Group #";
				// const string ERRitem = ", Items #";
				// const string ERRline = ", Line #";

				for (int gi = 0; gi < group.Members.Count; gi++)
				{
					//try
					//{
					LORMember4 member = group.Members.Items[gi];
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
							TreeNodeAdv subGroupNode = TreeAddGroup(seq, baseNodes, memGrp, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
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
							TreeNodeAdv subGroupNode = TreeAddCosmic(seq, baseNodes, memDev, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
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
							nodesBySI[si].Add(channelNode);
						}
					}
					if (member.MemberType == LORMemberType4.RGBChannel)
					{
						if ((memberTypes & LORSeqEnums4.MEMBER_RGBchannel) > 0)
						{
							LORRGBChannel4 memRGB = (LORRGBChannel4)member;
							TreeNodeAdv rgbChannelNode = TreeAddRGBchannel(seq, baseNodes, memRGB, ref nodesBySI, selectedOnly, includeRGBchildren);
							nodesBySI[si].Add(rgbChannelNode);
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
			}
			return groupNode;
		} // end TreeAddGroup

		private static TreeNodeAdv TreeAddCosmic(LORSequence4 seq, TreeNodeAdvCollection baseNodes, LORCosmic4 device, ref List<TreeNodeAdv>[] nodesBySI, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			int deviceSI = device.SavedIndex;
			if (deviceSI >= nodesBySI.Length)
			{
				Array.Resize(ref nodesBySI, deviceSI + 1);
			}

			TreeNodeAdv deviceNode = null;
			if (nodesBySI[deviceSI] != null)
			{
				//ChanInfo nodeTag = new ChanInfo();
				//nodeTag.MemberType = LORMemberType4.ChannelGroup;
				//nodeTag.objIndex = groupIndex;
				//nodeTag.SavedIndex = theGroup.SavedIndex;
				//nodeTag.nodeIndex = nodeIndex;

				//LORChannelGroup4 theGroup = seq.ChannelGroups[groupIndex];
				//LORCosmic4 device = (LORCosmic4)seq.Members.bySavedIndex[deviceSI];

				//LORMember4 groupID = theGroup;

				// Include groups in the TreeView?
				if ((memberTypes & LORSeqEnums4.MEMBER_CosmicDevice) > 0)
				{
					string nodeText = device.Name;
					deviceNode = new TreeNodeAdv(nodeText);
					baseNodes.Add(deviceNode);

					nodeIndex++;
					deviceNode.Tag = device;
					deviceNode.LeftImageIndices = ICONcosmicDevice;
					//deviceNode.SelectedImageKey = ICONcosmicDevice;
					deviceNode.Checked = device.Selected;
					baseNodes = deviceNode.Nodes;
					nodesBySI[deviceSI].Add(deviceNode);
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
					LORMember4 member = device.Members.Items[gi];
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
							TreeNodeAdv subGroupNode = TreeAddGroup(seq, baseNodes, memGrp, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
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
							TreeNodeAdv subGroupNode = TreeAddCosmic(seq, baseNodes, memDev, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
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
							nodesBySI[si].Add(channelNode);
						}
					}
					if (member.MemberType == LORMemberType4.RGBChannel)
					{
						if ((memberTypes & LORSeqEnums4.MEMBER_RGBchannel) > 0)
						{
							LORRGBChannel4 memRGB = (LORRGBChannel4)member;
							TreeNodeAdv rgbChannelNode = TreeAddRGBchannel(seq, baseNodes, memRGB, ref nodesBySI, selectedOnly, includeRGBchildren);
							nodesBySI[si].Add(rgbChannelNode);
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
			}
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
			//LORChannel4 channel = (LORChannel4)seq.Members.bySavedIndex[channelSI];
			int channelSI = channel.SavedIndex;
			string nodeText = channel.Name;
			TreeNodeAdv channelNode = new TreeNodeAdv(nodeText);
			baseNodes.Add(channelNode);
			List<TreeNodeAdv> nodeList;
			//LORMember4 nodeTag = channel;
			nodeIndex++;
			channelNode.Tag = channel;
			if (channel.Tag == null)
			{
				nodeList = new List<TreeNodeAdv>();
				channel.Tag = nodeList;
			}
			else
			{
				nodeList = (List<TreeNodeAdv>)channel.Tag;
			}
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

		private static TreeNodeAdv TreeAddRGBchannel(LORSequence4 seq, TreeNodeAdvCollection baseNodes, LORRGBChannel4 rgbChannel, ref List<TreeNodeAdv>[] nodesBySI, bool selectedOnly, bool includeRGBchildren)
		{
			TreeNodeAdv channelNode = null;
			List<TreeNodeAdv> nodeList;
			int RGBsi = rgbChannel.SavedIndex;

			if (RGBsi >= nodesBySI.Length)
			{
				Array.Resize(ref nodesBySI, RGBsi + 1);
			}
			if (nodesBySI[RGBsi] != null)
			{
				LORMember4 mbrR = seq.Members.bySavedIndex[RGBsi];
				if (mbrR.MemberType == LORMemberType4.RGBChannel)
				{
					//LORRGBChannel4 rgbChannel = (LORRGBChannel4)mbrR;
					string nodeText = rgbChannel.Name;
					channelNode = new TreeNodeAdv(nodeText);
					baseNodes.Add(channelNode);
					//LORMember4 nodeTag = rgbChannel;
					nodeIndex++;
					channelNode.Tag = rgbChannel;
					channelNode.LeftImageIndices = ICONrgbChannel;
					//channelNode.SelectedImageKey = ICONrgbChannel;
					channelNode.Checked = rgbChannel.Selected;
					if (rgbChannel.Tag == null)
					{
						nodeList = new List<TreeNodeAdv>();
						rgbChannel.Tag = nodeList;
					}
					else
					{
						nodeList = (List<TreeNodeAdv>)rgbChannel.Tag;
					}
					nodeList.Add(channelNode);

					if (includeRGBchildren)
					{
						// * * R E D   S U B  C H A N N E L * *
						TreeNodeAdv colorNode = null;
						int ci = rgbChannel.redChannel.SavedIndex;
						nodeText = rgbChannel.redChannel.Name;
						colorNode = new TreeNodeAdv(nodeText);
						channelNode.Nodes.Add(colorNode);
						//nodeTag = seq.Channels[ci];
						nodeIndex++;
						colorNode.Tag = rgbChannel.redChannel;
						colorNode.LeftImageIndices = ICONredChannel;
						//colorNode.SelectedImageKey = ICONredChannel;
						colorNode.Checked = rgbChannel.redChannel.Selected;
						nodesBySI[ci].Add(colorNode);
						channelNode.Nodes.Add(colorNode);
						if (rgbChannel.redChannel.Tag == null)
						{
							nodeList = new List<TreeNodeAdv>();
							rgbChannel.redChannel.Tag = nodeList;
						}
						else
						{
							nodeList = (List<TreeNodeAdv>)rgbChannel.redChannel.Tag;
						}
						nodeList.Add(channelNode);

						// * * G R E E N   S U B  C H A N N E L * *
						ci = rgbChannel.grnChannel.SavedIndex;
						nodeText = rgbChannel.grnChannel.Name;
						colorNode = new TreeNodeAdv(nodeText);
						channelNode.Nodes.Add(colorNode);
						//nodeTag = seq.Channels[ci];
						nodeIndex++;
						colorNode.Tag = rgbChannel.grnChannel;
						colorNode.LeftImageIndices = ICONgrnChannel;
						//colorNode.SelectedImageKey = ICONgrnChannel;
						colorNode.Checked = rgbChannel.grnChannel.Selected;
						nodesBySI[ci].Add(colorNode);
						channelNode.Nodes.Add(colorNode);
						if (rgbChannel.grnChannel.Tag == null)
						{
							nodeList = new List<TreeNodeAdv>();
							rgbChannel.grnChannel.Tag = nodeList;
						}
						else
						{
							nodeList = (List<TreeNodeAdv>)rgbChannel.grnChannel.Tag;
						}
						nodeList.Add(channelNode);

						// * * B L U E   S U B  C H A N N E L * *
						ci = rgbChannel.bluChannel.SavedIndex;
						if (nodesBySI[ci] != null)
							nodeText = rgbChannel.bluChannel.Name;
						colorNode = new TreeNodeAdv(nodeText);
						channelNode.Nodes.Add(colorNode);
						//nodeTag = seq.Channels[ci];
						nodeIndex++;
						colorNode.Tag = seq.Channels[ci];
						colorNode.LeftImageIndices = ICONbluChannel;
						//colorNode.SelectedImageKey = ICONbluChannel;
						colorNode.Checked = rgbChannel.bluChannel.Selected;
						nodesBySI[ci].Add(colorNode);
						channelNode.Nodes.Add(colorNode);
						if (rgbChannel.bluChannel.Tag == null)
						{
							nodeList = new List<TreeNodeAdv>();
							rgbChannel.bluChannel.Tag = nodeList;
						}
						else
						{
							nodeList = (List<TreeNodeAdv>)rgbChannel.bluChannel.Tag;
						}
						nodeList.Add(channelNode);
					} // end includeRGBchildren
				}
				else
				{
					string msg = "Attempt to add non-RGB member to RGB node!";
					System.Diagnostics.Debugger.Break();
				}
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
	}
}
		