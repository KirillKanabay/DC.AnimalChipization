using DC.AnimalChipization.Application.Features.Locations.Messages.Queries;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Locations.Validators;

public class GetIdByPointQueryMessageValidator : AbstractValidator<GetIdByPointQueryMessage>
{
    public GetIdByPointQueryMessageValidator()
    {
        RuleFor(x => x.Latitude)
            .Must(x => x is <= 90 and >= -90);

        RuleFor(x => x.Longitude)
            .Must(x => x is <= 180 and >= -180);
    }
}