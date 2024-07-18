using AutoMapper;
using Core.Models;
using Persistence.Abstract;
using Persistence.Database;
using Persistence.Entities;

namespace Persistence.Repositories;

public class ExamTaskRepository : IExamTaskRepository
{
    public ExamTaskRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public async Task AddAsync(ExamTask examTask)
    {
        ExamTaskEntity objectToAdd = _mapper.Map<ExamTask, ExamTaskEntity>(examTask);

        _dbContext.Teachers.Attach(objectToAdd.Author);
        _dbContext.Subjects.Attach(objectToAdd.Subject);
        
        _dbContext.ExamTasks.Add(objectToAdd);

        await _dbContext.SaveChangesAsync();
    }
}