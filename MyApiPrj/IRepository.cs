public interface IRepository<TEntity>
{
    TEntity GetById(int id);
    IEnumerable<TEntity> GetAll();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void delete(TEntity entity);
}

//https://readmedium.com/power-of-net-core-a-guide-to-repository-and-unit-of-work-patterns-3c0fbb49b610