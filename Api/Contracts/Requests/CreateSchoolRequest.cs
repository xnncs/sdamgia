using Core.Structures;

namespace Api.Contracts.Requests;

public record CreateSchoolRequest
{
    public string CourseName { get; set; }
    public string Description { get; set; }
    public Subjects Subject { get; set; }
}