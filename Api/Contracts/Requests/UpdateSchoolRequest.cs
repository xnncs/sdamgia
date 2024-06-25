namespace Api.Contracts.Requests;

public record UpdateSchoolRequest
{
    public string CourseName { get; set; }
    public string Description { get; set; }
}