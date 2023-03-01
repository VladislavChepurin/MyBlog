using MyBlog.Data.Repositiry;
using MyBlog.Models;
using MyBlog.Models.Articles;

namespace MyBlog.Data.Repository
{
    public class TegRepository: Repository<Teg>
    {
        public TegRepository(ApplicationDbContext db) : base(db)
        {
                
        }
    }





}
