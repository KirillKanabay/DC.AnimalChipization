using System.Text.RegularExpressions;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DC.AnimalChipization.Data.Repositories;

public class RoleRepository : RepositoryBase<RoleEntity>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<RoleEntity> GetByNameAsync(string name)
    {
        var pattern = $"^{Regex.Escape(name)}$";

        return GetQuery().FirstOrDefaultAsync(x => Regex.IsMatch(x.Name, pattern, RegexOptions.IgnoreCase));
    }

}