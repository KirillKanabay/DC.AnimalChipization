using DC.AnimalChipization.Data.Common.Extensions;
using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Contracts;
using DC.AnimalChipization.Data.Repositories.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DC.AnimalChipization.Data.Repositories;

public class AnimalRepository : RepositoryBase<AnimalEntity>, IAnimalRepository
{
    public AnimalRepository(ApplicationDbContext context) : base(context)
    {
    }

    private Dictionary<string, Expression<Func<AnimalEntity, object>>> SortingColumns => new()
    {
        [nameof(AnimalEntity.Id)] = x => x.Id
    };

    public Task<List<AnimalEntity>> ListAsync(AnimalFilter filter, Paging paging)
    {
        return GetQuery(filter).ToPagedList(paging, SortingColumns);
    }

    public Task<bool> ExistsAsync(AnimalFilter filter)
    {
        return GetQuery(filter).AnyAsync();
    }

    public Task<AnimalEntity> GetByIdAsync(long id)
    {
        return GetQuery().FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<AnimalEntity> FirstOrDefaultAsync(AnimalFilter filter, bool useTracking = false)
    {
        return GetQuery(filter, useTracking).FirstOrDefaultAsync();
    }

    public override Task<AnimalEntity> InsertAsync(AnimalEntity entity)
    {
        SetUnchangedStateForRelatedData(entity);

        return base.InsertAsync(entity);
    }

    public override Task<AnimalEntity> UpdateAsync(AnimalEntity entity)
    {
        if (Context.Entry(entity).State == EntityState.Detached)
        {
            Context.Attach(entity);
        }

        SetUnchangedStateForRelatedData(entity);

        return base.UpdateAsync(entity);
    }

    private IQueryable<AnimalEntity> GetQuery(AnimalFilter filter, bool useTracking = false)
    {
        var query = base.GetQuery(filter, useTracking);

        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id);
        }

        if (filter.StartDateTime.HasValue)
        {
            query = query.Where(x => x.ChippingDateTime >= filter.StartDateTime);
        }

        if (filter.EndDateTime.HasValue)
        {
            query = query.Where(x => x.ChippingDateTime <= filter.EndDateTime);
        }

        if (filter.ChipperId.HasValue)
        {
            query = query.Where(x => x.ChipperId == filter.ChipperId);
        }

        if (filter.ChippingLocationId.HasValue)
        {
            query = query.Where(x => x.ChippingLocationId == filter.ChippingLocationId);
        }

        if (filter.LifeStatus.HasValue)
        {
            query = query.Where(x => x.LifeStatus == filter.LifeStatus);
        }

        if (filter.Gender.HasValue)
        {
            query = query.Where(x => x.Gender == filter.Gender);
        }

        return query;
    }

    private void SetUnchangedStateForRelatedData(AnimalEntity entity)
    {
        if (entity.AnimalTypes?.Any() ?? false)
        {
            entity.AnimalTypes.ForEach(at => Context.Entry(at).State = EntityState.Unchanged);
        }

        if (entity.VisitedLocations?.Any() ?? false)
        {
            entity.VisitedLocations.ForEach(at => Context.Entry(at).State = EntityState.Unchanged);
        }

        if (entity.Chipper != null)
        {
            Context.Entry(entity.Chipper).State = EntityState.Unchanged;
        }

        if (entity.ChippingLocation != null)
        {
            Context.Entry(entity.ChippingLocation).State = EntityState.Unchanged;
        }
    }
}