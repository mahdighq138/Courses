using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.BaseEntities
{
    public class User
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? ActivationCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }

        #region Relation
        public required virtual List<UserRole> UserRoles { get; set; }
        #endregion


    }
}
