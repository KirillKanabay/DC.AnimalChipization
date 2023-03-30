using DC.AnimalChipization.Data.Common.Entities;

namespace DC.AnimalChipization.Data.Common.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<List<TEntity>> ListAllAsync();
        
        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
