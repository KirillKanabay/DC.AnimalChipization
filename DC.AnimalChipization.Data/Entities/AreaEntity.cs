using DC.AnimalChipization.Data.Common.Entities;

namespace DC.AnimalChipization.Data.Entities;

public class AreaEntity : EntityBase
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public List<AreaPointEntity> AreaPoints { get; set; }
}