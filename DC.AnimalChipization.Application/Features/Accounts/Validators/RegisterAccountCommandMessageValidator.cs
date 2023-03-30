using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Accounts.Validators
{
    public class RegisterAccountCommandMessageValidator : AbstractValidator<RegisterAccountCommandMessage>
    {
        public RegisterAccountCommandMessageValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            
            RuleFor(x => x.LastName).NotEmpty();
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
