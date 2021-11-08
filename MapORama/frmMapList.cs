using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LORUtils4; using FileHelper;
using System.Diagnostics;

namespace UtilORama4
{
	public partial class frmMapList : Form
	{
		public bool sortBySource = true;		// Otherwise sort by New LORSequence4
		public bool sortAlpha = false;  // Otherwise sort by Display Order
		private int lastCol = 0;
		private int level = 0;
		public LORSequence4 seqSource;
		public LORSequence4 seqDest;
		//public iLORMember4[] mapMastToSrc = null;  // Array indexed by destination.SavedIndex, elements contain Source.SaveIndex
		//public List<iLORMember4>[] mapSrcToMast = null; // Array indexed by Source.SavedIindex, elements are Lists of destination.SavedIndex-es

		private const char DELIM_Map = (char)164;  // ¤
		private const string NOTMAPPED = "\t(Not Mapped)";
		private const int ONEMEELION = 1000000;
		private MapInfo[] sortedMap;
		private int sortedMapCount = 0;
		private bool firstShown = false;
		private bool sourceOnRight = false;


		private const string MAPSUM = "LORChannel4 Mapping Summary - ";


		public frmMapList(LORSequence4 sourceSequence, LORSequence4 destSequence, ImageList icons)
		{
			InitializeComponent();

			seqSource = sourceSequence;
			seqDest = destSequence;
			//mapMastToSrc = mastToSrcMap;
			//mapSrcToMast = srcToMastMap;
			imlTreeIcons = icons;
			lstMap.SmallImageList = icons;

			//seqSource.Members.ReIndex();
			//seqDest.Members.ReIndex();


			//BuildList();
		}



		public frmMapList()
		{
			InitializeComponent();
	
		}

		private void frmMapList_Load(object sender, EventArgs e)
		{
			lstMap.Columns[0].TextAlign = HorizontalAlignment.Right;
			sourceOnRight = Properties.Settings.Default.SourceOnRight;
			ArrangePanes(sourceOnRight);
		}

		
		
		public void BuildList()
		{
			ImBusy(true);
			lstMap.Items.Clear();


			if (sortBySource)
			{
				lblKey.Text = "by Source";
				if (sortAlpha)
				{
					lblAlpha.Text = "by Alpha";
					this.Text = MAPSUM + "by Source in Alphabetical Order";
					FillBySourceAlpha();
				}
				else // NOT sort Alpha
				{
					lblAlpha.Text = "by DispOrder";
					this.Text = MAPSUM + "by Source in Display Order";
					FillBySourceDispOrder();
				} // end Sort by Alpha
			}
			else // Sort by Destination
			{
				lblKey.Text = "by Destination";
				if (sortAlpha)
				{
					lblAlpha.Text = "by Alpha";
					this.Text = MAPSUM + "By Destination in Alphabetical order";
					FillByDestAlpha();
				}
				else // NOT sort Alpha
				{
					lblAlpha.Text = "by DispOrder";
					this.Text = MAPSUM + "by Destination in Display Order";
					FillByDestDispOrder();
				} // end Sort by Alpha
			}
			ImBusy(false);
		} // end BuildList

		public void FillBySourceAlpha()
		{

			//lutils.ClearOutputWindow();
			//Console.WriteLine();
			//Console.WriteLine("***** FILL BY SOURCE ALPHA ******");
			//Console.WriteLine();
			//Console.WriteLine();

			//for (int i = 0; i < seqSource.Members.ByName.Count; i++)
			foreach(iLORMember4 sourceMember in seqSource.AllMembers.ByName.Values)
			{
				//iLORMember4 sourceMember = seqSource.Members.ByName[i];
				LORRGBChild4 rchild = LORRGBChild4.None;
				LORMemberType4 tt = sourceMember.MemberType;

				if (tt == LORMemberType4.Channel)
				{
					LORChannel4 ch = (LORChannel4)seqSource.AllMembers.BySavedIndex[sourceMember.SavedIndex];
					rchild = ch.rgbChild;
				}
					//string n = sourceMember.Name;
					//Console.Write(n + "\t");
					//Console.Write(sourceMember.SavedIndex);
					//Console.Write("\t");

					//iLORMember4 s2 = seqSource.Members.BySavedIndex[sourceMember.SavedIndex];
					//string n2 = s2.Name;
					//Console.Write(n2 + "\t");
					//Console.Write(s2.SavedIndex);
					//Console.Write("\t");

				if (rchild == LORRGBChild4.None)
				{
					if (sourceMember.Tag != null)
					{
						List<iLORMember4> mapsTos = (List<iLORMember4>)sourceMember.Tag;
						if (mapsTos.Count == 0)
						{
							if (chkUnmapped.Checked)
							{
								AddMapping(null, sourceMember, 0);
							}
						}
						else
						{
							for (int j = 0; j < mapsTos.Count; j++)
							{
								iLORMember4 destMember = mapsTos[j];
								AddMapping(destMember, sourceMember, 0);
							} // end loop thru mappings
						} // end mapped or not
					}
				} // end not rgb child
			} // end loop thru source by AlphaAllNames

			if (chkUnmapped.Checked)
			{
				//for (int i = 0; i < seqDest.Members.ByName.Count; i++)
				foreach (iLORMember4 destMember in seqDest.AllMembers.ByName.Values)
				{
					//int mSI = seqDest.Members.ByName[i].SavedIndex;
					if (destMember.MapTo == null)
					{
						LORRGBChild4 rchild = LORRGBChild4.None;
						if (destMember.MemberType == LORMemberType4.Channel)
						{
							LORChannel4 ch = (LORChannel4)destMember;
							rchild = ch.rgbChild;
						}
						if (rchild == LORRGBChild4.None)
						{
							AddMapping(destMember, null, 0);
						}
					} // end if not mapped
				} // end loop thru destination by AlphaAllNames
			} // end display unmapped
		} // end FillBySourceAlpha

		public void FillBySourceDispOrder()
		{
			level = 0; // Reset
			// Add all mapped channels, dig thru source tree
			for (int t = 0; t < seqSource.Tracks.Count; t++)
			{
				// Add mapped channels
				BuildListSourceDisplayOrder(seqSource.Tracks[t],true);
			}
			// Also display Un-Mapped channels?
			if (chkUnmapped.Checked)
			{
				level = 0;
				for (int t = 0; t < seqSource.Tracks.Count; t++)
				{
					// Add Un-Mapped Source channels after the mapped ones
					BuildListSourceDisplayOrder(seqSource.Tracks[t], false);
				}
				level = 0;
				for (int t = 0; t < seqDest.Tracks.Count; t++)
				{
					// Add Un-Mapped destination channels last
					BuildListDestDisplayOrder(seqDest.Tracks[t], false);
				}
			}
		}

		public void BuildListSourceDisplayOrder(iLORMember4 sourceMember, bool selected)
		{
			if (sourceMember.Selected = selected)
			{ 
				if (!selected)
				{
					AddMapping(sourceMember, null, level);
				}
				if (sourceMember.Tag != null)
				{
					List<iLORMember4> mapsTos = (List<iLORMember4>)sourceMember.Tag;
					for (int m = 0; m < mapsTos.Count; m++)
					{
						iLORMember4 destMember = mapsTos[m];
						AddMapping(sourceMember, destMember, level);
					}
				}
				LORMembership4 members = null;
				switch (sourceMember.MemberType)
				{
					case LORMemberType4.Channel:
						// Don't need to do anything additional
						break;
					case LORMemberType4.RGBChannel:
						level++;
						LORRGBChannel4 rgb = (LORRGBChannel4)sourceMember;
						BuildListSourceDisplayOrder(rgb.redChannel, selected);
						BuildListSourceDisplayOrder(rgb.grnChannel, selected);
						BuildListSourceDisplayOrder(rgb.bluChannel, selected);
						level--;
						break;
					case LORMemberType4.ChannelGroup:
						LORChannelGroup4 group = (LORChannelGroup4)sourceMember;
						members = group.Members;
						break;
					case LORMemberType4.Track:
						LORTrack4 track = (LORTrack4)sourceMember;
						members = track.Members;
						break;
					case LORMemberType4.Cosmic:
						LORCosmic4 cosmic = (LORCosmic4)sourceMember;
						members = cosmic.Members;
						break;
				}
				if (members != null)
				{
					level++;
					for (int n=0; n< members.Count; n++)
					{
						BuildListSourceDisplayOrder(members[n], selected);
					}
					level--;
				}
			}
		}

		public void BuildListDestDisplayOrder(iLORMember4 destMember, bool selected)
		{
			if (destMember.Selected = selected)
			{
				if (!selected)
				{
					AddMapping(null, destMember, level);
				}
				if (destMember.MapTo != null)
				{
					iLORMember4 sourceMember = destMember.MapTo;
					AddMapping(sourceMember, destMember, level);
				}
				LORMembership4 members = null;
				switch (destMember.MemberType)
				{
					case LORMemberType4.Channel:
						// Don't need to do anything additional
						break;
					case LORMemberType4.RGBChannel:
						level++;
						LORRGBChannel4 rgb = (LORRGBChannel4)destMember;
						BuildListDestDisplayOrder(rgb.redChannel, selected);
						BuildListDestDisplayOrder(rgb.grnChannel, selected);
						BuildListDestDisplayOrder(rgb.bluChannel, selected);
						level--;
						break;
					case LORMemberType4.ChannelGroup:
						LORChannelGroup4 group = (LORChannelGroup4)destMember;
						members = group.Members;
						break;
					case LORMemberType4.Track:
						LORTrack4 track = (LORTrack4)destMember;
						members = track.Members;
						break;
					case LORMemberType4.Cosmic:
						LORCosmic4 cosmic = (LORCosmic4)destMember;
						members = cosmic.Members;
						break;
				}
				if (members != null)
				{
					level++;
					for (int n = 0; n < members.Count; n++)
					{
						BuildListDestDisplayOrder(members[n], selected);
					}
					level--;
				}
			}
		}


		/*
		private void foo()
		{ 
			int c = lutils.DisplayOrderBuildLists(seqSource, ref savedIndexes, ref levels, false, false);

			for (int i = 0; i < savedIndexes.Length; i++)
			{
				iLORMember4 sourceMember = seqSource.AllMembers.BySavedIndex[savedIndexes[i]];
				LORRGBChild4 rchild = LORRGBChild4.None;
				LORMemberType4 sourceType = sourceMember.MemberType;

				if (sourceType == LORMemberType4.Channel)
				{
					LORChannel4 ch = (LORChannel4)sourceMember;
					rchild = ch.rgbChild;
				}

				if (rchild == LORRGBChild4.None)
				{
					if (mapSrcToMast[savedIndexes[i]].Count == 0)
					{
						if (chkUnmapped.Checked)
						{
							AddMapping(null, sourceMember, levels[i], 0);
						}
					}
					else
					{
						for (int j = 0; j < mapSrcToMast[savedIndexes[i]].Count; j++)
						{
							iLORMember4 destMember = mapSrcToMast[savedIndexes[i]][j];
							AddMapping(destMember, sourceMember, levels[i], 0);
						} // end loop thru mapped list
					} // end mapped or not
				} // end not rgb child
			} // end loop thru source by display order saved indexes

			if (chkUnmapped.Checked)
			{
				c = lutils.DisplayOrderBuildLists(seqDest, ref savedIndexes, ref levels, false, false);
				for (int i = 0; i < savedIndexes.Length; i++)
				{
					iLORMember4 destMember = seqDest.AllMembers.BySavedIndex[savedIndexes[i]];
					int destSI = destMember.SavedIndex;
					if (destSI < mapMastToSrc.Length)
					{
						if (mapMastToSrc[destSI] == null)
						{
							LORRGBChild4 rchild = LORRGBChild4.None;
							if (destMember.MemberType == LORMemberType4.Channel)
							{
								LORChannel4 ch = (LORChannel4)destMember;
								rchild = ch.rgbChild;
							}
							if (rchild == LORRGBChild4.None)
							{
								AddMapping(destMember, null, 0, 0);
							}
						} // end if unmapped
					}
					else
					{
						// Why is the index out of range?!?!
						if (Fyle.isWiz)
						{
							System.Diagnostics.Debugger.Break();
						}
					}
				} // end loop thru destination by display order saved indexes
			} // end show unmapped
		} // end FillBySourceDispOrder
		*/


		public void FillByDestAlpha()
		{
			//for (int i = 0; i < seqDest.Members.ByName.Count; i++)
			foreach(iLORMember4 destMember in seqDest.AllMembers.ByName.Values)
			{
				//iLORMember4 destMember = seqDest.Members.ByName[i];
				LORRGBChild4 rchild = LORRGBChild4.None;
				LORMemberType4 tt = destMember.MemberType;

				if (tt == LORMemberType4.Channel)
				{
					LORChannel4 ch = (LORChannel4)seqDest.AllMembers.BySavedIndex[destMember.SavedIndex];
					rchild = ch.rgbChild;
				}

				if (rchild == LORRGBChild4.None)
				{
					string n = destMember.Name;
					//Console.WriteLine(n);
					if (destMember.MapTo == null)
					{
						if (chkUnmapped.Checked)
						{
							AddMapping(null, destMember, 0);
						}
					}
					else
					{
						iLORMember4 sourceMember = destMember.MapTo;
						AddMapping(sourceMember, destMember, 0);
					} // end mapped or not
				} // end not RGB child
			} // end for loop thru AlphaAllNames

			if (chkUnmapped.Checked)
			{
				//for (int i = 0; i < seqSource.Members.ByName.Count; i++)
				foreach (iLORMember4 sourceMember in seqSource.AllMembers.ByName.Values)
				{
					if (sourceMember.ZCount < 1)
					{
						LORRGBChild4 rchild = LORRGBChild4.None;
						if (sourceMember.MemberType == LORMemberType4.Channel)
						{
							LORChannel4 ch = (LORChannel4)sourceMember;
							rchild = ch.rgbChild;
						}
						if (rchild == LORRGBChild4.None)
						{
							AddMapping(sourceMember, null, 0);
						} // end if not rgb child
					} // end not mapped
				} // end for loop thru AlphaAllNames list
			} // end include unmapped
		} // end FillByDestAlpha

		public void FillByDestDispOrder()
		{
			level = 0; // Reset
								 // Add all mapped channels, dig thru destination tree
			for (int t = 0; t < seqDest.Tracks.Count; t++)
			{
				// Add mapped channels
				BuildListDestDisplayOrder(seqDest.Tracks[t], true);
			}
			// Also display Un-Mapped channels?
			if (chkUnmapped.Checked)
			{
				level = 0;
				for (int t = 0; t < seqDest.Tracks.Count; t++)
				{
					// Add Un-Mapped Destination channels after the mapped ones
					BuildListDestDisplayOrder(seqDest.Tracks[t], false);
				}
				level = 0;
				for (int t = 0; t < seqSource.Tracks.Count; t++)
				{
					// Add Un-Mapped source channels last
					BuildListSourceDisplayOrder(seqSource.Tracks[t], false);
				}
			}
		} // end FillByDestDispOrder

		public void AddMapping(iLORMember4 sourceMember, iLORMember4 destMember, int indent)
		{
			string[] lvi = { "", "" };
			string s = "";
			//string k = "";
			int[] k = null;
			int iconIndex = lutils.UNDEFINED;
			for (int i = 0; i < indent; i++) 
			{
				s += "  ";
			}
			if (sourceMember == null)
			{
				s += "  (Unmapped)";
			}
			else
			{
				string n = sourceMember.Name;
				s += n;
			}
			if (sourceOnRight)
			{
				lvi[1] = s;
			}
			else
			{
				lvi[0] = s;
			}
			

		string m = "";
			for (int i = 0; i < indent; i++) 
			{
				m += "  ";
			}
			if (destMember == null)
			{
				m += "  (Unmapped)";
				LORMemberType4 o = sourceMember.MemberType;
				if (o == LORMemberType4.Track) k = TreeUtils.ICONtrack;
				if (o == LORMemberType4.RGBChannel) k = TreeUtils.ICONrgbChannel;
				if (o == LORMemberType4.ChannelGroup) k = TreeUtils.ICONchannelGroup;
				if (o == LORMemberType4.Channel)
				{
					LORChannel4 ch = (LORChannel4)sourceMember;
					iconIndex = lutils.ColorIcon(imlTreeIcons, ch.color);
				}
			}
			else
			{
				string n = destMember.Name;
				m += n;
				LORMemberType4 o = destMember.MemberType;
				if (o == LORMemberType4.Track) k = TreeUtils.ICONtrack;
				if (o == LORMemberType4.RGBChannel) k = TreeUtils.ICONrgbChannel;
				if (o == LORMemberType4.ChannelGroup) k = TreeUtils.ICONchannelGroup;
				if (o == LORMemberType4.Channel)
				{
					LORChannel4 ch = (LORChannel4)destMember;
					iconIndex = lutils.ColorIcon(imlTreeIcons, ch.color);
				}
			}
			if (sourceOnRight)
			{
				lvi[0] = m;
			}
			else
			{
				lvi[1] = m;
			}

			if (iconIndex < 0)
			{
				//iconIndex = imlTreeIcons.Images.IndexOfKey(k);
				//iconIndex = 
			}

			int w = imlTreeIcons.Images.Count;



			ListViewItem newItem = new ListViewItem(lvi);
			newItem.ImageIndex = iconIndex;
			//newItem.StateImageIndex = iconIndex;
			if (lstMap.Items.Count %2 == 0)
			{
				newItem.BackColor = Color.LightCyan;
			}
			if ((destMember == null) || (sourceMember == null))
			{
				newItem.Font = new Font(newItem.Font, FontStyle.Italic);
			}
			else
			{
				newItem.Font = new Font(newItem.Font, FontStyle.Bold);
			}


			lstMap.Items.Add(newItem);
			Console.Write(s + "\t");
			string u = lstMap.Items[lstMap.Items.Count - 1].Text;
			Console.WriteLine(u);
		}

		private void lstMap_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void lstMap_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			Point mousePosition = lstMap.PointToClient(Control.MousePosition);
			ListViewHitTestInfo hit = lstMap.HitTest(mousePosition);
			int columnindex = 0;
			try
			{
				columnindex = hit.Item.SubItems.IndexOf(hit.SubItem);
			}
			catch
			{ }

			if (columnindex == 0)
			{
				if (lastCol == 1)
				{
					sortAlpha = false;
					if (sourceOnRight)
					{
						sortBySource = false;
					}
					else
					{
						sortBySource = true;
					}
				}
				else
				{
					sortAlpha = !sortAlpha;
				}
			}
			else // Must be column 1
			{
				if (lastCol == 0)
				{
					sortAlpha = false;
					if (sourceOnRight)
					{
						sortBySource = true;
					}
					else
					{
						sortBySource = false;
					}
				}
				else
				{
					sortAlpha = !sortAlpha;
				}
			}
			BuildList();
			lastCol = columnindex;

		}

		private void chkUnmapped_CheckedChanged(object sender, EventArgs e)
		{
			BuildList();
		}

		private void ImBusy(bool workingHard)
		{
			lstMap.Enabled = !workingHard;
			if (workingHard)
			{
				this.Cursor = Cursors.WaitCursor;
			}
			else
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void frmMapList_Paint(object sender, PaintEventArgs e)
		{
			if (!firstShown)
			{
				firstShown = true;
				BuildList();
			}

		}

		private void ArrangePanes(bool sourceNowOnRight)
		{
			if (sourceNowOnRight)
			{
				lstMap.Columns[0].Text = "Destination LORChannel4";
				lstMap.Columns[1].Text = "Source LORChannel4";
			}
			else
			{
				lstMap.Columns[0].Text = "Source LORChannel4";
				lstMap.Columns[1].Text = "Destination LORChannel4";
			}
			sourceOnRight = sourceNowOnRight;
			//BuildList();
		}





	} // end public partial class frmMapList : Form
} // end namespace UtilORama4
 
	