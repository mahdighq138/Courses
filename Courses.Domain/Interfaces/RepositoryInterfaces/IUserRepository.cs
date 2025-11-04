using Courses.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public Task<bool> EmailExistsAsync(string email);
        public Task<bool> UserNameExistsAsync(string username);
        public Task<int> RegisterUserAsync(User user);
        public Task<User> FindUserByEmailAync(string email);
        public Task<User> FindUserByUserNameAync(string userName);
    }
}
