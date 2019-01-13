using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FormTools

{
	class FormTools
	{
	}

	public static void LoadFormPosition()
	{
		// Multi Monitor Aware - Restore Postition to last Used
		int posScreen;
		int posX;
		int posY;
		Screen screen;
		// Read the previous screen used from the INI file
		posScreen = Val(ReadIni(File, "Main", "PosScreen"))
		screen = screen.AllScreens(0)
		' Verify the Specified screen exists, if so then use it
		if (screen.AllScreens.Length > posScreen + 1)
		{
			screen = screen.AllScreens[0];
		}
		else
		{
			screen = screen.AllScreens[posScreen];
				}
		// Read the Position from the INI File
		posX = Val(ReadIni(File, "Main", "PosX"));
		posY = Val(ReadIni(File, "Main", "PosY"));
		Point pt As New Point(posX, posY)
		If screen.Bounds().Contains(pt) Then

			pt.X = posX - screen.Bounds().X

			pt.Y = posY - screen.Bounds().Y
		Else
			pt.X = 0

			pt.Y = 0
		End If
		Me.StartPosition = FormStartPosition.Manual
		Me.Location = screen.Bounds.Location + pt

	}

}
