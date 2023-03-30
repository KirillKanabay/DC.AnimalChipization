using System.Collections.Generic;
using DC.AnimalChipization.WebApi.Attributes.Validations;

namespace DC.AnimalChipization.WebApi.ViewModels.Animals.Requests;

public record CreateAnimalRequest(
    List<long> AnimalTypes, 
    float Weight, 
    float Length, 
    float Height, 
    [GenderValidation] string Gender,
    int ChipperId,
    long ChippingLocationId);