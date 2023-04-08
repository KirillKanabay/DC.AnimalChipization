using DC.AnimalChipization.Data.Common.Immutable;
using DC.AnimalChipization.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DC.AnimalChipization.Data.Seeding;

public static class AccountsSeeding
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<AccountEntity>().HasData(new []
        {
            new AccountEntity
            {
                Id = 1,
                FirstName = "adminFirstName",
                LastName = "adminLastName",
                Email = "admin@simbirsoft.com",
                Password = "qwerty123",
                RoleId = RoleId.Admin
            },
            new AccountEntity
            {
                Id = 2,
                FirstName = "chipperFirstName",
                LastName = "chipperLastName",
                Email = "chipper@simbirsoft.com",
                Password = "qwerty123",
                RoleId = RoleId.Chipper
            },
            new AccountEntity
            {
                Id = 3,
                FirstName = "userFirstName",
                LastName = "userLastName",
                Email = "user@simbirsoft.com",
                Password = "qwerty123",
                RoleId = RoleId.User
            },
        });
    }
}