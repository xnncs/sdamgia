using Core.Models;

namespace Persistence.Abstract;

public interface IPostRepository
{
    Task AddAsync(int schoolId, Post postData);
    Task UpdateAsync(string postData, int postId);
    Task DeleteAsync(int postId);
}