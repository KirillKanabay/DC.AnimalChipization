using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Handlers.Commands;

public class DeleteAnimalCommandHandler : IRequestHandler<DeleteAnimalCommandMessage>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAnimalCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(DeleteAnimalCommandMessage request, CancellationToken cancellationToken)
    {
        var filter = new AnimalFilter
        {
            Id = request.AnimalId
        };
        filter.Include(x => x.VisitedLocations.OrderBy(vl => vl.VisitDateTime));

        var animalEntity = await _unitOfWork.Animals.FirstOrDefaultAsync(filter);

        if (animalEntity == null)
        {
            throw new NotFoundException();
        }

        if (!IsDeleteAllowed(animalEntity))
        {
            throw new ValidationException();
        }

        await _unitOfWork.Animals.DeleteAsync(animalEntity);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }

    private bool IsDeleteAllowed(AnimalEntity animal)
    {
        var visitedLocations = animal.VisitedLocations;

        return !visitedLocations?.Any() ?? true;
    }
}