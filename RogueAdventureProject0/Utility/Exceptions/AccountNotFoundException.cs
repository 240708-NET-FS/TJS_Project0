namespace RevatureP0TimStDennis.Utility.Exceptions;

[Serializable]
public class AccountNotFoundException : Exception
{
    public AccountNotFoundException()
    {
    }

    public AccountNotFoundException(string? message) : base(message)
    {
    }

    public AccountNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}