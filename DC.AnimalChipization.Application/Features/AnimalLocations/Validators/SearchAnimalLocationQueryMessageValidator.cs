using DC.AnimalChipization.Application.Common.Validators;
using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Queries;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Validators;

public class SearchAnimalLocationQueryMessageValidator : AbstractValidator<SearchAnimalLocationQueryMessage>
{
    public SearchAnimalLocationQueryMessageValidator()
    {
        Include(new PagedMessageValidator());

        RuleFor(x => x.AnimalId)
            .Must(x => x > 0);
    }
}