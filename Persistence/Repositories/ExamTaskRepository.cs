using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstract;
using Persistence.Database;
using Persistence.Entities;
using Persistence.Models;

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
        
        // adding exam task to teacher properties 
        TeacherEntity teacher = await _dbContext.Teachers.FirstOrDefaultAsync(x => x.Id == examTask.Author.Id)
                                ?? throw new Exception();
        teacher.ExamTasksCreated.Add(objectToAdd);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ExamTask?> GetByIdAsync(int id)
    {
        ExamTaskEntity? result = await _dbContext.ExamTasks.AsNoTracking()
            .Include(x => x.Subject)
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<ExamTaskEntity?, ExamTask?>(result);
    }

    public async Task<IReadOnlyCollection<ExamTask>> GetAllAsync()
    {
        List<ExamTaskEntity> result = await _dbContext.ExamTasks.AsNoTracking()
            .Include(x => x.Subject)
            .Include(x => x.Author)
            .ToListAsync();

        return _mapper.Map<List<ExamTaskEntity>, List<ExamTask>>(result).AsReadOnly();
    }

    public async Task UpdateAsync(int id, ExamTaskUpdatingModel updatingModel)
    {
        ExamTaskEntity? objectToUpdate = await _dbContext.ExamTasks.AsNoTracking()
            .Include(x => x.Subject)
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (objectToUpdate is null)
        {
            return;
        }

        ChangeExamTaskOnUpdating(objectToUpdate, updatingModel);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _dbContext.ExamTasks.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

    private static void ChangeExamTaskOnUpdating(ExamTaskEntity objectToUpdate,
        ExamTaskUpdatingModel updatingModel)
    {
        objectToUpdate.Data = updatingModel.Data;
        objectToUpdate.Prototype = updatingModel.Prototype;
        objectToUpdate.DatesOfUpdating.Add(DateTime.UtcNow);
    }
}