using Application.Dto;
using Application.Dto.Post;
using Application.Dto.School;
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
    Task UpdatePost(EditPostRequestDto request);
    Task DeletePost(DeletePostRequestDto request);
    
}