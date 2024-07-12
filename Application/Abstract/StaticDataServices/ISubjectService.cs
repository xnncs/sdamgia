using Application.Dto.Subject;
using Core.StaticInfoModels;

namespace Application.Abstract.StaticDataServices;

public interface ISubjectService
{
    Task<IReadOnlyCollection<Subject>> GetAllAsync();
    Task CreateAsync(CreateSubjectDto request);
    Task UpdateAsync(UpdateSubjectDto request);
    Task DeleteAsync(int objectToDeleteId);
}