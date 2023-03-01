﻿using AutoMapper;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Users;

namespace MyBlog
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()               
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
        }
    }
}
