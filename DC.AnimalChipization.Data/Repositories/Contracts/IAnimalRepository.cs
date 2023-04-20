using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;

namespace DC.AnimalChipization.Data.Repositories.Contracts;

public interface IAnimalRepository
{
    Task<List<AnimalEntity>> ListAllAsync();
    Task<List<AnimalEntity>> ListAsync(AnimalFilter filter, Paging paging);
    Task<bool> ExistsAsync(AnimalFilter filter);
    Task<AnimalEntity> GetByIdAsync(long id);
    Task<AnimalEntity> FirstOrDefaultAsync(AnimalFilter filter, bool useTracking = false);
    Task<AnimalEntity> InsertAsync(AnimalEntity entity);
    Task<AnimalEntity> UpdateAsync(AnimalEntity entity);
    Task DeleteAsync(AnimalEntity entity);
}