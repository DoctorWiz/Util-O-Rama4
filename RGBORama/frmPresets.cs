using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using UtilORama;
using LORUtils; using FileHelper;
using static Globals;
using Shell32;

namespace RGBORama
{
	public partial class frmPresets : Form
	{

		public bool dirty = false;
		public bool changed = false;
		//public lorColor[] colorList = null;
		public Preset[] curPresets;
		public string curPresetName = "";
		public bool curIsFav = true;
		public int colorCount = 0;
		public int panelCount = 1;

		private int presetCount = 0;

		private bool pickRepl = false;
		private int picNumber = 0;






		public frmPresets()
		{
			InitializeComponent();
		}

		private void SetTheControlsForTheHeartOfTheSun()
		{
			string lastPreset = Properties.Settings.Default.LastPreset;
		}

		private int FillPresetList(bool JustFavs)
		{
			int count = 0;
			int foundAt = -1;
			try
			{

				DirectoryInfo di = new DirectoryInfo(utils.DefaultChannelConfigsPath);
				string pattern = "*" + PRESETEXT;
				if (JustFavs)
				{
					// Add the Unicode U+2605 BLACK STAR character to the end of the extension
					// to get ONLY files ending in ".RGB★"
					// NOTE: This is the actuall star symbol, as used for Favorites, NOT to be confused with the Asterick
					pattern += STAR;
				}
				else
				{
					// Add the Asterick * wildcard character to the end of the extension
					// To get both favorite and not-favorite preset files
					pattern += "*";
				}
				FullDirList(di, pattern);

				bool izFav = false;
				cboPresets.Items.Clear();
				foreach (FileInfo fi in files)
				{
					string txt = fi.Name;
					int pz = fi.Extension.IndexOf(STAR);
					if (pz > 0) izFav = true; else izFav = false;
					if (izFav) txt = STAR + " " + txt;
					cboPresets.Items.Add(txt);
					if (curPresetName.ToLower().CompareTo(txt.ToLower()) == 0) foundAt = count;
					count++;
				}
				if (foundAt >= 0) cboPresets.SelectedIndex = foundAt;
				if (dirty) cboPresets.Font = new Font(cboPresets.Font, FontStyle.Italic);
				else cboPresets.Font = new Font(cboPresets.Font, FontStyle.Regular);
			}
			catch
			{
				System.Diagnostics.Debugger.Break();
			}
			return count;
		}

		private void frmPresets_Load(object sender, EventArgs e)
		{

		}

		public DialogResult Show(string lastPresetName, bool wasDirty, Preset[] lastPresets)
		{
			// COPY the presets sent over from the main form (do not create reference)
			Array.Resize(ref curPresets, lastPresets.Length);
			for (int i = 0; i < lastPresets.Length; i++)
			{
				curPresets[i].FindRedPct = lastPresets[i].FindRedPct;
				curPresets[i].FindGrnPct = lastPresets[i].FindGrnPct;
				curPresets[i].FindBluPct = lastPresets[i].FindBluPct;
				curPresets[i].FindColorName = lastPresets[i].FindColorName;
				curPresets[i].ReplRedPct = lastPresets[i].ReplRedPct;
				curPresets[i].ReplGrnPct = lastPresets[i].ReplGrnPct;
				curPresets[i].ReplBluPct = lastPresets[i].ReplBluPct;
				curPresets[i].ReplColorName = lastPresets[i].ReplColorName;
			}
			changed = false;
			Colors_Refresh();

			FillPresetList(optFavs.Checked);



			return this.DialogResult;
		}




		private void MakeDirty()
		{
			dirty = true;
			changed = true;
			cboPresets.Font = new Font(cboPresets.Font, FontStyle.Italic);
		}


		#region Preset Files Operations

		private int LoadPreset(string theName)
		{
			int count = 0;

			string theFile = utils.DefaultChannelConfigsPath + theName + PRESETEXT;
			try
			{
				if (!System.IO.File.Exists(theFile))
				{
					theFile += STAR;
				}
				if (!System.IO.File.Exists(theFile))
				{
					System.IO.StreamReader reader = new System.IO.StreamReader(theFile);
					while (!reader.EndOfStream)
					{
						// While lines remain, get the next line
						string lineIn = reader.ReadLine();
						// Check for comments, and split the comment from the data
						string dat = lineIn;
						string comment = "";
						int sep = lineIn.IndexOf('#');
						if (sep < 0) sep = lineIn.IndexOf(';');
						if (sep < 0) sep = lineIn.IndexOf("\\\\");
						if (sep > 0)
						{
							dat = lineIn.Substring(0, sep);
							comment = lineIn.Substring(sep);
							string[] values = dat.Split(',');
							// Does the data consist of 6 numbers?
							if (values.Length == 6)
							{
								Preset set = new Preset();
								short pct = 0;
								Int16.TryParse(values[0], out pct);
								set.FindRedPct = (byte)pct;
								Int16.TryParse(values[1], out pct);
								set.FindGrnPct = (byte)pct;
								Int16.TryParse(values[2], out pct);
								set.FindBluPct = (byte)pct;
								Int16.TryParse(values[3], out pct);
								set.ReplRedPct = (byte)pct;
								Int16.TryParse(values[4], out pct);
								set.ReplGrnPct = (byte)pct;
								Int16.TryParse(values[5], out pct);
								set.ReplBluPct = (byte)pct;

								sep = comment.IndexOf(" to ");
								if (sep > 0)
								{
									string find = comment.Substring(0, sep);
									string repl = comment.Substring(sep + 4);
									set.FindColorName = find;
									set.ReplColorName = repl;
								}
								else
								{
									int lc = set.FindGrnPct << 16;
									lc += set.FindBluPct << 8;
									lc += set.FindRedPct;
									string find = NearestColor.FindNearestColorName(lc);
									set.FindColorName = find;

									lc = set.ReplGrnPct << 16;
									lc += set.ReplBluPct << 8;
									lc += set.ReplRedPct;
									string repl = NearestColor.FindNearestColorName(lc);
									set.ReplColorName = repl;
								}



								count++;
								Array.Resize(ref curPresets, count);
								curPresets[count - 1] = set;

							}
						}
					}
					reader.Close();
				}
			}
			catch
			{
				System.Diagnostics.Debugger.Break();
			}

			return count;
		}

		private int SavePresetAs(string newName, bool isFav)
		{
			// if newName is blank, use existing name
			int count = 0;

			string theFile = utils.DefaultChannelConfigsPath + newName + PRESETEXT;
			if (isFav)
			{
				theFile += STAR;
			}
			try
			{
				System.IO.StreamWriter writer = new System.IO.StreamWriter(theFile);
				for (int idx = 0; idx < curPresets.Length; idx++)
				{
					string lineOut = curPresets[idx].FindRedPct.ToString() + ",";
					lineOut += curPresets[idx].FindGrnPct.ToString() + ",";
					lineOut += curPresets[idx].FindBluPct.ToString() + ",";
					lineOut += curPresets[idx].ReplRedPct.ToString() + ",";
					lineOut += curPresets[idx].ReplGrnPct.ToString() + ",";
					lineOut += curPresets[idx].ReplBluPct.ToString() + ";";
					lineOut += curPresets[idx].FindColorName + " to ";
					lineOut += curPresets[idx].ReplColorName;
					count++;
					writer.WriteLine(lineOut);
				}
				writer.Close();
			}
			catch
			{
				System.Diagnostics.Debugger.Break();
			}

			return count;
		}

		private bool RenamePreset(string oldName, string newName)
		{
			bool success = false;
			bool izFav = false;

			string oldFile = utils.DefaultChannelConfigsPath + oldName + PRESETEXT;
			try
			{
				if (!System.IO.File.Exists(oldFile))
				{
					oldFile += STAR;
					izFav = true;
				}
				if (!System.IO.File.Exists(oldFile))
				{
					string newFile = utils.DefaultChannelConfigsPath + newName + PRESETEXT;
					if (izFav) newFile += STAR;
					System.IO.File.Move(oldFile, newFile);
					success = true;
				}
			}
			catch
			{
				System.Diagnostics.Debugger.Break();
			}
			return success;
		}

		private bool MakeFavorite(string theName, bool newFavState)
		{
			bool success = false;
			bool izFav = false;

			string oldFile = utils.DefaultChannelConfigsPath + theName + PRESETEXT;
			if (!newFavState) oldFile += STAR;
			try
			{
				if (System.IO.File.Exists(oldFile))
				{
					string newFile = utils.DefaultChannelConfigsPath + theName + PRESETEXT;
					if (newFavState) newFile += STAR;
					if (!System.IO.File.Exists(newFile))
					{
						System.IO.File.Move(oldFile, newFile);
						success = true;
					}
				}
			}
			catch
			{
				System.Diagnostics.Debugger.Break();
			}
			return success;

		}

		private bool DeletePreset(string theName)
		{
			bool success = false;

			string theFile = utils.DefaultChannelConfigsPath + theName + PRESETEXT;
			try
			{
				if (!System.IO.File.Exists(theFile))
				{
					theFile += STAR;
				}
				if (!System.IO.File.Exists(theFile))
				{
					Shell shell = new Shell();
					Folder RecyclingBin = shell.NameSpace(10);
					RecyclingBin.MoveHere(theFile);
					success = true;
				}
			}
			catch
			{
				System.Diagnostics.Debugger.Break();
			}
			return success;
		} // End DeletePreset

		static List<FileInfo> files = new List<FileInfo>();  // List that will hold the files and subfiles in path
		static List<DirectoryInfo> folders = new List<DirectoryInfo>(); // List that hold direcotries that cannot be accessed

		static void FullDirList(DirectoryInfo dir, string searchPattern)
		{
			// Console.WriteLine("Directory {0}", dir.FullName);
			// list the files
			try
			{
				foreach (FileInfo f in dir.GetFiles(searchPattern))
				{
					//Console.WriteLine("File {0}", f.FullName);
					files.Add(f);
				}
			}
			catch
			{
				string msg = "Directory {0}  \n could not be accessed!!!!" + dir.FullName;
				System.Diagnostics.Debugger.Break();
			}

		} // End Form

		#endregion

		#region OK/Cancel/Close

		private void btnOK_Click(object sender, EventArgs e)
		{
			DialogResult dirtyResult = DialogResult.None; // Default
			if (dirty)
			{
				dirtyResult = AskToSaveDirty();
			}
			if (dirtyResult != DialogResult.Cancel)
			{
				this.DialogResult = DialogResult.OK;
				this.Hide();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult dirtyResult = DialogResult.None; // Default
			if (dirty)
			{
				dirtyResult = AskToSaveDirty();
			}
			if (dirtyResult != DialogResult.Cancel)
			{
				this.DialogResult = DialogResult.Cancel;
				this.Hide();
			}
		}

		private void frmPresets_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult dirtyResult = DialogResult.None; // Default
			if (dirty)
			{
				dirtyResult = AskToSaveDirty();
			}
			if (dirtyResult != DialogResult.Cancel)
			{
				this.DialogResult = DialogResult.Abort;
				this.Hide();
			}
		}

		#endregion

		#region Add-Remove Panel

		private void AddColorPanel()
		{
			if (panelCount < 30)
			{
				int idx = panelCount;
				//colorCount++;
				panelCount++;
				string suffix = panelCount.ToString("00");

				GroupBox grp = new GroupBox();
				grp.Name = "grpColor" + suffix;
				grp.Size = grpColor01.Size;
				grp.Text = " Color " + colorCount.ToString() + " ";
				grp.Left = grpColor01.Left;
				grp.Top = grpColor01.Top + (grpColor01.Height + 6) * colorCount;
				pnlColors.Controls.Add(grp);

				Label lbl = new Label();
				lbl.Name = "lblFromName" + suffix;
				lbl.Location = lblFromName01.Location;
				lbl.Text = lblFromName01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblInfo" + suffix;
				lbl.Location = lblInfo01.Location;
				lbl.Text = lblInfo01.Text;
				lbl.Font = lblInfo01.Font;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblToName" + suffix;
				lbl.Location = lblToName01.Location;
				lbl.Text = lblToName01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblFromR" + suffix;
				lbl.Location = lblFromR01.Location;
				lbl.Text = lblFromR01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblPctFrR" + suffix;
				lbl.Location = lblPctFrR01.Location;
				lbl.Text = lblPctFrR01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblFromG" + suffix;
				lbl.Location = lblFromG01.Location;
				lbl.Text = lblFromG01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblPctFrG" + suffix;
				lbl.Location = lblPctFrG01.Location;
				lbl.Text = lblPctFrG01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblFromB" + suffix;
				lbl.Location = lblFromB01.Location;
				lbl.Text = lblFromB01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblPctFrB" + suffix;
				lbl.Location = lblPctFrB01.Location;
				lbl.Text = lblPctFrB01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblToR" + suffix;
				lbl.Location = lblToR01.Location;
				lbl.Text = lblToR01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblPctToR" + suffix;
				lbl.Location = lblPctToR01.Location;
				lbl.Text = lblPctToR01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblToG" + suffix;
				lbl.Location = lblToG01.Location;
				lbl.Text = lblToG01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblPctToG" + suffix;
				lbl.Location = lblPctToG01.Location;
				lbl.Text = lblPctToG01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblToB" + suffix;
				lbl.Location = lblToB01.Location;
				lbl.Text = lblToB01.Text;
				grp.Controls.Add(lbl);

				lbl = new Label();
				lbl.Name = "lblPctToB" + suffix;
				lbl.Location = lblPctToB01.Location;
				lbl.Text = lblPctToB01.Text;
				grp.Controls.Add(lbl);

				TextBox txt = new TextBox();
				txt.Name = "txtFindColorName" + suffix;
				txt.Size = txtFindColorName01.Size;
				txt.Location = txtFindColorName01.Location;
				txt.Text = "White";
				grp.Controls.Add(txt);

				txt = new TextBox();
				txt.Name = "txtReplColorName" + suffix;
				txt.Size = txtReplColorName01.Size;
				txt.Location = txtReplColorName01.Location;
				txt.Text = "Black";
				grp.Controls.Add(txt);

				PictureBox pic = new PictureBox();
				pic.Name = "picFindColor" + suffix;
				pic.Size = picFindColor01.Size;
				pic.Location = picFindColor01.Location;
				pic.BackColor = Color.White;
				grp.Controls.Add(pic);

				pic = new PictureBox();
				pic.Name = "picReplColor" + suffix;
				pic.Size = picReplColor01.Size;
				pic.Location = picReplColor01.Location;
				pic.BackColor = Color.Black;
				grp.Controls.Add(pic);

				NumericUpDown num = new NumericUpDown();
				num.Name = "numFindRedPct" + suffix;
				num.Size = numFindRedPct01.Size;
				num.Location = numFindRedPct01.Location;
				num.TextAlign = numFindRedPct01.TextAlign;
				num.Value = 100;
				num.Click += new EventHandler(num_ValueChanged);
				grp.Controls.Add(num);

				num = new NumericUpDown();
				num.Name = "numFindGrnPct" + suffix;
				num.Size = numFindGrnPct01.Size;
				num.Location = numFindGrnPct01.Location;
				num.TextAlign = numFindGrnPct01.TextAlign;
				num.Value = 100;
				num.Click += new EventHandler(num_ValueChanged);
				grp.Controls.Add(num);

				num = new NumericUpDown();
				num.Name = "numFindBluPct" + suffix;
				num.Size = numFindBluPct01.Size;
				num.Location = numFindBluPct01.Location;
				num.TextAlign = numFindBluPct01.TextAlign;
				num.Value = 100;
				num.Click += new EventHandler(num_ValueChanged);
				grp.Controls.Add(num);

				num = new NumericUpDown();
				num.Name = "numReplRedPct" + suffix;
				num.Size = numReplRedPct01.Size;
				num.Location = numReplRedPct01.Location;
				num.TextAlign = numReplRedPct01.TextAlign;
				num.Click += new EventHandler(num_ValueChanged);
				grp.Controls.Add(num);

				num = new NumericUpDown();
				num.Name = "numReplGrnPct" + suffix;
				num.Size = numReplGrnPct01.Size;
				num.Location = numReplGrnPct01.Location;
				num.TextAlign = numReplGrnPct01.TextAlign;
				num.Click += new EventHandler(num_ValueChanged);
				grp.Controls.Add(num);

				num = new NumericUpDown();
				num.Name = "numReplBluPct" + suffix;
				num.Size = numReplBluPct01.Size;
				num.Location = numReplBluPct01.Location;
				num.TextAlign = numReplBluPct01.TextAlign;
				num.Click += new EventHandler(num_ValueChanged);
				grp.Controls.Add(num);
			}
		} // End AddColorPanel

		private void RemoveColorPanel()
		{
			if (panelCount > 1)
			{
				int idx = panelCount;
				string suffix = panelCount.ToString("00");

				string grpName = "grpColor" + suffix;
				GroupBox grp = (GroupBox)pnlColors.Controls[grpName];
				grp.Size = grpColor01.Size;

				grp.Controls.Clear();
				pnlColors.Controls.Remove(grp);

				panelCount--;
			}
		} // End RemoveColorPanel

		#endregion

		private void SetColor(int idx)
		{
			string suffix = (idx + 1).ToString("00");
			string ctlName = "grpColor" + suffix;
			GroupBox grp = (GroupBox)pnlColors.Controls[ctlName];

			ctlName = "txtFindColorName" + suffix;
			TextBox txt = (TextBox)grp.Controls[ctlName];
			txt.Text = curPresets[idx].FindColorName;

			ctlName = "txtReplColorName" + suffix;
			txt = (TextBox)grp.Controls[ctlName];
			txt.Text = curPresets[idx].ReplColorName;

			ctlName = "numFindRedPct" + suffix;
			NumericUpDown num = (NumericUpDown)grp.Controls[ctlName];
			num.Value = curPresets[idx].FindRedPct;

			ctlName = "numFindGrnPct" + suffix;
			num = (NumericUpDown)grp.Controls[ctlName];
			num.Value = curPresets[idx].FindGrnPct;

			ctlName = "numFindBluPct" + suffix;
			num = (NumericUpDown)grp.Controls[ctlName];
			num.Value = curPresets[idx].FindBluPct;

			ctlName = "numReplRedPct" + suffix;
			num = (NumericUpDown)grp.Controls[ctlName];
			num.Value = curPresets[idx].ReplRedPct;

			ctlName = "numReplGrnPct" + suffix;
			num = (NumericUpDown)grp.Controls[ctlName];
			num.Value = curPresets[idx].ReplRedPct;

			ctlName = "numReplBluPct" + suffix;
			num = (NumericUpDown)grp.Controls[ctlName];
			num.Value = curPresets[idx].ReplRedPct;

			//ctlName = "picFindColor" + suffix;
			//PictureBox pic = (PictureBox)grp.Controls[ctlName];
			//Color c = Color.FromArgb(curPresets[idx].FindRedPct, curPresets[idx].FindGrnPct, curPresets[idx].FindBluPct);
			//pic.BackColor = c;

			//ctlName = "picReplColor" + suffix;
			//pic = (PictureBox)grp.Controls[ctlName];
			//c = Color.FromArgb(curPresets[idx].ReplRedPct, curPresets[idx].ReplGrnPct, curPresets[idx].ReplBluPct);
			//pic.BackColor = c;
		} // End SetColor

		private void num_ValueChanged(object sender, EventArgs e)
		{
			bool find = false;
			int rgb = 0;
			NumericUpDown num = (NumericUpDown)sender;
			if (num.Name.Substring(3, 4).CompareTo("Find") == 0) find = true;
			if (num.Name.Substring(7, 3).CompareTo("Red") == 0) rgb = 1;
			if (num.Name.Substring(7, 3).CompareTo("Grn") == 0) rgb = 2;
			if (num.Name.Substring(7, 3).CompareTo("Blu") == 0) rgb = 3;
			string suffix = num.Name.Substring(6, 2);
			int idx = -1;
			int.TryParse(suffix, out idx);
			idx--;

			if (find) // or From
			{
				if (rgb == 1) curPresets[idx].FindRedPct = (byte)num.Value;
				if (rgb == 2) curPresets[idx].FindGrnPct = (byte)num.Value;
				if (rgb == 3) curPresets[idx].FindBluPct = (byte)num.Value;
				string ctlName = "picFindColor" + suffix;
				PictureBox pic = (PictureBox)this.Controls[ctlName];
				int r = (int)(((float)curPresets[idx].FindRedPct) * 2.55F);
				int g = (int)(((float)curPresets[idx].FindGrnPct) * 2.55F);
				int b = (int)(((float)curPresets[idx].FindBluPct) * 2.55F);
				Color c = Color.FromArgb(r, g, b);
				pic.BackColor = c;
			}
			else // Replace or To
			{
				if (rgb == 1) curPresets[idx].ReplRedPct = (byte)num.Value;
				if (rgb == 2) curPresets[idx].ReplGrnPct = (byte)num.Value;
				if (rgb == 3) curPresets[idx].ReplBluPct = (byte)num.Value;
				string ctlName = "picReplColor" + suffix;
				PictureBox pic = (PictureBox)this.Controls[ctlName];
				int r = (int)(((float)curPresets[idx].ReplRedPct) * 2.55F);
				int g = (int)(((float)curPresets[idx].ReplGrnPct) * 2.55F);
				int b = (int)(((float)curPresets[idx].ReplBluPct) * 2.55F);
				Color c = Color.FromArgb(r, g, b);
				pic.BackColor = c;
			}
		} // End num_ValueChanged Event

		private void Colors_Refresh()
		{
			if (colorCount > panelCount)
			{
				for (int k = panelCount; k < colorCount; k++)
				{
					AddColorPanel();
				}
			}
			if (colorCount < panelCount)
			{
				for (int k = colorCount; k < panelCount; k++)
				{
					RemoveColorPanel();
				}
			}
			for (int idx = 0; idx < colorCount; idx++)
			{
				string suffix = (idx + 1).ToString("00");

				string ctlName = "numFindRedPct" + suffix;
				NumericUpDown num = (NumericUpDown)this.Controls[ctlName];
				num.Value = curPresets[idx].FindRedPct;

				ctlName = "numFindGrnPct" + suffix;
				num = (NumericUpDown)this.Controls[ctlName];
				num.Value = curPresets[idx].FindGrnPct;

				ctlName = "numFindBluPct" + suffix;
				num = (NumericUpDown)this.Controls[ctlName];
				num.Value = curPresets[idx].FindBluPct;

				ctlName = "numReplRedPct" + suffix;
				num = (NumericUpDown)this.Controls[ctlName];
				num.Value = curPresets[idx].ReplRedPct;

				ctlName = "numReplGrnPct" + suffix;
				num = (NumericUpDown)this.Controls[ctlName];
				num.Value = curPresets[idx].ReplGrnPct;

				ctlName = "numReplBluPct" + suffix;
				num = (NumericUpDown)this.Controls[ctlName];
				num.Value = curPresets[idx].ReplBluPct;

				// Probably Don't Need this, should be triggered by num_ValueChanged Event
				/*
				ctlName = "picFindColor" + suffix;
				PictureBox pic = (PictureBox)this.Controls[ctlName];
				int r = (int)(((float)curPresets[idx].FindRedPct) * 2.55F);
				int g = (int)(((float)curPresets[idx].FindGrnPct) * 2.55F);
				int b = (int)(((float)curPresets[idx].FindBluPct) * 2.55F);
				Color c = Color.FromArgb(r, g, b);
				pic.BackColor = c;

				ctlName = "picReplColor" + suffix;
				pic = (PictureBox)this.Controls[ctlName];
				r = (int)(((float)curPresets[idx].ReplRedPct) * 2.55F);
				g = (int)(((float)curPresets[idx].ReplGrnPct) * 2.55F);
				b = (int)(((float)curPresets[idx].ReplBluPct) * 2.55F);
				c = Color.FromArgb(r, g, b);
				pic.BackColor = c;
				*/
			}
		} // End Colors_Refesh

		private void picColor_Click(object sender, EventArgs e)
		{

		}

		private void txtColorName_Click(object sender, EventArgs e)
		{

		}

		private DialogResult AskToSaveDirty()
		{
			DialogResult ret = DialogResult.None;
			string msg = "Changes to Preset " + curPresetName + " have not been saved!\r";
			msg += "Do you wish to save them now?";
			DialogResult dr = MessageBox.Show(this, msg, "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			if (dr == DialogResult.Yes)
			{
				SavePresetAs(curPresetName, curIsFav);
			}


			return ret;
		}

		public int AddNewAt(int addAfterIdx)
		{
			colorCount++;
			Array.Resize(ref curPresets, colorCount);
			for (int i = colorCount - 1; i < addAfterIdx; i--)
			{
				curPresets[i] = curPresets[i - 1];
			}
			curPresets[addAfterIdx + 1] = new Preset();


			AddColorPanel();
			Colors_Refresh();



			return panelCount;
		}

		private void mnuFile_Click(object sender, EventArgs e)
		{
			MenuItem mi = (MenuItem)sender;
			string t = (string)mi.Tag;
			switch (t)
			{
				case "S":
					SavePresetAs(curPresetName, isFav);
					break;
				case "A":
					// Save As
					break;
				case "O":
					// Open
					break;
				case "R":
					// Rename
					break;
				case "D":
					// Delete
					break;
				case "F":
					// Toggle Favorite
					break;
				case "V":
					// show favorites only
					break;
				case "L":
					// Show all
					break;
			}
		}
	} // End Form
} // End Namespace
