using DC.AnimalChipization.Data.Common.Entities;

namespace DC.AnimalChipization.Data.Entities
{
    public class AccountEntity : EntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }

        public ICollection<AnimalEntity> ChippedAnimals { get; set; }
        public RoleEntity Role { get; set; }
    }
}
