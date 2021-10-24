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


		#region TreeStuff
		/*
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
			if (track.Nodes == null)
			{
				//nodeList = new List<TreeNode>();
				//track.Nodes = nodeList;
			}
			else
			{
				//nodeList = (List<TreeNode>)track.Nodes;
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
					if (group.Nodes == null)
					{
						nodeList = new List<TreeNode>();
						group.Nodes = nodeList;
					}
					else
					{
						nodeList = (List<TreeNode>)group.Nodes;
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
				//LORCosmic4 device = (LORCosmic4)seq.Members.BySavedIndex[deviceSI];

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
			//LORChannel4 channel = (LORChannel4)seq.Members.BySavedIndex[channelSI];
			int channelSI = channel.SavedIndex;
			string nodeText = channel.Name;
			TreeNode channelNode = baseNodes.Add(nodeText);
			List<TreeNode> nodeList;
			//iLORMember4 nodeTag = channel;
			nodeIndex++;
			channelNode.Tag = channel;
			if (channel.Nodes == null)
			{
				nodeList = new List<TreeNode>();
				channel.Nodes = nodeList;
			}
			else
			{
				nodeList = (List<TreeNode>)channel.Nodes;
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
				iLORMember4 mbrR = seq.Members.BySavedIndex[RGBsi];
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
					if (rgbChannel.Nodes == null)
					{
						nodeList = new List<TreeNode>();
						rgbChannel.Nodes = nodeList;
					}
					else
					{
						nodeList = (List<TreeNode>)rgbChannel.Nodes;
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
						//channelNode.Nodes.Add(colorNode);
						if (rgbChannel.redChannel.Nodes == null)
						{
							nodeList = new List<TreeNode>();
							rgbChannel.redChannel.Nodes = nodeList;
						}
						else
						{
							nodeList = (List<TreeNode>)rgbChannel.redChannel.Nodes;
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
						if (rgbChannel.grnChannel.Nodes == null)
						{
							nodeList = new List<TreeNode>();
							rgbChannel.grnChannel.Nodes = nodeList;
						}
						else
						{
							nodeList = (List<TreeNode>)rgbChannel.grnChannel.Nodes;
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
						if (rgbChannel.bluChannel.Nodes == null)
						{
							nodeList = new List<TreeNode>();
							rgbChannel.bluChannel.Nodes = nodeList;
						}
						else
						{
							nodeList = (List<TreeNode>)rgbChannel.bluChannel.Nodes;
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
		*/
		#endregion // Tree Stuff
