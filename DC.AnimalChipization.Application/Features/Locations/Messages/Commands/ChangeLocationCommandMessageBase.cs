using DC.AnimalChipization.Application.Features.Locations.DataTransfer;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Locations.Messages.Commands;

public abstract class ChangeLocationCommandMessageBase : IRequest<LocationDto>
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}