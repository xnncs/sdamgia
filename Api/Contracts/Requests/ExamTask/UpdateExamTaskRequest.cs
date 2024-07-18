namespace Api.Contracts.Requests.ExamTask;

public record UpdateExamTaskRequest
{
    public int Id { get; set; }
    
    public string Data { get; set; }
    public string Prototype { get; set; }
}
