using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Common.Helpers;
using DC.AnimalChipization.Application.Features.AnimalLocations.DataTransfer;
using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands;
using DC.AnimalChipization.Application.Features.Animals.Enums;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Handlers.Commands;

public class AddAnimalLocationCommandHandler : IRequestHandler<AddAnimalLocationCommandMessage, AnimalLocationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddAnimalLocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AnimalLocationDto> Handle(AddAnimalLocationCommandMessage request, CancellationToken cancellationToken)
    {
        var animalFilter = new AnimalFilter { Id = request.AnimalId };
        animalFilter.Include(x => x.VisitedLocations.OrderByDescending(l => l.VisitDateTime));

        var animal = await _unitOfWork.Animals.FirstOrDefaultAsync(animalFilter);

        if (animal == null)
        {
            throw new NotFoundException();
        }

        var location = await _unitOfWork.Locations.GetByIdAsync(request.PointId);

        if (location == null)
        {
            throw new NotFoundException();
        }

        if (!RequestIsValid(request, animal))
        {
            throw new ValidationException();
        }

        var entity = _mapper.Map<AnimalLocationEntity>(request);
        entity.VisitDateTime = DateTimeHelper.GetTimeStamp();
        
        await _unitOfWork.AnimalLocations.InsertAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AnimalLocationDto>(entity);
    }

    private bool RequestIsValid(AddAnimalLocationCommandMessage request, AnimalEntity animal)
    {
        if (animal.LifeStatus is (int)LifeStatus.Dead)
        {
            return false;
        }

        if (animal.ChippingLocationId == request.PointId && animal.VisitedLocations?.Count == 0)
        {
            return false;
        }

        var currentLocation = animal.VisitedLocations?.FirstOrDefault();

        if (currentLocation != null && currentLocation.LocationPointId == request.PointId)
        {
            return false;
        }

        return true;
    }
}