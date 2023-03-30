using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Validators;

public class AddAnimalLocationCommandMessageValidator : AbstractValidator<AddAnimalLocationCommandMessage>
{
    public AddAnimalLocationCommandMessageValidator()
    {
        RuleFor(x => x.PointId)
            .Must(x => x > 0);

        RuleFor(x => x.AnimalId)
            .Must(x => x > 0);
    }
}