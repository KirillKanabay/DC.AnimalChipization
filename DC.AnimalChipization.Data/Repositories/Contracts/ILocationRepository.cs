using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;

namespace DC.AnimalChipization.Data.Repositories.Contracts;

public interface ILocationRepository
{
    Task<bool> ExistsAsync(LocationFilter filter);
    Task<LocationEntity> FirstOrDefaultAsync(LocationFilter filter);
    Task<List<LocationEntity>> ListAsync(LocationFilter filter, Paging paging);
    Task<LocationEntity> GetByIdAsync(long id);
    Task<LocationEntity> InsertAsync(LocationEntity location);
    Task<LocationEntity> UpdateAsync(LocationEntity location);
    Task DeleteAsync(LocationEntity location);
}