using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Handlers.Commands;

public class ChangeAnimalTypeCommandHandler : IRequestHandler<ChangeAnimalTypeCommandMessage, AnimalDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeAnimalTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<AnimalDto> Handle(ChangeAnimalTypeCommandMessage request, CancellationToken cancellationToken)
    {
        var filter = new AnimalFilter
        {
            Id = request.AnimalId
        };
        filter.Include(x => x.AnimalTypes);

        var animalEntity = await _unitOfWork.Animals.FirstOrDefaultAsync(filter, useTracking: true);

        if (animalEntity == null)
        {
            throw new NotFoundException();
        }

        if (!animalEntity.AnimalTypes.Any(x => x.Id == request.OldTypeId))
        {
            throw new NotFoundException();
        }

        if (animalEntity.AnimalTypes.Any(x => x.Id == request.NewTypeId))
        {
            throw new ConflictException();
        }

        var oldAnimalType = await _unitOfWork.AnimalTypes.GetByIdAsync(request.OldTypeId);

        if (oldAnimalType == null)
        {
            throw new NotFoundException();
        }

        var newAnimalType = await _unitOfWork.AnimalTypes.GetByIdAsync(request.NewTypeId);

        if (newAnimalType == null)
        {
            throw new NotFoundException();
        }

        animalEntity.AnimalTypes = animalEntity.AnimalTypes.Where(x => x.Id != request.OldTypeId).ToList();
        animalEntity.AnimalTypes.Add(newAnimalType);

        await _unitOfWork.Animals.UpdateAsync(animalEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AnimalDto>(animalEntity);
    }
}