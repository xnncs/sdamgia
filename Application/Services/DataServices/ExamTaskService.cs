using Application.Abstract;
using Application.Dto.ExamTask;
using AutoMapper;
using Core.Models;
using Core.StaticInfoModels;
using Persistence.Abstract;
using Persistence.Models;

namespace Application.Services.DataServices;

public class ExamTaskService : IExamTaskService
{
    public ExamTaskService(ISchoolPermissionsHelper permissionsHelper, ITeacherRepository teacherRepository,
        IStudentRepository studentRepository, IExamTaskRepository examTaskRepository, ISubjectRepository subjectRepository, IMapper mapper)
    {
        _permissionsHelper = permissionsHelper;

        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;

        _examTaskRepository = examTaskRepository;
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    private readonly IExamTaskRepository _examTaskRepository;
    
    private readonly ITeacherRepository _teacherRepository;
    private readonly IStudentRepository _studentRepository;
    
    private readonly ISchoolPermissionsHelper _permissionsHelper;

    private readonly ISubjectRepository _subjectRepository;

    private readonly IMapper _mapper;
        
    
    public async Task CreateExamTaskAsync(CreateExamTaskDto request)
    {
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

    public async Task<IReadOnlyCollection<ExamTask>> GetAllAsync()
    {
        return await _examTaskRepository.GetAllAsync();
    }

    public async Task<ExamTask?> GetByIdAsync(int id)
    {
        return await _examTaskRepository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(UpdateExamTaskDto request)
    {
        ExamTask previous = await _examTaskRepository.GetByIdAsync(request.Id)
                            ?? throw new Exception("No such a exam task with this id");
        
        if (!await _subjectRepository.ContainsPrototypeAsync(
                prototype: request.Prototype,
                subjectId: previous.Subject.Id!.Value))
        {
            throw new Exception("This subject has no such a prototype, that you requested");
        }
        
        Teacher author = (await _teacherRepository.GetByUserIdAsync(request.ClientId))!;

        if (previous.Author.Id != author.Id)
        {
            throw new Exception("You have no author permissions to update this exam task");
        }

        ExamTaskUpdatingModel updatingModel = _mapper.Map<UpdateExamTaskDto, ExamTaskUpdatingModel>(request);

        await _examTaskRepository.UpdateAsync(request.Id, updatingModel);
    }

    public async Task DeleteAsync(DeleteExamTaskDto request)
    {
        ExamTask previous = await _examTaskRepository.GetByIdAsync(request.ExamTaskId)
                            ?? throw new Exception("No such a exam task with this id");
        
        Teacher author = (await _teacherRepository.GetByUserIdAsync(request.ClientId))!;

        if (previous.Author.Id != author.Id)
        {
            throw new Exception("You have no author permissions to update this exam task");
        }

        await _examTaskRepository.DeleteAsync(request.ExamTaskId);
    }

    private ExamTask GenerateExamTaskAndModifyTeacherObjects(CreateExamTaskDto request, Teacher author, Subject subject)
    {
        ExamTask examTask = ExamTask.Create(request.Data, subject, request.Prototype, author);

        return examTask;
    }
}