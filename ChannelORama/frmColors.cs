using FileHelper;
using FormHelper;
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
using LOR4;

namespace UtilORama4
{
	public partial class frmColors : Form
	{

		public bool isDirty = false;
		private Color selectedColor = Color.Black;
		public string selectedName = "";
		private bool moved = false;
		public frmChannel owner = null;

		public frmColors()
		{
			InitializeComponent();
		}

		public frmColors(Color color, frmChannel theOwner)
		{
			InitializeComponent();
			picSelection.BackColor = color;
			owner=theOwner;
		}

		public Color color
		{
			get
			{
				return picSelection.BackColor;
			}
			set
			{
				// To be called when shown, BEFORE the user has a chance to change anything, so we don't want to mark it as dirty.
				SetColor(value);
				MakeDirty(false);
			}
		}


		public void frmColors_Load(object sender, EventArgs e)
		{
			if (!moved)
			{
				Fourm.SetFormPosition(this);
				moved=true;
			}
		}

		private void color_Common_Click(object sender, EventArgs e)
		{
			// The 'Common LED Colors' colors at the top of the dialog.
			// These are not actually PictureBoxes, but Labels, so we need to cast the sender to a Label.
			Label lbl = (Label)sender;
			picSelection.BackColor = lbl.BackColor;
			picSelection.Image = null;
			selectedName = lbl.Text;
			SetColor(lbl.BackColor, lbl.Text);
		}

		private void color_Additional_Click(object sender, EventArgs e)
		{
			// The 'Additional' colors in the middle of the dialog.
			Label lbl = (Label)sender;
			picSelection.BackColor = lbl.BackColor;
			picSelection.Image = null;
			SetColor(lbl.BackColor);
		}

		private void color_Custom_Click(object sender, EventArgs e)
		{
			// The 'Custom' color at the bottom of the dialog.
			Label lbl = (Label)sender;
			picSelection.BackColor = lbl.BackColor;
			picSelection.Image = null;
			SetColor(lbl.BackColor);
		}

		private void color_Special_Click(object sender, EventArgs e)
		{
			if (sender.GetType() == typeof(Label))
			{
				// The 'Special' colors at the top of the dialog.
				Label lbl = (Label)sender;
				if (lbl.Text == "RGB")
				{
					picSelection.BackColor = Etc.Color_RGB;
					picSelection.Image = picRGB.Image;
					SetColor(Etc.Color_RGB);
				}
				else if (lbl.Text == "RGBW")
				{
					picSelection.BackColor = Etc.Color_RGBW;
					picSelection.Image = picRGBW.Image;
					SetColor(Etc.Color_RGBW);
				}
				else if (lbl.Text == "Multi")
				{
					picSelection.BackColor = Etc.Color_Multi;
					picSelection.Image = picMulti.Image;
					SetColor(Etc.Color_Multi);
				}
				selectedName = lbl.Text;
			}
			else if (sender.GetType() == typeof(PictureBox))
			{
				// The 'Special' color in the middle of the dialog.
				PictureBox pic = (PictureBox)sender;
				if (pic.Name == "picRGB")
				{
					picSelection.BackColor = Etc.Color_RGB;
					picSelection.Image = picRGB.Image;
					selectedName = "RGB";
					SetColor(Etc.Color_RGB);
				}
				else if (pic.Name == "picRGBW")
				{
					picSelection.BackColor = Etc.Color_RGBW;
					picSelection.Image = picRGBW.Image;
					selectedName = "RGBW";
					SetColor(Etc.Color_RGBW);
				}
				else if (pic.Name == "picMulti")
				{
					picSelection.BackColor = Etc.Color_Multi;
					picSelection.Image = picMulti.Image;
					selectedName = "Multi";
					SetColor(Etc.Color_Multi);
				}
			}
		}

		public void SetColor(Color theColor, string theName = "")
		{
			if (theColor != selectedColor)
			{
				if (theName == "")
				{
					theName = LOR4Admin.NearestColorName(theColor);
				}
				selectedName = theName;

				picSelection.BackColor = theColor;
				selectedColor = theColor;
				MakeDirty(true);
			}
		}



		public void MakeDirty(bool dirty)
		{
			if (dirty != isDirty)
			{
				if (dirty)
				{
					//if (!loading)
					{
						isDirty = true;
						lblDirty.ForeColor = Color.Red;
						lblDirty.Text = "Dirty";
						btnOK.Enabled = true;
						if (!Fyle.IsAWizard)
						{
							lblDirty.Visible = true;
						}
					}
				}
				else
				{
					isDirty = false;
					lblDirty.ForeColor = SystemColors.GrayText;
					lblDirty.Text = "Clean";
					btnOK.Enabled = false;
					if (!Fyle.IsAWizard)
					{
						lblDirty.Visible = false;
					}
				}
			}
		}


		private void btnCancel_Click(object sender, EventArgs e)
		{
			MakeDirty(false);
			this.Hide();
		}

		private void picRGB_Paint(object sender, PaintEventArgs e)
		{
			string text = "RGB";
			Font font = new Font("Arial Narrow", 8.0f, FontStyle.Bold, GraphicsUnit.Point);
			Brush brush = Brushes.Black;

			// 1. Create a StringFormat object
			using (StringFormat sf = new StringFormat())
			{
				// 2. Set horizontal and vertical alignment to Center
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				// 3. Draw the string using the control's ClientRectangle as the bounds
				e.Graphics.DrawString(text, font, brush, picRGB.ClientRectangle, sf);
			}
		}

		private void picRGBW_Paint(object sender, PaintEventArgs e)
		{
			string text = "RGBW";
			Font font = new Font("Arial Narrow", 8.0f, FontStyle.Bold, GraphicsUnit.Point);
			Brush brush = Brushes.Black;

			// 1. Create a StringFormat object
			using (StringFormat sf = new StringFormat())
			{
				// 2. Set horizontal and vertical alignment to Center
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				// 3. Draw the string using the control's ClientRectangle as the bounds
				e.Graphics.DrawString(text, font, brush, picRGBW.ClientRectangle, sf);
			}
		}

		private void picMulti_Paint(object sender, PaintEventArgs e)
		{
			string text = "Multi";
			Font font = new Font("Arial Narrow", 8.0f, FontStyle.Bold, GraphicsUnit.Point);
			Brush brush = Brushes.Black;

			// 1. Create a StringFormat object
			using (StringFormat sf = new StringFormat())
			{
				// 2. Set horizontal and vertical alignment to Center
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				// 3. Draw the string using the control's ClientRectangle as the bounds
				e.Graphics.DrawString(text, font, brush, picMulti.ClientRectangle, sf);

			}
		}

		private void picSelection_Paint(object sender, PaintEventArgs e)
		{
			string text = selectedName;
			Font font = new Font("Arial Narrow", 8.0f, FontStyle.Bold, GraphicsUnit.Point);
			Brush brush = null;

			if (selectedColor.GetBrightness() < 0.4f)
			{
				brush = Brushes.White;
			}
			else
			{
				brush = Brushes.Black;
			}

			// 1. Create a StringFormat object
			using (StringFormat sf = new StringFormat())
			{
				// 2. Set horizontal and vertical alignment to Center
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				// 3. Draw the string using the control's ClientRectangle as the bounds
				e.Graphics.DrawString(text, font, brush, picSelection.ClientRectangle, sf);
			}
		} // End event handlers for painting the text on the special color boxes and the selection box.
	} // End form class
} // End namespace
