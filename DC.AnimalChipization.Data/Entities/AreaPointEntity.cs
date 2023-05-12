using DC.AnimalChipization.Data.Common.Entities;

namespace DC.AnimalChipization.Data.Entities;

public class AreaPointEntity : EntityBase
{
    public long Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public long AreaId { get; set; }

    public AreaEntity Area { get; set; }
}