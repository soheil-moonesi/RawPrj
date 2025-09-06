
using Microsoft.EntityFrameworkCore;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    void SaveChanges();


}

