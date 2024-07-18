using Core.Models;
using Core.Structures;

namespace Persistence.Entities;

public record ExamOptionEntity
{
    public int Id { get; set; }
    public SubjectEntity Subject { get; set; }

    public IEnumerable<ExamTaskEntity> ExamTasks { get; set; } = Enumerable.Empty<ExamTaskEntity>();
    
    public TeacherEntity Author { get; set; }
    public int AuthorId { get; set; }
    
    public DateTime DateOfCreating { get; set; }
}