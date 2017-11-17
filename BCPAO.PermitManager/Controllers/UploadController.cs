using Microsoft.AspNetCore.Mvc;

namespace BCPAO.PermitManager.Controllers
{
	public class UploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}