﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Controllers.Account;
using MyBlog.Data;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Devises;

namespace MyBlog.Controllers.Articles;

[Authorize]
public class CommentController : Controller
{
    private readonly ILogger<RegisterUserController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;


    public CommentController(ILogger<RegisterUserController> logger, IMapper mapper, ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> CreateAsync(CommentViewModel model)
    {
        var comment = _mapper.Map<Comment>(model);
        comment.Id = Guid.NewGuid();
        comment.Created = DateTime.Now;
        comment.User = await _userManager.FindByIdAsync(comment.UserId);
        var repository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        repository?.CreateComment(comment);
        _unitOfWork.SaveChanges();
        return View(comment);
    }


    [HttpPost]
    [Route("Update")]
    public ActionResult Update(CommentViewModel model)
    {
        var comment = _mapper.Map<Comment>(model);
        comment.Updated = DateTime.Now;
        var repository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        repository?.UpdateComment(comment);
        _unitOfWork.SaveChanges();
        return View();
    }

    [HttpPost]
    [Route("Delete")]
    public ActionResult Delete(ArticleViewModel model)
    {
        var comment = _mapper.Map<Comment>(model);
        var repository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        repository?.DeleteComment(comment);
        _unitOfWork.SaveChanges();
        return View();
    }

    [HttpPost]
    [Route("CommentByUser")]
    public ActionResult ArticleByUser(Article article)
    {
        var repository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        var comment = repository?.GetCommentByArticle(article);
        return View(comment);
    }

    [HttpPost]
    [Route("CommentById")]
    public ActionResult ArticleByUserId(Guid id)
    {
        var repository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        var comment = repository?.GetCommentById(id);
        return View(comment);
    }
}
