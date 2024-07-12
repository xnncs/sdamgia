namespace Api.Contracts.Requests.Subject;

public class CreateSubjectRequest
{
    public string Name { get; set; }
    public List<string> Prototypes { get; set; }
}