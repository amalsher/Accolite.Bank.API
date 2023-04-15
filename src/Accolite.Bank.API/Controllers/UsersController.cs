using Accolite.Bank.Data.MsSql.Entities;
using Accolite.Bank.Data.MsSql.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Accolite.Bank.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _usersRepository;

    public UsersController(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetAsync(CancellationToken ct = default)
    {
        return Ok(await _usersRepository.FilterByAsync(ct: ct));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return Ok(await _usersRepository.FindByIdAsync(id, ct));
    }

    [HttpPost]
    public async Task<ActionResult<UserEntity>> AddAsync(UserEntity user, CancellationToken ct = default)
    {
        return Ok(await _usersRepository.InsertAsync(user, ct));
    }

    [HttpPut]
    public async Task<ActionResult<UserEntity?>> UpdateAsync(UserEntity user, CancellationToken ct = default)
    {
        return Ok(await _usersRepository.UpdateAsync(user, ct));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        await _usersRepository.DeleteAsync(id, ct);
        return Ok();
    }
}
