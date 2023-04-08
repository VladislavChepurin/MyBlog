using Contracts.Models.Tegs;
using BissnesLibrary.ContextServices.Interface;
using BissnesLibrary.ControllerServices.Interface;
using Contracts.ViewModels.Tegs;
using NLog;
using DataLibrary.Data.UoW;
using DataLibrary.Data.Repository;

namespace BissnesLibrary.ControllerServices;

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
        _logger = LogManager.Setup()/*.LoadConfigurationFromAppSettings()*/.GetCurrentClassLogger();
        _userResolverService = userResolverService;
    }

    public async Task CreateTeg(string content)
    {
        TegRepository?.CreateTeg(
               new Teg(content));
        _unitOfWork.SaveChanges();
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} создал тег {Teg}", currentUser?.Email, content);
    }

    public async Task DeleteTeg(Guid id)
    {
        var teg = TegRepository?.GetTegById(id);
        TegRepository?.DeleteTeg(teg!);
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

    public List<Teg> GetTegByArticle(Guid idArticle)
    {
        var tegs = TegRepository?.GetTegByArticle(idArticle);
        if (tegs?.Count != 0)
            return tegs!;
        return null!;
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

    public async Task UpdateTeg(Guid id, string content)
    {
        var teg = TegRepository?.GetTegById(id);
        teg!.Content = content;
        TegRepository?.UpdateTeg(teg);
        _unitOfWork.SaveChanges();
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} изменил тег с индификатором {Id} с новое содержимое тега: {Content}", currentUser?.Email, id, content);
    }
}
