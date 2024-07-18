using Core.StaticInfoModels;
using Persistence.Models;

namespace Persistence.Abstract;

public interface ISubjectRepository
{
    Task<IReadOnlyCollection<Subject>> GetAllAsync();
    Task<Subject?> GetByNameAsync(string name);
    Task<Subject?> GetByIdAsync(int id);
    Task AddAsync(Subject subject);
    Task UpdateAsync(int objectToUpdateId, SubjectUpdatingModel model);
    Task DeleteAsync(int id);
    Task<bool> ContainsByNameAsync(string name);
    Task<bool> ContainsByIdAsync(int id);
    Task<bool> ContainsPrototypeAsync(string prototype, string subjectName);
    Task<bool> ContainsPrototypeAsync(string prototype, int subjectId);
}