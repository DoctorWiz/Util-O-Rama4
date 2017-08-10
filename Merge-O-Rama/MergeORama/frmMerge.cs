using System;
using System.IO;
using System.Windows.Forms;
using LORUtils;
using Microsoft.Win32;

namespace MergeORama
{
	public partial class frmMerge : Form
	{
		private string basePath = "";
		private string seqFolder = "";
		private string file1 = "";
		private string file2 = "";
		private string saveFile = "";
		private Sequence seq1 = new Sequence();
		private Sequence seqNew;
		private Sequence seq2 = new Sequence();

		private class ChanInfo
		{
			public tableType chType = tableType.None;
			public int chIndex = 0;
			public int savedIndex = -1;
			public int mapCount = 0;
			//public int[] mapChIndexes;
			//public int[] mapSavedIndexes;
			public int nodeIndex = -1;
		}

		private class map
		{
			public int addIdx = -1;
			public int newIdx = -1;

		}

		int nodeIndex = -1;

		public frmMerge()
		{
			InitializeComponent();
		}

		private void frmMerge_Load(object sender, EventArgs e)
		{

		}

		private void InitForm()
		{
			basePath = GetLORFolder();

			seqFolder = Sequence.SequenceFolder;

			RestoreFormPosition();

		} // end InitForm

		private void RestoreFormPosition()
		{
			// Called when form is loaded
			//TODO: This only gets the area of the first screen in a multi-screen setup

			int itop = Properties.Settings.Default.Location.X;
			int ileft = Properties.Settings.Default.Location.Y;

			// Should get all screens and figure out if size/placement of the form is valid
			//TODO: Restore form.WindowState and if maximized use RestoreBounds()
			this.SetDesktopLocation(ileft, itop);

		} // End RestoreFormPosition

		private void frmMerge_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();
		}

		private void SaveFormPosition()
		{
			// Called with form is closed
			if (WindowState == FormWindowState.Normal)
			{
				Properties.Settings.Default.Location = Location;
				Properties.Settings.Default.Size = Size;
				Properties.Settings.Default.Minimized = false;
			}
			else
			{
				Properties.Settings.Default.Location = RestoreBounds.Location;
				Properties.Settings.Default.Size = RestoreBounds.Size;
				Properties.Settings.Default.Minimized = true;
			}
			Properties.Settings.Default.Save();

		} // End SaveFormPosition

		public string GetLORFolder()
		{
			const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
			string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string fldr = (string)Registry.GetValue(keyName, "UserDataPath", userDocs);
			return fldr;
		}

		private void FillChannels(TreeView tree, Sequence seq)
		{
			tree.Nodes.Clear();
			//ChanInfo tagInfo = new ChanInfo();
			string nodeText = "";
			ChanInfo nodeTag = new ChanInfo();
			nodeIndex = 1;

			for (int t = 0; t < seq.trackCount; t++)
			{
				int tNo = t + 1;
				nodeText = "Track " + tNo.ToString() + ":" + seq.tracks[t].name;
				TreeNode trackNode = tree.Nodes.Add(nodeText);
				nodeTag.chType = tableType.track;
				nodeTag.chIndex = t;
				nodeTag.nodeIndex = nodeIndex;
				nodeIndex++;
				trackNode.Tag = nodeTag;

				trackNode.ImageIndex = 0;
				trackNode.SelectedImageIndex = 0;

				for (int ti = 0; ti < seq.tracks[t].itemCount; ti++)
				{
					int si = seq.tracks[t].itemSavedIndexes[ti];
					if (seq.savedIndexes[si] != null)
					{
						if (seq.savedIndexes[si].objType == tableType.channelGroup)
						{
							TreeNode groupNode = AddGroup(seq, trackNode, seq.savedIndexes[si].objIndex);
						}
						if (seq.savedIndexes[si].objType == tableType.channel)
						{
							TreeNode groupNode = AddChannel(seq, trackNode, seq.savedIndexes[si].objIndex);
						}
						if (seq.savedIndexes[si].objType == tableType.rgbChannel)
						{
							TreeNode groupNode = AddRGBchannel(seq, trackNode, seq.savedIndexes[si].objIndex);
						}
					} // end not null
				} // end loop thru track items
			} // end loop thru tracks

		} // end fillOldChannels

		private TreeNode AddGroup(Sequence seq, TreeNode parentNode, int groupIndex)
		{
			string nodeText = seq.channelGroups[groupIndex].name;
			TreeNode groupNode = parentNode.Nodes.Add(nodeText);
			ChanInfo nodeTag = new ChanInfo();
			nodeTag.chType = tableType.channelGroup;
			nodeTag.chIndex = groupIndex;
			nodeTag.savedIndex = seq.channelGroups[groupIndex].savedIndex;
			nodeTag.nodeIndex = nodeIndex;
			nodeIndex++;
			groupNode.Tag = nodeTag;
			groupNode.ImageIndex = 1;
			groupNode.SelectedImageIndex = 1;

			for (int gi = 0; gi < seq.channelGroups[groupIndex].itemCount; gi++)
			{
				int si = seq.channelGroups[groupIndex].itemSavedIndexes[gi];
				if (seq.savedIndexes[si].objType == tableType.channelGroup)
				{
					TreeNode subGroupNode = AddGroup(seq, groupNode, seq.savedIndexes[si].objIndex);
				}
				if (seq.savedIndexes[si].objType == tableType.channel)
				{
					TreeNode channelNode = AddChannel(seq, groupNode, seq.savedIndexes[si].objIndex);
				}
				if (seq.savedIndexes[si].objType == tableType.rgbChannel)
				{
					TreeNode RGBchannelNode = AddRGBchannel(seq, groupNode, seq.savedIndexes[si].objIndex);
				}
			} // End loop thru items
			return groupNode;
		} // end AddGroup

		private TreeNode AddChannel(Sequence seq, TreeNode parentNode, int channelIndex)
		{
			string nodeText = seq.channels[channelIndex].name;
			TreeNode channelNode = parentNode.Nodes.Add(nodeText);
			ChanInfo nodeTag = new ChanInfo();
			nodeTag.chType = tableType.channel;
			nodeTag.chIndex = channelIndex;
			nodeTag.savedIndex = seq.channels[channelIndex].savedIndex;
			nodeTag.nodeIndex = nodeIndex;
			nodeIndex++;
			channelNode.Tag = nodeTag;
			channelNode.ImageIndex = 3;
			channelNode.SelectedImageIndex = 3;

			return channelNode;
		}

		private TreeNode AddRGBchannel(Sequence seq, TreeNode parentNode, int RGBIndex)
		{
			string nodeText = seq.rgbChannels[RGBIndex].name;
			TreeNode channelNode = parentNode.Nodes.Add(nodeText);
			ChanInfo nodeTag = new ChanInfo();
			nodeTag.chType = tableType.rgbChannel;
			nodeTag.chIndex = RGBIndex;
			nodeTag.savedIndex = seq.rgbChannels[RGBIndex].savedIndex;
			nodeTag.nodeIndex = nodeIndex;
			nodeIndex++;
			channelNode.Tag = nodeTag;
			channelNode.ImageIndex = 2;
			channelNode.SelectedImageIndex = 2;


			int ci = seq.rgbChannels[RGBIndex].redChannelIndex;
			nodeText = seq.channels[ci].name;
			TreeNode colorNode = channelNode.Nodes.Add(nodeText);
			nodeTag = new ChanInfo();
			nodeTag.chType = tableType.channel;
			nodeTag.chIndex = ci;
			nodeTag.savedIndex = seq.channels[ci].savedIndex;
			nodeTag.nodeIndex = nodeIndex;
			nodeIndex++;
			colorNode.Tag = nodeTag;
			colorNode.ImageIndex = 4;
			colorNode.SelectedImageIndex = 4;

			ci = seq.rgbChannels[RGBIndex].grnChannelIndex;
			nodeText = seq.channels[ci].name;
			colorNode = channelNode.Nodes.Add(nodeText);
			nodeTag = new ChanInfo();
			nodeTag.chType = tableType.channel;
			nodeTag.chIndex = ci;
			nodeTag.savedIndex = seq.channels[ci].savedIndex;
			nodeTag.nodeIndex = nodeIndex;
			nodeIndex++;
			colorNode.Tag = nodeTag;
			colorNode.ImageIndex = 5;
			colorNode.SelectedImageIndex = 5;

			ci = seq.rgbChannels[RGBIndex].bluChannelIndex;
			nodeText = seq.channels[ci].name;
			colorNode = channelNode.Nodes.Add(nodeText);
			nodeTag = new ChanInfo();
			nodeTag.chType = tableType.channel;
			nodeTag.chIndex = ci;
			nodeTag.savedIndex = seq.channels[ci].savedIndex;
			nodeTag.nodeIndex = nodeIndex;
			nodeIndex++;
			colorNode.Tag = nodeTag;
			colorNode.ImageIndex = 6;
			colorNode.SelectedImageIndex = 6;


			return channelNode;
		}


		private string ShortenLongPath(string longPath, int maxLen)
		{
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

		private void btnBrowseFirst_Click(object sender, EventArgs e)
		{
			string initDir = Sequence.SequenceFolder;
			string initFile = "";

			dlgOpenFile.Filter = "Musical Sequences (*.lms)|*.lms|Animated Sequences (*.las)|*.las";
			dlgOpenFile.DefaultExt = "*.lms";
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
				file1 = dlgOpenFile.FileName;

				FileInfo fi = new FileInfo(file1);
				Properties.Settings.Default.LastFile1 = file1;
				Properties.Settings.Default.Save();

				txtFirstFile.Text = ShortenLongPath(file1, 80);
				seq1.ReadSequenceFile(file1);
				FillChannels(treNewChannels, seq1);
				seqNew = seq1;
			} // end if (result = DialogResult.OK)
			//pnlAll.Enabled = true;
		} // end browse for First File

		private void btnBrowseSecond_Click(object sender, EventArgs e)
		{
			string initDir = Sequence.SequenceFolder;
			string initFile = "";

			dlgOpenFile.Filter = "Musical Sequences (*.lms)|*.lms|Animated Sequences (*.las)|*.las";
			dlgOpenFile.DefaultExt = "*.lms";
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
				file2 = dlgOpenFile.FileName;

				FileInfo fi = new FileInfo(file2);
				Properties.Settings.Default.LastFile2 = file2;
				Properties.Settings.Default.Save();

				txtSecondFile.Text = ShortenLongPath(file2, 80);
				seq2.ReadSequenceFile(file2);
				//MergeSequences();
				GetMergeOptions();
			} // end if (result = DialogResult.OK)
				//pnlAll.Enabled = true;

		}

		private void MergeSequences()
		{
			bool mergeTracks = Properties.Settings.Default.MergeTracks;
			bool mergeTracksByName = Properties.Settings.Default.MergeTracksByName;
			byte duplicateNameAction = Properties.Settings.Default.DuplicateNameAction;
			string numberFormat = Properties.Settings.Default.AddNumberFormat;

			const byte ACTIONkeepFirst = 1;
			const byte ACTIONuseSecond = 2;
			const byte ACTIONkeepBoth = 3;
			const byte ACTIONaddNumber = 4;

			bool matched = false;


			map[] groupMap = null;
			map[] trackMap = null;
			map[] timingMap = null;
			int newGroupCount = 0;
			int newTrackCount = 0;
			int newTimingCount = 0;

			/////////////////////
			//  TIMING GRIDS  //
			///////////////////

			for (int timings2Idx = 0; timings2Idx < seq2.timingGridCount; timings2Idx++)
			{
				matched = false;
				int matchingExTimingsGridIdx = -1;
				for (int exTimingGridsIdx = 0; exTimingGridsIdx < seqNew.timingGridCount; exTimingGridsIdx++)
				{
					if (seq2.timingGrids[timings2Idx].name.CompareTo(seqNew.timingGrids[exTimingGridsIdx].name) == 0)
					{
						matched = true;
						matchingExTimingsGridIdx = exTimingGridsIdx;
						exTimingGridsIdx = seqNew.timingGridCount; // Break out of loop
					}
				}
				if (matched)
				{
					if (seq2.timingGrids[timings2Idx].itemCount > 0)
					{
						if (seqNew.timingGrids[matchingExTimingsGridIdx].itemCount > 0)
						{
							int t2Idx = 0;
							int etIdx = 0;
							int dir = 0;

							long[] t2Timings = seq2.timingGrids[timings2Idx].timings;
							long[] exTimingGridsIdx = seqNew.timingGrids[matchingExTimingsGridIdx].timings;
							
							//TODO: Figure this out!
							// Compare timings
							// If timing 2 not found in new timings
							// add it

						}
						else
						{
							// Odd situation here, but plausable
							// Sequence2 and New Sequence have timing grids with the same name
							// but in the New Sequence the grid is empty, and in Sequence2 it is not
							// So--- Add all of them
							seqNew.timingGrids[matchingExTimingsGridIdx].itemCount = seq2.timingGrids[timings2Idx].itemCount;
							Array.Resize(ref seq2.timingGrids[matchingExTimingsGridIdx].timings, seq2.timingGrids[timings2Idx].itemCount);
							for (int t=0; t< seq2.timingGrids[timings2Idx].itemCount; t++)
							{
								seqNew.timingGrids[matchingExTimingsGridIdx].timings[t] = seq2.timingGrids[matchingExTimingsGridIdx].timings[t];
							}
						}
					}
				}
			}

			///////////////
			//  TRACKS  //
			/////////////

			for (int tracks2Idx = 0; tracks2Idx < seq2.trackCount; tracks2Idx++)
			{
				matched = false;
				int matchedExTracksIdx = -1;
				for (int exTracksIdx = 0; exTracksIdx < seqNew.trackCount; exTracksIdx++)
				{
					if (seq2.tracks[tracks2Idx].name.CompareTo(seqNew.tracks[exTracksIdx].name) == 0)
					{
						matchedExTracksIdx = exTracksIdx;
						exTracksIdx = seqNew.trackCount;
					}
					Array.Resize(ref trackMap, newTrackCount + 1);
					trackMap[newTrackCount].addIdx = tracks2Idx;
					if (matchedExTracksIdx == -1)
					{
						track newTrack = new track();
						newTrack.name = seq2.tracks[tracks2Idx].name;
						newTrack.totalCentiseconds = seq2.tracks[tracks2Idx].totalCentiseconds;
						int newIdx = seqNew.AddTrack(newTrack);
						trackMap[newTrackCount].newIdx = newIdx;
					}
					else
					{

					}
					newTrackCount++;
				}

				///////////////////////
				//  CHANNEL GROUPS  //
				/////////////////////

				for (int groups2Idx = 0; groups2Idx < seq2.channelGroupCount; groups2Idx++)
				{
					int matchedExGroupsIdx = -1;
					for (int exGroupsIdx = 0; exGroupsIdx < seqNew.channelGroupCount; exGroupsIdx++)
					{
						matched = false;
						if (seq2.channelGroups[groups2Idx].name.CompareTo(seqNew.tracks[exGroupsIdx].name) == 0)
						{
							matchedExGroupsIdx = exGroupsIdx;
							exGroupsIdx = seqNew.channelGroupCount;
						}
						Array.Resize(ref groupMap, newGroupCount + 1);
						groupMap[newGroupCount].addIdx = groups2Idx;
						if (matchedExGroupsIdx == -1)
						{
							channelGroup newGroup = new channelGroup();
							newGroup.name = seq2.tracks[groups2Idx].name;
							int newIdx = seqNew.AddChannelGroup(newGroup);
							groupMap[newGroupCount].newIdx = newIdx;
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



			/*
			public int MergeSequenceFile(string existingFileName)
			{
				int errState = 0;
				StreamReader reader = new StreamReader(existingFileName);
				string lineIn; // line read in (does not get modified)
				int pos1 = -1; // positions of certain key text in the line
				int lineCount = 0;


				// * PASS 1 - COUNT OBJECTS
				while ((lineIn = reader.ReadLine()) != null)
				{
					lineCount++;
					// Regular Channels
					pos1 = lineIn.IndexOf(Sequence.STFLD + Sequence.TABLEchannel + Sequence.SPC + Sequence.FIELDname);
					if (pos1 > 0)
					{
						channel chan = new channel();
						savedIndex si = new savedIndex();
						chan.name = Sequence.cleanName(Sequence.getKeyWord(lineIn, Sequence.FIELDname));
						chan.color = Sequence.getKeyValue(lineIn, Sequence.FIELDcolor);
						chan.centiseconds = Sequence.getKeyValue(lineIn, Sequence.FIELDcentiseconds);
						chan.deviceType = Sequence.enumDevice(Sequence.getKeyWord(lineIn, Sequence.FIELDdeviceType));
						chan.unit = Sequence.getKeyValue(lineIn, Sequence.FIELDunit);
						chan.network = Sequence.getKeyValue(lineIn, Sequence.FIELDnetwork);
						chan.circuit = Sequence.getKeyValue(lineIn, Sequence.FIELDcircuit);
						chan.savedIndex = Sequence.getKeyValue(lineIn, Sequence.FIELDsavedIndex);

						seqNew.AddChannel(chan);

						si.objType = tableType.channel;
						si.objIndex = seqNew.channelCount - 1;

						seqNew.AddSavedIndex(si, chan.savedIndex);

						pos1 = lineIn.IndexOf(Sequence.ENDFLD);
						if (pos1 < 0)
						{
							//lineIn = reader.ReadLine();
							//lineCount++;
							lineIn = reader.ReadLine();
							lineCount++;
							pos1 = lineIn.IndexOf(Sequence.TABLEeffect + Sequence.SPC + Sequence.FIELDtype);
							if (pos1 > 0)
							{
								effect ef = new effect();
								ef.type = Sequence.enumEffect(Sequence.getKeyWord(lineIn, Sequence.FIELDtype));
								ef.startCentisecond = Sequence.getKeyValue(lineIn, Sequence.FIELDstartCentisecond);
								ef.endCentisecond = Sequence.getKeyValue(lineIn, Sequence.FIELDendCentisecond);
								ef.intensity = Sequence.getKeyValue(lineIn, Sequence.SPC + Sequence.FIELDintensity);
								ef.startIntensity = Sequence.getKeyValue(lineIn, Sequence.FIELDstartIntensity);
								ef.endIntensity = Sequence.getKeyValue(lineIn, Sequence.FIELDendIntensity);
								chan.AddEffect(ef);

								lineIn = reader.ReadLine();
								lineCount++;
								pos1 = lineIn.IndexOf(Sequence.TABLEeffect + Sequence.SPC + Sequence.FIELDtype);
							}
						}
					}
					else // Not a regular channel
					{
						// RGB Channels
						pos1 = lineIn.IndexOf(Sequence.STFLD + Sequence.TABLErgbChannel + Sequence.SPC);
						if (pos1 > 0)
						{

							rgbChannel rgbc = new rgbChannel();
							savedIndex si = new savedIndex();
							rgbc.name = Sequence.cleanName(Sequence.getKeyWord(lineIn, Sequence.FIELDname));
							rgbc.totalCentiseconds = Sequence.getKeyValue(lineIn, Sequence.FIELDtotalCentiseconds);
							rgbc.savedIndex = Sequence.getKeyValue(lineIn, Sequence.FIELDsavedIndex);
							lineIn = reader.ReadLine();
							lineCount++;
							lineIn = reader.ReadLine();
							lineCount++;
							rgbc.redSavedIndex = Sequence.getKeyValue(lineIn, Sequence.FIELDsavedIndex);
							rgbc.redChannelIndex = seqNew.savedIndexes[rgbc.redSavedIndex].objIndex;
							lineIn = reader.ReadLine();
							lineCount++;
							rgbc.grnSavedIndex = Sequence.getKeyValue(lineIn, Sequence.FIELDsavedIndex);
							rgbc.grnChannelIndex = seqNew.savedIndexes[rgbc.grnSavedIndex].objIndex;
							lineIn = reader.ReadLine();
							lineCount++;
							rgbc.bluSavedIndex = Sequence.getKeyValue(lineIn, Sequence.FIELDsavedIndex);
							rgbc.bluChannelIndex = seqNew.savedIndexes[rgbc.bluSavedIndex].objIndex;

							seqNew.AddRGBChannel(rgbc);

							si.objType = tableType.rgbChannel;
							si.objIndex = seqNew.rgbChannelCount - 1;

							seqNew.AddSavedIndex(si, rgbc.savedIndex);
						}
						else  // Not an RGB Channel
						{
							// Channel Groups
							pos1 = lineIn.IndexOf(Sequence.STFLD + Sequence.TABLEchannelGroupList + Sequence.SPC);
							if (pos1 > 0)
							{
								channelGroup changl = new channelGroup();
								savedIndex si = new savedIndex();
								changl.name = Sequence.cleanName(Sequence.getKeyWord(lineIn, Sequence.FIELDname));
								changl.savedIndex = Sequence.getKeyValue(lineIn, Sequence.FIELDsavedIndex);
								//changl.channelIndex = curChannel;

								changl.channelSavedIndex = seqNew.curSavedIndex;
								changl.totalCentiseconds = Sequence.getKeyValue(lineIn, Sequence.FIELDtotalCentiseconds);
								//channelGroups[curChannelGroupList] = changl;

								seqNew.AddChannelGroup(changl);

								si.objType = tableType.channelGroup;
								si.objIndex = seqNew.channelGroupCount - 1;

								seqNew.AddSavedIndex(si, changl.savedIndex);

								pos1 = lineIn.IndexOf(Sequence.ENDFLD);
								if (pos1 < 0)
								{
									lineIn = reader.ReadLine();
									lineCount++;
									lineIn = reader.ReadLine();
									lineCount++;
									pos1 = lineIn.IndexOf(Sequence.TABLEchannelGroup + Sequence.SPC + Sequence.FIELDsavedIndex);
									while (pos1 > 0)
									{
										int isl = Sequence.getKeyValue(lineIn, Sequence.FIELDsavedIndex);
										changl.AddItem(isl);

										lineIn = reader.ReadLine();
										lineCount++;
										pos1 = lineIn.IndexOf(Sequence.TABLEchannelGroup + Sequence.SPC + Sequence.FIELDsavedIndex);
									}
								}
							}
							else // Not a channel group
							{
								// Timing Grids
								pos1 = lineIn.IndexOf(Sequence.STFLD + Sequence.TABLEtimingGrid + Sequence.SPC);
								if (pos1 > 0)
								{
									timingGrid tg = new timingGrid();
									tg.name = Sequence.cleanName(Sequence.getKeyWord(lineIn, Sequence.FIELDname));
									tg.type = Sequence.enumGridType(Sequence.getKeyWord(lineIn, Sequence.FIELDtype));
									tg.saveID = Sequence.getKeyValue(lineIn, Sequence.FIELDsaveID);
									tg.spacing = Sequence.getKeyValue(lineIn, Sequence.FIELDspacing);

									seqNew.AddTimingGrid(tg);

									if (tg.type == timingGridType.freeform)
									{
										lineIn = reader.ReadLine();
										lineCount++;
										pos1 = lineIn.IndexOf(Sequence.TABLEtiming + Sequence.SPC + Sequence.FIELDcentisecond);
										while (pos1 > 0)
										{
											int gpos = Sequence.getKeyValue(lineIn, Sequence.FIELDcentisecond);
											tg.AddTiming(gpos);
											lineIn = reader.ReadLine();
											lineCount++;
											pos1 = lineIn.IndexOf(Sequence.TABLEtiming + Sequence.SPC + Sequence.FIELDcentisecond);
										}
									}
								}
								else // Not a timing grid
								{
									// Tracks
									pos1 = lineIn.IndexOf(Sequence.STFLD + Sequence.TABLEtrack + Sequence.SPC);
									if (pos1 > 0)
									{
										track trk = new track();
										trk.name = Sequence.cleanName(Sequence.getKeyWord(lineIn, Sequence.FIELDname));
										trk.totalCentiseconds = Sequence.getKeyValue(lineIn, Sequence.FIELDtotalCentiseconds);
										seqNew.totalCentiseconds = trk.totalCentiseconds;
										trk.timingGridSaveID = Sequence.getKeyValue(lineIn, Sequence.TABLEtimingGrid);

										seqNew.AddTrack(trk);

										pos1 = lineIn.IndexOf(Sequence.ENDFLD);
										if (pos1 < 0)
										{
											lineIn = reader.ReadLine();
											lineCount++;
											lineIn = reader.ReadLine();
											lineCount++;
											pos1 = lineIn.IndexOf(Sequence.TABLEchannel + Sequence.SPC + Sequence.FIELDsavedIndex);
											while (pos1 > 0)
											{
												int isi = Sequence.getKeyValue(lineIn, Sequence.FIELDsavedIndex);
												trk.AddItem(isi);

												lineIn = reader.ReadLine();
												lineCount++;
												pos1 = lineIn.IndexOf(Sequence.TABLEchannel + Sequence.SPC + Sequence.FIELDsavedIndex);
											}
										}
									} // end if a track
									else // not a track
									{
										// does this line mark the start of a channel?
										pos1 = lineIn.IndexOf("xml version=");
										if (pos1 > 0)
										{
											seqNew.xmlInfo = lineIn;
										}
										pos1 = lineIn.IndexOf("saveFileVersion=");
										if (pos1 > 0)
										{
											seqNew.sequenceInfo = lineIn;
										}
										pos1 = lineIn.IndexOf("animation rows=");
										if (pos1 > 0)
										{
											seqNew.animationInfo = lineIn;
										}
									} // end if a track, or not
								} // end if a timing grid, or not
							} // end if a channel group, or not
						} // end if a RGB channel, or not
					} // end if a regular channel, or not
				} // while lines remain
				reader.Close();
				//totalCentiseconds = tracks[0].totalCentiseconds;

				if (errState <= 0)
				{
					FileName = existingFileName;
					string sMsg = summary();
					MessageBox.Show(sMsg, "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				return errState;
			} // end ReadSequenceFile
			*/
		}

		private void button1_Click(object sender, EventArgs e)
		{
			seq1.WriteSequenceFile("D:\\Light-O-Rama\\2017 Betty\\rewritten_norm.las");
			seq1.WriteFileInDisplayOrder("D:\\Light-O-Rama\\2017 Betty\\rewritten_disp.las");
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			GetMergeOptions();
		}

		void GetMergeOptions()
		{
			frmOptions optForm = new frmOptions();
			//DialogResult result = optForm.Show(this);
			optForm.Show();
			DialogResult result = optForm.DialogResult;
			if (result == DialogResult.OK)
			{
				//MergeSequences();
			}

		}

	}
}
