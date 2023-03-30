using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories;
using DC.AnimalChipization.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DC.AnimalChipization.Data;

public static class ConfigureServices
{
    public static void RegisterData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IAnimalTypeRepository, AnimalTypeRepository>();
        services.AddScoped<IAnimalLocationRepository, AnimalLocationRepository>();
        services.AddScoped<IAnimalRepository, AnimalRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}