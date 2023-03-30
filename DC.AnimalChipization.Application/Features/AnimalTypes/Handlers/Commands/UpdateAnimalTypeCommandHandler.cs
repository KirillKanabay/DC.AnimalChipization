using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.AnimalTypes.DataTransfer;
using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Handlers.Commands;

public class UpdateAnimalTypeCommandHandler : IRequestHandler<UpdateAnimalTypeCommandMessage, AnimalTypeDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAnimalTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<AnimalTypeDto> Handle(UpdateAnimalTypeCommandMessage request, CancellationToken cancellationToken)
    {
        var animalTypeEntity = await _unitOfWork.AnimalTypes.GetByIdAsync(request.AnimalTypeId);

        if (animalTypeEntity == null)
        {
            throw new NotFoundException($"Animal type with Id:{request.AnimalTypeId} not found");
        }

        var animalTypeWithSameType = await _unitOfWork.AnimalTypes.GetByTypeAsync(request.Type);

        if (animalTypeWithSameType != null && animalTypeWithSameType.Id != animalTypeEntity.Id)
        {
            throw new ConflictException($"Animal type with name ({request.Type}) already exists");
        }

        animalTypeEntity = _mapper.Map(request, animalTypeEntity);

        await _unitOfWork.AnimalTypes.UpdateAsync(animalTypeEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AnimalTypeDto>(animalTypeEntity);
    }
}