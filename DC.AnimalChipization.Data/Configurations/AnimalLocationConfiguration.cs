using DC.AnimalChipization.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DC.AnimalChipization.Data.Configurations;

public class AnimalLocationConfiguration : IEntityTypeConfiguration<AnimalLocationEntity>
{
    public void Configure(EntityTypeBuilder<AnimalLocationEntity> builder)
    {
        builder.HasOne(x => x.Location)
            .WithMany(x => x.AnimalVisitedLocations)
            .HasForeignKey(x => x.LocationPointId);

        builder.HasOne(x => x.Animal)
            .WithMany(x => x.VisitedLocations)
            .HasForeignKey(x => x.AnimalId);
    }
}