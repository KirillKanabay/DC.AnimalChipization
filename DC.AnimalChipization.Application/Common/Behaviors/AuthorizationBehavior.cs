using System.Reflection;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Identity.Attributes;
using DC.AnimalChipization.Application.Identity.Contracts;
using MediatR;

namespace DC.AnimalChipization.Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IIdentityManager _identityManager;

    public AuthorizationBehavior(IIdentityManager identityManager)
    {
        _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            var currentUser = _identityManager.GetCurrentUser();

            if (currentUser == null)
            {
                throw new UnauthorizedException();
            }
        }

        return next();
    }
}