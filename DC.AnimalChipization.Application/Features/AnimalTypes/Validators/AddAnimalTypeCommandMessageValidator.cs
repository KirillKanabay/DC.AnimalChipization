using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Validators;

public class AddAnimalTypeCommandMessageValidator : AbstractValidator<AddAnimalTypeCommandMessage>
{
    public AddAnimalTypeCommandMessageValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty();
    }
}