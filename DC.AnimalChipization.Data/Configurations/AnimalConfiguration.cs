using DC.AnimalChipization.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DC.AnimalChipization.Data.Configurations;

public class AnimalConfiguration : IEntityTypeConfiguration<AnimalEntity>
{
    public void Configure(EntityTypeBuilder<AnimalEntity> builder)
    {
        builder.HasOne(x => x.Chipper)
            .WithMany(x => x.ChippedAnimals)
            .HasForeignKey(x => x.ChipperId);

        builder.HasOne(x => x.ChippingLocation)
            .WithMany(x => x.ChippedAnimals)
            .HasForeignKey(x => x.ChippingLocationId);
        
        builder.HasMany(x => x.AnimalTypes)
            .WithMany(x => x.Animals)
            .UsingEntity(j => j.ToTable("Animal_AnimalType"));
    }
}