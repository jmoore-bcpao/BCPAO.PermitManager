using OfficeOpenXml;
using System.Data;
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
		public static DataTable ToDataTable(this ExcelPackage package)
		{
			ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
			DataTable table = new DataTable();
			foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
			{
				table.Columns.Add(firstRowCell.Text);
			}
			for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
			{
				var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
				var newRow = table.NewRow();
				foreach (var cell in row)
				{
					newRow[cell.Start.Column - 1] = cell.Text;
				}
				table.Rows.Add(newRow);
			}
			return table;
		}
	}
}
