using DC.AnimalChipization.Application.Features.AnimalTypes.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;

[Authorize]
public class AddAnimalTypeCommandMessage : IRequest<AnimalTypeDto>
{
    public string Type { get; set; }
}