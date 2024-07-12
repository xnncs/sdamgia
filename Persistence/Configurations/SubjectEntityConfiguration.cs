using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class SubjectEntityConfiguration : IEntityTypeConfiguration<SubjectEntity>
{
    public void Configure(EntityTypeBuilder<SubjectEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasData(new List<SubjectEntity>
        {
            new SubjectEntity
            {
                Id = 1,
                Name = "English",
                Prototypes = new List<string>
                {
                    "1", "2", "3", "...", "34", "35"
                },
                DateOfCreating = DateTime.UtcNow
            }
        });
    }
}