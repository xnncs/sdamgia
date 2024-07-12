namespace Core.StaticInfoModels;

public record Subject
{
    public static Subject Create(string name, List<string> prototypes)
    {
        return new Subject
        {
            Name = name,
            Prototypes = prototypes,
            DateOfCreating = DateTime.UtcNow
        };
    }

    public int? Id { get; set; } = null;
    
    public string Name { get; set; }
    public List<string> Prototypes { get; set; }
    
    public DateTime DateOfCreating { get; set; }
    public List<DateTime> DatesOfUpdating { get; set; } = Enumerable.Empty<DateTime>().ToList();
}