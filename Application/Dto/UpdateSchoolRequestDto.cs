namespace Application.Dto;

public class UpdateSchoolRequestDto
{
    public string CourseName { get; set; }
    public string Description { get; set; }
    
    public int UserId { get; set; }
}