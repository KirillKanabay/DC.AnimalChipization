using System.Collections.Generic;

namespace DC.AnimalChipization.WebApi.ViewModels.Areas.Requests;

public class UpdateAreaRequest
{
    public string Name { get; set; }
    public List<AreaPointViewModel> AreaPoints { get; set; }
}