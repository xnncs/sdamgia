using AutoMapper;
using Core.StaticInfoModels;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstract;
using Persistence.Database;
using Persistence.Entities;
using Persistence.Models;

namespace Persistence.Repositories;

public class SubjectRepository : ISubjectRepository
{
    public SubjectRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public async Task<IReadOnlyCollection<Subject>> GetAllAsync()
    {
        List<SubjectEntity> subjects = await _dbContext.Subjects.AsNoTracking().ToListAsync();
        
        return subjects.Any() ?
            _mapper.Map<List<SubjectEntity>, List<Subject>>(subjects).AsReadOnly() : 
            Enumerable.Empty<Subject>().ToList().AsReadOnly();
    }

    public async Task<Subject?> GetByNameAsync(string name)
    {
        SubjectEntity? subject = await _dbContext.Subjects.AsNoTracking()
            .FirstOrDefaultAsync(x => 
                string.Equals(x.Name.ToLower(), name.ToLower(), StringComparison.Ordinal));

        return _mapper.Map<SubjectEntity?, Subject?>(subject);
    }

    public async Task<Subject?> GetByIdAsync(int id)
    {
        SubjectEntity? subject = await _dbContext.Subjects.AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        
        return _mapper.Map<SubjectEntity?, Subject?>(subject);
    }

    public async Task AddAsync(Subject subject)
    {
        SubjectEntity objectToAdd = _mapper.Map<Subject, SubjectEntity>(subject);

        _dbContext.Subjects.Add(objectToAdd);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int objectToUpdateId, SubjectUpdatingModel model)
    {
        SubjectEntity? objectToIUpdate = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == objectToUpdateId);
        if (objectToIUpdate == null)
        {
            return;
        }

        ChangeSubjectOnUpdating(objectToIUpdate, model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _dbContext.Subjects.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> ContainsByNameAsync(string name)
    {
        return await _dbContext.Subjects.AnyAsync(x => x.Name == name);
    }

    public async Task<bool> ContainsByIdAsync(int id)
    {
        return await _dbContext.Subjects.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> ContainsPrototypeAsync(string prototype, string subjectName)
    {
        SubjectEntity? subjectEntity = await _dbContext.Subjects.AsNoTracking()
            .FirstOrDefaultAsync(x =>
                string.Equals(x.Name.ToLower(), subjectName.ToLower(), StringComparison.Ordinal));
        
        if (subjectEntity == null)
        {
            return false;
        }

        return subjectEntity.Prototypes.Contains(prototype);
    }

    public async Task<bool> ContainsPrototypeAsync(string prototype, int subjectId)
    {
        SubjectEntity? subjectEntity = await _dbContext.Subjects.AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == subjectId);
        
        if (subjectEntity == null)
        {
            return false;
        }

        return subjectEntity.Prototypes.Contains(prototype);
    }
    

    private void ChangeSubjectOnUpdating(SubjectEntity subject, SubjectUpdatingModel updatingModel)
    {
        subject.Name = updatingModel.Name;
        subject.Prototypes = updatingModel.Prototypes;
        subject.DatesOfUpdating.Add(DateTime.UtcNow);
    }
}