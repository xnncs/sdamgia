using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using Persistence.Entities;

namespace Persistence.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<ExamOptionEntity> ExamOptions { get; set; }
    public DbSet<ExamTaskEntity> ExamTasks { get; set; }

    public DbSet<PageEntity> Pages { get; set; }
    public DbSet<PostEntity> Posts { get; set; }

    public DbSet<TeacherEntity> Teachers { get; set; }
    public DbSet<StudentEntity> Students { get; set; }

    public DbSet<SchoolEntity> Schools { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    
    
    public DbSet<SubjectEntity> Subjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        
        modelBuilder.ApplyConfiguration(new SchoolEntityConfiguration());
        
        modelBuilder.ApplyConfiguration(new PageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
        
        modelBuilder.ApplyConfiguration(new ExamOptionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ExamTaskEntityConfiguration());

        modelBuilder.ApplyConfiguration(new TeacherEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StudentEntityConfiguration());

        modelBuilder.ApplyConfiguration(new SubjectEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}