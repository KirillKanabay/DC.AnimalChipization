using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.AnimalTypes.DataTransfer;
using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;
using DC.AnimalChipization.Data.Entities;
using MediatR;
using DC.AnimalChipization.Data.Common.UoW;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Handlers.Commands;

public class AddAnimalTypeCommandHandler : IRequestHandler<AddAnimalTypeCommandMessage, AnimalTypeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddAnimalTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }


    public async Task<AnimalTypeDto> Handle(AddAnimalTypeCommandMessage request, CancellationToken cancellationToken)
    {
        var existedAnimalType = await _unitOfWork.AnimalTypes.GetByTypeAsync(request.Type);

        if (existedAnimalType != null)
        {
            throw new ConflictException($"Animal type with name ({request.Type}) already exists");
        }

        var animalTypeEntity = _mapper.Map<AnimalTypeEntity>(request);
        await _unitOfWork.AnimalTypes.InsertAsync(animalTypeEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AnimalTypeDto>(animalTypeEntity);
    }
}