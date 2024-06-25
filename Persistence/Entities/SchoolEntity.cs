using Core.Models;
using Core.Structures;
using Microsoft.VisualBasic;

namespace Persistence.Entities;

public record SchoolEntity
{
    public int Id { get; set; }
    
    public string CourseName { get; set; }
    public string? Description { get; set; }
    
    public TeacherEntity Author { get; set; }

    public int StudentsNumber { get; set; }
    
    public List<StudentEntity> Students { get; set; } = Enumerable.Empty<StudentEntity>().ToList();
    
    public PageEntity Page { get; set; }
    
    public Subjects Subject { get; set; }
    
    public DateTime DateOfCreating { get; set; }
    public List<DateTime> DatesOfUpdating { get; set; } = Enumerable.Empty<DateTime>().ToList();
}