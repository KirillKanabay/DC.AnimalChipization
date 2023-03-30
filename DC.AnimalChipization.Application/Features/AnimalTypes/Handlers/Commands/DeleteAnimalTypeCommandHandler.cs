using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Data.Common.UoW;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Handlers.Commands;

public class DeleteAnimalTypeCommandHandler : IRequestHandler<DeleteAnimalTypeCommandMessage>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAnimalTypeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(DeleteAnimalTypeCommandMessage request, CancellationToken cancellationToken)
    {
        var filter = new AnimalTypeFilter
        {
            Ids = new List<long> { request.AnimalTypeId }
        };
        filter.Include(x => x.Animals);

        var animalTypeEntity = await _unitOfWork.AnimalTypes.FirstOrDefaultAsync(filter);

        if (animalTypeEntity == null)
        {
            throw new NotFoundException();
        }

        if (animalTypeEntity.Animals.Any())
        {
            throw new ValidationException();
        }

        await _unitOfWork.AnimalTypes.DeleteAsync(animalTypeEntity);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}