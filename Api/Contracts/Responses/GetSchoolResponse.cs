using Api.Contracts.Responses.ResponseHelpingModels;
using Core.Structures;

namespace Api.Contracts.Responses;

public record GetSchoolResponse
{
    public int? Id { get; set; } = null;
    
    public string CourseName { get; set; }
    public string? Description { get; set; }
    
    public int StudentsNumber { get; set; }
    
    public PageHelperResponseModel Page { get; set; }
    
    public Subjects Subject { get; set; }
    
    public DateTime DateOfCreating { get; set; }
    public List<DateTime> DatesOfUpdating { get; set; } = Enumerable.Empty<DateTime>().ToList();
}