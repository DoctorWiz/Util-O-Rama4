using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LORclipView
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnPaste_Click(object sender, EventArgs e)
		{
			IDataObject clipDat = Clipboard.GetDataObject();
			string[] clipFmts = clipDat.GetFormats();
			StringBuilder clipNames = new StringBuilder();
			for (int n=0; n<clipFmts.Length; n++)
			{
				clipNames.Append(clipFmts[n]);
				clipNames.Append(" = ");
				string clipText = Clipboard.GetData(clipFmts[n]).ToString();
				clipNames.Append(clipText);
				clipNames.Append("\r\n");
			}
			txtInfo.Text = clipNames.ToString();


			clipNames = new StringBuilder();
			object o = Clipboard.GetData("locale");
			if (o != null)
			{
				System.IO.MemoryStream locale = (System.IO.MemoryStream)o;
				if (locale != null)
				{
					//long l = locale.Length;
					Byte[] byteArray = locale.ToArray();

					for (int b = 0; b < byteArray.Length; b++)
					{
						clipNames.Append(byteArray[b].ToString("X2") + " ");
					}
					txtData.Text = clipNames.ToString();
				}
			}



		}
	}
}
