namespace Core.Exceptions;

public class JwtAuthenticationException : Exception
{
    private static readonly string ExceptionMessage = "Jwt token didnt set in cookies";

    public JwtAuthenticationException() : base(ExceptionMessage)
    { }
}