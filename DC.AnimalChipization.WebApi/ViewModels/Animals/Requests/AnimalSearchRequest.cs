using System;
using DC.AnimalChipization.WebApi.Attributes.Validations;

namespace DC.AnimalChipization.WebApi.ViewModels.Animals.Requests;

public class AnimalSearchRequest : PagedRequest
{
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public int? ChipperId { get; set; }
    public long? ChippingLocationId { get; set; }

    [LifeStatusValidation(AllowNull = true)]
    public string LifeStatus { get; set; }
    
    [GenderValidation(AllowNull = true)]
    public string Gender { get; set; }
}