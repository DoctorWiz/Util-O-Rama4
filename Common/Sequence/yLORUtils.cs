using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Media;
using System.Diagnostics;
using Microsoft.Win32;
using FuzzyString;

namespace LORUtils
{
	public class utils
	{
		public const int UNDEFINED = -1;
		public const string ICONtrack = "track";
		public const string ICONchannelGroup = "channelGroup";
		//public const string ICONchannel = "channel";
		public const string ICONrgbChannel = "rgbChannel";
		//public const string ICONredChannel = "redChannel";
		//public const string ICONgrnChannel = "grnChannel";
		//public const string ICONbluChannel = "bluChannel";
		public const string ICONredChannel = "FF0000";
		public const string ICONgrnChannel = "00FF00";
		public const string ICONbluChannel = "0000FF";

		public const string LOG_Error = "Error";
		public const string LOG_Info = "Info";
		public const string LOG_Debug = "Debug";

		public static int nodeIndex = LOR.UNDEFINED;

		public class ChanInfo
		{
			public tableType tableType = tableType.None;
			public int objIndex = LOR.UNDEFINED;
			public int savedIndex = LOR.UNDEFINED;
			public int mapCount = 0;
			public int nodeIndex = LOR.UNDEFINED;
		}

		public static void FillChannels(TreeView tree, Sequence4 seq, List<List<TreeNode>> siNodes, bool selected, bool includeRGB)
		{
			//TODO: 'selected' not implemented yet

			tree.Nodes.Clear();
			//ChanInfo tagInfo = new ChanInfo();
			string nodeText = "";
			ChanInfo nodeTag = new ChanInfo();
			nodeIndex = 1;
			int listSize = seq.highestSavedIndex + seq.tracks.Count + 1;
			//Array.Resize(ref siNodes, listSize);

			// TEMPORARY, FOR DEBUGGING
			int tcount = 0;
			int gcount = 0;
			int rcount = 0;
			int ccount = 0;

			const string ERRproc = " in FillChannels(";
			const string ERRtrk = "), in Track #";
			const string ERRitem = ", Item #";
			const string ERRline = ", Line #";


			for (int n=0; n< listSize; n++)
			{
				//siNodes[n] = null;
				//siNodes[n] = new List<TreeNode>();
				siNodes.Add(new List<TreeNode>());
			}

			for (int t = 0; t < seq.tracks.Count; t++)
			{
				try
				{
					int tNo = t + 1;
					//nodeText = "Track " + tNo.ToString() + ":" + seq.tracks[t].name;
					nodeTag = new ChanInfo();
					nodeText = seq.tracks[t].name;
					TreeNode trackNode = tree.Nodes.Add(nodeText);
					nodeTag.tableType = tableType.track;
					nodeTag.objIndex = t;
					nodeTag.nodeIndex = nodeIndex;
					nodeTag.savedIndex = seq.highestSavedIndex + t;
					nodeIndex++;
					trackNode.Tag = nodeTag;
					List<TreeNode> qlist;

					trackNode.ImageKey = ICONtrack;
					trackNode.SelectedImageKey = ICONtrack;
					qlist = siNodes[nodeTag.savedIndex];
					qlist.Add(trackNode);

					for (int ti = 0; ti < seq.tracks[t].itemSavedIndexes.Count; ti++)
					{
						try
						{
							int si = seq.tracks[t].itemSavedIndexes[ti];
							if (seq.savedIndexes[si] != null)
							{
								if (seq.savedIndexes[si].objType == tableType.channelGroup)
								{
									TreeNode groupNode = AddGroup(seq, trackNode, seq.savedIndexes[si].objIndex, siNodes, selected, includeRGB);
									//AddNode(siNodes[si], groupNode);
									qlist = siNodes[si];
									qlist.Add(groupNode);
									gcount++;
									//siNodes[si].Add(groupNode);
								}
								if (seq.savedIndexes[si].objType == tableType.rgbChannel)
								{
									TreeNode rgbNode = AddRGBchannel(seq, trackNode, seq.savedIndexes[si].objIndex, siNodes, selected, includeRGB);
									//AddNode(siNodes[si], rgbNode);
									//siNodes[si].Add(rgbNode);
									qlist = siNodes[si];
									qlist.Add(rgbNode);
									rcount++;
								}
								if (seq.savedIndexes[si].objType == tableType.channel)
								{
									TreeNode channelNode = AddChannel(seq, trackNode, seq.savedIndexes[si].objIndex, selected);
									//AddNode(siNodes[si], channelNode);
									//siNodes[si].Add(channelNode);
									qlist = siNodes[si];
									qlist.Add(channelNode);
									ccount++;
								}
							} // end not null
						} // end try
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

					} // end loop thru track items

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


			} // end loop thru tracks



			int x = 1; // Check ccount, rcount, gcount

		} // end fillOldChannels

		public static TreeNode AddGroup(Sequence4 seq, TreeNode parentNode, int groupIndex, List<List<TreeNode>> siNodes, bool selected, bool includeRGB)
		{
			string nodeText = seq.channelGroups[groupIndex].name;
			TreeNode groupNode = parentNode.Nodes.Add(nodeText);
			ChanInfo nodeTag = new ChanInfo();
			nodeTag.tableType = tableType.channelGroup;
			nodeTag.objIndex = groupIndex;
			nodeTag.savedIndex = seq.channelGroups[groupIndex].savedIndex;
			nodeTag.nodeIndex = nodeIndex;
			nodeIndex++;
			groupNode.Tag = nodeTag;
			groupNode.ImageKey = ICONchannelGroup;
			groupNode.SelectedImageKey = ICONchannelGroup;
			groupNode.ImageKey = ICONchannelGroup;
			groupNode.SelectedImageKey = ICONchannelGroup;
			List<TreeNode> qlist;

			const string ERRproc = " in FillChannels-AddGroup(";
			const string ERRgrp = "), in Group #";
			const string ERRitem = ", Item #";
			const string ERRline = ", Line #";

			for (int gi = 0; gi < seq.channelGroups[groupIndex].itemSavedIndexes.Count; gi++)
			{
				try
				{
					int si = seq.channelGroups[groupIndex].itemSavedIndexes[gi];
					if (seq.savedIndexes[si].objType == tableType.channelGroup)
					{
						TreeNode subGroupNode = AddGroup(seq, groupNode, seq.savedIndexes[si].objIndex, siNodes, selected, includeRGB);
						qlist = siNodes[si];
						qlist.Add(subGroupNode);
					}
					if (seq.savedIndexes[si].objType == tableType.channel)
					{
						TreeNode channelNode = AddChannel(seq, groupNode, seq.savedIndexes[si].objIndex, selected);
						qlist = siNodes[si];
						qlist.Add(channelNode);
					}
					if (seq.savedIndexes[si].objType == tableType.rgbChannel)
					{
						TreeNode rgbChannelNode = AddRGBchannel(seq, groupNode, seq.savedIndexes[si].objIndex, siNodes, selected, includeRGB);
						qlist = siNodes[si];
						qlist.Add(rgbChannelNode);
					}
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

		public static TreeNode AddChannel(Sequence4 seq, TreeNode parentNode, int channelIndex, bool selected)
		{
			string nodeText = seq.channels[channelIndex].name;
			TreeNode channelNode = parentNode.Nodes.Add(nodeText);
			ChanInfo nodeTag = new ChanInfo();
			nodeTag.tableType = tableType.channel;
			nodeTag.objIndex = channelIndex;
			nodeTag.savedIndex = seq.channels[channelIndex].savedIndex;
			nodeTag.nodeIndex = nodeIndex;
			nodeIndex++;
			channelNode.Tag = nodeTag;
			//channelNode.ImageIndex = imlTreeIcons.Images.IndexOfKey("Channel");
			//channelNode.SelectedImageIndex = imlTreeIcons.Images.IndexOfKey("Channel");
			//channelNode.ImageKey = ICONchannel;
			//channelNode.SelectedImageKey = ICONchannel;


			ImageList icons = parentNode.TreeView.ImageList;
			int iconIndex = ColorIcon(icons, seq.channels[channelIndex].color);
			channelNode.ImageIndex = iconIndex;
			channelNode.SelectedImageIndex = iconIndex;



			return channelNode;
		}

		public static TreeNode AddRGBchannel(Sequence4 seq, TreeNode parentNode, int RGBIndex, List<List<TreeNode>> siNodes, bool selected, bool includeSubChannels)
		{
			List<TreeNode> qlist;

			string nodeText = seq.rgbChannels[RGBIndex].name;
			TreeNode channelNode = parentNode.Nodes.Add(nodeText);
			ChanInfo nodeTag = new ChanInfo();
			nodeTag.tableType = tableType.rgbChannel;
			nodeTag.objIndex = RGBIndex;
			nodeTag.savedIndex = seq.rgbChannels[RGBIndex].savedIndex;
			nodeTag.nodeIndex = nodeIndex;
			nodeIndex++;
			channelNode.Tag = nodeTag;
			channelNode.ImageKey = ICONrgbChannel;
			channelNode.SelectedImageKey = ICONrgbChannel;

			if (includeSubChannels)
			{
				// * * R E D   S U B  C H A N N E L * *
				int ci = seq.rgbChannels[RGBIndex].redChannelObjIndex;
				nodeText = seq.channels[ci].name;
				TreeNode colorNode = channelNode.Nodes.Add(nodeText);
				nodeTag = new ChanInfo();
				nodeTag.tableType = tableType.channel;
				nodeTag.objIndex = ci;
				nodeTag.savedIndex = seq.channels[ci].savedIndex;
				nodeTag.nodeIndex = nodeIndex;
				nodeIndex++;
				colorNode.Tag = nodeTag;
				colorNode.ImageKey = ICONredChannel;
				colorNode.SelectedImageKey = ICONredChannel;
				qlist = siNodes[seq.channels[ci].savedIndex];
				qlist.Add(colorNode);

				// * * G R E E N   S U B  C H A N N E L * *
				ci = seq.rgbChannels[RGBIndex].grnChannelObjIndex;
				nodeText = seq.channels[ci].name;
				colorNode = channelNode.Nodes.Add(nodeText);
				nodeTag = new ChanInfo();
				nodeTag.tableType = tableType.channel;
				nodeTag.objIndex = ci;
				nodeTag.savedIndex = seq.channels[ci].savedIndex;
				nodeTag.nodeIndex = nodeIndex;
				nodeIndex++;
				colorNode.Tag = nodeTag;
				colorNode.ImageKey = ICONgrnChannel;
				colorNode.SelectedImageKey = ICONgrnChannel;
				qlist = siNodes[seq.channels[ci].savedIndex];
				qlist.Add(colorNode);

				// * * B L U E   S U B  C H A N N E L * *
				ci = seq.rgbChannels[RGBIndex].bluChannelObjIndex;
				nodeText = seq.channels[ci].name;
				colorNode = channelNode.Nodes.Add(nodeText);
				nodeTag = new ChanInfo();
				nodeTag.tableType = tableType.channel;
				nodeTag.objIndex = ci;
				nodeTag.savedIndex = seq.channels[ci].savedIndex;
				nodeTag.nodeIndex = nodeIndex;
				nodeIndex++;
				colorNode.Tag = nodeTag;
				colorNode.ImageKey = ICONbluChannel;
				colorNode.SelectedImageKey = ICONbluChannel;
				qlist = siNodes[seq.channels[ci].savedIndex];
				qlist.Add(colorNode);
			} // end includeSubChannels

			return channelNode;
		}

		private static int ColorIcon(ImageList icons, Int32 colorVal)
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


		public static int BTreeFindName(string[] names, string name)
		{
			// looks for an EXACT match (see also: FuzzyFindName)
			// names[] must be sorted!

			//TODO Test this THOROUGHLY!

			int ret = LOR.UNDEFINED;
			int bot = 0;
			int top = names.Length - 1;
			int mid = ((top - bot) / 2) + bot;

			while (top > bot)
			{
				mid = ((top - bot) / 2) + bot;

				if (names[mid].CompareTo(name) < 0)
				{
					mid = top;
				}
				else
				{
					if (names[mid].CompareTo(name) > 0)
					{
						mid = bot;
					}
					else
					{
						return mid;
					}
				}
			}
			return ret;
		} // end bTreeFindName

		public static int FuzzyFindName(string[] names, string name)
		{
			return FuzzyFindName(names, name, .8, .95);
		}

		public static int FuzzyFindName(string[] names, string name, double preMatchMin, double finalMatchMin)
		{
			// names[] do not have to be sorted (although they can be)

			int ret = LOR.UNDEFINED;  // default value, no match
			int[] preIdx = null;
			double[] finalMatchVals = null;
			int preMatchCount = 0;
			double matchVal = 0;

			// Loop thu ALL names
			for (int n=0; n< names.Length; n++)
			{
				// calculate a quick prematch value
				matchVal = names[n].LevenshteinDistance(name);
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
					finalMatchVals[nn] = names[preIdx[nn]].RankEquality(name);
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

		public static int InvalidCharacterCount(string testName)
		{
			int ret = 0;
			int pos1 = 0;
			int pos2 = LOR.UNDEFINED;

			// These are not valid anywhere
			char[] badChars = { '<', '>', '\"', '/', '|', '?', '*' };
			for (int c = 0; c < badChars.Length; c++)
			{
				pos1 = 0;
				pos2 = testName.IndexOf(badChars[c], pos1);
				while (pos2 > LOR.UNDEFINED)
				{
					ret++;
					pos1 = pos2 + 1;
					pos2 = testName.IndexOf(badChars[c], pos1);
				}
			}

			// and the colon is not valid past position 2
			pos1 = 2;
			pos2 = testName.IndexOf(':', pos1);
			while (pos2 > LOR.UNDEFINED)
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



	} // end class utils
} // end namespace LORUtils
