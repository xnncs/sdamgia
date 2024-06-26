namespace Api.Contracts.Requests.Auth;

public record LoginUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}