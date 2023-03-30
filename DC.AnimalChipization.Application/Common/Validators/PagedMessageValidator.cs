using DC.AnimalChipization.Application.Common.Messages;
using FluentValidation;

namespace DC.AnimalChipization.Application.Common.Validators
{
    public class PagedMessageValidator : AbstractValidator<PagedMessage>
    {
        public PagedMessageValidator()
        {
            RuleFor(x => x)
                .Must(x => x.Size > 0)
                .Must(x => x.From >= 0);
        }
    }
}
