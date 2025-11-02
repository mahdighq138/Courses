using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.EntityConfigs.UserConfigs
{
    public static class RoleConfiguration
    {
        
        public static void ConfigureUser (this EntityTypeBuilder<Domain.BaseEntities.User.User> entity)
        {
            entity.HasKey(u => u.UserId);
            entity.HasIndex(u => u.UserName).IsUnique();
            entity.HasIndex(u => u.Email).IsUnique();

            entity.Property(u=>u.FirstName).HasMaxLength(50);
            entity.Property(u=>u.LastName).HasMaxLength(50);
            entity.Property(u=>u.UserName).HasMaxLength(100);
            entity.Property(u=>u.Email).HasMaxLength(100);

            entity.HasMany(u => u.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId).
                OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
