using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Animals.Validators;

public class RemoveAnimalTypeCommandMessageValidator : AbstractValidator<RemoveAnimalTypeCommandMessage>
{
    public RemoveAnimalTypeCommandMessageValidator()
    {
        RuleFor(x => x.AnimalId)
            .Must(x => x > 0);

        RuleFor(x => x.TypeId)
            .Must(x => x > 0);
    }
}