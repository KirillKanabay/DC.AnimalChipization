using DC.AnimalChipization.Application.Common.Validators;
using DC.AnimalChipization.Application.Features.Animals.Enums;
using DC.AnimalChipization.Application.Features.Animals.Messages.Queries;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Animals.Validators;

public class AnimalSearchQueryMessageValidator : AbstractValidator<AnimalSearchQueryMessage>
{
    public AnimalSearchQueryMessageValidator()
    {
        Include(new PagedMessageValidator());

        RuleFor(x => x.ChipperId)
            .Must(x => x is null or > 0);

        RuleFor(x => x.ChippingLocationId)
            .Must(x => x is null or > 0);

        RuleFor(x => x.LifeStatus)
            .Must(x => !x.HasValue || Enum.IsDefined(typeof(LifeStatus), x));

        RuleFor(x => x.Gender)
            .Must(x => !x.HasValue || Enum.IsDefined(typeof(Gender), x));
    }
}