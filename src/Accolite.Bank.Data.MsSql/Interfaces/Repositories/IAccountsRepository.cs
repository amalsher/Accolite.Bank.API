using Accolite.Bank.Data.MsSql.Entities;
using Accolite.Bank.Data.MsSql.Interfaces.Repositories.Base;

namespace Accolite.Bank.Data.MsSql.Interfaces.Repositories;

public interface IAccountsRepository : IBaseRepository<AccountEntity>
{
}
