using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Locations.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Locations.Handlers.Commands;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommandMessage>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLocationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(DeleteLocationCommandMessage request, CancellationToken cancellationToken)
    {
        var locationEntity = await _unitOfWork.Locations.GetByIdAsync(request.PointId);

        if (locationEntity == null)
        {
            throw new NotFoundException();
        }

        if (!await IsDeleteAllowed(request.PointId))
        {
            throw new ValidationException();
        }

        await _unitOfWork.Locations.DeleteAsync(locationEntity);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }

    private async Task<bool> IsDeleteAllowed(long pointId)
    {
        var animals = await _unitOfWork.Animals.ListAsync(new AnimalFilter
        {
            ChippingLocationId = pointId
        }, null);

        if (animals?.Any() ?? false)
        {
            return false;
        }

        var visitedLocations = await _unitOfWork.AnimalLocations.ListAsync(new AnimalLocationFilter
        {
            LocationPointId = pointId
        }, null);

        if (visitedLocations?.Any() ?? false)
        {
            return false;
        }

        return true;
    }
}