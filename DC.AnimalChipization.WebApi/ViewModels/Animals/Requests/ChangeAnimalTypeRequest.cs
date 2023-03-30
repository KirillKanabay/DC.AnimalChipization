namespace DC.AnimalChipization.WebApi.ViewModels.Animals.Requests;

public class ChangeAnimalTypeRequest
{
    public long OldTypeId { get; set; }
    public long NewTypeId { get; set; }
}