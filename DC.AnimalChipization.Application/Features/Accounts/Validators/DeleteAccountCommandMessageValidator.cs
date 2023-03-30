using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Accounts.Validators;

public class DeleteAccountCommandMessageValidator : AbstractValidator<DeleteAccountCommandMessage>
{
    public DeleteAccountCommandMessageValidator()
    {
        RuleFor(x => x.AccountId)
            .Must(x => x > 0);
    }
}