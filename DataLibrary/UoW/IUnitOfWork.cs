using DataLibrary.Data.Repositiry;

namespace DataLibrary.Data.UoW;

public interface IUnitOfWork : IDisposable
{
    //DeviceRepository Device { get; }

    void SaveChanges();
    IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class;
}
