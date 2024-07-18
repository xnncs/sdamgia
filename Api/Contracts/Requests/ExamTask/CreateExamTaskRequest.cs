using Core.Structures;

namespace Api.Contracts.Requests.ExamTask;

public record CreateExamTaskRequest
{
    public string Data { get; set; }
    
    public int SubjectId { get; set; }
    public string Prototype { get; set; }
}