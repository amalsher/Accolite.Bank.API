namespace Accolite.Bank.Services.CustomExceptions;

public class AccountException : Exception
{
    public AccountException(string message) : base(message)
    {
    }

    public AccountException()
    {
    }

    public AccountException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
