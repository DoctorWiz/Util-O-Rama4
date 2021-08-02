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
using System.Media;

namespace xMove
{
	public partial class frmxMove : Form
	{
		private const float xAmount = 46.5F;
		private const float zAmount = -79.5F;
		private const string sourceFile = "W:\\Documents\\Christmas\\2021\\xLights\\WizLights\\xlights_rgbeffects.xml.original";
		private const string destFile = "W:\\Documents\\Christmas\\2021\\xLights\\WizLights\\xlights_rgbeffects.xml";

		public frmxMove()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void btnMove_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			this.Enabled = false;
			MovePoints();
			this.Enabled = true;
			this.Cursor = Cursors.Default;
			SystemSounds.Exclamation.Play();

		}

		private void MovePoints()
		{ 
			StreamReader reader = new StreamReader(sourceFile);
			StreamWriter writer = new StreamWriter(destFile);

			while (!reader.EndOfStream)
			{
				string lineIn = "";
				string lineOut = "";

				lineIn = reader.ReadLine();
				lineOut = UpdateKeyValue(lineIn, "WorldPosX", xAmount);
				lineOut = UpdateKeyValue(lineOut, "WorldPosZ", zAmount);
				lineOut = UpdatePoints(lineOut, "PointData", xAmount, zAmount);
				// cPointData = curve Point Data???
				//lineOut = UpdatePoints(lineOut, "cPointData", xAmount, zAmount);


				writer.WriteLine(lineOut);
				

			}
			writer.Close();
			reader.Close();

		}

		public string UpdateKeyValue(string lineIn, string keyWord, float offset)
		{
			const float defaultValue = -9999999F;
			int pos1 = FastIndexOf(lineIn, keyWord);
			if (pos1 >= 0)
			{
				int keySize = keyWord.Length;
				int pl = pos1 + keySize + 2;
				string fooo = lineIn.Substring(pl);
				int pos2 = fooo.IndexOf("\"");
				fooo = fooo.Substring(0, pos2);
				float oldValue = defaultValue;
				float.TryParse(fooo, out oldValue);
				if (oldValue != defaultValue)
				{
					float newValue = oldValue + offset;
					StringBuilder lineOut = new StringBuilder();
					lineOut.Append(lineIn.Substring(0, pos1));
					lineOut.Append(keyWord);
					lineOut.Append("=\"");
					lineOut.Append(newValue.ToString());
					lineOut.Append("\" ");
					lineOut.Append(lineIn.Substring(pl + pos2 + 2));
					return lineOut.ToString();
				}
				else
				{
					return lineIn;
				}
			}
			return lineIn;
		}

		public string UpdatePoints(string lineIn, string keyWord, float xOffset, float zOffset)
		{
			const float defaultValue = -9999999F;
			int pos1 = FastIndexOf(lineIn, keyWord);
			if (pos1 >= 0)
			{
				int keySize = keyWord.Length;
				int pl = pos1 + keySize + 2;
				string fooo = lineIn.Substring(pl);
				int pos2 = fooo.IndexOf("\"");
				fooo = fooo.Substring(0, pos2);
				if (fooo.Length > 3)
				{
					StringBuilder newCoords = new StringBuilder();
					string[] coords = fooo.Split(',');
					if (coords.Length > 2)
					{
						int idx = 0;
						while (idx < coords.Length)
						{
							float oldValue = defaultValue;
							float.TryParse(coords[idx], out oldValue);
							if (oldValue != defaultValue)
							{
								float newValue = oldValue + xOffset;
								coords[idx] = newValue.ToString();
							}
							oldValue = defaultValue;
							float.TryParse(coords[idx + 2], out oldValue);
							if (oldValue != defaultValue)
							{
								float newValue = oldValue + zOffset;
								coords[idx + 2] = newValue.ToString();
							}
							idx += 3;
						} // End while
						for (int i = 0; i < coords.Length; i++)
						{
							newCoords.Append(coords[i]);
							newCoords.Append(',');
						}
					}
					StringBuilder lineOut = new StringBuilder();
					lineOut.Append(lineIn.Substring(0, pos1));
					lineOut.Append(keyWord);
					lineOut.Append("=\"");
					lineOut.Append(newCoords.ToString());
					lineOut.Append("\" ");
					lineOut.Append(lineIn.Substring(pl + pos2 + 2));
					return lineOut.ToString();
				}
				else
				{
					return lineIn;
				}
			}
			return lineIn;
		}

		public static int FastIndexOf(string source, string pattern, int startAt = 0)
		{
			if (pattern == null) throw new ArgumentNullException();
			if (pattern.Length == 0) return 0;
			if (pattern.Length == 1) return source.IndexOf(pattern[0]);
			bool found;
			int limit = source.Length - pattern.Length + 1;
			if (limit < 1) return -1;
			// Store the first 2 characters of "pattern"
			char c0 = pattern[0];
			char c1 = pattern[1];
			// Find the first occurrence of the first character
			int first = source.IndexOf(c0, startAt, limit);
			while (first != -1)
			{
				// Check if the following character is the same like
				// the 2nd character of "pattern"
				if (source[first + 1] != c1)
				{
					first = source.IndexOf(c0, ++first + startAt, limit - first);
					continue;
				}
				// Check the rest of "pattern" (starting with the 3rd character)
				found = true;
				for (int j = 2; j < pattern.Length; j++)
					if (source[first + j] != pattern[j])
					{
						found = false;
						break;
					}
				// If the whole word was found, return its index, otherwise try again
				if (found) return first;
				first = source.IndexOf(c0, ++first + startAt, limit - first);
			}
			return -1;
		}


	}
}
