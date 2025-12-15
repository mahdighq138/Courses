using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Application.DTOs.UserDTOs
{
    public class UserSignInRequestDto
    {
        public required string UserNameOrEmail { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
