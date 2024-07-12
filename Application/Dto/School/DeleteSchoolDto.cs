namespace Application.Dto.School;

public class DeleteSchoolDto
{
    public static DeleteSchoolDto Create(int userId)
    {
        return new DeleteSchoolDto()
        {
            UserId = userId
        };
    }
    
    public int UserId { get; set; }
}