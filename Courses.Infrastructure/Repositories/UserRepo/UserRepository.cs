using Courses.Domain.Entities.User;
using Courses.Domain.Interfaces.RepositoryInterfaces;
using Courses.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.Repositories.UserRepo
{
    public class UserRepository(CoursesDbContext context): IUserRepository
    {
        private readonly CoursesDbContext _context = context;

        public async Task<bool> EmailExistsAsync(string email)
        {
            bool exists = await _context.Users.AnyAsync(u => u.Email == email);
            return exists;
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            bool exists = await _context.Users.AnyAsync(u => u.UserName == userName);
            return exists;
        }

        public async Task<int> RegisterUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        } 

    }
}
