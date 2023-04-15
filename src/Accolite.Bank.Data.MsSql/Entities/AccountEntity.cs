using Accolite.Bank.Data.MsSql.Entities.Base;

namespace Accolite.Bank.Data.MsSql.Entities;

public partial class AccountEntity : BaseEntity
{
    public string Name { get; set; } = null!;

    public int UserId { get; set; }

    public decimal Amount { get; set; }

    public virtual UserEntity User { get; set; } = null!;
}
