using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Data.Repositories.Contracts;

public interface IRoleRepository
{
    Task<RoleEntity> GetByNameAsync(string name);
}