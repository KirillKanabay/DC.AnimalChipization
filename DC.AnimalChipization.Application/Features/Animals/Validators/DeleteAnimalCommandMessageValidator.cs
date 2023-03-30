using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Animals.Validators;

public class DeleteAnimalCommandMessageValidator : AbstractValidator<DeleteAnimalCommandMessage>
{
    public DeleteAnimalCommandMessageValidator()
    {
        RuleFor(x => x.AnimalId)
            .Must(x => x > 0);
    }
}