namespace Application.Dto.School;

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