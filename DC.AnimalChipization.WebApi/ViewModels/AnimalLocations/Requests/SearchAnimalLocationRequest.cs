using System;

namespace DC.AnimalChipization.WebApi.ViewModels.AnimalLocations.Requests;

public class SearchAnimalLocationRequest : PagedRequest
{
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
}