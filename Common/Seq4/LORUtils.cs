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
using System.DirectoryServices;
using Microsoft.Win32;
using FileHelper;
//using FuzzyString;

namespace LORUtils4
{
	public static class lutils
	{
		#region Constants
		public const string COPYRIGHT = "Copyright © 2021+ by Doctor 🧙 Wizard and W⚡zlights Software";
		public const int UNDEFINED = -1;
		public const string ICONtrack = "Track";
		public const string ICONchannelGroup = "ChannelGroup";
		public const string ICONcosmicDevice = "Cosmic";
		//public const string ICONchannel = "channel";
		public const string ICONrgbChannel = "RGBChannel";
		//public const string ICONredChannel = "redChannel";
		//public const string ICONgrnChannel = "grnChannel";
		//public const string ICONbluChannel = "bluChannel";
		public const string ICONredChannel = "FF0000";
		public const string ICONgrnChannel = "00FF00";
		public const string ICONbluChannel = "0000FF";
		public const string ICONwhtChannel = "FFFFFF";
		// Note: LOR colors not in the same order as .Net or Web colors, Red and Blue are reversed
		public const Int32 LORCOLOR_RED = 255;      // 0x0000FF
		public const Int32 LORCOLOR_GRN = 65280;    // 0x00FF00
		public const Int32 LORCOLOR_BLU = 16711680; // 0xFF0000
		public const Int32 LORCOLOR_BLK = 0;
		public const Int32 LORCOLOR_WHT = 0xFFFFFF;

		public const int ADDshimmer = 0x200;
		public const int ADDtwinkle = 0x400;

		public const int MINCentiseconds = 950;
		public const int MAXCentiseconds = 360000;

		public const string LOG_Error = "Error";
		public const string LOG_Info = "SeqInfo";
		public const string LOG_Debug = "Debug";
		//private const string FORMAT_DATETIME = "MM/dd/yyyy hh:mm:ss tt";
		//private const string FORMAT_FILESIZE = "###,###,###,###,##0";

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
		public const string XMLINFO = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";

		public const string FILE_SEQ = "All Sequences *.las, *.lms|*.las;*.lms";
		public const string FILE_CFG = "All Sequences *.las, *.lms, *.lcc|*.las;*.lms;*.lcc";
		public const string FILE_LMS = "Musical Sequence *.lms|*.lms";
		public const string FILE_LAS = "Animated Sequence *.las|*.las";
		public const string FILE_LCC = "Channel Configuration *.lcc|*.lcc";
		public const string FILE_LEE = "Visualization *.lee|*.lee";
		public const string FILE_CHMAP = "Channel Map *.ChMap|*.ChMap";
		public const string FILE_ALL = "All Files *.*|*.*";
		public const string FILT_OPEN_ANY = FILE_SEQ + "|" + FILE_LMS + "|" + FILE_LAS;
		public const string FILT_OPEN_CFG = FILE_CFG + "|" + FILE_LMS + "|" + FILE_LAS + "|" + FILE_LCC;
		public const string FILT_SAVE_EITHER = FILE_LMS + "|" + FILE_LAS;
		public const string EXT_CHMAP = "ChMap";
		public const string EXT_LAS = "las";
		public const string EXT_LMS = "lms";
		public const string EXT_LEE = "lee";
		public const string EXT_LCC = "lcc";


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
		private const string ROOT = "C:\\";
		private const string LOR_REGKEY = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
		private const string LOR_DIR = "Light-O-Rama\\";
		//private static string noisePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Noises\\";
		//private static bool gotWiz = false;
		//private static bool isWiz = false;

		private static Dictionary<int, String> colorMap = new Dictionary<int, String>();
		#endregion // Constants

		/* Moved to FileHelper.Fyle
			public enum Noises
			{
				None, Activate, Boing, Bonnggg, Brain, Crap, Crash, Dammit, Doh, DrumRoll, Excellent, Gong, Kalimbra, Log, Medievel,
				Pop, ScaleUp, SamCurseC, SamCurseF, SystemWorks, TaDa, ThatsThat, Wheee, Wizard, WooHoo, WrongButton, Click
			}

		public static bool IsWizard
		{
			get
			{
				if (gotWiz) // Already figured it out?
				{
					return isWiz;
				}
				else
				{
					bool ret = false;
					string usr = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
					usr = usr.ToLower();
					int i = usr.IndexOf("wizard");
					if (i >= 0) ret = true;
					isWiz = ret;
					gotWiz = true; // Only figure this out ONCE
					return ret;
				}
			}
		}
		*/

		/* Moved to FileHelper.Fyle
		public static string WindowsUsername
		{
			get
			{
				string usr = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
				return usr;
			}
		}
		*/

		public static bool isWiz
		{
			get
			{ return Fyle.isWiz; }
		}


		#region TreeStuff
		public static void TreeFillChannels(TreeView tree, LORSequence4 seq, ref List<TreeNode>[] nodesBySI, bool selectedOnly, bool includeRGBchildren)
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

		public static void TreeFillChannels(TreeView tree, LORSequence4 seq, ref List<TreeNode>[] nodesBySI, bool selectedOnly, bool includeRGBchildren, int memberTypes)
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
				//nodesBySI[n] = new List<TreeNode>();
				//nodesBySI.Add(new List<TreeNode>());
				nodesBySI[n] = new List<TreeNode>();
			}

			for (int t = 0; t < seq.Tracks.Count; t++)
			{
				LORTrack4 trk = seq.Tracks[t];
				TreeNode trackNode = TreeAddTrack(seq, tree.Nodes, trk, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
			}
			int xx = 42;
		}

		private static TreeNode TreeAddTrack(LORSequence4 seq, TreeNodeCollection baseNodes, LORTrack4 track, ref List<TreeNode>[] nodesBySI, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			List<TreeNode> nodeList;
			
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
			TreeNode trackNode = baseNodes.Add(nodeText);
			List<TreeNode> qlist;

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

			trackNode.ImageKey = ICONtrack;
			trackNode.SelectedImageKey = ICONtrack;
			trackNode.Checked = track.Selected;
			if (track.Tag == null)
			{
				//nodeList = new List<TreeNode>();
				//track.Tag = nodeList;
			}
			else
			{
				//nodeList = (List<TreeNode>)track.Tag;
			}
			//nodeList.Add(trackNode);
			//}

			for (int ti = 0; ti < track.Members.Count; ti++)
			{
				//try
				//{
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
							TreeNode groupNode = TreeAddGroup(seq, baseNodes, memGrp, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
							//AddNode(nodesBySI[si], groupNode);
							qlist = nodesBySI[si];
							if (qlist == null) qlist = new List<TreeNode>();
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
							TreeNode cosmicNode = TreeAddCosmic(seq, baseNodes, memDev, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
							//AddNode(nodesBySI[si], groupNode);
							qlist = nodesBySI[si];
							if (qlist == null) qlist = new List<TreeNode>();
							qlist.Add(cosmicNode);
							dcount++;
							//nodesBySI[si].Add(groupNode);
						}
					}
					if (member.MemberType == LORMemberType4.RGBChannel)
					{
						LORRGBChannel4 memRGB = (LORRGBChannel4)member;
						TreeNode rgbNode = TreeAddRGBchannel(seq, baseNodes, memRGB, ref nodesBySI, selectedOnly, includeRGBchildren);
						//AddNode(nodesBySI[si], rgbNode);
						//nodesBySI[si].Add(rgbNode);
						qlist = nodesBySI[si];
						if (qlist == null) qlist = new List<TreeNode>();
						qlist.Add(rgbNode);
						rcount++;
					}
					if (member.MemberType == LORMemberType4.Channel)
					{
						LORChannel4 memCh = (LORChannel4)member;
						TreeNode channelNode = TreeAddChannel(seq, baseNodes, memCh, selectedOnly);
						//AddNode(nodesBySI[si], channelNode);
						//nodesBySI[si].Add(channelNode);
						qlist = nodesBySI[si];
						if (qlist == null) qlist = new List<TreeNode>();
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


		private static TreeNode TreeAddGroup(LORSequence4 seq, TreeNodeCollection baseNodes, LORChannelGroup4 group, ref List<TreeNode>[] nodesBySI, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			TreeNode groupNode = null;
			List<TreeNode> nodeList;
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
					groupNode = baseNodes.Add(nodeText);

					nodeIndex++;
					groupNode.Tag = group;
					groupNode.ImageKey = ICONchannelGroup;
					groupNode.SelectedImageKey = ICONchannelGroup;
					groupNode.Checked = group.Selected;
					baseNodes = groupNode.Nodes;
					nodesBySI[groupSI].Add(groupNode);
					if (group.Tag == null)
					{
						nodeList = new List<TreeNode>();
						group.Tag = nodeList;
					}
					else
					{
						nodeList = (List<TreeNode>)group.Tag;
					}
					nodeList.Add(groupNode);
				}
				//List<TreeNode> qlist;

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
							TreeNode subGroupNode = TreeAddGroup(seq, baseNodes, memGrp, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
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
							TreeNode subGroupNode = TreeAddCosmic(seq, baseNodes, memDev, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
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
							TreeNode channelNode = TreeAddChannel(seq, baseNodes, memCh, selectedOnly);
							nodesBySI[si].Add(channelNode);
						}
					}
					if (member.MemberType == LORMemberType4.RGBChannel)
					{
						if ((memberTypes & LORSeqEnums4.MEMBER_RGBchannel) > 0)
						{
							LORRGBChannel4 memRGB = (LORRGBChannel4)member;
							TreeNode rgbChannelNode = TreeAddRGBchannel(seq, baseNodes, memRGB, ref nodesBySI, selectedOnly, includeRGBchildren);
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

		private static TreeNode TreeAddCosmic(LORSequence4 seq, TreeNodeCollection baseNodes, LORCosmic4 device, ref List<TreeNode>[] nodesBySI, bool selectedOnly,
			bool includeRGBchildren, int memberTypes)
		{
			int deviceSI = device.SavedIndex;
			if (deviceSI >= nodesBySI.Length)
			{
				Array.Resize(ref nodesBySI, deviceSI + 1);
			}

			TreeNode deviceNode = null;
			if (nodesBySI[deviceSI] != null)
			{
				//ChanInfo nodeTag = new ChanInfo();
				//nodeTag.MemberType = LORMemberType4.ChannelGroup;
				//nodeTag.objIndex = groupIndex;
				//nodeTag.SavedIndex = theGroup.SavedIndex;
				//nodeTag.nodeIndex = nodeIndex;

				//LORChannelGroup4 theGroup = seq.ChannelGroups[groupIndex];
				//LORCosmic4 device = (LORCosmic4)seq.Members.bySavedIndex[deviceSI];

				//iLORMember4 groupID = theGroup;

				// Include groups in the TreeView?
				if ((memberTypes & LORSeqEnums4.MEMBER_CosmicDevice) > 0)
				{
					string nodeText = device.Name;
					deviceNode = baseNodes.Add(nodeText);

					nodeIndex++;
					deviceNode.Tag = device;
					deviceNode.ImageKey = ICONcosmicDevice;
					deviceNode.SelectedImageKey = ICONcosmicDevice;
					deviceNode.Checked = device.Selected;
					baseNodes = deviceNode.Nodes;
					nodesBySI[deviceSI].Add(deviceNode);
				}
				//List<TreeNode> qlist;

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
							TreeNode subGroupNode = TreeAddGroup(seq, baseNodes, memGrp, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
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
							TreeNode subGroupNode = TreeAddCosmic(seq, baseNodes, memDev, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
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
							TreeNode channelNode = TreeAddChannel(seq, baseNodes, chanMem, selectedOnly);
							nodesBySI[si].Add(channelNode);
						}
					}
					if (member.MemberType == LORMemberType4.RGBChannel)
					{
						if ((memberTypes & LORSeqEnums4.MEMBER_RGBchannel) > 0)
						{
							LORRGBChannel4 memRGB = (LORRGBChannel4)member;
							TreeNode rgbChannelNode = TreeAddRGBchannel(seq, baseNodes, memRGB, ref nodesBySI, selectedOnly, includeRGBchildren);
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

		private static void TreeAddNode(List<TreeNode> nodeList, TreeNode nOde)
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

		private static TreeNode TreeAddChannel(LORSequence4 seq, TreeNodeCollection baseNodes, LORChannel4 channel, bool selectedOnly)
		{
			//LORChannel4 channel = (LORChannel4)seq.Members.bySavedIndex[channelSI];
			int channelSI = channel.SavedIndex;
			string nodeText = channel.Name;
			TreeNode channelNode = baseNodes.Add(nodeText);
			List<TreeNode> nodeList;
			//iLORMember4 nodeTag = channel;
			nodeIndex++;
			channelNode.Tag = channel;
			if (channel.Tag == null)
			{
				nodeList = new List<TreeNode>();
				channel.Tag = nodeList;
			}
			else
			{
				nodeList = (List<TreeNode>)channel.Tag;
			}
			nodeList.Add(channelNode);
			//channelNode.ImageIndex = imlTreeIcons.Images.IndexOfKey("LORChannel4");
			//channelNode.SelectedImageIndex = imlTreeIcons.Images.IndexOfKey("LORChannel4");
			//channelNode.ImageKey = ICONchannel;
			//channelNode.SelectedImageKey = ICONchannel;


			ImageList icons = baseNodes[0].TreeView.ImageList;
			int iconIndex = ColorIcon(icons, channel.color);
			channelNode.ImageIndex = iconIndex;
			channelNode.SelectedImageIndex = iconIndex;
			channelNode.Checked = channel.Selected;


			return channelNode;
		}

		private static TreeNode TreeAddRGBchannel(LORSequence4 seq, TreeNodeCollection baseNodes, LORRGBChannel4 rgbChannel, ref List<TreeNode>[] nodesBySI, bool selectedOnly, bool includeRGBchildren)
		{
			TreeNode channelNode = null;
			List<TreeNode> nodeList;
			int RGBsi = rgbChannel.SavedIndex;

			if (RGBsi >= nodesBySI.Length)
			{
				Array.Resize(ref nodesBySI, RGBsi + 1);
			}
			if (nodesBySI[RGBsi] != null)
			{
				iLORMember4 mbrR = seq.Members.bySavedIndex[RGBsi];
				if (mbrR.MemberType == LORMemberType4.RGBChannel)
				{
					//LORRGBChannel4 rgbChannel = (LORRGBChannel4)mbrR;
					string nodeText = rgbChannel.Name;
					channelNode = baseNodes.Add(nodeText);
					//iLORMember4 nodeTag = rgbChannel;
					nodeIndex++;
					channelNode.Tag = rgbChannel;
					channelNode.ImageKey = ICONrgbChannel;
					channelNode.SelectedImageKey = ICONrgbChannel;
					channelNode.Checked = rgbChannel.Selected;
					if (rgbChannel.Tag == null)
					{
						nodeList = new List<TreeNode>();
						rgbChannel.Tag = nodeList;
					}
					else
					{
						nodeList = (List<TreeNode>)rgbChannel.Tag;
					}
					nodeList.Add(channelNode);

					if (includeRGBchildren)
					{
						// * * R E D   S U B  C H A N N E L * *
						TreeNode colorNode = null;
						int ci = rgbChannel.redChannel.SavedIndex;
						nodeText = rgbChannel.redChannel.Name;
						colorNode = channelNode.Nodes.Add(nodeText);
						//nodeTag = seq.Channels[ci];
						nodeIndex++;
						colorNode.Tag = rgbChannel.redChannel;
						colorNode.ImageKey = ICONredChannel;
						colorNode.SelectedImageKey = ICONredChannel;
						colorNode.Checked = rgbChannel.redChannel.Selected;
						nodesBySI[ci].Add(colorNode);
						channelNode.Nodes.Add(colorNode);
						if (rgbChannel.redChannel.Tag == null)
						{
							nodeList = new List<TreeNode>();
							rgbChannel.redChannel.Tag = nodeList;
						}
						else
						{
							nodeList = (List<TreeNode>)rgbChannel.redChannel.Tag;
						}
						nodeList.Add(channelNode);

						// * * G R E E N   S U B  C H A N N E L * *
						ci = rgbChannel.grnChannel.SavedIndex;
						nodeText = rgbChannel.grnChannel.Name;
						colorNode = channelNode.Nodes.Add(nodeText);
						//nodeTag = seq.Channels[ci];
						nodeIndex++;
						colorNode.Tag = rgbChannel.grnChannel;
						colorNode.ImageKey = ICONgrnChannel;
						colorNode.SelectedImageKey = ICONgrnChannel;
						colorNode.Checked = rgbChannel.grnChannel.Selected;
						nodesBySI[ci].Add(colorNode);
						channelNode.Nodes.Add(colorNode);
						if (rgbChannel.grnChannel.Tag == null)
						{
							nodeList = new List<TreeNode>();
							rgbChannel.grnChannel.Tag = nodeList;
						}
						else
						{
							nodeList = (List<TreeNode>)rgbChannel.grnChannel.Tag;
						}
						nodeList.Add(channelNode);

						// * * B L U E   S U B  C H A N N E L * *
						ci = rgbChannel.bluChannel.SavedIndex;
						if (nodesBySI[ci] != null)
							nodeText = rgbChannel.bluChannel.Name;
						colorNode = channelNode.Nodes.Add(nodeText);
						//nodeTag = seq.Channels[ci];
						nodeIndex++;
						colorNode.Tag = seq.Channels[ci];
						colorNode.ImageKey = ICONbluChannel;
						colorNode.SelectedImageKey = ICONbluChannel;
						colorNode.Checked = rgbChannel.bluChannel.Selected;
						nodesBySI[ci].Add(colorNode);
						channelNode.Nodes.Add(colorNode);
						if (rgbChannel.bluChannel.Tag == null)
						{
							nodeList = new List<TreeNode>();
							rgbChannel.bluChannel.Tag = nodeList;
						}
						else
						{
							nodeList = (List<TreeNode>)rgbChannel.bluChannel.Tag;
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

		public static Int32 ColorInt(Color theColor)
		{
			return theColor.ToArgb();
		}
		
		public static string ColorToHex(Color color)
			// Returns 7 characters in typical web color format starting with a #
		{
			return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
		}

		public static Color HexToColor(string hexCode)
		{
			Color c = Color.White;
			// Eliminate any characters before the last 6
			// This gets rid of "#" or "0x" or even "0x??" or "#??" if alpha is specified
			if (hexCode.Length > 5)
			{
				// Yes, this looks absurdly complicated.
				// I could combine most of this into one longer line of code but it wouldn't speed it
				// up because it basically breaks down to this anyway.
				// Might as well go for the obvious understandable way.
				hexCode = hexCode.Substring(hexCode.Length - 6, 6);
				string rs = hexCode.Substring(0, 2);
				string gs = hexCode.Substring(2, 2);
				string gb = hexCode.Substring(4, 2);
				int r = Convert.ToInt32(rs, 16);
				int g = Convert.ToInt32(gs, 16);
				int b = Convert.ToInt32(gb, 16);
				c = Color.FromArgb(r, g, b);
			}
			return c;
		}

		public static int ColorIcon(ImageList icons, Int32 LORcolorVal)
		{
			return ColorIcon(icons, Color_LORtoNet(LORcolorVal));
		}

		public static int ColorIcon(ImageList icons, Color color)
		{ 
			int ret = -1;
			// LOR's Color Format is in BGR format, so have to reverse the Red and the Blue
			string colorID = ColorToHex(color);
			ret = icons.Images.IndexOfKey(colorID);
			if (ret < 0)
			{
				// Create a temporary working bitmap
				Bitmap bmp = new Bitmap(16, 16);
				// get the graphics handle from it
				Graphics gr = Graphics.FromImage(bmp);
				// A colored solid brush to fill the middle
				SolidBrush b = new SolidBrush(color);
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

		public static string ColorName(Color color)
		{
			string name = "";
			
			foreach (Color c in Enum.GetValues(typeof(KnownColor)))
			{
				if (c == color)
				{
					name = c.Name;
				}
			}
			return name;
		}

		public static string NearestColorName(Color color)
		{
			string name = "";
			//TODO Fix 'Specified Cast is not valid' error!!
			/*
			if (color != null)
			{
				Int32 myARGB = color.ToArgb();
				int myR = (myARGB >> 0) & 255;
				int myG = (myARGB >> 8) & 255;
				int myB = (myARGB >> 16) & 255;

				Color match = Color.Black;
				Int32 diff = 9999999;
				foreach (Color c in Enum.GetValues(typeof(KnownColor)))
				{
					Int32 cARGB = c.ToArgb();
					int cR = (cARGB >> 0) & 255;
					int cG = (cARGB >> 8) & 255;
					int cB = (cARGB >> 16) & 255;
					int d = Math.Abs(myR - cR);
					d += Math.Abs(myG - cG);
					d += Math.Abs(myB - cB);
					if (d < diff)
					{
						match = c;
						diff = d;
						name = c.Name;
					}
				}
			}
			*/
			return name;
		}

		#endregion // Tree Stuff


		#region Get and Set XML Tagged Fields
		public static string HumanizeName(string XMLizedName)
		{
			// Takes a name from XML and converts symbols back to the real thing
			string ret = XMLizedName;
			ret = ret.Replace("&quot;", "\"");
			ret = ret.Replace("&lt;", "<");
			ret = ret.Replace("&gt;", ">");
			ret = ret.Replace("&#34;", "\"");
			ret = ret.Replace("&#38;", "&");
			ret = ret.Replace("&#39;", "'");
			ret = ret.Replace("&#60;", "<");
			ret = ret.Replace("&#62;", ">");
			ret = ret.Replace("&#9837;", "♭");
			ret = ret.Replace("&#9839;", "♯");
			return ret;
		}

		public static string XMLifyName(string HumanizedName)
		{
			// Takes a human friendly name, possibly with illegal symbols in it
			// And replaces the illegal symbols with codes to make it XML friendly
			string ret = HumanizedName;
			ret = ret.Replace("\"", "&#34;");
			ret = ret.Replace("<", "&#60;");
			ret = ret.Replace(">", "&#62;");
			ret = ret.Replace("&", "&#38;");
			ret = ret.Replace("'", "&#39;");
			ret = ret.Replace("♭", "&#9837;");
			ret = ret.Replace("♯", "&#9839;");
			return ret;
		}

		public static int ContainsKey(string lineIn, string keyWord)
		{
			string lowerLine = lineIn.ToLower();
			string lowerWord = keyWord.ToLower();
			int pos1 = UNDEFINED;
			// int pos2 = UNDEFINED;
			// string fooo = "";

			//pos1 = lowerLine.IndexOf(lowerWord + "=");
			pos1 = FastIndexOf(lowerLine, lowerWord);
			return pos1;
		}

		public static Int32 getKeyValue(string lineIn, string keyWord)
		{
			int p = ContainsKey(lineIn, keyWord + "=\"");
			if (p >= 0)
			{
				return getKeyValue(p, lineIn, keyWord);
			}
			else
			{
				return lutils.UNDEFINED;
			}
		}

		public static int getKeyValue(int pos1, string lineIn, string keyWord)
		{
			int valueOut = UNDEFINED;
			int pos2 = UNDEFINED;
			string fooo = "";

			fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
			pos2 = fooo.IndexOf("\"");
			fooo = fooo.Substring(0, pos2);
			//valueOut = Convert.ToInt32(fooo);
			bool itWorked = int.TryParse(fooo, out valueOut);
			if (!itWorked)
			{
				if (Fyle.isWiz)
				{
					System.Diagnostics.Debugger.Break();
				}
			}

			return valueOut;
		}

		public static string getKeyWord(string lineIn, string keyWord)
		{
			int p = ContainsKey(lineIn, keyWord + "=\"");
			if (p >= 0)
			{
				return getKeyWord(p, lineIn, keyWord);
			}
			else
			{
				return "";
			}

		}

		public static string getKeyWord(int pos1, string lineIn, string keyWord)
		{
			string valueOut = "";
			int pos2 = UNDEFINED;
			string fooo = "";

			fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
			pos2 = fooo.IndexOf("\"");
			fooo = fooo.Substring(0, pos2);
			valueOut = fooo;

			return valueOut;
		}

		public static bool getKeyState(string lineIn, string keyWord)
		{
			int p = ContainsKey(lineIn, keyWord + "=\"");
			if (p >= 0)
			{
				return getKeyState(p, lineIn, keyWord);
			}
			else
			{
				return false;
			}
		}

		public static bool getKeyState(int pos1, string lineIn, string keyWord)
		{
			bool stateOut = false;
			int pos2 = UNDEFINED;
			string fooo = "";

			fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
			pos2 = fooo.IndexOf("\"");
			fooo = fooo.Substring(0, pos2).ToLower();
			if (fooo == "true") stateOut = true;
			if (fooo == "yes") stateOut = true;
			if (fooo == "1") stateOut = true;
			return stateOut;
		}

		public static string SetKey(string fieldName, string value)
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(fieldName);
			ret.Append(FIELDEQ);
			ret.Append(value);
			ret.Append(ENDQT);

			return ret.ToString();
		}

		public static string SetKey(string fieldName, int value)
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(fieldName);
			ret.Append(FIELDEQ);
			ret.Append(value);
			ret.Append(ENDQT);

			return ret.ToString();
		}

		public static string StartTable(string tableName, int level)
		{
			StringBuilder ret = new StringBuilder();

			for (int l = 0; l < level; l++)
			{
				ret.Append(LEVEL1);
			}
			ret.Append(STFLD);
			ret.Append(tableName);
			return ret.ToString();
		}

		public static string EndTable(string tableName, int level)
		{
			StringBuilder ret = new StringBuilder();

			for (int l = 0; l < level; l++)
			{
				ret.Append(LEVEL1);
			}
			ret.Append(lutils.FINTBL);
			ret.Append(tableName);
			ret.Append(lutils.ENDTBL);
			return ret.ToString();
		}

		#endregion // Get and Set Tagged XML Fields

		#region DisplayOrder
		public static int DisplayOrderBuildLists(LORSequence4 seq, ref int[] savedIndexes, ref int[] levels, bool selectedOnly, bool includeRGBchildren)
		{
			//TODO: 'Selected' not implemented yet

			int count = 0;
			int c = 0;
			int level = 0;
			//int tot = seq.Tracks.Count + seq.Channels.Count + seq.ChannelGroups.Count;
			int tot = seq.Channels.Count + seq.ChannelGroups.Count;
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
			// int tcount = 0;
			// int gcount = 0;
			// int rcount = 0;
			// int ccount = 0;

			//const string ERRproc = " in TreeFillChannels(";
			// const string ERRtrk = "), in LORTrack4 #";
			// const string ERRitem = ", Items #";
			// const string ERRline = ", Line #";

			for (int t = 0; t < seq.Tracks.Count; t++)
			{
				level = 0;
				LORTrack4 theTrack = seq.Tracks[t];
				if (!selectedOnly || theTrack.Selected)
				{
					DisplayOrderBuildTrack(seq, seq.Tracks[t], level, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
					//Array.Resize(ref savedIndexes, count + 1);
					//Array.Resize(ref levels, count + 1);
					//savedIndexes[count] = theTrack.SavedIndex;
					//levels[count] = level;
					//count++;
					//c++;
				}
			}
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

		public static int DisplayOrderBuildTrack(LORSequence4 seq, LORTrack4 theTrack, int level, ref int count, ref int[] savedIndexes, ref int[] levels, bool selectedOnly, bool includeRGBchildren)
		{
			int c = 0;
			for (int ti = 0; ti < theTrack.Members.Count; ti++)
			{
				iLORMember4 member = theTrack.Members.Items[ti];
				if (member != null)
				{
					int si = member.SavedIndex;
					if (member.MemberType == LORMemberType4.ChannelGroup)
					{
						c += DisplayOrderBuildGroup(seq, (LORChannelGroup4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
					}
					if (member.MemberType == LORMemberType4.Cosmic)
					{
						c += DisplayOrderBuildCosmic(seq, (LORCosmic4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
					}
					if (member.MemberType == LORMemberType4.RGBChannel)
					{
						c += DisplayOrderBuildRGBchannel(seq, (LORRGBChannel4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
					}
					if (member.MemberType == LORMemberType4.Channel)
					{
						c += DisplayOrderBuildChannel(seq, (LORChannel4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly);
					}
				} // end not null
			}
			return c;
		}

		public static int DisplayOrderBuildGroup(LORSequence4 seq, LORChannelGroup4 theGroup, int level, ref int count, ref int[] savedIndexes, ref int[] levels, bool selectedOnly, bool includeRGBchildren)
		{
			int c = 0;
			string nodeText = theGroup.Name;

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
				iLORMember4 member = theGroup.Members.Items[gi];
				int si = member.SavedIndex;
				if (member.MemberType == LORMemberType4.ChannelGroup)
				{
					c += DisplayOrderBuildGroup(seq, (LORChannelGroup4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
				}
				if (member.MemberType == LORMemberType4.Cosmic)
				{
					c += DisplayOrderBuildCosmic(seq, (LORCosmic4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
				}
				if (member.MemberType == LORMemberType4.Channel)
				{
					c += DisplayOrderBuildChannel(seq, (LORChannel4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly);
				}
				if (member.MemberType == LORMemberType4.RGBChannel)
				{
					c += DisplayOrderBuildRGBchannel(seq, (LORRGBChannel4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
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
			return c;
		} // end AddGroup

		public static int DisplayOrderBuildCosmic(LORSequence4 seq, LORCosmic4 theDevice, int level, ref int count, ref int[] savedIndexes, ref int[] levels, bool selectedOnly, bool includeRGBchildren)
		{
			int c = 0;
			string nodeText = theDevice.Name;

			// const string ERRproc = " in TreeFillChannels-AddGroup(";
			// const string ERRgrp = "), in Group #";
			// const string ERRitem = ", Items #";
			// const string ERRline = ", Line #";

			if (!selectedOnly || theDevice.Selected)
			{
				Array.Resize(ref savedIndexes, count + 1);
				Array.Resize(ref levels, count + 1);
				savedIndexes[count] = theDevice.SavedIndex;
				levels[count] = level;
				count++;
				c++;
			}

			for (int gi = 0; gi < theDevice.Members.Count; gi++)
			{
				//try
				//{
				iLORMember4 member = theDevice.Members.Items[gi];
				int si = member.SavedIndex;
				if (member.MemberType == LORMemberType4.ChannelGroup)
				{
					c += DisplayOrderBuildGroup(seq, (LORChannelGroup4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
				}
				if (member.MemberType == LORMemberType4.Cosmic)
				{
					c += DisplayOrderBuildCosmic(seq, (LORCosmic4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
				}
				if (member.MemberType == LORMemberType4.Channel)
				{
					c += DisplayOrderBuildChannel(seq, (LORChannel4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly);
				}
				if (member.MemberType == LORMemberType4.RGBChannel)
				{
					c += DisplayOrderBuildRGBchannel(seq, (LORRGBChannel4)member, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
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
			return c;
		} // end AddGroup

		public static int DisplayOrderBuildChannel(LORSequence4 seq, LORChannel4 theChannel, int level, ref int count, ref int[] savedIndexes, ref int[] levels, bool selectedOnly)
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

		public static int DisplayOrderBuildRGBchannel(LORSequence4 seq, LORRGBChannel4 theRGB, int level, ref int count, ref int[] savedIndexes, ref int[] levels, bool selectedOnly, bool includeRGBchildren)
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

		#endregion // Display Order

		#region ColorFunctions (Some are generic, some are LOR specific)
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

		public static Int32 Color_RGBtoLOR(int r, int g, int b)
		{
			int c = r;
			c += g * 0x100;
			c += b * 0x10000;
			return c;
		}
		
		public static Int32 Color_HTMLtoLOR(string HTMLcolor)
		{
			// LOR's Color Format is in BGR format, so have to reverse the Red and the Blue
			string colorID = HTMLcolor.Substring(4, 2) + HTMLcolor.Substring(2, 2) + HTMLcolor.Substring(0, 2);
			Int32 c = Int32.Parse(colorID, System.Globalization.NumberStyles.HexNumber);
			return c;
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
		#endregion // Color Functions

		#region RenderEffects

		public static Bitmap RenderEffects(iLORMember4 member, int startCentiseconds, int endCentiseconds, int width, int height, bool useRamps)
		{
			// Create a temporary working bitmap
			Bitmap bmp = new Bitmap(width, height);
			if (member.MemberType == LORMemberType4.Channel)
			{
				LORChannel4 channel = (LORChannel4)member;
				bmp = RenderEffects(channel, startCentiseconds, endCentiseconds, width, height, useRamps);
			}
			if (member.MemberType == LORMemberType4.RGBChannel)
			{
				LORRGBChannel4 rgb = (LORRGBChannel4)member;
				bmp = RenderEffects(rgb, startCentiseconds, endCentiseconds, width, height, useRamps);
			}

			return bmp;

		}

		public static Bitmap RenderEffects(LORChannel4 channel, int startCentiseconds, int endCentiseconds, int width, int height, bool useRamps)
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
			//Debug.WriteLine(""); Debug.WriteLine("");
			if (channel.effects.Count > 0)
			{
				int[] levels = PlotLevels(channel, startCentiseconds, endCentiseconds, width);
				for (int x = 0; x < width; x++)
				{
					Debug.Write(levels[x].ToString() + " ");
					bool shimmer = ((levels[x] & lutils.ADDshimmer) > 0);
					bool twinkle = ((levels[x] & lutils.ADDtwinkle) > 0);
					levels[x] &= 0x0FF;
					if (useRamps)
					{
						//int lineLen = levels[x] * Convert.ToInt32((float)height / 100D);
						int ll = levels[x] * height;
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

		public static Bitmap RenderTimings(LORTimings4 grid, int startCentiseconds, int endCentiseconds, int width, int height)
		{
			int h12 = height / 2;
			int h13 = height / 3;
			// Create a temporary working bitmap
			Bitmap bmp = new Bitmap(width, height);
			// get the graphics handle from it
			Graphics gr = Graphics.FromImage(bmp);
			// Paint the entire 'background' pale yellow
			Brush br = new SolidBrush(Color.FromArgb(240, 240, 128));
			gr.FillRectangle(br, 0, 0, width - 1, height - 1);
			Color c = Color.Black;
			Pen p = new Pen(c, 1);
			br = new SolidBrush(c);
			int totalCS = endCentiseconds - startCentiseconds;
			float q = totalCS / width;
			//Debug.WriteLine(""); Debug.WriteLine("");
			int div = 1500;
			if (totalCS < 3000) div = 100;
			if (totalCS > 30000) div = 3000;

			// Taller, thicker tick marks every 30 seconds
			int n30 = totalCS / div;

			for (int cx=startCentiseconds; cx < endCentiseconds; cx+=div)
			{
				float xf = (cx - startCentiseconds) / q;
				int x = (int)Math.Round(xf, 0);
				gr.DrawLine(p, x, h13, x, height-1);
				gr.DrawLine(p, x + 1, h13, x + 1, height-1);
			}

			if (grid.timings.Count > 0)
			{
				for (int i = 0; i < grid.timings.Count; i++)
				{
					int t = grid.timings[i];
					if ((t >= startCentiseconds) && (t <= endCentiseconds))
					{
						float xf = (t - startCentiseconds) / q;
						int x = (int)Math.Round(xf, 0);
						gr.DrawLine(p, x, h12, x, height-1);
					} // End timing in range
				} // End loop thru timings
			} // End grid has timings

			return bmp;
		}


		public static Bitmap RenderEffects(LORRGBChannel4 rgb, int startCentiseconds, int endCentiseconds, int width, int height, bool useRamps)
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
				//bool shimmer = ((levels[x] & lutils.ADDshimmer) > 0);
				//bool twinkle = ((levels[x] & lutils.ADDtwinkle) > 0);
				//levels[x] &= 0x0FF;
				if (useRamps)
				{
					// * * R E D * *
					Pen p = new Pen(Color.Red, 1);
					Brush br = new SolidBrush(Color.Red);
					bool shimmer = ((rLevels[x] & lutils.ADDshimmer) > 0);
					bool twinkle = ((rLevels[x] & lutils.ADDtwinkle) > 0);
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
					shimmer = ((rLevels[x] & lutils.ADDshimmer) > 0);
					twinkle = ((rLevels[x] & lutils.ADDtwinkle) > 0);
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
					shimmer = ((rLevels[x] & lutils.ADDshimmer) > 0);
					twinkle = ((rLevels[x] & lutils.ADDtwinkle) > 0);
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
					int R = rLevels[x];
					int G = gLevels[x];
					int B = bLevels[x];

					// Shimmer and Twinkle
					if (R >= ADDtwinkle)
					{
						R -= ADDtwinkle;
						R /= 2;
					}
					if (R >= ADDshimmer)
					{
						R -= ADDshimmer;
						R /= 2;
					}
					if (R > 100)
					{
						R = 100;
					}
					if (G >= ADDtwinkle)
					{
						G -= ADDtwinkle;
						G /= 2;
					}
					if (G >= ADDshimmer)
					{
						G -= ADDshimmer;
						G /= 2;
					}
					if (G > 100)
					{
						G = 100;
					}
					if (B >= ADDtwinkle)
					{
						B -= ADDtwinkle;
						B /= 2;
					}
					if (B >= ADDshimmer)
					{
						B -= ADDshimmer;
						B /= 2;
					}
					if (B > 100)
					{
						B= 100;
					}

					int r = (int)((float)R * 2.55F);
					int g = (int)((float)G * 2.55F);
					int b = (int)((float)B * 2.55F);



					Color c = Color.FromArgb(r, g, b);
					Pen p = new Pen(c, 1);
					gr.DrawLine(p, x, 0, x, height - 1);
				}
			} // end For X Coord (Horiz)
			return bmp;
		}

		public static int[] PlotLevels(LORChannel4 channel, int startCentiseconds, int endCentiseconds, int width)
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
			LOREffect4 curEffect = channel.effects[effectIdx];


			if (cspp >= 1.0F)
			{
				for (int x = 0; x < width; x++)
				{
					if (Fyle.isWiz)
					{
						if (x==42)
						{
							//System.Diagnostics.Debugger.Break();
						}
					}
					
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
								if (curEffect.EffectTypeEX == LOREffectType4.Constant)
								{
									curLevel = curEffect.Intensity;
								}
								if (curEffect.EffectTypeEX == LOREffectType4.Shimmer)
								{
									curLevel = curEffect.Intensity | ADDshimmer;
								}
								if (curEffect.EffectTypeEX == LOREffectType4.Twinkle)
								{
									curLevel = curEffect.Intensity | ADDtwinkle;
								}
								if ((curEffect.EffectTypeEX == LOREffectType4.FadeDown) ||
										(curEffect.EffectTypeEX == LOREffectType4.FadeUp))
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
								{                   // move to next effect
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
											curEffect = new LOREffect4();
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
					if (curLevel > 100)
					{
						string msg = "WTF? More than 100%";
						if (Fyle.isWiz)
						{
							//stem.Diagnostics.Debugger.Break();
						}
					}
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

		#endregion // Render Effects

		#region FastIndexOf
		public static int FastIndexOf(string source, string pattern)
		{
			if (pattern == null) throw new ArgumentNullException();
			if (pattern.Length == 0) return 0;
			if (pattern.Length == 1) return source.IndexOf(pattern[0]);
			bool found;
			int limit = source.Length - pattern.Length + 1;
			if (limit < 1) return -1;
			// Store the first 2 characters of "pattern"
			char c0 = pattern[0];
			char c1 = pattern[1];
			// Find the first occurrence of the first character
			int first = source.IndexOf(c0, 0, limit);
			while (first != -1)
			{
				// Check if the following character is the same like
				// the 2nd character of "pattern"
				if (source[first + 1] != c1)
				{
					first = source.IndexOf(c0, ++first, limit - first);
					continue;
				}
				// Check the rest of "pattern" (starting with the 3rd character)
				found = true;
				for (int j = 2; j < pattern.Length; j++)
					if (source[first + j] != pattern[j])
					{
						found = false;
						break;
					}
				// If the whole word was found, return its index, otherwise try again
				if (found) return first;
				first = source.IndexOf(c0, ++first, limit - first);
			}
			return -1;
		}
		#endregion // FastIndexOf

		#region LOR Specific File & Directory Functions
		
		public static string SequenceEditor
		{
			get
			{
				string exe = "";
				string root = ROOT;
				try
				{
					string ky = "HKEY_CLASSES_ROOT\\lms_auto_file\\shell\\open";
					exe = (string)Registry.GetValue(ky, "command", root);
					exe = exe.Replace(" \"%1\"", "");
					if (exe == null)
					{
						exe = "C:\\Program Files (x86)\\Light-O-Rama\\LORSequenceEditor.exe";
					}
					if (exe.Length < 10)
					{
						exe = "C:\\Program Files (x86)\\Light-O-Rama\\LORSequenceEditor.exe";
					}
					bool valid = System.IO.File.Exists(exe);
					if (!valid)
					{
						exe = "";
					}
				}
				catch
				{
					exe = "";
				}
				return exe;

			}
		}
		
		public static string DefaultUserDataPath
		{
			get
			{
				string fldr = "";
				string root = ROOT;
				string userDocs = Fyle.DefaultDocumentsPath;
				try
				{
					fldr = (string)Registry.GetValue(LOR_REGKEY, "UserDataPath", root);
					if (fldr.Length < 6)
					{
						fldr = userDocs + LOR_DIR;
					}
					bool valid = Directory.Exists(fldr); // Fyle.IsValidPath(fldr);
					if (!valid)
					{
						fldr = userDocs + LOR_DIR;
					}
					if (!Directory.Exists(fldr))
					{
						Directory.CreateDirectory(fldr);
					}
				}
				catch
				{
					fldr = userDocs;
				}
				return fldr;
			} // End get UserDataPath
		}

		public static string DefaultAuthor
		{
			get
			{
				string author = "[Unknown Author]";
				string root = ROOT;
				try
				{
					string ky = "HKEY_CURRENT_USER\\Software\\Light-O-Rama\\Editor\\NewSequence";
					author = (string)Registry.GetValue(ky, "Author", root);
					if (author == null)
					{
						// If key does not exist, string will be NULL instead of empty
						author = "";
					}
					if (author.Length < 2)
					{
						ky = LOR_REGKEY + "\\Licensing";
						author = (string)Registry.GetValue(ky, "LicenseName", root);
						if (author.Length < 2)
						{
							// Fallback Failsafe
							author = Fyle.WindowsUsername;
						}
					}
				}
				catch
				{
					author = Fyle.WindowsUsername;
				}
				return author;
			} // End get UserDataPath
		}



		public static string DefaultNonAudioPath
		{
			// AKA Sequences Folder
			get
			{
				string fldr = "";
				string root = ROOT;
				string userDocs = Fyle.DefaultDocumentsPath;
				try
				{
					fldr = (string)Registry.GetValue(LOR_REGKEY, "NonAudioPath", root);
					if (fldr.Length < 6)
					{
						fldr = DefaultUserDataPath + "Sequences\\";
					}
					bool valid = Fyle.IsValidPath(fldr);
					if (!valid)
					{
						fldr = DefaultUserDataPath + "Sequences\\";
					}
					if (!Directory.Exists(fldr))
					{
						Directory.CreateDirectory(fldr);
					}
				}
				catch
				{
					fldr = userDocs;
				}
				return fldr;
			} // End get NonAudioPath (Sequences)
		}

		public static string DefaultVisualizationsPath
		{
			get
			{
				string fldr = "";
				string root = ROOT;
				string userDocs = Fyle.DefaultDocumentsPath;
				try
				{
					fldr = (string)Registry.GetValue(LOR_REGKEY, "VisualizationsPath", root);
					if (fldr.Length < 6)
					{
						fldr = DefaultUserDataPath + "Visualizations\\";
					}
					bool valid = Fyle.IsValidPath(fldr);
					if (!valid)
					{
						fldr = DefaultUserDataPath + "Visualizations\\";
					}
					if (!Directory.Exists(fldr))
					{
						Directory.CreateDirectory(fldr);
					}
				}
				catch
				{
					fldr = userDocs;
				}
				return fldr;
			} // End get Visualizations Path
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
				string fldr = "";
				string root = ROOT;
				string userDocs = Fyle.DefaultDocumentsPath;
				string userMusic = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
				try
				{
					fldr = (string)Registry.GetValue(LOR_REGKEY, "AudioPath", root);
					if (fldr.Length < 6)
					{
						fldr = DefaultUserDataPath + "Audio\\";
					}
					bool valid = Fyle.IsValidPath(fldr);
					if (!valid)
					{
						fldr = DefaultUserDataPath + "Audio\\";
					}
					if (!Directory.Exists(fldr))
					{
						Directory.CreateDirectory(fldr);
					}
				}
				catch
				{
					fldr = userMusic;
				}
				return fldr;
			} // End get AudioPath
		}

		public static string DefaultClipboardsPath
		{
			get
			{
				string fldr = "";
				string root = ROOT;
				string userDocs = Fyle.DefaultDocumentsPath;
				try
				{
					fldr = (string)Registry.GetValue(LOR_REGKEY, "ClipboardsPath", root);
					if (fldr.Length < 6)
					{
						fldr = DefaultUserDataPath + "Clipboards\\";
					}
					bool valid = Fyle.IsValidPath(fldr);
					if (!valid)
					{
						fldr = DefaultUserDataPath + "Clipboards\\";
					}
					if (!Directory.Exists(fldr))
					{
						Directory.CreateDirectory(fldr);
					}
				}
				catch
				{
					fldr = userDocs;
				}
				return fldr;
			} // End get ClipboardsPath
		}

		public static string DefaultChannelConfigsPath
		{
			get
			{
				string fldr = "";
				// string root = ROOT;
				string userDocs = Fyle.DefaultDocumentsPath;
				try
				{
					fldr = (string)Registry.GetValue(LOR_REGKEY, "ChannelConfigsPath", "");
					if (fldr.Length < 6)
					{
						fldr = DefaultUserDataPath + "Sequences\\ChannelConfigs\\";
						Registry.SetValue(LOR_REGKEY, "ChannelConfigsPath", fldr, RegistryValueKind.String);
					}
					bool valid = Fyle.IsValidPath(fldr);
					if (!valid)
					{
						fldr = DefaultUserDataPath + "Sequences\\ChannelConfigs\\";
						Registry.SetValue(LOR_REGKEY, "ChannelConfigsPath", fldr, RegistryValueKind.String);
					}
					if (!Directory.Exists(fldr))
					{
						Directory.CreateDirectory(fldr);
					}
				}
				catch
				{
					fldr = userDocs;
				}
				return fldr;
			} // End get ChannelConfigsPath
		}
		#endregion LOR Specific File Functions
	
		/* Moved to FileHelper.Fyle
		#region Generic File Functions - TODO: Move these to a separate new 'FileHelper' utility class
		public static string DefaultDocumentsPath
		{
			get
			{
				string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				if (myDocs.Substring(myDocs.Length - 1, 1).CompareTo("\\") != 0) myDocs += "\\";
				return myDocs;
			}
		}

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

		public static void WriteLogEntry (string message)
		{
			WriteLogEntry(message, "Debug", AssemblyTitle);
		}
		
		public static void WriteLogEntry(string message, string logType)
		{
			WriteLogEntry(message, logType, AssemblyTitle);
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
				string errMsg = "An error has occurred in this application!\r\n";
				errMsg += "Another error has occurred while trying to write the details of the first error to a log file!\r\n\r\n";
				errMsg += "The first error was: " + message + "\r\n";
				errMsg += "The second error was: " + ex.ToString();
				errMsg += "The log file is: " + file;
				DialogResult dr = MessageBox.Show(errMsg, "Errors!", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
			}
			finally
			{
			}

		} // end write log entry

		public static bool IsValidPath(string pathToCheck)
		{
			// Checks to see if path looks valid, does NOT check if it exists
			bool ret = false;
			try
			{
				string x = Path.GetFullPath(pathToCheck);
				ret = true;
				ret = Path.IsPathRooted(pathToCheck);
			}
			catch
			{
				ret = false;
			}
			return ret;
		}

		public static bool IsValidPath(string pathToCheck, bool andExists)
		{
			// Checks to see if path looks valid AND if it exists
			bool ret = IsValidPath(pathToCheck);
			if (ret)
			{
				try
				{
					string p = Path.GetDirectoryName(pathToCheck);
					ret = Directory.Exists(p);
				}
				catch
				{
					ret = false;
				}
			}
			return ret;
		}

		public static bool IsValidPath(string pathToCheck, bool andExists, bool tryToCreate)
		{
			// Checks to see if path looks valid AND if it exists
			// Tries to create it seemingly valid but not existent
			// Returns true only if successfully created/exists
			// Note: andExists parameter ignored, not used
			bool ret = IsValidPath(pathToCheck);
			if (ret)
			{
				try
				{
					ret = Directory.Exists(pathToCheck);
					if (!ret)
					{
						Directory.CreateDirectory(pathToCheck);
						ret = Directory.Exists(pathToCheck);
					}
				}
				catch
				{
					ret = false;
				}
			}
			return ret;
		}

		public static string WindowfyFilename(string proposedName, bool justName)
		{
			string finalName = proposedName;
			finalName = finalName.Replace('?', 'Ɂ');
			finalName = finalName.Replace('%', '‰');
			finalName = finalName.Replace('*', '＊');
			finalName = finalName.Replace('|', '∣');
			finalName = finalName.Replace("\"", "ʺ");
			finalName = finalName.Replace("&quot;", "ʺ");
			finalName = finalName.Replace('<', '˂');
			finalName = finalName.Replace('>', '˃');
			finalName = finalName.Replace('$', '﹩');
			if (justName)
			{
				// These are valid in a path as separators, but not in a file name
				finalName = finalName.Replace('/', '∕');
				finalName = finalName.Replace("\\", "╲");
				finalName = finalName.Replace(':', '：');
			}
			else
			{
				// Not valid in a WINDOWS path
				finalName = finalName.Replace('/', '\\');
			}


			return finalName;
		}

		public static string FileCreatedAt(string filename)
		{
			string ret = "";
			if (File.Exists(filename))
			{
				DateTime dt = File.GetCreationTime(filename);
				ret = dt.ToString(FORMAT_DATETIME);
			}
			return ret;
		}

		public static DateTime FileCreatedDateTime(string filename)
		{
			DateTime ret = new DateTime();
			if (File.Exists(filename))
			{
				ret = File.GetCreationTime(filename);
			}
			return ret;
		}

		public static string FileModiedAt(string filename)
		{
			string ret = "";
			if (File.Exists(filename))
			{
				DateTime dt = File.GetLastWriteTime(filename);
				ret = dt.ToString(FORMAT_DATETIME);
			}
			return ret;
		}

		public static string FormatDateTime(DateTime dt)
		{
			string ret = dt.ToString(FORMAT_DATETIME);
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

		public static string FileSizeFormatted(string filename)
		{
			long sz = GetFileSize(filename);
			return FileSizeFormatted(sz, "");
		}

		public static string FileSizeFormatted(string filename, string thousands)
		{
			long sz = GetFileSize(filename);
			return FileSizeFormatted(sz, thousands);
		}

		public static string FileSizeFormatted(long filesize)
		{
			return FileSizeFormatted(filesize, "");
		}

		public static string FileSizeFormatted(long filesize, string thousands)
		{
			string thou = thousands.ToUpper();
			string ret = "0";
			if (thou == "B") // Force value in Bytes
			{
				ret = Bytes(filesize);
			}
			else
			{
				if (thou == "K") // Force value in KiloBytes
				{
					ret = KiloBytes(filesize);
				}
				else
				{
					if (thou == "M") // Force value in MegaBytes
					{
						ret = MegaBytes(filesize);
					}
					else
					{
						if (thou == "G") // Force value in GigaBytes
						{
							ret = GigaBytes(filesize);
						}
						else
						{
							thou = ""; // Return value in nearest size group
							if (filesize < 100000)
							{
								ret = Bytes(filesize);
							}
							else
							{
								if (filesize < 100000000)
								{
									ret = KiloBytes(filesize);
								}
								else
								{
									if (filesize < 100000000000)
									{
										ret = MegaBytes(filesize);
									}
									else
									{
										//if (filesize < 1099511627776)
										//{
										ret = GigaBytes(filesize);
										//}
									}
								}
							}
						}
					}
				}
			}
			return ret;
		}

		private static string Bytes(long size)
		{
			return size.ToString(FORMAT_FILESIZE) + " Bytes";
		}

		private static string KiloBytes(long size)
		{
			long k = size >> 10;
			string ret = k.ToString(FORMAT_FILESIZE);
			if (k < 100)
			{
				double r = (int)(size % 0x400);
				int d = 0;
				double f = 0;
				if (k < 10) f = (r / 10D); else f = (r / 100D);
				d = (int)Math.Round(f);
				ret += "." + d.ToString();
			}
			else
			{
				double ds = (double)size;
				double dk = Math.Round(ds / 1024D);
				k = (int)dk;
				ret = k.ToString(FORMAT_FILESIZE);
			}
			ret += " KB";
			return ret;
		}

		private static string MegaBytes(long size)
		{
			long m = size >> 20;
			string ret = m.ToString(FORMAT_FILESIZE);
			if (m < 100)
			{
				double r = (int)(size % 0x10000);
				int d = 0;
				double f = 0;
				if (m < 10) f = (r / 10000D); else f = (r / 100000D);
				d = (int)Math.Round(f);
				ret += "." + d.ToString();
			}
			else
			{
				double ds = (double)size;
				double dm = Math.Round(ds / 1048576D);
				m = (int)dm;
				ret = m.ToString(FORMAT_FILESIZE);
			}
			ret += " MB";
			return ret;
		}

		private static string GigaBytes(long size)
		{
			long g = size >> 30;
			string ret = g.ToString(FORMAT_FILESIZE);
			if (g < 100)
			{
				double r = (int)(size % 0x40000000);
				int d = 0;
				double f = 0;
				if (g < 10) f = (r / 10000000D); else f = (r / 100000000D);
				d = (int)Math.Round(f);
				ret += "." + d.ToString();
			}
			else
			{
				double ds = (double)size;
				double dg = Math.Round(ds / 1073741824D);
				g = (int)dg;
				ret = g.ToString(FORMAT_FILESIZE);
			}
			ret += " GB";
			return ret;
		}

		public static string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title != "")
					{
						return titleAttribute.Title;
					}
				}
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public static long GetFileSize(string filename)
		{
			long ret = 0;
			if (File.Exists(filename))
			{
				FileInfo fi = new FileInfo(filename);
				ret = fi.Length;
			}
			return ret;
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
			//! Use AssemblyTitle instead?
			string foo = AssemblyTitle;
			mySubDir += Application.ProductName + "\\";
			mySubDir = mySubDir.Replace("-", "");
			tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			return tempPath;
		}

		public static string ReplaceInvalidFilenameCharacters(string oldName)
		{
			//! This is for the main part of the filename only!
			//! It replaces things like \ and : so it can't be used for full paths + names
			string newName = oldName.Replace('<', '＜');
			newName = newName.Replace('>', '＞');
			newName = newName.Replace(':', '﹕');
			newName = newName.Replace('\"', '＂');
			newName = newName.Replace('/', '∕');
			newName = newName.Replace('\\', '＼');
			newName = newName.Replace('?', '？');
			newName = newName.Replace('|', '￤');
			newName = newName.Replace('$', '§');
			newName = newName.Replace('*', '＊');
			return newName;
		}

		public static bool ValidFilename(string theName, bool testPath = false, bool testExists = false)
		{
			bool isValid = false;
			try
			{
				isValid = !string.IsNullOrEmpty(theName) && theName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
				if (isValid)
				{
					if (testPath)
					{
						string p = Path.GetDirectoryName(theName);
						isValid = Directory.Exists(p);
						if (isValid)
						{
							if (testExists)
							{
								isValid = File.Exists(theName);
							}
						}
					}
				}
			}
			catch
			{ }
			return isValid;
		}

		public static int SafeCopy(string fromFile, string toFile, bool overWrite = true)
		{
			int err = 0;
			try
			{
				System.IO.File.Copy(fromFile, toFile, overWrite);
			}
			catch (Exception e)
			{
				err = e.HResult;
			}
			return err;
		}

		public static string FixInvalidFilenameCharacters(string invalidFilename)
		{
			string validFilename = invalidFilename.Replace("*", "+");
			validFilename = validFilename.Replace("\"", "''");
			validFilename = validFilename.Replace("/", "-");
			validFilename = validFilename.Replace("\\", "_");
			validFilename = validFilename.Replace(':', ';');
			validFilename = validFilename.Replace('|', '!');
			validFilename = validFilename.Replace("<", "{");
			validFilename = validFilename.Replace(">", "}");
			validFilename = validFilename.Replace("?", "`");
			return validFilename;
		}

		private static string FixInvalidFilenameCharactersWithUnicode(string invalidFilename)
		{
			string validFilename = invalidFilename.Replace('*', '✻');
			validFilename = validFilename.Replace("\"", "”");
			validFilename = validFilename.Replace('/', '̷');
			validFilename = validFilename.Replace("\\", "╲");
			validFilename = validFilename.Replace(':', '∶');
			validFilename = validFilename.Replace('|', '∣');
			validFilename = validFilename.Replace('<', '≺');
			validFilename = validFilename.Replace('>', '≻');
			validFilename = validFilename.Replace('?', 'ʔ');
			return validFilename;
		}

		public static int LaunchFile(string theFile)
		{
			int err = 0;
			try
			{
				System.Diagnostics.Process sdp = System.Diagnostics.Process.Start(theFile);
				var q = sdp.MainWindowTitle;
			}
			catch (Exception ex)
			{
				err++;				
			}
			return err;
		}
		#endregion Generic File Functios
		*/

		#region TimeFunctions
		public static string FormatTime(int centiseconds)
		{
			string timeOut = "";

			int totsecs = (int)(centiseconds / 100);
			int centis = centiseconds % 100;
			int min = (int)(totsecs / 60);
			int secs = totsecs % 60;

			if (min > 0)
			{
				timeOut = min.ToString() + ":";
				timeOut += secs.ToString("00");
			}
			else
			{
				timeOut = secs.ToString();
			}
			timeOut += "." + centis.ToString("00");

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

		public static string Time_CentisecondsToMinutes(int centiseconds)
		{
			int mm = (int)(centiseconds / 6000);
			int ss = (int)((centiseconds - mm * 6000) / 100);
			int cs = (int)(centiseconds - mm * 6000 - ss * 100);
			string ret = mm.ToString("0") + ":" + ss.ToString("00") + "." + cs.ToString("00");
			return ret;
		}

		public static int Time_MinutesToCentiseconds(string timeInMinutes)
		{
			// Time string must be formated as mm:ss.cs
			// Where mm is minutes.  Must be specified, even if zero.
			// Where ss is seconds 0-59.
			// Where cs is centiseconds 0-99.  Must be specified, even if zero.
			// Time string must contain one colon (:) and one period (.)
			// Maximum of 60 minutes  (Anything longer can result in unmanageable sequences)
			string newTime = timeInMinutes.Trim();
			int ret = UNDEFINED;
			int posColon = newTime.IndexOf(':');
			if ((posColon > 0) && (posColon < 3))
			{
				int posc2 = newTime.IndexOf(':', posColon + 1);
				if (posc2 < 0)
				{
					string min = newTime.Substring(0, posColon);
					string rest = newTime.Substring(posColon + 1);
					int posPer = rest.IndexOf('.');
					if ((posPer == 2))
					{
						int posp2 = rest.IndexOf('.', posPer + 1);
						if (posp2 < 0)
						{
							string sec = rest.Substring(0, posPer);
							string cs = rest.Substring(posPer + 1);
							int mn = lutils.UNDEFINED;
							int.TryParse(min, out mn);
							if ((mn >= 0) && (mn < 61))
							{
								int sc = lutils.UNDEFINED;
								int.TryParse(sec, out sc);
								if ((sc >= 0) && (sc < 60))
								{
									int c = lutils.UNDEFINED;
									int.TryParse(cs, out c);
									if ((c >= 0) && (c < 100))
									{
										ret = mn * 6000 + sc * 100 + c;
									}
								}
							}
						}
					}
				}
			}

			return ret;
		}

		#endregion // Time Functions

		/* moved to FileHelper.Fyle
		public static void MakeNoise(Noises noise)
		{
			if (noise != Noises.None)
			{
				string file = "";
				switch (noise)
				{
					case Noises.Activate:
						file += "Activate";
						break;
					case Noises.Boing:
						file += "Boing";
						break;
					case Noises.Bonnggg:
						file += "Bonnggg";
						break;
					case Noises.Brain:
						file += "Brain";
						break;
					case Noises.Crap:
						file += "Crap";
						break;
					case Noises.Crash:
						file += "Crash";
						break;
					case Noises.Dammit:
						file += "Dammit";
						break;
					case Noises.Doh:
						file += "D'oh";
						break;
					case Noises.DrumRoll:
						file += "DrumRoll";
						break;
					case Noises.Excellent:
						file += "Excellent";
						break;
					case Noises.Gong:
						file += "Gong";
						break;
					case Noises.Kalimbra:
						file += "Kalimbra";
						break;
					case Noises.Log:
						file += "Log";
						break;
					case Noises.Medievel:
						file += "Medievel";
						break;
					case Noises.Pop:
						file += "Pop";
						break;
					case Noises.SamCurseC:
						file += "SamCurseC";
						break;
					case Noises.SamCurseF:
						file += "SamCurseF";
						break;
					case Noises.ScaleUp:
						file += "ScaleUp";
						break;
					case Noises.SystemWorks:
						file += "SystemWorks";
						break;
					case Noises.ThatsThat:
						file += "ThatsThat";
						break;
					case Noises.TaDa:
						file += "Ta-da";
						break;
					case Noises.Wheee:
						file += "Wheee";
						break;
					case Noises.Wizard:
						file += "I'mAWizard";
						break;
					case Noises.WooHoo:
						file += "Woo-Hoo";
						break;
					case Noises.WrongButton:
						file += "WrongButton";
						break;
				}
				if (file.Length > 1)
				{
					string wavFile = noisePath + file + ".wav";
					try
					{
						System.Media.SoundPlayer player = new System.Media.SoundPlayer(wavFile);
						player.Play();
					}
					catch (Exception e)
					{
						// Ignore error, don't play any noise
					}
				}
			}
		}
		*/
		public static int ExceptionLineNumber(Exception ex)
		{
			// Get stack trace for the exception with source file information
			StackTrace st = new StackTrace(ex, true);
			// Get the top stack frame
			StackFrame frame = st.GetFrame(0);
			// Get the line number from the stack frame
			int line = frame.GetFileLineNumber();
			return line;
		}


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

			// LORLoop4 thu ALL names
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


	} // end class lutils
} // end namespace LORUtils4
