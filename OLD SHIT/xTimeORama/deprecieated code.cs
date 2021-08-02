			//SelectChannel(ci.SavedIndex, select);
			SelectChannel(nOde, select);
			if (select)
			{
				IncrementSelectionCount(1);
			}
			else
			{
				IncrementSelectionCount(-1);
			}
			if (nOde.Nodes.Count > 0)
			{
				// And all seq.Members
				SelectChildNodes(nOde.Nodes, select);
			}
			// and now parents
			TreeNodeAdv pArent = nOde.Parent;
			while (pArent != null)
			{
				IMember pm = (IMember)pArent.Tag;
				if (select)
				{
					// If turning on the select, turn on each parent
					if (pm.Selected != select)
					{
						pm.Selected = select;
						HighlightNodeBackground(pArent, select);
						IncrementSelectionCount(1);
					}
				}
				else
				{
					bool otherMembers = HasSelectedMembers(pArent);
					if (otherMembers)
					{
						// Has other seq.Members that are still selected
						// therefore, do not unhighlight it
					}
					else
					{
						// Has no remaining seq.Members selected
						// OK to unhighlight it
						if (pm.Selected != select)
						{
							pm.Selected = false;
							HighlightNodeBackground(pArent, select);
							IncrementSelectionCount(-1);
						}
					}
				}
				pArent = pArent.Parent;
			}
		} // end highlight node

		void SelectChildNodes(TreeNodeAdvCollection nOdes, bool select)
		{
			foreach (TreeNodeAdv nOde in nOdes)
			{
				IMember m = (IMember)nOde.Tag;
				// Highlight all seq.Members
				if (m.Selected != select)
				{
					m.Selected = select;
					HighlightNodeBackground(nOde, select);
					if (select)
					{
						IncrementSelectionCount(1);
					}
					else
					{
						IncrementSelectionCount(-1);
					}
					if (nOde.Nodes.Count > 0)
					{
						SelectChildNodes(nOde.Nodes, select);
					}
				}
			}
		} // end highlight child nodes

		void SelectChannel(TreeNodeAdv nOde, bool select)
		{
			IMember m = (IMember)nOde.Tag;
			//List<TreeNodeAdv> chNodes = siNodes[m.SavedIndex];
			int selCount = 0;
			//bool selSome = false;

			//for (int cx=0; cx< chNodes.Count; cx++)
			{
				//IMember m2 = (IMember)chNodes[cx].Tag;
				//if (m2.Selected)
				{
					selCount++;
				}
			}
			//if (selCount > 0) selSome = true; else selSome = false;
			//selSome = (selCount > 0);  //? Why doesn't this work?
			//for (int cx = 0; cx < chNodes.Count; cx++)
			{
				//ItalisizeNode(chNodes[cx], selSome);
			}
		}

		/*
		void SelectNodes(int nodeSI, bool select, bool andMembers)
		{
			IMember m;
			List<TreeNodeAdv> qlist;

			//if (siNodes[nodeSI]!= null)
			if (nodeSI == utils.UNDEFINED)
			{
				// WHY?
				int xx = 1;
			}
			else
			{
				qlist = siNodes[nodeSI];
				if (qlist.Count > 0)
				{
					//if (siNodes[nodeSI].Length > 0)
					//if (siNodes[nodeSI])
					//{
					//foreach (TreeNodeAdv nOde in siNodes[nodeSI])
					for (int q = 0; q < siNodes[nodeSI].Count; q++)
					{
						TreeNodeAdv nOde = siNodes[nodeSI][q];
						m = (IMember)nOde.Tag;
						if (select)
						{
							if (!m.Selected) // sanity check, should not be checked
							{
								m.Selected = true;
								IncrementSelectionCount(1);
								nOde.ForeColor = Color.Yellow;
								nOde.BackColor = Color.DarkBlue;
								if (andMembers)
								{
									if (nOde.Nodes.Count > 0)
									{
										foreach (TreeNodeAdv childNode in nOde.Nodes)
										{
											m = (IMember)childNode.Tag;
											SelectNodes(m.SavedIndex, select, true);
										}
									}
								}
								if (nOde.Parent != null)
								{
									m = (IMember)nOde.Parent.Tag;
									SelectNodes(m.SavedIndex, select, false);
								}
							} // node.!checked
						}
						else // !select
						{
							if (m.Selected) // sanity check, should be checked
							{
								m.Selected = false;
								IncrementSelectionCount(-1);
								nOde.ForeColor = SystemColors.WindowText;
								nOde.BackColor = SystemColors.Window;
								if (andMembers)
								{
									if (nOde.Nodes.Count > 0)
									{
										foreach (TreeNodeAdv childNode in nOde.Nodes)
										{
											m = (IMember)childNode.Tag;
											SelectNodes(m.SavedIndex, select, true);
										}
									}
								}
								if (nOde.Parent != null)
								{
									if (!HasSelectedMembers(nOde.Parent))
									{
										m = (IMember)nOde.Parent.Tag;
										SelectNodes(m.SavedIndex, select, false);
									}
								}
							} // node.checked
						} // if (select)
					} // foreach (TreeNodeAdv nOde in siNodes[nodeSI])
						//}
				} // siNodes[nodeSI].Count > 0
				else
				{
					// siNodes[nodeSI].Count = 0
					//? Why Not?
					int x = 1;
				}
			}
		} // end SelectNode
		*/

		private bool HasSelectedMembers(TreeNodeAdv nOde)
		{
			bool ret = false;
			if (nOde.Nodes.Count > 0)
			{
				foreach (TreeNodeAdv childNode in nOde.Nodes)
				{
					IMember m = (IMember)childNode.Tag;
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
		private void ItalisizeNode(TreeNodeAdv nOde, bool italisize)
		{
			if (italisize)
			{
				nOde.Font = new Font(treChannels.Font, FontStyle.Italic);
							}
			else
			{
				nOde.Font = new Font(treChannels.Font, FontStyle.Regular);
			}
		}
		private void EmboldenNode(TreeNodeAdv nOde, bool embolden)
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
		private void HighlightNodeBackground(TreeNodeAdv nOde, bool highlight)
		{
			if (highlight)
			{
				//!nOde.Background.BackColor = HIGHLIGHTBACKGROUND;
			}
			else
			{
				//!nOde.BackColor = REGULARBACKGROUND;
			}
		}
		private void ColorNodeText(TreeNodeAdv nOde, bool colorize)
		{
			if (colorize)
			{
				//!nOde.ForeColor = COLOREDTEXT;
			}
			else
			{
				//!nOde.ForeColor = REGULARTEXT;
			}
		}
		#endregion

		private void treChannels_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			//UserSelectNode(e.Node);
		}

		private void CopySelectionsToSequence()
		{
			/*
			// Clear any previous selections
			for (int tr=0; tr< seq.Tracks.Count; tr++)
			{
				seq.Tracks[tr].Selected = false;
			}
			for (int tg = 0; tg < seq.TimingGrids.Count; tg++)
			{
				seq.TimingGrids[tg].Selected = false;
			}
			for (int chg=0; chg<seq.ChannelGroups.Count; chg++)
			{
				seq.ChannelGroups[chg].Selected = false;
			}
			for (int rch=0; rch< seq.RGBchannels.Count; rch++)
			{
				seq.RGBchannels[rch].Selected = false;
			}
			for (int ch=0; ch< seq.Channels.Count; ch++)
			{
				seq.Channels[ch].Selected = false;
			}
			for (int tg=0; tg<seq.TimingGrids.Count; tg++)
			{
				seq.TimingGrids[tg].Selected = false;
			}

			///*
			// Select only timing grids used by selected tracks
			foreach (TreeNodeAdv nOde in treChannels.Nodes)
			{
				if (nOde.Checked)
				{
					IMember nodeTag = (IMember)nOde.Tag;
					if (nodeTag.MemberType == MemberType.Track)  // Just a sanity check, all first level nodes should be tracks
					{
						Track t = seq.Tracks[nodeTag.Index];
						//int tgIdx = t.timingGridObjIndex;
						//for (int tg = 0; tg < seq.TimingGrids.Count; tg++)
						//{
						//	if (seq.TimingGrids[tg].saveID == tgIdx)
						//	{
						//		seq.TimingGrids[tg].Selected = true;
						//	}
						seq.TimingGrids[t.timingGrid.Index].Selected = true;
						//}
					}
				}
			}
			// * /

			//CopyNodeSelectionsToSequence(treChannels.Nodes);
			*/
		} // end CopySelectionToSequence

		private void CopyNodeSelectionsToSequence(TreeNodeAdvCollection nOdes)
		{
			/*
			IMember nodeTag;

			for (int i = 0; i < gridItem_Checked.Length; i++)
			{
				seq.TimingGrids[i].Selected = gridItem_Checked[i];
			}

			foreach (TreeNodeAdv nOde in nOdes)
			{
				if (nOde.Checked)
				{
					nodeTag = (IMember)nOde.Tag;
					if (nodeTag.MemberType == MemberType.Track)
					{
						seq.Tracks[nodeTag.Index].Selected = nOde.Checked;
					}
					if (nodeTag.MemberType == MemberType.Channel)
					{
						seq.Channels[nodeTag.Index].Selected = nOde.Checked;
					}
					if (nodeTag.MemberType == MemberType.RGBchannel)
					{
						seq.RGBchannels[nodeTag.Index].Selected = nOde.Checked;
					}
					if (nodeTag.MemberType == MemberType.ChannelGroup)
					{
						seq.ChannelGroups[nodeTag.Index].Selected = nOde.Checked;
					}
					if (nOde.Nodes.Count > 0)
					{
						CopyNodeSelectionsToSequence(nOde.Nodes);
					}
				} // loop thru nodes
			}
			*/


		} // end CopyNodeSelectionToSequence
