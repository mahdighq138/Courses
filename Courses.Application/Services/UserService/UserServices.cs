using AutoMapper;
using Courses.Application.DTOs.AccountViewModels;
using Courses.Application.Services.ServiceInterfaces;
using Courses.Domain.Convertors;
using Courses.Domain.Entities.User;
using Courses.Domain.Generator;
using Courses.Domain.Interfaces.RepositoryInterfaces;
using Courses.Domain.Interfaces.SecurityInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Application.Services.UserService
{
    public class UserServices(IMapper mapper, IPasswordHasher hasher, IUserRepository userRepository) : IUserServices
    {
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordHasher _hasher = hasher;
        private readonly IUserRepository _userRepository = userRepository;


        public async Task<int> RegisterUserAsync(SignUpViewModel signUpViewModel)
        {
            signUpViewModel.Email = FixText.FixEmail(signUpViewModel.Email);



            var userEntity = _mapper.Map<User>(signUpViewModel);
            userEntity.RegistrationDate = DateTime.Now;
            userEntity.ActivationCode = CodeNameGenerator.GenerateUniqueCode();
            userEntity.IsActive = false;
            userEntity.Password = _hasher.HashPassword(userEntity.Password);
            return await _userRepository.RegisterUserAsync(userEntity);

        }

        public Task<bool> EmailExistsAsync(string email)
        {
            return _userRepository.EmailExistsAsync(email);
        }


        public Task<bool> UserNameExistsAsync(string username)
        {
            return _userRepository.UserNameExistsAsync(username);
        }

        public async Task<User> SignInUserAsync(SignInViewModel signInViewModel, bool isEmail = false, bool isUserName = false)
        {
            User user;
            if (isEmail)
            {
                string email = FixText.FixEmail(signInViewModel.UserNameOrEmail);
                user = await _userRepository.FindUserByEmailAsync(email);
            }
            else
            {
                user = await _userRepository.FindUserByUserNameAync(signInViewModel.UserNameOrEmail);
            }
            bool correctPass = _hasher.VerifyPassword(user.Password, signInViewModel.Password);
            if (correctPass)
            {
                return user;
            }
            return null;

        }
    }
}
