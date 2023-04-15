using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Features.Areas.Messages.Commands;
using DC.AnimalChipization.Application.Features.Areas.Validators.Helpers;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Areas.Handlers.Commands;

public class AddAreaCommandHandler : IRequestHandler<AddAreaCommandMessage, AreaDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AreaDto> Handle(AddAreaCommandMessage request, CancellationToken cancellationToken)
    {
        await ValidateRequestAsync(request);

        var areaEntity = _mapper.Map<AreaEntity>(request);
        await _unitOfWork.Areas.InsertAsync(areaEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AreaDto>(areaEntity);
    }

    private async Task ValidateRequestAsync(AddAreaCommandMessage request)
    {
        var filter = new AreaFilter();
        filter.Include(x => x.AreaPoints);

        var areasEntities = await _unitOfWork.Areas.ListAsync(filter);

        var areas = _mapper.Map<List<AreaDto>>(areasEntities);

        if (areas.Any(area => area.Name.Equals(request.Name, StringComparison.InvariantCultureIgnoreCase) ||
                              AreaValidationHelper.IsPolygonsDuplicate(area.AreaPoints, request.AreaPoints)))
        {
            throw new ConflictException();
        }

        if (areas.Any(area => AreaValidationHelper.ArePolygonsIntersecting(area.AreaPoints, request.AreaPoints) ||
                              AreaValidationHelper.AreContainedPolygons(area.AreaPoints, request.AreaPoints) ||
                              AreaValidationHelper.AreContainedPolygons(request.AreaPoints, area.AreaPoints)))
        {
            throw new ValidationException();
        }
    }
}