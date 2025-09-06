using Microsoft.EntityFrameworkCore;

public class UnitofWork : IUnitOfWork
{

    private readonly DbContext _context;
    private readonly Dictionary<Type, object> _repositories;

    //todo: explain code
    public UnitofWork(DbContext context)
    {
        _context = context;

        // +----------------------+-------------------------------+
        // |         Key          |            Value              |
        // |       (Type)         |           (object)            |
        // +----------------------+-------------------------------+
        // | typeof(Customer)     | [Repository<Customer> instance]|
        // | typeof(Order)        | [Repository<Order> instance]   |
        // | typeof(Product)      | [Repository<Product> instance] |
        // +----------------------+-------------------------------+
        _repositories = new Dictionary<Type, object>();

    }



    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {

        // Think of ContainsKey like checking if a word exists in a physical dictionary:
        // Dictionary: The book itself (_repositories)
        // Key: The word you're looking up (typeof(Customer))
        // ContainsKey: The act of checking if the word exists in the dictionary
        // Value: The definition of the word (the Repository<Customer> instance)
    
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        //it is work like this var customerRepo = new Repository<Customer>(_context);
        var repository = new Repository<TEntity>(_context);

//This adds a new entry to the dictionary where:
// Key: typeof(TEntity) (the Type object representing the entity, e.g., typeof(Customer))
// Value: repository (the newly created Repository<TEntity> instance)
        _repositories.Add(typeof(TEntity), repository);

        return repository;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}