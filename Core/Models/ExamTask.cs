using Core.Structures;

namespace Core.Models;

public record ExamTask
{
    public int? Id { get; set; } = null;
    
    public Subjects Subject { get; set; }
    // html
    public string Data { get; set; }
    
    public Teacher Author { get; set; }
}