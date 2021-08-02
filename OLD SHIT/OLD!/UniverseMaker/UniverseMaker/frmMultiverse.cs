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

namespace UniverseMaker
{
	public partial class frmMultiverse : Form
	{
		string lastFile = "";
		string[] channels;
		int[] unitIDs;
		int[] unitChs;
		int chCount = 0;
		int changesMade = 0;
		private Sequence seq = new Sequence();

		public frmMultiverse()
		{
			InitializeComponent();
		}

		private void frmMultiverse_Load(object sender, EventArgs e)
		{
			initForm();
		}
		private void initForm()
		{
			lastFile = Multiverse.Default.lastFile;
			lastFile = Multiverse.Default.lastFile;
			string ChannelList = Multiverse.Default.lastFile;
			//channels = ChannelList.Split(',');
			string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

			if (lastFile.Length > basePath.Length)
			{
				if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
				{
					txtFile.Text = lastFile.Substring(basePath.Length);
				}
				else
				{
					txtFile.Text = lastFile;
				} // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
			} // end if (lastFile.Length > basePath.Length)

			/*
			if (channels.Length > 0)
			{
					for (int loop = 0; loop < channels.Length; loop++)
					{
							if (channels[loop].Length > 0)
							{
									channelsListBox.Items.Add(channels[loop]);
							} // end if (channels[loop].Length > 0)
					} // end for (int loop = 0; loop <= channels.Length; loop++)
			} // end if (channels.Length > 0)
			 */

			loadFormSettings();

			if (File.Exists(lastFile))
			{
				skimFile(lastFile);
			}
			else
			{
				txtFile.Font = new Font(txtFile.Font, FontStyle.Italic);
				txtFile.ForeColor = Color.Red;
			}

		} // end private void InitForm()

		private void EnableControls(bool newState)
		{
			btnMake.Enabled = newState;
			fraUniverse1.Enabled = newState;
			fraUniverse2.Enabled = newState;
			fraUniverse3.Enabled = newState;
			txtFile.Enabled = newState;
			btnBrowse.Enabled = newState;
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

			openFileDialog.DefaultExt = "*.lms";
			openFileDialog.InitialDirectory = basePath;
			openFileDialog.CheckFileExists = true;
			openFileDialog.CheckPathExists = true;
			openFileDialog.Multiselect = false;
			DialogResult result = openFileDialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				lastFile = openFileDialog.FileName;
				if (lastFile.Length > basePath.Length)
				{
					if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
					{
						txtFile.Text = lastFile.Substring(basePath.Length);
					}
					else
					{
						txtFile.Text = lastFile;

						txtFile.Font = new Font(txtFile.Font, FontStyle.Regular);
						txtFile.ForeColor = DefaultForeColor;
						skimFile(lastFile);
						Multiverse.Default.lastFile = lastFile;
						Multiverse.Default.Save();

					} // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
				} // end if (lastFile.Length > basePath.Length)
			} // end if (result = DialogResult.OK)
		} // end private void BrowseButton_Click(object sender, EventArgs e)

		private void frmMultiverse_FormClosed(object sender, FormClosedEventArgs e)
		{
			saveFormSettings();

		}

		private void saveFormSettings()
		{
			byte which;

			Multiverse.Default.Track = Convert.ToInt16(txtTrack.Text);

			Multiverse.Default.StartCh1 = Convert.ToInt16(txtStartCh1.Text);
			Multiverse.Default.PixelCt1 = Convert.ToInt16(txtPixelCt1.Text);
			which = 0;
			if (optRGB1.Checked) which = 1;
			if (optGRB1.Checked) which = 2;
			Multiverse.Default.ColorOrder1 = which;
			Multiverse.Default.ReverseOrder1 = optOrderNormal1.Checked;
			Multiverse.Default.Universe1 = (byte)numUniv1.Value;
			Multiverse.Default.BaseName1 = txtBaseName1.Text;

			Multiverse.Default.SecondUniverse = chkUniverse2.Checked;
			Multiverse.Default.StartCh2 = Convert.ToInt16(txtStartCh2.Text);
			Multiverse.Default.PixelCt2 = Convert.ToInt16(txtPixelCt2.Text);
			which = 0;
			if (optRGB2.Checked) which = 1;
			if (optGRB2.Checked) which = 2;
			Multiverse.Default.ColorOrder2 = which;
			Multiverse.Default.ReverseOrder2 = optOrderReverse2.Checked;
			Multiverse.Default.Universe2 = (byte)numUniv2.Value;
			Multiverse.Default.BaseName2 = txtBaseName2.Text;

			Multiverse.Default.ThirdUniverse = chkUniverse3.Checked;
			Multiverse.Default.StartCh3 = Convert.ToInt16(txtStartCh3.Text);
			Multiverse.Default.PixelCt3 = Convert.ToInt16(txtPixelCt3.Text);
			which = 0;
			if (optRGB3.Checked) which = 1;
			if (optGRB3.Checked) which = 2;
			Multiverse.Default.ColorOrder3 = which;
			Multiverse.Default.ReverseOrder3 = optOrderNormal3.Checked;
			Multiverse.Default.Universe3 = (byte)numUniv3.Value;
			Multiverse.Default.BaseName3 = txtBaseName1.Text;


			Multiverse.Default.Save();
		}

		private void loadFormSettings()
		{
			byte which;

			txtTrack.Text = Multiverse.Default.Track.ToString();

			txtStartCh1.Text = Multiverse.Default.StartCh1.ToString();
			txtPixelCt1.Text = Multiverse.Default.PixelCt1.ToString();
			which = Multiverse.Default.ColorOrder1;
			if (which == 1) optRGB1.Checked = true;
			if (which == 2) optGRB1.Checked = true;
			optOrderNormal1.Checked = Multiverse.Default.ReverseOrder1;
			numUniv1.Value = Multiverse.Default.Universe1;
			txtBaseName1.Text = Multiverse.Default.BaseName1;

			chkUniverse2.Checked = Multiverse.Default.SecondUniverse;
			txtStartCh2.Text = Multiverse.Default.StartCh2.ToString();
			txtPixelCt2.Text = Multiverse.Default.PixelCt2.ToString();
			which = Multiverse.Default.ColorOrder2;
			if (which == 1) optRGB2.Checked = true;
			if (which == 2) optGRB2.Checked = true;
			optOrderNormal2.Checked = Multiverse.Default.ReverseOrder2;
			numUniv2.Value = Multiverse.Default.Universe2;
			txtBaseName2.Text = Multiverse.Default.BaseName2;

			chkUniverse3.Checked = Multiverse.Default.ThirdUniverse;
			txtStartCh3.Text = Multiverse.Default.StartCh3.ToString();
			txtPixelCt3.Text = Multiverse.Default.PixelCt3.ToString();
			which = Multiverse.Default.ColorOrder3;
			if (which == 1) optRGB3.Checked = true;
			if (which == 2) optGRB3.Checked = true;
			optOrderNormal3.Checked = Multiverse.Default.ReverseOrder3;
			numUniv3.Value = Multiverse.Default.Universe3;
			txtBaseName3.Text = Multiverse.Default.BaseName3;

		}

		private void skimFile(string fileName)
		{
			int savedIndex = 0;
			int biggestIndex = 0;

			StreamReader reader = new StreamReader(fileName);
			string lineIn; // line to be written out, gets modified if necessary
			while ((lineIn = reader.ReadLine()) != null)
			{
				savedIndex = getNumberValue(lineIn, "savedIndex");
				if (savedIndex > biggestIndex) biggestIndex = savedIndex;
			}
			reader.Close();
			lblIndexCt.Text = biggestIndex.ToString();
		}

		private int getNumberValue(string lineIn, string keyword)
		{
			int ret = 0;
			keyword += "=\"";
			int found = lineIn.IndexOf(keyword);
			if (found >= 0)
			{
				string part2 = lineIn.Substring(found + keyword.Length);
				found = part2.IndexOf("\"");
				if (found >= 0)
				{
					part2 = part2.Substring(0, found);
					ret = Convert.ToInt16(part2);
				}
			}
			return ret;
		}

		private string getStringValue(string lineIn, string keyword)
		{
			string ret = "";
			keyword += "=\"";
			int found = lineIn.IndexOf(keyword);
			if (found >= 0)
			{
				string part2 = lineIn.Substring(found + keyword.Length);
				found = part2.IndexOf("\"");
				if (found >= 0)
				{
					ret = part2.Substring(0, found);
				}
			}
			return ret;
		}

		private void putNumberValue(ref string lineIn, string keyword, int value)
		{
			string part1 = "";
			string part2 = "";

			keyword += "=\"";
			int found = lineIn.IndexOf(keyword);
			if (found >= 0)
			{
				part1 = lineIn.Substring(0, found);
				part2 = lineIn.Substring(found);
				found = lineIn.IndexOf("\"");
				part2 = lineIn.Substring(0, found);
				lineIn = part1 + value.ToString() + part2;
			}
			else
			{
				found = lineIn.IndexOf(">");
				part1 = lineIn.Substring(0, found);
				part2 = lineIn.Substring(found);
				found = part1.IndexOf("\\");
				if (found >= 0)
				{
					part1 += part2.Substring(found);
					part2 = part2.Substring(found);
				}
				string lineOut = part1 + " " + keyword + "=\"" + value.ToString() + "\"" + part2;
				lineIn = lineOut;
			}
		}

		private void putStringValue(ref string lineIn, string keyword, string value)
		{
			string part1 = "";
			string part2 = "";

			keyword += "=\"";
			int found = lineIn.IndexOf(keyword);
			if (found >= 0)
			{
				part1 = lineIn.Substring(0, found);
				part2 = lineIn.Substring(found);
				found = lineIn.IndexOf("\"");
				part2 = lineIn.Substring(0, found);
				lineIn = part1 + value + part2;
			}
			else
			{
				found = lineIn.IndexOf(">");
				part1 = lineIn.Substring(0, found);
				part2 = lineIn.Substring(found);
				found = part1.IndexOf("\\");
				if (found >= 0)
				{
					part1 += part2.Substring(found);
					part2 = part2.Substring(found);
				}
				string lineOut = part1 + " " + keyword + value + "\"" + part2;
				lineIn = lineOut;
			}
		}

		private void btnMake_Click(object sender, EventArgs e)
		{
			int errState = 0;
			EnableControls(false);

			seq = new Sequence();
			errState = seq.readFile(lastFile);
			if (errState > 0)
			{
				txtFile.Font = new Font(txtFile.Font, FontStyle.Italic);
				txtFile.ForeColor = Color.Red;
			}
			else
			{
				string sMsg;
				sMsg = "Original File Parse Complete!\r\n\r\n";
				sMsg += seq.lineCount.ToString() + " lines\r\n";
				sMsg += seq.channelCount.ToString() + " channels\r\n";
				sMsg += seq.rgbChannelCount.ToString() + " RGB Channels\r\n";
				sMsg += seq.effectCount.ToString() + " effects\r\n";
				sMsg += seq.groupCount.ToString() + " groups\r\n";
				sMsg += seq.groupItemCount.ToString() + " group items\r\n";
				sMsg += seq.trackCount.ToString() + " tracks\r\n";
				sMsg += seq.trackItemCount.ToString() + " track items in all tracks\r\n";
				sMsg += seq.tracks[Convert.ToInt16(txtTrack.Text) - 1].trackItemCount.ToString() + " track items in track " + txtTrack.Text;

				DialogResult mReturn;
				//mReturn = MessageBox.Show(sMsg, "File Parse Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Add Universe 1 to the file
				MakeUniverses(txtBaseName1.Text, (int)numUniv1.Value, Convert.ToUInt16(txtStartCh1.Text), Convert.ToUInt16(txtPixelCt1.Text), 1, optOrderReverse1.Checked);
				// Add Universes 2 and 3 to the file - if checked
				if (chkUniverse2.Checked) MakeUniverses(txtBaseName2.Text, (int)numUniv2.Value, Convert.ToUInt16(txtStartCh2.Text), Convert.ToUInt16(txtPixelCt2.Text), 1, optOrderReverse2.Checked);
				if (chkUniverse3.Checked) MakeUniverses(txtBaseName3.Text, (int)numUniv3.Value, Convert.ToUInt16(txtStartCh3.Text), Convert.ToUInt16(txtPixelCt3.Text), 1, optOrderReverse3.Checked);

				string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";
				//string newFile = basePath + "!CHANGED " + txtFilename.Text;
				string newFile = Path.GetDirectoryName(lastFile) + "\\" + "!CHANGED " + Path.GetFileName(lastFile);

				seq.WriteFile(newFile);
				//seq.WriteFileInDisplayOrder(newFile);

				sMsg = "New File Build Complete!\r\n\r\n";
				sMsg += seq.lineCount.ToString() + " lines\r\n";
				sMsg += seq.channelCount.ToString() + " channels\r\n";
				sMsg += seq.rgbChannelCount.ToString() + " RGB Channels\r\n";
				sMsg += seq.effectCount.ToString() + " effects\r\n";
				sMsg += seq.groupCount.ToString() + " groups\r\n";
				sMsg += seq.groupItemCount.ToString() + " group items";

				//mReturn = MessageBox.Show(sMsg, "File Build Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

				EnableControls(true);

				System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\windows\Media\chimes.wav");
				player.Play();
			}

		}

		private void MakeUniverses(string baseName, int universe, int startCh, int pixelCt, byte pixelOrder, bool reverseOrder)
		{
			channel newch;
			rgbChannel newRGBch;
			channelGroupList newGroup;
			channelGroupItem newGroupItem;
			savedIndex newSI;
			int curPixel = 1;
			int curUniverse = universe;
			int curCh = startCh;
			int track = Convert.ToInt16(txtTrack.Text) - 1;
			int nextSI = seq.highestSavedIndex + 1;
			int nextGI = seq.groupItemCount;

			// Add new channels - Pixel Count * 3 (1 each for red, green, blue)
			Array.Resize(ref seq.channels, seq.channelCount + pixelCt * 3 + 2);
			// Add new RGB Channel groups, Pixel Count
			Array.Resize(ref seq.rgbChannels, seq.rgbChannelCount + pixelCt + 2);
			Array.Resize(ref seq.trackItems, seq.trackItemCount + pixelCt + 2);
			Array.Resize(ref seq.savedIndexes, nextSI + pixelCt * 4 + 2);
			Array.Resize(ref seq.channelGroups, seq.groupCount + 3);
			Array.Resize(ref seq.channelGroupItems, seq.groupItemCount + pixelCt + 2);

			// ** Universe Group **
			newGroup = new channelGroupList();
			int ch2 = startCh + pixelCt * 3 - 2;  // Temp, used only in the name
																						// Name Format:  BaseName001 (U001.001-003)
			newGroup.name = baseName + " Pixels001-" + pixelCt.ToString("000") + " (U" + curUniverse.ToString("00") + "." + startCh.ToString("000") + "-" + ch2.ToString("000") + ")";
			newGroup.savedIndex = nextSI + pixelCt * 4;

			// *!* Double-Check correct saved index for group!
			for (int pixelNo = 1; pixelNo <= pixelCt; pixelNo++)
			{
				curPixel = pixelNo;
				if (pixelCt > 170)
				{
					if ((pixelNo > 150) && (pixelNo < 301))
					{
						curPixel -= 150;
						curUniverse = universe + 1;
						curCh = curPixel * 3 - 2;
					}
					if ((pixelNo > 300) && (pixelNo < 450))
					{
						curPixel -= 300;
						curUniverse = universe + 2;
						curCh = curPixel * 3 - 2;
					}
					if ((pixelNo > 450) && (pixelNo < 600))
					{
						curPixel -= 450;
						curUniverse = universe + 3;
						curCh = curPixel * 3 - 2;
					}
				}

				// ** RGB Channel Group **
				newRGBch = new rgbChannel();
				ch2 = curCh + 2;  // Temp, used only in the name
														// Name Format:  BaseName001 (U001.001-003)
				newRGBch.name = baseName + " Pixel" + pixelNo.ToString("000") + " (U" + curUniverse.ToString("00") + "." + curCh.ToString("000") + "-" + ch2.ToString("000") + ")";

				// ** RED Channel **
				// Create new channel for Red
				newch = new channel();
				newch.deviceType = deviceType.DMX;
				newch.network = curUniverse;
				// Name Format: BaseName001 (U001.001) (R)
				newch.name = baseName + " Pixel" + pixelNo.ToString("000") + " (U" + curUniverse.ToString("00") + "." + curCh.ToString("000") + ") (R)";
				newch.circuit = curCh;
				newch.color = 255; // Red
				newch.savedIndex = nextSI;
				newRGBch.redSavedIndex = newch.savedIndex;

				// update Saved Indexes
				newSI = new savedIndex();
				newSI.objType = tableType.channel;
				newSI.objIndex = nextSI;
				seq.savedIndexes[nextSI] = newSI;
				nextSI++;

				// Add new channel to array, and incr count
				seq.channels[seq.channelCount] = newch;
				seq.channelCount++;
				// Incr the DMX channel #
				startCh++;
				curCh++;

				// ** GREEN Channel **
				// Create new channel for Green
				newch = new channel();
				newch.deviceType = deviceType.DMX;
				newch.network = curUniverse;
				// Name Format: BaseName001 (U001.002) (G)
				newch.name = baseName + " Pixel" + pixelNo.ToString("000") + " (U" + curUniverse.ToString("00") + "." + curCh.ToString("000") + ") (G)";
				newch.circuit = curCh;
				newch.color = 65280; // Green
				newch.savedIndex = nextSI;
				newRGBch.grnSavedIndex = newch.savedIndex;

				// update Saved Indexes
				newSI = new savedIndex();
				newSI.objType = tableType.channel;
				newSI.objIndex = nextSI;
				seq.savedIndexes[nextSI] = newSI;
				nextSI++;

				// Add new channel to array, and incr count
				seq.channels[seq.channelCount] = newch;
				seq.channelCount++;
				seq.highestSavedIndex++;
				// Incr the DMX channel #
				startCh++;
				curCh++;

				// ** BLUE Channel **
				// Create new channel for Blue
				newch = new channel();
				newch.deviceType = deviceType.DMX;
				newch.network = curUniverse;
				newch.name = baseName + " Pixel" + pixelNo.ToString("000") + " (U" + curUniverse.ToString("00") + "." + curCh.ToString("000") + ") (B)";
				newch.circuit = curCh;
				newch.color = 16711680; // Blue
				newch.savedIndex = nextSI;
				newRGBch.bluSavedIndex = newch.savedIndex;

				// update Saved Indexes
				newSI = new savedIndex();
				newSI.objType = tableType.channel;
				newSI.objIndex = nextSI;
				seq.savedIndexes[nextSI] = newSI;
				nextSI++;

				// Add new channel to array, and incr count
				seq.channels[seq.channelCount] = newch;
				seq.channelCount++;
				seq.highestSavedIndex++;
				// Incr the DMX channel #
				startCh++;
				curCh++;

				// ** RGB Channel Group **
				// Add new RGB channel to array, and incr count
				newRGBch.savedIndex = nextSI;
				seq.rgbChannels[seq.rgbChannelCount] = newRGBch;
				seq.rgbChannelCount++;


				// update Saved Indexes
				newSI = new savedIndex();
				newSI.objType = tableType.rgbChannel;
				newSI.objIndex = nextSI;
				seq.savedIndexes[nextSI] = newSI;
				nextSI++;

				// Add the RGB Channel Group to the Universe Group
				seq.channelGroupItems[seq.groupItemCount] = new channelGroupItem();
				seq.channelGroupItems[seq.groupItemCount].channelGroupSavedIndex = newRGBch.savedIndex;
				seq.channelGroupItems[seq.groupItemCount].channelGroupListIndex = seq.groupCount;

				seq.groupItemCount++;
			}

			seq.channelGroups[seq.groupCount] = newGroup;
			seq.groupCount++;

			seq.trackItems[seq.trackItemCount] = new trackItem();
			seq.trackItems[seq.trackItemCount].savedIndex = newGroup.savedIndex;
			seq.trackItems[seq.trackItemCount].trackIndex = track;

			seq.trackItemCount++;
			seq.tracks[track].trackItemCount++;



			seq.highestSavedIndex = nextSI ;


		}



		private int backupFile(string fileName)
		{
			int backupSuccess = 0;
			string bak2 = fileName + ".LorBak2";
			bool bakExists = File.Exists(bak2);
			if (bakExists)
			{
				File.Delete(bak2);
			}
			string bak1 = fileName + ".LorBak";
			bakExists = File.Exists(bak1);
			if (bakExists)
			{
				File.Copy(bak1, bak2);
				File.Delete(bak1);
			}
			File.Copy(fileName, bak1);

			return backupSuccess;
		}

		private int parseFile(string fileName)
		{
			int parseState = 0;
			string tmpFile = fileName + ".tmp";

			StreamReader reader = new StreamReader(fileName);
			StreamWriter writer = new StreamWriter(tmpFile);
			string lineIn; // line read in (does not get modified)
			string lineOut; // line to be written out, gets modified if necessary
			bool lorThisCh = false; // true if currently parsing a channel that needs to be loraxed
			int pos1 = -1; // positions of certain key text in the line
			int pos2 = -1;
			int pos3 = -1;
			int pos4 = -1;
			int posE = -1;
			string unitName; // Lor Unit No. as text
			string chName; // Lor Channel No. as text
			int unitNo = 0; // Unit No. as int 1-16
			int chNo = 0; // Channel No. as int 1-16
			string startI; // startIntensity as text
			string endI; // endIntensity as text
			int startInt = 0; // startIntensity as int 0-100
			int endInt = 0; // endIntensity as int 0-100
			string effect; // name of the effect being parsed
			bool changeMade = false;
			int lineCount = 0;

			// reset
			changesMade = 0;
			// loop thru input file as long as we read valid lines
			while ((lineIn = reader.ReadLine()) != null)
			{
				// copy lineIn to lineOut.  If changes are needed, they will be made to lineOut before writing
				lineOut = lineIn;
				changeMade = false;
				lineCount++;
				// does this line mark the start of a channel?
				pos1 = lineOut.IndexOf(" unit=");
				if (pos1 > 0)
				{
					// reset this, will be set true if needed
					lorThisCh = false;
					// get the unit and channel no.s
					unitName = lineOut.Substring(pos1 + 7, 1);
					unitNo = Int32.Parse(unitName);
					pos2 = lineOut.IndexOf(" circuit=");
					posE = lineOut.IndexOf("\"", pos2 + 10);
					chName = lineOut.Substring(pos2 + 10, posE - pos2 - 10);
					chNo = Int32.Parse(chName);
					// unit and ch no. valid?
					if (unitNo > 0 && chNo > 0)
					{
						// loop thru array of channels to be loraxed
						for (int loop = 0; loop < unitIDs.Length; loop++)
						{
							// this channel in the list?
							if (unitNo == unitIDs[loop] && chNo == unitChs[loop])
							{
								// if so, set this flag to true
								lorThisCh = true;
							} // if (unitNo == unitIDs[loop] && chNo == unitChs[loop])
						} // for (int loop = 0; loop < unitIDs.Length; loop++)
					} // if (unitNo > 0 && chNo > 0)
				} // if (pos1 > 0)
				else // else this line doesn't mark the start of a channel
				{
					// if the current channel needs to be loraxed
					if (lorThisCh)
					{
						// get the effect type
						pos2 = lineOut.IndexOf("effect type=");
						if (pos2 > 0)
						{
							effect = lineOut.Substring(pos2 + 13, 7);
							// check effect type
							if (effect == "shimmer")
							{
								// shimmer needs to be changed to plain intensity
								lineOut = lineOut.Substring(0, pos2 - 1) + "intensity" + lineOut.Substring(0, pos2 + 7);
								changesMade++;
								changeMade = true;
							} // if (effect == "shimmer")
							if (effect == "twinkle")
							{
								// twinkle needs to be changed to plain intensity
								lineOut = lineOut.Substring(0, pos2 - 1) + "intensity" + lineOut.Substring(0, pos2 + 7);
								changesMade++;
								changeMade = true;
							} // if (effect == "twinkle")
							if (effect == "intensi")
							{
								//Check for Ramp-ups and Ramp-downs
								pos3 = lineOut.IndexOf("startIntensity=");
								if (pos3 > 0)
								{
									posE = lineOut.IndexOf("\"", pos3 + 16);
									startI = lineOut.Substring(pos3 + 16, posE - pos3 - 16);
									startInt = Int32.Parse(startI);
									pos4 = lineOut.IndexOf("endIntensity=");
									posE = lineOut.IndexOf("\"", pos4 + 14);
									endI = lineOut.Substring(pos4 + 14, posE - pos4 - 14);
									endInt = Int32.Parse(endI);
									if (startInt + endInt > 95)
									{
										lineOut = lineOut.Substring(0, pos3) + "intensity=\"100\"/>";
										changesMade++;
										changeMade = true;
									}
									else
									{
										lineOut = lineOut.Substring(0, pos3) + "intensity=\"0\"/>";
										changesMade++;
										changeMade = true;
									}
								}
								else // line is not a Ramp-up or Ramp-down, but we still need to check the intensity
								{
									pos4 = lineOut.IndexOf("intensity=");
									posE = lineOut.IndexOf("\"", pos4 + 11);
									endI = lineOut.Substring(pos4 + 11, posE - pos4 - 11);
									endInt = Int32.Parse(endI);
									if (endInt > 49 && endInt < 100)
									{
										lineOut = lineOut.Substring(0, pos4 + 11) + "100\"/>";
										changesMade++;
										changeMade = true;
									} // end value > 49 < 100
									if (endInt > 0 && endInt < 50)
									{
										lineOut = lineOut.Substring(0, pos4 + 11) + "0\"/>";
										changesMade++;
										changeMade = true;
									} // end value > 0 < 50

								} // end line is not a Ramp-up or Ramp-down, but we still need to check the intensity
							} // end if (effect == "intensi")
						} // end line is effect type
					} // end this channel needs to be loraxed
				} // end line is/isnt a unit start

				if (changeMade)
				{
					Console.WriteLine(lineCount.ToString() + " In: " + lineIn);
					Console.WriteLine(lineCount.ToString() + " Out:" + lineOut);
					Console.WriteLine("");

				}


				writer.WriteLine(lineOut);
			} // end loop thru input file as long as we read valid lines

			writer.Flush();
			writer.Close();
			reader.Close();

			return parseState;
		}

		private void openFileDialog_FileOk(object sender, CancelEventArgs e)
		{

		}
	}
}


