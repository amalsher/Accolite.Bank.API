using Accolite.Bank.Services.Interfaces.Providers;
using Accolite.Bank.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Accolite.Bank.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountsProvider _accountsProvider;

    public AccountsController(IAccountsProvider accountsProvider)
    {
        _accountsProvider = accountsProvider;
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
    public async Task<ActionResult<Account>> AddAsync(Account account, CancellationToken ct = default)
    {
        return Ok(await _accountsProvider.InsertAsync(account, ct));
    }

    [HttpPut]
    public async Task<ActionResult<Account?>> UpdateAsync(Account account, CancellationToken ct = default)
    {
        return Ok(await _accountsProvider.UpdateAsync(account, ct));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        await _accountsProvider.DeleteAsync(id, ct);
        return Ok();
    }
}
