using DC.AnimalChipization.Application.Common.Validators;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Queries;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Accounts.Validators;

public class AccountSearchQueryMessageValidator : AbstractValidator<AccountSearchQueryMessage>
{
    public AccountSearchQueryMessageValidator()
    {
        Include(new PagedMessageValidator());
    }
}