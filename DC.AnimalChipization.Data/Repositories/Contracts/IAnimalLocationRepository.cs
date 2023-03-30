using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;

namespace DC.AnimalChipization.Data.Repositories.Contracts;

public interface IAnimalLocationRepository
{
    Task<AnimalLocationEntity> FirstOrDefaultAsync(AnimalLocationFilter filter);
    Task<AnimalLocationEntity> GetByIdAsync(long id);
    Task<List<AnimalLocationEntity>> ListAsync(AnimalLocationFilter filter, Paging paging);
    Task<AnimalLocationEntity> InsertAsync(AnimalLocationEntity entity);
    Task<AnimalLocationEntity> UpdateAsync(AnimalLocationEntity entity);
    Task DeleteAsync(AnimalLocationEntity entity);
}