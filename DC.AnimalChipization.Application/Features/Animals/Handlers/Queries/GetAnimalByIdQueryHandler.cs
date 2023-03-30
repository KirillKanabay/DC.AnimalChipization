using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Features.Animals.Messages.Queries;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Handlers.Queries;

public class GetAnimalByIdQueryHandler : IRequestHandler<GetAnimalByIdQueryMessage, AnimalDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAnimalByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AnimalDto> Handle(GetAnimalByIdQueryMessage request, CancellationToken cancellationToken)
    {
        var filter = new AnimalFilter
        {
            Id = request.AnimalId
        };

        filter.Include(x => x.AnimalTypes)
              .Include(x => x.VisitedLocations);

        var animalEntity = await _unitOfWork.Animals.FirstOrDefaultAsync(filter);

        if (animalEntity == null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<AnimalDto>(animalEntity);
    }
}