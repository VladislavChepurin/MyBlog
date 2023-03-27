﻿using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Tegs;
using MyBlog.Services.ContextServices.Interface;
using MyBlog.Services.ControllerServices.Interface;
using MyBlog.ViewModels.Tegs;
using NLog;
using NLog.Web;

namespace MyBlog.Services.ControllerServices;

public class TegService : ITegService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TegRepository? TegRepository;
    private readonly Logger _logger;
    private readonly IUserResolverService _userResolverService;

    public TegService(IUnitOfWork unitOfWork, IUserResolverService userResolverService)
    {
        _unitOfWork = unitOfWork;
        TegRepository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        _userResolverService = userResolverService;
    }

    public async Task CreateTeg(AddTegViewModel model)
    {
        TegRepository?.CreateTeg(
               new Teg(model.Content!));
        _unitOfWork.SaveChanges();
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} создал тег {Teg}", currentUser?.Email, model.Content);
    }

    public async Task DeleteTeg(Guid id)
    {
        var teg = TegRepository?.GetTegById(id);
        TegRepository?.DeleteTeg(teg);
        _unitOfWork.SaveChanges();
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} удалил тег с индификатором {Id} с содержимым {Content}", currentUser?.Email, id, teg?.Content);
    }

    public List<Teg> GetAllTeg()
    {
        return TegRepository?.GetAllTeg()!;
    }

    public async Task<TegViewModel> GetModelIndex()
    {
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} открыл страницу со списком тегов", currentUser?.Email);
        var tegs = TegRepository?.GetAllTeg();
        return new TegViewModel(tegs);
    }

    public Teg GetTegId(Guid id)
    {
        return TegRepository?.GetTegById(id)!;
    }

    public async Task<TegUpdateViewModel> UpdateTeg(Guid id)
    {
        var teg = TegRepository?.GetTegById(id);
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} открыл окно редактирования тега с индификатором {Id} с содержимым {Content}", currentUser?.Email, id, teg?.Content);
        return new TegUpdateViewModel(teg!);
    }

    public async Task UpdateTeg(TegUpdateViewModel model)
    {
        var teg = TegRepository?.GetTegById(model.Id);
        teg!.Content = model.Content;
        TegRepository?.UpdateTeg(teg);
        _unitOfWork.SaveChanges();
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} изменил тег с индификатором {Id} с новое содержимое тега: {Content}", currentUser?.Email, model.Id, model?.Content);
    }
}
