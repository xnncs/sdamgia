using Core.Models;
using Persistence.Models;

namespace Persistence.Abstract;

public interface IExamTaskRepository
{
    Task AddAsync(ExamTask examTask);
    Task<ExamTask?> GetByIdAsync(int id);
    Task<IReadOnlyCollection<ExamTask>> GetAllAsync();
    Task UpdateAsync(int id, ExamTaskUpdatingModel updatingModel);
    Task DeleteAsync(int id);
}