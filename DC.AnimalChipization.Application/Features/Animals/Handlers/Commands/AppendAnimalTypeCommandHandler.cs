using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Handlers.Commands;

public class AppendAnimalTypeCommandHandler : IRequestHandler<AppendAnimalTypeCommandMessage, AnimalDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AppendAnimalTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AnimalDto> Handle(AppendAnimalTypeCommandMessage request, CancellationToken cancellationToken)
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

        if (animalEntity.AnimalTypes.Any(x => x.Id == request.TypeId))
        {
            throw new ConflictException();
        }

        var animalType = await _unitOfWork.AnimalTypes.GetByIdAsync(request.TypeId);

        if (animalType == null)
        {
            throw new NotFoundException();
        }

        animalEntity.AnimalTypes.Add(animalType);

        await _unitOfWork.Animals.UpdateAsync(animalEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AnimalDto>(animalEntity);
    }
}