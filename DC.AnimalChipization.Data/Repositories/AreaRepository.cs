using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Contracts;
using DC.AnimalChipization.Data.Repositories.Filters;
using Microsoft.EntityFrameworkCore;

namespace DC.AnimalChipization.Data.Repositories;

public class AreaRepository : RepositoryBase<AreaEntity>, IAreaRepository
{
    public AreaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<List<AreaEntity>> ListAsync(AreaFilter filter)
    {
        return GetQuery(filter).ToListAsync();
    }

    public Task<AreaEntity> FirstOrDefaultAsync(AreaFilter filter)
    {
        return GetQuery(filter).FirstOrDefaultAsync();
    }
}