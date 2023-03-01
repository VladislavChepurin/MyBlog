using MyBlog.Data.Repositiry;
using MyBlog.Models.Articles;

namespace MyBlog.Data.Repository
{
    public class CommentRepository : Repository<Comment>
    {
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            
        }

        public void CreateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void GetCommentByArticle(Article article)
        {
            throw new NotImplementedException();
        }



    }
}
