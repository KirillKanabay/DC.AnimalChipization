using DC.AnimalChipization.Application.Features.Animals.Messages.Queries;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Animals.Validators;

public class GetAnimalByIdQueryMessageValidator : AbstractValidator<GetAnimalByIdQueryMessage>
{
    public GetAnimalByIdQueryMessageValidator()
    {
        RuleFor(x => x.AnimalId)
            .Must(x => x > 0);
    }
}