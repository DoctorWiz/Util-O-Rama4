using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Drawing;
using LORUtils4;
using FileHelper;

namespace xUtils
{

	public enum xModelTypes {  Unknown, Single_Line, Custom, Candy_Cane, Arch, Horz_Matrix, Image, Poly_Line, Tree, Circle, Star }
	public enum xSections { Header, Models, ViewObjects, Effects, Views, Palettes, ModelGroups, LayoutGroups, Perspectives, Settings, Viewpoints }

	public class xRGBEffects
	{

		//public List<xModel> xModels = new List<xModel>();
		public List<xRGBModel> xRGBmodels = new List<xRGBModel>();
		public List<xPixels> xPixels = new List<xPixels>();
		public List<xModelGroup> xModelGroups = new List<xModelGroup>();
		private string RGBeffectsFile = "";
		private bool isWiz = Fyle.isWiz;
		public static bool SortByName = false;

		public xRGBEffects()
		{
			string theFile = xutils.ShowDirectory;
			if (Directory.Exists(theFile))
			{
				theFile += "\\xlights_rgbeffects.xml";
				if (File.Exists(theFile))
				{
					LoadRGBEffects(theFile);
				}
			}
		}

		public xRGBEffects(string rgbeffectsFile)
		{
			LoadRGBEffects(rgbeffectsFile);
		}

		public int LoadRGBEffects(string rgbeffectsFile)
		{
			int errs = 0;
			int lineCount = 3;
			string lineIn = "";
			xSections section = xSections.Header;

			RGBeffectsFile = rgbeffectsFile;
			try
			{
				StreamReader reader = new StreamReader(rgbeffectsFile);
				lineIn = reader.ReadLine(); // xml version
				lineIn = reader.ReadLine(); // <xrgb>
				//lineIn = reader.ReadLine(); // <models>


				while (!reader.EndOfStream)
				{
					try
					{
						lineIn = reader.ReadLine();
						if (lineIn.IndexOf("<models") > 0)
						{
							section = xSections.Models;
							lineIn = reader.ReadLine();
						}
						else
						{
							if (lineIn.IndexOf("<modelGroups") > 0)
							{
								section = xSections.ModelGroups;
								lineIn = reader.ReadLine();
							}
							else
							{
								if (lineIn.IndexOf("<view_objects") > 0)
								{
									section = xSections.ViewObjects;
									lineIn = reader.ReadLine();
								}
								else
								{
									if (lineIn.IndexOf("<effects") > 0)
									{
										section = xSections.Effects;
										lineIn = reader.ReadLine();
									}
									else
									{
										if (lineIn.IndexOf("<views") > 0)
										{
											section = xSections.Views;
											lineIn = reader.ReadLine();
										}
										else
										{
											if (lineIn.IndexOf("<palettes") > 0)
											{
												section = xSections.ModelGroups;
												lineIn = reader.ReadLine();
											}
											else
											{
												if (lineIn.IndexOf("<layoutGroups") > 0)
												{
													section = xSections.ModelGroups;
													lineIn = reader.ReadLine();
												}
												else
												{
													if (lineIn.IndexOf("<perspectives") > 0)
													{
														section = xSections.ModelGroups;
														lineIn = reader.ReadLine();
													}
													else
													{
														if (lineIn.IndexOf("<settings") > 0)
														{
															section = xSections.ModelGroups;
															lineIn = reader.ReadLine();
														}
														else
														{
															if (lineIn.IndexOf("<colors") > 0)
															{
																section = xSections.ModelGroups;
																lineIn = reader.ReadLine();
															}
															else
															{
																if (lineIn.IndexOf("<Viewpoints") > 0)
																{
																	section = xSections.ModelGroups;
																	lineIn = reader.ReadLine();
																} // End start ov Viewpoints
															} // End start of Colors
														} // End of Settings
													} // End start of Perspective
												} // End start of Layout Groups
											} // End start of Palettes
										} // End start of View
									} // End start of Effects
								} // End start of View Objects
							} // End start of ModelGroups
						} // End start of Models


						if (section == xSections.Models)
						{

							string modelType = lutils.getKeyWord(lineIn, "DisplayAs");
							string modelName = lutils.getKeyWord(lineIn, "name");
							
							if (modelName.IndexOf("Bushes") >= 0)
							{
								string ItsABush = modelName;
							}
							
							//int xAddress = lutils.getKeyValue(lineIn, "StartChannel");
							string startAddress = xutils.getKeyWord(lineIn, "StartChannel");
							string mtl = modelType.ToLower();
							string stringType = lutils.getKeyWord(lineIn, "StringType");
							string stl = stringType.ToLower();
							xModelTypes xType = xModelTypes.Unknown;
							xPixels pixels = new xPixels("");
							xModel model = new xModel("");

							xMember member = null;

							int xAddress = -1;
							if (startAddress.Length > 0)
							{
								xAddress = xutils.GetAddress(startAddress, xModel.AllModels);
							}


							if (mtl.Length > 3)
							{
								string mt4 = mtl.Substring(0, 4);
								if (mt4.CompareTo("tree") == 0)
								{
									xType = xModelTypes.Tree;
									//model = new xModel(modelName, xType, xAddress);
									//xModels.Add(model);
									//member = model;
									//TODO Get Angle
								}
							}
							if (xType == xModelTypes.Unknown)
							{
								if (mtl.Length > 0)
								{
									switch (mtl)
									{
										case "single line":
											xType = xModelTypes.Single_Line;
											break;
										case "custom":
											xType = xModelTypes.Custom;
											break;
										case "candy canes":
											xType = xModelTypes.Candy_Cane;
											break;
										case "arches":
											xType = xModelTypes.Arch;
											break;
										case "horiz matrix":
											xType = xModelTypes.Horz_Matrix;
											break;
										case "image":
											xType = xModelTypes.Image;
											break;
										case "poly line":
											xType = xModelTypes.Poly_Line;
											break;
										case "circle":
											xType = xModelTypes.Circle;
											break;
										case "star":
											xType = xModelTypes.Star;
											break;
										default:
											if (isWiz)
											{
												string foo1 = modelType;
												System.Diagnostics.Debugger.Break();
											}
											xType = xModelTypes.Unknown;
											break;
									}
								}
							}

							if (stringType.Length > 0)
							{
								if (stringType.IndexOf("Single Color") == 0)
								{
									model = new xModel(modelName, xType, xAddress);
									xModel.AllModels.Add(model);
									member = model;
									//TODO Get Color
								}
								else
								{
									if (stringType.IndexOf("RGB Nodes") == 0)
									{
										pixels = new xPixels(modelName, xType, xAddress);
										xPixels.Add(pixels);
										member = pixels;
									}
									else
									{
										if (stringType.IndexOf("Strobes") == 0)
										{
											model = new xModel(modelName, xType, xAddress);
											xModel.AllModels.Add(model);
											member = model;
											//TODO Get Color
										}
										else
										{
											if (stringType.IndexOf("3 Channel RGB") == 0)
											{
												model = new xModel(modelName, xType, xAddress);
												xModel.AllModels.Add(model);
												member = model;
												//TODO Get Color
											}
											else
											{
												if (isWiz)
												{
													string foo2 = stringType;
													System.Diagnostics.Debugger.Break();
												}
											} // End 3-LORChannel4 RGB
										} // End Strobes
									} // End RGB Nodes
								} // End Single Color
							} // End StringType Length
						} // End ParseModels
						int qqm = xModel.AllModels.Count;

						if (section == xSections.ModelGroups)
						{
							string modelName = lutils.getKeyWord(lineIn, "name");
							if (modelName.Length > 0)
							{
								string groupMembers = lutils.getKeyWord(lineIn, "models");
								xModelGroup group = new xModelGroup(modelName);
								string[] membrz = groupMembers.Split(',');
								foreach (string membr in membrz)
								{
									bool found = false;
									for (int m = 0; m < xModel.AllModels.Count; m++)
									{
										xModel model = xModel.AllModels[m];
										if (membr.CompareTo(model.Name) == 0)
										{
											group.xMembers.Add(model);
											found = true;
											m = xModel.AllModels.Count; // Exit loop
										}
									}
									if (!found)
									{
										for (int g = 0; g < xModelGroups.Count; g++)
										{
											xModelGroup grp2 = xModelGroups[g];
											if (membr.CompareTo(grp2.Name) == 0)
											{
												group.xMembers.Add(grp2);
												found = true;
												g = xModelGroups.Count; // Exit loop
											}
										} // end loop thru other groups
									} // End not found in other groups
								} // End loop thru members (from split[])
								xModelGroups.Add(group);
							} // End Name.Length
						} // End section ModelGroups
						int qqg = xModelGroups.Count;
					} // End Try
					catch (Exception ex2)
					{
						string msg = "Error while reading rgbeffects line " + lineCount.ToString() + "\r\n";
						msg += lineIn + "\r\n\r\n";
						msg += ex2.ToString();
						if (isWiz)
						{
							DialogResult dr = MessageBox.Show(msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						errs++;
					} // End Catch
					lineCount++;
				} // End While Not End-of-Stream
				reader.Close();
				int qqqqm = xModel.AllModels.Count;
				int qqqqg = xModelGroups.Count;

				// Try go get any missing start addresses
				for (int m=0; m<xModel.AllModels.Count; m++)
				{
					xModel model = xModel.AllModels[m];
					if (model.xLightsAddress < 1)
					{
						if (model.StartChannel.Length > 5)
						{
							int a = xutils.GetAddress(model.StartChannel, xModel.AllModels);
							model.xLightsAddress = a;
						}
					}
				}

			} // End Try
			catch (Exception ex1)
			{
				string msg = "Error while opening rgbeffects file\r\n\r\n";
				msg += ex1.ToString();

				if (isWiz)
				{
					DialogResult dr = MessageBox.Show(msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				errs++;
			} // End Catch

			return errs;
		} // End Load RGBeffects



	} // End public class RGBEffects
} // End Namespace
