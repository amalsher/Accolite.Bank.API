using Accolite.Bank.Services.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Accolite.Bank.Services.Models;

public class User : BaseModel
{
    [MaxLength(64)]
    public string? FirstName { get; set; }

    [MaxLength(64)]
    public string? LastName { get; set; }

    [MaxLength(16)]
    public string? Phone { get; set; }

    [Required]
    [MaxLength(64)]
    public string Email { get; set; } = string.Empty;

    public IEnumerable<Account> Accounts { get; } = new List<Account>();
}
