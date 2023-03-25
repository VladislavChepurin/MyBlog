﻿using Microsoft.AspNetCore.Identity;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
using MyBlog.Models.Users;
using MyBlog.Services.ContextServices.Interface;
using MyBlog.Services.ControllerServices.Interface;
using MyBlog.ViewModels;
using MyBlog.ViewModels.Users;

namespace MyBlog.Services.ControllerServices
{
    public class AccountService : IAccountService
    {

        private readonly IUserResolverService _userResolverService;
        private readonly ISingInResolverService _singInResolverService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUserResolverService userResolverService, IUnitOfWork unitOfWork, ISingInResolverService singInResolverService)
        {
            _userResolverService = userResolverService;
            _unitOfWork = unitOfWork;
            _singInResolverService = singInResolverService;
        }

        public Task<LoginViewModel> GetLoginModel()
        {
            throw new NotImplementedException();
        }

        public async Task<UserPageViewModel> GetUserPageModel()
        {
            var user = await _userResolverService.GetUser();
            var model = new UserPageViewModel
            {
                UserViewModel = new UserViewModel(user!)
            };
            model.UserViewModel.AllArticles = GetUserArticles(user!);
            model.UserViewModel.AllComments = GetUserComments(user!);
            return model;
        }

        public async Task Logout() =>    
            await _singInResolverService.LogoutAction();

        public async Task<bool> IsLoggedIn(LoginViewModel model) =>
            await _singInResolverService.IsLoggedInAction(model);

        private List<Comment> GetUserComments(User user)
        {
            if (_unitOfWork.GetRepository<Comment>() is CommentRepository repository)
            {
                return repository.GetCommentByUser(user);
            }
            return new List<Comment>();
        }

        private List<Article> GetUserArticles(User user)
        {
            if (_unitOfWork.GetRepository<Article>() is ArticleRepository repository)
            {
                return repository.GetArticleByUser(user);
            }
            return new List<Article>();
        }

      
    }
}
