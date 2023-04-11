using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Common.Immutable;
using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Queries;
using DC.AnimalChipization.Application.Identity.Contracts;
using DC.AnimalChipization.Data.Common.UoW;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Handlers.Queries;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQueryMessage, AccountDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityManager _identityManager;

    public GetAccountByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IIdentityManager identityManager)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
    }

    public async Task<AccountDto> Handle(GetAccountByIdQueryMessage request, CancellationToken cancellationToken)
    {
        var accountEntity = await _unitOfWork.Accounts.GetByIdAsync(request.AccountId);
        var currentUser = _identityManager.GetCurrentUser();

        if (currentUser.Role.Equals(Roles.Admin))
        {
            if (accountEntity is null)
            {
                throw new NotFoundException();
            }
        }
        else
        {
            if (accountEntity is null || currentUser.Id != accountEntity.Id)
            {
                throw new AccessDeniedException();
            }
        }

        if (accountEntity == null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<AccountDto>(accountEntity);
    }
}