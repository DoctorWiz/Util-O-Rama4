using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoEllipsis
{
	public class LabelEllipsis : Label
	{
		public override string Text
		{
			// store full text and calculate ellipsis text
			set
			{
				longText = value;
				shortText = Ellipsis.Compact(longText, this, AutoEllipsis);

				tooltip.SetToolTip(this, longText);
				base.Text = shortText;
			}
		}

		private string longText;
		private string shortText;

		// control size changed, recalculate ellipsis text
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.Text = FullText;
		}

		#region AutoEllipsis property

		/// <summary>
		/// Get the text associated with the control without ellipsis.
		/// </summary>
		[Browsable(false)]
		public virtual string FullText
		{
			get { return longText; }
		}

		/// <summary>
		/// Get the text associated with the control truncated if it exceeds the width of the control.
		/// </summary>
		[Browsable(false)]
		public virtual string EllipsisText
		{
			get { return shortText; }
		}

		/// <summary>
		/// Indicates whether the text exceeds the witdh of the control.
		/// </summary>
		[Browsable(false)]
		public virtual bool IsEllipsis
		{
			get { return longText != shortText; }
		}
		
		private EllipsisFormat ellipsis;

		[Category("Behavior")]
		[Description("Define ellipsis format and alignment when text exceeds the width of the control")]
		public new EllipsisFormat AutoEllipsis
		{
			get { return ellipsis; }
			set
			{
				if (ellipsis != value)
				{
					ellipsis = value;
					// ellipsis type changed, recalculate ellipsis text
					this.Text = FullText;
					OnAutoEllipsisChanged(EventArgs.Empty);
				}
			}
		}

		[Category("Property Changed")]
		[Description("Event raised when the value of AutoEllipsis property is changed on Control")]
		public event EventHandler AutoEllipsisChanged;

		protected void OnAutoEllipsisChanged(EventArgs e)
		{
			if (AutoEllipsisChanged != null)
			{
				AutoEllipsisChanged(this, e);
			}
		}

		#endregion

		#region Tooltip

		ToolTip tooltip = new ToolTip();

		#endregion
	}
}
