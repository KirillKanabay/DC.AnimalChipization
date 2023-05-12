using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Accounts.Validators;

public class AddAccountCommandMessageValidator : ImportAccountCommandMessageValidatorBase<AddAccountCommandMessage>
{
    public AddAccountCommandMessageValidator()
    {
        RuleFor(x => x.Role).NotEmpty();
    }
}