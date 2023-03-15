using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Repositiry;
using MyBlog.Models.Articles;
using MyBlog.Models.Tegs;

#nullable disable

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

    public void DeleteTegInArticle(Article article, List<Guid> teg)
    {
        foreach (Guid id in teg)
        {
            article?.Tegs?.Remove(GetTegById(id));
        }
    }

    public void UpdateTegsInArticles(Article article, List<Guid> tegsCurrent)
    {
        var idTegsArticle = new List<Guid>();
        foreach (var teg in article.Tegs)
            idTegsArticle.Add(teg.Id);
        var deleteTeg = idTegsArticle.Except(tegsCurrent).ToList();
        var addTeg = tegsCurrent.Except(idTegsArticle).ToList();
        DeleteTegInArticle(article, deleteTeg);
        AddTegInArticles(article, addTeg);
    }

    public List<Teg> GetAllTeg()
    {
        return Tegs.Include(t => t.Articles).ToList();
    }
}
