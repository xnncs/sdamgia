using Application.Dto;
using Application.Dto.Post;
using Application.Dto.School;
using Core.Models;

namespace Application.Abstract;

public interface ISchoolService
{
    Task CreateSchoolAsync(CreateSchoolDto request);
    Task JoinSchoolAsync(JoinSchoolDto request);
    Task<School?> GetByUserIdAsync(int userId);
    Task LeaveSchool(int userId);
    Task UpdateSchool(UpdateSchoolDto request);
    Task DeleteSchool(DeleteSchoolDto request);

    Task CreatePost(CreatePostDto request);
    Task UpdatePost(EditPostDto request);
    Task DeletePost(DeletePostDto request);
    
}