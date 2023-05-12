using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using DC.AnimalChipization.Application.Identity.Contracts;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Handlers.Commands;

public class AddAccountCommandHandler : ImportAccountCommandHandlerBase<AddAccountCommandMessage>
{
    public AddAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    protected override async Task<RoleEntity> GetRole(AddAccountCommandMessage request)
    {
        var role = await UnitOfWork.Roles.GetByNameAsync(request.Role);

        if (role == null)
        {
            throw new ValidationException();
        }

        return role;
    }
}