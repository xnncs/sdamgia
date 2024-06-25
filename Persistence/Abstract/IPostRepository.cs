using Core.Models;

namespace Persistence.Abstract;

public interface IPostRepository
{
    public Task AddAsync(int schoolId, Post postData);
    public Task UpdateAsync(string postData, int postId);
    public Task DeleteAsync(int postId);
}