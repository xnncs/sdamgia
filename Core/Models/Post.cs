namespace Core.Models;

public record Post
{
    public int? Id { get; set; } = null;
    // html
    public string Data { get; set; }
    
    public DateTime DateOfCreating { get; set; }
    public List<DateTime> DatesOfUpdating { get; set; } = Enumerable.Empty<DateTime>().ToList();
}