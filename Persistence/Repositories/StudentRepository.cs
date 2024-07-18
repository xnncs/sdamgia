using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstract;
using Persistence.Database;
using Persistence.Entities;

namespace Persistence.Repositories;

public class StudentRepository : IStudentRepository
{
    public StudentRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public async Task AddAsync(Student student)
    {
        StudentEntity studentEntity = _mapper.Map<Student, StudentEntity>(student);

        _dbContext.Students.Add(studentEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsStudentByUserIdAsync(int userId)
    {
        return await _dbContext.Students.AnyAsync(x => x.UserId == userId);
    }

    public async Task<Student?> GetByUserIdAsync(int userId)
    {
        StudentEntity? student = await _dbContext.Students.AsNoTracking()
            .Include(x => x.School)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        return _mapper.Map<StudentEntity?, Student?>(student);
    }

    public async Task<int?> GetStudentIdByUserIdAsync(int userId)
    {
        UserEntity? userEntity = await _dbContext.Users
            .Include(x => x.Student)
            .FirstOrDefaultAsync(x => x.Id == userId);
        
        if (userEntity?.Student == null)
        {
            return null;
        }

        return userEntity?.Student!.Id;
    }

    public async Task<bool> HasSchoolBylIdAsync(int studentId)
    {
        StudentEntity? studentEntity = await _dbContext.Students
            .Include(x => x.School)
            .FirstOrDefaultAsync(x => x.Id == studentId);
        if (studentEntity == null)
        {
            return false;
        }

        return studentEntity.School == null;
    }
}