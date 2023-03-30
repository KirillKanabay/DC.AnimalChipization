using DC.AnimalChipization.Application.Features.AnimalLocations.DataTransfer;
using DC.AnimalChipization.Application.Features.Animals.Enums;
using DC.AnimalChipization.Application.Features.AnimalTypes.DataTransfer;

namespace DC.AnimalChipization.Application.Features.Animals.DataTransfer
{
    public class AnimalDto
    {
        public long Id { get; set; }
        public List<AnimalTypeDto> AnimalTypes { get; set; }
        public float Weight { get; set; }
        public float Length { get; set; }
        public float Height { get; set; }
        public Gender Gender { get; set; }
        public LifeStatus LifeStatus { get; set; }
        public DateTime ChippingDateTime { get; set; }
        public int ChipperId { get; set; }
        public long ChippingLocationId { get; set; }
        public List<AnimalLocationDto> VisitedLocations { get; set; }
        public DateTime? DeathDateTime { get; set; }
    }
}
