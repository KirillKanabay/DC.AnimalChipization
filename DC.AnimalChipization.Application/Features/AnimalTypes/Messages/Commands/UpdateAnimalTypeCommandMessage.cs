using DC.AnimalChipization.Application.Features.AnimalTypes.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;

[Authorize]
public class UpdateAnimalTypeCommandMessage : IRequest<AnimalTypeDto>
{
    public long AnimalTypeId { get; set; }
    public string Type { get; set; }
}