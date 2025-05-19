using Domain.Common;
using Microsoft.EntityFrameworkCore;
using TrackMe.Database.Context;

namespace Infrastructure.Common;

internal abstract class Repository<TEntity>
    where TEntity : Entity
{
    private readonly ApplicationDbContext _dbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }
}
