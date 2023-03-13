using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Repositiry;
using MyBlog.Models.Articles;
using MyBlog.Models.Users;

namespace MyBlog.Data.Repository;

#nullable disable

public class ArticleRepository : Repository<Article>
{

    public ArticleRepository(ApplicationDbContext db) : base(db)
    {
       
    }

    public void CreateArticle(Article article)
    {
        Create(article);
    }

    public void UpdateArticle(Article article)         
    {
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
        return Articles.Include(t => t.Tegs).Include(u => u.User).Include(c => c.Comments).Where(f => f.Id == id).FirstOrDefault(); ;
    }     
       
    public List<Article> GetAllArticle()
    {     
        return Articles.Include(t => t.Tegs).ToList();
    }    
}
