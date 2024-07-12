using Core.Models;

namespace Persistence.Abstract;

public interface IExamTaskRepository
{
    public Task AddAsync(ExamTask examTask);
}