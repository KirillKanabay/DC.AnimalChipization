using DC.AnimalChipization.Application.Common.Immutable;
using DC.AnimalChipization.Application.Common.Messages;
using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Messages.Queries;

[Authorize(Roles = new []{ Roles.Admin })]
public class AccountSearchQueryMessage : PagedMessage, IRequest<List<AccountDto>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}