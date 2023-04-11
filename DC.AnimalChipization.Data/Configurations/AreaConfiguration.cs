using DC.AnimalChipization.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DC.AnimalChipization.Data.Configurations;

public class AreaConfiguration : IEntityTypeConfiguration<AreaEntity>
{
    public void Configure(EntityTypeBuilder<AreaEntity> builder)
    {
        builder.HasMany(x => x.AreaPoints).WithOne(x => x.Area).HasForeignKey(x => x.Area);
    }
}