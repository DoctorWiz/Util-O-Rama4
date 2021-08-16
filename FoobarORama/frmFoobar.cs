using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LORUtils; using FileHelper;
using System.IO;
using System.Media;

namespace UtilORama4
{
	public partial class frmFoobar : Form
	{
		Sequence4 seq;
		string fileName = "";
		int errCount = 0;
		bool aborted = false;

		public frmFoobar()
		{
			InitializeComponent();
		}

		private void frmFoobar_Load(object sender, EventArgs e)
		{

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

		private void lblFile_Click(object sender, EventArgs e)
		{
			dlgFileOpen.InitialDirectory = utils.DefaultSequencesPath;
			dlgFileOpen.Filter = "Sequence Files *.las, *.lms|*.las;*.lms|Musical Sequence Files *.lms|*.lms|Animated Sequence Files *.las|*.las";
			dlgFileOpen.FilterIndex = 1;
			dlgFileOpen.Multiselect = false;
			dlgFileOpen.SupportMultiDottedExtensions = true;
			dlgFileOpen.Title = "Choose a Sequence to Foobar";
			dlgFileOpen.CheckFileExists = true;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.ValidateNames = true;
			DialogResult dr = dlgFileOpen.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				lblFile.Text = Path.GetFileName(dlgFileOpen.FileName);
				errCount = 0;

				//! PERFORM NEEDED TEST(S)
				GroupingTests(dlgFileOpen.FileName);

				if (aborted)
				{
					string msg = "Scan aborted after ";
					msg += errCount.ToString() + " errors identified.";
					DialogResult rr = MessageBox.Show(this, msg, "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);

				}
				else
				{
					if (errCount > 0)
					{
						string msg = "No known errors found.";
						DialogResult rr = MessageBox.Show(this, msg, "No Errors", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						string msg = errCount.ToString() + " errors identified.";
						DialogResult rr = MessageBox.Show(this, msg, "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void GroupingTests(string theFile)
		{
			////////////////////////////////////////////////////////////////////
			//
			// Tests for Grouping problems, trying to solve error
			// "This channel group is already in the channel group list"
			//
			////////////////////////////////////////////////////////////////////

			StreamReader reader = new StreamReader(theFile);
			int sid = utils.UNDEFINED;
			MemberType typ = MemberType.None;
			string name = "";
			string info = "";
			int highID = utils.UNDEFINED;
			string lineIn = reader.ReadLine();
			int p = lineIn.IndexOf(Sequence4.STARTchannel);
			int lineNumber = 0;
			string[] data = new string[7777];
			int[] childIDs = null;
			int childCount = 0;
			int lastGroupID = utils.UNDEFINED;
			pnlInfo.Visible = true;
			bool gotOne = false;
			bool ignoreDupes = false;
			bool ignoreCircles = false;
			bool ignoreConflicts = false;
			bool ignoreEmpty = false;



			while (!reader.EndOfStream && !aborted)
			{
				lineIn = reader.ReadLine();
				lineNumber++;
				gotOne = false;

				// Test 1 - Collect SavedIndexes of ALL Channels, RGB Channels, and Channel Groups
				//    Look for duplicates
				p = lineIn.IndexOf(Sequence4.STARTchannel);
				if (p>0)
				{
					typ = MemberType.Channel;
					gotOne = true;
				}
				p = lineIn.IndexOf(Sequence4.STARTrgbChannel);
				if (p > 0)
				{
					typ = MemberType.RGBchannel;
					gotOne = true;
				}
				p = lineIn.IndexOf(Sequence4.STARTchannelGroup);
				if (p > 0)
				{
					typ = MemberType.ChannelGroup;
					gotOne = true;
				}
				if (gotOne)
				{
					sid = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
					if (sid > highID) highID = sid;

					// Test 2 - Check for duplicate children
					// Test 3 - Check for circular reference (group includes itself)
					if (typ == MemberType.ChannelGroup) 
					{
						// First, check previous group
						if (childCount > 0)
						{
							Array.Resize(ref childIDs, childCount);
							Array.Sort(childIDs);
							for (int c=1; c < childCount; c++)
							{
								if (childIDs[c-1] == childIDs[c])
								{
									// Duplicate Members!!
									errCount++;
									if (!ignoreDupes && !aborted)
									{
										string[] zw = data[lastGroupID].Split(utils.DELIM1);
										string msg = "Duplicate Members in Group: " + zw[0] + "\r\n";
										msg += "SavedIndex=" + lastGroupID.ToString() + " starting at line " + zw[2] + "\r\n";
										string[] wz = data[childIDs[c]].Split(utils.DELIM1);
										msg += "Child: " + wz[0] + "\r\n";
										msg += "Saved Index=" + childIDs[c] + " Included more than once!";
										DialogResult dr = MessageBox.Show(this, msg, "Duplicate Child", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation);
										if (dr == DialogResult.Ignore) ignoreDupes = true;
										if (dr == DialogResult.Abort)
										{
											aborted = true;
											ignoreDupes = true;
										}

									}
								}
								if (childIDs[c] == lastGroupID)
								{
									// Circular Reference!!
									errCount++;
									if (!ignoreCircles && !aborted)
									{
										string[] zw = data[lastGroupID].Split(utils.DELIM1);
										string msg = "Circular Reference in Group: " + zw[0] + "\r\n";
										msg += "SavedIndex=" + lastGroupID.ToString() + " starting at line " + zw[2] + "\r\n";
										DialogResult dr = MessageBox.Show(this, msg, "Circular Reference", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation);
										if (dr == DialogResult.Ignore) ignoreCircles = true;
										if (dr == DialogResult.Abort)
										{
											aborted = true;
											ignoreCircles = true;
										}
									}
								}
							}
							if (childIDs[childCount-1] == lastGroupID)
							{
								// Circular Reference!!
								errCount++;
								if (!ignoreCircles & !aborted)
								{
									string[] zw = data[lastGroupID].Split(utils.DELIM1);
									string msg = "Circular Reference in Group: " + zw[0] + "\r\n";
									msg += "SavedIndex=" + lastGroupID.ToString() + " starting at line " + zw[2] + "\r\n";
									DialogResult dr = MessageBox.Show(this, msg, "Circular Reference", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
									if (dr == DialogResult.Ignore) ignoreCircles = true;
									if (dr == DialogResult.Abort)
									{
										aborted = true;
										ignoreCircles = true;
									}
								}
							}
						}
						// Finished checking previous group
						// Setup for checking this group
						childIDs = new int[50];
						childCount = 0;
						lastGroupID = sid;
					} // end if group

					name = utils.getKeyWord(lineIn, utils.FIELDname);
					info = name;
					info += utils.DELIM1 + SeqEnums.MemberName(typ);
					info += utils.DELIM1 + lineNumber.ToString();
					if (data[sid] == null)
					{
						data[sid] = info;
						lblNum.Text = sid.ToString();
						lblName.Text = name;
						lblType.Text = SeqEnums.MemberName(typ);
						pnlInfo.Refresh();
					}
					else
					{
						errCount++;
						if (!ignoreConflicts && !aborted)
						{
							string msg = "Conflict found on line " + lineNumber.ToString();
							string[] zw = data[sid].Split(utils.DELIM1);
							msg += " with line " + zw[2] + "\r\n";
							msg += zw[1] + " SavedIndex " + sid.ToString() + "\r\n";
							msg += zw[0] + "\r\n";
							msg += name;
							DialogResult dr = MessageBox.Show(this, msg, "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							if (dr == DialogResult.Ignore) ignoreConflicts = true;
							if (dr == DialogResult.Abort)
							{
								aborted = true;
								ignoreConflicts = true;
							}
						}
					} // end if saved item = null, or not
				} // End if Channel, RGB Channel, or Channel Group was found

				p = lineIn.IndexOf("<channelGroup savedInde");
				if (p>0)
				{
					// Group Child Item
					sid = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
					childIDs[childCount] = sid;
					childCount++;
				}

			} // End while lines remain
			reader.Close();

			if (!aborted)
			{
				// Test 4 - look for missing SavedIndexes
				for (int s = 0; s <= highID; s++)
				{
					if (data[s] == null)
					{
						errCount++;
						if (!ignoreEmpty && !aborted)
						{
							string msg = "Missing Item: SavedIndex " + s.ToString() + " not found.";
							DialogResult dr = MessageBox.Show(this, msg, "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							if (dr == DialogResult.Ignore) ignoreEmpty = true;
							if (dr == DialogResult.Abort)
							{
								aborted = true;
								ignoreEmpty = true;
							}
						}
					}
				}
			}

			pnlInfo.Visible = false;

		}


		private void PrevFoobarIt(string theFile)
		{
			seq = new Sequence4(theFile);

			for (int idx =0; idx < seq.Channels.Count; idx++)
			{
				Channel ch = seq.Channels[idx];
				lblNum.Text = (idx + 1).ToString() + " of " + seq.Channels.Count.ToString();
				lblName.Text = ch.Name;
				lblNum.Refresh();
				lblName.Refresh();

				int isr = ch.Name.IndexOf("Keywdel");
				if (isr > 0)
				{
					isr = ch.Name.IndexOf("(R)");
					if (isr > 0)
					{
						int oldChanNo = ch.output.circuit;
						int newChanNo = oldChanNo + 1;
						ch.output.circuit = newChanNo;
						string name2 = ch.Name.Replace(oldChanNo.ToString().Trim(), newChanNo.ToString().Trim());
						ch.ChangeName(name2);
					}
					isr = ch.Name.IndexOf("(G)");
					if (isr > 0)
					{
						int oldChanNo = ch.output.circuit;
						int newChanNo = oldChanNo - 1;
						ch.output.circuit = newChanNo;
						string name2 = ch.Name.Replace(oldChanNo.ToString().Trim(), newChanNo.ToString().Trim());
						ch.ChangeName(name2);
					}
				}



			}

			string fdir = Path.GetDirectoryName(theFile) + "\\";
			string fnam = Path.GetFileNameWithoutExtension(theFile);
			string fext = Path.GetExtension(theFile);
			string fnew = fdir + fnam + " v17i GRB" + fext;

			seq.WriteSequenceFile_DisplayOrder(fnew);
			//seq.WriteSequenceFile(fnew);

			string snd = Path.GetDirectoryName(Application.ExecutablePath) + "\\laser.wav";
			SoundPlayer simpleSound = new SoundPlayer(@snd);
			simpleSound.Play();


		}



	}
}
