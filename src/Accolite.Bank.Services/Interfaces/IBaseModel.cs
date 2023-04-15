namespace Accolite.Bank.Services.Interfaces;

public interface IBaseModel
{
    public int Id { get; }

    DateTime? Created { get; }

    DateTime? Updated { get; }
}
