using Accolite.Bank.Services.CustomExceptions;
using Accolite.Bank.Services.Interfaces.Providers;
using Accolite.Bank.Services.Interfaces.Services;
using Accolite.Bank.Services.Models;
using Accolite.Bank.Services.Services;

namespace Accolite.Bank.Services.Tests.Services;

public class AccountServiceShould
{
    private readonly Mock<IAccountsProvider> _mockAccountsProvider;
    private readonly IAccountsService _accountsService;

    public AccountServiceShould()
    {
        _mockAccountsProvider = new Mock<IAccountsProvider>();
        _accountsService = new AccountsService(_mockAccountsProvider.Object);
    }

    [Fact]
    public async Task ReturnInsertedAccountWhenInsertingWithValidData()
    {
        // Arrange
        var account = new Account { Id = 1, UserId = 1, Amount = 1000 };
        var userId = 1;

        _mockAccountsProvider.Setup(x => x.InsertAsync(account, It.IsAny<CancellationToken>())).ReturnsAsync(account);

        // Act
        var result = await _accountsService.InsertAsync(account, userId);

        // Assert
        Assert.Equal(account, result);
        _mockAccountsProvider.Verify(x => x.InsertAsync(account, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ThrowAccountExceptionWhenInsertingWithInvalidUserId()
    {
        // Arrange
        var account = new Account { Id = 1, UserId = 2, Amount = 1000 };
        var userId = 1;

        // Act and Assert
        await Assert.ThrowsAsync<AccountException>(() => _accountsService.InsertAsync(account, userId));
    }

    [Fact]
    public async Task ReturnUpdatedAccountWhenUpdatingWithValidData()
    {
        // Arrange
        var account = new Account { Id = 1, UserId = 1, Amount = 1000 };
        var userId = 1;

        _mockAccountsProvider.Setup(x => x.GetOneAsync(account.Id, It.IsAny<CancellationToken>())).ReturnsAsync(account);
        _mockAccountsProvider.Setup(x => x.UpdateAsync(account, It.IsAny<CancellationToken>())).ReturnsAsync(account);

        // Act
        var result = await _accountsService.UpdateAsync(account, userId);

        // Assert
        Assert.Equal(account, result);
        _mockAccountsProvider.Verify(x => x.GetOneAsync(account.Id, It.IsAny<CancellationToken>()), Times.Once);
        _mockAccountsProvider.Verify(x => x.UpdateAsync(account, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ThrowAccountExceptionWhenUpdatingWithInvalidUserId()
    {
        // Arrange
        var account = new Account { Id = 1, UserId = 2, Amount = 1000 };
        var userId = 1;

        // Act and Assert
        await Assert.ThrowsAsync<AccountException>(() => _accountsService.UpdateAsync(account, userId));
    }

    [Fact]
    public async Task DepositAmountToAccountWhenDeposittingWithValidData()
    {
        // Arrange
        var accountId = 1;
        var userId = 1;
        var amount = 500;
        var account = new Account { Id = accountId, UserId = userId, Amount = 1000 };

        _mockAccountsProvider.Setup(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>())).ReturnsAsync(account);
        _mockAccountsProvider.Setup(x => x.UpdateAsync(account, It.IsAny<CancellationToken>())).ReturnsAsync(account);

        // Act
        await _accountsService.DepositAsync(accountId, userId, amount);

        // Assert
        Assert.Equal(1500, account.Amount);
        _mockAccountsProvider.Verify(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>()), Times.Once);
        _mockAccountsProvider.Verify(x => x.UpdateAsync(account, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SuccessfullyDepositValidDepositAmount()
    {
        // Arrange
        int accountId = 1;
        int userId = 1;
        decimal amount = 5000;
        var account = new Account { Id = accountId, UserId = userId, Amount = 10000 };

        _mockAccountsProvider.Setup(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>())).ReturnsAsync(account);

        // Act
        await _accountsService.DepositAsync(accountId, userId, amount);

        // Assert
        Assert.Equal(15000, account.Amount); // Initial amount + deposited amount
        _mockAccountsProvider.Verify(x => x.UpdateAsync(account, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ThrowAccountExceptionWhenDeposittingMoreThanMaxDepositAmount()
    {
        // Arrange
        int accountId = 1;
        int userId = 1;
        decimal amount = 15000; // Max deposit amount
        var account = new Account { Id = accountId, UserId = userId, Amount = 10000 };
        _mockAccountsProvider.Setup(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>())).ReturnsAsync(account);

        // Act and Assert
        await Assert.ThrowsAsync<AccountException>(() => _accountsService.DepositAsync(accountId, userId, amount));
        _mockAccountsProvider.Verify(x => x.UpdateAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ThrowAccountExceptionWhenDeposittingWithInvalidAccountId()
    {
        // Arrange
        int accountId = 1;
        int userId = 1;
        decimal amount = 5000;
        _mockAccountsProvider.Setup(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>())).ReturnsAsync((Account?)null); // Account not found

        // Act and Assert
        await Assert.ThrowsAsync<AccountException>(() => _accountsService.DepositAsync(accountId, userId, amount));
        _mockAccountsProvider.Verify(x => x.UpdateAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ThrowAccountExceptionWhenDeposittingWithInvalidUserId()
    {
        // Arrange
        int accountId = 1;
        int userId = 1;
        decimal amount = 5000;
        var account = new Account { Id = accountId, UserId = 2, Amount = 10000 }; // Account belongs to different user
        _mockAccountsProvider.Setup(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>())).ReturnsAsync(account);

        // Act and Assert
        await Assert.ThrowsAsync<AccountException>(() => _accountsService.DepositAsync(accountId, userId, amount));
        _mockAccountsProvider.Verify(x => x.UpdateAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task SuccessfullyWithdrawValidWithdrawalAmount()
    {
        // Arrange
        int accountId = 1;
        int userId = 1;
        decimal amount = 5000;
        var account = new Account { Id = accountId, UserId = userId, Amount = 10000 };
        _mockAccountsProvider.Setup(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>())).ReturnsAsync(account);

        // Act
        await _accountsService.WithdrawAsync(accountId, userId, amount);

        // Assert
        Assert.Equal(5000, account.Amount); // Initial amount - withdrawn amount
        _mockAccountsProvider.Verify(x => x.UpdateAsync(account, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ThrowAccountExceptionForInvalidWithdrawalAmount()
    {
        // Arrange
        int accountId = 1;
        int userId = 1;
        decimal amount = 15000; // Withdrawal amount greater than account balance
        var account = new Account { Id = accountId, UserId = userId, Amount = 10000 };
        _mockAccountsProvider.Setup(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>())).ReturnsAsync(account);

        // Act and Assert
        await Assert.ThrowsAsync<AccountException>(() => _accountsService.WithdrawAsync(accountId, userId, amount));
        _mockAccountsProvider.Verify(x => x.UpdateAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ThrowAccountExceptionWhenWithdrawingWithInvalidAccountId()
    {
        // Arrange
        int accountId = 1;
        int userId = 1;
        decimal amount = 5000;
        _mockAccountsProvider.Setup(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>())).ReturnsAsync((Account?)null); // Account not found

        // Act and Assert
        await Assert.ThrowsAsync<AccountException>(() => _accountsService.WithdrawAsync(accountId, userId, amount));
        _mockAccountsProvider.Verify(x => x.UpdateAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ThrowAccountExceptionWhenWithdrawingWithInvalidUserId()
    {
        // Arrange
        int accountId = 1;
        int userId = 1;
        decimal amount = 5000;
        var account = new Account { Id = accountId, UserId = 2, Amount = 10000 }; // Account belongs to different user
        _mockAccountsProvider.Setup(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>())).ReturnsAsync(account);

        // Act and Assert
        await Assert.ThrowsAsync<AccountException>(() => _accountsService.WithdrawAsync(accountId, userId, amount));
        _mockAccountsProvider.Verify(x => x.UpdateAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task SuccessfullyDeleteAccountWhenDeletingWithValidParameters()
    {
        // Arrange
        int accountId = 1;
        int userId = 1;
        var account = new Account { Id = accountId, UserId = userId };
        _mockAccountsProvider.Setup(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>())).ReturnsAsync(account);

        // Act
        await _accountsService.DeleteAsync(accountId, userId);

        // Assert
        _mockAccountsProvider.Verify(x => x.DeleteAsync(accountId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ThrowAccountExceptionWhenDeletingWithWrongAccountId()
    {
        // Arrange
        int accountId = 1;
        int userId = 1;
        var account = new Account { Id = accountId, UserId = userId };
        _mockAccountsProvider.Setup(x => x.GetOneAsync(accountId, It.IsAny<CancellationToken>())).ReturnsAsync(account);

        // Act and Assert
        await Assert.ThrowsAsync<AccountException>(() => _accountsService.DeleteAsync(accountId, 2));
        _mockAccountsProvider.Verify(x => x.DeleteAsync(accountId, It.IsAny<CancellationToken>()), Times.Never);
    }
}
