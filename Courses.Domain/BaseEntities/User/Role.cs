using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.BaseEntities.User
{
    public class Role
    {
        public int RoleId { get; set; }
        public required string RoleTitle { get; set; }

        #region Relation
        public virtual required List<UserRole> UserRoles { get; set; }
        #endregion
    }
}
