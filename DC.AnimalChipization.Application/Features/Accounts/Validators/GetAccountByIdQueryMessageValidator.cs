using DC.AnimalChipization.Application.Features.Accounts.Messages.Queries;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Accounts.Validators;

public class GetAccountByIdQueryMessageValidator : AbstractValidator<GetAccountByIdQueryMessage>
{
    public GetAccountByIdQueryMessageValidator()
    {
        RuleFor(x => x.AccountId)
            .Must(x => x > 0);
    }
}