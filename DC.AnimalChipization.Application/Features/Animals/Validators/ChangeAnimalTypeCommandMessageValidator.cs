using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Animals.Validators;

public class ChangeAnimalTypeCommandMessageValidator : AbstractValidator<ChangeAnimalTypeCommandMessage>
{
    public ChangeAnimalTypeCommandMessageValidator()
    {
        RuleFor(x => x.AnimalId)
            .Must(x => x > 0);

        RuleFor(x => x.NewTypeId)
            .Must(x => x > 0);

        RuleFor(x => x.OldTypeId)
            .Must(x => x > 0);
    }
}