using AutoMapper;
using MyBlog.Models;
using MyBlog.Models.Users;
using MyBlog.ViewModels;
using MyBlog.ViewModels.Users;
using MyBlog.Extentions;
using MyBlog.Models.Articles;
using MyBlog.ViewModels.Devises;
using MyBlog.ViewModels.Articles;

namespace MyBlog;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterViewModel, User>()
            .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
            .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));

        CreateMap<InviteViewModel, Invate>()
            .ForMember(x => x.CodeInvite, opt => opt.MapFrom(c => c.Invite.ConvertMD5()));

        CreateMap<ArticleViewModel, Article>().
           ForSourceMember(x => x.Comments, opt => opt.DoNotValidate());

        CreateMap<CommentViewModel, Comment>();
        CreateMap<TegViewModel, Teg>();
    }
}