using System;

namespace DC.AnimalChipization.WebApi.ViewModels.AnimalLocations
{
    public class AnimalLocationViewModel
    {
        public long Id { get; set; }
        public DateTime DateTimeOfVisitLocationPoint { get; set; }
        public long LocationPointId { get; set; }
    }
}
