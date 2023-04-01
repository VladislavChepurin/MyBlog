using Microsoft.EntityFrameworkCore;
using DataLibrary.Data.Repositiry;
using Contracts.Models.Articles;
using Contracts.Models.Users;

namespace DataLibrary.Data.Repository;

#nullable disable
public class ArticleRepository : Repository<Article>
{

    public ArticleRepository(ApplicationDbContext db) : base(db)
    {
       
    }

    public void CreateArticle(Article article)
    {
        article.Created = DateTime.Now;
        Create(article);
    }

    public void UpdateArticle(Article article)         
    {
        article.Updated = DateTime.Now;
        Update(article);
    }
    public void DeleteArticle(Article article)
    {
        Delete(article);
    }

    public List<Article> GetArticleByUser(User target)
    {
        var articles = Set.AsEnumerable().Where(x => x?.User?.Id == target.Id);
        return articles.ToList();
    }

    public Article GetArticleById(Guid id)
    {
        var article = Articles.Include(t => t.Tegs).Include(u => u.User).Include(c => c.Comments).ThenInclude(u => u.User).FirstOrDefault(f => f.Id == id);      
        return article;
    }     
       
    public List<Article> GetAllArticle()
    {     
        return Articles.Include(t => t.Tegs).ToList();
    }    
}
