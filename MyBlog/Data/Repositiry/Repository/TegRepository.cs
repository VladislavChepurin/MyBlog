using MyBlog.Data.Repositiry;
using MyBlog.Models.Articles;

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

    public List<Teg> GetTegById(Guid id)
    {
        var articles = Set.AsEnumerable().Where(x => x?.Id == id);
        return articles.ToList();
    }


    public List<Teg> GetAllTeg()
    {
        var articles = Set.AsEnumerable().Select(x => x);
        return articles.ToList();
    }
}
