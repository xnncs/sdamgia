using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstract;
using Persistence.Entities;

namespace Persistence.Repositories;

public class PostRepository : IPostRepository
{
    public PostRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public async Task AddAsync(int schoolId, Post postData)
    {
        PostEntity postEntity = _mapper.Map<Post, PostEntity>(postData);

        SchoolEntity school = await _dbContext.Schools
                            .Include(x => x.Page)
                            .ThenInclude(x => x.Posts)
                            .FirstOrDefaultAsync(x => x.Id == schoolId)
                        ?? throw new Exception("No such schools with that id");
        
        school.Page.Posts.Add(postEntity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(string postData, int postId)
    {
        PostEntity post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == postId)
                          ?? throw new Exception("No such posts with that id");

        ModifyPostOnUpdate(post, postData);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int postId)
    {
        await _dbContext.Posts
            .Where(x => x.Id == postId)
            .ExecuteDeleteAsync();
    }

    private void ModifyPostOnUpdate(PostEntity post, string newPostData)
    {
        post.Data = newPostData;
        post.DatesOfUpdating.Add(DateTime.UtcNow);
    }
}