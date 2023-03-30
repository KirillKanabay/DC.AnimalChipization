using DC.AnimalChipization.Data.Common.Entities;

namespace DC.AnimalChipization.Data.Entities;

public class AnimalEntity : EntityBase
{
    public long Id { get; set; }
    public float Weight { get; set; }
    public float Height { get; set; }
    public float Length { get; set; }
    public int Gender { get; set; }
    public int LifeStatus { get; set; } 
    public DateTime ChippingDateTime { get; set; }
    public int ChipperId { get; set; }
    public long ChippingLocationId { get; set; }
    public DateTime? DeathDateTime { get; set; }

    public LocationEntity ChippingLocation { get; set; }
    public AccountEntity Chipper { get; set; }
    public List<AnimalTypeEntity> AnimalTypes { get; set; }
    public List<AnimalLocationEntity> VisitedLocations { get; set; }
}