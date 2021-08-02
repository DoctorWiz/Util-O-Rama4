using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;
//using FuzzyString;

namespace LORUtils
{
	public class utils
	{
		public const int UNDEFINED = -1;
		public const string ICONtrack = "Track";
		public const string ICONchannelGroup = "ChannelGroup";
		//public const string ICONchannel = "channel";
		public const string ICONrgbChannel = "RGBchannel";
		//public const string ICONredChannel = "redChannel";
		//public const string ICONgrnChannel = "grnChannel";
		//public const string ICONbluChannel = "bluChannel";
		public const string ICONredChannel = "FF0000";
		public const string ICONgrnChannel = "00FF00";
		public const string ICONbluChannel = "0000FF";

		public const string LOG_Error = "Error";
		public const string LOG_Info = "Info";
		public const string LOG_Debug = "Debug";

		public static int nodeIndex = UNDEFINED;

		public const string TABLEchannel = "channel";
		public const string FIELDname = " name";
		public const string FIELDtype = " type";
		public const string FIELDsavedIndex = " savedIndex";
		public const string FIELDcentisecond = " centisecond";
		public const string FIELDcentiseconds = FIELDcentisecond + PLURAL;
		public const string FIELDtotalCentiseconds = " totalCentiseconds";
		public const string FIELDstartCentisecond = " startCentisecond";
		public const string FIELDendCentisecond = " endCentisecond";


		public const string SPC = " ";
		public const string LEVEL0 = "";
		public const string LEVEL1 = "  ";
		public const string LEVEL2 = "    ";
		public const string LEVEL3 = "      ";
		public const string LEVEL4 = "        ";
		public const string LEVEL5 = "          ";
		public const string CRLF = "\r\n";
		// Or, if you prefer tabs instead of spaces...
		//public const string LEVEL1 = "\t";
		//public const string LEVEL2 = "\t\t";
		//public const string LEVEL3 = "\t\t\t";
		//public const string LEVEL4 = "\t\t\t\t";
		public const string PLURAL = "s";
		public const string FIELDEQ = "=\"";
		public const string ENDQT = "\"";
		public const string STFLD = "<";
		public const string ENDFLD = "/>";
		public const string FINFLD = ">";
		public const string STTBL = "<";
		public const string FINTBL = "</";
		public const string ENDTBL = ">";

		public const string COMMA = ",";
		public const string SLASH = "/";
		public const char DELIM1 = '⬖';
		public const char DELIM2 = '⬘';
		public const char DELIM3 = '⬗';
		public const char DELIM4 = '⬙';
		private const char DELIM_Map = (char)164;  // ¤
		private const char DELIM_SID = (char)177;  // ±
		private const char DELIM_Name = (char)167;  // §
		private const char DELIM_X = (char)182;  // ¶


		public static void TreeFillChannels(TreeView tree, Sequence4 seq, List<List<TreeNode>> siNodes, bool selectedOnly, bool includeRGBchildren)
		{
			//TODO: 'Selected' not implemented yet

			tree.Nodes.Clear();
			string nodeText = "";
			nodeIndex = 1;
			int listSize = seq.Children.HighestSavedIndex + seq.Tracks.Count + 1;
			//Array.Resize(ref siNodes, listSize);

			// TEMPORARY, FOR DEBUGGING
			int tcount = 0;
			int gcount = 0;
			int rcount = 0;
			int ccount = 0;

			const string ERRproc = " in TreeFillChannels(";
			const string ERRtrk = "), in Track #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";


			for (int n=0; n< listSize; n++)
			{
				//siNodes[n] = null;
				//siNodes[n] = new List<TreeNode>();
				siNodes.Add(new List<TreeNode>());
			}

			for (int t = 0; t < seq.Tracks.Count; t++)
			{
				//try
				//{
				Track theTrack = seq.Tracks[t];
				// Tracks don't normally have savedIndexes
				// But we will assign one for tracking and matching purposes
				//theTrack.SavedIndex = seq.Children.HighestSavedIndex + t + 1;
				int tNo = t + 1;
					nodeText = theTrack.Name;
					TreeNode trackNode = tree.Nodes.Add(nodeText);
					nodeIndex++;
					trackNode.Tag = theTrack;
					List<TreeNode> qlist;

					trackNode.ImageKey = ICONtrack;
					trackNode.SelectedImageKey = ICONtrack;
					qlist = siNodes[theTrack.SavedIndex];
					qlist.Add(trackNode);

					for (int ti = 0; ti < theTrack.Children.Count; ti++)
					{
					//try
					//{
					SeqPart part = theTrack.Children.Items[ti];
					int si = part.SavedIndex;
							if (part != null)
							{
								if (part.TableType == TableType.ChannelGroup)
								{
									TreeNode groupNode = AddGroup(seq, trackNode, part.Index, siNodes, selectedOnly, includeRGBchildren);
									//AddNode(siNodes[si], groupNode);
									qlist = siNodes[si];
									qlist.Add(groupNode);
									gcount++;
									//siNodes[si].Add(groupNode);
								}
								if (part.TableType == TableType.RGBchannel)
								{
									TreeNode rgbNode = AddRGBchannel(seq, trackNode, part.Index, siNodes, selectedOnly, includeRGBchildren);
									//AddNode(siNodes[si], rgbNode);
									//siNodes[si].Add(rgbNode);
									qlist = siNodes[si];
									qlist.Add(rgbNode);
									rcount++;
								}
								if (part.TableType == TableType.Channel)
								{
									TreeNode channelNode = AddChannel(seq, trackNode, part.Index, selectedOnly);
									//AddNode(siNodes[si], channelNode);
									//siNodes[si].Add(channelNode);
									qlist = siNodes[si];
									qlist.Add(channelNode);
									ccount++;
								}
							} // end not null
						//} // end try
				#region catch1
				/*	
				catch (System.NullReferenceException ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					catch (System.InvalidCastException ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					catch (Exception ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					*/
				#endregion

			} // end loop thru track items
			#region catch2 
			/*
				} // end try
				catch (System.NullReferenceException ex)
				{
					StackTrace st = new StackTrace(ex, true);
					StackFrame sf = st.GetFrame(st.FrameCount - 1);
					string emsg = ex.ToString();
					emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
					emsg += ERRline + sf.GetFileLineNumber();
					#if DEBUG
						System.Diagnostics.Debugger.Break();
					#endif
					utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
				}
				catch (System.InvalidCastException ex)
				{
					StackTrace st = new StackTrace(ex, true);
					StackFrame sf = st.GetFrame(st.FrameCount - 1);
					string emsg = ex.ToString();
					emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
					emsg += ERRline + sf.GetFileLineNumber();
					#if DEBUG
						System.Diagnostics.Debugger.Break();
					#endif
					utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
				}
				catch (Exception ex)
				{
					StackTrace st = new StackTrace(ex, true);
					StackFrame sf = st.GetFrame(st.FrameCount - 1);
					string emsg = ex.ToString();
					emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
					emsg += ERRline + sf.GetFileLineNumber();
					#if DEBUG
						System.Diagnostics.Debugger.Break();
					#endif
					utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
				}
				*/
			#endregion

		} // end loop thru Tracks



			int x = 1; // Check ccount, rcount, gcount

		} // end fillOldChannels

		public static TreeNode AddGroup(Sequence4 seq, TreeNode parentNode, int groupIndex, List<List<TreeNode>> siNodes, bool selectedOnly, bool includeRGBchildren)
		{
			//ChanInfo nodeTag = new ChanInfo();
			//nodeTag.TableType = TableType.ChannelGroup;
			//nodeTag.objIndex = groupIndex;
			//nodeTag.SavedIndex = theGroup.SavedIndex;
			//nodeTag.nodeIndex = nodeIndex;

			ChannelGroup theGroup = seq.ChannelGroups[groupIndex];
			SeqPart groupID = theGroup;
			string nodeText = theGroup.Name;
			TreeNode groupNode = parentNode.Nodes.Add(nodeText);


			nodeIndex++;
			groupNode.Tag = groupID;
			groupNode.ImageKey = ICONchannelGroup;
			groupNode.SelectedImageKey = ICONchannelGroup;
			groupNode.ImageKey = ICONchannelGroup;
			groupNode.SelectedImageKey = ICONchannelGroup;
			List<TreeNode> qlist;

			const string ERRproc = " in TreeFillChannels-AddGroup(";
			const string ERRgrp = "), in Group #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";

			for (int gi = 0; gi < theGroup.Children.Count; gi++)
			{
				//try
				//{
					SeqPart part = theGroup.Children.Items[gi];
				int si = part.SavedIndex;
				if (part.TableType == TableType.ChannelGroup)
					{
						TreeNode subGroupNode = AddGroup(seq, groupNode, part.Index, siNodes, selectedOnly, includeRGBchildren);
						qlist = siNodes[si];
						qlist.Add(subGroupNode);
					}
					if (part.TableType == TableType.Channel)
					{
						TreeNode channelNode = AddChannel(seq, groupNode, part.Index, selectedOnly);
						qlist = siNodes[si];
						qlist.Add(channelNode);
					}
					if (part.TableType == TableType.RGBchannel)
					{
						TreeNode rgbChannelNode = AddRGBchannel(seq, groupNode, part.Index, siNodes, selectedOnly, includeRGBchildren);
						qlist = siNodes[si];
						qlist.Add(rgbChannelNode);
					}
				#region catch
				/*
	} // end try
		catch (Exception ex)
		{
			StackTrace st = new StackTrace(ex, true);
			StackFrame sf = st.GetFrame(st.FrameCount - 1);
			string emsg = ex.ToString();
			emsg += ERRproc + seq.filename + ERRgrp + groupIndex.ToString() + ERRitem + gi.ToString();
			emsg += ERRline + sf.GetFileLineNumber();
			#if DEBUG
				Debugger.Break();
			#endif
			utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
		} // end catch
		*/
				#endregion

			} // End loop thru items
			return groupNode;
		} // end AddGroup

		private static void AddNode(List<TreeNode> nodeList, TreeNode nOde)
		{
			nodeList.Add(nOde);
			/*
			if (nodeList == null)
			{
				//Array.Resize(ref nodeList, 1);
				nodeList[0].
 			}
			else
			{
				Array.Resize(ref nodeList, nodeList.Length + 1);
				nodeList[nodeList.Length - 1] = nOde;
			}
			*/
		}

		public static TreeNode AddChannel(Sequence4 seq, TreeNode parentNode, int channelIndex, bool selectedOnly)
		{
			string nodeText = seq.Channels[channelIndex].Name;
			TreeNode channelNode = parentNode.Nodes.Add(nodeText);
			Channel theChannel = seq.Channels[channelIndex];
			SeqPart nodeTag = theChannel;
			nodeIndex++;
			channelNode.Tag = nodeTag;
			//channelNode.ImageIndex = imlTreeIcons.Images.IndexOfKey("Channel");
			//channelNode.SelectedImageIndex = imlTreeIcons.Images.IndexOfKey("Channel");
			//channelNode.ImageKey = ICONchannel;
			//channelNode.SelectedImageKey = ICONchannel;


			ImageList icons = parentNode.TreeView.ImageList;
			int iconIndex = ColorIcon(icons, seq.Channels[channelIndex].color);
			channelNode.ImageIndex = iconIndex;
			channelNode.SelectedImageIndex = iconIndex;



			return channelNode;
		}

		public static TreeNode AddRGBchannel(Sequence4 seq, TreeNode parentNode, int RGBIndex, List<List<TreeNode>> siNodes, bool selectedOnly, bool includeRGBchildren)
		{
			List<TreeNode> qlist;

			string nodeText = seq.RGBchannels[RGBIndex].Name;
			TreeNode channelNode = parentNode.Nodes.Add(nodeText);
			RGBchannel theRGB = seq.RGBchannels[RGBIndex];
			SeqPart nodeTag = theRGB;
			nodeIndex++;
			channelNode.Tag = nodeTag;
			channelNode.ImageKey = ICONrgbChannel;
			channelNode.SelectedImageKey = ICONrgbChannel;

			if (includeRGBchildren)
			{
				// * * R E D   S U B  C H A N N E L * *
				int ci = seq.RGBchannels[RGBIndex].redChannel.Index;
				nodeText = seq.Channels[ci].Name;
				TreeNode colorNode = channelNode.Nodes.Add(nodeText);
				nodeTag = seq.Channels[ci];
				nodeIndex++;
				colorNode.Tag = nodeTag;
				colorNode.ImageKey = ICONredChannel;
				colorNode.SelectedImageKey = ICONredChannel;
				qlist = siNodes[seq.Channels[ci].SavedIndex];
				qlist.Add(colorNode);

				// * * G R E E N   S U B  C H A N N E L * *
				ci = seq.RGBchannels[RGBIndex].grnChannel.Index;
				nodeText = seq.Channels[ci].Name;
				colorNode = channelNode.Nodes.Add(nodeText);
				nodeTag = seq.Channels[ci];
				nodeIndex++;
				colorNode.Tag = nodeTag;
				colorNode.ImageKey = ICONgrnChannel;
				colorNode.SelectedImageKey = ICONgrnChannel;
				qlist = siNodes[seq.Channels[ci].SavedIndex];
				qlist.Add(colorNode);

				// * * B L U E   S U B  C H A N N E L * *
				ci = seq.RGBchannels[RGBIndex].bluChannel.Index;
				nodeText = seq.Channels[ci].Name;
				colorNode = channelNode.Nodes.Add(nodeText);
				nodeTag = seq.Channels[ci];
				nodeIndex++;
				colorNode.Tag = nodeTag;
				colorNode.ImageKey = ICONbluChannel;
				colorNode.SelectedImageKey = ICONbluChannel;
				qlist = siNodes[seq.Channels[ci].SavedIndex];
				qlist.Add(colorNode);
			} // end includeRGBchildren

			return channelNode;
		}

		public static int ColorIcon(ImageList icons, Int32 colorVal)
		{
			int ret = -1;
			string tempID = colorVal.ToString("X6");
			// LOR's Color Format is in BGR format, so have to reverse the Red and the Blue
			string colorID = tempID.Substring(4, 2) + tempID.Substring(2, 2) + tempID.Substring(0, 2);
			ret = icons.Images.IndexOfKey(colorID);
			if (ret < 2)
			{
				// Convert rearranged hex value a real color
				Color theColor = System.Drawing.ColorTranslator.FromHtml("#" + colorID);
				// Create a temporary working bitmap
				Bitmap bmp = new Bitmap(16,16);
				// get the graphics handle from it
				Graphics gr = Graphics.FromImage(bmp);
				// A colored solid brush to fill the middle
				SolidBrush b = new SolidBrush(theColor);
				// define a rectangle for the middle
				Rectangle r = new Rectangle(2, 2, 12, 12);
				// Fill the middle rectangle with color
				gr.FillRectangle(b, r);
				// Draw a 3D button around it
				Pen p = new Pen(Color.Black);
				gr.DrawLine(p, 1, 15, 15, 15);
				gr.DrawLine(p, 15, 1, 15, 14);
				p = new Pen(Color.DarkGray);
				gr.DrawLine(p, 2, 14, 14, 14);
				gr.DrawLine(p, 14, 2, 14, 13);
				p = new Pen(Color.White);
				gr.DrawLine(p, 0, 0, 15, 0);
				gr.DrawLine(p, 0, 1, 0, 15);
				p = new Pen(Color.LightGray);
				gr.DrawLine(p, 1, 1, 14, 1);
				gr.DrawLine(p, 1, 2, 1, 14);

				// Add it to the image list, using it's hex color code as the key
				icons.Images.Add(colorID, bmp);
				// get it's numeric index
				ret = icons.Images.Count - 1;
			}
			// Return the numeric index of the new image
			return ret;
		}

		public static string ShortenLongPath(string longPath, int maxLen)
		{
			//TODO I'm not too pleased with the current results of this function
			//TODO Try to make something better

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

		/*
		public static int FindName(string[] names, string Name)
		{
			return Array.BinarySearch(names, Name);
		} // end FindName

		public static int FuzzyFindName(string[] names, string Name)
		{
			return FuzzyFindName(names, Name, .8, .95);
		}

		public static int FuzzyFindName(string[] names, string Name, double preMatchMin, double finalMatchMin)
		{
			// names[] do not have to be sorted (although they can be)

			int ret = UNDEFINED;  // default value, no match
			int[] preIdx = null;
			double[] finalMatchVals = null;
			int preMatchCount = 0;
			double matchVal = 0;

			// Loop thu ALL names
			for (int n=0; n< names.Length; n++)
			{
				// calculate a quick prematch value
				matchVal = names[n].LevenshteinDistance(Name);
				// if above the minimum
				if (matchVal > preMatchMin)
				{
					// add to array of prematches
					Array.Resize(ref preIdx, preMatchCount + 1);
					preIdx[preMatchCount] = n;
					preMatchCount++;
				}
			}
			// any prematches found?
			if (preMatchCount > 0)
			{
				// size array to hold final match values
				Array.Resize(ref finalMatchVals, preMatchCount);
				// loop thru the prematches
				for (int nn = 0; nn < preMatchCount; nn++)
				{
					// calculate and remember the final match value
					finalMatchVals[nn] = names[preIdx[nn]].RankEquality(Name);
				}
				// sort the prematches by their final match value
				Array.Sort(finalMatchVals, preIdx);
				// Is the last one (which will have the highest final match value once sorted)
				// above the final minimum?
				if (finalMatchVals[preMatchCount - 1] > finalMatchMin)
				{
					// set return value to it's index
					ret = preIdx[preMatchCount - 1];
				}
			}
			// return index of best match if found,
			// default of undefined if not
			return ret;
		} // end FuzzyFindName
		*/

		public static int InvalidCharacterCount(string testName)
		{
			int ret = 0;
			int pos1 = 0;
			int pos2 = UNDEFINED;

			// These are not valid anywhere
			char[] badChars = { '<', '>', '\"', '/', '|', '?', '*' };
			for (int c = 0; c < badChars.Length; c++)
			{
				pos1 = 0;
				pos2 = testName.IndexOf(badChars[c], pos1);
				while (pos2 > UNDEFINED)
				{
					ret++;
					pos1 = pos2 + 1;
					pos2 = testName.IndexOf(badChars[c], pos1);
				}
			}

			// and the colon is not valid past position 2
			pos1 = 2;
			pos2 = testName.IndexOf(':', pos1);
			while (pos2 > UNDEFINED)
			{
				ret++;
				pos1 = pos2 + 1;
				pos2 = testName.IndexOf(':', pos1);
			}

			return ret;
		}

		private static string NotifyGenericSound
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\AppEvents\\Schemes\\Apps\\.Default\\Notification.Default";
				string sound = (string)Registry.GetValue(keyName, null, "");
				return sound;
			} // End get the Notify.Generic Sound filename
		}

		public static void PlayNotifyGenericSound()
		{
			string sound = NotifyGenericSound;
			if (sound.Length > 6)
			{
				if (File.Exists(sound))
				{
					SoundPlayer player = new SoundPlayer(sound);
					player.Play();
				}
			}
		}

		private static string MenuClickSound
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\AppEvents\\Schemes\\Apps\\.Default\\Notification.Default";
				string sound = (string)Registry.GetValue(keyName, null, "");
				return sound;
			} // End get the Notify.Generic Sound filename
		}

		public static void PlayMenuClickSound()
		{
			string sound = MenuClickSound;
			if (sound.Length > 6)
			{
				if (File.Exists(sound))
				{
					SoundPlayer player = new SoundPlayer(sound);
					player.Play();
				}
			}
		}





		public static void WriteLogEntry(string message, string logType, string applicationName)
		{
			string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string mySubDir = "\\UtilORama\\";
			if (applicationName.Length > 2)
			{
				applicationName.Replace("-", "");
				mySubDir += applicationName + "\\";
			}
			string file = appDataDir + mySubDir;
			if (!Directory.Exists(file)) Directory.CreateDirectory(file);
			file += logType + ".log";
			//string dt = DateTime.Now.ToString("yyyy-MM-dd ")
			string dt = DateTime.Now.ToString("s") + "=";
			string msgLine = dt + message;
			try
			{
				StreamWriter writer = new StreamWriter(file, append: true);
				writer.WriteLine(msgLine);
				writer.Flush();
				writer.Close();
			}
			catch (System.IO.IOException ex)
			{
				string errMsg = "An error has occured in this application!\r\n";
				errMsg += "Another error has occured while trying to write the details of the first error to a log file!\r\n\r\n";
				errMsg += "The first error was: " + message + "\r\n";
				errMsg += "The second error was: " + ex.ToString();
				errMsg += "The log file is: " + file;
				DialogResult dr = MessageBox.Show(errMsg, "Errors!", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
			}
			finally
			{
			}

		} // end write log entry


		public static string DefaultUserDataPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "UserDataPath", userDocs);
				return fldr;
			} // End get UserDataPath
		}

		public static string DefaultNonAudioPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "NonAudioPath", userDocs);
				return fldr;
			} // End get NonAudioPath (Sequences)
		}

		public static string DefaultSequencesPath
		{
			get
			{
				return DefaultNonAudioPath;
			}
		}


		public static string DefaultAudioPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "AudioPath", userDocs);
				return fldr;
			} // End get AudioPath
		}

		public static string DefaultClipboardsPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "ClipboardsPath", userDocs);
				return fldr;
			} // End get ClipboardsPath
		}

		public static string DefaultChannelConfigsPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "ChannelConfigsPath", "");
				if (fldr.Length < 5)
				{
					fldr = DefaultNonAudioPath + "ChannelConfigs\\";
					if (fldr.Length < 18)
					{
						fldr = userDocs;
					}
					else
					{
						Registry.SetValue(keyName, "ChannelConfigsPath", fldr, RegistryValueKind.String);
					}
				}
				if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);
				return fldr;
			} // End get ChannelConfigsPath
		}

		public static string fileCreatedAt(string filename)
		{
			string ret = "";
			if (File.Exists(filename))
			{
				DateTime dt = File.GetCreationTime(filename);
				ret = dt.ToString("MM/dd/yyyy hh:mm:ss tt");
			}
			return ret;
		}

		public static DateTime fileCreatedDateTime(string filename)
		{
			DateTime ret = new DateTime();
			if (File.Exists(filename))
			{
				ret = File.GetCreationTime(filename);
			}
			return ret;
		}

		public static string fileModiedAt(string filename)
		{
			string ret = "";
			if (File.Exists(filename))
			{
				DateTime dt = File.GetLastWriteTime(filename);
				ret = dt.ToString("MM/dd/yyyy hh:mm:ss tt");
			}
			return ret;
		}

		public static DateTime fileModiedDateTime(string filename)
		{
			DateTime ret = new DateTime();
			if (File.Exists(filename))
			{
				ret = File.GetLastWriteTime(filename);
			}
			return ret;
		}

		public static string fileSizeFormated(string filename)
		{
			string ret = "0";
			if (File.Exists(filename))
			{
				FileInfo fi = new FileInfo(filename);
				long l = fi.Length;
				if (l < 1024)
				{
					ret = l.ToString() + " bytes";
				}
				else
				{
					if (l < 1048576)
					{
						ret = (l / 1024).ToString() + " KB";
					}
					else
					{
						if (l < 1073741824)
						{
							ret = (l / 1048576).ToString() + " MB";
						}
						else
						{
							if (l < 1099511627776)
							{
								ret = (l / 1073741824).ToString() + " GB";
							}
						}
					}
				}
			}
			return ret;
		}

		public static long fileSize(string filename)
		{
				long ret = 0;
				if (File.Exists(filename))
				{
					FileInfo fi = new FileInfo(filename);
					ret = fi.Length;
				}
				return ret;
		}

		public static string cleanName(string dirtyName)
		{
			string ret = dirtyName;
			ret = ret.Replace("&quot", "\"");
			return ret;
		}

		public static string dirtyName(string cleanName)
		{
			string ret = cleanName;
			ret = ret.Replace("\"", "&quot");
			return ret;
		}

		public static Int32 getKeyValue(string lineIn, string keyWord)
		{
			Int32 valueOut = UNDEFINED;
			int pos1 = UNDEFINED;
			int pos2 = UNDEFINED;
			string fooo = "";

			pos1 = lineIn.IndexOf(keyWord + "=");
			if (pos1 > 0)
			{
				fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
				pos2 = fooo.IndexOf("\"");
				fooo = fooo.Substring(0, pos2);
				valueOut = Convert.ToInt32(fooo);
			}
			else
			{
				valueOut = UNDEFINED;
			}

			return valueOut;
		}

		public static string getKeyWord(string lineIn, string keyWord)
		{
			string valueOut = "";
			int pos1 = UNDEFINED;
			int pos2 = UNDEFINED;
			string fooo = "";

			pos1 = lineIn.IndexOf(keyWord + "=");
			if (pos1 > 0)
			{
				fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
				pos2 = fooo.IndexOf("\"");
				fooo = fooo.Substring(0, pos2);
				valueOut = fooo;
			}
			else
			{
				valueOut = "";
			}

			return valueOut;
		}


		public static int DisplayOrderBuildLists(Sequence4 seq, ref int[] savedIndexes, ref int[] levels, bool selectedOnly, bool includeRGBchildren)
		{
			//TODO: 'Selected' not implemented yet

			int count = 0;
			int c = 0;
			int level = 0;
			int tot = seq.Tracks.Count + seq.Channels.Count + seq.ChannelGroups.Count;
			if (includeRGBchildren)
			{
				tot += seq.RGBchannels.Count;
			}
			else
			{
				// Why * 2? 'cus we won't show the 3 Children, but will show the parent.  3-1=2.
				tot -= (seq.RGBchannels.Count * 2);
			}
			//Array.Resize(ref savedIndexes, tot);
			//Array.Resize(ref levels, tot);




			// TEMPORARY, FOR DEBUGGING
			int tcount = 0;
			int gcount = 0;
			int rcount = 0;
			int ccount = 0;

			const string ERRproc = " in TreeFillChannels(";
			const string ERRtrk = "), in Track #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";

			for (int t = 0; t < seq.Tracks.Count; t++)
			{
				level = 0;
				Track theTrack = seq.Tracks[t];
				if (!selectedOnly || theTrack.Selected)
				{
					Array.Resize(ref savedIndexes, count + 1);
					Array.Resize(ref levels, count + 1);
					savedIndexes[count] = theTrack.SavedIndex;
					levels[count] = level;
					count++;
					c++;
				}
				//try
				//{
				for (int ti = 0; ti < theTrack.Children.Count; ti++)
				{
					//try
					//{
					SeqPart part = theTrack.Children.Items[ti];
					int si = part.SavedIndex;
					if (part != null)
					{
						if (part.TableType == TableType.ChannelGroup)
						{
							c += DisplayOrderBuildGroup(seq, si, level+1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
						}
						if (part.TableType == TableType.RGBchannel)
						{
							c += DisplayOrderBuildRGBchannel(seq, si, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
						}
						if (part.TableType == TableType.Channel)
						{
							c += DisplayOrderBuildChannel(seq, si, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly);
						}
					} // end not null
						//} // end try
					#region catch1
					/*	
					catch (System.NullReferenceException ex)
						{
							StackTrace st = new StackTrace(ex, true);
							StackFrame sf = st.GetFrame(st.FrameCount - 1);
							string emsg = ex.ToString();
							emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
							emsg += ERRline + sf.GetFileLineNumber();
							#if DEBUG
								System.Diagnostics.Debugger.Break();
							#endif
							utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
						}
						catch (System.InvalidCastException ex)
						{
							StackTrace st = new StackTrace(ex, true);
							StackFrame sf = st.GetFrame(st.FrameCount - 1);
							string emsg = ex.ToString();
							emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
							emsg += ERRline + sf.GetFileLineNumber();
							#if DEBUG
								System.Diagnostics.Debugger.Break();
							#endif
							utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
						}
						catch (Exception ex)
						{
							StackTrace st = new StackTrace(ex, true);
							StackFrame sf = st.GetFrame(st.FrameCount - 1);
							string emsg = ex.ToString();
							emsg += ERRproc + seq.filename + ERRtrk + t.ToString() + ERRitem + ti.ToString();
							emsg += ERRline + sf.GetFileLineNumber();
							#if DEBUG
								System.Diagnostics.Debugger.Break();
							#endif
							utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
						}
						*/
					#endregion

				} // end loop thru track items
				#region catch2 
				/*
					} // end try
					catch (System.NullReferenceException ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					catch (System.InvalidCastException ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					catch (Exception ex)
					{
						StackTrace st = new StackTrace(ex, true);
						StackFrame sf = st.GetFrame(st.FrameCount - 1);
						string emsg = ex.ToString();
						emsg += ERRproc + seq.filename + ERRtrk + t.ToString();
						emsg += ERRline + sf.GetFileLineNumber();
						#if DEBUG
							System.Diagnostics.Debugger.Break();
						#endif
						utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
					}
					*/
				#endregion

			} // end loop thru Tracks

			// Integrity Checks for debugging
			if (c != count)
			{
				string msg = "Houston, we have a problem!";
			}
			if (!selectedOnly)
			{
				if (c != tot)
				{
					string msg = "Houston, we have another problem!";
				}
			}



			return c;

		} // end fillOldChannels

		public static int DisplayOrderBuildGroup(Sequence4 seq, int groupSI, int level, ref int count, ref int[] savedIndexes, ref int[]levels, bool selectedOnly, bool includeRGBchildren)
		{
			int c = 0;
			ChannelGroup theGroup = (ChannelGroup)seq.Children.bySavedIndex[groupSI];
			SeqPart groupID = theGroup;
			string nodeText = theGroup.Name;

			const string ERRproc = " in TreeFillChannels-AddGroup(";
			const string ERRgrp = "), in Group #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";

			if (!selectedOnly || theGroup.Selected)
			{
				Array.Resize(ref savedIndexes, count + 1);
				Array.Resize(ref levels, count + 1);
				savedIndexes[count] = groupSI;
				levels[count] = level;
				count++;
				c++;
			}

			for (int gi = 0; gi < theGroup.Children.Count; gi++)
			{
				//try
				//{
				SeqPart part = theGroup.Children.Items[gi];
				int si = part.SavedIndex;
				if (part.TableType == TableType.ChannelGroup)
				{
					c += DisplayOrderBuildGroup(seq, si, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
				}
				if (part.TableType == TableType.Channel)
				{
					c += DisplayOrderBuildChannel(seq, si, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly);
				}
				if (part.TableType == TableType.RGBchannel)
				{
					c += DisplayOrderBuildRGBchannel(seq, si, level + 1, ref count, ref savedIndexes, ref levels, selectedOnly, includeRGBchildren);
				}
				#region catch
				/*
	} // end try
		catch (Exception ex)
		{
			StackTrace st = new StackTrace(ex, true);
			StackFrame sf = st.GetFrame(st.FrameCount - 1);
			string emsg = ex.ToString();
			emsg += ERRproc + seq.filename + ERRgrp + groupIndex.ToString() + ERRitem + gi.ToString();
			emsg += ERRline + sf.GetFileLineNumber();
			#if DEBUG
				Debugger.Break();
			#endif
			utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
		} // end catch
		*/
				#endregion

			} // End loop thru items
			return c;
		} // end AddGroup

		public static int DisplayOrderBuildChannel(Sequence4 seq, int channelSI, int level, ref int count, ref int[] savedIndexes, ref int[] levels, bool selectedOnly)
		{
			int c = 0;
			if (!selectedOnly || seq.Children.bySavedIndex[channelSI].Selected) 
			{
				Array.Resize(ref savedIndexes, count + 1);
				Array.Resize(ref levels, count + 1);
				savedIndexes[count] = channelSI;
				levels[count] = level;
				count++;
				c++;
			}
			return c;
		}

		public static int DisplayOrderBuildRGBchannel(Sequence4 seq, int channelSI, int level, ref int count, ref int[] savedIndexes, ref int[] levels, bool selectedOnly, bool includeRGBchildren)
		{
			int c = 0;
			RGBchannel theRGB = (RGBchannel)seq.Children.bySavedIndex[channelSI];

			if (!selectedOnly || theRGB.Selected)
			{
				Array.Resize(ref savedIndexes, count + 1);
				Array.Resize(ref levels, count + 1);
				savedIndexes[count] = channelSI;
				levels[count] = level;
				count++;
				c++;
				if (includeRGBchildren)
				{
					Array.Resize(ref savedIndexes, count + 3);
					Array.Resize(ref levels, count + 3);
					// * * R E D   S U B  C H A N N E L * *
					savedIndexes[count] = theRGB.redChannel.SavedIndex;
					levels[count] = level + 1;
					count++;

					// * * G R E E N   S U B  C H A N N E L * *
					savedIndexes[count] = theRGB.grnChannel.SavedIndex;
					levels[count] = level + 1;
					count++;

					// * * B L U E   S U B  C H A N N E L * *
					savedIndexes[count] = theRGB.bluChannel.SavedIndex;
					levels[count] = level + 1;
					count++;

					c += 3;
				} // end includeRGBchildren
			}

			return c;
		}

		public static void ClearOutputWindow()
		{
			if (!Debugger.IsAttached)
			{
				return;
			}

			/*
			//Application.DoEvents();  // This is for Windows.Forms.
			// This delay to get it to work. Unsure why. See http://stackoverflow.com/questions/2391473/can-the-visual-studio-debug-output-window-be-programatically-cleared
			Thread.Sleep(1000);
			// In VS2008 use EnvDTE80.DTE2
			EnvDTE.DTE ide = (EnvDTE.DTE)Marshal.GetActiveObject("VisualStudio.DTE.14.0");
			if (ide != null)
			{
				ide.ExecuteCommand("Edit.ClearOutputWindow", "");
				Marshal.ReleaseComObject(ide);
			}
			*/
		}

		public static string GetAppTempFolder()
		{

			string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string mySubDir = "\\UtilORama\\";
			string tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			mySubDir += Application.ProductName + "\\";
			mySubDir = mySubDir.Replace("-", "");
			tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			return tempPath;
		}




	} // end class utils
} // end namespace LORUtils
