namespace Application.Dto.ExamTask;

public class DeleteExamTaskDto
{
    public DeleteExamTaskDto(int examTaskId, int clientId)
    {
        ExamTaskId = examTaskId;
        ClientId = clientId;
    }
    
    public int ExamTaskId { get; set; }
    public int ClientId { get; set; }
}