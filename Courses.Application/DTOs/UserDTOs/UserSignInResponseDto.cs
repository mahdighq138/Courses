using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Application.DTOs.UserDTOs
{
    public class UserSignInResponseDto
    {
        public int UserId { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }

        public bool IsActive { get; set; }

    }
}
