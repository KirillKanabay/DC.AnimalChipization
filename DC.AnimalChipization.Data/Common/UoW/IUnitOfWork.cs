using DC.AnimalChipization.Data.Repositories.Contracts;

namespace DC.AnimalChipization.Data.Common.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        ILocationRepository Locations { get; }
        IAnimalTypeRepository AnimalTypes { get; }
        IAnimalRepository Animals { get; }
        IAnimalLocationRepository AnimalLocations { get; }
        IRoleRepository Roles { get; }

        Task SaveChangesAsync();
        
    }
}
