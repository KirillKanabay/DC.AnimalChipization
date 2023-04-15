using System.Collections.Generic;

namespace DC.AnimalChipization.WebApi.ViewModels.Areas.Requests;

public class CreateAreaRequest
{
    public string Name { get; set; }
    public List<AreaPointViewModel> AreaPoints { get; set; }
}