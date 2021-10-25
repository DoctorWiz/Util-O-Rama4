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
		public LORSequence4 seqSource;
		public LORSequence4 seqMaster;
		public iLORMember4[] mapMastToSrc = null;  // Array indexed by Master.SavedIndex, elements contain Source.SaveIndex
		public List<iLORMember4>[] mapSrcToMast = null; // Array indexed by Source.SavedIindex, elements are Lists of Master.SavedIndex-es

		private const char DELIM_Map = (char)164;  // ¤
		private const string NOTMAPPED = "\t(Not Mapped)";
		private const int ONEMEELION = 1000000;
		private MapInfo[] sortedMap;
		private int sortedMapCount = 0;
		private bool firstShown = false;
		private bool sourceOnRight = false;


		private const string MAPSUM = "LORChannel4 Mapping Summary - ";


		public frmMapList(LORSequence4 sourceSequence, LORSequence4 masterSequence, List<iLORMember4>[] srcToMastMap, iLORMember4[] mastToSrcMap, ImageList icons)
		{
			InitializeComponent();

			seqSource = sourceSequence;
			seqMaster = masterSequence;
			mapMastToSrc = mastToSrcMap;
			mapSrcToMast = srcToMastMap;
			imlTreeIcons = icons;
			lstMap.SmallImageList = icons;

			//seqSource.Members.ReIndex();
			//seqMaster.Members.ReIndex();


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
					FillByMasterAlpha();
				}
				else // NOT sort Alpha
				{
					lblAlpha.Text = "by DispOrder";
					this.Text = MAPSUM + "by Destination in Display Order";
					FillByMasterDispOrder();
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
					if (mapSrcToMast[sourceMember.SavedIndex].Count == 0)
					{
						if (chkUnmapped.Checked)
						{
							AddMapping(null, sourceMember, 0, 0);
						}
					}
					else
					{
						for (int j = 0; j < mapSrcToMast[sourceMember.SavedIndex].Count; j++)
						{
							iLORMember4 masterMember = mapSrcToMast[sourceMember.SavedIndex][j];
							AddMapping(masterMember, sourceMember, 0, 0);
						} // end loop thru mappings
					} // end mapped or not
				} // end not rgb child
			} // end loop thru source by AlphaAllNames

			if (chkUnmapped.Checked)
			{
				//for (int i = 0; i < seqMaster.Members.ByName.Count; i++)
				foreach (iLORMember4 masterMember in seqMaster.AllMembers.ByName.Values)
				{
					//int mSI = seqMaster.Members.ByName[i].SavedIndex;
					if (mapMastToSrc[masterMember.SavedIndex] == null)
					{
						LORRGBChild4 rchild = LORRGBChild4.None;
						if (masterMember.MemberType == LORMemberType4.Channel)
						{
							LORChannel4 ch = (LORChannel4)masterMember;
							rchild = ch.rgbChild;
						}
						if (rchild == LORRGBChild4.None)
						{
							AddMapping(masterMember, null, 0, 0);
						}
					} // end if not mapped
				} // end loop thru master by AlphaAllNames
			} // end display unmapped
		} // end FillBySourceAlpha

		public void FillBySourceDispOrder()
		{
			int[] savedIndexes = null;
			int[] levels = null;

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
							iLORMember4 masterMember = mapSrcToMast[savedIndexes[i]][j];
							AddMapping(masterMember, sourceMember, levels[i], 0);
						} // end loop thru mapped list
					} // end mapped or not
				} // end not rgb child
			} // end loop thru source by display order saved indexes

			if (chkUnmapped.Checked)
			{
				c = lutils.DisplayOrderBuildLists(seqMaster, ref savedIndexes, ref levels, false, false);
				for (int i = 0; i < savedIndexes.Length; i++)
				{
					iLORMember4 masterMember = seqMaster.AllMembers.BySavedIndex[savedIndexes[i]];
					int masterSI = masterMember.SavedIndex;
					if (masterSI < mapMastToSrc.Length)
					{
						if (mapMastToSrc[masterSI] == null)
						{
							LORRGBChild4 rchild = LORRGBChild4.None;
							if (masterMember.MemberType == LORMemberType4.Channel)
							{
								LORChannel4 ch = (LORChannel4)masterMember;
								rchild = ch.rgbChild;
							}
							if (rchild == LORRGBChild4.None)
							{
								AddMapping(masterMember, null, 0, 0);
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
				} // end loop thru master by display order saved indexes
			} // end show unmapped
		} // end FillBySourceDispOrder

		public void FillByMasterAlpha()
		{
			//for (int i = 0; i < seqMaster.Members.ByName.Count; i++)
			foreach(iLORMember4 masterMember in seqMaster.AllMembers.ByName.Values)
			{
				//iLORMember4 masterMember = seqMaster.Members.ByName[i];
				LORRGBChild4 rchild = LORRGBChild4.None;
				LORMemberType4 tt = masterMember.MemberType;

				if (tt == LORMemberType4.Channel)
				{
					LORChannel4 ch = (LORChannel4)seqMaster.AllMembers.BySavedIndex[masterMember.SavedIndex];
					rchild = ch.rgbChild;
				}

				if (rchild == LORRGBChild4.None)
				{
					string n = masterMember.Name;
					//Console.WriteLine(n);
					if (mapMastToSrc[masterMember.SavedIndex] == null)
					{
						if (chkUnmapped.Checked)
						{
							AddMapping(masterMember, null, 0, 0);
						}
					}
					else
					{
						iLORMember4 sourceMember = mapMastToSrc[masterMember.SavedIndex];
						AddMapping(masterMember, sourceMember, 0, 0);
					} // end mapped or not
				} // end not RGB child
			} // end for loop thru AlphaAllNames

			if (chkUnmapped.Checked)
			{
				//for (int i = 0; i < seqSource.Members.ByName.Count; i++)
				foreach (iLORMember4 sourceMember in seqSource.AllMembers.ByName.Values)
				{
					int sourceSI = sourceMember.SavedIndex;
					if (mapSrcToMast[sourceSI].Count == 0)
					{
						LORRGBChild4 rchild = LORRGBChild4.None;
						if (sourceMember.MemberType == LORMemberType4.Channel)
						{
							LORChannel4 ch = (LORChannel4)sourceMember;
							rchild = ch.rgbChild;
						}
						if (rchild == LORRGBChild4.None)
						{
							AddMapping(null, sourceMember, 0, 0);
						} // end if not rgb child
					} // end not mapped
				} // end for loop thru AlphaAllNames list
			} // end include unmapped
		} // end FillByMasterAlpha

		public void FillByMasterDispOrder()
		{
			int[] savedIndexes = null;
			int[] levels = null;

			int c = lutils.DisplayOrderBuildLists(seqMaster, ref savedIndexes, ref levels, false, false);

			for (int i = 0; i < savedIndexes.Length; i++)
			{
				iLORMember4 masterMember = seqMaster.AllMembers.BySavedIndex[savedIndexes[i]];
				int masterSI = masterMember.SavedIndex;
				LORRGBChild4 rchild = LORRGBChild4.None;
				LORMemberType4 masterType = masterMember.MemberType;

				if (masterType == LORMemberType4.Channel)
				{
					LORChannel4 ch = (LORChannel4)masterMember;
					rchild = ch.rgbChild;
				}

				if (rchild == LORRGBChild4.None)
				{
					if (mapMastToSrc[masterSI] == null)
					{
						if (chkUnmapped.Checked)
						{
							AddMapping(masterMember, null, 0, levels[i]);
						}
						else
						{
							iLORMember4 sourceMember = mapMastToSrc[masterSI];
							AddMapping(masterMember, sourceMember,  0, levels[i]);
						}
					} // end if mappings (or not)
				} // end if not an RGB child
			} // end for loop thru display order saved indexes
			if (chkUnmapped.Checked)
			{
				c = lutils.DisplayOrderBuildLists(seqSource, ref savedIndexes, ref levels, false, false);
				for (int i = 0; i < savedIndexes.Length; i++)
				{
					int sourceSI = savedIndexes[i];
					iLORMember4 sourceMember = seqSource.AllMembers.BySavedIndex[sourceSI];
					if (mapSrcToMast[sourceSI].Count == 0)
					{
						LORRGBChild4 rchild = LORRGBChild4.None;
						if (sourceMember.MemberType == LORMemberType4.Channel)
						{
							LORChannel4 ch = (LORChannel4)sourceMember;
							rchild = ch.rgbChild;
						}
						if (rchild == LORRGBChild4.None)
						{
							AddMapping(null, sourceMember, levels[i], 0);
						} // end if not RGB child
					} // end if not mapped
				} // end for loop thru display order saved indexes
			} // end include unmapped
		} // end FillByMasterDispOrder

		public void AddMapping(iLORMember4 masterMember, iLORMember4 sourceMember, int levelS, int levelM)
		{
			string[] lvi = { "", "" };
			string s = "";
			//string k = "";
			int[] k = null;
			int iconIndex = lutils.UNDEFINED;
			for (int i = 0; i < levelS; i++) 
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
			for (int i = 0; i < levelM; i++) 
			{
				m += "  ";
			}
			if (masterMember == null)
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
				string n = masterMember.Name;
				m += n;
				LORMemberType4 o = masterMember.MemberType;
				if (o == LORMemberType4.Track) k = TreeUtils.ICONtrack;
				if (o == LORMemberType4.RGBChannel) k = TreeUtils.ICONrgbChannel;
				if (o == LORMemberType4.ChannelGroup) k = TreeUtils.ICONchannelGroup;
				if (o == LORMemberType4.Channel)
				{
					LORChannel4 ch = (LORChannel4)masterMember;
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
			if ((masterMember == null) || (sourceMember == null))
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
 
	