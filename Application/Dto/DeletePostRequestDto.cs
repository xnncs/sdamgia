namespace Application.Dto;

public class DeletePostRequestDto
{
    public static DeletePostRequestDto Create(int postId, int userId)
    {
        return new DeletePostRequestDto()
        {
            PostId = postId,
            UserId = userId
        };
    }
    
    public int PostId { get; set; }
    public int UserId { get; set; }
}