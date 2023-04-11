using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Common.Immutable;
using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Features.Accounts.Exceptions;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using DC.AnimalChipization.Application.Identity.Contracts;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Handlers.Commands;

public abstract class ImportAccountCommandHandlerBase<TRequest> : IRequestHandler<TRequest, AccountDto>
    where TRequest : ImportAccountCommandMessageBase
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly IMapper Mapper;

    protected ImportAccountCommandHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AccountDto> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await PreExecuteAsync(request);

        var accountWithSameEmail = await UnitOfWork.Accounts.GetByEmailAsync(request.Email);

        if (accountWithSameEmail != null)
        {
            throw new AccountDuplicateEmailException();
        }

        var role = await GetRole(request);

        var accountEntity = Mapper.Map<AccountEntity>(request);
        accountEntity.RoleId = role.Id;

        accountEntity = await UnitOfWork.Accounts.InsertAsync(accountEntity);
        await UnitOfWork.SaveChangesAsync();

        var accountDto = Mapper.Map<AccountEntity, AccountDto>(accountEntity);
        accountDto.Role = role.Name;

        return accountDto;
    }

    protected virtual Task PreExecuteAsync(TRequest request)
    {
        return Task.CompletedTask;
    }

    protected abstract Task<RoleEntity> GetRole(TRequest request);
}