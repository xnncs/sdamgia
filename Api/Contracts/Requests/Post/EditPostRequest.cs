namespace Api.Contracts.Requests.Post;

public class EditPostRequest
{
    public int PostId { get; set; }
    public string Data { get; set; }
}