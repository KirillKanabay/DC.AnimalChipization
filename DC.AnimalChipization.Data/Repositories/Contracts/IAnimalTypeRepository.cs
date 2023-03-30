using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;

namespace DC.AnimalChipization.Data.Repositories.Contracts;

public interface IAnimalTypeRepository
{
    Task<AnimalTypeEntity> FirstOrDefaultAsync(AnimalTypeFilter filter);
    Task<List<AnimalTypeEntity>> ListAsync(AnimalTypeFilter filter, Paging paging);
    Task<AnimalTypeEntity> GetByTypeAsync(string type);
    Task<AnimalTypeEntity> GetByIdAsync(long id);
    Task<AnimalTypeEntity> InsertAsync(AnimalTypeEntity animalType);
    Task<AnimalTypeEntity> UpdateAsync(AnimalTypeEntity animalType);
    Task DeleteAsync(AnimalTypeEntity animalType);
}