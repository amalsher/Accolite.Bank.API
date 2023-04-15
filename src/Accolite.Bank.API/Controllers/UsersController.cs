using Accolite.Bank.Services.Interfaces.Providers;
using Accolite.Bank.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Accolite.Bank.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersProvider _usersProvider;

    public UsersController(IUsersProvider usersProvider)
    {
        _usersProvider = usersProvider;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAsync(CancellationToken ct = default)
    {
        return Ok(await _usersProvider.GetAllAsync(ct));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<User>>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return Ok(await _usersProvider.GetOneAsync(id, ct));
    }

    [HttpPost]
    public async Task<ActionResult<User>> AddAsync(User user)
    {
        return Ok(await _usersProvider.InsertAsync(user));
    }

    [HttpPut]
    public async Task<ActionResult<User?>> UpdateAsync(User user)
    {
        return Ok(await _usersProvider.UpdateAsync(user));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _usersProvider.DeleteAsync(id);
        return Ok();
    }
}
