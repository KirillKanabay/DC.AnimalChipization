using DC.AnimalChipization.Data.Common.Filters;
using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Data.Repositories.Filters;

public class LocationFilter : FilterBase<LocationEntity>
{
    public long? Id { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}