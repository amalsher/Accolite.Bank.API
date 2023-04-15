using Accolite.Bank.Services.Interfaces.Providers;
using Accolite.Bank.Services.Interfaces.Services;
using Accolite.Bank.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Accolite.Bank.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountsProvider _accountsProvider;
    private readonly IAccountsService _accountsService;

    public AccountsController(IAccountsProvider accountsProvider, IAccountsService accountsService)
    {
        _accountsProvider = accountsProvider;
        _accountsService = accountsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAsync(CancellationToken ct = default)
    {
        return Ok(await _accountsProvider.GetAllAsync(ct));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Account>>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return Ok(await _accountsProvider.GetOneAsync(id, ct));
    }

    [HttpPost]
    public async Task<ActionResult<Account>> AddAsync(Account account, int userId)
    {
        return Ok(await _accountsService.InsertAsync(account, userId));
    }

    [HttpPost("deposit")]
    public async Task<ActionResult> DepositAsync(int accountId, int userId, decimal amount)
    {
        await _accountsService.DepositAsync(accountId, userId, amount);
        return Ok();
    }

    [HttpPost("withdraw")]
    public async Task<ActionResult> WithdrawAsync(int accountId, int userId, decimal amount)
    {
        await _accountsService.WithdrawAsync(accountId, userId, amount);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<Account?>> UpdateAsync(Account account, int userId)
    {
        return Ok(await _accountsService.UpdateAsync(account, userId));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(int id, int userId)
    {
        await _accountsService.DeleteAsync(id, userId);
        return Ok();
    }
}
