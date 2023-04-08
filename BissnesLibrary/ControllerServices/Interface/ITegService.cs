using Contracts.Models.Tegs;
using Contracts.ViewModels.Tegs;

namespace BissnesLibrary.ControllerServices.Interface;

public interface ITegService
{
    Task<TegViewModel> GetModelIndex();

    Task<TegUpdateViewModel> UpdateTeg(Guid id);

    Task UpdateTeg(Guid id, string content);

    Task CreateTeg(string content);

    Task DeleteTeg(Guid id);

    List<Teg> GetAllTeg(); 

    Teg GetTegId(Guid id);

    List<Teg> GetTegByArticle(Guid idArticle);

}
