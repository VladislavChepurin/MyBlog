using MyBlog.Data.Repositiry;
using MyBlog.Models.Articles;
using MyBlog.Models.Users;

namespace MyBlog.Data.Repository
{
    public class ArticleRepository : Repository<Article>
    {
        public ArticleRepository(ApplicationDbContext db) : base(db)
        {
            throw new NotImplementedException();
        }

        public void CreateArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public void UpdateArticle(Article article)         
        {

            throw new NotImplementedException();
        }
        public void DeleteArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public List<Article> getArticleByUser(User target)
        {
            var articles = Set.AsEnumerable().Where(x => x?.User?.Id == target.Id);
            return articles.ToList();
        }
    
        public List <Article> GetAllArticle()
        {
            var articles = Set.AsEnumerable();
            return articles.ToList();
        }
    }
}
 