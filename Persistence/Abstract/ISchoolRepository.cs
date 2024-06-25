using Core.Models;
using Persistence.Models;

namespace Persistence.Abstract;

public interface ISchoolRepository
{
    Task AddSchoolAsync(School school);
    Task AddStudentToSchoolAsync(Student student, int schoolId);
    Task<bool> ContainsStudentByIdAsync(int schoolId, int studentId);
    Task LeaveSchoolAsync(int studentId);
    Task UpdateAsync(SchoolUpdatingModel data, int schoolId);
    Task<School?> GetSchoolByIdAsync(int schoolId);
    Task<School?> GetSchoolByTeacherIdAsync(int teacherId);
    Task<School?> GetSchoolByStudentIdAsync(int studentId);
    Task DeleteByIdAsync(int schoolId);
}