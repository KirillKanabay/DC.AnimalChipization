using MediatR;

namespace DC.AnimalChipization.Application.Features.Locations.Messages.Queries;

public class GetIdByPointQueryMessage : IRequest<long>
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}