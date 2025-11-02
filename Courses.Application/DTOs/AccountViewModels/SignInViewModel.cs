using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Application.DTOs.AccountViewModels
{
    public class SignInViewModel
    {
        public required string UserNameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
