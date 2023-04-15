using Accolite.Bank.Data.MsSql.DbContext;
using Accolite.Bank.Data.MsSql.Entities;
using Accolite.Bank.Data.MsSql.Interfaces.Repositories;
using Accolite.Bank.Data.MsSql.Repositories.Repositories.Base;

namespace Accolite.Bank.Data.MsSql.Repositories.Repositories;

public class AccountsRepository : BaseRepository<AccountEntity>, IAccountsRepository
{
    public AccountsRepository(AccoliteBankContext dbContext) : base(dbContext)
    {
    }
}
