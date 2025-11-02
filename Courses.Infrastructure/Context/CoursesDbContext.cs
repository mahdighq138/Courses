using Courses.Domain.Entities.User;
using Courses.Infrastructure.EntityConfigs.RoleConfigs;
using Courses.Infrastructure.EntityConfigs.UserConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.Context
{
    public class CoursesDbContext(DbContextOptions<CoursesDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Courses");

            modelBuilder.Entity<User>().ConfigureUser();
            modelBuilder.Entity<Role>().ConfigureRole();
            modelBuilder.Entity<UserRole>().HasKey(ur => ur.UserRoleId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
