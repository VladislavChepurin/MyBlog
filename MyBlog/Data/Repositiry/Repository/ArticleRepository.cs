using MyBlog.Data.Repositiry;
using MyBlog.Models.Articles;
using MyBlog.Models.Users;

namespace MyBlog.Data.Repository
{
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
            throw new NotImplementedException();
        }
        public void DeleteArticle(Article article)
        {
            Delete(article);
        }

        public List<Article> getArticleByUser(User target)
        {
            var articles = Set.AsEnumerable().Where(x => x?.User?.Id == target.Id);
            return articles.ToList();
        }

        public List<Article> getArticleById(int id)
        {
            var articles = Set.AsEnumerable().Where(x => x?.Id == id);
            return articles.ToList();
        }     
           
        public List<Article> GetAllArticle()
        {
            var articles = Set.AsEnumerable().Select(x => x);
            return articles.ToList();
        }
    }
}
 