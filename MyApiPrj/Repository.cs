
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _Context;
    private readonly DbSet<TEntity> _set;

    public Repository(DbContext Context)
    {
        _Context = Context;
        _set = Context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        _set.Add(entity);

    }

    public void delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TEntity> GetAll()
    {
        throw new NotImplementedException();
    }

    public TEntity GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }
}