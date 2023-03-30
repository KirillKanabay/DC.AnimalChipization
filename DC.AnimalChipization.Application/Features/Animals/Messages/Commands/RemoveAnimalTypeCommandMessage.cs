using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Messages.Commands;

[Authorize]
public class RemoveAnimalTypeCommandMessage : IRequest<AnimalDto>
{
    public long AnimalId { get; set; }
    public long TypeId { get; set; }
}