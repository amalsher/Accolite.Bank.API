using Accolite.Bank.Data.MsSql.Entities;
using Accolite.Bank.Data.MsSql.Interfaces.Repositories;
using Accolite.Bank.Services.Interfaces.Providers;
using Accolite.Bank.Services.Models;
using Accolite.Bank.Services.Providers.Base;
using AutoMapper;

namespace Accolite.Bank.Services.Providers;

public class AccountsProvider : BaseProvider<Account, AccountEntity>, IAccountsProvider
{
    public AccountsProvider(IMapper mapper, IAccountsRepository repository) : base(mapper, repository)
    {
    }
}
