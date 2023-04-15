using Accolite.Bank.Services.Interfaces;

namespace Accolite.Bank.Services.Models.Base;

public class BaseModel : IBaseModel
{
    public int Id { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }
}
