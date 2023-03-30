using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Validators;

public class DeleteAnimalLocationCommandMessageValidator : AbstractValidator<DeleteAnimalLocationCommandMessage>
{
    public DeleteAnimalLocationCommandMessageValidator()
    {
        RuleFor(x => x.AnimalId)
            .Must(x => x > 0);

        RuleFor(x => x.VisitedPointId)
            .Must(x => x > 0);
    }
}