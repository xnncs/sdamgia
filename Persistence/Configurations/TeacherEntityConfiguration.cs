using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class TeacherEntityConfiguration : IEntityTypeConfiguration<TeacherEntity>
{
    public void Configure(EntityTypeBuilder<TeacherEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(x => x.User)
            .WithOne(x => x.Teacher)
            .HasPrincipalKey<UserEntity>(x => x.Id)
            .HasForeignKey<TeacherEntity>(x => x.UserId);
        
        builder.HasMany(x => x.ExamTasksCreated)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId)
            .HasPrincipalKey(x => x.Id);
    }
}