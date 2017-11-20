using BCPAO.PermitManager.Data;
using BCPAO.PermitManager.Extensions;
using BCPAO.PermitManager.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BCPAO.PermitManager.Controllers
{
	public class UploadFileController : Controller
	{
		private readonly IHostingEnvironment _env;
		private readonly IConfiguration _config;
		private readonly DatabaseContext _context;

		public UploadFileController(IHostingEnvironment env, IConfiguration config, DatabaseContext context)
		{
			_env = env;
			_config = config;
			_context = context;
		}

		private static void ShowColumns(DataTable columnsTable)
		{
			//var selectedRows = from info in columnsTable.AsEnumerable()
			//                                    select new
			//                                    {
			//                                             TableCatalog = info["TABLE_CATALOG"],
			//                                             TableSchema = info["TABLE_SCHEMA"],
			//                                             TableName = info["TABLE_NAME"],
			//                                             ColumnName = info["COLUMN_NAME"],
			//                                             DataType = info["DATA_TYPE"]
			//                                    };

			//Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}", "TableCatalog", "TABLE_SCHEMA", "TABLE_NAME", "COLUMN_NAME", "DATA_TYPE");

			//foreach (var row in selectedRows)
			//{
			//       Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}", row.TableCatalog,
			//                row.TableSchema, row.TableName, row.ColumnName, row.DataType);
			//}
		}

		[HttpPost("UploadFile")]
		public async Task<IActionResult> Post(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return Content("file not selected");

			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);
			//var path = Path.Combine(_env.WebRootPath, "uploads");

			if (Path.GetExtension(file.FileName).Equals(".xlsx"))
			{
				using (var stream = new FileStream(path, FileMode.Create))
				{
					await file.CopyToAsync(stream);

					var excel = new ExcelPackage(stream);

					var dt = excel.ToDataTable();
					//dt.Columns.Add("PropertyId", typeof(int));
					//dt.Columns.Add("ParcelId", typeof(string));
					//dt.Columns.Add("PermitNumber", typeof(string));
					//dt.Columns.Add("IssueDate", typeof(DateTime));
					//dt.Columns.Add("FinalDate", typeof(DateTime));
					//dt.Columns.Add("PermitValue", typeof(decimal));
					//dt.Columns.Add("PermitCode", typeof(string));
					//dt.Columns.Add("PermitDesc", typeof(string));
					//dt.Columns.Add("PermitStatus", typeof(string));
					//dt.Columns.Add("DistrictAuthority", typeof(string));
					//dt.Columns.Add("CreateDate", typeof(DateTime));

					var schema = "bcpao";
					var table = "Permits";


					var connString = _config.GetConnectionString("DefaultConnection");

					using (var conn = new SqlConnection(connString))
					{
						conn.Open();

						DataTable metaDataTable = conn.GetSchema("MetaDataCollections");
						DataTable databasesSchemaTable = conn.GetSchema("Databases");
						DataTable allTablesSchemaTable = conn.GetSchema("Tables");
						DataTable allColumnsSchemaTable = conn.GetSchema("Columns");
						DataTable allIndexColumnsSchemaTable = conn.GetSchema("IndexColumns");


						var bulkCopy = new SqlBulkCopy(conn);
						bulkCopy.DestinationTableName = $"{schema}.{table}";
						bulkCopy.BulkCopyTimeout = 0;
						bulkCopy.BatchSize = 500;

						// You can specify the Catalog, Schema, Table Name, Column Name to get the specified column(s).
						// You can use four restrictions for Column, so you should create a 4 members array.
						String[] columnRestrictions = new String[4];

						columnRestrictions[0] = null;   // 0-member represents Catalog
						columnRestrictions[1] = schema; // 1-member represents Schema
						columnRestrictions[2] = table;  // 2-member represents Table Name
						columnRestrictions[3] = null;   // 3-member represents Column Name

						DataTable schemaTable = conn.GetSchema("Columns", columnRestrictions);

						//ShowColumns(schemaTable);

						foreach (DataColumn sourceColumn in dt.Columns)
						{
							foreach (DataRow row in schemaTable.Rows)
							{
								if (string.Equals(sourceColumn.ColumnName, "Tax Account No", StringComparison.OrdinalIgnoreCase))
								{

									var source = sourceColumn.ColumnName;
									var destination = "PropertyId";

									bulkCopy.ColumnMappings.Add(source, destination);

									break;
								}
								//if (string.Equals(sourceColumn.ColumnName, "Permit Number", StringComparison.OrdinalIgnoreCase))
								//{
								//         bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, "PermitNumber");
								//       break;
								//}
								//if (string.Equals(sourceColumn.ColumnName, "Issue Date", StringComparison.OrdinalIgnoreCase))
								//{
								//         bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, "IssueDate");
								//       break;
								//}
								//if (string.Equals(sourceColumn.ColumnName, "Issue Amount", StringComparison.OrdinalIgnoreCase))
								//{
								//         bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, "IssueValue");
								//       break;
								//}
								//if (string.Equals(sourceColumn.ColumnName, "Final Date", StringComparison.OrdinalIgnoreCase))
								//{
								//         bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, "FinalDate");
								//       break;
								//}
								//if (string.Equals(sourceColumn.ColumnName, "Permit Code Description", StringComparison.OrdinalIgnoreCase))
								//{
								//         bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, "PermitCode");
								//       break;
								//}
								//if (string.Equals(sourceColumn.ColumnName, "Structure Description", StringComparison.OrdinalIgnoreCase))
								//{
								//         bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, "PermitDesc");
								//       break;
								//}


								//if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], StringComparison.OrdinalIgnoreCase))
								//{
								//         bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, (string)row["COLUMN_NAME"]);
								//       break;
								//}
							}
						}

						bulkCopy.WriteToServer(dt);
					}
				}

				return RedirectToAction("Index", "Permit");
			}

			return RedirectToAction("Index", "Upload");
		}

		[HttpGet]
		[Route("Import")]
		public string Import()
		{
			string sWebRootFolder = _env.WebRootPath + "\\uploads";
			string sFileName = @"Permits Finaled July 2017.xlsx";
			FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

			try
			{
				using (ExcelPackage package = new ExcelPackage(file))
				{
					StringBuilder sb = new StringBuilder();
					ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
					int rowCount = worksheet.Dimension.Rows;
					int ColCount = worksheet.Dimension.Columns;
					bool bHeaderRow = true;
					for (int row = 1; row <= rowCount; row++)
					{
						for (int col = 1; col <= ColCount; col++)
						{
							if (bHeaderRow)
							{
								sb.Append(worksheet.Cells[row, col].Value.ToString() + "\t");
							}
							else
							{
								sb.Append(worksheet.Cells[row, col].Value.ToString() + "\t");
							}
						}
						sb.Append(Environment.NewLine);
					}
					return sb.ToString();
				}
			}
			catch (Exception ex)
			{
				return "Some error occured while importing." + ex.Message;
			}
		}

		public async Task<IActionResult> Download(string filename)
		{
			if (filename == null)
				return Content("filename not present");

			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filename);

			var memory = new MemoryStream();
			using (var stream = new FileStream(path, FileMode.Open))
			{
				await stream.CopyToAsync(memory);
			}
			memory.Position = 0;
			return File(memory, FileHelper.GetContentType(path), Path.GetFileName(path));
		}
	}
}
