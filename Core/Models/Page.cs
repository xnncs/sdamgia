namespace Core.Models;

public record Page
{
    public int? Id { get; set; } = null;
    public School School { get; set; }
    public List<Post> Posts { get; set; } = Enumerable.Empty<Post>().ToList();
}