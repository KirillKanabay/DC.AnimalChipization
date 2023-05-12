using DC.AnimalChipization.Application.Features.Locations.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Locations.Validators;

public class CalculateGeoHashCommandMessageValidator : AbstractValidator<CalculateGeoHashCommandMessage>
{
    public CalculateGeoHashCommandMessageValidator()
    {
        RuleFor(x => x.Latitude)
            .Must(x => x is <= 90 and >= -90);

        RuleFor(x => x.Longitude)
            .Must(x => x is <= 180 and >= -180);
    }
}