using DC.AnimalChipization.Data.Common.Immutable;
using DC.AnimalChipization.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DC.AnimalChipization.Data.Seeding;

public static class RolesSeeding
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<RoleEntity>().HasData(new[]
        {
            new RoleEntity
            {
                Id = RoleId.User,
                Name = "USER"
            },
            new RoleEntity
            {
                Id = RoleId.Admin,
                Name = "ADMIN"
            },
            new RoleEntity
            {
                Id = 3,
                Name = "CHIPPER"
            },
        });
    }
}