using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.WebApp.ViewModels.AccountViewModels
{
    public class SignInViewModel
    {
        [MaxLength(100, ErrorMessage = "Email / UserName Too Long")]
        public required string UserNameOrEmail { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
