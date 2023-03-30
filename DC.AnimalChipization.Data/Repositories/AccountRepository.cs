using DC.AnimalChipization.Data.Common.Extensions;
using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Contracts;
using DC.AnimalChipization.Data.Repositories.Filters;
using Microsoft.EntityFrameworkCore;

namespace DC.AnimalChipization.Data.Repositories
{
    public class AccountRepository : RepositoryBase<AccountEntity>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<AccountEntity>> ListAsync(AccountFilter filter, Paging paging)
        {
            return GetQuery(filter).ToPagedList(paging);
        }

        public Task<AccountEntity> GetByIdAsync(int id)
        {
            return GetQuery().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<AccountEntity> GetByEmailAsync(string email)
        {
            return GetQuery().FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public Task<AccountEntity> Authenticate(string email, string password)
        {
            return GetQuery().FirstOrDefaultAsync(x => x.Email.Equals(email) && x.Password.Equals(password));
        }

        private IQueryable<AccountEntity> GetQuery(AccountFilter filter)
        {
            var query = base.GetQuery(filter);

            if (filter == null)
            {
                return query;
            }

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                query = query.Where(x => x.Email.Contains(filter.Email));
            }

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                query = query.Where(x => x.FirstName.Contains(filter.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                query = query.Where(x => x.LastName.Contains(filter.LastName));
            }

            return query;
        }
    }
}
