using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.AnimalLocations.DataTransfer;
using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Handlers.Commands;

public class UpdateAnimalLocationCommandHandler : IRequestHandler<UpdateAnimalLocationCommandMessage, AnimalLocationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAnimalLocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AnimalLocationDto> Handle(UpdateAnimalLocationCommandMessage request, CancellationToken cancellationToken)
    {
        var animalLocationEntity = await _unitOfWork.AnimalLocations.GetByIdAsync(request.VisitedLocationPointId);

        if (animalLocationEntity == null)
        {
            throw new NotFoundException();
        }

        var animalFilter = new AnimalFilter
        {
            Id = request.AnimalId
        };
        animalFilter.Include(x => x.VisitedLocations.OrderBy(vl => vl.VisitDateTime));

        var animal = await _unitOfWork.Animals.FirstOrDefaultAsync(animalFilter);

        if (!animal?.VisitedLocations?.Any(x => x.Id == animalLocationEntity.Id) ?? true)
        {
            throw new NotFoundException();
        }

        var locationFilter = new LocationFilter
        {
            Id = request.LocationPointId
        };

        if (!await _unitOfWork.Locations.ExistsAsync(locationFilter))
        {
            throw new NotFoundException();
        }

        if (!IsUpdateValid(request, animalLocationEntity, animal))
        {
            throw new ValidationException();
        }

        animalLocationEntity = _mapper.Map(request, animalLocationEntity);

        await _unitOfWork.AnimalLocations.UpdateAsync(animalLocationEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AnimalLocationDto>(animalLocationEntity);
    }

    private bool IsUpdateValid(UpdateAnimalLocationCommandMessage request, AnimalLocationEntity animalLocation, AnimalEntity animalEntity)
    {
        var visitedLocations = animalEntity.VisitedLocations;
        
        if (visitedLocations.First().Id == animalLocation.Id &&
            animalEntity.ChippingLocationId == request.LocationPointId)
        {
            return false;
        }

        if (request.LocationPointId == animalLocation.LocationPointId)
        {
            return false;
        }

        var locationIdx = visitedLocations.FindIndex(x => x.Id == animalLocation.Id);
        var prevLocation = visitedLocations.ElementAtOrDefault(locationIdx - 1);
        
        if (prevLocation != null && prevLocation.LocationPointId == request.LocationPointId)
        {
            return false;
        }
        
        var nextLocation = visitedLocations.ElementAtOrDefault(locationIdx + 1);

        if (nextLocation != null && nextLocation.LocationPointId == request.LocationPointId)
        {
            return false;
        }

        return true;
    }
}