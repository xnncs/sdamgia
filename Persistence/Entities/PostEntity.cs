using Core.Models;

namespace Persistence.Entities;

public class PostEntity
{
    public int Id { get; set; }
    
    // html
    public string Data { get; set; }
    
    public DateTime DateOfCreating { get; set; }
    public List<DateTime> DatesOfUpdating { get; set; } = Enumerable.Empty<DateTime>().ToList();
    
    public PageEntity Page { get; set; }
    public int PageId { get; set; }
}