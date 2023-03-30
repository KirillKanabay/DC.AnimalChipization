using DC.AnimalChipization.Application.Identity.Attributes;

namespace DC.AnimalChipization.Application.Features.Locations.Messages.Commands;

[Authorize]
public class AddLocationCommandMessage : ChangeLocationCommandMessageBase
{
}