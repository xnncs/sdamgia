using AutoMapper;
using Core.StaticInfoModels;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstract;
using Persistence.Entities;

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

    public async Task AddAsync(Subject subject)
    {
        SubjectEntity objectToAdd = _mapper.Map<Subject, SubjectEntity>(subject);

        _dbContext.Subjects.Add(objectToAdd);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ContainsByNameAsync(string name)
    {
        return await _dbContext.Subjects.AnyAsync(x => x.Name == name);
    }
}