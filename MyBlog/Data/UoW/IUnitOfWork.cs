using MyBlog.Data.Repositiry;

namespace MyBlog.Data.UoW;

public interface IUnitOfWork : IDisposable
{
    //DeviceRepository Device { get; }

    void SaveChanges();
    IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class;
}
