using Core.Structures;

namespace Api.Contracts.Requests.School;

public record CreateSchoolRequest
{
    public string CourseName { get; set; }
    public string Description { get; set; }
    public int SubjectId { get; set; }
}