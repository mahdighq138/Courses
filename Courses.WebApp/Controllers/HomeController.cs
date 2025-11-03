using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
