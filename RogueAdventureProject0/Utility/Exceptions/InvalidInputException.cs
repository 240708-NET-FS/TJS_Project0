namespace RevatureP0TimStDennis.Utility.Exceptions;

[Serializable]
public class InvalidInputException : Exception
{
    public InvalidInputException()
    {
        Console.WriteLine("Your user name and / or password was invalid. Please try again.");
    }

    public InvalidInputException(string? message) : base(message)
    {
    }

    public InvalidInputException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}