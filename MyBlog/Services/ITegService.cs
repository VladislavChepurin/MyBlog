using MyBlog.Models.Tegs;
using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Tegs;

namespace MyBlog.Services
{
    public interface ITegService
    {
        TegViewModel GetModelIndex();

        TegUpdateViewModel UpdateTeg(Guid id);

        void UpdateTeg(TegUpdateViewModel model);

        void CreateTeg(AddTegViewModel model);

        void DeleteTeg(Guid id);

        List<Teg> GetAllTeg();

        Teg GetTegId(Guid id);
    }

}
