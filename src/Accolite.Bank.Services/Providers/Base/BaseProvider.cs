using Accolite.Bank.Data.MsSql.Interfaces;
using Accolite.Bank.Data.MsSql.Interfaces.Repositories.Base;
using Accolite.Bank.Services.Interfaces;
using Accolite.Bank.Services.Interfaces.Providers.Base;
using AutoMapper;

namespace Accolite.Bank.Services.Providers.Base;

public class BaseProvider<T, TEntity> : IBaseProvider<T>
    where T : class, IBaseModel
    where TEntity : class, IBaseEntity
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<TEntity> _repository;

    public BaseProvider(IMapper mapper, IBaseRepository<TEntity> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await _repository.FilterByAsync(ct: ct);

        return _mapper.Map<IEnumerable<T>>(entities);
    }

    public virtual async Task<T?> GetOneAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repository.FindByIdAsync(id, ct);
        return entity == null ? null : _mapper.Map<T>(entity);
    }

    public virtual async Task<T> InsertAsync(T model, CancellationToken ct = default)
    {
        var entity = _mapper.Map<TEntity>(model);

        return _mapper.Map<T>(await _repository.InsertAsync(entity, ct));
    }

    public virtual async Task<IEnumerable<T>> InsertManyAsync(IReadOnlyCollection<T> models, CancellationToken ct = default)
    {
        var entities = _mapper.Map<IReadOnlyCollection<TEntity>>(models);

        return _mapper.Map<IEnumerable<T>>(await _repository.InsertManyAsync(entities, ct));
    }

    public virtual async Task<T?> UpdateAsync(T model, CancellationToken ct = default)
    {
        var entity = _mapper.Map<TEntity>(model);

        return _mapper.Map<T>(await _repository.UpdateAsync(entity, ct));
    }

    public virtual Task DeleteAsync(int id, CancellationToken ct = default)
    {
        return _repository.DeleteAsync(id, ct);
    }
}
