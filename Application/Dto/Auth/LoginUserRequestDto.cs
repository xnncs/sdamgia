namespace Application.Dto.Auth;

public record LoginUserRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}