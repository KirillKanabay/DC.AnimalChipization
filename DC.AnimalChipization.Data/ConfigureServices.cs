using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories;
using DC.AnimalChipization.Data.Repositories.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DC.AnimalChipization.Data;

public static class ConfigureServices
{
    public static void RegisterData(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IAnimalTypeRepository, AnimalTypeRepository>();
        services.AddScoped<IAnimalLocationRepository, AnimalLocationRepository>();
        services.AddScoped<IAnimalRepository, AnimalRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}