using AutoMapper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Abstract;
using Persistence.Entities;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext, IMapper mapper, ILogger<UserRepository> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    private readonly ILogger<UserRepository> _logger;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _dbContext;
    
    public async Task AddAsync(User user)
    {
        UserEntity userEntity = _mapper.Map<User, UserEntity>(user);
        
        _dbContext.Users.Add(userEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetByLoginAsync(string email)
    {
        UserEntity? userEntity = await _dbContext.Users.AsNoTracking()
            .Include(x => x.Teacher)
            .Include(x => x.Student)
            .FirstOrDefaultAsync(x => x.Email == email);
        
        return _mapper.Map<UserEntity?, User?>(userEntity);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        UserEntity? userEntity = await _dbContext.Users.AsNoTracking()
            .Include(x => x.Student)
            .ThenInclude(x => x.School)
            .Include(x => x.Teacher)
            .ThenInclude(x => x.School)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return _mapper.Map<UserEntity?, User?>(userEntity);
    }

    public async Task<bool> ContainsByIdAsync(int id)
    {
        return await _dbContext.Users.AsNoTracking()
            .AnyAsync(x => x.Id == id);
    }
    
    public async Task<bool> ContainsByLoginAsync(string email)
    {
        return await _dbContext.Users.AsNoTracking()
            .AnyAsync(x => x.Email == email);
    }
}