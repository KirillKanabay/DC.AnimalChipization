using DC.AnimalChipization.Application.Features.Areas.Messages.Commands;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Areas.Validators;

public class UpdateAreaCommandMessageValidator : ImportAreaMessageValidatorBase<UpdateAreaCommandMessage>
{
    public UpdateAreaCommandMessageValidator()
    {
        RuleFor(x => x.AreaId)
            .Must(x => x > 0);
    }
}