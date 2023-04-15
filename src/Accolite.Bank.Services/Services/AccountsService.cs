using Accolite.Bank.Services.CustomExceptions;
using Accolite.Bank.Services.Interfaces.Providers;
using Accolite.Bank.Services.Interfaces.Services;
using Accolite.Bank.Services.Models;

namespace Accolite.Bank.Services.Services;

public class AccountsService : IAccountsService
{
    // TODO: move these to appsettings.json
    private const decimal MaxDepositAmount = 10000;
    private const decimal MinAmount = 100;

    private readonly IAccountsProvider _accountsProvider;

    public AccountsService(IAccountsProvider accountsProvider)
    {
        _accountsProvider = accountsProvider;
    }

    public Task<Account> InsertAsync(Account account, int userId)
    {
        if (account.UserId != userId)
        {
            throw new AccountException("You cannot create account for another user");
        }

        VerifyMinAmount(account);

        return _accountsProvider.InsertAsync(account);
    }

    public async Task<Account?> UpdateAsync(Account account, int userId)
    {
        VerifyAccountOwnerForModification(account, userId);

        var existingAccount = await _accountsProvider.GetOneAsync(account.Id);

        VerifyAccountExists(existingAccount);

        if (account.Amount != existingAccount!.Amount)
        {
            throw new AccountException("This method cannot be used to modify funds");
        }

        return await _accountsProvider.UpdateAsync(account);
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var account = await _accountsProvider.GetOneAsync(id);

        VerifyAccountExists(account);

        if (account!.UserId != userId)
        {
            throw new AccountException("You cannot delete account for another user");
        }

        await _accountsProvider.DeleteAsync(id);
    }

    public async Task DepositAsync(int accountId, int userId, decimal amount)
    {
        if (amount > MaxDepositAmount)
        {
            throw new AccountException($"Deposit amount cannot exceed ${MaxDepositAmount}");
        }

        var account = await _accountsProvider.GetOneAsync(accountId);

        VerifyAccountExists(account);

        VerifyAccountOwnerForModification(account!, userId);

        account!.Amount += amount;

        await _accountsProvider.UpdateAsync(account);
    }

    public async Task WithdrawAsync(int accountId, int userId, decimal amount)
    {
        var account = await _accountsProvider.GetOneAsync(accountId);

        VerifyAccountExists(account);

        VerifyAccountOwnerForModification(account!, userId);

        if (account!.Amount - amount < MinAmount)
        {
            throw new AccountException($"Account cannot have less than ${MinAmount} at any time");
        }

        if (account.Amount - amount < account.Amount / 10)
        {
            throw new AccountException($"Cannot withdraw more than 90% of total balance from an account in a single transaction");
        }

        account.Amount -= amount;

        await _accountsProvider.UpdateAsync(account);
    }

    private static void VerifyMinAmount(Account account)
    {
        if (account.Amount < MinAmount)
        {
            throw new AccountException($"Account cannot have less than ${MinAmount}");
        }
    }

    private static void VerifyAccountOwnerForModification(Account account, int userId)
    {
        if (account!.UserId != userId)
        {
            throw new AccountException("You cannot modify account of another user");
        }
    }

    private static void VerifyAccountExists(Account? account)
    {
        if (account == null)
        {
            throw new AccountException("Account doesn't exist");
        }
    }
}
