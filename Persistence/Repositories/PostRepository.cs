using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstract;
using Persistence.Database;
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
        PostEntity objectToAdd = _mapper.Map<Post, PostEntity>(postData);

        SchoolEntity? school = await _dbContext.Schools
            .Include(x => x.Page)
            .ThenInclude(x => x.Posts)
            .FirstOrDefaultAsync(x => x.Id == schoolId);

        if (school == null)
        {
            return;
        }
        
        school.Page.Posts.Add(objectToAdd);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(string postData, int postId)
    {
        PostEntity? post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        if (post == null)
        {
            return;
        }
        

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