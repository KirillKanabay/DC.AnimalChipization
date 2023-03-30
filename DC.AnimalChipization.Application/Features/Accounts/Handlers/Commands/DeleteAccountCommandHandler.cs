using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using DC.AnimalChipization.Application.Identity.Contracts;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Handlers.Commands;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommandMessage>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityManager _identityManager;

    public DeleteAccountCommandHandler(IUnitOfWork unitOfWork, IIdentityManager identityManager)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
    }

    public async Task<Unit> Handle(DeleteAccountCommandMessage request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.Accounts.GetByIdAsync(request.AccountId);
        var currentUser = _identityManager.GetCurrentUser();

        if (account == null || currentUser.Id != account.Id)
        {
            throw new AccessDeniedException();
        }

        var animalExists = await _unitOfWork.Animals.ExistsAsync(new AnimalFilter
        {
            ChipperId = account.Id
        });

        if (animalExists)
        {
            throw new ValidationException();
        }

        await _unitOfWork.Accounts.DeleteAsync(account);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}