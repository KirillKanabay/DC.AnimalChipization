using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Messages.Queries;

public class GetAnimalByIdQueryMessage : IRequest<AnimalDto>
{
    public long AnimalId { get; set; }
}