namespace Api.Contracts.Requests.School;

public record UpdateSchoolRequest
{
    public string CourseName { get; set; }
    public string Description { get; set; }
}