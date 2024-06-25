namespace Core.Models;

public record Teacher
{
    public static Teacher CreateBasedOnUserAndModifyIt(User user)
    {
        Teacher result = new Teacher()
        {
            User = user,
            School = null,
        };
        user.Teacher = result;

        return result;
    }
    
    public int? Id { get; set; } = null;
    
    public School? School { get; set; }
    public User User { get; set; }

    public List<ExamTask> ExamTasksCreated { get; set; } = Enumerable.Empty<ExamTask>().ToList();
}