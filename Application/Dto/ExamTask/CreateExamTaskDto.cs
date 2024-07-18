using Core.Structures;

namespace Application.Dto.ExamTask;

public record CreateExamTaskDto
{
    public string Data { get; set; }
    
    public int SubjectId { get; set; }
    public string Prototype { get; set; }
    
    public int UserId { get; set; }
}