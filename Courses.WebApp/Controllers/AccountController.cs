using Microsoft.AspNetCore.Mvc;

namespace Courses.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }
    }
}
