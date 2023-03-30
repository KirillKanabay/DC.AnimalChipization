using DC.AnimalChipization.Data.Common.Filters;
using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Data.Repositories.Filters
{
    public class AccountFilter : FilterBase<AccountEntity>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
