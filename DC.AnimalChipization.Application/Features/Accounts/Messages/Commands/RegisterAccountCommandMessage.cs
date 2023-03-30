using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Messages.Commands
{
    public sealed record RegisterAccountCommandMessage(string FirstName, string LastName, string Email, string Password)
        : IRequest<AccountDto>;
}
