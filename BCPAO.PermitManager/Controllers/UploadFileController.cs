using BCPAO.PermitManager.Extensions;
using BCPAO.PermitManager.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BCPAO.PermitManager.Controllers
{
	public class UploadFileController : Controller
	{
		private readonly IHostingEnvironment _env;

		public UploadFileController(IHostingEnvironment env)
		{
			_env = env;
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
				}

				//FileDetails fileDetails;
				using (var reader = new StreamReader(file.OpenReadStream()))
				{
					var fileContents = await reader.ReadToEndAsync();
					var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
					//fileDetails = new FileDetails
					//{
					//	Filename = parsedContentDisposition.FileName,
					//	Content = fileContent
					//};
				}

				//var excel = new ExcelPackage(fileContents);

				// TODO: Parse Excel file and save to database

				Import();

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