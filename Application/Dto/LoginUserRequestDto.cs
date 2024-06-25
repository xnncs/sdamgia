namespace Application.Dto;

public record LoginUserRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}