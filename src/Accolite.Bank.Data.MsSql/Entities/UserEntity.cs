using Accolite.Bank.Data.MsSql.Entities.Base;

namespace Accolite.Bank.Data.MsSql.Entities;

public partial class UserEntity : BaseEntity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();
}
