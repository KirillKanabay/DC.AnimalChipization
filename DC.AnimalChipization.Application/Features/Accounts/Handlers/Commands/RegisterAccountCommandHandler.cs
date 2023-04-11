using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Common.Immutable;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using DC.AnimalChipization.Application.Identity.Contracts;
using DC.AnimalChipization.Data.Common.Immutable;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Application.Features.Accounts.Handlers.Commands
{
    public class RegisterAccountCommandHandler : ImportAccountCommandHandlerBase<RegisterAccountCommandMessage>
    {
        private readonly IIdentityManager _identityManager;

        private (int Id, string Name) DefaultRole => (RoleId.User, Roles.User);

        public RegisterAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IIdentityManager identityManager) : base(unitOfWork, mapper)
        {
            _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
        }

        protected override Task PreExecuteAsync(RegisterAccountCommandMessage request)
        {
            var currentUser = _identityManager.GetCurrentUser();

            if (currentUser != null)
            {
                throw new AccessDeniedException();
            }

            return Task.CompletedTask;
        }

        protected override Task<RoleEntity> GetRole(RegisterAccountCommandMessage request)
        {
            return Task.FromResult(new RoleEntity
            {
                Id = DefaultRole.Id,
                Name = DefaultRole.Name
            });
        }
    }
}
