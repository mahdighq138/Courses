using Courses.Application.DTOs.AccountViewModels;
using Courses.Application.Services.ServiceInterfaces;
using Courses.Application.Services.UserService;
using Courses.Domain.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Courses.WebApp.Controllers
{
    public class AccountController(IUserServices userServices) : Controller
    {
        private readonly IUserServices _userServices = userServices;

        #region SignUp


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

            if (signUpViewModel.Email.Contains(signUpViewModel.UserName, StringComparison.InvariantCultureIgnoreCase))
            {
                ModelState.AddModelError("UserName", "UserName cannot be a substring of the Email");
                return View(signUpViewModel);
            }

            if (signUpViewModel.UserName.Contains(signUpViewModel.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                ModelState.AddModelError("Email", "Email cannot be a substring of the Username");
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
        #endregion


        #region SignIn

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(signInViewModel);
            }

            bool isEmail = false, isUserName = false;
            if (await _userServices.EmailExistsAsync(signInViewModel.UserNameOrEmail))
            {
                isEmail = true;
            }
            if (await _userServices.UserNameExistsAsync(signInViewModel.UserNameOrEmail))
            {
                isUserName = true;
            }

            if (!isEmail && !isUserName)
            {
                ModelState.AddModelError("UserNameOrEmail", "No such UserNameOrEmail");
                return View(signInViewModel);
            }

            var user = await _userServices.SignInUserAsync(signInViewModel, isEmail, isUserName);
            if (user == null)
            {
                ModelState.AddModelError("UserNameOrEmail", "No such UserNameOrEmail");
                return View(signInViewModel);
            }

            if (!user.IsActive)
            {
                ModelState.AddModelError("UserNameOrEmail", "You need to activate your account. Check your emails");
                return View(signInViewModel);
            }

            await AuthenticateUser(user, signInViewModel.RememberMe);

            return Redirect("/");
        }

        [HttpGet("ActivateUserAccount/{activationCode}")]
        public async Task<IActionResult> ActivateUserAccount(string activationCode)
        {
            if (await _userServices.ActivateAccountAsync(activationCode))
            {
                ViewData["isActive"] = "Active";
            }

            return View();
        }
        #endregion

        #region Authenticate
        private async Task AuthenticateUser(User user, bool rememberMe)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = rememberMe
            };

            await HttpContext.SignInAsync(principal, properties);
        }
        #endregion

    }
}
