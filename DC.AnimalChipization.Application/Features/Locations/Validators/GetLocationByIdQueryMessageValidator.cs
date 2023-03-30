using DC.AnimalChipization.Application.Features.Locations.Messages.Queries;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Locations.Validators;

public class GetLocationByIdQueryMessageValidator : AbstractValidator<GetLocationByIdQueryMessage>
{
    public GetLocationByIdQueryMessageValidator()
    {
        RuleFor(x => x.PointId)
            .Must(x => x > 0);
    }
}