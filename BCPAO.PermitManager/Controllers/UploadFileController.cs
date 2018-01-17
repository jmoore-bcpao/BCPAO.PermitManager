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

		[HttpPost("Titusville")]
		public async Task<IActionResult> Titusville(IFormFile file)
		{
			var connString = _config.GetConnectionString("DefaultConnection");

			if (file == null || file.Length == 0)
				return Content("file not selected");

			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/Titusville", file.FileName);
			//var path = Path.Combine(_env.WebRootPath, "uploads");

			if (Path.GetExtension(file.FileName).Equals(".xlsx"))
			{
				using (var fileStream = new FileStream(path, FileMode.Create))
				{
					#region FileStream
					await file.CopyToAsync(fileStream);

					var excelPackage = new ExcelPackage(fileStream);

					var dt = excelPackage.ToDataTable();
					
					var schema = "bcpao";
					var table = "Permits";
					
					using (var conn = new SqlConnection(connString))
					{
						#region SqlConnection
						conn.Open();

						using (var bulkCopy = new SqlBulkCopy(conn))
						{
							bulkCopy.DestinationTableName = $"{schema}.{table}";
							bulkCopy.BulkCopyTimeout = 0;
							bulkCopy.BatchSize = 500;
							
							String[] columnRestrictions = new String[4];

							columnRestrictions[0] = null;   // 0-member represents Catalog
							columnRestrictions[1] = schema; // 1-member represents Schema
							columnRestrictions[2] = table;  // 2-member represents Table Name
							columnRestrictions[3] = null;   // 3-member represents Column Name

							DataTable schemaTable = conn.GetSchema("Columns", columnRestrictions);

							foreach (DataColumn source in dt.Columns)
							{
								foreach (DataRow row in schemaTable.Rows)
								{
									#region DataRow

									var sourceColumn = source.ColumnName;

									// Property ID
									if (string.Equals(sourceColumn, "Tax Account No", StringComparison.OrdinalIgnoreCase))
									{
										bulkCopy.ColumnMappings.Add(sourceColumn, "PropertyId");
										break;
									}

									// Parcel ID
									if (string.Equals(sourceColumn, "Parcel ID", StringComparison.OrdinalIgnoreCase))
									{
										bulkCopy.ColumnMappings.Add(sourceColumn, "ParcelId");
										break;
									}

									// Permit Number
									if (string.Equals(sourceColumn, "Permit Number", StringComparison.OrdinalIgnoreCase))
									{
										bulkCopy.ColumnMappings.Add(sourceColumn, "PermitNumber");
										break;
									}

									// Permit Status
									if (string.Equals(sourceColumn, "Permit Status", StringComparison.OrdinalIgnoreCase))
									{
										bulkCopy.ColumnMappings.Add(sourceColumn, "PermitStatus");
										break;
									}

									// Issue Date 
									if (string.Equals(sourceColumn, "Issue Date ", StringComparison.OrdinalIgnoreCase))
									{
										bulkCopy.ColumnMappings.Add(sourceColumn, "IssueDate");
										break;
									}

									// Parcel Value
									if (string.Equals(sourceColumn, "Issue Amount", StringComparison.OrdinalIgnoreCase))
									{
										bulkCopy.ColumnMappings.Add(sourceColumn, "PermitValue");
										break;
									}

									// Final Date
									if (string.Equals(sourceColumn, "Final Date", StringComparison.OrdinalIgnoreCase))
									{
										bulkCopy.ColumnMappings.Add(sourceColumn, "FinalDate");
										break;
									}

									// Parcel Code
									if (string.Equals(sourceColumn, "Permit Code Description", StringComparison.OrdinalIgnoreCase))
									{
										bulkCopy.ColumnMappings.Add(sourceColumn, "PermitCode");
										break;
									}

									// Parcel Description
									if (string.Equals(sourceColumn, "Structure Description", StringComparison.OrdinalIgnoreCase))
									{
										bulkCopy.ColumnMappings.Add(sourceColumn, "PermitDesc");
										break;
									}
									#endregion
								}
							}

							bulkCopy.WriteToServer(dt);
						};
						#endregion
					};
					#endregion
				};

				return RedirectToAction("Index", "Permits");
			}

			return RedirectToAction("Index", "Upload");
		}

		[HttpPost("Melbourne")]
		public async Task<IActionResult> Melbourne(IFormFile file)
		{
			var connString = _config.GetConnectionString("DefaultConnection");

			if (file == null || file.Length == 0)
				return Content("file not selected");

			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/Melbourne", file.FileName);

			if (Path.GetExtension(file.FileName).Equals(".txt"))
			{
				using (var fileStream = new FileStream(path, FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}
				
				var schema = "bcpao";
				var table = "Permits";
				
				using (var conn = new SqlConnection(connString))
				{
					conn.Open();

					using (var bulkCopy = new SqlBulkCopy(conn))
					{
						bulkCopy.DestinationTableName = $"{schema}.{table}";
						bulkCopy.BulkCopyTimeout = 0;
						bulkCopy.BatchSize = 500;

						String[] columnRestrictions = new String[4];

						columnRestrictions[0] = null;   // 0-member represents Catalog
						columnRestrictions[1] = schema; // 1-member represents Schema
						columnRestrictions[2] = table;  // 2-member represents Table Name
						columnRestrictions[3] = null;   // 3-member represents Column Name

						DataTable schemaTable = conn.GetSchema("Columns", columnRestrictions);

						var propertyIDList = new List<string>();
						propertyIDList.Add("Property ID");
						propertyIDList.Add("Account");
						propertyIDList.Add("Renum");
						propertyIDList.Add("Renumber");
						propertyIDList.Add("Tax Account");
						propertyIDList.Add("Tax Account No");


						DataTable dt = TextFileHelper.ConvertToDataTable(path);

						foreach (DataColumn source in dt.Columns)
						{
							foreach (DataRow row in schemaTable.Rows)
							{
								#region DataRow

								var sourceColumn = source.ColumnName;

								// Property ID
								if (propertyIDList.Contains(sourceColumn))
								{
									bulkCopy.ColumnMappings.Add(sourceColumn, "PropertyId");
									break;
								}

								// Parcel ID
								if (string.Equals(sourceColumn, "Parcel ID", StringComparison.OrdinalIgnoreCase))
								{
									bulkCopy.ColumnMappings.Add(sourceColumn, "ParcelId");
									break;
								}

								// Permit Number
								if (string.Equals(sourceColumn, "Permit Number", StringComparison.OrdinalIgnoreCase))
								{
									bulkCopy.ColumnMappings.Add(sourceColumn, "PermitNumber");
									break;
								}

								// Permit Status
								if (string.Equals(sourceColumn, "Permit Status", StringComparison.OrdinalIgnoreCase))
								{
									bulkCopy.ColumnMappings.Add(sourceColumn, "PermitStatus");
									break;
								}

								// Issue Date 
								if (string.Equals(sourceColumn, "Issue Date ", StringComparison.OrdinalIgnoreCase))
								{
									bulkCopy.ColumnMappings.Add(sourceColumn, "IssueDate");
									break;
								}

								// Parcel Value
								if (string.Equals(sourceColumn, "Issue Amount", StringComparison.OrdinalIgnoreCase))
								{
									bulkCopy.ColumnMappings.Add(sourceColumn, "PermitValue");
									break;
								}

								// Final Date
								if (string.Equals(sourceColumn, "Final Date", StringComparison.OrdinalIgnoreCase))
								{
									bulkCopy.ColumnMappings.Add(sourceColumn, "FinalDate");
									break;
								}

								// Parcel Code
								if (string.Equals(sourceColumn, "Permit Code Description", StringComparison.OrdinalIgnoreCase))
								{
									bulkCopy.ColumnMappings.Add(sourceColumn, "PermitCode");
									break;
								}

								// Parcel Description
								if (string.Equals(sourceColumn, "Structure Description", StringComparison.OrdinalIgnoreCase))
								{
									bulkCopy.ColumnMappings.Add(sourceColumn, "PermitDesc");
									break;
								}
								#endregion
							}
						}

						bulkCopy.WriteToServer(dt);
					}
				}
			}

			return RedirectToAction("Index", "Permits");
		}
	}
}
