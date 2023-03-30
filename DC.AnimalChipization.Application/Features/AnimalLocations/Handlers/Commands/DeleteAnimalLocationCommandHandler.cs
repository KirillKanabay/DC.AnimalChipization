using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Handlers.Commands;

public class DeleteAnimalLocationCommandHandler : IRequestHandler<DeleteAnimalLocationCommandMessage>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAnimalLocationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(DeleteAnimalLocationCommandMessage request, CancellationToken cancellationToken)
    {
        var animalLocationEntity = await _unitOfWork.AnimalLocations.GetByIdAsync(request.VisitedPointId);

        if (animalLocationEntity == null)
        {
            throw new NotFoundException();
        }

        var animalFilter = new AnimalFilter
        {
            Id = request.AnimalId
        };
        animalFilter.Include(x => x.VisitedLocations.OrderBy(vl => vl.VisitDateTime));

        var animalEntity = await _unitOfWork.Animals.FirstOrDefaultAsync(animalFilter);

        if (!animalEntity?.VisitedLocations?.Any(x => x.Id == animalLocationEntity.Id) ?? true)
        {
            throw new NotFoundException();
        }

        var locationsForDelete = GetLocationsForDelete(animalLocationEntity, animalEntity);

        foreach (var location in locationsForDelete)
        {
            await _unitOfWork.AnimalLocations.DeleteAsync(location);
        }

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }

    private List<AnimalLocationEntity> GetLocationsForDelete(AnimalLocationEntity animalLocation, AnimalEntity animal)
    {
        var visitedLocations = animal.VisitedLocations;
        var firstVisitedLocation = visitedLocations.First();

        if (firstVisitedLocation.Id != animalLocation.Id)
        {
            return new List<AnimalLocationEntity> { animalLocation };
        }

        var secondVisitedLocation = visitedLocations.ElementAtOrDefault(1);

        if (secondVisitedLocation != null && secondVisitedLocation.LocationPointId == animal.ChippingLocationId)
        {
            return new List<AnimalLocationEntity> { animalLocation, secondVisitedLocation };
        }

        return new List<AnimalLocationEntity> { animalLocation };
    }
}