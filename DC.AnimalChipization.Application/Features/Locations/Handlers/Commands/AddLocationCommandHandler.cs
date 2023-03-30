using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Locations.DataTransfer;
using DC.AnimalChipization.Application.Features.Locations.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Locations.Handlers.Commands;

public class AddLocationCommandHandler : IRequestHandler<AddLocationCommandMessage, LocationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddLocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }


    public async Task<LocationDto> Handle(AddLocationCommandMessage request, CancellationToken cancellationToken)
    {
        var existedLocation = await _unitOfWork.Locations.ListAsync(new LocationFilter
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude
        }, null);

        if (existedLocation.Any())
        {
            throw new ConflictException();
        }

        var locationEntity = _mapper.Map<LocationEntity>(request);
        await _unitOfWork.Locations.InsertAsync(locationEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<LocationDto>(locationEntity);
    }
}