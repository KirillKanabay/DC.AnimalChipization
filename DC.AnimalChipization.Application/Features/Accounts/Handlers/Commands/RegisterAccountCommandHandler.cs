using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Features.Accounts.Exceptions;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using DC.AnimalChipization.Application.Identity.Contracts;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Handlers.Commands
{
    public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommandMessage, AccountDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IIdentityManager _identityManager;

        public RegisterAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IIdentityManager identityManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
        }

        public async Task<AccountDto> Handle(RegisterAccountCommandMessage request, CancellationToken cancellationToken)
        {
            var accountWithSameEmail = await _unitOfWork.Accounts.GetByEmailAsync(request.Email);

            if (accountWithSameEmail != null)
            {
                throw new AccountDuplicateEmailException();
            }

            var currentUser = _identityManager.GetCurrentUser();

            if (currentUser != null)
            {
                throw new AccessDeniedException();
            }

            var accountEntity = _mapper.Map<AccountEntity>(request);
            accountEntity = await _unitOfWork.Accounts.InsertAsync(accountEntity);
            await _unitOfWork.SaveChangesAsync();

            var accountDto = _mapper.Map<AccountEntity, AccountDto>(accountEntity);

            return accountDto;
        }
    }
}
