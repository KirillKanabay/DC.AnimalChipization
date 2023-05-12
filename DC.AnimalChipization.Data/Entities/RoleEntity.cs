using DC.AnimalChipization.Data.Common.Entities;

namespace DC.AnimalChipization.Data.Entities;

public class RoleEntity : EntityBase
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<AccountEntity> Accounts { get; set; }
}