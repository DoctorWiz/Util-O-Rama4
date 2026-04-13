using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2021.PowerPoint.Comment;
using FileHelper;
using LOR4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilORama4
{

	public class DesignerWorkaround { } // This stops VS from seeing this file as a 'Form'
	public partial class frmList : Form
	{

		public void BuildSpreadsheet(string filename)
		{
			this.Enabled = false;
			this.Cursor = Cursors.WaitCursor;
			lblLoading.Text = "Generating...";
			lblLoading.Visible = true;
			lblLoading.BringToFront();

			int lineCount = 1;
			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Channel List");

				// ===== Title =====
				var titleCell = worksheet.Cell("A1");
				titleCell.Value = "Channel List";
				titleCell.Style.Font.Bold = true;
				titleCell.Style.Font.FontSize = 18;
				titleCell.Style.Fill.BackgroundColor = XLColor.Yellow;
				titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

				worksheet.Range("A1:N1").Merge();
				worksheet.Row(1).Height = 30;
				lineCount++;
				// ===== Header Row =====
				var headers = new[] {
					"Universe",         // Column A
					"Controller",				// Column B
					"LOR Unit",					// C
					"LOR Output",       // D
					"DMX Address",      // E
					"xLights Address",  // F
					"Active",						// G
					"Name",             // H
					"Type",             // I
					"Color Single",     // J
					"Example",          // K Example Color
					"ColorHex",         // L
					"Location",         // M
					"Comment" };        // Column N

				for (int i = 0; i < headers.Length; i++)
				{
					var cell = worksheet.Cell(2, i + 1);
					cell.Value = headers[i];

					// Styling
					cell.Style.Font.Bold = true;
					cell.Style.Fill.BackgroundColor = XLColor.Red;
					cell.Style.Font.FontColor = XLColor.White;
					cell.Style.Alignment.Horizontal = ColumnAlignment(i);
					cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
				}
				lineCount++;
				foreach (Universe universe in AllUniverses)
				{
					// ===== Universe Row =====
					string[] uniline = {
						universe.Number.ToString(),          // Column A
						"",                                  // Column B
						"",                                  // C
						"",                                  // D
						"",                                  // E
						universe.xLightsAddress.ToString(),  // F
						universe.Active.ToString(),          // G
						universe.Name,                       // H
						"",                                  // I
						"",                                  // J
						"",                                  // K
						"",                                  // L
						universe.Location,                   // M
						universe.Comment };                  // Column N
					for (int i = 0; i < uniline.Length; i++)
					{
						var cell = worksheet.Cell(lineCount, i + 1);
						cell.Value = uniline[i];

						// Styling
						cell.Style.Font.Bold = true;
						cell.Style.Fill.BackgroundColor = XLColor.Green;
						cell.Style.Font.FontColor = XLColor.White;
						cell.Style.Alignment.Horizontal = ColumnAlignment(i);
						cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
					}
					lineCount++;
					foreach (Controller controller in universe.Controllers)
					{
						string[] ctlline = {
							controller.Universe.ToString(),        // Column A [0]
							controller.Identifier,                 // Column B [1]
							controller.UnitID.ToString(),          // C
							"",                                    // D
							controller.StartAddress.ToString(),    // E
							controller.xLightsAddress.ToString(),  // F
							controller.Active.ToString(),          // G
							controller.Name,                       // H
							"",                                    // I
							"",                                    // J
							"",                                    // K
							"",                                    // L
							controller.Location,                   // M
							controller.Comment};                   // Column N [13]
						if (controller.Comment.Length < 1)
							ctlline[13] = controller.ControllerBrand + " " + controller.ControllerModel;
						for (int i = 0; i < ctlline.Length; i++)
						{
							var cell = worksheet.Cell(lineCount, i + 1);
							cell.Value = ctlline[i];

							// Styling
							cell.Style.Font.Bold = true;
							cell.Style.Fill.BackgroundColor = XLColor.DarkBlue;
							cell.Style.Font.FontColor = XLColor.White;
							cell.Style.Alignment.Horizontal = ColumnAlignment(i);
							cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
						}
						lineCount++;
						foreach (Channel channel in controller.Channels)
						{
							string chex = ColorTranslator.ToHtml(channel.Color);
							string[] chanline = {
								channel.Universe.UniverseNumber.ToString(), // Column A [0]
								channel.Controller.Identifier,              // Column B [1]
								channel.Controller.UnitID.ToString(),       // C
								channel.OutputNum.ToString(),               // D
								channel.Address.ToString(),                 // E
								channel.xLightsAddress.ToString(),          // F
								channel.Active.ToString(),                  // G
								channel.Name,                               // H
								channel.DeviceType.Name,                    // I
								LOR4Admin.NearestColorName(channel.Color),  // J
								"",                                         // K [10] Example
								chex,                                       // L
								channel.Location,                           // M
								channel.Comment };                          // N [13]
							for (int i = 0; i < chanline.Length; i++)
							{
								var cell = worksheet.Cell(lineCount, i + 1);
								cell.Value = chanline[i];

								// Styling
								if (i == 10)
								{
									cell.Style.Fill.BackgroundColor = XLColor.FromColor(channel.Color);
								}
								else
								{
									cell.Style.Font.Bold = false;
									cell.Style.Fill.BackgroundColor = XLColor.White;
									if (channel.Active && channel.Controller.Active)
									{
										cell.Style.Font.FontColor = XLColor.Black;
									}
									else
									{
										cell.Style.Font.FontColor = XLColor.Gray;
									}
									cell.Style.Alignment.Horizontal = ColumnAlignment(i);
									cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
								} // End if column 10, the colro example, OR not...
							} // End Channels For Column Loop
							lineCount++;
						} // End Channels in Controller Loop
					} // End Controllers in Universe Loop
				} // End Universes Loop

				// Save file
				workbook.SaveAs(filename);

				this.Enabled = true;
				this.Cursor = Cursors.Default;
				lblLoading.Visible = false;
				lblLoading.SendToBack();
				treeChannels.Select();
				Fyle.MakeNoise(Fyle.Noises.Excellent);
				string msg = AllUniverses.Count.ToString() + " " + uniName + "s and " + AllControllers.Count.ToString() + " Controllers and " + AllChannels.Count.ToString();
				msg += " Channels exported to spreadsheet file " + filename;
				MessageBox.Show(this,msg,"Export to Spreadsheet",MessageBoxButtons.OK, MessageBoxIcon.Information);
			} // End Using Workbook
		} // End BuildSpreadsheet

		private XLAlignmentHorizontalValues ColumnAlignment(int columnNumber)
		{
			XLAlignmentHorizontalValues ret = XLAlignmentHorizontalValues.Left;
			if (columnNumber < 7)
			{
				ret = XLAlignmentHorizontalValues.Center;
			}
			return ret;
		}
	}
}
