using Courses.Application.DTOs.UserDTOs;
using Courses.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Application.Services.ServiceInterfaces
{
    public interface IUserServices
    {
        Task<int> RegisterUserAsync(UserRegisterRequestDto registerRequest);
        Task<UserSignInResponseDto> SignInUserAsync(UserSignInRequestDto signInRequest, bool isEmail, bool isUserName);
        public Task<bool> EmailExistsAsync(string email);
        public Task<bool> UserNameExistsAsync(string username);
        public Task<bool> ActivateAccountAsync(string activationCode);
    }
}
