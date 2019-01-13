using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LORUtils;
using System.Diagnostics;

namespace MapORama
{
	public partial class frmMapList : Form
	{
		public bool sortBySource = true;		// Otherwise sort by New Sequence4
		public bool sortAlpha = false;  // Otherwise sort by Display Order
		private int lastCol = 0;
		public Sequence4 seqSource;
		public Sequence4 seqMaster;
		public int[] MtoS = null;  // Array indexed by Master.SavedIndex, elements contain Source.SaveIndex
		public List<int>[] StoM = null; // Array indexed by Source.SavedIindex, elements are Lists of Master.SavedIndex-es

		private const char DELIM_Map = (char)164;  // ¤
		private const string NOTMAPPED = "\t(Not Mapped)";
		private const int ONEMEELION = 1000000;
		private MapInfo[] sortedMap;
		private int sortedMapCount = 0;
		private bool firstShown = false;
		private bool sourceOnRight = false;


		private const string MAPSUM = "Channel Mapping Summary - ";


		public frmMapList(Sequence4 seqSourceuence, Sequence4 seqMasteruence, List<int>[] stom, int[] mtos, ImageList icons)
		{
			InitializeComponent();

			seqSource = seqSourceuence;
			seqMaster = seqMasteruence;
			MtoS = mtos;
			StoM = stom;
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
			else // Sort by Master
			{
				lblKey.Text = "by Master";
				if (sortAlpha)
				{
					lblAlpha.Text = "by Alpha";
					this.Text = MAPSUM + "By Master in Alphabetical order";
					FillByMasterAlpha();
				}
				else // NOT sort Alpha
				{
					lblAlpha.Text = "by DispOrder";
					this.Text = MAPSUM + "by Master in Display Order";
					FillByMasterDispOrder();
				} // end Sort by Alpha
			}
			ImBusy(false);
		} // end BuildList

		public void FillBySourceAlpha()
		{

			//utils.ClearOutputWindow();
			//Console.WriteLine();
			//Console.WriteLine("***** FILL BY SOURCE ALPHA ******");
			//Console.WriteLine();
			//Console.WriteLine();

			//for (int i = 0; i < seqSource.Members.byName.Count; i++)
			foreach(IMember sID in seqSource.Members.byName.Values)
			{
				//IMember sID = seqSource.Members.byName[i];
				RGBchild rchild = RGBchild.None;
				MemberType tt = sID.MemberType;

				if (tt == MemberType.Channel)
				{
					Channel ch = (Channel)seqSource.Members.bySavedIndex[sID.SavedIndex];
					rchild = ch.rgbChild;
				}
					//string n = sID.Name;
					//Console.Write(n + "\t");
					//Console.Write(sID.SavedIndex);
					//Console.Write("\t");

					//IMember s2 = seqSource.Members.bySavedIndex[sID.SavedIndex];
					//string n2 = s2.Name;
					//Console.Write(n2 + "\t");
					//Console.Write(s2.SavedIndex);
					//Console.Write("\t");

				if (rchild == RGBchild.None)
				{ 
					if (StoM[sID.SavedIndex].Count == 0)
					{
						if (chkUnmapped.Checked)
						{
							AddMapping(sID.SavedIndex, utils.UNDEFINED, 0, 0);
						}
					}
					else
					{
						for (int j = 0; j < StoM[sID.SavedIndex].Count; j++)
						{
							int mSI = StoM[sID.SavedIndex][j];
							AddMapping(sID.SavedIndex, mSI, 0, 0);
						} // end loop thru mappings
					} // end mapped or not
				} // end not rgb child
			} // end loop thru source by AlphaAllNames

			if (chkUnmapped.Checked)
			{
				//for (int i = 0; i < seqMaster.Members.byName.Count; i++)
				foreach (IMember mID in seqMaster.Members.byName.Values)
				{
					//int mSI = seqMaster.Members.byName[i].SavedIndex;
					int mSI = mID.SavedIndex;
					if (MtoS[mSI] < 0)
					{
						RGBchild rchild = RGBchild.None;
						if (seqMaster.Members.bySavedIndex[mSI].MemberType == MemberType.Channel)
						{
							Channel ch = (Channel)seqMaster.Members.bySavedIndex[mSI];
							rchild = ch.rgbChild;
						}
						if (rchild == RGBchild.None)
						{
							AddMapping(utils.UNDEFINED, mSI, 0, 0);
						}
					} // end if not mapped
				} // end loop thru master by AlphaAllNames
			} // end display unmapped
		} // end FillBySourceAlpha

		public void FillBySourceDispOrder()
		{
			int[] savedIndexes = null;
			int[] levels = null;

			int c = utils.BuildDisplayOrder(seqSource, ref savedIndexes, ref levels, false, false);

			for (int i = 0; i < savedIndexes.Length; i++)
			{
				IMember sID = seqSource.Members.bySavedIndex[savedIndexes[i]];
				RGBchild rchild = RGBchild.None;
				MemberType tt = sID.MemberType;

				if (tt == MemberType.Channel)
				{
					Channel ch = (Channel)seqSource.Members.bySavedIndex[sID.SavedIndex];
					rchild = ch.rgbChild;
				}

				if (rchild == RGBchild.None)
				{
					if (StoM[savedIndexes[i]].Count == 0)
					{
						if (chkUnmapped.Checked)
						{
							AddMapping(savedIndexes[i], utils.UNDEFINED, levels[i], 0);
						}
					}
					else
					{
						for (int j = 0; j < StoM[savedIndexes[i]].Count; j++)
						{
							int mSI = StoM[savedIndexes[i]][j];
							AddMapping(savedIndexes[i], mSI, levels[i], 0);
						} // end loop thru mapped list
					} // end mapped or not
				} // end not rgb child
			} // end loop thru source by display order saved indexes

			if (chkUnmapped.Checked)
			{
				c = utils.BuildDisplayOrder(seqMaster, ref savedIndexes, ref levels, false, false);
				for (int i = 0; i < savedIndexes.Length; i++)
				{
					int mSI = savedIndexes[i];
					if (MtoS[mSI] < 0)
					{
						RGBchild rchild = RGBchild.None;
						if (seqMaster.Members.bySavedIndex[mSI].MemberType == MemberType.Channel)
						{
							Channel ch = (Channel)seqMaster.Members.bySavedIndex[mSI];
							rchild = ch.rgbChild;
						}
						if (rchild == RGBchild.None)
						{
							AddMapping(utils.UNDEFINED, mSI, 0, 0);
						}
					} // end if unmapped
				} // end loop thru master by display order saved indexes
			} // end show unmapped
		} // end FillBySourceDispOrder

		public void FillByMasterAlpha()
		{
			//for (int i = 0; i < seqMaster.Members.byName.Count; i++)
			foreach(IMember mID in seqMaster.Members.byName.Values)
			{
				//IMember mID = seqMaster.Members.byName[i];
				RGBchild rchild = RGBchild.None;
				MemberType tt = mID.MemberType;

				if (tt == MemberType.Channel)
				{
					Channel ch = (Channel)seqMaster.Members.bySavedIndex[mID.SavedIndex];
					rchild = ch.rgbChild;
				}

				if (rchild == RGBchild.None)
				{
					string n = mID.Name;
					//Console.WriteLine(n);
					if (MtoS[mID.SavedIndex] < 0)
					{
						if (chkUnmapped.Checked)
						{
							AddMapping(utils.UNDEFINED, mID.SavedIndex, 0, 0);
						}
					}
					else
					{
						int sSI = MtoS[mID.SavedIndex];
						AddMapping(sSI, mID.SavedIndex, 0, 0);
					} // end mapped or not
				} // end not RGB child
			} // end for loop thru AlphaAllNames

			if (chkUnmapped.Checked)
			{
				//for (int i = 0; i < seqSource.Members.byName.Count; i++)
				foreach (IMember sID in seqSource.Members.byName.Values)
				{
					int sSI = sID.SavedIndex;
					if (StoM[sSI].Count == 0)
					{
						RGBchild rchild = RGBchild.None;
						if (seqSource.Members.bySavedIndex[sSI].MemberType == MemberType.Channel)
						{
							Channel ch = (Channel)seqSource.Members.bySavedIndex[sSI];
							rchild = ch.rgbChild;
						}
						if (rchild == RGBchild.None)
						{
							AddMapping(sSI, utils.UNDEFINED, 0, 0);
						} // end if not rgb child
					} // end not mapped
				} // end for loop thru AlphaAllNames list
			} // end include unmapped
		} // end FillByMasterAlpha

		public void FillByMasterDispOrder()
		{
			int[] savedIndexes = null;
			int[] levels = null;

			int c = utils.BuildDisplayOrder(seqMaster, ref savedIndexes, ref levels, false, false);

			for (int i = 0; i < savedIndexes.Length; i++)
			{
				IMember mID = seqMaster.Members.bySavedIndex[savedIndexes[i]];
				RGBchild rchild = RGBchild.None;
				MemberType tt = mID.MemberType;

				if (tt == MemberType.Channel)
				{
					Channel ch = (Channel)seqMaster.Members.bySavedIndex[mID.SavedIndex];
					rchild = ch.rgbChild;
				}

				if (rchild == RGBchild.None)
				{
					if (MtoS[savedIndexes[i]] < 0)
					{
						if (chkUnmapped.Checked)
						{
							AddMapping(utils.UNDEFINED, savedIndexes[i], 0, levels[i]);
						}
						else
						{
							int sSI = MtoS[savedIndexes[i]];
							AddMapping(sSI, savedIndexes[i], 0, levels[i]);
						}
					} // end if mappings (or not)
				} // end if not an RGB child
			} // end for loop thru display order saved indexes
			if (chkUnmapped.Checked)
			{
				c = utils.BuildDisplayOrder(seqSource, ref savedIndexes, ref levels, false, false);
				for (int i = 0; i < savedIndexes.Length; i++)
				{
					int sSI = savedIndexes[i];
					if (StoM[sSI].Count == 0)
					{
						RGBchild rchild = RGBchild.None;
						if (seqSource.Members.bySavedIndex[sSI].MemberType == MemberType.Channel)
						{
							Channel ch = (Channel)seqSource.Members.bySavedIndex[sSI];
							rchild = ch.rgbChild;
						}
						if (rchild == RGBchild.None)
						{
							AddMapping(sSI, utils.UNDEFINED, levels[i], 0);
						} // end if not RGB child
					} // end if not mapped
				} // end for loop thru display order saved indexes
			} // end include unmapped
		} // end FillByMasterDispOrder

		public void AddMapping(int sourceSI, int masterSI, int levelS, int levelM)
		{
			string[] lvi = { "", "" };
			string s = "";
			string k = "";
			int iconIndex = utils.UNDEFINED;
			for (int i = 0; i < levelS; i++) 
			{
				s += "  ";
			}
			if (sourceSI < 0)
			{
				s += "  (Unmapped)";
			}
			else
			{
				string n = seqSource.Members.bySavedIndex[sourceSI].Name;
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
			if (masterSI < 0)
			{
				m += "  (Unmapped)";
				MemberType o = seqSource.Members.bySavedIndex[sourceSI].MemberType;
				if (o == MemberType.Track) k = utils.ICONtrack;
				if (o == MemberType.RGBchannel) k = utils.ICONrgbChannel;
				if (o == MemberType.ChannelGroup) k = utils.ICONchannelGroup;
				if (o == MemberType.Channel)
				{
					Channel ch = (Channel)seqSource.Members.bySavedIndex[sourceSI];
					iconIndex = utils.ColorIcon(imlTreeIcons, ch.color);
				}
			}
			else
			{
				string n = seqMaster.Members.bySavedIndex[masterSI].Name;
				m += n;
				MemberType o = seqMaster.Members.bySavedIndex[masterSI].MemberType;
				if (o == MemberType.Track) k = utils.ICONtrack;
				if (o == MemberType.RGBchannel) k = utils.ICONrgbChannel;
				if (o == MemberType.ChannelGroup) k = utils.ICONchannelGroup;
				if (o == MemberType.Channel)
				{
					Channel ch = (Channel)seqMaster.Members.bySavedIndex[masterSI];
					iconIndex = utils.ColorIcon(imlTreeIcons, ch.color);
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
				iconIndex = imlTreeIcons.Images.IndexOfKey(k);
			}

			int w = imlTreeIcons.Images.Count;



			ListViewItem newItem = new ListViewItem(lvi);
			newItem.ImageIndex = iconIndex;
			//newItem.StateImageIndex = iconIndex;
			if (lstMap.Items.Count %2 == 0)
			{
				newItem.BackColor = Color.LightCyan;
			}
			if ((masterSI < 0) || (sourceSI < 0))
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
				lstMap.Columns[0].Text = "Master Channel";
				lstMap.Columns[1].Text = "Source Channel";
			}
			else
			{
				lstMap.Columns[0].Text = "Source Channel";
				lstMap.Columns[1].Text = "Master Channel";
			}
			sourceOnRight = sourceNowOnRight;
			//BuildList();
		}





	} // end public partial class frmMapList : Form
} // end namespace MapORama
 
	