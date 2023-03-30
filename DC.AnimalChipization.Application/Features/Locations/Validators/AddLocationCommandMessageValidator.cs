using DC.AnimalChipization.Application.Features.Locations.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Locations.Validators;

public class AddLocationCommandMessageValidator : AbstractValidator<AddLocationCommandMessage>
{
    public AddLocationCommandMessageValidator()
    {
        Include(new ChangeLocationCommandMessageValidator());
    }
}