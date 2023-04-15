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
            .Must(IsValidPoint)
            .WithMessage("Area has invalid points");

        RuleFor(x => x.AreaPoints)
            .NotNull()
            .Must(x => x.Count >= AreaPointsMinCount)
            .WithMessage($"Area should have minimum {AreaPointsMinCount} points")
            .Must(x => !AreaValidationHelper.IsLine(x))
            .WithMessage("Given area is line")
            .Must(x => !AreaValidationHelper.HasDuplicates(x))
            .WithMessage("Area has points duplicates")
            .Must(x => !AreaValidationHelper.AreEdgesIntersecting(x))
            .WithMessage("Area has edges that intersected");
    }

    private bool IsValidPoint(AreaPointDto point)
    {
        var isLatitudeValid = point.Latitude is <= 90 and >= -90;
        var isLongitudeValid = point.Longitude is <= 180 and >= -180;

        return isLatitudeValid && isLongitudeValid;
    }
}