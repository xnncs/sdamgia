namespace Core.Exceptions;

public class NoSuchAccountException : Exception
{
    private static readonly string ExceptionMessage = "Failed to login, no such accounts with that email";

    public NoSuchAccountException() : base(ExceptionMessage)
    { }
}