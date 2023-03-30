using DC.AnimalChipization.Application.Identity.Attributes;

namespace DC.AnimalChipization.Application.Features.Locations.Messages.Commands;

[Authorize]
public class UpdateLocationCommandMessage : ChangeLocationCommandMessageBase
{
    public long PointId { get; set; }
}