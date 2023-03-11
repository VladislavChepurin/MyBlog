using MyBlog.Data.Repositiry;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;

namespace MyBlog.Data.Repository;

public class CommentRepository : Repository<Comment>
{
    public CommentRepository(ApplicationDbContext db) : base(db)
    {
        
    }

    public void CreateComment(Comment comment)
    {
        Create(comment);
    }

    public void UpdateComment(Comment comment)
    {
        Update(comment);
    }

    public void DeleteComment(Comment comment)
    {
        Delete(comment);
    }
    public List<Comment> GetCommentByArticle(Article article)
    {

        var articles = Set.AsEnumerable().Where(x => x?.ArticleId == article.Id);
        return articles.ToList();
    }

    public Comment GetCommentById(Guid id)
    {
        var articles = Set.AsEnumerable().Where(x => x?.Id == id).FirstOrDefault();
        return articles;
    }
}
