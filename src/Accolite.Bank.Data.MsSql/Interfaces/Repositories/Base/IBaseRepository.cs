using System.Linq.Expressions;

namespace Accolite.Bank.Data.MsSql.Interfaces.Repositories.Base;

public interface IBaseRepository<TEntity> where TEntity : class, IBaseEntity
{
    Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken ct = default);

    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>>? filter, CancellationToken ct = default);

    Task<TEntity?> FindByIdAsync(int id, CancellationToken ct = default);

    Task<TEntity> InsertAsync(TEntity entity, CancellationToken ct = default);

    Task<IEnumerable<TEntity>> InsertManyAsync(IReadOnlyCollection<TEntity> entities, CancellationToken ct = default);

    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);

    Task DeleteAsync(TEntity entity, CancellationToken ct = default);
}
