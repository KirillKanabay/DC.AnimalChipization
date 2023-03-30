using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands;

[Authorize]
public class DeleteAnimalLocationCommandMessage : IRequest
{
    public long AnimalId { get; set; }
    public long VisitedPointId { get; set; }
}