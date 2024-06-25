using Application.Dto;
using Core.Models;

namespace Application.Abstract;

public interface ISchoolService
{
    Task CreateSchoolAsync(CreateSchoolRequestDto request);
    Task JoinSchoolAsync(JoinSchoolRequestDto request);
    Task<School?> GetByUserIdAsync(int userId);
    Task LeaveSchool(int userId);
    Task UpdateSchool(UpdateSchoolRequestDto request);
    Task DeleteSchool(DeleteSchoolRequestDto request);

    Task CreatePost(CreatePostRequestDto request);
    Task EditPost(EditPostRequestDto request);
    Task DeletePost(DeletePostRequestDto request);
    
}