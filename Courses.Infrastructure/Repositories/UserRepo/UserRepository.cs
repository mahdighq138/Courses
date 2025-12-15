using Courses.Domain.Entities.User;
using Courses.Application.Interfaces.RepositoryInterfaces;
using Courses.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.Repositories.UserRepo
{
    public class UserRepository(CoursesDbContext context) : IUserRepository
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

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return (await _context.Users.FirstOrDefaultAsync(u => u.Email == email))!;
        }

        public async Task<User> FindUserByUserNameAync(string userName)
        {
            return (await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName))!;
        }

        public async Task<User> WhoseActivationCodeIsThisAsync(string activationCode)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.ActivationCode == activationCode);
            return user!;
        }

        public async Task<bool> ActivateUserAsync(User user)
        {
            var foundUser = await _context.Users.FindAsync(user.UserId);
            if (foundUser != null)
            {
                foundUser.IsActive = true;
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeActivationCodeAsync(User user, string newActivationCode)
        {
            var foundUser = await _context.Users.FindAsync(user.UserId);
            if (foundUser != null)
            {
                foundUser.ActivationCode = newActivationCode;
                return true;
            }
            return false;
        }

        public async Task<bool> DeActivateUserAsync(User user)
        {
            var foundUser = await _context.Users.FindAsync(user.UserId);
            if (foundUser != null)
            {
                foundUser.IsActive = false;
                return true;
            }
            return false;
        }

        public async Task<bool> CommitChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
