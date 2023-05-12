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

public class AddAreaCommandHandler : ImportAreaCommandHandlerBase<AddAreaCommandMessage>
{ 
    public AddAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    public override async Task<AreaDto> Handle(AddAreaCommandMessage request, CancellationToken cancellationToken)
    {
        await ValidateRequestAsync(request);

        var areaEntity = Mapper.Map<AreaEntity>(request);
        await UnitOfWork.Areas.InsertAsync(areaEntity);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<AreaDto>(areaEntity);
    }
}