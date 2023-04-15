using Accolite.Bank.Data.MsSql.DbContext;
using Accolite.Bank.Data.MsSql.Entities;
using Accolite.Bank.Data.MsSql.Interfaces.Repositories;
using Accolite.Bank.Data.MsSql.Repositories.Repositories.Base;

namespace Accolite.Bank.Data.MsSql.Repositories.Repositories;

public class UsersRepository : BaseRepository<UserEntity>, IUsersRepository
{
    public UsersRepository(AccoliteBankContext dbContext) : base(dbContext)
    {
    }
}
