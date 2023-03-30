using DC.AnimalChipization.Data.Common.Filters;
using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Data.Repositories.Filters;

public class AnimalTypeFilter : FilterBase<AnimalTypeEntity>
{
    public List<long> Ids { get; set; }
}