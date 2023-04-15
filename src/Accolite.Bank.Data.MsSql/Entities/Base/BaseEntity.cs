using Accolite.Bank.Data.MsSql.Interfaces;

namespace Accolite.Bank.Data.MsSql.Entities.Base;

public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
