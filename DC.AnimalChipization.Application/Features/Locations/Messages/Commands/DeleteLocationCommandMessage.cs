using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Locations.Messages.Commands;

[Authorize]
public class DeleteLocationCommandMessage : IRequest
{
    public long PointId { get; set; }
}