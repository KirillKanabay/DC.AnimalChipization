using DC.AnimalChipization.Application.Features.AnimalLocations.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands
{
    [Authorize]
    public class AddAnimalLocationCommandMessage : IRequest<AnimalLocationDto>
    {
        public long AnimalId { get; set; }
        public long PointId { get; set; }
    }
}
