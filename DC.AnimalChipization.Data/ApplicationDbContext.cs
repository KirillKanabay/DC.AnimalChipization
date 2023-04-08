using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Seeding;
using Microsoft.EntityFrameworkCore;

namespace DC.AnimalChipization.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<AnimalEntity> Animals { get; set; }
        public DbSet<AnimalLocationEntity> AnimalLocations { get; set; }
        public DbSet<AnimalTypeEntity> AnimalTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AccountsSeeding.Seed(modelBuilder);
            RolesSeeding.Seed(modelBuilder);
        }
    }
}
