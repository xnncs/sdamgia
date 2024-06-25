namespace Application.Dto;

public class EditPostRequestDto
{
    public string Data { get; set; }
    public int PostId { get; set; }
    
    public int UserId { get; set; }
}