using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Application.Interfaces.SecurityInterfaces;
using Courses.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Courses.Application.Services.Security.PasswordHash
{
    public class IdentityPasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException();
            }
            return _hasher.HashPassword(null, password);

        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(providedPassword))
            {
                throw new ArgumentNullException();
            }
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result != PasswordVerificationResult.Failed;
            
        }
    }
}
