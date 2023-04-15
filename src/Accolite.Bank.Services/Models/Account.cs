using Accolite.Bank.Services.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Accolite.Bank.Services.Models;

public class Account : BaseModel
{
    [Required]
    [MaxLength(16)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int UserId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public User? User { get; set; }
}
