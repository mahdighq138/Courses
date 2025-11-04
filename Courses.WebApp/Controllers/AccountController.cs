using Courses.Application.Services.ServiceInterfaces;
using Courses.Application.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebApp.Controllers
{
    public class AccountController(IUserServices userServices) : Controller
    {
        private readonly IUserServices _userServices = userServices;

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(Application.DTOs.AccountViewModels.SignUpViewModel signUpViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(signUpViewModel);
            }

            if (await _userServices.EmailExistsAsync(signUpViewModel.Email))
            {
                ModelState.AddModelError("Email", "Email already taken");
                return View(signUpViewModel);
            }

            if (await _userServices.UserNameExistsAsync(signUpViewModel.UserName))
            {
                ModelState.AddModelError("UserName", "UserName already taken");
                return View(signUpViewModel);
            }

            await _userServices.RegisterUserAsync(signUpViewModel);
            return View("AccountCreatedSuccessfully", signUpViewModel);
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
