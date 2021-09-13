		private static TreeNode Original_AddGroup(Sequence4 seq, TreeNodeCollection baseNodes, int groupIndex, List<List<TreeNode>> nodesBySI, bool selectedOnly, bool includeRGBchildren, int memberTypes)
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
				groupNode.Checked = theGroup.Selected;
				baseNodes = groupNode.Nodes;
			}
			List<TreeNode> qlist;

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
						TreeNode subGroupNode = Original_AddGroup(seq, baseNodes, member.Index, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
						qlist = nodesBySI[si];
						qlist.Add(subGroupNode);
					}
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
						TreeNode subGroupNode = Original_AddGroup(seq, baseNodes, member.Index, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
						qlist = nodesBySI[si];
						qlist.Add(subGroupNode);
					}
				}
				if (member.MemberType == MemberType.Channel)
				{
				if ((memberTypes & SeqEnums.MEMBER_Channel) > 0)
					{
						TreeNode channelNode = AddChannel(seq, baseNodes, member.Index, selectedOnly);
						qlist = nodesBySI[si];
						qlist.Add(channelNode);
					}
				}
				if (member.MemberType == MemberType.RGBchannel)
				{
					if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0)
					{
						TreeNode rgbChannelNode = Original_AddRGBchannel(seq, baseNodes, member.Index, ref nodesBySI, selectedOnly, includeRGBchildren);
						qlist = nodesBySI[si];
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
			utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
		} // end catch
		*/
				#endregion

			} // End loop thru items
			return groupNode;
		} // end AddGroup

		private static TreeNode Original_AddCosmic(Sequence4 seq, TreeNodeCollection baseNodes, int deviceIndex, List<List<TreeNode>> nodesBySI, bool selectedOnly, bool includeRGBchildren, int memberTypes)
		{
			//ChanInfo nodeTag = new ChanInfo();
			//nodeTag.MemberType = MemberType.ChannelGroup;
			//nodeTag.objIndex = groupIndex;
			//nodeTag.SavedIndex = theGroup.SavedIndex;
			//nodeTag.nodeIndex = nodeIndex;

			CosmicDevice theDevice = seq.CosmicDevices[deviceIndex];

			//IMember groupID = theGroup;
			string nodeText = theDevice.Name;
			TreeNode deviceNode = null;

			if ((memberTypes & SeqEnums.MEMBER_CosmicDevice) > 0)
			{
				deviceNode = baseNodes.Add(nodeText);

				nodeIndex++;
				deviceNode.Tag = theDevice;
				deviceNode.ImageKey = ICONcosmicDevice;
				deviceNode.SelectedImageKey = ICONcosmicDevice;
				deviceNode.Checked = theDevice.Selected;
				baseNodes = deviceNode.Nodes;
			}
			List<TreeNode> qlist;

			// const string ERRproc = " in FillChannels-AddGroup(";
			// const string ERRgrp = "), in Device #";
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
						TreeNode subGroupNode = Original_AddGroup(seq, baseNodes, member.Index, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
						qlist = nodesBySI[si];
						qlist.Add(subGroupNode);
					}
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
						TreeNode subGroupNode = Original_AddGroup(seq, baseNodes, member.Index, ref nodesBySI, selectedOnly, includeRGBchildren, memberTypes);
						qlist = nodesBySI[si];
						qlist.Add(subGroupNode);
					}
				}
				if (member.MemberType == MemberType.Channel)
				{
					if ((memberTypes & SeqEnums.MEMBER_Channel) > 0)
					{
						TreeNode channelNode = AddChannel(seq, baseNodes, member.Index, selectedOnly);
						qlist = nodesBySI[si];
						qlist.Add(channelNode);
					}
				}
				if (member.MemberType == MemberType.RGBchannel)
				{
					if ((memberTypes & SeqEnums.MEMBER_RGBchannel) > 0)
					{
						TreeNode rgbChannelNode = Original_AddRGBchannel(seq, baseNodes, member.Index, ref nodesBySI, selectedOnly, includeRGBchildren);
						qlist = nodesBySI[si];
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
			utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
		} // end catch
		*/
				#endregion

			} // End loop thru items
			return deviceNode;
		} // end AddGroup
		
				private static TreeNode Original_AddChannel(Sequence4 seq, TreeNodeCollection baseNodes, int channelIndex, bool selectedOnly)
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
			channelNode.Checked = theChannel.Selected;


			return channelNode;
		}

		private static TreeNode Original_AddRGBchannel(Sequence4 seq, TreeNodeCollection baseNodes, int RGBIndex, List<List<TreeNode>> nodesBySI, bool selectedOnly, bool includeRGBchildren)
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
			channelNode.Checked = theRGB.Selected;

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
				colorNode.Checked = seq.Channels[ci].Selected;
				qlist = nodesBySI[seq.Channels[ci].SavedIndex];
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
				colorNode.Checked = seq.Channels[ci].Selected;
				qlist = nodesBySI[seq.Channels[ci].SavedIndex];
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
				colorNode.Checked = seq.Channels[ci].Selected;
				qlist = nodesBySI[seq.Channels[ci].SavedIndex];
				qlist.Add(colorNode);
			} // end includeRGBchildren

			return channelNode;
		}


