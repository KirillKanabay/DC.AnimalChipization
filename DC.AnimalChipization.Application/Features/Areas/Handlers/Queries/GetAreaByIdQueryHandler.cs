using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Features.Areas.Messages.Queries;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Areas.Handlers.Queries;

public class GetAreaByIdQueryHandler : IRequestHandler<GetAreaByIdQueryMessage, AreaDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAreaByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AreaDto> Handle(GetAreaByIdQueryMessage request, CancellationToken cancellationToken)
    {
        var filter = new AreaFilter
        {
            Id = request.Id
        };
        filter.Include(x => x.AreaPoints);

        var entity = await _unitOfWork.Areas.FirstOrDefaultAsync(filter);

        if (entity == null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<AreaDto>(entity);
    }
}