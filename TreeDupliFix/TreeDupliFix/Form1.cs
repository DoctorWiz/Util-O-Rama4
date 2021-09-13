using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using LORUtils4; using FileHelper;

namespace UtilORama4
{ 	public partial class Form1 : Form
	{
		LORSequence4 seq = null;
		LORRGBChannel4[] rgbChans1 = null;
		LORRGBChannel4[] rgbChans2 = null;
		string fileName = "";


		public Form1()
		{
			InitializeComponent();
		}

		private void btnBrowseOpen_Click(object sender, EventArgs e)
		{
			dlgOpen.InitialDirectory = lutils.DefaultChannelConfigsPath;
			dlgOpen.Filter = lutils.FILT_OPEN_ANY;
			dlgOpen.DefaultExt = lutils.EXT_LAS;
			dlgOpen.Title = "Select sequence file to be fixed";
			DialogResult dr = dlgOpen.ShowDialog();
			if (dr == DialogResult.OK)
			{
				fileName = dlgOpen.FileName;
				ImBusy(true);
				this.Text = "Fixing " + fileName;
				FixFile(fileName);
				ImBusy(false);
				btnSaveAs.Enabled = true;
			}
		} // End Browse-Open

		void FixFile(string theFile)
		{
			seq = new LORSequence4(theFile);
			rgbChans1 = null;
			rgbChans2 = null;
			Array.Resize(ref rgbChans1, 301);
			Array.Resize(ref rgbChans2, 301);

			int ix = 0;

			// Select Everything
			for (int i = 0; i < seq.Tracks.Count; i++)
			{
				seq.Tracks[i].Selected = true;
			}
			for (int i = 0; i < seq.TimingGrids.Count; i++)
			{
				seq.TimingGrids[i].Selected = true;
			}
			for (int i = 0; i < seq.Channels.Count; i++)
			{
				seq.Channels[i].Selected = true;
			}
			for (int i = 0; i < seq.RGBchannels.Count; i++)
			{
				LORRGBChannel4 rgb = seq.RGBchannels[i];
				rgb.Selected = true;
				if (rgb.Name.Substring(0, 11).CompareTo("Tree Pixel ") == 0)
				{
					int pxNo = int.Parse(rgb.Name.Substring(11, 3));
					if (rgbChans1[pxNo] == null)
					{
						rgbChans1[pxNo] = rgb;
					}
					else
					{
						rgbChans2[pxNo] = rgb;
						rgb.Selected = false;
						rgb.redChannel.Selected = false;
						rgb.grnChannel.Selected = false;
						rgb.bluChannel.Selected = false;
					}
				}
			}
			for (int i = 0; i < seq.ChannelGroups.Count; i++)
			{
				LORChannelGroup4 cg = seq.ChannelGroups[i];
				cg.Selected = true;
				if (cg.Name.Length > 20)
				{
					if (cg.Name.Substring(0, 18).CompareTo("Tree Pixels Column") == 0)
					{
						FixGroup(cg);
					}
				}
			}
		} // End FixFile

		void FixGroup(LORChannelGroup4 gr)
		{
			for (int j=0; j< gr.Members.Items.Count; j++)
			{
				if (gr.Members.Items[j].MemberType == LORMemberType4.ChannelGroup)
				{
					FixGroup((LORChannelGroup4)gr.Members.Items[j]);  // Recurse!
				}
				if (gr.Members.Items[j].MemberType == LORMemberType4.RGBChannel)
				{
					int gsi = gr.Members.Items[j].SavedIndex;
					for (int k=0; k< 300; k++)
					{
						if (rgbChans2[k] != null)
						{
							if (gsi == rgbChans2[k].SavedIndex)
							{
								gr.Members.Items[j] = rgbChans1[k];
								k = 300; // force exit from loop
							} // End Saved Index Match
						} // End Element not null
					} // End rgbChans For LORLoop4
				} // End if Member is LORRGBChannel4
			} // End Group Items For LORLoop4
		} // End FixGroup

		private void btnSaveAs_Click(object sender, EventArgs e)
		{
			dlgSave.InitialDirectory = Path.GetDirectoryName(fileName);
			dlgSave.Filter = lutils.FILT_SAVE_EITHER;
			dlgSave.DefaultExt = Path.GetExtension(fileName);
			dlgSave.FileName = Path.GetFileNameWithoutExtension(fileName) + " Fixed";
			dlgSave.Title = "Save Fixed File As...";
			dlgSave.OverwritePrompt = true;
			dlgSave.CheckPathExists = true;
			dlgSave.ValidateNames = true;
			DialogResult dr = dlgSave.ShowDialog();
			if (dr == DialogResult.OK)
			{
				string newFile = dlgSave.FileName;
				seq.WriteSequenceFile_DisplayOrder(newFile, true, false);
			}
		} // End SaveAs

		void ImBusy(bool workingHard)
		{
			this.Enabled = !workingHard;
			if (workingHard)
			{
				this.Cursor = Cursors.WaitCursor;
			}
			else
			{
				this.Cursor = Cursors.Default;
			}
		} // end ImBusy

	} // End Class Form
} // End Namespace
