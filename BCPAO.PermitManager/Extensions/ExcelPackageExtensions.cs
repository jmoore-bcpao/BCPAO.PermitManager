using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace BCPAO.PermitManager.Extensions
{
	/* https://www.mikesdotnetting.com/article/297/the-best-way-to-import-data-from-excel-to-sql-server-via-asp-net
	 * 
	 * This method is a simple utility method that takes the content of an Excel file and puts it into a DataTable.
	 * The code assumes two things: that only the first worksheet is of any interest; and that the first row of the 
	 * worksheet contains headers.It uses those header values for ColumnName values in the DataTable. If you want to 
	 * manage more than one worksheet in a workbook or your sheet doesn't contain a header row, you will need to modify 
	 * the method accordingly. */

	public static class ExcelPackageExtensions
	{
		public static DataTable ToDataTable(this ExcelPackage excelPackage, bool hasHeaderRow = true)
		{
			DataTable dt = new DataTable();
			string errorMessages = "";

			ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.First();

			// Check if the worksheet is completely empty
			if (worksheet.Dimension == null)
			{
				return dt;
			}

			// Add the columns to the datatable
			for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
			{
				string columnName = "Column " + j;
				var excelCell = worksheet.Cells[1, j].Value;

				if (excelCell != null)
				{
					var excelCellDataType = excelCell;

					// If there is a headerrow, set the next cell for the datatype and set the column name
					if (hasHeaderRow == true)
					{
						excelCellDataType = worksheet.Cells[2, j].Value;

						columnName = excelCell.ToString();

						// Check if the column name already exists in the datatable, if so make a unique name
						if (dt.Columns.Contains(columnName) == true)
						{
							columnName = columnName + "_" + j;
						}
					}

					// Try to determine the datatype for the column (by looking at the next column if there is a header row)
					if (excelCellDataType is DateTime)
					{
						dt.Columns.Add(columnName, typeof(DateTime));
					}
					else if (excelCellDataType is Boolean)
					{
						dt.Columns.Add(columnName, typeof(Boolean));
					}
					else if (excelCellDataType is Double)
					{
						// Determine if the value is a decimal or int by looking for a decimal separator
						// not the cleanest of solutions but it works since excel always gives a double
						if (excelCellDataType.ToString().Contains(".") || excelCellDataType.ToString().Contains(","))
						{
							dt.Columns.Add(columnName, typeof(Decimal));
						}
						else
						{
							dt.Columns.Add(columnName, typeof(Int64));
						}
					}
					else
					{
						dt.Columns.Add(columnName, typeof(String));
					}
				}
				else
				{
					dt.Columns.Add(columnName, typeof(String));
				}
			}
			


			// Start adding data the datatable here by looping all rows and columns
			for (int i = worksheet.Dimension.Start.Row + Convert.ToInt32(hasHeaderRow); i <= worksheet.Dimension.End.Row; i++)
			{
				// Create a new datatable row
				DataRow row = dt.NewRow();

				// Loop all columns
				for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
				{
					var excelCell = worksheet.Cells[i, j].Value;

					// Add cell value to the datatable
					if (excelCell != null)
					{
						try
						{
							row[j - 1] = excelCell;
						}
						catch
						{
							errorMessages += "Row " + (i - 1) + ", Column " + j + ". Invalid " + dt.Columns[j - 1].DataType.ToString().Replace("System.", "") + " value:  " + excelCell.ToString() + "<br>";
						}
					}
				}

				// Add the new row to the datatable
				dt.Rows.Add(row);
			}
		
			// Show error messages if needed
			var error = errorMessages;

			return dt;
		}
	}
}
