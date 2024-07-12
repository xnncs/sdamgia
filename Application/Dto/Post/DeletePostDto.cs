namespace Application.Dto.Post;

public class DeletePostDto
{
    public static DeletePostDto Create(int postId, int userId)
    {
        return new DeletePostDto()
        {
            PostId = postId,
            UserId = userId
        };
    }
    
    public int PostId { get; set; }
    public int UserId { get; set; }
}