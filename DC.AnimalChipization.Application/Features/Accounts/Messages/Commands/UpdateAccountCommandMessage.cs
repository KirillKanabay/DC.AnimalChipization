using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;

[Authorize]
public class UpdateAccountCommandMessage : ImportAccountCommandMessageBase
{
    public int AccountId { get; set; }
    public string Role { get; set; }
}