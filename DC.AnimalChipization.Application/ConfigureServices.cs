using DC.AnimalChipization.Application.Behaviors;
using DC.AnimalChipization.Application.Common.Behaviors;
using DC.AnimalChipization.Application.Features.Accounts.Handlers.Commands;
using DC.AnimalChipization.Application.Features.Accounts.Mappings;
using DC.AnimalChipization.Application.Features.Accounts.Validators;
using DC.AnimalChipization.Application.Identity;
using DC.AnimalChipization.Application.Identity.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DC.AnimalChipization.Application
{
    public static class ConfigureServices
    {
        public static void RegisterApplicationLogic(this IServiceCollection services)
        {
            services.AddMediatR(typeof(RegisterAccountCommandHandler));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            services.AddValidatorsFromAssembly(typeof(RegisterAccountCommandMessageValidator).Assembly);
            
            services.AddAutoMapper(typeof(AccountProfile).Assembly);
            
            services.AddScoped<IIdentityManager, IdentityManager>();
        }
    }
}
