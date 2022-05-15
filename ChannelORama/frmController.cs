using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOR4Utils;
using FileHelper;

namespace UtilORama4
{
	public partial class frmController : Form
	{
		public List<DMXUniverse> universes = null;
		public DMXController controller = null;
		private bool loading = true;
		public bool dirty = false;
		private int oldUnivID = -1;
		private bool moved = false;
		public frmController()
		{
			InitializeComponent();
		}

		public frmController(DMXController ctlr, List<DMXUniverse> universeList)
		{
			InitializeComponent();
			universes = universeList;
			FillCombos();
			LoadController(ctlr);
			loading = false;
			dirty = false;

		}

		public void FillCombos()
		{
			cboUniverse.Items.Clear();
			if (universes != null)
			{
				for (int u = 0; u < universes.Count; u++)
				{
					ListItem li = new ListItem(universes[u].ToString(), universes[u].ID);
					cboUniverse.Items.Add(li);
				}
			}
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

		public void LoadController(DMXController ctlr)
		{
			controller = ctlr;
			txtName.Text = controller.Name;
			txtLocation.Text = controller.Location;
			txtComment.Text = controller.Comment;
			txtLetter.Text = controller.LetterID;
			txtModel.Text = controller.ControllerModel;
			numCount.Value = controller.OutputCount;
			numStart.Value = controller.DMXStartAddress;
			chkActive.Checked = controller.Active;
			oldUnivID = controller.DMXUniverse.ID;
			int e = controller.DMXStartAddress + controller.OutputCount - 1;
			lblLastDMX.Text = "Last DMX address: " + e.ToString();
			int s = controller.DMXUniverse.xLightsAddress + controller.DMXStartAddress - 1;
			e = s + controller.OutputCount - 1;
			lblxAddresses.Text = "xLights addresses " + s.ToString() + "-" + e.ToString();

			for (int u = 0; u < cboUniverse.Items.Count; u++)
			{
				ListItem li = (ListItem)cboUniverse.Items[u];
				if (li.ID == controller.DMXUniverse.ID)
				{
					cboUniverse.SelectedIndex = u;
					u = cboUniverse.Items.Count; // Force exit of loop
				}
			}

			string brd = controller.ControllerBrand.ToLower();
			for (int i = 0; i < cboBrand.Items.Count; i++)
			{
				if (brd.CompareTo(cboBrand.Items[i].ToString().ToLower()) == 0)
				{
					cboBrand.SelectedIndex = i;
					i = cboBrand.Items.Count; // Exit loop
				}
			}
			MakeDirty(false);
			loading = false;


		}

		private void txtLetter_TextChanged(object sender, EventArgs e)
		{
			if (universes != null)
			{
				string tipText = "Assign a letter to this controller as an Identifier.";
				controller.LetterID = txtLetter.Text.Trim().ToUpper();
				if (controller.BadLetter)
				{
					tipText = "You MUST assign a UNIQUE letter to this controller as an Identifier.";
				}
				else
				{
					// Use default tipText (above)
				}
				tipTool.SetToolTip(txtLetter, tipText);
				tipTool.SetToolTip(lblLetter, tipText);
				if (!loading) MakeDirty(true);
			}
		}

		private void txtName_Validating(object sender, CancelEventArgs e)
		{
		}

		private void cboUniverse_SelectedIndexChanged(object sender, EventArgs e)
		{
			controller.DMXUniverse = universes[cboUniverse.SelectedIndex];
			int ed = controller.DMXStartAddress + controller.OutputCount - 1;
			lblLastDMX.Text = "Last DMX address: " + ed.ToString();
			int st = controller.DMXUniverse.xLightsAddress + controller.DMXStartAddress - 1;
			ed = st + controller.OutputCount - 1;
			lblxAddresses.Text = "xLights addresses " + st.ToString() + "-" + ed.ToString();
			if (!loading) MakeDirty(true);

		}

		private void cboUniverse_Validating(object sender, CancelEventArgs e)
		{
			controller.DMXUniverse = universes[cboUniverse.SelectedIndex];
			int ed = controller.DMXStartAddress + controller.OutputCount - 1;
			lblLastDMX.Text = "Last DMX address: " + ed.ToString();
			int st = controller.DMXUniverse.xLightsAddress + controller.DMXStartAddress - 1;
			ed = st + controller.OutputCount - 1;
			lblxAddresses.Text = "xLights addresses " + st.ToString() + "-" + ed.ToString();
			if (!loading) MakeDirty(true);

		}

		private void numStart_Validating(object sender, CancelEventArgs e)
		{
			int ed = controller.DMXStartAddress + controller.OutputCount - 1;
			lblLastDMX.Text = "Last DMX address: " + ed.ToString();
			int st = controller.DMXUniverse.xLightsAddress + controller.DMXStartAddress - 1;
			ed = st + controller.OutputCount - 1;
			lblxAddresses.Text = "xLights addresses " + st.ToString() + "-" + ed.ToString();
			controller.DMXStartAddress = (int)numStart.Value;
			if (!loading) MakeDirty(true);

		}

		private void numCount_Validating(object sender, CancelEventArgs e)
		{
			int ed = controller.DMXStartAddress + controller.OutputCount - 1;
			lblLastDMX.Text = "Last DMX address: " + ed.ToString();
			int st = controller.DMXUniverse.xLightsAddress + controller.DMXStartAddress - 1;
			ed = st + controller.OutputCount - 1;
			lblxAddresses.Text = "xLights addresses " + st.ToString() + "-" + ed.ToString();
			controller.OutputCount = (int)numCount.Value;
			if (!loading) MakeDirty(true);

		}

		private void numCount_ValueChanged(object sender, EventArgs e)
		{
			controller.OutputCount = (int)numCount.Value;
			int ed = controller.DMXStartAddress + controller.OutputCount - 1;
			lblLastDMX.Text = "Last DMX address: " + ed.ToString();
			int st = controller.DMXUniverse.xLightsAddress + controller.DMXStartAddress - 1;
			ed = st + controller.OutputCount - 1;
			lblxAddresses.Text = "xLights addresses " + st.ToString() + "-" + ed.ToString();
			if (!loading) MakeDirty(true);
		}

		private void numStart_ValueChanged(object sender, EventArgs e)
		{
			controller.DMXStartAddress = (int)numStart.Value;
			int ed = controller.DMXStartAddress + controller.OutputCount - 1;
			lblLastDMX.Text = "Last DMX address: " + ed.ToString();
			int st = controller.DMXUniverse.xLightsAddress + controller.DMXStartAddress - 1;
			ed = st + controller.OutputCount - 1;
			lblxAddresses.Text = "xLights addresses " + st.ToString() + "-" + ed.ToString();
			if (!loading) MakeDirty(true);
		}

		public void MakeDirty(bool isDirty)
		{
			if (dirty != isDirty)
			{
				if (isDirty)
				{
					if (!loading)
					{
						this.Text = "Controller: " + controller.LetterID + ": " + controller.Name + " (Modified)";
						dirty = isDirty;
					}
				}
				else
				{
					this.Text = "Controller: " + controller.LetterID + ": " + controller.Name;
					dirty = isDirty;
				}
			}
		}

		private void frmController_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen p = new Pen(Color.Red, 2);

			if (controller.BadLetter)
			{
				int w = txtLetter.Left - lblLetter.Left + lblLetter.Width + txtLetter.Width;
				g.DrawRectangle(p, lblLetter.Left - 2, txtLetter.Top - 2, w + 4, txtLetter.Height + 4);
			}
			if (controller.BadName)
			{
				g.DrawRectangle(p, txtName.Left - 2, txtName.Top - 2, txtName.Width + 4, txtName.Height + 4);
			}
		}

		private void txtLocation_Validating(object sender, CancelEventArgs e)
		{
			controller.Location = txtLocation.Text.TrimStart();
			txtLocation.Text = controller.Location;
			if (!loading) MakeDirty(true);

		}

		private void txtLetter_Validating(object sender, CancelEventArgs e)
		{
			/*
			controller.badLetter = false;
			string tipText = "Assign a letter to this controller as an Identifier.";
			tipTool.SetToolTip(txtLetter, tipText);
			if (txtLetter.Text.Length > 0)
			{
				string t = txtLetter.Text.TrimStart().Substring(0, 1).ToUpper();
				txtLetter.Text = t;
				for (int u = 0; u < universes.Count; u++)
				{
					DMXUniverse univ = universes[u];
					for (int c = 0; c < univ.DMXControllers.Count; c++)
					{
						if (!univ.DMXControllers[c].Editing)
						{
							if (t == univ.DMXControllers[c].LetterID)
							{
								controller.badLetter = true;
								tipText = "You MUST assign a UNIQUE letter to this controller as an Identifier.";
								tipTool.SetToolTip(txtLetter, tipText);
								c = univ.DMXControllers.Count;
								u = universes.Count; // Exit loops
							}
						}
					}
				}
			}
			else
			{
				tipText = "You MUST assign a letter to this controller as an Identifier.";
				tipTool.SetToolTip(txtLetter, tipText);
			}
			controller.LetterID = txtLetter.Text;
			MakeDirty(true);
			*/
		}

		private void chkActive_Validating(object sender, CancelEventArgs e)
		{
			controller.Active = chkActive.Checked;
			if (!loading) MakeDirty(true);
		}

		private void txtModel_Validating(object sender, CancelEventArgs e)
		{
			controller.ControllerModel = txtModel.Text.TrimStart();
			txtModel.Text = controller.ControllerModel;
			if (!loading) MakeDirty(true);
		}

		private void txtComment_Validating(object sender, CancelEventArgs e)
		{
			controller.Comment = txtComment.Text.TrimStart();
			txtComment.Text = controller.Comment;
			if (!loading) MakeDirty(true);
		}

		private void cboBrand_SelectedIndexChanged(object sender, EventArgs e)
		{
			controller.ControllerBrand = cboBrand.Text;
			if (!loading) MakeDirty(true);
		}

		private void btnChannels_Click(object sender, EventArgs e)
		{
			StringBuilder txt = new StringBuilder();
			for (int c = 0; c < controller.DMXChannels.Count; c++)
			{
				txt.Append(controller.DMXChannels[c].OutputNum.ToString());
				txt.Append(": ");
				txt.Append(controller.DMXChannels[c].Name);
				txt.Append("\r\n");
			}
			string cap = "Channels on " + controller.LetterID + ": " + controller.Name;
			DialogResult dr = MessageBox.Show(this, txt.ToString(), cap, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void frmController_Shown(object sender, EventArgs e)
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
			string tipText = "The name of this controller";
			string theName = txtName.Text.TrimStart();
			txtName.Text = theName;
			controller.Name = theName;
			if (controller.BadName)
			{
				tipText = "The controller MUST have a name!";
			}
			else
			{
				// Use default tipText (above)
			}
			tipTool.SetToolTip(txtName, tipText);
			tipTool.SetToolTip(lblName, tipText);
			if (!loading) MakeDirty(true);

		}
	}
}