using Microsoft.AspNetCore.Mvc;

namespace MyUniversity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Index.cshtml");
        }
    }
}
