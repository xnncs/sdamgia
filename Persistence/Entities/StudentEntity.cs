namespace Persistence.Entities;

public record StudentEntity
{
    public int Id { get; set; }
    
    public SchoolEntity? School { get; set; }
    public int? SchoolId { get; set; }
    
    public UserEntity User { get; set; }
    public int UserId { get; set; }
}