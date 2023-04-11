using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Common.Helpers;
using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Features.Animals.Enums;
using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Handlers.Commands;

public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommandMessage, AnimalDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAnimalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }


    public async Task<AnimalDto> Handle(UpdateAnimalCommandMessage request, CancellationToken cancellationToken)
    {
        var filter = new AnimalFilter
        {
            Id = request.AnimalId
        };

        filter.Include(x => x.AnimalTypes)
              .Include(x => x.VisitedLocations);
        
        var entity = await _unitOfWork.Animals.FirstOrDefaultAsync(filter);

        if (entity == null)
        {
            throw new NotFoundException();
        }

        var animalDto = _mapper.Map<AnimalDto>(entity);

        if (!IsUpdateValid(animalDto, request))
        {
            throw new ValidationException();
        }

        if (!await RelatedDataExists(request))
        {
            throw new NotFoundException();
        }

        animalDto = _mapper.Map(request, animalDto);

        if (animalDto.LifeStatus is LifeStatus.Dead)
        {
            animalDto.DeathDateTime = DateTimeHelper.GetTimeStamp();
        }

        var updatedEntity = _mapper.Map<AnimalEntity>(animalDto);

        await _unitOfWork.Animals.UpdateAsync(updatedEntity);
        await _unitOfWork.SaveChangesAsync();

        return animalDto;
    }
    
    private bool IsUpdateValid(AnimalDto dto, UpdateAnimalCommandMessage request)
    {
        if (dto.LifeStatus is LifeStatus.Dead && request.LifeStatus is LifeStatus.Alive)
        {
            return false;
        }

        var firstVisitedLocation = dto.VisitedLocations?.FirstOrDefault();

        if (firstVisitedLocation != null && firstVisitedLocation.LocationPointId == request.ChippingLocationId)
        {
            return false;
        }

        return true;
    }
    
    private async Task<bool> RelatedDataExists(UpdateAnimalCommandMessage request)
    {
        var account = await _unitOfWork.Accounts.GetByIdAsync(request.ChipperId);

        if (account == null)
        {
            return false;
        }

        var location = await _unitOfWork.Locations.GetByIdAsync(request.ChippingLocationId);

        if (location == null)
        {
            return false;
        }

        return true;
    }
}