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

namespace UtilORama4
{
	public partial class frmUniverse : Form
	{
		public List<DMXUniverse> universes = null;
		public DMXUniverse universe = new DMXUniverse();
		public bool dirty = false;
		public bool loading = true;
		private bool moved = false;

		public frmUniverse()
		{
			InitializeComponent();
		}

		public frmUniverse(DMXUniverse univs, List<DMXUniverse> universeList)
		{
			InitializeComponent();
			universes = universeList;
			FillCombos();
			LoadUniverse(univs);
			//loading = true;
			dirty = false;
		}

		public void FillCombos()
		{
			// No Combo Boxes on this form to fill!
		}

		public void SetFormPosition()
		{
			if (!moved)
			{
				int l = (this.Owner.Width - this.Width) / 2 + this.Owner.Left;
				int t = (this.Owner.Height - this.Height) / 2 + this.Owner.Top;
				this.Left = l;
				this.Top = t;
				moved = true;
			}
		}

		public void LoadUniverse(DMXUniverse univs)
		{
			loading = true;
			universe = univs;
			txtName.Text = universe.Name;
			txtLocation.Text = universe.Location;
			txtComment.Text = universe.Comment;
			txtConnection.Text = universe.Connection;
			numNumber.Value = universe.UniverseNumber;
			chkActive.Checked = universe.Active;
			numSize.Value = universe.SizeLimit;
			numxStart.Value = universe.xLightsAddress;
			StringBuilder sb = new StringBuilder();
			for (int c = 0; c < universe.DMXControllers.Count; c++)
			{
				sb.Append(universe.DMXControllers[c].LetterID);
				sb.Append(": ");
				sb.Append(universe.DMXControllers[c].Name);
				sb.Append("\r\n");
			}
			lblControllers.Text = sb.ToString();

			MakeDirty(false);
			loading = false;
		}
		private void frmUniverse_Load(object sender, EventArgs e)
		{

		}


		public void MakeDirty(bool isDirty)
		{
			if (dirty != isDirty)
			{
				if (dirty)
				{
					if (!loading)
					{
						this.Text = "Universe: " + universe.UniverseNumber.ToString() + ": " + universe.Name + " (Modified)";
						dirty = isDirty;
					}
				}
				else
				{
					this.Text = "Universe: " + universe.UniverseNumber.ToString() + ": " + universe.Name;
					dirty = isDirty;
				}
			}
		}

		private void txtName_Validating(object sender, CancelEventArgs e)
		{
			/*
			string tipText = "The name of this universe";
			string theName = txtName.Text.TrimStart().ToLower();
			txtName.Text = theName;
			universe.Name = theName;

			if (universe.BadName)
			{
				tipText = "The universe MUST have a UNIQUE name!";
			}
			else
			{
				// keep default tooltip text (above)
			}
			tipTool.SetToolTip(txtName, tipText);
			tipTool.SetToolTip(lblName, tipText);
			if (!loading) MakeDirty(true);
			*/

		}

		private void numNumber_Validating(object sender, CancelEventArgs e)
		{
			/*
			string tipText = "The number for this universe";
			universe.UniverseNumber = (int)numNumber.Value;
			if (universe.BadNumber)
			{
				tipText = "The universe number MUST be UNIQUE!";
			}
			tipTool.SetToolTip(numNumber, tipText);
			tipTool.SetToolTip(lblNumber, tipText);

			if (!loading) MakeDirty(true);
			*/
		}

		private void numNumber_ValueChanged(object sender, EventArgs e)
		{
			string tipText = "The number for this universe";
			universe.UniverseNumber = (int)numNumber.Value;
			if (universe.BadNumber)
			{
				tipText = "The universe number MUST be UNIQUE!";
			}
			tipTool.SetToolTip(numNumber, tipText);
			tipTool.SetToolTip(lblNumber, tipText);

			if (!loading) MakeDirty(true);
		}
		/*
		public bool ValidateUniverse()
		{
			bool valid = true;
			if (!loading)
			{
				if (universes != null)
				{
					tipTool.SetToolTip(numNumber, "The number for this universe");
					for (int u = 0; u < universes.Count; u++)
					{
						if (!universes[u].Editing)
						{
							if (universe.UniverseNumber == universes[u].UniverseNumber)
							{
								valid = false;
								tipTool.SetToolTip(numNumber, "The universe number MUST be UNIQUE!");
								u = universes.Count; // Force exit loop
							}
						}
					}
				}
			}
			return valid;
		}
		*/
		private void txtConnection_Validating(object sender, CancelEventArgs e)
		{
			universe.Connection = txtConnection.Text.TrimStart();
			txtConnection.Text = universe.Connection;
			if (!loading) MakeDirty(true);
		}

		private void txtLocation_Validating(object sender, CancelEventArgs e)
		{
			universe.Location = txtLocation.Text.TrimStart();
			txtLocation.Text = universe.Location;
			if (!loading) MakeDirty(true);
		}

		private void txtComment_Validating(object sender, CancelEventArgs e)
		{
			universe.Comment = txtComment.Text.TrimStart();
			txtComment.Text = universe.Comment;
			if (!loading) MakeDirty(true);
		}

		private void numSize_ValueChanged(object sender, EventArgs e)
		{
			universe.SizeLimit = (int)numSize.Value;
			if (!loading) MakeDirty(true);
		}

		private void numxStart_ValueChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				universe.xLightsAddress = (int)numxStart.Value;
				MakeDirty(true);
			}
		}

		private void frmUniverse_Shown(object sender, EventArgs e)
		{
			if (Owner != null)
			{
				SetFormPosition();
			}
			else
			{
				Fyle.MakeNoise(Fyle.Noises.Dammit);
			}

		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			string tipText = "The name of this universe";
			string theName = txtName.Text.TrimStart();
			txtName.Text = theName;
			universe.Name = theName;

			if (universe.BadName)
			{
				tipText = "The universe MUST have a UNIQUE name!";
			}
			else
			{
				// keep default tooltip text (above)
			}
			tipTool.SetToolTip(txtName, tipText);
			tipTool.SetToolTip(lblName, tipText);
			if (!loading) MakeDirty(true);

		}
	}
}
