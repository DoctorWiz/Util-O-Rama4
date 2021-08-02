using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StripViewer
{
	public partial class frmView : Form
	{
		Point[] coords;
		int[] sizes;

		// Static, used by addPixelAt
		const int beginCh = 39;
		int stripPixel = 3;
		int pixelNum = 1;
		int savedIndex;
		int universeNum = 2;
		int stripNum = 1;
		int circuit = 1;
		int firstIndex = 0;
		int loPixel = 0;
		int order = 1;
		int hiPixel = 25;
		int beginIndex = 3;
		int dobjIndex = 1;
		string lineOut = ""; // line to be written out, gets modified if necessary
		string tmpFile = "D:\\!MyDocs\\Light-O-Rama\\2016 Betty\\Visualizations\\StripView" + ".tmp";
		StreamWriter writer;




		public frmView()
		{
			InitializeComponent();
		}

		private void frmView_Load(object sender, EventArgs e)
		{
			LoadFormSettings();
		}
		private void LoadFormSettings()
		{
			this.Left = Properties.Settings.Default.Left;
			this.Top = Properties.Settings.Default.Top;

			txtX0.Text = Properties.Settings.Default.X0.ToString();
			txtY0.Text = Properties.Settings.Default.Y0.ToString();
			txtX1.Text = Properties.Settings.Default.X1.ToString();
			txtY1.Text = Properties.Settings.Default.Y1.ToString();
			txtX2.Text = Properties.Settings.Default.X2.ToString();
			txtY2.Text = Properties.Settings.Default.Y2.ToString();
			txtX3.Text = Properties.Settings.Default.X3.ToString();
			txtY3.Text = Properties.Settings.Default.Y3.ToString();
			txtX4.Text = Properties.Settings.Default.X4.ToString();
			txtY4.Text = Properties.Settings.Default.Y4.ToString();
			txtX5.Text = Properties.Settings.Default.X5.ToString();
			txtY5.Text = Properties.Settings.Default.Y5.ToString();
			txtX6.Text = Properties.Settings.Default.X6.ToString();
			txtY6.Text = Properties.Settings.Default.Y6.ToString();
			txtX7.Text = Properties.Settings.Default.X7.ToString();
			txtY7.Text = Properties.Settings.Default.Y7.ToString();

			txtS0.Text = Properties.Settings.Default.S0.ToString();
			txtS1.Text = Properties.Settings.Default.S1.ToString();
			txtS2.Text = Properties.Settings.Default.S2.ToString();
			txtS3.Text = Properties.Settings.Default.S3.ToString();
			txtS4.Text = Properties.Settings.Default.S4.ToString();
			txtS5.Text = Properties.Settings.Default.S5.ToString();
			txtS6.Text = Properties.Settings.Default.S6.ToString();
		}

		private void SaveFormSettings()
		{
			Properties.Settings.Default.Left = this.Left;
			Properties.Settings.Default.Top = this.Top;

			Properties.Settings.Default.X0 = Int16.Parse(txtX0.Text);
			Properties.Settings.Default.Y0 = Int16.Parse(txtY0.Text);
			Properties.Settings.Default.X1 = Int16.Parse(txtX1.Text);
			Properties.Settings.Default.Y1 = Int16.Parse(txtY1.Text);
			Properties.Settings.Default.X2 = Int16.Parse(txtX2.Text);
			Properties.Settings.Default.Y2 = Int16.Parse(txtY2.Text);
			Properties.Settings.Default.X3 = Int16.Parse(txtX3.Text);
			Properties.Settings.Default.Y3 = Int16.Parse(txtY3.Text);
			Properties.Settings.Default.X4 = Int16.Parse(txtX4.Text);
			Properties.Settings.Default.Y4 = Int16.Parse(txtY4.Text);
			Properties.Settings.Default.X5 = Int16.Parse(txtX5.Text);
			Properties.Settings.Default.Y5 = Int16.Parse(txtY5.Text);
			Properties.Settings.Default.X6 = Int16.Parse(txtX6.Text);
			Properties.Settings.Default.Y6 = Int16.Parse(txtY6.Text);
			Properties.Settings.Default.X7 = Int16.Parse(txtX7.Text);
			Properties.Settings.Default.Y7 = Int16.Parse(txtY7.Text);

			Properties.Settings.Default.S0 = Int16.Parse(txtS0.Text);
			Properties.Settings.Default.S1 = Int16.Parse(txtS1.Text);
			Properties.Settings.Default.S2 = Int16.Parse(txtS2.Text);
			Properties.Settings.Default.S3 = Int16.Parse(txtS3.Text);
			Properties.Settings.Default.S4 = Int16.Parse(txtS4.Text);
			Properties.Settings.Default.S5 = Int16.Parse(txtS5.Text);
			Properties.Settings.Default.S6 = Int16.Parse(txtS6.Text);

			Properties.Settings.Default.Save();
		}

		private int getVal(TextBox txtbx)
		{
			return Int16.Parse(txtbx.Text);
		}

		private void MakeArrays()
		{
			Array.Resize(ref coords, 8);
			Array.Resize(ref sizes, 7);

			coords[0] = new Point(getVal(txtX0), getVal(txtY0));
			coords[1] = new Point(getVal(txtX1), getVal(txtY1));
			coords[2] = new Point(getVal(txtX2), getVal(txtY2));
			coords[3] = new Point(getVal(txtX3), getVal(txtY3));
			coords[4] = new Point(getVal(txtX4), getVal(txtY4));
			coords[5] = new Point(getVal(txtX5), getVal(txtY5));
			coords[6] = new Point(getVal(txtX6), getVal(txtY6));
			coords[7] = new Point(getVal(txtX7), getVal(txtY7));

			sizes[0] = getVal(txtS0);
			sizes[1] = getVal(txtS1);
			sizes[2] = getVal(txtS2);
			sizes[3] = getVal(txtS3);
			sizes[4] = getVal(txtS4);
			sizes[5] = getVal(txtS5);
			sizes[6] = getVal(txtS6);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			SaveFormSettings();
			MakeArrays();
			MakeViewerFile();
		}

		private void MakeViewerFile()
		{

			string tmpFile = "D:\\!MyDocs\\Light-O-Rama\\2016 Betty\\Visualizations\\StripView" + ".lee";
			writer = new StreamWriter(tmpFile);
			//beginCh = 39;
			stripPixel = 3;
			circuit = 1;
			firstIndex = 0;
			beginIndex = 3;
			dobjIndex = beginCh;
			pixelNum = sizes[0] + 1;

			lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
			writer.WriteLine(lineOut);
			lineOut = "<channelConfig channelConfigFileVersion=\"11\">";
			lineOut = "<LViz lvizSaveFileVersion=\"2.1.0\" CreatedAt=\"11 - 01 - 2016 00:00:00\">";
			writer.WriteLine(lineOut);
			//lineOut = "	<channels>";
			//lineOut = "\t<Background BackgroundType=\"2\" PicFileName=\"2016 Betty's House Dark.jpg\" PicIntensity=\"100\" EditorWidthTwips=\"19005\" EditorHeightTwips=\"14250\"/>";
			lineOut = "\t<Background BackgroundType=\"2\" PicFileName=\"\" PicIntensity=\"100\" EditorWidthTwips=\"19005\" EditorHeightTwips=\"14250\"/>";
			writer.WriteLine(lineOut);

			WriteLevels();
			WriteItems();


			// NUMBER OF SAVEDINDEXES BEFORE STARTING LED STRIPS
			savedIndex = beginCh;



			lineOut = "\t<DrawObjects>";
			writer.WriteLine(lineOut);

			stripPixel = 150 - sizes[0];

			// Sections go from 0 to 7, but we are only going to draw 1 thru 6 (skip first and last)
			for (int section = 1; section <= 6; section++)
			{
				Single curX = coords[section].X;
				Single curY = coords[section].Y;
				Single xIncr = Convert.ToSingle(coords[section + 1].X - coords[section].X) / Convert.ToSingle(sizes[section]);
				Single yIncr = Convert.ToSingle(coords[section + 1].Y - coords[section].Y) / Convert.ToSingle(sizes[section]);

				switch (section)
				{
					case 1:
						order = -1;
						stripPixel = 150 - sizes[0];
						loPixel = 1;
						hiPixel = stripPixel;
						circuit = stripPixel * 3 - 2;
						stripNum = 1;
						universeNum = 2;
						break;
					case 2:
						order = 1;
						stripPixel = 1;
						loPixel = 1;
						hiPixel = sizes[2];
						circuit = 1;
						stripNum = 2;
						universeNum = 3;
						break;
					case 3:
						loPixel = sizes[2] + 1;
						hiPixel = 150;
						break;
					case 4:
						order = -1;
						stripPixel = 150;
						hiPixel = 150;
						loPixel = sizes[5]+1;
						circuit = 448;
						stripNum = 3;
						universeNum = 4;
						break;
					case 5:
						loPixel = 1;
						hiPixel = sizes[5];
						break;
					case 6:
						order = 1;
						stripPixel = 1;
						loPixel = 1;
						hiPixel = sizes[6];
						circuit = 1;
						stripNum = 4;
						universeNum = 5;
						break;
				}

				while ((stripPixel >= loPixel) && (stripPixel <= hiPixel))
				{
					curX += xIncr;
					curY += yIncr;
					int twipX = Convert.ToInt16(curX * 15);
					int twipY = Convert.ToInt16(curY * 15);
					addPixelAt(twipX, twipY);
				} // end for subGrp 1-25
			}

			lineOut = "\t</DrawObjects>";
			writer.WriteLine(lineOut);
			lineOut = "</LViz>";
			writer.WriteLine(lineOut);

			writer.Flush();
			writer.Close();

			MessageBox.Show("File Completed!");


		} // end MakeViewerFile

		int addPixelAt(int X, int Y)
		{

			//firstIndex = savedIndex;
			lineOut = "\t\t<DrawObject ID=\"";
			lineOut += dobjIndex.ToString();
			dobjIndex++;
			lineOut += "\" Name=\"Pixel " + pixelNum.ToString("000");
			lineOut += " / S" + stripNum.ToString() + "." + stripPixel.ToString("000");
			lineOut += " / U" + universeNum.ToString() + "." + (stripPixel * 3 - 2).ToString("000") + "-" + (stripPixel * 3).ToString("000");
			lineOut += "\" BulbSize=\"1\" BulbSpacing=\"1\" Comment=\"\" BulbShape=\"1\" ZOrder=\"1\" AssignedItem=\"0\" Locked=\"False\"";
			lineOut += " Fixture_Type=\"3\" Channel_Type=\"2\" Max_Opacity=\"0\" LED=\"True\">";
			writer.WriteLine(lineOut);

			lineOut = "\t\t\t<Sample Background_Color=\"0\" RGB_R=\"0\" RGB_G=\"0\" RGB_B=\"0\"/>";
			writer.WriteLine(lineOut);

			lineOut = "\t\t\t<AssignedChannels>";
			writer.WriteLine(lineOut);


			// RED CHANNEL
			circuit++;
			lineOut = "\t\t\t\t<Channel ID=\"1\" Name=\"Pixel " + pixelNum.ToString("000");
			lineOut += " (R) / S" + stripNum.ToString() + "." + stripPixel.ToString("000");
			lineOut += " / U" + universeNum.ToString() + "." + circuit.ToString("000");
			lineOut += " \" DeviceType=\"7\" Network=\"" + universeNum.ToString() + "\" Controller=\"0\" Channel=\"";
			lineOut += circuit.ToString();
			lineOut += "\" Color=\"255\" Sub_Type=\"0\" Sub_Parm=\"0\"";
			lineOut += " Multi_1=\"0\" Multi_2=\"0\" Multi_3=\"0\" Multi_4=\"0\" Multi_5=\"0\"/>";
			writer.WriteLine(lineOut);
			//savedIndex++;
			circuit--;

			// GREEN CHANNEL
			lineOut = "\t\t\t\t<Channel ID=\"2\" Name=\"Pixel " + pixelNum.ToString("000");
			lineOut += " (G) / S" + stripNum.ToString() + "." + stripPixel.ToString("000");
			lineOut += " / U" + universeNum.ToString() + "." + circuit.ToString("000");
			lineOut += " \" DeviceType=\"7\" Network=\"" + universeNum.ToString() + "\" Controller=\"0\" Channel=\"";
			lineOut += circuit.ToString();
			lineOut += "\" Color=\"65280\" Sub_Type=\"0\" Sub_Parm=\"0\"";
			lineOut += " Multi_1=\"0\" Multi_2=\"0\" Multi_3=\"0\" Multi_4=\"0\" Multi_5=\"0\"/>";
			writer.WriteLine(lineOut);
			//savedIndex ++;
			circuit+=2;

			// BLUE CHANNEL
			lineOut = "\t\t\t\t<Channel ID=\"3\" Name=\"Pixel " + pixelNum.ToString("000");
			lineOut += " (B) / S" + stripNum.ToString() + "." + stripPixel.ToString("000");
			lineOut += " / U" + universeNum.ToString() + "." + circuit.ToString("000");
			lineOut += " \" DeviceType=\"7\" Network=\"" + universeNum.ToString() + "\" Controller=\"0\" Channel=\"";
			lineOut += circuit.ToString();
			lineOut += "\" Color=\"16711680\" Sub_Type=\"0\" Sub_Parm=\"0\"";
			lineOut += " Multi_1=\"0\" Multi_2=\"0\" Multi_3=\"0\" Multi_4=\"0\" Multi_5=\"0\"/>";
			writer.WriteLine(lineOut);
			//savedIndex ++;
			if (order > 0)
			{
				circuit ++;
			}
			else
			{
				circuit -= 5;
			}

			// RGB CHANNEL GROUP
			lineOut = "\t\t\t</AssignedChannels>";
			writer.WriteLine(lineOut);

			lineOut = "\t\t\t<DrawPoints>";
			writer.WriteLine(lineOut);

			lineOut = "\t\t\t\t<DrawPoint ID=\"1\" Type=\"16\" X=\"";
			lineOut += X.ToString();
			lineOut += "\" Y=\"";
			lineOut += Y.ToString();
			lineOut += "\"/>";
			writer.WriteLine(lineOut);

			lineOut = "\t\t\t</DrawPoints>";
			writer.WriteLine(lineOut);

			lineOut = "\t\t</DrawObject>";
			writer.WriteLine(lineOut);

			stripPixel += order;
			pixelNum++;



			return savedIndex;

		} // end addPixelAt

		private void WriteLevels()
		{
			lineOut = "\t<Levels Dimmed=\"50\">";
			writer.WriteLine(lineOut);
			for (int i = 1; i < 17; i++)
			{
				lineOut= "\t\t<Level ID=\"";
				lineOut += i.ToString();
				lineOut += "\" Edit_Enabled=\"True\" Sim_Enabled=\"True\" Name=\"";
				if (i == 1)
				{
					lineOut += "Background";
				}
				else if (i == 16)
				{
					lineOut += "Foreground";
				}
				else
				{
					lineOut += i.ToString();
				}
				lineOut += "\"/>";
				writer.WriteLine(lineOut);
			}


			lineOut = "\t</Levels>";
			writer.WriteLine(lineOut);



		}

		private void WriteItems()
		{
			lineOut = "\t<Items>";
			writer.WriteLine(lineOut);
			lineOut = "\t</Items>";
			writer.WriteLine(lineOut);
		}


		private void frmView_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormSettings();
		}
	}

		
	}
