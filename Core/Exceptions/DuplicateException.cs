namespace Core.Exceptions;

public class DuplicateException : Exception
{
    private static readonly string ExceptionMessage = "Login is already used";

    public DuplicateException() : base(ExceptionMessage)
    { }
}