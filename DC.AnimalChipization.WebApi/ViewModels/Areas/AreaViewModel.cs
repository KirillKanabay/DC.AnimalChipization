using System.Collections.Generic;

namespace DC.AnimalChipization.WebApi.ViewModels.Areas;

public class AreaViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<AreaPointViewModel> AreaPoints { get; set; } 
}