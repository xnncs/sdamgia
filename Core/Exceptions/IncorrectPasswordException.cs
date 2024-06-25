namespace Core.Exceptions;

public class IncorrectPasswordException : Exception
{
    private static readonly string ExceptionMessage = "Failed to login, incorrect password";
    
    public IncorrectPasswordException() : base(ExceptionMessage)
    { }
}