using Microsoft.EntityFrameworkCore.Infrastructure;
using MyBlog.Data.Repositiry;

namespace MyBlog.Data.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _appContext;
    private Dictionary<Type, object>? _repositories;

    public UnitOfWork(ApplicationDbContext app)
    {
        _appContext = app;
    }

    public void Dispose()
    {

    }

    public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
    {
        if (_repositories == null)
        {
            _repositories = new Dictionary<Type, object>();
        }

        if (hasCustomRepository)
        {
            var customRepo = _appContext.GetService<IRepository<TEntity>>();
            if (customRepo != null)
            {
                return customRepo;
            }
        }

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<TEntity>(_appContext);
        }

        return (IRepository<TEntity>)_repositories[type];

    }
    public void SaveChanges()
    {
        _appContext.SaveChanges();
    }
}
