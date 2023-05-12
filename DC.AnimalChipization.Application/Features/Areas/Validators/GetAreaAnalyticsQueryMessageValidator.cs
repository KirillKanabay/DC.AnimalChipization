using DC.AnimalChipization.Application.Features.Areas.Messages.Queries;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Areas.Validators;

public class GetAreaAnalyticsQueryMessageValidator : AbstractValidator<GetAreaAnalyticsQueryMessage>
{
    public GetAreaAnalyticsQueryMessageValidator()
    {
        RuleFor(x => x)
            .Must(x => x.StartDate < x.EndDate);
    }
}