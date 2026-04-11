using FileHelper;
using FormHelper;
using LOR4;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UtilORama4
{
	public partial class frmUniverse : Form
	{
		private List<DMXUniverse> universes = null;
		public DMXUniverse uniOriginal = null;
		public DMXUniverse universe = null;
		public bool isDirty = false;
		public bool renumber = false;
		public bool loading = true;
		private bool moved = false;
		public int changes = 0;
		private const int WM_SYSCOMMAND = 0x0112;
		private const int SC_MINIMIZE = 0xf020;
		public frmList owner = null;

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32() == SC_MINIMIZE)
			{
				// Minimize the owner (parent) form
				if (this.Owner != null)
				{
					this.Owner.WindowState = FormWindowState.Minimized;
				}

				// Optionally: Prevent the child from minimizing independently or closing
				m.Result = IntPtr.Zero;
				return;
			}
			base.WndProc(ref m);
		}

		public frmUniverse()
		{
			InitializeComponent();
		}

		public frmUniverse(DMXUniverse univs)
		{
			owner = this.Owner as frmList;
			InitializeComponent();
			uniOriginal = univs;
			universe = univs.Copy();
			universe.Editing = true;
			loading = true;
			if (Etc.xLightsVersion > 0)
			{
				lblxStart.Visible = false;
				numxStart.Visible = false;
			}
			if (owner != null)
			{
			}
			LoadUniverse(univs);
			MakeDirty(false);
			if (Fyle.IsAWizard)
			{
				lblDirty.Visible = true;
			}
		}

		public frmUniverse(DMXUniverse univs, frmList listform)
		{
			owner = listform;
			InitializeComponent();
			uniOriginal = univs;
			universe = univs.Copy();
			universe.Editing = true;
			loading = true;
			if (Etc.xLightsVersion > 0)
			{
				lblxStart.Visible = false;
				numxStart.Visible = false;
			}
			LoadUniverse(univs);
			MakeDirty(false);
			if (Fyle.IsAWizard)
			{
				lblDirty.Visible = true;
			}
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
			uniOriginal = univs;
			universe = univs.Copy();
			txtName.Text = universe.Name;
			txtLocation.Text = universe.Location;
			txtComment.Text = universe.Comment;
			txtConnection.Text = universe.Connection;
			numNumber.Value = universe.UniverseNumber;
			chkActive.Checked = universe.Active;
			numSize.Value = universe.MaxChannelsAllowed;
			numxStart.Value = universe.xLightsAddress;
			StringBuilder sb = new StringBuilder();
			for (int c = 0; c < universe.DMXControllers.Count; c++)
			{
				if (universe.DMXControllers[c].Identifier.Length > 0)
				{
					sb.Append(universe.DMXControllers[c].Identifier);
					sb.Append(": ");
				}
				sb.Append(universe.DMXControllers[c].Name);
				sb.Append("\r\n");
			}
			lblControllers.Text = sb.ToString();
			numSize.Minimum = universe.HighestChannel;
			MakeDirty(false);
			loading = false;
		}
		private void frmUniverse_Load(object sender, EventArgs e)
		{
			if (!moved)
			{
				Fourm.SetFormPosition(this);
				moved = true;
			}
			lblDirty.Visible = Fyle.IsAWizard;
		}


		public void MakeDirty(bool dirty)
		{
			if (dirty != isDirty)
			{
				if (dirty)
				{
					if (!loading)
					{
						this.Text = "Universe: " + universe.UniverseNumber.ToString() + ": " + universe.Name + " (Modified)";
						isDirty = true;
						lblDirty.ForeColor = Color.Red;
						lblDirty.Text = "Dirty";
					}
				}
				else
				{
					this.Text = "Universe: " + universe.UniverseNumber.ToString() + ": " + universe.Name;
					isDirty = false;
					lblDirty.ForeColor = SystemColors.GrayText;
					lblDirty.Text = "Clean";
					lblDirty.Visible = false;
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
			//string tipText = "The number for this universe";
			//universe.UniverseNumber = (int)numNumber.Value;
			//if (universe.BadNumber)
			//{
			//tipText = "The universe number MUST be UNIQUE!";
			//}
			//tipTool.SetToolTip(numNumber, tipText);
			//tipTool.SetToolTip(lblNumber, tipText);

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
			//universe.Connection = txtConnection.Text.TrimStart();
			//txtConnection.Text = universe.Connection;
			//if (!loading) MakeDirty(true);
		}

		private void txtLocation_Validating(object sender, CancelEventArgs e)
		{
			//universe.Location = txtLocation.Text.TrimStart();
			//txtLocation.Text = universe.Location;
			//if (!loading) MakeDirty(true);
		}

		private void txtComment_Validating(object sender, CancelEventArgs e)
		{
			//universe.Comment = txtComment.Text.TrimStart();
			//txtComment.Text = universe.Comment;
			//if (!loading) MakeDirty(true);
		}

		private void numSize_ValueChanged(object sender, EventArgs e)
		{
			//universe.SizeLimit = (int)numSize.Value;
			//if (!loading) MakeDirty(true);
		}

		private void numxStart_ValueChanged(object sender, EventArgs e)
		{
			//if (!loading)
			//{
			//	universe.xLightsAddress = (int)numxStart.Value;
			//	MakeDirty(true);
			//}
		}

		private void frmUniverse_Shown(object sender, EventArgs e)
		{
			FillCombos();
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			//string t = txtName.Text.Trim();
			//if (t.Length == 0)
			//{
			//	tipTool.SetToolTip(txtName, "The universe MUST have a UNIQUE name!");
			//	tipTool.SetToolTip(lblName, "The universe MUST have a UNIQUE name!");
			//}
		}

		private void txtName_Enter(object sender, EventArgs e)
		{
			string tipText = "The name of this universe";
			tipTool.SetToolTip(txtName, tipText);
			tipTool.SetToolTip(lblName, tipText);
		}

		private void txtName_Leave(object sender, EventArgs e)
		{
			string tipText = "The name of this universe";
			string t = txtName.Text.Trim();
			if (t.Length == 0)
			{
				tipText = "The universe MUST have a UNIQUE name!";
			}
			else
			{
				txtName.Text = t;
				universe.Name = t;
				if (universe.BadName)
				{
					tipText = "The universe MUST have a UNIQUE name!";
				}
			}
			tipTool.SetToolTip(txtName, tipText);
			tipTool.SetToolTip(lblName, tipText);
		}

		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				txtName.Text = universe.Name;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string tipText = "The name of this universe";
				string t = txtName.Text.Trim();
				if (t.Length == 0)
				{
					tipText = "The universe MUST have a UNIQUE name!";
				}
				else
				{
					txtName.Text = t;
					universe.Name = t;
					if (universe.BadName)
					{
						tipText = "The universe MUST have a UNIQUE name!";
					}
				}
				tipTool.SetToolTip(txtName, tipText);
				tipTool.SetToolTip(lblName, tipText);
			}
			else
			{
				// Do nothing
			}
		}

		private void txtLocation_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				txtLocation.Text = universe.Location;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = txtLocation.Text.Trim();
				if (t != universe.Location)
				{
					universe.Location = t;
					txtLocation.Text = universe.Location;
					if (!loading) MakeDirty(true);
				}
			}
			else
			{
				// Do nothing
			}
		}

		private void txtLocation_Enter(object sender, EventArgs e)
		{
			string tipText = "The location of this universe";
			tipTool.SetToolTip(txtLocation, tipText);
			tipTool.SetToolTip(lblLocation, tipText);
		}

		private void txtLocation_Leave(object sender, EventArgs e)
		{
			string t = txtLocation.Text.Trim();
			if (t != universe.Location)
			{
				universe.Location = t;
				txtLocation.Text = universe.Location;
				if (!loading) MakeDirty(true);
			}
		}

		private void numNumber_Enter(object sender, EventArgs e)
		{
			string tipText = "The number for this universe";
			tipTool.SetToolTip(numNumber, tipText);
			tipTool.SetToolTip(lblNumber, tipText);
		}

		private void numNumber_Leave(object sender, EventArgs e)
		{
			int num = (int)numNumber.Value;
			if (num != universe.UniverseNumber)
			{
				universe.UniverseNumber = num;
				if (!loading) MakeDirty(true);
			}
		}

		private void numNumber_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				numNumber.Value = universe.UniverseNumber;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				int num = (int)numNumber.Value;
				if (num != universe.UniverseNumber)
				{
					universe.UniverseNumber = num;
					if (!loading) MakeDirty(true);
				}
			}
			else
			{
				// Do nothing
			}
		}

		private void txtConnection_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				txtConnection.Text = universe.Connection;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = txtConnection.Text.Trim();
				if (t != universe.Connection)
				{
					universe.Connection = t;
					if (!loading) MakeDirty(true);
				}
			}
			else
			{
				// Do nothing
			}
		}

		private void txtConnection_Enter(object sender, EventArgs e)
		{
			string tipText = "The connection for this universe";
			tipTool.SetToolTip(txtConnection, tipText);
			tipTool.SetToolTip(lblConnection, tipText);
		}

		private void txtConnection_Leave(object sender, EventArgs e)
		{
			string t = txtConnection.Text.Trim();
			if (t != universe.Connection)
			{
				universe.Connection = t;
				txtConnection.Text = universe.Connection;
				if (!loading) MakeDirty(true);
			}
		}

		private void chkActive_CheckedChanged(object sender, EventArgs e)
		{
			// Do anything?
		}

		private void chkActive_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				chkActive.Checked = universe.Active;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				universe.Active = chkActive.Checked;
				if (!loading) MakeDirty(true);
			}
			else
			{
				// Do nothing
			}
		}

		private void chkActive_Enter(object sender, EventArgs e)
		{
			string tiptext = "Is this universe active?";
			tipTool.SetToolTip(chkActive, tiptext);
		}

		private void chkActive_Leave(object sender, EventArgs e)
		{
			if (chkActive.Checked != universe.Active)
			{
				universe.Active = chkActive.Checked;
				if (!loading) MakeDirty(true);
			}
		}

		private void numSize_Enter(object sender, EventArgs e)
		{
			String tipText = "The size limit for this universe";
			tipTool.SetToolTip(numSize, tipText);
			tipTool.SetToolTip(lblSize, tipText);
		}

		private void numSize_Leave(object sender, EventArgs e)
		{
			if ((int)numSize.Value != universe.MaxChannelsAllowed)
			{
				universe.MaxChannelsAllowed = (int)numSize.Value;
				if (!loading) MakeDirty(true);
			}
		}

		private void numSize_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				numSize.Value = universe.MaxChannelsAllowed;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if ((int)numSize.Value != universe.MaxChannelsAllowed)
				{
					universe.MaxChannelsAllowed = (int)numSize.Value;
					if (!loading) MakeDirty(true);
				}
			}
			else
			{
				// Do nothing
			}
		}

		private void numxStart_Enter(object sender, EventArgs e)
		{
			string tipText = "The xLights DMX address for this universe";
			tipTool.SetToolTip(numxStart, tipText);
			tipTool.SetToolTip(lblxStart, tipText);
		}

		private void numxStart_Leave(object sender, EventArgs e)
		{
			if ((int)numxStart.Value != universe.xLightsAddress)
			{
				universe.xLightsAddress = (int)numxStart.Value;
				if (!loading) MakeDirty(true);
			}
		}

		private void numxStart_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				numxStart.Value = universe.xLightsAddress;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if ((int)numxStart.Value != universe.xLightsAddress)
				{
					universe.xLightsAddress = (int)numxStart.Value;
					if (!loading) MakeDirty(true);
				}
			}
			else
			{
				// Do nothing
			}
		}

		private void txtComment_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				txtComment.Text = universe.Comment;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				string t = txtComment.Text.Trim();
				if (t != universe.Comment)
				{
					universe.Comment = t;
					if (!loading) MakeDirty(true);
				}
			}
			else
			{
				// Do nothing
			}
		}

		private void txtComment_Enter(object sender, EventArgs e)
		{
			string tipText = "Any comments about this universe";
			tipTool.SetToolTip(txtComment, tipText);
			tipTool.SetToolTip(lblComment, tipText);
		}

		private void txtComment_Leave(object sender, EventArgs e)
		{
			string t = txtComment.Text.Trim();
			if (t != universe.Comment)
			{
				universe.Comment = t;
				txtComment.Text = universe.Comment;
				if (!loading) MakeDirty(true);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			SaveAndExit();
		}

		private bool SaveAndExit()
		{
			uniOriginal.ApplyChanges(universe);
			// Do not close or unload, just hide the form. The caller will check the dirty flag and decide what to do.
			this.Hide();
			return true;
		}


		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void frmUniverse_ResizeBegin(object sender, EventArgs e)
		{

		}

		private void frmUniverse_ResizeEnd(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
			{
				owner.WindowState = FormWindowState.Minimized;
				this.WindowState = FormWindowState.Normal;
			}
		}

		private void frmUniverse_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Fyle.isWiz)
			{
				//System.Diagnostics.Debugger.Break();
			}
			if (isDirty)
			{
				string dtxt = "Universe settings have changed.  Save them?";
				DialogResult dr = MessageBox.Show(this, dtxt, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.Yes)
				{
					SaveAndExit();
				}
				else if (dr == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
			}

		}
	}
}
