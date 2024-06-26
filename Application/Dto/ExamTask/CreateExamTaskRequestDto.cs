using Core.Structures;

namespace Application.Dto.ExamTask;

public record CreateExamTaskRequestDto
{
    public Subjects Subject { get; set; }
    public double Prototype { get; set; }
    public string Data { get; set; }
    
    public int UserId { get; set; }
}