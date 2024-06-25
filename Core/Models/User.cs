using Core.Structures;

namespace Core.Models;

public record User
{
    public static User Create(string email, string passwordHash, string username, string firstname, string? lastname, Roles role)
    {
        return new User
        {
            Email = email,
            PasswordHash = passwordHash,
            Username = username,
            FirstName = firstname,
            LastName = lastname,
            DateOfCreating = DateTime.UtcNow,
            Roles = [ role ]
        };
    }
    
    public int? Id { get; set; } = null;

    public string Username { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }

    public Teacher? Teacher { get; set; }
    public Student? Student { get; set; }
    
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public List<Roles> Roles { get; set; }
    public DateTime DateOfCreating { get; set; }
}