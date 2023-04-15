using Accolite.Bank.Data.MsSql.DbContext;
using Accolite.Bank.Data.MsSql.Interfaces;
using Accolite.Bank.Data.MsSql.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Accolite.Bank.Data.MsSql.Repositories.Repositories.Base;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
{
    protected BaseRepository(AccoliteBankContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    protected AccoliteBankContext DbContext;
    protected DbSet<TEntity> DbSet { get; }

    public async Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken ct = default)
    {
        filter ??= _ => true;

        return await DbSet.AsNoTracking().Where(filter).ToListAsync(ct);
    }

    public Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>>? filter, CancellationToken ct = default)
    {
        filter ??= _ => true;

        return DbSet.AsNoTracking().FirstOrDefaultAsync(filter, ct);
    }

    public Task<TEntity?> FindByIdAsync(int id, CancellationToken ct = default)
    {
        return DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken ct = default)
    {
        entity.Updated = DateTime.UtcNow;
        entity.Created = DateTime.UtcNow;

        await DbSet.AddAsync(entity, ct);
        await DbContext.SaveChangesAsync(ct);

        return entity;
    }

    public async Task<IEnumerable<TEntity>> InsertManyAsync(IReadOnlyCollection<TEntity> entities, CancellationToken ct = default)
    {
        foreach (var entity in entities)
        {
            entity.Updated = DateTime.UtcNow;
            entity.Created = DateTime.UtcNow;
        }

        await DbSet.AddRangeAsync(entities, ct);
        await DbContext.SaveChangesAsync(ct);

        return entities;
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        var existingEntity = await FindByIdAsync(entity.Id, ct);

        if (existingEntity == null || existingEntity.Id != entity.Id)
        {
            return null;
        }

        entity.Created = existingEntity.Created;
        entity.Updated = DateTime.UtcNow;

        DbSet.Update(entity);
        await DbContext.SaveChangesAsync(ct);

        return entity;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entityToRemove = await FindByIdAsync(id, ct);
        if (entityToRemove != null)
        {
            await DeleteAsync(entityToRemove, ct);
        }
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
    {
        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync(ct);
    }
}
