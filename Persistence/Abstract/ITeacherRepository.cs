using Core.Models;
using Persistence.Entities;

namespace Persistence.Abstract;

public interface ITeacherRepository
{
    Task AddAsync(Teacher teacher);
    Task<bool> IsTeacherByUserIdAsync(int userId);
    Task<Teacher?> GetByUserIdAsync(int userId);
    Task<bool> HasSchoolByIdAsync(int teacherId);
    Task<School?> GetSchoolByTeacherIdAsync(int teacherId);
    Task<int?> GetTeacherIdByUserIdAsync(int userId);
}