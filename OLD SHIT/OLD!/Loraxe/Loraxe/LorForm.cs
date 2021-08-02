using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace loraxe
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
            lastFile = Properties.Settings.Default.lastFile;
            string ChannelList = Properties.Settings.Default.channels;
            channels = ChannelList.Split(',');
            string basePath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

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
        } // end private void InitForm()

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

            //openFileDialog.DefaultExt = "lms";
            //openFileDialog.Filter = "Light-O-Rama Sequences (*.lms)|*.lms|Light-O-Rama Animations (*.las)|*.las|All Files (*.*)|*.*";
            //openFileDialog.FilterIndex = 1;
            //openFileDialog.FileName = "*.lms";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.InitialDirectory = @basePath;
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
            Properties.Settings.Default.lastFile = lastFile;

            if (channelsListBox.Items.Count > 0)
            {
                Array.Resize(ref channels, channelsListBox.Items.Count);
                for (int loop=0; loop < channelsListBox.Items.Count ; loop++)
                {
                    channels[loop] = channelsListBox.Items[loop].ToString();
                }
                string chnls = string.Join(",", channels);
                Properties.Settings.Default.channels = chnls;
            }
            Properties.Settings.Default.Save();
        }

        private void addChannelButton_Click(object sender, EventArgs e)
        {
            if (addChannelTextBox.Text.Length > 0)
            {
                channelsListBox.Items.Add(addChannelTextBox.Text);
                addChannelTextBox.Text = "";
                addChannelButton.Enabled = false;
            }
        }

        private void addChannelButton_TextChanged(object sender, EventArgs e)
        {
            if (addChannelTextBox.Text.Length > 0)
            {
                addChannelButton.Enabled = true;
            }

        }

        private void channelsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (channelsListBox.SelectedIndex >= 0)
            {
                deleteChannelButton.Enabled = true;
            }
        }

        private void deleteChannelButton_Click(object sender, EventArgs e)
        {
            if (channelsListBox.SelectedIndex >= 0)
            {
                channelsListBox.Items.RemoveAt(channelsListBox.SelectedIndex);
                deleteChannelButton.Enabled = false ;
            }

        }

        private void addChannelTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (addChannelTextBox.Text.Length > 0)
            {
                addChannelButton.Enabled = true;
            }

        }

        private void lorilizeButton_Click(object sender, EventArgs e)
        {
            int chGet = buildChannelArrays();
            DialogResult iRet;

            if (chGet > 0)
            {
                //iRet = MessageBox.Show("DEBUG: " + chGet.ToString() + " channels to be loraxed in the channel list.", "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int bState = backupFile(lastFile);
                if (bState == 0)
                {
                    int pState = parseFile(lastFile);

                    if (pState == 0)
                    {
                        iRet = MessageBox.Show("File " + fileTextBox.Text + " successfully loraxed for " + chGet.ToString() + " channels.  " + changesMade.ToString() + " changes made.", "Loraxing Completed Sucessfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    iRet = MessageBox.Show("Could not back up file " + fileTextBox.Text + "!", "File Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                iRet = MessageBox.Show("No valid channels to be loraxed in the channel list.  Use the format ##-## where the first ## is the Unit number, and the second ## is the Channel number on that unit.  Separate them with a dash.  Any other format is unacceptable as input", "No Channels to Loraxe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            statusLabel.Text = "Select file, then click Axe!";
        }

        private int buildChannelArrays()
        {
            chCount = 0;
            if (channelsListBox.Items.Count > 0)
            {
                string itemTxt = "";
                string chName = "";
                string unitName = "";
                int chNo = 0;
                int unitNo = 0;
                int dashPos = -1;
                
                for (int loop = 0; loop < channelsListBox.Items.Count; loop++)
                {
                    itemTxt = channelsListBox.Items[loop].ToString();
                    dashPos = itemTxt.IndexOf("-");
                    if (dashPos > 0)
                    {
                        unitName = itemTxt.Substring(0, dashPos);
                        // ToInt32 can throw FormatException or OverflowException. 
                        try
                        {
                            unitNo = Convert.ToInt32(unitName);
                        }
                        catch (FormatException e)
                        {
                            unitNo = -1;
                        }
                        catch (OverflowException e)
                        {
                            unitNo = -1;
                        }
                        finally
                        {
                            /*
                            if (unitNo < Int32.MaxValue)
                            {
                                Console.WriteLine("The new value is {0}", numVal + 1);
                            }
                            else
                            {
                                Console.WriteLine("numVal cannot be incremented beyond its current value");
                            }
                            */
                        }

                        if (itemTxt.Length > dashPos+1)
                        {
                            chName = itemTxt.Substring(dashPos+1);
                            // ToInt32 can throw FormatException or OverflowException. 
                            try
                            {
                                chNo = Convert.ToInt32(chName);
                            }
                            catch (FormatException e)
                            {
                                chNo = -1;
                            }
                            catch (OverflowException e)
                            {
                                chNo = -1;
                            }
                            finally
                            {
                                /*
                                if (unitNo < Int32.MaxValue)
                                {
                                    Console.WriteLine("The new value is {0}", numVal + 1);
                                }
                                else
                                {
                                    Console.WriteLine("numVal cannot be incremented beyond its current value");
                                }
                                */
                            } // finally
                        } // if (itemTxt.Length > dashPos)
                    } // if (dashPos > 0)
                    if (unitNo > 0 && unitNo < 17 && chNo > 0 && chNo < 17)
                    {
                        chCount++;
                        Array.Resize(ref unitIDs,chCount);
                        Array.Resize(ref unitChs,chCount);
                        unitIDs[chCount -1]=unitNo;
                        unitChs[chCount - 1] = chNo;
                    } // if (unitNo > 0 && unitNo < 17 && chNo > 0 && chNo < 17)
                } // for (int loop = 0; loop < channelsListBox.Items.Count; loop++)
            } // if (channelsListBox.Items.Count > 0)
            return chCount;
        } // private int buildChannelArrays()

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
                                lineOut = lineOut.Substring(0, pos2 +13) + "intensity" + lineOut.Substring(pos2 + 20);
                                changesMade++;
                                changeMade = true;
                            } // if (effect == "shimmer")
                            if (effect == "twinkle")
                            {
                                // twinkle needs to be changed to plain intensity
                                lineOut = lineOut.Substring(0, pos2 +13) + "intensity" + lineOut.Substring(pos2 +20);
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

                statusLabel.Text = "Line:" + lineCount.ToString() + "    " + changesMade.ToString() + " Changes made";
                //lorForm.refesh

                writer.WriteLine(lineOut);
            } // end loop thru input file as long as we read valid lines

            writer.Flush();
            writer.Close();
            reader.Close();

            File.Delete(fileName);
            File.Copy(tmpFile, fileName);
            File.Delete(tmpFile);

            return parseState;
        }



    } // end public partial class lorForm : Form

} // end namespace loraxe

