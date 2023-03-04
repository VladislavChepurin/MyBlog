using Microsoft.EntityFrameworkCore;

namespace MyBlog.Data.Repositiry;

public class Repository<T> : IRepository<T> where T : class
{
    protected DbContext _db;

    public DbSet<T> Set
    {
        get;
        private set;
    }

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        var set = _db.Set<T>();
        set.Load();

        Set = set;
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
