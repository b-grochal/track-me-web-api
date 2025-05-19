using Domain.Common;

namespace Application.Common.Data;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task<TEntity?> GetByIdAsync(int id);

    Task<IEnumerable<TEntity>> GetAllAsync();
}
