using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Validators;

public class DeleteAnimalTypeCommandMessageValidator : AbstractValidator<DeleteAnimalTypeCommandMessage>
{
    public DeleteAnimalTypeCommandMessageValidator()
    {
        RuleFor(x => x.AnimalTypeId)
            .Must(x => x > 0);
    }
}