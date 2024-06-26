namespace Api.Contracts.Requests.Auth;

public record RegisterUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public int RoleStudentOrTeacher { get; set; }
}