using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.AnimalLocations.DataTransfer;
using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Queries;
using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Handlers.Queries;

public class SearchAnimalLocationQueryHandler : IRequestHandler<SearchAnimalLocationQueryMessage, List<AnimalLocationDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public SearchAnimalLocationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<List<AnimalLocationDto>> Handle(SearchAnimalLocationQueryMessage request, CancellationToken cancellationToken)
    {
        var animalExists = await _unitOfWork.Animals.ExistsAsync(new AnimalFilter
        {
            Id = request.AnimalId
        });
        
        if (!animalExists)
        {
            throw new NotFoundException();
        }
        
        var filter = _mapper.Map<AnimalLocationFilter>(request);
        var entities = await _unitOfWork.AnimalLocations.ListAsync(filter, new Paging
        {
            From = request.From,
            Size = request.Size
        });

        return _mapper.Map<List<AnimalLocationDto>>(entities);
    }
}