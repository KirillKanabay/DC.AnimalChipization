using DC.AnimalChipization.Data.Common.Immutable;
using DC.AnimalChipization.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DC.AnimalChipization.Data.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.Property(x => x.Email).UseCollation(Collations.CaseInsensitive);
        builder.Property(x => x.FirstName).UseCollation(Collations.CaseInsensitive);
        builder.Property(x => x.LastName).UseCollation(Collations.CaseInsensitive);

        builder.HasOne(x => x.Role).WithMany(x => x.Accounts).HasForeignKey(x => x.RoleId);
    }
}