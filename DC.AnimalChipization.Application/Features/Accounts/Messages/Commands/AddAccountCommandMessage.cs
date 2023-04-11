using DC.AnimalChipization.Application.Common.Immutable;
using DC.AnimalChipization.Application.Identity.Attributes;

namespace DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;

[Authorize(Roles = new []{ Roles.Admin })]
public class AddAccountCommandMessage : ImportAccountCommandMessageBase
{
    public string Role { get; set; }
}