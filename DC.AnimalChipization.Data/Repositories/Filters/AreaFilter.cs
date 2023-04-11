using DC.AnimalChipization.Data.Common.Filters;
using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Data.Repositories.Filters;

public class AreaFilter : FilterBase<AreaEntity>
{
    public long Id { get; set; }
}