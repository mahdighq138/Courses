using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Courses.Application.DTOs.AccountViewModels;
using Courses.Domain.Entities.User;

namespace Courses.Application.Mappers.UserMapper
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile()
        {
            CreateMap<SignUpViewModel, User>()
            // These two don’t exist in User, so tell AutoMapper to ignore them
            .ForSourceMember(src => src.ConfirmPassword, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.AgreeToTerms, opt => opt.DoNotValidate())

            // These three exist in User but not in SignUpViewModel
            .ForMember(dest => dest.ActivationCode, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore());
        }
    }
}
