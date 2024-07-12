namespace Application.Dto.Auth;

public record LoginUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}