using Application.Abstract;
using Application.Dto.ExamTask;
using Core.Models;
using Core.StaticInfoModels;
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

    private readonly ISubjectRepository _subjectRepository;
    
    public async Task CreateExamTaskAsync(CreateExamTaskDto request)
    {
        await _permissionsHelper.CheckTeacherPermissionsAsync(request.UserId);

        if (!await _subjectRepository.ContainsPrototypeAsync(
                prototype: request.Prototype,
                subjectId: request.SubjectId))
        {
            throw new Exception("This subject has no such a prototype, that you requested");
        }
        
        Teacher author = (await _teacherRepository.GetByUserIdAsync(request.UserId))!;
        
        Subject subject = await _subjectRepository.GetByIdAsync(request.SubjectId)
            ?? throw new Exception("No such a subject with this name");
        
        
        ExamTask examTask = GenerateExamTaskAndModifyTeacherObjects(request, author, subject);

        await _examTaskRepository.AddAsync(examTask);
    }

    private ExamTask GenerateExamTaskAndModifyTeacherObjects(CreateExamTaskDto request, Teacher author, Subject subject)
    {
        ExamTask examTask = ExamTask.Create(request.Data, subject, request.Prototype, author);

        return examTask;
    }
}