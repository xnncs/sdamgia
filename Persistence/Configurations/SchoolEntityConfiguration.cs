using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class SchoolEntityConfiguration : IEntityTypeConfiguration<SchoolEntity>
{
    public void Configure(EntityTypeBuilder<SchoolEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(s => s.Page)
            .WithOne(p => p.School)
            .HasPrincipalKey<SchoolEntity>(s => s.Id)
            .HasForeignKey<PageEntity>(p => p.SchoolId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Students)
            .WithOne(x => x.School)
            .HasForeignKey(x => x.SchoolId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(s => s.Author)
            .WithOne(t => t.School)
            .HasPrincipalKey<SchoolEntity>(x => x.Id)
            .HasForeignKey<TeacherEntity>(x => x.SchoolId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}