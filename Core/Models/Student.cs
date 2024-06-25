namespace Core.Models;

public record Student
{
    public static Student CreateBasedOnUserAndModifyIt(User user)
    {
       Student result = new Student()
        {
            User = user,
            School = null,
        };
        user.Student = result;

        return result;
    }

    public int? Id { get; set; } = null;
    public School? School { get; set; }
    public User User { get; set; }
}