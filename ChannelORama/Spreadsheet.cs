using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace UtilORama4
{
	class Spreadsheet
	{
		// SAMPLE / EXAMPLE
		static void WriteSheet()
		{
			string filePath = "FormattedSpreadsheet.xlsx";

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Report");

				// ===== Title =====
				var titleCell = worksheet.Cell("A1");
				titleCell.Value = "Sales Report";
				titleCell.Style.Font.Bold = true;
				titleCell.Style.Font.FontSize = 18;
				titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

				worksheet.Range("A1:D1").Merge();
				worksheet.Row(1).Height = 30;

				// ===== Header Row =====
				var headers = new[] { "Product", "Region", "Sales", "Date" };

				for (int i = 0; i < headers.Length; i++)
				{
					var cell = worksheet.Cell(2, i + 1);
					cell.Value = headers[i];

					// Styling
					cell.Style.Font.Bold = true;
					cell.Style.Fill.BackgroundColor = XLColor.LightBlue;
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
					cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
				}

				// ===== Data Rows =====
				var data = new object[,]
				{
									{ "Laptop", "North", 1200, DateTime.Now },
									{ "Phone", "South", 800, DateTime.Now },
									{ "Tablet", "West", 600, DateTime.Now }
				};

				for (int row = 0; row < data.GetLength(0); row++)
				{
					for (int col = 0; col < data.GetLength(1); col++)
					{
						var cell = worksheet.Cell(row + 3, col + 1);
						cell.Value = (XLCellValue)data[row, col];

						// Alignment
						if (col == 2) // Sales column
							cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
						else
							cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

						// Borders
						cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
					}
				}

				// ===== Column Formatting =====
				worksheet.Column(3).Style.NumberFormat.Format = "$#,##0.00"; // Currency
				worksheet.Column(4).Style.DateFormat.Format = "mm/dd/yyyy";

				// Auto-size columns
				worksheet.Columns().AdjustToContents();

				// Save file
				workbook.SaveAs(filePath);
			}

			Console.WriteLine("Spreadsheet created: " + filePath);
		} // End Sample / Example WriteSpreadsheet();
	} // End class Spreadsheet
} // End namespace UtilORama4