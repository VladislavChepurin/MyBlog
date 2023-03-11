using MyBlog.Data.Repositiry;
using MyBlog.Models.Articles;
using MyBlog.Models.Tegs;

namespace MyBlog.Data.Repository;

public class TegRepository: Repository<Teg>
{
    public TegRepository(ApplicationDbContext db) : base(db)
    {
            
    }

    public void CreateTeg(Teg teg)
    {
        Create(teg);
    }

    public void UpdateTeg(Teg teg)
    {
        Update(teg);
    }
    public void DeleteTeg(Teg teg)
    {
        Delete(teg);
    }

    public Teg GetTegById(Guid id)
    {
        var teg = Set.AsEnumerable().Where(x => x?.Id == id).FirstOrDefault();
        return teg;
    }

    public void AddTegInArticles(Article article, List<Guid> teg)
    {
        var tegsCurrent = new List<Teg>();
        foreach (Guid id in teg)
        {
            tegsCurrent.Add(GetTegById(id));
        }
        article?.Tegs?.AddRange(tegsCurrent);
    }

    public List<Teg> GetAllTeg()
    {
        var articles = Set.AsEnumerable().Select(x => x);
        return articles.ToList();
    }
}
