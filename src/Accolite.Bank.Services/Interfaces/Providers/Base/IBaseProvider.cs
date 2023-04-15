namespace Accolite.Bank.Services.Interfaces.Providers.Base;

public interface IBaseProvider<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);

    Task<T?> GetOneAsync(int id, CancellationToken ct = default);

    Task<T> InsertAsync(T model, CancellationToken ct = default);

    Task<IEnumerable<T>> InsertManyAsync(IReadOnlyCollection<T> models, CancellationToken ct = default);

    Task<T?> UpdateAsync(T model, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);
}
