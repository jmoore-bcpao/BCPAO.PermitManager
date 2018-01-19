using BCPAO.PermitManager.Data;
using BCPAO.PermitManager.Data.Entities;
using BCPAO.PermitManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BCPAO.PermitManager.Controllers
{
	public class HomeController : Controller
	{
		//public HomeController(UserManager<User> userManager, IPermitRepository repo) : base(userManager, repo)
		//{
		//}

		public IActionResult Index()
		{
			return View();
		}
		
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
