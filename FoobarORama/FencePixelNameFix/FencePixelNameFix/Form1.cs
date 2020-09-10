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

namespace FencePixelNameFix
{
	public partial class Form1 : Form
	{

		string workDir = "C:\\Users\\Wizard\\!ActiveDocuments\\Light-O-Rama\\2018 Betty\\Sequences\\ChannelConfigs\\";
		string fileIn1 = "Betty Master Channel List v18o.las";
		string fileOut1 = "Betty Master Channel List v18p.las";
		string pixStart1 = "channel name=\"Fence Pixel ";
		string reset1 = "rgbChannel totalCentiseconds";

		string fileIn2 = "17j to 18g2 JustPixelsOffset.ChMap";
		string fileOut2 = "17j to 18p JustPixelsOffset.ChMap";
		string pixStart2 = "masterChannel name=\"Fence Pixel ";
		string reset2 = "rgbChannel totalCentiseconds";

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			StreamReader reader = new StreamReader(workDir + fileIn1);
			StreamWriter writer = new StreamWriter(workDir + fileOut1);
			string lineIn = "";
			string lineOut = "";
			int lineCount = 0;
			int pos = -1;
			int cid = 1;

			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				pos = lineIn.IndexOf(pixStart1);
				if (pos < 1)
				{
					// NOT found
					lineOut = lineIn;
					pos = lineIn.IndexOf(reset1);
					if (pos > 1)
					{
						cid = 1;
					}
				}
				else
				{
					// Found
					if (cid == 1)
					{
						lineOut = lineIn;
					}
					if (cid==2)
					{
						lineOut = lineIn.Substring(0, 42);
						lineOut += lineIn.Substring(56);
					}
					if (cid == 3)
					{
						lineOut = lineIn.Substring(0, 42);
						lineOut += lineIn.Substring(70);
						lineOut = lineOut.Replace("(R)", "(B)");
					}
					cid++;
					string x = lineOut;
				}
				writer.WriteLine(lineOut);

			}
			writer.Close();
			reader.Close();


		}

		private void button2_Click(object sender, EventArgs e)
		{
			StreamReader reader = new StreamReader(workDir + fileIn2);
			StreamWriter writer = new StreamWriter(workDir + fileOut2);
			string lineIn = "";
			string lineOut = "";
			int lineCount = 0;
			int pos = -1;
			int cid = 1;

			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				pos = lineIn.IndexOf(pixStart2);
				if (pos < 1)
				{
					// NOT found
					lineOut = lineIn;
				}
				else
				{
					// Found
					if (lineIn.Length > 110)
					{
						lineOut = lineIn.Substring(0, 44);
						lineOut += lineIn.Substring(70);
						lineOut = lineOut.Replace("(R)", "(B)");
					}
					else
					{
						if (lineIn.Length > 96)
						{
							lineOut = lineIn.Substring(0, 44);
							lineOut += lineIn.Substring(57);
						}
						else
						{
							lineOut = lineIn;
						}
					}
					string x = lineOut;
				}
				writer.WriteLine(lineOut);

			}
			writer.Close();
			reader.Close();


		}
	}
}
