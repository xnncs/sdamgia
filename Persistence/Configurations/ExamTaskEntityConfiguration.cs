using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class ExamTaskEntityConfiguration : IEntityTypeConfiguration<ExamTaskEntity>
{
    public void Configure(EntityTypeBuilder<ExamTaskEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}