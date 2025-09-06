
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
//_Context: Reference to the shared DbContext (passed from UnitOfWork)
    private readonly DbContext _Context;
//_set: The specific DbSet for the entity type (e.g., DbSet<Customer>)
    private readonly DbSet<TEntity> _set;

    public Repository(DbContext Context)
    {
        _Context = Context;
        //Set<TEntity>() = "Give me the table for [Entity]"
        //Context is whole database
        //├── Set<Customer>() → DbSet<Customer> (Customers table)
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