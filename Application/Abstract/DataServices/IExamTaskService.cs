using Application.Dto.ExamTask;

namespace Application.Abstract;

public interface IExamTaskService
{
    Task CreateExamTaskAsync(CreateExamTaskDto request);
}