using Application.Abstract.StaticDataServices;
using Application.Dto.Subject;
using AutoMapper;
using Core.StaticInfoModels;
using Persistence.Abstract;
using Persistence.Models;

namespace Application.Services.StaticDataServices;

public class SubjectService : ISubjectService
{
    public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;
    
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

    public async Task UpdateAsync(UpdateSubjectDto request)
    {
        if (!await _subjectRepository.ContainsByIdAsync(request.ObjectToUpdateId))
        {
            throw new Exception("No such a subject with this id");
        }

        SubjectUpdatingModel model = _mapper.Map<UpdateSubjectDto, SubjectUpdatingModel>(request);
        await _subjectRepository.UpdateAsync(request.ObjectToUpdateId, model);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _subjectRepository.ContainsByIdAsync(id))
        {
            throw new Exception("No such a subject with this id");
        }

        await _subjectRepository.DeleteAsync(id);
    }
}