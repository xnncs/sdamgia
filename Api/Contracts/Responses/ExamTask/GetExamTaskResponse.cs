namespace Api.Contracts.Responses.ExamTask;

public class GetExamTaskResponse
{
    public int? Id { get; set; } 
    
    public string Data { get; set; }
    
    public string SubjectName { get; set; }

    public string Prototype { get; set; }
    
    public int AuthorId { get; set; }
    public DateTime DateOfCreating { get; set; }
    public List<DateTime> DatesOfUpdating { get; set; } = Enumerable.Empty<DateTime>().ToList();
}