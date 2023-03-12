using Microsoft.EntityFrameworkCore;
using MyBlog.Models.Articles;

namespace MyBlog.Data.Repositiry;

public class Repository<T> : IRepository<T> where T : class
{
    protected DbContext _context;

    public DbSet<Article>? Articles { get; set; }

    public DbSet<T> Set
    {
        get;
        private set;
    }

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        var set = _context.Set<T>();
        set.Load();
        Set = set;

        var articles = context.Articles;
        articles?.Load();
        Articles = articles;
    }

    public void Create(T item)
    {
        Set.Add(item);
    }

    public void Delete(T item)
    {
        Set.Remove(item);
    }

    public T Get(int id)
    {
        return Set.Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return Set;
    }

    public void Update(T item)
    {
        Set.Update(item);
    }
}
