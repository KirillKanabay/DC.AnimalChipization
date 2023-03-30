using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Messages.Commands;

[Authorize]
public class ChangeAnimalTypeCommandMessage : IRequest<AnimalDto>
{
    public long AnimalId { get; set; }
    public long OldTypeId { get; set;}
    public long NewTypeId { get; set;}
}