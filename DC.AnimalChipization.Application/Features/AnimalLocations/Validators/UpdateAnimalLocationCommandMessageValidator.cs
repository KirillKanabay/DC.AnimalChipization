using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Validators;

public class UpdateAnimalLocationCommandMessageValidator : AbstractValidator<UpdateAnimalLocationCommandMessage>
{
    public UpdateAnimalLocationCommandMessageValidator()
    {
        RuleFor(x => x.AnimalId)
            .Must(x => x > 0);

        RuleFor(x => x.VisitedLocationPointId)
            .Must(x => x > 0);

        RuleFor(x => x.LocationPointId)
            .Must(x => x > 0);
    }
}