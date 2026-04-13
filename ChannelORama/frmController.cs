using FileHelper;
using FormHelper;
using LOR4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Data.Text;
using Windows.Media.Protection.PlayReady;

namespace UtilORama4
{
	public partial class frmController : Form
	{
		//private List<Universe> universes = null;
		public Controller ctlrOriginal = null;
		private Controller controller = null;
		private bool loading = true;
		public bool isDirty = false;
		public bool renumber = false;
		private int oldUnivID = -1;
		public int changes = 0;
		private bool moved = false;
		public bool userClose = false;
		public frmList owner = null;
		private string uniName = "";

		private const int WM_SYSCOMMAND = 0x0112;
		private const int SC_MINIMIZE = 0xf020;
		private FormWindowState prevWindowState = FormWindowState.Normal;
		public frmController()
		{
			InitializeComponent();
		}

		public frmController(Controller ctlr)
		{
			owner = this.Owner as frmList;
			uniName = frmList.uniName;
			if (uniName != "Universe")
			{
				lblUniverse.Text = uniName;

			}
			InitializeComponent();
			ctlrOriginal = ctlr;
			controller = ctlr.Copy();
			loading = true;
			controller.Editing = true;
			FillCombos();
			txtModel.Size = txtLocation.Size; // Design time size is based on brand, but txtModel may be hidden if LOR is selected as brand, so set txtModel size to match location size at run time
																				//FillCombos();
			LoadController(controller);
			MakeDirty(false);
			if (Fyle.IsAWizard)
			{
				lblDirty.Visible = true;
			}
			loading = false;
		}

		public frmController(Controller ctlr, frmList listform)
		{
			owner = listform;
			uniName = frmList.uniName;
			InitializeComponent();
			ctlrOriginal = ctlr;
			controller = ctlr.Copy();
			loading = true;
			controller.Editing = true;
			FillCombos();
			txtModel.Size = txtLocation.Size; // Design time size is based on brand, but txtModel may be hidden if LOR is selected as brand, so set txtModel size to match location size at run time
																				//FillCombos();
			LoadController(controller);
			MakeDirty(false);
			if (Fyle.IsAWizard)
			{
				lblDirty.Visible = true;
			}
			loading = false;
		}

		public void FillCombos()
		{
			cboUniverse.Items.Clear();
			int uninum = 0;

			if (owner == null)
			{
				owner = (frmList)this.Owner;
			}

			if (owner != null)
			{
				if (owner.AllUniverses != null)
				{
					for (int u = 0; u < owner.AllUniverses.Count; u++)
					{
						ListItem li = new ListItem(owner.AllUniverses[u].ToString(), owner.AllUniverses[u].ID);
						cboUniverse.Items.Add(li);
						if (owner.AllUniverses[u].ID == controller.Universe.ID)
						{
							cboUniverse.SelectedIndex = u;
						}
					}
				}
			}
			if (frmList.hasxLights)
			{
				lblxAddresses.Visible = false;
			}
			if (controller.ControllerBrand == "LOR")
			{
				ShowUnit(true);
			}
			else
			{
				ShowUnit(false);
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

		public void LoadController(Controller ctlr)
		{
			//controller = ctlr;
			txtName.Text = ctlr.Name;
			txtLocation.Text = ctlr.Location;
			txtComment.Text = ctlr.Comment;
			txtIdentifier.Text = ctlr.Identifier;
			txtModel.Text = ctlr.ControllerModel;
			numCount.Value = ctlr.OutputCount;
			numStart.Value = ctlr.StartAddress;
			chkActive.Checked = ctlr.Active;
			numUnit.Value=ctlr.UnitID;
			oldUnivID = ctlr.Universe.ID;
			int e = ctlr.StartAddress + ctlr.OutputCount - 1;
			lblLastDMX.Text = "Last DMX address: " + e.ToString();
			int s = ctlr.Universe.xLightsAddress + ctlr.StartAddress - 1;
			e = s + ctlr.OutputCount - 1;
			RefreshAddresses();

			for (int u = 0; u < cboUniverse.Items.Count; u++)
			{
				ListItem li = (ListItem)cboUniverse.Items[u];
				if (li.ID == ctlr.Universe.ID)
				{
					cboUniverse.SelectedIndex = u;
					u = cboUniverse.Items.Count; // Force exit of loop
				}
			}

			string brd = ctlr.ControllerBrand.ToLower();
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
			//if (universes != null)
			{
				//string tipText = "Assign an ID number or letter to this controller as an Identifier.";
				//controller.ControllerID = txtCtontrollerID.Text.Trim().ToUpper();
				//if (controller.BadLetter)
				//{
				//	tipText = "You MUST assign a UNIQUE letter to this controller as an Identifier.";
				//}
				//else
				//{
				// Use default tipText (above)
				//}
				//tipTool.SetToolTip(txtCtontrollerID, tipText);
				//tipTool.SetToolTip(lblIdentifier, tipText);
				//if (!loading) MakeDirty(true);
			}
		}

		private void txtName_Validating(object sender, CancelEventArgs e)
		{
		}

		private void cboUniverse_SelectedIndexChanged(object sender, EventArgs e)
		{
			//controller.Universe = universes[cboUniverse.SelectedIndex];
			//int ed = controller.StartAddress + controller.OutputCount - 1;
			//lblLastDMX.Text = "Last DMX address: " + ed.ToString();
			//int st = controller.Universe.xLightsAddress + controller.StartAddress - 1;
			//ed = st + controller.OutputCount - 1;
			//lblxAddresses.Text = "xLights addresses " + st.ToString() + "-" + ed.ToString();
			//if (!loading) MakeDirty(true);

		}

		private void cboUniverse_Validating(object sender, CancelEventArgs e)
		{
			//controller.Universe = universes[cboUniverse.SelectedIndex];
			//int ed = controller.StartAddress + controller.OutputCount - 1;
			//lblLastDMX.Text = "Last DMX address: " + ed.ToString();
			//int st = controller.Universe.xLightsAddress + controller.StartAddress - 1;
			//ed = st + controller.OutputCount - 1;
			//lblxAddresses.Text = "xLights addresses " + st.ToString() + "-" + ed.ToString();
			//if (!loading) MakeDirty(true);

		}

		private void numStart_Validating(object sender, CancelEventArgs e)
		{
			//int ed = controller.StartAddress + controller.OutputCount - 1;
			//lblLastDMX.Text = "Last DMX address: " + ed.ToString();
			//int st = controller.Universe.xLightsAddress + controller.StartAddress - 1;
			//ed = st + controller.OutputCount - 1;
			//lblxAddresses.Text = "xLights addresses " + st.ToString() + "-" + ed.ToString();
			//controller.StartAddress = (int)numStart.Value;
			//if (!loading) MakeDirty(true);

		}

		private void numCount_Validating(object sender, CancelEventArgs e)
		{
			//int ed = controller.StartAddress + controller.OutputCount - 1;
			//lblLastDMX.Text = "Last DMX address: " + ed.ToString();
			//int st = controller.Universe.xLightsAddress + controller.StartAddress - 1;
			//ed = st + controller.OutputCount - 1;
			//lblxAddresses.Text = "xLights addresses " + st.ToString() + "-" + ed.ToString();
			//controller.OutputCount = (int)numCount.Value;
			//if (!loading) MakeDirty(true);

		}

		private void numCount_ValueChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				controller.OutputCount = (int)numCount.Value;
				RefreshAddresses();
				MakeDirty(true);
			}
		}

		private void numStart_ValueChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				controller.StartAddress = (int)numStart.Value;
				RefreshAddresses();
				MakeDirty(true);
			}
		}

		public void MakeDirty(bool dirty)
		{
			if (dirty != isDirty)
			{
				string ttxt = "Controller: ";
				if (controller.Identifier.Length > 0)
				{
					ttxt += controller.Identifier + ": ";
				}
				ttxt += controller.Name;
				if (isDirty)
				{
					if (!loading)
					{
						this.Text = ttxt + " (Modified)";
						isDirty = true;
						lblDirty.ForeColor = Color.Red;
						lblDirty.Text = "Dirty";
					}
				}
				else
				{
					this.Text = ttxt;
					isDirty = false;
					lblDirty.ForeColor = SystemColors.GrayText;
					lblDirty.Text = "Clean";
				}
			}
		}

		private void frmController_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen p = new Pen(Color.Red, 2);

			if (controller.BadIdentity)
			{
				int w = txtIdentifier.Left - lblIdentifier.Left + lblIdentifier.Width + txtIdentifier.Width;
				g.DrawRectangle(p, lblIdentifier.Left - 2, txtIdentifier.Top - 2, w + 4, txtIdentifier.Height + 4);
			}
			if (controller.BadName)
			{
				g.DrawRectangle(p, txtName.Left - 2, txtName.Top - 2, txtName.Width + 4, txtName.Height + 4);
			}
			if (controller.BadAddress)
			{
				int w = lblStart.Left - lblStart.Left + lblStart.Width + numStart.Width;
				g.DrawRectangle(p, lblStart.Left - 2, numStart.Top - 2, w + 4, numStart.Height + 4);
			}
		}

		private void txtLocation_Validating(object sender, CancelEventArgs e)
		{
			//controller.Location = txtLocation.Text.TrimStart();
			//txtLocation.Text = controller.Location;
			//if (!loading) MakeDirty(true);

		}

		private void txtIdentifier_Validating(object sender, CancelEventArgs e)
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
					Universe univ = universes[u];
					for (int c = 0; c < univ.Controllers.Count; c++)
					{
						if (!univ.Controllers[c].Editing)
						{
							if (t == univ.Controllers[c].ControllerID)
							{
								controller.badLetter = true;
								tipText = "You MUST assign a UNIQUE letter to this controller as an Identifier.";
								tipTool.SetToolTip(txtLetter, tipText);
								c = univ.Controllers.Count;
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
			controller.ControllerID = txtLetter.Text;
			MakeDirty(true);
			*/
		}

		private void chkActive_Validating(object sender, CancelEventArgs e)
		{
			//controller.Active = chkActive.Checked;
			//if (!loading) MakeDirty(true);
		}

		private void txtModel_Validating(object sender, CancelEventArgs e)
		{
			//controller.ControllerModel = txtModel.Text.TrimStart();
			//txtModel.Text = controller.ControllerModel;
			//if (!loading) MakeDirty(true);
		}

		private void txtComment_Validating(object sender, CancelEventArgs e)
		{
			//controller.Comment = txtComment.Text.TrimStart();
			//txtComment.Text = controller.Comment;
			//if (!loading) MakeDirty(true);
		}

		private void cboBrand_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				controller.ControllerBrand = cboBrand.Text;
				if (controller.ControllerBrand == "LOR")
				{
					ShowUnit(true);
				}
				else
				{
					ShowUnit(false);
				}
				MakeDirty(true);
			}
		}

		private void ShowUnit(bool show)
		{
			if (show)
			{
				cboLORModels.Location = txtModel.Location;
				cboLORModels.Visible = true;
				lblUnit.Visible = true;
				numUnit.Visible = true;
				txtModel.Visible = false;
				txtModel.Width = 290;

				if (controller.ControllerModel.IndexOf("24") > 0)
				{
					// Leave showUnit = false
					if (controller.OutputCount != 24)
					{
						controller.OutputCount = 24;
						numCount.Value = 24;
						numCount.Enabled = false;
						if (!loading)
							ValidateOutputCount(24);
					} // End if new count is not 24
				} // End if model includes 24 channels
				else if (controller.ControllerModel.IndexOf("16") > 0)
				{
					controller.OutputCount = 16;
					numCount.Value = 16;
					numCount.Enabled = false;
					int st = (controller.UnitID - 1) * 16 + 1;
					numStart.Value = st;
					controller.StartAddress = st;
					if (!loading)
						ValidateOutputCount(16);
				}
				else if (controller.ControllerModel.IndexOf("8") > 0)
				{
					controller.OutputCount = 8;
					numCount.Value = 8;
					numCount.Enabled = false;
					int st = (controller.UnitID - 1) * 16 + 1;
					numStart.Value = st;
					controller.StartAddress = st;
					if (!loading)
						ValidateOutputCount(8);
				}
				else if (controller.ControllerModel.IndexOf("4") > 0)
				{
					controller.OutputCount = 4;
					numCount.Value = 24;
					numCount.Enabled = false;
					int st = (controller.UnitID - 1) * 16 + 1;
					numStart.Value = st;
					controller.StartAddress = st;
					if (!loading)
						ValidateOutputCount(4);
				}

			}
			else
			{
				cboLORModels.Visible = false;
				lblUnit.Visible = false;
				numUnit.Visible = false;
				txtModel.Visible = true;
				txtModel.Size = txtLocation.Size;
			}
		}


		private void btnChannels_Click(object sender, EventArgs e)
		{
			StringBuilder txt = new StringBuilder();
			for (int c = 0; c < controller.Channels.Count; c++)
			{
				txt.Append(controller.Channels[c].OutputNum.ToString());
				txt.Append(": ");
				txt.Append(controller.Channels[c].Name);
				txt.Append("\r\n");
			}
			string cap = "Channels on ";
			if (controller.Identifier.Length > 0)
			{
				cap += controller.Identifier + ": ";
			}
			cap += controller.ControllerBrand + " " + controller.Name;
			DialogResult dr = MessageBox.Show(this, txt.ToString(), cap, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void frmController_Shown(object sender, EventArgs e)
		{
			if (owner != null)
			{
				//FillCombos();
			}
			else
			{
				string dtxt = "Error: No owner form found. This form must be opened from the Controller List form.";
				Fyle.MakeNoise(Fyle.Noises.Dammit);
				DialogResult dr = MessageBox.Show(this, dtxt, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				if (Fyle.isWiz)
				{
					System.Diagnostics.Debugger.Break();
				}
				//this.Close();
				//return;	
			}
			//loading = false;
			//dirty = false;

		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			string tipText = "The name of this controller";
			string t = txtName.Text.Trim();
			if (t.Length == 0)
			{
				tipText = "The controller MUST have a name!";
			}
			tipTool.SetToolTip(txtName, tipText);
			tipTool.SetToolTip(lblName, tipText);
			if (!loading) MakeDirty(true);

		}

		private void cboLORModels_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				string t = cboLORModels.Text.Trim();
				bool showUnit = false;
				if (t != controller.ControllerModel)
				{
					controller.ControllerModel = t;
					txtModel.Text = cboLORModels.Text;
					ShowUnit(true);
					RefreshAddresses();
					MakeDirty(true);
				}
			}
		}

		private DialogResult ValidateOutputCount(int submodel)
		{
			DialogResult dr = DialogResult.OK;
			if (controller.ControllerModel.IndexOf(submodel.ToString()) > 0)
			{
				if (controller.OutputCount != submodel)
				{
					string dtxt = "The model you have selected includes " + submodel.ToString() + " channels. Do you want to set the channel count to 16?";
					dr = MessageBox.Show(this, dtxt, "Output Count", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dr == DialogResult.Yes)
					{
						controller.OutputCount = submodel;
						numCount.Value = submodel;
					}
				}  // End if new count is not xx
			} // End if model includes xx channels
			return dr;
		}


		private void txtModel_TextChanged(object sender, EventArgs e)
		{
			//if (!loading) MakeDirty(true);
		}


		private void txtModel_Leave(object sender, EventArgs e)
		{
			string t = txtModel.Text.Trim();
			if (t != controller.ControllerModel)
			{
				controller.ControllerModel = t;
				txtModel.Text = controller.ControllerModel;
				if (!loading) MakeDirty(true);
			}
		}

		private void txtName_Leave(object sender, EventArgs e)
		{
			string tipText = "The name of this controller";
			string t = txtModel.Text.Trim();
			if (controller.BadName)
			{
				tipText = "The controller MUST have a name!";
			}
			else
			{
				// Use default tipText (above)
				if (t != controller.Name)
				{
					controller.Name = t;
					txtName.Text = controller.Name;
					if (!loading) MakeDirty(true);
				}
			}
			tipTool.SetToolTip(txtName, tipText);
			tipTool.SetToolTip(lblName, tipText);
			if (!loading) MakeDirty(true);
		}

		private void txtName_Enter(object sender, EventArgs e)
		{
			string tipText = "The name of this controller";
			tipTool.SetToolTip(txtName, tipText);
			tipTool.SetToolTip(lblName, tipText);
		}

		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			// If escape is pressed, restore to the original value
			if (e.KeyCode == Keys.Escape)
			{
				txtName.Text = ctlrOriginal.Name;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = txtName.Text.Trim();
				if (t != ctlrOriginal.Name)
				{
					controller.Name = t;
					if (!loading) MakeDirty(true);
				}
			}

		}

		private void txtModel_Enter(object sender, EventArgs e)
		{
			string tipText = "The model of this controller";
			tipTool.SetToolTip(txtModel, tipText);
			tipTool.SetToolTip(lblModel, tipText);
		}

		private void txtModel_KeyDown(object sender, KeyEventArgs e)
		{
			// If escape is pressed, restore to the original value
			if (e.KeyCode == Keys.Escape)
			{
				txtModel.Text = ctlrOriginal.ControllerModel;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = txtModel.Text.Trim();
				if (t != ctlrOriginal.ControllerModel)
				{
					controller.ControllerModel = t;
					if (!loading) MakeDirty(true);
				}
			}

		}

		private void txtLocation_Enter(object sender, EventArgs e)
		{
			string tipText = "The location of this controller";
			tipTool.SetToolTip(txtLocation, tipText);
			tipTool.SetToolTip(lblLocation, tipText);
		}

		private void txtLocation_KeyDown(object sender, KeyEventArgs e)
		{
			// If escape is pressed, restore to the original value
			if (e.KeyCode == Keys.Escape)
			{
				txtLocation.Text = ctlrOriginal.Location;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = txtLocation.Text.Trim();
				if (t != ctlrOriginal.Location)
				{
					controller.Location = t;
					if (!loading) MakeDirty(true);
				}
			}

		}

		private void txtLocation_Leave(object sender, EventArgs e)
		{
			string t = txtLocation.Text.Trim();
			if (t != controller.Location)
			{
				controller.Location = t;
				txtLocation.Text = controller.Location;
				if (!loading) MakeDirty(true);
			}
		}

		private void cboUniverse_Enter(object sender, EventArgs e)
		{
			string tipText = "The DMX Universe this controller is on.";
			if (uniName != "Universe")
			{
				tipText = "The " + uniName + " this controller is on.";
			}
			tipTool.SetToolTip(cboUniverse, tipText);
			tipTool.SetToolTip(lblUniverse, tipText);
		}

		private void cboUniverse_Leave(object sender, EventArgs e)
		{
			if (cboUniverse.SelectedIndex >= 0)
			{
				string t = cboUniverse.Text.Trim();
				if (t != controller.Universe.Name)
				{
					controller.Universe = owner.AllUniverses[cboUniverse.SelectedIndex];
					RefreshAddresses();
					if (!loading)
					{
						MakeDirty(true);
						renumber = true;
					}
				}
			}
		}

		private void cboUniverse_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				for (int u = 0; u < cboUniverse.Items.Count; u++)
				{
					ListItem li = (ListItem)cboUniverse.Items[u];
					if (li.Name == ctlrOriginal.Universe.Name)
					{
						cboUniverse.SelectedIndex = u;
						u = cboUniverse.Items.Count; // Force exit of loop
					}
				}
				controller.Universe = ctlrOriginal.Universe;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = cboUniverse.Text.Trim();
				if (t != controller.Universe.Name)
				{
					controller.Universe = owner.AllUniverses[cboUniverse.SelectedIndex];
					RefreshAddresses();
					if (!loading)
					{
						MakeDirty(true);
						renumber = true;
					}
				}
			}
		}

		private void RefreshAddresses()
		{
			int ed = controller.StartAddress + controller.OutputCount - 1;
			lblLastDMX.Text = "Last DMX address: " + ed.ToString();
			int st = controller.Universe.xLightsAddress + controller.StartAddress - 1;
			ed = st + controller.OutputCount - 1;
			if (frmList.hasxLights)
			{
				lblxAddresses.Text = "xLights addresses " + st.ToString() + "-" + ed.ToString();
			}
		}

		private void chkActive_CheckedChanged(object sender, EventArgs e)
		{
			if (!loading)
			{
				controller.Active = chkActive.Checked;
				// This affects the BadOutput flag
				// Need to refresh the form to show or hide any BadOutput indicators??
				MakeDirty(true);
			}

			// Need to do anything?
		}

		private void chkActive_Enter(object sender, EventArgs e)
		{
			string tipText = "Whether this controller is active or not. Inactive controllers will be ignored by the software.";
			tipTool.SetToolTip(chkActive, tipText);
		}

		private void chkActive_Leave(object sender, EventArgs e)
		{
			if (chkActive.Checked != controller.Active)
			{
				controller.Active = chkActive.Checked;
				if (!loading) MakeDirty(true);
			}
		}

		private void chkActive_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				chkActive.Checked = ctlrOriginal.Active;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if (chkActive.Checked != ctlrOriginal.Active)
				{
					controller.Active = chkActive.Checked;
					if (!loading) MakeDirty(true);
				}
			}

		}

		private void cboBrand_Enter(object sender, EventArgs e)
		{
			string tipText = "The brand of this controller. This may affect the available models and other settings.";
			tipTool.SetToolTip(cboBrand, tipText);
			tipTool.SetToolTip(lblBrand, tipText);
		}

		private void cboBrand_Leave(object sender, EventArgs e)
		{
			string t = cboBrand.Text.Trim();
			if (t != controller.ControllerBrand)
			{
				controller.ControllerBrand = t;
				if (!loading) MakeDirty(true);
				if (controller.ControllerBrand == "LOR")
				{
					cboLORModels.Location = txtModel.Location;
					cboLORModels.Visible = true;
					txtModel.Visible = false;
				}
				else
				{
					cboLORModels.Visible = false;
					txtModel.Visible = true;
				}
			}
		}

		private void cboBrand_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				cboBrand.Text = ctlrOriginal.ControllerBrand;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = cboBrand.Text.Trim();
				if (t != ctlrOriginal.ControllerBrand)
				{
					controller.ControllerBrand = t;
					if (!loading) MakeDirty(true);
					if (controller.ControllerBrand == "LOR")
					{
						cboLORModels.Location = txtModel.Location;
						cboLORModels.Visible = true;
						txtModel.Visible = false;
					}
					else
					{
						cboLORModels.Visible = false;
						txtModel.Visible = true;
					}
				}
			}
		}

		private void numStart_Leave(object sender, EventArgs e)
		{
			if (numStart.Value != controller.StartAddress)
			{
				int v = (int)numStart.Value;
				int vminus = v = 1;
				int sminus = (int)(vminus / 16);
				int start = sminus + 1;
				if (sminus * 16 != vminus)
				{
					string dtxt = "The DMX start address should be a multiple of 16. Do you want to set it to " + start.ToString() + "?";
					DialogResult = MessageBox.Show(this, dtxt, "DMX Start Address", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (DialogResult == DialogResult.Yes)
					{
						int vnew = start * 16 + 1;
						numStart.Value = vnew;
					}
				}
				bool unitchek = false;
				if (controller.ControllerBrand == "LOR")
				{
					if (controller.ControllerModel.IndexOf("CTB16") > 0)
					{
						unitchek = true;
					}
					else if (controller.ControllerModel.IndexOf("LOR160") >= 0)
					{
						if (controller.ControllerModel.IndexOf("Wg3") < 2)
						{
							unitchek = true;
						}
					}
					if (unitchek)
					{
						int uni = sminus + 1;
						if (uni != controller.UnitID)
						{
							string dtxt = "To achieve a DMX start address of " + start.ToString() + ", the unit number would need to be set to " + uni.ToString() + ". Do you want to set the unit number to " + uni.ToString() + "?";
							DialogResult = MessageBox.Show(this, dtxt, "DMX Start Address", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
							if (DialogResult == DialogResult.Yes)
							{
								controller.StartAddress = start;
								controller.UnitID = uni;
							}
						}  // End if new count is not 16
					} // End if model includes 16 channels
				} // End if LOR controller






				controller.StartAddress = (int)numStart.Value;
				RefreshAddresses();
				if (!loading)
				{
					MakeDirty(true);
					renumber = true;
				}
			}
		}

		private void numStart_Enter(object sender, EventArgs e)
		{
			string tipText = "The DMX start address for this controller. This is the first DMX address that this controller will use. The number of addresses used is determined by the Output Count setting.";
			tipTool.SetToolTip(numStart, tipText);
		}

		private void numStart_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				numStart.Value = ctlrOriginal.StartAddress;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				int num = (int)numStart.Value;
				if (num != ctlrOriginal.StartAddress)
				{
					controller.StartAddress = num;
					if (!loading) MakeDirty(true);
				}
			}
		}

		private void numCount_Enter(object sender, EventArgs e)
		{
			string tipText = "The number of DMX addresses this controller uses. This, combined with the DMX start address, determines the range of DMX addresses this controller uses.";
			tipTool.SetToolTip(numCount, tipText);
		}

		private void numCount_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				numCount.Value = ctlrOriginal.OutputCount;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				int num = (int)numCount.Value;
				if (num != controller.OutputCount)
				{
					controller.OutputCount = num;
					if (!loading) MakeDirty(true);
				}
			}
		}

		private void numCount_Leave(object sender, EventArgs e)
		{
			if (numCount.Value != controller.OutputCount)
			{
				int newCount = (int)numCount.Value;
				if (controller.ControllerBrand == "LOR")
				{
					if (controller.ControllerModel.IndexOf("16") > 0)
					{
						if (newCount != 16)
						{
							string dtxt = "The model you have selected includes 16 channels. Do you really want to set the channel count to " + newCount.ToString() + "?";
							DialogResult = MessageBox.Show(this, dtxt, "Output Count", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
							if (DialogResult == DialogResult.No)
							{
								newCount = 16;
								numCount.Value = 16;
							}
						}  // End if new count is not 16
					} // End if model includes 16 channels
					else if (controller.ControllerModel.IndexOf("24") > 0)
					{
						if (newCount != 24)
						{
							string dtxt = "The model you have selected includes 24 channels (or 8 sets of 3). Do you really want to set the channel count to " + newCount.ToString() + "?";
							DialogResult = MessageBox.Show(this, dtxt, "Output Count", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
							if (DialogResult == DialogResult.No)
							{
								newCount = 24;
								numCount.Value = 24;
							}
						} // End if new count is not 24
					} // End if model includes 24 channels
					else
					{
						// Model does not include a channel count in the name, so no need to confirm change with user
					}
				} // End if LOR controller

				if (newCount != ctlrOriginal.OutputCount)
				{
					controller.OutputCount = newCount;
					RefreshAddresses();
					if (!loading)
					{
						MakeDirty(true);
						renumber = true;
					}
				} // End if count actually changed
			}
		}

		private void cboLORModels_Enter(object sender, EventArgs e)
		{
			string tipText = "The model of LOR controller. This will determine the number of channels and other settings for this controller.";
			tipTool.SetToolTip(cboLORModels, tipText);
			tipTool.SetToolTip(lblModel, tipText);
		}

		private void cboLORModels_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				cboLORModels.Text = ctlrOriginal.ControllerModel;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = cboLORModels.Text.Trim();
				if (t != ctlrOriginal.ControllerModel)
				{
					controller.ControllerModel = t;
					if (!loading) MakeDirty(true);
				}
			}
		}

		private void cboLORModels_Leave(object sender, EventArgs e)
		{
			string t = cboLORModels.Text.Trim();
			if (t != controller.ControllerModel)
			{
				controller.ControllerModel = t;
				txtModel.Text = cboLORModels.Text;

				if (controller.ControllerModel.IndexOf("16") > 0)
				{
					if (controller.OutputCount != 16)
					{
						string dtxt = "The model you have selected includes 16 channels. Do you want to set the channel count to 16?";
						DialogResult = MessageBox.Show(this, dtxt, "Output Count", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (DialogResult == DialogResult.Yes)
						{
							controller.OutputCount = 16;
							numCount.Value = 16;
						}
					}  // End if new count is not 16
				} // End if model includes 16 channels
				else if (controller.ControllerModel.IndexOf("24") > 0)
				{
					if (controller.OutputCount != 24)
					{
						string dtxt = "The model you have selected includes 24 channels (or 8 sets of 3). Do you want to set the channel count to 24?";
						DialogResult = MessageBox.Show(this, dtxt, "Output Count", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (DialogResult == DialogResult.Yes)
						{
							controller.OutputCount = 24;
							numCount.Value = 24;
						}
					} // End if new count is not 24
				} // End if model includes 24 channels
				else
				{
					// Model does not include a channel count in the name, so no need to confirm change with user
				}
				RefreshAddresses();
				if (!loading) MakeDirty(true);
			} // End if model changed
		}

		private void txtComment_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				txtComment.Text = ctlrOriginal.Comment;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = txtComment.Text.Trim();
				if (t != ctlrOriginal.Comment)
				{
					controller.Comment = t;
					if (!loading) MakeDirty(true);
				}
			}

		}

		private void txtComment_Enter(object sender, EventArgs e)
		{
			string tipText = "Any comments about this controller. This is for your reference only and is not used by the software.";
			tipTool.SetToolTip(txtComment, tipText);
			tipTool.SetToolTip(lblComment, tipText);
		}

		private void txtComment_Leave(object sender, EventArgs e)
		{
			string t = txtComment.Text.Trim();
			if (t != controller.Comment)
			{
				controller.Comment = t;
				txtComment.Text = controller.Comment;
				if (!loading) MakeDirty(true);
			}
		}

		private void frmController_ResizeBegin(object sender, EventArgs e)
		{
			if (Fyle.isWiz)
			{
				//System.Diagnostics.Debugger.Break();
			}
			prevWindowState = this.WindowState;
		}

		private void frmController_ResizeEnd(object sender, EventArgs e)
		{
			/*
			// Did the window state change?
			if (prevWindowState == FormWindowState.Normal)
			{
				// Is it now minimized?
				if (this.WindowState == FormWindowState.Minimized)
				{
					// Minimize my owner/parent
					owner.WindowState = FormWindowState.Minimized;
					// And set my own window state back to normal,
					// so that when the owner/parent is restored, I'll be normal and showing.
					this.WindowState = FormWindowState.Normal;
				}
			}
			*/

		}

		private void numUnit_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				numUnit.Value = ctlrOriginal.UnitID;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				int num = (int)numUnit.Value;
				if (num != ctlrOriginal.UnitID)
				{
					controller.UnitID = num;
					if (!loading) MakeDirty(true);
				}
			}

		}

		private void numUnit_Leave(object sender, EventArgs e)
		{
			int num = (int)numUnit.Value;
			if (num != ctlrOriginal.UnitID)
			{
				bool affectStart = false;
				if (controller.ControllerBrand == "LOR")
				{
					if (controller.ControllerModel.IndexOf("CTB") > 0)
					{
						affectStart = true;
					}  // End if model is CTB series
					else if (controller.ControllerModel.IndexOf("LOR160") > 0)
					{
						if (controller.ControllerModel.IndexOf("Wg3") < 2)
						{
							affectStart = true;
						} // End if model is LOR160 series but not Gen 3
					} // End if model is LOR1600 series	
					if (affectStart)
					{
						int u = controller.UnitID - 1;
						int s = u * 16 + 1;
						numStart.Value = s;
						RefreshAddresses();
						if (s != controller.StartAddress)
						{
							controller.StartAddress = s;
							numStart.Value = s;
							string dtxt = "The Unit Number on this model also determines the DMX start address.  The DMX start address has been changed to " + s.ToString() + ".";
							DialogResult dr = MessageBox.Show(this, dtxt, "DMX Start Address", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
						numStart.Enabled = false;
					}
					else
					{
						numStart.Enabled = true;
					}
				} // End if LOR controller

				controller.UnitID = num;
				if (!loading)
				{
					MakeDirty(true);
					renumber = true;
				}
			}
		}

		private void numUnit_Enter(object sender, EventArgs e)
		{
			string tipText = "The LOR Unit Number assigned to this controller. This affects the DMX start address for many models.";
			tipTool.SetToolTip(numUnit, tipText);
			tipTool.SetToolTip(lblUnit, tipText);

		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			//if (dr != DialogResult.Cancel)
			userClose = true;
			if (isDirty)
			{
				SaveAndExit();
			}
		}

		private bool SaveAndExit()
		{
			string dtxt = "";
			int ndd = 0;
			// Is this a new controller, with no channels yet?
			if (controller.Channels.Count < controller.OutputCount)
			{
				if (controller.Channels.Count > 0)
				{
					// Existing controller with some channels, but not enough for the new output count.  Prompt user to add the additional channels needed.
					ndd = controller.OutputCount - controller.Channels.Count;
					dtxt = "This controller has " + controller.OutputCount.ToString() + " channels, but only " + controller.Channels.Count.ToString() + " are currently defined.";
					dtxt += "\r\nWould you like to add the " + ndd + " missing channels to this controller now?";
				}
				else
				{
					dtxt = "Would you like to add " + controller.OutputCount.ToString() + " new channels to this controller now?";
				}
				DialogResult dr = MessageBox.Show(this, dtxt, "Add Channels", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.Yes)
				{
					controller.Channels.Sort();
					for (int co = 1; co <= controller.OutputCount; co++)
					{
						bool found = false;
						for (int ci = 1; ci <= controller.Channels.Count; ci++)
						{
							if (controller.Channels[ci].OutputNum == co)
							{
								found = true;
								ci = controller.Channels.Count + 1; // Force exit of loop
							}
						} // End inner loop to assigned channels count
						if (!found)
						{
							Channel ch = new Channel();
							owner.lastID++;
							ch.ID = owner.lastID;
							ch.Name = "Channel " + (co).ToString();
							ch.OutputNum = co;
							ch.Active = false;
							ch.DeviceType = owner.DeviceTypes[0]; // Default to unclassified/undefined device type
							ch.Color = Color.White;  // Most likely color
							controller.Channels.Add(ch);
						} // End if not found
					} // end outer loop to output count
				} // Dialog answer 'Yes' to add channels
			} // End if channel count is less than output count
			ctlrOriginal.ApplyChanges(controller);
			// Do not close or unload, just hide.  This allows the parent to check it's dirty status and prompt the user to save changes if needed.
			this.Hide();
			return true;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			// Abandon changes by not applying them to the original controller object.
			// Do not close or unload, just hide.  This allows the parent to check it's dirty status and prompt the user to save changes if needed.
			userClose = true;
			MakeDirty(false);
			this.Hide();
		}

		private void frmController_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Fyle.isWiz)
			{
				//System.Diagnostics.Debugger.Break();
			}
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (!userClose)
				{
					if (isDirty)
					{
						string dtxt = "Controller settings have changed.  Save them?";
						DialogResult dr = MessageBox.Show(this, dtxt, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
						if (dr == DialogResult.Yes)
						{
							DialogResult = DialogResult.OK;
							userClose = true;
							// Don't actually close/unload it, just hide it
							e.Cancel = true;
							SaveAndExit();
						}
						else if (dr == DialogResult.No)
						{
							DialogResult = DialogResult.Cancel;
							userClose = true;
							MakeDirty(false);
							// Don't actually close/unload it, just hide it
							e.Cancel = true;
							this.Hide();
						}
						else if (dr == DialogResult.Cancel)
						{
							e.Cancel = true;
						}
					} // End if dirty
				} // End if not userClose flag
			} // End if reason for closing is by user
		}

		private void txtIdentifier_Enter(object sender, EventArgs e)
		{
			string tipText = "Assign an ID number or letter(s) to this controller as an Identifier. This is for your reference only and is not used by the software.";
			tipText += "\r\n  (Suggested to keep it short, 4 characters or less.)";
			tipTool.SetToolTip(txtIdentifier, tipText);
			tipTool.SetToolTip(lblIdentifier, tipText);
		}

		private void txtIdentifier_Leave(object sender, EventArgs e)
		{
			string t = txtIdentifier.Text.Trim().ToUpper();
			if (t != controller.Identifier)
			{
				controller.Identifier = t;
				txtIdentifier.Text = controller.Identifier;
				if (!loading) MakeDirty(true);
			}
		}

		private void txtIdentifier_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				txtIdentifier.Text = ctlrOriginal.Identifier;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = txtIdentifier.Text.Trim().ToUpper();
				if (t != ctlrOriginal.Identifier)
				{
					controller.Identifier = t;
					if (!loading) MakeDirty(true);
				}
			}
		}

		private void frmController_Load(object sender, EventArgs e)
		{
			if (!moved)
			{
				Fourm.SetFormPosition(this);
				moved = true;
			}
			lblDirty.Visible = Fyle.IsAWizard;
		} // End frmController_Load

		private void numUnit_ValueChanged(object sender, EventArgs e)
		{
			int un = (int)numUnit.Value;
			int st = (un - 1) * 16 + 1;
			numStart.Value = st;
			controller.StartAddress = st;
		}
	} // End form class
} // End namespace