using System.Linq.Expressions;
using System.Text.RegularExpressions;
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
        
        private Dictionary<string, Expression<Func<AccountEntity, object>>> SortingColumns => new()
        {
            [nameof(AccountEntity.Id)] = x => x.Id
        };

        public Task<List<AccountEntity>> ListAsync(AccountFilter filter, Paging paging)
        {
            return GetQuery(filter).ToPagedList(paging, SortingColumns);
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
                var pattern = Regex.Escape(filter.Email);
                query = query.Where(x => Regex.IsMatch(x.Email, pattern, RegexOptions.IgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                var pattern = Regex.Escape(filter.FirstName);
                query = query.Where(x => Regex.IsMatch(x.FirstName, pattern, RegexOptions.IgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                var pattern = Regex.Escape(filter.LastName);
                query = query.Where(x => Regex.IsMatch(x.LastName, pattern, RegexOptions.IgnoreCase));
            }

            return query;
        }
    }
}
