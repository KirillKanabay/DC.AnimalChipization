﻿using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;

namespace DC.AnimalChipization.Data.Repositories.Contracts;

public interface IAreaRepository
{
    Task<bool> ExistsAsync(AreaFilter filter);
    Task<List<AreaEntity>> ListAsync(AreaFilter filter);
    Task<AreaEntity> FirstOrDefaultAsync(AreaFilter filter);
    Task<AreaEntity> InsertAsync(AreaEntity entity);
    Task<AreaEntity> UpdateAsync(AreaEntity entity);
    Task DeleteAsync(AreaEntity entity);
}