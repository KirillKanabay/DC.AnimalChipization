using DC.AnimalChipization.Application.Features.Areas.Messages.Queries;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Areas.Validators;

public class GetAreaByIdQueryMessageValidator : AbstractValidator<GetAreaByIdQueryMessage>
{
    public GetAreaByIdQueryMessageValidator()
    {
        RuleFor(x => x.Id).Must(x => x > 0);
    }
}