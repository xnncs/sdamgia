using AutoMapper;
using Core.Models;
using Persistence.Abstract;
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
        ExamTaskEntity examTaskEntity = _mapper.Map<ExamTask, ExamTaskEntity>(examTask);

        _dbContext.Attach(examTaskEntity.Author);
        
        _dbContext.ExamTasks.Add(examTaskEntity);

        await _dbContext.SaveChangesAsync();
    }
}