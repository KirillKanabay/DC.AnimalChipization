using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Locations.Messages.Queries;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Locations.Handlers.Queries;

public class GetIdByPointQueryHandler : IRequestHandler<GetIdByPointQueryMessage, long>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetIdByPointQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new NullReferenceException(nameof(unitOfWork));
    }

    public async Task<long> Handle(GetIdByPointQueryMessage request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Locations.FirstOrDefaultAsync(new LocationFilter
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        });

        if (location == null)
        {
            throw new NotFoundException();
        }

        return location.Id;
    }
}