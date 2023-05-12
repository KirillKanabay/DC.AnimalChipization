using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Features.Areas.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;

namespace DC.AnimalChipization.Application.Features.Areas.Handlers.Commands;

public class UpdateAreaCommandHandler : ImportAreaCommandHandlerBase<UpdateAreaCommandMessage>
{
    public UpdateAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public override async Task<AreaDto> Handle(UpdateAreaCommandMessage request, CancellationToken cancellationToken)
    {
        var area = await UnitOfWork.Areas.FirstOrDefaultAsync(new AreaFilter
        {
            Id = request.AreaId
        });

        if (area == null)
        {
            throw new NotFoundException();
        }

        await ValidateRequestAsync(request);

        await UnitOfWork.AreaPoints.DeletePointsByAreaIdAsync(area.Id);
        await UnitOfWork.SaveChangesAsync();

        area = Mapper.Map(request, area);

        await UnitOfWork.Areas.UpdateAsync(area);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<AreaDto>(area);
    }

    protected override async Task<List<AreaDto>> GetExistedAreasForValidateAsync(UpdateAreaCommandMessage request)
    {
        var areas = await base.GetExistedAreasForValidateAsync(request);

        return areas.Where(x => x.Id != request.AreaId).ToList();
    }
}