using MyBlog.Models.Articles;
using System.Data.Common;

namespace MyBlog.Data.Repositiry.Repository
{
    public class ArticleTegRepository : Repository<ArticleTeg>
    {
        public ArticleTegRepository(ApplicationDbContext db) : base(db)
        {
        }

        public void AddTegInArticles (Article article, Teg teg)
        {
            var model = new ArticleTeg() { ArticlesId = article.Id, TegsId = teg.Id };
            Create(model);            
        }

        public void DeleteTegInArticles(Article article, Teg teg)
        {
            var model = new ArticleTeg() { ArticlesId = article.Id, TegsId = teg.Id };
            Delete(model);
        }
    }
}
