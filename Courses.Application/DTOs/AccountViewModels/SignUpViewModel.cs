using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Application.DTOs.AccountViewModels
{
    public class SignUpViewModel
    {
        [MaxLength(50, ErrorMessage = "First Name Too Long")]
        public string? FirstName { get; set; }


        [MaxLength(50, ErrorMessage = "Last Name Too Long")]
        public string? LastName { get; set; }


        [MaxLength(50, ErrorMessage = "User Name Too Long")]
        public required string UserName { get; set; }


        [MaxLength(100, ErrorMessage ="Email Address Too Long")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }


        public required string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }
    }
}
