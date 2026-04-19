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
	public partial class frmInput : Form
	{
		public bool isDirty = false;

		public frmInput()
		{
			InitializeComponent();
			DialogResult = DialogResult.None;
		}

		public DialogResult Show(Form owner, string message, string caption,
			MessageBoxButtons buttons = MessageBoxButtons.OKCancel,
			MessageBoxIcon icon = MessageBoxIcon.Question,
			MessageBoxDefaultButton button = MessageBoxDefaultButton.Button2)
		{
			// DialogResult ret = DialogResult.None;
			if (message.Length > 0)
			{
				lblInput.Text = message;
			}
			if (caption.Length > 0)
			{
				this.Text = caption;
			}


			return DialogResult;
		}

		public DialogResult Show(Form owner)
		{
			return DialogResult;
		}
		private void btnOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			this.Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Hide();
		}

		private void frmInput_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				DialogResult = DialogResult.Cancel;
				this.Hide();
			}
		}

		private void txtName_KeyPress(object sender, KeyPressEventArgs e)
		{
			isDirty = true;
		}

		private void txtComment_KeyPress(object sender, KeyPressEventArgs e)
		{
			isDirty = true;
		}

		private void lblInput_Click(object sender, EventArgs e)
		{

		}
	}
}
