using Core.Structures;

namespace Persistence.Entities;

public record ExamTaskEntity
{
    public int Id { get; set; }
    public SubjectEntity Subject { get; set; }
    public string Prototype { get; set; }
    // html
    public string Data { get; set; }
    
    public TeacherEntity Author { get; set; }
    public int AuthorId { get; set; }
    
    public DateTime DateOfCreating { get; set; }
    public List<DateTime> DatesOfUpdating { get; set; }
}