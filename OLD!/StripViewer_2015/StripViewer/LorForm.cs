using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace StripViewer
{
    public partial class lorForm : Form
    {
        string lastFile = "";
        string[] channels;
        int[] unitIDs;
        int[] unitChs;
        int chCount = 0;
        int changesMade = 0;

        public lorForm()
        {
            InitializeComponent();
        }

        private void lorForm_Load(object sender, EventArgs e)
        {
            initForm();
        }

        private void initForm()
        {
            lastFile = Properties.Settings.Default.LastFile;
            string ChannelList = Properties.Settings.Default.StartCh1;
            //channels = ChannelList.Split(',');
            string basePath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Visualizations\\";

            if (lastFile.Length > basePath.Length)
            {
                if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                {
                    fileTextBox.Text = lastFile.Substring(basePath.Length );
                }
                else
                {
                    fileTextBox.Text = lastFile;
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
            txtTrack.Text = Properties.Settings.Default.Track.ToString();
            txtStartCh1.Text = Properties.Settings.Default.StartCh1.ToString();
            txtStartCh2.Text = Properties.Settings.Default.StartCh2.ToString();
            txtStartCh3.Text = Properties.Settings.Default.StartCh3.ToString();
            txtPixelCt1.Text = Properties.Settings.Default.PixelCt1.ToString();
            txtPixelCt2.Text = Properties.Settings.Default.PixelCt2.ToString();
            txtPixelCt3.Text = Properties.Settings.Default.PixelCt3.ToString();
        } // end private void InitForm()

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Visualizations\\";

            openFileDialog.Filter = "Visualizations (*.lee)|*.lee";
            openFileDialog.DefaultExt = "*.lee";
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
                        fileTextBox.Text = lastFile.Substring(basePath.Length);
                    }
                    else
                    {
                        fileTextBox.Text = lastFile;
                    } // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                } // end if (lastFile.Length > basePath.Length)
            } // end if (result = DialogResult.OK)
        } // end private void BrowseButton_Click(object sender, EventArgs e)

        private void lorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.LastFile = lastFile;
            Properties.Settings.Default.Save();
        }

        private void lorilizeButton_Click(object sender, EventArgs e)
        {

            btnMake.Enabled = false;


            string tmpFile = lastFile + ".tmp";
            StreamWriter writer = new StreamWriter(tmpFile);
            string lineOut; // line to be written out, gets modified if necessary

            
            // NUMBER OF SAVEDINDEXES BEFORE STARTING LED STRIPS
            int beginCh = 1;
            
            int pixel = 3;
            int savedIndex = beginCh;
            int circuit = 1;
            int firstIndex = 0;
            int startPixel = 0;
            int endPixel = 25;
            int beginIndex = 3;
            int curX = 120;
            int xIncr = 50;
            int curY = 5880;
            int yIncr = 35;


            lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
            //writer.WriteLine(lineOut);
            lineOut = "<channelConfig channelConfigFileVersion=\"11\">";
            //writer.WriteLine(lineOut);
            lineOut = "	<channels>";
            //writer.WriteLine(lineOut);


            
            // Starting Value for DMX Channel / Circuit
            if (optOrderNormal1.Checked)
            {
                if (optGRB1.Checked)
                {
                    circuit = 2;
                }
                else
                {
                    circuit = 1;
                }
            }
            else
            {
                if (optGRB1.Checked)
                {
                    circuit = 449;
                }
                else
                {
                    circuit = 448;
                }
            }


            lineOut = "\t<DrawObjects>";
            writer.WriteLine(lineOut);


            // UNIVERSE 1

            for (byte grp = 0; grp < 6; grp ++)
            {
                for (byte subGrp = 1; subGrp < 26; subGrp++)
                {
                    pixel = grp * 25 + subGrp;

                    firstIndex = savedIndex;
                    lineOut = "\t\t<DrawObject ID=\"";
                    lineOut += savedIndex.ToString();
                    lineOut += "\" Name=\"1.";
                    lineOut += pixel.ToString("000");
                    lineOut += "\" BulbSize=\"3\" BulbSpacing=\"1\" Comment=\"\" BulbShape=\"1\" ZOrder=\"1\" AssignedItem=\"0\" Locked=\"False\"";
                    lineOut += " Fixture_Type=\"3\" Channel_Type=\"2\" Max_Opacity=\"0\" LED=\"True\">";
                    writer.WriteLine(lineOut);

                    lineOut = "\t\t\t<Sample Background_Color=\"0\" RGB_R=\"0\" RGB_G=\"0\" RGB_B=\"0\"/>";
                    writer.WriteLine(lineOut);

                    lineOut = "\t\t\t<AssignedChannels>";
                    writer.WriteLine(lineOut);


                    // RED CHANNEL
                    lineOut = "\t\t\t\t<Channel ID=\"1\" Name=\"Pixel 1.";
                    lineOut += pixel.ToString("000");                    
                    lineOut += " (R)\" DeviceType=\"7\" Network=\"1\" Controller=\"0\" Channel=\"";
                    lineOut += circuit.ToString();
                    lineOut += "\" Color=\"255\" Sub_Type=\"0\" Sub_Parm=\"0\"";
                    lineOut += " Multi_1=\"16777215\" Multi_2=\"16777215\" Multi_3=\"16777215\" Multi_4=\"16777215\" Multi_5=\"16777215\"/>";
                    writer.WriteLine(lineOut);
                    savedIndex ++;
                    if (optGRB1.Checked)
                    {
                        circuit--;
                    }
                    else
                    {
                        circuit++;
                    }

                    // GREEN CHANNEL
                    lineOut = "\t\t\t\t<Channel ID=\"2\" Name=\"Pixel 1.";
                    lineOut += pixel.ToString("000");
                    lineOut += " (G)\" DeviceType=\"7\" Network=\"1\" Controller=\"0\" Channel=\"";
                    lineOut += circuit.ToString();
                    lineOut += "\" Color=\"65280\" Sub_Type=\"0\" Sub_Parm=\"0\"";
                    lineOut += " Multi_1=\"16777215\" Multi_2=\"16777215\" Multi_3=\"16777215\" Multi_4=\"16777215\" Multi_5=\"16777215\"/>";
                    writer.WriteLine(lineOut);
                    //savedIndex ++;
                    circuit++;
                    if (optGRB1.Checked)
                    {
                        circuit++;
                    }
                    
                    // BLUE CHANNEL
                    lineOut = "\t\t\t\t<Channel ID=\"3\" Name=\"Pixel 1.";
                    lineOut += pixel.ToString("000");
                    lineOut += " (B)\" DeviceType=\"7\" Network=\"1\" Controller=\"0\" Channel=\"";
                    lineOut += circuit.ToString();
                    lineOut += "\" Color=\"16711680\" Sub_Type=\"0\" Sub_Parm=\"0\"";
                    lineOut += " Multi_1=\"16777215\" Multi_2=\"16777215\" Multi_3=\"16777215\" Multi_4=\"16777215\" Multi_5=\"16777215\"/>";
                    writer.WriteLine(lineOut);
                    //savedIndex ++;

                    // RGB CHANNEL GROUP
                    lineOut = "\t\t\t</AssignedChannels>";
                    writer.WriteLine(lineOut);

                    lineOut = "\t\t\t<DrawPoints>";
                    writer.WriteLine(lineOut);

                    lineOut = "\t\t\t\t<DrawPoint ID=\"1\" Type=\"16\" X=\"";
                    lineOut += curX.ToString();
                    lineOut += "\" Y=\"";
                    lineOut += curY.ToString();
                    lineOut += "\"/>";
                    writer.WriteLine(lineOut);

                    lineOut = "\t\t\t</DrawPoints>";
                    writer.WriteLine(lineOut);

                    lineOut = "\t\t</DrawObject>";
                    writer.WriteLine(lineOut);

                    curX += xIncr;
                    curY -= yIncr;

                    if (optOrderReverse1.Checked)
                    {
                        circuit -= 5;
                    }
                    else
                    {
                        circuit++;
                        if (optGRB1.Checked)
                        {
                            circuit++;
                        }
                    }


                } // end for subGrp 1-25
                startPixel = grp * 25 + 1;
                endPixel = (grp + 1) * 25;

                lineOut = "		<channelGroupList name=\"Pixels 1.";
                lineOut += startPixel.ToString("000");
                lineOut += "-";
                lineOut += endPixel.ToString("000");
                lineOut += "\" savedIndex=\"";
                lineOut += savedIndex.ToString();
                lineOut += "\">";
                //writer.WriteLine(lineOut);

                lineOut = "			<channelGroups>";
                //writer.WriteLine(lineOut);
                //savedIndex++;

                beginIndex = grp * 101 + 3 + beginCh; 
                /*
                for (int grpGrp = 0; grpGrp < 25; grpGrp++)
                {
                    lineOut = "				<channelGroup savedIndex=\"";
                    lineOut += beginIndex.ToString();
                    lineOut += "\"/>";
                    //writer.WriteLine(lineOut);

                    beginIndex += 4;
                }
                */

                lineOut = "			</channelGroups>";
                //writer.WriteLine(lineOut);
                lineOut = "		</channelGroupList>";
                //writer.WriteLine(lineOut);

            } // end for grp 1-6

            curY += yIncr;

            // * UNIVERSE 2 *
            if (chkUniverse2.Checked )
            {
                // Starting Value for DMX Channel / Circuit
                if (optOrderNormal2.Checked)
                {
                    if (optGRB2.Checked)
                    {
                        circuit = 2;
                    }
                    else
                    {
                        circuit = 1;
                    }
                }
                else
                {
                    if (optGRB2.Checked)
                    {
                        circuit = 449;
                    }
                    else
                    {
                        circuit = 448;
                    }
                }

                for (byte grp = 0; grp < 6; grp++)
                {
                    for (byte subGrp = 1; subGrp < 26; subGrp++)
                    {
                        pixel = grp * 25 + subGrp;

                        firstIndex = savedIndex;
                        lineOut = "\t\t<DrawObject ID=\"";
                        lineOut += savedIndex.ToString();
                        lineOut += "\" Name=\"2.";
                        lineOut += pixel.ToString("000");
                        lineOut += "\" BulbSize=\"3\" BulbSpacing=\"1\" Comment=\"\" BulbShape=\"1\" ZOrder=\"1\" AssignedItem=\"0\" Locked=\"False\"";
                        lineOut += " Fixture_Type=\"3\" Channel_Type=\"2\" Max_Opacity=\"0\" LED=\"True\">";
                        writer.WriteLine(lineOut);

                        lineOut = "\t\t\t<Sample Background_Color=\"0\" RGB_R=\"0\" RGB_G=\"0\" RGB_B=\"0\"/>";
                        writer.WriteLine(lineOut);

                        lineOut = "\t\t\t<AssignedChannels>";
                        writer.WriteLine(lineOut);


                        // RED CHANNEL
                        lineOut = "\t\t\t\t<Channel ID=\"1\" Name=\"Pixel 2.";
                        lineOut += pixel.ToString("000");
                        lineOut += " (R)\" DeviceType=\"7\" Network=\"2\" Controller=\"0\" Channel=\"";
                        lineOut += circuit.ToString();
                        lineOut += "\" Color=\"255\" Sub_Type=\"0\" Sub_Parm=\"0\"";
                        lineOut += " Multi_1=\"16777215\" Multi_2=\"16777215\" Multi_3=\"16777215\" Multi_4=\"16777215\" Multi_5=\"16777215\"/>";
                        writer.WriteLine(lineOut);
                        savedIndex++;
                        if (optGRB2.Checked)
                        {
                            circuit--;
                        }
                        else
                        {
                            circuit++;
                        }

                        // GREEN CHANNEL
                        lineOut = "\t\t\t\t<Channel ID=\"2\" Name=\"Pixel 2.";
                        lineOut += pixel.ToString("000");
                        lineOut += " (G)\" DeviceType=\"7\" Network=\"2\" Controller=\"0\" Channel=\"";
                        lineOut += circuit.ToString();
                        lineOut += "\" Color=\"65280\" Sub_Type=\"0\" Sub_Parm=\"0\"";
                        lineOut += " Multi_1=\"16777215\" Multi_2=\"16777215\" Multi_3=\"16777215\" Multi_4=\"16777215\" Multi_5=\"16777215\"/>";
                        writer.WriteLine(lineOut);
                        //savedIndex ++;
                        circuit++;
                        if (optGRB2.Checked)
                        {
                            circuit++;
                        }

                        // BLUE CHANNEL
                        lineOut = "\t\t\t\t<Channel ID=\"3\" Name=\"Pixel 2.";
                        lineOut += pixel.ToString("000");
                        lineOut += " (B)\" DeviceType=\"7\" Network=\"2\" Controller=\"0\" Channel=\"";
                        lineOut += circuit.ToString();
                        lineOut += "\" Color=\"16711680\" Sub_Type=\"0\" Sub_Parm=\"0\"";
                        lineOut += " Multi_1=\"16777215\" Multi_2=\"16777215\" Multi_3=\"16777215\" Multi_4=\"16777215\" Multi_5=\"16777215\"/>";
                        writer.WriteLine(lineOut);
                        //savedIndex ++;

                        // RGB CHANNEL GROUP
                        lineOut = "\t\t\t</AssignedChannels>";
                        writer.WriteLine(lineOut);

                        lineOut = "\t\t\t<DrawPoints>";
                        writer.WriteLine(lineOut);

                        lineOut = "\t\t\t\t<DrawPoint ID=\"1\" Type=\"16\" X=\"";
                        lineOut += curX.ToString();
                        lineOut += "\" Y=\"";
                        lineOut += curY.ToString();
                        lineOut += "\"/>";
                        writer.WriteLine(lineOut);

                        lineOut = "\t\t\t</DrawPoints>";
                        writer.WriteLine(lineOut);

                        lineOut = "\t\t</DrawObject>";
                        writer.WriteLine(lineOut);

                        curX += xIncr;
                        curY += yIncr;

                        if (optOrderReverse2.Checked)
                        {
                            circuit -= 3;
                        }
                        else
                        {
                            circuit++;
                            if (optGRB2.Checked)
                            {
                                circuit++;
                            }
                        }


                    } // end for subGrp 1-25
                    startPixel = grp * 25 + 1;
                    endPixel = (grp + 1) * 25;

                    lineOut = "		<channelGroupList name=\"Pixels 2.";
                    lineOut += startPixel.ToString("000");
                    lineOut += "-";
                    lineOut += endPixel.ToString("000");
                    lineOut += "\" savedIndex=\"";
                    lineOut += savedIndex.ToString();
                    lineOut += "\">";
                    //writer.WriteLine(lineOut);

                    lineOut = "			<channelGroups>";
                    //writer.WriteLine(lineOut);
                    //savedIndex++;

                    beginIndex = grp * 101 + 609 + beginCh;
                    /*
                    for (int grpGrp = 0; grpGrp < 25; grpGrp++)
                    {
                        lineOut = "				<channelGroup savedIndex=\"";
                        lineOut += beginIndex.ToString();
                        lineOut += "\"/>";
                        writer.WriteLine(lineOut);

                        beginIndex += 4;
                    }
                    */
                    lineOut = "			</channelGroups>";
                    //writer.WriteLine(lineOut);
                    lineOut = "		</channelGroupList>";
                    //writer.WriteLine(lineOut);

                } // end for grp 1-6
            }

            // * UNIVERSE 3 *
            if (chkUniverse3.Checked)
            {
                // Starting Value for DMX Channel / Circuit
                if (optOrderNormal3.Checked)
                {
                    if (optGRB3.Checked)
                    {
                        circuit = 2;
                    }
                    else
                    {
                        circuit = 1;
                    }
                }
                else
                {
                    if (optGRB3.Checked)
                    {
                        circuit = 449;
                    }
                    else
                    {
                        circuit = 448;
                    }
                }

                for (byte grp = 0; grp < 6; grp++)
                {
                    for (byte subGrp = 1; subGrp < 26; subGrp++)
                    {
                        pixel = grp * 25 + subGrp;

                        
                        // RED CHANNEL
                        firstIndex = savedIndex;
                        lineOut = "		<channel name=\"Pixel 3.";
                        lineOut += pixel.ToString("000");
                        lineOut += " (R)\" Color=\"255\" DeviceType=\"DMX Universe\" circuit=\"";
                        lineOut += circuit.ToString();
                        lineOut += "\" network=\"3\" savedIndex=\"";
                        lineOut += savedIndex.ToString();
                        lineOut += "\"/>";
                        writer.WriteLine(lineOut);
                        savedIndex++;
                        if (optGRB3.Checked)
                        {
                            circuit--;
                        }
                        else
                        {
                            circuit++;
                        }

                        // GREEN CHANNEL
                        lineOut = "		<channel name=\"Pixel 3.";
                        lineOut += pixel.ToString("000");
                        lineOut += " (G)\" Color=\"65280\" DeviceType=\"DMX Universe\" circuit=\"";
                        lineOut += circuit.ToString();
                        lineOut += "\" network=\"3\" savedIndex=\"";
                        lineOut += savedIndex.ToString();
                        lineOut += "\"/>";
                        writer.WriteLine(lineOut);
                        savedIndex++;
                        circuit++;
                        if (optGRB3.Checked)
                        {
                            circuit++;
                        }

                        // BLUE CHANNEL
                        lineOut = "		<channel name=\"Pixel 3.";
                        lineOut += pixel.ToString("000");
                        lineOut += " (B)\" Color=\"16711680\" DeviceType=\"DMX Universe\" circuit=\"";
                        lineOut += circuit.ToString();
                        lineOut += "\" network=\"3\" savedIndex=\"";
                        lineOut += savedIndex.ToString();
                        lineOut += "\"/>";
                        writer.WriteLine(lineOut);
                        savedIndex++;

                        
                        // RGB CHANNEL GROUP
                        lineOut = "		<rgbChannel name=\"Pixel 3.";
                        lineOut += pixel.ToString("000");
                        lineOut += "\" savedIndex=\"";
                        lineOut += savedIndex.ToString();
                        lineOut += "\">";
                        writer.WriteLine(lineOut);

                        lineOut = "			<channels>";
                        writer.WriteLine(lineOut);

                        lineOut = "				<channel savedIndex=\"";
                        lineOut += firstIndex.ToString();
                        lineOut += "\"/>";
                        writer.WriteLine(lineOut);

                        firstIndex++;
                        lineOut = "				<channel savedIndex=\"";
                        lineOut += firstIndex.ToString();
                        lineOut += "\"/>";
                        writer.WriteLine(lineOut);

                        firstIndex++;
                        lineOut = "				<channel savedIndex=\"";
                        lineOut += firstIndex.ToString();
                        lineOut += "\"/>";
                        writer.WriteLine(lineOut);
                        savedIndex++;

                        lineOut = "      </channels>";
                        writer.WriteLine(lineOut);

                        lineOut = "    </rgbChannel>";
                        writer.WriteLine(lineOut);

                        if (optOrderReverse3.Checked)
                        {
                            circuit -= 3;
                        }
                        else
                        {
                            circuit++;
                            if (optGRB3.Checked)
                            {
                                circuit++;
                            }
                        }


                    } // end for subGrp 1-25
                    startPixel = grp * 25 + 1;
                    endPixel = (grp + 1) * 25;

                    lineOut = "		<channelGroupList name=\"Pixels 3.";
                    lineOut += startPixel.ToString("000");
                    lineOut += "-";
                    lineOut += endPixel.ToString("000");
                    lineOut += "\" savedIndex=\"";
                    lineOut += savedIndex.ToString();
                    lineOut += "\">";
                    writer.WriteLine(lineOut);

                    lineOut = "			<channelGroups>";
                    writer.WriteLine(lineOut);
                    savedIndex++;

                    beginIndex = grp * 101 + 1215 + beginCh;
                    for (int grpGrp = 0; grpGrp < 25; grpGrp++)
                    {
                        lineOut = "				<channelGroup savedIndex=\"";
                        lineOut += beginIndex.ToString();
                        lineOut += "\"/>";
                        writer.WriteLine(lineOut);

                        beginIndex += 4;
                    }

                    lineOut = "			</channelGroups>";
                    writer.WriteLine(lineOut);
                    lineOut = "		</channelGroupList>";
                    writer.WriteLine(lineOut);

                } // end for grp 1-6
            }

            lineOut = "\t</DrawObjects>";
            writer.WriteLine(lineOut);


            writer.Flush();
            writer.Close();

            MessageBox.Show("File Completed!");
            btnMake.Enabled = true;
        
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
                    chName = lineOut.Substring(pos2 + 10, posE-pos2-10);
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
                                    startI = lineOut.Substring(pos3 + 16, posE-pos3-16);
                                    startInt = Int32.Parse(startI);
                                    pos4 = lineOut.IndexOf("endIntensity=");
                                    posE = lineOut.IndexOf("\"", pos4 + 14);
                                    endI = lineOut.Substring(pos4 + 14, posE-pos4-14);
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
                                    endI = lineOut.Substring(pos4 + 11, posE-pos4-11);
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
                    Console.WriteLine (lineCount.ToString() + " In: " + lineIn);
                    Console.WriteLine (lineCount.ToString() + " Out:" + lineOut);
                    Console.WriteLine("");
                    
                }

                
                writer.WriteLine(lineOut);
            } // end loop thru input file as long as we read valid lines

            writer.Flush();
            writer.Close();
            reader.Close();

            return parseState;
        }

        private void optGRB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optRGB1_CheckedChanged(object sender, EventArgs e)
        {

        }



    } // end public partial class lorForm : Form

} // end namespace stripMaker

