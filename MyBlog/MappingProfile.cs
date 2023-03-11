using AutoMapper;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
using MyBlog.Models.Tegs;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Comments;
using MyBlog.ViewModels.Tegs;
using MyBlog.ViewModels.Users;

namespace MyBlog;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterViewModel, User>()
            .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
            .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));

        CreateMap<AddArticleViewModel, Article>();              
        CreateMap<CommentViewModel, Comment>();
        CreateMap<TegViewModel, Teg>();
    }
}