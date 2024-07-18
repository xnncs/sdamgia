namespace Core.Exceptions;

public class JwtAuthenticationException : Exception
{
    private static readonly string ExceptionMessage = "You are not authorized";

    public JwtAuthenticationException() : base(ExceptionMessage)
    { }
}