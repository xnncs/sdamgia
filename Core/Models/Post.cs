namespace Core.Models;

public record Post
{
    public static Post Create(string data)
    {
        return new Post
        {
            Data = data,
            DateOfCreating = DateTime.UtcNow
        };
    }
    
    public int? Id { get; set; } = null;
    // html
    public string Data { get; set; }
    
    public DateTime DateOfCreating { get; set; }
    public List<DateTime> DatesOfUpdating { get; set; } = Enumerable.Empty<DateTime>().ToList();
}