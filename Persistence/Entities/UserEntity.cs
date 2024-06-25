using Core.Models;
using Core.Structures;

namespace Persistence.Entities;

public record UserEntity
{
    public int Id { get; set; }

    public string Username { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }

    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public List<Roles> Roles { get; set; }

    public TeacherEntity? Teacher { get; set; }

    public StudentEntity? Student { get; set; }
    

    public DateTime DateOfCreating { get; set; }
}