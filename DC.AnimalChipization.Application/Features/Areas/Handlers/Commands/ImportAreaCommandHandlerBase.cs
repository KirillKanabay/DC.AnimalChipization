using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Features.Areas.Messages.Commands;
using DC.AnimalChipization.Application.Features.Areas.Validators.Helpers;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Areas.Handlers.Commands;

public abstract class ImportAreaCommandHandlerBase<TCommand> : IRequestHandler<TCommand, AreaDto>
    where TCommand : ImportAreaCommandMessageBase
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly IMapper Mapper;

    protected ImportAreaCommandHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public abstract Task<AreaDto> Handle(TCommand request, CancellationToken cancellationToken);

    protected virtual async Task ValidateRequestAsync(TCommand request)
    {
        var filter = new AreaFilter();
        filter.Include(x => x.AreaPoints);

        var areasEntities = await UnitOfWork.Areas.ListAsync(filter);

        var areas = Mapper.Map<List<AreaDto>>(areasEntities);

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