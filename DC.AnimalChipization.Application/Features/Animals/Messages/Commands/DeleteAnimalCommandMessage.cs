using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Messages.Commands;

[Authorize]
public class DeleteAnimalCommandMessage : IRequest
{
    public long AnimalId { get; set; }
}