using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Courses.Application.DTOs.UserDTOs;
using Courses.WebApp.ViewModels.AccountViewModels;

namespace Courses.WebApp.Mappers.UserMapper
{
    public class UserViewModelMappingProfile : Profile
    {
        public UserViewModelMappingProfile()
        {
            CreateMap<SignUpViewModel, UserRegisterRequestDto>().ReverseMap();
            CreateMap<SignInViewModel, UserSignInRequestDto>().ReverseMap();
        }
    }
}
