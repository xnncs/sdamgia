using Application.Abstract.StaticDataServices;
using Application.Dto.Subject;
using Core.StaticInfoModels;
using Persistence.Abstract;

namespace Application.Services.StaticDataServices;

public class SubjectService : ISubjectService
{
    public SubjectService(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    private readonly ISubjectRepository _subjectRepository;

    
    public async Task<IReadOnlyCollection<Subject>> GetAllAsync()
    {
        return await _subjectRepository.GetAllAsync();
    }

    public async Task CreateAsync(CreateSubjectDto request)
    {
        if (await _subjectRepository.ContainsByNameAsync(request.Name))
        {
            throw new Exception("Subject with this name already exists");
        }
            
        Subject objectToAdd = Subject.Create(request.Name, request.Prototypes);

        await _subjectRepository.AddAsync(objectToAdd);
    }
}