using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Application.DTOs.UserDTOs
{
    public class UserRegisterRequestDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string ConfirmPassword { get; set; }

        public bool AgreeToTerms { get; set; }

    }
}
