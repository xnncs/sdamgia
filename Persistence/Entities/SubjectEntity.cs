namespace Persistence.Entities;

public record SubjectEntity
{
    public int Id { get; set; } 
    
    public string Name { get; set; }
    public List<string> Prototypes { get; set; }
    
    public DateTime DateOfCreating { get; set; }
    public List<DateTime> DatesOfUpdating { get; set; } = Enumerable.Empty<DateTime>().ToList();
}