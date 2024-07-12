namespace Api.Contracts.Requests.Subject;

public record UpdateSubjectRequest
{
    public int ObjectToUpdateId { get; set; }
    
    public string Name { get; set; }
    public List<string> Prototypes { get; set; }
}