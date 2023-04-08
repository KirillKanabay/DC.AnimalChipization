using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = DC.AnimalChipization.Application.Common.Exceptions.ValidationException;

namespace DC.AnimalChipization.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var errors = new List<ValidationFailure>();

            foreach (var validator in _validators)
            {
                var validationResult = await validator.ValidateAsync(context, cancellationToken);

                if (!validationResult.IsValid)
                {
                    errors.AddRange(validationResult.Errors);
                }
            }

            if (errors.Any())
            {
                var errorsDictionary = errors.GroupBy(x => x.PropertyName, x => x.ErrorMessage, 
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                    .ToDictionary(k => k.Key, v => v.Values);

                throw new ValidationException(errorsDictionary);
            }

            return await next();
        }
    }
}
