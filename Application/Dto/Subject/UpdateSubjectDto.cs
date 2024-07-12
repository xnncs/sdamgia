namespace Application.Dto.Subject;

public record UpdateSubjectDto
{
    public int ObjectToUpdateId { get; set; }
    
    public string Name { get; set; }
    public List<string> Prototypes { get; set; }
}