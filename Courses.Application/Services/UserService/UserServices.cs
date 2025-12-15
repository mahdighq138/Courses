using AutoMapper;
using Courses.Application.DTOs.UserDTOs;
using Courses.Application.Interfaces.RepositoryInterfaces;
using Courses.Application.Interfaces.SecurityInterfaces;



//using Courses.Application.DTOs.AccountViewModels;
using Courses.Application.Services.ServiceInterfaces;
using Courses.Domain.Convertors;
using Courses.Domain.Entities.User;
using Courses.Domain.Generator;
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


        public async Task<int> RegisterUserAsync(UserRegisterRequestDto registerRequest)
        {
            registerRequest.Email = FixText.FixEmail(registerRequest.Email);

            var userEntity = _mapper.Map<User>(registerRequest);
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

        public async Task<UserSignInResponseDto> SignInUserAsync(UserSignInRequestDto signInRequest, bool isEmail = false, bool isUserName = false)
        {
            User user;
            if (isEmail)
            {
                string email = FixText.FixEmail(signInRequest.UserNameOrEmail);
                user = await _userRepository.FindUserByEmailAsync(email);
            }
            else
            {
                user = await _userRepository.FindUserByUserNameAync(signInRequest.UserNameOrEmail);
            }
            bool correctPass = _hasher.VerifyPassword(user.Password, signInRequest.Password);
            if (correctPass)
            {
                // map to response
                var signInResponse = _mapper.Map<UserSignInResponseDto>(user);
                return signInResponse;
            }
            return null;

        }

        public async Task<bool> ActivateAccountAsync(string activationCode)
        {
            var user = await _userRepository.WhoseActivationCodeIsThisAsync(activationCode);
            if (user == null)
            {
                return false;
            }

            if (await _userRepository.ActivateUserAsync(user))
            {
                string newActivationCode = CodeNameGenerator.GenerateUniqueCode();
                if (await _userRepository.ChangeActivationCodeAsync(user, newActivationCode))
                {
                    await _userRepository.CommitChangesAsync();
                    return true;
                }
            }
            return false;

        }


    }
}
