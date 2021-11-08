using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using LORUtils4;
using FileHelper;
using FuzzyString;
using Syncfusion.Windows.Forms.Tools;

namespace UtilORama4
{
	public partial class frmRemapper : Form
	{


		#region Node Operations
		private TreeNodeAdv NodeFind(TreeNodeAdvCollection treeNodes, string tag)
		{
			int tl = tag.Length;
			string st = "";
			TreeNodeAdv ret = new TreeNodeAdv("NOT FOUND");
			bool found = false;
			foreach (TreeNodeAdv nOde in treeNodes)
			{
				if (!found)
				{
					if (nOde.Tag.ToString().Length >= tag.Length)
					{
						st = nOde.Tag.ToString().Substring(0, tl);
						if (tag.CompareTo(st) == 0)
						{
							ret = nOde;
							found = true;
							break;
						}
					}
					if (!found)
					{
						if (nOde.Nodes.Count > 0)
						{
							ret = NodeFind(nOde.Nodes, tag);
							if (ret.Text.CompareTo("NOT FOUND") != 0)
							{
								found = true;
							}
						}
					}
				}
			}
			return ret;
		}
		private void NodesUnselectAll(TreeNodeAdvCollection treeNodes)
		{
			foreach (TreeNodeAdv nOde in treeNodes)
			{
				// Reset any previous selections
				NodeHighlight(nOde, false);
				if (nOde.Nodes.Count > 0)
				{
					NodesUnselectAll(nOde.Nodes);
				}
			} // end foreach
		}



		private void olddeadcode(iLORMember4 sourceMember)
		{ 
			if (lastSelectedSourceMember != null)
			{
				TreeUtils.HighlightMember(lastSelectedSourceMember, ColorDestSelectedNormalFG, ColorDestSelectedNormalBG);
				if (lastSelectedDestMember.MapTo != null)
				{
					if (lastSelectedSourceMember.ID == lastSelectedSourceMember.MapTo.ID)
					{

					}
				}


			}
			// Unhighlight anything the previous source was mapped to
			List<iLORMember4> mapsTo = (List<iLORMember4>)lastSelectedSourceMember.Tag;
			int lastDestID = lutils.UNDEFINED;
			if (lastSelectedDestMember != null) lastDestID = lastSelectedDestMember.ID;
			for (int m = 0; m < mapsTo.Count; m++)
			{
				// Was it mapped to the current destination
				if (mapsTo[m].ID == lastSelectedDestMember.ID)
				{
					TreeUtils.HighlightMember(lastSelectedDestMember, ColorDestSelectedNormalFG, ColorDestSelectedNormalBG);
					TreeUtils.HighlightMember(lastSelectedDestMember.MapTo, ColorMappedSourceFG, ColorMappedSourceBG);
				}
				else
				{
					TreeUtils.HighlightMember(mapsTo[m], ColorUnselectedFG, ColorUnselectedBG);
				}
			}




			// Is the new source member mapped to dest member(s)?
			if (sourceMember.RuleID > 0)
			{
				// is the selected destination member mapped to this new source member?
				int partner = lutils.UNDEFINED;
				List<iLORMember4> mapsTo = (List<iLORMember4>)sourceMember.Tag;
				for (int m=0; m<mapsTo.Count; m++)
				{
					if (mapsTo[m].ID == lastSelectedDestMember.ID)
					{
						partner = m;
						m = mapsTo.Count; // Force exit of loop
					}
				}
				if (partner >=0)
				{

				}

			}
			
			SelectSourceMember(sourceMember);
			UpdateMappability();
			// Remember this selection
			lastSelectionWasDest = false;
			lastSelectionWasSource = true;
			#region label source text
			List<iLORMember4> mappedTo = (List<iLORMember4>)sourceMember.Tag;
			string lblTxt = sourceMember.Name;
			if (mappedTo.Count == 0)
			{
				lblTxt += " is unmapped.";
			}
			else
			{
				lblTxt += " is mapped to ";
				for (int m=0; m< mappedTo.Count; m++)
				{
					lblTxt += mappedTo[m].Name + ", ";
				}
				lblTxt = lblTxt.Substring(0, lblTxt.Length - 2);
				int cp = lblTxt.LastIndexOf(",");
				if (cp >0)
				{
					lblTxt = lblTxt.Substring(0, cp + 2) + "and " + lblTxt.Substring(cp + 2);
				}
				lblSourceMappedTo.Text = lblTxt;
				#endregion
			}

		}

		public void UpdateHighlights()
		{
			// I think, for now at least, the easiest is just to undo everything and start over
			if (lastSelectedSourceMember != null)
			{ TreeUtils.HighlightMember(lastSelectedSourceMember, ColorUnselectedFG, ColorUnselectedBG); }
			if (lastSelectedDestMember != null)
			{ TreeUtils.HighlightMember(lastSelectedDestMember, ColorUnselectedFG, ColorUnselectedBG); }
			if (lastHighlightedSourceMember != null)
			{ TreeUtils.HighlightMember(lastHighlightedSourceMember, ColorUnselectedFG, ColorUnselectedBG); }
			for (int n = 0; n < lastHighlightedDestMembers.Count; n++)
			{ TreeUtils.HighlightMember(lastHighlightedDestMembers[n], ColorUnselectedFG, ColorUnselectedBG); }

			if (lastSelectedDestMember != null)
			{
				TreeUtils
			}

		}
		// Nodes & Members which have been mapped are BOLD
		// All the Nodes for Members which are selected are Highlighted
		// Nodes & Members which...
		private void SelectSourceMember(iLORMember4 sourceMember, bool select)
		{
			// Did the selection actually change?
			if (sourceMember != lastSelectedSourceMember)
			{
				// Was something previously selected?
				if (lastSelectedSourceMember != null)
				{
					// Un-Highlight the previous selection.
					TreeUtils.SelectMember(lastSelectedSourceMember, false);
				}
				// Highlight the new selection.
				TreeUtils.SelectMember(sourceMember, true);

				// Is the new selection a regular channel?
				if (sourceMember.MemberType == LORMemberType4.Channel)
				{
					// Cast source to regular channel.
					LORChannel4 sourceChan = (LORChannel4)sourceMember;
					// Show a preview of any effects
					Bitmap bmp = lutils.RenderEffects(sourceChan, 0, sourceChan.Centiseconds, 300, 20, true);
					picPreviewSource.Visible = true;
					picPreviewSource.Image = bmp;
					//picPreview.Refresh();
				}
				else // NOT a regular channel
				{
					// Is the new selction an RGB channel?
					if (sourceMember.MemberType == LORMemberType4.RGBChannel)
					{
						// Cast source to RGB channel
						LORRGBChannel4 sourceRGB = (LORRGBChannel4)sourceMember;
						// Show a preview of any effects
						Bitmap bmp = lutils.RenderEffects(sourceRGB, 0, sourceRGB.Centiseconds, 300, 21, false);
						picPreviewSource.Image = bmp;
						picPreviewSource.Visible = true;
					}
					else // NOT an RGB channel either
					{
						// Is the new selection a group-type member (Track, ChannelGroup, or CosmicDevice)
						if ((sourceMember.MemberType == LORMemberType4.ChannelGroup) ||
								(sourceMember.MemberType == LORMemberType4.Track) ||
								(sourceMember.MemberType == LORMemberType4.Cosmic))
						{
							// Hide the preview panel
							picPreviewSource.Visible = false;
						} // End a group
					} // End an RGB channel, or not
				} // End a regular channel, or not

				// Was this source member mapped to any destination members?
				if (sourceMember.Tag != null)
				{
					// Get the list of mapped member(s)
					List<iLORMember4> mappedMembers = (List<iLORMember4>)sourceMember.Tag;
					// Anything in it?
					if (mappedMembers.Count > 0)
					{
						for (int mm=0; mm< mappedMembers.Count; mm++)
						{
							iLORMember4 mappedMember = mappedMembers[mm];
							HighlightDestMember(mappedMember);
						}
					}
				}
				lastHighlightedSourceMember = sourceMember;
			}
		}
		private void OldDestMember_Click(iLORMember4 destMember)
		{
			HighlightDestMember(destMember);
			// Remember this selection
			lastSelectionWasDest = true;
			lastSelectionWasSource = false;
			#region label destination text
			string lblTxt = destMember.Name;
			if (destMember.MapTo != null)
			{
				lblTxt += " is unmapped.";
			}
			else
			{
				lblTxt = destMember.MapTo.Name + " is mapped to " + lblTxt

			}
			lblDestMappedTo.Text = lblTxt;
			#endregion

		}
		private void HighlightDestMember(iLORMember4 destMember)
		{ 			
			//? What was this for?
			//Color overBackColor = SystemColors.Control;

			// Did the selection actually change?
			if (destMember != lastSelectedDestMember)
			{
				// Was something previously selected?
				if (lastSelectedDestMember != null)
				{
					// Un-Highlight the previous selection.
					TreeUtils.SelectMember(lastSelectedDestMember, false);
				}
				// Highlight the new selection.
				TreeUtils.SelectMember(destMember, true);

				// Is the new selection a regular channel?
				if (destMember.MemberType == LORMemberType4.Channel)
				{
					// Cast destination to regular channel.
					LORChannel4 masterChan = (LORChannel4)destMember;
					// Show a preview of any effects
					Bitmap bmp = lutils.RenderEffects(masterChan, 0, masterChan.Centiseconds, 300, 20, true);
					picPreviewDest.Visible = true;
					pnlOverwrite.Visible = true;
					picPreviewDest.Image = bmp;
					if (masterChan.effects.Count > 0)
					{
						//? What was this for?
						//overBackColor = Color.Red;
					}
				}
				else // NOT a regular channel
				{
					// Is the new selection an RGB channel?
					if (destMember.MemberType == LORMemberType4.RGBChannel)
					{
						// Cast destination to RGB Channel
						LORRGBChannel4 masterRGB = (LORRGBChannel4)destMember;
						// Show a preview of any effects
						Bitmap bmp = lutils.RenderEffects(masterRGB, 0, masterRGB.Centiseconds, 300, 21, false);
						picPreviewDest.Image = bmp;
						picPreviewDest.Visible = true;
						pnlOverwrite.Visible = true;
						//? What is this for, I don't remember...
						if (mapDestToSource[destMember.SavedIndex] != null)
						{
							if ((masterRGB.redChannel.effects.Count > 0) ||
									(masterRGB.grnChannel.effects.Count > 0) ||
									(masterRGB.bluChannel.effects.Count > 0))
							{
								//overBackColor = Color.Red;
							}
						}
					}
					else // Not a RGB channel either
					{
						// Is the new selection a group type member (Track, ChannelGroup, or CosmicDevice)
						if ((destMember.MemberType == LORMemberType4.ChannelGroup) ||
								(destMember.MemberType == LORMemberType4.Track) ||
								(destMember.MemberType == LORMemberType4.Cosmic))
						{
							// Hide the preview panal
							picPreviewDest.Visible = false;
							pnlOverwrite.Visible = false;
						} // End a group
					} // End RGB, or not
				} // End regular channel, or not
				
				// Is this destination already mapped to a source member?
				if (destMember.MapTo != null)
				{
					// Is it mapped to something other than the selected source member?
					if (destMember.MapTo != lastSelectedSourceMember)
					{
						// Select the mapped source member as well
						HighlightSourceMember(destMember.MapTo);
						btnMap.Enabled = false;
						btnUnmap.Enabled = true;
					}
				}
				lastHighlightedDestMember = destMember;
			} // End newly selected destination != last selected destination
		}
		private void NodesMapSelected()
		{
			string tipText = "";
			// The button should not have even been enabled if the 2 Channels are not eligible to be mapped ... but--
			// Verify it anyway!

			// Is a node Selected on each side?
			if (selectedSourceNode != null)
			{
				if (selectedDestNode != null)
				{
					// Types match?
					masterMapLevel = 0; // Reset
					sourceMapLevel = 0;
					if (selectedDestMember.MemberType == LORMemberType4.Channel)
					{
						if (selectedSourceMember.MemberType == LORMemberType4.Channel)
						{
							int newMaps = MapMembers(selectedDestMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = false; // because already mapped
							tipText = selectedSourceMember.Name + " is now mapped to " + selectedDestMember.Name;
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = true;
							tipText = "Unmap " + selectedSourceMember.Name + " from " + selectedDestMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_MapRegularToRegular;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are not the same type";
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is not mapped to " + selectedDestMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == LORMemberType4.RGBChannel)
					{
						if (selectedDestMember.MemberType == LORMemberType4.RGBChannel)
						{
							int newMaps = MapMembers(selectedDestMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = false; // because already mapped
							tipText = selectedSourceMember.Name + " is now mapped to " + selectedDestMember.Name;
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = true;
							tipText = "Unmap " + selectedSourceMember.Name + " from " + selectedDestMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_MapRGBtoRGB;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is not mapped to " + selectedDestMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == LORMemberType4.ChannelGroup)
					{
						if (selectedDestMember.MemberType == LORMemberType4.ChannelGroup)
						{
							int newMaps = MapMembers(selectedDestMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = false; // because already mapped
							tipText = selectedSourceMember.Name + " is now mapped to " + selectedDestMember.Name;
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = true;
							tipText = "Unmap " + selectedSourceMember.Name + " from " + selectedDestMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_GroupToGroup;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is not mapped to " + selectedDestMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == LORMemberType4.Track)
					{
						statMsg = Resources.MSG_Tracks;
						StatusMessage(statMsg);
						System.Media.SystemSounds.Beep.Play();
						btnMap.Enabled = false; // tracks not mapable at this time
						tipText = "Tracks cannot be mapped";
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = false;
						tipText = selectedSourceMember.Name + " is not mapped to " + selectedDestMember.Name + "\r\n(And Tracks cannot be mapped)";
						ttip.SetToolTip(btnUnmap, tipText);
					}
					if (selectedDestMember.MemberType == LORMemberType4.Track)
					{
						statMsg = Resources.MSG_Tracks;
						StatusMessage(statMsg);
						System.Media.SystemSounds.Beep.Play();
						btnMap.Enabled = false; // Tracks not mappable at this time
						tipText = "Tracks cannot be mapped";
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = false;
						tipText = selectedSourceMember.Name + " is not mapped to " + selectedDestMember.Name + "\r\n(And Tracks cannot be mapped)";
						ttip.SetToolTip(btnUnmap, tipText);
					}
					UpdateMappedCount(0);
				}
				else
				{
					System.Media.SystemSounds.Beep.Play();
					btnMap.Enabled = false;
					tipText = "No destination selected";
					ttip.SetToolTip(btnMap, tipText);
					btnUnmap.Enabled = false;
					ttip.SetToolTip(btnUnmap, tipText);
				}
			}
			else
			{
				System.Media.SystemSounds.Beep.Play();
				btnMap.Enabled = false;
				tipText = "No source is selected";
				ttip.SetToolTip(btnMap, tipText);
				btnUnmap.Enabled = false;
				ttip.SetToolTip(btnUnmap, tipText);
			}
			// Copy enabled state from buttons to menus
			mnuMap.Enabled = btnMap.Enabled;
			mnuUnmap.Enabled = btnUnmap.Enabled;
			mnuSaveNewMap.Enabled = btnSaveMap.Enabled;


		} // end btnMap_Click



		/*		private void NodeHighlight(TreeNodeAdv nOde, bool highlight)
		{
			if (highlight)
			{
				if (nOde.BackColor == COLOR_BK_SELECTED)
				{
					nOde.BackColor = COLOR_BK_BOTH;
				}
				else
				{
					nOde.BackColor = COLOR_BK_MATCHED;
				}
				nOde.ForeColor = COLOR_FR_SELECTED;
			}
			else
			{
				if (nOde.BackColor == COLOR_BK_BOTH)
				{
					nOde.BackColor = COLOR_BK_SELECTED;
				}
				else
				{
					nOde.BackColor = COLOR_BK_NONE;
					nOde.ForeColor = COLOR_FR_NONE;
				}
			}
		}
		*/

		private void NodeHighlight(TreeNodeAdv nOde, bool highlight)
		{
			bool wasSelected = false;
			//string bs = nOde.BaseStyle;
			Color backColor = nOde.Background.BackColor;
			if ((backColor == COLOR_BK_SelUnhigh) || (backColor == COLOR_BK_SelHigh))
			//if ((bs.CompareTo("Sh") == 0) || (bs.CompareTo("SH") == 0))
			//f (bs.Contains("S"))
							{
				wasSelected = true;
			}

			if (highlight)
			{
				if (wasSelected)
				{
					nOde.TextColor = COLOR_FR_SelHigh;
					//nOde.CheckColor = COLOR_BK_SelHigh;
					//nOde.BaseStyle = "SH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelHigh);
				}
				else
				{
					nOde.TextColor = COLOR_FR_UnselHigh;
					//nOde.CheckColor = COLOR_BK_UnselHigh;
					//nOde.BaseStyle = "sH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselHigh);
				}
			}
			else
			{
				if (wasSelected)
				{
					nOde.TextColor = COLOR_FR_SelUnhigh;
					//nOde.CheckColor = COLOR_BK_SelUnhigh;
					//nOde.BaseStyle = "Sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelUnhigh);
				}
				else
				{
					nOde.TextColor = COLOR_FR_UnselUnhigh;
					//nOde.CheckColor = COLOR_BK_UnselUnhigh;
					//nOde.BaseStyle = "sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselUnhigh);
				}
			}
		}
		private void NodeSelect(TreeNodeAdv nOde, bool select)
		{
			bool wasHighlighted = false;
			//string bs = nOde.BaseStyle;
			Color backColor = nOde.Background.BackColor;
			if ((backColor == COLOR_BK_SelHigh) || (backColor == COLOR_BK_UnselHigh))
			//if ((bs.CompareTo("SH")==0)||(bs.CompareTo("sH")==0))
			//if (bs.Contains("H"))
			{
				wasHighlighted = true;
			}

			if (select)
			{
				if (wasHighlighted)
				{
					nOde.TextColor = COLOR_FR_SelHigh;
					//nOde.CheckColor = COLOR_BK_SelHigh;
					//nOde.BaseStyle = "SH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelHigh);
				}
				else
				{
					nOde.TextColor = COLOR_FR_SelUnhigh;
					//nOde.CheckColor = COLOR_BK_SelUnhigh;
					//nOde.BaseStyle = "Sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelUnhigh);
				}
			}
			else
			{
				if (wasHighlighted)
				{
					nOde.TextColor = COLOR_FR_UnselHigh;
					//nOde.CheckColor = COLOR_BK_UnselHigh;
					//nOde.BaseStyle = "sH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselHigh);
				}
				else
				{
					nOde.TextColor = COLOR_FR_UnselUnhigh;
					//nOde.CheckColor = COLOR_BK_UnselUnhigh;
					//nOde.BaseStyle = "sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselUnhigh);
				}
			}
		}

		/*		private void NodesSelect(TreeNodeCollection nOdes, int SavedIndex, bool select)
		{
			//string nTag = "";
			iLORMember4 nID = null;
			foreach (TreeNodeAdv nOde in nOdes)
			{
				nID = (iLORMember4)nOde.Tag;
				if (nID.SavedIndex == SavedIndex)
				{
					NodeSelect(nOde, select);
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					NodesSelect(nOde.Nodes, SavedIndex, select);
				}
			}
			//nOdes[0].EnsureVisible();
		}
		*/

		private void NodesBold(List<TreeNodeAdv> nodeList, bool isMapped)
		{
			if (nodeList.Count < 1)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				for (int i = 0; i < nodeList.Count; i++)
				{
					TreeNodeAdv nOde = nodeList[i];
					if (isMapped)
					{
						nOde.Font = new Font(treeSource.Font, FontStyle.Bold);
						nOde.Checked = true;
					}
					else
					{
						nOde.Font = new Font(treeSource.Font, FontStyle.Regular);
						nOde.Checked = false;
					}
				}
			}
		}
		private void NodesHighlight(List<TreeNodeAdv> nodeList, bool highlight)
		{
			if (nodeList.Count < 1)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				for (int i=0; i<nodeList.Count; i++)
				{
					NodeHighlight(nodeList[i], highlight);
				}
			}
		}
		private void NodesClearFormatting(TreeNodeCollection nodez, bool clearHighlight, bool clearSelect)
		{
			bool selected = false;
			bool highlighted = false;

			foreach(TreeNodeAdv nOde in nodez)
			{
				//	Color back = nOde.CheckColor;
				//string bs = nOde.BaseStyle;
				Color backColor = nOde.Background.BackColor;
				if ((backColor == COLOR_BK_SelHigh) || (backColor==COLOR_BK_SelUnhigh))
				//if ((bs.CompareTo("SH") == 0) || (bs.CompareTo("Sh") == 0))
				//if (bs.Contains("S"))
				{
					if (!clearSelect)
					{
						selected = true;
					}
				}
				if ((backColor == COLOR_BK_SelHigh) || (backColor == COLOR_BK_UnselHigh))
				//if ((bs.CompareTo("SH") == 0) || (bs.CompareTo("sH") == 0))
				//if (bs.Contains("H")) 
					{
						if (!clearHighlight)
					{
						highlighted = true;
					}
				}

				if (selected && highlighted)
				{
					nOde.TextColor = COLOR_FR_SelHigh;
					//nOde.CheckColor = COLOR_BK_SelHigh;
					//nOde.BaseStyle = "SH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelHigh);
				}
				if (!selected && highlighted)
				{
					nOde.TextColor = COLOR_FR_UnselHigh;
					//nOde.CheckColor = COLOR_BK_UnselHigh;
					//nOde.BaseStyle = "sH";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselHigh);
				}
				if (selected && !highlighted)
				{
					nOde.TextColor = COLOR_FR_SelUnhigh;
					//nOde.CheckColor = COLOR_BK_SelUnhigh;
					//nOde.BaseStyle = "Sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelUnhigh);
				}
				if (!selected && !highlighted)
				{
					nOde.TextColor = COLOR_FR_UnselUnhigh;
					//nOde.CheckColor = COLOR_BK_UnselUnhigh;
					//nOde.BaseStyle = "sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselUnhigh);
				}
			}
		}
		private void NodesUnhighlightAllMaster()
		{
			bool wasSelected = false;
			foreach (TreeNodeAdv nOde in treeDest.Nodes)
			{
				//string bs = nOde.BaseStyle;
				Color backColor = nOde.Background.BackColor;
				if ((backColor == COLOR_BK_SelUnhigh) || (backColor == COLOR_BK_SelHigh))
				//if ((bs.CompareTo("Sh") == 0) || (bs.CompareTo("SH") == 0))
				//if (bs.Contains("S"))
				{
						wasSelected = true;
				}
				if (wasSelected)
				{
					nOde.TextColor = COLOR_FR_SelUnhigh;
					//nOde.CheckColor = COLOR_BK_SelUnhigh;
					//nOde.BaseStyle = "Sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_SelUnhigh);
				}
				else
				{
					nOde.TextColor = COLOR_FR_UnselUnhigh;
					//nOde.CheckColor = COLOR_BK_UnselUnhigh;
					//nOde.BaseStyle = "sh";
					nOde.Background = new Syncfusion.Drawing.BrushInfo(COLOR_BK_UnselUnhigh);
				}
			}
		}
		private void NodesSelect(List<TreeNodeAdv> nodeList, bool select)
		{
			if (nodeList.Count < 1)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				for (int i = 0; i < nodeList.Count; i++)
				{
					NodeSelect(nodeList[i], select);
				}
			}
		}
		private void HighlightMasterNodes(iLORMember4 master, bool highlight)
		{
			if(master != lastHighlightedDest)
			{
				if (lastHighlightedDest != null)
				{
					TreeUtils.HighlightMemberBackground(lastSelectedDest, false);
				}
				if (master != null)
				{
					TreeUtils.HighlightMemberBackground(master, true);
				}
				lastHighlightedDest = master;
			}
		}
		private void HighlightSourceNodes(iLORMember4 source, bool highlight)
		{
			if (source != lastHighlightedSource)
			{
				if (lastHighlightedSource != null)
				{
					TreeUtils.HighlightMemberBackground(lastHighlightedSource, false);
				}
				if (source != null)
				{
					TreeUtils.HighlightMemberBackground(source, true);
				}
				lastHighlightedSource = source;
			}
		}
		private void SelectMasterNodes(iLORMember4 master)
		{
			if (master != lastSelectedDest)
			{
				if (lastSelectedDest != null)
				{
					TreeUtils.SelectMember(lastSelectedDest, false);
				}
				if (master!= null)
				{
					TreeUtils.SelectMember(master, true);
				}
				lastSelectedDest = master;
			}
		}
		private void SelectSourceNodes(iLORMember4 source)
		{
			if (source != lastSelectedSource)
			{
				if (lastSelectedSource != null)
				{
					TreeUtils.SelectMember(lastSelectedSource, false);
				}
				if (source != null)
				{
					TreeUtils.SelectMember(source, true);
				}
				lastSelectedSource = source;
			}
		}

		/*
		 * private void NodesUnselectAll(TreeNodeCollection nOdes)
		{
			foreach (TreeNodeAdv nOde in nOdes)
			{
				if (nOde.BackColor == COLOR_BK_BOTH)
				{
					nOde.BackColor = COLOR_BK_MATCHED;
				}
				else
				{
					nOde.BackColor = COLOR_BK_NONE;
					nOde.ForeColor = COLOR_FR_NONE;
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					NodesUnselectAll(nOde.Nodes);
				}
			}
		}
		*/
		/*		private void NodesHighlight(TreeNodeCollection nOdes, int SavedIndex, bool highlight)
		{
			//string nTag = "";
			iLORMember4 nID = null;
			foreach (TreeNodeAdv nOde in nOdes)
			{
				nID = (iLORMember4)nOde.Tag;
				if (nID.SavedIndex == SavedIndex)
				{
					NodeHighlight(nOde, highlight);
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					NodesHighlight(nOde.Nodes, SavedIndex, highlight);
				}
			}
		}
		*/
		/*
		 * private void NodesHighlight(List<TreeNodeAdv> nodeList, bool highlight)
		{
			if (nodeList.Count < 1)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				for (int i = 0; i < nodeList.Count; i++)
				{
					NodeHighlight(nodeList[i], highlight);
				}
			}
		}

		

		private void NodesBold(List<TreeNodeAdv> nodeList, bool emBolden)
		{
			//string nTag = "";


			foreach (TreeNodeAdv thenOde in nodeList)
			{
				if (emBolden)
				{
					thenOde.NodeFont = new Font(treeSource.Font, FontStyle.Bold);
				}
				else
				{
					thenOde.NodeFont = new Font(treeSource.Font, FontStyle.Regular);
				}
				thenOde.Checked = emBolden;
			}
		}
		*/

		private void NodesMap(bool foo, TreeNodeAdv theSourceNode, TreeNodeAdv theMasterNode, bool map)
		{
			// Is a node Selected on each side?
			if (theSourceNode != null)
			{
				iLORMember4 sid = (iLORMember4)selectedSourceNode.Tag;
				if (selectedDestNode != null)
				{
					iLORMember4 mid = (iLORMember4)selectedDestNode.Tag;
					int newMaps = MapMembers(mid.SavedIndex, sid.SavedIndex, map, true);
					UpdateMappedCount(newMaps);
				} // end destinationNodeannel Node isn't null
			} // end sourceNodeannel Node isn't null
		} // end btnMap_Click
		#endregion
		#region Member Mapping Operations
		bool IsMappable(LORMemberType4 sourceType, LORMemberType4 masterType)
		{
			bool enM = false;
			// Are they both regular Channels?
			if (sourceType == LORMemberType4.Channel)
			{
				if (masterType == LORMemberType4.Channel)
				{
					enM = true;
				}
				else
				{
					StatusMessage(MSG_MapRegularToRegular);
				}
			} // end old type is regular ch
				// Are they both RGB Channels?
			if (sourceType == LORMemberType4.RGBChannel)
			{
				if (masterType == LORMemberType4.RGBChannel)
				{
					enM = true;
				}
				else
				{
					StatusMessage(MSG_MapRGBtoRGB);
				}
			} // end old type is RGB
				// Are they both groups?
			if (sourceType == LORMemberType4.ChannelGroup)
			{
				if (masterType == LORMemberType4.ChannelGroup)
				{
					// are they similar enough?
					TreeNodeAdv selectedSourceNode = treeSource.SelectedNode;
					iLORMember4 selectedSourceMember = (iLORMember4)selectedSourceNode.Tag;
					int sourceSI = selectedSourceMember.SavedIndex;
					TreeNodeAdv selectedDestNode = treeDest.SelectedNode;

					iLORMember4 selectedDestMember = (iLORMember4)selectedDestNode.Tag;
					int masterSI = selectedDestMember.SavedIndex;
					enM = IsCompatible(sourceSI, masterSI);
					if (!enM)
					{
						StatusMessage(MSG_GroupMatch);
					}
				}
				else
				{
					StatusMessage(MSG_GroupToGroup);
				}
			} // end old type is group
			if (masterType == LORMemberType4.Track)
			{
				StatusMessage(MSG_Tracks);
			}
			if (sourceType == LORMemberType4.Track)
			{
				StatusMessage(MSG_Tracks);
			}
			return enM;
		} // end IsMappable
		private void SelectSourceMember(iLORMember4 sourceMember)
		{
			iLORMember4 newSourceMember = sourceMember;
			iLORMember4 lastSelectedSourceMember = selectedSourceMember;
			//string mapText = "";

			int masterSI = lutils.UNDEFINED;
			bool doSelect = false;
			List<TreeNodeAdv> nodeList = null;
			// Assume (for now) that neither mapping or unmapping is valid
			btnMap.Enabled = false;
			btnUnmap.Enabled = false;
			

			if (newSourceMember.MemberType == LORMemberType4.Track)
			{
				// Unselect any previous selections
				if (lastSelectedSourceMember != null)
				{
					int oldSSI = lastSelectedSourceMember.SavedIndex;
					// Is it different from before?
					if (oldSSI != newSourceMember.SavedIndex)
					{
						// Clear previous selection
						//nodeList = sourceNodesBySI[oldSSI];
						NodesSelect(nodeList, false);

						// Was the old one mapped to one or more destinations?
						if (mapSourceToDest[lastSelectedSourceMember.SavedIndex].Count > 0)
						{
							// Unhighlight All Previous Master Nodes
							foreach (iLORMember4 destMember in mapSourceToDest[lastSelectedSourceMember.SavedIndex])
							{
								//nodeList = masterNodesBySI[destMember.SavedIndex];
								NodesHighlight(nodeList, false);
							}
						} // End old source mapped to destination(s)
					} // End selection changed
				} // End there was a previous selection
				selectedSourceMember = null;
				//mapText = newSourceMember.Name + " is a track.\n\r";
				//mapText += "Tracks cannot be mapped (at this time)";
			}
			else // NOT a LORTrack4
			{
				// Was one already selected?
				if (lastSelectedSourceMember != null)
				{
					int oldSSI = lastSelectedSourceMember.SavedIndex;
					// Is it different from before?
					if (oldSSI != newSourceMember.SavedIndex)
					{
						// Clear previous selection
						//nodeList = sourceNodesBySI[oldSSI];
						nodeList = MemberNodeList(lastSelectedSourceMember);
						NodesSelect(nodeList, false);

						// Was the old one mapped to one or more masters?
						if (mapSourceToDest[lastSelectedSourceMember.SavedIndex].Count > 0)
						{
							// Unhighlight previously selected & mapped Sources
							//nodeList = sourceNodesBySI[oldSSI];
							nodeList = MemberNodeList(newSourceMember);
							NodesHighlight(nodeList, false);
							
							// Unhighlight All Previous Master Nodes
							foreach (iLORMember4 destMember in mapSourceToDest[lastSelectedSourceMember.SavedIndex])
							{
								//nodeList = masterNodesBySI[destMember.SavedIndex];
								nodeList = MemberNodeList(destMember);
								NodesHighlight(nodeList, false);
							}
						} // End old source mapped to destination(s)
					} // End selection changed
				} // End there was a previous selection
				// if no source node was previously selected, then obviously this selection is different (from nothing)
				// Is the new one mapped to one or more destinations?
				if (mapSourceToDest[newSourceMember.SavedIndex].Count > 0)
				{
					// highlight all Master Nodes already mapped to this Source node
					foreach (iLORMember4 destMember in mapSourceToDest[newSourceMember.SavedIndex])
					{
						//nodeList = masterNodesBySI[destMember.SavedIndex];
						nodeList = MemberNodeList(destMember);
						NodesHighlight(nodeList, true);
						// Build status text
						//mapText += destMember.Name + ", ";

						// Is a destination member selected, and does it happen to be one of the destinations mapped to this source?
						if (selectedDestMember != null)
						{
							if (destMember.SavedIndex == selectedDestMember.SavedIndex)
							{
								// Allow it to be unmapped
								btnUnmap.Enabled = true;
								string tipText = "Unmap " + sourceMember.Name + " from " + destMember.Name;
								ttip.SetToolTip(btnUnmap, tipText);
							}
						}
					}
							
					/*
					// Update status text and fix grammar
					mapText = mapText.Substring(0, mapText.Length - 2);
					int lastComma = mapText.LastIndexOf(',');
					if (lastComma > 1)
					{
						string mt = mapText.Substring(0, lastComma);
						mt += " and";
						mt += mapText.Substring(lastComma + 1);
						mapText = mt;
					}
					if (sourceOnRight)
					{
						if (mapSourceToDest[newSourceMember.SavedIndex].Count > 1)
						{
							mapText = mapText + " are mapped to " + newSourceMember.Name;
						}
						else
						{
							mapText = mapText + " is mapped to " + newSourceMember.Name;
						}
						
					}
					*/
					//else
					//{
					//	mapText = newSourceMember.Name + " is mapped to " + mapText;
					//}
				} // End new source is NOT mapped to one or more destinations
				//else
				//{
				//	mapText = newSourceMember.Name + " is unmapped";
				//} // End new source is, or isn't, already mapped to anything

				// If none of the destinations mapped to the source is selected
				// (and thus unmap is disabled)
				// Is there a destination selected, and is compatible with this source
				if (!btnUnmap.Enabled)
				{
					if (selectedDestMember != null)
					{
						if (selectedDestMember.MemberType == newSourceMember.MemberType)
						{
							btnMap.Enabled = true;
							string tipText = "Map " + newSourceMember.Name + " to " + selectedDestMember.Name;
							ttip.SetToolTip(btnMap, tipText);
						}
					}
				} // End A mapped destination is not selected

				// Finally, lets select all nodes for this new source member
				selectedSourceMember = newSourceMember;
				//nodeList = sourceNodesBySI[newSourceMember.SavedIndex];
				nodeList = MemberNodeList(newSourceMember);
				NodesSelect(nodeList, true);
			} // End new selected source is not a track

			// Menu should mirror button state
			//lblSourceMappedTo.Text = mapText;
			StatusSourceUpdate(newSourceMember);
			mnuMap.Enabled = btnMap.Enabled;
			mnuUnmap.Enabled = btnUnmap.Enabled;

		} // END SELECT SOURCE MEMBER
		private void MemberSelectMaster(iLORMember4 destMember)
		{
			iLORMember4 newDestMember = destMember;
			iLORMember4 lastSelectedDestMember = selectedDestMember;

			int sourceSI = lutils.UNDEFINED;
			bool doSelect = false;
			List<TreeNodeAdv> nodeList = null;
			btnMap.Enabled = false;
			btnUnmap.Enabled = false;

			if (newDestMember.MemberType == LORMemberType4.Track)
			{
				// Unselect any previous selections
				if (lastSelectedDestMember != null)
				{
					int oldMSI = lastSelectedDestMember.SavedIndex;
					// Is it different from before
					if (oldMSI != newDestMember.SavedIndex)
					{
						// Clear previous selection
						TreeUtils.SelectMember(lastSelectedDestMember, false);

						// Was a source member mapped to the old destination?
						if (mapDestToSource[oldMSI] != null)
						{
							TreeUtils.SelectMember(mapDestToSource[oldMSI], false);
						} // End a source was mapped to the old destination
					} // End selection changed
				} // End there was a previous selection
				selectedDestMember = null;
				//mapText = newDestMember.Name + " is a track.\n\r";
				//mapText += "Tracks cannot be mapped (at this time)";
			}
			else // NOT a LORTrack4
			{
				// was one already selected?
				if (lastSelectedDestMember != null)
				{
					int oldMSI = lastSelectedDestMember.SavedIndex;
					// Is it different from before
					if (oldMSI != newDestMember.SavedIndex)
					{
						// Clear previous selection
						TreeUtils.SelectMember(lastSelectedDestMember, false);

						// Was a source member mapped to the old destination?
						if (mapDestToSource[oldMSI] != null)
						{
							TreeUtils.HighlightMemberBackground(mapDestToSource[oldMSI], false);
							TreeUtils.HighlightMemberBackground(newDestMember, false);
						} // End a source was mapped to the old destination
					} // End selection changed
				} // End there was a previous selection
					// If no destination node was previously selected, then obviously this selection is different (from nothing)
					// Is a source member mapped to this new destination member?
				iLORMember4 newSource = mapDestToSource[newDestMember.SavedIndex];
				if (newSource != null)
				{
					TreeUtils.HighlightMemberBackground(newSource, true);

					// Build status text
					//if (sourceOnRight)
					//{
					//	mapText = newSource.Name + " is mapped to " + destMember.Name;
					//}
					//else
					//{
					//	mapText = destMember.Name + " is mapped to " + newSource.Name;
					//}

					// Is a source member selected, and does it happen to be the one mapped to this destination?
					if (selectedSourceMember != null)
					{
						if (selectedSourceMember.SavedIndex == newSource.SavedIndex)
						{
							// Allow it to be unmapped
							btnUnmap.Enabled = true;
							string tipText = "Unmap " + selectedSourceMember.Name + " from " + selectedDestMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
				} // End a source is mapped to the new destination
					//else
					//{
					//	mapText = newDestMember.Name + " is unmapped";
					//} // End new destination does or does not have something mapped to it

				// If the selected source is not the one mapped to this new destination
				// (and thus unmap is still disapbled)
				// Is there a source selected, and is it compatible to this destination?
				if (!btnUnmap.Enabled)
				{
					if (selectedSourceMember != null)
					{
						if (selectedSourceMember.MemberType == newDestMember.MemberType)
						{
							btnMap.Enabled = true;
							string tipText = "Map " + selectedSourceMember.Name + " to " + newDestMember.Name;
							ttip.SetToolTip(btnMap, tipText);
						}
					}
				}
				// Finally, lets select all nodes for this new destination member
				selectedDestMember = newDestMember;
				//nodeList = masterNodesBySI[newDestMember.SavedIndex];
				nodeList = MemberNodeList(newDestMember);
				NodesSelect(nodeList, true);
			} // End newly selected destination is not a track

			// Finally, lets select all the nodes for this new destination
			StatusMasterUpdate(newDestMember);
			// Menu state follows button state
			mnuMap.Enabled = btnMap.Enabled;
			mnuUnmap.Enabled = btnUnmap.Enabled;

		} // END SELECT MASTER MEMBER
		private int MapMembersBySI(int masterSI, int sourceSI, bool andMembers, bool map)
		{
			int mapCount = 0;
			// Are we mapping or unmapping?
			try
			{
				iLORMember4 destMember = seqDest.AllMembers[masterSI];
				iLORMember4 newSourceMember = seqSource.AllMembers[sourceSI];

				if (map)
				{
					mapCount = MapMembers(destMember, newSourceMember, andMembers);
				}
				else
				{
					mapCount = UnmapMembers(destMember, newSourceMember, andMembers);
				}

				bool e = false;
				btnMap.Enabled = !map;
				mnuMap.Enabled = !map;
				btnUnmap.Enabled = map;
				btnUnmap.Enabled = map;
				if (mapCount > 0) e = true;
				btnSummary.Enabled = e;
				mnuSummary.Enabled = e;
				btnSaveMap.Enabled = e;
				mnuSaveNewMap.Enabled = e;
				btnSaveNewSeq.Enabled = e;
				mnuSaveNewSequence.Enabled = e;
			}
			catch
			{
				// Ignore?  Do Nothing?
				System.Diagnostics.Debugger.Break();
			}
			return mapCount;
		}
		private int MapMembers_Old(iLORMember4 destMember, iLORMember4 sourceMember, bool andMembers)
		{
			int mapCount = 0;
			bool alreadyMapped = false;
			masterMapLevel++;
			// First, is it a track?  And do we want to map its children?
			// Note: Tracks themselves cannot be mapped, but we can try to map its children
			if (destMember.MemberType == LORMemberType4.Track)
			{
				if (andMembers)
				{
					// Cast member to tracks so we can get the child membership
					LORTrack4 sourceTrack = (LORTrack4)sourceMember;
					LORTrack4 masterTrack = (LORTrack4)destMember;
					// Is it even possible to map children, do they have the same number?
					if (sourceTrack.Members.Items.Count == masterTrack.Members.Items.Count)
					{
						for (int i = 0; i < sourceTrack.Members.Items.Count; i++)
						{
							iLORMember4 sourceItem = sourceTrack.Members.Items[i];
							iLORMember4 masterItem = masterTrack.Members.Items[i];
							// Are they the same type
							if (sourceItem.MemberType ==
									masterItem.MemberType)
							{
								//mapCount += MapMembers(masterItem, sourceItem, andMembers);
							}
						}
					}
				}
			}
			else
			{
				// Fetch the source and destination
				//iLORMember4 sourceMember = seqSource.AllMembers.BySavedIndex[theSourceSI];
				int masterSI = destMember.SavedIndex;
				int sourceSI = sourceMember.SavedIndex;

				// Is the destination channel already mapped to a different source?
				//   Note: A source can map to multiple destinations.
				//     A destination cannot map to more than one source.
				//       Therefore, mappings list is indexed by the Master SavedIndex

				// First, is this destination mapped to something, anything?
				if (mapDestToSource[masterSI] != null)
				{
					// Is this redundant?  Is this destination already mapped to this source?
					if (mapDestToSource[destSI].SavedIndex == sourceSI)
					{
						alreadyMapped = true;
					}
					else // NOT alreadyMapped
					{
						// Is this source channel mapped to more than one destination channel?
						if (mapSourceToDest[sourceSI].Count > 0)
						{
							// If so, find this destination in its list of mappings and remove it
							for (int i = 0; i < mapSourceToDest[sourceSI].Count; i++)
							{
								if (mapSourceToDest[sourceSI][i].SavedIndex == masterSI)
								{
									//mapSourceToDest[sourceSI].RemoveAt(i);
									//mapCount--;
									int newMaps = UnmapMembers(destMember, mapSourceToDest[sourceSI][i], true);
									mapCount += newMaps;
								}
							}
							// Was that the only channel the source was mapped to, and thus is now mapped to nothing?
							if (mapSourceToDest[sourceSI].Count == 0)
							{
								// Deselect it
								//? List<TreeNodeAdv> nodeList = MemberNodeList(sourceMember);
								//? OR List<TreeNodeAdv> nodeList = sourceNodesBySI[sourceSI];
								sourceMember.Selected = false;
								//NodesBold(nodeList, false);
								if (!bulkOp)
								{
									if (masterMapLevel < 2)
									{
										//NodesHighlight(nodeList, false);
									}
								}
							}
						}
					}
				}

				if (!alreadyMapped)
				{
					// Sanity Check: Are they the same type?
					if (sourceMember.MemberType == destMember.MemberType)
					{
						string msgOut = "Mapping " + destMember.MemberType.ToString() + " MEMBER " + sourceMember.Name + " to " + destMember.Name;
						Debug.WriteLine(msgOut);
						// Map this source to this destination in the mapping list
						mapDestToSource[masterSI] = sourceMember;
						//mappedSIs[masterSI] = sourceSI;
						//List<TreeNodeAdv> nodeList = masterNodesBySI[masterSI];
						destMember.Selected = true;
						//NodesBold(nodeList, true);
						if (!bulkOp)
						{
							if (masterMapLevel < 2)
							{
								//NodesHighlight(nodeList, true);
							}
						}
						// In addition to the map list, put the source SavedIndex in the destination channels AltSavedIndex
						destMember.AltID = sourceSI;
						destMember.MapTo = sourceMember;

						// Map this destination to this source in the other mapping list
						mapSourceToDest[sourceSI].Add(destMember);
						sourceMember.Selected = true;
						//nodeList = sourceNodesBySI[sourceSI];
						//NodesBold(nodeList, true);
						if (!bulkOp)
						{
							if (masterMapLevel < 2)
							{
								//NodesHighlight(nodeList, true);
							}
						}

						// Increment count(s)
						mapCount++;
						if (destMember.MemberType == LORMemberType4.Channel)
						{
							mappedChannelCount++;
						}

						// Do we need to map its children also?
						if (andMembers)
						{
							// Is it a group, and do we want its children mapped?
							if (destMember.MemberType == LORMemberType4.ChannelGroup)
							{
								// Cast Member to Group (so we can get its children membership)
								LORChannelGroup4 sourceGroup = (LORChannelGroup4)sourceMember;
								LORChannelGroup4 masterGroup = (LORChannelGroup4)destMember;
								// Is it even possible to map children, do they have the same number?
								if (sourceGroup.Members.Items.Count == masterGroup.Members.Items.Count)
								{
									msgOut = "Recurse GROUP " + sourceMember.Name + " to " + destMember.Name;
									Debug.WriteLine(msgOut);
									for (int i = 0; i < sourceGroup.Members.Items.Count; i++)
									{
										iLORMember4 sourceItem = sourceGroup.Members.Items[i];
										iLORMember4 masterItem = masterGroup.Members.Items[i];
										// Are they the same type
										if (masterItem.MemberType == sourceItem.MemberType)
										{
											int newMaps = MapMembers(masterItem, sourceItem, andMembers);
											mapCount += newMaps;
										}
									}
								}
							}
						}

						// Or, instead of a group or track, is it an RGB channel?
						if (destMember.MemberType == LORMemberType4.RGBChannel)
						{
							// You know the drill by now, cast to LORRGBChannel4 so we can get its 3 subchannels	
							LORRGBChannel4 sourceRGBchannel = (LORRGBChannel4)sourceMember;
							LORRGBChannel4 masterRGBchannel = (LORRGBChannel4)destMember;
							// Always map an RGBchannels subchannels (don't bother to check 'andMembers' flag)
							msgOut = "Recurse RGB CHANNEL " + sourceMember.Name + " to " + destMember.Name;
							Debug.WriteLine(msgOut);
							int newMaps = MapMembers(masterRGBchannel.redChannel, sourceRGBchannel.redChannel, false);
							mapCount += newMaps;
							newMaps = MapMembers(masterRGBchannel.grnChannel, sourceRGBchannel.grnChannel, false);
							mapCount += newMaps;
							newMaps = MapMembers(masterRGBchannel.bluChannel, sourceRGBchannel.bluChannel, false);
							mapCount += newMaps;
						}

						// Set button states
						btnMap.Enabled = false;
						string tipText = sourceMember.Name + " is now mapped to " + destMember.Name;
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = true;
						tipText = "Unmap " + sourceMember.Name + " from " + destMember.Name;
						ttip.SetToolTip(btnUnmap, tipText);
						btnUnmap.Enabled = true;
					} // end if map
				} // End if track, or not
			} // end NOT alreadyMapped

			StatusSourceUpdate(sourceMember);
			StatusMasterUpdate(destMember);
			masterMapLevel--;
			return mapCount;
		} // End Map Members
		private int UnmapMembers(iLORMember4 destMember, iLORMember4 sourceMember, bool andMembers)
		{
			int mapCount = 0;
			List<TreeNodeAdv> nodeList = null;

			// First, is it a track?  And do we want to map its children?
			// Note: Tracks themselves cannot be mapped, but we can try to map its children
			if (destMember.MemberType == LORMemberType4.Track)
			{
				// Cast the members to groups so we can get thier child memberships
				//LORTrack4 sourceTrack = (LORTrack4)sourceMember;
				LORTrack4 masterTrack = (LORTrack4)destMember;
				for (int i = 0; i < masterTrack.Members.Items.Count; i++)
				{
					//mapCount += UnmapMembers(masterTrack.AllMembers.Items[i].SavedIndex, masterTrack.AltID, true);
					int masterItemSI = masterTrack.Members.Items[i].SavedIndex;
					int newMaps = UnmapMembers(masterTrack.Members.Items[i], mapDestToSource[masterItemSI], true);
					mapCount += newMaps;
				}

			}
			else // Not a track
			{
				// Fetch the source and destination
				int sourceSI = sourceMember.SavedIndex;
				int masterSI = destMember.SavedIndex;

				// First sanity check, are these two channels actually already mapped?
				if (mapDestToSource[masterSI].SavedIndex == sourceSI)
				{
					// Next sanity check, Is this source channel mapped to any destinations (one or more)?
					if (mapSourceToDest[sourceSI].Count > 0)
					{
						// Find and remove this destination
						for (int i = 0; i < mapSourceToDest[sourceSI].Count; i++)
						{
							if (mapSourceToDest[sourceSI][i].SavedIndex == masterSI)
							{
								mapSourceToDest[sourceSI].RemoveAt(i);
								i = mapSourceToDest[sourceSI].Count; // Force loop to end
							}
						}
					}
					// Was that the only channel the source was mapped to, and thus is now mapped to nothing?
					if (mapSourceToDest[sourceSI].Count == 0)
					{
						// Deselect it
						nodeList = MemberNodeList(sourceMember);
						//nodeList = sourceNodesBySI[sourceSI];
						sourceMember.Selected = false;
						NodesBold(nodeList, false);
						NodesHighlight(nodeList, false);
					}
					destMember.AltID = lutils.UNDEFINED;
					//destMember.MapTo = null;
					mapDestToSource[masterSI] = null;
					//mappedSIs[masterSI] = lutils.UNDEFINED;
					nodeList = MemberNodeList(destMember);
					//nodeList = masterNodesBySI[masterSI];
					destMember.Selected = false;
					NodesBold(nodeList, false);
					NodesHighlight(nodeList, false);
					mapCount--;
					if (destMember.MemberType == LORMemberType4.Channel)
					{
						mappedChannelCount--;
					}
					btnMap.Enabled = true;
					string tipText = "Map " + sourceMember.Name + " to " + destMember.Name;
					ttip.SetToolTip(btnMap, tipText);
					mnuMap.Enabled = true;
					btnUnmap.Enabled = false;
					tipText = sourceMember.Name + " is no longer mapped to " + destMember.Name;
					ttip.SetToolTip(btnUnmap, tipText);
					mnuUnmap.Enabled = false;

					// ALWAYS UNGROUP ALL CHILD MEMBERS whether specified or not
					//if (andMembers)
					//{ 
					// Are they groups?
					if (destMember.MemberType == LORMemberType4.ChannelGroup)
					{
						// Cast the members to groups so we can get their child memberships
						//LORChannelGroup4 sourceGroup = (LORChannelGroup4)sourceMember;
						LORChannelGroup4 masterGroup = (LORChannelGroup4)destMember;
						for (int i = 0; i < masterGroup.Members.Items.Count; i++)
						{
							//Unmap all children and descendants	
							//mapCount += UnmapMembers(masterGroup.Members.Items[i].SavedIndex, masterGroup.AltID, true);
							mapCount += UnmapMembers(masterGroup.Members.Items[i], mapDestToSource[masterSI], true);
						}
					}

					// if not a group, is it an RGB channel?
					if (destMember.MemberType == LORMemberType4.RGBChannel)
					{
						//LORRGBChannel4 sourceRGBchannel = (LORRGBChannel4)seqSource.AllMembers.BySavedIndex[theSourceSI];
						LORRGBChannel4 masterRGBchannel = (LORRGBChannel4)destMember;
						LORRGBChannel4 sourceRGBchannel = (LORRGBChannel4)sourceMember;
						//mapCount += UnmapMembers(masterRGBchannel.redChannel.SavedIndex, sourceRGBchannel.redChannel.SavedIndex, true);
						//mapCount += UnmapMembers(masterRGBchannel.grnChannel.SavedIndex, sourceRGBchannel.grnChannel.SavedIndex, true);
						//mapCount += UnmapMembers(masterRGBchannel.bluChannel.SavedIndex, sourceRGBchannel.bluChannel.SavedIndex, true);
						int newMaps = UnmapMembers(masterRGBchannel.redChannel, sourceRGBchannel.redChannel, true);
						mapCount += newMaps;
						newMaps = UnmapMembers(masterRGBchannel.grnChannel, sourceRGBchannel.grnChannel, true);
						mapCount += newMaps;
						newMaps = UnmapMembers(masterRGBchannel.bluChannel, sourceRGBchannel.bluChannel, true);
						mapCount += newMaps;
					}
				}
			} // end map or unmap
				//mappedMemberCount += mapCount;
				//lblMapCount.Text = mappedMemberCount.ToString();
			StatusSourceUpdate(sourceMember);
			StatusMasterUpdate(destMember);
			return mapCount;
		}
		private void UnmapSelectedMembers()
		{
			string tipText = "";
			// The button should not have even been enabled if the 2 Channels are not alreaDY mapped ... but--
			// Verify it anyway!

			// Is a node Selected on each side?
			if (selectedSourceNode != null)
			{
				if (selectedDestNode != null)
				{
					// Types match?
					masterMapLevel = 0; // Reset
					sourceMapLevel = 0;
					if (selectedSourceMember.MemberType == LORMemberType4.Channel)
					{
						if (selectedDestMember.MemberType == LORMemberType4.Channel)
						{
							//MapMembers(masterSI, sourceSI, false, true);
							int newMaps = UnmapMembers(selectedDestMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = true;
							tipText = "Map " + selectedSourceMember.Name + " to " + selectedDestMember.Name;
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is no longer mapped to " + selectedDestMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_MapRegularToRegular;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are different types";
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are not mapped";
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == LORMemberType4.RGBChannel)
					{
						if (selectedDestMember.MemberType == LORMemberType4.RGBChannel)
						{
							//MapMembers(masterSI, sourceSI, false, true);
							int newMaps = UnmapMembers(selectedDestMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = true;
							tipText = "Map " + selectedSourceMember.Name + " to " + selectedDestMember.Name;
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is no longer mapped to " + selectedDestMember.Name;
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_MapRGBtoRGB;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are different types";
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are not mapped";
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == LORMemberType4.ChannelGroup)
					{
						if (selectedDestMember.MemberType == LORMemberType4.ChannelGroup)
						{
							//MapMembers(masterSI, sourceSI, false, true);
							int newMaps = UnmapMembers(selectedDestMember, selectedSourceMember, true);
							mappedMemberCount += newMaps;
							dirtyMap = true;
							btnSaveMap.Enabled = dirtyMap;
							btnMap.Enabled = false;
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are not mapped";
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							statMsg = Resources.MSG_GroupToGroup;
							StatusMessage(statMsg);
							System.Media.SystemSounds.Beep.Play();
							btnMap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are different types";
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are not mapped";
							ttip.SetToolTip(btnUnmap, tipText);
						}
					}
					if (selectedSourceMember.MemberType == LORMemberType4.Track)
					{
						statMsg = Resources.MSG_Tracks;
						StatusMessage(statMsg);
						System.Media.SystemSounds.Beep.Play();
						btnMap.Enabled = false;
						tipText = "Tracks cannot be mapped";
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = false;
						tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are not mapped";
						ttip.SetToolTip(btnUnmap, tipText);
					}
					if (selectedDestMember.MemberType == LORMemberType4.Track)
					{
						statMsg = Resources.MSG_Tracks;
						StatusMessage(statMsg);
						System.Media.SystemSounds.Beep.Play();
						btnMap.Enabled = false;
						tipText = "Tracks cannot be mapped";
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = false;
						tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are not mapped";
						ttip.SetToolTip(btnUnmap, tipText);
					}
					UpdateMappedCount(0);
				}
				else
				{
					System.Media.SystemSounds.Beep.Play();
					btnMap.Enabled = false;
					tipText = "No destination is selected";
					ttip.SetToolTip(btnMap, tipText);
					btnUnmap.Enabled = false;
					tipText = selectedSourceMember.Name + " is not mapped";
					ttip.SetToolTip(btnUnmap, tipText);
				}
			}
			else
			{
				System.Media.SystemSounds.Beep.Play();
				btnMap.Enabled = false;
				tipText = "No source is selected";
				ttip.SetToolTip(btnMap, tipText);
				btnUnmap.Enabled = false;
				tipText = "Selection is not mapped";
				ttip.SetToolTip(btnUnmap, tipText);
			}

			// Copy button enable state to menus
			mnuMap.Enabled = btnMap.Enabled;
			mnuUnmap.Enabled = btnUnmap.Enabled;
			mnuSaveNewMap.Enabled = btnSaveMap.Enabled;
		}
		private string MapLine(iLORMember4 member)
		{
			string ret = member.Name + lutils.DELIM1;
			ret += member.SavedIndex.ToString() + lutils.DELIM1;
			ret += LORSeqEnums4.MemberName(member.MemberType);
			return ret;
		}
		private int UpdateMappedCount(int change)
		{
			mappedMemberCount += change;
			lblMappedCount.Text = mappedMemberCount.ToString() + " / " + mappedChannelCount.ToString();
			if (mappedMemberCount > 0)
			{
				btnSummary.Enabled = true;
				btnSaveNewSeq.Enabled = true;
			}
			else
			{
				btnSummary.Enabled = false;
				btnSaveNewSeq.Enabled = false;
			}
			EnableMappingButtons();

			return mappedMemberCount;
		}
		private void EnableMappingButtons()
		{
			string tipText = "";
			// Is a destination selected?
			if (selectedDestMember == null)
			{
				// No destination selected, so can't map or unmap to nothing
				btnMap.Enabled = false;
				tipText = "No destination selected";
				ttip.SetToolTip(btnMap, tipText);
				btnUnmap.Enabled = false;
				ttip.SetToolTip(btnUnmap, tipText);
			}
			else
			{
				// a destination is selected, is a source selected?
				if (selectedSourceMember == null)
				{
					// No source is selected, so can't map or unmap anything from that destination
					btnMap.Enabled = false;
					tipText = "No source is selected";
					ttip.SetToolTip(btnMap, tipText);
					btnUnmap.Enabled = false;
					ttip.SetToolTip(btnUnmap, tipText);
				}
				else
				{
					// We have both a destination and a source selected
					// TODO: Allow tracks to be mapped
					if (selectedDestMember.MemberType == LORMemberType4.Track)
					{
						// If destination is a track, disallow mapping (for now, TODO)
						btnMap.Enabled = false;
						tipText = "Tracks cannot be mapped";
						ttip.SetToolTip(btnMap, tipText);
						btnUnmap.Enabled = false;
						tipText = selectedSourceMember.Name + " is not mapped to " + selectedDestMember.Name + "\r\n(And Tracks cannot be mapped)";
						ttip.SetToolTip(btnUnmap, tipText);
					}
					else
					{
						// Master is not track, what about the source?
						if (selectedDestMember.MemberType == LORMemberType4.Track)
						{
							// Source is a track, disallow mapping (for now, TODO)
							btnMap.Enabled = false;
							tipText = "Tracks cannot be mapped";
							ttip.SetToolTip(btnMap, tipText);
							btnUnmap.Enabled = false;
							tipText = selectedSourceMember.Name + " is not mapped to " + selectedDestMember.Name + "\r\n(And Tracks cannot be mapped)";
							ttip.SetToolTip(btnUnmap, tipText);
						}
						else
						{
							// Neither is a track
							// Get the selected destination's SavedIndex, and check to see if it's mapped
							int masterSI = selectedDestMember.SavedIndex;
							iLORMember4 mapTo = mapDestToSource[masterSI];
							if (mapTo == null)
							{
								// Master isn't mapped to ANYTHING, much less this source, so we certainly can't unmap it
								btnUnmap.Enabled = false;
								tipText = selectedDestMember.Name + " is not mapped to a source";
								ttip.SetToolTip(btnUnmap, tipText);
								// So is the selected source the same type as the selected destination?
								if (selectedDestMember.MemberType == selectedSourceMember.MemberType)
								{
									// Types match, so possible to map them
									btnMap.Enabled = true;
									tipText = "Map " + selectedSourceMember.Name + " to " + selectedDestMember.Name;
									ttip.SetToolTip(btnMap, tipText);
								}
								else
								{
									// Types do NOT match, so can't map them
									btnMap.Enabled = false;
									tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are different types";
									ttip.SetToolTip(btnMap, tipText);
								} // End Types Match (or not)
							}
							else
							{
								// Master is mapped, but to what?
								int mappedSI = mapTo.SavedIndex;
								// Is it mapped to the selected source?
								if (mappedSI == selectedSourceMember.SavedIndex)
								{
									// So the selected destination and source ARE mapped, so allow them to be unmapped
									// but not to (redundantly) mapped to each other again
									btnMap.Enabled = false;
									tipText = selectedSourceMember.Name + " is already mapped to " + selectedDestMember.Name;
									ttip.SetToolTip(btnMap, tipText);
									btnUnmap.Enabled = true;
									tipText = "Unmap " + selectedSourceMember.Name + " from " + selectedDestMember.Name;
									ttip.SetToolTip(btnUnmap, tipText);
								}
								else
								{
									// The selected destination is mapped, but NOT to the selected source
									btnUnmap.Enabled = false;
									tipText = selectedSourceMember.Name + " is not mapped to " + selectedDestMember.Name;
									ttip.SetToolTip(btnUnmap, tipText);
									// That's OK though, since we could remap the destination to something different
									// So is the selected source the same type as the selected destination?
									if (selectedDestMember.MemberType == selectedSourceMember.MemberType)
									{
										// Types match, so possible to map them
										btnMap.Enabled = true;
										tipText = "Map " + selectedSourceMember.Name + " to " + selectedDestMember.Name;
										ttip.SetToolTip(btnMap, tipText);
									}
									else
									{
										// Types do NOT match, so can't map them
										btnMap.Enabled = false;
										tipText = selectedSourceMember.Name + " and " + selectedDestMember.Name + " are different types";
										ttip.SetToolTip(btnMap, tipText);
									} // End Types Match (or not)
								} // End Already Mapped (or not)
							} // End Master is already mappee
						} // End Not LORTrack4
					} // End Not LORTrack4
				} // End Source Set
			} // End Master Set
		} // End EnableMappingButtons
		#endregion

		#region Status Operations
		private void StatusSourceUpdate(iLORMember4 sourceMember)
		{
			string mapText = "";
			if (sourceMember.MemberType == LORMemberType4.Track)
			{
				mapText = sourceMember.Name + " is a track.\n\r";
				mapText += "Tracks cannot be mapped (at this time)";
			}
			else
			{
				// Is the new one mapped to one or more destinations?
				if (mapSourceToDest[sourceMember.SavedIndex].Count > 0)
				{
					// highlight all Master Nodes already mapped to this Source node
					foreach (iLORMember4 destMember in mapSourceToDest[sourceMember.SavedIndex])
					{
						// Build status text
						mapText += destMember.Name + ", ";
					}

					// Update status text and fix grammar
					mapText = mapText.Substring(0, mapText.Length - 2);
					int lastComma = mapText.LastIndexOf(',');
					if (lastComma > 1)
					{
						string mt = mapText.Substring(0, lastComma);
						mt += " and";
						mt += mapText.Substring(lastComma + 1);
						mapText = mt;
					}
					if (sourceOnRight)
					{
						if (mapSourceToDest[sourceMember.SavedIndex].Count > 1)
						{
							mapText = mapText + " are mapped to " + sourceMember.Name;
						}
						else
						{
							mapText = mapText + " is mapped to " + sourceMember.Name;
						}
					}
					else
					{
						mapText = sourceMember.Name + " is mapped to " + mapText;
					}
				} // End new source is NOT mapped to one or more destinations
				else
				{
					mapText = sourceMember.Name + " is unmapped";
				} // End new source is, or isn't, already mapped to anything
			} // End LORTrack4, or not
			lblSourceMappedTo.Text = mapText;
		}

		private void StatusMasterUpdate(iLORMember4 destMember)
		{
			string mapText = "";
			if (destMember.MemberType == LORMemberType4.Track)
			{
				mapText = destMember.Name + " is a track.\n\r";
				mapText += "Tracks cannot be mapped (at this time)";
			}
			else
			{
				iLORMember4 mappedSource = mapDestToSource[destMember.SavedIndex];
				if (mappedSource == null)
				{
					mapText = destMember.Name + " is unmapped";
				}
				else
				{
					// Build status text
					if (sourceOnRight)
					{
						mapText = mappedSource.Name + " is mapped to " + destMember.Name;
					}
					else
					{
						mapText = destMember.Name + " is mapped to " + mappedSource.Name;
					}
				}
			}
			lblDestMappedTo.Text = mapText;
		}

		private void LogMessage(string message)
		{
			//TODO: This
		}



		#endregion


		/*		private int NodeHighlight(TreeView tree, string tag)
		{
			int tl = tag.Length;
			int found = 0;
			foreach (TreeNodeAdv nOde in tree.Nodes)
			{
				// Reset any previous selections
				//NodeHighlight(nOde, false);

				// If node is mapped, it's tag will include the node it is mapped to, and thus the tag length
				// will be longer than just it's own tag info
				int iPos = nOde.Tag.ToString().IndexOf(DELIM_Map);
				if (iPos > 0)
				{
					string mappedTag = nOde.Tag.ToString().Substring(iPos + 1);
					if (mappedTag.CompareTo(oldTag) == 0)
					{
						// The old channel is mapped to this one
						nOde.EnsureVisible();
						NodeHighlight(nOde, true);
						if (found == 0)
						{
							treeMaster.SelectedNode = nOde;
						}
						found++;

					}
				} // end long tag
			} // end foreach
			return found;
		} // end HighlightNewNodes
		*/
		/* private int HighlightSourceNode(string tag)
		{
			int tl = tag.Length;
			int found = 0;
			foreach (TreeNodeAdv nOde in treeSource.Nodes)
			{
				// Reset any previous selections
				NodeHighlight(nOde, false);

				// If node is mapped, it's tag will include the node it is mapped to, and thus the tag length
				// will be longer than just it's own tag info
				if (nOde.Tag.ToString().Length > tl)
				{
					string myTag = nOde.Tag.ToString().Substring(0, tl);
					if (tag.CompareTo(myTag) == 0)
					{
						// The old channel is mapped to this one
						nOde.EnsureVisible();
						NodeHighlight(nOde, true);
						treeSource.SelectedNode = nOde;
						found++;

					}
				} // end long tag
			} // end foreach
			return found;
		} // end HighlightNewNodes
		*/

		/*		private void X_UnhighlightAllNodes(TreeNodeCollection nOdes)
		{
			foreach (TreeNodeAdv nOde in nOdes)
			{
				if (nOde.BackColor == COLOR_BK_BOTH)
				{
					nOde.BackColor = COLOR_BK_SELECTED;
				}
				else
				{
					nOde.BackColor = COLOR_BK_NONE;
					nOde.ForeColor = COLOR_FR_NONE;
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					X_UnhighlightAllNodes(nOde.Nodes);
				}
			}
		}
		*/

		private int addElement(ref int[] numbers)
		{
			int newIdx = lutils.UNDEFINED;

			if (numbers == null)
			{
				Array.Resize(ref numbers, 1);
				newIdx = 0;
			}
			else
			{
				int l = numbers.Length;
				Array.Resize(ref numbers, l + 1);
				newIdx = l;
			}

			return newIdx;
		}
		private int removeElement(ref int[] numbers, int index)
		{
			int l = numbers.Length;
			if (index < l)
			{
				l--;
				if (index < (l))
				{
					for (int i = index; i < l; i++)
					{
						numbers[i] = numbers[i + 1];
					}
				}
			}
			if (l == 0)
			{
				numbers = null;
			}
			else
			{
				Array.Resize(ref numbers, l);
			}

			return l;
		}
		private int FindElement(ref int[] numbers, int SID)
		{
			int found = lutils.UNDEFINED;
			for (int i = 0; i < numbers.Length; i++)
			{
				if (numbers[i] == SID)
				{
					found = i;
					break;
				}
			}
			return found;
		}

		/*private void NodesHighlight(bool destination, int SID)
		{
			if (master)
			{
				foreach (TreeNodeAdv nOde in masterNodesBySI[SID])
				{
					NodeHighlight(nOde, true);
				}
			}
			else
			{
				foreach (TreeNodeAdv nOde in sourceNodesBySI[SID])
				{
					NodeHighlight(nOde, true);
				}
			}
		}
		*/

		private int[] FindChannelNames(LORSequence4 seq, string theName, LORMemberType4 type)
		{
			// Not just regular Channels, but RGB Channels and Channel Groups too
			// Returns an array of Saved Indexes
			// If one or more exact matches are found, it returns a list of those, and no fuzzy find is performed
			// If no exact matches are found, then perform a fuzzy find and return closest match
			int[] ret = null;
			match[] matches = null;
			int matchCount = 0;

			// Pass 1, look for exact matches
			if (type == LORMemberType4.ChannelGroup)
			{
				foreach (LORChannelGroup4 grp in seq.ChannelGroups)
				{
					if (theName.CompareTo(grp.Name) == 0)
					{
						Array.Resize(ref matches, matchCount + 1);
						matches[matchCount].savedIdx = grp.SavedIndex;
						matches[matchCount].MemberType = LORMemberType4.ChannelGroup;
						matches[matchCount].itemIdx = grp.Index;
						matches[matchCount].score = 1;
						matchCount++;
					} // match
				} // loop thru Channel Groups
			} // end if type is Channel Group

			if (type == LORMemberType4.RGBChannel)
			{
				foreach (LORRGBChannel4 rgb in seq.RGBchannels)
				{
					if (theName.CompareTo(rgb.Name) == 0)
					{
						Array.Resize(ref matches, matchCount + 1);
						matches[matchCount].savedIdx = rgb.SavedIndex;
						matches[matchCount].MemberType = LORMemberType4.RGBChannel;
						matches[matchCount].itemIdx = rgb.Index;
						matches[matchCount].score = 1;
						matchCount++;
					} // match
				} // loop thru RGB Channels
			} // end if type is RGB LORChannel4

			if (type == LORMemberType4.Channel)
			{
				foreach (LORChannel4 ch in seq.Channels)
				{
					if (theName.CompareTo(ch.Name) == 0)
					{
						Array.Resize(ref matches, matchCount + 1);
						matches[matchCount].savedIdx = ch.SavedIndex;
						matches[matchCount].MemberType = LORMemberType4.Channel;
						matches[matchCount].itemIdx = ch.Index;
						matches[matchCount].score = 1;
						matchCount++;
					} // match
				} // End first pass channel loop
			} // type is regular channel

			// Found any exact matches?
			if (matchCount >0)
			{
				Array.Resize(ref ret, matchCount);
				for (int m = 0; m< matchCount; m++)
				{
					ret[m] = matches[m].savedIdx;
				}
			}
			else
			{
				// No exact match(es) found, perform a fuzzy find
				// Pass 2, for the sake of speed, prequalify matches with a quick simple fuzzy algorithm
				// then run full algorithms later only on those that prequalify
				double score = 0;
				if (type == LORMemberType4.ChannelGroup)
				{
					foreach(LORChannelGroup4 grp in seq.ChannelGroups)
					{
						score = theName.LevenshteinDistance(grp.Name);
						if (score > MINSCORE)
						{
							Array.Resize(ref matches, matchCount + 1);
							matches[matchCount].savedIdx = grp.SavedIndex;
							matches[matchCount].MemberType = LORMemberType4.ChannelGroup;
							matches[matchCount].itemIdx = grp.Index;
							matches[matchCount].score = score;
							matchCount++;
						}
					}
				} // end if type is Channel Group

				if (type == LORMemberType4.RGBChannel)
				{
					foreach (LORChannel4 ch in seq.Channels)
					{
						score = theName.LevenshteinDistance(ch.Name);
						if (score > MINSCORE)
						{
							Array.Resize(ref matches, matchCount + 1);
							matches[matchCount].savedIdx = ch.SavedIndex;
							matches[matchCount].MemberType = LORMemberType4.Channel;
							matches[matchCount].itemIdx = ch.Index;
							matches[matchCount].score = score;
							matchCount++;
						}
					}
				} // end if type is RGB LORChannel4

				if (type == LORMemberType4.Channel)
				{
					foreach (LORRGBChannel4 rgb in seq.RGBchannels)
					{
						score = theName.LevenshteinDistance(rgb.Name);
						if (score > MINSCORE)
						{
							Array.Resize(ref matches, matchCount + 1);
							matches[matchCount].savedIdx = rgb.SavedIndex;
							matches[matchCount].MemberType = LORMemberType4.RGBChannel;
							matches[matchCount].itemIdx = rgb.Index;
							matches[matchCount].score = score;
							matchCount++;
						}
					}
				} // end if type is regular channel



				// Did we get ANY prequalified fuzzy matches?
				if (matchCount > 0)
				{
					for (int q = 0; q < matchCount; q++)
					{
						string myName = "";
						if (matches[q].MemberType == LORMemberType4.Channel)
						{
							myName = seq.Channels[matches[q].itemIdx].Name;
						}
						if (matches[q].MemberType == LORMemberType4.RGBChannel)
						{
							myName = seq.RGBchannels[matches[q].itemIdx].Name;
						}
						if (matches[q].MemberType == LORMemberType4.ChannelGroup)
						{
							myName = seq.ChannelGroups[matches[q].itemIdx].Name;
						}
						// update the score using a FULL fuzzy match
						matches[q].score = theName.RankEquality(myName);
					}

					SortByScore(matches);
					// Is the first one past the minimum
					// NOTE: Minimum score for prequalification, and minimum score for final matching are NOT the same
					if (matches[0].score >= MINMATCH)
					{ 
						for (int q = 0; q < matchCount; q++)
						{
							if (matches[q].score >= MINMATCH)
							{
								Array.Resize(ref ret, q);
								ret[q] = matches[q].savedIdx;
							}
						} // end loop
					} // end any past minimum score
				} // end any prequalified fuzzies
			} // end single exact match
			return ret;
		} // end FindChannelNames
		/*		private void SortMatches(match[] matches, int low, int high)
		{
			int pivot_loc = 0;

			if (low < high)
			{
				pivot_loc = PartitionMatches(matches, low, high);
			}
			SortMatches(matches, low, pivot_loc - 1);
			SortMatches(matches, pivot_loc + 1, high);
		}

		private int PartitionMatches(match[] matches, int low, int high)
		{
			double pivot = matches[high].score;
			int i = low - 1;

			for (int j = low; j < high - 1; j++)
			{
				if (matches[j].score <= pivot)
				{
					i++;
					SwapMatches(matches, i, j);
				}
			}
			SwapMatches(matches, i + 1, high);
			return i + 1;
		}

		private void SwapMatches(match[] matches, int a, int b)
		{
			match temp = matches[a];
			matches[a] = matches[b];
			matches[b] = temp;
		}
		*/

		private string SuggestedNewName(string originalName)
		{
			string p = Path.GetDirectoryName(originalName);
			string n = Path.GetFileNameWithoutExtension(originalName);
			string e = Path.GetExtension(originalName);
			string ty = DateTime.Now.Year.ToString();
			string ly = (DateTime.Now.Year - 1).ToString();
			n = n.Replace(ly, ty);
			ly = (DateTime.Now.Year - 2).ToString();
			n = n.Replace(ly, ty);
			int i = 1;
			string ret = p + n + " Remapped" + e;
			while(File.Exists(ret))
			{
				i++;
				ret = p + n + " Remapped (" + i.ToString() + ")" + e;
			}
			return ret;
		} // end SuggestedNewName
		private int GetMapFileLineCount(string mapFile)
		{
			int lineCount = 0;
			string lineIn = "";
			StreamReader reader = new StreamReader(mapFile);
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
			}
			reader.Close();
			return lineCount;
		}
		private void ProcessSourcesBatch(string[] batchFilenames)
		{
			//if (batch_fileCount > 0)
			if (seqDest.AllMembers.Items.Count > 1)
			{
				ShowProgressBars(2);
				for (int f = 0; f < batch_fileCount; f++)
				{
					LoadSourceFile(batch_fileList[f]);
					if (File.Exists(mapFile))
					{
						txtMappingFile.Text = Path.GetFileNameWithoutExtension(mapFile);
						batch_fileNumber = f;
						LoadApplyMapFile(mapFile);
						string oldnm = Path.GetFileNameWithoutExtension(sourceFile);
						string newnm = SuggestedNewName(oldnm); // R we gettin here?
						string newfl = Path.GetDirectoryName(saveFile) + "\\";
						newfl += newnm;
						newfl += Path.GetExtension(sourceFile).ToLower();
						SaveNewMappedSequence(newfl);
					}
					else
					{
						f = batch_fileCount;
						break;
					}
				} // cmdSeqFileCount-- Batch Mode or Not
			}
			batchMode = false;
			ShowProgressBars(0);
			ImBusy(false);
			//string msg = "Batch mode is not supported... yet.\r\nLook for this feature in a future release (soon)!";
			//MessageBox.Show(this, msg, "Batch Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
		} // end ProcessSourcesBatch
		public static iLORMember4 FindName(string theName, List<iLORMember4> IDs)
		{
			iLORMember4 ret = null;
			int idx = BinarySearch(theName, IDs);
			if (idx > lutils.UNDEFINED)
			{
				ret = IDs[idx];
			}
			return ret;
		}
		public static int BinarySearch(string theName, List<iLORMember4> IDs)
		{
			return BinarySearch3(theName, IDs, 0, IDs.Count - 1);
		}
		public static int BinarySearch3(string theName, List<iLORMember4> IDs, int start, int end)
		{
			int index = -1;
			int mid = (start + end) / 2;
			if ((theName.CompareTo(IDs[start].Name) > 0) && (theName.CompareTo(IDs[end].Name) < 0))
			{
				int cmp = theName.CompareTo(IDs[mid].Name);
				if (cmp < 0)
					BinarySearch3(theName, IDs, start, mid);
				else if (cmp > 0)
					BinarySearch3(theName, IDs, mid + 1, end);
				else if (cmp == 0)
					index = mid;
				//if (index != -1)
					//Console.WriteLine("got it at " + index);
			}
			return index;
		}
		public iLORMember4 FindByName(string theName, LORMembership4 children, LORMemberType4 PartType)
		{
			iLORMember4 ret = null;
			ret = FindByName(theName, children, PartType, 0, 0, 0, 0, false);
			return ret;
		}
		public static iLORMember4 FindByName(string theName, LORMembership4 children)
		{
			iLORMember4 ret = null;
			int idx = BinarySearch(theName, children.Items);
			if (idx > lutils.UNDEFINED)
			{
				ret = children.Items[idx];
			}
			return ret;
		}
		private int LeftBushesChildCount()
		{
			int ret = 0;
			for (int g = 0; g < seqDest.ChannelGroups.Count; g++)
			{
				int p = seqDest.ChannelGroups[g].Name.IndexOf("eft Bushes");
				if (p > 0)
				{
					ret = seqDest.ChannelGroups[g].Members.Count;
					g = seqDest.ChannelGroups.Count; // Force exit of for loop
				}
			}
			return ret;

		}
		private List<TreeNodeAdv> MemberNodeList(iLORMember4 member)
		{
			return (List<TreeNodeAdv>)member.Nodes;
		}




	} // end partial class frmRemapper
} // end namespace UtilORama4
