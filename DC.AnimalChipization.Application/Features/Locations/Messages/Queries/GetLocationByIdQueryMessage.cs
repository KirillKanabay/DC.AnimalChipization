using DC.AnimalChipization.Application.Features.Locations.DataTransfer;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Locations.Messages.Queries
{
    public class GetLocationByIdQueryMessage : IRequest<LocationDto>
    {
        public long PointId { get; set; }
    }
}
