namespace Application.Dto.Subject;

public record CreateSubjectDto
{
    public string Name { get; set; }
    public List<string> Prototypes { get; set; }
}