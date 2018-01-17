using System.Data;
using System.IO;

namespace BCPAO.PermitManager.Helpers
{
	public static class TextFileHelper
    {
		public static DataTable ConvertToDataTable(string filePath)
		{
			//using (GenericParser parser = new GenericParser())
			//{
			//	parser.SetDataSource(filePath);
			//	parser.ColumnDelimiter = ',';
			//	parser.FirstRowHasHeader = false;
			//	parser.MaxBufferSize = 4096;
			//	parser.TextQualifier = '\"';

			//	while (parser.Read())
			//	{
			//		var propertyId = parser["PropertyId"];
			//	}
			//}
			
			DataTable dt = new DataTable();
			dt.Clear();
			dt.Columns.Add("Parcel ID", typeof(string));
			dt.Columns.Add("Property ID", typeof(string));
			dt.Columns.Add("Permit ID", typeof(string));
			dt.Columns.Add("Permit Status", typeof(string));
			dt.Columns.Add("Issue Date", typeof(string));
			dt.Columns.Add("Permit Amount", typeof(string));
			dt.Columns.Add("Final Date", typeof(string));
			dt.Columns.Add("Permit Type", typeof(string));
			dt.Columns.Add("Permit Code", typeof(string));
			dt.Columns.Add("Res Com", typeof(string));
			dt.Columns.Add("Address Line", typeof(string));
			dt.Columns.Add("Permit Desc", typeof(string));
			
			string[] rows = File.ReadAllLines(filePath);

			foreach (string row in rows)
			{
				DataRow dr = dt.NewRow();

				var columns = row.Split(',');
				for (int i = 0; i < columns.Length - 1; i++)
				{
					dr[i] = columns[i];
				}

				dt.Rows.Add(dr);
			}

			return dt;
		}

		public static DataTable ToDataTable(FileStream fileStream)
		{
			return null;
		}
	}
}
