using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class ExamOptionEntityConfiguration: IEntityTypeConfiguration<ExamOptionEntity>
{
    public void Configure(EntityTypeBuilder<ExamOptionEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasMany(x => x.ExamTasks)
            .WithOne();

        builder.HasOne(x => x.Subject)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);
    }
}