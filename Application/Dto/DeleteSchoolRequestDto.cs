namespace Application.Dto;

public class DeleteSchoolRequestDto
{
    public static DeleteSchoolRequestDto Create(int userId)
    {
        return new DeleteSchoolRequestDto()
        {
            UserId = userId
        };
    }
    
    public int UserId { get; set; }
}