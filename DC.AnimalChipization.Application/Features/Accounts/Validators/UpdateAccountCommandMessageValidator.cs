using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Accounts.Validators;

public class UpdateAccountCommandMessageValidator : ImportAccountCommandMessageValidatorBase<UpdateAccountCommandMessage>
{
    public UpdateAccountCommandMessageValidator()
    {
        RuleFor(x => x.AccountId)
            .Must(x => x > 0);

        RuleFor(x => x.Role)
            .NotEmpty();
    }
}