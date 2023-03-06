﻿using AutoMapper;
using MyBlog.Models.Articles;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Devises;
using MyBlog.ViewModels.Users;

namespace MyBlog;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterViewModel, User>()
            .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
            .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
        
        CreateMap<ArticleViewModel, Article>().
           ForSourceMember(x => x.Comments, opt => opt.DoNotValidate());

        CreateMap<CommentViewModel, Comment>();
        CreateMap<TegViewModel, Teg>();
    }
}