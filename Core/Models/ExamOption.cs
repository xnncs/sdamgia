using Core.StaticInfoModels;
using Core.Structures;

namespace Core.Models;

public record ExamOption
{
    public int? Id { get; set; } = null;
    
    public Subject Subject { get; set; }

    public IEnumerable<ExamTask> ExamTasks { get; set; } = Enumerable.Empty<ExamTask>();
    
    public User Author { get; set; }
    
    public DateTime DateOfCreating { get; set; }
}