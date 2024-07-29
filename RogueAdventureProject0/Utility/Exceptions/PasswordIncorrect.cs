namespace RevatureP0TimStDennis.Utility.Exceptions;

[Serializable]
public class PasswordIncorrect : Exception
{
    public PasswordIncorrect()
    {
        Console.WriteLine("The password that was entered was incorrect.");
    }

    public PasswordIncorrect(string? message) : base(message)
    {
    }

    public PasswordIncorrect(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}