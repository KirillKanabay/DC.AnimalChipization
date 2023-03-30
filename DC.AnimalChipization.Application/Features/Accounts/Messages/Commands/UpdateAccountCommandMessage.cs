using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;

[Authorize]
public sealed record UpdateAccountCommandMessage(string FirstName, string LastName, string Email, string Password)
    : IRequest<AccountDto>
{
    public int AccountId { get; set; }
}