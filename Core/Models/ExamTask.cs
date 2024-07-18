using Core.StaticInfoModels;
using Core.Structures;

namespace Core.Models;

public record ExamTask
{
    public static ExamTask Create(string data, Subject subject, string prototype, Teacher author)
    {
        return new ExamTask
        {
            Data = data,
            Subject = subject,
            Prototype = prototype,
            Author = author,
            DateOfCreating = DateTime.UtcNow
        };
    }

    
    public int? Id { get; set; } 
    // html
    public string Data { get; set; }
    
    
    public Subject Subject { get; set; }

    public string Prototype { get; set; }
    
    public Teacher Author { get; set; }
    public DateTime DateOfCreating { get; set; }
    public List<DateTime> DatesOfUpdating { get; set; } = Enumerable.Empty<DateTime>().ToList();
}