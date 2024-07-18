namespace Application.Dto.ExamTask;

public class UpdateExamTaskDto
{
    public int Id { get; set; }
    
    public string Data { get; set; }
    public string Prototype { get; set; }
    
    public int ClientId { get; set; }
}