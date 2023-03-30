using DC.AnimalChipization.Data.Common.Filters;
using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Data.Repositories.Filters;

public class AnimalFilter : FilterBase<AnimalEntity>
{
    public long? Id { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public int? ChipperId { get; set; }
    public long? ChippingLocationId { get; set; }
    public int? LifeStatus { get; set; }
    public int? Gender { get; set; }
}