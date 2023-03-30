using DC.AnimalChipization.WebApi.Attributes.Validations;

namespace DC.AnimalChipization.WebApi.ViewModels.Animals.Requests;

public class UpdateAnimalRequest
{
    public float Weight { get; set; }
    
    public float Length { get; set; }
    
    public float Height { get; set; }
    
    [GenderValidation]
    public string Gender { get; set; }

    [LifeStatusValidation]
    public string LifeStatus { get; set; }
    
    public int ChipperId { get; set; }
    
    public long ChippingLocationId { get; set; }
}