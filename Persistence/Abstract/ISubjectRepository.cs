using Core.StaticInfoModels;

namespace Persistence.Abstract;

public interface ISubjectRepository
{
    Task<IReadOnlyCollection<Subject>> GetAllAsync();
    Task AddAsync(Subject subject);
    Task<bool> ContainsByNameAsync(string name);
}