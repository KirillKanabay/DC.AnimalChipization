using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Areas.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Areas.Handlers.Commands;

public class DeleteAreaCommandHandler : IRequestHandler<DeleteAreaCommandMessage>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAreaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(DeleteAreaCommandMessage request, CancellationToken cancellationToken)
    {
        var area = await _unitOfWork.Areas.FirstOrDefaultAsync(new AreaFilter
        {
            Id = request.AreaId
        });

        if (area == null)
        {
            throw new NotFoundException();
        }

        await _unitOfWork.Areas.DeleteAsync(area);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}