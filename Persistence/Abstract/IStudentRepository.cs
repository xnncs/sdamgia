using Core.Models;

namespace Persistence.Abstract;

public interface IStudentRepository
{
    Task AddAsync(Student student);
    Task<bool> IsStudentByUserIdAsync(int userId);
    Task<Student?> GetByUserIdAsync(int userId);
    Task<int?> GetStudentIdByUserIdAsync(int userId);
    Task<bool> HasSchoolBylIdAsync(int studentId);
}
    