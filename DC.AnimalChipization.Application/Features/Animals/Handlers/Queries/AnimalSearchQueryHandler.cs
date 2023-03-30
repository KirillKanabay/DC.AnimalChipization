using AutoMapper;
using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Features.Animals.Messages.Queries;
using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Handlers.Queries;

public class AnimalSearchQueryHandler : IRequestHandler<AnimalSearchQueryMessage, List<AnimalDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public AnimalSearchQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    public async Task<List<AnimalDto>> Handle(AnimalSearchQueryMessage request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<AnimalFilter>(request);
        filter.Include(x => x.AnimalTypes)
              .Include(x => x.VisitedLocations);

        var animalEntities = await _unitOfWork.Animals.ListAsync(filter, new Paging
        {
            From = request.From,
            Size = request.Size
        });

        return _mapper.Map<List<AnimalDto>>(animalEntities);
    }
}