using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Locations.DataTransfer;
using DC.AnimalChipization.Application.Features.Locations.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Locations.Handlers.Commands;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommandMessage, LocationDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<LocationDto> Handle(UpdateLocationCommandMessage request, CancellationToken cancellationToken)
    {
        var locationEntity = await _unitOfWork.Locations.GetByIdAsync(request.PointId);

        if (locationEntity == null)
        {
            throw new NotFoundException();
        }

        var locationWithSamePoint = (await _unitOfWork.Locations.ListAsync(new LocationFilter
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        }, null)).FirstOrDefault();

        if (locationWithSamePoint != null && locationWithSamePoint.Id != locationEntity.Id)
        {
            throw new ConflictException();
        }

        locationEntity = _mapper.Map(request, locationEntity);

        await _unitOfWork.Locations.UpdateAsync(locationEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<LocationDto>(locationEntity);
    }
}