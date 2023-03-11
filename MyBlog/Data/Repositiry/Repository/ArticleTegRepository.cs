using MyBlog.Models;
using MyBlog.Models.Articles;
using MyBlog.Models.Tegs;

namespace MyBlog.Data.Repositiry.Repository
{
    public class ArticleTegRepository : Repository<ArticleTeg>
    {
        public ArticleTegRepository(ApplicationDbContext db) : base(db)
        {
        }

       

        public void DeleteTegInArticles(Article article, List<Guid> teg)
        {
            throw new NotImplementedException();
        }
    }
}
