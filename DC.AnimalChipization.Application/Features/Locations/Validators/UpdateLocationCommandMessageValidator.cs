using DC.AnimalChipization.Application.Features.Locations.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Locations.Validators;

public class UpdateLocationCommandMessageValidator : AbstractValidator<UpdateLocationCommandMessage>
{
    public UpdateLocationCommandMessageValidator()
    {
        Include(new ChangeLocationCommandMessageValidator());

        RuleFor(x => x.PointId)
            .Must(x => x > 0);
    }    
}