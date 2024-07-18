using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstract;
using Persistence.Database;
using Persistence.Entities;
using Persistence.Models;

namespace Persistence.Repositories;

public class SchoolRepository : ISchoolRepository
{
    public SchoolRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public async Task AddSchoolAsync(School school)
    {
        SchoolEntity objectToAdd = _mapper.Map<School, SchoolEntity>(school);

        _dbContext.Teachers.Attach(objectToAdd.Author);
        _dbContext.Subjects.Attach(objectToAdd.Subject);
        
        _dbContext.Schools.Add(objectToAdd);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<School?> GetSchoolByIdAsync(int id)
    {
        SchoolEntity? schoolEntity =  await _dbContext.Schools.AsNoTracking()
            .Include(x => x.Subject)
            .Include(x => x.Author)
            .Include(x => x.Students)
            .Include(x => x.Page)
            .ThenInclude(x => x.Posts)
            .FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<SchoolEntity?, School?>(schoolEntity);
    }

    public async Task<School?> GetSchoolByTeacherIdAsync(int teacherId)
    {
        TeacherEntity? teacher = await _dbContext.Teachers.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == teacherId);
        if (teacher == null)
        {
            return null;
        }

        int schoolId = teacher.SchoolId!.Value;

        return await GetSchoolByIdAsync(schoolId);
    }

    public async Task<School?> GetSchoolByStudentIdAsync(int studentId)
    {
        StudentEntity? student = await _dbContext.Students.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == studentId);
        if (student == null)
        {
            return null;
        }

        int schoolId = student.SchoolId!.Value;

        return await GetSchoolByIdAsync(schoolId);
    }

    public async Task AddStudentToSchoolAsync(Student student, int schoolId)
    {
        StudentEntity studentEntity = _mapper.Map<Student, StudentEntity>(student);

        _dbContext.Students.Attach(studentEntity);

        SchoolEntity? schoolEntity = await _dbContext.Schools
            .Include(x => x.Students)
            .FirstOrDefaultAsync(x => x.Id == schoolId);
        if (studentEntity == null)
        {
            return;
        }
        
        ModifySchoolAndStudentOnJoin(studentEntity, schoolEntity!);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ContainsStudentByIdAsync(int schoolId, int studentId)
    {
        SchoolEntity? schoolEntity = await _dbContext.Schools.AsNoTracking()
            .Include(x => x.Students)
            .FirstOrDefaultAsync(x => x.Id == schoolId);

        if (schoolEntity == null)
        {
            return false;
        }
                                    
        
        return schoolEntity.Students.Any(x => x.Id == studentId);
    }

    public async Task LeaveSchoolAsync(int studentId)
    {
        StudentEntity? studentEntity = await _dbContext.Students
            .Include(x => x.School)
            .FirstOrDefaultAsync(x => x.Id == studentId);
        if (studentEntity == null)
        {
            return;
        }
        
        bool hasSchool = studentEntity.School != null;
        if (!hasSchool)
        {
            return;
        }
        
        ModifySchoolAndStudentOnLeft(studentEntity, studentEntity.School!);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(SchoolUpdatingModel data, int schoolId)
    {
        SchoolEntity? schoolEntity = await _dbContext.Schools
            .FirstOrDefaultAsync(x => x.Id == schoolId);
        if (schoolEntity == null)
        {
            return;
        }
                                    

        ModifySchoolOnUpdate(schoolEntity, data);

        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteByIdAsync(int schoolId)
    {
        await _dbContext.Schools
            .Where(x => x.Id == schoolId)
            .ExecuteDeleteAsync();
    }
    

    private static void ModifySchoolOnUpdate(SchoolEntity objectToUpdate, SchoolUpdatingModel data)
    {
        objectToUpdate.DatesOfUpdating.Add(DateTime.UtcNow);

        objectToUpdate.CourseName = data.CourseName;
        objectToUpdate.Description = data.Description;
    }

    private static void ModifySchoolAndStudentOnLeft(StudentEntity studentEntity, SchoolEntity schoolEntity)
    {
        bool isDeleted = schoolEntity.Students.Remove(studentEntity);
        if (!isDeleted)
        {
            return;
        }
        
        studentEntity.School = null;
    }
    
    private static void ModifySchoolAndStudentOnJoin(StudentEntity studentEntity, SchoolEntity schoolEntity)
    {
        studentEntity.School = schoolEntity;
        
        schoolEntity.Students.Add(studentEntity);
    }
}