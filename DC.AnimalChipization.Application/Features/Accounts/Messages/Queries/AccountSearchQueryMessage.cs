using DC.AnimalChipization.Application.Common.Messages;
using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Messages.Queries;

public class AccountSearchQueryMessage : PagedMessage, IRequest<List<AccountDto>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}