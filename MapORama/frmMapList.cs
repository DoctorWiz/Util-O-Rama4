using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOR4Utils;
using FileHelper;
using System.Diagnostics;

namespace UtilORama4
{
	public partial class frmMapList : Form
	{
		public bool sortBySource = true;    // Otherwise sort by New LOR4Sequence
		public bool sortAlpha = false;  // Otherwise sort by Display Order
		private int lastCol = 0;
		private int level = 0;
		public LOR4Sequence seqSource;
		public LOR4Sequence seqDest;
		//public iLOR4Member[] mapMastToSrc = null;  // Array indexed by destination.SavedIndex, elements contain Source.SaveIndex
		//public List<iLOR4Member>[] mapSrcToMast = null; // Array indexed by Source.SavedIindex, elements are Lists of destination.SavedIndex-es

		private const char DELIM_Map = (char)164;  // ¤
		private const string NOTMAPPED = "\t(Not Mapped)";
		private const int ONEMEELION = 1000000;
		//private MapInfo[] sortedMap;
		private int sortedMapCount = 0;
		private bool firstShown = false;
		private bool sourceOnRight = false;


		private const string MAPSUM = "LOR4Channel Mapping Summary - ";


		public frmMapList(LOR4Sequence sourceSequence, LOR4Sequence destSequence, ImageList icons)
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
			foreach (iLOR4Member sourceMember in seqSource.AllMembers.ByName.Values)
			{
				//iLOR4Member sourceMember = seqSource.Members.ByName[i];
				LORRGBChild4 rchild = LORRGBChild4.None;
				LOR4MemberType tt = sourceMember.MemberType;

				if (tt == LOR4MemberType.Channel)
				{
					LOR4Channel ch = (LOR4Channel)seqSource.AllMembers.BySavedIndex[sourceMember.SavedIndex];
					rchild = ch.rgbChild;
				}
				//string n = sourceMember.Name;
				//Console.Write(n + "\t");
				//Console.Write(sourceMember.SavedIndex);
				//Console.Write("\t");

				//iLOR4Member s2 = seqSource.Members.BySavedIndex[sourceMember.SavedIndex];
				//string n2 = s2.Name;
				//Console.Write(n2 + "\t");
				//Console.Write(s2.SavedIndex);
				//Console.Write("\t");

				if (rchild == LORRGBChild4.None)
				{
					if (sourceMember.Tag != null)
					{
						List<iLOR4Member> mapsTos = (List<iLOR4Member>)sourceMember.Tag;
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
								iLOR4Member destMember = mapsTos[j];
								AddMapping(destMember, sourceMember, 0);
							} // end loop thru mappings
						} // end mapped or not
					}
				} // end not rgb child
			} // end loop thru source by AlphaAllNames

			if (chkUnmapped.Checked)
			{
				//for (int i = 0; i < seqDest.Members.ByName.Count; i++)
				foreach (iLOR4Member destMember in seqDest.AllMembers.ByName.Values)
				{
					//int mSI = seqDest.Members.ByName[i].SavedIndex;
					if (destMember.MapTo == null)
					{
						LORRGBChild4 rchild = LORRGBChild4.None;
						if (destMember.MemberType == LOR4MemberType.Channel)
						{
							LOR4Channel ch = (LOR4Channel)destMember;
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
				BuildListSourceDisplayOrder(seqSource.Tracks[t], true);
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

		public void BuildListSourceDisplayOrder(iLOR4Member sourceMember, bool selected)
		{
			if (sourceMember.Selected = selected)
			{
				if (!selected)
				{
					AddMapping(sourceMember, null, level);
				}
				if (sourceMember.Tag != null)
				{
					List<iLOR4Member> mapsTos = (List<iLOR4Member>)sourceMember.Tag;
					for (int m = 0; m < mapsTos.Count; m++)
					{
						iLOR4Member destMember = mapsTos[m];
						AddMapping(sourceMember, destMember, level);
					}
				}
				LOR4Membership members = null;
				switch (sourceMember.MemberType)
				{
					case LOR4MemberType.Channel:
						// Don't need to do anything additional
						break;
					case LOR4MemberType.RGBChannel:
						level++;
						LOR4RGBChannel rgb = (LOR4RGBChannel)sourceMember;
						BuildListSourceDisplayOrder(rgb.redChannel, selected);
						BuildListSourceDisplayOrder(rgb.grnChannel, selected);
						BuildListSourceDisplayOrder(rgb.bluChannel, selected);
						level--;
						break;
					case LOR4MemberType.ChannelGroup:
						LOR4ChannelGroup group = (LOR4ChannelGroup)sourceMember;
						members = group.Members;
						break;
					case LOR4MemberType.Track:
						LORTrack4 track = (LORTrack4)sourceMember;
						members = track.Members;
						break;
					case LOR4MemberType.Cosmic:
						LOR4Cosmic cosmic = (LOR4Cosmic)sourceMember;
						members = cosmic.Members;
						break;
				}
				if (members != null)
				{
					level++;
					for (int n = 0; n < members.Count; n++)
					{
						BuildListSourceDisplayOrder(members[n], selected);
					}
					level--;
				}
			}
		}

		public void BuildListDestDisplayOrder(iLOR4Member destMember, bool selected)
		{
			if (destMember.Selected = selected)
			{
				if (!selected)
				{
					AddMapping(null, destMember, level);
				}
				if (destMember.MapTo != null)
				{
					iLOR4Member sourceMember = destMember.MapTo;
					AddMapping(sourceMember, destMember, level);
				}
				LOR4Membership members = null;
				switch (destMember.MemberType)
				{
					case LOR4MemberType.Channel:
						// Don't need to do anything additional
						break;
					case LOR4MemberType.RGBChannel:
						level++;
						LOR4RGBChannel rgb = (LOR4RGBChannel)destMember;
						BuildListDestDisplayOrder(rgb.redChannel, selected);
						BuildListDestDisplayOrder(rgb.grnChannel, selected);
						BuildListDestDisplayOrder(rgb.bluChannel, selected);
						level--;
						break;
					case LOR4MemberType.ChannelGroup:
						LOR4ChannelGroup group = (LOR4ChannelGroup)destMember;
						members = group.Members;
						break;
					case LOR4MemberType.Track:
						LORTrack4 track = (LORTrack4)destMember;
						members = track.Members;
						break;
					case LOR4MemberType.Cosmic:
						LOR4Cosmic cosmic = (LOR4Cosmic)destMember;
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
				iLOR4Member sourceMember = seqSource.AllMembers.BySavedIndex[savedIndexes[i]];
				LORRGBChild4 rchild = LORRGBChild4.None;
				LOR4MemberType sourceType = sourceMember.MemberType;

				if (sourceType == LOR4MemberType.Channel)
				{
					LOR4Channel ch = (LOR4Channel)sourceMember;
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
							iLOR4Member destMember = mapSrcToMast[savedIndexes[i]][j];
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
					iLOR4Member destMember = seqDest.AllMembers.BySavedIndex[savedIndexes[i]];
					int destSI = destMember.SavedIndex;
					if (destSI < mapMastToSrc.Length)
					{
						if (mapMastToSrc[destSI] == null)
						{
							LORRGBChild4 rchild = LORRGBChild4.None;
							if (destMember.MemberType == LOR4MemberType.Channel)
							{
								LOR4Channel ch = (LOR4Channel)destMember;
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
			foreach (iLOR4Member destMember in seqDest.AllMembers.ByName.Values)
			{
				//iLOR4Member destMember = seqDest.Members.ByName[i];
				LORRGBChild4 rchild = LORRGBChild4.None;
				LOR4MemberType tt = destMember.MemberType;

				if (tt == LOR4MemberType.Channel)
				{
					LOR4Channel ch = (LOR4Channel)seqDest.AllMembers.BySavedIndex[destMember.SavedIndex];
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
						iLOR4Member sourceMember = destMember.MapTo;
						AddMapping(sourceMember, destMember, 0);
					} // end mapped or not
				} // end not RGB child
			} // end for loop thru AlphaAllNames

			if (chkUnmapped.Checked)
			{
				//for (int i = 0; i < seqSource.Members.ByName.Count; i++)
				foreach (iLOR4Member sourceMember in seqSource.AllMembers.ByName.Values)
				{
					if (sourceMember.ZCount < 1)
					{
						LORRGBChild4 rchild = LORRGBChild4.None;
						if (sourceMember.MemberType == LOR4MemberType.Channel)
						{
							LOR4Channel ch = (LOR4Channel)sourceMember;
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

		public void AddMapping(iLOR4Member sourceMember, iLOR4Member destMember, int indent)
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
				LOR4MemberType o = sourceMember.MemberType;
				if (o == LOR4MemberType.Track) k = TreeUtils.ICONtrack;
				if (o == LOR4MemberType.RGBChannel) k = TreeUtils.ICONrgbChannel;
				if (o == LOR4MemberType.ChannelGroup) k = TreeUtils.ICONchannelGroup;
				if (o == LOR4MemberType.Channel)
				{
					LOR4Channel ch = (LOR4Channel)sourceMember;
					iconIndex = lutils.ColorIcon(imlTreeIcons, ch.color);
				}
			}
			else
			{
				string n = destMember.Name;
				m += n;
				LOR4MemberType o = destMember.MemberType;
				if (o == LOR4MemberType.Track) k = TreeUtils.ICONtrack;
				if (o == LOR4MemberType.RGBChannel) k = TreeUtils.ICONrgbChannel;
				if (o == LOR4MemberType.ChannelGroup) k = TreeUtils.ICONchannelGroup;
				if (o == LOR4MemberType.Channel)
				{
					LOR4Channel ch = (LOR4Channel)destMember;
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
			if (lstMap.Items.Count % 2 == 0)
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
				lstMap.Columns[0].Text = "Destination LOR4Channel";
				lstMap.Columns[1].Text = "Source LOR4Channel";
			}
			else
			{
				lstMap.Columns[0].Text = "Source LOR4Channel";
				lstMap.Columns[1].Text = "Destination LOR4Channel";
			}
			sourceOnRight = sourceNowOnRight;
			//BuildList();
		}





	} // end public partial class frmMapList : Form
} // end namespace UtilORama4

