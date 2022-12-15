using Microsoft.AspNetCore.Mvc;
using PrismaPicker.Models;

namespace PrismaPicker.Controllers
{
	public class HomeController : Controller
	{
		// GET method displays /Home/Index View
		[HttpGet]
		public IActionResult Index()
		{
			ViewBag.Title = "PrismaPicker | Glass Colors Made Simple";
			return View();
		}

		// GET method displays /Home/Error View
		[HttpGet]
		public IActionResult Error()
        {
			ViewBag.Title = "Oops!";
			return View();
        }
	}
}
