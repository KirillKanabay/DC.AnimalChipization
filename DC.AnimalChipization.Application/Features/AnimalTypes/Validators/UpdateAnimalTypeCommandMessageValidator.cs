using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Validators;

public class UpdateAnimalTypeCommandMessageValidator : AbstractValidator<UpdateAnimalTypeCommandMessage>
{
    public UpdateAnimalTypeCommandMessageValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty();

        RuleFor(x => x.AnimalTypeId)
            .Must(x => x > 0);
    }
}