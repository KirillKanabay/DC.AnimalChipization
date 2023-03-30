using DC.AnimalChipization.Data.Common.Entities;

namespace DC.AnimalChipization.Data.Entities;

public class LocationEntity : EntityBase
{
    public long Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public ICollection<AnimalEntity> ChippedAnimals { get; set; }
    public ICollection<AnimalLocationEntity> AnimalVisitedLocations { get; set; }
}