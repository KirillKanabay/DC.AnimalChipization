namespace DC.AnimalChipization.Application.Features.AnimalLocations.DataTransfer
{
    public class AnimalLocationDto
    {
        public long Id { get; set; }
        public DateTime VisitDateTime { get; set; }
        public long LocationPointId { get; set; }
        public long AnimalId { get; set; }
    }
}
