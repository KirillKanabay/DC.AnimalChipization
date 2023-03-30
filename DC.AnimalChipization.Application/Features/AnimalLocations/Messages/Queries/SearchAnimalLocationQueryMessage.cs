using DC.AnimalChipization.Application.Common.Messages;
using DC.AnimalChipization.Application.Features.AnimalLocations.DataTransfer;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Queries
{
    public class SearchAnimalLocationQueryMessage : PagedMessage, IRequest<List<AnimalLocationDto>>
    {
        public long AnimalId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
