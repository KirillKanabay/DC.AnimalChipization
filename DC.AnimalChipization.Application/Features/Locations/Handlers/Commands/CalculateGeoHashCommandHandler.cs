using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Locations.Enums;
using DC.AnimalChipization.Application.Features.Locations.Messages.Commands;
using DC.AnimalChipization.Application.Features.Locations.Services.Contracts;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Locations.Handlers.Commands;

public class CalculateGeoHashCommandHandler : IRequestHandler<CalculateGeoHashCommandMessage, string>
{
    private readonly IGeoHashService _geoHashService;
    private readonly IUnitOfWork _unitOfWork;

    public CalculateGeoHashCommandHandler(IGeoHashService geoHashService, IUnitOfWork unitOfWork)
    {
        _geoHashService = geoHashService ?? throw new ArgumentNullException(nameof(geoHashService));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<string> Handle(CalculateGeoHashCommandMessage request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Locations.FirstOrDefaultAsync(new LocationFilter
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude
        });

        if (location == null)
        {
            throw new NotFoundException();
        }

        var result = request.Version switch
        {
            GeoHashVersion.V1 => _geoHashService.GetGeoHash(location.Latitude, location.Longitude),
            GeoHashVersion.V2 => _geoHashService.GetGeoHashV2(location.Latitude, location.Longitude),
            _                 => throw new ValidationException()
        };

        return result;
    }
}