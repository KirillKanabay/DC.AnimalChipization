using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Features.Areas.Messages.Queries;
using DC.AnimalChipization.Application.Features.Areas.Validators.Helpers;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Areas.Handlers.Queries;

public class GetAreaAnalyticsQueryHandler : IRequestHandler<GetAreaAnalyticsQueryMessage, AreaAnalyticResultDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private readonly HashSet<long> _cachedAreaLocations;

    public GetAreaAnalyticsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _cachedAreaLocations = new HashSet<long>();
    }

    public async Task<AreaAnalyticResultDto> Handle(GetAreaAnalyticsQueryMessage request, CancellationToken cancellationToken)
    {
        var filter = new AreaFilter
        {
            Id = request.AreaId
        };
        filter.Include(x => x.AreaPoints);

        var area = await _unitOfWork.Areas.FirstOrDefaultAsync(filter);

        if (area == null)
        {
            throw new NotFoundException();
        }

        var areaDto = _mapper.Map<AreaDto>(area);
        var animals = await _unitOfWork.Animals.GetAnimalsByVisitDatePeriodAsync(request.StartDate, request.EndDate);

        return GetAnalytic(animals, areaDto);
    }

    private AreaAnalyticResultDto GetAnalytic(List<AnimalEntity> animals, AreaDto area)
    {
        var analyticResult = new AreaAnalyticResultDto();
        var animalAnalyticsByType = new Dictionary<long, AreaAnalyticItemDto>();

        foreach (var animal in animals)
        {
            var isArrived = false;
            var isGone = false;
            var animalInArea = IsLocationInArea(area, animal.ChippingLocation);

            foreach (var visitedLocation in animal.VisitedLocations)
            {
                var isLocationInArea = IsLocationInArea(area, visitedLocation.Location);

                if (isLocationInArea && !animalInArea)
                {
                    isArrived = true;
                }
                else if (!isLocationInArea && animalInArea)
                {
                    isGone = true;
                }

                animalInArea = isLocationInArea;
            }

            analyticResult.TotalQuantityAnimals += animalInArea ? 1 : 0;
            analyticResult.TotalAnimalsArrived += isArrived ? 1 : 0;
            analyticResult.TotalAnimalsGone += isGone ? 1 : 0;

            if (isArrived || isGone || animalInArea)
            {
                foreach (var animalType in animal.AnimalTypes)
                {
                    if (!animalAnalyticsByType.TryGetValue(animalType.Id, out var analyticItem))
                    {
                        analyticItem = new AreaAnalyticItemDto
                        {
                            AnimalType = animalType.Type,
                            AnimalTypeId = animalType.Id
                        };

                        animalAnalyticsByType.Add(analyticItem.AnimalTypeId, analyticItem);
                    }

                    analyticItem.QuantityAnimals += animalInArea ? 1 : 0;
                    analyticItem.AnimalsArrived += isArrived ? 1 : 0;
                    analyticItem.AnimalsGone += isGone ? 1 : 0;
                }
            }
        }

        analyticResult.AnimalsAnalytics = animalAnalyticsByType.Select(x => x.Value).ToList();

        return analyticResult;
    }

    private bool IsLocationInArea(AreaDto area, LocationEntity location)
    {
        if (location == null)
        {
            return false;
        }

        if (_cachedAreaLocations.Contains(location.Id))
        {
            return true;
        }

        var isLocationInArea = AreaValidationHelper.IsPointInsidePolygon(new AreaPointDto
        {
            Longitude = location.Longitude,
            Latitude = location.Latitude,
        }, area.AreaPoints);

        if (isLocationInArea)
        {
            _cachedAreaLocations.Add(location.Id);
        }

        return isLocationInArea;
    }
}