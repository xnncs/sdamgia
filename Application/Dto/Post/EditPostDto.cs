namespace Application.Dto.Post;

public class EditPostDto
{
    public string Data { get; set; }
    public int ObjectToUpdateId { get; set; }
    
    public int UserId { get; set; }
}