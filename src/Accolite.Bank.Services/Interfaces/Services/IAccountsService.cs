using Accolite.Bank.Services.Models;

namespace Accolite.Bank.Services.Interfaces.Services;

public interface IAccountsService
{
    Task<Account> InsertAsync(Account account, int userId);
    Task<Account?> UpdateAsync(Account account, int userId);
    Task DeleteAsync(int id, int userId);
    Task DepositAsync(int accountId, int userId, decimal amount);
    Task WithdrawAsync(int accountId, int userId, decimal amount);
}
