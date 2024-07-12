using Core.StaticInfoModels;
using Persistence.Models;

namespace Persistence.Abstract;

public interface ISubjectRepository
{
    Task<IReadOnlyCollection<Subject>> GetAllAsync();
    Task AddAsync(Subject subject);
    Task UpdateAsync(int objectToUpdateId, SubjectUpdatingModel model);
    Task<bool> ContainsByNameAsync(string name);
    Task<bool> ContainsByIdAsync(int id);
}