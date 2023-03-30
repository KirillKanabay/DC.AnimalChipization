using DC.AnimalChipization.Data.Common.Filters;
using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Data.Repositories.Filters;

public class AnimalLocationFilter : FilterBase<AnimalLocationEntity>
{
    public long? Id { get; set; }
    
    public long? AnimalId { get; set; }

    public long? LocationPointId { get; set; }
    
    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }
}