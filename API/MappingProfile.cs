using AutoMapper;
using Contracts.ApiModels.Article;
using Contracts.Models.Articles;
using Contracts.Models.Users;
using Contracts.ViewModels.Articles;
using Contracts.ViewModels.Users;

namespace MyBlog;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterViewModel, User>()
            .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
            .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));

        CreateMap<AddArticleViewModel, Article>();

        CreateMap<CreateArticleApi, Article>()
            .ForMember(x => x.Tegs, opt => opt.Ignore()); ;
    }
}