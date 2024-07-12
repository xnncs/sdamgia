namespace Api.Contracts.Requests.Post;

public class EditPostRequest
{
    public int ObjectToUpdateId { get; set; }
    public string Data { get; set; }
}