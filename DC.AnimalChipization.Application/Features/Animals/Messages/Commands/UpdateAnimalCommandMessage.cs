using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Features.Animals.Enums;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Messages.Commands;

[Authorize]
public class UpdateAnimalCommandMessage : IRequest<AnimalDto>
{
    public long AnimalId { get; set; }
    public float Weight { get; set; }
    public float Length { get; set; }
    public float Height { get; set; }
    public Gender Gender { get; set; }
    public LifeStatus LifeStatus { get; set; }
    public int ChipperId { get; set; }
    public long ChippingLocationId { get; set; }
}