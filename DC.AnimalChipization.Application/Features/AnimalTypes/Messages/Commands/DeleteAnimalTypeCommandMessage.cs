using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;

[Authorize]
public class DeleteAnimalTypeCommandMessage : IRequest
{
    public long AnimalTypeId { get; set; }
}