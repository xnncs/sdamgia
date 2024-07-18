using Application.Dto.ExamTask;
using Core.Models;

namespace Application.Abstract;

public interface IExamTaskService
{
    Task CreateExamTaskAsync(CreateExamTaskDto request);
    Task<IReadOnlyCollection<ExamTask>> GetAllAsync();
    Task<ExamTask?> GetByIdAsync(int id);
    Task UpdateAsync(UpdateExamTaskDto request);
    Task DeleteAsync(DeleteExamTaskDto request);
}