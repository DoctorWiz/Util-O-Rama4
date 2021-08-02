using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Text.RegularExpressions;
using LORUtils;

namespace PixelDust
{

    
    
    public partial class frmDust : Form
    {

        private const string SPC = " ";
        private const string FIELDEQ = "=\"";
        private const string ENDQT = "\"";
        private const string ENDFLD = "/>";
        private const string TABLEpresetSet = "presetSet";
        private const string TABLEcolorChange = "colorChange";
        private const string FIELDfromName = "fromName";
        private const string FIELDtoName = "toName";
        private const string FIELDfromR = "fromR";
        private const string FIELDfromG = "fromG";
        private const string FIELDfromB = "fromB";
        private const string FIELDtoR = "toR";
        private const string FIELDtoG = "toG";
        private const string FIELDtoB = "toB";
        private const string FIELDname = "name";

        private bool doneInit = false;
        private string lastFile = "";
        private bool colorChanged = false;
        private bool changesMade = false;
        private Sequence seq = new Sequence();
        bool showChanges = false;
        bool fadeFlag = true;
        int channelIndex = 0;
        int redChannelIndex;  // The Array Index of the Red Channel of the current RGB Channel
        int grnChannelIndex;  // Note: Array Index, NOT 'SavedIndex'
        int bluChannelIndex;
        int rgbChannelIndex = 0;
        frmPixels fp = new frmPixels();
        long startCentisecond = 0;
        long endCentisecond = 0;
        int colorCount = 0;
        LORcolor[] LorColors = new LORcolor[1];
        int[] order = new int[1];


        // Create temp new pixel array which will hold RGB values for each and every centisecond
        pixel[] Pixels = new pixel[1];


 
        int presetSetCount = 0;
        int colorChangeCount = 0;


        private effect[] NEWeffects;
        int newEffectCount = 0;

        public frmDust()
        {
            InitializeComponent();
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

            dlgFile.Filter = "Musical Sequences (*.lms)|*.lms|Animated Sequences (*.las)|*.las";
            dlgFile.DefaultExt = "*.lms";
            dlgFile.InitialDirectory = basePath;
            dlgFile.CheckFileExists = true;
            dlgFile.CheckPathExists = true;
            dlgFile.Multiselect = false;
            DialogResult result = dlgFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                lastFile = dlgFile.FileName;
                if (lastFile.Substring(1, 2) != ":\\")
                {
                    lastFile = basePath + "\\" + lastFile;
                }

                Properties.Settings.Default.lastFile = lastFile;
                Properties.Settings.Default.Save();

                if (lastFile.Length > basePath.Length)
                {
                    if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                    {
                        txtFilename.Text = lastFile.Substring(basePath.Length);
                    }
                    else
                    {
                        txtFilename.Text = lastFile;
                    } // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                } // end if (lastFile.Length > basePath.Length)

                if (chkAutoRead.Checked)
                {
                    readSequence(lastFile);
                }
                else
                {
                    btnRead.Enabled = true;
                }

            } // end if (result = DialogResult.OK)
        }

        private void frmColors_Load(object sender, EventArgs e)
        {
            initForm();
        }

        private void initForm()
        {
            
            lastFile = Properties.Settings.Default.lastFile;
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

            if (lastFile.Length > basePath.Length)
            {
                if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                {
                    txtFilename.Text = lastFile.Substring(basePath.Length);
                }
                else
                {
                    txtFilename.Text = lastFile;
                } // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
            } // end if (lastFile.Length > basePath.Length)

            readColorPresets();
            fillColorPresets();
            cboFade.SelectedIndex = 0;


            order = chooseOrder(50);

        } // end initForm


        private void saveSettings()
        {

            Properties.Settings.Default.lastFile = lastFile;
            
            Properties.Settings.Default.Save();
        
        } // end saveSettings


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
        } // end backupFile

        
        private int GetCurrentColors()
        {
            int errState = 0;
            int redFirstEffectIndex = -1;   // The Array Index of the first Effect in this Red Channel
            int redFinalEffectIndex = -1;     // The Array Index of the first Effect PAST this Red Channel
            int grnFirstEffectIndex = -1;
            int grnFinalEffectIndex = -1;
            int bluFirstEffectIndex = -1;
            int bluFinalEffectIndex = -1;
            int redIntensity; // The Red Intensity, right this very Centisecond
            int grnIntensity;
            int bluIntensity;
            int redEndIntensity = -1;
            int grnEndIntensity = -1;
            int bluEndIntensity = -1;
            long redEndCentisecond = -1;
            long grnEndCentisecond = -1;
            long bluEndCentisecond = -1;
            int redEffectIndex = -1;    // the Array Index of the Effect for the Red Channeol for the current Centisecond
            int grnEffectIndex = -1;
            int bluEffectIndex = -1;
            effect blankEffect = new effect();  // Used if there is NO effects for a channel, and also when past the last effect for the channel
            //blankEffect.startIntensity = 0;
            //blankEffect.endIntensity = 0;
            blankEffect.intensity = 0;
            effect redEffect = blankEffect;   // Current Red Effect structure for the current Centisecond
            effect grnEffect = blankEffect;
            effect bluEffect = blankEffect;
            effectType redEffectType = effectType.intensity;
            effectType grnEffectType = effectType.intensity;
            effectType bluEffectType = effectType.intensity;
            int effectIndex = 0;
            //long lastEndCenti = -1;
            pixel p = new pixel();
            string sMsg = "";
                
            
            // Loop thru ALL channels
            while (channelIndex < seq.channelCount)
            {
                // Is this channel an RGB channel, or a regular one?
                if (seq.rgbChannels[rgbChannelIndex].redSavedIndex != seq.channels[channelIndex].savedIndex &&
                    seq.rgbChannels[rgbChannelIndex].grnSavedIndex != seq.channels[channelIndex].savedIndex &&
                    seq.rgbChannels[rgbChannelIndex].bluSavedIndex != seq.channels[channelIndex].savedIndex)
                {
                    // NOT an RGB, this is a regular
                    // Are there any effects for this channel?
                    effectIndex = seq.channels[channelIndex].firstEffectIndex;
                    if (effectIndex >= 0)
                    {
                        while (seq.effects[effectIndex].channelIndex == channelIndex)
                        {
                            // Copy them to the NEW array
                            Array.Resize(ref NEWeffects, newEffectCount + 1);
                            NEWeffects[newEffectCount] = seq.effects[effectIndex];
                            newEffectCount++;
                            effectIndex++;
                        } // end While effect
                    }
                }
                else
                {
                    // This is an RGB Channel
                    // Get the Array Index of the Red, Green, and Blue channels in this RGB Channel
                    redChannelIndex = seq.rgbChannels[rgbChannelIndex].redChannelIndex;
                    grnChannelIndex = seq.rgbChannels[rgbChannelIndex].grnChannelIndex;
                    bluChannelIndex = seq.rgbChannels[rgbChannelIndex].bluChannelIndex;
                    // Reset
                    redFirstEffectIndex = -1;
                    redFinalEffectIndex = -1;
                    grnFirstEffectIndex = -1;
                    grnFinalEffectIndex = -1;
                    bluFirstEffectIndex = -1;
                    bluFinalEffectIndex = -1;

                    // Loop thru all effects, looking for the ones for these red, green and blue channels


                    redFirstEffectIndex = seq.channels[redChannelIndex].firstEffectIndex;
                    redEffectIndex = redFirstEffectIndex;
                    if (redEffectIndex >= 0)
                    {
                        redEffect = seq.effects[redEffectIndex];
                        redEffectType = redEffect.type;
                        redEndIntensity = redEffect.endIntensity;
                        redEndCentisecond = redEffect.endCentisecond;
                    }

                    grnFirstEffectIndex = seq.channels[grnChannelIndex].firstEffectIndex;
                    grnEffectIndex = grnFirstEffectIndex;
                    if (grnEffectIndex >= 0)
                    {
                        grnEffect = seq.effects[grnEffectIndex];
                        grnEffectType = grnEffect.type;
                        grnEndIntensity = grnEffect.endIntensity;
                        grnEndCentisecond = grnEffect.endCentisecond;
                    }

 
                    bluFirstEffectIndex = seq.channels[bluChannelIndex].firstEffectIndex;
                    bluEffectIndex = bluFirstEffectIndex;
                    if (bluEffectIndex >= 0)
                    {
                        bluEffect = seq.effects[bluEffectIndex];
                        bluEffectType = bluEffect.type;
                        bluEndIntensity = bluEffect.endIntensity;
                        bluEndCentisecond = bluEffect.endCentisecond;
                    }

                    // Reset to initial values of BLACK
                    redIntensity = 0;
                    grnIntensity = 0;
                    bluIntensity = 0;



                    // Count thru all centiseconds to the end of the sequence
                    for (long centiSec = 0; centiSec < seq.totalCentiseconds; centiSec++)
                    {
                        // Create a new pixel for this centisecond
                        p = new pixel();

                        if (redEffectIndex >= 0)
                        {
                            if (redEffect.channelIndex == redChannelIndex)
                            {
                                // Check current centisecond against the end of the current Red effect
                                if (redEffect.endCentisecond == centiSec)
                                {
                                    // got it! potential change coming up!

                                    // check end intensity (Ramps)
                                    if (redEffect.endIntensity > -1)
                                    {
                                        //redIntensity = redEffect.endIntensity;
                                        redEndIntensity = redEffect.endIntensity;
                                    }
                                    else
                                    {
                                        // not a ramp, set intensity to zero
                                        redIntensity = 0;
                                    }
                                    redEndIntensity = -2;
                                    redEndCentisecond = -2;
                                    redEffectType = effectType.None;

                                    redEffectIndex++;
                                    redEffect = seq.effects[redEffectIndex];
                                } // end if endCentisecond

                                if (redEffect.startCentisecond == centiSec)
                                {
                                    // current centisecond matches the start centisecond of the current effect
                                    if (redEffect.startIntensity > -1)
                                    {
                                        redIntensity = redEffect.startIntensity;
                                    }
                                    if (redEffect.intensity > -1)
                                    {
                                        redIntensity = redEffect.intensity;
                                    }
                                    redEndIntensity = redEffect.endIntensity;
                                    redEffectType = redEffect.type;
                                    redEndIntensity = redEffect.endIntensity;
                                    redEndCentisecond = redEffect.endCentisecond;

                                } // end  if startCentisecond, endcentiSecond
                            } // end if channel indexes match
                        } // end if we have any red effects

                        //sMsg = "Pixel[" + centiSec.ToString() + "].redIntensity = " + redIntensity.ToString();
                        //Debug.Print(sMsg);

                        // Now lets go thru the same thing for the Green
                        // See comments above for Red, same thing happening here

                        if (grnEffectIndex >= 0)
                        {
                            if (grnEffect.channelIndex == grnChannelIndex)
                            {
                                // Check current centisecond against the end of the current grn effect
                                if (grnEffect.endCentisecond == centiSec)
                                {
                                    // got it! potential change coming up!

                                    // check end intensity (Ramps)
                                    if (grnEffect.endIntensity > -1)
                                    {
                                        //grnIntensity = grnEffect.endIntensity;
                                        grnEndIntensity = grnEffect.endIntensity;
                                    }
                                    else
                                    {
                                        // not a ramp, set intensity to zero
                                        grnIntensity = 0;
                                    }
                                    grnEndIntensity = -2;
                                    grnEndCentisecond = -2;
                                    grnEffectType = effectType.None;

                                    grnEffectIndex++;
                                    grnEffect = seq.effects[grnEffectIndex];
                                }
                                if (grnEffect.startCentisecond == centiSec)
                                {
                                    // current centisecond matches the start centisecond of the current effect
                                    if (grnEffect.startIntensity > -1)
                                    {
                                        grnIntensity = grnEffect.startIntensity;
                                    }
                                    if (grnEffect.intensity > -1)
                                    {
                                        grnIntensity = grnEffect.intensity;
                                    }
                                    grnEndIntensity = grnEffect.endIntensity;
                                    grnEffectType = grnEffect.type;
                                    grnEndIntensity = grnEffect.endIntensity;
                                    grnEndCentisecond = grnEffect.endCentisecond;

                                } // end startCentisecond, endcentiSecond
                            } // end if channel Indexes match
                        } // end if we have any green effects

                        // And now finally lets go thru the same thing for the Blue
                        // See comments above for grn, same thing happening here

                        if (bluEffectIndex >= 0)
                        {
                            if (bluEffect.channelIndex == bluChannelIndex)
                            {
                                // Check current centisecond against the end of the current Blue effect
                                if (bluEffect.endCentisecond == centiSec)
                                {
                                    // got it! potential change coming up!

                                    // check end intensity (Ramps)
                                    if (bluEffect.endIntensity > -1)
                                    {
                                        //bluIntensity = bluEffect.endIntensity;
                                        bluEndIntensity = bluEffect.endIntensity;
                                    }
                                    else
                                    {
                                        // not a ramp, set intensity to zero
                                        bluIntensity = 0;
                                    }
                                    bluEndIntensity = -2;
                                    bluEndCentisecond = -2;
                                    bluEffectType = effectType.None;

                                    bluEffectIndex++;
                                    bluEffect = seq.effects[bluEffectIndex];
                                }
                                if (bluEffect.startCentisecond == centiSec)
                                {
                                    // current centisecond matches the start centisecond of the current effect
                                    if (bluEffect.startIntensity > -1)
                                    {
                                        bluIntensity = bluEffect.startIntensity;
                                    }
                                    if (bluEffect.intensity > -1)
                                    {
                                        bluIntensity = bluEffect.intensity;
                                    }
                                    bluEndIntensity = bluEffect.endIntensity;
                                    bluEffectType = bluEffect.type;
                                    bluEndIntensity = bluEffect.endIntensity;
                                    bluEndCentisecond = bluEffect.endCentisecond;

                                } // end startCentisecond, endcentiSecond
                            } // end if channel indexes match
                        } // end if we have any blue effects
                    
                    
                        // Set the red, green, and blue intensities on the current temp pixel
                        p.redIntensity = redIntensity;
                        p.grnIntensity = grnIntensity;
                        p.bluIntensity = bluIntensity;
                        p.redEndIntensity = redEndIntensity;
                        p.grnEndIntensity = grnEndIntensity;
                        p.bluEndIntensity = bluEndIntensity;
                        p.redEndCentisecond = redEndCentisecond;
                        p.grnEndCentisecond = grnEndCentisecond;
                        p.bluEndCentisecond = bluEndCentisecond;
                        p.redEffectType = redEffectType;
                        p.grnEffectType = grnEffectType;
                        p.bluEffectType = bluEffectType;



                        // save this pixel for this centisecond in the array
                        Pixels[centiSec] = p;

                        //sMsg = "Channel[" + redChannelIndex.ToString() + "] ";
                        //sMsg += "Pixel[" + centiSec.ToString() + "].redIntensity = " + p.redIntensity.ToString();
                        //Debug.Print(sMsg);

                    }
                    Pixels[seq.totalCentiseconds] = new pixel();
                    Pixels[seq.totalCentiseconds + 1] = new pixel();
                    // Done building pixel array!!!
                    // We now have an array with the RGB values of this rgbChannel for each and every centisecond of the sequence

                    if (showChanges)
                    {
                        fp = new frmPixels();
                        for (int i = 0; i < seq.totalCentiseconds; i++)
                        {
                            fp.setPixel(i, 0, intensity(Pixels[i].redIntensity), intensity(Pixels[i].grnIntensity), intensity(Pixels[i].bluIntensity));
                        }
                    }

                    ChangeColors();
                    BuildNewEffects();

                } // End rgbChannel loop

                if (bluFinalEffectIndex > 0)
                {
                    // pick up where we left off
                    effectIndex = bluFinalEffectIndex;
                }
                else if (grnFinalEffectIndex > 0)
                {
                    effectIndex = grnFinalEffectIndex;
                }
                else if (redFinalEffectIndex > 0)
                {
                    effectIndex = redFinalEffectIndex;
                }
                updateProgress(channelIndex, 0);
                channelIndex++;
                effectIndex++;


            } // end While Loop thru ALL channels

            Array.Resize(ref NEWeffects, newEffectCount + 1);
            NEWeffects[newEffectCount] = new effect();

            seq.effects = NEWeffects;
            seq.effectCount = newEffectCount;
            changesMade = true;

            return errState;

        }  // end ChangeColors


        private void ChangeColors()
        {
            pixel p = new pixel();


            if (fadeFlag)
            {

                CryptoRandom rnd = new CryptoRandom();
                int rx = 0;
                int pct = 0;
                char nx = seq.channels[channelIndex].name[7];
                rx = rnd.Next(1, (int)nx);

                // Now, lets go thru that array, and look for the colors we are supposed to be changing FROM
                for (long centiSec = 0; centiSec < seq.totalCentiseconds; centiSec += 5)
                {
                    rx = rnd.Next(0, 32);
                    pct = Convert.ToInt16(centiSec * 1000 / seq.totalCentiseconds);
                    if (rx == 11)
                    {
                        for (int l2 = 0; l2 < 5; l2++)
                        {
                            Pixels[centiSec + l2].redIntensity = 100;
                            Pixels[centiSec + l2].grnIntensity = 0;
                            Pixels[centiSec + l2].bluIntensity = 0;
                            rx = rnd.Next(0, rx);
                        }
                    }

                    else if (rx == 7)
                    {
                        for (int l2 = 0; l2 < 5; l2++)
                        {
                            Pixels[centiSec + l2].redIntensity = 0;
                            Pixels[centiSec + l2].grnIntensity = 100;
                            Pixels[centiSec + l2].bluIntensity = 0;
                        }
                        rx = rnd.Next(0, 777);
                    }

                    else
                    {
                        for (int l2 = 0; l2 < 5; l2++)
                        {
                            Pixels[centiSec + l2].redIntensity = 0;
                            Pixels[centiSec + l2].grnIntensity = 0;
                            Pixels[centiSec + l2].bluIntensity = 0;
                        }
                        rx = rnd.Next(0, 777);
                    }


                    rx = rnd.Next(0, 11);



                }
            }

            if (showChanges)
            {
                for (int i = 0; i < seq.totalCentiseconds; i++)
                {
                    fp.setPixel(i, 1, intensity(Pixels[i].redIntensity), intensity(Pixels[i].grnIntensity), intensity(Pixels[i].bluIntensity));
                }
                fp.ShowDialog();
            } // end ChangeColors
        }

        private void BuildNewEffects()
        {
            // ** BUILD NEW EFFECTS **
            int redIntensity; // The Red Intensity, right this very Centisecond
            int grnIntensity;
            int bluIntensity;
            int replaceCount = 0;
            long centisecond = 0;
            pixel p = new pixel();
            int effectIndex = 0;

            // ** RED STARTS HERE **

            // now create NEW effects lists for the Red channel
            effect[] replaceEffs = new effect[1];
            effect newE = new effect();
            replaceCount = 0;
            // Reset to initial values of BLACK
            redIntensity = 0;
            centisecond = 0;
            // Is the Red turned on at the very first centisecond (0)?
            p = Pixels[0];
            if (p.redIntensity > 0)
            {
                // If New Intensity then Create New Effect
                redIntensity = p.redIntensity;
                newE.channelIndex = redChannelIndex;
                newE.savedIndex = seq.channels[redChannelIndex].savedIndex;
                newE.type = GetBestEffectType(p.redEffectType, p.grnEffectType, p.bluEffectType);
                newE.startCentisecond = 0;
                if (p.redEndIntensity > -1)
                {
                    // for fades
                    newE.endIntensity = p.redEndIntensity;
                    newE.startIntensity = p.redIntensity;
                }
                else
                {
                    // for steady
                    newE.intensity = p.redIntensity;
                }

                centisecond = 1;
                // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                while (centisecond < seq.totalCentiseconds + 1)
                {
                    // get pixel for current centisecond
                    p = Pixels[centisecond];
                    // did the anything change?
                    if (!p.redEquals(Pixels[centisecond - 1]))
                    {
                        // save end centisecond
                        newE.endCentisecond = centisecond;
                        // add this to the array of replacement effects, incr counter
                        Array.Resize(ref replaceEffs, replaceCount + 1);
                        replaceEffs[replaceCount] = newE;
                        replaceCount++;
                        // get ready to continue for loop above  (4 levels <-) by:
                        //    creating a new effect,
                        //       and backing the centisecond up by 1 (in case the next effect needs to start immediately after this one)
                        newE = new effect();
                        centisecond--;
                        break; // from while centisecond loop
                    }  // end if (p.redIntensity != redIntensity) indicating end centisecond
                    // If we didn't find the change, and thus didn't break out of this while loop
                    centisecond++;
                } // end while (centiSec < seq.totalCentiseconds) loop looking for end centisecond
            } // end if new intensity > 0
            else
            {
                centisecond = 1;
            }
            // set new Current intensity
            redIntensity = p.redIntensity;


            // Now, start checking from end of the last effect, comparing to the previous centisecond
            while (centisecond <= seq.totalCentiseconds)
            {
                //updateProgress(2, redChannelIndex, centisecond);
                // get pixel for current centisec
                p = Pixels[centisecond];
                // Did the intensity change?
                if (!p.redEquals(Pixels[centisecond - 1]))
                {
                    if (p.redIntensity > 0)
                    {
                        // If New Intensity then Create New Effect
                        redIntensity = p.redIntensity;
                        newE.channelIndex = redChannelIndex;
                        newE.savedIndex = seq.channels[redChannelIndex].savedIndex;
                        newE.type = GetBestEffectType(p.redEffectType, p.grnEffectType, p.bluEffectType);
                        newE.startCentisecond = centisecond;
                        if (p.redEndIntensity > -1)
                        {
                            // for fades
                            newE.endIntensity = p.redEndIntensity;
                            newE.startIntensity = p.redIntensity;
                        }
                        else
                        {
                            // for steady
                            newE.intensity = p.redIntensity;
                        }

                        centisecond++;
                        // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                        while (centisecond < seq.totalCentiseconds + 1)
                        {
                            // get pixel for current centisecond
                            p = Pixels[centisecond];
                            // did the intensity change?
                            if (p.redIntensity != redIntensity)
                            {
                                // save end centisecond
                                newE.endCentisecond = centisecond;
                                // add this to the array of replacement effects, incr counter
                                Array.Resize(ref replaceEffs, replaceCount + 1);
                                replaceEffs[replaceCount] = newE;
                                replaceCount++;
                                // get ready to continue for loop above  (4 levels <-) by:
                                //    creating a new effect, and backing the centisecond up by 1
                                newE = new effect();
                                centisecond--;
                                break;
                            }  // end if (p.redIntensity != redIntensity) indicating end centisecond
                            // If we didn't find the change, and thus didn't break out of this while loop
                            centisecond++;
                        } // end while (centiSec < seq.totalCentiseconds) loop looking for end centisecond
                    } // end if new intensity > 0
                    // set new Current intensity
                    redIntensity = p.redIntensity;
                } // end if intensity changed
                centisecond++;
            } // end for loop thru all centiseconds

            // now REPLACE the effects list for this Red channel
            for (int fx = 0; fx < replaceCount; fx++)
            {
                // Loop thru Replacement Effects (for this Red channel) and add them to the master list of all new effects
                //    (including those we copied from the non-rgb channels, near the start of this procedure)
                Array.Resize(ref NEWeffects, newEffectCount + 1);
                NEWeffects[newEffectCount] = replaceEffs[fx];
                newEffectCount++;
                effectIndex++;
            }
            // END Red        

            // ** GREEN STARTS HERE **

            // now create NEW effects lists for the Greenchannel
            replaceEffs = new effect[1];
            newE = new effect();
            replaceCount = 0;
            // Reset to initial values of BLACK
            grnIntensity = 0;
            centisecond = 0;

            // Is the Greenturned on at the very first centisecond (0)?
            p = Pixels[0];
            if (p.grnIntensity > 0)
            {
                // If New Intensity then Create New Effect
                grnIntensity = p.grnIntensity;
                newE.channelIndex = grnChannelIndex;
                newE.savedIndex = seq.channels[grnChannelIndex].savedIndex;
                newE.type = GetBestEffectType(p.grnEffectType, p.bluEffectType, p.redEffectType);
                newE.startCentisecond = 0;
                if (p.grnEndIntensity > -1)
                {
                    // for fades
                    newE.endIntensity = p.grnEndIntensity;
                    newE.startIntensity = p.grnIntensity;
                }
                else
                {
                    // for steady
                    newE.intensity = p.grnIntensity;
                }

                centisecond = 1;
                // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                while (centisecond < seq.totalCentiseconds + 1)
                {
                    // get pixel for current centisecond
                    p = Pixels[centisecond];
                    // did the anything change?
                    if (!p.grnEquals(Pixels[centisecond - 1]))
                    {
                        // save end centisecond
                        newE.endCentisecond = centisecond;
                        // add this to the array of replacement effects, incr counter
                        Array.Resize(ref replaceEffs, replaceCount + 1);
                        replaceEffs[replaceCount] = newE;
                        replaceCount++;
                        // get ready to continue for loop above  (4 levels <-) by:
                        //    creating a new effect, and backing the centisecond up by 1
                        newE = new effect();
                        centisecond--;
                        break;
                    }  // end if (p.grnIntensity != grnIntensity) indicating end centisecond
                    // If we didn't find the change, and thus didn't break out of this while loop
                    centisecond++;
                } // end while (centiSec < seq.totalCentiseconds) loop looking for end centisecond
            } // end if new intensity > 0
            else
            {
                centisecond = 1;
            }
            // set new Current intensity
            grnIntensity = p.grnIntensity;


            // Now, start checking from end of the last effect, comparing to the previous centisecond
            while (centisecond <= seq.totalCentiseconds)
            {
                //updateProgress(2, grnChannelIndex, centisecond);
                // get pixel for current centisec
                p = Pixels[centisecond];
                // Did the intensity change?
                if (!p.grnEquals(Pixels[centisecond - 1]))
                {
                    if (p.grnIntensity > 0)
                    {
                        // If New Intensity then Create New Effect
                        grnIntensity = p.grnIntensity;
                        newE.channelIndex = grnChannelIndex;
                        newE.savedIndex = seq.channels[grnChannelIndex].savedIndex;
                        newE.type = GetBestEffectType(p.grnEffectType, p.bluEffectType, p.redEffectType);
                        newE.startCentisecond = centisecond;
                        if (p.grnEndIntensity > -1)
                        {
                            // for fades
                            newE.endIntensity = p.grnEndIntensity;
                            newE.startIntensity = p.grnIntensity;
                        }
                        else
                        {
                            // for steady
                            newE.intensity = p.grnIntensity;
                        }

                        centisecond++;
                        // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                        while (centisecond < seq.totalCentiseconds + 1)
                        {
                            // get pixel for current centisecond
                            p = Pixels[centisecond];
                            // did the intensity change?
                            if (p.grnIntensity != grnIntensity)
                            {
                                // save end centisecond
                                newE.endCentisecond = centisecond;
                                // add this to the array of replacement effects, incr counter
                                Array.Resize(ref replaceEffs, replaceCount + 1);
                                replaceEffs[replaceCount] = newE;
                                replaceCount++;
                                // get ready to continue for loop above  (4 levels <-) by:
                                //    creating a new effect, and backing the centisecond up by 1
                                newE = new effect();
                                centisecond--;
                                break;
                            }  // end if (p.grnIntensity != grnIntensity) indicating end centisecond
                            // If we didn't find the change, and thus didn't break out of this while loop
                            centisecond++;
                        } // end while (centiSec < seq.totalCentiseconds) loop looking for end centisecond
                    } // end if new intensity > 0
                    // set new Current intensity
                    grnIntensity = p.grnIntensity;
                } // end if intensity changed
                centisecond++;
            } // end for loop thru all centiseconds

            // now REPLACE the effects list for this Greenchannel
            for (int fx = 0; fx < replaceCount; fx++)
            {
                // Loop thru Replacement Effects (for this Greenchannel) and add them to the master list of all new effects
                //    (including those we copied from the non-rgb channels, near the start of this procedure)
                Array.Resize(ref NEWeffects, newEffectCount + 1);
                NEWeffects[newEffectCount] = replaceEffs[fx];
                newEffectCount++;
                effectIndex++;
            }
            // END GREEN

            // ** BLUE STARTS HERE **
            replaceEffs = new effect[1];
            newE = new effect();
            replaceCount = 0;
            // Reset to initial values of BLACK
            bluIntensity = 0;
            centisecond = 0;
            // Is the Blue turned on at the very first centisecond (0)?
            p = Pixels[0];
            if (p.bluIntensity > 0)
            {
                // If New Intensity then Create New Effect
                bluIntensity = p.bluIntensity;
                newE.channelIndex = bluChannelIndex;
                newE.savedIndex = seq.channels[bluChannelIndex].savedIndex;
                newE.type = GetBestEffectType(p.bluEffectType, p.grnEffectType, p.bluEffectType);
                newE.startCentisecond = 0;
                if (p.bluEndIntensity > -1)
                {
                    // for fades
                    newE.endIntensity = p.bluEndIntensity;
                    newE.startIntensity = p.bluIntensity;
                }
                else
                {
                    // for steady
                    newE.intensity = p.bluIntensity;
                }

                centisecond = 1;
                // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                while (centisecond < seq.totalCentiseconds + 1)
                {
                    // get pixel for current centisecond
                    p = Pixels[centisecond];
                    // did the anything change?
                    if (!p.bluEquals(Pixels[centisecond - 1]))
                    {
                        // save end centisecond
                        newE.endCentisecond = centisecond;
                        // add this to the array of replacement effects, incr counter
                        Array.Resize(ref replaceEffs, replaceCount + 1);
                        replaceEffs[replaceCount] = newE;
                        replaceCount++;
                        // get ready to continue for loop above  (4 levels <-) by:
                        //    creating a new effect,
                        //       and backing the centisecond up by 1 (in case the next effect needs to start immediately after this one)
                        newE = new effect();
                        centisecond--;
                        break; // from while centisecond loop
                    }  // end if (p.bluIntensity != bluIntensity) indicating end centisecond
                    // If we didn't find the change, and thus didn't break out of this while loop
                    centisecond++;
                } // end while (centiSec < seq.totalCentiseconds) loop looking for end centisecond
            } // end if new intensity > 0
            else
            {
                centisecond = 1;
            }
            // set new Current intensity
            bluIntensity = p.bluIntensity;


            // Now, start checking from end of the last effect, comparing to the previous centisecond
            while (centisecond <= seq.totalCentiseconds)
            {
                //updateProgress(2, bluChannelIndex, centisecond);
                // get pixel for current centisec
                p = Pixels[centisecond];
                // Did the intensity change?
                if (!p.bluEquals(Pixels[centisecond - 1]))
                {
                    if (p.bluIntensity > 0)
                    {
                        // If New Intensity then Create New Effect
                        bluIntensity = p.bluIntensity;
                        newE.channelIndex = bluChannelIndex;
                        newE.savedIndex = seq.channels[bluChannelIndex].savedIndex;
                        newE.type = GetBestEffectType(p.bluEffectType, p.grnEffectType, p.bluEffectType);
                        newE.startCentisecond = centisecond;
                        if (p.bluEndIntensity > -1)
                        {
                            // for fades
                            newE.endIntensity = p.bluEndIntensity;
                            newE.startIntensity = p.bluIntensity;
                        }
                        else
                        {
                            // for steady
                            newE.intensity = p.bluIntensity;
                        }

                        centisecond++;
                        // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                        while (centisecond < seq.totalCentiseconds + 1)
                        {
                            // get pixel for current centisecond
                            p = Pixels[centisecond];
                            // did the intensity change?
                            if (p.bluIntensity != bluIntensity)
                            {
                                // save end centisecond
                                newE.endCentisecond = centisecond;
                                // add this to the array of replacement effects, incr counter
                                Array.Resize(ref replaceEffs, replaceCount + 1);
                                replaceEffs[replaceCount] = newE;
                                replaceCount++;
                                // get ready to continue for loop above  (4 levels <-) by:
                                //    creating a new effect, and backing the centisecond up by 1
                                newE = new effect();
                                centisecond--;
                                break;
                            }  // end if (p.bluIntensity != bluIntensity) indicating end centisecond
                            // If we didn't find the change, and thus didn't break out of this while loop
                            centisecond++;
                        } // end while (centiSec < seq.totalCentiseconds) loop looking for end centisecond
                    } // end if new intensity > 0
                    // set new Current intensity
                    bluIntensity = p.bluIntensity;
                } // end if intensity changed
                centisecond++;
            } // end for loop thru all centiseconds

            // now REPLACE the effects list for this Blue channel
            for (int fx = 0; fx < replaceCount; fx++)
            {
                // Loop thru Replacement Effects (for this Blue channel) and add them to the master list of all new effects
                //    (including those we copied from the non-rgb channels, near the start of this procedure)
                Array.Resize(ref NEWeffects, newEffectCount + 1);
                NEWeffects[newEffectCount] = replaceEffs[fx];
                newEffectCount++;
                effectIndex++;
            }
            // END BLUE                    


            channelIndex++;  // Skip next channel which will be the green one of this rgbChannel, we've already done it!
            channelIndex++;  // Likewise, skip again, which whill be the blue one, it's done also

            rgbChannelIndex++;
            effectIndex++;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            readSequence(lastFile);
        }

        private void readSequence(string fileName)
        {

            btnOK.Enabled = false;
            grpFile.Enabled = false;
            grpColors.Enabled = false;

            seq = new Sequence();
            seq.readFile(fileName);

            string sMsg;
            sMsg = "File Parse Complete!\r\n\r\n";
            sMsg += seq.lineCount.ToString() + " lines\r\n";
            sMsg += seq.channelCount.ToString() + " channels\r\n";
            sMsg += seq.rgbChannelCount.ToString() + " RGB Channels\r\n";
            sMsg += seq.effectCount.ToString() + " effects\r\n";
            //sMsg += seq.groupCount.ToString() + " groups\r\n";
            //sMsg += seq.groupItemCount.ToString() + " group items\r\n";

            string len = FormatCentisecondsAsTime(seq.totalCentiseconds);

            sMsg += " length " + len + " (" + seq.totalCentiseconds.ToString() + " centiseconds)\r\n";

            DialogResult mReturn;
            mReturn = MessageBox.Show(sMsg, "File Parse Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);


            // Create temp new pixel array which will hold RGB values for each and every centisecond
            Pixels = new pixel[seq.totalCentiseconds + 2];
            txtStartTime.Text = "0:00.00";
            txtEndTime.Text = len;
            txtUntil.Text = len;
            startCentisecond = 0;
            endCentisecond = seq.totalCentiseconds;
            
            cboStartChannel.Items.Clear();
            cboEndChannel.Items.Clear();

            if (seq.rgbChannelCount > 0)
            {
                for (int i = 0; i < seq.rgbChannelCount; i++)
                {
                    cboStartChannel.Items.Add(seq.rgbChannels[i].name);
                    cboEndChannel.Items.Add(seq.rgbChannels[i].name);
                }
                cboStartChannel.SelectedIndex = 0;
                cboEndChannel.SelectedIndex = cboEndChannel.Items.Count - 1;

                lastFile = fileName;
                Properties.Settings.Default.lastFile = lastFile;
                Properties.Settings.Default.Save();
                grpLimits.Enabled = true;
            }
            else
            {
                string Msg = "Sequence file " + Path.GetFileNameWithoutExtension(lastFile) + " does not contain any RGB Channels!\n";
                Msg += "No Pixel Dust can be applied to a file without RGB Channels.\n";
                Msg += "Cannot continue with this file.  Please choose another which has RGB Channels.";
                DialogResult dr = MessageBox.Show(this, Msg, "No RGB Channels", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            btnRead.Enabled = false;
            grpFile.Enabled = true;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ChangeColors();
            
            
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";
            //string newFile = basePath + "!CHANGED " + txtFilename.Text;
            string newFile = Path.GetDirectoryName(lastFile) + "\\" + "!CHANGED " + Path.GetFileName(lastFile);
            
            //seq.WriteFile(newFile);
            seq.WriteFileInDisplayOrder(newFile);

            grpColors.Enabled = true;
            grpFile.Enabled = true;
            btnOK.Enabled = true;

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\windows\Media\chimes.wav");
            player.Play();

        } // end parseFile

        

        private int intensity(int amount)
        {
            return Convert.ToInt16(amount * 2.5);
        }

        public static Color makeColor(int Rd, int Gn, int Bl)
        {
            Color nc = Color.Gray;
            if (Rd + Gn + Bl > -1)
            {
                int r2 = Convert.ToInt16(Rd * 2.55);
                int g2 = Convert.ToInt16(Gn * 2.55);
                int b2 = Convert.ToInt16(Bl * 2.55);
                nc = Color.FromArgb(r2, g2, b2);
            }
            return nc;
        }

        public effectType  GetBestEffectType(effectType FirstType, effectType SecondType, effectType ThirdType)
        {
            effectType returnType = effectType.intensity;

            if (FirstType > effectType.None)
            { 
                returnType = FirstType ;
            }
            else if (SecondType > effectType.None)
            { 
                returnType = SecondType;
            }
            else if (ThirdType > effectType.None)
            {
                returnType = ThirdType;
            }
            

            return returnType;
        }

 
        public void updateProgress(int chanNo, long centiSecond)
        {

            long pct = chanNo * 100 / seq.channelCount;
            int pi = Convert.ToInt16(pct);
            prgProgress.Value = pi;

            string sMsg = "Channel " + (chanNo+1).ToString() + " of " + seq.channelCount.ToString();
            //sMsg += ", Pass " + part.ToString() + " of 2";
            //sMsg += ", Centisecond " + centiSecond.ToString() + " of " + seq.totalCentiseconds.ToString();
            //Debug.Print(sMsg);
            staInfo1.Text = sMsg;
            staStatus.Refresh();

        } // end Form

        public struct rgbPix
        {
            public int r;
            public int g;
            public int b;
        }

        private void frmDust_Shown(object sender, EventArgs e)
        {
            if (!doneInit)
            {
                if (chkAutoRead.Checked)
                {
                    if (File.Exists(lastFile))
                    {
                        readSequence(lastFile);
                    }
                }
                else
                {
                    btnRead.Enabled = true;
                }
            
            }
            doneInit = true;
        }

        private void chkAutoRead_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.autoRead = chkAutoRead.Checked;
            Properties.Settings.Default.Save();
        }

        private void txtEndTime_Leave(object sender, EventArgs e)
        {
            //string reg = @"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";
            //if (!Regex.IsMatch(txtEndTime.Text.Trim(), reg))
            //{
            //    MessageBox.Show("不合法！");
            //}
        }

        private void txtEndTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits 0-9 (KeyChar 48-57)
            // Allow          . (KeyChar 46)
            // Allow          : (KeyChar 58)
            // Disallow anything else
            
            if (e.KeyChar > 31 && e.KeyChar < 46)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 47)
            {
                e.Handled = true;
            }
            else if (e.KeyChar > 58)
            {
                e.Handled = true;
            }
        }

        private void txtEndTime_Validating(object sender, CancelEventArgs e)
        {
            string Msg;
            DialogResult dl;
            long centisecs = TimeTextToCentiseconds(txtEndTime.Text);
            if (centisecs < 0)
            {
                Msg = "Time Format Invalid!\r\n";
                Msg += "Please use format m:ss.cc where:\r\n";
                Msg += "   m  = minutes\r\n";
                Msg += "   ss = seconds, and\r\n";
                Msg += "   cc = hundreth's of a second.";
                dl = MessageBox.Show (Msg,"Invalid Time",MessageBoxButtons.OK ,MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
            else if (centisecs < startCentisecond)
            {
                Msg = "Can't end before we start!";
                dl = MessageBox.Show (Msg,"Invalid Time",MessageBoxButtons.OK ,MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
            else if (centisecs > seq.totalCentiseconds)
            {
                Msg = "The sequence isn't that long!";
                dl = MessageBox.Show (Msg,"Invalid Time",MessageBoxButtons.OK ,MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
            else
            {
                endCentisecond = centisecs;
            }
        
        }

        private long TimeTextToCentiseconds(string TimeText)
        { 
            DateTime dt = new DateTime();
            long centisecs = -1;
            if (DateTime.TryParse(txtEndTime.Text, out dt))
            {
                centisecs = dt.Minute * 6000;
                centisecs += dt.Second * 100;
                centisecs += dt.Millisecond / 100;

                string Msg = txtEndTime.Text + " = " + centisecs.ToString() + " centiseconds";
                DialogResult dr = MessageBox.Show (Msg,"Conversion",MessageBoxButtons.OK ,MessageBoxIcon.Information);
            }
            return centisecs;
 
        }

        private string FormatCentisecondsAsTime(long centiseconds)
        {
            int min = (int)(centiseconds / 6000);
            int sec = (int)(centiseconds % 6000 / 100);
            long cent = centiseconds % 100;
            string len = min.ToString() + ":" + sec.ToString("00") + "." + cent.ToString("00");
            return len;
        }

        private void txtStartTime_Validating(object sender, CancelEventArgs e)
        {
            string Msg;
            DialogResult dl;
            long centisecs = TimeTextToCentiseconds(txtEndTime.Text);
            if (centisecs < 0)
            {
                Msg = "Time Format Invalid!\r\n";
                Msg += "Please use format m:ss.cc where:\r\n";
                Msg += "   m  = minutes\r\n";
                Msg += "   ss = seconds, and\r\n";
                Msg += "   cc = hundreth's of a second.";
                dl = MessageBox.Show(Msg, "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
            else if (centisecs > endCentisecond)
            {
                Msg = "Can't end before we start!";
                dl = MessageBox.Show(Msg, "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
            else
            {
                startCentisecond = centisecs;
            }

        }

        private void txtStartTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits 0-9 (KeyChar 48-57)
            // Allow          . (KeyChar 46)
            // Allow          : (KeyChar 58)
            // Disallow anything else

            if (e.KeyChar > 31 && e.KeyChar < 46)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 47)
            {
                e.Handled = true;
            }
            else if (e.KeyChar > 58)
            {
                e.Handled = true;
            }

        }

        private void readColorPresets()
        {

            string lineIn = "";
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\";
            string fileName = basePath + "PixelDustColors.xml";
            int pos1;

            if (!File.Exists(fileName))
            {
                createColorPresets();
            }
            if (File.Exists(fileName))
            {
                StreamReader reader = new StreamReader(fileName);
                while ((lineIn = reader.ReadLine()) != null)
                {
                    pos1 = lineIn.IndexOf(FIELDname);
                    if (pos1 > 2)
                    {
                        Array.Resize(ref LorColors, colorCount + 1);
                        LORcolor c = new LORcolor();
                        c.name = Sequence.getKeyWord(lineIn, FIELDname);
                        c.r = Sequence.getKeyValue(lineIn, "r");
                        c.g = Sequence.getKeyValue(lineIn, "g");
                        c.b = Sequence.getKeyValue(lineIn, "b");
                        LorColors[colorCount] = c;
                        colorCount++;
                    }
                }
                reader.Close();
            }
        }

        private void createColorPresets()
        {
            string sLine = "";

            // TODO: 1. Get sequences path from registry key HKEY_CURRENT_USER\Software\Light-O-Rama\Shared
            //           2. Make sure exists before trying to open it
            //           3. Allow user to browse to it create new default in the sequences folder
          string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama 2015\\";
            string fileName = basePath + "PixelDustColors.xml";

            StreamWriter writer = new StreamWriter(fileName);

            sLine = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
            writer.WriteLine(sLine);
            sLine = "<colors>";
            writer.WriteLine(sLine);
            sLine = "	<color name=\"Current\" r=\"-1\" g=\"-1\" b=\"-1\" />";
            writer.WriteLine(sLine);
            sLine = "	<color name=\"Black\" r=\"0\" g=\"0\" b=\"0\" />";
            writer.WriteLine(sLine);
            sLine = "	<color name=\"White\" r=\"100\" g=\"100\" b=\"100\" />";
            writer.WriteLine(sLine);
            sLine = "	<color name=\"Red\" r=\"100\" g=\"0\" b=\"0\" />";
            writer.WriteLine(sLine);
            sLine = "	<color name=\"Yellow\" r=\"70\" g=\"70\" b=\"0\" />";
            writer.WriteLine(sLine);
            sLine = "	<color name=\"Green\" r=\"0\" g=\"100\" b=\"0\" />";
            writer.WriteLine(sLine);
            sLine = "	<color name=\"Cyan\" r=\"0\" g=\"70\" b=\"70\" />";
            writer.WriteLine(sLine);
            sLine = "	<color name=\"Blue\" r=\"0\" g=\"0\" b=\"100\" />";
            writer.WriteLine(sLine);
            sLine = "	<color name=\"Magenta\" r=\"70\" g=\"0\" b=\"70\" />";
            writer.WriteLine(sLine);
            sLine = "	<color name=\"Pink\" r=\"100\" g=\"40\" b=\"40\" />";
            writer.WriteLine(sLine);
            sLine = "	<color name=\"Lt. Blue\" r=\"40\" g=\"40\" b=\"100\" />";
            writer.WriteLine(sLine);
            sLine = "</colors>";
            writer.WriteLine(sLine);

            writer.Close();
        
        }

        private void fillColorPresets()
        {

            for (int i = 0; i < colorCount; i++)
            {
                cboFromColor.Items.Add(LorColors[i].name);
                cboToColor.Items.Add(LorColors[i].name);
            }
            cboFromColor.SelectedIndex = 0;
            cboToColor.SelectedIndex = 1;
        }


        private void cboFade_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (cboFade.SelectedIndex == 0)
            {
                pnlUntil.Enabled = false;
                txtUntil.Enabled = false;
                pnlUntil.Visible = false;
                txtUntil.Visible = false;
            }
            else if (cboFade.SelectedIndex == 1)
            {
                txtUntil.Visible = true;
                pnlUntil.Visible = true;
                txtUntil.Enabled = true;
                pnlUntil.Enabled = true;
                
            }
            */
        }

        private void cboFromColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            LORcolor c = LorColors[cboFromColor.SelectedIndex];
            picFromColor.BackColor = makeColor(c.r, c.g, c.b);
        }

        private void cboToColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            LORcolor c = LorColors[cboToColor.SelectedIndex];
            picToColor.BackColor = makeColor(c.r, c.g, c.b);

        }

        private int[] chooseOrder(int Count)
        {
            bool[] done = new bool[Count];
            int[] order = new int[Count];
            int compl = 0;
            int next = 0;
            CryptoRandom rnd = new CryptoRandom();
            var foo = new List<int>();

            for (int i = 0; i < Count; i++)
            {
                foo.Add(i);
            }

            for (int i = Count - 1; i >= 0; i--)
            {
                next = rnd.Next(0, i + 1);
                order[i] = foo[next];
                foo.RemoveAt(next);
            }

            for (int i = 0; i < Count; i++)
            {
                Debug.Print(order[i].ToString() + ", ");
            
            }

            return order;
        }

        private class LORcolor
        {
            public string name;
            public int r;
            public int g;
            public int b;
        }

    } // end frmDust
    

} // end Namespace

