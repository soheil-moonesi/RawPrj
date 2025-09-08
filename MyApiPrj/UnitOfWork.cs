using Microsoft.EntityFrameworkCore;

public class UnitofWork : IUnitOfWork
{
    //An error occurred while accessing the Microsoft.Extensions.Hosting services. 
    // Continuing without the application service provider.
    //  Error: Some services are not able to be constructed (Error while validating the service descriptor 
    // 'ServiceType: IUnitOfWork Lifetime: Scoped ImplementationType: UnitofWork':
    //  Unable to resolve service for type 'Microsoft.EntityFrameworkCore.DbContext' while attempting to activate 'UnitofWork'.)
    //Unable to create a 'DbContext' of type 'RuntimeType'. 
    //The exception 'Unable to resolve service for type 'Microsoft.EntityFrameworkCore.DbContextOptions`1[ApplicationDbContext]' 
    // while attempting to activate 'ApplicationDbContext'.' was thrown while attempting to create an instance.
    //  For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728

//this error above is resolve when i change Dbcontext to ApplicationDbContext
    private readonly ApplicationDbContext _context;
    private readonly Dictionary<Type, object> _repositories;

    //todo: explain code
    public UnitofWork(ApplicationDbContext context)
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