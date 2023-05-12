using System.Collections.Generic;

namespace DC.AnimalChipization.WebApi.ViewModels.Areas;

public class AreaAnalyticResultViewModel
{
    public long TotalQuantityAnimals { get; set; }
    public long TotalAnimalsArrived { get; set; }
    public long TotalAnimalsGone { get; set; }
    public List<AreaAnalyticItemViewModel> AnimalsAnalytics { get; set; }
}