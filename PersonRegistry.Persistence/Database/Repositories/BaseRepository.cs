using Microsoft.EntityFrameworkCore;
using PersonRegistry.Domain.Entities;

namespace PersonRegistry.Persistence.Database.Repositories;

public class BaseRepository<TEntity> where TEntity : class
{
    protected readonly PersonRegistryDbContext context;
    protected readonly DbSet<TEntity> dbSet;

    public BaseRepository(PersonRegistryDbContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }
    
    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await this.dbSet.AddAsync(entity, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        this.dbSet.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        this.dbSet.Remove(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}