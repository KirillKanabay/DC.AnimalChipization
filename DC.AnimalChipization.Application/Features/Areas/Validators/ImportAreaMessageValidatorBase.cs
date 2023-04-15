using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Features.Areas.Messages.Commands;
using DC.AnimalChipization.Application.Features.Areas.Validators.Helpers;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Areas.Validators;

public class ImportAreaMessageValidatorBase<TMessage> : AbstractValidator<TMessage>
    where TMessage : ImportAreaCommandMessageBase
{
    private const int AreaPointsMinCount = 3;

    public ImportAreaMessageValidatorBase()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleForEach(x => x.AreaPoints)
            .NotNull()
            .Must(IsValidPoint);

        RuleFor(x => x.AreaPoints)
            .NotNull()
            .Must(x => x.Count < AreaPointsMinCount)
            .Must(x => !AreaValidationHelper.IsLine(x))
            .Must(x => !AreaValidationHelper.HasDuplicates(x))
            .Must(x => !AreaValidationHelper.AreEdgesIntersecting(x));
    }

    private bool IsValidPoint(AreaPointDto point)
    {
        var isLatitudeValid = point.Latitude is <= 90 and >= -90;
        var isLongitudeValid = point.Longitude is <= 90 and >= -90;

        return isLatitudeValid && isLongitudeValid;
    }
}