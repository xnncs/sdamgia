using Core.Structures;

namespace Persistence.Entities;

public record ExamTaskEntity
{
    public int Id { get; set; }
    public Subjects Subject { get; set; }
    // html
    public string Data { get; set; }
    
    public TeacherEntity Author { get; set; }
    public int AuthorId { get; set; }
}