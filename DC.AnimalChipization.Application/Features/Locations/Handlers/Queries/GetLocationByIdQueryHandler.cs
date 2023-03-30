using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Locations.DataTransfer;
using DC.AnimalChipization.Application.Features.Locations.Messages.Queries;
using DC.AnimalChipization.Data.Common.UoW;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Locations.Handlers.Queries;

public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQueryMessage, LocationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLocationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<LocationDto> Handle(GetLocationByIdQueryMessage request, CancellationToken cancellationToken)
    {
        var locationEntity = await _unitOfWork.Locations.GetByIdAsync(request.PointId);

        if (locationEntity == null)
        {
            throw new NotFoundException();
        }

        var dto = _mapper.Map<LocationDto>(locationEntity);

        return dto;
    }
}