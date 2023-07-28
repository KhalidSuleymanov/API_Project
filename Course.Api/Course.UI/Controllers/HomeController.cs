using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Course.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}