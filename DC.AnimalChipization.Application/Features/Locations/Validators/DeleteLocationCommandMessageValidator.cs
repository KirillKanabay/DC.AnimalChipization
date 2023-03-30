using DC.AnimalChipization.Application.Features.Locations.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Locations.Validators;

public class DeleteLocationCommandMessageValidator : AbstractValidator<DeleteLocationCommandMessage>
{
    public DeleteLocationCommandMessageValidator()
    {
        RuleFor(x => x.PointId)
            .Must(x => x > 0);
    }
}