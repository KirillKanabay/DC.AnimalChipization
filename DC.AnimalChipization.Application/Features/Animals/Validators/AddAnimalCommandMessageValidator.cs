using DC.AnimalChipization.Application.Features.Animals.Enums;
using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Animals.Validators
{
    public class AddAnimalCommandMessageValidator : AbstractValidator<AddAnimalCommandMessage>
    {
        public AddAnimalCommandMessageValidator()
        {
            RuleFor(x => x.AnimalTypes)
                .NotEmpty()
                .ForEach(animalType => animalType.NotNull().Must(at => at > 0));

            RuleFor(x => x.Weight)
                .Must(x => x > 0);

            RuleFor(x => x.Length)
                .Must(x => x > 0);

            RuleFor(x => x.Height)
                .Must(x => x > 0);

            RuleFor(x => x.Gender)
                .Must(x => Enum.IsDefined(typeof(Gender), x));

            RuleFor(x => x.ChipperId)
                .Must(x => x > 0);

            RuleFor(x => x.ChippingLocationId)
                .Must(x => x > 0);
        }
    }
}
