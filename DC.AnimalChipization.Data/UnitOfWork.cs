using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Contracts;

namespace DC.AnimalChipization.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IAccountRepository Accounts { get; }
        public ILocationRepository Locations { get; }
        public IAnimalTypeRepository AnimalTypes { get; }
        public IAnimalRepository Animals { get; }
        public IAnimalLocationRepository AnimalLocations { get; }
        
        public UnitOfWork(ApplicationDbContext context, 
            IAccountRepository accountRepository, 
            ILocationRepository locationRepository, 
            IAnimalTypeRepository animalTypeRepository,
            IAnimalRepository animalRepository,
            IAnimalLocationRepository animalLocationRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Accounts = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            Locations = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
            AnimalTypes = animalTypeRepository ?? throw new ArgumentNullException(nameof(animalTypeRepository));
            Animals = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
            AnimalLocations = animalLocationRepository ?? throw new ArgumentNullException(nameof(animalLocationRepository));
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
