using Core.Models;

namespace Persistence.Entities;

public record PageEntity
{
    public int Id { get; set; }
    public SchoolEntity School { get; set; }
    public int SchoolId { get; set; }
    public List<PostEntity> Posts { get; set; } = Enumerable.Empty<PostEntity>().ToList();
}