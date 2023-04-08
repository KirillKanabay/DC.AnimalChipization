using DC.AnimalChipization.Data.Common.Entities;

namespace DC.AnimalChipization.Data.Entities;

public class AnimalTypeEntity : EntityBase
{
    public long Id { get; set; }
    public string Type { get; set; }
    public ICollection<AnimalEntity> Animals { get; set; }
}