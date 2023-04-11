using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;

public class ImportAccountCommandMessageBase : IRequest<AccountDto>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}