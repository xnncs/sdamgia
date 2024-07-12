using Application.Abstract;
using Application.Dto.ExamTask;
using Core.Models;
using Persistence.Abstract;

namespace Application.Services;

public class ExamTaskService : IExamTaskService
{
    public ExamTaskService(ISchoolPermissionsHelper permissionsHelper, ITeacherRepository teacherRepository,
        IStudentRepository studentRepository, IExamTaskRepository examTaskRepository)
    {
        _permissionsHelper = permissionsHelper;

        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;

        _examTaskRepository = examTaskRepository;
    }

    private readonly IExamTaskRepository _examTaskRepository;
    
    private readonly ITeacherRepository _teacherRepository;
    private readonly IStudentRepository _studentRepository;
    
    private readonly ISchoolPermissionsHelper _permissionsHelper;
    
    public async Task CreateExamTaskAsync(CreateExamTaskDto request)
    {
        await _permissionsHelper.CheckTeacherPermissionsAsync(request.UserId);

        Teacher author = (await _teacherRepository.GetByUserIdAsync(request.UserId))!;
        ExamTask examTask = GenerateExamTaskAndModifyTeacherObjects(request, author);

        await _examTaskRepository.AddAsync(examTask);
    }

    private ExamTask GenerateExamTaskAndModifyTeacherObjects(CreateExamTaskDto request, Teacher author)
    {
        ExamTask examTask = ExamTask.Create(request.Data, request.Subject, request.Prototype, author);

        return examTask;
    }
}