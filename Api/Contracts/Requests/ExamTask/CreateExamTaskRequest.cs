using Core.Structures;

namespace Api.Contracts.Requests.ExamTask;

public record CreateExamTaskRequest
{
    public Subjects Subject { get; set; }
    public double Prototype { get; set; }
    public string Data { get; set; }
}