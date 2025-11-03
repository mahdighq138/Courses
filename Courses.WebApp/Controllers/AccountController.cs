using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(Application.DTOs.AccountViewModels.SignUpViewModel signUpViewModel)
        {
            return Content("Post method of SignUp reached");
        }



        [HttpGet]

        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Application.DTOs.AccountViewModels.SignInViewModel signInViewModel)
        {
            return View();
        }
    }
}
