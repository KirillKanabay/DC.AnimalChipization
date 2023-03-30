using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.AnimalTypes.DataTransfer;
using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Queries;
using DC.AnimalChipization.Data.Common.UoW;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Handlers.Queries;

public class GetAnimalTypeByIdQueryHandler : IRequestHandler<GetAnimalTypeByIdQueryMessage, AnimalTypeDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAnimalTypeByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<AnimalTypeDto> Handle(GetAnimalTypeByIdQueryMessage request, CancellationToken cancellationToken)
    {
        var animalTypEntity = await _unitOfWork.AnimalTypes.GetByIdAsync(request.AnimalTypeId);

        if (animalTypEntity == null)
        {
            throw new NotFoundException();
        }

        var dto = _mapper.Map<AnimalTypeDto>(animalTypEntity);

        return dto;
    }
}