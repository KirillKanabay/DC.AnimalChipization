using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;

[Authorize]
public class DeleteAccountCommandMessage : IRequest
{
    public int AccountId { get; set; }
}