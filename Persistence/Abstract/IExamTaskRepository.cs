using Core.Models;

namespace Persistence.Abstract;

public interface IExamTaskRepository
{
    Task AddAsync(ExamTask examTask);
}