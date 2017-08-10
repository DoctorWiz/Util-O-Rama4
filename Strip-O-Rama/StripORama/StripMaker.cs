using LORUtils;
using System;
using System.IO;
using System.Windows.Forms;
using System.Media;
using LORUtils;

namespace StripORama
{
	public partial class frmStripMaker : Form
	{
		private string lastFile = "";
		private string[] channels;
		private int[] unitIDs;
		private int[] unitChs;
		private int chCount = 0;
		private int changesMade = 0;
		private Sequence seq;

		private string stripName = "";
		private int stripNum = 1;
		private int stripCount = 150;
		private int stripStart = 1;
		private int stripEnd = 150;
		private int chOrder = 1;
		private int groupSize = 25;
		private int chIncr = 1;
		private int universeNum;
		private int pixelNum = 1;
		


		public frmStripMaker()
		{
			InitializeComponent();
		}

		private void lorForm_Load(object sender, EventArgs e)
		{
			initForm();
		}

		private void initForm()
		{
			lastFile = Properties.Settings.Default.lastFile;
			
			string ChannelList = Properties.Settings.Default.lastFile;
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
		} // end private void InitForm()

		private void EnableControls(bool newState)
		{
			btnMake.Enabled = newState;
			fraStrip1.Enabled = newState;
			fraStrip2.Enabled = newState;
			fraStrip3.Enabled = newState;
			txtFile.Enabled = newState;
			btnBrowse.Enabled = newState;
		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";
			FileInfo fi = new FileInfo(lastFile);

			openFileDialog.Filter = "Sequences (*.lms,las)|*.lms;*.las|Just Musical Sequences (*.lms)|*.lms|Just Animated Sequences (*.las)|*.las";
			openFileDialog.DefaultExt = "*.lms;*.las";
			//openFileDialog.InitialDirectory = basePath;
			openFileDialog.InitialDirectory = fi.DirectoryName;
			openFileDialog.FileName = fi.Name;
			
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
					} // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
				} // end if (lastFile.Length > basePath.Length)
			} // end if (result = DialogResult.OK)
		} // end private void BrowseButton_Click(object sender, EventArgs e)

		private void lorForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			saveFormSettings();
		}

		private void saveFormSettings()
		{
			byte which;

			Properties.Settings.Default.FormLeft = this.Left;
			Properties.Settings.Default.FormTop = this.Top;

			Properties.Settings.Default.Track = Int16.Parse(txtTrack.Text);

			Properties.Settings.Default.Make1 = chkMake1.Checked;
			Properties.Settings.Default.Universe1 = Convert.ToInt16(numUniverse1.Value);
			Properties.Settings.Default.StripName1 = txtStripName1.Text;
			Properties.Settings.Default.GroupSize1 = Int16.Parse(cboGroupSize1.Text);
			Properties.Settings.Default.StartCh1 = Int16.Parse(txtStartCh1.Text);
			Properties.Settings.Default.PixelCt1 = Int16.Parse(txtPixelCt1.Text);
			which = 0;
			if (optRGB1.Checked) which = 1;
			if (optGRB1.Checked) which = 2;
			Properties.Settings.Default.ColorOrder1 = which;
			Properties.Settings.Default.Reverse1 = optReverse1.Checked;

			Properties.Settings.Default.Make2 = chkMake2.Checked;
			Properties.Settings.Default.Universe2 = Convert.ToInt16(numUniverse2.Value);
			Properties.Settings.Default.StripName2 = txtStripName2.Text;
			Properties.Settings.Default.GroupSize2 = Int16.Parse(cboGroupSize2.Text);
			Properties.Settings.Default.StartCh2 = Int16.Parse(txtStartCh2.Text);
			Properties.Settings.Default.PixelCt2 = Int16.Parse(txtPixelCt2.Text);
			which = 0;
			if (optRGB2.Checked) which = 1;
			if (optGRB2.Checked) which = 2;
			Properties.Settings.Default.ColorOrder2 = which;
			Properties.Settings.Default.Reverse2 = optReverse2.Checked;

			Properties.Settings.Default.Make3 = chkMake3.Checked;
			Properties.Settings.Default.Universe3 = Convert.ToInt16(numUniverse3.Value);
			Properties.Settings.Default.StripName3 = txtStripName3.Text;
			Properties.Settings.Default.GroupSize3 = Int16.Parse(cboGroupSize3.Text);
			Properties.Settings.Default.StartCh3 = Int16.Parse(txtStartCh3.Text);
			Properties.Settings.Default.PixelCt3 = Int16.Parse(txtPixelCt3.Text);
			which = 0;
			if (optRGB3.Checked) which = 1;
			if (optGRB3.Checked) which = 2;
			Properties.Settings.Default.ColorOrder3 = which;
			Properties.Settings.Default.Reverse3 = optForward3.Checked;

			Properties.Settings.Default.Make4 = chkMake4.Checked;
			Properties.Settings.Default.Universe4 = Convert.ToInt16(numUniverse4.Value);
			Properties.Settings.Default.StripName4 = txtStripName4.Text;
			Properties.Settings.Default.GroupSize4 = Int16.Parse(cboGroupSize4.Text);
			Properties.Settings.Default.StartCh4 = Int16.Parse(txtStartCh4.Text);
			Properties.Settings.Default.PixelCt4 = Int16.Parse(txtPixelCt4.Text);
			which = 0;
			if (optRGB4.Checked) which = 1;
			if (optGRB4.Checked) which = 2;
			Properties.Settings.Default.ColorOrder4 = which;
			Properties.Settings.Default.Reverse4 = optForward4.Checked;

			Properties.Settings.Default.lastFile = txtFile.Text;

			Properties.Settings.Default.Save();
		}

		private void loadFormSettings()
		{
			int which;

			this.Left = Properties.Settings.Default.FormLeft;
			this.Top = Properties.Settings.Default.FormTop;

			txtTrack.Text = Properties.Settings.Default.Track.ToString();

			chkMake1.Checked = Properties.Settings.Default.Make1;
			numUniverse1.Value = Properties.Settings.Default.Universe1;
			txtStripName1.Text = Properties.Settings.Default.StripName1;
			SetGroupSize(cboGroupSize1, Properties.Settings.Default.GroupSize1);
			txtStartCh1.Text = Properties.Settings.Default.StartCh1.ToString();
			txtPixelCt1.Text = Properties.Settings.Default.PixelCt1.ToString();
			which = Properties.Settings.Default.ColorOrder1;
			if (which == 1) optRGB1.Checked = true;
			if (which == 2) optGRB1.Checked = true;
			optReverse1.Checked = Properties.Settings.Default.Reverse1;

			chkMake2.Checked = Properties.Settings.Default.Make2;
			numUniverse2.Value = Properties.Settings.Default.Universe2;
			txtStripName2.Text = Properties.Settings.Default.StripName2;
			SetGroupSize(cboGroupSize2, Properties.Settings.Default.GroupSize2);
			txtStartCh2.Text = Properties.Settings.Default.StartCh2.ToString();
			txtPixelCt2.Text = Properties.Settings.Default.PixelCt2.ToString();
			which = Properties.Settings.Default.ColorOrder2;
			if (which == 1) optRGB2.Checked = true;
			if (which == 2) optGRB2.Checked = true;
			optReverse2.Checked = Properties.Settings.Default.Reverse2;

			chkMake3.Checked = Properties.Settings.Default.Make3;
			numUniverse3.Value = Properties.Settings.Default.Universe3;
			txtStripName3.Text = Properties.Settings.Default.StripName3;
			SetGroupSize(cboGroupSize3, Properties.Settings.Default.GroupSize3);
			txtStartCh3.Text = Properties.Settings.Default.StartCh3.ToString();
			txtPixelCt3.Text = Properties.Settings.Default.PixelCt3.ToString();
			which = Properties.Settings.Default.ColorOrder3;
			if (which == 1) optRGB3.Checked = true;
			if (which == 2) optGRB3.Checked = true;
			optReverse3.Checked = Properties.Settings.Default.Reverse3;

			chkMake4.Checked = Properties.Settings.Default.Make4;
			numUniverse4.Value = Properties.Settings.Default.Universe4;
			txtStripName4.Text = Properties.Settings.Default.StripName4;
			SetGroupSize(cboGroupSize4, Properties.Settings.Default.GroupSize4);
			txtStartCh4.Text = Properties.Settings.Default.StartCh4.ToString();
			txtPixelCt4.Text = Properties.Settings.Default.PixelCt4.ToString();
			which = Properties.Settings.Default.ColorOrder4;
			if (which == 1) optRGB4.Checked = true;
			if (which == 2) optGRB4.Checked = true;
			optReverse4.Checked = Properties.Settings.Default.Reverse4;
		}

		private void SetGroupSize(ComboBox GrpSizeCbo, int size)
		{
			GrpSizeCbo.SelectedIndex = 0;
			for (int q = 0; q < GrpSizeCbo.Items.Count; q++)
			{
				if (Convert.ToInt16(GrpSizeCbo.Items[q]) == size)
				{
					GrpSizeCbo.SelectedIndex = q;
				}
			}
		}

		private void skimFile(string fileName)
		{
			int savedIndex = 0;
			int biggestIndex = 0;

			StreamReader reader = new StreamReader(fileName);
			string lineIn; // line to be written out, gets modified if necessary
			while ((lineIn = reader.ReadLine()) != null)
			{
				savedIndex = getNumberValue(lineIn, "SavedIndex");
				if (savedIndex > biggestIndex) biggestIndex = savedIndex;
			}
			reader.Close();
		}

		private int getNumberValue(string lineIn, string keyword)
		{
			keyword += "=\"";
			int found = lineIn.IndexOf(keyword);
			string part2 = lineIn.Substring(found);
			found = lineIn.IndexOf("\"");
			part2 = lineIn.Substring(0, found);
			int ret = Convert.ToInt16(lineIn);
			return ret;
		}

		private string getStringValue(string lineIn, string keyword)
		{
			keyword += "=\"";
			int found = lineIn.IndexOf(keyword);
			string part2 = lineIn.Substring(found);
			found = lineIn.IndexOf("\"");
			string ret = lineIn.Substring(0, found);
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


		private void EnableAll(bool newState)
		{
			fraStrip1.Enabled = newState;
			fraStrip2.Enabled = newState;
			fraStrip3.Enabled = newState;
			fraStrip4.Enabled = newState;
			btnMake.Enabled = newState;
			txtFile.Enabled = newState;
			btnBrowse.Enabled = newState;
		}

		private void btnMake_Click(object sender, EventArgs e)
		{
			EnableAll(false);
			pixelNum = 1;

			seq = new Sequence();
			if (File.Exists(lastFile))
			{
				seq.ReadSequenceFile(lastFile);
			}

			// Strip 1
			if (chkMake1.Checked)
			{
				stripName = txtStripName1.Text;
				universeNum = Convert.ToInt16(numUniverse1.Value);
				stripCount = Int16.Parse(txtPixelCt1.Text);
				stripNum = 1;
				pixelNum = 1;
				if (optReverse1.Checked)
				{
					stripStart = Int16.Parse(txtPixelCt1.Text);
					stripEnd = Int16.Parse(txtStartCh1.Text);
					chIncr = -1;
				}
				else
				{
					stripStart = Int16.Parse(txtStartCh1.Text);
					stripEnd = Int16.Parse(txtPixelCt1.Text);
					chIncr = 1;
				}
				if (optRGB1.Checked) chOrder = 1;
				if (optGRB1.Checked) chOrder = 2;
				groupSize = Int16.Parse(cboGroupSize1.Text);
				AddStrip();
			}
			
			// Strip 2
			if (chkMake2.Checked)
			{
				stripName = txtStripName2.Text;
				universeNum = Convert.ToInt16(numUniverse2.Value);
				stripCount = Int16.Parse(txtPixelCt2.Text);
				stripNum = 2;
				if (optReverse2.Checked)
				{
					stripStart = Int16.Parse(txtPixelCt2.Text);
					stripEnd = Int16.Parse(txtStartCh2.Text);
					chIncr = -1;
				}
				else
				{
					stripStart = Int16.Parse(txtStartCh2.Text);
					stripEnd = Int16.Parse(txtPixelCt2.Text);
					chIncr = 1;
				}
				if (optRGB2.Checked) chOrder = 1;
				if (optGRB2.Checked) chOrder = 2;
				groupSize = Int16.Parse(cboGroupSize2.Text);
				AddStrip();
			}

			// Strip 3
			if (chkMake3.Checked)
			{
				stripName = txtStripName3.Text;
				universeNum = Convert.ToInt16(numUniverse3.Value);
				stripCount = Int16.Parse(txtPixelCt3.Text);
				stripNum = 3;
				if (optReverse3.Checked)
				{
					stripStart = Int16.Parse(txtPixelCt3.Text);
					stripEnd = Int16.Parse(txtStartCh3.Text);
					chIncr = -1;
				}
				else
				{
					stripStart = Int16.Parse(txtStartCh3.Text);
					stripEnd = Int16.Parse(txtPixelCt3.Text);
					chIncr = 1;
				}
				if (optRGB3.Checked) chOrder = 1;
				if (optGRB3.Checked) chOrder = 2;
				groupSize = Int16.Parse(cboGroupSize3.Text);
				AddStrip();
			}

			// Strip 4
			if (chkMake4.Checked)
			{
				stripName = txtStripName4.Text;
				universeNum = Convert.ToInt16(numUniverse4.Value);
				stripCount = Int16.Parse(txtPixelCt4.Text);
				stripNum = 4;
				if (optReverse4.Checked)
				{
					stripStart = Int16.Parse(txtPixelCt4.Text);
					stripEnd = Int16.Parse(txtStartCh4.Text);
					chIncr = -1;
				}
				else
				{
					stripStart = Int16.Parse(txtStartCh4.Text);
					stripEnd = Int16.Parse(txtPixelCt4.Text);
					chIncr = 1;
				}
				if (optRGB4.Checked) chOrder = 1;
				if (optGRB4.Checked) chOrder = 2;
				groupSize = Int16.Parse(cboGroupSize4.Text);
				AddStrip();
			}

			FileInfo oFilenfo = new FileInfo(lastFile);
			string newFile = oFilenfo.Directory + "\\" + JustName(lastFile) + " + Strips" + oFilenfo.Extension;

			seq.WriteSequenceFile(newFile);
			//seq.WriteFileInDisplayOrder(newFile);

			EnableAll(true);
			SystemSounds.Exclamation.Play();

		} // end btnMake_Click()
 
		public void AddStrip()
		{
			int pixID = stripStart;
			int nextSI = seq.highestSavedIndex + 1;
			int groupMember = 1;
			int uch = 1;
			if (chIncr < 0) uch = stripCount * 3 - 2;
			int trkNum = Int16.Parse(txtTrack.Text)-1;
			string chName;
			channel redChannel;
			channel grnChannel;
			channel bluChannel;
			channelGroup stripGroup = new channelGroup();
			int dmxCount = stripCount * 3;
			if (chIncr < 0)
			{
				//Reverse
				int bb = stripEnd * 3 - 2;
				//chName = stripName + " Pixels " + stripStart.ToString("000") + "-" + stripEnd.ToString("000") + " (U" + universeNum.ToString() + "." + bb.ToString("000") + "-" + dmxCount.ToString("000") + ")";
				chName = stripName + " Pixels " + pixelNum.ToString("000") + "-" + (pixelNum + stripCount-1).ToString("000");
				chName += " / S" + stripNum.ToString() + "." + stripStart.ToString("000") + "-" + stripEnd.ToString("000");
				chName += " / U" + universeNum.ToString() + "." + bb.ToString("000") + "-" + dmxCount.ToString("000") + ")";
			}
			else
			{
				// Forward
				//chName = stripName + " Pixels " + stripStart.ToString("000") + "-" + stripCount.ToString("000") + " (U" + universeNum.ToString() + "." + stripStart.ToString("000") + "-" + dmxCount.ToString("000") + ")";
				chName = stripName + " Pixels " + pixelNum.ToString("000") + "-" + (pixelNum + stripCount - 1).ToString("000");
				chName += " / S" + stripNum.ToString() + "." + stripStart.ToString("000") + "-" + stripEnd.ToString("000");
				chName += " / U" + universeNum.ToString() + "." + stripStart.ToString("000") + "-" + dmxCount.ToString("000") + ")";
			}

			stripGroup.name = chName;
			stripGroup.totalCentiseconds = seq.totalCentiseconds;
			channelGroup pixelGroup = new channelGroup();
			pixelGroup.totalCentiseconds = seq.totalCentiseconds;
			rgbChannel RGB_Channel;
			int chx;
			int groupFirstPixel = stripStart;
			int grpCounter = 0;
			int stripPixel = stripStart;
			int groupFirstDMXchannel = 1;
			int groupLastDMXchannel = -1;
			int RGBFirstDMXchannel = 1;
			int RGBLastDMXchannel = -1;
			while ((stripPixel > 0) && (stripPixel <= stripCount))
			{
				redChannel = new channel();
				grnChannel = new channel();
				bluChannel = new channel();
				RGB_Channel = new rgbChannel();

				//redChannel.deviceType = deviceType.DMX;
				redChannel.output.network = universeNum;
				redChannel.color = 255;
				redChannel.centiseconds = seq.totalCentiseconds;
				//grnChannel.deviceType = deviceType.DMX;
				grnChannel.output.network = universeNum;
				grnChannel.color = 65280;
				grnChannel.centiseconds = seq.totalCentiseconds;
				//bluChannel.deviceType = deviceType.DMX;
				bluChannel.output.network = universeNum;
				bluChannel.color = 16711680;
				bluChannel.centiseconds = seq.totalCentiseconds;
				RGB_Channel.totalCentiseconds = seq.totalCentiseconds;

				if (chOrder == 1) // RGB Order
				{
					//chName = stripName + " Pixel " + stripPixel.ToString("000") + "(R) (U" + universeNum.ToString() + "." + uch.ToString("000") + ")";
					chName = "Pixel " + pixelNum.ToString("000");
					chName += " (R) / S" + stripNum.ToString() + "." + stripPixel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					redChannel.name = chName;
					redChannel.output.circuit = uch;
					RGB_Channel.redChannelIndex = seq.channelCount;
					seq.AddChannel(redChannel);
					RGBFirstDMXchannel = uch;
					uch++;

					//chName = stripName + " Pixel " + stripPixel.ToString("000") + "(G) (U" + universeNum.ToString() + "." + uch.ToString("000") + ")";
					chName = "Pixel " + pixelNum.ToString("000");
					chName += " (G) / S" + stripNum.ToString() + "." + stripPixel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					grnChannel.name = chName;
					grnChannel.output.circuit = uch;
					RGB_Channel.bluChannelIndex = seq.channelCount;
					seq.AddChannel(grnChannel);
					uch++;

					chName = "Pixel " + pixelNum.ToString("000");
					chName += " (B) / S" + stripNum.ToString() + "." + stripPixel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					bluChannel.name = chName;
					bluChannel.output.circuit = uch;
					RGB_Channel.grnChannelIndex = seq.channelCount;
					seq.AddChannel(bluChannel);
					RGBLastDMXchannel = uch;
					uch++;
				}

				if (chOrder == 2) // GRB Order
				{
					//chName = stripName + " Pixel " + stripPixel.ToString("000") + "(G) (U" + universeNum.ToString() + "." + uch.ToString("000") + ")";
					chName = "Pixel " + pixelNum.ToString("000");
					chName += " (G) / S" + stripNum.ToString() + "." + stripPixel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					grnChannel.name = chName;
					grnChannel.output.circuit = uch;
					RGB_Channel.grnChannelIndex = seq.channelCount;
					seq.AddChannel(grnChannel);
					RGBFirstDMXchannel = uch;
					uch++;

					//chName = stripName + " Pixel " + stripPixel.ToString("000") + "(R) (U" + universeNum.ToString() + "." + uch.ToString("000") + ")";
					chName = "Pixel " + pixelNum.ToString("000");
					chName += " (R) / S" + stripNum.ToString() + "." + stripPixel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					redChannel.name = chName;
					redChannel.output.circuit = uch;
					RGB_Channel.redChannelIndex = seq.channelCount;
					seq.AddChannel(redChannel);
					uch++;

					//chName = stripName + " Pixel " + stripPixel.ToString("000") + "(B) (U" + universeNum.ToString() + "." + uch.ToString("000") + ")";
					chName = "Pixel " + pixelNum.ToString("000");
					chName += " (B) / S" + stripNum.ToString() + "." + stripPixel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					bluChannel.name = chName;
					bluChannel.output.circuit = uch;
					RGB_Channel.bluChannelIndex = seq.channelCount;
					seq.AddChannel(bluChannel);
					RGBLastDMXchannel = uch;
					uch++;
				}

				groupLastDMXchannel = uch - 1;
				chx = uch - 2;
				//chName = stripName + " Pixel " + stripPixel.ToString("000") + " (U" + universeNum.ToString() + "." + RGBFirstDMXchannel.ToString("000") + "-" + RGBLastDMXchannel.ToString("000") + ")";
				chName = "Pixel " + pixelNum.ToString("000");
				chName += " / S" + stripNum.ToString() + "." + stripPixel.ToString("000");
				chName += " / U" + universeNum.ToString() + "." + RGBFirstDMXchannel.ToString("000") + "-" + RGBLastDMXchannel.ToString("000");
				RGB_Channel.name = chName;
				RGB_Channel.redSavedIndex = redChannel.savedIndex;
				RGB_Channel.grnSavedIndex = grnChannel.savedIndex;
				RGB_Channel.bluSavedIndex = bluChannel.savedIndex;
				seq.AddRGBChannel(RGB_Channel);
				pixelNum++;

				if (groupSize > 0)
				{
					grpCounter++;
					pixelGroup.channelIndex = seq.rgbChannelCount;
					pixelGroup.AddItem(RGB_Channel.savedIndex);
					if (grpCounter == groupSize)
					{
						int groupLastPixel = groupFirstPixel + groupSize;
						//int uchStart = stripPixel * 3 - 2;
						if (chIncr < 0)
						{
							// Reverse
							groupFirstDMXchannel = stripPixel * 3 - 2;
							groupLastDMXchannel = groupFirstPixel * 3;
						}
						else
						{
							// Forward
							// OK as-is
						}


						//chName = stripName + " Pixels " + groupFirstPixel.ToString("000") + "-" + stripPixel.ToString("000");
						chName = stripName + " Pixels " + (pixelNum - groupSize).ToString("000") + "-" + (pixelNum - 1).ToString("000");
						chName += " / S" + stripNum.ToString() + "." + groupFirstPixel.ToString("000") + "-" + stripPixel.ToString("000");
						chName += " / U" + universeNum.ToString() + "." + groupFirstDMXchannel.ToString("000") + "-" + groupLastDMXchannel.ToString("000") + ")";
						//chName = " Pixels " + (pixelNum - groupSize).ToString("000") + "-" + pixelNum.ToString("000") + " / S" + stripNum.ToString() + "." + groupFirstPixel.ToString("000") + "-" + stripPixel.ToString("000") + " / U" + universeNum.ToString() + "." + groupFirstDMXchannel.ToString("000") + "-" + groupLastDMXchannel.ToString("000");
						pixelGroup.name = chName;
						pixelGroup.totalCentiseconds = seq.totalCentiseconds;
						seq.AddChannelGroup(pixelGroup);
						stripGroup.AddItem(pixelGroup.savedIndex);
						pixelGroup = new channelGroup();
						grpCounter = 0; // Reset
						groupFirstDMXchannel = uch;
						groupFirstPixel = stripPixel + chIncr;
					
					}
				}
				else
				{
					stripGroup.AddItem(RGB_Channel.savedIndex);
				}

				stripPixel += chIncr;
				if (chIncr < 0) uch -= 6;
			} // end while pixel # in range
			seq.AddChannelGroup(stripGroup);
			seq.tracks[trkNum].AddItem(stripGroup.savedIndex);

		} // end void AddStrip();

		public string JustName(string fileName)
		{
			// Returns just the name portion of a filename
			// without the path, without any /'s
			// and without the extension and it's associated .
			string nameOnly = "";
			int i = fileName.IndexOf("\\");
			if (i < 0)
			{
				nameOnly = fileName;
			}
			else
			{
				string[] parts = fileName.Split('\\');
				nameOnly = parts[parts.Length-1 ];
			}
			i = nameOnly.LastIndexOf(".");
			if (i > 0)
			{
				nameOnly = nameOnly.Substring(0, i );
			}


			return nameOnly;
		}

	} // end public partial class lorForm : Form
} // end namespace StripORama