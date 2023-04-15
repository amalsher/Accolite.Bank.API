using Accolite.Bank.Data.MsSql.Entities.Base;

namespace Accolite.Bank.Data.MsSql.Entities;

public partial class UserEntity : BaseEntity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

#pragma warning disable CA2227 // Collection properties should be read only
    public virtual ICollection<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();
#pragma warning restore CA2227 // Collection properties should be read only
}
