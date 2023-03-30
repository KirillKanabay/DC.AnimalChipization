using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;

namespace DC.AnimalChipization.Data.Repositories.Contracts;

public interface IAccountRepository
{
    Task<List<AccountEntity>> ListAsync(AccountFilter filter, Paging paging);
    Task<AccountEntity> GetByIdAsync(int id);
    Task<AccountEntity> InsertAsync(AccountEntity account);
    Task<AccountEntity> GetByEmailAsync(string email);
    Task<AccountEntity> Authenticate(string email, string password);
    Task<AccountEntity> UpdateAsync(AccountEntity account);
    Task DeleteAsync(AccountEntity entity);
}