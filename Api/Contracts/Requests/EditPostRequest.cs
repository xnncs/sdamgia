namespace Api.Contracts.Requests;

public class EditPostRequest
{
    public int PostId { get; set; }
    public string Data { get; set; }
}