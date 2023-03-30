using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Features.Accounts.Exceptions;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using DC.AnimalChipization.Application.Identity.Contracts;
using DC.AnimalChipization.Data.Common.UoW;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Handlers.Commands;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommandMessage, AccountDto>
{
    private readonly IIdentityManager _identityManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAccountCommandHandler(IIdentityManager identityManager, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AccountDto> Handle(UpdateAccountCommandMessage request, CancellationToken cancellationToken)
    {
        var currentUser = _identityManager.GetCurrentUser();

        if (currentUser.Id != request.AccountId)
        {
            throw new AccessDeniedException();
        }

        var accountEntity = await _unitOfWork.Accounts.GetByIdAsync(request.AccountId);

        if (accountEntity == null)
        {
            throw new AccessDeniedException();
        }

        var accountWithSameEmail = await _unitOfWork.Accounts.GetByEmailAsync(request.Email);

        if (accountWithSameEmail != null && accountWithSameEmail.Id != accountEntity.Id)
        {
            throw new AccountDuplicateEmailException();
        }

        accountEntity = _mapper.Map(request, accountEntity);

        await _unitOfWork.Accounts.UpdateAsync(accountEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AccountDto>(accountEntity);
    }
}