using MyBlog.Models.Tegs;
using MyBlog.ViewModels.Tegs;

namespace MyBlog.Services.ControllerServices.Interface;

public interface ITegService
{
    Task<TegViewModel> GetModelIndex();

    Task<TegUpdateViewModel> UpdateTeg(Guid id);

    Task UpdateTeg(TegUpdateViewModel model);

    Task CreateTeg(AddTegViewModel model);

    Task DeleteTeg(Guid id);

    List<Teg> GetAllTeg();

    Teg GetTegId(Guid id);
}
