using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstract;
using Persistence.Entities;

namespace Persistence.Repositories;

public class TeacherRepository : ITeacherRepository
{
    public TeacherRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public async Task AddAsync(Teacher teacher)
    {
        TeacherEntity teacherEntity = _mapper.Map<Teacher, TeacherEntity>(teacher);

        _dbContext.Teachers.Add(teacherEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsTeacherByUserIdAsync(int userId)
    {
        return await _dbContext.Teachers.AnyAsync(x => x.UserId == userId);
    }

    public async Task<Teacher?> GetByUserIdAsync(int userId)
    {
        TeacherEntity? teacherEntity = await _dbContext.Teachers.AsNoTracking()
            .Include(x => x.School)
            .Include(x => x.User)
            .Include(x => x.ExamTasksCreated)
            .FirstOrDefaultAsync(x => x.UserId == userId);
        
        return _mapper.Map<TeacherEntity?, Teacher?>(teacherEntity);
    }

    public async Task<bool> HasSchoolByIdAsync(int teacherId)
    {
        return await _dbContext.Schools.AsNoTracking()
            .Include(x => x.Author)
            .AnyAsync(x => x.Author.Id == teacherId);
    }

    public async Task<int?> GetTeacherIdByUserIdAsync(int userId)
    {
        TeacherEntity teacherEntity = await _dbContext.Teachers.AsNoTracking()
                                          .FirstOrDefaultAsync(x => x.UserId == userId)
                                      ?? throw new Exception("No such teachers with that userId");
        return teacherEntity.Id;
    }
}