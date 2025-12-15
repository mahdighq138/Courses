using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Courses.Application.DTOs.UserDTOs;
using Courses.Domain.Entities.User;

namespace Courses.Application.Mappers.UserMapper
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserSignInResponseDto, User>()
            // These three exist in User but not in SignUpViewModel
            .ForMember(dest => dest.ActivationCode, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore()).ReverseMap();
        }
    }
}
