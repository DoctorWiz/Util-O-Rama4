using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LORUtils;
using Microsoft.Win32;

namespace MergeORama
{
	public partial class frmMerge : Form
	{
		private string basePath = "";
		private string seqFolder = "";
		private string lastFile1 = "";
		private string lastFile2 = "";
		private string lastNewFile = "";
		private Sequence4 seqOne = new Sequence4();
		private Sequence4 seqNew;
		private Sequence4 seqTwo = new Sequence4();

		private const byte ACTIONkeepFirst = 1;
		private const byte ACTIONuseSecond = 2;
		private const byte ACTIONkeepBoth = 3;
		private const byte ACTIONaddNumber = 4;

		private bool mergeTracks = true;
		private bool appendTracks = false;
		private bool mergeTracksByName = true;
		private bool mergeTracksByNumber = false;
		private bool mergeGroupsByName = true;
		private bool mergeGrids = true;
		private byte duplicateNameAction = ACTIONkeepFirst;
		private bool mergeEffects = false;
		private string numberFormat = " (#)";



		private List<TreeNode>[] siNodes = null;



		private class Map
		{
			//public int addIdx = utils.UNDEFINED;
			//public int newIdx = utils.UNDEFINED;
			public IMember addID = null;
			public IMember newID = null;

			public Map()
			{
				// default constructor
			}
			public Map(IMember idAdd, IMember idNew)
			{
				addID = idAdd;
				newID = idNew;
			}

		}

		int nodeIndex = utils.UNDEFINED;

		public frmMerge()
		{
			InitializeComponent();
		}

		private void frmMerge_Load(object sender, EventArgs e)
		{
			InitForm();
		}

		private void InitForm()
		{
			basePath = utils.DefaultUserDataPath;

			seqFolder = utils.DefaultSequencesPath;
			lastFile1 = Properties.Settings.Default.LastFile1;
			lastFile2 = Properties.Settings.Default.LastFile2;
			lastNewFile = Properties.Settings.Default.LastNewFile;

			bool valid = false;
			string dir = "";
			if (lastFile1.Length > 6)
			{
				valid = utils.IsValidPath(lastFile1, true);
			}
			if (!valid) lastFile1 = utils.DefaultSequencesPath;
			valid = false;
			if (lastFile2.Length > 6)
			{
				valid = utils.IsValidPath(lastFile2, true);
			}
			if (!valid) lastFile2 = utils.DefaultSequencesPath;
			valid = false;
			if (lastNewFile.Length > 6)
			{
				valid = utils.IsValidPath(lastNewFile, true);
			}
			if (!valid) lastNewFile = utils.DefaultSequencesPath;
			button1.Visible = utils.IsWizard;


			RestoreFormPosition();

		} // end InitForm

		private void RestoreFormPosition()
		{
			// Multi-Monitor aware
			// with bounds checking
			// repositions as necessary
			// should(?) be able to handle an additional screen that is no longer there,
			// a repositioned taskbar or gadgets bar,
			// or a resolution change.

			// Note: If the saved position spans more than one screen
			// the form will be repositioned to fit all within the
			// screen containing the center point of the form.
			// Thus, when restoring the position, it will no longer
			// span monitors.
			// This is by design!
			// Alternative 1: Position it entirely in the screen containing
			// the top left corner

			Point savedLoc = Properties.Settings.Default.Location;
			Size savedSize = Properties.Settings.Default.Size;
			if (this.FormBorderStyle != FormBorderStyle.Sizable)
			{
				savedSize = new Size(this.Width, this.Height);
				this.MinimumSize = this.Size;
				this.MaximumSize = this.Size;
			}
			FormWindowState savedState = (FormWindowState)Properties.Settings.Default.WindowState;
			int x = savedLoc.X; // Default to saved postion and size, will override if necessary
			int y = savedLoc.Y;
			int w = savedSize.Width;
			int h = savedSize.Height;
			Point center = new Point(x + w / w, y + h / 2); // Find center point
			int onScreen = 0; // Default to primary screen if not found on screen 2+
			Screen screen = Screen.AllScreens[0];

			// Find which screen it is on
			for (int si = 0; si < Screen.AllScreens.Length; si++)
			{
				// Alternative 1: Change "Contains(center)" to "Contains(savedLoc)"
				if (Screen.AllScreens[si].WorkingArea.Contains(center))
				{
					screen = Screen.AllScreens[si];
					onScreen = si;
				}
			}
			Rectangle bounds = screen.WorkingArea;
			// Alternate 2:
			//Rectangle bounds = Screen.GetWorkingArea(center);

			// Test Horizontal Positioning, correct if necessary
			if (this.MinimumSize.Width > bounds.Width)
			{
				// Houston, we have a problem, monitor is too narrow
				System.Diagnostics.Debugger.Break();
				w = this.MinimumSize.Width;
				// Center it horizontally over the working area...
				//x = (bounds.Width - w) / 2 + bounds.Left;
				// OR position it on left edge
				x = bounds.Left;
			}
			else
			{
				// Should fit horizontally
				// Is it too far left?
				if (x < bounds.Left) x = bounds.Left; // Move over
																							// Is it too wide?
				if (w > bounds.Width) w = bounds.Width; // Shrink it
																								// Is it too far right?
				if ((x + w) > bounds.Right)
				{
					// Keep width, move it over
					x = (bounds.Width - w) + bounds.Left;
				}
			}

			// Test Vertical Positioning, correct if necessary
			if (this.MinimumSize.Height > bounds.Height)
			{
				// Houston, we have a problem, monitor is too short
				System.Diagnostics.Debugger.Break();
				h = this.MinimumSize.Height;
				// Center it vertically over the working area...
				//y = (bounds.Height - h) / 2 + bounds.Top;
				// OR position at the top edge
				y = bounds.Top;
			}
			else
			{
				// Should fit vertically
				// Is it too high?
				if (y < bounds.Top) y = bounds.Top; // Move it down
																						// Is it too tall;
				if (h > bounds.Height) h = bounds.Height; // Shorten it
																									// Is it too low?
				if ((y + h) > bounds.Bottom)
				{
					// Kepp height, raise it up
					y = (bounds.Height - h) + bounds.Top;
				}
			}

			// Position and Size should be safe!
			// Move and Resize the form
			this.SetDesktopLocation(x, y);
			this.Size = new Size(w, h);

			// Window State
			if (savedState == FormWindowState.Maximized)
			{
				if (this.MaximizeBox)
				{
					// Optional.  Personally, I think it should always be reloaded non-maximized.
					//this.WindowState = savedState;
				}
			}
			if (savedState == FormWindowState.Minimized)
			{
				if (this.MinimizeBox)
				{
					// Don't think it's right to reload to a minimized state (confuses the user),
					// but you can enable this if you want.
					//this.WindowState = savedState;
				}
			}

		} // End LoadFormPostion

		private void frmMerge_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();
		}

		private void SaveFormPosition()
		{
			// Get current location, size, and state
			Point myLoc = this.Location;
			Size mySize = this.Size;
			FormWindowState myState = this.WindowState;
			// if minimized or maximized
			if (myState != FormWindowState.Normal)
			{
				// override with the restore location and size
				myLoc = new Point(this.RestoreBounds.X, this.RestoreBounds.Y);
				mySize = new Size(this.RestoreBounds.Width, this.RestoreBounds.Height);
			}

			// Save it for later!
			Properties.Settings.Default.Location = myLoc;
			Properties.Settings.Default.Size = mySize;
			Properties.Settings.Default.WindowState = (int)myState;
			Properties.Settings.Default.Save();
		} // End SaveFormPostion

		private void btnBrowseFirst_Click(object sender, EventArgs e)
		{
			string initDir = "q:\\dfkjalshjdfklja";
			if (lastFile1.Length > 6) initDir = Path.GetDirectoryName(lastFile1);
			if (!Directory.Exists(initDir)) initDir = utils.DefaultChannelConfigsPath;
			if (!Directory.Exists(initDir)) initDir = utils.DefaultSequencesPath;
			if (!Directory.Exists(initDir)) initDir = utils.DefaultDocumentsPath;
			if (!Directory.Exists(initDir))
			{
				initDir = utils.DefaultSequencesPath;
			}
			string initFile = "";
			if (File.Exists(lastFile1))
			{
				initFile = Path.GetFileName(lastFile1);
			}

			dlgOpenFile.Filter = "Sequence Files *.las, *.lms|*.las;*.lms|Musical Sequence Files *.lms|*.lms|Animated Sequence Files *.las|*.las";
			//dlgOpenFile.DefaultExt = "*.lms";

			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			dlgOpenFile.Title = "Select First Sequence...";
			//pnlAll.Enabled = false;
			DialogResult result = dlgOpenFile.ShowDialog();

			if (result == DialogResult.OK)
			{
				lastFile1 = dlgOpenFile.FileName;

				FileInfo fi = new FileInfo(lastFile1);
				Properties.Settings.Default.LastFile1 = lastFile1;
				Properties.Settings.Default.Save();

				txtFirstFile.Text = utils.ShortenLongPath(lastFile1, 80);
				seqOne.ReadSequenceFile(lastFile1);
				utils.TreeFillChannels(treNewChannels, seqOne, ref siNodes, false, true);
				seqNew = seqOne;
			} // end if (result = DialogResult.OK)
				//pnlAll.Enabled = true;
		} // end browse for First File

		private void btnBrowseSecond_Click(object sender, EventArgs e)
		{
			string initDir = "q:\\dfkjalshjdfklja";
			if (lastFile2.Length > 6) initDir = Path.GetDirectoryName(lastFile2);
			if (!Directory.Exists(initDir)) initDir = utils.DefaultChannelConfigsPath;
			if (!Directory.Exists(initDir)) initDir = utils.DefaultSequencesPath;
			if (!Directory.Exists(initDir)) initDir = utils.DefaultDocumentsPath;
			if (!Directory.Exists(initDir))
			{
				initDir = utils.DefaultSequencesPath;
			}
			string initFile = "";
			if (File.Exists(lastFile2))
			{
				initFile = Path.GetFileName(lastFile2);
			}

			dlgOpenFile.Filter = "Sequence Files *.las, *.lms|*.las;*.lms|Musical Sequence Files *.lms|*.lms|Animated Sequence Files *.las|*.las";
			//dlgOpenFile.DefaultExt = "*.lms";
			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			dlgOpenFile.Title = "Select Second Sequence...";
			//pnlAll.Enabled = false;
			DialogResult result = dlgOpenFile.ShowDialog();

			if (result == DialogResult.OK)
			{
				lastFile2 = dlgOpenFile.FileName;

				FileInfo fi = new FileInfo(lastFile2);
				Properties.Settings.Default.LastFile2 = lastFile2;
				Properties.Settings.Default.Save();

				txtSecondFile.Text = utils.ShortenLongPath(lastFile2, 80);

				//MergeSequences();
				DialogResult dr = GetMergeOptions();
				if (dr == DialogResult.OK)
				{
					ImBusy(true);
					seqTwo.ReadSequenceFile(lastFile2);
					MergeSequences();

					utils.TreeFillChannels(treNewChannels, seqNew, ref siNodes, false, true);
					ImBusy(false);
					btnSave.Enabled = true;
				}
			} // end if (result = DialogResult.OK)
				//pnlAll.Enabled = true;

		}

		private void ImBusy(bool busyState)
		{
			this.Enabled = !busyState;
			if (busyState)
			{
				this.Cursor = Cursors.WaitCursor;
			}
			else
			{
				this.Cursor = Cursors.Default;
			}
			this.Refresh();
				
		}

		private void MergeSequences()
		{

			mergeTracks = Properties.Settings.Default.MergeTracks;
			mergeTracksByName = Properties.Settings.Default.MergeTracksByName;
			duplicateNameAction = Properties.Settings.Default.DuplicateNameAction;
			mergeEffects = Properties.Settings.Default.MergeEffects;
			numberFormat = Properties.Settings.Default.AddNumberFormat;

			bool matched = false;
			int exIdx = 0;


			List<Map> groupMap = new List<Map>();
			List<Map> trackMap = new List<Map>();
			List<Map> timingMap = new List<Map>();
			int newGroupCount = 0;
			int newTrackCount = 0;
			int newTimingCount = 0;
			bool found = false;



			/////////////////////
			//  TIMING GRIDS  //
			///////////////////
			if (mergeGrids)
			{
				for (int g2Idx = 0; g2Idx < seqTwo.TimingGrids.Count; g2Idx++)
				{
					TimingGrid sourceGrid = seqTwo.TimingGrids[g2Idx];
					sourceGrid.timings.Sort();
					TimingGrid destGrid = null;
					// Grids treated like tracks.  Merge or Append?
					if (mergeTracks)
					{
						found = true; // Reset to default
						// Search for it, do NOT create if not found
						destGrid = (TimingGrid)seqNew.Members.Find(sourceGrid.Name, MemberType.TimingGrid, false);
						if (destGrid == null) // no match found
						{
							found = false;
							destGrid = seqNew.CreateTimingGrid(sourceGrid.Name);
						}
						else // match found!
						{
							// Check for conflicting types and warn user
							if (sourceGrid.TimingGridType == TimingGridType.FixedGrid)
							{
								if (destGrid.TimingGridType == TimingGridType.Freeform)
								{
									GridMismatchError(sourceGrid.Name);
								}
							}
							if (sourceGrid.TimingGridType == TimingGridType.Freeform)
							{
								if (destGrid.TimingGridType == TimingGridType.FixedGrid)
								{
									GridMismatchError(sourceGrid.Name);
								}
							}
							// What to do if match is found?
							if (duplicateNameAction == ACTIONkeepBoth)
							{
								destGrid = seqNew.CreateTimingGrid(sourceGrid.Name);
							}
							if (duplicateNameAction == ACTIONaddNumber)
							{
								destGrid = seqNew.CreateTimingGrid(sourceGrid.Name + " (2)");
							}
							if (duplicateNameAction == ACTIONuseSecond)
							{
								destGrid.timings.Clear();
							}
						}
					}
					else // Append
					{
						destGrid = seqNew.CreateTimingGrid(sourceGrid.Name);
						destGrid.TimingGridType = sourceGrid.TimingGridType;
					} // Enbd if merge or append

					// if not found, or any action other than keep first
					if (!found || (duplicateNameAction != ACTIONkeepFirst))
					{
						// Copy type, spacing and timings
						destGrid.TimingGridType = sourceGrid.TimingGridType;
						destGrid.spacing = sourceGrid.spacing;
						if (destGrid.TimingGridType == TimingGridType.Freeform)
						{
							destGrid.CopyTimings(sourceGrid.timings, false);
						}
					}
				} // end loop thru 2nd sequence's timing grids
			} // if Merge Grids

			///////////////
			//  TRACKS  //
			/////////////

			if (mergeTracks)
			{ 
				//foreach (Track track2 in seqTwo.Tracks)
				for (int t2Idx = 0; t2Idx < seqTwo.Tracks.Count; t2Idx++)
				{
					Track sourceTrack = seqTwo.Tracks[t2Idx];
					Track destTrack = null;
					if (mergeTracksByNumber)
					{
						// Merge by number or name?
						// If by number, do we even have that many in the destination?
						if (t2Idx < seqNew.Tracks.Count)
						{
							destTrack = seqNew.Tracks[t2Idx];
						}
						else
						{
							// Not enough, make one
							destTrack = seqNew.CreateTrack(sourceTrack.Name);
						}
					}
					if (mergeTracksByName)
					{
						found = true; // reset to default
						destTrack = (Track)seqNew.Members.Find(sourceTrack.Name, MemberType.Track, false);
						if (destTrack == null) // no matching name found
						{
							found = false;
							destTrack = seqNew.CreateTrack(sourceTrack.Name);
						}
						else // matching name found!
						{
						}
					}
					if (appendTracks)
					{
						destTrack = seqNew.CreateTrack(sourceTrack.Name);
					}
					MergeMembers(destTrack.Members, sourceTrack.Members);

					Console.WriteLine (destTrack.Name);
				}

			}
		}

		private void GridMismatchError(string gridName)
		{
			//? Not sure what to do here
			string sMsg = "Help!  This situation was not programmed for!\r\n";
			sMsg += "Both sequences contain a timing grid named '" + gridName;
			sMsg += "' but they are not the same type!\r\n";
			sMsg += "It is highly recommended that you rename one (or both) of the timing grids in one (or both) ";
			sMsg += "sequences and re-perform this merge.";
			MessageBox.Show(this, sMsg, "Grid Conflict", MessageBoxButtons.OK, MessageBoxIcon.Error);

		}


		private void OldMergeSequences()
		{
			mergeTracks = Properties.Settings.Default.MergeTracks;
			mergeTracksByName = Properties.Settings.Default.MergeTracksByName;
			duplicateNameAction = Properties.Settings.Default.DuplicateNameAction;
			numberFormat = Properties.Settings.Default.AddNumberFormat;

			bool matched = false;
			int t2Idx = 0;
			int exIdx = 0;


			List<Map> groupMap = new List<Map>();
			List<Map> trackMap = new List<Map>();
			List<Map> timingMap = new List<Map>();
			int newGroupCount = 0;
			int newTrackCount = 0;
			int newTimingCount = 0;

			/////////////////////
			//  TIMING GRIDS  //
			///////////////////

			// Make sure existing time grids are in order (they should be, but make sure anyway)
			for (int t = 0; t < seqNew.TimingGrids.Count; t++)
			{
				seqNew.TimingGrids[t].timings.Sort();
			}

			for (int timings2Idx = 0; timings2Idx < seqTwo.TimingGrids.Count; timings2Idx++)
			{
				seqTwo.TimingGrids[timings2Idx].timings.Sort();
				matched = false;
				int matchingExTimingsGridIdx = utils.UNDEFINED;
				for (int exTimingGridsIdx = 0; exTimingGridsIdx < seqNew.TimingGrids.Count; exTimingGridsIdx++)
				{
					// Compare names
					if (seqTwo.TimingGrids[timings2Idx].CompareTo(seqNew.TimingGrids[exTimingGridsIdx]) == 0)
					{
						// Matching names found
						matched = true;
						matchingExTimingsGridIdx = exTimingGridsIdx;
						exTimingGridsIdx = seqNew.TimingGrids.Count; // Break out of loop
					}
				} // end loop thru new sequence's timing grids
				if (matched)
				{
					//MergeTimingGrids(timings2Idx, matchingExTimingsGridIdx);
				}
				else
				{
					// No timing grid with matching name found, so add this one
					// Create a new timing grid and copy the name and type
					int newSaveID = seqNew.Members.HighestSaveID + 1;

					TimingGrid tGrid = seqNew.CreateTimingGrid(seqTwo.TimingGrids[timings2Idx].Name);
					//tGrid.type = seqTwo.TimingGrids[timings2Idx].type;
					tGrid.spacing = seqTwo.TimingGrids[timings2Idx].spacing;
					// Create a new array for timings, and copy them
					tGrid.CopyTimings(seqTwo.TimingGrids[timings2Idx].timings, false);
				} // end if matched, or not
			} // end loop thru 2nd sequence's timing grids

			///////////////
			//  TRACKS  //
			/////////////

			foreach (Track track2 in seqTwo.Tracks)
			//for (int tracks2Idx = 0; tracks2Idx < seqTwo.Tracks.Count; tracks2Idx++)
			{
				matched = false;
				if (mergeTracks)
				{
					int matchedExTracksIdx = utils.UNDEFINED;
					foreach (Track newTrack in seqNew.Tracks)
					//for (int exTracksIdx = 0; exTracksIdx < seqNew.Tracks.Count; exTracksIdx++)
					{
						if (mergeTracksByName)
						{
							// Merge by Name
							if (track2.CompareTo(newTrack) == 0)
							{
								matched = true;
								t2Idx = track2.Index;
								exIdx = newTrack.Index;
								//exTracksIdx = seqNew.Tracks.Count; // Break out of loop
								break;
							}
						}
						else
						{
							// Merge by Number
							if (track2.Index == newTrack.Index)
							{
								matched = true;
								t2Idx = track2.Index;
								exIdx = newTrack.Index;
								//exTracksIdx = seqNew.trackCount; // Break out of loop
								break;
							}
						}
					} // New Sequence Track Loop
					if (matched)
					{
						//MergeTracks(t2Idx, exIdx);

						newTrackCount++;
					}


				}



				///////////////////////
				//  CHANNEL GROUPS  //
				/////////////////////

				foreach (ChannelGroup group2 in seqTwo.ChannelGroups)
				//for (int groups2Idx = 0; groups2Idx < seqTwo.channelGroupCount; groups2Idx++)
				{
					int matchedExGroupsIdx = utils.UNDEFINED;
					foreach (ChannelGroup newGroup in seqNew.ChannelGroups)
					//for (int exGroupsIdx = 0; exGroupsIdx < seqNew.channelGroupCount; exGroupsIdx++)
					{
						matched = false;
						if (group2.CompareTo(newGroup) == 0)
						{
							matchedExGroupsIdx = newGroup.Index;
							//exGroupsIdx = seqNew.channelGroupCount;
							break;
						}
						//Array.Resize(ref groupMap, newGroupCount + 1);
						Map gm = new Map(group2, newGroup);
						groupMap.Add(gm);
						if (matchedExGroupsIdx == utils.UNDEFINED)
						{
							ChannelGroup group3 = seqNew.CreateChannelGroup(group2.Name);
							gm = new Map(group3, newGroup);
							groupMap.Add(gm);
						}
						else
						{

						}
						newGroupCount++;
					}



					/////////////////////
					//  RGB CHANNELS  //
					///////////////////



					///////////////////////////
					//  (Regular) CHANNELS  //
					/////////////////////////



				}



			}


		}

		private void MergeTimingGrids(TimingGrid destGrid, TimingGrid sourceGrid)
		{
			int t2Idx = 0;
			int exIdx = 0;

			if (sourceGrid.timings.Count > 0)
			{
				if (destGrid.timings.Count > 0)
				{
					t2Idx = 0;
					exIdx = 0;
					int dir = 0;

					List<int> t2Timings = sourceGrid.timings;
					List<int> exTimings = destGrid.timings;

					while ((t2Idx < t2Timings.Count) && (exIdx < exTimings.Count))
					{
						if (t2Idx == exIdx)
						{
							// Already there!  Do Nothing!
							// Increment BOTH indexes
							exIdx++;
							t2Idx++;
						}
						else
						{
							if (t2Idx > exIdx)
							{
								// Haven't reached same point yet
								// Increment new index
								exIdx++;
							}
							else
							{
								// t2Idx < exIdx
								// Didn't exist in new sequence
								// Add it
								destGrid.CopyTimings(sourceGrid.timings, true);
								// Increment BOTH indexes
								exIdx++;
								t2Idx++;
							} // haven't reached it
						} // Not already there
					} // while indexes are below counts
				}
				else
				{
					// Unlikely situation here, but nonetheless plausible
					// Sequence2 and New Sequence have timing grids with the same name
					// but in the New Sequence the grid is empty, and in Sequence2 it is not
					// Names match, but are they the same type?  (might be why it's empty)
					if (sourceGrid.TimingGridType == destGrid.TimingGridType)
					{
						// So--- Add all of them
						//destGrid.itemCount = sourceGrid.itemCount;
						//Array.Resize(ref seqTwo.TimingGrids[seqNewGridIdx].timings, sourceGrid.itemCount);
						foreach (int tc in sourceGrid.timings)
						//for (int t = 0; t < sourceGrid.itemCount; t++)
						{
							destGrid.timings.Add(tc);
							//sourceGrid.timings[t];
						}
					}
					else
					{
						//? Not sure what to do here
						string sMsg = "Help!  This situation was not programmed for!\r\n";
						sMsg += "Both sequences contain a timing grid named '" + sourceGrid.Name;
						sMsg += "' but they are not the same type!\r\n";
						sMsg += "It is highly recommended that you rename one (or both) of the timing grids in one (or both) ";
						sMsg += "sequences and re-perform this merge.";
						MessageBox.Show(this, sMsg, "Grid Conflict", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				} // end new timing grid has timings (or not)
			} // end 2nd timing grid had items
		}

		private void MergeMembers(Membership destMembers, Membership sourceMembers)
		{
			bool found = true;
			
			// May be called recursively
			//foreach (IMember sourceMember in sourceMembers) // foreach and enumerable not working, fix!
			for (int smi=0; smi<sourceMembers.Count; smi++)
			{
				IMember sourceMember = sourceMembers[smi];
				if (sourceMember.MemberType == MemberType.Channel)
				{
					Channel sourceCh = (Channel)sourceMember;
					Channel destCh = MergeChannel(sourceCh, destMembers);
				}

				if (sourceMember.MemberType == MemberType.RGBchannel)
				{
					found = true; // reset to default
					Channel destCh = null; // placeholder
					RGBchannel sourceRGB = (RGBchannel)sourceMember;
					RGBchannel destRGB = (RGBchannel)destMembers.Find(sourceRGB.Name, MemberType.RGBchannel, false);
					if (destRGB == null)
					{
						found = false;
						destRGB = seqNew.CreateRGBchannel(sourceRGB.Name);
						destMembers.Add(destRGB);

						MergeRGBchildren(sourceRGB, destRGB);
					}
					else
					{
						if (duplicateNameAction == ACTIONuseSecond)
						{
							MergeRGBchildren(sourceRGB, destRGB);
						}
						if (duplicateNameAction == ACTIONkeepBoth)
						{
							destRGB = seqNew.CreateRGBchannel(sourceRGB.Name);
							destMembers.Add(destRGB);

							MergeRGBchildren(sourceRGB, destRGB);
						}
						if (duplicateNameAction == ACTIONaddNumber)
						{
							destRGB = seqNew.CreateRGBchannel(sourceRGB.Name + " (2)");
							destMembers.Add(destRGB);

							MergeRGBchildren(sourceRGB, destRGB);
						}
					}
				}

				if (sourceMember.MemberType == MemberType.ChannelGroup)
				{
					ChannelGroup sourceGroup = (ChannelGroup)sourceMember;
					ChannelGroup destGroup = (ChannelGroup)destMembers.Find(sourceGroup.Name, MemberType.ChannelGroup, true);



					// Should only happen once
					if (destGroup.SavedIndex == 31) System.Diagnostics.Debugger.Break();
					
					
					
					
					
					//Recurse
					MergeMembers(destGroup.Members, sourceGroup.Members);
				}
			} // end loop thru 2nd Sequence's Track Items
		}

		private Channel MergeChannel(Channel sourceCh, Membership destMembers)
		{
			bool found = true; // reset to default
			Channel destCh = (Channel)destMembers.Find(sourceCh.Name, MemberType.Channel, false);

			if (destCh == null)
			{
				found = false;
				destCh = seqNew.CreateChannel(sourceCh.Name);
				destMembers.Add(destCh);
				destCh.CopyFrom(sourceCh, mergeEffects);
			}
			else
			{
				if (duplicateNameAction == ACTIONuseSecond)
				{
					//destCh = (Channel)destMember;
				}
				if (duplicateNameAction == ACTIONkeepBoth)
				{
					destCh = seqNew.CreateChannel(sourceCh.Name);
					destMembers.Add(destCh);
				}
				if (duplicateNameAction == ACTIONaddNumber)
				{
					destCh = seqNew.CreateChannel(sourceCh.Name + " (2)");
					destMembers.Add(destCh);
				}

				if (duplicateNameAction != ACTIONkeepFirst)
				{
					destCh.CopyFrom(sourceCh, mergeEffects);
				}
			}
			return destCh;
		}

		private void MergeRGBchildren(RGBchannel sourceRGB, RGBchannel destRGB)
		{
			Channel destCh = MergeChannel(sourceRGB.redChannel, seqNew.Members);
			destRGB.redChannel = destCh;
			destCh = MergeChannel(sourceRGB.grnChannel, seqNew.Members);
			destRGB.grnChannel = destCh;
			destCh = MergeChannel(sourceRGB.bluChannel, seqNew.Members);
			destRGB.bluChannel = destCh;
		}


		private void button1_Click(object sender, EventArgs e)
		{
			seqOne.WriteSequenceFile("D:\\Light-O-Rama\\2017 Betty\\rewritten_norm.las");
			seqOne.WriteSequenceFile_DisplayOrder("D:\\Light-O-Rama\\2017 Betty\\rewritten_disp.las");
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			string initDir = "";
			if (lastNewFile.Length > 5)
			{
				Path.GetDirectoryName(lastNewFile);
			}
			if (!Directory.Exists(initDir))
			{
				initDir = utils.DefaultSequencesPath;
			}
			string initFile = Path.GetFileNameWithoutExtension(lastFile1);
			initFile += " merged with ";
			initFile += Path.GetFileNameWithoutExtension(lastFile2);

			dlgSaveFile.Filter = "Musical Sequence Files *.lms|*.lms|Animated Sequence Files *.las|*.las";
			dlgSaveFile.FileName = initFile;
			dlgSaveFile.InitialDirectory = initDir;
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.Title = "Save Merged Sequences As...";
			DialogResult result = dlgSaveFile.ShowDialog();
			if (result == DialogResult.OK)
			{
				ImBusy(true);
				lastNewFile = dlgSaveFile.FileName;
				Properties.Settings.Default.LastNewFile = lastNewFile;
				Properties.Settings.Default.Save();
				seqNew.WriteSequenceFile_DisplayOrder(lastNewFile);
				ImBusy(false);
			}





		}

		private DialogResult GetMergeOptions()
		{
			frmOptions optForm = new frmOptions();
			//DialogResult result = optForm.Show(this);
			DialogResult dr = optForm.ShowDialog(this);
			//DialogResult result = optForm.DialogResult;
			if (dr == DialogResult.OK)
			{
				//MergeSequences();
			}
			return dr;

		}

		private void treNewChannels_AfterSelect(object sender, TreeViewEventArgs e)
		{

		}

		private void SetOptions()
		{
			mergeTracks = Properties.Settings.Default.MergeTracks;
			appendTracks = !mergeTracks;
			mergeGrids = Properties.Settings.Default.MergeGrids;
			if (mergeTracks)
			{
				mergeTracksByName = Properties.Settings.Default.MergeTracksByName;
				mergeTracksByNumber = !mergeTracksByName;
				mergeGroupsByName = Properties.Settings.Default.MergeGroupsByName;
				duplicateNameAction = Properties.Settings.Default.DuplicateNameAction;
			}
			else
			{
				mergeTracksByName = false;
				mergeTracksByNumber = false;
				mergeGroupsByName = false;
				duplicateNameAction = ACTIONkeepBoth;
			}
			numberFormat = Properties.Settings.Default.AddNumberFormat;

		}
	}

}
