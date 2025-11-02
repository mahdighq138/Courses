using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.EntityConfigs.RoleConfigs
{
    public static class RoleConfiguration
    {
        
        public static void ConfigureRole (this EntityTypeBuilder<Domain.BaseEntities.User.Role> entity)
        {
            entity.HasKey(r => r.RoleId);
            
            entity.HasMany(r=>r.UserRoles).WithOne(ur=>ur.Role).HasForeignKey(ur=>ur.RoleId).
                OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
