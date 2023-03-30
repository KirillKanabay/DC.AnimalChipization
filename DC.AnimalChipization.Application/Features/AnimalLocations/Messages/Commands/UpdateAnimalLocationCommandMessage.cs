using DC.AnimalChipization.Application.Features.AnimalLocations.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands;

[Authorize]
public class UpdateAnimalLocationCommandMessage : IRequest<AnimalLocationDto>
{
    public long AnimalId { get; set; }
    public long VisitedLocationPointId { get; set; }
    public long LocationPointId { get; set; }
}