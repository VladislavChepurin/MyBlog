using Contracts.Models.Tegs;

namespace Contracts.ViewModels.Tegs;

public class TegViewModel
{
    public List<Teg>? Tegs { get; set; }

    public TegViewModel(List<Teg>? tegs)
    {
        Tegs = tegs;
    }
}
