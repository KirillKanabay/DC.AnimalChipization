using DC.AnimalChipization.Application.Features.AnimalTypes.DataTransfer;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Queries;

public class GetAnimalTypeByIdQueryMessage : IRequest<AnimalTypeDto>
{
    public long AnimalTypeId { get; set; }
}