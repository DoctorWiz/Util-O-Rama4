using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoEllipsis
{
	public class TextBoxEllipsis : TextBox
	{
		public override string Text
		{
			// store full text, calculate ellipsis text and
			// display full text if textbox has focused, display truncated text otherwise
			set
			{
				longText = value;
				shortText = Ellipsis.Compact(longText, this, AutoEllipsis);

				tooltip.SetToolTip(this, longText);
				base.Text = Focused ? longText : shortText;
			}
		}

		private string longText;
		private string shortText;

		// control size changed, recalculate ellipsis text
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			if (!Focused) // doesn't apply if textbox has the focus
			{
				this.Text = FullText;
			}
		}

		// control gains focus, display full text
		protected override void OnGotFocus(EventArgs e)
		{
			base.Text = FullText;
			base.OnGotFocus(e);
		}

		// lose focus, calculate ellipsis of (possibly) modified text
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.Text = base.Text;
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
		public virtual EllipsisFormat AutoEllipsis
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

		#region P/Invoke for context menu

		// credit is due to zsh
		// http://www.codeproject.com/KB/edit/cmenuedit.aspx?msg=2774005#xx2774005xx

		const uint MSGF_MENU = 2;
		const uint OBJID_CLIENT = 0xFFFFFFFC;

		const uint MF_SEPARATOR = 0x800;
		const uint MF_BYCOMMAND = 0;
		const uint MF_POPUP = 16;
		const uint MF_UNCHECKED = 0;
		const uint MF_CHECKED = 8;

		const int WM_ENTERIDLE = 0x121;
		const int WM_APP = 0x8000;

		// user-defined windows messages
		const int WM_NONE = WM_APP + 1;
		const int WM_LEFT = WM_APP + 2;
		const int WM_RIGHT = WM_APP + 3;
		const int WM_CENTER = WM_APP + 4;
		const int WM_PATH = WM_APP + 5;
		const int WM_WORD = WM_APP + 6;

		[StructLayout(LayoutKind.Sequential)]
		struct RECT
		{
			int Left;
			int Top;
			int Right;
			int Bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		struct MENUBARINFO
		{
			public int cbSize;
			public RECT rcBar;
			public IntPtr hMenu;
			public IntPtr hwndMenu;
			public ushort fBarFocused_fFocused;
		}

		[DllImport("user32.dll")]
		static extern bool GetMenuBarInfo(IntPtr hwnd, uint idObject, uint idItem, ref MENUBARINFO pmbi);

		[DllImport("user32.dll")]
		static extern int GetMenuState(IntPtr hMenu, uint uId, uint uFlags);

		[DllImport("user32.dll")]
		static extern uint AppendMenu(IntPtr hMenu, uint uFlags, uint uIDNewItem, string lpNewItem);

		[DllImport("user32.dll")]
		static extern IntPtr CreatePopupMenu();

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_ENTERIDLE:
					base.WndProc(ref m);

					if (MSGF_MENU == (int)m.WParam)
					{
						MENUBARINFO mbi = new MENUBARINFO();
						mbi.cbSize = Marshal.SizeOf(mbi);

						GetMenuBarInfo(m.LParam, OBJID_CLIENT, 0, ref mbi);

						if (GetMenuState(mbi.hMenu, WM_APP + 1, MF_BYCOMMAND) == -1)
						{
							IntPtr hSubMenu = CreatePopupMenu();

							if (hSubMenu != IntPtr.Zero)
							{
								AppendMenu(hSubMenu, isChecked(EllipsisFormat.None), WM_NONE, "None");
								AppendMenu(hSubMenu, isChecked(EllipsisFormat.Start), WM_LEFT, "Left");
								AppendMenu(hSubMenu, isChecked(EllipsisFormat.End), WM_RIGHT, "Right");
								AppendMenu(hSubMenu, isChecked(EllipsisFormat.Middle), WM_CENTER, "Center");
								AppendMenu(hSubMenu, MF_SEPARATOR, 0, null);
								AppendMenu(hSubMenu, isChecked(EllipsisFormat.Path), WM_PATH, "Path Ellipsis");
								AppendMenu(hSubMenu, isChecked(EllipsisFormat.Word), WM_WORD, "Word Ellipsis");

								AppendMenu(mbi.hMenu, MF_SEPARATOR, 0, null);
								AppendMenu(mbi.hMenu, MF_POPUP, (uint)hSubMenu, "Auto Ellipsis");
							}
						}
					}
					break;

				case WM_NONE:
					AutoEllipsis = EllipsisFormat.None;
					break;

				case WM_LEFT:
					AutoEllipsis = AutoEllipsis & ~EllipsisFormat.Middle | EllipsisFormat.Start;
					break;

				case WM_RIGHT:
					AutoEllipsis = AutoEllipsis & ~EllipsisFormat.Middle | EllipsisFormat.End;
					break;

				case WM_CENTER:
					AutoEllipsis |= EllipsisFormat.Middle;
					break;

				case WM_PATH:
					if ((AutoEllipsis & EllipsisFormat.Path) == 0)
					{
						AutoEllipsis |= EllipsisFormat.Path;
					}
					else
					{
						AutoEllipsis &= ~EllipsisFormat.Path;
					}
					break;

				case WM_WORD:
					if ((AutoEllipsis & EllipsisFormat.Word) == 0)
					{
						AutoEllipsis |= EllipsisFormat.Word;
					}
					else
					{
						AutoEllipsis &= ~EllipsisFormat.Word;
					}
					break;

				default:
					base.WndProc(ref m);
					break;
			}
		}

		uint isChecked(EllipsisFormat fmt)
		{
			EllipsisFormat mask = fmt;

			switch (fmt)
			{
				case EllipsisFormat.None:
				case EllipsisFormat.Start:
				case EllipsisFormat.End:
					mask = EllipsisFormat.Middle;
					break;
			}
			return ((AutoEllipsis & mask) == fmt) ? MF_CHECKED : MF_UNCHECKED;
		}
	
		#endregion

		#region Tooltip

		ToolTip tooltip = new ToolTip();

		#endregion
	}
}
