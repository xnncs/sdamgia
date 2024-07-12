using Core.Structures;

namespace Application.Dto.School;

public class CreateSchoolDto
{
    public string CourseName { get; set; }
    public string Description { get; set; }
    public int AuthorId { get; set; }
    
    public Subjects Subject { get; set; }
}