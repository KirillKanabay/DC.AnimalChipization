using DC.AnimalChipization.Application.Common.Messages;
using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Features.Animals.Enums;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Messages.Queries;

public class AnimalSearchQueryMessage : PagedMessage, IRequest<List<AnimalDto>>
{
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public int? ChipperId { get; set; }
    public long? ChippingLocationId { get; set; }
    public LifeStatus? LifeStatus { get; set; }
    public Gender? Gender { get; set; }
}