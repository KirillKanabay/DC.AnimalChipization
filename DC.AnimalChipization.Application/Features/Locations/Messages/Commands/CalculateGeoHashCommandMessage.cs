using DC.AnimalChipization.Application.Features.Locations.Enums;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Locations.Messages.Commands;

public class CalculateGeoHashCommandMessage : IRequest<string>
{
    public GeoHashVersion Version { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}