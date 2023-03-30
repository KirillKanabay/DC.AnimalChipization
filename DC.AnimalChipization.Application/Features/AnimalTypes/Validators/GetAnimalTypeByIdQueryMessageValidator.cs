using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Queries;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Validators;

public class GetAnimalTypeByIdQueryMessageValidator : AbstractValidator<GetAnimalTypeByIdQueryMessage>
{
    public GetAnimalTypeByIdQueryMessageValidator()
    {
        RuleFor(x => x.AnimalTypeId)
            .Must(x => x > 0);
    }
}