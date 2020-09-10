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
using LORUtils;

namespace RGBORama
{

    
    
    public partial class frmColors : Form
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

        private string lastFile = "";
        private bool colorChanged = false;
        private bool changesMade = false;
        private Sequence4 seq = new Sequence4();
        private PresetSet[] presetSets;



 
        int presetSetCount = 0;
        int colorChangeCount = 0;


        private Effect[] NEWeffects;
        int newEffectCount = 0;

        public frmColors()
        {
            InitializeComponent();
        }

        private void optTime1_CheckedChanged(object sender, EventArgs e)
        {
            txtTimeFrom.Enabled = false;
            txtTimeTo.Enabled = false;
        }

        private void optTime2_CheckedChanged(object sender, EventArgs e)
        {
            txtTimeTo.Enabled = true;
            txtTimeFrom.Enabled = true;
            txtTimeFrom.Focus();
        }

        private void optColor1_CheckedChanged(object sender, EventArgs e)
        {
            if (optColor1.Checked)
            {
                if (colorChanged)
                {
                    saveColors();
                }

                txtColorFrom1.Enabled = false;
                txtColorFrom2.Enabled = false;
                txtColorFrom3.Enabled = false;
                txtColorFrom4.Enabled = false;
                txtColorFrom5.Enabled = false;
                txtColorFrom6.Enabled = false;
                txtColorTo1.Enabled = false;
                txtColorTo2.Enabled = false;
                txtColorTo3.Enabled = false;
                txtColorTo4.Enabled = false;
                txtColorTo5.Enabled = false;
                txtColorTo6.Enabled = false;
            } // end optColor1.Checked
        } // end optColor1_CheckedChanged

        private void optColor3_CheckedChanged(object sender, EventArgs e)
        {
            if (optColor3.Checked)
            {
                txtColorFrom1.Enabled = true;
                txtColorFrom2.Enabled = true;
                txtColorFrom3.Enabled = true;
                txtColorFrom4.Enabled = true;
                txtColorFrom5.Enabled = true;
                txtColorFrom6.Enabled = true;
                txtColorTo1.Enabled = true;
                txtColorTo2.Enabled = true;
                txtColorTo3.Enabled = true;
                txtColorTo4.Enabled = true;
                txtColorTo5.Enabled = true;
                txtColorTo6.Enabled = true;

                txtColorFrom1.Focus();
            } // end optColor3.Checked
        } // end optColor3_CheckedChanged

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

			dlgFileOpen.Filter = "Sequence Files *.las, *.lms|*.las;*.lms|Musical Sequences *.lms|*.lms|Animated Sequences *.las|*.las";
			dlgFileOpen.DefaultExt = "*.lms";
            dlgFileOpen.InitialDirectory = basePath;
            dlgFileOpen.CheckFileExists = true;
            dlgFileOpen.CheckPathExists = true;
            dlgFileOpen.Multiselect = false;
            DialogResult result = dlgFileOpen.ShowDialog();

            if (result == DialogResult.OK)
            {
                lastFile = dlgFileOpen.FileName;
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
                        txtInputFilename.Text = lastFile.Substring(basePath.Length);
                    }
                    else
                    {
                        txtInputFilename.Text = lastFile;
                    } // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                } // end if (lastFile.Length > basePath.Length)
            } // end if (result = DialogResult.OK)
        }

        private void frmColors_Load(object sender, EventArgs e)
        {
            initForm();
        }

        private void initForm()
        {
            LoadPresets();
            fillPresetList();
						RestoreFormPosition();

            
            lastFile = Properties.Settings.Default.lastFile;
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

            if (lastFile.Length > basePath.Length)
            {
                if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                {
                    txtInputFilename.Text = lastFile.Substring(basePath.Length);
                }
                else
                {
                    txtInputFilename.Text = lastFile;
                } // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
            } // end if (lastFile.Length > basePath.Length)

            string opt = Properties.Settings.Default.lastTimeOpt;
            if (opt == "2")
            {
                optTime2.Checked = true;
            }
            else
            {
                optTime1.Checked = true;
            }
            string timeTxt = Properties.Settings.Default.lastTimeFrom;
            txtTimeFrom.Text = timeTxt;
            timeTxt = Properties.Settings.Default.lastTimeTo;
            txtTimeTo.Text = timeTxt;

            opt = Properties.Settings.Default.lastColorOpt;
            if (opt == "1")
            {
                optColor1.Checked = true;
            }
            else
            {
                optColor3.Checked = true;
            }

            string colorTxt = Properties.Settings.Default.lastColorFrom1;
            txtColorFrom1.Text = colorTxt;
            colorTxt = Properties.Settings.Default.lastColorFrom2;
            txtColorFrom2.Text = colorTxt;
            colorTxt = Properties.Settings.Default.lastColorFrom3;
            txtColorFrom3.Text = colorTxt;
            colorTxt = Properties.Settings.Default.lastColorFrom4;
            txtColorFrom4.Text = colorTxt;
            colorTxt = Properties.Settings.Default.lastColorFrom5;
            txtColorFrom5.Text = colorTxt;
            colorTxt = Properties.Settings.Default.lastColorFrom6;
            txtColorFrom6.Text = colorTxt;
            colorTxt = Properties.Settings.Default.lastColorTo1;
            txtColorTo1.Text = colorTxt;
            colorTxt = Properties.Settings.Default.lastColorTo2;
            txtColorTo2.Text = colorTxt;
            colorTxt = Properties.Settings.Default.lastColorTo3;
            txtColorTo3.Text = colorTxt;
            colorTxt = Properties.Settings.Default.lastColorTo4;
            txtColorTo4.Text = colorTxt;
            colorTxt = Properties.Settings.Default.lastColorTo5;
            txtColorTo5.Text = colorTxt;
            colorTxt = Properties.Settings.Default.lastColorTo6;
            txtColorTo6.Text = colorTxt;
            colorChanged = false;

        } // end initForm

        private void saveColors()
        {
            Properties.Settings.Default.lastColorFrom1 = txtColorFrom1.Text;
            Properties.Settings.Default.lastColorFrom2 = txtColorFrom2.Text;
            Properties.Settings.Default.lastColorFrom3 = txtColorFrom3.Text;
            Properties.Settings.Default.lastColorFrom4 = txtColorFrom4.Text;
            Properties.Settings.Default.lastColorFrom5 = txtColorFrom5.Text;
            Properties.Settings.Default.lastColorFrom6 = txtColorFrom6.Text;
            Properties.Settings.Default.lastColorTo1 = txtColorTo1.Text;
            Properties.Settings.Default.lastColorTo2 = txtColorTo2.Text;
            Properties.Settings.Default.lastColorTo3 = txtColorTo3.Text;
            Properties.Settings.Default.lastColorTo4 = txtColorTo4.Text;
            Properties.Settings.Default.lastColorTo5 = txtColorTo5.Text;
            Properties.Settings.Default.lastColorTo6 = txtColorTo6.Text;
            colorChanged = false;
            Properties.Settings.Default.Save();
        } // end saveColors

        private void saveSettings()
        {

            Properties.Settings.Default.lastFile = lastFile;
            
            string opt = "1";
            if (optTime2.Checked)
            {
                opt = "2";
            }
            Properties.Settings.Default.lastTimeOpt = opt;
            Properties.Settings.Default.lastTimeTo = txtTimeTo.Text;
            Properties.Settings.Default.lastTimeFrom = txtTimeFrom.Text;

            if (optColor1.Checked)
            {
                opt = "1";
            }
            else
            {
                opt = "3";
            }
            Properties.Settings.Default.lastColorOpt = opt;
            Properties.Settings.Default.Save();
        } // end saveSettings

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

		private DialogResult MakeSelections()
		{
			ImBusy(true);
			DialogResult ret = DialogResult.None;
			frmChannels chanForm = new frmChannels((Sequence4)seq.Duplicate());
			ret = chanForm.ShowDialog(this);

			if (ret == DialogResult.OK)
			{
				seq = chanForm.seq;
			}


			ImBusy(false);
			return ret;
		}

		public void ImBusy(bool busyState)
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
		}




		private void txtColorFrom1_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
        }

        private void txtColorTo1_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
        }

        private void txtColorFrom2_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
        }

        private void txtColorFrom3_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
        }

        private void txtColorFrom4_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
        }

        private void txtColorFrom5_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
        }

        private void txtColorFrom6_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
        }

        private void txtColorTo2_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
        }

        private void txtColorTo3_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
        }

        private void txtColorTo4_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
        }

        private void txtColorTo5_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
        }

        private void txtColorTo6_TextChanged(object sender, EventArgs e)
        {
            colorChanged = true;
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
        } // end backupFile

        
        private int ChangeColors()
        {
            int errState = 0;
            int redChannel.Index;  // The Array Index of the Red Channel of the current RGB Channel
            int grnChannel.Index;  // Note: Array Index, NOT 'SavedIndex'
            int bluChannel.Index;
            int redFirstEffectIndex = -1;   // The Array Index of the first Effect in this Red Channel
            int redFinalEffectIndex = -1;     // The Array Index of the first Effect PAST this Red Channel
            int grnFirstEffectIndex = -1;
            int grnFinalEffectIndex = -1;
            int bluFirstEffectIndex = -1;
            int bluFinalEffectIndex = -1;
            int redIntensity; // The Red Intensity, right this very Centisecond
            int grnIntensity;
            int bluIntensity;
            int redEffectIndex = -1;    // the Array Index of the Effect for the Red Channeol for the current Centisecond
            int grnEffectIndex = -1;
            int bluEffectIndex = -1;
            Effect blankEffect = new Effect();  // Used if there is NO Effects for a channel, and also when past the last Effect for the channel
            //blankEffect.startIntensity = 0;
            //blankEffect.endIntensity = 0;
            blankEffect.Intensity = 0;
            Effect redEffect = blankEffect;   // Current Red Effect structure for the current Centisecond
            Effect grnEffect = blankEffect;
            Effect bluEffect = blankEffect;
            ColorChange[] changeList = presetSets[cboPresets.SelectedIndex].colorChanges; // Temp, for convenience, a copy of the array of changes to be made
            EffectType redEffectType = EffectType.Intensity;
            EffectType grnEffectType = EffectType.Intensity;
            EffectType bluEffectType = EffectType.Intensity;
            int channelIndex = 0;
            int rgbChannelIndex = 0;
            int effectIndex = 0;
            int redEndIntensity = -1;
            int grnEndIntensity = -1;
            int bluEndIntensity = -1;
            int redEndCentisecond = -1;
            int grnEndCentisecond = -1;
            int bluEndCentisecond = -1;
            int replaceCount = 0;
            int centisecond = 0;
            //int lastEndCenti = -1;
            pixel p = new pixel();
            // Create temp new pixel array which will hold RGB values for each and every centisecond
            pixel[] Keywdels = new pixel[seq.Centiseconds + 2];
            string sMsg = "";
            frmKeywdels fp = new frmKeywdels();
            bool showChanges = false;
            bool copyEffects = false;
                
            
            // Loop thru ALL Channels
            while (channelIndex < seq.Channels.Count)
            {
                copyEffects = true;
                if (rgbChannelIndex <= seq.RGBchannels.Count)
                {
                    // Is this channel an RGB channel, or a regular one?
                    if (seq.RGBchannels[rgbChannelIndex].redChannel.SavedIndex == seq.Channels[channelIndex].SavedIndex ||
                        seq.RGBchannels[rgbChannelIndex].grnChannel.SavedIndex == seq.Channels[channelIndex].SavedIndex ||
                        seq.RGBchannels[rgbChannelIndex].bluChannel.SavedIndex == seq.Channels[channelIndex].SavedIndex)
                    {
                        copyEffects = false;
                    }
                }
                if (copyEffects)
                {
                    // NOT an RGB, this is a regular
                    // Are there any Effects for this channel?
                    effectIndex = seq.Channels[channelIndex].firstEffectIndex;
                    if (effectIndex >= 0)
										
                    {
                        while (seq.Effects[effectIndex].channelIndex == channelIndex)
                        {
                            // Copy them to the NEW array
                            Array.Resize(ref NEWeffects, newEffectCount + 1);
                            NEWeffects[newEffectCount] = seq.Effects[effectIndex];
                            newEffectCount++;
                            effectIndex++;
                        } // end While Effect
                    }
                }
                else
                {
                    // This is an RGB Channel
                    // Get the Array Index of the Red, Green, and Blue Channels in this RGB Channel
                    redChannel.Index = seq.RGBchannels[rgbChannelIndex].redChannel.Index;
                    grnChannel.Index = seq.RGBchannels[rgbChannelIndex].grnChannel.Index;
                    bluChannel.Index = seq.RGBchannels[rgbChannelIndex].bluChannel.Index;
                    // Reset
                    redFirstEffectIndex = -1;
                    redFinalEffectIndex = -1;
                    grnFirstEffectIndex = -1;
                    grnFinalEffectIndex = -1;
                    bluFirstEffectIndex = -1;
                    bluFinalEffectIndex = -1;

                    // Loop thru all Effects, looking for the ones for these red, green and blue Channels


                    redFirstEffectIndex = seq.Channels[redChannel.Index].firstEffectIndex;
                    redEffectIndex = redFirstEffectIndex;
                    if (redEffectIndex >= 0)
                    {
                        redEffect = seq.Effects[redEffectIndex];
                        redEffectType = redEffect.type;
                        redEndIntensity = redEffect.endIntensity;
                        redEndCentisecond = redEffect.endCentisecond;
                    }

                    grnFirstEffectIndex = seq.Channels[grnChannel.Index].firstEffectIndex;
                    grnEffectIndex = grnFirstEffectIndex;
                    if (grnEffectIndex >= 0)
                    {
                        grnEffect = seq.Effects[grnEffectIndex];
                        grnEffectType = grnEffect.type;
                        grnEndIntensity = grnEffect.endIntensity;
                        grnEndCentisecond = grnEffect.endCentisecond;
                    }

 
                    bluFirstEffectIndex = seq.Channels[bluChannel.Index].firstEffectIndex;
                    bluEffectIndex = bluFirstEffectIndex;
                    if (bluEffectIndex >= 0)
                    {
                        bluEffect = seq.Effects[bluEffectIndex];
                        bluEffectType = bluEffect.type;
                        bluEndIntensity = bluEffect.endIntensity;
                        bluEndCentisecond = bluEffect.endCentisecond;
                    }

                    // Reset to initial values of BLACK
                    redIntensity = 0;
                    grnIntensity = 0;
                    bluIntensity = 0;



                    // Count thru all centiseconds to the end of the sequence
                    for (int centiSec = 0; centiSec < seq.Centiseconds; centiSec++)
                    {
                        // Create a new pixel for this centisecond
                        p = new pixel();

                        if (redEffectIndex >= 0)
                        {
                            if (redEffect.channelIndex == redChannel.Index)
                            {
                                // Check current centisecond against the end of the current Red Effect
                                if (redEffect.endCentisecond == centiSec)
                                {
                                    // got it! potential change coming up!

                                    // check end Intensity (Ramps)
                                    if (redEffect.endIntensity > -1)
                                    {
                                        //redIntensity = redEffect.endIntensity;
                                        redEndIntensity = redEffect.endIntensity;
                                    }
                                    else
                                    {
                                        // not a ramp, set Intensity to zero
                                        redIntensity = 0;
                                    }
                                    redEndIntensity = -2;
                                    redEndCentisecond = -2;
                                    redEffectType = EffectType.None;

                                    redEffectIndex++;
                                    redEffect = seq.Effects[redEffectIndex];
                                } // end if endCentisecond

                                if (redEffect.startCentisecond == centiSec)
                                {
                                    // current centisecond matches the start centisecond of the current Effect
                                    if (redEffect.startIntensity > -1)
                                    {
                                        redIntensity = redEffect.startIntensity;
                                    }
                                    if (redEffect.Intensity > -1)
                                    {
                                        redIntensity = redEffect.Intensity;
                                    }
                                    redEndIntensity = redEffect.endIntensity;
                                    redEffectType = redEffect.type;
                                    redEndIntensity = redEffect.endIntensity;
                                    redEndCentisecond = redEffect.endCentisecond;

                                } // end  if startCentisecond, endcentiSecond
                            } // end if channel indexes match
                        } // end if we have any red Effects

                        //sMsg = "Keywdel[" + centiSec.ToString() + "].redIntensity = " + redIntensity.ToString();
                        //Debug.Print(sMsg);

                        // Now lets go thru the same thing for the Green
                        // See comments above for Red, same thing happening here

                        if (grnEffectIndex >= 0)
                        {
                            if (grnEffect.channelIndex == grnChannel.Index)
                            {
                                // Check current centisecond against the end of the current grn Effect
                                if (grnEffect.endCentisecond == centiSec)
                                {
                                    // got it! potential change coming up!

                                    // check end Intensity (Ramps)
                                    if (grnEffect.endIntensity > -1)
                                    {
                                        //grnIntensity = grnEffect.endIntensity;
                                        grnEndIntensity = grnEffect.endIntensity;
                                    }
                                    else
                                    {
                                        // not a ramp, set Intensity to zero
                                        grnIntensity = 0;
                                    }
                                    grnEndIntensity = -2;
                                    grnEndCentisecond = -2;
                                    grnEffectType = EffectType.None;

                                    grnEffectIndex++;
                                    grnEffect = seq.Effects[grnEffectIndex];
                                }
                                if (grnEffect.startCentisecond == centiSec)
                                {
                                    // current centisecond matches the start centisecond of the current Effect
                                    if (grnEffect.startIntensity > -1)
                                    {
                                        grnIntensity = grnEffect.startIntensity;
                                    }
                                    if (grnEffect.Intensity > -1)
                                    {
                                        grnIntensity = grnEffect.Intensity;
                                    }
                                    grnEndIntensity = grnEffect.endIntensity;
                                    grnEffectType = grnEffect.type;
                                    grnEndIntensity = grnEffect.endIntensity;
                                    grnEndCentisecond = grnEffect.endCentisecond;

                                } // end startCentisecond, endcentiSecond
                            } // end if channel Indexes match
                        } // end if we have any green Effects

                        // And now finally lets go thru the same thing for the Blue
                        // See comments above for grn, same thing happening here

                        if (bluEffectIndex >= 0)
                        {
                            if (bluEffect.channelIndex == bluChannel.Index)
                            {
                                // Check current centisecond against the end of the current Blue Effect
                                if (bluEffect.endCentisecond == centiSec)
                                {
                                    // got it! potential change coming up!

                                    // check end Intensity (Ramps)
                                    if (bluEffect.endIntensity > -1)
                                    {
                                        //bluIntensity = bluEffect.endIntensity;
                                        bluEndIntensity = bluEffect.endIntensity;
                                    }
                                    else
                                    {
                                        // not a ramp, set Intensity to zero
                                        bluIntensity = 0;
                                    }
                                    bluEndIntensity = -2;
                                    bluEndCentisecond = -2;
                                    bluEffectType = EffectType.None;

                                    bluEffectIndex++;
                                    bluEffect = seq.Effects[bluEffectIndex];
                                }
                                if (bluEffect.startCentisecond == centiSec)
                                {
                                    // current centisecond matches the start centisecond of the current Effect
                                    if (bluEffect.startIntensity > -1)
                                    {
                                        bluIntensity = bluEffect.startIntensity;
                                    }
                                    if (bluEffect.Intensity > -1)
                                    {
                                        bluIntensity = bluEffect.Intensity;
                                    }
                                    bluEndIntensity = bluEffect.endIntensity;
                                    bluEffectType = bluEffect.type;
                                    bluEndIntensity = bluEffect.endIntensity;
                                    bluEndCentisecond = bluEffect.endCentisecond;

                                } // end startCentisecond, endcentiSecond
                            } // end if channel indexes match
                        } // end if we have any blue Effects
                    
                    
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
                        Keywdels[centiSec] = p;

                        //sMsg = "Channel[" + redChannel.Index.ToString() + "] ";
                        //sMsg += "Keywdel[" + centiSec.ToString() + "].redIntensity = " + p.redIntensity.ToString();
                        //Debug.Print(sMsg);

                    }
                    Keywdels[seq.Centiseconds] = new pixel();
                    Keywdels[seq.Centiseconds + 1] = new pixel();
                    // Done building pixel array!!!
                    // We now have an array with the RGB values of this rgbChannel for each and every centisecond of the sequence

                    if (showChanges)
                    {
                        fp = new frmKeywdels();
                        for (int i = 0; i < seq.Centiseconds; i++)
                        {
                            fp.setKeywdel(i, 0, Intensity(Keywdels[i].redIntensity), Intensity(Keywdels[i].grnIntensity), Intensity(Keywdels[i].bluIntensity));
                        }
                    }


                    // Now, lets go thru that array, and look for the colors we are supposed to be changing FROM
                    for (int centiSec = 0; centiSec < seq.Centiseconds; centiSec++)
                    {
                        //updateProgress(2, grnChannel.Index, centiSec);
                        // Loop thru list of changes
                        for (int cg = 0; cg < presetSets[cboPresets.SelectedIndex].changeCount; cg++)
                        {
                            // compare red
                            if (Keywdels[centiSec].redIntensity == changeList[cg].fromR)
                            {
                                // AND compare green
                                if (Keywdels[centiSec].grnIntensity == changeList[cg].fromG)
                                {
                                    // AND compare blue
                                    if (Keywdels[centiSec].bluIntensity == changeList[cg].fromB)
                                    {
                                        // if all 3 colors match the current FROM color
                                        // change it to the current TO color
                                        if (cg == 4)
                                        { sMsg = "B to O"; }
                                        Keywdels[centiSec].redIntensity = changeList[cg].toR;
                                        Keywdels[centiSec].grnIntensity = changeList[cg].toG;
                                        Keywdels[centiSec].bluIntensity = changeList[cg].toB;
                                        // force exit of loop thru change list
                                        // (Since some colors may be SWAPPED and we don't want to change it,
                                        //   then change it back again!)
                                        cg = 99;
                                    } // End blue match
                                } // End green match
                            } // End Red match
                        } // end ChangeList loop
                    } // end centisecond loop

                    if (showChanges)
                    {
                        for (int i = 0; i < seq.Centiseconds; i++)
                        {
                            fp.setKeywdel(i, 1, Intensity(Keywdels[i].redIntensity), Intensity(Keywdels[i].grnIntensity), Intensity(Keywdels[i].bluIntensity));
                        }
                        fp.ShowDialog();
                    }


                    // ** BUILD NEW EFFECTS **

                    // ** RED STARTS HERE **

                    // now create NEW Effects lists for the Red channel
                    Effect[] replaceEffs = new Effect[1];
                    Effect newE = new Effect();
                    replaceCount = 0;
                    // Reset to initial values of BLACK
                    redIntensity = 0;
                    centisecond = 0;
                    // Is the Red turned on at the very first centisecond (0)?
                    p = Keywdels[0];
                    if (p.redIntensity > 0)
                    {
                        // If New Intensity then Create New Effect
                        redIntensity = p.redIntensity;
                        newE.channelIndex = redChannel.Index;
                        newE.SavedIndex = seq.Channels[redChannel.Index].SavedIndex;
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
                            newE.Intensity = p.redIntensity;
                        }

                        centisecond = 1;
                        // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                        while (centisecond < seq.Centiseconds + 1)
                        {
                            // get pixel for current centisecond
                            p = Keywdels[centisecond];
                            // did the anything change?
                            if (!p.redEquals(Keywdels[centisecond - 1]))
                            {
                                // save end centisecond
                                newE.endCentisecond = centisecond;
                                // add this to the array of replacement Effects, incr counter
                                Array.Resize(ref replaceEffs, replaceCount + 1);
                                replaceEffs[replaceCount] = newE;
                                replaceCount++;
                                // get ready to continue for loop above  (4 levels <-) by:
                                //    creating a new Effect,
                                //       and backing the centisecond up by 1 (in case the next Effect needs to start immediately after this one)
                                newE = new Effect();
                                centisecond--;
                                break; // from while centisecond loop
                            }  // end if (p.redIntensity != redIntensity) indicating end centisecond
                            // If we didn't find the change, and thus didn't break out of this while loop
                            centisecond++;
                        } // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
                    } // end if new Intensity > 0
                    else
                    {
                        centisecond = 1;
                    }
                    // set new Current Intensity
                    redIntensity = p.redIntensity;


                    // Now, start checking from end of the last Effect, comparing to the previous centisecond
                    while (centisecond <= seq.Centiseconds)
                    {
                        //updateProgress(2, redChannel.Index, centisecond);
                        // get pixel for current centisec
                        p = Keywdels[centisecond];
                        // Did the Intensity change?
                        if (!p.redEquals(Keywdels[centisecond - 1]))
                        {
                            if (p.redIntensity > 0)
                            {
                                // If New Intensity then Create New Effect
                                redIntensity = p.redIntensity;
                                newE.channelIndex = redChannel.Index;
                                newE.SavedIndex = seq.Channels[redChannel.Index].SavedIndex;
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
                                    newE.Intensity = p.redIntensity;
                                }

                                centisecond++;
                                // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                                while (centisecond < seq.Centiseconds + 1)
                                {
                                    // get pixel for current centisecond
                                    p = Keywdels[centisecond];
                                    // did the Intensity change?
                                    if (p.redIntensity != redIntensity)
                                    {
                                        // save end centisecond
                                        newE.endCentisecond = centisecond;
                                        // add this to the array of replacement Effects, incr counter
                                        Array.Resize(ref replaceEffs, replaceCount + 1);
                                        replaceEffs[replaceCount] = newE;
                                        replaceCount++;
                                        // get ready to continue for loop above  (4 levels <-) by:
                                        //    creating a new Effect, and backing the centisecond up by 1
                                        newE = new Effect();
                                        centisecond--;
                                        break;
                                    }  // end if (p.redIntensity != redIntensity) indicating end centisecond
                                    // If we didn't find the change, and thus didn't break out of this while loop
                                    centisecond++;
                                } // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
                            } // end if new Intensity > 0
                            // set new Current Intensity
                            redIntensity = p.redIntensity;
                        } // end if Intensity changed
                        centisecond++;
                    } // end for loop thru all centiseconds

                    // now REPLACE the Effects list for this Red channel
                    for (int fx = 0; fx < replaceCount; fx++)
                    {
                        // Loop thru Replacement Effects (for this Red channel) and add them to the master list of all new Effects
                        //    (including those we copied from the non-rgb Channels, near the start of this procedure)
                        Array.Resize(ref NEWeffects, newEffectCount + 1);
                        NEWeffects[newEffectCount] = replaceEffs[fx];
                        newEffectCount++;
                        effectIndex++;
                    }
                    // END Red        

                    // ** GREEN STARTS HERE **

                    // now create NEW Effects lists for the Greenchannel
                    replaceEffs = new Effect[1];
                    newE = new Effect();
                    replaceCount = 0;
                    // Reset to initial values of BLACK
                    grnIntensity = 0;
                    centisecond = 0;

                    // Is the Greenturned on at the very first centisecond (0)?
                    p = Keywdels[0];
                    if (p.grnIntensity > 0)
                    {
                        // If New Intensity then Create New Effect
                        grnIntensity = p.grnIntensity;
                        newE.channelIndex = grnChannel.Index;
                        newE.SavedIndex = seq.Channels[grnChannel.Index].SavedIndex;
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
                            newE.Intensity = p.grnIntensity;
                        }

                        centisecond = 1;
                        // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                        while (centisecond < seq.Centiseconds + 1)
                        {
                            // get pixel for current centisecond
                            p = Keywdels[centisecond];
                            // did the anything change?
                            if (!p.grnEquals(Keywdels[centisecond - 1]))
                            {
                                // save end centisecond
                                newE.endCentisecond = centisecond;
                                // add this to the array of replacement Effects, incr counter
                                Array.Resize(ref replaceEffs, replaceCount + 1);
                                replaceEffs[replaceCount] = newE;
                                replaceCount++;
                                // get ready to continue for loop above  (4 levels <-) by:
                                //    creating a new Effect, and backing the centisecond up by 1
                                newE = new Effect();
                                centisecond--;
                                break;
                            }  // end if (p.grnIntensity != grnIntensity) indicating end centisecond
                            // If we didn't find the change, and thus didn't break out of this while loop
                            centisecond++;
                        } // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
                    } // end if new Intensity > 0
                    else
                    {
                        centisecond = 1;
                    }
                    // set new Current Intensity
                    grnIntensity = p.grnIntensity;


                    // Now, start checking from end of the last Effect, comparing to the previous centisecond
                    while (centisecond <= seq.Centiseconds)
                    {
                        //updateProgress(2, grnChannel.Index, centisecond);
                        // get pixel for current centisec
                        p = Keywdels[centisecond];
                        // Did the Intensity change?
                        if (!p.grnEquals(Keywdels[centisecond - 1]))
                        {
                            if (p.grnIntensity > 0)
                            {
                                // If New Intensity then Create New Effect
                                grnIntensity = p.grnIntensity;
                                newE.channelIndex = grnChannel.Index;
                                newE.SavedIndex = seq.Channels[grnChannel.Index].SavedIndex;
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
                                    newE.Intensity = p.grnIntensity;
                                }

                                centisecond++;
                                // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                                while (centisecond < seq.Centiseconds + 1)
                                {
                                    // get pixel for current centisecond
                                    p = Keywdels[centisecond];
                                    // did the Intensity change?
                                    if (p.grnIntensity != grnIntensity)
                                    {
                                        // save end centisecond
                                        newE.endCentisecond = centisecond;
                                        // add this to the array of replacement Effects, incr counter
                                        Array.Resize(ref replaceEffs, replaceCount + 1);
                                        replaceEffs[replaceCount] = newE;
                                        replaceCount++;
                                        // get ready to continue for loop above  (4 levels <-) by:
                                        //    creating a new Effect, and backing the centisecond up by 1
                                        newE = new Effect();
                                        centisecond--;
                                        break;
                                    }  // end if (p.grnIntensity != grnIntensity) indicating end centisecond
                                    // If we didn't find the change, and thus didn't break out of this while loop
                                    centisecond++;
                                } // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
                            } // end if new Intensity > 0
                            // set new Current Intensity
                            grnIntensity = p.grnIntensity;
                        } // end if Intensity changed
                        centisecond++;
                    } // end for loop thru all centiseconds

                    // now REPLACE the Effects list for this Greenchannel
                    for (int fx = 0; fx < replaceCount; fx++)
                    {
                        // Loop thru Replacement Effects (for this Greenchannel) and add them to the master list of all new Effects
                        //    (including those we copied from the non-rgb Channels, near the start of this procedure)
                        Array.Resize(ref NEWeffects, newEffectCount + 1);
                        NEWeffects[newEffectCount] = replaceEffs[fx];
                        newEffectCount++;
                        effectIndex++;
                    }
                    // END GREEN

                    // ** BLUE STARTS HERE **
                    replaceEffs = new Effect[1];
                    newE = new Effect();
                    replaceCount = 0;
                    // Reset to initial values of BLACK
                    bluIntensity = 0;
                    centisecond = 0;
                    // Is the Blue turned on at the very first centisecond (0)?
                    p = Keywdels[0];
                    if (p.bluIntensity > 0)
                    {
                        // If New Intensity then Create New Effect
                        bluIntensity = p.bluIntensity;
                        newE.channelIndex = bluChannel.Index;
                        newE.SavedIndex = seq.Channels[bluChannel.Index].SavedIndex;
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
                            newE.Intensity = p.bluIntensity;
                        }

                        centisecond = 1;
                        // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                        while (centisecond < seq.Centiseconds + 1)
                        {
                            // get pixel for current centisecond
                            p = Keywdels[centisecond];
                            // did the anything change?
                            if (!p.bluEquals(Keywdels[centisecond - 1]))
                            {
                                // save end centisecond
                                newE.endCentisecond = centisecond;
                                // add this to the array of replacement Effects, incr counter
                                Array.Resize(ref replaceEffs, replaceCount + 1);
                                replaceEffs[replaceCount] = newE;
                                replaceCount++;
                                // get ready to continue for loop above  (4 levels <-) by:
                                //    creating a new Effect,
                                //       and backing the centisecond up by 1 (in case the next Effect needs to start immediately after this one)
                                newE = new Effect();
                                centisecond--;
                                break; // from while centisecond loop
                            }  // end if (p.bluIntensity != bluIntensity) indicating end centisecond
                            // If we didn't find the change, and thus didn't break out of this while loop
                            centisecond++;
                        } // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
                    } // end if new Intensity > 0
                    else
                    {
                        centisecond = 1;
                    }
                    // set new Current Intensity
                    bluIntensity = p.bluIntensity;


                    // Now, start checking from end of the last Effect, comparing to the previous centisecond
                    while (centisecond <= seq.Centiseconds)
                    {
                        //updateProgress(2, bluChannel.Index, centisecond);
                        // get pixel for current centisec
                        p = Keywdels[centisecond];
                        // Did the Intensity change?
                        if (!p.bluEquals(Keywdels[centisecond - 1]))
                        {
                            if (p.bluIntensity > 0)
                            {
                                // If New Intensity then Create New Effect
                                bluIntensity = p.bluIntensity;
                                newE.channelIndex = bluChannel.Index;
                                newE.SavedIndex = seq.Channels[bluChannel.Index].SavedIndex;
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
                                    newE.Intensity = p.bluIntensity;
                                }

                                centisecond++;
                                // Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
                                while (centisecond < seq.Centiseconds + 1)
                                {
                                    // get pixel for current centisecond
                                    p = Keywdels[centisecond];
                                    // did the Intensity change?
                                    if (p.bluIntensity != bluIntensity)
                                    {
                                        // save end centisecond
                                        newE.endCentisecond = centisecond;
                                        // add this to the array of replacement Effects, incr counter
                                        Array.Resize(ref replaceEffs, replaceCount + 1);
                                        replaceEffs[replaceCount] = newE;
                                        replaceCount++;
                                        // get ready to continue for loop above  (4 levels <-) by:
                                        //    creating a new Effect, and backing the centisecond up by 1
                                        newE = new Effect();
                                        centisecond--;
                                        break;
                                    }  // end if (p.bluIntensity != bluIntensity) indicating end centisecond
                                    // If we didn't find the change, and thus didn't break out of this while loop
                                    centisecond++;
                                } // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
                            } // end if new Intensity > 0
                            // set new Current Intensity
                            bluIntensity = p.bluIntensity;
                        } // end if Intensity changed
                        centisecond++;
                    } // end for loop thru all centiseconds

                    // now REPLACE the Effects list for this Blue channel
                    for (int fx = 0; fx < replaceCount; fx++)
                    {
                        // Loop thru Replacement Effects (for this Blue channel) and add them to the master list of all new Effects
                        //    (including those we copied from the non-rgb Channels, near the start of this procedure)
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


            } // end While Loop thru ALL Channels

            Array.Resize(ref NEWeffects, newEffectCount + 1);
            NEWeffects[newEffectCount] = new Effect();

            seq.Effects = NEWeffects;
            seq.effectCount = newEffectCount;
            changesMade = true;

            return errState;

        }  // end ChangeColors

        private void btnOK_Click(object sender, EventArgs e)
        {

            btnOK.Enabled = false;
            grpFile.Enabled = false;
            grpColors.Enabled = false;
            
            string theFile = dlgFileOpen.InitialDirectory + "\\" + txtInputFilename.Text;

            seq = new Sequence4();
            seq.ReadSequenceFile(lastFile);

            string sMsg;
            sMsg = "File Parse Complete!\r\n\r\n";
            sMsg += seq.lineCount.ToString() + " lines\r\n";
            sMsg += seq.Channels.Count.ToString() + " Channels\r\n";
            sMsg += seq.RGBchannels.Count.ToString() + " RGB Channels\r\n";
            sMsg += seq.effectCount.ToString() + " Effects\r\n";
            sMsg += seq.channelGroupCount.ToString() + " groups\r\n";
            sMsg += seq.groupItemCount.ToString() + " group items";

            DialogResult mReturn;
            //mReturn = MessageBox.Show(sMsg, "File Parse Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);



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

		private void ReadInputFile(string filename)
		{
			Sequence4 seq = new Sequence4();
			seq.ReadClipboardFile(filename);
		}

		private void WriteOutputFile(string filename)
		{
			seq.WriteClipboardFile(filename);
		}


        private int SavePresets()
        {
            int errStatus = 0;

            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";
            string fileName = basePath + "ColorChanger Presets.xml";

            StreamWriter writer = new StreamWriter(fileName);
            string lineOut; // line read in (does not get modified)

            lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
            writer.WriteLine(lineOut);

            lineOut = "<"  + TABLEpresetSet  + "s>";
            writer.WriteLine (lineOut);

           
            for (int i=0; i<presetSetCount ; i++)
            {
                if (presetSets[i].changeCount > 0)
                {
                    lineOut = "  <" + TABLEpresetSet;                
                    lineOut += SPC + FIELDname + FIELDEQ + presetSets[i].name + ENDQT + "/>";
                    writer.WriteLine (lineOut);
                    lineOut = "    <"  + TABLEcolorChange  + "s>";
                    writer.WriteLine (lineOut);
            
                    for (int j=0; j < presetSets[i].changeCount; j++)
                    {
                        lineOut = "      <" + TABLEcolorChange;
                        lineOut += SPC + FIELDfromName + FIELDEQ + presetSets[i].colorChanges[j].fromName + ENDQT;
                        lineOut += SPC + FIELDfromR + FIELDEQ + presetSets[i].colorChanges[j].fromR.ToString() + ENDQT;
                        lineOut += SPC + FIELDfromG + FIELDEQ + presetSets[i].colorChanges[j].fromG.ToString() + ENDQT;
                        lineOut += SPC + FIELDfromB + FIELDEQ + presetSets[i].colorChanges[j].fromB.ToString() + ENDQT;
                        lineOut += SPC + FIELDtoName + FIELDEQ + presetSets[i].colorChanges[j].toName + ENDQT;
                        lineOut += SPC + FIELDtoR + FIELDEQ + presetSets[i].colorChanges[j].toR.ToString() + ENDQT;
                        lineOut += SPC + FIELDtoG + FIELDEQ + presetSets[i].colorChanges[j].toG.ToString() + ENDQT;
                        lineOut += SPC + FIELDtoB + FIELDEQ + presetSets[i].colorChanges[j].toB.ToString() + ENDQT;
                        lineOut += "/>";
                        writer.WriteLine(lineOut);
                    }
            
                    lineOut = "    </"  + TABLEcolorChange  + "s>";
                    writer.WriteLine (lineOut);
                    lineOut = "  </"  + TABLEpresetSet  + ">";
                    writer.WriteLine (lineOut);
                }
            }

            lineOut = "</"  + TABLEpresetSet  + "s>";
            writer.WriteLine (lineOut);

            return errStatus;
        }

        private int LoadPresets()
        {
            int errStatus = 0;

            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama 2015\\Sequences\\";
            string fileName = basePath + "ColorChanger Presets.xml";

            StreamReader reader = new StreamReader(fileName);
            string lineIn; // line read in (does not get modified)
            int pos1 = -1; // positions of certain key text in the line
            int lineCount = 0;

            // Zero these out from any previous run
            presetSetCount = 0;
            colorChangeCount = 0;

            int curPreset = -1;
            int curChange = -1;

            ColorChange[] colorChanges = new ColorChange[1];
            

            // * PASS 1 - COUNT OBJECTS
            while ((lineIn = reader.ReadLine()) != null)
            {
                lineCount++;
                // does this line mark the start of a preset set?
                pos1 = lineIn.IndexOf("<presetSet name=");
                if (pos1 > 0)
                {
                    presetSetCount++;
                }
                pos1 = lineIn.IndexOf("<colorChange fromName=");
                if (pos1 > 0)
                {
                    colorChangeCount++;
                }
            }
            reader.Close();

            // CREATE ARRAYS TO HOLD OBJECTS
            presetSets = new PresetSet[presetSetCount];

            // * PASS 2 - COLLECT OBJECTS
            reader = new StreamReader(fileName);
            while ((lineIn = reader.ReadLine()) != null)
            {
                lineCount++;
                // does this line mark the start of a preset set?
                pos1 = lineIn.IndexOf(TABLEpresetSet + " " + FIELDname);
                if (pos1 > 0)
                {
                    curPreset++;
                    PresetSet ps = new PresetSet();
                    ps.name = Sequence4.getKeyWord(lineIn, FIELDname);
                    presetSets[curPreset] = ps;

                    lineIn = reader.ReadLine();
                    lineIn = reader.ReadLine();
                    curChange = -1;
                    pos1 = lineIn.IndexOf(TABLEcolorChange + " " + FIELDfromName);
                    while (pos1 > 0)
                    {
                        curChange++;
                        Array.Resize (ref colorChanges,curChange+1);
                        ColorChange cc = new ColorChange();
                        cc.fromName = Sequence4.getKeyWord (lineIn,FIELDfromName );
                        cc.fromR = Sequence4.getKeyValue(lineIn, FIELDfromR);
                        cc.fromG = Sequence4.getKeyValue(lineIn, FIELDfromG);
                        cc.fromB = Sequence4.getKeyValue(lineIn, FIELDfromB);
                        cc.toName = Sequence4.getKeyWord(lineIn, FIELDtoName);
                        cc.toR = Sequence4.getKeyValue(lineIn, FIELDtoR);
                        cc.toG = Sequence4.getKeyValue(lineIn, FIELDtoG);
                        cc.toB = Sequence4.getKeyValue(lineIn, FIELDtoB);
                        colorChanges[curChange]=cc;

                        lineIn = reader.ReadLine();
                        pos1 = lineIn.IndexOf(TABLEcolorChange + " " + FIELDfromName);
                    }
                    if (curChange > -1)
                    {
                        ps.colorChanges = colorChanges;
                        ps.changeCount = curChange + 1;
                        presetSets[curPreset] = ps;
                    }
                }

            }

            reader.Close();

                /*
            string sMsg;
            sMsg = "File Parse Complete!\r\n\r\n";
            sMsg += lineCount.ToString() + " lines\r\n";
            sMsg += seq.Channels.Count.ToString() + " Channels\r\n";
            sMsg += RGBchannels.Count.ToString() + " RGB Channels\r\n";
            sMsg += seq.effectCount.ToString() + " Effects\r\n";
            sMsg += groupCount.ToString() + " groups\r\n";
            sMsg += groupItemCount.ToString() + " group items";

            DialogResult mReturn;
            mReturn = MessageBox.Show(sMsg, "File Parse Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            */    

            return errStatus;
        }

        private void fillPresetList()
        {
            cboPresets.Items.Clear();
            for (int i = 0; i < presetSetCount; i++)
            {
                cboPresets.Items.Add(presetSets[i].name);
            }
            //cboPresets.SelectedIndex = 0;  // TODO: Normal one!  Restore this after debugging
            cboPresets.SelectedIndex = 3;    // for debugging purposes
        }

        private void cboPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            PresetSet ps = presetSets[cboPresets.SelectedIndex];
            ColorChange[] css = ps.colorChanges;
            string txt;

            txtColorFrom1.Text = "";
            txtColorFrom2.Text = "";
            txtColorFrom3.Text = "";
            txtColorFrom4.Text = "";
            txtColorFrom5.Text = "";
            txtColorFrom6.Text = "";
            txtColorTo1.Text = "";
            txtColorTo2.Text = "";
            txtColorTo3.Text = "";
            txtColorTo4.Text = "";
            txtColorTo5.Text = "";
            txtColorTo6.Text = "";
            picFrom1.BackColor = grpColors.BackColor;
            picFrom2.BackColor = grpColors.BackColor;
            picFrom3.BackColor = grpColors.BackColor;
            picFrom4.BackColor = grpColors.BackColor;
            picFrom5.BackColor = grpColors.BackColor;
            picFrom6.BackColor = grpColors.BackColor;
            picTo1.BackColor = grpColors.BackColor;
            picTo2.BackColor = grpColors.BackColor;
            picTo3.BackColor = grpColors.BackColor;
            picTo4.BackColor = grpColors.BackColor;
            picTo5.BackColor = grpColors.BackColor;
            picTo6.BackColor = grpColors.BackColor;


            if (ps.changeCount  > 0)
            {
                txt = css[0].fromR.ToString() + "," + css[0].fromG.ToString() + "," + css[0].fromB.ToString();
                txtColorFrom1.Text = txt;
                picFrom1.BackColor = makeColor (css[0].fromR, css[0].fromG, css[0].fromB);
                txt = css[0].toR.ToString() + "," + css[0].toG.ToString() + "," + css[0].toB.ToString();
                txtColorTo1.Text = txt;
                picTo1.BackColor = makeColor(css[0].toR, css[0].toG, css[0].toB);
            }
            if (ps.changeCount > 1)
            {
                txt = css[1].fromR.ToString() + "," + css[1].fromG.ToString() + "," + css[1].fromB.ToString();
                txtColorFrom2.Text = txt;
                picFrom2.BackColor = makeColor(css[1].fromR, css[1].fromG, css[1].fromB);
                txt = css[1].toR.ToString() + "," + css[1].toG.ToString() + "," + css[1].toB.ToString();
                txtColorTo2.Text = txt;
                picTo2.BackColor = makeColor(css[1].toR, css[1].toG, css[1].toB);
            }
            if (ps.changeCount > 2)
            {
                txt = css[2].fromR.ToString() + "," + css[2].fromG.ToString() + "," + css[2].fromB.ToString();
                txtColorFrom3.Text = txt;
                picFrom3.BackColor = makeColor(css[2].fromR, css[2].fromG, css[2].fromB);
                txt = css[2].toR.ToString() + "," + css[2].toG.ToString() + "," + css[2].toB.ToString();
                txtColorTo3.Text = txt;
                picTo3.BackColor = makeColor(css[2].toR, css[2].toG, css[2].toB);
            }
            if (ps.changeCount > 3)
            {
                txt = css[3].fromR.ToString() + "," + css[3].fromG.ToString() + "," + css[3].fromB.ToString();
                txtColorFrom4.Text = txt;
                picFrom4.BackColor = makeColor(css[3].fromR, css[3].fromG, css[3].fromB);
                txt = css[3].toR.ToString() + "," + css[3].toG.ToString() + "," + css[3].toB.ToString();
                txtColorTo4.Text = txt;
                picTo4.BackColor = makeColor(css[3].toR, css[3].toG, css[3].toB);
            }
            if (ps.changeCount > 4)
            {
                txt = css[4].fromR.ToString() + "," + css[4].fromG.ToString() + "," + css[4].fromB.ToString();
                txtColorFrom5.Text = txt;
                picFrom5.BackColor = makeColor(css[4].fromR, css[4].fromG, css[4].fromB);
                txt = css[4].toR.ToString() + "," + css[4].toG.ToString() + "," + css[4].toB.ToString();
                txtColorTo5.Text = txt;
                picTo5.BackColor = makeColor(css[4].toR, css[4].toG, css[4].toB);
            }
            if (ps.changeCount > 5)
            {
                txt = css[5].fromR.ToString() + "," + css[5].fromG.ToString() + "," + css[5].fromB.ToString();
                txtColorFrom6.Text = txt;
                picFrom6.BackColor = makeColor(css[5].fromR, css[5].fromG, css[5].fromB);
                txt = css[5].toR.ToString() + "," + css[5].toG.ToString() + "," + css[5].toB.ToString();
                txtColorTo6.Text = txt;
                picTo6.BackColor = makeColor(css[5].toR, css[5].toG, css[5].toB);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }


        private int Intensity(int amount)
        {
            return Convert.ToInt16(amount * 2.5);
        }

        public static Color makeColor(int Rd, int Gn, int Bl)
        {
            Color nc = Color.Gray;
            int r2 = Convert.ToInt16(Rd * 2.5);
            int g2 = Convert.ToInt16(Gn * 2.5);
            int b2 = Convert.ToInt16(Bl * 2.5);
            nc = Color.FromArgb (r2, g2, b2);
            return nc;
        }

        public EffectType  GetBestEffectType(EffectType FirstType, EffectType SecondType, EffectType ThirdType)
        {
            EffectType returnType = EffectType.Intensity;

            if (FirstType > EffectType.None)
            { 
                returnType = FirstType ;
            }
            else if (SecondType > EffectType.None)
            { 
                returnType = SecondType;
            }
            else if (ThirdType > EffectType.None)
            {
                returnType = ThirdType;
            }
            

            return returnType;
        }

 
        public void updateProgress(int chanNo, int centiSecond)
        {

            int pct = chanNo * 100 / seq.Channels.Count;
            int pi = Convert.ToInt16(pct);
            prgProgress.Value = pi;

            string sMsg = "Channel " + (chanNo+1).ToString() + " of " + seq.Channels.Count.ToString();
            //sMsg += ", Pass " + member.ToString() + " of 2";
            //sMsg += ", Centisecond " + centiSecond.ToString() + " of " + seq.Centiseconds.ToString();
            //Debug.Print(sMsg);
            staInfo1.Text = sMsg;
            staStatus.Refresh();

        } // end Form

        public struct rgbKeywd
        {
            public int r;
            public int g;
            public int b;
        }

		private void frmColors_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();
		}
	} // end Namespace

    
  

}
