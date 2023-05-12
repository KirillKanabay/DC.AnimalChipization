using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DC.AnimalChipization.Data.Repositories;

public class AreaPointRepository : RepositoryBase<AreaPointEntity>, IAreaPointRepository
{
    public AreaPointRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task DeletePointsByAreaIdAsync(long areaId)
    {
        var points = await Set.Where(x => x.AreaId.Equals(areaId)).ToListAsync();

        if (points.Any())
        {
            Set.RemoveRange(points);
        }
    }
}