using Core.Structures;

namespace Core.Models;

public record School
{
    public static School CreateSchool(string courseName, string description, Teacher author, Subjects subject)
    {
        Page page = new Page();
        
        School school = new School
        {
            CourseName = courseName,
            Description = description,
            Author = author,
            Subject = subject,
            Page = page,
            DateOfCreating = DateTime.UtcNow
        };
        page.School = school;

        return school;
    }
    
    public int? Id { get; set; } = null;
    
    public string CourseName { get; set; }
    public string? Description { get; set; }
    
    public int StudentsNumber { get; set; }
    
    public Teacher Author { get; set; }
    public List<Student> Students { get; set; } = Enumerable.Empty<Student>().ToList();
    public Page Page { get; set; }
    
    public Subjects Subject { get; set; }
    
    public DateTime DateOfCreating { get; set; }
    public List<DateTime> DatesOfUpdating { get; set; } = Enumerable.Empty<DateTime>().ToList();
}